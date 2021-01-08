using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using System.IO;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Data.Common;

using System.Data.OleDb;

using System.Web.Security;


namespace EnvanterTveY
{
    public partial class SQLislemleri : System.Web.UI.Page
    {
        SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["KabloHE"].ConnectionString);
        string sayfa = "SQLislemler";
        DataTable dt;
        DataTable dt_excel = new DataTable();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {


                if (Session["KULLANICI_ID"] == null)
                {
                    Response.Redirect("Giris.aspx?url=" + sayfa);
                }
                else
                {
                    if (Session["KULLANICI_ROL"].ToString() != "ADMIN")
                    {
                        Response.Redirect("Default");
                    }
                }
                tablo_doldur();


            }
        }
        void tablo_doldur()
        {

            SqlDataSource1.SelectCommand = "SELECT NAME FROM sys.tables ORDER BY NAME ASC";
            string db = "";


            db = "KabloHE";

            conn.ConnectionString = ConfigurationManager.ConnectionStrings[db].ConnectionString;
            SqlDataSource1.ConnectionString = ConfigurationManager.ConnectionStrings[db].ConnectionString;

            conn.Open();
            //SqlDataSource1.SelectCommand = "SELECT IL, ID FROM IL ";
            SqlDataSource1.DataBind();
            conn.Close();




        }
        protected void Button3_Click(object sender, EventArgs e)
        {
            try
            {
                //SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["randevu"].ConnectionString);
                //SQL_baglanti_sec();
                conn.Open();
                SqlCommand sorgu = new SqlCommand();
                sorgu.Connection = conn;
                sorgu.CommandText = TextBox1.Text;
                sorgu.ExecuteNonQuery();
                conn.Close();
                Label2.Text = "Komut Başarıyla işletilmiştir.";
                ScriptManager.RegisterStartupScript(this, this.GetType(), System.Guid.NewGuid().ToString(), "YeniMesaj('Komut Başarıyla işletilmiştir.','Tamam');", true);

                Label2.CssClass = "label_durum";
            }
            catch (Exception ex)
            {
                Label2.Text = "Komut işletme sırasında hata oluştu. " + ex.Message;
                ScriptManager.RegisterStartupScript(this, this.GetType(), System.Guid.NewGuid().ToString(), "YeniMesaj('Komut işletme sırasında hata oluştu.','Hata');", true);

                Label2.CssClass = "label_durum_hata";
            }
        }

        protected void Button11_Click(object sender, EventArgs e)
        {
            try
            {


                string sql = TextBox1.Text;

                //SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["randevu"].ConnectionString);
                //SQL_baglanti_sec();

                conn.Open();
                SqlCommand sorgu = new SqlCommand();
                sorgu.Connection = conn;

                SqlDataAdapter da = new SqlDataAdapter(sql, conn);
                DataSet ds = new DataSet();
                da.Fill(ds);

                GridView1.DataSource = ds.Tables[0];
                GridView1.DataBind();
                conn.Close();
                ScriptManager.RegisterStartupScript(this, this.GetType(), System.Guid.NewGuid().ToString(), "YeniMesaj('Tablo yüklenmiştir.','Tamam');", true);

                Label2.Text = "Toplam " + GridView1.Rows.Count.ToString() + " adet kayıt bulunmuştur.";

            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), System.Guid.NewGuid().ToString(), "YeniMesaj('Komut işletme sırasında hata oluştu.','Hata');", true);



            }
        }

        protected void btnara_Click(object sender, EventArgs e)
        {
            try
            {


                string sql = "SELECT * FROM " + comtable.SelectedItem.Text + " ORDER BY ID DESC";
                //string sql = "SELECT TOP 50 * FROM " + comtable.SelectedItem.Text + " ORDER BY ID DESC";

                //SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["randevu"].ConnectionString);
                //SQL_baglanti_sec();

                conn.Open();
                SqlCommand sorgu = new SqlCommand();
                sorgu.Connection = conn;

                SqlDataAdapter da = new SqlDataAdapter(sql, conn);
                DataSet ds = new DataSet();
                da.Fill(ds);

                GridView1.DataSource = ds.Tables[0];
                GridView1.DataBind();
                conn.Close();

                Label2.Text = "Toplam " + GridView1.Rows.Count.ToString() + " adet kayıt bulunmuştur.";
                ScriptManager.RegisterStartupScript(this, this.GetType(), System.Guid.NewGuid().ToString(), "YeniMesaj('Tablo yüklenmiştir.','Tamam');", true);
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), System.Guid.NewGuid().ToString(), "YeniMesaj('Komut işletme sırasında hata oluştu.','Hata');", true);
            }

        }

        protected void Button13_Click(object sender, EventArgs e)
        {
            string baglanti = "";

            if (FileUpload1.PostedFile.FileName != "")
            {
                string dosyaadi = Path.GetFileName(FileUpload1.PostedFile.FileName);
                string dosyauzanti = Path.GetExtension(FileUpload1.PostedFile.FileName);
                string dosyayeri = Server.MapPath("~/Data/Yuklemeler/" + Guid.NewGuid().ToString().Substring(0, 8) + "_" + dosyaadi);
                if (dosyaadi.Length > 200)
                    lbldosyadi.Text = dosyaadi.Substring(0, 199);
                else
                    lbldosyadi.Text = dosyaadi;
                FileUpload1.SaveAs(dosyayeri);
                //Response.Write(dosyayeri);
                //www.aspnetornekleri.com
                if (dosyauzanti == ".xls")
                {
                    baglanti = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + dosyayeri + ";Extended Properties=\"Excel 8.0;HDR=Yes;IMEX=2\"";
                }
                else if (dosyauzanti == ".xlsx")
                {
                    baglanti = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + dosyayeri + ";Extended Properties=\"Excel 12.0;HDR=Yes;IMEX=2\"";
                }
                //www.aspnetornekleri.com
                OleDbConnection bag = new OleDbConnection(baglanti);
                OleDbCommand komut = new OleDbCommand();
                komut.CommandType = System.Data.CommandType.Text;
                komut.Connection = bag;
                OleDbDataAdapter dAdapter = new OleDbDataAdapter(komut);
                //DataTable excel = new DataTable();
                dt_excel.Clear();
                //ds.Clear();
                //ds.Tables.Clear();
                bag.Open();
                DataTable excelsayfasi = bag.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, null);
                string excelsayfasininadi = excelsayfasi.Rows[0]["Table_Name"].ToString();
                komut.CommandText = "SELECT * FROM [" + excelsayfasininadi + "]";
                dAdapter.SelectCommand = komut;
                dAdapter.Fill(dt_excel);
                //excelkayitlari.Columns[0].ColumnName = "adsoyad";
                //excelkayitlari.Columns[1].ColumnName = "sinif";
                //excelkayitlari.Columns[2].ColumnName = "numara";
                bag.Close();
                GridView1.DataSource = dt_excel;
                GridView1.DataBind();
                //dAdapter.Fill(ds);
                File.Delete(dosyayeri);

                FileUpload1.CssClass = "btn btn-info disable";

            }
            else
                Label2.Text = "Dosya seçmediniz!!!";


        }

        protected void Button14_Click(object sender, EventArgs e)
        {
            try
            {
                //SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["randevu"].ConnectionString);
                //SQL_baglanti_sec();
                conn.Open();
                SqlCommand sorgu = new SqlCommand();
                sorgu.Connection = conn;
                string sql;
                for (int i = 0; i < GridView1.Rows.Count; i++)
                {

                    sql = TextBox1.Text;
                    if (sql.IndexOf("@deger0") > -1)
                        sql = sql.Replace("@deger0", HttpUtility.HtmlDecode(GridView1.Rows[i].Cells[0].Text));
                    if (sql.IndexOf("@deger1") > -1)
                        sql = sql.Replace("@deger1", HttpUtility.HtmlDecode(GridView1.Rows[i].Cells[1].Text));
                    if (sql.IndexOf("@deger2") > -1)
                        sql = sql.Replace("@deger2", HttpUtility.HtmlDecode(GridView1.Rows[i].Cells[2].Text));
                    if (sql.IndexOf("@deger3") > -1)
                        sql = sql.Replace("@deger3", HttpUtility.HtmlDecode(GridView1.Rows[i].Cells[3].Text));
                    if (sql.IndexOf("@deger4") > -1)
                        sql = sql.Replace("@deger4", HttpUtility.HtmlDecode(GridView1.Rows[i].Cells[4].Text));
                    sorgu.CommandText = sql;
                    sorgu.ExecuteNonQuery();


                }


                conn.Close();
                Label2.Text = "Komut Başarıyla işletilmiştir.";
                Label2.CssClass = "label_durum";
            }
            catch (Exception ex)
            {
                Label2.Text = "Komut işletme sırasında hata oluştu. " + ex.Message;
                Label2.CssClass = "label_durum_hata";
            }

        }
    }
}