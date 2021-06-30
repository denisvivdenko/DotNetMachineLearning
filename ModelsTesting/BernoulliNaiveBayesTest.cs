using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using DotNetML.NaiveBayes;
using DotNetML.Metrics;

namespace ModelsTesting
{
	public class BernoulliNaiveBayesTest
	{
		public BernoulliNaiveBayesTest()
		{
            string dir = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location);
            string file = dir + "\\datasets\\processed_spam_dataset.csv";

            var (data, target) = ReadCSVFile(file);

            var classifier = new BernoulliNaiveBayesClassifier();
            classifier.TrainModel(data, target);
            classifier.PrintInfo();

            Console.WriteLine("+++");
            int[] predictions = classifier.PredictLabels(data);

            for (int i = 0; i < target.Length; i++)
			{   
                Console.WriteLine($"id:{i}\t{predictions[i]}\t {target[i]}");
			}

            Console.WriteLine($"Precision: {new Precision(target, predictions).GetResult()}");
            Console.WriteLine($"Recall: {new Recall(target, predictions).GetResult()}");
            Console.WriteLine($"F1: {new F1Score(target, predictions).GetResult()}");


            Console.WriteLine();
        }


        private (int[][], int[]) ReadCSVFile(string path)
		{
            List<int[]> data = new List<int[]>();
            List<int> target = new List<int>();
            using (var reader = new StreamReader(path))
            {
                bool header = true;
                int rowLength = 0;
                while (!reader.EndOfStream)
                {
                    var line = reader.ReadLine();
                    if (line == "" || header)
                    {
                        header = false;
                        continue;
                    }

                    var values = line.Split(',');
                    List<int> wordsData = new List<int>();

                    rowLength = values.Length;

                    for (int index = 0; index < values.Length; index++)
                    {
                        if (index == rowLength - 1)
                        {
                            target.Add(Int32.Parse(values[index]));
                            break;
                        }

                        wordsData.Add(Int32.Parse(values[index]));
                    }

                    data.Add(wordsData.ToArray());
                }
            }

            return (data.ToArray(), target.ToArray());
        }
	}
}
