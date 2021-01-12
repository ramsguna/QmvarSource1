<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="Rpa_logview.aspx.vb" Inherits="Ganges33.Rpa_logview" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <table>
                <tr><td style="text-align:center"><asp:Button ID="btnClose" runat="server" Text="Close" /></td></tr>

                 <tr><td><asp:TextBox ID="txtViewLog" runat="server" TextMode="MultiLine" Height="768px" Width="909px"></asp:TextBox></td></tr>

            </table>
            

            
        </div>
    </form>
</body>
</html>
