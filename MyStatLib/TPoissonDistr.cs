// SimpleStat
// MyStatLib
// TPoissonDistr.cs
// ---------------------------------------------
// Alex Samarkin
// Alex
// 
// 5:38 09 03 2022

using System;
using System.Collections.Generic;
using MathNet.Numerics.Distributions;
using SimpleStat1;

namespace MyStatLib
{
    public class TPoissonDistr : TRandGen, IGenerateSample
    {
        public uint K { get; set; }

        #region Implementation of IGenerateSample

        public void GenerateSample(out double[] c)
        {
            Volume = Volume < 3 ? 3 : Volume;
            var c1 = new int[Volume];
            Poisson.Samples(rng, c1, K);
            c = new double[Volume];
            for (int i = 0; i < c.Length; i++)
            {
                c[i] = c1[i];
            }
        }

        public void GenerateSample(int volume, out double[] c)
        {
            Volume = volume;
            GenerateSample(out c);
        }

        public List<double> GenerateSample()
        {
            double[] c;
            GenerateSample(out c);
            return new List<double>(c);
        }

        public List<double> GenerateSample(int volume)
        {
            Volume = volume;
            double[] c;
            GenerateSample(out c);
            return new List<double>(c);
        }

        #endregion
    }
}