using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Bilgisayar_Grafikleri
{
    public partial class Form1 : Form
    {

        public Form1()
        {
            InitializeComponent();
        }

        double[,] XCisimNoktaBezierYuzeyKontrolN = new double[20, 4] {  { 0.3, 0.1, 1.5, 1 },
                                                                        { 0.2, 1.0, 1.0, 1 },
                                                                        { 0.2, 0.7, 0.8, 1 },
                                                                        { 0.3, 1.0, 0.2, 1 },

                                                                        { 0.5, 0.8, 1.4, 1 },
                                                                        { 0.6, 0.6, 1.1, 1 },
                                                                        { 0.5, 1.2, 0.8, 1 },
                                                                        { 0.5, 1.0, 0.1, 1 },

                                                                        { 1.2, 1.8, 1.6, 1 },
                                                                        { 1.1, 0.6, 1.0, 1 },
                                                                        { 1.2, 1.2, 0.7, 1 },
                                                                        { 1.3, 1.0, 0.3, 1 },

                                                                        { 1.5, 1.8, 1.5, 1 },
                                                                        { 1.4, 0.6, 1.0, 1 },
                                                                        { 1.1, 1.2, 0.7, 1 },
                                                                        { 0.3, 1.0, 0.1, 1 },

                                                                        { 1.8, 0.4, 1.5, 1 },
                                                                        { 1.8, 1.6, 1.1, 1 },
                                                                        { 1.8, 0.7, 0.8, 1 },
                                                                        { 0.3, 1.0, 0.2, 1 }};

        double[,] XCisimBezierİzometrik = new double[20, 4];

        double[,] Tizometrik = new double[4, 4] { {0.707   , -0.408 , 0, 0},
                                                  {0       , 0.816  , 0, 0},
                                                  {-0.707  , -0.408 , 0, 0},
                                                  {0       ,  0     , 0, 1},
         };

        double[,] BezierNoktaBulutuizometrik = new double[25, 4];
        double[,] BezierYuzeyNoktaBulutu = new double[25, 4];

        private double BezierHesapla(double uu, double ww,int xyz)
        {
            double sonuc = 0, T = 0, K = 0;
            int n = 4, m = 3;
            for (int i = 0; i < 5; i++)
            {
                for(int j=0;j<4;j++)
                {
                    T = (Faktor(n) / (Faktor(i) * Faktor((n - i)))) * Math.Pow(uu, i) * Math.Pow((1 - uu), (n - i));
                    K = (Faktor(m) / (Faktor(j) * Faktor((m - j)))) * Math.Pow(ww, j) * Math.Pow((1 - ww), (m - j));
                    sonuc += XCisimNoktaBezierYuzeyKontrolN[i * 3 + j, xyz] * T * K;
                }
            }
            return sonuc;
        }

        private double Faktor(double sayi)
        {
            double sonuc = 1;
            for (int i = 1; i <= sayi; i++) sonuc *= (double)i;
            return sonuc;
        }
        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            listBox1.Items.Add("#\t" + "x\t" + "y\t" + "z\t"); 
            for (int i = 0; i < 20; i++) {
                if (i % 4 == 0) listBox1.Items.Add("");
                       listBox1.Items.Add(i + "\t" + XCisimNoktaBezierYuzeyKontrolN[i, 0] + "\t" + XCisimNoktaBezierYuzeyKontrolN[i, 1] + "\t" + XCisimNoktaBezierYuzeyKontrolN[i, 2]);
                    
        }
    }

        private void button2_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < 20; i++) { 
                for (int j = 0; j < 4; j++) { 
                    for (int k = 0; k < 4; k++) {
                        XCisimBezierİzometrik[i, j] += XCisimNoktaBezierYuzeyKontrolN[i,k] * Tizometrik[k, j];


                    }
                    XCisimBezierİzometrik[i, j] = Math.Round(XCisimBezierİzometrik[i, j], 4);
                }

}
            listBox2.Items.Add("#\t" + "x\t" + "y\t" + "z\t");
            for (int i = 0; i < 20; i++)
            {
                if (i % 3 == 0) listBox2.Items.Add("");
                listBox2.Items.Add(i + "\t" + XCisimBezierİzometrik[i, 0] + "\t" + XCisimBezierİzometrik[i, 1] + "\t" + XCisimBezierİzometrik[i, 2]);

            }
        }

        private void button3_Click(object sender, EventArgs e)
        {

            double hassasiyet = 0.25, u = 0, w;
            listBox3.Items.Add("#\t" + "u\t" + "w\t" + "x\t" + "y\t" + "z\t"); 
            for (int i = 0; i < 5; i++) 
            { 
            w = 0; 
                for (int j =0; j < 5; j++)
                { 
                    BezierYuzeyNoktaBulutu[i * 5 + j, 0] = Math.Round(BezierHesapla(u, w, 0), 4);
                    BezierYuzeyNoktaBulutu[i * 5 + j, 1] = Math.Round(BezierHesapla(u, w, 1), 4);
                    BezierYuzeyNoktaBulutu[i * 5 + j, 2] = Math.Round(BezierHesapla(u, w, 2), 4);
                    if ((i * 5 + j) % 5== 0) listBox3.Items.Add(""); 
                    listBox3.Items.Add((i * 5 + j) + "\t" + u + "\t" + w + "\t" +  BezierYuzeyNoktaBulutu[i* 5 + j, 0] + "\t" + BezierYuzeyNoktaBulutu[i * 5 + j, 1] + "\t" + BezierYuzeyNoktaBulutu[i * 5 + j, 2]);
                    w += hassasiyet;
                }
                u += hassasiyet;
            }

            for (int i = 0; i < 25; i++) 
            { 
                for (int j = 0; j < 4; j++) 
                { 
                    for (int k = 0; k < 4; k++) 
                    { 
                        BezierNoktaBulutuizometrik[i, j] += BezierYuzeyNoktaBulutu[i, k] *Tizometrik[k, j];
                    }
                    BezierNoktaBulutuizometrik[i, j] = Math.Round(BezierNoktaBulutuizometrik[i, j], 4);
                }
            }
            listBox4.Items.Add("#\t" + "x\t" + "y\t" + "z\t");
            for (int i = 0; i < 25; i++) 
            { 
                if (i % 5 == 0) listBox4.Items.Add(""); 
                listBox4.Items.Add(i + "\t" + BezierNoktaBulutuizometrik[i, 0] + "\t" + BezierNoktaBulutuizometrik[i, 1] +"\t" + BezierNoktaBulutuizometrik[i, 2]);
            }
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label5_Click(object sender, EventArgs e)
        {

        }
    }
}
