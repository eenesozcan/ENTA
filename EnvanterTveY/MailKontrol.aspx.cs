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

namespace EnvanterTveY
{
    public partial class MailKontrol : System.Web.UI.Page
    {
        private SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["KabloHE"].ConnectionString);
        string sayfa = "MailKontrol.aspx";
        //PersonelKodlari perkod = new PersonelKodHasan.PersonelKodlari();
        KodT.kodlar tkod = new KodT.kodlar();


        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["KULLANICI_ID"] == null)
            {
                Response.Redirect("Giris.aspx?url=" + sayfa);
            }
            else
            {
                if (!IsPostBack)
                {
                    string rol = tkod.Kul_ROL();
                    if (rol == "ADMIN" || rol == "ALTYAPI")
                    {
                        bekleyenler();
                        tummailler();
                    }
                    else
                    {
                        hata_sayfasi("Erişmek istediğiniz sayfaya yetkiniz bulunmamaktadır. ", "-");
                    }
                }
            }
        }


        private void tummailler()
        {
            conn.Open();
            //SqlDataAdapter sqlAdap = new SqlDataAdapter("Select * from ONLINEZIYARETCILER WHERE zaman > getdate() order by ONLINE.zaman desc", conn);
            SqlDataAdapter sqlAdap = new SqlDataAdapter("Select TOP(100) * FROM MAIL WHERE DURUM='true' ORDER BY ID DESC  ", conn);
            DataTable Ds = new DataTable();
            sqlAdap.Fill(Ds);
            GridView2.DataSource = Ds;
            GridView2.DataBind();
            conn.Close();
            Label4.Text = "Toplam " + GridView1.Rows.Count.ToString() + " kayıt bulunmuştur.";
        }

        private void bekleyenler()
        {

            conn.Open();
            //SqlDataAdapter sqlAdap = new SqlDataAdapter("Select * from ONLINEZIYARETCILER WHERE zaman > getdate() order by ONLINE.zaman desc", conn);
            SqlDataAdapter sqlAdap = new SqlDataAdapter("Select TOP(100) * FROM MAIL WHERE DURUM='false' ORDER BY ID DESC ", conn);
            DataTable Ds = new DataTable();
            sqlAdap.Fill(Ds);
            GridView1.DataSource = Ds;
            GridView1.DataBind();
            conn.Close();
            Label4.Text = "Toplam " + GridView2.Rows.Count.ToString() + " kayıt bulunmuştur.";
        }

        void hata_sayfasi(string mesaj, string user)
        {
            string kullanici = "", dosyaadi;
            dosyaadi = Server.MapPath("~\\Data\\Hata\\" + DateTime.Now.ToShortDateString().Replace(".", "") + "_HataDosyasi.txt");
            if (!File.Exists(dosyaadi))
                File.Create(dosyaadi).Close();

            if (Session["hatalog"] != null)
                Session["hatalog"] = mesaj;
            else
                Session.Add("hatalog", mesaj);

            StreamWriter hd = new StreamWriter(dosyaadi, true);
            //Oluşan hatalar HataDosyasi adlı bir dosyaya kaydediliyor. 
            hd.Write(DateTime.Now.ToString());
            hd.Write(",[");
            hd.Write(Request.ServerVariables["REMOTE_ADDR"].ToString());
            hd.Write("],");
            hd.Write(user);
            hd.Write(",");
            //Server nesnesini GetLastError metodu sunucuda oluşan son hatayı Exception tipinden getirir. Bu da şu an oluşan hata olacaktır. 
            hd.Write(mesaj);

            hd.Write(",");
            hd.Write(Request.RawUrl != null ? Request.RawUrl : "");
            hd.WriteLine();
            hd.Close();

            Server.Transfer("HataOlustu.aspx");
        }
    }
}