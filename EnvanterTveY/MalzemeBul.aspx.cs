using System;
using System.Web.UI.WebControls;


using System.Data;
using System.Data.SqlClient;
using System.Configuration;

using System.Drawing;

//ok
namespace EnvanterTveY
{
    public partial class MalzemeBul : System.Web.UI.Page
    {
        SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["KabloHE"].ConnectionString);
        string sayfa = "MalzemeBul";
        DataTable dt;
        KodT.kodlar tkod = new KodT.kodlar();
        bool tamirdepokontrol = false;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                //listele();
            }
        }
        protected void btnara_Click(object sender, EventArgs e)
        {
            listele();
        }

        void listele()
        {
            try
            {
                if (txtserinoara.Text != "")
                {
                    SqlCommand cmd;
                    string sql = "", sql1 = "";

                    sql = " SELECT M.ID, MALZEME_TIP.TIP, MALZEME_TURU.TURU, MALZEME_MARKAMODEL.MARKA, MALZEME_MODEL.MODEL, M.SERI_NO, MD.DURUM, " +
                        " BOLGE.BOLGE_ADI, DEPO.DEPO, M.GUNCELLEME_DURUMU, KULLANICI.ISIM + ' ' +CONVERT(NVARCHAR(10),M.KAYIT_TARIHI,104) AS KAYIT" +
                        " FROM MALZEMELER AS M" +
                        " LEFT JOIN MALZEME_DURUM AS MD ON MD.ID=M.DURUM" +
                        //" LEFT JOIN COMDOLDUR AS CD ON CD.DEGER=M.DURUM" +
                        " LEFT JOIN BOLGE ON M.BOLGE_ID=BOLGE.ID" +
                        " LEFT JOIN DEPO ON M.DEPO_ID=DEPO.ID" +
                        " LEFT JOIN KULLANICI ON KULLANICI.ID=M.KAYIT_EDEN" +
                        " LEFT JOIN MALZEME_TIP  ON MALZEME_TIP.ID=M.MALZEME_TIP" +
                        " LEFT JOIN MALZEME_TURU ON MALZEME_TURU.ID=M.MALZEME_TUR" +
                        " LEFT JOIN MALZEME_MARKAMODEL ON MALZEME_MARKAMODEL.ID=M.MARKA" +
                        " LEFT JOIN MALZEME_MODEL ON MALZEME_MODEL.ID=M.MODEL" +
                        " WHERE M.ID > 0  ";

                    if (txtserinoara.Text.Length > 0)
                        sql1 += " AND M.SERI_NO LIKE '%" + txtserinoara.Text + "%'  ";

                    sql = sql + sql1 + "   ORDER BY ID";

                    cmd = new SqlCommand(sql, conn);

                    conn.Open();

                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    conn.Close();

                    grid.DataSource = dt;
                    grid.DataBind();
                    lblmalzemesayisi.Text = grid.Rows.Count.ToString() + " " + "adet kayıt bulunmuştur.";
                }
            }
            catch (Exception ex)
            {
                lblmalzemesayisi.Text = ex.Message;
            }

        }

        void malzeme_log_listele(string malzemeid)

        {
            SqlCommand cmd;
            string sql = "", sql1 = "";

            if (malzemeid == "")
                malzemeid = "-1";

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
                " LEFT JOIN KULLANICI ON KULLANICI.ID=MLOG.KAYIT_EDEN WHERE MLOG.M_ID  = '" + malzemeid + "'  ";

            sql = sql + sql1 + "   ORDER BY MLOG.ID DESC";

            cmd = new SqlCommand(sql, conn);

            conn.Open();

            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            grid_malzeme_log.DataSource = dt;
            grid_malzeme_log.DataBind();
            conn.Close();
            lblhareketsayisi.Text = grid_malzeme_log.Rows.Count.ToString() + " " + "adet kayıt bulunmuştur.";
        }

        protected void grid_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName.Equals("sec"))
            {
                string id = Convert.ToString(e.CommandArgument);
                //string id = grid.DataKeys[index].Value.ToString();
                malzeme_log_listele(id);
            }
        }

        protected void grid_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowIndex > -1)
            {
                Button butonguncelle = (Button)e.Row.FindControl("btnguncelle");
                Button butonsil = (Button)e.Row.FindControl("btnsil");

                //////////////////////////
                try
                {
                    if (e.Row.Cells[7].Text == "ARIZALI")
                    {
                        e.Row.Cells[7].ForeColor = Color.SteelBlue;
                        e.Row.Cells[7].ToolTip = "Arızalı malzeme.";
                    }
                }
                catch (Exception ex)
                {
                    e.Row.Cells[7].ToolTip = "Hata oluştu : " + ex.Message;
                }
                //////////////////////////
                //////////////////////////
                try
                {
                    if (e.Row.Cells[9].Text == "KSAVİD TAMİR DEPO")
                    {
                        e.Row.Cells[9].BackColor = Color.Tomato;
                        e.Row.Cells[9].ToolTip = "Bu malzemenin Tamir Depo'da işlemi devam etmektedir.";
                    }
                    if (e.Row.Cells[9].Text == "TRON TAMİR DEPO")
                    {
                        e.Row.Cells[9].BackColor = Color.Salmon;
                        e.Row.Cells[9].ToolTip = "Bu malzemenin Tamir Depo'da işlemi devam etmektedir.";
                    }
                }
                catch (Exception ex)
                {
                    e.Row.Cells[7].ToolTip = "Hata oluştu : " + ex.Message;
                }
                //////////////////////////
                //////////////////////////
                if (e.Row.Cells[7].Text == "HURDA")
                {
                    e.Row.BackColor = Color.DimGray;
                }
                //////////////////////////
                tamirdepokontrol = Convert.ToBoolean(tkod.sql_calistir1(" SELECT ISNULL(TAMIRDEPO_KONTROL,'FALSE')  FROM MALZEMELER WHERE ID=" + e.Row.Cells[1].Text));
                if (tamirdepokontrol == true)
                    e.Row.BackColor = Color.LemonChiffon;
            }
        }

        protected void grid_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {

        }

        protected void grid_DataBound(object sender, EventArgs e)
        {
            if (grid.Rows.Count == 1)
                malzeme_log_listele(grid.Rows[0].Cells[1].Text);
        }
    }
}