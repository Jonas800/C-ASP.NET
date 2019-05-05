<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/MasterPage.master" AutoEventWireup="true" CodeFile="RetOrdrer.aspx.cs" Inherits="Admin_RetOrdrer" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <h2>Ret en ordre</h2>
    <asp:DropDownList ID="DropDownList_Status" runat="server" DataTextField="status_navn" DataValueField="status_id"></asp:DropDownList>


    <asp:Button ID="Button_Gem" runat="server" OnClick="Button_Gem_Click" Text="Gem" />
</asp:Content>

