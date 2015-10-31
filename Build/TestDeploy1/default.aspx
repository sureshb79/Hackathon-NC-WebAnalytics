<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="default.aspx.cs" Inherits="TestDeploy1._default" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="refresh" content="1"/>
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    <h1> <span style="color:bisque">  Hello Welcome </span></h1>
        <h2> <span style="color:blueviolet">  <%=dtCurrentDateTime.ToString() %> </span></h2>
    </div>
    </form>
</body>
</html>
