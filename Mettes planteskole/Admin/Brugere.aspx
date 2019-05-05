<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/MasterPage.master" AutoEventWireup="true" CodeFile="Brugere.aspx.cs" Inherits="Admin_Brugere" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <h2>Kunder</h2>

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
    <label>Giv bruger administrative egenskaber</label>
    <asp:CheckBox ID="CheckBox_Rolle" runat="server" />
    <label>Sæt bruger til aktiv</label>
    <asp:CheckBox ID="CheckBox_Deaktiver" runat="server" Checked="true" />
    <asp:Button ID="Button_Gem" runat="server" Text="Gem" OnClick="Button_Gem_Click" />

    <h3>Kundeoversigt</h3>
    <asp:Repeater ID="Repeater_Kunder" runat="server">
        <HeaderTemplate>
            <table>
                <tr>
                    <th>Email</th>
                    <th>Navn</th>
                    <th>Adresse</th>
                    <th>Postnummer</th>
                    <th>By</th>
                    <th>Telefon</th>
                    <th>Er admin</th>
                    <th>Er aktiv</th>
                    <th>Ret</th>
                </tr>
        </HeaderTemplate>
        <ItemTemplate>
            <tr>
                <td><%# Eval("bruger_email") %></td>
                <td><%# Eval("kunde_navn") %></td>
                <td><%# Eval("kunde_adresse") %></td>
                <td><%# Eval("kunde_postnummer") %></td>
                <td><%# Eval("kunde_by") %></td>
                <td><%# Eval("kunde_telefon") %></td>
                <td><%# (Boolean.Parse(Eval("bruger_er_admin").ToString())) ? "Ja" : "Nej" %></td>
                <td><%# (Boolean.Parse(Eval("bruger_er_aktiv").ToString())) ? "Ja" : "Nej" %></td>
                <td><a href="Brugere.aspx?action=ret&id=<%# Eval("bruger_id") %>">Ret</a></td>
            </tr>
        </ItemTemplate>
        <FooterTemplate>
            </table>
        </FooterTemplate>
    </asp:Repeater>
</asp:Content>

