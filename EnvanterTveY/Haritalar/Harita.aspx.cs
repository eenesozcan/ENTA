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

namespace bim.Haritalar
{
    public partial class Harita : System.Web.UI.Page
    {
        SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["ENTA"].ConnectionString);

        protected void Page_Load(object sender, EventArgs e)
        {
            string islem, id;
            id = Request.QueryString["id"];




            if (id == null || id == "")
            {

                Response.Redirect("../Default.aspx");
            }
            else
            {

                SqlCommand sorgu = new SqlCommand();
                sorgu.Connection = conn;
                conn.Open();
                //sorgu.CommandText = "select KMZ FROM KML WHERE ID2='" + id + "'";
                //lblkmlfile.Text = "" + sorgu.ExecuteScalar().ToString() + "";

                sorgu.CommandText = "select URL FROM RUHSAT_DOSYALAR WHERE ID2='" + id + "'";
                string url = sorgu.ExecuteScalar().ToString();
                //lblkml.Text = "dosyalar/kml/"+Path.GetFileName(url);
                //lblkml.Text = "" + sorgu.ExecuteScalar().ToString() + "";


                conn.Close();


                //lblkmlfile.Text = Path.GetFileName(url);
                //lblkml.Text = "http://" + Request.ServerVariables["HTTP_HOST"] + "/Download/indir?id=" + id + "";
                //lblkml.Text = url;
                frmharita.Attributes["src"] = "Harita.html?kmzurl=" +  id + "&kmzfile="+ Path.GetFileName(url); 

            }

        }
    }
}