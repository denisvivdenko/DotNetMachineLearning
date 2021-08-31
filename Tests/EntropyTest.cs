using NUnit.Framework;
using DotNetML.MathFunctions;
using DotNetML.Statistics;

namespace Tests
{
	public class EntropyTest
	{

		[Test]
		public void EntropyIsLow()
		{
            double[][] array = new double[][] 
            {
                new double[] {1, 0, 0, 0, 0},
                new double[] {1, 0, 0, 0, 0},
                new double[] {1, 0, 0, 0, 0},
                new double[] {1, 0, 0, 0, 0},
                new double[] {1, 0, 0, 0, 0},
                new double[] {0, 1, 0, 0, 0}
            };
            
            double actual = new Entropy(array).GetResult();
            double expected = 0.6500224216483541;

			Assert.AreEqual(expected, actual);
		}


        [Test]
        public void EntropyIsHigh()
        {
            double[][] array = new double[][] 
            {
                new double[] {1, 0, 0, 0, 0},
                new double[] {1, 0, 0, 0, 0},
                new double[] {1, 0, 0, 0, 0},
                new double[] {0, 1, 0, 0, 0},
                new double[] {0, 1, 0, 0, 0},
                new double[] {0, 1, 0, 0, 0}
            };

            double actual = new Entropy(array).GetResult();
            double expected = 1;

			Assert.AreEqual(expected, actual);

        }


        [Test]
        public void EntropyZeroProbabilityHandler()
        {
            double[][] array = new double[][] 
            {
                new double[] {0, 0, 0, 0, 0},
                new double[] {0, 0, 0, 0, 0},
                new double[] {0, 0, 0, 0, 0},
                new double[] {0, 0, 0, 0, 0},
                new double[] {0, 0, 0, 0, 0},
                new double[] {0, 0, 0, 0, 0}
            };

            double actual = new Entropy(array).GetResult();
            double expected = 0;

			Assert.AreEqual(expected, actual);
        }
	}
}
