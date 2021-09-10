using System.Collections.Generic;

namespace DotNetML.ModelSelection
{
    public class TrainingDataset
    {
        public double[][] Data { get; set; }
        public double[] Target { get; set; }
        public int RecordsCount { get; set; }
        public int FeaturesCount { get; set; }
        

        public TrainingDataset(double[][] data, double[] target)
        {
            Data = data;
            Target = target;
            RecordsCount = Data.Length;
            FeaturesCount = Data[0].Length;
        }


        public double[] ExtractFeature(int featureIndex)
        {
            List<double> featureColumn = new List<double>();
            foreach (double[] record in Data)
            {
                featureColumn.Add(record[featureIndex]);
            }

            return featureColumn.ToArray();
        }
    }
}