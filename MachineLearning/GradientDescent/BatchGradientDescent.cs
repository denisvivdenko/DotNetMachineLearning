using DotNetML.LinearAlgebra;
using DotNetML.LinearRegression;
using System;


namespace DotNetML.GradientDescent
{
    public class BatchGradientDescent
    {
        private double[] _bestParameters;
        private int _maxEpohs;
        private double _stepSizeThreshold;
        private double _learningRate;


        public BatchGradientDescent(MultipleRegressionCostFunctionGradient costFunctionGradient, double[] startParameters,
                                    double learningRate=0.01, double stepSizeThreshold=0.01, int maxEpohs=1000)
        {
            _learningRate = learningRate;
            _stepSizeThreshold = stepSizeThreshold;
            _maxEpohs = maxEpohs;
            _bestParameters = FindBestParameters(costFunctionGradient, startParameters);
        }


        public double[] GetResult()
        {
            return _bestParameters;
        }


        private double[] FindBestParameters(MultipleRegressionCostFunctionGradient costFunctionGradient, double[] startParameters)
        {
            double[] currentParameters = startParameters;
            for (int epoh = 0; epoh < _maxEpohs; epoh++)
            {
                costFunctionGradient = costFunctionGradient.SetNewParameters(currentParameters);
                double[] newParameters = MakeStepToFunctionMinimum(currentParameters, costFunctionGradient.GetResult(), _learningRate);

                double stepSize = new Vector(currentParameters).ComputeDistance(new Vector(newParameters));
                currentParameters = newParameters;
                if (stepSize <= _stepSizeThreshold)
                {
                    return currentParameters;
                }
            }

            throw new Exception("maxEpohs are less than current algorithm needs");
        }


        private double[] MakeStepToFunctionMinimum(double[] parameters, double[] gradient, double learningRate)
        {
            double[] newParameters = new double[parameters.Length];
            for (int parameterIndex = 0; parameterIndex < parameters.Length; parameterIndex++)
            {
                double parameter = parameters[parameterIndex];
                double direction = gradient[parameterIndex];
                newParameters[parameterIndex] = parameter - direction * learningRate;
            }

            return newParameters;
        }
    }
}