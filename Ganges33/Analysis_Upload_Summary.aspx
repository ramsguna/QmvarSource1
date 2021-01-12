<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Analysis.Master" CodeBehind="Analysis_Upload_Summary.aspx.vb" Inherits="Ganges33.Analysis_Upload_Summary" %>
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
    <script>
        $(function () {
            $('.multiselect-ui').multiselect({
                includeSelectAllOption: true
            });
        });
    </script>
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
                  <h3 class="card-title ">Upload Summary</h3>
                  <p class="card-category"></p>
                </div>
                <div class="card-body scrollbar" id="style-10">
             

                    <br />
                    
                   
                    <div class="row col-sm-12">
                      <div class="col-sm-4 row">
                        
                         <div class="form-group col-sm-6">
                      <label id="" class="fontFamily bmd-label-floating">Target Store</label>
                             </div>
                          <div class=" col-sm-6">
<asp:ListBox ID="lstLocation" runat="server"  Height="133px" Width="100px"  class="multiselect-ui form-control" multiple="multiple"  SelectionMode="Multiple">

                      </asp:ListBox>
                    
                  </div>
                    
                      </div>
					
                      <div class=" col-sm-4 row">
                        <div class="form-group col-sm-7">
                      <label id="" class="fontFamily bmd-label-floating">Create Start Date</label>
                     </div>
                          <div class=" col-sm-5">
                       <asp:TextBox ID="TextDateFrom" runat="server" class="form-file-upload date serverlbl" style="width: 100%;"></asp:TextBox>
                     <%-- <ajaxToolkit:CalendarExtender ID="TextDateFrom_CalendarExtender" runat="server" BehaviorID="TextDateFrom_CalendarExtender" TargetControlID="TextDateFrom" PopupPosition="Left">
                      </ajaxToolkit:CalendarExtender>--%>
                     
                  </div>
                
                 
                      </div>


                      <div class="row col-sm-4" >
                        <div class="form-group col-sm-7">
                      <label id="" class="bmd-label-floating fontFamily">Create End Date</label>
          </div>
                          <div class=" col-sm-5">
               <asp:TextBox ID="TextDateTo" runat="server"  class="form-file-upload serverlbl date" style="width: 100%"></asp:TextBox>
                     <%-- <ajaxToolkit:CalendarExtender ID="TextDateTo_CalendarExtender" runat="server" BehaviorID="TextDateTo_CalendarExtender" TargetControlID="TextDateTo" PopupPosition="right">
                      </ajaxToolkit:CalendarExtender>     --%>  
                      
                      
                  </div>
 
                      </div>


                      <div class="row col-sm-4">
                        <div class="form-group col-sm-4">
                            <br />
                            <label id="" class="fontFamily bmd-label-floating">Active</label>
                           </div>
                          <div class="col-sm-4">
                              <div class="col-sm-12">
                               <br />
                        <asp:DropDownList ID="drpStatus" runat="server" Height="32px" class="form-control " style="width: 120px;"  >
                              <asp:ListItem Text="All" Value="0,1" Enabled="false" ></asp:ListItem>
                             <asp:ListItem Text="Active" Value="0" selected="True"></asp:ListItem>
                              <asp:ListItem Text="In Active" Value="1"></asp:ListItem>
                       </asp:DropDownList>
                              </div>
                              </div>
                          </div>

                    </div>

                   <asp:CheckBox  ID="chkIndividual" runat="server" Text="Current User" Visible="false"/>
                       
                     <asp:Button ID="btnExport" runat="server" text="Export"  class="btn btn-primary pull-right" Visible="false"/>&nbsp
                     <asp:Button ID="btnSearch" runat="server" text="Search" class="btn btn-primary pull-right" />
                    
                    
               

        
                
                  <div class="col-sm-12">
                      <div >
         <asp:Label ID="lblReccount" runat="server" Text="" Class="serverlbl" Visible="false"></asp:Label>
      </div>
                      <br />
                      <br />
                      <div class="col-sm-12">

                      </div>
 </div>
        <div class="col-sm-12"  >

       
                    
               <asp:GridView ID="gvExportReport" runat="server" AutoGenerateColumns="false"
             RowStyle-Wrap="false"  HeaderStyle-BackColor="#8e24aa" HeaderStyle-ForeColor="White"   ShowHeaderWhenEmpty="false" HeaderStyle-Wrap="false"  >
            <Columns>
               <asp:BoundField  DataField="Target_Store" HeaderText="Target Store"  ControlStyle-Width="85px" Visible="false" />
                <asp:BoundField DataField="Seq" HeaderText="Seq No"  ControlStyle-Width="52px"/>
                <asp:BoundField DataField="Report_Name" HeaderText="Report Name"  ControlStyle-Width="300px" />
                
                <asp:HyperLinkField HeaderText="No of Records" runat="server" Target="_blank" DataNavigateUrlFields="Target_Store,Seq"
                DataNavigateUrlFormatString="~/Analysis_Upload_Details.aspx?Store_ID={0}&Seq={1}" DataTextField="Cnt"/>
          
                <asp:BoundField DataField="Create_Date_From" HeaderText="Start Date Time"  ControlStyle-Width="100px"/>
                <asp:BoundField DataField="Create_Date_To" HeaderText="End Date Time"  ControlStyle-Width="100px"/>
            </Columns>
                 
            
                 <AlternatingRowStyle BackColor="white" ForeColor="Black"  />
            <HeaderStyle BackColor="#8e24aa" Font-Bold="True" ForeColor="white" />
            <RowStyle ForeColor="Black" BackColor="White" />
        </asp:GridView>
                      
          <br />
            <br />
        </div>
             
        <div class="Cell" style="width:300px">
            
        </div>
          </div>
        </div>
           
   <br />
                       <br />
	    <asp:HiddenField  runat="server" ID ="hdnStoreValue" />
        <asp:HiddenField  runat="server" ID ="hdnStoreName" />
        <asp:HiddenField  runat="server" ID ="hdnColumnName" />
        <asp:HiddenField  runat="server" ID ="hdnDateFrom" />
        <asp:HiddenField  runat="server" ID ="hdnDateTo" />
        <asp:HiddenField  runat="server" ID ="hdnActiveFlag" />
        <asp:HiddenField  runat="server" ID ="hdnCurrentUser" />
        <asp:HiddenField  runat="server" ID ="hdnUserLevel" />
        <asp:HiddenField  runat="server" ID ="hdnAdminFlag" />
        <asp:HiddenField  runat="server" ID ="hdnUserId" />
		 
      
  
              </div>
          
              </div>
              </div>
              </div>
              </div>
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
        
    <div id="dialog" title="message" style="display:none;"> >
        <asp:Label ID="lblMsg" runat="server" Text=""></asp:Label>
    </div>
   
  <!--   Core JS Files   -->
 


   
</asp:Content>
