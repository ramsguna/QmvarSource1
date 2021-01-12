<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="Maintenance.aspx.vb" Inherits="Ganges33.Maintenance" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
          <div style="text-align:center;">
        <h2>
            <font color="black"> Processing was interrupted.<br />
                Sorry, please log in and try again.<br />
                 If the problem persists, please contact your MVS administrator.  </font>
        </h2>
        <%--ログイン画面へボタン--%>
        <asp:Button ID="btnBack" runat="server" Text=">> Login Screen" CssClass="Button120" 
            UseSubmitBehavior="False" onclick="btnBack_Click" />
    </div>
    </form>
</body>
</html>
