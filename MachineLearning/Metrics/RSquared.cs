using DotNetML.Statistics;
using System;

namespace DotNetML.Metrics
{
    public class RSquared
    {
        private double _rSquared;


        public RSquared(double[] predictions, double[] actual)
        {
            _rSquared = CalculateRSquared(predictions, actual);
        }


        public double GetResult()
        {
            return _rSquared;
        }


        private double CalculateRSquared(double[] predictions, double[] actual)
        {
            double sumSquaredErrors = new SumSquaredErrors(predictions, actual).GetResult();
            double sumDeviationsFromMean = CalculateSumDeviationsFromMean(actual);

            return 1 - (sumSquaredErrors / sumDeviationsFromMean);
        }


        private double CalculateSumDeviationsFromMean(double[] data)
        {
            double mean = new Mean(data).GetResult();
            double sumSquaredDeviationsFromMean = 0;

            for (int recordIndex = 0; recordIndex < data.Length; recordIndex++)
            {
                sumSquaredDeviationsFromMean += Math.Pow(mean - data[recordIndex], 2);
            }

            return sumSquaredDeviationsFromMean;
        }
    }
}