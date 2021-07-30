using System;

namespace DotNetML.Metrics
{
	public abstract class Metric
	{
		public abstract double GetResult();
		
		public bool CheckSetsCompatibility(double[] firstSet, double[] secondSet)
		{
			if (firstSet.Length == secondSet.Length) {
				return true;
			}

			throw new Exception("sets aren't compatible");
		}
	}
}
