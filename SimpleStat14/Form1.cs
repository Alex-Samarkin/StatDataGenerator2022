//--------------------------
using SimpleStat14;
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

namespace SimpleStat14
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

        private TNormDistr ProlaktMaleA1XRayYes = new TNormDistr();
        private TNormDistr ProlaktMaleA2XRayYes = new TNormDistr();
        private TNormDistr ProlaktMaleA3XRayYes = new TNormDistr();

        private TNormDistr ProlaktMaleA1XRayNo = new TNormDistr();
        private TNormDistr ProlaktMaleA2XRayNo = new TNormDistr();
        private TNormDistr ProlaktMaleA3XRayNo = new TNormDistr();

        private TNormDistr ProlaktFeMaleA1XRayYes = new TNormDistr();
        private TNormDistr ProlaktFeMaleA2XRayYes = new TNormDistr();
        private TNormDistr ProlaktFeMaleA3XRayYes = new TNormDistr();

        private TNormDistr ProlaktFeMaleA1XRayNo = new TNormDistr();
        private TNormDistr ProlaktFeMaleA2XRayNo = new TNormDistr();
        private TNormDistr ProlaktFeMaleA3XRayNo = new TNormDistr();


        // пролактин
        public List<double> Items = new List<double>();
        public List<string> Gender = new List<string>();
        public List<string> Age = new List<string>();
        public List<string> XRay = new List<string>();

        public void Generate()
        {
            // male 73 - 407
            // female 109-557
            int h = user.GetHashCode();
            normDistr.Seed = user.Hash;
            normDistr.Reset();
            normDistr.MinInt = 200;
            normDistr.MaxInt = 800;
            int N = normDistr.NextInt;

            diapason.Nomimal = Math.Round((double)normDistr.NextInt / 10, 1);
            diapason.TMinus = Math.Round(-(double)normDistr.NextInt / 2000, 3);
            diapason.TPlus = Math.Round((double)normDistr.NextInt / 1800, 3);

            normDistr.mean = diapason.MidValue;
            normDistr.sd = Math.Round(diapason.Range / 5.0, 3);
            normDistr.Volume = N;

            // Заполняем пол
            TGenderStringList gender = new TGenderStringList();
            gender.Seed = h;
            gender.Reset();
            Gender = gender.GenerateSample(N);
            // Заполняем возраст
            TFromStringList ages = new TFromStringList();
            ages.ItemList = new List<string>() { "18-30_лет", "30-50_лет", "50_и_более" };
            ages.Seed = h;
            ages.Reset();
            Age = ages.GenerateSample(N);
            // Лучевая терапия
            TYesNoStringList xray = new TYesNoStringList();
            xray.Seed = h+1;
            xray.Reset();
            XRay = xray.GenerateSample(N);

            // Задаем параметры генераторов

            ProlaktMaleA1XRayYes.Reset(h);
            ProlaktMaleA2XRayYes.Reset(h);
            ProlaktMaleA3XRayYes.Reset(h);

            ProlaktMaleA1XRayNo.Reset(h);
            ProlaktMaleA2XRayNo.Reset(h);
            ProlaktMaleA3XRayNo.Reset(h);

            ProlaktFeMaleA1XRayYes.Reset(h);
            ProlaktFeMaleA2XRayYes.Reset(h);
            ProlaktFeMaleA3XRayYes.Reset(h);

            ProlaktFeMaleA1XRayNo.Reset(h);
            ProlaktFeMaleA2XRayNo.Reset(h);
            ProlaktFeMaleA3XRayNo.Reset(h);

            //
            TNormDistr nd = new TNormDistr();
            nd.Seed = user.Hash;
            nd.Reset();
            nd.mean = 220;
            nd.sd = 32;
            ProlaktMaleA1XRayYes.mean = nd.NextNormal;
            nd.mean = 260;
            ProlaktMaleA2XRayYes.mean = nd.NextNormal;
            nd.mean = 240;
            ProlaktMaleA3XRayYes.mean = nd.NextNormal;

            nd.mean = 240;
            ProlaktMaleA1XRayNo.mean = nd.NextNormal;
            nd.mean = 280;
            ProlaktMaleA2XRayNo.mean = nd.NextNormal;
            nd.mean = 250;
            ProlaktMaleA3XRayNo.mean = nd.NextNormal;

            nd.sd = 48;
            nd.mean = 240;
            ProlaktFeMaleA1XRayYes.mean = nd.NextNormal;
            nd.mean = 300;
            ProlaktFeMaleA2XRayYes.mean = nd.NextNormal;
            nd.mean = 260;
            ProlaktFeMaleA3XRayYes.mean = nd.NextNormal;

            nd.mean = 290;
            ProlaktFeMaleA1XRayNo.mean = nd.NextNormal;
            nd.mean = 320;
            ProlaktFeMaleA2XRayNo.mean = nd.NextNormal;
            nd.mean = 270;
            ProlaktFeMaleA3XRayNo.mean = nd.NextNormal;

            nd.MinInt = 8;
            nd.MaxInt = 15;
            ProlaktMaleA1XRayYes.sd = nd.NextInt;
            ProlaktMaleA2XRayYes.sd = nd.NextInt;
            ProlaktMaleA3XRayYes.sd = nd.NextInt;

            ProlaktMaleA1XRayNo.sd = nd.NextInt;
            ProlaktMaleA2XRayNo.sd = nd.NextInt;
            ProlaktMaleA3XRayNo.sd = nd.NextInt;

            ProlaktFeMaleA1XRayYes.sd = nd.NextInt;
            ProlaktFeMaleA2XRayYes.sd = nd.NextInt;
            ProlaktFeMaleA3XRayYes.sd = nd.NextInt;

            ProlaktFeMaleA1XRayNo.sd = nd.NextInt;
            ProlaktFeMaleA2XRayNo.sd = nd.NextInt;
            ProlaktFeMaleA3XRayNo.sd = nd.NextInt;


            Items.Clear();

            for (int i = 0; i < N; i++)
            {
                
                // f
                if (Gender[i] == gender.ItemList[0])
                {
                    // age 1
                    if (Age[i] == ages.ItemList[0])
                    {
                        // no xray
                        if (XRay[i] == xray.ItemList[0])
                        {
                            Items.Add(ProlaktFeMaleA1XRayNo.NextNormal);
                        }
                        else
                        {
                            Items.Add(ProlaktFeMaleA1XRayYes.NextNormal);
                        }
                    }
                    // age 2
                    if (Age[i] == ages.ItemList[1])
                    {
                        // no xray
                        if (XRay[i] == xray.ItemList[0])
                        {
                            Items.Add(ProlaktFeMaleA2XRayNo.NextNormal);
                        }
                        else
                        {
                            Items.Add(ProlaktFeMaleA2XRayYes.NextNormal);
                        }
                    }
                    // age 3
                    if (Age[i] == ages.ItemList[2])
                    {
                        // no xray
                        if (XRay[i] == xray.ItemList[0])
                        {
                            Items.Add(ProlaktFeMaleA3XRayNo.NextNormal);
                        }
                        else
                        {
                            Items.Add(ProlaktFeMaleA3XRayYes.NextNormal);
                        }
                    }

                }

                // m
                if (Gender[i] == gender.ItemList[1])
                {
                    // age 1
                    if (Age[i] == ages.ItemList[0])
                    {
                        // no xray
                        if (XRay[i] == xray.ItemList[0])
                        {
                            Items.Add(ProlaktMaleA1XRayNo.NextNormal);
                        }
                        else
                        {
                            Items.Add(ProlaktMaleA1XRayYes.NextNormal);
                        }
                    }
                    // age 2
                    if (Age[i] == ages.ItemList[1])
                    {
                        // no xray
                        if (XRay[i] == xray.ItemList[0])
                        {
                            Items.Add(ProlaktMaleA2XRayNo.NextNormal);
                        }
                        else
                        {
                            Items.Add(ProlaktMaleA2XRayYes.NextNormal);
                        }
                    }
                    // age 3
                    if (Age[i] == ages.ItemList[2])
                    {
                        // no xray
                        if (XRay[i] == xray.ItemList[0])
                        {
                            Items.Add(ProlaktMaleA3XRayNo.NextNormal);
                        }
                        else
                        {
                            Items.Add(ProlaktMaleA3XRayYes.NextNormal);
                        }
                    }

                }

                Console.WriteLine($"i= {i} item {Items[i]}");
            }

            for (int i = 0; i < N; i++)
            {
                Items[i] = Math.Round(Items[i], 0);
            }
            // Items = normDistr.GenerateSample();


        }

        public void FillGrid()
        {
            dataGridView1.Rows.Clear();
            int i = 0;
            foreach (double item in Items)
            {
                var index = dataGridView1.Rows.Add();
                dataGridView1.Rows[index].Cells[0].Value = i;
                dataGridView1.Rows[index].Cells[1].Value = Gender[i];
                dataGridView1.Rows[index].Cells[2].Value = Age[i];
                dataGridView1.Rows[index].Cells[3].Value = XRay[i];
                dataGridView1.Rows[index].Cells[4].Value = item;

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

            var sr1 = chart2.Series[0];
            sr1.LegendText = "Male";
            var sr2 = chart2.Series[1];
            sr2.LegendText = "Female";

            sr1.Points.Clear();
            sr2.Points.Clear();
            int j = 0;
            int k = 0;
            TGenderStringList gender = new TGenderStringList();

            i = 0;
            foreach (double item in Items)
            {
                if (Gender[i] == gender.ItemList[0])
                {
                    sr1.Points.AddXY(j, item);
                    j++;
                }
                if (Gender[i] == gender.ItemList[1])
                {
                    sr2.Points.AddXY(k, item);
                    k++;
                }

                i++;
            }

            var sr3 = chart3.Series[0];
            sr3.LegendText = "18-30";
            var sr4 = chart3.Series[1];
            sr4.LegendText = "30-50";
            var sr5 = chart3.Series[2];
            sr5.LegendText = ">50";
            
            sr3.Points.Clear();
            sr4.Points.Clear();
            sr5.Points.Clear();

            j = 0;
            k = 0;
            int l = 0;

            TFromStringList ages = new TFromStringList();
            ages.ItemList = new List<string>() { "18-30_лет", "30-50_лет", "50_и_более" };

            i = 0;
            foreach (double item in Items)
            {
                if (Age[i] == ages.ItemList[0])
                {
                    sr3.Points.AddXY(j, item);
                    j++;
                }
                if (Age[i] == ages.ItemList[1])
                {
                    sr4.Points.AddXY(j, item);
                    k++;
                }
                if (Age[i] == ages.ItemList[2])
                {
                    sr5.Points.AddXY(j, item);
                    l++;
                }

                i++;
            }

            var sr6 = chart4.Series[0];
            sr6.LegendText = "XRay+";
            var sr7 = chart4.Series[1];
            sr7.LegendText = "XRay-";


            sr6.Points.Clear();
            sr7.Points.Clear();

            j = 0;
            k = 0;

            TYesNoStringList xray = new TYesNoStringList();
            i = 0;
            foreach (double item in Items)
            {
                if (XRay[i] == xray.ItemList[0])
                {
                    sr6.Points.AddXY(j, item);
                    j++;
                }
                if (XRay[i] == xray.ItemList[1])
                {
                    sr7.Points.AddXY(k, item);
                    k++;
                }

                i++;
            }

        }


        public void FillClipboard()
        {
            StringBuilder sb = new StringBuilder();
            int i = 0;
            foreach (double item in Items)
            {
                sb.AppendLine($"{i+1}\t{Gender[i]}\t{Age[i]}\t{XRay[i]}\t{item}");
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
