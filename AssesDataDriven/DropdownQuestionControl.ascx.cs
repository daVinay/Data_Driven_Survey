using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace AssesDataDriven
{
    public partial class DropdownQuestionControl : System.Web.UI.UserControl
    {
         public Label QuestionLabel
        {
            get
            {
                return questionLabel; 
            }
            set
            {
                questionLabel = value;
            }
        }
        public DropDownList QuestionDropdownList
        {
            get
            {
                return questionDropDownList;
            }
            set
            {
                questionDropDownList = value;
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {

        }
    }
}