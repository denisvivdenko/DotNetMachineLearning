using System;
using System.Collections.Generic;
using System.Text;

namespace DotNetML.DecisionTree
{
	public class DecisionTreeEvaluator
	{
		private double[] _prediction;


		public DecisionTreeEvaluator(Node rootNode, double[] predictingData)
		{
			_prediction = EvaluateTree(rootNode, predictingData);
		}


		public double[] GetResult()
		{
			return _prediction;
		}


		private double[] EvaluateTree(Node rootNode, double[] predictionData)
		{
			var componentType = rootNode.Type;
			int featuresNumber = predictionData.Length;
			Node currentNode = rootNode;

			for (int iteration = 0; iteration < featuresNumber; iteration++)
			{
				foreach (Node subnode in currentNode.Values)
				{
					if (subnode.Values[0].Type == DecisionTreeComponentType.Decision)
					{
						Decision decision = (Decision) subnode.Values[0];
						return decision.Value;
					}
					else if (subnode.IsCorrespondCondition(predictionData))
					{
						currentNode = subnode;
						break;
					}
				}
			}

			throw new Exception("haven't found any solution");
		} 
	}
}
