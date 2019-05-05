<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/MasterPage.master" AutoEventWireup="true" CodeFile="Oplysninger.aspx.cs" Inherits="Admin_Oplysninger" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">


    <h3>Hovedoplysninger</h3>

    <label for="TextBox_Åbningstider">Åbningstider</label>
    <asp:TextBox ID="TextBox_Åbningstider" runat="server" CssClass="ckeditor" TextMode="MultiLine" Rows="7"></asp:TextBox>

    <label for="TextBox_Footer">Footer</label>
    <asp:TextBox ID="TextBox_Footer" runat="server" CssClass="ckeditor" TextMode="MultiLine" Rows="3"></asp:TextBox>

    <h3>Tekster</h3>

    <label for="TextBox_Forside">Forside</label>
    <asp:TextBox ID="TextBox_Forside" runat="server" CssClass="ckeditor" TextMode="MultiLine" Rows="5"></asp:TextBox>

        <label for="TextBox_Medlem">Bliv medlem</label>
    <asp:TextBox ID="TextBox_Medlem" runat="server" CssClass="ckeditor" TextMode="MultiLine" Rows="5"></asp:TextBox>

    <h3>Kontaktsiden</h3>

    <label for="TextBox_Kontaktoplysninger">Kontakt</label>
    <asp:TextBox ID="TextBox_Kontaktoplysninger" runat="server" CssClass="ckeditor" TextMode="MultiLine" Rows="5"></asp:TextBox>

    <label for="TextBox_Adresse">Adresse</label>
    <asp:TextBox ID="TextBox_Adresse" runat="server"></asp:TextBox>
    <label for="TextBox_Telefon">Telefon</label>
    <asp:TextBox ID="TextBox_Telefon" runat="server"></asp:TextBox>
    <label for="TextBox_Mobil">Mobil</label>
    <asp:TextBox ID="TextBox_Mobil" runat="server"></asp:TextBox>

    <label for="TextBox_Email">Email</label>
    <asp:TextBox ID="TextBox_Email" runat="server"></asp:TextBox>
    <asp:RequiredFieldValidator ID="RequiredFieldValidator_Email" runat="server" ControlToValidate="TextBox_Email" ErrorMessage="Skal udfyldes" Display="Dynamic"></asp:RequiredFieldValidator>
    <asp:RegularExpressionValidator ID="RegularExpressionValidator_Email" runat="server" ErrorMessage="Ikke en gyldig email" ControlToValidate="TextBox_Email" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*" Display="Dynamic"></asp:RegularExpressionValidator>

    <h3>Reglersiden</h3>

    <label for="TextBox_Regler">Regler</label>
    <asp:TextBox ID="TextBox_Regler" runat="server" CssClass="ckeditor" TextMode="MultiLine" Rows="10"></asp:TextBox>

    <script src="ckeditor/ckeditor.js"></script>
    <asp:Button ID="Button_Gem" runat="server" Text="Gem" OnClick="Button_Gem_Click" />

</asp:Content>

