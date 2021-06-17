using System;
using System.Collections.Generic;
using System.Text;

namespace DotNetML.Metrics
{
	public class Recall : IMetric
	{
		private double _recall;


		public Recall(int[] expected, int[] actual)
		{
			_recall = CalculateRecall(expected, actual);
		}

		public double GetResult()
		{
			return _recall;
		}

		private double CalculateRecall(int[] expected, int[] actual)
		{

			if (expected.Length != actual.Length)
			{
				throw new Exception("different dimentions");
			}

			double truePositive = 0;
			double falsePositive = 0;

			for (int index = 0; index < expected.Length; index++)
			{
				if (expected[index] == actual[index])
				{
					truePositive += 1;
				}
				else
				{
					falsePositive += 1;
				}
			}

			return (truePositive) / (falsePositive + truePositive);
		}
	}
}
