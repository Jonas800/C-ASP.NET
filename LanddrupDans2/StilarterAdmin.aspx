<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPageAdmin.master" AutoEventWireup="true" CodeFile="StilarterAdmin.aspx.cs" Inherits="StilarterAdmin" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div id="div_forside_admin">

        <h2>Stilarter</h2>
        <a class="admin_button" href="?opret=true">Opret ny stilart</a>
        <br />
        <asp:Panel ID="Panel_Styles_Create" runat="server" DefaultButton="Button_Styles_Create" Visible="false">

            <asp:Label ID="Label_Styles_Name" runat="server" CssClass="label_contact" Text="Navn"></asp:Label>
            <asp:TextBox ID="TextBox_Styles_Name" runat="server" CssClass="textbox_contact"></asp:TextBox>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator_Styles_Name" ControlToValidate="TextBox_Styles_Name" runat="server" ErrorMessage="Skal udfyldes" Display="Dynamic" ValidationGroup="Styless" ForeColor="DarkRed"></asp:RequiredFieldValidator>

            <asp:Label ID="Label_Styles_Description" runat="server" CssClass="label_contact" Text="Beskrivelse"></asp:Label>
            <asp:TextBox ID="TextBox_Styles_Description" runat="server" TextMode="MultiLine" CssClass="textbox_contact textbox_description"></asp:TextBox>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator_Styles_Description" ControlToValidate="TextBox_Styles_Description" runat="server" ErrorMessage="Skal udfyldes" Display="Dynamic" ValidationGroup="Styless" ForeColor="DarkRed"></asp:RequiredFieldValidator>

            <asp:Label ID="Label_Styles_Image" runat="server" CssClass="label_contact" Text="Upload billede"></asp:Label>
            <asp:Image ID="Image_Old_Picture" runat="server" /><br />

            <asp:FileUpload ID="FileUpload_Styles_Image" runat="server" CssClass="fileupload" />
            <asp:Label ID="Label_Styles_Image_Error" runat="server" CssClass="error_message"></asp:Label>
            <asp:HiddenField ID="HiddenField_Old_Picture" runat="server" />

            <asp:Button ID="Button_Styles_Create" runat="server" Text="Upload" CssClass="button" OnClick="Button_Styles_Create_Click" ValidationGroup="Styless" />

        </asp:Panel>
        <asp:Repeater ID="Repeater_List" runat="server">
            <HeaderTemplate>
                <table>
                    <tr>
                        <th>ID <a href="?column=style_id&direction=ASC"><i class="fa fa-angle-up"></i></a><a href="?column=style_id&direction=DESC"><i class="fa fa-angle-down"></i></a></th>
                        <th>Billede</th>
                        <th>Navn <a href="?column=style_name&direction=ASC"><i class="fa fa-angle-up"></i></a><a href="?column=style_name&direction=DESC"><i class="fa fa-angle-down"></i></a></th>
                        <th>Beskrivelse</th>
                        <th></th>
                        <th></th>
                    </tr>
            </HeaderTemplate>
            <ItemTemplate>
                <tr>
                    <td><%# Eval("style_id") %></td>
                    <td>
                        <img src="<%# Eval("style_image", "/billeder/thumbs/{0}") %>" /></td>
                    <td><%# Eval("style_name") %></td>
                    <td><%# Eval("style_description").ToString().Replace(Environment.NewLine,"<br />") %></td>
                    <td><a class="admin_button" href="StilarterAdmin.aspx?id=<%# Eval("style_id") %>&ret=true">Rediger</a></td>
                    <td><a class="admin_button" href="StilarterAdmin.aspx?id=<%# Eval("style_id") %>&slet=true" onclick="return confirm('Er du sikker?')">Slet</a></td>
                </tr>
            </ItemTemplate>
            <FooterTemplate>
                </table>
            </FooterTemplate>
        </asp:Repeater>
    </div>
</asp:Content>

