<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/MasterPage.master" AutoEventWireup="true" CodeFile="Billeder.aspx.cs" Inherits="Admin_Billeder" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:Panel ID="Panel_Besked" CssClass="Panel_Besked" runat="server">
        <asp:Label ID="Label_Besked" runat="server"></asp:Label>
    </asp:Panel>
    <label>Upload billeder til produktet</label>
    <asp:Image ID="Image_Billede" runat="server" />
    <asp:FileUpload ID="FileUpload_Billede" AllowMultiple="true" runat="server" />
    <asp:HiddenField ID="HiddenField_Billede" runat="server" />
    <asp:Button ID="Button_Gem" runat="server" Text="Gem" OnClick="Button_Gem_Click" />


    <asp:Repeater ID="Repeater_Produkter" runat="server" OnItemDataBound="Repeater_Produkter_ItemDataBound">
        <ItemTemplate>
            <h3><%# Eval("produkt_navn") %></h3>

            <asp:Repeater ID="Repeater_Billeder" runat="server">
                <HeaderTemplate>
                    <div class="galleri">
                </HeaderTemplate>
                <ItemTemplate>
                    <div class="billede">
                        <img src="<%# Eval("billede_sti", "/billeder/thumbs/{0}") %>" />
                        <a href="Billeder.aspx?id=<%# Request.QueryString["id"] %>&action=ret&billede_id=<%# Eval("billede_id") %>"><i title="Ret" class="fa fa-edit"></i></a>
                        <a onclick="return confirm('Er du sikker?')" href="Billeder.aspx?id=<%# Request.QueryString["id"] %>&action=slet&billede_id=<%# Eval("billede_id") %>"><i title="Slet" class="fa fa-trash"></i></a>
                        <%-- <asp:DropDownList AutoPostBack="true" OnSelectedIndexChanged="DropDownList_Order_SelectedIndexChanged" ID="DropDownList_Order" DataTextField="" DataValueField="total" runat="server"></asp:DropDownList>--%>
                        <a href="Billeder.aspx?id=<%# Request.QueryString["id"] %>&action=head&billede_id=<%# Eval("billede_id") %>"><i title="Sæt primær billede" class="fa fa-arrow-circle-o-up <%# (Eval("billede_prioritet")).ToString() == "True" ? "blue" : "red" %>"></i></a>
                    </div>
                </ItemTemplate>
                <FooterTemplate>
                    </div>
                </FooterTemplate>
            </asp:Repeater>
        </ItemTemplate>
    </asp:Repeater>
    <a href="Produkter.aspx">Tilbage til produkter</a>
</asp:Content>

