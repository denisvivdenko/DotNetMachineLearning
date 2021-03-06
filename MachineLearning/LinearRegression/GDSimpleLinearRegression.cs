using System;
using System.Collections.Generic;
using DotNetML.LinearAlgebra;

namespace DotNetML.LinearRegression
{
    public class GDSimpleLinearRegression
    {
        /*
            Gradient Descent Simple Linear Regression
        */
        
        private double _slope;
        private double _intercept;
        private bool _isTrained = false;   
        private double _learningRate;
        private double _stepSizeThreshold;
        private double _maxEpohs;
        

        public GDSimpleLinearRegression(double learningRate=0.01, 
                double stepSizeThreshold=0.01, int maxEpohs=1000)
        {
            _learningRate = learningRate;
            _stepSizeThreshold = stepSizeThreshold;
            _maxEpohs = maxEpohs;
        }

        
        public void TrainModel(double[] data, double[] target)
        {
            (_slope, _intercept) = FindBestParameters(data, target);
            _isTrained = true;
        }


        public double PredictTarget(double input)
        {
            if (!_isTrained)
            {
                throw new System.Exception("model isn't trained");
            }

            double prediction = _slope * input + _intercept;
            return prediction;
        }


        public double[] PredictTargets(double[] inputs)
        {
            List<double> predictions = new List<double>();

            foreach (double input in inputs)
            {
                var prediction = PredictTarget(input);
                predictions.Add(prediction);
            }

            return predictions.ToArray();
        }


        public void PrintParameters()
        {
            Console.WriteLine($"Slope: {_slope} Interception: {_intercept}");
        }


        private (double slope, double intercept) FindBestParameters(double[] data, double[] target)
        {
            (double slope, double intercept) parameters = (1, 0);
            for (int epoh=0; epoh < _maxEpohs; epoh++)
            {
                (double slopeGradient, double interceptGradient) gradient = ComputeGradientDescent(parameters, data, target);
                (double slope, double intercept) newParameters = MakeStep(parameters, gradient);

                Vector oldParametersVector = new Vector(new double[] { parameters.slope, parameters.intercept });
                Vector newParametersVector = new Vector(new double[] { newParameters.slope, newParameters.intercept });
                double stepSize = oldParametersVector.ComputeDistance(newParametersVector);

                parameters = newParameters;

                if (stepSize < _stepSizeThreshold)
                {
                    return parameters;
                }
            }

            return parameters;
        }


        private (double slopeGradient, double interceptGradient) ComputeGradientDescent((double slope, double intercept) parameters,
                                                                                         double[] data, double[] target)
        {
            int subsetLength = data.Length;
            (double slopeGradient, double interceptGradient) gradient = (0, 0);
            for (int pointIndex=0; pointIndex < subsetLength; pointIndex++)
            {
                (double dataPoint, double target) currentPoint = (data[pointIndex], target[pointIndex]);

                double slopeGradient = -2 * currentPoint.dataPoint * (currentPoint.target - (parameters.slope * currentPoint.dataPoint + parameters.intercept));
                double interceptGradient = -2 * (currentPoint.target - (parameters.slope * currentPoint.dataPoint + parameters.intercept)); 
                (double slopeGradient, double interceptGradient) nextGradient = (slopeGradient, interceptGradient);
                
                gradient = CombineGradients(gradient, nextGradient);
            }

            return gradient;
        }


        private (double slopeGradient, double interceptGradient) CombineGradients((double slopeGradient, double interceptGradient) firstGradient,
                                    (double slopeGradient, double interceptGradient) secondGradient)
        {
            return (firstGradient.slopeGradient + secondGradient.slopeGradient, 
                    firstGradient.interceptGradient + secondGradient.interceptGradient);
        }


        private (double slope, double intercept) MakeStep((double slope, double intercept) parameters,
                                                                (double slopeGradient, double interceptGradient) gradient)
        {
            double newSlope = parameters.slope - gradient.slopeGradient * _learningRate;
            double newIntercept = parameters.intercept - gradient.interceptGradient * _learningRate;

            return (newSlope, newIntercept);
        }
    }
}