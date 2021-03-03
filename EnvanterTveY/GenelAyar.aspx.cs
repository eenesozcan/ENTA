using System;
using System.Web.UI;
using System.Web.UI.WebControls;

using System.Data;
using System.Data.SqlClient;
using System.Configuration;

//ok
namespace EnvanterTveY
{
    public partial class GenelAyar : System.Web.UI.Page
    {
        SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["KabloHE"].ConnectionString);
        string sayfa = "GenelAyar";

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
                listele_comdoldur();
                genel_ayar_listele();
                program_ayar_listele();
            }
        }

        protected void btnayarkaydet_Click(object sender, EventArgs e)
        {
            try
            {
                string sql = "";

                SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["KabloHE"].ConnectionString);
                conn.Open();
                SqlCommand sorgu = new SqlCommand();
                sorgu.Connection = conn;
                sorgu.CommandText = "UPDATE AYARLAR SET DEGER=@LOGIN WHERE TIP='LOGIN'  ";
                sorgu.Parameters.AddWithValue("LOGIN", comgiris.SelectedValue.ToString());
                sorgu.ExecuteNonQuery();

                sorgu.CommandText = "UPDATE AYARLAR SET DEGER=@OZEL_LOGIN WHERE TIP='OZEL_LOGIN'  ";
                sorgu.Parameters.AddWithValue("OZEL_LOGIN", txtlocalgiris.Text);
                sorgu.ExecuteNonQuery();

                sorgu.CommandText = "UPDATE AYARLAR SET DEGER=@EPOSTAAYAR WHERE TIP='EPOSTAAYAR'  ";
                sorgu.Parameters.AddWithValue("EPOSTAAYAR", commailserver.SelectedItem.ToString());
                sorgu.ExecuteNonQuery();

                sorgu.CommandText = "UPDATE AYARLAR SET DEGERB=@SSL WHERE TIP='SSL'  ";
                sorgu.Parameters.AddWithValue("SSL", Convert.ToBoolean(comssl.SelectedValue.ToString()));
                sorgu.ExecuteNonQuery();

                sorgu.CommandText = "UPDATE AYARLAR SET DEGERB=@YON   WHERE TIP='PROGRAM-YONLENDIRME-DURUM'  ";
                sorgu.Parameters.AddWithValue("YON", comyonlendirme.SelectedValue.ToString());

                sorgu.ExecuteNonQuery();

                sorgu.CommandText = "UPDATE AYARLAR SET DEGER=@YONLINK  WHERE TIP='PROGRAM-YONLENDIRME-LINK'  ";
                sorgu.Parameters.AddWithValue("YONLINK", txtyonlink.Text);
                sorgu.ExecuteNonQuery();

                sorgu.CommandText = "UPDATE AYARLAR SET DEGER=@POSTA1 WHERE TIP='POSTA-KULLANICI-ADI'  ";
                sorgu.Parameters.AddWithValue("POSTA1", txtpostakullanici.Text);
                sorgu.ExecuteNonQuery();

                sorgu.CommandText = "UPDATE AYARLAR SET DEGER=@POSTA2   WHERE TIP='POSTA-SIFRE'  ";
                sorgu.Parameters.AddWithValue("POSTA2", txtpostasifre.Text);
                sorgu.ExecuteNonQuery();

                sorgu.CommandText = "UPDATE AYARLAR SET DEGER=@POSTA3   WHERE TIP='POSTA-SUNUCU'  ";
                sorgu.Parameters.AddWithValue("POSTA3", txtpostasunucu.Text);
                sorgu.ExecuteNonQuery();

                sorgu.CommandText = "UPDATE AYARLAR SET DEGER=@POSTA4   WHERE TIP='POSTA-PORT'  ";
                sorgu.Parameters.AddWithValue("POSTA4", txtpostaport.Text);
                sorgu.ExecuteNonQuery();

                sorgu.CommandText = "UPDATE AYARLAR SET DEGERB=@LOCALLOGIN   WHERE TIP='LOCAL-DEVELOP-LOGIN'  ";
                sorgu.Parameters.AddWithValue("LOCALLOGIN", comlocalgiris.SelectedValue.ToString());
                sorgu.ExecuteNonQuery();



                //lbl_asgariucret_durum.Text = "Kaydedilmiştir.";
                ScriptManager.RegisterStartupScript(this, this.GetType(), System.Guid.NewGuid().ToString(), "YeniMesaj('Kayıt başarılı.','Tamam');", true);
                conn.Close();


            }
            catch (Exception ex)
            {
                //lbl_asgariucret_durum.Text = "Kayıt sırasında hata oluştu. " + ex.Message;
                ScriptManager.RegisterStartupScript(this, this.GetType(), System.Guid.NewGuid().ToString(), "YeniMesaj('Kayıt sırasında hata oluştu.','Hata');", true);

            }
        }

        void genel_ayar_listele()
        {
            ayar_kontrol();

            bool sonuc = false;
            SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["KabloHE"].ConnectionString);
            conn.Open();
            SqlCommand sorgu = new SqlCommand();

            sorgu.Connection = conn;

            sorgu.CommandText = "SELECT DEGER FROM AYARLAR WHERE TIP='LOGIN'  "; ;
            comgiris.SelectedIndex = comgiris.Items.IndexOf(comgiris.Items.FindByValue(sorgu.ExecuteScalar().ToString()));

            sorgu.CommandText = "SELECT DEGER FROM AYARLAR WHERE TIP='OZEL_LOGIN'  "; ;
            txtlocalgiris.Text = Convert.ToString(sorgu.ExecuteScalar());

            sorgu.CommandText = "SELECT DEGER FROM AYARLAR WHERE TIP='EPOSTAAYAR'  ";
            commailserver.SelectedIndex = commailserver.Items.IndexOf(commailserver.Items.FindByText(sorgu.ExecuteScalar().ToString()));
            //commailserver.Items.FindByText(sorgu.ExecuteScalar().ToString()).Selected=true;

            sorgu.CommandText = "SELECT DEGERB FROM AYARLAR WHERE TIP='SSL'  "; ;
            comssl.SelectedIndex = comssl.Items.IndexOf(comssl.Items.FindByValue(Convert.ToBoolean(sorgu.ExecuteScalar()).ToString()));


            sorgu.CommandText = "SELECT DEGERB FROM AYARLAR WHERE TIP='PROGRAM-YONLENDIRME-DURUM'  ";
            comyonlendirme.SelectedIndex = comyonlendirme.Items.IndexOf(comyonlendirme.Items.FindByValue(sorgu.ExecuteScalar().ToString()));
            //comyonlendirme.Items.FindByText(sorgu.ExecuteScalar().ToString()).Selected = true;


            sorgu.CommandText = "SELECT DEGERB FROM AYARLAR WHERE TIP='LOCAL-DEVELOP-LOGIN'  ";
            comlocalgiris.SelectedIndex = comlocalgiris.Items.IndexOf(comlocalgiris.Items.FindByValue(sorgu.ExecuteScalar().ToString()));
            //comyonlendirme.Items.FindByText(sorgu.ExecuteScalar().ToString()).Selected = true;


            sorgu.CommandText = "SELECT DEGER FROM AYARLAR WHERE TIP='PROGRAM-YONLENDIRME-LINK'  "; ;
            txtyonlink.Text = Convert.ToString(sorgu.ExecuteScalar());

            sorgu.CommandText = "SELECT DEGER FROM AYARLAR WHERE TIP='POSTA-KULLANICI-ADI'  "; ;
            txtpostakullanici.Text = Convert.ToString(sorgu.ExecuteScalar());

            sorgu.CommandText = "SELECT DEGER FROM AYARLAR WHERE TIP='POSTA-SIFRE'  "; ;
            txtpostasifre.Text = Convert.ToString(sorgu.ExecuteScalar());

            sorgu.CommandText = "SELECT DEGER FROM AYARLAR WHERE TIP='POSTA-SUNUCU'  "; ;
            txtpostasunucu.Text = Convert.ToString(sorgu.ExecuteScalar());

            sorgu.CommandText = "SELECT DEGER FROM AYARLAR WHERE TIP='POSTA-PORT'  "; ;
            txtpostaport.Text = Convert.ToString(sorgu.ExecuteScalar());



           


            //LOCAL-DEVELOP-LOGIN

            conn.Close();

        }

        public void ayar_kontrol()
        {
            SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["KabloHE"].ConnectionString);
            conn.Open();
            SqlCommand sorgu = new SqlCommand();

            sorgu.Connection = conn;

            //string sql="if exists(select ID from AYARLAR where TIP = '" + id + "') update ONLINEZIYARETCILER set ZAMAN = DATEADD(mi, 10, getdate()), URL='" + adres + "',IP='" + ipadresi + "' where USERID = '" + id + "' " + "else " + "insert into ONLINEZIYARETCILER (zaman,IP,url,USERID,ISIM) values(DATEADD(mi, 10, getdate()),'" + ipadresi + "','" + adres + "','" + id + "','" + isim + "') " + "", conn);

            sorgu.CommandText = ayar_sql_str("LOGIN");
            sorgu.ExecuteNonQuery();

            sorgu.CommandText = ayar_sql_str("OZEL_LOGIN");
            sorgu.ExecuteNonQuery();

            sorgu.CommandText = ayar_sql_str("EPOSTAAYAR");
            sorgu.ExecuteNonQuery();

            sorgu.CommandText = ayar_sql_str("PROGRAM-YONLENDIRME-DURUM");
            sorgu.ExecuteNonQuery();

            sorgu.CommandText = ayar_sql_str("PROGRAM-YONLENDIRME-LINK");
            sorgu.ExecuteNonQuery();

            sorgu.CommandText = ayar_sql_str("POSTA-KULLANICI-ADI");
            sorgu.ExecuteNonQuery();

            sorgu.CommandText = ayar_sql_str("POSTA-SIFRE");
            sorgu.ExecuteNonQuery();

            sorgu.CommandText = ayar_sql_str("POSTA-SUNUCU");
            sorgu.ExecuteNonQuery();

            sorgu.CommandText = ayar_sql_str("POSTA-PORT");
            sorgu.ExecuteNonQuery();

            sorgu.CommandText = ayar_sql_str("LOCAL-DEVELOP-LOGIN");
            sorgu.ExecuteNonQuery();

            sorgu.CommandText = ayar_sql_str("EPOSTA-SABLON-BASLIK");
            sorgu.ExecuteNonQuery();

            sorgu.CommandText = ayar_sql_str("EPOSTA-SABLON-SON");
            sorgu.ExecuteNonQuery();

            sorgu.CommandText = ayar_sql_str("EPOSTA-SABLON-ONAY");
            sorgu.ExecuteNonQuery();

           

            conn.Close();

        }

        public string ayar_sql_str(string sql)
        {

            sql = "if exists(select ID from AYARLAR where TIP = '" + sql + "') SELECT DEGER FROM AYARLAR WHERE  TIP='" + sql + "' " + "else " + "insert into AYARLAR (DEGER,DEGERB,TIP) values('','FALSE','" + sql + "') " + " ";

            return sql;

        }


        void program_ayar_listele()
        {
            bool sonuc = false;
            SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["KabloHE"].ConnectionString);
            conn.Open();
            SqlCommand sorgu = new SqlCommand();
            sorgu.Connection = conn;

            sorgu.CommandText = "SELECT DEGER FROM AYARLAR WHERE TIP='PROGRAM-ADI'  "; ;
            txtprograminadi.Text = Convert.ToString(sorgu.ExecuteScalar());

            sorgu.CommandText = "SELECT DEGER FROM AYARLAR WHERE TIP='PROGRAM-ADRESI'  "; ;
            txtprograminadresi.Text = Convert.ToString(sorgu.ExecuteScalar());

            conn.Close();

        }

        void mesaj_yaz(string hata, string mesaj, string func)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), System.Guid.NewGuid().ToString(), "ShowMessage('" + mesaj + "','" + hata + "','" + func + "');", true);
        }

        void listele_comdoldur()
        {
            SqlCommand cmd;
            string sql = " ", sql1 = " ";

            sql = "SELECT ID, COM_ADI, SECENEK, DEGER, SIRA FROM COMDOLDUR ";

            sql = sql + sql1;
            cmd = new SqlCommand(sql, conn);
            conn.Open();
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            grid_comdoldur.DataSource = dt;
            grid_comdoldur.DataBind();
            conn.Close();
            lblcomdoldur.Text = grid_comdoldur.Rows.Count.ToString() + " " + "adet kayıt bulunmuştur.";
        }

        protected void btncomdoldurkaydet_Click(object sender, EventArgs e)
        {
            SqlCommand sorgu = new SqlCommand();
            sorgu.Connection = conn;
            conn.Open();

            if (txtcomadi.Text == "" || txtsira.Text == "" || txtsecenek.Text == "" || txtdeger.Text == "")
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), System.Guid.NewGuid().ToString(), "YeniMesaj('Tüm alanların doldurulması gerekmektedir.','Hata');", true);
            }
            else
            {
                try
                {
                    string userid = Session["KULLANICI_ID"].ToString();
                    sorgu.CommandText = "INSERT INTO COMDOLDUR (COM_ADI, SECENEK, DEGER, SIRA, KAYIT_EDEN, KAYIT_TARIHI) " +
                        " VALUES(@CA, @SCNK, @DGR, @SR, @UI, getdate() ) ";
                    sorgu.Parameters.AddWithValue("@CA", txtcomadi.Text);
                    sorgu.Parameters.AddWithValue("@SCNK", txtsecenek.Text);
                    sorgu.Parameters.AddWithValue("@DGR", txtdeger.Text);
                    sorgu.Parameters.AddWithValue("@SR", txtsira.Text);
                    sorgu.Parameters.AddWithValue("@UI", userid);
                    sorgu.ExecuteNonQuery();

                    conn.Close();

                    txtcomadi.Text = "";
                    txtsecenek.Text = "";
                    txtdeger.Text = "";
                    txtsira.Text = "";

                    listele_comdoldur();

                    ScriptManager.RegisterStartupScript(this, this.GetType(), System.Guid.NewGuid().ToString(), "YeniMesaj('Kayıt başarılıdır.','Tamam');", true);
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

        protected void grid_comdoldur_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName.Equals("sil"))
            {
                //int index = Convert.ToInt32(e.CommandArgument);
                lblidsil.Text = Convert.ToString(e.CommandArgument);

                lblislem.Text = "comdoldur-sil";
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
            if (lblislem.Text == "comdoldur-sil")
            {
                SqlCommand sorgu = new SqlCommand();
                sorgu.Connection = conn;
                conn.Open();

                sorgu.CommandText = "DELETE FROM COMDOLDUR WHERE ID=" + lblidsil.Text;
                sorgu.ExecuteNonQuery();
                conn.Close();
                ScriptManager.RegisterStartupScript(this, this.GetType(), System.Guid.NewGuid().ToString(), "ShowMessage('Kayıt başarıyla silinmiştir.','Tamam','sil');", true);
                ScriptManager.RegisterStartupScript(this, this.GetType(), System.Guid.NewGuid().ToString(), "YeniMesaj('Kayıt başarıyla silinmiştir.','Tamam');", true);

                btnsil.Enabled = false;
                btnsil.CssClass = "btn btn-danger disabled";

                if (ConnectionState.Open == conn.State)
                    conn.Close();
                listele_comdoldur();
            }
        }

        protected void grid_comdoldur_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            grid_comdoldur.PageIndex = e.NewPageIndex;
            listele_comdoldur();
        }

        protected void btnprogramayarkaydet_Click(object sender, EventArgs e)
        {
            try
            {
                string sql2 = "";

                SqlConnection conn6 = new SqlConnection(ConfigurationManager.ConnectionStrings["KabloHE"].ConnectionString);
                conn6.Open();
                SqlCommand sorgu = new SqlCommand();
                sorgu.Connection = conn6;

                sorgu.CommandText = "UPDATE AYARLAR SET DEGER=@DEGER1  WHERE TIP = 'PROGRAM-ADI' ";
                sorgu.Parameters.AddWithValue("DEGER1", txtprograminadi.Text);
                sorgu.ExecuteNonQuery();
                sorgu.CommandText = "UPDATE AYARLAR SET DEGER=@DEGER2  WHERE TIP = 'PROGRAM-ADRESI' ";
                sorgu.Parameters.AddWithValue("DEGER2", txtprograminadresi.Text);
                sorgu.ExecuteNonQuery();



                ScriptManager.RegisterStartupScript(this, this.GetType(), System.Guid.NewGuid().ToString(), "YeniMesaj('Kayıt başarılı.','Tamam');", true);

                conn6.Close();
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), System.Guid.NewGuid().ToString(), "YeniMesaj('Hata. Kayıt edilemedi.','Hata');", true);
            }
        }
    }
}