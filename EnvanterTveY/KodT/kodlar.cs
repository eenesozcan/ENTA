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
            return System.Web.HttpContext.Current.Session["KULLANICI_ROL"].ToString();
        }

        public string Kul_ID()
        {

            return System.Web.HttpContext.Current.Session["KULLANICI_ID"].ToString();
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
    }
}