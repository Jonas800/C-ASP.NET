<%@ Control Language="C#" AutoEventWireup="true" CodeFile="UserControl_Nyhedsbrev.ascx.cs" Inherits="UserControls_UserControl_Nyhedsbrev" %>

<h3>Nyhedsbrev</h3>
<asp:Panel ID="Panel_Nyhedsbrev" runat="server" CssClass="nyhedsbrev">
    <form>
        <asp:TextBox ID="TextBox_Email" runat="server" ValidationGroup="Panel_Nyhedsbrev"></asp:TextBox>
        <asp:RequiredFieldValidator ValidationGroup="Panel_Nyhedsbrev" ID="RequiredFieldValidator_Email" runat="server" ControlToValidate="TextBox_Email" ErrorMessage="Skal udfyldes" Display="Dynamic"></asp:RequiredFieldValidator>
        <asp:RegularExpressionValidator ValidationGroup="Panel_Nyhedsbrev" ID="RegularExpressionValidator_Email" runat="server" ErrorMessage="Ikke en gyldig email" ControlToValidate="TextBox_Email" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*" Display="Dynamic"></asp:RegularExpressionValidator>
        <button validationgroup="Panel_Nyhedsbrev" runat="server" id="Button_Tilmeld" onserverclick="Button_Tilmeld_ServerClick" type="button">
            Tilmeld <i class="fa fa-check"></i>
        </button>
        <button validationgroup="Panel_Nyhedsbrev" runat="server" id="Button_Frameld" onserverclick="Button_Frameld_ServerClick" type="button">
            Frameld <i class="fa fa-times"></i>
        </button>
    </form>
</asp:Panel>
<asp:Label ID="Label_Nyhedsbrev_Besked" runat="server"></asp:Label>