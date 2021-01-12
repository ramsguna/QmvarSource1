<%@ Page Title="QMVQR-Analysis_Cash_Tracking2" Language="vb" AutoEventWireup="false" MasterPageFile="~/Site4.Master" CodeBehind="Analysis_Cash_Tracking2.aspx.vb" Inherits="Ganges33.Analysis_Cash_Tracking2" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
      <script src="http://ajax.googleapis.com/ajax/libs/jquery/1.7.0/jquery.min.js"></script>
    <link type="text/css" href="http://ajax.googleapis.com/ajax/libs/jqueryui/1/themes/start/jquery-ui.css" rel="stylesheet">
    <script type="text/javascript" src="http://ajax.googleapis.com/ajax/libs/jqueryui/1/jquery-ui.min.js"></script>
       <link type="text/css" href="CSS/Common/Analysis_Cash_Tracking_2.css" rel="stylesheet" />
    <style type="text/css">
        </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
        <div class="div-backgroundImage" style="background-image: url('pagewall_2/wall_analysis-2.png')">
      <div class="div-EntirePage">
          <table class="tabel-lbl-7-16-17-lblMonNow-lblDay">
              <tr>
                  <td class="td-Blank-1-2"></td>
                  <td class="td-lbl7">
                      <asp:Label ID="Label7" runat="server" CssClass="lbl7-ReviewCashandCardMethod" Text="Review Cash &amp; Card Method"></asp:Label>
                  </td>
                  <td class="td-lbl16-month" align = "right"> 
                      <asp:Label ID="Label16" runat="server" CssClass="fontFamily" Text="Month"></asp:Label>
                  </td>
                  <td class="td-lblMonNow"> 
                      <asp:Label ID="lblMonNow" runat="server" CssClass="lblMonNow-lblDay-lblCount-lblCount0-lblCash0-lblCard0"></asp:Label>
                  </td>
                  <td class="td-lbl17" align = "right"> 
                      <asp:Label ID="Label17" runat="server" CssClass="fontFamily" Text="Day"></asp:Label>
                  </td>
                  <td class="td-lblday"> 
                      <asp:Label ID="lblDay" runat="server" CssClass="lblMonNow-lblDay-lblCount-lblCount0-lblCash0-lblCard0"></asp:Label>
                  </td>
                  <td class="td-Blank-1-2"> </td>
              </tr>
          </table>
          <table align = "center">
              <tr>
                  <td>
                      &nbsp;</td>
                  <td align = "right">
                    <asp:ImageButton ID="btnBack" runat="server" ImageUrl="~/icon/back.png" CssClass="imgbtn-Back-SendDown" />
                  </td>  
              </tr>
              <tr>
                  <td class="td-GridView" colspan = "2">
                      <asp:GridView ID="GridInfo" runat="server" EmptyDataText="There was no corresponding information." AllowPaging="True">
                          <Columns>
                              <asp:TemplateField HeaderText="del_chk">
                                  <ItemTemplate>
                                      <asp:CheckBox ID="chkDel" runat="server" />
                                  </ItemTemplate>
                              </asp:TemplateField>
                          </Columns>
                       <HeaderStyle BackColor="#66FFFF" />
                       <RowStyle CssClass="RowStyle" Wrap="False"/>
                      </asp:GridView>
                  </td>
              </tr>
              <tr>
                  <td>
                      &nbsp;</td>
                  <td align = "right">
                    <asp:ImageButton ID="btnSend" runat="server" ImageUrl="~/icon/send.png" CssClass="imgbtn-Back-SendDown" />
                  </td>  
              </tr>
              <tr>
                  <td colspan= "2" ></td>
              </tr>
          </table>
          <table align = "center" class="tableEntirePage">
              <tr>
                  <td class="td-lbl-invoicetotal" rowspan = "4">
                      <asp:Label ID="Label8" runat="server" Text="invoice total" CssClass="fontFamily"></asp:Label>
                  </td>
                  <td class="lbl-9-11-12-13-14-15">
                      <asp:Label ID="Label9" runat="server" Text="Count" CssClass="fontFamily"></asp:Label>
                  </td>
                  <td class="lbl-9-11-12-13-14-15">
                      <asp:Label ID="label" runat="server" Text="IW" CssClass="fontFamily"></asp:Label>
                  </td>
                  <td class="lbl-9-11-12-13-14-15">
                      <asp:Label ID="Label11" runat="server" Text="OOW" CssClass="fontFamily"></asp:Label>
                  </td>
                  <td class="lbl-9-11-12-13-14-15">
                      <asp:Label ID="Label12" runat="server" Text="Other" CssClass="fontFamily"></asp:Label>
                  </td>
                  <td class="lbl-9-11-12-13-14-15">
                      <asp:Label ID="Label13" runat="server" Text="Cash" CssClass="fontFamily"></asp:Label>
                  </td>
                  <td class="lbl-9-11-12-13-14-15">
                      <asp:Label ID="Label14" runat="server" Text="Card" CssClass="fontFamily"></asp:Label>
                  </td>
                  <td class="lbl-9-11-12-13-14-15">
                      <asp:Label ID="Label15" runat="server" Text="Amount" CssClass="fontFamily"></asp:Label>
                  </td>
              </tr>
              <tr>
                  <td class="td-lblCount-lblIW-lblOOW-lblOther-lblCash-lblCard-lblAmount-lblIWAmount-lblOOWAmount-lblOtherAmount" rowspan = "3">
                      <asp:Label ID="lblCount" runat="server" CssClass="lblMonNow-lblDay-lblCount-lblCount0-lblCash0-lblCard0"></asp:Label>
                  </td>
                  <td class="td-lblCount-lblIW-lblOOW-lblOther-lblCash-lblCard-lblAmount-lblIWAmount-lblOOWAmount-lblOtherAmount">
                      <asp:Label ID="lblIW" runat="server" CssClass="fontFamily"></asp:Label>
                  </td>
                  <td class="td-lblCount-lblIW-lblOOW-lblOther-lblCash-lblCard-lblAmount-lblIWAmount-lblOOWAmount-lblOtherAmount">
                      <asp:Label ID="lblOOW" runat="server" CssClass="fontFamily"></asp:Label>
                  </td>
                  <td class="td-lblCount-lblIW-lblOOW-lblOther-lblCash-lblCard-lblAmount-lblIWAmount-lblOOWAmount-lblOtherAmount">
                      <asp:Label ID="lblOther" runat="server" CssClass="fontFamily"></asp:Label>
                  </td>
                  <td class="td-lblCount-lblIW-lblOOW-lblOther-lblCash-lblCard-lblAmount-lblIWAmount-lblOOWAmount-lblOtherAmount" rowspan = "3">
                      <asp:Label ID="lblCash" runat="server" CssClass="fontFamily"></asp:Label>
                  </td>
                  <td class="td-lblCount-lblIW-lblOOW-lblOther-lblCash-lblCard-lblAmount-lblIWAmount-lblOOWAmount-lblOtherAmount" rowspan = "3">
                      <asp:Label ID="lblCard" runat="server" CssClass="fontFamily"></asp:Label>
                  </td>
                  <td class="td-lblCount-lblIW-lblOOW-lblOther-lblCash-lblCard-lblAmount-lblIWAmount-lblOOWAmount-lblOtherAmount" rowspan = "3">
                      <asp:Label ID="lblAmount" runat="server" CssClass="lblAmount-lblAmount0"></asp:Label>
                  </td>
              </tr>
              <tr>
                  <td class="td-lbl18-lbl29">
                      <asp:Label ID="Label18" runat="server" CssClass="fontFamily" Text="IW Amount"></asp:Label>
                  </td>
                  <td class="td-lbl19-lbl30">
                      <asp:Label ID="Label19" runat="server" CssClass="fontFamily" Text="OOW Amount"></asp:Label>
                  </td>
                  <td class="td-lbl20-lbl31">
                      <asp:Label ID="Label20" runat="server" CssClass="fontFamily" Text="Other Amount"></asp:Label>
                  </td>
              </tr>
              <tr>
                  <td class="td-lblCount-lblIW-lblOOW-lblOther-lblCash-lblCard-lblAmount-lblIWAmount-lblOOWAmount-lblOtherAmount">
                      <asp:Label ID="lblIWAmount" runat="server"></asp:Label>
                  </td>
                  <td class="td-lblCount-lblIW-lblOOW-lblOther-lblCash-lblCard-lblAmount-lblIWAmount-lblOOWAmount-lblOtherAmount">
                      <asp:Label ID="lblOOWAmount" runat="server"></asp:Label>
                  </td>
                  <td class="td-lblCount-lblIW-lblOOW-lblOther-lblCash-lblCard-lblAmount-lblIWAmount-lblOOWAmount-lblOtherAmount">
                      <asp:Label ID="lblOtherAmount" runat="server"></asp:Label>
                  </td>
              </tr>              
              <tr>
                  <td colspan = "8"></td>
              </tr>
              <tr>
                  <td class="td-lbl1" rowspan = "4">
                      <asp:Label ID="Label1" runat="server" Text="Collected total" CssClass="fontFamily"></asp:Label>
                  </td>
                  <td class="td-lbl2-3-4-5-6-10-21">
                      <asp:Label ID="Label2" runat="server" Text="Count" CssClass="fontFamily"></asp:Label>
                  </td>
                  <td class="td-lbl2-3-4-5-6-10-21">
                      <asp:Label ID="label3" runat="server" Text="IW" CssClass="fontFamily"></asp:Label>
                  </td>
                  <td class="td-lbl2-3-4-5-6-10-21">
                      <asp:Label ID="Label4" runat="server" Text="OOW" CssClass="fontFamily"></asp:Label>
                  </td>
                  <td class="td-lbl2-3-4-5-6-10-21">
                      <asp:Label ID="Label5" runat="server" Text="Other" CssClass="fontFamily"></asp:Label>
                  </td>
                  <td class="td-lbl2-3-4-5-6-10-21">
                      <asp:Label ID="Label6" runat="server" Text="Cash" CssClass="fontFamily"></asp:Label>
                  </td>
                  <td class="td-lbl2-3-4-5-6-10-21">
                      <asp:Label ID="Label10" runat="server" Text="Card" CssClass="fontFamily"></asp:Label>
                  </td>
                  <td class="td-lbl2-3-4-5-6-10-21">
                      <asp:Label ID="Label21" runat="server" Text="Amount" CssClass="fontFamily"></asp:Label>
                  </td>
              </tr>
              <tr>
                  <td class="lblCount0-lblIW0-lblOther0-lblCash0-lblAmount0-lblIWAmount0-lblOOWAmount0-lblOtherAmount0" rowspan = "3">
                      <asp:Label ID="lblCount0" runat="server" CssClass="lblMonNow-lblDay-lblCount-lblCount0-lblCash0-lblCard0"></asp:Label>
                  </td>
                  <td class="lblCount0-lblIW0-lblOther0-lblCash0-lblAmount0-lblIWAmount0-lblOOWAmount0-lblOtherAmount0">
                      <asp:Label ID="lblIW0" runat="server" CssClass="fontFamily"></asp:Label>
                  </td>
                  <td class="lblCount0-lblIW0-lblOther0-lblCash0-lblAmount0-lblIWAmount0-lblOOWAmount0-lblOtherAmount0">
                      <asp:Label ID="lblOOW0" runat="server" CssClass="fontFamily"></asp:Label>
                  </td>
                  <td class="lblCount0-lblIW0-lblOther0-lblCash0-lblAmount0-lblIWAmount0-lblOOWAmount0-lblOtherAmount0">
                      <asp:Label ID="lblOther0" runat="server" CssClass="fontFamily"></asp:Label>
                  </td>
                  <td class="lblCount0-lblIW0-lblOther0-lblCash0-lblAmount0-lblIWAmount0-lblOOWAmount0-lblOtherAmount0" rowspan = "3">
                      <asp:Label ID="lblCash0" runat="server" CssClass="lblMonNow-lblDay-lblCount-lblCount0-lblCash0-lblCard0"></asp:Label>
                  </td>
                  <td class="lblCount0-lblIW0-lblOther0-lblCash0-lblAmount0-lblIWAmount0-lblOOWAmount0-lblOtherAmount0" rowspan = "3">
                      <asp:Label ID="lblCard0" runat="server" CssClass="lblMonNow-lblDay-lblCount-lblCount0-lblCash0-lblCard0"></asp:Label>
                  </td>
                  <td class="lblCount0-lblIW0-lblOther0-lblCash0-lblAmount0-lblIWAmount0-lblOOWAmount0-lblOtherAmount0" rowspan = "3">
                      <asp:Label ID="lblAmount0" runat="server" CssClass="lblAmount-lblAmount0"></asp:Label>
                  </td>
              </tr>
              <tr>
                  <td class="td-lbl18-lbl29">
                      <asp:Label ID="Label29" runat="server" CssClass="fontFamily" Text="IW Amount"></asp:Label>
                  </td>
                  <td class="td-lbl19-lbl30">
                      <asp:Label ID="Label30" runat="server" CssClass="fontFamily" Text="OOW Amount"></asp:Label>
                  </td>
                  <td class="td-lbl20-lbl31">
                      <asp:Label ID="Label31" runat="server" CssClass="fontFamily" Text="Other Amount"></asp:Label>
                  </td>
              </tr>
              <tr>
                  <td class="lblCount0-lblIW0-lblOther0-lblCash0-lblAmount0-lblIWAmount0-lblOOWAmount0-lblOtherAmount0">
                      <asp:Label ID="lblIWAmount0" runat="server"></asp:Label>
                  </td>
                  <td class="lblCount0-lblIW0-lblOther0-lblCash0-lblAmount0-lblIWAmount0-lblOOWAmount0-lblOtherAmount0">
                      <asp:Label ID="lblOOWAmount0" runat="server"></asp:Label>
                  </td>
                  <td class="lblCount0-lblIW0-lblOther0-lblCash0-lblAmount0-lblIWAmount0-lblOOWAmount0-lblOtherAmount0">
                      <asp:Label ID="lblOtherAmount0" runat="server"></asp:Label>
                  </td>
              </tr>
              <tr>
                  <td align = "right" colspan = "8">
                    <asp:ImageButton ID="btnDown" runat="server" ImageUrl="~/icon/download.png" CssClass="imgbtn-Back-SendDown" />
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
