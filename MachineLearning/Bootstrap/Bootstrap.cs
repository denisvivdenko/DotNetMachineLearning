using System;
using DotNetML.ModelSelection;

namespace DotNetML.Bootstrap
{
    public class Bootstrap
    {
        private double[][] _dataSample;
        private double[] _targetSample;


        public Bootstrap(TrainingDataset population, int sampleSize)
        {
            (_dataSample, _targetSample) = SelectRandomSample(population, sampleSize);
        }


        public TrainingDataset GetResult()
        {
            return new TrainingDataset(_dataSample, _targetSample);
        }


        private (double[][] data, double[] target) SelectRandomSample(TrainingDataset population, int sampleSize)
        {
            double[][] dataSample = new double[sampleSize][];
            double[] targetSample = new double[sampleSize];

            for (int count = 0; count < sampleSize; count++)
            {
                Random numberGenerator = new Random();
                int randomValueIndex = numberGenerator.Next(0, population.Target.Length);

                dataSample[count] = population.Data[randomValueIndex];
                targetSample[count] = population.Target[randomValueIndex];
            }

            return (dataSample, targetSample);
        }
    }
}