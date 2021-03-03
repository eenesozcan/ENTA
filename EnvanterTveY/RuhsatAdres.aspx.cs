using System;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using System.Data;
using System.Data.SqlClient;
using System.Configuration;

namespace EnvanterTveY
{
    public partial class RuhsatAdres : System.Web.UI.Page
    {
        SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["KabloHE"].ConnectionString);
        string sayfa = "RuhsatAdres";
        KodT.kodlar tkod = new KodT.kodlar();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["KULLANICI_ID"] == null)
            {
                Response.Redirect("Giris.aspx?url=" + sayfa);
            }
            if (!IsPostBack)
            {
                listele_il();
                listele_bolge();
                listele_ilce();
                listele_mahalle();
                listele_caddesokak();

            }
        }

        void listele_il()
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

        void listele_ilce()
        {
            SqlCommand cmd;
            string sql = " ", sql1 = " ";

            sql = "SELECT A.ID, IL.IL, BOLGE.BOLGE_ADI, A.ILCE FROM RUHSAT_ADRES_ILCE AS A" +
                " INNER JOIN IL ON IL.ID=A.IL_ID" +
                " INNER JOIN BOLGE ON BOLGE.ID=A.BOLGE_ID" +
                " ";

            sql = sql + tkod.yetki_tablosu_inner("BOLGE", "") + "  WHERE A.ID > 0  " + sql1 + tkod.yetki_tablosu_filtre("BOLGE") + "   ORDER BY ID"; ;
            cmd = new SqlCommand(sql, conn);
            conn.Open();
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            grid_ilce_liste.DataSource = dt;
            grid_ilce_liste.DataBind();
            conn.Close();
        }
        void listele_mahalle()
        {
            SqlCommand cmd;
            string sql = " ", sql1 = " ";

            sql = "SELECT AM.ID, IL.IL, BOLGE.BOLGE_ADI, AI.ILCE, AM.MAHALLE FROM RUHSAT_ADRES_MAHALLE AS AM" +
                " INNER JOIN RUHSAT_ADRES_ILCE AI ON AI.ID=AM.ILCE_ID" +
                " INNER JOIN IL ON IL.ID=AI.IL_ID" +
                " INNER JOIN BOLGE ON BOLGE.ID=AI.BOLGE_ID" +

                " ";

            sql = sql + tkod.yetki_tablosu_inner("BOLGE", "") + "  WHERE AM.ID > 0  " + sql1 + tkod.yetki_tablosu_filtre("BOLGE") + "   ORDER BY AM.ID"; ;
            cmd = new SqlCommand(sql, conn);
            conn.Open();
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            grid_mahalle_liste.DataSource = dt;
            grid_mahalle_liste.DataBind();
            conn.Close();
        }
        void listele_caddesokak()
        {
            SqlCommand cmd;
            string sql = " ", sql1 = " ";

            sql = "SELECT ACS.ID, IL.IL, BOLGE.BOLGE_ADI, AI.ILCE, AM.MAHALLE, ACS.CADDESOKAK FROM RUHSAT_ADRES_CADDESOKAK AS ACS" +
                " INNER JOIN RUHSAT_ADRES_MAHALLE AM ON AM.ID=ACS.MAHALLE_ID" +
                " INNER JOIN IL ON IL.ID=AM.IL_ID" +
                " INNER JOIN BOLGE ON BOLGE.ID=AM.BOLGE_ID" +
                " INNER JOIN RUHSAT_ADRES_ILCE AI ON AI.ID=AM.ILCE_ID" +
                " ";

            sql = sql + tkod.yetki_tablosu_inner("BOLGE", "") + "  WHERE ACS.ID > 0  " + sql1 + tkod.yetki_tablosu_filtre("BOLGE") + "   ORDER BY ACS.ID"; ;
            cmd = new SqlCommand(sql, conn);
            conn.Open();
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            grid_caddesokak_liste.DataSource = dt;
            grid_caddesokak_liste.DataBind();
            conn.Close();
        }

        void il_sec()
        {
            if (lblcomsecici_mahalle.Text == "bolge")
            {
                comil_sec1.Items.Clear();
                comil_sec1.Items.Insert(0, new ListItem("İl Seçiniz", "0"));
                comil_sec1.AppendDataBoundItems = true;

                comil_sec1.DataSource = tkod.dt_il_Liste();
                comil_sec1.DataBind();
            }
            if (lblcomsecici_mahalle.Text == "ilce")
            {
                comil_sec.Items.Clear();
                comil_sec.Items.Insert(0, new ListItem("İl Seçiniz", "0"));
                comil_sec.AppendDataBoundItems = true;

                comil_sec.DataSource = tkod.dt_il_Liste();
                comil_sec.DataBind();
            }
            if (lblcomsecici_mahalle.Text == "mahalle")
            {
                comil_sec_mahalle.Items.Clear();
                comil_sec_mahalle.Items.Insert(0, new ListItem("İl Seçiniz", "0"));
                comil_sec_mahalle.AppendDataBoundItems = true;

                comil_sec_mahalle.DataSource = tkod.dt_il_Liste();
                comil_sec_mahalle.DataBind();
            }
            if (lblcomsecici_mahalle.Text == "caddesokak")
            {
                comil_sec_caddesokak.Items.Clear();
                comil_sec_caddesokak.Items.Insert(0, new ListItem("İl Seçiniz", "0"));
                comil_sec_caddesokak.AppendDataBoundItems = true;

                comil_sec_caddesokak.DataSource = tkod.dt_il_Liste();
                comil_sec_caddesokak.DataBind();
            }

        }
        void bolge_sec()
        {
            if (lblcomsecici_mahalle.Text == "ilce")
            {
                if (comil_sec.SelectedIndex == 0)
                    combolge_sec.Enabled = false;
                else
                    combolge_sec.Enabled = true;

                combolge_sec.Items.Clear();
                combolge_sec.Items.Insert(0, new ListItem("Bölge Seçiniz", "0"));
                combolge_sec.AppendDataBoundItems = true;

                if (comil_sec.SelectedIndex > 0)
                    combolge_sec.DataSource = tkod.dt_bolge_Liste(comil_sec.SelectedValue.ToString());
                else
                    combolge_sec.DataSource = tkod.dt_bolge_Liste();
                combolge_sec.DataBind();
            }
            if (lblcomsecici_mahalle.Text == "mahalle")
            {
                if (comil_sec_mahalle.SelectedIndex == 0)
                    combolge_sec_mahalle.Enabled = false;
                else
                    combolge_sec_mahalle.Enabled = true;

                combolge_sec_mahalle.Items.Clear();
                combolge_sec_mahalle.Items.Insert(0, new ListItem("Bölge Seçiniz", "0"));
                combolge_sec_mahalle.AppendDataBoundItems = true;

                if (comil_sec_mahalle.SelectedIndex > 0)
                    combolge_sec_mahalle.DataSource = tkod.dt_bolge_Liste(comil_sec_mahalle.SelectedValue.ToString());
                else
                    combolge_sec_mahalle.DataSource = tkod.dt_bolge_Liste();
                combolge_sec_mahalle.DataBind();
            }
            if (lblcomsecici_mahalle.Text == "caddesokak")
            {
                if (comil_sec_caddesokak.SelectedIndex == 0)
                    combolge_sec_caddesokak.Enabled = false;
                else
                    combolge_sec_caddesokak.Enabled = true;

                combolge_sec_caddesokak.Items.Clear();
                combolge_sec_caddesokak.Items.Insert(0, new ListItem("Bölge Seçiniz", "0"));
                combolge_sec_caddesokak.AppendDataBoundItems = true;

                if (comil_sec_caddesokak.SelectedIndex > 0)
                    combolge_sec_caddesokak.DataSource = tkod.dt_bolge_Liste(comil_sec_caddesokak.SelectedValue.ToString());
                else
                    combolge_sec_caddesokak.DataSource = tkod.dt_bolge_Liste();
                combolge_sec_caddesokak.DataBind();
            }          
        }
        void ilce_sec()
        {
            if (lblcomsecici_mahalle.Text == "mahalle")
            {
                comilce_sec.Items.Clear();
                comilce_sec.Items.Insert(0, new ListItem("İlçe seçiniz", "0"));
                comilce_sec.AppendDataBoundItems = true;
                string sql2 = "";

                if (combolge_sec_mahalle.SelectedIndex > 0)
                    sql2 = " AND BOLGE_ID='" + combolge_sec_mahalle.SelectedValue.ToString() + "' ";

                string sql = "Select ID,ILCE FROM RUHSAT_ADRES_ILCE WHERE ID>0 " + sql2 + " ORDER BY ID";
                comilce_sec.DataSource = tkod.GetData(sql);
                comilce_sec.DataBind();
            }
            if (lblcomsecici_mahalle.Text == "caddesokak")
            {
                comilce_sec_caddesokak.Items.Clear();
                comilce_sec_caddesokak.Items.Insert(0, new ListItem("İlçe seçiniz", "0"));
                comilce_sec_caddesokak.AppendDataBoundItems = true;
                string sql2 = "";

                if (combolge_sec_caddesokak.SelectedIndex > 0)
                    sql2 = " AND BOLGE_ID='" + combolge_sec_caddesokak.SelectedValue.ToString() + "' ";

                string sql = "Select ID,ILCE FROM RUHSAT_ADRES_ILCE WHERE ID>0 " + sql2 + "  ORDER BY ID";
                comilce_sec_caddesokak.DataSource = tkod.GetData(sql);
                comilce_sec_caddesokak.DataBind();
            }
        }
        void mahalle_sec()
        {
            if (lblcomsecici_mahalle.Text == "caddesokak")
            {
                commahalle_sec.Items.Clear();
                commahalle_sec.Items.Insert(0, new ListItem("Mahalle Seçiniz", "0"));
                commahalle_sec.AppendDataBoundItems = true;
                string sql2 = "";

                if (comilce_sec_caddesokak.SelectedIndex > 0)
                    sql2 = " AND ILCE_ID='" + comilce_sec_caddesokak.SelectedValue.ToString() + "' ";

                string sql = "Select ID,MAHALLE FROM RUHSAT_ADRES_MAHALLE WHERE ID>0 " + sql2 + " ORDER BY ID";
                commahalle_sec.DataSource = tkod.GetData(sql);
                commahalle_sec.DataBind();
            }
        }
        void caddesokak_sec()
        {
        }

        //-----------------------//

        protected void btnilce_ekle_Click(object sender, EventArgs e)
        {
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            sb.Append(@"<script type='text/javascript'>");
            sb.Append("$(\"#Modal_ilce_Kaydet\").modal(\"show\");");
            sb.Append(@"</script>");
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(),
                       "ModalScript", sb.ToString(), false);

            comil_sec.Enabled = true;

            lblcomsecici_mahalle.Text = "ilce";
            lblidcihaz.Text = "";
            txtilce.Text = "";
            btnilce_kaydet.Enabled = true;
            btnilce_kaydet.CssClass = "btn btn-success ";

            lblmodalyenibaslik.Text = "İlçe Ekle";
            il_sec();
            combolge_sec.Enabled = false;
        }

        protected void comil_sec_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comil_sec.SelectedIndex == 0)
                combolge_sec.Enabled = false;
            else
                combolge_sec.Enabled = true;

            bolge_sec();
        }

        protected void grid_ilce_liste_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName.Equals("sil"))
            {
                int index = Convert.ToInt32(e.CommandArgument);
                lblidsil.Text = grid_ilce_liste.DataKeys[index].Value.ToString();
                lblislem.Text = "ilce-sil";
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
                string id = grid_ilce_liste.DataKeys[index].Value.ToString();
                lblidcihaz.Text = id;

                lblcomsecici_mahalle.Text = "ilce";
                il_sec();
                bolge_sec();
                ilce_sec();

                comil_sec.SelectedIndex = comil_sec.Items.IndexOf(comil_sec.Items.FindByText(HttpUtility.HtmlDecode(grid_ilce_liste.Rows[index].Cells[1].Text)));
                combolge_sec.SelectedIndex = combolge_sec.Items.IndexOf(combolge_sec.Items.FindByText(HttpUtility.HtmlDecode(grid_ilce_liste.Rows[index].Cells[2].Text)));
                txtilce.Text = HttpUtility.HtmlDecode(grid_ilce_liste.Rows[index].Cells[3].Text);

                btnilce_kaydet.Enabled = true;
                btnilce_kaydet.CssClass = "btn btn-success ";

                comil_sec.Enabled = false;
                combolge_sec.Enabled = false;

                lblmodalyenibaslik.Text = "İlçe Kayıt Güncelle";

                System.Text.StringBuilder sb = new System.Text.StringBuilder();
                sb.Append(@"<script type='text/javascript'>");
                sb.Append("$(\"#Modal_ilce_Kaydet\").modal(\"show\");");
                sb.Append(@"</script>");
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(),
                           "ModalScript", sb.ToString(), false);
            }
        }

        protected void combolge_sec_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        protected void btnilce_kaydet_Click(object sender, EventArgs e)
        {
            try
            {
                SqlCommand sorgu = new SqlCommand();
                sorgu.Connection = conn;
                conn.Open();

                if (lblidcihaz.Text == "")
                {
                    sorgu.CommandText = "SELECT COUNT(*) FROM RUHSAT_ADRES_ILCE WHERE ILCE=@ADRES_ILCE ";
                    sorgu.Parameters.AddWithValue("@ADRES_ILCE", txtilce.Text);
                    int sayi = Convert.ToInt16(sorgu.ExecuteScalar());

                    if (sayi == 0)
                    {
                        string userid = Session["KULLANICI_ID"].ToString();
                        sorgu.CommandText = "INSERT INTO RUHSAT_ADRES_ILCE (IL_ID, BOLGE_ID,ILCE, KAYIT_EDEN , KAYIT_TARIHI)  " +
                            "                   VALUES(@ILCE_IL_K, @ILCE_BOLGE_K, @ILCE_ILCE_K, @UI_ILCE, getdate() ) ";
                        sorgu.Parameters.AddWithValue("@ILCE_IL_K", comil_sec.SelectedValue);
                        sorgu.Parameters.AddWithValue("@ILCE_BOLGE_K", combolge_sec.SelectedValue);
                        sorgu.Parameters.AddWithValue("@ILCE_ILCE_K", txtilce.Text);
                        sorgu.Parameters.AddWithValue("@UI_ILCE", userid);
                        sorgu.ExecuteNonQuery();

                        ScriptManager.RegisterStartupScript(this, this.GetType(), System.Guid.NewGuid().ToString(), "ShowMessage('İlçe Adı kayıt edilmiştir.','Tamam','yeni1');", true);
                        ScriptManager.RegisterStartupScript(this, this.GetType(), System.Guid.NewGuid().ToString(), "YeniMesaj('İlçe Adı kayıt edilmiştir.','Tamam');", true);

                        btnilce_kaydet.Enabled = false;
                        btnilce_kaydet.CssClass = "btn btn-success disabled";
                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(this, this.GetType(), System.Guid.NewGuid().ToString(), "YeniMesaj('Bu İlçe adı daha önce kayıt edilmişti. Lütfen kontrol ediniz.','Hata');", true);
                        ScriptManager.RegisterStartupScript(this, this.GetType(), System.Guid.NewGuid().ToString(), "ShowMessage('Bu İlçe adı daha önce kayıt edilmişti. Lütfen kontrol ediniz.','Hata','yeni1');", true);
                    }
                }
                else
                {
                    string userid = Session["KULLANICI_ID"].ToString();
                    sorgu.CommandText = "UPDATE RUHSAT_ADRES_ILCE SET  ILCE=@ILCE_ILCE_G   WHERE ID=@ILCE_ID_G ";
                    sorgu.Parameters.AddWithValue("@ILCE_ILCE_G", txtilce.Text);
                    sorgu.Parameters.AddWithValue("@ILCE_ID_G", lblidcihaz.Text);
                    sorgu.ExecuteNonQuery();

                    ScriptManager.RegisterStartupScript(this, this.GetType(), System.Guid.NewGuid().ToString(), "YeniMesaj('İlçe bilgisi başarıyla güncellenmiştir.','Tamam');", true);
                    ScriptManager.RegisterStartupScript(this, this.GetType(), System.Guid.NewGuid().ToString(), "ShowMessage('İlçe bilgisi başarıyla güncellenmiştir.','Tamam','yeni1');", true);
                    btnilce_kaydet.Enabled = false;
                    btnilce_kaydet.CssClass = "btn btn-success disabled";
                }
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), System.Guid.NewGuid().ToString(), "YeniMesaj('Beklenmedik bir hata oluştu.','Hata');", true);
                ScriptManager.RegisterStartupScript(this, this.GetType(), System.Guid.NewGuid().ToString(), "ShowMessage('Hata oluştu :'" + ex.Message.Replace("'", "") + ",'Hata','yeni1');", true);
            }

            if (ConnectionState.Open == conn.State)
                conn.Close();
            listele_ilce();
            listele_mahalle();
            listele_caddesokak();

        }

        //------------------------//

        protected void btnmahalle_ekle_Click(object sender, EventArgs e)
        {
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            sb.Append(@"<script type='text/javascript'>");
            sb.Append("$(\"#Modal_Mahalle_Kaydet\").modal(\"show\");");
            sb.Append(@"</script>");
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(),
                       "ModalScript", sb.ToString(), false);

            comil_sec_mahalle.Enabled = true;

            lblcomsecici_mahalle.Text = "mahalle";
            lblidcihaz1.Text = "";
            txtmahalle.Text = "";
            btnmahalle_kaydet.Enabled = true;
            btnmahalle_kaydet.CssClass = "btn btn-success ";

            lblmodalyenibaslik1.Text = "Mahalle Ekle";
            il_sec();
            bolge_sec();
        }

        protected void comil_sec_mahalle_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comil_sec_mahalle.SelectedIndex == 0)
            {
                combolge_sec_mahalle.Enabled = false;
                comilce_sec.Enabled = false;
            }
            else
                combolge_sec_mahalle.Enabled = true;

            bolge_sec();
        }

        protected void combolge_sec_mahalle_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (combolge_sec_mahalle.SelectedIndex == 0)
                comilce_sec.Enabled = false;
            else
                comilce_sec.Enabled = true;

            ilce_sec();
        }

        protected void grid_mahalle_liste_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName.Equals("sil"))
            {
                int index = Convert.ToInt32(e.CommandArgument);
                lblidsil.Text = grid_mahalle_liste.DataKeys[index].Value.ToString();
                lblislem.Text = "mahalle-sil";
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
                string id = grid_mahalle_liste.DataKeys[index].Value.ToString();
                lblidcihaz1.Text = id;

                lblcomsecici_mahalle.Text = "mahalle";
                il_sec();
                bolge_sec();
                ilce_sec();


                comil_sec_mahalle.SelectedIndex = comil_sec_mahalle.Items.IndexOf(comil_sec_mahalle.Items.FindByText(HttpUtility.HtmlDecode(grid_mahalle_liste.Rows[index].Cells[1].Text)));
                combolge_sec_mahalle.SelectedIndex = combolge_sec_mahalle.Items.IndexOf(combolge_sec_mahalle.Items.FindByText(HttpUtility.HtmlDecode(grid_mahalle_liste.Rows[index].Cells[2].Text)));
                comilce_sec.SelectedIndex = comilce_sec.Items.IndexOf(comilce_sec.Items.FindByText(HttpUtility.HtmlDecode(grid_mahalle_liste.Rows[index].Cells[3].Text)));
                txtmahalle.Text = HttpUtility.HtmlDecode(grid_mahalle_liste.Rows[index].Cells[4].Text);

                btnmahalle_kaydet.Enabled = true;
                btnmahalle_kaydet.CssClass = "btn btn-success ";

                comil_sec_mahalle.Enabled = false;
                combolge_sec_mahalle.Enabled = false;
                comilce_sec.Enabled = false;

                lblmodalyenibaslik1.Text = "Mahalle Kayıt Güncelle";

                System.Text.StringBuilder sb = new System.Text.StringBuilder();
                sb.Append(@"<script type='text/javascript'>");
                sb.Append("$(\"#Modal_Mahalle_Kaydet\").modal(\"show\");");
                sb.Append(@"</script>");
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(),
                           "ModalScript", sb.ToString(), false);
            }
        }

        protected void btnmahalle_kaydet_Click(object sender, EventArgs e)
        {
            try
            {
                SqlCommand sorgu = new SqlCommand();
                sorgu.Connection = conn;
                conn.Open();

                if (lblidcihaz1.Text == "")
                {
                    sorgu.CommandText = "SELECT COUNT(*) FROM RUHSAT_ADRES_MAHALLE WHERE MAHALLE=@ADRES_MAHALLE ";
                    sorgu.Parameters.AddWithValue("@ADRES_MAHALLE", txtmahalle.Text);
                    int sayi = Convert.ToInt16(sorgu.ExecuteScalar());

                    if (sayi == 0)
                    {
                        string userid = Session["KULLANICI_ID"].ToString();
                        sorgu.CommandText = "INSERT INTO RUHSAT_ADRES_MAHALLE (IL_ID, BOLGE_ID, ILCE_ID, MAHALLE, KAYIT_EDEN , KAYIT_TARIHI)  " +
                            "                   VALUES(@MAH_IL_K, @MAH_BOLGE_K, @MAH_ILCE_K, @MAH_MAHALLE_K, @UI_MAHALLE, getdate() ) ";
                        sorgu.Parameters.AddWithValue("@MAH_IL_K", comil_sec_mahalle.SelectedValue);
                        sorgu.Parameters.AddWithValue("@MAH_BOLGE_K", combolge_sec_mahalle.SelectedValue);
                        sorgu.Parameters.AddWithValue("@MAH_ILCE_K", comilce_sec.SelectedValue);
                        sorgu.Parameters.AddWithValue("@MAH_MAHALLE_K", txtmahalle.Text);
                        sorgu.Parameters.AddWithValue("@UI_MAHALLE", userid);
                        sorgu.ExecuteNonQuery();

                        ScriptManager.RegisterStartupScript(this, this.GetType(), System.Guid.NewGuid().ToString(), "ShowMessage('Mahalle Adı kayıt edilmiştir.','Tamam','yeni1');", true);
                        ScriptManager.RegisterStartupScript(this, this.GetType(), System.Guid.NewGuid().ToString(), "YeniMesaj('Mahalle Adı kayıt edilmiştir.','Tamam');", true);

                        //btnmahalle_kaydet.Enabled = false;
                        //btnmahalle_kaydet.CssClass = "btn btn-success disabled";
                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(this, this.GetType(), System.Guid.NewGuid().ToString(), "YeniMesaj('Bu Mahalle adı daha önce kayıt edilmişti. Lütfen kontrol ediniz.','Hata');", true);
                        ScriptManager.RegisterStartupScript(this, this.GetType(), System.Guid.NewGuid().ToString(), "ShowMessage('Bu Mahalle adı daha önce kayıt edilmişti. Lütfen kontrol ediniz.','Hata','yeni1');", true);
                    }
                }
                else
                {
                    string userid = Session["KULLANICI_ID"].ToString();
                    sorgu.CommandText = "UPDATE RUHSAT_ADRES_MAHALLE SET MAHALLE=@MAH_MAHALLE_G   WHERE ID=@MAHALLE_ID_G ";
                    sorgu.Parameters.AddWithValue("@MAH_MAHALLE_G", txtmahalle.Text);
                    sorgu.Parameters.AddWithValue("@MAHALLE_ID_G", lblidcihaz1.Text);
                    sorgu.ExecuteNonQuery();

                    ScriptManager.RegisterStartupScript(this, this.GetType(), System.Guid.NewGuid().ToString(), "YeniMesaj('Mahalle bilgisi başarıyla güncellenmiştir.','Tamam');", true);
                    ScriptManager.RegisterStartupScript(this, this.GetType(), System.Guid.NewGuid().ToString(), "ShowMessage('Mahalle bilgisi başarıyla güncellenmiştir.','Tamam','yeni1');", true);
                    btnmahalle_kaydet.Enabled = false;
                    btnmahalle_kaydet.CssClass = "btn btn-success disabled";
                }
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), System.Guid.NewGuid().ToString(), "YeniMesaj('Beklenmedik bir hata oluştu.','Hata');", true);
                ScriptManager.RegisterStartupScript(this, this.GetType(), System.Guid.NewGuid().ToString(), "ShowMessage('Hata oluştu :'" + ex.Message.Replace("'", "") + ",'Hata','yeni1');", true);
            }

            if (ConnectionState.Open == conn.State)
                conn.Close();
            listele_ilce();
            listele_mahalle();
            listele_caddesokak();

        }

        //--------------------------------------//

        protected void btncaddesokak_ekle_Click(object sender, EventArgs e)
        {
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            sb.Append(@"<script type='text/javascript'>");
            sb.Append("$(\"#Modal_CaddeSokak_Kaydet\").modal(\"show\");");
            sb.Append(@"</script>");
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(),
                       "ModalScript", sb.ToString(), false);

            comil_sec_caddesokak.Enabled = true;

            lblcomsecici_mahalle.Text = "caddesokak";

            lblidcihaz2.Text = "";
            txtcaddesokak.Text = "";
            btncaddesokak_kaydet.Enabled = true;
            btncaddesokak_kaydet.CssClass = "btn btn-success ";

            lblmodalyenibaslik2.Text = "Cadde/Sokak Ekle";
            il_sec();
            combolge_sec_caddesokak.Enabled = false;
            comilce_sec_caddesokak.Enabled = false;
            commahalle_sec.Enabled = false;
        }

        protected void grid_caddesokak_liste_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName.Equals("sil"))
            {
                int index = Convert.ToInt32(e.CommandArgument);
                lblidsil.Text = grid_caddesokak_liste.DataKeys[index].Value.ToString();
                lblislem.Text = "caddesokak-sil";
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
                string id = grid_caddesokak_liste.DataKeys[index].Value.ToString();
                lblidcihaz2.Text = id;

                lblcomsecici_mahalle.Text = "caddesokak";
                il_sec();
                bolge_sec();
                ilce_sec();
                mahalle_sec();

                comil_sec_caddesokak.SelectedIndex = comil_sec_caddesokak.Items.IndexOf(comil_sec_caddesokak.Items.FindByText(HttpUtility.HtmlDecode(grid_caddesokak_liste.Rows[index].Cells[1].Text)));
                combolge_sec_caddesokak.SelectedIndex = combolge_sec_caddesokak.Items.IndexOf(combolge_sec_caddesokak.Items.FindByText(HttpUtility.HtmlDecode(grid_caddesokak_liste.Rows[index].Cells[2].Text)));
                comilce_sec_caddesokak.SelectedIndex = comilce_sec_caddesokak.Items.IndexOf(comilce_sec_caddesokak.Items.FindByText(HttpUtility.HtmlDecode(grid_caddesokak_liste.Rows[index].Cells[3].Text)));
                commahalle_sec.SelectedIndex = comilce_sec.Items.IndexOf(comilce_sec.Items.FindByText(HttpUtility.HtmlDecode(grid_caddesokak_liste.Rows[index].Cells[4].Text)));
                txtcaddesokak.Text = HttpUtility.HtmlDecode(grid_caddesokak_liste.Rows[index].Cells[5].Text);

                comil_sec_caddesokak.Enabled = false;
                combolge_sec_caddesokak.Enabled = false;
                comilce_sec_caddesokak.Enabled = false;
                commahalle_sec.Enabled = false;

                btncaddesokak_kaydet.Enabled = true;
                btncaddesokak_kaydet.CssClass = "btn btn-success ";

                lblmodalyenibaslik2.Text = "Cadde/Sokak Kayıt Güncelle";

                System.Text.StringBuilder sb = new System.Text.StringBuilder();
                sb.Append(@"<script type='text/javascript'>");
                sb.Append("$(\"#Modal_CaddeSokak_Kaydet\").modal(\"show\");");
                sb.Append(@"</script>");
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(),
                           "ModalScript", sb.ToString(), false);
            }
        }

        protected void commahalle_sec_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        protected void btncaddesokak_kaydet_Click(object sender, EventArgs e)
        {
            try
            {
                SqlCommand sorgu = new SqlCommand();
                sorgu.Connection = conn;
                conn.Open();

                if (lblidcihaz2.Text == "")
                {
                    sorgu.CommandText = "SELECT COUNT(*) FROM RUHSAT_ADRES_CADDESOKAK WHERE CADDESOKAK=@ADRES_CADDESOKAK ";
                    sorgu.Parameters.AddWithValue("@ADRES_CADDESOKAK", txtcaddesokak.Text);
                    int sayi = Convert.ToInt16(sorgu.ExecuteScalar());

                    if (sayi == 0)
                    {
                        string userid = Session["KULLANICI_ID"].ToString();
                        sorgu.CommandText = "INSERT INTO RUHSAT_ADRES_CADDESOKAK (IL_ID, BOLGE_ID, ILCE_ID, MAHALLE_ID, CADDESOKAK, KAYIT_EDEN , KAYIT_TARIHI)  " +
                            "                   VALUES(@CD_IL_K, @CD_BOLGE_K, @CD_ILCE_K, @CD_MAHALLE_K, @CD_CADDESOKAK_K, @UI_CADDESOKAK_K, getdate() ) ";
                        sorgu.Parameters.AddWithValue("@CD_IL_K", comil_sec_caddesokak.SelectedValue);
                        sorgu.Parameters.AddWithValue("@CD_BOLGE_K", combolge_sec_caddesokak.SelectedValue);
                        sorgu.Parameters.AddWithValue("@CD_ILCE_K", comilce_sec_caddesokak.SelectedValue);
                        sorgu.Parameters.AddWithValue("@CD_MAHALLE_K", commahalle_sec.SelectedValue);
                        sorgu.Parameters.AddWithValue("@CD_CADDESOKAK_K", txtcaddesokak.Text);
                        sorgu.Parameters.AddWithValue("@UI_CADDESOKAK_K", userid);
                        sorgu.ExecuteNonQuery();

                        ScriptManager.RegisterStartupScript(this, this.GetType(), System.Guid.NewGuid().ToString(), "ShowMessage('Cadde/Sokak Adı kayıt edilmiştir.','Tamam','yeni1');", true);
                        ScriptManager.RegisterStartupScript(this, this.GetType(), System.Guid.NewGuid().ToString(), "YeniMesaj('Cadde/Sokak Adı kayıt edilmiştir.','Tamam');", true);

                        //btncaddesokak_kaydet.Enabled = false;
                        //btncaddesokak_kaydet.CssClass = "btn btn-success disabled";
                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(this, this.GetType(), System.Guid.NewGuid().ToString(), "YeniMesaj('Bu Cadde/Sokak adı daha önce kayıt edilmişti. Lütfen kontrol ediniz.','Hata');", true);
                        ScriptManager.RegisterStartupScript(this, this.GetType(), System.Guid.NewGuid().ToString(), "ShowMessage('Bu Cadde/Sokak adı daha önce kayıt edilmişti. Lütfen kontrol ediniz.','Hata','yeni1');", true);
                    }
                }
                else
                {
                    string userid = Session["KULLANICI_ID"].ToString();
                    sorgu.CommandText = "UPDATE RUHSAT_ADRES_CADDESOKAK SET CADDESOKAK=@CD_CADDESOKAK_G  WHERE ID=@MAHALLE_ID_G ";
                    sorgu.Parameters.AddWithValue("@CD_CADDESOKAK_G", txtcaddesokak.Text);
                    sorgu.Parameters.AddWithValue("@MAHALLE_ID_G", lblidcihaz2.Text);
                    sorgu.ExecuteNonQuery();

                    ScriptManager.RegisterStartupScript(this, this.GetType(), System.Guid.NewGuid().ToString(), "YeniMesaj('Cadde/Sokak bilgisi başarıyla güncellenmiştir.','Tamam');", true);
                    ScriptManager.RegisterStartupScript(this, this.GetType(), System.Guid.NewGuid().ToString(), "ShowMessage('Cadde/Sokak bilgisi başarıyla güncellenmiştir.','Tamam','yeni1');", true);
                    btncaddesokak_kaydet.Enabled = false;
                    btncaddesokak_kaydet.CssClass = "btn btn-success disabled";
                }
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), System.Guid.NewGuid().ToString(), "YeniMesaj('Beklenmedik bir hata oluştu.','Hata');", true);
                ScriptManager.RegisterStartupScript(this, this.GetType(), System.Guid.NewGuid().ToString(), "ShowMessage('Hata oluştu :'" + ex.Message.Replace("'", "") + ",'Hata','yeni1');", true);
            }

            if (ConnectionState.Open == conn.State)
                conn.Close();
            listele_ilce();
            listele_mahalle();
            listele_caddesokak();

        }

        protected void comil_sec_caddesokak_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comil_sec_caddesokak.SelectedIndex == 0)
            {
                combolge_sec_caddesokak.Enabled = false;
                comilce_sec_caddesokak.Enabled = false;
                commahalle_sec.Enabled = false;
            }
            else
                combolge_sec_caddesokak.Enabled = true;

            bolge_sec();
        }

        protected void combolge_sec_caddesokak_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (combolge_sec_caddesokak.SelectedIndex == 0)
            {
                comilce_sec_caddesokak.Enabled = false;
                commahalle_sec.Enabled = false;
            }
            else
                comilce_sec_caddesokak.Enabled = true;

            ilce_sec();
        }

        protected void comilce_sec_caddesokak_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comilce_sec_caddesokak.SelectedIndex == 0)
            {
                commahalle_sec.Enabled = false;
            }
            else
                commahalle_sec.Enabled = true;

            mahalle_sec();
        }

        //--------------------------------------//

        protected void btnsil_Click(object sender, EventArgs e)
        {
            SqlCommand sorgu = new SqlCommand();
            sorgu.Connection = conn;
            conn.Open();

            if (lblislem.Text == "ilce-sil")
            {
                sorgu.CommandText = "SELECT COUNT(*) FROM RUHSAT_ADRES_MAHALLE WHERE ILCE_ID=@ILCE_ID_SAY ";
                sorgu.Parameters.AddWithValue("@ILCE_ID_SAY", lblidsil.Text);
                int sayi = Convert.ToInt16(sorgu.ExecuteScalar());
                conn.Close();

                if (sayi == 0)
                {
                    SqlCommand sorgu1 = new SqlCommand();
                    sorgu1.Connection = conn;
                    conn.Open();

                    sorgu1.CommandText = "DELETE FROM RUHSAT_ADRES_ILCE WHERE ID=@ILCE_ID_SIL";
                    sorgu1.Parameters.AddWithValue("@ILCE_ID_SIL", lblidsil.Text);
                    sorgu1.ExecuteNonQuery();
                    conn.Close();
                    ScriptManager.RegisterStartupScript(this, this.GetType(), System.Guid.NewGuid().ToString(), "YeniMesaj('İlçe adı başarıyla silinmiştir.','Tamam');", true);
                    ScriptManager.RegisterStartupScript(this, this.GetType(), System.Guid.NewGuid().ToString(), "ShowMessage('İlçe adı başarıyla silinmiştir.','Tamam','sil');", true);

                    btnsil.Enabled = false;
                    btnsil.CssClass = "btn btn-danger disabled";

                    listele_ilce();
                    listele_mahalle();
                    listele_caddesokak();

                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), System.Guid.NewGuid().ToString(), "YeniMesaj('Bu ilçe adı bir Mahalle ile eşleştirildiğinden silme işlemi yapılamaz!','Hata');", true);
                    ScriptManager.RegisterStartupScript(this, this.GetType(), System.Guid.NewGuid().ToString(), "ShowMessage('Bu ilçe adı bir Mahalle ile eşleştirildiğinden silme işlemi yapılamaz!','Hata','sil');", true);
                    btnsil.Enabled = false;
                    btnsil.CssClass = "btn btn-danger disabled";
                }
                if (ConnectionState.Open == conn.State)
                    conn.Close();
            }

            if (lblislem.Text == "mahalle-sil")
            {

                sorgu.CommandText = "SELECT COUNT(*) FROM RUHSAT_ADRES_CADDESOKAK WHERE MAHALLE_ID=@MAHALLE_ID_SAY ";
                sorgu.Parameters.AddWithValue("@MAHALLE_ID_SAY", lblidsil.Text);
                int sayi = Convert.ToInt16(sorgu.ExecuteScalar());
                conn.Close();

                if (sayi == 0)
                {

                    SqlCommand sorgu1 = new SqlCommand();
                    sorgu1.Connection = conn;
                    conn.Open();

                    sorgu1.CommandText = "DELETE FROM RUHSAT_ADRES_MAHALLE WHERE ID=@MAHALLE_ID_SIL";
                    sorgu1.Parameters.AddWithValue("@MAHALLE_ID_SIL", lblidsil.Text);
                    sorgu1.ExecuteNonQuery();
                    conn.Close();
                    ScriptManager.RegisterStartupScript(this, this.GetType(), System.Guid.NewGuid().ToString(), "YeniMesaj('Mahalle adı başarıyla silinmiştir.','Tamam');", true);
                    ScriptManager.RegisterStartupScript(this, this.GetType(), System.Guid.NewGuid().ToString(), "ShowMessage('Mahalle adı başarıyla silinmiştir.','Tamam','sil');", true);

                    btnsil.Enabled = false;
                    btnsil.CssClass = "btn btn-danger disabled";

                    listele_ilce();
                    listele_mahalle();
                    listele_caddesokak();

                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), System.Guid.NewGuid().ToString(), "YeniMesaj('Bu Mahalle adı bir Cadde/Sokak adı ile eşleştirildiğinden silme işlemi yapılamaz!','Hata');", true);
                    ScriptManager.RegisterStartupScript(this, this.GetType(), System.Guid.NewGuid().ToString(), "ShowMessage('Bu Mahalle adı bir Cadde/Sokak adı ile eşleştirildiğinden silme işlemi yapılamaz!','Hata','sil');", true);
                    btnsil.Enabled = false;
                    btnsil.CssClass = "btn btn-danger disabled";
                }
                if (ConnectionState.Open == conn.State)
                    conn.Close();
            }

            if (lblislem.Text == "caddesokak-sil")
            {

                sorgu.CommandText = "SELECT COUNT(*) FROM RUHSAT_ADRES_BINANO WHERE CADDESOKAK_ID=@CADDESOKAK_ID_SAY ";
                sorgu.Parameters.AddWithValue("@CADDESOKAK_ID_SAY", lblidsil.Text);
                int sayi = Convert.ToInt16(sorgu.ExecuteScalar());
                conn.Close();

                if (sayi == 0)
                {

                    SqlCommand sorgu1 = new SqlCommand();
                    sorgu1.Connection = conn;
                    conn.Open();

                    sorgu1.CommandText = "DELETE FROM ADRES_CADDESOKAK WHERE ID=@CADDESOKAK_ID_SIL";
                    sorgu1.Parameters.AddWithValue("@CADDESOKAKID_SIL", lblidsil.Text);
                    sorgu1.ExecuteNonQuery();
                    conn.Close();
                    ScriptManager.RegisterStartupScript(this, this.GetType(), System.Guid.NewGuid().ToString(), "YeniMesaj('Cadde/Sokak adı başarıyla silinmiştir.','Tamam');", true);
                    ScriptManager.RegisterStartupScript(this, this.GetType(), System.Guid.NewGuid().ToString(), "ShowMessage('Cadde/Sokak adı başarıyla silinmiştir.','Tamam','sil');", true);

                    btnsil.Enabled = false;
                    btnsil.CssClass = "btn btn-danger disabled";

                    listele_ilce();
                    listele_mahalle();
                    listele_caddesokak();

                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), System.Guid.NewGuid().ToString(), "YeniMesaj('Bu Cadde/Sokak adı bir Cadde/Sokak adı ile eşleştirildiğinden silme işlemi yapılamaz!','Hata');", true);
                    ScriptManager.RegisterStartupScript(this, this.GetType(), System.Guid.NewGuid().ToString(), "ShowMessage('Bu Cadde/Sokak adı bir Cadde/Sokak adı ile eşleştirildiğinden silme işlemi yapılamaz!','Hata','sil');", true);
                    btnsil.Enabled = false;
                    btnsil.CssClass = "btn btn-danger disabled";
                }
                if (ConnectionState.Open == conn.State)
                    conn.Close();
            }
        }

        protected void btnilara_Click(object sender, EventArgs e)
            {
                listele_il();
            }

        //--------------------------------------//

        protected void btnbolgeara_Click(object sender, EventArgs e)
        {
            listele_bolge();
        }

    }
}