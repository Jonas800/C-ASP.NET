<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="Møbel.aspx.cs" Inherits="Møbel" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <link href="bootstrap-3.3.7-dist/css/bootstrap.min.css" rel="stylesheet" />
    <link href="StyleSheet.css" rel="stylesheet" />
    <script src="bootstrap-3.3.7-dist/js/bootstrap.min.js"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <article class="møbel">
        <asp:Repeater ID="Repeater_Carousel" runat="server">
            <HeaderTemplate>
                <div id="myCarousel" class="carousel slide" data-ride="carousel">
                    <div class="carousel-inner">
            </HeaderTemplate>
            <ItemTemplate>
                <div data-slide-number="<%# Container.ItemIndex %>" class="item <%# (Container.ItemIndex == 0 ? "active" : "") %>">
                    <img src="<%# Eval("billede_sti", "/billeder/produkter/{0}") %>" alt="">
                </div>
            </ItemTemplate>
            <FooterTemplate>
                </div>
                <a class="left carousel-control" href="#myCarousel" role="button" data-slide="prev">
                    <span class="glyphicon glyphicon-chevron-left" aria-hidden="true"></span>
                    <span class="sr-only">Previous</span>
                </a>
                <a class="right carousel-control" href="#myCarousel" role="button" data-slide="next">
                    <span class="glyphicon glyphicon-chevron-right" aria-hidden="true"></span>
                    <span class="sr-only">Next</span>
                </a>
                </div>
            </FooterTemplate>
        </asp:Repeater>

        <asp:Repeater ID="Repeater_Møbel" runat="server">
            <ItemTemplate>
                <h2><%# Eval("produkt_navn") %></h2>
                <div><%# Eval("produkt_beskrivelse") %></div>

                <h2>Detajler</h2>
                <ul>
                    <li>Designer: <%# Eval("designer_navn") %></li>
                    <li>Design år: <%# Eval("produkt_aar") %></li>
                    <li>Varenr: <%# Eval("produkt_varenummer") %></li>
                    <li>Pris: <%# Eval("produkt_pris") %></li>
                </ul>
            </ItemTemplate>
        </asp:Repeater>

        <asp:Repeater ID="Repeater_Thumbs" runat="server">
            <HeaderTemplate>
                <h2>Varianter</h2>
                <div class="col-md-12" id="slider-thumbs">
                    <!-- thumb navigation carousel items -->
                    <ul class="list-inline">
            </HeaderTemplate>
            <ItemTemplate>
                <li class="thumb"><a id="carousel-selector-<%# Container.ItemIndex %>" class="<%# (Container.ItemIndex == 0 ? "selected" : "") %>">
                    <img src="<%# Eval("billede_sti", "/billeder/produkter/{0}") %>" class="img-responsive">
                </a></li>
            </ItemTemplate>
            <FooterTemplate>
                </ul>
                </div>
            </FooterTemplate>
        </asp:Repeater>
    </article>
    <script type="text/javascript">
        $('#myCarousel').carousel({
            interval: 4000
        });

        // handles the carousel thumbnails
        $('[id^=carousel-selector-]').click(function () {
            var id_selector = $(this).attr("id");
            var id = id_selector.substr(id_selector.length - 1);
            id = parseInt(id);
            $('#myCarousel').carousel(id);
            $('[id^=carousel-selector-]').removeClass('selected');
            $(this).addClass('selected');
        });

        // when the carousel slides, auto update
        $('#myCarousel').on('slid', function (e) {
            var id = $('.item.active').data('slide-number');
            id = parseInt(id);
            $('[id^=carousel-selector-]').removeClass('selected');
            $('[id=carousel-selector-' + id + ']').addClass('selected');
        });
    </script>
</asp:Content>

