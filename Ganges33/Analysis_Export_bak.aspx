<%@ Page Language="vb" AutoEventWireup="false" MasterPageFile="~/Analysis.Master" CodeBehind="Analysis_Export_bak.aspx.vb" Inherits="Ganges33.Analysis_Export_bak" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="ajaxToolkit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script src="http://ajax.googleapis.com/ajax/libs/jquery/1.7.0/jquery.min.js"></script>
    <link type="text/css" href="http://ajax.googleapis.com/ajax/libs/jqueryui/1/themes/start/jquery-ui.css" rel="stylesheet">
    <script type="text/javascript" src="http://ajax.googleapis.com/ajax/libs/jqueryui/1/jquery-ui.min.js"></script>
    <link type="text/css" href="CSS/Common/Analysis_Export_bak.css" rel="stylesheet" /> 

        <style type="text/css">
        </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="div-backgroungimage" style="background-image: url('pagewall_2/wall_analysis-2.png')">
      <div class="div-EntirePage">
        <br />
          <table class="table-EntirePage">
              <tr>
                  <td class="td-Blank1"></td>
                  <td class="td-btnAnalysis">
                      <asp:ImageButton ID="btnAnalysis" runat="server" CssClass="img-btnAnalysis" ImageUrl="~/icon/analysis.png" />
                  </td>
                  <td class="td-lblAnalysisfileExport">
                      <asp:Label ID="Label3" runat="server" CssClass="lblAnalysisFileExport" Text="Analysis file Export"></asp:Label>
                  </td>
                  <td class="td-Blank2"></td>
                  <td class="td-Blank3"></td>
                  <td class="td-Blank4"></td>
              </tr>
              <tr>
                  <td class="td-Blank5-Blank6"></td>
                  <td colspan = "2">
                      <asp:Label ID="Label1" runat="server" Text="current location : " CssClass="fontFamily"></asp:Label>
                      <asp:Label ID="lblLoc" runat="server" CssClass="fontFamily"></asp:Label>
                  </td>
                  <td class="td-Blank7-Blank8"></td>
                  <td class="td-Blank9-Blank10"></td>
                  <td class="td-Blank11-Blank12"></td>
              </tr>
              <tr>
                  <td class="td-Blank5-Blank6"></td>
                  <td colspan = "2">
                      <asp:Label ID="Label2" runat="server" Text="current username : " CssClass="fontFamily"></asp:Label>
                      <asp:Label ID="lblName" runat="server" CssClass="fontFamily"></asp:Label>
                  </td>
                  <td class="td-Blank7-Blank8"></td>
                  <td class="td-Blank9-Blank10"></td>
                  <td class="td-Blank11-Blank12"></td>
              </tr>
              <tr>
                  <td class="td-Blank13"></td>
                  <td colspan = "2">
                      <asp:Label ID="Label4" runat="server" CssClass="fontFamily" Text="Target Store"></asp:Label>
                      <span class="fontFamily">&nbsp;&nbsp; </span>
                      <asp:DropDownList ID="DropListLocation" runat="server" CssClass="Doplistlocation-DropDownExportFile">
                      </asp:DropDownList>
                  </td>
                  <td class="td-Blank14"></td>
                  <td class="td-Blank15">
                      <asp:Label ID="Label5" runat="server" CssClass="fontFamily" Text="export file"></asp:Label>
                      <span class="fontFamily">&nbsp;&nbsp; </span>
                      <asp:DropDownList ID="DropDownExportFile" runat="server" CssClass="Doplistlocation-DropDownExportFile">
                      </asp:DropDownList>
                  </td>
                  <td class="td-Blank16"></td>
              </tr>
              <tr>
                  <td class="td-Blank17"></td>
                  <td class="td-Blank18"></td>
                  <td class="td-Blank19" align = "right">
                      </td>
                  <td class="td-Blank20"></td>
                  <td class="td-Blank21"></td>
                  <td class="td-Blank22"></td>
              </tr>
              <tr>
                  <td class="td-Blank23"></td>
                  <td class="td-Blank24" colspan = "4">
                      <asp:Label ID="Label6" runat="server" Text="month：" CssClass="fontFamily"></asp:Label>
                      &nbsp;&nbsp;
                      <asp:DropDownList ID="DropDownMonth" runat="server" CssClass="DropDownMonth">
                      </asp:DropDownList>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                       <asp:ScriptManager ID="ScriptManager1" runat="server">
                      </asp:ScriptManager>
                      <asp:Label ID="Label7" runat="server" CssClass="fontFamily" Text=" Date：　From"></asp:Label>
                      <asp:TextBox ID="TextDateFrom" runat="server" CssClass="fontFamily"></asp:TextBox>&nbsp;
                      <ajaxToolkit:CalendarExtender ID="TextDateFrom_CalendarExtender" runat="server" BehaviorID="TextDateFrom_CalendarExtender" TargetControlID="TextDateFrom" PopupPosition="Left">
                      </ajaxToolkit:CalendarExtender>
                      <asp:Label ID="Label8" runat="server" CssClass="fontFamily" Text="To"></asp:Label>
                      <asp:TextBox ID="TextDateTo" runat="server" CssClass="fontFamily"></asp:TextBox>
                      <ajaxToolkit:CalendarExtender ID="TextDateTo_CalendarExtender" runat="server" BehaviorID="TextDateTo_CalendarExtender" TargetControlID="TextDateTo" PopupPosition="right">
                      </ajaxToolkit:CalendarExtender>
                  </td>
                  <td class="td-Blank25"></td>
              </tr>
              <tr>
                  <td class="td-Blank26">&nbsp;</td>
                  <td class="td-Blank27">&nbsp;</td>
                  <td class="td-Blank28">&nbsp;</td>
                  <td class="td-Blank29">&nbsp;</td>
                  <td class="td-Blank30" align = "right">
                    <asp:ImageButton ID="btnSend" runat="server" ImageUrl="~/icon/send.png" CssClass="btnSend" />
                  </td>
                  <td>&nbsp;</td>
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
