using System;
using DotNetML.Statistics;

namespace DotNetML.Metrics
{
    public class Correlation : Metric
    {
        private double _correlation;


        public Correlation(double[] firstSet, double[] secondSet)
        {
            CheckSetsCompatibility(firstSet, secondSet);
            _correlation = CalculateCorrelation(firstSet, secondSet);
        }

        public override double GetResult() 
        {
            return Math.Round(_correlation, 3, MidpointRounding.ToEven);
        }

        private double CalculateCorrelation(double[] firstSet, double[] secondSet)
        {
            double covariance = new Covariance(firstSet, secondSet).GetResult();
            double firstSetSd = new StandardDeviation(firstSet).GetResult();
            double secondSetSd = new StandardDeviation(secondSet).GetResult();

            return covariance / (firstSetSd * secondSetSd);
        }
    }
}