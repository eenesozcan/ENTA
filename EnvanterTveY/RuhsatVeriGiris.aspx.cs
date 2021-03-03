using System;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using System.IO;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Web.Security;

namespace EnvanterTveY
{
    public partial class RuhsatVeriGiris : System.Web.UI.Page
    {
        SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["KabloHE"].ConnectionString);
        string sayfa = "RuhsatVeriGiris";
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

            }
        }

        void il_sec()
        {

            if (lbl_il_sec.Text == "RVIK")
            {
                com_il_sec_ruhsat_veren.Items.Clear();
                com_il_sec_ruhsat_veren.Items.Insert(0, new ListItem("İl Seçiniz", "0"));
                com_il_sec_ruhsat_veren.AppendDataBoundItems = true;

                com_il_sec_ruhsat_veren.DataSource = tkod.dt_il_Liste();
                com_il_sec_ruhsat_veren.DataBind();
            }
            if (lbl_il_sec.Text == "RDKK")
            {
                com_il_sec_Donem.Items.Clear();
                com_il_sec_Donem.Items.Insert(0, new ListItem("İl Seçiniz", "0"));
                com_il_sec_Donem.AppendDataBoundItems = true;

                com_il_sec_Donem.DataSource = tkod.dt_il_Liste();
                com_il_sec_Donem.DataBind();
            }
            if (lbl_il_sec.Text == "KTURUK")
            {
                com_il_sec_Kazi_Turu.Items.Clear();
                com_il_sec_Kazi_Turu.Items.Insert(0, new ListItem("İl Seçiniz", "0"));
                com_il_sec_Kazi_Turu.AppendDataBoundItems = true;

                com_il_sec_Kazi_Turu.DataSource = tkod.dt_il_Liste();
                com_il_sec_Kazi_Turu.DataBind();
            }
            if (lbl_il_sec.Text == "KTIPIK")
            {
                com_il_sec_Kazi_Tipi.Items.Clear();
                com_il_sec_Kazi_Tipi.Items.Insert(0, new ListItem("İl Seçiniz", "0"));
                com_il_sec_Kazi_Tipi.AppendDataBoundItems = true;

                com_il_sec_Kazi_Tipi.DataSource = tkod.dt_il_Liste();
                com_il_sec_Kazi_Tipi.DataBind();
            }
            if (lbl_il_sec.Text == "RT")
            {
                com_il_sec_Ruhsat_Tarife.Items.Clear();
                com_il_sec_Ruhsat_Tarife.Items.Insert(0, new ListItem("İl Seçiniz", "0"));
                com_il_sec_Ruhsat_Tarife.AppendDataBoundItems = true;

                com_il_sec_Ruhsat_Tarife.DataSource = tkod.dt_il_Liste();
                com_il_sec_Ruhsat_Tarife.DataBind();
            }
        }
        void ruhsat_veren_idare_sec()
        {
            if (lbl_il_sec.Text == "RDKK")
            {
                com_ruhsat_veren_sec_donem.Items.Clear();
                com_ruhsat_veren_sec_donem.Items.Insert(0, new ListItem("Ruhsat Veren İdareyi Seçiniz", "0"));
                com_ruhsat_veren_sec_donem.AppendDataBoundItems = true;
                string sql2 = "";

                if (com_il_sec_Donem.SelectedIndex > 0)
                    sql2 = " AND IL_ID='" + com_il_sec_Donem.SelectedValue.ToString() + "' ";

                string sql = "Select ID,RUHSAT_VEREN FROM RUHSAT_RUHSAT_VEREN WHERE ID>0 " + sql2 + " ORDER BY ID";
                com_ruhsat_veren_sec_donem.DataSource = tkod.GetData(sql);
                com_ruhsat_veren_sec_donem.DataBind();
            }
            if (lbl_il_sec.Text == "KTURUK")
            {
                com_ruhsat_veren_sec_Kazi_Turu.Items.Clear();
                com_ruhsat_veren_sec_Kazi_Turu.Items.Insert(0, new ListItem("Ruhsat Veren İdareyi Seçiniz", "0"));
                com_ruhsat_veren_sec_Kazi_Turu.AppendDataBoundItems = true;
                string sql2 = "";

                if (com_il_sec_Kazi_Turu.SelectedIndex > 0)
                    sql2 = " AND IL_ID='" + com_il_sec_Kazi_Turu.SelectedValue.ToString() + "' ";

                string sql = "Select ID,RUHSAT_VEREN FROM RUHSAT_RUHSAT_VEREN WHERE ID>0 " + sql2 + " ORDER BY ID";
                com_ruhsat_veren_sec_Kazi_Turu.DataSource = tkod.GetData(sql);
                com_ruhsat_veren_sec_Kazi_Turu.DataBind();
            }
            if (lbl_il_sec.Text == "KTIPIK")
            {
                com_ruhsat_veren_sec_Kazi_Tipi.Items.Clear();
                com_ruhsat_veren_sec_Kazi_Tipi.Items.Insert(0, new ListItem("Ruhsat Veren İdareyi Seçiniz", "0"));
                com_ruhsat_veren_sec_Kazi_Tipi.AppendDataBoundItems = true;
                string sql2 = "";

                if (com_il_sec_Kazi_Tipi.SelectedIndex > 0)
                    sql2 = " AND IL_ID='" + com_il_sec_Kazi_Tipi.SelectedValue.ToString() + "' ";

                string sql = "Select ID,RUHSAT_VEREN FROM RUHSAT_RUHSAT_VEREN WHERE ID>0 " + sql2 + " ORDER BY ID";
                com_ruhsat_veren_sec_Kazi_Tipi.DataSource = tkod.GetData(sql);
                com_ruhsat_veren_sec_Kazi_Tipi.DataBind();
            }
            if (lbl_il_sec.Text == "RT")
            {
                com_ruhsat_veren_sec_Ruhsat_Tarife.Items.Clear();
                com_ruhsat_veren_sec_Ruhsat_Tarife.Items.Insert(0, new ListItem("Ruhsat Veren İdareyi Seçiniz", "0"));
                com_ruhsat_veren_sec_Ruhsat_Tarife.AppendDataBoundItems = true;
                string sql2 = "";

                if (com_il_sec_Ruhsat_Tarife.SelectedIndex > 0)
                    sql2 = " AND IL_ID='" + com_il_sec_Ruhsat_Tarife.SelectedValue.ToString() + "' ";

                string sql = "Select ID,RUHSAT_VEREN FROM RUHSAT_RUHSAT_VEREN WHERE ID>0 " + sql2 + " ORDER BY ID";
                com_ruhsat_veren_sec_Ruhsat_Tarife.DataSource = tkod.GetData(sql);
                com_ruhsat_veren_sec_Ruhsat_Tarife.DataBind();
            }
        }
        void ruhsat_donem_sec()
        {
            if (lbl_il_sec.Text == "KTURUK")
            {
                com_donem_sec_Kazi_Turu.Items.Clear();
                com_donem_sec_Kazi_Turu.Items.Insert(0, new ListItem("Dönem Seçiniz", "0"));
                com_donem_sec_Kazi_Turu.AppendDataBoundItems = true;
                string sql2 = "";

                if (com_ruhsat_veren_sec_Kazi_Turu.SelectedIndex > 0)
                    sql2 = " AND RUHSAT_VEREN_ID='" + com_ruhsat_veren_sec_Kazi_Turu.SelectedValue.ToString() + "' ";

                string sql = "Select ID,DONEM FROM RUHSAT_DONEM WHERE ID>0 " + sql2 + " ORDER BY ID";
                com_donem_sec_Kazi_Turu.DataSource = tkod.GetData(sql);
                com_donem_sec_Kazi_Turu.DataBind();
            }
            if (lbl_il_sec.Text == "KTIPIK")
            {
                com_donem_sec_Kazi_Tipi.Items.Clear();
                com_donem_sec_Kazi_Tipi.Items.Insert(0, new ListItem("Dönem Seçiniz", "0"));
                com_donem_sec_Kazi_Tipi.AppendDataBoundItems = true;
                string sql2 = "";

                if (com_ruhsat_veren_sec_Kazi_Tipi.SelectedIndex > 0)
                    sql2 = " AND RUHSAT_VEREN_ID='" + com_ruhsat_veren_sec_Kazi_Tipi.SelectedValue.ToString() + "' ";

                string sql = "Select ID,DONEM FROM RUHSAT_DONEM WHERE ID>0 " + sql2 + " ORDER BY ID";
                com_donem_sec_Kazi_Tipi.DataSource = tkod.GetData(sql);
                com_donem_sec_Kazi_Tipi.DataBind();
            }
            if (lbl_il_sec.Text == "RT")
            {
                com_donem_sec_Ruhsat_Tarife.Items.Clear();
                com_donem_sec_Ruhsat_Tarife.Items.Insert(0, new ListItem("Dönem Seçiniz", "0"));
                com_donem_sec_Ruhsat_Tarife.AppendDataBoundItems = true;
                string sql2 = "";

                if (com_ruhsat_veren_sec_Ruhsat_Tarife.SelectedIndex > 0)
                    sql2 = " AND RUHSAT_VEREN_ID='" + com_ruhsat_veren_sec_Ruhsat_Tarife.SelectedValue.ToString() + "' ";

                string sql = "Select ID,DONEM FROM RUHSAT_DONEM WHERE ID>0 " + sql2 + " ORDER BY ID";
                com_donem_sec_Ruhsat_Tarife.DataSource = tkod.GetData(sql);
                com_donem_sec_Ruhsat_Tarife.DataBind();
            }
        }
        void ruhsat_kazi_turu_sec()
        {
            if (lbl_il_sec.Text == "KTIPIK")
            {
                com_kazi_turu_Kazi_Tipi.Items.Clear();
                com_kazi_turu_Kazi_Tipi.Items.Insert(0, new ListItem("Kazı Tipi Seçiniz", "0"));
                com_kazi_turu_Kazi_Tipi.AppendDataBoundItems = true;
                string sql2 = "";

                if (com_donem_sec_Kazi_Tipi.SelectedIndex > 0)
                    sql2 = " AND DONEM_ID='" + com_donem_sec_Kazi_Tipi.SelectedValue.ToString() + "' ";

                string sql = "Select ID,KAZI_TURU FROM RUHSAT_KAZI_TURU WHERE ID>0 " + sql2 + " ORDER BY ID";
                com_kazi_turu_Kazi_Tipi.DataSource = tkod.GetData(sql);
                com_kazi_turu_Kazi_Tipi.DataBind();
            }
            if (lbl_il_sec.Text == "RT")
            {
                com_kazi_turu_Ruhsat_Tarife.Items.Clear();
                com_kazi_turu_Ruhsat_Tarife.Items.Insert(0, new ListItem("Kazı Tipi Seçiniz", "0"));
                com_kazi_turu_Ruhsat_Tarife.AppendDataBoundItems = true;
                string sql2 = "";

                if (com_donem_sec_Ruhsat_Tarife.SelectedIndex > 0)
                    sql2 = " AND DONEM_ID='" + com_donem_sec_Ruhsat_Tarife.SelectedValue.ToString() + "' ";

                string sql = "Select ID,KAZI_TURU FROM RUHSAT_KAZI_TURU WHERE ID>0 " + sql2 + " ORDER BY ID";
                com_kazi_turu_Ruhsat_Tarife.DataSource = tkod.GetData(sql);
                com_kazi_turu_Ruhsat_Tarife.DataBind();
            }
        }
        void ruhsat_kazi_birim_sec()
        {
            if (lbl_il_sec.Text == "KTIPIK")
            {
                com_birim_sec_Kazi_Tipi.Items.Clear();
                com_birim_sec_Kazi_Tipi.Items.Insert(0, new ListItem("Kazı Birimi Seçiniz", "0"));
                com_birim_sec_Kazi_Tipi.AppendDataBoundItems = true;

                string sql = "Select ID,BIRIM FROM RUHSAT_KAZI_BIRIMI WHERE ID>0  ORDER BY ID";
                com_birim_sec_Kazi_Tipi.DataSource = tkod.GetData(sql);
                com_birim_sec_Kazi_Tipi.DataBind();
            }
        }

        //------PANEL 1 - AYKOME PROJE TİPİ------\\

        protected void btnkaydet_Aykome_Proje_Tipi_Click(object sender, EventArgs e)
        {
            try
            {
                SqlCommand sorgu = new SqlCommand();
                sorgu.Connection = conn;
                conn.Open();

                if (lblidcihaz.Text == "")
                {
                    sorgu.CommandText = "SELECT COUNT(*) FROM RUHSAT_AYKOME_PROJE_TIPI WHERE A_PROJE_TIPI=@SAY1 ";
                    sorgu.Parameters.AddWithValue("@SAY1", txt_aproje_tipi.Text);
                    int sayi = Convert.ToInt16(sorgu.ExecuteScalar());

                    if (sayi == 0)
                    {
                        string userid = Session["KULLANICI_ID"].ToString();
                        sorgu.CommandText = "INSERT INTO RUHSAT_AYKOME_PROJE_TIPI (A_PROJE_TIPI, KAYIT_EDEN , KAYIT_TARIHI)  " +
                                            " VALUES(@A_PROJE_TIPI_K, @UI_K, getdate() ) ";
                        sorgu.Parameters.AddWithValue("@A_PROJE_TIPI_K", txt_aproje_tipi.Text);
                        sorgu.Parameters.AddWithValue("@UI_K", userid);
                        sorgu.ExecuteNonQuery();

                        ScriptManager.RegisterStartupScript(this, this.GetType(), System.Guid.NewGuid().ToString(), "ShowMessage('Kayıt gerçekleşmiştir.','Tamam','yeni');", true);
                        ScriptManager.RegisterStartupScript(this, this.GetType(), System.Guid.NewGuid().ToString(), "YeniMesaj('Kayıt gerçekleşmiştir.','Tamam');", true);

                        //btnkaydet_Aykome_Proje_Tipi.Enabled = false;
                        //btnkaydet_Aykome_Proje_Tipi.CssClass = "btn btn-success disabled";
                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(this, this.GetType(), System.Guid.NewGuid().ToString(), "YeniMesaj('Mükerrer kayıt. Lütfen kontrol ediniz.','Hata');", true);
                        ScriptManager.RegisterStartupScript(this, this.GetType(), System.Guid.NewGuid().ToString(), "ShowMessage('Mükerrer kayıt. Lütfen kontrol ediniz.','Hata','yeni');", true);
                    }
                }
                else
                {
                    string userid = Session["KULLANICI_ID"].ToString();
                    sorgu.CommandText = "UPDATE RUHSAT_AYKOME_PROJE_TIPI SET  A_PROJE_TIPI=@A_PROJE_TIPI_G   WHERE ID=@APROJETIP_G ";
                    sorgu.Parameters.AddWithValue("@A_PROJE_TIPI_G", txt_aproje_tipi.Text);
                    sorgu.Parameters.AddWithValue("@APROJETIP_G", lblidcihaz.Text);
                    sorgu.ExecuteNonQuery();

                    ScriptManager.RegisterStartupScript(this, this.GetType(), System.Guid.NewGuid().ToString(), "YeniMesaj('Kayıt güncellenmiştir.','Tamam');", true);
                    ScriptManager.RegisterStartupScript(this, this.GetType(), System.Guid.NewGuid().ToString(), "ShowMessage('Kayıt güncellenmiştir.','Tamam','yeni');", true);
                    //btnkaydet_Aykome_Proje_Tipi.Enabled = false;
                    //btnkaydet_Aykome_Proje_Tipi.CssClass = "btn btn-success disabled";
                }
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), System.Guid.NewGuid().ToString(), "YeniMesaj('Beklenmedik bir hata oluştu.','Hata');", true);
                ScriptManager.RegisterStartupScript(this, this.GetType(), System.Guid.NewGuid().ToString(), "ShowMessage('Hata oluştu :'" + ex.Message.Replace("'", "") + ",'Hata','yeni1');", true);
            }

            if (ConnectionState.Open == conn.State)
                conn.Close();
            listele_Aykome_Proje_Tipi();
        }


        protected void grid_Aykome_Proje_Tipi_RowCommand(object sender, GridViewCommandEventArgs e)
        {

        }

        protected void grid_Aykome_Proje_Tipi_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {

        }

        protected void btnara_Aykome_Proje_Tipi_Click(object sender, EventArgs e)
        {
            listele_Aykome_Proje_Tipi();
        }

        void listele_Aykome_Proje_Tipi()
        {
            SqlCommand cmd;
            string sql = "", sql1 = "";

            sql = "SELECT * FROM RUHSAT_AYKOME_PROJE_TIPI WHERE ID>0  ";

            if (txt_Aykome_Proje_Tipi_ara.Text.Length > 0)
                sql1 += " AND A_PROJE_TIPI LIKE '%" + txt_Aykome_Proje_Tipi_ara.Text + "%'  ";
            sql = sql + sql1;
            cmd = new SqlCommand(sql, conn);
            conn.Open();
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            grid_Aykome_Proje_Tipi.DataSource = dt;
            grid_Aykome_Proje_Tipi.DataBind();
            conn.Close();
        }

        protected void btn_Aykome_Proje_Tipi_ekle_Click(object sender, EventArgs e)
        {
            lblmodalyenibaslik.Text = "Aykome Proje Tipi - Kayıt";
            lbl_il_sec.Text = "APTK";

            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            sb.Append(@"<script type='text/javascript'>");
            sb.Append("$(\"#ModalRuhsatVeri\").modal(\"show\");");
            sb.Append(@"</script>");
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(),
                       "ModalScript", sb.ToString(), false);

            Panel_Ruhsat_Veren.Visible = false;
            Panel_Donem.Visible = false;
            Panel_Kazi_Turu.Visible = false;
            Panel_Kazi_Tipi.Visible = false;
            Panel_Turksat_Proje_Tipi.Visible = false;
            Panel_Aykome_Proje_Tipi.Visible = true;
            Panel_Kazi_Birimi.Visible = false;
            Panel_Ruhsat_Tarife.Visible = false;
        }

        //------PANEL 2 - TÜRKSAT PROJE TİPİ------\\
        protected void grid_Turksat_Proje_Tipi_RowCommand(object sender, GridViewCommandEventArgs e)
        {

        }

        protected void grid_Turksat_Proje_Tipi_SelectedIndexChanging(object sender, GridViewSelectEventArgs e)
        {

        }

        protected void btnara_Turksat_Proje_Tipi_Click(object sender, EventArgs e)
        {
            listele_Turksat_Proje_Tipi();
        }

        void listele_Turksat_Proje_Tipi()
        {
            SqlCommand cmd;
            string sql = "", sql1 = "";

            sql = "SELECT * FROM RUHSAT_TURKSAT_PROJE_TIPI WHERE ID>0  ";

            if (txt_Turksat_Proje_Tipi_ara.Text.Length > 0)
                sql1 += " AND T_PROJE_TIPI LIKE '%" + txt_Turksat_Proje_Tipi_ara.Text + "%'  ";
            sql = sql + sql1;
            cmd = new SqlCommand(sql, conn);
            conn.Open();
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            grid_Turksat_Proje_Tipi.DataSource = dt;
            grid_Turksat_Proje_Tipi.DataBind();
            conn.Close();
        }

        protected void btn_Turksat_Proje_Tipi_ekle_Click(object sender, EventArgs e)
        {
            lblmodalyenibaslik.Text = "Türksat Proje Tipi - Kayıt";
            lbl_il_sec.Text = "TPTK";

            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            sb.Append(@"<script type='text/javascript'>");
            sb.Append("$(\"#ModalRuhsatVeri\").modal(\"show\");");
            sb.Append(@"</script>");
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(),
                       "ModalScript", sb.ToString(), false);

            Panel_Ruhsat_Veren.Visible = false;
            Panel_Donem.Visible = false;
            Panel_Kazi_Turu.Visible = false;
            Panel_Kazi_Tipi.Visible = false;
            Panel_Turksat_Proje_Tipi.Visible = true;
            Panel_Aykome_Proje_Tipi.Visible = false;
            Panel_Kazi_Birimi.Visible = false;
            Panel_Ruhsat_Tarife.Visible = false;
        }

        protected void btnkaydet_Turksat_Proje_Tipi_Click(object sender, EventArgs e)
        {
            try
            {
                SqlCommand sorgu = new SqlCommand();
                sorgu.Connection = conn;
                conn.Open();

                if (lblidcihaz.Text == "")
                {
                    sorgu.CommandText = "SELECT COUNT(*) FROM RUHSAT_TURKSAT_PROJE_TIPI WHERE T_PROJE_TIPI=@SAY1 ";
                    sorgu.Parameters.AddWithValue("@SAY1", txt_tproje_tipi.Text);
                    int sayi = Convert.ToInt16(sorgu.ExecuteScalar());

                    if (sayi == 0)
                    {
                        string userid = Session["KULLANICI_ID"].ToString();
                        sorgu.CommandText = "INSERT INTO RUHSAT_TURKSAT_PROJE_TIPI (T_PROJE_TIPI, KAYIT_EDEN , KAYIT_TARIHI)  " +
                                            " VALUES(@R_PROJE_TIPI_K, @UI_K, getdate() ) ";
                        sorgu.Parameters.AddWithValue("@R_PROJE_TIPI_K", txt_tproje_tipi.Text);
                        sorgu.Parameters.AddWithValue("@UI_K", userid);
                        sorgu.ExecuteNonQuery();

                        ScriptManager.RegisterStartupScript(this, this.GetType(), System.Guid.NewGuid().ToString(), "ShowMessage('Kayıt gerçekleşmiştir.','Tamam','yeni');", true);
                        ScriptManager.RegisterStartupScript(this, this.GetType(), System.Guid.NewGuid().ToString(), "YeniMesaj('Kayıt gerçekleşmiştir.','Tamam');", true);

                        //btnkaydet_Turksat_Proje_Tipi.Enabled = false;
                        //btnkaydet_Turksat_Proje_Tipi.CssClass = "btn btn-success disabled";
                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(this, this.GetType(), System.Guid.NewGuid().ToString(), "YeniMesaj('Mükerrer kayıt. Lütfen kontrol ediniz.','Hata');", true);
                        ScriptManager.RegisterStartupScript(this, this.GetType(), System.Guid.NewGuid().ToString(), "ShowMessage('Mükerrer kayıt. Lütfen kontrol ediniz.','Hata','yeni');", true);
                    }
                }
                else
                {
                    string userid = Session["KULLANICI_ID"].ToString();
                    sorgu.CommandText = "UPDATE RUHSAT_TURKSAT_PROJE_TIPI SET  T_PROJE_TIPI=@T_PROJE_TIPI_G   WHERE ID=@TPROJETIP_G ";
                    sorgu.Parameters.AddWithValue("@T_PROJE_TIPI_G", txt_tproje_tipi.Text);
                    sorgu.Parameters.AddWithValue("@TPROJETIP_G", lblidcihaz.Text);
                    sorgu.ExecuteNonQuery();

                    ScriptManager.RegisterStartupScript(this, this.GetType(), System.Guid.NewGuid().ToString(), "YeniMesaj('Kayıt güncellenmiştir.','Tamam');", true);
                    ScriptManager.RegisterStartupScript(this, this.GetType(), System.Guid.NewGuid().ToString(), "ShowMessage('Kayıt güncellenmiştir.','Tamam','yeni');", true);
                    //btnkaydet_Turksat_Proje_Tipi.Enabled = false;
                    //btnkaydet_Turksat_Proje_Tipi.CssClass = "btn btn-success disabled";
                }
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), System.Guid.NewGuid().ToString(), "YeniMesaj('Beklenmedik bir hata oluştu.','Hata');", true);
                ScriptManager.RegisterStartupScript(this, this.GetType(), System.Guid.NewGuid().ToString(), "ShowMessage('Hata oluştu :'" + ex.Message.Replace("'", "") + ",'Hata','yeni1');", true);
            }

            if (ConnectionState.Open == conn.State)
                conn.Close();
            listele_Turksat_Proje_Tipi();
        }

        //------PANEL 3 - KAZI BİRİMİ------\\
        void listele_Kazi_Birim()
        {
            SqlCommand cmd;
            string sql = "", sql1 = "";

            sql = "SELECT * FROM RUHSAT_KAZI_BIRIMI WHERE ID>0  ";

            if (txt_Kazi_Birim_ara.Text.Length > 0)
                sql1 += " AND KAZI_BIRIM LIKE '%" + txt_Kazi_Birim_ara.Text + "%'  ";
            sql = sql + sql1;
            cmd = new SqlCommand(sql, conn);
            conn.Open();
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            grid_Kazi_Birim.DataSource = dt;
            grid_Kazi_Birim.DataBind();
            conn.Close();
        }

        protected void btnara_Kazi_Birim_Click(object sender, EventArgs e)
        {
            listele_Kazi_Birim();
        }

        protected void btn_Kazi_Birim_ekle_Click(object sender, EventArgs e)
        {
            lblmodalyenibaslik.Text = "Kazı Birimi - Kayıt";
            lbl_il_sec.Text = "KBK";

            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            sb.Append(@"<script type='text/javascript'>");
            sb.Append("$(\"#ModalRuhsatVeri\").modal(\"show\");");
            sb.Append(@"</script>");
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(),
                       "ModalScript", sb.ToString(), false);

            Panel_Ruhsat_Veren.Visible = false;
            Panel_Donem.Visible = false;
            Panel_Kazi_Turu.Visible = false;
            Panel_Kazi_Tipi.Visible = false;
            Panel_Turksat_Proje_Tipi.Visible = false;
            Panel_Aykome_Proje_Tipi.Visible = false;
            Panel_Kazi_Birimi.Visible = true;
            Panel_Ruhsat_Tarife.Visible = false;
        }

        protected void grid_Kazi_Birim_RowCommand(object sender, GridViewCommandEventArgs e)
        {

        }

        protected void grid_Kazi_Birim_SelectedIndexChanging(object sender, GridViewSelectEventArgs e)
        {

        }

        protected void btnkaydet_Ruhsat_Veren_Kazi_Birimi_Click(object sender, EventArgs e)
        {
            try
            {
                SqlCommand sorgu = new SqlCommand();
                sorgu.Connection = conn;
                conn.Open();

                if (lblidcihaz.Text == "")
                {
                    sorgu.CommandText = "SELECT COUNT(*) FROM RUHSAT_KAZI_BIRIMI WHERE BIRIM=@SAY1 ";
                    sorgu.Parameters.AddWithValue("@SAY1", txt_birim.Text);
                    int sayi = Convert.ToInt16(sorgu.ExecuteScalar());

                    if (sayi == 0)
                    {
                        string userid = Session["KULLANICI_ID"].ToString();
                        sorgu.CommandText = "INSERT INTO RUHSAT_KAZI_BIRIMI (BIRIM, KAYIT_EDEN, KAYIT_TARIHI)  " +
                                            " VALUES(@BIRIM_K, @UI_K, getdate() ) ";
                        sorgu.Parameters.AddWithValue("@BIRIM_K", txt_birim.Text);
                        sorgu.Parameters.AddWithValue("@UI_K", userid);
                        sorgu.ExecuteNonQuery();

                        ScriptManager.RegisterStartupScript(this, this.GetType(), System.Guid.NewGuid().ToString(), "ShowMessage('Kayıt gerçekleşmiştir.','Tamam','yeni');", true);
                        ScriptManager.RegisterStartupScript(this, this.GetType(), System.Guid.NewGuid().ToString(), "YeniMesaj('Kayıt gerçekleşmiştir.','Tamam');", true);

                        //btnkaydet_Ruhsat_Veren_Kazi_Birimi.Enabled = false;
                        //btnkaydet_Ruhsat_Veren_Kazi_Birimi.CssClass = "btn btn-success disabled";
                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(this, this.GetType(), System.Guid.NewGuid().ToString(), "YeniMesaj('Mükerrer kayıt. Lütfen kontrol ediniz.','Hata');", true);
                        ScriptManager.RegisterStartupScript(this, this.GetType(), System.Guid.NewGuid().ToString(), "ShowMessage('Mükerrer kayıt. Lütfen kontrol ediniz.','Hata','yeni');", true);
                    }
                }
                else
                {
                    string userid = Session["KULLANICI_ID"].ToString();
                    sorgu.CommandText = "UPDATE RUHSAT_KAZI_BIRIMI SET  BIRIM=@BIRIM_G   WHERE ID=@ID_G ";
                    sorgu.Parameters.AddWithValue("@BIRIM_G", txt_birim.Text);
                    sorgu.Parameters.AddWithValue("@ID_G", lblidcihaz.Text);
                    sorgu.ExecuteNonQuery();

                    ScriptManager.RegisterStartupScript(this, this.GetType(), System.Guid.NewGuid().ToString(), "YeniMesaj('Kayıt güncellenmiştir.','Tamam');", true);
                    ScriptManager.RegisterStartupScript(this, this.GetType(), System.Guid.NewGuid().ToString(), "ShowMessage('Kayıt güncellenmiştir.','Tamam','yeni');", true);
                    //btnkaydet_Ruhsat_Veren_Kazi_Birimi.Enabled = false;
                    //btnkaydet_Ruhsat_Veren_Kazi_Birimi.CssClass = "btn btn-success disabled";
                }
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), System.Guid.NewGuid().ToString(), "YeniMesaj('Beklenmedik bir hata oluştu.','Hata');", true);
                ScriptManager.RegisterStartupScript(this, this.GetType(), System.Guid.NewGuid().ToString(), "ShowMessage('Hata oluştu :'" + ex.Message.Replace("'", "") + ",'Hata','yeni1');", true);
            }

            if (ConnectionState.Open == conn.State)
                conn.Close();
            listele_Kazi_Birim();
        }

        //------PANEL 4 - RUHSAT VEREN------\\

        void listele_Ruhsat_Veren_Idare()
        {
            SqlCommand cmd;
            string sql = "", sql1 = "";

            sql = "SELECT RRV.ID, IL.IL, RRV.RUHSAT_VEREN  FROM RUHSAT_RUHSAT_VEREN RRV " +
                  " INNER JOIN IL ON IL.ID=RRV.IL_ID" +
                  " WHERE RRV.ID>0  ";

            if (txt_Ruhsat_Veren_Idare_ara.Text.Length > 0)
                sql1 += " AND RRV.RUHSAT_VEREN LIKE '%" + txt_Ruhsat_Veren_Idare_ara.Text + "%'  ";
            sql = sql + sql1;
            cmd = new SqlCommand(sql, conn);
            conn.Open();
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            grid_Ruhsat_Veren_Idare.DataSource = dt;
            grid_Ruhsat_Veren_Idare.DataBind();
            conn.Close();
        }

        protected void btnara_Ruhsat_Veren_Idare_Click(object sender, EventArgs e)
        {
            listele_Ruhsat_Veren_Idare();
        }

        protected void btn_Ruhsat_Veren_Idare_ekle_Click(object sender, EventArgs e)
        {
            lblmodalyenibaslik.Text = "Ruhsat Veren İdare - Kayıt";
            lbl_il_sec.Text = "RVIK";
            il_sec();

            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            sb.Append(@"<script type='text/javascript'>");
            sb.Append("$(\"#ModalRuhsatVeri\").modal(\"show\");");
            sb.Append(@"</script>");
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(),
                       "ModalScript", sb.ToString(), false);

            Panel_Ruhsat_Veren.Visible = true;
            Panel_Donem.Visible = false;
            Panel_Kazi_Turu.Visible = false;
            Panel_Kazi_Tipi.Visible = false;
            Panel_Turksat_Proje_Tipi.Visible = false;
            Panel_Aykome_Proje_Tipi.Visible = false;
            Panel_Kazi_Birimi.Visible = false;
            Panel_Ruhsat_Tarife.Visible = false;
        }

        protected void grid_Ruhsat_Veren_Idare_RowCommand(object sender, GridViewCommandEventArgs e)
        {

        }

        protected void grid_Ruhsat_Veren_Idare_SelectedIndexChanging(object sender, GridViewSelectEventArgs e)
        {

        }

        protected void btnkaydet_Ruhsat_Veren_Click(object sender, EventArgs e)
        {
            try
            {
                SqlCommand sorgu = new SqlCommand();
                sorgu.Connection = conn;
                conn.Open();

                if (lblidcihaz.Text == "")
                {
                    sorgu.CommandText = "SELECT COUNT(*) FROM RUHSAT_RUHSAT_VEREN WHERE RUHSAT_VEREN=@SAY1 ";
                    sorgu.Parameters.AddWithValue("@SAY1", txt_ruhsat_veren.Text);
                    int sayi = Convert.ToInt16(sorgu.ExecuteScalar());

                    if (sayi == 0)
                    {
                        string userid = Session["KULLANICI_ID"].ToString();
                        sorgu.CommandText = "INSERT INTO RUHSAT_RUHSAT_VEREN (IL_ID, RUHSAT_VEREN, KAYIT_EDEN, KAYIT_TARIHI)  " +
                                            " VALUES(@IL_ID_K, @RUHSAT_VEREN_K, @UI_K, getdate() ) ";
                        sorgu.Parameters.AddWithValue("@IL_ID_K", com_il_sec_ruhsat_veren.SelectedValue);
                        sorgu.Parameters.AddWithValue("@RUHSAT_VEREN_K", txt_ruhsat_veren.Text);
                        sorgu.Parameters.AddWithValue("@UI_K", userid);
                        sorgu.ExecuteNonQuery();

                        ScriptManager.RegisterStartupScript(this, this.GetType(), System.Guid.NewGuid().ToString(), "ShowMessage('Kayıt gerçekleşmiştir.','Tamam','yeni');", true);
                        ScriptManager.RegisterStartupScript(this, this.GetType(), System.Guid.NewGuid().ToString(), "YeniMesaj('Kayıt gerçekleşmiştir.','Tamam');", true);

                        //btnkaydet_Ruhsat_Veren.Enabled = false;
                        //btnkaydet_Ruhsat_Veren.CssClass = "btn btn-success disabled";
                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(this, this.GetType(), System.Guid.NewGuid().ToString(), "YeniMesaj('Mükerrer kayıt. Lütfen kontrol ediniz.','Hata');", true);
                        ScriptManager.RegisterStartupScript(this, this.GetType(), System.Guid.NewGuid().ToString(), "ShowMessage('Mükerrer kayıt. Lütfen kontrol ediniz.','Hata','yeni');", true);
                    }
                }
                else
                {
                    string userid = Session["KULLANICI_ID"].ToString();
                    sorgu.CommandText = "UPDATE RUHSAT_RUHSAT_VEREN SET IL_ID=@IL_ID_G,  RUHSAT_VEREN=@RUHSAT_VEREN_G   WHERE ID=@ID_G ";
                    sorgu.Parameters.AddWithValue("@IL_ID_G", com_il_sec_ruhsat_veren.SelectedValue);
                    sorgu.Parameters.AddWithValue("@RUHSAT_VEREN_G", txt_ruhsat_veren.Text);
                    sorgu.Parameters.AddWithValue("@ID_G", lblidcihaz.Text);
                    sorgu.ExecuteNonQuery();

                    ScriptManager.RegisterStartupScript(this, this.GetType(), System.Guid.NewGuid().ToString(), "YeniMesaj('Kayıt güncellenmiştir.','Tamam');", true);
                    ScriptManager.RegisterStartupScript(this, this.GetType(), System.Guid.NewGuid().ToString(), "ShowMessage('Kayıt güncellenmiştir.','Tamam','yeni');", true);
                    //btnkaydet_Ruhsat_Veren.Enabled = false;
                    //btnkaydet_Ruhsat_Veren.CssClass = "btn btn-success disabled";
                }
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), System.Guid.NewGuid().ToString(), "YeniMesaj('Beklenmedik bir hata oluştu.','Hata');", true);
                ScriptManager.RegisterStartupScript(this, this.GetType(), System.Guid.NewGuid().ToString(), "ShowMessage('Hata oluştu :'" + ex.Message.Replace("'", "") + ",'Hata','yeni1');", true);
            }

            if (ConnectionState.Open == conn.State)
                conn.Close();
            listele_Ruhsat_Veren_Idare();
        }


        //------PANEL 5 - DÖNEM------\\

        void listele_Ruhsat_Donemi()
        {
            SqlCommand cmd;
            string sql = "", sql1 = "";

            sql = "SELECT RD.ID, IL.IL, RRV.RUHSAT_VEREN, RD.DONEM  FROM RUHSAT_DONEM RD " +
                  " LEFT JOIN IL ON IL.ID=RD.IL_ID" +
                  " LEFT JOIN RUHSAT_RUHSAT_VEREN RRV ON RRV.ID=RD.RUHSAT_VEREN_ID" +
                  " WHERE RD.ID>0  ";

            if (txt_Ruhsat_Donemi_ara.Text.Length > 0)
                sql1 += " AND RD.DONEM LIKE '%" + txt_Ruhsat_Donemi_ara.Text + "%'  ";
            sql = sql + sql1;
            cmd = new SqlCommand(sql, conn);
            conn.Open();
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            grid_Ruhsat_Donemi.DataSource = dt;
            grid_Ruhsat_Donemi.DataBind();
            conn.Close();
        }

        protected void btnara_Ruhsat_Donemi_Click(object sender, EventArgs e)
        {
            listele_Ruhsat_Donemi();
        }

        protected void btn_Ruhsat_Donemi_ekle_Click(object sender, EventArgs e)
        {
            lblmodalyenibaslik.Text = "Ruhsat Dönemi - Kayıt";
            lbl_il_sec.Text = "RDKK";
            il_sec();

            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            sb.Append(@"<script type='text/javascript'>");
            sb.Append("$(\"#ModalRuhsatVeri\").modal(\"show\");");
            sb.Append(@"</script>");
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(),
                       "ModalScript", sb.ToString(), false);

            Panel_Ruhsat_Veren.Visible = false;
            Panel_Donem.Visible = true;
            Panel_Kazi_Turu.Visible = false;
            Panel_Kazi_Tipi.Visible = false;
            Panel_Turksat_Proje_Tipi.Visible = false;
            Panel_Aykome_Proje_Tipi.Visible = false;
            Panel_Kazi_Birimi.Visible = false;
            Panel_Ruhsat_Tarife.Visible = false;
        }

        protected void grid_Ruhsat_Donemi_RowCommand(object sender, GridViewCommandEventArgs e)
        {

        }

        protected void grid_Ruhsat_Donemi_SelectedIndexChanging(object sender, GridViewSelectEventArgs e)
        {

        }

        protected void com_il_sec_Donem_SelectedIndexChanged(object sender, EventArgs e)
        {
            ruhsat_veren_idare_sec();
        }

        protected void btnkaydet_Donem_Click(object sender, EventArgs e)
        {
            try
            {
                SqlCommand sorgu = new SqlCommand();
                sorgu.Connection = conn;
                conn.Open();

                if (lblidcihaz.Text == "")
                {
                    sorgu.CommandText = "SELECT COUNT(*) FROM RUHSAT_DONEM WHERE RUHSAT_VEREN_ID=@SAY1 ";
                    sorgu.Parameters.AddWithValue("@SAY1", com_ruhsat_veren_sec_donem.SelectedValue);
                    int sayi = Convert.ToInt16(sorgu.ExecuteScalar());

                    sorgu.CommandText = "SELECT COUNT(*) FROM RUHSAT_DONEM WHERE DONEM=@SAY2 ";
                    sorgu.Parameters.AddWithValue("@SAY2", txt_donem.Text);
                    int sayi1 = Convert.ToInt16(sorgu.ExecuteScalar());

                    if (sayi == 0 || sayi1 == 0)
                    {
                        string userid = Session["KULLANICI_ID"].ToString();
                        sorgu.CommandText = "INSERT INTO RUHSAT_DONEM (IL_ID, RUHSAT_VEREN_ID, DONEM, KAYIT_EDEN, KAYIT_TARIHI)  " +
                                            " VALUES(@IL_ID_K, @RUHSAT_VEREN_ID_K, @DONEM_K, @UI_K, getdate() ) ";
                        sorgu.Parameters.AddWithValue("@IL_ID_K", com_il_sec_Donem.SelectedValue);
                        sorgu.Parameters.AddWithValue("@RUHSAT_VEREN_ID_K", com_ruhsat_veren_sec_donem.SelectedValue);
                        sorgu.Parameters.AddWithValue("@DONEM_K", txt_donem.Text);
                        sorgu.Parameters.AddWithValue("@UI_K", userid);
                        sorgu.ExecuteNonQuery();

                        ScriptManager.RegisterStartupScript(this, this.GetType(), System.Guid.NewGuid().ToString(), "ShowMessage('Kayıt gerçekleşmiştir.','Tamam','yeni');", true);
                        ScriptManager.RegisterStartupScript(this, this.GetType(), System.Guid.NewGuid().ToString(), "YeniMesaj('Kayıt gerçekleşmiştir.','Tamam');", true);

                        //btnkaydet_Donem.Enabled = false;
                        //btnkaydet_Donem.CssClass = "btn btn-success disabled";
                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(this, this.GetType(), System.Guid.NewGuid().ToString(), "YeniMesaj('Mükerrer kayıt. Lütfen kontrol ediniz.','Hata');", true);
                        ScriptManager.RegisterStartupScript(this, this.GetType(), System.Guid.NewGuid().ToString(), "ShowMessage('Mükerrer kayıt. Lütfen kontrol ediniz.','Hata','yeni');", true);
                    }
                }
                else
                {
                    string userid = Session["KULLANICI_ID"].ToString();
                    sorgu.CommandText = "UPDATE RUHSAT_DONEM SET IL_ID=@IL_ID_G,  RUHSAT_VEREN_ID=@RUHSAT_VEREN_ID_G, DONEM=@DONEM_G   WHERE ID=@ID_G ";
                    sorgu.Parameters.AddWithValue("@IL_ID_G", com_il_sec_Donem.SelectedValue);
                    sorgu.Parameters.AddWithValue("@RUHSAT_VEREN_ID_G", com_ruhsat_veren_sec_donem.SelectedValue);
                    sorgu.Parameters.AddWithValue("@DONEM_G", txt_donem.Text);
                    sorgu.Parameters.AddWithValue("@ID_G", lblidcihaz.Text);
                    sorgu.ExecuteNonQuery();

                    ScriptManager.RegisterStartupScript(this, this.GetType(), System.Guid.NewGuid().ToString(), "YeniMesaj('Kayıt güncellenmiştir.','Tamam');", true);
                    ScriptManager.RegisterStartupScript(this, this.GetType(), System.Guid.NewGuid().ToString(), "ShowMessage('Kayıt güncellenmiştir.','Tamam','yeni');", true);
                    //btnkaydet_Donem.Enabled = false;
                    //btnkaydet_Donem.CssClass = "btn btn-success disabled";
                }
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), System.Guid.NewGuid().ToString(), "YeniMesaj('Beklenmedik bir hata oluştu.','Hata');", true);
                ScriptManager.RegisterStartupScript(this, this.GetType(), System.Guid.NewGuid().ToString(), "ShowMessage('Hata oluştu :'" + ex.Message.Replace("'", "") + ",'Hata','yeni1');", true);
            }

            if (ConnectionState.Open == conn.State)
                conn.Close();
            listele_Ruhsat_Donemi();
        }

        //------PANEL 6 - KAZI TÜRÜ------\\

        void listele_Kazi_Turu()
        {
            SqlCommand cmd;
            string sql = "", sql1 = "";

            sql = "SELECT RKT.ID, IL.IL, RRV.RUHSAT_VEREN, RD.DONEM, RKT.KAZI_TURU  FROM RUHSAT_KAZI_TURU RKT " +
                  " LEFT JOIN IL ON IL.ID=RKT.IL_ID" +
                  " LEFT JOIN RUHSAT_RUHSAT_VEREN RRV ON RRV.ID=RKT.RUHSAT_VEREN_ID" +
                  " LEFT JOIN RUHSAT_DONEM RD ON RD.ID=RKT.DONEM_ID" +
                  " WHERE RKT.ID>0  ";

            if (txt_Kazi_Turu_ara.Text.Length > 0)
                sql1 += " AND RKT.KAZI_TURU LIKE '%" + txt_Kazi_Turu_ara.Text + "%'  ";
            sql = sql + sql1;
            cmd = new SqlCommand(sql, conn);
            conn.Open();
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            grid_Kazi_Turu.DataSource = dt;
            grid_Kazi_Turu.DataBind();
            conn.Close();
        }

        protected void btnara_Kazi_Turu_Click(object sender, EventArgs e)
        {
            listele_Kazi_Turu();
        }

        protected void btn_Kazi_Turu_ekle_Click(object sender, EventArgs e)
        {
            lblmodalyenibaslik.Text = "Kazı Türü - Kayıt";
            lbl_il_sec.Text = "KTURUK";
            il_sec();

            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            sb.Append(@"<script type='text/javascript'>");
            sb.Append("$(\"#ModalRuhsatVeri\").modal(\"show\");");
            sb.Append(@"</script>");
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(),
                       "ModalScript", sb.ToString(), false);

            Panel_Ruhsat_Veren.Visible = false;
            Panel_Donem.Visible = false;
            Panel_Kazi_Turu.Visible = true;
            Panel_Kazi_Tipi.Visible = false;
            Panel_Turksat_Proje_Tipi.Visible = false;
            Panel_Aykome_Proje_Tipi.Visible = false;
            Panel_Kazi_Birimi.Visible = false;
            Panel_Ruhsat_Tarife.Visible = false;
        }

        protected void grid_Kazi_Turu_RowCommand(object sender, GridViewCommandEventArgs e)
        {

        }

        protected void grid_Kazi_Turu_SelectedIndexChanging(object sender, GridViewSelectEventArgs e)
        {

        }

        protected void com_il_sec_Kazi_Turu_SelectedIndexChanged(object sender, EventArgs e)
        {
            ruhsat_veren_idare_sec();
        }

        protected void com_ruhsat_veren_sec_Kazi_Turu_SelectedIndexChanged(object sender, EventArgs e)
        {
            ruhsat_kazi_turu_sec();
        }

        protected void btnkaydet_Kazi_Turu_Click(object sender, EventArgs e)
        {
            try
            {
                SqlCommand sorgu = new SqlCommand();
                sorgu.Connection = conn;
                conn.Open();

                if (lblidcihaz.Text == "")
                {
                    sorgu.CommandText = "SELECT COUNT(*) FROM RUHSAT_KAZI_TURU WHERE DONEM_ID=@SAY1 ";
                    sorgu.Parameters.AddWithValue("@SAY1", com_donem_sec_Kazi_Turu.SelectedValue);
                    int sayi = Convert.ToInt16(sorgu.ExecuteScalar());

                    sorgu.CommandText = "SELECT COUNT(*) FROM RUHSAT_KAZI_TURU WHERE KAZI_TURU=@SAY2 ";
                    sorgu.Parameters.AddWithValue("@SAY2", txt_kazi_turu_Kazi_Turu.Text);
                    int sayi1 = Convert.ToInt16(sorgu.ExecuteScalar());

                    if (sayi == 0 || sayi1 == 0)
                    {
                        string userid = Session["KULLANICI_ID"].ToString();
                        sorgu.CommandText = "INSERT INTO RUHSAT_KAZI_TURU (IL_ID, RUHSAT_VEREN_ID, DONEM_ID, KAZI_TURU,  KAYIT_EDEN, KAYIT_TARIHI)  " +
                                            " VALUES(@IL_ID_K, @RUHSAT_VEREN_ID_K, @DONEM_ID_K, @KAZI_TURU_K, @UI_K, getdate() ) ";
                        sorgu.Parameters.AddWithValue("@IL_ID_K", com_il_sec_Kazi_Turu.SelectedValue);
                        sorgu.Parameters.AddWithValue("@RUHSAT_VEREN_ID_K", com_ruhsat_veren_sec_Kazi_Turu.SelectedValue);
                        sorgu.Parameters.AddWithValue("@DONEM_ID_K", com_donem_sec_Kazi_Turu.SelectedValue);
                        sorgu.Parameters.AddWithValue("@KAZI_TURU_K", txt_kazi_turu_Kazi_Turu.Text);
                        sorgu.Parameters.AddWithValue("@UI_K", userid);
                        sorgu.ExecuteNonQuery();

                        ScriptManager.RegisterStartupScript(this, this.GetType(), System.Guid.NewGuid().ToString(), "ShowMessage('Kayıt gerçekleşmiştir.','Tamam','yeni');", true);
                        ScriptManager.RegisterStartupScript(this, this.GetType(), System.Guid.NewGuid().ToString(), "YeniMesaj('Kayıt gerçekleşmiştir.','Tamam');", true);

                        //btnkaydet_Kazi_Turu.Enabled = false;
                        //btnkaydet_Kazi_Turu.CssClass = "btn btn-success disabled";
                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(this, this.GetType(), System.Guid.NewGuid().ToString(), "YeniMesaj('Mükerrer kayıt. Lütfen kontrol ediniz.','Hata');", true);
                        ScriptManager.RegisterStartupScript(this, this.GetType(), System.Guid.NewGuid().ToString(), "ShowMessage('Mükerrer kayıt. Lütfen kontrol ediniz.','Hata','yeni');", true);
                    }
                }
                else
                {
                    string userid = Session["KULLANICI_ID"].ToString();
                    sorgu.CommandText = "UPDATE RUHSAT_KAZI_TURU SET IL_ID=@IL_ID_G,  RUHSAT_VEREN_ID=@RUHSAT_VEREN_ID_G, DONEM_ID=@DONEM_ID_G, KAZI_TURU=@KAZI_TURU_G    WHERE ID=@ID_G ";
                    sorgu.Parameters.AddWithValue("@IL_ID_G", com_il_sec_Kazi_Turu.SelectedValue);
                    sorgu.Parameters.AddWithValue("@RUHSAT_VEREN_ID_G", com_ruhsat_veren_sec_Kazi_Turu.SelectedValue);
                    sorgu.Parameters.AddWithValue("@DONEM_ID_G", com_donem_sec_Kazi_Turu.SelectedValue);
                    sorgu.Parameters.AddWithValue("@KAZI_TURU_G", txt_kazi_turu_Kazi_Turu.Text);
                    sorgu.Parameters.AddWithValue("@ID_G", lblidcihaz.Text);
                    sorgu.ExecuteNonQuery();

                    ScriptManager.RegisterStartupScript(this, this.GetType(), System.Guid.NewGuid().ToString(), "YeniMesaj('Kayıt güncellenmiştir.','Tamam');", true);
                    ScriptManager.RegisterStartupScript(this, this.GetType(), System.Guid.NewGuid().ToString(), "ShowMessage('Kayıt güncellenmiştir.','Tamam','yeni');", true);
                    //btnkaydet_Kazi_Turu.Enabled = false;
                    //btnkaydet_Kazi_Turu.CssClass = "btn btn-success disabled";
                }
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), System.Guid.NewGuid().ToString(), "YeniMesaj('Beklenmedik bir hata oluştu.','Hata');", true);
                ScriptManager.RegisterStartupScript(this, this.GetType(), System.Guid.NewGuid().ToString(), "ShowMessage('Hata oluştu :'" + ex.Message.Replace("'", "") + ",'Hata','yeni1');", true);
            }

            if (ConnectionState.Open == conn.State)
                conn.Close();
            listele_Kazi_Turu();
        }

        //------PANEL 7 - KAZI TİPİ------\\

        void listele_Kazi_Tipi_Tarife()
        {
            SqlCommand cmd;
            string sql = "", sql1 = "";

            sql = "SELECT RKTIP.ID, IL.IL, RRV.RUHSAT_VEREN, RD.DONEM, RKT.KAZI_TURU, RKTIP.KAZI_TIPI,RKTIP.UCRET, CASE WHEN ISNULL(RKTIP.KDV, 0)=1 THEN 'Dahil' ELSE 'Hariç' END AS KDV, RKB.BIRIM   FROM RUHSAT_KAZI_TIPI RKTIP " +
                  " LEFT JOIN IL ON IL.ID=RKTIP.IL_ID" +
                  " LEFT JOIN RUHSAT_RUHSAT_VEREN RRV ON RRV.ID=RKTIP.RUHSAT_VEREN_ID" +
                  " LEFT JOIN RUHSAT_DONEM RD ON RD.ID=RKTIP.DONEM_ID" +
                  " LEFT JOIN RUHSAT_KAZI_TURU RKT ON RKT.ID=RKTIP.KAZI_TURU_ID" +
                  " LEFT JOIN RUHSAT_KAZI_BIRIMI RKB ON RKB.ID=RKTIP.BIRIM_ID" +
                  " ";

            if (txt_Kazi_Tipi_Tarife_ara.Text.Length > 0)
                sql1 += " AND RKTIP.KAZI_TURU LIKE '%" + txt_Kazi_Tipi_Tarife_ara.Text + "%'  ";
            //sql = sql + sql1;
            sql = sql + tkod.yetki_tablosu_il()  + "WHERE RKTIP.ID > 0  " + tkod.yetki_tablosu_2_il() + sql1 +   " ORDER BY ID";
            cmd = new SqlCommand(sql, conn);
            conn.Open();
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            grid_Kazi_Tipi_Tarife.DataSource = dt;
            grid_Kazi_Tipi_Tarife.DataBind();
            conn.Close();
        }

        protected void btnara_Kazi_Tipi_Tarife_Click(object sender, EventArgs e)
        {
            listele_Kazi_Tipi_Tarife();
        }

        protected void btn_Kazi_Tipi_Tarife_ekle_Click(object sender, EventArgs e)
        {
            lblmodalyenibaslik.Text = "Kazı Tipi ve Tarife - Kayıt";
            lbl_il_sec.Text = "KTIPIK";
            il_sec();
            ruhsat_kazi_birim_sec();
            chc_Kazi_Tipi_kontrol();

            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            sb.Append(@"<script type='text/javascript'>");
            sb.Append("$(\"#ModalRuhsatVeri\").modal(\"show\");");
            sb.Append(@"</script>");
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(),
                       "ModalScript", sb.ToString(), false);

            Panel_Ruhsat_Veren.Visible = false;
            Panel_Donem.Visible = false;
            Panel_Kazi_Turu.Visible = false;
            Panel_Kazi_Tipi.Visible = true;
            Panel_Turksat_Proje_Tipi.Visible = false;
            Panel_Aykome_Proje_Tipi.Visible = false;
            Panel_Kazi_Birimi.Visible = false;
            Panel_Ruhsat_Tarife.Visible = false;
        }

        protected void grid_Kazi_Tipi_Tarife_RowCommand(object sender, GridViewCommandEventArgs e)
        {

        }

        protected void grid_Kazi_Tipi_Tarife_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {

        }

        protected void com_il_sec_Kazi_Tipi_SelectedIndexChanged(object sender, EventArgs e)
        {
            ruhsat_veren_idare_sec();
        }

        protected void com_ruhsat_veren_sec_Kazi_Tipi_SelectedIndexChanged(object sender, EventArgs e)
        {
            ruhsat_donem_sec();
        }

        protected void com_donem_sec_Kazi_Tipi_SelectedIndexChanged(object sender, EventArgs e)
        {
            ruhsat_kazi_turu_sec();
        }

        protected void btnkaydet_Kazi_Tipi_Click(object sender, EventArgs e)
        {
            try
            {
                SqlCommand sorgu = new SqlCommand();
                sorgu.Connection = conn;
                conn.Open();

                if (lblidcihaz.Text == "")
                {
                    sorgu.CommandText = "SELECT COUNT(*) FROM RUHSAT_KAZI_TIPI WHERE DONEM_ID=@SAY1 ";
                    sorgu.Parameters.AddWithValue("@SAY1", com_donem_sec_Kazi_Tipi.SelectedValue);
                    int sayi = Convert.ToInt16(sorgu.ExecuteScalar());

                    sorgu.CommandText = "SELECT COUNT(*) FROM RUHSAT_KAZI_TIPI WHERE KAZI_TURU_ID=@SAY2 ";
                    sorgu.Parameters.AddWithValue("@SAY2", com_kazi_turu_Kazi_Tipi.SelectedValue);
                    int sayi1 = Convert.ToInt16(sorgu.ExecuteScalar());

                    sorgu.CommandText = "SELECT COUNT(*) FROM RUHSAT_KAZI_TIPI WHERE KAZI_TIPI=@SAY3 ";
                    sorgu.Parameters.AddWithValue("@SAY3", txt_kazi_tipi_Kazi_Tipi.Text);
                    int sayi2 = Convert.ToInt16(sorgu.ExecuteScalar());

                    if (sayi == 0 || sayi1 == 0 || sayi2 == 0)
                    {
                        string userid = Session["KULLANICI_ID"].ToString();
                        sorgu.CommandText = "INSERT INTO RUHSAT_KAZI_TIPI (IL_ID, RUHSAT_VEREN_ID, DONEM_ID, KAZI_TURU_ID, KAZI_TIPI, UCRET, KDV, BIRIM_ID,  KAYIT_EDEN, KAYIT_TARIHI)  " +
                                            " VALUES(@IL_ID_K, @RUHSAT_VEREN_ID_K, @DONEM_ID_K, @KAZI_TURU_ID_K, @KAZI_TIPI_K, @UCRET_K, @KDV_K, @BIRIM_ID_K,   @UI_K, getdate() ) ";
                        sorgu.Parameters.AddWithValue("@IL_ID_K", com_il_sec_Kazi_Tipi.SelectedValue);
                        sorgu.Parameters.AddWithValue("@RUHSAT_VEREN_ID_K", com_ruhsat_veren_sec_Kazi_Tipi.SelectedValue);
                        sorgu.Parameters.AddWithValue("@DONEM_ID_K", com_donem_sec_Kazi_Tipi.SelectedValue);
                        sorgu.Parameters.AddWithValue("@KAZI_TURU_ID_K", com_kazi_turu_Kazi_Tipi.SelectedValue);
                        sorgu.Parameters.AddWithValue("@KAZI_TIPI_K", txt_kazi_tipi_Kazi_Tipi.Text);
                        sorgu.Parameters.AddWithValue("@UCRET_K", Convert.ToDecimal(txt_kazi_tarife_Kazi_Tipi.Text));
                        sorgu.Parameters.AddWithValue("@KDV_K", chc_kdv_Kazi_Tipi.Checked);
                        sorgu.Parameters.AddWithValue("@BIRIM_ID_K", com_birim_sec_Kazi_Tipi.SelectedValue);
                        sorgu.Parameters.AddWithValue("@UI_K", userid);
                        sorgu.ExecuteNonQuery();

                        ScriptManager.RegisterStartupScript(this, this.GetType(), System.Guid.NewGuid().ToString(), "ShowMessage('Kayıt gerçekleşmiştir.','Tamam','yeni');", true);
                        ScriptManager.RegisterStartupScript(this, this.GetType(), System.Guid.NewGuid().ToString(), "YeniMesaj('Kayıt gerçekleşmiştir.','Tamam');", true);

                        //btnkaydet_Kazi_Tipi.Enabled = false;
                        //btnkaydet_Kazi_Tipi.CssClass = "btn btn-success disabled";
                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(this, this.GetType(), System.Guid.NewGuid().ToString(), "YeniMesaj('Mükerrer kayıt. Lütfen kontrol ediniz.','Hata');", true);
                        ScriptManager.RegisterStartupScript(this, this.GetType(), System.Guid.NewGuid().ToString(), "ShowMessage('Mükerrer kayıt. Lütfen kontrol ediniz.','Hata','yeni');", true);
                    }
                }
                else
                {
                    string userid = Session["KULLANICI_ID"].ToString();
                    sorgu.CommandText = "UPDATE RUHSAT_KAZI_TIPI SET IL_ID=@IL_ID_G,  RUHSAT_VEREN_ID=@RUHSAT_VEREN_ID_G, DONEM_ID=@DONEM_ID_G, KAZI_TURU_ID=@KAZI_TURU_ID_G, KAZI_TIPI=@KAZI_TIPI_G, UCRET=@UCRET_G, KDV=@KDV_G, BIRIM_ID=@BIRIM_ID_G     WHERE ID=@ID_G ";
                    sorgu.Parameters.AddWithValue("@IL_ID_G", com_il_sec_Kazi_Tipi.SelectedValue);
                    sorgu.Parameters.AddWithValue("@RUHSAT_VEREN_ID_G", com_ruhsat_veren_sec_Kazi_Tipi.SelectedValue);
                    sorgu.Parameters.AddWithValue("@DONEM_ID_G", com_donem_sec_Kazi_Tipi.SelectedValue);
                    sorgu.Parameters.AddWithValue("@KAZI_TURU_ID_G", com_kazi_turu_Kazi_Tipi.SelectedValue);
                    sorgu.Parameters.AddWithValue("@KAZI_TIPI_G", txt_kazi_tipi_Kazi_Tipi.Text);
                    sorgu.Parameters.AddWithValue("@UCRET_G", txt_kazi_tarife_Kazi_Tipi.Text);
                    sorgu.Parameters.AddWithValue("@KDV_G", chc_kdv_Kazi_Tipi.Checked);
                    sorgu.Parameters.AddWithValue("@BIRIM_ID_G", com_birim_sec_Kazi_Tipi.SelectedValue);
                    sorgu.Parameters.AddWithValue("@ID_G", lblidcihaz.Text);
                    sorgu.ExecuteNonQuery();

                    ScriptManager.RegisterStartupScript(this, this.GetType(), System.Guid.NewGuid().ToString(), "YeniMesaj('Kayıt güncellenmiştir.','Tamam');", true);
                    ScriptManager.RegisterStartupScript(this, this.GetType(), System.Guid.NewGuid().ToString(), "ShowMessage('Kayıt güncellenmiştir.','Tamam','yeni');", true);
                    //btnkaydet_Kazi_Tipi.Enabled = false;
                    //btnkaydet_Kazi_Tipi.CssClass = "btn btn-success disabled";
                }
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), System.Guid.NewGuid().ToString(), "YeniMesaj('Beklenmedik bir hata oluştu.','Hata');", true);
                ScriptManager.RegisterStartupScript(this, this.GetType(), System.Guid.NewGuid().ToString(), "ShowMessage('Hata oluştu :'" + ex.Message.Replace("'", "") + ",'Hata','yeni1');", true);
            }

            if (ConnectionState.Open == conn.State)
                conn.Close();
            listele_Kazi_Tipi_Tarife();
        }

        protected void chc_kdv_Kazi_Tipi_CheckedChanged(object sender, EventArgs e)
        {
            chc_Kazi_Tipi_kontrol();
        }

        void chc_Kazi_Tipi_kontrol()
        {
            if (chc_kdv_Kazi_Tipi.Checked == true)
            {
                lbl_kdv_Kazi_Tipi.Text = "Dahil";
            }
            else
            {
                lbl_kdv_Kazi_Tipi.Text = "Hariç";
            }
        }

        //------PANEL 8 - RUHSAT TARIFE ------\\

        void listele_Ruhsat_Tarife()
        {
            SqlCommand cmd;
            string sql = "", sql1 = "";

            sql = "SELECT RT.ID, IL.IL, RRV.RUHSAT_VEREN, RD.DONEM, RKT.KAZI_TURU, RT.KESIF_KONTROL_BEDELI, RT.RUHSAT_BEDELI, RT.KAZI_IZIN_HARCI, RT.TEMINATA_DAHIL_OLMA_ALTLIMITI, RT.TEMINATA_DAHIL_OLMA_YUZDESI, CASE WHEN ISNULL(RT.KDV, 0)=1 THEN 'Dahil' ELSE 'Hariç' END AS KDV  FROM RUHSAT_TARIFE RT " +
                  " LEFT JOIN IL ON IL.ID=RT.IL_ID" +
                  " LEFT JOIN RUHSAT_RUHSAT_VEREN RRV ON RRV.ID=RT.RUHSAT_VEREN_ID" +
                  " LEFT JOIN RUHSAT_DONEM RD ON RD.ID=RT.DONEM_ID" +
                  " LEFT JOIN RUHSAT_KAZI_TURU RKT ON RKT.ID=RT.KAZI_TURU_ID" +
                  " WHERE RT.ID>0  ";

            if (txt_Ruhsat_Tarife_ara.Text.Length > 0)
                sql1 += " AND RT.KAZI_TURU LIKE '%" + txt_Ruhsat_Tarife_ara.Text + "%'  ";
            sql = sql + sql1;
            cmd = new SqlCommand(sql, conn);
            conn.Open();
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            grid_Ruhsat_Tarife.DataSource = dt;
            grid_Ruhsat_Tarife.DataBind();
            conn.Close();
        }

        protected void com_il_sec_Ruhsat_Tarife_SelectedIndexChanged(object sender, EventArgs e)
        {
            ruhsat_veren_idare_sec();
        }

        protected void com_ruhsat_veren_sec_Ruhsat_Tarife_SelectedIndexChanged(object sender, EventArgs e)
        {
            ruhsat_donem_sec();
        }

        protected void com_donem_sec_Ruhsat_Tarife_SelectedIndexChanged(object sender, EventArgs e)
        {
            ruhsat_kazi_turu_sec();
        }

        protected void btnkaydet_Ruhsat_Tarife_Click(object sender, EventArgs e)
        {
            try
            {
                SqlCommand sorgu = new SqlCommand();
                sorgu.Connection = conn;
                conn.Open();

                if (lblidcihaz.Text == "")
                {
                    sorgu.CommandText = "SELECT COUNT(*) FROM RUHSAT_TARIFE WHERE DONEM_ID=@SAY1 ";
                    sorgu.Parameters.AddWithValue("@SAY1", com_donem_sec_Ruhsat_Tarife.SelectedValue);
                    int sayi = Convert.ToInt16(sorgu.ExecuteScalar());

                    sorgu.CommandText = "SELECT COUNT(*) FROM RUHSAT_TARIFE WHERE KAZI_TURU_ID=@SAY2 ";
                    sorgu.Parameters.AddWithValue("@SAY2", com_kazi_turu_Ruhsat_Tarife.SelectedValue);
                    int sayi1 = Convert.ToInt16(sorgu.ExecuteScalar());

                    sorgu.CommandText = "SELECT COUNT(*) FROM RUHSAT_TARIFE WHERE RUHSAT_VEREN_ID=@SAY3 ";
                    sorgu.Parameters.AddWithValue("@SAY3", com_ruhsat_veren_sec_Ruhsat_Tarife.SelectedValue);
                    int sayi2 = Convert.ToInt16(sorgu.ExecuteScalar());

                    if (sayi == 0 || sayi1 == 0 || sayi2 == 0)
                    {
                        string userid = Session["KULLANICI_ID"].ToString();
                        sorgu.CommandText = "INSERT INTO RUHSAT_TARIFE (IL_ID, RUHSAT_VEREN_ID, DONEM_ID, KAZI_TURU_ID, KESIF_KONTROL_BEDELI, RUHSAT_BEDELI, KAZI_IZIN_HARCI, " +
                                                                        " TEMINATA_DAHIL_OLMA_ALTLIMITI, TEMINATA_DAHIL_OLMA_YUZDESI, KDV, KAYIT_EDEN, KAYIT_TARIHI)  " +
                                            " VALUES(@IL_ID_K, @RUHSAT_VEREN_ID_K, @DONEM_ID_K, @KAZI_TURU_ID_K, @KESIF_KONTROL_BEDELI_K, @RUHSAT_BEDELI_K, @KAZI_IZIN_HARCI_K," +
                                                        " @TEMINATA_DAHIL_OLMA_ALTLIMITI_K, @TEMINATA_DAHIL_OLMA_YUZDESI_K, @KDV_K,  @UI_K, getdate() ) ";
                        sorgu.Parameters.AddWithValue("@IL_ID_K", com_il_sec_Ruhsat_Tarife.SelectedValue);
                        sorgu.Parameters.AddWithValue("@RUHSAT_VEREN_ID_K", com_ruhsat_veren_sec_Ruhsat_Tarife.SelectedValue);
                        sorgu.Parameters.AddWithValue("@DONEM_ID_K", com_donem_sec_Ruhsat_Tarife.SelectedValue);
                        sorgu.Parameters.AddWithValue("@KAZI_TURU_ID_K", com_kazi_turu_Ruhsat_Tarife.SelectedValue);
                        sorgu.Parameters.AddWithValue("@KESIF_KONTROL_BEDELI_K", Convert.ToDecimal(txt_kesif_kontrol_bedeli_Ruhsat_Tarife.Text));
                        sorgu.Parameters.AddWithValue("@RUHSAT_BEDELI_K", Convert.ToDecimal(txt_ruhsat_bedeli_Ruhsat_Tarife.Text));
                        sorgu.Parameters.AddWithValue("@KAZI_IZIN_HARCI_K", Convert.ToDecimal(txt_kazi_izin_harci_Ruhsat_Tarife.Text));
                        sorgu.Parameters.AddWithValue("@TEMINATA_DAHIL_OLMA_ALTLIMITI_K", Convert.ToDecimal(txt_teminat_limiti_Ruhsat_Tarife.Text));
                        sorgu.Parameters.AddWithValue("@TEMINATA_DAHIL_OLMA_YUZDESI_K", txt_teminat_yuzde_Ruhsat_Tarife.Text);
                        sorgu.Parameters.AddWithValue("@KDV_K", chc_kdv_Ruhsat_Tarife.Checked);
                        sorgu.Parameters.AddWithValue("@UI_K", userid);
                        sorgu.ExecuteNonQuery();

                        ScriptManager.RegisterStartupScript(this, this.GetType(), System.Guid.NewGuid().ToString(), "ShowMessage('Kayıt gerçekleşmiştir.','Tamam','yeni');", true);
                        ScriptManager.RegisterStartupScript(this, this.GetType(), System.Guid.NewGuid().ToString(), "YeniMesaj('Kayıt gerçekleşmiştir.','Tamam');", true);

                        //btnkaydet_Ruhsat_Tarife.Enabled = false;
                        //btnkaydet_Ruhsat_Tarife.CssClass = "btn btn-success disabled";
                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(this, this.GetType(), System.Guid.NewGuid().ToString(), "YeniMesaj('Mükerrer kayıt. Lütfen kontrol ediniz.','Hata');", true);
                        ScriptManager.RegisterStartupScript(this, this.GetType(), System.Guid.NewGuid().ToString(), "ShowMessage('Mükerrer kayıt. Lütfen kontrol ediniz.','Hata','yeni');", true);
                    }
                }
                else
                {
                    string userid = Session["KULLANICI_ID"].ToString();
                    sorgu.CommandText = "UPDATE RUHSAT_KAZI_TIPI SET IL_ID=@IL_ID_G,  RUHSAT_VEREN_ID=@RUHSAT_VEREN_ID_G, DONEM_ID=@DONEM_ID_G, KAZI_TURU_ID=@KAZI_TURU_ID_G, " +
                        " KESIF_KONTROL_BEDELI=@KESIF_KONTROL_BEDELI_U, RUHSAT_BEDELI=@RUHSAT_BEDELI_U, KAZI_IZIN_HARCI=@KAZI_IZIN_HARCI_U, TEMINATA_DAHIL_OLMA_ALTLIMITI=@TEMINATA_DAHIL_OLMA_ALTLIMITI_U, TEMINATA_DAHIL_OLMA_YUZDESI=@TEMINATA_DAHIL_OLMA_YUZDESI_U, KDV=@KDV_U       WHERE ID=@ID_G ";
                    sorgu.Parameters.AddWithValue("@IL_ID_U", com_il_sec_Ruhsat_Tarife.SelectedValue);
                    sorgu.Parameters.AddWithValue("@RUHSAT_VEREN_ID_U", com_ruhsat_veren_sec_Ruhsat_Tarife.SelectedValue);
                    sorgu.Parameters.AddWithValue("@DONEM_ID_U", com_donem_sec_Ruhsat_Tarife.SelectedValue);
                    sorgu.Parameters.AddWithValue("@KAZI_TURU_ID_U", com_kazi_turu_Ruhsat_Tarife.SelectedValue);
                    sorgu.Parameters.AddWithValue("@KESIF_KONTROL_BEDELI_U", Convert.ToDecimal(txt_kesif_kontrol_bedeli_Ruhsat_Tarife.Text));
                    sorgu.Parameters.AddWithValue("@RUHSAT_BEDELI_U", Convert.ToDecimal(txt_ruhsat_bedeli_Ruhsat_Tarife.Text));
                    sorgu.Parameters.AddWithValue("@KAZI_IZIN_HARCI_U", Convert.ToDecimal(txt_kazi_izin_harci_Ruhsat_Tarife.Text));
                    sorgu.Parameters.AddWithValue("@TEMINATA_DAHIL_OLMA_ALTLIMITI_U", Convert.ToDecimal(txt_teminat_limiti_Ruhsat_Tarife.Text));
                    sorgu.Parameters.AddWithValue("@TEMINATA_DAHIL_OLMA_YUZDESI_U", Convert.ToDecimal(txt_teminat_yuzde_Ruhsat_Tarife.Text));
                    sorgu.Parameters.AddWithValue("@KDV_U", chc_kdv_Ruhsat_Tarife.Checked);
                    sorgu.Parameters.AddWithValue("@ID_G", lblidcihaz.Text);
                    sorgu.ExecuteNonQuery();

                    ScriptManager.RegisterStartupScript(this, this.GetType(), System.Guid.NewGuid().ToString(), "YeniMesaj('Kayıt güncellenmiştir.','Tamam');", true);
                    ScriptManager.RegisterStartupScript(this, this.GetType(), System.Guid.NewGuid().ToString(), "ShowMessage('Kayıt güncellenmiştir.','Tamam','yeni');", true);
                    //btnkaydet_Ruhsat_Tarife.Enabled = false;
                    //btnkaydet_Ruhsat_Tarife.CssClass = "btn btn-success disabled";
                }
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), System.Guid.NewGuid().ToString(), "YeniMesaj('Beklenmedik bir hata oluştu.','Hata');", true);
                ScriptManager.RegisterStartupScript(this, this.GetType(), System.Guid.NewGuid().ToString(), "ShowMessage('Hata oluştu :'" + ex.Message.Replace("'", "") + ",'Hata','yeni1');", true);
            }

            if (ConnectionState.Open == conn.State)
                conn.Close();
            listele_Ruhsat_Tarife();
        }

        protected void btnara_Ruhsat_Tarife_Click(object sender, EventArgs e)
        {
            listele_Ruhsat_Tarife();
        }

        protected void btn_Ruhsat_Tarife_ekle_Click(object sender, EventArgs e)
        {
            {
                lblmodalyenibaslik.Text = "Ruhsat Tarife - Kayıt";
                lbl_il_sec.Text = "RT";
                il_sec();
                ruhsat_kazi_birim_sec();
                chc_Ruhsat_Tarife_kontrol();

                System.Text.StringBuilder sb = new System.Text.StringBuilder();
                sb.Append(@"<script type='text/javascript'>");
                sb.Append("$(\"#ModalRuhsatVeri\").modal(\"show\");");
                sb.Append(@"</script>");
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(),
                           "ModalScript", sb.ToString(), false);

                Panel_Ruhsat_Veren.Visible = false;
                Panel_Donem.Visible = false;
                Panel_Kazi_Turu.Visible = false;
                Panel_Kazi_Tipi.Visible = false;
                Panel_Turksat_Proje_Tipi.Visible = false;
                Panel_Aykome_Proje_Tipi.Visible = false;
                Panel_Kazi_Birimi.Visible = false;
                Panel_Ruhsat_Tarife.Visible = true;
            }
        }
        protected void grid_Ruhsat_Tarife_RowCommand(object sender, GridViewCommandEventArgs e)
        {

        }

        protected void grid_Ruhsat_Tarife_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {

        }

        void chc_Ruhsat_Tarife_kontrol()
        {
            if (chc_kdv_Ruhsat_Tarife.Checked == true)
            {
                lbl_kdv_Ruhsat_Tarife.Text = "Dahil";
            }
            else
            {
                lbl_kdv_Ruhsat_Tarife.Text = "Hariç";
            }
        }

        protected void chc_kdv_Ruhsat_Tarife_CheckedChanged(object sender, EventArgs e)
        {
            chc_Ruhsat_Tarife_kontrol();
        }











        //bitti


    }
}