namespace DotNetML.DecisionTree
{
    public class Decision : TreeComponent
    {
        public double[] Value { get; private set; }


        public Decision(double[] value)
        {
            Value = value;
            Type = DecisionTreeComponentType.Decision;
        }
    }
}