using System;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using System.Data;
using System.Data.SqlClient;
using System.Configuration;

//ok
namespace EnvanterTveY
{
    public partial class SevkAyar : System.Web.UI.Page
    {
        SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["KabloHE"].ConnectionString);
        string sayfa = "SevkAyar";
        KodT.kodlar tkod = new KodT.kodlar();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                listele_sevkdurum();
            }
        }
        protected void btnsil_Click(object sender, EventArgs e)
        {
            if (lblislem.Text == "sevkdurum-sil")
            {
                try
                {
                    SqlCommand sorgu = new SqlCommand();
                    sorgu.Connection = conn;
                    conn.Open();

                    sorgu.CommandText = "SELECT COUNT(*) FROM MALZEME_SEVK WHERE SEVK_DURUM=@SEVKDURUM9_ID ";
                    sorgu.Parameters.AddWithValue("@SEVKDURUM9_ID", lblidsil.Text);
                    int sayi = Convert.ToInt16(sorgu.ExecuteScalar());

                    if (sayi == 0)
                    {
                        sorgu.CommandText = "DELETE FROM MALZEME_SEVK_DURUM WHERE ID=" + lblidsil.Text;
                        sorgu.ExecuteNonQuery();
                        conn.Close();
                        ScriptManager.RegisterStartupScript(this, this.GetType(), System.Guid.NewGuid().ToString(), "ShowMessage('Sevk Durum başarıyla silinmiştir.','Tamam','sil');", true);
                    }
                    else
                    {
                        tkod.mesaj("Hata", "Bu sevk durumu kullanılmaktadır.", this);
                        ScriptManager.RegisterStartupScript(this, this.GetType(), System.Guid.NewGuid().ToString(), "ShowMessage('Bu sevk durumu kullanılmaktadır.','Hata','sil');", true);
                    }
                }
                catch (Exception ex)
                {
                    tkod.mesaj("Hata", "Hata oluştu!", this);
                    ScriptManager.RegisterStartupScript(this, this.GetType(), System.Guid.NewGuid().ToString(), "ShowMessage('Hata oluştu!','Hata','sil');", true);
                }
                conn.Close();

                btnsil.Enabled = false;
                btnsil.CssClass = "btn btn-danger disabled";

                listele_sevkdurum();
            }
        }

        void listele_sevkdurum()
        {
            SqlCommand cmd;
            string sql = " ", sql1 = " ";

            sql = "SELECT * FROM MALZEME_SEVK_DURUM  WHERE ID>0 ";

            sql = sql + sql1;
            cmd = new SqlCommand(sql, conn);
            conn.Open();
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            grid_sevkdurum_liste.DataSource = dt;
            grid_sevkdurum_liste.DataBind();
            conn.Close();
        }

        protected void btnsevkdurumekle_Click(object sender, EventArgs e)
        {
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            sb.Append(@"<script type='text/javascript'>");
            sb.Append("$(\"#Modal_SevkDurum_Kaydet\").modal(\"show\");");
            sb.Append(@"</script>");
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(),
                       "ModalScript", sb.ToString(), false);

            lblidcihaz.Text = "";
            txtsevkdurum.Text = "";
            btnsevkdurumkaydet.Enabled = true;
            btnsevkdurumkaydet.CssClass = "btn btn-success ";

            lblmodalyenibaslik.Text = "Yeni Sevk Durum Ekle";
        }

        protected void grid_sevkdurum_liste_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName.Equals("sil"))
            {
                int index = Convert.ToInt32(e.CommandArgument);
                lblidsil.Text = grid_sevkdurum_liste.DataKeys[index].Value.ToString();
                lblislem.Text = "sevkdurum-sil";
                btnsil.Enabled = true;
                btnsil.CssClass = "btn btn-danger ";

                System.Text.StringBuilder sb = new System.Text.StringBuilder();
                sb.Append(@"<script type='text/javascript'>");
                sb.Append("$(\"#ModalSil\").modal(\"show\");");
                sb.Append(@"</script>");
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(),
                           "ModalScript", sb.ToString(), false);
            }

            if (e.CommandName.Equals("guncelle"))
            {
                int index = Convert.ToInt32(e.CommandArgument);
                string id = grid_sevkdurum_liste.DataKeys[index].Value.ToString();
                lblidcihaz.Text = id;

                txtsevkdurum.Text = HttpUtility.HtmlDecode(grid_sevkdurum_liste.Rows[index].Cells[1].Text);

                btnsevkdurumkaydet.Enabled = true;
                btnsevkdurumkaydet.CssClass = "btn btn-success ";

                lblmodalyenibaslik.Text = "Sevk Durum Güncelle";

                System.Text.StringBuilder sb = new System.Text.StringBuilder();
                sb.Append(@"<script type='text/javascript'>");
                sb.Append("$(\"#Modal_SevkDurum_Kaydet\").modal(\"show\");");
                sb.Append(@"</script>");
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(),
                           "ModalScript", sb.ToString(), false);
            }
        }

        protected void btnsevkdurumkaydet_Click(object sender, EventArgs e)
        {
            try
            {
                SqlCommand sorgu = new SqlCommand();
                sorgu.Connection = conn;
                conn.Open();

                if (lblidcihaz.Text == "")
                {
                    sorgu.CommandText = "SELECT COUNT(*) FROM MALZEME_SEVK_DURUM WHERE S_DURUM='" + txtsevkdurum.Text + "' ";
                    int sayi = Convert.ToInt16(sorgu.ExecuteScalar());

                    if (sayi == 0)
                    {
                        string userid = Session["KULLANICI_ID"].ToString();
                        sorgu.CommandText = "INSERT INTO MALZEME_SEVK_DURUM (S_DURUM, KAYIT_EDEN , KAYIT_TARIHI)  VALUES('" + txtsevkdurum.Text + "','" + userid + "', getdate() ) ";
                        sorgu.ExecuteNonQuery();
                        ScriptManager.RegisterStartupScript(this, this.GetType(), System.Guid.NewGuid().ToString(), "ShowMessage('Sevk Durum bilgisi başarıyla kaydedilmiştir..','Tamam','yeni');", true);

                        btnsevkdurumkaydet.Enabled = false;
                        btnsevkdurumkaydet.CssClass = "btn btn-success disabled";
                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(this, this.GetType(), System.Guid.NewGuid().ToString(), "ShowMessage('Bu Sevk Durumu daha önce kaydedilmişti. Lütfen kontrol ediniz.','Hata','yeni');", true);
                    }
                }
                else
                {
                    string userid = Session["KULLANICI_ID"].ToString();
                    sorgu.CommandText = "UPDATE MALZEME_SEVK_DURUM SET S_DURUM='" + txtsevkdurum.Text + "' WHERE ID='" + lblidcihaz.Text + "' ";
                    sorgu.ExecuteNonQuery();
                    ScriptManager.RegisterStartupScript(this, this.GetType(), System.Guid.NewGuid().ToString(), "ShowMessage('Sevk Durumu başarıyla güncellenmiştir.','Tamam','yeni');", true);
                    btnsevkdurumkaydet.Enabled = false;
                    btnsevkdurumkaydet.CssClass = "btn btn-success disabled";
                }
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), System.Guid.NewGuid().ToString(), "ShowMessage('Hata oluştu :'" + ex.Message.Replace("'", "") + ",'Hata','yeni');", true);
            }

            if (ConnectionState.Open == conn.State)
                conn.Close();
            listele_sevkdurum();
        }
    }
}