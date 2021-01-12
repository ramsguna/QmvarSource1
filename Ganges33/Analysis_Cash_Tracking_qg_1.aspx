<%@ Page Language="vb" AutoEventWireup="false" MasterPageFile="~/Site4.Master" CodeBehind="Analysis_Cash_Tracking_qg_1.aspx.vb" Inherits="Ganges33.Analysis_Cash_Tracking_qg_1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
     <script src="http://ajax.googleapis.com/ajax/libs/jquery/1.7.0/jquery.min.js"></script>
    <link href="CSS/Common/Analysis-Cash-Tracking_qg1.css" rel="stylesheet" />
    <link type="text/css" href="http://ajax.googleapis.com/ajax/libs/jqueryui/1/themes/start/jquery-ui.css" rel="stylesheet">
    <script type="text/javascript" src="http://ajax.googleapis.com/ajax/libs/jqueryui/1/jquery-ui.min.js"></script>
       </asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="div-entire-tbl" style="background-image: url('pagewall_2/wall_analysis-2.png')">
      <div class="div-analysis-cash-tracking">
            <br />
            <table class="tbl-analysis-cash-tracking">
              <tr>
                  <td class="td-btn-money" rowspan = "3" >
                      <asp:ImageButton ID="btnMoney" runat="server" Height="108px" ImageUrl="~/icon/money.png" Width="108px" />
                  </td>
                  <td rowspan = "2">
                      &nbsp;</td>
                  <td class="td-lbl-cash-credit" rowspan ="2" >
                      <asp:Label ID="Label5" runat="server" CssClass="lbl-font-xxlarge" Text="Cash &amp; Credit Method Input"></asp:Label>
                  </td>
                  <td class="td-blank1"></td>
                  <td colspan =" 2" class="td-lbl-currentlocation">
                      <asp:Label ID="Label4" runat="server" Text="current location : " CssClass="lbl-font-common"></asp:Label>
                      <asp:Label ID="lblLoc" runat="server"></asp:Label><br />
                      <asp:Label ID="Label2" runat="server" Text="current username : " CssClass="lbl-font-common"></asp:Label>
                      <asp:Label ID="lblName" runat="server"></asp:Label>
                  </td>
              </tr>
              <tr>
                  <td class="td-blank1"></td>
                  <td colspan = "2" class="td-blank1">
                  </td>
              </tr>
              <tr>
                  <td colspan = "2" class="td-lbl-activ-month">
                      <asp:Label ID="Label1" runat="server" CssClass="lbl-font-xxlarge">Activ month</asp:Label>
                  &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                      <asp:Label ID="lblMonNow" runat="server" CssClass="lbl-mon-now"></asp:Label>
                      <asp:DropDownList ID="DropDownActiveMonth" runat="server" cssclass="drpdown-warranty-month-day" AutoPostBack="True" >
                      </asp:DropDownList>
                  </td>
                  <td class="td-lbl-review-report">
                      <asp:Label ID="Label9" runat="server" Text="Review Report " CssClass="lbl-font-common"></asp:Label>
                      <br />
                      <br />
                      <br />
                      <br />
                      <br />
                      <br />
                      <br />
                      <br />
                  </td>
                  <td class="td-lbl-review-report">
                      <asp:Label ID="Label10" runat="server" Text="Select Warranty" CssClass="lbl-font-common"></asp:Label><br />
                      <asp:Label ID="Label3" runat="server" Text="select month" CssClass="lbl-font-common"></asp:Label><br />
                      <asp:Label ID="Label6" runat="server" Text="select day" CssClass="lbl-font-common"></asp:Label>
                      <br />
                      <br />
                      <br />
                      <br />
                  </td>
                  <td class="td-lbl-select-month-date">
                      <asp:DropDownList ID="DropDownWarranty" runat="server" CssClass="drpdown-warranty-month-day" >
                      </asp:DropDownList><br />
                      <asp:DropDownList ID="DropDownMonth" runat="server" CssClass="drpdown-warranty-month-day"  AutoPostBack="True">
                      </asp:DropDownList><br />
                      <asp:DropDownList ID="DropDownDay" runat="server" CssClass="drpdown-warranty-month-day" >
                      </asp:DropDownList><br />
                      <br />
                      <asp:Button ID="btnToday" runat="server" Text="today" CssClass="btn-send-open" />
                  </td>
              </tr>
              <tr>
                  <td class="td-blank1"></td>
                  <td colspan = "3" class="td-drpdown-select-month-date">
                      <asp:Label ID="Label7" runat="server" CssClass="lbl-font-common" Text="select day"></asp:Label>
                      <asp:DropDownList ID="DropDownDay2" runat="server" CssClass="drpdown-warranty-month-day" >
                      </asp:DropDownList>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<asp:Label ID="Label20" runat="server" CssClass="lbl-font-common" Text="select Warranty"></asp:Label>
                      <asp:DropDownList ID="DropDownWarranty2" runat="server" CssClass="drpdown-warranty-month-day"  AutoPostBack="True">
                      </asp:DropDownList>
                      </td>
                  <td align = "right" class="td-blank1">
                      </td>
                  <td class="td-btn-open-send">
                      &nbsp;&nbsp;
                    <asp:ImageButton ID="btnOpen" runat="server"  ImageUrl="~/icon/open.png" CssClass="btn-send-open" />
                  </td>
              </tr>
          </table>

            <table class="tbl-analysis-cash-tracking-tbl">
                <tr>
                    <td class="td-blank2"></td>
                    <td class="td-payment-service-customers-invoice-collected-authorization-card">
                        <asp:Label ID="lblPayment" runat="server" Text="Payment Method" BackColor="#6699FF" CssClass="lbl-font-common" ></asp:Label>
                    </td>
                    <td class="td-payment-service-customers-invoice-collected-authorization-card">
                        <asp:Label ID="lblClaimNo" runat="server" Text="Service Order No." BackColor="#6699FF" CssClass="lbl-font-common" ></asp:Label>
                    </td>
                    <td class="td-payment-service-customers-invoice-collected-authorization-card">
                        <asp:Label ID="lblCustomerName" runat="server" Text="Customer Name" BackColor="#6699FF" CssClass="lbl-font-common" ></asp:Label>
                    </td>
                    <td class="td-payment-service-customers-invoice-collected-authorization-card">
                        <asp:Label ID="lblTotalAmount" runat="server" Text="Invoice Amount" BackColor="#6699FF" CssClass="lbl-font-common" ></asp:Label>
                    </td>
                    <td class="td-payment-service-customers-invoice-collected-authorization-card">
                        <asp:Label ID="lblClaim" runat="server" BackColor="#6699FF" CssClass="lbl-font-common" >Collected Amount </asp:Label>
                    </td>
                    <td class="td-payment-service-customers-invoice-collected-authorization-card">
                        <asp:Label ID="lblPoNo" runat="server" Text="Authorization Code" BackColor="#6699FF" CssClass="lbl-font-common" ></asp:Label>
                    </td>
                    <td class="td-payment-service-customers-invoice-collected-authorization-card">
                        <asp:Label ID="lblCardType" runat="server" Text="Card Type" BackColor="#6699FF" CssClass="lbl-font-common" ></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td class="td-blank2"></td>
                    <td class="td-chkbox-cash-card" >
                        <asp:CheckBox ID="ChkCash" runat="server" AutoPostBack="True" CssClass="lbl-font-common" Text="Cash" />
                    </td>
                    <td class="td-txtbox-service-customer-invoice" rowspan = "2">
                        <asp:TextBox ID="TextClaimNo" runat="server" cssclss="txtbox-service-customer-invoice" ></asp:TextBox>
                    </td>
                    <td class="td-txtbox-service-customer-invoice" rowspan = "2">
                        <asp:TextBox ID="TextCustomerName" runat="server" cssclass="txtbox-service-customer-invoice"></asp:TextBox>
                    </td>
                    <td class="td-txtbox-service-customer-invoice" rowspan = "2">
                        <asp:TextBox ID="TextTotalAmount" runat="server" cssclass="txtbox-service-customer-invoice" AutoPostBack="True"></asp:TextBox>
                    </td>
                    <td class="td-txtbox-service-customer-invoice">
                        <asp:TextBox ID="TextClaim" runat="server" cssclass="txtbox-collect-authorization-card" AutoPostBack="True"></asp:TextBox>
                    </td>
                    <td class="td-blank1">
                       </td>
                    <td class="td-blank1">
                    </td>
                </tr>
                <tr>
                    <td class="td-blank2"></td>
                    <td class="td-chkbox-cash-card" >
                        <asp:CheckBox ID="ChkCredit" runat="server" AutoPostBack="True" CssClass="lbl-font-common" Text="Credit Card" />
                    </td>
                    <td class="td-chkbox-cash-card">
                        <asp:TextBox ID="TextClaimCredit" runat="server" cssclass="txtbox-collect-authorization-card" AutoPostBack="True"></asp:TextBox>
                    </td>
                    <td class="td-chkbox-cash-card" >
                        <asp:TextBox ID="TextPONo" runat="server" cssclass="txtbox-collect-authorization-card" ></asp:TextBox>
                    </td>
                    <td class="td-chkbox-cash-card" >
                        <asp:DropDownList ID="DropDownCard" runat="server" CssClass="txtbox-collect-authorization-card" >
                        </asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <td class="td-blank2" ></td>
                    <td class="td-blank2" >
                        <asp:Label ID="Label24" runat="server" CssClass="lbl-font-common" Text="-Remark-"></asp:Label>
                    </td>
                    <td class="td-blank1" ></td>
                    <td class="td-blank1" ></td>
                    <td class="td-blank1"></td>
                    <td class="td-blank1">
                        </td>
                    <td class="td-blank1" ></td>
                    <td class="td-blank1" ></td>
                </tr>
                <tr>
                    <td class="td-blank2"></td>
                    <td class="td-txt-box" rowspan = "3" colspan = "2">
                        <asp:TextBox ID="TextMessage" runat="server" CssClass="txtbox-remarks"  TextMode="MultiLine"></asp:TextBox>
                    </td>
                    <td class="td-blank1"></td>
                    <td class="td-payment-service-customers-invoice-collected-authorization-card">
                        <asp:CheckBox ID="ChkFullDiscount" runat="server" BackColor="#6699FF" CssClass="lbl-full-cash-discount-balance" Text="Full Discount" AutoPostBack="True" />
                    </td>
                    <td class="td-payment-service-customers-invoice-collected-authorization-card">
                        <asp:Label ID="lblDeposit" runat="server" Text="cash recived" BackColor="#6699FF" CssClass="lbl-full-cash-discount-balance" ></asp:Label>
                    </td>
                    <td class="td-blank1"></td>
                    <td class="td-blank1"></td>
                </tr>
                <tr>
                    <td class="td-blank2"></td>
                    <td class="td-blank1" ></td>
                    <td class="td-payment-service-customers-invoice-collected-authorization-card" >
                        <asp:Label ID="lblDiscount" runat="server" Text="Discount" BackColor="#6699FF" CssClass="lbl-full-cash-discount-balance" ></asp:Label>
                    </td>
                    <td class="td-lbl-climp-blank" >
                        <asp:TextBox ID="TextDeposi" runat="server" AutoPostBack="True"></asp:TextBox>
                    </td>
                    <td class="td-blank1" ></td>
                    <td class="td-blank1" ></td>
                </tr>
                <tr>
                    <td class="td-blank2"></td>
                    <td class="td-blank1"></td>
                    <td class="td-lbl-climp-blank">
                        <asp:TextBox ID="TextDiscount" runat="server" AutoPostBack="True"></asp:TextBox>
                    </td>
                    <td class="td-payment-service-customers-invoice-collected-authorization-card">
                        <asp:Label ID="lblChangeKoumoku" runat="server" Text="Balance" BackColor="#6699FF" CssClass="lbl-full-cash-discount-balance" ></asp:Label>
                    </td>
                    <td class="td-blank1"></td>
                    <td class="td-blank1"></td>
                </tr>
                <tr>
                    <td class="td-blank2"></td>
                    <td class="td-blank2">
                        <asp:Label ID="Label23" runat="server" CssClass="lbl-font-common" Text="-message-"></asp:Label>
                    </td>
                    <td class="td-blank1" ></td>
                    <td class="td-blank1" ></td>
                    <td class="td-lbl-climp-blank">
                        <asp:Label ID="lblDisClaim" runat="server" CssClass="lbl-font-common"></asp:Label>
                    </td>
                    <td class="td-lbl-climp-blank" >
                        <asp:Label ID="lblChange" runat="server" CssClass="lbl-font-common"></asp:Label>
                    </td>
                    <td class="td-blank1" ></td>
                    <td class="td-btn-open-send">
                    <asp:ImageButton ID="btnSend" runat="server"  ImageUrl="~/icon/send.png" CssClass="btn-send-open" />
                    </td>
                </tr>
                <tr>
                   <td></td>
                   <td colspan = "7">
                      <asp:ListBox ID="ListMsg" runat="server" CssClass="txtbox-unbound" ></asp:ListBox>
                   </td>
                </tr>
            </table>

        <asp:Button ID="BtnCancel" runat="server" Text="Button" style="display:none;"/>
        <asp:Button ID="BtnOK" runat="server" Text="Button" style="display:none;" />
      
      </div>
    </div>

    <div id="dialog" title="message" style="display:none;"> >
        <asp:Label ID="lblMsg" runat="server" Text=""></asp:Label>
    </div>

</asp:Content>