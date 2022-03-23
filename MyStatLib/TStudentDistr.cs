// SimpleStat
// MyStatLib
// TStudentDistr.cs
// ---------------------------------------------
// Alex Samarkin
// Alex
// 
// 1:50 23 03 2022

using System.Collections.Generic;
using MathNet.Numerics.Distributions;
using SimpleStat1;

namespace MyStatLib
{
    public class TStudentDistr : TRandGen, IGenerateSample
    {
        public double mean { get; set; } = 0;
        public double sd { get; set; } = 1;

        public double location { get => mean; set => mean = value; }
        public double scale { get => sd; set => sd = value; }

        public int freedom { get; set; } = 32;


        public double NextNormal => Normal.Sample(rng, mean, sd);
        public double NextStudent => StudentT.Sample(rng, mean, sd, freedom);

        // moved to TRandGen
        //public int Volume { get; set; } = 1000;

        #region IGenerateSample

        public virtual void GenerateSample(out double[] c)
        {
            Volume = Volume < 3 ? 3 : Volume;
            c = new double[Volume];
            StudentT.Samples(rng, c, mean, sd, freedom);
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