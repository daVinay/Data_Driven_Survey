<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="StaffSearchPage.aspx.cs" Inherits="AssesDataDriven.StaffSearchPage" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="Styles/Site.css" rel="stylesheet"/>
    <link href="Styles/bootstrap.min.css" rel="Stylesheet" />
    <link href="Styles/font-awesome.min.css" rel="stylesheet"/>
</head>
<body style="width:100%">

    <form id="form1" runat="server" style="width:100%">
       <div class="header" style="border:1px solid; background-color: White; margin:10px;">
              
                <h1>&nbsp; <img src="https://www.theloop.com.au/app/serve/view_file/L21udC9zaXRlX2RhdGEvcHJvZmlsZS85NjExMi1sLmpwZywxNDE0NjQwOTcxLGltYWdlL2pwZw,," 
                        alt="AIT logo" style="height: 58px; width: 84px"/> AIT Research</h1>
                <div style="text-align:right">
                   <a href ="#"><img src="Styles/user.png" style="width:20px; height:19px;"/>Staff</a>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                    <a href ="QuestionPage.aspx">main</a>
                </div>
            </div>
        <div style="width: 100%; height: 400px; overflow: scroll">
            <asp:GridView ID="GridView1" runat="server" style="width:100%" >
            </asp:GridView>
        </div>
        <div style="margin:20px">
            <asp:Label ID="Label1" runat="server" Text="Search by:" Font-Bold="True" 
                Font-Size="Large"></asp:Label>

  
            &nbsp;&nbsp;&nbsp;&nbsp;
 
            <br/>&nbsp;&nbsp;&nbsp;&nbsp;Age range:&nbsp;&nbsp;&nbsp; &nbsp; 
                 <asp:DropDownList ID="DropDownAgeRange" runat="server">
                 <asp:ListItem>-Select-</asp:ListItem>
                 <asp:ListItem Value="6">15-20</asp:ListItem>
                 <asp:ListItem Value="7">21-25</asp:ListItem>
                 <asp:ListItem Value="8">26-35</asp:ListItem>
                 <asp:ListItem Value="9">Above 35</asp:ListItem>
                 </asp:DropDownList>
            <br />
    &nbsp;&nbsp;&nbsp; Gender:&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
            <asp:DropDownList ID="DropDownGender" runat="server">

            <asp:ListItem>-Select-</asp:ListItem>
                 <asp:ListItem Value="4">male</asp:ListItem>
                 <asp:ListItem Value="5">female</asp:ListItem>
                 
            </asp:DropDownList>
            <br />
    &nbsp;&nbsp;&nbsp; Suburb:&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
            <asp:TextBox ID="TextBox3" runat="server"></asp:TextBox>
            &nbsp;&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
            <br />
    &nbsp;&nbsp;&nbsp; Bank Used:&nbsp;&nbsp;&nbsp;&nbsp;
            <asp:DropDownList ID="DropDownBanks" runat="server">
                  <asp:ListItem>-Select-</asp:ListItem>
                 <asp:ListItem Value="21">CBA</asp:ListItem>
                 <asp:ListItem Value="22">ANZ</asp:ListItem>
                 <asp:ListItem Value="23">Westpac</asp:ListItem>
                 <asp:ListItem Value="24">ING Direct</asp:ListItem>
                 <asp:ListItem Value="25">CITI</asp:ListItem>
                
                 
            </asp:DropDownList>
            <br />
    &nbsp;&nbsp;&nbsp; Newspaper:&nbsp;&nbsp;&nbsp;
            <asp:DropDownList ID="DropDownNewspaper" runat="server">
                  <asp:ListItem>-Select-</asp:ListItem>
                 <asp:ListItem Value="30">smh.com.au</asp:ListItem>
                 <asp:ListItem Value="31">daily telegraph</asp:ListItem>
                 <asp:ListItem Value="32">the Australian</asp:ListItem>
                 <asp:ListItem Value="33">Sydney Times</asp:ListItem>

                 
            </asp:DropDownList>
            <br />
    &nbsp;&nbsp;&nbsp;
            <br />
    &nbsp;&nbsp;&nbsp;
            <asp:Button ID="BtnClearAll" runat="server" Text="Clear all" 
                onclick="BtnClearAll_Click" />
    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
            <asp:Button ID="BtnSearch" runat="server" Text="Search" 
                onclick="BtnSearch_Click" />
    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
            <asp:Button ID="BtnShowAll" runat="server" Text="Show all" 
                onclick="BtnShowAll_Clicked" />
            <br />
            <br />
     </div>

    </form>
   
</body>
</html>
