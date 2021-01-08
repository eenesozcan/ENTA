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
    public partial class Bolge : System.Web.UI.Page
    {
        SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["KabloHE"].ConnectionString);
        string sayfa = "Bolge";
        KodT.kodlar tkod = new KodT.kodlar();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                listele_bolge();
            }
        }

        protected void grid_bolge_liste_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName.Equals("sil"))
            {
                int index = Convert.ToInt32(e.CommandArgument);
                lblidsil.Text = grid_bolge_liste.DataKeys[index].Value.ToString();
                lblislem.Text = "cihaz-sil";
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
                string id = grid_bolge_liste.DataKeys[index].Value.ToString();
                lblidcihaz.Text = id;

                comil.SelectedIndex = comil.Items.IndexOf(comil.Items.FindByText(HttpUtility.HtmlDecode(grid_bolge_liste.Rows[index].Cells[1].Text)));
                txtbolgeadi.Text = HttpUtility.HtmlDecode(grid_bolge_liste.Rows[index].Cells[2].Text.Trim());

                btnbolgekaydet.Enabled = true;
                btnbolgekaydet.CssClass = "btn btn-success ";

                lblmodalyenibaslik.Text = "Bölge Güncelle";

                System.Text.StringBuilder sb = new System.Text.StringBuilder();
                sb.Append(@"<script type='text/javascript'>");
                sb.Append("$(\"#Modal_bolge_kaydet\").modal(\"show\");");
                sb.Append(@"</script>");
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(),
                           "ModalScript", sb.ToString(), false);
            }
        }

        void listele_bolge()
        {
            SqlCommand cmd;
            string sql = "", sql1 = "";

            sql = "SELECT BOLGE.ID,BOLGE.BOLGE_ADI, IL.IL FROM BOLGE INNER JOIN IL ON IL.ID=BOLGE.IL WHERE BOLGE.ID>0  ";

            if (txtbadi.Text.Length > 0)

                sql1 += " AND BOLGE_ADI LIKE '%" + txtbadi.Text + "%'  ";
            sql = sql + sql1;
            cmd = new SqlCommand(sql, conn);
            conn.Open();
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            grid_bolge_liste.DataSource = dt;
            grid_bolge_liste.DataBind();
            conn.Close();
        }

        protected void btnsil_Click(object sender, EventArgs e)
        {
            if (lblislem.Text == "cihaz-sil")
            {
                try
                {
                    SqlCommand sorgu = new SqlCommand();
                    sorgu.Connection = conn;
                    conn.Open();

                    sorgu.CommandText = "SELECT COUNT(*) FROM ADRES_ILCE WHERE BOLGE_ID=@BOLILVS14_ID ";
                    sorgu.Parameters.AddWithValue("@BOLILVS14_ID", lblidsil.Text);
                    int sayi = Convert.ToInt16(sorgu.ExecuteScalar());

                    sorgu.CommandText = "SELECT COUNT(*) FROM DEPO WHERE BOLGE_ID=@BOLILVS15_ID ";
                    sorgu.Parameters.AddWithValue("@BOLILVS15_ID", lblidsil.Text);
                    int sayi1 = Convert.ToInt16(sorgu.ExecuteScalar());

                    if (sayi == 0 && sayi1 == 0)
                    {
                        sorgu.CommandText = "DELETE FROM BOLGE WHERE ID=@ID";
                        sorgu.Parameters.AddWithValue("@ID", lblidsil.Text);
                        sorgu.ExecuteNonQuery();
                        conn.Close();
                        ScriptManager.RegisterStartupScript(this, this.GetType(), System.Guid.NewGuid().ToString(), "ShowMessage('Bölge başarıyla silinmiştir.','Tamam','sil');", true);
                    }
                    else
                    {
                        tkod.mesaj("Hata", "Bu Bölgeye bağlı İlçe veya Depo bulunmaktadır.", this);
                        ScriptManager.RegisterStartupScript(this, this.GetType(), System.Guid.NewGuid().ToString(), "ShowMessage('Bu Bölgeye bağlı İlçe veya Depo bulunmaktadır.','Hata','sil');", true);
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

                listele_bolge();
            }
        }

        protected void btnbolgeekle_Click(object sender, EventArgs e)
        {
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            sb.Append(@"<script type='text/javascript'>");
            sb.Append("$(\"#Modal_bolge_kaydet\").modal(\"show\");");
            sb.Append(@"</script>");
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(),
                       "ModalScript", sb.ToString(), false);

            lblidcihaz.Text = "";
            btnbolgekaydet.Enabled = true;
            btnbolgekaydet.CssClass = "btn btn-success ";

            lblmodalyenibaslik.Text = "Yeni Bölge Ekle";
            txtbolgeadi.Text = "";
            comil.SelectedIndex = 0;
        }

        protected void btnbolgeara_Click(object sender, EventArgs e)
        {
            listele_bolge();
        }

        protected void btnbolgekaydet_Click(object sender, EventArgs e)
        {
            try
            {
                SqlCommand sorgu = new SqlCommand();
                sorgu.Connection = conn;
                conn.Open();

                if (lblidcihaz.Text == "")
                {
                    sorgu.CommandText = "SELECT COUNT(*) FROM BOLGE WHERE BOLGE_ADI=@BADI ";
                    sorgu.Parameters.AddWithValue("BADI", txtbolgeadi.Text);
                    int sayi = Convert.ToInt16(sorgu.ExecuteScalar());

                    if (sayi == 0)
                    {
                        string userid = Session["KULLANICI_ID"].ToString();
                        sorgu.CommandText = "INSERT INTO BOLGE (BOLGE_ADI, IL, KAYIT_EDEN, KAYIT_TARIHI)  VALUES(@BADI1, @IL, '" + userid + "', getdate() ) ";
                        sorgu.Parameters.AddWithValue("@BADI1", txtbolgeadi.Text);
                        sorgu.Parameters.AddWithValue("@IL", comil.SelectedValue.ToString());

                        sorgu.ExecuteNonQuery();
                        ScriptManager.RegisterStartupScript(this, this.GetType(), System.Guid.NewGuid().ToString(), "ShowMessage('Bölge başarıyla kaydedilmiştir..','Tamam','yeni');", true);

                        btnbolgekaydet.Enabled = false;
                        btnbolgekaydet.CssClass = "btn btn-success disabled";
                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(this, this.GetType(), System.Guid.NewGuid().ToString(), "ShowMessage('Bu Bölge daha önce kaydedilmişti. Lütfen kontrol ediniz.','Hata','yeni');", true);
                    }
                }
                else
                {
                    string userid = Session["KULLANICI_ID"].ToString();
                    sorgu.CommandText = "UPDATE BOLGE SET IL='" + comil.SelectedValue + "', BOLGE_ADI='" + txtbolgeadi.Text + "' WHERE ID = " + lblidcihaz.Text + " ";

                    sorgu.ExecuteNonQuery();
                    ScriptManager.RegisterStartupScript(this, this.GetType(), System.Guid.NewGuid().ToString(), "ShowMessage('Bölge başarıyla güncellenmiştir.','Tamam','yeni');", true);
                    btnbolgekaydet.Enabled = false;
                    btnbolgekaydet.CssClass = "btn btn-success disabled";
                }
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), System.Guid.NewGuid().ToString(), "ShowMessage('Hata oluştu :'" + ex.Message.Replace("'", "") + ",'Hata','yeni');", true);
            }

            if (ConnectionState.Open == conn.State)
                conn.Close();
            listele_bolge();
        }
    }
}