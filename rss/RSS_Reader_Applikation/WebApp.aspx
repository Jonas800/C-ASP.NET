<%@ Page Language="C#" AutoEventWireup="true" CodeFile="WebApp.aspx.cs" Inherits="WebApp" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link rel="stylesheet" href="http://code.jquery.com/mobile/1.4.5/jquery.mobile-1.4.5.min.css" />

    <link href="StyleSheet.css" rel="stylesheet" />
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <h1>RSS Reader</h1>
            <div id="indhold">
            </div>

            <div data-role="page" id="newyork" class="demo-page" data-dom-cache="true" data-theme="b"  data-next="buenosaires" data-url="newyork">
                <div role="main" class="ui-content">
                    <div id="trivia-newyork" class="trivia ui-content" data-role="popup" data-position-to="window" data-tolerance="50,30,30,30" data-theme="a">
                        <a href="#" data-rel="back" class="ui-btn ui-btn-right ui-btn-b ui-btn-icon-notext ui-icon-delete ui-corner-all">Close</a>
                        <p>Here some text.</p>
                    </div>
                    <!-- /popup -->
                </div>
                <!-- /content -->
            </div>
            <div data-role="page" id="buenosaires" class="demo-page" data-dom-cache="true" data-theme="b" data-prev="newyork" data-next="nextCity" data-url="buenosaires">
                <div role="main" class="ui-content">
                    <div id="trivia-buenosaires" class="trivia ui-content" data-role="popup" data-position-to="window" data-tolerance="50,30,30,30" data-theme="a">
                        <a href="#" data-rel="back" class="ui-btn ui-btn-right ui-btn-b ui-btn-icon-notext ui-icon-delete ui-corner-all">Close</a>
                        <p>Here some text.</p>
                    </div>
                    <!-- /popup -->
                </div>
                <!-- /content -->
            </div>
            <script src="http://code.jquery.com/jquery-1.11.1.min.js"></script>
            <script src="http://code.jquery.com/mobile/1.4.5/jquery.mobile-1.4.5.min.js"></script>
            <script src="Swipe.js"></script>

        </div>
    </form>
    <%--<script>
        hent_data();

        setInterval(hent_data, 5000);

        function hent_data() {
            $('#indhold').load('Ajax.aspx');
        }
    </script>--%>
</body>
</html>
