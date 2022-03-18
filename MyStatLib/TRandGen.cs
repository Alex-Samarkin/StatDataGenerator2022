using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using MathNet;
using MathNet.Numerics;
using MathNet.Numerics.Distributions;
using MathNet.Numerics.Random;


namespace SimpleStat1
{
    public class TRandGen
    {
        public int Seed { get; set; } = 23;
        public System.Random rng { get; set; } = new SystemRandomSource();

        public void Reset()
        {
            // заменить на другой при необходимости
            // 
            // rng = new SystemRandomSource(Seed);
            rng = new MersenneTwister(Seed);
        }
        public void Reset(int newSeed)
        {
            Seed = newSeed;
            Reset();
        }

        public void ResetInt(int Cycles)
        {
            Reset();
            for (int i = 0; i < Cycles; i++)
            {
                rng.Next();
            }
        }
        public void ResetDouble(int Cycles)
        {
            Reset();
            for (int i = 0; i < Cycles; i++)
            {
                rng.NextDouble();
            }
        }

        public int Volume { get; set; } = 1000;
        public double NextDouble => rng.NextDouble();
        public int MinInt { get; set; } = 0;
        public int MaxInt { get; set; } = 100;
        public int NextInt => rng.Next(MinInt,MaxInt);
    }
}
