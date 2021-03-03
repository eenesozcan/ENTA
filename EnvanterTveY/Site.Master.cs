using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

//ok
namespace EnvanterTveY
{
    public partial class SiteMaster : MasterPage
    {
        KodT.kodlar tkod = new KodT.kodlar();
        SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["KabloHE"].ConnectionString);
        string programadi = "";

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["KULLANICI_ID"] == null)
            {
                Response.Redirect("Giris.aspx");
            }
            string adres = Request.Url.ToString();

            // sayfa yetkiis var mı yok mu kontrol ediliyor.
            if (!IsPostBack)
                if (sayfa_yetki_kontrol() == false)
                    Server.Transfer("Yetkisiz.aspx");

            if (sayfa_bakim_kontrol() == true)
                Server.Transfer("Bakimda.aspx");
            //Response.Redirect("Default.aspx");


            // güvensiz ise güvenli siteye yönelndir... //



            if (SSL_kontrol() == true)
            {
                string adresb = adres.Substring(0, 5);
                if (adresb == "http:")
                    Response.Redirect(adres.Substring(0, 4) + "s" + adres.Substring(4, adres.Length - 4));



            }


            //sol_menu_olustur();
            string islem = Request.QueryString["sifresilem"];


            if (Session["KULLANICI_ID"] == null && Session["KULLANICI_2_ID"] == null)
            {
                //btncikis.Visible = false;
                Response.Redirect("Giris.aspx");

            }
            else
            {



                if (!IsPostBack)
                {
                    onlineuser();

                    programadi = tkod.ayar_al("PROGRAM-ADI");

                    if (session_kontrol() != true)
                    {
                        Session.Abandon();
                        Response.Redirect("Giris.aspx?url=" + adres);
                    }

                    string ip = GetIP();

                    //menu_olustur();
                    if (Session["KULLANICI_2_ID"] == null)
                        lblgiris.Text = Session["KULLANICI_ADI"].ToString() + " ";
                    else
                        lblgiris.Text = Session["KULLANICI_ADI"].ToString() + " - " + Session["KULLANICI_2_ISIM"].ToString() + " ";
                    btncikis.Visible = true;


                    root_yaz();
                    kul_listele();
                    root_yaz();
                    menu_olustur_sol();
                    lblip.Text = ip;


                }


                //================================================================
                // 30 GÜN İÇİNDE ŞİFRE DEĞİŞTİRME BÖLÜMÜ LDAP İÇİN KAPATILDI

                /*
                if (islem != "1")
                {
                    string s1 = "";
                    s1 = sifre_tarihi();
                    if (s1 != "")
                    {
                        DateTime sonsifre = Convert.ToDateTime(s1);
                        TimeSpan span = DateTime.Now.Subtract(sonsifre);
                        int fark = Convert.ToInt32(span.TotalDays);
                        if (fark > 30)
                            Response.Redirect("SifreDegistir.aspx?sifresilem=1");
                        else
                        {



                        }
                        
                    }
                    else
                        Response.Redirect("SifreDegistir.aspx?sifresilem=1");
                }
                */

            }

            Response.Cache.SetCacheability(HttpCacheability.NoCache);

            Page.Title = Page.Title + " - " + programadi;

           
              
            
            lblgiris.Text = Session["KULLANICI_ADI"].ToString() ;
            lblsayfaolusturulma.Text ="Sayfanın oluşturulma zamanı "+ DateTime.Now.ToString("dd.MM.yyyy HH:mm:ss");


        }

        void kul_listele()
        {
            comkullanici.Items.Clear();
            comkullanici.DataSource = tkod.GetData("SELECT ID, ISIM FROM KULLANICI ORDER BY ISIM ");
            comkullanici.DataBind();
        }

        void root_yaz()
        {
            string adres = Request.Url.ToString();

            string urlPath = Request.Url.AbsolutePath;
            FileInfo fileInfo = new FileInfo(urlPath);
            string pageName = fileInfo.Name;
            adres = pageName.ToLower();
            SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["KabloHE"].ConnectionString);
            conn.Open();
            SqlCommand sorgu = new SqlCommand();
            sorgu.Connection = conn;
            //sorgu.CommandText = "SELECT CASE WHEN T2.MENU_ISMI IS NULL THEN T1.MENU_ISMI ELSE  ISNULL(T3.MENU_ISMI,'')+  T2.MENU_ISMI + T1.MENU_ISMI END AS Expr1 FROM  MENU AS T1 INNER JOIN (SELECT ID, MENU_ISMI + ' >> ' AS MENU_ISMI, ANA_MENU_ID, LINK, DURUM, SIRA, SOL_MENU_SIRA FROM MENU AS MENU_1) AS T2 ON T2.ID = T1.ANA_MENU_ID  LEFT JOIN (SELECT ID, MENU_ISMI + ' >> ' AS MENU_ISMI, ANA_MENU_ID, LINK, DURUM, SIRA, SOL_MENU_SIRA FROM MENU AS MENU_1) AS T3 ON T3.ID = T2.ANA_MENU_ID WHERE LOWER(T1.LINK)='" + adres+"'";
            sorgu.CommandText = " declare @sayfa nvarchar(100) set @sayfa = '" + Request.RawUrl.ToString() + "'  SELECT CASE WHEN T2.MENU_ISMI IS NULL THEN   ' <a    href=\"' + @sayfa + '\"  class=\"menu_root_a\"  >' + T1.MENU_ISMI + '</a>    '  " +
                " ELSE ISNULL(T3.MENU_ISMI,'')+T2.MENU_ISMI + ' <a    href=\"' + @sayfa + '\" class=\"menu_root_a\"  >' + T1.MENU_ISMI + '</a>    '  END AS Expr1  " +
                "   FROM  MENU AS T1  " +
                "    INNER JOIN(SELECT ID, ' <a    href=\"' + LINK + '\"  class=\"menu_root_a\"  >' + MENU_ISMI + '</a>    ' + ' <span style=\"font-size:12;\"> >> </span>' AS MENU_ISMI, ANA_MENU_ID, LINK, DURUM, SIRA, SOL_MENU_SIRA FROM MENU AS MENU_1) AS T2 ON T2.ID = T1.ANA_MENU_ID  " +
                " LEFT JOIN(SELECT ID,  ' <a    href=\"' + LINK + '\"  class=\"menu_root_a\"  >' + MENU_ISMI + '</a>    ' + ' <span style=\"font-size:12;\"> >> </span> ' AS MENU_ISMI, ANA_MENU_ID, LINK, DURUM, SIRA, SOL_MENU_SIRA FROM MENU AS MENU_1) AS T3 ON T3.ID = T2.ANA_MENU_ID  " +
                " WHERE LOWER(T1.LINK)= '" + adres + "'";


            //lblroot.Text= Convert.ToString(sorgu.ExecuteScalar());
            menu_root.InnerHtml = Convert.ToString(sorgu.ExecuteScalar());

            try
            {
                sorgu.CommandText = "  SELECT  '<table><tr><td>Email : </td><td>'+EMAIL+'</td></tr> " +
                    "<tr><td>Rol :</td><td>'+ROL.ROL + '</td></tr>  " +
                    "<tr><td>Son Giriş : </td><td>'+CONVERT(NVARCHAR(10),CAST(SON_GIRIS AS DATETIME),104)+ ' ' + CONVERT(NVARCHAR(8),CAST(SON_GIRIS AS DATETIME) ,108)+'</td></tr> " +
                    "<tr><td>Bölge : </td><td>'+ ISNULL((SELECT   BOLGE.BOLGE_ADI + ', '  FROM BOLGE INNER JOIN YETKILER ON YETKILER.YETKI = BOLGE.ID  WHERE YETKI_TIPI = 'BOLGE' AND YETKILER.KULLANICI_ID = K.ID  FOR XML PATH('')  ),'') +'</td></tr></table> '  " +
                    " FROM KULLANICI AS K   INNER JOIN ROL ON ROL.ID = K.ROL_ID   WHERE K.ID =" + tkod.Kul_ID() + " ";
                lbluserbilgi.Text = Convert.ToString(sorgu.ExecuteScalar());
            }
            catch
            {

            }

            try
            {
                sorgu.CommandText = "select count(*) " +
                    " FROM MAIL AS M INNER JOIN KULLANICI AS K ON M.KIME = K.EMAIL  " +
                    " WHERE  M.KAYIT_TARIHI>dateadd(day,-10,GETDATE() ) and K.ID ='" + tkod.Kul_ID() + "' ";
                lbluayrisayi.Text = Convert.ToString(sorgu.ExecuteScalar());

            }
            catch
            {

            }


            try
            {
                string sql1 = "", sql2 = "";
                if (!tkod.admin_yetki_kontrol())
                    sql1 += "  AND  ( M.KAYIT_EDEN=" + tkod.Kul_ID() + " or M.KIME='" + tkod.Kul_ID() + "'  ) ";

                if (!tkod.admin_yetki_kontrol())
                    sql2 += "  AND  ( KAYIT_EDEN=" + tkod.Kul_ID() + " or KIME='" + tkod.Kul_ID() + "'  ) ";

                sorgu.CommandText = "SELECT count(*) " +
                    "  FROM MESAJ AS M INNER JOIN KULLANICI AS K1 ON K1.ID=M.KIME " +
                    " INNER JOIN KULLANICI AS K2 ON K2.ID=M.KAYIT_EDEN  " +
                    " INNER JOIN ( SELECT ANA_MESAJ,ID FROM  MESAJ WHERE ID>0 " + sql2 + "    ) AS T2 ON T2.ANA_MESAJ=M.ID OR T2.ID=M.ID " +
                    " WHERE OKUNDU='FALSE' AND M.ANA_MESAJ=0 GROUP BY M.ID,M.KONU,M.ANA_MESAJ,K1.ISIM , K2.ISIM ,M.KAYIT_TARIHI  ORDER BY M.ID DESC ";
                lblmesajsayi.Text = Convert.ToString(sorgu.ExecuteScalar());
                if (lblmesajsayi.Text == "")
                    lblmesajsayi.Text = "0";

            }
            catch
            {
                lblmesajsayi.Text = "0";

            }

            //lbluserbilgi.Text = "";
            conn.Close();
        }


        void menu_olustur_sol()
        {
            string k_id = Session["KULLANICI_ID"].ToString();

            SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["KabloHE"].ConnectionString);
            conn.Open();
            SqlCommand sorgu = new SqlCommand();
            sorgu.Connection = conn;

            //sorgu.CommandText = "select YETKI FROM KULLANICI WHERE ID='" + k_id + "'  ";
            //string yetki = Convert.ToString(sorgu.ExecuteScalar());




            if (tkod.Kul_ROL() == "ADMIN")
            {
                sorgu.CommandText = "SELECT * FROM MENU where ANA_MENU_ID='0' ORDER BY SIRA";
            }
            else
            {
                //sorgu.CommandText = "SELECT MENU.LINK,MENU.ID, MENU.MENU_ISMI FROM MENU INNER JOIN YETKILER ON YETKILER.YETKI=MENU.ID WHERE (YETKILER.YETKITIPI='MENU' ) AND (ANA_MENU_ID='0') AND (YETKILER.KULLANICI_ID='" + Session["KULLANICI_ID"].ToString() + "')  ORDER BY MENU.SIRA";
                sorgu.CommandText = "SELECT MENU.SVG, MENU.LINK,MENU.ID, MENU.MENU_ISMI,MENU.SVG FROM MENU INNER JOIN YETKILER ON YETKILER.YETKI=MENU.ID WHERE (YETKILER.YETKI_TIPI='MENU' ) AND (ANA_MENU_ID='0') AND ((YETKILER.KULLANICI_ID='" + Session["KULLANICI_ID"].ToString() + "')  or (YETKILER.ROL_ID='" + Session["KULLANICI_ROL_ID"].ToString() + "')  or (YETKILER.ROL_ID='" + Session["VEKALET_ROL"].ToString() + "')    ) GROUP BY MENU.LINK,MENU.ID, MENU.MENU_ISMI,MENU.SIRA,MENU.SVG ORDER BY MENU.SIRA";
            }


            //DataTable dt = new DataTable();
            SqlDataReader reader;

            try
            {
                //Connection açma ve DataBinding işlemim
                //conn.Open();
                reader = sorgu.ExecuteReader();
                repeater_sol_main.DataSource = reader;
                repeater_sol_main.DataBind();
                reader.Close();
            }
            catch (Exception ex)
            {
                Response.Write("Veri okuma işleminde hata meydana geldi " + ex.Message.ToString());
            }

            conn.Close();
            /*
            string s = "[";
            for (int i = 1; i <= repeater_sol_main.Items.Count; i++)
            {
                if (s.Length > 5)
                    s += ",";
                s += "\"ctl00_repeater_sol_main_ctl0" + i + "_div_menu_main_item\"";
            }

            s += "]";
            */
            //ScriptManager.RegisterStartupScript(this, this.GetType(), System.Guid.NewGuid().ToString(), "menu_islem(" + s + ");", true);
            // menu_islem


        }


        protected void repeater_sol_main_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                string alt_menu_id1 = (e.Item.FindControl("main_menu_id") as HiddenField).Value;
                Repeater rpt_sol_sub = e.Item.FindControl("repeater_sol_sub") as Repeater;
                string sql = "";


                if (tkod.Kul_ROL() == "ADMIN")
                {
                    sql = "SELECT * FROM MENU WHERE ANA_MENU_ID='" + alt_menu_id1 + "' ORDER BY SIRA";
                }
                else
                {
                    //sorgu.CommandText = "SELECT MENU.MENU_ISMI,MENU.ID, MENU.LINK,MENU.SOL_MENU_SIRA FROM MENU INNER JOIN YETKILER ON YETKILER.YETKI=MENU.ID WHERE  (YETKILER.YETKITIPI='MENU' ) AND (MENU.ANA_MENU_ID='" + alt_menu_id + "') AND (YETKILER.KULLANICI_ID='" + Session["KULLANICI_ID"].ToString() + "') ORDER BY MENU.SIRA";
                    sql = "SELECT MENU.MENU_ISMI,MENU.ID, MENU.LINK,MENU.SOL_MENU_SIRA  FROM MENU INNER JOIN YETKILER ON YETKILER.YETKI=MENU.ID WHERE  (YETKILER.YETKI_TIPI='MENU' ) AND (MENU.ANA_MENU_ID='" + alt_menu_id1 + "') AND ((YETKILER.KULLANICI_ID='" + Session["KULLANICI_ID"].ToString() + "') or (YETKILER.ROL_ID='" + Session["KULLANICI_ROL_ID"].ToString() + "')    or (YETKILER.ROL_ID='" + Session["VEKALET_ROL"].ToString() + "') ) GROUP BY MENU.MENU_ISMI,MENU.ID, MENU.LINK,MENU.SOL_MENU_SIRA,MENU.SIRA ORDER BY MENU.SIRA";
                }
                rpt_sol_sub.DataSource = tkod.GetData(sql);
                rpt_sol_sub.DataBind();
            }
        }


        private string GetIP()
        {
            return Request.ServerVariables["REMOTE_ADDR"].ToString();
        }

        protected void lnkbutoncikis_Click(object sender, EventArgs e)
        {
            try
            {
                HttpContext.Current.Application.Remove("usr_" + Session.SessionID);
                Session.Abandon();
                Response.Redirect("Giris");
            }
            catch
            {

            }
            finally
            {

            }
        }


        protected void btndegistir_Click(object sender, EventArgs e)
        {

            Session.Add("KULLANICI_2_ID", Session["KULLANICI_ID"].ToString());
            Session.Add("KULLANICI_2_ISIM", Session["KULLANICI_ADI"].ToString());
            Session.Add("KULLANICI_2_ROL", Session["KULLANICI_ROL"].ToString());
            Session.Add("KULLANICI_2_ROL_ID", Session["KULLANICI_ROL_ID"].ToString());
            //Session.Add("KULLANICI_2_ID", comkullanici.SelectedValue.ToString());

            SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["personel"].ConnectionString);
            conn.Open();
            SqlCommand sorgu = new SqlCommand();
            sorgu.Connection = conn;
            sorgu.CommandText = "select ROL_ID FROM KULLANICI WHERE ID='" + comkullanici.SelectedValue.ToString() + "' ";
            string rol_id = Convert.ToString(sorgu.ExecuteScalar());
            sorgu.CommandText = "select R.ROL FROM KULLANICI AS K INNER JOIN ROL AS R ON R.ID=K.ROL_ID WHERE K.ID='" + comkullanici.SelectedValue.ToString() + "' ";
            string rol = Convert.ToString(sorgu.ExecuteScalar());

            //sorgu.CommandText = "if exists(select ID from ONLINEZIYARETCILER where USERID = '" + comkullanici.SelectedValue + "') update ONLINEZIYARETCILER set SESSION ='" + Session.SessionID.ToString() + "' where USERID = '" + comkullanici.SelectedValue + "' else " + "insert into ONLINEZIYARETCILER (SESSION,USERID,ISIM) values('" + Session.SessionID.ToString() + "','" + comkullanici.SelectedValue + "','" + comkullanici.SelectedItem.ToString() + "') ";
            //sorgu.CommandText = "UPDATE ONLINEZIYARETCILER SET SESSION='" + Session.SessionID.ToString() + "' WHERE USERID='" + id + "' ";
            //sorgu.ExecuteNonQuery();
            string sql = "SELECT ISNULL(ROL,'0') FROM VEKALET WHERE VEKALET.USER_ID='" + comkullanici.SelectedValue.ToString() + "'   AND ( BASLANGIC_TARIHI<=GETDATE() AND BITIS_TARIHI>=GETDATE()-1 )  ";
            sorgu.CommandText = sql;
            //Response.Write(sql);
            string veklaet_rol = Convert.ToString(sorgu.ExecuteScalar());
            if (veklaet_rol == "")
            {
                Session.Add("VEKALET_ROL", "0");
            }
            else
                Session.Add("VEKALET_ROL", veklaet_rol);

            conn.Close();

            Session["KULLANICI_ADI"] = comkullanici.SelectedItem.Text;
            Session["KULLANICI_ID"] = comkullanici.SelectedValue.ToString();
            Session["KULLANICI_ROL_ID"] = rol_id;
            Session["KULLANICI_ROL"] = rol;

            Response.Redirect("");
        }

        protected void btniptal_Click(object sender, EventArgs e)
        {
            Session.Add("KULLANICI_ID", Session["KULLANICI_2_ID"].ToString());
            Session.Add("KULLANICI_ADI", Session["KULLANICI_2_ISIM"].ToString());
            Session.Add("KULLANICI_ROL", Session["KULLANICI_2_ROL"].ToString());
            Session.Add("KULLANICI_ROL_ID", Session["KULLANICI_2_ROL_ID"].ToString());
            Session["KULLANICI_2_ID"] = null;
            Session["KULLANICI_2_ISIM"] = null;
            Session["KULLANICI_2_ROL"] = null;
            Session["KULLANICI_2_ROL_ID"] = null;
            if (Session["VEKALET_ROL"] != null)
                Session["VEKALET_ROL"] = "";


            Response.Redirect("AnaSayfa.aspx");
        }

        public bool session_kontrol()
        {
            if (conn.State == ConnectionState.Closed)
                conn.Open();
            SqlCommand sorgu = new SqlCommand();
            sorgu.Connection = conn;

            if (Session["KULLANICI_2_ID"] == null)
                sorgu.CommandText = "select SESSION FROM ONLINEZIYARETCILER WHERE USERID='" + Session["KULLANICI_ID"].ToString() + "'";
            else
                sorgu.CommandText = "select SESSION FROM ONLINEZIYARETCILER WHERE USERID='" + Session["KULLANICI_2_ID"].ToString() + "'";

            string session = Convert.ToString(sorgu.ExecuteScalar());
            conn.Close();

            if (Session.SessionID.ToString() == session)
                return true;
            else
                return false;
        }

        public bool sayfa_yetki_kontrol()
        {
            int sayi = 0;
            string adres = "";
            string er = "";
            if (Session["KULLANICI_ROL"].ToString() != "ADMIN")
            {
                if (conn.State == ConnectionState.Closed)
                    conn.Open();



                try
                {
                    adres = "";
                    string urlPath = Request.Url.AbsolutePath;
                    string referans = "-";
                    string pageName = "";
                    try
                    {
                        if (Request.UrlReferrer != null)
                            referans = Request.UrlReferrer.Segments[1].ToLower();
                        FileInfo fileInfo = new FileInfo(urlPath);
                        pageName = fileInfo.Name;
                        adres = pageName.ToLower();
                    }
                    catch (Exception ex)
                    {
                        er = "1. bölüm Hatası : " + ex.Message;
                    }

                    if (adres == "sifredegistir.aspx" || adres == "giris.aspx")
                        return true;

                    string rol_id = Session["KULLANICI_ROL_ID"].ToString();
                    string kul_id = tkod.Kul_ID();

                    SqlCommand sorgu = new SqlCommand();
                    sorgu.Connection = conn;
                    sorgu.CommandText = "SELECT count (*) FROM MENU INNER JOIN YETKILER ON YETKILER.YETKI=MENU.ID WHERE (YETKILER.YETKI_TIPI='MENU' ) AND ((YETKILER.KULLANICI_ID='" + kul_id + "')  or (YETKILER.ROL_ID='" + rol_id + "'))   AND LOWER(MENU.LINK)='" + adres + "' ";
                    sayi = Convert.ToInt16(sorgu.ExecuteScalar());
                    //adres = referans;
                    //sorgu.CommandText = "SELECT count (*) FROM MENU INNER JOIN YETKILER ON YETKILER.YETKI=MENU.ID WHERE (YETKILER.YETKI_TIPI='MENU' ) AND ((YETKILER.KULLANICI_ID='" + Session["KULLANICI_ID"].ToString() + "')  or (YETKILER.ROL_ID='" + Session["KULLANICI_ROL_ID"].ToString() + "'))   AND LOWER(MENU.LINK)='" + referans + "' ";
                    //sayi2 = Convert.ToInt16(sorgu.ExecuteScalar());

                    // USER LOG OLUŞTUR. 
                    string USER = "0";
                    string sayfa2 = Request.Url.PathAndQuery;

                    if (Session["KULLANICI_ID"] != null)
                        USER = Session["KULLANICI_ID"].ToString();

                    //sorgu.CommandText = "INSERT INTO USER_LOG (KAYIT_EDEN,KAYIT_TARIHI,IP_ADRES,SAYFA,SAYFA2) VALUES (" + USER + ", getdate(),'" + GetIP() + "','" + pageName + "','" + sayfa2 + "' )  ";
                    //sorgu.ExecuteNonQuery();

                }
                catch (Exception ex)
                {
                    sayi = -1;
                    er = "2. bölüm Hatası : " + ex.Message;
                }
                finally
                {
                    conn.Close();
                }

                if (sayi <= 0)
                {

                    try
                    {

                        /*
                        string kullanici = "", dosyaadi;
                        try
                        {
                            if (Session["KULLANICI_ID"] == null)
                                kullanici = "";
                            else
                                kullanici = "[" + Session["KULLANICI_ID"].ToString() + " - " + Session["KULLANICI_ADI"].ToString() + "] ";
                        }
                        catch
                        {
                            kullanici = "-";
                        }

                        dosyaadi = Server.MapPath("~\\Data\\Hata\\" + DateTime.Now.ToShortDateString().Replace(".", "") + "_HataDosyasi.txt");
                        if (!File.Exists(dosyaadi))
                            File.Create(dosyaadi).Close();

                        Exception exc = Server.GetLastError();

                        StreamWriter hd = new StreamWriter(dosyaadi, true);
                        //Oluşan hatalar HataDosyasi adlı bir dosyaya kaydediliyor. 
                        hd.WriteLine(" ");
                        hd.WriteLine(" ============================================================== ");
                        hd.WriteLine(" ");
                        hd.Write(DateTime.Now.ToString());
                        hd.Write(",[");
                        hd.Write(Request.ServerVariables["REMOTE_ADDR"].ToString());
                        hd.Write("],");
                        hd.Write(kullanici);
                        hd.Write(", Yetkisiz bir sayfaya erişim sağlanmıştır.");

                        hd.WriteLine("");
                        hd.Write("Yönlendirelen Adres: ");
                        hd.WriteLine(Request.UrlReferrer.ToString());
                        hd.Write("Sonuç: ");
                        hd.WriteLine(sayi.ToString());
                        hd.Write("Hata Mesajı: ");
                        hd.WriteLine(er);
                        hd.Write("Raw URL: ");
                        hd.WriteLine(Request.RawUrl != null ? Request.RawUrl : "");
                        hd.Write("Adres path : ");
                        hd.Write(adres);

                        hd.WriteLine();
                        hd.Close();
                        */
                    }
                    catch (Exception ex)
                    {
                        er = "3. bölüm Hatası : " + ex.Message;
                    }

                    string log = "";
                    try
                    {
                        log = sayi.ToString() + "/R " + Request.UrlReferrer != null ? Request.UrlReferrer.ToString() : "" + " /P" + Request.RawUrl != null ? Request.RawUrl : "";
                    }
                    catch
                    {

                    }
                    //tkod.log_kaydet("", log, "YETKISIZ");

                    return false;
                }



            }

            return true;
            /*
            int sayi = 0, sayi2 = 0;
            if (Session["KULLANICI_ROL"].ToString() != "ADMIN")
            {
                if (conn.State == ConnectionState.Closed)
                    conn.Open();

                try
                {
                    string adres = "";
                    string urlPath = Request.Url.AbsolutePath;
                    string referans = "-";
                    if (Request.UrlReferrer != null)
                        referans = Request.UrlReferrer.Segments[1].ToLower();


                    FileInfo fileInfo = new FileInfo(urlPath);
                    string pageName = fileInfo.Name;
                    adres = pageName.ToLower();

                    if (adres == "sifredegistir.aspx" || adres == "giris.aspx" || adres == "projedetayhfc.aspx")
                        return true;

                    SqlCommand sorgu = new SqlCommand();
                    sorgu.Connection = conn;
                    sorgu.CommandText = "SELECT count (*) FROM MENU INNER JOIN YETKILER ON YETKILER.YETKI=MENU.ID WHERE (YETKILER.YETKI_TIPI='MENU' ) AND ((YETKILER.KULLANICI_ID='" + Session["KULLANICI_ID"].ToString() + "')  or (YETKILER.ROL_ID='" + Session["KULLANICI_ROL_ID"].ToString() + "'))   AND LOWER(MENU.LINK)='" + adres + "' ";
                    sayi = Convert.ToInt16(sorgu.ExecuteScalar());
                    adres = referans;
                    sorgu.CommandText = "SELECT count (*) FROM MENU INNER JOIN YETKILER ON YETKILER.YETKI=MENU.ID WHERE (YETKILER.YETKI_TIPI='MENU' ) AND ((YETKILER.KULLANICI_ID='" + Session["KULLANICI_ID"].ToString() + "')  or (YETKILER.ROL_ID='" + Session["KULLANICI_ROL_ID"].ToString() + "'))   AND LOWER(MENU.LINK)='" + referans + "' ";
                    sayi2 = Convert.ToInt16(sorgu.ExecuteScalar());

                }
                catch
                {

                }
                finally
                {
                    conn.Close();
                }

                if (sayi == 0 && sayi2 == 0)
                    return false;


            }

            return true;
            */
        }

        public bool sayfa_bakim_kontrol()
        {
            bool durum = false;
            if (Session["KULLANICI_ROL"].ToString() != "ADMIN")
            {
                if (conn.State == ConnectionState.Closed)
                    conn.Open();

                try
                {
                    string adres = "";
                    string urlPath = Request.Url.AbsolutePath;
                    string referans = "-";
                    if (Request.UrlReferrer != null)
                        referans = Request.UrlReferrer.Segments[1].ToLower();


                    FileInfo fileInfo = new FileInfo(urlPath);
                    string pageName = fileInfo.Name;
                    adres = pageName.ToLower();

                    if (adres == "sifredegistir.aspx" || adres == "giris.aspx" || adres == "personeldetay.aspx")
                        return false;

                    SqlCommand sorgu = new SqlCommand();
                    sorgu.Connection = conn;
                    sorgu.CommandText = "SELECT ISNULL(BAKIM,'FALSE') FROM MENU where LOWER(MENU.LINK)='" + adres + "' ";
                    durum = Convert.ToBoolean(sorgu.ExecuteScalar());


                }
                catch
                {

                }
                finally
                {
                    conn.Close();
                }

                return durum;


            }

            return false;
        }

        public bool SSL_kontrol()
        {
            if (conn.State == ConnectionState.Closed)
                conn.Open();
            SqlCommand sorgu = new SqlCommand();
            sorgu.Connection = conn;
            sorgu.CommandText = "select DEGERB FROM AYARLAR WHERE TIP='SSL' ";
            bool ssl = Convert.ToBoolean(sorgu.ExecuteScalar());
            conn.Close();

            return ssl;
        }

        public void onlineuser()
        {
            SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["KabloHE"].ConnectionString);
            string ipadresi = HttpContext.Current.Request.UserHostAddress.ToString();
            string adres = Request.Url.ToString().Replace("'", "-");

            if (Session["KULLANICI_ID"] != null && Session["KULLANICI_2_ID"] == null)
            {
                string isim = Session["KULLANICI_ADI"].ToString();
                string id = Session["KULLANICI_ID"].ToString();
                conn.Open();
                SqlCommand aktifkullanicilar = new SqlCommand("if exists(select ID from ONLINEZIYARETCILER where USERID = '" + id + "') update ONLINEZIYARETCILER set ZAMAN = DATEADD(mi, 10, getdate()), URL='" + adres + "',IP='" + ipadresi + "' where USERID = '" + id + "' " + "else " + "insert into ONLINEZIYARETCILER (zaman,IP,url,USERID,ISIM) values(DATEADD(mi, 10, getdate()),'" + ipadresi + "','" + adres + "','" + id + "','" + isim + "') " + "", conn);
                aktifkullanicilar.ExecuteNonQuery();
                conn.Close();
                //conn.Dispose();
            }
        }


       

    }
}