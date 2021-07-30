using System;
using System.Collections.Generic;
using System.Text;

namespace DotNetML.Metrics
{
	public class F1Score : Metric
	{
		private double _f1Score;


		public F1Score(int[] expected, int[] actual) //	multiclass score
		{
			_f1Score = CalculateF1Score(expected, actual);
		}


		public override double GetResult()
		{
			return _f1Score;
		}


		private double CalculateF1Score(int[] expected, int[] actual)
		{
			Precision precisionMetric = new Precision(expected, actual);
			Recall recallMetric = new Recall(expected, actual);

			double precision = precisionMetric.GetResult();
			double recall = recallMetric.GetResult();

			double f1Score = (2 * precision * recall) / (precision + recall);

			return f1Score;
		}
	}
}
