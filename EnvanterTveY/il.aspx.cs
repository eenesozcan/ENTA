using System;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using System.Data;
using System.Data.SqlClient;
using System.Configuration;

//OK
namespace EnvanterTveY
{
    public partial class il : System.Web.UI.Page
    {
        SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["KabloHE"].ConnectionString);
        KodT.kodlar tkod = new KodT.kodlar();
        string sayfa = "il";
        DataTable dt;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                listele();
            }
        }

            protected void grid_il_liste_RowCommand(object sender, GridViewCommandEventArgs e)
            {
                if (e.CommandName.Equals("sil"))
                {
                    int index = Convert.ToInt32(e.CommandArgument);
                    lblidsil.Text = grid_il_liste.DataKeys[index].Value.ToString();
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
                    string id = grid_il_liste.DataKeys[index].Value.ToString();
                    lblidcihaz.Text = id;

                    txtiladi.Text = HttpUtility.HtmlDecode(grid_il_liste.Rows[index].Cells[1].Text);

                    btnilkaydet.Enabled = true;
                    btnilkaydet.CssClass = "btn btn-success ";

                    lblmodalyenibaslik.Text = "İl Güncelle";

                    System.Text.StringBuilder sb = new System.Text.StringBuilder();
                    sb.Append(@"<script type='text/javascript'>");
                    sb.Append("$(\"#Modal_il_kaydet\").modal(\"show\");");
                    sb.Append(@"</script>");
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(),
                               "ModalScript", sb.ToString(), false);
                }
            }

        protected void btnilara_Click(object sender, EventArgs e)
        {
            listele();
        }

                protected void btnilekle_Click(object sender, EventArgs e)
                {
                    System.Text.StringBuilder sb = new System.Text.StringBuilder();
                    sb.Append(@"<script type='text/javascript'>");
                    sb.Append("$(\"#Modal_il_kaydet\").modal(\"show\");");
                    sb.Append(@"</script>");
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(),
                               "ModalScript", sb.ToString(), false);

                    lblidcihaz.Text = "";
                    btnilkaydet.Enabled = true;
                    btnilkaydet.CssClass = "btn btn-success ";

                    lblmodalyenibaslik.Text = "Yeni İl Ekle";
                }

                protected void btnilkaydet_Click(object sender, EventArgs e)
                {
                    try
                    {
                        SqlCommand sorgu = new SqlCommand();
                        sorgu.Connection = conn;
                        conn.Open();

                        if (lblidcihaz.Text == "")
                        {
                            sorgu.CommandText = "SELECT COUNT(*) FROM IL WHERE IL=@ILL ";
                            sorgu.Parameters.AddWithValue("@ILL", txtiladi.Text);
                            int sayi = Convert.ToInt16(sorgu.ExecuteScalar());

                            if (sayi == 0)
                            {
                                string userid = Session["KULLANICI_ID"].ToString();
                                sorgu.CommandText = "INSERT INTO    IL (IL, KAYIT_EDEN , KAYIT_TARIHI)  VALUES(@IL, @UI, getdate() ) ";
                                sorgu.Parameters.AddWithValue("@IL", txtiladi.Text);
                                sorgu.Parameters.AddWithValue("@UI", userid);
                                sorgu.ExecuteNonQuery();
                                ScriptManager.RegisterStartupScript(this, this.GetType(), System.Guid.NewGuid().ToString(), "ShowMessage('İl başarıyla kaydedilmiştir.','Tamam','yeni');", true);
                                ScriptManager.RegisterStartupScript(this, this.GetType(), System.Guid.NewGuid().ToString(), "YeniMesaj('İl başarıyla kaydedilmiştir.','Tamam');", true);

                                btnilkaydet.Enabled = false;
                                btnilkaydet.CssClass = "btn btn-success disabled";
                            }
                            else
                            {
                                ScriptManager.RegisterStartupScript(this, this.GetType(), System.Guid.NewGuid().ToString(), "ShowMessage('Bu İl daha önce kaydedilmişti. Lütfen kontrol ediniz.','Hata','yeni');", true);
                            }
                        }
                        else
                        {
                            string userid = Session["KULLANICI_ID"].ToString();
                            sorgu.CommandText = "UPDATE IL SET IL=@IL_U   WHERE ID=@ID_U ";
                            sorgu.Parameters.AddWithValue("@IL_U", txtiladi.Text);
                            sorgu.Parameters.AddWithValue("@ID_U", lblidcihaz.Text);
                            sorgu.ExecuteNonQuery();
                            ScriptManager.RegisterStartupScript(this, this.GetType(), System.Guid.NewGuid().ToString(), "ShowMessage('İl başarıyla güncellenmiştir.','Tamam','yeni');", true);
                            btnilkaydet.Enabled = false;
                            btnilkaydet.CssClass = "btn btn-success disabled";
                        }
                    }
                    catch (Exception ex)
                    {
                        ScriptManager.RegisterStartupScript(this, this.GetType(), System.Guid.NewGuid().ToString(), "ShowMessage('Hata oluştu :'" + ex.Message.Replace("'", "") + ",'Hata','yeni');", true);
                    }

                    if (ConnectionState.Open == conn.State)
                        conn.Close();
                    listele();
                }

        void listele()
        {
            SqlCommand cmd;
            string sql = "", sql1 = "";

            sql = "SELECT * FROM IL WHERE ID>0  ";

            if (txtadi.Text.Length > 0)
                sql1 += " AND IL LIKE '%" + txtadi.Text + "%'  ";
            sql = sql + sql1;
            cmd = new SqlCommand(sql, conn);
            conn.Open();
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            grid_il_liste.DataSource = dt;
            grid_il_liste.DataBind();
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

                    sorgu.CommandText = "SELECT COUNT(*) FROM BOLGE WHERE IL=@IL14_ID ";
                    sorgu.Parameters.AddWithValue("@IL14_ID", lblidsil.Text);
                    int sayi = Convert.ToInt16(sorgu.ExecuteScalar());

                    if (sayi == 0)
                    {
                        sorgu.CommandText = "DELETE FROM IL WHERE ID=@IL_S";
                        sorgu.Parameters.AddWithValue("@IL_S", lblidsil.Text);
                        sorgu.ExecuteNonQuery();
                        conn.Close();
                        ScriptManager.RegisterStartupScript(this, this.GetType(), System.Guid.NewGuid().ToString(), "ShowMessage('İl başarıyla silinmiştir.','Tamam','sil');", true);
                    }
                    else
                    {
                        tkod.mesaj("Hata", "Bu İl e bağlı Bölge bulunmaktadır.", this);
                        ScriptManager.RegisterStartupScript(this, this.GetType(), System.Guid.NewGuid().ToString(), "ShowMessage('Bu İl e bağlı Bölge bulunmaktadır.','Hata','sil');", true);
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

                listele();
            }
        }
    }
}