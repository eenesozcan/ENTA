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
    public partial class RuhsatKabul : System.Web.UI.Page
    {
        SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["KabloHE"].ConnectionString);
        string sayfa = "RuhsatKabul";
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
                listele_ruhsat();
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
        void ilce_sec()
        {
            comilce_kayit.Items.Clear();
            comilce_kayit.Items.Insert(0, new ListItem("İlçe seçiniz", "0"));
            comilce_kayit.AppendDataBoundItems = true;
            string sql2 = "";

            if (combolge_kayit.SelectedIndex > 0)
                sql2 = " AND BOLGE_ID='" + combolge_kayit.SelectedValue.ToString() + "' ";

            string sql = "Select ID,ILCE FROM RUHSAT_ADRES_ILCE WHERE ID>0 " + sql2 + " ORDER BY ID";
            comilce_kayit.DataSource = tkod.GetData(sql);
            comilce_kayit.DataBind();
        }
        void mahalle_sec()
        {
            commahalle_kayit.Items.Clear();
            commahalle_kayit.Items.Insert(0, new ListItem("Mahalle Seçiniz", "0"));
            commahalle_kayit.AppendDataBoundItems = true;
            string sql2 = "";

            if (comilce_kayit.SelectedIndex > 0)
                sql2 = " AND ILCE_ID='" + comilce_kayit.SelectedValue.ToString() + "' ";

            string sql = "Select ID,MAHALLE FROM RUHSAT_ADRES_MAHALLE WHERE ID>0 " + sql2 + " ORDER BY ID";
            commahalle_kayit.DataSource = tkod.GetData(sql);
            commahalle_kayit.DataBind();
        }
        void caddesokak_sec()
        {
            comcdsk_kayit.Items.Clear();
            comcdsk_kayit.Items.Insert(0, new ListItem("Cadde/Sokak Seçiniz", "0"));
            comcdsk_kayit.AppendDataBoundItems = true;
            string sql2 = "";

            if (commahalle_kayit.SelectedIndex > 0)
                sql2 = " AND MAHALLE_ID='" + commahalle_kayit.SelectedValue.ToString() + "' ";

            string sql = "Select ID,CADDESOKAK FROM RUHSAT_ADRES_CADDESOKAK WHERE ID>0 " + sql2 + " ORDER BY ID";
            comcdsk_kayit.DataSource = tkod.GetData(sql);
            comcdsk_kayit.DataBind();
        }
        void turksat_proje_tip_sec()
        {
            comtprojetipi_kayit.Items.Clear();
            comtprojetipi_kayit.Items.Insert(0, new ListItem("Türksat Proje Seçiniz", "0"));
            comtprojetipi_kayit.AppendDataBoundItems = true;


            string sql = "Select ID,T_PROJE_TIPI FROM RUHSAT_TURKSAT_PROJE_TIPI WHERE ID>0  ORDER BY ID";
            comtprojetipi_kayit.DataSource = tkod.GetData(sql);
            comtprojetipi_kayit.DataBind();
        }
        void aykome_proje_tip_sec()
        {
            comaprojetipi_kayit.Items.Clear();
            comaprojetipi_kayit.Items.Insert(0, new ListItem("Aykome Proje  Seçiniz", "0"));
            comaprojetipi_kayit.AppendDataBoundItems = true;


            string sql = "Select ID,A_PROJE_TIPI FROM RUHSAT_AYKOME_PROJE_TIPI WHERE ID>0  ORDER BY ID";
            comaprojetipi_kayit.DataSource = tkod.GetData(sql);
            comaprojetipi_kayit.DataBind();
        }
        void ruhsat_kazi_turu_sec()
        {
            comkazituru.Items.Clear();
            comkazituru.Items.Insert(0, new ListItem("Kazı Türü Seçiniz", "0"));
            comkazituru.AppendDataBoundItems = true;


            string sql = "Select ID,KAZI_TURU FROM RUHSAT_KAZI_TURU WHERE ID>0  ORDER BY ID";
            comkazituru.DataSource = tkod.GetData(sql);
            comkazituru.DataBind();
        }
        void ruhsat_kazi_tipi_sec()
        {
            comkazitipi_sec.Items.Clear();
            comkazitipi_sec.Items.Insert(0, new ListItem("Kazı Tipi Seçiniz", "0"));
            comkazitipi_sec.AppendDataBoundItems = true;


            string sql = "Select ID,KAZI_TIPI FROM RUHSAT_KAZI_TIPI WHERE ID>0  ORDER BY ID";
            comkazitipi_sec.DataSource = tkod.GetData(sql);
            comkazitipi_sec.DataBind();
        }

        protected void grid_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName.Equals("sil"))
            {
                int index = Convert.ToInt32(e.CommandArgument) % grid.PageSize;
                lblruhsatidsil.Text = grid.DataKeys[index].Value.ToString();
                //int index = Convert.ToInt32(e.CommandArgument);
                //lblidsil.Text = grid.DataKeys[index].Value.ToString();
                lblislem_ruhsatsil.Text = "varlik-sil";
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

                TabPanel1.Visible = true;
                TabPanel2.Visible = true;
                TabPanel3.Visible = true;
                TabPanel4.Visible = true;
                TabPanel5.Visible = true;

                listele_metraj();

                il_sec();
                bolge_sec();
                ilce_sec();
                mahalle_sec();
                caddesokak_sec();
                turksat_proje_tip_sec();
                aykome_proje_tip_sec();

                ruhsat_kazi_turu_sec();
                ruhsat_kazi_tipi_sec();

                txtruhsatno_kayit.Text = HttpUtility.HtmlDecode(grid.Rows[index].Cells[1].Text.Trim());
                txtprojeno_kayit.Text = HttpUtility.HtmlDecode(grid.Rows[index].Cells[2].Text.Trim());

                comil_kayit.SelectedIndex = comil_kayit.Items.IndexOf(comil_kayit.Items.FindByText(HttpUtility.HtmlDecode(grid.Rows[index].Cells[3].Text)));
                combolge_kayit.SelectedIndex = combolge_kayit.Items.IndexOf(combolge_kayit.Items.FindByText(HttpUtility.HtmlDecode(grid.Rows[index].Cells[4].Text)));
                comilce_kayit.SelectedIndex = comilce_kayit.Items.IndexOf(comilce_kayit.Items.FindByText(HttpUtility.HtmlDecode(grid.Rows[index].Cells[5].Text)));
                commahalle_kayit.SelectedIndex = commahalle_kayit.Items.IndexOf(commahalle_kayit.Items.FindByText(HttpUtility.HtmlDecode(grid.Rows[index].Cells[6].Text)));
                comcdsk_kayit.SelectedIndex = comcdsk_kayit.Items.IndexOf(comcdsk_kayit.Items.FindByText(HttpUtility.HtmlDecode(grid.Rows[index].Cells[7].Text)));

                txtadresdetay.Text = HttpUtility.HtmlDecode(grid.Rows[index].Cells[8].Text.Trim());

                comtprojetipi_kayit.SelectedIndex = comtprojetipi_kayit.Items.IndexOf(comtprojetipi_kayit.Items.FindByText(HttpUtility.HtmlDecode(grid.Rows[index].Cells[9].Text)));
                comaprojetipi_kayit.SelectedIndex = comaprojetipi_kayit.Items.IndexOf(comaprojetipi_kayit.Items.FindByText(HttpUtility.HtmlDecode(grid.Rows[index].Cells[10].Text)));


                txtDate_baslangic.Text = tkod.sql_calistir_param("SELECT BASLANGIC_TARIHI FROM RUHSAT_RUHSATLAR  WHERE ID=@ID", new SqlParameter("ID", id));
                txtDate_bitis.Text = tkod.sql_calistir_param("SELECT BITIS_TARIHI FROM RUHSAT_RUHSATLAR  WHERE ID=@ID1", new SqlParameter("ID1", id));
                txtaciklama.Text = tkod.sql_calistir_param("SELECT ACIKLAMA FROM RUHSAT_RUHSATLAR  WHERE ID=@ID2", new SqlParameter("ID2", id));

                //txtDate_baslangic.Text = HttpUtility.HtmlDecode(grid.Rows[index].Cells[11].Text.Trim());
                //txtDate_bitis.Text = HttpUtility.HtmlDecode(grid.Rows[index].Cells[12].Text.Trim());




                //int sayi = Convert.ToInt16(tkod.sql_calistir1("SELECT COUNT(SEVK_ID) FROM MALZEME_SEVK_MALZEMELER WHERE SEVK_ID=" + id));



                chcaktif_pasif.Checked = Convert.ToBoolean(tkod.sql_calistir_param("SELECT DURUM FROM RUHSAT_RUHSATLAR WHERE ID=@ID98", new SqlParameter("ID98", id)));

                btnruhstkaydet.Enabled = true;
                btnruhstkaydet.CssClass = "btn btn-success";

                lblmodalyenibaslik.Text = "Ruhsat Güncelleme";

                System.Text.StringBuilder sb = new System.Text.StringBuilder();
                sb.Append(@"<script type='text/javascript'>");
                sb.Append("$(\"#ModalRuhsatTalep\").modal(\"show\");");
                sb.Append(@"</script>");
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(),
                           "ModalScript", sb.ToString(), false);
            }
            if (e.CommandName.Equals("kabul"))
            {
                int index = Convert.ToInt32(e.CommandArgument) % grid.PageSize;
                lblruhsatidkabul.Text = grid.DataKeys[index].Value.ToString();

                System.Text.StringBuilder sb = new System.Text.StringBuilder();
                sb.Append(@"<script type='text/javascript'>");
                sb.Append("$(\"#ModalKabul\").modal(\"show\");");
                sb.Append(@"</script>");
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(),
                           "ModalScript", sb.ToString(), false);
            }
        }

        protected void grid_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            grid.PageIndex = e.NewPageIndex;
            listele_ruhsat();
        }

        void listele_ruhsat()
        {

            SqlCommand cmd;
            string sql = " ", sql1 = " ";


            sql = "SELECT V.ID, V.RUHSATNO, V.PROJENO, IL.IL, BOLGE.BOLGE_ADI, RAI.ILCE, RAM.MAHALLE, RACS.CADDESOKAK, V.ADRES_DETAY, RTPT.T_PROJE_TIPI, RAPT.A_PROJE_TIPI, V.BASLANGIC_TARIHI, V.BITIS_TARIHI, " +
                " CASE WHEN ISNULL(V.DURUM, 0)=1 THEN 'Aktif' ELSE 'Pasif' END AS DURUM " +
                "  FROM RUHSAT_RUHSATLAR AS V " +
                " LEFT JOIN IL ON IL.ID=V.IL_ID" +
                " LEFT JOIN BOLGE ON BOLGE.ID=V.BOLGE_ID " +

                " LEFT JOIN RUHSAT_ADRES_ILCE AS RAI ON RAI.ID=V.ILCE_ID " +
                " LEFT JOIN RUHSAT_ADRES_MAHALLE AS RAM ON RAM.ID=V.MAHALLE_ID " +
                " LEFT JOIN RUHSAT_ADRES_CADDESOKAK AS RACS ON RACS.ID=V.CADDESOKAK_ID " +
                " LEFT JOIN RUHSAT_TURKSAT_PROJE_TIPI  AS RTPT ON RTPT.ID=V.T_PROJETIP_ID " +
                " LEFT JOIN RUHSAT_AYKOME_PROJE_TIPI  AS RAPT ON RAPT.ID=V.A_PROJETIP_ID " +

                "  ";

            if (combolgeara.SelectedIndex > 0)
                sql1 += " AND (V.BOLGE_ID='" + combolgeara.SelectedValue + "' )  ";

            sql = sql + tkod.yetki_tablosu() + "  WHERE V.ID > 0  " + sql1 + tkod.yetki_tablosu_2() + " and KABULMU='True'   ORDER BY ID";

            cmd = new SqlCommand(sql, conn);
            conn.Open();
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            grid.DataSource = dt;
            grid.DataBind();
            conn.Close();
            lblruhsatsayisi.Text = " " + " Bu sayfada " + grid.Rows.Count.ToString() + " " + "adet kayıt bulunmuştur.";
        }

        void listele_metraj()
        {
            if (lblidcihaz.Text != "")
            {
                SqlCommand cmd;
                string sql = " ", sql1 = " ";


                sql = "SELECT RKB.ID,RKB.UZUNLUK, RKB.GENISLIK, RKTURU.KAZI_TURU, RKTIPI.KAZI_TIPI  FROM RUHSAT_KAZI_BILGI AS RKB " +
                    " INNER JOIN RUHSAT_KAZI_TURU AS RKTURU ON RKTURU.ID = RKB.KAZI_TURU_ID " +
                    " INNER JOIN RUHSAT_KAZI_TIPI AS RKTIPI ON RKTIPI.ID = RKB.KAZI_TIPI_ID " +
                    " WHERE RKB.ID > 0 AND RKB.RUHSAT_TALEP_KABUL_ID= " + lblidcihaz.Text + " ";

                cmd = new SqlCommand(sql, conn);
                conn.Open();
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);
                grid_metraj.DataSource = dt;
                grid_metraj.DataBind();
                conn.Close();
                lblmetrajsayisi.Text = " " + " Bu sayfada " + grid_metraj.Rows.Count.ToString() + " " + "adet kayıt bulunmuştur.";

            }
            else
            {
                grid_metraj.DataBind();
            }

        }

        protected void btnruhsatekle_Click(object sender, EventArgs e)
        {
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            sb.Append(@"<script type='text/javascript'>");
            sb.Append("$(\"#ModalRuhsatTalep\").modal(\"show\");");
            sb.Append(@"</script>");
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(),
                       "ModalScript", sb.ToString(), false);

            lblmodalyenibaslik.Text = "Ruhsat Talep Kaydet";

            if (combolge_kayit.SelectedIndex > 0) ;
            combolge_kayit.Items.Clear();

            txtruhsatno_kayit.Text = "";
            txtprojeno_kayit.Text = "";
            txtDate_baslangic.Text = "";
            txtDate_bitis.Text = "";
            txtadresdetay.Text = "";
            txtaciklama.Text = "";
            txtkaziuzunluk.Text = "";
            txtkazigenislik.Text = "";

            lblidcihaz.Text = "";

            il_sec();
            turksat_proje_tip_sec();
            aykome_proje_tip_sec();
            ruhsat_kazi_turu_sec();
            ruhsat_kazi_tipi_sec();
            listele_metraj();

            TabPanel1.Visible = true;
            TabPanel2.Visible = false;
            TabPanel3.Visible = false;
            TabPanel4.Visible = false;
            TabPanel5.Visible = false;

            comil_kayit.SelectedIndex = 0;
            if (comilce_kayit.SelectedIndex > 0)
            {
                comilce_kayit.SelectedIndex = 0;
            }
            if (commahalle_kayit.SelectedIndex > 0)
            {
                commahalle_kayit.SelectedIndex = 0;
            }
            if (comcdsk_kayit.SelectedIndex > 0)
            {
                comcdsk_kayit.SelectedIndex = 0;
            }

            comtprojetipi_kayit.SelectedIndex = 0;
            comaprojetipi_kayit.SelectedIndex = 0;
            comkazituru.SelectedIndex = 0;
            comkazitipi_sec.SelectedIndex = 0;

            combolge_kayit.Enabled = false;
            comilce_kayit.Enabled = false;
            commahalle_kayit.Enabled = false;
            comcdsk_kayit.Enabled = false;
        }

        protected void btnara_Click(object sender, EventArgs e)
        {
            listele_ruhsat();
        }

        void bolge_ara()
        {
            combolgeara.Items.Clear();
            combolgeara.Items.Insert(0, new ListItem("Bölge Seçiniz", "0"));
            combolgeara.AppendDataBoundItems = true;

            combolgeara.DataSource = tkod.dt_bolge_Liste();
            combolgeara.DataBind();
        }
        protected void comil_kayit_SelectedIndexChanged(object sender, EventArgs e)
        {
            bolge_sec();
            if (comil_kayit.SelectedIndex == 0)
            {
                combolge_kayit.Enabled = false;
                comilce_kayit.Enabled = false;
                commahalle_kayit.Enabled = false;
                comcdsk_kayit.Enabled = false;
            }
            else
            {
                combolge_kayit.Enabled = true;
                comilce_kayit.Enabled = false;
                commahalle_kayit.Enabled = false;
                comcdsk_kayit.Enabled = false;
            }
        }

        protected void comilce_kayit_SelectedIndexChanged(object sender, EventArgs e)
        {
            mahalle_sec();
            if (comilce_kayit.SelectedIndex == 0)
            {
                commahalle_kayit.Enabled = false;
                comcdsk_kayit.Enabled = false;
            }
            else
            {
                commahalle_kayit.Enabled = true;
                comcdsk_kayit.Enabled = false;
            }
        }

        protected void combolge_kayit_SelectedIndexChanged(object sender, EventArgs e)
        {
            ilce_sec();
            if (combolge_kayit.SelectedIndex == 0)
            {
                comilce_kayit.Enabled = false;
                commahalle_kayit.Enabled = false;
                comcdsk_kayit.Enabled = false;
            }
            else
            {
                comilce_kayit.Enabled = true;
                commahalle_kayit.Enabled = false;
                comcdsk_kayit.Enabled = false;
            }
        }

        protected void commahalle_kayit_SelectedIndexChanged(object sender, EventArgs e)
        {
            caddesokak_sec();
            if (commahalle_kayit.SelectedIndex == 0)
            {
                comcdsk_kayit.Enabled = false;
            }
            else
            {
                comcdsk_kayit.Enabled = true;
            }
        }

        protected void btnruhstkaydet_Click(object sender, EventArgs e)
        {
            SqlCommand sorgu = new SqlCommand();
            sorgu.Connection = conn;
            conn.Open();

            string userid = Session["KULLANICI_ID"].ToString();


            //String tarih = Convert.ToString("datetimepicker1");
            //String tarih1 = Convert.ToString("txtbitis_tarihi");


            string bas = txtDate_baslangic.Text;
            string son = txtDate_bitis.Text;
            if (bas == "")
                bas = "01.01.2000";
            if (son == "")
                son = "01.01.2000";
            try
            {
                //string baslangic = Request.Form["datetimepicker1"].ToString();
                //string txtbitis_tarihi = Request.Form["txtbitis_tarihi"].ToString();

                if (lblidcihaz.Text == "")
                {
                    sorgu.CommandText = "INSERT INTO    RUHSAT_RUHSATLAR (RUHSATNO, PROJENO, IL_ID, BOLGE_ID, ILCE_ID, MAHALLE_ID, CADDESOKAK_ID, ADRES_DETAY, T_PROJETIP_ID, A_PROJETIP_ID, BASLANGIC_TARIHI, BITIS_TARIHI, ACIKLAMA, DURUM, KABUL_YAPILDIMI, KESIFMI, KAYIT_EDEN, KAYIT_TARIHI) " +
                        " VALUES(@RUHSATNO_K, @PROJENO_K, @IL_ID_K, @B_ID_K, @ILCE_ID_K, @MAHALLE_ID_K, @CDSK_ID_K, @ADRESDETAY_K, @TPROJETIP_ID_K, @APROJETIP_ID_K, @BASTARIH_K, @BITTARIH_K, @ACKLM_K, @DRM_K, @KABULYAPILDIMI, @KESIFMI, @UI_K, getdate() ); SELECT @@IDENTITY AS 'Identity' ";
                    sorgu.Parameters.AddWithValue("@RUHSATNO_K", txtruhsatno_kayit.Text);
                    sorgu.Parameters.AddWithValue("@PROJENO_K", txtprojeno_kayit.Text);
                    sorgu.Parameters.AddWithValue("@IL_ID_K", comil_kayit.SelectedValue);
                    sorgu.Parameters.AddWithValue("@B_ID_K", combolge_kayit.SelectedValue);
                    sorgu.Parameters.AddWithValue("@ILCE_ID_K", comilce_kayit.SelectedValue);
                    sorgu.Parameters.AddWithValue("@MAHALLE_ID_K", commahalle_kayit.SelectedValue);
                    sorgu.Parameters.AddWithValue("@CDSK_ID_K", comcdsk_kayit.SelectedValue);
                    sorgu.Parameters.AddWithValue("@ADRESDETAY_K", txtadresdetay.Text);
                    sorgu.Parameters.AddWithValue("@TPROJETIP_ID_K", comtprojetipi_kayit.SelectedValue);
                    sorgu.Parameters.AddWithValue("@APROJETIP_ID_K", comaprojetipi_kayit.SelectedValue);
                    sorgu.Parameters.AddWithValue("@BASTARIH_K", Convert.ToDateTime(bas));
                    sorgu.Parameters.AddWithValue("@BITTARIH_K", Convert.ToDateTime(son));
                    sorgu.Parameters.AddWithValue("@ACKLM_K", txtaciklama.Text);
                    sorgu.Parameters.AddWithValue("@DRM_K", chcaktif_pasif.Checked);
                    sorgu.Parameters.AddWithValue("@KABULYAPILDIMI", 0);
                    sorgu.Parameters.AddWithValue("@KESIFMI", 1);
                    sorgu.Parameters.AddWithValue("@UI_K", userid);
                    string id = sorgu.ExecuteScalar().ToString();
                    lblidcihaz.Text = id;

                    ScriptManager.RegisterStartupScript(this, this.GetType(), System.Guid.NewGuid().ToString(), "YeniMesaj('Ruhsat başarıyla kaydedilmiştir.','Tamam');", true);
                    ScriptManager.RegisterStartupScript(this, this.GetType(), System.Guid.NewGuid().ToString(), "ShowMessage('Ruhsat başarıyla kaydedilmiştir.','Tamam','yeni');", true);

                    btnruhstkaydet.Enabled = false;
                    btnruhstkaydet.CssClass = "btn btn-success disabled";
                }
                else
                {
                    // ------   UPDATE   ------ \\
                    sorgu.CommandText = "UPDATE RUHSAT_RUHSATLAR SET RUHSATNO=@RUHSATNO_U, PROJENO=@PROJENO_U, IL_ID=@IL_ID_U, BOLGE_ID=@B_ID_U, ILCE_ID=@ILCE_ID_U, " +
                        " MAHALLE_ID=@MAHALLE_ID_U, CADDESOKAK_ID=@CDSK_ID_U, ADRES_DETAY=@ADRESDETAY_U, T_PROJETIP_ID=@TPROJETIP_ID_U, A_PROJETIP_ID=@APROJETIP_ID_U, " +
                        " BASLANGIC_TARIHI=@BASTARIH_U, BITIS_TARIHI=@BITTARIH_U, ACIKLAMA=@ACKLM_U, DURUM=@DRM_U  " +
                        " WHERE ID=@V_ID_U ";
                    sorgu.Parameters.AddWithValue("@RUHSATNO_U", txtruhsatno_kayit.Text);
                    sorgu.Parameters.AddWithValue("@PROJENO_U", txtprojeno_kayit.Text);
                    sorgu.Parameters.AddWithValue("@IL_ID_U", comil_kayit.SelectedValue);
                    sorgu.Parameters.AddWithValue("@B_ID_U", combolge_kayit.SelectedValue);
                    sorgu.Parameters.AddWithValue("@ILCE_ID_U", comilce_kayit.SelectedValue);
                    sorgu.Parameters.AddWithValue("@MAHALLE_ID_U", commahalle_kayit.SelectedValue);
                    sorgu.Parameters.AddWithValue("@CDSK_ID_U", comcdsk_kayit.SelectedValue);
                    sorgu.Parameters.AddWithValue("@ADRESDETAY_U", txtadresdetay.Text);
                    sorgu.Parameters.AddWithValue("@TPROJETIP_ID_U", comtprojetipi_kayit.SelectedValue);
                    sorgu.Parameters.AddWithValue("@APROJETIP_ID_U", comaprojetipi_kayit.SelectedValue);
                    sorgu.Parameters.AddWithValue("@BASTARIH_U", Convert.ToDateTime(bas));
                    sorgu.Parameters.AddWithValue("@BITTARIH_U", Convert.ToDateTime(son));
                    sorgu.Parameters.AddWithValue("@ACKLM_U", txtaciklama.Text);
                    sorgu.Parameters.AddWithValue("@DRM_U", chcaktif_pasif.Checked);
                    sorgu.Parameters.AddWithValue("@V_ID_U", lblidcihaz.Text);
                    sorgu.ExecuteNonQuery();

                    // ------   UPDATE   ------ \\

                    ScriptManager.RegisterStartupScript(this, this.GetType(), System.Guid.NewGuid().ToString(), "YeniMesaj('Ruhsat başarıyla güncellenmiştir.','Tamam');", true);
                    ScriptManager.RegisterStartupScript(this, this.GetType(), System.Guid.NewGuid().ToString(), "ShowMessage('Ruhsat başarıyla güncellenmiştir.','Tamam','yeni');", true);
                    btnruhstkaydet.Enabled = false;
                    btnruhstkaydet.CssClass = "btn btn-success disabled";




                }
                if (FileUpload1.HasFile)
                {
                    //string dosyaadi = Path.GetFileName(FileUpload1.PostedFile.FileName);
                    string dosyaadi = lblidcihaz.Text + "-" + txtruhsatno_kayit.Text + FileUpload1.PostedFile.FileName.ToString().Substring(FileUpload1.PostedFile.FileName.Length - 4, 4);
                    string dosyauzanti = Path.GetExtension(FileUpload1.PostedFile.FileName);


                    Guid uid = Guid.NewGuid();
                    string uids = uid.ToString();
                    uids = uids.Substring(0, 8);
                    string yol = "/dosyalar/kmz/" + txtruhsatno_kayit.Text + "/";
                    string dosyayeri2 = yol + uids + "-" + dosyaadi;
                    string dosyayeri = Server.MapPath("~" + dosyayeri2);

                    if (!Directory.Exists(Server.MapPath("~" + yol)))
                        Directory.CreateDirectory(Server.MapPath("~" + yol));


                    sorgu.Connection = conn;
                    sorgu.CommandText = "INSERT INTO RUHSAT_DOSYALAR (DOSYA_ADI,URL,URL2) VALUES ('" + uids + "-" + dosyaadi + "','" + dosyayeri + "','" + dosyayeri2 + "');SELECT @@IDENTITY AS 'Identity' ";
                    string id = sorgu.ExecuteScalar().ToString();

                    sorgu.CommandText = "UPDATE RUHSAT_RUHSATLAR SET KMZ='" + id + "' WHERE ID='" + lblidcihaz.Text + "' ";
                    sorgu.ExecuteNonQuery();

                    FileUpload1.SaveAs(dosyayeri);
                    lbldurumdosya.Text = "Dosya Başarıyla Yüklenmiştir. ";
                }
                else
                    lbldurumdosya.Text = "Dosya seçmediniz!!!";
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), System.Guid.NewGuid().ToString(), "YeniMesaj('Beklenmeyen bir hata oluştu.','Hata');", true);
                ScriptManager.RegisterStartupScript(this, this.GetType(), System.Guid.NewGuid().ToString(), "ShowMessage('Hata oluştu :'" + ex.Message.Replace("'", "") + ",'Hata','yeni');", true);
            }

            if (ConnectionState.Open == conn.State)
                conn.Close();
            listele_ruhsat();

            TabPanel2.Visible = true;
            TabPanel3.Visible = true;
            TabPanel4.Visible = true;
            TabPanel5.Visible = true;

        }

        protected void btnharita_Click(object sender, EventArgs e)
        {
            conn.Open();
            SqlCommand sorgu = new SqlCommand();
            sorgu.Connection = conn;
            sorgu.CommandText = "SELECT  RUHSAT_DOSYALAR.ID2 FROM RUHSAT_DOSYALAR INNER JOIN RUHSAT_RUHSATLAR ON RUHSAT_RUHSATLAR.KMZ=RUHSAT_DOSYALAR.ID WHERE RUHSAT_RUHSATLAR.ID=" + lblidcihaz.Text + "";
            string kmmz = sorgu.ExecuteScalar().ToString();
            conn.Close();

            //Response.Redirect("Haritalar/Harita.aspx?id=" + kmmz);
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "openpage", "window.open('" + "Haritalar/Harita.aspx?id=" + kmmz + "', 'popup_window', 'width=1000,height=800,left=100,top=100,resizable=yes', '_blank');", true);
            //ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "openpage", "window.open('" + "/pdf/web/ViewPDF.aspx', 'popup_window', 'width=800,height=600,left=100,top=100,resizable=yes', '_blank');", true);


        }

        protected void btnindir_Click(object sender, EventArgs e)
        {

        }

        protected void btnkmzsil_Click(object sender, EventArgs e)
        {

        }



        protected void btnsil_Click(object sender, EventArgs e)
        {

        }

        protected void btnmetrajekle_Click(object sender, EventArgs e)
        {
            SqlCommand sorgu = new SqlCommand();
            sorgu.Connection = conn;
            conn.Open();

            string userid = Session["KULLANICI_ID"].ToString();

            //txtDate_baslangic.Text = DateTime.Now.Year.ToString();

            try
            {
                if (lblidcihaz.Text != "")
                {
                    sorgu.CommandText = "INSERT INTO    RUHSAT_KAZI_BILGI (RUHSAT_TALEP_KABUL_ID, UZUNLUK, GENISLIK, KAZI_TURU_ID, KAZI_TIPI_ID, KAZI_YIL_ID) " +
                        " VALUES(@RTK_ID_K, @UZN_K, @GNS_K, @KTURU_ID_K, @KTIPI_ID_K, @KYILI_ID_K); SELECT @@IDENTITY AS 'Identity' ";
                    sorgu.Parameters.AddWithValue("@RTK_ID_K", lblidcihaz.Text);
                    sorgu.Parameters.AddWithValue("@UZN_K", Convert.ToDecimal(txtkaziuzunluk.Text));
                    sorgu.Parameters.AddWithValue("@GNS_K", Convert.ToDecimal(txtkazigenislik.Text));
                    sorgu.Parameters.AddWithValue("@KTURU_ID_K", comkazituru.SelectedValue);
                    sorgu.Parameters.AddWithValue("@KTIPI_ID_K", comkazitipi_sec.SelectedValue);
                    sorgu.Parameters.AddWithValue("@KYILI_ID_K", "2021");
                    string id_metraj = sorgu.ExecuteScalar().ToString();
                    lblidmetraj.Text = id_metraj;

                    ScriptManager.RegisterStartupScript(this, this.GetType(), System.Guid.NewGuid().ToString(), "YeniMesaj('Metraj bilgisi başarıyla kaydedilmiştir.','Tamam');", true);
                    ScriptManager.RegisterStartupScript(this, this.GetType(), System.Guid.NewGuid().ToString(), "ShowMessage('Metraj bilgisi başarıyla kaydedilmiştir.','Tamam','yeni');", true);
                }
                else
                {
                    /*// ------   UPDATE   ------ \\
                    sorgu.CommandText = "UPDATE RUHSAT_KAZI_BILGI SET (RUHSAT_TALEP_KABUL_ID=@RTK_ID_U, UZUNLUK=@PROJENO_U, GENISLIK=@IL_ID_U, KAZI_TURU_ID=@B_ID_U, KAZI_TIPI_ID=@ILCE_ID_U WHERE ID=@V_ID_U ";
                    sorgu.Parameters.AddWithValue("@RTK_ID_U", lblidcihaz.Text);
                    sorgu.Parameters.AddWithValue("@UZN_U", txtkaziuzunluk.Text);
                    sorgu.Parameters.AddWithValue("@GNS_U", txtkazigenislik.Text);
                    sorgu.Parameters.AddWithValue("@KTURU_ID_U", comkazituru.SelectedValue);
                    sorgu.Parameters.AddWithValue("@KTIPI_ID_U", comkazitipi_sec.SelectedValue);

                    sorgu.ExecuteNonQuery();
                    // ------   UPDATE   ------ \\*/

                    ScriptManager.RegisterStartupScript(this, this.GetType(), System.Guid.NewGuid().ToString(), "YeniMesaj('Ruhsat bilgisi boş.','Hata');", true);
                    ScriptManager.RegisterStartupScript(this, this.GetType(), System.Guid.NewGuid().ToString(), "ShowMessage('Ruhsat bilgisi boş.','Hata','yeni');", true);
                }

            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), System.Guid.NewGuid().ToString(), "YeniMesaj('Beklenmeyen bir hata oluştu.','Hata');", true);
                ScriptManager.RegisterStartupScript(this, this.GetType(), System.Guid.NewGuid().ToString(), "ShowMessage('Hata oluştu :'" + ex.Message.Replace("'", "") + ",'Hata','yeni');", true);
            }

            if (ConnectionState.Open == conn.State)
                conn.Close();

            listele_metraj();
        }

        protected void grid_metraj_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            grid_metraj.PageIndex = e.NewPageIndex;
            listele_metraj();
        }

        protected void btnkabulolustur_Click(object sender, EventArgs e)
        {
            try
            {
                SqlCommand sorgu = new SqlCommand();
                sorgu.Connection = conn;
                conn.Open();
                //FormsIdentity id = (FormsIdentity)Context.User.Identity;
                //FormsAuthenticationTicket ticket = id.Ticket;
                //string[] data = ticket.UserData.Split(',');
                //string userid = data[1];
                string userid = Session["KULLANICI_ID"].ToString();


                sorgu.CommandText = "INSERT INTO RUHSAT_RUHSATLAR (RUHSATNO, PROJENO, IL_ID, BOLGE_ID, ILCE_ID, MAHALLE_ID, CADDESOKAK_ID, ADRES_DETAY, T_PROJETIP_ID, " +
                                    " A_PROJETIP_ID, BASLANGIC_TARIHI, BITIS_TARIHI, ACIKLAMA, DURUM) " +

                                    " SELECT RUHSATNO, PROJENO, IL_ID, BOLGE_ID, ILCE_ID, MAHALLE_ID, CADDESOKAK_ID, ADRES_DETAY, T_PROJETIP_ID, A_PROJETIP_ID, BASLANGIC_TARIHI, " +
                                    " BITIS_TARIHI, ACIKLAMA, DURUM FROM RUHSAT_RUHSATLAR WHERE ID=" + lblruhsatidkabul.Text + "; SELECT @@IDENTITY AS 'Identity'  ";
                string id2 = Convert.ToString(sorgu.ExecuteScalar());
                sorgu.CommandText = "UPDATE RUHSAT_RUHSATLAR SET KABULMU='true', KESIF_ID=" + lblruhsatidkabul.Text + ", KAYIT_EDEN=" + userid + ",KAYIT_TARIHI=getdate() WHERE ID='" + id2 + "' ";
                sorgu.ExecuteNonQuery();

                sorgu.CommandText = "UPDATE RUHSAT_RUHSATLAR SET KABUL_YAPILDIMI='true' WHERE ID='" + lblruhsatidkabul.Text + "' ";
                sorgu.ExecuteNonQuery();

                sorgu.CommandText = "INSERT INTO RUHSAT_KAZI_BILGI (RUHSAT_TALEP_KABUL_ID, UZUNLUK, GENISLIK, KAZI_TURU_ID, KAZI_TIPI_ID, KAZI_YIL_ID) " +
                                        " SELECT " + id2 + ", UZUNLUK, GENISLIK, KAZI_TURU_ID, KAZI_TIPI_ID, KAZI_YIL_ID FROM RUHSAT_KAZI_BILGI WHERE RUHSAT_TALEP_KABUL_ID=" + lblruhsatidkabul.Text + "  ";
                sorgu.ExecuteNonQuery();



                btnkabulolustur.Enabled = false;
                btnkabulolustur.CssClass = "btn btn-success disabled";


                conn.Close();
                ScriptManager.RegisterStartupScript(this, this.GetType(), System.Guid.NewGuid().ToString(), "YeniMesaj('Aktarım tamamlanmıştır.','Tamam');", true);
                ScriptManager.RegisterStartupScript(this, this.GetType(), System.Guid.NewGuid().ToString(), "ShowMessage('Aktarım tamamlanmıştır.','Tamam','ruhsatkabul');", true);

                listele_ruhsat();

            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), System.Guid.NewGuid().ToString(), "YeniMesaj('Beklenmeyen bir hata oluştu.','Hata');", true);
                ScriptManager.RegisterStartupScript(this, this.GetType(), System.Guid.NewGuid().ToString(), "ShowMessage('Hata oluştu :'" + ex.Message.Replace("'", "") + ",'Hata','ruhsatkabul');", true);
            }

            if (ConnectionState.Open == conn.State)
                conn.Close();

            listele_ruhsat();



        }
    }
}