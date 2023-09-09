using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DigitsOfRoot
{
    public partial class Form1 : Form
    {
        const int NrOfDigits = 1000;
        public Form1()
        {
            InitializeComponent();
        }

        int[] sayı, sonuç;
        int rOffset = 0;
        private void Form1_Load(object sender, EventArgs e)
        {
            int DP = 0;
            sayı = new int[NrOfDigits];
            sonuç = new int[NrOfDigits];

            int a = 9;
            int idx = 0;
            while (a > 0)
            {
                sayı[idx++] = a % 10;
                a /= 10;
            }

            rOffset = (1 - Uzunluk(sayı)) / 2 * 2;//it represents which digit do we check
            int offset = 0; //it stores where is the index of digit 
            while (offset < NrOfDigits)
            {
                if (rOffset == 0)
                    DP = offset;
                Kaydır();

                for (int dg = 9; dg >= 0; dg--)
                {
                    sonuç[0] = dg;
                    if (İkincisiKüçük(sayı, Kare()))
                        break;
                }
                rOffset += 2;
                ++offset;
            }

            bool leadingzero = true;
            string res = "";
            for (int i = offset - 1; i > 0; i--)
            {
                if (leadingzero)
                {
                    if (sonuç[i] > 0)
                    {
                        leadingzero = false;
                        res += sonuç[i].ToString();
                    }
                }
                else
                {
                    if (DP == offset - i - 2)
                        res += ",";
                    res += sonuç[i].ToString();
                }
            }
            this.Text = res;
        }

        int[] Kare()
        {
            int sl = Uzunluk(sonuç);

            int[] r = new int[sl * 2 + 4];

            for (int i = 0; i < sl; i++)
                for (int j = 0; j < sl; j++)
                    r[i + j] += sonuç[i] * sonuç[j];

            for (int i = 0; i < r.Length - 1; i++)
                if (r[i] > 9)
                {
                    r[i + 1] += r[i] / 10;
                    r[i] = r[i] % 10;
                }
            return r;
        }

        bool İkincisiKüçük(int[] x1, int[] x2)
        {
            int s2 = Uzunluk(x2) - 1;

            for (int i = s2; i >= 0; i--)
                if (x2[i] > 0 || x1[i - rOffset] > 0)
                    return x2[i] < x1[i - rOffset];

            return default;
        }

        int Uzunluk(int[] x1)
        {
            for (int i = x1.Length - 1; i >= 0; i--)
                if (x1[i] > 0)
                    return i + 1;

            return default;
        }

        void Kaydır()
        {
            for (int i = sonuç.Length - 1; i > 0; i--)
                sonuç[i] = sonuç[i - 1];
        }

    }
}

