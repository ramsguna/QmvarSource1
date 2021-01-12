<%@ Page Language="vb" AutoEventWireup="false" MasterPageFile="~/Site4.Master" CodeBehind="Money_Sales.aspx.vb" Inherits="Ganges33.Money_Sales" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="ajaxToolkit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
        <script src="http://ajax.googleapis.com/ajax/libs/jquery/1.7.0/jquery.min.js"></script>
    <link href="CSS/Common/Money-Sales.css" rel="stylesheet" />
    <link type="text/css" href="http://ajax.googleapis.com/ajax/libs/jqueryui/1/themes/start/jquery-ui.css" rel="stylesheet">
    <script type="text/javascript" src="http://ajax.googleapis.com/ajax/libs/jqueryui/1/jquery-ui.min.js"></script>
       

<style type="text/css">
        .auto-style6 {
            z-index: 1;
            left: 38px;
            top: 122px;
            position: absolute;
            height: 1750px;
            width: 1273px;
            background-size: contain;
            }
            .auto-style7 {
                height: 100%;
                background: rgba(255,255,255,0.8);
                /*opacity: 0.5;*/
            }
            .auto-style145 {
                width: 100%;
                border-collapse: collapse;
                margin-bottom: 0px;
                height: 272px;
            }
            .auto-style153 {
                width: 163px;
                height: 20px;
            }
            .auto-style154 {
                height: 20px;
            }
            .auto-style158 {
                width: 69px;
                height: 23px;
            }
            .auto-style159 {
                width: 163px;
                height: 23px;
                border: 1px solid black;
            }
            .auto-style160 {
                height: 23px;
            }
            .auto-style161 {
                font-family: "Meiryo UI";
                font-size: xx-large;
            }
            .auto-style163 {
                text-align: center;
                height: 34px;
                border-bottom: 1px solid black;
                border-bottom-style:double;
                border-bottom-width:3px; 
            }
            .auto-style164 {
                font-family: "Meiryo UI";
            }
            .auto-style165 {
                width: 163px;
                height: 23px;
                border: 1px solid black;
                text-align: center;
                background-color: #ffff99;
            }
            .auto-style171 {
                font-family: "Meiryo UI";
            }
            .auto-style173 {
                height: 36px;
                width: 163px;
            }
            .auto-style174 {
                height: 36px;
            }
            .auto-style175 {
                height: 36px;
                width: 303px;
                text-align: left;
            }
            .auto-style208 {
                height: 20px;
            }
            .auto-style212 {
                /*height: 37px;*/
                width: 163px;
                height: 46px;
            }
            .auto-style213 {
                /*height: 37px;*/
                text-align: center;
                border-bottom: 3px double black;
                height: 46px;
            }
            .auto-style216 {
                background-color: #00CC00;
                height: 36px;
                width: 163px;
            }
              .auto-style216_ {
                background-color: #ffff99;
                height: 36px;
                width: 163px;
            }
            .auto-style217 {
                background-color: #6699FF;
                height: 36px;
            }
            .auto-style224 {
                width: 303px;
                height: 23px;
                border: 1px solid black;
                background-color: #ffff99;
            }
            .auto-style225{
                width: 163px;
                height: 23px;
                border: 1px solid black;
                background-color: #ffcc99;
            }
            .auto-style235 {
                height: 36px;
                width: 163px;
                text-align: left;
            }
            .auto-style238 {
                width: 69px;
                height: 20px;
            }
            .auto-style239 {
                height: 36px;
                width: 69px;
            }
            .auto-style241 {
                /*height: 37px;*/
                width: 69px;
                height: 46px;
            }
            .auto-style242 {
                width: 303px;
                height: 20px;
            }
            .auto-style243 {
                width: 303px;
                height: 23px;
                border: 1px solid black;
                text-align: center;
                background-color: #ffff99;
            }
            .auto-style244 {
                width: 303px;
                height: 23px;
                border: 1px solid black;
                background-color: #ffcc99;
            }
            .auto-style246 {
                /*height: 37px;*/
                width: 303px;
                height: 46px;
            }
            .auto-style259 {
                height: 36px;
                width: 303px;
            }
            .auto-style266 {
                width: 100%;
                border-collapse: collapse;
            }
            .auto-style268 {
                width: 1312px;
                height: 26px;
            }
            .auto-style269 {
                height: 26px;
            }
            .auto-style270 {
                width: 187px;
                height: 15px;
            }
            .auto-style271 {
                height: 15px;
            }
            .auto-style274 {
                height: 26px;
                width: 145px;
            }
            .auto-style275 {
                height: 15px;
                width: 145px;
            }
            .auto-style276 {
                height: 23px;
                width: 145px;
                border: 1px solid black;
            }
            .auto-style278 {
                height: 26px;
                width: 11px;
            }
            .auto-style279 {
                height: 15px;
                width: 11px;
            }
            .auto-style280 {
                height: 23px;
                width: 11px;
            }
            .auto-style285 {
                width: 1312px;
                height: 31px;
            }
            .auto-style286 {
                height: 31px;
                width: 145px;
            }
            .auto-style287 {
                height: 31px;
                width: 11px;
            }
            .auto-style290 {
                width: 187px;
                height: 23px;
                border: 1px solid black;
                background-color: #ffff99;
            }
            .auto-style291 {
                height: 23px;
                width: 145px;
                border: 1px solid black;
                background-color: #ffcc99;
            }
            .auto-style292 {
                width: 187px;
                height: 23px;
                border: 1px solid black;
                background-color: #ffcc99;
            }
            .auto-style303 {
                height: 23px;
                width: 145px;
                border: 1px solid black;
                background-color: #ffff99;
                text-align: center;
            }
                        
        .auto-style138 {
        font-size: xx-large;
        font-family: "Meiryo UI";
    }
    .auto-style139 {
        font-family: "Meiryo UI";
    }
    
            .auto-style186 {
                font-family: "Meiryo UI";
            }

            .gridviewPager {
            background-color: #fff;
            padding: 2px;
            margin: 2% auto;
        }

            .gridviewPager a {
                margin: auto 1%;
                border-radius: 50%;
                background-color: #545454;
                padding: 5px 10px 5px 10px;
                color: #fff;
                text-decoration: none;
                -o-box-shadow: 1px 1px 1px #111;
                -moz-box-shadow: 1px 1px 1px #111;
                -webkit-box-shadow: 1px 1px 1px #111;
                box-shadow: 1px 1px 1px #111;
            }

                .gridviewPager a:hover {
                    background-color: #337ab7;
                    color: #fff;
                }

            .gridviewPager span {
                background-color: #066091;
                color: #fff;
                -o-box-shadow: 1px 1px 1px #111;
                -moz-box-shadow: 1px 1px 1px #111;
                -webkit-box-shadow: 1px 1px 1px #111;
                box-shadow: 1px 1px 1px #111;
                border-radius: 50%;
                padding: 5px 10px 5px 10px;
            }

            </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
     <div class="auto-style6" style="background-image: url('pagewall_2/wall_money-2.png')">
       <div class="auto-style7">
           <div>
               <table>
<tr>
                  <td colspan = "8"></td>
              </tr>
              
                  <tr>
                    
                      
                    <td colspan = "4" style="padding-left:150px;"><asp:Label ID="Label2" runat="server" CssClass="auto-style138" Text="Sales Report"></asp:Label></td>
                       
                   </tr>
                 <tr>
                  <td colspan = "8"></td>
              </tr>
                <tr>
                    
                     
                    <td colspan = "8"  style="padding-left:150px;">
                        <asp:Label ID="Label217" runat="server" CssClass="auto-style139" Text="Target store"></asp:Label>
                       <asp:DropDownList ID="DropListLocation" runat="server" CssClass="auto-style139" Height="28px" Width="259px">
                       </asp:DropDownList>

                    </td>
                    <td></td>
                  
                   </tr>
                 <tr>
                  <td colspan = "8"></td>
              </tr>
               <tr>
                 
                   <td colspan = "8" style="padding-left:150px;">
                       <asp:Label ID="Label219" runat="server" CssClass="auto-style139" Text="Target Date"></asp:Label>
                    
                       <asp:TextBox ID="TextCompleteDateFrom" runat="server"  CssClass="auto-style139" Height="16px" Width="133px"></asp:TextBox>
                        <ajaxToolkit:CalendarExtender ID="TextCompleteDateFrom_CalendarExtender" Format="yyyy/MM/dd" runat="server" BehaviorID="TextCompleteDateFrom_CalendarExtender" TargetControlID="TextCompleteDateFrom" PopupPosition="Left">
                    </ajaxToolkit:CalendarExtender>
                       <asp:Label ID="Label218" runat="server" CssClass="auto-style139" Text="(yyyy/mm/dd)"></asp:Label>
                   &nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<asp:ImageButton ID="btnSearch" runat="server" Height="29px" ImageUrl="~/icon/search.png" Width="81px" CssClass="auto-style186" />
                   </td>
                   
               </tr>
                 <tr>
                  <td colspan = "8" style="padding-top:20px;"></td>
              </tr>

               </table>

           </div>

           <div id="divHead" runat="server">
           <table class="auto-style145" id="tblHead" runat="server">
              <tr>
                  <td colspan = "8"></td>
              </tr>
            
              
               <tr>
                   <td class="auto-style158"></td>
                   <td class="auto-style243"></td>
                   <td class="auto-style165">
                       <asp:Label ID="Label3" runat="server" CssClass="auto-style164" Text="IW"></asp:Label>
                   </td>
                   <td class="auto-style165">
                       <asp:Label ID="Label4" runat="server" CssClass="auto-style164" Text="OOW(cash)"></asp:Label>
                   </td>
                   <td class="auto-style165">
                       <asp:Label ID="Label5" runat="server" CssClass="auto-style164" Text="OOW(card)"></asp:Label>
                   </td>
                   <td class="auto-style165">
                       <asp:Label ID="Label6" runat="server" CssClass="auto-style164" Text="Other(cash)"></asp:Label>
                   </td>
                   <td class="auto-style165">
                       <asp:Label ID="Label7" runat="server" CssClass="auto-style164" Text="Other(card)"></asp:Label>
                   </td>
                   <td class="auto-style160"></td>
                   <td class="auto-style160"></td>
               </tr>
               <tr>
                   <td class="auto-style158"></td>
                   <td class="auto-style224">
                       <asp:Label ID="Label8" runat="server" CssClass="auto-style164" Text="Number"></asp:Label>
                   </td>
                   <td class="auto-style159">
                    <asp:Label ID="lblIWCnt" runat="server" CssClass="auto-style171"></asp:Label>
                   </td>
                   <td class="auto-style159">
                    <asp:Label ID="lblOOWCashCnt" runat="server" CssClass="auto-style171"></asp:Label>
                   </td>
                   <td class="auto-style159">
                    <asp:Label ID="lblOOWCardCnt" runat="server" CssClass="auto-style171"></asp:Label>
                   </td>
                   <td class="auto-style159">
                    <asp:Label ID="lblOtherCashCnt" runat="server" CssClass="auto-style171"></asp:Label>
                   </td>
                   <td class="auto-style159">
                    <asp:Label ID="lblOtherCardCnt" runat="server" CssClass="auto-style171"></asp:Label>
                   </td>
                   <td class="auto-style160"></td>
                   <td class="auto-style160"></td>
               </tr>
               <tr>
                   <td class="auto-style158"></td>
                   <td class="auto-style244">
                       <asp:Label ID="Label9" runat="server" CssClass="auto-style164" Text="Amount(notax)"></asp:Label>
                   </td>
                   <td class="auto-style225">
                    <asp:Label ID="lblIWNoTax" runat="server" CssClass="auto-style171"></asp:Label>
                   </td>
                   <td class="auto-style225">
                    <asp:Label ID="lblOWCashNoTax" runat="server" CssClass="auto-style171"></asp:Label>
                   </td>
                   <td class="auto-style225">
                    <asp:Label ID="lblOWCardNoTax" runat="server" CssClass="auto-style171"></asp:Label>
                   </td>
                   <td class="auto-style225">
                    <asp:Label ID="lblOtCashNoTax" runat="server" CssClass="auto-style171"></asp:Label>
                   </td>
                   <td class="auto-style225">
                    <asp:Label ID="lblOtCardNoTax" runat="server" CssClass="auto-style171"></asp:Label>
                   </td>
                   <td class="auto-style160"></td>
                   <td class="auto-style160"></td>
               </tr>
               <tr>
                   <td class="auto-style158"></td>
                   <td class="auto-style224">
                       <asp:Label ID="Label10" runat="server" CssClass="auto-style164" Text="Amount(taxin)"></asp:Label>
                   </td>
                   <td class="auto-style159">
                    <asp:Label ID="lblIWSum" runat="server" CssClass="auto-style171"></asp:Label>
                   </td>
                   <td class="auto-style159">
                    <asp:Label ID="lblOOWCashSum" runat="server" CssClass="auto-style171"></asp:Label>
                   </td>
                   <td class="auto-style159">
                    <asp:Label ID="lblOOWCardSum" runat="server" CssClass="auto-style171"></asp:Label>
                   </td>
                   <td class="auto-style159">
                    <asp:Label ID="lblOtherCashSum" runat="server" CssClass="auto-style171"></asp:Label>
                   </td>
                   <td class="auto-style159">
                    <asp:Label ID="lblOtherCardSum" runat="server" CssClass="auto-style171"></asp:Label>
                   </td>
                   <td class="auto-style160"></td>
                   <td class="auto-style160"></td>
               </tr>
               <tr>
                   <td class="auto-style158"></td>
                   <td class="auto-style244">
                    <asp:Label ID="Label76" runat="server" Text="Claim Amount(notax)" CssClass="auto-style171"></asp:Label>
                   </td>
                   <td class="auto-style225">
                    <asp:Label ID="lblIWNoTaxClaim" runat="server" CssClass="auto-style171"></asp:Label>
                   </td>
                   <td class="auto-style225">
                    <asp:Label ID="lblOWCashNoTaxClaim" runat="server" CssClass="auto-style171"></asp:Label>
                   </td>
                   <td class="auto-style225">
                    <asp:Label ID="lblOWCardNoTaxClaim" runat="server" CssClass="auto-style171"></asp:Label>
                   </td>
                   <td class="auto-style225">
                    <asp:Label ID="lblOtCashNoTaxClaim" runat="server" CssClass="auto-style171"></asp:Label>
                   </td>
                   <td class="auto-style225">
                    <asp:Label ID="lblOtCardNoTaxClaim" runat="server" CssClass="auto-style171"></asp:Label>
                   </td>
                   <td class="auto-style160"></td>
                   <td class="auto-style160"></td>
               </tr>
               <tr>
                   <td class="auto-style158"></td>
                   <td class="auto-style224">
                    <asp:Label ID="Label77" runat="server" Text="Claim Amount(taxin)" CssClass="auto-style171"></asp:Label>
                   </td>
                   <td class="auto-style159">
                    <asp:Label ID="lblIWSumClaim" runat="server" CssClass="auto-style171"></asp:Label>
                   </td>
                   <td class="auto-style159">
                    <asp:Label ID="lblOOWCashSumClaim" runat="server" CssClass="auto-style171"></asp:Label>
                   </td>
                   <td class="auto-style159">
                    <asp:Label ID="lblOOWCardSumClaim" runat="server" CssClass="auto-style171"></asp:Label>
                   </td>
                   <td class="auto-style159">
                    <asp:Label ID="lblOtherCashSumClaim" runat="server" CssClass="auto-style171"></asp:Label>
                   </td>
                   <td class="auto-style159">
                    <asp:Label ID="lblOtherCardSumClaim" runat="server" CssClass="auto-style171"></asp:Label>
                   </td>
                   <td class="auto-style160"></td>
                   <td class="auto-style160"></td>
               </tr>
               <tr>
                   <td class="auto-style238"></td>
                   <td class="auto-style242"></td>
                   <td class="auto-style153"></td>
                   <td class="auto-style153"></td>
                   <td class="auto-style153"></td>
                   <td class="auto-style153"></td>
                   <td class="auto-style153"></td>
                   <td class="auto-style154"></td>
                   <td class="auto-style154"></td>
               </tr>
               <tr>
                   <td class="auto-style239"></td>
                   <td class="auto-style175">
                       <b><asp:Label ID="Label20" runat="server" CssClass="auto-style170" Text="Cash Total "></asp:Label></b><br />open deposit + sales(cash) + other(cash) - discount - bank deposit
                   </td>
                   <td class="auto-style216">
                    <asp:Label ID="lblCashTotal" runat="server" CssClass="auto-style171"></asp:Label>
                    </td>
                   <td class="auto-style173"></td>
                   <td class="auto-style235">
                       <asp:Label ID="Label21" runat="server" CssClass="auto-style164" Text="Sales Total"></asp:Label>
                   </td>
                   <td class="auto-style217">
                    <asp:Label ID="lblSales" runat="server" CssClass="auto-style171"></asp:Label>
                   </td>
                   <td class="auto-style173"></td>
                   <td class="auto-style174"></td>
                   <td class="auto-style174"></td>
               </tr>
               <tr>
                   <td class="auto-style239"></td>
                   <td class="auto-style259">
                       <asp:Label ID="Label220" runat="server" CssClass="auto-style164" Text="Card Total"></asp:Label>
                   </td>
                    <td class="auto-style216_"> 
                    <asp:Label ID="lblCardTotal" runat="server" CssClass="auto-style171"></asp:Label>
                   </td>
                   <td class="auto-style173"></td>
                   <td>
                       &nbsp;</td>
                   <td>
                       &nbsp;</td>
                   <td></td>
                   <td colspan = "2" class="auto-style174" align = "right">
                       &nbsp;</td>
               </tr>
               <tr>
                   <td class="auto-style239"></td>
                   <td class="auto-style259">
                       <asp:Label ID="Label82" runat="server" CssClass="auto-style164" Text="Customer Deposit"></asp:Label></td>
                   <td class="auto-style173">
                       <asp:Label ID="lblDepositCusto" runat="server" CssClass="auto-style171"></asp:Label>
                   </td>
                   <td class="auto-style173"></td>
                   <td class="auto-style173">
                       <asp:Label ID="Label84" runat="server" CssClass="auto-style164" Text="Number of Custody"></asp:Label>
                   </td>
                   <td class="auto-style174">
                       <asp:Label ID="lblNumCusto" runat="server" CssClass="auto-style164"></asp:Label>
                   </td>
                   <td class="auto-style174"></td>
                   <td class="auto-style174"></td>
                   <td class="auto-style174"></td>
               </tr>
               <tr>
                   <td class="auto-style239"></td>
                   <td class="auto-style259">
                       <asp:Label ID="Label22" runat="server" CssClass="auto-style164" Text="Bank Deposit"></asp:Label>
                   </td>
                   <td class="auto-style173">
                       <asp:Label ID="lblDeposit" runat="server" CssClass="auto-style164"></asp:Label>
                   </td>
                   <td class="auto-style173"></td>
                   <td class="auto-style173">
                       &nbsp;</td>
                   <td class="auto-style174">&nbsp;</td>
                   <td class="auto-style174"></td>
                   <td class="auto-style174"></td>
                   <td class="auto-style174"></td>
               </tr>
             
              
           </table>
           
           </div>
           <div id="divDetails" runat="server">
               <asp:ScriptManager ID="ScriptManager2" runat="server"></asp:ScriptManager>
  <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
           <table class="auto-style266">
                 <tr>
                   <td class="auto-style241"></td>
                   <td class="auto-style246"></td>
                   <td class="auto-style213" colspan = "4">
                       <asp:Label ID="Label23" runat="server" CssClass="auto-style161" Text="Details"></asp:Label>
                   </td>
                   <td class="auto-style212"></td>
                   <td class="auto-style212">
                       &nbsp;</td>
                   <td class="auto-style212">
                       &nbsp;</td>
               </tr>
               <tr>
                   <td colspan = "8">&nbsp;
                       </td>
                   
               </tr>
           
               <asp:GridView ID="grvSalesReport" runat="server" AllowPaging="true" PageSize="5" AutoGenerateColumns="False"
                                        EmptyDataText="Data Is Empty" BackColor="White" BorderColor="Black" CssClass="mGrid"
                                        BorderStyle="Double" OnPageIndexChanging="grvSalesReport_PageIndexChanging" Visible="true"
                                        Width="100%">
                  
                    <PagerStyle CssClass="gridviewPager" />

                                        <Columns>
                                            <asp:TemplateField HeaderText="">
                                                <ItemStyle Width="150px" HorizontalAlign="left" VerticalAlign="Top" />
                                                <ItemTemplate>
                                                       <asp:Label ID="lblBranchCode" Visible="false" runat="server" Width="150px" Text='<%# Eval("CodeValue")  %>'></asp:Label>
                                                       <asp:Label ID="lblBranchName" Visible="false"  runat="server" Width="150px" Text='<%# Eval("CodeDispValue")  %>'></asp:Label>
                                                        <table>
               <tr>
                   <td>
                       <asp:Label ID="lblLocation" runat="server" CssClass="auto-style164" Text='<%# Eval("CodeDispValue")  %>'  ></asp:Label>
                   </td>
                   <td colspan = "4">
                       &nbsp;</td>
                   <td></td>
                   <td></td>
                   <td></td>
               </tr>
               <tr>
                   <td class="auto-style270"></td>
                   <td class="auto-style275"></td>
                   <td class="auto-style275"></td>
                   <td class="auto-style275"></td>
                   <td class="auto-style275"></td>
                   <td class="auto-style275"></td>
                   <td class="auto-style279"></td>
                   <td class="auto-style271" rowspan = "12">
                       <asp:ListBox ID="lstMsg" runat="server" Height="297px" Width="297px"></asp:ListBox>
                   </td>
               </tr>
               <tr>
                   <td class="auto-style290"></td>
                   <td class="auto-style303">
                       <asp:Label ID="Label74" runat="server" CssClass="auto-style164" Text="IW"></asp:Label>
                   </td>
                   <td class="auto-style303">
                       <asp:Label ID="Label75" runat="server" CssClass="auto-style164" Text="OOW(cash)"></asp:Label>
                   </td>
                   <td class="auto-style303">
                       <asp:Label ID="Label78" runat="server" CssClass="auto-style164" Text="OOW(card)"></asp:Label>
                   </td>
                   <td class="auto-style303">
                       <asp:Label ID="Label79" runat="server" CssClass="auto-style164" Text="Other(cash)"></asp:Label>
                   </td>
                   <td class="auto-style303">
                       <asp:Label ID="Label80" runat="server" CssClass="auto-style164" Text="Other(card)"></asp:Label>
                   </td>
                   <td class="auto-style280"></td>
               </tr>
               <tr>
                   <td class="auto-style290">
                       <asp:Label ID="Label81" runat="server" CssClass="auto-style164" Text="Number"></asp:Label>
                   </td>
                   <td class="auto-style276">
                    <asp:Label ID="lblIWCnt1" runat="server" CssClass="auto-style171"></asp:Label>
                   </td>
                   <td class="auto-style276">
                    <asp:Label ID="lblOOWCashCnt1" runat="server" CssClass="auto-style171"></asp:Label>
                   </td>
                   <td class="auto-style276">
                    <asp:Label ID="lblOOWCardCnt1" runat="server" CssClass="auto-style171"></asp:Label>
                   </td>
                   <td class="auto-style276">
                    <asp:Label ID="lblOtherCashCnt1" runat="server" CssClass="auto-style171"></asp:Label>
                   </td>
                   <td class="auto-style276">
                    <asp:Label ID="lblOtherCardCnt1" runat="server" CssClass="auto-style171"></asp:Label>
                   </td>
                   <td class="auto-style280"></td>
               </tr>
               <tr>
                   <td class="auto-style292">
                       <asp:Label ID="Label91" runat="server" CssClass="auto-style164" Text="Amount(notax)"></asp:Label>
                   </td>
                   <td class="auto-style291">
                    <asp:Label ID="lblIWNoTax1" runat="server" CssClass="auto-style171"></asp:Label>
                   </td>
                   <td class="auto-style291">
                    <asp:Label ID="lblOWCashNoTax1" runat="server" CssClass="auto-style171"></asp:Label>
                   </td>
                   <td class="auto-style291">
                    <asp:Label ID="lblOWCardNoTax1" runat="server" CssClass="auto-style171"></asp:Label>
                   </td>
                   <td class="auto-style291">
                    <asp:Label ID="lblOtCashNoTax1" runat="server" CssClass="auto-style171"></asp:Label>
                   </td>
                   <td class="auto-style291">
                    <asp:Label ID="lblOtCardNoTax1" runat="server" CssClass="auto-style171"></asp:Label>
                   </td>
                   <td class="auto-style280"></td>
                  </tr>
               <tr>
                   <td class="auto-style290">
                       <asp:Label ID="Label97" runat="server" CssClass="auto-style164" Text="Amount(taxin)"></asp:Label>
                   </td>
                   <td class="auto-style276">
                    <asp:Label ID="lblIWSum1" runat="server" CssClass="auto-style171"></asp:Label>
                   </td>
                   <td class="auto-style276">
                    <asp:Label ID="lblOOWCashSum1" runat="server" CssClass="auto-style171"></asp:Label>
                   </td>
                   <td class="auto-style276">
                    <asp:Label ID="lblOOWCardSum1" runat="server" CssClass="auto-style171"></asp:Label>
                   </td>
                   <td class="auto-style276">
                    <asp:Label ID="lblOtherCashSum1" runat="server" CssClass="auto-style171"></asp:Label>
                   </td>
                   <td class="auto-style276">
                    <asp:Label ID="lblOtherCardSum1" runat="server" CssClass="auto-style171"></asp:Label>
                   </td>
                   <td class="auto-style280"></td>
               </tr>
               <tr>
                   <td class="auto-style292">
                    <asp:Label ID="Label103" runat="server" Text="Claim Amount(notax)" CssClass="auto-style171"></asp:Label>
                   </td>
                   <td class="auto-style291">
                    <asp:Label ID="lblIWNoTaxClaim1" runat="server" CssClass="auto-style171"></asp:Label>
                   </td>
                   <td class="auto-style291">
                    <asp:Label ID="lblOWCashNoTaxClaim1" runat="server" CssClass="auto-style171"></asp:Label>
                   </td>
                   <td class="auto-style291">
                    <asp:Label ID="lblOWCardNoTaxClaim1" runat="server" CssClass="auto-style171"></asp:Label>
                   </td>
                   <td class="auto-style291">
                    <asp:Label ID="lblOtCashNoTaxClaim1" runat="server" CssClass="auto-style171"></asp:Label>
                   </td>
                   <td class="auto-style291">
                    <asp:Label ID="lblOtCardNoTaxClaim1" runat="server" CssClass="auto-style171"></asp:Label>
                   </td>
                   <td class="auto-style280"></td>
                 </tr>
               <tr>
                   <td class="auto-style290">
                    <asp:Label ID="Label109" runat="server" Text="Claim Amount(taxin)" CssClass="auto-style171"></asp:Label>
                   </td>
                   <td class="auto-style276">
                    <asp:Label ID="lblIWSumClaim1" runat="server" CssClass="auto-style171"></asp:Label>
                   </td>
                   <td class="auto-style276">
                    <asp:Label ID="lblOOWCashSumClaim1" runat="server" CssClass="auto-style171"></asp:Label>
                   </td>
                   <td class="auto-style276">
                    <asp:Label ID="lblOOWCardSumClaim1" runat="server" CssClass="auto-style171"></asp:Label>
                   </td>
                   <td class="auto-style276">
                    <asp:Label ID="lblOtherCashSumClaim1" runat="server" CssClass="auto-style171"></asp:Label>
                   </td>
                   <td class="auto-style276">
                    <asp:Label ID="lblOtherCardSumClaim1" runat="server" CssClass="auto-style171"></asp:Label>
                   </td>
                   <td class="auto-style280"></td>
                  </tr>
               <tr>
                   <td></td>
                   <td></td>
                   <td></td>
                   <td></td>
                   <td></td>
                   <td></td>
                   <td></td>
                </tr>
               <tr>
                   <td>
                       <asp:Label ID="Label115" runat="server" Text="Cash Total"></asp:Label>
                   </td>
                   <td>
                    <asp:Label ID="lblCashTotal1" runat="server" ></asp:Label>
                    </td>
                   <td></td>
                   <td>
                       <asp:Label ID="Label117" runat="server"  Text="Sales Total"></asp:Label>
                   </td>
                   <td>
                    <asp:Label ID="lblSales1" runat="server" ></asp:Label>
                   </td>
                   <td></td>
                   <td></td>
                  </tr>
               <tr>
                   <td><asp:Label ID="Label12" runat="server" Text="Card Total"></asp:Label>
                       </td>
                   <td>
                       <asp:Label ID="lblCardTotal1" runat="server" ></asp:Label>
                   </td>
                   <td></td>
                   <td></td>
                   <td>
                      
                   </td>
                   <td></td>
                   <td></td>
                 </tr>
               <tr>
                   <td><asp:Label ID="Label119" runat="server" Text="Customer Deposit"></asp:Label></td>
                   <td><asp:Label ID="lblDepositCusto1" runat="server" ></asp:Label></td>
                   <td></td>
                   <td  colspan = "2">
                        <asp:Label ID="Label121" runat="server"  Text="Number of Custody"></asp:Label>&nbsp;&nbsp;
                       <asp:Label ID="lblNumCusto1" runat="server" ></asp:Label>

                   </td>
                   <td></td>
                   <td></td>
                   <td></td>
               </tr>
               <tr>
                <td>
                       <asp:Label ID="Label11" runat="server"  Text="Bank Deposit"></asp:Label></td>
                   <td>
                       <asp:Label ID="lblDeposit1" runat="server" ></asp:Label>
                   </td>
                   <td></td>
                   <td></td>
                   <td></td>
                   <td></td>
                   <td></td>
                </tr>
 <tr>
                   <td></td>
                   <td></td>
                   <td></td>
                   <td></td>
                   <td></td>
                   <td></td>
                   <td></td>
                </tr>
           </table>

                                                </ItemTemplate>
                                            </asp:TemplateField>
                                          
                                        </Columns>
                                        <PagerSettings FirstPageText="" LastPageText="" NextPageText="" PreviousPageText=""
                                            Position="Top"></PagerSettings>
                                        <HeaderStyle BackColor="LightBlue" Font-Bold="True" ForeColor="Black" />
                                        <PagerStyle HorizontalAlign="Left" />
                                    </asp:GridView>
               <tr>
                   <td class="auto-style268">
                       &nbsp;</td>
                   <td colspan = "4" class="auto-style269">
                       &nbsp;</td>
                   <td class="auto-style274">&nbsp;</td>
                   <td class="auto-style278">&nbsp;</td>
                   <td class="auto-style269">&nbsp;</td>
               </tr>
          
               <tr>
                   <td class="auto-style285"></td>
                   <td class="auto-style286"></td>
                   <td class="auto-style286"></td>
                   <td class="auto-style286"></td>
                   <td class="auto-style286"></td>
                   <td class="auto-style286"></td>
                   <td class="auto-style287"></td>
                </tr>
           </table>

            </ContentTemplate>

    </asp:UpdatePanel>
               </div>


       </div>
     </div>

      <div id="dialog" title="message" style="display:none;"> 
        <asp:Label ID="lblMsg" runat="server" Text=""></asp:Label>
     </div>

</asp:Content>