<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/MasterPage.master" AutoEventWireup="true" CodeFile="Billeder.aspx.cs" Inherits="Admin_Billeder" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <h2>Billeder</h2>

    <label>Upload billeder til produktet</label>
    <asp:Image ID="Image_Billede" runat="server" />
    <asp:FileUpload ID="FileUpload_Billede" AllowMultiple="true" runat="server" />
    <asp:HiddenField ID="HiddenField_Billede" runat="server" />
    <asp:Button ID="Button_Gem" runat="server" Text="Gem" OnClick="Button_Gem_Click" />
    <asp:Label ID="Label_Error" runat="server"></asp:Label>
    <asp:Repeater ID="Repeater_Produkter" runat="server" OnItemDataBound="Repeater_Produkter_ItemDataBound">
        <ItemTemplate>
            <h3><%# Eval("produkt_navn") %></h3>

            <asp:Repeater ID="Repeater_Billeder" runat="server">
                <HeaderTemplate>
                    <div class="galleri">
                </HeaderTemplate>
                <ItemTemplate>
                    <div class="billede">
                        <img src="<%# Eval("billede_sti", "/billeder/produkter/{0}") %>" />
                        <a href="<%# Request.RawUrl %>&action=ret&billede_id=<%# Eval("billede_id") %>">Ret</a>
                        <a onclick="return confirm('Er du sikker?')" href="<%# Request.RawUrl %>&action=slet&billede_id=<%# Eval("billede_id") %>">Slet</a>
                    </div>
                </ItemTemplate>
                <FooterTemplate>
                    </div>
                </FooterTemplate>
            </asp:Repeater>
        </ItemTemplate>
    </asp:Repeater>
</asp:Content>

