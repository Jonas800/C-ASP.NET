<%@ Control Language="C#" AutoEventWireup="true" CodeFile="UserControl_Login.ascx.cs" Inherits="UserControl_UserControl_Login" %>
<div>
    <asp:Panel ID="Panel_Login" runat="server" DefaultButton="Button_Login">
        <label>Email</label>
        <asp:TextBox ID="TextBox_Email" runat="server"></asp:TextBox>
        <label>Kodeord</label>
        <asp:TextBox ID="TextBox_Kodeord" TextMode="Password" runat="server"></asp:TextBox>
        <asp:Button ID="Button_Login" OnClick="Button_Login_Click" runat="server" Text="Login" ValidationGroup="Login" /><br />
        <asp:Label ID="Label_Error" runat="server" ForeColor="DarkRed" Font-Size="12px"></asp:Label>
    </asp:Panel>

    <asp:Panel ID="Panel_Logud" runat="server" Visible="false">
        <asp:Label ID="Label_Velkommen" runat="server" Text="Velkommen " ForeColor="Black"></asp:Label>
        <asp:Label ID="Label_Navn" runat="server" ForeColor="Black"></asp:Label>
        <asp:Literal ID="Literal_Go_Admin" runat="server"></asp:Literal>
        <br />
        <br />
<%--        <a href="?logud=true">Logud</a>--%>
    </asp:Panel>
</div>
