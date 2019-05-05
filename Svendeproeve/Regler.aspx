<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="Regler.aspx.cs" Inherits="Regler" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <asp:Repeater ID="Repeater_Regler" runat="server">
        <ItemTemplate>
            <section class="kontaktOplysninger_tekst">
                <%# Eval("oplysning_regler") %>
            </section>
        </ItemTemplate>
    </asp:Repeater>
</asp:Content>

