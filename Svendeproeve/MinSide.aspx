<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="MinSide.aspx.cs" Inherits="MinSide" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <p class="kontaktOplysninger_tekst">
        Herunder kan du tilmelde og framelde dig vores træningsaktiviteter.<br />
        Øverst i højre side kan du se, hvor mange træningspoint, du har optjent.
    </p>
    <div class="hold_tid">
        <h3 style="margin-bottom:20px;">Tilmeld dig aktiviteter</h3>
        <asp:Repeater ID="Repeater_Måneder" runat="server">

            <ItemTemplate>
                <h4><a href="?year=<%# Eval("aar") %>&month=<%# Eval("maaned") %>"><i class="fa fa-caret-right"></i><%# Eval("aar") %> - <%# UppercaseFirst( System.Globalization.CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(Convert.ToInt32(Eval("maaned"))))
                %></a></h4>
            </ItemTemplate>
        </asp:Repeater>
    </div>
    <div class="hold">
        <asp:Repeater ID="Repeater_Hold" OnItemCommand="Repeater_Hold_ItemCommand" OnItemDataBound="Repeater_Hold_ItemDataBound" runat="server">
            <HeaderTemplate>
                <table>
                    <tr>
                        <th>Aktivitet</th>
                        <th>Instruktør</th>
                        <th>Dato</th>
                        <th>Tid</th>
                        <th>Point</th>
                        <th>Tilmeldte</th>
                        <th>Status</th>
                    </tr>
            </HeaderTemplate>
            <ItemTemplate>
                <tr>
                    <td><%# Eval("aktivitet_navn") %></td>
                    <td><%# Eval("bruger_navn") %></td>
                    <td><%# Convert.ToDateTime(Eval("hold_tidspunkt")).ToString("dd-MMMM-yyyy") %></td>
                    <td><%# Convert.ToDateTime(Eval("hold_tidspunkt")).ToString("HH:mm") %></td>
                    <td><%# Eval("hold_point") %></td>
                    <td><%# Eval("tilmeldte") %>/<%# Eval("hold_max_antal") %></td>
                    <td>
                        <asp:HiddenField ID="HiddenField_ID" Value='<%# Eval("hold_id") %>' runat="server" />
                        <asp:Button ID="Button_Status" runat="server" Text='<%# LavKnap(Convert.ToInt32(Eval("hold_id")))  %>' CommandName='<%# LavKnap(Convert.ToInt32(Eval("hold_id")))  %>' CssClass='<%# LavKnap(Convert.ToInt32(Eval("hold_id")))  %>' />
                </tr>
            </ItemTemplate>
            <FooterTemplate>
                </table>
            </FooterTemplate>
        </asp:Repeater>
    </div>
</asp:Content>

