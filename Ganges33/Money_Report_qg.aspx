<%@ Page Language="vb" AutoEventWireup="false" MasterPageFile="~/Site4.Master" CodeBehind="Money_Report_qg.aspx.vb" Inherits="Ganges33.Money_Report_qg" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="ajaxToolkit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script src="http://ajax.googleapis.com/ajax/libs/jquery/1.7.0/jquery.min.js"></script>
    <link href="CSS/Common/Money_Rport_qg.css" rel="stylesheet" />
    <link type="text/css" href="http://ajax.googleapis.com/ajax/libs/jqueryui/1/themes/start/jquery-ui.css" rel="stylesheet">
    <script type="text/javascript" src="http://ajax.googleapis.com/ajax/libs/jqueryui/1/jquery-ui.min.js"></script>
        </asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:HiddenField ID="hidDate" runat="server" />
    <asp:HiddenField ID="hidOpeningDt" runat="server" />
    <asp:HiddenField ID="hidFullDiscAmt" runat="server" />
    <asp:HiddenField ID="hidFullDiscCnt" runat="server" />

     <div class="div-entire-tbl" style="background-image: url('pagewall_2/wall_money-2.png')">
       <div class="div-entire-tbl2">
        <table class="tbl-money-report">
            <tr>
                <td class="td-blank1"></td>
                <td class="td-blank2">
                </td>
                <td class="td-blank2">
                </td>
                <td class="td-blank2"></td>
                <td class="td-blank2"></td>
                <td class="td-blank2"></td>
                <td class="td-blank2"></td>
                <td class="td-blank2"></td>
            </tr>
            <tr>
                <td class="td-blank1"> </td>
                <td colspan = "5"> 
                    <asp:Label ID="lblDate" runat="server" Text="Record date&amp;time : " CssClass="lbl-font-common"></asp:Label>
                    <asp:Label ID="lblRecord" runat="server" CssClass="lbl-text-common"></asp:Label>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                    <asp:Label ID="lblName" runat="server" Text="user name: " CssClass="lbl-text-common"></asp:Label>
                    <asp:Label ID="lblYousername" runat="server" CssClass="lbl-text-common"></asp:Label>
                </td>
                <td class="td-btn-start-send" align = "right"> 
                    <asp:ImageButton ID="btnStart" runat="server"  ImageUrl="~/icon/start.png"   CssClass="btn-img-strat-send" />
                </td>
                <td class="td-blank2"> </td>
            </tr>
            <tr>
                <td class="td-blank1"></td>
                <td class="td-lbl-total-number-count">
                    <asp:Label ID="Label24" runat="server" CssClass="td-large-font" Text="Total Number"></asp:Label>
                </td>
                <td class="td-lbl-total-number-count">
                    <asp:Label ID="Label25" runat="server" CssClass="td-large-font" Text="Count :"></asp:Label>
                    <asp:Label ID="lblCount" runat="server" CssClass="lbl-font-common"></asp:Label>
                </td>
                <td class="td-blank2">&nbsp;</td>
                <td class="td-blank2"></td>
                <td class="td-blank2">
                    &nbsp;</td>
                <td class="td-blank2"></td>
                <td class="td-blank2"></td>
            </tr>
            <tr>
                <td class="td-blank1"></td>
                <td class="td-blank-grid"></td>
                <td class="td-cards-cnt-climp">
                    <asp:Label ID="Label10" runat="server" Text="IW" CssClass="lbl-font-common"></asp:Label>
                </td>
                <td class="td-cards-cnt-climp">
                    <asp:Label ID="Label11" runat="server" Text="OOW(cash)" CssClass="lbl-font-common"></asp:Label>
                </td>
                <td class="td-cards-cnt-climp">
                    <asp:Label ID="Label16" runat="server" Text="OOW(card)" CssClass="lbl-fpnt-common"></asp:Label>
                </td>
                <td class="td-cards-cnt-climp">
                    <asp:Label ID="Label13" runat="server" Text="Other(cash)" CssClass="lbl-font-common"></asp:Label>
                </td>
                <td class="td-cards-cnt-climp">
                    <asp:Label ID="Label15" runat="server" Text="Other(card)" CssClass="lbl-font-common"></asp:Label>
                </td>
                <td class="td-blank2"></td>
            </tr>
            <tr>
                <td class="td-blank1"></td>
                <td class="td-number-climp_taxin-amount_taxin">
                    <asp:Label ID="Label7" runat="server" Text="Number" CssClass="lbl-font-common"></asp:Label>
                </td>
                <td class="td-cards-cnt-climp">
                    <asp:Label ID="lblIWCnt" runat="server" CssClass="lbl-font-common"></asp:Label>
                </td>
                <td class="td-cards-cnt-climp">
                    <asp:Label ID="lblOOWCashCnt" runat="server" CssClass="lbl-font-common"></asp:Label>
                </td>
                <td class="td-cards-cnt-climp">
                    <asp:Label ID="lblOOWCardCnt" runat="server" CssClass="lbl-font-common"></asp:Label>
                </td>
                <td class="td-cards-cnt-climp">
                    <asp:Label ID="lblOtherCashCnt" runat="server" CssClass="lbl-font-common"></asp:Label>
                </td>
                <td class="td-cards-cnt-climp">
                    <asp:Label ID="lblOtherCardCnt" runat="server" CssClass="lbl-font-common"></asp:Label>
                </td>
                <td class="td-blank2"></td>
            </tr>
            <tr>
                <td class="td-blank1"></td>
                <td class="td-amount-climp-Notax">
                    <asp:Label ID="Label8" runat="server" Text="Amount(notax)" CssClass="lbl-font-common"></asp:Label>
                </td>
                <td class="td-cash-climp-NOtax">
                    <asp:Label ID="lblIWNoTax" runat="server" CssClass="lbl-font-common"></asp:Label>
                </td>
                <td class="td-cash-climp-NOtax">
                    <asp:Label ID="lblOWCashNoTax" runat="server" CssClass="lbl-font-common"></asp:Label>
                </td>
                <td class="td-cash-climp-NOtax">
                    <asp:Label ID="lblOWCardNoTax" runat="server" CssClass="lbl-font-common"></asp:Label>
                </td>
                <td class="td-cash-climp-NOtax">
                    <asp:Label ID="lblOtCashNoTax" runat="server" CssClass="lbl-font-common"></asp:Label>
                </td>
                <td class="td-cash-climp-NOtax">
                    <asp:Label ID="lblOtCardNoTax" runat="server" CssClass="lbl-font-common"></asp:Label>
                </td>
                <td class="td-blank2"></td>
            </tr>
            <tr>
                <td class="td-blank1"></td>
                <td class="td-number-climp_taxin-amount_taxin">
                    <asp:Label ID="Label9" runat="server" Text="Amoun(taxin)" CssClass="lbl-font-common"></asp:Label>
                </td>
                <td class="td-cards-cnt-climp">
                    <asp:Label ID="lblIWSum" runat="server" CssClass="lbl-font-common"></asp:Label>
                </td>
                <td class="td-cards-cnt-climp">
                    <asp:Label ID="lblOOWCashSum" runat="server" CssClass="lbl-font-common"></asp:Label>
                </td>
                <td class="td-cards-cnt-climp">
                    <asp:Label ID="lblOOWCardSum" runat="server" CssClass="lbl-font-common"></asp:Label>
                </td>
                <td class="td-cards-cnt-climp">
                    <asp:Label ID="lblOtherCashSum" runat="server" CssClass="lbl-font-common"></asp:Label>
                </td>
                <td class="td-cards-cnt-climp">
                    <asp:Label ID="lblOtherCardSum" runat="server" CssClass="lbl-font-common"></asp:Label>
                </td>
                <td class="td-blank2"></td>
            </tr>
            <tr>
                <td class="td-blank1"></td>
                <td class="td-amount-climp-Notax">
                    <asp:Label ID="Label1" runat="server" Text="Claim Amount(notax)" CssClass="lbl-font-common"></asp:Label>
                </td>
                <td class="td-cash-climp-NOtax">
                    <asp:Label ID="lblIWSumClaim" runat="server" CssClass="lbl-font-common"></asp:Label>
                </td>
                <td class="td-cash-climp-NOtax">
                    <asp:Label ID="lblOOWCashSumClaim" runat="server" CssClass="lbl-font-common"></asp:Label>
                </td>
                <td class="td-cash-climp-NOtax">
                    <asp:Label ID="lblOOWCardSumClaim" runat="server" CssClass="lbl-font-common"></asp:Label>
                </td>
                <td class="td-cash-climp-NOtax">
                    <asp:Label ID="lblOtherCashSumClaim" runat="server" CssClass="lbl-font-common"></asp:Label>
                </td>
                <td class="td-cash-climp-NOtax">
                    <asp:Label ID="lblOtherCardSumClaim" runat="server" CssClass="lbl-font-common"></asp:Label>
                </td>-
                <td class="td-blank2"></td>
            </tr>
            <tr>
                <td class="td-blank1"></td>
                <td class="td-number-climp_taxin-amount_taxin">
                    <asp:Label ID="Label2" runat="server" Text="Claim Amoun(taxin)" CssClass="lbl-font-common"></asp:Label>
                </td>
                <td class="td-cards-cnt-climp">
                    <asp:Label ID="lblIWNoTaxClaim" runat="server" CssClass="lbl-fontcommon"></asp:Label>
                </td>
                <td class="td-cards-cnt-climp">
                    <asp:Label ID="lblOWCashNoTaxClaim" runat="server" CssClass="lbl-font-common"></asp:Label>
                </td>
                <td class="td-cards-cnt-climp">
                    <asp:Label ID="lblOWCardNoTaxClaim" runat="server" CssClass="lbl-font-common"></asp:Label>
                </td>
                <td class="td-cards-cnt-climp">
                    <asp:Label ID="lblOtCashNoTaxClaim" runat="server" CssClass="lbl-font-common"></asp:Label>
                </td>
                <td class="td-cards-cnt-climp">
                    <asp:Label ID="lblOtCardNoTaxClaim" runat="server" CssClass="lbl-font-common"></asp:Label>
                </td>
                <td class="td-blank2"></td>
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
                <td class="td-cash-sales-total">
                    <asp:Label ID="Label27" runat="server" CssClass="td-xlarge-font" Text="Cash Total"></asp:Label>
                </td>
                <td class="td-lbl-cash-total">
                    <asp:Label ID="lblCashTotal" runat="server" CssClass="lbl-font-common"></asp:Label>
                </td>
                <td class="td-blank2">
                    </td>
                <td class=".td-lbl-cash-total {
                border-bottom: 1px solid black;
            }" colspan = "2">
                    <asp:Label ID="Label30" runat="server" Text="Bank Deposit" CssClass="lbl-font-common"></asp:Label>
                   
                    <asp:Label ID="lblDeposit" runat="server" CssClass="lbl-font-common"></asp:Label>
                </td>
                <td></td>
                <td class="td-blank2"></td>
            </tr>
            <tr>
                <td class="td-blank1"></td>
                <td colspan = "2">
                    <asp:Label ID="Label29" runat="server" CssClass="lbl-font-common" Text="open deposit + sales(cash) + other(cash) - discount - bank deposit"></asp:Label>
                </td>
                <td class="td-blank2"colspan = "2">
                    &nbsp;</td>
                <td class="td-blank2"></td>
                <td class="td-blank2"></td>
                <td class="td-blank2"></td>
            </tr>
            <tr>
                <td class="td-blank1"></td>
                <td class="td-cash-sales-total">
                    <asp:Label ID="Label28" runat="server" CssClass="td-xlarge-font" Text="Sales Total"></asp:Label>
                </td>
                <td class="td-lbl-discount-amount-sales_calculate">
                    <asp:Label ID="lblSales" runat="server" CssClass="lbl-font-common"></asp:Label>
                </td>
                <td class="td-blank2">&nbsp;</td>
                <td class="td-lbl-discount-amount-sales_calculate">
                    <asp:Label ID="Label32" runat="server" Text="Discount Number" CssClass="lbl-font-common"></asp:Label>
                </td>
                <td class="td-lbl-discount-amount-sales_calculate">
                    <asp:Label ID="lblDisNum" runat="server" CssClass="lbl-font-common"></asp:Label>
                </td>
                <td class="td-blank2"></td>
                <td class="td-blank2"></td>
            </tr>
            <tr>
                <td class="td-blank1"></td>
                <td class="td-lbl-discount-amount-sales_calculate"><asp:Label ID="Label26" runat="server" Text="sales(cash) + sales(card)+IW+other+GST" CssClass="lbl-fontcommon"></asp:Label></td>
                <td class="td-blank2"></td>
                <td class="td-blank2">
                </td>
                <td class="td-lbl-discount-amount-sales_calculate">
                    <asp:Label ID="Label33" runat="server" Text="Discount Amount" CssClass="lbl-font-common"></asp:Label>
                </td>
                <td class="td-lbl-discount-amount-sales_calculate">
                    <asp:Label ID="lblDisAmount" runat="server" CssClass="lbl-font-common"></asp:Label>
                </td>
                <td class="td-blank2"></td>
            </tr>
                        <tr>
                <td class="td-blank1"></td>
                <td class="td-blank2"></td>
                <td class="td-blank2"></td>
                <td class="td-blank2"></td>
                <td class="td-blank2"></td>
                <td class="td-blank2"></td>
                <td class="td-btn-start-send" align = "right"> 
                    <asp:ImageButton ID="btnSend" runat="server"  ImageUrl="~/icon/send.png" CssClass="btn-img-strat-send" />
                </td>
                <td class="td-blank2"></td>
            </tr>
            <tr>
                <td class="td-blank1"></td>
                <td class="td-blank2"></td>
                <td class="td-blank2"></td>
                <td class="td-blank2" colspan = "2">
                    </td>
                <td class="td-blank2"></td>
                <td class="td-blank2"></td>
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
