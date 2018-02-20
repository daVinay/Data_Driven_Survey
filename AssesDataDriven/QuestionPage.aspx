<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="QuestionPage.aspx.cs" Inherits="AssesDataDriven.QuestionPage" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
    <head id="Head1" runat="server">
        <title></title>
        <link href="Styles/Site.css" rel="Stylesheet" />
    </head>
    <body>
        <form id="Form1" runat="server">
            <div class="header" style="border:1px solid; background-color: White; margin:10px;">
              
                <h1><img src="https://www.theloop.com.au/app/serve/view_file/L21udC9zaXRlX2RhdGEvcHJvZmlsZS85NjExMi1sLmpwZywxNDE0NjQwOTcxLGltYWdlL2pwZw,," 
                        alt="AIT logo" style="height: 58px; width: 84px"/> AIT Research</h1>
                <div style="text-align:right">
                   <a href ="StaffLogin.aspx">staff login</a>
                   <a href ="QuestionPage.aspx">Main</a>
                </div>
            </div>

             <div class="middlebox">

                 <asp:PlaceHolder ID="PlaceHolderQues" runat="server"></asp:PlaceHolder>
                 <br />
                 <asp:Button ID="btnNext" runat="server" Text="Next" onclick="btnNext_Click" />
                 <br />
                 <asp:Label ID="errosMsg" runat="server" ForeColor="#FF3300"></asp:Label>
                
                 <br />
                 <asp:Label ID="selectedAnswerLabel" runat="server"></asp:Label>
                
            </div>
        </form>
        <script src="Scripts/bootstrap.min.js" type="text/javascript" />
    </body>
</html>

