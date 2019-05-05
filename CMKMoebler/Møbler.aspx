<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="Møbler.aspx.cs" Inherits="Møbler" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <asp:Panel ID="Panel_Søgning" runat="server" CssClass="Panel_Søgning">
        <asp:Panel ID="Panel_Fejl" CssClass="søgFejl" runat="server" Visible="false">
            <p>Der er desværre ikke nogen emner der matcher dine kriterier.</p>
            <p>Vi anbefaler du udvider din søgning og prøver igen.</p>
        </asp:Panel>
        <asp:Panel ID="Panel_Varenummer" DefaultButton="Button_Varenummer" runat="server" CssClass="søgVarenummer">
            <table class="søgning">
                <tr class="firstTr">
                    <td>
                        <label>Vare nr:</label></td>
                    <td>
                        <asp:TextBox ID="TextBox_Varenummer" runat="server" ValidationGroup="Panel_Varenummer"></asp:TextBox>
                        <asp:CompareValidator ID="CompareValidator_Varenummer" runat="server" Operator="DataTypeCheck" Type="Integer"
                            ControlToValidate="TextBox_Varenummer" Display="Dynamic" ErrorMessage="Kun tal" ValidationGroup="Panel_Varenummer" />
                    </td>
                    <td>
                        <asp:Button ID="Button_Varenummer" runat="server" Text="Søg" OnClick="Button_Varenummer_Click" ValidationGroup="Panel_Varenummer" /></td>
                </tr>
            </table>
        </asp:Panel>

        <asp:Panel ID="Panel_Udvidet_Søgning" runat="server" DefaultButton="Button_Udvidet_Søgning">
            <table class="søgning">
                <tr>
                    <td colspan="3">&nbsp;</td>
                </tr>
                <tr>
                    <td>Møbelserie
                    </td>
                    <td colspan="2">
                        <asp:CheckBoxList ID="CheckBoxList_Serie" Width="100%" RepeatLayout="Table" DataTextField="serie_navn" DataValueField="serie_id" RepeatColumns="2" ValidationGroup="Panel_Udvidet_Søgning" runat="server"></asp:CheckBoxList>
                    </td>
                </tr>
                <tr>
                    <td>Designer
                    </td>
                    <td colspan="2">
                        <asp:DropDownList ID="DropDownList_Designer" DataTextField="designer_navn" ValidationGroup="Panel_Udvidet_Søgning" DataValueField="designer_id" runat="server"></asp:DropDownList></td>
                </tr>
                <tr>
                    <td>Design år</td>
                    <td>Min:
                    <asp:TextBox ID="TextBox_År_Min" Text="0" ValidationGroup="Panel_Udvidet_Søgning" runat="server"></asp:TextBox></td>
                    <td>Max:
                    <asp:TextBox ID="TextBox_År_Max" ValidationGroup="Panel_Udvidet_Søgning" runat="server"></asp:TextBox></td>
                </tr>
                <tr>
                    <td>Pris</td>
                    <td>Min:
                    <asp:TextBox ID="TextBox_Pris_Min" ValidationGroup="Panel_Udvidet_Søgning" Text="0" runat="server"></asp:TextBox></td>
                    <td>Max:
                    <asp:TextBox ID="TextBox_Pris_Max" ValidationGroup="Panel_Udvidet_Søgning" Text="100000000" runat="server"></asp:TextBox></td>
                </tr>
                <tr class="lastTr">
                    <td></td>
                    <td></td>
                    <td>
                        <asp:Button ID="Button_Udvidet_Søgning" runat="server" Text="Søg" ValidationGroup="Panel_Udvidet_Søgning" OnClick="Button_Udvidet_Søgning_Click" /></td>
                </tr>
            </table>
        </asp:Panel>
    </asp:Panel>

    <asp:Repeater ID="Repeater_Produkter" runat="server" OnItemDataBound="Repeater_Produkter_ItemDataBound" Visible="false">
        <ItemTemplate>
            <section class="søgeResultater">
                <asp:Repeater ID="Repeater_nested" runat="server">
                    <ItemTemplate>
                        <a href="Møbel.aspx?id=<%# Eval("fk_produkt_id") %>">
                            <img src="<%# Eval("billede_sti", "/billeder/produkter/{0}") %>" /></a>
                    </ItemTemplate>
                </asp:Repeater>

                <div>
                    <h3><%# Eval("produkt_navn") %></h3>
                    <a href="Møbel.aspx?id=<%# Eval("produkt_id") %>">

                        <p><%# Helpers.Truncate(Eval("produkt_beskrivelse").ToString()) %></p>
                    </a>

                </div>
            </section>
        </ItemTemplate>
    </asp:Repeater>
</asp:Content>

