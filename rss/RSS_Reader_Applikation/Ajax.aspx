<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Ajax.aspx.cs" Inherits="Ajax" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="StyleSheet.css" rel="stylesheet" />
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <%--            <asp:Repeater ID="Repeater_RSS" runat="server" OnItemDataBound="Repeater_RSS_ItemDataBound">
                <ItemTemplate>
                    <%# XPath("title") %>
                    <asp:Repeater ID="Repeater2" runat="server">
                        <ItemTemplate>
                            <%# XPath("title") %>
                            <%# XPath("link") %>
                            <%# XPath("description") %>
                            <%# XPath("pubDate") %>
                        </ItemTemplate>
                    </asp:Repeater>
                </ItemTemplate>
            </asp:Repeater>--%>

            <asp:Repeater ID="Repeater_Feeds"
                OnItemDataBound="Repeater_Feeds_ItemDataBound"
                runat="server">
                <ItemTemplate>
                    <div class="outer_box">

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
        </div>
    </form>
</body>
</html>

<%--<div data-role="page" id="city" class="demo-page" data-dom-cache="true" data-theme="b" data-prev="prevCity" data-next="nextCity" data-url="city">
                <div role="main" class="ui-content">
                    <div id="trivia-city" class="trivia ui-content" data-role="popup" data-position-to="window" data-tolerance="50,30,30,30" data-theme="a">
                        <a href="#" data-rel="back" class="ui-btn ui-btn-right ui-btn-b ui-btn-icon-notext ui-icon-delete ui-corner-all">Close</a>
                        <p>Here some text.</p>
                    </div>
                    <!-- /popup -->
                </div>
                <!-- /content -->
            </div>



            <asp:Repeater ID="Repeater1"
                OnItemDataBound="Repeater_Feeds_ItemDataBound"
                runat="server">
                <ItemTemplate>
                    <div data-role="page" id="city" class="outer_box demo-page" data-dom-cache="true" data-theme="b" data-prev="prevCity" data-next="nextCity" data-url="city">

                        <asp:Repeater ID="Repeater_Channels" OnItemDataBound="Repeater_Channels_ItemDataBound" runat="server">
                            <ItemTemplate>
                                <div id="trivia-city" class="trivia ui-content inner_box" data-role="popup" data-position-to="window" data-tolerance="50,30,30,30" data-theme="a">
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
            </asp:Repeater>--%>
