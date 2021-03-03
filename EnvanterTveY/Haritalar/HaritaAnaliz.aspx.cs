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
    public partial class HaritaAnaliz : System.Web.UI.Page
    {
        SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["ENTA"].ConnectionString);

        protected void Page_Load(object sender, EventArgs e)
        {
           
        }



    }
}