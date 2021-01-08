using System;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Drawing;

//ok
namespace EnvanterTveY
{
    public partial class SevkYonetimi : System.Web.UI.Page
    {
        SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["KabloHE"].ConnectionString);
        string sayfa = "SevkYonetimi";
        DataTable dt;
        KodT.kodlar tkod = new KodT.kodlar();
        bool tamirdepokontrol = false;

        int kbolge_sevk_h_bolge_yetkikontrol;
        int kbolge_sevk_k_bolge_yetkikontrol;

        bool garantili_malzeme_kabul_kontrol;
        bool garantisiz_malzeme_kabul_kontrol;

        bool garantili_kabul_kaydet;
        bool garantisiz_kabul_kaydet;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["KULLANICI_ID"] == null)
            {
                Response.Redirect("Giris.aspx?url=" + sayfa);
            }
            if (!IsPostBack)
            {
                Kaynak_Bolge_Ara_Listele();
                Hedef_Bolge_Ara_Listele();
                Sevk_Durum_Ara_Listele();

                string islem;
                islem = Request.QueryString["islem"];
                if (islem == "tamirsevk")
                {
                    if (Session["SEVK-ID"] != null)
                    {
                        txtsevkidara.Text = Session["SEVK-ID"].ToString();
                        Session.Remove("SEVK-ID");
                        listele_sevk();
                        btn_islemler(grid_sevk_listele.Rows[0].Cells[0].Text, 0);
                    }
                    else
                        listele_sevk();
                }
                else
                    listele_sevk();
            }
        }

        public void js_calistir(string str)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), System.Guid.NewGuid().ToString(), str, true);
        }

        void modal_ac(string modal_isim)
        {
            js_calistir("ModalAc('" + modal_isim + "');");
        }

        void modal_kapat(string modal_isim)
        {
            js_calistir("ModalKapat('" + modal_isim + "');");
        }

        void btn_islemler(string id, int index)
        {
            lblidsevk.Text = id;

            lblkaynakbolge.Text = HttpUtility.HtmlDecode(grid_sevk_listele.Rows[index].Cells[1].Text.Trim());
            lblkaynakdepo.Text = HttpUtility.HtmlDecode(grid_sevk_listele.Rows[index].Cells[2].Text.Trim());
            lblhedefbolge.Text = HttpUtility.HtmlDecode(grid_sevk_listele.Rows[index].Cells[3].Text.Trim());
            lblshedefdepo.Text = HttpUtility.HtmlDecode(grid_sevk_listele.Rows[index].Cells[4].Text.Trim());

            //Kullancının yetkili olduğu bölgelere göre sevk işlemleri sırasında görünecek butonların yetkileri için aşağıdaki kodlar kullanılıyor
            // listele_sevk, listele_sevk_tamir, ve bunların gird.rowdatabond^ları için

            //Kaynak Depo tanımına bakıyor. 2 yani Tamir Depo ise 
            lbldepotanimkontrol_tamir.Text = tkod.sql_calistir_param("SELECT DEPOTANIMI_ID FROM DEPO WHERE DEPO=@DTID_D", new SqlParameter("DTID_D", lblkaynakdepo.Text)); // KAYNAK DEPO tanımına bakılıyor.

            if (lbldepotanimkontrol_tamir.Text == "2" || lbldepotanimkontrol_tamir.Text == "4") //Tamir veya Tedarik Depodan diğer depolara sevk için listele_sevkmalzemeleri_tamirsevkolustur çalışacak
            {
                lblkaynakbolge_tamir.Text = lblkaynakbolge.Text;
                lblkaynakdepo_tamir.Text = lblkaynakdepo.Text;
                lblhedefbolge_tamir.Text = lblhedefbolge.Text;
                lblshedefdepo_tamir.Text = lblshedefdepo.Text;

                lblmodalyenibaslik2.Text = "Tamir Depodan Sevk İşlemleri";
                lblsevkdurumtexttamir.Text = HttpUtility.HtmlDecode(grid_sevk_listele.Rows[index].Cells[5].Text.Trim());
                combolgesec_tamirsevkolustur.SelectedValue = "0";

                listele_sevkmalzemeleri_tamirsevkolustur();

                listele_bolge_sevkmalzemeekle();
                modal_ac("ModalTamir_MalzemeEkle");

                /*
                System.Text.StringBuilder sb = new System.Text.StringBuilder();
                sb.Append(@"<script type='text/javascript'>");
                sb.Append("$(\"#ModalTamir_MalzemeEkle\").modal(\"show\");");
                sb.Append(@"</script>");
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(),
                           "ModalScript", sb.ToString(), false);
                           */
                //ClientScript.RegisterStartupScript(this.GetType(), "Popup", "ModalAc('ModalTamir_MalzemeEkle');", true);
            }
            else 
            {
                System.Text.StringBuilder sb = new System.Text.StringBuilder();
                sb.Append(@"<script type='text/javascript'>");
                sb.Append("$(\"#ModalMalzemeEkle\").modal(\"show\");");
                sb.Append(@"</script>");
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(),
                            "ModalScript", sb.ToString(), false);
                lblmodalyenibaslik1.Text = "Sevk İşlemleri";
                lblsevkdurumtext.Text = HttpUtility.HtmlDecode(grid_sevk_listele.Rows[index].Cells[5].Text.Trim());
                txtserinoara.Text = "";
                btnsevket.Enabled = true;
                btnsevket.CssClass = "btn btn-success";

                lbldepotanimkontrol.Text = tkod.sql_calistir_param("SELECT DEPOTANIMI_ID FROM DEPO WHERE DEPO=@DEPO", new SqlParameter("DEPO", lblshedefdepo.Text));  // HEDEF DEPO'nun tanımına bakılıyor. Hedef depo tanımı TAMİR DEPO ise SEVK ET butonunun işlevi değişiyor. bkn:btnsevket_click
                lblhedefdepoid.Text = tkod.sql_calistir_param("SELECT ID FROM DEPO WHERE DEPO=@DEPO", new SqlParameter("DEPO", lblshedefdepo.Text));  // HEDEF DEPO'nun ID'sine bakılıyor. Hedef depo id'si TAMIR_MALZEME_ISLEME'E yazılıyor. Böylelikle farklı tamir depoelara giden mazlemeler farklı yerlerde listelenebilecek. Örnek KSAVİD Tamir Depo ise Tamir İşlemleri sayfasında. TRon Tamir Depo ise farklı bir sayfada listelenecek.  btnsevket_click

                listele_sevkmalzemeleri();
            }
            // listele_sevkmalzemeleri_tamirsevkolustur();
            // grid_sevk_listele.DataBind();
            txtserino_tamirsevkolustur.Text = "";
        }

        // SEVK Oluştur >>> MALZEMEM_SEVK tablosunu kullanıyor.  Kaynak ve Hedef Bölge ve Depolar belirleniyor. 
        //SEVK_DURUMU 1-Sevk kaydı oluşturulmuş ama sevk başlamamış.  2- Sevk edilmiş 3-Tevk teslim alınmış yani tamamlanmış.

        void listele_sevk()
        {
            SqlCommand cmd;
            string sql = " ", sql1 = " ", sql2 = "";

            sql = "SELECT MSO.ID, HBOLGE.BOLGE_ADI AS 'HBOLGE_ADI', HDEPO.DEPO AS 'HDEPO',  BOLGE.BOLGE_ADI, DEPO.DEPO, '<span class=\"text rounded p-10 ' + MALZEME_SEVK_DURUM.CLASS + ' \"> '+ MALZEME_SEVK_DURUM.S_DURUM + ' </span>' AS S_DURUM, " +
                " KULLANICI.ISIM + ' ' +CONVERT(NVARCHAR(10),MSO.KAYIT_TARIHI,104) AS KAYIT " +

                " FROM MALZEME_SEVK AS MSO " +
                    " LEFT JOIN BOLGE AS HBOLGE ON HBOLGE.ID=MSO.HEDEF_BOLGE  " +
                    " LEFT JOIN DEPO AS HDEPO ON HDEPO.ID=MSO.HEDEF_DEPO " +
                    " LEFT JOIN BOLGE ON BOLGE.ID=MSO.KAYNAK_BOLGE " +
                    " LEFT JOIN DEPO ON DEPO.ID=MSO.KAYNAK_DEPO " +
                    " LEFT JOIN MALZEME_SEVK_DURUM ON MSO.SEVK_DURUM=MALZEME_SEVK_DURUM.ID " +
                " LEFT JOIN KULLANICI ON KULLANICI.ID=MSO.KAYIT_EDEN ";

            if (comkaynakbolgeara.SelectedIndex > 0)
                sql1 += " AND MSO.KAYNAK_BOLGE='" + comkaynakbolgeara.SelectedValue + "'   ";

            if (comhedefbolgeara.SelectedIndex > 0)
                sql1 += " AND MSO.HEDEF_BOLGE='" + comhedefbolgeara.SelectedValue + "'  ";

            if (comsevkdurumara.SelectedIndex > 0)
                sql1 += " AND MSO.SEVK_DURUM='" + comsevkdurumara.SelectedValue + "'  ";

            if (txtsevkidara.Text.Length > 0)
                sql1 += " AND MSO.ID='" + txtsevkidara.Text + "'  ";

            sql = sql + tkod.yetki_tablosu_inner("HDEPO", "DEPO") + "   WHERE MSO.ID > 0  " + sql1 + tkod.yetki_tablosu_filtre("DEPO") + "   ORDER BY MSO.ID DESC";

            cmd = new SqlCommand(sql, conn);
            conn.Open();
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            grid_sevk_listele.DataSource = dt;
            grid_sevk_listele.DataBind();
            conn.Close();
            lblsevksayisi.Text = grid_sevk_listele.Rows.Count.ToString() + " " + "adet kayıt bulunmuştur.";
        }

        void Kaynak_Bolge_Ara_Listele()
        {
            comkaynakbolgeara.Items.Clear();
            comkaynakbolgeara.Items.Insert(0, new ListItem("Kaynak Bölge Seçiniz", "0"));
            comkaynakbolgeara.AppendDataBoundItems = true;

            comkaynakbolgeara.DataSource = tkod.dt_bolge_Liste();
            comkaynakbolgeara.DataBind();
        }

        void Hedef_Bolge_Ara_Listele()
        {
            comhedefbolgeara.Items.Clear();
            comhedefbolgeara.Items.Insert(0, new ListItem("Hedef Bölge Seçiniz", "0"));
            comhedefbolgeara.AppendDataBoundItems = true;

            string sql = "SELECT  HBOLGE.BOLGE_ADI, HBOLGE.ID " +
                " FROM MALZEME_SEVK AS MSO " +
                    " LEFT JOIN BOLGE AS HBOLGE ON HBOLGE.ID=MSO.HEDEF_BOLGE  " +
                    " LEFT JOIN DEPO AS HDEPO ON HDEPO.ID=MSO.HEDEF_DEPO " +
                    " LEFT JOIN BOLGE ON BOLGE.ID=MSO.KAYNAK_BOLGE " +
                    " LEFT JOIN DEPO ON DEPO.ID=MSO.KAYNAK_DEPO " +
                    " LEFT JOIN MALZEME_SEVK_DURUM ON MSO.SEVK_DURUM=MALZEME_SEVK_DURUM.ID " +
                " LEFT JOIN KULLANICI ON KULLANICI.ID=MSO.KAYIT_EDEN " +
                " LEFT JOIN KULLANICI AS SKULLANICI ON SKULLANICI.ID=MSO.SEVK_EDEN " +
                " LEFT JOIN KULLANICI AS TKULLANICI ON TKULLANICI.ID=MSO.TESLIM_ALAN " +
                " LEFT JOIN KULLANICI AS IKULLANICI ON IKULLANICI.ID=MSO.IADE_EDEN " +
                "  ";

            sql = sql + tkod.yetki_tablosu() + "  WHERE MSO.ID > 0  " + tkod.yetki_tablosu_2() + " GROUP BY HBOLGE.ID, HBOLGE.BOLGE_ADI ORDER BY HBOLGE.BOLGE_ADI";

            comhedefbolgeara.DataSource = tkod.GetData(sql);
            comhedefbolgeara.DataBind();
        }

        void Sevk_Durum_Ara_Listele()
        {
            comsevkdurumara.Items.Clear();
            comsevkdurumara.Items.Insert(0, new ListItem("Sevk Durumu Seçiniz", "0"));
            comsevkdurumara.AppendDataBoundItems = true;

            SqlDataAdapter da = new SqlDataAdapter();
            SqlCommand cmd = new SqlCommand("Select DEGER AS ID,SECENEK FROM COMDOLDUR WHERE COM_ADI = 'sevk-sevkdurum-ara'  ORDER BY SIRA ");
            DataTable dt = new DataTable();
            da.SelectCommand = cmd;
            da.SelectCommand.Connection = conn;
            da.Fill(dt);
            comsevkdurumara.DataSource = dt;
            comsevkdurumara.DataTextField = "SECENEK";
            comsevkdurumara.DataValueField = "ID";
            comsevkdurumara.DataBind();
        }

        protected void comkaynakbolgesec_SelectedIndexChanged(object sender, EventArgs e)
        {
            listele_kaynakdepo();
            comkaynakdeposec.Enabled = true;
        }

        protected void comkaynakdeposec_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (lblidcihaz.Text == "")
            {
                listele_hedefbolge();
                comhedefbolgesec.Enabled = true;
            }
            else
            {
                comhedefbolgesec.Enabled = true;
            }
        }

        protected void comhedefbolgesec_SelectedIndexChanged(object sender, EventArgs e)
        {
            listele_hedefdepo();
            comhedefdeposec.Enabled = true;
        }

        protected void comhedefdeposec_SelectedIndexChanged(object sender, EventArgs e)
        {
        }

        void listele_kaynakbolge()
        {
            comkaynakbolgesec.Items.Clear();
            comkaynakbolgesec.Items.Insert(0, new ListItem("Bölge Seçiniz", "0"));
            comkaynakbolgesec.AppendDataBoundItems = true;

            comkaynakbolgesec.DataSource = tkod.dt_bolge_Liste();
            comkaynakbolgesec.DataBind();
        }

        void listele_kaynakdepo()
        {
            comkaynakdeposec.Items.Clear();
            comkaynakdeposec.Items.Insert(0, new ListItem("Depo Seçiniz", "0"));
            comkaynakdeposec.AppendDataBoundItems = true;

            if (comkaynakbolgesec.SelectedIndex > 0)
                comkaynakdeposec.DataSource = tkod.dt_depo_Liste(comkaynakbolgesec.SelectedValue.ToString());
            else
                comkaynakdeposec.DataSource = tkod.dt_depo_Liste();

            comkaynakdeposec.DataBind();
        }

        void listele_hedefbolge()
        {
            conn.Open();
            comhedefbolgesec.Items.Clear();
            comhedefbolgesec.Items.Insert(0, new ListItem("-", "0"));
            comhedefbolgesec.AppendDataBoundItems = true;
            string sql1 = "";

            SqlDataAdapter da = new SqlDataAdapter();
            SqlCommand sorgu = new SqlCommand("Select ID,BOLGE_ADI FROM BOLGE ORDER BY ID ");
            DataTable dt = new DataTable();
            da.SelectCommand = sorgu;
            da.SelectCommand.Connection = conn;
            da.Fill(dt);
            comhedefbolgesec.DataSource = dt;
            comhedefbolgesec.DataBind();
            conn.Close();
        }

        void listele_hedefdepo()
        {
            comhedefdeposec.Items.Clear();
            comhedefdeposec.Items.Insert(0, new ListItem("-", "0"));
            comhedefdeposec.AppendDataBoundItems = true;
            string sql2 = "";

            if (comhedefbolgesec.SelectedIndex > 0)
                sql2 = " AND BOLGE_ID='" + comhedefbolgesec.SelectedValue.ToString() + "' ";

            string sql = "SELECT DEPO,ID FROM DEPO WHERE ID>0 " + sql2 + " ORDER BY DEPO";
            comhedefdeposec.DataSource = tkod.GetData(sql);
            comhedefdeposec.DataBind();
        }

        protected void btnara_Click(object sender, EventArgs e)
        {
            listele_sevk();
        }

        protected void btnsevkekle_Click(object sender, EventArgs e)
        {
            lblidcihaz.Text = "";
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            sb.Append(@"<script type='text/javascript'>");
            sb.Append("$(\"#ModalSevkOlustur\").modal(\"show\");");
            sb.Append(@"</script>");
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(),
                       "ModalScript", sb.ToString(), false);

            listele_kaynakbolge();
            comkaynakbolgesec.Enabled = true;
            comkaynakdeposec.SelectedValue = "0";
            comkaynakdeposec.Enabled = false;
            comhedefbolgesec.SelectedValue = "0";
            comhedefbolgesec.Enabled = false;
            comhedefdeposec.SelectedValue = "0";
            comhedefdeposec.Enabled = false;

            txtkargo.Text = "";
            txtaciklama.Text = "";
            lblsurecno.Text = "";

            chcgarantilimalzemekabul.Checked = false;
            chcgarantisizmalzemekabul.Checked = false;

            chcgarantilimalzemekabul.Visible = false;
            chcgarantisizmalzemekabul.Visible = false;

            btnsevkkaydet.Enabled = true;
            btnsevkkaydet.CssClass = "btn btn-success";

            lblmodalyenibaslik.Text = "Yeni Sevk Oluştur";

            btnislemler.Visible = false;
        }

        protected void btnsevkkaydet_Click(object sender, EventArgs e)
        {
            try
            {
                SqlCommand sorgu = new SqlCommand();
                sorgu.Connection = conn;
                conn.Open();

                if (lblidcihaz.Text == "")
                {
                    if (comhedefdeposec.SelectedIndex > 0)
                    {
                        sorgu.CommandText = "SELECT DEPOTANIMI_ID FROM DEPO WHERE ID = @ID1 ";
                        sorgu.Parameters.AddWithValue("@ID1", comhedefdeposec.SelectedValue);
                        int garanti_kabul_tespit = Convert.ToInt16(sorgu.ExecuteScalar());

                        sorgu.CommandText = "SELECT GARANTILI_MALZEME_KABUL FROM DEPO_TANIMI WHERE ID = '" + garanti_kabul_tespit + "'  ";
                        garantili_kabul_kaydet = Convert.ToBoolean(sorgu.ExecuteScalar());

                        sorgu.CommandText = "SELECT GARANTISIZ_MALZEME_KABUL FROM DEPO_TANIMI WHERE ID = '" + garanti_kabul_tespit + "'  ";
                        garantisiz_kabul_kaydet = Convert.ToBoolean(sorgu.ExecuteScalar());

                        string userid = Session["KULLANICI_ID"].ToString();
                        sorgu.CommandText = "INSERT INTO    MALZEME_SEVK (KAYNAK_BOLGE, KAYNAK_DEPO, HEDEF_BOLGE, HEDEF_DEPO, SEVK_DURUM, KARGO," +
                            " ACIKLAMA, GARANTILI_MALZEME_KABUL, GARANTISIZ_MALZEME_KABUL, KAYIT_EDEN , KAYIT_TARIHI)  VALUES(@KB, @KD, @HB, @HD, @SD, @KRG, @ACKLM, @GRNL_M_K, @GRNSZ_M_K, @UI, getdate() ); SELECT @@IDENTITY AS 'Identity' "; 
                        sorgu.Parameters.AddWithValue("@KB", comkaynakbolgesec.SelectedValue);
                        sorgu.Parameters.AddWithValue("@KD", comkaynakdeposec.SelectedValue);
                        sorgu.Parameters.AddWithValue("@HB", comhedefbolgesec.SelectedValue);
                        sorgu.Parameters.AddWithValue("@HD", comhedefdeposec.SelectedValue);
                        sorgu.Parameters.AddWithValue("@SD", surecdurumid.Text); //SEVK DURUMUNU YENİ YAPMASI İÇİN TEXT İÇERİĞİNDE 1 YAZIYOR //sonradan baktım da niye böyle bişey yapmışım anlamadım ama çalışıyor. Çalışan koda dokunulmaz.
                        sorgu.Parameters.AddWithValue("@KRG", txtkargo.Text);
                        sorgu.Parameters.AddWithValue("@ACKLM", txtaciklama.Text);
                        sorgu.Parameters.AddWithValue("@GRNL_M_K", garantili_kabul_kaydet);
                        sorgu.Parameters.AddWithValue("@GRNSZ_M_K", garantisiz_kabul_kaydet);
                        sorgu.Parameters.AddWithValue("@UI", userid);

                        string id = sorgu.ExecuteScalar().ToString();
                        lblsurecno.Text = id;

                        // ------   ***   ------ \\
                        // ------   LOG   ------ \\
                        // ------   ***   ------ \\
                        sorgu.CommandText = "INSERT INTO    SEVK_LOG (SEVK_ID, KAYNAK_BOLGE_ID, KAYNAK_DEPO_ID, HEDEF_BOLGE_ID, HEDEF_DEPO_ID, SEVK_DURUM_ID, KAYIT_EDEN_ID, KAYIT_TARIHI,  ACIKLAMA ) " +
                            " VALUES(@SID_LOG, @KBID_LOG, @KDID_LOG, @HBID_LOG, @HDID_LOG, @SD_LOG, @KE_LOG, getdate(), @ACKLM_LOG ) ";
                        sorgu.Parameters.AddWithValue("@SID_LOG", lblsurecno.Text);
                        sorgu.Parameters.AddWithValue("@KBID_LOG", comkaynakbolgesec.SelectedValue);
                        sorgu.Parameters.AddWithValue("@KDID_LOG", comkaynakdeposec.SelectedValue);
                        sorgu.Parameters.AddWithValue("@HBID_LOG", comhedefbolgesec.SelectedValue);
                        sorgu.Parameters.AddWithValue("@HDID_LOG", comhedefdeposec.SelectedValue);
                        sorgu.Parameters.AddWithValue("@SD_LOG", surecdurumid.Text);
                        sorgu.Parameters.AddWithValue("@KE_LOG", userid);
                        sorgu.Parameters.AddWithValue("@ACKLM_LOG", "Yeni Sevk kaydı oluşturuldu");
                        sorgu.ExecuteNonQuery();
                        // ------   ***   ------ \\
                        // ------   LOG   ------ \\
                        // ------   ***   ------ \\

                        ScriptManager.RegisterStartupScript(this, this.GetType(), System.Guid.NewGuid().ToString(), "YeniMesaj('Kayıt başarılı.','Tamam');", true);
                        ScriptManager.RegisterStartupScript(this, this.GetType(), System.Guid.NewGuid().ToString(), "ShowMessage('Sevk Kaydı başarıyla oluşturulmuştur.','Tamam','yeni');", true);

                        btnsevkkaydet.Enabled = false;
                        btnsevkkaydet.CssClass = "btn btn-success disabled";
                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(this, this.GetType(), System.Guid.NewGuid().ToString(), "YeniMesaj('Lütfen tüm alanları doldurun.','Hata');", true);
                        ScriptManager.RegisterStartupScript(this, this.GetType(), System.Guid.NewGuid().ToString(), "ShowMessage('Lütfen tüm alanları doldurun.','Hata','yeni');", true);
                    }
                }
                else
                {
                    string userid = Session["KULLANICI_ID"].ToString();
                    sorgu.CommandText = "UPDATE MALZEME_SEVK SET KAYNAK_BOLGE=@KB, KAYNAK_DEPO=@KD, HEDEF_BOLGE=@HB, HEDEF_DEPO=@HD, KARGO=@KRG, ACIKLAMA=@ACKLM, GARANTILI_MALZEME_KABUL=@GRNL_M_K,GARANTISIZ_MALZEME_KABUL=@GRNSZ_M_K  WHERE ID=@MS_ID_U ";
                    sorgu.Parameters.AddWithValue("@KB", comkaynakbolgesec.SelectedValue);
                    sorgu.Parameters.AddWithValue("@KD", comkaynakdeposec.SelectedValue);
                    sorgu.Parameters.AddWithValue("@HB", comhedefbolgesec.SelectedValue);
                    sorgu.Parameters.AddWithValue("@HD", comhedefdeposec.SelectedValue);
                    sorgu.Parameters.AddWithValue("@KRG", txtkargo.Text);
                    sorgu.Parameters.AddWithValue("@ACKLM", txtaciklama.Text);
                    sorgu.Parameters.AddWithValue("@MS_ID_U", lblidcihaz.Text);
                    sorgu.Parameters.AddWithValue("@GRNL_M_K", chcgarantilimalzemekabul.Checked);
                    sorgu.Parameters.AddWithValue("@GRNSZ_M_K", chcgarantisizmalzemekabul.Checked);

                    sorgu.ExecuteNonQuery();
                    ScriptManager.RegisterStartupScript(this, this.GetType(), System.Guid.NewGuid().ToString(), "ShowMessage('Sevk Kaydı başarıyla güncellenmiştir.','Tamam','yeni');", true);
                    ScriptManager.RegisterStartupScript(this, this.GetType(), System.Guid.NewGuid().ToString(), "YeniMesaj('Sevk Kaydı başarıyla güncellenmiştir.','Tamam');", true);

                    btnsevkkaydet.Enabled = false;
                    btnsevkkaydet.CssClass = "btn btn-success disabled";

                    // ------   ***   ------ \\
                    // ------   LOG   ------ \\
                    // ------   ***   ------ \\
                    sorgu.CommandText = "INSERT INTO    SEVK_LOG (SEVK_ID, KAYNAK_BOLGE_ID, KAYNAK_DEPO_ID, HEDEF_BOLGE_ID, HEDEF_DEPO_ID, SEVK_DURUM_ID, KAYIT_EDEN_ID, KAYIT_TARIHI,  ACIKLAMA) " +
                        " VALUES(@SID_LOG, @KBID_LOG, @KDID_LOG, @HBID_LOG, @HDID_LOG, @SD_LOG, @KE_LOG, getdate(), @ACKLM_LOG ) ";
                    sorgu.Parameters.AddWithValue("@SID_LOG", lblsurecno.Text);
                    sorgu.Parameters.AddWithValue("@KBID_LOG", comkaynakbolgesec.SelectedValue);
                    sorgu.Parameters.AddWithValue("@KDID_LOG", comkaynakdeposec.SelectedValue);
                    sorgu.Parameters.AddWithValue("@HBID_LOG", comhedefbolgesec.SelectedValue);
                    sorgu.Parameters.AddWithValue("@HDID_LOG", comhedefdeposec.SelectedValue);
                    sorgu.Parameters.AddWithValue("@SD_LOG", surecdurumid.Text);
                    sorgu.Parameters.AddWithValue("@KE_LOG", userid);
                    sorgu.Parameters.AddWithValue("@ACKLM_LOG", "Sevk güncellendi.");
                    sorgu.ExecuteNonQuery();
                    // ------   ***   ------ \\
                    // ------   LOG   ------ \\
                    // ------   ***   ------ \\
                }
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), System.Guid.NewGuid().ToString(), "YeniMesaj('Beklenmeyen bir hata oluştu.','Hata');", true);
                ScriptManager.RegisterStartupScript(this, this.GetType(), System.Guid.NewGuid().ToString(), "ShowMessage('Hata oluştu :'" + ex.Message.Replace("'", "") + ",'Hata','yeni');", true);
            }

            if (ConnectionState.Open == conn.State)
                conn.Close();

            listele_sevk();
            //btnislemler.Visible = true;
        }

        //Sevk sürecinde malzeme bulunmaması durumunda silme işlemi yapılmaktadır.
        //**************** Malzeme silinince  MALZEMELER SEVKDURUM = false YAPILIYOR **************** Bu sayede malzeme tekrar sevk sürecine dahil edilebilir. Aksi halde edilemez.

        protected void btnsil_Click(object sender, EventArgs e)
        {
            if (lblislem.Text == "sevk-sil")
            {
                SqlCommand sorgu = new SqlCommand();
                sorgu.Connection = conn;
                conn.Open();

                sorgu.CommandText = "SELECT COUNT(*) FROM MALZEME_SEVK_MALZEMELER WHERE SEVK_ID=@MSM_SID ";
                sorgu.Parameters.AddWithValue("@MSM_SID", lblidsil.Text);
                int sayi = Convert.ToInt16(sorgu.ExecuteScalar());

                if (sayi == 0)
                {
                    sorgu.CommandText = "DELETE FROM MALZEME_SEVK WHERE ID=@MSM_SID_1";
                    sorgu.Parameters.AddWithValue("@MSM_SID_1", lblidsil.Text);
                    sorgu.ExecuteNonQuery();

                    string userid = Session["KULLANICI_ID"].ToString();

                    // ------   ***   ------ \\
                    // ------   LOG   ------ \\
                    // ------   ***   ------ \\
                    sorgu.CommandText = "INSERT INTO    SEVK_LOG (SEVK_ID, SILEN_ID, SILME_TARIHI, ACIKLAMA) " +
                        " VALUES(@SID_LOG, @SLN_LOG, getdate(), @ACKLM_LOG ) ";
                    sorgu.Parameters.AddWithValue("@SID_LOG", lblidsil.Text);
                    sorgu.Parameters.AddWithValue("@SLN_LOG", comkaynakbolgesec.SelectedValue);
                    sorgu.Parameters.AddWithValue("@ACKLM_LOG", "Sevk kaydı silindi.");
                    sorgu.ExecuteNonQuery();
                    // ------   ***   ------ \\
                    // ------   LOG   ------ \\
                    // ------   ***   ------ \\

                    conn.Close();
                    ScriptManager.RegisterStartupScript(this, this.GetType(), System.Guid.NewGuid().ToString(), "ShowMessage('Sevk Kaydı başarıyla silinmiştir.','Tamam','sil');", true);
                    ScriptManager.RegisterStartupScript(this, this.GetType(), System.Guid.NewGuid().ToString(), "YeniMesaj('Sevk Kaydı başarıyla silinmiştir.','Tamam');", true);

                    btnsil.Enabled = false;
                    btnsil.CssClass = "btn btn-danger disabled";
                    modal_kapat("ModalSil");
                    listele_sevk();
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), System.Guid.NewGuid().ToString(), "YeniMesaj('Sevk sürecine malzeme dahil edildiği için silinemez. Öncelikle malzemelerin silinmesi gerekmektedir.','Hata');", true);
                    ScriptManager.RegisterStartupScript(this, this.GetType(), System.Guid.NewGuid().ToString(), "ShowMessage('Sevk sürecine malzeme dahil edildiği için silinemez. Öncelikle malzemelerin silinmesi gerekmektedir.','Hata','sil');", true);
                }
                if (ConnectionState.Open == conn.State)
                    conn.Close();
            }
            if (lblislem.Text == "sevk-malzeme-sil")
            {
                SqlCommand sorgu = new SqlCommand();
                sorgu.Connection = conn;
                SqlCommand sorgu5 = new SqlCommand();
                sorgu5.Connection = conn;
                conn.Open();

                sorgu.CommandText = "SELECT MALZEME_ID FROM MALZEME_SEVK_MALZEMELER WHERE ID = @MSM_SID_2 ";
                sorgu.Parameters.AddWithValue("@MSM_SID_2", lblidsil.Text);
                int malzemeidbul = Convert.ToInt16(sorgu.ExecuteScalar()); //malzemeidbul int'a malzeme_sevk_malzemeler tablosundaki malzemenin id'si atanıyor

                SqlCommand sorgu3 = new SqlCommand();
                sorgu3.Connection = conn;

                sorgu3.CommandText = "SELECT BOLGE_ID FROM MALZEMELER WHERE ID = '" + malzemeidbul + "' ";
                int bolgeid = Convert.ToInt16(sorgu3.ExecuteScalar());

                sorgu3.CommandText = "SELECT DEPO_ID FROM MALZEMELER WHERE ID = '" + malzemeidbul + "' ";
                int depoid = Convert.ToInt16(sorgu3.ExecuteScalar());

                //sorgu3.CommandText = "SELECT ISNULL(ONCEKI_BOLGE_ID,0) FROM MALZEMELER WHERE ID = '" + malzemeidbul + "' ";
                //int oncekibolgeid = Convert.ToInt16(sorgu3.ExecuteScalar());

                //sorgu3.CommandText = "SELECT ISNULL(ONCEKI_DEPO_ID,0) FROM MALZEMELER WHERE ID = '" + malzemeidbul + "' ";
                //int oncekidepoid = Convert.ToInt16(sorgu3.ExecuteScalar());

                sorgu3.CommandText = "UPDATE MALZEMELER SET SEVKDURUMU=@SD WHERE ID='" + malzemeidbul + "' ";
                // malzeme eklenip sevk süreci hiç başlamadan malzeme silinirse sadece sevk durumu false olacak. depolar zaten değişmemiş durumda bu nedenle aşağıdaki ifadeleri kaldırdım.
                //, BOLGE_ID = @BI, DEPO_ID = @DI, ONCEKI_BOLGE_ID = @OBI, ONCEKI_DEPO_ID = @ODI
                sorgu3.Parameters.AddWithValue("@SD", false);
                //sorgu3.Parameters.AddWithValue("@GD", false);  Güncelleme Durumu'nu false yaparsak 1. sevkte sorun yok ama 2.sevk etme işleminde malzeme silindiğinde de güncellemeye izin veriyor bu nedenle false yapmamak gerekiyor. Yada ilk sevkten sonrakilere bakma demek lazım. o da zor iş :)
                //sorgu3.Parameters.AddWithValue("@BI", oncekibolgeid);
                //sorgu3.Parameters.AddWithValue("@DI", oncekidepoid);
                //sorgu3.Parameters.AddWithValue("@OBI", DBNull.Value);
                //sorgu3.Parameters.AddWithValue("@ODI", DBNull.Value);
                sorgu3.ExecuteNonQuery();

                // ------   ***   ------ \\
                // ------   LOG   ------ \\
                // ------   ***   ------ \\
                string userid = Session["KULLANICI_ID"].ToString();
                sorgu5.CommandText = "INSERT INTO    MALZEMELER_LOG (M_ID, SEVK_ID, E_BOLGE, E_DEPO, ACIKLAMA, KAYIT_EDEN, KAYIT_TARIHI) " +
                    " VALUES(@MID, @SID, @EBOLGE, @EDEPO, @ACIKLAMA, @UI, getdate() ) ";
                sorgu5.Parameters.AddWithValue("@MID", malzemeidbul);
                sorgu5.Parameters.AddWithValue("@SID", lblidsevk.Text);
                sorgu5.Parameters.AddWithValue("@EBOLGE", bolgeid);
                sorgu5.Parameters.AddWithValue("@EDEPO", depoid);
                sorgu5.Parameters.AddWithValue("@ACIKLAMA", "Malzeme sevk sürecinden çıkartıldı.");
                sorgu5.Parameters.AddWithValue("@UI", userid);
                sorgu5.ExecuteNonQuery();

                // ------   ***   ------ \\
                // ------   LOG   ------ \\
                // ------   ***   ------ \\

                // ------   Malzemeyi Sevk sürecinden çıkar   ------ \\
                sorgu.CommandText = "DELETE FROM MALZEME_SEVK_MALZEMELER WHERE ID=@MSM_SID_3";
                sorgu.Parameters.AddWithValue("@MSM_SID_3", lblidsil.Text);
                sorgu.ExecuteNonQuery();
                // ------   Malzemeyi Sevk sürecinden çıkar   ------ \\
                conn.Close();

                ScriptManager.RegisterStartupScript(this, this.GetType(), System.Guid.NewGuid().ToString(), "ShowMessage('Malzeme başarıyla silinmiştir.','Tamam','sil');", true);
                ScriptManager.RegisterStartupScript(this, this.GetType(), System.Guid.NewGuid().ToString(), "YeniMesaj('Malzeme başarıyla silinmiştir.','Tamam');", true);

                btnsil.Enabled = false;
                btnsil.CssClass = "btn btn-danger disabled";
                modal_kapat("ModalSil");

                listele_sevkmalzemeleri();
                if (ConnectionState.Open == conn.State)
                    conn.Close();

                listele_sevk();
            }
            if (lblislem.Text == "sevk-tamir-malzeme-sil")
            {
                SqlCommand sorgu = new SqlCommand();
                sorgu.Connection = conn;
                SqlCommand sorgu5 = new SqlCommand();
                sorgu5.Connection = conn;
                conn.Open();
                sorgu.CommandText = "SELECT MALZEME_ID FROM MALZEME_SEVK_MALZEMELER WHERE ID = @MSM_SID_4 ";
                sorgu.Parameters.AddWithValue("@MSM_SID_4", lblidsil.Text);
                int malzemeidbul = Convert.ToInt16(sorgu.ExecuteScalar());   //malzemeidbul int'a malzeme_sevk_malzemeler tablosundaki malzemenin id'si atanıyor

                SqlCommand sorgu3 = new SqlCommand();
                sorgu3.Connection = conn;

                sorgu3.CommandText = "SELECT BOLGE_ID FROM MALZEMELER WHERE ID = '" + malzemeidbul + "' ";
                int bolgeid = Convert.ToInt16(sorgu3.ExecuteScalar());

                sorgu3.CommandText = "SELECT DEPO_ID FROM MALZEMELER WHERE ID = '" + malzemeidbul + "' ";
                int depoid = Convert.ToInt16(sorgu3.ExecuteScalar());

                sorgu.CommandText = "SELECT ISNULL(TAMIRE_GELEN_SEVK_ID, '0') FROM MALZEME_SEVK_MALZEMELER WHERE MALZEME_ID = '" + malzemeidbul + "' AND ID = @MSM_SID_5 ";
                sorgu.Parameters.AddWithValue("@MSM_SID_5", lblidsil.Text);
                int tamiregelensevkid1 = Convert.ToInt16(sorgu.ExecuteScalar());

                sorgu3.CommandText = "UPDATE MALZEMELER SET SEVKDURUMU=@SD, TAMIRDEPO_KONTROL=@TDK WHERE ID='" + malzemeidbul + "' ";

                sorgu3.Parameters.AddWithValue("@SD", 0);
                sorgu3.Parameters.AddWithValue("@TDK", 1);  // Malzeme sevk'e ekleninde TDK=false oluyor çünkü başka depoya gidecek ve Tamir depoda listelenmemesi lazım. Ama sevk'in içinden silinince TDK=true yapılıyor ki Tamir Depoda listelenmesi için
                sorgu3.ExecuteNonQuery();

                sorgu3.CommandText = "UPDATE MALZEME_TAMIR_ISLEM SET SEVK_KONTROL=@SK, SEVK_DURUM=@SDD WHERE M_ID='" + malzemeidbul + "' AND GELEN_SEVK_ID = '" + tamiregelensevkid1 + "' ";
                sorgu3.Parameters.AddWithValue("@SK", 0);
                sorgu3.Parameters.AddWithValue("@SDD", "0");

                sorgu3.ExecuteNonQuery();

                // ------   ***   ------ \\
                // ------   LOG   ------ \\
                // ------   ***   ------ \\
                string userid = Session["KULLANICI_ID"].ToString();
                sorgu5.CommandText = "INSERT INTO    MALZEMELER_LOG (M_ID, SEVK_ID, E_BOLGE, E_DEPO, ACIKLAMA, KAYIT_EDEN, KAYIT_TARIHI) " +
                    " VALUES(@MID, @SID, @EBOLGE, @EDEPO, @ACIKLAMA, @UI, getdate() ) ";
                sorgu5.Parameters.AddWithValue("@MID", malzemeidbul);
                sorgu5.Parameters.AddWithValue("@SID", lblidsevk.Text);
                sorgu5.Parameters.AddWithValue("@EBOLGE", bolgeid);
                sorgu5.Parameters.AddWithValue("@EDEPO", depoid);
                sorgu5.Parameters.AddWithValue("@ACIKLAMA", "Malzeme sevk sürecinden çıkartıldı.");
                sorgu5.Parameters.AddWithValue("@UI", userid);
                sorgu5.ExecuteNonQuery();
                // ------   ***   ------ \\
                // ------   LOG   ------ \\
                // ------   ***   ------ \\

                // ------   Malzemeyi Sevk sürecinden çıkar   ------ \\
                sorgu.CommandText = "DELETE FROM MALZEME_SEVK_MALZEMELER WHERE ID=@MSM_SID_6 ";
                sorgu.Parameters.AddWithValue("@MSM_SID_6", lblidsil.Text);
                sorgu.ExecuteNonQuery();
                // ------   Malzemeyi Sevk sürecinden çıkar   ------ \\
                conn.Close();

                ScriptManager.RegisterStartupScript(this, this.GetType(), System.Guid.NewGuid().ToString(), "ShowMessage('Malzeme başarıyla silinmiştir.','Tamam','sil');", true);
                ScriptManager.RegisterStartupScript(this, this.GetType(), System.Guid.NewGuid().ToString(), "YeniMesaj('Malzeme başarıyla silinmiştir.','Tamam');", true);

                btnsil.Enabled = false;
                btnsil.CssClass = "btn btn-danger disabled";
                modal_kapat("ModalSil");

                listele_sevkmalzemeleri_tamirsevkolustur();
                listele_bolge_sevkmalzemeekle();

                if (ConnectionState.Open == conn.State)
                    conn.Close();

                listele_sevk();
            }
        }

        protected void grid_sevk_listele_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName.Equals("sil"))
            {
                int index = Convert.ToInt32(e.CommandArgument) % grid_sevk_listele.PageSize;
                lblidsil.Text = grid_sevk_listele.DataKeys[index].Value.ToString();
                lblislem.Text = "sevk-sil";
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
                int index = Convert.ToInt32(e.CommandArgument) % grid_sevk_listele.PageSize;
                string id = grid_sevk_listele.DataKeys[index].Value.ToString();
                lblidcihaz.Text = id;

                listele_kaynakbolge();
                listele_kaynakdepo();
                listele_hedefbolge();
                listele_hedefdepo();

                lblsurecno.Text = HttpUtility.HtmlDecode(grid_sevk_listele.Rows[index].Cells[0].Text.Trim());
                comkaynakbolgesec.SelectedIndex = comkaynakbolgesec.Items.IndexOf(comkaynakbolgesec.Items.FindByText(HttpUtility.HtmlDecode(grid_sevk_listele.Rows[index].Cells[1].Text)));
                comkaynakdeposec.SelectedIndex = comkaynakdeposec.Items.IndexOf(comkaynakdeposec.Items.FindByText(HttpUtility.HtmlDecode(grid_sevk_listele.Rows[index].Cells[2].Text)));
                comhedefbolgesec.SelectedIndex = comhedefbolgesec.Items.IndexOf(comhedefbolgesec.Items.FindByText(HttpUtility.HtmlDecode(grid_sevk_listele.Rows[index].Cells[3].Text)));
                comhedefdeposec.SelectedIndex = comhedefdeposec.Items.IndexOf(comhedefdeposec.Items.FindByText(HttpUtility.HtmlDecode(grid_sevk_listele.Rows[index].Cells[4].Text)));

                SqlConnection conn2 = new SqlConnection(ConfigurationManager.ConnectionStrings["KabloHE"].ConnectionString);
                SqlCommand sorgu = new SqlCommand();
                sorgu.Connection = conn2;
                conn2.Open();
                sorgu.CommandText = "SELECT KARGO FROM MALZEME_SEVK WHERE ID= " + id;
                txtkargo.Text = Convert.ToString(sorgu.ExecuteScalar());
                sorgu.CommandText = "SELECT ACIKLAMA FROM MALZEME_SEVK WHERE ID= " + id;
                txtaciklama.Text = Convert.ToString(sorgu.ExecuteScalar());

                conn2.Close();

                int sayi = Convert.ToInt16(tkod.sql_calistir1("SELECT COUNT(SEVK_ID) FROM MALZEME_SEVK_MALZEMELER WHERE SEVK_ID=" + id));
                if (sayi >= 1)
                {
                    comkaynakbolgesec.Enabled = false;
                    comkaynakdeposec.Enabled = false;
                    comhedefbolgesec.Enabled = false;
                    comhedefdeposec.Enabled = false;
                }
                else
                {
                    comkaynakbolgesec.Enabled = true;
                    comkaynakdeposec.Enabled = true;
                    comhedefbolgesec.Enabled = true;
                    comhedefdeposec.Enabled = true;
                }
                chcgarantilimalzemekabul.Visible = true;
                chcgarantisizmalzemekabul.Visible = true;
                chcgarantilimalzemekabul.Checked = Convert.ToBoolean(tkod.sql_calistir_param("SELECT GARANTILI_MALZEME_KABUL FROM MALZEME_SEVK WHERE ID=@ID98", new SqlParameter("ID98", id)));
                chcgarantisizmalzemekabul.Checked = Convert.ToBoolean(tkod.sql_calistir_param("SELECT GARANTISIZ_MALZEME_KABUL FROM MALZEME_SEVK WHERE ID=@ID97", new SqlParameter("ID97", id)));

                btnsevkkaydet.Enabled = true;
                btnsevkkaydet.CssClass = "btn btn-success";
                btnislemler.Visible = false;

                lblmodalyenibaslik.Text = "Sevk Güncelleme";

                System.Text.StringBuilder sb = new System.Text.StringBuilder();
                sb.Append(@"<script type='text/javascript'>");
                sb.Append("$(\"#ModalSevkOlustur\").modal(\"show\");");
                sb.Append(@"</script>");
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(),
                           "ModalScript", sb.ToString(), false);
            }

            // **** Oluşturulan SEVK'e malzeme eklemek için 2 farklı modal kullanılıyor. 
            //** Oluşturulan Sevk'in KAYNAK DEPO'sunun TANIMI "TAMİR DEPO" yani "2" ise  ModalTamir_MalzemeEkle modalı değilse ModalMalzemeEkle modalı açılıyor.

            if (e.CommandName.Equals("islemler"))
            {
                int index = Convert.ToInt32(e.CommandArgument) % grid_sevk_listele.PageSize;
                btn_islemler(grid_sevk_listele.DataKeys[index][0].ToString(), index);
            }

            if (e.CommandName.Equals("detay"))
            {
                int index = Convert.ToInt32(e.CommandArgument) % grid_sevk_listele.PageSize;
                string id = grid_sevk_listele.DataKeys[index][0].ToString();
                lblidsevk.Text = id;

                lblkaynakbolge.Text = HttpUtility.HtmlDecode(grid_sevk_listele.Rows[index].Cells[1].Text.Trim());
                lblkaynakdepo.Text = HttpUtility.HtmlDecode(grid_sevk_listele.Rows[index].Cells[2].Text.Trim());
                lblhedefbolge.Text = HttpUtility.HtmlDecode(grid_sevk_listele.Rows[index].Cells[3].Text.Trim());
                lblshedefdepo.Text = HttpUtility.HtmlDecode(grid_sevk_listele.Rows[index].Cells[4].Text.Trim());

                System.Text.StringBuilder sb = new System.Text.StringBuilder();
                sb.Append(@"<script type='text/javascript'>");
                sb.Append("$(\"#ModalMalzemeEkle\").modal(\"show\");");
                sb.Append(@"</script>");
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(),
                           "ModalScript", sb.ToString(), false);
                lblmodalyenibaslik1.Text = "Sevk İşlemleri ";
                lblsevkdurumtext.Text = HttpUtility.HtmlDecode(grid_sevk_listele.Rows[index].Cells[5].Text.Trim());
                listele_sevkmalzemeleri();
                btnsevkiptal.Visible = false;
            }

            if (e.CommandName.Equals("hareket"))
            {
                int index = Convert.ToInt32(e.CommandArgument) % grid_sevk_listele.PageSize;
                string id = grid_sevk_listele.DataKeys[index][0].ToString();
                lblidsevk.Text = id;

                System.Text.StringBuilder sb = new System.Text.StringBuilder();
                sb.Append(@"<script type='text/javascript'>");
                sb.Append("$(\"#ModalHareket\").modal(\"show\");");
                sb.Append(@"</script>");
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(),
                           "ModalScript", sb.ToString(), false);
                lblmodalyenibaslik6.Text = "Sevk Hareket";

                listele_sevkhareket();
            }
        }

        void listele_sevkhareket()
        {
            SqlCommand cmd;
            string sql = "", sql1 = "";

            sql = " SELECT SLOG.ID, SLOG.SEVK_ID, BOLGE.BOLGE_ADI, DEPO.DEPO, " +
                " HBOLGE.BOLGE_ADI AS 'HBOLGE_ADI', " +
                " HDEPO.DEPO AS 'HDEPO', " +
                " SLOG.ACIKLAMA, MSD.S_DURUM,  " +
                " KULLANICI.ISIM + ' ' +CONVERT(NVARCHAR(10),SLOG.KAYIT_TARIHI,104) AS KAYIT, " +
                " SKULLANICI.ISIM + ' ' +CONVERT(NVARCHAR(10),SLOG.SEVK_TARIHI,104) AS SEVK, " +
                " TKULLANICI.ISIM + ' ' +CONVERT(NVARCHAR(10),SLOG.TESLIM_ALMA_TARIHI,104) AS TESLIM, " +
                " IKULLANICI.ISIM + ' ' +CONVERT(NVARCHAR(10),SLOG.IADE_TARIHI,104) AS IADE, " +
                " SIKULLANICI.ISIM + ' ' +CONVERT(NVARCHAR(10),SLOG.SILME_TARIHI,104) AS SILEN " +
                " FROM SEVK_LOG AS SLOG " +

                " LEFT JOIN MALZEME_SEVK_DURUM AS MSD ON MSD.ID=SLOG.SEVK_DURUM_ID" +
                " LEFT JOIN BOLGE AS HBOLGE ON HBOLGE.ID=SLOG.HEDEF_BOLGE_ID" +
                " LEFT JOIN DEPO AS HDEPO ON HDEPO.ID=SLOG.HEDEF_DEPO_ID" +
                " LEFT JOIN BOLGE ON BOLGE.ID=SLOG.KAYNAK_BOLGE_ID" +
                " LEFT JOIN DEPO ON DEPO.ID=SLOG.KAYNAK_DEPO_ID " +
                " LEFT JOIN KULLANICI ON KULLANICI.ID=SLOG.KAYIT_EDEN_ID " +
                " LEFT JOIN KULLANICI AS SKULLANICI ON SKULLANICI.ID=SLOG.SEVK_EDEN_ID " +
                " LEFT JOIN KULLANICI AS TKULLANICI ON TKULLANICI.ID=SLOG.TESLIM_ALAN_ID " +
                " LEFT JOIN KULLANICI AS IKULLANICI ON IKULLANICI.ID=SLOG.IADE_EDEN " +
                " LEFT JOIN KULLANICI AS SIKULLANICI ON SIKULLANICI.ID=SLOG.SILEN_ID " +

                " WHERE SLOG.SEVK_ID = '" + lblidsevk.Text + "'  ";

            sql = sql + sql1 + "   ORDER BY SLOG.ID DESC";

            cmd = new SqlCommand(sql, conn);

            conn.Open();

            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            grid_sevk_log.DataSource = dt;
            grid_sevk_log.DataBind();
            conn.Close();
            lblhareketsayisi.Text = grid_sevk_log.Rows.Count.ToString() + " " + "adet kayıt bulunmuştur.";
        }

        protected void grid_sevk_listele_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowIndex > -1)
            {   //kullanici_kaynak_hedef_bolge_yetki_kontrol();
                if (tkod.admin_yetki())
                    kbolge_sevk_k_bolge_yetkikontrol = 1;
                else
                    kbolge_sevk_k_bolge_yetkikontrol = Convert.ToInt16(tkod.sql_calistir_param("SELECT count(*)  FROM YETKILER INNER JOIN BOLGE ON BOLGE.ID=YETKILER.YETKI  WHERE YETKI_TIPI='BOLGE' AND  BOLGE.BOLGE_ADI='" + HttpUtility.HtmlEncode(e.Row.Cells[1].Text) + "' AND KULLANICI_ID=@ID", new SqlParameter("ID", tkod.Kul_ID())));

                Button butonislemler = (Button)e.Row.FindControl("btnislemler");
                Button butonguncelle = (Button)e.Row.FindControl("btnguncelle");
                Button butonsil = (Button)e.Row.FindControl("btnsil");
                Button butondetay = (Button)e.Row.FindControl("btndetay");
                Button butonhareket = (Button)e.Row.FindControl("btnhareket");

                butonguncelle.Visible = true;
                butonsil.Visible = true;

                int sayi = Convert.ToInt16(tkod.sql_calistir1("SELECT COUNT(SEVK_ID) FROM MALZEME_SEVK_MALZEMELER WHERE SEVK_ID=" + e.Row.Cells[0].Text));
                if (sayi >= 1)
                {
                    butonsil.Visible = false;
                }
                ////////
                if (e.Row.Cells[5].Text.IndexOf("YENİ") > -1)
                {
                    butonislemler.Visible = true;
                    butondetay.Visible = false;
                    butonhareket.Visible = false;
                }
                if (e.Row.Cells[5].Text.IndexOf("TRANSFER") > -1)
                {
                    butonislemler.Visible = true;
                    butondetay.Visible = false;
                    butonsil.Visible = false;
                    butonhareket.Visible = false;
                }

                if (e.Row.Cells[5].Text.IndexOf("TAMAMLANDI") > -1)
                {
                    butondetay.Visible = true;
                    butonguncelle.Visible = false;
                    butonsil.Visible = false;
                    butonislemler.Visible = false;
                    butonhareket.Visible = false;
                }
                if (e.Row.Cells[5].Text.IndexOf("İADE") > -1)
                {
                    butonislemler.Visible = true;
                    butonsil.Visible = false;
                    butondetay.Visible = false;
                    butonhareket.Visible = false;
                }
                if (e.Row.Cells[5].Text.IndexOf("İPTAL") > -1)
                {
                    butonhareket.Visible = true;
                    butonguncelle.Visible = false;
                    butonsil.Visible = false;
                    butonislemler.Visible = false;
                    butondetay.Visible = false;
                }
            }
        }

        void garantili_garantisiz_malzeme_kabul()
        {
            garantili_malzeme_kabul_kontrol = Convert.ToBoolean(tkod.sql_calistir_param("SELECT GARANTILI_MALZEME_KABUL FROM MALZEME_SEVK WHERE ID=@ID98", new SqlParameter("ID98", lblidsevk.Text)));
            garantisiz_malzeme_kabul_kontrol = Convert.ToBoolean(tkod.sql_calistir_param("SELECT GARANTISIZ_MALZEME_KABUL FROM MALZEME_SEVK WHERE ID=@ID97", new SqlParameter("ID97", lblidsevk.Text)));
        }

        void kullanici_kaynak_hedef_bolge_yetki_kontrol()
        {
            //Kullancının yetkili olduğu gblgelere göre sevk işlemleri sırasında görünecek butonların yetkileri için aşağıdaki kodlar kullanılıyor
            // listele_sevk, listele_sevk_tamir, ve bunların gird.rowdatabond^ları için
            string userid = Session["KULLANICI_ID"].ToString();
            lblkaynakbolgeid.Text = tkod.sql_calistir_param("SELECT ID FROM BOLGE WHERE BOLGE_ADI=@ID1", new SqlParameter("ID1", lblkaynakbolge.Text));
            lblhedefbolgeid.Text = tkod.sql_calistir_param("SELECT ID FROM BOLGE WHERE BOLGE_ADI=@ID2", new SqlParameter("ID2", lblhedefbolge.Text));
            kbolge_sevk_k_bolge_yetkikontrol = Convert.ToInt16(tkod.sql_calistir_param("SELECT count(*)  FROM YETKILER WHERE YETKI_TIPI='BOLGE' AND  YETKI='" + lblkaynakbolgeid.Text + "' AND KULLANICI_ID=@ID", new SqlParameter("ID", userid)));
            kbolge_sevk_h_bolge_yetkikontrol = Convert.ToInt16(tkod.sql_calistir_param("SELECT count(*)  FROM YETKILER WHERE YETKI_TIPI='BOLGE' AND  YETKI='" + lblhedefbolgeid.Text + "' AND KULLANICI_ID=@ID", new SqlParameter("ID", userid)));
        }

        // SEVK MALZEME Ekle >>>

        void listele_sevkmalzemeleri()
        {
            kullanici_kaynak_hedef_bolge_yetki_kontrol();
            garantili_garantisiz_malzeme_kabul();

            btnhareket.Visible = true;

            lblsevkdurum.Text = tkod.sql_calistir_param("SELECT SEVK_DURUM FROM MALZEME_SEVK WHERE ID=@ID", new SqlParameter("ID", lblidsevk.Text));
            
            chc_garantili.Checked = garantili_malzeme_kabul_kontrol;
            chc_garantisiz.Checked = garantisiz_malzeme_kabul_kontrol;

            if (lblsevkdurum.Text == "1" || lblsevkdurum.Text == "4") // Yeni veya İade
            {
                btnmalzemeara.Visible = false;
                btnsevket.Visible = false;
                btnsevkiptal.Visible = false;
                btnsevkteslimal.Visible = false;
                btnsevkiadeet.Visible = false;

                if (tkod.admin_yetki())
                {
                    btnsevket.Visible = true;
                    btnsevkiptal.Visible = true;
                    btnsevkmalzemeekle.Visible = true;
                    txtserinoara.Visible = true;
                }
                else
                if (kbolge_sevk_k_bolge_yetkikontrol > 0)
                {
                   
                    btnsevkmalzemeekle.Visible = true;
                    txtserinoara.Visible = true;
                    btnsevket.Visible = true;
                }
                else
                {
                    btnsevket.Visible = false;
                    btnsevkiptal.Visible = false;
                    btnsevkmalzemeekle.Visible = false;
                    txtserinoara.Visible = false;
                }
            }

            if (lblsevkdurum.Text == "2") //TRANSFER
            {
                btnsevket.Visible = false;
                btnsevkiptal.Visible = false;
                btnsevkmalzemeekle.Visible = false;
                btnsevkteslimal.Visible = true;
                btnsevkiadeet.Visible = true;

                if (tkod.admin_yetki())
                {
                    btnmalzemeara.Visible = true;
                    txtserinoara.Visible = true;
                }
                else
                if (kbolge_sevk_h_bolge_yetkikontrol > 0)
                {
                    txtserinoara.Visible = true;
                    btnmalzemeara.Visible = true;
                }
                else
                {
                    btnsevkteslimal.Visible = false;
                    btnsevkiadeet.Visible = false;
                    txtserinoara.Visible = false;
                    btnmalzemeara.Visible = false;
                }
            }

            if (lblsevkdurum.Text == "3") //TAMAMLANDI
            {
                btnsevket.Visible = false;
                btnsevkmalzemeekle.Visible = false;

                btnmalzemeara.Visible = true;
                txtserinoara.Visible = true;
                btnsevkteslimal.Visible = false;
                btnsevkiadeet.Visible = false;
            }

            if (lblsevkdurum.Text == "5") //İPTAL
            {
                btnsevket.Visible = false;
                btnsevkiptal.Visible = false;
                btnmalzemeara.Visible = true;
            }

            SqlCommand cmd;
            string sql = " ", sql1 = " ";

            sql = "SELECT MSM.ID, MM.MARKA, MMO.MODEL, M.SERI_NO, MD.DURUM, " +
                " CASE WHEN MSM.GARANTIBITIS > GETDATE() THEN 'Garantisi devam ediyor. ' +  Convert(NVARCHAR(10),MSM.GARANTIBITIS, 104) ELSE 'Garantisi Bitmiştir. ' + Convert(NVARCHAR(10),MSM.GARANTIBITIS, 104) END AS GARANTI, " +
                " KULLANICI.ISIM + ' ' +CONVERT(NVARCHAR(10),MSM.KAYIT_TARIHI,104) AS KAYIT " +
                " FROM MALZEME_SEVK_MALZEMELER AS MSM " +
                " LEFT JOIN MALZEME_DURUM AS MD ON MD.ID=MSM.DURUM" +
                " INNER JOIN MALZEMELER         AS M   ON M.ID = MSM.MALZEME_ID " +
                " INNER JOIN MALZEME_MARKAMODEL AS MM  ON M.MARKA=MM.ID " +
                " INNER JOIN MALZEME_MODEL      AS MMO ON M.MODEL=MMO.ID " +
                " LEFT JOIN KULLANICI                  ON KULLANICI.ID = MSM.KAYIT_EDEN " +
                " WHERE MSM.SEVK_ID= '" + lblidsevk.Text + "' ";

            sql = sql + sql1;
            cmd = new SqlCommand(sql, conn);
            conn.Open();
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            grid_sevk_malzeme_sec.DataSource = dt;
            grid_sevk_malzeme_sec.DataBind();
            conn.Close();
            chc_garantili.Visible = false;
            chc_garantisiz.Visible = false;

            if (grid_sevk_malzeme_sec.Rows.Count == 0)
                btnsevket.Visible = false;

            lblmalzemesayisi.Text = grid_sevk_malzeme_sec.Rows.Count.ToString() + " " + "adet kayıt bulunmuştur.";
        }

        protected void btnsevkmalzemeekle_Click(object sender, EventArgs e)
        {
            SqlCommand sorgu = new SqlCommand();
            sorgu.Connection = conn;
            SqlCommand sorgu1 = new SqlCommand();
            sorgu1.Connection = conn;
            SqlCommand sorgu4 = new SqlCommand();
            sorgu4.Connection = conn;
            SqlCommand sorgu5 = new SqlCommand();
            sorgu5.Connection = conn;

            conn.Open();
            sorgu.CommandText = "SELECT  CASE WHEN ISNULL(GARANTI_BITIS_TARIHI, CAST('12.31.2000' AS datetime)) > GETDATE() THEN 'true'" +
                " ELSE 'false' END AS QuantityText FROM MALZEMELER WHERE SERI_NO = '" + txtserinoara.Text + "' ";
            bool garanti = Convert.ToBoolean(sorgu.ExecuteScalar());

            if ((garanti && chc_garantili.Checked == true) || (chc_garantisiz.Checked == true && !garanti))
            {
                sorgu.CommandText = "SELECT COUNT(*) FROM MALZEMELER WHERE SERI_NO=@MSERI_NOO AND VARLIK_ID>0 ";
                sorgu.Parameters.AddWithValue("@MSERI_NOO", txtserinoara.Text);
                int sayi = Convert.ToInt16(sorgu.ExecuteScalar());

                sorgu1.CommandText = "SELECT COUNT(*) FROM MALZEMELER WHERE SERI_NO=@MSERI_NOO1 AND SEVKDURUMU = 1";
                sorgu1.Parameters.AddWithValue("@MSERI_NOO1", txtserinoara.Text);
                int sayi1 = Convert.ToInt16(sorgu1.ExecuteScalar());

                sorgu1.CommandText = "SELECT DURUM FROM MALZEMELER WHERE SERI_NO=@MSERI_NOO2 AND DURUM = 1"; //seri numarası bu olan malzemenin durumu arızalı mı?
                sorgu1.Parameters.AddWithValue("@MSERI_NOO2", txtserinoara.Text);
                int malzemedurum_tamirdepo = Convert.ToInt16(sorgu1.ExecuteScalar());

                sorgu4.CommandText = "SELECT BOLGE_ID FROM MALZEMELER WHERE SERI_NO = @MSERI_NOO3 ";
                sorgu4.Parameters.AddWithValue("@MSERI_NOO3", txtserinoara.Text);
                int bolgeid = Convert.ToInt16(sorgu4.ExecuteScalar());

                sorgu4.CommandText = "SELECT DEPO_ID FROM MALZEMELER WHERE SERI_NO = @MSERI_NOO4 ";
                sorgu4.Parameters.AddWithValue("@MSERI_NOO4", txtserinoara.Text);
                int depoid = Convert.ToInt16(sorgu4.ExecuteScalar());

                sorgu4.CommandText = "SELECT HEDEF_BOLGE FROM MALZEME_SEVK WHERE ID = @HBMS_ID ";
                sorgu4.Parameters.AddWithValue("@HBMS_ID", lblidsevk.Text);
                int sevkbolgeid = Convert.ToInt16(sorgu4.ExecuteScalar());

                sorgu4.CommandText = "SELECT HEDEF_DEPO FROM MALZEME_SEVK WHERE ID = @HDMS_ID ";
                sorgu4.Parameters.AddWithValue("@HDMS_ID", lblidsevk.Text);
                int sevkdepoid = Convert.ToInt16(sorgu4.ExecuteScalar());

                string sevk_bolge_kontrol = tkod.sql_calistir_param("SELECT KAYNAK_BOLGE FROM MALZEME_SEVK WHERE ID=@ID", new SqlParameter("ID", lblidsevk.Text));
                string malzeme_bolge_kontrol = tkod.sql_calistir_param("SELECT BOLGE_ID FROM MALZEMELER WHERE SERI_NO=@SERINO", new SqlParameter("SERINO", txtserinoara.Text));

                string sevk_depo_kontrol = tkod.sql_calistir_param("SELECT KAYNAK_DEPO FROM MALZEME_SEVK WHERE ID=@ID", new SqlParameter("ID", lblidsevk.Text));
                string malzeme_depo_kontrol = tkod.sql_calistir_param("SELECT DEPO_ID FROM MALZEMELER WHERE SERI_NO=@SERINO", new SqlParameter("SERINO", txtserinoara.Text));

                string seri_no_kontrol = tkod.sql_calistir_param("SELECT SERI_NO FROM MALZEMELER WHERE SERI_NO=@SERINO", new SqlParameter("SERINO", txtserinoara.Text));

                lblhedefdepokontrol.Text = tkod.sql_calistir_param("SELECT DEPOTANIMI_ID FROM DEPO WHERE DEPO=@DEPO", new SqlParameter("DEPO", lblshedefdepo.Text)); // HEDEF DEPO tanımına bakılıyor.

                if (malzemedurum_tamirdepo != 1 && lblhedefdepokontrol.Text == "2")
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), System.Guid.NewGuid().ToString(), "YeniMesaj('Tamir Depoya sadece durumu ARIZALI olan malzemeler Sevk edilebilir.','Hata');", true);
                    ScriptManager.RegisterStartupScript(this, this.GetType(), System.Guid.NewGuid().ToString(), "ShowMessage('Tamir Depoya sadece durumu ARIZALI olan malzemeler Sevk edilebilir.','Hata','yeni1');", true);
                }
                else
                {
                    if (sayi1 != 0)
                    {
                        ScriptManager.RegisterStartupScript(this, this.GetType(), System.Guid.NewGuid().ToString(), "YeniMesaj('Bu malzeme Sevk sürecinde.','Hata');", true);
                        ScriptManager.RegisterStartupScript(this, this.GetType(), System.Guid.NewGuid().ToString(), "ShowMessage('Bu malzeme Sevk sürecinde.','Hata','sevket');", true);
                        lblmalzemeid.Text = "";
                        lblmalzememarka.Text = "";
                        lblmalzememodel.Text = "";
                        lblmalzemedurum.Text = "";
                        lblmalzemesevkdurumu.Text = "";
                        txtserinoara.ToolTip = "Seri No";
                    }
                    else
                    {
                        if (sayi != 0)
                        {
                            ScriptManager.RegisterStartupScript(this, this.GetType(), System.Guid.NewGuid().ToString(), "YeniMesaj('Bu malzeme bir VARLIK a bağlı görünmektedir. Öncelikle VARLIK bağlantısının kesilmesi gerekmektedir.','Hata');", true);
                            ScriptManager.RegisterStartupScript(this, this.GetType(), System.Guid.NewGuid().ToString(), "ShowMessage('Bu malzeme bir VARLIK a bağlı görünmektedir. Öncelikle VARLIK bağlantısının kesilmesi gerekmektedir.','Hata','sevket');", true);
                            lblmalzemeid.Text = "";
                            lblmalzememarka.Text = "";
                            lblmalzememodel.Text = "";
                            lblmalzemedurum.Text = "";
                            lblmalzemesevkdurumu.Text = "";
                            txtserinoara.ToolTip = "Seri No";
                        }
                        else
                        {
                            if (seri_no_kontrol != txtserinoara.Text)
                            {
                                ScriptManager.RegisterStartupScript(this, this.GetType(), System.Guid.NewGuid().ToString(), "YeniMesaj('Malzeme Seri No hatalı.','Hata');", true);
                                ScriptManager.RegisterStartupScript(this, this.GetType(), System.Guid.NewGuid().ToString(), "ShowMessage('Malzeme Seri No hatalı.','Hata','sevket');", true);
                                lblmalzemeid.Text = "";
                                lblmalzememarka.Text = "";
                                lblmalzememodel.Text = "";
                                lblmalzemedurum.Text = "";
                                lblmalzemesevkdurumu.Text = "";
                                txtserinoara.ToolTip = "Seri No";
                            }
                            else
                            {
                                if (sevk_bolge_kontrol != malzeme_bolge_kontrol)
                                {
                                    ScriptManager.RegisterStartupScript(this, this.GetType(), System.Guid.NewGuid().ToString(), "YeniMesaj('Bu Malzemenin bulunduğu Bölge ile Sevk Kaynak Bölgesi aynı değil.','Hata');", true);
                                    ScriptManager.RegisterStartupScript(this, this.GetType(), System.Guid.NewGuid().ToString(), "ShowMessage('Bu Malzemenin bulunduğu Bölge ile Sevk Kaynak Bölgesi aynı değil.','Hata','sevket');", true);
                                    lblmalzemeid.Text = "";
                                    lblmalzememarka.Text = "";
                                    lblmalzememodel.Text = "";
                                    lblmalzemedurum.Text = "";
                                    lblmalzemesevkdurumu.Text = "";
                                    txtserinoara.ToolTip = "Seri No";
                                }
                                else
                                {
                                    if (sevk_depo_kontrol != malzeme_depo_kontrol)
                                    {
                                        ScriptManager.RegisterStartupScript(this, this.GetType(), System.Guid.NewGuid().ToString(), "YeniMesaj('Bu Malzemenin bulunduğu Depo ile Sevk Kaynak Deposu aynı değil.','Hata');", true);
                                        ScriptManager.RegisterStartupScript(this, this.GetType(), System.Guid.NewGuid().ToString(), "ShowMessage('Bu Malzemenin bulunduğu Depo ile Sevk Kaynak Deposu aynı değil.','Hata','sevket');", true);
                                        lblmalzemeid.Text = "";
                                        lblmalzememarka.Text = "";
                                        lblmalzememodel.Text = "";
                                        lblmalzemedurum.Text = "";
                                        lblmalzemesevkdurumu.Text = "";
                                        txtserinoara.ToolTip = "Seri No";
                                    }
                                    else
                                    {
                                        if (sayi == 0 || sayi1 == 0)
                                        {
                                            lblmalzemeid.Text = tkod.sql_calistir_param("SELECT ID, SEVKDURUMU FROM MALZEMELER WHERE SERI_NO=@SERINO", new SqlParameter("SERINO", txtserinoara.Text));
                                            lblmalzememarka.Text = tkod.sql_calistir_param("SELECT MALZEME_MARKAMODEL.MARKA FROM MALZEMELER AS M INNER JOIN MALZEME_MARKAMODEL ON M.MARKA=MALZEME_MARKAMODEL.ID  WHERE SERI_NO=@SERINO", new SqlParameter("SERINO", txtserinoara.Text));
                                            lblmalzememodel.Text = tkod.sql_calistir_param("SELECT MALZEME_MODEL.MODEL FROM MALZEMELER AS M INNER JOIN MALZEME_MODEL ON M.MODEL=MALZEME_MODEL.ID  WHERE SERI_NO=@SERINO", new SqlParameter("SERINO", txtserinoara.Text));
                                            lblmalzemedurum.Text = tkod.sql_calistir_param("SELECT DURUM FROM MALZEMELER WHERE SERI_NO=@SERINO", new SqlParameter("SERINO", txtserinoara.Text));
                                            lblmalzemesevkdurumu.Text = tkod.sql_calistir_param("SELECT SEVKDURUMU FROM MALZEMELER WHERE SERI_NO=@SERINO", new SqlParameter("SERINO", txtserinoara.Text));
                                            lblsevkdurum.Text = tkod.sql_calistir_param("SELECT SEVK_DURUM FROM MALZEME_SEVK WHERE ID=@ID", new SqlParameter("ID", lblidsevk.Text));
                                            lblgarantibitistarihi.Text = tkod.sql_calistir_param("SELECT GARANTI_BITIS_TARIHI FROM MALZEMELER WHERE SERI_NO=@SERINO", new SqlParameter("SERINO", txtserinoara.Text));

                                            string id = tkod.sql_calistir_param("SELECT ID, SEVKDURUMU FROM MALZEMELER WHERE SERI_NO=@SERINO", new SqlParameter("SERINO", txtserinoara.Text));
                                            string marka = tkod.sql_calistir_param("SELECT MMM.MARKA, MM.MODEL FROM MALZEMELER AS M INNER JOIN MALZEME_MARKAMODEL AS MMM ON M.MARKA=MMM.ID INNER JOIN MALZEME_MODEL AS MM ON M.MODEL=MM.ID  WHERE SERI_NO=@SERINO", new SqlParameter("SERINO", txtserinoara.Text));
                                            string model = tkod.sql_calistir_param("SELECT MALZEME_MODEL.MODEL FROM MALZEMELER AS M INNER JOIN MALZEME_MODEL ON M.MODEL=MALZEME_MODEL.ID  WHERE SERI_NO=@SERINO", new SqlParameter("SERINO", txtserinoara.Text));
                                            string durum = tkod.sql_calistir_param("SELECT DURUM FROM MALZEMELER WHERE SERI_NO=@SERINO", new SqlParameter("SERINO", txtserinoara.Text));
                                            txtserinoara.ToolTip = "ID : " + id + Environment.NewLine + "Marka : " + marka + Environment.NewLine + "Model : " + model + Environment.NewLine + "Durum : " + durum;

                                            try
                                            {
                                                SqlCommand sorgu2 = new SqlCommand();
                                                sorgu2.Connection = conn;
                                                string userid = Session["KULLANICI_ID"].ToString();
                                                sorgu2.CommandText = "INSERT INTO    MALZEME_SEVK_MALZEMELER (MALZEME_ID, SEVK_ID, DURUM, KAYIT_EDEN, GARANTIBITIS, KAYIT_TARIHI)  VALUES(@MI, @SI, @DU,  @UI, @GBT, getdate() ) ";
                                                sorgu2.Parameters.AddWithValue("@MI", lblmalzemeid.Text);
                                                sorgu2.Parameters.AddWithValue("@SI", lblidsevk.Text);
                                                sorgu2.Parameters.AddWithValue("@DU", lblmalzemedurum.Text);
                                                sorgu2.Parameters.AddWithValue("@GBT", Convert.ToDateTime(lblgarantibitistarihi.Text));
                                                sorgu2.Parameters.AddWithValue("@UI", userid);
                                                sorgu2.ExecuteNonQuery();

                                                SqlCommand sorgu3 = new SqlCommand();
                                                sorgu3.Connection = conn;
                                                sorgu3.CommandText = "UPDATE MALZEMELER SET SEVKDURUMU=@SD, GUNCELLEME_DURUMU=@GD WHERE SERI_NO=@MSNO9 ";
                                                sorgu3.Parameters.AddWithValue("@SD", true);
                                                sorgu3.Parameters.AddWithValue("@GD", true);
                                                sorgu3.Parameters.AddWithValue("@MSNO9", txtserinoara.Text);
                                                sorgu3.ExecuteNonQuery();

                                                txtserinoara.Text = "";

                                                // ------   ***   ------ \\
                                                // ------   LOG   ------ \\
                                                // ------   ***   ------ \\
                                                sorgu5.CommandText = "INSERT INTO    MALZEMELER_LOG (M_ID, SEVK_ID, E_BOLGE, E_DEPO, ACIKLAMA, KAYIT_EDEN, KAYIT_TARIHI) " +
                                                    " VALUES(@MID, @SID, @EBOLGE, @EDEPO, @ACIKLAMA, @UI, getdate() ) ";
                                                sorgu5.Parameters.AddWithValue("@MID", lblmalzemeid.Text);
                                                sorgu5.Parameters.AddWithValue("@SID", lblidsevk.Text);
                                                sorgu5.Parameters.AddWithValue("@EBOLGE", bolgeid);
                                                sorgu5.Parameters.AddWithValue("@EDEPO", depoid);

                                                sorgu5.Parameters.AddWithValue("@ACIKLAMA", "Malzeme sevk sürecine girdi.");
                                                sorgu5.Parameters.AddWithValue("@UI", userid);
                                                sorgu5.ExecuteNonQuery();
                                                // ------   ***   ------ \\
                                                // ------   LOG   ------ \\
                                                // ------   ***   ------ \\

                                                ScriptManager.RegisterStartupScript(this, this.GetType(), System.Guid.NewGuid().ToString(), "YeniMesaj('Kayıt başarılı.','Tamam');", true);
                                                ScriptManager.RegisterStartupScript(this, this.GetType(), System.Guid.NewGuid().ToString(), "ShowMessage('Kayıt başarılı.','Tamam','sevket');", true);
                                            }
                                            catch (Exception ex)
                                            {
                                                ScriptManager.RegisterStartupScript(this, this.GetType(), System.Guid.NewGuid().ToString(), "YeniMesaj('Beklenmeyen bir hata oluştu.','Hata');", true);
                                                ScriptManager.RegisterStartupScript(this, this.GetType(), System.Guid.NewGuid().ToString(), "ShowMessage('Hata oluştu :'" + ex.Message.Replace("'", "") + ",'Hata','sevket');", true);
                                            }

                                            if (ConnectionState.Open == conn.State)
                                                conn.Close();
                                            listele_sevkmalzemeleri();
                                            listele_sevk();
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), System.Guid.NewGuid().ToString(), "YeniMesaj('Malzemenin garanti durumuna göre Deponun kabul edip edemeyeceği malzemenin karşılaştırılması gerekmektedir. Özel durumlar için Hedef Depo yetkilisi ile iletişim kurulmalı.','Hata');", true);
                ScriptManager.RegisterStartupScript(this, this.GetType(), System.Guid.NewGuid().ToString(), "ShowMessage('Malzemenin garanti durumuna göre Deponun kabul edip edemeyeceği malzemenin karşılaştırılması gerekmektedir. Özel durumlar için Hedef Depo yetkilisi ile iletişim kurulmalı.','Hata','sevket');", true);
            }
        }

        protected void grid_sevk_malzeme_sec_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName.Equals("sil"))
            {
                int index = Convert.ToInt32(e.CommandArgument);
                lblidsil.Text = grid_sevk_malzeme_sec.DataKeys[index].Value.ToString();
                lblislem.Text = "sevk-malzeme-sil";
                btnsil.Enabled = true;
                btnsil.CssClass = "btn btn-danger ";

                System.Text.StringBuilder sb = new System.Text.StringBuilder();
                sb.Append(@"<script type='text/javascript'>");
                sb.Append("$(\"#ModalSil\").modal(\"show\");");
                sb.Append(@"</script>");
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(),
                           "ModalScript", sb.ToString(), false);
            }

            if (e.CommandName.Equals("ariza"))
            {
                int index = Convert.ToInt32(e.CommandArgument);
                lblidsevk_ariza.Text = grid_sevk_malzeme_sec.DataKeys[index].Value.ToString();

                lblidcihaz_ariza.Text = tkod.sql_calistir_param("SELECT MALZEME_ID FROM MALZEME_SEVK_MALZEMELER WHERE ID=@ID", new SqlParameter("ID", lblidsevk_ariza.Text));

                lblmodalyenibaslik5.Text = "Arıza Geçmişi";

                modal_ac("ModalAriza");
                modal_kapat("ModalMalzemeEkle");
                lblmodalarizayonlendir.Text = "ModalMalzemeEkle";
                listele_ariza();
            }
        }

        //SEVK ET buton
        // ******* Hedef deponun depo tanımı "Tamir Depo" ise 
        // 1- MALZEME_SEVK set SEVK_DURUM = 2 yap. 
        // ****** 2- Sevk'e eklenen malzemelerin; TAMIRDEPO_KONTROL = true yap.  Bu sayede malzemeler Tamir İşlemleri sayfasına görünecek.  *****
        // ****** 3- Sevk'e eklenen malzemelerin; DEPO_ID'sini Tamir depo yap ONCEKI_DEPO_ID'sini eski deposu yap.

        // Hedef deponun depo tanımı Tamir depo DEĞİLSE; 
        // 1- MALZEME_SEVK set SEVK_DURUM = 2 yap. 
        // 2- Sevk'e eklenen malzemelerin; Bölge ve Depolarını sevk'in bölge ve deposu ile dğeiştir. Malzemenin eski bölge ve depo bilgisini de kaydet.

        protected void btnsevket_Click(object sender, EventArgs e)
        {

            if (lbldepotanimkontrol.Text == "2" || lbldepotanimkontrol.Text == "4")  //Eğer HEDEF DEPO tamını Tamir Depo ise 
            {
                if (grid_sevk_malzeme_sec.Rows.Count < 1)
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), System.Guid.NewGuid().ToString(), "YeniMesaj('Malzeme seçimi yapılması gerekmektedir.','Hata');", true);
                    ScriptManager.RegisterStartupScript(this, this.GetType(), System.Guid.NewGuid().ToString(), "ShowMessage('Malzeme seçimi yapılması gerekmektedir.','Hata','sevket');", true);
                }
                else
                {
                    SqlCommand sorgu = new SqlCommand();
                    sorgu.Connection = conn;
                    SqlCommand sorgu1 = new SqlCommand();
                    sorgu1.Connection = conn;
                    SqlCommand sorgu5 = new SqlCommand();
                    sorgu5.Connection = conn;
                    conn.Open();

                    try
                    {
                        string userid = Session["KULLANICI_ID"].ToString();
                        sorgu.CommandText = "UPDATE MALZEME_SEVK SET SEVK_DURUM=@SD, SEVK_EDEN=@UI, SEVK_TARIHI=getdate()  WHERE ID=@MSID8 ";
                        sorgu.Parameters.AddWithValue("@SD", 2);
                        sorgu.Parameters.AddWithValue("@UI", userid);
                        sorgu.Parameters.AddWithValue("@MSID8", lblidsevk.Text);
                        sorgu.ExecuteNonQuery();

                        string malzemeserino;

                        for (int i = 0; i < grid_sevk_malzeme_sec.Rows.Count; i++)
                        {
                            malzemeserino = HttpUtility.HtmlDecode(grid_sevk_malzeme_sec.Rows[i].Cells[3].Text).Trim();
                            sorgu.CommandText = "SELECT ISNULL(TAMIRDEPO_KONTROL,'FALSE') FROM MALZEMELER WHERE SERI_NO = '" + malzemeserino + "' ";
                            int tamirdepo_kontrol = Convert.ToInt16(sorgu.ExecuteScalar());

                            sorgu.CommandText = "SELECT BOLGE_ID FROM MALZEMELER WHERE SERI_NO = '" + malzemeserino + "' ";
                            int bolgeid = Convert.ToInt16(sorgu.ExecuteScalar());

                            sorgu.CommandText = "SELECT DEPO_ID FROM MALZEMELER WHERE SERI_NO = '" + malzemeserino + "' ";
                            int depoid = Convert.ToInt16(sorgu.ExecuteScalar());

                            sorgu.CommandText = "SELECT HEDEF_BOLGE FROM MALZEME_SEVK WHERE ID = @HB_MS_ID99 ";
                            sorgu.Parameters.Clear();
                            sorgu.Parameters.AddWithValue("@HB_MS_ID99", lblidsevk.Text);
                            int sevkbolgeid = Convert.ToInt16(sorgu.ExecuteScalar());

                            sorgu.CommandText = "SELECT HEDEF_DEPO FROM MALZEME_SEVK WHERE ID = @HB_MS_ID1 ";
                            sorgu.Parameters.Clear();
                            sorgu.Parameters.AddWithValue("@HB_MS_ID1", lblidsevk.Text);
                            int sevkdepoid = Convert.ToInt16(sorgu.ExecuteScalar());

                            sorgu.CommandText = "SELECT MALZEME_TIP FROM MALZEMELER WHERE SERI_NO = '" + malzemeserino + "' ";
                            int malzemetip = Convert.ToInt16(sorgu.ExecuteScalar());

                            sorgu.CommandText = "SELECT MALZEME_TUR FROM MALZEMELER WHERE SERI_NO = '" + malzemeserino + "' ";
                            int malzemetur = Convert.ToInt16(sorgu.ExecuteScalar());

                            sorgu.CommandText = "SELECT MARKA FROM MALZEMELER WHERE SERI_NO = '" + malzemeserino + "' ";
                            int malzememarka = Convert.ToInt16(sorgu.ExecuteScalar());

                            sorgu.CommandText = "SELECT MODEL FROM MALZEMELER WHERE SERI_NO = '" + malzemeserino + "' ";
                            int malzememodel = Convert.ToInt16(sorgu.ExecuteScalar());

                            sorgu.CommandText = "SELECT DURUM FROM MALZEMELER WHERE SERI_NO = '" + malzemeserino + "' ";
                            int malzemedurum = Convert.ToInt16(sorgu.ExecuteScalar());

                            sorgu1.CommandText = "UPDATE MALZEMELER SET TAMIRDEPO_KONTROL=@TDK, DEPO_ID=@DID, ONCEKI_DEPO_ID=@ODID WHERE SERI_NO='" + malzemeserino + "'  ";
                            sorgu1.Parameters.Clear();
                            sorgu1.Parameters.AddWithValue("@DID", sevkdepoid);
                            sorgu1.Parameters.AddWithValue("@ODID", depoid);
                            sorgu1.Parameters.AddWithValue("@TDK", true); //Burada olursa eklenen malzemeler SEVK ET butonuna basılında daha teslim alınmadan TAMİR İŞLEMLERİ sayfasında görünüyor. >> Tamir Depo sayfasına teslim alma tarihi > 0'dan ekledim. Böylelikle teslim Malzeme_Tamir_Islem tablosunda teslim alma tarihi 0'dan büyük olanlar ve TDK true olanlar listeleniyor.   
                            sorgu1.ExecuteNonQuery();

                            grid_sevk_malzeme_sec.Rows[i].BackColor = Color.YellowGreen;
                            lblmalzemeid.Text = tkod.sql_calistir_param("SELECT ID FROM MALZEMELER WHERE SERI_NO=@SERINO", new SqlParameter("SERINO", malzemeserino));

                            // ------   MALZEME VE SEVK BİLGİLERİNİ MALZEME_TAMIR_ISLEM TABLOSUNA YAZMA   ------ \\
                            sorgu1.CommandText = "INSERT INTO    MALZEME_TAMIR_ISLEM (M_ID, GELEN_SEVK_ID, BOLGE_ID, ILK_DURUM, M_TIPI," +
                                                                                    " M_TURU, M_MARKA, M_MODEL, M_SERINO, TAMIR_EDILECEK_DEPO, SEVK_EDEN, SEVK_TARIHI) " +
                                                                                    " VALUES(@MID, @GSID, @BLG, @DRM, @MTIP, @MTUR, @MRK, @MDL, @SN, @TED, @UI, getdate() ) ";
                            sorgu1.Parameters.Clear();
                            sorgu1.Parameters.AddWithValue("@MID", lblmalzemeid.Text);
                            sorgu1.Parameters.AddWithValue("@GSID", lblidsevk.Text); 
                            sorgu1.Parameters.AddWithValue("@BLG", bolgeid);
                            sorgu1.Parameters.AddWithValue("@DRM", malzemedurum);
                            sorgu1.Parameters.AddWithValue("@MTIP", malzemetip);
                            sorgu1.Parameters.AddWithValue("@MTUR", malzemetur);
                            sorgu1.Parameters.AddWithValue("@MRK", malzememarka);
                            sorgu1.Parameters.AddWithValue("@MDL", malzememodel);
                            sorgu1.Parameters.AddWithValue("@SN", malzemeserino);
                            sorgu1.Parameters.AddWithValue("@TED", lblhedefdepoid.Text);
                            sorgu1.Parameters.AddWithValue("@UI", userid);
                            sorgu1.ExecuteNonQuery();
                            // ------   MALZEME VE SEVK BİLGİLERİNİ MALZEME_TAMIR_ISLEM TABLOSUNA YAZMA   ------ \\

                            // ------   ***   ------ \\
                            // ------   LOG   ------ \\
                            // ------   ***   ------ \\
                            lblmalzemeid.Text = tkod.sql_calistir1("SELECT ID FROM MALZEMELER WHERE SERI_NO= '" + malzemeserino + "' ");
                            sorgu5.CommandText = "INSERT INTO    MALZEMELER_LOG (M_ID, SEVK_ID, E_BOLGE, E_DEPO, Y_BOLGE, Y_DEPO, ACIKLAMA, KAYIT_EDEN, KAYIT_TARIHI) " +
                                " VALUES(@MID1, @SID, @EBOLGE, @EDEPO, @YBOLGE, @YDEPO, @ACIKLAMA, @UI, getdate() ) ";
                            sorgu5.Parameters.Clear();
                            sorgu5.Parameters.AddWithValue("@MID1", lblmalzemeid.Text);
                            sorgu5.Parameters.AddWithValue("@SID", lblidsevk.Text);
                            sorgu5.Parameters.AddWithValue("@EBOLGE", bolgeid);
                            sorgu5.Parameters.AddWithValue("@EDEPO", depoid);
                            sorgu5.Parameters.AddWithValue("@YBOLGE", "0");
                            sorgu5.Parameters.AddWithValue("@YDEPO", sevkdepoid);
                            sorgu5.Parameters.AddWithValue("@ACIKLAMA", "Malzeme TAMİR depoya sevk edildi.");
                            sorgu5.Parameters.AddWithValue("@UI", userid);
                            sorgu5.ExecuteNonQuery();
                            // ------   ***   ------ \\
                            // ------   LOG   ------ \\
                            // ------   ***   ------ \\
                        }
                        // ------   ***   ------ \\
                        // ------   LOG   ------ \\
                        // ------   ***   ------ \\
                        sorgu.CommandText = "INSERT INTO    SEVK_LOG (SEVK_ID, SEVK_DURUM_ID, SEVK_EDEN_ID, SEVK_TARIHI, ACIKLAMA) " +
                            " VALUES(@SID_LOG, @SD_LOG, @SE_LOG, getdate(), @ACKLM_LOG ) ";
                        sorgu.Parameters.Clear();
                        sorgu.Parameters.AddWithValue("@SID_LOG", lblidsevk.Text);
                        sorgu.Parameters.AddWithValue("@SD_LOG", 2);
                        sorgu.Parameters.AddWithValue("@SE_LOG", userid);
                        sorgu.Parameters.AddWithValue("@ACKLM_LOG", "Temir Depoya Sevk süreci başlatıldı.");
                        sorgu.ExecuteNonQuery();
                        // ------   ***   ------ \\
                        // ------   LOG   ------ \\
                        // ------   ***   ------ \\

                        conn.Close();

                        btnsevket.Visible = false;
                        btnsevkiptal.Visible = false;

                        btnsevkmalzemeekle.Visible = false;

                        lblsevkdurum.Text = "2";

                        listele_sevkmalzemeleri();
                        listele_sevk();

                        ScriptManager.RegisterStartupScript(this, this.GetType(), System.Guid.NewGuid().ToString(), "YeniMesaj('Tamir Depo Sevk süreci başlatıldı.','Tamam');", true);
                        ScriptManager.RegisterStartupScript(this, this.GetType(), System.Guid.NewGuid().ToString(), "ShowMessage('Tamir Depo Sevk süreci başlatıldı.','Tamam','sevket');", true);
                        modal_kapat("ModalMalzemeEkle");
                    }
                    catch (Exception ex)
                    {
                        ScriptManager.RegisterStartupScript(this, this.GetType(), System.Guid.NewGuid().ToString(), "YeniMesaj('Beklenmeyen bir hata oluştu.','Hata');", true);
                        ScriptManager.RegisterStartupScript(this, this.GetType(), System.Guid.NewGuid().ToString(), "ShowMessage('Hata oluştu :'" + ex.Message.Replace("'", "") + ",'Hata','sevket');", true);
                    }
                    finally
                    {
                        if (ConnectionState.Open == conn.State)
                            conn.Close();
                    }
                }
            }

            else
            {
                if (grid_sevk_malzeme_sec.Rows.Count < 1)
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), System.Guid.NewGuid().ToString(), "YeniMesaj('Malzeme seçimi yapılması gerekmektedir.','Hata');", true);
                    ScriptManager.RegisterStartupScript(this, this.GetType(), System.Guid.NewGuid().ToString(), "ShowMessage('Malzeme seçimi yapılması gerekmektedir.','Hata','sevket');", true);
                }
                else
                {
                    SqlCommand sorgu = new SqlCommand();
                    sorgu.Connection = conn;
                    SqlCommand sorgu1 = new SqlCommand();
                    sorgu1.Connection = conn;
                    SqlCommand sorgu5 = new SqlCommand();
                    sorgu5.Connection = conn;
                    conn.Open();

                    try
                    {
                        string userid = Session["KULLANICI_ID"].ToString();
                        sorgu.CommandText = "UPDATE MALZEME_SEVK SET SEVK_DURUM=@SD, SEVK_EDEN=@UI, SEVK_TARIHI=getdate()  WHERE ID=@MSID_U8 ";
                        sorgu.Parameters.AddWithValue("@SD", 2);
                        sorgu.Parameters.AddWithValue("@UI", userid);
                        sorgu.Parameters.AddWithValue("@MSID_U8", lblidsevk.Text);
                        sorgu.ExecuteNonQuery();

                        string malzemeserino;

                        for (int i = 0; i < grid_sevk_malzeme_sec.Rows.Count; i++)
                        {
                            malzemeserino = HttpUtility.HtmlDecode(grid_sevk_malzeme_sec.Rows[i].Cells[3].Text).Trim();
                            sorgu.CommandText = "SELECT BOLGE_ID FROM MALZEMELER WHERE SERI_NO = '" + malzemeserino + "' ";
                            int bolgeid = Convert.ToInt16(sorgu.ExecuteScalar());

                            sorgu.CommandText = "SELECT DEPO_ID FROM MALZEMELER WHERE SERI_NO = '" + malzemeserino + "' ";
                            int depoid = Convert.ToInt16(sorgu.ExecuteScalar());

                            sorgu.CommandText = "SELECT HEDEF_BOLGE FROM MALZEME_SEVK WHERE ID = '" + lblidsevk.Text + "' ";
                            int sevkbolgeid = Convert.ToInt16(sorgu.ExecuteScalar());

                            sorgu.CommandText = "SELECT HEDEF_DEPO FROM MALZEME_SEVK WHERE ID = '" + lblidsevk.Text + "' ";
                            int sevkdepoid = Convert.ToInt16(sorgu.ExecuteScalar());

                            sorgu1.CommandText = "UPDATE MALZEMELER SET BOLGE_ID=@BI, DEPO_ID=@DI, ONCEKI_BOLGE_ID=@OBI, ONCEKI_DEPO_ID=@ODI  WHERE SERI_NO='" + malzemeserino + "'  ";
                            sorgu1.Parameters.Clear();
                            sorgu1.Parameters.AddWithValue("@DI", sevkdepoid);
                            sorgu1.Parameters.AddWithValue("@BI", sevkbolgeid);
                            sorgu1.Parameters.AddWithValue("@OBI", bolgeid);
                            sorgu1.Parameters.AddWithValue("@ODI", depoid);

                            grid_sevk_malzeme_sec.Rows[i].BackColor = Color.YellowGreen;

                            sorgu1.ExecuteNonQuery();

                            // ------   ***   ------ \\
                            // ------   LOG   ------ \\
                            // ------   ***   ------ \\
                            lblmalzemeid.Text = tkod.sql_calistir1("SELECT ID FROM MALZEMELER WHERE SERI_NO= '" + malzemeserino + "' ");
                            sorgu5.CommandText = "INSERT INTO    MALZEMELER_LOG (M_ID, SEVK_ID, E_BOLGE, E_DEPO, Y_BOLGE, Y_DEPO, ACIKLAMA, KAYIT_EDEN, KAYIT_TARIHI) " +
                                " VALUES(@MID, @SID, @EBOLGE, @EDEPO, @YBOLGE, @YDEPO, @ACIKLAMA, @UI, getdate() ) ";
                            sorgu5.Parameters.Clear();
                            sorgu5.Parameters.AddWithValue("@MID", lblmalzemeid.Text);
                            sorgu5.Parameters.AddWithValue("@SID", lblidsevk.Text);
                            sorgu5.Parameters.AddWithValue("@EBOLGE", bolgeid);
                            sorgu5.Parameters.AddWithValue("@EDEPO", depoid);
                            sorgu5.Parameters.AddWithValue("@YBOLGE", sevkbolgeid);
                            sorgu5.Parameters.AddWithValue("@YDEPO", sevkdepoid);
                            sorgu5.Parameters.AddWithValue("@ACIKLAMA", "Malzeme sevk edildi.");
                            //sorgu5.Parameters.AddWithValue("@EDURUM", lblmevcutdurum_durum.Text);
                            //sorgu5.Parameters.AddWithValue("@YDURUM", comyenidurum_durum.SelectedValue);
                            //sorgu5.Parameters.AddWithValue("@GRKC", txtgerekce_durum.Text);
                            sorgu5.Parameters.AddWithValue("@UI", userid);
                            sorgu5.ExecuteNonQuery();
                            // ------   ***   ------ \\
                            // ------   LOG   ------ \\
                            // ------   ***   ------ \\
                        }
                        // ------   ***   ------ \\
                        // ------   LOG   ------ \\
                        // ------   ***   ------ \\
                        sorgu.CommandText = "INSERT INTO    SEVK_LOG (SEVK_ID, SEVK_DURUM_ID, SEVK_EDEN_ID, SEVK_TARIHI, ACIKLAMA) " +
                            " VALUES(@SID_LOG, @SD_LOG, @SE_LOG, getdate(), @ACKLM_LOG ) ";
                        sorgu.Parameters.Clear();
                        sorgu.Parameters.AddWithValue("@SID_LOG", lblidsevk.Text);
                        sorgu.Parameters.AddWithValue("@SD_LOG", 2);
                        sorgu.Parameters.AddWithValue("@SE_LOG", userid);
                        sorgu.Parameters.AddWithValue("@ACKLM_LOG", "Sevk süreci başlatıldı.");
                        sorgu.ExecuteNonQuery();
                        // ------   ***   ------ \\
                        // ------   LOG   ------ \\
                        // ------   ***   ------ \\
                        conn.Close();

                        btnsevket.Visible = false;
                        btnsevkiptal.Visible = false;

                        lblsevkdurum.Text = "2";

                        listele_sevkmalzemeleri();
                        listele_sevk();

                        modal_kapat("ModalMalzemeEkle");
                        ScriptManager.RegisterStartupScript(this, this.GetType(), System.Guid.NewGuid().ToString(), "YeniMesaj('Sevk süreci başlatıldı.','Tamam');", true);
                        ScriptManager.RegisterStartupScript(this, this.GetType(), System.Guid.NewGuid().ToString(), "ShowMessage('Sevk süreci başlatıldı.','Tamam','sevket');", true);
                    }
                    catch (Exception ex)
                    {
                        ScriptManager.RegisterStartupScript(this, this.GetType(), System.Guid.NewGuid().ToString(), "YeniMesaj('Beklenmeyen bir hata oluştu.','Hata');", true);
                        ScriptManager.RegisterStartupScript(this, this.GetType(), System.Guid.NewGuid().ToString(), "ShowMessage('Hata oluştu :'" + ex.Message.Replace("'", "") + ",'Hata','sevket');", true);
                    }
                    finally
                    {
                        if (ConnectionState.Open == conn.State)
                            conn.Close();
                    }
                }
            }
        }

        protected void grid_sevk_malzeme_sec_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowIndex > -1)
            {
                Button butonmalzemesil = (Button)e.Row.FindControl("btnsil");
                butonmalzemesil.Visible = true;

                if (tkod.ConvertToIntParse(lblsevkdurum.Text, 0) == 1 || tkod.ConvertToIntParse(lblsevkdurum.Text, 0) == 4)
                {
                    if (kbolge_sevk_k_bolge_yetkikontrol > 0)
                        butonmalzemesil.Visible = true;
                    else
                        butonmalzemesil.Visible = false;

                    if (tkod.admin_yetki())
                        butonmalzemesil.Visible = true;
                }
                else
                {
                    butonmalzemesil.Visible = false;
                }
            }
        }

        protected void btnsevkteslimal_Click(object sender, EventArgs e)
        {
            SqlCommand sorgu = new SqlCommand();
            sorgu.Connection = conn;
            SqlCommand sorgu5 = new SqlCommand();
            sorgu5.Connection = conn;
            conn.Open();

            try
            {
                string userid = Session["KULLANICI_ID"].ToString();
                sorgu.CommandText = "UPDATE MALZEME_SEVK SET SEVK_DURUM=@SD, TESLIM_ALAN=@UI, TESLIM_ALMA_TARIHI=getdate() WHERE ID=@MS_U_8 ";
                sorgu.Parameters.AddWithValue("@SD", 3);
                sorgu.Parameters.AddWithValue("@UI", userid);
                sorgu.Parameters.AddWithValue("@MS_U_8", lblidsevk.Text);
                sorgu.ExecuteNonQuery();
                string malzemeserinoo;

                for (int i = 0; i < grid_sevk_malzeme_sec.Rows.Count; i++)
                {
                    malzemeserinoo = HttpUtility.HtmlDecode(grid_sevk_malzeme_sec.Rows[i].Cells[3].Text).Trim();

                    // ------   MALZEME_TAMIR_ISLEM TABLOSUNA SEVK'İ TESLİM ALAN VE SEVK TARIHI BİLGİSİ YAZMA   ------ \\
                    if (lbldepotanimkontrol.Text == "2" || lbldepotanimkontrol.Text == "4")  //Eğer HEDEF DEPO tamını Tamir Depo ise 
                    {
                        sorgu.CommandText = "UPDATE MALZEME_TAMIR_ISLEM SET TESLIM_ALAN=@UI88, TESLIM_ALMA_TARIHI=getdate() WHERE M_SERINO=@MTI_S_NO  and GELEN_SEVK_ID=@MTI_GSID ";
                        sorgu.Parameters.Clear();
                        sorgu.Parameters.AddWithValue("@UI88", userid);
                        sorgu.Parameters.AddWithValue("@MTI_S_NO", malzemeserinoo);
                        sorgu.Parameters.AddWithValue("@MTI_GSID", lblidsevk.Text);
                        sorgu.ExecuteNonQuery();
                        // ------   MALZEME_TAMIR_ISLEM TABLOSUNA SEVK'İ TESLİM ALAN VE SEVK TARIHI BİLGİSİ YAZMA   ------ \\
                    }
                    else
                    {
                        sorgu.CommandText = "SELECT BOLGE_ID FROM MALZEMELER WHERE SERI_NO = '" + malzemeserinoo + "' ";
                        int bolgeid = Convert.ToInt16(sorgu.ExecuteScalar());

                        sorgu.CommandText = "SELECT DEPO_ID FROM MALZEMELER WHERE SERI_NO = '" + malzemeserinoo + "' ";
                        int depoid = Convert.ToInt16(sorgu.ExecuteScalar());

                        sorgu.CommandText = "SELECT ONCEKI_DEPO_ID FROM MALZEMELER WHERE SERI_NO = '" + malzemeserinoo + "' ";
                        int eskidepoid = Convert.ToInt16(sorgu.ExecuteScalar());

                        sorgu.CommandText = "UPDATE MALZEMELER SET SEVKDURUMU=@SD WHERE SERI_NO='" + malzemeserinoo + "'  ";
                        sorgu.Parameters.Clear();
                        sorgu.Parameters.AddWithValue("@SD", false);

                        grid_sevk_malzeme_sec.Rows[i].BackColor = Color.YellowGreen;

                        sorgu.ExecuteNonQuery();
                        // ------   ***   ------ \\
                        // ------   LOG   ------ \\
                        // ------   ***   ------ \\
                        lblmalzemeid.Text = tkod.sql_calistir1("SELECT ID FROM MALZEMELER WHERE SERI_NO= '" + malzemeserinoo + "' ");
                        sorgu5.CommandText = "INSERT INTO    MALZEMELER_LOG (M_ID, SEVK_ID, E_BOLGE, E_DEPO, Y_DEPO, ACIKLAMA, KAYIT_EDEN, KAYIT_TARIHI) " +
                            " VALUES(@MID, @SID, @EBOLGE, @EDEPO, @YDEPO, @ACIKLAMA, @UI9, getdate() ) ";
                        sorgu5.Parameters.Clear();
                        sorgu5.Parameters.AddWithValue("@MID", lblmalzemeid.Text);
                        sorgu5.Parameters.AddWithValue("@SID", lblidsevk.Text);
                        sorgu5.Parameters.AddWithValue("@EBOLGE", bolgeid);
                        sorgu5.Parameters.AddWithValue("@EDEPO", eskidepoid);
                        //sorgu5.Parameters.AddWithValue("@YBOLGE", sevkbolgeid);
                        sorgu5.Parameters.AddWithValue("@YDEPO", depoid);
                        sorgu5.Parameters.AddWithValue("@ACIKLAMA", "Sevk teslim alındı.");
                        //sorgu5.Parameters.AddWithValue("@EDURUM", lblmevcutdurum_durum.Text);
                        //sorgu5.Parameters.AddWithValue("@YDURUM", comyenidurum_durum.SelectedValue);
                        //sorgu5.Parameters.AddWithValue("@GRKC", txtgerekce_durum.Text);
                        sorgu5.Parameters.AddWithValue("@UI9", userid);
                        sorgu5.ExecuteNonQuery();
                        // ------   ***   ------ \\
                        // ------   LOG   ------ \\
                        // ------   ***   ------ \\
                    }
                }
                // ------   ***   ------ \\
                // ------   LOG   ------ \\
                // ------   ***   ------ \\
                sorgu.CommandText = "INSERT INTO    SEVK_LOG (SEVK_ID, SEVK_DURUM_ID, TESLIM_ALAN_ID, TESLIM_ALMA_TARIHI, ACIKLAMA) " +
                    " VALUES(@SID_LOG, @SD_LOG, @TA_LOG, getdate(), @ACKLM_LOG ) ";
                sorgu.Parameters.AddWithValue("@SID_LOG", lblidsevk.Text);
                sorgu.Parameters.AddWithValue("@SD_LOG", 3);
                sorgu.Parameters.AddWithValue("@TA_LOG", userid);
                sorgu.Parameters.AddWithValue("@ACKLM_LOG", "Sevk teslim alındı.");
                sorgu.ExecuteNonQuery();
                // ------   ***   ------ \\
                // ------   LOG   ------ \\
                // ------   ***   ------ \\

                conn.Close();

                btnsevkteslimal.Visible = false;
                btnsevkiadeet.Visible = false;

                lblsevkdurum.Text = "3";

                listele_sevkmalzemeleri();
                listele_sevk();

                modal_kapat("ModalMalzemeEkle");
                ScriptManager.RegisterStartupScript(this, this.GetType(), System.Guid.NewGuid().ToString(), "YeniMesaj('Sevk işlemi tamamlandı.','Tamam');", true);
                ScriptManager.RegisterStartupScript(this, this.GetType(), System.Guid.NewGuid().ToString(), "ShowMessage('Sevk işlemi tamamlandı.','Tamam','yeni1');", true);
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), System.Guid.NewGuid().ToString(), "YeniMesaj('Beklenmeyen bir hata oluştu.','Hata');", true);
                ScriptManager.RegisterStartupScript(this, this.GetType(), System.Guid.NewGuid().ToString(), "ShowMessage('Hata oluştu :'" + ex.Message.Replace("'", "") + ",'Hata','yeni1');", true);
            }
            finally
            {
                if (ConnectionState.Open == conn.State)
                    conn.Close();
            }
        }

        protected void btnsevkiadeet_Click(object sender, EventArgs e)
        {
            if (lbldepotanimkontrol.Text == "2")  //Hedef deponun depo tanımı Tamir Depo ise
            {
                SqlCommand sorgu = new SqlCommand();
                sorgu.Connection = conn;
                SqlCommand sorgu1 = new SqlCommand();
                sorgu1.Connection = conn;
                SqlCommand sorgu5 = new SqlCommand();
                sorgu5.Connection = conn;
                conn.Open();

                try
                {
                    string userid = Session["KULLANICI_ID"].ToString();
                    sorgu.CommandText = "UPDATE MALZEME_SEVK SET SEVK_DURUM=@SD, IADE_EDEN=@UI, IADE_TARIHI=getdate()   WHERE ID=@MS_ID_7 ";
                    sorgu.Parameters.AddWithValue("@SD", 4);
                    sorgu.Parameters.AddWithValue("@UI", userid);
                    sorgu.Parameters.AddWithValue("@MS_ID_7", lblidsevk.Text);
                    sorgu.ExecuteNonQuery();
                    string malzemeserino;

                    for (int i = 0; i < grid_sevk_malzeme_sec.Rows.Count; i++)
                    {
                        malzemeserino = HttpUtility.HtmlDecode(grid_sevk_malzeme_sec.Rows[i].Cells[3].Text).Trim();
                        sorgu.CommandText = "SELECT ISNULL(TAMIRDEPO_KONTROL,'FALSE') FROM MALZEMELER WHERE SERI_NO = '" + malzemeserino + "' ";
                        int tamirdepo_kontrol = Convert.ToInt16(sorgu.ExecuteScalar());

                        sorgu.CommandText = "SELECT BOLGE_ID FROM MALZEMELER WHERE SERI_NO = '" + malzemeserino + "' ";
                        int bolgeid = Convert.ToInt16(sorgu.ExecuteScalar());

                        sorgu.CommandText = "SELECT DEPO_ID FROM MALZEMELER WHERE SERI_NO = '" + malzemeserino + "' ";
                        int depoid = Convert.ToInt16(sorgu.ExecuteScalar());

                        sorgu.CommandText = "SELECT ONCEKI_DEPO_ID FROM MALZEMELER WHERE SERI_NO = '" + malzemeserino + "' ";
                        int oncekidepoid = Convert.ToInt16(sorgu.ExecuteScalar());


                        sorgu1.CommandText = "UPDATE MALZEMELER SET DEPO_ID=@DI, ONCEKI_DEPO_ID=@ODI, TAMIRDEPO_KONTROL=@TDK WHERE SERI_NO='" + malzemeserino + "'  ";
                        sorgu1.Parameters.Clear();
                        sorgu1.Parameters.AddWithValue("@DI", oncekidepoid);
                        sorgu1.Parameters.AddWithValue("@ODI", depoid);
                        sorgu1.Parameters.AddWithValue("@TDK", false);

                        grid_sevk_malzeme_sec.Rows[i].BackColor = Color.YellowGreen;

                        sorgu1.ExecuteNonQuery();

                        // ------   ***   ------ \\
                        // ------   LOG   ------ \\
                        // ------   ***   ------ \\
                        lblmalzemeid.Text = tkod.sql_calistir1("SELECT ID FROM MALZEMELER WHERE SERI_NO= '" + malzemeserino + "' ");

                        sorgu5.CommandText = "INSERT INTO    MALZEMELER_LOG (M_ID, SEVK_ID, E_BOLGE, E_DEPO, Y_BOLGE, Y_DEPO, ACIKLAMA, KAYIT_EDEN, KAYIT_TARIHI) " +
                            " VALUES(@MID, @SID, @EBOLGE, @EDEPO, @YBOLGE, @YDEPO, @ACIKLAMA, @UI, getdate() ) ";
                        sorgu5.Parameters.Clear();
                        sorgu5.Parameters.AddWithValue("@MID", lblmalzemeid.Text);
                        sorgu5.Parameters.AddWithValue("@SID", lblidsevk.Text);
                        sorgu5.Parameters.AddWithValue("@EBOLGE", bolgeid);
                        sorgu5.Parameters.AddWithValue("@EDEPO", depoid);
                        sorgu5.Parameters.AddWithValue("@YBOLGE", "0");
                        sorgu5.Parameters.AddWithValue("@YDEPO", oncekidepoid);
                        sorgu5.Parameters.AddWithValue("@ACIKLAMA", "Malzeme TAMİR depo sevki iade etti.");
                        sorgu5.Parameters.AddWithValue("@UI", userid);
                        sorgu5.ExecuteNonQuery();
                        // ------   ***   ------ \\
                        // ------   LOG   ------ \\
                        // ------   ***   ------ \\

                        // ------   MALZEME VE SEVK BİLGİLERİNİ MALZEME_TAMIR_ISLEM TABLOSUNDAN SİLME   ------ \\
                        sorgu1.CommandText = "DELETE FROM MALZEME_TAMIR_ISLEM WHERE M_ID=@MTI_M_ID_S  and GELEN_SEVK_ID=@MTI_GSID_S ";
                        sorgu1.Parameters.Clear();
                        sorgu1.Parameters.AddWithValue("@MTI_M_ID_S", lblmalzemeid.Text);
                        sorgu1.Parameters.AddWithValue("@MTI_GSID_S", lblidsevk.Text);
                        sorgu1.ExecuteNonQuery();
                        // ------   MALZEME VE SEVK BİLGİLERİNİ MALZEME_TAMIR_ISLEM TABLOSUNDAN SİLME   ------ \\
                    }
                    // ------   ***   ------ \\
                    // ------   LOG   ------ \\
                    // ------   ***   ------ \\
                    sorgu.CommandText = "INSERT INTO    SEVK_LOG (SEVK_ID, SEVK_DURUM_ID, IADE_EDEN, IADE_TARIHI, ACIKLAMA) " +
                        " VALUES(@SID_LOG, @SD_LOG, @IE_LOG, getdate(), @ACKLM_LOG ) ";
                    sorgu.Parameters.Clear();
                    sorgu.Parameters.AddWithValue("@SID_LOG", lblidsevk.Text);
                    sorgu.Parameters.AddWithValue("@SD_LOG", 4);
                    sorgu.Parameters.AddWithValue("@KE_LOG", userid);
                    sorgu.Parameters.AddWithValue("@IE_LOG", userid);
                    sorgu.Parameters.AddWithValue("@ACKLM_LOG", "Tamir Depo Sevk'i iade etti.");
                    sorgu.ExecuteNonQuery();
                    // ------   ***   ------ \\
                    // ------   LOG   ------ \\
                    // ------   ***   ------ \\
                    conn.Close();

                    btnsevkiadeet.Visible = false;
                    btnsevkteslimal.Visible = false;

                    lblsevkdurum.Text = "1";

                    listele_sevkmalzemeleri();
                    listele_sevk();

                    modal_kapat("ModalMalzemeEkle");

                    ScriptManager.RegisterStartupScript(this, this.GetType(), System.Guid.NewGuid().ToString(), "YeniMesaj('Sevk iade edildi.','Tamam');", true);
                    ScriptManager.RegisterStartupScript(this, this.GetType(), System.Guid.NewGuid().ToString(), "ShowMessage('Sevk iade edildi.','Tamam','sevket');", true);
                }
                catch (Exception ex)
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), System.Guid.NewGuid().ToString(), "YeniMesaj('Beklenmeyen bir hata oluştu.','Hata');", true);
                    ScriptManager.RegisterStartupScript(this, this.GetType(), System.Guid.NewGuid().ToString(), "ShowMessage('Hata oluştu :'" + ex.Message.Replace("'", "") + ",'Hata','sevket');", true);
                }
                finally
                {
                    if (ConnectionState.Open == conn.State)
                        conn.Close();
                }
            }
            else
            {
                SqlCommand sorgu = new SqlCommand();
                sorgu.Connection = conn;
                SqlCommand sorgu1 = new SqlCommand();
                sorgu1.Connection = conn;
                SqlCommand sorgu5 = new SqlCommand();
                sorgu5.Connection = conn;
                conn.Open();

                try
                {
                    string userid = Session["KULLANICI_ID"].ToString();
                    sorgu.CommandText = "UPDATE MALZEME_SEVK SET SEVK_DURUM=@SD, IADE_EDEN=@UI, IADE_TARIHI=getdate()   WHERE ID=@MSID_U_5 ";
                    sorgu.Parameters.AddWithValue("@SD", 4);
                    sorgu.Parameters.AddWithValue("@UI", userid);
                    sorgu.Parameters.AddWithValue("@MSID_U_5", lblidsevk.Text);
                    sorgu.ExecuteNonQuery();
                    string malzemeserino;

                    for (int i = 0; i < grid_sevk_malzeme_sec.Rows.Count; i++)
                    {
                        malzemeserino = HttpUtility.HtmlDecode(grid_sevk_malzeme_sec.Rows[i].Cells[3].Text).Trim();
                        sorgu.CommandText = "SELECT BOLGE_ID FROM MALZEMELER WHERE SERI_NO = '" + malzemeserino + "' ";
                        int bolgeid = Convert.ToInt16(sorgu.ExecuteScalar());

                        sorgu.CommandText = "SELECT DEPO_ID FROM MALZEMELER WHERE SERI_NO = '" + malzemeserino + "' ";
                        int depoid = Convert.ToInt16(sorgu.ExecuteScalar());

                        sorgu.CommandText = "SELECT ONCEKI_BOLGE_ID FROM MALZEMELER WHERE SERI_NO = '" + malzemeserino + "' ";
                        int oncekibolgeid = Convert.ToInt16(sorgu.ExecuteScalar());

                        sorgu.CommandText = "SELECT ONCEKI_DEPO_ID FROM MALZEMELER WHERE SERI_NO = '" + malzemeserino + "' ";
                        int oncekidepoid = Convert.ToInt16(sorgu.ExecuteScalar());

                        sorgu1.CommandText = "UPDATE MALZEMELER SET BOLGE_ID=@BI, DEPO_ID=@DI, ONCEKI_BOLGE_ID=@OBI, ONCEKI_DEPO_ID=@ODI WHERE SERI_NO='" + malzemeserino + "'  ";
                        sorgu1.Parameters.Clear();
                        sorgu1.Parameters.AddWithValue("@DI", oncekidepoid);
                        sorgu1.Parameters.AddWithValue("@BI", oncekibolgeid);
                        sorgu1.Parameters.AddWithValue("@OBI", bolgeid);
                        sorgu1.Parameters.AddWithValue("@ODI", depoid);

                        grid_sevk_malzeme_sec.Rows[i].BackColor = Color.YellowGreen;

                        sorgu1.ExecuteNonQuery();

                        // ------   ***   ------ \\
                        // ------   LOG   ------ \\
                        // ------   ***   ------ \\
                        lblmalzemeid.Text = tkod.sql_calistir1("SELECT ID FROM MALZEMELER WHERE SERI_NO= '" + malzemeserino + "' ");

                        sorgu5.CommandText = "INSERT INTO    MALZEMELER_LOG (M_ID, SEVK_ID, E_BOLGE, E_DEPO, Y_BOLGE, Y_DEPO, ACIKLAMA, KAYIT_EDEN, KAYIT_TARIHI) " +
                            " VALUES(@MID, @SID, @EBOLGE, @EDEPO, @YBOLGE, @YDEPO, @ACIKLAMA, @UI, getdate() ) ";
                        sorgu5.Parameters.Clear();
                        sorgu5.Parameters.AddWithValue("@MID", lblmalzemeid.Text);
                        sorgu5.Parameters.AddWithValue("@SID", lblidsevk.Text);
                        sorgu5.Parameters.AddWithValue("@EBOLGE", bolgeid);
                        sorgu5.Parameters.AddWithValue("@EDEPO", depoid);
                        sorgu5.Parameters.AddWithValue("@YBOLGE", oncekibolgeid);
                        sorgu5.Parameters.AddWithValue("@YDEPO", oncekidepoid);
                        sorgu5.Parameters.AddWithValue("@ACIKLAMA", "Sevk iade edildi.");
                        //sorgu5.Parameters.AddWithValue("@EDURUM", lblmevcutdurum_durum.Text);
                        //sorgu5.Parameters.AddWithValue("@YDURUM", comyenidurum_durum.SelectedValue);
                        //sorgu5.Parameters.AddWithValue("@GRKC", txtgerekce_durum.Text);
                        sorgu5.Parameters.AddWithValue("@UI", userid);
                        sorgu5.ExecuteNonQuery();
                        // ------   ***   ------ \\
                        // ------   LOG   ------ \\
                        // ------   ***   ------ \\
                    }
                    // ------   ***   ------ \\
                    // ------   LOG   ------ \\
                    // ------   ***   ------ \\
                    sorgu.CommandText = "INSERT INTO    SEVK_LOG (SEVK_ID, SEVK_DURUM_ID, IADE_EDEN, IADE_TARIHI, ACIKLAMA) " +
                        " VALUES(@SID_LOG, @SD_LOG, @IE_LOG, getdate(), @ACKLM_LOG ) ";
                    sorgu.Parameters.Clear();
                    sorgu.Parameters.AddWithValue("@SID_LOG", lblidsevk.Text);
                    sorgu.Parameters.AddWithValue("@SD_LOG", 4);
                    sorgu.Parameters.AddWithValue("@KE_LOG", userid);
                    sorgu.Parameters.AddWithValue("@IE_LOG", userid);
                    sorgu.Parameters.AddWithValue("@ACKLM_LOG", "Sevk iade edildi.");
                    sorgu.ExecuteNonQuery();
                    // ------   ***   ------ \\
                    // ------   LOG   ------ \\
                    // ------   ***   ------ \\
                    conn.Close();

                    btnsevkiadeet.Visible = false;
                    btnsevkteslimal.Visible = false;

                    lblsevkdurum.Text = "1";
                    listele_sevkmalzemeleri();
                    listele_sevk();

                    ScriptManager.RegisterStartupScript(this, this.GetType(), System.Guid.NewGuid().ToString(), "YeniMesaj('Sevk iade edildi.','Tamam');", true);
                    ScriptManager.RegisterStartupScript(this, this.GetType(), System.Guid.NewGuid().ToString(), "ShowMessage('Sevk iade edildi.','Tamam','sevket');", true);
                }
                catch (Exception ex)
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), System.Guid.NewGuid().ToString(), "YeniMesaj('Beklenmeyen bir hata oluştu.','Hata');", true);
                    ScriptManager.RegisterStartupScript(this, this.GetType(), System.Guid.NewGuid().ToString(), "ShowMessage('Hata oluştu :'" + ex.Message.Replace("'", "") + ",'Hata','sevket');", true);
                }
                finally
                {
                    if (ConnectionState.Open == conn.State)
                        conn.Close();
                }
            }
            listele_sevkmalzemeleri();
        }

        protected void btnmalzemeara_Click(object sender, EventArgs e)
        {
        }

        protected void grid_sevk_malzeme_sec_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            grid_sevk_malzeme_sec.PageIndex = e.NewPageIndex;
            listele_sevkmalzemeleri();
        }

        // TAMİR DEPO SEVK MALZEME Ekle >>> Kaynak Depo TAMİR, Hedef Depo İl
        void listele_sevkmalzemeleri_tamirsevkolustur()
        {
            btnhareket_tamirsevkolustur.Visible = true;
            btnsevkmalzemeexcel_tamirsevkolustur.Visible = true;

            lblsevkdurum_tamirsevkolustur.Text = tkod.sql_calistir_param("SELECT SEVK_DURUM FROM MALZEME_SEVK WHERE ID=@ID", new SqlParameter("ID", lblidsevk.Text));
           
            kullanici_kaynak_hedef_bolge_yetki_kontrol();

            if (lblsevkdurum_tamirsevkolustur.Text == "1" || lblsevkdurum_tamirsevkolustur.Text == "4")
            {
                if (tkod.admin_yetki())
                {
                    conn.Open();
                    btnsevket_tamirsevkolustur.Visible = true;
                    btnsevkiptal_tamirsevkolustur.Visible = true;
                    panel_malzeme_ekle.Visible = true;
                    conn.Close();
                }
                else
                if (kbolge_sevk_k_bolge_yetkikontrol > 0)
                {
                    panel_malzeme_ekle.Visible = true;
                }
                else
                {
                    btnsevket_tamirsevkolustur.Visible = false;
                    btnsevkiptal_tamirsevkolustur.Visible = false;
                    panel_malzeme_ekle.Visible = false;
                }
            }
            if (lblsevkdurum_tamirsevkolustur.Text == "2")
            {
                btnsevket_tamirsevkolustur.Visible = false;
                btnsevkiptal_tamirsevkolustur.Visible = false;
                panel_malzeme_ekle.Visible = false;

                if (tkod.admin_yetki())
                {
                    conn.Open();
                    btnsevkteslimal_tamirsevkolustur.Visible = true;
                    btnsevkiadeet_tamirsevkolustur.Visible = true;

                    conn.Close();
                }
                else
                if (kbolge_sevk_h_bolge_yetkikontrol > 0)
                {
                    btnsevkteslimal_tamirsevkolustur.Visible = true;
                    btnsevkiadeet_tamirsevkolustur.Visible = true;
                }
                else
                {
                    btnsevkteslimal_tamirsevkolustur.Visible = false;
                    btnsevkiadeet_tamirsevkolustur.Visible = false;
                }
            }
            if (lblsevkdurum_tamirsevkolustur.Text == "3")
            {
                btnsevket_tamirsevkolustur.Visible = false;
                btnsevkiptal_tamirsevkolustur.Visible = false;
                btnsevkteslimal_tamirsevkolustur.Visible = false;
                btnsevkiadeet_tamirsevkolustur.Visible = false;
                panel_malzeme_ekle.Visible = false;
            }
            if (lblsevkdurum_tamirsevkolustur.Text == "5")
            {
                btnsevket_tamirsevkolustur.Visible = false;
                btnsevkiptal_tamirsevkolustur.Visible = false;
                btnsevkteslimal_tamirsevkolustur.Visible = false;
                btnsevkiadeet_tamirsevkolustur.Visible = false;
                panel_malzeme_ekle.Visible = false;
            }

            SqlCommand cmd;
            string sql = " ", sql1 = " ";

            sql = "SELECT MSM.ID, MM.MARKA, MMO.MODEL, M.SERI_NO, BOLGE.BOLGE_ADI, MD.DURUM,  " +
                " KULLANICI.ISIM + ' ' +CONVERT(NVARCHAR(10),MSM.KAYIT_TARIHI,104) AS KAYIT " +

                " FROM MALZEME_SEVK_MALZEMELER AS MSM " +

                " INNER JOIN MALZEMELER         AS M   ON M.ID = MSM.MALZEME_ID " +
                " INNER JOIN MALZEME_MARKAMODEL AS MM  ON M.MARKA=MM.ID " +
                " INNER JOIN MALZEME_MODEL      AS MMO ON M.MODEL=MMO.ID " +
                " INNER JOIN MALZEME_DURUM AS MD ON MD.ID=MSM.DURUM" +
                " LEFT JOIN KULLANICI                  ON KULLANICI.ID = MSM.KAYIT_EDEN " +
                " LEFT JOIN BOLGE ON BOLGE.ID = M.BOLGE_ID" +

                " WHERE MSM.SEVK_ID= '" + lblidsevk.Text + "' ";

            sql = sql + sql1;
            cmd = new SqlCommand(sql, conn);
            conn.Open();
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            grid_sevk_malzeme_sec_tamirsevkolustur.DataSource = dt;
            conn.Close();
            grid_sevk_malzeme_sec_tamirsevkolustur.DataBind();

            if (grid_sevk_malzeme_sec_tamirsevkolustur.Rows.Count == 0)
                btnsevket_tamirsevkolustur.Visible = false;

            lblmalzemesayisi_tamirsevkolustur.Text = grid_sevk_malzeme_sec_tamirsevkolustur.Rows.Count.ToString() + " " + "adet kayıt bulunmuştur.";
        }

        void listele_bolge_sevkmalzemeekle()
        {
            int kaynakdepoid1 = Convert.ToInt16(tkod.sql_calistir_param("SELECT ID FROM DEPO WHERE DEPO=@DEPO", new SqlParameter("DEPO", lblkaynakdepo.Text)));  // KAYNAK DEPO id'sine bakıyor. Bu bilgi tamir depodan malzeme eklenirken malzemenin tamir olduğu depo ile sevk'in kaynak deposunu karşılaştırmak için kullanılacak.

            conn.Open();
            combolgesec_tamirsevkolustur.Items.Clear();
            combolgesec_tamirsevkolustur.Items.Insert(0, new ListItem("Seçiniz", "0"));
            combolgesec_tamirsevkolustur.AppendDataBoundItems = true;
            string sql1 = "";

            SqlDataAdapter da = new SqlDataAdapter();
            SqlCommand sorgu = new SqlCommand("Select BOLGE.BOLGE_ADI, BOLGE.ID FROM MALZEME_TAMIR_ISLEM AS MTI " +
                " LEFT JOIN BOLGE ON BOLGE.ID=MTI.BOLGE_ID WHERE ISNULL(SEVK_KONTROL,'FALSE') = 0 AND ISNULL(ISLEMYAPILDIMI,'FALSE') = 1 AND TAMIR_EDILECEK_DEPO =@KDID GROUP BY BOLGE_ADI,  BOLGE.ID ");
            sorgu.Parameters.AddWithValue("KDID", kaynakdepoid1);

            DataTable dt = new DataTable();
            da.SelectCommand = sorgu;
            da.SelectCommand.Connection = conn;
            da.Fill(dt);
            combolgesec_tamirsevkolustur.DataSource = dt;
            combolgesec_tamirsevkolustur.DataBind();
            conn.Close();
        }

        protected void grid_sevk_malzeme_sec_tamirsevkolustur_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName.Equals("sil"))
            {
                lblidsil.Text = Convert.ToString(e.CommandArgument);

                lblislem.Text = "sevk-tamir-malzeme-sil";
                btnsil.Enabled = true;
                btnsil.CssClass = "btn btn-danger ";
                modal_kapat("ModalTamir_MalzemeEkle");
                modal_ac("ModalSil");
                lblmodalsilyonlendir.Text = "ModalTamir_MalzemeEkle";
            }
            if (e.CommandName.Equals("ariza"))
            {
                int index = Convert.ToInt32(e.CommandArgument);
                lblidsevk_ariza.Text = grid_sevk_malzeme_sec_tamirsevkolustur.DataKeys[index].Value.ToString();

                lblidcihaz_ariza.Text = tkod.sql_calistir_param("SELECT MALZEME_ID FROM MALZEME_SEVK_MALZEMELER WHERE ID=@ID", new SqlParameter("ID", lblidsevk_ariza.Text));

                lblmodalyenibaslik5.Text = "Arıza Geçmişi";
                modal_kapat("ModalTamir_MalzemeEkle");
                modal_ac("ModalAriza");
                lblmodalarizayonlendir.Text = "ModalTamir_MalzemeEkle";
                listele_ariza();
            }
        }

        protected void grid_sevk_malzeme_sec_tamirsevkolustur_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowIndex > -1)
            {
                Button butonmalzemesil = (Button)e.Row.FindControl("btnsil");
                if (lblsevkdurum_tamirsevkolustur.Text == "2" || lblsevkdurum_tamirsevkolustur.Text == "3")
                    butonmalzemesil.Visible = false;
                else
                {
                    if (tkod.admin_yetki())
                    {
                        butonmalzemesil.Visible = true;
                    }
                    else
                    {
                        if (kbolge_sevk_k_bolge_yetkikontrol > 0)
                        {
                            butonmalzemesil.Visible = true;
                        }
                        else
                        {
                            butonmalzemesil.Visible = false;
                        }
                    }
                }
            }
        }

        protected void btnsevkmalzemeekle_tamirsevkolustur_Click(object sender, EventArgs e)
        {
            SqlCommand sorgu = new SqlCommand();
            sorgu.Connection = conn;
            SqlCommand sorgu1 = new SqlCommand();
            sorgu1.Connection = conn;
            conn.Open();

            // combolgesec_tamirsevkolustur'dan seçilen bölgedeki tamir depoda yer alan malzemeler data table aracılığı ile grid'e yükleniyor.
            int kaynakdepoid1 = Convert.ToInt16(tkod.sql_calistir_param("SELECT ID FROM DEPO WHERE DEPO=@DEPO", new SqlParameter("DEPO", lblkaynakdepo_tamir.Text)));  // KAYNAK DEPO id'sine bakıyor. Bu bilgi tamir depodan malzeme eklenirken malzemenin tamir olduğu depo ile sevk'in kaynak deposunu karşılaştırmak için kullanılacak.

            DataTable dt_bolge = new DataTable();
            sorgu.CommandText = "SELECT M_ID, SON_DURUM, GELEN_SEVK_ID FROM MALZEME_TAMIR_ISLEM WHERE BOLGE_ID='" + combolgesec_tamirsevkolustur.SelectedValue + "' AND ISNULL(SEVK_KONTROL, 'false')= 0 AND ISNULL(ISLEMYAPILDIMI,'FALSE') = 1 AND TAMIR_EDILECEK_DEPO =@KDID ";
            sorgu.Parameters.AddWithValue("KDID", kaynakdepoid1);
            SqlDataAdapter da = new SqlDataAdapter(sorgu);
            da.Fill(dt_bolge);

            //sorgu.CommandText = "SELECT GELEN_SEVK_ID FROM MALZEME_TAMIR_ISLEM WHERE M_SERINO = '" + txtserino_tamirsevkolustur.Text + "' AND ISNULL(SEVK_KONTROL, 'FALSE') = 'FALSE' ";
            //int tamiregelensevkid = Convert.ToInt16(sorgu.ExecuteScalar());

            try
            {
                string userid = Session["KULLANICI_ID"].ToString();

                for (int i = 0; i < dt_bolge.Rows.Count; i++)
                {
                    sorgu.CommandText = "INSERT INTO    MALZEME_SEVK_MALZEMELER (MALZEME_ID, SEVK_ID, DURUM, TAMIRE_GELEN_SEVK_ID, KAYIT_EDEN , KAYIT_TARIHI)  VALUES(@MI, @SI, @DU, @TGSID, @UI, getdate() ) ";
                    sorgu.Parameters.Clear();
                    sorgu.Parameters.AddWithValue("@MI", dt_bolge.Rows[i][0].ToString());
                    sorgu.Parameters.AddWithValue("@SI", lblidsevk.Text);
                    sorgu.Parameters.AddWithValue("@DU", dt_bolge.Rows[i][1].ToString());
                    sorgu.Parameters.AddWithValue("@TGSID", dt_bolge.Rows[i][2].ToString());
                    sorgu.Parameters.AddWithValue("@UI", userid);
                    sorgu.ExecuteNonQuery();

                    sorgu.CommandText = "UPDATE MALZEMELER SET SEVKDURUMU=@SD, TAMIRDEPO_KONTROL=@TDK  WHERE ID='" + dt_bolge.Rows[i][0].ToString() + "' ";
                    sorgu.Parameters.Clear();
                    sorgu.Parameters.AddWithValue("@SD", 1);
                    sorgu.Parameters.AddWithValue("@TDK", 0);
                    sorgu.ExecuteNonQuery();

                    sorgu.CommandText = "UPDATE MALZEME_TAMIR_ISLEM SET SEVK_KONTROL=@SK  WHERE M_ID='" + dt_bolge.Rows[i][0].ToString() + "' ";
                    sorgu.Parameters.Clear();
                    sorgu.Parameters.AddWithValue("@SK", 1);
                    sorgu.ExecuteNonQuery();

                    // ------   ***   ------ \\
                    // ------   LOG   ------ \\
                    // ------   ***   ------ \\
                    sorgu.CommandText = "SELECT KAYNAK_BOLGE FROM MALZEME_SEVK WHERE ID = @SORGU1 ";
                    sorgu.Parameters.Clear();
                    sorgu.Parameters.AddWithValue("@SORGU1", lblidsevk.Text);
                    int kaynakbolgeid = Convert.ToInt16(sorgu.ExecuteScalar());

                    sorgu.CommandText = "SELECT KAYNAK_DEPO FROM MALZEME_SEVK WHERE ID = @SORGU2 ";
                    sorgu.Parameters.Clear();
                    sorgu.Parameters.AddWithValue("@SORGU2", lblidsevk.Text);
                    int kaynakdepoid = Convert.ToInt16(sorgu.ExecuteScalar());

                    sorgu.CommandText = "SELECT HEDEF_BOLGE FROM MALZEME_SEVK WHERE ID = @SORGU3 ";
                    sorgu.Parameters.Clear();
                    sorgu.Parameters.AddWithValue("@SORGU3", lblidsevk.Text);
                    int hedefbolgeid = Convert.ToInt16(sorgu.ExecuteScalar());

                    sorgu.CommandText = "SELECT HEDEF_DEPO FROM MALZEME_SEVK WHERE ID = @SORGU4 ";
                    sorgu.Parameters.Clear();
                    sorgu.Parameters.AddWithValue("@SORGU4", lblidsevk.Text);
                    int hedefdepoid = Convert.ToInt16(sorgu.ExecuteScalar());

                    sorgu.CommandText = "INSERT INTO    MALZEMELER_LOG (M_ID, SEVK_ID, E_BOLGE, E_DEPO, Y_BOLGE, Y_DEPO, ACIKLAMA, E_DURUM, KAYIT_EDEN, KAYIT_TARIHI) " +
                        " VALUES(@MID, @SID, @EBOLGE, @EDEPO, @YBOLGE, @YDEPO, @ACIKLAMA, @EDURUM, @UI, getdate() ) ";
                    sorgu.Parameters.Clear();
                    sorgu.Parameters.AddWithValue("@MID", dt_bolge.Rows[i][0].ToString());
                    sorgu.Parameters.AddWithValue("@SID", lblidsevk.Text);
                    sorgu.Parameters.AddWithValue("@EBOLGE", kaynakbolgeid);
                    sorgu.Parameters.AddWithValue("@EDEPO", kaynakdepoid);
                    sorgu.Parameters.AddWithValue("@YBOLGE", "0");
                    sorgu.Parameters.AddWithValue("@YDEPO", "0");
                    sorgu.Parameters.AddWithValue("@ACIKLAMA", "Malzeme Tamir işleminden sonra Sevk sürecine dahil edildi.");
                    sorgu.Parameters.AddWithValue("@EDURUM", dt_bolge.Rows[i][1].ToString());
                    //sorgu.Parameters.AddWithValue("@YDURUM", dt_bolge.Rows[i][1].ToString());
                    //sorgu5.Parameters.AddWithValue("@GRKC", txtgerekce_durum.Text);
                    sorgu.Parameters.AddWithValue("@UI", userid);
                    sorgu.ExecuteNonQuery();
                    // ------   ***   ------ \\
                    // ------   LOG   ------ \\
                    // ------   ***   ------ \\
                }
                conn.Close();

                listele_sevkmalzemeleri_tamirsevkolustur();
                listele_bolge_sevkmalzemeekle();

                ScriptManager.RegisterStartupScript(this, this.GetType(), System.Guid.NewGuid().ToString(), "YeniMesaj('Malzeme başarıyla eklendi.','Tamam');", true);
                ScriptManager.RegisterStartupScript(this, this.GetType(), System.Guid.NewGuid().ToString(), "ShowMessage('Malzeme başarıyla eklendi.','Tamam','yeni2');", true);
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), System.Guid.NewGuid().ToString(), "YeniMesaj('Beklenmeyen bir hata oluştu.','Hata');", true);
                ScriptManager.RegisterStartupScript(this, this.GetType(), System.Guid.NewGuid().ToString(), "ShowMessage('Hata oluştu :'" + ex.Message.Replace("'", "") + ",'Hata','yeni2');", true);
            }
            finally
            {
                if (ConnectionState.Open == conn.State)
                    conn.Close();
            }
            listele_sevk();
        }

        protected void btnsevket_tamirsevkolustur_Click(object sender, EventArgs e)
        {
            if (grid_sevk_malzeme_sec_tamirsevkolustur.Rows.Count < 1)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), System.Guid.NewGuid().ToString(), "YeniMesaj('Malzeme seçimi yapılması gerekmektedir.','Hata');", true);
                ScriptManager.RegisterStartupScript(this, this.GetType(), System.Guid.NewGuid().ToString(), "ShowMessage('Malzeme seçimi yapılması gerekmektedir.','Hata','yeni2');", true);
            }
            else
            {
                SqlCommand sorgu = new SqlCommand();
                sorgu.Connection = conn;
                SqlCommand sorgu1 = new SqlCommand();
                sorgu1.Connection = conn;
                SqlCommand sorgu5 = new SqlCommand();
                sorgu5.Connection = conn;
                conn.Open();

                try
                {
                    string userid = Session["KULLANICI_ID"].ToString();
                    sorgu.CommandText = "UPDATE MALZEME_SEVK SET SEVK_DURUM=@SD, SEVK_EDEN=@UI, SEVK_TARIHI=getdate()  WHERE ID=@MSID_U_5 ";
                    sorgu.Parameters.AddWithValue("@SD", 2);
                    sorgu.Parameters.AddWithValue("@UI", userid);
                    sorgu.Parameters.AddWithValue("@MSID_U_5", lblidsevk.Text);
                    sorgu.ExecuteNonQuery();

                    string malzemeserino;

                    for (int i = 0; i < grid_sevk_malzeme_sec_tamirsevkolustur.Rows.Count; i++)
                    {
                        malzemeserino = HttpUtility.HtmlDecode(grid_sevk_malzeme_sec_tamirsevkolustur.Rows[i].Cells[4].Text).Trim();
                        sorgu.CommandText = "SELECT BOLGE_ID FROM MALZEMELER WHERE SERI_NO = '" + malzemeserino + "' ";
                        int bolgeid = Convert.ToInt16(sorgu.ExecuteScalar());

                        sorgu.CommandText = "SELECT DEPO_ID FROM MALZEMELER WHERE SERI_NO = '" + malzemeserino + "' ";
                        int depoid = Convert.ToInt16(sorgu.ExecuteScalar());

                        sorgu.CommandText = "SELECT HEDEF_BOLGE FROM MALZEME_SEVK WHERE ID = @SORGU5 ";
                        sorgu.Parameters.Clear();
                        sorgu.Parameters.AddWithValue("@SORGU5", lblidsevk.Text);
                        int sevkbolgeid = Convert.ToInt16(sorgu.ExecuteScalar());

                        sorgu.CommandText = "SELECT HEDEF_DEPO FROM MALZEME_SEVK WHERE ID = @SORGU6 ";
                        sorgu.Parameters.Clear();
                        sorgu.Parameters.AddWithValue("@SORGU6", lblidsevk.Text);
                        int sevkdepoid = Convert.ToInt16(sorgu.ExecuteScalar());

                        // ------   MALZEME BÖLGE VE DEPO BİLGİLERİ GÜNCELLEME   ------ \\
                        sorgu1.CommandText = "UPDATE MALZEMELER SET ONCEKI_BOLGE_ID=@OBI, ONCEKI_DEPO_ID=@ODI, BOLGE_ID=@BI, DEPO_ID=@DI  WHERE SERI_NO='" + malzemeserino + "'  ";
                        sorgu1.Parameters.Clear();
                        sorgu1.Parameters.AddWithValue("@OBI", bolgeid);
                        sorgu1.Parameters.AddWithValue("@ODI", depoid);
                        sorgu1.Parameters.AddWithValue("@DI", sevkdepoid);
                        sorgu1.Parameters.AddWithValue("@BI", sevkbolgeid);
                        sorgu1.ExecuteNonQuery();
                        // ------   MALZEME BÖLGE VE DEPO BİLGİLERİ GÜNCELLEME   ------ \\

                        grid_sevk_malzeme_sec_tamirsevkolustur.Rows[i].BackColor = Color.YellowGreen;

                        // ------   ***   ------ \\
                        // ------   LOG   ------ \\
                        // ------   ***   ------ \\
                        lblmalzemeid.Text = tkod.sql_calistir1("SELECT ID FROM MALZEMELER WHERE SERI_NO= '" + malzemeserino + "' ");
                        sorgu5.CommandText = "INSERT INTO    MALZEMELER_LOG (M_ID, SEVK_ID, E_BOLGE, E_DEPO, Y_BOLGE, Y_DEPO, ACIKLAMA, KAYIT_EDEN, KAYIT_TARIHI) " +
                            " VALUES(@MID, @SID, @EBOLGE, @EDEPO, @YBOLGE, @YDEPO, @ACIKLAMA, @UI, getdate() ) ";
                        sorgu5.Parameters.Clear();
                        sorgu5.Parameters.AddWithValue("@MID", lblmalzemeid.Text);
                        sorgu5.Parameters.AddWithValue("@SID", lblidsevk.Text);
                        sorgu5.Parameters.AddWithValue("@EBOLGE", bolgeid);
                        sorgu5.Parameters.AddWithValue("@EDEPO", depoid);
                        sorgu5.Parameters.AddWithValue("@YBOLGE", sevkbolgeid);
                        sorgu5.Parameters.AddWithValue("@YDEPO", sevkdepoid);
                        sorgu5.Parameters.AddWithValue("@ACIKLAMA", "Malzeme sevk edildi. (Tamir Depo'dan gelen)");
                        sorgu5.Parameters.AddWithValue("@UI", userid);
                        sorgu5.ExecuteNonQuery();
                        // ------   ***   ------ \\
                        // ------   LOG   ------ \\
                        // ------   ***   ------ \\
                    }

                    conn.Close();

                    modal_kapat("ModalTamir_MalzemeEkle");

                    listele_sevkmalzemeleri_tamirsevkolustur();

                    btnsevket_tamirsevkolustur.Visible = false;
                    btnsevkiptal_tamirsevkolustur.Visible = false;

                    btnsevkteslimal_tamirsevkolustur.Visible = false;
                    btnsevkiadeet_tamirsevkolustur.Visible = false;
                    panel_malzeme_ekle.Visible = false;

                    lblsevkdurum.Text = "2";

                    listele_sevkmalzemeleri();
                    listele_sevk();


                    ScriptManager.RegisterStartupScript(this, this.GetType(), System.Guid.NewGuid().ToString(), "YeniMesaj('Sevk süreci başlatıldı.','Tamam');", true);
                    ScriptManager.RegisterStartupScript(this, this.GetType(), System.Guid.NewGuid().ToString(), "ShowMessage('Sevk süreci başlatıldı.','Tamam','yeni2');", true);
                }
                catch (Exception ex)
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), System.Guid.NewGuid().ToString(), "YeniMesaj('Beklenmeyen bir hata oluştu.','Hata');", true);
                    ScriptManager.RegisterStartupScript(this, this.GetType(), System.Guid.NewGuid().ToString(), "ShowMessage('Hata oluştu :'" + ex.Message.Replace("'", "") + ",'Hata','yeni2');", true);
                }
                finally
                {
                    if (ConnectionState.Open == conn.State)
                        conn.Close();
                }
            }
        }

        protected void btnsevkteslimal_tamirsevkolustur_Click(object sender, EventArgs e)
        {
            SqlCommand sorgu = new SqlCommand();
            sorgu.Connection = conn;
            SqlCommand sorgu5 = new SqlCommand();
            sorgu5.Connection = conn;
            conn.Open();

            try
            {
                string userid = Session["KULLANICI_ID"].ToString();
                sorgu.CommandText = "UPDATE MALZEME_SEVK SET SEVK_DURUM=@SD, TESLIM_ALAN=@UI, TESLIM_ALMA_TARIHI=getdate() WHERE ID=@SORGU7 ";
                sorgu.Parameters.AddWithValue("@SD", 3);
                sorgu.Parameters.AddWithValue("@UI", userid);
                sorgu.Parameters.AddWithValue("@SORGU7", lblidsevk.Text);
                sorgu.ExecuteNonQuery();
                string malzemeserinoo;

                for (int i = 0; i < grid_sevk_malzeme_sec_tamirsevkolustur.Rows.Count; i++)
                {
                    malzemeserinoo = HttpUtility.HtmlDecode(grid_sevk_malzeme_sec_tamirsevkolustur.Rows[i].Cells[4].Text).Trim();

                    sorgu.CommandText = "SELECT BOLGE_ID FROM MALZEMELER WHERE SERI_NO = '" + malzemeserinoo + "' ";
                    int bolgeid = Convert.ToInt16(sorgu.ExecuteScalar());

                    sorgu.CommandText = "SELECT DEPO_ID FROM MALZEMELER WHERE SERI_NO = '" + malzemeserinoo + "' ";
                    int depoid = Convert.ToInt16(sorgu.ExecuteScalar());

                    sorgu.CommandText = "SELECT ONCEKI_DEPO_ID FROM MALZEMELER WHERE SERI_NO = '" + malzemeserinoo + "' ";
                    int eskidepoid = Convert.ToInt16(sorgu.ExecuteScalar());

                    sorgu.CommandText = "UPDATE MALZEMELER SET SEVKDURUMU=@SD WHERE SERI_NO='" + malzemeserinoo + "'  ";
                    sorgu.Parameters.Clear();
                    sorgu.Parameters.AddWithValue("@SD", false);

                    grid_sevk_malzeme_sec_tamirsevkolustur.Rows[i].BackColor = Color.YellowGreen;

                    sorgu.ExecuteNonQuery();
                    // ------   ***   ------ \\
                    // ------   LOG   ------ \\
                    // ------   ***   ------ \\
                    lblmalzemeid.Text = tkod.sql_calistir1("SELECT ID FROM MALZEMELER WHERE SERI_NO= '" + malzemeserinoo + "' ");
                    sorgu5.CommandText = "INSERT INTO    MALZEMELER_LOG (M_ID, SEVK_ID, E_BOLGE, E_DEPO, Y_DEPO, ACIKLAMA, KAYIT_EDEN, KAYIT_TARIHI) " +
                        " VALUES(@MID, @SID, @EBOLGE, @EDEPO, @YDEPO, @ACIKLAMA, @UI, getdate() ) ";
                    sorgu5.Parameters.Clear();
                    sorgu5.Parameters.AddWithValue("@MID", lblmalzemeid.Text);
                    sorgu5.Parameters.AddWithValue("@SID", lblidsevk.Text);
                    sorgu5.Parameters.AddWithValue("@EBOLGE", bolgeid);
                    sorgu5.Parameters.AddWithValue("@EDEPO", eskidepoid);
                    sorgu5.Parameters.AddWithValue("@YDEPO", depoid);
                    sorgu5.Parameters.AddWithValue("@ACIKLAMA", "Sevk teslim alındı. (Tamir depo'dan gelen sevk)");
                    sorgu5.Parameters.AddWithValue("@UI", userid);
                    sorgu5.ExecuteNonQuery();
                    // ------   ***   ------ \\
                    // ------   LOG   ------ \\
                    // ------   ***   ------ \\
                }

                conn.Close();
                modal_kapat("ModalTamir_MalzemeEkle");

                btnsevkteslimal_tamirsevkolustur.Enabled = false;
                btnsevkteslimal_tamirsevkolustur.CssClass = "btn btn-success disabled ";
                btnsevkiadeet_tamirsevkolustur.Visible = false;

                lblsevkdurum.Text = "3";

                listele_sevkmalzemeleri_tamirsevkolustur();
                listele_sevk();

                ScriptManager.RegisterStartupScript(this, this.GetType(), System.Guid.NewGuid().ToString(), "YeniMesaj('Sevk işlemi tamamlandı.','Tamam');", true);
                ScriptManager.RegisterStartupScript(this, this.GetType(), System.Guid.NewGuid().ToString(), "ShowMessage('Sevk işlemi tamamlandı.','Tamam','yeni1');", true);
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), System.Guid.NewGuid().ToString(), "YeniMesaj('Beklenmeyen bir hata oluştu.','Hata');", true);
                ScriptManager.RegisterStartupScript(this, this.GetType(), System.Guid.NewGuid().ToString(), "ShowMessage('Hata oluştu :'" + ex.Message.Replace("'", "") + ",'Hata','yeni1');", true);
            }
            finally
            {
                if (ConnectionState.Open == conn.State)
                    conn.Close();
            }
        }

        protected void btnsevkmalzemeekle_1_tamirsevkolustur_Click(object sender, EventArgs e)
        {
            SqlCommand sorgu = new SqlCommand();
            sorgu.Connection = conn;
            SqlCommand sorgu1 = new SqlCommand();
            sorgu1.Connection = conn;
            conn.Open();

            sorgu.CommandText = "SELECT TAMIR_EDILECEK_DEPO FROM MALZEME_TAMIR_ISLEM WHERE M_SERINO = @SORGU8 AND ISNULL(SEVK_KONTROL, 'FALSE') = 'FALSE' ";
            sorgu.Parameters.AddWithValue("@SORGU8", txtserino_tamirsevkolustur.Text);
            int tamiredilecekdepo_sevkhedefdepo = Convert.ToInt16(sorgu.ExecuteScalar()); //Tamir edilen malzeme hangi tamir depoda tamir edilmişse sevk'in kaynak deposu'da o olmalı

            sorgu.CommandText = "SELECT KAYNAK_DEPO FROM MALZEME_SEVK WHERE ID = @SORGU10 ";
            sorgu.Parameters.AddWithValue("@SORGU10", lblidsevk.Text);
            int sevkkaynakdepo = Convert.ToInt16(sorgu.ExecuteScalar()); //Tamir edilen malzeme hangi tamir depoda tamir edilmişse sevk'in kaynak deposu'da o olmalı

            sorgu.CommandText = "SELECT ID FROM MALZEMELER WHERE SERI_NO = @SORGU11 ";
            sorgu.Parameters.AddWithValue("@SORGU11", txtserino_tamirsevkolustur.Text);
            int malzemeid = Convert.ToInt16(sorgu.ExecuteScalar());

            sorgu.CommandText = "SELECT SEVKDURUMU FROM MALZEMELER WHERE SERI_NO = @SORGU12 ";
            sorgu.Parameters.AddWithValue("@SORGU12", txtserino_tamirsevkolustur.Text);
            int malzemesevkdurum = Convert.ToInt16(sorgu.ExecuteScalar());

            sorgu.CommandText = "SELECT DURUM FROM MALZEMELER WHERE SERI_NO = @SORGU13 ";
            sorgu.Parameters.AddWithValue("@SORGU13", txtserino_tamirsevkolustur.Text);
            int malzemedurum = Convert.ToInt16(sorgu.ExecuteScalar());

            sorgu.CommandText = "SELECT GELEN_SEVK_ID FROM MALZEME_TAMIR_ISLEM WHERE M_SERINO = @SORGU14 AND ISNULL(SEVK_KONTROL, 'FALSE') = 'FALSE' ";
            sorgu.Parameters.AddWithValue("@SORGU14", txtserino_tamirsevkolustur.Text);
            int tamiregelensevkid = Convert.ToInt16(sorgu.ExecuteScalar()); // malzeme hiç eklenmemişse sevk_kontrolü false oluyor. Tamir depodan başka il'e sevk için eklenmişse sevk_kontrrol true olduğu için  sorgu sonucu sıfır çıkıyor. bu durumda MALZEMELER SEVKDURUM devreye giriyor ve malzeme mükerrer kayıt edilmemiş oluyor.

            sorgu1.CommandText = "SELECT ISNULL(ISLEMYAPILDIMI, 'FALSE') FROM MALZEME_TAMIR_ISLEM  WHERE M_ID=  '" + malzemeid + "' AND  GELEN_SEVK_ID='" + tamiregelensevkid + "' ";
            int malzemetamiredildimi = Convert.ToInt16(sorgu1.ExecuteScalar());

            sorgu.CommandText = "SELECT COUNT(*) FROM MALZEME_SEVK_MALZEMELER WHERE MALZEME_ID='" + malzemeid + "' AND  TAMIRE_GELEN_SEVK_ID='" + tamiregelensevkid + "' ";
            int sayi = Convert.ToInt16(sorgu.ExecuteScalar());

            if (sayi > 0)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), System.Guid.NewGuid().ToString(), "YeniMesaj('Bu malzeme daha önce eklenmiş. ','Hata');", true);
                ScriptManager.RegisterStartupScript(this, this.GetType(), System.Guid.NewGuid().ToString(), "ShowMessage('Bu malzeme daha önce eklenmiş.','Hata','yeni2');", true);
            }
            else

                if (tamiredilecekdepo_sevkhedefdepo != sevkkaynakdepo)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), System.Guid.NewGuid().ToString(), "YeniMesaj('Malzemenin tamir edildiği depo ile Sevk Kaynak Depo aynı değil. ','Hata');", true);
                ScriptManager.RegisterStartupScript(this, this.GetType(), System.Guid.NewGuid().ToString(), "ShowMessage('Malzemenin tamir edildiği depo ile Sevk Kaynak Depo aynı değil.','Hata','yeni2');", true);
            }
            else

                    if (malzemetamiredildimi == 0)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), System.Guid.NewGuid().ToString(), "YeniMesaj('Henüz bu malzeme üzerinde TAMİR işlemi yapılmamış.','Hata');", true);
                ScriptManager.RegisterStartupScript(this, this.GetType(), System.Guid.NewGuid().ToString(), "ShowMessage('Henüz bu malzeme üzerinde TAMİR işlemi yapılmamış.','Hata','yeni2');", true);

            }
            else
                        if (malzemesevkdurum == 1)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), System.Guid.NewGuid().ToString(), "YeniMesaj('Bu malzeme sevk durumunda.','Hata');", true);
                ScriptManager.RegisterStartupScript(this, this.GetType(), System.Guid.NewGuid().ToString(), "ShowMessage('Bu malzeme sevk durumunda.','Hata','yeni2');", true);
            }
            else
            {
                try
                {
                    string userid = Session["KULLANICI_ID"].ToString();

                    sorgu.CommandText = "INSERT INTO    MALZEME_SEVK_MALZEMELER (MALZEME_ID, SEVK_ID, DURUM, TAMIRE_GELEN_SEVK_ID, KAYIT_EDEN , KAYIT_TARIHI)  VALUES(@MI, @SI, @DU, @TGSID, @UI, getdate() ) ";
                    sorgu.Parameters.AddWithValue("@MI", malzemeid);
                    sorgu.Parameters.AddWithValue("@SI", lblidsevk.Text);
                    sorgu.Parameters.AddWithValue("@DU", malzemedurum);
                    sorgu.Parameters.AddWithValue("@TGSID", tamiregelensevkid);
                    sorgu.Parameters.AddWithValue("@UI", userid);
                    sorgu.ExecuteNonQuery();

                    sorgu.CommandText = "UPDATE MALZEMELER SET SEVKDURUMU=@SD, TAMIRDEPO_KONTROL=@TDK  WHERE ID='" + malzemeid + "' ";
                    sorgu.Parameters.AddWithValue("@SD", 1);
                    sorgu.Parameters.AddWithValue("@TDK", 0);
                    sorgu.ExecuteNonQuery();

                    sorgu.CommandText = "UPDATE MALZEME_TAMIR_ISLEM SET SEVK_KONTROL=@SK, SEVK_DURUM=@SDD  WHERE M_ID='" + malzemeid + "' AND GELEN_SEVK_ID ='" + tamiregelensevkid + "'  ";
                    sorgu.Parameters.AddWithValue("@SK", 1);
                    sorgu.Parameters.AddWithValue("@SDD", "1");
                    sorgu.ExecuteNonQuery();

                    // ------   ***   ------ \\
                    // ------   LOG   ------ \\
                    // ------   ***   ------ \\
                    sorgu.CommandText = "SELECT KAYNAK_BOLGE FROM MALZEME_SEVK WHERE ID = @SORGU15 ";
                    sorgu.Parameters.AddWithValue("@SORGU15", lblidsevk.Text);
                    int kaynakbolgeid = Convert.ToInt16(sorgu.ExecuteScalar());

                    sorgu.CommandText = "SELECT KAYNAK_DEPO FROM MALZEME_SEVK WHERE ID = @SORGU16 ";
                    sorgu.Parameters.AddWithValue("@SORGU16", lblidsevk.Text);
                    int kaynakdepoid = Convert.ToInt16(sorgu.ExecuteScalar());

                    sorgu.CommandText = "SELECT HEDEF_BOLGE FROM MALZEME_SEVK WHERE ID = @SORGU17 ";
                    sorgu.Parameters.AddWithValue("@SORGU17", lblidsevk.Text);
                    int hedefbolgeid = Convert.ToInt16(sorgu.ExecuteScalar());

                    sorgu.CommandText = "SELECT HEDEF_DEPO FROM MALZEME_SEVK WHERE ID = @SORGU18 ";
                    sorgu.Parameters.AddWithValue("@SORGU18", lblidsevk.Text);
                    int hedefdepoid = Convert.ToInt16(sorgu.ExecuteScalar());

                    sorgu.CommandText = "INSERT INTO    MALZEMELER_LOG (M_ID, SEVK_ID, E_BOLGE, E_DEPO, Y_BOLGE, Y_DEPO, ACIKLAMA, E_DURUM, KAYIT_EDEN, KAYIT_TARIHI) " +
                        " VALUES(@MID, @SID, @EBOLGE, @EDEPO, @YBOLGE, @YDEPO, @ACIKLAMA, @EDURUM, @UI, getdate() ) ";
                    sorgu.Parameters.Clear();
                    sorgu.Parameters.AddWithValue("@MID", malzemeid);
                    sorgu.Parameters.AddWithValue("@SID", lblidsevk.Text);
                    sorgu.Parameters.AddWithValue("@EBOLGE", kaynakbolgeid);
                    sorgu.Parameters.AddWithValue("@EDEPO", kaynakdepoid);
                    sorgu.Parameters.AddWithValue("@YBOLGE", "0");
                    sorgu.Parameters.AddWithValue("@YDEPO", "0");
                    sorgu.Parameters.AddWithValue("@ACIKLAMA", "Malzeme Tamir işleminden sonra Sevk sürecine dahil edildi.");
                    sorgu.Parameters.AddWithValue("@EDURUM", malzemedurum);
                    sorgu.Parameters.AddWithValue("@UI", userid);
                    sorgu.ExecuteNonQuery();
                    // ------   ***   ------ \\
                    // ------   LOG   ------ \\
                    // ------   ***   ------ \\

                    conn.Close();

                    listele_sevkmalzemeleri_tamirsevkolustur();
                    listele_bolge_sevkmalzemeekle();

                    ScriptManager.RegisterStartupScript(this, this.GetType(), System.Guid.NewGuid().ToString(), "YeniMesaj('Malzeme başarıyla eklendi.','Tamam');", true);
                    ScriptManager.RegisterStartupScript(this, this.GetType(), System.Guid.NewGuid().ToString(), "ShowMessage('Malzeme başarıyla eklendi.','Tamam','yeni2');", true);
                }
                catch (Exception ex)
                {

                    ScriptManager.RegisterStartupScript(this, this.GetType(), System.Guid.NewGuid().ToString(), "YeniMesaj('Beklenmeyen bir hata oluştu.','Hata');", true);
                    ScriptManager.RegisterStartupScript(this, this.GetType(), System.Guid.NewGuid().ToString(), "ShowMessage('Hata oluştu :'" + ex.Message.Replace("'", "") + ",'Hata','yeni2');", true);
                }
                finally
                {
                    if (ConnectionState.Open == conn.State)
                        conn.Close();
                }
            }
            conn.Close();
            listele_sevk();
        }

        protected void btnsevkiadeet_tamirsevkolustur_Click(object sender, EventArgs e)
        {
            SqlCommand sorgu = new SqlCommand();
            sorgu.Connection = conn;
            SqlCommand sorgu1 = new SqlCommand();
            sorgu1.Connection = conn;
            SqlCommand sorgu5 = new SqlCommand();
            sorgu5.Connection = conn;
            conn.Open();

            try
            {
                string userid = Session["KULLANICI_ID"].ToString();
                sorgu.CommandText = "UPDATE MALZEME_SEVK SET SEVK_DURUM=@SD, IADE_EDEN=@UI, IADE_TARIHI=getdate()   WHERE ID=@SORGU19 ";
                sorgu.Parameters.AddWithValue("@SD", 4);
                sorgu.Parameters.AddWithValue("@UI", userid);
                sorgu.Parameters.AddWithValue("@SORGU19", lblidsevk.Text);
                sorgu.ExecuteNonQuery();
                string malzemeserino;

                for (int i = 0; i < grid_sevk_malzeme_sec_tamirsevkolustur.Rows.Count; i++)
                {
                    malzemeserino = HttpUtility.HtmlDecode(grid_sevk_malzeme_sec_tamirsevkolustur.Rows[i].Cells[4].Text).Trim();
                    sorgu.CommandText = "SELECT BOLGE_ID FROM MALZEMELER WHERE SERI_NO = '" + malzemeserino + "' ";
                    int bolgeid = Convert.ToInt16(sorgu.ExecuteScalar());

                    sorgu.CommandText = "SELECT DEPO_ID FROM MALZEMELER WHERE SERI_NO = '" + malzemeserino + "' ";
                    int depoid = Convert.ToInt16(sorgu.ExecuteScalar());

                    sorgu.CommandText = "SELECT ONCEKI_BOLGE_ID FROM MALZEMELER WHERE SERI_NO = '" + malzemeserino + "' ";
                    int oncekibolgeid = Convert.ToInt16(sorgu.ExecuteScalar());

                    sorgu.CommandText = "SELECT ONCEKI_DEPO_ID FROM MALZEMELER WHERE SERI_NO = '" + malzemeserino + "' ";
                    int oncekidepoid = Convert.ToInt16(sorgu.ExecuteScalar());


                    sorgu1.CommandText = "UPDATE MALZEMELER SET BOLGE_ID=@BI, DEPO_ID=@DI, ONCEKI_BOLGE_ID=@OBI, ONCEKI_DEPO_ID=@ODI WHERE SERI_NO='" + malzemeserino + "'  ";
                    sorgu1.Parameters.Clear();
                    sorgu1.Parameters.AddWithValue("@DI", oncekidepoid);
                    sorgu1.Parameters.AddWithValue("@BI", oncekibolgeid);
                    sorgu1.Parameters.AddWithValue("@OBI", bolgeid);
                    sorgu1.Parameters.AddWithValue("@ODI", depoid);

                    grid_sevk_malzeme_sec_tamirsevkolustur.Rows[i].BackColor = Color.YellowGreen;

                    sorgu1.ExecuteNonQuery();

                    // ------   ***   ------ \\
                    // ------   LOG   ------ \\
                    // ------   ***   ------ \\
                    lblmalzemeid.Text = tkod.sql_calistir1("SELECT ID FROM MALZEMELER WHERE SERI_NO= '" + malzemeserino + "' ");

                    sorgu5.CommandText = "INSERT INTO    MALZEMELER_LOG (M_ID, SEVK_ID, E_BOLGE, E_DEPO, Y_BOLGE, Y_DEPO, ACIKLAMA, KAYIT_EDEN, KAYIT_TARIHI) " +
                        " VALUES(@MID, @SID, @EBOLGE, @EDEPO, @YBOLGE, @YDEPO, @ACIKLAMA, @UI, getdate() ) ";
                    sorgu5.Parameters.Clear();
                    sorgu5.Parameters.AddWithValue("@MID", lblmalzemeid.Text);
                    sorgu5.Parameters.AddWithValue("@SID", lblidsevk.Text);
                    sorgu5.Parameters.AddWithValue("@EBOLGE", bolgeid);
                    sorgu5.Parameters.AddWithValue("@EDEPO", depoid);
                    sorgu5.Parameters.AddWithValue("@YBOLGE", oncekibolgeid);
                    sorgu5.Parameters.AddWithValue("@YDEPO", oncekidepoid);
                    sorgu5.Parameters.AddWithValue("@ACIKLAMA", "Sevk iade edildi. (Tamir depoya iade)");
                    sorgu5.Parameters.AddWithValue("@UI", userid);
                    sorgu5.ExecuteNonQuery();
                    // ------   ***   ------ \\
                    // ------   LOG   ------ \\
                    // ------   ***   ------ \\
                }

                conn.Close();
                modal_kapat("ModalTamir_MalzemeEkle");

                btnsevkiadeet_tamirsevkolustur.Visible = false;
                btnsevkteslimal_tamirsevkolustur.Visible = false;
                txtserino_tamirsevkolustur.Visible = true;

                lblsevkdurum.Text = "1";

                listele_sevkmalzemeleri();
                listele_sevk();

                ScriptManager.RegisterStartupScript(this, this.GetType(), System.Guid.NewGuid().ToString(), "YeniMesaj('Sevk iade edildi.','Tamam');", true);
                ScriptManager.RegisterStartupScript(this, this.GetType(), System.Guid.NewGuid().ToString(), "ShowMessage('Sevk iade edildi.','Tamam','sevket');", true);
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), System.Guid.NewGuid().ToString(), "YeniMesaj('Beklenmeyen bir hata oluştu.','Hata');", true);
                ScriptManager.RegisterStartupScript(this, this.GetType(), System.Guid.NewGuid().ToString(), "ShowMessage('Hata oluştu :'" + ex.Message.Replace("'", "") + ",'Hata','sevket');", true);
            }
            finally
            {
                if (ConnectionState.Open == conn.State)
                    conn.Close();
            }
            listele_sevk();
            listele_sevkmalzemeleri_tamirsevkolustur();
        }

        // Tamir Depodan Malzeme Seç modalı içeriği

        protected void btnsevkmalzemeekle1_tamirsevkolustur_Click(object sender, EventArgs e)
        {
            lblmodalmalzemesecyonlendir.Text = "ModalTamir_MalzemeEkle";
            modal_ac("ModalTamir_MalzemeSec");
            modal_kapat("ModalTamir_MalzemeEkle");

            lblmodalyenibaslik3.Text = "Tamir Depodan Malzeme Seç";

            listele_bolge_sevkmalzemesec();
            comdurumsec_malzemesec.SelectedValue = "0";
        }

        protected void combolgesec_malzemesec_SelectedIndexChanged(object sender, EventArgs e)
        {
            listele_tamir_malzeme_sec();
            listele_malzemedurum_sevkmalzemesec();
        }

        protected void comdurumsec_malzemesec_SelectedIndexChanged(object sender, EventArgs e)
        {
            listele_tamir_malzeme_sec();
        }

        void listele_bolge_sevkmalzemesec()
        {
            int kaynakdepoid1 = Convert.ToInt16(tkod.sql_calistir_param("SELECT ID FROM DEPO WHERE DEPO=@DEPO", new SqlParameter("DEPO", lblkaynakdepo.Text)));  // KAYNAK DEPO id'sine bakıyor. Bu bilgi tamir depodan malzeme eklenirken malzemenin tamir olduğu depo ile sevk'in kaynak deposunu karşılaştırmak için kullanılacak.

            conn.Open();
            combolgesec_malzemesec.Items.Clear();
            combolgesec_malzemesec.Items.Insert(0, new ListItem("Seçiniz", "0"));
            combolgesec_malzemesec.AppendDataBoundItems = true;
            string sql1 = "";

            SqlDataAdapter da = new SqlDataAdapter();
            SqlCommand sorgu = new SqlCommand("Select BOLGE.BOLGE_ADI, BOLGE.ID FROM MALZEME_TAMIR_ISLEM AS MTI " +
                " LEFT JOIN BOLGE ON BOLGE.ID=MTI.BOLGE_ID WHERE ISNULL(SEVK_KONTROL,'FALSE') = 0 AND ISNULL(ISLEMYAPILDIMI,'FALSE') = 1 AND TAMIR_EDILECEK_DEPO =@KDID GROUP BY BOLGE_ADI,  BOLGE.ID ");
            sorgu.Parameters.AddWithValue("KDID", kaynakdepoid1);

            DataTable dt = new DataTable();
            da.SelectCommand = sorgu;
            da.SelectCommand.Connection = conn;
            da.Fill(dt);
            combolgesec_malzemesec.DataSource = dt;
            combolgesec_malzemesec.DataBind();
            conn.Close();
        }

        void listele_malzemedurum_sevkmalzemesec()
        {
            comdurumsec_malzemesec.Items.Clear();
            comdurumsec_malzemesec.Items.Insert(0, new ListItem("Seçiniz", "0"));
            comdurumsec_malzemesec.AppendDataBoundItems = true;
            string sql2 = "";

            if (combolgesec_malzemesec.SelectedIndex > 0)
                sql2 = " AND MTI.BOLGE_ID='" + combolgesec_malzemesec.SelectedValue.ToString() + "' ";

            string sql = "SELECT MD.DURUM  FROM MALZEME_TAMIR_ISLEM AS MTI LEFT JOIN MALZEME_DURUM AS MD ON MD.ID=MTI.SON_DURUM   " + sql2 + " GROUP BY MD.DURUM ";
            comdurumsec_malzemesec.DataSource = tkod.GetData(sql);
            comdurumsec_malzemesec.DataBind();
        }

        void listele_tamir_malzeme_sec()
        {
            int kaynakdepoid1 = Convert.ToInt16(tkod.sql_calistir_param("SELECT ID FROM DEPO WHERE DEPO=@DEPO", new SqlParameter("DEPO", lblkaynakdepo.Text)));  // KAYNAK DEPO id'sine bakıyor. Bu bilgi tamir depodan malzeme eklenirken malzemenin tamir olduğu depo ile sevk'in kaynak deposunu karşılaştırmak için kullanılacak.

            SqlCommand cmd = new SqlCommand();
            string sql = "", sql1 = "";

            sql = " SELECT MTI.M_ID, BOLGE.BOLGE_ADI, MTI.TESLIM_ALMA_TARIHI, MTI.GELEN_SEVK_ID, MALZEME_TIP.TIP, MALZEME_TURU.TURU," +
                " MALZEME_MARKAMODEL.MARKA, MALZEME_MODEL.MODEL, MTI.M_SERINO, MD.DURUM" +
                " FROM MALZEME_TAMIR_ISLEM AS MTI " +
                " LEFT JOIN BOLGE ON BOLGE.ID=MTI.BOLGE_ID " +
                " LEFT JOIN MALZEME_TIP ON MALZEME_TIP.ID = MTI.M_TIPI" +
                " LEFT JOIN MALZEME_TURU ON MALZEME_TURU.ID=MTI.M_TURU" +
                " LEFT JOIN MALZEME_MARKAMODEL ON MALZEME_MARKAMODEL.ID=MTI.M_MARKA" +
                " LEFT JOIN MALZEME_MODEL ON MALZEME_MODEL.ID=MTI.M_MODEL" +
                " LEFT JOIN MALZEME_DURUM AS MD ON MD.ID=MTI.SON_DURUM" +

                " WHERE MTI.M_ID > 0  AND ISNULL(MTI.SEVK_KONTROL, 'FALSE') = 0 AND ISNULL(MTI.ISLEMYAPILDIMI, 'FALSE') = 1 AND TAMIR_EDILECEK_DEPO =@KDID";
            cmd.Parameters.AddWithValue("KDID", kaynakdepoid1);

            if (combolgesec_malzemesec.SelectedIndex > 0)
                sql1 += " AND (MTI.BOLGE_ID='" + combolgesec_malzemesec.SelectedValue + "' )  ";

            if (comdurumsec_malzemesec.SelectedIndex > 0)
                sql1 += " AND (MD.DURUM='" + comdurumsec_malzemesec.SelectedValue + "' )  ";

            sql = sql + sql1;

            cmd = new SqlCommand(sql, conn);
            if (combolgesec_malzemesec.SelectedIndex > 0)
            {
                conn.Open();

                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.SelectCommand.Parameters.AddWithValue("KDID", kaynakdepoid1);
                DataTable dt = new DataTable();
                da.Fill(dt);
                grid_tamir_malzeme_sec_tamirsevkolustur.DataSource = dt;
                conn.Close();

                grid_tamir_malzeme_sec_tamirsevkolustur.DataBind();
            }
            lblmalzemesayisi_malzemesec.Text = grid_tamir_malzeme_sec_tamirsevkolustur.Rows.Count.ToString() + " " + "adet kayıt bulunmuştur.";
        }

        protected void grid_tamir_malzeme_sec_tamirsevkolustur_RowDataBound(object sender, GridViewRowEventArgs e)
        {
        }

        protected void grid_tamir_malzeme_sec_tamirsevkolustur_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName.Equals("ekle"))
            {
                int index = Convert.ToInt32(e.CommandArgument);
                lblidmalzeme.Text = grid_tamir_malzeme_sec_tamirsevkolustur.DataKeys[index].Value.ToString();
                lbldurummalzeme.Text = HttpUtility.HtmlDecode(grid_tamir_malzeme_sec_tamirsevkolustur.Rows[index].Cells[7].Text.Trim());

                SqlCommand sorgu = new SqlCommand();
                sorgu.Connection = conn;
                SqlCommand sorgu1 = new SqlCommand();
                sorgu1.Connection = conn;
                conn.Open();
                string userid = Session["KULLANICI_ID"].ToString();

                lblmalzemedurumid.Text = tkod.sql_calistir_param("SELECT ID FROM MALZEME_DURUM WHERE DURUM=@DRM", new SqlParameter("DRM", lbldurummalzeme.Text));

                sorgu1.CommandText = "SELECT TAMIR_EDILECEK_DEPO FROM MALZEME_TAMIR_ISLEM WHERE M_ID=@MID AND ISNULL(SEVK_KONTROL, 'FALSE') = 'FALSE' ";
                sorgu1.Parameters.AddWithValue("@MID", lblidmalzeme.Text);
                int tamiredilecekdepo_sevkhedefdepo1 = Convert.ToInt16(sorgu1.ExecuteScalar()); //Tamir edilen malzeme hangi tamir depoda tamir edilmişse sevk'in kaynak deposu'da o olmalı

                sorgu.CommandText = "SELECT KAYNAK_DEPO FROM MALZEME_SEVK WHERE ID = @SORGU20 ";
                sorgu.Parameters.AddWithValue("@SORGU20", lblidsevk.Text);
                int sevkkaynakdepo1 = Convert.ToInt16(sorgu.ExecuteScalar()); //Tamir edilen malzeme hangi tamir depoda tamir edilmişse sevk'in kaynak deposu'da o olmalı

                sorgu.CommandText = "SELECT ISNULL(ISLEMYAPILDIMI, 'FALSE') FROM MALZEME_TAMIR_ISLEM  WHERE M_ID=  @SORGU21 ";
                sorgu.Parameters.AddWithValue("@SORGU21", lblidmalzeme.Text);
                int malzemetamiredildimi = Convert.ToInt16(sorgu.ExecuteScalar());

                sorgu.CommandText = "SELECT GELEN_SEVK_ID FROM MALZEME_TAMIR_ISLEM WHERE M_ID = @SORGU22 AND ISNULL(SEVK_KONTROL, 'FALSE') = 'FALSE' ";
                sorgu.Parameters.AddWithValue("@SORGU22", lblidmalzeme.Text);
                int tamiregelensevkid = Convert.ToInt16(sorgu.ExecuteScalar());

                sorgu.CommandText = "SELECT COUNT(*) FROM MALZEME_SEVK_MALZEMELER WHERE MALZEME_ID=@SORGU23 AND  TAMIRE_GELEN_SEVK_ID='" + tamiregelensevkid + "' ";
                sorgu.Parameters.AddWithValue("@SORGU23", lblidmalzeme.Text);
                int sayi = Convert.ToInt16(sorgu.ExecuteScalar());

                if (tamiredilecekdepo_sevkhedefdepo1 != sevkkaynakdepo1)
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), System.Guid.NewGuid().ToString(), "YeniMesaj('Malzemenin tamir edildiği depo ile Sevk Kaynak Depo aynı değil. ','Hata');", true);
                    ScriptManager.RegisterStartupScript(this, this.GetType(), System.Guid.NewGuid().ToString(), "ShowMessage('Malzemenin tamir edildiği depo ile Sevk Kaynak Depo aynı değil.','Hata','yeni3');", true);
                }
                else
                    if (sayi > 0)
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), System.Guid.NewGuid().ToString(), "YeniMesaj('Bu malzeme daha önce eklenmiş. ','Hata');", true);
                    ScriptManager.RegisterStartupScript(this, this.GetType(), System.Guid.NewGuid().ToString(), "ShowMessage('Bu malzeme daha önce eklenmiş.','Hata','yeni3');", true);
                }
                else
                {
                    if (malzemetamiredildimi == 0)
                    {
                        ScriptManager.RegisterStartupScript(this, this.GetType(), System.Guid.NewGuid().ToString(), "YeniMesaj('Henüz bu malzeme üzerinde TAMİR işlemi yapılmamış.','Hata');", true);
                        ScriptManager.RegisterStartupScript(this, this.GetType(), System.Guid.NewGuid().ToString(), "ShowMessage('Henüz bu malzeme üzerinde TAMİR işlemi yapılmamış.','Hata','yeni3');", true);
                    }
                    else
                    {
                        try
                        {
                            sorgu.CommandText = "INSERT INTO    MALZEME_SEVK_MALZEMELER (MALZEME_ID, SEVK_ID, DURUM, TAMIRE_GELEN_SEVK_ID, KAYIT_EDEN , KAYIT_TARIHI)  VALUES(@MI, @SI, @DU, @TGSID, @UI, getdate() ) ";
                            sorgu.Parameters.AddWithValue("@MI", lblidmalzeme.Text);
                            sorgu.Parameters.AddWithValue("@SI", lblidsevk.Text);
                            sorgu.Parameters.AddWithValue("@DU", lblmalzemedurumid.Text);
                            sorgu.Parameters.AddWithValue("@TGSID", tamiregelensevkid);
                            sorgu.Parameters.AddWithValue("@UI", userid);
                            sorgu.ExecuteNonQuery();

                            sorgu.CommandText = "UPDATE MALZEMELER SET SEVKDURUMU=@SD, TAMIRDEPO_KONTROL=@TDK  WHERE ID=@SORGU24 ";
                            sorgu.Parameters.AddWithValue("@SD", 1);
                            sorgu.Parameters.AddWithValue("@TDK", 0);
                            sorgu.Parameters.AddWithValue("@SORGU24", lblidmalzeme.Text);
                            sorgu.ExecuteNonQuery();

                            sorgu.CommandText = "UPDATE MALZEME_TAMIR_ISLEM SET SEVK_KONTROL=@SK  WHERE M_ID=@SORGU25 ";
                            sorgu.Parameters.AddWithValue("@SK", 1);
                            sorgu.Parameters.AddWithValue("@SORGU25", lblidmalzeme.Text);
                            sorgu.ExecuteNonQuery();

                            // ------   ***   ------ \\
                            // ------   LOG   ------ \\
                            // ------   ***   ------ \\
                            sorgu.CommandText = "SELECT KAYNAK_BOLGE FROM MALZEME_SEVK WHERE ID = @SORGU26 ";
                            sorgu.Parameters.AddWithValue("@SORGU26", lblidsevk.Text);
                            int kaynakbolgeid = Convert.ToInt16(sorgu.ExecuteScalar());

                            sorgu.CommandText = "SELECT KAYNAK_DEPO FROM MALZEME_SEVK WHERE ID = @SORGU27 ";
                            sorgu.Parameters.AddWithValue("@SORGU27", lblidsevk.Text);
                            int kaynakdepoid = Convert.ToInt16(sorgu.ExecuteScalar());

                            sorgu.CommandText = "SELECT HEDEF_BOLGE FROM MALZEME_SEVK WHERE ID = @SORGU28 ";
                            sorgu.Parameters.AddWithValue("@SORGU28", lblidsevk.Text);
                            int hedefbolgeid = Convert.ToInt16(sorgu.ExecuteScalar());

                            sorgu.CommandText = "SELECT HEDEF_DEPO FROM MALZEME_SEVK WHERE ID = @SORGU29 ";
                            sorgu.Parameters.AddWithValue("@SORGU29", lblidsevk.Text);
                            int hedefdepoid = Convert.ToInt16(sorgu.ExecuteScalar());

                            sorgu.CommandText = "INSERT INTO    MALZEMELER_LOG (M_ID, SEVK_ID, E_BOLGE, E_DEPO, Y_BOLGE, Y_DEPO, ACIKLAMA, E_DURUM, KAYIT_EDEN, KAYIT_TARIHI) " +
                                " VALUES(@MID, @SID, @EBOLGE, @EDEPO, @YBOLGE, @YDEPO, @ACIKLAMA, @EDURUM, @UI, getdate() ) ";
                            sorgu.Parameters.Clear();
                            sorgu.Parameters.AddWithValue("@MID", lblidmalzeme.Text);
                            sorgu.Parameters.AddWithValue("@SID", lblidsevk.Text);
                            sorgu.Parameters.AddWithValue("@EBOLGE", kaynakbolgeid);
                            sorgu.Parameters.AddWithValue("@EDEPO", kaynakdepoid);
                            sorgu.Parameters.AddWithValue("@YBOLGE", "0");
                            sorgu.Parameters.AddWithValue("@YDEPO", "0");
                            sorgu.Parameters.AddWithValue("@ACIKLAMA", "Malzeme Tamir işleminden sonra Sevk sürecine dahil edildi.");
                            sorgu.Parameters.AddWithValue("@EDURUM", lblmalzemedurumid.Text);
                            sorgu.Parameters.AddWithValue("@UI", userid);
                            sorgu.ExecuteNonQuery();
                            // ------   ***   ------ \\
                            // ------   LOG   ------ \\
                            // ------   ***   ------ \\

                            grid_tamir_malzeme_sec_tamirsevkolustur.DataBind();
                            conn.Close();

                            listele_tamir_malzeme_sec();
                            //listele_bolge_sevkmalzemesec();

                            listele_sevkmalzemeleri_tamirsevkolustur();
                            listele_bolge_sevkmalzemeekle();

                            ScriptManager.RegisterStartupScript(this, this.GetType(), System.Guid.NewGuid().ToString(), "YeniMesaj('Malzeme başarıyla eklendi.','Tamam');", true);
                            ScriptManager.RegisterStartupScript(this, this.GetType(), System.Guid.NewGuid().ToString(), "ShowMessage('Malzeme başarıyla eklendi.','Tamam','yeni3');", true);
                        }
                        catch (Exception ex)
                        {

                            ScriptManager.RegisterStartupScript(this, this.GetType(), System.Guid.NewGuid().ToString(), "YeniMesaj('Beklenmeyen bir hata oluştu.','Hata');", true);
                            ScriptManager.RegisterStartupScript(this, this.GetType(), System.Guid.NewGuid().ToString(), "ShowMessage('Hata oluştu :'" + ex.Message.Replace("'", "") + ",'Hata','yeni3');", true);
                        }
                        finally
                        {
                            if (ConnectionState.Open == conn.State)
                                conn.Close();
                        }
                    }
                }
                listele_sevk();
            }
        }

        protected void grid_sevk_malzeme_sec_tamirsevkolustur_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            grid_sevk_malzeme_sec_tamirsevkolustur.PageIndex = e.NewPageIndex;
            listele_sevkmalzemeleri_tamirsevkolustur();
        }

        protected void grid_tamir_malzeme_sec_tamirsevkolustur_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            grid_tamir_malzeme_sec_tamirsevkolustur.PageIndex = e.NewPageIndex;
            listele_tamir_malzeme_sec();
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
                " LEFT JOIN KULLANICI ON KULLANICI.ID=MLOG.KAYIT_EDEN WHERE MLOG.M_ID = '" + lblidcihaz_ariza.Text + "'  AND ACIKLAMA LIKE 'Durum Değişikliği yapıldı%' ";

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

        // Sevk Log
        protected void btnhareket_Click(object sender, EventArgs e)
        {
            modal_kapat("ModalMalzemeEkle");
            modal_ac("ModalHareket");

            lblmodalyenibaslik6.Text = "Sevk Hareket";

            lblmodalyonlendirhareket.Text = "ModalMalzemeEkle";
            listele_sevkhareket();
        }

        protected void btnsevkiptal_Click(object sender, EventArgs e)
        {
            if (grid_sevk_malzeme_sec.Rows.Count > 0)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), System.Guid.NewGuid().ToString(), "YeniMesaj('Sevk sürecinin iptal olabilmesi için öncelikle malzemelerin silinmesi gerekmektedir.','Hata');", true);
                ScriptManager.RegisterStartupScript(this, this.GetType(), System.Guid.NewGuid().ToString(), "ShowMessage('Sevk sürecinin iptal olabilmesi için öncelikle malzemelerin silinmesi gerekmektedir.','Hata','sevket');", true);
            }
            else
            {
                SqlCommand sorgu = new SqlCommand();
                sorgu.Connection = conn;
                conn.Open();
                try
                {
                    string userid = Session["KULLANICI_ID"].ToString();
                    sorgu.CommandText = "UPDATE MALZEME_SEVK SET SEVK_DURUM=@SD WHERE ID=@MSID8 ";
                    sorgu.Parameters.AddWithValue("@SD", 5);
                    sorgu.Parameters.AddWithValue("@MSID8", lblidsevk.Text);
                    sorgu.ExecuteNonQuery();

                    // ------   ***   ------ \\
                    // ------   LOG   ------ \\
                    // ------   ***   ------ \\
                    sorgu.CommandText = "INSERT INTO    SEVK_LOG (SEVK_ID, SEVK_DURUM_ID, SILEN_ID, SILME_TARIHI, ACIKLAMA) " +
                        " VALUES(@SID_LOG, @SD_LOG, @SIL_LOG, getdate(), @ACKLM_LOG ) ";
                    sorgu.Parameters.AddWithValue("@SID_LOG", lblidsevk.Text);
                    sorgu.Parameters.AddWithValue("@SD_LOG", 5);
                    sorgu.Parameters.AddWithValue("@SIL_LOG", userid);
                    sorgu.Parameters.AddWithValue("@ACKLM_LOG", "Sevk İptal edildi.");
                    sorgu.ExecuteNonQuery();
                    // ------   ***   ------ \\
                    // ------   LOG   ------ \\
                    // ------   ***   ------ \\

                    conn.Close();

                    btnsevkiptal.Visible = false;
                    btnsevket.Visible = false;
                    btnhareket.Visible = false;
                    modal_kapat("ModalMalzemeEkle");

                    ScriptManager.RegisterStartupScript(this, this.GetType(), System.Guid.NewGuid().ToString(), "YeniMesaj('Sevk iptal edildi.','Tamam');", true);
                    ScriptManager.RegisterStartupScript(this, this.GetType(), System.Guid.NewGuid().ToString(), "ShowMessage('Sevk iptal edildi.','Tamam','sevket');", true);
                }
                catch (Exception ex)
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), System.Guid.NewGuid().ToString(), "YeniMesaj('Beklenmeyen bir hata oluştu.','Hata');", true);
                    ScriptManager.RegisterStartupScript(this, this.GetType(), System.Guid.NewGuid().ToString(), "ShowMessage('Hata oluştu :'" + ex.Message.Replace("'", "") + ",'Hata','sevket');", true);
                }
                finally
                {
                    if (ConnectionState.Open == conn.State)
                        conn.Close();
                }
                listele_sevk();
            }
        }

        protected void btnhareket_tamirsevkolustur_Click(object sender, EventArgs e)
        {
            lblmodalyenibaslik6.Text = "Sevk Hareket";
            modal_ac("ModalHareket");

            modal_kapat("ModalTamir_MalzemeEkle");
            lblmodalyonlendirhareket.Text = "ModalTamir_MalzemeEkle";

            listele_sevkhareket();
        }

        protected void btnsevkiptal_tamirsevkolustur_Click(object sender, EventArgs e)
        {
            if (grid_sevk_malzeme_sec_tamirsevkolustur.Rows.Count > 0)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), System.Guid.NewGuid().ToString(), "YeniMesaj('Sevk sürecinin iptal olabilmesi için öncelikle malzemelerin silinmesi gerekmektedir.','Hata');", true);
                ScriptManager.RegisterStartupScript(this, this.GetType(), System.Guid.NewGuid().ToString(), "ShowMessage('Sevk sürecinin iptal olabilmesi için öncelikle malzemelerin silinmesi gerekmektedir.','Hata','sevket');", true);
            }
            else
            {
                SqlCommand sorgu = new SqlCommand();
                sorgu.Connection = conn;
                conn.Open();
                try
                {
                    string userid = Session["KULLANICI_ID"].ToString();
                    sorgu.CommandText = "UPDATE MALZEME_SEVK SET SEVK_DURUM=@SD WHERE ID=@MSID8 ";
                    sorgu.Parameters.AddWithValue("@SD", 5);
                    sorgu.Parameters.AddWithValue("@MSID8", lblidsevk.Text);
                    sorgu.ExecuteNonQuery();

                    // ------   ***   ------ \\
                    // ------   LOG   ------ \\
                    // ------   ***   ------ \\
                    sorgu.CommandText = "INSERT INTO    SEVK_LOG (SEVK_ID, SEVK_DURUM_ID, SILEN_ID, SILME_TARIHI, ACIKLAMA) " +
                        " VALUES(@SID_LOG, @SD_LOG, @SIL_LOG, getdate(), @ACKLM_LOG ) ";
                    sorgu.Parameters.AddWithValue("@SID_LOG", lblidsevk.Text);
                    sorgu.Parameters.AddWithValue("@SD_LOG", 5);
                    sorgu.Parameters.AddWithValue("@SIL_LOG", userid);
                    sorgu.Parameters.AddWithValue("@ACKLM_LOG", "Sevk İptal edildi. (Kaynak Tamir depo)");
                    sorgu.ExecuteNonQuery();
                    // ------   ***   ------ \\
                    // ------   LOG   ------ \\
                    // ------   ***   ------ \\

                    conn.Close();
                    modal_kapat("ModalTamir_MalzemeEkle");

                    btnsevkiptal_tamirsevkolustur.Visible = false;
                    btnsevket_tamirsevkolustur.Visible = false;
                    btnsevkmalzemeexcel_tamirsevkolustur.Visible = false;
                    btnhareket_tamirsevkolustur.Visible = false;

                    ScriptManager.RegisterStartupScript(this, this.GetType(), System.Guid.NewGuid().ToString(), "YeniMesaj('Sevk iptal edildi.','Tamam');", true);
                    ScriptManager.RegisterStartupScript(this, this.GetType(), System.Guid.NewGuid().ToString(), "ShowMessage('Sevk iptal edildi.','Tamam','sevket');", true);
                }
                catch (Exception ex)
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), System.Guid.NewGuid().ToString(), "YeniMesaj('Beklenmeyen bir hata oluştu.','Hata');", true);
                    ScriptManager.RegisterStartupScript(this, this.GetType(), System.Guid.NewGuid().ToString(), "ShowMessage('Hata oluştu :'" + ex.Message.Replace("'", "") + ",'Hata','sevket');", true);
                }
                finally
                {
                    if (ConnectionState.Open == conn.State)
                        conn.Close();
                }
                listele_sevk();
            }
        }

        protected void btnislemler_Click(object sender, EventArgs e)
        {
            modal_kapat("ModalSevkOlustur");
        }
        
        protected void chc_garantisiz_CheckedChanged(object sender, EventArgs e)
        {
            SqlCommand sorgu = new SqlCommand();
            sorgu.Connection = conn;
            conn.Open();
            if (chc_garantisiz.Checked == true)
            {
                try
                {
                    chc_garantisiz.ToolTip = "Garantisiz Malzeme Eklenebilir";
                    lblgarantisiz_mlz_kbl.Text = "1";

                    sorgu.CommandText = "UPDATE MALZEME_SEVK SET GARANTISIZ_MALZEME_KABUL=@GME  WHERE ID=@OFOF ";
                    sorgu.Parameters.AddWithValue("@GME", lblgarantisiz_mlz_kbl.Text);
                    sorgu.Parameters.AddWithValue("@OFOF", lblidsevk.Text);
                    sorgu.ExecuteNonQuery();
                    ScriptManager.RegisterStartupScript(this, this.GetType(), System.Guid.NewGuid().ToString(), "YeniMesaj('Garantisiz Malzeme Eklenebilir.','Tamam');", true);
                }
                catch (Exception ex)
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), System.Guid.NewGuid().ToString(), "YeniMesaj('Beklenmeyen bir hata oluştu.','Hata');", true);
                    ScriptManager.RegisterStartupScript(this, this.GetType(), System.Guid.NewGuid().ToString(), "ShowMessage('Hata oluştu :'" + ex.Message.Replace("'", "") + ",'Hata','sevket');", true);
                }
                finally
                {
                    if (ConnectionState.Open == conn.State)
                        conn.Close();
                }
            }
            else
            {
                try
                {
                    chc_garantisiz.ToolTip = "Garantisiz Malzeme EKLENEMEZ";
                    lblgarantisiz_mlz_kbl.Text = "0";
                    sorgu.CommandText = "UPDATE MALZEME_SEVK SET GARANTISIZ_MALZEME_KABUL=@GME1  WHERE ID=@OFOF1 ";
                    sorgu.Parameters.AddWithValue("@GME1", lblgarantisiz_mlz_kbl.Text);
                    sorgu.Parameters.AddWithValue("@OFOF1", lblidsevk.Text);
                    sorgu.ExecuteNonQuery();
                    ScriptManager.RegisterStartupScript(this, this.GetType(), System.Guid.NewGuid().ToString(), "YeniMesaj('Garantisiz Malzeme EKLENEMEZ.','Tamam');", true);
                }
                catch (Exception ex)
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), System.Guid.NewGuid().ToString(), "YeniMesaj('Beklenmeyen bir hata oluştu.','Hata');", true);
                    ScriptManager.RegisterStartupScript(this, this.GetType(), System.Guid.NewGuid().ToString(), "ShowMessage('Hata oluştu :'" + ex.Message.Replace("'", "") + ",'Hata','sevket');", true);
                }
                finally
                {
                    if (ConnectionState.Open == conn.State)
                        conn.Close();
                }
            }
            conn.Close();
        }

        protected void chc_garantili_CheckedChanged(object sender, EventArgs e)
        {
            SqlCommand sorgu = new SqlCommand();
            sorgu.Connection = conn;
            conn.Open();
            if (chc_garantili.Checked == true)
            {
                try
                {
                    chc_garantili.ToolTip = "Garantili Malzeme Eklenebilir";
                    lblgarantili_mlz_kbl.Text = "1";

                    sorgu.CommandText = "UPDATE MALZEME_SEVK SET GARANTILI_MALZEME_KABUL=@GME  WHERE ID=@OFOF ";
                    sorgu.Parameters.AddWithValue("@GME", lblgarantili_mlz_kbl.Text);
                    sorgu.Parameters.AddWithValue("@OFOF", lblidsevk.Text);
                    sorgu.ExecuteNonQuery();
                    ScriptManager.RegisterStartupScript(this, this.GetType(), System.Guid.NewGuid().ToString(), "YeniMesaj('Garantili Malzeme Eklenebilir.','Tamam');", true);
                }
                catch (Exception ex)
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), System.Guid.NewGuid().ToString(), "YeniMesaj('Beklenmeyen bir hata oluştu.','Hata');", true);
                    ScriptManager.RegisterStartupScript(this, this.GetType(), System.Guid.NewGuid().ToString(), "ShowMessage('Hata oluştu :'" + ex.Message.Replace("'", "") + ",'Hata','sevket');", true);
                }
                finally
                {
                    if (ConnectionState.Open == conn.State)
                        conn.Close();
                }
            }
            else
            {
                try
                {
                    chc_garantili.ToolTip = "Garantili Malzeme EKLENEMEZ";
                    lblgarantili_mlz_kbl.Text = "0";
                    sorgu.CommandText = "UPDATE MALZEME_SEVK SET GARANTILI_MALZEME_KABUL=@GME1  WHERE ID=@OFOF1 ";
                    sorgu.Parameters.AddWithValue("@GME1", lblgarantili_mlz_kbl.Text);
                    sorgu.Parameters.AddWithValue("@OFOF1", lblidsevk.Text);
                    sorgu.ExecuteNonQuery();
                    ScriptManager.RegisterStartupScript(this, this.GetType(), System.Guid.NewGuid().ToString(), "YeniMesaj('Garantili Malzeme EKLENEMEZ.','Tamam');", true);
                }
                catch (Exception ex)
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), System.Guid.NewGuid().ToString(), "YeniMesaj('Beklenmeyen bir hata oluştu.','Hata');", true);
                    ScriptManager.RegisterStartupScript(this, this.GetType(), System.Guid.NewGuid().ToString(), "ShowMessage('Hata oluştu :'" + ex.Message.Replace("'", "") + ",'Hata','sevket');", true);
                }
                finally
                {
                    if (ConnectionState.Open == conn.State)
                        conn.Close();
                }
            }
            conn.Close();
        }

        protected void grid_sevk_listele_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            grid_sevk_listele.PageIndex = e.NewPageIndex;
            listele_sevk();
        }

        protected void btnarizamodalkapat_Click(object sender, EventArgs e)
        {
            modal_kapat("ModalAriza");

            if (lblmodalarizayonlendir.Text != "")
                modal_ac(lblmodalarizayonlendir.Text);
            lblmodalarizayonlendir.Text = "";
        }

        protected void btnhareketkapat_Click(object sender, EventArgs e)
        {
            modal_kapat("ModalHareket");

            if (lblmodalyonlendirhareket.Text != "")
                modal_ac(lblmodalyonlendirhareket.Text);

            lblmodalyonlendirhareket.Text = "";
        }

        protected void btnmodalsilkapat_Click(object sender, EventArgs e)
        {
            modal_kapat("ModalSil");

            if (lblmodalsilyonlendir.Text != "")
                modal_ac(lblmodalsilyonlendir.Text);

            lblmodalsilyonlendir.Text = "";
        }

        protected void btnmodalmalzemsectemirkapat_Click(object sender, EventArgs e)
        {
            modal_kapat("ModalTamir_MalzemeSec");

            if (lblmodalmalzemesecyonlendir.Text != "")
                modal_ac(lblmodalmalzemesecyonlendir.Text);

            lblmodalmalzemesecyonlendir.Text = "";
        }
    }
}