<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="StaffLogin.aspx.cs" Inherits="AssesDataDriven.StaffLogin" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
    <head runat="server">
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

            <%--<div class="middlebox">--%>
            <div style="background:#CCCCCC; padding: 10px; text-align:center; min-height: 100px;">
                <asp:Label ID="Label1" runat="server" Text="Staff id:&#160;&#160;&#160;"></asp:Label>
                
                <asp:TextBox ID="TextBox1" runat="server"  placeholder="staff@gmail.com"
                 style="margin-left:10px;"></asp:TextBox>
                <br />
                <asp:Label ID="Label2" runat="server" Text="Password:        "></asp:Label>
                <asp:TextBox ID="TextBox2" runat="server"  placeholder="111111"></asp:TextBox>
                
                <div style="padding-left:200px; text-align:center; margin-top: 10px;">
                    <asp:Button ID="NextButton" runat="server" Text="Login" type="button" 
                        class="btn btn-info btn-md" onclick="NextButton_Click"/>
                </div>

                <br />
                <asp:Label ID="errorMsg" runat="server" ForeColor="#FF3300"></asp:Label>
                

            </div>
        </form>
        <script src="Scripts/bootstrap.min.js" type="text/javascript" />
    </body>
</html>

