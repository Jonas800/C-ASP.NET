<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="_Default" %>


<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

        <asp:Repeater ID="Repeater_Pictures" runat="server">
            <HeaderTemplate>
                <div id="thumbnails">
            </HeaderTemplate>
            <ItemTemplate>
                <a href="<%# Eval("picture_name", "/billeder/{0}") %>" data-lightbox="html5Gallery" data-title="<h5><%# Eval("picture_title") %></h5><p><%# Eval("picture_comment") %></p><p><%# Eval("picture_datetime") %></p>">
                    <img src="<%# Eval("picture_name", "/billeder/thumbs/{0}") %>" alt="<%# Eval("picture_title") %>"/>
                </a>
            </ItemTemplate>
            <FooterTemplate>
                </div>
            </FooterTemplate>
        </asp:Repeater>
    
        <asp:Repeater ID="Repeater_Filtered_Pictures_Large" runat="server" Visible="false">
            <HeaderTemplate>
                <div id="filtered_pictures">
            </HeaderTemplate>
            <ItemTemplate>
                <a href="<%# Eval("picture_name", "/billeder/{0}") %>" data-lightbox="html5Gallery" data-title="<h5><%# Eval("picture_title") %></h5><p><%# Eval("picture_comment") %></p><p><%# Eval("picture_datetime") %></p>">
                    <img src="<%# Eval("picture_name", "/billeder/resized/{0}") %>" alt="<%# Eval("picture_title") %>"/>
                </a>
            </ItemTemplate>
            <FooterTemplate>
                </div>
            </FooterTemplate>
        </asp:Repeater>

    <asp:Repeater ID="Repeater_Slider" runat="server" Visible="false">
        <HeaderTemplate>
            <div class="swiper-container">
            <div class="swiper-wrapper">
        </HeaderTemplate>
        <ItemTemplate>
            <div class="swiper-slide"><img src="<%# Eval("picture_name", "/billeder/resized/{0}") %>"/></div>
        </ItemTemplate>
        <FooterTemplate>
                </div>
                <!-- Add Pagination -->
                <div class="swiper-pagination"></div>
                <!-- Add Arrows -->
                <div class="swiper-button-next"></div>
                <div class="swiper-button-prev"></div>
            </div>
        </FooterTemplate>
    </asp:Repeater>


    <footer>
        Jonas Olsen &copy; 2016
    </footer>

    <script type="text/javascript" src="http://code.jquery.com/jquery-latest.min.js"></script>
    <script src="https://cdnjs.cloudflare.com/ajax/libs/Swiper/3.2.5/js/swiper.min.js"></script>
    <script src="lightbox/js/lightbox.js"></script>
    <script src="java.js"></script>
</asp:Content>

