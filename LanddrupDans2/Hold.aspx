<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="Hold.aspx.cs" Inherits="Hold" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div id="div_forside_admin">
        <h2>Hold oversigt</h2>
        <asp:Label ID="Label_Message" runat="server" Text=""></asp:Label>
        <asp:Repeater ID="Repeater_List" runat="server">
            <HeaderTemplate>
                <table>
                    <tr>
                        <th>Holdnummer <a href="?column=team_number&direction=ASC"><i class="fa fa-angle-up"></i></a><a href="?column=team_number&direction=DESC"><i class="fa fa-angle-down"></th>
                        <th>Instruktør <a href="?column=instructor_name&direction=ASC"><i class="fa fa-angle-up"></i></a><a href="?column=instructor_name&direction=DESC"><i class="fa fa-angle-down"></th>
                        <th>Stilart <a href="?column=style_name&direction=ASC"><i class="fa fa-angle-up"></i></a><a href="?column=style_name&direction=DESC"><i class="fa fa-angle-down"></th>
                        <th>Niveau <a href="?column=level_name&direction=ASC"><i class="fa fa-angle-up"></i></a><a href="?column=level_name&direction=DESC"><i class="fa fa-angle-down"></th>
                        <th>Alder <a href="?column=agegroup_name&direction=ASC"><i class="fa fa-angle-up"></i></a><a href="?column=agegroup_name&direction=DESC"><i class="fa fa-angle-down"></th>
                        <th>Pris <a href="?column=team_price&direction=ASC"><i class="fa fa-angle-up"></i></a><a href="?column=team_price&direction=DESC"><i class="fa fa-angle-down"></th>
                        <th></th>
                    </tr>
            </HeaderTemplate>
            <ItemTemplate>
                <tr>
                    <td><%# Eval("team_number") %></td>
                    <td><%# Eval("instructor_name") %></td>
                    <td><%# Eval("style_name") %></td>
                    <td><%# Eval("level_name") %></td>
                    <td><%# Eval("agegroup_name") %></td>
                    <td><%# Eval("team_price") %> kr.</td>
                    <td><a class="admin_button" href="Hold.aspx?join=true&id=<%# Eval("team_id") %>">Tilmeld</a></td>
                </tr>
            </ItemTemplate>
            <FooterTemplate>
                </table>
            </FooterTemplate>
        </asp:Repeater>
    </div>
</asp:Content>

