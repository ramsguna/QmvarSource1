<%@ Page Language="vb" AutoEventWireup="true" CodeBehind="test.aspx.vb" Inherits="Ganges33.test" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="ajaxToolkit" %>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
       <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>

    <div>
    <br />
    <br />
        <table width="800px" align="center">
            <tr>
                <td colspan="2" align="center"><b>RPA Scheduler</b></td>
            </tr>          
            <tr>
            <td colspan="2">
            <asp:GridView ID="gvRPADetails" runat="server" Width="100%" 
                    AutoGenerateColumns="false" ShowFooter="true"
                    onrowcommand="gvRPADetails_RowCommand" 
                    onrowdeleting="gvRPADetails_RowDeleting" 
                    onrowupdating="gvRPADetails_RowUpdating" 
                    onrowcancelingedit="gvRPADetails_RowCancelingEdit" 
                    onrowediting="gvRPADetails_RowEditing">
                <Columns>            
                    <asp:TemplateField HeaderText="RPA ID" Visible="false">
                        <ItemTemplate>
                            <asp:Label ID="lblRPASCHID" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "RPASCHID") %>'></asp:Label>
                        </ItemTemplate>
                        <EditItemTemplate>            
                            <asp:Label ID="lblEditRPASCHID" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "RPASCHID") %>'></asp:Label>            
                        </EditItemTemplate>
                        <FooterTemplate>
                            <asp:TextBox ID="txtAddRPASCHID" runat="server" ></asp:TextBox>
                        </FooterTemplate>
                    </asp:TemplateField>
 
                    <asp:TemplateField HeaderText="TASK_NAME" >
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
 
                    <asp:TemplateField HeaderText="TASK_SOURCE" >
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
 
                    <asp:TemplateField HeaderText="START_DATETIME">
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



                    <asp:TemplateField HeaderText="END_DATETIME">
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

 
                    <asp:TemplateField HeaderText="RECURRING_TYPE">
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


                      <asp:TemplateField HeaderText="REPEAT_TIME">
                        <ItemTemplate>
                            <asp:Label ID="lblREPEAT_TIME" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "REPEAT_TIME") %>'></asp:Label>
                        </ItemTemplate>
                        <EditItemTemplate>            
                            <asp:TextBox ID="txtEditREPEAT_TIME" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "REPEAT_TIME") %>'></asp:TextBox>            
                        </EditItemTemplate>
                        <FooterTemplate>
                            <asp:TextBox ID="txtAddREPEAT_TIME" runat="server" ></asp:TextBox>
                        </FooterTemplate>
                    </asp:TemplateField>


                       <asp:TemplateField HeaderText="STATUS">
                        <ItemTemplate>
                            <asp:Label ID="lblSTATUS" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "STATUS") %>'></asp:Label>
                        </ItemTemplate>
                        
                    </asp:TemplateField>



                      <asp:TemplateField HeaderText="LAST_RUN_DATETIME">
                        <ItemTemplate>
                            <asp:Label ID="lblLAST_RUN_DATETIME" runat="server"  Text='<%#DataBinder.Eval(Container.DataItem, "LAST_RUN_DATETIME") %>'></asp:Label>
                        </ItemTemplate>
                        
                    </asp:TemplateField>

                       <asp:TemplateField HeaderText="LAST_RUN_STATUS">
                        <ItemTemplate>
                            <asp:Label ID="lblLAST_RUN_STATUS" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "LAST_RUN_STATUS") %>'></asp:Label>
                        </ItemTemplate>
                      
                    </asp:TemplateField>
 
                    <asp:TemplateField HeaderText="Action">
                        <ItemTemplate>
                           <asp:ImageButton ID="imgbtnEdit" runat="server" CommandName="Edit" ImageUrl="~/Image/icon-edit.png" Height="32px" Width="32px"/>
                           <asp:ImageButton ID="imgbtnDelete" runat="server" CommandName="Delete" ImageUrl="~/Image/Delete.png"  Height="32px" Width="32px"/>
                        </ItemTemplate>
                        <EditItemTemplate>
                           <asp:ImageButton ID="imgbtnUpdate" runat="server" CommandName="Update" ImageUrl="~/Image/icon-update.png"  Height="32px" Width="32px"/>
                           <asp:ImageButton ID="imgbtnCancel" runat="server" CommandName="Cancel" ImageUrl="~/Image/icon-Cancel.png"  Height="32px" Width="32px"/>
                        </EditItemTemplate>
                        <FooterTemplate>
                           <asp:LinkButton ID="lbtnAdd" runat="server" CommandName="ADD" Text="Add" Width="100px"></asp:LinkButton> 
                        </FooterTemplate>
                    </asp:TemplateField>                    
                </Columns>            
            </asp:GridView> 
            </td>
            </tr>
        </table>
    </div>
    </form>
</body>
</html> 