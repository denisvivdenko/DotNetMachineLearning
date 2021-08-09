using System.Linq;

namespace DotNetML.LinearRegression
{
    public class MultipleRegressionCostFunctionGradient
    {
        private double[] _gradient;
        private MultipleLinearRegression _regressor;
        private (double[][] data, double[] target) _trainingData;


        public MultipleRegressionCostFunctionGradient((double[][] data, double[] target) trainingData, double[] coefficients)
        {
            _regressor = new MultipleLinearRegression(coefficients);
            _trainingData = trainingData;
            _gradient = ComputeGradient(coefficients, trainingData.data, trainingData.target);
        }


        public double[] GetResult()
        {
            return _gradient;
        }


        public MultipleRegressionCostFunctionGradient SetNewParameters(double[] paramaters)
		{
            return new MultipleRegressionCostFunctionGradient(_trainingData, paramaters);
		}

        
        private double[] ComputeGradient(double[] coefficients, double[][] data, double[] target)
        {
            double[] gradient = new double[data.GetLength(1)];

            int recordsNumber = data.GetLength(0);
            for (int coefficientIndex = 0; coefficientIndex < coefficients.Length; coefficientIndex++)
            {
                double coefficientGradient = 0;
                for (int recordIndex = 0; recordIndex < recordsNumber; recordIndex++)
                {
                    double[] inputVector = data[recordIndex];
                    double prediction = _regressor.PredictTarget(inputVector);
                    double actual = target[recordIndex];

                    if (coefficientIndex == coefficients.Length - 1)
                    {
                        coefficientGradient = (-2) * (actual - prediction);
                        break;
                    }

                    coefficientGradient += (-2) * inputVector[coefficientIndex] * (actual - prediction); 
                }
                gradient[coefficientIndex] = coefficientGradient; 
            }

            return gradient;
        }
    }
}