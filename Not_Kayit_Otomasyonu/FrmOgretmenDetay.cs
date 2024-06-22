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
    public partial class FrmOgretmenDetay : Form
    {
        public FrmOgretmenDetay()
        {
            InitializeComponent();
        }

        SqlConnection conn = new SqlConnection(@"Data Source=DESKTOP-3JI920O\SQLEXPRESS;Initial Catalog=DbNotKayit;Integrated Security=True;");

        private void FrmOgretmenDetay_Load(object sender, EventArgs e)
        {
            // TODO: Bu kod satırı 'dbNotKayitDataSet.TBLDERS' tablosuna veri yükler. Bunu gerektiği şekilde taşıyabilir, veya kaldırabilirsiniz.
            this.tBLDERSTableAdapter.Fill(this.dbNotKayitDataSet.TBLDERS);

        }

        private void button1_Click(object sender, EventArgs e)
        {
            conn.Open();
            SqlCommand cmd = new SqlCommand("insert into TBLDERS (OGRNUMARA,OGRAD,OGRSOYAD) values (@P1,@P2,@P3)", conn);
            cmd.Parameters.AddWithValue("@P1", mskNumara.Text);
            cmd.Parameters.AddWithValue("@P2", txtAd.Text);
            cmd.Parameters.AddWithValue("@P3", txtSoyad.Text);
            cmd.ExecuteNonQuery();
            conn.Close();
            MessageBox.Show("Öğrenci Sisteme Eklendi");
            this.tBLDERSTableAdapter.Fill(this.dbNotKayitDataSet.TBLDERS);
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int secilen = dataGridView1.SelectedCells[0].RowIndex;
            mskNumara.Text = dataGridView1.Rows[secilen].Cells[1].Value.ToString();
            txtAd.Text = dataGridView1.Rows[secilen].Cells[2].Value.ToString();
            txtSoyad.Text = dataGridView1.Rows[secilen].Cells[3].Value.ToString();
            txtS1.Text = dataGridView1.Rows[secilen].Cells[4].Value.ToString();
            txtS2.Text = dataGridView1.Rows[secilen].Cells[5].Value.ToString();
            txtS3.Text = dataGridView1.Rows[secilen].Cells[6].Value.ToString();
        }

        public string durum;

        private void btnGuncelle_Click(object sender, EventArgs e)
        {
            double ortalama, s1, s2, s3;
            
            s1 = Convert.ToDouble(txtS1.Text);
            s2 = Convert.ToDouble(txtS2.Text);
            s3 = Convert.ToDouble(txtS3.Text);

            ortalama = (s1 + s2 + s3) / 3;
            lblOrt.Text = ortalama.ToString();

            if (ortalama >= 50)
            {
                durum = "True";
            }
            else
            {
                durum = "False";
            }

            conn.Open();
            SqlCommand cmd = new SqlCommand("update TBLDERS set OGRS1 = @P1, OGRS2 = @P2, OGRS3 = @P3, ORT = @P4, DURUM = @P5 where OGRNUMARA = @P6", conn);
            cmd.Parameters.AddWithValue("@P1", txtS1.Text);
            cmd.Parameters.AddWithValue("@P2", txtS2.Text);
            cmd.Parameters.AddWithValue("@P3", txtS3.Text);
            cmd.Parameters.AddWithValue("@P4", decimal.Parse(lblOrt.Text));
            cmd.Parameters.AddWithValue("@P5", durum);
            cmd.Parameters.AddWithValue("@P6", mskNumara.Text);
            cmd.ExecuteNonQuery();
            conn.Close();
            MessageBox.Show("Öğrenci Notları Güncellendi");
            this.tBLDERSTableAdapter.Fill(this.dbNotKayitDataSet.TBLDERS);

        }
    }
}
