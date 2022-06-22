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
    public partial class FrmOgrenciDetaylar : Form
    {
        public FrmOgrenciDetaylar()
        {
            InitializeComponent();
        }
        databaseClass dbClass = new databaseClass();
        private void FrmOgrenciDetaylar_Load(object sender, EventArgs e)
        {
            comboboxSube();
            textBox1.Text = globalVeriAktarimi.Numara.ToString();
            textBox2.Text = globalVeriAktarimi.TCNo;
            textBox3.Text = globalVeriAktarimi.AdSoyad;
            comboBox1.Text = globalVeriAktarimi.Sinif.ToString();
            comboBox2.Text = globalVeriAktarimi.Sube;
            comboBox3.Text = globalVeriAktarimi.Program;
        }
        void comboboxSube()
        {
            for (char a = 'A'; a <= 'Z'; a++)
            {
                comboBox2.Items.Add(a);
            }
            comboBox1.SelectedIndex = 0;comboBox2.SelectedIndex = 0;comboBox3.SelectedIndex = 0;
        }

        private void btnTemizle_Click(object sender, EventArgs e)
        {
            textBox1.Text = "";
            textBox2.Text = "";
            textBox3.Text = "";
            comboboxSube();
        }
        DialogResult dialog = new DialogResult();
        private void btnSil_Click(object sender, EventArgs e)
        {
            if (globalVeriAktarimi.Yetki == "Yönetici")
            {
                dialog = MessageBox.Show("Bu işleme devam etmek istiyor musunuz?", "UYARI", MessageBoxButtons.YesNo);
                if (dialog == DialogResult.Yes)
                {
                    string sorgu = "DELETE FROM tblOgrenci WHERE OgrenciNo = '" + textBox1.Text + "'";
                    dbClass.kitapSorgu(sorgu);
                    MessageBox.Show("Öğrenci kaydı silindi.");
                    this.Close();
                }
            }
            else
            {
                dbClass.YetkiIzin();
            }
        }
        int sinif;
        private void btnGuncelle_Click(object sender, EventArgs e)
        {
            if (globalVeriAktarimi.Yetki == "Yönetici")
            {
                dialog = MessageBox.Show("Bu işleme devam etmek istiyor musunuz?", "UYARI", MessageBoxButtons.YesNo);
                if (dialog == DialogResult.Yes)
                {
                    sinif = int.Parse(comboBox1.Text);
                    string sorgu = "UPDATE tblOgrenci SET Tcno='" + textBox2.Text + "', OgrenciNo='" + textBox1.Text + "', AdSoyad='" + textBox3.Text + "', Sinif=" + sinif + ", Sube='" + comboBox2.Text + "', Program='" + comboBox3.Text + "' WHERE OgrenciNo='" + globalVeriAktarimi.Numara + "'";
                    dbClass.kitapSorgu(sorgu);
                    MessageBox.Show("Öğrenci bilgileri güncellendi.");
                    this.Close();
                }
            }
            else
            {
               dbClass.YetkiIzin();
            }
        }

        private void btnEkle_Click(object sender, EventArgs e)
        {
            if (globalVeriAktarimi.Yetki == "Yönetici")
            {
                sinif = int.Parse(comboBox1.Text);
                if (textBox1.Text != "" && textBox2.Text != "" && textBox3.Text != "" && comboBox1.Text != "" && comboBox2.Text != "" && comboBox3.Text != "" && globalVeriAktarimi.Numara != Convert.ToInt16(textBox1.Text))
                {
                    dialog = MessageBox.Show("Bu işleme devam etmek istiyor musunuz?", "UYARI", MessageBoxButtons.YesNo);
                    if (dialog == DialogResult.Yes)
                    {
                        string sorgu = "INSERT INTO tblOgrenci (Tcno,OgrenciNo,AdSoyad,Sinif,Sube,Program) values ('" + textBox2.Text + "','" + textBox1.Text + "','" + textBox3.Text + "','" + sinif + "','" + comboBox2.Text + "','" + comboBox3.Text + "')";
                        dbClass.kitapSorgu(sorgu);
                        MessageBox.Show("Öğrenci eklendi.");
                        this.Close();
                    }
                }
                else
                    MessageBox.Show("Numara farklı olmalıdır ve alanlar boş bırakılamaz.");
            }
            else
            {
                dbClass.YetkiIzin();
            }
        }
    }
}
