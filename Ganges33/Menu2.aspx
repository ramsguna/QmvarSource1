<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Site2.Master" CodeBehind="Menu2.aspx.vb" Inherits="Ganges33.Menu2" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
        <script src="http://ajax.googleapis.com/ajax/libs/jquery/1.7.0/jquery.min.js"></script>
    <link href="CSS/Common/Menu2.css" rel="stylesheet" />
    <link type="text/css" href="http://ajax.googleapis.com/ajax/libs/jqueryui/1/themes/start/jquery-ui.css" rel="stylesheet">
    <script type="text/javascript" src="http://ajax.googleapis.com/ajax/libs/jqueryui/1/jquery-ui.min.js"></script>
        
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="tbl-Menu-Entire-Tbl ">
       

        <table class="tbl-Menu-tbl">
            <tr>
                <td class="td-btn-tbl">
                    <asp:ImageButton ID="btnRepair" runat="server" CssClass="Btn-size" ImageUrl="~/icon/repair.png"  />
                </td>
                <td class="td-lbl-tbl">
                    <asp:Label ID="Label1" runat="server" CssClass="Menu-Font" Text="Repair"></asp:Label>
                </td>
                <td class="td-btn-tbl">
                    <asp:ImageButton ID="btnAnalysis" runat="server" CssClass="Btn-size" ImageUrl="~/icon/analysis.png"  />
                </td>
                <td class="td-lbl-tbl">
                    <asp:Label ID="Label6" runat="server" CssClass="Menu-Font" Text="Analysis"></asp:Label>
                </td>
                
                <td class="td-News" rowspan = "4">
                    <asp:Label ID="Label7" runat="server" Text="News" CssClass="lb"></asp:Label>　<br />
                    <asp:TextBox ID="TextBox1" runat="server" Height="519px" TextMode="MultiLine" Width="505px"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td class="td-btn-tbl">
                    <asp:ImageButton ID="btnInventory" runat="server" CssClass="Btn-size" ImageUrl="~/icon/inventory.png"  />
                </td>
                <td class="td-lbl-tbl">
                    <asp:Label ID="Label2" runat="server" CssClass="Menu-Font" Text="Inventory"></asp:Label>
                </td>
                 <td class="td-btn-tbl">
                    <asp:ImageButton ID="btnMoney" runat="server" CssClass="Btn-size" ImageUrl="~/icon/money.png" />
                </td>
                <td class="td-lbl-tbl">
                    <asp:Label ID="Label3" runat="server" CssClass="Menu-Font" Text="Money"></asp:Label>
                </td>
                
            </tr>
            <tr>
                <td class="td-btn-tbl">
                    <asp:ImageButton ID="BtnDailyReport" runat="server" CssClass="Btn-size" ImageUrl="~/icon/daylyreport.png"  />
                </td>
                <td class="td-lbl-tbl">
                    <asp:Label ID="Label4" runat="server" CssClass="Menu-Font" Text="Daily Report"></asp:Label>
                </td>
               
            </tr>
            <tr>
                <td class="td-information-tbl" colspan = "4">
                    <asp:Label ID="Label5" runat="server" Text="Information" CssClass="lbl-information-News"></asp:Label> <br />
                    <asp:TextBox ID="TextTodayMsg" runat="server" Height="65px" Width="687px" CssClass="Textbox-information" TextMode="MultiLine" ReadOnly="True"></asp:TextBox>
                </td>
            </tr>
        </table>
       

    </div>

    <div id="dialog" title="message" style="display:none;"> >
        <asp:Label ID="lblMsg" runat="server" Text=""></asp:Label>
    </div>

</asp:Content>