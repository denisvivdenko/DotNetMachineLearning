using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DotNetML.Metrics
{
	public class Precision : CategoricalMetric, IMetric
	{
		private double _precision;
		private int[] _classes;


		public Precision(int[] expected, int[] actual)
		{
			_classes = ExtractClasses(expected, actual);
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

			int[,] crosstab = FillCrosstab(actual, expected, _classes);


			int classesNumber = _classes.Length;
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
	}
}
