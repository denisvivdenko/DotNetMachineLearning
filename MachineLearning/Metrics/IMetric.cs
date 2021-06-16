using System;
using System.Collections.Generic;
using System.Text;

namespace DotNetML.Metrics
{
	public interface IMetric
	{
		double GetResult();
	}
}
