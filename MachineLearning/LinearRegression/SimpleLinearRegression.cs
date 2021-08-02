using DotNetML.Statistics;
using DotNetML.Metrics;
using System.Collections.Generic;

namespace DotNetML.LinearRegression
{
    public class SimpleLinearRegression
    {
        /**
            Simple linear regression algorigthm using
            least squares method
        **/

        private double _beta;
        private double _alpha;
        private bool _isTrained = false;   

        
        public void TrainModel(double[] data, double[] target)
        {
            _beta = CalculateBeta(data, target);
            _alpha = CalculateAlpha(data, target, _beta);
            _isTrained = true;
        }


        public double PredictTarget(double input)
        {
            if (!_isTrained)
            {
                throw new System.Exception("model isn't trained");
            }

            double prediction = _beta * input + _alpha;
            return prediction;
        }


        public double[] PredictTargets(double[] inputs)
        {
            List<double> predictions = new List<double>();

            foreach (double input in inputs)
            {
                var prediction = PredictTarget(input);
                predictions.Add(prediction);
            }

            return predictions.ToArray();
        }


        private double CalculateBeta(double[] data, double[] target)
        {
            double correlation = new Correlation(data, target).GetResult();
            double data_sd = new StandardDeviation(data).GetResult();
            double target_sd = new StandardDeviation(target).GetResult();

            double beta = correlation * target_sd / data_sd;

            return beta;
        }
        

        private double CalculateAlpha(double[] data, double[] target, double beta)
        {
            double target_mean = new Mean(target).GetResult();
            double data_mean = new Mean(data).GetResult();

            double alpha = target_mean - beta * data_mean;

            return alpha;
        }
    }
}