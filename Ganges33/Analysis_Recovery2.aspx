<%@ Page Title="QMVQR-Analysis_Recovery2" Language="vb" AutoEventWireup="false" MasterPageFile="~/Analysis.Master" CodeBehind="Analysis_Recovery2.aspx.vb" Inherits="Ganges33.Analysis_Recovery2" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script src="http://ajax.googleapis.com/ajax/libs/jquery/1.7.0/jquery.min.js"></script>
    <link type="text/css" href="http://ajax.googleapis.com/ajax/libs/jqueryui/1/themes/start/jquery-ui.css" rel="stylesheet">
    <script type="text/javascript" src="http://ajax.googleapis.com/ajax/libs/jqueryui/1/jquery-ui.min.js"></script>
    <link type="text/css" href="CSS/Common/Analysis_Recovery2.css" rel="stylesheet" /> 
        <style type="text/css">
       
            </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="div-backgroundimg" style="background-image: url('pagewall_2/wall_analysis-2.png')">
      <div class="div-EntirePage">
          <table class="table-EntirePage">
              <tr>
                  <td class="td-Blank1-btnAnalysis"></td>
                  <td class="td-Blank1-btnAnalysis">
                      <asp:ImageButton ID="btnAnalysis" runat="server" CssClass="btnAnalysis" ImageUrl="~/icon/analysis.png" />
                  </td>
                  <td class="td-lbl3">
                      <asp:Label ID="Label3" runat="server" CssClass="lbl3" Text="Analysis Recovery Data Result"></asp:Label>
                  </td>
                  <td class="td-Blank2-3"></td>
                  <td class="td-Blank4"></td>
                  <td class="td-Blank2-3"></td>
                  <td class="td-Blank5-6"></td>
                  <td class="td-Blank5-6"></td>
              </tr>
              <tr>
                  <td class="td-Blank7-8"></td>
                  <td class="td-Blank7-8"></td>
                  <td class="td-lblUploadData">
                      <asp:Label ID="lblUploadData" runat="server" CssClass="lblUploadData-lblBranch"></asp:Label>
                  </td>
                  <td class="td-lblFromDate-ToDate">
                      <asp:Label ID="lblFromdate" runat="server" CssClass="lblFromDate-lbl5-todate"></asp:Label>
                  </td>
                  <td class="td-lbl5">
                      <asp:Label ID="Label5" runat="server" CssClass="lblFromDate-lbl5-todate" Text="～"></asp:Label>
                  </td>
                  <td class="td-lblFromDate-ToDate">
                      <asp:Label ID="lblTodate" runat="server" CssClass="lblFromDate-lbl5-todate"></asp:Label>
                  </td>
                  <td class="td-Blank9-10"></td>
                  <td class="td-Blank9-10"></td>
              </tr>
              <tr>
                  <td class="td-Blank11-12"></td>
                  <td class="td-Blank11-12"></td>
                  <td class="td-lblBranch">
                      <asp:Label ID="lblBranch" runat="server" CssClass="lblUploadData-lblBranch"></asp:Label>
                  </td>
                  <td class="td-Blank13-14"></td>
                  <td class="td-Blank15"></td>
                  <td class="td-Blank13-14"></td>
                  <td class="td-Blank15-16">   
                    <asp:ImageButton ID="btnBack" runat="server" ImageUrl="~/icon/back.png" CssClass="imgbtn-back-start" />
                   </td>
                  <td class="td-Blank15-16"></td>
              </tr>
          </table>
          <br />
           <table align = "center">
               <tr>
                  <td>
                      <asp:GridView ID="grd_info" runat="server" PageSize="3">
                          <Columns>
                              <asp:TemplateField HeaderText="del_chk">
                                  <ItemTemplate>
                                      <asp:CheckBox ID="CheckBox1" AutoPostBack="True" runat="server" />
                                  </ItemTemplate>
                              </asp:TemplateField>
                          </Columns>
                          <HeaderStyle CssClass="HeaderStyle" BackColor="#00FFCC"/>
                          <RowStyle BackColor="#E8FFCA" CssClass="RowStyle" Wrap="False" />
                      </asp:GridView>
                  </td>
               </tr>
               <tr>
                   <td align ="right">   
                    <asp:ImageButton ID="btnStart" runat="server" ImageUrl="~/icon/start.png" CssClass="imgbtn-back-start" />
                   </td>
               </tr>
           </table>
        <br />
        <asp:Button ID="BtnCancel" runat="server" Text="Button" style="display:none;"/>
        <asp:Button ID="BtnOK" runat="server" Text="Button" style="display:none;" />
      </div>
    </div>
    <div id="dialog" title="message" style="display:none;"> >
        <asp:Label ID="lblMsg" runat="server" Text=""></asp:Label>
    </div>
</asp:Content>
