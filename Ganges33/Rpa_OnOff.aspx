<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Analysis.Master" CodeBehind="Rpa_OnOff.aspx.vb" Inherits="Ganges33.Rpa_OnOff" %>

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
                  <h3 class="card-title  ">RPA On/Off</h3>
                  <p class="card-category"></p>
                </div>
              
                   <div class="card-body scrollbar " id="style-10">
                         <div class="form-group">
           <asp:Label ID="lblInfo" CssClass="bmd-label-floating serverlbl" runat="server"></asp:Label>
        </div>
                         <div>
           <div >
                <asp:Button ID="btnSuspendScheduler" runat="server" Text="Suspend Scheduler" class="btn btn-primary btn-lbl"  />
                                </div> <div>
                <asp:Button ID="btnSuspendTask" runat="server" Text="Suspend Tasks" class="btn btn-primary btn-lbl"  />
            </div>
            <div> <div class="GridviewDiv">
                <br />
                <asp:GridView runat="server" ID="gvDetails" AllowPaging="true"   AutoGenerateColumns="false" Width="420px" >
<HeaderStyle BackColor="#F0F0F0" />
<Columns>
 <asp:TemplateField HeaderText="Select">  
<ItemTemplate>  
 <asp:CheckBox ID="chk" runat="server" />  
 </ItemTemplate>  
</asp:TemplateField>  
<asp:BoundField DataField="ship_code" HeaderText="ship_code" Visible="false" />
 <asp:TemplateField HeaderText="ship_code"  Visible="false">
                        <ItemTemplate>
                            <asp:Label ID="lblId" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "Id") %>'></asp:Label>   
                            <asp:Label ID="lblStatus" runat="server" Visible="false" Text='<%#DataBinder.Eval(Container.DataItem, "Column3") %>'></asp:Label>   

                        </ItemTemplate>
                    </asp:TemplateField>

<asp:BoundField DataField="Column2" HeaderText="Scheduler/Task Name" />
<asp:BoundField DataField="Column3" HeaderText="Status"  />
</Columns>
</asp:GridView>
<br /><div>
                  <asp:Button ID="btnUpdate" runat="server" Text="" CssClass="btn btn-primary btn-lbl" Visible="false" />
    </div>
</div></div>
        </div>





                       </div>

                            <asp:Button ID="BtnCancel" runat="server"  Text="Button" style="display:none;" />
        <asp:Button ID="BtnOK" runat="server" Text="Button" style="display:none;" />
　　　　<asp:Button ID="Btn2OK" runat="server" Text="Button" style="display:none;" />   
                            
                             <asp:Label ID="lblMsg" runat="server" Text=""></asp:Label>
                        </div>
                      
            </div>
            </div>
          </div>
        </div>
      <%--</div>--%>

    </div>
  

     <div style="vertical-align:top ; visibility:hidden;">
                <asp:Button ID="btnAllTaskEnable" runat="server" Text="Update Enable Scheduler" class="btn btn-primary btn-lbl" />
               </div>
                          
                     <div style="visibility:hidden;">
               
                <asp:Button ID="btnAllTaskDisable" runat="server" Text="Update Disable Scheduler" class="btn btn-primary btn-lbl"  />
               
                   </div> <div style="visibility:hidden;">
                <asp:Button ID="btnLoadAutoChecker" runat="server" Text="Load Auto Task Checker (ATC)" class="btn btn-primary btn-lbl" />
                  
                       </div> <div style="visibility:hidden;">
                <asp:Button ID="btnEnableAutoChecker" runat="server" Text="Enable Auto Task Checker (ATC)" class="btn btn-primary btn-lbl" />
                   
                           </div> <div style="visibility:hidden;">
                <asp:Button ID="btnDisableAutoChecker" runat="server" Text="Disable Auto Task Checker (ATC)" class="btn btn-primary btn-lbl"   OnClientClick="return confirm('Are you sure you want to disable the auto checker scheduler?');" />
                   
                               </div> 

</asp:Content>

