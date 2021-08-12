namespace DotNetML.ModelSelection
{
    public class TrainingDataset
    {
        public double[][] Data { get; set; }
        public double[] Target { get; set; }
        

        public TrainingDataset(double[][] data, double[] target)
        {
            Data = data;
            Target = target;
        }
    }
}