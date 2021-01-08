using System;
using System.Web.UI.WebControls;

using System.IO;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;

//ok
namespace EnvanterTveY
{
    public partial class Giris : System.Web.UI.Page
    {
        SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["KabloHE"].ConnectionString);
        KodT.kodlar tkod = new KodT.kodlar();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["KULLANICI_ID"] != null)
            {
                Response.Redirect("Default.aspx");

            }             
                  lblprogramadi.Text = tkod.ayar_al("PROGRAM-ADI");
                  this.Title = lblprogramadi.Text;
                  giris_hazirla();             
        }

        void giris_hazirla()
        {
            string url = Request.QueryString["url"];
            string adres = Request.Url.ToString();


            if (!IsPostBack)
                Panel_captcha.Style.Add("display", "none");

            if (SSL_kontrol() == true)
            {
                string adresb = adres.Substring(0, 5);
                if (adresb == "http:")
                    Response.Redirect(adres.Substring(0, 4) + "s" + adres.Substring(4, adres.Length - 4));

            }
            txtkullaniciadi.Focus();

            if (Session["hataligiris"] != null)
            {
                int hata = Convert.ToInt16(Session["hataligiris"].ToString());
                if (hata > 2)
                    Panel_captcha.Style.Add("display", "block");
            }
        }

        public bool SSL_kontrol()
        {
            if (conn.State == ConnectionState.Closed)
                conn.Open();
            SqlCommand sorgu = new SqlCommand();
            sorgu.Connection = conn;
            sorgu.CommandText = "select DEGERB FROM AYARLAR WHERE TIP='SSL' ";
            bool ssl = Convert.ToBoolean(sorgu.ExecuteScalar());
            conn.Close();

            return ssl;
        }

        void giris()
        {
            string sifrev1 = tkod.TextSifrele(txtsifre.Text);

            string url = Request.QueryString["url"];
            SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["KabloHE"].ConnectionString);
            conn.Open();
            SqlCommand sorgu = new SqlCommand();
            sorgu.Connection = conn;
            sorgu.CommandText = "select count(*) FROM KULLANICI WHERE KULLANICI_ADI=@KID ";
            sorgu.Parameters.AddWithValue("@KID", txtkullaniciadi.Text);
            int sayi = Convert.ToInt32(sorgu.ExecuteScalar());

            if (sayi == 0)
            {
                lblgirisdurum.Text = "Kullanıcı adı ve/veya şifre hatalıdır !";
                hatali_giris();
            }
            else
            {
                sorgu.CommandText = "select count(*) FROM KULLANICI WHERE KULLANICI_ADI=@KADI and SIFRE='" + sifrev1 + "'";
                sorgu.Parameters.AddWithValue("@KADI", txtkullaniciadi.Text);
                int sayi2 = Convert.ToInt32(sorgu.ExecuteScalar());

                if (sayi2 == 0)
                {
                    lblgirisdurum.Text = "Kullanıcı adı ve/veya şifre hatalıdır !";
                    hatali_giris();
                }
                else
                {
                    sorgu.CommandText = "select DURUM FROM KULLANICI WHERE KULLANICI_ADI=@KADI1 and SIFRE='" + sifrev1 + "'";
                    sorgu.Parameters.AddWithValue("@KADI1", txtkullaniciadi.Text);
                    Boolean durum = Convert.ToBoolean(sorgu.ExecuteScalar());

                    if (durum == true)
                    {
                        sorgu.CommandText = "select ID FROM KULLANICI WHERE KULLANICI_ADI=@KADI1  And SIFRE='" + sifrev1 + "' ";
                        string id = Convert.ToString(sorgu.ExecuteScalar());

                        user_login2(id);
                    }
                    else
                    {
                        lblgirisdurum.Text = "Kullanıcı adı ve/veya şifre hatalıdır !";
                        hatali_giris();
                    }
                }
            }
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


            //https://bidb.itu.edu.tr/seyir-defteri/blog/2013/09/06/global.asax-dosyas%C4%B1
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

        public void hatali_giris()
        {
            if (Session["hataligiris"] == null)
                Session.Add("hataligiris", "1");
            else
            {
                int hata = Convert.ToInt16(Session["hataligiris"].ToString());
                hata++;
                Session["hataligiris"] = Convert.ToInt16(hata);
                if (hata > 2)
                    Panel_captcha.Style.Add("display", "block");
            }
        }

        protected void ValidateCaptcha(object sender, ServerValidateEventArgs e)
        {
            Captcha1.ValidateCaptcha(txtCaptcha.Text.Trim());
            e.IsValid = Captcha1.UserValidated;
            if (e.IsValid)
            {
                //ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('Valid Captcha!');", true);
                lblgirisdurum.Text = "Kod hatalıdır";
            }
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            int hata = 0;
            if (Session["hataligiris"] != null)
            {
                hata = Convert.ToInt16(Session["hataligiris"].ToString());
                if (hata > 2)
                {
                    Panel_captcha.Style.Add("display", "block");
                    Captcha1.ValidateCaptcha(txtCaptcha.Text.Trim());
                    if (Captcha1.UserValidated)
                        giris();
                    else
                        lblgirisdurum.Text = "Kod hatalıdır";
                }
                else
                    giris();
            }
            else
                giris();
        }

        public void user_login2(string id)
        {
            {
                string url = Request.QueryString["url"];
                SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["KabloHE"].ConnectionString);
                conn.Open();
                SqlCommand sorgu = new SqlCommand();
                sorgu.Connection = conn;


                sorgu.CommandText = "select ISIM FROM KULLANICI WHERE ID=" + id;
                string isim = Convert.ToString(sorgu.ExecuteScalar());
                sorgu.CommandText = "select R.ROL FROM KULLANICI AS K INNER JOIN ROL AS R ON R.ID=K.ROL_ID WHERE K.ID='" + id + "' ";
                string rol = Convert.ToString(sorgu.ExecuteScalar());
                sorgu.CommandText = "select R.ID FROM KULLANICI AS K INNER JOIN ROL AS R ON R.ID=K.ROL_ID WHERE K.ID='" + id + "' ";
                string rol_id = Convert.ToString(sorgu.ExecuteScalar());

                Session.Add("KULLANICI_ADI", isim);
                Session.Add("KULLANICI_ID", id);
                Session.Add("KULLANICI_ROL", rol);
                Session.Add("KULLANICI_ROL_ID", rol_id);

                String tarih = Convert.ToString(DateTime.Now);

                if (url != null)
                    Response.Redirect(url);
                else
                    Response.Redirect("Default.aspx");
            }
        }
    }
}