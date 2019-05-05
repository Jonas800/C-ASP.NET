<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="_Default" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content_Image" runat="server" ContentPlaceHolderID="ContentPlaceHolder_Image">
    <div id="div_forside_billede">
        <asp:Repeater ID="Repeater_Forside_Billede" runat="server">
            <ItemTemplate>
                <img src="<%# Eval("forside_image", "/billeder/resized/{0}") %>" />
            </ItemTemplate>
        </asp:Repeater>
    </div>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div id="div_forside_tekst">
        <p>
            <asp:Repeater ID="Repeater_Forside_Text" runat="server">
                <ItemTemplate><%# Eval("forside_text").ToString().Replace(Environment.NewLine,"<br />") %></ItemTemplate>
            </asp:Repeater>
        </p>
    </div>
</asp:Content>

