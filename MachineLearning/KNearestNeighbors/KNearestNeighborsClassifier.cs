using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DotNetML.LinearAlgebra;

namespace DotNetML.KNearestNeighbors
{
	public class KNearestNeighborsClassifier
	{
		private int _kcoefficient;
		private double[][] _data;
		private int[] _labels;
		private bool _isTrained = false;


		public KNearestNeighborsClassifier(int kcoefficient)
		{
			_kcoefficient = kcoefficient;
		}

		public void TrainModel(double[][] data, int[] labels)
		{
			_data = data;
			_labels = labels;
			_isTrained = true;
		}

		public int PredictLabel(double[] input)
		{
			CheckIsTrained();

			var distances = ComputeDistances(_data, input);
			var nearestLabels = GetKNearestLabels(distances);
			var mostFrequentLabels = GetMostFrequentLabels(nearestLabels);

			int k = _kcoefficient;
			if (mostFrequentLabels.Length > 1)
			{
				_kcoefficient -= 1;
				return PredictLabel(input);
			}
			_kcoefficient = k;

			return mostFrequentLabels[0];
		}

		public int[] PredictLabels(double[][] inputs)
		{
			List<int> results = new List<int>();

			foreach (double[] input in inputs)
			{
				results.Add(PredictLabel(input));
			}

			return results.ToArray();
		}

		private int[] GetMostFrequentLabels(int[] labels)
		{
			List<int> result = new List<int>();

			Dictionary<int, int> counts = new Dictionary<int, int>();
			foreach (int label in labels)
			{
				if (!counts.ContainsKey(label))
				{
					counts.Add(label, 0);
				}

				counts[label] += 1;
			}

			int maxCount = counts.Values.Max();

			foreach (KeyValuePair<int, int> labelAndCount in counts)
			{
				if (labelAndCount.Value == maxCount)
				{
					result.Add(labelAndCount.Key);
				} 
			}

			return result.ToArray();
		}

		private int[] GetKNearestLabels(SortedDictionary<double, int[]> distances)
		{
			List<int> resultLabels = new List<int>();

			int k = _kcoefficient;
			foreach (int[] labels in distances.Values)
			{
				if (k < 1)
				{
					break;
				}

				foreach (int label in labels)
				{
					resultLabels.Add(label);
				}

				k--;
			}

			return resultLabels.ToArray();
		}

		private SortedDictionary<double, int[]> ComputeDistances(double[][] fittedData, double[] input)
		{
			Vector inputVector = new Vector(input);
			SortedDictionary<double, int[]> distances = new SortedDictionary<double, int[]>();

			for (int index = 0; index < fittedData.Length; index++)
			{
				Vector fittedVector = new Vector(fittedData[index]);
				double distance = inputVector.ComputeDistance(fittedVector);

				if (distances.ContainsKey(distance))
				{
					int lastIndex = distances[distance].Length - 1;
					distances[distance][lastIndex] = _labels[index];
				}
				else
				{
					distances.Add(distance, new int[] { _labels[index] });
				}
			}

			return distances;
		}

		private void CheckIsTrained()
		{
			if (!_isTrained)
			{
				throw new Exception("the model wasn't trained");
			}
		}
	}
}
