﻿using System;

namespace DotNetML.MathFunctions
{
    public class ErrorFunction
	{
        private double _errorFunctionResult;


        public ErrorFunction(double x)
		{
            _errorFunctionResult = ComputeErrorFunction(x);
		}


        public double GetResult()
		{
            return _errorFunctionResult;
		}


        private double ComputeErrorFunction(double x)
        {
            double a1 = 0.254829592;
            double a2 = -0.284496736;
            double a3 = 1.421413741;
            double a4 = -1.453152027;
            double a5 = 1.061405429;
            double p = 0.3275911;

            int sign = 1;
            if (x < 0)
                sign = -1;
            
            x = Math.Abs(x);

            double t = 1.0 / (1.0 + p * x);
            double y = 1.0 - (((((a5 * t + a4) * t) + a3) * t + a2) * t + a1) * t * Math.Exp(-x * x);

            return sign * y;
        }
	}
    
}



