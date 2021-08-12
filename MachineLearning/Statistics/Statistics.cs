namespace DotNetML.Statistics
{
    public abstract class Statistics
    {
        public abstract double GetResult();
        public abstract Statistics SetData(double[] newData);
    }
}