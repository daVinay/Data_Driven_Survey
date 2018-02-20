using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace AssesDataDriven
{
    public partial class StaffLogin : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void NextButton_Click(object sender, EventArgs e)
        {
            if ((TextBox1.Text == "staff@gmail.com") && TextBox2.Text == "111111")
                Response.Redirect("~/StaffSearchPage.aspx");
            else
                errorMsg.Text = "Wrong username or password!";
        }

    }
}