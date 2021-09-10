using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
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
        }


        public double[][] ReadCSV(string path) 
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