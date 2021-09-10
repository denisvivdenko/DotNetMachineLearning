using System.Collections.Generic;
using System.Linq;
using DotNetML.ModelSelection;

namespace DotNetML.LinearRegression
{
    public class MultipleRegressionEquation : Model
    {
        private double[] _regressionCoefficients;
        private double _intercept;          


        public MultipleRegressionEquation() { }


        public MultipleRegressionEquation(double[] regressionCoefficients, double intercept)
        {
            _regressionCoefficients = regressionCoefficients;
            _intercept = intercept;
        }


        public MultipleRegressionEquation(double[] coefficients)
        {
            _regressionCoefficients = coefficients.SkipLast(1).ToArray();
            _intercept = coefficients[coefficients.Length - 1];
        }


        public override void TrainModel(TrainingDataset trainingDataset)
        {
            throw new System.NotImplementedException();
        }


        public override double[] PredictTarget(double[] inputVector)
        {
            return new double[] { ComputeRegressionEquation(_regressionCoefficients, _intercept, inputVector) };
        }


        public double[] GetCoefficients()
		{
            List<double> regressionCoefficients = new List<double>(_regressionCoefficients);
            regressionCoefficients.Add(_intercept);

            return regressionCoefficients.ToArray();
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