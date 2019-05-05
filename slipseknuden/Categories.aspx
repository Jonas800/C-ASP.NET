<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="Categories.aspx.cs" Inherits="Categories" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Main" runat="Server">

    <div class="wrapper_categories">
        <asp:Panel ID="Panel_List" runat="server" Visible="false">
            <h2>Kategorier</h2>
            <asp:Repeater ID="Repeater_List" runat="server">
                <HeaderTemplate>
                    <ul>
                </HeaderTemplate>
                <ItemTemplate>
                    <li>
                        <a href="Categories.aspx?id=<%# Eval("category_id") %>"><%# Eval("category_name") %></a>
                    </li>
                </ItemTemplate>
                <FooterTemplate></ul></FooterTemplate>
            </asp:Repeater>
        </asp:Panel>
        <asp:Repeater ID="Repeater_Categories_Title" runat="server">
            <ItemTemplate>
                <h2><%# Eval("category_name") %></h2>
            </ItemTemplate>
        </asp:Repeater>
        <asp:Repeater ID="Repeater_Categories_Products" runat="server">
            <HeaderTemplate>
                <div class="center">
            </HeaderTemplate>
            <ItemTemplate>
                <a href="Details.aspx?id=<%# Eval("product_id") %>">
                    <figure>
                        <figcaption><%# Eval("product_name") %></figcaption>
                        <img src="<%# Eval("product_image", "/billeder/thumbs/{0}") %>" />
                        <figcaption><%# Eval("product_price") %> kr,-</figcaption>
                    </figure>
                </a>
            </ItemTemplate>
            <FooterTemplate></div></FooterTemplate>
        </asp:Repeater>
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
</asp:Content>

