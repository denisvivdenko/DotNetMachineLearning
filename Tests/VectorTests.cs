using NUnit.Framework;
using System.Collections.Generic;
using System;
using DotNetML.LinearAlgebra;

namespace Tests
{
	public class VectorTests
	{
		private Vector _vector1;
		private Vector _vector2;

		[SetUp]
		public void Setup()
		{
			_vector1 = new Vector(new double[2] { 1, 5 });
			_vector2 = new Vector(new double[2] { 2, 3 });
		}


		[Test]
		public void ComputeVectorDistanceTest()
		{
			double actual = _vector1.ComputeDistance(_vector2);
			Assert.AreEqual(Math.Sqrt(5), actual);
		}

		[Test]
		public void ExceptionVectorsHaveDifferentShapes()
		{
			Vector vector2 = new Vector(new double[3] { 2, 3, 5 });
			Assert.Throws<Exception>(() => _vector1.ComputeDistance(vector2));
		}
	}
}
