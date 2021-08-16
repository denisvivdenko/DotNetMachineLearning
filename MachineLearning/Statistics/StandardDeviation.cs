using System;

namespace DotNetML.Statistics
{
    public class StandardDeviation : Statistics
    {
        private double _standardDeviation;
        private double _freedomDegree;


        public StandardDeviation() { }


        public StandardDeviation(double[] dataset, int freedomDegree=1)
        {
            _freedomDegree = freedomDegree;
            _standardDeviation = CalculateStandardDeviation(dataset);
        }


        public override double GetResult()
        {
            return _standardDeviation;
        }


		public override Statistics SetData(double[] newData)
		{
            return new StandardDeviation(newData);
		}


		private double CalculateStandardDeviation(double[] dataset)
        {
            double datasetMean = new Mean(dataset).GetResult();
            int datasetLength = dataset.Length;
            double squaredDeviationsSum = 0;

            foreach(double value in dataset) 
            {
                squaredDeviationsSum += Math.Pow((value - datasetMean), 2); 
            }

            return Math.Sqrt(squaredDeviationsSum / (datasetLength - _freedomDegree));
        }
    }
}