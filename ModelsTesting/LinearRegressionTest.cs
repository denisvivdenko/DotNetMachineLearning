using System;
using System.Collections.Generic;
using System.IO;
using DotNetML.LinearRegression;
using DotNetML.ModelSelection;
using DotNetML.Metrics;
using System.Globalization;

namespace ModelsTesting
{
    public class LinearRegressionTest
    {
        public LinearRegressionTest()
        {
            string dir = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location);
            string file = dir + "\\datasets\\simple_linear_regression.csv";

            var (X, y) = ReadCSV(file);
            
            var regressor = new SimpleLinearRegression();
            regressor.TrainModel(X, y);
            double[] predictions = regressor.PredictTargets(X);

            double rSquared = new RSquared(predictions, y).GetResult();
            regressor.PrintParameters();
            Console.WriteLine($"RSquared: {rSquared}");

            // ================

            var sgdRegressor = new GDSimpleLinearRegression(learningRate:0.0001, stepSizeThreshold:0.00001);
            sgdRegressor.TrainModel(X, y);
            double[] sgdPredictions = sgdRegressor.PredictTargets(X);

            double sgdRSquared = new RSquared(sgdPredictions, y).GetResult();
            sgdRegressor.PrintParameters();
            Console.WriteLine($"RSquared: {sgdRSquared}");
        }

        public (double[], double[]) ReadCSV(string path) 
        {
            List<double> listA = new List<double>();
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

                    listA.Add((double)double.Parse(values[1], CultureInfo.InvariantCulture));
                    listB.Add((double)double.Parse(values[2], CultureInfo.InvariantCulture));
                }
            }

            return (listA.ToArray(), listB.ToArray());
        }
    }
}