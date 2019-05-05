<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/MasterPage.master" AutoEventWireup="true" CodeFile="Aktiviteter.aspx.cs" Inherits="Admin_Aktiviteter" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <a class="a_opretprodukt" href="OpretAktivitet.aspx">Opret ny aktivitet</a>

    <h3>Aktiviteter</h3>
    <div class="responsivTable">
        <asp:Repeater ID="Repeater_Aktiviteter" runat="server">
            <HeaderTemplate>
                <table>
                    <tr>
                        <th>Billede</th>
                        <th>Navn</th>
                        <th>Beskrivelse</th>
                        <th>Ret</th>
                        <th>Slet</th>
                    </tr>
            </HeaderTemplate>
            <ItemTemplate>
                <tr>
                    <td><img src="<%# Eval("aktivitet_billede", "/billeder/thumbs/{0}") %>"/></td>
                    <td><%# Eval("aktivitet_navn") %></td>
                    <td><%# Eval("aktivitet_tekst").ToString().PadRight(30).Substring(0,30).TrimEnd() %></td>
                    <td><a href="OpretAktivitet.aspx?id=<%# Eval("aktivitet_id") %>"><i class="fa fa-edit"></i></a></td>
                    <td><a class="sletTd" href="Aktiviteter.aspx?id=<%# Eval("aktivitet_id") %>" onclick="return confirm('Er du sikker?')"><i class="fa fa-trash"></i></a></td>
                </tr>
            </ItemTemplate>
            <FooterTemplate>
                </table>
            </FooterTemplate>
        </asp:Repeater>
    </div>
</asp:Content>

