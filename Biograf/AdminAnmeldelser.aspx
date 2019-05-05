<%@ Page Title="" Language="C#" MasterPageFile="~/AdminMasterPage.master" AutoEventWireup="true" CodeFile="AdminAnmeldelser.aspx.cs" Inherits="AdminAnmeldelser" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <h1>Anmeldelser</h1>
    <asp:Repeater ID="Repeater_Anmeldelser" runat="server">
        <HeaderTemplate><table class="Admin_Anmeldelser_Table">
            <tr>
                <th>
                    Bruger
                </th>
                <th>
                    Film
                </th>
                <th>
                    Anmeldelse
                </th>
                <th>
                    Slet
                </th>
            </tr></HeaderTemplate>
        <ItemTemplate>
            <tr>
                <td><%# Eval("bruger_navn") %></td>
                <td><%# Eval("film_navn") %></td>
                <td class="Anmeldelse_td"><%# Eval("anmeldelse_tekst") %></td>
                <td><a href="?id=<%# Eval("anmeldelse_id") %>&slet=true"><i class="fa fa-trash" aria-hidden="true"></a></td>
            </tr>
        </ItemTemplate>
        <FooterTemplate></table></FooterTemplate>
    </asp:Repeater>
</asp:Content>

