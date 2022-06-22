using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using System.Data.OleDb;


namespace Tubitak
{
    public partial class FrmGiris : Form
    {
        #region FormKenarlari
        [DllImport("Gdi32.dll", EntryPoint = "CreateRoundRectRgn")]

        private static extern IntPtr CreateRoundRectRgn
         (
              int nLeftRect,
              int nTopRect,
              int nRightRect,
              int nBottomRect,
              int nWidthEllipse,
              int nHeightEllipse

          );
        #endregion
        public FrmGiris()
        {
            InitializeComponent();
            Region = System.Drawing.Region.FromHrgn(CreateRoundRectRgn(0, 0, Width, Height, 25, 25));

        }
        OleDbConnection con = new OleDbConnection("Provider=Microsoft.ACE.OLEDB.12.0;Data Source=C:\\Users\\PC\\source\\repos\\Tubitak\\Tubitak\\bin\\Debug\\Kitap.accdb");
        //OleDbConnection con = new OleDbConnection(@"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=Kitap.accdb");
        //Provider=Microsoft.ACE.OLEDB.12.0;Data Source=C:\Users\PC\source\repos\Tubitak\Kitap.accdb

        databaseClass SorguClass = new databaseClass();
        private void btnKapat_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void FrmGiris_Load(object sender, EventArgs e)
        {

        }


        int hak = 3;
        public bool durum;

        private void btnGiris_Click_1(object sender, EventArgs e)
        {
            if (hak != 0)
            {
                con.Open();
                OleDbCommand cmd = new OleDbCommand("SELECT * FROM tblKullanicilar",con);
                OleDbDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    if (radioButton2.Checked)
                    {
                        if (dr["KullaniciAdi"].ToString() == textBox1.Text && dr["KullaniciSifre"].ToString() == textBox2.Text && dr["Yetki"].ToString() == "Yönetici")
                        {                       
                            durum = true;
                            globalVeriAktarimi.KullaniciAd = dr.GetValue(2).ToString();
                            globalVeriAktarimi.Yetki = dr.GetValue(4).ToString();
                            this.Hide();
                            Form1 frm1 = new Form1();
                            frm1.Show();
                            break;
                        
                        }
                    }
                    if (radioButton1.Checked)
                    {
                        if (dr["KullaniciAdi"].ToString() == textBox1.Text && dr["KullaniciSifre"].ToString() == textBox2.Text && dr["Yetki"].ToString() == "Görevli")
                        {
                            durum = true;
                            globalVeriAktarimi.KullaniciAd = dr.GetValue(2).ToString();
                            globalVeriAktarimi.Yetki = dr.GetValue(4).ToString();
                            this.Hide();
                            Form1 frm1 = new Form1();
                            frm1.Show();
                            break;
                        }

                    }
                }

                if (durum == false)
                {
                    hak--;
                    con.Close();
                }
                label7.Text = hak.ToString();

                if (hak == 0)
                {
                    btnGiris.Enabled = false;
                    MessageBox.Show("Giriş Hakkı Kalmadı!", "AOS Kütüphane", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    this.Close();
                }

            }
        }

        private void FrmGiris_Activated(object sender, EventArgs e)
        {
            textBox1.Focus();
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked)
            {
                textBox2.PasswordChar.ToString();
            }
            else
            {
                textBox2.PasswordChar = '*';
            }
        }

        private void checkBox1_CheckedChanged_1(object sender, EventArgs e)
        {
            if (checkBox1.Checked)
            {
                textBox2.PasswordChar = '\0';
            }
            else
            {
                textBox2.PasswordChar = '*';
            }
        }
    }
}
