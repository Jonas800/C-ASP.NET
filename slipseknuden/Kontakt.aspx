<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="Kontakt.aspx.cs" Inherits="Kontakt" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Main" runat="Server">
    <div class="wrapper">
        <asp:Panel ID="Panel_Contact" runat="server" DefaultButton="Button_Contact">
            <h2>Kontakt</h2>
            <asp:Label ID="Label_Contact_Name" runat="server" Text="Navn" CssClass="label_contact"></asp:Label>
            <asp:TextBox ID="TextBox_Contact_Name" runat="server" CssClass="textbox_contact"></asp:TextBox>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator_Contact_Name" runat="server" ErrorMessage="Skal udfyldes" ControlToValidate="TextBox_Contact_Name" ForeColor="DarkRed" Display="Dynamic"></asp:RequiredFieldValidator>

            <asp:Label ID="Label_Contact_Email" runat="server" Text="Email" CssClass="label_contact"></asp:Label>
            <asp:TextBox ID="TextBox_Contact_Email" runat="server" CssClass="textbox_contact"></asp:TextBox>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator_Contact_Email" runat="server" ErrorMessage="Skal udfyldes" ControlToValidate="TextBox_Contact_Email" ForeColor="DarkRed" Display="Dynamic"></asp:RequiredFieldValidator>
            <asp:RegularExpressionValidator ID="RegularExpressionValidator_Contact_Email" ControlToValidate="TextBox_Contact_Email" runat="server" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*" ErrorMessage="Skal være en gyldig email" ForeColor="DarkRed"></asp:RegularExpressionValidator>

            <asp:Label ID="Label_Contact_Subject" runat="server" CssClass="label_contact" Text="Emne"></asp:Label>
            <asp:TextBox ID="TextBox_Contact_Subject" CssClass="textbox_contact" runat="server"></asp:TextBox>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator_Contact_Subject" runat="server" ErrorMessage="Skal udfyldes" ControlToValidate="TextBox_Contact_Subject" ForeColor="DarkRed" Display="Dynamic"></asp:RequiredFieldValidator>

            <asp:Label ID="Label_Contact_Message" runat="server" CssClass="label_contact" Text="Besked"></asp:Label>
            <asp:TextBox ID="TextBox_Contact_Message" TextMode="MultiLine" CssClass="textbox_contact" runat="server"></asp:TextBox>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator_Contact_Message" runat="server" ErrorMessage="Skal udfyldes" ControlToValidate="TextBox_Contact_Message" ForeColor="DarkRed" Display="Dynamic"></asp:RequiredFieldValidator>
            <br />
            <asp:Button ID="Button_Contact" runat="server" Text="Send" CssClass="button" />
            <br />
            
        </asp:Panel>
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
</asp:Content>

