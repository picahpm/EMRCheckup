<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="TestCalendar.aspx.cs" Inherits="CheckUpToDoList.TestCalendar" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta charset="utf-8" />
    <title></title>

    <link rel="stylesheet" href="http://code.jquery.com/ui/1.10.3/themes/smoothness/jquery-ui.css" />
  <script src="http://code.jquery.com/jquery-1.9.1.js"></script>
  <script src="http://code.jquery.com/ui/1.10.3/jquery-ui.js"></script>
  <link rel="stylesheet" href="http://jqueryui.com/datepicker/resources/demos/style.css" />
  <script>
      $(function () {
          $("#datepicker").datepicker();
      });
  </script>
</head>
<body>
    <form id="form1" runat="server">
    <p>Date: <input type="text" id="datepicker" /></p>
    </form>
</body>
</html>
