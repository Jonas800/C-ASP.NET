<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="Nyhedsarkiv.aspx.cs" Inherits="Nyhedsarkiv" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:Repeater ID="Repeater_Nyheder" runat="server">
        <ItemTemplate>
            <article id="nyhed<%# Eval("nyhed_id") %>">
                <h3><a href="Nyhedsarkiv.aspx#nyhed<%# Eval("nyhed_id") %>"><%# Eval("nyhed_overskrift") %></a></h3>
                <span>af
                    <h4><%# (String.IsNullOrEmpty(Eval("bruger_navn").ToString()) ? "Slettet" : Eval("bruger_navn")) %></h4>
                </span>
                <span class="dato"><i class="fa fa-clock-o"></i>&nbsp;<%# Convert.ToDateTime(Eval("nyhed_dato")).ToString("d. MMM yyyy") %></span>
                <div class="brødtekst">
                    <%# Eval("nyhed_tekst") %>
                </div>
            </article>
        </ItemTemplate>
    </asp:Repeater>

</asp:Content>

