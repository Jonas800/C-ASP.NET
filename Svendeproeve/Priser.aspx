<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="Priser.aspx.cs" Inherits="Priser" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:Repeater ID="Repeater_Priser" runat="server">
        <HeaderTemplate>
        </HeaderTemplate>
        <ItemTemplate>
            <table class="priser">
                <tr>
                    <th><%# Eval("pris_overskrift") %></th>
                    <th><%# (String.IsNullOrEmpty(Eval("pris_nu").ToString()) ? "" : "tilbud") %></th>
                    <th><%# (String.IsNullOrEmpty(Eval("pris_nu").ToString()) ? Eval("pris_pris") : Eval("pris_nu", "{0:F2}")) %> kr</th>
                </tr>
                <tr>
                    <%# (String.IsNullOrEmpty(Eval("pris_tilbud").ToString()) ? "" : "<td colspan='3'>" + Eval("pris_pris") + " kr</td>") %>
                </tr>
                <tr>
                    <td colspan="3"><%# Eval("pris_tekst") %></td>
                </tr>
            </table>
        </ItemTemplate>
    </asp:Repeater>
</asp:Content>

