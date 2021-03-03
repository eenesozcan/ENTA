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
    public partial class VarlikYonetimi : System.Web.UI.Page
    {
        SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["KabloHE"].ConnectionString);
        string sayfa = "VarlikYonetimi";
        DataTable dt;
        KodT.kodlar tkod = new KodT.kodlar();
        string id;
        string varlikid;
        string malzemeid;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["KULLANICI_ID"] == null)
            {
                Response.Redirect("Giris.aspx?url=" + sayfa);
            }
            if (!IsPostBack)
            {
                bolge_ara();
                lokasyon_ara();
                tip_ara();
                ad_ara();
                listele_varlik();
            }
        }
        // Varlık sayfası GRİD
        protected void grid_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName.Equals("sil"))
            {
                int index = Convert.ToInt32(e.CommandArgument) % grid.PageSize;
                lblidsil.Text = grid.DataKeys[index].Value.ToString();
                //int index = Convert.ToInt32(e.CommandArgument);
                //lblidsil.Text = grid.DataKeys[index].Value.ToString();
                lblislem.Text = "varlik-sil";
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
                comil_kayit.Enabled = false;
                combolge_kayit.Enabled = false;
                comlokasyon_kayit.Enabled = false;
                comtip_kayit.Enabled = false;

                int index = Convert.ToInt32(e.CommandArgument) % grid.PageSize;
                id = grid.DataKeys[index].Value.ToString();
                lblidcihaz.Text = id;
                
                il_doldur();
                comil_kayit.SelectedIndex = comil_kayit.Items.IndexOf(comil_kayit.Items.FindByText(HttpUtility.HtmlDecode(grid.Rows[index].Cells[1].Text)));

                bolge_doldur();
                combolge_kayit.SelectedIndex = combolge_kayit.Items.IndexOf(combolge_kayit.Items.FindByText(HttpUtility.HtmlDecode(grid.Rows[index].Cells[2].Text)));

                lokasyon_doldur();
                comlokasyon_kayit.SelectedIndex = comlokasyon_kayit.Items.IndexOf(comlokasyon_kayit.Items.FindByText(HttpUtility.HtmlDecode(grid.Rows[index].Cells[3].Text)));

                tip_doldur();
                comtip_kayit.SelectedIndex = comtip_kayit.Items.IndexOf(comtip_kayit.Items.FindByText(HttpUtility.HtmlDecode(grid.Rows[index].Cells[4].Text)));
                
                ad_doldur();
                comvarlikadi_kayit.SelectedIndex = comvarlikadi_kayit.Items.IndexOf(comvarlikadi_kayit.Items.FindByText(HttpUtility.HtmlDecode(grid.Rows[index].Cells[5].Text)));
                txtvarlikkodu_kayit.Text = HttpUtility.HtmlDecode(grid.Rows[index].Cells[6].Text.Trim());
                chckontrol.Text = HttpUtility.HtmlDecode(grid.Rows[index].Cells[7].Text.Trim());
                //chcaktif_pasif.Checked = Convert.ToBoolean(HttpUtility.HtmlDecode(grid.Rows[index].Cells[6].Text.Trim()));
                txtadres_kayit.Text = HttpUtility.HtmlDecode(grid.Rows[index].Cells[8].Text.Trim());

                conn.Open();
                SqlCommand sorgu = new SqlCommand();
                sorgu.Connection = conn;
                
                sorgu.CommandText = "SELECT V_ADRES FROM VARLIK WHERE ID='" + lblidcihaz.Text + "'  ";
                lblidadres.Text = Convert.ToString(sorgu.ExecuteScalar());

                sorgu.CommandText = "SELECT V_EN FROM VARLIK WHERE ID='" + lblidcihaz.Text + "'  ";
                txten_kayit.Text = Convert.ToString(sorgu.ExecuteScalar());
                sorgu.CommandText = "SELECT V_BOY FROM VARLIK WHERE ID='" + lblidcihaz.Text + "'  ";
                txtboy_kayit.Text = Convert.ToString(sorgu.ExecuteScalar());
                sorgu.CommandText = "SELECT V_YUKSEKLIK FROM VARLIK WHERE ID='" + lblidcihaz.Text + "'  ";
                txtyukseklik_kayit.Text = Convert.ToString(sorgu.ExecuteScalar());
                sorgu.CommandText = "SELECT V_ENLEM FROM VARLIK WHERE ID='" + lblidcihaz.Text + "'  ";
                txtenlem_kayit.Text = Convert.ToString(sorgu.ExecuteScalar());
                sorgu.CommandText = "SELECT V_BOYLAM FROM VARLIK WHERE ID='" + lblidcihaz.Text + "'  ";
                txtboylam_kayit.Text = Convert.ToString(sorgu.ExecuteScalar());
                conn.Close();

                if (comtip_kayit.SelectedItem.ToString() == "DOLAP")
                {
                    panel_dolap.Visible = true;
                    panel_bina.Visible = false;

                    dolap_yeri_doldur();
                    dolap_sinir_doldur();

                    conn.Open();
                    SqlCommand sorgu9 = new SqlCommand();
                    sorgu9.Connection = conn;

                    sorgu9.CommandText = "Select V_DOLAP_YERI FROM VARLIK WHERE ID = '" + id + "' ";
                    comdolapkonum_kayit.SelectedIndex = comdolapkonum_kayit.Items.IndexOf(comdolapkonum_kayit.Items.FindByText(sorgu9.ExecuteScalar().ToString()));

                    sorgu9.CommandText = "Select V_DOLAP_SINIR FROM VARLIK WHERE ID = '" + id + "' ";
                    comdolapkira_kayit.SelectedIndex = comdolapkira_kayit.Items.IndexOf(comdolapkira_kayit.Items.FindByText(sorgu9.ExecuteScalar().ToString()));


                    conn.Close();
                }
                else
                    panel_dolap.Visible = false;

                if (comlokasyon_kayit.SelectedItem.ToString() == "BİNA")
                {
                    panel_dolap.Visible = false;
                    panel_bina.Visible = true;

                    txtvarlikkodu_kayit.Enabled = false;
                    commahalle_kayit.Enabled = false;
                    comcdsk_kayit.Enabled = false;
                    combinano_kayit.Enabled = false;

                    txtvarlikkodu_kayit.Font.Size = 8;
                    
                    SqlCommand sorgu8 = new SqlCommand();
                    sorgu8.Connection = conn;

                    Bina_Ilce_Listele();
                    conn.Open();
                    sorgu8.CommandText = "SELECT AI.ILCE FROM ADRES_ILCE AI WHERE AI.ID IN (SELECT V_ADRES_ILCE_ID FROM VARLIK WHERE VARLIK.ID='" + id + "')  ";
                    comilce_kayit.SelectedIndex = comilce_kayit.Items.IndexOf(comilce_kayit.Items.FindByText(sorgu8.ExecuteScalar().ToString()));
                    conn.Close();

                    bina_mahalle_doldur();
                    //Bina_Mahalle_Listele();
                    conn.Open();
                    sorgu8.CommandText = "SELECT AM.MAHALLE FROM ADRES_MAHALLE AM WHERE AM.ID IN (SELECT V_ADRES_MAHALLE_ID FROM VARLIK WHERE VARLIK.ID='" + id + "')  ";
                    commahalle_kayit.SelectedIndex = commahalle_kayit.Items.IndexOf(commahalle_kayit.Items.FindByText(sorgu8.ExecuteScalar().ToString()));
                    conn.Close();

                    bina_sokak_doldur();
                    conn.Open();
                    sorgu8.CommandText = "SELECT ACS.CADDESOKAK FROM ADRES_CADDESOKAK ACS WHERE ACS.ID IN (SELECT V_ADRES_CADDESOKAK_ID FROM VARLIK WHERE VARLIK.ID='" + id + "')  ";
                    comcdsk_kayit.SelectedIndex = comcdsk_kayit.Items.IndexOf(comcdsk_kayit.Items.FindByText(sorgu8.ExecuteScalar().ToString()));
                    conn.Close();

                    Bina_BinaNo_Listele();
                    conn.Open();
                    sorgu8.CommandText = "SELECT ABN.BINANO FROM ADRES_BINANO ABN WHERE ABN.ID IN (SELECT V_ADRES_BINANO_ID FROM VARLIK WHERE VARLIK.ID='" + id + "')  ";
                    combinano_kayit.SelectedIndex = combinano_kayit.Items.IndexOf(combinano_kayit.Items.FindByText(sorgu8.ExecuteScalar().ToString()));
                    conn.Close();



                    /* sorgu8.CommandText = "SELECT ADRES.BINANO FROM ADRES WHERE ADRES.ID IN (SELECT V_ADRES_ID FROM VARLIK WHERE ID='" + id + "')  ";
                     txtbinano_kayit.Text = Convert.ToString(sorgu8.ExecuteScalar());

                     sorgu8.CommandText = "SELECT ADRES.BINA_ID FROM ADRES WHERE ADRES.ID IN (SELECT V_ADRES_ID FROM VARLIK WHERE ID='" + id + "')  ";
                     txtbinakodu_kayit.Text = Convert.ToString(sorgu8.ExecuteScalar());

                     sorgu8.CommandText = "SELECT ADRES.UAVTNO FROM ADRES WHERE ADRES.ID IN (SELECT V_ADRES_ID FROM VARLIK WHERE ID='" + id + "')  ";
                     txtuavtno_kayit.Text = Convert.ToString(sorgu8.ExecuteScalar());
                     */

                    conn.Close();
                }
                else
                    panel_bina.Visible = false;

                if (chckontrol.Text == "Aktif")
                    chcaktif_pasif.Checked = true;
                else
                    chcaktif_pasif.Checked = false;

                /**
                string il = tkod.sql_calistir_param("SELECT V_IL_ID FROM VARLIK  WHERE ID=@ID", new SqlParameter ("ID", lblidcihaz.Text) );
                comil_kayit.SelectedIndex = comil_kayit.Items.IndexOf(comil_kayit.Items.FindByValue(il));
                        **/

                //txtaciklama_kayit.Text = tkod.sql_calistir_param("SELECT V_ACIKLAMA FROM VARLIK WHERE ID=@ID", new SqlParameter ("ID", lblidcihaz.Text) );

                lblmodalyenibaslik.Text = "Varlık Güncelle";

                System.Text.StringBuilder sb = new System.Text.StringBuilder();
                sb.Append(@"<script type='text/javascript'>");
                sb.Append("$(\"#ModalVarlikEkle\").modal(\"show\");");
                sb.Append(@"</script>");
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(),
                           "ModalScript", sb.ToString(), false);

                btnvarlikkaydet.Enabled = true;
                btnvarlikkaydet.CssClass = "btn btn-success";
            }

            if (e.CommandName.Equals("islemler"))
            {
                int index = Convert.ToInt32(e.CommandArgument) % grid.PageSize;
                //int index = Convert.ToInt32(e.CommandArgument);
                //lblidcihaz_islemler.Text = grid.DataKeys[index].Value.ToString();
                string id = grid.DataKeys[index].Value.ToString();
                lblidcihaz_islemler.Text = id;

                System.Text.StringBuilder sb = new System.Text.StringBuilder();
                sb.Append(@"<script type='text/javascript'>");
                sb.Append("$(\"#ModalMalzemeVarlikEkle\").modal(\"show\");");
                sb.Append(@"</script>");
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(),
                           "ModalScript", sb.ToString(), false);

                lblmodalyenibaslik2.Text = "Malzeme - Varlık Eşleştirme";

                btnmalzemeekle.Enabled = true;
                btnmalzemeekle.CssClass = "btn btn-success";

                txtserinoara.Text = "";


                listele_varlik_malzeme();
            }

        }

        protected void grid_RowDataBound(object sender, GridViewRowEventArgs e)
        {

        }

        protected void grid_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            grid.PageIndex = e.NewPageIndex;
            listele_varlik();
        }

        // Varlık sayfası
        void listele_varlik()
        {
            SqlCommand cmd;
            string sql = " ", sql1 = " ";

            sql = "SELECT V.ID, IL.IL, BOLGE.BOLGE_ADI, VL.V_LOKASYON, VT.V_TIP, VA.V_ADI, V.V_KODU,V.V_ADRES, CASE WHEN ISNULL(V.V_DURUM, 0)=1 THEN 'Aktif' ELSE 'Pasif' END AS V_DURUM " +
                "  FROM VARLIK AS V " +
                " INNER JOIN IL ON IL.ID=V.V_IL_ID" +
                " INNER JOIN BOLGE ON BOLGE.ID=V.V_BOLGE_ID " +
                " INNER JOIN VARLIK_LOKASYON AS VL ON VL.ID=V.V_LOKASYON_ID" +
                " INNER JOIN VARLIK_TIP AS VT ON VT.ID=V.V_TIP_ID" +
                " INNER JOIN VARLIK_ADI AS VA ON VA.ID=V.V_ADI_ID" +
                "  ";

            if (combolge_ara.SelectedIndex > 0)
                sql1 += " AND (V.V_BOLGE_ID='" + combolge_ara.SelectedValue + "' )  ";

            if (comlokasyon_ara.SelectedIndex > 0)
                sql1 += " AND V.V_LOKASYON_ID='" + comlokasyon_ara.SelectedValue + "'  ";

            if (comtip_ara.SelectedIndex > 0)
                sql1 += " AND V.V_TIP_ID='" + comtip_ara.SelectedValue + "'  ";

            if (comvarlikadi_ara.SelectedIndex > 0)
                sql1 += " AND V.V_ADI_ID='" + comvarlikadi_ara.SelectedValue + "'  ";

            if (comdurum_ara.SelectedIndex > 0)
                sql1 += " AND V.V_DURUM='" + comdurum_ara.SelectedValue + "'  ";

            if (txtvarlikkodu_ara.Text.Length > 0)
                sql1 += " AND V.V_KODU LIKE '%" + txtvarlikkodu_ara.Text + "%'  ";

            sql = sql + tkod.yetki_tablosu() + "  WHERE V.ID > 0  " + sql1 + tkod.yetki_tablosu_2() + "   ORDER BY ID";

            cmd = new SqlCommand(sql, conn);
            conn.Open();
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            grid.DataSource = dt;
            grid.DataBind();
            conn.Close();
        }
        void bolge_ara()
        {
            combolge_ara.Items.Clear();
            combolge_ara.Items.Insert(0, new ListItem("Bölge Seçiniz", "0"));
            combolge_ara.AppendDataBoundItems = true;

            combolge_ara.DataSource = tkod.dt_bolge_Liste();
            combolge_ara.DataBind();
        }
        protected void comlokasyon_ara_SelectedIndexChanged(object sender, EventArgs e)
        {
            tip_ara();
        }
        protected void comtip_ara_SelectedIndexChanged(object sender, EventArgs e)
        {
            ad_ara();
        }
        void lokasyon_ara()
        {
            conn.Open();
            comlokasyon_ara.Items.Clear();
            comlokasyon_ara.Items.Insert(0, new ListItem("-", "0"));
            comlokasyon_ara.AppendDataBoundItems = true;
            string sql1 = "";

            SqlDataAdapter da = new SqlDataAdapter();
            SqlCommand sorgu = new SqlCommand("Select ID,V_LOKASYON FROM VARLIK_LOKASYON ORDER BY ID ");
            DataTable dt = new DataTable();
            da.SelectCommand = sorgu;
            da.SelectCommand.Connection = conn;
            da.Fill(dt);
            comlokasyon_ara.DataSource = dt;
            comlokasyon_ara.DataBind();
            conn.Close();
        }
        void tip_ara()
        {
            comtip_ara.Items.Clear();
            comtip_ara.Items.Insert(0, new ListItem("Seçiniz", "0"));
            comtip_ara.AppendDataBoundItems = true;
            string sql2 = "";

            if (comlokasyon_ara.SelectedIndex > 0)
                sql2 = " AND V_LOKASYON_ID='" + comlokasyon_ara.SelectedValue.ToString() + "' ";

            string sql = "SELECT ID,V_TIP FROM VARLIK_TIP WHERE ID>0 " + sql2 + " ORDER BY ID";
            comtip_ara.DataSource = tkod.GetData(sql);
            comtip_ara.DataBind();
        }
        void ad_ara()
        {
            comvarlikadi_ara.Items.Clear();
            comvarlikadi_ara.Items.Insert(0, new ListItem("Seçiniz", "0"));
            comvarlikadi_ara.AppendDataBoundItems = true;
            string sql2 = "";

            if (comtip_ara.SelectedIndex > 0)
                sql2 = " AND V_TIP_ID='" + comtip_ara.SelectedValue.ToString() + "' ";

            string sql = "SELECT ID,V_ADI FROM VARLIK_ADI WHERE ID>0 " + sql2 + " ORDER BY ID";
            comvarlikadi_ara.DataSource = tkod.GetData(sql);
            comvarlikadi_ara.DataBind();
        }

        protected void btnvarlikekle_Click(object sender, EventArgs e)
        {
            comil_kayit.Enabled = true;

            panel_dolap.Visible = false;
            panel_bina.Visible = false;

            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            sb.Append(@"<script type='text/javascript'>");
            sb.Append("$(\"#ModalVarlikEkle\").modal(\"show\");");
            sb.Append(@"</script>");
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(),
                       "ModalScript", sb.ToString(), false);

            lblmodalyenibaslik.Text = "Varlık Bilgisi Kaydet";

            if (combolge_kayit.SelectedIndex > 0) ;
            combolge_kayit.Items.Clear();
            if (comlokasyon_kayit.SelectedIndex > 0) ;
            comlokasyon_kayit.Items.Clear();
            if (comtip_kayit.SelectedIndex > 0) ;
            comtip_kayit.Items.Clear();
            if (comvarlikadi_kayit.SelectedIndex > 0) ;
            comvarlikadi_kayit.Items.Clear();

            combolge_kayit.Enabled = false;
            comlokasyon_kayit.Enabled = false;
            comtip_kayit.Enabled = false;
            comvarlikadi_kayit.Enabled = false;
            txtvarlikkodu_kayit.Text = "";
            txtenlem_kayit.Text = "";
            txtboylam_kayit.Text = "";
            //txtaciklama_kayit.Text = "";
            txtadres_kayit.Text = "";

            lblidcihaz.Text = "";

            il_doldur();

            btnvarlikkaydet.Enabled = true;
            btnvarlikkaydet.CssClass = "btn btn-success";
        }

        protected void btnsil_Click(object sender, EventArgs e)
        {
            if (lblislem.Text == "varlik-sil")
            {
                SqlCommand sorgu = new SqlCommand();
                sorgu.Connection = conn;
                conn.Open();

                try
                {
                    sorgu.CommandText = "SELECT COUNT(*) FROM VARLIK_MALZEME WHERE V_ID=@V9874_ID ";
                    sorgu.Parameters.AddWithValue("@V9874_ID", lblidsil.Text);
                    int sayi = Convert.ToInt16(sorgu.ExecuteScalar());

                    if (sayi == 0)
                    {

                        sorgu.CommandText = "DELETE FROM VARLIK WHERE ID=@V_SIL";
                        sorgu.Parameters.AddWithValue("@V_SIL", lblidsil.Text);
                        sorgu.ExecuteNonQuery();
                        conn.Close();

                        ScriptManager.RegisterStartupScript(this, this.GetType(), System.Guid.NewGuid().ToString(), "YeniMesaj('Varlık başarıyla silinmiştir.','Tamam');", true);
                        ScriptManager.RegisterStartupScript(this, this.GetType(), System.Guid.NewGuid().ToString(), "ShowMessage('Varlık başarıyla silinmiştir.','Tamam','sil');", true);
                    }
                    else
                    {
                        tkod.mesaj("Hata", "Bu varlık daha önce bir malzemeye bağlanmış. Bu nedenle silinemez sadece durumu değiştirilebilir.", this);
                        ScriptManager.RegisterStartupScript(this, this.GetType(), System.Guid.NewGuid().ToString(), "ShowMessage('Bu varlık daha önce bir malzemeye bağlanmış. Bu nedenle silinemez sadece durumu değiştirilebilir.','Hata','sil');", true);
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

                listele_varlik();
            }

        }

        protected void btnara_Click(object sender, EventArgs e)
        {
            listele_varlik();

        }

        // Varlik Kayıt Modal

        void il_doldur()
        {
            comil_kayit.Items.Clear();
            comil_kayit.Items.Insert(0, new ListItem("İl Seçiniz", "0"));
            comil_kayit.AppendDataBoundItems = true;

            comil_kayit.DataSource = tkod.dt_il_Liste();
            comil_kayit.DataBind();
            /*
            conn.Open();
            comil_kayit.Items.Clear();
            comil_kayit.Items.Insert(0, new ListItem("-", "0"));
            comil_kayit.AppendDataBoundItems = true;
            string sql1 = "";

            SqlDataAdapter da = new SqlDataAdapter();
            SqlCommand sorgu = new SqlCommand("Select ID,IL FROM IL ORDER BY ID ");
            DataTable dt = new DataTable();
            da.SelectCommand = sorgu;
            da.SelectCommand.Connection = conn;
            da.Fill(dt);
            comil_kayit.DataSource = dt;
            comil_kayit.DataBind();
            conn.Close();*/
        }
        void bolge_doldur()
        {
            combolge_kayit.Items.Clear();
            combolge_kayit.Items.Insert(0, new ListItem("Bölge Seçiniz", "0"));
            combolge_kayit.AppendDataBoundItems = true;
            string sql2 = "";

            if (comil_kayit.SelectedIndex > 0)
                sql2 = " AND IL='" + comil_kayit.SelectedValue.ToString() + "' ";

            string sql = "SELECT ID,BOLGE_ADI FROM BOLGE WHERE ID>0 " + sql2 + " ORDER BY ID";
            combolge_kayit.DataSource = tkod.GetData(sql);
            combolge_kayit.DataBind();
        }
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

        void dolap_yeri_doldur()
        {
            comdolapkonum_kayit.Items.Clear();
            comdolapkonum_kayit.Items.Insert(0, new ListItem("Seçiniz", "0"));
            comdolapkonum_kayit.AppendDataBoundItems = true;

            SqlDataAdapter da = new SqlDataAdapter();
            SqlCommand cmd = new SqlCommand("Select DEGER AS ID,SECENEK FROM COMDOLDUR WHERE COM_ADI = 'comdolapkonum' ORDER BY SIRA  ");
            DataTable dt = new DataTable();
            da.SelectCommand = cmd;
            da.SelectCommand.Connection = conn;
            da.Fill(dt);
            comdolapkonum_kayit.DataSource = dt;
            //comdolapkonum_kayit.DataTextField = "V_DOLAP_YERI";
            //comdolapkonum_kayit.DataValueField = "ID";
            comdolapkonum_kayit.DataBind();

        }
        
        void dolap_sinir_doldur()
        {
            conn.Open();
            comdolapkira_kayit.Items.Clear();
            comdolapkira_kayit.Items.Insert(0, new ListItem("-", "0"));
            comdolapkira_kayit.AppendDataBoundItems = true;
            string sql1 = "";

            SqlDataAdapter da = new SqlDataAdapter();
            SqlCommand sorgu = new SqlCommand("Select DEGER AS ID,SECENEK FROM COMDOLDUR WHERE COM_ADI = 'comdolapkirabolgesi' ORDER BY SIRA  ");
            //SqlCommand sorgu = new SqlCommand("Select ID,V_DOLAP_SINIR FROM VARLIK where ID='" + lblidcihaz.Text + "' ");
            DataTable dt = new DataTable();
            da.SelectCommand = sorgu;
            da.SelectCommand.Connection = conn;
            da.Fill(dt);
            comdolapkira_kayit.DataSource = dt;
            //comdolapkonum_kayit.DataTextField = "V_DOLAP_SINIR";
            //comdolapkonum_kayit.DataValueField = "ID";
            comdolapkira_kayit.DataBind();
            conn.Close();
        }
        void bina_ilce_doldur()
        {
            comilce_kayit.Items.Clear();
            comilce_kayit.Items.Insert(0, new ListItem("Seçiniz", "0"));
            comilce_kayit.AppendDataBoundItems = true;
            string sql2 = "";

            if (combolge_kayit.SelectedIndex > 0)
                sql2 = " AND BOLGE_ID='" + combolge_kayit.SelectedValue.ToString() + "' ";

            string sql = "Select ID,ILCE FROM ADRES_ILCE WHERE ID>0 " + sql2 + " ORDER BY ID";
            comilce_kayit.DataSource = tkod.GetData(sql);
            comilce_kayit.DataBind();

        }

        void bina_mahalle_doldur()
        {
            commahalle_kayit.Items.Clear();
            commahalle_kayit.Items.Insert(0, new ListItem("Mahalle Seçiniz", "0"));
            commahalle_kayit.AppendDataBoundItems = true;
            string sql2 = "";

            if (comilce_kayit.SelectedIndex > 0)
                sql2 = " AND ILCE_ID='" + comilce_kayit.SelectedValue.ToString() + "' ";

            string sql = "Select ID,MAHALLE FROM ADRES_MAHALLE WHERE ID>0 " + sql2 + " ORDER BY ID";
            commahalle_kayit.DataSource = tkod.GetData(sql);
            commahalle_kayit.DataBind();

            /*
            conn.Open();
            commahalle_kayit.Items.Clear();
            commahalle_kayit.Items.Insert(0, new ListItem("-", "0"));
            commahalle_kayit.AppendDataBoundItems = true;
            string sql2 = "";

            //if (comilce_kayit.SelectedIndex > 0)
            sql2 = "  ILCE='" + comilce_kayit.SelectedItem.ToString() + "' ";

            SqlDataAdapter da = new SqlDataAdapter();
            SqlCommand sorgu = new SqlCommand("Select MAHALLE FROM ADRES WHERE " + sql2 + " GROUP BY MAHALLE");
            DataTable dt = new DataTable();
            da.SelectCommand = sorgu;
            da.SelectCommand.Connection = conn;
            da.Fill(dt);
            commahalle_kayit.DataSource = dt;
            commahalle_kayit.DataBind();
            conn.Close();
            */
        }
        void bina_sokak_doldur()
        {
            comcdsk_kayit.Items.Clear();
            comcdsk_kayit.Items.Insert(0, new ListItem("Mahalle Seçiniz", "0"));
            comcdsk_kayit.AppendDataBoundItems = true;
            string sql2 = "";

            if (commahalle_kayit.SelectedIndex > 0)
                sql2 = " AND MAHALLE_ID='" + commahalle_kayit.SelectedValue.ToString() + "' ";

            string sql = "Select ID,CADDESOKAK FROM ADRES_CADDESOKAK WHERE ID>0 " + sql2 + " ORDER BY ID";
            comcdsk_kayit.DataSource = tkod.GetData(sql);
            comcdsk_kayit.DataBind();
            /*
            conn.Open();
            comcdsk_kayit.Items.Clear();
            comcdsk_kayit.Items.Insert(0, new ListItem("-", "0"));
            comcdsk_kayit.AppendDataBoundItems = true;
            string sql2 = "";

            //if (comilce_kayit.SelectedIndex > 0)
            sql2 = "  MAHALLE='" + commahalle_kayit.SelectedItem.ToString() + "' ";

            SqlDataAdapter da = new SqlDataAdapter();
            SqlCommand sorgu = new SqlCommand("Select CADDESOKAK FROM ADRES CADDESOKAK WHERE " + sql2 + " GROUP BY CADDESOKAK");
            DataTable dt = new DataTable();
            da.SelectCommand = sorgu;
            da.SelectCommand.Connection = conn;
            da.Fill(dt);
            comcdsk_kayit.DataSource = dt;
            comcdsk_kayit.DataBind();
            conn.Close();
            */
        }
        void binano_doldur()
        {
            combinano_kayit.Items.Clear();
            combinano_kayit.Items.Insert(0, new ListItem("Mahalle Seçiniz", "0"));
            combinano_kayit.AppendDataBoundItems = true;
            string sql2 = "";

            if (comcdsk_kayit.SelectedIndex > 0)
                sql2 = " AND CADDESOKAK_ID='" + comcdsk_kayit.SelectedValue.ToString() + "' ";

            string sql = "Select ID,BINANO FROM ADRES_BINANO WHERE ID>0 " + sql2 + " ORDER BY ID";
            combinano_kayit.DataSource = tkod.GetData(sql);
            combinano_kayit.DataBind();
        }

        protected void comil_kayit_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comil_kayit.SelectedIndex == 0)
            {
                combolge_kayit.Enabled = false;
                comilce_kayit.Enabled = false;
                commahalle_kayit.Enabled = false;
                comcdsk_kayit.Enabled = false;
                combinano_kayit.Enabled = false;
            }
            else
                combolge_kayit.Enabled = true;

            bolge_doldur();
            lokasyon_doldur();
        }
        protected void combolge_kayit_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (combolge_kayit.SelectedIndex == 0)
            {
                comilce_kayit.Enabled = false;
                commahalle_kayit.Enabled = false;
                comcdsk_kayit.Enabled = false;
                combinano_kayit.Enabled = false;
            }
            else
                comilce_kayit.Enabled = true;

            comlokasyon_kayit.Enabled = true;

            panel_dolap.Visible = false;

        }
        protected void comlokasyon_kayit_SelectedIndexChanged(object sender, EventArgs e)
        {
            comtip_kayit.Enabled = true;
            tip_doldur();

            panel_dolap.Visible = false;


            if (comlokasyon_kayit.SelectedItem.ToString() == "BİNA")
            {
                txtvarlikkodu_kayit.Enabled = false;
                panel_bina.Visible = true;
                Bina_Ilce_Listele();
                commahalle_kayit.Enabled = false;
                comcdsk_kayit.Enabled = false;
                combinano_kayit.Enabled = false;

            }

            else
            {
                txtvarlikkodu_kayit.Enabled = true;
                panel_bina.Visible = false;
            }



        }
        protected void comtip_kayit_SelectedIndexChanged(object sender, EventArgs e)
        {
            comvarlikadi_kayit.Enabled = true;
            ad_doldur();

            if (comtip_kayit.SelectedItem.ToString() == "DOLAP")
            {
                panel_dolap.Visible = true;
                Dolap_Konum_Listele();
                Dolap_Sinir_Listele();
                panel_bina.Visible = false;
            }
            else
                panel_dolap.Visible = false;

        }

        protected void btnvarlikkaydet_Click(object sender, EventArgs e)
        {
            SqlCommand sorgu = new SqlCommand();
            sorgu.Connection = conn;
            SqlCommand sorgu1 = new SqlCommand();
            sorgu1.Connection = conn;
            conn.Open();

            sorgu.CommandText = "SELECT COUNT(V_ID) FROM VARLIK_MALZEME WHERE V_ID=@V_VKODU876 AND V_M_BAGLANTI_DURUMU='True' ";
            sorgu.Parameters.AddWithValue("@V_VKODU876", lblidcihaz.Text);
            int sayi1 = Convert.ToInt16(sorgu.ExecuteScalar());

            sorgu.CommandText = "SELECT COUNT(*) FROM VARLIK WHERE V_KODU=@V_VKODU ";
            sorgu.Parameters.AddWithValue("@V_VKODU", txtvarlikkodu_kayit.Text);
            int sayi = Convert.ToInt16(sorgu.ExecuteScalar());

            string userid = Session["KULLANICI_ID"].ToString();

            try
            {
                if (lblidcihaz.Text == "")
                {
                    if (sayi == 0)
                    {
                        if (comlokasyon_kayit.SelectedItem.ToString() == "SAHA")
                        {
                            if(comvarlikadi_kayit.SelectedItem.ToString()=="SANAL")
                            {
                                sorgu.CommandText = "INSERT INTO    VARLIK (V_IL_ID, V_BOLGE_ID, V_LOKASYON_ID, V_TIP_ID, V_ADI_ID, V_KODU, V_DURUM, KAYIT_EDEN, KAYIT_TARIHI) " +
                                    " VALUES(@IL_ID, @B_ID, @L_ID, @T_ID, @A_ID, @VK, @VDRM, @UI, getdate() ); SELECT @@IDENTITY AS 'Identity' ";
                                sorgu.Parameters.AddWithValue("@IL_ID", comil_kayit.SelectedValue);
                                sorgu.Parameters.AddWithValue("@B_ID", combolge_kayit.SelectedValue);
                                sorgu.Parameters.AddWithValue("@L_ID", comlokasyon_kayit.SelectedValue);
                                sorgu.Parameters.AddWithValue("@T_ID", comtip_kayit.SelectedValue);
                                sorgu.Parameters.AddWithValue("@A_ID", comvarlikadi_kayit.SelectedValue);
                                sorgu.Parameters.AddWithValue("@VK", txtvarlikkodu_kayit.Text);
                                //sorgu.Parameters.AddWithValue("@VACKLM", txtaciklama_kayit.Text);
                                sorgu.Parameters.AddWithValue("@VDRM", chcaktif_pasif.Checked);
                                sorgu.Parameters.AddWithValue("@UI", userid);
                                string id = sorgu.ExecuteScalar().ToString();
                                lblidcihaz.Text = id;
                            }
                            else
                            {
                                sorgu.CommandText = "INSERT INTO    VARLIK (V_IL_ID, V_BOLGE_ID, V_LOKASYON_ID, V_TIP_ID, V_ADI_ID, V_KODU, V_ADRES, V_DURUM, V_EN, V_BOY, V_YUKSEKLIK, V_ENLEM, V_BOYLAM, V_DOLAP_YERI, V_DOLAP_SINIR, KAYIT_EDEN, KAYIT_TARIHI) " +
                                    " VALUES(@IL_ID, @B_ID, @L_ID, @T_ID, @A_ID, @VK, @VADRES, @VDRM, @VEN, @VBOY, @VYUKSEKLIK, @VENLEM, @VBOYLAM, @VDOLAPKONUM, @VDOLAPSINIR, @UI, getdate() ); SELECT @@IDENTITY AS 'Identity' ";
                                sorgu.Parameters.AddWithValue("@IL_ID", comil_kayit.SelectedValue);
                                sorgu.Parameters.AddWithValue("@B_ID", combolge_kayit.SelectedValue);
                                sorgu.Parameters.AddWithValue("@L_ID", comlokasyon_kayit.SelectedValue);
                                sorgu.Parameters.AddWithValue("@T_ID", comtip_kayit.SelectedValue);
                                sorgu.Parameters.AddWithValue("@A_ID", comvarlikadi_kayit.SelectedValue);
                                sorgu.Parameters.AddWithValue("@VK", txtvarlikkodu_kayit.Text);
                                sorgu.Parameters.AddWithValue("@VADRES", txtadres_kayit.Text);
                                //sorgu.Parameters.AddWithValue("@VACKLM", txtaciklama_kayit.Text);
                                sorgu.Parameters.AddWithValue("@VDRM", chcaktif_pasif.Checked);
                                sorgu.Parameters.AddWithValue("@VEN", txten_kayit.Text);
                                sorgu.Parameters.AddWithValue("@VBOY", txtboy_kayit.Text);
                                sorgu.Parameters.AddWithValue("@VYUKSEKLIK", txtyukseklik_kayit.Text);
                                sorgu.Parameters.AddWithValue("@VENLEM", txtenlem_kayit.Text);
                                sorgu.Parameters.AddWithValue("@VBOYLAM", txtboylam_kayit.Text);
                                sorgu.Parameters.AddWithValue("@VDOLAPKONUM", comdolapkonum_kayit.SelectedItem.ToString());
                                sorgu.Parameters.AddWithValue("@VDOLAPSINIR", comdolapkira_kayit.SelectedItem.ToString());
                                sorgu.Parameters.AddWithValue("@UI", userid);
                                string id = sorgu.ExecuteScalar().ToString();
                                lblidcihaz.Text = id;
                            }

                        }
                        if (comlokasyon_kayit.SelectedItem.ToString() == "BİNA")
                        {
                            /*
                            sorgu.CommandText = "INSERT INTO    ADRES (IL, BOLGE, ILCE, MAHALLE, CADDESOKAK, BINANO, BINA_ID, UAVTNO, MANUEL_KAYIT, KAYIT_EDEN, KAYIT_TARIHI) " +
                                " VALUES(@IL_A, @BOLGE_A, @ILCE_A, @MAHALLE_A, @CADDESOKAK_A, @BINANO_A, @BINA_ID_A, @UAVTNO_A, @MANUEL_KAYIT_A, @UI, getdate() ); SELECT @@IDENTITY AS 'Identity' ";
                            sorgu.Parameters.AddWithValue("@IL_A", comil_kayit.SelectedItem.ToString());
                            sorgu.Parameters.AddWithValue("@BOLGE_A", combolge_kayit.SelectedItem.ToString());
                            sorgu.Parameters.AddWithValue("@ILCE_A", comilce_kayit.SelectedItem.ToString());
                            sorgu.Parameters.AddWithValue("@MAHALLE_A", commahalle_kayit.SelectedItem.ToString());
                            sorgu.Parameters.AddWithValue("@CADDESOKAK_A", comcdsk_kayit.SelectedItem.ToString());
                            sorgu.Parameters.AddWithValue("@BINANO_A", txtbinano_kayit.Text);
                            sorgu.Parameters.AddWithValue("@BINA_ID_A", txtbinakodu_kayit.Text);
                            sorgu.Parameters.AddWithValue("@UAVTNO_A", txtuavtno_kayit.Text);
                            sorgu.Parameters.AddWithValue("@MANUEL_KAYIT_A", 1);
                            sorgu.Parameters.AddWithValue("@UI", userid);
                            string id_a = sorgu.ExecuteScalar().ToString();
                            lblidadres.Text = id_a;
                            */

                            sorgu.CommandText = "INSERT INTO    VARLIK (V_IL_ID, V_BOLGE_ID, V_LOKASYON_ID, V_TIP_ID, V_ADI_ID, V_KODU, V_DURUM, V_ADRES_ILCE_ID,V_ADRES_MAHALLE_ID,V_ADRES_CADDESOKAK_ID,V_ADRES_BINANO_ID, KAYIT_EDEN, KAYIT_TARIHI) " +
                                " VALUES(@IL_ID, @B_ID, @L_ID, @T_ID, @A_ID, @VK, @VDRM, @VADRESILCE_ID_K, @VADRESMAHALLE_ID_K, @VADRESCADDESOKAK_ID_K, @VADRESBINANO_ID_K, @UI66, getdate() ); SELECT @@IDENTITY AS 'Identity' ";
                            sorgu.Parameters.AddWithValue("@IL_ID", comil_kayit.SelectedValue);
                            sorgu.Parameters.AddWithValue("@B_ID", combolge_kayit.SelectedValue);
                            sorgu.Parameters.AddWithValue("@L_ID", comlokasyon_kayit.SelectedValue);
                            sorgu.Parameters.AddWithValue("@T_ID", comtip_kayit.SelectedValue);
                            sorgu.Parameters.AddWithValue("@A_ID", comvarlikadi_kayit.SelectedValue);
                            sorgu.Parameters.AddWithValue("@VK", txtvarlikkodu_kayit.Text);
                            //sorgu.Parameters.AddWithValue("@VADRES", txtadres_kayit.Text);
                            //sorgu.Parameters.AddWithValue("@VACKLM", txtaciklama_kayit.Text);
                            sorgu.Parameters.AddWithValue("@VDRM", chcaktif_pasif.Checked);
                            sorgu.Parameters.AddWithValue("@VADRESILCE_ID_K", comilce_kayit.SelectedValue);
                            sorgu.Parameters.AddWithValue("@VADRESMAHALLE_ID_K", commahalle_kayit.SelectedValue);
                            sorgu.Parameters.AddWithValue("@VADRESCADDESOKAK_ID_K", comcdsk_kayit.SelectedValue);
                            sorgu.Parameters.AddWithValue("@VADRESBINANO_ID_K", combinano_kayit.SelectedValue);
                            sorgu.Parameters.AddWithValue("@UI66", userid);
                            string id = sorgu.ExecuteScalar().ToString();
                            lblidcihaz.Text = id;


                        }
                        if (comlokasyon_kayit.SelectedItem.ToString() == "YAYIN MERKEZİ")
                        {

                            sorgu.CommandText = "INSERT INTO    VARLIK (V_IL_ID, V_BOLGE_ID, V_LOKASYON_ID, V_TIP_ID, V_ADI_ID, V_KODU, V_DURUM, KAYIT_EDEN, KAYIT_TARIHI) " +
                                " VALUES(@IL_ID_3, @B_ID_3, @L_ID_3, @T_ID_3, @A_ID_3, @VK_3, @VDRM_3, @UI_3, getdate() ); SELECT @@IDENTITY AS 'Identity' ";
                            sorgu.Parameters.AddWithValue("@IL_ID_3", comil_kayit.SelectedValue);
                            sorgu.Parameters.AddWithValue("@B_ID_3", combolge_kayit.SelectedValue);
                            sorgu.Parameters.AddWithValue("@L_ID_3", comlokasyon_kayit.SelectedValue);
                            sorgu.Parameters.AddWithValue("@T_ID_3", comtip_kayit.SelectedValue);
                            sorgu.Parameters.AddWithValue("@A_ID_3", comvarlikadi_kayit.SelectedValue);
                            sorgu.Parameters.AddWithValue("@VK_3", txtvarlikkodu_kayit.Text);
                            sorgu.Parameters.AddWithValue("@VDRM_3", chcaktif_pasif.Checked);
                            sorgu.Parameters.AddWithValue("@UI_3", userid);
                            string id = sorgu.ExecuteScalar().ToString();
                            lblidcihaz.Text = id;


                        }

                        // ------   ***   ------ \\
                        // ------   LOG   ------ \\
                        // ------   ***   ------ \\
                        sorgu1.CommandText = "INSERT INTO    VARLIK_LOG (V_ID, IL_ID, BOLGE_ID, LOKASYON_ID, TIP_ID, AD_ID, V_KODU, V_DURUM, ADRES, ENLEM, BOYLAM, LOG_ACIKLAMA, KAYIT_EDEN, KAYIT_TARIHI) " +
                        " VALUES(@VID, @ILID, @BID, @LID, @TID, @ADID, @VKODU, @VDURUM, @ADRS, @ENLEM, @BOYLAM, @L_ACKLM, @UI1, getdate() ) ";
                        sorgu1.Parameters.AddWithValue("@VID", lblidcihaz.Text);
                        sorgu1.Parameters.AddWithValue("@ILID", comil_kayit.SelectedValue);
                        sorgu1.Parameters.AddWithValue("@BID", combolge_kayit.SelectedValue);
                        sorgu1.Parameters.AddWithValue("@LID", comlokasyon_kayit.SelectedValue);
                        sorgu1.Parameters.AddWithValue("@TID", comtip_kayit.SelectedValue);
                        sorgu1.Parameters.AddWithValue("@ADID", comvarlikadi_kayit.SelectedValue);
                        sorgu1.Parameters.AddWithValue("@VKODU", txtvarlikkodu_kayit.Text);
                        sorgu1.Parameters.AddWithValue("@VDURUM", chcaktif_pasif.Checked);
                        sorgu1.Parameters.AddWithValue("@ADRS", txtadres_kayit.Text);
                        //sorgu1.Parameters.AddWithValue("@ACIKLAMA", txtaciklama_kayit.Text);
                        sorgu1.Parameters.AddWithValue("@ENLEM", txtenlem_kayit.Text);
                        sorgu1.Parameters.AddWithValue("@BOYLAM", txtboylam_kayit.Text);
                        sorgu1.Parameters.AddWithValue("@L_ACKLM", "Yeni kayıt yapıldı.");
                        sorgu1.Parameters.AddWithValue("@UI1", userid);
                        sorgu1.ExecuteNonQuery();
                        // ------   ***   ------ \\
                        // ------   LOG   ------ \\
                        // ------   ***   ------ \\


                        ScriptManager.RegisterStartupScript(this, this.GetType(), System.Guid.NewGuid().ToString(), "YeniMesaj('Varlık başarıyla kaydedilmiştir.','Tamam');", true);
                        ScriptManager.RegisterStartupScript(this, this.GetType(), System.Guid.NewGuid().ToString(), "ShowMessage('Varlık başarıyla kaydedilmiştir.','Tamam','yeni');", true);

                        btnvarlikkaydet.Enabled = false;
                        btnvarlikkaydet.CssClass = "btn btn-success disabled";
                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(this, this.GetType(), System.Guid.NewGuid().ToString(), "YeniMesaj('Bu Varlık Kodu daha önce kaydedilmişti. Lütfen kontrol ediniz.','Hata');", true);
                        ScriptManager.RegisterStartupScript(this, this.GetType(), System.Guid.NewGuid().ToString(), "ShowMessage('Bu Varlık Kodu daha önce kaydedilmişti. Lütfen kontrol ediniz.','Hata','yeni');", true);
                    }
                }
                else
                {
                    if (sayi1 != 0)
                    {
                        ScriptManager.RegisterStartupScript(this, this.GetType(), System.Guid.NewGuid().ToString(), "YeniMesaj('Bu Varlık a bağlı malzeme bulunmaktadır. Öncelikle Varlık Malzeme bağlantısı kesilmeli.','Hata');", true);
                        ScriptManager.RegisterStartupScript(this, this.GetType(), System.Guid.NewGuid().ToString(), "ShowMessage('Bu Varlık a bağlı malzeme bulunmaktadır. Öncelikle Varlık Malzeme bağlantısı kesilmeli.','Hata','yeni');", true);
                    }
                    else
                    {
                        if (comlokasyon_kayit.SelectedItem.ToString() == "SAHA")
                        {

                            // ------   UPDATE   ------ \\
                            sorgu.CommandText = "UPDATE VARLIK SET V_IL_ID=@IL_ID, V_BOLGE_ID=@B_ID, V_LOKASYON_ID=@L_ID, " +
                                "V_TIP_ID=@T_ID, V_ADI_ID=@A_ID, V_KODU=@VK, " +
                                "V_ADRES=@VADRES, V_DURUM=@VDURUM, V_EN=@VEN_U, V_BOY=@VBOY_U, V_YUKSEKLIK=@VYUKSEKLIK_U, " +
                                "V_ENLEM=@VENLEM_U, V_BOYLAM=@VBOYLAM_U, V_DOLAP_YERI=@VDOLAPKONUM_U, V_DOLAP_SINIR=@VDOLAPSINIR_U  WHERE ID=@V_ID_U ";
                            sorgu.Parameters.AddWithValue("@IL_ID", comil_kayit.SelectedValue);
                            sorgu.Parameters.AddWithValue("@B_ID", combolge_kayit.SelectedValue);
                            sorgu.Parameters.AddWithValue("@L_ID", comlokasyon_kayit.SelectedValue);
                            sorgu.Parameters.AddWithValue("@T_ID", comtip_kayit.SelectedValue);
                            sorgu.Parameters.AddWithValue("@A_ID", comvarlikadi_kayit.SelectedValue);
                            sorgu.Parameters.AddWithValue("@VK", txtvarlikkodu_kayit.Text);
                            sorgu.Parameters.AddWithValue("@VADRES", txtadres_kayit.Text);
                            //sorgu.Parameters.AddWithValue("@VACKLM", txtaciklama_kayit.Text);
                            sorgu.Parameters.AddWithValue("@VDURUM", chcaktif_pasif.Checked);
                            sorgu.Parameters.AddWithValue("@VEN_U", txten_kayit.Text);
                            sorgu.Parameters.AddWithValue("@VBOY_U", txtboy_kayit.Text);
                            sorgu.Parameters.AddWithValue("@VYUKSEKLIK_U", txtyukseklik_kayit.Text);
                            sorgu.Parameters.AddWithValue("@VENLEM_U", txtenlem_kayit.Text);
                            sorgu.Parameters.AddWithValue("@VBOYLAM_U", txtboylam_kayit.Text);
                            sorgu.Parameters.AddWithValue("@VDOLAPKONUM_U", comdolapkonum_kayit.SelectedItem.ToString());
                            sorgu.Parameters.AddWithValue("@VDOLAPSINIR_U", comdolapkira_kayit.SelectedItem.ToString());
                            sorgu.Parameters.AddWithValue("@UI", userid);
                            sorgu.Parameters.AddWithValue("@V_ID_U", lblidcihaz.Text);
                            sorgu.ExecuteNonQuery();
                            // ------   UPDATE   ------ \\

                        }
                        if (comlokasyon_kayit.SelectedItem.ToString() == "BİNA")
                        {
                            //txtvarlikkodu_kayit.Text = commahalle_kayit.SelectedItem.ToString() + "-" + comcdsk_kayit.SelectedItem.ToString() + "-" + combinano_kayit.SelectedItem.ToString() + "-" + comilce_kayit.SelectedItem.ToString();

                            // ------   UPDATE   ------ \\
                            sorgu.CommandText = "UPDATE VARLIK SET V_IL_ID=@IL_ID_G, V_BOLGE_ID=@B_ID_G, V_LOKASYON_ID=@L_ID_G, " +
                                " V_TIP_ID=@T_ID_G,  V_ADI_ID=@A_ID_G, V_KODU=@VK_G, " +
                                " V_DURUM=@VDURUM_G, V_ADRES_ILCE_ID=@VADRESILCE_ID_G, V_ADRES_MAHALLE_ID=@VADRESMAHALLE_ID_G, " +
                                " V_ADRES_CADDESOKAK_ID=@VADRESCADDESOKAK_ID_G, V_ADRES_BINANO_ID=@VADRESBINANO_ID_G     WHERE ID=@V_ID_U_G ";
                            sorgu.Parameters.AddWithValue("@IL_ID_G", comil_kayit.SelectedValue);
                            sorgu.Parameters.AddWithValue("@B_ID_G", combolge_kayit.SelectedValue);
                            sorgu.Parameters.AddWithValue("@L_ID_G", comlokasyon_kayit.SelectedValue);
                            sorgu.Parameters.AddWithValue("@T_ID_G", comtip_kayit.SelectedValue);
                            sorgu.Parameters.AddWithValue("@A_ID_G", comvarlikadi_kayit.SelectedValue);
                            sorgu.Parameters.AddWithValue("@VK_G", txtvarlikkodu_kayit.Text);
                            sorgu.Parameters.AddWithValue("@VDURUM_G", chcaktif_pasif.Checked);
                            sorgu.Parameters.AddWithValue("@VADRESILCE_ID_G", comilce_kayit.SelectedValue);
                            sorgu.Parameters.AddWithValue("@VADRESMAHALLE_ID_G", commahalle_kayit.SelectedValue);
                            sorgu.Parameters.AddWithValue("@VADRESCADDESOKAK_ID_G", comcdsk_kayit.SelectedValue);
                            sorgu.Parameters.AddWithValue("@VADRESBINANO_ID_G", combinano_kayit.SelectedValue);
                            //sorgu.Parameters.AddWithValue("@UI", userid);
                            sorgu.Parameters.AddWithValue("@V_ID_U_G", lblidcihaz.Text);
                            sorgu.ExecuteNonQuery();
                            // ------   UPDATE   ------ \\



                            /*
                            sorgu.CommandText = "UPDATE ADRES SET IL=@IL_A_U, BOLGE=@BOLGE_A_U, ILCE=@ILCE_A_U, MAHALLE=@MAHALLE_A_U, " +
                                "CADDESOKAK=@CADDESOKAK_A_U, BINANO=@BINANO_A_U,  MANUEL_KAYIT=@MANUEL_KAYIT_A_U WHERE ID=@V_ID_A_U ";
                            sorgu.Parameters.AddWithValue("@IL_A_U", comil_kayit.SelectedItem.ToString());
                            sorgu.Parameters.AddWithValue("@BOLGE_A_U", combolge_kayit.SelectedItem.ToString());
                            sorgu.Parameters.AddWithValue("@ILCE_A_U", comilce_kayit.SelectedItem.ToString());
                            sorgu.Parameters.AddWithValue("@MAHALLE_A_U", commahalle_kayit.SelectedItem.ToString());
                            sorgu.Parameters.AddWithValue("@CADDESOKAK_A_U", comcdsk_kayit.SelectedItem.ToString());
                            sorgu.Parameters.AddWithValue("@BINANO_A_U", combinano_kayit.SelectedItem.ToString());
                            sorgu.Parameters.AddWithValue("@MANUEL_KAYIT_A_U", 1);
                            sorgu.Parameters.AddWithValue("@UI_U", userid);
                            sorgu.Parameters.AddWithValue("@V_ID_A_U", lblidadres.Text);

                            sorgu.ExecuteNonQuery();
                            */
                        }
                        if (comlokasyon_kayit.SelectedItem.ToString() == "YAYIN MERKEZİ")
                        {

                            // ------   UPDATE   ------ \\
                            sorgu.CommandText = "UPDATE VARLIK SET V_IL_ID=@IL_ID, V_BOLGE_ID=@B_ID, V_LOKASYON_ID=@L_ID, " +
                                "V_TIP_ID=@T_ID, V_ADI_ID=@A_ID, V_KODU=@VK, " +
                                " V_DURUM=@VDURUM  WHERE ID=@V_ID_U ";
                            sorgu.Parameters.AddWithValue("@IL_ID", comil_kayit.SelectedValue);
                            sorgu.Parameters.AddWithValue("@B_ID", combolge_kayit.SelectedValue);
                            sorgu.Parameters.AddWithValue("@L_ID", comlokasyon_kayit.SelectedValue);
                            sorgu.Parameters.AddWithValue("@T_ID", comtip_kayit.SelectedValue);
                            sorgu.Parameters.AddWithValue("@A_ID", comvarlikadi_kayit.SelectedValue);
                            sorgu.Parameters.AddWithValue("@VK", txtvarlikkodu_kayit.Text);
                            sorgu.Parameters.AddWithValue("@VDURUM", chcaktif_pasif.Checked);
                            sorgu.Parameters.AddWithValue("@UI", userid);
                            sorgu.Parameters.AddWithValue("@V_ID_U", lblidcihaz.Text);
                            sorgu.ExecuteNonQuery();
                            // ------   UPDATE   ------ \\

                        }
                        // ------   ***   ------ \\
                        // ------   LOG   ------ \\
                        // ------   ***   ------ \\
                        sorgu1.CommandText = "INSERT INTO    VARLIK_LOG (V_ID, IL_ID, BOLGE_ID, LOKASYON_ID, TIP_ID, AD_ID, V_KODU, " +
                            "V_DURUM, ADRES, ENLEM, BOYLAM, LOG_ACIKLAMA, KAYIT_EDEN, KAYIT_TARIHI) " +
                            " VALUES(@VID, @ILID, @BID, @LID, @TID, @ADID, @VKODU, @VDURUM, @ADRS, @ENLEM, @BOYLAM, @L_ACKLM, @UI1, getdate() ) ";
                        sorgu1.Parameters.AddWithValue("@VID", lblidcihaz.Text);
                        sorgu1.Parameters.AddWithValue("@ILID", comil_kayit.SelectedValue);
                        sorgu1.Parameters.AddWithValue("@BID", combolge_kayit.SelectedValue);
                        sorgu1.Parameters.AddWithValue("@LID", comlokasyon_kayit.SelectedValue);
                        sorgu1.Parameters.AddWithValue("@TID", comtip_kayit.SelectedValue);
                        sorgu1.Parameters.AddWithValue("@ADID", comvarlikadi_kayit.SelectedValue);
                        sorgu1.Parameters.AddWithValue("@VKODU", txtvarlikkodu_kayit.Text);
                        sorgu1.Parameters.AddWithValue("@VDURUM", chcaktif_pasif.Checked);
                        sorgu1.Parameters.AddWithValue("@ADRS", txtadres_kayit.Text);
                        //sorgu1.Parameters.AddWithValue("@ACIKLAMA", txtaciklama_kayit.Text);
                        sorgu1.Parameters.AddWithValue("@ENLEM", txtenlem_kayit.Text);
                        sorgu1.Parameters.AddWithValue("@BOYLAM", txtboylam_kayit.Text);
                        sorgu1.Parameters.AddWithValue("@L_ACKLM", "Kayıt güncellendi.");
                        sorgu1.Parameters.AddWithValue("@UI1", userid);
                        sorgu1.ExecuteNonQuery();
                        // ------   ***   ------ \\
                        // ------   LOG   ------ \\
                        // ------   ***   ------ \\

                        ScriptManager.RegisterStartupScript(this, this.GetType(), System.Guid.NewGuid().ToString(), "YeniMesaj('Varlık başarıyla güncellenmiştir.','Tamam');", true);
                        ScriptManager.RegisterStartupScript(this, this.GetType(), System.Guid.NewGuid().ToString(), "ShowMessage('Varlık başarıyla güncellenmiştir.','Tamam','yeni');", true);
                        btnvarlikkaydet.Enabled = false;
                        btnvarlikkaydet.CssClass = "btn btn-success disabled";
                    }
                }
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), System.Guid.NewGuid().ToString(), "YeniMesaj('Beklenmeyen bir hata oluştu.','Hata');", true);
                ScriptManager.RegisterStartupScript(this, this.GetType(), System.Guid.NewGuid().ToString(), "ShowMessage('Hata oluştu :'" + ex.Message.Replace("'", "") + ",'Hata','yeni');", true);
            }

            if (ConnectionState.Open == conn.State)
                conn.Close();
            listele_varlik();
        }

        protected void chcaktif_pasif_CheckedChanged(object sender, EventArgs e)
        {
            if (chcaktif_pasif.Checked == true)
                chckontrol.Text = "Aktif";
            else
                chckontrol.Text = "Pasif";

        }
        void Dolap_Konum_Listele()
        {
            comdolapkonum_kayit.Items.Clear();
            comdolapkonum_kayit.Items.Insert(0, new ListItem("Seçiniz", "0"));
            comdolapkonum_kayit.AppendDataBoundItems = true;

            SqlDataAdapter da = new SqlDataAdapter();
            SqlCommand cmd = new SqlCommand("Select DEGER AS ID,SECENEK FROM COMDOLDUR WHERE COM_ADI = 'comdolapkonum' ORDER BY SIRA  ");
            DataTable dt = new DataTable();
            da.SelectCommand = cmd;
            da.SelectCommand.Connection = conn;
            da.Fill(dt);
            comdolapkonum_kayit.DataSource = dt;
            //comdolapkonum_kayit.DataTextField = "SECENEK";
            //comdolapkonum_kayit.DataValueField = "ID";
            comdolapkonum_kayit.DataBind();
        }
        void Dolap_Sinir_Listele()
        {
            comdolapkira_kayit.Items.Clear();
            comdolapkira_kayit.Items.Insert(0, new ListItem("Seçiniz", "0"));
            comdolapkira_kayit.AppendDataBoundItems = true;

            SqlDataAdapter da = new SqlDataAdapter();
            SqlCommand cmd = new SqlCommand("Select DEGER AS ID,SECENEK FROM COMDOLDUR WHERE COM_ADI = 'comdolapkirabolgesi' ORDER BY SIRA  ");
            DataTable dt = new DataTable();
            da.SelectCommand = cmd;
            da.SelectCommand.Connection = conn;
            da.Fill(dt);
            comdolapkira_kayit.DataSource = dt;
            //comdolapkira_kayit.DataTextField = "SECENEK";
            //comdolapkira_kayit.DataValueField = "ID";
            comdolapkira_kayit.DataBind();
        }

        void Bina_Ilce_Listele()
        {
            comilce_kayit.Items.Clear();
            comilce_kayit.Items.Insert(0, new ListItem("İlçe Seçiniz", "0"));
            comilce_kayit.AppendDataBoundItems = true;
            string sql2 = "";

            if (comil_kayit.SelectedIndex > 0)
                sql2 = " AND BOLGE_ID='" + combolge_kayit.SelectedValue.ToString() + "' ";

            string sql = "Select ID,ILCE FROM ADRES_ILCE WHERE ID>0 " + sql2 + " ORDER BY ID";
            comilce_kayit.DataSource = tkod.GetData(sql);
            comilce_kayit.DataBind();

            /**comilce_kayit.Items.Clear();
            comilce_kayit.Items.Insert(0, new ListItem("Seçiniz", "0"));
            comilce_kayit.AppendDataBoundItems = true;

            SqlDataAdapter da = new SqlDataAdapter();
            //GROUP BY ID, ILCE
            SqlCommand cmd = new SqlCommand("Select distinct  ILCE, ID FROM ADRES ");
            DataTable dt = new DataTable();
            da.SelectCommand = cmd;
            da.SelectCommand.Connection = conn;
            da.Fill(dt);
            comilce_kayit.DataSource = dt;
            //comilce_kayit.DataTextField = "ILCE";
            //comilce_kayit.DataValueField = "ID";
            comilce_kayit.DataBind();**/
        }

        void Bina_Mahalle_Listele()
        {

            commahalle_kayit.Items.Clear();
            commahalle_kayit.Items.Insert(0, new ListItem("Mahalle Seçiniz", "0"));
            commahalle_kayit.AppendDataBoundItems = true;
            string sql2 = "";

            if (comilce_kayit.SelectedIndex > 0)
                sql2 = " AND ILCE_ID='" + comilce_kayit.SelectedValue.ToString() + "' ";

            string sql = "SELECT ID,MAHALLE FROM ADRES_MAHALLE WHERE ID>0 " + sql2 + "  ORDER BY ID";
            commahalle_kayit.DataSource = tkod.GetData(sql);
            commahalle_kayit.DataBind();



        }

        void Bina_CaddeSokak_Listele()
        {
            comcdsk_kayit.Items.Clear();
            comcdsk_kayit.Items.Insert(0, new ListItem("Cadde/Sokak Seçiniz", "0"));
            comcdsk_kayit.AppendDataBoundItems = true;
            string sql2 = "";

            if (commahalle_kayit.SelectedIndex > 0)
                sql2 = " AND MAHALLE_ID='" + commahalle_kayit.SelectedValue.ToString() + "' ";

            string sql = "SELECT ID,CADDESOKAK FROM ADRES_CADDESOKAK WHERE ID>0 " + sql2 + "  ORDER BY ID ";
            comcdsk_kayit.DataSource = tkod.GetData(sql);
            comcdsk_kayit.DataBind();
        }

        void Bina_BinaNo_Listele()
        {
            combinano_kayit.Items.Clear();
            combinano_kayit.Items.Insert(0, new ListItem("Bina No Seçiniz", "0"));
            combinano_kayit.AppendDataBoundItems = true;
            string sql2 = "";

            if (comcdsk_kayit.SelectedIndex > 0)
                sql2 = " AND CADDESOKAK_ID='" + comcdsk_kayit.SelectedValue.ToString() + "' ";

            string sql = "Select ID,BINANO FROM ADRES_BINANO WHERE ID>0 " + sql2 + " ORDER BY ID";
            combinano_kayit.DataSource = tkod.GetData(sql);
            combinano_kayit.DataBind();
        }

        protected void comilce_kayit_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comilce_kayit.SelectedIndex == 0)
            {
                commahalle_kayit.Enabled = false;
                comcdsk_kayit.Enabled = false;
                combinano_kayit.Enabled = false;
            }
            else
                commahalle_kayit.Enabled = true;

            Bina_Mahalle_Listele();
            //txtvarlikkodu_kayit.Text = commahalle_kayit.SelectedItem.ToString() + "-" + comcdsk_kayit.SelectedItem.ToString() + "-" + txtbinano_kayit.Text + "-" + comilce_kayit.SelectedItem.ToString();
        }

        protected void commahalle_kayit_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (commahalle_kayit.SelectedIndex == 0)
            {
                comcdsk_kayit.Enabled = false;
                combinano_kayit.Enabled = false;
            }
            else
                comcdsk_kayit.Enabled = true;

            Bina_CaddeSokak_Listele();
            //txtvarlikkodu_kayit.Text = commahalle_kayit.SelectedItem.ToString() + "-" + comcdsk_kayit.SelectedItem.ToString() + "-" + txtbinano_kayit.Text + "-" + comilce_kayit.SelectedItem.ToString();

        }

        protected void comcdsk_kayit_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comcdsk_kayit.SelectedIndex == 0)
            {
                combinano_kayit.Enabled = false;
            }
            else
                combinano_kayit.Enabled = true;
            Bina_BinaNo_Listele();
            txtvarlikkodu_kayit.Text = commahalle_kayit.SelectedItem.ToString() + "-" + comcdsk_kayit.SelectedItem.ToString() + "-" + combinano_kayit.SelectedItem.ToString() + "-" + comilce_kayit.SelectedItem.ToString();
            txtvarlikkodu_kayit.Font.Size = 8;
        }

        protected void combinano_kayit_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtvarlikkodu_kayit.Text = commahalle_kayit.SelectedItem.ToString() + "-" + comcdsk_kayit.SelectedItem.ToString() + "-" + combinano_kayit.SelectedItem.ToString() + "-" + comilce_kayit.SelectedItem.ToString();
        }

        protected void grid_varlik_malzeme_baglama_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName.Equals("sil"))
            {
                int index = Convert.ToInt32(e.CommandArgument) % grid.PageSize;
                lblidsil.Text = grid_varlik_malzeme_baglama.DataKeys[index].Value.ToString();
                lblislem.Text = "varlik-malzeme-sil";
                btnmalzemesil.Enabled = true;
                btnmalzemesil.CssClass = "btn btn-danger ";
                                           

                varlikid = HttpUtility.HtmlDecode(grid_varlik_malzeme_baglama.Rows[index].Cells[1].Text.Trim());
                malzemeid = HttpUtility.HtmlDecode(grid_varlik_malzeme_baglama.Rows[index].Cells[3].Text.Trim());

                System.Text.StringBuilder sb = new System.Text.StringBuilder();
                sb.Append(@"<script type='text/javascript'>");
                sb.Append("$(\"#ModalMalzemeSil\").modal(\"show\");");
                sb.Append(@"</script>");
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(),
                           "ModalScript", sb.ToString(), false);

                txtgerekce_varlikbaglantisil.Text = "";

                listele_varlik_malzeme();
                Varlik_Sil_Durum_Degistir_Doldur();


            }
        }

        protected void btnmalzemeekle_Click(object sender, EventArgs e)
        {

            try
            {
                SqlCommand sorgu = new SqlCommand();
                sorgu.Connection = conn;
                SqlCommand sorgu1 = new SqlCommand();
                sorgu1.Connection = conn;
                conn.Open();

                //lblsevkdurumkontrol_durum.Text = tkod.sql_calistir_param("SELECT ISNULL(SEVKDURUMU,0) FROM MALZEMELER WHERE SERI_NO=@SERINO", new SqlParameter("SERINO", txtserinoara.Text));
                //string varlikdurumkontrol = tkod.sql_calistir_param("SELECT VARLIK_ID FROM MALZEMELER WHERE ID=@ID", new SqlParameter("ID", lblidcihaz_islemler.Text));
                //lblvarlikdurumkontrol_durum.Text = tkod.sql_calistir_param("SELECT VARLIK_ID FROM MALZEMELER WHERE ID=@ID", new SqlParameter("ID", lblidcihaz_islemler.Text));

                sorgu.CommandText = "SELECT ISNULL(SEVKDURUMU,0) FROM MALZEMELER WHERE SERI_NO=@SERINO ";
                sorgu.Parameters.AddWithValue("@SERINO", txtserinoara.Text);
                lblsevkdurumkontrol_durum.Text = Convert.ToString(sorgu.ExecuteScalar());

                sorgu.CommandText = "SELECT ISNULL(VARLIK_ID,0) FROM MALZEMELER WHERE SERI_NO=@SERINO1 ";
                sorgu.Parameters.AddWithValue("@SERINO1", txtserinoara.Text);
                int varlikdurumkontrol1 = Convert.ToInt16(sorgu.ExecuteScalar());

                sorgu.CommandText = "SELECT ISNULL(BOLGE_ID,0) FROM MALZEMELER WHERE SERI_NO=@SERINO2 ";
                sorgu.Parameters.AddWithValue("@SERINO2", txtserinoara.Text);
                int malzemebolgeidkontrol = Convert.ToInt16(sorgu.ExecuteScalar());

                sorgu.CommandText = "SELECT ISNULL(V_BOLGE_ID,0) FROM VARLIK WHERE ID=@ID99 ";
                sorgu.Parameters.AddWithValue("@ID99", lblidcihaz_islemler.Text);
                int varlikbolgeidkontrol = Convert.ToInt16(sorgu.ExecuteScalar());

                //{ "Conversion failed when converting the nvarchar value 'Label' to data type int."}

                //lblebolge_durum.Text = tkod.sql_calistir("SELECT BOLGE_ID FROM MALZEMELER WHERE ID=@ID", new SqlParameter ("ID", lblidcihaz_islemler.Text) );
                //lbledepo_durum.Text = tkod.sql_calistir("SELECT DEPO_ID FROM MALZEMELER WHERE ID=@ID", new SqlParameter ("ID", lblidcihaz_islemler.Text) );
                lbltamirdepokontrol_durum.Text = tkod.sql_calistir_param("SELECT ISNULL(TAMIRDEPO_KONTROL, 'FALSE') FROM MALZEMELER WHERE SERI_NO=@SERINO3", new SqlParameter("SERINO3", txtserinoara.Text));
                //lblmevcutdurum_durumid.Text = tkod.sql_calistir("SELECT ID FROM MALZEME_DURUM WHERE DURUM=  '" + lblmevcutdurum_durum.Text + "' ");



                sorgu.CommandText = "SELECT ID FROM MALZEMELER WHERE SERI_NO=@SERINO4 ";
                sorgu.Parameters.AddWithValue("@SERINO4", txtserinoara.Text);
                int malzemeid = Convert.ToInt16(sorgu.ExecuteScalar());




                sorgu.CommandText = "SELECT ISNULL(V_DURUM, 0) FROM VARLIK WHERE ID =@VID ";
                sorgu.Parameters.AddWithValue("@VID", lblidcihaz_islemler.Text);
                int varlikdurum = Convert.ToInt16(sorgu.ExecuteScalar());

                if (varlikdurum == 0)
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), System.Guid.NewGuid().ToString(), "YeniMesaj('Varlık durum pasif, bu nedenle bağlantı yapılamaz.','Hata');", true);
                    ScriptManager.RegisterStartupScript(this, this.GetType(), System.Guid.NewGuid().ToString(), "ShowMessage('Varlık durum pasif, bu nedenle bağlantı yapılamaz.','Hata','malzemeekle');", true);
                }    
                else
                {
                    if (lblsevkdurumkontrol_durum.Text != "False")
                    {
                        ScriptManager.RegisterStartupScript(this, this.GetType(), System.Guid.NewGuid().ToString(), "YeniMesaj('Bu malzeme sevk durumunda. Sevk Durumu tamamlanmadan varlık bağlantısı yapılamaz.','Hata');", true);
                        ScriptManager.RegisterStartupScript(this, this.GetType(), System.Guid.NewGuid().ToString(), "ShowMessage('Bu malzeme sevk durumunda. Sevk Durumu tamamlanmadan varlık bağlantısı yapılamaz.','Hata','malzemeekle');", true);
                    }
                    else
                    {
                        if (malzemebolgeidkontrol != varlikbolgeidkontrol)
                        {
                            tkod.mesaj("Hata", "Bu malzeme farklı bir Bölgeye ait", this.Page);

                            ScriptManager.RegisterStartupScript(this, this.GetType(), System.Guid.NewGuid().ToString(), "YeniMesaj('Bu malzeme farklı bir Bölgeye ait.','Hata');", true);
                            ScriptManager.RegisterStartupScript(this, this.GetType(), System.Guid.NewGuid().ToString(), "ShowMessage('Bu malzeme farklı bir Bölgeye ait.','Hata','malzemeekle');", true);
                        }
                        else
                        {
                            if (lbltamirdepokontrol_durum.Text != "False")
                            {
                                ScriptManager.RegisterStartupScript(this, this.GetType(), System.Guid.NewGuid().ToString(), "YeniMesaj('Bu malzeme Tamir Deposunda. Bu nedenle varlık bağlantısı yapılamaz.','Hata');", true);
                                ScriptManager.RegisterStartupScript(this, this.GetType(), System.Guid.NewGuid().ToString(), "ShowMessage('Bu malzeme Tamir Deposunda. Bu nedenle varlık bağlantısı yapılamaz.','Hata','malzemeekle');", true);
                            }
                            else
                            { 
                                if (varlikdurumkontrol1 > 0)
                                {
                                    ScriptManager.RegisterStartupScript(this, this.GetType(), System.Guid.NewGuid().ToString(), "YeniMesaj('Bu malzeme zaten bir Varlık a bağlıdır.','Hata');", true);
                                    ScriptManager.RegisterStartupScript(this, this.GetType(), System.Guid.NewGuid().ToString(), "ShowMessage('Bu malzeme bir Varlık a bağlıdır.','Hata','malzemeekle');", true);
                                }
                                else
                                {
                                    sorgu.CommandText = "UPDATE MALZEMELER SET VARLIK_ID=@VID10, DURUM=@DRM  WHERE SERI_NO=@M_ID ";
                                    sorgu.Parameters.AddWithValue("@VID10", lblidcihaz_islemler.Text);
                                    sorgu.Parameters.AddWithValue("@M_ID", txtserinoara.Text);
                                    sorgu.Parameters.AddWithValue("@DRM", 5); //Kullanımda
                                    sorgu.ExecuteNonQuery();

                                    sorgu.CommandText = "UPDATE VARLIK SET V_M_BAGLANTI_DURUMU=@VMBD  WHERE ID=@VID_U ";
                                    sorgu.Parameters.AddWithValue("@VMBD", true);
                                    sorgu.Parameters.AddWithValue("@VID_U", lblidcihaz_islemler.Text);
                                    sorgu.ExecuteNonQuery();

                                    string userid = Session["KULLANICI_ID"].ToString();
                                    sorgu.CommandText = "INSERT INTO    VARLIK_MALZEME (V_ID, M_ID, V_M_BAGLANTI_DURUMU, V_M_BAGLANTI_TARIHI, KAYIT_EDEN , KAYIT_TARIHI) " +
                                        " VALUES(@VID1, @MID1, @BD1, @BT1, @UI, getdate() )";
                                    sorgu.Parameters.AddWithValue("@VID1", lblidcihaz_islemler.Text);
                                    sorgu.Parameters.AddWithValue("@MID1", malzemeid);
                                    sorgu.Parameters.AddWithValue("@BD1", true);
                                    sorgu.Parameters.AddWithValue("@BT1", Convert.ToDateTime(DateTime.Now.ToString()));
                                    sorgu.Parameters.AddWithValue("@UI", userid);
                                    sorgu.ExecuteNonQuery();


                                    // ------   ***   ------ \\
                                    // ------   LOG   ------ \\
                                    // ------   ***   ------ \\
                                    sorgu1.CommandText = "INSERT INTO    VARLIK_LOG (V_ID, M_ID, LOG_ACIKLAMA, KAYIT_EDEN, KAYIT_TARIHI) " +
                                        " VALUES(@VID2, @MAID, @L_ACKLM, @UI1, getdate() ) ";
                                    sorgu1.Parameters.AddWithValue("@VID2", lblidcihaz_islemler.Text);
                                    sorgu1.Parameters.AddWithValue("@MAID", malzemeid);
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
                                    sorgu.Parameters.AddWithValue("@MID", malzemeid);
                                    sorgu.Parameters.AddWithValue("@GRKC", "Malzeme bir varlık'a bağlandı.");
                                    sorgu.Parameters.AddWithValue("@ACIKLAMA", "Durum Değişikliği yapıldı, Malzeme Varlık bağlantısı yapıldı.");
                                    sorgu.Parameters.AddWithValue("@YDRM", 5);
                                    sorgu.Parameters.AddWithValue("@UI2", userid);
                                    sorgu.ExecuteNonQuery();
                                    // ------   ***   ------ \\
                                    // ------   LOG   ------ \\
                                    // ------   ***   ------ \\


                                    ScriptManager.RegisterStartupScript(this, this.GetType(), System.Guid.NewGuid().ToString(), "YeniMesaj('Varlık - Malzeme Eşleşmesi başarıyla kaydedildi.','Tamam');", true);
                                    ScriptManager.RegisterStartupScript(this, this.GetType(), System.Guid.NewGuid().ToString(), "ShowMessage('Varlık - Malzeme Eşleşmesi başarıyla kaydedildi.','Tamam','malzemeekle');", true);
                                    btnmalzemeekle.Enabled = false;
                                    btnmalzemeekle.CssClass = "btn btn-success disabled";
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), System.Guid.NewGuid().ToString(), "YeniMesaj('Beklenmeyen bir hata oluştu.','Hata');", true);
                ScriptManager.RegisterStartupScript(this, this.GetType(), System.Guid.NewGuid().ToString(), "ShowMessage('Hata oluştu :'" + ex.Message.Replace("'", "") + ",'Hata','malzemeekle');", true);
            }

            if (ConnectionState.Open == conn.State)
                conn.Close();
            listele_varlik_malzeme();
            listele_varlik();
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

                " WHERE VM.V_ID = '" + lblidcihaz_islemler.Text + "' AND  VM.V_M_BAGLANTI_DURUMU = 'True'  ";

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

        protected void btnmalzemesil_Click(object sender, EventArgs e)
        {


            try
            {
                if (lblislem.Text == "varlik-malzeme-sil")
                {
                    SqlCommand sorgu = new SqlCommand();
                    sorgu.Connection = conn;
                    conn.Open();

                    sorgu.CommandText = "SELECT M_ID FROM VARLIK_MALZEME WHERE ID =@MID78 ";
                    sorgu.Parameters.AddWithValue("@MID78", lblidsil.Text);
                    int malzemeid1 = Convert.ToInt16(sorgu.ExecuteScalar());

                    sorgu.CommandText = "SELECT V_ID FROM VARLIK_MALZEME WHERE ID =@MID87 ";
                    sorgu.Parameters.AddWithValue("@MID87", lblidsil.Text);
                    int varlikid1 = Convert.ToInt16(sorgu.ExecuteScalar());


                    sorgu.CommandText = "UPDATE VARLIK_MALZEME SET V_M_BAGLANTI_DURUMU=@VMBD2, V_M_AYRILMA_TARIHI=@VMAT2  WHERE ID=@VMSID ";
                    sorgu.Parameters.AddWithValue("@VMBD2", false);
                    sorgu.Parameters.AddWithValue("@VMAT2", DateTime.Now);
                    sorgu.Parameters.AddWithValue("@VMSID", lblidsil.Text);
                    sorgu.ExecuteNonQuery();
                    
                    sorgu.CommandText = "UPDATE MALZEMELER SET VARLIK_ID=@VID, DURUM=@DRM  WHERE ID=@MID45 ";
                    sorgu.Parameters.AddWithValue("@VID", DBNull.Value);
                    sorgu.Parameters.AddWithValue("@DRM", comyenidurumsec_varlikbaglantisil.SelectedValue);
                    sorgu.Parameters.AddWithValue("@MID45", malzemeid1);
                    sorgu.ExecuteNonQuery();

                    sorgu.CommandText = "UPDATE VARLIK SET V_M_BAGLANTI_DURUMU=@VMBD  WHERE ID=@VID9 ";
                    sorgu.Parameters.AddWithValue("@VMBD", false);
                    sorgu.Parameters.AddWithValue("@VID9", varlikid1);
                    sorgu.ExecuteNonQuery();

                    // ------   ***   ------ \\
                    // ------   LOG   ------ \\
                    // ------   ***   ------ \\
                    string userid = Session["KULLANICI_ID"].ToString();
                    sorgu.CommandText = "INSERT INTO    VARLIK_LOG (V_ID, M_ID, LOG_ACIKLAMA, KAYIT_EDEN, KAYIT_TARIHI) " +
                        " VALUES(@VID3, @MAID3, @L_ACKLM3, @UI3, getdate() ) ";
                    sorgu.Parameters.AddWithValue("@VID3", varlikid1);
                    sorgu.Parameters.AddWithValue("@MAID3", malzemeid1);
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
                    sorgu.Parameters.AddWithValue("@MID9", malzemeid1);
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
                    ScriptManager.RegisterStartupScript(this, this.GetType(), System.Guid.NewGuid().ToString(), "ShowMessage('Varlık ve Malzeme bağlantısı başarıyla silinmiştir.','Tamam','silmalzeme');", true);

                    btnmalzemesil.Enabled = false;
                    btnmalzemesil.CssClass = "btn btn-danger disabled";

                    listele_varlik_malzeme();

                }
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), System.Guid.NewGuid().ToString(), "YeniMesaj('Beklenmeyen bir hata oluştu. Hata:" + ex.Message.ToString().Replace("'", "") + "','Hata');", true);
                ScriptManager.RegisterStartupScript(this, this.GetType(), System.Guid.NewGuid().ToString(), "ShowMessage('Hata oluştu :'" + ex.Message.Replace("'", "") + ",'Hata','silmalzeme');", true);
            }
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