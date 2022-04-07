using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SimpleStat1;

namespace MyStatLib
{
    /// <summary>
    /// генератор эллипсоида, ортогонального осям c центром в начале координат
    /// </summary>
    public class TEllipsoid
    {
        /// <summary>
        /// начальное значение для генераторов случайных чисел
        /// </summary>
        public int Seed { get; set; } = 0;

        public TNormDistr NormDistr { get; set; } = new TNormDistr();
        /// <summary>
        /// количество размерностей
        /// </summary>
        public int DimCount { get; set; } = 4;

        /// <summary>
        /// диапазон значений стандартных отклонений
        /// </summary>
        public TDiapason SDDiapason { get; set; } = new TDiapason() { Nomimal = 3, TMinus = -2, TPlus = 2, HardCheck = false, KSoft = 0.2 };

        public TDiapason VolumeDiapason { get; set; } = new TDiapason() { Nomimal = 1800, TMinus = -480, TPlus = 480, HardCheck = false, KSoft = 0.2 };

        /// <summary>
        /// массив стандартных отклонений
        /// </summary>
        public List<double> Sd { get; set; } = new List<double>();

        /// <summary>
        /// углы поворота
        /// </summary>
        public List<double> Angle { get; set; } = new List<double>();

        /// <summary>
        /// установка объема либо по диапазону {VolumeDiapason} либо value
        /// </summary>
        /// <param name="value"> объем данных </param>
        public void SetVolume(int value = 0)
        {
            if (value < 3)
            {
                var nd = new TNormDistr();
                nd.MinInt = (int)VolumeDiapason.MinValue;
                nd.MaxInt = (int)VolumeDiapason.MaxValue;
                nd.Reset(Seed);

                value = nd.NextInt;
            }

            NormDistr.Volume = value;
        }
        /// <summary>
        /// случайные стандартные отклонения по числу размерностей, отсортированы по убыванию
        /// </summary>
        public void SetSD()
        {
            List<double> sds = new List<double>();

            var nd = new TNormDistr();
            nd.MinInt = (int)VolumeDiapason.MinValue;
            nd.MaxInt = (int)VolumeDiapason.MaxValue;

            nd.mean = SDDiapason.Nomimal;
            nd.sd = SDDiapason.Range / 6.0;

            nd.Reset(Seed);

            for (int i = 0; i < DimCount; i++)
            {
                sds.Add(SDDiapason.Correct(nd.NextNormal));
            }

            sds.Sort();
            sds.Reverse();
            Sd = sds;

        }

        public void SetAngle()
        {
            List<double> a = new List<double>();

            var nd = new TNormDistr();
            nd.MinInt = (int)VolumeDiapason.MinValue;
            nd.MaxInt = (int)VolumeDiapason.MaxValue;

            nd.mean = 0;
            nd.sd = 2.0 * Math.PI / 6.0;

            nd.Reset(Seed);

            for (int i = 0; i < DimCount; i++)
            {
                a.Add(nd.NextNormal);
            }

            Angle = a;
        }


        public List<List<double>> Items { get; set; } = new List<List<double>>();
        public List<List<double>> RotatedItems { get; set; } = new List<List<double>>();

        public void SetItems()
        {
            Items.Clear();
            for (int i = 0; i < DimCount; i++)
            {
                Items.Add(new List<double>());
            }
            RotatedItems.Clear();
            for (int i = 0; i < DimCount; i++)
            {
                RotatedItems.Add(new List<double>());
            }
        }

        public void SetData()
        {
            NormDistr.Reset();

            for (int i = 0; i < Items.Count; i++)
            {
                //var dim =Items[i];
                (Items[i]).Clear();
                NormDistr.mean = 0.0;
                NormDistr.sd = Sd[i];
                (Items[i]) = NormDistr.GenerateSample();
            }
        }

        public void RotateData()
        {
            for (int i = 0; i < DimCount; i++)
            {
                var x = Items[i];
                var xr = RotatedItems[i];
                xr.Clear();
                xr.AddRange(x);
            }

            for (int i = 0; i < DimCount; i++)
            {
                var xr = RotatedItems[i];
                var yr = RotatedItems[i % DimCount];
                for (int j = 0; j < xr.Count; j++)
                {
                    var r = Math.Sqrt(xr[j] * xr[j] + yr[j] * yr[j]);
                    var alpha = Math.Atan2(yr[j], xr[j]);
                    alpha = alpha + Angle[i];
                    xr[j] = (r * Math.Cos(alpha));
                    yr[j] = (r * Math.Sin(alpha));
                }
            }
        }

        public void CorrectSD(double k = 0.4)
        {
            for (int i = 0; i < Sd.Count; i++)
            {
                Sd[i] = Sd[i]* Math.Pow(k,i);
            }
        }

        public void mix23()
        {
            for (int i = 0; i < Items[0].Count; i++)
            {
                Items[0][i] += Items[1][i];
                Items[1][i] += Items[2][i];
                Items[2][i] += Items[3][i];
            }
        }
        public void Setup(bool AdjustSd=true, bool mix23=true)
        {
            SetVolume();
            SetSD();
            if (AdjustSd)
            {
                CorrectSD();
            }
            SetAngle();
            SetItems();
            SetData();
            if (mix23)
            {
                this.mix23();
            }

            RotateData();
        }

    }
}
