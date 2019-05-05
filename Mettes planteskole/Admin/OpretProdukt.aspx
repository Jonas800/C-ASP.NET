<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/MasterPage.master" AutoEventWireup="true" CodeFile="OpretProdukt.aspx.cs" Inherits="Admin_OpretProdukt" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <h2>Opret produkt</h2>

    <label>Varenummer</label>
    <asp:TextBox ID="TextBox_Varenummer" runat="server"></asp:TextBox>
    <asp:RequiredFieldValidator ID="RequiredFieldValidator_Varenummer" runat="server" ControlToValidate="TextBox_Varenummer" ErrorMessage="Skal udfyldes" Display="Dynamic"></asp:RequiredFieldValidator>
    <asp:CompareValidator ID="CompareValidator_Varenummer" runat="server" Operator="DataTypeCheck" Type="Integer"
        ControlToValidate="TextBox_Varenummer" Display="Dynamic" ErrorMessage="Skal være hele tal" />
    <label>Navn</label>
    <asp:TextBox ID="TextBox_Navn" runat="server"></asp:TextBox>
    <asp:RequiredFieldValidator ID="RequiredFieldValidator_Navn" runat="server" ControlToValidate="TextBox_Navn" ErrorMessage="Skal udfyldes" Display="Dynamic"></asp:RequiredFieldValidator>

    <label>Pris</label>
    <asp:TextBox ID="TextBox_Pris" runat="server"></asp:TextBox>
    <asp:RequiredFieldValidator ID="RequiredFieldValidator_Pris" runat="server" ControlToValidate="TextBox_Pris" ErrorMessage="Skal udfyldes" Display="Dynamic"></asp:RequiredFieldValidator>
    <asp:RegularExpressionValidator ID="RegularExpressionValidator_Pris" ControlToValidate="TextBox_Pris" Display="Dynamic" ValidationExpression="\d+(?:,\d{1,2})?" runat="server" ErrorMessage="Skal være i formateret som penge"></asp:RegularExpressionValidator>
    <asp:RangeValidator ID="RangeValidator_Pris" runat="server" ControlToValidate="TextBox_Pris" Display="Dynamic" ErrorMessage="For højt tal" MinimumValue="0" MaximumValue="99999"></asp:RangeValidator>

    <label>Beskrivelse</label>
    <asp:TextBox ID="TextBox_Beskrivelse" TextMode="MultiLine" runat="server"></asp:TextBox>
    <asp:RequiredFieldValidator ID="RequiredFieldValidator_Beskrivelse" runat="server" ControlToValidate="TextBox_Beskrivelse" ErrorMessage="Skal udfyldes" Display="Dynamic"></asp:RequiredFieldValidator>

    <label>Nuværende lager</label>
    <asp:TextBox ID="TextBox_Lager_Stand" runat="server"></asp:TextBox>
    <asp:RequiredFieldValidator ID="RequiredFieldValidator_Lager_Stand" runat="server" ControlToValidate="TextBox_Lager_Stand" ErrorMessage="Skal udfyldes" Display="Dynamic"></asp:RequiredFieldValidator>
    <asp:CompareValidator ID="CompareValidator_Lager_Stand" runat="server" Operator="DataTypeCheck" Type="Integer"
        ControlToValidate="TextBox_Lager_Stand" Display="Dynamic" ErrorMessage="Skal være hele tal" />

    <label>Maximum lager</label>
    <asp:TextBox ID="TextBox_Lager_Max" runat="server"></asp:TextBox>
    <asp:RequiredFieldValidator ID="RequiredFieldValidator_Lager_Max" runat="server" ControlToValidate="TextBox_Lager_Max" ErrorMessage="Skal udfyldes" Display="Dynamic"></asp:RequiredFieldValidator>
    <asp:CompareValidator ID="CompareValidator_Lager_Max" runat="server" Operator="DataTypeCheck" Type="Integer"
        ControlToValidate="TextBox_Lager_Max" Display="Dynamic" ErrorMessage="Skal være hele tal" />

    <label>Minimum lager</label>
    <asp:TextBox ID="TextBox_Lager_Min" runat="server"></asp:TextBox>
    <asp:RequiredFieldValidator ID="RequiredFieldValidator_Lager_Min" runat="server" ControlToValidate="TextBox_Lager_Min" ErrorMessage="Skal udfyldes" Display="Dynamic"></asp:RequiredFieldValidator>
    <asp:CompareValidator ID="CompareValidator_Lager_Min" runat="server" Operator="DataTypeCheck" Type="Integer"
        ControlToValidate="TextBox_Lager_Min" Display="Dynamic" ErrorMessage="Skal være hele tal" />

    <label>Kategori</label>
    <asp:DropDownList ID="DropDownList_Kategorier" DataTextField="kategori_navn" DataValueField="kategori_id" runat="server"></asp:DropDownList>
    <label>Dyrkningstid</label>
    <asp:DropDownList ID="DropDownList_Dyrkningstider" DataTextField="dyrkningstid_navn" DataValueField="dyrkningstid_id" runat="server"></asp:DropDownList>
    <label>Jordtype</label>
    <asp:DropDownList ID="DropDownList_Jordtyper" DataTextField="jordtype_navn" DataValueField="jordtype_id" runat="server"></asp:DropDownList>
    <label>Sæt produkt til aktivt</label>
    <asp:CheckBox ID="CheckBox_Er_Aktiv" runat="server" Checked="true" />
    <asp:Button ID="Button_Gem" runat="server" Text="Gem" OnClick="Button_Gem_Click" />
</asp:Content>

