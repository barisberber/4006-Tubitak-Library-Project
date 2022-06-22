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
    public partial class FrmKitapAl : Form
    {
        public FrmKitapAl()
        {
            InitializeComponent();
        }

        DialogResult dialog = new DialogResult();


        databaseClass kitapCls = new databaseClass();
        int id;
        private void button2_Click(object sender, EventArgs e)
        {
            dialog = MessageBox.Show("Bu işleme devam etmek istiyor musunuz?", "UYARI", MessageBoxButtons.YesNo);
            if (dialog == DialogResult.Yes)
            {
                string sorgu = "UPDATE tblKitap SET AdetSayisi=AdetSayisi+1 WHERE KitapAd='" + globalVeriAktarimi.KitapAd + "'";
                kitapCls.kitapSorgu(sorgu);
                sorgu = "UPDATE tblAlinanKitaplar SET AlinanTarih='" + label13.Text + "' WHERE ID=" + id + "";
                kitapCls.kitapSorgu(sorgu);
                sorgu = "UPDATE tblOgrenci SET KitapNo =0 WHERE OgrenciNo='" + label1.Text + "'";
                kitapCls.kitapSorgu(sorgu);
                MessageBox.Show("Kitap geri alındı.");
                this.Close();
            }
        }




        // KAPATMA BUTONU
        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void FrmKitapAl_Activated(object sender, EventArgs e)
        {
            label12.Text = globalVeriAktarimi.KitapAd;
            label1.Text = globalVeriAktarimi.OgrenciNo;
            globalVeriAktarimi.Bugun = DateTime.Now;
            label13.Text = globalVeriAktarimi.Bugun.ToShortDateString().ToString();

            string sinif;
            OleDbConnection con = new OleDbConnection(globalVeriAktarimi.BaglantiYolu);
            string sorgu = "select tblKitap.KitapNo,tblKitap.KitapAd,tblOgrenci.OgrenciNo,tblOgrenci.AdSoyad,tblOgrenci.Sinif,tblOgrenci.Sube,tblOgrenci.Program,tblAlinanKitaplar.VerilenTarih,tblAlinanKitaplar.AlinacakTarih,tblAlinanKitaplar.ID from (tblKitap inner join tblAlinanKitaplar on tblKitap.KitapNo=tblAlinanKitaplar.KitapNo) inner join tblOgrenci on tblAlinanKitaplar.OgrenciNo=tblOgrenci.OgrenciNo where tblAlinanKitaplar.AlinanTarih is null order by tblKitap.KitapNo asc";


            OleDbCommand cmd = new OleDbCommand(sorgu, con);
            con.Open();
            OleDbDataReader dr = cmd.ExecuteReader();

            while(dr.Read())
            {
                if (dr["OgrenciNo"].ToString() == label1.Text && dr["KitapAd"].ToString() == label12.Text)
                {
                    id = Convert.ToInt16(dr[9]);
                    label11.Text = dr[3].ToString();
                    sinif = $"{dr[4].ToString()} / {dr[5].ToString()}";
                    label5.Text = sinif;
                    globalVeriAktarimi.VerilenTarih = Convert.ToDateTime(dr[7]);
                    globalVeriAktarimi.AlinacakTarih = Convert.ToDateTime(dr[8]);
                    label8.Text = globalVeriAktarimi.VerilenTarih.ToShortDateString().ToString();
                    label9.Text = globalVeriAktarimi.AlinacakTarih.ToShortDateString().ToString();
                    break;
                }
            }

            con.Close();



        }

        private void FrmKitapAl_Load(object sender, EventArgs e)
        {

        }
    }
}
