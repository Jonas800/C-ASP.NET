<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/MasterPage.master" AutoEventWireup="true" CodeFile="Hold.aspx.cs" Inherits="Admin_Hold" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <label>Aktivitet</label>
    <asp:DropDownList ID="DropDownList_Aktivitet" DataTextField="aktivitet_navn" DataValueField="aktivitet_id" runat="server"></asp:DropDownList>
    <asp:RequiredFieldValidator ID="RequiredFieldValidator_Aktivitet" runat="server" ControlToValidate="DropDownList_Aktivitet" InitialValue="Vælg aktivitet" ErrorMessage="Skal vælges" ForeColor="DarkRed" Display="Dynamic"></asp:RequiredFieldValidator>

    <label>Instruktør</label>
    <asp:DropDownList ID="DropDownList_Instruktør" DataTextField="bruger_navn" DataValueField="bruger_id" runat="server"></asp:DropDownList>
    <asp:RequiredFieldValidator ID="RequiredFieldValidator_Instruktør" runat="server" ControlToValidate="DropDownList_Instruktør" InitialValue="Vælg instruktør" ErrorMessage="Skal vælges" ForeColor="DarkRed" Display="Dynamic"></asp:RequiredFieldValidator>

    <label>Dato</label>
    <asp:Calendar ID="Calendar_Dato" runat="server"></asp:Calendar>
    <asp:Label ID="Label_Calendar_Error" ForeColor="DarkRed" runat="server"></asp:Label>


    <label>Klokkeslæt</label>
    <asp:TextBox ID="TextBox_Tidspunkt" runat="server"></asp:TextBox>
    <asp:RequiredFieldValidator ID="RequiredFieldValidator_Tidspunkt" runat="server" ErrorMessage="Skal udfyldes" ControlToValidate="TextBox_Tidspunkt" ForeColor="DarkRed"></asp:RequiredFieldValidator>
    <asp:RegularExpressionValidator ID="RegularExpressionValidator_Tidspunkt" ControlToValidate="TextBox_Tidspunkt" ValidationExpression="^(([01][0-9]|[012][0-3]):([0-5][0-9]))*$" runat="server" Display="Dynamic" ErrorMessage="Skal være i formatet 01:59" ForeColor="DarkRed"></asp:RegularExpressionValidator>

    <label>Max antal medlemmer på holdet</label>
    <asp:TextBox ID="TextBox_Antal" runat="server" TextMode="Number"></asp:TextBox>
    <asp:RequiredFieldValidator ID="RequiredFieldValidator_Antal" runat="server" ErrorMessage="Skal udfyldes" Display="Dynamic" ControlToValidate="TextBox_Antal" ForeColor="DarkRed"></asp:RequiredFieldValidator>
    <asp:CompareValidator ID="CompareValidator_Antal" ControlToValidate="TextBox_Antal" Type="Integer" Operator="DataTypeCheck" runat="server" ErrorMessage="Skal være et heltal" Display="Dynamic" ForeColor="DarkRed"></asp:CompareValidator>

    <label>Anfør hvor mange point holdet skal give</label>
    <asp:TextBox ID="TextBox_Point" runat="server" TextMode="Number"></asp:TextBox>
    <asp:RequiredFieldValidator ID="RequiredFieldValidator_Point" runat="server" ErrorMessage="Skal udfyldes" Display="Dynamic" ControlToValidate="TextBox_Point" ForeColor="DarkRed"></asp:RequiredFieldValidator>
    <asp:CompareValidator ID="CompareValidator_Point" ControlToValidate="TextBox_Point" Type="Integer" Operator="DataTypeCheck" runat="server" ErrorMessage="Skal være et heltal" Display="Dynamic" ForeColor="DarkRed"></asp:CompareValidator>


    <asp:Button ID="Button_Gem" runat="server" Text="Gem" OnClick="Button_Gem_Click" />

    <h3>Holdoversigt</h3>
    <div class="responsivTable">
        <asp:Repeater ID="Repeater_Hold" runat="server">
            <HeaderTemplate>
                <table>
                    <tr>
                        <th>Aktivitet</th>
                        <th>Tidspunkt</th>
                        <th>Instruktør</th>
                        <th>Point</th>
                        <th>Tilmeldte</th>
                        <th>Ret</th>
                        <th>Slet</th>
                    </tr>
            </HeaderTemplate>
            <ItemTemplate>
                <tr>
                    <td><%# Eval("aktivitet_navn") %></td>
                    <td><%# Convert.ToDateTime(Eval("hold_tidspunkt")).ToString("dd/MM/yyyy - HH:mm") %></td>
                    <td><%# Eval("bruger_navn") %></td>
                    <td><%# Eval("hold_point") %></td>
                    <td><a href="Tilmeldte.aspx?hold_id=<%# Eval("hold_id") %>">(<%# Eval("total") %>) tilmeldte</a></td>
                    <td><a href="Hold.aspx?id=<%# Eval("hold_id") %>&action=ret"><i class="fa fa-edit"></i></a></td>
                    <td><a class="sletTd" href="Hold.aspx?id=<%# Eval("hold_id") %>&action=slet" onclick="return confirm('Er du sikker?')"><i class="fa fa-trash"></i></a></td>
                </tr>
            </ItemTemplate>
            <FooterTemplate>
                </table>
            </FooterTemplate>
        </asp:Repeater>
    </div>
</asp:Content>

