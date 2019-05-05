<%@ Page Title="" Language="C#" MasterPageFile="~/AdminMasterPage.master" AutoEventWireup="true" CodeFile="AdminBrugere.aspx.cs" Inherits="AdminBrugere" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <h1>Administrer brugere
    </h1>
    <h3>Navn</h3>
    <asp:TextBox ID="TextBox_Navn" runat="server"></asp:TextBox>
    <asp:RequiredFieldValidator ID="RequiredFieldValidator_Navn" runat="server" ControlToValidate="TextBox_Navn" ErrorMessage="Skal udfyldes" Display="Dynamic"></asp:RequiredFieldValidator>

    <h3>Email</h3>
    <asp:TextBox ID="TextBox_Email" runat="server"></asp:TextBox>
    <asp:RequiredFieldValidator ID="RequiredFieldValidator_Email" runat="server" ControlToValidate="TextBox_Email" ErrorMessage="Skal udfyldes" Display="Dynamic"></asp:RequiredFieldValidator>
    <asp:RegularExpressionValidator ID="RegularExpressionValidator_Email" runat="server" ErrorMessage="Ikke en gyldig email" ControlToValidate="TextBox_Email" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*" Display="Dynamic"></asp:RegularExpressionValidator>

    <h3>Kodeord</h3>
    <asp:TextBox ID="TextBox_Kodeord" TextMode="Password" runat="server"></asp:TextBox>
    <asp:RequiredFieldValidator ID="RequiredFieldValidator_Kodeord" runat="server" ErrorMessage="Skal udfyldes" ControlToValidate="TextBox_Kodeord" Display="Dynamic"></asp:RequiredFieldValidator>

    <h3>Gentag kodeord</h3>
    <asp:TextBox ID="TextBox_Kodeord_Gentag" TextMode="Password" runat="server"></asp:TextBox>
    <asp:CompareValidator ID="CompareValidator_Kodeord" runat="server" ErrorMessage="Kodeord er ikke ens" ControlToValidate="TextBox_Kodeord_Gentag" ControlToCompare="TextBox_Kodeord" Display="Dynamic"></asp:CompareValidator>

    <h3>Vælg rolle</h3>
    <asp:RadioButtonList ID="RadioButtonList_Roller" runat="server"></asp:RadioButtonList>
    <asp:RequiredFieldValidator ID="RequiredFieldValidator_Roller" runat="server" ErrorMessage="Skal vælge en" ControlToValidate="RadioButtonList_Roller" Display="Dynamic"></asp:RequiredFieldValidator>

    <asp:Button ID="Button_Gem" runat="server" Text="Gem" OnClick="Button_Gem_Click" />
    <a class="Form_a2" href="AdminBrugere.aspx">Annullér</a>

    <h3>Eksisterende brugere</h3>
    <asp:Repeater ID="Repeater_Roller" OnItemDataBound="Repeater_Roller_ItemDataBound" runat="server">
        <ItemTemplate>
            <h4 class="Roller"><%# Eval("rolle_navn") %></h4>
            <asp:Repeater ID="Repeater_Brugere" runat="server">
                <HeaderTemplate>
                    <table>
                        <tr>
                            <th>Navn
                            </th>
                            <th>Email
                            </th>
                            <th>Point
                            </th>
                            <th>Ret
                            </th>
                            <th>Slet
                            </th>
                        </tr>
                </HeaderTemplate>
                <ItemTemplate>
                    <tr>
                        <td><%# Eval("bruger_navn") %></td>
                        <td><%# Eval("bruger_email") %></td>
                        <td><%# Eval("bruger_point") %></td>
                        <td><a href="AdminBrugere.aspx?handling=ret&id=<%# Eval("bruger_id") %>"><i class="fa fa-pencil" aria-hidden="true"></i></a></td>
                        <td><a href="AdminBrugere.aspx?handling=slet&id=<%# Eval("bruger_id") %>" onclick="return confirm('Dette vil slette brugeren')"><i class="fa fa-trash" aria-hidden="true"></i></a></td>
                    </tr>
                </ItemTemplate>
                <FooterTemplate>
                    </table>
                </FooterTemplate>
            </asp:Repeater>
        </ItemTemplate>
    </asp:Repeater>
</asp:Content>

