using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZXing;
using System.Drawing;
using System.Drawing.Printing;
using System.Data.OleDb;

namespace Tubitak
{
    class yazici
    {
        OleDbConnection baglanti = new OleDbConnection(globalVeriAktarimi.BaglantiYolu);
        FrmBarkod FormBarkod = new FrmBarkod();
        public int miktar = 0;

        private System.Drawing.Printing.PrintDocument printDocument1;
        private System.Windows.Forms.PageSetupDialog pageSetupDialog1;
        private System.Windows.Forms.PrintPreviewDialog printPreviewDialog1;
        public yazici()
        {
            this.printDocument1 = new System.Drawing.Printing.PrintDocument();
            this.pageSetupDialog1 = new System.Windows.Forms.PageSetupDialog();
            this.printPreviewDialog1 = new System.Windows.Forms.PrintPreviewDialog();
            this.printDocument1.PrintPage += new System.Drawing.Printing.PrintPageEventHandler(this.printDocument1_PrintPage);
        }
        public void yazdir()
        {
            printDocument1.DefaultPageSettings.PaperSize = new PaperSize("100 x 80 mm", 590, 714);
            // kağıt ölçüsü
            this.printPreviewDialog1.AutoScrollMargin = new System.Drawing.Size(0, 0);
            this.printPreviewDialog1.AutoScrollMinSize = new System.Drawing.Size(0, 0);
            this.printPreviewDialog1.ClientSize = new System.Drawing.Size(400, 300);
            this.printPreviewDialog1.Document = this.printDocument1;
            this.printPreviewDialog1.Enabled = true;
            this.printPreviewDialog1.Name = "printPreviewDialog1";
            this.printPreviewDialog1.Text = "Baskı Önizleme";
            this.printPreviewDialog1.Visible = false;
            printPreviewDialog1.ShowDialog();
            //printDocument1.Print();
        }
        private void printDocument1_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {
            Font myFont = new System.Drawing.Font("Calibri", 10);
            Font myFont2 = new Font("Calibri", 10, FontStyle.Bold);
            Font kucukfont = new Font("Calibri", 8);

            SolidBrush sbrush = new SolidBrush(Color.Black);

            e.Graphics.PageUnit = GraphicsUnit.Millimeter;

            

            var br = new BarcodeWriter();
            br.Format = BarcodeFormat.CODE_128;
            br.Options.Height = 80;

            /*           
                       e.Graphics.DrawString("Cemil Meriç", myFont2, sbrush, 17f, 10f);
                       Image qrresim = br.Write("AOS22001001");
                       e.Graphics.DrawImage(qrresim, 10f, 15f);

                       e.Graphics.DrawString("Tolstoy", myFont2, sbrush, 60f, 10f);
                       Image qrresim2 = br.Write("AOS22001002");
                       e.Graphics.DrawImage(qrresim2, 50f, 15f);

                       e.Graphics.DrawString("İsmet Özel", myFont2, sbrush, 100f, 10f);
                       Image qrresim3 = br.Write("AOS22001003");
                       e.Graphics.DrawImage(qrresim3, 90f, 15f);

                       // 2. satır
                       e.Graphics.DrawString("Alev Alatlı", myFont2, sbrush, 20f, 40f);
                       Image qrresim4 = br.Write("AOS22001004");
                       e.Graphics.DrawImage(qrresim3, 10f, 45f);*/

            // barkod başlangıç --> "AOS22001001"

            int barkodsayi = globalVeriAktarimi.SonBarkod+1;
            string barkodbaslik = "AOS2200";
            float x = 0;
            float y = 0;

            int satır = 0, sayaç = 0;

            // miktar = 11;
            satır = miktar / 3;
            satır += miktar % 3;


            for (int a = 1; a <= satır; a++)
            {
                x = 0;
                for (int b = 1; b <= 3; b++)
                {
                    Image qrresim = br.Write(barkodbaslik + barkodsayi);
                    e.Graphics.DrawImage(qrresim, 10f + x, 15f + y);
                    x = 40 * b;
                    try
                    {
                        if (baglanti.State ==System.Data.ConnectionState.Closed)
                            baglanti.Open();
                        string kayit = "insert into tblBarkod(BarkodBaslik,BarkodSayi,Barkod) values (@barkodbaslik,@barkodsayi,@barkod)";
                        OleDbCommand komut = new OleDbCommand(kayit, baglanti);
                        komut.Parameters.AddWithValue("@barkodbaslik", barkodbaslik);
                        komut.Parameters.AddWithValue("@barkodsayi", barkodsayi);
                        komut.Parameters.AddWithValue("@barkod", barkodbaslik+barkodsayi);
                        komut.ExecuteNonQuery();
                        baglanti.Close();
                        ++barkodsayi;
                        sayaç++;
                    }
                    catch (Exception hata)
                    {
                        System.Windows.Forms.MessageBox.Show("İşlem sırasında hata oluştu. "+ hata.Message);
                    }
                    if (sayaç == miktar) break;
                }
                if (sayaç == miktar) break;
                y = 30 * a;
                

            }
        }

    }
}

