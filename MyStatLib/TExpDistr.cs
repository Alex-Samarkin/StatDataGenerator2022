// SimpleStat
// MyStatLib
// TExpDistr.cs
// ---------------------------------------------
// Alex Samarkin
// Alex
// 
// 5:07 09 03 2022

using System;
using System.Collections.Generic;
using MathNet.Numerics.Distributions;
using SimpleStat1;

namespace MyStatLib
{
    public class TExpDistr : TRandGen, IGenerateSample
    {
        public double Lambda
        {
            get
            {
                if(Lambda < Double.Epsilon) Lambda = Double.Epsilon;
                return Lambda;
            }
            set
            {
                if (value < Double.Epsilon) value = Double.Epsilon;
                Lambda = value;
            }
        }

        public TExpDistr(double newLambda = 1 )
        {
            Lambda = newLambda;
        }

        public double InvLambda
        {
            get
            {
                return 1.0 / Lambda;
            } 
            set
            {
                Lambda = 1.0 / value;
            }
        }

        #region Implementation of IGenerateSample

        public virtual void GenerateSample(out double[] c)
        {
            Volume = Volume < 3 ? 3 : Volume;
            c = new double[Volume];
            Exponential.Samples(rng, c, Lambda);
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