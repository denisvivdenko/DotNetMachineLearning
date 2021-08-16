using System;
using System.Collections.Generic;

namespace DotNetML.PDFs
{
    public class NormalDistribution : IDistribution
    {
        private double _mean;
        private double _sigma;
        private double[] _probabilities;


        public NormalDistribution(double[] dataset, double mean, double sigma)
        {
            _mean = mean;
            _sigma = sigma;
            _probabilities = ComputeDensityFunction(dataset);
        }


        public double[] GetResult()
        {
            return _probabilities;
        }


        private double[] ComputeDensityFunction(double[] dataset)
        {
            List<double> probabilities = new List<double>();
            foreach (double value in dataset)
            {
                double f1 = Math.Pow((value - _mean), 2);
                double f2 = 2 * Math.Pow(_sigma, 2);
                double f3 = _sigma * Math.Sqrt(2 * Math.PI);
                double probability = Math.Exp(-(f1/f2)) / f3;
                probabilities.Add(probability);
            }

            return probabilities.ToArray();
        }
    }
}