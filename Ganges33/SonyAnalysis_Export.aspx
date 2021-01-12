<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/SonyAnalysis.Master" CodeBehind="SonyAnalysis_Export.aspx.vb" Inherits="Ganges33.SonyAnalysis_Export" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="ajaxToolkit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
        <script src="http://ajax.googleapis.com/ajax/libs/jquery/1.7.0/jquery.min.js"></script>
    <link type="text/css" href="http://ajax.googleapis.com/ajax/libs/jqueryui/1/themes/start/jquery-ui.css" rel="stylesheet">
    <script type="text/javascript" src="http://ajax.googleapis.com/ajax/libs/jqueryui/1/jquery-ui.min.js"></script>

        <style type="text/css">
        .auto-style6 {
            z-index: 1;
            left: 38px;
            top: 122px;
            position: absolute;
            height: 560px;
            width: 1255px;
            background-size: contain;
            }
            .auto-style7 {
                height: 100%;
                background: rgba(255,255,255,0.8);
                /*opacity: 0.5;*/
            }
            .auto-style135 {
                width: 100%;
                height: 481px;
            }
            .auto-style136 {
                width: 101px;
            }
            .auto-style137 {
                width: 101px;
                height: 99px;
            }
            .auto-style138 {
                height: 99px;
            }
            .auto-style139 {
                height: 99px;
                width: 107px;
            }
            .auto-style140 {
                width: 107px;
                font-family: "Meiryo UI";
            }
            .auto-style141 {
                height: 99px;
                width: 363px;
            }
            .auto-style145 {
                width: 101px;
                height: 36px;
            }
            .auto-style149 {
                height: 36px;
            }
            .auto-style150 {
                width: 101px;
                height: 51px;
            }
            .auto-style154 {
                height: 51px;
            }
            .auto-style160 {
                width: 101px;
                height: 9px;
            }
            .auto-style161 {
                width: 107px;
                height: 9px;
            }
            .auto-style168 {
                height: 51px;
                width: 470px;
            }
            .auto-style173 {
                width: 363px;
                font-family: "Meiryo UI";
            }
            .auto-style174 {
                height: 99px;
                width: 470px;
            }
            .auto-style175 {
                width: 470px;
                height: 36px;
            }
            .auto-style179 {
                height: 99px;
                width: 102px;
            }
            .auto-style180 {
                width: 102px;
                height: 36px;
            }
            .auto-style181 {
                width: 102px;
                height: 51px;
            }
            .auto-style184 {
                width: 102px;
                font-family: "Meiryo UI";
            }
            .auto-style185 {
                width: 470px;
            }
            .auto-style186 {
                font-family: "Meiryo UI";
            }
            .auto-style187 {
                font-family: "Meiryo UI";
                font-size: xx-large;
            }
            .auto-style193 {
                height: 9px;
            }
        </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
  <asp:ScriptManager ID="ScriptManager1" runat="server"  >  </asp:ScriptManager>
    <div class="auto-style6" style="background-image: url('pagewall_2/wall_analysis-2.png')">
      <div class="auto-style7">
        <br />
          
                <div>
          <table class="auto-style135">
              <%--<tr>--%>
                  <td class="auto-style137"></td>
                  <td class="auto-style139">
                      <asp:ImageButton ID="btnAnalysis" runat="server" Height="108px" ImageUrl="~/icon/analysis.png" Width="108px" />
                  </td>
                  <td class="auto-style141">
                      <asp:Label ID="Label3" runat="server" CssClass="auto-style187" Text="Sony File Export"></asp:Label>
                  </td>
                  <td class="auto-style179"></td>
                  <td class="auto-style174"></td>
                  <td class="auto-style138"></td>
              </tr>
              <tr>
                  <td class="auto-style145"></td>
                  <td colspan = "2">
                      <asp:Label ID="Label1" runat="server" Text="Current Location : " CssClass="auto-style186"></asp:Label>
                      <asp:Label ID="lblLoc" runat="server" CssClass="auto-style186"></asp:Label>
                  </td>
                  <td class="auto-style180"></td>
                  <td class="auto-style175"></td>
                  <td class="auto-style149"></td>
              </tr>
              <tr>
                  <td class="auto-style145"></td>
                  <td colspan = "2">
                      <asp:Label ID="Label2" runat="server" Text="Current Username : " CssClass="auto-style186"></asp:Label>
                      <asp:Label ID="lblName" runat="server" CssClass="auto-style186"></asp:Label>
                  </td>
                  <td class="auto-style180"></td>
                  <td class="auto-style175"></td>
                  <td class="auto-style149"></td>
              </tr>
                          <tr>
                  <td class="auto-style150"></td>
                  <td colspan = "2">
                      <asp:Label ID="Label4" runat="server" CssClass="auto-style186" Text="Target Store"></asp:Label>
                      <span class="auto-style186">&nbsp;&nbsp; </span>
                      <asp:DropDownList ID="DropListLocation" runat="server" CssClass="auto-style186" Height="33px" Width="303px" >
                   
                          </asp:DropDownList>
                  </td>
                  <td class="auto-style181"></td>
                  <td class="auto-style168">
                      <asp:Label ID="Label5" runat="server" CssClass="auto-style186" Text="Export File"></asp:Label>
                      <span class="auto-style186">&nbsp;&nbsp; </span>
                    
                      <asp:DropDownList ID="DropDownExportFile" runat="server" CssClass="auto-style186" Height="33px" Width="303px" Visible="false" >
                      </asp:DropDownList>
                           
                         <asp:DropDownList ID="drpTaskExport" runat="server" Height="32px" Width="380px"  AutoPostBack="true"  >
                              <asp:ListItem Text="Select..." Value="-1"></asp:ListItem>
              

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
                                <asp:ListItem Text="114.Daily Accumulated KPI Report" Value="114"></asp:ListItem>
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

                             
                            
                      </asp:DropDownList>
                  </td>
                  <td class="auto-style154"></td>
              </tr>
           
              <tr>
                  <td class="auto-style160"></td>

                  <td class="auto-style161" colspan = "4">
                      <asp:Label ID="Label6" runat="server" Text="Month：" CssClass="auto-style186"></asp:Label>   
                      &nbsp;&nbsp;
                      <asp:DropDownList ID="DropDownMonth" runat="server" CssClass="auto-style186" Height="24px" Width="169px" >
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
                      <asp:DropDownList ID="DropDownYear" runat="server" CssClass="auto-style186" Height="24px" Width="60px" >
                            <asp:ListItem Text="2019" Value="2019"></asp:ListItem>
                              <asp:ListItem Text="2020" Value="2020" Selected="True"></asp:ListItem>
                              <asp:ListItem Text="2021" Value="2021"></asp:ListItem>
                             <asp:ListItem Text="2022" Value="2022"></asp:ListItem>
                              <asp:ListItem Text="2023" Value="2023"></asp:ListItem>
                               <asp:ListItem Text="2024" Value="2024"></asp:ListItem>
                             <asp:ListItem Text="2025" Value="2025"></asp:ListItem>
                         
                      </asp:DropDownList>
                      		<asp:DropDownList ID="DropDownDaySub" runat="server" CssClass="auto-style186" Height="24px" Width="60px" Visible="false">
                              <asp:ListItem Text="Select Day..." Value="0"></asp:ListItem>
                            <asp:ListItem Text="01" Value="01"></asp:ListItem>
                              <asp:ListItem Text="11" Value="11"></asp:ListItem>
                              <asp:ListItem Text="21" Value="21"></asp:ListItem>
               </asp:DropDownList>
                      <asp:DropDownList ID="DropDownDTSub" runat="server" CssClass="auto-style186" Height="24px" Width="60px" Visible="false" >
                              <asp:ListItem Text="Select..." Value="0"></asp:ListItem>
                            <asp:ListItem Text="DT1" Value="DT1"></asp:ListItem>
                              <asp:ListItem Text="DT2" Value="DT2"></asp:ListItem>
                 </asp:DropDownList>
                          <asp:DropDownList ID="DropDownGR" runat="server" CssClass="auto-style186" Height="24px" Width="100px" Visible="false" >
                            <asp:ListItem Text="Summary" Value="GR"></asp:ListItem>
                              <asp:ListItem Text="Detail" Value="GD"></asp:ListItem>
                 </asp:DropDownList>

                      &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                          
                      <asp:Label ID="Label7" runat="server" CssClass="auto-style186" Text=" Date：　From"></asp:Label>
                      <asp:TextBox ID="TextDateFrom" runat="server" CssClass="auto-style186"></asp:TextBox>&nbsp;
                      <ajaxToolkit:CalendarExtender ID="TextDateFrom_CalendarExtender" runat="server" BehaviorID="TextDateFrom_CalendarExtender" TargetControlID="TextDateFrom" PopupPosition="Left">
                      </ajaxToolkit:CalendarExtender>
                      <asp:Label ID="Label8" runat="server" CssClass="auto-style186" Text="To"></asp:Label>
                      <asp:TextBox ID="TextDateTo" runat="server" CssClass="auto-style186"></asp:TextBox>
                      <ajaxToolkit:CalendarExtender ID="TextDateTo_CalendarExtender" runat="server" BehaviorID="TextDateTo_CalendarExtender" TargetControlID="TextDateTo" PopupPosition="right">
                      </ajaxToolkit:CalendarExtender> 
                  &nbsp;
                     
                  </td>
                  <td style="text-align:left" class="auto-style193">
                          <asp:ImageButton ID="btnExport" runat="server" Height="29px" ImageUrl="~/icon/btnExport.png" Width="81px" CssClass="auto-style186" />
                    <asp:ImageButton ID="btnSend" runat="server" Height="29px" ImageUrl="~/icon/send.png" Width="81px" CssClass="auto-style186" Visible="false" />
                       </td>
              </tr>
                                  
         
              <tr>
                  <td class="auto-style136">&nbsp;</td>
                  <td class="auto-style140">&nbsp;</td>
                  <td class="auto-style173">&nbsp;</td>
                  <td class="auto-style184">&nbsp;</td>
                  <td class="auto-style185" align = "right">
                     
                  
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
    <div>

         
        </div>
    </div>
</asp:Content>
