<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="_Default" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <h1>MEGA STOR ANIME DATABASE SØG</h1>
            Fritekst<br />
            <asp:TextBox ID="TextBox_Fritekst" runat="server"></asp:TextBox><br />
            Studio<br />
            <asp:DropDownList ID="DropDownList_Studio" runat="server" DataTextField="studio_name" DataValueField="studio_id"></asp:DropDownList><br />
            Genrer<br />
            <asp:CheckBoxList ID="CheckBoxList_Genre" runat="server" DataTextField="genre_navn" DataValueField="genre_id"></asp:CheckBoxList><br />
            År<br />
            Min
            <asp:TextBox ID="TextBox_År_Min" runat="server" Text="1900"></asp:TextBox>
            Max
            <asp:TextBox ID="TextBox_År_Max" runat="server"></asp:TextBox>
            <br />
            <asp:Button ID="Button_Søg" runat="server" Text="SØG!" OnClick="Button_Søg_Click" />


            <asp:Label ID="Label_Error" runat="server" Text=""></asp:Label>
            <asp:Repeater ID="Repeater_Results" runat="server" OnItemDataBound="Repeater_Results_ItemDataBound">
                <HeaderTemplate>
                    <table>
                        <tr>
                            <th>Navn</th>
                            <th>Studio</th>
                            <th>Genrer</th>
                            <th>År</th>
                        </tr>
                </HeaderTemplate>
                <ItemTemplate>
                    <tr>
                        <td><%# Eval("anime_navn") %></td>
                        <td><%# Eval("studio_name") %></td>
                        <td>
                            <asp:Repeater ID="Repeater_nested" runat="server">
                                <ItemTemplate><%# Eval("genre_navn") %></ItemTemplate>
                                <SeparatorTemplate>, </SeparatorTemplate>
                            </asp:Repeater>
                        </td>
                        <td><%# Eval("aar") %></td>
                    </tr>
                </ItemTemplate>
                <FooterTemplate>
                    </table>
                </FooterTemplate>
            </asp:Repeater>
        </div>
    </form>
</body>
</html>
