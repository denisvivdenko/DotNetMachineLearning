using System.Linq;

namespace DotNetML.LinearRegression
{
    public class MultipleLinearRegression
    {
        private double[] _regressionCoefficients;
        private double _intercept;          


        public MultipleLinearRegression(double[] regressionCoefficients, double intercept)
        {
            _regressionCoefficients = regressionCoefficients;
            _intercept = intercept;
        }


        public MultipleLinearRegression(double[] coefficients)
        {
            _regressionCoefficients = coefficients.SkipLast(1).ToArray();
            _intercept = coefficients[coefficients.Length - 1];
        }


        public double PredictTarget(double[] inputVector)
        {
            return ComputeRegressionEquation(_regressionCoefficients, _intercept, inputVector);
        }


        private double ComputeRegressionEquation(double[] regressionCoefficients, double intercept, double[] inputVector)
        {
            double weightedValuesSum = 0;
            for (int featureIndex = 0; featureIndex < inputVector.Length; featureIndex++) 
            {
                double feature = inputVector[featureIndex];
                double betaCoefficient = regressionCoefficients[featureIndex];
                weightedValuesSum += betaCoefficient * feature;
            }

            double prediction = weightedValuesSum + intercept;

            return prediction;
        }
    }
}