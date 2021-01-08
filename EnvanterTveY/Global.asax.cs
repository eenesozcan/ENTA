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
   
    public class Global : HttpApplication
    {
        void Application_Start(object sender, EventArgs e)
        {
            // Uygulama başlangıcında çalışan kod
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }

        protected void Application_End(object sender, EventArgs e)
        {
        }

        protected void Application_Error(object sender, EventArgs e)
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
                //Server nesnesini GetLastError metodu sunucuda oluşan son hatayı Exception tipinden getirir. Bu da şu an oluşan hata olacaktır. 
                if (Server.GetLastError().InnerException != null)
                {
                    hd.Write(Server.GetLastError().InnerException.Message);
                    hd.Write(",");
                    hd.Write(Server.GetLastError().InnerException.StackTrace.ToString());
                    if (Session["hatalog"] != null)
                        Session["hatalog"] = Server.GetLastError().InnerException.Message;
                    else
                        Session.Add("hatalog", Server.GetLastError().InnerException.Message);
                }
                else
                    hd.Write(Server.GetLastError().Message);

                hd.Write(",");
                hd.Write(Request.RawUrl != null ? Request.RawUrl : "");
                hd.WriteLine();
                hd.Close();

                // Handle HTTP errors
                if (exc.GetType() == typeof(System.Web.HttpUnhandledException))
                {
                    if (exc.Message.Contains("NoCatch") || exc.Message.Contains("maxUrlLength"))
                        return;
                }

                //Server.Transfer("..\\..\\HataOlustu.aspx");

                Server.ClearError();
            }
            catch (Exception ex)
            {
                return;
            }
            finally
            {

            }
        }
    }
}