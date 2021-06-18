using System;
using System.Collections.Generic;
using System.Linq;
using Accord.DataSets;
using DotNetML.KNearestNeighbors;
using DotNetML.Metrics;
using DotNetML.ModelSelection;

namespace MLAlgorithmsTests
{
	class KNearestNeighborsClassifierTest
	{
		static void Main(string[] args)
		{
			for (int i = 0; i < 20; i++)
			{
				var iris = new Iris();
				double[][] data = iris.Instances;
				int[] labels = iris.ClassLabels;

				TrainTestSelector trainTestSelector = new TrainTestSelector(0.3);
				var ((XTrain, XTest), (yTrain, yTest)) = trainTestSelector.SplitData(data, labels);

				var clf = new KNearestNeighborsClassifier(3);
				clf.TrainModel(XTrain, yTrain);
				int[] predictions = clf.PredictLabels(XTest);

				Precision precisionMetric = new Precision(yTest, predictions);
				Recall recallMetric = new Recall(yTest, predictions);
				F1Score f1Score = new F1Score(yTest, predictions);

				Console.WriteLine(String.Format("PRECISION: {0:F3}  RECALL: {0:F3}  F1: {0:F3}", 
					precisionMetric.GetResult(), recallMetric.GetResult(), f1Score.GetResult()));
			}
		}

	}
}

/**
OUTPUT:
PRECISION: 0,982  RECALL: 0,982  F1: 0,982
PRECISION: 1,000  RECALL: 1,000  F1: 1,000
PRECISION: 0,978  RECALL: 0,978  F1: 0,978
PRECISION: 0,952  RECALL: 0,952  F1: 0,952
PRECISION: 0,929  RECALL: 0,929  F1: 0,929
PRECISION: 0,906  RECALL: 0,906  F1: 0,906
PRECISION: 0,952  RECALL: 0,952  F1: 0,952
PRECISION: 0,980  RECALL: 0,980  F1: 0,980
PRECISION: 0,937  RECALL: 0,937  F1: 0,937
PRECISION: 1,000  RECALL: 1,000  F1: 1,000
PRECISION: 1,000  RECALL: 1,000  F1: 1,000
PRECISION: 0,924  RECALL: 0,924  F1: 0,924
PRECISION: 0,978  RECALL: 0,978  F1: 0,978
PRECISION: 0,941  RECALL: 0,941  F1: 0,941
PRECISION: 1,000  RECALL: 1,000  F1: 1,000
PRECISION: 0,983  RECALL: 0,983  F1: 0,983
PRECISION: 0,944  RECALL: 0,944  F1: 0,944
PRECISION: 0,955  RECALL: 0,955  F1: 0,955
PRECISION: 0,939  RECALL: 0,939  F1: 0,939
PRECISION: 0,979  RECALL: 0,979  F1: 0,979
**/
