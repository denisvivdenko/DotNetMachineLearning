using System;
using System.Collections.Generic;
using System.Text;
/**
namespace DotNetML.Metrics
{
	public class Recall : IMetric
	{
		private double _precision;
		private int[] _classes;
		private Dictionary<int, int> _classesEnum;


		public Precision(int[] expected, int[] actual)
		{
			_classes = ExtractClasses(expected, actual);
			_classesEnum = CreateClassesEnum(_classes);
			_precision = CalculatePrecision(expected, actual);
		}


		public double GetResult()
		{
			return _precision;
		}


		private Dictionary<int, int> CreateClassesEnum(int[] classes)
		{
			Dictionary<int, int> classesEnum = new Dictionary<int, int>();

			for (int classIndex = 0; classIndex < classes.Length; classIndex++)
			{
				int encodedClass = classes[classIndex];
				classesEnum[encodedClass] = classIndex;
			}

			return classesEnum;
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


		private int[,] FillCrosstab(int[] actual, int[] expected, int[] classes)
		{
			int answersNumber = actual.Length;
			int classesNumber = classes.Length;
			int[,] crosstab = new int[classesNumber, classesNumber];

			for (int answerId = 0; answerId < answersNumber; answerId++)
			{
				if (actual[answerId] == expected[answerId])
				{
					int classIdentifier = _classesEnum[actual[answerId]];
					crosstab[classIdentifier, classIdentifier] += 1;
				}
				else
				{
					int expectedClassIdentifier = _classesEnum[expected[answerId]];
					int actualClassIdentifier = _classesEnum[actual[answerId]];

					crosstab[actualClassIdentifier, expectedClassIdentifier] += 1;
				}
			}

			return crosstab;
		}
	}
}
**/