using System;
using System.Linq;

namespace DotNetML.Metrics
{
	public class Recall : CategoricalMetric
	{
		private double _recall;
		private int[] _classes;


		public Recall(int[] expected, int[] actual) //	multiclass score
		{
			_classes = ExtractClasses(expected, actual);
			_recall = CalculateRecall(expected, actual);
		}


		public override double GetResult()
		{
			return _recall;
		}

		private double CalculateRecall(int[] expected, int[] actual)
		{

			if (expected.Length != actual.Length)
			{
				throw new Exception("different dimentions");
			}

			int[,] crosstab = FillConfusionMatrix(actual, expected, _classes);


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
