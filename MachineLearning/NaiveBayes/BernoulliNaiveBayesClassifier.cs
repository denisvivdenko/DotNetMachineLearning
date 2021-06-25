using System;
using System.Collections.Generic;
using System.Text;

namespace DotNetML.NaiveBayes
{
	/** 
			Bernoulli Naive Bayes Binary Classifier for categorical data
	**/

	public class BernoulliNaiveBayesClassifier 
	{
		private bool _isTrained = false;


		public BernoulliNaiveBayesClassifier()
		{

		}
	

		public void TrainModel(int[][] data, int[] labels)
		{
			var r = CountFeatureOccuranceFrequency(data, labels);
			_isTrained = true;
		}

		public int PredictLabel(int[] data)
		{
			return 0;
		}

		public int[] PredictLabels(int[][] data)
		{
			return null;
		}

		private Dictionary<int, double[]> CountFeatureOccuranceFrequency(int[][] data, int[] labels)
		{
			Dictionary<int, int[]> counts = new Dictionary<int, int[]>();

			int firstClassCount = CountOccurances(labels, 0);
			int secondClassCount = labels.Length - firstClassCount;

			for (int featureIndex = 0; featureIndex < data[0].Length; featureIndex++)
			{
				if (!counts.ContainsKey(featureIndex))
				{
					counts.Add(featureIndex, new int[2]);
				}

				for (int recordIndex = 0; recordIndex < data.GetLength(0); recordIndex++)
				{
					if (data[recordIndex][featureIndex] == 1)
					{
						if (labels[recordIndex] == 1)
						{
							counts[featureIndex][1] += 1;
						}
						else
						{
							counts[featureIndex][0] += 1;
 						}
					}
				}
			}


			Dictionary<int, double[]> frequencies = new Dictionary<int, double[]>();
			foreach (KeyValuePair<int, int[]> item in counts)
			{
				double[] featureFrequencies = new double[2];

				featureFrequencies[0] = (double)item.Value[0] / (double)firstClassCount;
				featureFrequencies[1] = (double)item.Value[1] / (double)secondClassCount;
				Console.WriteLine(featureFrequencies[0].ToString() + "\t" +featureFrequencies[1].ToString());

				frequencies.Add(item.Key, featureFrequencies);
			}

			return frequencies;
		} 

		private int CountOccurances(int[] array, int value)
		{
			int count = 0;

			foreach (int arrayValue in array)
			{
				if (arrayValue == value)
				{
					count++;
				}
			}

			return count;
		}
	}
}
