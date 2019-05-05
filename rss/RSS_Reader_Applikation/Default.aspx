<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="_Default" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>RSS Reader Applikation</title>
    <link href="StyleSheet.css" rel="stylesheet" />
    <script
        src="https://code.jquery.com/jquery-3.1.1.min.js"
        integrity="sha256-hVVnYaiADRTO2PzUGmuLJr8BLUSjGIZsDYGmIJLv2b8="
        crossorigin="anonymous"></script>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <h1>RSS Reader</h1>
            <div id="indhold">
            </div>


        </div>
    </form>
    <script>
        hent_data();

        setInterval(hent_data, 5000);

        function hent_data() {
            $('#indhold').load('Ajax.aspx');
        }
    </script>

</body>
</html>
