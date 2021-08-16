namespace DotNetML.PDFs
{
    public abstract class Distribution
    {
        public abstract double[] GetResult();
        protected abstract double[] ComputeDensityFunction(double[] dataset);
    } 
}