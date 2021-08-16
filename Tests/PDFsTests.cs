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
		public void NormalDistributionPDFTest()
		{
            double[] x = {140, 150, 155, 156, 160, 162, 170, 175, 173, 172, 170, 182, 180, 190, 202};
            double mean = new Mean(x).GetResult();
            double sd = new StandardDeviation(x, freedomDegree:0).GetResult();
            
			double[] actual = new NormalDistributionPDF(x, mean, sd).GetResult();
            double[] expected = { 0.00436264, 0.01199304, 0.01699324, 0.01799228, 0.02168345,
                                        0.02321296, 0.0257838 , 0.02402777, 0.02502801, 0.02538359,
                                        0.0257838 , 0.01825555, 0.02016439, 0.01037152, 0.00268619 };

			Assert.That(actual, Is.EqualTo(expected).Within(0.000001));
		}


		[Test]
		public void NormalDistributionCDFTest()
		{
            double[] x = { 140, 150, 155, 156, 160, 162, 170, 175, 175, 176, 177, 182, 185, 190, 202 };
            double mean = new Mean(x).GetResult();
            double sd = new StandardDeviation(x, freedomDegree:0).GetResult();
            
			double[] actual = new NormalDistributionCDF(x, mean, sd).GetResult();
            double[] expected = { 0.02852356, 0.10104784, 0.16804223, 0.18427414, 0.25840918,
									0.30056077, 0.49165811, 0.61514777, 0.61514777, 0.63889101,
									0.66211077, 0.76788633, 0.82124172, 0.89135766, 0.9765164 };

			Assert.That(actual, Is.EqualTo(expected).Within(0.000001));
		}
	}
}
