using System;

namespace Parser 
{
    public abstract class Node 
    {
        protected bool hasVariable_;

        protected bool toBeEnclosed_;

        public Node()
        {
            toBeEnclosed_ = false;
        }

        public abstract double EvalNumericalValue();

        protected abstract String EvalStringValue();

        public String Eval()
        {
            String result = EvalStringValue();
            if (toBeEnclosed_)
            {
                result = '(' + result + ')';
            }
            return result;
        }

        public bool HasVariable
        {
            get { return hasVariable_; }
        }

        public void Enclose()
        {
            toBeEnclosed_ = true;
        }
    }

    public sealed class NodeNumber : Node 
    {
        private double number_;

        public NodeNumber(double number)
        {
            number_ = number;

            hasVariable_ = false;
        }

        public override double EvalNumericalValue()
        {
            return number_;
        }

        protected override String EvalStringValue()
        {
            return number_.ToString();
        }
    }

    public sealed class NodeBinary : Node
    {
        private Node left_;
        private Node right_;
        char operatorSymbol_;

        public NodeBinary(Node left, Node right, char operatorSymbol) 
        {
            left_ = left;
            right_ = right;
            operatorSymbol_ = operatorSymbol;

            hasVariable_ = (left_.HasVariable | right_.HasVariable);
        }

        public override double EvalNumericalValue()
        {
            double leftValue = left_.EvalNumericalValue();
            double rightValue = right_.EvalNumericalValue();
            return Operations.getBinary(operatorSymbol_)(leftValue, rightValue);
        }

        protected override String EvalStringValue()
        {
            if (!left_.HasVariable && !right_.HasVariable)
            {
                return EvalNumericalValue().ToString();
            }
            return left_.Eval() + ' ' + operatorSymbol_ + ' ' + right_.Eval();
        }
    }

    public sealed class NodeUnary : Node 
    {        
        private Node node_;
        char operatorSymbol_;
        public NodeUnary(Node node, char operatorSymbol)
        {
            node_ = node;
            operatorSymbol_ = operatorSymbol;

            hasVariable_ = node_.HasVariable;
        }

        public override double EvalNumericalValue()
        {
            return Operations.getUnary(operatorSymbol_)(node_.EvalNumericalValue());
        }

        protected override String EvalStringValue()
        {
            return operatorSymbol_ + node_.Eval();
        }
    }

    public sealed class NodeVariable : Node
    {
        String name_;

        public NodeVariable(String name)
        {
            name_ = name;

            hasVariable_ = true;
        }

        public override double EvalNumericalValue()
        {
            throw new InvalidOperationException("Cannot call this method in a variable node.");
        }

        protected override String EvalStringValue()
        {
            return name_;
        }
    }
}