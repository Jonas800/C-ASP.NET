<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="Admin_Default" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <h2>Titel</h2>
            <asp:TextBox ID="TextBox_Titel" runat="server"></asp:TextBox>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator_Titel" runat="server" ErrorMessage="Skal udfyldes" ControlToValidate="TextBox_Titel"></asp:RequiredFieldValidator>

            <h2>Url</h2>
            <asp:TextBox ID="TextBox_Url" runat="server"></asp:TextBox>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator_Url" runat="server" ErrorMessage="Skal udfyldes" ControlToValidate="TextBox_Url"></asp:RequiredFieldValidator>

            <asp:Button ID="Button_Gem" runat="server" Text="Gem" OnClick="Button_Gem_Click" />

            <asp:Repeater ID="Repeater_RSS" runat="server">
                <ItemTemplate>
                    <p>
                         <a href="<%# Eval("feed_url") %>"><%# Eval("feed_titel") %></a>
                        <a href="?id=<%# Eval("feed_id") %>&action=ret"> Ret</a>
                        <a href="?id=<%# Eval("feed_id") %>&action=slet"> Slet</a>
                    </p>
                </ItemTemplate>
            </asp:Repeater>
        </div>
    </form>
</body>
</html>
