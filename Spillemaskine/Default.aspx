<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="_Default" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="StyleSheet.css" rel="stylesheet" />
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <h1>SPILLEMASKINEN</h1>
            <h2>Fang tre ens og vind gevinster!</h2>
            <asp:Label ID="Label_Bets" runat="server" Text="Pokéballs tilbage: 0"></asp:Label>
            <br />
            <asp:Image ID="Image1" runat="server" /><asp:Image ID="Image2" runat="server" /><asp:Image ID="Image3" runat="server" />
            <br />
            <asp:Label ID="Label_Gevinst" runat="server" Font-Size="40px" ForeColor="DarkRed" Visible="false"></asp:Label>
            <br />
            <asp:Button CssClass="spin" ID="Button_Spin" OnClick="Button_Spin_Click" runat="server" Text="KAST" />
            <asp:Label ID="Label_End" runat="server" Text="GAME OVER" Font-Size="82px" ForeColor="DarkRed" Visible="false"></asp:Label>
            <br />
            <asp:Panel ID="Panel_Kill" Visible="false" runat="server">
                <a href="?session=kill">Start forfra!</a>
            </asp:Panel>
        </div>
    </form>
</body>
</html>
