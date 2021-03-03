using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Optimization;
using System.Web.Routing;
using System.Web.Security;
using System.Web.SessionState;

using System.Globalization;
using System.Threading;
using System.Net;
using System.Net.Mail;
using System.IO;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Data.Common;

namespace EnvanterTveY
{
    public class MailGondermeServisi
    {
        private Timer threadingTimer;

        KodT.kodlar tkod = new KodT.kodlar();

        public void ServisBaslat()
        {
            if (null == threadingTimer)
            {
                threadingTimer =
                new Timer(new TimerCallback(MailKontrol),
                    null, 0, 1000 * 60 * 2);
            }

        }

        public void ServisDurdur()
        {
            if (null != threadingTimer)
            {
                threadingTimer.Dispose();

            }


        }



        private void MailKontrol(object sender)
        {
            mail_kontrol();

        }

        public void mail_kontrol()
        {
            //mail_gonder("TRK1001", "", 28, 26, 22);
            SqlConnection baglanti2 = new SqlConnection(ConfigurationManager.ConnectionStrings["KabloHE"].ConnectionString);
            baglanti2.Open();
            SqlCommand sorgu = new SqlCommand();
            sorgu.Connection = baglanti2;
            sorgu.CommandText = "select COUNT(*) FROM MAIL WHERE DURUM='FALSE' ";
            int sayi = Convert.ToInt16(sorgu.ExecuteScalar());

            if (sayi > 0)
            {

                sorgu.CommandText = "select KIME,KIMDEN,KONU,ICERIK,ID FROM MAIL WHERE DURUM='FALSE' ";
                SqlDataAdapter da = new SqlDataAdapter(sorgu);
                DataSet ds = new DataSet();
                da.Fill(ds, "MAIL");

                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    try
                    {
                        sorgu.Parameters.Clear();
                        string sonuc = tkod.eposta_gonder(ds.Tables[0].Rows[i][1].ToString(), ds.Tables[0].Rows[i][0].ToString(), ds.Tables[0].Rows[i][2].ToString(), ds.Tables[0].Rows[i][3].ToString());
                        sorgu.CommandText = "UPDATE MAIL SET  DURUM='true',GONDERILME_TARIHI=@TARIH,HATA_KOD='" + sonuc + "' WHERE ID='" + ds.Tables[0].Rows[i][4].ToString() + "' ";
                        sorgu.Parameters.AddWithValue("TARIH", DateTime.Now);
                        sorgu.ExecuteNonQuery();
                    }
                    catch (Exception ex)
                    {
                        sorgu.Parameters.Clear();
                        sorgu.CommandText = "UPDATE MAIL SET  DURUM='true',GONDERILME_TARIHI=@TARIH,HATA_KOD='" + ex.Message.Replace("'", "''") + "' WHERE ID='" + ds.Tables[0].Rows[i][4].ToString() + "' ";
                        sorgu.Parameters.AddWithValue("TARIH", DateTime.Now);
                        sorgu.ExecuteNonQuery();
                    }


                }




            }

            baglanti2.Close();

        }

    }

    public class Global : HttpApplication
    {
        KodT.kodlar tkod = new KodT.kodlar();

        void Application_Start(object sender, EventArgs e)
        {
            // Uygulama başlangıcında çalışan kod
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            MailGondermeServisi mail = new MailGondermeServisi();
            mail.ServisBaslat();
        }

        protected void Application_End(object sender, EventArgs e)
        {
            //Application.Remove("OnlineZiyaretci"); //Burada uygulama sonlandırılmıştır ve OnlineZiyaretci State'imizi siliyoruz.
            MailGondermeServisi mail = new MailGondermeServisi();
            mail.ServisDurdur();
        }

        protected void Application_Error(object sender, EventArgs e)
        {
            Exception exc = Server.GetLastError();

            if (exc is HttpUnhandledException)
            {
                // Pass the error on to the error page.
                if (Server.GetLastError().InnerException != null)
                {
                    if (Session["hatalog"] != null)
                        Session["hatalog"] = Server.GetLastError().InnerException.Message;
                    else
                        Session.Add("hatalog", Server.GetLastError().InnerException.Message);


                    hata_dosyasina_yaz();

                }
                Server.ClearError();
                Response.Redirect("~\\HataOlustu.aspx");
                //Server.Transfer("HataOlustu.aspx");


            }

        }

        public void hata_dosyasina_yaz()
        {

            try
            {

                // Burada loglama, dosyaya yazma, mail gönderme gibi işlemler yapılabilir.
                string kullanici = "", dosyaadi;
                try
                {
                    if (Session["KULLANICI_ID"] == null)
                        kullanici = "";
                    else
                        kullanici = "[" + Session["KULLANICI_ID"].ToString() + " - " + Session["KULLANICI_ADI"].ToString() + "] ";
                }
                catch
                {
                    kullanici = "-";
                }

                dosyaadi = Server.MapPath("~\\Data\\Hata\\" + DateTime.Now.ToString("yyyy.MM.dd") + "_HataDosyasi.txt");
                if (!File.Exists(dosyaadi))
                    File.Create(dosyaadi).Close();

                Exception exc = Server.GetLastError();

                StreamWriter hd = new StreamWriter(dosyaadi, true);
                //Oluşan hatalar HataDosyasi adlı bir dosyaya kaydediliyor. 
                hd.WriteLine(" ");
                hd.WriteLine(" ============================================================== ");
                hd.WriteLine(" ");
                hd.Write(DateTime.Now.ToString());
                hd.Write(",[");
                hd.Write(Request.ServerVariables["REMOTE_ADDR"].ToString());
                hd.Write("],");
                hd.Write(kullanici);
                hd.Write(",");

                try
                {
                    //Server nesnesini GetLastError metodu sunucuda oluşan son hatayı Exception tipinden getirir. Bu da şu an oluşan hata olacaktır. 
                    if (Server.GetLastError().InnerException != null)
                    {
                        hd.Write(Server.GetLastError().InnerException.Message);
                        hd.Write(",");
                        hd.Write(Server.GetLastError().InnerException.StackTrace.ToString());

                    }
                    else
                        hd.Write(Server.GetLastError().Message);

                }
                catch (Exception ex)
                {
                    hd.Write(" Dosya yazma sırasında hata oluştu : " + ex.Message);
                }

                hd.Write(",");
                hd.Write(Request.RawUrl != null ? Request.RawUrl : "");
                hd.WriteLine();
                hd.Close();

            }
            catch (Exception ex)
            {
                //hd.Write(" Dosya yazma sırasında hata oluştu2 : " + ex.Message);
                FileStream fs = new FileStream(Server.MapPath("~\\Data\\Hata\\" + DateTime.Now.ToString("yyyy.MM.dd") + "_HataDosyasi.txt"), FileMode.OpenOrCreate);
                fs.Flush();
                fs.Close();
            }

        }

    }
}