using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DotNetML.Metrics
{
	public class Precision : IMetric
	{
		private double _precision;
		private int[] _allClasses;


		public Precision(int[] expected, int[] actual)
		{
			_allClasses = ExtractClasses(expected, actual);
			_precision = CalculatePrecision(expected, actual);
		}


		public double GetResult()
		{
			return _precision;
		}

		
		private int[] ExtractClasses(int[] expected, int[] actual)
		{
			HashSet<int> expectedClasses = new HashSet<int>(expected);
			HashSet<int> actualClasses = new HashSet<int>(actual);

			HashSet<int> allClasses = new HashSet<int>(expectedClasses.Union(actualClasses));

			return allClasses.ToArray<int>();
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
