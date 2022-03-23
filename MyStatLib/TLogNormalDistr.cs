// SimpleStat
// MyStatLib
// TLogNormalDistr.cs
// ---------------------------------------------
// Alex Samarkin
// Alex
// 
// 1:38 23 03 2022

using System.Collections.Generic;
using MathNet.Numerics.Distributions;
using SimpleStat1;

namespace MyStatLib
{
    public class TLogNormalDistr : TRandGen, IGenerateSample
    {
        public double mean { get; set; } = 0;
        public double sd { get; set; } = 1;

        public double mu { get => mean; set => mean = value; }
        public double sigma { get => sd; set => sd = value; }


        public double NextNormal => Normal.Sample(rng, mean, sd);
        public double NextLogNormal => LogNormal.Sample(rng, mean, sd);

        // moved to TRandGen
        //public int Volume { get; set; } = 1000;

        #region IGenerateSample

        public virtual void GenerateSample(out double[] c)
        {
            Volume = Volume < 3 ? 3 : Volume;
            c = new double[Volume];
            LogNormal.Samples(rng, c, mean, sd);
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