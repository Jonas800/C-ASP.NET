<%@ Page Title="" Language="C#" MasterPageFile="~/WebApp/MasterPage.master" AutoEventWireup="true" CodeFile="DynamicPage.aspx.cs" Inherits="WebApp_DynamicPage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <%--    <link href="../StyleSheet.css" rel="stylesheet" />--%>
        <script src="JavaScript.js"></script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    

    <asp:Repeater ID="Repeater_Feeds"
        OnItemDataBound="Repeater_Feeds_ItemDataBound"
        runat="server">
        <ItemTemplate>
            <div data-role="page" id="<%# Eval("feed_titel") %>" class="demo-page outer_box" data-dom-cache="true" data-prev="<%# Eval("PreviousValue") %>" data-next="<%# Eval("NextValue") %>" data-url="<%# Eval("feed_titel") %>">
                <div data-role="navbar">
                    <ul>
                        <li><a href="Default.aspx" class="ui-btn-active ui-state-persist" data-ajax="false">Nyheder</a></li>
                        <li><a href="Settings.aspx" data-ajax="false">Indstillinger</a></li>
                    </ul>
                </div>
                <label for="flip-checkbox-2"></label>
                <input data-role="flipswitch" name="flip-checkbox-2" id="flip-checkbox-2" data-on-text="Light" data-off-text="Dark" data-wrapper-class="custom-label-flipswitch" type="checkbox">
                <asp:Repeater ID="Repeater_Channels" OnItemDataBound="Repeater_Channels_ItemDataBound" runat="server">
                    <ItemTemplate>
                        <div class="inner_box">
                            <div class="rss_overskrift">
                                <h2><%# XPath("title") %></h2>
                            </div>
                            <h3><%# XPath("description") %><h3>
                                <a href='<%# XPath("link") %>'>gå til kilde</a>
                                <br />
                                <hr />
                                <div id="<%# XPath("title") %>">


                                    <asp:Repeater ID="Repeater_Item" runat="server">
                                        <ItemTemplate>
                                            <h3><%# XPath("title") %></h3>
                                            <p><%# XPath("description") %></p>
                                            <a href='<%# XPath("link") %>'>Læs mere</a>
                                            <h5><%# XPath("pubDate") %></h5>
                                            <hr />
                                        </ItemTemplate>
                                    </asp:Repeater>

                                </div>
                        </div>
                    </ItemTemplate>
                </asp:Repeater>
            </div>
        </ItemTemplate>
    </asp:Repeater>
</asp:Content>

