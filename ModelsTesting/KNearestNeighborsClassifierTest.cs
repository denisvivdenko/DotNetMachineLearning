using System;
using System.Collections.Generic;
using System.Linq;
using Accord.DataSets;
using DotNetML.KNearestNeighbors;
using DotNetML.Metrics;
using DotNetML.ModelSelection;

namespace ModelsTesting
{
	public class KNearestNeighborsClassifierTest
	{
		public KNearestNeighborsClassifierTest()
		{
			var iris = new Iris();
			double[][] data = iris.Instances;
			int[] labels = iris.ClassLabels;


			TrainTestSelector trainTestSelector = new TrainTestSelector(0.3);
			var ((XTrain, XTest), (yTrainDouble, yTestDouble)) = trainTestSelector.SplitData(data, ConvertIntArrayToDoubleArray(labels));

			int[] yTrain = ConvertDoubleArrayToIntArray(yTrainDouble);
			int[] yTest = ConvertDoubleArrayToIntArray(yTestDouble);

			var clf = new KNearestNeighborsClassifier(3);
			clf.TrainModel(XTrain, yTrain);	
			int[] predictions = clf.PredictLabels(XTest);

			Precision precisionMetric = new Precision(yTest, predictions);
			Recall recallMetric = new Recall(yTest, predictions);
			F1Score f1Score = new F1Score(yTest, predictions);

			Console.WriteLine(String.Format("PRECISION: {0:F3}  RECALL: {1:F3}  F1: {2:F3}", 
				precisionMetric.GetResult(), recallMetric.GetResult(), f1Score.GetResult()));
			
		}


		private double[] ConvertIntArrayToDoubleArray(int[] array)
		{
			return array.Select(value => (double)value).ToArray();
		}


		private int[] ConvertDoubleArrayToIntArray(double[] array)
		{
			return array.Select(value => (int)value).ToArray();
		}
	}
}

/**
OUTPUT:
PRECISION: 1,000  RECALL: 1,000  F1: 1,000
PRECISION: 0,980  RECALL: 0,978  F1: 0,979
PRECISION: 0,954  RECALL: 0,954  F1: 0,954
PRECISION: 0,960  RECALL: 0,960  F1: 0,960
...
**/
