using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace Not_Kayit_Otomasyonu
{
    public partial class FrmOgrenciDetay : Form
    {
        public FrmOgrenciDetay()
        {
            InitializeComponent();
        }
        SqlConnection conn = new SqlConnection(@"Data Source=DESKTOP-3JI920O\SQLEXPRESS;Initial Catalog=DbNotKayit;Integrated Security=True;");
        public string numara;
        private void FrmOgrenciDetay_Load(object sender, EventArgs e)
        {
            FrmOgretmenDetay frm = new FrmOgretmenDetay();
            lblNumara.Text = numara;
            conn.Open();
            SqlCommand cmd = new SqlCommand("select * from TBLDERS where OGRNUMARA = @p1", conn);
            cmd.Parameters.AddWithValue("@p1", numara);
            SqlDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                lblAdSoyad.Text = dr[2].ToString() + " " + dr[3].ToString();
                lblS1.Text = dr[4].ToString();
                lblS2.Text = dr[5].ToString();
                lblS3.Text = dr[6].ToString();
                lblOrt.Text = dr[7].ToString();
                lblDurum.Text = dr[8].ToString();
            }
            conn.Close();
        }
    }
}
