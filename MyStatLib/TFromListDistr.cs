using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using MathNet.Numerics.Distributions;
using MathNet.Numerics.Random;
using SimpleStat1;

namespace MyStatLib
{
    public class TFromListDistr<T> : TRandGen
    {
        public List<T> ItemList { get; set; } = new List<T>();

        public int Count => ItemList.Count;

        public int MinInt => (base.MinInt = 0);
        public int MaxInt => (base.MaxInt = ItemList.Count);

        public T this[int index]
        {
            get
            {
                return ItemList[index];
            }
            set
            {
                ItemList[index] = value;
            }
        }

        #region Implementation of IGenerateSample

        public void GenerateSample(out T[] c)
        {
            // throw new NotImplementedException();
            Volume = Volume < 3 ? 3 : Volume;

            base.MinInt = 0;
            base.MaxInt = Count;

            c = new T[Volume];
            for (int i = 0; i < Volume; i++)
            {
                c[i] = ItemList[NextInt];
            }
        }

        public void GenerateSample(int volume, out T[] c)
        {
            Volume = volume;
            GenerateSample(out c);
        }

        public virtual List<T> GenerateSample()
        {
            T[] c;
            GenerateSample(out c);
            return new List<T>(c);
        }

        public virtual List<T> GenerateSample(int volume)
        {
            Volume = volume;
            T[] c;
            GenerateSample(out c);
            return new List<T>(c);
        }

        #endregion
    }

    public class TFromStringList : TFromListDistr<string>
    {}

    public class TYesNoStringList : TFromStringList
    {
        public TYesNoStringList()
        {
            ItemList.Clear();
            ItemList.Add("No");
            ItemList.Add("Yes");
        }

    }
    public class TGenderStringList : TFromStringList
    {
        public TGenderStringList()
        {
            ItemList.Clear();
            ItemList.Add("Female");
            ItemList.Add("Male");
        }

    }

}
