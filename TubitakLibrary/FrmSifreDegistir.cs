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
    public partial class FrmSifreDegistir : Form
    {
        public FrmSifreDegistir()
        {
            InitializeComponent();
        }
        databaseClass dbClass = new databaseClass();
        OleDbConnection con = new OleDbConnection(@"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=Kitap.accdb");

        private void btnGuncelle_Click(object sender, EventArgs e)
        {
            con.Open();
            OleDbCommand komut = new OleDbCommand("Select * From tblKullanicilar where KullaniciAdi='" + textBox1.Text.ToString() + "'", con);
            OleDbDataReader dr = komut.ExecuteReader();

            while (dr.Read())
            {
                if (textBox1.Text.ToString() == dr["KullaniciAdi"].ToString() && textBox2.Text.ToString() == dr["KullaniciSifre"].ToString() && textBox3.Text == textBox4.Text)
                {
                    string sorgu = "UPDATE tblKullanicilar SET KullaniciSifre='" + textBox4.Text + "' WHERE KullaniciAdi='" + textBox1.Text + "'";
                    dbClass.kitapSorgu(sorgu);
                    MessageBox.Show("Kullanıcı bilgileri güncellendi.");
                    textBox1.Clear();textBox2.Clear();textBox3.Clear();textBox4.Clear();
                }
                else
                    MessageBox.Show("Şifreyi kontrol ediniz.");
            }
            con.Close();

        }



    }
}
