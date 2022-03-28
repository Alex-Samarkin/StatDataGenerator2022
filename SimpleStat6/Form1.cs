//--------------------------
using SimpleStat6;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.VisualStyles;
using MyStatLib;
using SimpleStat1;

namespace SimpleStat6
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
        private TDiapason diapason = new TDiapason();

        private TMultiDimDistr[] clusters = new TMultiDimDistr[]
            { new TMultiDimDistr(), new TMultiDimDistr(), new TMultiDimDistr() };
        private TMultiDimDistr cluster0 => clusters[0];
        private TMultiDimDistr cluster1 => clusters[1];
        private TMultiDimDistr cluster2 => clusters[2];


        public List<double> Items = new List<double>();
        public List<List<double>>[] xyz = new List<List<double>>[3];
        public List<XYZPoint> xyz1 = new List<XYZPoint>();

        public void StackXYZ()
        {
            xyz1.Clear();
            foreach (var list in xyz)
            {
                for (int i = 0; i < 3; i++)
                {
                    for (int j = 0; j < list[i].Count; j++)
                    {
                        double x = list[0][j];
                        double y = list[1][j];
                        double z = list[2][j];
                        xyz1.Add(new XYZPoint() { X = Math.Round(x,2), Y = Math.Round(y,2), Z = Math.Round(z,2) });
                    }
                }
            }
        }

        public void PermutateXYZ()
        {
            var r = new Random();
            var MaxIndex = xyz1.Count;
            for (int i = 0; i < xyz1.Count / 2; i++)
            {
                var index_from = r.Next(0, MaxIndex);
                var index_to = r.Next(0, MaxIndex);
                (xyz1[index_to], xyz1[index_from]) = (xyz1[index_from], xyz1[index_to]);
            }
        }

        public void Generate()
        {
            int h = user.GetHashCode();
            normDistr.Seed = user.Hash;
            normDistr.Reset();
            normDistr.MinInt = 400;
            normDistr.MaxInt = 600;
            int N = normDistr.NextInt;

            diapason.Nomimal = Math.Round((double)normDistr.NextInt / 10, 1);
            diapason.TMinus = Math.Round(-(double)normDistr.NextInt / 2000, 3);
            diapason.TPlus = Math.Round((double)normDistr.NextInt / 1800, 3);

            normDistr.mean = diapason.MidValue;
            normDistr.sd = Math.Round(diapason.Range / 5.0, 3);
            normDistr.Volume = N;

            Items = normDistr.GenerateSample();

            foreach (TMultiDimDistr cluster in clusters)
            {
                cluster.Reset(h,1);
                cluster.Volume = normDistr.NextInt;
            }

            normDistr.MinInt = -75;
            normDistr.MaxInt = 150;

            foreach (TMultiDimDistr cluster in clusters)
            {
                TNormDistr ndx = cluster.XGen as TNormDistr;
                ndx.mean = normDistr.NextInt;
                ndx.sd = Math.Abs(normDistr.NextInt) / 1.0;

                TNormDistr ndy = cluster.YGen as TNormDistr;
                ndy.mean = normDistr.NextInt;
                ndy.sd = Math.Abs(normDistr.NextInt) / 0.5;

                TNormDistr ndz = cluster.ZGen as TNormDistr;
                ndz.mean = normDistr.NextInt;
                ndz.sd = Math.Abs(normDistr.NextInt) / 1.0;
            }

            for (int i = 0; i < 3; i++)
            {
                xyz[i] = clusters[i].GetXYZSamples();
            }

            StackXYZ();
            PermutateXYZ();
        }

        public void FillGrid()
        {
            dataGridView1.Rows.Clear();
            int i = 0;
            foreach (var item in xyz1)
            {
                var index = dataGridView1.Rows.Add();
                dataGridView1.Rows[index].Cells[0].Value = i+1;
                dataGridView1.Rows[index].Cells[1].Value = item.X;
                dataGridView1.Rows[index].Cells[2].Value = item.Y;
                dataGridView1.Rows[index].Cells[3].Value = item.Z;

                i++;
            }
        }

        public void FillChart()
        {
            var sr = chart1.Series[0];
            var sr1 = chart1.Series[1];
            var sr2 = chart1.Series[2];
            sr.Points.Clear();
            sr1.Points.Clear();
            sr2.Points.Clear();
            int i = 1;
            foreach (var item in xyz1)
            {
                sr.Points.AddXY(item.X, item.Y);
                sr1.Points.AddXY(item.Y, item.Z);
                sr2.Points.AddXY(item.Z, item.X);
                i++;
            }
            chart1.ChartAreas[0].AxisY.IsStartedFromZero = false;
        }


        public void FillClipboard()
        {
            StringBuilder sb = new StringBuilder();
            int i = 0;
            foreach (var item in xyz1)
            {
                sb.AppendLine($"{i+1}\t{item.X}\t{item.Y}\t{item.Z}");
                i++;
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
