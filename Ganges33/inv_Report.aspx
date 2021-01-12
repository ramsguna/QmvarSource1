<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Site3.Master" CodeBehind="inv_Report.aspx.vb" Inherits="Ganges33.inv_Report2" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script src="http://ajax.googleapis.com/ajax/libs/jquery/1.7.0/jquery.min.js"></script>
    <link href="CSS/Common/Inv-report.css" rel="stylesheet" />
    <link type="text/css" href="http://ajax.googleapis.com/ajax/libs/jqueryui/1/themes/start/jquery-ui.css" rel="stylesheet">
    <script type="text/javascript" src="http://ajax.googleapis.com/ajax/libs/jqueryui/1/jquery-ui.min.js"></script>
   </asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="div-entire-tbl" style="background-image: url('pagewall_2/wall_inventry-2.png')">
       <div class="div-report-tbl">
        <table class="tbl-report">
            <tr>
                <td class="td-chk-use-serial">
                   <asp:CheckBox ID="chkUseSerial" runat="server" AutoPostBack="True" CssClass="font-common-report" />
                    <asp:Label ID="Label18" runat="server" Text="use Serial" CssClass="font-common-report"></asp:Label>
                </td>
                <td class="td-chk-unuse-serial">
                   <asp:CheckBox ID="chkUnUseSerial" runat="server" AutoPostBack="True" CssClass="font-common-report" />
                    <asp:Label ID="Label19" runat="server" Text="unuse Serial" CssClass="font-common-report"></asp:Label>
                </td>
                <td class="td-blank1">&nbsp;</td>
                <td class="td-blank2">&nbsp;</td>
                <td class="td-blank3">&nbsp;</td>
            </tr>
            <tr>
                <td class="td-lbl-csv-在庫差異確認">
                    <asp:Label ID="Label20" runat="server" Text="CSV File Import" CssClass="font-common-report"></asp:Label>
                </td>
                <td class="td-txtbox-csv">
                    <asp:FileUpload ID="FileUploadInv" runat="server" Width="319px" CssClass="font-common-report" />
                    <asp:ImageButton ID="btnCsv" runat="server" Height="29px" ImageUrl="~/icon/folder-13.png" Width="36px" CssClass="font-common-report" />
                </td>
                <td class="td-blank4">
                     &nbsp;</td>
                <td class="td-blank4"></td>
                <td class="td-blank4"></td>
            </tr>
            <tr>
                <td class="td-lbl-csv-inventory-difference-ckeck">
                    <asp:Label ID="Label21" runat="server" Text="在庫差異確認" CssClass="font-common-report"></asp:Label>
                </td>
                <td class="td-btn-difference">
                  <asp:ImageButton ID="btnDifference" runat="server" Height="59px" ImageUrl="~/icon/differnce.png" Width="163px" CssClass="font-common-report" />
                </td>
                <td class="td-blank4">&nbsp;</td>
                <td class="td-blank4">&nbsp;</td>
                <td class="td-blank4">&nbsp;</td>
            </tr>
            <tr>
                <td class="td-lbl-current-output">
                    <asp:Label ID="Label22" runat="server" Text="現在個出力" CssClass="font-common-report"></asp:Label>
                </td>
                <td class="td-btn-stock-outbut">
                    <asp:ImageButton ID="btnStockOutput" runat="server" Height="59px" ImageUrl="~/icon/stock_out.png" Width="163px" CssClass="font-common-report" />
                </td>
                <td class="td-blank4"></td>
                <td class="td-lbl-inventory-report">
                    <asp:Label ID="Label23" runat="server" Text="棚卸報告" CssClass="font-common-report"></asp:Label>
                </td>
                <td class="td-btn-inventary">
                     <asp:ImageButton ID="btnInventoryReport" runat="server" Height="59px" ImageUrl="~/icon/inventory_report.png" Width="163px" CssClass="font-common-report" />
                </td>
            </tr>
            <tr>         
               
                <td class="td-blank3"></td>                
            </tr>
        </table>

        <asp:Button ID="BtnCancel" runat="server" Text="Button" style="display:none;"/>
    </div>
  </div>

  <div id="dialog" title="message" style="display:none;"> >
    <asp:Label ID="lblMsg" runat="server" Text=""></asp:Label>
  </div>

</asp:Content>
