// SimpleStat
// MyStatLib
// TBiModalNormDistr.cs
// ---------------------------------------------
// Alex Samarkin
// Alex
// 
// 2:50 26 02 2022

using System.Collections.Generic;
using System.Linq;
using SimpleStat1;

namespace MyStatLib.Properties
{
    /// <summary>
    /// двумодальное распределение, получается как сумма двух нормальных распределений
    /// оба распределения имеют независимый объем и параметры
    /// версия генератора с параметром volume генерирует выборку в пропорции 2/3 - основное распределение
    /// 1/3 - вторичное
    /// если нужны иные пропорции следует установить volume класса для основного распределения
    /// и SecondNormDistr.volume для вторичного
    ///
    /// соединение выборок происходит по схеме основная выборка, вторичная выборка,
    /// перемешивание не осуществляется
    /// 
    /// </summary>
    public class TBiModalNormDistr : TNormDistr,IGenerateSample
    {

        public TNormDistr SecondNormDistr { get; set; } = new TNormDistr();

        #region Implementation of IGenerateSample

        #region Overrides of TNormDistr
        public override void GenerateSample(out double[] c)
        {
            double[] c1;
            base.GenerateSample(out c1);
            double[] c2;
            SecondNormDistr.GenerateSample(out c2);
            c = c1.Concat(c2).ToArray();
        }

        /// <summary>
        /// генерирует выборку заданного объема в пропорции 2/3 - основное распределение, 1/3 - вторичное
        /// </summary>
        /// <param name="volume"></param>
        /// <param name="c"></param>
        public override void GenerateSample(int volume, out double[] c)
        {
            double[] c1;
            base.GenerateSample(2*volume/3, out c1);
 
            double[] c2;
            SecondNormDistr.GenerateSample(volume-2*volume / 3,out c2);

            c = c1.Concat(c2).ToArray();

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

        #endregion

        #endregion

    }
}