using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

//ok
namespace EnvanterTveY.App_Start
{
    public partial class HataOlustu : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string HataMesaji, EkBilgi, saat, sayfa;
            KodT.kodlar tkod = new KodT.kodlar();

            try
            {
                HataMesaji = Session["hatalog"].ToString();
                Session.Remove("hatalog");
                saat = DateTime.Now.ToString();
                lblhata.Text = HataMesaji;
                lblsaat.Text = saat;
                lblprgramadi.Text = tkod.ayar_al("PROGRAM-ADI");
            }
            catch
            {

            }
        }
    }
}