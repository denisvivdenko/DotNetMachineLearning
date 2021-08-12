using DotNetML.GradientDescent;
using DotNetML.ModelSelection;
using System.Collections.Generic;
using System.Linq;


namespace DotNetML.LinearRegression
{
    public class GDMultipleLinearRegression : MultipleRegressionEquation
    {         
        private MultipleRegressionEquation _equation;
        private double _learningRate;
        private double _stepSizeThreshold;
        private int _maxEpohs;
        private bool _isTrained = false; 


        public GDMultipleLinearRegression(double learningRate=0.01, 
                double stepSizeThreshold=0.01, int maxEpohs=1000)
        {
            _learningRate = learningRate;
            _stepSizeThreshold = stepSizeThreshold;
            _maxEpohs = maxEpohs;
        }

        
        public void TrainModel(TrainingDataset trainingDataset)
        {
            var parameters = SearchBestParameters(trainingDataset);
            _equation = new MultipleRegressionEquation(parameters.regressionCoefficients, parameters.intercept);
            _isTrained = true;
        }


        public new double PredictTarget(double[] inputVector)
        {
            if (!_isTrained)
            {
                throw new System.Exception("model isn't trained");
            }

            return _equation.PredictTarget(inputVector);
        }


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


        public MultipleRegressionEquation GetEquation()
		{
            return _equation;
		}


        public new double[] GetCoefficients()
		{
            return _equation.GetCoefficients();
		}


        private (double[] regressionCoefficients, double intercept) SearchBestParameters(TrainingDataset trainingDataset)
        {

            double[] startParameters = new double[trainingDataset.Data[0].Length + 1];
            var costFunctionGradient = new MultipleRegressionCostFunctionGradient(trainingDataset, startParameters);

            var gradientDescent = new BatchGradientDescent(costFunctionGradient, startParameters, learningRate:_learningRate, 
                                            stepSizeThreshold:_stepSizeThreshold, maxEpohs:_maxEpohs);
            double[] foundBestParameters = gradientDescent.GetResult();
            (double[] regressionCoefficients, double intercept) regressionParameters = (foundBestParameters.SkipLast(1).ToArray(), foundBestParameters[foundBestParameters.Length-1]);

            return regressionParameters;
        }
    }
}