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
    public partial class VarlikAyar : System.Web.UI.Page
    {
        SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["KabloHE"].ConnectionString);
        string sayfa = "TamirAyar";
        DataTable dt;
        KodT.kodlar tkod = new KodT.kodlar();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                listele_varlik_tip();
                listele_lokasyon();
                listele_varlik_ad();
            }
        }

        protected void btnsil_Click(object sender, EventArgs e)
        {
            if (lblislem.Text == "lokasyon-sil")
            {
                try
                {
                    SqlCommand sorgu = new SqlCommand();
                    sorgu.Connection = conn;
                    conn.Open();

                    sorgu.CommandText = "SELECT COUNT(*) FROM VARLIK_TIP WHERE V_LOKASYON_ID=@VLOK97_ID ";
                    sorgu.Parameters.AddWithValue("@VLOK97_ID", lblidsil.Text);
                    int sayi = Convert.ToInt16(sorgu.ExecuteScalar());

                    if (sayi == 0)
                    {
                        sorgu.CommandText = "DELETE FROM VARLIK_LOKASYON WHERE ID=@VL_ID1";
                        sorgu.Parameters.AddWithValue("@VL_ID1", lblidsil.Text);
                        sorgu.ExecuteNonQuery();
                        conn.Close();
                        ScriptManager.RegisterStartupScript(this, this.GetType(), System.Guid.NewGuid().ToString(), "YeniMesaj('Varlık Lokasyonu başarıyla silinmiştir.','Tamam');", true);
                        ScriptManager.RegisterStartupScript(this, this.GetType(), System.Guid.NewGuid().ToString(), "ShowMessage('Varlık Lokasyonu başarıyla silinmiştir.','Tamam','sil');", true);
                    }
                    else
                    {
                        tkod.mesaj("Hata", "Bu Varlık Lokasyonu bir Varlık Tipine bağlı bulunmaktadır.", this);
                        ScriptManager.RegisterStartupScript(this, this.GetType(), System.Guid.NewGuid().ToString(), "ShowMessage('Bu Varlık Lokasyonu bir Varlık Tipine bağlı bulunmaktadır.','Hata','sil');", true);
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

                listele_lokasyon();
            }
            if (lblislem.Text == "tip-sil")
            {
                try
                {
                    SqlCommand sorgu = new SqlCommand();
                    sorgu.Connection = conn;
                    conn.Open();

                    sorgu.CommandText = "SELECT COUNT(*) FROM VARLIK_ADI WHERE V_TIP_ID=@VTIP97_ID ";
                    sorgu.Parameters.AddWithValue("@VTIP97_ID", lblidsil.Text);
                    int sayi = Convert.ToInt16(sorgu.ExecuteScalar());

                    if (sayi == 0)
                    {
                        sorgu.CommandText = "DELETE FROM VARLIK_TIP WHERE ID=@VL_ID2";
                        sorgu.Parameters.AddWithValue("@VL_ID2", lblidsil.Text);
                        sorgu.ExecuteNonQuery();
                        conn.Close();

                        ScriptManager.RegisterStartupScript(this, this.GetType(), System.Guid.NewGuid().ToString(), "YeniMesaj('Varlık Tipi başarıyla silinmiştir.','Tamam');", true);
                        ScriptManager.RegisterStartupScript(this, this.GetType(), System.Guid.NewGuid().ToString(), "ShowMessage('Varlık Tipi başarıyla silinmiştir.','Tamam','sil');", true);
                    }
                    else
                    {
                        tkod.mesaj("Hata", "Bu Varlık tipi bir Varlık adına bağlı bulunmaktadır.", this);
                        ScriptManager.RegisterStartupScript(this, this.GetType(), System.Guid.NewGuid().ToString(), "ShowMessage('Bu Varlık tipi bir Varlık adına bağlı bulunmaktadır.','Hata','sil');", true);
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

                listele_varlik_tip();
            }
            if (lblislem.Text == "ad-sil")
            {
                try
                {
                    SqlCommand sorgu = new SqlCommand();
                    sorgu.Connection = conn;
                    conn.Open();

                    sorgu.CommandText = "SELECT COUNT(*) FROM VARLIK WHERE V_ADI_ID=@VADI97_ID ";
                    sorgu.Parameters.AddWithValue("@VADI97_ID", lblidsil.Text);
                    int sayi = Convert.ToInt16(sorgu.ExecuteScalar());

                    if (sayi == 0)
                    {
                        sorgu.CommandText = "DELETE FROM VARLIK_ADI WHERE ID=@VL_ID3";
                        sorgu.Parameters.AddWithValue("@VL_ID3", lblidsil.Text);
                        sorgu.ExecuteNonQuery();
                        conn.Close();

                        ScriptManager.RegisterStartupScript(this, this.GetType(), System.Guid.NewGuid().ToString(), "YeniMesaj('Varlık Adı başarıyla silinmiştir.','Tamam');", true);
                        ScriptManager.RegisterStartupScript(this, this.GetType(), System.Guid.NewGuid().ToString(), "ShowMessage('Varlık Adı başarıyla silinmiştir.','Tamam','sil');", true);
                    }
                    else
                    {
                        tkod.mesaj("Hata", "Bu Varlık Adı bir Varlık a bağlı bulunmaktadır.", this);
                        ScriptManager.RegisterStartupScript(this, this.GetType(), System.Guid.NewGuid().ToString(), "ShowMessage('Bu Varlık Adı bir Varlık a bağlı bulunmaktadır.','Hata','sil');", true);
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

                listele_varlik_ad();
            }
        }

        //Varlık Lokasyon

        protected void btnvarliklokasyon_ekle_Click(object sender, EventArgs e)
        {
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            sb.Append(@"<script type='text/javascript'>");
            sb.Append("$(\"#Modal_Lokasyon_Kaydet\").modal(\"show\");");
            sb.Append(@"</script>");
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(),
                       "ModalScript", sb.ToString(), false);

            lblidcihaz.Text = "";
            txtlokasyon.Text = "";
            btnlokasyon_kaydet.Enabled = true;
            btnlokasyon_kaydet.CssClass = "btn btn-success ";

            lblmodalyenibaslik.Text = "Yeni Lokasyon Ekle";
        }

        protected void grid_lokasyon_liste_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName.Equals("sil"))
            {
                int index = Convert.ToInt32(e.CommandArgument);
                lblidsil.Text = grid_lokasyon_liste.DataKeys[index].Value.ToString();
                lblislem.Text = "lokasyon-sil";
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
                string id = grid_lokasyon_liste.DataKeys[index].Value.ToString();
                lblidcihaz.Text = id;

                txtlokasyon.Text = HttpUtility.HtmlDecode(grid_lokasyon_liste.Rows[index].Cells[1].Text);

                btnlokasyon_kaydet.Enabled = true;
                btnlokasyon_kaydet.CssClass = "btn btn-success ";

                lblmodalyenibaslik.Text = "Lokasyon Güncelle";

                System.Text.StringBuilder sb = new System.Text.StringBuilder();
                sb.Append(@"<script type='text/javascript'>");
                sb.Append("$(\"#Modal_Lokasyon_Kaydet\").modal(\"show\");");
                sb.Append(@"</script>");
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(),
                           "ModalScript", sb.ToString(), false);
            }
        }

        void listele_lokasyon()
        {
            SqlCommand cmd;
            string sql = " ", sql1 = " ";

            sql = "SELECT * FROM VARLIK_LOKASYON WHERE ID>0 ";

            sql = sql + sql1;
            cmd = new SqlCommand(sql, conn);
            conn.Open();
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            grid_lokasyon_liste.DataSource = dt;
            grid_lokasyon_liste.DataBind();
            conn.Close();

        }

        protected void btnlokasyon_kaydet_Click(object sender, EventArgs e)
        {
            try
            {
                SqlCommand sorgu = new SqlCommand();
                sorgu.Connection = conn;
                conn.Open();

                if (lblidcihaz.Text == "")
                {
                    sorgu.CommandText = "SELECT COUNT(*) FROM VARLIK_LOKASYON WHERE V_LOKASYON=@VL_VL ";
                    sorgu.Parameters.AddWithValue("@VL_VL", txtlokasyon.Text);
                    int sayi = Convert.ToInt16(sorgu.ExecuteScalar());

                    if (sayi == 0)
                    {
                        string userid = Session["KULLANICI_ID"].ToString();
                        //string userid = "1";
                        sorgu.CommandText = "INSERT INTO VARLIK_LOKASYON (V_LOKASYON, KAYIT_EDEN , KAYIT_TARIHI)  VALUES(@VL, @UI, getdate() ) ";
                        sorgu.Parameters.AddWithValue("@VL", txtlokasyon.Text);
                        sorgu.Parameters.AddWithValue("@UI", userid);
                        sorgu.ExecuteNonQuery();

                        ScriptManager.RegisterStartupScript(this, this.GetType(), System.Guid.NewGuid().ToString(), "YeniMesaj('Varlık Lokasyonu başarıyla kaydedilmiştir.','Tamam');", true);
                        ScriptManager.RegisterStartupScript(this, this.GetType(), System.Guid.NewGuid().ToString(), "ShowMessage('Varlık Lokasyonu başarıyla kaydedilmiştir.','Tamam','yeni');", true);

                        btnlokasyon_kaydet.Enabled = false;
                        btnlokasyon_kaydet.CssClass = "btn btn-success disabled";
                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(this, this.GetType(), System.Guid.NewGuid().ToString(), "YeniMesaj('Bu Varlık Lokasyonu daha önce kaydedilmişti. Lütfen kontrol ediniz.','Hata');", true);
                        ScriptManager.RegisterStartupScript(this, this.GetType(), System.Guid.NewGuid().ToString(), "ShowMessage('Bu Varlık Lokasyonu daha önce kaydedilmişti. Lütfen kontrol ediniz.','Hata','yeni');", true);
                    }
                }
                else
                {
                    string userid = Session["KULLANICI_ID"].ToString();
                    sorgu.CommandText = "UPDATE VARLIK_LOKASYON SET V_LOKASYON=@VL_U   WHERE ID=@VLID_U ";
                    sorgu.Parameters.AddWithValue("@VL_U", txtlokasyon.Text);
                    sorgu.Parameters.AddWithValue("@VLID_U", lblidcihaz.Text);
                    sorgu.ExecuteNonQuery();

                    ScriptManager.RegisterStartupScript(this, this.GetType(), System.Guid.NewGuid().ToString(), "YeniMesaj('Varlık Lokasyonu başarıyla güncellenmiştir.','Tamam');", true);
                    ScriptManager.RegisterStartupScript(this, this.GetType(), System.Guid.NewGuid().ToString(), "ShowMessage('Varlık Lokasyonu başarıyla güncellenmiştir.','Tamam','yeni');", true);

                    btnlokasyon_kaydet.Enabled = false;
                    btnlokasyon_kaydet.CssClass = "btn btn-success disabled";
                }
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), System.Guid.NewGuid().ToString(), "YeniMesaj('Beklenmedik bir hata oluştu.','Hata');", true);
                ScriptManager.RegisterStartupScript(this, this.GetType(), System.Guid.NewGuid().ToString(), "ShowMessage('Hata oluştu :'" + ex.Message.Replace("'", "") + ",'Hata','yeni');", true);
            }
            if (ConnectionState.Open == conn.State)
                conn.Close();
            listele_lokasyon();
        }

        //Varlık Tip

        protected void btnvarliktipi_ekle_Click(object sender, EventArgs e)
        {
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            sb.Append(@"<script type='text/javascript'>");
            sb.Append("$(\"#Modal_Tip_Kaydet\").modal(\"show\");");
            sb.Append(@"</script>");
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(),
                       "ModalScript", sb.ToString(), false);

            lblidcihaz1.Text = "";
            //comtipsec.SelectedIndex = 0;
            txttip.Text = "";
            btntip_kaydet.Enabled = true;
            btntip_kaydet.CssClass = "btn btn-success ";

            lblmodalyenibaslik1.Text = "Varlık Tipi Ekle";
            listele_lokasyon_sec();
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
                lblidcihaz1.Text = id;

                listele_lokasyon_sec();
                comlokasyon_sec.SelectedIndex = comlokasyon_sec.Items.IndexOf(comlokasyon_sec.Items.FindByText(HttpUtility.HtmlDecode(grid_tip_liste.Rows[index].Cells[1].Text)));
                txttip.Text = HttpUtility.HtmlDecode(grid_tip_liste.Rows[index].Cells[2].Text);

                btntip_kaydet.Enabled = true;
                btntip_kaydet.CssClass = "btn btn-success ";

                lblmodalyenibaslik1.Text = "Varlık Tipi Güncelle";

                System.Text.StringBuilder sb = new System.Text.StringBuilder();
                sb.Append(@"<script type='text/javascript'>");
                sb.Append("$(\"#Modal_Tip_Kaydet\").modal(\"show\");");
                sb.Append(@"</script>");
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(),
                           "ModalScript", sb.ToString(), false);
            }
        }

        protected void btntip_kaydet_Click(object sender, EventArgs e)
        {
            try
            {
                SqlCommand sorgu = new SqlCommand();
                sorgu.Connection = conn;
                conn.Open();

                if (lblidcihaz1.Text == "")
                {
                    sorgu.CommandText = "SELECT COUNT(*) FROM VARLIK_TIP WHERE V_TIP=@VT_VTIP ";
                    sorgu.Parameters.AddWithValue("@VT_VTIP", txttip.Text);
                    int sayi = Convert.ToInt16(sorgu.ExecuteScalar());

                    if (sayi == 0)
                    {
                        string userid = Session["KULLANICI_ID"].ToString();
                        sorgu.CommandText = "INSERT INTO VARLIK_TIP (V_LOKASYON_ID, V_TIP, KAYIT_EDEN , KAYIT_TARIHI)  VALUES(@VL_ID, @VT, @UI, getdate() ) ";
                        sorgu.Parameters.AddWithValue("@VL_ID", comlokasyon_sec.SelectedValue);
                        sorgu.Parameters.AddWithValue("@VT", txttip.Text);
                        sorgu.Parameters.AddWithValue("@UI", userid);
                        sorgu.ExecuteNonQuery();

                        ScriptManager.RegisterStartupScript(this, this.GetType(), System.Guid.NewGuid().ToString(), "ShowMessage('Varlık Tipi kaydedilmiştir.','Tamam','yeni1');", true);
                        ScriptManager.RegisterStartupScript(this, this.GetType(), System.Guid.NewGuid().ToString(), "YeniMesaj('Varlık Tipi kaydedilmiştir.','Tamam');", true);

                        btntip_kaydet.Enabled = false;
                        btntip_kaydet.CssClass = "btn btn-success disabled";
                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(this, this.GetType(), System.Guid.NewGuid().ToString(), "YeniMesaj('Bu Varlık Tipi daha önce kaydedilmişti. Lütfen kontrol ediniz.','Hata');", true);
                        ScriptManager.RegisterStartupScript(this, this.GetType(), System.Guid.NewGuid().ToString(), "ShowMessage('Bu Varlık Tipi daha önce kaydedilmişti. Lütfen kontrol ediniz.','Hata','yeni1');", true);
                    }
                }
                else
                {
                    string userid = Session["KULLANICI_ID"].ToString();
                    sorgu.CommandText = "UPDATE VARLIK_TIP SET V_LOKASYON_ID=@VL_ID_U, V_TIP=@VT_U   WHERE ID=@VTID_U1 ";
                    sorgu.Parameters.AddWithValue("@VL_ID_U", comlokasyon_sec.SelectedValue);
                    sorgu.Parameters.AddWithValue("@VT_U", txttip.Text);
                    sorgu.Parameters.AddWithValue("@VTID_U1", lblidcihaz1.Text);
                    sorgu.ExecuteNonQuery();

                    ScriptManager.RegisterStartupScript(this, this.GetType(), System.Guid.NewGuid().ToString(), "YeniMesaj('Varlık Tipi başarıyla güncellenmiştir.','Tamam');", true);
                    ScriptManager.RegisterStartupScript(this, this.GetType(), System.Guid.NewGuid().ToString(), "ShowMessage('Varlık Tipi başarıyla güncellenmiştir.','Tamam','yeni1');", true);
                    btntip_kaydet.Enabled = false;
                    btntip_kaydet.CssClass = "btn btn-success disabled";
                }
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), System.Guid.NewGuid().ToString(), "YeniMesaj('Beklenmedik bir hata oluştu.','Hata');", true);
                ScriptManager.RegisterStartupScript(this, this.GetType(), System.Guid.NewGuid().ToString(), "ShowMessage('Hata oluştu :'" + ex.Message.Replace("'", "") + ",'Hata','yeni1');", true);
            }

            if (ConnectionState.Open == conn.State)
                conn.Close();
            listele_varlik_tip();
        }

        void listele_varlik_tip()
        {
            SqlCommand cmd;
            string sql = " ", sql1 = " ";

            sql = "SELECT VT.ID, VL.V_LOKASYON, VT.V_TIP " +
                " FROM VARLIK_TIP AS VT " +
                " INNER JOIN VARLIK_LOKASYON AS VL ON VL.ID=VT.V_LOKASYON_ID " +
                " WHERE VT.ID>0 ";

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
        void listele_lokasyon_sec()
        {
            comlokasyon_sec.Items.Clear();
            comlokasyon_sec.Items.Insert(0, new ListItem("Seçiniz", "0"));
            comlokasyon_sec.AppendDataBoundItems = true;

            SqlDataAdapter da = new SqlDataAdapter();
            SqlCommand cmd = new SqlCommand("Select ID,V_LOKASYON FROM VARLIK_LOKASYON ORDER BY ID ");
            DataTable dt = new DataTable();
            da.SelectCommand = cmd;
            da.SelectCommand.Connection = conn;
            da.Fill(dt);
            comlokasyon_sec.DataSource = dt;
            comlokasyon_sec.DataTextField = "V_LOKASYON";
            comlokasyon_sec.DataValueField = "ID";
            comlokasyon_sec.DataBind();
        }

        //Varlık Adı

        protected void btnvarlikadi_ekle_Click(object sender, EventArgs e)
        {
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            sb.Append(@"<script type='text/javascript'>");
            sb.Append("$(\"#Modal_Ad_Kaydet\").modal(\"show\");");
            sb.Append(@"</script>");
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(),
                       "ModalScript", sb.ToString(), false);

            lblidcihaz2.Text = "";
            txtad.Text = "";
            btnad_kaydet.Enabled = true;
            btnad_kaydet.CssClass = "btn btn-success ";

            lblmodalyenibaslik2.Text = "Varlık Adı Ekle";
            listele_lokasyon_sec1();

            if (comtip_sec1.SelectedIndex > 0) ;
            comtip_sec1.Items.Clear();
            comtip_sec1.Enabled = false;
        }

        protected void grid_adi_liste_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName.Equals("sil"))
            {
                int index = Convert.ToInt32(e.CommandArgument);
                lblidsil.Text = grid_adi_liste.DataKeys[index].Value.ToString();
                lblislem.Text = "ad-sil";
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
                string id = grid_adi_liste.DataKeys[index].Value.ToString();
                lblidcihaz2.Text = id;

                listele_lokasyon_sec1();
                listele_tip_sec1();
                comlokasyon_sec1.SelectedIndex = comlokasyon_sec1.Items.IndexOf(comlokasyon_sec1.Items.FindByText(HttpUtility.HtmlDecode(grid_adi_liste.Rows[index].Cells[1].Text)));
                comtip_sec1.SelectedIndex = comtip_sec1.Items.IndexOf(comtip_sec1.Items.FindByText(HttpUtility.HtmlDecode(grid_adi_liste.Rows[index].Cells[2].Text)));
                txtad.Text = HttpUtility.HtmlDecode(grid_adi_liste.Rows[index].Cells[3].Text);

                btnad_kaydet.Enabled = true;
                btnad_kaydet.CssClass = "btn btn-success ";

                lblmodalyenibaslik2.Text = "Varlık Adı Güncelle";

                System.Text.StringBuilder sb = new System.Text.StringBuilder();
                sb.Append(@"<script type='text/javascript'>");
                sb.Append("$(\"#Modal_Ad_Kaydet\").modal(\"show\");");
                sb.Append(@"</script>");
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(),
                           "ModalScript", sb.ToString(), false);
            }
        }

        protected void btnad_kaydet_Click(object sender, EventArgs e)
        {
            try
            {
                SqlCommand sorgu = new SqlCommand();
                sorgu.Connection = conn;
                conn.Open();

                if (lblidcihaz1.Text == "")
                {
                    sorgu.CommandText = "SELECT COUNT(*) FROM VARLIK_ADI WHERE V_ADI=@VADI_VADI ";
                    sorgu.Parameters.AddWithValue("@VADI_VADI", txtad.Text);
                    int sayi = Convert.ToInt16(sorgu.ExecuteScalar());

                    if (sayi == 0)
                    {
                        string userid = Session["KULLANICI_ID"].ToString();
                        sorgu.CommandText = "INSERT INTO VARLIK_ADI (V_LOKASYON_ID, V_TIP_ID, V_ADI, KAYIT_EDEN , KAYIT_TARIHI)  VALUES(@VL_ID, @VT_ID, @V_A, @UI, getdate() ) ";
                        sorgu.Parameters.AddWithValue("@VL_ID", comlokasyon_sec1.SelectedValue);
                        sorgu.Parameters.AddWithValue("@VT_ID", comtip_sec1.SelectedValue);
                        sorgu.Parameters.AddWithValue("@V_A", txtad.Text);
                        sorgu.Parameters.AddWithValue("@UI", userid);
                        sorgu.ExecuteNonQuery();

                        ScriptManager.RegisterStartupScript(this, this.GetType(), System.Guid.NewGuid().ToString(), "ShowMessage('Varlık Adı kaydedilmiştir.','Tamam','yeni2');", true);
                        ScriptManager.RegisterStartupScript(this, this.GetType(), System.Guid.NewGuid().ToString(), "YeniMesaj('Varlık Adı kaydedilmiştir.','Tamam');", true);

                        btntip_kaydet.Enabled = false;
                        btntip_kaydet.CssClass = "btn btn-success disabled";
                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(this, this.GetType(), System.Guid.NewGuid().ToString(), "YeniMesaj('Bu Varlık Adı daha önce kaydedilmişti. Lütfen kontrol ediniz.','Hata');", true);
                        ScriptManager.RegisterStartupScript(this, this.GetType(), System.Guid.NewGuid().ToString(), "ShowMessage('Bu Varlık Adı daha önce kaydedilmişti. Lütfen kontrol ediniz.','Hata','yeni2');", true);
                    }
                }
                else
                {
                    string userid = Session["KULLANICI_ID"].ToString();
                    sorgu.CommandText = "UPDATE VARLIK_ADI SET V_LOKASYON_ID=@VL_ID_U, V_TIP_ID=@VT_ID_U, V_ADI=@V_A_U   WHERE ID=@V_A_ID_U ";
                    sorgu.Parameters.AddWithValue("@VL_ID_U", comlokasyon_sec1.SelectedValue);
                    sorgu.Parameters.AddWithValue("@VT_ID_U", comtip_sec1.SelectedValue);
                    sorgu.Parameters.AddWithValue("@V_A_U", txtad.Text);
                    sorgu.Parameters.AddWithValue("@V_A_ID_U", lblidcihaz2.Text);
                    sorgu.ExecuteNonQuery();

                    ScriptManager.RegisterStartupScript(this, this.GetType(), System.Guid.NewGuid().ToString(), "YeniMesaj('Varlık Adı başarıyla güncellenmiştir.','Tamam');", true);
                    ScriptManager.RegisterStartupScript(this, this.GetType(), System.Guid.NewGuid().ToString(), "ShowMessage('Varlık Adı başarıyla güncellenmiştir.','Tamam','yeni2');", true);
                    btnad_kaydet.Enabled = false;
                    btnad_kaydet.CssClass = "btn btn-success disabled";
                }
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), System.Guid.NewGuid().ToString(), "YeniMesaj('Beklenmedik bir hata oluştu.','Hata');", true);
                ScriptManager.RegisterStartupScript(this, this.GetType(), System.Guid.NewGuid().ToString(), "ShowMessage('Hata oluştu :'" + ex.Message.Replace("'", "") + ",'Hata','yeni1');", true);
            }

            if (ConnectionState.Open == conn.State)
                conn.Close();
            listele_varlik_ad();
        }

        void listele_varlik_ad()
        {
            SqlCommand cmd;
            string sql = " ", sql1 = " ";

            sql = "SELECT VA.ID, VL.V_LOKASYON, VT.V_TIP, VA.V_ADI" +
                " FROM VARLIK_ADI AS VA " +
                " INNER JOIN VARLIK_LOKASYON AS VL ON VL.ID=VA.V_LOKASYON_ID " +
                " INNER JOIN VARLIK_TIP AS VT ON VT.ID=VA.V_TIP_ID " +
                " WHERE VA.ID>0 ";

            sql = sql + sql1;
            cmd = new SqlCommand(sql, conn);
            conn.Open();
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            grid_adi_liste.DataSource = dt;
            grid_adi_liste.DataBind();
            conn.Close();
        }

        void listele_lokasyon_sec1()
        {
            comlokasyon_sec1.Items.Clear();
            comlokasyon_sec1.Items.Insert(0, new ListItem("Seçiniz", "0"));
            comlokasyon_sec1.AppendDataBoundItems = true;

            SqlDataAdapter da = new SqlDataAdapter();
            SqlCommand cmd = new SqlCommand("Select ID,V_LOKASYON FROM VARLIK_LOKASYON ORDER BY ID ");
            DataTable dt = new DataTable();
            da.SelectCommand = cmd;
            da.SelectCommand.Connection = conn;
            da.Fill(dt);
            comlokasyon_sec1.DataSource = dt;
            comlokasyon_sec1.DataTextField = "V_LOKASYON";
            comlokasyon_sec1.DataValueField = "ID";
            comlokasyon_sec1.DataBind();
        }

        void listele_tip_sec1()
        {
            comtip_sec1.Items.Clear();
            comtip_sec1.Items.Insert(0, new ListItem("Seçiniz", "0"));
            comtip_sec1.AppendDataBoundItems = true;
            string sql2 = "";

            if (comlokasyon_sec1.SelectedIndex > 0)
                sql2 = " AND V_LOKASYON_ID='" + comlokasyon_sec1.SelectedValue.ToString() + "' ";

            string sql = "SELECT  ID,V_TIP FROM VARLIK_TIP WHERE ID>0 " + sql2 + " ORDER BY ID";
            comtip_sec1.DataSource = tkod.GetData(sql);
            comtip_sec1.DataBind();
        }

        protected void comlokasyon_sec1_SelectedIndexChanged(object sender, EventArgs e)
        {
            comtip_sec1.Enabled = true;
            listele_tip_sec1();
        }

    }
}