<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="Instruktoerer.aspx.cs" Inherits="Instruktoerer" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="center">
        <h2>Instruktører</h2>
        <asp:Repeater ID="Repeater_Instruktoerer" runat="server">
            <ItemTemplate>
                <article>
                    <img src="<%# Eval("instructor_image", "/billeder/resized/{0}") %>" />
                    <h3><%# Eval("instructor_name") %></h3>
                    <p><%# Eval("instructor_description") %></p>
                </article>
            </ItemTemplate>
        </asp:Repeater>
    </div>
</asp:Content>

