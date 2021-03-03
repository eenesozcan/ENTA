using System;
using System.Collections.Generic;

using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using Sifreleme;

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

            if (tkod.admin_yetki() == true || tkod.Kul_ROL() == "ALTYAPI")
            {

            }
            else
                Response.Redirect("Yetkisiz");

            tkod.ozel_yetki_kontrol("kullanici-yonetim");

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
            //SqlCommand sorgu = new SqlCommand();
            //sorgu.Connection = conn;
            SqlDataAdapter da = new SqlDataAdapter(sql, conn);
            DataSet ds = new DataSet();


            if (comrol.SelectedIndex > 0)
            {
                sql3 = " and (K.ROL_ID=@ROL_ID  ) ";
                da.SelectCommand.Parameters.AddWithValue("ROL_ID", comrol.SelectedValue.ToString());
            }

            if (comilara.SelectedIndex > 0)
            {
                sql3 += " and K.IL=@IL ";
                da.SelectCommand.Parameters.AddWithValue("IL", comilara.SelectedValue.ToString());

            }

            if (txtkullaniciadiarama.Text.Length > 0)
            {
                sql3 += " AND K.ISIM like '%'+@ISIM+'%' ";
                da.SelectCommand.Parameters.AddWithValue("ISIM", txtkullaniciadiarama.Text);

            }



            /*
            if (comfirmaekle.SelectedValue.ToString() != "0")
                sql3 += " and K.FIRMA='" + comfirmaekle.SelectedValue.ToString() + "' ";

            if (combolgeekle.SelectedValue.ToString() != "0")
                sql3 += " and K.BOLGE='" + combolgeekle.SelectedValue.ToString() + "' ";
            */

            sql = "SELECT K.LDAP_USER, K.ID,K.TC,K.KULLANICI_ADI,K.DURUM,R.ROL,K.EMAIL,K.TELEFON,K.ISIM, IL.IL,FIRMA.FIRMA_ADI,BOLGE.BOLGE_ADI FROM KULLANICI as K INNER JOIN ROL AS R ON R.ID=K.ROL_ID LEFT  JOIN IL ON IL.ID=K.IL LEFT JOIN FIRMA ON FIRMA.ID=K.FIRMA LEFT JOIN BOLGE ON BOLGE.ID=K.BOLGE where K.ID>0 ";
            sql += sql3 + " ORDER BY ID ASC ";
            da.SelectCommand.CommandText = sql;
            da.Fill(ds);
            gridkullanici.DataSource = ds.Tables[0];
            gridkullanici.DataBind();
            conn.Close();
            lblsayi.Text = gridkullanici.Rows.Count.ToString() + " adet kayıt bulunmuştur.";


        }

  

        void mesaj_yaz_yeni(string hata, string mesaj)
        {

            //ScriptManager.RegisterStartupScript(this, this.GetType(), System.Guid.NewGuid().ToString(), "ShowMessage('" + mesaj + "','" + hata + "','" + yer + "');", true);
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

            //SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["personel"].ConnectionString);
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

            //SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["personel"].ConnectionString);
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

            //SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["personel"].ConnectionString);
            conn.Open();

            combolgeekle.Items.Clear();
            combolgeekle.Items.Insert(0, new ListItem("-", "0"));
            combolgeekle.AppendDataBoundItems = true;
            sqlbolge.SelectParameters.Clear();

            if (comilekle.SelectedIndex > 0)
                sqlbolge.SelectCommand = "SELECT ID,BOLGE_ADI FROM BOLGE WHERE IL=@IL  ORDER BY BOLGE_ADI";
            else
                sqlbolge.SelectCommand = "SELECT ID,BOLGE_ADI FROM BOLGE ORDER BY BOLGE_ADI";

            sqlbolge.SelectParameters.Clear();
            sqlbolge.SelectParameters.Add("IL", comilekle.SelectedValue.ToString());
            sqlbolge.DataBind();

            conn.Close();

        }

        void firma_listele()
        {

            //SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["personel"].ConnectionString);
            conn.Open();

            comfirmaekle.Items.Clear();
            comfirmaekle.Items.Insert(0, new ListItem("-", "0"));
            comfirmaekle.AppendDataBoundItems = true;



            sqlfirma.SelectCommand = "SELECT FIRMA_ADI,ID FROM FIRMA ORDER BY FIRMA_ADI";

            sqlfirma.DataBind();

            conn.Close();

        }

        void rol_listele(string rol_id)
        {

            if (Session["KULLANICI_ID"] == null)
            {
                Response.Redirect("Giris.aspx?url=" + sayfa);
            }
            else
            {
                //string id = lblid.Text;

                //SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["Randevu"].ConnectionString);
                conn.Open();
                string sql = "SELECT YETKILER.ID, YETKILER.YETKI_TIPI,YETKILER.YETKI,MENU.MENU_ISMI FROM YETKILER INNER JOIN  MENU ON YETKILER.YETKI=MENU.ID  WHERE YETKILER.ROL_ID=@ROL   and YETKILER.YETKI_TIPI='MENU' ";

                SqlDataAdapter da = new SqlDataAdapter(sql, conn);
                da.SelectCommand.Parameters.AddWithValue("ROL", rol_id);
                DataTable dt = new DataTable();
                da.Fill(dt);
                conn.Close();

                string b_id;
                int l1 = 0;

                for (int j = 0; j < dt.Rows.Count; j++)
                {
                    b_id = dt.Rows[j][2].ToString();

                    l1 = listmenu3.Items.IndexOf(listmenu3.Items.FindByValue(b_id));
                    listmenu4.Items.Add(listmenu3.Items[l1]);
                    listmenu3.Items.Remove(listmenu3.Items[l1]);
                    listmenu4.SelectedIndex = -1;


                }

                //SqlDataSource2.SelectCommand = "SELECT MENU_ISMI,ID FROM MENU";
                //string sql = "SELECT MENU.MENU_ISMI,MENU.ID FROM MENU INNER JOIN  YETKILER ON YETKILER.YETKI_NO = MENU.ID WHERE (YETKILER.KULLANICI_ID!='" + id + "') and (YETKILER.YETKI_YIPI='MENU') ";
                //commenu.DataBind();

            }


        }

        public void yetki_menusu_ac(string id)
        {

            lstbolge1.Items.Clear();
            lstbolge2.Items.Clear();

            listyetki1.Items.Clear();
            listyetki2.Items.Clear();

            listmenu1.Items.Clear();
            listmenu2.Items.Clear();

            listmenu3.Items.Clear();
            listmenu4.Items.Clear();

            SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["KabloHE"].ConnectionString);
            SqlCommand sorgu = new SqlCommand();
            sorgu.Connection = conn;
            bool durum = false;
            conn.Open();
            sorgu.CommandText = "SELECT DURUM FROM KULLANICI WHERE ID=@ID  ";
            sorgu.Parameters.AddWithValue("ID", id);
            durum = Convert.ToBoolean(sorgu.ExecuteScalar());
            chcdurum.Checked = durum;

            sorgu.CommandText = "SELECT ISNULL(TUM_BOLGE_GORSUN,'FALSE') FROM KULLANICI WHERE ID=@ID ";
            durum = Convert.ToBoolean(sorgu.ExecuteScalar());
            // chctumbolge.Checked = durum;


            SqlDataSource3.SelectCommand = "SELECT  ID,BOLGE_ADI FROM BOLGE ORDER BY BOLGE_ADI";
            SqlDataSource3.DataBind();
            SqlDataSource8.SelectCommand = "SELECT  ID,YETKI_TIPI FROM YETKI_TIPI ORDER BY YETKI_TIPI";
            SqlDataSource8.DataBind();
            SqlDataSource2.SelectCommand = "SELECT ISNULL(T2.MENU_ISMI, '#') + ' - ' + MENU.MENU_ISMI AS MENU, MENU.ID FROM MENU LEFT OUTER JOIN (SELECT ID, MENU_ISMI FROM MENU AS MENU_1) AS T2 ON T2.ID = MENU.ANA_MENU_ID ORDER BY MENU.MENU_ISMI";
            SqlDataSource2.DataBind();

            SqlDataSource9.SelectCommand = "SELECT ISNULL(T2.MENU_ISMI, '#') + ' - ' + MENU.MENU_ISMI AS MENU, MENU.ID FROM MENU LEFT OUTER JOIN (SELECT ID, MENU_ISMI FROM MENU AS MENU_1) AS T2 ON T2.ID = MENU.ANA_MENU_ID ORDER BY MENU.MENU_ISMI";
            SqlDataSource9.DataBind();

            conn.Close();

            /*
            //menu_yetki_listele();
            if (comrolkaydet.SelectedIndex > -1)
                rol_listele(comrolkaydet.SelectedValue.ToString());
            */

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

                //SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["Randevu"].ConnectionString);
                conn.Open();
                string sql = "SELECT BOLGE.BOLGE_ADI,BOLGE.ID FROM YETKILER INNER JOIN  BOLGE ON YETKILER.YETKI = BOLGE.ID WHERE YETKILER.KULLANICI_ID=@ID   and YETKILER.YETKI_TIPI='BOLGE' ";
                SqlDataAdapter da = new SqlDataAdapter(sql, conn);
                da.SelectCommand.Parameters.AddWithValue("ID", id);
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

        void menu_yetki_listele()
        {

            if (Session["KULLANICI_ID"] == null)
            {
                Response.Redirect("Giris.aspx?url=" + sayfa);
            }
            else
            {
                string id = lblidguncelle.Text;

                //SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["Randevu"].ConnectionString);
                conn.Open();
                string sql = "SELECT YETKILER.ID, YETKILER.YETKI_TIPI,YETKILER.YETKI,MENU.MENU_ISMI FROM YETKILER INNER JOIN  MENU ON YETKILER.YETKI=MENU.ID  WHERE YETKILER.KULLANICI_ID='" + id + "' and YETKILER.YETKI_TIPI='MENU' ";
                SqlDataAdapter da = new SqlDataAdapter(sql, conn);
                DataTable dt = new DataTable();
                da.Fill(dt);
                conn.Close();

                string b_id;
                int l1 = 0;

                for (int j = 0; j < dt.Rows.Count; j++)
                {
                    b_id = dt.Rows[j][2].ToString();

                    l1 = listmenu1.Items.IndexOf(listmenu1.Items.FindByValue(b_id));
                    listmenu2.Items.Add(listmenu1.Items[l1]);
                    listmenu1.Items.Remove(listmenu1.Items[l1]);
                    listmenu2.SelectedIndex = -1;


                }

                //SqlDataSource2.SelectCommand = "SELECT MENU_ISMI,ID FROM MENU";
                //string sql = "SELECT MENU.MENU_ISMI,MENU.ID FROM MENU INNER JOIN  YETKILER ON YETKILER.YETKI_NO = MENU.ID WHERE (YETKILER.KULLANICI_ID!='" + id + "') and (YETKILER.YETKI_YIPI='MENU') ";
                //commenu.DataBind();

            }

        }

        void ozel_yetki_listele()
        {
            if (Session["KULLANICI_ID"] == null)
            {
                Response.Redirect("Giris.aspx?url=" + sayfa);
            }
            else
            {



                string id = lblidguncelle.Text;

                //SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["Randevu"].ConnectionString);
                conn.Open();
                string sql = "SELECT YETKI_TIPI.YETKI_TIPI,YETKI_TIPI.ID FROM YETKILER INNER JOIN  YETKI_TIPI ON YETKILER.YETKI = YETKI_TIPI.ID WHERE YETKILER.KULLANICI_ID='" + id + "'  and YETKILER.YETKI_TIPI='OZEL' ";
                SqlDataAdapter da = new SqlDataAdapter(sql, conn);
                DataTable dt = new DataTable();
                da.Fill(dt);
                conn.Close();

                string b_id;
                int l1 = 0;
                for (int j = 0; j < dt.Rows.Count; j++)
                {
                    b_id = dt.Rows[j][1].ToString();

                    l1 = listyetki1.Items.IndexOf(listyetki1.Items.FindByValue(b_id));
                    listyetki2.Items.Add(listyetki1.Items[l1]);
                    listyetki1.Items.Remove(listyetki1.Items[l1]);
                    listyetki2.SelectedIndex = -1;


                }

            }





        }

        void ozel_yetki_listele_rol()
        {
            if (Session["KULLANICI_ID"] == null)
            {
                Response.Redirect("Giris.aspx?url=" + sayfa);
            }
            else
            {



                //string id = lblid.Text;

                //SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["Randevu"].ConnectionString);
                conn.Open();
                string sql = "SELECT YETKI_TIPI.YETKI_TIPI,YETKI_TIPI.ID FROM YETKILER INNER JOIN  YETKI_TIPI ON YETKILER.YETKI = YETKI_TIPI.ID WHERE YETKILER.ROL_ID='" + comrolkaydet.SelectedValue + "'  and YETKILER.YETKI_TIPI='OZEL' ";
                SqlDataAdapter da = new SqlDataAdapter(sql, conn);
                DataTable dt = new DataTable();
                da.Fill(dt);
                conn.Close();

                string b_id;
                int l1 = 0;
                for (int j = 0; j < dt.Rows.Count; j++)
                {
                    b_id = dt.Rows[j][1].ToString();

                    l1 = listrolozelyetki1.Items.IndexOf(listrolozelyetki1.Items.FindByValue(b_id));
                    listrolozelyetki2.Items.Add(listrolozelyetki1.Items[l1]);
                    listrolozelyetki1.Items.Remove(listrolozelyetki1.Items[l1]);
                    listrolozelyetki2.SelectedIndex = -1;


                }

            }





        }

    
        protected void btnsifreguncelle_Click(object sender, EventArgs e)
        {
            Sifreleme1 sifreleme = new Sifreleme1();

            try
            {
                //SqlConnection baglanti2 = new SqlConnection(ConfigurationManager.ConnectionStrings["Randevu"].ConnectionString);
                conn.Open();
                SqlCommand sorgu = new SqlCommand();
                sorgu.Connection = conn;
                sorgu.CommandText = "UPDATE KULLANICI SET SIFRE='" + sifreleme.TextSifrele(txtyenisifre.Text) + "' ,SIFRE_DEGISTIRME_TARIHI=@TARIH WHERE ID='" + lblidguncelle.Text + "'";
                sorgu.Parameters.AddWithValue("TARIH", DateTime.Today);
                /*
                if (chcsifirla.Checked)
                    sorgu.Parameters.AddWithValue("TARIH", Convert.ToDateTime("01.01.1900"));
                else
                    sorgu.Parameters.AddWithValue("TARIH", DateTime.Today);
                */

                sorgu.ExecuteNonQuery();
                conn.Close();
                mesaj_yaz("Tamam", "Şifre Başarıyla Güncellenmiştir.", "guncelle");

            }
            catch (Exception ex)
            {
                mesaj_yaz("Hata", "Kayıt sırasında hata oluştu. " + ex.Message.Replace("'", ""), "guncelle");


            }
        }

        protected void btnsifregonder_Click(object sender, EventArgs e)
        {
            tkod.sifre_yenile(Session["KULLANICI_ADI"].ToString(), lblidguncelle.Text);
            mesaj_yaz("Tamam", "Şifre Başarıyla Gönderilmiştir.", "guncelle");

        }

        protected void btnkaydet_Click(object sender, EventArgs e)
        {
            try
            {
                //SqlConnection baglanti2 = new SqlConnection(ConfigurationManager.ConnectionStrings["Randevu"].ConnectionString);
                conn.Open();
                SqlCommand sorgu = new SqlCommand();
                sorgu.Connection = conn;

                sorgu.CommandText = "SELECT COUNT(*) FROM KULLANICI WHERE TC=@TC  ";
                sorgu.Parameters.AddWithValue("TC", txtTC.Text);
                int bulunan_tc = Convert.ToInt16(sorgu.ExecuteScalar().ToString());

                sorgu.CommandText = "SELECT COUNT(*) FROM KULLANICI WHERE LDAP_USER=@LDAP ";
                sorgu.Parameters.AddWithValue("LDAP", txtldap.Text);
                int bulunan_ldap = Convert.ToInt16(sorgu.ExecuteScalar().ToString());


                //sorgu.CommandText = "SELECT COUNT(*) FROM KULLANICI WHERE EMAIL='" + txtemail.Text+ "' ";
                //int bulunan_email = Convert.ToInt16(sorgu.ExecuteScalar().ToString());
                if (bulunan_tc == 0 && bulunan_ldap == 0)
                {
                    string tum_bolge = "false";

                    sorgu.CommandText = "insert INTO KULLANICI(KULLANICI_ADI,LDAP_USER,SIFRE,TC,ROL_ID,DURUM,MAIL_BILDIRIM,ISIM,EMAIL,TELEFON,IL,FIRMA,BOLGE)  VALUES(@KULLANICI_ADI,@LDAP,'',@TC,@ROL_ID,'TRUE','TRUE',@ISIM,@EPOSTA,@TELEFON,@IL,@FIRMA,@BOLGE ) ;SELECT @@IDENTITY AS 'Identity'";
                    sorgu.Parameters.AddWithValue("KULLANICI_ADI", txtkullaniciadi.Text);
                    sorgu.Parameters.AddWithValue("ROL_ID", comrolekle.SelectedValue.ToString());
                    //sorgu.Parameters.AddWithValue("LDAP", txtldap.Text);
                    sorgu.Parameters.AddWithValue("ISIM", txtisim.Text);
                    sorgu.Parameters.AddWithValue("EPOSTA", txteposta.Text);
                    sorgu.Parameters.AddWithValue("TELEFON", txttelefon.Text);
                    sorgu.Parameters.AddWithValue("IL", comilekle.SelectedValue);
                    sorgu.Parameters.AddWithValue("FIRMA", comfirmaekle.SelectedValue);
                    sorgu.Parameters.AddWithValue("BOLGE", combolgeekle.SelectedValue);

                    // + "','" + txteposta.Text + "','" + .Text + "','" + .SelectedValue + "','" +  + "','" +  + 
                    //@ISIM,@EPOSTA,@TELEFON,@IL,@FIRMA,@BOLGE
                    string id = sorgu.ExecuteScalar().ToString();
                    lblidguncelle.Text = id;
                    conn.Close();

                    mesaj_yaz("Tamam", "Kullanıcı başarıyla eklenmiştir. Şifre mail adresine gönderilmiştir.", "kul_ekle");
                    btnkaydet.Enabled = false;
                    btnkaydet.CssClass = "btn btn-success disabled";
                    btnyetkiver.Enabled = true;
                    btnyetkiver.CssClass = "btn btn-success";
                    tkod.ilk_sifre_gonder(Session["KULLANICI_ADI"].ToString(), id);
                    Kullanici_Listele();
                }
                else
                    mesaj_yaz("Hata", "TCKN yada LDAP daha önce kayıt edilmiştir.", "kul_ekle");
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
                //SqlConnection baglanti2 = new SqlConnection(ConfigurationManager.ConnectionStrings["Randevu"].ConnectionString);
                conn.Open();
                SqlCommand sorgu = new SqlCommand();
                sorgu.Connection = conn;

                sorgu.CommandText = "SELECT COUNT(*) FROM KULLANICI WHERE TC='" + txttc2.Text + "' AND ID<>'" + lblidguncelle.Text + "' ";
                int bulunan_tc = Convert.ToInt16(sorgu.ExecuteScalar().ToString());

                sorgu.CommandText = "SELECT COUNT(*) FROM KULLANICI WHERE LDAP_USER='" + txtldap2.Text + "' AND ID<>'" + lblidguncelle.Text + "' ";
                int bulunan_ldap = Convert.ToInt16(sorgu.ExecuteScalar().ToString());


                //sorgu.CommandText = "SELECT COUNT(*) FROM KULLANICI WHERE EMAIL='" + txtemail.Text+ "' ";
                //int bulunan_email = Convert.ToInt16(sorgu.ExecuteScalar().ToString());
                if (bulunan_tc == 0 && bulunan_ldap == 0)
                {
                    string tum_bolge = "false";

                    sorgu.CommandText = "UPDATE KULLANICI SET KULLANICI_ADI='" + txtkullaniciadi2.Text + "',LDAP_USER='" + txtldap2.Text + "',TC='" + txttc2.Text + "',ROL_ID='" + comrol2.SelectedValue + "',DURUM='" + chcdurum.Checked + "',ISIM='" + txtisim2.Text + "',EMAIL='" + txteposta2.Text + "',TELEFON='" + txttelefon2.Text + "',IL='" + comil2.SelectedValue + "',FIRMA='" + comfirma2.SelectedValue + "',BOLGE='" + combolge2.SelectedValue + "'  WHERE ID = " + lblidguncelle.Text + " ";
                    sorgu.ExecuteNonQuery();
                    conn.Close();

                    mesaj_yaz("Tamam", "Kullanıcı başarıyla güncellenmiştir.", "guncelle");
                    btnkullaniciguncelle.Enabled = false;
                    btnkullaniciguncelle.CssClass = "btn btn-success disabled";

                    //hasankod.ilk_sifre_gonder(Session["KULLANICI_ADI"].ToString(), id);
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
            bolge_listele();
        }

        protected void comilekle_DataBound(object sender, EventArgs e)
        {
            bolge_listele();
        }

        void mesaj_yaz(string hata, string mesaj, string func)
        {

            ScriptManager.RegisterStartupScript(this, this.GetType(), System.Guid.NewGuid().ToString(), "ShowMessage('" + mesaj + "','" + hata + "','" + func + "');", true);

        }

        protected void gridkullanici_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName.Equals("guncelle"))
            {
                int index = Convert.ToInt32(e.CommandArgument);
                string id = gridkullanici.DataKeys[index].Value.ToString();
                lblidguncelle.Text = id;
                /*
                IEnumerable<DataRow> query = from i in dt.AsEnumerable()
                                             where i.Field<String>("Code").Equals(code)
                select i;
                */
                //SqlConnection conn;
                SqlCommand sorgu = new SqlCommand();
                sorgu.Connection = conn;
                bool durum = false;
                //cmd = new SqlCommand("SELECT RUHSATLAR.ID,DONEM,RUHSAT_NO,RUHSAT_TARIHI,PROJE_TIPI,PROJE_NO,ZEMIN_TAHRIP_BEDELI,RUHSAT_HARCI,KDV,TEMINAT,TOPLAM,ODEME,ICMAL_TARIHI,ODEME_YAZI_NO,ODEME_YAZI_TARIHI,ILCE,ADRES,KAYIT_TARIHI,KULLANICI.ISIM AS KAYIT_EDEN, KESIF_BEDELI,TEMINAT_ID,TIP,KMZ,ID2 FROM RUHSATLAR INNER JOIN KULLANICI ON KULLANICI.ID=RUHSATLAR.KAYIT_EDEN WHERE RUHSATLAR.ID=" + id, conn);
                conn.Open();

                txtkullaniciadi2.Text = HttpUtility.HtmlDecode(gridkullanici.Rows[index].Cells[1].Text);
                txttc2.Text = HttpUtility.HtmlDecode(gridkullanici.Rows[index].Cells[4].Text);
                txtisim2.Text = HttpUtility.HtmlDecode(gridkullanici.Rows[index].Cells[3].Text);
                txttelefon2.Text = HttpUtility.HtmlDecode(gridkullanici.Rows[index].Cells[11].Text);
                txteposta2.Text = HttpUtility.HtmlDecode(gridkullanici.Rows[index].Cells[10].Text);
                comil2.SelectedIndex = comil2.Items.IndexOf(comil2.Items.FindByText(HttpUtility.HtmlDecode(gridkullanici.Rows[index].Cells[7].Text)));
                combolge2.SelectedIndex = combolge2.Items.IndexOf(combolge2.Items.FindByText(HttpUtility.HtmlDecode(gridkullanici.Rows[index].Cells[8].Text)));
                comfirma2.SelectedIndex = comfirma2.Items.IndexOf(comfirma2.Items.FindByText(HttpUtility.HtmlDecode(gridkullanici.Rows[index].Cells[9].Text)));
                if (HttpUtility.HtmlDecode(gridkullanici.Rows[index].Cells[5].Text) == "True")
                    chcdurum.Checked = true;
                else
                    chcdurum.Checked = false;

                comrol2.SelectedIndex = comrol2.Items.IndexOf(comrol2.Items.FindByText(gridkullanici.Rows[index].Cells[7].Text));

                lblmodalkullaniciadi.Text = txtisim2.Text;


                sorgu.CommandText = "SELECT ISNULL(TUM_BOLGE_GORSUN,'FALSE') FROM KULLANICI WHERE ID='" + id + "' ";
                durum = Convert.ToBoolean(sorgu.ExecuteScalar());
                chctumbolge2.Checked = durum;


                //SqlDataAdapter da = new SqlDataAdapter(cmd);
                //DataTable dt = new DataTable();
                //da.Fill(dt);

                conn.Close();

                lblidguncelle.Text = id;
                yetki_menusu_ac(id);

                btnkullaniciguncelle.Enabled = true;
                btnkullaniciguncelle.CssClass = "btn btn-success";
                System.Text.StringBuilder sb = new System.Text.StringBuilder();
                sb.Append(@"<script type='text/javascript'>");
                sb.Append("$(\"#ModalKullaniciGuncelle\").modal(\"show\");");
                sb.Append(@"</script>");
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(),
                           "ModalScript", sb.ToString(), false);

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

            if (e.CommandName.Equals("sifreyenile"))
            {
                int index = Convert.ToInt32(e.CommandArgument);
                string id = gridkullanici.DataKeys[index].Value.ToString();

                tkod.sifre_yenile(Session["KULLANICI_ADI"].ToString(), id);
                mesaj_yaz_yeni("Tamam", gridkullanici.Rows[index].Cells[3].Text + " <br/>kullanıcının yeni şifresi mail adresine gönderilmiştir.");

            }



        }

        protected void btnsil_Click(object sender, EventArgs e)
        {
            if (lblislem.Text == "kullanici")
            {
                SqlCommand sorgu = new SqlCommand();
                sorgu.Connection = conn;
                conn.Open();
                //sorgu.CommandText = "insert INTO PERSONEL (AD,SOYAD,TC,FIRMA_ID,ALT_FIRMA_ID,IL_ID,BOLGE_ID,ISE_GIRIS,GOREVI)  VALUES('" + txtad.Text + "','" + txtsoyad.Text + "','" + txttc.Text + "','" + comfirma.SelectedValue.ToString() + "','" + comaltfirma.SelectedValue.ToString() + "','" + comil.SelectedValue.ToString() + "','" + combolge.SelectedValue.ToString() + "','" + txtgorevi.Text +//  "',convert(varchar, getdate(), 13), '" + Session["KULLANICI_ID"].ToString() + "','" + comil.SelectedValue.ToString() + "');SELECT @@IDENTITY AS 'Identity' ";

                sorgu.CommandText = "DELETE FROM KULLANICI WHERE ID=@ID ";
                sorgu.Parameters.AddWithValue("ID", lblidsil.Text);
                //sorgu.CommandText = "INSERT INTO RUHSATLAR (DONEM,RUHSAT_NO,RUHSAT_TARIHI,PROJE_TIPI,PROJE_NO,ADRES,ILCE,KAYIT_TARIHI,KAYIT_TARIHI)  VALUES('" + txtdonem.Text + "','" + txtruhsatno.Text + "','" + txtruhsattarihi.Text + "','" + comprojetipi.SelectedValue.ToString() + "','" + txtprojeno.Text + "','" + txtadres.Text + "','" + comilce.SelectedValue.ToString() + "','" + Session["KULLANICI_ID"].ToString() + "',convert(varchar, getdate(), 13) ) ";
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

            listmenu3.Items.Clear();
            listmenu4.Items.Clear();
            listrolozelyetki1.Items.Clear();
            listrolozelyetki2.Items.Clear();


            SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["KabloHE"].ConnectionString);
            conn.Open();
            SqlDataSource9.SelectCommand = "SELECT ISNULL(T2.MENU_ISMI, '#') + ' - ' + MENU.MENU_ISMI AS MENU, MENU.ID FROM MENU LEFT OUTER JOIN (SELECT ID, MENU_ISMI FROM MENU AS MENU_1) AS T2 ON T2.ID = MENU.ANA_MENU_ID ORDER BY MENU.MENU_ISMI";
            SqlDataSource9.DataBind();

            SqlDataSource12.SelectCommand = "SELECT  ID,YETKI_TIPI FROM YETKI_TIPI ORDER BY YETKI_TIPI";
            SqlDataSource12.DataBind();

            conn.Close();

        }

        protected void btnrolkaydet_Click(object sender, EventArgs e)
        {

            try
            {
                //SqlConnection baglanti2 = new SqlConnection(ConfigurationManager.ConnectionStrings["Randevu"].ConnectionString);
                conn.Open();
                SqlCommand sorgu = new SqlCommand();
                sorgu.Connection = conn;

                sorgu.CommandText = "SELECT COUNT(*) FROM ROL WHERE ROL=@ROL ";
                sorgu.Parameters.AddWithValue("ROL", txtroladi.Text);
                int bulunan = Convert.ToInt16(sorgu.ExecuteScalar().ToString());


                //sorgu.CommandText = "SELECT COUNT(*) FROM KULLANICI WHERE EMAIL='" + txtemail.Text+ "' ";
                //int bulunan_email = Convert.ToInt16(sorgu.ExecuteScalar().ToString());
                if (bulunan == 0)
                {
                    sorgu.CommandText = "INSERT INTO ROL (ROL) VALUES (@ROL) ";
                    sorgu.ExecuteNonQuery();
                    conn.Close();

                    mesaj_yaz("Tamam", "Rol başarıyla eklenmiştir.", "rol");
                    btnrolekle.Enabled = false;
                    btnrolekle.CssClass = "btn btn-success disabled";

                    //hasankod.ilk_sifre_gonder(Session["KULLANICI_ADI"].ToString(), id);
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

        protected void btnrolozelyetkikaydet_Click(object sender, EventArgs e)
        {
            if (Session["KULLANICI_ID"] == null)
            {
                Response.Redirect("Giris.aspx?url=" + sayfa);
            }
            else
            {

                try
                {

                    //SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["Randevu"].ConnectionString);
                    conn.Open();
                    SqlCommand sorgu = new SqlCommand();
                    sorgu.Connection = conn;
                    string sql = "select YETKILER.ID, YETKILER.YETKI FROM YETKILER WHERE YETKILER.ROL_ID=@ROL   AND YETKILER.YETKI_TIPI='MENU' ";

                    int list_sira = 0;

                    // özel yetkiler
                    sql = "select YETKILER.ID, YETKILER.YETKI FROM YETKILER WHERE YETKILER.ROL_ID=@ROL  AND YETKILER.YETKI_TIPI='OZEL' ";
                    SqlDataAdapter da2 = new SqlDataAdapter(sql, conn);
                    da2.SelectCommand.Parameters.AddWithValue("ROL", comrolkaydet.SelectedValue);
                    DataTable dt2 = new DataTable();
                    sorgu.CommandText = sql;

                    da2.Fill(dt2);

                    sorgu.Parameters.Clear();
                    sorgu.Parameters.AddWithValue("ROL", comrolkaydet.SelectedValue);

                    list_sira = 0;
                    for (int j = 0; j < dt2.Rows.Count; j++)
                    {
                        list_sira = listrolozelyetki2.Items.IndexOf(listrolozelyetki2.Items.FindByValue(dt2.Rows[j][1].ToString()));
                        if (list_sira == -1)
                        {
                            sorgu.CommandText = "delete from YETKILER where YETKILER.ROL_ID=@ROL  and YETKILER.YETKI_TIPI='OZEL' AND YETKILER.YETKI='" + dt2.Rows[j][1].ToString() + "' ";

                            sorgu.ExecuteNonQuery();
                        }
                    }

                    for (int l1 = 0; l1 < listrolozelyetki2.Items.Count; l1++)
                    {
                        sorgu.CommandText = "select COUNT(*) FROM YETKILER WHERE ROL_ID=@ROL and YETKI='" + listrolozelyetki2.Items[l1].Value + "' and YETKI_TIPI='OZEL'";
                        int i = Convert.ToInt32(sorgu.ExecuteScalar());

                        if (i == 0)
                        {
                            sorgu.CommandText = "insert INTO YETKILER(ROL_ID,YETKI_TIPI,YETKI)  VALUES(@ROL,'OZEL','" + listrolozelyetki2.Items[l1].Value + "')";
                            sorgu.ExecuteNonQuery();

                        }
                        else
                        {

                        }
                    }

                    conn.Close();
                    //yetki_listele();
                    mesaj_yaz("Tamam", "Özel Yetkiler başarıyla kaydedilmiştir.", "rol");

                }
                catch (Exception ex)
                {
                    mesaj_yaz("Hata", "Hata Oluştu " + ex.Message.Replace("'", ""), "rol");
                }
            }
        }

        protected void btnrolyetkikaydet_Click(object sender, EventArgs e)
        {
            if (Session["KULLANICI_ID"] == null)
            {
                Response.Redirect("Giris.aspx?url=" + sayfa);
            }
            else
            {

                try
                {

                    //SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["Randevu"].ConnectionString);
                    conn.Open();
                    SqlCommand sorgu = new SqlCommand();
                    sorgu.Connection = conn;
                    string sql = "select YETKILER.ID, YETKILER.YETKI FROM YETKILER WHERE YETKILER.ROL_ID=@ROL AND YETKILER.YETKI_TIPI='MENU' ";
                    SqlDataAdapter da = new SqlDataAdapter(sql, conn);
                    DataTable dt = new DataTable();
                    sorgu.CommandText = sql;
                    sorgu.Parameters.AddWithValue("ROL", comrolkaydet.SelectedValue);
                    da.SelectCommand.Parameters.AddWithValue("ROL", comrolkaydet.SelectedValue);
                    da.Fill(dt);

                    int list_sira = 0;
                    for (int j = 0; j < dt.Rows.Count; j++)
                    {
                        string menu_id = dt.Rows[j][1].ToString();
                        list_sira = listmenu4.Items.IndexOf(listmenu4.Items.FindByValue(menu_id));
                        if (list_sira == -1)
                        {
                            sorgu.CommandText = "delete from YETKILER where YETKILER.ROL_ID=@ROL and YETKILER.YETKI_TIPI='MENU' AND YETKILER.YETKI='" + dt.Rows[j][1].ToString() + "' ";

                            sorgu.ExecuteNonQuery();
                        }
                    }

                    for (int l1 = 0; l1 < listmenu4.Items.Count; l1++)
                    {
                        sorgu.CommandText = "select COUNT(*) FROM YETKILER WHERE ROL_ID=@ROL and YETKI='" + listmenu4.Items[l1].Value + "' and YETKI_TIPI='MENU'";
                        int i = Convert.ToInt32(sorgu.ExecuteScalar());

                        if (i == 0)
                        {
                            sorgu.CommandText = "insert INTO YETKILER(ROL_ID,YETKI_TIPI,YETKI)  VALUES(@ROL,'MENU','" + listmenu4.Items[l1].Value + "')";
                            sorgu.ExecuteNonQuery();

                        }
                        else
                        {

                        }
                    }



                    conn.Close();
                    //yetki_listele();
                    mesaj_yaz("Tamam", "Menü Yetkileri başarıyla kaydedilmiştir.", "rol");
                }
                catch (Exception ex)
                {
                    mesaj_yaz("Hata", "Hata Oluştu " + ex.Message.Replace("'", ""), "rol");

                }
            }
        }

        protected void listrolmenu1_DataBound(object sender, EventArgs e)
        {
            rol_listele(comrolkaydet.SelectedValue);
        }

        protected void btnrolmenuekle_Click(object sender, EventArgs e)
        {
            List<ListItem> deletedItems = new List<ListItem>();

            foreach (ListItem item in listmenu3.Items)
            {
                if (item.Selected)
                {
                    listmenu4.Items.Add(item);
                    deletedItems.Add(item);
                    //listmenu3.Items.Remove(listmenu3.Items[i]);
                    //listmenu4.SelectedIndex = -1;
                }
            }


            foreach (ListItem item in deletedItems)
            {
                listmenu3.Items.Remove(item);
            }

            listmenu4.SelectedIndex = -1;
        }

        protected void btnrolmenucikar_Click(object sender, EventArgs e)
        {
            List<ListItem> deletedItems = new List<ListItem>();

            for (int i = 0; i < listmenu4.Items.Count; i++)
            {
                if (listmenu4.Items[i].Selected)
                {
                    listmenu3.Items.Add(listmenu4.Items[i]);
                    deletedItems.Add(listmenu4.Items[i]);
                    //listmenu4.Items.Remove(listmenu4.Items[i]);
                    //listmenu3.SelectedIndex = -1;
                }
            }
            foreach (ListItem item in deletedItems)
            {
                listmenu4.Items.Remove(item);
            }

            listmenu3.SelectedIndex = -1;
        }

        protected void listrolozelyetki1_DataBound(object sender, EventArgs e)
        {
            ozel_yetki_listele_rol();
        }

        protected void btnrolozelyetkiadd_Click(object sender, EventArgs e)
        {

            List<ListItem> deletedItems = new List<ListItem>();

            for (int i = 0; i < listrolozelyetki1.Items.Count; i++)
            {
                if (listrolozelyetki1.Items[i].Selected)
                {
                    listrolozelyetki2.Items.Add(listrolozelyetki1.Items[i]);
                    deletedItems.Add(listrolozelyetki1.Items[i]);

                    //listrolozelyetki1.Items.Remove(listrolozelyetki1.Items[i]);
                    //listrolozelyetki2.SelectedIndex = -1;
                }
            }
            foreach (ListItem item in deletedItems)
            {
                listrolozelyetki1.Items.Remove(item);
            }

            listrolozelyetki2.SelectedIndex = -1;

        }

        protected void btnrolozelyetkisil_Click(object sender, EventArgs e)
        {

            List<ListItem> deletedItems = new List<ListItem>();

            for (int i = 0; i < listrolozelyetki2.Items.Count; i++)
            {
                if (listrolozelyetki2.Items[i].Selected)
                {
                    listrolozelyetki1.Items.Add(listrolozelyetki2.Items[i]);
                    deletedItems.Add(listrolozelyetki2.Items[i]);

                    //listrolozelyetki2.Items.Remove(listrolozelyetki2.Items[i]);
                    //listrolozelyetki1.SelectedIndex = -1;
                }
            }
            foreach (ListItem item in deletedItems)
            {
                listrolozelyetki2.Items.Remove(item);
            }

            listrolozelyetki1.SelectedIndex = -1;

        }



        //kullanıcı menü yetkleri


        protected void btnmenukaydet_Click(object sender, EventArgs e)
        {
            if (Session["KULLANICI_ID"] == null)
            {
                Response.Redirect("Giris.aspx?url=" + sayfa);
            }
            else
            {

                try
                {

                    //SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["Randevu"].ConnectionString);
                    conn.Open();
                    SqlCommand sorgu = new SqlCommand();
                    sorgu.Connection = conn;
                    string sql = "select YETKILER.ID,YETKILER.YETKI FROM YETKILER WHERE YETKILER.KULLANICI_ID=@ID  and YETKILER.YETKI_TIPI='MENU'";
                    SqlDataAdapter da = new SqlDataAdapter(sql, conn);
                    da.SelectCommand.Parameters.AddWithValue("ID", lblidguncelle.Text);
                    DataTable dt = new DataTable();
                    sorgu.CommandText = sql;
                    sorgu.Parameters.AddWithValue("ID", lblidguncelle.Text);
                    da.Fill(dt);

                    int list_sira = 0;
                    for (int j = 0; j < dt.Rows.Count; j++)
                    {
                        list_sira = listmenu2.Items.IndexOf(listmenu2.Items.FindByValue(dt.Rows[j][1].ToString()));
                        if (list_sira == -1)
                        {
                            sorgu.CommandText = "delete from YETKILER where YETKILER.KULLANICI_ID=@ID  and YETKILER.YETKI_TIPI='MENU' AND YETKILER.YETKI='" + dt.Rows[j][1].ToString() + "' ";
                            sorgu.ExecuteNonQuery();
                        }
                    }

                    for (int l1 = 0; l1 < listmenu2.Items.Count; l1++)
                    {
                        sorgu.CommandText = "select COUNT(*) FROM YETKILER WHERE KULLANICI_ID=@ID and YETKI='" + listmenu2.Items[l1].Value + "' and YETKI_TIPI='MENU'";
                        int i = Convert.ToInt32(sorgu.ExecuteScalar());

                        if (i == 0)
                        {
                            sorgu.CommandText = "insert INTO YETKILER(KULLANICI_ID,YETKI_TIPI,YETKI)  VALUES(@ID,'MENU','" + listmenu2.Items[l1].Value + "')";
                            sorgu.ExecuteNonQuery();

                        }
                        else
                        {

                        }
                    }
                    conn.Close();
                    //yetki_listele();
                    //lbldurummenu.Text = "Yetkiler başarıyla kaydedilmiştir.";
                    mesaj_yaz("Tamam", "Menü Yetkileri Başarıyla Güncellenmiştir.", "guncelle");

                }
                catch (Exception ex)
                {
                    //lbldurummenu.Text = "Hata oluştu " + ex.Message;
                    mesaj_yaz("Hata", "Kayıt sırasında hata oluşmuştur : " + ex.Message.Replace("'", ""), "guncelle");

                }
            }
        }

        protected void btnmenuaktar_Click(object sender, EventArgs e)
        {

            List<ListItem> deletedItems = new List<ListItem>();

            for (int i = 0; i < listmenu1.Items.Count; i++)
            {
                if (listmenu1.Items[i].Selected)
                {
                    listmenu2.Items.Add(listmenu1.Items[i]);
                    deletedItems.Add(listmenu1.Items[i]);

                    //listmenu1.Items.Remove(listmenu1.Items[i]);
                    //listmenu2.SelectedIndex = -1;
                }
            }
            foreach (ListItem item in deletedItems)
            {
                listmenu1.Items.Remove(item);
            }

            listmenu2.SelectedIndex = -1;

        }

        protected void btnmenuaktar2_Click(object sender, EventArgs e)
        {

            List<ListItem> deletedItems = new List<ListItem>();

            for (int i = 0; i < listmenu2.Items.Count; i++)
            {
                if (listmenu2.Items[i].Selected)
                {
                    listmenu1.Items.Add(listmenu2.Items[i]);
                    deletedItems.Add(listmenu2.Items[i]);

                    //listmenu2.Items.Remove(listmenu2.Items[i]);
                    //listmenu1.SelectedIndex = -1;
                }
            }
            foreach (ListItem item in deletedItems)
            {
                listmenu2.Items.Remove(item);
            }


            listmenu1.SelectedIndex = -1;

        }

        protected void listmenu1_DataBound(object sender, EventArgs e)
        {
            menu_yetki_listele();
        }




        //kullanıcı özel yetkleri

        protected void listyetki1_DataBound(object sender, EventArgs e)
        {
            ozel_yetki_listele();
        }

        protected void btnozelyetkisil_Click(object sender, EventArgs e)
        {

            List<ListItem> deletedItems = new List<ListItem>();

            for (int i = 0; i < listyetki2.Items.Count; i++)
            {
                if (listyetki2.Items[i].Selected)
                {
                    listyetki1.Items.Add(listyetki2.Items[i]);
                    deletedItems.Add(listyetki2.Items[i]);

                    //listyetki2.Items.Remove(listyetki2.Items[i]);
                    //listyetki1.SelectedIndex = -1;
                }
            }
            foreach (ListItem item in deletedItems)
            {
                listyetki2.Items.Remove(item);
            }

            listyetki1.SelectedIndex = -1;

        }

        protected void btnozelyetkiekle_Click(object sender, EventArgs e)
        {

            List<ListItem> deletedItems = new List<ListItem>();

            for (int i = 0; i < listyetki1.Items.Count; i++)
            {
                if (listyetki1.Items[i].Selected)
                {
                    listyetki2.Items.Add(listyetki1.Items[i]);
                    deletedItems.Add(listyetki1.Items[i]);

                    //listyetki1.Items.Remove(listyetki1.Items[i]);
                    //listyetki2.SelectedIndex = -1;
                }
            }
            foreach (ListItem item in deletedItems)
            {
                listyetki1.Items.Remove(item);
            }


            listyetki2.SelectedIndex = -1;

        }

        protected void btnozelyetkikaydet_Click(object sender, EventArgs e)
        {
            if (Session["KULLANICI_ID"] == null)
            {
                Response.Redirect("Giris.aspx?url=" + sayfa);
            }
            else
            {

                try
                {

                    //SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["Randevu"].ConnectionString);
                    conn.Open();
                    SqlCommand sorgu = new SqlCommand();
                    sorgu.Connection = conn;
                    string sql = "select YETKI_TIPI.ID,YETKI_TIPI.YETKI_TIPI FROM YETKILER INNER JOIN YETKI_TIPI ON YETKI_TIPI.ID=YETKILER.YETKI WHERE YETKILER.KULLANICI_ID=@ID  and YETKILER.YETKI_TIPI='OZEL'";
                    SqlDataAdapter da = new SqlDataAdapter(sql, conn);
                    da.SelectCommand.Parameters.AddWithValue("ID", lblidguncelle.Text);
                    DataTable dt = new DataTable();
                    //sorgu.CommandText = sql;
                    sorgu.Parameters.AddWithValue("ID", lblidguncelle.Text);
                    da.Fill(dt);

                    int list_sira = 0;
                    for (int j = 0; j < dt.Rows.Count; j++)
                    {
                        list_sira = listyetki2.Items.IndexOf(listyetki2.Items.FindByValue(dt.Rows[j][1].ToString()));
                        if (list_sira == -1)
                        {
                            sorgu.CommandText = "delete from YETKILER where YETKILER.KULLANICI_ID=@ID and YETKILER.YETKI_TIPI='OZEL' AND YETKILER.YETKI='" + dt.Rows[j][0].ToString() + "' ";
                            sorgu.ExecuteNonQuery();
                        }
                    }

                    for (int l1 = 0; l1 < listyetki2.Items.Count; l1++)
                    {
                        sorgu.CommandText = "select COUNT(*) FROM YETKILER WHERE KULLANICI_ID=@ID and YETKI='" + listyetki2.Items[l1].Value + "' and YETKI_TIPI='OZEL'";
                        int i = Convert.ToInt32(sorgu.ExecuteScalar());

                        if (i == 0)
                        {
                            sorgu.CommandText = "insert INTO YETKILER(KULLANICI_ID,YETKI_TIPI,YETKI)  VALUES(@ID,'OZEL','" + listyetki2.Items[l1].Value + "')";
                            sorgu.ExecuteNonQuery();

                        }
                        else
                        {

                        }
                    }
                    conn.Close();
                    //yetki_listele();
                    //lbldurumozel.Text = "Yetkiler başarıyla kaydedilmiştir.";
                    mesaj_yaz("Tamam", "Özel Yetkileri Başarıyla Güncellenmiştir.", "guncelle");

                }
                catch (Exception ex)
                {
                    //lbldurumozel.Text = "Hata oluştu " + ex.Message;
                    mesaj_yaz("Hata", "Kayıt sırasında hata oluşmuştur : " + ex.Message.Replace("'", ""), "guncelle");

                }
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

                    //SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["Randevu"].ConnectionString);
                    conn.Open();
                    SqlCommand sorgu = new SqlCommand();
                    sorgu.Connection = conn;

                    if (chctumbolge2.Checked == true)
                        sorgu.CommandText = "UPDATE KULLANICI SET TUM_BOLGE_GORSUN='true' WHERE ID=@ID ";
                    else
                        sorgu.CommandText = "UPDATE KULLANICI SET TUM_BOLGE_GORSUN='false' WHERE ID=@ID ";

                    sorgu.Parameters.AddWithValue("ID", lblidguncelle.Text);
                    sorgu.ExecuteNonQuery();



                    string sql = "select BOLGE.ID,BOLGE.BOLGE_ADI FROM YETKILER INNER JOIN BOLGE ON BOLGE.ID=YETKILER.YETKI WHERE YETKILER.KULLANICI_ID=@ID and YETKILER.YETKI_TIPI='BOLGE'";
                    SqlDataAdapter da = new SqlDataAdapter(sql, conn);
                    DataTable dt = new DataTable();
                    sorgu.CommandText = "select BOLGE.ID,BOLGE.BOLGE_ADI FROM YETKILER INNER JOIN BOLGE ON BOLGE.ID=YETKILER.YETKI WHERE YETKILER.KULLANICI_ID=@ID and YETKILER.YETKI_TIPI='BOLGE'";
                    da.SelectCommand.Parameters.AddWithValue("ID", lblidguncelle.Text);
                    //sorgu.Parameters.AddWithValue("ID", lblidguncelle.Text); 

                    da.Fill(dt);

                    int list_sira = 0;
                    for (int j = 0; j < dt.Rows.Count; j++)
                    {
                        list_sira = lstbolge2.Items.IndexOf(lstbolge2.Items.FindByValue(dt.Rows[j][0].ToString()));
                        if (list_sira == -1)
                        {
                            sorgu.CommandText = "delete from YETKILER where YETKILER.KULLANICI_ID=@ID and YETKILER.YETKI_TIPI='BOLGE' AND YETKILER.YETKI='" + dt.Rows[j][0].ToString() + "' ";
                            sorgu.ExecuteNonQuery();
                        }
                    }

                    for (int l1 = 0; l1 < lstbolge2.Items.Count; l1++)
                    {
                        sorgu.CommandText = "select COUNT(*) FROM YETKILER WHERE KULLANICI_ID=@ID and YETKI='" + lstbolge2.Items[l1].Value + "' and YETKI_TIPI='BOLGE'";
                        int i = Convert.ToInt32(sorgu.ExecuteScalar());

                        if (i == 0)
                        {
                            sorgu.CommandText = "insert INTO YETKILER(KULLANICI_ID,YETKI_TIPI,YETKI)  VALUES(@ID,'BOLGE','" + lstbolge2.Items[l1].Value + "')";
                            sorgu.ExecuteNonQuery();

                        }
                        else
                        {

                        }
                    }
                    conn.Close();
                    //yetki_listele();
                    //lblyetkidurum.Text = "Yetkiler başarıyla kaydedilmiştir.";
                    mesaj_yaz("Tamam", "Bölge Yetkileri Başarıyla Güncellenmiştir.", "guncelle");

                }
                catch (Exception ex)
                {
                    //lblyetkidurum.Text = "Hata oluştu " + ex.Message;
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
                    //lstbolge1.Items.Remove(lstbolge1.Items[i]);
                    //lstbolge2.SelectedIndex = -1;
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
                    //lstbolge2.Items.Remove(lstbolge2.Items[i]);
                    //lstbolge1.SelectedIndex = -1;
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
            txtldap2.Text = txtldap.Text;
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

       


  
     
    }
}