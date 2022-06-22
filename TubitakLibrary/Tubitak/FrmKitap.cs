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
    public partial class FrmKitap : Form
    {
        public FrmKitap()
        {
            InitializeComponent();
        }
        databaseClass kitapcls = new databaseClass();
        public void TumKitaplariListele()
        {
            try
            {
                string sorgu = "SELECT tblKitap.KitapNo,tblKitap.KitapAd,tblYazar.YazarAd,tblKitap.Yayinevi,tblTur.TurAd,tblKitap.SayfaSayisi,tblKitap.AdetSayisi,tblKitap.Barkod from" +
                    " (tblKitap inner join tblYazar on tblKitap.YazarNo=tblYazar.YazarNo)" +
                    " inner join tblTur on tblKitap.TurNo=tblTur.TurNo order by tblKitap.KitapNo asc";
                dataGridView1.DataSource = kitapcls.Tablo(sorgu);

            }
             catch (Exception hata)
             {
                 MessageBox.Show(hata.Message);
             }
         }

         public void GeriGelmeyenKitaplariListele()
         {
              string sorgu = "select tblKitap.KitapNo,tblKitap.KitapAd,tblOgrenci.OgrenciNo,tblOgrenci.AdSoyad,tblOgrenci.Sinif,tblOgrenci.Sube,tblOgrenci.Program,tblAlinanKitaplar.VerilenTarih,tblAlinanKitaplar.AlinacakTarih from (tblKitap inner join tblAlinanKitaplar on tblKitap.KitapNo=tblAlinanKitaplar.KitapNo) inner join tblOgrenci on tblAlinanKitaplar.OgrenciNo=tblOgrenci.OgrenciNo where tblAlinanKitaplar.AlinanTarih is null order by tblKitap.KitapNo asc";
              dataGridView1.DataSource = kitapcls.Tablo(sorgu);
         }


         private void FrmKitap_Load(object sender, EventArgs e)
         {
             TumKitaplariListele();
             radioButton1.Checked = true;
             DataGridViewSettings(dataGridView1);
             comboBox1.SelectedIndex = 0;
             comboBox2.SelectedIndex = 0;
             textBox1.Focus();
         }

         public void DataGridViewSettings(DataGridView datagridview)
         {
             datagridview.RowHeadersVisible = false;
             datagridview.AllowUserToAddRows = false;
             datagridview.BorderStyle = BorderStyle.None;
             datagridview.DefaultCellStyle.SelectionBackColor = Color.FromArgb(0, 126, 249);
             datagridview.DefaultCellStyle.SelectionForeColor = Color.White;
             datagridview.EnableHeadersVisualStyles = false;
             datagridview.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(50, 50, 50);
             datagridview.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
             datagridview.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
             datagridview.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
         }

         // AD - YAZAR - BARKOD'A GORE ARAMA FILTRELEME
         private void textBox1_TextChanged(object sender, EventArgs e)
         {
            textBox1.Text = textBox1.Text.ToUpper();
            textBox1.SelectionStart = textBox1.Text.Length;
            string sorgu = "select tblKitap.KitapNo,tblKitap.KitapAd,tblYazar.YazarAd,tblKitap.Yayinevi,tblTur.TurAd,tblKitap.SayfaSayisi,tblKitap.AdetSayisi " +
                 "from (tblKitap inner join tblYazar on tblKitap.YazarNo=tblYazar.YazarNo) inner join tblTur on tblKitap.TurNo=tblTur.TurNo order by tblKitap.KitapNo asc";
            if (radioButton1.Checked)
            {

            
             // okuldaki kitapları listele
             if (radioButton1.Checked && comboBox1.SelectedIndex == 0)
                 sorgu = "select tblKitap.KitapNo, tblKitap.KitapAd,tblYazar.YazarAd,tblKitap.Yayinevi,tblTur.TurAd,tblKitap.SayfaSayisi,tblKitap.AdetSayisi,tblKitap.Barkod " +
                     "from (tblKitap inner join tblYazar on tblKitap.YazarNo=tblYazar.YazarNo) " +
                     "inner join tblTur on tblKitap.TurNo=tblTur.TurNo " +
                     "WHERE tblKitap.KitapAd LIKE '%" + textBox1.Text + "%' order by tblKitap.KitapNo asc";
             // Barkoda göre listele
             if (radioButton1.Checked && comboBox1.SelectedIndex == 1)
             {
                 sorgu = "select tblKitap.KitapNo, tblKitap.KitapAd,tblYazar.YazarAd,tblKitap.Yayinevi,tblTur.TurAd,tblKitap.SayfaSayisi,tblKitap.AdetSayisi,tblKitap.Barkod " +
                     "from (tblKitap inner join tblYazar on tblKitap.YazarNo=tblYazar.YazarNo) " +
                     "inner join tblTur on tblKitap.TurNo=tblTur.TurNo " +
                     "WHERE tblKitap.Barkod LIKE '%" + textBox1.Text + "%' order by tblKitap.KitapNo asc";
             }

            dataGridView1.DataSource = kitapcls.Tablo(sorgu);
            }

        }

        // SECİLEN HUCREDEKİ VERİLER CLASS'A AKTARILDI
        FrmKitapDetaylar frmKitapDetaylar = new FrmKitapDetaylar();
        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            globalVeriAktarimi.Numara = Convert.ToInt32( dataGridView1.CurrentRow.Cells[0].Value );
            globalVeriAktarimi.KitapAd = dataGridView1.CurrentRow.Cells[1].Value.ToString();
            if (radioButton1.Checked)
            {
                globalVeriAktarimi.YazarAd = dataGridView1.CurrentRow.Cells[2].Value.ToString();
                globalVeriAktarimi.Yayinevi = dataGridView1.CurrentRow.Cells[3].Value.ToString();
                globalVeriAktarimi.Tur = dataGridView1.CurrentRow.Cells[4].Value.ToString();
                globalVeriAktarimi.Sayfa = dataGridView1.CurrentRow.Cells[5].Value.ToString();
                globalVeriAktarimi.Adet = Convert.ToInt16(dataGridView1.CurrentRow.Cells[6].Value);
                globalVeriAktarimi.Barkod = dataGridView1.CurrentRow.Cells[7].Value.ToString();
                frmKitapDetaylar.ShowDialog();
            }
            if (radioButton3.Checked)
            {
                globalVeriAktarimi.OgrenciNo = dataGridView1.CurrentRow.Cells[2].Value.ToString();
                globalVeriAktarimi.AdSoyad = dataGridView1.CurrentRow.Cells[3].Value.ToString();
                globalVeriAktarimi.OgrenciSinif = dataGridView1.CurrentRow.Cells[4].Value.ToString() + dataGridView1.CurrentRow.Cells[5].Value.ToString();
                FrmKitapAl frmKitapAl = new FrmKitapAl();
                frmKitapAl.ShowDialog();
            }
            
        }

        private void btnEkle_Click(object sender, EventArgs e)
        {
            globalVeriAktarimi.Numara = 0; 
            globalVeriAktarimi.KitapAd = "";
            globalVeriAktarimi.YazarAd = "";
            globalVeriAktarimi.Yayinevi = "";
            globalVeriAktarimi.Barkod = "";
            globalVeriAktarimi.Tur = "";
            globalVeriAktarimi.Sayfa = "";
            globalVeriAktarimi.Kimde = "";
            globalVeriAktarimi.Adet = 0;
            if (globalVeriAktarimi.Yetki == "Yönetici")
            {
                frmKitapDetaylar.ShowDialog();
            }
            else
            {
                dbClass.YetkiIzin();
            }
        }
        databaseClass dbClass = new databaseClass();
        private void button1_Click(object sender, EventArgs e)
        {
            radioButton1.Checked = true;
            TumKitaplariListele();
        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            TumKitaplariListele();
            textBox1.Focus();
        }

        private void radioButton3_CheckedChanged(object sender, EventArgs e)
        {
            GeriGelmeyenKitaplariListele();
            textBox2.Focus();
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            textBox1.Clear();
            textBox1.Focus();
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            textBox2.Text = textBox2.Text.ToUpper();
            textBox2.SelectionStart = textBox2.Text.Length;
            string sorgu = "select tblKitap.KitapNo,tblKitap.KitapAd,tblOgrenci.OgrenciNo,tblOgrenci.AdSoyad,tblOgrenci.Sinif,tblOgrenci.Sube,tblOgrenci.Program,tblAlinanKitaplar.VerilenTarih,tblAlinanKitaplar.AlinacakTarih from (tblKitap inner join tblAlinanKitaplar on tblKitap.KitapNo=tblAlinanKitaplar.KitapNo) inner join tblOgrenci on tblAlinanKitaplar.OgrenciNo=tblOgrenci.OgrenciNo where tblAlinanKitaplar.AlinanTarih is null order by tblKitap.KitapNo asc";


            if (radioButton3.Checked)
            {

                // öğrencilerdeki kitapları listele (barkoda göre)
                if (radioButton3.Checked && comboBox2.SelectedIndex == 1)

                    sorgu = "select tblKitap.KitapNo,tblKitap.KitapAd,tblOgrenci.OgrenciNo,tblOgrenci.AdSoyad,tblOgrenci.Sinif,tblOgrenci.Sube,tblOgrenci.Program,tblAlinanKitaplar.VerilenTarih,tblAlinanKitaplar.AlinacakTarih from (tblKitap inner join tblAlinanKitaplar on tblKitap.KitapNo=tblAlinanKitaplar.KitapNo) inner join tblOgrenci on tblAlinanKitaplar.OgrenciNo=tblOgrenci.OgrenciNo WHERE tblAlinanKitaplar.AlinanTarih is null AND tblKitap.Barkod LIKE '%" + textBox2.Text + "%' order by tblKitap.KitapNo asc";
               /* sorgu = "select tblKitap.KitapNo,tblKitap.KitapAd,tblYazar.YazarAd,tblKitap.Yayinevi,tblTur.TurAd,tblKitap.SayfaSayisi,tblKitap.AdetSayisi,tblKitap.Barkod " +
                    "from (tblKitap inner join tblYazar on tblKitap.YazarNo=tblYazar.YazarNo) " +
                    "inner join tblTur on tblKitap.TurNo=tblTur.TurNo " +
                    "WHERE tblKitap.Barkod LIKE '%" + textBox1.Text + "%' order by tblKitap.KitapNo asc";*/

            // öğrencilerdeki kitapları listele (isme göre)
            if (radioButton3.Checked && comboBox2.SelectedIndex == 0)
            {
                sorgu = "select tblKitap.KitapNo,tblKitap.KitapAd,tblOgrenci.OgrenciNo,tblOgrenci.AdSoyad,tblOgrenci.Sinif,tblOgrenci.Sube,tblOgrenci.Program,tblAlinanKitaplar.VerilenTarih,tblAlinanKitaplar.AlinacakTarih from (tblKitap inner join tblAlinanKitaplar on tblKitap.KitapNo=tblAlinanKitaplar.KitapNo) inner join tblOgrenci on tblAlinanKitaplar.OgrenciNo=tblOgrenci.OgrenciNo WHERE tblAlinanKitaplar.AlinanTarih is null AND tblKitap.KitapAd LIKE '%" + textBox2.Text + "%' order by tblKitap.KitapNo asc";
            }
            if (radioButton3.Checked && comboBox2.SelectedIndex == 2)
            {
                sorgu = "select tblKitap.KitapNo,tblKitap.KitapAd,tblOgrenci.OgrenciNo,tblOgrenci.AdSoyad,tblOgrenci.Sinif,tblOgrenci.Sube,tblOgrenci.Program,tblAlinanKitaplar.VerilenTarih,tblAlinanKitaplar.AlinacakTarih from (tblKitap inner join tblAlinanKitaplar on tblKitap.KitapNo=tblAlinanKitaplar.KitapNo) inner join tblOgrenci on tblAlinanKitaplar.OgrenciNo=tblOgrenci.OgrenciNo WHERE tblAlinanKitaplar.AlinanTarih is null AND tblOgrenci.OgrenciNo LIKE '%" + textBox2.Text + "%' order by tblKitap.KitapNo asc";
            }
                dataGridView1.DataSource = kitapcls.Tablo(sorgu);

            }
        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            textBox2.Clear();
            textBox2.Focus();
        }
    }
}
