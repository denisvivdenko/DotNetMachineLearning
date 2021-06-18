﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DotNetML.Metrics
{
	public abstract class CategoricalMetric
	{

		protected Dictionary<int, int> CreateClassesEnum(int[] classes)
		{
			Dictionary<int, int> classesEnum = new Dictionary<int, int>();

			for (int classIndex = 0; classIndex < classes.Length; classIndex++)
			{
				int encodedClass = classes[classIndex];
				classesEnum[encodedClass] = classIndex;
			}

			return classesEnum;
		}

		protected int[] ExtractClasses(int[] expected, int[] actual)
		{
			HashSet<int> expectedClasses = new HashSet<int>(expected);
			HashSet<int> actualClasses = new HashSet<int>(actual);

			HashSet<int> allClasses = new HashSet<int>(expectedClasses.Union(actualClasses));

			return allClasses.ToArray<int>();
		}

		protected int[,] FillCrosstab(int[] actual, int[] expected, int[] classes)
		{
			Dictionary<int, int> classesEnum = CreateClassesEnum(classes);
			int answersNumber = actual.Length;
			int classesNumber = classes.Length;
			int[,] crosstab = new int[classesNumber, classesNumber];

			for (int answerId = 0; answerId < answersNumber; answerId++)
			{
				if (actual[answerId] == expected[answerId])
				{
					int classIdentifier = classesEnum[actual[answerId]];
					crosstab[classIdentifier, classIdentifier] += 1;
				}
				else
				{
					int expectedClassIdentifier = classesEnum[expected[answerId]];
					int actualClassIdentifier = classesEnum[actual[answerId]];

					crosstab[actualClassIdentifier, expectedClassIdentifier] += 1;
				}
			}

			return crosstab;
		}
	}
}