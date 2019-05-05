<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Afslut.aspx.cs" Inherits="Afslut" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <asp:Panel ID="Panel_Kunde" runat="server">
                Navn<br />
                <asp:TextBox ID="TextBox_Navn" runat="server"></asp:TextBox><br />
                Adresse<br />
                <asp:TextBox ID="TextBox_Adresse" runat="server"></asp:TextBox><br />
                By<br />
                <asp:TextBox ID="TextBox_By" runat="server"></asp:TextBox><br />
                Email<br />
                <asp:TextBox ID="TextBox_Email" runat="server"></asp:TextBox><br />

                <asp:Button ID="Button_Afslut" runat="server" Text="Send" OnClick="Button_Afslut_Click" />
            </asp:Panel>

            <asp:Panel ID="Panel_Tak" runat="server" Visible="false">
                TAK FOR ORDREN<br />
                <a href="Default.aspx">VIL DU BESTILLE MERE??!?!</a>
            </asp:Panel>
        </div>
    </form>
</body>
</html>
