// SimpleStat
// MyStatLib
// TExpDistri.cs
// ---------------------------------------------
// Alex Samarkin
// Alex
// 
// 1:44 23 03 2022

using System.Collections.Generic;
using MathNet.Numerics.Distributions;
using SimpleStat1;

namespace MyStatLib
{
    public class TExpDistr2 : TRandGen, IGenerateSample
    {
        public double mean { get; set; } = 0;
        public double sd { get; set; } = 1;

        public double rate { get => mean; set => mean = value; }

        public double NextNormal => Normal.Sample(rng, mean, sd);
        public double NextExp => Exponential.Sample(rng, rate);

        // moved to TRandGen
        //public int Volume { get; set; } = 1000;

        #region IGenerateSample

        public virtual void GenerateSample(out double[] c)
        {
            Volume = Volume < 3 ? 3 : Volume;
            c = new double[Volume];
            Exponential.Samples(rng, c, mean);
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