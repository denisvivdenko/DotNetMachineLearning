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
		private Dictionary<int, double[]> _attributesProbabilities;
		private Dictionary<int, double> _labelsProbability;
		private double _alpha = 2;
		private double _kSmothingCoefficient = 0.5;
		private double _probabilityThreshold;


		public BernoulliNaiveBayesClassifier(double probabilityThreshold = 0.5)
		{
			_probabilityThreshold = probabilityThreshold;
		}
	

		public void TrainModel(int[][] data, int[] labels)
		{
			_attributesProbabilities = CountAttributesProbabilities(data, labels);
			_labelsProbability = CountLabelsProbability(labels);

			_isTrained = true;
		}


		public int[] PredictLabels(int[][] data)
		{
			int[] result = new int[data.GetLength(0)];

			for (int index = 0; index < data.GetLength(0); index++)
			{
				double prediction = PredictPostitiveOutcomeProbability(data[index]);

				if (prediction > _probabilityThreshold)
				{
					result[index] = 1;
				}
				else
				{
					result[index] = 0;
				}
			}

			return result;
		}


		public void PrintInfo()
		{
			if (_isTrained == false)
			{
				throw new Exception("PrintInfo(). Model is not trained yet");
			}

			foreach (double[] featureProbability in _attributesProbabilities.Values)
			{
				Console.WriteLine(featureProbability[0].ToString() + "\t" + featureProbability[1].ToString());
			}
			Console.WriteLine("\nLabels probability\n======");

			Console.WriteLine(_labelsProbability[0].ToString() + "\t" + _labelsProbability[1].ToString());
		}


		private int PredictPostitiveOutcomeProbability(int[] data)
		{
			double logProbabilityPositive = 0.0;
			double logProbabilityNegative = 0.0;

			foreach (KeyValuePair<int, double[]> attributeProbabilities in _attributesProbabilities)
			{
				int attributeIndex = attributeProbabilities.Key;
				double[] probabilities = attributeProbabilities.Value;

				if (data[attributeIndex] == 1)
				{
					logProbabilityNegative += Math.Log(probabilities[0]);
					logProbabilityPositive += Math.Log(probabilities[1]);
				}
				else
				{
					logProbabilityNegative += Math.Log(1 - probabilities[0]);
					logProbabilityPositive += Math.Log(1 - probabilities[1]);
				}
			}

			logProbabilityNegative = Math.Exp(logProbabilityNegative);
			logProbabilityPositive = Math.Exp(logProbabilityPositive);

			double probability = logProbabilityPositive / (logProbabilityPositive + logProbabilityNegative);
			if (probability > _probabilityThreshold)
			{
				return 1;
			}

			return 0;
		}


		private Dictionary<int, double[]> CountAttributesProbabilities(int[][] data, int[] labels)
		{
			Dictionary<int, int[]> counts = new Dictionary<int, int[]>();

			int firstClassCount = CountOccurances(labels, 0);
			int secondClassCount = labels.Length - firstClassCount;

			int recordsNumber = data.GetLength(0);
			int featuresNumber = data[0].Length;

			for (int featureIndex = 0; featureIndex < featuresNumber; featureIndex++)
			{
				if (!counts.ContainsKey(featureIndex))
				{
					counts.Add(featureIndex, new int[2]);
				}

				for (int recordIndex = 0; recordIndex < recordsNumber; recordIndex++)
				{
					if (IsPositiveRecord(data, recordIndex, featureIndex))
					{
						if (IsPositiveRecord(labels, recordIndex))
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


			Dictionary<int, double[]> probabilities = new Dictionary<int, double[]>();
			foreach (KeyValuePair<int, int[]> item in counts)
			{
				double[] featureProbabilities = new double[2];

				featureProbabilities[0] = ((double)item.Value[0] + _kSmothingCoefficient) / ((double)firstClassCount + _kSmothingCoefficient * _alpha);
				featureProbabilities[1] = ((double)item.Value[1] + _kSmothingCoefficient) / ((double)secondClassCount + _kSmothingCoefficient * _alpha);

				probabilities.Add(item.Key, featureProbabilities);
			}

			return probabilities;
		} 

		
		private Dictionary<int, double> CountLabelsProbability(int[] labels)
		{
			var count = new Dictionary<int, double>();
			int labelsNumber = labels.Length;

			count.Add(0, ((double)CountOccurances(labels, 0) + _kSmothingCoefficient) / ((double)labelsNumber + _kSmothingCoefficient * _alpha));
			count.Add(1, ((double)CountOccurances(labels, 1) + _kSmothingCoefficient) / ((double)labelsNumber + _kSmothingCoefficient * _alpha));

			return count;
		}


		private bool IsPositiveRecord(int[][] data, int recordIndex, int featureIndex)
		{
			if (data[recordIndex][featureIndex] == 1)
			{
				return true;
			}

			return false;
		}


		private bool IsPositiveRecord(int[] data, int recordIndex)
		{
			if (data[recordIndex] == 1)
			{
				return true;
			}

			return false;
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
