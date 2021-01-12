<%@ Page Title="QMVAR-Analysis_Custody_Search" Language="vb" AutoEventWireup="false" MasterPageFile="~/Site4.Master" CodeBehind="Analysis_Custody_Search.aspx.vb" Inherits="Ganges33.Analysis_Custody_Search" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script src="http://ajax.googleapis.com/ajax/libs/jquery/1.7.0/jquery.min.js"></script>
    <link type="text/css" href="http://ajax.googleapis.com/ajax/libs/jqueryui/1/themes/start/jquery-ui.css" rel="stylesheet">
    <script type="text/javascript" src="http://ajax.googleapis.com/ajax/libs/jqueryui/1/jquery-ui.min.js"></script>
    <link type="text/css" href="CSS/Common/Analysis_Custody_Search.css" rel="stylesheet" /> 

        <style type="text/css">
       
            </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="div-BackgroundImages" style="background-image: url('pagewall_2/wall_analysis-2.png')">
      <div class="div-EntirePage">
          <table class="table-EntirePage">
              <tr>
                  <td class="td-Blank1"></td>
                  <td class="td-imgbtnMoney">
                      <asp:ImageButton ID="btnMoney" runat="server" CssClass="btnMoney" ImageUrl="~/icon/money.png" />
                  </td>
                  <td class="td-lbl1">
                      <asp:Label ID="Label1" runat="server" Text="Search Result" CssClass="lbl1"></asp:Label>
                  </td>
                  <td class="td-Blank2"></td>
                  <td class="td-Blank3"></td>
                  <td align = "right">
                    <asp:ImageButton ID="btnBack" runat="server" ImageUrl="~/icon/back.png" CssClass="btnBack-TakeOut" />
                  </td>
              </tr>
              <tr>
                  <td class="td-Blank4-5-6-7-8-9"></td>
                  <td class="td-lbl-8-15-16">
                      <asp:Label ID="Label15" runat="server" CssClass="lbl-7-8-13-14-15-16-TotalCash-lblKeepNo-lblKeepNo1-TotoalNum-CustomerName-lblCustomerTelNO-lblCash" Text="Total Cash"></asp:Label>
                  </td>
                  <td class="lblTotalcash-lblTotalNumber-td-Blank10">
                      <asp:Label ID="lblTotalCash" runat="server" CssClass="lbl-7-8-13-14-15-16-TotalCash-lblKeepNo-lblKeepNo1-TotoalNum-CustomerName-lblCustomerTelNO-lblCash"></asp:Label>
                  </td>
                  <td class="td-Blank-11-12-13-14-15-16-17">
                      &nbsp;</td>
                  <td class="lbl1-7-8">
                      <asp:Label ID="lblKeepNo1" runat="server" CssClass="lbl-7-8-13-14-15-16-TotalCash-lblKeepNo-lblKeepNo1-TotoalNum-CustomerName-lblCustomerTelNO-lblCash" Text="Keep No."></asp:Label>
                  </td>
                  <td class="td-lblKeepNo-lblCustomerName-lblCustomerTelNO-lblSamsungClaimNo-lblProductName-lblCash">
                      <asp:Label ID="lblKeepNo" runat="server" CssClass="lbl-7-8-13-14-15-16-TotalCash-lblKeepNo-lblKeepNo1-TotoalNum-CustomerName-lblCustomerTelNO-lblCash"></asp:Label>
                  </td>
              </tr>
              <tr>
                  <td class="td-Blank4-5-6-7-8-9"></td>
                  <td class="td-lbl-8-15-16">
                      <asp:Label ID="Label16" runat="server" CssClass="lbl-7-8-13-14-15-16-TotalCash-lblKeepNo-lblKeepNo1-TotoalNum-CustomerName-lblCustomerTelNO-lblCash" Text="Total Number"></asp:Label>
                  </td>
                  <td class="lblTotalcash-lblTotalNumber-td-Blank10">
                      <asp:Label ID="lblTotalNumber" runat="server" CssClass="lbl-7-8-13-14-15-16-TotalCash-lblKeepNo-lblKeepNo1-TotoalNum-CustomerName-lblCustomerTelNO-lblCash"></asp:Label>
                  </td>
                  <td class="td-Blank-11-12-13-14-15-16-17">
                      &nbsp;</td>
                  <td class="lbl1-7-8">
                      <asp:Label ID="Label7" runat="server" CssClass="lbl-7-8-13-14-15-16-TotalCash-lblKeepNo-lblKeepNo1-TotoalNum-CustomerName-lblCustomerTelNO-lblCash" Text="Customer Name"></asp:Label>
                  </td>
                  <td class="td-lblKeepNo-lblCustomerName-lblCustomerTelNO-lblSamsungClaimNo-lblProductName-lblCash">
                      <asp:Label ID="lblCustomerName" runat="server" CssClass="lbl-7-8-13-14-15-16-TotalCash-lblKeepNo-lblKeepNo1-TotoalNum-CustomerName-lblCustomerTelNO-lblCash"></asp:Label>
                  </td>
              </tr>
              <tr>
                  <td class="td-Blank4-5-6-7-8-9"></td>
                  <td class="td-lbl-8-15-16"></td>
                  <td class="lblTotalcash-lblTotalNumber-td-Blank10"></td>
                  <td class="td-Blank-11-12-13-14-15-16-17"></td>
                  <td class="lbl1-7-8">
                      <asp:Label ID="Label8" runat="server" CssClass="lbl-7-8-13-14-15-16-TotalCash-lblKeepNo-lblKeepNo1-TotoalNum-CustomerName-lblCustomerTelNO-lblCash" Text="Customer Tel No."></asp:Label>
                  </td>
                  <td class="td-lblKeepNo-lblCustomerName-lblCustomerTelNO-lblSamsungClaimNo-lblProductName-lblCash">
                      <asp:Label ID="lblCustomerTelNO" runat="server" CssClass="lbl-7-8-13-14-15-16-TotalCash-lblKeepNo-lblKeepNo1-TotoalNum-CustomerName-lblCustomerTelNO-lblCash"></asp:Label>
                  </td>
              </tr>
              <tr>
                  <td class="td-Blank4-5-6-7-8-9"></td>
                  <td class="td-Blank-18-lbls-9-13-14"></td>
                  <td class="td-Blank-18-lbls-9-13-14"></td>
                  <td class="td-Blank-11-12-13-14-15-16-17"></td>
                  <td class="td-Blank-18-lbls-9-13-14">
                      <asp:Label ID="Label9" runat="server" CssClass="lbl-7-8-13-14-15-16-TotalCash-lblKeepNo-lblKeepNo1-TotoalNum-CustomerName-lblCustomerTelNO-lblCash" Text="Samsung Claim No."></asp:Label>
                  </td>
                  <td class="td-lblKeepNo-lblCustomerName-lblCustomerTelNO-lblSamsungClaimNo-lblProductName-lblCash">
                      <asp:Label ID="lblSamsungClaimNo" runat="server" CssClass="lbl-7-8-13-14-15-16-TotalCash-lblKeepNo-lblKeepNo1-TotoalNum-CustomerName-lblCustomerTelNO-lblCash"></asp:Label>
                  </td>
              </tr>
              <tr>
                  <td class="td-Blank4-5-6-7-8-9"></td>
                  <td class="td-Blank-18-lbls-9-13-14"></td>
                  <td class="td-Blank-18-lbls-9-13-14"></td>
                  <td class="td-Blank-11-12-13-14-15-16-17"></td>
                  <td class="td-Blank-18-lbls-9-13-14">
                      <asp:Label ID="Label13" runat="server" CssClass="lbl-7-8-13-14-15-16-TotalCash-lblKeepNo-lblKeepNo1-TotoalNum-CustomerName-lblCustomerTelNO-lblCash" Text="Product Name"></asp:Label>
                  </td>
                  <td class="td-lblKeepNo-lblCustomerName-lblCustomerTelNO-lblSamsungClaimNo-lblProductName-lblCash">
                      <asp:Label ID="lblProductName" runat="server" CssClass="lbl-7-8-13-14-15-16-TotalCash-lblKeepNo-lblKeepNo1-TotoalNum-CustomerName-lblCustomerTelNO-lblCash"></asp:Label>
                  </td>
              </tr>
              <tr>
                  <td class="td-Blank4-5-6-7-8-9"></td>
                  <td class="td-Blank-18-lbls-9-13-14"></td>
                  <td class="td-Blank-18-lbls-9-13-14"></td>
                  <td class="td-Blank-11-12-13-14-15-16-17"></td>
                  <td class="td-Blank-18-lbls-9-13-14">
                      <asp:Label ID="Label14" runat="server" CssClass="lbl-7-8-13-14-15-16-TotalCash-lblKeepNo-lblKeepNo1-TotoalNum-CustomerName-lblCustomerTelNO-lblCash" Text="Cash"></asp:Label>
                  </td>
                  <td class="td-lblKeepNo-lblCustomerName-lblCustomerTelNO-lblSamsungClaimNo-lblProductName-lblCash">
                      <asp:Label ID="lblCash" runat="server" CssClass="lbl-7-8-13-14-15-16-TotalCash-lblKeepNo-lblKeepNo1-TotoalNum-CustomerName-lblCustomerTelNO-lblCash"></asp:Label>
                  </td>
              </tr>
              <tr>
                  <td class="td-Blank4-5-6-7-8-9"></td>
                  <td class="td-Blank-18-lbls-9-13-14"></td>
                  <td class="td-Blank-18-lbls-9-13-14"></td>
                  <td class="td-Blank-11-12-13-14-15-16-17"></td>
                  <td class="td-Blank-18-lbls-9-13-14">&nbsp;</td>
                  <td class="td-Blank-18-lbls-9-13-14" align = "right">
                    <asp:ImageButton ID="btnTakeOut" runat="server" ImageUrl="~/icon/takeout.png" CssClass="btnBack-TakeOut" />
                  </td>
              </tr>
          </table>
          <br />
          <table align = "center">
              <tr>
                  <td>
                      <asp:GridView ID="GridInfo" runat="server" EmptyDataText="There was no corresponding information." AllowPaging="True">
                       <Columns>
                       <asp:ButtonField CommandName="TakeOut" HeaderText="TakeOut" ShowHeader="True" Text="check" />
                       </Columns>
                       <HeaderStyle BackColor="#66FFFF" />
                       <RowStyle CssClass="RowStyle" Wrap="False"/>
                      </asp:GridView> 
                  </td>
              </tr>
              <tr>
                  <td align = "right" class="td-txtbox1">
                      <asp:TextBox ID="TextBox1" runat="server" BorderColor="White" BorderStyle="None" ReadOnly="True" ></asp:TextBox>
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
