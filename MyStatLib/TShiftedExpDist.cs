// SimpleStat
// MyStatLib
// TShiftedExpDist.cs
// ---------------------------------------------
// Alex Samarkin
// Alex
// 
// 5:29 09 03 2022

using System.Collections.Generic;
using MathNet.Numerics.Distributions;

namespace MyStatLib
{
    public class TShiftedExpDist : TExpDistr
    {
        public double Shift { get; set; } = 0;

        #region Implementation of IGenerateSample

        public override void GenerateSample(out double[] c)
        {
            Volume = Volume < 3 ? 3 : Volume;
            c = new double[Volume];
            Exponential.Samples(rng, c, Lambda);
            for (int i = 0; i < c.Length; i++)
            {
                c[i] += Shift;
            }
        }

        #endregion
    }
}