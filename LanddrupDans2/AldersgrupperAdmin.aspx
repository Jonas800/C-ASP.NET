<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPageAdmin.master" AutoEventWireup="true" CodeFile="AldersgrupperAdmin.aspx.cs" Inherits="AldersgrupperAdmin" %>

<%@ Register Src="~/UserControl_Aldersgruppe_Form.ascx" TagPrefix="uc1" TagName="UserControl_Aldersgruppe_Form" %>


<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div id="div_forside_admin">

        <h2>Aldersgrupper</h2>
        <a class="admin_button" href="?opret=true">Opret ny aldersgruppe</a>
        <br />
        <uc1:UserControl_Aldersgruppe_Form runat="server" id="UserControl_Aldersgruppe_Form" />
        <%--<asp:Panel ID="Panel_Aldersgruppe_Create" runat="server" DefaultButton="Button_Aldersgruppe_Create" Visible="false">

            <asp:Label ID="Label_Aldersgruppe_Name" runat="server" CssClass="label_contact" Text="Navn"></asp:Label>
            <asp:TextBox ID="TextBox_Aldersgruppe_Name" runat="server" CssClass="textbox_contact"></asp:TextBox>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator_Aldersgruppe_Name" ControlToValidate="TextBox_Aldersgruppe_Name" runat="server" ErrorMessage="Skal udfyldes" Display="Dynamic" ValidationGroup="Aldersgruppe" ForeColor="DarkRed"></asp:RequiredFieldValidator>

            <asp:Label ID="Label_Aldersgruppe_Description" runat="server" CssClass="label_contact" Text="Beskrivelse"></asp:Label>
            <asp:TextBox ID="TextBox_Aldersgruppe_Description" runat="server" TextMode="MultiLine" CssClass="textbox_contact textbox_description"></asp:TextBox>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator_Aldersgruppe_Description" ControlToValidate="TextBox_Aldersgruppe_Description" runat="server" ErrorMessage="Skal udfyldes" Display="Dynamic" ValidationGroup="Aldersgruppe" ForeColor="DarkRed"></asp:RequiredFieldValidator>

            <asp:Button ID="Button_Aldersgruppe_Create" runat="server" Text="Upload" CssClass="button" OnClick="Button_Aldersgruppe_Create_Click" ValidationGroup="Aldersgruppe" />

        </asp:Panel>--%>
        <asp:Repeater ID="Repeater_List" runat="server">
            <HeaderTemplate>
                <table>
                    <tr>
                        <th>ID <a href="?column=agegroup_id&direction=ASC"><i class="fa fa-angle-up"></i></a><a href="?column=agegroup_id&direction=DESC"><i class="fa fa-angle-down"></th>
                        <th>Navn <a href="?column=agegroup_name&direction=ASC"><i class="fa fa-angle-up"></i></a><a href="?column=agegroup_name&direction=DESC"><i class="fa fa-angle-down"></th>
                        <th>Beskrivelse</th>
                        <th></th>
                        <th></th>
                    </tr>
            </HeaderTemplate>
            <ItemTemplate>
                <tr>
                    <td><%# Eval("agegroup_id") %></td>
                    <td><%# Eval("agegroup_name") %></td>
                    <td><%# Eval("agegroup_description").ToString().Replace(Environment.NewLine,"<br />") %></td>
                    <td><a class="admin_button" href="AldersgrupperAdmin.aspx?id=<%# Eval("agegroup_id") %>&ret=true">Rediger</a></td>
                    <td><a class="admin_button" href="AldersgrupperAdmin.aspx?id=<%# Eval("agegroup_id") %>&slet=true" onclick="return confirm('Er du sikker?')">Slet</a></td>
                </tr>
            </ItemTemplate>
            <FooterTemplate>
                </table>
            </FooterTemplate>
        </asp:Repeater>
    </div>
</asp:Content>

