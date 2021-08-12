namespace DotNetML.ModelSelection
{
    public class TrainDataset
    {
        public double[][] Data { get; set; }
        public double[] Target { get; set; }
        

        public TrainDataset(double[][] data, double[] target)
        {
            Data = data;
            Target = target;
        }
    }
}