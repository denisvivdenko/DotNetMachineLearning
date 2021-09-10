using System;
using System.Collections.Generic;
using System.Linq;

namespace DotNetML.ModelSelection
{
	public class TrainingDatasetCategoricalTarget : TrainingDataset
	{
		public new double[][] Target { get; set; }


		public TrainingDatasetCategoricalTarget(double[][] data, double[][] target) : base(data, null)
		{
			Target = target;
		}


		public TrainingDatasetCategoricalTarget DropFeature(int featureIndexToDrop)
		{
			double[][] newData = new double[RecordsCount][];

			for (int recordIndex = 0; recordIndex < RecordsCount; recordIndex++)
			{
				double[] dataVector = new double[FeaturesCount - 1];
				int elementIndex = 0;
				for (int featureIndex = 0; featureIndex < FeaturesCount; featureIndex++)
				{
					if (featureIndex == featureIndexToDrop)
					{
						continue;
					}

					dataVector[elementIndex] = Data[recordIndex][featureIndex];
					elementIndex++;
				}

				newData[recordIndex] = dataVector;
			}

			return new TrainingDatasetCategoricalTarget(newData, Target);
		}


		public TrainingDatasetCategoricalTarget SelectSubset(int featureIndex, double featureValue)
		{
			List<double[]> selectedTarget = new List<double[]>();
			List<double[]> selectedData = new List<double[]>();

			for (int recordIndex = 0; recordIndex < Data.Length; recordIndex++)
			{
				if (Data[recordIndex][featureIndex] == featureValue)
				{
					selectedData.Add(Data[recordIndex]);
					selectedTarget.Add(Target[recordIndex]);
				}

			}

			return new TrainingDatasetCategoricalTarget(selectedData.ToArray(), selectedTarget.ToArray());
		}

		
		public double[] GetMostFrequentTarget()
		{
			int classesNum = Target[0].Length;
			int[] classesCount = new int[classesNum];

			foreach (double[] record in Target)
			{
				for (int classIndex = 0; classIndex < classesNum; classIndex++)
				{
					if (record[classIndex] == 1)
					{
						classesCount[classIndex]++;
					}
				}
			}

			int maxValue = classesCount.Max();
			int maxIndex = classesCount.ToList().IndexOf(maxValue);
			double[] mostFrequentClass = new double[classesNum];
			mostFrequentClass[maxIndex] = 1;

			return mostFrequentClass;
		}
	}
}