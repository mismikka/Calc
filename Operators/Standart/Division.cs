using System;

namespace Calc
{
    internal sealed class Division : IOperation
    {
        public double Calculate(params double[] parameters)
        {
            if (parameters == null)
                throw new ArgumentNullException();
            if (parameters.Length != 2)
                throw new ArgumentException("It's binary opreation");
          
            return parameters[0] / parameters[1];
        }
    }
}
