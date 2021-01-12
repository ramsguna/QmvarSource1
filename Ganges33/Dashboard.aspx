<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Analysis.Master" CodeBehind="Dashboard.aspx.vb" Inherits="Ganges33.WebForm1" %>
<%@ Register assembly="System.Web.DataVisualization, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" namespace="System.Web.UI.DataVisualization.Charting" tagprefix="asp" %>

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
                  <h3 class="card-title ">DashBoard</h3>
                  <p class="card-category"></p>
                </div>
  <div  class="card-body scrollbar " id="style-10">
                  <div class="form-group">
            <asp:Label ID="lblInfo" Class="bmd-label-floating" runat="server"></asp:Label>
        </div>
                   <div class="col-sm-12">
                       <div class="col-sm-12">

           <%-- <div class="row col-sm-12" style="vertical-align:top">
                <asp:Button ID="btnUser" runat="server" class="btn btn-primary btn-lbl" Text="Update GSPN Credentials" />
                &nbsp;&nbsp;&nbsp;&nbsp;
                <asp:Button ID="btnUploadPrg" runat="server" class="btn btn-primary btn-lbl" Text="New Task (Python Code)  " />
             &nbsp;&nbsp;&nbsp;&nbsp;
                <asp:Button ID="btnMailManagement" runat="server" class="btn btn-primary btn-lbl" Text="     Mail Management       "  />
            </div>--%>
                         <%--  <asp:Chart ID="Chart1" runat="server" Width="1000px" Height="491px">
                               <Series>
		<asp:Series Name="Testing" YValueType="Int32" YValuesPerPoint="2">

			<%--<Points>
				<asp:DataPoint AxisLabel="Test 1" YValues="10,0" />
				<asp:DataPoint AxisLabel="Test 2" YValues="20,0" />

				<asp:DataPoint AxisLabel="Test 3" YValues="30,0" />
				<asp:DataPoint AxisLabel="Test 4" YValues="40,0" />

			</Points>--%>
		<%--</asp:Series>
	</Series>
	<ChartAreas>
		<asp:ChartArea Name="ChartArea1">
		</asp:ChartArea>

	</ChartAreas>
                           </asp:Chart>--%>
                           <div class="row">
                              

                             <div class="col-sm-4">
                            <div  class=" text-center" >  
                                   <label class="bmd-label-floating " style="font-weight:bold">
                                       Customer Visit
                                   </label>
                               </div>
                               <div>
               
                            <asp:Chart ID="Chart2" runat="server" Width="320px" Height="276px">

            <series>

                <asp:Series Name="Series1" XValueMember="0" YValueMembers="1" BackGradientStyle="Center" BackImageTransparentColor="White" BackSecondaryColor="White" BorderColor="White" Color="White">

                </asp:Series>

            </series>

            <chartareas>

                <asp:ChartArea Name="ChartArea1" BackColor="Orange" BackSecondaryColor ="Yellow">

                </asp:ChartArea>

            </chartareas>

        </asp:Chart>
                                   </div>
                               </div>
                            
                            <div class="col-sm-4">
                            <div class="text-center">    
                                <label class="bmd-label-floating" style=" font-weight:bold">
                                    Call Registerd
                                </label>
                                </div>
                           <div>
                           
                                         <asp:Chart ID="Chart3" runat="server" Width="320px" Height="276px" Palette="SeaGreen">

            <series>

              <asp:Series Name="Series1" XValueMember="0" YValueMembers="2" BackGradientStyle="Center" BackImageTransparentColor="White" BackSecondaryColor="White" BorderColor="White" Color="White">

                </asp:Series>


                <%--<asp:Series  Name="Series2" XValueMember="0" YValueMembers="1" ChartArea="ChartArea1" ChartType="Point" BackImageTransparentColor="White" BorderColor="white" LabelForeColor="White" MarkerBorderColor="White" YValuesPerPoint="2">
                </asp:Series>--%>

                

            </series>

            <chartareas>

                <asp:ChartArea Name="ChartArea1"  BackColor="Red" BackSecondaryColor ="Yellow" ShadowColor="White">

                </asp:ChartArea>

            </chartareas>

        </asp:Chart>
                               </div>
                                   </div>    
                       <div class="col-sm-4">
                                       <div class="text-center" >
                                           <label class="bmd-label-floating" style="font-weight:bold">
                                               Repair completed
                                           </label>
                                       </div>
                                <div>
                                    <asp:Chart ID="Chart4" runat="server" Width="320px" Height="276px">

            <series>

               <asp:Series Name="Series1" XValueMember="0" YValueMembers="3" BackGradientStyle="Center" BackImageTransparentColor="White" BackSecondaryColor="White" BorderColor="White" Color="White">

                </asp:Series>

            </series>

            <chartareas>

                <asp:ChartArea Name="ChartArea1"  BackColor="green" BackSecondaryColor ="Yellow" ShadowColor="White">

                </asp:ChartArea>

            </chartareas>

        </asp:Chart>
                                       </div>
                                   </div>
                               
                           </div>

                           <div class="col-sm-12">

                           </div>
                           <br />
                           <br />
                            <div class="row">
                              
                                  
             <div class="col-sm-4">
                            <div class="text-center">    
                                <label class="bmd-label-floating" style="font-weight:bold">
                                    Goods Delivered
                                </label>
                                </div>
                           <div>
                           
                            <asp:Chart ID="Chart5" runat="server" Width="320px" Height="276px">

            <series>

             <asp:Series Name="Series1" XValueMember="0" YValueMembers="4" BackGradientStyle="Center" BackImageTransparentColor="White" BackSecondaryColor="White" BorderColor="White" Color="White">

                </asp:Series>


               <%-- <asp:Series  Name="Series2" XValueMember="0" YValueMembers="1" ChartArea="ChartArea1" ChartType="Point" BackImageTransparentColor="0, 192, 0" BorderColor="white" LabelForeColor="White" MarkerBorderColor="White">
                </asp:Series>--%>
            </series>

            <chartareas>

                <asp:ChartArea Name="ChartArea1"  BackColor="Blue" BackSecondaryColor ="Yellow" ShadowColor="White">

                </asp:ChartArea>

            </chartareas>

        </asp:Chart>
                             </div>
                                  
                                   </div>
                               <div class="col-sm-4">
                                       <div class="text-center">
                                           <label class="bmd-label-floating" style="font-weight:bold">
                                               Pending Calls
                                           </label>
                                       </div>
                                        <div>
 <asp:Chart ID="Chart6" runat="server" Width="320px" Height="276px">

            <series>

                 <asp:Series Name="Series1" XValueMember="0" YValueMembers="5" BackGradientStyle="Center" BackImageTransparentColor="White" BackSecondaryColor="White" BorderColor="White" Color="White">

                </asp:Series>

            </series>

            <chartareas>

                <asp:ChartArea Name="ChartArea1"  BackColor="#EE00EE" BackSecondaryColor ="Yellow" ShadowColor="White">

                </asp:ChartArea>

            </chartareas>

        </asp:Chart>
                              </div>
                                  </div>
                               
                                    <div class="col-sm-4">
                            <div class="text-center">    
                                <label class="bmd-label-floating"  style="font-weight:bold">
                                    Cancelled Calls
                                </label>
                                </div>
                           <div>
                            <asp:Chart ID="Chart7" runat="server" Width="320px" Height="276px">

            <series>
<asp:Series Name="Series1" XValueMember="0" YValueMembers="6" BackGradientStyle="Center" BackImageTransparentColor="White" BackSecondaryColor="White" BorderColor="White" Color="White">

                </asp:Series>


<%--                <asp:Series  Name="Series2" XValueMember="0" YValueMembers="1" ChartArea="ChartArea1" ChartType="Point" BackImageTransparentColor="0, 192, 0" BorderColor="white" LabelForeColor="White" MarkerBorderColor="White">
                </asp:Series>--%>
            </series>

            <chartareas>
 <asp:ChartArea Name="ChartArea1"  BackColor="#420042" BackSecondaryColor ="Yellow" ShadowColor="White">

                </asp:ChartArea>

            </chartareas>

        </asp:Chart>
                               </div>
                                </div>
                               
                         </div>
                                <br />
                           <br />
                            
                                <div class="row">
                               
                                 
                                    </div>

   </div>
                          
              <div class="col-sm-4" style="display:none">
                            <div class=" bg-white">    
                                <label class="bmd-label-floating" style="color:white">
                                   .
                                </label>
                                </div>
                           <div class="col-sm-6">
                           <asp:Chart ID="Chart1" runat="server"  Width="320px" Height="276px" BackColor="Transparent" Palette="None" BackImageTransparentColor="White" BackSecondaryColor="White" TextAntiAliasingQuality="Normal">

            <series>

                <asp:Series Name="Series1" XValueMember="0" YValueMembers="1" ChartType="FastLine" BackImageTransparentColor="0, 192, 0" BorderColor="white" LabelForeColor="White" MarkerBorderColor="White">

                </asp:Series>

                <asp:Series  Name="Series2" XValueMember="0" YValueMembers="1" ChartArea="ChartArea1" ChartType="Point" BackImageTransparentColor="0, 192, 0" BorderColor="white" LabelForeColor="White" MarkerBorderColor="White">
                </asp:Series>

            </series>

            <chartareas>

                <asp:ChartArea Name="ChartArea1" BackColor="Green" BackSecondaryColor="yellow" BorderColor="White" ShadowColor="MintCream">

                </asp:ChartArea>

            </chartareas>

                               <BorderSkin BackColor="White" BorderColor="White" />

        </asp:Chart>
                     </div>       
                  </div>                </div>
                           <asp:SqlDataSource ID="SqlDataSource1" runat="server"></asp:SqlDataSource>
                           </div>
          <%--  <div class="col-sm-11"> <div class="GridviewDiv" id="divGspn" runat="server" visible="false">
                <br />
<asp:GridView runat="server" ID="gvDetails" AllowPaging="true" PageSize="10" AutoGenerateColumns="false" HeaderStyle-BackColor="#8e24aa" HeaderStyle-ForeColor="White"  Width="420px" OnPageIndexChanging="gvDetails_PageIndexChanging">
<HeaderStyle BackColor="#8e24aa" />
<Columns>
 <asp:TemplateField HeaderText="Select">  
<ItemTemplate>  
 <asp:CheckBox ID="chkChangePwd" runat="server" />  
 </ItemTemplate>  
</asp:TemplateField>  
<asp:BoundField DataField="ship_code" HeaderText="ship_code" Visible="false" />
 <asp:TemplateField HeaderText="ship_code"  Visible="false">
                        <ItemTemplate>
                            <asp:Label ID="lblShipCode" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "ship_code") %>'></asp:Label>   
                            <asp:Label ID="lblShipName" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "ship_name") %>'></asp:Label>            
                        </ItemTemplate>
                    </asp:TemplateField>

<asp:BoundField DataField="ship_name" HeaderText="Branch" />
<asp:BoundField DataField="RpaClientUserId" HeaderText="GSPN&nbsp;Username"  />
<asp:BoundField DataField="RpaClientPwd" HeaderText="Password" />

  <asp:TemplateField HeaderText="New GSPN Usernmae"  Visible="true">
                        <ItemTemplate>  
                            <asp:TextBox ID="txtClientUsername" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "RpaClientUserId") %>'></asp:TextBox>            
                        </ItemTemplate>  
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="New Password" Visible="true">
                        <ItemTemplate>
                            <asp:TextBox ID="txtPassword" runat="server" Text=""></asp:TextBox>            
                        </ItemTemplate>
                    </asp:TemplateField>

</Columns>
</asp:GridView><br />
                <asp:Button id="btnUpDateGspnPwd" runat="server" class="btn btn-primary pull-right" Text="Update GSPN password"/>
            <br />     
            </div>
                <br />
                <div class="GridviewDiv" id="divMailManagement" runat="server" visible="false">
                
                       <asp:GridView ID="gvRPAMail" runat="server" Width="100%" 
                    AutoGenerateColumns="false" ShowFooter="true"
                    onrowcommand="gvRPAMail_RowCommand" 
                     HeaderStyle-BackColor="#8e24aa"
                           HeaderStyle-ForeColor="White" 
                    onrowdeleting="gvRPAMail_RowDeleting" 
                    onrowupdating="gvRPAMail_RowUpdating" 
                    onrowcancelingedit="gvRPAMail_RowCancelingEdit" 
                    onrowediting="gvRPAMail_RowEditing">
                <AlternatingRowStyle BackColor="#F0F0F0" VerticalAlign="Top" />
                <RowStyle VerticalAlign="Top" />
                <EditRowStyle VerticalAlign="Top" />
                <FooterStyle VerticalAlign="Top" />
                <Columns>            
                    <asp:TemplateField HeaderText="RPA ID" Visible="false">
                        <ItemTemplate >
                            <asp:Label ID="lblEmailId" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "EMAILID") %>'></asp:Label>
                        </ItemTemplate>
                        <EditItemTemplate>            
                            <asp:Label ID="lblEditEmailId" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "EMAILID") %>'></asp:Label>            
                        </EditItemTemplate>
                        <FooterTemplate>
                            <asp:TextBox ID="txtAddEmailId" runat="server" ></asp:TextBox>
                        </FooterTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="Email Type" >
                        <ItemTemplate>
                            <asp:Label ID="lblEmailType" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "EMAIL_TYPE") %>'></asp:Label>
                        </ItemTemplate>
                        <EditItemTemplate>            
                            <asp:TextBox ID="txtEditEmailType" runat="server" Enabled="false"  Text='<%#DataBinder.Eval(Container.DataItem, "EMAIL_TYPE") %>'></asp:TextBox>            
                        </EditItemTemplate>
                        <FooterTemplate>
                            <asp:TextBox ID="txtAddEmailType" runat="server" ></asp:TextBox>
                        </FooterTemplate>
                    </asp:TemplateField>

                                           <asp:TemplateField HeaderText="Smtp"  >
                        <ItemTemplate>
                            <asp:Label ID="lblSmtp" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "SMTP") %>'></asp:Label>
                        </ItemTemplate>
                        <EditItemTemplate>            
                            <asp:TextBox ID="txtEditSmtp" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "SMTP") %>'></asp:TextBox>            
                        </EditItemTemplate>
                        <FooterTemplate>
                            <asp:TextBox ID="txtAddSmtp" runat="server" ></asp:TextBox>
                        </FooterTemplate>
                    </asp:TemplateField>

                        <asp:TemplateField HeaderText="SMTP_PORT"  Visible="false">
                        <ItemTemplate>
                            <asp:Label ID="lblSmtpPort" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "SMTP_PORT") %>'></asp:Label>
                        </ItemTemplate>
                        <EditItemTemplate>            
                            <asp:TextBox ID="txtEditSmtpPort" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "SMTP_PORT") %>'></asp:TextBox>            
                        </EditItemTemplate>
                        <FooterTemplate>
                            <asp:TextBox ID="txtAddSmtpPort" runat="server" Text="587" ></asp:TextBox>
                        </FooterTemplate>
                    </asp:TemplateField>
 

                        <asp:TemplateField HeaderText="SMTP_SSL_ENABLE"  Visible="false">
                        <ItemTemplate>
                            <asp:Label ID="lblSmtpSslEnable" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "SMTP_SSL_ENABLE") %>'></asp:Label>
                        </ItemTemplate>
                        <EditItemTemplate>            
                            <asp:TextBox ID="txtEditSmtpSslEnable" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "SMTP_SSL_ENABLE") %>'></asp:TextBox>            
                        </EditItemTemplate>
                        <FooterTemplate>
                            <asp:TextBox ID="txtAddSmtpSslEnable" runat="server" Text="0" ></asp:TextBox>
                        </FooterTemplate>
                    </asp:TemplateField>

 
                         <asp:TemplateField HeaderText="Smtp Username"  >
                        <ItemTemplate>
                            <asp:Label ID="lblSmtpCredentialsUserName" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "SMTP_CREDENTIALS_USER_NAME") %>'></asp:Label>
                        </ItemTemplate>
                        <EditItemTemplate>            
                            <asp:TextBox ID="txtEditSmtpCredentialsUserName" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "SMTP_CREDENTIALS_USER_NAME") %>'></asp:TextBox>            
                        </EditItemTemplate>
                        <FooterTemplate>
                            <asp:TextBox ID="txtAddSmtpCredentialsUserName" runat="server" ></asp:TextBox>
                        </FooterTemplate>
                    </asp:TemplateField>

                            <asp:TemplateField HeaderText="Smtp Password"  >
                        <ItemTemplate>
                            <asp:Label ID="lblSmtpCredentialsUserPassword" Visible="false"  runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "SMTP_CREDENTIALS_USER_PASSWORD") %>'></asp:Label>
                        </ItemTemplate>
                        <EditItemTemplate>            
                            <asp:TextBox ID="txtEditSmtpCredentialsUserPassword" visible="false" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "SMTP_CREDENTIALS_USER_PASSWORD") %>'></asp:TextBox>            
                        </EditItemTemplate>
                        <FooterTemplate>
                            <asp:TextBox ID="txtAddSmtpCredentialsUserPassword" runat="server" ></asp:TextBox>
                        </FooterTemplate>
                    </asp:TemplateField>

                                               <asp:TemplateField HeaderText="Sender"  >
                        <ItemTemplate>
                            <asp:Label ID="lblSender" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "SENDER") %>'></asp:Label>
                        </ItemTemplate>
                        <EditItemTemplate>            
                            <asp:TextBox ID="txtEditSender" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "SENDER") %>'></asp:TextBox>            
                        </EditItemTemplate>
                        <FooterTemplate>
                            <asp:TextBox ID="txtAddSender" runat="server" ></asp:TextBox>
                        </FooterTemplate>
                    </asp:TemplateField>

                       <asp:TemplateField HeaderText="Email To"  >
                        <ItemTemplate>
                            <asp:Label ID="lblEmailTo" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "EMAIL_TO") %>'></asp:Label>
                        </ItemTemplate>
                        <EditItemTemplate>            
                            <asp:TextBox ID="txtEditEmailTo" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "EMAIL_TO") %>'></asp:TextBox>            
                        </EditItemTemplate>
                        <FooterTemplate>
                            <asp:TextBox ID="txtAddEmailTo" runat="server" ></asp:TextBox>
                        </FooterTemplate>
                    </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Email Cc"  >
                        <ItemTemplate>
                            <asp:Label ID="lblEmailCc" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "EMAIL_CC") %>'></asp:Label>
                        </ItemTemplate>
                        <EditItemTemplate>            
                            <asp:TextBox ID="txtEditEmailCc" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "EMAIL_CC") %>'></asp:TextBox>            
                        </EditItemTemplate>
                        <FooterTemplate>
                            <asp:TextBox ID="txtAddEmailCc" runat="server" ></asp:TextBox>
                        </FooterTemplate>
                    </asp:TemplateField>


                     <asp:TemplateField HeaderText="Email Bcc"  >
                        <ItemTemplate>
                            <asp:Label ID="lblEmailBcc" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "EMAIL_BCC") %>'></asp:Label>
                        </ItemTemplate>
                        <EditItemTemplate>            
                            <asp:TextBox ID="txtEditEmailBcc" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "EMAIL_BCC") %>'></asp:TextBox>            
                        </EditItemTemplate>
                        <FooterTemplate>
                            <asp:TextBox ID="txtAddEmailBcc" runat="server" ></asp:TextBox>
                        </FooterTemplate>
                    </asp:TemplateField>

                     <asp:TemplateField HeaderText="Select SSC"  >
                    <ItemTemplate>
                         <asp:CheckBoxList ID="chkLstSsc" runat="server" DataSource="<%# SelectSSC() %>"
                              DataTextField="ship_name"  DataValueField="ship_code"  
                             RepeatDirection="Horizontal" RepeatColumns="4"  Width="432px">  
                            </asp:CheckBoxList>   
                    </ItemTemplate>
                          <EditItemTemplate>
                              <asp:CheckBoxList ID="chkLstEditSsc" runat="server" DataSource="<%# SelectSSC() %>"  
                                   DataTextField="ship_name"  DataValueField="ship_code"
                                  RepeatDirection="Horizontal" RepeatColumns="4" Width="432px">  
                            </asp:CheckBoxList>  
                          </EditItemTemplate>
                         <FooterTemplate>
                               <asp:CheckBoxList ID="chkLstAddSsc" runat="server" DataSource="<%# SelectSSC() %>"  
                                   DataTextField="ship_name"  DataValueField="ship_code"
                                   RepeatDirection="Horizontal" RepeatColumns="4" Width="432px">  
                            </asp:CheckBoxList>  
                          </FooterTemplate>
                </asp:TemplateField>

                        <asp:TemplateField HeaderText="Source"  Visible="false">
                        <ItemTemplate>
                            <asp:Label ID="lblSource" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "SOURCE") %>'></asp:Label>
                        </ItemTemplate>
                        <EditItemTemplate>            
                            <asp:TextBox ID="txtEditSource" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "SOURCE") %>'></asp:TextBox>            
                        </EditItemTemplate>
                        <FooterTemplate>
                            <asp:TextBox ID="txtAddSource" runat="server" ></asp:TextBox>
                        </FooterTemplate>
                    </asp:TemplateField>

                                          <asp:TemplateField HeaderText="status"  Visible="false">
                        <ItemTemplate>
                            <asp:Label ID="lblStatus" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "status") %>'></asp:Label>
                        </ItemTemplate>
                        <EditItemTemplate>            
                            <asp:TextBox ID="txtEditStatus" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "status") %>'></asp:TextBox>            
                        </EditItemTemplate>
                        <FooterTemplate>
                            <asp:TextBox ID="txtAddStatus" runat="server" ></asp:TextBox>
                        </FooterTemplate>
                    </asp:TemplateField>

                             <asp:TemplateField HeaderText="Status">
                        <ItemTemplate>
                            <asp:DropDownList ID="drpStatus" runat="server" Enabled="false" >
                                 <asp:ListItem Enabled="true" Text="Active" Value="0"></asp:ListItem>
                                <asp:ListItem Text="Inactive" Value="1"></asp:ListItem>
                            </asp:DropDownList>
                        </ItemTemplate>
                        <EditItemTemplate> 
                            <asp:DropDownList ID="drpEditStatus" runat="server">
                                 <asp:ListItem Enabled="true" Text="Active" Value="0"></asp:ListItem>
                                <asp:ListItem Text="Inactive" Value="1"></asp:ListItem>
                            </asp:DropDownList>
                        </EditItemTemplate>
                        <FooterTemplate>
                            <asp:DropDownList ID="drpAddStatus" runat="server">
                             <asp:ListItem Enabled="true" Text="Active" Value="0"></asp:ListItem>
                                <asp:ListItem Text="Inactive" Value="1"></asp:ListItem>
                                </asp:DropDownList>
                        </FooterTemplate>
                    </asp:TemplateField>
 
                    <asp:TemplateField HeaderText="Action">
                        <ItemTemplate>
                           <asp:ImageButton ID="imgbtnEdit" runat="server" CommandName="Edit" ImageUrl="~/Image/icon-edit.png" Height="32px" Width="32px" OnClientClick="return confirm('Are you sure you want to edit this email type?');"/>
                           <asp:ImageButton ID="imgbtnDelete" runat="server" CommandName="Delete" ImageUrl="~/Image/Delete.png"  Height="32px" Width="32px"  OnClientClick="return confirm('Are you sure you want to delete this email type from the email managements?');"/>
                             
                        </ItemTemplate>
                        <EditItemTemplate>
                           <asp:ImageButton ID="imgbtnUpdate" runat="server" CommandName="Update" ImageUrl="~/Image/icon-update.png"  Height="32px" Width="32px"/>
                           <asp:ImageButton ID="imgbtnCancel" runat="server" CommandName="Cancel" ImageUrl="~/Image/pause.png"  Height="32px" Width="32px"/>
                        </EditItemTemplate>
                        <FooterTemplate>
                           <asp:LinkButton ID="lbtnAdd" runat="server" CommandName="ADD" Text="Add" Width="100px"></asp:LinkButton> 
                        </FooterTemplate>
                    </asp:TemplateField>                    
                </Columns>            
                <HeaderStyle BackColor="#8e24aa" />
            </asp:GridView> 
          --%>          <br />
                    
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

</asp:Content>
