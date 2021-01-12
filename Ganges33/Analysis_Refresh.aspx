<%@ Page Title="QMVAR-Analysis-Refresh" Language="vb" AutoEventWireup="false" MasterPageFile="~/Analysis.Master" CodeBehind="Analysis_Refresh.aspx.vb" Inherits="Ganges33.Analysis_Refresh" %>
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
   

   
   <div class="wrapper  col-sm-12 sidebar-wrapper position-fixed scrolbar contain"  id="style-10">
   
    <div class="content" >
        <div class="container-fluid">
          <div class="row">
            <div class="col-md-12">
              <div class="card">
                <div class="card-header card-header-primary">
                  <h3 class="card-title ">Recovery</h3>
                  <p class="card-category"></p>
                </div>
                <div class="card-body scrollbar" id="style-10">
                  
                    

                       <br />
                  
                  
                      
                  
                    <div class="row">
                        <div class="col-sm-6">
                      <div class="col-md-12">
                          <div class="row">
                         <div class="form-group col-sm-4">
                             
                             <Label runat="server" CssClass="bmd-label-floating">Target Store </Label>
                             </div>
                          <div Class="form-group">
                      <asp:DropDownList ID="DropListLocation" runat="server" CssClass="form-control " style="width: 270px; height:33px;">
                      </asp:DropDownList>
                              </div>
                          </div>

                          <div class="row">
                           <div class="form-group col-sm-4 ">
                          <Label  runat="server"  CssClass="bmd-label-floating">Month</Label>
                               </div>
                              <div>
                     <asp:DropDownList ID="DropDownMonth" runat="server" CssClass="form-control " style="width: 270px; height:33px;">
                      </asp:DropDownList>
                       <asp:ScriptManager ID="ScriptManager1" runat="server">
                      </asp:ScriptManager>

                    
                  </div>
                   </div>
                        
                     </div>

                        </div>


                          <div class="col-md-6">
                              <div class="row">
                        <div class="form-group col-sm-4">
                             <Label  runat="server" CssClass="bmd-label-floating" >Target Data</Label>
                            </div>
                    <div>
                             <asp:DropDownList ID="DropDownTargetData" runat="server" class="form-control " style="width: 270px; height:33px">
                      </asp:DropDownList>
                    
                  </div>
             </div>

                              <div class="row">
                  <div class="form-group col-sm-4">

                        <Label ID="Label7" runat="server" CssClass="bmd-label-floating">Date:&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp From</Label>
                      </div>
                                  <div>
                      <asp:TextBox ID="TextDateFrom" runat="server" class="form-file-upload serverlbl  date" AutoCompleteType="Disabled" style="width: 120px; height:33px;"></asp:TextBox>
                      <%--<ajaxToolkit:CalendarExtender ID="TextDateFrom_CalendarExtender" runat="server" BehaviorID="TextDateFrom_CalendarExtender" TargetControlID="TextDateFrom" PopupPosition="Left">
                      </ajaxToolkit:CalendarExtender>--%>
                                    &nbsp;&nbsp;
                      <asp:Label ID="Label8" runat="server" class="bmd-label-floating serverlbl" Text="To"></asp:Label>&nbsp;&nbsp;
                      <asp:TextBox ID="TextDateTo" runat="server" class="form-file-upload serverlbl date" AutoCompleteType="Disabled" style="width: 120px; height:33px"></asp:TextBox>
                     <%-- <ajaxToolkit:CalendarExtender ID="TextDateTo_CalendarExtender" runat="server" BehaviorID="TextDateTo_CalendarExtender" TargetControlID="TextDateTo" PopupPosition="right">
                      </ajaxToolkit:CalendarExtender>--%>

</div>
                  </div>
                  </div>
                      </div>
                 
                    <div>
                       <asp:Button ID="btnSend" runat="server" class="btn btn-primary pull-right" text="Delete"/>
                      </div> 
                    <div class="clearfix"></div>
                    <div class="row">
                      <div class="col-md-12">
                        <div class="form-group col-sm-12">
                            <Label ID="Label9" runat="server" class="bmd-label-floating " Font-Bold="true">Deleted data history</Label>
                             <asp:ListBox ID="ListHistory" runat="server" CssClass="listbox scrollbar form-control bmd-label-floating serverlbl"  style="Height: 130px;Width: 100%;"></asp:ListBox>
                   

                            <asp:Button ID="BtnCancel" runat="server" Text="Button" style="display:none;" />
        <asp:Button ID="BtnOK" runat="server" Text="Button" style="display:none;" />
　　　　<asp:Button ID="Btn2OK" runat="server" Text="Button" style="display:none;" />   
                            
                             <div id="dialog" title="message" style="display:none;"> >
        <asp:Label ID="lblMsg" runat="server" Text=""></asp:Label>
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
            </div>
          
      <%--</div>--%>

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
  </body>
    </html>
 
  <!--   Core JS Files   -->
  
   <%-- </body>--%>


<%--<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
     <div class="div-background-image" style="background-image: url('pagewall_2/wall_analysis-2.png')">
      <div class="div-EntirePage">
        <br />
          <table class="table-EntirePage">
              <tr>
                  <td class="td-Blank1"></td>
                  <td class="td-btn-Analysis">
                      <asp:ImageButton ID="btnAnalysis" runat="server" CssClass="btn-Analysis" ImageUrl="~/icon/analysis.png" />
                  </td>
                  <td class="td-Analysis-Refresh-data">
                      <asp:Label ID="Label3" runat="server" CssClass="lbl-Analysis-Refresh-data" Text="Analysis Refresh Data"></asp:Label>
                  </td>
                  <td class="td-Blank2"></td>
                  <td class="td-Blank3"></td>
                  <td class="td-Blank4"></td>
              </tr>
              <tr>
                  <td class="td-Blank5"></td>
                  <td  colspan = "2" class="td-Blank6">
                      <asp:Label ID="Label1" runat="server" Text="Current location : " CssClass="fontFamily"></asp:Label>
                      <asp:Label ID="lblLoc" runat="server" CssClass="fontFamily"></asp:Label>
                  </td>
                  <td class="td-Blank7"></td>
                  <td class="td-Blank8"></td>
                  <td class="td-Blank6"></td>
              </tr>
              <tr>
                  <td class="td-Blank5"></td>
                  <td colspan = "2" class="td-Blank6">
                      <asp:Label ID="Label2" runat="server" Text="Current username : " CssClass="fontFamily"></asp:Label>
                      <asp:Label ID="lblName" runat="server" CssClass="fontFamily"></asp:Label>
                  </td>
                  <td class="td-Blank7"></td>
                  <td class="td-Blank8"></td>
                  <td class="td-Blank6"></td>
              </tr>
              <tr>
                  <td class="td-Blank9"></td>
                  <td colspan = "2" class="td-Blank10">
                      <asp:Label ID="Label4" runat="server" CssClass="fontFamily" Text="Target Store"></asp:Label>
                      <span class="fontFamily">&nbsp;&nbsp; </span>
                      <asp:DropDownList ID="DropListLocation" runat="server" CssClass="dropdownlist-location-TargetData ">
                      </asp:DropDownList>
                  </td>
                  <td class="td-Blank11"></td>
                  <td class="td-Blank12">
                      <asp:Label ID="Label5" runat="server" CssClass="fontFamily" Text="Target Data"></asp:Label>
                      <span class="fontFamily">&nbsp;&nbsp; </span>
                      <asp:DropDownList ID="DropDownTargetData" runat="server" CssClass="dropdownlist-location-TargetData ">
                      </asp:DropDownList>
                  </td>
                  <td class="td-Blank10"></td>
              </tr>
              <tr>
                  <td class="td-Blank13"></td>
                  <td colspan = "4" class="td-Blank14">
                      <asp:Label ID="Label6" runat="server" Text="Month：" CssClass="fontFamily"></asp:Label>
                      &nbsp;&nbsp;
                      <asp:DropDownList ID="DropDownMonth" runat="server" CssClass="dropdown-month">
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
                  <td class="td-Blank14"></td>
              </tr>
              <tr>
                  <td class="td-Blank15-Blank16-btnSend-Blank17"></td>
                  <td colspan = "2">
                      <asp:Label ID="Label9" runat="server" CssClass="fontFamily" Text="Deleted data history"></asp:Label>
                  </td>
                  <td class="td-Blank15-Blank16-btnSend-Blank17"></td>
                  <td class="td-Blank15-Blank16-btnSend-Blank17" align = "right">
                    <asp:ImageButton ID="btnSend" runat="server" ImageUrl="~/icon/send.png" CssClass="btn-Send" />
                  </td>
                  <td class="td-Blank15-Blank16-btnSend-Blank17"></td>
              </tr>
              <tr>
                  <td class="td-Blank18-ListHistory-Blank19"></td>
                  <td  colspan = "4" class="td-Blank18-ListHistory-Blank19">
                      <asp:ListBox ID="ListHistory" runat="server" CssClass="list-History"></asp:ListBox>
                  </td>
                  <td class="td-Blank18-ListHistory-Blank19"></td>
              </tr>
          </table>        
        <asp:Button ID="BtnCancel" runat="server" Text="Button" style="display:none;" />
        <asp:Button ID="BtnOK" runat="server" Text="Button" style="display:none;" />
　　　　<asp:Button ID="Btn2OK" runat="server" Text="Button" style="display:none;" />      
      </div>
   </div>
    <div id="dialog" title="message" style="display:none;"> >
        <asp:Label ID="lblMsg" runat="server" Text=""></asp:Label>
    </div>
</asp:Content>--%>
</asp:Content>
