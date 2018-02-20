<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="userRegistrationPage.aspx.cs" Inherits="AssesDataDriven.userRegistrationPage" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
    <head id="Head1" runat="server">
        <title></title>
        <link href="Styles/Site.css" rel="Stylesheet" />
        <link href="Styles/bootstrap.min.css" rel="Stylesheet" />
        <link href="Styles/font-awesome.min.css" rel="stylesheet"/>
    </head>
    <body>
        <form id="Form1" runat="server">
       <div class="header" style="border:1px solid; background-color: White; margin:10px;">
              
                <h1><img src="https://www.theloop.com.au/app/serve/view_file/L21udC9zaXRlX2RhdGEvcHJvZmlsZS85NjExMi1sLmpwZywxNDE0NjQwOTcxLGltYWdlL2pwZw,," 
                        alt="AIT logo" style="height: 58px; width: 84px"/> AIT Research</h1>
                <div style="text-align:right">
                   <a href ="StaffLogin.aspx">staff login</a>
                   <a href ="QuestionPage.aspx">main</a>
                </div>
            </div>
            <div class="middlebox">
             
                <asp:Label ID="Label1" runat="server" Font-Bold="True" Font-Size="Large" 
                    Text="User Registration"></asp:Label>
                <br />
                <asp:Label ID="Label2" runat="server" Text="First name: "></asp:Label>
&nbsp;<asp:TextBox ID="TextBox1" runat="server"></asp:TextBox>
                <br />
                <asp:Label ID="Label3" runat="server" Text="Last name: "></asp:Label>
&nbsp;<asp:TextBox ID="TextBox2" runat="server"></asp:TextBox>
                <br />
                DOB:&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;
                <asp:TextBox ID="TextBox3" runat="server">dd/mm/yyyy</asp:TextBox>
                <br />
                Contact:&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                <asp:TextBox ID="TextBox4" runat="server"></asp:TextBox>
                <br />
                <br />
                <br />
                <div>
                    <asp:Button ID="Button2" runat="server" Text="Cancel"  type="button" 
                    class="btn btn-info btn-md" />
       
                    <asp:Button ID="Button1" runat="server" Text="Register"  type="button" 
                    class="btn btn-info btn-md" onclick="Button1_Click"/>
             </div>
            </div>
        </form>
        <script src="Scripts/bootstrap.min.js" type="text/javascript" />
    </body>
</html>

