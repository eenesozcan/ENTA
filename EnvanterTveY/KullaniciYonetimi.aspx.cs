using System;
using System.Collections.Generic;

using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using System.Data;
using System.Data.SqlClient;
using System.Configuration;

//ok
namespace EnvanterTveY
{
    public partial class KullaniciYonetimi : System.Web.UI.Page
    {
        SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["KabloHE"].ConnectionString);
        string sayfa = "KullaniciYonetimi";
        DataTable dt;
        KodT.kodlar tkod = new KodT.kodlar();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["KULLANICI_ID"] == null)
            {
                Response.Redirect("Giris.aspx?url=" + sayfa);
            }
            else
            {
                if (Session["KULLANICI_ROL"].ToString() != "ADMIN")
                {
                    Response.Redirect("Default");
                }
            }

            if (!IsPostBack)
            {
                rol_listele();
                il_listele();
                firma_listele();
                Kullanici_Listele();
            }
        }

        void Kullanici_Listele()
        {
            string sql = "";
            string sql2 = "", sql3 = "", sql4 = "", sqli = "";
            SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["KabloHE"].ConnectionString);
            conn.Open();
            SqlCommand sorgu = new SqlCommand();
            sorgu.Connection = conn;

            if (comrol.SelectedIndex > 0)
                sql3 = " and (K.ROL_ID='" + comrol.SelectedValue.ToString() + "'   )";

            if (comilara.SelectedIndex > 0)
                sql3 += " and K.IL='" + comilara.SelectedValue.ToString() + "' ";

            if (txtkullaniciadiarama.Text.Length > 0)
                sql3 += " AND K.ISIM LIKE '%" + txtkullaniciadiarama.Text + "%' ";

            sql = "SELECT K.ID,K.TC,K.ISIM,K.KULLANICI_ADI,K.EMAIL,K.TELEFON,K.DURUM,R.ROL,IL.IL,BOLGE.BOLGE_ADI, FIRMA.FIRMA_ADI FROM KULLANICI as K " +
                        " INNER JOIN ROL AS R ON R.ID=K.ROL_ID " +
                        " LEFT JOIN IL ON IL.ID=K.IL " +
                        " LEFT JOIN BOLGE ON BOLGE.ID=K.BOLGE" +
                        " LEFT JOIN FIRMA ON FIRMA.ID=K.FIRMA " +
                        " where k.ID>0"
                ;
            sql += sql3 + " ORDER BY ID ASC ";
            SqlDataAdapter da = new SqlDataAdapter(sql, conn);
            DataSet ds = new DataSet();
            da.Fill(ds);
            gridkullanici.DataSource = ds.Tables[0];
            gridkullanici.DataBind();
            conn.Close();
            lblsayi.Text = gridkullanici.Rows.Count.ToString() + " adet kayıt bulunmuştur.";
        }

        void mesaj_yaz_yeni(string hata, string mesaj)
        {
            js_calistir("YeniMesaj('" + mesaj + "','" + hata + "');");
        }

        public void js_calistir(string str)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), System.Guid.NewGuid().ToString(), str, true);
        }

        protected void btnyenikullaniciekle_Click(object sender, EventArgs e)
        {
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            sb.Append(@"<script type='text/javascript'>");
            sb.Append("$(\"#ModalKullaniciEkle\").modal(\"show\");");
            sb.Append(@"</script>");
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(),
                       "ModalScript", sb.ToString(), false);
            txtTC.Text = "";
            txtkullaniciadi.Text = "";
            txteposta.Text = "";
            txttelefon.Text = "";
            txtisim.Text = "";
            comilekle.SelectedIndex = 0;
            combolgeekle.SelectedIndex = 0;
            comrolekle.SelectedIndex = 0;
            comfirmaekle.SelectedIndex = 0;
            combolgeekle.Enabled = false;

            btnkaydet.Enabled = true;
            btnkaydet.CssClass = "btn btn-success";
            btnyetkiver.Enabled = false;
            btnyetkiver.CssClass = "btn btn-success disabled";
        }

        protected void btnkullanicilistele_Click(object sender, EventArgs e)
        {
            Kullanici_Listele();
        }

        protected void btnrolekle_Click(object sender, EventArgs e)
        {
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            sb.Append(@"<script type='text/javascript'>");
            sb.Append("$(\"#ModalRol\").modal(\"show\");");
            sb.Append(@"</script>");
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "ModalScript", sb.ToString(), false);
        }

        void il_listele()
        {
            conn.Open();

            comilekle.Items.Clear();
            comilekle.Items.Insert(0, new ListItem("-", "0"));
            comilekle.AppendDataBoundItems = true;

            comilara.Items.Clear();
            comilara.Items.Insert(0, new ListItem("-", "0"));
            comilara.AppendDataBoundItems = true;
            sqlil.SelectCommand = "SELECT ID,IL FROM IL ORDER BY IL";
            sqlil.DataBind();
            conn.Close();
        }

        void rol_listele()
        {
            conn.Open();

            comrol.Items.Clear();
            comrol2.Items.Clear();
            comrol.Items.Insert(0, new ListItem("-", "0"));
            comrol.AppendDataBoundItems = true;
            sqlRol.SelectCommand = "SELECT ID,ROL FROM ROL ORDER BY ROL";
            sqlil.DataBind();
            conn.Close();
        }

        void bolge_listele()
        {
            conn.Open();

            combolgeekle.Items.Clear();
            combolgeekle.Items.Insert(0, new ListItem("-", "0"));
            combolgeekle.AppendDataBoundItems = true;

            if (comilekle.SelectedIndex > 0)
                sqlbolge.SelectCommand = "SELECT ID,BOLGE_ADI FROM BOLGE WHERE IL='" + comilekle.SelectedValue + "' ORDER BY BOLGE_ADI";
            else
                sqlbolge.SelectCommand = "SELECT ID,BOLGE_ADI FROM BOLGE ORDER BY BOLGE_ADI";
            sqlbolge.DataBind();
            conn.Close();
        }

        void bolge_listele2()
        {
            combolge2.Items.Clear();
            combolge2.Items.Insert(0, new ListItem("-", "0"));
            combolge2.AppendDataBoundItems = true;

            if (comil2.SelectedIndex > 0)
                sqlbolge.SelectCommand = "SELECT ID,BOLGE_ADI FROM BOLGE WHERE IL='" + comil2.SelectedValue + "' ORDER BY BOLGE_ADI";
            else
                sqlbolge.SelectCommand = "SELECT ID,BOLGE_ADI FROM BOLGE ORDER BY BOLGE_ADI";
            sqlbolge.DataBind();
        }

        void firma_listele()
        {
            conn.Open();

            comfirmaekle.Items.Clear();
            comfirmaekle.Items.Insert(0, new ListItem("-", "0"));
            comfirmaekle.AppendDataBoundItems = true;
            sqlfirma.SelectCommand = "SELECT FIRMA_ADI,ID FROM FIRMA ORDER BY FIRMA_ADI";
            sqlfirma.DataBind();
            conn.Close();
        }

        public void yetki_menusu_ac(string id)
        {
            lstbolge1.Items.Clear();
            lstbolge2.Items.Clear();

            lstdepo1.Items.Clear();
            lstdepo2.Items.Clear();

            SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["KabloHE"].ConnectionString);
            SqlCommand sorgu = new SqlCommand();
            sorgu.Connection = conn;
            bool durum = false;
            conn.Open();
            sorgu.CommandText = "SELECT DURUM FROM KULLANICI WHERE ID='" + id + "' ";
            durum = Convert.ToBoolean(sorgu.ExecuteScalar());
            chcdurum.Checked = durum;

            sorgu.CommandText = "SELECT ISNULL(TUM_BOLGE_GORSUN,'FALSE') FROM KULLANICI WHERE ID='" + id + "' ";
            durum = Convert.ToBoolean(sorgu.ExecuteScalar());

            depo_doldur();

            SqlDataSource3.SelectCommand = "SELECT  ID,BOLGE_ADI FROM BOLGE ORDER BY BOLGE_ADI";
            SqlDataSource3.DataBind();
            SqlDataSource4.SelectCommand = "SELECT DEPO.DEPO, DEPO.ID FROM DEPO INNER JOIN YETKILER AS Y2 ON Y2.YETKI = DEPO.BOLGE_ID WHERE Y2.YETKI_TIPI = 'BOLGE' AND Y2.KULLANICI_ID = '" + id + "'   ";
            SqlDataSource4.DataBind();

            conn.Close();
        }

        void yetki_listele()
        {
            if (Session["KULLANICI_ID"] == null)
            {
                Response.Redirect("Giris.aspx?url=" + sayfa);
            }
            else
            {
                string id = lblidguncelle.Text;

                conn.Open();
                string sql = "SELECT BOLGE.BOLGE_ADI,BOLGE.ID FROM YETKILER " +
                                " INNER JOIN  BOLGE ON YETKILER.YETKI = BOLGE.ID " +
                                " WHERE YETKILER.KULLANICI_ID='" + id + "'  " +
                                "    and YETKILER.YETKI_TIPI='BOLGE' ";
                SqlDataAdapter da = new SqlDataAdapter(sql, conn);
                DataTable dt = new DataTable();
                da.Fill(dt);
                conn.Close();

                string b_id;
                int l1 = 0;
                for (int j = 0; j < dt.Rows.Count; j++)
                {
                    b_id = dt.Rows[j][1].ToString();

                    l1 = lstbolge1.Items.IndexOf(lstbolge1.Items.FindByValue(b_id));
                    lstbolge2.Items.Add(lstbolge1.Items[l1]);
                    lstbolge1.Items.Remove(lstbolge1.Items[l1]);
                    lstbolge2.SelectedIndex = -1;
                }
            }
        }
        
        void depo_yetki_listele()
        {
            if (Session["KULLANICI_ID"] == null)
            {
                Response.Redirect("Giris.aspx?url=" + sayfa);
            }
            else
            {
                string id = lblidguncelle.Text;
                lstdepo2.Items.Clear();

                conn.Open();
                string sql = "SELECT DEPO.DEPO, DEPO.ID FROM YETKILER INNER JOIN  DEPO ON YETKILER.YETKI = DEPO.ID   WHERE  YETKILER.KULLANICI_ID='" + id + "'  and YETKILER.YETKI_TIPI='DEPO' ";
                SqlDataAdapter da = new SqlDataAdapter(sql, conn);
                DataTable dt = new DataTable();
                da.Fill(dt);
                conn.Close();

                string b_id;
                int l1 = 0;
                for (int j = 0; j < dt.Rows.Count; j++)
                {
                    b_id = dt.Rows[j][1].ToString();

                    l1 = lstdepo1.Items.IndexOf(lstdepo1.Items.FindByValue(b_id));
                    if (l1 != -1)
                    {
                        lstdepo2.Items.Add(lstdepo1.Items[l1]);
                        lstdepo1.Items.Remove(lstdepo1.Items[l1]);
                        lstdepo2.SelectedIndex = -1;
                    }
                    else
                    {
                        ListItem list1 = new ListItem();
                        list1.Text = dt.Rows[j][0].ToString();
                        list1.Value = dt.Rows[j][1].ToString();
                        lstdepo2.Items.Add(list1);
                        lstdepo2.SelectedIndex = -1;
                    }
                }
            }
        }

        void depo_doldur()
        {
            string id = lblidguncelle.Text;
            conn.Open();
            string sql = "SELECT DEPO.DEPO, DEPO.ID FROM DEPO INNER JOIN YETKILER AS Y2 ON Y2.YETKI = DEPO.BOLGE_ID WHERE Y2.YETKI_TIPI = 'BOLGE' AND Y2.KULLANICI_ID = '" + id + "'   ";
            SqlDataAdapter da = new SqlDataAdapter(sql, conn);
            DataTable dt = new DataTable();
            da.Fill(dt);
            conn.Close();
            lstdepo1.DataSource = dt;
            lstdepo1.DataBind();
        }
       
        protected void btnsifreguncelle_Click(object sender, EventArgs e)
        {
            try
            {
                conn.Open();
                SqlCommand sorgu = new SqlCommand();
                sorgu.Connection = conn;
                sorgu.CommandText = "UPDATE KULLANICI SET SIFRE=@KLCS_U WHERE ID=@KLCID_U ";
                sorgu.Parameters.AddWithValue("@KLCS_U", tkod.TextSifrele(txtyenisifre.Text));
                sorgu.Parameters.AddWithValue("@KLCID_U", lblidguncelle.Text);

                sorgu.ExecuteNonQuery();
                conn.Close();
                mesaj_yaz("Tamam", "Şifre Başarıyla Güncellenmiştir.", "guncelle");
            }
            catch (Exception ex)
            {
                mesaj_yaz("Hata", "Kayıt sırasında hata oluştu. " + ex.Message.Replace("'", ""), "guncelle");
            }
        }

        protected void btnkaydet_Click(object sender, EventArgs e)
        {
            try
            {
                conn.Open();
                SqlCommand sorgu = new SqlCommand();
                sorgu.Connection = conn;

                sorgu.CommandText = "SELECT COUNT(*) FROM KULLANICI WHERE TC=@KLC_TC ";
                sorgu.Parameters.AddWithValue("@KLC_TC", txtTC.Text);
                int bulunan_tc = Convert.ToInt16(sorgu.ExecuteScalar().ToString());

                sorgu.CommandText = "SELECT COUNT(*) FROM KULLANICI WHERE KULLANICI_ADI=@KLC_LU ";
                sorgu.Parameters.AddWithValue("@KLC_LU", txtkullaniciadi.Text);
                int user = Convert.ToInt16(sorgu.ExecuteScalar().ToString());

                if (bulunan_tc == 0 && user == 0)
                {
                    string tum_bolge = "false";

                    sorgu.CommandText = "insert INTO KULLANICI(KULLANICI_ADI,SIFRE,TC,ROL_ID,DURUM,ISIM,EMAIL,TELEFON,IL,FIRMA,BOLGE)" +
                        "  VALUES(@KA,'',@KTC,@KROL,'TRUE',@KISIM,@KEPOSTA,@KTLF,@KILE,@KFE,@KBE);SELECT @@IDENTITY AS 'Identity'";
                    sorgu.Parameters.AddWithValue("@KA ", txtkullaniciadi.Text);
                    sorgu.Parameters.AddWithValue("@KTC ", txtTC.Text);
                    sorgu.Parameters.AddWithValue("@KROL ", comrolekle.SelectedValue.ToString());
                    sorgu.Parameters.AddWithValue("@KISIM ", txtisim.Text);
                    sorgu.Parameters.AddWithValue("@KEPOSTA ", txteposta.Text);
                    sorgu.Parameters.AddWithValue("@KTLF ", txttelefon.Text);
                    sorgu.Parameters.AddWithValue("@KILE ", comilekle.SelectedValue);
                    sorgu.Parameters.AddWithValue("@KFE ", comfirmaekle.SelectedValue);
                    sorgu.Parameters.AddWithValue("@KBE ", combolgeekle.SelectedValue);

                    string id = sorgu.ExecuteScalar().ToString();
                    lblidguncelle.Text = id;
                    conn.Close();

                    mesaj_yaz("Tamam", "Kullanıcı başarıyla eklenmiştir.", "kul_ekle");
                    btnkaydet.Enabled = false;
                    btnkaydet.CssClass = "btn btn-success disabled";
                    btnyetkiver.Enabled = true;
                    btnyetkiver.CssClass = "btn btn-success";
                    Kullanici_Listele();
                }
                else
                    mesaj_yaz("Hata", "Bu TC Kimlik No veya Kullanıcı adı daha önce kayıt edilmiştir.", "kul_ekle");
            }
            catch (Exception ex)
            {
                mesaj_yaz("Hata", "Kayıt sırasında hata oluştu. " + ex.Message.Replace("'", ""), "kul_ekle");
            }
        }

        protected void btnkullaniciguncelle_Click(object sender, EventArgs e)
        {
            try
            {
                conn.Open();
                SqlCommand sorgu = new SqlCommand();
                sorgu.Connection = conn;

                sorgu.CommandText = "SELECT COUNT(*) FROM KULLANICI WHERE TC=@KTC_S AND ID<> @KID_S ";
                sorgu.Parameters.AddWithValue("@KTC_S", txttc2.Text);
                sorgu.Parameters.AddWithValue("@KID_S", lblidguncelle.Text);
                int bulunan_tc = Convert.ToInt16(sorgu.ExecuteScalar().ToString());

                btnkullaniciguncelle.Enabled = true;
                btnkullaniciguncelle.CssClass = "btn btn-success ";

                if (bulunan_tc == 0 )
                {
                    string tum_bolge = "false";

                    sorgu.CommandText = "UPDATE KULLANICI SET KULLANICI_ADI=@KADI_U, TC=@KTC_U," +
                        " ROL_ID=@KROL_U, DURUM=@KCHC_U, ISIM=@KISIM_U, EMAIL=@KPOSTA_U, TELEFON=@KTEL_U," +
                        " IL=@KIL_U, FIRMA=@KFIRMA_U, BOLGE=@KBOLGE_U  WHERE ID = @KID_U ";
                    sorgu.Parameters.AddWithValue("@KADI_U ", txtkullaniciadi2.Text);
                    sorgu.Parameters.AddWithValue("@KTC_U ", txttc2.Text);
                    sorgu.Parameters.AddWithValue("@KROL_U ", comrol2.SelectedValue);
                    sorgu.Parameters.AddWithValue("@KCHC_U ", chcdurum.Checked);
                    sorgu.Parameters.AddWithValue("@KISIM_U ", txtisim2.Text);
                    sorgu.Parameters.AddWithValue("@KPOSTA_U ", txteposta2.Text);
                    sorgu.Parameters.AddWithValue("@KTEL_U ", txttelefon2.Text);
                    sorgu.Parameters.AddWithValue("@KIL_U ", comil2.SelectedValue);
                    sorgu.Parameters.AddWithValue("@KFIRMA_U ", comfirma2.SelectedValue);
                    sorgu.Parameters.AddWithValue("@KBOLGE_U ", combolge2.SelectedValue);
                    sorgu.Parameters.AddWithValue("@KID_U ", lblidguncelle.Text);
                    sorgu.ExecuteNonQuery();
                    conn.Close();

                    mesaj_yaz("Tamam", "Kullanıcı başarıyla güncellenmiştir.", "guncelle");
                    btnkullaniciguncelle.Enabled = false;
                    btnkullaniciguncelle.CssClass = "btn btn-success disabled";

                    Kullanici_Listele();
                }
                else
                    mesaj_yaz("Hata", "TCKN daha önce kayıt edilmiştir.", "guncelle");
            }
            catch (Exception ex)
            {
                mesaj_yaz("Hata", "Kayıt sırasında hata oluştu. " + ex.Message.Replace("'", ""), "guncelle");
            }
        }

        protected void comilekle_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(comilekle.SelectedIndex>0)
                combolgeekle.Enabled = true;
            else
                combolgeekle.Enabled = false;

            bolge_listele();
        }


        protected void comil2_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comil2.SelectedIndex > 0)
                combolge2.Enabled = true;
            else
                combolge2.Enabled = false;

            bolge_listele2();
        }

        protected void comilekle_DataBound(object sender, EventArgs e)
        {
            bolge_listele();
        }

        void mesaj_yaz(string hata, string mesaj, string func)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), System.Guid.NewGuid().ToString(), "ShowMessage('" + mesaj + "','" + hata + "','" + func + "');", true);
            mesaj_yaz_yeni(hata, mesaj);
        }

        protected void gridkullanici_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName.Equals("guncelle"))
            {
                int index = Convert.ToInt32(e.CommandArgument);
                string id = gridkullanici.DataKeys[index].Value.ToString();
                lblidguncelle.Text = id;
                
                SqlCommand sorgu = new SqlCommand();
                sorgu.Connection = conn;
                bool durum = false;
                conn.Open();

                txttc2.Text = HttpUtility.HtmlDecode(gridkullanici.Rows[index].Cells[1].Text);
                txtkullaniciadi2.Text = HttpUtility.HtmlDecode(gridkullanici.Rows[index].Cells[2].Text);
                txtisim2.Text = HttpUtility.HtmlDecode(gridkullanici.Rows[index].Cells[3].Text);
                txttelefon2.Text = HttpUtility.HtmlDecode(gridkullanici.Rows[index].Cells[5].Text);
                txteposta2.Text = HttpUtility.HtmlDecode(gridkullanici.Rows[index].Cells[4].Text);
                comrol2.SelectedIndex = comrol2.Items.IndexOf(comrol2.Items.FindByText(HttpUtility.HtmlDecode(gridkullanici.Rows[index].Cells[7].Text)));
                comil2.SelectedIndex = comil2.Items.IndexOf(comil2.Items.FindByText(HttpUtility.HtmlDecode(gridkullanici.Rows[index].Cells[8].Text)));
               // bolge_listele2();
                combolge2.SelectedIndex = combolge2.Items.IndexOf(combolge2.Items.FindByText(HttpUtility.HtmlDecode(gridkullanici.Rows[index].Cells[9].Text)));
                comfirma2.SelectedIndex = comfirma2.Items.IndexOf(comfirma2.Items.FindByText(HttpUtility.HtmlDecode(gridkullanici.Rows[index].Cells[10].Text)));
                if (HttpUtility.HtmlDecode(gridkullanici.Rows[index].Cells[6].Text) == "True")
                    chcdurum.Checked = true;
                else
                    chcdurum.Checked = false;

                lblmodalkullaniciadi.Text = txtisim2.Text;

                sorgu.CommandText = "SELECT ISNULL(TUM_BOLGE_GORSUN,'FALSE') FROM KULLANICI WHERE ID='" + id + "' ";
                durum = Convert.ToBoolean(sorgu.ExecuteScalar());
                chctumbolge2.Checked = durum;

                conn.Close();

                lblidguncelle.Text = id;
                yetki_menusu_ac(id);

                combolge2.Enabled = false;
                
                System.Text.StringBuilder sb = new System.Text.StringBuilder();
                sb.Append(@"<script type='text/javascript'>");
                sb.Append("$(\"#ModalKullaniciGuncelle\").modal(\"show\");");
                sb.Append(@"</script>");
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(),
                           "ModalScript", sb.ToString(), false);
                btnkullaniciguncelle.Enabled = true;
                btnkullaniciguncelle.CssClass = "btn btn-success ";
            }


            if (e.CommandName.Equals("sil"))
            {
                int index = Convert.ToInt32(e.CommandArgument);
                lblidsil.Text = gridkullanici.DataKeys[index].Value.ToString();
                lblislem.Text = "kullanici";
                btnsil.Enabled = true;
                btnsil.CssClass = "btn btn-danger ";

                System.Text.StringBuilder sb = new System.Text.StringBuilder();
                sb.Append(@"<script type='text/javascript'>");
                sb.Append("$(\"#ModalSil\").modal(\"show\");");
                sb.Append(@"</script>");
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(),
                           "ModalScript", sb.ToString(), false);
            }
        }

        protected void btnsil_Click(object sender, EventArgs e)
        {
            if (lblislem.Text == "kullanici")
            {
                SqlCommand sorgu = new SqlCommand();
                sorgu.Connection = conn;
                conn.Open();

                sorgu.CommandText = "DELETE FROM KULLANICI WHERE ID=@KID_SIL";
                sorgu.Parameters.AddWithValue("@KID_SIL", lblidsil.Text);
                sorgu.ExecuteNonQuery();
                conn.Close();
                ScriptManager.RegisterStartupScript(this, this.GetType(), System.Guid.NewGuid().ToString(), "ShowMessage('Kullanıcı başarıyla silinmiştir.','Tamam','sil');", true);

                btnsil.Enabled = false;
                btnsil.CssClass = "btn btn-danger disabled";

                Kullanici_Listele();
            }
        }

        protected void comrolkaydet_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        protected void btnrolkaydet_Click(object sender, EventArgs e)
        {
            try
            {
                conn.Open();
                SqlCommand sorgu = new SqlCommand();
                sorgu.Connection = conn;

                sorgu.CommandText = "SELECT COUNT(*) FROM ROL WHERE ROL=@ROL_ADI ";
                sorgu.Parameters.AddWithValue("@ROL_ADI", txtroladi.Text);
                int bulunan = Convert.ToInt16(sorgu.ExecuteScalar().ToString());

                if (bulunan == 0)
                {
                    sorgu.CommandText = "INSERT INTO ROL (ROL) VALUES (@ROL_ADI1) ";
                    sorgu.Parameters.AddWithValue("@ROL_ADI1", txtroladi.Text);

                    sorgu.ExecuteNonQuery();
                    conn.Close();

                    mesaj_yaz("Tamam", "Rol başarıyla eklenmiştir.", "rol");
                    btnrolekle.Enabled = false;
                    btnrolekle.CssClass = "btn btn-success disabled";

                    rol_listele();
                }
                else
                    mesaj_yaz("Hata", "Bu rol daha önce kayıt edilmiştir.", "rol");
            }
            catch (Exception ex)
            {
                mesaj_yaz("Hata", "Kayıt sırasında hata oluştu. " + ex.Message.Replace("'", ""), "rol");
            }
        }

        //bölge yetkileri

        protected void btnilekle_Click(object sender, EventArgs e)
        {
            if (Session["KULLANICI_ID"] == null)
            {
                Response.Redirect("Giris.aspx?url=" + sayfa);
            }
            else
            {
                try
                {
                    conn.Open();
                    SqlCommand sorgu = new SqlCommand();
                    sorgu.Connection = conn;
                    sorgu.Parameters.AddWithValue("@USER", lblidguncelle.Text);

                    if (chctumbolge2.Checked == true)
                    {
                        sorgu.CommandText = "UPDATE KULLANICI SET TUM_BOLGE_GORSUN='true' WHERE ID=@USER ";

                        sorgu.ExecuteNonQuery();
                    }
                    else
                    {
                        sorgu.CommandText = "UPDATE KULLANICI SET TUM_BOLGE_GORSUN='false' WHERE ID=@USER ";
                        sorgu.ExecuteNonQuery();
                    }

                    string sql = "select BOLGE.ID,BOLGE.BOLGE_ADI FROM YETKILER INNER JOIN BOLGE ON BOLGE.ID=YETKILER.YETKI WHERE YETKILER.KULLANICI_ID='" + lblidguncelle.Text + "' and YETKILER.YETKI_TIPI='BOLGE'";
                    SqlDataAdapter da = new SqlDataAdapter(sql, conn);
                    DataTable dt = new DataTable();
                    sorgu.CommandText = "select BOLGE.ID,BOLGE.BOLGE_ADI FROM YETKILER INNER JOIN BOLGE ON BOLGE.ID=YETKILER.YETKI WHERE YETKILER.KULLANICI_ID='" + lblidguncelle.Text + "' and YETKILER.YETKI_TIPI='BOLGE'";

                    da.Fill(dt);

                    int list_sira = 0;
                    for (int j = 0; j < dt.Rows.Count; j++)
                    {
                        list_sira = lstbolge2.Items.IndexOf(lstbolge2.Items.FindByValue(dt.Rows[j][0].ToString()));
                        if (list_sira == -1)
                        {
                            sorgu.CommandText = "delete from YETKILER where YETKILER.KULLANICI_ID=@USER and YETKILER.YETKI_TIPI='BOLGE' AND YETKILER.YETKI='" + dt.Rows[j][0].ToString() + "' ";
                            sorgu.ExecuteNonQuery();
                        }
                    }

                    for (int l1 = 0; l1 < lstbolge2.Items.Count; l1++)
                    {
                        sorgu.CommandText = "select COUNT(*) FROM YETKILER WHERE KULLANICI_ID=@USER and YETKI='" + lstbolge2.Items[l1].Value + "' and YETKI_TIPI='BOLGE'";
                        int i = Convert.ToInt32(sorgu.ExecuteScalar());

                        if (i == 0)
                        {
                            sorgu.CommandText = "insert INTO YETKILER(KULLANICI_ID,YETKI_TIPI,YETKI)  VALUES(@USER,'BOLGE','" + lstbolge2.Items[l1].Value + "')";
                            sorgu.ExecuteNonQuery();
                        }
                        else
                        {
                        }
                    }
                    conn.Close();
                    mesaj_yaz("Tamam", "Bölge Yetkileri Başarıyla Güncellenmiştir.", "guncelle");
                    depo_doldur();
                }
                catch (Exception ex)
                {
                    mesaj_yaz("Hata", "Kayıt sırasında hata oluşmuştur : " + ex.Message.Replace("'", ""), "guncelle");
                }
            }
        }

        protected void btnbolgeekle_Click(object sender, EventArgs e)
        {

            List<ListItem> deletedItems = new List<ListItem>();
            for (int i = 0; i < lstbolge1.Items.Count; i++)
            {
                if (lstbolge1.Items[i].Selected)
                {
                    lstbolge2.Items.Add(lstbolge1.Items[i]);
                    deletedItems.Add(lstbolge1.Items[i]);
                }
            }
            foreach (ListItem item in deletedItems)
            {
                lstbolge1.Items.Remove(item);
            }
            lstbolge2.SelectedIndex = -1;
        }

        protected void btnbolgeekle0_Click(object sender, EventArgs e)
        {
            List<ListItem> deletedItems = new List<ListItem>();

            for (int i = 0; i < lstbolge2.Items.Count; i++)
            {
                if (lstbolge2.Items[i].Selected)
                {
                    lstbolge1.Items.Add(lstbolge2.Items[i]);
                    deletedItems.Add(lstbolge2.Items[i]);
                }
            }
            foreach (ListItem item in deletedItems)
            {
                lstbolge2.Items.Remove(item);
            }
            lstbolge1.SelectedIndex = -1;
        }

        protected void lstbolge1_DataBound(object sender, EventArgs e)
        {
            yetki_listele();
        }

        protected void btnyetkiver_Click(object sender, EventArgs e)
        {
            txtkullaniciadi2.Text = txtkullaniciadi.Text;
            txttc2.Text = txtTC.Text;
            txtisim2.Text = txtisim.Text;
            txttelefon2.Text = txttelefon.Text;
            txteposta2.Text = txteposta.Text;
            comil2.SelectedIndex = comil2.Items.IndexOf(comil2.Items.FindByValue(comilekle.SelectedValue));
            combolge2.SelectedIndex = combolge2.Items.IndexOf(combolge2.Items.FindByValue(combolgeekle.SelectedValue));
            comfirma2.SelectedIndex = comfirma2.Items.IndexOf(comfirma2.Items.FindByValue(comfirmaekle.SelectedValue));
            chcdurum.Checked = true;
            comrol2.SelectedIndex = comrol2.Items.IndexOf(comrol2.Items.FindByValue(comrolekle.SelectedValue));

            lblmodalkullaniciadi.Text = txtisim2.Text;
            yetki_menusu_ac(lblidguncelle.Text);

            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            sb.Append(@"<script type='text/javascript'>");
            sb.Append("$(\"#ModalKullaniciEkle\").modal(\"hide\");");
            sb.Append("$(\"#ModalKullaniciGuncelle\").modal(\"show\");");
            sb.Append(@"</script>");
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(),
                       "ModalScript", sb.ToString(), false);
        }
        
        // depo yetkileri 

        protected void btndepokaydet_Click(object sender, EventArgs e)
        {
            if (Session["KULLANICI_ID"] == null)
            {
                Response.Redirect("Giris.aspx?url=" + sayfa);
            }
            else
            {
                try
                {
                    conn.Open();
                    SqlCommand sorgu = new SqlCommand();
                    sorgu.Connection = conn;
                    sorgu.Parameters.AddWithValue("@USER", lblidguncelle.Text);

                    string sql = "select DEPO.ID,DEPO.DEPO FROM YETKILER INNER JOIN DEPO ON DEPO.ID=YETKILER.YETKI WHERE YETKILER.KULLANICI_ID='" + lblidguncelle.Text + "' and YETKILER.YETKI_TIPI='DEPO'";
                    SqlDataAdapter da = new SqlDataAdapter(sql, conn);
                    DataTable dt = new DataTable();
                    sorgu.CommandText = "select DEPO.ID,DEPO.DEPO FROM YETKILER INNER JOIN DEPO ON DEPO.ID=YETKILER.YETKI WHERE YETKILER.KULLANICI_ID='" + lblidguncelle.Text + "' and YETKILER.YETKI_TIPI='DEPO'";

                    da.Fill(dt);

                    int list_sira = 0;
                    for (int j = 0; j < dt.Rows.Count; j++)
                    {
                        list_sira = lstdepo2.Items.IndexOf(lstdepo2.Items.FindByValue(dt.Rows[j][0].ToString()));
                        if (list_sira == -1)
                        {
                            sorgu.CommandText = "delete from YETKILER where YETKILER.KULLANICI_ID=@USER and YETKILER.YETKI_TIPI='DEPO' AND YETKILER.YETKI='" + dt.Rows[j][0].ToString() + "' ";

                            sorgu.ExecuteNonQuery();
                        }
                    }

                    for (int l1 = 0; l1 < lstdepo2.Items.Count; l1++)
                    {
                        sorgu.CommandText = "select COUNT(*) FROM YETKILER WHERE KULLANICI_ID=@USER and YETKI='" + lstdepo2.Items[l1].Value + "' and YETKI_TIPI='DEPO'";
                        int i = Convert.ToInt32(sorgu.ExecuteScalar());

                        if (i == 0)
                        {
                            sorgu.CommandText = "insert INTO YETKILER(KULLANICI_ID,YETKI_TIPI,YETKI)  VALUES(@USER,'DEPO','" + lstdepo2.Items[l1].Value + "')";
                            sorgu.ExecuteNonQuery();
                        }
                        else
                        {
                        }
                    }
                    conn.Close();
                    mesaj_yaz("Tamam", "Depo Yetkileri Başarıyla Güncellenmiştir.", "guncelle");

                }
                catch (Exception ex)
                {
                    mesaj_yaz("Hata", "Kayıt sırasında hata oluşmuştur : " + ex.Message.Replace("'", ""), "guncelle");
                }
            }
        }

        protected void btndepoekle_Click(object sender, EventArgs e)
        {
            List<ListItem> deletedItems = new List<ListItem>();
            for (int i = 0; i < lstdepo1.Items.Count; i++)
            {
                if (lstdepo1.Items[i].Selected)
                {
                    lstdepo2.Items.Add(lstdepo1.Items[i]);
                    deletedItems.Add(lstdepo1.Items[i]);
                }
            }
            foreach (ListItem item in deletedItems)
            {
                lstdepo1.Items.Remove(item);
            }
            lstdepo2.SelectedIndex = -1;
        }

        protected void btndepoekle0_Click(object sender, EventArgs e)
        {
            List<ListItem> deletedItems = new List<ListItem>();

            for (int i = 0; i < lstdepo2.Items.Count; i++)
            {
                if (lstdepo2.Items[i].Selected)
                {
                    lstdepo1.Items.Add(lstdepo2.Items[i]);
                    deletedItems.Add(lstdepo2.Items[i]);
                }
            }
            foreach (ListItem item in deletedItems)
            {
                lstdepo2.Items.Remove(item);
            }
            lstdepo1.SelectedIndex = -1;
        }

        protected void lstdepo1_DataBound(object sender, EventArgs e)
        {
            depo_yetki_listele();
        }

        protected void comil2_DataBound(object sender, EventArgs e)
        {
            bolge_listele2();
        }
    }
}