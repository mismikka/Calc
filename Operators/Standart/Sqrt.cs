using System;

namespace Calc
{
    public class Sqrt : IOperation
    {
        public double Calculate(params double[] parameters)
        {
            if (parameters == null)
                throw new ArgumentNullException();
            if (parameters.Length != 1)
                throw new ArgumentException("It's unary operation");
            if (parameters[0] < 0)
                throw new ArgumentException("Operand must be equal or higher 0");

            return Math.Sqrt(parameters[0]);
        }
    }
}
