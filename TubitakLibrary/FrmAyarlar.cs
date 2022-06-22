using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace Tubitak
{
    public partial class FrmAyarlar : Form
    {
        public FrmAyarlar()
        {
            InitializeComponent();
        }

        private void FrmAyarlar_Load(object sender, EventArgs e)
        {
            if (globalVeriAktarimi.Yetki != "Yönetici")
            {
                button1.Enabled = false;
                button3.Enabled = false;
                button4.Enabled = false;
            }
        }
        DialogResult dialog = new DialogResult();
        private void button2_Click(object sender, EventArgs e)
        {
            dialog = MessageBox.Show("Bu işleme devam etmek istiyor musunuz?", "UYARI", MessageBoxButtons.YesNo);
            if (dialog == DialogResult.Yes)
            {
                Application.Restart();
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            panel1.Controls.Clear();
            KullaniciTanimla frmKullaniciTanimla = new KullaniciTanimla() { Dock = DockStyle.Fill, TopLevel = false, TopMost = true };
            this.panel1.Controls.Add(frmKullaniciTanimla);
            frmKullaniciTanimla.Show();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            panel1.Controls.Clear();
            FrmSifreDegistir FormSifreDegistir = new FrmSifreDegistir() { Dock = DockStyle.Fill, TopLevel = false, TopMost = true };
            this.panel1.Controls.Add(FormSifreDegistir);
            FormSifreDegistir.Show();
        }
        string yol;
        string tarih = DateTime.Now.ToShortDateString().ToString();
        void KlasorOlustur()
        {
            yol = "D:\\YEDEKVT\\"+tarih;
            Directory.CreateDirectory(yol);
        }

        private void VeritabaniYedekle()
        {
            dialog = MessageBox.Show("Bu işleme devam etmek istiyor musunuz?", "UYARI", MessageBoxButtons.YesNo);
            if (dialog == DialogResult.Yes)
            {
                try
                {
                    KlasorOlustur();
                    File.Copy(Application.StartupPath + "\\Kitap.accdb", yol + "\\KutuphaneYedek.accdb");
                    MessageBox.Show("Veritabanı yedeği D: klasöründe YEDEKVT adlı dosyanın içine alındı.");
                }
                catch (Exception)
                {
                    MessageBox.Show("Bugün zaten veritabanı yedeği alınmış.");
                }
            }
        }
        private void button3_Click(object sender, EventArgs e)
        {
            VeritabaniYedekle();
        }
    }
}
