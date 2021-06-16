using System;
using System.Collections.Generic;
using System.Text;

namespace DotNetML.Metrics
{
	public class Precision : IMetric
	{
		private double _precision;

		public Precision(int[] expected, int[] actual)
		{
			_precision = CalculatePrecision(expected, actual);
		}

		public double GetResult()
		{
			return _precision;
		}

		private double CalculatePrecision(int[] expected, int[] actual)
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
