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
    public partial class KullaniciTanimla : Form
    {
        public KullaniciTanimla()
        {
            InitializeComponent();
        }
        databaseClass dbClass = new databaseClass();
        string yetki="";
        private void btnEkle_Click(object sender, EventArgs e)
        {
            string adsoyad = $"{textBox2.Text} {textBox3.Text}";
            if (textBox1.Text != "" && textBox2.Text != "" && textBox3.Text != "" && textBox4.Text != "" && textBox5.Text != "" && yetki != "" && textBox5.Text == textBox4.Text)
            {
                string sorgu = "INSERT INTO tblKullanicilar (KullaniciAdi,KullaniciAdSoyad,KullaniciSifre,Yetki) values ('" + textBox1.Text + "','" + adsoyad + "','" + textBox5.Text + "','" + yetki + "')";
                dbClass.kitapSorgu(sorgu);
                MessageBox.Show($"{yetki} eklendi.");
                textBox1.Clear();textBox2.Clear();textBox3.Clear();textBox4.Clear();textBox5.Clear();radioButton1.Checked = false;radioButton2.Checked = false;
            }
            else
                MessageBox.Show("Alanlar boş bırakılamaz.");
        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            yetki = "Yönetici";
        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            yetki = "Görevli";
        }
    }
}
