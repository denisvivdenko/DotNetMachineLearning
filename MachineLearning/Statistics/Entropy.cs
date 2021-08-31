using System;

namespace DotNetML.Statistics
{
    public class Entropy
    {
        private double _entropy;


        public Entropy(double[][] sample)
        {
            _entropy = ComputeEntropy(sample);
        }


        public double GetResult()
        {
            return _entropy;
        }


        private double ComputeEntropy(double[][] sample)
        {
            int classesCount = sample[0].Length;
            double[] classesProbabilities = ComputeClassesFrequencyProbabilities(sample);

            double entropy = 0;
            for (int classIndex = 0; classIndex < classesCount; classIndex++)
            {
                double classProbability = classesProbabilities[classIndex];
                if (classProbability == 0) 
                {
                    continue;
                }

                entropy += Math.Log2(classProbability) * classProbability * (-1);
            }

            return entropy;
        }


        private double[] ComputeClassesFrequencyProbabilities(double[][] sample)
        {
            int sampleLength = sample.Length;
            int classesCount = sample[0].Length;
            double[] classesProbabilities = new double[classesCount];

            for (int classIndex = 0; classIndex < classesCount; classIndex++)
            {
                int encodedValuesSum = 0;
                for (int sampleValueIndex = 0; sampleValueIndex < sampleLength; sampleValueIndex++)
                {
                    int encodedValue = (int) sample[sampleValueIndex][classIndex];
                    encodedValuesSum += encodedValue;
                }
                classesProbabilities[classIndex] = ((double) encodedValuesSum / (double) sampleLength);
            }

            return classesProbabilities;
        }
    }
}