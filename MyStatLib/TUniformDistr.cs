// SimpleStat
// MyStatLib
// TUniformDistr.cs
// ---------------------------------------------
// Alex Samarkin
// Alex
// 
// 1:56 28 03 2022

using System.Collections.Generic;
using MathNet.Numerics.Distributions;
using SimpleStat1;

namespace MyStatLib
{
    public class TUniformDistr : TRandGen, IGenerateSample
    {
        public double mean { get; set; } = 0;
        public double sd { get; set; } = 1;
        public double NextNormal => Normal.Sample(rng, mean, sd);

        public double LowLimit { get; set; } = 0;
        public double UpperLimit { get; set; } = 0;

        #region IGenerateSample

        public virtual void GenerateSample(out double[] c)
        {
            Volume = Volume < 3 ? 3 : Volume;
            c = new double[Volume];
            ContinuousUniform.Samples(rng, c, LowLimit, UpperLimit);
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