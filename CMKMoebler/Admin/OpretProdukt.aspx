<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/MasterPage.master" AutoEventWireup="true" CodeFile="OpretProdukt.aspx.cs" Inherits="Admin_OpretProdukt" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:Panel ID="Panel_Besked" CssClass="Panel_Besked" runat="server">
        <asp:Label ID="Label_Besked" runat="server"></asp:Label>
    </asp:Panel>

    <label>Varenummer</label>
    <asp:TextBox ID="TextBox_Varenummer" runat="server"></asp:TextBox>
    <asp:RequiredFieldValidator ID="RequiredFieldValidator_Varenummer" runat="server" ControlToValidate="TextBox_Varenummer" ErrorMessage="Skal udfyldes" Display="Dynamic"></asp:RequiredFieldValidator>
    <asp:CompareValidator ID="CompareValidator_Varenummer" runat="server" Operator="DataTypeCheck" Type="Integer"
        ControlToValidate="TextBox_Varenummer" Display="Dynamic" ErrorMessage="Kun tal" />

    <label>Navn</label>
    <asp:TextBox ID="TextBox_Navn" runat="server"></asp:TextBox>
    <asp:RequiredFieldValidator ID="RequiredFieldValidator_Navn" runat="server" ControlToValidate="TextBox_Navn" ErrorMessage="Skal udfyldes" Display="Dynamic"></asp:RequiredFieldValidator>

    <label>Pris</label>
    <asp:TextBox ID="TextBox_Pris" runat="server"></asp:TextBox>
    <asp:RequiredFieldValidator ID="RequiredFieldValidator_Pris" runat="server" ControlToValidate="TextBox_Pris" ErrorMessage="Skal udfyldes" Display="Dynamic"></asp:RequiredFieldValidator>
    <asp:CompareValidator ID="CompareValidator_Pris" runat="server" Operator="DataTypeCheck" Type="Integer"
        ControlToValidate="TextBox_Pris" Display="Dynamic" ErrorMessage="Skal være et tal (uden komma)" />

    <label>År</label>
    <asp:TextBox ID="TextBox_År" runat="server"></asp:TextBox>
    <asp:RequiredFieldValidator ID="RequiredFieldValidator_År" runat="server" ControlToValidate="TextBox_År" ErrorMessage="Skal udfyldes" Display="Dynamic"></asp:RequiredFieldValidator>
    <asp:CompareValidator ID="CompareValidator1" runat="server" Operator="DataTypeCheck" Type="Integer"
        ControlToValidate="TextBox_År" Display="Dynamic" ErrorMessage="Kun tal" />

    <label>Beskrivelse</label>
    <asp:TextBox ID="TextBox_Beskrivelse" CssClass="ckeditor" TextMode="MultiLine" runat="server"></asp:TextBox>
    <script src="ckeditor/ckeditor.js"></script>

    <label>Designer</label>
    <asp:DropDownList ID="DropDownList_Designer" DataTextField="designer_navn" DataValueField="designer_id" runat="server"></asp:DropDownList>
    <asp:RequiredFieldValidator ID="RequiredFieldValidator_Designer" runat="server" ControlToValidate="DropDownList_Designer" ErrorMessage="Skal vælges" Display="Dynamic" InitialValue="Skal vælges"></asp:RequiredFieldValidator>

    <label>Møbelserie</label>
    <asp:DropDownList ID="DropDownList_Serie" DataTextField="serie_navn" DataValueField="serie_id" runat="server"></asp:DropDownList>
    <asp:RequiredFieldValidator ID="RequiredFieldValidator_Serie" runat="server" ControlToValidate="DropDownList_Serie" ErrorMessage="Skal vælges" Display="Dynamic" InitialValue="Skal vælges"></asp:RequiredFieldValidator>

    <asp:Button ID="Button_Gem" runat="server" Text="Gem" OnClick="Button_Gem_Click" />

    <a href="Produkter.aspx">Gå tilbage til produkter</a>
</asp:Content>

