//--------------------------
using SimpleStat5;
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

namespace SimpleStat5
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
        private TCauchyDistr cauchyDistr = new TCauchyDistr();
        private TLogNormalDistr logNormalDistr = new TLogNormalDistr();
        private TStudentDistr studentDistr = new TStudentDistr();

        private TDiapason diapason = new TDiapason();

        public List<double> Items = new List<double>();
        public List<double> cItems = new List<double>();
        public List<double> lItems = new List<double>();
        public List<double> sItems = new List<double>();


        public void Generate()
        {
            int h = user.GetHashCode();
            normDistr.Seed = user.Hash;
            normDistr.Reset();
            normDistr.MinInt = 300;
            normDistr.MaxInt = 450;
            int N = normDistr.NextInt;

            diapason.Nomimal = Math.Round((double)normDistr.NextInt / 10, 1);
            diapason.TMinus = Math.Round(-(double)normDistr.NextInt / 2000, 3);
            diapason.TPlus = Math.Round((double)normDistr.NextInt / 1800, 3);

            normDistr.mean = diapason.MidValue;
            normDistr.sd = normDistr.NextInt/30;
            normDistr.Volume = N;

            cauchyDistr.location = diapason.MidValue;
            cauchyDistr.scale = normDistr.sd/64;

            logNormalDistr.mu = 2;
            logNormalDistr.sigma = normDistr.sd/10;

            studentDistr.location = normDistr.mean;
            studentDistr.scale = normDistr.sd;
            studentDistr.freedom = 24;

            Items = normDistr.GenerateSample();
            cItems = cauchyDistr.GenerateSample();
            lItems = logNormalDistr.GenerateSample();
            sItems = studentDistr.GenerateSample();
        }

        public void FillGrid()
        {
            dataGridView1.Rows.Clear();
            /* int i = 0;
            foreach (double item in Items)
            {
                var index = dataGridView1.Rows.Add();
                dataGridView1.Rows[index].Cells[0].Value = i;
                dataGridView1.Rows[index].Cells[1].Value = item;

                i++;
            }*/
            for (int i = 0; i < Items.Count; i++)
            {
                var index = dataGridView1.Rows.Add();
                dataGridView1.Rows[index].Cells[0].Value = i+1;
                dataGridView1.Rows[index].Cells[1].Value = Math.Round(Items[i], 3);
                dataGridView1.Rows[index].Cells[2].Value = Math.Round(cItems[i], 3);
                dataGridView1.Rows[index].Cells[3].Value = Math.Round(lItems[i], 3);
                dataGridView1.Rows[index].Cells[4].Value = Math.Round(sItems[i], 3);
            }
        }

        public void FillChart()
        {
            var sr = chart1.Series[0];
            sr.Points.Clear();
            var sr1 = chart1.Series[1];
            sr1.Points.Clear();
            var sr2 = chart1.Series[2];
            sr.Points.Clear();
            var sr3 = chart1.Series[3];
            sr3.Points.Clear();

            for (int i = 0; i < Items.Count; i++)
            {
                sr.Points.AddXY(i, Items[i]);
                sr1.Points.AddXY(i, cItems[i]);
                sr2.Points.AddXY(i, lItems[i]);
                sr3.Points.AddXY(i, sItems[i]);
            }

            chart1.ChartAreas[0].AxisY.IsStartedFromZero = false;
        }


        public void FillClipboard()
        {
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < Items.Count; i++)
            {
                sb.AppendLine($"{i}\t{Items[i]:F3}\t{cItems[i]:F3}\t{lItems[i]:F3}\t{sItems[i]:F3}");
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
