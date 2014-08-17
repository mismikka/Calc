using System;

namespace Calc
{
    /// <summary>
    /// Деление
    /// </summary>
    internal sealed class Division : IOperation
    {
        public double Calculate(params double[] parameters)
        {
            if (parameters == null)
                throw new ArgumentNullException();
            if (parameters.Length != 2)
                throw new ArgumentException("It's binary opreation");
            if (parameters[1] == 0)
                throw new DivideByZeroException("Divide by zero");
          
            return parameters[0] / parameters[1];
        }
    }
}
