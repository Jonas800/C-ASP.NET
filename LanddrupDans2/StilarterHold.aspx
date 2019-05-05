<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="StilarterHold.aspx.cs" Inherits="StilarterHold" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div id="div_stilarter_hold">
        <asp:Label ID="Label_Message" runat="server" Text=""></asp:Label>
        <asp:Repeater ID="Repeater_Hold" runat="server" OnItemDataBound="Repeater_Hold_ItemDataBound">
            <ItemTemplate>
                <h2><%# Eval("style_name") %></h2>
                <asp:Repeater ID="Repeater_nested" runat="server">
                    <HeaderTemplate>
                        <table>
                            <tr>
                                <th>Holdnummer <a href="?column=team_number&direction=ASC"><i class="fa fa-angle-up"></i></a><a href="?column=team_number&direction=DESC"><i class="fa fa-angle-down"></th>
                                <th>Instruktør <a href="?column=instructor_name&direction=ASC"><i class="fa fa-angle-up"></i></a><a href="?column=instructor_name&direction=DESC"><i class="fa fa-angle-down"></th>
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
                            <td><%# Eval("level_name") %></td>
                            <td><%# Eval("agegroup_name") %></td>
                            <td><%# Eval("team_price") %> kr.</td>
                            <td><a class="admin_button" href="StilarterHold.aspx?join=true&id=<%# Eval("style_id") %>&team_id=<%# Eval("team_id") %>">Tilmeld</a></td>
                        </tr>
                    </ItemTemplate>
                    <FooterTemplate>
                        </table>
                    </FooterTemplate>
                </asp:Repeater>
            </ItemTemplate>
        </asp:Repeater>
    </div>
    <aside>
        <asp:Repeater ID="Repeater_Stilarter" runat="server">
            <HeaderTemplate>
                <ul>
                    <li>Andre</li>
            </HeaderTemplate>
            <ItemTemplate>
                <li>
                    <a href="StilarterHold.aspx?id=<%# Eval("style_id") %>"><%# Eval("style_name") %></a>
                </li>
            </ItemTemplate>
            <FooterTemplate>
                </ul>
            </FooterTemplate>
        </asp:Repeater>
    </aside>
</asp:Content>

