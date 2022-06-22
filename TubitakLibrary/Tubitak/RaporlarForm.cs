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
    public partial class RaporlarForm : Form
    {
        public RaporlarForm()
        {
            InitializeComponent();
        }



        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            
        }

        private void linkLabel2_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            
        }

        private void linkLabel3_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            
        }
        databaseClass dbClass = new databaseClass();
        private void RaporlarForm_Load(object sender, EventArgs e)
        {
            KitapOkumaSiralamasi();

        }
        private void KitapOkumaSiralamasi()
        {
            listBox1.Items.Clear();
            string sorgu = "SELECT TOP 5 OgrenciNo,AdSoyad,ToplamOkunanKitap FROM tblOgrenci ORDER BY ToplamOkunanKitap DESC";
            dbClass.openConnection();
            dbClass.cmd = new OleDbCommand(sorgu, dbClass.con);
            OleDbDataReader dr = dbClass.cmd.ExecuteReader();
            while (dr.Read())
            {
                chart1.Series["Öğrenciler"].Points.AddXY(dr["OgrenciNo"].ToString() + " " + dr["AdSoyad"].ToString() + "\n [" + dr["ToplamOkunanKitap"].ToString() + " Kitap]", dr["ToplamOkunanKitap"].ToString());
                listBox1.Items.Add(dr["OgrenciNo"].ToString() + "   |    " + dr["AdSoyad"].ToString() + "     -->>  " + dr["ToplamOkunanKitap"].ToString());
            }
            string adsoyad = listBox1.Items[0].ToString();
            label2.Text = adsoyad;
            dbClass.closeConnection();
        }
    }
}
