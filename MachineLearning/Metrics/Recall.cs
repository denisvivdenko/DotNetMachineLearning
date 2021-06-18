﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DotNetML.Metrics
{
	public class Recall : CategoricalMetric, IMetric
	{
		private double _recall;
		private int[] _classes;


		public Recall(int[] expected, int[] actual)
		{
			_classes = ExtractClasses(expected, actual);
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

			int[,] crosstab = FillCrosstab(actual, expected, _classes);


			int classesNumber = _classes.Length;
			double[] recalls = new double[classesNumber];

			int recordIndex = 0;
			for (int row = 0; row < classesNumber; row++)
			{
				double truePositiveAnswers = 0;
				double falseNegativeAnswers = 0;

				for (int column = 0; column < classesNumber; column++)
				{
					if (column == row)
					{
						truePositiveAnswers = crosstab[row, column];
					}
					else
					{
						falseNegativeAnswers += crosstab[row, column];
					}
				}

				double recall = truePositiveAnswers / (truePositiveAnswers + falseNegativeAnswers);
				recalls[recordIndex] = recall;
				recordIndex++;
			}

			double averageRecall = recalls.Average();
			return averageRecall;
		}
	}
}