<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="Tilmeld.aspx.cs" Inherits="Tilmeld" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="formular_wrap">
        <asp:Panel ID="Panel_Users" runat="server" DefaultButton="Button_User_Create">
            <h2>Brugere</h2>

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
            <asp:CompareValidator ID="CompareValidator_User_Password" ControlToCompare="TextBox_User_Password_Repeat" ControlToValidate="TextBox_User_Password" ForeColor="DarkRed" runat="server" ErrorMessage="Kodeord er ikke ens" Display="Dynamic"></asp:CompareValidator>
            <br />

            <asp:Label ID="Label_User_Address" runat="server" Text="Adresse" CssClass="label_contact"></asp:Label>
            <asp:TextBox ID="TextBox_User_Address" runat="server" CssClass="textbox_contact"></asp:TextBox>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator_User_Address" ControlToValidate="TextBox_User_Address" runat="server" ErrorMessage="Skal udfyldes" Display="Dynamic" ValidationGroup="Users" ForeColor="DarkRed"></asp:RequiredFieldValidator>

            <asp:Label ID="Label_User_City" runat="server" Text="By" CssClass="label_contact"></asp:Label>
            <asp:TextBox ID="TextBox_User_City" runat="server" CssClass="textbox_contact"></asp:TextBox>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator_User_City" ControlToValidate="TextBox_User_City" runat="server" ErrorMessage="Skal udfyldes" Display="Dynamic" ValidationGroup="Users" ForeColor="DarkRed"></asp:RequiredFieldValidator>

            <asp:Label ID="Label_User_Postal" runat="server" Text="Postnummer" CssClass="label_contact"></asp:Label>
            <asp:TextBox ID="TextBox_User_Postal" runat="server" CssClass="textbox_contact"></asp:TextBox>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator_User_Postal" ControlToValidate="TextBox_User_Postal" runat="server" ErrorMessage="Skal udfyldes" Display="Dynamic" ValidationGroup="Users" ForeColor="DarkRed"></asp:RequiredFieldValidator>

            <asp:Label ID="Label_User_Phone" runat="server" Text="Telefonnummer (valgfri)" CssClass="label_contact"></asp:Label>
            <asp:TextBox ID="TextBox_User_Phone" runat="server" CssClass="textbox_contact"></asp:TextBox>

            <asp:Button ID="Button_User_Create" runat="server" Text="Opret" CssClass="button" OnClick="Button_User_Create_Click" ValidationGroup="Users" />
        </asp:Panel>
    </div>
</asp:Content>

