using System;

namespace Calc
{
    /// <summary>
    /// Сложение
    /// </summary>
    internal sealed class Addition : IOperation
    {
        public double Calculate(params double[] parameters)
        {
            if (parameters == null)
                throw new ArgumentNullException();
            if (parameters.Length != 2)
                throw new ArgumentException("It's binary operation");

            return parameters[0] + parameters[1];
        }
    }
}

