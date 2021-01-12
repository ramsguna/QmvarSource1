<%@ Page Title="QMVAR - RPA-Logs" Language="vb" AutoEventWireup="false" MasterPageFile="~/Analysis.Master" CodeBehind="Rpa_Logs.aspx.vb" Inherits="Ganges33.Rpa_Logs" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>

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
                  <h3 class="card-title ">RPA Logs</h3>
                  <p class="card-category"></p>
                </div>
                <div  class="card-body   scrollbar "  id="style-10" >
                  
                    <div class="row">
                      <div class="col-md-12">
                        <div class="form-group">
                            
                           <div class="form-group droplistlocation">

                               <div class="col-sm-12">
                               <asp:Label id="lblInfo" runat="server"  CssClass=" serverlbl" Text=""></asp:Label>
                                <br />
                                   </div>
                              

                               </div>
                                   <div class="col-sm-12">
                                       <br />     <br />
                               <asp:Label id="Label1" runat="server"  CssClass="bmd-label-floating serverlbl" Text="From"></asp:Label>
         <asp:TextBox ID="txtDateFrom" runat="server" class="form-file-upload date serverlbl" style="width: 120px;" > </asp:TextBox> &nbsp;&nbsp;&nbsp;

             <asp:Label id="lblTo" runat="server" class="serverlbl  " Text="To"></asp:Label>&nbsp;&nbsp;&nbsp;
         <asp:TextBox ID="txtDateTo" runat="server"   class="form-file-upload date serverlbl" style="width: 146px;"></asp:TextBox>
   <%-- <ajaxToolkit:CalendarExtender ID="CalendarExtender1" Format="yyyy/MM/dd" runat="server" BehaviorID="txtDateFrom" TargetControlID="txtDateFrom" PopupPosition="Left"/>
   <ajaxToolkit:CalendarExtender ID="CalendarExtender2" Format="yyyy/MM/dd" runat="server" BehaviorID="txtDateTo" TargetControlID="txtDateTo" PopupPosition="Left"/>
       --%>   
           
                                       <asp:Button  ID="btnSearch" runat="server" class="btn pull-right btn-primary " Text="Search"/>
           </div>
                               <div class="">
                        <div class="form-group">

                               <asp:GridView runat="server" ID="gvDetails" AllowPaging="true" PageSize="10" AutoGenerateColumns="false"  OnPageIndexChanging="gvDetails_PageIndexChanging">
<HeaderStyle  BackColor="#F0F0F0" />
<Columns>
<asp:BoundField DataField="SCHEDULER_NAME" HeaderText="SchedulerName" />
<asp:BoundField DataField="TaskName" HeaderText="TaskName" />
<asp:TemplateField HeaderText="ProcessID">
<ItemStyle HorizontalAlign="Center" VerticalAlign="Middle"></ItemStyle>
<ItemTemplate>
<a onclick="javascript:window.open('Rpa_LogsDetails.aspx?id=<%#DataBinder.Eval(Container.DataItem, "ProcessId")%>');" href="#" id="a6" ><%#DataBinder.Eval(Container.DataItem, "ProcessId")%></a>
</ItemTemplate>
</asp:TemplateField>


<asp:BoundField DataField="ProcessType" HeaderText="Type"  />
<asp:BoundField DataField="StartDateTime" HeaderText="StartDateTime" />
<asp:BoundField DataField="EndDateTime" HeaderText="EndDateTime" />
<asp:BoundField DataField="Status" HeaderText="Status"  Visible="false"/>
<asp:TemplateField HeaderText="Status" >  
        <ItemTemplate>  
        <asp:Label ID="lblStatus" runat="server" Text='<%# Bind("Status") %>'></asp:Label>  
         </ItemTemplate>  
        </asp:TemplateField>  
<asp:BoundField DataField="SSC" HeaderText="SSC" />
<asp:BoundField DataField="TaskCount" HeaderText="Task" ItemStyle-HorizontalAlign="Center"/>
<asp:BoundField DataField="SuccessCount" HeaderText="Success" ItemStyle-HorizontalAlign="Center"/>
<asp:BoundField DataField="ReRunCount" HeaderText="ReRun" ItemStyle-HorizontalAlign="Center"/>
<asp:BoundField DataField="FailureCount" HeaderText="Failure" ItemStyle-HorizontalAlign="Center"/>
<asp:BoundField DataField="DataFoundCount" HeaderText="DataFound" ItemStyle-HorizontalAlign="Center"/>
<asp:BoundField DataField="NoDataCount" HeaderText="NoData" ItemStyle-HorizontalAlign="Center" />
<asp:BoundField DataField="UploadSuccessCount" HeaderText="Upload Success" ItemStyle-HorizontalAlign="Center"/>
<asp:BoundField DataField="UploadFailureCount" HeaderText="Upload Failure" ItemStyle-HorizontalAlign="Center" />
<asp:TemplateField HeaderText="logview">
<ItemStyle HorizontalAlign="Center" VerticalAlign="Middle"></ItemStyle>
<ItemTemplate>
<a onclick="javascript:window.open('Rpa_logview.aspx?id=<%#DataBinder.Eval(Container.DataItem, "logview")%>&src=<%#DataBinder.Eval(Container.DataItem, "SSC")%>');" href="#" id="a6" ><%#DataBinder.Eval(Container.DataItem, "logview")%></a>
</ItemTemplate>
</asp:TemplateField>


</Columns>
</asp:GridView>
</div>
                                    </div>

                        <%--<asp:Label ID="Label7" runat="server" CssClass="bmd-label-floating" Text=" Date：　From"></asp:Label>
                      <asp:TextBox ID="TextDateFrom" runat="server" class="form-file-upload" style="width: 120px;"></asp:TextBox>&nbsp;&nbsp;&nbsp;
                      <ajaxToolkit:CalendarExtender ID="TextDateFrom_CalendarExtender" runat="server" BehaviorID="TextDateFrom_CalendarExtender" TargetControlID="TextDateFrom" PopupPosition="Left">
                      </ajaxToolkit:CalendarExtender>


                             <asp:ImageButton ID="btnSend" runat="server" class="btn btn-primary pull-right" text="Import" />

                      <asp:Label ID="Label8" runat="server" class="fontFamily" Text="To"></asp:Label>&nbsp;&nbsp;&nbsp;
                      <asp:TextBox ID="TextDateTo" runat="server" class="form-file-upload" style="width: 146px;"></asp:TextBox>
                      <ajaxToolkit:CalendarExtender ID="TextDateTo_CalendarExtender" runat="server" BehaviorID="TextDateTo_CalendarExtender" TargetControlID="TextDateTo" PopupPosition="right">
                      </ajaxToolkit:CalendarExtender>--%>


                   <%-- <label id="bmd-label-floating" class="fontFamily" >Date: From</label>
                    <input type="text" value="" class="form-file-upload" style="width: 120px;">&nbsp;&nbsp;&nbsp;
                    <label id="bmd-label-floating" class="fontFamily">To</label>&nbsp;&nbsp;&nbsp;
                    <input type="text" value="" class="form-file-upload" style="width: 146px;">--%>
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

               


   <%-- <br />
    <br />
    <br />
    <br />
    <br />
    <br />
    <br />
    <table>
        <tr><td><asp:Label id="lblInfo" runat="server"></asp:Label></td></tr>
        <tr><td>From <asp:TextBox ID="txtDateFrom" runat="server" Width="80px" >  </asp:TextBox> To: <asp:TextBox ID="txtDateTo" runat="server"  Width="80px" ></asp:TextBox>
    <ajaxToolkit:CalendarExtender ID="CalendarExtender1" Format="yyyy/MM/dd" runat="server" BehaviorID="txtDateFrom" TargetControlID="txtDateFrom" PopupPosition="Left"/>
    <ajaxToolkit:CalendarExtender ID="CalendarExtender2" Format="yyyy/MM/dd" runat="server" BehaviorID="txtDateTo" TargetControlID="txtDateTo" PopupPosition="Left"/>
            <asp:Button  ID="btnSearch" runat="server" Text="Search"/>
            </td></tr>
        <tr><td>
     <div class="GridviewDiv">
<asp:GridView runat="server" ID="gvDetails" AllowPaging="true" PageSize="10" AutoGenerateColumns="false" Width="420px" OnPageIndexChanging="gvDetails_PageIndexChanging">
<HeaderStyle  BackColor="#F0F0F0" />
<Columns>
<asp:BoundField DataField="SCHEDULER_NAME" HeaderText="SchedulerName" />
<asp:BoundField DataField="TaskName" HeaderText="TaskName" />
<asp:TemplateField HeaderText="ProcessID">
<ItemStyle HorizontalAlign="Center" VerticalAlign="Middle"></ItemStyle>
<ItemTemplate>
<a onclick="javascript:window.open('Rpa_LogsDetails.aspx?id=<%#DataBinder.Eval(Container.DataItem, "ProcessId")%>');" href="#" id="a6" ><%#DataBinder.Eval(Container.DataItem, "ProcessId")%></a>
</ItemTemplate>
</asp:TemplateField>


<asp:BoundField DataField="ProcessType" HeaderText="Type"  />
<asp:BoundField DataField="StartDateTime" HeaderText="StartDateTime" />
<asp:BoundField DataField="EndDateTime" HeaderText="EndDateTime" />
<asp:BoundField DataField="Status" HeaderText="Status"  Visible="false"/>
<asp:TemplateField HeaderText="Status" >  
        <ItemTemplate>  
        <asp:Label ID="lblStatus" runat="server" Text='<%# Bind("Status") %>'></asp:Label>  
         </ItemTemplate>  
        </asp:TemplateField>  
<asp:BoundField DataField="SSC" HeaderText="SSC" />
<asp:BoundField DataField="TaskCount" HeaderText="Task" ItemStyle-HorizontalAlign="Center"/>
<asp:BoundField DataField="SuccessCount" HeaderText="Success" ItemStyle-HorizontalAlign="Center"/>
<asp:BoundField DataField="ReRunCount" HeaderText="ReRun" ItemStyle-HorizontalAlign="Center"/>
<asp:BoundField DataField="FailureCount" HeaderText="Failure" ItemStyle-HorizontalAlign="Center"/>
<asp:BoundField DataField="DataFoundCount" HeaderText="DataFound" ItemStyle-HorizontalAlign="Center"/>
<asp:BoundField DataField="NoDataCount" HeaderText="NoData" ItemStyle-HorizontalAlign="Center" />
<asp:BoundField DataField="UploadSuccessCount" HeaderText="Upload Success" ItemStyle-HorizontalAlign="Center"/>
<asp:BoundField DataField="UploadFailureCount" HeaderText="Upload Failure" ItemStyle-HorizontalAlign="Center" />
<asp:TemplateField HeaderText="logview">
<ItemStyle HorizontalAlign="Center" VerticalAlign="Middle"></ItemStyle>
<ItemTemplate>
<a onclick="javascript:window.open('Rpa_logview.aspx?id=<%#DataBinder.Eval(Container.DataItem, "logview")%>&src=<%#DataBinder.Eval(Container.DataItem, "SSC")%>');" href="#" id="a6" ><%#DataBinder.Eval(Container.DataItem, "logview")%></a>
</ItemTemplate>
</asp:TemplateField>


</Columns>
</asp:GridView></div>
        </td></tr></table>--%>
</asp:Content>
