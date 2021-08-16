using System;
using System.Collections.Generic;
using DotNetML.MathFunctions;

namespace DotNetML.PDFs
{
    public class NormalDistributionCDF : Distribution
    {
        /**
            Cumulative distribution function for normal distribution
        **/
        private double _mean;
        private double _sigma;
        private double[] _probabilities;


        public NormalDistributionCDF(double[] dataset, double mean, double sigma)
        {
            _mean = mean;
            _sigma = sigma;
            _probabilities = ComputeCDF(dataset);
        }


        public NormalDistributionCDF(double value, double mean, double sigma)
        {
            _mean = mean;
            _sigma = sigma;
            double[] dataset = { value };
            _probabilities = ComputeCDF(dataset);
        }


        public override double[] GetResult()
        {
            return _probabilities;
        }


        private double[] ComputeCDF(double[] dataset)
        {
            List<double> probabilities = new List<double>();
            foreach (double value in dataset)
            {
                double f1 = (value - _mean) / (Math.Sqrt(2) * _sigma);
                double f2 = new ErrorFunction(f1).GetResult();
                double probability = (1 + f2) / 2;
                probabilities.Add(probability);
            }

            return probabilities.ToArray();
        }
    }
}