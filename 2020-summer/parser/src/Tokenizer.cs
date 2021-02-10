using System;
using System.Text;
using System.IO;

namespace Parser
{
    public class Tokenizer
    {
        private TextReader textReader_;
        private char currentChar_;
        private Token currentToken_;
        private double number_;
        private String variable_;

        public Tokenizer(TextReader textReader) 
        {
            textReader_ = textReader;
            NextChar();
            NextToken();
        }

        public Token Token 
        {
            get { return currentToken_; }
        }
        
        public double Number 
        {
            get { return number_; }
        }

        public String Variable
        {
            get { return variable_; }
        }

        private void NextChar()
        {
            int symbol = textReader_.Read();
            if (symbol < 0)
                currentChar_ = '\0';
            else
                currentChar_ = (char)symbol;
        }

        public void NextToken()
        {
            while (char.IsWhiteSpace(currentChar_))
            {
                NextChar();
            }
            switch (currentChar_)
            {
                case '\0':
                    currentToken_ = Token.EOF;
                    return;
                case '+':
                    NextChar();
                    currentToken_ = Token.Add;
                    return;
                case '-':
                    NextChar();
                    currentToken_ = Token.Substract;
                    return;
                case '*':
                    NextChar();
                    currentToken_ = Token.Multiply;
                    return;
                case '/':
                    NextChar();
                    currentToken_ = Token.Divide;
                    return;
                case '(':
                    NextChar();
                    currentToken_ = Token.OpenParen;
                    return;
                case ')':
                    NextChar();
                    currentToken_ = Token.CloseParen;
                    return;
            }
            if (char.IsDigit(currentChar_)) 
            {
                StringBuilder stringBuilder = new StringBuilder();
                while (char.IsDigit(currentChar_)) 
                {
                    stringBuilder.Append(currentChar_);
                    NextChar();
                }
                number_ = int.Parse(stringBuilder.ToString());
                currentToken_ = Token.Number;

                return;
            }
            if (char.IsLetter(currentChar_))
            {
                StringBuilder stringBuilder = new StringBuilder();
                while (char.IsLetter(currentChar_))
                {
                    stringBuilder.Append(currentChar_);
                    NextChar();
                }
                variable_ = stringBuilder.ToString();
                currentToken_ = Token.Variable;

                return;
            }
            throw new FormatException("Unexpected character.");
        }
    }
}