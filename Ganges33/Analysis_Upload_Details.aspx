<%@ Page Language="vb" AutoEventWireup="false" MasterPageFile="~/Analysis.Master"  CodeBehind="Analysis_Upload_Details.aspx.vb" Inherits="Ganges33.Analysis_Upload_Details" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="ajaxToolkit"  %>
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
      
      <!-- End Navbar -->
      <div class="content">
        <div class="container-fluid">
          <div class="row">
            <div class="col-md-12">
              <div class="card">
                <div class="card-header card-header-primary">
                  <h3 class="card-title ">Upload Details</h3>
                  <p class="card-category"></p>
                </div>
                <div class="card-body scrollbar" id="style-10">
                    <br />
                    <div >
                 <table>
                          <tr>
                           
                              <td style="width:170px" >
                                  <asp:Label ID="lblLocationValue" runat="server"   ></asp:Label>
                              </td>
                              <td style="width:310px">
                                  <asp:Label ID="lblDateFrom" runat="server"  ></asp:Label>
                              </td>
                              <td style="width:290px">
                                  <asp:Label ID="lblDateTo" runat="server"  ></asp:Label>
                              </td>
                              <td style="width:160px">
                                  <asp:Label ID="lblActive" runat="server"  ></asp:Label>
                                  <asp:CheckBox  ID="chkIndividual" runat="server" Text="Current User" Enabled="false" Visible="false"/>
                              </td>
                          </tr>
                      </table>
                        </div>
                      <br />
                    <div>
                        <asp:Button ID="btnExport" Text="Export" runat="server" class="btn btn-primary pull-right"  Visible="false"/>
                   <asp:Button ID="btnClose"  Text="Close" runat="server" class="btn btn-primary pull-right"  OnClientClick="window.close();"  />
                 
                    </div>

                    <div>
                        <br />
                        <br />
                          <asp:Label ID ="lbltotal" CssClass="serverlbl" runat="server"> </asp:Label>

             
          <br />
          <b><asp:Label ID ="lblPagesize" Class="serverlbl font-weight-bold"  runat="server" Visible="false">Page Size:</asp:Label></b>;&nbsp<asp:TextBox ID="txtPageSize" runat="server" MaxLength="4" class="serverlbl" Style="Width: 40px"  AutoPostBack="true" OnTextChanged="txtPageSize_TextChanged"></asp:TextBox>
          <asp:Label ID ="lblErrorMessage" Class="serverlbl"  style="color: red;" runat="server">Please enter a valid Page Size Range betwwwn 1 to 9999</asp:Label>
            <br />
            <br />
                        <div>
            <asp:GridView ID="gvExportReport" runat="server" AutoGenerateColumns="false" AllowPaging="true"
            AllowSorting="true" OnSorting="OnSorting" HeaderStyle-BackColor="#8e24aa" HeaderStyle-ForeColor="White"  OnPageIndexChanging="OnPageIndexChanging"
            PageSize="10"  RowStyle-Wrap="false"  ShowHeaderWhenEmpty="true" HeaderStyle-Wrap="false">
       
                 
            <EmptyDataTemplate >No Record Available</EmptyDataTemplate> 
            <EmptyDataRowStyle HorizontalAlign="Center" />
                 <AlternatingRowStyle BackColor="#e2e2e2" ForeColor="Black"  />
            <HeaderStyle BackColor="#8e24aa" Font-Bold="True" ForeColor="white" />
            <RowStyle ForeColor="Black" BackColor="White" />
        </asp:GridView>
           </div>
                    </div>
                    <br />
                    <div>
                         <asp:Label ID="lblReccount" Class="serverlbl"  runat="server" Text="" Visible="false"></asp:Label>
                         <asp:Label ID="lblTitle" Class="serverlbl"  runat="server"  Width="700px"   ></asp:Label>
                    </div>


                    </div>
                  </div>
                </div>
              </div>
            </div>
          </div>
      </div>
   
       </div>
    











    <div>

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
        <asp:HiddenField  runat="server" ID ="hdnSeqNo" />
        <asp:HiddenField  runat="server" ID ="hdnStoreID" />
        <asp:HiddenField  runat="server" ID ="hdnStoreIDValue" />
   </div>
      
    <div id="dialog" title="message" style="display:none;">
        <asp:Label ID="lblMsg" runat="server" Text=""></asp:Label>
    </div>
   <div style="visibility:hidden">
  <asp:Label ID="lblName" runat="server" CssClass="auto-style186"></asp:Label>
                      <asp:Label ID="lblLoc" runat="server" CssClass="auto-style186"></asp:Label>
   </div>
</asp:Content>