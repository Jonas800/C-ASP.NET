<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/MasterPage.master" AutoEventWireup="true" CodeFile="Priser.aspx.cs" Inherits="Admin_Priser" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <label>Overskrift</label>
    <asp:TextBox ID="TextBox_Overskrift" runat="server"></asp:TextBox>
    <asp:RequiredFieldValidator ID="RequiredFieldValidator_Overskrift" runat="server" ControlToValidate="TextBox_Overskrift" ErrorMessage="Skal udfyldes" ForeColor="DarkRed" Display="Dynamic"></asp:RequiredFieldValidator>

    <label>Kort beskrivelse (valgfri)</label>
    <asp:TextBox ID="TextBox_Beskrivelse" runat="server"></asp:TextBox>

    <label>Pris</label>
    <asp:TextBox ID="TextBox_Pris" runat="server"></asp:TextBox>
    <asp:RequiredFieldValidator ID="RequiredFieldValidator_Pris" runat="server" ControlToValidate="TextBox_Pris" ErrorMessage="Skal udfyldes" ForeColor="DarkRed" Display="Dynamic"></asp:RequiredFieldValidator>
    <asp:CompareValidator ID="CompareValidator_Pris" runat="server" Operator="DataTypeCheck" Type="Currency" ForeColor="DarkRed"
        ControlToValidate="TextBox_Pris" Display="Dynamic" ErrorMessage="Skal være et beløb" />

    <label>Tilbud (rabat i procent, valgfri)</label>
    <asp:TextBox ID="TextBox_Tilbud" runat="server"></asp:TextBox>
    <asp:CompareValidator ID="CompareValidator_Tilbud" runat="server" Operator="DataTypeCheck" Type="Integer" ForeColor="DarkRed"
        ControlToValidate="TextBox_Tilbud" Display="Dynamic" ErrorMessage="Skal være et tal (uden komma)" />

    <asp:Button ID="Button_Gem" runat="server" Text="Gem" OnClick="Button_Gem_Click" />

    <h3>Prisoversigt</h3>
    <div class="responsivTable">
        <asp:Repeater ID="Repeater_Priser" runat="server">
            <HeaderTemplate>
                <table>
                    <tr>
                        <th>Overskrift</th>
                        <th>Beskrivelse</th>
                        <th>Pris</th>
                        <th>Tilbud</th>
                        <th>Tilbudspris</th>
                        <th>Ret</th>
                        <th>Slet</th>
                    </tr>
            </HeaderTemplate>
            <ItemTemplate>
                <tr>
                    <td><%# Eval("pris_overskrift") %></td>
                    <td><%# Eval("pris_tekst") %></td>
                    <td><%# Eval("pris_pris") %></td>
                    <td> <%# (String.IsNullOrEmpty(Eval("pris_tilbud").ToString()) ? "" : Eval("pris_tilbud") + "%") %></td>
                    <td><%# Eval("pris_nu", "{0:F2}") %></td>
                    <td><a href="Priser.aspx?action=ret&id=<%# Eval("pris_id") %>"><i class="fa fa-edit"></i></a></td>
                    <td><a class="sletTd" href="Priser.aspx?action=slet&id=<%# Eval("pris_id") %>" onclick="return confirm('Er du sikker?')"><i class="fa fa-trash"></i></a></td>
                </tr>
            </ItemTemplate>
            <FooterTemplate>
                </table>
            </FooterTemplate>
        </asp:Repeater>
    </div>

</asp:Content>

