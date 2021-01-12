<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Site4.Master" CodeBehind="Money_Salse_show2.aspx.vb" Inherits="Ganges33.Money_Salse_show21" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script src="http://ajax.googleapis.com/ajax/libs/jquery/1.7.0/jquery.min.js"></script>
    <link href="CSS/Common/Money-sales-shows2.css" rel="stylesheet" />
    <link type="text/css" href="http://ajax.googleapis.com/ajax/libs/jqueryui/1/themes/start/jquery-ui.css" rel="stylesheet">
    <script type="text/javascript" src="http://ajax.googleapis.com/ajax/libs/jqueryui/1/jquery-ui.min.js"></script>
        </asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="div-entire-tbl" style="background-image: url('pagewall_2/wall_money-2.png')">
   <div class="div-money-sales-show2">
       <table class="tbl-money-sales-shows2">
           <tr>
               <td class="td-blank1"></td>
               <td class="td-lbl-location" colspan = "4">
                       <asp:Label ID="lblLocation" runat="server" CssClass="lbl-font-common"></asp:Label>
                   </td>
               <td class="td-blank1"></td>
               <td class="td-blank2"></td>
               <td align = "right" class="td-img-back">
                    <asp:ImageButton ID="btnBack" runat="server" Height="29px" ImageUrl="~/icon/back.png" Width="81px" />
               </td>
           </tr>
           <tr>
               <td class="td-blank1"></td>
               <td class="td-blank1"></td>
               <td class="td-blank1"></td>
               <td class="td-blank1"></td>
               <td class="td-blank1"></td>
               <td class="td-blank1"></td>
               <td class="td-blank2"></td>
               <td class="td-txtbox" rowspan = "12">
                   <asp:ListBox ID="List1Msg" runat="server" CssClass="txtbox-unbound"></asp:ListBox>
               </td>
           </tr>
           <tr>
               <td class="td-cash-card-number-amount-taxin-climp-amount-taxin"></td>
               <td class="td-cash-card-number-amount-taxin-climp-amount-taxin">
                       <asp:Label ID="Label3" runat="server" CssClass="lbl-font-common" Text="IW"></asp:Label>
                   </td>
               <td class="td-cash-card-number-amount-taxin-climp-amount-taxin">
                       <asp:Label ID="Label4" runat="server" CssClass="lbl-font-common" Text="OOW(cash)"></asp:Label>
                   </td>
               <td class="td-cash-card-number-amount-taxin-climp-amount-taxin">
                       <asp:Label ID="Label5" runat="server" CssClass="lbl-font-common" Text="OOW(card)"></asp:Label>
                   </td>
               <td class="td-cash-card-number-amount-taxin-climp-amount-taxin">
                       <asp:Label ID="Label6" runat="server" CssClass="lbl-font-common" Text="Other(cash)"></asp:Label>
                   </td>
               <td class="td-cash-card-number-amount-taxin-climp-amount-taxin">
                       <asp:Label ID="Label7" runat="server" CssClass="lbl-font-common" Text="Other(card)"></asp:Label>
                   </td>
               <td class="td-blank2"></td>
           </tr>
           <tr>
               <td class="td-cash-card-number-amount-taxin-climp-amount-taxin">
                       <asp:Label ID="Label8" runat="server" CssClass="lbl-font-common" Text="number"></asp:Label>
               </td>
               <td class="td-lbl-cnt-sum0-sum1">
                    <asp:Label ID="lblIWCnt" runat="server" CssClass="lbl-font-common"></asp:Label>
                   </td>
               <td class="td-lbl-cnt-sum0-sum1">
                    <asp:Label ID="lblOOWCashCnt" runat="server" CssClass="lbl-font-common"></asp:Label>
                   </td>
               <td class="td-lbl-cnt-sum0-sum1">
                    <asp:Label ID="lblOOWCardCnt" runat="server" CssClass="lbl-font-common"></asp:Label>
                   </td>
               <td class="td-lbl-cnt-sum0-sum1">
                    <asp:Label ID="lblOtherCashCnt" runat="server" CssClass="lbl-font-common"></asp:Label>
                   </td>
               <td class="td-lbl-cnt-sum0-sum1">
                    <asp:Label ID="lblOtherCardCnt" runat="server" CssClass="lbl-font-common"></asp:Label>
                   </td>
               <td class="td-blank2"></td>
           </tr>
           <tr>
               <td class="td-amount-notax-climpamount-notax">
                       <asp:Label ID="Label9" runat="server" CssClass="lbl-font-common" Text="amount(notax)"></asp:Label>
               </td>
               <td class="td-amount-notax-climpamount-notax">
                    <asp:Label ID="lblIWNoTax" runat="server" CssClass="lbl-font-common"></asp:Label>
               </td>
               <td class="td-amount-notax-climpamount-notax">
                    <asp:Label ID="lblOWCashNoTax" runat="server" CssClass="lbl-font-common"></asp:Label>
                   </td>
               <td class="td-amount-notax-climpamount-notax">
                    <asp:Label ID="lblOWCardNoTax" runat="server" CssClass="lbl-font-common"></asp:Label>
                   </td>
               <td class="td-amount-notax-climpamount-notax">
                    <asp:Label ID="lblOtCashNoTax" runat="server" CssClass="lbl-font-common"></asp:Label>
                   </td>
               <td class="td-amount-notax-climpamount-notax">
                    <asp:Label ID="lblOtCardNoTax" runat="server" CssClass="lbl-font-common"></asp:Label>
                   </td>
               <td class="td-blank2"></td>
           </tr>
           <tr>
               <td class="td-cash-card-number-amount-taxin-climp-amount-taxin">
                       <asp:Label ID="Label10" runat="server" CssClass="lbl-font-common" Text="amount(taxin)"></asp:Label>
               </td>
               <td class="td-lbl-cnt-sum0-sum1">
                    <asp:Label ID="lblIWSum0" runat="server" CssClass="lbl-font-common"></asp:Label>
                   </td>
               <td class="td-lbl-cnt-sum0-sum1">
                    <asp:Label ID="lblOOWCashSum0" runat="server" CssClass="lbl-font-common"></asp:Label>
                   </td>
               <td class="td-lbl-cnt-sum0-sum1">
                    <asp:Label ID="lblOOWCardSum0" runat="server" CssClass="lbl-font-common"></asp:Label>
                   </td>
               <td class="td-lbl-cnt-sum0-sum1">
                    <asp:Label ID="lblOtherCashSum0" runat="server" CssClass="lbl-font-common"></asp:Label>
                   </td>
               <td class="td-lbl-cnt-sum0-sum1">
                    <asp:Label ID="lblOtherCardSum0" runat="server" CssClass="lbl-font-common"></asp:Label>
                   </td>
               <td class="td-blank2"></td>
           </tr>
           <tr>
               <td class="td-amount-notax-climpamount-notax">
                    <asp:Label ID="Label28" runat="server" Text="claim amount(notax)" CssClass="lbl-font-common"></asp:Label>
                </td>
               <td class="td-amount-notax-climpamount-notax">
                    <asp:Label ID="lblIWNoTax0" runat="server" CssClass="lbl-font-common"></asp:Label>
                   </td>
               <td class="td-amount-notax-climpamount-notax">
                    <asp:Label ID="lblOWCashNoTax0" runat="server" CssClass="lbl-font-common"></asp:Label>
                   </td>
               <td class="td-amount-notax-climpamount-notax">
                    <asp:Label ID="lblOWCardNoTax0" runat="server" CssClass="lbl-font-common"></asp:Label>
                   </td>
               <td class="td-amount-notax-climpamount-notax">
                    <asp:Label ID="lblOtCashNoTax0" runat="server" CssClass="lbl-font-common"></asp:Label>
                   </td>
               <td class="td-amount-notax-climpamount-notax">
                    <asp:Label ID="lblOtCardNoTax0" runat="server" CssClass="lbl-font-common"></asp:Label>
                   </td>
               <td class="td-blank2"></td>
           </tr>
           <tr>
               <td class="td-cash-card-number-amount-taxin-climp-amount-taxin">
                    <asp:Label ID="Label29" runat="server" Text="claim amount(taxin)" CssClass="lbl-font-common"></asp:Label>
                </td>
               <td class="td-lbl-cnt-sum0-sum1">
                    <asp:Label ID="lblIWSum1" runat="server" CssClass="lbl-font-common"></asp:Label>
                   </td>
               <td class="td-lbl-cnt-sum0-sum1">
                    <asp:Label ID="lblOOWCashSum1" runat="server" CssClass="lbl-font-common"></asp:Label>
                   </td>
               <td class="td-lbl-cnt-sum0-sum1">
                    <asp:Label ID="lblOOWCardSum1" runat="server" CssClass="lbl-font-common"></asp:Label>
                   </td>
               <td class="td-lbl-cnt-sum0-sum1">
                    <asp:Label ID="lblOtherCashSum1" runat="server" CssClass="lbl-font-common"></asp:Label>
                   </td>
               <td class="td-lbl-cnt-sum0-sum1">
                    <asp:Label ID="lblOtherCardSum1" runat="server" CssClass="lbl-font-common"></asp:Label>
                   </td>
               <td class="td-blank2"></td>
           </tr>
           <tr>
               <td class="td-blank1"></td>
               <td class="td-blank1"></td>
               <td class="td-blank1"></td>
               <td class="td-blank1"></td>
               <td class="td-blank1"></td>
               <td class="td-blank1"></td>
               <td class="td-blank2"></td>
           </tr>
           <tr>
               <td class="td-lbl-cash-sales-deposit">
                       <asp:Label ID="Label11" runat="server" CssClass="lbl-font-common" Text="cash total"></asp:Label>
                   </td>
               <td class="td-lbl-cash-sales-deposit">
                    <asp:Label ID="lblCashTotal" runat="server" CssClass="lbl-font-common"></asp:Label>
                    </td>
               <td class="td-blank1"></td>
               <td class="td-lbl-cash-sales-deposit">
                       <asp:Label ID="Label12" runat="server" CssClass="lbl-font-common" Text="sales total"></asp:Label>
                   </td>
               <td class="td-lbl-cash-sales-deposit">
                    <asp:Label ID="lblSales" runat="server" CssClass="lbl-font-common"></asp:Label>
                   </td>
               <td class="td-blank1"></td>
               <td class="td-blank1"></td>
           </tr>
           <tr>
               <td class="td-blank1"></td>
               <td class="td-blank1"></td>
               <td class="td-blank1"></td>
               <td class="td-lbl-cash-sales-deposit">
                       <asp:Label ID="Label13" runat="server" CssClass="lbl-font-common" Text="Bank Deposit"></asp:Label>
                   </td>
               <td class="td-lbl-cash-sales-deposit">
                    <asp:Label ID="lblDeposit" runat="server" CssClass="lbl-font-common"></asp:Label>
                   </td>
               <td class="td-blank1"></td>
               <td class="td-blank1"></td>
           </tr>
           <tr>
               <td class="td-blank1"></td>
               <td class="td-blank1"></td>
               <td class="td-blank1"></td>
               <td class="td-blank1"></td>
               <td class="td-blank1"></td>
               <td class="td-blank1"></td>
               <td class="td-blank1"></td>
           </tr>
           <tr>
               <td class="td-blank1" >
               </td >
                       <asp:Label ID="Label16" runat="server" CssClass="lbl-font-common" Text="customer deposit"></asp:Label>
               <td class="td-lbl-cash-sales-deposit">
                       <asp:Label ID="lblDepositCusto" runat="server" CssClass="lbl-font-common"></asp:Label>
               </td>
               <td class="td-blank1">
               </td>
               <td colspan = "2" class="td-lbl-custody">
                   <asp:Label ID="Label17" runat="server" CssClass="lbl-font-common" Text="Number of Custody"></asp:Label>&nbsp;&nbsp;
                   <asp:Label ID="lblNumCusto" runat="server" CssClass="lbl-font-common"></asp:Label> 
               </td>
               <td class="td-blank1"></td>
               <td class="td-blank2"></td>
           </tr>
           <tr>
               <td class="td-blank1">&nbsp;</td>
               <td class="td-blank1">&nbsp;</td>
               <td class="td-blank1">&nbsp;</td>
               <td class="td-blank1">&nbsp;</td>
               <td class="td-blank1">&nbsp;</td>
               <td class="td-blank1">&nbsp;</td>
               <td class="td-blank1">&nbsp;</td>
               <td></td>
           </tr>
           <tr>
               <td class="td-blank1">&nbsp;</td>
               <td class="td-blank1">&nbsp;</td>
               <td class="td-blank1">&nbsp;</td>
               <td class="td-blank1">&nbsp;</td>
               <td class="td-blank1">&nbsp;</td>
               <td class="td-blank1">&nbsp;</td>
               <td class="td-blank1">&nbsp;</td>
               <td></td>
           </tr>
       </table>
   </div>
  </div>

     <div id="dialog" title="message" style="display:none;"> >
        <asp:Label ID="lblMsg" runat="server" Text=""></asp:Label>
     </div>

</asp:Content>
