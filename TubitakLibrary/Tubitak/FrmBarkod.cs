using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.OleDb;

namespace Tubitak
{
    public partial class FrmBarkod : Form
    {
        public FrmBarkod()
        {
            InitializeComponent();
        }
        public int deger;
        private void button2_Click(object sender, EventArgs e)
        {
            VeriGetir();
            yazici Yz = new yazici();

            if (textBox1.Text != "")
            {
                Yz.miktar = Convert.ToInt32(textBox1.Text);
                if (Yz.miktar <= 15) { 
                    Yz.yazdir();
                    VeriGetir();
                    label3.Text = barkodbaslik+globalVeriAktarimi.SonBarkod.ToString();
                    MessageBox.Show("İstenilen miktarda barkod üretilip veri tabanına kaydedildi.");
                    textBox1.Clear();
                }
                else
                    MessageBox.Show("Maksimum 15 barkod üretilebilir");
            }
            else
            {
                MessageBox.Show("Üretilecek Barkod Miktarı Giriniz.");
            }
        }
        string barkodbaslik = "AOS2200";
        private void FrmBarkod_Load(object sender, EventArgs e)
        {

            VeriGetir();
            textBox1.Focus();
            label3.Text = barkodbaslik+globalVeriAktarimi.SonBarkod.ToString();
        }
        public void VeriGetir()
        {
            OleDbConnection baglanti = new OleDbConnection(globalVeriAktarimi.BaglantiYolu);
            string sorgu = "Select BarkodSayi from tblBarkod order by tblBarkod.BarkodSayi desc";
            OleDbCommand komut = new OleDbCommand(sorgu, baglanti);
            baglanti.Open();
            globalVeriAktarimi.SonBarkod = Convert.ToInt32(komut.ExecuteScalar());
            baglanti.Close();
        }
    }
}

