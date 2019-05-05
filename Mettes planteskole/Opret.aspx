<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="Opret.aspx.cs" Inherits="Opret" %>

<%@ Register Src="~/UserControl/UserControl_Opret.ascx" TagPrefix="uc1" TagName="UserControl_Opret" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <uc1:UserControl_Opret runat="server" ID="UserControl_Opret" />
</asp:Content>

