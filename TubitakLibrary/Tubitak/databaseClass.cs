using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.OleDb;

namespace Tubitak
{
    class databaseClass
    {
        public OleDbConnection con = new OleDbConnection(globalVeriAktarimi.BaglantiYolu);
        public OleDbCommand cmd;
        public OleDbDataReader dr;

        public void openConnection()
        {
            con.Open();
        }

        public void closeConnection()
        {
            if (con != null) con.Close();
        }

        public void kitapSorgu(string sorgu)
        {
            openConnection();
            cmd = new OleDbCommand(sorgu,con);
            cmd.ExecuteNonQuery();
            closeConnection();
        }
        public void Giris(string sorgu)
        {
            cmd = new OleDbCommand(sorgu,con);
            dr = cmd.ExecuteReader();
        }

        public DataTable Tablo(string sorgu)
        {
            openConnection();
            OleDbDataAdapter da = new OleDbDataAdapter(sorgu,con);
            DataTable dt = new DataTable();
            da.Fill(dt);
            closeConnection();
            return dt;
        }

        public void YetkiIzin()
        {
            System.Windows.Forms.MessageBox.Show("Bu işleme erişiminiz bulunmamaktadır.");
        }
    }
}
