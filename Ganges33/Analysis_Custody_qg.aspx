<%@ Page Language="vb" AutoEventWireup="false"  MasterPageFile="~/Site4.Master" CodeBehind="Analysis_Custody_qg.aspx.vb" Inherits="Ganges33.Analysis_Custody_qg" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script src="http://ajax.googleapis.com/ajax/libs/jquery/1.7.0/jquery.min.js"></script>
    <link type="text/css" href="http://ajax.googleapis.com/ajax/libs/jqueryui/1/themes/start/jquery-ui.css" rel="stylesheet">
    <script type="text/javascript" src="http://ajax.googleapis.com/ajax/libs/jqueryui/1/jquery-ui.min.js"></script>
    <link type="text/css" href="CSS/Common/Analysis_Custody_qg.css" rel="stylesheet" /> 
        <style type="text/css">
                     
                 </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="div-bgimg-EntirePage" style="background-image: url('pagewall_2/wall_analysis-2.png')">
      <div class="div-EntirePage">
          <table class="tabel-EntirePage">
              <tr>
                  <td class="td-Blank1-Blank3"></td>
                  <td class="td-Blank2" rowspan = "3">
                      <asp:ImageButton ID="btnMoney" runat="server" CssClass="img-btnMoney" ImageUrl="~/icon/money.png" />
                  </td>
                  <td rowspan = "3" colspan = "2">
                      <asp:Label ID="Label1" runat="server" Text="Products &amp; Money Keep" CssClass="label-ProductsMpneyKeep-lbl10-Result"></asp:Label>
                  </td>
                  <td class="td-Blank4-Blank5"></td>
                  <td class="td-Blank6-Blank7">
                      <asp:Label ID="Label2" runat="server" Text="Search No" CssClass="fontFamily"></asp:Label>
                  </td>
                  <td>
                      <asp:TextBox ID="TextSearchNo" runat="server" CssClass="txtbox-SearchNo-SearchName-TextSearchtel"></asp:TextBox>
                  </td>
              </tr>
              <tr>
                  <td class="td-Blank8"></td>
                  <td class="td-Blank9"></td>
                  <td class="td-Blank10">
                      <asp:Label ID="Label3" runat="server" Text="Search Name" CssClass="fontFamily"></asp:Label>
                  </td>
                  <td>
                      <asp:TextBox ID="TextSearchName" runat="server" CssClass="txtbox-SearchNo-SearchName-TextSearchtel"></asp:TextBox>
                  </td>
              </tr>
              <tr>
                  <td class="td-Blank1-Blank3"></td>
                  <td class="td-Blank4-Blank5"></td>
                  <td class="td-Blank6-Blank7">
                      <asp:Label ID="Label4" runat="server" Text="Search tel" CssClass="fontFamily"></asp:Label>
                  </td>
                  <td class="td-Blank11">
                      <asp:TextBox ID="TextSearchtel" runat="server" CssClass="txtbox-SearchNo-SearchName-TextSearchtel"></asp:TextBox>
                  </td>
              </tr>
              <tr>
                  <td class="td-Blank12"></td>
                  <td class="td-Blank13"></td>
                  <td class="td-Blank14"></td>
                  <td class="td-Blank15"></td>
                  <td class="td-Blank16"></td>
                  <td class="td-Blank17">
                      <asp:CheckBox ID="CheckFalse" runat="server" CssClass="fontFamily" Text="FALSE" />
                  </td>
                  <td >
                    <asp:ImageButton ID="btnStart" runat="server" ImageUrl="~/icon/start.png" CssClass="img-btnStart-btnSend" />
                  </td>
              </tr>
              <tr>
                  <td class="td-Blank18"></td>
                  <td colspan = "2" class="td-Blank19-td-Blank20-td-Blank21">
                      <asp:Label ID="Label5" runat="server" Text="Customer Name" CssClass="fontFamily"></asp:Label>
                  </td>
                  <td class="td-customername-telNo-SmsungClaimNo-ProductsName-textCash-KeepNo" >
                      <asp:TextBox ID="TextCustomerName" runat="server" CssClass="txtbox-CustomerName-CustomerTelNo-SmsungClaimNo-ProductsName-TextCash-Claimno"></asp:TextBox>
                  </td>
                  <td class="td-Blank22">
                      </td>
                  <td class="td-Blank23"></td>
                  <td class="td-Blank19-td-Blank20-td-Blank21" ></td>
              </tr>
              <tr>
                  <td class="td-Blank24-Blank25-Blank26-Blank27-Blank28"></td>
                  <td colspan = "2" class="td-Blank19-td-Blank20-td-Blank21">
                      <asp:Label ID="Label6" runat="server" Text="Customer Tel No." CssClass="fontFamily"></asp:Label>
                  </td>
                  <td class="td-customername-telNo-SmsungClaimNo-ProductsName-textCash-KeepNo" >
                      <asp:TextBox ID="TextCustomerTelNo" runat="server" CssClass="txtbox-CustomerName-CustomerTelNo-SmsungClaimNo-ProductsName-TextCash-Claimno"></asp:TextBox>
                  </td>
                  <td>
                      &nbsp;</td>
                  <td class="td-Blank31-Blank32-Blank33"></td>
                  <td ></td>
              </tr>
              <tr>
                  <td class="td-Blank24-Blank25-Blank26-Blank27-Blank28"></td>
                  <td colspan = "2" class="td-Blank34-td-Blank35">
                      <asp:Label ID="Label7" runat="server" Text="Sumsung Claim No." CssClass="fontFamily"></asp:Label>
                  </td>
                  <td class="td-customername-telNo-SmsungClaimNo-ProductsName-textCash-KeepNo" >
                      <asp:TextBox ID="TextSumsungClaimNo" runat="server" CssClass="txtbox-CustomerName-CustomerTelNo-SmsungClaimNo-ProductsName-TextCash-Claimno"></asp:TextBox>
                  </td>
                  <td class="td-Blank29-Blank30">
                      </td>
                  <td class="td-Blank31-Blank32-Blank33"></td>
                  <td ></td>
              </tr>
              <tr>
                  <td class="td-Blank24-Blank25-Blank26-Blank27-Blank28"></td>
                  <td colspan = "2" class="td-Blank34-td-Blank35">
                      <asp:Label ID="Label8" runat="server" Text="Products Name" CssClass="fontFamily"></asp:Label>
                  </td>
                  <td class="td-customername-telNo-SmsungClaimNo-ProductsName-textCash-KeepNo" >
                      <asp:TextBox ID="TextProductsName" runat="server" CssClass="txtbox-CustomerName-CustomerTelNo-SmsungClaimNo-ProductsName-TextCash-Claimno"></asp:TextBox>
                  </td>
                  <td class="td-Blank29-Blank30">
                      </td>
                  <td class="td-Blank31-Blank32-Blank33"></td>
                  <td ></td>
              </tr>
              <tr>
                  <td class="td-Blank24-Blank25-Blank26-Blank27-Blank28"></td>
                  <td class="td-lbl9-lbl20" colspan = "2">
                      <asp:Label ID="Label9" runat="server" Text="Cash" CssClass="fontFamily"></asp:Label>
                  </td>
                  <td class="td-customername-telNo-SmsungClaimNo-ProductsName-textCash-KeepNo">
                      <asp:TextBox ID="TextCash" runat="server" CssClass="txtbox-CustomerName-CustomerTelNo-SmsungClaimNo-ProductsName-TextCash-Claimno"></asp:TextBox>
                  </td>
                  <td>
                      <asp:Label ID="Label19" runat="server" CssClass="fontFamily" Text="INR"></asp:Label>
                  </td>
                  <td >
                      &nbsp;</td>
                  <td ></td>
              </tr>
              <tr>
                  <td class="td-Blank24-Blank25-Blank26-Blank27-Blank28">&nbsp;</td>
                  <td class="td-lbl9-lbl20" colspan = "2">
                       <asp:Label ID="Label20" runat="server" Text="Keep No." CssClass="fontFamily" Visible="false"></asp:Label>
                      </td>
                  <td class="td-customername-telNo-SmsungClaimNo-ProductsName-textCash-KeepNo">
                      <asp:TextBox ID="txtKeepNo" runat="server"  CssClass="txtbox-CustomerName-CustomerTelNo-SmsungClaimNo-ProductsName-TextCash-Claimno" Visible="false"></asp:TextBox>
                  </td>
                  <td>
                      &nbsp;</td>
                  <td >
                    <asp:ImageButton ID="btnSend" runat="server" ImageUrl="~/icon/send.png" CssClass="img-btnStart-btnSend" />
                  </td>
                  <td >&nbsp;</td>
              </tr>
          </table>
          <table class="table-center" align = "center">
              <tr>
                  <td class="td-center-lbl10" align = "center">
                      <asp:Label ID="Label10" runat="server" CssClass="label-ProductsMpneyKeep-lbl10-Result" Text="Reslut"></asp:Label>
                  </td>
              </tr>
          </table>
          <br />
          <table>
              <tr>
                  <td class="td-Blank36-Blank37"></td>
                  <td class="td-Blank38-Blank39">
                      <asp:Label ID="Label11" runat="server" Text="Customer Name" CssClass="fontFamily"></asp:Label>
                  </td>
                  <td class="td-lblCustomer-lblCustomerNo">
                      <asp:Label ID="lblCustomerName" runat="server" CssClass="fontFamily"></asp:Label>
                  </td>
                  <td class="td-Blank40-Blank41"></td>
                  <td class="td-lbl17-lbl18">
                      <asp:Label ID="Label17" runat="server" Text="Total Cash" CssClass="fontFamily"></asp:Label>
                  </td>
                  <td class="td-lbltotalcash-totalnumber">
                      <asp:Label ID="lblTotalCash" runat="server" CssClass="fontFamily"></asp:Label>
                  </td>
                  <td></td>
              </tr>
              <tr>
                  <td class="td-Blank36-Blank37"></td>
                  <td class="td-Blank38-Blank39">
                      <asp:Label ID="Label12" runat="server" Text="Customer Tel No." CssClass="fontFamily"></asp:Label>
                  </td>
                  <td class="td-lblCustomer-lblCustomerNo">
                      <asp:Label ID="lblCustomerTelNo" runat="server" CssClass="fontFamily"></asp:Label>
                  </td>
                  <td class="td-Blank40-Blank41"></td>
                  <td class="td-lbl17-lbl18">
                      <asp:Label ID="Label18" runat="server" Text="Total Number" CssClass="fontFamily"></asp:Label>
                  </td>
                  <td class="td-lbltotalcash-totalnumber">
                      <asp:Label ID="lblTotalNumber" runat="server" CssClass="fontFamily"></asp:Label>
                  </td>
                  <td></td>
              </tr>
              <tr>
                  <td class="td-Blank42-43-44-45-46">&nbsp;</td>
                  <td class="td-lbl-13-14-15-16-Blank47">
                      <asp:Label ID="Label13" runat="server" Text="Sumsung Claim No." CssClass="fontFamily"></asp:Label>
                  </td>
                  <td class="td-lbl-SamsungClaimNo-ProductsName-lblCash-KeepNo">
                      <asp:Label ID="lblSumsungClaimNo" runat="server" CssClass="fontFamily"></asp:Label>
                  </td>
                  <td class="td-Blank-48-49-50-51-52">&nbsp;</td>
                  <td class="td-Blank-53-54-55-56-57">&nbsp;</td>
                  <td class="td-Blank-58-59-60-61-62">&nbsp;</td>
                  <td></td>
              </tr>
              <tr>
                  <td class="td-Blank42-43-44-45-46">&nbsp;</td>
                  <td class="td-lbl-13-14-15-16-Blank47">
                      <asp:Label ID="Label14" runat="server" Text="Products Name" CssClass="fontFamily"></asp:Label>
                  </td>
                  <td class="td-lbl-SamsungClaimNo-ProductsName-lblCash-KeepNo">
                      <asp:Label ID="lblProductsName" runat="server" CssClass="fontFamily"></asp:Label>
                  </td>
                  <td class="td-Blank-48-49-50-51-52">&nbsp;</td>
                  <td class="td-Blank-53-54-55-56-57">&nbsp;</td>
                  <td class="td-Blank-58-59-60-61-62">&nbsp;</td>
                  <td></td>
              </tr>
              <tr>
                  <td class="td-Blank42-43-44-45-46">&nbsp;</td>
                  <td class="td-lbl-13-14-15-16-Blank47">
                      <asp:Label ID="Label15" runat="server" Text="Cash" CssClass="fontFamily"></asp:Label>
                  </td>
                  <td class="td-lbl-SamsungClaimNo-ProductsName-lblCash-KeepNo">
                      <asp:Label ID="lblCash" runat="server" CssClass="fontFamily"></asp:Label>
                  </td>
                  <td class="td-Blank-48-49-50-51-52">&nbsp;</td>
                  <td class="td-Blank-53-54-55-56-57">&nbsp;</td>
                  <td class="td-Blank-58-59-60-61-62">&nbsp;</td>
                  <td></td>
              </tr>
              <tr>
                  <td class="td-Blank42-43-44-45-46">&nbsp;</td>
                  <td class="td-lbl-13-14-15-16-Blank47">
                      <asp:Label ID="Label16" runat="server" Text="Keep No." CssClass="fontFamily"></asp:Label>
                  </td>
                  <td class="td-lbl-SamsungClaimNo-ProductsName-lblCash-KeepNo">
                      <asp:Label ID="lblKeepNo" runat="server" CssClass="fontFamily"></asp:Label>
                  </td>
                  <td class="td-Blank-48-49-50-51-52">&nbsp;</td>
                  <td class="td-Blank-53-54-55-56-57">&nbsp;</td>
                  <td class="td-Blank-58-59-60-61-62">&nbsp;</td>
                  <td></td>
              </tr>
              <tr>
                  <td class="td-Blank42-43-44-45-46">&nbsp;</td>
                  <td class="td-lbl-13-14-15-16-Blank47">&nbsp;</td>
                  <td class="td-Blank63" >&nbsp;</td>
                  <td class="td-Blank-48-49-50-51-52">&nbsp;</td>
                  <td class="td-Blank-53-54-55-56-57">&nbsp;</td>
                  <td class="td-Blank-58-59-60-61-62">&nbsp;</td>
                  <td></td>
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
