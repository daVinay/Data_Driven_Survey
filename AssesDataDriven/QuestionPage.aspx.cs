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

namespace AssesDataDriven
{



    public partial class QuestionPage : System.Web.UI.Page
    {
        /// <summary>
        /// creating and initialising user answer table session
        /// </summary>
        /// 


        public List<UserAnswerDetail> UserAnswer
        {
            get
            {
                if (HttpContext.Current.Session["UserAnswerDetail"] == null)
                {
                    HttpContext.Current.Session["UserAnswerDetail"] = new List<UserAnswerDetail>();
                }
                return HttpContext.Current.Session["UserAnswerDetail"] as List<UserAnswerDetail>;
            }
            set
            {
                HttpContext.Current.Session["UserAnswerDetail"] = value;
            }

        }


        /// <summary>
        /// get user Ip address
        /// </summary>
        /// <returns></returns>
 
        public string GetIpAddress()
        {
            //get IP through PROXY
            //====================
            System.Web.HttpContext context = System.Web.HttpContext.Current;
            string ipAddress = context.Request.ServerVariables["HTTP_X_FORWARDED_FOR"];

            //should break ipAddress down, but here is what it looks like:
           // return ipAddress;
            if (!string.IsNullOrEmpty(ipAddress))
            {
                string[] address = ipAddress.Split(',');
                if (address.Length != 0)
                {
                    return address[0];
                }
            }
            //if not proxy, get nice ip, give that back :(
            //ACROSS WEB HTTP REQUEST
            //=======================
            ipAddress = context.Request.UserHostAddress;//ServerVariables["REMOTE_ADDR"];

            if (ipAddress.Trim() == "::1")//ITS LOCAL(either lan or on same machine), CHECK LAN IP INSTEAD
            {
                //This is for Local(LAN) Connected ID Address
                string stringHostName = System.Net.Dns.GetHostName();
                //Get Ip Host Entry
                System.Net.IPHostEntry ipHostEntries = System.Net.Dns.GetHostEntry(stringHostName);
                //Get Ip Address From The Ip Host Entry Address List
                System.Net.IPAddress[] arrIpAddress = ipHostEntries.AddressList;

                try
                {
                    ipAddress = arrIpAddress[1].ToString();
                }
                catch
                {
                    try
                    {
                        ipAddress = arrIpAddress[0].ToString();
                    }
                    catch
                    {
                        try
                        {
                            arrIpAddress = System.Net.Dns.GetHostAddresses(stringHostName);
                            ipAddress = arrIpAddress[0].ToString();
                        }
                        catch
                        {
                            ipAddress = "127.0.0.1";
                        }
                    }
                }
            }
            return ipAddress;
        }



        public class UserDetail
        {

            public string UserIp { get; set; }
            public DateTime Date { get; set; }

        }


        public class UserAnswerDetail
        {
            public string AnswerText { get; set; }
            public int QuesId { get; set; }
        }


        protected void Page_Load(object sender, EventArgs e)
        {
            DropdownQuestionControl dropdownControl = (DropdownQuestionControl)LoadControl("~/DropdownQuestionControl.ascx");
            //get Ip and Date only once, don't repeat
            if (!IsPostBack)
            {
                List<UserDetail> UserDetailList = new List<UserDetail>();

                UserDetailList.Add(new UserDetail
                {
                    UserIp = GetIpAddress(),
                    Date = DateTime.Now
                });

                HttpContext.Current.Session["UserInfo"] = UserDetailList;

            }


                //Get current question number, session initialised
                int currentQuestion = 8;//TESTING, CHANGE BACK TO 1!!!!
                Console.WriteLine(currentQuestion);
                if (HttpContext.Current.Session["questionNumber"] == null)
                    HttpContext.Current.Session["questionNumber"] = 8; //then set it
                else
                    currentQuestion = Convert.ToInt32(HttpContext.Current.Session["questionNumber"]);


                //get current question from DB
                SqlConnection connection;
                SqlCommand command;

                //testConnectionString from webconfig
                string connectionString = ConfigurationManager.ConnectionStrings["VinayConnectionString"].ConnectionString;

                connection = new SqlConnection();
                connection.ConnectionString = connectionString;

                connection.Open();//open the sql connection using the connection string info

                //just setup a basic sql command (referencing the connection)
                command = new SqlCommand("SELECT * FROM QuestionTable WHERE questionId =" + currentQuestion, connection);

                SqlDataReader reader = command.ExecuteReader(); //execute above query
                Console.WriteLine("command results: " + command);

                while (reader.Read())
                {
                    string questionText = reader["description"].ToString();
                    string questionType = reader["quesType"].ToString().ToLower(); //just incase, so we dont have to check for textBox vs TextBox vs textbox

                    if (questionType.Equals("textbox"))
                    {

                        //TODO load up textbox controls
                        TextQuestionControl textControl = (TextQuestionControl)LoadControl("~/TextQuestionControl.ascx");
                        textControl.ID = "textQuestion";
                        textControl.QuestionLabel.Text = questionText;
                        //add it to the ui
                        PlaceHolderQues.Controls.Add(textControl);
                    }
                    else if (questionType.Equals("dropdown"))
                    {
                        //TODO load up checkbox controls
                       
                        dropdownControl.ID = "dropDownQuestion";
                        dropdownControl.QuestionLabel.Text = questionText;

                        //TODO load up checkbox options/choices to add to checkbox control
                        SqlCommand optionCommand = new SqlCommand("SELECT * FROM AnswerTable WHERE quesId = " + currentQuestion, connection);

                        SqlDataReader optionReader = optionCommand.ExecuteReader();
                        //cycle through all options
                        while (optionReader.Read())
                        {
                            //                           text you see,                      value its worth
                            ListItem item = new ListItem(optionReader["answerText"].ToString(), optionReader["nextQuesId"].ToString());
                            dropdownControl.QuestionDropdownList.Items.Add(item); //add item to option list
                        }

                        //done, add it to placeholder
                        PlaceHolderQues.Controls.Add(dropdownControl);

                    }
                    else if (questionType.Equals("checkbox"))
                    {
                        //TODO load up checkbox controls
                        CheckBoxQuestionControl checkBoxControl = (CheckBoxQuestionControl)LoadControl("~/CheckBoxQuestionControl.ascx");
                        checkBoxControl.ID = "checkBoxQuestion";
                        checkBoxControl.QuestionLabel.Text = questionText;

                        //TODO load up checkbox options/choices to add to checkbox control
                        SqlCommand optionCommand = new SqlCommand("SELECT * FROM AnswerTable WHERE quesId = " + currentQuestion, connection);
                        Console.WriteLine(currentQuestion);

                        SqlDataReader optionReader = optionCommand.ExecuteReader();
                        //cycle through all options
                        while (optionReader.Read())
                        {
                            //                           text you see,                      value its worth
                            ListItem item = new ListItem(optionReader["answerText"].ToString(), optionReader["nextQuesId"].ToString());
                            checkBoxControl.QuestionCheckBoxList.Items.Add(item); //add item to option list
                        }

                        //done, add it to placeholder
                        PlaceHolderQues.Controls.Add(checkBoxControl);

                    }

                }

                connection.Close();
           
        }




        protected void btnNext_Click(object sender, EventArgs e)
        {


            SqlConnection connection;
            SqlCommand command;

            //testConnectionString from webconfig
            string connectionString = ConfigurationManager.ConnectionStrings["VinayConnectionString"].ConnectionString;
            connection = new SqlConnection();
            connection.ConnectionString = connectionString;
            connection.Open();
            int NextQuestionId = 0;


            DropdownQuestionControl dropDownQuestion = (DropdownQuestionControl)PlaceHolderQues.FindControl("dropDownQuestion");
           // DropdownQuestionControl dropDownQuestion = (DropdownQuestionControl)LoadControl("~/DropdownQuestionControl.ascx");
                if (dropDownQuestion != null)
                {

                    foreach (ListItem item in dropDownQuestion.QuestionDropdownList.Items)
                    {
                        if (item.Selected)
                        {

                            if (Convert.ToInt16(HttpContext.Current.Session["questionNumber"]) == 20 && item.Text == "yes")
                            {
                                Response.Redirect("~/userRegistrationPage.aspx");
                            }
                            else if (item.Text == "No")
                            {
                                //chuck data to database

                                    foreach (var item1 in (List<AssesDataDriven.QuestionPage.UserDetail>)HttpContext.Current.Session["UserInfo"])
                                    {
                                        int userID = InsertDataToUserTable(connectionString, item1.UserIp, item1.Date);


                                        foreach (AssesDataDriven.QuestionPage.UserAnswerDetail item2 in UserAnswer)
                                        {
                                            InsertDataToUserAnswerTable(connectionString, userID, item2.QuesId, item2.AnswerText);
                                        }

                                    }
                                    Response.Redirect("~/ThankyouPage.aspx");
                               
                            }


                            // add values in session - start  
                            
                            UserAnswer.Add(new UserAnswerDetail
                                {
                                    QuesId = Convert.ToInt16(HttpContext.Current.Session["questionNumber"]),
                                    AnswerText = item.Text
                                });
                            //  HttpContext.Current.Session["UserAnswerDetail"] = UserAnswer;
                            // end



                            //add answer to db against this user
                            // SqlCommand updateCommand = new SqlCommand("INSERT INTO UserAnswerTable(userId, quesId, answerText) VALUES (1," + currentQuestion + "," + item.Text + ") ", connection);
                            NextQuestionId = Convert.ToInt32(item.Value);
                            HttpContext.Current.Session["questionNumber"] = NextQuestionId;
                            //if selected item leads to bonus question, maybe load that up somehowe

                            // selectedAnswerLabel.Text += item.Text + "<br/>";
                        }
                    }
                }
            
          


            TextQuestionControl textQuestion = (TextQuestionControl)PlaceHolderQues.FindControl("textQuestion");
            try
            {
                if (textQuestion != null)
                {
                    SqlCommand optionCommand = new SqlCommand("SELECT * FROM AnswerTable WHERE quesId = " + HttpContext.Current.Session["questionNumber"], connection);

                    SqlDataReader optionReader = optionCommand.ExecuteReader();

                    //cycle through all options
                    while (optionReader.Read())
                    {
                        // add values in session - start  
                        UserAnswer.Add(new UserAnswerDetail
                        {
                            QuesId = Convert.ToInt16(HttpContext.Current.Session["questionNumber"]),
                            AnswerText = textQuestion.QuestionTextBox.Text
                        });

                        //                           text you see,                      value its worth
                        HttpContext.Current.Session["questionNumber"] = Convert.ToInt16(optionReader["nextQuesId"]);
                        NextQuestionId = Convert.ToInt16(HttpContext.Current.Session["questionNumber"]);
                    }
                }
            }
            catch (Exception ex)
            {
                throw new ApplicationException(string.Format("Something happened..."), ex);
            }

            CheckBoxQuestionControl checkBoxQuestion = (CheckBoxQuestionControl)PlaceHolderQues.FindControl("checkBoxQuestion");

            try
            {
                if (checkBoxQuestion != null)
                {

                    if (Convert.ToInt16(HttpContext.Current.Session["questionNumber"]) == 14)
                    {
                        int count = 0;

                        foreach (ListItem item in checkBoxQuestion.QuestionCheckBoxList.Items)
                        {
                            if (item.Selected)
                            {
                                count = count + 1;

                                if (count > 4)
                                {
                                    errosMsg.Text = "Invalid Selection.";
                                    return;
                                }

                            }
                        }
                    }


                    foreach (ListItem item in checkBoxQuestion.QuestionCheckBoxList.Items)
                    {
                        if (item.Selected)
                        {

                            // add values in session - start  
                            UserAnswer.Add(new UserAnswerDetail
                            {
                                QuesId = Convert.ToInt16(HttpContext.Current.Session["questionNumber"]),
                                AnswerText = item.Text
                            });

                            errosMsg.Text = string.Empty;
                            if (item.Text == "ANZ" || item.Text == "CBA" || item.Text == "Westpac")
                            {
                                NextQuestionId = Convert.ToInt32(item.Value);
                                HttpContext.Current.Session["questionNumber"] = NextQuestionId;
                                break;
                            }

                            //add answer to db against this user
                            // SqlCommand updateCommand = new SqlCommand("INSERT INTO UserAnswerTable(userId, quesId, answerText) VALUES (1," + currentQuestion + "," + item.Text + ") ", connection);
                            NextQuestionId = Convert.ToInt32(item.Value);
                            HttpContext.Current.Session["questionNumber"] = NextQuestionId;
                            //if selected item leads to bonus question, maybe load that up somehowe

                            // selectedAnswerLabel.Text += item.Text + "<br/>";
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw new ApplicationException(string.Format("Something happened..."), ex);
            }

            //just setup a basic sql command (referencing the connection)
            command = new SqlCommand("SELECT * FROM QuestionTable WHERE questionId =" + NextQuestionId, connection);

            SqlDataReader reader = command.ExecuteReader(); //execute above query
            Console.WriteLine("command results: " + command);
            while (reader.Read())
            {
                string questionText = reader["description"].ToString();
                string questionType = reader["quesType"].ToString().ToLower(); //just incase, so we dont have to check for textBox vs TextBox vs textbox

                if (questionType.Equals("textbox"))
                {


                    //TODO load up textbox controls
                    TextQuestionControl textControl = (TextQuestionControl)LoadControl("~/TextQuestionControl.ascx");
                    textControl.ID = "textQuestion";
                    textControl.QuestionLabel.Text = questionText;


                    //add it to the ui
                    PlaceHolderQues.Controls.Clear();
                    PlaceHolderQues.Controls.Add(textControl);
                }
                else if (questionType.Equals("dropdown"))
                {
                    //TODO load up checkbox controls
                    DropdownQuestionControl dropdownControl = (DropdownQuestionControl)LoadControl("~/DropdownQuestionControl.ascx");
                    dropdownControl.ID = "dropDownQuestion";
                    dropdownControl.QuestionLabel.Text = questionText;

                    //TODO load up checkbox options/choices to add to checkbox control
                    SqlCommand optionCommand = new SqlCommand("SELECT * FROM AnswerTable WHERE quesId = " + NextQuestionId, connection);

                    SqlDataReader optionReader = optionCommand.ExecuteReader();
                    //cycle through all options
                    while (optionReader.Read())
                    {
                        //                           text you see,                      value its worth
                        ListItem item = new ListItem(optionReader["answerText"].ToString(), optionReader["nextQuesId"].ToString());
                        dropdownControl.QuestionDropdownList.Items.Add(item); //add item to option list
                    }

                    //done, add it to placeholder
                    PlaceHolderQues.Controls.Clear();
                    PlaceHolderQues.Controls.Add(dropdownControl);

                }
                else if (questionType.Equals("checkbox"))
                {
                    //TODO load up checkbox controls
                    CheckBoxQuestionControl checkBoxControl = (CheckBoxQuestionControl)LoadControl("~/CheckBoxQuestionControl.ascx");
                    checkBoxControl.ID = "checkBoxQuestion";
                    checkBoxControl.QuestionLabel.Text = questionText;

                    //TODO load up checkbox options/choices to add to checkbox control
                    SqlCommand optionCommand = new SqlCommand("SELECT * FROM AnswerTable WHERE quesId = " + NextQuestionId, connection);
                    // Console.WriteLine(currentQuestion);

                    SqlDataReader optionReader = optionCommand.ExecuteReader();
                    //cycle through all options
                    while (optionReader.Read())
                    {
                        //                           text you see,                      value its worth
                        ListItem item = new ListItem(optionReader["answerText"].ToString(), optionReader["nextQuesId"].ToString());
                        checkBoxControl.QuestionCheckBoxList.Items.Add(item); //add item to option list
                    }

                    //done, add it to placeholder
                    PlaceHolderQues.Controls.Clear();
                    PlaceHolderQues.Controls.Add(checkBoxControl);

                }
            }


            connection.Close();//<==============
        }

        /// <summary>
        /// to push data to UserTable
        /// </summary>
        /// <param name="connectionString"></param>
        /// <param name="ipAddress"></param>
        /// <param name="dateSurveyConducted"></param>
        /// <returns></returns>
        private int InsertDataToUserTable(string connectionString, string ipAddress, DateTime dateSurveyConducted)
        {
            //Output will get the latest created userID in UserTable
            string query = "INSERT INTO dbo.UserTable(ipAddress,dateSurveyConducted)    OUTPUT INSERTED.userID " +
                           "VALUES (@ipAddress,@dateSurveyConducted) ";

            int userID;
            // create connection and command
            using (SqlConnection cn = new SqlConnection(connectionString))
            using (SqlCommand cmd = new SqlCommand(query, cn))
            {
                // define parameters and their values

                cmd.Parameters.Add("@ipAddress", SqlDbType.NVarChar, 20).Value = ipAddress;
                cmd.Parameters.Add("@dateSurveyConducted", SqlDbType.Date).Value = dateSurveyConducted.Date;

                // open connection, execute INSERT, close connection
                cn.Open();
                userID = Convert.ToInt32(cmd.ExecuteScalar());  //scalar - returnng userID from user table

               // cmd.ExecuteNonQuery();
                cn.Close();
                
            }
            return userID;
        }

        /// <summary>
        /// to push data to UserAnswerTable
        /// </summary>
        /// <param name="connectionString"></param>
        /// <param name="userId"></param>
        /// <param name="quesId"></param>
        /// <param name="answerText"></param>
        private void InsertDataToUserAnswerTable(string connectionString, int userId, int quesId, string answerText)
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

    }
}