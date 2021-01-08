using System;
using System.Web.UI;

using System.Data.SqlClient;
using System.Configuration;


//ok
namespace EnvanterTveY
{
    public partial class SifreDegistir : System.Web.UI.Page
    {
        SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["KabloHE"].ConnectionString);
        KodT.kodlar tkod = new KodT.kodlar();

        protected void Page_Load(object sender, EventArgs e)
        {
            string sayfa = "SifreDegistir.aspx";
            if (Session["KULLANICI_ID"] == null)
            {
                Response.Redirect("Giris.aspx?url=" + sayfa);
            }
        }
        protected void btnsifreyenile_Click(object sender, EventArgs e)
        {
            degistir();
        }

        void degistir()
        {
            //Sifreleme1 sifreleme = new Sifreleme1();
            string sifrev1 = tkod.TextSifrele(txteskisifree.Text);
            string sifrev2 = tkod.TextSifrele(txtyenisifre1.Text);

            SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["KabloHE"].ConnectionString);
            conn.Open();
            SqlCommand sorgu = new SqlCommand();
            sorgu.Connection = conn;
            sorgu.CommandText = "select count(*) FROM KULLANICI WHERE ID='" + Session["KULLANICI_ID"].ToString() + "' and SIFRE=@SIFRE ";
            sorgu.Parameters.AddWithValue("SIFRE", sifrev1);
            int sayi = Convert.ToInt32(sorgu.ExecuteScalar());

            sorgu.CommandText = "select DURUM FROM KULLANICI WHERE ID='" + Session["KULLANICI_ID"].ToString() + "'";
            Boolean durum = Convert.ToBoolean(sorgu.ExecuteScalar());

            if (sayi == 0)
            {
                lbldurum.Text = "Eski porolanız hatalıdır !";
                lbldurum.CssClass = "label_durum_hata";
            }
            else
            {
                if (durum == true)
                {
                    if(txtyenisifre1.Text != "" )
                    {
                        if (txtyenisifre1.Text == txtyenisifre2.Text)
                        {
                            if (txtyenisifre1.Text != txteskisifree.Text)
                            {
                                sorgu.CommandText = "UPDATE KULLANICI SET SIFRE=@SIFREY   WHERE ID='" + Session["KULLANICI_ID"].ToString() + "'";
                                sorgu.Parameters.AddWithValue("SIFREY", sifrev2);
                                sorgu.ExecuteNonQuery();

                                btnsifreyenile.Enabled = false;
                                btnsifreyenile.CssClass = "btn btn-success disabled";

                                lbldurum.Text = "Şifreniz başarıyla değiştirilmiştir.";
                                lbldurum.CssClass = "text text-success";
                                ScriptManager.RegisterStartupScript(this, this.GetType(), System.Guid.NewGuid().ToString(), "YeniMesaj('Şifreniz başarıyla değiştirilmiştir.','Tamam');", true);

                            }
                            else
                            {
                                lbldurum.Text = "Yeni şifreniz eski şifrenizden farklı olmalıdır!";
                                lbldurum.CssClass = "text text-danger";
                                ScriptManager.RegisterStartupScript(this, this.GetType(), System.Guid.NewGuid().ToString(), "YeniMesaj('Yeni şifreniz eski şifrenizden farklı olmalıdır!','Hata');", true);

                            }
                        }
                        else
                        {
                            lbldurum.Text = "Girilen şifreler uyumsuzdur!";
                            lbldurum.CssClass = "text text-danger";
                            txteskisifree.Text = txteskisifree.Text;
                            ScriptManager.RegisterStartupScript(this, this.GetType(), System.Guid.NewGuid().ToString(), "YeniMesaj('Girilen şifreler uyumsuzdur!','Hata');", true);
                        }
                    }
                    else
                    {
                        lbldurum.Text = "Yeni şifre alanı boş geçilemez!";
                        lbldurum.CssClass = "text text-danger";
                        ScriptManager.RegisterStartupScript(this, this.GetType(), System.Guid.NewGuid().ToString(), "YeniMesaj('Yeni şifre alanı boş geçilemez!','Hata');", true);
                    }
                }
                else
                {
                    lbldurum.Text = "Hesabınız Kapalıdır!";
                    ScriptManager.RegisterStartupScript(this, this.GetType(), System.Guid.NewGuid().ToString(), "YeniMesaj('Hesabınız Kapalıdır!','Hata');", true);
                }
            }
        }
    }
}