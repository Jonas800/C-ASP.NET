<%@ Control Language="C#" AutoEventWireup="true" CodeFile="UserControl_Opret.ascx.cs" Inherits="UserControl_Opret" %>
<div class="form">
    <h2>Opret</h2>
    <asp:Panel ID="Panel_Opret" runat="server" DefaultButton="Button_Opret">
        <h3>Dette bruges til login</h3>
        <label>Email</label>
        <asp:TextBox ID="TextBox_Email" runat="server"></asp:TextBox>
        <asp:RequiredFieldValidator ID="RequiredFieldValidator_Email" runat="server" ControlToValidate="TextBox_Email" ErrorMessage="Skal udfyldes" Display="Dynamic"></asp:RequiredFieldValidator>
        <asp:RegularExpressionValidator ID="RegularExpressionValidator_Email" runat="server" ErrorMessage="Ikke en gyldig email" ControlToValidate="TextBox_Email" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*" Display="Dynamic"></asp:RegularExpressionValidator>
        <asp:Label ID="Label_Error" runat="server"></asp:Label>

        <label>Kodeord</label>
        <asp:TextBox ID="TextBox_Kodeord" runat="server"></asp:TextBox>
        <asp:RequiredFieldValidator ID="RequiredFieldValidator_Kodeord" runat="server" ControlToValidate="TextBox_Kodeord" ErrorMessage="Skal udfyldes" Display="Dynamic"></asp:RequiredFieldValidator>
        <label>Gentag kodeord</label>
        <asp:TextBox ID="TextBox_Kodeord_Gentag" runat="server"></asp:TextBox>
        <asp:CompareValidator ID="CompareValidator_Kodeord" Display="Dynamic" runat="server" ControlToCompare="TextBox_Kodeord_Gentag" ControlToValidate="TextBox_Kodeord" ErrorMessage="Kodeord er ikke ens"></asp:CompareValidator>

        <h3>Dette bruges til køb og kontakt</h3>
        <label>Navn</label>
        <asp:TextBox ID="TextBox_Navn" runat="server"></asp:TextBox>
        <asp:RequiredFieldValidator ID="RequiredFieldValidator_Navn" runat="server" ControlToValidate="TextBox_Navn" ErrorMessage="Skal udfyldes" Display="Dynamic"></asp:RequiredFieldValidator>

        <label>By</label>
        <asp:TextBox ID="TextBox_By" runat="server"></asp:TextBox>
        <asp:RequiredFieldValidator ID="RequiredFieldValidator_By" runat="server" ControlToValidate="TextBox_By" ErrorMessage="Skal udfyldes" Display="Dynamic"></asp:RequiredFieldValidator>

        <label>Postnummer</label>
        <asp:TextBox ID="TextBox_Postnummer" runat="server"></asp:TextBox>
        <asp:RequiredFieldValidator ID="RequiredFieldValidator_Postnummer" ControlToValidate="TextBox_Postnummer" runat="server" ErrorMessage="Skal udfyldes" Display="Dynamic"></asp:RequiredFieldValidator>
        <asp:CompareValidator ID="CompareValidator_Postnummer" runat="server" Operator="DataTypeCheck" Type="Integer"
            ControlToValidate="TextBox_Postnummer" Display="Dynamic" ErrorMessage="Skal være hele tal" />

        <label>Adresse</label>
        <asp:TextBox ID="TextBox_Adresse" runat="server"></asp:TextBox>
        <asp:RequiredFieldValidator ID="RequiredFieldValidator_Adresse" ControlToValidate="TextBox_Adresse" runat="server" ErrorMessage="Skal udfyldes" Display="Dynamic"></asp:RequiredFieldValidator>

        <label>Telefon</label>
        <asp:TextBox ID="TextBox_Telefon" runat="server"></asp:TextBox>
        <asp:RequiredFieldValidator ID="RequiredFieldValidator_Telefon" ControlToValidate="TextBox_Telefon" runat="server" ErrorMessage="Skal udfyldes" Display="Dynamic"></asp:RequiredFieldValidator>
        <asp:CompareValidator ID="CompareValidator_Telefon" runat="server" Operator="DataTypeCheck" Type="Integer" Display="Dynamic"
            ControlToValidate="TextBox_Telefon" ErrorMessage="Skal være hele tal" />

        <asp:Button ID="Button_Opret" runat="server" Text="Opret" OnClick="Button_Opret_Click" />
    </asp:Panel>
    <asp:Label ID="Label_Success" runat="server" Text="Velkommen, du er nu logget ind" Visible="false"></asp:Label>
</div>
