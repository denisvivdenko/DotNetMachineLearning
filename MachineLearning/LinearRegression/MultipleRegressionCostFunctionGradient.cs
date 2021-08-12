using System.Linq;
using DotNetML.GradientDescent;
using DotNetML.ModelSelection;

namespace DotNetML.LinearRegression
{
    public class MultipleRegressionCostFunctionGradient : CostFunctionGradient
    {
        private MultipleRegressionEquation _equation;


        public MultipleRegressionCostFunctionGradient(TrainingDataset trainingData, double[] coefficients)
                                : base(trainingData, coefficients)
        {
            _equation = new MultipleRegressionEquation(coefficients);
            _gradient = ComputeGradient(coefficients, trainingData);
        }


        public override CostFunctionGradient SetNewParameters(double[] paramaters)
		{
            return new MultipleRegressionCostFunctionGradient(_trainingData, paramaters);
		}

        
        private double[] ComputeGradient(double[] coefficients, TrainingDataset trainingData)
        {
            double[][] data = trainingData.Data;
            double[] target = trainingData.Target;
            
            double[] gradient = new double[coefficients.Length];

            int recordsNumber = data.GetLength(0);
            for (int coefficientIndex = 0; coefficientIndex < coefficients.Length; coefficientIndex++)
            {
                double coefficientGradient = 0;
                for (int recordIndex = 0; recordIndex < recordsNumber; recordIndex++)
                {
                    double[] inputVector = data[recordIndex];
                    double prediction = _equation.PredictTarget(inputVector);
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