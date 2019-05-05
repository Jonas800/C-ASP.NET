<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="Kundeprofil.aspx.cs" Inherits="Kundeprofil" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div id="div_forside_admin">

        <h2>Profil</h2>
        <br />
        <asp:Panel ID="Panel_Users" runat="server" DefaultButton="Button_User_Create" Visible="false">
            <asp:Label ID="Label_Login_Name" runat="server" Text="Email" CssClass="label_contact"></asp:Label>
            <asp:TextBox ID="TextBox_Login_Name" runat="server" CssClass="textbox_contact"></asp:TextBox>
            <asp:RegularExpressionValidator ID="RegularExpressionValidator_Login_Name" runat="server" ErrorMessage="Ugyldig email" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*" ControlToValidate="TextBox_Login_Name" Display="Dynamic" ForeColor="DarkRed" ValidationGroup="Users"></asp:RegularExpressionValidator>

            <asp:Label ID="Label_User_Firstname" runat="server" Text="Fornavn" CssClass="label_contact"></asp:Label>
            <asp:TextBox ID="TextBox_User_Firstname" runat="server" CssClass="textbox_contact"></asp:TextBox>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator_User_Firstname" ControlToValidate="TextBox_User_Firstname" runat="server" ErrorMessage="Skal udfyldes" Display="Dynamic" ValidationGroup="Users" ForeColor="DarkRed"></asp:RequiredFieldValidator>

            <asp:Label ID="Label_User_Lastname" runat="server" Text="Efternavn" CssClass="label_contact"></asp:Label>
            <asp:TextBox ID="TextBox_User_Lastname" runat="server" CssClass="textbox_contact"></asp:TextBox>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator_User_Lastname" ControlToValidate="TextBox_User_Lastname" runat="server" ErrorMessage="Skal udfyldes" Display="Dynamic" ValidationGroup="Users" ForeColor="DarkRed"></asp:RequiredFieldValidator>

            <asp:Label ID="Label_User_Password" runat="server" Text="Kodeord" CssClass="label_contact"></asp:Label>
            <asp:TextBox ID="TextBox_User_Password" runat="server" CssClass="textbox_contact" TextMode="Password"></asp:TextBox>
            <asp:Label ID="Label_User_Password_Error" runat="server" Text="" ForeColor="DarkRed" CssClass="error_message"></asp:Label>

            <asp:Label ID="Label_User_Password_Repeat" runat="server" Text="Gentag kodeord" CssClass="label_contact"></asp:Label>
            <asp:TextBox ID="TextBox_User_Password_Repeat" runat="server" CssClass="textbox_contact" TextMode="Password"></asp:TextBox>
            <asp:CompareValidator ID="CompareValidator_User_Password" ControlToCompare="TextBox_User_Password_Repeat" ControlToValidate="TextBox_User_Password" ForeColor="DarkRed" runat="server" ErrorMessage="Kodeord er ikke ens" Display="Dynamic" ValidationGroup="Users"></asp:CompareValidator>
            <br />

            <asp:Label ID="Label_User_Address" runat="server" Text="Adresse" CssClass="label_contact"></asp:Label>
            <asp:TextBox ID="TextBox_User_Address" runat="server" CssClass="textbox_contact"></asp:TextBox>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator_User_Address" ControlToValidate="TextBox_User_Address" runat="server" ErrorMessage="Skal udfyldes" Display="Dynamic" ValidationGroup="Users" ForeColor="DarkRed"></asp:RequiredFieldValidator>

            <asp:Label ID="Label_User_City" runat="server" Text="By" CssClass="label_contact"></asp:Label>
            <asp:TextBox ID="TextBox_User_City" runat="server" CssClass="textbox_contact"></asp:TextBox>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator_User_City" ControlToValidate="TextBox_User_City" runat="server" ErrorMessage="Skal udfyldes" Display="Dynamic" ValidationGroup="Users" ForeColor="DarkRed"></asp:RequiredFieldValidator>

            <asp:Label ID="Label_User_Postal" runat="server" Text="Postnummer" CssClass="label_contact"></asp:Label>
            <asp:TextBox ID="TextBox_User_Postal" runat="server" CssClass="textbox_contact"></asp:TextBox>
            <asp:RegularExpressionValidator ID="RegularExpressionValidator_User_Postal" runat="server" ErrorMessage="Fem tal eller mindre" ValidationExpression="\d{0,5}" ControlToValidate="TextBox_User_Postal" ValidationGroup="Users" ForeColor="DarkRed"></asp:RegularExpressionValidator>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator_User_Postal" ControlToValidate="TextBox_User_Postal" runat="server" ErrorMessage="Skal udfyldes" Display="Dynamic" ValidationGroup="Users" ForeColor="DarkRed"></asp:RequiredFieldValidator>

            <asp:Label ID="Label_User_Phone" runat="server" Text="Telefonnummer (valgfri)" CssClass="label_contact"></asp:Label>
            <asp:TextBox ID="TextBox_User_Phone" runat="server" CssClass="textbox_contact"></asp:TextBox>
            <asp:HiddenField ID="HiddenField_FK_user_id" runat="server" />

            <asp:Button ID="Button_User_Create" runat="server" Text="Opret" CssClass="button" OnClick="Button_User_Create_Click" ValidationGroup="Users" />
        </asp:Panel>

        <asp:Repeater ID="Repeater_List" runat="server">
            <HeaderTemplate>
                <table>
                    <tr>
                        <th>Fornavn</th>
                        <th>Efternavn</th>
                        <th>Email</th>
                        <th>By</th>
                        <th>Postnr.</th>
                        <th>Adresse</th>
                        <th>Telefon</th>
                        <th></th>
                        <th></th>
                    </tr>
            </HeaderTemplate>
            <ItemTemplate>
                <tr>
                    <td><%# Eval("customer_firstname") %></td>
                    <td><%# Eval("customer_lastname") %></td>
                    <td><%# Eval("user_email") %></td>
                    <td><%# Eval("customer_city") %></td>
                    <td><%# Eval("customer_postal") %></td>
                    <td><%# Eval("customer_address") %></td>
                    <td><%# Eval("customer_phone") %></td>
                    <td><a class="admin_button" href="Kundeprofil.aspx?id=<%# Eval("customer_id") %>&ret=true">Rediger</a></td>
                    <td><a class="admin_button" href="Kundeprofil.aspx?id=<%# Eval("customer_id") %>&slet=true" onclick="return confirm('Er du sikker?')">Slet</a></td>
                </tr>
            </ItemTemplate>
            <FooterTemplate>
                </table>
            </FooterTemplate>
        </asp:Repeater>

        <asp:Repeater ID="Repeater_Teams" runat="server">
            <HeaderTemplate>
                <table>
                    <tr>
                        <th>Holdnummer</th>
                        <th>Stilart</th>
                        <th>Instruktør</th>
                        <th>Pris</th>
                        <th></th>
                    </tr>
            </HeaderTemplate>
            <ItemTemplate>
                <tr>
                    <td><%# Eval("team_number") %></td>
                    <td><%# Eval("style_name") %></td>
                    <td><%# Eval("instructor_name") %></td>
                    <td><%# Eval("team_price") %></td>
                    <td><a class="admin_button" href="Kundeprofil.aspx?id=<%# Eval("teams_and_customers_id") %>&afmeld=true" onclick="return confirm('Er du sikker?')">Afmeld</a></td>
                </tr>
            </ItemTemplate>
            <FooterTemplate>
                </table>
            </FooterTemplate>
        </asp:Repeater>
    </div>
</asp:Content>

