<%@ Control Language="C#" AutoEventWireup="true" CodeFile="UserControl_Login.ascx.cs" Inherits="UserControl_Login" %>

<h2>User control</h2>
    <div class="formular_wrap">
        <asp:Panel ID="Panel_Login" runat="server" DefaultButton="Button_Login">
            <h2>Login</h2>
            <asp:Label ID="Label_Login_Name" runat="server" Text="Email" CssClass="label_contact"></asp:Label>
            <asp:TextBox ID="TextBox_Login_Name" runat="server" CssClass="textbox_contact"></asp:TextBox>
            <asp:RegularExpressionValidator ID="RegularExpressionValidator_Login_Name" runat="server" ErrorMessage="Ugyldig email" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*" ControlToValidate="TextBox_Login_Name" Display="Dynamic" ForeColor="DarkRed"></asp:RegularExpressionValidator>

            <asp:Label ID="Label_Login_Password" runat="server" Text="Kodeord" CssClass="label_contact"></asp:Label>
            <asp:TextBox ID="TextBox_Login_Password" runat="server" CssClass="textbox_contact" TextMode="Password"></asp:TextBox>
            <asp:Button ID="Button_Login" runat="server" Text="Login" CssClass="button" OnClick="Button_Login_Click" />
            <br />
            <asp:Label ID="Label_Message" runat="server" Text="" ForeColor="DarkRed"></asp:Label>
        </asp:Panel>
    </div>