using DotNetML.ModelSelection;

namespace DotNetML.GradientDescent
{
    public abstract class CostFunctionGradient
    {
        protected double[] _gradient;
        protected TrainingDataset _trainingData;


        public CostFunctionGradient(TrainingDataset trainingData, double[] coefficients)
		{
            _trainingData = trainingData;
		}


        public double[] GetResult()
        {
            return _gradient;
        }


        public abstract CostFunctionGradient SetNewParameters(double[] paramaters);
    }
}