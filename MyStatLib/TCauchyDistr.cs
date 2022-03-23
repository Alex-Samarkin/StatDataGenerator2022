// SimpleStat
// MyStatLib
// TCauchyDistr
// ---------------------------------------------
// Alex Samarkin
// Alex
// 
// 1:32 23 03 2022

using System.Collections.Generic;
using MathNet.Numerics.Distributions;
using SimpleStat1;

namespace MyStatLib
{
    public class TCauchyDistr : TRandGen, IGenerateSample
    {
        public double mean { get; set; } = 0;
        public double sd { get; set; } = 1;

        public double location { get=>mean; set=>mean=value; }
        public double scale { get=>sd; set=>sd=value; }

        public double NextNormal => Normal.Sample(rng, mean, sd);
        public double NextCauchy => Cauchy.Sample(rng, mean, sd);

        // moved to TRandGen
        //public int Volume { get; set; } = 1000;

        #region IGenerateSample

        public virtual void GenerateSample(out double[] c)
        {
            Volume = Volume < 3 ? 3 : Volume;
            c = new double[Volume];
            Cauchy.Samples(rng, c, mean, sd);
        }
        public virtual void GenerateSample(int volume, out double[] c)
        {
            Volume = volume;
            GenerateSample(out c);
        }

        public virtual List<double> GenerateSample()
        {
            double[] c;
            GenerateSample(out c);
            return new List<double>(c);
        }
        public virtual List<double> GenerateSample(int volume)
        {
            Volume = volume;
            double[] c;
            GenerateSample(out c);
            return new List<double>(c);
        }

        #endregion
    }
}