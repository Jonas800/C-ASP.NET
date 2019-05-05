<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="Kontakt.aspx.cs" Inherits="Kontakt" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <asp:Repeater ID="Repeater_Kontakt" runat="server">
        <ItemTemplate>
            <section class="kontaktOplysninger_tekst"><%# Eval("oplysning_kontakt") %></section>
            <table class="kontaktOplysninger">
                <tr>
                    <th><i class="fa fa-caret-right"></i>Adresse</th>
                </tr>
                <tr>
                    <td colspan="2"><%# Eval("oplysning_adresse") %></td>
                </tr>
                <tr>
                    <th><i class="fa fa-caret-right"></i>Telefon</th>
                    <th><i class="fa fa-caret-right"></i>Mobil</th>
                </tr>
                <tr>
                    <td><%# Eval("oplysning_telefon") %></td>
                    <td><%# Eval("oplysning_mobil") %></td>
                </tr>
                <tr>
                    <th><i class="fa fa-caret-right"></i>E-mail</th>
                </tr>
                <tr>
                    <td colspan="2"><%# Eval("oplysning_email") %></td>
                </tr>
            </table>
        </ItemTemplate>
    </asp:Repeater>
</asp:Content>

