using System;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Web.UI;

namespace EnvanterTveY
{
    public partial class RaporPort : System.Web.UI.Page
    {
        SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["KabloHE_Port"].ConnectionString);
        string sayfa = "RaporPort";
        DataTable dt;
        KodT.kodlar tkod = new KodT.kodlar();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                yil_doldur();
                panel_tarih.Visible = false;
                panel_raporlama.Visible = false;

                btnara.Enabled = false;


            }
        }

        void yil_doldur()
        {
            comrapor_yil_tarih.Items.Clear();
            comrapor_yil_tarih.Items.Insert(0, new ListItem("Seçiniz", "0"));
            comrapor_yil_tarih.AppendDataBoundItems = true;

            SqlDataAdapter da = new SqlDataAdapter();
            SqlCommand cmd = new SqlCommand("select year(tarih) as tarih from Port group by year(tarih) order by year(tarih) ");
            DataTable dt = new DataTable();
            da.SelectCommand = cmd;
            da.SelectCommand.Connection = conn;
            da.Fill(dt);
            comrapor_yil_tarih.DataSource = dt;
            comrapor_yil_tarih.DataTextField = "tarih";
            comrapor_yil_tarih.DataBind();
        }
        void ay_doldur()
        {
            comrapor_ay_tarih.Items.Clear();
            comrapor_ay_tarih.Items.Insert(0, new ListItem("Seçiniz", "0"));
            comrapor_ay_tarih.AppendDataBoundItems = true;
            string sql2 = "";

            if (comrapor_yil_tarih.SelectedIndex > 0)
                sql2 = "  year(tarih)='" + comrapor_yil_tarih.SelectedValue.ToString() + "' ";

            string sql = "select convert(char(2),tarih,101) as tarih from Port where  " + sql2 + " group by convert(char(2),tarih,101) order by convert(char(2),tarih,101)";
            comrapor_ay_tarih.DataTextField = "tarih";
            comrapor_ay_tarih.DataSource = tkod.GetData_port(sql);
            comrapor_ay_tarih.DataBind();
        }
        void gun_doldur()
        {
            comrapor_gun_tarih.Items.Clear();
            comrapor_gun_tarih.Items.Insert(0, new ListItem("Seçiniz", "0"));
            comrapor_gun_tarih.AppendDataBoundItems = true;
            string sql2 = "";
            string sql3 = "";

            if (comrapor_ay_tarih.SelectedIndex > 0)
                sql2 = "  MONTH(tarih)='" + comrapor_ay_tarih.SelectedValue.ToString() + "' ";
                sql3 = "  and year(tarih)='" + comrapor_yil_tarih.SelectedValue.ToString() + "' ";

            string sql = "select convert(char(2),tarih,104) as tarih from Port where  " + sql2 + sql3 + " group by convert(char(2),tarih,104) order by convert(char(2),tarih,104)";
            comrapor_gun_tarih.DataTextField = "tarih";
            comrapor_gun_tarih.DataSource = tkod.GetData_port(sql);
            comrapor_gun_tarih.DataBind();
        }
        void rapor_sec_doldur()
        {
            comrapor_raporsec_tarih.Items.Clear();
            comrapor_raporsec_tarih.Items.Insert(0, new ListItem("Rapor Seç", "0"));
            comrapor_raporsec_tarih.AppendDataBoundItems = true;
            string sql2 = "";

            if (comrapor_gun_tarih.SelectedIndex > 0)
                sql2 = "  '" + comrapor_yil_tarih.SelectedValue.ToString() + "-" + comrapor_ay_tarih.SelectedValue.ToString() + "-" + comrapor_gun_tarih.SelectedValue.ToString() + "%" + "' ";

            string sql = "SELECT tarih from Port where CONVERT(VARCHAR(25), tarih, 126) LIKE " + sql2 + " group by tarih order by tarih";
            comrapor_raporsec_tarih.DataTextField = "tarih";
            comrapor_raporsec_tarih.DataSource = tkod.GetData_port(sql);
            comrapor_raporsec_tarih.DataBind();
        }

        void cmts_sec_doldur()
        {
            if (comrapor_raporsec_tarih.SelectedIndex < 1)
            {
                comrapor_cmts.Items.Clear();
                comrapor_cmts.Items.Insert(0, new ListItem("CMTS Seç", "0"));
                comrapor_cmts.AppendDataBoundItems = true;

                string sql = "select distinct cmts from port group by cmts";
                comrapor_cmts.DataTextField = "cmts";
                comrapor_cmts.DataSource = tkod.GetData_port(sql);
                comrapor_cmts.DataBind();
            }
            else
            {
                comrapor_cmts.Items.Clear();
                comrapor_cmts.Items.Insert(0, new ListItem("CMTS Seç", "0"));
                comrapor_cmts.AppendDataBoundItems = true;
                string sql2 = "";

                if (comrapor_gun_tarih.SelectedIndex > 0)
                    sql2 = "  '" + comrapor_yil_tarih.SelectedValue.ToString() + "-" + comrapor_ay_tarih.SelectedValue.ToString() + "-" + comrapor_gun_tarih.SelectedValue.ToString() + "%" + "' ";

                string sql = "SELECT cmts from Port where CONVERT(VARCHAR(25), tarih, 126) LIKE " + sql2 + " group by cmts order by cmts";
                comrapor_cmts.DataTextField = "cmts";
                comrapor_cmts.DataSource = tkod.GetData_port(sql);
                comrapor_cmts.DataBind();
            }

        }
        void usport_doldur()
        {
            if (chc_port.Checked==true)
            {
                comusport_port.Items.Clear();
                comusport_port.Items.Insert(0, new ListItem("US Port Seç", "0"));
                comusport_port.AppendDataBoundItems = true;
                string sql2 = "";

                if (comrapor_cmts.SelectedIndex > 0)
                    sql2 = " CMTS= '" + comrapor_cmts.SelectedValue.ToString() + "' ";

                string sql = "select STUFF(LTRIM(RTRIM(substring(interface,CHARINDEX('-',interface,0),8))),1,1,'') from port where  " + sql2 + " group by STUFF(LTRIM(RTRIM(substring(interface,CHARINDEX('-',interface,0),8))),1,1,'') order by STUFF(LTRIM(RTRIM(substring(interface,CHARINDEX('-',interface,0),8))),1,1,'')";
                comusport_port.DataTextField = "interface";
                comusport_port.DataSource = tkod.GetData_port(sql);
                comusport_port.DataBind();
            }

        }

        protected void comrapor_yil_tarih_SelectedIndexChanged(object sender, EventArgs e)
        {
            ay_doldur();
        }

        protected void comrapor_ay_tarih_SelectedIndexChanged(object sender, EventArgs e)
        {
            gun_doldur();
        }

        protected void comrapor_gun_tarih_SelectedIndexChanged(object sender, EventArgs e)
        {
            rapor_sec_doldur();
        }

        protected void comrapor_raporsec_tarih_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (chc_cmts.Checked==true)
            {
                cmts_sec_doldur();
            }
            if (chc_port.Checked == true)
            {
                cmts_sec_doldur();
            }
            raporlama_sec();

        }
        protected void comrapor_cmts_SelectedIndexChanged(object sender, EventArgs e)
        {
            raporlama_sec();

            //if (chc_port.Checked==true)
            //{
            //    usport_doldur();
            //}
        }

        void raporlama_sec()
        {
            if (chc_tarih.Checked==true)
            {
                comraporlama_sec.Items.Clear();
                comraporlama_sec.Items.Insert(0, new ListItem("Seçiniz", "0"));
                comraporlama_sec.AppendDataBoundItems = true;

                SqlDataAdapter da = new SqlDataAdapter();
                SqlCommand cmd = new SqlCommand("Select ID,SECENEK FROM COMDOLDUR WHERE COM_ADI = 'comraporsec_tarih'  ORDER BY SIRA  ");
                DataTable dt = new DataTable();
                da.SelectCommand = cmd;
                da.SelectCommand.Connection = conn;
                da.Fill(dt);
                comraporlama_sec.DataSource = dt;
                comraporlama_sec.DataTextField = "SECENEK";
                comraporlama_sec.DataValueField = "ID";
                comraporlama_sec.DataBind();
            }
            if (chc_genel.Checked == true)
            {
                comraporlama_sec.Items.Clear();
                comraporlama_sec.Items.Insert(0, new ListItem("Seçiniz", "0"));
                comraporlama_sec.AppendDataBoundItems = true;

                SqlDataAdapter da = new SqlDataAdapter();
                SqlCommand cmd = new SqlCommand("Select ID,SECENEK FROM COMDOLDUR WHERE COM_ADI = 'comraporsec_genel'  ORDER BY SIRA  ");
                DataTable dt = new DataTable();
                da.SelectCommand = cmd;
                da.SelectCommand.Connection = conn;
                da.Fill(dt);
                comraporlama_sec.DataSource = dt;
                comraporlama_sec.DataTextField = "SECENEK";
                comraporlama_sec.DataValueField = "ID";
                comraporlama_sec.DataBind();
            }
            if (chc_cmts.Checked == true)
            {
                comraporlama_sec.Items.Clear();
                comraporlama_sec.Items.Insert(0, new ListItem("Seçiniz", "0"));
                comraporlama_sec.AppendDataBoundItems = true;

                SqlDataAdapter da = new SqlDataAdapter();
                SqlCommand cmd = new SqlCommand("Select ID,SECENEK FROM COMDOLDUR WHERE COM_ADI = 'comraporsec_cmts'  ORDER BY SIRA  ");
                DataTable dt = new DataTable();
                da.SelectCommand = cmd;
                da.SelectCommand.Connection = conn;
                da.Fill(dt);
                comraporlama_sec.DataSource = dt;
                comraporlama_sec.DataTextField = "SECENEK";
                comraporlama_sec.DataValueField = "ID";
                comraporlama_sec.DataBind();
            }
            if (chc_port.Checked == true)
            {
                comraporlama_sec.Items.Clear();
                comraporlama_sec.Items.Insert(0, new ListItem("Seçiniz", "0"));
                comraporlama_sec.AppendDataBoundItems = true;

                SqlDataAdapter da = new SqlDataAdapter();
                SqlCommand cmd = new SqlCommand("Select ID,SECENEK FROM COMDOLDUR WHERE COM_ADI = 'comraporsec_port'  ORDER BY SIRA  ");
                DataTable dt = new DataTable();
                da.SelectCommand = cmd;
                da.SelectCommand.Connection = conn;
                da.Fill(dt);
                comraporlama_sec.DataSource = dt;
                comraporlama_sec.DataTextField = "SECENEK";
                comraporlama_sec.DataValueField = "ID";
                comraporlama_sec.DataBind();
            }
            if (chc_mac.Checked == true)
            {
                comraporlama_sec.Items.Clear();
                comraporlama_sec.Items.Insert(0, new ListItem("Seçiniz", "0"));
                comraporlama_sec.AppendDataBoundItems = true;

                SqlDataAdapter da = new SqlDataAdapter();
                SqlCommand cmd = new SqlCommand("Select ID,SECENEK FROM COMDOLDUR WHERE COM_ADI = 'comraporsec_mac'  ORDER BY SIRA  ");
                DataTable dt = new DataTable();
                da.SelectCommand = cmd;
                da.SelectCommand.Connection = conn;
                da.Fill(dt);
                comraporlama_sec.DataSource = dt;
                comraporlama_sec.DataTextField = "SECENEK";
                comraporlama_sec.DataValueField = "ID";
                comraporlama_sec.DataBind();
            }
            if (chc_devreno.Checked == true)
            {
                comraporlama_sec.Items.Clear();
                comraporlama_sec.Items.Insert(0, new ListItem("Seçiniz", "0"));
                comraporlama_sec.AppendDataBoundItems = true;

                SqlDataAdapter da = new SqlDataAdapter();
                SqlCommand cmd = new SqlCommand("Select ID,SECENEK FROM COMDOLDUR WHERE COM_ADI = 'comraporsec_devre'  ORDER BY SIRA  ");
                DataTable dt = new DataTable();
                da.SelectCommand = cmd;
                da.SelectCommand.Connection = conn;
                da.Fill(dt);
                comraporlama_sec.DataSource = dt;
                comraporlama_sec.DataTextField = "SECENEK";
                comraporlama_sec.DataValueField = "ID";
                comraporlama_sec.DataBind();
            }
        }



        protected void btnara_Click(object sender, EventArgs e)
        {
            try
            {
                SqlCommand sorgu1 = new SqlCommand();
                sorgu1.Connection = conn;
                conn.Open();

                if (chc_tarih.Checked == true)
                {
                    string t = comrapor_raporsec_tarih.SelectedItem.ToString();
                    string s = DateTime.Parse(t).ToString("yyyy-MM-dd HH:mm:ss");

                    string sql = tkod.sql_calistir_port("Select DEGER FROM COMDOLDUR WHERE ID = '" + comraporlama_sec.SelectedValue + "' ");

                    string sql2 = sql.Replace("#", "tarih = '" + s.ToString() + "'");

                    GridView1.DataSource = tkod.GetData_port(sql2);
                    //GridView1.DataSource = tkod.GetData_port(sql + " ' " + s.ToString() + " ' " +  " group by tarih ");

                    GridView1.DataBind();
                    ScriptManager.RegisterStartupScript(this, this.GetType(), System.Guid.NewGuid().ToString(), "YeniMesaj('Tablo yüklenmiştir.'+' Bulunan kayıt sayısı: '+'" + GridView1.Rows.Count.ToString() + "','Tamam');", true);
                }

                if (chc_genel.Checked == true)
                {
                    string sql = tkod.sql_calistir_port("Select DEGER FROM COMDOLDUR WHERE ID = '" + comraporlama_sec.SelectedValue + "' ");

                    //string sql1 = "Select " + sql;
                    
                    SqlDataAdapter da = new SqlDataAdapter(sql, conn);
                    DataSet ds = new DataSet();
                    da.Fill(ds);

                    GridView1.DataSource = ds.Tables[0];
                    GridView1.DataBind();
                    conn.Close();
                    ScriptManager.RegisterStartupScript(this, this.GetType(), System.Guid.NewGuid().ToString(), "YeniMesaj('Tablo yüklenmiştir.'+' Bulunan kayıt sayısı: '+'" + GridView1.Rows.Count.ToString() + "','Tamam');", true);
                }
                if (chc_cmts.Checked == true)
                {
                    if (comrapor_raporsec_tarih.SelectedIndex < 1)
                    {
                        string sql = tkod.sql_calistir_port("Select DEGER FROM COMDOLDUR WHERE ID = '" + comraporlama_sec.SelectedValue + "' ");

                        string sql1 = sql.Replace("select", "Select top 2 ");
                        string sql2 = sql1.Replace("#", "");
                        string sql3 = sql2.Replace("$", " cmts= '" + comrapor_cmts.SelectedItem.ToString() + "'");

                        GridView1.DataSource = tkod.GetData_port(sql3);
                        //GridView1.DataSource = tkod.GetData_port(sql + " ' " + s.ToString() + " ' " +  " group by tarih ");

                        GridView1.DataBind();
                        ScriptManager.RegisterStartupScript(this, this.GetType(), System.Guid.NewGuid().ToString(), "YeniMesaj('Tablo yüklenmiştir.'+' Bulunan kayıt sayısı: '+'" + GridView1.Rows.Count.ToString() + "','Tamam');", true);
                    }
                    else
                    {
                        string t = comrapor_raporsec_tarih.SelectedItem.ToString();
                        string s = DateTime.Parse(t).ToString("yyyy-MM-dd HH:mm:ss");

                        string sql = tkod.sql_calistir_port("Select DEGER FROM COMDOLDUR WHERE ID = '" + comraporlama_sec.SelectedValue + "' ");

                        if (comrapor_cmts.SelectedIndex>0)
                        {
                            string sql2 = sql.Replace("#", "tarih= '" + s.ToString() + "'");
                            string sql3 = sql2.Replace("$", " and cmts= '" + comrapor_cmts.SelectedItem.ToString() + "'");
                            GridView1.DataSource = tkod.GetData_port(sql3);
                        }
                        else
                        {
                            string sql2 = sql.Replace("#", "tarih= '" + s.ToString() + "'");
                            string sql3 = sql2.Replace("$", "");
                            GridView1.DataSource = tkod.GetData_port(sql3);
                        }
                        //GridView1.DataSource = tkod.GetData_port(sql + " ' " + s.ToString() + " ' " +  " group by tarih ");

                        GridView1.DataBind();
                        ScriptManager.RegisterStartupScript(this, this.GetType(), System.Guid.NewGuid().ToString(), "YeniMesaj('Tablo yüklenmiştir.'+' Bulunan kayıt sayısı: '+'" + GridView1.Rows.Count.ToString() + "','Tamam');", true);
                    }
                }
                if (chc_port.Checked == true)
                {
                    if (comraporlama_sec.SelectedIndex>0 || comrapor_raporsec_tarih.SelectedIndex>0)
                    {
                        var us = txt_usport_port.Text;
                        var ds = txt_dsport_port.Text;

                        if (us != "" && ds != "")
                        {
                            string t = comrapor_raporsec_tarih.SelectedItem.ToString();
                            string s = DateTime.Parse(t).ToString("yyyy-MM-dd HH:mm:ss");

                            string sql = tkod.sql_calistir_port("Select DEGER FROM COMDOLDUR WHERE ID = '" + comraporlama_sec.SelectedValue + "' ");

                            string sql2 = sql.Replace("#", "'" + s.ToString() + "'");
                            string sql3 = sql2.Replace("$", " and cmts= '" + comrapor_cmts.SelectedItem.ToString() + "' ");
                            string sql4 = sql3.Replace("}", " and STUFF(LTRIM(RTRIM(substring(interface,CHARINDEX('-',interface,0),8))),1,1,'')= '" + txt_usport_port.Text + "' ");
                            string sql5 = sql4.Replace("{", " and substring(interface,0,CHARINDEX('-',interface,0)) LIKE '" + txt_dsport_port.Text + "%' ");

                            GridView1.DataSource = tkod.GetData_port(sql5);
                            //GridView1.DataSource = tkod.GetData_port(sql + " ' " + s.ToString() + " ' " +  " group by tarih ");

                            GridView1.DataBind();
                            ScriptManager.RegisterStartupScript(this, this.GetType(), System.Guid.NewGuid().ToString(), "YeniMesaj('Tablo yüklenmiştir.'+' Bulunan kayıt sayısı: '+'" + GridView1.Rows.Count.ToString() + "','Tamam');", true);
                        }
                        if (us == "" && ds != "")
                        {
                            string t = comrapor_raporsec_tarih.SelectedItem.ToString();
                            string s = DateTime.Parse(t).ToString("yyyy-MM-dd HH:mm:ss");

                            string sql = tkod.sql_calistir_port("Select DEGER FROM COMDOLDUR WHERE ID = '" + comraporlama_sec.SelectedValue + "' ");

                            string sql2 = sql.Replace("#", "'" + s.ToString() + "'");
                            string sql3 = sql2.Replace("$", " and cmts= '" + comrapor_cmts.SelectedItem.ToString() + "' ");
                            string sql4 = sql3.Replace("}", "");
                            string sql5 = sql4.Replace("{", " and substring(interface,0,CHARINDEX('-',interface,0)) LIKE '" + txt_dsport_port.Text + "%' ");

                            GridView1.DataSource = tkod.GetData_port(sql5);
                            //GridView1.DataSource = tkod.GetData_port(sql + " ' " + s.ToString() + " ' " +  " group by tarih ");

                            GridView1.DataBind();
                            ScriptManager.RegisterStartupScript(this, this.GetType(), System.Guid.NewGuid().ToString(), "YeniMesaj('Tablo yüklenmiştir.'+' Bulunan kayıt sayısı: '+'" + GridView1.Rows.Count.ToString() + "','Tamam');", true);
                        }
                        if (us != "" && ds == "")
                        {
                            string t = comrapor_raporsec_tarih.SelectedItem.ToString();
                            string s = DateTime.Parse(t).ToString("yyyy-MM-dd HH:mm:ss");

                            string sql = tkod.sql_calistir_port("Select DEGER FROM COMDOLDUR WHERE ID = '" + comraporlama_sec.SelectedValue + "' ");

                            string sql2 = sql.Replace("#", "'" + s.ToString() + "'");
                            string sql3 = sql2.Replace("$", " and cmts= '" + comrapor_cmts.SelectedItem.ToString() + "' ");
                            string sql4 = sql3.Replace("}", " and STUFF(LTRIM(RTRIM(substring(interface,CHARINDEX('-',interface,0),8))),1,1,'')= '" + txt_usport_port.Text + "' ");
                            string sql5 = sql4.Replace("{", "");

                            GridView1.DataSource = tkod.GetData_port(sql5);
                            //GridView1.DataSource = tkod.GetData_port(sql + " ' " + s.ToString() + " ' " +  " group by tarih ");

                            GridView1.DataBind();
                            ScriptManager.RegisterStartupScript(this, this.GetType(), System.Guid.NewGuid().ToString(), "YeniMesaj('Tablo yüklenmiştir.'+' Bulunan kayıt sayısı: '+'" + GridView1.Rows.Count.ToString() + "','Tamam');", true);
                        }
                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(this, this.GetType(), System.Guid.NewGuid().ToString(), "YeniMesaj('Rapor ve Raporlama seçeneklerini seçiniz..','Hata');", true);

                    }

                }
                if (chc_mac.Checked == true)
                {
                    if (comrapor_raporsec_tarih.SelectedIndex<1)
                    {

                        string sql = tkod.sql_calistir_port("Select DEGER FROM COMDOLDUR WHERE ID = '" + comraporlama_sec.SelectedValue + "' ");

                        string sql1 = sql.Replace("select", "Select top 1 ");
                        string sql2 = sql1.Replace("#", "");
                        string sql3 = sql2.Replace("$", " MACAdresi= '" + txt_mac.Text + "'");

                        GridView1.DataSource = tkod.GetData_port(sql3);
                        //GridView1.DataSource = tkod.GetData_port(sql + " ' " + s.ToString() + " ' " +  " group by tarih ");

                        GridView1.DataBind();
                        ScriptManager.RegisterStartupScript(this, this.GetType(), System.Guid.NewGuid().ToString(), "YeniMesaj('Tablo yüklenmiştir.'+' Bulunan kayıt sayısı: '+'" + GridView1.Rows.Count.ToString() + "','Tamam');", true);
                    }
                    else
                    {
                        string t = comrapor_raporsec_tarih.SelectedItem.ToString();
                        string s = DateTime.Parse(t).ToString("yyyy-MM-dd HH:mm:ss");

                        string sql = tkod.sql_calistir_port("Select DEGER FROM COMDOLDUR WHERE ID = '" + comraporlama_sec.SelectedValue + "' ");

                        string sql2 = sql.Replace("#", "tarih=  '" + s.ToString() + "'");
                        string sql3 = sql2.Replace("$", " and MACAdresi= '" + txt_mac.Text + "'");

                        GridView1.DataSource = tkod.GetData_port(sql3);
                        //GridView1.DataSource = tkod.GetData_port(sql + " ' " + s.ToString() + " ' " +  " group by tarih ");

                        GridView1.DataBind();
                        ScriptManager.RegisterStartupScript(this, this.GetType(), System.Guid.NewGuid().ToString(), "YeniMesaj('Tablo yüklenmiştir.'+' Bulunan kayıt sayısı: '+'" + GridView1.Rows.Count.ToString() + "','Tamam');", true);
                    }

                }
                if (chc_devreno.Checked == true)
                {
                    if (comrapor_raporsec_tarih.SelectedIndex < 1)
                    {

                        string sql = tkod.sql_calistir_port("Select DEGER FROM COMDOLDUR WHERE ID = '" + comraporlama_sec.SelectedValue + "' ");

                        string sql1 = sql.Replace("select", "Select max(tarih), ");
                        string sql2 = sql1.Replace("#", "");
                        string sql3 = sql2.Replace("$", " LIKE '%" + txt_devreno.Text + "%'");
                        //string sql4 = sql3.Replace("{", "  group by tarih, CMTS, il+'.'+bolge+'.'+fn+'.'+anfi+'.'+Tapoff+'.'+kol");


                        GridView1.DataSource = tkod.GetData_port(sql3);
                        //GridView1.DataSource = tkod.GetData_port(sql + " ' " + s.ToString() + " ' " +  " group by tarih ");

                        GridView1.DataBind();
                        ScriptManager.RegisterStartupScript(this, this.GetType(), System.Guid.NewGuid().ToString(), "YeniMesaj('Tablo yüklenmiştir.'+' Bulunan kayıt sayısı: '+'" + GridView1.Rows.Count.ToString() + "','Tamam');", true);
                    }
                    else
                    {
                        string t = comrapor_raporsec_tarih.SelectedItem.ToString();
                        string s = DateTime.Parse(t).ToString("yyyy-MM-dd HH:mm:ss");

                        string sql = tkod.sql_calistir_port("Select DEGER FROM COMDOLDUR WHERE ID = '" + comraporlama_sec.SelectedValue + "' ");

                        string sql2 = sql.Replace("#", "tarih=  '" + s.ToString() + "' and");
                        string sql3 = sql2.Replace("$", " LIKE '%" + txt_devreno.Text + "%'");

                        GridView1.DataSource = tkod.GetData_port(sql3);
                        //GridView1.DataSource = tkod.GetData_port(sql + " ' " + s.ToString() + " ' " +  " group by tarih ");

                        GridView1.DataBind();
                        ScriptManager.RegisterStartupScript(this, this.GetType(), System.Guid.NewGuid().ToString(), "YeniMesaj('Tablo yüklenmiştir.'+' Bulunan kayıt sayısı: '+'" + GridView1.Rows.Count.ToString() + "','Tamam');", true);
                    }

                }

                Label2.Text = "Toplam " + GridView1.Rows.Count.ToString() + " adet kayıt bulunmuştur.";

            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), System.Guid.NewGuid().ToString(), "YeniMesaj('Komut işletme sırasında hata oluştu.','Hata');", true);
            }
        }

        protected void grid_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {

        }

                     
        protected void chc_tarih_CheckedChanged(object sender, EventArgs e)
        {
            if (chc_tarih.Checked==true)
            {
                panel_tarih.Visible = true;
                panel_port.Visible = false;
                panel_devreno.Visible = false;
                panel_deger.Visible = false;
                panel_mac.Visible = false;
                panel_cmts.Visible = false;
                panel_raporlama.Visible = true;

                //chc_tarih.Checked == true
                chc_genel.Checked = false;
                chc_cmts.Checked = false;
                chc_port.Checked = false;
                chc_mac.Checked = false;
                chc_devreno.Checked = false;
                chc_deger.Checked = false;
                lblaciklama.Text = "";
            }
            else
            {
                panel_tarih.Visible = false;
                panel_raporlama.Visible = false;
            }

            raporlama_sec();

        }
        protected void chc_genel_CheckedChanged(object sender, EventArgs e)
        {
            if (chc_genel.Checked == true)
            {
                panel_tarih.Visible = false;
                panel_port.Visible = false;
                panel_devreno.Visible = false;
                panel_deger.Visible = false;
                panel_mac.Visible = false;
                panel_cmts.Visible = false;
                panel_raporlama.Visible = true;

                chc_tarih.Checked = false;
                //chc_genel.Checked = false;
                chc_cmts.Checked = false;
                chc_port.Checked = false;
                chc_mac.Checked = false;
                chc_devreno.Checked = false;
                chc_deger.Checked = false;
            }
            else
            {
                panel_raporlama.Visible = false;
            }


            raporlama_sec();

        }
        protected void chc_cmts_CheckedChanged(object sender, EventArgs e)
        {
            if (chc_cmts.Checked == true)
            {
                panel_tarih.Visible = true;
                panel_port.Visible = false;
                panel_devreno.Visible = false;
                panel_deger.Visible = false;
                panel_mac.Visible = false;
                panel_cmts.Visible = true;
                panel_raporlama.Visible = true;

                chc_tarih.Checked = false;
                chc_genel.Checked = false;
                //chc_cmts.Checked = false;
                chc_port.Checked = false;
                chc_mac.Checked = false;
                chc_devreno.Checked = false;
                chc_deger.Checked = false;
            }
            else
            {
                panel_raporlama.Visible = false;
                panel_tarih.Visible = false;
                panel_cmts.Visible = false;

            }


            raporlama_sec();
            cmts_sec_doldur();
        }
        protected void chc_port_CheckedChanged(object sender, EventArgs e)
        {
            if (chc_port.Checked == true)
            {
                panel_tarih.Visible = true;
                panel_port.Visible = true;
                panel_devreno.Visible = false;
                panel_deger.Visible = false;
                panel_mac.Visible = false;
                panel_cmts.Visible = true;
                panel_raporlama.Visible = true;

                chc_tarih.Checked = false;
                chc_genel.Checked = false;
                chc_cmts.Checked = false;
                //chc_port.Checked = false;
                chc_mac.Checked = false;
                chc_devreno.Checked = false;
                chc_deger.Checked = false;
            }
            else
            {
                panel_raporlama.Visible = false;
                panel_tarih.Visible = false;
                panel_port.Visible = false;
                panel_cmts.Visible = false;

            }

            cmts_sec_doldur();
            raporlama_sec();
        }
        protected void chc_mac_CheckedChanged(object sender, EventArgs e)
        {
            if (chc_mac.Checked == true)
            {
                panel_tarih.Visible = true;
                panel_port.Visible = false;
                panel_devreno.Visible = false;
                panel_deger.Visible = false;
                panel_mac.Visible = true;
                panel_cmts.Visible = false;
                panel_raporlama.Visible = true;

                chc_tarih.Checked = false;
                chc_genel.Checked = false;
                chc_cmts.Checked = false;
                chc_port.Checked = false;
                //chc_mac.Checked = false;
                chc_devreno.Checked = false;
                chc_deger.Checked = false;
            }
            else
            {
                panel_tarih.Visible = false;
                panel_raporlama.Visible = false;
                panel_mac.Visible = false;
            }


            raporlama_sec();
        }
        protected void chc_devreno_CheckedChanged(object sender, EventArgs e)
        {
            if (chc_devreno.Checked == true)
            {
                panel_tarih.Visible = true;
                panel_port.Visible = false;
                panel_devreno.Visible = true;
                panel_deger.Visible = false;
                panel_mac.Visible = false;
                panel_cmts.Visible = false;
                panel_raporlama.Visible = true;

                chc_tarih.Checked = false;
                chc_genel.Checked = false;
                chc_cmts.Checked = false;
                chc_port.Checked = false;
                chc_mac.Checked = false;
                //chc_devreno.Checked = false;
                chc_deger.Checked = false;
            }
            else
            {
                panel_tarih.Visible = false;
                panel_raporlama.Visible = false;
                panel_devreno.Visible = false;
            }


            raporlama_sec();
        }
        protected void chc_deger_CheckedChanged(object sender, EventArgs e)
        {
            if (chc_deger.Checked == true)
            {
                panel_tarih.Visible = true;
                panel_port.Visible = false;
                panel_devreno.Visible = false;
                panel_deger.Visible = true;
                panel_mac.Visible = false;
                panel_cmts.Visible = true;
                panel_raporlama.Visible = true;

                chc_tarih.Checked = false;
                chc_genel.Checked = false;
                chc_cmts.Checked = false;
                chc_port.Checked = false;
                chc_mac.Checked = false;
                chc_devreno.Checked = false;
                //chc_deger.Checked = false;
            }
            else
            {
                panel_tarih.Visible = false;
                panel_deger.Visible = false;
                panel_cmts.Visible = false;
                panel_raporlama.Visible = false;
            }


            raporlama_sec();
        }





        protected void comraporlama_sec_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comraporlama_sec.SelectedIndex>0)
            {
                btnara.Enabled = true;
            }
            else
            {
                btnara.Enabled = false;
            }
        }

        protected void test_Click(object sender, EventArgs e)
        {
            panel_tarih.Controls.Add(new TextBox());
            panel_tarih.Controls.Add(new System.Web.UI.HtmlControls.HtmlButton());
        }


    }
}