<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPageAdmin.master" AutoEventWireup="true" CodeFile="NiveauAdmin.aspx.cs" Inherits="NiveauAdmin" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div id="div_forside_admin">

        <h2>Niveauer</h2>
        <a class="admin_button" href="?opret=true">Opret nyt niveau</a>
        <br />
        <asp:Panel ID="Panel_Niveau_Create" runat="server" DefaultButton="Button_Niveau_Create" Visible="false">

            <asp:Label ID="Label_Niveau_Name" runat="server" CssClass="label_contact" Text="Navn"></asp:Label>
            <asp:TextBox ID="TextBox_Niveau_Name" runat="server" CssClass="textbox_contact"></asp:TextBox>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator_Niveau_Name" ControlToValidate="TextBox_Niveau_Name" runat="server" ErrorMessage="Skal udfyldes" Display="Dynamic" ValidationGroup="Niveau" ForeColor="DarkRed"></asp:RequiredFieldValidator>

            <asp:Label ID="Label_Niveau_Description" runat="server" CssClass="label_contact" Text="Beskrivelse"></asp:Label>
            <asp:TextBox ID="TextBox_Niveau_Description" runat="server" TextMode="MultiLine" CssClass="textbox_contact textbox_description"></asp:TextBox>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator_Niveau_Description" ControlToValidate="TextBox_Niveau_Description" runat="server" ErrorMessage="Skal udfyldes" Display="Dynamic" ValidationGroup="Niveau" ForeColor="DarkRed"></asp:RequiredFieldValidator>

            <asp:Button ID="Button_Niveau_Create" runat="server" Text="Upload" CssClass="button" OnClick="Button_Niveau_Create_Click" ValidationGroup="Niveau" />

        </asp:Panel>
        <asp:Repeater ID="Repeater_List" runat="server">
            <HeaderTemplate>
                <table>
                    <tr>
                        <th>ID <a href="?column=level_id&direction=ASC"><i class="fa fa-angle-up"></i></a><a href="?column=level_id&direction=DESC"><i class="fa fa-angle-down"></th>
                        <th>Navn <a href="?column=level_name&direction=ASC"><i class="fa fa-angle-up"></i></a><a href="?column=level_name&direction=DESC"><i class="fa fa-angle-down"></th>
                        <th>Beskrivelse</th>
                        <th></th>
                        <th></th>
                    </tr>
            </HeaderTemplate>
            <ItemTemplate>
                <tr>
                    <td><%# Eval("level_id") %></td>
                    <td><%# Eval("level_name") %></td>
                    <td><%# Eval("level_description").ToString().Replace(Environment.NewLine,"<br />") %></td>
                    <td><a class="admin_button" href="NiveauAdmin.aspx?id=<%# Eval("level_id") %>&ret=true">Rediger</a></td>
                    <td><a class="admin_button" href="NiveauAdmin.aspx?id=<%# Eval("level_id") %>&slet=true" onclick="return confirm('Er du sikker?')">Slet</a></td>
                </tr>
            </ItemTemplate>
            <FooterTemplate>
                </table>
            </FooterTemplate>
        </asp:Repeater>
    </div>
</asp:Content>

