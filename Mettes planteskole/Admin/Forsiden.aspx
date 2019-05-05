<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/MasterPage.master" AutoEventWireup="true" CodeFile="Forsiden.aspx.cs" Inherits="Admin_Forsiden" %>

<%@ Register Assembly="CKEditor.NET" Namespace="CKEditor.NET" TagPrefix="CKEditor" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <h2>Forsiden</h2>

    <label>Titel</label>
    <asp:TextBox ID="TextBox_Titel" runat="server"></asp:TextBox>
    <asp:RequiredFieldValidator ID="RequiredFieldValidator_Titel" runat="server" ControlToValidate="TextBox_Titel" ErrorMessage="Skal udfyldes" Display="Dynamic"></asp:RequiredFieldValidator>
    <label>Tekst</label>
    <asp:TextBox ID="TextBox_Tekst" TextMode="MultiLine" runat="server"></asp:TextBox>
    <asp:RequiredFieldValidator ID="RequiredFieldValidator_Tekst" runat="server" ControlToValidate="TextBox_Tekst" ErrorMessage="Skal udfyldes" Display="Dynamic"></asp:RequiredFieldValidator>
    <label>Billede</label>
    <asp:Image ID="Image_Billede" runat="server" />
    <asp:HiddenField ID="HiddenField_Billede" runat="server" />
    <asp:FileUpload ID="FileUpload_Billede" runat="server" />

    

    <CKEditor:CKEditorControl ID="CKEditor1" BasePath="../CKeditor/" runat="server" Toolbar="Full"></CKEditor:CKEditorControl>


    <asp:Button ID="Button_Gem" runat="server" Text="Gem" OnClick="Button_Gem_Click" />
</asp:Content>

