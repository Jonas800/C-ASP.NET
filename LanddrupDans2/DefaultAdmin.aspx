<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPageAdmin.master" AutoEventWireup="true" CodeFile="DefaultAdmin.aspx.cs" Inherits="DefaultAdmin" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div id="div_forside_admin">
        <h2>Forside</h2>
        <div>
            <asp:Image ID="Image_Forside_Current_Image" runat="server" CssClass="image_forside_current_image" />
            <asp:HiddenField ID="HiddenField_Forside_Current_Image" runat="server" />
            <asp:FileUpload ID="FileUpload_Forside_Image" runat="server" CssClass="fileupload_forside_image" />
        </div>
        <div>
            <asp:TextBox ID="TextBox_Forside_Text" runat="server" TextMode="MultiLine" CssClass="textbox_forside_text"></asp:TextBox>
            <asp:Button ID="Button_Forside" runat="server" Text="Gem" OnClick="Button_Forside_Click" CssClass="button_forside" />
        </div>
    </div>
</asp:Content>

