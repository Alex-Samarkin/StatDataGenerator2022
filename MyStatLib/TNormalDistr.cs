// SimpleStat
// SimpleStat1
// TNormalDistr.cs
// ---------------------------------------------
// Alex Samarkin
// Alex
// 
// 23:36 13 02 2022

using System.Collections.Generic;
using MathNet.Numerics.Distributions;
using MyStatLib;

namespace SimpleStat1
{
    public class TNormDistr : TRandGen, IGenerateSample
    {
        public double mean { get; set; } = 0;
        public double sd { get; set; } = 1;

        public double NextNormal => Normal.Sample(rng, mean, sd);

        // moved to TRandGen
        //public int Volume { get; set; } = 1000;

        #region IGenerateSample

        public virtual void GenerateSample(out double[] c)
        {
            Volume = Volume < 3 ? 3 : Volume;
            c = new double[Volume];
            Normal.Samples(rng,c, mean, sd);
        }
        public virtual void GenerateSample(int volume,out double[] c)
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