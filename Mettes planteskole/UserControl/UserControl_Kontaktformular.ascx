<%@ Control Language="C#" AutoEventWireup="true" CodeFile="UserControl_Kontaktformular.ascx.cs" Inherits="UserControl_Kontaktformular" %>
<div class="form">
    <asp:Panel ID="Panel_Kontaktformular" DefaultButton="Button_Send" runat="server">
        <label>Navn</label>
        <asp:TextBox ID="TextBox_Navn" runat="server" ValidationGroup="Kontakt"></asp:TextBox>
        <asp:RequiredFieldValidator ID="RequiredFieldValidator_Navn" runat="server" ControlToValidate="TextBox_Navn" ErrorMessage="Skal udfyldes" Display="Dynamic"></asp:RequiredFieldValidator>

        <label>Emne</label>
        <asp:TextBox ID="TextBox_Emne" runat="server" ValidationGroup="Kontakt"></asp:TextBox>
        <asp:RequiredFieldValidator ID="RequiredFieldValidator_Emne" runat="server" ControlToValidate="TextBox_Emne" ErrorMessage="Skal udfyldes" Display="Dynamic"></asp:RequiredFieldValidator>

        <label>Email</label>
        <asp:TextBox ID="TextBox_Email" runat="server" ValidationGroup="Kontakt"></asp:TextBox>
        <asp:RequiredFieldValidator ID="RequiredFieldValidator_Email" runat="server" ControlToValidate="TextBox_Email" ErrorMessage="Skal udfyldes" Display="Dynamic"></asp:RequiredFieldValidator>
        <asp:RegularExpressionValidator ID="RegularExpressionValidator_Email" runat="server" ErrorMessage="Ikke en gyldig email" ControlToValidate="TextBox_Email" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*" Display="Dynamic"></asp:RegularExpressionValidator>

        <label class="multiline_label">Besked</label>
        <asp:TextBox ID="TextBox_Besked" TextMode="MultiLine" runat="server" ValidationGroup="Kontakt"></asp:TextBox>
        <asp:RequiredFieldValidator ID="RequiredFieldValidator_Besked" runat="server" ControlToValidate="TextBox_Besked" ErrorMessage="Skal udfyldes" Display="Dynamic"></asp:RequiredFieldValidator>


        <asp:Button ID="Button_Send" runat="server" OnClick="Button_Send_Click" Text="Send" ValidationGroup="Kontakt" />
        <asp:Label ID="Label_Fejl" runat="server"></asp:Label>
    </asp:Panel>
</div>
