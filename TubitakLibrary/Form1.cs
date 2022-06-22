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

namespace Tubitak
{
    public partial class Form1 : Form
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


        public Form1()
        {
            InitializeComponent();
            // Formun kenarlarını ovalleştirme.
            Region = System.Drawing.Region.FromHrgn(CreateRoundRectRgn(0, 0, Width, Height, 25, 25));
        }

        // TIKLANAN BUTONUN RENGİNİ DEĞİŞTİRME
        #region TiklananButonunRenginiDegistirme
        public void btnColorChange()
        {
            btnKitap.BackColor = Color.FromArgb(24, 30, 54);
            btnRaporlar.BackColor = Color.FromArgb(24, 30, 54);
            btnAyarlar.BackColor = Color.FromArgb(24, 30, 54);
            btnBarkod.BackColor = Color.FromArgb(24, 30, 54);
            btnOgrenci.BackColor = Color.FromArgb(24, 30, 54);
        }
        #endregion
        // TIKLANAN BUTONUN RENGİNİ DEĞİŞTİRME BİTİŞ


        public void btnKitap_Click(object sender, EventArgs e)
        {
            btnColorChange();
            btnKitap.BackColor = Color.AliceBlue;
            lblBaslik.Text = "KITAP AYARLARI";
            panelMain.Controls.Clear();
            FrmKitap FormKitap = new FrmKitap() { Dock = DockStyle.Fill, TopLevel = false, TopMost = true };
            this.panelMain.Controls.Add(FormKitap);
            FormKitap.Show();
        }
        
        private void btnOgrenci_Click(object sender, EventArgs e)
        {
            btnColorChange();
            btnOgrenci.BackColor = Color.AliceBlue;
            lblBaslik.Text = "ÖĞRENCİ AYARLARI";
            panelMain.Controls.Clear();
            FrmOgrenci FormOgrenci = new FrmOgrenci() { Dock = DockStyle.Fill, TopLevel = false, TopMost = true };
            this.panelMain.Controls.Add(FormOgrenci);
            FormOgrenci.Show();
        }


        private void btnRaporlar_Click(object sender, EventArgs e)
        {
            btnColorChange();
            btnRaporlar.BackColor = Color.AliceBlue;
            lblBaslik.Text = "RAPORLAR";
            panelMain.Controls.Clear();
            RaporlarForm FrmRapor= new RaporlarForm() { Dock = DockStyle.Fill, TopLevel = false, TopMost = true };
            this.panelMain.Controls.Add(FrmRapor);
            FrmRapor.Show();
        }



        private void btnAyarlar_Click(object sender, EventArgs e)
        {
            btnColorChange();
            btnAyarlar.BackColor = Color.AliceBlue;
            lblBaslik.Text = "PROGRAM AYARLARI";
            panelMain.Controls.Clear();
            FrmAyarlar FormAyarlar = new FrmAyarlar() { Dock = DockStyle.Fill, TopLevel = false, TopMost = true };
            this.panelMain.Controls.Add(FormAyarlar);
            FormAyarlar.Show();
        }
        private void btnKapat_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void btnBarkod_Click(object sender, EventArgs e)
        {
            btnColorChange();
            btnBarkod.BackColor = Color.AliceBlue;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            label1.Text = globalVeriAktarimi.KullaniciAd;
            label2.Text = globalVeriAktarimi.Yetki;
        }

        
    }
}
