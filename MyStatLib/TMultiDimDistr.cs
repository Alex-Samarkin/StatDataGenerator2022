// SimpleStat
// MyStatLib
// TMultiDimDistr.cs
// ---------------------------------------------
// Alex Samarkin
// Alex
// 
// 1:27 28 03 2022

using System.Collections.Generic;
using SimpleStat1;

namespace MyStatLib
{
    public class TMultiDimDistr:TNormDistr,IGenerateSample
    {
        public List<IGenerateSample> RandGens { get; set; } = new List<IGenerateSample>(){new TNormDistr(),new TNormDistr(),new TNormDistr()};
        public IGenerateSample XGen { get=>RandGens[0]; set=>RandGens[0]=value; }
        public IGenerateSample YGen { get => RandGens[1]; set => RandGens[1] = value; }
        public IGenerateSample ZGen { get => RandGens[2]; set => RandGens[2] = value; }

        public List<double> XSampleList()
        {
            return XGen.GenerateSample(Volume);
        }
        public List<double> YSampleList()
        {
            return YGen.GenerateSample(Volume);
        }
        public List<double> ZSampleList()
        {
            return YGen.GenerateSample(Volume);
        }

        public override void GenerateSample(out double[] c)
        {
            Volume = Volume < 3 ? 3 : Volume;
            c = new double[Volume];
            base.GenerateSample(out c);
        }
    
        public override void  GenerateSample(int volume, out double[] c)
        {
            Volume = volume;
            GenerateSample(out c);
        }

        public override List<double> GenerateSample()
        {
            double[] c;
            GenerateSample(out c);
            return new List<double>(c);
        }

        public override List<double> GenerateSample(int volume)
        {
            Volume = volume;
            double[] c;
            GenerateSample(out c);
            return new List<double>(c);
        }

        public override void Reset()
        {
            base.Reset();
            foreach (var gen in RandGens)
            {
                Reset();
            }
        }
        public override void Reset(int newSeed)
        {
            base.Reset(Seed);
            foreach (var gen in RandGens)
            {
                Reset(Seed);
            }
        }
        public void Reset(int newSeed, int step)
        {
            base.Reset(Seed);
            foreach (var gen in RandGens)
            {
                Reset(Seed+step);
                step += step;
            }
        }

    }
}