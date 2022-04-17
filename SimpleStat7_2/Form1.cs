//--------------------------
using SimpleStat7_2;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using SimpleStat1;


namespace SimpleStat7_2
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

        public List<List<double>> YItems = new List<List<double>>();

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

            TNormDistr eps = new TNormDistr();
            eps.Reset(h+1);
            eps.mean = 0.0;
            eps.sd = normDistr.sd / 3.0;
            eps.Volume = normDistr.Volume;
            
            YItems.Clear();
            
            //YItems.Add(new List<double>());

            // Linear with small noise
            YItems.Add(normDistr.GenerateSample());
            var epsList = eps.GenerateSample();

            for (int i = 0; i < YItems[0].Count; i++)
            {
                YItems[0][i] = Items[i] * 2.0 + YItems[0][i] + epsList[i];
            }

            // Linear with medium noise
            YItems.Add(normDistr.GenerateSample());
            eps.sd = eps.sd * 3.0;
            epsList = eps.GenerateSample();

            for (int i = 0; i < YItems[1].Count; i++)
            {
                YItems[1][i] = Items[i] * (-2.0) + YItems[1][i]*(-2.5) + epsList[i];
            }

            // Linear with big noise
            YItems.Add(normDistr.GenerateSample());
            eps.sd = eps.sd * 6.0;
            epsList = eps.GenerateSample();

            for (int i = 0; i < YItems[2].Count; i++)
            {
                YItems[2][i] = Items[i] * 2.0 + YItems[2][i] * 3.5 + epsList[i];
            }


        }

        public void FillGrid()
        {
            dataGridView1.Rows.Clear();
            int i = 1;
            foreach (double item in Items)
            {
                var index = dataGridView1.Rows.Add();
                dataGridView1.Rows[index].Cells[0].Value = i;
                dataGridView1.Rows[index].Cells[1].Value = Math.Round(item,2);

                dataGridView1.Rows[index].Cells[2].Value = Math.Round(YItems[0][i - 1],2);
                dataGridView1.Rows[index].Cells[3].Value = Math.Round(YItems[1][i - 1],2);
                dataGridView1.Rows[index].Cells[4].Value = Math.Round(YItems[2][i - 1],2);

                i++;
            }
        }

        public void FillChart()
        {
            int j = (int) numericUpDown1.Value;
            if (j < 1) j = 1;
            if (j > 3) j = 3;
            var sr = chart1.Series[0];
            sr.Points.Clear();
            int i = 1;
            foreach (double item in Items)
            {
                sr.Points.AddXY(item, YItems[j-1][i-1]);
                i++;
            }
            chart1.ChartAreas[0].AxisY.IsStartedFromZero = false;
            chart1.Titles[0].Text = $"X vs Y{j}";
        }


        public void FillClipboard()
        {
            StringBuilder sb = new StringBuilder();
            int i = 1;
            foreach (double item in Items)
            {
                sb.AppendLine($"{i}\t{Math.Round(item,2)}\t{Math.Round(YItems[0][i-1],2)}\t{Math.Round(YItems[1][i - 1], 2)}\t{Math.Round(YItems[2][i - 1], 2)}");
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

        private void button2_Click(object sender, EventArgs e)
        {
            if(Items.Count<1) return;

            FillChart();
        }
    }
}
