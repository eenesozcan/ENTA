using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using System.Data;
using System.Data.SqlClient;
using System.Configuration;

//ok
namespace EnvanterTveY
{
    public partial class MalzemeYonetimi : System.Web.UI.Page
    {
        SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["KabloHE"].ConnectionString);
        SqlConnection conn3 = new SqlConnection(ConfigurationManager.ConnectionStrings["KabloHE"].ConnectionString);
        string sayfa = "MalzemeYonetimi   ";
        DataTable dt;
        KodT.kodlar tkod = new KodT.kodlar();
        bool gd = false;

        string hurdakontrol;

        DataTable dt_excel = new DataTable();
        string baglanti = "";

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["KULLANICI_ID"] == null)
            {
                Response.Redirect("Giris.aspx?url=" + sayfa);
            }
            if (!IsPostBack)
            {
                Bolge_Ara_Listele();
                Depo_Ara_Listele();
                Tip_Ara_Listele();
                Durum_Ara_Listele();
                listele();
                Malzeme_Tip_Listele();
            }
        }
        protected void grid_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName.Equals("sil"))
            {
                int index = Convert.ToInt32(e.CommandArgument) % grid.PageSize;
                lblmalzemeidsil.Text = grid.DataKeys[index].Value.ToString();
                lblislem_malzemesil.Text = "cihaz-sil";
                btnmalzemesil.Enabled = true;
                btnmalzemesil.CssClass = "btn btn-danger ";

                System.Text.StringBuilder sb = new System.Text.StringBuilder();
                sb.Append(@"<script type='text/javascript'>");
                sb.Append("$(\"#ModalMalzemeSil\").modal(\"show\");");
                sb.Append(@"</script>");
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(),
                           "ModalScript", sb.ToString(), false);
            }

            if (e.CommandName.Equals("guncelle"))
            {
                int index = Convert.ToInt32(e.CommandArgument) % grid.PageSize;
                string id = grid.DataKeys[index].Value.ToString();
                lblidcihaz.Text = id;

                Malzeme_Bolge_Listele();
                Malzeme_Tip_Listele();
                commalzemetip.SelectedIndex = commalzemetip.Items.IndexOf(commalzemetip.Items.FindByText(HttpUtility.HtmlDecode(grid.Rows[index].Cells[1].Text)));
                Malzeme_Tur_Listele();
                commalzemetur.SelectedIndex = commalzemetur.Items.IndexOf(commalzemetur.Items.FindByText(HttpUtility.HtmlDecode(grid.Rows[index].Cells[2].Text)));
                Malzeme_Marka_Listele();
                commalzememarka.SelectedIndex = commalzememarka.Items.IndexOf(commalzememarka.Items.FindByText(HttpUtility.HtmlDecode(grid.Rows[index].Cells[3].Text)));
                Malzeme_Model_Listele();
                commalzememodel.SelectedIndex = commalzememodel.Items.IndexOf(commalzememodel.Items.FindByText(HttpUtility.HtmlDecode(grid.Rows[index].Cells[4].Text)));

                Malzeme_DalgaBoyu_Listele();

                txtserino.Text = HttpUtility.HtmlDecode(grid.Rows[index].Cells[5].Text.Trim());
                comdurum.SelectedIndex = comdurum.Items.IndexOf(comdurum.Items.FindByText(HttpUtility.HtmlDecode(grid.Rows[index].Cells[6].Text)));
                combolge.SelectedIndex = combolge.Items.IndexOf(combolge.Items.FindByText(HttpUtility.HtmlDecode(grid.Rows[index].Cells[7].Text)));

                string dalgaboyu = tkod.sql_calistir_param("SELECT DALGABOYU FROM MALZEMELER  WHERE ID=@ID", new SqlParameter("ID", lblidcihaz.Text));
                comdalgaboyu.SelectedIndex = comdalgaboyu.Items.IndexOf(comdalgaboyu.Items.FindByValue(dalgaboyu));

                txtgarantibitistarihi.Text = tkod.sql_calistir_param("SELECT GARANTI_BITIS_TARIHI FROM MALZEMELER  WHERE ID=@ID", new SqlParameter("ID", lblidcihaz.Text));
                txtstokkodu.Text = tkod.sql_calistir1("SELECT STOK_KODU FROM MALZEMELER  WHERE ID=  '" + lblidcihaz.Text + "' ");

                Malzeme_Depo_Listele();

                comdepo.SelectedIndex = comdepo.Items.IndexOf(comdepo.Items.FindByText(HttpUtility.HtmlDecode(grid.Rows[index].Cells[8].Text)));

                lblmodalyenibaslik.Text = "Malzeme Güncelleme";

                System.Text.StringBuilder sb = new System.Text.StringBuilder();
                sb.Append(@"<script type='text/javascript'>");
                sb.Append("$(\"#ModalMalzeme\").modal(\"show\");");
                sb.Append(@"</script>");
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(),
                           "ModalScript", sb.ToString(), false);

                seri_no_span.Visible = false;

                lblguncellemedurumu.Text = tkod.sql_calistir_param("SELECT GUNCELLEME_DURUMU FROM MALZEMELER WHERE ID=@ID", new SqlParameter("ID", lblidcihaz.Text));
                if (lblguncellemedurumu.Text == "True")
                {
                    commalzemetip.Enabled = false;
                    commalzemetur.Enabled = false;
                    commalzememarka.Enabled = false;
                    commalzememodel.Enabled = false;
                    comdalgaboyu.Enabled = false;
                    txtserino.Enabled = false;
                    txtgarantibitistarihi.Enabled = false;
                    txtstokkodu.Enabled = false;
                    comdurum.Enabled = false;
                    combolge.Enabled = false;
                    comdepo.Enabled = false;

                    btnmalzemekaydet.Enabled = false;
                    btnmalzemekaydet.CssClass = "btn btn-success disabled";
                }
                else
                {
                    commalzemetip.Enabled = true;
                    commalzemetur.Enabled = true;
                    commalzememarka.Enabled = true;
                    commalzememodel.Enabled = true;
                    comdalgaboyu.Enabled = true;
                    txtserino.Enabled = true;
                    txtgarantibitistarihi.Enabled = true;
                    txtstokkodu.Enabled = true;
                    comdurum.Enabled = true;
                    combolge.Enabled = true;
                    comdepo.Enabled = true;

                    btnmalzemekaydet.Enabled = true;
                    btnmalzemekaydet.CssClass = "btn btn-success ";
                }
            }

            if (e.CommandName.Equals("islemler"))
            {
                int index = Convert.ToInt32(e.CommandArgument) % grid.PageSize;
                string id = grid.DataKeys[index].Value.ToString();
                lblidcihaz_islemler.Text = id;
                lblmevcutdurum_durum.Text = tkod.sql_calistir1("SELECT MD.DURUM FROM MALZEMELER INNER JOIN MALZEME_DURUM AS MD ON MD.ID=MALZEMELER.DURUM WHERE MALZEMELER.ID='" + id + "'");

                System.Text.StringBuilder sb = new System.Text.StringBuilder();
                sb.Append(@"<script type='text/javascript'>");
                sb.Append("$(\"#ModalIslemler\").modal(\"show\");");
                sb.Append(@"</script>");
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(),
                           "ModalScript", sb.ToString(), false);

                lokasyon_doldur();

                if (comtip_kayit.SelectedIndex > 0) ;
                comtip_kayit.Items.Clear();
                if (comvarlikadi_kayit.SelectedIndex > 0) ;
                comvarlikadi_kayit.Items.Clear();
                if (comvarlikkod_kayit.SelectedIndex > 0) ;
                comvarlikkod_kayit.Items.Clear();

                comtip_kayit.Enabled = false;
                comvarlikadi_kayit.Enabled = false;
                comvarlikkod_kayit.Enabled = false;

                lbltur_varlik.Text = HttpUtility.HtmlDecode(grid.Rows[index].Cells[2].Text.Trim());
                lblmarka_varlik.Text = HttpUtility.HtmlDecode(grid.Rows[index].Cells[3].Text.Trim());
                lblmodel_varlik.Text = HttpUtility.HtmlDecode(grid.Rows[index].Cells[4].Text.Trim());
                lblserino_varlik.Text = HttpUtility.HtmlDecode(grid.Rows[index].Cells[5].Text.Trim());
                lbldurum_varlik.Text = HttpUtility.HtmlDecode(grid.Rows[index].Cells[6].Text.Trim());

                lbldurum_varlik.CssClass = "";

                Durum_Degistir_Listele();

                btndurumdegistir_durum.Enabled = true;
                btndurumdegistir_durum.CssClass = "btn btn-success";
                txtgerekce_durum.Text = "";

                malzeme_log_listele();
                lblgarantibitistarihi.Text = tkod.sql_calistir_param("SELECT CASE WHEN GARANTI_BITIS_TARIHI > GETDATE() THEN 'Garantisi devam ediyor ' + convert(NVARCHAR,GARANTI_BITIS_TARIHI,104) ELSE 'Garantisi Bitmiştir. ' + convert(NVARCHAR,GARANTI_BITIS_TARIHI,104) END  FROM MALZEMELER WHERE ID=@ID", new SqlParameter("ID", lblidcihaz_islemler.Text));

                listele_ariza();
                listele_varlik_malzeme();

                if (grid_varlik_malzeme_baglama.Rows.Count > 0)
                {
                    btnvarlikeslestir.Enabled = false;
                    btnvarlikeslestir.CssClass = "btn btn-success disabled";
                }
                else
                {
                    btnvarlikeslestir.Enabled = true;
                    btnvarlikeslestir.CssClass = "btn btn-success";
                }
            }
        }

        protected void btnislemler_Click(object sender, EventArgs e)
        {
            lblidcihaz.Text = "";
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            sb.Append(@"<script type='text/javascript'>");
            sb.Append("$(\"#ModalMalzeme\").modal(\"show\");");
            sb.Append(@"</script>");
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(),
                       "ModalScript", sb.ToString(), false);

            Malzeme_Tip_Listele();

            Malzeme_Bolge_Listele();

            if (commalzemetur.SelectedIndex > 0) ;
            commalzemetur.Items.Clear();
            if (commalzememarka.SelectedIndex > 0) ;
            commalzememarka.Items.Clear();
            if (commalzememodel.SelectedIndex > 0) ;
            commalzememodel.Items.Clear();
            if (comdepo.SelectedIndex > 0) ;
            comdepo.Items.Clear();

            commalzemetip.SelectedIndex = 0;

            comdurum.SelectedIndex = 0;
            combolge.SelectedIndex = 0;

            txtserino.Text = "";
            btnmalzemekaydet.Enabled = true;
            btnmalzemekaydet.CssClass = "btn btn-success ";
            lblmodalyenibaslik.Text = "Yeni Malzeme Kaydet";
        }

        protected void btnara_Click(object sender, EventArgs e)
        {
            listele();
        }

        protected void combolgeara_SelectedIndexChanged(object sender, EventArgs e)
        {
            Depo_Ara_Listele();
        }

        void Bolge_Ara_Listele()
        {
            combolgeara.Items.Clear();
            combolgeara.Items.Insert(0, new ListItem("Bölge Seçiniz", "0"));
            combolgeara.AppendDataBoundItems = true;

            combolgeara.DataSource = tkod.dt_bolge_Liste();
            combolgeara.DataBind();
        }

        void Depo_Ara_Listele()
        {
            comdepoara.Items.Clear();
            comdepoara.Items.Insert(0, new ListItem("Depo Seçiniz", "0"));
            comdepoara.AppendDataBoundItems = true;

            if (combolgeara.SelectedIndex > 0)
                comdepoara.DataSource = tkod.dt_depo_Liste(combolgeara.SelectedValue.ToString());
            else
                comdepoara.DataSource = tkod.dt_depo_Liste();

            comdepoara.DataBind();
        }

        protected void comtipara_SelectedIndexChanged(object sender, EventArgs e)
        {
            Tur_Ara_Listele();
        }

        void Tip_Ara_Listele()
        {
            comtipara.Items.Clear();
            comtipara.Items.Insert(0, new ListItem("Seçiniz", "0"));
            comtipara.AppendDataBoundItems = true;

            SqlDataAdapter da = new SqlDataAdapter();
            SqlCommand cmd = new SqlCommand("Select ID,TIP FROM MALZEME_TIP ORDER BY ID ");
            DataTable dt = new DataTable();
            da.SelectCommand = cmd;
            da.SelectCommand.Connection = conn;
            da.Fill(dt);
            comtipara.DataSource = dt;
            comtipara.DataTextField = "TIP";
            comtipara.DataValueField = "ID";
            comtipara.DataBind();
        }

        void Tur_Ara_Listele()
        {
            comturara.Items.Clear();
            comturara.Items.Insert(0, new ListItem("Seçiniz", "0"));
            comturara.AppendDataBoundItems = true;
            string sql2 = "";

            if (comtipara.SelectedIndex > 0)
                sql2 = " AND TIP_ID='" + comtipara.SelectedValue.ToString() + "' ";

            string sql = "SELECT ID, TURU FROM MALZEME_TURU WHERE ID>0 " + sql2 + " ORDER BY ID";
            comturara.DataSource = tkod.GetData(sql);
            comturara.DataBind();
        }

        void Durum_Ara_Listele()
        {
            comdurumara.Items.Clear();
            comdurumara.Items.Insert(0, new ListItem("Seçiniz", "0"));
            comdurumara.AppendDataBoundItems = true;

            SqlDataAdapter da = new SqlDataAdapter();
            SqlCommand cmd = new SqlCommand("Select DEGER AS ID,SECENEK FROM COMDOLDUR WHERE COM_ADI = 'depom-malzemedurum-ara'  ORDER BY SIRA  ");
            DataTable dt = new DataTable();
            da.SelectCommand = cmd;
            da.SelectCommand.Connection = conn;
            da.Fill(dt);
            comdurumara.DataSource = dt;
            comdurumara.DataTextField = "SECENEK";
            comdurumara.DataValueField = "ID";
            comdurumara.DataBind();
        }

        protected void commalzemetip_SelectedIndexChanged(object sender, EventArgs e)
        {
            Malzeme_Tur_Listele();
        }

        protected void commalzemetur_SelectedIndexChanged(object sender, EventArgs e)
        {
            Malzeme_Marka_Listele();
            Malzeme_DalgaBoyu_Listele();
        }

        protected void commalzememarka_SelectedIndexChanged(object sender, EventArgs e)
        {
            Malzeme_Model_Listele();
        }

        void Malzeme_Tip_Listele()
        {
            commalzemetip.Items.Clear();
            commalzemetip.Items.Insert(0, new ListItem("Seçiniz", "0"));
            commalzemetip.AppendDataBoundItems = true;

            SqlDataAdapter da = new SqlDataAdapter();
            SqlCommand cmd = new SqlCommand("Select ID,TIP FROM MALZEME_TIP ORDER BY ID ");
            DataTable dt = new DataTable();
            da.SelectCommand = cmd;
            da.SelectCommand.Connection = conn;
            da.Fill(dt);
            commalzemetip.DataSource = dt;
            commalzemetip.DataTextField = "TIP";
            commalzemetip.DataValueField = "ID";
            commalzemetip.DataBind();
        }

        void Malzeme_Tur_Listele()
        {
            commalzemetur.Items.Clear();
            commalzemetur.Items.Insert(0, new ListItem("Seçiniz", "0"));
            commalzemetur.AppendDataBoundItems = true;
            string sql2 = "";

            if (commalzemetip.SelectedIndex > 0)
                sql2 = " AND TIP_ID='" + commalzemetip.SelectedValue.ToString() + "' ";

            string sql = "SELECT ID, TURU FROM MALZEME_TURU WHERE ID>0 " + sql2 + " ORDER BY ID";
            commalzemetur.DataSource = tkod.GetData(sql);
            commalzemetur.DataBind();
        }

        void Malzeme_DalgaBoyu_Listele()
        {
            comdalgaboyu.Items.Clear();
            comdalgaboyu.Items.Insert(0, new ListItem("Seçiniz", "0"));
            comdalgaboyu.AppendDataBoundItems = true;
            string sql2 = "";

            if (commalzemetur.SelectedIndex > 0)
                sql2 = " AND MALZEME_TURU='" + commalzemetur.SelectedValue.ToString() + "' ";

            string sql = "SELECT ID, DALGABOYU FROM DALGABOYU WHERE ID>0 " + sql2 + " ORDER BY ID";
            comdalgaboyu.DataSource = tkod.GetData(sql);
            comdalgaboyu.DataBind();
        }

        void Malzeme_Marka_Listele()
        {
            commalzememarka.Items.Clear();
            commalzememarka.Items.Insert(0, new ListItem("Seçiniz", "0"));
            commalzememarka.AppendDataBoundItems = true;
            string sql2 = "";

            if (commalzemetur.SelectedIndex > 0)
                sql2 = " AND TUR_ID='" + commalzemetur.SelectedValue.ToString() + "' ";

            string sql = "SELECT ID, MARKA FROM MALZEME_MARKAMODEL WHERE ID>0 " + sql2 + "  ORDER BY ID";
            commalzememarka.DataSource = tkod.GetData(sql);
            commalzememarka.DataBind();
        }

        void Malzeme_Model_Listele()
        {
            commalzememodel.Items.Clear();
            commalzememodel.Items.Insert(0, new ListItem("Seçiniz", "0"));
            commalzememodel.AppendDataBoundItems = true;
            string sql5 = "";

            if (commalzememarka.SelectedIndex > 0)
                sql5 = " AND MARKA_ID='" + commalzememarka.SelectedValue.ToString() + "' ";

            string sql = "SELECT ID,MODEL FROM MALZEME_MODEL WHERE ID>0 " + sql5 + " ";
            commalzememodel.DataSource = tkod.GetData(sql);
            commalzememodel.DataBind();
        }

        void Malzeme_Bolge_Listele()
        {
            combolge.Items.Clear();
            combolge.Items.Insert(0, new ListItem("Bölge Seçiniz", "0"));
            combolge.AppendDataBoundItems = true;

            combolge.DataSource = tkod.dt_bolge_Liste();
            combolge.DataBind();
        }

        void Malzeme_Depo_Listele()
        {
            comdepo.Items.Clear();
            comdepo.Items.Insert(0, new ListItem("Depo Seçiniz", "0"));
            comdepo.AppendDataBoundItems = true;

            if (combolge.SelectedIndex > 0)
                comdepo.DataSource = tkod.dt_depo_Liste(combolge.SelectedValue.ToString());
            else
                comdepo.DataSource = tkod.dt_depo_Liste();

            comdepo.DataBind();
        }

        protected void btnmalzemeekle_Click(object sender, EventArgs e)
        {
            lblidcihaz.Text = "";
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            sb.Append(@"<script type='text/javascript'>");
            sb.Append("$(\"#ModalMalzeme\").modal(\"show\");");
            sb.Append(@"</script>");
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(),
                       "ModalScript", sb.ToString(), false);

            seri_no_span.Visible = true;
            
            Malzeme_Tip_Listele();
            Malzeme_Bolge_Listele();

            if (commalzemetur.SelectedIndex > 0) ;
            commalzemetur.Items.Clear();
            if (commalzememarka.SelectedIndex > 0) ;
            commalzememarka.Items.Clear();
            if (commalzememodel.SelectedIndex > 0) ;
            commalzememodel.Items.Clear();
            if (comdepo.SelectedIndex > 0) ;
            comdepo.Items.Clear();
            if (comdalgaboyu.SelectedIndex > 0) ;
            comdalgaboyu.Items.Clear();

            commalzemetip.SelectedIndex = 0;

            combolge.SelectedIndex = 0;

            if (txtgarantibitistarihi.Text == "")
                txtgarantibitistarihi.Text = "31.12.2005";

            txtserino.Text = "";
            btnmalzemekaydet.Enabled = true;
            btnmalzemekaydet.CssClass = "btn btn-success ";
            lblmodalyenibaslik.Text = "Yeni Malzeme Kaydet";

        }

        protected void btnmalzemekaydet_Click(object sender, EventArgs e)
        {
            try
            {
                SqlCommand sorgu = new SqlCommand();
                sorgu.Connection = conn;
                SqlCommand sorgu1 = new SqlCommand();
                sorgu1.Connection = conn;
                conn.Open();

                string tarih = txtgarantibitistarihi.Text;
                if (tarih == "")
                    tarih = "01.01.2000";

                if (lblidcihaz.Text == "")
                {
                    List<string> lstserino = txtserino.Text.Split(',').ToList();
                    List<string> lststokkodu = txtstokkodu.Text.Split(',').ToList();
                    string stokk = "";

                    for (int i = 0; i < lstserino.Count; i++)
                    {
                        sorgu.CommandText = "SELECT COUNT(*) FROM MALZEMELER WHERE SERI_NO='" + lstserino[i] + "' ";
                        int sayi = Convert.ToInt16(sorgu.ExecuteScalar());

                        if (sayi == 0)
                        {
                            string userid = Session["KULLANICI_ID"].ToString();
                            if (i < lststokkodu.Count)
                                stokk = lststokkodu[i];
                            else
                                stokk = "";
                            sorgu.Parameters.Clear();
                            sorgu.CommandText = "INSERT INTO    MALZEMELER (MALZEME_TIP , MALZEME_TUR , MARKA , MODEL , DALGABOYU, SERI_NO, GARANTI_BITIS_TARIHI, STOK_KODU, DURUM , DEPO_ID , BOLGE_ID , KAYIT_EDEN , KAYIT_TARIHI) " +
                                " VALUES(@MTIP, @MTUR, @MMARKA, @MMODEL, @DB, @MSERI, @GBT, @SK, @MDURUM, @MDEPO, @MBOLGE, @UI, getdate() ); SELECT @@IDENTITY AS 'Identity' ";
                            sorgu.Parameters.AddWithValue("@MTIP", commalzemetip.SelectedValue);
                            sorgu.Parameters.AddWithValue("@MTUR", commalzemetur.SelectedValue);
                            sorgu.Parameters.AddWithValue("@MMARKA", commalzememarka.SelectedValue);
                            sorgu.Parameters.AddWithValue("@MMODEL", commalzememodel.SelectedValue);
                            sorgu.Parameters.AddWithValue("@DB", comdalgaboyu.SelectedValue);
                            sorgu.Parameters.AddWithValue("@MSERI", lstserino[i]);
                            sorgu.Parameters.AddWithValue("@GBT", Convert.ToDateTime(tarih));
                            sorgu.Parameters.AddWithValue("@SK", stokk);
                            sorgu.Parameters.AddWithValue("@MDURUM", "6");
                            sorgu.Parameters.AddWithValue("@MDEPO", comdepo.SelectedValue);
                            sorgu.Parameters.AddWithValue("@MBOLGE", combolge.SelectedValue);
                            sorgu.Parameters.AddWithValue("@UI", userid);
                            string id = sorgu.ExecuteScalar().ToString();
                            lblidcihaz.Text = id;

                            // ------   ***   ------ \\
                            // ------   LOG   ------ \\
                            // ------   ***   ------ \\
                            sorgu1.Parameters.Clear();
                            sorgu1.CommandText = "INSERT INTO    MALZEMELER_LOG (M_ID, SEVK_ID, E_BOLGE, E_DEPO, Y_BOLGE, Y_DEPO, ACIKLAMA, E_DURUM, Y_DURUM, GEREKCE, E_TUR, E_TIP, Y_TUR, Y_TIP, E_MARKA, E_MODEL, Y_MARKA, Y_MODEL, E_SERINO, Y_SERINO, KAYIT_EDEN, KAYIT_TARIHI) " +
                                " VALUES(@MID, @SID, @EBOLGE, @EDEPO, @YBOLGE, @YDEPO, @ACIKLAMA, @EDURUM, @YDURUM, @GRKC, @ETUR, @ETIP, @YTUR, @YTIP, @EMARKA, @EMODEL, @YMARKA, @YMODEL, @ESERINO, @YSERINO, @UI, getdate() ) ";
                            sorgu1.Parameters.AddWithValue("@MID", lblidcihaz.Text);
                            sorgu1.Parameters.AddWithValue("@SID", "0");
                            sorgu1.Parameters.AddWithValue("@EBOLGE", combolge.SelectedValue);
                            sorgu1.Parameters.AddWithValue("@EDEPO", comdepo.SelectedValue);
                            sorgu1.Parameters.AddWithValue("@YBOLGE", "0");
                            sorgu1.Parameters.AddWithValue("@YDEPO", "0");
                            sorgu1.Parameters.AddWithValue("@ACIKLAMA", "Yeni kayıt yapıldı.");
                            sorgu1.Parameters.AddWithValue("@EDURUM", "6");
                            sorgu1.Parameters.AddWithValue("@YDURUM", "0");
                            sorgu1.Parameters.AddWithValue("@GRKC", "0");
                            sorgu1.Parameters.AddWithValue("@ETUR", commalzemetur.SelectedValue);
                            sorgu1.Parameters.AddWithValue("@ETIP", commalzemetip.SelectedValue);
                            sorgu1.Parameters.AddWithValue("@YTUR", "0");
                            sorgu1.Parameters.AddWithValue("@YTIP", "0");
                            sorgu1.Parameters.AddWithValue("@EMARKA", commalzememarka.SelectedValue);
                            sorgu1.Parameters.AddWithValue("@EMODEL", commalzememodel.SelectedValue);
                            sorgu1.Parameters.AddWithValue("@YMARKA", "0");
                            sorgu1.Parameters.AddWithValue("@YMODEL", "0");
                            sorgu1.Parameters.AddWithValue("@ESERINO", lstserino[i]);
                            sorgu1.Parameters.AddWithValue("@YSERINO", "0");
                            sorgu1.Parameters.AddWithValue("@UI", userid);
                            sorgu1.ExecuteNonQuery();
                            // ------   ***   ------ \\
                            // ------   LOG   ------ \\
                            // ------   ***   ------ \\

                            btnmalzemekaydet.Enabled = false;
                            btnmalzemekaydet.CssClass = "btn btn-success disabled";

                            ScriptManager.RegisterStartupScript(this, this.GetType(), System.Guid.NewGuid().ToString(), "YeniMesaj('Malzeme başarıyla kaydedilmiştir.','Tamam');", true);
                            ScriptManager.RegisterStartupScript(this, this.GetType(), System.Guid.NewGuid().ToString(), "ShowMessage('Malzeme başarıyla kaydedilmiştir.','Tamam','yeni');", true);
                        }
                        else
                        {
                            ScriptManager.RegisterStartupScript(this, this.GetType(), System.Guid.NewGuid().ToString(), "YeniMesaj('Bu Seri Numaralı Malzeme daha önce kaydedilmişti. Lütfen kontrol ediniz.','Hata');", true);
                            ScriptManager.RegisterStartupScript(this, this.GetType(), System.Guid.NewGuid().ToString(), "ShowMessage('Bu Seri Numaralı Malzeme daha önce kaydedilmişti. Lütfen kontrol ediniz.','Hata','yeni');", true);
                        }
                    }
                }
                else
                {
                    // ------   ***   ------ \\
                    // ------   LOG   ------ \\
                    // ------   ***   ------ \\
                    sorgu.CommandText = "SELECT BOLGE_ID FROM MALZEMELER WHERE ID =@MID ";
                    sorgu.Parameters.AddWithValue("@MID", lblidcihaz.Text);
                    int ebolgeid = Convert.ToInt16(sorgu.ExecuteScalar());

                    sorgu.CommandText = "SELECT DEPO_ID FROM MALZEMELER WHERE ID =@MID1 ";
                    sorgu.Parameters.AddWithValue("@MID1", lblidcihaz.Text);
                    int edepoid = Convert.ToInt16(sorgu.ExecuteScalar());

                    sorgu.CommandText = "SELECT MALZEME_TUR FROM MALZEMELER WHERE ID =@MID2 ";
                    sorgu.Parameters.AddWithValue("@MID2", lblidcihaz.Text);
                    int etur = Convert.ToInt16(sorgu.ExecuteScalar());

                    sorgu.CommandText = "SELECT MALZEME_TIP FROM MALZEMELER WHERE ID = @MID3 ";
                    sorgu.Parameters.AddWithValue("@MID3", lblidcihaz.Text);
                    int etip = Convert.ToInt16(sorgu.ExecuteScalar());

                    sorgu.CommandText = "SELECT MARKA FROM MALZEMELER WHERE ID =@MID4 ";
                    sorgu.Parameters.AddWithValue("@MID4", lblidcihaz.Text);
                    int emarka = Convert.ToInt16(sorgu.ExecuteScalar());

                    sorgu.CommandText = "SELECT MODEL FROM MALZEMELER WHERE ID =@MID5 ";
                    sorgu.Parameters.AddWithValue("@MID5", lblidcihaz.Text);
                    int emodel = Convert.ToInt16(sorgu.ExecuteScalar());

                    sorgu.CommandText = "SELECT SERI_NO FROM MALZEMELER WHERE ID =@MID6 ";
                    sorgu.Parameters.AddWithValue("@MID6", lblidcihaz.Text);
                    string eserino = Convert.ToString(sorgu.ExecuteScalar());
                    // ------   ***   ------ \\
                    // ------   LOG   ------ \\
                    // ------   ***   ------ \\

                    // ------   UPDATE   ------ \\
                    string userid = Session["KULLANICI_ID"].ToString();
                    //string userid = "1";
                    sorgu.CommandText = "UPDATE MALZEMELER SET MALZEME_TIP=@MTIP, MALZEME_TUR=@MTUR, MARKA=@MMARKA, MODEL=@MMODEL, DALGABOYU=@DB, SERI_NO=@MSERI, GARANTI_BITIS_TARIHI=@GBT, STOK_KODU=@SK, DEPO_ID=@MDEPO, BOLGE_ID=@MBOLGE  WHERE ID=@MLZMID ";
                    sorgu.Parameters.AddWithValue("@MTIP", commalzemetip.SelectedValue);
                    sorgu.Parameters.AddWithValue("@MTUR", commalzemetur.SelectedValue);
                    sorgu.Parameters.AddWithValue("@MMARKA", commalzememarka.SelectedValue);
                    sorgu.Parameters.AddWithValue("@MMODEL", commalzememodel.SelectedValue);
                    sorgu.Parameters.AddWithValue("@DB", comdalgaboyu.SelectedValue);
                    sorgu.Parameters.AddWithValue("@MSERI", txtserino.Text);
                    sorgu.Parameters.AddWithValue("@GBT", Convert.ToDateTime(tarih));
                    sorgu.Parameters.AddWithValue("@SK", txtstokkodu.Text);
                    sorgu.Parameters.AddWithValue("@MDEPO", comdepo.SelectedValue);
                    sorgu.Parameters.AddWithValue("@MBOLGE", combolge.SelectedValue);
                    sorgu.Parameters.AddWithValue("@MLZMID", lblidcihaz.Text);
                    sorgu.ExecuteNonQuery();
                    // ------   UPDATE   ------ \\

                    // ------   ***   ------ \\
                    // ------   LOG   ------ \\
                    // ------   ***   ------ \\

                    sorgu1.CommandText = "INSERT INTO    MALZEMELER_LOG (M_ID, SEVK_ID, E_BOLGE, E_DEPO, Y_BOLGE, Y_DEPO, ACIKLAMA, E_DURUM, Y_DURUM, GEREKCE, E_TUR, E_TIP, Y_TUR, Y_TIP, E_MARKA, E_MODEL, Y_MARKA, Y_MODEL, E_SERINO, Y_SERINO, KAYIT_EDEN, KAYIT_TARIHI) " +
                        " VALUES(@MID7, @SID, @EBOLGE, @EDEPO, @YBOLGE, @YDEPO, @ACIKLAMA, @EDURUM, @YDURUM, @GRKC, @ETUR, @ETIP, @YTUR, @YTIP, @EMARKA, @EMODEL, @YMARKA, @YMODEL, @ESERINO, @YSERINO,  @UI, getdate() ) ";
                    sorgu1.Parameters.AddWithValue("@MID7", lblidcihaz.Text);
                    sorgu1.Parameters.AddWithValue("@SID", "0");
                    sorgu1.Parameters.AddWithValue("@EBOLGE", ebolgeid);
                    sorgu1.Parameters.AddWithValue("@EDEPO", edepoid);
                    sorgu1.Parameters.AddWithValue("@YBOLGE", combolge.SelectedValue);
                    sorgu1.Parameters.AddWithValue("@YDEPO", comdepo.SelectedValue);
                    sorgu1.Parameters.AddWithValue("@ACIKLAMA", "Malzeme kaydı güncellendi.");
                    sorgu1.Parameters.AddWithValue("@EDURUM", "0");
                    sorgu1.Parameters.AddWithValue("@YDURUM", "0");
                    sorgu1.Parameters.AddWithValue("@GRKC", "0");
                    sorgu1.Parameters.AddWithValue("@ETUR", etur);
                    sorgu1.Parameters.AddWithValue("@ETIP", etip);
                    sorgu1.Parameters.AddWithValue("@YTUR", commalzemetur.SelectedValue);
                    sorgu1.Parameters.AddWithValue("@YTIP", commalzemetip.SelectedValue);
                    sorgu1.Parameters.AddWithValue("@EMARKA", emarka);
                    sorgu1.Parameters.AddWithValue("@EMODEL", emodel);
                    sorgu1.Parameters.AddWithValue("@YMARKA", commalzememarka.SelectedValue);
                    sorgu1.Parameters.AddWithValue("@YMODEL", commalzememodel.SelectedValue);
                    sorgu1.Parameters.AddWithValue("@ESERINO", eserino);
                    sorgu1.Parameters.AddWithValue("@YSERINO", txtserino.Text);
                    sorgu1.Parameters.AddWithValue("@UI", userid);
                    sorgu1.ExecuteNonQuery();
                    // ------   ***   ------ \\
                    // ------   LOG   ------ \\
                    // ------   ***   ------ \\

                    ScriptManager.RegisterStartupScript(this, this.GetType(), System.Guid.NewGuid().ToString(), "YeniMesaj('Malzeme başarıyla güncellenmiştir.','Tamam');", true);
                    ScriptManager.RegisterStartupScript(this, this.GetType(), System.Guid.NewGuid().ToString(), "ShowMessage('Malzeme başarıyla güncellenmiştir.','Tamam','yeni');", true);
                    btnmalzemekaydet.Enabled = false;
                    btnmalzemekaydet.CssClass = "btn btn-success disabled";
                }
               
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), System.Guid.NewGuid().ToString(), "YeniMesaj('Beklenmeyen bir hata oluştu. Hata:" + ex.Message.ToString().Replace("'", "") + "','Hata');", true);
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

            sql = " SELECT M.ID, MALZEME_TIP.TIP, MALZEME_TURU.TURU, MALZEME_MARKAMODEL.MARKA, MALZEME_MODEL.MODEL, M.SERI_NO,'<span class=\"text rounded p-10 ' + MD.CLASS + ' \"> '+ MD.DURUM + ' </span>' AS DURUM, " +
                " BOLGE.BOLGE_ADI, DEPO.DEPO, M.GUNCELLEME_DURUMU, KULLANICI.ISIM + ' ' +CONVERT(NVARCHAR(10),M.KAYIT_TARIHI,104) AS KAYIT," +

                " CASE WHEN M.GARANTI_BITIS_TARIHI > GETDATE() THEN 'Garantisi devam ediyor. ' +  Convert(NVARCHAR(10),M.GARANTI_BITIS_TARIHI, 104) ELSE 'Garantisi Bitmiştir. ' + Convert(NVARCHAR(10),M.GARANTI_BITIS_TARIHI, 104) END AS GARANTI " +

                " FROM MALZEMELER AS M" +
                " LEFT JOIN MALZEME_DURUM AS MD ON MD.ID=M.DURUM" +
                " LEFT JOIN BOLGE ON M.BOLGE_ID=BOLGE.ID" +
                " LEFT JOIN DEPO ON M.DEPO_ID=DEPO.ID" +
                " LEFT JOIN KULLANICI ON KULLANICI.ID=M.KAYIT_EDEN" +
                " LEFT JOIN MALZEME_TIP  ON MALZEME_TIP.ID=M.MALZEME_TIP" +
                " LEFT JOIN MALZEME_TURU ON MALZEME_TURU.ID=M.MALZEME_TUR" +
                " LEFT JOIN MALZEME_MARKAMODEL ON MALZEME_MARKAMODEL.ID=M.MARKA" +
                " LEFT JOIN MALZEME_MODEL ON MALZEME_MODEL.ID=M.MODEL" +
                "  ";


            if (combolgeara.SelectedIndex > 0)
                sql1 += " AND (M.BOLGE_ID='" + combolgeara.SelectedValue + "' )  ";

            if (comdepoara.SelectedIndex > 0)
                sql1 += " AND M.DEPO_ID='" + comdepoara.SelectedValue + "'  ";

            if (comtipara.SelectedIndex > 0)
                sql1 += " AND M.MALZEME_TIP='" + comtipara.SelectedValue + "'  ";

            if (comturara.SelectedIndex > 0)
                sql1 += " AND M.MALZEME_TUR='" + comturara.SelectedValue + "'  ";

            if (comdurumara.SelectedIndex > 0)
                sql1 += " AND M.DURUM='" + comdurumara.SelectedValue + "'  ";

            if (txtserinoara.Text.Length > 0)
                sql1 += " AND M.SERI_NO LIKE '%" + txtserinoara.Text + "%'  ";

            sql = sql + tkod.yetki_tablosu_inner("DEPO", "") + "  WHERE M.ID > 0  " + sql1 + tkod.yetki_tablosu_filtre("DEPO") + "   ORDER BY ID";

            cmd = new SqlCommand(sql, conn);

            conn.Open();

            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            grid.DataSource = dt;
            grid.DataBind();
            conn.Close();
            lblmalzemesayisi.Text = grid.Rows.Count.ToString() + " " + "adet kayıt bulunmuştur.";
        }

        protected void btnsil_Click(object sender, EventArgs e)
        {
            if (lblislem.Text == "varlik-malzeme-sil")
            {
                SqlCommand sorgu = new SqlCommand();
                sorgu.Connection = conn;
                conn.Open();

                sorgu.CommandText = "UPDATE VARLIK_MALZEME SET V_M_BAGLANTI_DURUMU=@VMBD2, V_M_AYRILMA_TARIHI=@VMAT2  WHERE ID=@VMSID ";
                sorgu.Parameters.AddWithValue("@VMBD2", "0");
                sorgu.Parameters.AddWithValue("@VMAT2", Convert.ToDateTime(DateTime.Now.ToString()));
                sorgu.Parameters.AddWithValue("@VMSID", lblidsil.Text);
                sorgu.ExecuteNonQuery();

                sorgu.CommandText = "UPDATE MALZEMELER SET VARLIK_ID=@VID, DURUM=@DRM  WHERE ID=@MID ";
                sorgu.Parameters.AddWithValue("@VID", DBNull.Value);
                sorgu.Parameters.AddWithValue("@DRM", comyenidurumsec_varlikbaglantisil.SelectedValue);
                sorgu.Parameters.AddWithValue("@MID", lblidcihaz_islemler.Text);
                sorgu.ExecuteNonQuery();

                sorgu.CommandText = "UPDATE VARLIK SET V_M_BAGLANTI_DURUMU=@VMBD  WHERE ID=@VID9 ";
                sorgu.Parameters.AddWithValue("@VMBD", "0");
                sorgu.Parameters.AddWithValue("@VID9", lblvarlikid.Text);
                sorgu.ExecuteNonQuery();

                // ------   ***   ------ \\
                // ------   LOG   ------ \\
                // ------   ***   ------ \\
                string userid = Session["KULLANICI_ID"].ToString();
                sorgu.CommandText = "INSERT INTO    VARLIK_LOG (V_ID, M_ID, LOG_ACIKLAMA, KAYIT_EDEN, KAYIT_TARIHI) " +
                    " VALUES(@VID3, @MAID3, @L_ACKLM3, @UI3, getdate() ) ";
                sorgu.Parameters.AddWithValue("@VID3", lblvarlikid.Text);
                sorgu.Parameters.AddWithValue("@MAID3", lblidcihaz_islemler.Text);
                sorgu.Parameters.AddWithValue("@L_ACKLM3", "Varlık - Malzeme Eşleşme kaydı silindi.");
                sorgu.Parameters.AddWithValue("@UI3", userid);
                sorgu.ExecuteNonQuery();
                // ------   ***   ------ \\
                // ------   LOG   ------ \\
                // ------   ***   ------ \\

                // ------   ***   ------ \\
                // ------   LOG   ------ \\
                // ------   ***   ------ \\
                //string userid = "1";
                sorgu.CommandText = "INSERT INTO    MALZEMELER_LOG (M_ID, GEREKCE, ACIKLAMA, Y_DURUM, KAYIT_EDEN, KAYIT_TARIHI) " +
                    " VALUES(@MID9, @GRKC9,  @ACIKLAMA9, @YDRM9, @UI9, getdate() ) ";
                sorgu.Parameters.AddWithValue("@MID9", lblidcihaz_islemler.Text);
                sorgu.Parameters.AddWithValue("@GRKC9", txtgerekce_varlikbaglantisil.Text);
                sorgu.Parameters.AddWithValue("@ACIKLAMA9", "Durum Değişikliği yapıldı, Malzeme Varlık bağlantısı silindi.");
                sorgu.Parameters.AddWithValue("@YDRM9", comyenidurumsec_varlikbaglantisil.SelectedValue);
                sorgu.Parameters.AddWithValue("@UI9", userid);
                sorgu.ExecuteNonQuery();
                // ------   ***   ------ \\
                // ------   LOG   ------ \\
                // ------   ***   ------ \\

                conn.Close();

                ScriptManager.RegisterStartupScript(this, this.GetType(), System.Guid.NewGuid().ToString(), "YeniMesaj('Varlık ve Malzeme bağlantısı başarıyla silinmiştir.','Tamam');", true);
                ScriptManager.RegisterStartupScript(this, this.GetType(), System.Guid.NewGuid().ToString(), "ShowMessage('Varlık ve Malzeme bağlantısı başarıyla silinmiştir.','Tamam','sil');", true);

                btnsil.Enabled = false;
                btnsil.CssClass = "btn btn-danger disabled";

                listele_varlik_malzeme();

                listele();
            }
        }

        protected void btnmalzemesil_Click(object sender, EventArgs e)
        {
            SqlCommand sorgu = new SqlCommand();
            sorgu.Connection = conn;
            conn.Open();

            sorgu.CommandText = "SELECT COUNT(VARLIK_ID) FROM MALZEMELER WHERE ID=@MID111 ";
            sorgu.Parameters.AddWithValue("@MID111", lblmalzemeidsil.Text);
            int varlik_say = Convert.ToInt16(sorgu.ExecuteScalar());

            if (lblislem_malzemesil.Text == "cihaz-sil")
            {
                lblguncellemedurumu.Text = tkod.sql_calistir_param("SELECT GUNCELLEME_DURUMU FROM MALZEMELER WHERE ID=@ID", new SqlParameter("ID", lblmalzemeidsil.Text));
                if (lblguncellemedurumu.Text == "True")
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), System.Guid.NewGuid().ToString(), "YeniMesaj('Bu malzeme daha önce sevk sürecine girmiş. Bu nedenle silinemez.','Hata');", true);
                    ScriptManager.RegisterStartupScript(this, this.GetType(), System.Guid.NewGuid().ToString(), "ShowMessage('Bu malzeme daha önce sevk sürecine girmiş. Bu nedenle silinemez.','Hata','malzemesil');", true);

                    btnsil.Enabled = false;
                    btnsil.CssClass = "btn btn-danger disabled";
                }
                if (varlik_say > 0)
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), System.Guid.NewGuid().ToString(), "YeniMesaj('Bu malzeme bir Varlık a bağlıdır. Önce Varlık bağlantısının kesilmesi gerekmektedir.','Hata');", true);
                    ScriptManager.RegisterStartupScript(this, this.GetType(), System.Guid.NewGuid().ToString(), "ShowMessage('Bu malzeme bir Varlık a bağlıdır. Önce Varlık bağlantısının kesilmesi gerekmektedir.','Hata','yeni3');", true);
                }
                else
                {
                    try
                    {
                        sorgu.CommandText = "SELECT COUNT(*) FROM VARLIK_MALZEME WHERE M_ID=@M9874_ID ";
                        sorgu.Parameters.AddWithValue("@M9874_ID", lblmalzemeidsil.Text);
                        int sayi = Convert.ToInt16(sorgu.ExecuteScalar());

                        if (sayi == 0)
                        {
                            sorgu.CommandText = "DELETE FROM MALZEMELER WHERE ID=@MID";
                            sorgu.Parameters.AddWithValue("MID", lblmalzemeidsil.Text);
                            sorgu.ExecuteNonQuery();
                            conn.Close();

                            ScriptManager.RegisterStartupScript(this, this.GetType(), System.Guid.NewGuid().ToString(), "YeniMesaj('Malzeme başarıyla silinmiştir.','Tamam');", true);
                            ScriptManager.RegisterStartupScript(this, this.GetType(), System.Guid.NewGuid().ToString(), "ShowMessage('Malzeme başarıyla silinmiştir.','Tamam','malzemesil');", true);
                        }
                    
                        else
                        {
                            tkod.mesaj("Hata", "Bu malzeme daha önce bir Varlık a bağlanmış. Bu nedenle silinemez sadece durumu değiştirilebilir.", this);
                            ScriptManager.RegisterStartupScript(this, this.GetType(), System.Guid.NewGuid().ToString(), "ShowMessage('Bu malzeme daha önce bir Varlık a bağlanmış. Bu nedenle silinemez sadece durumu değiştirilebilir.','Hata','malzemesil');", true);
                        }
                    }
                    catch (Exception ex)
                    {
                        tkod.mesaj("Hata", "Hata oluştu!", this);
                        ScriptManager.RegisterStartupScript(this, this.GetType(), System.Guid.NewGuid().ToString(), "ShowMessage('Hata oluştu!','Hata','malzemesil');", true);
                    }
                    conn.Close();

                    btnmalzemesil.Enabled = false;
                    btnmalzemesil.CssClass = "btn btn-danger disabled";

                    listele();
                }
            }
        }

        protected void combolge_SelectedIndexChanged(object sender, EventArgs e)
        {
            Malzeme_Depo_Listele();
        }

        protected void grid_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowIndex > -1)
            {
                Button butonguncelle = (Button)e.Row.FindControl("btnguncelle");
                Button butonsil = (Button)e.Row.FindControl("btnsil");
                Button butonislemler = (Button)e.Row.FindControl("islemler");

                butonislemler.Visible = true;  //Özel yetki
                butonguncelle.Visible = true;  //Özel yetki

                gd = Convert.ToBoolean(tkod.sql_calistir1(" SELECT ISNULL(GUNCELLEME_DURUMU,'FALSE')  FROM MALZEMELER WHERE ID=" + e.Row.Cells[0].Text));

                if (gd == true)
                {
                    butonguncelle.Visible = false;
                    butonsil.Visible = false;
                }
                else
                {
                    if (e.Row.Cells[6].Text.IndexOf("YENİ") > -1)
                    {
                        butonsil.Visible = true;  //Özel yetki
                    }
                }

                //////////////////////////
                try
                {
                    if (e.Row.Cells[6].Text.IndexOf("ARIZALI") > -1)
                    {
                        //e.Row.Cells[6].ForeColor = Color.SteelBlue;
                        e.Row.Cells[6].ToolTip = "Arızalı malzeme.";
                    }
                }
                catch (Exception ex)
                {
                    e.Row.Cells[6].ToolTip = "Hata oluştu : " + ex.Message;
                }
                //////////////////////////
                //////////////////////////
                try
                {
                    if (e.Row.Cells[8].Text == "KSAVİD TAMİR DEPO")
                    {
                        //e.Row.Cells[8].BackColor = Color.Tomato;
                        e.Row.Cells[8].ToolTip = "Bu malzemenin Tamir Depo'da işlemi devam etmektedir.";
                    }
                    if (e.Row.Cells[8].Text == "TRON TAMİR DEPO")
                    {
                        //e.Row.Cells[8].BackColor = Color.Salmon;
                        e.Row.Cells[8].ToolTip = "Bu malzemenin Tamir Depo'da işlemi devam etmektedir.";
                    }
                }
                catch (Exception ex)
                {
                    e.Row.Cells[6].ToolTip = "Hata oluştu : " + ex.Message;
                }
            }
        }

        // ----- DURUM DEĞİŞTİR ----- //

        void Durum_Degistir_Listele()
        {
            comyenidurum_durum.Items.Clear();
            comyenidurum_durum.Items.Insert(0, new ListItem("Yeni Durum Seçiniz", "0"));
            comyenidurum_durum.AppendDataBoundItems = true;

            SqlDataAdapter da = new SqlDataAdapter();
            SqlCommand cmd = new SqlCommand("Select DEGER AS ID,SECENEK FROM COMDOLDUR WHERE COM_ADI = 'commalzemedurum' ORDER BY SIRA  ");
            DataTable dt = new DataTable();
            da.SelectCommand = cmd;
            da.SelectCommand.Connection = conn;
            da.Fill(dt);
            comyenidurum_durum.DataSource = dt;
            comyenidurum_durum.DataTextField = "SECENEK";
            comyenidurum_durum.DataValueField = "ID";
            comyenidurum_durum.DataBind();
        }

        protected void btndurumdegistir_durum_Click(object sender, EventArgs e)
        {
            if (comyenidurum_durum.SelectedItem.ToString() == lblmevcutdurum_durum.Text)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), System.Guid.NewGuid().ToString(), "YeniMesaj('Mevcut durumla Yeni durumu aynı seçilmemeli.','Hata');", true);
                ScriptManager.RegisterStartupScript(this, this.GetType(), System.Guid.NewGuid().ToString(), "ShowMessage('Mevcut durumla Yeni durumu aynı seçilmemeli.','Hata','yeni3');", true);
            }
            else
                if (comyenidurum_durum.SelectedIndex < 0 || txtgerekce_durum.Text == "")
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), System.Guid.NewGuid().ToString(), "YeniMesaj('Durum bilgisi ve gerekçe alanları boş bırakılamaz.','Hata');", true);
                ScriptManager.RegisterStartupScript(this, this.GetType(), System.Guid.NewGuid().ToString(), "ShowMessage('Durum bilgisi ve gerekçe alanları boş bırakılamaz.','Hata','yeni3');", true);
            }
            else
            {
                try
                {
                    lblsevkdurumkontrol_durum.Text = tkod.sql_calistir_param("SELECT ISNULL(SEVKDURUMU, 'FALSE') FROM MALZEMELER WHERE ID=@ID", new SqlParameter("ID", lblidcihaz_islemler.Text));
                    lblebolge_durum.Text = tkod.sql_calistir_param("SELECT BOLGE_ID FROM MALZEMELER WHERE ID=@ID", new SqlParameter("ID", lblidcihaz_islemler.Text));
                    lbledepo_durum.Text = tkod.sql_calistir_param("SELECT DEPO_ID FROM MALZEMELER WHERE ID=@ID", new SqlParameter("ID", lblidcihaz_islemler.Text));
                    lbltamirdepokontrol_durum.Text = tkod.sql_calistir_param("SELECT ISNULL(TAMIRDEPO_KONTROL, 'FALSE') FROM MALZEMELER WHERE ID=@ID", new SqlParameter("ID", lblidcihaz_islemler.Text));
                    lblmevcutdurum_durumid.Text = tkod.sql_calistir_param("SELECT ID FROM MALZEME_DURUM WHERE DURUM=@MDD", new SqlParameter("MDD", lblmevcutdurum_durum.Text));

                    SqlCommand sorgu = new SqlCommand();
                    sorgu.Connection = conn;

                    SqlCommand sorgu1 = new SqlCommand();
                    sorgu1.Connection = conn;
                    conn.Open();

                    sorgu.CommandText = "SELECT COUNT(VARLIK_ID) FROM MALZEMELER WHERE ID=@MID ";
                    sorgu.Parameters.AddWithValue("@MID", lblidcihaz_islemler.Text);
                    int sayi = Convert.ToInt16(sorgu.ExecuteScalar());

                    if (lblsevkdurumkontrol_durum.Text != "False")
                    {
                        ScriptManager.RegisterStartupScript(this, this.GetType(), System.Guid.NewGuid().ToString(), "YeniMesaj('Bu malzeme sevk durumunda. Sevk Durumu tamamlanmadan durumu değiştirilemez.','Hata');", true);
                        ScriptManager.RegisterStartupScript(this, this.GetType(), System.Guid.NewGuid().ToString(), "ShowMessage('Bu malzeme sevk durumunda. Sevk Durumu tamamlanmadan durumu değiştirilemez.','Hata','yeni3');", true);
                    }
                    else
                    {
                        if (lbltamirdepokontrol_durum.Text != "False")
                        {
                            ScriptManager.RegisterStartupScript(this, this.GetType(), System.Guid.NewGuid().ToString(), "YeniMesaj('Bu malzeme Tamir Deposunda. Bu nedenle durumu değiştirilemez.','Hata');", true);
                            ScriptManager.RegisterStartupScript(this, this.GetType(), System.Guid.NewGuid().ToString(), "ShowMessage('Bu malzeme Tamir Deposunda. Bu nedenle durumu değiştirilemez.','Hata','yeni3');", true);
                        }
                        else
                        {
                            if (sayi > 0)
                            {
                                ScriptManager.RegisterStartupScript(this, this.GetType(), System.Guid.NewGuid().ToString(), "YeniMesaj('Bu malzeme bir Varlık a bağlıdır. Önce Varlık bağlantısının kesilmesi gerekmektedir.','Hata');", true);
                                ScriptManager.RegisterStartupScript(this, this.GetType(), System.Guid.NewGuid().ToString(), "ShowMessage('Bu malzeme bir Varlık a bağlıdır. Önce Varlık bağlantısının kesilmesi gerekmektedir.','Hata','yeni3');", true);
                            }
                            else
                            {
                                sorgu.CommandText = "UPDATE MALZEMELER SET DURUM=@DRM  WHERE ID=@MLZMID ";
                                sorgu.Parameters.AddWithValue("@DRM", comyenidurum_durum.SelectedValue);
                                sorgu.Parameters.AddWithValue("@MLZMID", lblidcihaz_islemler.Text);
                                sorgu.ExecuteNonQuery();

                                // ------   ***   ------ \\
                                // ------   LOG   ------ \\
                                // ------   ***   ------ \\
                                string userid = Session["KULLANICI_ID"].ToString();
                                sorgu1.CommandText = "INSERT INTO    MALZEMELER_LOG (M_ID, SEVK_ID, E_BOLGE, E_DEPO, Y_BOLGE, Y_DEPO, ACIKLAMA, E_DURUM, Y_DURUM, GEREKCE, KAYIT_EDEN, KAYIT_TARIHI) " +
                                    " VALUES(@MID, @SID, @EBOLGE, @EDEPO, @YBOLGE, @YDEPO, @ACIKLAMA, @EDURUM, @YDURUM, @GRKC, @UI, getdate() ) ";
                                sorgu1.Parameters.AddWithValue("@MID", lblidcihaz_islemler.Text);
                                sorgu1.Parameters.AddWithValue("@SID", "0");
                                sorgu1.Parameters.AddWithValue("@EBOLGE", lblebolge_durum.Text);
                                sorgu1.Parameters.AddWithValue("@EDEPO", lbledepo_durum.Text);
                                sorgu1.Parameters.AddWithValue("@YBOLGE", "0");
                                sorgu1.Parameters.AddWithValue("@YDEPO", "0");
                                sorgu1.Parameters.AddWithValue("@ACIKLAMA", "Durum Değişikliği yapıldı");
                                sorgu1.Parameters.AddWithValue("@EDURUM", lblmevcutdurum_durumid.Text);
                                sorgu1.Parameters.AddWithValue("@YDURUM", comyenidurum_durum.SelectedValue);
                                sorgu1.Parameters.AddWithValue("@GRKC", txtgerekce_durum.Text);
                                sorgu1.Parameters.AddWithValue("@UI", userid);
                                sorgu1.ExecuteNonQuery();
                                // ------   ***   ------ \\
                                // ------   LOG   ------ \\
                                // ------   ***   ------ \\

                                ScriptManager.RegisterStartupScript(this, this.GetType(), System.Guid.NewGuid().ToString(), "YeniMesaj('Malzeme Durumu başarıyla güncellenmiştir.','Tamam');", true);
                                ScriptManager.RegisterStartupScript(this, this.GetType(), System.Guid.NewGuid().ToString(), "ShowMessage('Malzeme Durumu başarıyla güncellenmiştir.','Tamam','yeni3');", true);
                                btndurumdegistir_durum.Enabled = false;
                                btndurumdegistir_durum.CssClass = "btn btn-success disabled";
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), System.Guid.NewGuid().ToString(), "YeniMesaj('Beklenmeyen bir hata oluştu.','Hata');", true);
                    ScriptManager.RegisterStartupScript(this, this.GetType(), System.Guid.NewGuid().ToString(), "ShowMessage('Hata oluştu :'" + ex.Message.Replace("'", "") + ",'Hata','yeni3');", true);
                }

                if (ConnectionState.Open == conn.State)
                    conn.Close();
                listele();
            }
        }

        void malzeme_log_listele()
        {
            SqlCommand cmd;
            string sql = "", sql1 = "";

            sql = " SELECT MLOG.ID, MLOG.M_ID, MLOG.SEVK_ID, MLOG.E_SERINO, MLOG.Y_SERINO, " +
                " BOLGE.BOLGE_ADI, DEPO.DEPO, " +
                " YBOLGE.BOLGE_ADI AS 'YBOLGE_ADI', " +
                " YDEPO.DEPO AS 'YDEPO', " +
                " MLOG.ACIKLAMA," +

                " E_MALZEME_DURUM.DURUM AS 'E_DURUM'," +
                " Y_MALZEME_DURUM.DURUM AS 'Y_DURUM'," +

                " MLOG.GEREKCE, KULLANICI.ISIM + ' ' +CONVERT(NVARCHAR(10),MLOG.KAYIT_TARIHI,104) AS KAYIT, " +
                " MALZEME_TURU.TURU, MALZEME_TIP.TIP, " +
                " YMALZEME_TURU.TURU AS YTURU, " +
                " YMALZEME_TIP.TIP AS YTIP," +
                " MALZEME_MARKAMODEL.MARKA, MALZEME_MODEL.MODEL, " +
                " YMALZEME_MARKAMODEL.MARKA AS YMARKA, " +
                " YMALZEME_MODEL.MODEL AS YMODEL " +

                " FROM MALZEMELER_LOG AS MLOG " +

                " LEFT JOIN MALZEME_DURUM AS E_MALZEME_DURUM ON E_MALZEME_DURUM.ID=MLOG.E_DURUM " +
                " LEFT JOIN MALZEME_DURUM AS Y_MALZEME_DURUM ON Y_MALZEME_DURUM.ID=MLOG.Y_DURUM" +

                " LEFT JOIN BOLGE AS YBOLGE ON YBOLGE.ID=MLOG.Y_BOLGE" +
                " LEFT JOIN DEPO AS YDEPO ON YDEPO.ID=MLOG.Y_DEPO" +

                " LEFT JOIN BOLGE ON BOLGE.ID=MLOG.E_BOLGE" +
                " LEFT JOIN DEPO ON DEPO.ID=MLOG.E_DEPO " +
                " LEFT JOIN MALZEME_TURU ON MALZEME_TURU.ID=MLOG.E_TUR " +
                " LEFT JOIN MALZEME_TIP ON MALZEME_TIP.ID=MLOG.E_TIP " +
                " LEFT JOIN MALZEME_TURU AS YMALZEME_TURU ON YMALZEME_TURU.ID=MLOG.Y_TUR " +
                " LEFT JOIN MALZEME_TIP AS YMALZEME_TIP ON YMALZEME_TIP.ID=MLOG.Y_TIP " +
                " LEFT JOIN MALZEME_MARKAMODEL ON MALZEME_MARKAMODEL.ID=MLOG.E_MARKA " +
                " LEFT JOIN MALZEME_MODEL ON MALZEME_MODEL.ID=MLOG.E_MODEL " +
                " LEFT JOIN MALZEME_MARKAMODEL AS YMALZEME_MARKAMODEL ON YMALZEME_MARKAMODEL.ID=MLOG.Y_MARKA " +
                " LEFT JOIN MALZEME_MODEL AS YMALZEME_MODEL ON YMALZEME_MODEL.ID=MLOG.Y_MODEL" +
                " LEFT JOIN KULLANICI ON KULLANICI.ID=MLOG.KAYIT_EDEN WHERE MLOG.M_ID = '" + lblidcihaz_islemler.Text + "'  ";

            sql = sql + sql1 + "   ORDER BY MLOG.ID DESC";

            cmd = new SqlCommand(sql, conn);

            conn.Open();

            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            grid_malzeme_log.DataSource = dt;
            grid_malzeme_log.DataBind();
            conn.Close();
            lblhareketsayisi.Text = grid_malzeme_log.Rows.Count.ToString() + " " + "adet kayıt bulunmuştur.";
        }

        protected void grid_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            grid.PageIndex = e.NewPageIndex;
            listele();
        }

        // Arıza Durum Listele
        void listele_ariza()
        {
            SqlCommand cmd;
            string sql = "", sql1 = "";

            sql = " SELECT MLOG.ID, MLOG.M_ID, MLOG.SEVK_ID, MLOG.E_SERINO, MLOG.Y_SERINO, " +
                " BOLGE.BOLGE_ADI, DEPO.DEPO, " +
                " YBOLGE.BOLGE_ADI AS 'YBOLGE_ADI', " +
                " YDEPO.DEPO AS 'YDEPO', " +
                " MLOG.ACIKLAMA," +

                " E_MALZEME_DURUM.DURUM AS 'E_DURUM'," +
                " Y_MALZEME_DURUM.DURUM AS 'Y_DURUM'," +

                " MLOG.GEREKCE, KULLANICI.ISIM + ' ' +CONVERT(NVARCHAR(10),MLOG.KAYIT_TARIHI,104) AS KAYIT, " +
                " MALZEME_TURU.TURU, MALZEME_TIP.TIP, " +
                " YMALZEME_TURU.TURU AS YTURU, " +
                " YMALZEME_TIP.TIP AS YTIP," +
                " MALZEME_MARKAMODEL.MARKA, MALZEME_MODEL.MODEL, " +
                " YMALZEME_MARKAMODEL.MARKA AS YMARKA, " +
                " YMALZEME_MODEL.MODEL AS YMODEL " +

                " FROM MALZEMELER_LOG AS MLOG " +

                " LEFT JOIN MALZEME_DURUM AS E_MALZEME_DURUM ON E_MALZEME_DURUM.ID=MLOG.E_DURUM " +
                " LEFT JOIN MALZEME_DURUM AS Y_MALZEME_DURUM ON Y_MALZEME_DURUM.ID=MLOG.Y_DURUM" +

                " LEFT JOIN BOLGE AS YBOLGE ON YBOLGE.ID=MLOG.Y_BOLGE" +
                " LEFT JOIN DEPO AS YDEPO ON YDEPO.ID=MLOG.Y_DEPO" +

                " LEFT JOIN BOLGE ON BOLGE.ID=MLOG.E_BOLGE" +
                " LEFT JOIN DEPO ON DEPO.ID=MLOG.E_DEPO " +
                " LEFT JOIN MALZEME_TURU ON MALZEME_TURU.ID=MLOG.E_TUR " +
                " LEFT JOIN MALZEME_TIP ON MALZEME_TIP.ID=MLOG.E_TIP " +
                " LEFT JOIN MALZEME_TURU AS YMALZEME_TURU ON YMALZEME_TURU.ID=MLOG.Y_TUR " +
                " LEFT JOIN MALZEME_TIP AS YMALZEME_TIP ON YMALZEME_TIP.ID=MLOG.Y_TIP " +
                " LEFT JOIN MALZEME_MARKAMODEL ON MALZEME_MARKAMODEL.ID=MLOG.E_MARKA " +
                " LEFT JOIN MALZEME_MODEL ON MALZEME_MODEL.ID=MLOG.E_MODEL " +
                " LEFT JOIN MALZEME_MARKAMODEL AS YMALZEME_MARKAMODEL ON YMALZEME_MARKAMODEL.ID=MLOG.Y_MARKA " +
                " LEFT JOIN MALZEME_MODEL AS YMALZEME_MODEL ON YMALZEME_MODEL.ID=MLOG.Y_MODEL" +
                " LEFT JOIN KULLANICI ON KULLANICI.ID=MLOG.KAYIT_EDEN WHERE MLOG.M_ID = '" + lblidcihaz_islemler.Text + "' AND   " +
                "  ( E_DURUM<>0 AND Y_DURUM<>0 AND E_DURUM IS NOT NULL AND Y_DURUM IS NOT NULL AND E_DURUM<>Y_DURUM)     ";

            sql = sql + sql1 + "   ORDER BY MLOG.ID DESC";

            cmd = new SqlCommand(sql, conn);

            conn.Open();

            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            grid_ariza.DataSource = dt;
            grid_ariza.DataBind();
            conn.Close();
            lblhareketsayisi_ariza.Text = grid_ariza.Rows.Count.ToString() + " " + "adet kayıt bulunmuştur.";
        }

        //Varlık İşlemleri
        void lokasyon_doldur()
        {
            conn.Open();
            comlokasyon_kayit.Items.Clear();
            comlokasyon_kayit.Items.Insert(0, new ListItem("-", "0"));
            comlokasyon_kayit.AppendDataBoundItems = true;
            string sql1 = "";

            SqlDataAdapter da = new SqlDataAdapter();
            SqlCommand sorgu = new SqlCommand("Select ID,V_LOKASYON FROM VARLIK_LOKASYON ORDER BY ID ");
            DataTable dt = new DataTable();
            da.SelectCommand = sorgu;
            da.SelectCommand.Connection = conn;
            da.Fill(dt);
            comlokasyon_kayit.DataSource = dt;
            comlokasyon_kayit.DataBind();
            conn.Close();
        }

        void tip_doldur()
        {
            comtip_kayit.Items.Clear();
            comtip_kayit.Items.Insert(0, new ListItem("Seçiniz", "0"));
            comtip_kayit.AppendDataBoundItems = true;
            string sql2 = "";

            if (comlokasyon_kayit.SelectedIndex > 0)
                sql2 = " AND V_LOKASYON_ID='" + comlokasyon_kayit.SelectedValue.ToString() + "' ";

            string sql = "SELECT ID,V_TIP FROM VARLIK_TIP WHERE ID>0 " + sql2 + " ORDER BY ID";
            comtip_kayit.DataSource = tkod.GetData(sql);
            comtip_kayit.DataBind();
        }

        void ad_doldur()
        {
            comvarlikadi_kayit.Items.Clear();
            comvarlikadi_kayit.Items.Insert(0, new ListItem("Seçiniz", "0"));
            comvarlikadi_kayit.AppendDataBoundItems = true;
            string sql2 = "";

            if (comtip_kayit.SelectedIndex > 0)
                sql2 = " AND V_TIP_ID='" + comtip_kayit.SelectedValue.ToString() + "' ";

            string sql = "SELECT ID,V_ADI FROM VARLIK_ADI WHERE ID>0 " + sql2 + " ORDER BY ID";
            comvarlikadi_kayit.DataSource = tkod.GetData(sql);
            comvarlikadi_kayit.DataBind();
        }

        void kod_doldur()
        {
            comvarlikkod_kayit.Items.Clear();
            comvarlikkod_kayit.Items.Insert(0, new ListItem("Seçiniz", "0"));
            comvarlikkod_kayit.AppendDataBoundItems = true;
            string sql1 = "";
            string sql2 = "";
            string sql3 = "";

            if (comvarlikadi_kayit.SelectedIndex > 0)
                sql1 = " INNER JOIN MALZEMELER AS M ON M.BOLGE_ID=VARLIK.V_BOLGE_ID ";
                sql2 = " AND V_ADI_ID='" + comvarlikadi_kayit.SelectedValue.ToString() + "' ";
                sql3 = " AND M.ID='" + lblidcihaz_islemler.Text + "' ";

            string sql = "SELECT VARLIK.ID,V_KODU FROM VARLIK " + sql1 + " WHERE VARLIK.ID>0 " + sql2 + sql3 + " ORDER BY ID";
            comvarlikkod_kayit.DataSource = tkod.GetData(sql);
            comvarlikkod_kayit.DataBind();
            lblvarlikid.Text = comvarlikkod_kayit.SelectedValue;
        }

        protected void comlokasyon_kayit_SelectedIndexChanged(object sender, EventArgs e)
        {
            tip_doldur();
            comtip_kayit.Enabled = true;
        }

        protected void comtip_kayit_SelectedIndexChanged(object sender, EventArgs e)
        {
            ad_doldur();
            comvarlikadi_kayit.Enabled = true;
        }

        protected void comvarlikadi_kayit_SelectedIndexChanged(object sender, EventArgs e)
        {
            kod_doldur();
            comvarlikkod_kayit.Enabled = true;
        }

        protected void comvarlikkod_kayit_SelectedIndexChanged(object sender, EventArgs e)
        {
            lblvarlikid.Text = comvarlikkod_kayit.SelectedValue;
        }

        protected void btnvarlikeslestir_Click(object sender, EventArgs e)
        {
            try
            {
                SqlCommand sorgu = new SqlCommand();
                sorgu.Connection = conn;
                SqlCommand sorgu1 = new SqlCommand();
                sorgu1.Connection = conn;
                conn.Open();

                lblsevkdurumkontrol_durum.Text = tkod.sql_calistir_param("SELECT ISNULL(SEVKDURUMU,0) FROM MALZEMELER WHERE ID=@ID", new SqlParameter("ID", lblidcihaz_islemler.Text));

                sorgu.CommandText = "SELECT ISNULL(VARLIK_ID,0) FROM MALZEMELER WHERE ID=@ID ";
                sorgu.Parameters.AddWithValue("@ID", lblidcihaz_islemler.Text);
                int varlikdurumkontrol1 = Convert.ToInt16(sorgu.ExecuteScalar());

                lbltamirdepokontrol_durum.Text = tkod.sql_calistir_param("SELECT ISNULL(TAMIRDEPO_KONTROL, 'FALSE') FROM MALZEMELER WHERE ID=@ID", new SqlParameter("ID", lblidcihaz_islemler.Text));

                sorgu.CommandText = "SELECT ISNULL(V_DURUM, 0) FROM VARLIK WHERE ID =@VID ";
                sorgu.Parameters.AddWithValue("@VID", lblvarlikid.Text);
                int varlikdurum = Convert.ToInt16(sorgu.ExecuteScalar());

                if (varlikdurum == 0)
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), System.Guid.NewGuid().ToString(), "YeniMesaj('Varlık durum pasif, bu nedenle bağlantı yapılamaz.','Hata');", true);
                    ScriptManager.RegisterStartupScript(this, this.GetType(), System.Guid.NewGuid().ToString(), "ShowMessage('Varlık durum pasif, bu nedenle bağlantı yapılamaz.','Hata','yeni3');", true);
                }
                else
                    if (lblsevkdurumkontrol_durum.Text != "False")
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), System.Guid.NewGuid().ToString(), "YeniMesaj('Bu malzeme sevk durumunda. Sevk Durumu tamamlanmadan varlık bağlantısı yapılamaz.','Hata');", true);
                    ScriptManager.RegisterStartupScript(this, this.GetType(), System.Guid.NewGuid().ToString(), "ShowMessage('Bu malzeme sevk durumunda. Sevk Durumu tamamlanmadan varlık bağlantısı yapılamaz.','Hata','yeni3');", true);
                }
                else
                {
                    if (lbltamirdepokontrol_durum.Text != "False")
                    {
                        ScriptManager.RegisterStartupScript(this, this.GetType(), System.Guid.NewGuid().ToString(), "YeniMesaj('Bu malzeme Tamir Deposunda. Bu nedenle varlık bağlantısı yapılamaz.','Hata');", true);
                        ScriptManager.RegisterStartupScript(this, this.GetType(), System.Guid.NewGuid().ToString(), "ShowMessage('Bu malzeme Tamir Deposunda. Bu nedenle varlık bağlantısı yapılamaz.','Hata','yeni3');", true);
                    }
                    else
                    {
                        if (varlikdurumkontrol1 > 0)
                        {
                            ScriptManager.RegisterStartupScript(this, this.GetType(), System.Guid.NewGuid().ToString(), "YeniMesaj('Bu malzeme zaten bir Varlık a bağlıdır.','Hata');", true);
                            ScriptManager.RegisterStartupScript(this, this.GetType(), System.Guid.NewGuid().ToString(), "ShowMessage('Bu malzeme bir Varlık a bağlıdır.','Hata','yeni3');", true);
                        }
                        else
                        {
                            sorgu.CommandText = "UPDATE MALZEMELER SET VARLIK_ID=@VID10, DURUM=@DRM, GUNCELLEME_DURUMU=@GUNCELLEME_DURUMU_K  WHERE ID=@M_ID ";
                            sorgu.Parameters.AddWithValue("@VID10", lblvarlikid.Text);
                            sorgu.Parameters.AddWithValue("@M_ID", lblidcihaz_islemler.Text);
                            sorgu.Parameters.AddWithValue("@GUNCELLEME_DURUMU_K", true);    ///// SONRADAN YAPILDI DİĞER MODULLERE ETKİSİNİ DETAYLI İNCELE
                            sorgu.Parameters.AddWithValue("@DRM", 5); //Kullanımda
                            sorgu.ExecuteNonQuery();

                            sorgu.CommandText = "UPDATE VARLIK SET V_M_BAGLANTI_DURUMU=@VMBD  WHERE ID=@VID_U ";
                            sorgu.Parameters.AddWithValue("@VMBD", "1");
                            sorgu.Parameters.AddWithValue("@VID_U", lblvarlikid.Text);
                            sorgu.ExecuteNonQuery();

                            string userid = Session["KULLANICI_ID"].ToString();
                            sorgu.CommandText = "INSERT INTO    VARLIK_MALZEME (V_ID, M_ID, V_M_BAGLANTI_DURUMU, V_M_BAGLANTI_TARIHI, KAYIT_EDEN , KAYIT_TARIHI) " +
                                " VALUES(@VID1, @MID1, @BD1, @BT1, @UI, getdate() )";
                            sorgu.Parameters.AddWithValue("@VID1", lblvarlikid.Text);
                            sorgu.Parameters.AddWithValue("@MID1", lblidcihaz_islemler.Text);
                            sorgu.Parameters.AddWithValue("@BD1", "1");
                            sorgu.Parameters.AddWithValue("@BT1", Convert.ToDateTime(DateTime.Now.ToString()));
                            sorgu.Parameters.AddWithValue("@UI", userid);
                            sorgu.ExecuteNonQuery();

                            // ------   ***   ------ \\
                            // ------   LOG   ------ \\
                            // ------   ***   ------ \\
                            sorgu1.CommandText = "INSERT INTO    VARLIK_LOG (V_ID, M_ID, LOG_ACIKLAMA, KAYIT_EDEN, KAYIT_TARIHI) " +
                                " VALUES(@VID2, @MAID, @L_ACKLM, @UI1, getdate() ) ";
                            sorgu1.Parameters.AddWithValue("@VID2", lblvarlikid.Text);
                            sorgu1.Parameters.AddWithValue("@MAID", lblidcihaz_islemler.Text);
                            sorgu1.Parameters.AddWithValue("@L_ACKLM", "Varlık - Malzeme Eşleşme kaydı yapıldı.");
                            sorgu1.Parameters.AddWithValue("@UI1", userid);
                            sorgu1.ExecuteNonQuery();
                            // ------   ***   ------ \\
                            // ------   LOG   ------ \\
                            // ------   ***   ------ \\

                            // ------   ***   ------ \\
                            // ------   LOG   ------ \\
                            // ------   ***   ------ \\
                            sorgu.CommandText = "INSERT INTO    MALZEMELER_LOG (M_ID, GEREKCE, ACIKLAMA, Y_DURUM, KAYIT_EDEN, KAYIT_TARIHI) " +
                                " VALUES(@MID, @GRKC, @ACIKLAMA, @YDRM, @UI2, getdate() ) ";
                            sorgu.Parameters.AddWithValue("@MID", lblidcihaz_islemler.Text);
                            sorgu.Parameters.AddWithValue("@GRKC", "Malzeme bir varlık'a bağlandı.");
                            sorgu.Parameters.AddWithValue("@ACIKLAMA", "Durum Değişikliği yapıldı, Malzeme Varlık bağlantısı yapıldı.");
                            sorgu.Parameters.AddWithValue("@YDRM", 5);
                            sorgu.Parameters.AddWithValue("@UI2", userid);
                            sorgu.ExecuteNonQuery();
                            // ------   ***   ------ \\
                            // ------   LOG   ------ \\
                            // ------   ***   ------ \\

                            ScriptManager.RegisterStartupScript(this, this.GetType(), System.Guid.NewGuid().ToString(), "YeniMesaj('Varlık - Malzeme Eşleşmesi başarıyla kaydedildi.','Tamam');", true);
                            ScriptManager.RegisterStartupScript(this, this.GetType(), System.Guid.NewGuid().ToString(), "ShowMessage('Varlık - Malzeme Eşleşmesi başarıyla kaydedildi.','Tamam','yeni3');", true);
                            btndurumdegistir_durum.Enabled = false;
                            btndurumdegistir_durum.CssClass = "btn btn-success disabled";
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), System.Guid.NewGuid().ToString(), "YeniMesaj('Beklenmeyen bir hata oluştu.','Hata');", true);
                ScriptManager.RegisterStartupScript(this, this.GetType(), System.Guid.NewGuid().ToString(), "ShowMessage('Hata oluştu :'" + ex.Message.Replace("'", "") + ",'Hata','yeni3');", true);
            }

            if (ConnectionState.Open == conn.State)
                conn.Close();
            listele_varlik_malzeme();
            listele();
        }

        void listele_varlik_malzeme()
        {
            SqlCommand cmd;
            string sql = "", sql1 = "";

            sql = "SELECT        VM.ID, VM.V_ID, VM.M_ID, " +
                " VARLIK.V_KODU, MALZEMELER.SERI_NO, MMM.MARKA, MM.MODEL, " +
                " CASE WHEN ISNULL(VM.V_M_BAGLANTI_DURUMU, 0)=1 THEN 'Aktif' ELSE 'Pasif' END AS V_M_BAGLANTI_DURUMU, " +
                " VM.V_M_BAGLANTI_TARIHI, VM.V_M_AYRILMA_TARIHI, KULLANICI.ISIM + ' ' +CONVERT(NVARCHAR(10),VM.KAYIT_TARIHI,104) AS KAYIT" +
                " FROM VARLIK_MALZEME AS VM " +
                " LEFT JOIN MALZEMELER ON MALZEMELER.ID = VM.M_ID" +
                " LEFT JOIN MALZEME_MARKAMODEL AS MMM ON MMM.ID = MALZEMELER.MARKA" +
                " LEFT JOIN MALZEME_MODEL AS MM ON MM.ID = MALZEMELER.MODEL" +
                " LEFT JOIN VARLIK ON VARLIK.ID = VM.V_ID" +
                " LEFT JOIN KULLANICI ON KULLANICI.ID=VM.KAYIT_EDEN" +

                " WHERE VM.M_ID = '" + lblidcihaz_islemler.Text + "' AND  VM.V_M_BAGLANTI_DURUMU = 'True'  ";

            sql = sql + sql1;

            cmd = new SqlCommand(sql, conn);

            conn.Open();

            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            grid_varlik_malzeme_baglama.DataSource = dt;
            grid_varlik_malzeme_baglama.DataBind();
            conn.Close();
            lblmalzemesayisi_varlik_malzeme.Text = grid_varlik_malzeme_baglama.Rows.Count.ToString() + " " + "adet kayıt bulunmuştur.";
        }

        protected void grid_varlik_malzeme_baglama_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName.Equals("sil"))
            {
                int index = Convert.ToInt32(e.CommandArgument);
                lblidsil.Text = grid_varlik_malzeme_baglama.DataKeys[index].Value.ToString();
                lblislem.Text = "varlik-malzeme-sil";
                btnsil.Enabled = true;
                btnsil.CssClass = "btn btn-danger ";

                System.Text.StringBuilder sb = new System.Text.StringBuilder();
                sb.Append(@"<script type='text/javascript'>");
                sb.Append("$(\"#ModalSil\").modal(\"show\");");
                sb.Append(@"</script>");
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(),
                           "ModalScript", sb.ToString(), false);

                Varlik_Sil_Durum_Degistir_Doldur();
            }
        }

        void listele_varlik_hareket()
        {
            SqlCommand cmd;
            string sql = "", sql1 = "";

            sql = "SELECT VLOG.ID, VLOG.V_ID, VLOG.M_ID, VL.V_LOKASYON, VT.V_TIP, VA.V_ADI, VLOG.V_KODU, IL.IL, BOLGE.BOLGE_ADI, VLOG.V_DURUM, VLOG.ENLEM, VLOG.BOYLAM, VLOG.ADRES, VLOG.LOG_ACIKLAMA, " +
                " KULLANICI.ISIM + ' ' +CONVERT(NVARCHAR(10),VLOG.KAYIT_TARIHI,104) AS KAYIT" +
                " FROM VARLIK_LOG AS VLOG" +
                " LEFT JOIN VARLIK_LOKASYON AS VL ON VL.ID=VLOG.LOKASYON_ID" +
                " LEFT JOIN VARLIK_TIP AS VT ON VT.ID=VLOG.TIP_ID" +
                " LEFT JOIN VARLIK_ADI AS VA ON VA.ID=VLOG.AD_ID" +
                " LEFT JOIN IL ON IL.ID=VLOG.IL_ID" +
                " LEFT JOIN BOLGE ON BOLGE.ID=VLOG.BOLGE_ID" +
                " LEFT JOIN KULLANICI ON KULLANICI.ID=VLOG.KAYIT_EDEN WHERE VLOG.M_ID = '" + lblidcihaz_islemler.Text + "' ";

            sql = sql + sql1 + "   ORDER BY VLOG.ID DESC";

            cmd = new SqlCommand(sql, conn);

            conn.Open();

            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            grid_varlik_hareket.DataSource = dt;
            grid_varlik_hareket.DataBind();
            conn.Close();
            lblhareketsayisi_varlikhareket.Text = grid_varlik_hareket.Rows.Count.ToString() + " " + "adet kayıt bulunmuştur.";
        }

        protected void btnhareketgor_Click(object sender, EventArgs e)
        {
            listele_varlik_hareket();
        }

        void Varlik_Sil_Durum_Degistir_Doldur()
        {
            comyenidurumsec_varlikbaglantisil.Items.Clear();
            comyenidurumsec_varlikbaglantisil.Items.Insert(0, new ListItem("Yeni Durum Seçiniz", "0"));
            comyenidurumsec_varlikbaglantisil.AppendDataBoundItems = true;

            SqlDataAdapter da = new SqlDataAdapter();
            SqlCommand cmd = new SqlCommand("Select DEGER AS ID,SECENEK FROM COMDOLDUR WHERE COM_ADI = 'commalzemedurum' ORDER BY SIRA  ");
            DataTable dt = new DataTable();
            da.SelectCommand = cmd;
            da.SelectCommand.Connection = conn;
            da.Fill(dt);
            comyenidurumsec_varlikbaglantisil.DataSource = dt;
            comyenidurumsec_varlikbaglantisil.DataTextField = "SECENEK";
            comyenidurumsec_varlikbaglantisil.DataValueField = "ID";
            comyenidurumsec_varlikbaglantisil.DataBind();
        }
    }
}