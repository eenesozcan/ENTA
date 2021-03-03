using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using System.IO;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;

namespace EnvanterTveY
{
    public partial class MenuYonetimi : System.Web.UI.Page
    {
        string sayfa = "MenuYonetimi";
        private SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["KabloHE"].ConnectionString);
        KodT.kodlar tkod = new KodT.kodlar();

        protected void Page_Load(object sender, EventArgs e)
        {

            if (Session["KULLANICI_ID"] == null)
            {
                Response.Redirect("Giris.aspx?url=" + sayfa);
            }
            else
            {

                if (!IsPostBack)
                {


                    if (yetki_kontrol() == "ADMIN")
                    {
                        com_hazirla();
                        //grid_menu();
                        agac_yukle();
                        DataTable dt = this.GetData("SELECT ID, SIRA, MENU_ISMI FROM MENU where ANA_MENU_ID='0' ORDER BY SIRA");
                        this.PopulateTreeView(dt, 0, null);

                    }
                    else
                        Response.Redirect("Default.aspx");
                }
            }

            grid_menu();
            if (!IsPostBack)
            {


            }
        }

        void agac_yukle()
        {


        }

        private void BindData()
        {
            //grid.DataSource = Session["dt-menu"];
            //grid.DataBind();
        }

        public string yetki_kontrol()
        {

            SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["KabloHE"].ConnectionString);
            conn.Open();
            SqlCommand sorgu = new SqlCommand();
            sorgu.Connection = conn;
            sorgu.CommandText = "select R.ROL FROM KULLANICI AS K INNER JOIN ROL AS R ON R.ID=K.ROL_ID WHERE K.ID='" + Session["KULLANICI_ID"].ToString() + "'  ";
            string yetki = Convert.ToString(sorgu.ExecuteScalar());
            conn.Close();
            return yetki;
        }

        protected void grid_menu()
        {
            conn.Open();
            string komut = "";
            komut = "SELECT * FROM MENU ORDER BY ANA_MENU_ID ASC, SIRA ASC ";

            SqlCommand cmd = new SqlCommand(komut, conn);

            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            da.Fill(ds);
            conn.Close();
            DataTable dt = new DataTable();
            dt = ds.Tables[0];
            Session["dt-menu"] = dt;
            BindData();

            /*
            if (ds.Tables[0].Rows.Count > 0)
            {
                gridekip.DataSource = ds;
                gridekip.DataBind();
            }
            else
            {
                ds.Tables[0].Rows.Add(ds.Tables[0].NewRow());
                gridekip.DataSource = ds;
                gridekip.DataBind();
                int columncount = gridekip.Rows[0].Cells.Count;
                gridekip.Rows[0].Cells.Clear();
                gridekip.Rows[0].Cells.Add(new TableCell());
                gridekip.Rows[0].Cells[0].ColumnSpan = columncount;
                gridekip.Rows[0].Cells[0].Text = "No Records Found";
            }
             * */


        }

        protected void btnekle_Click(object sender, EventArgs e)
        {

            if (Session["KULLANICI_ID"] == null)
            {
                Response.Redirect("Default.aspx?url=" + sayfa);
            }
            else
            {


                try
                {
                    SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["KabloHE"].ConnectionString);
                    conn.Open();
                    SqlCommand sorgu = new SqlCommand();
                    sorgu.Connection = conn;



                    if (lblid.Text == "")
                    {
                        sorgu.CommandText = "insert INTO MENU(MENU_ISMI,LINK,ANA_MENU_ID,SIRA,BAKIM,SVG)  VALUES(   @MENU_ISMI, @LINK, @ANA_MENU_ID, @SIRA, @BAKIM,@SVG )";

                    }
                    else
                    {
                        sorgu.CommandText = "Update MENU  set MENU_ISMI=@MENU_ISMI ,LINK=@LINK , ANA_MENU_ID=@ANA_MENU_ID ,SIRA=@SIRA ,BAKIM=@BAKIM WHERE ID=@ID ";
                    }
                    //sorgu.CommandText = "UPDATE KULLANICI SET SIFRE='" + txtsifre.Text + "' WHERE ID='" + grid.SelectedRow.Cells[1].Text + "'";

                    //,,,,
                    sorgu.Parameters.AddWithValue("MENU_ISMI", txtmenuismi.Text);
                    sorgu.Parameters.AddWithValue("LINK", txtlink.Text);
                    sorgu.Parameters.AddWithValue("ANA_MENU_ID", comanamenu.SelectedValue.ToString());
                    sorgu.Parameters.AddWithValue("SIRA", txtsira.Text);
                    sorgu.Parameters.AddWithValue("BAKIM", chcbakim.Checked);
                    sorgu.Parameters.AddWithValue("ID", lblid.Text);
                    sorgu.Parameters.AddWithValue("SVG", txtsvg.Text);




                    sorgu.ExecuteNonQuery();
                    conn.Close();
                    //lbldurum.Text = "Menu başarıyla Kaydedilmiştir.";
                    mesaj_yaz("Tamam", "Menü başarıyla eklenmiştir.", "menu");

                    grid_menu();
                }
                catch (Exception ex)
                {
                    //lbldurum.Text = "Kayıt sırasında hata oluştu. " + ex.Message;
                    mesaj_yaz("Hata", "Kayıt sırasında hata oluştu. " + ex.Message.Replace("'", ""), "menu");

                }
            }


            agac_yukle();
        }





        protected void btniptal_Click(object sender, EventArgs e)
        {
            txtlink.Text = "";
            txtmenuismi.Text = "";
            txtsira.Text = "";
            lblid.Text = "";
            btnsil.Visible = false;
        }

        void com_hazirla()
        {
            comanamenu.Items.Clear();
            comanamenu.Items.Insert(0, new ListItem("ANA_MENU", "0"));
            //comil2.Items.Add("", "");
            comanamenu.AppendDataBoundItems = true;
            SqlDataSource1.SelectCommand = "SELECT MENU_ISMI, ID FROM MENU WHERE ANA_MENU_ID='0'";
            conn.Open();
            //SqlDataSource1.SelectCommand = "SELECT IL, ID FROM IL ";
            SqlDataSource1.DataBind();
            conn.Close();
        }

        protected void ListBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        protected void TreeView1_SelectedNodeChanged(object sender, EventArgs e)
        {
            conn.Open();
            SqlCommand sorgu = new SqlCommand();
            sorgu.Connection = conn;
            string sql = "SELECT * FROM MENU WHERE ID=" + TreeView1.SelectedValue;
            SqlDataAdapter da = new SqlDataAdapter(sql, conn);
            DataSet ds = new DataSet();
            da.Fill(ds);

            if (ds.Tables[0].Rows.Count == 1)
            {
                txtmenuismi.Text = ds.Tables[0].Rows[0][1].ToString();
                txtlink.Text = ds.Tables[0].Rows[0][3].ToString();
                txtsira.Text = ds.Tables[0].Rows[0][5].ToString();
                //comanamenu.Items.FindByValue(ds.Tables[0].Rows[0][2].ToString()).Selected = true;
                lblid.Text = ds.Tables[0].Rows[0][0].ToString();
                txtsvg.Text = ds.Tables[0].Rows[0][8].ToString();
                //txtsolsira.Text = ds.Tables[0].Rows[0][6].ToString();
                if (ds.Tables[0].Rows[0][7].ToString().ToLower() == "true")
                    chcbakim.Checked = true;
                else
                    chcbakim.Checked = false;

                comanamenu.SelectedIndex = comanamenu.Items.IndexOf(comanamenu.Items.FindByValue(ds.Tables[0].Rows[0][2].ToString()));
                btnsil.Visible = true;
            }





        }

        private void PopulateTreeView(DataTable dtParent, int parentId, TreeNode treeNode)
        {
            foreach (DataRow row in dtParent.Rows)
            {
                TreeNode child = new TreeNode
                {
                    Text = row["SIRA"].ToString() + " - " + row["MENU_ISMI"].ToString(),
                    Value = row["ID"].ToString()

                };
                if (parentId == 0)
                {
                    TreeView1.Nodes.Add(child);
                    DataTable dtChild = this.GetData("SELECT ID, SIRA,MENU_ISMI  FROM MENU WHERE ANA_MENU_ID = " + child.Value);
                    PopulateTreeView(dtChild, int.Parse(child.Value), child);
                }
                else
                {
                    treeNode.ChildNodes.Add(child);
                }
            }
        }

        private DataTable GetData(string query)
        {
            DataTable dt = new DataTable();
            string constr = ConfigurationManager.ConnectionStrings["KabloHE"].ConnectionString;
            using (SqlConnection con = new SqlConnection(constr))
            {
                using (SqlCommand cmd = new SqlCommand(query))
                {
                    using (SqlDataAdapter sda = new SqlDataAdapter())
                    {
                        cmd.CommandType = CommandType.Text;
                        cmd.Connection = con;
                        sda.SelectCommand = cmd;
                        sda.Fill(dt);
                    }
                }
                return dt;
            }
        }

        protected void btnsil_Click(object sender, EventArgs e)
        {
            if (Session["KULLANICI_ID"] == null)
            {
                Response.Redirect("Default.aspx?url=" + sayfa);
            }
            else
            {


                try
                {
                    SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["KabloHE"].ConnectionString);
                    conn.Open();
                    SqlCommand sorgu = new SqlCommand();
                    sorgu.Connection = conn;
                    if (lblid.Text != "")
                    {
                        sorgu.CommandText = "DELETE MENU  WHERE ID=@ID  ";
                        sorgu.Parameters.AddWithValue("ID", lblid.Text);

                    }
                    sorgu.ExecuteNonQuery();
                    conn.Close();
                    //lbldurum.Text = "Menu başarıyla silinmiştir.";
                    mesaj_yaz("Hata", "Menü başarıyla silinmiştir.", "menu");
                    btnsil.Visible = false;
                    grid_menu();
                    agac_yukle();
                }
                catch (Exception ex)
                {
                    //lbldurum.Text = "Kayıt sırasında hata oluştu. " + ex.Message;
                    mesaj_yaz("Hata", "Silme işlemi sırasında hata oluştu. " + ex.Message.Replace("'", ""), "menu");

                }
            }
        }

        void mesaj_yaz(string hata, string mesaj, string func)
        {

            ScriptManager.RegisterStartupScript(this, this.GetType(), System.Guid.NewGuid().ToString(), "ShowMessage('" + mesaj + "','" + hata + "','" + func + "');", true);

        }
    }
}