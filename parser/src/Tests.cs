using System;

namespace Parser
{
    public class Tests
    {
        private int totalNumber;

        private int failedNumber;

        public void TestAll()
        {
            totalNumber = 0;
            failedNumber = 0;

            TestNumberResults();
            TestCorrectness();
            TestSimpleCases();
            TestDifficultCase();

            Console.WriteLine("Total number of tests:  " + totalNumber.ToString());
            Console.WriteLine("Number of failed tests: " + failedNumber.ToString());
        }

        private void Check(String expression, String expectedResult)
        {
            totalNumber++;
            String actualResult = Parser.Eval(expression);

            if (actualResult != expectedResult)
            {
                failedNumber++;

                Console.WriteLine("Expression: " + expression);
                Console.WriteLine("> Expected: " + expectedResult);
                Console.WriteLine("> Actually: " + actualResult + "\n");
            }
        }

        private void CheckLength(String expression, int expectedLength)
        {
            totalNumber++;
            int actualLength = Parser.Eval(expression).Length;

            if (actualLength != expectedLength)
            {
                failedNumber++;

                Console.WriteLine("Expression: " + expression);
                Console.WriteLine("> Expected length: " + expectedLength);
                Console.WriteLine("> Actual length:   " + actualLength + "\n");
            }
        }

        private void TestNumberResults()
        {
            Check("721", "721");
            Check("73 - 74", "-1");
            Check("(1 + 2) * 2", "6");
            Check("(1 + 7 * 3 + (1 - 2) * (-1)) * 2 - 10", "36");
            Check("1 / 2 * 2 * 4 / 4 * 2 + 1", "3");
            Check("7 - 3 + +--3", "7");
            Check("1 + (7 - 3) * (2 / 2 + 1) * (1 + (2 + 3) * 2)", "89");
        }

        String Substitute(String expression, String variable, String value)
        {
            return expression.Replace(variable, value);
        }

        private void TestCorrectness(String expression)
        {
            Random random = new Random();
            String aValue = random.Next(-1000, 1000).ToString();
            String bValue = random.Next(-1000, 1000).ToString();

            String specExpression = Substitute(Substitute(expression, "a", aValue), "b", bValue);
            String evaled = Parser.Eval(expression);
            String specEvaled = Substitute(Substitute(evaled, "a", aValue), "b", bValue);
            Check(specExpression, Parser.Eval(specEvaled));
        }

        private void TestCorrectness()
        {
            for (int i = 0; i < 10; i++) {
                TestCorrectness("a * (1 + 2) * b * 3 / 1 + 7 - 123 * (a + b) / 3 + 14");
                TestCorrectness("a * b * c + 7 + 3 * 2");
                TestCorrectness("a * b * (12 / 3 + 2)");
                TestCorrectness("a + b - b + a - b * 2 * 14 / 2 + 7 * (2 + a + c + c * b + 3 * (1 - 3 - a))");
                TestCorrectness("a + b * (1 + a + 10 + b * 10 / 2 + b * (2 - 3 + 2 * 6 - a)) - 8");
            }
        }

        private void TestSimpleCases()
        {
            CheckLength("7 + a + 10 + 13 - 12 * 2", 5);
            CheckLength("a + b + c + d", 13);
            CheckLength("-3 + 2 * a * 7 * 3 + 7", 10);
        }

        private void TestDifficultCase()
        {
            CheckLength("1 + 2 * 3 / (1 + 7 - b - 100 * 10 / c - d + (7 + e) * (2 - 3 + f) + 10 * a) * 2 - 3 * f", 69);
        }
    }
}