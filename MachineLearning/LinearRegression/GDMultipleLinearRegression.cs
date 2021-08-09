using DotNetML.GradientDescent;
using System;
using System.Collections.Generic;
using System.Linq;


namespace DotNetML.LinearRegression
{
    public class GDMultipleLinearRegression
    {         
        private MultipleLinearRegression _regressor;
        private double _learningRate;
        private double _stepSizeThreshold;
        private int _maxEpohs;
        private bool _isTrained = false; 


        public GDMultipleLinearRegression(double learningRate=0.01, 
                double stepSizeThreshold=0.01, int maxEpohs=1000)
        {
            _learningRate = learningRate;
            _stepSizeThreshold = stepSizeThreshold;
            _maxEpohs = maxEpohs;
        }

        
        public void TrainModel(double[][] data, double[] target)
        {
            var parameters = SearchBestParameters(data, target);
            _regressor = new MultipleLinearRegression(parameters.regressionCoefficients, parameters.intercept);
            _isTrained = true;
        }


        public double PredictTarget(double[] inputVector)
        {
            if (!_isTrained)
            {
                throw new System.Exception("model isn't trained");
            }

            return _regressor.PredictTarget(inputVector);
        }


        public double[] PredictTargets(double[][] inputs)
        {
            List<double> predictions = new List<double>();
            foreach (double[] input in inputs)
            {
                var prediction = PredictTarget(input);
                predictions.Add(prediction);
            }

            return predictions.ToArray();
        }


        private (double[] regressionCoefficients, double intercept) SearchBestParameters(double[][] data, double[] target)
        {
            double[] startParameters = new double[data[0].Length];
            (double[][] data, double[] target) trainingData = (data, target);
            var costFunctionGradient = new MultipleRegressionCostFunctionGradient(trainingData, startParameters);

            var gradientDescent = new BatchGradientDescent(costFunctionGradient, startParameters, learningRate:_learningRate, 
                                            stepSizeThreshold:_stepSizeThreshold, maxEpohs:_maxEpohs);
            double[] foundBestParameters = gradientDescent.GetResult();
            (double[] regressionCoefficients, double intercept) regressionParameters = (foundBestParameters.SkipLast(1).ToArray(), foundBestParameters[foundBestParameters.Length-1]);

            return regressionParameters;
        }
    }
}