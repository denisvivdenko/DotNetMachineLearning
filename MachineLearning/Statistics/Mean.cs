namespace DotNetML.Statistics
{
    public class Mean : Statistics
    {
        private double _mean;

        
        public Mean(double[] dataset)
        {
            _mean = CalculateMean(dataset);
        }

        public override double GetResult()
        {
            return _mean;
        }

        private double CalculateMean(double[] dataset)
        {
            int datasetLength = dataset.Length;
            double sum = 0;

            foreach(double value in dataset)
            {
                sum += value;
            }

            return sum / (double)datasetLength;
        }
    }
}