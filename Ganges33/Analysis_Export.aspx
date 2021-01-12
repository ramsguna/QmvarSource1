<%@ Page Title="QMVAR-AnalysisExport" Language="vb" AutoEventWireup="false" MasterPageFile="~/Analysis.Master" CodeBehind="Analysis_Export.aspx.vb" Inherits="Ganges33.Analysis_Export" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="ajaxToolkit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
   
    <script src="http://ajax.googleapis.com/ajax/libs/jquery/1.7.0/jquery.min.js"></script>
    <link type="text/css" href="http://ajax.googleapis.com/ajax/libs/jqueryui/1/themes/start/jquery-ui.css" rel="stylesheet">
     <script type="text/javascript" src="http://ajax.googleapis.com/ajax/libs/jqueryui/1/jquery-ui.min.js"></script>

   

    <link href="assets/jquery-ui_theme.css" rel="stylesheet" />
    <link href="assets/jquery-ui.css" rel="stylesheet" />
    <script type="text/javascript"  src="assets/jquery-ui.min_lips.js"></script>


    <link href="assets/css/material-dashboard.css" rel="stylesheet" />  
    <link href="assets/css/material-dashboard-rtl.css" rel="stylesheet" />
    <link href="assets/css/material-dashboard.min.css" rel="stylesheet" />
    <meta charset="utf-8" />
  <link rel="apple-touch-icon" sizes="76x76" href="../assets/img/apple-icon.png">
  <link rel="icon" type="image/png" href="../assets/img/favicon.png">
  <meta http-equiv="X-UA-Compatible" content="IE=edge,chrome=1" />
  
  <meta content='width=device-width, initial-scale=1.0, shrink-to-fit=no' name='viewport' />
  <!--     Fonts and icons     -->
  <link rel="stylesheet" type="text/css" href="https://fonts.googleapis.com/css?family=Roboto:300,400,500,700|Roboto+Slab:400,700|Material+Icons" />
  <link rel="stylesheet" href="https://maxcdn.bootstrapcdn.com/font-awesome/latest/css/font-awesome.min.css">
  <!-- CSS Files -->
   <link href="assets/css/material-dashboard.css" rel="stylesheet" /> 
  <!-- CSS Just for demo purpose, don't include it in your project -->
  <link href="assets/demo/demo.css" rel="stylesheet" />
    <style type="text/css">
 
        </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
    

  <<div class="wrapper  col-sm-12 sidebar-wrapper position-fixed scrolbar contain" >
   
    <div class="content" >
        <div class="container-fluid">
          <div class="row">
            <div class="col-md-12">
              
              <div class="card">
                <div class="card-header card-header-primary">
                  <h3 class="card-title ">Analysis File Export</h3>
                  <p class="card-category"></p>
                </div>
                <div class="card-body scrollbar" id="style-10" >
                 
                   <div style="height:45px">

                </div>

                       
                     
                     
                      
               <div  class="col-sm-12">
					
                    <div class="row">
                   
                      <div class="col-md-6">
                              <div class="row">
                                  <div class="col-sm-3">
                         <label  class="fontFamily">Target Store</label>
                                      </div>
                                  <div class="col-sm-8">
                       <asp:DropDownList ID="DropListLocation" runat="server" CssClass="form-control dropdown-toggle" style="width: 100%; height:33px" >
                   
                          </asp:DropDownList>
                                      </div>
                    </div>
				  
      <div class="row  ">
                        <div class="form-group col-sm-3 ">
                                 <br />
                     <label id="" class="fontFamily">Month</label>
                            </div>
                            <div class="col-sm-4">
                                <br />

				  <asp:DropDownList ID="DropDownMonth" runat="server" CssClass="form-control dropdown-toggle" style="width: 100%; height:33px" >
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
                         </div> 
					 
                        
                          <div class="col-sm-2">
                                 <br />  
					<asp:DropDownList ID="DropDownYear" runat="server" CssClass="form-control dropdown-toggle"   style="width: 130%; height:33px" >
                            <asp:ListItem Text="2019" Value="2019"></asp:ListItem>
                              <asp:ListItem Text="2020" Value="2020" Selected="True"></asp:ListItem>
                              <asp:ListItem Text="2021" Value="2021"></asp:ListItem>
                             <asp:ListItem Text="2022" Value="2022"></asp:ListItem>
                              <asp:ListItem Text="2023" Value="2023"></asp:ListItem>
                               <asp:ListItem Text="2024" Value="2024"></asp:ListItem>
                             <asp:ListItem Text="2025" Value="2025"></asp:ListItem>
                         
                      </asp:DropDownList>

                        </div>
					  
					
          
           
                          <div class="col-sm-2">
                                   <br />
				 <asp:DropDownList ID="DropDownDaySub" runat="server" CssClass="form-control dropdown-toggle"  style="width: 100%; height:33px;"  Visible="false">
                              <asp:ListItem Text="Select Day..." Value="0"></asp:ListItem>
                            <asp:ListItem Text="01" Value="01"></asp:ListItem>
                              <asp:ListItem Text="11" Value="11"></asp:ListItem>
                              <asp:ListItem Text="21" Value="21"></asp:ListItem>
               </asp:DropDownList>
                                  
                      <asp:DropDownList ID="DropDownDTSub" runat="server" CssClass="form-control dropdown-toggle"  style="width: 100%; height:33px;" Visible="false" >
                              <asp:ListItem Text="Select..." Value="0"></asp:ListItem>
                            <asp:ListItem Text="DT1" Value="DT1"></asp:ListItem>
                              <asp:ListItem Text="DT2" Value="DT2"></asp:ListItem>
                 </asp:DropDownList>
                          <asp:DropDownList ID="DropDownGR" runat="server" CssClass="form-control dropdown-toggle"  style="width: 100%; height:33px;" Visible="false" >
                            <asp:ListItem Text="GR" Value="GR"></asp:ListItem>
                              <asp:ListItem Text="GD" Value="GD"></asp:ListItem>
                 </asp:DropDownList>
                         </div>
					  </div>
					
                     
                        </div>     
                     
                     
					<div class="col-sm-6 ">
                    <div class="row">
                        <div class="col-sm-3">
                      <label  class="bmd-label-floating ">Export File</label>
                            </div>
                        <div class="col-sm-9">
                             <asp:DropDownList ID="DropDownExportFile" runat="server" CssClass="form-control dropdown-toggle" Height="33px" Width="303px" Visible="false" >
                      </asp:DropDownList>
                           
                         <asp:DropDownList ID="drpTaskExport" CssClass="form-control dropdown-toggle" runat="server" style="width: 100%; height:33px;" AutoPostBack="true"  >
                              <asp:ListItem Text="Select..." Value="-1"></asp:ListItem>
                             <asp:ListItem Text="0.PL_Tracking_Sheet" Value="0"></asp:ListItem>
                              <asp:ListItem Text="1.DailyRepairStatement" Value="1"></asp:ListItem>
                              <asp:ListItem Text="2A.Sales Register from GSPIN software for In warranty" Value="2.1"></asp:ListItem>
                              <asp:ListItem Text="2B.Sales Register from GSPIN software for Out warranty" Value="2.2"></asp:ListItem>
                              <asp:ListItem Text="2C.Sales Register from GSPIN software for Other Sales" Value="2.3"></asp:ListItem>

                             <asp:ListItem Text="3A.Sales Register from GSPN Samsung_C IW" Value="3"></asp:ListItem>
                               <asp:ListItem Text="3B.Sales Register from GSPN Samsung_EXC IW" Value="-3"></asp:ListItem>
                              <asp:ListItem Text="4.Sales Register from GSPN OOW" Value="4"></asp:ListItem>
                               <asp:ListItem Text="5.Debit Note" Value="5"></asp:ListItem>
                             <asp:ListItem Text="6.Purchase Register Summary" Value="6"></asp:ListItem>
                              <asp:ListItem Text="7.Purchase Register Detail" Value="7"></asp:ListItem>
                              <asp:ListItem Text="8.Goods Receiving" Value="8"></asp:ListItem>
                              <asp:ListItem Text="9.Stockoverview" Value="9"></asp:ListItem>
                              <asp:ListItem Text="9A.StockoverView Count" Value="9A"></asp:ListItem>
                              <asp:ListItem Text="10.SAW Discount" Value="10"></asp:ListItem>
                             <asp:ListItem Text="11.Parts In and Out History" Value="11"></asp:ListItem>
                              <asp:ListItem Text="12.Fixed Asset" Value="12"></asp:ListItem>
                              <asp:ListItem Text="13.Consumable Purchase List" Value="13"></asp:ListItem>
                             <asp:ListItem Text="14.GSS Paid to Samsung" Value="14"></asp:ListItem>
                             <asp:ListItem Text="15.Return Credit" Value="15"></asp:ListItem>
                              <asp:ListItem Text="16.Samsung Ledger" Value="16"></asp:ListItem>
                             <%--<asp:ListItem Text="18.Debit Note Register & Service Part Return" Value="18A"></asp:ListItem>-VJ 2019/10/14--%>
                             <asp:ListItem Text="18.Debit Note Register" Value="18"></asp:ListItem><%--VJ 2019/10/14--%>
                             <asp:ListItem Text="19.Service Part Return" Value="19"></asp:ListItem><%--VJ 2019/10/14--%>
                            <asp:ListItem Text="19A.PO Confirmation" Value="19A"></asp:ListItem>
                             <asp:ListItem Text="19B.HSN Code" Value="19B"></asp:ListItem><%--VJ 2019/10/18--%>
                            <asp:ListItem Text="20.OtherSalesExtendedWarranty" Value="20"></asp:ListItem>
                            <asp:ListItem Text="21.PO Status" Value="21"></asp:ListItem>
                            <asp:ListItem Text="22.Activity Report" Value="22"></asp:ListItem>
                             <asp:ListItem Text="23.PO Confirmation" Value="23"></asp:ListItem>
                            <asp:ListItem Text="24.Samsung to GSS paid (BOI)" Value="24"></asp:ListItem>							 
							 <asp:ListItem Text="25.Account Report" Value="25"></asp:ListItem>
                             <asp:ListItem Text="99.Final_Report" Value="99"></asp:ListItem> <%--VJ 2019/10/14--%>
                             
                            
                      </asp:DropDownList>
                        </div>
                        </div>
                     
                 
				
		
					<div class="row">
                        <div class=" col-sm-5">
                        <div class="row" runat="server" ID="Label8">
                           <br />
                            <div class="col-sm-8">
                                <br />
              <label  class="bmd-label-floating "> Date:
              </label> 

                            </div> 
                          
                            <div > 
                                   <br />
                                <label class="bmd-label-floating ">
                                    From
                                </label>
                            </div>
                            </div>
              </div>
                       
                        <div class="col-sm-7">
                             <br />
                           
                 <asp:TextBox ID="TextDateFrom" runat="server" class="form-file-upload  date serverlbl" AutoCompleteType="Disabled" style="width: 40%;"></asp:TextBox>&nbsp;&nbsp;&nbsp;
                      <%--<ajaxToolkit:CalendarExtender ID="TextDateFrom_CalendarExtender" runat="server" BehaviorID="TextDateFrom_CalendarExtender" TargetControlID="TextDateFrom" PopupPosition="Left">
                      </ajaxToolkit:CalendarExtender> --%>             
              <label runat="server" ID="Label7" class="bmd-label-floating fontFamily">To</label>
            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                <asp:TextBox ID="TextDateTo" runat="server" class="form-file-upload date serverlbl" AutoCompleteType="Disabled" style="width: 40%;"></asp:TextBox>
                     <%-- <ajaxToolkit:CalendarExtender ID="TextDateTo_CalendarExtender" runat="server" BehaviorID="TextDateTo_CalendarExtender" TargetControlID="TextDateTo" PopupPosition="right">
                      </ajaxToolkit:CalendarExtender>--%>
                            </div>
           </div>
                        </div>
                        </div>
                          
					   </div>
					   <div>
			</div>
					               <div class="col-sm-12">
                      <asp:Button runat="server" ID="btnExport" class="btn btn-primary pull-right" Text="Export" />
                        </div>
                      <div class="clearfix"></div>
               </div>
                    </div>
        
                 </div>
                </div>
              </div>
            </div>
            </div>
          
    <div id="dialog" title="message" style="display:none;"> >
        <asp:Label ID="lblMsg" runat="server" Text=""></asp:Label>
    </div>
        </body>
    </html>
    <div style="visibility:hidden">
         <div class="form-group row">
                            <div class="col-sm-2">
                          <label class="bmd-label-floating">Current Location</label> 
                                </div>
                            <div>
                                <label>:</label>
                                <asp:Label ID="lblLoc" class="bmd-label-floating  serverlbl" runat="server"></asp:Label>
                       </div>
                        </div>
                 
                      
                      
                     
                        <div class="form-group row">
                            <div class="col-sm-2">
                           <label class="bmd-label-floating">Current Username</label>
                                </div>
                            <div>
                                <label>:</label>
                          <asp:label class="bmd-label-floating serverlbl" ID="lblName"  runat="server"> </asp:label>
                          </div>
                        </div>
    </div>
 

</asp:Content>
