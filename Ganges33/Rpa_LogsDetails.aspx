<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="Rpa_LogsDetails.aspx.vb" Inherits="Ganges33.Rpa_LogsDetails" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
      <link href="assets/css/material-dashboard-rtl.css" rel="stylesheet" />
    <link href="assets/css/material-dashboard.css" rel="stylesheet" />
    <link href="assets/css/material-dashboard.min.css" rel="stylesheet" />
    <meta content='width=device-width, initial-scale=1.0, shrink-to-fit=no' name='viewport' />
  <!--     Fonts and icons     -->
  <link rel="stylesheet" type="text/css" href="https://fonts.googleapis.com/css?family=Roboto:300,400,500,700|Roboto+Slab:400,700|Material+Icons" />
  <link rel="stylesheet" href="https://maxcdn.bootstrapcdn.com/font-awesome/latest/css/font-awesome.min.css" />
  <!-- CSS Files -->
    <link href="assets/jquery-ui.css" rel="stylesheet" />
    <script src="assets/jquery-ui.min_lips.js"></script>
    <link href="assets/jquery-ui_theme.css" rel="stylesheet" />
  <!-- CSS Just for demo purpose, don't include it in your project -->
  <link href="../assets/demo/demo.css" rel="stylesheet" />
    
    <script src="http://ajax.googleapis.com/ajax/libs/jquery/1.7.0/jquery.min.js"></script>
    <link type="text/css" href="http://ajax.googleapis.com/ajax/libs/jqueryui/1/themes/start/jquery-ui.css" rel="stylesheet" />
     <script type="text/javascript" src="http://ajax.googleapis.com/ajax/libs/jqueryui/1/jquery-ui.min.js"></script>


    <link href="assets/jquery-ui_theme.css" rel="stylesheet" />
    <link href="assets/jquery-ui.css" rel="stylesheet" />
    <script type="text/javascript"  src="assets/jquery-ui.min_lips.js"></script>

        <style type="text/css">
       
        </style>
</head>
<body>
    <form id="form1" runat="server">
       <div class="wrapper h-100 col-sm-12 position-fixed scrollbar " id="style-10">
   
    <div class="content" >
        <div class="container-fluid">
          <div class="row">
            <div class="col-md-12">
              <div class="card">
                <div class="card-header card-header-primary">
                  <h3 class="card-title ">RPA Log Details</h3>
                  <p class="card-category"></p>
                </div>
                  <div class="card-body scrollbar" id="style-10" >
                       <br />
                      <div>
                         <asp:Button ID="btnClose" runat="server" CssClass="btn btn-primary" Text="Close" />
                      </div>
                       <br />
                       <div class="GridviewDiv">
<asp:GridView runat="server" ID="gvDetails" AllowPaging="true" PageSize="10" AutoGenerateColumns="false" Width="420px" OnPageIndexChanging="gvDetails_PageIndexChanging">
<HeaderStyle BackColor="#F0F0F0" />
<Columns>
 <asp:TemplateField HeaderText="ReRun">  
                   
                    <ItemTemplate>  
                        <asp:CheckBox ID="chkReRun" runat="server" />  
                    </ItemTemplate>  
                </asp:TemplateField>  
<asp:BoundField DataField="Ssc" HeaderText="Ssc" />

<asp:BoundField DataField="TaskName" HeaderText="TaskName" />
<asp:BoundField DataField="Status" HeaderText="Status" visible="false"/>
 <asp:TemplateField HeaderText="Status" >  
        <ItemTemplate>  
        <asp:Label ID="lblStatus" runat="server" Text='<%# Bind("Status") %>'></asp:Label>  
         </ItemTemplate>  
        </asp:TemplateField>  
<asp:BoundField DataField="StartDateTime" HeaderText="StartDateTime" />
<asp:BoundField DataField="EndDateTime" HeaderText="EndDateTime" />
<asp:BoundField DataField="ProcessId" HeaderText="ProcessId" />
<asp:BoundField DataField="ReRunCount" HeaderText="ReRun Count" ItemStyle-HorizontalAlign="Center" />
<asp:BoundField DataField="SrcStatusFlg" HeaderText="Downloaded Status" Visible="false" />
       <asp:TemplateField HeaderText="Downloaded Status" >  
        <ItemTemplate>  
        <asp:Label ID="lblSrcStatusFlg" runat="server" Text='<%# Bind("SrcStatusText") %>'></asp:Label>  
         </ItemTemplate>  
        </asp:TemplateField>  
<asp:BoundField DataField="SrcRecordCount" HeaderText="Record Count" Visible="false"  ItemStyle-HorizontalAlign="Center" />
       <asp:TemplateField HeaderText="Record Count" ItemStyle-HorizontalAlign="Center" >  
        <ItemTemplate>  
        <asp:Label ID="lblSrcRecordCount" runat="server" Text='<%# Bind("SrcRecordCountTxt") %>'></asp:Label>  
         </ItemTemplate>  
        </asp:TemplateField> 
<asp:BoundField DataField="TargetStatusFlg" HeaderText="Upload Status" Visible="false" />
        <asp:TemplateField HeaderText="Upload Status" >  
        <ItemTemplate>  
        <asp:Label ID="lblTargetStatusFlg" runat="server" Text='<%# Bind("TargetStatusFlgTxt") %>'></asp:Label>  
         </ItemTemplate>  
        </asp:TemplateField> 
<asp:BoundField DataField="TargetStatusText" HeaderText="QMVAR Result" />
<asp:BoundField DataField="SrcFileName" HeaderText="Downloaded File" />
<asp:BoundField DataField="CvtFileName" HeaderText="Uploaded File" />

<asp:BoundField DataField="ErrorLogs" HeaderText="ErrorLogs" />


                    <asp:TemplateField HeaderText="ssc" Visible="false">  
                   
                    <ItemTemplate>  
                        <asp:Label ID="lblSsc" runat="server" Text='<%# Bind("ssc") %>'></asp:Label>  
                    </ItemTemplate>  
                </asp:TemplateField>  

                        <asp:TemplateField HeaderText="TaskName" Visible="false">  
                 
                    <ItemTemplate>  
                        <asp:Label ID="lblTaskName" runat="server" Text='<%# Bind("TaskName") %>'></asp:Label>  
                        <asp:Label ID="lblSchedulerName" runat="server" Text='<%# Bind("SCHEDULER_NAME") %>'></asp:Label>  
                    </ItemTemplate>  
                </asp:TemplateField>  

</Columns>
</asp:GridView></div>
<div>
    <br />
                   <asp:Button ID="btnReRun" CssClass="btn btn-primary"     width="16%"  runat="server" OnClick="btnReRun_Click"
                Text="Selected SSC ReRun" /> 
                      </div>
                  <div>
                       <br />
                       <asp:Label ID="lblInfo" runat="server"></asp:Label>
                  </div>
</div>
                  
                  </div>
                </div>
              </div>
            </div>
        </div>
        </div>
    </form>
</body>
</html>
