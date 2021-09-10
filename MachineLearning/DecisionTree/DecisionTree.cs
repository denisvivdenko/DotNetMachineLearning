using System;
using System.Linq;
using DotNetML.ModelSelection;
using System.Collections.Generic;
using DotNetML.Statistics;

namespace DotNetML.DecisionTree
{
    public class DecisionTree : Model
    {
        private Node _rootNode;


        public DecisionTree()
        {
        }


        public override double[] PredictTarget(double[] inputVector)
        {
            var evaluator = new DecisionTreeEvaluator(_rootNode, inputVector);
            return evaluator.GetResult();
        }


		public override void TrainModel(TrainingDataset trainingDataset)
        {

            _rootNode = new Node(ConstructDecisionTree((TrainingDatasetCategoricalTarget) trainingDataset), 
                                                                            DecisionTreeComponentType.RootNode);
        }


        private List<TreeComponent> ConstructDecisionTree(TrainingDatasetCategoricalTarget dataset, double nodeValue = 0)
        {
            double entropy = new Entropy(dataset.Target).GetResult();
            if (entropy == 0)
			{
                var decisonLeaf = new Decision(dataset.Target[0]);
                return new List<TreeComponent>() { decisonLeaf };
			}
            else if (dataset.FeaturesCount <= 0)
			{
                var decisionLeaf = new Decision(dataset.GetMostFrequentTarget());
                return new List<TreeComponent>() { decisionLeaf };
            }

            int bestSelectingFeatureIndex = GetBestSelectingFeatureIndex(dataset);         
            double[] featureColumn = dataset.ExtractFeature(bestSelectingFeatureIndex);
            var disctinctValues = new HashSet<double>(featureColumn);

            List<TreeComponent> nodes = new List<TreeComponent>();
            foreach (double featureValue in disctinctValues)
			{
                var selectedDataset = dataset.SelectSubset(bestSelectingFeatureIndex, featureValue);
                selectedDataset = selectedDataset.DropFeature(bestSelectingFeatureIndex);
                var newNode = new Node(ConstructDecisionTree(selectedDataset), featureIndex: bestSelectingFeatureIndex,
                                                                featureValue: featureValue);
                nodes.Add(newNode);
			}

            return nodes;
        }


        private int GetBestSelectingFeatureIndex(TrainingDatasetCategoricalTarget dataset)
		{
            double minEntropy = 1;
            int bestFeatureIndex = 0;
            for (int featureIndex = 0; featureIndex < dataset.FeaturesCount; featureIndex++)
            {
                double[] featureColumn = dataset.ExtractFeature(featureIndex);

                var disctinctValues = new HashSet<double>(featureColumn);
                if (disctinctValues.Count() < 2)
                {
                    continue;
                }

                foreach (double featureValue in disctinctValues)
                {
                    var selectedDataset = dataset.SelectSubset(featureIndex, featureValue);
                    double selectedDatasetEntropy = new Entropy(selectedDataset.Target).GetResult();
                    if (selectedDatasetEntropy < minEntropy)
                    {
                        minEntropy = selectedDatasetEntropy;
                        bestFeatureIndex = featureIndex;
                    }
                }
            }

            return bestFeatureIndex;
        }

    }
}