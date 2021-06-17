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

			int[,] crosstab = FillCrosstab(actual, expected, _allClasses);


			int classesNumber = _allClasses.Length;
			double[] precisions = new double[classesNumber];

			for (int column = 0; column < classesNumber; column++)
			{
				double truePositiveAnswers = 0;
				double falsePositiveAnswers = 0;

				for (int row = 0; row < classesNumber; row++)
				{
					if (column == row)
					{
						truePositiveAnswers = crosstab[row, column];
					}
					else
					{
						falsePositiveAnswers += crosstab[row, column];
					}
				}

				double precision = truePositiveAnswers / (truePositiveAnswers + falsePositiveAnswers);
				precisions[column] = precision;
			}

			double averagePrecision = precisions.Average();
			return averagePrecision;
		}

		
		private int[,] FillCrosstab(int[] actual, int[] expected, int[] classes)
		{
			int answersNumber = actual.Length;
			int classesNumber = classes.Length;
			int[,] crosstab = new int[classesNumber, classesNumber];

			for (int answerId = 0; answerId < answersNumber; answerId++)
			{
				if (actual[answerId] == expected[answerId])
				{
					int classIdentifier = actual[answerId];
					crosstab[classIdentifier, classIdentifier] += 1;
				}
				else
				{
					int expectedClassIdentifier = expected[answerId];
					int actualClassIdentifier = actual[answerId];

					crosstab[actualClassIdentifier, expectedClassIdentifier] += 1;
				}
			}

			return crosstab;
		}
	}
}
