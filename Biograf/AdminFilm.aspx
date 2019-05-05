<%@ Page Title="" Language="C#" MasterPageFile="~/AdminMasterPage.master" AutoEventWireup="true" CodeFile="AdminFilm.aspx.cs" Inherits="AdminFilm" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <h1>Administrer film</h1>
    <h3>Titel</h3>
    <asp:TextBox ID="TextBox_Navn" runat="server"></asp:TextBox>
    <asp:RequiredFieldValidator ID="RequiredFieldValidator_Navn" runat="server" ErrorMessage="Skal udfyldes" ControlToValidate="TextBox_Navn"></asp:RequiredFieldValidator>
    <h3>Beskrivelse</h3>
    <asp:TextBox ID="TextBox_Beskrivelse" runat="server" TextMode="MultiLine"></asp:TextBox>
    <asp:RequiredFieldValidator ID="RequiredFieldValidator_Beskrivelse" runat="server" ErrorMessage="Skal udfyldes" ControlToValidate="TextBox_Beskrivelse"></asp:RequiredFieldValidator>
    <h3>Vælg genrer</h3>
    <asp:CheckBoxList ID="CheckBoxList_Genre" CssClass="CheckBoxList" RepeatDirection="Horizontal" runat="server" DataTextField="genre_navn" DataValueField="genre_id">
    </asp:CheckBoxList>
    <asp:Panel ID="Panel_Billede" runat="server">
        <h3>Upload et billede</h3>
        <asp:FileUpload ID="FileUpload_Billede" runat="server" />
        <br />
        <h4>Vælg en prioritet for billedet</h4>
        <asp:TextBox CssClass="Prioritet" ID="TextBox_Prioritet" runat="server"></asp:TextBox><br />
        <asp:RequiredFieldValidator ID="RequiredFieldValidator_Prioritet" runat="server" ErrorMessage="Skal vælge prioritet" ControlToValidate="TextBox_Prioritet" Display="Dynamic" Enabled="false"></asp:RequiredFieldValidator>
        <asp:RegularExpressionValidator ID="RegularExpressionValidator_Prioritet" runat="server" ErrorMessage="Indtast et tal" ControlToValidate="TextBox_Prioritet" Enabled="false" Display="Dynamic" ValidationExpression="^\d+$"></asp:RegularExpressionValidator>
    </asp:Panel>
    <asp:Button ID="Button_Gem" runat="server" Text="Gem" OnClick="Button_Gem_Click" />
    <a class="Form_a2" href="AdminFilm.aspx">Annullér</a>

    <asp:Repeater ID="Repeater_Film" OnItemDataBound="Repeater_Film_ItemDataBound" runat="server">
        <ItemTemplate>
            <div class="Filmliste">
                <asp:Repeater ID="Repeater_billede" runat="server">
                    <ItemTemplate>
                        <img src="<%# Eval("billede_sti", "/billeder/filmliste/{0}") %>" />
                    </ItemTemplate>
                </asp:Repeater>
                <div class="Beskrivelse">
                    <h2><%# Eval("film_navn") %></h2>
                    <p><%# Eval("film_beskrivelse") %></p>
                </div>
                <asp:Repeater ID="Repeater_nested" runat="server">
                    <HeaderTemplate>
                        <p>
                    </HeaderTemplate>
                    <ItemTemplate>
                        <%# Eval("genre_navn") %>
                    </ItemTemplate>
                    <FooterTemplate></p></FooterTemplate>
                </asp:Repeater>
                <a class="Form_a2" href="AdminFilm.aspx?handling=ret&id=<%# Eval("film_id") %>">Ret</a> <a class="Form_a2" href="AdminFilm.aspx?handling=slet&id=<%# Eval("film_id") %>" onclick="return confirm('Dette vil slette denne film')">Slet</a> <a class="Form_a2" href="AdminBilleder.aspx?id=<%# Eval("film_id") %>">Administrer billeder</a>
            </div>
        </ItemTemplate>
    </asp:Repeater>
</asp:Content>

