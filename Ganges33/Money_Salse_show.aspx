<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Site4.Master" CodeBehind="Money_Salse_show.aspx.vb" Inherits="Ganges33.Money_Salse_show1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
        <script src="http://ajax.googleapis.com/ajax/libs/jquery/1.7.0/jquery.min.js"></script>
    <link href="CSS/Common/money-salse-show.css" rel="stylesheet" />
    <link type="text/css" href="http://ajax.googleapis.com/ajax/libs/jqueryui/1/themes/start/jquery-ui.css" rel="stylesheet">
    <script type="text/javascript" src="http://ajax.googleapis.com/ajax/libs/jqueryui/1/jquery-ui.min.js"></script>
      </asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
     <div class="div-entire-tbl" style="background-image: url('pagewall_2/wall_money-2.png')">
       <div class="tiv-money-sales">
           <table class="tbl-money-sales">
               <tr>
                   <td class="td-blank1"></td>
                   <td class="td-blank2"></td>
                   <td colspan = "4" class="td-lbl-result-detail">
                       <asp:Label ID="Label1" runat="server" CssClass="lbl-font-xxlarge" Text="Result"></asp:Label>
                   </td>
                   <td class="td-blank2">
                      </td>
                   <td class="td-blank3">
                       </td>
                   <td class="td-blank2"></td>
               </tr>
               <tr>
                   <td class="td-blank4"></td>
                   <td class="td-blank2"></td>
                   <td class="td-blank5"></td>
                   <td class="td-blank5"></td>
                   <td class="td-blank5"></td>
                   <td class="td-blank5"></td>
                   <td class="td-blank5"></td>
                   <td class="td-blank3"></td>
                   <td class="td-blank3"></td>
               </tr>
               <tr>
                   <td class="td-blank4"></td>
                   <td class="td-cash-card-number-amount-taxin-climp-amount-taxin "></td>
                   <td class="td-cash-card-number-amount-taxin-climp-amount-taxin ">
                       <asp:Label ID="Label3" runat="server" CssClass="lbl-font-common" Text="IW"></asp:Label>
                   </td>
                   <td class="td-cash-card-number-amount-taxin-climp-amount-taxin ">
                       <asp:Label ID="Label4" runat="server" CssClass="lbl-font-common" Text="OOW(cash)"></asp:Label>
                   </td>
                   <td class="td-cash-card-number-amount-taxin-climp-amount-taxin ">
                       <asp:Label ID="Label5" runat="server" CssClass="lbl-font-common" Text="OOW(card)"></asp:Label>
                   </td>
                   <td class="td-cash-card-number-amount-taxin-climp-amount-taxin ">
                       <asp:Label ID="Label6" runat="server" CssClass="lbl-font-common" Text="Other(cash)"></asp:Label>
                   </td>
                   <td class="td-cash-card-number-amount-taxin-climp-amount-taxin ">
                       <asp:Label ID="Label7" runat="server" CssClass="lbl-font-common" Text="Other(card)"></asp:Label>
                   </td>
                   <td class="td-blank3"></td>
                   <td class="td-blank3"></td>
               </tr>
               <tr>
                   <td class="td-blank4"></td>
                   <td class="td-cash-card-number-amount-taxin-climp-amount-taxin ">
                       <asp:Label ID="Label8" runat="server" CssClass="lbl-font-common" Text="number"></asp:Label>
                   </td>
                   <td class="td-lbl-cnt-sum-climp">
                    <asp:Label ID="lblIWCnt" runat="server" CssClass="lbl-font-common"></asp:Label>
                   </td>
                   <td class="td-lbl-cnt-sum-climp">
                    <asp:Label ID="lblOOWCashCnt" runat="server" CssClass="lbl-font-common"></asp:Label>
                   </td>
                   <td class="td-lbl-cnt-sum-climp">
                    <asp:Label ID="lblOOWCardCnt" runat="server" CssClass="lbl-font-common"></asp:Label>
                   </td>
                   <td class="td-lbl-cnt-sum-climp">
                    <asp:Label ID="lblOtherCashCnt" runat="server" CssClass="lbl-font-common"></asp:Label>
                   </td>
                   <td class="td-lbl-cnt-sum-climp">
                    <asp:Label ID="lblOtherCardCnt" runat="server" CssClass="lbl-font-common"></asp:Label>
                   </td>
                   <td class="td-blank4"></td>
                   <td class="td-blank4"></td>
               </tr>
               <tr>
                   <td class="td-blank4"></td>
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
                   <td class="td-blank3"></td>
                   <td class="td-blank3"></td>
               </tr>
               <tr>
                   <td class="td-blank4"></td>
                   <td class="td-cash-card-number-amount-taxin-climp-amount-taxin ">
                       <asp:Label ID="Label10" runat="server" CssClass="lbl-font-common" Text="amount(taxin)"></asp:Label>
                   </td>
                   <td class="td-lbl-cnt-sum-climp">
                    <asp:Label ID="lblIWSum0" runat="server" CssClass="lbl-font-common"></asp:Label>
                   </td>
                   <td class="td-lbl-cnt-sum-climp">
                    <asp:Label ID="lblOOWCashSum0" runat="server" CssClass="lbl-font-common"></asp:Label>
                   </td>
                   <td class="td-lbl-cnt-sum-climp">
                    <asp:Label ID="lblOOWCardSum0" runat="server" CssClass="lbl-font-common"></asp:Label>
                   </td>
                   <td class="td-lbl-cnt-sum-climp">
                    <asp:Label ID="lblOtherCashSum0" runat="server" CssClass="lbl-font-common"></asp:Label>
                   </td>
                   <td class="td-lbl-cnt-sum-climp">
                    <asp:Label ID="lblOtherCardSum0" runat="server" CssClass="lbl-font-common"></asp:Label>
                   </td>
                   <td class="td-blank6"></td>
                   <td class="td-blank6"></td>
               </tr>
               <tr>
                   <td class="td-blank4"></td>
                   <td class="td-amount-notax-climpamount-notax">
                    <asp:Label ID="Label76" runat="server" Text="claim amount(notax)" CssClass="lbl-font-common"></asp:Label>
                   </td>
                   <td class="td-amount-notax-climpamount-notax">
                    <asp:Label ID="lblIWNoTaxCla" runat="server" CssClass="lbl-font-common"></asp:Label>
                   </td>
                   <td class="td-amount-notax-climpamount-notax">
                    <asp:Label ID="lblOWCashNoTaxCla" runat="server" CssClass="lbl-font-common"></asp:Label>
                   </td>
                   <td class="td-amount-notax-climpamount-notax">
                    <asp:Label ID="lblOWCardNoTaxCla" runat="server" CssClass="lbl-font-common"></asp:Label>
                   </td>
                   <td class="td-amount-notax-climpamount-notax">
                    <asp:Label ID="lblOtCashNoTaxCla" runat="server" CssClass="lbl-font-common"></asp:Label>
                   </td>
                   <td class="td-amount-notax-climpamount-notax">
                    <asp:Label ID="lblOtCardNoTaxCla" runat="server" CssClass="lbl-font-common"></asp:Label>
                   </td>
                   <td class="td-blank3"></td>
                   <td class="td-blank3"></td>
               </tr>
               <tr>
                   <td class="td-blank4"></td>
                   <td class="td-cash-card-number-amount-taxin-climp-amount-taxin ">
                    <asp:Label ID="Label77" runat="server" Text="claim amount(taxin)" CssClass="lbl-font-common"></asp:Label>
                   </td>
                   <td class="td-lbl-cnt-sum-climp">
                    <asp:Label ID="lblIWClaim" runat="server" CssClass="lbl-font-common"></asp:Label>
                   </td>
                   <td class="td-lbl-cnt-sum-climp">
                    <asp:Label ID="lblOWCashClaim" runat="server" CssClass="lbl-font-common"></asp:Label>
                   </td>
                   <td class="td-lbl-cnt-sum-climp">
                    <asp:Label ID="lblOWCardClaim" runat="server" CssClass="lbl-font-common"></asp:Label>
                   </td>
                   <td class="td-lbl-cnt-sum-climp">
                    <asp:Label ID="lblOtCashClaim" runat="server" CssClass="lbl-font-common"></asp:Label>
                   </td>
                   <td class="td-lbl-cnt-sum-climp">
                    <asp:Label ID="lblOtCardClaim" runat="server" CssClass="lbl-font-common"></asp:Label>
                   </td>
                   <td class="td-blank4"></td>
                   <td class="td-blank4"></td>
               </tr>
               <tr>
                   <td class="td-blank2"></td>
                   <td class="td-blank4"></td>
                   <td class="td-blank4"></td>
                   <td class="td-blank4"></td>
                   <td class="td-blank4"></td>
                   <td class="td-blank4"></td>
                   <td class="td-blank4"></td>
                   <td class="td-blank3"></td>
                   <td class="td-blank3"></td>
               </tr>
               <tr>
                   <td class="td-balnk4"></td>
                   <td class="td-lbl-cash-sales-deposit">
                       <asp:Label ID="Label11" runat="server" CssClass="lbl-font-common" Text="cash total"></asp:Label>
                   </td>
                   <td class="td-lbl-cash-sales-deposit">
                    <asp:Label ID="lblCashTotal" runat="server" CssClass="lbl-font-common"></asp:Label>
                    </td>
               
                  
                   <td class="td-blank6"></td>
                   <td class="td-lbl-cash-sales-deposit">
                       <asp:Label ID="Label12" runat="server" CssClass="lbl-font-common" Text="sales total"></asp:Label>
                   </td>
                   <td class="td-lbl-cash-sales-deposit">
                    <asp:Label ID="lblSales" runat="server" CssClass="lbl-font-common"></asp:Label>
                   </td>
               
                   <td class="td-blank4"></td>
                   <td class="td-blank6"></td>
                   <td class="td-blank6"></td>
               </tr>
               <tr>
                >
                   <td class="td-blank4"></td>
                   <td class="td-blank4"></td>
                   <td class="td-blank6"></td>
                   <td class="td-blank6"></td>
                   <td class="td-lbl-cash-sales-deposit">
                       <asp:Label ID="Label13" runat="server" CssClass="lbl-font-common" Text="Bank Deposit"></asp:Label>
                   </td>
                   <td class="td-lbl-cash-sales-deposit">
                       <asp:Label ID="lblDeposit" runat="server" CssClass="lbl-font-common"></asp:Label>
                   </td>
                   <td class="td-blank4"></td>
                   <td colspan = "2" class="td-btn-back" align = "right">
                    <asp:ImageButton ID="btnBack" runat="server" CssClass="btn-back" ImageUrl="~/icon/back.png" />
                   </td>
               </tr>
               <tr>
                
                   <td class="td-blank6"></td>
                   <td class="td-lbl-cash-sales-deposit">
                       <asp:Label ID="Label82" runat="server" CssClass="lbl-font-common" Text="customer deposit"></asp:Label></td>
                   <td class="td-lbl-cash-sales-deposit">
                       <asp:Label ID="lblDepositCusto" runat="server" CssClass="lbl-font-common"></asp:Label>
                   </td>
                
                   <td class="td-blank4"></td>
                   <td class="td-blank4"></td>
                   <td class="td-blank4"></td>
                   <td class="td-blank4"></td>
                   <td class="td-blank6"></td>
                   <td class="td-blank6"></td>
               <tr>
                  
                   <td class="td-blank6"></td>
                   <td class="td-lbl-cash-sales-deposit">
                       <asp:Label ID="Label84" runat="server" CssClass="lbl-font-common" Text="Number of Custody"></asp:Label></td>
                   <td class="td-lbl-cash-sales-deposit">
                       <asp:Label ID="lblNumCusto" runat="server" CssClass="lbl-font-common"></asp:Label>
                   </td>
               
                   <td class="td-blank4"></td>
                   <td class="td-blank4"></td>
                   <td class="td-blank4"></td>
                   <td class="td-blank4"></td>
                   <td class="td-blank6"></td>
                   <td class="td-blank6"></td>
               </tr>
               <tr>
              
                   <td class="td-blank6"></td>
                   <td class="td-blank6"></td>
                   <td class="td-lbl-result-detail" colspan = "4">
                       <asp:Label ID="Label14" runat="server" CssClass="lbl-font-xxlarge" Text="Details"></asp:Label>
                   </td>
                   <td class="td-blank5"></td>
                   <td class="td-btn-next">
                       <asp:Button ID="btnNextPage" runat="server" BackColor="#FF9966"  Text="next page" CssClass="btn-next-start" />
                   </td>
                   <td class="td-btn-next">
                       <asp:Button ID="BtnStartPage" runat="server" BackColor="#FF9966"  Text="start page" CssClass="btn-next-start" />
                   </td>
               </tr>
               <tr>
                 <td class="td-blank4"></td>
                   <td class="td-blank4"></td>
                   <td class="td-blank4"></td>
                   <td class="td-blank4"></td>
                   <td class="td-blank4"></td>
                   <td class="td-blank6"></td>
                   <td class="td-blank6"></td>
               </tr>
           </table>
           <table class="tbl-tables">
               <tr>
                   <td class="td-lbl-cash-sales-deposit">
                       <asp:Label ID="lblLocation1" runat="server" CssClass="lbl-font-common"></asp:Label>
                   </td>
                   <td colspan = "4" class="td-blank2">
                       &nbsp;</td>
                
                  
                   <td class="td-blank4"></td>
                   <td class="td-blank6"></td>
                   <td class="td-blank6"></td>
               </tr>
               <tr>
                   <td class="td-blank4"></td>
                   <td class="td-blank4"></td>
                   <td class="td-blank4"></td>
                   <td class="td-blank4"></td>
                   <td class="td-blank4"></td>
                   <td class="td-blank6"></td>
                   <td class="td-blank6"></td>
                   <td class="txtbox-unbound" rowspan = "12">
                       <asp:ListBox ID="List1Msg" runat="server" Height="297px" Width="297px"></asp:ListBox>
                   </td>
               </tr>
               <tr>
                   <td class="td-cash-card-number-amount-taxin-climp-amount-taxin "></td>
                   <td class="td-cash-card-number-amount-taxin-climp-amount-taxin ">
                       <asp:Label ID="Label18" runat="server" CssClass="lbl-font-common" Text="IW"></asp:Label>
                   </td>
                   <td class="td-cash-card-number-amount-taxin-climp-amount-taxin ">
                       <asp:Label ID="Label19" runat="server" CssClass="lbl-font-common" Text="OOW(cash)"></asp:Label>
                   </td>
                   <td class="td-cash-card-number-amount-taxin-climp-amount-taxin ">
                       <asp:Label ID="Label20" runat="server" CssClass="lbl-font-common" Text="OOW(card)"></asp:Label>
                   </td>
                   <td class="td-cash-card-number-amount-taxin-climp-amount-taxin ">
                       <asp:Label ID="Label21" runat="server" CssClass="lbl-font-common" Text="Other(cash)"></asp:Label>
                   </td>
                   <td class="td-cash-card-number-amount-taxin-climp-amount-taxin ">
                       <asp:Label ID="Label22" runat="server" CssClass="lbl-font-common" Text="Other(card)"></asp:Label>
                   </td>
                   <td class="td-blank6"></td>
               </tr>
               <tr>
                   <td class="td-cash-card-number-amount-taxin-climp-amount-taxin">
                       <asp:Label ID="Label15" runat="server" CssClass="lbl-font-common" Text="number"></asp:Label>
                   </td>
                   <td class="td-lbl-cnt-sum-climp">
                    <asp:Label ID="lblIWCnt1" runat="server" CssClass="lbl-font-common"></asp:Label>
                   </td>
                   <td class="td-lbl-cnt-sum-climp">
                    <asp:Label ID="lblOOWCashCnt1" runat="server" CssClass="lbl-font-common"></asp:Label>
                   </td>
                   <td class="td-lbl-cnt-sum-climp">
                    <asp:Label ID="lblOOWCardCnt1" runat="server" CssClass="lbl-font-common"></asp:Label>
                   </td>
                   <td class="td-lbl-cnt-sum-climp">
                    <asp:Label ID="lblOtherCashCnt1" runat="server" CssClass="lbl-font-common"></asp:Label>
                   </td>
                   <td class="td-lbl-cnt-sum-climp">
                    <asp:Label ID="lblOtherCardCnt1" runat="server" CssClass="lbl-font-common"></asp:Label>
                   </td>
                   <td class="td-blank4"></td>
               </tr>
               <tr>
                   <td class="td-amount-notax-climpamount-notax">
                       <asp:Label ID="Label16" runat="server" CssClass="lbl-font-common" Text="amount(notax)"></asp:Label>
                   </td>
                   <td class="td-amount-notax-climpamount-notax">
                    <asp:Label ID="lblIWNoTax1" runat="server" CssClass="lbl-font-common"></asp:Label>
                   </td>
                   <td class="td-amount-notax-climpamount-notax">
                    <asp:Label ID="lblOWCashNoTax1" runat="server" CssClass="lbl-font-common"></asp:Label>
                   </td>
                   <td class="td-amount-notax-climpamount-notax">
                    <asp:Label ID="lblOWCardNoTax1" runat="server" CssClass="lbl-font-common"></asp:Label>
                   </td>
                   <td class="td-amount-notax-climpamount-notax">
                    <asp:Label ID="lblOtCashNoTax1" runat="server" CssClass="lbl-font-common"></asp:Label>
                   </td>
                   <td class="td-amount-notax-climpamount-notax">
                    <asp:Label ID="lblOtCardNoTax1" runat="server" CssClass="lbl-font-common"></asp:Label>
                   </td>
                   <td class="td-blank6"></td>
                  </tr>
               <tr>
                   <td class="td-cash-card-number-amount-taxin-climp-amount-taxin">
                       <asp:Label ID="Label17" runat="server" CssClass="lbl-font-common" Text="amount(taxin)"></asp:Label>
                   </td>
                   <td class="td-lbl-cnt-sum-climp">
                    <asp:Label ID="lblIWSum1" runat="server" CssClass="lbl-font-common"></asp:Label>
                   </td>
                   <td class="td-lbl-cnt-sum-climp">
                    <asp:Label ID="lblOOWCashSum1" runat="server" CssClass="lbl-font-common"></asp:Label>
                   </td>
                   <td class="td-lbl-cnt-sum-climp">
                    <asp:Label ID="lblOOWCardSum1" runat="server" CssClass="lbl-font-common"></asp:Label>
                   </td>
                   <td class="td-lbl-cnt-sum-climp">
                    <asp:Label ID="lblOtherCashSum1" runat="server" CssClass="lbl-font-common"></asp:Label>
                   </td>
                   <td class="td-lbl-cnt-sum-climp">
                    <asp:Label ID="lblOtherCardSum1" runat="server" CssClass="lbl-font-common"></asp:Label>
                   </td>
                   <td class="td-blank4"></td>
               </tr>
               <tr>
                   <td class="td-amount-notax-climpamount-notax">
                    <asp:Label ID="Label28" runat="server" Text="claim amount(notax)" CssClass="lbl-font-common"></asp:Label>
                   </td>
                   <td class="td-amount-notax-climpamount-notax">
                    <asp:Label ID="lblIWNoTaxCla1" runat="server" CssClass="lbl-font-common"></asp:Label>
                   </td>
                   <td class="td-amount-notax-climpamount-notax">
                    <asp:Label ID="lblOWCashNoTaxCla1" runat="server" CssClass="lbl-font-common"></asp:Label>
                   </td>
                   <td class="td-amount-notax-climpamount-notax">
                    <asp:Label ID="lblOWCardNoTaxCla1" runat="server" CssClass="lbl-font-common"></asp:Label>
                   </td>
                   <td class="td-amount-notax-climpamount-notax">
                    <asp:Label ID="lblOtCashNoTaxCla1" runat="server" CssClass="lbl-font-common"></asp:Label>
                   </td>
                   <td class="td-amount-notax-climpamount-notax">
                    <asp:Label ID="lblOtCardNoTaxCla1" runat="server" CssClass="lbl-font-common"></asp:Label>
                   </td>
                   <td class="td-blank6"></td>
                 </tr>
               <tr>
                   <td class="td-cash-card-number-amount-taxin-climp-amount-taxin">
                    <asp:Label ID="Label29" runat="server" Text="claim amount(taxin)" CssClass="lbl-font-common"></asp:Label>
                   </td>
                   <td class="td-lbl-cnt-sum-climp">
                    <asp:Label ID="lblIWClaim1" runat="server" CssClass="lbl-font-common"></asp:Label>
                   </td>
                   <td class="td-lbl-cnt-sum-climp">
                    <asp:Label ID="lblOWCashClaim1" runat="server" CssClass="lbl-font-common"></asp:Label>
                   </td>
                   <td class="td-lbl-cnt-sum-climp">
                    <asp:Label ID="lblOWCardClaim1" runat="server" CssClass="lbl-font-common"></asp:Label>
                   </td>
                   <td class="td-lbl-cnt-sum-climp">
                    <asp:Label ID="lblOtCashClaim1" runat="server" CssClass="lbl-font-common"></asp:Label>
                   </td>
                   <td class="td-lbl-cnt-sum-climp">
                    <asp:Label ID="lblOtCardClaim1" runat="server" CssClass="lbl-font-common"></asp:Label>
                   </td>
                   <td class="td-blank4"></td>
                  </tr>
               <tr>
                   <td class="td-blank6"></td>
                   <td class="td-blank6"></td>
                   <td class="td-blank6"></td>
                   <td class="td-blank6"></td>
                   <td class="td-blank6"></td>
                   <td class="td-blank4"></td>
                   <td class="td-blank4"></td>
                </tr>
               <tr>
                   <td class="td-lbl-cash-sales-deposit">
                       <asp:Label ID="Label23" runat="server" CssClass="lbl-font-common" Text="cash total"></asp:Label>
                   </td>
                   <td class="td-lbl-cash-sales-deposit">
                    <asp:Label ID="lblCashTotal1" runat="server" CssClass="lbl-font-common"></asp:Label>
                    </td>
                  
                   <td class="td-blank6"></td>
                   <td class="td-lbl-cash-sales-deposit">
                       <asp:Label ID="Label24" runat="server" CssClass="lbl-font-common" Text="sales total"></asp:Label>
                   </td>
                   <td class="td-lbl-cash-sales-deposit">
                    <asp:Label ID="lblSales1" runat="server" CssClass="lbl-font-common"></asp:Label>
                   </td>
                 
                   <td class="td-blank6"></td>
                   <td class="td-blank6"></td>
                  </tr>
               <tr>
                   <td class="td-lbl-cash-sales-deposit">
                       <asp:Label ID="Label85" runat="server" CssClass="lbl-font-common" Text="customer deposit"></asp:Label></td>
                   <td class="td-lbl-cash-sales-deposit">
                       <asp:Label ID="lblDepositCusto1" runat="server" CssClass="lbl-font-common"></asp:Label>
                   </td>
                   <td class="td-blank6"></td>
                   <td colspan = "2" class="td-lbl-custody">
                       <asp:Label ID="Label86" runat="server" CssClass="lbl-font-common" Text="Number of Custody"></asp:Label>&nbsp;&nbsp;
                       <asp:Label ID="lblNumCusto1" runat="server" CssClass="lbl-font-common"></asp:Label>
                   </td>
              
                   <td class="td-blank6"></td>
                   <td class="td-blank6"></td>
                 </tr>
               <tr>
                   <td class="td-lbl-cash-sales-deposit">
                       <asp:Label ID="Label56" runat="server" CssClass="lbl-font-common" Text="Bank Deposit"></asp:Label></td>
                   <td class="td-lbl-cash-sales-deposit">
                       <asp:Label ID="lblDeposit1" runat="server" CssClass="lbl-font-common"></asp:Label>
                   </td>
            <td class="td-blank4"></td>
               
                   <td class="td-blank4"></td>
                   <td class="td-blank4"></td>
                   <td class="td-blank4"></td>
                   <td class="td-blank6"></td>
                   <td class="td-blank6"></td>
               </tr>
               <tr>
                   <td class="td-blank4"></td>
                   <td class="td-blank4"></td>
                   <td class="td-blank4"></td>
                   <td class="td-blank4"></td>
                   <td class="td-blank4"></td>
                   <td class="td-blank6"></td>
                   <td class="td-blank6"></td>
                </tr>
           </table>
           <table class="tbl-tables">
               <tr>
                   <td class="td-lbl-cash-sales-deposit">
                       <asp:Label ID="lblLocation2" runat="server" CssClass="lbl-font-common"></asp:Label>
                   </td>
                   <td colspan = "4" class="td-blank2">
                       &nbsp;</td>
                   
                   <td class="td-blank4"></td>
                   <td class="td-blank6"></td>
                   <td class="td-blank6"></td>
               </tr>
               <tr>
                   <td class="td-blank4"></td>
                   <td class="td-blank4"></td>
                   <td class="td-blank4"></td>
                   <td class="td-blank4"></td>
                   <td class="td-blank4"></td>
                   <td class="td-blank6"></td>
                   <td class="td-blank6"></td>
                   <td class="txtbox-unbound" rowspan = "12">
                       <asp:ListBox ID="List2Msg" runat="server" Height="297px" Width="297px"></asp:ListBox>
                   </td>
               </tr>
               <tr>
                   <td class="td-cash-card-number-amount-taxin-climp-amount-taxin"></td>
                   <td class="td-cash-card-number-amount-taxin-climp-amount-taxin">
                       <asp:Label ID="Label25" runat="server" CssClass="lbl-font-common" Text="IW"></asp:Label>
                   </td>
                   <td class="td-cash-card-number-amount-taxin-climp-amount-taxin">
                       <asp:Label ID="Label26" runat="server" CssClass="lbl-font-common" Text="OOW(cash)"></asp:Label>
                   </td>
                   <td class="td-cash-card-number-amount-taxin-climp-amount-taxin">
                       <asp:Label ID="Label27" runat="server" CssClass="lbl-font-common" Text="OOW(card)"></asp:Label>
                   </td>
                   <td class="td-cash-card-number-amount-taxin-climp-amount-taxin">
                       <asp:Label ID="Label30" runat="server" CssClass="lbl-font-common" Text="Other(cash)"></asp:Label>
                   </td>
                   <td class="td-cash-card-number-amount-taxin-climp-amount-taxin">
                       <asp:Label ID="Label31" runat="server" CssClass="lbl-font-common" Text="Other(card)"></asp:Label>
                   </td>
                   <td class="td-blank6"></td>
               </tr>
               <tr>
                   <td class="td-cash-card-number-amount-taxin-climp-amount-taxin">
                       <asp:Label ID="Label32" runat="server" CssClass="lbl-font-common" Text="number"></asp:Label>
                   </td>
                   <td class="td-lbl-cnt-sum-climp">
                    <asp:Label ID="lblIWCnt2" runat="server" CssClass="lbl-font-common"></asp:Label>
                   </td>
                   <td class="td-lbl-cnt-sum-climp">
                    <asp:Label ID="lblOOWCashCnt2" runat="server" CssClass="lbl-font-common"></asp:Label>
                   </td>
                   <td class="td-lbl-cnt-sum-climp">
                    <asp:Label ID="lblOOWCardCnt2" runat="server" CssClass="lbl-font-common"></asp:Label>
                   </td>
                   <td class="td-lbl-cnt-sum-climp">
                    <asp:Label ID="lblOtherCashCnt2" runat="server" CssClass="lbl-font-common"></asp:Label>
                   </td>
                   <td class="td-lbl-cnt-sum-climp">
                    <asp:Label ID="lblOtherCardCnt2" runat="server" CssClass="lbl-font-common"></asp:Label>
                   </td>
                   <td class="td-blank3"></td>
               </tr>
               <tr>
                   <td class="td-amount-notax-climpamount-notax">
                       <asp:Label ID="Label38" runat="server" CssClass="lbl-font-common" Text="amount(notax)"></asp:Label>
                   </td>
                   <td class="td-amount-notax-climpamount-notax">
                    <asp:Label ID="lblIWNoTax2" runat="server" CssClass="lbl-font-common"></asp:Label>
                   </td>
                   <td class="td-amount-notax-climpamount-notax">
                    <asp:Label ID="lblOWCashNoTax2" runat="server" CssClass="lbl-font-common"></asp:Label>
                   </td>
                   <td class="td-amount-notax-climpamount-notax">
                    <asp:Label ID="lblOWCardNoTax2" runat="server" CssClass="lbl-font-common"></asp:Label>
                   </td>
                   <td class="td-amount-notax-climpamount-notax">
                    <asp:Label ID="lblOtCashNoTax2" runat="server" CssClass="lbl-font-common"></asp:Label>
                   </td>
                   <td class="td-amount-notax-climpamount-notax">
                    <asp:Label ID="lblOtCardNoTax2" runat="server" CssClass="lbl-font-common"></asp:Label>
                   </td>
                   <td class="td-blank6"></td>
                  </tr>
               <tr>
                   <td class="td-cash-card-number-amount-taxin-climp-amount-taxin">
                       <asp:Label ID="Label44" runat="server" CssClass="lbl-font-common" Text="amount(taxin)"></asp:Label>
                   </td>
                   <td class="td-lbl-cnt-sum-climp">
                    <asp:Label ID="lblIWSum2" runat="server" CssClass="lbl-font-common"></asp:Label>
                   </td>
                   <td class="td-lbl-cnt-sum-climp">
                    <asp:Label ID="lblOOWCashSum2" runat="server" CssClass="lbl-font-common"></asp:Label>
                   </td>
                   <td class="td-lbl-cnt-sum-climp">
                    <asp:Label ID="lblOOWCardSum2" runat="server" CssClass="lbl-font-common"></asp:Label>
                   </td>
                   <td class="td-lbl-cnt-sum-climp">
                    <asp:Label ID="lblOtherCashSum2" runat="server" CssClass="lbl-font-common"></asp:Label>
                   </td>
                   <td class="td-lbl-cnt-sum-climp">
                    <asp:Label ID="lblOtherCardSum2" runat="server" CssClass="lbl-font-common"></asp:Label>
                   </td>
                   <td class="td-blannk4"></td>
               </tr>
               <tr>
                   <td class="td-amount-notax-climpamount-notax">
                    <asp:Label ID="Label50" runat="server" Text="claim amount(notax)" CssClass="lbl-font-common"></asp:Label>
                   </td>
                   <td class="td-amount-notax-climpamount-notax">
                    <asp:Label ID="lblIWNoTaxCla2" runat="server" CssClass="lbl-font-common"></asp:Label>
                   </td>
                   <td class="td-amount-notax-climpamount-notax">
                    <asp:Label ID="lblOWCashNoTaxCla2" runat="server" CssClass="lbl-font-common"></asp:Label>
                   </td>
                   <td class="td-amount-notax-climpamount-notax">
                    <asp:Label ID="lblOWCardNoTaxCla2" runat="server" CssClass="lbl-font-common"></asp:Label>
                   </td>
                   <td class="td-amount-notax-climpamount-notax">
                    <asp:Label ID="lblOtCashNoTaxCla2" runat="server" CssClass="lbl-font-common"></asp:Label>
                   </td>
                   <td class="td-amount-notax-climpamount-notax">
                    <asp:Label ID="lblOtCardNoTaxCla2" runat="server" CssClass="lbl-font-common"></asp:Label>
                   </td>
                   <td class="td-blank6"></td>
                 </tr>
               <tr>
                   <td class="td-cash-card-number-amount-taxin-climp-amount-taxin">
                    <asp:Label ID="Label57" runat="server" Text="claim amount(taxin)" CssClass="lbl-font-common"></asp:Label>
                   </td>
                   <td class="td-lbl-cnt-sum-climp">
                    <asp:Label ID="lblIWClaim2" runat="server" CssClass="lbl-font-common"></asp:Label>
                   </td>
                   <td class="td-lbl-cnt-sum-climp">
                    <asp:Label ID="lblOWCashClaim2" runat="server" CssClass="lbl-font-common"></asp:Label>
                   </td>
                   <td class="td-lbl-cnt-sum-climp">
                    <asp:Label ID="lblOWCardClaim2" runat="server" CssClass="lbl-font-common"></asp:Label>
                   </td>
                   <td class="td-lbl-cnt-sum-climp">
                    <asp:Label ID="lblOtCashClaim2" runat="server" CssClass="lbl-font-common"></asp:Label>
                   </td>
                   <td class="td-lbl-cnt-sum-climp">
                    <asp:Label ID="lblOtCardClaim2" runat="server" CssClass="lbl-font-common"></asp:Label>
                   </td>
                   <td class="td-blank3"></td>
                  </tr>
               <tr>
                   <td class="td-blank6"></td>
                   <td class="td-blank6"></td>
                   <td class="td-blank6"></td>
                   <td class="td-blank6"></td>
                   <td class="td-blank6"></td>
                   <td class="td-blank4"></td>
                   <td class="td-blank4"></td>
                </tr>
               <tr>
                   <td class="td-lbl-cash-sales-deposit">
                       <asp:Label ID="Label63" runat="server" CssClass="lbl-font-common" Text="cash total"></asp:Label>
                   </td>
                   <td class="td-lbl-cash-sales-deposit">
                    <asp:Label ID="lblCashTotal2" runat="server" CssClass="lbl-font-common"></asp:Label>
                    </td><td class="td-blank4"></td>
                  
                  
                   <td class="td-blank6"></td>
                   <td class="td-lbl-cash-sales-deposit">
                       <asp:Label ID="Label65" runat="server" CssClass="lbl-font-common" Text="sales total"></asp:Label>
                   </td>
                   <td class="td-lbl-cash-sales-deposit">
                    <asp:Label ID="lblSales2" runat="server" CssClass="lbl-font-common"></asp:Label>
                   </td>
               <td class="td-blank4"></td>
                
                   <td class="td-blank6"></td>
                   <td class="td-blank6"></td>
                  </tr>
               <tr>
                   <td class="td-lbl-cash-sales-deposit">
                       <asp:Label ID="Label67" runat="server" CssClass="lbl-font-common" Text="customer deposit"></asp:Label></td>
                   <td class="td-lbl-cash-sales-deposit">
                       <asp:Label ID="lblDepositCusto2" runat="server" CssClass="lbl-font-common"></asp:Label>
                   </td>
                   <td class="td-lbl-cash-sales-deposit"></td>
                   <td colspan = "2" class="td-lbl-custody">
                       <asp:Label ID="Label69" runat="server" CssClass="lbl-font-common" Text="Number of Custody"></asp:Label>&nbsp;&nbsp;
                       <asp:Label ID="lblNumCusto2" runat="server" CssClass="lbl-font-common"></asp:Label>
                   </td>
                 
                   <td class="td-blank6"></td>
                   <td class="td-blank6"></td>
                 </tr>
               <tr>
                   <td class="td-lbl-cash-sales-deposit">
                       <asp:Label ID="Label71" runat="server" CssClass="lbl-font-common" Text="Bank Deposit"></asp:Label></td>
                   <td class="td-lbl-cash-sales-deposit">
                       <asp:Label ID="lblDeposit2" runat="server" CssClass="lbl-font-common"></asp:Label>
                   </td>
         <td class="td-blank4"></td>
                   
                   <td class="td-blank4"></td>
                   <td class="td-blank4"></td>
                   <td class="td-blank4"></td>
                   <td class="td-blank6"></td>
                   <td class="td-blank6"></td>
               </tr>
               <tr>
        <td class="td-blank4"></td>
                   <td class="td-blank4"></td>
                   <td class="td-blank4"></td>
                   <td class="td-blank4"></td>
                   <td class="td-blank4"></td>
                   <td class="td-blank6"></td>
                   <td class="td-blank6"></td>
                </tr>
           </table>
           <table class="tbl-tables">
               <tr>
                   <td class="td-lbl-cash-sales-deposit">
                       <asp:Label ID="lblLocation3" runat="server" CssClass="lbl-font-common"></asp:Label>
                   </td>
                   <td colspan = "4" class="td-blank2">
                       &nbsp;</td>
                 
                   <td class="td-blank4"></td>
                   <td class="td-blank6"></td>
                   <td class="td-blank6"></td>
               </tr>
               <tr>
                   <td class="td-blank4"></td>
                   <td class="td-blank4"></td>
                   <td class="td-blank4"></td>
                   <td class="td-blank4"></td>
                   <td class="td-blank4"></td>
                   <td class="td-blank6"></td>
                   <td class="td-blank6"></td>
                   <td class="txtbox-unbound" rowspan = "12">
                       <asp:ListBox ID="List3Msg" runat="server" Height="297px" Width="297px"></asp:ListBox>
                   </td>
               </tr>
               <tr>
                   <td class="td-cash-card-number-amount-taxin-climp-amount-taxin"></td>
                   <td class="td-cash-card-number-amount-taxin-climp-amount-taxin">
                       <asp:Label ID="Label74" runat="server" CssClass="lbl-font-common" Text="IW"></asp:Label>
                   </td>
                   <td class="td-cash-card-number-amount-taxin-climp-amount-taxin">
                       <asp:Label ID="Label75" runat="server" CssClass="lbl-font-common" Text="OOW(cash)"></asp:Label>
                   </td>
                   <td class="td-cash-card-number-amount-taxin-climp-amount-taxin">
                       <asp:Label ID="Label78" runat="server" CssClass="lbl-font-common" Text="OOW(card)"></asp:Label>
                   </td>
                   <td class="td-cash-card-number-amount-taxin-climp-amount-taxin">
                       <asp:Label ID="Label79" runat="server" CssClass="lbl-font-common" Text="Other(cash)"></asp:Label>
                   </td>
                   <td class="td-cash-card-number-amount-taxin-climp-amount-taxin">
                       <asp:Label ID="Label80" runat="server" CssClass="lbl-font-common" Text="Other(card)"></asp:Label>
                   </td>
                   <td class="td-blank6"></td>
               </tr>
               <tr>
                   <td class="td-cash-card-number-amount-taxin-climp-amount-taxin">
                       <asp:Label ID="Label81" runat="server" CssClass="lbl-font-common" Text="number"></asp:Label>
                   </td>
                   <td class="td-lbl-cnt-sum-climp">
                    <asp:Label ID="lblIWCnt3" runat="server" CssClass="lbl-font-common"></asp:Label>
                   </td>
                   <td class="td-lbl-cnt-sum-climp">
                    <asp:Label ID="lblOOWCashCnt3" runat="server" CssClass="lbl-font-common"></asp:Label>
                   </td>
                   <td class="td-lbl-cnt-sum-climp">
                    <asp:Label ID="lblOOWCardCnt3" runat="server" CssClass="lbl-font-common"></asp:Label>
                   </td>
                   <td class="td-lbl-cnt-sum-climp">
                    <asp:Label ID="lblOtherCashCnt3" runat="server" CssClass="lbl-font-common"></asp:Label>
                   </td>
                   <td class="td-lbl-cnt-sum-climp">
                    <asp:Label ID="lblOtherCardCnt3" runat="server" CssClass="lbl-font-common"></asp:Label>
                   </td>
                   <td class="td-blank3"></td>
               </tr>
               <tr>
                   <td class="td-amount-notax-climpamount-notax">
                       <asp:Label ID="Label91" runat="server" CssClass="lbl-font-common" Text="amount(notax)"></asp:Label>
                   </td>
                   <td class="td-amount-notax-climpamount-notax">
                    <asp:Label ID="lblIWNoTax3" runat="server" CssClass="lbl-font-common"></asp:Label>
                   </td>
                   <td class="td-amount-notax-climpamount-notax">
                    <asp:Label ID="lblOWCashNoTax3" runat="server" CssClass="lbl-font-common"></asp:Label>
                   </td>
                   <td class="td-amount-notax-climpamount-notax">
                    <asp:Label ID="lblOWCardNoTax3" runat="server" CssClass="lbl-font-common"></asp:Label>
                   </td>
                   <td class="td-amount-notax-climpamount-notax">
                    <asp:Label ID="lblOtCashNoTax3" runat="server" CssClass="lbl-font-common"></asp:Label>
                   </td>
                   <td class="td-amount-notax-climpamount-notax">
                    <asp:Label ID="lblOtCardNoTax3" runat="server" CssClass="lbl-font-common"></asp:Label>
                   </td>
                   <td class="td-blank6"></td>
                  </tr>
               <tr>
                   <td class="td-cash-card-number-amount-taxin-climp-amount-taxin">
                       <asp:Label ID="Label97" runat="server" CssClass="lbl-font-common" Text="amount(taxin)"></asp:Label>
                   </td>
                   <td class="td-lbl-cnt-sum-climp">
                    <asp:Label ID="lblIWSum3" runat="server" CssClass="lbl-font-common"></asp:Label>
                   </td>
                   <td class="td-lbl-cnt-sum-climp">
                    <asp:Label ID="lblOOWCashSum3" runat="server" CssClass="lbl-font-common"></asp:Label>
                   </td>
                   <td class="td-lbl-cnt-sum-climp">
                    <asp:Label ID="lblOOWCardSum3" runat="server" CssClass="lbl-font-common"></asp:Label>
                   </td>
                   <td class="td-lbl-cnt-sum-climp">
                    <asp:Label ID="lblOtherCashSum3" runat="server" CssClass="lbl-font-common"></asp:Label>
                   </td>
                   <td class="td-lbl-cnt-sum-climp">
                    <asp:Label ID="lblOtherCardSum3" runat="server" CssClass="lbl-font-common"></asp:Label>
                   </td>
                   <td class="td-blank3"></td>
               </tr>
               <tr>
                   <td class="td-amount-notax-climpamount-notax">
                    <asp:Label ID="Label103" runat="server" Text="claim amount(notax)" CssClass="lbl-font-common"></asp:Label>
                   </td>
                   <td class="td-amount-notax-climpamount-notax">
                    <asp:Label ID="lblIWNoTaxCla3" runat="server" CssClass="lbl-font-common"></asp:Label>
                   </td>
                   <td class="td-amount-notax-climpamount-notax">
                    <asp:Label ID="lblOWCashNoTaxCla3" runat="server" CssClass="lbl-font-common"></asp:Label>
                   </td>
                   <td class="td-amount-notax-climpamount-notax">
                    <asp:Label ID="lblOWCardNoTaxCla3" runat="server" CssClass="lbl-font-common"></asp:Label>
                   </td>
                   <td class="td-amount-notax-climpamount-notax">
                    <asp:Label ID="lblOtCashNoTaxCla3" runat="server" CssClass="lbl-font-common"></asp:Label>
                   </td>
                   <td class="td-amount-notax-climpamount-notax">
                    <asp:Label ID="lblOtCardNoTaxCla3" runat="server" CssClass="lbl-font-common"></asp:Label>
                   </td>
                   <td class="td-amount-notax-climpamount-notax"></td>
                 </tr>
               <tr>
                   <td class="td-cash-card-number-amount-taxin-climp-amount-taxin">
                    <asp:Label ID="Label109" runat="server" Text="claim amount(taxin)" CssClass="lbl-font-common"></asp:Label>
                   </td>
                   <td class="td-lbl-cnt-sum-climp">
                    <asp:Label ID="lblIWClaim3" runat="server" CssClass="lbl-font-common"></asp:Label>
                   </td>
                   <td class="td-lbl-cnt-sum-climp">
                    <asp:Label ID="lblOWCashClaim3" runat="server" CssClass="lbl-font-common"></asp:Label>
                   </td>
                   <td class="td-lbl-cnt-sum-climp">
                    <asp:Label ID="lblOWCardClaim3" runat="server" CssClass="lbl-font-common"></asp:Label>
                   </td>
                   <td class="td-lbl-cnt-sum-climp">
                    <asp:Label ID="lblOtCashClaim3" runat="server" CssClass="lbl-font-common"></asp:Label>
                   </td>
                   <td class="td-lbl-cnt-sum-climp">
                    <asp:Label ID="lblOtCardClaim3" runat="server" CssClass="lbl-font-common"></asp:Label>
                   </td>
                   <td class="td-blank3"></td>
                  </tr>
               <tr>
                   <td class="td-blank6"></td>
                   <td class="td-blank6"></td>
                   <td class="td-blank6"></td>
                   <td class="td-blank6"></td>
                   <td class="td-blank6"></td>
                   <td class="td-blank4"></td>
                   <td class="td-blank4"></td>
                </tr>
               <tr>
                   <td class="td-lbl-cash-sales-deposit">
                       <asp:Label ID="Label115" runat="server" CssClass="lbl-font-common" Text="cash total"></asp:Label>
                   </td>
                   <td class="td-lbl-cash-sales-deposit">
                    <asp:Label ID="lblCashTotal3" runat="server" CssClass="lbl-font-common"></asp:Label>
                    </td>
                
                   <td class="td-blank6"></td>
                   <td class="td-lbl-cash-sales-deposit">
                       <asp:Label ID="Label117" runat="server" CssClass="lbl-font-common" Text="sales total"></asp:Label>
                   </td>
                   <td class="td-lbl-cash-sales-deposit">
                    <asp:Label ID="lblSales3" runat="server" CssClass="lbl-font-common"></asp:Label>
                   </td>
                  
                   <td class="td-blank6"></td>
                   <td class="td-blank6"></td>
                  </tr>
               <tr>
                   <td class="td-lbl-cash-sales-deposit">
                       <asp:Label ID="Label119" runat="server" CssClass="lbl-font-common" Text="customer deposit"></asp:Label></td>
                   <td class="td-lbl-cash-sales-deposit">
                       <asp:Label ID="lblDepositCusto3" runat="server" CssClass="lbl-font-common"></asp:Label>
                   </td>
                   <td class="td-blank4"></td>
                   <td colspan = "2" class="td-lbl-custody">
                       <asp:Label ID="Label121" runat="server" CssClass="lbl-font-common" Text="Number of Custody"></asp:Label>&nbsp;&nbsp;
                       <asp:Label ID="lblNumCusto3" runat="server" CssClass="lbl-font-common"></asp:Label>
                   </td>
                  
                   <td class="td-blank6"></td>
                   <td class="td-blank6"></td>
                 </tr>
               <tr>
                   <td class="td-lbl-cash-sales-deposit">
                       <asp:Label ID="Label123" runat="server" CssClass="lbl-font-common" Text="Bank Deposit"></asp:Label></td>
                   <td class="td-lbl-cash-sales-deposit">
                       <asp:Label ID="lblDeposit3" runat="server" CssClass="lbl-font-common"></asp:Label>
                   </td>
                
                   <td class="td-blank4"></td>
                   <td class="td-blank4"></td>
                   <td class="td-blank4"></td>
                   <td class="td-blank6"></td>
                   <td class="td-blank6"></td>
               </tr>
               <tr>
                   <td class="td-blank4"></td>
                   <td class="td-blank4"></td>
                   <td class="td-blank4"></td>
                   <td class="td-blank4"></td>
                   <td class="td-blank4"></td>
                   <td class="td-blank6"></td>
                   <td class="td-blank6"></td>
                </tr>
           </table>
           <table class="tbl-tables">
               <tr>
                   <td class="td-lbl-cash-sales-deposit">
                       <asp:Label ID="lblLocation4" runat="server" CssClass="lbl-font-common"></asp:Label>
                   </td>
                   <td colspan = "4" class="td-blank2">
                       &nbsp;</td>
                
                   <td class="td-blank4"></td>
                   <td class="td-blank6"></td>
                   <td class="td-blank6"></td>
               </tr>
               <tr>
                   <td class="td-blank4"></td>
                   <td class="td-blank4"></td>
                   <td class="td-blank4"></td>
                   <td class="td-blank4"></td>
                   <td class="td-blank4"></td>
                   <td class="td-blank6"></td>
                   <td class="td-blank6"></td>
                   <td class="txtbox-unbound" rowspan = "12">
                       <asp:ListBox ID="List4Msg" runat="server" Height="297px" Width="297px"></asp:ListBox>
                   </td>
               </tr>
               <tr>
                   <td class="td-cash-card-number-amount-taxin-climp-amount-taxin"></td>
                   <td class="td-cash-card-number-amount-taxin-climp-amount-taxin">
                       <asp:Label ID="Label126" runat="server" CssClass="lbl-font-common" Text="IW"></asp:Label>
                   </td>
                   <td class="td-cash-card-number-amount-taxin-climp-amount-taxin">
                       <asp:Label ID="Label127" runat="server" CssClass="lbl-font-common" Text="OOW(cash)"></asp:Label>
                   </td>
                   <td class="td-cash-card-number-amount-taxin-climp-amount-taxin">
                       <asp:Label ID="Label128" runat="server" CssClass="lbl-font-common" Text="OOW(card)"></asp:Label>
                   </td>
                   <td class="td-cash-card-number-amount-taxin-climp-amount-taxin">
                       <asp:Label ID="Label129" runat="server" CssClass="lbl-font-common" Text="Other(cash)"></asp:Label>
                   </td>
                   <td class="td-cash-card-number-amount-taxin-climp-amount-taxin">
                       <asp:Label ID="Label130" runat="server" CssClass="lbl-font-common" Text="Other(card)"></asp:Label>
                   </td>
                   <td class="td-blank6"></td>
               </tr>
               <tr>
                   <td class="td-cash-card-number-amount-taxin-climp-amount-taxin">
                       <asp:Label ID="Label131" runat="server" CssClass="lbl-font-common" Text="number"></asp:Label>
                   </td>
                   <td class="td-lbl-cnt-sum-climp">
                    <asp:Label ID="lblIWCnt4" runat="server" CssClass="lbl-font-common"></asp:Label>
                   </td>
                   <td class="td-lbl-cnt-sum-climp">
                    <asp:Label ID="lblOOWCashCnt4" runat="server" CssClass="lbl-font-common"></asp:Label>
                   </td>
                   <td class="td-lbl-cnt-sum-climp">
                    <asp:Label ID="lblOOWCardCnt4" runat="server" CssClass="lbl-font-common"></asp:Label>
                   </td>
                   <td class="td-lbl-cnt-sum-climp">
                    <asp:Label ID="lblOtherCashCnt4" runat="server" CssClass="lbl-font-common"></asp:Label>
                   </td>
                   <td class="td-lbl-cnt-sum-climp">
                    <asp:Label ID="lblOtherCardCnt4" runat="server" CssClass="lbl-font-common"></asp:Label>
                   </td>
                   <td class="td-blank3"></td>
               </tr>
               <tr>
                   <td class="td-amount-notax-climpamount-notax">
                       <asp:Label ID="Label137" runat="server" CssClass="lbl-font-common" Text="amount(notax)"></asp:Label>
                   </td>
                   <td class="td-amount-notax-climpamount-notax">
                    <asp:Label ID="lblIWNoTax4" runat="server" CssClass="lbl-font-common"></asp:Label>
                   </td>
                   <td class="td-amount-notax-climpamount-notax">
                    <asp:Label ID="lblOWCashNoTax4" runat="server" CssClass="lbl-font-common"></asp:Label>
                   </td>
                   <td class="td-amount-notax-climpamount-notax">
                    <asp:Label ID="lblOWCardNoTax4" runat="server" CssClass="lbl-font-common"></asp:Label>
                   </td>
                   <td class="td-amount-notax-climpamount-notax">
                    <asp:Label ID="lblOtCashNoTax4" runat="server" CssClass="lbl-font-common"></asp:Label>
                   </td>
                   <td class="td-amount-notax-climpamount-notax">
                    <asp:Label ID="lblOtCardNoTax4" runat="server" CssClass="lbl-font-common"></asp:Label>
                   </td>
                   <td class="td-blank6"></td>
               <tr>
                   <td class="td-cash-card-number-amount-taxin-climp-amount-taxin">
                       <asp:Label ID="Label143" runat="server" CssClass="lbl-font-common" Text="amount(taxin)"></asp:Label>
                   </td>
                   <td class="td-lbl-cnt-sum-climp">
                    <asp:Label ID="lblIWSum4" runat="server" CssClass="lbl-font-common"></asp:Label>
                   </td>
                   <td class="td-lbl-cnt-sum-climp">
                    <asp:Label ID="lblOOWCashSum4" runat="server" CssClass="lbl-font-common"></asp:Label>
                   </td>
                   <td class="td-lbl-cnt-sum-climp">
                    <asp:Label ID="lblOOWCardSum4" runat="server" CssClass="lbl-font-common"></asp:Label>
                   </td>
                   <td class="td-lbl-cnt-sum-climp">
                    <asp:Label ID="lblOtherCashSum4" runat="server" CssClass="lbl-font-common"></asp:Label>
                   </td>
                   <td class="td-lbl-cnt-sum-climp">
                    <asp:Label ID="lblOtherCardSum4" runat="server" CssClass="lbl-font-common"></asp:Label>
                   </td>
                   <td class="td-blank3"></td>
               </tr>
               <tr>
                   <td class="td-amount-notax-climpamount-notax">
                    <asp:Label ID="Label149" runat="server" Text="claim amount(notax)" CssClass="lbl-font-common"></asp:Label>
                   </td>
                   <td class="td-amount-notax-climpamount-notax">
                    <asp:Label ID="lblIWNoTaxCla4" runat="server" CssClass="lbl-font-common"></asp:Label>
                   </td>
                   <td class="td-amount-notax-climpamount-notax">
                    <asp:Label ID="lblOWCashNoTaxCla4" runat="server" CssClass="lbl-font-common"></asp:Label>
                   </td>
                   <td class="td-amount-notax-climpamount-notax">
                    <asp:Label ID="lblOWCardNoTaxCla4" runat="server" CssClass="lbl-font-common"></asp:Label>
                   </td>
                   <td class="td-amount-notax-climpamount-notax">
                    <asp:Label ID="lblOtCashNoTaxCla4" runat="server" CssClass="lbl-font-common"></asp:Label>
                   </td>
                   <td class="td-amount-notax-climpamount-notax">
                    <asp:Label ID="lblOtCardNoTaxCla4" runat="server" CssClass="lbl-font-common"></asp:Label>
                   </td>
                   <td class="td-blank3"></td>
                 </tr>
               <tr>
                   <td class="td-cash-card-number-amount-taxin-climp-amount-taxin">
                    <asp:Label ID="Label155" runat="server" Text="claim amount(taxin)" CssClass="lbl-font-common"></asp:Label>
                   </td>
                   <td class="td-lbl-cnt-sum-climp">
                    <asp:Label ID="lblIWClaim4" runat="server" CssClass="lbl-font-common"></asp:Label>
                   </td>
                   <td class="td-lbl-cnt-sum-climp">
                    <asp:Label ID="lblOWCashClaim4" runat="server" CssClass="lbl-font-common"></asp:Label>
                   </td>
                   <td class="td-lbl-cnt-sum-climp">
                    <asp:Label ID="lblOWCardClaim4" runat="server" CssClass="lbl-font-common"></asp:Label>
                   </td>
                   <td class="td-lbl-cnt-sum-climp">
                    <asp:Label ID="lblOtCashClaim4" runat="server" CssClass="lbl-font-common"></asp:Label>
                   </td>
                   <td class="td-lbl-cnt-sum-climp">
                    <asp:Label ID="lblOtCardClaim4" runat="server" CssClass="lbl-font-common"></asp:Label>
                   </td>
                   <td class="td-blank3"></td>
                  </tr>
               <tr>
                   <td class="td-blank4"></td>
                   <td class="td-blank6"></td>
                   <td class="td-blank6"></td>
                   <td class="td-blank6"></td>
                   <td class="td-blank6"></td>
                   <td class="td-blank4"></td>
                   <td class="td-blank4"></td>
                </tr>
               <tr>
                   <td class="td-lbl-cash-sales-deposit">
                       <asp:Label ID="Label161" runat="server" CssClass="lbl-font-common" Text="cash total"></asp:Label>
                   </td>
                   <td class="td-lbl-cash-sales-deposit">
                    <asp:Label ID="lblCashTotal4" runat="server" CssClass="lbl-font-common"></asp:Label>
                    </td>
                   <td class="td-blank6"></td>
                   <td class="td-lbl-cash-sales-deposit">
                       <asp:Label ID="Label163" runat="server" CssClass="lbl-font-common" Text="sales total"></asp:Label>
                   </td>
                   <td class="td-lbl-cash-sales-deposit">
                    <asp:Label ID="lblSales4" runat="server" CssClass="lbl-font-common"></asp:Label>
                   </td>
                   
                   <td class="td-blank6"></td>
                   <td class="td-blank6"></td>
                  </tr>
               <tr>
                   <td class="td-lbl-cash-sales-deposit">
                       <asp:Label ID="Label165" runat="server" CssClass="lbl-font-common" Text="customer deposit"></asp:Label></td>
                   <td class="td-lbl-cash-sales-deposit">
                       <asp:Label ID="lblDepositCusto4" runat="server" CssClass="lbl-font-common"></asp:Label>
                   </td>
                   <td class="td-blank6"></td>
                   <td colspan = "2" class="td-lbl-custody">
                       <asp:Label ID="Label167" runat="server" CssClass="lbl-font-common" Text="Number of Custody"></asp:Label>&nbsp;&nbsp;
                       <asp:Label ID="lblNumCusto4" runat="server" CssClass="lbl-font-common"></asp:Label>
                   </td>
                
                   <td class="td-blank6"></td>
                   <td class="td-blank6"></td>
                 </tr>
               <tr>
                   <td class="td-lbl-cash-sales-deposit">
                       <asp:Label ID="Label169" runat="server" CssClass="lbl-font-common" Text="Bank Deposit"></asp:Label></td>
                   <td class="td-lbl-cash-sales-deposit">
                       <asp:Label ID="lblDeposit4" runat="server" CssClass="lbl-font-common"></asp:Label>
                   </td>
                  
                   <td class="td-blank4"></td>
                   <td class="td-blank4"></td>
                   <td class="td-blank4"></td>
                   <td class="td-blank6"></td>
                   <td class="td-blank6"></td>
               </tr>
               <tr>
                  <td class="td-blank4"></td>
                   <td class="td-blank4"></td>
                   <td class="td-blank4"></td>
                   <td class="td-blank4"></td>
                   <td class="td-blank4"></td>
                   <td class="td-blank6"></td>
                   <td class="td-blank6"></td>
                </tr>
           </table>
           <table class="tbl-tables">
               <tr>
                   <td class="td-lbl-cash-sales-deposit">
                       <asp:Label ID="lblLocation5" runat="server" CssClass="lbl-font-common"></asp:Label>
                   </td>
                   <td colspan = "4" class="td-blank2">
                       &nbsp;</td>
                  <td class="td-blank4"></td>
                   <td class="td-blank4"></td>
                   <td class="td-blank4"></td>
                 
               </tr>
               <tr>
               
                   <td class="td-blank4"></td>
                   <td class="td-blank4"></td>
                   <td class="td-blank4"></td>
                   <td class="td-blank4"></td>
                   <td class="td-blank6"></td>
                   <td class="td-blank6"></td>
                   <td class="txtbox-unbound" rowspan = "12">
                       <asp:ListBox ID="List5Msg" runat="server" Height="297px" Width="297px"></asp:ListBox>
                   </td>
               </tr>
               <tr>
                   <td class="td-cash-card-number-amount-taxin-climp-amount-taxin"></td>
                   <td class="td-cash-card-number-amount-taxin-climp-amount-taxin">
                       <asp:Label ID="Label172" runat="server" CssClass="lbl-font-common" Text="IW"></asp:Label>
                   </td>
                   <td class="td-cash-card-number-amount-taxin-climp-amount-taxin">
                       <asp:Label ID="Label173" runat="server" CssClass="lbl-font-common" Text="OOW(cash)"></asp:Label>
                   </td>
                   <td class="td-cash-card-number-amount-taxin-climp-amount-taxin">
                       <asp:Label ID="Label174" runat="server" CssClass="lbl-font-common" Text="OOW(card)"></asp:Label>
                   </td>
                   <td class="td-cash-card-number-amount-taxin-climp-amount-taxin">
                       <asp:Label ID="Label175" runat="server" CssClass="lbl-font-common" Text="Other(cash)"></asp:Label>
                   </td>
                   <td class="td-cash-card-number-amount-taxin-climp-amount-taxin">
                       <asp:Label ID="Label176" runat="server" CssClass="lbl-font-common" Text="Other(card)"></asp:Label>
                   </td>
                   <td class="td-blank6"></td>
               </tr>
               <tr>
                   <td class="td-cash-card-number-amount-taxin-climp-amount-taxin">
                       <asp:Label ID="Label177" runat="server" CssClass="lbl-font-common" Text="number"></asp:Label>
                   </td>
                   <td class="td-lbl-cnt-sum-climp">
                    <asp:Label ID="lblIWCnt5" runat="server" CssClass="lbl-font-common"></asp:Label>
                   </td>
                   <td class="td-lbl-cnt-sum-climp">
                    <asp:Label ID="lblOOWCashCnt5" runat="server" CssClass="lbl-font-common"></asp:Label>
                   </td>
                   <td class="td-lbl-cnt-sum-climp">
                    <asp:Label ID="lblOOWCardCnt5" runat="server" CssClass="lbl-font-common"></asp:Label>
                   </td>
                   <td class="td-lbl-cnt-sum-climp">
                    <asp:Label ID="lblOtherCashCnt5" runat="server" CssClass="lbl-font-common"></asp:Label>
                   </td>
                   <td class="td-lbl-cnt-sum-climp">
                    <asp:Label ID="lblOtherCardCnt5" runat="server" CssClass="lbl-font-common"></asp:Label>
                   </td>
                   <td class="td-blank3"></td>
               </tr>
               <tr>
                   <td class="td-amount-notax-climpamount-notax">
                       <asp:Label ID="Label183" runat="server" CssClass="lbl-font-common" Text="amount(notax)"></asp:Label>
                   </td>
                   <td class="td-amount-notax-climpamount-notax">
                    <asp:Label ID="lblIWNoTax5" runat="server" CssClass="lbl-font-common"></asp:Label>
                   </td>
                   <td class="td-amount-notax-climpamount-notax">
                    <asp:Label ID="lblOWCashNoTax5" runat="server" CssClass="lbl-font-common"></asp:Label>
                   </td>
                   <td class="td-amount-notax-climpamount-notax">
                    <asp:Label ID="lblOWCardNoTax5" runat="server" CssClass="lbl-font-common"></asp:Label>
                   </td>
                   <td class="td-amount-notax-climpamount-notax">
                    <asp:Label ID="lblOtCashNoTax5" runat="server" CssClass="lbl-font-common"></asp:Label>
                   </td>
                   <td class="td-amount-notax-climpamount-notax">
                    <asp:Label ID="lblOtCardNoTax5" runat="server" CssClass="lbl-font-common"></asp:Label>
                   </td>
                   <td class="td-amount-notax-climpamount-notax"></td>
                  </tr>
               <tr>
                   <td class="td-cash-card-number-amount-taxin-climp-amount-taxin">
                       <asp:Label ID="Label189" runat="server" CssClass="lbl-font-common" Text="amount(taxin)"></asp:Label>
                   </td>
                   <td class="td-lbl-cnt-sum-climp">
                    <asp:Label ID="lblIWSum5" runat="server" CssClass="lbl-font-common"></asp:Label>
                   </td>
                   <td class="td-lbl-cnt-sum-climp">
                    <asp:Label ID="lblOOWCashSum5" runat="server" CssClass="lbl-font-common"></asp:Label>
                   </td>
                   <td class="td-lbl-cnt-sum-climp">
                    <asp:Label ID="lblOOWCardSum5" runat="server" CssClass="lbl-font-common"></asp:Label>
                   </td>
                   <td class="td-lbl-cnt-sum-climp">
                    <asp:Label ID="lblOtherCashSum5" runat="server" CssClass="lbl-font-common"></asp:Label>
                   </td>
                   <td class="td-lbl-cnt-sum-climp">
                    <asp:Label ID="lblOtherCardSum5" runat="server" CssClass="lbl-font-common"></asp:Label>
                   </td>
                   <td class="td-blank3"></td>
               <tr>
                   <td class="td-amount-notax-climpamount-notax">
                    <asp:Label ID="Label195" runat="server" Text="claim amount(notax)" CssClass="lbl-font-common"></asp:Label>
                   </td>
                   <td class="td-amount-notax-climpamount-notax">
                    <asp:Label ID="lblIWNoTaxCla5" runat="server" CssClass="lbl-font-common"></asp:Label>
                   </td>
                   <td class="td-amount-notax-climpamount-notax">
                    <asp:Label ID="lblOWCashNoTaxCla5" runat="server" CssClass="lbl-font-common"></asp:Label>
                   </td>
                   <td class="td-amount-notax-climpamount-notax">
                    <asp:Label ID="lblOWCardNoTaxCla5" runat="server" CssClass="lbl-font-common"></asp:Label>
                   </td>
                   <td class="td-amount-notax-climpamount-notax">
                    <asp:Label ID="lblOtCashNoTaxCla5" runat="server" CssClass="lbl-font-common"></asp:Label>
                   </td>
                   <td class="td-amount-notax-climpamount-notax">
                    <asp:Label ID="lblOtCardNoTaxCla5" runat="server" CssClass="lbl-font-common"></asp:Label>
                   </td>
                   <td class="td-blank6"></td>
                 </tr>
               <tr>
                   <td class="td-cash-card-number-amount-taxin-climp-amount-taxin">
                    <asp:Label ID="Label201" runat="server" Text="claim amount(taxin)" CssClass="lbl-font-common"></asp:Label>
                   </td>
                   <td class="td-lbl-cnt-sum-climp">
                    <asp:Label ID="lblIWClaim5" runat="server" CssClass="lbl-font-common"></asp:Label>
                   </td>
                   <td class="td-lbl-cnt-sum-climp">
                    <asp:Label ID="lblOWCashClaim5" runat="server" CssClass="lbl-font-common"></asp:Label>
                   </td>
                   <td class="td-lbl-cnt-sum-climp">
                    <asp:Label ID="lblOWCardClaim5" runat="server" CssClass="lbl-font-common"></asp:Label>
                   </td>
                   <td class="td-lbl-cnt-sum-climp">
                    <asp:Label ID="lblOtCashClaim5" runat="server" CssClass="lbl-font-common"></asp:Label>
                   </td>
                   <td class="td-lbl-cnt-sum-climp">
                    <asp:Label ID="lblOtCardClaim5" runat="server" CssClass="lbl-font-common"></asp:Label>
                   </td>
                   <td class="td-blank6"></td>
                  </tr>
               <tr>
                   
                   <td class="td-blank4"></td>
                   <td class="td-blank4"></td>
                   <td class="td-blank4"></td>
                   <td class="td-blank4"></td>
                   <td class="td-blank6"></td>
                   <td class="td-blank6"></td>
                </tr>
               <tr>
                   <td class="td-lbl-cash-sales-deposit">
                       <asp:Label ID="Label207" runat="server" CssClass="lbl-font-common" Text="cash total"></asp:Label>
                   </td>
                   <td class="td-lbl-cash-sales-deposit">
                    <asp:Label ID="lblCashTotal5" runat="server" CssClass="lbl-font-common"></asp:Label>
                    </td>
                   <td class="td-blank4"></td>
                   <td class="td-lbl-cash-sales-deposit">
                       <asp:Label ID="Label209" runat="server" CssClass="lbl-font-common" Text="sales total"></asp:Label>
                   </td>
                   <td class="td-blank4">
                    <asp:Label ID="lblSales5" runat="server" CssClass="lbl-font-common"></asp:Label>
                   </td>
                   <td class="td-blank4"></td>
                   <td class="td-blank4"></td>
                  </tr>
               <tr>
                   <td class="td-lbl-cash-sales-deposit">
                       <asp:Label ID="Label211" runat="server" CssClass="lbl-font-common" Text="customer deposit"></asp:Label></td>
                   <td class="td-lbl-cash-sales-deposit">
                       <asp:Label ID="lblDepositCusto5" runat="server" CssClass="lbl-font-common"></asp:Label>
                   </td>
                   <td class="td-blank4"></td>
                   <td colspan = "2" class="td-lbl-custody">
                       <asp:Label ID="Label213" runat="server" CssClass="lbl-font-common" Text="Number of Custody"></asp:Label>&nbsp;&nbsp;
                       <asp:Label ID="lblNumCusto5" runat="server" CssClass="lbl-font-common"></asp:Label>
                   </td>
                   <td class="td-blank6"></td>
                   <td class="td-blank6"></td>
                 </tr>
               <tr>
                   <td class="td-lbl-cash-sales-deposit">
                       <asp:Label ID="Label215" runat="server" CssClass="lbl-font-common" Text="Bank Deposit"></asp:Label></td>
                   <td class="td-lbl-cash-sales-deposit">
                       <asp:Label ID="lblDeposit5" runat="server" CssClass="lbl-font-common"></asp:Label>
                   </td>
<td class="td-blank4"></td>
                   
                   <td class="td-blank4"></td>
                   <td class="td-blank4"></td>
                   <td class="td-blank4"></td>
                   <td class="td-blank6"></td>
                   <td class="td-blank6"></td>
               </tr>
               <tr>
                   
                   <td class="td-blank4"></td>
                   <td class="td-blank4"></td>
                   <td class="td-blank4"></td>
                   <td class="td-blank4"></td>
                   <td class="td-blank6"></td>
                   <td class="td-blank6"></td>
                </tr>
           </table>



       </div>
     </div>

      <div id="dialog" title="message" style="display:none;"> 
        <asp:Label ID="lblMsg" runat="server" Text=""></asp:Label>
     </div>

</asp:Content>

