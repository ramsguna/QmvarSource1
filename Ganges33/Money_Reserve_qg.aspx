<%@ Page Language="vb" AutoEventWireup="false" MasterPageFile="~/Site4.Master" CodeBehind="Money_Reserve_qg.aspx.vb" Inherits="Ganges33.Money_Reserve_qg" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script src="http://ajax.googleapis.com/ajax/libs/jquery/1.7.0/jquery.min.js"></script>
    <link href="CSS/Common/Money-reverse-qg.css" rel="stylesheet" />
    <link type="text/css" href="http://ajax.googleapis.com/ajax/libs/jqueryui/1/themes/start/jquery-ui.css" rel="stylesheet">
    <script type="text/javascript" src="http://ajax.googleapis.com/ajax/libs/jqueryui/1/jquery-ui.min.js"></script>
     
      </asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
     <asp:ScriptManager ID="smContent2" runat="server">
    </asp:ScriptManager>
    <div class="div-entire-tbl" style="background-image: url('pagewall_2/wall_money-2.png')">
      <div class="div-money-reserve-tbl">
        <asp:Label ID="Label1" runat="server" CssClass="lbl-status-open-credit-checkout" Text="Status: "></asp:Label>
        <asp:Label ID="lblDispStatu" runat="server" CssClass="lbl-status-open-credit-checkout"></asp:Label>
        <br />
        <asp:DropDownList ID="DropListStatus" runat="server"  CssClass="drpdown-status"></asp:DropDownList>
        <br /> 
            <table class="tbl-money-reserve">
              <tr>
                  <td class="td-bill">
                      <asp:Label ID="Label2" runat="server" CssClass="lbl-font-common" Text="Bill"></asp:Label>
                  </td>
                  <td class="td-blank1"></td>
                  <td class="td-blank2"></td>
                  <td class="td-blank3"></td>
                  <td class="td-blank4"></td>
              </tr>
              <tr>
                  <td class="td-blank5"></td>
                  <td class="td-rupee">
                      <asp:Label ID="Label3" runat="server" CssClass="lbl-font-common" Text="2000"></asp:Label>
                  </td>
                  <td class="td-txtbox-rupee">
                      <asp:TextBox ID="TextM2000Cnt" runat="server" CssClass="txtbox-rupee1" ></asp:TextBox>
                    
                  </td>
                  <td class="td-txtbox2-rupee">
                      <asp:TextBox ID="TextM2000Sum" runat="server" CssClass="txtbox-common" ></asp:TextBox>
                  </td>
                  <td class="td-lbl-open-credit-checkout">
                    <strong class="strong-lbl-open-credit-checkout">
                    <asp:Label ID="Label18" runat="server" CssClass="lbl-status-open-credit-checkout" Text="Open Deposit"></asp:Label>
                    </strong>

                  </td>
              </tr>
              <tr>
                  <td class="td-blank5"></td>
                  <td class="td-rupee">
                      <asp:Label ID="Label4" runat="server" CssClass="lbl-font-common" Text="500"></asp:Label>
                  </td>
                  <td class="td-txtbox-rupee">
                      <asp:TextBox ID="TextM500Cnt" runat="server" CssClass="txtbox-rupee1" ></asp:TextBox>
                  </td>
                  <td class="td-txtbox2-rupee">
                      <asp:TextBox ID="TextM500Sum" runat="server" CssClass="txtbox-common"></asp:TextBox>
                  </td>
                  <td class="td-lbl-open-credit-checkout">
                    <asp:Label ID="lblResult" runat="server" CssClass="lbl-large-font" ForeColor="Black" ></asp:Label>&nbsp;&nbsp;
                    <asp:Label ID="lblName" runat="server" CssClass="lbl-font-common"></asp:Label>&nbsp;&nbsp;
                    <asp:Label ID="lblTime" runat="server" CssClass="lbl-font-common"></asp:Label>&nbsp;&nbsp;
                    <asp:Label ID="lblRegiDeposi" runat="server" CssClass="lbl-font-common"></asp:Label>&nbsp;&nbsp;
                    <asp:Label ID="lblmistake0" runat="server" CssClass="lbl-font-common">mistake : </asp:Label>
                    <asp:Label ID="lblmistakeOpen" runat="server" CssClass="lbl-font-common"></asp:Label>&nbsp;&nbsp;
                      <asp:Label ID="lblConfirm" runat="server" CssClass="lbl-font-common"></asp:Label>
                      <br />
                  </td>
              </tr>
              <tr>
                  <td class="td-blank5"></td>
                  <td class="td-rupee">
                      <asp:Label ID="Label5" runat="server" CssClass="lbl-font-common" Text="200"></asp:Label>
                  </td>
                  <td class="td-txtbox-rupee">
                      <asp:TextBox ID="TextM200Cnt" runat="server" CssClass="txtbox-rupee1"></asp:TextBox>
                  </td>
                  <td class="td-txtbox2-rupee">
                      <asp:TextBox ID="TextM200Sum" runat="server" CssClass="txtbox-common"></asp:TextBox>
                  </td>
                  <td class="td-lbl-open-credit-checkout">
                      <asp:Label ID="lblConfirm2" runat="server" CssClass="lbl-font-common"></asp:Label>
                  </td>
              </tr>
              <tr>
                  <td class="td-blank5"></td>
                  <td class="td-rupee">
                      <asp:Label ID="Label6" runat="server" CssClass="lbl-font-common" Text="100"></asp:Label>
                  </td>
                  <td class="td-txtbox-rupee">
                      <asp:TextBox ID="TextM100Cnt" runat="server" CssClass="txtbox-rupee1" ></asp:TextBox>
                  </td>
                  <td class="td-txtbox2-rupee">
                      <asp:TextBox ID="TextM100Sum" runat="server" CssClass="txtbox-common"></asp:TextBox>
                  </td>
                  <td class="td-lbl-open-credit-checkout">
                    <strong class="strong-lbl-open-credit-checkout">
                    <asp:Label ID="Label19" runat="server" CssClass="lbl-status-open-credit-checkout" Text="Credit Report"></asp:Label>
                    </strong>
                  </td>
              </tr>
              <tr>
                  <td class="td-blank5"></td>
                  <td class="td-rupee">
                      <asp:Label ID="Label7" runat="server" CssClass="lbl-font-common" Text="50"></asp:Label>
                  </td>
                  <td class="td-txtbox-rupee">
                      <asp:TextBox ID="TextM50Cnt" runat="server" CssClass="txtbox-rupee1" ></asp:TextBox>
                  </td>
                  <td class="td-txtbox2-rupee">
                      <asp:TextBox ID="TextM50Sum" runat="server" CssClass="txtbox-common"></asp:TextBox>
                  </td>
                  <td class="td-lbl-open-credit-checkout">
                    <asp:Label ID="lblResultIns1" runat="server" CssClass="lbl-large-font"></asp:Label>&nbsp;&nbsp;
                    <asp:Label ID="lblName0" runat="server" CssClass="lbl-font-common"></asp:Label>&nbsp;&nbsp;
                    <asp:Label ID="lblTime0" runat="server" CssClass="lbl-font-common"></asp:Label>&nbsp;&nbsp;
                    <asp:Label ID="lblDiff0" runat="server" CssClass="lbl-font-common"></asp:Label>&nbsp;&nbsp;
                    <asp:Label ID="lblmistake1" runat="server" CssClass="lbl-font-common">mistake : </asp:Label>
                    <asp:Label ID="lblmistakeIns1" runat="server" CssClass="lbl-font-common"></asp:Label>&nbsp;&nbsp;
                    <asp:Label ID="lblCIns1" runat="server" CssClass="lbl-font-common"></asp:Label>&nbsp;&nbsp;
                    <asp:Label ID="lblCIns1_" runat="server" CssClass="lbl-font-common"></asp:Label>
                  </td>
              </tr>
              <tr>
                  <td class="td-blank5"></td>
                  <td class="td-rupee">
                      <asp:Label ID="Label8" runat="server" CssClass="lbl-font-common" Text="20"></asp:Label>
                  </td>
                  <td class="td-txtbox-rupee">
                      <asp:TextBox ID="TextM20Cnt" runat="server" CssClass="txtbox-rupee1" ></asp:TextBox>
                  </td>
                  <td class="td-txtbox2-rupee">
                      <asp:TextBox ID="TextM20Sum" runat="server" CssClass="txtbox-common"></asp:TextBox>
                  </td>
                  <td class="td-lbl-open-credit-checkout">
                    <asp:Label ID="lblResultIns2" runat="server" CssClass="lbl-large-font"></asp:Label>&nbsp;&nbsp;
                    <asp:Label ID="lblName1" runat="server" CssClass="lbl-font-common"></asp:Label>&nbsp;&nbsp;
                    <asp:Label ID="lblTime1" runat="server" CssClass="lbl-font-common"></asp:Label>&nbsp;&nbsp;
                    <asp:Label ID="lblDiff1" runat="server" CssClass="lbl-font-common"></asp:Label>&nbsp;&nbsp;
                    <asp:Label ID="lblmistake2" runat="server" CssClass="lbl-font-common">mistake : </asp:Label>
                    <asp:Label ID="lblmistakeIns2" runat="server" CssClass="lbl-font-common"></asp:Label>&nbsp;&nbsp;
                    <asp:Label ID="lblCIns2" runat="server" CssClass="lbl-font-common"></asp:Label>
                  &nbsp;&nbsp;
                    <asp:Label ID="lblCIns2_" runat="server" CssClass="lbl-font-common"></asp:Label>
                  </td>
              </tr>
              <tr>
                  <td class="td-blank5"></td>
                  <td class="td-rupee">
                      <asp:Label ID="Label9" runat="server" CssClass="lbl-font-common" Text="10"></asp:Label>
                  </td>
                  <td class="td-txtbox-rupee">
                      <asp:TextBox ID="TextM10Cnt" runat="server" CssClass="txtbox-rupee1" ></asp:TextBox>
                  </td>
                  <td class="td-txtbox2-rupee">
                      <asp:TextBox ID="TextM10Sum" runat="server" CssClass="txtbox-common"></asp:TextBox>
                  </td>
                  <td class="td-lbl-open-credit-checkout">
                    <asp:Label ID="lblResultIns3" runat="server" CssClass="lbl-large-font"></asp:Label>&nbsp;&nbsp;
                    <asp:Label ID="lblName2" runat="server" CssClass="lbl-font-common"></asp:Label>&nbsp;&nbsp;
                    <asp:Label ID="lblTime2" runat="server" CssClass="lbl-font-common"></asp:Label>&nbsp;&nbsp;
                    <asp:Label ID="lblDiff2" runat="server" CssClass="lbl-font-common"></asp:Label>&nbsp;&nbsp;
                    <asp:Label ID="lblmistake3" runat="server" CssClass="lbl-font-common">mistake : </asp:Label>
                    <asp:Label ID="lblmistakeIns3" runat="server" CssClass="lbl-font-common"></asp:Label>&nbsp;&nbsp;
                    <asp:Label ID="lblCIns3" runat="server" CssClass="lbl-font-common"></asp:Label>&nbsp;&nbsp;
                    <asp:Label ID="lblCIns3_" runat="server" CssClass="lbl-font-common"></asp:Label>
                  </td>
              </tr>
              <tr>
                  <td class="td-blank5">
                      <asp:Label ID="Label10" runat="server" CssClass="lbl-font-common" Text="Coin"></asp:Label>
                  </td>
                  <td class="td-blank1"></td>
                  <td class="td-blank2"></td>
                  <td class="td-blank3"></td>
                  <td class="td-blank4"></td>
              </tr>
              <tr>
                  <td class="td-blank5"></td>
                  <td class="td-rupee">
                      <asp:Label ID="Label13" runat="server" CssClass="lbl-font-common" Text="10"></asp:Label>
                  </td>
                  <td class="td-txtbox-rupee">
                      <asp:TextBox ID="TextCoin10Cnt" runat="server" CssClass="txtbox-rupee1" ></asp:TextBox>
                  </td>
                  <td class="td-txtbox2-rupee">
                      <asp:TextBox ID="TextCoin10Sum" runat="server" CssClass="txtbox-common"></asp:TextBox>
                  </td>
                  <td class="td-lbl-open-credit-checkout">
                     <strong class="strong-lbl-open-credit-checkout">
                     <asp:Label ID="Label20" runat="server" CssClass="lbl-status-open-credit-checkout" Text="Checkout"></asp:Label>
                     </strong>
                  </td>
              </tr>
              <tr>
                  <td class="td-blank5"></td>
                  <td class="td-rupee">
                      <asp:Label ID="Label14" runat="server" CssClass="lbl-font-common" Text="5"></asp:Label>
                  </td>
                  <td class="td-txtbox-rupee">
                      <asp:TextBox ID="TextCoin5Cnt" runat="server" CssClass="txtbox-rupee1" ></asp:TextBox>
                  </td>
                  <td class="td-txtbox2-rupee">
                      <asp:TextBox ID="TextCoin5Sum" runat="server" CssClass="txtbox-common"></asp:TextBox>
                  </td>
                  <td class="td-lbl-open-credit-checkout">
                    <asp:Label ID="lblResultClose" runat="server" CssClass="lbl-large-font"></asp:Label>&nbsp;&nbsp;
                    <asp:Label ID="lblName3" runat="server" CssClass="lbl-font-common"></asp:Label>&nbsp;&nbsp;
                    <asp:Label ID="lblTime3" runat="server" CssClass="lbl-font-common"></asp:Label>&nbsp;&nbsp;
                    <asp:Label ID="lblDiff3" runat="server" CssClass="lbl-font-common"></asp:Label>&nbsp;&nbsp;
                    <asp:Label ID="lblmistake4" runat="server" CssClass="lbl-font-common">mistake : </asp:Label>
                    <asp:Label ID="lblmistakeClose" runat="server" CssClass="lbl-font-common"></asp:Label>&nbsp;&nbsp;
                    <asp:Label ID="lblConfirmOut" runat="server" CssClass="lbl-font-common"></asp:Label>
                  </td>
              </tr>
              <tr>
                  <td class="td-blank5"></td>
                  <td class="td-rupee">
                      <asp:Label ID="Label15" runat="server" CssClass="lbl-font-common" Text="2"></asp:Label>
                  </td>
                  <td class="td-txtbox-rupee">
                      <asp:TextBox ID="TextCoin2Cnt" runat="server" CssClass="txtbox-rupee1"></asp:TextBox>
                  </td>
                  <td class="td-txtbox2-rupee">
                      <asp:TextBox ID="TextCoin2Sum" runat="server" CssClass="txtbox-common"></asp:TextBox>
                  </td>
                  <td class="td-lbl-open-credit-checkout">
                      <asp:Label ID="lblConfirmOut2" runat="server" CssClass="lbl-font-common"></asp:Label>
                  </td>
              </tr>
              <tr>
                  <td class="td-blank5"></td>
                  <td class="td-rupee">1</td>
                  <td class="td-txtbox-rupee">
                      <asp:TextBox ID="TextCoin1Cnt" runat="server" CssClass="txtbox-rupee1"></asp:TextBox>
                  </td>
                  <td class="td-txtbox2-rupee">
                      <asp:TextBox ID="TextCoin1Sum" runat="server" Height="18px"></asp:TextBox>
                  </td>
                  <td class="td-lbl-open-credit-checkout">
                    <asp:Label ID="lblLastMsg" runat="server" Text="Deposit complete!!" CssClass="lbl-font-common"></asp:Label>
                  </td>
              </tr>
              <tr>
                  <td class="td-blank5"></td>
                  <td class="td-blank1"></td>
                  <td class="td-txtbox-rupee">
                    <asp:Label ID="Label16" runat="server" Text="Total" CssClass="lbl-font-common"></asp:Label>
                  </td>
                  <td class="td-txtbox2-rupee">
                      <asp:TextBox ID="TextTotal" runat="server" CssClass="txtbox-common"></asp:TextBox>
                  </td>
                  <td class="td-bnt-send">
                    <asp:ImageButton ID="btnSend" runat="server"  ImageUrl="~/icon/send.png"  CssClass="btn-send"  CausesValidation="true" />
                  </td>
              </tr>
              <tr>
                  <td class="td-blank5"></td>
                  <td class="td-blank1"></td>
                  <td class="td-txtbox-rupee">
                    <asp:Label ID="Label17" runat="server" Text="Difference" CssClass="lbl-font-common"></asp:Label>
                  </td>
                  <td class="td-txtbox2-rupee">
                      <asp:TextBox ID="TextDiff" runat="server" CssClass="txtbox-common"></asp:TextBox>
                  </td>
                  <td class="td-bnt-send">
                      <table><tr>
                      <td><asp:Label ID="Label21" runat="server" CssClass="lbl-small-font" Text="Confirmation User"></asp:Label>&nbsp;&nbsp;</td>
                      <td><asp:Label ID="Label22" runat="server" CssClass="lbl-small-font" Text="ID"></asp:Label>&nbsp;&nbsp;</td>
                      <td><asp:TextBox ID="txtConfUser" runat="server"></asp:TextBox>&nbsp;&nbsp;</td>
                      <td><asp:Label ID="Label12" runat="server" CssClass="lbl-small-font" Text="Password"></asp:Label>&nbsp;&nbsp;</td>
                      <td><asp:TextBox ID="txtConfPwd" runat="server" TextMode="Password" Height="19px"></asp:TextBox>&nbsp;&nbsp;</td>
                      <td><asp:ImageButton ID="btnSend2" runat="server"  ImageUrl="~/icon/send.png" CssClass="btn-send" /></td></tr>
                          </table>
                  </td>
              </tr>
              <tr>
                  <td class="td-blank5"></td>
                  <td class="td-blank1"></td>
                  <td class="td-blank2"></td>
                  <td class="td-blank3"></td>
                  <td class="td-bnt-send">
                 <asp:Label ID="lblUpdateShow" runat="server" Text="Lastupdate : " CssClass="lbl-font-common"></asp:Label>
                 <asp:Label ID="lblLastupdate" runat="server" CssClass="lbl-font-common"></asp:Label>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                 <asp:Label ID="lblIPShow" runat="server" CssClass="lbl-font-common" Text="IP : "></asp:Label>
                 <asp:Label ID="lblIP" runat="server" CssClass="lbl-font-common"></asp:Label>&nbsp;&nbsp;
                 &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                  </td>
              </tr>
              <tr>
                   <td class="td-blank5"></td>
                  <td class="td-blank1"></td>
                  <td class="td-blank2"></td>
                  <td class="td-blank3"></td>
                  <td class="td-bnt-send">
                 <asp:Label ID="lblUpdateShow2" runat="server" Text="Confirm Lastupdate : " CssClass="hidden-field"></asp:Label>
                 <asp:Label ID="lblLastupdate2" runat="server" CssClass="lbl-font-common"></asp:Label>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                 <asp:Label ID="lblIPShow2" runat="server" CssClass="lbl-font-common" Text="IP : "></asp:Label>
                 <asp:Label ID="lblIP2" runat="server" CssClass="lbl-font-common"></asp:Label>
                      &nbsp;&nbsp;
                 &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                  </td>
              </tr>
          </table>
       
      </div> 
    </div>
    <div id="dialog" title="message" style="display:none;"> <span class="hidden-field">>
        </span>
        <asp:Label ID="lblMsg" runat="server" Text="" CssClass="hidden-field"></asp:Label>
    </div>

</asp:Content>
