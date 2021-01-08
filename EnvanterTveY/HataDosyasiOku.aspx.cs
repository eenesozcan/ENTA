using System;
using System.Collections.Generic;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Configuration;
using System.IO;

//ok
namespace EnvanterTveY.App_Start
{
    public partial class HataDosyasiOku : System.Web.UI.Page
    {
        SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["KabloHE"].ConnectionString);

        protected void Page_Load(object sender, EventArgs e)
        {
            string sayfa = "HataDosyasiOku.aspx";
            if (Session["KULLANICI_ID"] == null)
            {
                Response.Redirect("Giris.aspx?url=" + sayfa);
            }

            if (yetki_kontrol() == "ADMIN")
            {
                if (File.Exists(Server.MapPath("//Data//Hata//" + DateTime.Now.ToString("yyyy.MM.dd") + "_HataDosyasi.txt")))
                {
                    StreamReader StreamReader1 = new StreamReader(Server.MapPath("//Data//Hata//" + DateTime.Now.ToString("yyyy.MM.dd") + "_HataDosyasi.txt"));

                    txthata.Text = StreamReader1.ReadToEnd();

                    StreamReader1.Close();
                }

                if (!IsPostBack)
                {
                    string[] filePaths = Directory.GetFiles(Server.MapPath("//Data//Hata//"));
                    List<ListItem> files = new List<ListItem>();
                    foreach (string filePath in filePaths)
                    {
                        files.Add(new ListItem(Path.GetFileName(filePath), filePath));
                    }

                    DropDownList1.DataSource = files;
                    DropDownList1.DataBind();
                }
            }
            else
                Response.Redirect("Default.aspx");

        }
        string yetki_kontrol()
        {
            conn.Open();
            SqlCommand sorgu = new SqlCommand();
            sorgu.Connection = conn;
            sorgu.CommandText = "select R.ROL FROM KULLANICI AS K INNER JOIN ROL AS R ON R.ID=K.ROL_ID WHERE K.ID='" + Session["KULLANICI_ID"].ToString() + "'  ";
            string yetki = Convert.ToString(sorgu.ExecuteScalar());
            conn.Close();
            return yetki;
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            File.Copy(Server.MapPath("/Data/Hata/HataDosyasi.txt"), Server.MapPath("/data/Hata/" + DateTime.Now.ToShortDateString().Replace(".", "") + "_" + DateTime.Now.ToShortTimeString().Replace(":", "") + "_HataDosyasi.txt"));
            File.Delete(Server.MapPath("/Data/Hata/HataDosyasi.txt"));
            File.Create(Server.MapPath("/Data/Hata/HataDosyasi.txt"));
        }

        protected void DropDownList1_DataBound(object sender, EventArgs e)
        {
            string dosyaadi = DateTime.Now.ToShortDateString().Replace(".", "") + "_HataDosyasi.txt";
            try
            {
                DropDownList1.SelectedIndex = DropDownList1.Items.IndexOf(DropDownList1.Items.FindByText(dosyaadi));
            }
            catch
            {
            }
        }

        protected void DropDownList1_SelectedIndexChanged(object sender, EventArgs e)
        {
            StreamReader StreamReader1 = new StreamReader(Server.MapPath("//Data//Hata//" + DropDownList1.SelectedItem.ToString() + ""));

            txthata.Text = StreamReader1.ReadToEnd();

            StreamReader1.Close();
        }
    }
}