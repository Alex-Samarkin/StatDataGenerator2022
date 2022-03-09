//--------------------------
using SimpleStat3;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MyStatLib;
using SimpleStat1;

namespace SimpleStat3
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();

            propertyGrid1.SelectedObject = user;

            Generate();
        }

        private TUser user = new TUser();
        private TNormDistr normDistr = new TNormDistr();
        private TPoissonDistr poissonDistr1 = new TPoissonDistr();
        private TPoissonDistr poissonDistr2 = new TPoissonDistr();
        private TDiapason diapason = new TDiapason();

        public List<double> Items = new List<double>();
        public List<double> Items1 = new List<double>();
        public List<double> Items2 = new List<double>();

        public void Generate()
        {
            // среднее число звонков в час
            double t = 150;
            int h = user.GetHashCode();
            normDistr.Seed = user.Hash;
            normDistr.Reset();
            normDistr.MinInt = (int)(0.8 * t*2);
            normDistr.MaxInt = (int)(1.8 * t*2);


            int N = normDistr.NextInt;
            int N1 = normDistr.NextInt;
            int N2 = normDistr.NextInt;

            diapason.Nomimal = Math.Round((double)normDistr.NextInt / 10, 1);
            diapason.TMinus = Math.Round(-(double)normDistr.NextInt / 2000, 3);
            diapason.TPlus = Math.Round((double)normDistr.NextInt / 1800, 3);

            normDistr.mean = diapason.MidValue;
            normDistr.sd = Math.Round(diapason.Range / 5.0, 3);

            poissonDistr1.Seed = user.Hash;
            poissonDistr1.Reset();
            poissonDistr2.Seed = user.Hash;
            poissonDistr2.Reset();

            normDistr.Volume = N;
            poissonDistr1.Volume = N1;
            poissonDistr2.Volume = N2;

            Items = normDistr.GenerateSample();

            normDistr.MinInt = (int)(0.8 * t / 40.0);
            normDistr.MaxInt = (int)(1.3 * t / 40.0);

            poissonDistr1.K = (uint)normDistr.NextInt;
            poissonDistr2.K = (uint)normDistr.NextInt;

            Items1 = poissonDistr1.GenerateSample();
            Items2 = poissonDistr2.GenerateSample();
            Items2.Clear();
            Items2 = poissonDistr2.GenerateSample();

        }

        public void FillGrid()
        {
            dataGridView1.Rows.Clear();

            List<double> itm = Items1.Count > Items2.Count ? Items1 : Items2;

            for (int j = 0; j < itm.Count; j++)
            {
                var index = dataGridView1.Rows.Add();
                dataGridView1.Rows[index].Cells[0].Value = j;
                if (j<Items1.Count)
                {
                    dataGridView1.Rows[index].Cells[1].Value = Items1[j];
                }
                if (j < Items2.Count)
                {
                    dataGridView1.Rows[index].Cells[2].Value = Items2[j];
                }
            }
        }

        public void FillChart()
        {
            var sr = chart1.Series[0];
            sr.Points.Clear();
            int i = 1;
            foreach (double item in Items1)
            {
                sr.Points.AddXY(i, item);
                i++;
            }
//            chart1.ChartAreas[0].AxisY.IsStartedFromZero = false;
            sr = chart1.Series[1];
            sr.Points.Clear();
            i = 1;
            foreach (double item in Items2)
            {
                sr.Points.AddXY(i, item);
                i++;
            }
            chart1.ChartAreas[0].AxisY.IsStartedFromZero = true;

        }


        public void FillClipboard()
        {
            StringBuilder sb = new StringBuilder();

            int count = Items1.Count > Items2.Count ? Items1.Count : Items2.Count;
            for (int i = 0; i < count; i++)
            {
                double a = 0;
                double b = 0;
                if (i<Items1.Count)
                {
                    a = Items1[i];
                }
                if (i < Items2.Count)
                {
                    b = Items2[i];
                }

                sb.AppendLine($"{i}\t{a}\t{b}");
            }

            Clipboard.Clear();
            Clipboard.SetText(sb.ToString());
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Generate();
            FillGrid();
            FillClipboard();
            FillChart();
        }
    }
}
