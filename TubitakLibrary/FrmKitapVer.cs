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
    public partial class FrmKitapVer : Form
    {
        public FrmKitapVer()
        {
            InitializeComponent();
        }
        databaseClass kitapCls = new databaseClass();


        private void FrmKitapAlVer_Load(object sender, EventArgs e)
        {
            globalVeriAktarimi.VerilenTarih = DateTime.Now;
            globalVeriAktarimi.AlinacakTarih = globalVeriAktarimi.VerilenTarih.AddDays(15);
            label8.Text = globalVeriAktarimi.VerilenTarih.ToShortDateString().ToString();
            label9.Text = globalVeriAktarimi.AlinacakTarih.ToShortDateString().ToString();
            comboBox1.SelectedIndex = 0;
            label1.Text = globalVeriAktarimi.KitapAd;
        }
        string sinif,sube,numara;
        OleDbConnection con = new OleDbConnection(@"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=Kitap.accdb");

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            textBox1.CharacterCasing = CharacterCasing.Upper;
            string adsoyad;
            string sorgu = "SELECT * FROM tblOgrenci";
            if (comboBox1.SelectedIndex == 0) 
              sorgu = "SELECT * FROM tblOgrenci WHERE AdSoyad='" + textBox1.Text + "'";
            if (comboBox1.SelectedIndex == 1)
              sorgu = "SELECT * FROM tblOgrenci WHERE OgrenciNo='" + textBox1.Text + "'";

            OleDbCommand cmd = new OleDbCommand(sorgu, con);
            try
            {

                con.Open();
                OleDbDataReader rd = cmd.ExecuteReader();
                rd.Read();
                adsoyad = $"{rd[3].ToString()}";
                sinif = $"{rd[4].ToString()}";
                sube =  $"{rd[5].ToString()}";
                numara = $"{rd[2].ToString()}";
                label2.Text = adsoyad;
                label5.Text = $"{sinif} / {sube}";
                label11.Text = numara;

            }
            catch (Exception)
            {
                label2.Text = "Öğrenci Bulunamadı.";
                label5.Text = "Öğrenci Bulunamadı.";
                label11.Text = "Öğrenci Bulunamadı.";
            }
            finally
            {
                con.Close();
            }


        }
        
        private void button2_Click(object sender, EventArgs e)
        {
            string sorguu = "SELECT * FROM tblOgrenci";


            OleDbCommand cmd = new OleDbCommand(sorguu, con);
            con.Open();
            OleDbDataReader dr = cmd.ExecuteReader();
            try
            {
                while (dr.Read())
                {
                    if (label2.Text != "Öğrenci Bulunamadı." && label1.Text != "")
                    {
                        if (dr["OgrenciNo"].ToString() == label11.Text && Convert.ToInt16(dr["KitapNo"].ToString()) != 0)
                        {
                            MessageBox.Show($"{label2.Text} adlı öğrencide zaten kitap bulunuyor.");
                            break;
                        }
                        if (dr["OgrenciNo"].ToString() == label11.Text)
                        {
                            string sorgu = "UPDATE tblKitap SET AdetSayisi = AdetSayisi-1 WHERE KitapAd='" + globalVeriAktarimi.KitapAd + "'";
                            kitapCls.kitapSorgu(sorgu);
                            MessageBox.Show($"{label1.Text} ADLI KITAP {label2.Text} İSİMLİ {label11.Text} NUMARALI ÖĞRENCİYE VERİLMİŞTİR.");
                            sorgu = "INSERT INTO tblAlinanKitaplar (KitapNo,OgrenciNo,VerilenTarih,AlinacakTarih) values ('" + globalVeriAktarimi.Numara + "','" + label11.Text + "','" + label8.Text + "','" + label9.Text + "')";
                            kitapCls.kitapSorgu(sorgu);
                            sorgu = "UPDATE tblOgrenci SET KitapNo = " + globalVeriAktarimi.Numara + ", ToplamOkunanKitap = ToplamOkunanKitap+1 WHERE OgrenciNo='" + label11.Text + "'";
                            kitapCls.kitapSorgu(sorgu);
                            break;
                        }
                    }
                    else
                    {
                        MessageBox.Show("Geçerli bir kitap veya öğrenci bulunamadı.");
                        break;
                    }
                }
            }
            catch (Exception msgg)
            {
                MessageBox.Show(msgg.ToString());
            }
            finally
            {
                con.Close();
            }
            
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            textBox1.Clear();
        }

        private void FrmKitapVer_Activated(object sender, EventArgs e)
        {
            textBox1.Focus();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
