<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="Stilarter.aspx.cs" Inherits="Stilarter" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="center">
        <h2>Stilarter</h2>
        <asp:Repeater ID="Repeater_Stilarter" runat="server">
            <ItemTemplate>
                <article>
                    <a href="StilarterHold.aspx?id=<%# Eval("style_id") %>">
                        <img src="<%# Eval("style_image", "/billeder/resized/{0}") %>" />
                    </a>
                    <h3><%# Eval("style_name") %></h3>
                    <p><%# Eval("style_description") %></p>
                    <a href="StilarterHold.aspx?id=<%# Eval("style_id") %>">Se hold!</a>
                </article>
            </ItemTemplate>
        </asp:Repeater>
    </div>
</asp:Content>

