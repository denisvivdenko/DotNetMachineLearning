using NUnit.Framework;
using System.Collections.Generic;
using System;
using DotNetML;

namespace Tests
{
	public class CorrelationTests
	{
		private double[] _firstDataset;
		private double[] _secondDataset;


		[SetUp]
		public void Setup()
		{
			_firstDataset = new double[] { 0, 1, 3, 2, 9, 1 };
            _secondDataset = new double[] { 15, 1, 2, 2, 10, 7 };
		}

		[Test]
		public void TestPartialCorrelation()
		{
            double actual = new DotNetML.Metrics.Correlation(_firstDataset, _secondDataset).GetResult();
            double expected = 0.103;

			Assert.AreEqual(expected, actual);
		}

        [Test]
		public void TestStrongCorrelation()
		{
            var firstDataset = new double[] { 0, 1, 3, 2, 9, 1 };
            var secondDataset = new double[] { 0, 1, 3, 2, 9, 1 };
            double actual = new DotNetML.Metrics.Correlation(firstDataset, secondDataset).GetResult();
            double expected = 1;
			Assert.AreEqual(expected, actual);
		}

        [Test]
        public void TestStrongNegativeCorrelation()
		{
            var firstDataset = new double[] { 1, 0 };
            var secondDataset = new double[] { 0, 1 };
            double actual = new DotNetML.Metrics.Correlation(firstDataset, secondDataset).GetResult();
            double expected = -1;
			Assert.AreEqual(expected, actual);
		}
	}
}
