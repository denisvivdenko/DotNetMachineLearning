using System.Collections.Generic;

namespace DotNetML.DecisionTree
{
    public class Node : TreeComponent
    {
        public List<TreeComponent> Values { get; private set; }
        public double Entropy { get; set; }
        private int _featureIndex;
        private double _featureValue;


        public Node(List<TreeComponent> values, int featureIndex, double featureValue)
        {
            Values = values;
            _featureIndex = featureIndex;
            _featureValue = featureValue;
            Type = DecisionTreeComponentType.Node;
        }


        public Node(List<TreeComponent> values, DecisionTreeComponentType nodeType)
		{
            if (nodeType == DecisionTreeComponentType.Decision)
			{
                throw new System.Exception("error when passing to node Decision type");
			}

            Values = values;
            Type = nodeType;
		}


        public bool IsCorrespondCondition(double[] record)
		{
            if (record[_featureIndex] == _featureValue)
			{
                return true;
			}

            return false;
		}
    }
}