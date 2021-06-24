using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace ModelsTesting
{
	public class BernoulliNaiveBayesTest
	{
		public BernoulliNaiveBayesTest()
		{
            string dir = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location);
            string file = dir + "\\datasets\\spam_none_spam_dataset.csv";

            List<string> textData = new List<string>();
            List<int> target = new List<int>();
            using (var reader = new StreamReader(file))
            {
                bool header = true;
                while (!reader.EndOfStream)
                {
                    var line = reader.ReadLine();
                    if (line == "" || header)
					{
                        header = false;
                        continue;
					}

                    var values = line.Split(',');

                    textData.Add(values[0]);

                    int targetValue = 0;
                    if (values[1] == "True")
					{
                        targetValue = 1;
					}

                    target.Add(targetValue);
                }
            }

            Console.WriteLine();
        }
	}
}
