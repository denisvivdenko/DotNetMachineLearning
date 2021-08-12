namespace DotNetML.Statistics
{
    public class Mean : Statistics
    {
        private double _mean;

        
        public Mean() { }


        public Mean(double[] dataset)
        {
            _mean = CalculateMean(dataset);
        }


        public override double GetResult()
        {
            return _mean;
        }


        public override Statistics SetData(double[] newData)
        {
            return new Mean(newData);
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