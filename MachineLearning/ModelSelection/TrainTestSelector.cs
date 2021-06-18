using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DotNetML.ModelSelection
{
	public class TrainTestSelector
	{
		private double _testSize;


		public TrainTestSelector(double testSize=0.2)
		{
			_testSize = testSize;
		}


		public ((double[][], double[][]), (int[], int[])) SplitData(double[][] data, int[] target)
		{
			CheckDatasetsCompatibility(data, target);

			int testSampleSize = GetTestSampleSize(_testSize, data);
			HashSet<int> testIndices = GenerateTestSampleIndices(testSampleSize, target.Length);


			double[][] XTrain = data;
			double[][] XTest = new double[testSampleSize][];
			int[] yTrain = target;
			int[] yTest = new int[testSampleSize];

			int lastIndex = 0;
			foreach (int index in testIndices.OrderByDescending(v => v))
			{
				XTest[lastIndex] = XTrain[index];
				yTest[lastIndex] = yTrain[index];
				lastIndex++;

				List<double[]> XTrainUpdated = new List<double[]>(XTrain);
				List<int> yTrainUpdated = new List<int>(yTrain);
				XTrainUpdated.RemoveAt(index);
				yTrainUpdated.RemoveAt(index);

				XTrain = XTrainUpdated.ToArray();
				yTrain = yTrainUpdated.ToArray();
			}
			

			return ((XTrain, XTest), (yTrain, yTest));
		}


		private HashSet<int> GenerateTestSampleIndices(int testSampleSize, int maxIndex)
		{
			HashSet<int> testIndices = new HashSet<int>();
			while (testIndices.Count < testSampleSize)
			{
				Random random = new Random();
				int number = random.Next(0, maxIndex);
				testIndices.Add(number);
			}

			return testIndices;
		}
		

		private bool CheckDatasetsCompatibility(double[][] data, int[] target)
		{
			if (data.Length != target.Length)
			{
				throw new Exception("train test split error. Datasets are not compatible");
			}

			return true;
		}


		private int GetTestSampleSize(double percents, double[][] data)
		{
			int dataSize = data.Length;
			int testSampleSize = (int) (dataSize * percents);

			return testSampleSize;
		}
	}
}
