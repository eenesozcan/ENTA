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
    public partial class RuhsatTalep : System.Web.UI.Page
    {
        SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["KabloHE"].ConnectionString);
        string sayfa = "RuhsatTalep";
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
                string tip=Request.QueryString["t"];
                if (tip == "kesif")
                    comdurumara.SelectedIndex = 1;
                else if  (tip=="kabul")
                    comdurumara.SelectedIndex = 2;
                else if (tip == "tamam")
                    comdurumara.SelectedIndex = 3;
                else
                    comdurumara.SelectedIndex = 0;


                Bolge_Ara_Listele();
                Durum_Ara_Listele();
                listele_ruhsat();
                
            }
        }

        public void js_calistir(string str)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), System.Guid.NewGuid().ToString(), str, true);
        }

        void mesaj_yaz(string hata, string mesaj, string yer)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), System.Guid.NewGuid().ToString(), "ShowMessage('" + mesaj + "','" + hata + "','" + yer + "');", true);
        }

        void mesaj_yaz_yeni(string hata, string mesaj)
        {
            js_calistir("YeniMesaj('" + mesaj + "','" + hata + "');");
        }

        void modal_ac(string modal_isim)
        {
            js_calistir("ModalAc('" + modal_isim + "');");
        }

        void modal_kapat(string modal_isim)
        {
            js_calistir("ModalKapat('" + modal_isim + "');");
        }

        void Bolge_Ara_Listele()
        {
            combolgeara.Items.Clear();
            combolgeara.Items.Insert(0, new ListItem("Bölge Seçiniz", "0"));
            combolgeara.AppendDataBoundItems = true;

            combolgeara.DataSource = tkod.dt_bolge_Liste();
            combolgeara.DataBind();
        }

        void Durum_Ara_Listele()
        {
            comdurumara.Items.Clear();
            comdurumara.Items.Insert(0, new ListItem("Seçiniz", "0"));
            comdurumara.AppendDataBoundItems = true;

            SqlDataAdapter da = new SqlDataAdapter();
            SqlCommand cmd = new SqlCommand("Select DEGER AS ID,SECENEK FROM COMDOLDUR WHERE COM_ADI = 'ruhsat-durum-ara'  ORDER BY SIRA  ");
            DataTable dt = new DataTable();
            da.SelectCommand = cmd;
            da.SelectCommand.Connection = conn;
            da.Fill(dt);
            comdurumara.DataSource = dt;
            comdurumara.DataTextField = "SECENEK";
            comdurumara.DataValueField = "ID";
            comdurumara.DataBind();
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
            if (lblidcihaz.Text == "")
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
            else
            {
                comilce_kayit.Items.Clear();
                comilce_kayit.Items.Insert(0, new ListItem("İlçe seçiniz", "0"));
                comilce_kayit.AppendDataBoundItems = true;
                string sql2 = "";


                    sql2 = " AND BOLGE_ID='" + combolge_kayit.SelectedValue + "' ";

                string sql = "Select ID,ILCE FROM RUHSAT_ADRES_ILCE WHERE ID>0 " + sql2 + " ORDER BY ID";
                comilce_kayit.DataSource = tkod.GetData(sql);
                comilce_kayit.DataBind();
            }
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
            if (lblidcihaz.Text == "")
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
            else
            {
                comcdsk_kayit.Items.Clear();
                comcdsk_kayit.Items.Insert(0, new ListItem("Cadde/Sokak Seçiniz", "0"));
                comcdsk_kayit.AppendDataBoundItems = true;
                string sql2 = "";

                    sql2 = " AND MAHALLE_ID='" + commahalle_kayit.SelectedValue + "' ";

                string sql = "Select ID,CADDESOKAK FROM RUHSAT_ADRES_CADDESOKAK WHERE ID>0 " + sql2 + " ORDER BY ID";
                comcdsk_kayit.DataSource = tkod.GetData(sql);
                comcdsk_kayit.DataBind();
            }
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


            if (grid_metraj.Rows.Count>0)
            {
                SqlCommand sorgu = new SqlCommand();
                sorgu.Connection = conn;
                conn.Open();

                sorgu.CommandText = "SELECT KAZI_TURU_ID FROM RUHSAT_KAZI_BILGI WHERE RUHSAT_ID=@ID_CIHAZ ";
                sorgu.Parameters.AddWithValue("@ID_CIHAZ", lblidcihaz.Text);
                int kazi_turu_id_bul = Convert.ToInt16(sorgu.ExecuteScalar());

                comkazituru.Items.Clear();
                comkazituru.Items.Insert(0, new ListItem("Kazı Türü Seçiniz", "0"));
                comkazituru.AppendDataBoundItems = true;
                string sql2 = "";

                if (lbl_ruhsat_yetkilisi.Text != "")
                    sql2 = " AND RUHSAT_VEREN_ID='" + lbl_ruhsat_yetkilisi.Text + "' ";

                string sql = "Select ID,KAZI_TURU FROM RUHSAT_KAZI_TURU WHERE ID=" + kazi_turu_id_bul + " " + sql2 + "  ORDER BY ID";
                comkazituru.DataSource = tkod.GetData(sql);
                comkazituru.DataBind();
                conn.Close();

            }
            else
            {
                comkazituru.Items.Clear();
                comkazituru.Items.Insert(0, new ListItem("Kazı Türü Seçiniz", "0"));
                comkazituru.AppendDataBoundItems = true;
                string sql2 = "";

                if (lbl_ruhsat_yetkilisi.Text != "")
                    sql2 = " AND RUHSAT_VEREN_ID='" + lbl_ruhsat_yetkilisi.Text + "' ";

                string sql = "Select ID,KAZI_TURU FROM RUHSAT_KAZI_TURU WHERE ID>0 " + sql2 + "  ORDER BY ID";
                comkazituru.DataSource = tkod.GetData(sql);
                comkazituru.DataBind();
            }



        }
        void ruhsat_kazi_tipi_sec()
        {
            comkazitipi_sec.Items.Clear();
            comkazitipi_sec.Items.Insert(0, new ListItem("Kazı Tipi Seçiniz", "0"));
            comkazitipi_sec.AppendDataBoundItems = true;
            string sql2 = "";

            if (lbl_aykome_kazi_turu.Text != "")
                sql2 = " AND KAZI_TURU_ID='" + lbl_aykome_kazi_turu.Text + "' ";

            string sql = "Select ID,KAZI_TIPI FROM RUHSAT_KAZI_TIPI WHERE ID>0 " + sql2 + "  ORDER BY ID";
            comkazitipi_sec.DataSource = tkod.GetData(sql);
            comkazitipi_sec.DataBind();
        }

        void ruhsat_veren_sec()
        {
            if (lblidcihaz.Text=="")
            {
                comruhsatveren.Items.Clear();
                comruhsatveren.Items.Insert(0, new ListItem("Ruhsat Veren Kurum Seç", "0"));
                comruhsatveren.AppendDataBoundItems = true;
                string sql2 = "";

                if (comil_kayit.SelectedIndex > 0)
                    sql2 = " AND IL_ID='" + comil_kayit.SelectedValue.ToString() + "' ";

                string sql = "Select ID,RUHSAT_VEREN FROM RUHSAT_RUHSAT_VEREN WHERE ID>0 " + sql2 + " ORDER BY ID";
                comruhsatveren.DataSource = tkod.GetData(sql);
                comruhsatveren.DataBind();
            }
            else
            {
                comruhsatveren.Items.Clear();
                comruhsatveren.Items.Insert(0, new ListItem("Ruhsat Veren Kurum Seç", "0"));
                comruhsatveren.AppendDataBoundItems = true;
                string sql2 = "";


                    sql2 = " AND IL_ID='" + comil_kayit.SelectedValue + "' ";

                string sql = "Select ID,RUHSAT_VEREN FROM RUHSAT_RUHSAT_VEREN WHERE ID>0 " + sql2 + " ORDER BY ID";
                comruhsatveren.DataSource = tkod.GetData(sql);
                comruhsatveren.DataBind();
            }

        }

        void ruhsat_donem_sec()
        {
            if (lblidcihaz.Text == "")
            {
                comdonem.Items.Clear();
                comdonem.Items.Insert(0, new ListItem("Dönem Seç", "0"));
                comdonem.AppendDataBoundItems = true;
                string sql2 = "";

                if (comruhsatveren.SelectedIndex > 0)
                    sql2 = " AND RUHSAT_VEREN_ID='" + comruhsatveren.SelectedValue.ToString() + "' ";

                string sql = "Select ID,DONEM FROM RUHSAT_DONEM WHERE ID>0 " + sql2 + " ORDER BY ID";
                comdonem.DataSource = tkod.GetData(sql);
                comdonem.DataBind();
            }
            else
            {
                comdonem.Items.Clear();
                comdonem.Items.Insert(0, new ListItem("Dönem Seç", "0"));
                comdonem.AppendDataBoundItems = true;
                string sql2 = "";

                    sql2 = " AND RUHSAT_VEREN_ID='" + comruhsatveren.SelectedValue + "' ";

                string sql = "Select ID,DONEM FROM RUHSAT_DONEM WHERE ID>0 " + sql2 + " ORDER BY ID";
                comdonem.DataSource = tkod.GetData(sql);
                comdonem.DataBind();
            }
        }

        void listele_ruhsat()
        {

            SqlCommand cmd;
            string sql = " ", sql1 = " ";


            sql = "SELECT V.ID, V.RUHSATNO, V.PROJENO, IL.IL, BOLGE.BOLGE_ADI, RAI.ILCE, RAM.MAHALLE, RACS.CADDESOKAK, V.ADRES_DETAY, RTPT.T_PROJE_TIPI, RAPT.A_PROJE_TIPI, V.BASLANGIC_TARIHI, V.BITIS_TARIHI, " +
                " V.DURUM," +
                //" CASE WHEN V.DURUM, 0)=1 THEN 'Aktif' ELSE 'Pasif' END AS DURUM, " +

                //çalışıyor " CASE WHEN  RO.TEMINAT_ID<>0 THEN  '<span class=\"text-black bg-warning rounded  p-1\">Teminat ID= ' + CONVERT(NVARCHAR,RO.TEMINAT_ID) + ' Teminat Tutarı= '  +  CONVERT(NVARCHAR,RO.TEMINAT_TOPLAM) + ' </span>'	ELSE '<span class=\"text-white bg-danger rounded  p-1\"> Nakit= ' +  CONVERT(NVARCHAR,RO.NAKIT_TOPLAM) + ' </span>' END AS TEMINAT  " +
                //çalışıyor " CASE WHEN  V.DURUM='Keşif' THEN  '<span class=\"text-black bg-info rounded  p-1\"> ' + V.DURUM +' </span>' WHEN  V.DURUM='Kabul' THEN  '<span class=\"text-black bg-primary rounded  p-1\"> ' + V.DURUM +' </span>'	ELSE '<span class=\"text-white bg-success rounded  p-1\"> ' + V.DURUM +'  </span>' END AS DURUM ," +
                " CASE WHEN  RO.TEMINAT_ID<>0 THEN  '<span class=\"text-black bg-warning rounded  p-1\"> '  +  CONVERT(NVARCHAR,RO.TEMINAT_TOPLAM) + ' </span>'	ELSE '<span class=\"text-white bg-danger rounded  p-1\"> ' +  CONVERT(NVARCHAR,RO.NAKIT_TOPLAM) + ' </span>' END AS TEMINAT  " +

                "  FROM RUHSAT_RUHSATLAR AS V " +
                " LEFT JOIN IL ON IL.ID=V.IL_ID" +
                " LEFT JOIN BOLGE ON BOLGE.ID=V.BOLGE_ID " +

                " LEFT JOIN RUHSAT_ADRES_ILCE AS RAI ON RAI.ID=V.ILCE_ID " +
                " LEFT JOIN RUHSAT_ADRES_MAHALLE AS RAM ON RAM.ID=V.MAHALLE_ID " +
                " LEFT JOIN RUHSAT_ADRES_CADDESOKAK AS RACS ON RACS.ID=V.CADDESOKAK_ID " +
                " LEFT JOIN RUHSAT_TURKSAT_PROJE_TIPI  AS RTPT ON RTPT.ID=V.T_PROJETIP_ID " +
                " LEFT JOIN RUHSAT_AYKOME_PROJE_TIPI  AS RAPT ON RAPT.ID=V.A_PROJETIP_ID " +
                " LEFT JOIN RUHSAT_ODEMELER  AS RO ON RO.RUHSAT_ID=V.ID " +

                "  ";

            if (combolgeara.SelectedIndex > 0)
                sql1 += " AND (V.BOLGE_ID='" + combolgeara.SelectedValue + "' )  ";

            if (comdurumara.SelectedIndex > 0)
                sql1 += " AND (V.DURUM='" + comdurumara.SelectedItem + "' )  ";

            //sql = sql + tkod.yetki_tablosu() + "  WHERE V.ID > 0  " + sql1 + tkod.yetki_tablosu_2() + " and KESIFMI='True' and KABUL_YAPILDIMI!='True'  ORDER BY V.ID DESC";
            sql = sql + tkod.yetki_tablosu() + "  WHERE V.ID > 0  " + sql1 + tkod.yetki_tablosu_2() + " and GOREV_TAMAMMI='False' ORDER BY V.ID DESC";

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


                sql = "SELECT RKB.ID,RKB.UZUNLUK, RKB.GENISLIK,RKB.DERINLIK, RKTURU.KAZI_TURU, RKTIPI.KAZI_TIPI  FROM RUHSAT_KAZI_BILGI AS RKB " +
                    " INNER JOIN RUHSAT_KAZI_TURU AS RKTURU ON RKTURU.ID = RKB.KAZI_TURU_ID " +
                    " INNER JOIN RUHSAT_KAZI_TIPI AS RKTIPI ON RKTIPI.ID = RKB.KAZI_TIPI_ID " +
                    " WHERE RKB.ID > 0 AND RKB.RUHSAT_ID= " + lblidcihaz.Text + " ";

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
            //tab_ac();
        }

        void teminatverilenyer_sec()
        {

            string sql2 = tkod.sql_calistir_param("SELECT RUHSAT_VEREN_ID FROM RUHSAT_RUHSATLAR  WHERE ID=@ID", new SqlParameter("ID", lblidcihaz.Text));

            com_teminat_sec.Items.Clear();
            com_teminat_sec.Items.Insert(0, new ListItem("Teminat Seçiniz", "0"));
            com_teminat_sec.AppendDataBoundItems = true;


            string sql = "Select ID,TARIH  as AA FROM RUHSAT_TEMINATLAR WHERE ID>0 AND RUHSAT_VEREN_ID=" + sql2 + " ORDER BY ID";
            com_teminat_sec.DataSource = tkod.GetData(sql);
            com_teminat_sec.DataBind();
        }

        void tab_ac()
        {
            listele_metraj();
            if (grid_metraj.Rows.Count>0 && txtruhsatno_kayit.Text.Length > 1) // aslında 0'dan büyük yapılacak ancak boş iken görüntüleye basınca textbox^'ın içi bir kararkter boş geliyor bu nedenle dolu gibi görünüyor. bu sorunu çöz.
            {
                TabPanel3.Visible = true;
                TabPanel4.Visible = true;
                TabPanel5.Visible = true;
                //TabPanel6.Visible = true;
            }
            else
            {
                TabPanel3.Visible = false;
                TabPanel4.Visible = false;
                TabPanel5.Visible = false;
                //TabPanel6.Visible = false;
            }


        }

        /*
        void chc_Ruhsat_durum_kontrol()
        {
            if (chcaktif_pasif.Checked == true)
            {
                lbl_ruhsat_aktif_pasif.Text = "Aktif";
            }
            else
            {
                lbl_ruhsat_aktif_pasif.Text = "Pasif";
            }
        }
        */
        void tab_kontrol()
        {
            if (lblidcihaz.Text != "")
            {
                TabPanel1.Visible = true;
                TabPanel2.Visible = true;
            }
        }

        public void metraj_txt_kontrol()
        {
            conn.Close();

            int il_id1 = Convert.ToInt16(lbl_il.Text);
            int ruhsat_yetkilisi1 = Convert.ToInt16(lbl_ruhsat_yetkilisi.Text);
            int donem1 = Convert.ToInt16(lbl_donem.Text);
            string metraj_id = lblidmetraj.Text;

            
            //string kazi_turu1 = tkod.sql_calistir_param("SELECT KAZI_TURU_ID FROM RUHSAT_KAZI_BILGI  WHERE ID=@ID335", new SqlParameter("ID335", metraj_id));
            //string kazi_tipi1 = tkod.sql_calistir_param("SELECT KAZI_TIPI_ID FROM RUHSAT_KAZI_BILGI  WHERE ID=@ID340", new SqlParameter("ID340", metraj_id));
            string kazi_turu1 = comkazituru.SelectedValue;
            string kazi_tipi1 = comkazitipi_sec.SelectedValue;

            if (comkazitipi_sec.SelectedIndex ==0)
            {
                txtkaziuzunluk.Enabled = false;
                txtkazigenislik.Enabled = false;
                txtkaziderinlik.Enabled = false;
            }
            else
            {
                SqlCommand sorgu = new SqlCommand();
                sorgu.Connection = conn;
                conn.Open();

                sorgu.CommandText = " select BIRIM from RUHSAT_KAZI_BIRIMI where ID= (SELECT BIRIM_ID FROM RUHSAT_KAZI_TIPI WHERE IL_ID='" + il_id1 + "' and RUHSAT_VEREN_ID='" + ruhsat_yetkilisi1 + "' and DONEM_ID='" + donem1 + "' and KAZI_TURU_ID='" + kazi_turu1 + "' and ID='" + kazi_tipi1 + "' ) "; ;
                string birimmm = sorgu.ExecuteScalar().ToString();
                conn.Close();

                if (birimmm == "m2")
                {
                    txtkaziuzunluk.Enabled = true;
                    txtkazigenislik.Enabled = true;
                    txtkaziderinlik.Enabled = false;
                    txtkazigenislik.Text = "";
                    txtkaziderinlik.Text = "0";
                }
                if (birimmm == "m3")
                {
                    txtkaziuzunluk.Enabled = true;
                    txtkazigenislik.Enabled = true;
                    txtkaziderinlik.Enabled = true;
                    txtkaziderinlik.Text = "";

                }
                if (birimmm == "mt")
                {
                    txtkaziuzunluk.Enabled = true;
                    txtkazigenislik.Enabled = false;
                    txtkaziderinlik.Enabled = false;
                    txtkazigenislik.Text = "0";
                    txtkaziderinlik.Text = "0";
                }
            }
        }

        void deplase_ariza_mesaj()
        {
            if (comaprojetipi_kayit.SelectedItem.ToString() == "Arıza" || comaprojetipi_kayit.SelectedItem.ToString() == "Deplase")
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), System.Guid.NewGuid().ToString(), "YeniMesaj('Deplase ve Arıza kazılarında Keşif ve Kontrol Bedeli, Altyapı Ruhsat Bedeli ve Altyapı Kazı İzin Harcı ödenmez.','');", true);
                ScriptManager.RegisterStartupScript(this, this.GetType(), System.Guid.NewGuid().ToString(), "ShowMessage('Deplase ve Arıza kazılarında Keşif ve Kontrol Bedeli, Altyapı Ruhsat Bedeli ve Altyapı Kazı İzin Harcı ödenmez.','','yeni');", true);

            }
        }

        protected void btnara_Click(object sender, EventArgs e)
        {
            listele_ruhsat();
        }


        void bilgi_topla(string id)
        {
            lbl_il.Text = tkod.sql_calistir_param("SELECT IL_ID FROM RUHSAT_RUHSATLAR  WHERE ID=@ID34", new SqlParameter("ID34", id));
            lbl_kazi_turu.Text = tkod.sql_calistir_param("SELECT KAZI_TURU_ID FROM RUHSAT_KAZI_BILGI  WHERE RUHSAT_ID=@ID35", new SqlParameter("ID35", id));
            lbl_kazi_tipi.Text = tkod.sql_calistir_param("SELECT KAZI_TIPI_ID FROM RUHSAT_KAZI_BILGI  WHERE RUHSAT_ID=@ID36", new SqlParameter("ID36", id));

            lbl_ruhsat_yetkilisi.Text = tkod.sql_calistir_param("SELECT RUHSAT_VEREN_ID FROM RUHSAT_RUHSATLAR  WHERE ID=@ID31", new SqlParameter("ID31", id));
            lbl_donem.Text = tkod.sql_calistir_param("SELECT RUHSAT_DONEM_ID FROM RUHSAT_RUHSATLAR  WHERE ID=@ID32", new SqlParameter("ID32", id));
            lbl_aykome_kazi_turu.Text = tkod.sql_calistir_param("SELECT KAZI_TURU_ID FROM RUHSAT_KAZI_BILGI  WHERE RUHSAT_ID=@ID33", new SqlParameter("ID33", id));

        }

        void ruhsat_guncelleme_kontrol()
        {
            if (grid_metraj.Rows.Count != 0)
            {
                comil_kayit.Enabled = false;
                combolge_kayit.Enabled = false;
                comruhsatveren.Enabled = false;
                comdonem.Enabled = false;
            }      
        }

        protected void grid_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName.Equals("sil"))
            {
                int index = Convert.ToInt32(e.CommandArgument) % grid.PageSize;
                string id = grid.DataKeys[index].Value.ToString();
                lblruhsatidsil.Text = id;
                //lblruhsatidsil.Text = grid.DataKeys[index].Value.ToString();
                //int index = Convert.ToInt32(e.CommandArgument);
                //lblidsil.Text = grid.DataKeys[index].Value.ToString();
                lblislem_sil.Text = "ruhsat_kesif-sil";
                btnsil.Enabled = true;
                btnsil.CssClass = "btn btn-danger ";

                btnsil.Visible = true;
                btnkabulolustur.Visible = false;
                btntamamlandiolustur.Visible = false;
                               
                Panel_Sil.Visible = true;
                Panel_Kabul_Olustur.Visible = false;
                Panel_Tamamlandi_Olustur.Visible = false;

                System.Text.StringBuilder sb = new System.Text.StringBuilder();
                sb.Append(@"<script type='text/javascript'>");
                sb.Append("$(\"#ModalSil\").modal(\"show\");");
                sb.Append(@"</script>");
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(),
                           "ModalScript", sb.ToString(), false);
            }

            if (e.CommandName.Equals("goruntule"))
            {
                int index = Convert.ToInt32(e.CommandArgument) % grid.PageSize;
                string id = grid.DataKeys[index].Value.ToString();
                lblidcihaz.Text = id;

                bilgi_topla(id);


                txtkaziuzunluk.Enabled = false;
                txtkazigenislik.Enabled = false;
                txtkaziderinlik.Enabled = false;

                if (comkazituru.SelectedIndex > 0)
                    comkazitipi_sec.Enabled = true;
                else
                    comkazitipi_sec.Enabled = false;

                //TabPanel1.Visible = true;
                //TabPanel2.Visible = true;
                //TabPanel3.Visible = true;
                //TabPanel4.Visible = true;
                //TabPanel5.Visible = true;

                listele_metraj();
                listele_kmz();

                il_sec();
                comil_kayit.SelectedIndex = comil_kayit.Items.IndexOf(comil_kayit.Items.FindByText(HttpUtility.HtmlDecode(grid.Rows[index].Cells[4].Text)));

                bolge_sec();
                combolge_kayit.SelectedIndex = combolge_kayit.Items.IndexOf(combolge_kayit.Items.FindByText(HttpUtility.HtmlDecode(grid.Rows[index].Cells[5].Text)));


                ilce_sec();
                comilce_kayit.SelectedIndex = comilce_kayit.Items.IndexOf(comilce_kayit.Items.FindByText(HttpUtility.HtmlDecode(grid.Rows[index].Cells[6].Text)));


                mahalle_sec();
                commahalle_kayit.SelectedIndex = commahalle_kayit.Items.IndexOf(commahalle_kayit.Items.FindByText(HttpUtility.HtmlDecode(grid.Rows[index].Cells[7].Text)));

                caddesokak_sec();
                comcdsk_kayit.SelectedIndex = comcdsk_kayit.Items.IndexOf(comcdsk_kayit.Items.FindByText(HttpUtility.HtmlDecode(grid.Rows[index].Cells[8].Text)));

                turksat_proje_tip_sec();
                comtprojetipi_kayit.SelectedIndex = comtprojetipi_kayit.Items.IndexOf(comtprojetipi_kayit.Items.FindByText(HttpUtility.HtmlDecode(grid.Rows[index].Cells[10].Text)));

                aykome_proje_tip_sec();
                comaprojetipi_kayit.SelectedIndex = comaprojetipi_kayit.Items.IndexOf(comaprojetipi_kayit.Items.FindByText(HttpUtility.HtmlDecode(grid.Rows[index].Cells[11].Text)));



                ruhsat_kazi_turu_sec();
                //ruhsat_kazi_tipi_sec();

                teminatverilenyer_sec();

                txtruhsatno_kayit.Text = "";
                txtruhsatno_kayit.Text = HttpUtility.HtmlDecode(grid.Rows[index].Cells[1].Text.Trim());
                tab_ac();

                txtprojeno_kayit.Text = HttpUtility.HtmlDecode(grid.Rows[index].Cells[2].Text.Trim());

                txtadresdetay.Text = HttpUtility.HtmlDecode(grid.Rows[index].Cells[9].Text.Trim());

                ruhsat_veren_sec();
                comruhsatveren.SelectedValue = tkod.sql_calistir_param("SELECT ISNULL(RUHSAT_VEREN_ID,0) FROM RUHSAT_RUHSATLAR  WHERE ID=@ID22", new SqlParameter("ID22", id));

                ruhsat_donem_sec();
                comdonem.SelectedValue = tkod.sql_calistir_param("SELECT ISNULL(RUHSAT_DONEM_ID,0) FROM RUHSAT_RUHSATLAR  WHERE ID=@ID23", new SqlParameter("ID23", id));

                txtDate_baslangic.Text = tkod.sql_calistir_param("SELECT BASLANGIC_TARIHI FROM RUHSAT_RUHSATLAR  WHERE ID=@ID", new SqlParameter("ID", id));
                txtDate_bitis.Text = tkod.sql_calistir_param("SELECT BITIS_TARIHI FROM RUHSAT_RUHSATLAR  WHERE ID=@ID1", new SqlParameter("ID1", id));
                txtaciklama.Text = tkod.sql_calistir_param("SELECT ACIKLAMA FROM RUHSAT_RUHSATLAR  WHERE ID=@ID2", new SqlParameter("ID2", id));


                txt_alan_tahrip_bedeli.Text = tkod.sql_calistir_param("SELECT ALAN_TAHRIP_BEDELI FROM RUHSAT_ODEMELER  WHERE RUHSAT_ID=@ID3", new SqlParameter("ID3", id));
                txt_kesif_kontrol_bedeli.Text = tkod.sql_calistir_param("SELECT KESIF_KONTROL_BEDELI FROM RUHSAT_ODEMELER  WHERE RUHSAT_ID=@ID4", new SqlParameter("ID4", id));
                txt_altyapi_ruhsat_bedeli.Text = tkod.sql_calistir_param("SELECT ALTYAPI_RUHSAT_BEDELI FROM RUHSAT_ODEMELER  WHERE RUHSAT_ID=@ID5", new SqlParameter("ID5", id));
                txt_kazi_izin_harci.Text = tkod.sql_calistir_param("SELECT ALTYAPI_KAZI_IZIN_HARCI FROM RUHSAT_ODEMELER  WHERE RUHSAT_ID=@ID6", new SqlParameter("ID6", id));
                txt_kdv.Text = tkod.sql_calistir_param("SELECT KDV FROM RUHSAT_ODEMELER  WHERE RUHSAT_ID=@ID7", new SqlParameter("ID7", id));
                txt_toplam.Text = tkod.sql_calistir_param("SELECT NAKIT_TOPLAM FROM RUHSAT_ODEMELER  WHERE RUHSAT_ID=@ID8", new SqlParameter("ID8", id));
                txt_teminata_konu_tutar.Text = tkod.sql_calistir_param("SELECT TEMINAT_TOPLAM FROM RUHSAT_ODEMELER  WHERE RUHSAT_ID=@ID9", new SqlParameter("ID9", id));


                ruhsat_guncelleme_kontrol();




                string kontrol = tkod.sql_calistir_param("SELECT ODEME_TIPI FROM RUHSAT_ODEMELER  WHERE RUHSAT_ID=@ID12", new SqlParameter("ID12", id));
                if (kontrol =="Nakit")
                {
                    lbl_nakit_teminat.Text = "Nakit";
                    chc_nakit_teminat.Checked = false;
                }
                if (kontrol == "Teminat")
                {
                    lbl_nakit_teminat.Text = "Teminat";
                    chc_nakit_teminat.Checked = true;
                    com_teminat_sec.SelectedValue = tkod.sql_calistir_param("SELECT ISNULL(TEMINAT_ID,0) FROM RUHSAT_ODEMELER  WHERE RUHSAT_ID=@ID11", new SqlParameter("ID11", id));
                }
                teminat_bolum_ac_kapat();
                //if (com_teminat_sec.SelectedIndex > 0)
                //    chc_nakit_teminat.Enabled = false;
                //else
                //    chc_nakit_teminat.Enabled = true;



                tab_kontrol();

                //txtDate_baslangic.Text = HttpUtility.HtmlDecode(grid.Rows[index].Cells[11].Text.Trim());
                //txtDate_bitis.Text = HttpUtility.HtmlDecode(grid.Rows[index].Cells[12].Text.Trim());

                //int sayi = Convert.ToInt16(tkod.sql_calistir1("SELECT COUNT(SEVK_ID) FROM MALZEME_SEVK_MALZEMELER WHERE SEVK_ID=" + id));

                //chcaktif_pasif.Checked = Convert.ToBoolean(tkod.sql_calistir_param("SELECT DURUM FROM RUHSAT_RUHSATLAR WHERE ID=@ID98", new SqlParameter("ID98", id)));
                //chc_Ruhsat_durum_kontrol();

                Panel_Ruhsat_Olustur.Visible = true;
                Panel_Ruhsat_Bilgi.Visible = false;

                btnruhstkaydet.Enabled = true;
                btnruhstkaydet.CssClass = "btn btn-success";

                lblmodalyenibaslik.Text = "Ruhsat Güncelleme - " + "Ruhsat ID: " + id;

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
                lblruhsatid.Text = grid.DataKeys[index].Value.ToString();

                lblislem_ruhsatkabul.Text = "";
                lblislem_ruhsatkabul.Text = HttpUtility.HtmlDecode(grid.Rows[index].Cells[3].Text.Trim());

                if (lblislem_ruhsatkabul.Text=="Keşif")
                {
                    lblmodalyenibaslik2.Text = "Kabule Aktar";

                    btnsil.Visible = false;
                    btnkabulolustur.Visible = true;
                    btntamamlandiolustur.Visible = false;

                    Panel_Sil.Visible = false;
                    Panel_Kabul_Olustur.Visible = true;
                    Panel_Tamamlandi_Olustur.Visible = false;
                }
                if (lblislem_ruhsatkabul.Text == "Kabul")
                {
                    lblmodalyenibaslik2.Text = "Ruhsat Süreci Tamamlama";

                    btnsil.Visible = false;
                    btnkabulolustur.Visible = false;
                    btntamamlandiolustur.Visible = true;

                    Panel_Sil.Visible = false;
                    Panel_Kabul_Olustur.Visible = false;
                    Panel_Tamamlandi_Olustur.Visible = true;
                }

                lblmodalyenibaslik2.Text = "Kabule Aktar";

                System.Text.StringBuilder sb = new System.Text.StringBuilder();
                sb.Append(@"<script type='text/javascript'>");
                sb.Append("$(\"#ModalSil\").modal(\"show\");");
                sb.Append(@"</script>");
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(),
                           "ModalScript", sb.ToString(), false);
            }
            if (e.CommandName.Equals("kesifbilgi"))
            {
                int index = Convert.ToInt32(e.CommandArgument) % grid.PageSize;
                string id = grid.DataKeys[index].Value.ToString();

                string id1 = tkod.sql_calistir_param("SELECT KESIF_ID  FROM RUHSAT_RUHSATLAR  WHERE ID=@ID355", new SqlParameter("ID355", id));


                lblruhsatislem.Text = "kesifbilgi";

                Panel_Ruhsat_Olustur.Visible = false;
                Panel_Ruhsat_Bilgi.Visible = true;

                lblmodalyenibaslik.Text = "Ruhsat Keşif Bilgileri - " + "Ruhsat ID: " + id1;

                System.Text.StringBuilder sb = new System.Text.StringBuilder();
                sb.Append(@"<script type='text/javascript'>");
                sb.Append("$(\"#ModalRuhsatTalep\").modal(\"show\");");
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

        protected void grid_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowIndex > -1)
            {
                Button butonguncelle = (Button)e.Row.FindControl("goruntule");
                Button butonislemler = (Button)e.Row.FindControl("islemler");      //Kabul ve Tamamla işlemleri için buton
                Button butonkesifbilgi = (Button)e.Row.FindControl("kesifbilgi");
                Button butonsil = (Button)e.Row.FindControl("btnsil");

                try
                {
                    if (e.Row.Cells[3].Text.IndexOf("Kabul") > -1)
                        butonislemler.Text = "Tamamla";
                    if (e.Row.Cells[3].Text.IndexOf("Keşif") > -1)
                        butonislemler.Text = "Kabul";
                }
                catch (Exception ex)
                {
                    e.Row.Cells[3].ToolTip = "Hata oluştu : " + ex.Message;
                }

                try
                {
                    if (e.Row.Cells[3].Text.IndexOf("Kabul") > -1)
                    {
                        butonkesifbilgi.Visible = true;
                        butonsil.Visible = false;
                    }

                    else
                    {
                        butonkesifbilgi.Visible = false;
                    }
                }
                catch (Exception ex)
                {
                    e.Row.Cells[3].ToolTip = "Hata oluştu : " + ex.Message;
                }

                try
                {
                    if (e.Row.Cells[3].Text.IndexOf("Tamamlandı") > -1)
                    {
                        butonguncelle.Visible = false;
                        butonislemler.Visible = false;
                        butonsil.Visible = false;
                        butonkesifbilgi.Visible = true;
                    }


                }
                catch (Exception ex)
                {
                    e.Row.Cells[3].ToolTip = "Hata oluştu : " + ex.Message;
                }
            }



        }


        protected void comil_kayit_SelectedIndexChanged(object sender, EventArgs e)
        {
            bolge_sec();
            ruhsat_veren_sec();

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
                comruhsatveren.Enabled = true;
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

        protected void comruhsatveren_SelectedIndexChanged(object sender, EventArgs e)
        {
            lbl_ruhsat_yetkilisi.Text = comruhsatveren.SelectedValue;

            ruhsat_donem_sec();
            if (comruhsatveren.SelectedIndex == 0)
            {
                comdonem.Enabled = false;
            }
            else
            {
                comdonem.Enabled = true;
            }
        }
        protected void comdonem_SelectedIndexChanged(object sender, EventArgs e)
        {
            lbl_donem.Text = comdonem.SelectedValue;

            ruhsat_kazi_turu_sec();
            if (comruhsatveren.SelectedIndex == 0)
            {
                comkazituru.Enabled = false;
            }
            else
            {
                comkazituru.Enabled = true;
            }
        }
        protected void comkazituru_SelectedIndexChanged(object sender, EventArgs e)
        {
            lbl_aykome_kazi_turu.Text = comkazituru.SelectedValue;

            ruhsat_kazi_tipi_sec();
            if (comruhsatveren.SelectedIndex == 0)
                comkazitipi_sec.Enabled = false;
            else
                comkazitipi_sec.Enabled = true;


            if (comkazituru.SelectedIndex > 0)
                comkazitipi_sec.Enabled = true;
            else
                comkazitipi_sec.Enabled = false;
        }

        protected void comkazitipi_sec_SelectedIndexChanged(object sender, EventArgs e)
        {
            metraj_txt_kontrol();

        }
        protected void btnruhsatekle_Click(object sender, EventArgs e)
        {
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            sb.Append(@"<script type='text/javascript'>");
            sb.Append("$(\"#ModalRuhsatTalep\").modal(\"show\");");
            sb.Append(@"</script>");
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(),
                       "ModalScript", sb.ToString(), false);

            Panel_Ruhsat_Olustur.Visible = true;
            Panel_Ruhsat_Bilgi.Visible = false;


            lblmodalyenibaslik.Text = "Ruhsat Talep Kaydet";

            btnruhstkaydet.Enabled = true;

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
            listele_metraj();

            TabPanel1.Visible = true;
            TabPanel2.Visible = false;
            TabPanel3.Visible = false;
            TabPanel4.Visible = false;
            TabPanel5.Visible = false;
            //TabPanel6.Visible = false;

            comil_kayit.SelectedIndex = 0;
            comil_kayit.Enabled = true;

            if (combolge_kayit.SelectedIndex > 0) ;
            combolge_kayit.Items.Clear();

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
            //comkazituru.SelectedIndex = 0;
            //comkazitipi_sec.SelectedIndex = 0;

            combolge_kayit.Enabled = false;
            comilce_kayit.Enabled = false;
            commahalle_kayit.Enabled = false;
            comcdsk_kayit.Enabled = false;
            comdonem.Enabled = false;
        }

        //----------------------------------------//


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
                    sorgu.CommandText = "INSERT INTO    RUHSAT_RUHSATLAR (RUHSATNO, PROJENO, IL_ID, BOLGE_ID, ILCE_ID, MAHALLE_ID, CADDESOKAK_ID, ADRES_DETAY, T_PROJETIP_ID, A_PROJETIP_ID, BASLANGIC_TARIHI, BITIS_TARIHI, ACIKLAMA, DURUM, GOREV_TAMAMMI, KESIFMI, RUHSAT_VEREN_ID, RUHSAT_DONEM_ID, KAYIT_EDEN, KAYIT_TARIHI) " +
                        " VALUES(@RUHSATNO_K, @PROJENO_K, @IL_ID_K, @B_ID_K, @ILCE_ID_K, @MAHALLE_ID_K, @CDSK_ID_K, @ADRESDETAY_K, @TPROJETIP_ID_K, @APROJETIP_ID_K, @BASTARIH_K, @BITTARIH_K, @ACKLM_K, @DRM_K, @GOREV_TAMAMMI, @KESIFMI, @RUHSATVEREN_K, @RUHSATDONEM_K, @UI_K, getdate() ); SELECT @@IDENTITY AS 'Identity' ";
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
                    sorgu.Parameters.AddWithValue("@DRM_K", "Keşif");
                    sorgu.Parameters.AddWithValue("@GOREV_TAMAMMI", 0);
                    sorgu.Parameters.AddWithValue("@KESIFMI", 1);
                    sorgu.Parameters.AddWithValue("@RUHSATVEREN_K", comruhsatveren.SelectedValue);
                    sorgu.Parameters.AddWithValue("@RUHSATDONEM_K", comdonem.SelectedValue);
                    sorgu.Parameters.AddWithValue("@UI_K", userid);
                    string id = sorgu.ExecuteScalar().ToString();
                    lblidcihaz.Text = id;

                    ScriptManager.RegisterStartupScript(this, this.GetType(), System.Guid.NewGuid().ToString(), "YeniMesaj('Ruhsat başarıyla kaydedilmiştir.','Tamam');", true);
                    ScriptManager.RegisterStartupScript(this, this.GetType(), System.Guid.NewGuid().ToString(), "ShowMessage('Ruhsat başarıyla kaydedilmiştir.','Tamam','yeni');", true);

                    bilgi_topla(id);

                    btnruhstkaydet.Enabled = false;
                    btnruhstkaydet.CssClass = "btn btn-success disabled";
                }
                else
                {
                    // ------   UPDATE   ------ \\
                    sorgu.CommandText = "UPDATE RUHSAT_RUHSATLAR SET RUHSATNO=@RUHSATNO_U, PROJENO=@PROJENO_U, IL_ID=@IL_ID_U, BOLGE_ID=@B_ID_U, ILCE_ID=@ILCE_ID_U, " +
                        " MAHALLE_ID=@MAHALLE_ID_U, CADDESOKAK_ID=@CDSK_ID_U, ADRES_DETAY=@ADRESDETAY_U, T_PROJETIP_ID=@TPROJETIP_ID_U, A_PROJETIP_ID=@APROJETIP_ID_U, " +
                        " BASLANGIC_TARIHI=@BASTARIH_U, BITIS_TARIHI=@BITTARIH_U, ACIKLAMA=@ACKLM_U, RUHSAT_VEREN_ID=@RUHSATVEREN_U, RUHSAT_DONEM_ID=@RUHSATDONEM_U  " + 
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
                    sorgu.Parameters.AddWithValue("@RUHSATVEREN_U", comruhsatveren.SelectedValue);
                    sorgu.Parameters.AddWithValue("@RUHSATDONEM_U", comdonem.SelectedValue);
                    sorgu.Parameters.AddWithValue("@V_ID_U", lblidcihaz.Text);
                    sorgu.ExecuteNonQuery();

                    // ------   UPDATE   ------ \\

                    ScriptManager.RegisterStartupScript(this, this.GetType(), System.Guid.NewGuid().ToString(), "YeniMesaj('Ruhsat başarıyla güncellenmiştir.','Tamam');", true);
                    ScriptManager.RegisterStartupScript(this, this.GetType(), System.Guid.NewGuid().ToString(), "ShowMessage('Ruhsat başarıyla güncellenmiştir.','Tamam','yeni');", true);
                    btnruhstkaydet.Enabled = false;
                    btnruhstkaydet.CssClass = "btn btn-success disabled";




                }

            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), System.Guid.NewGuid().ToString(), "YeniMesaj('Beklenmeyen bir hata oluştu.','Hata');", true);
                ScriptManager.RegisterStartupScript(this, this.GetType(), System.Guid.NewGuid().ToString(), "ShowMessage('Hata oluştu :'" + ex.Message.Replace("'", "") + ",'Hata','yeni');", true);
            }

            if (ConnectionState.Open == conn.State)
                conn.Close();
            listele_ruhsat();


            teminatverilenyer_sec();

            tab_ac();
            TabPanel2.Visible = true;
            txtkaziuzunluk.Enabled = false;
            txtkazigenislik.Enabled = false;
            txtkaziderinlik.Enabled = false;
            //TabPanel3.Visible = true;
            //TabPanel4.Visible = true;
            //TabPanel5.Visible = true;

        }
        protected void btnsilkapat_Click(object sender, EventArgs e)
        {

            js_calistir("ModalKapat('ModalSil');");
            js_calistir("ModalAc('ModalRuhsatTalep');");
        }
        protected void btnsil_Click(object sender, EventArgs e)
        {
            if (lblislem_sil.Text == "ruhsat_kesif-sil")
            {
                SqlCommand sorgu = new SqlCommand();
                sorgu.Connection = conn;
                conn.Open();

                sorgu.CommandText = "SELECT COUNT(*) FROM RUHSAT_RUHSATLAR WHERE KESIF_ID=@SAY1 ";
                sorgu.Parameters.AddWithValue("@SAY1", lblruhsatidsil.Text);
                int sayi = Convert.ToInt16(sorgu.ExecuteScalar());

                if (sayi == 0)
                {
                    sorgu.CommandText = "DELETE FROM RUHSAT_RUHSATLAR WHERE ID=@SIL1";
                    sorgu.Parameters.AddWithValue("@SIL1", lblruhsatidsil.Text);
                    sorgu.ExecuteNonQuery();

                    sorgu.CommandText = "DELETE FROM RUHSAT_KAZI_BILGI WHERE RUHSAT_ID=@SIL2";
                    sorgu.Parameters.AddWithValue("@SIL2", lblruhsatidsil.Text);
                    sorgu.ExecuteNonQuery();

                    sorgu.CommandText = "DELETE FROM RUHSAT_DOSYALAR WHERE RUHSAT_ID=@SIL3";
                    sorgu.Parameters.AddWithValue("@SIL3", lblruhsatidsil.Text);
                    sorgu.ExecuteNonQuery();

                    conn.Close();
                    ScriptManager.RegisterStartupScript(this, this.GetType(), System.Guid.NewGuid().ToString(), "YeniMesaj('Bu ruhsata ilişkin tüm veriler silinmiştir.','Tamam');", true);
                    ScriptManager.RegisterStartupScript(this, this.GetType(), System.Guid.NewGuid().ToString(), "ShowMessage('Bu ruhsata ilişkin tüm veriler silinmiştir.','Tamam','sil');", true);

                    btnsil.Enabled = false;
                    btnsil.CssClass = "btn btn-danger disabled";

                    listele_ruhsat();
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), System.Guid.NewGuid().ToString(), "YeniMesaj('Bu ruhsat kaydının kabul kaydı oluşturulduğu için silme işlemi yapılamaz.','Hata');", true);
                    ScriptManager.RegisterStartupScript(this, this.GetType(), System.Guid.NewGuid().ToString(), "ShowMessage('Bu ruhsat kaydının kabul kaydı oluşturulduğu için silme işlemi yapılamaz.','Hata','sil');", true);
                    btnsil.Enabled = false;
                    btnsil.CssClass = "btn btn-danger disabled";
                }
                if (ConnectionState.Open == conn.State)
                    conn.Close();
                //keşif ruhsatın metraj bilgisi, dosya bilgisi, odeme bilgisi vs varsa silme işlemi yapılamaz yap

            }
            if (lblislem_sil.Text == "ruhsat_metraj-sil")
            {
                SqlCommand sorgu = new SqlCommand();
                sorgu.Connection = conn;
                conn.Open();

                sorgu.CommandText = "DELETE FROM RUHSAT_KAZI_BILGI WHERE ID=@SIL2";
                sorgu.Parameters.AddWithValue("@SIL2", lblruhsatidsil.Text);
                sorgu.ExecuteNonQuery();

                conn.Close();
                ScriptManager.RegisterStartupScript(this, this.GetType(), System.Guid.NewGuid().ToString(), "YeniMesaj('Metraj bilgisi silinmiştir.','Tamam');", true);
                ScriptManager.RegisterStartupScript(this, this.GetType(), System.Guid.NewGuid().ToString(), "ShowMessage('Metraj bilgisi silinmiştir.','Tamam','sil');", true);

                listele_metraj();

                btnsil.Enabled = false;
                btnsil.CssClass = "btn btn-danger disabled";

                if (ConnectionState.Open == conn.State)
                    conn.Close();
                //keşif ruhsatın metraj bilgisi, dosya bilgisi, odeme bilgisi vs varsa silme işlemi yapılamaz yap

                js_calistir("ModalKapat('ModalSil');");
                js_calistir("ModalAc('ModalRuhsatTalep');");

                tab_ac();

                ruhsat_kazi_turu_sec();
            }
        }


        //----------------------------------------//


        protected void btnmetrajekle_Click(object sender, EventArgs e)
        {
            SqlCommand sorgu = new SqlCommand();
            sorgu.Connection = conn;

            conn.Open();

            string userid = Session["KULLANICI_ID"].ToString();


            try
            {

                if (lblidcihaz.Text != "")
                {                                                                          
                    sorgu.CommandText = "INSERT INTO    RUHSAT_KAZI_BILGI (RUHSAT_ID, KESIFMIKABULMU, UZUNLUK, GENISLIK, DERINLIK, KAZI_TURU_ID, KAZI_TIPI_ID, KAZI_DONEM_ID) " +
                        " VALUES(@RTK_ID_K, @KESIFMIKABULMU_K, @UZN_K, @GNS_K, @DRN_K, @KTURU_ID_K, @KTIPI_ID_K, @KDONEM_ID_K); SELECT @@IDENTITY AS 'Identity' ";
                    sorgu.Parameters.AddWithValue("@RTK_ID_K", lblidcihaz.Text);
                    sorgu.Parameters.AddWithValue("@KESIFMIKABULMU_K", 0); //keşif=0 , kabul=1
                    sorgu.Parameters.AddWithValue("@UZN_K", Convert.ToDecimal(txtkaziuzunluk.Text));
                    //sorgu.Parameters.AddWithValue("@UZN_K", Convert.ToDecimal(txtkaziuzunluk.Text.Replace(".", ",")));
                    sorgu.Parameters.AddWithValue("@GNS_K", Convert.ToDecimal(txtkazigenislik.Text));
                    sorgu.Parameters.AddWithValue("@DRN_K", Convert.ToDecimal(txtkaziderinlik.Text));
                    sorgu.Parameters.AddWithValue("@KTURU_ID_K", comkazituru.SelectedValue);
                    sorgu.Parameters.AddWithValue("@KTIPI_ID_K", comkazitipi_sec.SelectedValue);
                    sorgu.Parameters.AddWithValue("@KDONEM_ID_K", comdonem.SelectedValue);
                    string id_metraj = sorgu.ExecuteScalar().ToString();
                    lblidmetraj.Text = id_metraj;

                    /*
                    // HESAP Kitap //

                    DataTable dt_kazi_bilgi;
                    dt_kazi_bilgi = tkod.GetData("SELECT * FROM  RUHSAT_KAZI_BILGI WHERE  ID= " + id_metraj + " " );

                    for (int i = 0; i < dt_kazi_bilgi.Rows.Count; i++)
                    {
                        
                    }

                    if (dt_kazi_bilgi.Rows.Count > 0)
                    {
                        for (int i = 1; i < dt_kazi_bilgi.Rows.Count + 1; i++)
                        {
                            if (Convert.ToInt16(dt_kazi_bilgi.Rows[i - 1][0].ToString()) != i)
                            {
                   
                            }
                        }
                    }
                    */


                    string id = lblidcihaz.Text;
                    bilgi_topla(id);

                    string kazi_turu = tkod.sql_calistir_param("SELECT KAZI_TURU_ID FROM RUHSAT_KAZI_BILGI  WHERE ID=@ID335", new SqlParameter("ID335", id_metraj));
                    string kazi_tipi = tkod.sql_calistir_param("SELECT KAZI_TIPI_ID FROM RUHSAT_KAZI_BILGI  WHERE ID=@ID340", new SqlParameter("ID340", id_metraj));

                    int il_id = Convert.ToInt16(lbl_il.Text);
                    int ruhsat_yetkilisi = Convert.ToInt16(lbl_ruhsat_yetkilisi.Text);
                    int donem = Convert.ToInt16(lbl_donem.Text);
                    
                    sorgu.CommandText = " SELECT UCRET FROM RUHSAT_KAZI_TIPI WHERE IL_ID='"+ il_id + "' and RUHSAT_VEREN_ID='" + ruhsat_yetkilisi + "' and DONEM_ID='" + donem + "' and KAZI_TURU_ID='" + kazi_turu + "' and ID='" + kazi_tipi + "'  ";
                    string ATB_Tarife = sorgu.ExecuteScalar().ToString();

                    sorgu.CommandText = " SELECT BIRIM_ID FROM RUHSAT_KAZI_TIPI WHERE IL_ID='" + il_id + "' and RUHSAT_VEREN_ID='" + ruhsat_yetkilisi + "' and DONEM_ID='" + donem + "' and KAZI_TURU_ID='" + kazi_turu + "' and ID='" + kazi_tipi + "'  ";
                    string birim_id = sorgu.ExecuteScalar().ToString();

                    //metraj_txt_kontrol(il_id, ruhsat_yetkilisi, donem, kazi_turu, kazi_tipi);

                    sorgu.CommandText = " SELECT BIRIM FROM RUHSAT_KAZI_BIRIMI WHERE ID='" + birim_id + "' ";
                    string birim = sorgu.ExecuteScalar().ToString();

                    sorgu.CommandText = " SELECT KDV FROM RUHSAT_KAZI_TIPI WHERE IL_ID='" + il_id + "' and RUHSAT_VEREN_ID='" + ruhsat_yetkilisi + "' and DONEM_ID='" + donem + "' and KAZI_TURU_ID='" + kazi_turu + "' and ID='" + kazi_tipi + "'  ";
                    bool kdv_atb_tarife = Convert.ToBoolean(sorgu.ExecuteScalar());

                    sorgu.CommandText = " SELECT COUNT(KESIF_KONTROL_BEDELI) FROM RUHSAT_TARIFE WHERE IL_ID='" + il_id + "' and RUHSAT_VEREN_ID='" + ruhsat_yetkilisi + "' and DONEM_ID='" + donem + "' and KAZI_TURU_ID='" + kazi_turu + "' ";
                    int KKB_tarife_say = Convert.ToInt32(sorgu.ExecuteScalar());
                    if (KKB_tarife_say>0)
                    {
                        sorgu.CommandText = " SELECT ISNULL(KESIF_KONTROL_BEDELI,0) FROM RUHSAT_TARIFE WHERE IL_ID='" + il_id + "' and RUHSAT_VEREN_ID='" + ruhsat_yetkilisi + "' and DONEM_ID='" + donem + "' and KAZI_TURU_ID='" + kazi_turu + "' ";
                        string KKB_tarife = sorgu.ExecuteScalar().ToString();
                    

                        sorgu.CommandText = " SELECT RUHSAT_BEDELI FROM RUHSAT_TARIFE WHERE IL_ID='" + il_id + "' and RUHSAT_VEREN_ID='" + ruhsat_yetkilisi + "' and DONEM_ID='" + donem + "' and KAZI_TURU_ID='" + kazi_turu + "' ";
                        string ruhsat_bedeli_tarife = sorgu.ExecuteScalar().ToString();

                        sorgu.CommandText = " SELECT KAZI_IZIN_HARCI FROM RUHSAT_TARIFE WHERE IL_ID='" + il_id + "' and RUHSAT_VEREN_ID='" + ruhsat_yetkilisi + "' and DONEM_ID='" + donem + "' and KAZI_TURU_ID='" + kazi_turu + "' ";
                        decimal kazi_izin_harci_tarife = Convert.ToDecimal(sorgu.ExecuteScalar());

                        sorgu.CommandText = " SELECT KDV FROM RUHSAT_TARIFE WHERE IL_ID='" + il_id + "' and RUHSAT_VEREN_ID='" + ruhsat_yetkilisi + "' and DONEM_ID='" + donem + "' and KAZI_TURU_ID='" + kazi_turu + "' ";
                        string kdv_tarife = sorgu.ExecuteScalar().ToString();

                        sorgu.CommandText = " SELECT TEMINATA_DAHIL_OLMA_ALTLIMITI FROM RUHSAT_TARIFE WHERE IL_ID='" + il_id + "' and RUHSAT_VEREN_ID='" + ruhsat_yetkilisi + "' and DONEM_ID='" + donem + "' and KAZI_TURU_ID='" + kazi_turu + "' ";
                        string teminat_altlimit = sorgu.ExecuteScalar().ToString();

                        sorgu.CommandText = " SELECT TEMINATA_DAHIL_OLMA_YUZDESI FROM RUHSAT_TARIFE WHERE IL_ID='" + il_id + "' and RUHSAT_VEREN_ID='" + ruhsat_yetkilisi + "' and DONEM_ID='" + donem + "' and KAZI_TURU_ID='" + kazi_turu + "' ";
                        string teminat_yuzde = sorgu.ExecuteScalar().ToString();

                        if (birim == "m2")
                        {
                            if (kdv_atb_tarife == true)
                            {
                                decimal m2_sonuc = Convert.ToDecimal(txtkaziuzunluk.Text) * Convert.ToDecimal(txtkazigenislik.Text);  //bunu for ile çalıştır
                                decimal m2_tutar = Convert.ToDecimal(m2_sonuc) * Convert.ToDecimal(ATB_Tarife);
                                lbl_alan_tahrip_bedeli.Text = Convert.ToString(m2_tutar);

                                decimal kesif_kontrol_bedeli = Convert.ToDecimal(ATB_Tarife) + (Convert.ToDecimal(lbl_alan_tahrip_bedeli.Text) * Convert.ToDecimal("0,01"));
                                lbl_kesif_kontrol_bedeli.Text= Convert.ToString(kesif_kontrol_bedeli);

                                decimal altyapi_ruhsat_bedeli = Convert.ToDecimal(ruhsat_bedeli_tarife) * m2_sonuc;
                                lbl_altyapi_ruhsat_bedeli.Text = Convert.ToString(altyapi_ruhsat_bedeli);

                                decimal kazi_izin_harci = m2_tutar * kazi_izin_harci_tarife;
                                lbl_kazi_izin_harci.Text = Convert.ToString(kazi_izin_harci);

                                decimal toplam = kazi_izin_harci + altyapi_ruhsat_bedeli + kesif_kontrol_bedeli + m2_tutar;
                                lbl_toplam.Text = Convert.ToString(toplam) ;


                                //sorgu.CommandText = "UPDATE RUHSAT_KAZI_BILGI SET ATB=" + m2_sonuc + ", TUTAR=" + m2_tutar + " WHERE ID='" + id_metraj + "' ";
                                //sorgu.ExecuteNonQuery();



                            }
                            else
                            {
                                decimal m3_sonuc = Convert.ToDecimal(txtkaziuzunluk.Text) * Convert.ToDecimal(txtkazigenislik.Text) * Convert.ToDecimal(txtkaziderinlik.Text);  //bunu for ile çalıştır
                                decimal m3_tutar = Convert.ToDecimal(m3_sonuc) * Convert.ToDecimal(ATB_Tarife);
                                lbl_alan_tahrip_bedeli.Text = Convert.ToString(m3_tutar);

                                decimal kesif_kontrol_bedeli = Convert.ToDecimal(ATB_Tarife) + (Convert.ToDecimal(lbl_alan_tahrip_bedeli.Text) * Convert.ToDecimal("0,01"));
                                lbl_kesif_kontrol_bedeli.Text = Convert.ToString(kesif_kontrol_bedeli);

                                decimal altyapi_ruhsat_bedeli = Convert.ToDecimal(ruhsat_bedeli_tarife) * m3_sonuc;
                                lbl_altyapi_ruhsat_bedeli.Text = Convert.ToString(altyapi_ruhsat_bedeli);

                                decimal kazi_izin_harci = m3_tutar * kazi_izin_harci_tarife;
                                lbl_kazi_izin_harci.Text = Convert.ToString(kazi_izin_harci);

                                decimal toplam = kazi_izin_harci + altyapi_ruhsat_bedeli + kesif_kontrol_bedeli + m3_tutar;
                                lbl_toplam.Text = Convert.ToString(toplam);

                                //decimal m2_sonuc = Convert.ToDecimal(txtkaziuzunluk.Text.Replace(",", ".")) * Convert.ToDecimal(txtkazigenislik.Text.Replace(",", "."));
                                //decimal m2_tutar = Convert.ToDecimal(m2_sonuc) * Convert.ToDecimal(ATB_Tarife) * 0.18;
                                //sorgu.CommandText = "UPDATE RUHSAT_KAZI_BILGI SET ATB=" + m2_sonuc + ", TUTAR=" + m2_tutar + " WHERE ID='" + id_metraj + "' ";
                                //sorgu.ExecuteNonQuery();
                            }

                            //}
                            //if (birim == "m3")
                            //{
                            //    //decimal m3_sonuc = Convert.ToDecimal(txtkaziuzunluk.Text) * Convert.ToDecimal(txtkazigenislik.Text) * Convert.ToDecimal(txtkaziderinlik.Text);
                            //    //decimal m3_tutar = Convert.ToDecimal(m3_sonuc) * Convert.ToDecimal(ATB_Tarife);
                            //    //sorgu.CommandText = "UPDATE RUHSAT_KAZI_BILGI SET ATB=" + m3_sonuc + ", TUTAR=" + m3_tutar + " WHERE ID='" + id_metraj + "' ";
                            //    //sorgu.ExecuteNonQuery();
                        }

                        // ------   UPDATE   ------ \\
                        sorgu.CommandText = "UPDATE RUHSAT_KAZI_BILGI SET KKB_TARIFE=@KKB_TARIFE_U, " +
                                                " RUHSAT_BEDELI_TARIFE=@RUHSAT_BEDELI_TARIFE_U, KAZI_IZIN_HARCI_TARIFE=@KAZI_IZIN_HARCI_TARIFE_U, KDV_TARIFE=@KDV_TARIFE_U    WHERE ID=@V_ID_U ";
                        sorgu.Parameters.AddWithValue("@KKB_TARIFE_U", Convert.ToDecimal(KKB_tarife));
                        sorgu.Parameters.AddWithValue("@RUHSAT_BEDELI_TARIFE_U", Convert.ToDecimal(ruhsat_bedeli_tarife));
                        sorgu.Parameters.AddWithValue("@KAZI_IZIN_HARCI_TARIFE_U", Convert.ToDecimal(kazi_izin_harci_tarife));
                        sorgu.Parameters.AddWithValue("@KDV_TARIFE_U", kdv_tarife);
                        sorgu.Parameters.AddWithValue("@V_ID_U", id_metraj);

                        sorgu.ExecuteNonQuery();
                        // ------   UPDATE   ------ \\

                    }
                    // ------   UPDATE   ------ \\
                    sorgu.CommandText = "UPDATE RUHSAT_KAZI_BILGI SET ATB_TARIFE=@ATB_TARIFE_U, KDV_ATB_TARIFE=@KDV_ATB_TARIFE_U, BIRIM=@BIRIM_U " +
                                            "   WHERE ID=@V_ID_U1 ";
                    sorgu.Parameters.AddWithValue("@ATB_TARIFE_U", Convert.ToDecimal(ATB_Tarife));
                    sorgu.Parameters.AddWithValue("@KDV_ATB_TARIFE_U", kdv_atb_tarife);
                    sorgu.Parameters.AddWithValue("@BIRIM_U", birim);
                    sorgu.Parameters.AddWithValue("@V_ID_U1", id_metraj);

                    sorgu.ExecuteNonQuery();
                    // ------   UPDATE   ------ \\

                    ScriptManager.RegisterStartupScript(this, this.GetType(), System.Guid.NewGuid().ToString(), "YeniMesaj('Metraj bilgisi başarıyla kaydedilmiştir.','Tamam');", true);
                    ScriptManager.RegisterStartupScript(this, this.GetType(), System.Guid.NewGuid().ToString(), "ShowMessage('Metraj bilgisi başarıyla kaydedilmiştir.','Tamam','yeni');", true);

                    ruhsat_guncelleme_kontrol();

                }
                else
                {
                    /*// ------   UPDATE   ------ \\
                    sorgu.CommandText = "UPDATE RUHSAT_KAZI_BILGI SET (RUHSAT_ID=@RTK_ID_U, UZUNLUK=@PROJENO_U, GENISLIK=@IL_ID_U, KAZI_TURU_ID=@B_ID_U, KAZI_TIPI_ID=@ILCE_ID_U WHERE ID=@V_ID_U ";
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
            tab_ac();
            ruhsat_kazi_turu_sec();


        }
        protected void btnhesapla_Click(object sender, EventArgs e)
        {

            SqlCommand sorgu = new SqlCommand();
            sorgu.Connection = conn;

            conn.Open();
            decimal atb = 0;
            decimal kkb = 0;
            decimal arb = 0;
            decimal kih = 0;
            decimal toplam = 0;





            for (int i = 0; i < grid_metraj.Rows.Count; i++)
            {
                string id_metraj = HttpUtility.HtmlDecode(grid_metraj.Rows[i].Cells[0].Text);

                int a = i + i;


                string id = lblidcihaz.Text;
                bilgi_topla(id);

                string kazi_turu = tkod.sql_calistir_param("SELECT KAZI_TURU_ID FROM RUHSAT_KAZI_BILGI  WHERE ID=@ID335", new SqlParameter("ID335", id_metraj));
                string kazi_tipi = tkod.sql_calistir_param("SELECT KAZI_TIPI_ID FROM RUHSAT_KAZI_BILGI  WHERE ID=@ID340", new SqlParameter("ID340", id_metraj));

                int il_id = Convert.ToInt16(lbl_il.Text);
                int ruhsat_yetkilisi = Convert.ToInt16(lbl_ruhsat_yetkilisi.Text);
                int donem = Convert.ToInt16(lbl_donem.Text);

                sorgu.CommandText = " SELECT UCRET FROM RUHSAT_KAZI_TIPI WHERE IL_ID='" + il_id + "' and RUHSAT_VEREN_ID='" + ruhsat_yetkilisi + "' and DONEM_ID='" + donem + "' and KAZI_TURU_ID='" + kazi_turu + "' and ID='" + kazi_tipi + "'  ";
                string ATB_Tarife = sorgu.ExecuteScalar().ToString();

                sorgu.CommandText = " SELECT BIRIM_ID FROM RUHSAT_KAZI_TIPI WHERE IL_ID='" + il_id + "' and RUHSAT_VEREN_ID='" + ruhsat_yetkilisi + "' and DONEM_ID='" + donem + "' and KAZI_TURU_ID='" + kazi_turu + "' and ID='" + kazi_tipi + "'  ";
                string birim_id = sorgu.ExecuteScalar().ToString();

                //metraj_txt_kontrol(il_id, ruhsat_yetkilisi, donem, kazi_turu, kazi_tipi);

                sorgu.CommandText = " SELECT BIRIM FROM RUHSAT_KAZI_BIRIMI WHERE ID='" + birim_id + "' ";
                string birim = sorgu.ExecuteScalar().ToString();

                sorgu.CommandText = " SELECT KDV FROM RUHSAT_KAZI_TIPI WHERE IL_ID='" + il_id + "' and RUHSAT_VEREN_ID='" + ruhsat_yetkilisi + "' and DONEM_ID='" + donem + "' and KAZI_TURU_ID='" + kazi_turu + "' and ID='" + kazi_tipi + "'  ";
                bool kdv_atb_tarife = Convert.ToBoolean(sorgu.ExecuteScalar());

                sorgu.CommandText = " SELECT COUNT(KESIF_KONTROL_BEDELI) FROM RUHSAT_TARIFE WHERE IL_ID='" + il_id + "' and RUHSAT_VEREN_ID='" + ruhsat_yetkilisi + "' and DONEM_ID='" + donem + "' and KAZI_TURU_ID='" + kazi_turu + "' ";
                int KKB_tarife_say = Convert.ToInt32(sorgu.ExecuteScalar());
                if (KKB_tarife_say > 0)
                {
                    sorgu.CommandText = " SELECT ISNULL(KESIF_KONTROL_BEDELI,0) FROM RUHSAT_TARIFE WHERE IL_ID='" + il_id + "' and RUHSAT_VEREN_ID='" + ruhsat_yetkilisi + "' and DONEM_ID='" + donem + "' and KAZI_TURU_ID='" + kazi_turu + "' ";
                    string KKB_tarife = sorgu.ExecuteScalar().ToString();


                    sorgu.CommandText = " SELECT RUHSAT_BEDELI FROM RUHSAT_TARIFE WHERE IL_ID='" + il_id + "' and RUHSAT_VEREN_ID='" + ruhsat_yetkilisi + "' and DONEM_ID='" + donem + "' and KAZI_TURU_ID='" + kazi_turu + "' ";
                    string ruhsat_bedeli_tarife = sorgu.ExecuteScalar().ToString();

                    sorgu.CommandText = " SELECT KAZI_IZIN_HARCI FROM RUHSAT_TARIFE WHERE IL_ID='" + il_id + "' and RUHSAT_VEREN_ID='" + ruhsat_yetkilisi + "' and DONEM_ID='" + donem + "' and KAZI_TURU_ID='" + kazi_turu + "' ";
                    decimal kazi_izin_harci_tarife = Convert.ToDecimal(sorgu.ExecuteScalar());

                    sorgu.CommandText = " SELECT KDV FROM RUHSAT_TARIFE WHERE IL_ID='" + il_id + "' and RUHSAT_VEREN_ID='" + ruhsat_yetkilisi + "' and DONEM_ID='" + donem + "' and KAZI_TURU_ID='" + kazi_turu + "' ";
                    string kdv_tarife = sorgu.ExecuteScalar().ToString();

                    sorgu.CommandText = " SELECT TEMINATA_DAHIL_OLMA_ALTLIMITI FROM RUHSAT_TARIFE WHERE IL_ID='" + il_id + "' and RUHSAT_VEREN_ID='" + ruhsat_yetkilisi + "' and DONEM_ID='" + donem + "' and KAZI_TURU_ID='" + kazi_turu + "' ";
                    string teminat_altlimit = sorgu.ExecuteScalar().ToString();

                    sorgu.CommandText = " SELECT TEMINATA_DAHIL_OLMA_YUZDESI FROM RUHSAT_TARIFE WHERE IL_ID='" + il_id + "' and RUHSAT_VEREN_ID='" + ruhsat_yetkilisi + "' and DONEM_ID='" + donem + "' and KAZI_TURU_ID='" + kazi_turu + "' ";
                    string teminat_yuzde = sorgu.ExecuteScalar().ToString();

                    if (birim == "m2")
                    {
                        if (kdv_atb_tarife == true)
                        {
                            string txtkaziuzunluk = HttpUtility.HtmlDecode(grid_metraj.Rows[i].Cells[1].Text);
                            string txtkazigenislik = HttpUtility.HtmlDecode(grid_metraj.Rows[i].Cells[2].Text);

                            decimal m2_sonuc = Convert.ToDecimal(txtkaziuzunluk) * Convert.ToDecimal(txtkazigenislik);
                            decimal m2_tutar = Convert.ToDecimal(m2_sonuc) * Convert.ToDecimal(ATB_Tarife);
                            //lbl_alan_tahrip_bedeli.Text = Convert.ToString(m2_tutar);
                            atb += m2_tutar;

                            decimal kesif_kontrol_bedeli = Convert.ToDecimal(ATB_Tarife) + (Convert.ToDecimal(m2_tutar) * Convert.ToDecimal("0,01"));
                            //lbl_kesif_kontrol_bedeli.Text = Convert.ToString(kesif_kontrol_bedeli);
                            kkb += kesif_kontrol_bedeli;

                            decimal altyapi_ruhsat_bedeli = Convert.ToDecimal(ruhsat_bedeli_tarife) * m2_sonuc;
                            //lbl_altyapi_ruhsat_bedeli.Text = Convert.ToString(altyapi_ruhsat_bedeli);
                            arb += altyapi_ruhsat_bedeli;

                            decimal kazi_izin_harci = m2_tutar * kazi_izin_harci_tarife;
                            //lbl_kazi_izin_harci.Text = Convert.ToString(kazi_izin_harci);
                            kih += kazi_izin_harci;

                            //lbl_toplam.Text = Convert.ToString(toplam);
                            //sorgu.CommandText = "UPDATE RUHSAT_KAZI_BILGI SET ATB=" + m2_sonuc + ", TUTAR=" + m2_tutar + " WHERE ID='" + id_metraj + "' ";
                            //sorgu.ExecuteNonQuery();
                            txt_kdv.Text = "0";

                        }
                        else
                        {

                        }
                    }
                    if (birim == "m3")
                    {
                        if (kdv_atb_tarife == true)
                        {
                            string txtkaziderinlik = HttpUtility.HtmlDecode(grid_metraj.Rows[i].Cells[3].Text);
                            string txtkazigenislik = HttpUtility.HtmlDecode(grid_metraj.Rows[i].Cells[2].Text);
                            string txtkaziuzunluk = HttpUtility.HtmlDecode(grid_metraj.Rows[i].Cells[1].Text);

                            decimal m3_sonuc = Convert.ToDecimal(txtkaziuzunluk) * Convert.ToDecimal(txtkazigenislik) * Convert.ToDecimal(txtkaziderinlik);
                            decimal m3_tutar = Convert.ToDecimal(m3_sonuc) * Convert.ToDecimal(ATB_Tarife);
                            //lbl_alan_tahrip_bedeli.Text = Convert.ToString(m3_tutar);
                            atb += m3_tutar;

                            decimal kesif_kontrol_bedeli = Convert.ToDecimal(ATB_Tarife) + (Convert.ToDecimal(m3_tutar) * Convert.ToDecimal("0,01"));
                            //lbl_kesif_kontrol_bedeli.Text = Convert.ToString(kesif_kontrol_bedeli);
                            kkb += kesif_kontrol_bedeli;

                            decimal altyapi_ruhsat_bedeli = Convert.ToDecimal(ruhsat_bedeli_tarife) * m3_sonuc;
                            //lbl_altyapi_ruhsat_bedeli.Text = Convert.ToString(altyapi_ruhsat_bedeli);
                            arb += altyapi_ruhsat_bedeli;

                            decimal kazi_izin_harci = m3_tutar * kazi_izin_harci_tarife;
                            //lbl_kazi_izin_harci.Text = Convert.ToString(kazi_izin_harci);
                            kih += kazi_izin_harci;


                            //decimal toplam = kazi_izin_harci + altyapi_ruhsat_bedeli + kesif_kontrol_bedeli + m3_tutar;
                            //lbl_toplam.Text = Convert.ToString(toplam);
                            txt_kdv.Text = "0";


                        }
                        else
                        {

                        }

                    }
                    if (birim == "mt")
                    {
                        if (kdv_atb_tarife == true)
                        {
                            string txtkaziuzunluk = HttpUtility.HtmlDecode(grid_metraj.Rows[i].Cells[1].Text);

                            decimal mt_sonuc = Convert.ToDecimal(txtkaziuzunluk);
                            decimal mt_tutar = Convert.ToDecimal(mt_sonuc) * Convert.ToDecimal(ATB_Tarife);
                            //lbl_alan_tahrip_bedeli.Text = Convert.ToString(m3_tutar);
                            atb += mt_tutar;

                            decimal kesif_kontrol_bedeli = Convert.ToDecimal(ATB_Tarife) + (Convert.ToDecimal(mt_tutar) * Convert.ToDecimal("0,01"));
                            //lbl_kesif_kontrol_bedeli.Text = Convert.ToString(kesif_kontrol_bedeli);
                            kkb += kesif_kontrol_bedeli;

                            decimal altyapi_ruhsat_bedeli = Convert.ToDecimal(ruhsat_bedeli_tarife) * mt_sonuc;
                            //lbl_altyapi_ruhsat_bedeli.Text = Convert.ToString(altyapi_ruhsat_bedeli);
                            arb += altyapi_ruhsat_bedeli;

                            decimal kazi_izin_harci = mt_tutar * kazi_izin_harci_tarife;
                            //lbl_kazi_izin_harci.Text = Convert.ToString(kazi_izin_harci);
                            kih += kazi_izin_harci;

                            txt_kdv.Text = "0";

                        }
                        else
                        {

                        }
                    }
                }
            }
            toplam = kih + arb + kkb + atb;
            /*
            lbl_alan_tahrip_bedeli.Text = atb.ToString("###,###.##");
            lbl_kesif_kontrol_bedeli.Text = kkb.ToString("###,###.##");
            lbl_altyapi_ruhsat_bedeli.Text = arb.ToString("###,###.##");
            lbl_kazi_izin_harci.Text = kih.ToString("###,###.##");
            lbl_toplam.Text = toplam.ToString("###,###.##");
            */
            txt_alan_tahrip_bedeli.Text = atb.ToString("###,###.##");
            txt_kesif_kontrol_bedeli.Text = kkb.ToString("###,###.##");
            txt_altyapi_ruhsat_bedeli.Text = arb.ToString("###,###.##");
            txt_kazi_izin_harci.Text = kih.ToString("###,###.##");
            txt_toplam.Text = toplam.ToString("###,###.##");
            
        }

        protected void grid_metraj_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName.Equals("sil"))
            {
                int index = Convert.ToInt32(e.CommandArgument) % grid_metraj.PageSize;
                string id = grid_metraj.DataKeys[index].Value.ToString();
                lblruhsatidsil.Text = id;
                //lblruhsatidsil.Text = grid.DataKeys[index].Value.ToString();
                //int index = Convert.ToInt32(e.CommandArgument);
                //lblidsil.Text = grid.DataKeys[index].Value.ToString();
                lblislem_sil.Text = "ruhsat_metraj-sil";
                btnsil.Enabled = true;
                btnsil.CssClass = "btn btn-danger ";

                js_calistir("ModalKapat('ModalRuhsatTalep');");
                js_calistir("ModalAc('ModalSil');");


                lblmodalyenibaslik2.Text = "Metraj Sil";

                btnsil.Visible = true;
                btnkabulolustur.Visible = false;
                btntamamlandiolustur.Visible = false;

                Panel_Sil.Visible = true;
                Panel_Kabul_Olustur.Visible = false;
                Panel_Tamamlandi_Olustur.Visible = false;
                

                System.Text.StringBuilder sb = new System.Text.StringBuilder();
                sb.Append(@"<script type='text/javascript'>");
                sb.Append("$(\"#ModalSil\").modal(\"show\");");
                sb.Append(@"</script>");
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(),
                           "ModalScript", sb.ToString(), false);
            }
        }

        protected void grid_metraj_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            grid_metraj.PageIndex = e.NewPageIndex;
            listele_metraj();
        }

        //----------------------------------------//


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

                //Ruhsat ile ilgili temel bilgileri kopyala
                sorgu.CommandText = "INSERT INTO RUHSAT_RUHSATLAR (RUHSATNO, PROJENO, IL_ID, BOLGE_ID, ILCE_ID, MAHALLE_ID, CADDESOKAK_ID, ADRES_DETAY, T_PROJETIP_ID, " +
                                    " A_PROJETIP_ID, BASLANGIC_TARIHI, BITIS_TARIHI, ACIKLAMA, DURUM, GOREV_TAMAMMI, RUHSAT_VEREN_ID, RUHSAT_DONEM_ID) " +

                                    " SELECT RUHSATNO, PROJENO, IL_ID, BOLGE_ID, ILCE_ID, MAHALLE_ID, CADDESOKAK_ID, ADRES_DETAY, T_PROJETIP_ID, A_PROJETIP_ID, BASLANGIC_TARIHI, " +
                                    " BITIS_TARIHI, ACIKLAMA, 'Kabul', 'false', RUHSAT_VEREN_ID, RUHSAT_DONEM_ID FROM RUHSAT_RUHSATLAR WHERE ID=@ID_KESIF   SELECT @@IDENTITY AS 'Identity'  ";
                                    sorgu.Parameters.AddWithValue("@ID_KESIF", lblruhsatid.Text);
                string id2 = Convert.ToString(sorgu.ExecuteScalar()); //id2 > KABUL ID'si YANİ YENİ ID

                //Kopyalanan yeni kayıt için Kabul Kaydı olduğunu belirt, keşif kaydının id'sini kabul kaydının keşif_id alanına yaz, kayıt eden kayıt tarihi
                sorgu.CommandText = "UPDATE RUHSAT_RUHSATLAR SET KABULMU='true', KESIF_ID=@ID_KESIF1,   KAYIT_EDEN=" + userid + ",KAYIT_TARIHI=getdate() WHERE ID='" + id2 + "' ";
                sorgu.Parameters.AddWithValue("@ID_KESIF1", lblruhsatid.Text);
                sorgu.ExecuteNonQuery();

                //Keşif kaydının görevinin bittiği bilgisi işleniyor. Kabul kaydının ID'si keşif kaydının KABUL_Id alanına işleniyor.
                sorgu.CommandText = "UPDATE RUHSAT_RUHSATLAR SET GOREV_TAMAMMI='true', KABUL_ID='" + id2 + "' WHERE ID=@ID_KESIF2 ";
                sorgu.Parameters.AddWithValue("@ID_KESIF2", lblruhsatid.Text);
                sorgu.ExecuteNonQuery();

                //Keşif ödeme ve teminat bilgilerinin toplam rakama dahil edilmemesi için TOPLAMADAHILMI alanı false yapılıyor
                sorgu.CommandText = "UPDATE RUHSAT_ODEMELER SET TOPLAMADAHILMI='false' WHERE RUHSAT_ID=@ID_KESIF8 ";
                sorgu.Parameters.AddWithValue("@ID_KESIF8", lblruhsatid.Text);
                sorgu.ExecuteNonQuery();

                //Keşif kaydına ait ödeme bilgilerini yeni kayda kopyalanıyor.
                sorgu.CommandText = "INSERT INTO RUHSAT_ODEMELER (IL_ID, BOLGE_ID, RUHSAT_ID, RUHSAT_ID_KESIF, KESIFMIKABULMU,  TEMINAT_ID, ODEME_TIPI, TOPLAMADAHILMI, ALAN_TAHRIP_BEDELI, KESIF_KONTROL_BEDELI, ALTYAPI_RUHSAT_BEDELI, ALTYAPI_KAZI_IZIN_HARCI, KDV, NAKIT_TOPLAM, TEMINAT_TOPLAM, KAYIT_EDEN, KAYIT_TARIHI) " +
                                        " SELECT IL_ID, BOLGE_ID, " + id2 + ", @ID_KESIF4, 1, TEMINAT_ID, ODEME_TIPI, 1, ALAN_TAHRIP_BEDELI, KESIF_KONTROL_BEDELI, ALTYAPI_RUHSAT_BEDELI, ALTYAPI_KAZI_IZIN_HARCI, KDV, NAKIT_TOPLAM, TEMINAT_TOPLAM, " + userid + ", getdate() FROM RUHSAT_ODEMELER WHERE RUHSAT_ID=@ID_KESIF4  ";
                sorgu.Parameters.AddWithValue("@ID_KESIF4", lblruhsatid.Text);
                sorgu.ExecuteNonQuery();

                //Keşif kaydına ait kazı bilgilerini yeni kayda kopyalanıyor.
                sorgu.CommandText = "INSERT INTO RUHSAT_KAZI_BILGI (RUHSAT_ID, KESIFMIKABULMU, UZUNLUK, GENISLIK, DERINLIK, CARPIM, ATB_TARIFE, KDV_ATB_TARIFE, BIRIM, KKB_TARIFE, RUHSAT_BEDELI_TARIFE, KAZI_IZIN_HARCI_TARIFE, KDV_TARIFE, KAZI_TURU_ID, KAZI_TIPI_ID, KAZI_DONEM_ID) " +
                                        " SELECT " + id2 + ",                  'true', UZUNLUK, GENISLIK, DERINLIK, CARPIM, ATB_TARIFE, KDV_ATB_TARIFE, BIRIM, KKB_TARIFE, RUHSAT_BEDELI_TARIFE, KAZI_IZIN_HARCI_TARIFE, KDV_TARIFE, KAZI_TURU_ID, KAZI_TIPI_ID, KAZI_DONEM_ID FROM RUHSAT_KAZI_BILGI WHERE RUHSAT_ID=@ID_KESIF3  ";
                sorgu.Parameters.AddWithValue("@ID_KESIF3", lblruhsatid.Text);
                sorgu.ExecuteNonQuery();



                btnkabulolustur.Enabled = false;
                btnkabulolustur.CssClass = "btn btn-success disabled";


                conn.Close();
                ScriptManager.RegisterStartupScript(this, this.GetType(), System.Guid.NewGuid().ToString(), "YeniMesaj('Aktarım tamamlanmıştır.','Tamam');", true);
                ScriptManager.RegisterStartupScript(this, this.GetType(), System.Guid.NewGuid().ToString(), "ShowMessage('Aktarım tamamlanmıştır.','Tamam','sil');", true);

                listele_ruhsat();

            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), System.Guid.NewGuid().ToString(), "YeniMesaj('Beklenmeyen bir hata oluştu.','Hata');", true);
                ScriptManager.RegisterStartupScript(this, this.GetType(), System.Guid.NewGuid().ToString(), "ShowMessage('Hata oluştu :'" + ex.Message.Replace("'", "") + ",'Hata','ruhsatislem');", true);
            }

            if (ConnectionState.Open == conn.State)
                conn.Close();

            listele_ruhsat();



        }

        protected void btntamamlandiolustur_Click(object sender, EventArgs e)
        {
            try
            {
                SqlCommand sorgu = new SqlCommand();
                sorgu.Connection = conn;
                conn.Open();

                string userid = Session["KULLANICI_ID"].ToString();

                //
                sorgu.CommandText = "UPDATE RUHSAT_RUHSATLAR SET TAMAMLANDIMI='true', DURUM='Tamamlandı', KABULMU='False' WHERE ID=@ID_KABUL1 ";
                sorgu.Parameters.AddWithValue("@ID_KABUL1", lblruhsatid.Text);
                sorgu.ExecuteNonQuery();

                btntamamlandiolustur.Enabled = false;
                btntamamlandiolustur.CssClass = "btn btn-success disabled";


                conn.Close();
                ScriptManager.RegisterStartupScript(this, this.GetType(), System.Guid.NewGuid().ToString(), "YeniMesaj('Ruhsat süreci tamamlanmıştır.','Tamam');", true);
                ScriptManager.RegisterStartupScript(this, this.GetType(), System.Guid.NewGuid().ToString(), "ShowMessage('Ruhsat süreci tamamlanmıştır.','Tamam','sil');", true);

                listele_ruhsat();

            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), System.Guid.NewGuid().ToString(), "YeniMesaj('Beklenmeyen bir hata oluştu.','Hata');", true);
                ScriptManager.RegisterStartupScript(this, this.GetType(), System.Guid.NewGuid().ToString(), "ShowMessage('Hata oluştu :'" + ex.Message.Replace("'", "") + ",'Hata','ruhsatislem');", true);
            }

            if (ConnectionState.Open == conn.State)
                conn.Close();

            listele_ruhsat();
        }


        //----------------------------------------//

        protected void btnkmzyukle_Click(object sender, EventArgs e)
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

        protected void grid_kmz_RowCommand(object sender, GridViewCommandEventArgs e)
        {

        }

        protected void grid_kmz_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {

        }

        void listele_kmz()
        {
            if (lblidcihaz.Text != "")
            {
                SqlCommand cmd;
                string sql = " ", sql1 = " ";


                sql = "SELECT RD.ID,RD.DOSYA_ADI FROM RUHSAT_DOSYALAR AS RD " +

                    " WHERE RD.ID > 0 AND RD.RUHSAT_ID= " + lblidcihaz.Text + " ";

                cmd = new SqlCommand(sql, conn);
                conn.Open();
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);
                grid_kmz.DataSource = dt;
                grid_kmz.DataBind();
                conn.Close();
                lblkmzsayisi.Text = " " + " Bu sayfada " + grid_metraj.Rows.Count.ToString() + " " + "adet kayıt bulunmuştur.";

            }
            else
            {
                grid_metraj.DataBind();
            }
        }

        void teminat_bolum_ac_kapat()
        {
            if (chc_nakit_teminat.Checked == true)
            {
                Panel_Teminat.Visible = true;
                //lbl_txt_teminata_konu_tutar.Visible = true;
                //txt_teminata_konu_tutar.Visible = true;
                //lbl_com_teminat_sec.Visible = true;
                //com_teminat_sec.Visible = true;
            }
            else
            {
                Panel_Teminat.Visible = false;
                //lbl_txt_teminata_konu_tutar.Visible = false;
                //txt_teminata_konu_tutar.Visible = false;
                //lbl_com_teminat_sec.Visible = false;
                //com_teminat_sec.Visible = false;
                txt_teminata_konu_tutar.Text = "0";
                com_teminat_sec.SelectedIndex = 0;
            }
        }

        protected void chc_nakit_teminat_CheckedChanged(object sender, EventArgs e)
        {
            lbl_nakit_teminat.Text = (chc_nakit_teminat.Checked == true) ? "Teminat" : "Nakit";
            teminat_bolum_ac_kapat();


        }


        protected void btnodemekaydet_Click(object sender, EventArgs e)
        {
            SqlCommand sorgu = new SqlCommand();
            sorgu.Connection = conn;
            conn.Open();

            string userid = Session["KULLANICI_ID"].ToString();

            sorgu.CommandText = " SELECT COUNT(*) FROM RUHSAT_ODEMELER WHERE RUHSAT_ID=@SAY1 ";
            sorgu.Parameters.AddWithValue("@SAY1", lblidcihaz.Text);
            int sayi = Convert.ToInt16(sorgu.ExecuteScalar());

            if (chc_nakit_teminat.Checked==true && com_teminat_sec.SelectedIndex==0)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), System.Guid.NewGuid().ToString(), "YeniMesaj('Teminat seçimi veya tutar girişi yapılmadı.','Hata');", true);
                ScriptManager.RegisterStartupScript(this, this.GetType(), System.Guid.NewGuid().ToString(), "ShowMessage('Teminat seçimi veya tutar girişi yapılmadı.','Hata','yeni');", true);
            }
            else
            {
                try
                {
                    if (sayi == 0)
                    {
                        sorgu.CommandText = "INSERT INTO    RUHSAT_ODEMELER (IL_ID, BOLGE_ID, RUHSAT_ID, KESIFMIKABULMU, TEMINAT_ID, ODEME_TIPI, TOPLAMADAHILMI, " +
                            " ALAN_TAHRIP_BEDELI, KESIF_KONTROL_BEDELI, ALTYAPI_RUHSAT_BEDELI, ALTYAPI_KAZI_IZIN_HARCI, KDV, NAKIT_TOPLAM, TEMINAT_TOPLAM, " +
                            " KAYIT_EDEN, KAYIT_TARIHI) " +
                            " VALUES(@IL_ID_K, @BOLGE_ID_K, @RUHSAT_ID_K, @KESIFMIKABULMU_K, @TEMINAT_ID_K, @ODEME_TIPI_K, @TOPLAMADAHILMI_K, @ALAN_TAHRIP_BEDELI_K, @KESIF_KONTROL_BEDELI_K, @ALTYAPI_RUHSAT_BEDELI_K, " +
                            " @ALTYAPI_KAZI_IZIN_HARCI_K, @KDV_K, @NAKIT_TOPLAM_K, @TEMINAT_TOPLAM_K, @UI_K, getdate()  ); SELECT @@IDENTITY AS 'Identity' ";
                        sorgu.Parameters.AddWithValue("@IL_ID_K", comil_kayit.SelectedValue);
                        sorgu.Parameters.AddWithValue("@BOLGE_ID_K", combolge_kayit.SelectedValue);
                        sorgu.Parameters.AddWithValue("@RUHSAT_ID_K", lblidcihaz.Text);
                        sorgu.Parameters.AddWithValue("@KESIFMIKABULMU_K", 0); //keşif=0 , kabul=1
                        sorgu.Parameters.AddWithValue("@TEMINAT_ID_K", com_teminat_sec.SelectedValue);
                        sorgu.Parameters.AddWithValue("@ODEME_TIPI_K", lbl_nakit_teminat.Text);
                        sorgu.Parameters.AddWithValue("@TOPLAMADAHILMI_K", 1); //keşif=1 , kabul=0
                        sorgu.Parameters.AddWithValue("@ALAN_TAHRIP_BEDELI_K", Convert.ToDecimal(txt_alan_tahrip_bedeli.Text));
                        sorgu.Parameters.AddWithValue("@KESIF_KONTROL_BEDELI_K", Convert.ToDecimal(txt_kesif_kontrol_bedeli.Text));
                        sorgu.Parameters.AddWithValue("@ALTYAPI_RUHSAT_BEDELI_K", Convert.ToDecimal(txt_altyapi_ruhsat_bedeli.Text));
                        sorgu.Parameters.AddWithValue("@ALTYAPI_KAZI_IZIN_HARCI_K", Convert.ToDecimal(txt_kazi_izin_harci.Text));
                        sorgu.Parameters.AddWithValue("@KDV_K", Convert.ToDecimal(txt_kdv.Text));
                        sorgu.Parameters.AddWithValue("@NAKIT_TOPLAM_K", Convert.ToDecimal(txt_toplam.Text));
                        sorgu.Parameters.AddWithValue("@TEMINAT_TOPLAM_K", Convert.ToDecimal(txt_teminata_konu_tutar.Text));
                        sorgu.Parameters.AddWithValue("@UI_K", userid);
                        string id_odeme = sorgu.ExecuteScalar().ToString();
                        lblidodeme.Text = id_odeme;

                        ScriptManager.RegisterStartupScript(this, this.GetType(), System.Guid.NewGuid().ToString(), "YeniMesaj('Ödeme bilgisi başarıyla kaydedilmiştir.','Tamam');", true);
                        ScriptManager.RegisterStartupScript(this, this.GetType(), System.Guid.NewGuid().ToString(), "ShowMessage('Ödeme bilgisi başarıyla kaydedilmiştir.','Tamam','yeni');", true);
                    }
                    else
                    {
                        // ------   UPDATE   ------ \\
                        sorgu.CommandText = "UPDATE RUHSAT_ODEMELER SET IL_ID=@IL_ID_U, BOLGE_ID=@BOLGE_ID_U, RUHSAT_ID=@RUHSAT_ID_U, TEMINAT_ID=@TEMINAT_ID_U, ODEME_TIPI=@ODEME_TIPI_U, " +
                            " ALAN_TAHRIP_BEDELI=@ALAN_TAHRIP_BEDELI_U, KESIF_KONTROL_BEDELI=@KESIF_KONTROL_BEDELI_U, ALTYAPI_RUHSAT_BEDELI=@ALTYAPI_RUHSAT_BEDELI_U, ALTYAPI_KAZI_IZIN_HARCI=@ALTYAPI_KAZI_IZIN_HARCI_U, KDV=@KDV_U, NAKIT_TOPLAM=@NAKIT_TOPLAM_U, " +
                            " TEMINAT_TOPLAM=@TEMINAT_TOPLAM_U WHERE RUHSAT_ID=@V_ID_U ";
                        sorgu.Parameters.AddWithValue("@IL_ID_U", comil_kayit.SelectedValue);
                        sorgu.Parameters.AddWithValue("@BOLGE_ID_U", combolge_kayit.SelectedValue);
                        sorgu.Parameters.AddWithValue("@RUHSAT_ID_U", lblidcihaz.Text);
                        sorgu.Parameters.AddWithValue("@TEMINAT_ID_U", com_teminat_sec.SelectedValue);
                        sorgu.Parameters.AddWithValue("@ODEME_TIPI_U", lbl_nakit_teminat.Text);
                        sorgu.Parameters.AddWithValue("@ALAN_TAHRIP_BEDELI_U", Convert.ToDecimal(txt_alan_tahrip_bedeli.Text));
                        sorgu.Parameters.AddWithValue("@KESIF_KONTROL_BEDELI_U", Convert.ToDecimal(txt_kesif_kontrol_bedeli.Text));
                        sorgu.Parameters.AddWithValue("@ALTYAPI_RUHSAT_BEDELI_U", Convert.ToDecimal(txt_altyapi_ruhsat_bedeli.Text));
                        sorgu.Parameters.AddWithValue("@ALTYAPI_KAZI_IZIN_HARCI_U", Convert.ToDecimal(txt_kazi_izin_harci.Text));
                        sorgu.Parameters.AddWithValue("@KDV_U", Convert.ToDecimal(txt_kdv.Text));
                        sorgu.Parameters.AddWithValue("@NAKIT_TOPLAM_U", Convert.ToDecimal(txt_toplam.Text));
                        sorgu.Parameters.AddWithValue("@TEMINAT_TOPLAM_U", Convert.ToDecimal(txt_teminata_konu_tutar.Text));
                        sorgu.Parameters.AddWithValue("@V_ID_U", lblidcihaz.Text);
                        sorgu.ExecuteNonQuery();
                        // ------   UPDATE   ------ \\

                        ScriptManager.RegisterStartupScript(this, this.GetType(), System.Guid.NewGuid().ToString(), "YeniMesaj('Ödeme bilgisi başarıyla güncellenmiştir.','Tamam');", true);
                        ScriptManager.RegisterStartupScript(this, this.GetType(), System.Guid.NewGuid().ToString(), "ShowMessage('Ödeme bilgisi başarıyla güncellenmiştir.','Tamam','yeni');", true);
                    }

                }
                catch (Exception ex)
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), System.Guid.NewGuid().ToString(), "YeniMesaj('Beklenmeyen bir hata oluştu.','Hata');", true);
                    ScriptManager.RegisterStartupScript(this, this.GetType(), System.Guid.NewGuid().ToString(), "ShowMessage('Hata oluştu :'" + ex.Message.Replace("'", "") + ",'Hata','yeni');", true);
                }
            }



            if (ConnectionState.Open == conn.State)
                conn.Close();

            listele_ruhsat();
        }

        protected void com_teminat_sec_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (com_teminat_sec.SelectedIndex == 0)
            {
                teminat_bolum_ac_kapat();
            }

            else
            {
                teminat_bolum_ac_kapat();
            }

        }


        //----------------------------------------//

        void ruhsat_bedel_topla()
        {
            decimal a = txt_alan_tahrip_bedeli.Text == "" ? Convert.ToDecimal(txt_alan_tahrip_bedeli.Text = "0") : Convert.ToDecimal(txt_alan_tahrip_bedeli.Text);
            decimal b = txt_kesif_kontrol_bedeli.Text == "" ? Convert.ToDecimal(txt_kesif_kontrol_bedeli.Text = "0") : Convert.ToDecimal(txt_kesif_kontrol_bedeli.Text);
            decimal c = txt_altyapi_ruhsat_bedeli.Text == "" ? Convert.ToDecimal(txt_altyapi_ruhsat_bedeli.Text = "0") : Convert.ToDecimal(txt_altyapi_ruhsat_bedeli.Text);
            decimal d = txt_kazi_izin_harci.Text == "" ? Convert.ToDecimal(txt_kazi_izin_harci.Text = "0") : Convert.ToDecimal(txt_kazi_izin_harci.Text);
            decimal e = txt_kdv.Text == "" ? Convert.ToDecimal(txt_kdv.Text = "0") : Convert.ToDecimal(txt_kdv.Text);
            decimal t = Convert.ToDecimal(a+b+c+d+e);
            txt_toplam.Text = t.ToString();


        }

        protected void txt_alan_tahrip_bedeli_TextChanged(object sender, EventArgs e)
        {
            ruhsat_bedel_topla();
        }

        protected void txt_kesif_kontrol_bedeli_TextChanged(object sender, EventArgs e)
        {
            ruhsat_bedel_topla();
        }

        protected void txt_altyapi_ruhsat_bedeli_TextChanged(object sender, EventArgs e)
        {
            ruhsat_bedel_topla();
        }

        protected void txt_kazi_izin_harci_TextChanged(object sender, EventArgs e)
        {
            ruhsat_bedel_topla();
        }

        protected void txt_kdv_TextChanged(object sender, EventArgs e)
        {
            ruhsat_bedel_topla();
        }

        protected void comaprojetipi_kayit_SelectedIndexChanged(object sender, EventArgs e)
        {
            deplase_ariza_mesaj();
        }























        //----------------------------------------//

    }
}