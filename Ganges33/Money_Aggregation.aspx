<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Site4.Master" CodeBehind="Money_Aggregation.aspx.vb" Inherits="Ganges33.Money_Syukei" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="ajaxToolkit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script src="http://ajax.googleapis.com/ajax/libs/jquery/1.7.0/jquery.min.js"></script>
    <link href="CSS/Common/Money_Aggregation.css" rel="stylesheet" />
    <link type="text/css" href="http://ajax.googleapis.com/ajax/libs/jqueryui/1/themes/start/jquery-ui.css" rel="stylesheet">
    <script type="text/javascript" src="http://ajax.googleapis.com/ajax/libs/jqueryui/1/jquery-ui.min.js"></script>
               
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="div-entire-tbl" style="background-image: url('pagewall_2/wall_money-2.png')">
       <div class="div-money-aggregation">
        <table class="tbl-money-aggregation">
            <tr>
                <td class="td-blank1">
                    <asp:Button ID="HiddenDummyButton" runat="server" Text="Button" onClientClick="return false;" style="display:none;"/>
                </td>
                <td class="td-blank1"></td>
                <td class="td-blank2"></td>
                <td class="td-blank2"></td>
                <td class="td-blank2"></td>
                <td class="td-blank2"></td>
                <td class="td-blank2"></td>
                <td class="td-blank2"></td>
            </tr>
            <tr>
                <td colspan = "2" align = "right">    
                         <asp:Label ID="Label23" runat="server" CssClass="lbl-font-large" Text="Repair complete date"></asp:Label><span class="auto-style171">&nbsp;&nbsp;&nbsp;
                </span>
                </td>     
                
                <td colspan ="3" class="td-hidden-script">
                    <asp:ScriptManager ID="ScriptManager1" runat="server">
                    </asp:ScriptManager>
                    <asp:TextBox ID="TextCompleteDateFrom" runat="server"  CssClass="txtbox-scriptmanagement"></asp:TextBox>
                    <ajaxToolkit:CalendarExtender ID="TextCompleteDateFrom_CalendarExtender" runat="server" BehaviorID="TextCompleteDateFrom_CalendarExtender" TargetControlID="TextCompleteDateFrom" PopupPosition="Right">
                    </ajaxToolkit:CalendarExtender>
                </td>
                <td class="td-btn-today-send">
                    <asp:Button ID="btnToday" runat="server" Text="today" Height="29px" Width="81px" CssClass="btn-today" />
                </td>
                <td class="td-btn-today-send" colspan = "2">
                    <asp:ImageButton ID="btnSend" runat="server"  ImageUrl="~/icon/send.png" Height="29px" Width="81px"  CssClass="btn-send" />
                </td>
            </tr>
            <tr>
                <td class="td-blank1"></td>
                <td class="td-blank2"></td>
                <td class="td-blank2"></td>
                <td class="td-blank2"></td>
                <td class="td-blank2"></td>
                <td class="td-blank2"></td>
                <td class="td-blank2"></td>
                <td class="td-blank2"></td>
            </tr>
            <tr>
                <td class="td-blank1"></td>
                <td class="td-blank2">
                    <asp:Label ID="Label24" runat="server" CssClass="lbl-font-large" Text="Total Number"></asp:Label>
                </td>
                <td class="td-blank2">
                    <asp:Label ID="Label25" runat="server" CssClass="lbl-font-large" Text="Count :"></asp:Label>
                    <asp:Label ID="lblCount" runat="server" CssClass="lbl-font-common"></asp:Label>
                </td>
                <td class="td-blank2"></td>
                <td class="td-blank2"></td>
                <td class="td-blank2"></td>
                <td class="td-blank2"></td>
                <td class="td-blank3"></td>
            </tr>
            <tr>
                <td class="td-blank2"></td>
                <td class="td-cash-card-number-climpamount"></td>
                <td class="td-cash-card-number-climpamount">
                    <asp:Label ID="Label10" runat="server" Text="IW" CssClass="lbl-font-common"></asp:Label>
                </td>
                <td class="td-cash-card-number-climpamount">
                    <asp:Label ID="Label11" runat="server" Text="OOW(cash)" CssClass="lbl-font-common"></asp:Label>
                </td>
                <td class="td-cash-card-number-climpamount">
                    <asp:Label ID="Label16" runat="server" Text="OOW(card)" CssClass="lbl-font-common"></asp:Label>
                </td>
                <td class="td-cash-card-number-climpamount">
                    <asp:Label ID="Label13" runat="server" Text="other(cash)" CssClass="lbl-font-common"></asp:Label>
                </td>
                <td class="td-cash-card-number-climpamount">
                    <asp:Label ID="Label15" runat="server" Text="other(card)" CssClass="lbl-font-common"></asp:Label>
                </td>
                <td class="td-blank3"></td>
            </tr>
            <tr>
                <td class="td-blank1"></td>
                <td class="td-cash-card-number-climpamount">
                    <asp:Label ID="Label7" runat="server" Text="number" CssClass="lbl-font-common"></asp:Label>
                </td>
                <td class="td-cnt-sum1">
                    <asp:Label ID="lblIWCnt" runat="server" CssClass="lbl-font-common"></asp:Label>
                </td>
                <td class="td-cnt-sum1">
                    <asp:Label ID="lblOOWCashCnt" runat="server" CssClass="lbl-font-common"></asp:Label>
                </td>
                <td class="td-cnt-sum1">
                    <asp:Label ID="lblOOWCardCnt" runat="server" CssClass="lbl-font-common"></asp:Label>
                </td>
                <td class="td-cnt-sum1">
                    <asp:Label ID="lblOtherCashCnt" runat="server" CssClass="lbl-font-common"></asp:Label>
                </td>
                <td class="td-cnt-sum1">
                    <asp:Label ID="lblOtherCardCnt" runat="server" CssClass="lbl-font-common"></asp:Label>
                </td>
                <td class="td-blank3"></td>
            </tr>
            <tr>
                <td class="td-blank1"></td>
                <td class="td-sum0-amounttaxin">
                    <asp:Label ID="Label9" runat="server" Text="amount(taxin)" CssClass="lbl-font-common"></asp:Label>
                </td>
                <td class="td-sum0-amounttaxin">
                    <asp:Label ID="lblIWSum0" runat="server" CssClass="lbl-font-common"></asp:Label>
                </td>
                <td class="td-sum0-amounttaxin">
                    <asp:Label ID="lblOOWCashSum0" runat="server" CssClass="lbl-font-common"></asp:Label>
                </td>
                <td class="td-sum0-amounttaxin">
                    <asp:Label ID="lblOOWCardSum0" runat="server" CssClass="lbl-font-common"></asp:Label>
                </td>
                <td class="td-sum0-amounttaxin">
                    <asp:Label ID="lblOtherCashSum0" runat="server" CssClass="lbl-font-common"></asp:Label>
                </td>
                <td class="td-sum0-amounttaxin">
                    <asp:Label ID="lblOtherCardSum0" runat="server" CssClass="lbl-font-common"></asp:Label>
                </td>
                <td class="td-blank3"></td>
            </tr>
            <tr>
                <td class="td-blank1"></td>
                <td class="td-cash-card-number-climpamount">
                    <asp:Label ID="Label28" runat="server" Text="claim amount(taxin)" CssClass="lbl-font-common"></asp:Label>
                </td>
                <td class="td-cnt-sum1">
                    <asp:Label ID="lblIWSum1" runat="server" CssClass="lbl-font-common"></asp:Label>
                </td>
                <td class="td-cnt-sum1">
                    <asp:Label ID="lblOOWCashSum1" runat="server" CssClass="lbl-font-common"></asp:Label>
                </td>
                <td class="td-cnt-sum1">
                    <asp:Label ID="lblOOWCardSum1" runat="server" CssClass="lbl-font-common"></asp:Label>
                </td>
                <td class="td-cnt-sum1">
                    <asp:Label ID="lblOtherCashSum1" runat="server" CssClass="lbl-font-common"></asp:Label>
                </td>
                <td class="td-cnt-sum1">
                    <asp:Label ID="lblOtherCardSum1" runat="server" CssClass="lbl-font-common"></asp:Label>
                </td>
                <td class="td-blanl3"></td>
            </tr>
            <tr>
                <td class="td-blank1"></td>
                <td class="td-blank2"></td>
                <td class="td-blank2"></td>
                <td class="td-blank2"></td>
                <td class="td-blank2"></td>
                <td class="td-blank2"></td>
                <td class="td-blank2"></td>
                <td class="td-blank3"></td>
            </tr>
            <tr>
                <td class="td-blank1"></td>
                <td class="td-lbl-common">
                    <asp:Label ID="Label1" runat="server" CssClass="lbl-font-xlarge" Text="Open Cash"></asp:Label>
                </td>
                <td class="td-lbl-common">
                    <asp:Label ID="lblReserve" runat="server" CssClass="lbl-font-common"></asp:Label>
                </td>
                <td class="td-lbl-common">
                    <asp:Label ID="Label18" runat="server" CssClass="lbl-font-xlarge" Text="cash total"></asp:Label>
                </td>
                <td class="td-lbl-cash">
                    <asp:Label ID="lblCashTotal" runat="server" CssClass="lbl-font-common"></asp:Label>
                    </td>
                <td class="td-lbl-common">
                    <asp:Label ID="Label32" runat="server" Text="Discount Number" CssClass="lbl-font-common"></asp:Label>
                </td>
                <td class="td-lbl-common">
                    <asp:Label ID="lblDisNum" runat="server" CssClass="lbl-font-common"></asp:Label>
                    </td>
                <td class="td-blank3"></td>
            </tr>
            <tr>
                <td class="td-blank1"></td>
                <td class="td-blank2"></td>
                <td class="td-blank2"></td>
                <td colspan = "2" class="td-lbl-common">
                    <asp:Label ID="Label20" runat="server" CssClass="lbl-font-common" Text="open deposit + sales(cash)"></asp:Label>
                </td>
                <td class="td-lbl-common">
                    <asp:Label ID="Label33" runat="server" Text="Discount Amount" CssClass="lbl-font-common"></asp:Label>
                </td>
                <td class="td-lbl-common">
                    <asp:Label ID="lblDisAmount" runat="server" CssClass="lbl-font-common"></asp:Label>
                </td>
                <td class="td-blank3"></td>
            </tr>
            <tr>
                <td class="td-blank1"></td>
                <td class="td-blank2"></td>
                <td class="td-blank2"></td>
                <td class="td-blank2"></td>
                <td class="td-blank2"></td>
                <td colspan = "2" class="td-blank2">
                </td>
                <td class="td-blank3"></td>
            </tr>
            <tr>
                <td class="td-blank1"></td>
                <td class="auto-style175">
                    <asp:Label ID="Label3" runat="server" CssClass="lbl-font-xlarge" Text="Deposit"></asp:Label>
                </td>
                <td class="td-lbl-common">
                    <asp:Label ID="lblDeposit" runat="server" CssClass="lbl-font-common"></asp:Label>
                </td>
                <td class="td-lbl-common">
                    <asp:Label ID="Label27" runat="server" CssClass="lbl-font-xlarge" Text="sales"></asp:Label>
                </td>
                <td class="td-lbl-sales">
                    <asp:Label ID="lblSales" runat="server" CssClass="lbl-font-common"></asp:Label>
                </td>
                <td class="td-blank2"></td>
                <td class="td-blank2"></td>
                <td class="td-blank2"></td>
            </tr>
            <tr>
                <td class="td-blank2"></td>
                <td class="td-blank2"></td>
                <td class="td-blank3"></td>
                <td class="auto-style191" colspan = "3">
                    <asp:Label ID="Label26" runat="server" Text="sales(cash) + sales(card)+IW+other+GST" CssClass="lbl-font-common"></asp:Label>
                </td>
                <td class="td-blank3"></td>
            </tr>
                        <tr>
                <td class="td-blank1"></td>
                <td class="td-blank2"></td>
                <td class="td-blank2"></td>
                <td class="td-blank2"></td>
                <td class="td-blank3"></td>
                <td colspan = "2" class="td-blank2">
                </td>
                <td class="td-blank3"></td>
            </tr>
            <tr>
                <td class="td-blank1"></td>
                <td class="td-lbl-common" >
                    <asp:Label ID="Label29" runat="server" CssClass="lbl-font-xlarge" Text="Customer Advance"></asp:Label>
                </td>
                <td class="td-lbl-common">
                    <asp:Label ID="lblDepositCusto" runat="server" CssClass="lbl-font-common"></asp:Label>
                </td>
                <td colspan = "3">
                    <asp:Label ID="Label30" runat="server" CssClass="lbl-font-xlarge" Text="Number of Advance"></asp:Label>&nbsp;&nbsp;
                    <asp:Label ID="lblNumCusto" runat="server" CssClass="lbl-font-common"></asp:Label>
                </td>
                <td class="td-blank2"></td>
                <td class="td-blank3"></td>
            </tr>
            <tr>
                <td class="td-blank1"></td>
                <td class="td-blank2"></td>
                <td class="td-blank2"></td>
                <td class="td-blank3" colspan = "2">
                    &nbsp;</td>
                <td class="td-blank2"></td>
                <td class="td-blank3"></td>
            </tr>
        </table>
    </div>
  </div>

  <div id="dialog" title="message" style="display:none;"> >
        <asp:Label ID="lblMsg" runat="server" Text=""></asp:Label>
  </div>

    <%-- <script type="text/javascript">  ASP側に埋め込み済
        $(function () {
            $("#dialog" ).dialog({
                width: 400,
                buttons:
                {
                    "OK": function () {
                        $(this).dialog('close');
                        $('[id$="BtnOK"]').click();
                    },
                    "CANCEL": function () {
                        $(this).dialog('close');
                        $('[id$="BtnCancel"]').click();
                    }
                }
            });
        });    
    </script>--%>
</asp:Content>
