<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="Details.aspx.cs" Inherits="Details" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Main" runat="Server">
    <div class="wrapper_details">
        <asp:Repeater ID="Repeater_Details" runat="server">
            <ItemTemplate>
                <article>
                    <h2><%# Eval("product_name") %></h2>
                    <p><%# Eval("product_description").ToString().Replace(Environment.NewLine,"<br />") %></p>
                    <figure>
                        <img src="<%# Eval("product_image", "/billeder/resized/{0}") %>" />
                        <figcaption><%# Eval("product_price") %> kr,-</figcaption>
                    </figure>
                </article>
                <a href="Categories.aspx?id=<%# Eval("fk_category_id") %>">Tilbage til kategori</a>
            </ItemTemplate>
        </asp:Repeater>
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
</asp:Content>

