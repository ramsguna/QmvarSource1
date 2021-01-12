<%@ Page Title="QMVQR-Money_Report" Language="vb" AutoEventWireup="false" MasterPageFile="~/Site4.Master" CodeBehind="Money_Report.aspx.vb" Inherits="Ganges33.Money_Report" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="ajaxToolkit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script src="http://ajax.googleapis.com/ajax/libs/jquery/1.7.0/jquery.min.js"></script>
    <link type="text/css" href="http://ajax.googleapis.com/ajax/libs/jqueryui/1/themes/start/jquery-ui.css" rel="stylesheet">
    <script type="text/javascript" src="http://ajax.googleapis.com/ajax/libs/jqueryui/1/jquery-ui.min.js"></script>
    <link type="text/css" href="CSS/Common/Money_Report.css" rel="stylesheet" /> 

        <style type="text/css">
        
      </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
     <div class="div-imgBackground" style="background-image: url('pagewall_2/wall_money-2.png')">
       <div class="div-EntirePage">
        <table class="tabel-EntirePage">
            <tr>
                <td class="td-Blank1"></td>
                <td class="td-Blank2">
                </td>
                <td class="td-Blank3-4-5-6-7-8-9">
                </td>
                <td class="td-Blank3-4-5-6-7-8-9"></td>
                <td class="td-Blank3-4-5-6-7-8-9"></td>
                <td class="td-Blank3-4-5-6-7-8-9"></td>
                <td class="td-Blank3-4-5-6-7-8-9"></td>
                <td class="td-Blank3-4-5-6-7-8-9">
                </td>
            </tr>
            <tr>
                <td class="td-Blank10"> </td>
                <td colspan = "5"> 
                    <asp:Label ID="lblDate" runat="server" Text="Record date&amp;time : " CssClass="fontFamily"></asp:Label>
                    <asp:Label ID="lblRecord" runat="server" CssClass="fontFamily"></asp:Label>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                    <asp:Label ID="lblName" runat="server" Text="user name: " CssClass="fontFamily"></asp:Label>
                    <asp:Label ID="lblYousername" runat="server" CssClass="fontFamily"></asp:Label>
                </td>
                <td class="td-Blank11" align = "right"> 
                    <asp:ImageButton ID="btnStart" runat="server" ImageUrl="~/icon/start.png" CssClass="img-btnStart" />
                </td>
                <td class="td-Blank12"> </td>
            </tr>
            <tr>
                <td class="td-Blank13-14"></td>
                <td class="td-label24-Blank15">
                    <asp:Label ID="Label24" runat="server" CssClass="lbl24-25-TotalNumber-Count" Text="Total Number"></asp:Label>
                </td>
                <td class="td-lbl25-lblCount-td-Blank-16-17-18-19-20-21-22-23">
                    <asp:Label ID="Label25" runat="server" CssClass="lbl24-25-TotalNumber-Count" Text="Count :"></asp:Label>
                    <asp:Label ID="lblCount" runat="server" CssClass="fontFamily"></asp:Label>
                </td>
                <td class="td-lbl25-lblCount-td-Blank-16-17-18-19-20-21-22-23">&nbsp;</td>
                <td class="td-lbl25-lblCount-td-Blank-16-17-18-19-20-21-22-23"></td>
                <td class="td-lbl25-lblCount-td-Blank-16-17-18-19-20-21-22-23">
                    &nbsp;</td>
                <td class="td-lbl25-lblCount-td-Blank-16-17-18-19-20-21-22-23"></td>
                <td class="td-lbl25-lblCount-td-Blank-16-17-18-19-20-21-22-23"></td>
            </tr>
            <tr>
                <td class="td-Blank-16-17-18-19-20-21"></td>
                <td class="td-Blank22-lbl-7-9-2"></td>
                <td class="td-lbl-10-11-6-13-15-lblIWCnt-lblOOWCashCnt-lblOOWCardCnt-lblOtherCashCnt-lblOtherCardCnt-lblIWSum0-lblOOWCashSum0-lblOOWCardSum0-lblOtherCashSum0-lblOtherCardSum0">
                    <asp:Label ID="Label10" runat="server" Text="IW" CssClass="fontFamily"></asp:Label>
                </td>
                <td class="td-lbl-10-11-6-13-15-lblIWCnt-lblOOWCashCnt-lblOOWCardCnt-lblOtherCashCnt-lblOtherCardCnt-lblIWSum0-lblOOWCashSum0-lblOOWCardSum0-lblOtherCashSum0-lblOtherCardSum0">
                    <asp:Label ID="Label11" runat="server" Text="OOW(cash)" CssClass="fontFamily"></asp:Label>
                </td>
                <td class="td-lbl-10-11-6-13-15-lblIWCnt-lblOOWCashCnt-lblOOWCardCnt-lblOtherCashCnt-lblOtherCardCnt-lblIWSum0-lblOOWCashSum0-lblOOWCardSum0-lblOtherCashSum0-lblOtherCardSum0">
                    <asp:Label ID="Label16" runat="server" Text="OOW(card)" CssClass="fontFamily"></asp:Label>
                </td>
                <td class="td-lbl-10-11-6-13-15-lblIWCnt-lblOOWCashCnt-lblOOWCardCnt-lblOtherCashCnt-lblOtherCardCnt-lblIWSum0-lblOOWCashSum0-lblOOWCardSum0-lblOtherCashSum0-lblOtherCardSum0">
                    <asp:Label ID="Label13" runat="server" Text="other(cash)" CssClass="fontFamily"></asp:Label>
                </td>
                <td class="td-lbl-10-11-6-13-15-lblIWCnt-lblOOWCashCnt-lblOOWCardCnt-lblOtherCashCnt-lblOtherCardCnt-lblIWSum0-lblOOWCashSum0-lblOOWCardSum0-lblOtherCashSum0-lblOtherCardSum0">
                    <asp:Label ID="Label15" runat="server" Text="other(card)" CssClass="fontFamily"></asp:Label>
                </td>
                <td class="td-Blank-23-24-25-26-27-28"></td>
            </tr>
            <tr>
                <td class="td-Blank-16-17-18-19-20-21"></td>
                <td class="td-Blank22-lbl-7-9-2">
                    <asp:Label ID="Label7" runat="server" Text="number" CssClass="fontFamily"></asp:Label>
                </td>
                <td class="td-lbl-10-11-6-13-15-lblIWCnt-lblOOWCashCnt-lblOOWCardCnt-lblOtherCashCnt-lblOtherCardCnt-lblIWSum0-lblOOWCashSum0-lblOOWCardSum0-lblOtherCashSum0-lblOtherCardSum0">
                    <asp:Label ID="lblIWCnt" runat="server" CssClass="fontFamily"></asp:Label>
                </td>
                <td class="td-lbl-10-11-6-13-15-lblIWCnt-lblOOWCashCnt-lblOOWCardCnt-lblOtherCashCnt-lblOtherCardCnt-lblIWSum0-lblOOWCashSum0-lblOOWCardSum0-lblOtherCashSum0-lblOtherCardSum0">
                    <asp:Label ID="lblOOWCashCnt" runat="server" CssClass="fontFamily"></asp:Label>
                </td>
                <td class="td-lbl-10-11-6-13-15-lblIWCnt-lblOOWCashCnt-lblOOWCardCnt-lblOtherCashCnt-lblOtherCardCnt-lblIWSum0-lblOOWCashSum0-lblOOWCardSum0-lblOtherCashSum0-lblOtherCardSum0">
                    <asp:Label ID="lblOOWCardCnt" runat="server" CssClass="fontFamily"></asp:Label>
                </td>
                <td class="td-lbl-10-11-6-13-15-lblIWCnt-lblOOWCashCnt-lblOOWCardCnt-lblOtherCashCnt-lblOtherCardCnt-lblIWSum0-lblOOWCashSum0-lblOOWCardSum0-lblOtherCashSum0-lblOtherCardSum0">
                    <asp:Label ID="lblOtherCashCnt" runat="server" CssClass="fontFamily"></asp:Label>
                </td>
                <td class="td-lbl-10-11-6-13-15-lblIWCnt-lblOOWCashCnt-lblOOWCardCnt-lblOtherCashCnt-lblOtherCardCnt-lblIWSum0-lblOOWCashSum0-lblOOWCardSum0-lblOtherCashSum0-lblOtherCardSum0">
                    <asp:Label ID="lblOtherCardCnt" runat="server" CssClass="fontFamily"></asp:Label>
                </td>
                <td class="td-Blank-23-24-25-26-27-28"></td>
            </tr>
            <tr>
                <td class="td-Blank-16-17-18-19-20-21"></td>
                <td class="td-lbl-8-1">
                    <asp:Label ID="Label8" runat="server" Text="amount(notax)" CssClass="fontFamily"></asp:Label>
                </td>
                <td class="td-lblIWNoTax-lblOWCashNoTax-lblOtCashNoTax-lblOtCardNoTax">
                    <asp:Label ID="lblIWNoTax" runat="server" CssClass="fontFamily"></asp:Label>
                </td>
                <td class="td-lblIWNoTax-lblOWCashNoTax-lblOtCashNoTax-lblOtCardNoTax">
                    <asp:Label ID="lblOWCashNoTax" runat="server" CssClass="fontFamily"></asp:Label>
                </td>
                <td class="td-lblIWNoTax-lblOWCashNoTax-lblOtCashNoTax-lblOtCardNoTax">
                    <asp:Label ID="lblOWCardNoTax" runat="server" CssClass="fontFamily"></asp:Label>
                </td>
                <td class="td-lblIWNoTax-lblOWCashNoTax-lblOtCashNoTax-lblOtCardNoTax">
                    <asp:Label ID="lblOtCashNoTax" runat="server" CssClass="fontFamily"></asp:Label>
                </td>
                <td class="td-lblIWNoTax-lblOWCashNoTax-lblOtCashNoTax-lblOtCardNoTax">
                    <asp:Label ID="lblOtCardNoTax" runat="server" CssClass="fontFamily"></asp:Label>
                </td>
                <td class="td-Blank-23-24-25-26-27-28"></td>
            </tr>
            <tr>
                <td class="td-Blank-16-17-18-19-20-21"></td>
                <td class="td-Blank22-lbl-7-9-2">
                    <asp:Label ID="Label9" runat="server" Text="amoun(taxin)" CssClass="fontFamily"></asp:Label>
                </td>
                <td class="td-lbl-10-11-6-13-15-lblIWCnt-lblOOWCashCnt-lblOOWCardCnt-lblOtherCashCnt-lblOtherCardCnt-lblIWSum0-lblOOWCashSum0-lblOOWCardSum0-lblOtherCashSum0-lblOtherCardSum0">
                    <asp:Label ID="lblIWSum0" runat="server" CssClass="fontFamily"></asp:Label>
                </td>
                <td class="td-lbl-10-11-6-13-15-lblIWCnt-lblOOWCashCnt-lblOOWCardCnt-lblOtherCashCnt-lblOtherCardCnt-lblIWSum0-lblOOWCashSum0-lblOOWCardSum0-lblOtherCashSum0-lblOtherCardSum0">
                    <asp:Label ID="lblOOWCashSum0" runat="server" CssClass="fontFamily"></asp:Label>
                </td>
                <td class="td-lbl-10-11-6-13-15-lblIWCnt-lblOOWCashCnt-lblOOWCardCnt-lblOtherCashCnt-lblOtherCardCnt-lblIWSum0-lblOOWCashSum0-lblOOWCardSum0-lblOtherCashSum0-lblOtherCardSum0">
                    <asp:Label ID="lblOOWCardSum0" runat="server" CssClass="fontFamily"></asp:Label>
                </td>
                <td class="td-lbl-10-11-6-13-15-lblIWCnt-lblOOWCashCnt-lblOOWCardCnt-lblOtherCashCnt-lblOtherCardCnt-lblIWSum0-lblOOWCashSum0-lblOOWCardSum0-lblOtherCashSum0-lblOtherCardSum0">
                    <asp:Label ID="lblOtherCashSum0" runat="server" CssClass="fontFamily"></asp:Label>
                </td>
                <td class="td-lbl-10-11-6-13-15-lblIWCnt-lblOOWCashCnt-lblOOWCardCnt-lblOtherCashCnt-lblOtherCardCnt-lblIWSum0-lblOOWCashSum0-lblOOWCardSum0-lblOtherCashSum0-lblOtherCardSum0">
                    <asp:Label ID="lblOtherCardSum0" runat="server" CssClass="fontFamily"></asp:Label>
                </td>
                <td class="td-Blank-23-24-25-26-27-28"></td>
            </tr>
            <tr>
                <td class="td-Blank-16-17-18-19-20-21"></td>
                <td class="td-lbl-8-1">
                    <asp:Label ID="Label1" runat="server" Text="claim amount(notax)" CssClass="fontFamily"></asp:Label>
                </td>
                <td class="lblIWNoTax0-lblOWCashNoTax0-lblOWCardNoTax0-lblOtCashNoTax0-lblOtCardNoTax0">
                    <asp:Label ID="lblIWNoTax0" runat="server" CssClass="fontFamily"></asp:Label>
                </td>
                <td class="lblIWNoTax0-lblOWCashNoTax0-lblOWCardNoTax0-lblOtCashNoTax0-lblOtCardNoTax0">
                    <asp:Label ID="lblOWCashNoTax0" runat="server" CssClass="fontFamily"></asp:Label>
                </td>
                <td class="lblIWNoTax0-lblOWCashNoTax0-lblOWCardNoTax0-lblOtCashNoTax0-lblOtCardNoTax0">
                    <asp:Label ID="lblOWCardNoTax0" runat="server" CssClass="fontFamily"></asp:Label>
                </td>
                <td class="lblIWNoTax0-lblOWCashNoTax0-lblOWCardNoTax0-lblOtCashNoTax0-lblOtCardNoTax0">
                    <asp:Label ID="lblOtCashNoTax0" runat="server" CssClass="fontFamily"></asp:Label>
                </td>
                <td class="lblIWNoTax0-lblOWCashNoTax0-lblOWCardNoTax0-lblOtCashNoTax0-lblOtCardNoTax0">
                    <asp:Label ID="lblOtCardNoTax0" runat="server" CssClass="fontFamily"></asp:Label>
                </td>
                <td class="td-Blank-23-24-25-26-27-28"></td>
            </tr>
            <tr>
                <td class="td-Blank-16-17-18-19-20-21"></td>
                <td class="td-Blank22-lbl-7-9-2">
                    <asp:Label ID="Label2" runat="server" Text="claim amoun(taxin)" CssClass="fontFamily"></asp:Label>
                </td>
                <td class="lblIWSum1-lblOOWCashSum1-lblOOWCardSum1-lblOtherCashSum1-lblOtherCardSum1">
                    <asp:Label ID="lblIWSum1" runat="server" CssClass="fontFamily"></asp:Label>
                </td>
                <td class="lblIWSum1-lblOOWCashSum1-lblOOWCardSum1-lblOtherCashSum1-lblOtherCardSum1">
                    <asp:Label ID="lblOOWCashSum1" runat="server" CssClass="fontFamily"></asp:Label>
                </td>
                <td class="lblIWSum1-lblOOWCashSum1-lblOOWCardSum1-lblOtherCashSum1-lblOtherCardSum1">
                    <asp:Label ID="lblOOWCardSum1" runat="server" CssClass="fontFamily"></asp:Label>
                </td>
                <td class="lblIWSum1-lblOOWCashSum1-lblOOWCardSum1-lblOtherCashSum1-lblOtherCardSum1">
                    <asp:Label ID="lblOtherCashSum1" runat="server" CssClass="fontFamily"></asp:Label>
                </td>
                <td class="lblIWSum1-lblOOWCashSum1-lblOOWCardSum1-lblOtherCashSum1-lblOtherCardSum1">
                    <asp:Label ID="lblOtherCardSum1" runat="server" CssClass="fontFamily"></asp:Label>
                </td>
                <td class="td-Blank-23-24-25-26-27-28"></td>
            </tr>
            <tr>
                <td class="td-Blank29"></td>
                <td class="td-Blank30"></td>
                <td class="td-Blank-31-32-33-34-35-36"></td>
                <td class="td-Blank-31-32-33-34-35-36"></td>
                <td class="td-Blank-31-32-33-34-35-36"></td>
                <td class="td-Blank-31-32-33-34-35-36"></td>
                <td class="td-Blank-31-32-33-34-35-36"></td>
                <td class="td-Blank-31-32-33-34-35-36"></td>
            </tr>
            <tr>
                <td class="td-Blank-37-38-39-40"></td>
                <td class="td-Blank-41-42-43-44">
                    <asp:Label ID="Label27" runat="server" CssClass="lbl-27-28" Text="cash total"></asp:Label>
                </td>
                <td class="td-lblCashTotal-Blank-45-46-47-48-49-50-lblSales-lbl32-lblDisNum-lbl33-lblDisAmount-btnSend">
                    <asp:Label ID="lblCashTotal" runat="server" CssClass="fontFamily"></asp:Label>
                </td>
                <td class="td-Blank46-47">
                    &nbsp;</td>
                <td class="td-lbl30" colspan = "2">
                    <asp:Label ID="Label30" runat="server" Text="Deposit" CssClass="fontFamily"></asp:Label>
                     &nbsp;&nbsp;
                    <asp:Label ID="lblDeposit" runat="server" CssClass="fontFamily"></asp:Label>
                </td>
                <td></td>
                <td class="td-lblCashTotal-Blank-45-46-47-48-49-50-lblSales-lbl32-lblDisNum-lbl33-lblDisAmount-btnSend"></td>
            </tr>
            <tr>
                <td class="td-Blank48"></td>
                <td colspan = "2">
                    <asp:Label ID="Label29" runat="server" CssClass="fontFamily" Text="open deposit + sales(cash)"></asp:Label>
                </td>
                <td class="td-Blank49"colspan = "2">
                    &nbsp;</td>
                <td class="td-Blank50-51-52"></td>
                <td class="td-Blank50-51-52"></td>
                <td class="td-Blank50-51-52"></td>
            </tr>
            <tr>
                <td class="td-Blank-37-38-39-40"></td>
                <td class="td-Blank-41-42-43-44">
                    <asp:Label ID="Label28" runat="server" CssClass="lbl-27-28" Text="sales total"></asp:Label>
                </td>
                <td class="td-lblCashTotal-Blank-45-46-47-48-49-50-lblSales-lbl32-lblDisNum-lbl33-lblDisAmount-btnSend">
                    <asp:Label ID="lblSales" runat="server" CssClass="fontFamily"></asp:Label>
                </td>
                <td class="td-lblCashTotal-Blank-45-46-47-48-49-50-lblSales-lbl32-lblDisNum-lbl33-lblDisAmount-btnSend">&nbsp;</td>
                <td class="td-lblCashTotal-Blank-45-46-47-48-49-50-lblSales-lbl32-lblDisNum-lbl33-lblDisAmount-btnSend">
                    <asp:Label ID="Label32" runat="server" Text="Discount Number" CssClass="fontFamily"></asp:Label>
                </td>
                <td class="td-lblCashTotal-Blank-45-46-47-48-49-50-lblSales-lbl32-lblDisNum-lbl33-lblDisAmount-btnSend">
                    <asp:Label ID="lblDisNum" runat="server" CssClass="fontFamily"></asp:Label>
                </td>
                <td class="td-lblCashTotal-Blank-45-46-47-48-49-50-lblSales-lbl32-lblDisNum-lbl33-lblDisAmount-btnSend"></td>
                <td class="td-lblCashTotal-Blank-45-46-47-48-49-50-lblSales-lbl32-lblDisNum-lbl33-lblDisAmount-btnSend"></td>
            </tr>
            <tr>
                <td class="td-Blank-37-38-39-40"></td>
                <td class="td-Blank-41-42-43-44"></td>
                <td class="td-lblCashTotal-Blank-45-46-47-48-49-50-lblSales-lbl32-lblDisNum-lbl33-lblDisAmount-btnSend"></td>
                <td class="td-Blank-48">
                </td>
                <td class="td-lblCashTotal-Blank-45-46-47-48-49-50-lblSales-lbl32-lblDisNum-lbl33-lblDisAmount-btnSend">
                    <asp:Label ID="Label33" runat="server" Text="Discount Amount" CssClass="fontFamily"></asp:Label>
                </td>
                <td class="td-lblCashTotal-Blank-45-46-47-48-49-50-lblSales-lbl32-lblDisNum-lbl33-lblDisAmount-btnSend">
                    <asp:Label ID="lblDisAmount" runat="server" CssClass="fontFamily"></asp:Label>
                </td>
                <td class="td-lblCashTotal-Blank-45-46-47-48-49-50-lblSales-lbl32-lblDisNum-lbl33-lblDisAmount-btnSend"></td>
            </tr>
                        <tr>
                <td class="td-Blank-37-38-39-40"></td>
                <td class="td-Blank-41-42-43-44"></td>
                <td class="td-Blank46-47">
                            </td>
                <td class="td-lblCashTotal-Blank-45-46-47-48-49-50-lblSales-lbl32-lblDisNum-lbl33-lblDisAmount-btnSend"></td>
                <td class="td-lblCashTotal-Blank-45-46-47-48-49-50-lblSales-lbl32-lblDisNum-lbl33-lblDisAmount-btnSend"></td>
                <td class="td-lblCashTotal-Blank-45-46-47-48-49-50-lblSales-lbl32-lblDisNum-lbl33-lblDisAmount-btnSend"></td>
                <td class="td-lblCashTotal-Blank-45-46-47-48-49-50-lblSales-lbl32-lblDisNum-lbl33-lblDisAmount-btnSend" align = "right"> 
                    <asp:ImageButton ID="btnSend" runat="server" ImageUrl="~/icon/send.png" CssClass="img-btnSend" />
                </td>
                <td class="td-lblCashTotal-Blank-45-46-47-48-49-50-lblSales-lbl32-lblDisNum-lbl33-lblDisAmount-btnSend"></td>
            </tr>
            <tr>
                <td class="td-Blank13-14"></td>
                <td class="td-label24-Blank15"></td>
                <td class="td-lbl25-lblCount-td-Blank-16-17-18-19-20-21-22-23"></td>
                <td class="td-Blank45" colspan = "2">
                    &nbsp;</td>
                <td class="td-lbl25-lblCount-td-Blank-16-17-18-19-20-21-22-23"></td>
                <td class="td-lbl25-lblCount-td-Blank-16-17-18-19-20-21-22-23"></td>
            </tr>
        </table>
    </div>
  </div>
  <div id="dialog" title="message" style="display:none;"> >
        <asp:Label ID="lblMsg" runat="server" Text=""></asp:Label>
  </div>
</asp:Content>
