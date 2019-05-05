<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="_Default" %>

<%@ Register Src="~/UserControls/UserControl_Kurv.ascx" TagPrefix="uc1" TagName="UserControl_Kurv" %>


<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <link href="StyleSheet.css" rel="stylesheet" />
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <%--            <h3>ID</h3>
            <asp:TextBox ID="TextBox_ID" runat="server"></asp:TextBox>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator_ID" Display="Dynamic" ControlToValidate="TextBox_ID" runat="server" ErrorMessage="Skal udfyldes"></asp:RequiredFieldValidator>
            <asp:RegularExpressionValidator ID="RegularExpressionValidator_ID" Display="Dynamic" runat="server" ErrorMessage="Tal kun" ControlToValidate="TextBox_ID"
                ValidationExpression="^\d+$"></asp:RegularExpressionValidator>

            <h3>NAVN</h3>
            <asp:TextBox ID="TextBox_Navn" runat="server"></asp:TextBox>

            <h3>PRIS</h3>
            <asp:TextBox ID="TextBox_Pris" runat="server"></asp:TextBox>
            <asp:RegularExpressionValidator ID="RegularExpressionValidator_Pris" Display="Dynamic" ControlToValidate="TextBox_Pris" ValidationExpression="\d+(?:,\d{1,2})?" runat="server" ErrorMessage="Skal være i tal (komma er ok)"></asp:RegularExpressionValidator>

            <h3>ANTAL</h3>
            <asp:TextBox ID="TextBox_Antal" runat="server"></asp:TextBox>
            <asp:RegularExpressionValidator ID="RegularExpressionValidator_Antal" Display="Dynamic" runat="server" ErrorMessage="Tal kun" ControlToValidate="TextBox_ID"
                ValidationExpression="^\d+$"></asp:RegularExpressionValidator>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator_Antal" Display="Dynamic" ControlToValidate="TextBox_Antal" runat="server" ErrorMessage="Skal udfyldes"></asp:RequiredFieldValidator>

            <asp:Button ID="Button_Send" runat="server" Text="Køb" OnClick="Button_Send_Click" />--%>

            <asp:Repeater ID="Repeater_Produkter" OnItemCommand="Repeater_Produkter_ItemCommand" runat="server">
                <HeaderTemplate>
                    <table>
                </HeaderTemplate>
                <ItemTemplate>
                    <tr>
                        <td>
                            <asp:Literal ID="Literal_ProduktNavn" Text='<%# Eval("produkt_navn") %>' runat="server"></asp:Literal>
                        </td>
                        <td>
                            <asp:Literal ID="Literal_ProduktPris" Text='<%# Eval("produkt_pris") %>' runat="server"></asp:Literal>
                            kr.
                        </td>
                        <td>
                            <asp:HiddenField ID="HiddenField_ProduktID" Value='<%# Eval("produkt_id") %>' runat="server" />
                            <asp:TextBox ID="TextBox_Antal" TextMode="Number" Text="1" runat="server"></asp:TextBox>
                            <asp:Button ID="Button_Køb" runat="server" Text="Læg i kurv" CommandName="køb" />
                        </td>
                        <td>
                          Lager status: <asp:Literal ID="Literal_ProduktLagerStand" Text='<%# Eval("produkt_lager_stand") %>' runat="server"></asp:Literal>
                        </td>
                    </tr>
                </ItemTemplate>
                <FooterTemplate></table></FooterTemplate>
            </asp:Repeater>

            <uc1:UserControl_Kurv runat="server" ID="UserControl_Kurv" />

            <a href="Afslut.aspx">Afslut ordre</a>
        </div>
    </form>
</body>
</html>
