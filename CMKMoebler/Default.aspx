<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="_Default" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <title>CMK Møbler</title>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:Repeater ID="Repeater_Forside" runat="server">
        <ItemTemplate>
            <article>
                <h3><a href="Nyhedsarkiv.aspx#nyhed<%# Eval("nyhed_id") %>"><%# Eval("nyhed_overskrift") %></a></h3>
                <span>af
                    <h4><%# (String.IsNullOrEmpty(Eval("bruger_navn").ToString()) ? "Slettet" : Eval("bruger_navn")) %></h4>
                </span>
                <span class="dato"><i class="fa fa-clock-o"></i>&nbsp;<%# Convert.ToDateTime(Eval("nyhed_dato")).ToString("d. MMM yyyy") %></span>
                <%--                <div class="brødtekst">
                    <%# Eval("nyhed_tekst").ToString().Length <= 180 ? Eval("nyhed_tekst") : Eval("nyhed_tekst").ToString().Substring(0,180) + " <a class='læsMere' href='Nyhedsarkiv.aspx#nyhed" + Eval("nyhed_id") + "'>Læs mere <i class='fa fa-angle-right'></i></a>" %>
                </div>--%>
                <div class="brødtekst">
                    <p>
                        <%# Helpers.Truncate(Eval("nyhed_tekst").ToString()).Length <= 180 ? Eval("nyhed_tekst") : Helpers.Truncate(Eval("nyhed_tekst").ToString()) + " <a class='læsMere' href='Nyhedsarkiv.aspx#nyhed" + Eval("nyhed_id") + "'>Læs mere <i class='fa fa-angle-right'></i></a>" %>
                    </p>
                </div>
            </article>
        </ItemTemplate>
    </asp:Repeater>
</asp:Content>

