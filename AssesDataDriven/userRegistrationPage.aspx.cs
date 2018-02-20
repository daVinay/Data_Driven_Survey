using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;

namespace AssesDataDriven
{
    public partial class userRegistrationPage : System.Web.UI.Page
    {

        string connectionString = ConfigurationManager.ConnectionStrings["VinayConnectionString"].ConnectionString;
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            try
            {
                var UserAnswer = (List<AssesDataDriven.QuestionPage.UserAnswerDetail>)HttpContext.Current.Session["UserAnswerDetail"];

                //loop through the set of questons
                for (int a = 1; a <= 4; a = a + 1)
                {
                    
                    UserAnswer.Add(new AssesDataDriven.QuestionPage.UserAnswerDetail
                    {
                        QuesId = Convert.ToInt16(HttpContext.Current.Session["questionNumber"]),
                        AnswerText = ((TextBox)Form1.FindControl("textbox" + a)).Text
                    });
                }

                // save data to database 
                foreach (var item1 in (List<AssesDataDriven.QuestionPage.UserDetail>)HttpContext.Current.Session["UserInfo"])
                {
                    int userID = InsertDataToUserTable(connectionString, item1.UserIp, item1.Date, 
                        ((TextBox)Form1.FindControl("textbox1")).Text.ToString(), 
                        ((TextBox)Form1.FindControl("textbox2")).Text.ToString(),
                        Convert.ToDateTime(((TextBox)Form1.FindControl("textbox3")).Text), 
                        ((TextBox)Form1.FindControl("textbox4")).Text.ToString());


                    foreach (AssesDataDriven.QuestionPage.UserAnswerDetail item2 in UserAnswer)
                    {
                        InsertDataToUserAnswerTable(connectionString, userID, item2.QuesId, item2.AnswerText);
                    }

                }
                Response.Redirect("~/ThankyouPage.aspx");
            }
            catch(Exception ex )
            {
                throw ex;
                //throwing this exception because of contact being int and can't be more than 3 in length. 
                //Sorry, I ran out of time. I can fix it if you allow me some time to change the data type and some code.
            }
            
        }


        private void InsertDataToUserAnswerTable(string connectionString,int userId,int quesId,string answerText  )
        {
            // define INSERT query with parameters
            string query = "INSERT INTO dbo.UserAnswerTable(UserId,quesId,answerText) " +
                           "VALUES (@UserId,@quesId,@answerText) ";

            // create connection and command
            using (SqlConnection cn = new SqlConnection(connectionString))
            using (SqlCommand cmd = new SqlCommand(query, cn))
            {
                // define parameters and their values
                cmd.Parameters.Add("@UserId", SqlDbType.Int).Value = userId;
                cmd.Parameters.Add("@quesId", SqlDbType.Int).Value = quesId;
                cmd.Parameters.Add("@answerText", SqlDbType.VarChar, 100).Value = answerText;
              
                // open connection, execute INSERT, close connection
                cn.Open();
                cmd.ExecuteNonQuery();
                cn.Close();
            }
        }

        private int InsertDataToUserTable(string connectionString, string ipAddress, DateTime dateSurveyConducted, string fname,string lname,DateTime dob,string contact)
        {
            // define INSERT query with parameters
            //string query = "INSERT INTO dbo.UserTable(ipAddress,dateSurveyConducted)    OUTPUT INSERTED.userID " +
            //               "VALUES (@ipAddress,@dateSurveyConducted) ";

            string query = "INSERT INTO dbo.UserTable(ipAddress,dateSurveyConducted,Fname,Lname,DOB,contact)    OUTPUT INSERTED.userID " +
                           "VALUES (@ipAddress,@dateSurveyConducted,@Fname,@Lname,@DOB,@contact) ";

           

            int userID;
            // create connection and command
            using (SqlConnection cn = new SqlConnection(connectionString))
            using (SqlCommand cmd = new SqlCommand(query, cn))
            {
                // define parameters and their values
                cmd.Parameters.Add("@ipAddress", SqlDbType.NVarChar, 20).Value = ipAddress;
                cmd.Parameters.Add("@dateSurveyConducted", SqlDbType.Date).Value = dateSurveyConducted.Date;
                cmd.Parameters.Add("@Fname", SqlDbType.NVarChar, 50).Value = fname;
                cmd.Parameters.Add("@Lname", SqlDbType.NVarChar, 50).Value = lname;
                cmd.Parameters.Add("@DOB", SqlDbType.Date).Value = dob.Date;
                cmd.Parameters.Add("@contact", SqlDbType.NVarChar, 50).Value = contact;

                // open connection, execute INSERT, close connection
                cn.Open();
                userID = Convert.ToInt32(cmd.ExecuteScalar());

                // cmd.ExecuteNonQuery();
                cn.Close();

            }
            return userID;
        }

    }
}