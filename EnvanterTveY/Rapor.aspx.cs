using System;
using System.Web.UI.WebControls;


using System.Data;
using System.Data.SqlClient;
using System.Configuration;

using System.Drawing;
using System.Web.UI;

namespace EnvanterTveY
{
    public partial class Rapor : System.Web.UI.Page
    {
        SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["KabloHE"].ConnectionString);
        string sayfa = "Rapor";
        DataTable dt;
        KodT.kodlar tkod = new KodT.kodlar();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Durum_Ara_Listele();
            }
        }
        protected void btnara_Click(object sender, EventArgs e)
        {
            lblsql.Text = tkod.sql_calistir1("Select DEGER FROM COMDOLDUR WHERE SECENEK = '" + comraporsec.SelectedItem + "' ");

            try
            {
                string sql1 = "Select "  + lblsql.Text;

                conn.Open();
                SqlCommand sorgu = new SqlCommand();
                sorgu.Connection = conn;

                SqlDataAdapter da = new SqlDataAdapter( sql1, conn);
                DataSet ds = new DataSet();
                da.Fill(ds);

                GridView1.DataSource = ds.Tables[0];
                GridView1.DataBind();
                conn.Close();
                ScriptManager.RegisterStartupScript(this, this.GetType(), System.Guid.NewGuid().ToString(), "YeniMesaj('Tablo yüklenmiştir.','Tamam');", true);

                Label2.Text = "Toplam " + GridView1.Rows.Count.ToString() + " adet kayıt bulunmuştur.";

            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), System.Guid.NewGuid().ToString(), "YeniMesaj('Komut işletme sırasında hata oluştu.','Hata');", true);
            }
        }

        void Durum_Ara_Listele()
        {
            comraporsec.Items.Clear();
            comraporsec.Items.Insert(0, new ListItem("Seçiniz", "0"));
            comraporsec.AppendDataBoundItems = true;

            SqlDataAdapter da = new SqlDataAdapter();
            SqlCommand cmd = new SqlCommand("Select DEGER AS ID,SECENEK FROM COMDOLDUR WHERE COM_ADI = 'comraporsec'  ORDER BY SIRA  ");
            DataTable dt = new DataTable();
            da.SelectCommand = cmd;
            da.SelectCommand.Connection = conn;
            da.Fill(dt);
            comraporsec.DataSource = dt;
            comraporsec.DataTextField = "SECENEK";
            comraporsec.DataValueField = "ID";
            comraporsec.DataBind();
        }

        protected void grid_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {

        }

        protected void comraporsec_SelectedIndexChanged(object sender, EventArgs e)
        {
            //lblsql.Text = tkod.sql_calistir1("Select DEGER FROM COMDOLDUR WHERE SECENEK = '" + comraporsec.SelectedItem+ "' ");
        }
    }
}