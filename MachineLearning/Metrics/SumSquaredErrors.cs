using System;
using System.Linq;

namespace DotNetML.Metrics
{
    public class SumSquaredErrors
    {
        private double _sumSquaredError;


        public SumSquaredErrors(double[] predictions, double[] actual)
        {
            double[] squaredErrors = CalculateSquaredErrors(predictions, actual);
            _sumSquaredError = squaredErrors.Sum();
        }

        
        public double GetResult()
        {
            return _sumSquaredError;
        }


        private double[] CalculateSquaredErrors(double[] predictions, double[] actual)
        {
            if (predictions.Length != actual.Length) 
            {
                throw new System.Exception("arrays have different length");
            }

            double[] squaredErrors = new double[predictions.Length];

            for (int predictionIndex = 0; predictionIndex < predictions.Length; predictionIndex++)
            {
                double squaredError = Math.Pow(actual[predictionIndex] - predictions[predictionIndex], 2);
                squaredErrors[predictionIndex] = squaredError;
            }

            return squaredErrors;
        }
    }
}