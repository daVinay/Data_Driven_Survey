using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

//extras
using System.Data.SqlClient;
using System.Configuration;
using System.Data;
using System.Text.RegularExpressions;
using System.Text;

namespace AssesDataDriven
{
    public partial class StaffSearchPage : System.Web.UI.Page
    {
        public DropDownList DD
        {
            get;
            set;
        }

        SqlConnection connection;
        SqlCommand command;

        //testConnectionString from webconfig
        string connectionString = ConfigurationManager.ConnectionStrings["VinayConnectionString"].ConnectionString;

        protected void Page_Load(object sender, EventArgs e)
        {
            //get current question from DB


            connection = new SqlConnection();
            connection.ConnectionString = connectionString;

            connection.Open();//open the sql connection using the connection string info

            command = new SqlCommand("SELECT * FROM QuestionTable WHERE questionId =");

            if (!IsPostBack)
            {
                BindGrid();

            }

            //DD ageDD = new DD

        }

        protected void BtnShowAll_Clicked(object sender, EventArgs e)
        {
            BindGrid();
        }

        protected void BtnSearch_Click(object sender, EventArgs e)
        {
            int count = -1;
            StringBuilder searchQuery = new StringBuilder("select * from usertable where userid in ( select userid from useranswertable where answertext in (");
            if (Regex.IsMatch(DropDownAgeRange.SelectedItem.Value, @"^\d+$"))
            {
                count = count + 1;
                searchQuery.Append("'" + DropDownAgeRange.SelectedItem.Text + "',");

            }
            if (Regex.IsMatch(DropDownGender.SelectedItem.Value, @"^\d+$"))
            {
                count = count + 1;
                searchQuery.Append("'" + DropDownGender.SelectedItem.Text + "',");
            }
            if (Regex.IsMatch(DropDownBanks.SelectedItem.Value, @"^\d+$"))
            {
                count = count + 1;
                searchQuery.Append("'" + DropDownBanks.SelectedItem.Text + "',");
            }
            if (Regex.IsMatch(DropDownNewspaper.SelectedItem.Value, @"^\d+$"))
            {
                count = count + 1;
                searchQuery.Append("'" + DropDownNewspaper.SelectedItem.Text + "',");
            }

            if (!string.IsNullOrEmpty(TextBox3.Text))
            {
                count = count + 1;
                searchQuery.Append("'" + TextBox3.Text.Trim() + "',");
            }


            searchQuery.Append(") group by userid having count (*) > " + count + ")");
            searchQuery.Replace(",)", ")");

            BindGrid(searchQuery.ToString());


        }

        protected void BtnClearAll_Click(object sender, EventArgs e)
        {
            Response.Redirect(Request.RawUrl); //refresh page
        }


        private void BindGrid(string searchQuery = "")
        {

            DataTable dt = new DataTable();
            SqlConnection connection = new SqlConnection(connectionString);

            //reference from stack overflow
            try
            {
                string sqlStatement;
                connection.Open();
                if (searchQuery == "")
                    sqlStatement = "SELECT * FROM UserTable";
                else
                    sqlStatement = searchQuery;


                SqlCommand sqlCmd = new SqlCommand(sqlStatement, connection);
                SqlDataAdapter sqlDa = new SqlDataAdapter(sqlCmd);
                sqlDa.Fill(dt);

                if (dt.Rows.Count > 0)
                {
                    GridView1.DataSource = dt;
                    GridView1.DataBind();
                }
                else
                {
                    GridView1.DataSource = null;
                    GridView1.DataBind();

                    dt.Rows.Add(dt.NewRow());
                    GridView1.DataSource = dt;
                    GridView1.DataBind();
                    GridView1.Rows[0].Visible = false;
                }
            }
            catch (System.Data.SqlClient.SqlException ex)
            {
                string msg = "Fetch Error:";
                msg += ex.Message;
                throw new Exception(msg);
            }
            finally
            {
                connection.Close();
            }

        }



    }
}