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
    public class TMultiDimDistr
    {
        public List<TNormDistr> RandGens { get; set; } = new List<TNormDistr>(){new TNormDistr(),new TNormDistr(),new TNormDistr()};
        public TNormDistr XGen { get=>RandGens[0]; set=>RandGens[0]=value; }
        public TNormDistr YGen { get => RandGens[1]; set => RandGens[1] = value; }
        public TNormDistr ZGen { get => RandGens[2]; set => RandGens[2] = value; }

        public int Volume { get => XGen.Volume; set=>XGen.Volume = value; }

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

        public List<List<double>> GetAllSamples()
        {
            List<List<double>> res = new List<List<double>>();
            foreach (var gen in RandGens)
            {
                res.Add(gen.GenerateSample(Volume));
            }

            return res;
        }

        public List<List<double>> GetXYZSamples()
        {
            List<List<double>> res = new List<List<double>>();
            res.Add(XGen.GenerateSample(Volume));
            res.Add(YGen.GenerateSample(Volume));
            res.Add(ZGen.GenerateSample(Volume));

            return res;
        }


        public void GenerateSample(out double[] c)
        {
            Volume = Volume < 3 ? 3 : Volume;
            c = new double[Volume];
            XGen.GenerateSample(out c);
        }
    
        public void  GenerateSample(int volume, out double[] c)
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

        public void Reset()
        {
            foreach (var gen in RandGens)
            {
                gen.Reset();
            }
        }
        public void Reset(int newSeed)
        {
            foreach (var gen in RandGens)
            {
                gen.Reset(newSeed);
            }
        }
        public void Reset(int newSeed, int step)
        {
            // base.Reset(Seed);
            foreach (var gen in RandGens)
            {
                gen.Reset(newSeed+step);
                step += step;
            }
        }

    }
}