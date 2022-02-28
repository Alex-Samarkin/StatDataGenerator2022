//--------------------------
using SimpleStat2;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MyStatLib.Properties;
using SimpleStat1;

namespace SimpleStat2
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
        private TBiModalNormDistr biModalNormDistr = new TBiModalNormDistr();
        private TDiapason diapason = new TDiapason();

        public List<double> Items = new List<double>();

        public void Generate()
        {
            int h = user.GetHashCode();
            normDistr.Seed = user.Hash;
            normDistr.Reset();
            normDistr.MinInt =500;
            normDistr.MaxInt = 750;
            int N = normDistr.NextInt;

            biModalNormDistr.Seed = user.Hash;
            biModalNormDistr.Reset();
            /// 400 - 600
            biModalNormDistr.MinInt = 500;
            biModalNormDistr.MaxInt = 800;

             // biModalNormDistr.SecondNormDistr

            biModalNormDistr.SecondNormDistr.Seed = user.Hash;
            biModalNormDistr.SecondNormDistr.Reset();
            biModalNormDistr.SecondNormDistr.MinInt = 500;
            biModalNormDistr.SecondNormDistr.MaxInt = 800;

            diapason.Nomimal = Math.Round((double)normDistr.NextInt / 10, 1);
            diapason.TMinus = Math.Round(-(double)normDistr.NextInt / 20, 3);
            diapason.TPlus = Math.Round((double)normDistr.NextInt / 18, 3);

            biModalNormDistr.mean = diapason.MidValue;
            biModalNormDistr.sd = Math.Round(diapason.Range / 5.0, 3);
            biModalNormDistr.Volume = N;

            biModalNormDistr.SecondNormDistr.mean = diapason.MidValue+2*biModalNormDistr.sd;
            biModalNormDistr.SecondNormDistr.sd = Math.Round(diapason.Range / 6.0, 3);
            biModalNormDistr.SecondNormDistr.Volume = N*2/3;

            Items = biModalNormDistr.GenerateSample();
        }

        public void FillGrid()
        {
            dataGridView1.Rows.Clear();
            int i = 1;
            foreach (double item in Items)
            {
                var index = dataGridView1.Rows.Add();
                dataGridView1.Rows[index].Cells[0].Value = i;
                dataGridView1.Rows[index].Cells[1].Value = item;

                i++;
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


        public void FillClipboard()
        {
            StringBuilder sb = new StringBuilder();
            int i = 1;
            foreach (double item in Items)
            {
                sb.AppendLine($"{i}\t{item}");
                i++;
            }
            Clipboard.Clear();
            Clipboard.SetText(sb.ToString());
        }

        public void Permutate()
        {
            TRandGen r = new TRandGen() { MaxInt = Items.Count, MinInt = 1 };

            for (int i = 0; i < Items.Count; i++)
            {
                var i1 = r.NextInt;
                var i2 = r.NextInt;
                var d = Items[i1];
                Items[i1] = Items[i2];
                Items[i2] = d;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Generate();
            Permutate();
            FillGrid();
            FillClipboard();
            FillChart();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
