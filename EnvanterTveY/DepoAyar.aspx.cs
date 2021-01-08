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
    public partial class DepoAyar : System.Web.UI.Page
    {
        SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["KabloHE"].ConnectionString);
        string sayfa = "DepoAyar";
        DataTable dt;
        KodT.kodlar tkod = new KodT.kodlar();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                listele_tip();
                listele_tur();
                listele_depotur();
                listele_malzemedurum();
                listele_markamodel();
                listele_model();
                listele_depotanimi();
                listele_depotanimi1();
            }
        }

        protected void btnsil_Click(object sender, EventArgs e)
        {
            if (lblislem.Text == "tip-sil")
            {
                try
                {
                    SqlCommand sorgu = new SqlCommand();
                    sorgu.Connection = conn;
                    conn.Open();

                    sorgu.CommandText = "SELECT COUNT(*) FROM MALZEME_TURU WHERE TIP_ID=@TIP14_ID ";
                    sorgu.Parameters.AddWithValue("@TIP14_ID", lblidsil.Text);
                    int sayi = Convert.ToInt16(sorgu.ExecuteScalar());

                    if (sayi == 0)
                    {
                        sorgu.CommandText = "DELETE FROM MALZEME_TIP WHERE ID=@ID";
                        sorgu.Parameters.AddWithValue("@ID", lblidsil.Text);
                        sorgu.ExecuteNonQuery();
                        conn.Close();
                        ScriptManager.RegisterStartupScript(this, this.GetType(), System.Guid.NewGuid().ToString(), "ShowMessage('Tip bilgisi başarıyla silinmiştir.','Tamam','sil');", true);
                        tkod.mesaj("Tamam", "Tip bilgisi başarıyla silinmiştir.", this);
                    }
                    else
                    {
                        tkod.mesaj("Hata", "Bu malzeme tipine bağlı malzeme türü  bulunmaktadır.", this);
                        ScriptManager.RegisterStartupScript(this, this.GetType(), System.Guid.NewGuid().ToString(), "ShowMessage('Bu malzeme tipine bağlı malzeme türü  bulunmaktadır.','Hata','sil');", true);
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

                listele_tip();
            }
            if (lblislem.Text == "malzemeturu-sil")
            {
                try
                {
                    SqlCommand sorgu = new SqlCommand();
                    sorgu.Connection = conn;
                    conn.Open();

                    sorgu.CommandText = "SELECT COUNT(*) FROM MALZEME_MARKAMODEL WHERE TUR_ID=@TUR19_ID ";
                    sorgu.Parameters.AddWithValue("@TUR19_ID", lblidsil.Text);
                    int sayi = Convert.ToInt16(sorgu.ExecuteScalar());

                    if (sayi == 0)
                    {
                        sorgu.CommandText = "DELETE FROM MALZEME_TURU WHERE ID=@ID";
                        sorgu.Parameters.AddWithValue("@ID", lblidsil.Text);
                        sorgu.ExecuteNonQuery();

                        conn.Close();

                        ScriptManager.RegisterStartupScript(this, this.GetType(), System.Guid.NewGuid().ToString(), "ShowMessage('Malzeme Türü başarıyla silinmiştir.','Tamam','sil');", true);
                        tkod.mesaj("Tamam", "Malzeme Türü başarıyla silinmiştir.", this);
                    }
                    else
                    {
                        tkod.mesaj("Hata", "Bu malzeme türüne bağlı malzeme bulunmaktadır.", this);
                        ScriptManager.RegisterStartupScript(this, this.GetType(), System.Guid.NewGuid().ToString(), "ShowMessage('Bu malzeme türüne bağlı malzeme bulunmaktadır.','Hata','sil');", true);
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

                listele_tur();
                
            }
            if (lblislem.Text == "depoturu-sil")
            {
                try
                {
                    SqlCommand sorgu = new SqlCommand();
                    sorgu.Connection = conn;
                    conn.Open();

                    sorgu.CommandText = "SELECT COUNT(*) FROM DEPO WHERE DEPOTURU_ID=@DEPOTURU15_ID ";
                    sorgu.Parameters.AddWithValue("@DEPOTURU15_ID", lblidsil.Text);
                    int sayi = Convert.ToInt16(sorgu.ExecuteScalar());

                    if (sayi < 1)
                    {
                        sorgu.CommandText = "DELETE FROM DEPO_TURU WHERE ID=@ID";
                        sorgu.Parameters.AddWithValue("@ID", lblidsil.Text);
                        sorgu.ExecuteNonQuery();
                        conn.Close();
                        ScriptManager.RegisterStartupScript(this, this.GetType(), System.Guid.NewGuid().ToString(), "ShowMessage('Depo Türü başarıyla silinmiştir.','Tamam','sil');", true);
                        tkod.mesaj("Tamam", "Depo Türü başarıyla silinmiştir.", this);
                    }
                    else
                    {
                        tkod.mesaj("Hata", "Bu Depo Türüne bağlı Depo bulunmaktadır.", this);
                        ScriptManager.RegisterStartupScript(this, this.GetType(), System.Guid.NewGuid().ToString(), "ShowMessage('Bu Depo Türüne bağlı Depo bulunmaktadır.','Hata','sil');", true);
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

                listele_depotur();
            }
            if (lblislem.Text == "depotanimi-sil")
            {
                try
                {
                    SqlCommand sorgu = new SqlCommand();
                    sorgu.Connection = conn;
                    conn.Open();

                    sorgu.CommandText = "SELECT COUNT(*) FROM DEPO_TURU WHERE DEPOTANIMI_ID=@DEPOTANIMI76_ID ";
                    sorgu.Parameters.AddWithValue("@DEPOTANIMI76_ID", lblidsil.Text);
                    int sayi = Convert.ToInt16(sorgu.ExecuteScalar());

                    if (sayi < 1)
                    {
                        sorgu.CommandText = "DELETE FROM DEPO_TANIMI WHERE ID=@ID";
                        sorgu.Parameters.AddWithValue("@ID", lblidsil.Text);
                        sorgu.ExecuteNonQuery();
                        conn.Close();
                        ScriptManager.RegisterStartupScript(this, this.GetType(), System.Guid.NewGuid().ToString(), "ShowMessage('Depo Tanımı başarıyla silinmiştir.','Tamam','sil');", true);
                        tkod.mesaj("Tamam", "Depo Tanımı başarıyla silinmiştir.", this);
                    }
                    else
                    {
                        tkod.mesaj("Hata", "Bu Depo Tanımına bağlı Depo Türü bulunmaktadır.", this);
                        ScriptManager.RegisterStartupScript(this, this.GetType(), System.Guid.NewGuid().ToString(), "ShowMessage('Bu Depo Tanımına bağlı Depo Türü bulunmaktadır.','Hata','sil');", true);
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

                listele_depotanimi1();
            }
            if (lblislem.Text == "malzemedurum-sil")
            {
                try
                {
                    SqlCommand sorgu = new SqlCommand();
                    sorgu.Connection = conn;
                    conn.Open();

                    sorgu.CommandText = "SELECT COUNT(*) FROM MALZEMELER WHERE DURUM=@DURUM19_ID ";
                    sorgu.Parameters.AddWithValue("@DURUM19_ID", lblidsil.Text);
                    int sayi = Convert.ToInt16(sorgu.ExecuteScalar());

                    if (sayi < 1)
                    {
                        sorgu.CommandText = "DELETE FROM MALZEME_DURUM WHERE ID=@ID";
                        sorgu.Parameters.AddWithValue("@ID", lblidsil.Text);

                        sorgu.ExecuteNonQuery();
                        conn.Close();
                        ScriptManager.RegisterStartupScript(this, this.GetType(), System.Guid.NewGuid().ToString(), "ShowMessage('Malzeme Durumu başarıyla silinmiştir.','Tamam','sil');", true);
                        tkod.mesaj("Tamam", "Malzeme Durumu başarıyla silinmiştir.", this);
                    }
                    else
                    {
                        tkod.mesaj("Hata", "Bu malzeme durumu kullanılmaktadır.", this);
                        ScriptManager.RegisterStartupScript(this, this.GetType(), System.Guid.NewGuid().ToString(), "ShowMessage('Bu malzeme durumu kullanılmaktadır.','Hata','sil');", true);
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

                listele_malzemedurum();
            }
            if (lblislem.Text == "markamodel-sil")
            {
                try
                {
                    SqlCommand sorgu = new SqlCommand();
                    sorgu.Connection = conn;
                    conn.Open();

                    sorgu.CommandText = "SELECT COUNT(*) FROM MALZEME_MODEL WHERE MARKA_ID=@MARKDAID ";
                    sorgu.Parameters.AddWithValue("@MARKDAID", lblidsil.Text);
                    int sayi = Convert.ToInt16(sorgu.ExecuteScalar());

                    if (sayi < 1)
                    {
                        sorgu.CommandText = "DELETE FROM MALZEME_MARKAMODEL WHERE ID=@ID";
                        sorgu.Parameters.AddWithValue("@ID", lblidsil.Text);
                        sorgu.ExecuteNonQuery();
                        conn.Close();
                        ScriptManager.RegisterStartupScript(this, this.GetType(), System.Guid.NewGuid().ToString(), "ShowMessage('Malzeme Marka bilgisi başarıyla silinmiştir.','Tamam','sil');", true);
                        tkod.mesaj("Tamam", "Malzeme Marka bilgisi başarıyla silinmiştir.", this);
                    }
                    else
                    {
                        tkod.mesaj("Hata", "Bu markaya bağlı model bilgisi bulunmaktadır.", this);
                        ScriptManager.RegisterStartupScript(this, this.GetType(), System.Guid.NewGuid().ToString(), "ShowMessage('Bu markaya bağlı model bilgisi bulunmaktadır.','Hata','sil');", true);
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

                listele_markamodel();
            }
            if (lblislem.Text == "model-sil")
            {
                try
                {
                    SqlCommand sorgu = new SqlCommand();
                    sorgu.Connection = conn;
                    conn.Open();

                    sorgu.CommandText = "SELECT COUNT(*) FROM MALZEMELER WHERE MODEL=@MODEL87_ID ";
                    sorgu.Parameters.AddWithValue("@MODEL87_ID", lblidsil.Text);
                    int sayi = Convert.ToInt16(sorgu.ExecuteScalar());

                    if (sayi < 1)
                    {
                        sorgu.CommandText = "DELETE FROM MALZEME_MODEL WHERE ID=@ID";
                        sorgu.Parameters.AddWithValue("@ID", lblidsil.Text);
                        sorgu.ExecuteNonQuery();
                        conn.Close();
                        ScriptManager.RegisterStartupScript(this, this.GetType(), System.Guid.NewGuid().ToString(), "ShowMessage('Malzeme Model bilgisi başarıyla silinmiştir.','Tamam','sil');", true);
                        tkod.mesaj("Tamam", "Malzeme Model başarıyla silinmiştir.", this);
                    }
                    else
                    {
                        tkod.mesaj("Hata", "Bu modele bağlı malzeme bulunmaktadır.", this);
                        ScriptManager.RegisterStartupScript(this, this.GetType(), System.Guid.NewGuid().ToString(), "ShowMessage('Bu modele bağlı malzeme bulunmaktadır.','Hata','sil');", true);
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

                listele_model();
            }
        }

        //MALZEME TİPİ

        protected void btntipekle_Click(object sender, EventArgs e)
        {
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            sb.Append(@"<script type='text/javascript'>");
            sb.Append("$(\"#Modal_Tip_Kaydet\").modal(\"show\");");
            sb.Append(@"</script>");
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(),
                       "ModalScript", sb.ToString(), false);

            lblidcihaz.Text = "";
            txttip.Text = "";
            btntipkaydet.Enabled = true;
            btntipkaydet.CssClass = "btn btn-success ";

            lblmodalyenibaslik.Text = "Yeni Tip Ekle";
        }

        protected void btntipkaydet_Click(object sender, EventArgs e)
        {
            try
            {
                SqlCommand sorgu = new SqlCommand();
                sorgu.Connection = conn;
                conn.Open();

                if (lblidcihaz.Text == "")
                {
                    sorgu.CommandText = "SELECT COUNT(*) FROM MALZEME_TIP WHERE TIP=@TP ";
                    sorgu.Parameters.AddWithValue("@TP", txttip.Text);
                    int sayi = Convert.ToInt16(sorgu.ExecuteScalar());

                    if (sayi == 0)
                    {
                        string userid = Session["KULLANICI_ID"].ToString();
                        sorgu.CommandText = "INSERT INTO    MALZEME_TIP (TIP, KAYIT_EDEN , KAYIT_TARIHI)  VALUES(@MTIP, @UI, getdate() ) ";
                        sorgu.Parameters.AddWithValue("@MTIP", txttip.Text);
                        sorgu.Parameters.AddWithValue("@UI", userid);
                        sorgu.ExecuteNonQuery();
                        tkod.mesaj("Tamam", "Tip başarıyla kaydedilmiştir.", this);

                        btntipkaydet.Enabled = false;
                        btntipkaydet.CssClass = "btn btn-success disabled";
                    }
                    else
                    {
                        tkod.mesaj("Hata", "Bu tip daha önce kaydedilmişti.", this);
                    }
                }
                else
                {
                    string userid = Session["KULLANICI_ID"].ToString();
                    sorgu.CommandText = "UPDATE MALZEME_TIP SET TIP=@MT   WHERE ID=@ID ";
                    sorgu.Parameters.AddWithValue("@MT", txttip.Text);
                    sorgu.Parameters.AddWithValue("@ID", userid);
                    sorgu.ExecuteNonQuery();
                    tkod.mesaj("Tamam", "Tip başarıyla güncellenmiştir.", this);
                    btntipkaydet.Enabled = false;
                    btntipkaydet.CssClass = "btn btn-success disabled";
                }
            }
            catch (Exception ex)
            {
                tkod.mesaj("Hata", "Hata oluştu : " + ex.Message.Replace("'", ""), this);
            }

            if (ConnectionState.Open == conn.State)
                conn.Close();
            listele_tip();
        }

        protected void grid_tip_liste_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName.Equals("sil"))
            {
                int index = Convert.ToInt32(e.CommandArgument);
                lblidsil.Text = grid_tip_liste.DataKeys[index].Value.ToString();
                lblislem.Text = "tip-sil";
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
                string id = grid_tip_liste.DataKeys[index].Value.ToString();
                lblidcihaz.Text = id;

                txttip.Text = HttpUtility.HtmlDecode(grid_tip_liste.Rows[index].Cells[1].Text);

                btntipkaydet.Enabled = true;
                btntipkaydet.CssClass = "btn btn-success ";

                lblmodalyenibaslik.Text = "Tip Güncelle";

                System.Text.StringBuilder sb = new System.Text.StringBuilder();
                sb.Append(@"<script type='text/javascript'>");
                sb.Append("$(\"#Modal_Tip_Kaydet\").modal(\"show\");");
                sb.Append(@"</script>");
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(),
                           "ModalScript", sb.ToString(), false);
            }
        }

        void listele_tip()
        {
            SqlCommand cmd;
            string sql = " ", sql1 = " ";

            sql = "SELECT * FROM MALZEME_TIP WHERE ID>0 ";

            sql = sql + sql1;
            cmd = new SqlCommand(sql, conn);
            conn.Open();
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            grid_tip_liste.DataSource = dt;
            grid_tip_liste.DataBind();
            conn.Close();

        }

        //MALZEME TÜRÜ   //önceki adı CİNS

        protected void btncinsekle_Click1(object sender, EventArgs e)
        {
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            sb.Append(@"<script type='text/javascript'>");
            sb.Append("$(\"#Modal_Cins_Kaydet\").modal(\"show\");");
            sb.Append(@"</script>");
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(),
                       "ModalScript", sb.ToString(), false);

            lblidcihaz1.Text = "";
            txtcins.Text = "";
            txtmakssure.Text = "";
            btncinskaydet.Enabled = true;
            btncinskaydet.CssClass = "btn btn-success ";

            lblmodalyenibaslik1.Text = "Yeni Malzeme Türü Ekle";
            listele_tipsec();
        }

        protected void btncinskaydet_Click(object sender, EventArgs e)
        {
            try
            {
                SqlCommand sorgu = new SqlCommand();
                sorgu.Connection = conn;
                conn.Open();

                if (lblidcihaz1.Text == "")
                {
                    sorgu.CommandText = "SELECT COUNT(*) FROM MALZEME_TURU WHERE TURU=@MT ";
                    sorgu.Parameters.AddWithValue("@MT", txtcins.Text);
                    int sayi = Convert.ToInt16(sorgu.ExecuteScalar());

                    if (sayi == 0)
                    {
                        string userid = Session["KULLANICI_ID"].ToString();
                        sorgu.CommandText = "INSERT INTO    MALZEME_TURU (TIP_ID, TURU, MAKS_TAMIR_SURESI, KAYIT_EDEN , KAYIT_TARIHI)  VALUES(@MTID, @MTUR, @MTSURE, @UI, getdate() ) ";
                        sorgu.Parameters.AddWithValue("@MTID", comtipsec.SelectedValue);
                        sorgu.Parameters.AddWithValue("@MTUR", txtcins.Text);
                        sorgu.Parameters.AddWithValue("@MTSURE", txtmakssure.Text);
                        sorgu.Parameters.AddWithValue("@UI", userid);
                        sorgu.ExecuteNonQuery();
                        tkod.mesaj("Tamam", "Tür başarıyla kaydedilmiştir.", this);

                        btntipkaydet.Enabled = false;
                        btntipkaydet.CssClass = "btn btn-success disabled";
                    }
                    else
                    {
                        tkod.mesaj("Hata", "Bu Tür daha önce kaydedilmişti.", this);
                    }
                }
                else
                {
                    string userid = Session["KULLANICI_ID"].ToString();
                    sorgu.CommandText = "UPDATE MALZEME_TURU SET TIP_ID=@MTID_U, TURU=@MTUR_U, MAKS_TAMIR_SURESI=@MTSURE1   WHERE ID=@ID_U ";
                    sorgu.Parameters.AddWithValue("@MTID_U", comtipsec.SelectedValue);
                    sorgu.Parameters.AddWithValue("@MTUR_U", txtcins.Text);
                    sorgu.Parameters.AddWithValue("@MTSURE1", Convert.ToInt16(txtmakssure.Text));
                    sorgu.Parameters.AddWithValue("@ID_U", lblidcihaz1.Text);
                    sorgu.ExecuteNonQuery();
                    tkod.mesaj("Tamam", "Tür başarıyla güncellenmiştir.", this);
                    btncinskaydet.Enabled = false;
                    btncinskaydet.CssClass = "btn btn-success disabled";
                }
            }
            catch (Exception ex)
            {
                tkod.mesaj("Hata", "Hata oluştu : " + ex.Message.Replace("'", ""), this);
            }

            if (ConnectionState.Open == conn.State)
                conn.Close();
            listele_tur();

        }

        protected void grid_cins_liste_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName.Equals("sil"))
            {
                int index = Convert.ToInt32(e.CommandArgument);
                lblidsil.Text = grid_cins_liste.DataKeys[index].Value.ToString();
                lblislem.Text = "malzemeturu-sil";
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
                string id = grid_cins_liste.DataKeys[index].Value.ToString();
                lblidcihaz1.Text = id;

                listele_tipsec();
                comtipsec.SelectedIndex = comtipsec.Items.IndexOf(comtipsec.Items.FindByText(HttpUtility.HtmlDecode(grid_cins_liste.Rows[index].Cells[1].Text)));
                txtcins.Text = HttpUtility.HtmlDecode(grid_cins_liste.Rows[index].Cells[2].Text);
                txtmakssure.Text = HttpUtility.HtmlDecode(grid_cins_liste.Rows[index].Cells[3].Text);

                btncinskaydet.Enabled = true;
                btncinskaydet.CssClass = "btn btn-success ";

                lblmodalyenibaslik1.Text = "Malzeme Türü Güncelle";

                System.Text.StringBuilder sb = new System.Text.StringBuilder();
                sb.Append(@"<script type='text/javascript'>");
                sb.Append("$(\"#Modal_Cins_Kaydet\").modal(\"show\");");
                sb.Append(@"</script>");
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(),
                           "ModalScript", sb.ToString(), false);
            }
        }

        void listele_tur()
        {
            SqlCommand cmd;
            string sql = " ", sql1 = " ";

            sql = "SELECT MT.ID, MT.TURU, MT.MAKS_TAMIR_SURESI, MALZEME_TIP.TIP FROM MALZEME_TURU AS MT " +
                " INNER JOIN MALZEME_TIP ON MALZEME_TIP.ID=MT.TIP_ID WHERE MT.ID>0 ";

            sql = sql + sql1;
            cmd = new SqlCommand(sql, conn);
            conn.Open();
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            grid_cins_liste.DataSource = dt;
            grid_cins_liste.DataBind();
            conn.Close();
        }
        void listele_tipsec()
        {
            comtipsec.Items.Clear();
            comtipsec.Items.Insert(0, new ListItem("Seçiniz", "0"));
            comtipsec.AppendDataBoundItems = true;

            SqlDataAdapter da = new SqlDataAdapter();
            SqlCommand cmd = new SqlCommand("Select ID,TIP FROM MALZEME_TIP ORDER BY ID ");
            DataTable dt = new DataTable();
            da.SelectCommand = cmd;
            da.SelectCommand.Connection = conn;
            da.Fill(dt);
            comtipsec.DataSource = dt;
            comtipsec.DataTextField = "TIP";
            comtipsec.DataValueField = "ID";
            comtipsec.DataBind();
        }

        //DEPO TANIMI

        protected void btndepotanimikaydet_Click(object sender, EventArgs e)
        {
            try
            {
                SqlCommand sorgu = new SqlCommand();
                sorgu.Connection = conn;
                conn.Open();

                if (lblidcihaz6.Text == "")
                {
                    sorgu.CommandText = "SELECT COUNT(*) FROM DEPO_TANIMI WHERE DEPOTANIMI=@DT76 ";
                    sorgu.Parameters.AddWithValue("@DT76", txtdepotanimi.Text);
                    int sayi = Convert.ToInt16(sorgu.ExecuteScalar());

                    if (sayi == 0)
                    {
                        string userid = Session["KULLANICI_ID"].ToString();
                        sorgu.CommandText = "INSERT INTO    DEPO_TANIMI (DEPOTANIMI, GARANTILI_MALZEME_KABUL, GARANTISIZ_MALZEME_KABUL, KAYIT_EDEN , KAYIT_TARIHI)  " +
                            " VALUES(@DTANIMI89, @GLIMLKBL, @GSIZLKBL, @UI, getdate() ) ";
                        sorgu.Parameters.AddWithValue("@DTANIMI89", txtdepotanimi.Text);
                        sorgu.Parameters.AddWithValue("@GLIMLKBL", chc_garantili.Checked);
                        sorgu.Parameters.AddWithValue("@GSIZLKBL", chc_garantisiz.Checked);
                        sorgu.Parameters.AddWithValue("@UI", userid);
                        sorgu.ExecuteNonQuery();
                        ScriptManager.RegisterStartupScript(this, this.GetType(), System.Guid.NewGuid().ToString(), "ShowMessage('Depo Tanımı başarıyla kaydedilmiştir..','Tamam','yeni6');", true);
                        tkod.mesaj("Tamam", "Depo Tanımı başarıyla kaydedilmiştir.", this);

                        btndepotanimikaydet.Enabled = false;
                        btndepotanimikaydet.CssClass = "btn btn-success disabled";
                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(this, this.GetType(), System.Guid.NewGuid().ToString(), "ShowMessage('Bu Depo Tanımı daha önce kaydedilmişti. Lütfen kontrol ediniz.','Hata','yeni6');", true);
                        tkod.mesaj("Hata", "Bu Depo Tanımı dana önce kaydedilmişti.", this);
                    }
                }
                else
                {
                    string userid = Session["KULLANICI_ID"].ToString();
                    sorgu.CommandText = "UPDATE DEPO_TANIMI SET DEPOTANIMI=@DTANIMI88, GARANTILI_MALZEME_KABUL=@GLIMLKBL_U, GARANTISIZ_MALZEME_KABUL=@GSIZLKBL_U    WHERE ID=@DTANIM56_D ";
                    sorgu.Parameters.AddWithValue("@DTANIMI88", txtdepotanimi.Text);
                    sorgu.Parameters.AddWithValue("@GLIMLKBL_U", chc_garantili.Checked);
                    sorgu.Parameters.AddWithValue("@GSIZLKBL_U", chc_garantisiz.Checked);
                    sorgu.Parameters.AddWithValue("@DTANIM56_D", lblidcihaz6.Text);
                    sorgu.ExecuteNonQuery();
                    ScriptManager.RegisterStartupScript(this, this.GetType(), System.Guid.NewGuid().ToString(), "ShowMessage('Depo Tanımı başarıyla güncellenmiştir.','Tamam','yeni6');", true);
                    tkod.mesaj("Tamam", "Depo tanımı güncellenmiştir.", this);
                    btndepotanimikaydet.Enabled = false;
                    btndepotanimikaydet.CssClass = "btn btn-success disabled";
                }
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), System.Guid.NewGuid().ToString(), "ShowMessage('Hata oluştu :'" + ex.Message.Replace("'", "") + ",'Hata','yeni6');", true);
                tkod.mesaj("Hata", "Hata Oluştu: " + ex.Message.Replace("'", ""), this);
            }

            if (ConnectionState.Open == conn.State)
                conn.Close();
            listele_depotanimi1();
        }

        protected void btndepotanimiekle_Click(object sender, EventArgs e)
        {
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            sb.Append(@"<script type='text/javascript'>");
            sb.Append("$(\"#Modal_DepoTanimi_Kaydet\").modal(\"show\");");
            sb.Append(@"</script>");
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(),
                       "ModalScript", sb.ToString(), false);

            lblidcihaz6.Text = "";
            txtdepotanimi.Text = "";
            btndepotanimikaydet.Enabled = true;
            btndepotanimikaydet.CssClass = "btn btn-success ";

            lblmodalyenibaslik6.Text = "Yeni Depo Tanımı Ekle";
        }

        protected void grid_depotanimi_liste_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName.Equals("sil"))
            {
                int index = Convert.ToInt32(e.CommandArgument);
                lblidsil.Text = grid_depotanimi_liste.DataKeys[index].Value.ToString();
                lblislem.Text = "depotanimi-sil";
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
                string id = grid_depotanimi_liste.DataKeys[index].Value.ToString();
                lblidcihaz6.Text = id;

                txtdepotanimi.Text = HttpUtility.HtmlDecode(grid_depotanimi_liste.Rows[index].Cells[1].Text);

                chc_garantili.Checked = Convert.ToBoolean(tkod.sql_calistir_param("SELECT GARANTILI_MALZEME_KABUL FROM DEPO_TANIMI WHERE ID=@ID98", new SqlParameter("ID98", id)));
                chc_garantisiz.Checked = Convert.ToBoolean(tkod.sql_calistir_param("SELECT GARANTISIZ_MALZEME_KABUL FROM DEPO_TANIMI WHERE ID=@ID97", new SqlParameter("ID97", id)));


                btndepotanimikaydet.Enabled = true;
                btndepotanimikaydet.CssClass = "btn btn-success ";

                lblmodalyenibaslik6.Text = "Depo Tanımı Güncelle";

                System.Text.StringBuilder sb = new System.Text.StringBuilder();
                sb.Append(@"<script type='text/javascript'>");
                sb.Append("$(\"#Modal_DepoTanimi_Kaydet\").modal(\"show\");");
                sb.Append(@"</script>");
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(),
                           "ModalScript", sb.ToString(), false);
            }
        }

        void listele_depotanimi1()
        {
            SqlCommand cmd;
            string sql = " ", sql1 = " ";

            sql = " SELECT DT.ID, DT.DEPOTANIMI, DT.GARANTILI_MALZEME_KABUL, DT.GARANTISIZ_MALZEME_KABUL, KULLANICI.ISIM + ' ' +CONVERT(NVARCHAR(10),DT.KAYIT_TARIHI,104) AS KAYIT FROM DEPO_TANIMI AS DT " +
                "LEFT JOIN KULLANICI ON KULLANICI.ID=DT.KAYIT_EDEN " +
                "WHERE DT.ID > 0  ";

            sql = sql + sql1;
            cmd = new SqlCommand(sql, conn);
            conn.Open();
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            grid_depotanimi_liste.DataSource = dt;
            grid_depotanimi_liste.DataBind();
            conn.Close();
        }


        //DEPO TÜRÜ

        protected void btndepoturkaydet_Click(object sender, EventArgs e)
        {
            try
            {
                SqlCommand sorgu = new SqlCommand();
                sorgu.Connection = conn;
                conn.Open();

                if (lblidcihaz2.Text == "")
                {
                    sorgu.CommandText = "SELECT COUNT(*) FROM DEPO_TURU WHERE DEPOTURU=@DT ";
                    sorgu.Parameters.AddWithValue("@DT", txtdepotur.Text);
                    int sayi = Convert.ToInt16(sorgu.ExecuteScalar());

                    if (sayi == 0)
                    {
                        string userid = Session["KULLANICI_ID"].ToString();
                        sorgu.CommandText = "INSERT INTO    DEPO_TURU (DEPOTURU, DEPOTANIMI_ID, KAYIT_EDEN , KAYIT_TARIHI)  VALUES(@DTUR, @DTANIM, @UI, getdate() ) ";
                        sorgu.Parameters.AddWithValue("@DTUR", txtdepotur.Text);
                        sorgu.Parameters.AddWithValue("@DTANIM", comdepotanimi.SelectedValue);
                        sorgu.Parameters.AddWithValue("@UI", userid);
                        sorgu.ExecuteNonQuery();
                        tkod.mesaj("Tamam", "Depo Türü başarıyla kaydedilmiştir.", this);

                        btndepoturkaydet.Enabled = false;
                        btndepoturkaydet.CssClass = "btn btn-success disabled";
                    }
                    else
                    {
                        tkod.mesaj("Hata", "Bu depo türü dana önce kaydedilmişti.", this);
                    }
                }
                else
                {
                    string userid = Session["KULLANICI_ID"].ToString();
                    sorgu.CommandText = "UPDATE DEPO_TURU SET DEPOTURU=@DTUR, DEPOTANIMI_ID=@DTANIM   WHERE ID=@DTID ";
                    sorgu.Parameters.AddWithValue("@DTUR", txtdepotur.Text);
                    sorgu.Parameters.AddWithValue("@DTANIM", comdepotanimi.SelectedValue);
                    sorgu.Parameters.AddWithValue("@DTID", lblidcihaz2.Text);
                    sorgu.ExecuteNonQuery();
                    tkod.mesaj("Tamam", "Depo türü güncellenmiştir.", this);
                    btndepoturkaydet.Enabled = false;
                    btndepoturkaydet.CssClass = "btn btn-success disabled";
                }
            }
            catch (Exception ex)
            {
                tkod.mesaj("Hata", "Hata Oluştu: " + ex.Message.Replace("'", ""), this);
            }

            if (ConnectionState.Open == conn.State)
                conn.Close();
            listele_depotur();
        }

        protected void btndepoturekle_Click(object sender, EventArgs e)
        {
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            sb.Append(@"<script type='text/javascript'>");
            sb.Append("$(\"#Modal_DepoTur_Kaydet\").modal(\"show\");");
            sb.Append(@"</script>");
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(),
                       "ModalScript", sb.ToString(), false);

            lblidcihaz2.Text = "";
            txtdepotur.Text = "";
            btndepoturkaydet.Enabled = true;
            btndepoturkaydet.CssClass = "btn btn-success ";

            lblmodalyenibaslik2.Text = "Yeni Depo Türü Ekle";
        }

        protected void grid_depotur_liste_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName.Equals("sil"))
            {
                int index = Convert.ToInt32(e.CommandArgument);
                lblidsil.Text = grid_depotur_liste.DataKeys[index].Value.ToString();
                lblislem.Text = "depoturu-sil";
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
                string id = grid_depotur_liste.DataKeys[index].Value.ToString();
                lblidcihaz2.Text = id;

                comdepotanimi.SelectedIndex = comdepotanimi.Items.IndexOf(comdepotanimi.Items.FindByText(HttpUtility.HtmlDecode(grid_depotur_liste.Rows[index].Cells[1].Text)));
                txtdepotur.Text = HttpUtility.HtmlDecode(grid_depotur_liste.Rows[index].Cells[2].Text);

                btndepoturkaydet.Enabled = true;
                btndepoturkaydet.CssClass = "btn btn-success ";

                lblmodalyenibaslik2.Text = "Depo Türü Güncelle";

                System.Text.StringBuilder sb = new System.Text.StringBuilder();
                sb.Append(@"<script type='text/javascript'>");
                sb.Append("$(\"#Modal_DepoTur_Kaydet\").modal(\"show\");");
                sb.Append(@"</script>");
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(),
                           "ModalScript", sb.ToString(), false);
            }
        }

        void listele_depotur()
        {
            SqlCommand cmd;
            string sql = " ", sql1 = " ";

            sql = " SELECT DTUR.ID, DTUR.DEPOTURU, DEPO_TANIMI.DEPOTANIMI, KULLANICI.ISIM + ' ' +CONVERT(NVARCHAR(10),DTUR.KAYIT_TARIHI,104) AS KAYIT FROM DEPO_TURU AS DTUR " +
                "LEFT JOIN DEPO_TANIMI ON DEPO_TANIMI.ID=DTUR.DEPOTANIMI_ID " +
                "LEFT JOIN KULLANICI ON KULLANICI.ID=DTUR.KAYIT_EDEN " +
                "WHERE DTUR.ID > 0  ";

            sql = sql + sql1;
            cmd = new SqlCommand(sql, conn);
            conn.Open();
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            grid_depotur_liste.DataSource = dt;
            grid_depotur_liste.DataBind();
            conn.Close();
        }

        //MALZEME DURUMU

        protected void btnmalzemedurumekle_Click(object sender, EventArgs e)
        {
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            sb.Append(@"<script type='text/javascript'>");
            sb.Append("$(\"#Modal_MalzemeDurum_Kaydet\").modal(\"show\");");
            sb.Append(@"</script>");
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(),
                       "ModalScript", sb.ToString(), false);

            lblidcihaz3.Text = "";
            txtmalzemedurum.Text = "";
            btnmalzemedurumkaydet.Enabled = true;
            btnmalzemedurumkaydet.CssClass = "btn btn-success ";

            lblmodalyenibaslik3.Text = "Yeni Malzeme Durum Ekle";
        }

        protected void grid_malzemedurum_liste_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName.Equals("sil"))
            {
                int index = Convert.ToInt32(e.CommandArgument);
                lblidsil.Text = grid_malzemedurum_liste.DataKeys[index].Value.ToString();
                lblislem.Text = "malzemedurum-sil";
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
                string id = grid_malzemedurum_liste.DataKeys[index].Value.ToString();
                lblidcihaz3.Text = id;

                txtmalzemedurum.Text = HttpUtility.HtmlDecode(grid_malzemedurum_liste.Rows[index].Cells[1].Text);

                btnmalzemedurumkaydet.Enabled = true;
                btnmalzemedurumkaydet.CssClass = "btn btn-success ";

                lblmodalyenibaslik3.Text = "Depo Durum Güncelle";

                System.Text.StringBuilder sb = new System.Text.StringBuilder();
                sb.Append(@"<script type='text/javascript'>");
                sb.Append("$(\"#Modal_MalzemeDurum_Kaydet\").modal(\"show\");");
                sb.Append(@"</script>");
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(),
                           "ModalScript", sb.ToString(), false);
            }
        }

        protected void btnmalzemedurumkaydet_Click(object sender, EventArgs e)
        {
            try
            {
                SqlCommand sorgu = new SqlCommand();
                sorgu.Connection = conn;
                conn.Open();

                if (lblidcihaz3.Text == "")
                {
                    sorgu.CommandText = "SELECT COUNT(*) FROM MALZEME_DURUM WHERE DURUM=@MDD ";
                    sorgu.Parameters.AddWithValue("MDD", txtmalzemedurum.Text);
                    int sayi = Convert.ToInt16(sorgu.ExecuteScalar());

                    if (sayi == 0)
                    {
                        string userid = Session["KULLANICI_ID"].ToString();
                        sorgu.CommandText = "INSERT INTO    MALZEME_DURUM (DURUM, KAYIT_EDEN , KAYIT_TARIHI)  VALUES(@MDD1, @UI1, getdate() ) ";
                        sorgu.Parameters.AddWithValue("@MDD1", txtmalzemedurum.Text);
                        sorgu.Parameters.AddWithValue("@UI1", userid);
                        sorgu.ExecuteNonQuery();
                        tkod.mesaj("Tamam", "Malzeme Durumu Kaydedilmiştir..", this);

                        btnmalzemedurumkaydet.Enabled = false;
                        btnmalzemedurumkaydet.CssClass = "btn btn-success disabled";
                    }
                    else
                    {
                        tkod.mesaj("Hata", "Bu malzeme durumu daha önce kaydedilmişti", this);
                    }
                }
                else
                {
                    string userid = Session["KULLANICI_ID"].ToString();
                    sorgu.CommandText = "UPDATE MALZEME_DURUM SET DURUM=@DD   WHERE ID=@DID ";
                    sorgu.Parameters.AddWithValue("@DD", txtmalzemedurum.Text);
                    sorgu.Parameters.AddWithValue("@DID", lblidcihaz3.Text);
                    sorgu.ExecuteNonQuery();
                    tkod.mesaj("Tamam", "Malzeme durumu başarıyla güncellenmiştir.", this);
                    btnmalzemedurumkaydet.Enabled = false;
                    btnmalzemedurumkaydet.CssClass = "btn btn-success disabled";
                }
            }
            catch (Exception ex)
            {
                tkod.mesaj("Hata", "Hata oluştu: " + ex.Message.Replace("'", ""), this);
            }

            if (ConnectionState.Open == conn.State)
                conn.Close();
            listele_malzemedurum();
        }

        void listele_depotanimi()
        {
            comdepotanimi.Items.Clear();
            comdepotanimi.Items.Insert(0, new ListItem("Seçiniz", "0"));
            comdepotanimi.AppendDataBoundItems = true;

            SqlDataAdapter da = new SqlDataAdapter();
            SqlCommand cmd = new SqlCommand("Select ID,DEPOTANIMI FROM DEPO_TANIMI ORDER BY ID ");
            DataTable dt = new DataTable();
            da.SelectCommand = cmd;
            da.SelectCommand.Connection = conn;
            da.Fill(dt);
            comdepotanimi.DataSource = dt;
            comdepotanimi.DataTextField = "DEPOTANIMI";
            comdepotanimi.DataValueField = "ID";
            comdepotanimi.DataBind();
        } //doldur

        void listele_malzemedurum()
        {
            SqlCommand cmd;
            string sql = " ", sql1 = " ";

            sql = "SELECT * FROM MALZEME_DURUM WHERE ID>0 ";

            sql = sql + sql1;
            cmd = new SqlCommand(sql, conn);
            conn.Open();
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            grid_malzemedurum_liste.DataSource = dt;
            grid_malzemedurum_liste.DataBind();
            conn.Close();
        }

        //MARKA 
        protected void btnmarkamodelekle_Click(object sender, EventArgs e)
        {
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            sb.Append(@"<script type='text/javascript'>");
            sb.Append("$(\"#Modal_MarkaModel_Kaydet\").modal(\"show\");");
            sb.Append(@"</script>");
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(),
                       "ModalScript", sb.ToString(), false);

            lblidcihaz4.Text = "";
            txtmarka.Text = "";
            txtmodel.Text = "";
            btnmarkamodelkaydet.Enabled = true;
            btnmarkamodelkaydet.CssClass = "btn btn-success ";

            lblmodalyenibaslik4.Text = "Yeni Marka Ekle";

            Malzeme_Tip_Listele();
        }

        void listele_markamodel()
        {
            SqlCommand cmd;
            string sql = " ", sql1 = " ";

            sql = " SELECT MMM.ID, MMM.MARKA, MM.MODEL, MALZEME_TIP.TIP, MALZEME_TURU.TURU FROM MALZEME_MARKAMODEL AS MMM " +
                " INNER JOIN MALZEME_TIP ON MALZEME_TIP.ID=MMM.TIP_ID " +
                " INNER JOIN MALZEME_TURU ON MALZEME_TURU.ID=MMM.TUR_ID " +
                " INNER JOIN MALZEME_MODEL MM ON MM.MARKA_ID=MMM.ID " +
                " WHERE MMM.ID>0 ORDER BY MMM.ID, MALZEME_TIP.TIP, MALZEME_TURU.TURU ";


            sql = sql + sql1;
            cmd = new SqlCommand(sql, conn);
            conn.Open();
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            grid_markamodel_liste.DataSource = dt;
            grid_markamodel_liste.DataBind();
            conn.Close();
        }

        protected void grid_markamodel_liste_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName.Equals("sil"))
            {
                int index = Convert.ToInt32(e.CommandArgument);
                lblidsil.Text = grid_markamodel_liste.DataKeys[index].Value.ToString();
                lblislem.Text = "markamodel-sil";
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
                string id = grid_markamodel_liste.DataKeys[index].Value.ToString();
                lblidcihaz4.Text = id;
                Malzeme_Tip_Listele();
                commarkamodel_tipsec.SelectedIndex = commarkamodel_tipsec.Items.IndexOf(commarkamodel_tipsec.Items.FindByText(HttpUtility.HtmlDecode(grid_markamodel_liste.Rows[index].Cells[1].Text)));

                Malzeme_Tur_Listele();
                commarkamodel_tursec.SelectedIndex = commarkamodel_tursec.Items.IndexOf(commarkamodel_tursec.Items.FindByText(HttpUtility.HtmlDecode(grid_markamodel_liste.Rows[index].Cells[2].Text)));
                txtmarka.Text = HttpUtility.HtmlDecode(grid_markamodel_liste.Rows[index].Cells[3].Text);
                txtmodel.Text = HttpUtility.HtmlDecode(grid_markamodel_liste.Rows[index].Cells[4].Text);

                btnmarkamodelkaydet.Enabled = true;
                btnmarkamodelkaydet.CssClass = "btn btn-success ";

                lblmodalyenibaslik4.Text = "Marka Güncelle";

                System.Text.StringBuilder sb = new System.Text.StringBuilder();
                sb.Append(@"<script type='text/javascript'>");
                sb.Append("$(\"#Modal_MarkaModel_Kaydet\").modal(\"show\");");
                sb.Append(@"</script>");
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(),
                           "ModalScript", sb.ToString(), false);
            }
        }

        protected void btnmarkamodelkaydet_Click(object sender, EventArgs e)
        {
            try
            {
                SqlCommand sorgu = new SqlCommand();
                sorgu.Connection = conn;
                conn.Open();

                if (lblidcihaz4.Text == "")
                {
                    string userid = Session["KULLANICI_ID"].ToString();
                    sorgu.CommandText = "INSERT INTO    MALZEME_MARKAMODEL (TIP_ID, TUR_ID, MARKA, KAYIT_EDEN , KAYIT_TARIHI)  VALUES(@MMM_TIPID, @MMM_TURID, @MMM_M, @UI2, getdate() ) ";
                    sorgu.Parameters.AddWithValue("@MMM_TIPID", commarkamodel_tipsec.SelectedValue);
                    sorgu.Parameters.AddWithValue("@MMM_TURID", commarkamodel_tursec.SelectedValue);
                    sorgu.Parameters.AddWithValue("@MMM_M", txtmarka.Text);
                    sorgu.Parameters.AddWithValue("@UI2", userid);
                    sorgu.ExecuteNonQuery();
                    tkod.mesaj("Tamam", "Marka bilgisi başarıyla kaydedilmiştir.", this);
                    btnmarkamodelkaydet.Enabled = false;
                    btnmarkamodelkaydet.CssClass = "btn btn-success disabled";
                }
                else
                {
                    string userid = Session["KULLANICI_ID"].ToString();
                    sorgu.CommandText = "UPDATE MALZEME_MARKAMODEL SET TIP_ID=@MMM_TIPID_U, TUR_ID=@MMM_TURID_U, MARKA=@MMM_M_U   WHERE ID=@MMM_ID_U ";
                    sorgu.Parameters.AddWithValue("@MMM_TIPID_U", commarkamodel_tipsec.SelectedValue);
                    sorgu.Parameters.AddWithValue("@MMM_TURID_U", commarkamodel_tursec.SelectedValue);
                    sorgu.Parameters.AddWithValue("@MMM_M_U", txtmarka.Text);
                    sorgu.Parameters.AddWithValue("@MMM_ID_U", lblidcihaz4.Text);
                    sorgu.ExecuteNonQuery();
                    tkod.mesaj("Tamam", "Marka bilgisi başarıyla güncellenmiştir.", this);
                    btnmarkamodelkaydet.Enabled = false;
                    btnmarkamodelkaydet.CssClass = "btn btn-success disabled";
                }
            }
            catch (Exception ex)
            {
                tkod.mesaj("Hata", "Hata Oluştu: " + ex.Message.Replace("'", ""), this);
            }

            if (ConnectionState.Open == conn.State)
                conn.Close();
            listele_markamodel();
        }

        protected void commarkamodel_tipsec_SelectedIndexChanged(object sender, EventArgs e)
        {
            Malzeme_Tur_Listele();
        }

        public void comdoldur(DropDownList com, string sql, string text, string id)
        {
            conn.Open();
            com.Items.Clear();
            com.Items.Insert(0, new ListItem("-", "0"));
            com.AppendDataBoundItems = true;

            SqlDataAdapter da = new SqlDataAdapter();
            SqlCommand sorgu = new SqlCommand(sql);
            DataTable dt = new DataTable();
            da.SelectCommand = sorgu;
            da.SelectCommand.Connection = conn;
            da.Fill(dt);
            com.DataSource = dt;
            com.DataTextField = text;
            com.DataValueField = id;
            com.DataBind();
            conn.Close();
        }

        void Malzeme_Tip_Listele()
        {
            //kaydet modal içindeki comboboxları doldurmak için
            SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["KabloHE"].ConnectionString);

            conn.Open();
            commarkamodel_tipsec.Items.Clear();
            commarkamodel_tipsec.Items.Insert(0, new ListItem("-", "0"));
            commarkamodel_tipsec.AppendDataBoundItems = true;
            string sql1 = "";

            SqlDataAdapter da = new SqlDataAdapter();
            SqlCommand sorgu = new SqlCommand("Select ID,TIP FROM MALZEME_TIP ORDER BY ID ");
            DataTable dt = new DataTable();
            da.SelectCommand = sorgu;
            da.SelectCommand.Connection = conn;
            da.Fill(dt);
            commarkamodel_tipsec.DataSource = dt;
            commarkamodel_tipsec.DataTextField = "TIP";
            commarkamodel_tipsec.DataValueField = "ID";
            commarkamodel_tipsec.DataBind();
            conn.Close();
        }

        void Malzeme_Tur_Listele()
        {
            //kaydet modal içindeki comboboxları doldurmak için
            commarkamodel_tursec.Items.Clear();
            commarkamodel_tursec.Items.Insert(0, new ListItem("Seçiniz", "0"));
            commarkamodel_tursec.AppendDataBoundItems = true;
            string sql2 = "";

            if (commarkamodel_tipsec.SelectedIndex > 0)
                sql2 = " AND TIP_ID='" + commarkamodel_tipsec.SelectedValue.ToString() + "' ";

            string sql = "SELECT ID, TURU FROM MALZEME_TURU WHERE ID>0 " + sql2 + " ORDER BY ID";
            commarkamodel_tursec.DataSource = tkod.GetData(sql);
            commarkamodel_tursec.DataBind();
        }

        // MODEL
        protected void btnmodelekle_Click(object sender, EventArgs e)
        {
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            sb.Append(@"<script type='text/javascript'>");
            sb.Append("$(\"#Modal_Model_Kaydet\").modal(\"show\");");
            sb.Append(@"</script>");
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(),
                       "ModalScript", sb.ToString(), false);

            lblidcihaz5.Text = "";
            txtmodel.Text = "";
            btnmodelkaydet.Enabled = true;
            btnmodelkaydet.CssClass = "btn btn-success ";

            lblmodalyenibaslik5.Text = "Yeni Model Ekle";

            comdoldur(commodel_tip, "Select ID,TIP FROM MALZEME_TIP ORDER BY ID", "TIP", "ID");
            Malzeme_Tur_MODEL_Listele();
            MALZEME_MARKAMODEL_Listele();
        }

        void listele_model()
        {
            SqlCommand cmd;
            string sql = " ", sql1 = " ";

            sql = " SELECT MModel.ID, MModel.MODEL, MALZEME_MARKAMODEL.MARKA,MTIP.TIP, MTUR.TURU FROM MALZEME_MODEL AS MModel " +
                "  INNER JOIN MALZEME_MARKAMODEL ON MALZEME_MARKAMODEL.ID=MModel.MARKA_ID  " +
                "  INNER JOIN MALZEME_TIP AS MTIP ON MTIP.ID=MALZEME_MARKAMODEL.TIP_ID  " +
                "  INNER JOIN MALZEME_TURU AS MTUR ON MTUR.ID=MALZEME_MARKAMODEL.TUR_ID WHERE MModel.ID>0  ";

            sql = sql + sql1;
            cmd = new SqlCommand(sql, conn);
            conn.Open();
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            grid_model_liste.DataSource = dt;
            grid_model_liste.DataBind();
            conn.Close();
        }

        void MALZEME_MARKAMODEL_Listele()
        {
            //kaydet modal içindeki comboboxları doldurmak için
            SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["KabloHE"].ConnectionString);

            conn.Open();
            commarka_sec.Items.Clear();
            commarka_sec.Items.Insert(0, new ListItem("-", "0"));
            commarka_sec.AppendDataBoundItems = true;
            string sql1 = "";

            sql1 = " AND TUR_ID='" + commodel_tur.SelectedValue.ToString() + "' ";

            SqlDataAdapter da = new SqlDataAdapter();
            SqlCommand sorgu = new SqlCommand("Select ID,MARKA FROM MALZEME_MARKAMODEL WHERE ID>0 " + sql1 + " GROUP BY ID,MARKA ORDER BY ID ");
            DataTable dt = new DataTable();
            da.SelectCommand = sorgu;
            da.SelectCommand.Connection = conn;
            da.Fill(dt);
            commarka_sec.DataSource = dt;
            commarka_sec.DataTextField = "MARKA";
            commarka_sec.DataValueField = "ID";
            commarka_sec.DataBind();
            conn.Close();
        }

        void Malzeme_Tur_MODEL_Listele()
        {
            //kaydet modal içindeki comboboxları doldurmak için
            commodel_tur.Items.Clear();
            commodel_tur.Items.Insert(0, new ListItem("Seçiniz", "0"));
            commodel_tur.AppendDataBoundItems = true;
            string sql2 = "";

            sql2 = " AND TIP_ID='" + commodel_tip.SelectedValue.ToString() + "' ";

            string sql = "SELECT ID, TURU FROM MALZEME_TURU WHERE ID>0 " + sql2 + " ORDER BY ID";
            commodel_tur.DataSource = tkod.GetData(sql);
            commodel_tur.DataBind();
        }

        protected void grid_model_liste_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName.Equals("sil"))
            {
                int index = Convert.ToInt32(e.CommandArgument);
                lblidsil.Text = grid_model_liste.DataKeys[index].Value.ToString();
                lblislem.Text = "model-sil";
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
                string id = grid_model_liste.DataKeys[index].Value.ToString();
                lblidcihaz5.Text = id;

                comdoldur(commodel_tip, "Select ID,TIP FROM MALZEME_TIP ORDER BY ID", "TIP", "ID");
                commodel_tip.SelectedIndex = commodel_tip.Items.IndexOf(commodel_tip.Items.FindByText(HttpUtility.HtmlDecode(grid_model_liste.Rows[index].Cells[1].Text)));
                Malzeme_Tur_MODEL_Listele();
                commodel_tur.SelectedIndex = commodel_tur.Items.IndexOf(commodel_tur.Items.FindByText(HttpUtility.HtmlDecode(grid_model_liste.Rows[index].Cells[2].Text)));

                MALZEME_MARKAMODEL_Listele();
                commarka_sec.SelectedIndex = commarka_sec.Items.IndexOf(commarka_sec.Items.FindByText(HttpUtility.HtmlDecode(grid_model_liste.Rows[index].Cells[3].Text)));

                txtmodel.Text = HttpUtility.HtmlDecode(grid_model_liste.Rows[index].Cells[4].Text);

                btnmodelkaydet.Enabled = true;
                btnmodelkaydet.CssClass = "btn btn-success ";

                lblmodalyenibaslik5.Text = "Model Güncelle";

                System.Text.StringBuilder sb = new System.Text.StringBuilder();
                sb.Append(@"<script type='text/javascript'>");
                sb.Append("$(\"#Modal_Model_Kaydet\").modal(\"show\");");
                sb.Append(@"</script>");
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(),
                           "ModalScript", sb.ToString(), false);
            }
        }

        protected void btnmodelkaydet_Click(object sender, EventArgs e)
        {
            try
            {
                SqlCommand sorgu = new SqlCommand();
                sorgu.Connection = conn;
                conn.Open();

                if (lblidcihaz5.Text == "")
                {
                    sorgu.CommandText = "SELECT COUNT(*) FROM MALZEME_MODEL WHERE MODEL=@MMM ";
                    sorgu.Parameters.AddWithValue("MMM", txtmodel.Text);
                    int sayi = Convert.ToInt16(sorgu.ExecuteScalar());

                    if (sayi == 0)
                    {
                        string userid = Session["KULLANICI_ID"].ToString();
                        sorgu.CommandText = "INSERT INTO    MALZEME_MODEL (MARKA_ID, MODEL, KAYIT_EDEN , KAYIT_TARIHI)  VALUES(@MMM_ID, @MMMODEL, @UI4, getdate() ) ";
                        sorgu.Parameters.AddWithValue("@MMM_ID", commarka_sec.SelectedValue);
                        sorgu.Parameters.AddWithValue("@MMMODEL", txtmodel.Text);
                        sorgu.Parameters.AddWithValue("@UI4", userid);
                        sorgu.ExecuteNonQuery();
                        tkod.mesaj("Tamam", "Model bilgisi başarıyla kaydedilmiştir.", this);

                        btnmodelkaydet.Enabled = false;
                        btnmodelkaydet.CssClass = "btn btn-success disabled";
                    }
                    else
                    {
                        tkod.mesaj("Hata", "Bu Model daha önce kaydedilmişti.", this);
                    }
                }
                else
                {
                    string userid = Session["KULLANICI_ID"].ToString();
                    sorgu.CommandText = "UPDATE MALZEME_MODEL SET MARKA_ID=@MMM_ID_U, MODEL=@MMMODEL_U   WHERE ID=@MMID_U ";
                    sorgu.Parameters.AddWithValue("@MMM_ID_U", commarka_sec.SelectedValue);
                    sorgu.Parameters.AddWithValue("@MMMODEL_U", txtmodel.Text);
                    sorgu.Parameters.AddWithValue("@MMID_U", lblidcihaz5.Text);
                    sorgu.ExecuteNonQuery();

                    tkod.mesaj("Tamam", "Model bilgisi başarıyla güncellenmiştir.", this);
                    btnmodelkaydet.Enabled = false;
                    btnmodelkaydet.CssClass = "btn btn-success disabled";
                }
            }
            catch (Exception ex)
            {
                tkod.mesaj("Hata", "Hata olustu: " + ex.Message.Replace("'", ""), this);
            }

            if (ConnectionState.Open == conn.State)
                conn.Close();
            listele_model();
        }

        protected void commodel_tip_SelectedIndexChanged(object sender, EventArgs e)
        {
            Malzeme_Tur_MODEL_Listele();
        }

        protected void commodel_tur_SelectedIndexChanged(object sender, EventArgs e)
        {
            MALZEME_MARKAMODEL_Listele();
        }

        protected void commodel_tip_DataBound(object sender, EventArgs e)
        {
            Malzeme_Tur_MODEL_Listele();
        }

        protected void commodel_tur_DataBound(object sender, EventArgs e)
        {
            MALZEME_MARKAMODEL_Listele();
        }

        protected void chc_garantili_CheckedChanged(object sender, EventArgs e)
        {

        }

        protected void chc_garantisiz_CheckedChanged(object sender, EventArgs e)
        {

        }
    }
}