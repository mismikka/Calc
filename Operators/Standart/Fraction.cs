using System;

namespace Calc
{
    /// <summary>
    /// Деление 1 на x
    /// </summary>
    internal sealed class Fraction : IOperation
    {
        public double Calculate(params double[] parameters)
        {
            if (parameters == null)
                throw new ArgumentNullException();
            if (parameters.Length != 1)
                throw new ArgumentException("It's unary operation");

            return 1 / parameters[0];
        }
    }
}

