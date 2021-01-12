<%@ Page Title="" Language="vb" AutoEventWireup="true" MasterPageFile="~/Analysis.Master" CodeBehind="rpa_scheduler.aspx.vb" Inherits="Ganges33.rpa_scheduler" %>

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
    

   
   <div class="wrapper  col-sm-12 sidebar-wrapper position-fixed  contain" id="style-10" >
   
    <div class="content "  >
        <div class="container-fluid">
          <div class="row">
            <div class="col-md-12">
              <div class="card " >
                <div class="card-header card-header-primary">
                              <h3 class="card-title ">
                                  RPA Scheduler
                                  </h3>
                             </div>
                
                  <div class="card-body scrollbar " id="style-10" style="height:460px">
                 

                          <br />
                          <br />
                          <br />

         <div class="col-md-12">
          
            <asp:GridView ID="gvRPADetails" runat="server" Width="100%" 
                    AutoGenerateColumns="false" ShowFooter="true"
                    onrowcommand="gvRPADetails_RowCommand" 
                    
                    onrowdeleting="gvRPADetails_RowDeleting" 
                    onrowupdating="gvRPADetails_RowUpdating" 
                    onrowcancelingedit="gvRPADetails_RowCancelingEdit" 
                    onrowediting="gvRPADetails_RowEditing">
                <AlternatingRowStyle BackColor="#F0F0F0" VerticalAlign="Top" />
                <RowStyle VerticalAlign="Top" />
                <EditRowStyle VerticalAlign="Top" />
                <FooterStyle VerticalAlign="Top" />
                <Columns>            
                    <asp:TemplateField HeaderText="RPA ID" Visible="false">
                        <ItemTemplate >
                            <asp:Label ID="lblRPASCHID" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "RPASCHID") %>'></asp:Label>
                        </ItemTemplate>
                        <EditItemTemplate>            
                            <asp:Label ID="lblEditRPASCHID" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "RPASCHID") %>'></asp:Label>            
                        </EditItemTemplate>
                        <FooterTemplate>
                            <asp:TextBox ID="txtAddRPASCHID" runat="server" ></asp:TextBox>
                        </FooterTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="Scheduler Name" >
                        <ItemTemplate>
                            <asp:Label ID="lblSCHEDULER_NAME" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "SCHEDULER_NAME") %>'></asp:Label>
                        </ItemTemplate>
                        <EditItemTemplate>            
                            <asp:TextBox ID="txtEditSCHEDULER_NAME" runat="server" Enabled="false"  Text='<%#DataBinder.Eval(Container.DataItem, "SCHEDULER_NAME") %>'></asp:TextBox>            
                        </EditItemTemplate>
                        <FooterTemplate>
                            <asp:TextBox ID="txtAddSCHEDULER_NAME" runat="server" ></asp:TextBox>
                        </FooterTemplate>
                    </asp:TemplateField>

                        <asp:TemplateField HeaderText="TASK_SOURCE"  Visible="false">
                        <ItemTemplate>
                            <asp:Label ID="lblTASK_SOURCE" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "TASK_SOURCE") %>'></asp:Label>
                        </ItemTemplate>
                        <EditItemTemplate>            
                            <asp:TextBox ID="txtEditTASK_SOURCE" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "TASK_SOURCE") %>'></asp:TextBox>            
                        </EditItemTemplate>
                        <FooterTemplate>
                            <asp:TextBox ID="txtAddTASK_SOURCE" runat="server" ></asp:TextBox>
                        </FooterTemplate>
                    </asp:TemplateField>
 
 
                         <asp:TemplateField HeaderText="TASK_NAME"  Visible="false">
                        <ItemTemplate>
                            <asp:Label ID="lblTASK_NAME" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "TASK_NAME") %>'></asp:Label>
                        </ItemTemplate>
                        <EditItemTemplate>            
                            <asp:TextBox ID="txtEditTASK_NAME" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "TASK_NAME") %>'></asp:TextBox>            
                        </EditItemTemplate>
                        <FooterTemplate>
                            <asp:TextBox ID="txtAddTASK_NAME" runat="server" ></asp:TextBox>
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


                      <asp:TemplateField HeaderText="Task Name">
                        <ItemTemplate>
                            <asp:DropDownList ID="drpTASK_NAME" runat="server"  Enabled="false" DataSource="<%# SelectTask() %>" 
                                 DataTextField="TASK_NAME"  DataValueField="TASKID">
                            </asp:DropDownList>
                        </ItemTemplate>
                        <EditItemTemplate> 
                            <asp:DropDownList ID="drpEditTASK_NAME" runat="server"   DataSource="<%# SelectTask() %>" 
                                 DataTextField="TASK_NAME"  DataValueField="TASKID">                                
                            </asp:DropDownList>
                        </EditItemTemplate>
                        <FooterTemplate>
                            <asp:DropDownList ID="drpAddTASK_NAME" runat="server"  DataSource="<%# SelectTask() %>" 
                             DataTextField="TASK_NAME"  DataValueField="TASKID">
                                </asp:DropDownList>
                        </FooterTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="Start DateTime">
                        <ItemTemplate>
                            <asp:Label ID="lblSTART_DATETIME" runat="server"  Text='<%#DataBinder.Eval(Container.DataItem, "START_DATETIME") %>'></asp:Label>
                        </ItemTemplate>
                        <EditItemTemplate>            
                            <asp:TextBox ID="txtEditSTART_DATETIME"  runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "START_DATETIME") %>'></asp:TextBox>  
                            <ajaxToolkit:CalendarExtender ID="CalendarExtender1" Format="yyyy/MM/dd" runat="server" BehaviorID="txtEditSTART_DATETIME" TargetControlID="txtEditSTART_DATETIME" PopupPosition="Left">
                      </ajaxToolkit:CalendarExtender>
                    </ajax:CalendarExtender>
                        </EditItemTemplate>
                        <FooterTemplate>
                            <asp:TextBox ID="txtAddSTART_DATETIME"   runat="server" ></asp:TextBox>
                            <ajaxToolkit:CalendarExtender ID="CalendarExtender2" Format="yyyy/MM/dd" runat="server" BehaviorID="txtAddSTART_DATETIME" TargetControlID="txtAddSTART_DATETIME" PopupPosition="Left">
                      </ajaxToolkit:CalendarExtender>
                    </ajax:CalendarExtender>
                        </FooterTemplate>
                    </asp:TemplateField>



                    <asp:TemplateField HeaderText="End DateTime">
                        <ItemTemplate>
                            <asp:Label ID="lblEND_DATETIME" runat="server"  Text='<%#DataBinder.Eval(Container.DataItem, "END_DATETIME") %>'></asp:Label>
                        </ItemTemplate>
                        <EditItemTemplate>            
                            <asp:TextBox ID="txtEditEND_DATETIME"  runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "END_DATETIME") %>'></asp:TextBox>  
                            <ajaxToolkit:CalendarExtender ID="CalendarExtender3" Format="yyyy/MM/dd" runat="server" BehaviorID="txtEditEND_DATETIME" TargetControlID="txtEditEND_DATETIME" PopupPosition="Left">
                      </ajaxToolkit:CalendarExtender>
                    </ajax:CalendarExtender>
                        </EditItemTemplate>
                        <FooterTemplate>
                            <asp:TextBox ID="txtAddEND_DATETIME"   runat="server" ></asp:TextBox>
                            <ajaxToolkit:CalendarExtender ID="CalendarExtender4" Format="yyyy/MM/dd" runat="server" BehaviorID="txtAddEND_DATETIME" TargetControlID="txtAddEND_DATETIME" PopupPosition="Left">
                      </ajaxToolkit:CalendarExtender>
                    </ajax:CalendarExtender>
                        </FooterTemplate>
                    </asp:TemplateField>

 
                    <asp:TemplateField HeaderText="Recurring Type (Daily/Weekly/10 Days/Monthly)" Visible ="false">
                        <ItemTemplate>
                            <asp:Label ID="lblRECURRING_TYPE" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "RECURRING_TYPE") %>'></asp:Label>
                        </ItemTemplate>
                        <EditItemTemplate>            
                            <asp:TextBox ID="txtEditRECURRING_TYPE" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "RECURRING_TYPE") %>'></asp:TextBox>            
                        </EditItemTemplate>
                        <FooterTemplate>
                            <asp:TextBox ID="txtAddRECURRING_TYPE" runat="server" ></asp:TextBox>
                        </FooterTemplate>
                    </asp:TemplateField>


                     


                      <asp:TemplateField HeaderText="Repeat Time (Minutes)" Visible="false">
                        <ItemTemplate>
                            <asp:Label ID="lblREPEAT_TIME" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "REPEAT_TIME") %>'></asp:Label>
                        </ItemTemplate>
                        <EditItemTemplate>            
                            <asp:TextBox ID="txtEditREPEAT_TIME" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "REPEAT_TIME") %>'></asp:TextBox>            
                        </EditItemTemplate>
                        <FooterTemplate>
                            <asp:TextBox ID="txtAddREPEAT_TIME" runat="server" Text="0" ></asp:TextBox>
                        </FooterTemplate>
                    </asp:TemplateField>


                       <asp:TemplateField HeaderText="Status">
                        <ItemTemplate>
                            <asp:Label ID="lblSTATUS" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "STATUS") %>'></asp:Label>
                        </ItemTemplate>
                        
                    </asp:TemplateField>



                      <asp:TemplateField HeaderText="LAST_RUN_DATETIME" Visible="false">
                        <ItemTemplate>
                            <asp:Label ID="lblLAST_RUN_DATETIME" runat="server"  Text='<%#DataBinder.Eval(Container.DataItem, "LAST_RUN_DATETIME") %>'></asp:Label>
                        </ItemTemplate>
                        
                    </asp:TemplateField>

                       <asp:TemplateField HeaderText="LAST_RUN_STATUS" Visible="false">
                        <ItemTemplate>
                            <asp:Label ID="lblLAST_RUN_STATUS" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "LAST_RUN_STATUS") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>

                     <asp:TemplateField HeaderText="BATCH_FILE" Visible="false">
                        <ItemTemplate>
                            <asp:Label ID="lblBATCH_FILE" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "BATCH_FILE") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
 
                    <asp:TemplateField HeaderText="Action">
                        <ItemTemplate>
                           <asp:ImageButton ID="imgbtnEdit" runat="server" CommandName="Edit" ImageUrl="Image/icon-edit.png" Height="32px" Width="32px" OnClientClick="return confirm('Are you sure you want to edit this task?');"/>
                           <asp:ImageButton ID="imgbtnPause" runat="server" CommandName="Pause" CommandArgument="<%# Container.DataItemIndex %>" ImageUrl="Image/pause.png"  Height="32px" Width="32px" OnClientClick="return confirm('Are you sure you want to disable this task?');"/>
                            <asp:ImageButton ID="imgbtnRun" runat="server" CommandName="Run" CommandArgument="<%# Container.DataItemIndex %>" ImageUrl="Image/icon-run.png"  Height="32px" Width="32px" OnClientClick="return confirm('Are you sure you want to run this task?');"/>
                           <asp:ImageButton ID="imgbtnDelete" runat="server" CommandName="Delete" ImageUrl="Image/Delete.png"  Height="32px" Width="32px"  OnClientClick="return confirm('Are you sure you want to delete this task from the scheduler?');"/>
                             
                        </ItemTemplate>
                        <EditItemTemplate>
                           <asp:ImageButton ID="imgbtnUpdate" runat="server" CommandName="Update" ImageUrl="Image/icon-update.png"  Height="32px" Width="32px"/>
                           <asp:ImageButton ID="imgbtnCancel" runat="server" CommandName="Cancel" ImageUrl="Image/pause.png"  Height="32px" Width="32px"/>
                        </EditItemTemplate>
                        <FooterTemplate>
                           <asp:LinkButton ID="lbtnAdd" runat="server" CommandName="ADD" Text="Add" Width="100px"></asp:LinkButton> 
                        </FooterTemplate>
                    </asp:TemplateField>                    
                </Columns>            
                <HeaderStyle BackColor="#F0F0F0" />
            </asp:GridView> 
           </div>
</div>
                    </div>
            </div>
    </div>
           </div>
    <div id="dialog" title="message" style="display:none;"> >
        <asp:Label ID="lblMsg" runat="server" Text=""></asp:Label>
    </div>

                  
               </div>
               </div>
               </div>
              
                      
                 
					
                
    
</asp:Content>

