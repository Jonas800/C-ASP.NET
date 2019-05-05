<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="_Default" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
</asp:Content>
<asp:Content ID="Main" ContentPlaceHolderID="Main" runat="server">
    <div class="wrapper">
        <%--FORSIDE--%>
        <asp:Panel ID="Panel_Home" runat="server">
            <h2>Seneste slips</h2>
            <asp:Repeater ID="Repeater_Home" runat="server">
                <ItemTemplate>
                    <section>
                        <a href="Details.aspx?id=<%# Eval("product_id") %>" class="forside_a">
                            <img src="<%# Eval("product_image", "/billeder/thumbs/{0}") %>" />
                        </a>
                        <div>
                            <h3><%# Eval("product_name") %></h3>
                            <h3><%# Eval("product_price") %> kr,-</h3>
                            <p><%# Eval("product_description").ToString().Replace(Environment.NewLine,"<br />") %></p>
                        </div>

                    </section>
                </ItemTemplate>
            </asp:Repeater>
            <article>
                <h2>Om slips</h2>
                <p>
                    Sed ut perspiciatis unde omnis iste natus error sit voluptatem accusantium doloremque laudantium, totam rem aperiam, eaque ipsa quae ab illo inventore veritatis et quasi architecto beatae vitae dicta sunt explicabo. Nemo enim ipsam voluptatem quia voluptas sit aspernatur aut odit aut fugit, sed quia consequuntur magni dolores eos qui ratione voluptatem sequi nesciunt. Neque porro quisquam est, qui dolorem ipsum quia dolor sit amet, consectetur, adipisci velit, sed quia non numquam
                </p>
            </article>
        </asp:Panel>
    </div>
</asp:Content>

