using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.OleDb;
using System.Windows.Forms;

namespace Tubitak
{
    public partial class FrmKitapDetaylar : Form
    {
        public FrmKitapDetaylar()
        {
            InitializeComponent();
        }
        databaseClass kitapClss = new databaseClass();

        private void YetkiKontrol()
        {
            /*if (globalVeriAktarimi.Yetki != "Yönetici")
            {
                btnSil.Enabled = false;
                btnEkle.Enabled = false;
                btnGuncelle.Enabled = false;
                btnTemizle.Enabled = false;
            }*/

        }
        databaseClass dbClass = new databaseClass();
        private void FrmKitapDetaylar_Load(object sender, EventArgs e)
        {
            textBox1.Text = globalVeriAktarimi.Numara.ToString();
            textBox2.Text = globalVeriAktarimi.KitapAd;
            textBox3.Text = globalVeriAktarimi.YazarAd;
            textBox4.Text = globalVeriAktarimi.Yayinevi;
            textBox5.Text = globalVeriAktarimi.Barkod;
            textBox6.Text = globalVeriAktarimi.Tur;
            textBox7.Text = globalVeriAktarimi.Sayfa;
            numericUpDown1.Value = globalVeriAktarimi.Adet;
            if (textBox1.Text == "0" && textBox2.Text == "" && textBox3.Text == "")
            {
                string sorgu = "SELECT YazarAd FROM tblYazar";
                dbClass.cmd = new OleDbCommand(sorgu,dbClass.con);
                dbClass.openConnection();
                OleDbDataReader dr = dbClass.cmd.ExecuteReader();
                AutoCompleteStringCollection autotext = new AutoCompleteStringCollection();
                while (dr.Read())
                {
                    autotext.Add(dr.GetString(0));
                }
                textBox3.AutoCompleteMode = AutoCompleteMode.Suggest;
                textBox3.AutoCompleteSource = AutoCompleteSource.CustomSource;
                textBox3.AutoCompleteCustomSource = autotext;

                string sorgu2 = "SELECT TurAd FROM tblTur";
                dbClass.cmd = new OleDbCommand(sorgu2, dbClass.con);
                
                OleDbDataReader dr2 = dbClass.cmd.ExecuteReader();
                AutoCompleteStringCollection autotext2 = new AutoCompleteStringCollection();
                while (dr2.Read())
                {
                    autotext2.Add(dr2.GetString(0));
                }
                textBox6.AutoCompleteMode = AutoCompleteMode.Suggest;
                textBox6.AutoCompleteSource = AutoCompleteSource.CustomSource;
                textBox6.AutoCompleteCustomSource = autotext2;
                dbClass.closeConnection();
            }
        }
        private void YazilariBuyut()
        {
            textBox2.Text = textBox2.Text.ToUpper();
            textBox3.Text = textBox3.Text.ToUpper();
            textBox4.Text = textBox4.Text.ToUpper();
            textBox6.Text = textBox6.Text.ToUpper();
        }
        private void TextboxTemizle()
        {
            textBox1.Clear();textBox2.Clear(); textBox3.Clear(); textBox4.Clear(); textBox5.Clear(); textBox6.Clear(); textBox7.Clear();numericUpDown1.Value = 0;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (globalVeriAktarimi.Yetki == "Yönetici")
            {
                TextboxTemizle();
            }
            else
            {
                dbClass.YetkiIzin();
            }
        }

        DialogResult dialog = new DialogResult();

        private void btnSil_Click(object sender, EventArgs e)
        {
            if (globalVeriAktarimi.Yetki == "Yönetici")
            {
                dialog = MessageBox.Show("Bu işleme devam etmek istiyor musunuz?", "UYARI", MessageBoxButtons.YesNo);
                if (dialog == DialogResult.Yes) { 
                    YazilariBuyut();
                    string sorgu = "DELETE FROM tblKitap WHERE KitapNo = " + textBox1.Text + "";
                    kitapClss.kitapSorgu(sorgu);
                    MessageBox.Show("Kitap kaydı silindi.");
                    this.Close();
                }
            }
            else
            {
                kitapClss.YetkiIzin();
            }
        }
        OleDbConnection baglanti = new OleDbConnection(globalVeriAktarimi.BaglantiYolu);
        
        private void btnGuncelle_Click(object sender, EventArgs e)
        {
            
            
            if (globalVeriAktarimi.Yetki == "Yönetici")
            {
                try
                {
                    if (baglanti.State == ConnectionState.Closed)
                    {
                        baglanti.Open();
                        dialog = MessageBox.Show("Bu işleme devam etmek istiyor musunuz?", "UYARI", MessageBoxButtons.YesNo);
                        if (dialog == DialogResult.Yes)
                        {
                            YazarNum = 0;
                            TurNum = 0;
                            YazarSorgu();
                            TurSorgu();
                            if (YazarNum == 0 && textBox3.Text != "")
                            {
                                string sorgu1 = "INSERT INTO tblYazar(YazarAd) VALUES ('" + textBox3.Text + "')";
                                kitapClss.kitapSorgu(sorgu1);
                                MessageBox.Show(YazarNum.ToString());
                            }
                            if (TurNum == 0 && textBox6.Text != "")
                            {
                                string sorgu1 = "INSERT INTO tblTur(TurAd) VALUES ('" + textBox6.Text + "')";
                                kitapClss.kitapSorgu(sorgu1);
                                //MessageBox.Show(TurNum.ToString());
                            }
                            YazarSorgu();
                            TurSorgu();

                            YazilariBuyut();
                            OleDbCommand cmd = new OleDbCommand();
                            cmd.Connection = baglanti;
                            cmd.CommandText = "UPDATE tblKitap SET KitapAd='" + textBox2.Text + "', YazarNo=" + YazarNum + ", Yayinevi='" + textBox4.Text + "', Barkod='" + textBox5.Text + "', TurNo=" + TurNum.ToString() + ", SayfaSayisi='" + textBox7.Text + "', AdetSayisi=" + numericUpDown1.Value + " WHERE KitapNo=" + textBox1.Text + "";
                            cmd.ExecuteNonQuery();
                            baglanti.Close();
                            MessageBox.Show("Kitap bilgileri güncellendi.");
                            this.Close();
                        }
                    }
                }
                catch (Exception hata)
                {
                    MessageBox.Show(hata.Message);
                }
                dbClass.closeConnection();
            }
            else
            {
                kitapClss.YetkiIzin();
            }
        }

        void YazarSorgu()
        {
            dbClass.openConnection();
            string sorgu1 = "SELECT * FROM tblYazar WHERE YazarAd='"+textBox3.Text+"'";
            dbClass.cmd = new OleDbCommand(sorgu1, dbClass.con);
            OleDbDataReader dr = dbClass.cmd.ExecuteReader();

            while (dr.Read())
            {
                if (dr["YazarAd"].ToString() == textBox3.Text)
                {
                    YazarNum = Convert.ToInt32(dr.GetValue(0).ToString());
                    break;
                }
            }
            dbClass.closeConnection();
        }
        
        void TurSorgu()
        {
            dbClass.openConnection();
            string sorgu1 = "SELECT * FROM tblTur WHERE TurAd='" + textBox6.Text + "'";
            dbClass.cmd = new OleDbCommand(sorgu1, dbClass.con);
            OleDbDataReader dr = dbClass.cmd.ExecuteReader();

            while (dr.Read())
            {
                if (dr["TurAd"].ToString() == textBox6.Text)
                {
                    TurNum = Convert.ToInt32(dr.GetValue(0).ToString());
                    break;
                }
            }
            dbClass.closeConnection();
        }
        int YazarNum = 0;
        int TurNum = 0;
        private void btnEkle_Click(object sender, EventArgs e)
        {
            if (globalVeriAktarimi.Yetki == "Yönetici")
            {
                try
                {
                    dialog = MessageBox.Show("Bu işleme devam etmek istiyor musunuz?", "UYARI", MessageBoxButtons.YesNo);
                    if (dialog == DialogResult.Yes)
                    {
                        YazilariBuyut();
                        if (textBox2.Text != "" && textBox3.Text != "" && textBox4.Text != "" && textBox5.Text != "" && textBox6.Text != "" && textBox7.Text != "" && numericUpDown1.Value != 0)
                        {
                            YazarNum = 0;
                            TurNum = 0;

                            YazarSorgu();
                            TurSorgu();
                            
                            
                            if (YazarNum == 0 && textBox3.Text != "")
                            {
                                string sorgu1 = "INSERT INTO tblYazar(YazarAd) VALUES ('" + textBox3.Text + "')";
                                kitapClss.kitapSorgu(sorgu1);
                                YazarGetir();
                            }
                            if (TurNum == 0 && textBox6.Text != "")
                            {
                                string sorgu1 = "INSERT INTO tblTur(TurAd) VALUES ('" + textBox6.Text + "')";
                                kitapClss.kitapSorgu(sorgu1);
                                TurGetir();
                            }
                            string sorgu = "INSERT INTO tblKitap(KitapAd,YazarNo,Yayinevi,Barkod,TurNo,SayfaSayisi,AdetSayisi) VALUES ('" + textBox2.Text + "','" + YazarNum + "','" + textBox4.Text + "','" + textBox5.Text + "','" + TurNum + "','" + textBox7.Text + "','" + numericUpDown1.Value + "')";

                            YazarSorgu();
                            TurSorgu();
                            kitapClss.kitapSorgu(sorgu);
                            MessageBox.Show("Kitap eklendi.");
                            this.Close();
                        }
                    }
                    else
                        MessageBox.Show("Tüm alanları doldurduğunuzdan emin olun.");
                }
                catch (Exception hata)
                {
                    MessageBox.Show(hata.Message);
                }
                
            }
            else
            {
                kitapClss.YetkiIzin();
            }
        }
        
        public void YazarGetir()
        {

            OleDbConnection baglanti = new OleDbConnection(globalVeriAktarimi.BaglantiYolu);
            string sorgu = "Select YazarNo from tblYazar order by tblYazar.YazarNo desc";
            OleDbCommand komut = new OleDbCommand(sorgu, baglanti);
            baglanti.Open();
            YazarNum = Convert.ToInt32(komut.ExecuteScalar());
            baglanti.Close();

        }
        public void TurGetir()
        {

            OleDbConnection baglanti = new OleDbConnection(globalVeriAktarimi.BaglantiYolu);
            string sorgu = "Select TurNo from tblTur order by tblTur.TurNo desc";
            OleDbCommand komut = new OleDbCommand(sorgu, baglanti);
            baglanti.Open();
            TurNum = Convert.ToInt32(komut.ExecuteScalar());
            baglanti.Close();

        }
        private void button1_Click(object sender, EventArgs e)
        {
            if (globalVeriAktarimi.Adet >= 1)
            {
                FrmKitapVer FormKitapVer = new FrmKitapVer();
                FormKitapVer.ShowDialog();
            }
            else
                MessageBox.Show($"{globalVeriAktarimi.KitapAd} adlı kitabın tümü öğrencilerde.");
        }


        private void FrmKitapDetaylar_Activated(object sender, EventArgs e)
        {
           
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void textBox7_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
        }
    }
}
