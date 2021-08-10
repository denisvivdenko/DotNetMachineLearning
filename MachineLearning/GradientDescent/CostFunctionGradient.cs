namespace DotNetML.GradientDescent
{
    public abstract class CostFunctionGradient
    {
        protected double[] _gradient;
        protected (double[][] data, double[] target) _trainingData;


        public CostFunctionGradient((double[][] data, double[] target) trainingData, double[] coefficients)
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