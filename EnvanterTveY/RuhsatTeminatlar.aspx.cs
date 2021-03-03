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
    public partial class Teminatlar : System.Web.UI.Page
    {
        SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["KabloHE"].ConnectionString);
        string sayfa = "RuhsatTeminatlar";
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
                bolge_ara();
                listele_teminat();
            }
        }

        void il_sec()
        {
            comil_kayit.Items.Clear();
            comil_kayit.Items.Insert(0, new ListItem("İl Seçiniz", "0"));
            comil_kayit.AppendDataBoundItems = true;

            comil_kayit.DataSource = tkod.dt_il_Liste();
            comil_kayit.DataBind();
        }
        void bolge_sec()
        {
            if (comil_kayit.SelectedIndex == 0)
                combolge_kayit.Enabled = false;
            else
                combolge_kayit.Enabled = true;

            combolge_kayit.Items.Clear();
            combolge_kayit.Items.Insert(0, new ListItem("Bölge Seçiniz", "0"));
            combolge_kayit.AppendDataBoundItems = true;

            if (comil_kayit.SelectedIndex > 0)
                combolge_kayit.DataSource = tkod.dt_bolge_Liste(comil_kayit.SelectedValue.ToString());
            else
                combolge_kayit.DataSource = tkod.dt_bolge_Liste();
            combolge_kayit.DataBind();
        }
        void bolge_ara()
        {
            combolgeara.Items.Clear();
            combolgeara.Items.Insert(0, new ListItem("Bölge Seçiniz", "0"));
            combolgeara.AppendDataBoundItems = true;

            combolgeara.DataSource = tkod.dt_bolge_Liste();
            combolgeara.DataBind();
        }
        void teminatverilenyer_sec()
        {
            if (combolge_kayit.SelectedIndex == 0)
                comteminatinverilen_kayit.Enabled = false;
            else
                comteminatinverilen_kayit.Enabled = true;

            comteminatinverilen_kayit.Items.Clear();
            comteminatinverilen_kayit.Items.Insert(0, new ListItem("Teminat Verilecek Yeri Seçiniz", "0"));
            comteminatinverilen_kayit.AppendDataBoundItems = true;
            string sql2 = "";

            if (comil_kayit.SelectedIndex > 0)
                sql2 = " AND IL_ID='" + comil_kayit.SelectedValue.ToString() + "' ";

            string sql = "Select ID,RUHSAT_VEREN FROM RUHSAT_RUHSAT_VEREN WHERE ID>0 " + sql2 + " ORDER BY ID";
            comteminatinverilen_kayit.DataSource = tkod.GetData(sql);
            comteminatinverilen_kayit.DataBind();
        }

        void listele_teminat()
        {

            SqlCommand cmd;
            string sql = " ", sql1 = " ";


            sql = "SELECT V.ID, IL.IL, BOLGE.BOLGE_ADI, RRV.RUHSAT_VEREN, V.SURE, V.TARIH, FORMAT(V.TUTAR, 'N', 'tr-tr')  as TUTAR, " +
                " FORMAT(TUTAR-ISNULL(SUM(R.TEMINAT_TOPLAM),0),'N','tr-tr') AS KALAN " +
                " FROM RUHSAT_TEMINATLAR AS V " +
                " LEFT JOIN IL ON IL.ID=V.IL_ID" +
                " LEFT JOIN BOLGE ON BOLGE.ID=V.BOLGE_ID " +

                " LEFT JOIN RUHSAT_RUHSAT_VEREN AS RRV ON RRV.ID=V.RUHSAT_VEREN_ID " +
                " LEFT JOIN (SELECT * FROM RUHSAT_ODEMELER WHERE ODEME_TIPI='teminat' and TOPLAMADAHILMI='true'  ) AS R ON R.TEMINAT_ID=V.ID  " +

                "  ";

            if (combolgeara.SelectedIndex > 0)
                sql1 += " AND (V.BOLGE_ID='" + combolgeara.SelectedValue + "' )  ";

            sql = sql + tkod.yetki_tablosu() + "  WHERE V.ID > 0  " + sql1 + tkod.yetki_tablosu_2() + " " +
                " GROUP BY V.ID, IL.IL, BOLGE.BOLGE_ADI,RRV.RUHSAT_VEREN, V.SURE, V.TARIH,V.TUTAR " +
                " ORDER BY ID";

            cmd = new SqlCommand(sql, conn);
            conn.Open();
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            grid.DataSource = dt;
            grid.DataBind();
            conn.Close();
            lblgridsay.Text = " " + " Bu sayfada " + grid.Rows.Count.ToString() + " " + "adet kayıt bulunmuştur.";
        }

        void listele_teminat_ruhsat(string id)
        {
            SqlCommand cmd;
            string sql = " ", sql1 = " ";


            sql = "SELECT V.ID, V.RUHSATNO, IL.IL, BOLGE.BOLGE_ADI, RAI.ILCE,  " +
                " RTPT.T_PROJE_TIPI,  V.BASLANGIC_TARIHI, FORMAT(RO.TEMINAT_TOPLAM, 'N', 'tr-tr')  as TEMINAT_TOPLAM" +
                "       FROM RUHSAT_RUHSATLAR AS V " +
                " LEFT JOIN IL ON IL.ID=V.IL_ID" +
                " LEFT JOIN BOLGE ON BOLGE.ID=V.BOLGE_ID " +

                " LEFT JOIN RUHSAT_ADRES_ILCE AS RAI ON RAI.ID=V.ILCE_ID " +
                " LEFT JOIN RUHSAT_TURKSAT_PROJE_TIPI  AS RTPT ON RTPT.ID=V.T_PROJETIP_ID " +
                " LEFT JOIN RUHSAT_ODEMELER  AS RO ON RO.RUHSAT_ID=V.ID " +

                "  ";


            sql = sql + tkod.yetki_tablosu() + "  WHERE V.ID > 0 and RO.TEMINAT_ID= " + id + " " +  tkod.yetki_tablosu_2() + "  AND GOREV_TAMAMMI!='true'  ORDER BY ID";

            cmd = new SqlCommand(sql, conn);
            conn.Open();
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            grid_teminat_ruhsat.DataSource = dt;
            grid_teminat_ruhsat.DataBind();
            conn.Close();
            lblgrid_teminat_ruhsatsay.Text = " " + " Bu sayfada " + grid_teminat_ruhsat.Rows.Count.ToString() + " " + "adet kayıt bulunmuştur.";
        }

        protected void btnteminatekle_Click(object sender, EventArgs e)
        {
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            sb.Append(@"<script type='text/javascript'>");
            sb.Append("$(\"#ModalTeminat\").modal(\"show\");");
            sb.Append(@"</script>");
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(),
                       "ModalScript", sb.ToString(), false);


            panel_goruntule.Visible = false;
            panel_kayit_guncelle.Visible = true;

            lblmodalyenibaslik.Text = "Teminat Kaydet";

            if (combolge_kayit.SelectedIndex > 0) ;
            combolge_kayit.Items.Clear();

            if (comteminatinverilen_kayit.SelectedIndex > 0);
            comteminatinverilen_kayit.Items.Clear();

            txtteminatsuresi_kayit.Text = "";
            txtteminattarihi_kayit.Text = "";
            txttutar_kayit.Text = "";


            lblidcihaz.Text = "";

            il_sec();


            comil_kayit.SelectedIndex = 0;
            if (comteminatinverilen_kayit.SelectedIndex > 0)
            {
                comteminatinverilen_kayit.SelectedIndex = 0;
            }

            combolge_kayit.Enabled = false;
            comteminatinverilen_kayit.Enabled = false;

        }

        protected void grid_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName.Equals("sil"))
            {
                int index = Convert.ToInt32(e.CommandArgument) % grid.PageSize;
                lblidsil.Text = grid.DataKeys[index].Value.ToString();
                //int index = Convert.ToInt32(e.CommandArgument);
                //lblidsil.Text = grid.DataKeys[index].Value.ToString();
                lblislem_sil.Text = "teminat-sil";
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
                int index = Convert.ToInt32(e.CommandArgument) % grid.PageSize;
                string id = grid.DataKeys[index].Value.ToString();
                lblidcihaz.Text = id;

                il_sec();
                bolge_sec();
                teminatverilenyer_sec();

                panel_goruntule.Visible = false;
                panel_kayit_guncelle.Visible = true;

                comil_kayit.SelectedIndex = comil_kayit.Items.IndexOf(comil_kayit.Items.FindByText(HttpUtility.HtmlDecode(grid.Rows[index].Cells[1].Text)));
                combolge_kayit.SelectedIndex = combolge_kayit.Items.IndexOf(combolge_kayit.Items.FindByText(HttpUtility.HtmlDecode(grid.Rows[index].Cells[2].Text)));
                comteminatinverilen_kayit.SelectedIndex = comteminatinverilen_kayit.Items.IndexOf(comteminatinverilen_kayit.Items.FindByText(HttpUtility.HtmlDecode(grid.Rows[index].Cells[3].Text)));
                txtteminattarihi_kayit.Text = HttpUtility.HtmlDecode(grid.Rows[index].Cells[4].Text.Trim());
                txtteminatsuresi_kayit.Text = HttpUtility.HtmlDecode(grid.Rows[index].Cells[5].Text.Trim());
                txttutar_kayit.Text = HttpUtility.HtmlDecode(grid.Rows[index].Cells[6].Text.Trim());

                btnkaydet.Enabled = true;
                btnkaydet.CssClass = "btn btn-success";

                lblmodalyenibaslik.Text = "Teminat Güncelleme";

                System.Text.StringBuilder sb = new System.Text.StringBuilder();
                sb.Append(@"<script type='text/javascript'>");
                sb.Append("$(\"#ModalTeminat\").modal(\"show\");");
                sb.Append(@"</script>");
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(),
                           "ModalScript", sb.ToString(), false);
            }

            if (e.CommandName.Equals("goruntule"))
            {
                int index = Convert.ToInt32(e.CommandArgument) % grid.PageSize;
                string id = grid.DataKeys[index].Value.ToString();
                lblidcihaz.Text = id;

                lblmodalyenibaslik.Text = "Teminat Bilgileri Görüntüle";

                panel_goruntule.Visible = true;
                panel_kayit_guncelle.Visible = false;

                lblil_goster.Text = HttpUtility.HtmlDecode(grid.Rows[index].Cells[1].Text.Trim());
                lblbolge_goster.Text = HttpUtility.HtmlDecode(grid.Rows[index].Cells[2].Text.Trim());
                lblteminatverilenyer_goster.Text = HttpUtility.HtmlDecode(grid.Rows[index].Cells[3].Text.Trim());
                lbltarih_goster.Text = HttpUtility.HtmlDecode(grid.Rows[index].Cells[4].Text.Trim());
                lblsure_goster.Text = HttpUtility.HtmlDecode(grid.Rows[index].Cells[5].Text.Trim());
                lbltutar_goster.Text = HttpUtility.HtmlDecode(grid.Rows[index].Cells[6].Text.Trim());
                lblkalantutar_goster.Text = HttpUtility.HtmlDecode(grid.Rows[index].Cells[7].Text.Trim());

                listele_teminat_ruhsat(id);

                System.Text.StringBuilder sb = new System.Text.StringBuilder();
                sb.Append(@"<script type='text/javascript'>");
                sb.Append("$(\"#ModalTeminat\").modal(\"show\");");
                sb.Append(@"</script>");
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(),
                           "ModalScript", sb.ToString(), false);
            }
        }

        protected void grid_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            grid.PageIndex = e.NewPageIndex;
            listele_teminat();
        }

        protected void btnsil_Click(object sender, EventArgs e)
        {
            if (lblislem_sil.Text == "teminat-sil")
            {
                SqlCommand sorgu = new SqlCommand();
                sorgu.Connection = conn;
                conn.Open();

                sorgu.CommandText = "SELECT COUNT(*) FROM RUHSAT_ODEMELER WHERE TEMINAT_ID=@SAY1 ";
                sorgu.Parameters.AddWithValue("@SAY1", lblidcihaz.Text);
                int sayi = Convert.ToInt16(sorgu.ExecuteScalar());

                sorgu.CommandText = "SELECT DURUM FROM RUHSAT_TEMINATLAR WHERE ID=@SAY2 ";
                sorgu.Parameters.AddWithValue("@SAY2", lblidcihaz.Text);
                int sayi2 = Convert.ToInt16(sorgu.ExecuteScalar());

                if (sayi == 0 &&  sayi2 == 1)
                {
                    sorgu.CommandText = "DELETE FROM RUHSAT_TEMINATLAR WHERE ID=@SIL1";
                    sorgu.Parameters.AddWithValue("@SIL1", lblidcihaz.Text);
                    sorgu.ExecuteNonQuery();

                    conn.Close();
                    ScriptManager.RegisterStartupScript(this, this.GetType(), System.Guid.NewGuid().ToString(), "YeniMesaj('Teminat kaydı silinmiştir.','Tamam');", true);
                    ScriptManager.RegisterStartupScript(this, this.GetType(), System.Guid.NewGuid().ToString(), "ShowMessage('Teminat kaydı silinmiştir.','Tamam','sil');", true);

                    btnsil.Enabled = false;
                    btnsil.CssClass = "btn btn-danger disabled";

                    listele_teminat();
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), System.Guid.NewGuid().ToString(), "YeniMesaj('Teminat kullanımdadır.','Hata');", true);
                    ScriptManager.RegisterStartupScript(this, this.GetType(), System.Guid.NewGuid().ToString(), "ShowMessage('Teminat kullanımdadır.','Hata','sil');", true);
                    btnsil.Enabled = false;
                    btnsil.CssClass = "btn btn-danger disabled";
                }
                if (ConnectionState.Open == conn.State)
                    conn.Close();
                //keşif ruhsatın metraj bilgisi, dosya bilgisi, odeme bilgisi vs varsa silme işlemi yapılamaz yap


            }
        }

        protected void comil_kayit_SelectedIndexChanged(object sender, EventArgs e)
        {
            bolge_sec();
            if (comil_kayit.SelectedIndex == 0)
            {
                combolge_kayit.Enabled = false;
                comteminatinverilen_kayit.Enabled = false;
            }
            else
            {
                combolge_kayit.Enabled = true;
                comteminatinverilen_kayit.Enabled = false;
            }
        }

        protected void combolge_kayit_SelectedIndexChanged(object sender, EventArgs e)
        {
            teminatverilenyer_sec();
            if (combolge_kayit.SelectedIndex == 0)
                comteminatinverilen_kayit.Enabled = false;
            else
                comteminatinverilen_kayit.Enabled = true;
        }

        protected void btnara_Click(object sender, EventArgs e)
        {
            listele_teminat();
        }

        protected void btnkaydet_Click(object sender, EventArgs e)
        {
            SqlCommand sorgu = new SqlCommand();
            sorgu.Connection = conn;
            conn.Open();

            string userid = Session["KULLANICI_ID"].ToString();

            try
            {

                if (lblidcihaz.Text == "")
                {
                    sorgu.CommandText = "INSERT INTO    RUHSAT_TEMINATLAR ( IL_ID, BOLGE_ID, RUHSAT_VEREN_ID, SURE, TARIH, TUTAR, KAYIT_EDEN, KAYIT_TARIHI) " +
                        " VALUES(@IL_ID_K, @B_ID_K, @RUHSAT_VEREN_ID_K, @SURE_K, @TARIH_K, @TUTAR_K, @UI_K, getdate() ); SELECT @@IDENTITY AS 'Identity' ";
                    sorgu.Parameters.AddWithValue("@IL_ID_K", comil_kayit.SelectedValue);
                    sorgu.Parameters.AddWithValue("@B_ID_K", combolge_kayit.SelectedValue);
                    sorgu.Parameters.AddWithValue("@RUHSAT_VEREN_ID_K", comteminatinverilen_kayit.SelectedValue);
                    sorgu.Parameters.AddWithValue("@SURE_K", txtteminatsuresi_kayit.Text);
                    sorgu.Parameters.AddWithValue("@TARIH_K", txtteminattarihi_kayit.Text);
                    sorgu.Parameters.AddWithValue("@TUTAR_K", txttutar_kayit.Text);
                    //sorgu.Parameters.AddWithValue("@DURUM_K", 1);
                    sorgu.Parameters.AddWithValue("@UI_K", userid);
                    string id = sorgu.ExecuteScalar().ToString();
                    lblidcihaz.Text = id;

                    ScriptManager.RegisterStartupScript(this, this.GetType(), System.Guid.NewGuid().ToString(), "YeniMesaj('Teminat başarıyla kaydedilmiştir.','Tamam');", true);
                    ScriptManager.RegisterStartupScript(this, this.GetType(), System.Guid.NewGuid().ToString(), "ShowMessage('Teminat başarıyla kaydedilmiştir.','Tamam','yeni');", true);

                    btnkaydet.Enabled = false;
                    btnkaydet.CssClass = "btn btn-success disabled";
                }
                else
                {
                    // ------   UPDATE   ------ \\
                    sorgu.CommandText = "UPDATE RUHSAT_TEMINATLAR SET IL_ID=@IL_ID_U, BOLGE_ID=@B_ID_U, RUHSAT_VEREN_ID=@RUHSAT_VEREN_ID_U, " +
                        " SURE=@SURE_U, TARIH=@TARIH_U, TUTAR=@TUTAR_U " +
                        " WHERE ID=@V_ID_U ";
                    sorgu.Parameters.AddWithValue("@IL_ID_U", comil_kayit.SelectedValue);
                    sorgu.Parameters.AddWithValue("@B_ID_U", combolge_kayit.SelectedValue);
                    sorgu.Parameters.AddWithValue("@RUHSAT_VEREN_ID_U", comteminatinverilen_kayit.SelectedValue);
                    sorgu.Parameters.AddWithValue("@SURE_U", txtteminatsuresi_kayit.Text);
                    sorgu.Parameters.AddWithValue("@TARIH_U", txtteminattarihi_kayit.Text);
                    sorgu.Parameters.AddWithValue("@TUTAR_U", Convert.ToDecimal(txttutar_kayit.Text));
                    sorgu.Parameters.AddWithValue("@V_ID_U", lblidcihaz.Text);
                    sorgu.ExecuteNonQuery();

                    // ------   UPDATE   ------ \\

                    ScriptManager.RegisterStartupScript(this, this.GetType(), System.Guid.NewGuid().ToString(), "YeniMesaj('Teminat başarıyla güncellenmiştir.','Tamam');", true);
                    ScriptManager.RegisterStartupScript(this, this.GetType(), System.Guid.NewGuid().ToString(), "ShowMessage('Teminat başarıyla güncellenmiştir.','Tamam','yeni');", true);
                    btnkaydet.Enabled = false;
                    btnkaydet.CssClass = "btn btn-success disabled";
                }
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), System.Guid.NewGuid().ToString(), "YeniMesaj('Beklenmeyen bir hata oluştu.','Hata');", true);
                ScriptManager.RegisterStartupScript(this, this.GetType(), System.Guid.NewGuid().ToString(), "ShowMessage('Hata oluştu :'" + ex.Message.Replace("'", "") + ",'Hata','yeni');", true);
            }

            if (ConnectionState.Open == conn.State)
                conn.Close();
            listele_teminat();

        }

        protected void grid_teminat_ruhsat_RowCommand(object sender, GridViewCommandEventArgs e)
        {

        }

        protected void grid_teminat_ruhsat_SelectedIndexChanging(object sender, GridViewSelectEventArgs e)
        {

        }
    }
}