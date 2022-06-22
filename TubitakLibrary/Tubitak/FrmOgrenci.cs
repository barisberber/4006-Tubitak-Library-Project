using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Tubitak
{
    public partial class FrmOgrenci : Form
    {
        public FrmOgrenci()
        {
            InitializeComponent();
        }
        databaseClass dbClass = new databaseClass();
        private void FrmOgrenci_Load(object sender, EventArgs e)
        {
            FrmKitap frmKitap = new FrmKitap();
            frmKitap.DataGridViewSettings(dataGridView1);
            OgrenciListele();
            comboBox1.SelectedIndex = 0;
            textBox1.Focus();
        }

        public void OgrenciListele()
        {
            string sorgu = "SELECT tblOgrenci.Tcno,tblOgrenci.OgrenciNo,tblOgrenci.AdSoyad,tblOgrenci.Sinif,tblOgrenci.Sube,tblOgrenci.Program,tblKitap.KitapAd,tblOgrenci.ToplamOkunanKitap FROM tblOgrenci left join tblKitap on tblOgrenci.KitapNo=tblKitap.KitapNo order by tblOgrenci.OgrenciNo asc";
            dataGridView1.DataSource = null;
            dataGridView1.DataSource = dbClass.Tablo(sorgu);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if(textBox1.Text=="")
                OgrenciListele();
        }
        FrmOgrenciDetaylar frmOgrenciDetaylar = new FrmOgrenciDetaylar();

        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            globalVeriAktarimi.TCNo = dataGridView1.CurrentRow.Cells[0].Value.ToString();
            globalVeriAktarimi.Numara = Convert.ToInt16(dataGridView1.CurrentRow.Cells[1].Value);
            globalVeriAktarimi.AdSoyad = dataGridView1.CurrentRow.Cells[2].Value.ToString();
            globalVeriAktarimi.Sinif = Convert.ToInt16(dataGridView1.CurrentRow.Cells[3].Value);
            globalVeriAktarimi.Sube = dataGridView1.CurrentRow.Cells[4].Value.ToString();
            globalVeriAktarimi.Program = dataGridView1.CurrentRow.Cells[5].Value.ToString();
            frmOgrenciDetaylar.ShowDialog();
        }
        private void btnEkle_Click(object sender, EventArgs e)
        {
            globalVeriAktarimi.Numara = 0;
            globalVeriAktarimi.AdSoyad = "";
            globalVeriAktarimi.TCNo = "";
            globalVeriAktarimi.Sinif = 9;
            globalVeriAktarimi.Sube = "A";
            globalVeriAktarimi.Program = "AMP";
            if (globalVeriAktarimi.Yetki == "Yönetici")
            {
                frmOgrenciDetaylar.ShowDialog();
            }
            else
            {
                dbClass.YetkiIzin();
            }
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            

            string sorgu = "SELECT tblOgrenci.Tcno,tblOgrenci.OgrenciNo,tblOgrenci.AdSoyad,tblOgrenci.Sinif,tblOgrenci.Sube,tblOgrenci.Program," +
                "tblKitap.KitapAd,tblOgrenci.ToplamOkunanKitap " +
                "FROM tblOgrenci inner join tblKitap on tblOgrenci.KitapNo=tblKitap.KitapNo";

            if (comboBox1.SelectedIndex == 0)
                sorgu = "SELECT tblOgrenci.Tcno,tblOgrenci.OgrenciNo,tblOgrenci.AdSoyad,tblOgrenci.Sinif,tblOgrenci.Sube,tblOgrenci.Program," +
                    "tblKitap.KitapAd,tblOgrenci.ToplamOkunanKitap " +
                    "FROM tblOgrenci inner join tblKitap on tblOgrenci.KitapNo=tblKitap.KitapNo " +
                    "WHERE tblOgrenci.AdSoyad LIKE '%" + textBox1.Text + "%'";

            if (comboBox1.SelectedIndex == 1)
                sorgu = "SELECT tblOgrenci.Tcno,tblOgrenci.OgrenciNo,tblOgrenci.AdSoyad,tblOgrenci.Sinif,tblOgrenci.Sube,tblOgrenci.Program," +
                    "tblKitap.KitapAd,tblOgrenci.ToplamOkunanKitap " +
                    "FROM tblOgrenci inner join tblKitap on tblOgrenci.KitapNo=tblKitap.KitapNo " +
                    "WHERE tblOgrenci.OgrenciNo LIKE '%" + textBox1.Text + "%'";

            dataGridView1.DataSource = dbClass.Tablo(sorgu);

            if (textBox1.Text == "")
                OgrenciListele();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            textBox1.Clear();
            textBox1.Focus();
        }
    }
}
