using System;

namespace Parser
{
    public static class Operations
    {
        public static Func<double, double, double> getBinary(char operatorSymbol)
        {
            switch (operatorSymbol)
            {
                case '+':
                    return ((first, second) => first + second);
                case '-':
                    return ((first, second) => first - second);
                case '*':
                    return ((first, second) => first * second);
                case '/':
                    return ((first, second) => first / second);
                default:
                    throw new FormatException("Unexpected operator.");
            }
        }

        public static Func<double, double> getUnary(char operatorSymbol)
        {
            switch (operatorSymbol)
            {
                case '+':
                    return ((value) => value);
                case '-':
                    return ((value) => -value);
                default:
                    throw new FormatException("Unexpected operator.");
            }
        }

        public static int getType(char operatorSymbol)
        {
            switch (operatorSymbol)
            {
                case '+':
                    return 0;
                case '-':
                    return 0;
                case '*':
                    return 1;
                case '/':
                    return 1;
                default:
                    return -1;
            }
        }
    }
}