<%@ Page Title="QMVQR-Analysis_Recovery" Language="vb" AutoEventWireup="false" MasterPageFile="~/Analysis.Master" CodeBehind="Analysis_Recovery.aspx.vb" Inherits="Ganges33.Analysis_Recovery" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="ajaxToolkit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script src="http://ajax.googleapis.com/ajax/libs/jquery/1.7.0/jquery.min.js"></script>
    <link type="text/css" href="http://ajax.googleapis.com/ajax/libs/jqueryui/1/themes/start/jquery-ui.css" rel="stylesheet">
    <script type="text/javascript" src="http://ajax.googleapis.com/ajax/libs/jqueryui/1/jquery-ui.min.js"></script>

    <link type="text/css" href="CSS/Common/Analysis_Recovery.css" rel="stylesheet" /> 
        <style type="text/css">
                     
            </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="div-bgImg" style="background-image: url('pagewall_2/wall_analysis-2.png')">
      <div class="div-EntirePage">
          <table class="table-EntirePage">
              <tr>
                  <td class="td-Blank1"></td>
                  <td class="td-btnAnalysis">
                      <asp:ImageButton ID="btnAnalysis" runat="server" CssClass="img-btnAnalysis" ImageUrl="~/icon/analysis.png" />
                  </td>
                  <td class="td-lbl3" colspan = "2">
                      <asp:Label ID="Label3" runat="server" CssClass="lbl3-AnalysisRecoveryData" Text="Analysis Recovery Data"></asp:Label>
                  </td>
                  <td class="td-Blank2"></td>
                  <td class="td-Blank3"></td>
                  <td class="td-Blank4"></td>
              </tr>
              <tr>
                  <td class="td-Blank-5-6-7"></td>
                  <td class="td-Blank8-9-10"></td>
                  <td class="td-lbl-4-5-6-DropListUploadFile-DropListLocation">
                      <asp:Label ID="Label4" runat="server" CssClass="lbl-4-5-6" Text="upload date"></asp:Label>
                  </td>
                  <td class="td-Script-FromUpDate-CalenderExternal-lbl1-toUpdate-textToCalenderExternal" colspan = "3">
                      <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
                      <asp:TextBox ID="TextFromUpDate" runat="server" CssClass="txtboxUpdate-txttoUpdate"></asp:TextBox>
                      <ajaxToolkit:CalendarExtender ID="TextFromUpDate_CalendarExtender" runat="server" BehaviorID="TextBox1_CalendarExtender" TargetControlID="TextFromUpDate">
                      </ajaxToolkit:CalendarExtender>
                      <asp:Label ID="Label1" runat="server" Text="　～　"></asp:Label>
                      <asp:TextBox ID="TextToUpDate" runat="server" CssClass="txtboxUpdate-txttoUpdate"></asp:TextBox>
                      <ajaxToolkit:CalendarExtender ID="TextToUpDate_CalendarExtender" runat="server" BehaviorID="TextBox2_CalendarExtender" TargetControlID="TextToUpDate">
                      </ajaxToolkit:CalendarExtender>
　　　　　　　　　</td>
                  <td class="td-Blank-14-15-16"></td>
              </tr>
              <tr>
                  <td class="td-Blank-5-6-7"></td>
                  <td class="td-Blank8-9-10"></td>
                  <td class="td-lbl-4-5-6-DropListUploadFile-DropListLocation">
                      <asp:Label ID="Label5" runat="server" CssClass="lbl-4-5-6" Text="upload data"></asp:Label>
                  </td>
                  <td class="td-lbl-4-5-6-DropListUploadFile-DropListLocation">
                      <asp:DropDownList ID="DropListUploadFile" runat="server" CssClass="DropListUploadFile"></asp:DropDownList>
                      </td>
                  <td class="td-Blank-11-12"></td>
                  <td class="td-Blank13"></td>
                  <td class="td-Blank-14-15-16"></td>
              </tr>
              <tr>
                  <td class="td-Blank-5-6-7"></td>
                  <td class="td-Blank8-9-10"></td>
                  <td class="td-lbl-4-5-6-DropListUploadFile-DropListLocation">
                      <asp:Label ID="Label6" runat="server" CssClass="lbl-4-5-6" Text="Branch"></asp:Label>
                  </td>
                  <td class="td-lbl-4-5-6-DropListUploadFile-DropListLocation">
                      <asp:DropDownList ID="DropListLocation" runat="server" CssClass="dropdownList">
                      </asp:DropDownList>
                  </td>
                  <td class="td-Blank-11-12"></td>
                  <td align = "right" class="td-Blank17">
                      &nbsp;</td>
                  <td class="td-Blank-14-15-16"></td>
              </tr>
              <tr>
                  <td class="td-Blank18"></td>
                  <td class="td-Blank19"></td>
                  <td class="td-Blank20-21"></td>
                  <td class="td-Blank20-21"></td>
                  <td class="td-Blank22"></td>
                  <td class="td-Blank23" align = "right">
                    <asp:ImageButton ID="btnSearch" runat="server" ImageUrl="~/icon/search.png" CssClass="imgbtn-Search" />
                  </td>
                  <td class="td-Blank24"></td>
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
