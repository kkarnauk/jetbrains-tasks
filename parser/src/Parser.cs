using System;
using System.IO;

namespace Parser
{
    public class Parser
    {
        private Tokenizer tokenizer_;

        private Parser(String expression)
        {
            tokenizer_ = new Tokenizer(new StringReader(expression));
        }

        private Node ParseExpression()
        {
            Node result = ParseAddSubstract();
            if (tokenizer_.Token != Token.EOF)
            {
                throw new FormatException("Unexpected characters.");
            }
            return result;
        }

        private Node ParseAddSubstract()
        {
            Result result = new Result(0);
            result.Merge(ParseMultiplyDivide(), '+');

            while (true) 
            {
                char operatorSymbol = '\0';
                if (tokenizer_.Token == Token.Add)
                {
                    operatorSymbol = '+';
                }
                else if (tokenizer_.Token == Token.Substract)
                {
                    operatorSymbol = '-';
                }
                if (operatorSymbol == '\0')
                {
                    return result.GetResult();
                }

                tokenizer_.NextToken();
                Node node = ParseMultiplyDivide();
                result.Merge(node, operatorSymbol);
            }
        }

        private Node ParseMultiplyDivide()
        {
            Result result = new Result(1);
            result.Merge(ParseUnary(), '*');

            while (true)
            {
                char operatorSymbol = '\0';
                if (tokenizer_.Token == Token.Multiply)
                {
                    operatorSymbol = '*';
                }
                else if (tokenizer_.Token == Token.Divide)
                {
                    operatorSymbol = '/';
                }
                if (operatorSymbol == '\0')
                {
                    return result.GetResult();
                }

                tokenizer_.NextToken();
                Node node = ParseUnary();
                result.Merge(node, operatorSymbol);
            }
        }

        private Node ParseUnary()
        {
            if (tokenizer_.Token == Token.Add)
            {
                tokenizer_.NextToken();
                return ParseUnary();
            }
            if (tokenizer_.Token == Token.Substract)
            {
                tokenizer_.NextToken();
                Node node = ParseUnary();
                return new NodeUnary(node, '-');
            }

            return ParseLeaf();
        }

        private Node ParseLeaf()
        {
            Node node = null;
            if (tokenizer_.Token == Token.OpenParen)
            {
                tokenizer_.NextToken();
                node = ParseAddSubstract();
                node.Enclose();
                if (tokenizer_.Token != Token.CloseParen) 
                {
                    throw new FormatException("Expected closing paren but not got.");
                }
            }
            else if (tokenizer_.Token == Token.Variable)
            {
                node = new NodeVariable(tokenizer_.Variable);
            }
            else 
            {
                node = new NodeNumber(tokenizer_.Number);
            }
            tokenizer_.NextToken();
            return node;
        }

        private class Result
        {
            private Node calcedPart;
            private String variablePart;

            private bool calcedPartChanged;

            public Result(double initValue)
            {
                calcedPart = new NodeNumber(initValue);
                variablePart = "";
                calcedPartChanged = false;
            }

            public void Merge(Node node, char operatorSymbol)
            {
                if (node.HasVariable)
                {
                    variablePart += " " + operatorSymbol + " " + node.Eval();
                }
                else
                {
                    calcedPart = new NodeBinary(calcedPart, node, operatorSymbol);
                    calcedPartChanged = true;
                }
            }

            public Node GetResult() 
            {
                if (variablePart == "")
                {
                    return calcedPart;
                }
                else if (!calcedPartChanged)
                {
                    return new NodeVariable(variablePart.Remove(0, 3));
                }
                else 
                {
                    return new NodeVariable(calcedPart.Eval() + variablePart);
                }
            }
        }
    
        public static String Eval(String expression)
        {
            return (new Parser(expression)).ParseExpression().Eval();
        }
    }
}