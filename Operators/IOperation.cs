using System;

namespace Calc
{
    public interface IOperation
    {
        /// <summary>
        /// Returns a result of operator calculation
        /// </summary>
        /// <param name="parameters">Parameters.</param>
        double Calculate(params double[] parameters);


    }
}
