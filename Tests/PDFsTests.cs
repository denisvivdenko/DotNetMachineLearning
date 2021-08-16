using NUnit.Framework;
using System.Collections.Generic;
using System;
using DotNetML.PDFs;
using DotNetML.Statistics;

namespace Tests
{
	public class PDFsTests
	{
        
        [Test]
		public void NormalDistributionTest()
		{
            double[] x = {140, 150, 155, 156, 160, 162, 170, 175, 173, 172, 170, 182, 180, 190, 202};
            double mean = new Mean(x).GetResult();
            double sd = new StandardDeviation(x).GetResult();
            
			double[] actual = new NormalDistribution(x, mean, sd).GetResult();
            double[] expected = { 0.00436264, 0.01199304, 0.01699324, 0.01799228, 0.02168345,
                                        0.02321296, 0.0257838 , 0.02402777, 0.02502801, 0.02538359,
                                        0.0257838 , 0.01825555, 0.02016439, 0.01037152, 0.00268619 };

			Assert.That(actual, Is.EqualTo(expected).Within(0.001));
		}
	}
}
