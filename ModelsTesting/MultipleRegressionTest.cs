using System;
using System.Collections.Generic;
using System.IO;
using DotNetML.LinearRegression;
using DotNetML.ModelSelection;
using DotNetML.Metrics;
using System.Globalization;


namespace ModelsTesting
{
    public class MultipleRegressionTest
    {
        public MultipleRegressionTest()
        {
            string dir = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location);
            string file = dir + "\\datasets\\multiple_regression_dataset.csv";

            var (X, y) = ReadCSV(file);

            var regressor = new GDMultipleLinearRegression(learningRate:0.0001);
            regressor.TrainModel(X, y);
            var predictions = regressor.PredictTargets(X);

            var score = new RSquared(predictions, y);
            Console.WriteLine($"RSquared score: {score.GetResult()}");
        }

        public (double[][], double[]) ReadCSV(string path) 
        {
            List<double[]> listA = new List<double[]>();
            List<double> listB = new List<double>();
            bool isHeader = true;
            using(var reader = new StreamReader(path))
            {
                
                while (!reader.EndOfStream)
                {
                    var line = reader.ReadLine();
                    var values = line.Split(',');

                    if (isHeader)
                    {
                        isHeader = false;
                        continue;
                    }

                    double[] X = new double[2]
                    {
                        (double)double.Parse(values[1], CultureInfo.InvariantCulture),
                        (double)double.Parse(values[2], CultureInfo.InvariantCulture)
                    };

                    listA.Add(X);
                    listB.Add((double)double.Parse(values[3], CultureInfo.InvariantCulture));
                }
            }

            return (listA.ToArray(), listB.ToArray());
        }
    }
}