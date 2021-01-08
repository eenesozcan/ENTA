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
    public partial class Depom : System.Web.UI.Page
    {
            SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["KabloHE"].ConnectionString);
            string sayfa = "Depom";
            DataTable dt;
            KodT.kodlar tkod = new KodT.kodlar();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["KULLANICI_ID"] == null)
            {
                Response.Redirect("Giris.aspx?url=" + sayfa);
            }
            if (!IsPostBack)
            {
                listele_depo();
            }
        }
        protected void btndepoekle_Click(object sender, EventArgs e)
        {
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            sb.Append(@"<script type='text/javascript'>");
            sb.Append("$(\"#Modal_depo_kaydet\").modal(\"show\");");
            sb.Append(@"</script>");
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(),
                       "ModalScript", sb.ToString(), false);

            lblidcihaz.Text = "";
            btndepokaydet.Enabled = true;
            btndepokaydet.CssClass = "btn btn-success ";

            lblmodalyenibaslik.Text = "Yeni Depo Ekle";
            txtdepoadi.Text = "";
            combolge.SelectedIndex = 0;
            comdepoturu.SelectedIndex = 0;

            Bolge_Listele();
            Depo_Tanimi_Listele();
        }

        protected void btndepoara_Click(object sender, EventArgs e)
        {
            listele_depo();
        }

        protected void grid_depo_liste_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName.Equals("sil"))
            {
                int index = Convert.ToInt32(e.CommandArgument);
                lblidsil.Text = grid_depo_liste.DataKeys[index].Value.ToString();
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
                Bolge_Listele();
                Depo_Tanimi_Listele();
                Depo_Turu_Listele();

                int index = Convert.ToInt32(e.CommandArgument);
                string id = grid_depo_liste.DataKeys[index].Value.ToString();
                lblidcihaz.Text = id;

                combolge.SelectedIndex = combolge.Items.IndexOf(combolge.Items.FindByText(HttpUtility.HtmlDecode(grid_depo_liste.Rows[index].Cells[1].Text)));
                txtdepoadi.Text = HttpUtility.HtmlDecode(grid_depo_liste.Rows[index].Cells[2].Text.Trim());
                comdepoturu.SelectedIndex = comdepoturu.Items.IndexOf(comdepoturu.Items.FindByText(HttpUtility.HtmlDecode(grid_depo_liste.Rows[index].Cells[4].Text)));
                comdepotanimi.SelectedIndex = comdepotanimi.Items.IndexOf(comdepotanimi.Items.FindByText(HttpUtility.HtmlDecode(grid_depo_liste.Rows[index].Cells[3].Text)));

                btndepokaydet.Enabled = true;
                btndepokaydet.CssClass = "btn btn-success ";

                lblmodalyenibaslik.Text = "Depo Güncelle";

                System.Text.StringBuilder sb = new System.Text.StringBuilder();
                sb.Append(@"<script type='text/javascript'>");
                sb.Append("$(\"#Modal_depo_kaydet\").modal(\"show\");");
                sb.Append(@"</script>");
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(),
                           "ModalScript", sb.ToString(), false);
            }
        }

        protected void btndepokaydet_Click(object sender, EventArgs e)
        {
            try
            {
                SqlCommand sorgu = new SqlCommand();
                sorgu.Connection = conn;
                conn.Open();

                    if (lblidcihaz.Text == "")
                    {
                        sorgu.CommandText = "SELECT COUNT(*) FROM DEPO WHERE DEPO=@DP ";
                        sorgu.Parameters.AddWithValue("DP", txtdepoadi.Text);
                        int sayi = Convert.ToInt16(sorgu.ExecuteScalar());

                        if (sayi == 0)
                        {
                            string userid = Session["KULLANICI_ID"].ToString();
                            sorgu.CommandText = "INSERT INTO DEPO (DEPO, BOLGE_ID, DEPOTURU_ID, DEPOTANIMI_ID, KAYIT_EDEN, KAYIT_TARIHI)  VALUES(@DADI, @DBOLGE, @DTUR, @DTANIM, @UI, getdate() ) ";
                            sorgu.Parameters.AddWithValue("@DADI", txtdepoadi.Text);
                            sorgu.Parameters.AddWithValue("@DBOLGE", combolge.SelectedValue);
                            sorgu.Parameters.AddWithValue("@DTUR", comdepoturu.SelectedValue);
                            sorgu.Parameters.AddWithValue("@DTANIM", comdepotanimi.SelectedValue);
                            sorgu.Parameters.AddWithValue("@UI", userid);

                            sorgu.ExecuteNonQuery();
                            ScriptManager.RegisterStartupScript(this, this.GetType(), System.Guid.NewGuid().ToString(), "YeniMesaj('Depo başarıyla kaydedilmiştir.','Tamam');", true);
                            ScriptManager.RegisterStartupScript(this, this.GetType(), System.Guid.NewGuid().ToString(), "ShowMessage('Depo başarıyla kaydedilmiştir.','Tamam','yeni');", true);

                            btndepokaydet.Enabled = false;
                            btndepokaydet.CssClass = "btn btn-success disabled";
                        }
                        else
                        {
                            ScriptManager.RegisterStartupScript(this, this.GetType(), System.Guid.NewGuid().ToString(), "YeniMesaj('Bu Depo daha önce kaydedilmişti. Lütfen kontrol ediniz.','Hata');", true);
                            ScriptManager.RegisterStartupScript(this, this.GetType(), System.Guid.NewGuid().ToString(), "ShowMessage('Bu Depo daha önce kaydedilmişti. Lütfen kontrol ediniz.','Hata','yeni');", true);
                        }
                    }

                    else
                    {
                        string userid = Session["KULLANICI_ID"].ToString();
                        sorgu.CommandText = "UPDATE DEPO SET BOLGE_ID=@DBOLGE, DEPO=@DADI, DEPOTURU_ID=@DTUR, DEPOTANIMI_ID=@DTANIM WHERE ID =@LIDC ";
                        sorgu.Parameters.AddWithValue("@DADI", txtdepoadi.Text);
                        sorgu.Parameters.AddWithValue("@DBOLGE", combolge.SelectedValue);
                        sorgu.Parameters.AddWithValue("@DTUR", comdepoturu.SelectedValue);
                        sorgu.Parameters.AddWithValue("@DTANIM", comdepotanimi.SelectedValue);
                        sorgu.Parameters.AddWithValue("@UI", userid);
                        sorgu.Parameters.AddWithValue("@LIDC", lblidcihaz.Text);
                        sorgu.ExecuteNonQuery();

                        ScriptManager.RegisterStartupScript(this, this.GetType(), System.Guid.NewGuid().ToString(), "YeniMesaj('Depo başarıyla güncellenmiştir.','Tamam');", true);
                        ScriptManager.RegisterStartupScript(this, this.GetType(), System.Guid.NewGuid().ToString(), "ShowMessage('Depo başarıyla güncellenmiştir.','Tamam','yeni');", true);
                        btndepokaydet.Enabled = false;
                        btndepokaydet.CssClass = "btn btn-success disabled";
                    }
                

                

            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), System.Guid.NewGuid().ToString(), "YeniMesaj('Beklenmeyen bir hata oluştu.','Hata');", true);
                ScriptManager.RegisterStartupScript(this, this.GetType(), System.Guid.NewGuid().ToString(), "ShowMessage('Hata oluştu :'" + ex.Message.Replace("'", "") + ",'Hata','yeni');", true);
            }

            if (ConnectionState.Open == conn.State)
                conn.Close();
            listele_depo();

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

                    sorgu.CommandText = "SELECT COUNT(*) FROM MALZEMELER WHERE DEPO_ID=@DEPOMALVS15_ID ";
                    sorgu.Parameters.AddWithValue("@DEPOMALVS15_ID", lblidsil.Text);
                    int sayi = Convert.ToInt16(sorgu.ExecuteScalar());

                    if (sayi == 0)
                    {
                        sorgu.CommandText = "DELETE FROM DEPO WHERE ID=@ID";
                        sorgu.Parameters.AddWithValue("@ID", lblidsil.Text);
                        sorgu.ExecuteNonQuery();
                        conn.Close();
                        ScriptManager.RegisterStartupScript(this, this.GetType(), System.Guid.NewGuid().ToString(), "YeniMesaj('Depo başarıyla silinmiştir.','Tamam');", true);
                        ScriptManager.RegisterStartupScript(this, this.GetType(), System.Guid.NewGuid().ToString(), "ShowMessage('Depo başarıyla silinmiştir.','Tamam','sil');", true);
                    }
                    else
                    {
                        tkod.mesaj("Hata", "Bu Depoya bağlı malzemeler bulunmaktadır.", this);
                        ScriptManager.RegisterStartupScript(this, this.GetType(), System.Guid.NewGuid().ToString(), "ShowMessage('Bu Depoya bağlı malzemeler bulunmaktadır.','Hata','sil');", true);
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

                listele_depo();
            }
        }

        void Bolge_Listele()
        {
            conn.Open();
            combolge.Items.Clear();
            combolge.Items.Insert(0, new ListItem("Bölge Seç", "0"));
            combolge.AppendDataBoundItems = true;
            string sql1 = "";

            SqlDataAdapter da = new SqlDataAdapter();
            SqlCommand sorgu = new SqlCommand("Select ID,BOLGE_ADI FROM BOLGE ORDER BY ID ");
            DataTable dt = new DataTable();
            da.SelectCommand = sorgu;
            da.SelectCommand.Connection = conn;
            da.Fill(dt);
            combolge.DataSource = dt;
            combolge.DataBind();
            conn.Close();
        }

        void Depo_Tanimi_Listele()
        {
            conn.Open();
            comdepotanimi.Items.Clear();
            comdepotanimi.Items.Insert(0, new ListItem("Depo Tanımı Seç", "0"));
            comdepotanimi.AppendDataBoundItems = true;
            string sql1 = "";

            SqlDataAdapter da = new SqlDataAdapter();
            SqlCommand sorgu = new SqlCommand("Select ID,DEPOTANIMI FROM DEPO_TANIMI ORDER BY ID ");
            DataTable dt = new DataTable();
            da.SelectCommand = sorgu;
            da.SelectCommand.Connection = conn;
            da.Fill(dt);
            comdepotanimi.DataSource = dt;
            comdepotanimi.DataBind();
            conn.Close();
        }

        void Depo_Turu_Listele()
        {
            comdepoturu.Items.Clear();
            comdepoturu.Items.Insert(0, new ListItem("Depo Türü Seç", "0"));
            comdepoturu.AppendDataBoundItems = true;
            string sql2 = "";

            if (comdepotanimi.SelectedIndex > 0)
                sql2 = " AND DEPOTANIMI_ID='" + comdepotanimi.SelectedValue.ToString() + "' ";

            string sql = "SELECT ID,DEPOTURU FROM DEPO_TURU WHERE ID>0 " + sql2 + " ORDER BY ID";
            comdepoturu.DataSource = tkod.GetData(sql);
            comdepoturu.DataBind();

            comdepotanimi.Items.ToString();
        }
        
        void listele_depo()
        {
            SqlCommand cmd;
            string sql = "", sql1 = "";

            sql = "SELECT DEPO.ID,DEPO.DEPO, BOLGE.BOLGE_ADI, DEPO_TURU.DEPOTURU, DEPO_TANIMI.DEPOTANIMI " +
                "FROM DEPO " +
                "INNER JOIN BOLGE ON BOLGE.ID=DEPO.BOLGE_ID " +
                "INNER JOIN DEPO_TURU   ON DEPO_TURU.ID   = DEPO.DEPOTURU_ID " +
                "INNER JOIN DEPO_TANIMI ON DEPO_TANIMI.ID = DEPO.DEPOTANIMI_ID " +
                "WHERE DEPO.ID>0 ORDER BY ID ";
            if (txtdepoadiara.Text.Length > 0)

                sql1 += " AND DEPO LIKE '%" + txtdepoadiara.Text + "%'  ";
            sql = sql + sql1;
            cmd = new SqlCommand(sql, conn);
            conn.Open();
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            grid_depo_liste.DataSource = dt;
            grid_depo_liste.DataBind();
            conn.Close();
        }

        protected void comdepotanimi_SelectedIndexChanged(object sender, EventArgs e)
        {
            Depo_Turu_Listele();
        }
    }
}