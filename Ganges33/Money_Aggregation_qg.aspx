<%@ Page Language="vb" AutoEventWireup="false"  MasterPageFile="~/Site4.Master" CodeBehind="Money_Aggregation_qg.aspx.vb" Inherits="Ganges33.Money_Aggregation_qg" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="ajaxToolkit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script src="http://ajax.googleapis.com/ajax/libs/jquery/1.7.0/jquery.min.js"></script>
    <link href="CSS/Common/Money_Aggregation_qg.css" rel="stylesheet" />
    <link type="text/css" href="http://ajax.googleapis.com/ajax/libs/jqueryui/1/themes/start/jquery-ui.css" rel="stylesheet">
    <script type="text/javascript" src="http://ajax.googleapis.com/ajax/libs/jqueryui/1/jquery-ui.min.js"></script>
         
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="div-entire-tbl" style="background-image: url('pagewall_2/wall_money-2.png')">
       <div class="div-money_aggregation-tbl">
        <table class="tbl-money_aggregation">
            <tr>
                <td class="td-btn-hidden">
                    <asp:Button ID="HiddenDummyButton" runat="server" Text="Button" onClientClick="return false;" style="display:none;"/>
                </td>
                <td class="td-blank1"></td>
                <td class="td-blank2"></td>
                <td class="td-blank2"></td>
                <td class="td-blank2"></td>
                <td class="td-blank2"></td>
                <td class="td-blank2"></td>
            </tr>
            <tr>
                <td colspan = "2" align = "right">    
                         <asp:Label ID="Label23" runat="server" CssClass="lbl-font-Repair-date-total_number" Text="Repair complete date"></asp:Label><span class="lbl-common-font">&nbsp;&nbsp;&nbsp;
                </span>
                </td>     
                
                <td colspan ="3" class="td-ScriptManager">
                    <asp:ScriptManager ID="ScriptManager1" runat="server">
                    </asp:ScriptManager>
                    <asp:TextBox ID="TextCompleteDateFrom" runat="server" Height="19px" Width="197px" CssClass="lbl-common-font"></asp:TextBox>
                    <ajaxToolkit:CalendarExtender ID="TextCompleteDateFrom_CalendarExtender" Format="yyyy/MM/dd" runat="server" BehaviorID="TextCompleteDateFrom_CalendarExtender" TargetControlID="TextCompleteDateFrom" PopupPosition="Left">
                    </ajaxToolkit:CalendarExtender>
                </td>
                <td class="td-btn-send">
                    <asp:Button ID="btnToday" runat="server" Text="Today" Height="29px" Width="81px" CssClass="btn-today" />
                </td>
                <td class="td-btn-send" colspan = "2">
                    <asp:ImageButton ID="btnSend" runat="server" Height="29px" ImageUrl="~/icon/send.png" Width="81px" CssClass="lbl-common-font" />
                </td>
            </tr>
            <tr>
                <td class="td-blank3"></td>
                <td class="td-blank4"></td>
                <td class="td-blank4"></td>
                <td class="td-blank4"></td>
                <td class="td-blank4"></td>
                <td class="td-blank4"></td>
                <td class="td-blank4"></td>
                <td class="td-blank5"></td>
            </tr>
            <tr>
                <td class="td-blank5"></td>
                <td class="td-lbl-total-number">
                    <asp:Label ID="Label24" runat="server" CssClass="lbl-font-Repair-date-total_number" Text="Total Number"></asp:Label>
                </td>
                <td class="td-lbl-count">
                    <asp:Label ID="Label25" runat="server" CssClass="lbl-font-Repair-date-total_number" Text="Count :"></asp:Label>
                    <asp:Label ID="lblCount" runat="server" CssClass="lbl-common-font"></asp:Label>
                </td>
                <td class="td-blank6"></td>
                <td class="td-blank6"></td>
                <td class="td-blank6">
                    &nbsp;</td>
                <td class="td-blank6"></td>
                <td class="td-blank6"></td>
            </tr>
            <tr>
                <td class="td-blank7"></td>
                <td class="td-blank8"></td>
                <td class="td-lbl-cash-card">
                    <asp:Label ID="Label10" runat="server" Text="IW" CssClass="lbl-common-font"></asp:Label>
                </td>
                <td class="td-lbl-cash-card">
                    <asp:Label ID="Label11" runat="server" Text="OOW(cash)" CssClass="lbl-common-font"></asp:Label>
                </td>
                <td class="td-lbl-cash-card">
                    <asp:Label ID="Label16" runat="server" Text="OOW(card)" CssClass="lbl-common-font"></asp:Label>
                </td>
                <td class="td-lbl-cash-card">
                    <asp:Label ID="Label13" runat="server" Text="other(cash)" CssClass="lbl-common-font"></asp:Label>
                </td>
                <td class="td-lbl-cash-card">
                    <asp:Label ID="Label15" runat="server" Text="other(card)" CssClass="lbl-common-font"></asp:Label>
                </td>
                <td class="td-blank6"></td>
            </tr>
            <tr>
                <td class="td-blank7"></td>
                <td class="td-Number-claim">
                    <asp:Label ID="Label7" runat="server" Text="Number" CssClass="lbl-common-font"></asp:Label>
                </td>
                <td class="td-lbl-cnt-claim">
                    <asp:Label ID="lblIWCnt" runat="server" CssClass="lbl-common-font"></asp:Label>
                </td>
                <td class="td-lbl-cnt-claim">
                    <asp:Label ID="lblOOWCashCnt" runat="server" CssClass="lbl-common-font"></asp:Label>
                </td>
                <td class="td-lbl-cnt-claim">
                    <asp:Label ID="lblOOWCardCnt" runat="server" CssClass="lbl-common-font"></asp:Label>
                </td>
                <td class="td-lbl-cnt-claim">
                    <asp:Label ID="lblOtherCashCnt" runat="server" CssClass="lbl-common-font"></asp:Label>
                </td>
                <td class="td-lbl-cnt-claim">
                    <asp:Label ID="lblOtherCardCnt" runat="server" CssClass="lbl-common-font"></asp:Label>
                </td>
                <td class="td-blank6"></td>
            </tr>
            <tr>
                <td class="td-blank7"></td>
                <td class="td-lbl-sum">
                    <asp:Label ID="Label9" runat="server" Text="Amount(taxin)" CssClass="lbl-common-font"></asp:Label>
                </td>
                <td class="td-lbl-sum">
                    <asp:Label ID="lblIWSum" runat="server" CssClass="lbl-common-font"></asp:Label>
                </td>
                <td class="td-lbl-sum">
                    <asp:Label ID="lblOOWCashSum" runat="server" CssClass="lbl-common-font"></asp:Label>
                </td>
                <td class="td-lbl-sum">
                    <asp:Label ID="lblOOWCardSum" runat="server" CssClass="lbl-common-font"></asp:Label>
                </td>
                <td class="td-lbl-sum">
                    <asp:Label ID="lblOtherCashSum" runat="server" CssClass="lbl-common-font"></asp:Label>
                </td>
                <td class="td-lbl-sum">
                    <asp:Label ID="lblOtherCardSum" runat="server" CssClass="lbl-common-font"></asp:Label>
                </td>
                <td class="td-blank6"></td>
            </tr>
            <tr>
                <td class="td-blank6"></td>
                <td class="td-Number-claim">
                    <asp:Label ID="Label28" runat="server" Text="Claim Amount(taxin)" CssClass="lbl-common-font"></asp:Label>
                </td>
                <td class="td-lbl-cnt-claim">
                    <asp:Label ID="lblIWSumClaim" runat="server" CssClass="lbl-common-font"></asp:Label>
                </td>
                <td class="td-lbl-cnt-claim">
                    <asp:Label ID="lblOOWCashSumClaim" runat="server" CssClass="lbl-common-font"></asp:Label>
                </td>
                <td class="td-lbl-cnt-claim">
                    <asp:Label ID="lblOOWCardSumClaim" runat="server" CssClass="lbl-common-font"></asp:Label>
                </td>
                <td class="td-lbl-cnt-claim">
                    <asp:Label ID="lblOtherCashSumClaim" runat="server" CssClass="lbl-common-font"></asp:Label>
                </td>
                <td class="td-lbl-cnt-claim">
                    <asp:Label ID="lblOtherCardSumClaim" runat="server" CssClass="lbl-common-font"></asp:Label>
                </td>
                <td class="td-blank6"></td>
            </tr>
            <tr>
                <td class="td-blank6"></td>
                <td class="td-blank4"></td>
                <td class="td-blank4"></td>
                <td class="td-blank4"></td>
                <td class="td-blank4"></td>
                <td class="td-blank4"></td>
                <td class="td-blank4"></td>
                <td class="td-blank6"></td>
            </tr>
            <tr>
                <td class="td-blank6"></td>
                <td class="td-lbl-opening-bank_DP-sales-customer_ad-Number-ad">
                    <asp:Label ID="Label1" runat="server" CssClass="lbl-common-xlarge-font" Text="Opening Cash"></asp:Label>
                </td>
                <td class="td-lbl-reverse-discount-number-deposite">
                    <asp:Label ID="lblReserve" runat="server" CssClass="lbl-common-font"></asp:Label>
                </td>
                <td class="td-lbl-reverse-discount-number-deposite">
                    <asp:Label ID="Label18" runat="server" CssClass="lbl-common-xlarge-font" Text="Cash Total"></asp:Label>
                </td>
                <td class="td-lbl_sml-cash">
                    <asp:Label ID="lblCashTotal" runat="server" CssClass="lbl-common-font"></asp:Label>
                    </td>
                <td class="td-lbl-reverse-discount-number-deposite">
                    <asp:Label ID="Label32" runat="server" Text="Discount Number" CssClass="lbl-common-font"></asp:Label>
                </td>
                <td class="td-reverse-cash-discount-number">
                    <asp:Label ID="lblDisNum" runat="server" CssClass="lbl-common-font"></asp:Label>
                    </td>
                <td class="td-blank6"></td>
            </tr>
            <tr>
                <td class="td-blank6"></td>
                <td class="td-blank4"></td>
                <td class="td-blank4"></td>
                <td colspan = "2" class="td-cash-calculate">
                    <asp:Label ID="Label20" runat="server" CssClass="lbl-common-font" Text="open deposit + sales(cash) + other(cash) - discount - bank deposit"></asp:Label>
                </td>
                <td class="td-reverse-cash-discount-number">
                    <asp:Label ID="Label33" runat="server" Text="Discount Amount" CssClass="lbl-common-font"></asp:Label>
                </td>
                <td class="td-reverse-cash-discount-number">
                    <asp:Label ID="lblDisAmount" runat="server" CssClass="lbl-common-font"></asp:Label>
                </td>
                <td class="td-blank6"></td>
            </tr>
            <tr>
                <td class="td-blank6"></td>
                <td class="td-blank4"></td>
                <td class="td-blank4"></td>
                <td class="td-blank4"></td>
                <td class="td-blank4"></td>
                <td class="td-blank4"></td>
                <td class="td-blank4"></td>
                <td class="td-blank6"></td>
            </tr>
                     
            <tr>
                <td class="td-blank6"></td>
                <td class="td-lbl-opening-bank_DP-sales-customer_ad-Number-ad">
                    <asp:Label ID="Label3" runat="server" CssClass="lbl-common-xlarge-font" Text="Bank Deposit"></asp:Label>
                </td>
                <td class="td-lbl-reverse-discount-number-deposite">
                    <asp:Label ID="lblDeposit" runat="server" CssClass="lbl-common-font"></asp:Label>
                </td>
                <td class="td-lbl-opening-bank_DP-sales-customer_ad-Number-ad">
                    <asp:Label ID="Label27" runat="server" CssClass="lbl-common-xlarge-font" Text="Sales"></asp:Label>
                </td>
                <td class="td-lbl_sml-cash">
                    <asp:Label ID="lblSales" runat="server" CssClass="lbl-common-font"></asp:Label>
                </td>
                <td class="td-blank4">&nbsp;</td>
                <td class="td-blank4"></td>
                <td class="td-blank6"></td>
            </tr>
            <tr>
                <td class="td-blank6"></td>
                <td class="td-blank4"></td>
                <td class="td-blank4"></td>
                <td class="td-cash-calculate2" colspan = "3">
                    <asp:Label ID="Label26" runat="server" Text="sales(cash) + sales(card)+IW+other+GST" CssClass="lbl-common-font"></asp:Label>
                </td>
                <td class="td-blank4"></td>
            </tr>
                   
            <tr>
                <td class="td-blank6"></td>
                <td class="td-lbl-opening-bank_DP-sales-customer_ad-Number-ad" >
                    <asp:Label ID="Label29" runat="server" CssClass="lbl-common-xlarge-font" Text="Customer Advance"></asp:Label>
                </td>
                <td class="td-lbl-reverse-discount-number-deposite">
                    <asp:Label ID="lblDepositCusto" runat="server" CssClass="lbl-common-font"></asp:Label>
                </td>
                <td colspan = "3">
                    <asp:Label ID="Label30" runat="server" CssClass="lbl-common-xlarge-font" Text="Number of Advance"></asp:Label>&nbsp;&nbsp;
                    <asp:Label ID="lblNumCusto" runat="server" CssClass="lbl-common-font"></asp:Label>
                </td>
                <td class="td-blank4"></td>
                <td class="td-blank6"></td>
            </tr>
            <tr>
                <td class="td-blank6"></td>
                <td class="td-blank4"></td>
                <td class="td-blank4"></td>
                <td class="td-blank4" colspan = "2">
                    &nbsp;</td>
                <td class="td-blank4"></td>
                <td class="td-blank4"></td>
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
