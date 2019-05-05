<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPageAdmin.master" AutoEventWireup="true" CodeFile="InstruktoererAdmin.aspx.cs" Inherits="InstruktoererAdmin" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div id="div_forside_admin">

        <h2>Instruktører</h2>
        <a class="admin_button" href="?opret=true">Opret ny instruktør</a>
        <br />
        <asp:Panel ID="Panel_Instruktoerer_Create" runat="server" DefaultButton="Button_Instruktoerer_Create" Visible="false">

            <asp:Label ID="Label_Instruktoerer_Name" runat="server" CssClass="label_contact" Text="Navn"></asp:Label>
            <asp:TextBox ID="TextBox_Instruktoerer_Name" runat="server" CssClass="textbox_contact"></asp:TextBox>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator_Instruktoerer_Name" ControlToValidate="TextBox_Instruktoerer_Name" runat="server" ErrorMessage="Skal udfyldes" Display="Dynamic" ValidationGroup="Instruktoerers" ForeColor="DarkRed"></asp:RequiredFieldValidator>

            <asp:Label ID="Label_Login_Name" runat="server" Text="Email" CssClass="label_contact"></asp:Label>
            <asp:TextBox ID="TextBox_Login_Name" runat="server" CssClass="textbox_contact"></asp:TextBox>
            <asp:RegularExpressionValidator ID="RegularExpressionValidator_Login_Name" runat="server" ErrorMessage="Ugyldig email" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*" ControlToValidate="TextBox_Login_Name" Display="Dynamic" ForeColor="DarkRed"></asp:RegularExpressionValidator>

            <asp:Label ID="Label_User_Password" runat="server" Text="Kodeord" CssClass="label_contact"></asp:Label>
            <asp:TextBox ID="TextBox_User_Password" runat="server" CssClass="textbox_contact" TextMode="Password"></asp:TextBox>
            <asp:Label ID="Label_User_Password_Error" runat="server" Text="" ForeColor="DarkRed" CssClass="error_message"></asp:Label>

            <asp:Label ID="Label_User_Password_Repeat" runat="server" Text="Gentag kodeord" CssClass="label_contact"></asp:Label>
            <asp:TextBox ID="TextBox_User_Password_Repeat" runat="server" CssClass="textbox_contact" TextMode="Password"></asp:TextBox>
            <asp:CompareValidator ID="CompareValidator_User_Password" ControlToCompare="TextBox_User_Password_Repeat" ControlToValidate="TextBox_User_Password" ForeColor="DarkRed" runat="server" ErrorMessage="Kodeord er ikke ens" Display="Dynamic"></asp:CompareValidator>

            <asp:Label ID="Label_Instruktoerer_Description" runat="server" CssClass="label_contact" Text="Beskrivelse"></asp:Label>
            <asp:TextBox ID="TextBox_Instruktoerer_Description" runat="server" TextMode="MultiLine" CssClass="textbox_contact textbox_description"></asp:TextBox>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator_Instruktoerer_Description" ControlToValidate="TextBox_Instruktoerer_Description" runat="server" ErrorMessage="Skal udfyldes" Display="Dynamic" ValidationGroup="Instruktoerers" ForeColor="DarkRed"></asp:RequiredFieldValidator>

            <asp:Label ID="Label_Instruktoerer_Image" runat="server" CssClass="label_contact" Text="Upload billede"></asp:Label>
            <asp:Image ID="Image_Old_Picture" runat="server" /><br />

            <asp:FileUpload ID="FileUpload_Instruktoerer_Image" runat="server" CssClass="fileupload" />
            <asp:Label ID="Label_Instruktoerer_Image_Error" runat="server" CssClass="error_message"></asp:Label>
            <asp:HiddenField ID="HiddenField_Old_Picture" runat="server" />
            <asp:HiddenField ID="HiddenField_FK_user_id" runat="server" />

            <asp:Button ID="Button_Instruktoerer_Create" runat="server" Text="Upload" CssClass="button" OnClick="Button_Instruktoerer_Create_Click" ValidationGroup="Instruktoerers" />

        </asp:Panel>
        <asp:Repeater ID="Repeater_List" runat="server">
            <HeaderTemplate>
                <table>
                    <tr>
                        <th>ID <a href="?column=instructor_id&direction=ASC"><i class="fa fa-angle-up"></i></a><a href="?column=instructor_id&direction=DESC"><i class="fa fa-angle-down"></th>
                        <th>Billede</th>
                        <th>Navn <a href="?column=instructor_name&direction=ASC"><i class="fa fa-angle-up"></i></a><a href="?column=instructor_name&direction=DESC"><i class="fa fa-angle-down"></th>
                        <th>Beskrivelse</th>
                        <th></th>
                        <th></th>
                    </tr>
            </HeaderTemplate>
            <ItemTemplate>
                <tr>
                    <td><%# Eval("instructor_id") %></td>
                    <td>
                        <img src="<%# Eval("instructor_image", "/billeder/thumbs/{0}") %>" /></td>
                    <td><%# Eval("instructor_name") %></td>
                    <td><%# Eval("instructor_description").ToString().Replace(Environment.NewLine,"<br />") %></td>
                    <td><a class="admin_button" href="InstruktoererAdmin.aspx?id=<%# Eval("instructor_id") %>&ret=true">Rediger</a></td>
                    <td><a class="admin_button" href="InstruktoererAdmin.aspx?id=<%# Eval("instructor_id") %>&slet=true" onclick="return confirm('Er du sikker?')">Slet</a></td>
                </tr>
            </ItemTemplate>
            <FooterTemplate>
                </table>
            </FooterTemplate>
        </asp:Repeater>
    </div>
</asp:Content>

