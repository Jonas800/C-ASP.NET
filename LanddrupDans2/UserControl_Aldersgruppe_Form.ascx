<%@ Control Language="C#" AutoEventWireup="true" CodeFile="UserControl_Aldersgruppe_Form.ascx.cs" Inherits="UserControl_Aldersgruppe_Form" %>

<asp:Panel ID="Panel_Aldersgruppe_Create" runat="server" DefaultButton="Button_Aldersgruppe_Create" Visible="false">
    USER CONTROL

    <asp:Label ID="Label_Aldersgruppe_Name" runat="server" CssClass="label_contact" Text="Navn"></asp:Label>
    <asp:TextBox ID="TextBox_Aldersgruppe_Name" runat="server" CssClass="textbox_contact"></asp:TextBox>
    <asp:RequiredFieldValidator ID="RequiredFieldValidator_Aldersgruppe_Name" ControlToValidate="TextBox_Aldersgruppe_Name" runat="server" ErrorMessage="Skal udfyldes" Display="Dynamic" ValidationGroup="Aldersgruppe" ForeColor="DarkRed"></asp:RequiredFieldValidator>

    <asp:Label ID="Label_Aldersgruppe_Description" runat="server" CssClass="label_contact" Text="Beskrivelse"></asp:Label>
    <asp:TextBox ID="TextBox_Aldersgruppe_Description" runat="server" TextMode="MultiLine" CssClass="textbox_contact textbox_description"></asp:TextBox>
    <asp:RequiredFieldValidator ID="RequiredFieldValidator_Aldersgruppe_Description" ControlToValidate="TextBox_Aldersgruppe_Description" runat="server" ErrorMessage="Skal udfyldes" Display="Dynamic" ValidationGroup="Aldersgruppe" ForeColor="DarkRed"></asp:RequiredFieldValidator>

    <asp:Button ID="Button_Aldersgruppe_Create" runat="server" Text="Upload" CssClass="button" OnClick="Button_Aldersgruppe_Create_Click" ValidationGroup="Aldersgruppe" />

    USER CONTROL
</asp:Panel>
