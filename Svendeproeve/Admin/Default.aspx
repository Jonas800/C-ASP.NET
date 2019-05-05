<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/MasterPage.master" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="Admin_Default" %>

<%@ Register Src="~/UserControls/UserControl_Login.ascx" TagPrefix="uc1" TagName="UserControl_Login" %>


<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <uc1:UserControl_Login runat="server" ID="UserControl_Login" />

    <asp:Panel ID="Panel_Velkommen" runat="server" CssClass="Panel_Velkommen" Visible="false">
        <ul>
            <li>
                <h3>Velkommen til Motionscentrets adminpanel. Vælg her hvad du vil administrere.</h3>
            </li>
            <li><a href="Aktiviteter.aspx">Aktiviteter</a></li>
            <li><a href="Hold.aspx">Hold</a>
            <li><a href="Priser.aspx">Priser</a></li>
            <li><a href="Oplysninger.aspx">Oplysninger</a></li>
            <li><a href="Brugere.aspx">Brugere</a></li>
            <li><a href="Medlemmer.aspx">Medlemmer</a></li>
            <li><a href="../Default.aspx">Se hjemmeside</a></li>
        </ul>
    </asp:Panel>
</asp:Content>

