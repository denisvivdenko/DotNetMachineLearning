using System;
using System.Collections.Generic;
using System.Text;

namespace DotNetML.LinearAlgebra
{
	public class Vector
	{
		private double[] _values;
		private int _size;


		public Vector(double[] values)
		{
			_values = values;
			_size = values.Length;
		}

		public double GetElement(int index)
		{
			return _values[index];
		}

		public int GetShape()
		{
			return _size;
		}

		public double ComputeDistance(Vector vector)
		{
			IsCompatible(vector);

			Vector resultingVector = SubstractVector(vector);
			double sumOfSquares = resultingVector.GetSumOfSquares();
			return Math.Sqrt(sumOfSquares);
		}

		private Vector SubstractVector(Vector vector)
		{
			IsCompatible(vector);

			double[] newValues = new double[vector.GetShape()];
			for (int elementIndex = 0; elementIndex < _size; elementIndex++)
			{
				newValues[elementIndex] = _values[elementIndex] - vector.GetElement(elementIndex);
			}

			return new Vector(newValues);
		}

		private double GetSumOfSquares()
		{
			double result = 0;

			for (int elementIndex = 0; elementIndex < _size; elementIndex++)
			{
				result += Math.Pow(_values[elementIndex], 2);
			}

			return result;
		}

		private bool IsCompatible(Vector vector)
		{
			if (_size != vector.GetShape())
			{
				throw new Exception("vectors have different shapes");
			}

			return true;
		}
	}
}
