using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;


using System.IO;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Data.Common;
using System.Web.Script.Services;


namespace bim.Haritalar.HaritaData
{
    /// <summary>
    /// istek için özet açıklama
    /// </summary>
    public class istek : IHttpHandler
    {
        SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["ENTA"].ConnectionString);

        public void ProcessRequest(HttpContext context)
        {
            //context.Response.ContentType = "metin/düz";
            //context.Response.Write("Merhaba Dünya");
            string istek = context.Request["istek"];
            string id = context.Request["id"];
            string filtre = context.Request["filtre"];
            string mesaj = "";

            if (istek == "kmz")
                mesaj = kmz(id,filtre.Replace("*"," "));

            context.Response.Write(mesaj);
        }


        public string kmz(string id,string filtre)
        {
            SqlCommand cmd;
            string sql = "", sql1 = "";

            sql = "SELECT RUHSAT_DOSYALAR.ID2 FROM RUHSAT_RUHSATLAR AS RUHSATLAR " +
                " INNER JOIN RUHSAT_DOSYALAR ON RUHSAT_DOSYALAR.ID=RUHSATLAR.KMZ WHERE RUHSATLAR.KMZ IS NOT NULL " + filtre;
            cmd = new SqlCommand(sql, conn);
            string str = "";
            conn.Open();
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
           
            for (int i=0;i<dt.Rows.Count;i++)
            {
                str += dt.Rows[i][0].ToString()+",";
            }

            //str = str.Substring(0, str.Length - 1);

            conn.Close();

            return str;
        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}