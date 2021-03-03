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
    public partial class Download : System.Web.UI.Page
    {
        SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["ENTA"].ConnectionString);

        protected void Page_Load(object sender, EventArgs e)
        {
            string islem, id;
            islem = Request.QueryString["islem"];
            id = Request.QueryString["id"];




            if (id == null || id == "")
            {

                Response.Redirect("Default.aspx");
            }
            else
            {

                SqlCommand sorgu = new SqlCommand();
                sorgu.Connection = conn;
                conn.Open();

                sorgu.CommandText = "select D.url FROM RUHSAT_DOSYALAR AS D  WHERE D.ID2='" + id + "'"; // and IP='" + ip_adres + "'";
                string url = Convert.ToString(sorgu.ExecuteScalar());

                /*
                if (url == null || url == "")
                {
                    sorgu.CommandText = "select K.url FROM KML AS K  WHERE K.ID2='" + id + "'"; // and IP='" + ip_adres + "'";
                    url = Convert.ToString(sorgu.ExecuteScalar());

                }
                */

                //string filename = Path.GetFileName(url);
                //string url2 = yol + filename;

                conn.Close();



                FileInfo file = new FileInfo(url);

                if (file.Exists)
                {

                    Response.ClearContent();
                    Response.AddHeader("Content-Disposition", "attachment; filename=" + Path.GetFileName(url).Replace(" ", "_").Replace(",", "_"));
                    //Response.AddHeader("Content-Length", file.Length.ToString());
                    Response.ContentType = GetFileExtension(file.Extension.ToLower());
                    Response.TransmitFile(url);
                    Response.End();
                }
                else
                {
                    Response.ClearContent();
                    //Response.AddHeader("Content-Disposition", "attachment; filename=" + Path.GetFileName(url2));
                    //Response.AddHeader("Content-Length", file.Length.ToString());
                    //Response.ContentType = GetFileExtension(file.Extension.ToLower());
                    //Response.TransmitFile(url2);
                    Response.Write("Dosya bulunamadı");
                    Response.End();
                }



            }



        }

        private string GetFileExtension(string fileExtension)
        {
            switch (fileExtension)
            {
                case ".htm":
                case ".html":
                case ".log":
                    return "text/HTML";
                case ".txt":
                    return "text/plain";
                case ".doc":
                    return "application/ms-word";
                case ".tiff":
                case ".tif":
                    return "image/tiff";
                case ".asf":
                    return "video/x-ms-asf";
                case ".avi":
                    return "video/avi";
                case ".zip":
                    return "application/zip";
                case ".xls":
                case ".csv":
                    return "application/vnd.ms-excel";
                case ".gif":
                    return "image/gif";
                case ".jpg":
                case "jpeg":
                    return "image/jpeg";
                case ".bmp":
                    return "image/bmp";
                case ".wav":
                    return "audio/wav";
                case ".mp3":
                    return "audio/mpeg3";
                case ".mpg":
                case "mpeg":
                    return "video/mpeg";
                case ".rtf":
                    return "application/rtf";
                case ".asp":
                    return "text/asp";
                case ".pdf":
                    return "application/pdf";
                case ".fdf":
                    return "application/vnd.fdf";
                case ".ppt":
                    return "application/mspowerpoint";
                case ".dwg":
                    return "image/vnd.dwg";
                case ".msg":
                    return "application/msoutlook";
                case ".7z":
                    return "application/zip";
                case ".xml":
                case ".kml":
                case ".sdxl":
                    return "application/xml";


                case ".xdp":
                    return "application/vnd.adobe.xdp+xml";
                default:
                    return "application/octet-stream";
            }
        }
    }
}