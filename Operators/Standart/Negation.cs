﻿using System;

namespace Calc
{
    /// <summary>
    /// Умножение на -1
    /// </summary>
    internal sealed class Negation : IOperation
    {
        public double Calculate(params double[] parameters)
        {
            if (parameters == null)
                throw new ArgumentNullException();
            if (parameters.Length != 1)
                throw new ArgumentException("It's unary operation");

            return (-parameters[0]);
        }
    }
}

