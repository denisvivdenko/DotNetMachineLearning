using System;
using System.Collections.Generic;
using System.IO;
using DotNetML.LinearRegression;
using DotNetML.ModelSelection;
using DotNetML.Metrics;

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
            Console.WriteLine($"RSquared: {rSquared}");
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

                    listA.Add((double)Int32.Parse(values[1]));
                    listB.Add((double)Int32.Parse(values[2]));
                }
            }

            return (listA.ToArray(), listB.ToArray());
        }
    }
}