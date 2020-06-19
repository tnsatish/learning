<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Ajax.aspx.cs" Inherits="SampleWebApplication1.Ajax" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Hare Krishna</title>
	<script type="text/javascript" src="Scripts/jquery-3.5.1.min.js">
    </script>
</head>
<body>
    <form id="form1">
    <div id="bidamount">
        Hare Krishna.

		In this, we try to make an Ajax call and in that Ajax call, it tries to do something in a thread even when main thread is not active. 
    </div>
    </form>
    <input type="button" id="btnInput" value="Submit" />
    <script type="text/javascript">
        $("#btnInput").click(function ()
        {
            $("#bidamount").html("Hare Rama");
            var bidData = { op: "PostBid", auctionId: "10", amount: "100" };
            $.ajax({
                url: 'Handler1.ashx',
                type: 'POST',
                contentType: 'application/json; charset=utf-8',
                dataType: 'json',
                data: { op: "PostBid", auctionId: "10", amount: "100" },
                //responseType: "json",
                success: onComplete,
                error: onFail
            });
            function onComplete(result) {
                $("#bidamount").html("Got Result");
            }
            function onFail(result) {
                $("#bidamount").html("Result Failed " + result.responseText);
            }
        });
    </script>
</body>
</html>
