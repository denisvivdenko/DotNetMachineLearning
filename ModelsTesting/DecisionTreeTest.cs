using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using DotNetML.DecisionTree;
using DotNetML.ModelSelection;

namespace ModelsTesting
{
    public class DecisionTreeTest
    {
        public DecisionTreeTest()
        {
            string dir = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location);
            string file = dir + "\\datasets\\decision_tree_X.csv";
            string file2 = dir + "\\datasets\\decision_tree_y.csv";

            double[][] X = ReadCSV(file);   
            double[][] y = ReadCSV(file2);

            var classifier = new DecisionTree();
            classifier.TrainModel(new TrainingDatasetCategoricalTarget(X, y));

            int trueClassified = 0;
            int falseClassified = 0;

            for (int recordIndex = 0; recordIndex < X.Length; recordIndex++)
			{
                double[] prediction = classifier.PredictTarget(X[recordIndex]);
                double[] actual = y[recordIndex];
                
                if (prediction.SequenceEqual(actual))
				{
                    trueClassified++;
				}
                falseClassified++;

                Console.Write("PREDICTION: ");
                PrintArray(prediction);
                Console.Write(" ACTUAL: ");
                PrintArray(actual);
                Console.WriteLine();
			}

            Console.WriteLine($"TRUE: {trueClassified} FALSE: {falseClassified}. {trueClassified / (double)(trueClassified + falseClassified)}");
        }


        private void PrintArray(double[] array)
		{
            Console.Write("[");
            foreach (double element in array)
			{
                Console.Write($" {element} ");
			}
            Console.Write("]");
		}

        private double[][] ReadCSV(string path) 
        {
            List<double[]> data = new List<double[]>();
            bool isHeader = true;
            using(var reader = new StreamReader(path))
            {
                
                while (!reader.EndOfStream)
                {
                    var line = reader.ReadLine();
                    var values = line.Split(',');
                    int valuesCount = values.Length;

                    if (isHeader)
                    {
                        isHeader = false;
                        continue;
                    }

                    double[] X = new double[valuesCount - 1];
                    for (int valueIndex = 1; valueIndex < valuesCount - 1; valueIndex++) 
                    {
                        X[valueIndex] = (double)double.Parse(values[valueIndex], CultureInfo.InvariantCulture);
                    }

                    data.Add(X);
                }
            }

            return data.ToArray();
        }
    }
}