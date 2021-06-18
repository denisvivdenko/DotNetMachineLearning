using System;
using System.Collections.Generic;
using System.Linq;
using Accord.DataSets;
using DotNetML.KNearestNeighbors;
using DotNetML.Metrics;

namespace MLAlgorithms
{
	class KNearestNeighborsClassifierTest
	{
		static void Main(string[] args)
		{
			for (int i = 0; i < 20; i++)
			{
				var iris = new Iris();
				double[][] inputs = iris.Instances;
				int[] labels = iris.ClassLabels;

				int testSampleSize = 20;

				HashSet<int> testIndices = new HashSet<int>();
				while (testIndices.Count < testSampleSize)
				{
					Random random = new Random();
					int number = random.Next(0, labels.Length);
					testIndices.Add(number);
				}

				double[][] X_train = inputs;
				double[][] X_test = new double[testSampleSize][];
				int[] y_train = labels;
				int[] y_test = new int[testSampleSize];

				int lastIndex = 0;
				foreach (int index in testIndices)
				{
					X_test[lastIndex] = X_train[index];
					y_test[lastIndex] = y_train[index];

					lastIndex++;
				}


				foreach (int index in testIndices.OrderByDescending(v => v))
				{
					List<double[]> X_train_ = new List<double[]>(X_train);
					List<int> y_train_ = new List<int>(y_train);
					X_train_.RemoveAt(index);
					y_train_.RemoveAt(index);

					X_train = X_train_.ToArray();
					y_train = y_train_.ToArray();
				}


				var clf = new KNearestNeighborsClassifier(3);
				clf.TrainModel(X_train, y_train);
				int[] predictions = clf.PredictLabels(X_test);

				Precision precisionMetric = new Precision(y_test, predictions);
				Recall recallMetric = new Recall(y_test, predictions);

				Console.WriteLine($"PRECISION: {precisionMetric.GetResult()}\tRECALL: {recallMetric.GetResult()}");
			}
		}

	}
}

/**
OUTPUT:

0,95
0,95
1
0,95
0,95
1
1
0,9
0,85
0,95
0,95
0,9
1
1
1
0,95
0,9
0,85
1
1
0,95
0,95
0,95
1
0,85
0,95
0,95
0,95
1
1
0,95
0,95
1
0,95
0,95
1
0,95
0,95
0,85
0,95
**/
