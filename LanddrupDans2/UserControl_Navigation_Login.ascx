<%@ Control Language="C#" AutoEventWireup="true" CodeFile="UserControl_Navigation_Login.ascx.cs" Inherits="UserControl_Navigation_Login" %>
<asp:Panel ID="Panel_Logout" runat="server" Visible="false">
    <div class="login_position">
        <a class="login" href="Default.aspx">Hjem</a>

        <a class="login" href="Login.aspx?logout=true">Logout</a>
    </div>
</asp:Panel>

<asp:Panel ID="Panel_Login" runat="server">
    <div class="login_position">
        <a class="login" href="Login.aspx">Login</a>
    </div>
</asp:Panel>
