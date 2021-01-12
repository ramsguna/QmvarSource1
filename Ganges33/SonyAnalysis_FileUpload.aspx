<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/SonyAnalysis.Master" CodeBehind="SonyAnalysis_FileUpload.aspx.vb" Inherits="Ganges33.SonyAnalysis_FileUpload" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="ajaxToolkit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script src="http://ajax.googleapis.com/ajax/libs/jquery/1.7.0/jquery.min.js"></script>
    <link type="text/css" href="http://ajax.googleapis.com/ajax/libs/jqueryui/1/themes/start/jquery-ui.css" rel="stylesheet">
    <script type="text/javascript" src="http://ajax.googleapis.com/ajax/libs/jqueryui/1/jquery-ui.min.js"></script>
    <link type="text/css" href="CSS/Common/Analysis_FileUploaded_1.css" rel="stylesheet" />
        <style type="text/css">
        
        </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
    <div class="div-background-image" style="background-image: url('pagewall_2/wall_analysis-2.png')">
      <div class="div-Entirepage">
        <br />           
                <div>
          <table class="table-EntirePage">
              <tr>
                  <td class="td-Blank1"></td>
                  <td class="td-btnAnalysis">
                      <asp:ImageButton ID="btnAnalysis" runat="server" CssClass="btn-Analysis" ImageUrl="~/icon/analysis.png" />
                  </td>
                  <td class="td-AnalysisFileUpload">
                      <asp:Label ID="Label3" runat="server" CssClass="lbl3-AnalysisFileUpload" Text="Sony File Upload"></asp:Label>
                  </td>
                  <td class="td-Blank2"></td>
                  <td class="td-Blank3"></td>
                  <td class="td-Blank4"></td>
              </tr>
              <tr>
                  <td class="td-Blank-5-6"></td>
                  <td class="td-lbl-1-2-Currentlocation-CurrentUsername" colspan = "2">
                      <asp:Label ID="Label1" runat="server" Text="Current Location : " CssClass="fontFamily"></asp:Label>
                      <asp:Label ID="lblLoc" runat="server" CssClass="fontFamily"></asp:Label>
                  </td>
                  <td class="td-Blank-7-8"></td>
                  <td class="td-lbl4-droplistlocation-td-Blank9">
                      <asp:Label ID="Label4" runat="server" CssClass="fontFamily" Text="Upload Branch"></asp:Label>&nbsp;&nbsp;&nbsp;
                      <asp:DropDownList ID="DropListLocation" runat="server" CssClass="droplistlocation">
                      </asp:DropDownList>
                  </td>
                  <td class="td-Blank-9-10"></td>
              </tr>
              <tr>
                  <td class="td-Blank-5-6"></td>
                  <td class="td-lbl-1-2-Currentlocation-CurrentUsername" colspan = "2">
                      <asp:Label ID="Label2" runat="server" Text="Current Username : " CssClass="fontFamily"></asp:Label>
                      <asp:Label ID="lblName" runat="server" CssClass="fontFamily"></asp:Label>
                      <br />
                  </td>
                  <td class="td-Blank-7-8"></td>
                  <td class="td-lbl4-droplistlocation-td-Blank9"></td>
                  <td class="td-Blank-9-10"></td>
              </tr>
              <tr>
                  <td class="td-Blank23"></td>
                  <td class="td-Blank24-DropDownList" colspan = "2" rowspan = "2">
                     
                                          <asp:DropDownList ID="drpTask" runat="server" CssClass="DropDownList" AutoPostBack="true"  OnSelectedIndexChanged="OnSelectedIndexChanged">
                                           <asp:ListItem Text="Select..." Value="0"></asp:ListItem>

                                
                                <asp:ListItem Text="101.In Bound " Value="101"></asp:ListItem>
                                <asp:ListItem Text="102.Out bound " Value="102"></asp:ListItem>
                                <asp:ListItem Text="103.PartOrderListReport" Value="103"></asp:ListItem>
                                <asp:ListItem Text="104.RPSI Inquiry" Value="104"></asp:ListItem>
                                <asp:ListItem Text="105.Stock Report" Value="105"></asp:ListItem>
                                <asp:ListItem Text="106.Date Wise Sales Report" Value="106"></asp:ListItem>
                                <asp:ListItem Text="107.Stock Available" Value="107"></asp:ListItem>
                                <asp:ListItem Text="108.AscGstTaxReport" Value="108"></asp:ListItem>
                                <asp:ListItem Text="109.ASCTaxReport" Value="109"></asp:ListItem>
                                <asp:ListItem Text="110.ASC_Tax_Report" Value="110"></asp:ListItem>
                                <asp:ListItem Text="111.ClaimInvoiceDetail" Value="111"></asp:ListItem>
                                <asp:ListItem Text="112.ASC Invoice Data" Value="112"></asp:ListItem>
                                <asp:ListItem Text="113.Daily Abandon" Value="113"></asp:ListItem>
                                <asp:ListItem Text="114.Daily Accumulated_KPI_Report" Value="114"></asp:ListItem>
                                <asp:ListItem Text="115.Daily_ASCPartsUsed" Value="115"></asp:ListItem>
                                <asp:ListItem Text="116.Daily_Claim" Value="116"></asp:ListItem>
                                <asp:ListItem Text="117.Daily_Delivered" Value="117"></asp:ListItem>
                                <asp:ListItem Text="118.Daily_OS_Reservation" Value="118"></asp:ListItem>
                                <asp:ListItem Text="119.Daily_Receiveset" Value="119"></asp:ListItem>



                               <asp:ListItem Text="120.Daily_OS_specialtreatment" Value="120"></asp:ListItem>
                              <asp:ListItem Text="121.Daily_Acct Stmt" Value="121"></asp:ListItem> 
                             <asp:ListItem Text="122.Daily_RepairsetSet_NP" Value="122"></asp:ListItem>
                             <asp:ListItem Text="123.Daily_UnDeliveredSet" Value="123"></asp:ListItem>
                              <asp:ListItem Text="124.Daily_UnRepaipairset_NP" Value="124"></asp:ListItem>
                              <asp:ListItem Text="125.Monthly Reservationvation" Value="125"></asp:ListItem>
                              <%--<asp:ListItem Text="135.Monthly Repairset" Value="135"></asp:ListItem>--%>
                             <asp:ListItem Text="126.Monthly Repairset" Value="126"></asp:ListItem>
                            <%--  <asp:ListItem Text="136.Monthly Abandon" Value="136"></asp:ListItem>--%>
                               <asp:ListItem Text="127.Monthly Abandon" Value="127"></asp:ListItem>
                               <%--<asp:ListItem Text="137.Monthly_SOMC_Claim" Value="137"></asp:ListItem>--%>
                             <asp:ListItem Text="128.Monthly_SOMC_Claim" Value="128"></asp:ListItem>



                                <%--<asp:ListItem Text="137.Monthly_SOMC_Claim" Value="137"></asp:ListItem>--%>




                      </asp:DropDownList>
 
                       <asp:DropDownList ID="DropDownMonth" runat="server" CssClass="dropdownmonth" Visible ="false" >
                        		     <asp:ListItem Text="Select..." Value="0"></asp:ListItem>
                             <asp:ListItem Text="January" Value="01"></asp:ListItem>
                              <asp:ListItem Text="February" Value="02"></asp:ListItem>
                              <asp:ListItem Text="March" Value="03"></asp:ListItem>
                             <asp:ListItem Text="April" Value="04"></asp:ListItem>
                              <asp:ListItem Text="May" Value="05"></asp:ListItem>
                               <asp:ListItem Text="June" Value="06"></asp:ListItem>
                             <asp:ListItem Text="July" Value="07"></asp:ListItem>
                              <asp:ListItem Text="August" Value="08"></asp:ListItem>
                              <asp:ListItem Text="September" Value="09"></asp:ListItem>
                              <asp:ListItem Text="October" Value="10"></asp:ListItem>
                              <asp:ListItem Text="November" Value="11"></asp:ListItem>
                              <asp:ListItem Text="December" Value="12"></asp:ListItem>
                      </asp:DropDownList> 
                      <asp:DropDownList ID="DropDownYear" runat="server" CssClass="dropdownyear" Visible ="false" >
                            <asp:ListItem Text="2019" Value="2019"></asp:ListItem>
                              <asp:ListItem Text="2020" Value="2020"></asp:ListItem>
                              <asp:ListItem Text="2021" Value="2021"></asp:ListItem>
                             <asp:ListItem Text="2022" Value="2022"></asp:ListItem>
                              <asp:ListItem Text="2023" Value="2023"></asp:ListItem>
                               <asp:ListItem Text="2024" Value="2024"></asp:ListItem>
                             <asp:ListItem Text="2025" Value="2025"></asp:ListItem>
                         
                      </asp:DropDownList>
                      <br /><br />
                      <asp:Label ID="Label10" runat="server" Text="Invoice Date" CssClass="fontFamily"></asp:Label> &nbsp;&nbsp;&nbsp
                      <asp:RadioButton ID="RadioBtnMM" runat="server" CssClass="fontFamily" AutoPostBack="True" Text="mm/dd/yyyy" /><span class="fontFamily">&nbsp;&nbsp;</span>&nbsp;&nbsp;<asp:RadioButton ID="RadioBtnDD" runat="server" AutoPostBack="True" CssClass="fontFamily" Text="dd/mm/yyyy" /><br /><br />
                      <asp:FileUpload ID="FileUploadAnalysis" runat="server" CssClass="FileUploadAnalysis"/>
                  </td>
                  <td class="td-Blank11"></td>
                  <td class="td-Blank12">
                     
                      <asp:Label ID="Label7" runat="server" Text="Parts Invoice No" CssClass="fontFamily"></asp:Label>&nbsp;&nbsp;&nbsp;
                      <asp:TextBox ID="TextPartsInvoiceNo" runat="server" CssClass="TextPartsInvoiceNo"></asp:TextBox> <br class="fontFamily" /><br />
                      <asp:Label ID="Label8" runat="server" Text="Labor Invoice No" CssClass="fontFamily"></asp:Label>&nbsp;&nbsp;
                      <asp:TextBox ID="TextLaborInvoiceNo" runat="server" CssClass="TextLaborInvoiceNo"></asp:TextBox><br class="fontFamily" /><br />
                      <asp:Label ID="Label9" runat="server" Text="Invoice Date(mm/dd/yyyy)" CssClass="fontFamily"></asp:Label>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp;&nbsp;
                      <asp:TextBox ID="TextInvoiceDate" runat="server" Height="23px" Width="160px"></asp:TextBox>&nbsp;&nbsp;&nbsp;<ajaxToolkit:CalendarExtender ID="TextInvoiceDate_CalendarExtender" runat="server" BehaviorID="TextInvoiceDate_CalendarExtender" TargetControlID="TextInvoiceDate" PopupPosition="Left">
                      </ajaxToolkit:CalendarExtender>
                      &nbsp;&nbsp;&nbsp; <br />
                  </td>
                  <td class="td-Blank13"></td>
              </tr>
              <tr>
                  <td class="td-Blank14"></td>
<%--                  <td  colspan = "2">
                      <asp:Label ID="Label10" runat="server" Text="Invoice Date" CssClass="auto-style186"></asp:Label> &nbsp;&nbsp;&nbsp
                      <asp:RadioButton ID="RadioBtnMM" runat="server" CssClass="auto-style186" AutoPostBack="True" Text="mm/dd/yyyy" /><span class="auto-style186">&nbsp;&nbsp;</span>&nbsp;&nbsp;<asp:RadioButton ID="RadioBtnDD" runat="server" AutoPostBack="True" CssClass="auto-style186" Text="dd/mm/yyyy" />
                  </td>--%>
                  <td class="td-Blank15"  align = "right" >                      
                  </td>
                  <td align = "left" style="vertical-align:middle">
                       <asp:Label ID="lblDate" runat="server" CssClass="fontFamily">Date</asp:Label>
                     <asp:TextBox ID="txtDate" runat="server" Width="100px"></asp:TextBox>
                      <ajaxToolkit:CalendarExtender ID="txtDate_CalendarExtender" runat="server" Format="MM/dd/yyyy" BehaviorID="txtDate_CalendarExtender" TargetControlID="txtDate" PopupPosition="Left">
                      </ajaxToolkit:CalendarExtender>
                      &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                      <asp:Label ID="lblAmount" runat="server" CssClass="fontFamily">Amount</asp:Label>&nbsp;&nbsp;<asp:TextBox ID="txtAmount" runat="server" Width="100px"></asp:TextBox>
                        <br /><br />
                      <asp:Label ID="lblArNo" runat="server" CssClass="fontFamily" Text="Advice Reference No"></asp:Label>&nbsp;&nbsp;<asp:TextBox ID="txtArNo" runat="server" Text="" Enabled="false" ></asp:TextBox>
                  </td>
                  <td class="td-btnUpload" align = "left"><asp:ImageButton ID="btnUpload" runat="server" ImageUrl="~/icon/import.png" CssClass="btnUpload" /></td>
              </tr>
              <tr><td colspan="4" style="padding-top:10px;"></td></tr>
              <tr>
                  <td class="td-Blank16"></td>
                  <td class="td-lbl5-listmsg" colspan = "3">
                      <asp:Label ID="Label5" runat="server" Text="Message" CssClass="fontFamily"></asp:Label> <br />
                      <asp:ListBox ID="ListMsg" runat="server" CssClass="listbox"></asp:ListBox>
                  </td>
                  <td class="td-lbl6-ListHistory">
                      <asp:Label ID="Label6" runat="server" Text="History" CssClass="fontFamily"></asp:Label> <br />
                      <asp:ListBox ID="ListHistory" runat="server" CssClass="ListHistory"></asp:ListBox>
                  </td>
                  <td class="td-Blank17"></td>
              </tr>
              <tr>
                  <td class="td-Blank18">&nbsp;</td>
                  <td class="td-Blank19">&nbsp;</td>
                  <td class="td-Blank20">&nbsp;</td>
                  <td class="td-Blank21">&nbsp;</td>
                  <td class="td-Blank22">
                  </td>
                  <td>&nbsp;</td>
              </tr>
          </table>
        </div>                   
        <asp:Button ID="BtnCancel" runat="server" Text="Button" style="display:none;"/>
        <asp:Button ID="BtnOK" runat="server" Text="Button" style="display:none;" />
      </div>
   </div>
    <div id="dialog" title="message" style="display:none;"> >
        <asp:Label ID="lblMsg" runat="server" Text=""></asp:Label>
    </div>
</asp:Content>
