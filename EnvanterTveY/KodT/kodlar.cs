using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;

using System.Net.Mail;
using System.Net.Mime;
using System.Security.Cryptography;
using Sifreleme;


namespace EnvanterTveY.KodT
{
    public class kodlar
    {
        public string hash = "enesenes456456";

        public void js(string str, Page pg)
        {
            ScriptManager.RegisterStartupScript(pg, this.GetType(), System.Guid.NewGuid().ToString(), str, true);
        }

        public void mesaj(string hata, string mesaj, Page pg)
        {
            js("YeniMesaj('" + mesaj + "','" + hata + "');", pg);
        }

        public string Kul_ROL()
        {
            /*
            SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["KabloHE"].ConnectionString);

            conn.Open();
            SqlCommand sorgu = new SqlCommand();
            sorgu.Connection = conn;
            sorgu.CommandText = "select R.ROL FROM KULLANICI AS K INNER JOIN ROL AS R ON R.ID=K.ROL_ID WHERE K.ID='" + HttpContext.Current.Session["KULLANICI_ID"].ToString() + "'  ";
            string yetki = Convert.ToString(sorgu.ExecuteScalar());
            conn.Close();
            return yetki;
            */


            return System.Web.HttpContext.Current.Session["KULLANICI_ROL"].ToString();
        }

        public string Kul_ID()
        {

            return System.Web.HttpContext.Current.Session["KULLANICI_ID"].ToString();
        }

        public string Rol_ID()
        {

            return System.Web.HttpContext.Current.Session["KULLANICI_ROL_ID"].ToString();
        }

        public string Rol_ADI()
        {

            return System.Web.HttpContext.Current.Session["KULLANICI_ROL"].ToString();
        }

        public string Kul_Adi()
        {

            return System.Web.HttpContext.Current.Session["KULLANICI_ADI"].ToString();
        }

        public string yetki_kontrol()
        {
            SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["KabloHE"].ConnectionString);

            conn.Open();
            SqlCommand sorgu = new SqlCommand();
            sorgu.Connection = conn;
            sorgu.CommandText = "select R.ROL FROM KULLANICI AS K INNER JOIN ROL AS R ON R.ID=K.ROL_ID WHERE K.ID='" + HttpContext.Current.Session["KULLANICI_ID"].ToString() + "'  ";
            string yetki = Convert.ToString(sorgu.ExecuteScalar());
            conn.Close();
            return yetki;

            //return "ADMIN";
        }



        public string sifre_yenile(string raporolusturan, string id)
        {

            SqlConnection baglanti2 = new SqlConnection(ConfigurationManager.ConnectionStrings["KabloHE"].ConnectionString);
            string sonuc = "ok";
            try
            {
                Guid uid = Guid.NewGuid();
                string sifre = uid.ToString();
                sifre = sifre.Substring(0, 8);


                Sifreleme1 sifreleme = new Sifreleme1();
                string sifrev1 = sifreleme.TextSifrele(sifre);

                baglanti2.Open();
                SqlCommand sorgu = new SqlCommand();
                sorgu.Connection = baglanti2;


                sorgu.CommandText = "UPDATE KULLANICI SET SIFRE='" + sifrev1 + "', SIFRE_DEGISTIRME_TARIHI=@TARIH1 WHERE ID=@id";
                sorgu.Parameters.AddWithValue("id", id);
                sorgu.Parameters.AddWithValue("@TARIH1", Convert.ToDateTime("01.01.1900"));
                sorgu.ExecuteNonQuery();

                sorgu.CommandText = "select ISIM FROM KULLANICI WHERE ID=@id";
                string isim = Convert.ToString(sorgu.ExecuteScalar());

                sorgu.CommandText = "select EMAIL FROM KULLANICI WHERE ID=@id";
                string eposta = Convert.ToString(sorgu.ExecuteScalar());

                sorgu.CommandText = "select KULLANICI_ADI FROM KULLANICI WHERE ID=@id";
                string username = Convert.ToString(sorgu.ExecuteScalar());

                string icerik = "Merhaba " + isim + " <br/> <br/>";
                icerik += "" + ayar_al("PROGRAM-ADI") + " için şifreniz yenilenmiştir. <br>";
                icerik += "Programa " + ayar_al("PROGRAM-ADRESI") + " adresinden erişebilirsiniz. <br>";
                icerik += "Kullanıcı bilgileriniz aşağıdaki gibidir. <br/><br/>";
                icerik += "<br/>Kullanıcı adınız: " + username + "<br/>";
                icerik += "<br/>Şifreniz: " + sifre + "<br/>";
                icerik += "<br/>";

                sonuc = eposta_kaydet(raporolusturan, eposta, "Yeni Şifre", icerik);

            }

            catch (Exception hataTuru)
            {
                sonuc = hataTuru.Message;

            }
            finally
            {

                if (baglanti2.State == ConnectionState.Open)
                    baglanti2.Close();

            }

            return sonuc;

        }


        public bool ozel_yetki_kontrol(string yetki_tipi)
        {
            if (Kul_ROL() != "ADMIN")
            {
                //SqlConnection conn2 = new SqlConnection(ConfigurationManager.ConnectionStrings["personel"].ConnectionString);
                SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["KabloHE"].ConnectionString);
                conn.Open();
                SqlCommand sorgu = new SqlCommand();
                sorgu.Connection = conn;

                // "if exists(select ID from YETKI_TIPI where YETKI_TIPI = '" + yetki_tipi + "')  else  INSERT INTO YETKI_TIPI (YETKI_TIPI, DURUM) VALUES ('" + yetki_tipi + "','TRUE')  "


                sorgu.CommandText = "SELECT COUNT(*) FROM YETKI_TIPI where YETKI_TIPI=@deger ";
                sorgu.Parameters.AddWithValue("deger", yetki_tipi);
                int sayi2 = Convert.ToInt16(sorgu.ExecuteScalar());
                if (sayi2 == 0)
                {
                    sorgu.CommandText = "INSERT INTO YETKI_TIPI (YETKI_TIPI, DURUM) VALUES (@deger,'TRUE') ";
                    sorgu.ExecuteNonQuery();

                }

                sorgu.CommandText = "SELECT COUNT(*) FROM YETKILER INNER JOIN YETKI_TIPI AS YT ON YT.ID=YETKILER.YETKI WHERE (YETKILER.YETKI_TIPI='OZEL' ) AND (YT.YETKI_TIPI=@deger) AND ((YETKILER.KULLANICI_ID='" + HttpContext.Current.Session["KULLANICI_ID"].ToString() + "')  or (YETKILER.ROL_ID='" + HttpContext.Current.Session["KULLANICI_ROL_ID"].ToString() + "')) ";
                int sayi = Convert.ToInt16(sorgu.ExecuteScalar());

                if (sayi == 0)
                {
                    sorgu.CommandText = "select count(*) from YETKILER  INNER JOIN ( SELECT YETKI FROM YETKILER WHERE KULLANICI_ID='" + Kul_ID() + "' AND YETKI_TIPI='BOLGE' ) AS Y2 ON Y2.YETKI=YETKILER.IL  INNER JOIN YETKI_TIPI ON YETKI_TIPI.ID=YETKILER.YETKI  WHERE YETKILER.ROL_ID='" + Rol_ID() + "' AND YETKILER.YETKI_TIPI='ILBAZLI' AND YETKI_TIPI.YETKI_TIPI=@deger  ";
                    sayi = Convert.ToInt16(sorgu.ExecuteScalar());
                }
                conn.Close();

                if (sayi == 0)
                    return false;
                else
                    return true;
            }
            else
                return true;
        }


        public string ilk_sifre_gonder(string raporolusturan, string id)
        {

            SqlConnection baglanti2 = new SqlConnection(ConfigurationManager.ConnectionStrings["KabloHE"].ConnectionString);
            string sonuc = "ok";
            try
            {
                Guid uid = Guid.NewGuid();
                string sifre = uid.ToString();
                sifre = sifre.Substring(0, 8);


                Sifreleme1 sifreleme = new Sifreleme1();
                string sifrev1 = sifreleme.TextSifrele(sifre);

                baglanti2.Open();
                SqlCommand sorgu = new SqlCommand();
                sorgu.Connection = baglanti2;


                sorgu.CommandText = "UPDATE KULLANICI SET SIFRE='" + sifrev1 + "', SIFRE_DEGISTIRME_TARIHI=@TARIH1 WHERE ID=@id";
                sorgu.Parameters.AddWithValue("id", id);
                sorgu.Parameters.AddWithValue("@TARIH1", Convert.ToDateTime("01.01.1900"));
                sorgu.ExecuteNonQuery();

                sorgu.CommandText = "select ISIM FROM KULLANICI WHERE ID=@id";
                string isim = Convert.ToString(sorgu.ExecuteScalar());

                sorgu.CommandText = "select KULLANICI_ADI FROM KULLANICI WHERE ID=@id";
                string username = Convert.ToString(sorgu.ExecuteScalar());


                string icerik = "Merhaba " + isim + " <br/> <br/>";
                icerik += "" + ayar_al("PROGRAM-ADI") + "na yeni kullanıcı kaydınız yapılmıştır. <br>";
                icerik += "Programa " + ayar_al("PROGRAM-ADRESI") + " adresinden erişebilirsiniz. <br>";
                icerik += "Kullanıcı bilgileriniz aşağıdaki gibidir. <br/><br/>";
                icerik += "<br/>Kullanıcı adınız: " + username + "<br/>";
                icerik += "<br/>Şifreniz: " + sifre + "<br/>";
                icerik += "<br/>";

                sorgu.CommandText = "select EMAIL FROM KULLANICI WHERE ID='" + id + "'  ";
                string mail = Convert.ToString(sorgu.ExecuteScalar());
                sonuc = eposta_kaydet(raporolusturan, mail, "Yeni Şifre", icerik);
                //sonuc = eposta_gonder(raporolusturan, mail, "Yeni Şifre", icerik);



            }

            catch (Exception hataTuru)
            {
                sonuc = hataTuru.Message;

            }
            finally
            {

                if (baglanti2.State == ConnectionState.Open)
                    baglanti2.Close();

            }

            return sonuc;

        }


        public bool admin_yetki_kontrol()
        {
            string rol = Kul_ROL();

            if (rol == "ADMIN" || rol == "YONETICI")
                return true;
            else
                return false;
            //return "ADMIN";
        }

        public bool admin_yetki()
        {
            string rol = Kul_ROL();

            if (rol == "ADMIN" || rol == "YONETICI")
                return true;
            else
                return false;           
    }
     
        public bool tum_bolgeleri_gorsun()
        {
            if (!admin_yetki())
                return Convert.ToBoolean(sql_calistir1("SELECT ISNULL(TUM_BOLGE_GORSUN,'False') FROM KULLANICI WHERE KULLANICI.ID='" + HttpContext.Current.Session["KULLANICI_ID"].ToString() + "' "));
            else
                return true;
        }
        
        public string ayar_al(string ayar_adi)
        {
            string sonuc = "0";
            SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["KabloHE"].ConnectionString);
            conn.Open();
            SqlCommand sorgu = new SqlCommand();
            sorgu.Connection = conn;

            sorgu.CommandText = "SELECT COUNT(*) FROM AYARLAR where TIP='" + ayar_adi + "' ";
            int sayi2 = Convert.ToInt16(sorgu.ExecuteScalar());
            if (sayi2 > 0)
            {
                sorgu.CommandText = "SELECT deger FROM AYARLAR where TIP='" + ayar_adi + "' ";
                sonuc = Convert.ToString(sorgu.ExecuteScalar());
            }
            else
            {
                sorgu.CommandText = "INSERT INTO AYARLAR (TIP) VALUES ('" + ayar_adi + "') ";
                sorgu.ExecuteNonQuery();
            }

            conn.Close();
            return sonuc;
        }

        public string sql_calistir1(string sql)
        {
            string sonuc = "";

            SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["KabloHE"].ConnectionString);
            conn.Open();
            SqlCommand sorgu = new SqlCommand();
            sorgu.Connection = conn;

            sorgu.CommandText = sql;
            sonuc = Convert.ToString(sorgu.ExecuteScalar());

            conn.Close();
            return sonuc;
        }

        public string sql_calistir_port(string sql)
        {
            string sonuc = "";

            SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["KabloHE_Port"].ConnectionString);
            conn.Open();
            SqlCommand sorgu = new SqlCommand();
            sorgu.Connection = conn;

            sorgu.CommandText = sql;
            sonuc = Convert.ToString(sorgu.ExecuteScalar());

            conn.Close();
            return sonuc;
        }

        public string sql_calistir_param1(string sql, SqlParameter[] param)
        {
            string sonuc = "";
            SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["KabloHE"].ConnectionString);
            conn.Open();
            SqlCommand sorgu = new SqlCommand();
            sorgu.Connection = conn;

            sorgu.CommandText = sql;

            for (int i = 0; i < param.Count(); i++)
                sorgu.Parameters.Add(param[i]);

            sorgu.Parameters.AddWithValue("nulldeger", DBNull.Value);
            sonuc = Convert.ToString(sorgu.ExecuteScalar());

            conn.Close();
            return sonuc;
        }

        public string sql_calistir_param(string sql, SqlParameter param)
        {
            string sonuc = "";
            SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["KabloHE"].ConnectionString);
            conn.Open();
            SqlCommand sorgu = new SqlCommand();
            sorgu.Connection = conn;

            sorgu.CommandText = sql;
            sorgu.Parameters.Add(param);
            sorgu.Parameters.AddWithValue("nulldeger", DBNull.Value);

            sonuc = Convert.ToString(sorgu.ExecuteScalar());

            conn.Close();
            return sonuc;
        }

        public string yetki_tablosu_il()
        {
            if (!tum_bolgeleri_gorsun())
                return " INNER JOIN YETKILER AS Y ON Y.YETKI=IL.ID   ";
            else
                return "";
        }

        public string yetki_tablosu_2_il()
        {
            if (!tum_bolgeleri_gorsun())
                return " AND Y.YETKI_TIPI='IL' AND Y.KULLANICI_ID='" + Kul_ID() + "'  ";
            else
                return " ";
        }


        public string yetki_tablosu()
        {
            if (!tum_bolgeleri_gorsun())
                return " INNER JOIN YETKILER AS Y ON Y.YETKI=BOLGE.ID   ";
            else
                return "";
        }

        public string yetki_tablosu_2()
        {
            if (!tum_bolgeleri_gorsun())
                return " AND Y.YETKI_TIPI='BOLGE' AND Y.KULLANICI_ID='" + Kul_ID() + "'  ";
            else
                return " ";
        }

        public string yetki_tablosu_inner(string depo_db, string depo_db2)
        {
            string sql2 = "";
            if (depo_db2 != "")
                sql2 = " OR Y.YETKI=" + depo_db2 + ".ID   ";
            if (!tum_bolgeleri_gorsun())
                return " INNER JOIN YETKILER AS Y ON Y.YETKI=" + depo_db + ".ID " + sql2;
            else
                return "";
        }

        public string yetki_tablosu_filtre(string yetki_tipi)
        {
            if (!tum_bolgeleri_gorsun())
                return " AND Y.YETKI_TIPI='" + yetki_tipi + "' AND Y.KULLANICI_ID='" + Kul_ID() + "'  ";
            else
                return " ";
        }

        public string yetki_tablosu_inner_filtre(string depo_db)
        {
            if (!tum_bolgeleri_gorsun())
                return " AND Y.YETKI=" + depo_db + ".ID ";
            else
                return "";
        }

        public DataTable GetData(string sql)
        {
            SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["KabloHE"].ConnectionString);

            DataTable dt = new DataTable();
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conn;
            conn.Open();
            cmd.CommandText = sql;
            SqlDataReader dr = cmd.ExecuteReader();
            dt.Load(dr);
            conn.Close();
            return dt;
        }
        public DataTable GetData_port(string sql)
        {
            SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["KabloHE_Port"].ConnectionString);

            DataTable dt = new DataTable();
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conn;
            conn.Open();
            cmd.CommandText = sql;
            SqlDataReader dr = cmd.ExecuteReader();
            dt.Load(dr);
            conn.Close();
            return dt;
        }


        public DataTable GetData_param(SqlParameter[] param, string sql)
        {
            SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["KabloHE"].ConnectionString);

            //string sql = "", sql2 = "";
            DataTable dt = new DataTable();
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conn;
            conn.Open();
            cmd.CommandText = sql;

            for (int i = 0; i < param.Count(); i++)
                cmd.Parameters.Add(param[i]);

            SqlDataReader dr = cmd.ExecuteReader();
            dt.Load(dr);
            //conn.Close();
            return dt;
        }

        public DataTable GetData_param(SqlParameter param, string sql)
        {
            SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["KabloHE"].ConnectionString);

            //string sql = "", sql2 = "";
            DataTable dt = new DataTable();
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conn;
            conn.Open();
            cmd.CommandText = sql;
            cmd.Parameters.Add(param);

            SqlDataReader dr = cmd.ExecuteReader();
            dt.Load(dr);
            //conn.Close();
            return dt;
        }


        public DataTable dt_bolge_Liste()
        {
            SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["KabloHE"].ConnectionString);

            string sql = "", sql2 = "";
            DataTable dt = new DataTable();
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conn;

            conn.Open();

            if (tum_bolgeleri_gorsun())
                sql = "SELECT BOLGE.BOLGE_ADI, BOLGE.ID FROM BOLGE  ";
            else
                sql = "SELECT BOLGE.BOLGE_ADI, BOLGE.ID FROM BOLGE INNER JOIN  YETKILER ON YETKILER.YETKI = BOLGE.ID  WHERE (YETKILER.YETKI_TIPI = 'BOLGE') AND (YETKILER.KULLANICI_ID = '" + HttpContext.Current.Session["KULLANICI_ID"].ToString() + "')    ";

            sql += " ORDER BY BOLGE.BOLGE_ADI";
            cmd.CommandText = sql;
            SqlDataReader dr = cmd.ExecuteReader();
            dt.Load(dr);
            conn.Close();

            return dt;
        }

        public DataTable dt_bolge_Liste(string il)
        {
            SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["KabloHE"].ConnectionString);

            string sql = "", sql2 = "";
            DataTable dt = new DataTable();
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conn;

            conn.Open();

            if (tum_bolgeleri_gorsun())
                sql = "SELECT BOLGE.BOLGE_ADI, BOLGE.ID FROM BOLGE   WHERE IL=" + il + "  ";
            else
                sql = "SELECT BOLGE.BOLGE_ADI, BOLGE.ID FROM BOLGE INNER JOIN  YETKILER ON YETKILER.YETKI = BOLGE.ID  WHERE (YETKILER.YETKI_TIPI = 'BOLGE') AND (YETKILER.KULLANICI_ID = '" + HttpContext.Current.Session["KULLANICI_ID"].ToString() + "')   and  BOLGE.IL=" + il + "   ";

            sql += " ORDER BY BOLGE.BOLGE_ADI";
            cmd.CommandText = sql;
            SqlDataReader dr = cmd.ExecuteReader();
            dt.Load(dr);
            conn.Close();

            return dt;
        }

        public DataTable dt_il_Liste()
        {
            SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["KabloHE"].ConnectionString);

            string sql = "", sql2 = "";
            DataTable dt = new DataTable();
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conn;

            conn.Open();

            if (tum_bolgeleri_gorsun())
                sql = "SELECT IL.IL, IL.ID FROM IL  ";
            else
                sql = "SELECT IL.ID, IL.IL FROM IL  INNER JOIN BOLGE ON BOLGE.IL=IL.ID INNER JOIN  YETKILER ON YETKILER.YETKI = BOLGE.ID  WHERE (YETKILER.YETKI_TIPI = 'BOLGE') AND (YETKILER.KULLANICI_ID = '" + HttpContext.Current.Session["KULLANICI_ID"].ToString() + "')  GROUP BY IL.ID,IL.IL  ";

            sql += " ORDER BY IL.IL";
            cmd.CommandText = sql;
            SqlDataReader dr = cmd.ExecuteReader();
            dt.Load(dr);
            conn.Close();

            return dt;
        }

        public DataTable dt_depo_Liste()
        {
            return dt_depo_Liste("0");
        }

        public DataTable dt_depo_Liste(string bolge)
        {
            SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["KabloHE"].ConnectionString);

            string sql = "", sql2 = "";
            DataTable dt = new DataTable();
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conn;

            conn.Open();

            if (bolge == "0")
            {
                if (tum_bolgeleri_gorsun())
                    sql = "SELECT DEPO.ID,DEPO.DEPO FROM DEPO  ";
                else
                    sql = "SELECT DEPO.ID,DEPO.DEPO FROM DEPO  INNER JOIN  YETKILER ON YETKILER.YETKI = DEPO.ID  WHERE (YETKILER.YETKI_TIPI = 'DEPO') AND (YETKILER.KULLANICI_ID = '" + HttpContext.Current.Session["KULLANICI_ID"].ToString() + "')   GROUP BY DEPO.ID, DEPO.DEPO  ";
            }
            else
            {
                if (tum_bolgeleri_gorsun())
                    sql = "SELECT DEPO.ID,DEPO.DEPO FROM DEPO WHERE DEPO.BOLGE_ID=" + bolge + "  ";
                else
                    sql = "SELECT DEPO.ID,DEPO.DEPO FROM DEPO  INNER JOIN  YETKILER ON YETKILER.YETKI = DEPO.ID  WHERE DEPO.BOLGE_ID=" + bolge + " AND (YETKILER.YETKI_TIPI = 'DEPO') AND (YETKILER.KULLANICI_ID = '" + HttpContext.Current.Session["KULLANICI_ID"].ToString() + "')   GROUP BY DEPO.ID, DEPO.DEPO  ";
            }

            sql += " ORDER BY DEPO.DEPO";
            cmd.CommandText = sql;
            SqlDataReader dr = cmd.ExecuteReader();
            dt.Load(dr);
            conn.Close();

            return dt;
        }

        public DataTable dt_depo_Liste_bolge(string bolge)
        {
            SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["KabloHE"].ConnectionString);

            string sql = "", sql2 = "";
            DataTable dt = new DataTable();
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conn;

            conn.Open();

            if (bolge == "0")
            {
                if (tum_bolgeleri_gorsun())
                    sql = "SELECT DEPO.ID,DEPO.DEPO FROM DEPO  ";
                else
                    sql = "SELECT DEPO.ID,DEPO.DEPO FROM DEPO  INNER JOIN  YETKILER ON YETKILER.YETKI = DEPO.BOLGE_ID  WHERE (YETKILER.YETKI_TIPI = 'BOLGE') AND (YETKILER.KULLANICI_ID = '" + HttpContext.Current.Session["KULLANICI_ID"].ToString() + "')   GROUP BY DEPO.ID, DEPO.DEPO  ";
            }
            else
            {
                if (tum_bolgeleri_gorsun())
                    sql = "SELECT DEPO.ID,DEPO.DEPO FROM DEPO WHERE DEPO.BOLGE_ID=" + bolge + "  ";
                else
                    sql = "SELECT DEPO.ID,DEPO.DEPO FROM DEPO  INNER JOIN  YETKILER ON YETKILER.YETKI = DEPO.BOLGE_ID  WHERE DEPO.BOLGE_ID=" + bolge + " AND (YETKILER.YETKI_TIPI = 'BOLGE') AND (YETKILER.KULLANICI_ID = '" + HttpContext.Current.Session["KULLANICI_ID"].ToString() + "')   GROUP BY DEPO.ID, DEPO.DEPO  ";
            }

            sql += " ORDER BY DEPO.DEPO";
            cmd.CommandText = sql;
            SqlDataReader dr = cmd.ExecuteReader();
            dt.Load(dr);
            conn.Close();

            return dt;
        }

        public DataTable sevk_malzeme(string sevkid)
        {
            SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["KabloHE"].ConnectionString);
            string sql = "SELECT MSM.ID, MM.MARKA, MMO.MODEL, M.SERI_NO, MD.DURUM, " +
                " CASE WHEN MSM.GARANTIBITIS > GETDATE() THEN 'Garantisi devam ediyor. ' +  Convert(NVARCHAR(10),MSM.GARANTIBITIS, 104) ELSE 'Garantisi Bitmiştir. ' + Convert(NVARCHAR(10),MSM.GARANTIBITIS, 104) END AS GARANTI, " +
                " KULLANICI.ISIM + ' ' +CONVERT(NVARCHAR(10),MSM.KAYIT_TARIHI,104) AS KAYIT FROM MALZEME_SEVK_MALZEMELER AS MSM  LEFT JOIN MALZEME_DURUM AS MD ON MD.ID=MSM.DURUM" +
                " INNER JOIN MALZEMELER         AS M   ON M.ID = MSM.MALZEME_ID  INNER JOIN MALZEME_MARKAMODELMODEL AS MM  ON M.MARKA=MM.ID " +
                " INNER JOIN MALZEME_MODEL      AS MMO ON M.MODEL=MMO.ID LEFT JOIN KULLANICI                  ON KULLANICI.ID = MSM.KAYIT_EDEN " +
                " WHERE MSM.SEVK_ID= '" + sevkid + "' ";
            DataTable dt = new DataTable();
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conn;
            conn.Open();
            cmd.CommandText = sql;
            SqlDataReader dr = cmd.ExecuteReader();
            dt.Load(dr);
            conn.Close();
            return dt;
        }
               
        public int ConvertToIntParse(string intString, int def)
        {
            int i = 0;
            if (!Int32.TryParse(intString, out i))
            {
                i = def;
            }
            return i;
        }
               
        public string TextSifrele(string sifre)
        {
            byte[] data = UTF8Encoding.UTF8.GetBytes(sifre);
            using (MD5CryptoServiceProvider md5 = new MD5CryptoServiceProvider())
            {
                byte[] keys = md5.ComputeHash(UTF8Encoding.UTF8.GetBytes(hash));
                using (TripleDESCryptoServiceProvider tripDes = new TripleDESCryptoServiceProvider() { Key = keys, Mode = CipherMode.ECB, Padding = PaddingMode.PKCS7 })
                {
                    ICryptoTransform transform = tripDes.CreateEncryptor();
                    byte[] results = transform.TransformFinalBlock(data, 0, data.Length);
                    return Convert.ToBase64String(results, 0, results.Length);
                }
            }
        }

        public string TextSifreCoz(string SifrelenmisDeger)
        {
            byte[] data = Convert.FromBase64String(SifrelenmisDeger);
            using (MD5CryptoServiceProvider md5 = new MD5CryptoServiceProvider())
            {
                byte[] keys = md5.ComputeHash(UTF8Encoding.UTF8.GetBytes(hash));
                using (TripleDESCryptoServiceProvider tripDes = new TripleDESCryptoServiceProvider() { Key = keys, Mode = CipherMode.ECB, Padding = PaddingMode.PKCS7 })
                {
                    ICryptoTransform transform = tripDes.CreateDecryptor();
                    byte[] results = transform.TransformFinalBlock(data, 0, data.Length);
                    return UTF8Encoding.UTF8.GetString(results);
                }
            }
        }




        public string eposta_sablon_baslik()
        {
            string baslik = "";

            //baslik = "<html><head>   <meta http-equiv='Content-Type' content='text/html; charset=utf-8'>   <title>"+ayar_al("PROGRAM-ADI")+" Duyuru</title>   <style type='text/css'>a {color: #4A72AF;} body, #header h1, #header h2, p {margin: 0; padding: 0;} #main { box-shadow: 0px 0px 10px 4px #000000; border: 1px solid #cfcece;} #tablo {border: 1px solid #cfcece; color: #3f4042; font-size: 12px; font-family: Arial, Helvetica, sans-serif;}img {display: block;}#top-message p, #bottom-message p {color: #3f4042; font-size: 12px; font-family: Arial, Helvetica, sans-serif; }#header h1 {color: #ffffff !important; font-family: 'Lucida Grande', 'Lucida Sans', 'Lucida Sans Unicode', sans-serif; font-size: 24px; margin-bottom: 0!important; padding-bottom: 0; }#header h2 {color: #ffffff !important; font-family: Arial, Helvetica, sans-serif; font-size: 24px; margin-bottom: 0 !important; padding-bottom: 0; }#header p {color: #ffffff !important; font-family: 'Lucida Grande', 'Lucida Sans', 'Lucida Sans Unicode', sans-serif; font-size: 12px;  }h1, h2, h3, h4, h5, h6 {margin: 0 0 0.8em 0;}h3 {font-size: 28px; color: #444444 !important; font-family: Arial, Helvetica, sans-serif; }h4 {font-size: 22px; color: #4A72AF !important; font-family: Arial, Helvetica, sans-serif; }h5 {font-size: 18px; color: #444444 !important; font-family: Arial, Helvetica, sans-serif; }p {font-size: 12px; color: #444444 !important; font-family: 'Lucida Grande', 'Lucida Sans', 'Lucida Sans Unicode', sans-serif; line-height: 1.5;}   </style></head> ";
            //baslik += "<body> <table width='100%' cellpadding='0' cellspacing='0' bgcolor='e4e4e4'><tr><td> <table id='top-message' cellpadding='20' cellspacing='0' width='600' align='center'><tr><td align='center'></td></tr></table><!-- top message --><table id='main' width='600' align='center' cellpadding='0' cellspacing='15' bgcolor='ffffff'><tr><td><table id='header' cellpadding='10' cellspacing='0' align='center' bgcolor='8fb3e9'><tr><td width='570' bgcolor='7aa7e9'><h1>Türksat Özlük Hakları Takip Programı</h1></td></tr><tr><td width='570' bgcolor='8fb3e9' style='background: url(https://firmapersonel.turksat.com.tr/images/email-bg.jpg);'><h2 style='color:#ffffff!important'>Duyuru</h2></td></tr><tr><td width='570' align='right' bgcolor='7aa7e9'><p>" + DateTime.Today.ToShortDateString() + "</p></td></tr></table><!-- header --></td></tr><!-- header -->";
            //baslik += "<tr><td></td></tr><tr><td></td></tr><!-- content 1 --><tr><td>	<table id='content-2' cellpadding='0' cellspacing='0' align='center'><tr><td width='570'><p>";



            return baslik;
        }

        public string eposta_sablon_orta()
        {
            string orta = "";

            orta = "</p></td></tr></table><!-- content-2 --></td></tr><!-- content-2 --><tr><td></td></tr><!-- content-4 --><tr></tr><tr><td><table id='content-6' cellpadding='0' cellspacing='0' align='center'><p align='center'>";

            return orta;
        }

        public string eposta_sablon_son()
        {
            string son = "";

            son = "</p></table></td></tr></td></tr></table><!-- wrapper --></body></html>";

            return son;
        }

        public string eposta_kaydet(string raporolustuan, string email, string konu, string metin)
        {
            SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["KabloHE"].ConnectionString);

            try
            {
                metin = metin.Replace("'", "''");
                conn.Open();
                SqlCommand sorgu = new SqlCommand();
                sorgu.Connection = conn;
                //sorgu.CommandText = " INSERT INTO ";
                int kayıt_eden = 0;
                if (HttpContext.Current.Session["KULLANICI_ID"] == null)
                    kayıt_eden = 0;
                else
                    kayıt_eden = Convert.ToInt16(HttpContext.Current.Session["KULLANICI_ID"].ToString());
                sorgu.CommandText = "insert INTO MAIL(KIME,KIMDEN,KONU,ICERIK,DURUM,KAYIT_TARIHI,KAYIT_EDEN)  VALUES(@email,@raporolusturan,@konu,@metin,'false',convert(varchar, getdate(), 13),'" + kayıt_eden + "') ";
                sorgu.Parameters.AddWithValue("email", email);
                sorgu.Parameters.AddWithValue("raporolusturan", raporolustuan);
                sorgu.Parameters.AddWithValue("konu", konu);
                sorgu.Parameters.AddWithValue("metin", metin);
                sorgu.ExecuteNonQuery();

                //string yetki = Convert.ToString(sorgu.ExecuteScalar());
                conn.Close();


                return "ok";
            }
            catch (Exception hataTuru)
            {
                return hataTuru.Message;
            }
            finally
            {
                if (conn.State == ConnectionState.Open)
                    conn.Close();
            }

        }

        string sendemail_turksat(string raporolustuan, string email, string konu, string metin)
        {
            try
            {
                MailMessage mm = new MailMessage();
                mm.Subject = konu;

                metin = metin.Replace("[SABLONBASLIK]", sql_calistir1("SELECT DEGER FROM AYARLAR WHERE TIP='EPOSTA-SABLON-BASLIK'  "));
                //metin = metin.Replace("[SABLONORTA]", sql_calistir("SELECT DEGER FROM AYARLAR WHERE TIP='EPOSTA-SABLON-BASLIK'  "));
                metin = metin.Replace("[SABLONSON]", sql_calistir1("SELECT DEGER FROM AYARLAR WHERE TIP='EPOSTA-SABLON-SON'  "));



                string icerik = "";
                icerik += metin;
                icerik += "  <br/> <br/> <div class=\"row margin-body\"> <a href='www.turksat.com.tr'><img src='" + ayar_al("PROGRAM-ADRESI") + "/Images/logo_tr_k2.png'></a> ";
                icerik += "<br> <h6>" + ayar_al("PROGRAM-ADI") + "<br>Kullanıcı " + raporolustuan + " - Eposta gönderme zamanı : " + DateTime.Now.ToString() + " </h6><br></div> </body></html>";

                icerik += eposta_sablon_son();

                mm.Body = @icerik;
                mm.IsBodyHtml = true;
                mm.To.Add(email);


                mm.From = new MailAddress("kasa@turksat.com.tr", "" + ayar_al("PROGRAM-ADI") + "");

                /*
                SmtpClient smtp = new SmtpClient();
                smtp.Host = "10.101.30.135";
                smtp.EnableSsl = false;
                System.Net.NetworkCredential NetworkCred = new System.Net.NetworkCredential();
                NetworkCred.UserName = "no-reply@hasansavun.com.tr";
                NetworkCred.Password = "h2431079";
                smtp.UseDefaultCredentials = true;
                smtp.Credentials = NetworkCred;
                smtp.Port = 587;
                */
                SmtpClient smtp = new SmtpClient("10.101.30.135", 25);
                smtp.Send(mm);

                return "ok";
            }
            catch (Exception hataTuru)
            {
                return hataTuru.Message;
            }
        }

        string sendemail_hasan(string raporolustuan, string email, string konu, string metin)
        {
            string icerik = "";
            try
            {
                MailMessage mm = new MailMessage();
                mm.Subject = konu;

                metin = metin.Replace("[SABLONBASLIK]", sql_calistir1("SELECT DEGER FROM AYARLAR WHERE TIP='EPOSTA-SABLON-BASLIK'  "));
                //metin = metin.Replace("[SABLONORTA]", sql_calistir("SELECT DEGER FROM AYARLAR WHERE TIP='EPOSTA-SABLON-BASLIK'  "));
                metin = metin.Replace("[SABLONSON]", sql_calistir1("SELECT DEGER FROM AYARLAR WHERE TIP='EPOSTA-SABLON-SON'  "));

                icerik += metin;
                icerik += "<br/> <br/> <a href='www.turksat.com.tr'><img src='" + ayar_al("PROGRAM-ADRESI") + "/Images/logo_tr.png'></a> ";
                icerik += "<br> <h6>Hasan SAVUN - Enes ÖZCAN -  2019 © Balıkesir<br>Mail Oluşturan " + raporolustuan + " - Mail oluşturma zamanı : " + DateTime.Now.ToString() + " </h6><br></body></html>";

                icerik += eposta_sablon_son();

                mm.Body = @icerik;
                mm.IsBodyHtml = true;
                mm.To.Add(email);

                SmtpClient smtp = new SmtpClient();
                mm.From = new MailAddress("no-reply@hasansavun.com.tr", "" + ayar_al("PROGRAM-ADI") + " - Duyuru");

                smtp.Host = "hasansavun.com.tr";
                smtp.EnableSsl = false;
                System.Net.NetworkCredential NetworkCred = new System.Net.NetworkCredential();
                NetworkCred.UserName = "no-reply@hasansavun.com.tr";
                NetworkCred.Password = "h2431079";
                smtp.UseDefaultCredentials = true;
                smtp.Credentials = NetworkCred;
                smtp.Port = 587;
                smtp.Send(mm);

                return "ok";
            }
            catch (Exception hataTuru)
            {
                return hataTuru.Message;
            }

        }

        string sendemail_hasan_custom(string raporolustuan, string email, string konu, string metin)
        {
            string icerik = "";
            try
            {
                string sunucu, userid, pass, port;
                sunucu = ayar_al("POSTA-SUNUCU");
                userid = ayar_al("POSTA-KULLANICI-ADI");
                pass = ayar_al("POSTA-SIFRE");
                port = ayar_al("POSTA-PORT");
                string programadresi = ayar_al("PROGRAM-ADRESI");
                string programadi = ayar_al("PROGRAM-ADI");

                MailMessage mm = new MailMessage();
                mm.Subject = konu;

                metin = metin.Replace("[SABLONBASLIK]", sql_calistir1("SELECT DEGER FROM AYARLAR WHERE TIP='EPOSTA-SABLON-BASLIK'  "));
                //metin = metin.Replace("[SABLONORTA]", sql_calistir("SELECT DEGER FROM AYARLAR WHERE TIP='EPOSTA-SABLON-BASLIK'  "));
                metin = metin.Replace("[SABLONSON]", sql_calistir1("SELECT DEGER FROM AYARLAR WHERE TIP='EPOSTA-SABLON-SON'  "));


                icerik += metin;
                icerik += "<br/> <br/> <a href='www.turksat.com.tr'><img src='" + programadresi + "/Images/logo_tr.png'></a> ";
                icerik += "<br> <h6>Hasan SAVUN - Enes ÖZCAN -  2019 © Balıkesir<br>Mail Oluşturan " + raporolustuan + " - Mail oluşturma zamanı : " + DateTime.Now.ToString() + " </h6><br></body></html>";

                icerik += eposta_sablon_son();

                mm.Body = @icerik;
                mm.IsBodyHtml = true;
                mm.To.Add(email);

                SmtpClient smtp = new SmtpClient();
                smtp.Host = sunucu;
                mm.From = new MailAddress(userid, programadi);
                smtp.EnableSsl = false;
                System.Net.NetworkCredential NetworkCred = new System.Net.NetworkCredential();
                NetworkCred.UserName = userid;
                NetworkCred.Password = pass;
                smtp.UseDefaultCredentials = true;
                smtp.Credentials = NetworkCred;
                smtp.Port = ConvertToIntParse(port, 537);
                smtp.Send(mm);

                return "ok";


            }
            catch (Exception hataTuru)
            {
                return hataTuru.Message;
            }

        }

        public string eposta_gonder(string raporolustuan1, string email1, string konu1, string metin1)
        {

            SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["KabloHE"].ConnectionString);

            conn.Open();
            SqlCommand sorgu = new SqlCommand();
            sorgu.Connection = conn;
            sorgu.CommandText = "select DEGER FROM AYARLAR WHERE TIP='EPOSTAAYAR' ";
            string eposta = Convert.ToString(sorgu.ExecuteScalar());
            conn.Close();

            if (eposta == "hasan")
                return sendemail_hasan(raporolustuan1, email1, konu1, metin1);
            else if (eposta == "turksat")
                return sendemail_turksat(raporolustuan1, email1, konu1, metin1);
            else if (eposta == "custom")
                return sendemail_hasan_custom(raporolustuan1, email1, konu1, metin1);
            else
                return "ayar bulunamadi";

        }


    }
}