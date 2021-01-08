using System;
using System.Web;
using System.Web.UI;

//ok
namespace EnvanterTveY
{
    public partial class SiteMaster : MasterPage
    {
        KodT.kodlar tkod = new KodT.kodlar();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["KULLANICI_ID"] == null)
            {
                Response.Redirect("Giris.aspx");
            }

            if(!IsPostBack)
            {
                if (!tkod.admin_yetki())
                {
                    menu_ayarlar.Visible = false;
                    menu_ayarlar_alt.Visible = false;
                    menu_yonetim.Visible = false;
                    menu_yonetim_alt.Visible = false;
                }
            }
            string ip = GetIP();
            lblgiris.Text = Session["KULLANICI_ADI"].ToString() + " (" + ip + ")";
            lnkbutoncikis.Visible = true;
        }

        private string GetIP()
        {
            return Request.ServerVariables["REMOTE_ADDR"].ToString();
        }

        protected void lnkbutoncikis_Click(object sender, EventArgs e)
        {
            try
            {
                HttpContext.Current.Application.Remove("usr_" + Session.SessionID);
                Session.Abandon();
                Response.Redirect("Giris");
            }
            catch
            {

            }
            finally
            {

            }
        }
    }
}