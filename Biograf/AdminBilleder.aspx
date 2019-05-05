<%@ Page Title="" Language="C#" MasterPageFile="~/AdminMasterPage.master" AutoEventWireup="true" CodeFile="AdminBilleder.aspx.cs" Inherits="AdminBilleder" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <asp:Image ID="Image_Billede" runat="server" />
    <h3>Upload et billede</h3>
    <asp:FileUpload ID="FileUpload_Billede" runat="server" /><br />
    <h4>Vælg en prioritet for billedet</h4>
    <asp:TextBox CssClass="Prioritet" ID="TextBox_Prioritet" runat="server"></asp:TextBox>
    <asp:RequiredFieldValidator ID="RequiredFieldValidator_Prioritet" runat="server" ErrorMessage="Skal vælge prioritet" ControlToValidate="TextBox_Prioritet" Display="Dynamic"></asp:RequiredFieldValidator>
    <asp:RegularExpressionValidator ID="RegularExpressionValidator_Prioritet" runat="server" ErrorMessage="Indtast et tal" ControlToValidate="TextBox_Prioritet" Display="Dynamic" ValidationExpression="^\d+$"></asp:RegularExpressionValidator>
    <asp:Button ID="Button_Gem" runat="server" Text="Gem" OnClick="Button_Gem_Click" />
    <a class="Form_a2" href="AdminBilleder.aspx">Annullér</a>

    <asp:Repeater ID="Repeater_Billede" runat="server" OnItemDataBound="Repeater_Billede_ItemDataBound">
        <ItemTemplate>
            <h2 class="Billedeliste_h2"><%# Eval("film_navn") %></h2>
            <asp:Repeater ID="Repeater_nested" runat="server">
                <ItemTemplate>
                    <div class="Billedeliste">
                        <p>
                            Prioritet: <%# Eval("billede_prioritet") %>
                        </p>
                        <img src="<%# Eval("billede_sti", "/billeder/filmbeskrivelse/{0}") %>" />
                        <a class="Form_a2" href="AdminBilleder.aspx?handling=ret&id=<%# Eval("fk_film_id") %>&billede_id=<%# Eval("billede_id") %>">Ret</a>
                        <a class="Form_a2" href="AdminBilleder.aspx?handling=slet&id=<%# Eval("fk_film_id") %>&billede_id=<%# Eval("billede_id") %>" onclick="return confirm('Er du sikker?')">Slet</a>
                    </div>
                </ItemTemplate>
            </asp:Repeater>
        </ItemTemplate>
    </asp:Repeater>
</asp:Content>

