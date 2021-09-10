using System.Collections.Generic;
using DotNetML.ModelSelection;

namespace DotNetML
{
    public abstract class Model
    {
        public abstract void TrainModel(TrainingDataset trainingDataset);


        public abstract double PredictTarget(double[] inputVector);


        public double[] PredictTargets(double[][] inputs)
        {
            List<double> predictions = new List<double>();
            foreach (double[] input in inputs)
            {
                var prediction = PredictTarget(input);
                predictions.Add(prediction);
            }

            return predictions.ToArray();
        }
    }
}