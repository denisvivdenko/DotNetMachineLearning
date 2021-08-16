using DotNetML.ModelSelection;
using DotNetML.LinearRegression;

namespace DotNetML.Statistics
{
    public class RegressionStandardErrors
    {
        private double[] _standardErrors;
        private double _learningRate = 0.0001;
        private double _sampleSizeCoefficient = 0.3;
        private double _samplesNumberCoefficient = 0.1;


        public RegressionStandardErrors(TrainingDataset data)
        {
            int sampleSize = (int) (data.Target.Length * _sampleSizeCoefficient);
            int samplesNumber = (int)(data.Target.Length * _samplesNumberCoefficient);
            double[][] regressionsCoefficients = ComputeRegressionsCoefficients(data, sampleSize, samplesNumber);
            _standardErrors = ComputeStandardErrors(regressionsCoefficients);
        }


        public double[] GetResult()
		{
            return _standardErrors;
		}


        private double[][] ComputeRegressionsCoefficients(TrainingDataset data, int sampleSize, int sampleNumber)
		{
            double[][] samplesCoefficients = new double[data.Data[0].Length + 1][];            

            for (int sampleCount = 0; sampleCount < sampleNumber; sampleCount++)
			{
                Bootstrap.Bootstrap bootstrap = new Bootstrap.Bootstrap(data, sampleSize);
                TrainingDataset sampleDataset = bootstrap.GetResult();

                var regressor = new GDMultipleLinearRegression(learningRate: _learningRate);
                regressor.TrainModel(sampleDataset);
                double[] coefficients = regressor.GetEquation().GetCoefficients();

                for (int coefficientIndex = 0; coefficientIndex < coefficients.Length; coefficientIndex++)
				{
                    if (samplesCoefficients[coefficientIndex] == null)
					{
                        samplesCoefficients[coefficientIndex] = new double[sampleNumber];
					}
                    samplesCoefficients[coefficientIndex][sampleCount] = coefficients[coefficientIndex];
				}
			}

            return samplesCoefficients;
		}


        private double[] ComputeStandardErrors(double[][] regressionsCoefficients)
		{
            int coefficientsNumber = regressionsCoefficients.Length;
            double[] standardErrors = new double[coefficientsNumber];

            for (int coefficientIndex = 0; coefficientIndex < coefficientsNumber; coefficientIndex++)
			{
                var coefficientStandardDeviation = new StandardDeviation(regressionsCoefficients[coefficientIndex]);
                standardErrors[coefficientIndex] = coefficientStandardDeviation.GetResult();
			}

            return standardErrors;
		}

    }
}