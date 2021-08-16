using NUnit.Framework;
using DotNetML.MathFunctions;

namespace Tests
{
	public class MathTests
	{

		[Test]
		public void ErrorFunctionExpected1()
		{

			double x = 121.2;
			double actual = new ErrorFunction(x).GetResult();
			double expected = 1;

			Assert.AreEqual(expected, actual);
		}


		[Test]
		public void ErrorFunctionExpectedMinus1()
		{

			double x = -121.2;
			double actual = new ErrorFunction(x).GetResult();
			double expected = -1;

			Assert.AreEqual(expected, actual);
		}


		[Test]
		public void ErrorFunctionExpectedPositive()
		{

			double x = 1;
			double actual = new ErrorFunction(x).GetResult();
			double expected = 0.8427007929497149;

			Assert.That(actual, Is.EqualTo(expected).Within(0.0001));
		}


		[Test]
		public void ErrorFunctionExpectedNegative()
		{

			double x = -1;
			double actual = new ErrorFunction(x).GetResult();
			double expected = -0.8427007929497149;

			Assert.That(actual, Is.EqualTo(expected).Within(0.0001));
		}
	}
}
