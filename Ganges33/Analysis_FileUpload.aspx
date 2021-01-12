<%@ Page Title="QMVAR-AnalysisFileUpload" Language="vb" AutoEventWireup="false" MasterPageFile="~/Analysis.Master" CodeBehind="Analysis_FileUpload.aspx.vb" Inherits="Ganges33.Analysis_FileUpload" %>
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
    

   
   <div class="wrapper  col-sm-12 sidebar-wrapper position-fixed scrolbar contain" id="style-10">
   
    <div class="content" >
        <div class="container-fluid">
          <div class="row">
            <div class="col-md-12">
              <div class="card">
                <div class="card-header card-header-primary">
                  <h3 class="card-title ">Analysis File Upload</h3>
                  <p class="card-category"></p>
                </div>
                <div class="card-body scrollbar" id="style-10">
<br /><br />
  <div class="row col-sm-12">

           <div class="row col-sm-6"> 
              
                        <div class="col-sm-4">
                            
                             <label id="" class="fontFamily bmd-label-floating">Upload Branch</label>
                       </div>
                         <div class="col-sm-8">
                             
                            <asp:DropDownList class="form-control  " ID="DropListLocation" runat="server" style="width: 100%;height:33px; ">

                      </asp:DropDownList>
                        </div>
               </div>
               
                        
          <div class="col-sm-6 row">
              <div class="col-sm-12">
              <div  class="col-sm-7">
                   <div  class="col-sm-12">
                           <label id="" class="fontFamily bmd-label-floating">Parts Invoice No</label>
                                </div>
                                </div>
                            <div class="col-sm-5">
                                 <asp:TextBox ID="TextPartsInvoiceNo" runat="server" value="" Height="33px" Width="100%" class="form-file-upload  serverlbl" />
                    </div>
      </div>
      </div>
</div>

          
                  <div class="col-sm-12 row">



                      <div class="col-sm-6 row">
                           <div  class="col-sm-4">
                                <br />
                        <label id="" class="fontFamily bmd-label-floating">Task Name</label>
                               </div>
                        <div class="col-sm-8">
                             <br />
                         <asp:DropDownList class="form-control " ID="drpTask" runat="server" AutoPostBack="true"  OnSelectedIndexChanged="OnSelectedIndexChanged"  style="width: 100%;height:33px;" >
                            <asp:ListItem Text="Select..." Value="0"></asp:ListItem>
                             <asp:ListItem Text="1.DailyStatementReport" Value="1"></asp:ListItem>
                              <asp:ListItem Text="2.WarrantyExcel File" Value="2"></asp:ListItem>
                              <asp:ListItem Text="3A.Sales Invoice to Samsung_C IW" Value="3"></asp:ListItem>
                              <asp:ListItem Text="3B.Sales Invoice to Samsung_EXC IW" Value="-3"></asp:ListItem>
                             <asp:ListItem Text="4.Invoice Update OOW" Value="4"></asp:ListItem>
                             <asp:ListItem Text="5.Debit Note" Value="5"></asp:ListItem>
                              <asp:ListItem Text="6.PurchaseRegister summary" Value="6"></asp:ListItem>
                             <asp:ListItem Text="7.PurchaseRegister Detail" Value="7"></asp:ListItem>
                              <asp:ListItem Text="8.Good Receiving" Value="8"></asp:ListItem>
                              <asp:ListItem Text="9.StockoverView" Value="9"></asp:ListItem>
                              <asp:ListItem Text="9A.StockoverView Count" Value="9A"></asp:ListItem>
                              <asp:ListItem Text="10.SAW Discount" Value="10"></asp:ListItem>
                              <asp:ListItem Text="11.Parts In and Out History" Value="11"></asp:ListItem>
                              <asp:ListItem Text="12.Fixed Asset" Value="12"></asp:ListItem>
                              <asp:ListItem Text="13.Consumable Purchase List" Value="13"></asp:ListItem>
                              <asp:ListItem Text="14.GSS Paid to Samsung" Value="14"></asp:ListItem>
                             <asp:ListItem Text="15.Return Credit" Value="15"></asp:ListItem>
                              <asp:ListItem Text="16.Samsung Ledger" Value="16"></asp:ListItem>
                              <asp:ListItem Text="17.Service Parts Return" Value="17"></asp:ListItem><%--VJ 2019/10/10--%>
                              <asp:ListItem Text="18.Debit Note Register" Value="18"></asp:ListItem><%--VJ 2019/10/10--%>
                              <%--<asp:ListItem Text="19A.HSN Code with Purchase Detail" Value="19A"></asp:ListItem><%--VJ 2019/10/15--%>
                              <asp:ListItem Text="19B.HSN Code" Value="19B"></asp:ListItem><%--VJ 2019/10/15--%>
                              <asp:ListItem Text="20.OtherSalesExtendedWarranty" Value="20"></asp:ListItem>
                              <asp:ListItem Text="21.PO Status" Value="21"></asp:ListItem>
                              <asp:ListItem Text="22.Activity Report" Value="22"></asp:ListItem>
                              <asp:ListItem Text="23.PODC" Value="23"></asp:ListItem>
                               <asp:ListItem Text="24.Samsung to GSS paid (BOI)" Value="24"></asp:ListItem>
                          
                          </asp:DropDownList>
                            </div>
                      </div>
                      
                         <div class="col-sm-6 row">
                             <div class="col-sm-12">
                            <div  class="col-sm-7">
                                 <br />
                                 <div  class="col-sm-12">
                          <label id="" class="fontFamily bmd-label-floating">labor Invoice No</label>
                                </div>
                                </div>
                            <div class="col-sm-5">
                                 <br />
                                <asp:TextBox ID="TextLaborInvoiceNo" runat="server" Height="33px" Width="100%" value="" class="form-file-upload serverlbl" />  
              </div>
                          </div>
                          </div>
                   </div>
                    <div class="col-sm-12 row">
                        <div class="col-sm-6 row">
    <div  class="col-sm-4">
         <br />
                         <label  class="fontFamily bmd-label-floating">Invoice Date</label>
                  </div>
                         <div class="col-sm-8">
                              <br />
                      <span class=" fontFamily"><asp:RadioButton type="radio"  value="RadioBtnMM" disabled="disabled"  ID="RadioBtnMM" runat="server" />&nbsp;&nbsp;<label>MM/DD/YYYY</label></span>
                      <span class="fontFamily">&nbsp;&nbsp;</span>&nbsp;&nbsp; &nbsp;<span class=" fontFamily"><asp:RadioButton ID="RadioBtnDD" runat="server" AutoPostBack="True" type="radio" value="RadioBtnDD" disabled="disabled" />&nbsp;&nbsp;<label>DD/MM/YYYY</label></span>
                      
                 </div>
                    </div>
                        <div class="col-sm-6 row">
                            <div class="col-sm-12">
                          <div  class="col-sm-7">
                                <br />
                               <div  class="col-sm-12">
                    <label id="" class="fontFamily bmd-label-floating" >Invoice Date(DD/MM/YYYY)</label>
                    </div>
                    </div>
                            
                      <div class="col-sm-5">
                            <br />
                          <asp:TextBox value="" ID="TextInvoiceDate" runat="server" Height="33px" Width="100%" AutoCompleteType="Disabled" class="form-file-upload serverlbl date  " />
                   <%--   <ajaxToolkit:CalendarExtender ID="TextInvoiceDate_CalendarExtender" runat="server"  CssClass="date" BehaviorID="TextInvoiceDate_CalendarExtender" TargetControlID="TextInvoiceDate" PopupPosition="right">
                      </ajaxToolkit:CalendarExtender>--%>
                          </div>
                          </div>
                        </div>
                    </div>
                    
                
                    <div class="col-sm-12 row">
                        <div class="col-sm-6 row">
                           <div class="col-sm-12">
                                 <br />
                               <asp:FileUpload ID="FileUploadAnalysis" runat="server" Class="serverlbl" />
                      
                               </div>

                        </div>
                                
                        <div class="col-sm-6 row">
                            <div class="col-sm-12">
                              <div  class="col-sm-7">
                                   <br />
                                   <div  class="col-sm-12">
                    <label id="" class="fontFamily bmd-label-floating">Date</label>
                    </div>
                    </div>

                      <div class="col-sm-5">
                           <br />
                    <asp:TextBox value="" ID="txtDate" runat="server" Height="33px" Width="100%" AutoCompleteType="Disabled"  class="form-file-upload serverlbl date  calender"  />&nbsp;&nbsp;&nbsp;
               <%--       <ajaxToolkit:CalendarExtender ID="txtDate_CalendarExtender" runat="server" Format="MM/dd/yyyy" BehaviorID="txtDate_CalendarExtender" TargetControlID="txtDate" PopupPosition="Left">
                      </ajaxToolkit:CalendarExtender> --%> 
                          </div>
                        </div>
                    </div>
                        </div>
                    
                  
                          <div class="col-sm-12 row">

                              <div class="col-sm-6 row">


                              </div>

                              <div class="col-sm-6 row">
                                  <div class="col-sm-12">
                                   <div  class="col-sm-7">
                                        <br />
                                        <div  class="col-sm-12">
                  <label id="" class="  bmd-label-floating">Amount</label>
               </div>
               </div>
                    <div class="col-sm-5">
                         <br />
                        <asp:TextBox ID="txtAmount" runat="server" value="" Height="33px" Width="100%" class="form-file-upload serverlbl" />
                   </div>
                              </div>
                              </div>


                          </div>
                   
                              <div class="col-sm-12 row">
                                  <div class="col-sm-6 row">
                                      </div>
                                  <div class="col-sm-6 row">
                                      <div class="col-sm-12">
                                        <div  class="col-sm-7">
                                             <br />
                                            <div class="col-sm-12">
                    <label class="bmd-label-floating">Advice Reference No</label>
                  </div></div>
                      <div class="col-sm-5">
                           <br />
                          <asp:TextBox ID="txtArNo" runat="server" value="" Height="33px" Width="100%" class="form-control serverlbl" />
               </div>
                        
                                  </div>
                                  </div>

                                  </div>
                     
                  <div class="col-sm-12">
                      <br />
                        <asp:Button ID="btnUpload" runat="server" Text="Import" class="btn btn-primary pull-right" />
                  </div>
                                         <div class="row col-sm-12">
                      <div class="col-sm-6">
                        
                            <div >
                            <label class="bmd-label-floating">Message</label>
                          
                                </div>
                          <div>
                      <select size="4" ID="ListMsg" runat="server" class="listbox form-control" style="Height: 70%;Width: 100%;overflow: hidden;">
                      </select>      
                        </div>
                           
                      </div>
                      <div class="col-sm-6">
                    <div>
                            <label class="bmd-label-floating"> History</label>
                           </div>
                          <div>
                      <select size="4" ID="ListHistory" runat="server" class="listbox form-control" style="Height: 70%;Width: 100%;overflow: hidden;">
                       </select>              
                        </div>
                            </div>
                      </div>
                 
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
                      <div style="visibility:hidden">
                            <label class="bmd-label-floating">Current Location</label>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                            <label class="bmd-label-floating">:</label>
                            <asp:Label ID="lblLoc" class="bmd-label-floating serverlbl" runat="server"></asp:Label>
                      
    
                       <label class="bmd-label-floating">Current Username</label> &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; 
                         <label class="bmd-label-floating">:</label>
                            <asp:label class="bmd-label-floating serverlbl" ID="lblName"  runat="server"> </asp:label>
                      
                          </div>
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
  
   
     

</asp:Content>
