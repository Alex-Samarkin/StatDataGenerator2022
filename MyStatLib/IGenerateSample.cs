// SimpleStat
// MyStatLib
// IGenerateSample.cs
// ---------------------------------------------
// Alex Samarkin
// Alex
// 
// 2:54 26 02 2022

using System.Collections.Generic;

namespace MyStatLib
{
    public interface IGenerateSample
    {
        /// <summary>
        /// генерация единственного случайного числа
        /// </summary>
        /// <param name="c">случайное число</param>
        void GenerateSample(out double[] c);
        /// <summary>
        /// генерация массива случайных числе
        /// </summary>
        /// <param name="volume">объем выборки</param>
        /// <param name="c">массив</param>
        void GenerateSample(int volume, out double[] c);
        /// <summary>
        /// генерация выборки как списка, объем определяется классом реализующим интерфейс
        /// </summary>
        /// <returns>список случайных чисел</returns>
        List<double> GenerateSample();
        /// <summary>
        /// генерация выборки случайных чисел как списка
        /// </summary>
        /// <param name="volume">объем выборки</param>
        /// <returns>список случаййных чисел</returns>
        List<double> GenerateSample(int volume);

        int Volume { get; set; }
    }
}