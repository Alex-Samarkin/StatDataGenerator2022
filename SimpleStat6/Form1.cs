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

        public List<double> Items = new List<double>();

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

        private void button1_Click(object sender, EventArgs e)
        {
            Generate();
            FillGrid();
            FillClipboard();
            FillChart();
        }
    }
}
