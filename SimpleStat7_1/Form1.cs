//--------------------------
using SimpleStat7_1;
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

namespace SimpleStat7_1
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

        public List<double> Items = new List<double>();

        public TEllipsoid Ellipsoid { get; set; } = new TEllipsoid();

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

            Ellipsoid.Seed = h;
            Ellipsoid.Setup();
        }

        public void FillGrid()
        {
            dataGridView1.Rows.Clear();

            for (int i = 0; i < Ellipsoid.Items[0].Count; i++)
            {
                var index = dataGridView1.Rows.Add();
                dataGridView1.Rows[index].Cells[0].Value = i+1;

                for (int j = 0; j < Ellipsoid.DimCount; j++)
                {
                    dataGridView1.Rows[index].Cells[j+1].Value = Math.Round(Ellipsoid.Items[j][i], 3);
                    dataGridView1.Rows[index].Cells[j + 1+Ellipsoid.DimCount].Value = 
                        Math.Round(Ellipsoid.RotatedItems[j][i], 3);
                } 

            }

        }

        public void FillChart()
        {
            var sr = chart1.Series[0];
            sr.Points.Clear();
            int i = 1;
            foreach (double item in Items)
            {
                sr.Points.AddXY(i, item);
                i++;
            }
            chart1.ChartAreas[0].AxisY.IsStartedFromZero = false;
        }

        public void FillChart(List<double> x,List<double> y)
        {
            var sr = chart1.Series[0];
            sr.Points.Clear();

            for (int i = 0; i < x.Count; i++)
            {
                sr.Points.AddXY(x[i], y[i]);
            }

            chart1.ChartAreas[0].AxisY.IsStartedFromZero = false;
            chart1.ChartAreas[0].AxisX.IsStartedFromZero = false;
        }


        public void FillClipboard()
        {
            StringBuilder sb = new StringBuilder();
            sb.Clear();

            for (int i = 0; i < Ellipsoid.Items[0].Count; i++)
            {
                var s = $"{i}";
                for (int j = 0; j < Ellipsoid.DimCount; j++)
                {
                   s+=$"\t{Math.Round(Ellipsoid.Items[j][i], 3)}";
                }
                for (int j = 0; j < Ellipsoid.DimCount; j++)
                {
                    s += $"\t{Math.Round(Ellipsoid.RotatedItems[j][i], 3)}";
                }

                sb.AppendLine(s);
            }

            Clipboard.Clear();
            Clipboard.SetText(sb.ToString());
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Generate();
            FillGrid();
            FillClipboard();
            FillChart(Ellipsoid.Items[0],Ellipsoid.Items[1]);

            numericUpDown1.Maximum = Ellipsoid.DimCount * 2 - 1;
            numericUpDown2.Maximum = Ellipsoid.DimCount * 2 - 1;

            numericUpDown1.Value = 0;
            numericUpDown2.Value = 1;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            List<double> x;
            List<double> y;
            if (numericUpDown1.Value<Ellipsoid.DimCount)
            {
                x = Ellipsoid.Items[(int)numericUpDown1.Value];
            }
            else
            {
                x = Ellipsoid.RotatedItems[(int)numericUpDown1.Value - Ellipsoid.DimCount];
            }
            if (numericUpDown2.Value < Ellipsoid.DimCount)
            {
                y = Ellipsoid.Items[(int)numericUpDown2.Value];
            }
            else
            {
                y = Ellipsoid.RotatedItems[(int)numericUpDown2.Value - Ellipsoid.DimCount];
            }
            FillChart(x,y);
        }
    }
}
