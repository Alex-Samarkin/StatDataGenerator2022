// SimpleStat
// SimpleStat1
// TDiapason.cs
// ---------------------------------------------
// Alex Samarkin
// Alex
// 
// 0:37 14 02 2022

using System.Collections.Generic;

namespace SimpleStat1
{
    public class TDiapason
    {
        public double Nomimal { get; set; } = 0;
        public double TMinus { get; set; } = -0.1;
        public double TPlus { get; set; } = +0.2;
        public bool HardCheck { get; set; } = true;
        public double KSoft { get; set; } = 0.05;

        public double MinValue => Nomimal + TMinus;
        public double MaxValue => Nomimal + TPlus;
        public double Range => MaxValue - MinValue;
        public double MidValue => (MaxValue + MinValue) / 2.0;

        public double SoftMinValue => Nomimal + TMinus-Range*KSoft;
        public double SoftMaxValue => Nomimal + TPlus+Range*KSoft;
        public double SoftRange => SoftMaxValue - SoftMinValue;

        public int Check(double value)
        {
            if (HardCheck)
            {
                if (value < MinValue) return -1;
                else if (value > MaxValue) return 1;
                return 0;
            }

            if (value < SoftMinValue) return -1;
            else if (value > SoftMaxValue) return 1;
            return 0;
        }

        public double Correct(double value)
        {
            int flag = Check(value);
            if (HardCheck)
            {
                if (flag < 0) return MinValue;
                if (flag > 0) return MaxValue;
            }
            else
            {
                if (flag < 0) return SoftMinValue;
                if (flag > 0) return SoftMaxValue;
            }
            return value;
        }

        public void Correct(double[] c)
        {
            for (int i = 0; i < c.Length; i++)
            {
                c[i] = Correct(c[i]);
            }
        }
        public void Correct(List<double> c)
        {
            for (int i = 0; i < c.Count; i++)
            {
                c[i] = Correct(c[i]);
            }
        }
    }
}