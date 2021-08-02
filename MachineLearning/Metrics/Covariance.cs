using System;
using DotNetML.Statistics;


namespace DotNetML.Metrics
{
    public class Covariance : Metric
    {
        private double _covariance;

        
        public Covariance(double[] firstSet, double[] secondSet)
        {
            CheckSetsCompatibility(firstSet, secondSet);
            _covariance = CalculateCovariance(firstSet, secondSet);
        }


        public override double GetResult() 
        {
            return _covariance;
        }


        private double CalculateCovariance(double[] firstSet, double[] secondSet)
        {
            double covariance = 0;
            int setsLenght = firstSet.Length;

            double firstSetMean = new Mean(firstSet).GetResult();
            double secondSetMean = new Mean(secondSet).GetResult();
            
            double sumSetsDeviationFromMean = 0;

            for (int valueIndex = 0; valueIndex < setsLenght; valueIndex++)
            {
                double firstDeviationFromMean = firstSet[valueIndex] - firstSetMean;
                double secondDeviationFromMean = secondSet[valueIndex] - secondSetMean;

                sumSetsDeviationFromMean += firstDeviationFromMean * secondDeviationFromMean;
            }

            covariance = sumSetsDeviationFromMean / (setsLenght-1);

            return covariance;
        }
    }
}