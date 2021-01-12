<%@ Page Language="vb"  Title="QMVAR-Analysis Data" AutoEventWireup="false" MasterPageFile="~/Analysis.Master"  CodeBehind="Analysis_Store_Management.aspx.vb" Inherits="Ganges33.Analysis_Store_Management" %>

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
              <div class="card">                        <div class="card-header card-header-primary">


                              <h3 class="card-title ">
                                  <asp:Label ID="Label32" runat="server"  >Analysis Data</asp:Label>
                                   <asp:Label ID="Label3" runat="server"  >Store Management</asp:Label>
                        <asp:Label ID="Label43" runat="server"  Text="Credit Info Management"></asp:Label>
                        <asp:Label ID="Label40" runat="server"  Text="General Info "></asp:Label>
                        <asp:Label ID="Label104" runat="server"  Text="Count DRS"></asp:Label>
                         <asp:Label ID="Label91" runat="server"  Text="Payment Value"></asp:Label>
                         <asp:Label ID="Label200" runat="server" Text="Collection"></asp:Label>
                 
                              </h3>
                           <p class="card-category"></p>
                           </div>

                          <br />
                           <br />
                            <div class="card-body scrollbar" id="style-10">
                           <div >
                               <div class="col-sm-12 row">
                                <div class="col-sm-2">
                           
                               
                                <label class="bmd-label-floating"> Process Model</label>
                                  
                            </div>
                               <div class=" ">
                             <asp:DropDownList ID="drpManagementFunc" runat="server" AutoPostBack="true" Class="form-control" Width="220px" style="height: 33px;" >
                           <asp:ListItem Text="Select" Value="00"></asp:ListItem>
                           <asp:ListItem Text="Store Management" Value="01"></asp:ListItem>
                           <asp:ListItem Text=" Credit Info Management" Value="02"></asp:ListItem>
                           <asp:ListItem Text=" General Info " Value="03"></asp:ListItem>
                           <asp:ListItem Text="Count DRS" Value="04"></asp:ListItem>
                           <asp:ListItem Text="Payment Value" Value="05"></asp:ListItem>
                            <asp:ListItem Text="Collection" Value="06"></asp:ListItem>
                       </asp:DropDownList>
                               </div>
                          </div>
                               </div>
                         
                          <%--startdiv--%>

                          <%--************  Stored Management **************--%>
                          <div class="" runat="server" id="Stored">
                          <div class="row col-md-12">
                                  <div class="col-sm-2">
                                    <div class="form-group droplistlocation">
                                <label class="bmd-label-floating"> Download Type</label>
                                  </div>
                                   </div>
                              <div class="form-group droplistlocation">
                                    <asp:DropDownList ID="DrpDownloadExcel" runat="server" CssClass="Form-Control" Height="33px" Width="220px">
                            <asp:ListItem Text="CSV" Value="01"></asp:ListItem>
                            <asp:ListItem Text="Excel" Value="02"></asp:ListItem>
                              </asp:DropDownList>
                              </div>
                          </div>
                        
                               <div class="row col-md-12">
                                  <div class="col-sm-2">
                                    <div class="form-group droplistlocation">
                                <label class="bmd-label-floating">Target Store</label>
                                  </div>
                                   </div>
                              <div class="form-group droplistlocation">
                                    <asp:DropDownList ID="DropListLocation" runat="server" CssClass="Form-control" Height="33px" Width="220px">
                                  </asp:DropDownList>
                              </div>
                          </div>

                         
                           <div class="row col-md-12">
                                  <div class="col-sm-2">
                                    <div class="form-group droplistlocation">
                                <label class="bmd-label-floating">Select Type</label>
                                  </div>
                                   </div>
                              <div class="form-group droplistlocation">
                                     <asp:DropDownList ID="DrpType" runat="server" Class="Form-control" Height="33px" Width="220px" AutoPostBack="true">
                          <asp:ListItem Text="Select..." Value="0"></asp:ListItem>
                          <asp:ListItem Text="4week" Value="01"></asp:ListItem>
                          <asp:ListItem Text="Monthly" Value="02"></asp:ListItem>
                          <asp:ListItem Text="Specified period" Value="03"></asp:ListItem>
                          </asp:DropDownList>
                              </div>
                              
                          </div>


                                     <div class="row col-md-12">
                                  <div class="col-sm-2">
                                    <div class="form-group droplistlocation">
                               <label class="bmd-label-floating"> GM</label> 
                                  </div>
                                   </div>
                              <div class="form-group droplistlocation">
                                     <asp:TextBox ID="txtGM" class="Form-control" runat="server" Width="220px" Text="0.88"></asp:TextBox>
                                 
                              </div>
                              
                          </div>

                              
                           <div class="row col-md-12">
                                  <div class="col-sm-2">
                                   
                                 <label ID="lblMonth" runat="server"  Visible="false"  class="bmd-label-floating" >Month</label>
                                <Label ID="lblDateFrom" runat="server" CssClass="bmd-label-floating"  Visible="false"> Date From (yyyy/mm/dd)</Label>
                    
                                  
                                   </div>
                              <div class=" ">
                                      <asp:DropDownList ID="DropDownMonth" runat="server" CssClass="Form-control" Height="33px" Width="100%" Visible="false">
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
                                  </div>
<div class=" ">
                      <asp:DropDownList ID="DropDownYear" runat="server" CssClass="Form-control" Height="33px" Width="100%" Visible="false">
                          <asp:ListItem Text="2019" Value="2019"></asp:ListItem>
                          <asp:ListItem Text="2020" Value="2020"></asp:ListItem>
                          <asp:ListItem Text="2021" Value="2021"></asp:ListItem>
                          <asp:ListItem Text="2022" Value="2022"></asp:ListItem>
                          <asp:ListItem Text="2023" Value="2023"></asp:ListItem>
                          <asp:ListItem Text="2024" Value="2024"></asp:ListItem>
                          <asp:ListItem Text="2025" Value="2025"></asp:ListItem>
                      </asp:DropDownList>
                              </div>
                                 
                               
                                           
                                <div class="form-group row">
 
                                    <div>
                        <asp:TextBox ID="TextDateFrom" runat="server" CssClass="serverlbl date form-control" AutoCompleteType="Disabled" Visible="false" Width="100px"></asp:TextBox>&nbsp;
                      <%--<ajaxToolkit:CalendarExtender ID="TextDateFrom_CalendarExtender" runat="server" BehaviorID="TextDateFrom_CalendarExtender" TargetControlID="TextDateFrom" PopupPosition="Left" Format="yyyy/MM/dd"></ajaxToolkit:CalendarExtender>--%>
                       </div>
                                    <div>
                                        <Label ID="lblDateTo" runat="server" CssClass="bmd-label-floating"  Visible="false">To</Label>
                                        </div>
                                    <div>
                        <asp:TextBox ID="TextDateTo" runat="server" CssClass="serverlbl date form-control" AutoCompleteType="Disabled"  Visible="false" Width="100px"></asp:TextBox>
                        <%--<ajaxToolkit:CalendarExtender ID="TextDateTo_CalendarExtender" runat="server" BehaviorID="TextDateTo_CalendarExtender" TargetControlID="TextDateTo" PopupPosition="right" Format="yyyy/MM/dd"></ajaxToolkit:CalendarExtender>--%>
                       </div>
                                </div>
                              
                          </div>


                   
                              <div class="col-sm-12">
                               <asp:Button ID="btnSend" runat="server" Text="Send" CssClass="btn btn-primary pull-right" />
                 </div>
                          </div>

                           <%--************End Stored Management **************--%>

                           <%--************Start Creditinfo management **************--%>

                        <div runat="server" id="Credit">
                          <div class=" ">
                              <div class="form-group col-md-12">
                                    <asp:GridView ID="CreitInfo" runat="server" Class="form-group" HeaderStyle-BackColor="#8e24aa" CssClass="col-sm-6" HeaderStyle-ForeColor="White"  AutoGenerateColumns="false"   >
            <Columns>
               <asp:BoundField  DataField="BRANCH_NO" HeaderText="Service center" />
                <asp:BoundField DataField="CREDIT_LIMIT" HeaderText="Credit Limit" />
                <asp:BoundField DataField="PER_DAY" HeaderText=" Consumption Per Day"/>   
            </Columns>            
        </asp:GridView>
                              </div>
                              
                                     <div class="col-sm-12">
                                  <br />
              <label style="font-weight: bold" id="LblINFO" class="bmd-label-floating" runat="server">CREDIT VALUE CHANGE</label>
          <br />
                              </div>
                              <div  class="row col-sm-6 ">
                   <div class="col-sm-5">
                        <Label ID="Label9" Class="bmd-label-floating" runat="server" >Select Service Center</Label>
                       </div>
                                   <div>
                         <asp:DropDownList ID="DropdownList1" AutoPostBack="true" runat="server" Class="Form-control" Height="33px" Width="173px">
                         </asp:DropDownList>
                    </div>
                    
                </div>
                       

                              
                               
                     <div class="row col-sm-12">
                            <div class=" col-sm-6">
                                       <Label ID="Label33" Class="bmd-label-floating" runat="server" style="font-weight:bold" >CREDIT LIMIT</Label>
                                  </div>
                         <div >
                                       <Label ID="Label51" Class="bmd-label-floating" runat="server" style="font-weight:bold" >CONSUMPTION PER DAY</Label>
                                  </div>
                     </div>

                    <div class="row col-sm-6">
                        <div class="col-sm-5">
                        <Label ID="Label11" Class="bmd-label-floating" runat="server" >Current Value</Label>
                       </div>
                        <div >
                                <asp:TextBox ID="TextBox5" ReadOnly="true" class="form-control" style="width: 92%;" runat="server"></asp:TextBox>
                         </div>
                    </div>
                    
                    <div class="row col-sm-6">
                        <div class="col-sm-5">
                                <Label ID="Label15" Class="bmd-label-floating" runat="server" >Current Value</Label>
                         </div>
                        <div >
                                <asp:TextBox ID="TextBox6" Class="Form-control" style="width: 92%;" ReadOnly="true" runat="server"></asp:TextBox>
                    </div>
                        </div>
              
               
  <div class="row col-sm-6">
                        <div class="col-sm-5 ">
                            <br />
                        <Label ID="Label12" Class="bmd-label-floating" runat="server" >New Value</Label>
                        </div>
                         <div  >
                             <br />
                                <asp:TextBox ID="TextBox3" Class="form-control" style="width: 92%;" runat="server"></asp:TextBox>
                        </div>
                    </div>
  
                    <div class="row col-sm-6">
                         <div class="col-sm-5">
                               <br />
                                <Label ID="Label16" Class="bmd-label-floating" runat="server" >New Value</Label>
                       </div>
                        <div>
                           
                             <br />
                                <asp:TextBox ID="TextBox4" style="width: 92%;" Class="form-control" runat="server"></asp:TextBox>
                                </div>
                        </div>
                 
                      
               
   
                              
                <div class="col-sm-12">  
                    <asp:Button ID="ImageButton2" runat="server" text="Send"  OnClick="ImageButton2_Click" Class="btn btn-primary pull-right"   />           
               <br />
                            <br />
                </div>
                            </div>
                            
                             </div>
                     
                  <%--************End Creditinfo management **************--%>


                 <%--************Start General Info management**************--%>

<div  runat="server" id="Div1">
                          <div>

                               <div id="Table1" class="form-group col-sm-12" runat="server">
               
                 
                    <div >
                        <asp:GridView ID="GridView2" runat="server" HeaderStyle-BackColor="#8e24aa" HeaderStyle-ForeColor="White"  CssClass="col-sm-6"  AutoGenerateColumns="false"  >
            <Columns>
               <asp:BoundField  DataField="ship_Name" HeaderText="Service center" />
                <asp:BoundField DataField="GSTIN" HeaderText="GSTIN" />                 
            </Columns>            
           </asp:GridView>
                    </div>

               

            </div>




<div class="col-sm-12">
            <label Class="bmd-label-floating" style="font-weight:bold;" id="P1" runat="server"> GSTIN VALUE CHANGE</label>
    </div>       
    <div id="Table2" runat="server">

                <div class="row col-sm-12 ">
                 <div class="col-sm-3">
                        <Label ID="Label41" Class="bmd-label-floating" runat="server" >Select Service Center</Label>
                       </div>
                    <div >
                         <asp:DropDownList ID="DropDownList2" AutoPostBack="true" CssClass="form-control"  runat="server" OnSelectedIndexChanged="DropDownList2_SelectedIndexChanged"  Height="33px" Width="280%">
                         </asp:DropDownList>
                    </div>
                </div>

                <div class="col-sm-12">
                    
                    <div class="col-s-12">
                        <Label ID="Label42" runat="server" Class="bmd-label-floating"  Style="font-weight: bold">GSTIN</Label>
                    </div>
                  

                </div>
                <div class="col-sm-12 row ">
            
                    <div class="col-sm-3 ">
                        <Label ID="Label63" Class="bmd-label-floating"  runat="server" >Current Value</Label>
                      </div>
                    <div >
                                <asp:TextBox ID="TextBox1" CssClass="form-control" Width="116%"  ReadOnly="true" runat="server"></asp:TextBox>
                    </div>
                
                </div>

                <div class="col-sm-12 row">
                
                    <div class="col-sm-3" >
                        <br />
                        <Label ID="Label65" Class="bmd-label-floating" runat="server">New Value</Label>
                       </div>
                    <div >
                          <br />
                                <asp:TextBox ID="TextBox7" CssClass="form-control" Width="116%" runat="server"></asp:TextBox>
                      
                    </div>
                </div>

                <div class="col-sm-12">
                 
                    <div class="col-sm-12">
                     <asp:Button ID="ImageButton3" runat="server" text="Send"  OnClick="ImageButton3_Click" Class="btn btn-primary pull-right"   />           
              
                    </div>
                  
                </div>

            </div>
                              </div>
    </div>
                          
 
           <%--************End General Info management**************--%>
 
 <%--************Start Payment Value**************--%>
           <div id="Table6" runat="server" >
            <div class=" ">
               <div >
                   
                       
                        <div class="row col-12" >
                            <div class="col-sm-2">
                                <br />
                            <Label ID="Label88" runat="server"  Class="bmd-label-floating" >Select SSC</Label>
                       </div>
                            <div>  <br />
                            <asp:DropDownList ID="DropdownList5" runat="server" height="33px" Width="220px" Class="form-control" >
                            </asp:DropDownList>
                           </div>
                        </div>

                        <div class="row col-12">
                             <div class="col-sm-2" >  <br />
                            <Label ID="Label89" runat="server" Class="bmd-label-floating" >Payment Amount</Label>
                      </div>
                            <div>  <br />
                            <asp:TextBox ID="txtPaymentAmount" class="Form-control" Height="33px" Width="220px" runat="server" ></asp:TextBox>
                                
                        </div>
                            </div>
                           
                        <div class="row col-12">
                            <div class="col-sm-2">  <br />
                            <Label ID="Label90" runat="server"  Class="bmd-label-floating" >Target Date</Label>
                     </div>
                            <div>  <br />
                            <asp:TextBox ID="txtTargetDate" runat="server" height="33px" Width="220px" AutoCompleteType="Disabled"  Class="form-control date" ></asp:TextBox>
                            
                          <%--<ajaxToolkit:CalendarExtender ID="CalendarExtender3" runat="server" BehaviorID="TextDateFrom_CalendarExtender" TargetControlID="txtTargetDate" PopupPosition="Left"></ajaxToolkit:CalendarExtender>--%>
                       </div>
                                </div>

                         <div class="col-m-12">
                            <asp:button ID="ImageButton1" runat="server" text="send" OnClick="ImageButton1_Click" Class="btn btn-primary pull-right"  />
                     </div>
                       
                         
                      
                    </div>
                    <div >

             <div id="tblPaymentGrid" runat="server" class="col-sm-12" >
                <div >
                    <br />  <br />  <br />
                        <b>
                            <asp:Label ID="lbltotal" Class="bmd-label-floating serverlbl" Style="font-weight:bold" runat="server"> </asp:Label>
                        </b>
                        <br />
                        <b>
                            <Label ID="lblPagesize" Class="bmd-label-floating " runat="server">Page Size:</Label></b><asp:TextBox ID="txtPageSize" runat="server" CssClass="serverlbl" MaxLength="4" Style="width: 40px" AutoPostBack="true" OnTextChanged="txtPageSize_TextChanged"></asp:TextBox>
                        <asp:Label ID="lblErrorMessage"  Style="color: red;" CssClass="bmd-label-floating" runat="server">Please enter a valid Page Size Range betwwwn 1 to 9999</asp:Label>
                        <asp:GridView ID="gvDisplayPaymentValue" runat="server" AutoGenerateColumns="false" AllowPaging="true" HeaderStyle-Font-Bold="true"
                            AllowSorting="true" OnSorting="OnSorting" OnPageIndexChanging="OnPageIndexChanging"  HeaderStyle-BackColor="#8e24aa" HeaderStyle-ForeColor="White"  CssClass="col-sm-6" 
                            PageSize="10" RowStyle-Wrap="false" ShowHeaderWhenEmpty="true" 
                            DataKeyNames="unq_no" OnRowEditing="OnRowEditing" OnRowCancelingEdit="OnRowCancelingEdit" OnRowUpdating="OnRowUpdating" >

                            <Columns>
                                <asp:TemplateField HeaderText="unq_no" ItemStyle-Width="150" Visible="false">
                                    <ItemTemplate>
                                        <asp:Label ID="lblunq_no1" runat="server" Text='<%# Eval("unq_no") %>'></asp:Label>
                                    </ItemTemplate>
                                    <EditItemTemplate>
                                        <asp:TextBox ID="txtunq_no1" runat="server" Text='<%# Eval("unq_no") %>' Width="140"></asp:TextBox>
                                    </EditItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Created Date" ItemStyle-Width="150" SortExpression="CREATED_DATE">
                                    <ItemTemplate>
                                        <asp:Label ID="lblcreateddate1" runat="server" Enabled="false" Text='<%# Eval("CREATED_DATE") %>' ></asp:Label>
                                    </ItemTemplate>
                                    <EditItemTemplate>
                                        <asp:TextBox ID="txtcreateddate1" runat="server" Enabled="false" Text='<%# Eval("CREATED_DATE") %>' Width="140"></asp:TextBox>
                                    </EditItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Target SSC" ItemStyle-Width="150" SortExpression="TargetSSC">
                                    <ItemTemplate>
                                        <asp:Label ID="lbltargetssc1" runat="server"   Text='<%# Eval("TargetSSC") %>'></asp:Label>
                                    </ItemTemplate>
                                    <EditItemTemplate>
                                        <%--<asp:TextBox ID="txttargetssc1" runat="server"  Text='<%# Eval("TargetSSC") %>' Width="140"></asp:TextBox>--%>
                                       <asp:HiddenField ID="HiddenField1" runat="server" 
            Value='<%# Eval("TargetSSC") %>' />
                                        <asp:DropDownList ID="drpdowntargetssc" runat="server">  
                                        </asp:DropDownList>  
                                    </EditItemTemplate>
                                </asp:TemplateField>


                                <asp:TemplateField HeaderText="Payment Amount" ItemStyle-Width="150" SortExpression="PaymentAmount">
                                    <ItemTemplate>
                                        <asp:Label ID="lblpaymentamount1" runat="server" Text='<%# Eval("PaymentAmount") %>'></asp:Label>
                                    </ItemTemplate>
                                    <EditItemTemplate>
                                        <asp:TextBox ID="txtpaymentamount1" runat="server" Text='<%# Eval("PaymentAmount") %>' Width="140"></asp:TextBox>
                                    </EditItemTemplate>
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Target Date" ItemStyle-Width="150" SortExpression="Target_Date">
                                    <ItemTemplate>
                                        <asp:Label ID="lbltargetdate1" runat="server" Text='<%# Eval("Target_Date") %>'></asp:Label>
                                    </ItemTemplate>
                                    <EditItemTemplate>
                                        <asp:TextBox ID="txttargetdate1" runat="server" Text='<%# Eval("Target_Date") %>' Width="140" ClientIDMode="AutoID"></asp:TextBox>
                                        <ajaxToolkit:CalendarExtender ID="CalendarExtendertxttargetdate1" runat="server" BehaviorID="txttargetdate1_CalendarExtender" TargetControlID="txttargetdate1" Format="yyyy/MM/dd"></ajaxToolkit:CalendarExtender>
                                     
                                    </EditItemTemplate>
                                </asp:TemplateField>
                                <asp:CommandField ButtonType="Link" ShowEditButton="true"
                                    ItemStyle-Width="150" />
                            </Columns>




                            <EmptyDataTemplate>No Record Available</EmptyDataTemplate>
                            <EmptyDataRowStyle HorizontalAlign="Center" />
                            
                            
                           
                        </asp:GridView>
                    <br />
                       <br />
                       <br />
                </div>
            </div>
          </div>
               
                   
                </div>
               
               <div class="col-sm-12">
                   </div>
               </div>

 <%--************End Payment Value**************--%>
                          
 <%--************start Table Collection**************--%>
  <div id="TableCollections" runat="server" class="">

            <div  class="">
              
                   
                    <div class="row col-sm-12">
                        <div class="col-sm-2">
                        <br />
                        <Label ID="Label13" runat="server" Class="bmd-label-floating">Select SSC</Label>
                       </div>
                  <div>
                        <br />
                        <asp:DropDownList ID="DropdownList6" runat="server" height="33px" Width="220px" Class="Form-control" >
                        </asp:DropDownList>
                          </div>
                    </div>
                
                

                    <div class="row col-sm-12">
                        <div class="col-sm-2">
                              <br />
                        <Label ID="Label18" runat="server" Class="bmd-label-floating" >Target Date</Label>
                 </div>
                        <div>
                              <br />
                        <asp:TextBox ID="TextBoxTargetDate" runat="server" height="33px" Width="220px" AutoCompleteType="Disabled" Class="Form-control date" ></asp:TextBox>
                          
                            <%--<ajaxToolkit:CalendarExtender ID="CalendarExtender4" runat="server" BehaviorID="TextDateFrom_CalendarExtender" TargetControlID="TextBoxTargetDate" PopupPosition="Left"></ajaxToolkit:CalendarExtender>--%>
                        </div>
                    </div>
                    
               
                    
                    <div class="row col-sm-12">
                        <div class="col-sm-2">
                              <br />
                        <Label ID="Label17" runat="server" Class="bmd-label-floating">Daily Deposit</Label>
                        </div>
                        <div>
                              <br />
                        <asp:TextBox ID="TextBoxDeposit" Class="Form-control" height="33px" Width="220px" runat="server"></asp:TextBox>
                         </div>
                    </div>
                   
                
                <div class="row col-sm-12">
                    <div class="col-sm-2">
                     <br />
                        <Label ID="Label19" runat="server" Class="bmd-label-floating" >Credit Sales</Label>
                       </div>
                    <div>
                          <br />
                        <asp:TextBox ID="TextBoxCreditSales" height="33px" Width="220px" Class="Form-control" runat="server"></asp:TextBox>
                       </div>
                   
                </div>
                <div class="col-sm-12 ">
                    <asp:button ID="ImageButton5" runat="server" Text="Send"  OnClick="ImageButton5_Click" Class="btn btn-primary pull-right" />
                </div>
            </div>
      </div>
       
 <%--************End Table Collection**************--%>

 <%--************Start count DRS**************--%>
<p style="font-weight: bold" id="P2" runat="server">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &nbsp;&nbsp; COUNT DRS</p>
        
  <div runat="server" class="" id="Table3" >
                   
                        <div class="row col-sm-12">
                        <div class="col-sm-2" >
                            <br />
                            <Label ID="Label105"  runat="server" Class="bmd-label-floating" >Target Store</Label>
                           </div>
                            <div>
                                <br />
                            <asp:DropDownList ID="DropdownList3" runat="server" height="33px" Width="220px" Class=" form-control" >
                            </asp:DropDownList>
                               
                        </div>
            
                      
                    </div>
                  
                        <div class="row col-sm-12">
                              <div class="col-sm-2">
                                  <br />
                                  <label Class="bmd-label-floating"> From Date
                                      </label>
                            </div>
      <div><br />
                      <asp:TextBox ID="TextFromDate" runat="server" Class="form-control date" AutoCompleteType="Disabled" height="33px" Width="220px" ></asp:TextBox>
                            <%--<ajaxToolkit:CalendarExtender ID="CalendarExtender1" runat="server" BehaviorID="TextDateFrom_CalendarExtender" TargetControlID="TextFromDate" PopupPosition="Left"></ajaxToolkit:CalendarExtender>--%>
                        </div>
       <div >
           <br />
           <label Class="bmd-label-floating">&nbsp;&nbsp;&nbsp; to &nbsp;&nbsp;&nbsp;</label>
                            </div>
                       <div class="" >
                           <br />
                       
                      <asp:TextBox ID="TextToDate" runat="server" CssClass="form-control date" AutoCompleteType="Disabled" style="width: 178px; height: 33px;" ></asp:TextBox>
                            <%--<ajaxToolkit:CalendarExtender ID="CalendarExtender2" runat="server" BehaviorID="TextDateTo_CalendarExtender" TargetControlID="TextToDate" PopupPosition="Right"></ajaxToolkit:CalendarExtender>--%>
                       </div>
                        <div class="">
                            <br />
                            <label Class="bmd-label-floating">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Previous day: </label>

                        </div>
                       
                        <div class="col-sm-1">
                                 <br />
                            <asp:CheckBox ID="CheckBox1" runat="server"  AutoPostBack="true"   OnCheckedChanged="CheckBox1_CheckedChanged" />                            
                        </div>
                        </div>
                      
                    
      <div class="col-sm-12 row">
        
                    <div class="col-sm-2">
                       <div>  
                           <br />
                            <Label ID="Label106" Class="bmd-label-floating" runat="server"  >Type</Label>
                        </div>
                        </div>
      <div>
          <br />
                             <asp:DropDownList ID="DropDownList4" runat="server" height="33px" Width="220px" Class="form-control">

                                <asp:ListItem Text="Select" Value="00"></asp:ListItem>
                                <asp:ListItem Text="CSV" Value="01"></asp:ListItem>
                                <asp:ListItem Text="Output" Value="02"></asp:ListItem>
                            </asp:DropDownList>
                          </div>
           </div>

      <div class="col-sm-12">
        <asp:Button ID="ImageButton4" runat="server" OnClick="ImageButton4_Click" Text="Send"  Class="btn btn-primary pull-right" />
    </div>

       <div class="table-responsive"  runat="server" id="Table5">
                     
                        <table class="table">
                          <thead class=" text-primary">
                            
                            <tr>
                              <th style="background: transparent;border: none;"></th>
                                <th colspan="2">
                              <asp:Label ID="Label44" runat="server" Text="SSC1"></asp:Label>
                            </th>
                            <th colspan="2">
                             <asp:Label ID="Label64" runat="server" Text="SSC2"></asp:Label>
                            </th>
                            <th colspan="2">
                              <asp:Label ID="Label66" runat="server" Text="SSC3"></asp:Label>
                            </th>
                            <th colspan="2">
                             <asp:Label ID="Label67" runat="server" Text="SSC4"></asp:Label>
                            </th>
                            <th colspan="2">
                              <asp:Label ID="Label68" runat="server" Text="SSC5"></asp:Label>
                            </th>
                            <th colspan="2">
                               <asp:Label ID="Label69" runat="server" Text="SSC6"></asp:Label>
                            </th>
                                 <th colspan="2">
                               <asp:Label ID="Label70" runat="server" Text="SSC7"></asp:Label>
                                 </th>
                                <th colspan="2">
                                     <asp:Label ID="Label71" runat="server" Text="SSC8"></asp:Label>
                                 </th>
                                <th colspan="2">
                                     <asp:Label ID="Label92" runat="server" Text="SSC9"></asp:Label>
                                 </th>
                                <th colspan="2">
                                      <asp:Label ID="Label20" runat="server" Text="SSC10"></asp:Label>
                                 </th>
                                <th colspan="2">
                                     <asp:Label ID="Label31" runat="server" Text="SSC11"></asp:Label>
                                 </th>
                               
                          </tr>

                          </thead>
                          <tbody>
                            <tr>
                            <td style="background: transparent;border: none;"></td>
                              <td><asp:Label ID="Label72" runat="server" Text="IW"></asp:Label></td>
                                <td>
                          <asp:Label ID="Label80" runat="server" Text="OW"></asp:Label>                               
                              </td>
                              <td>
                                <asp:Label ID="Label1" runat="server" Text="IW"></asp:Label>
                            </td>
                                <td>
                                    <asp:Label ID="Label2" runat="server" Text="OW"></asp:Label>
                              </td>
                              <td>
                                <asp:Label ID="Label4" runat="server" Text="IW"></asp:Label>
                           </td>
                                <td>
                                    <asp:Label ID="Label5" runat="server" Text="OW"></asp:Label>
                              </td>
                              <td>
                                <asp:Label ID="Label6" runat="server" Text="IW"></asp:Label>
                          </td>
                                <td>
                                <asp:Label ID="Label7" runat="server" Text="OW"></asp:Label>
                              </td>
                              <td >
                                 <asp:Label ID="Label8" runat="server" Text="IW"></asp:Label>
                            </td>
                                <td>
                                    <asp:Label ID="Label10" runat="server" Text="OW"></asp:Label>
                              </td>
                              <td >
                                 <asp:Label ID="Label14" runat="server" Text="IW"></asp:Label>
                           </td>
                                <td>
                                    <asp:Label ID="Label35" runat="server" Text="OW"></asp:Label>
                              </td>
                           
                              <td>
                                 <asp:Label ID="Label36" runat="server" Text="IW"></asp:Label>
                           </td>
                                <td>
                                    <asp:Label ID="Label37" runat="server" Text="OW"></asp:Label>
                              </td>
                              <td> <asp:Label ID="Label38" runat="server" Text="IW"></asp:Label>
                           </td>
                                <td>
                                    <asp:Label ID="Label39" runat="server" Text="OW"></asp:Label>
                                
                              </td>
                              <td> <asp:Label ID="Label45" runat="server" Text="IW"></asp:Label>
                           </td>
                                <td>
                                    <asp:Label ID="Label46" runat="server" Text="OW"></asp:Label>
                               
                              </td>
                              <td> <asp:Label ID="Label47" runat="server" Text="IW"></asp:Label>
                          </td>
                                <td>
                                    <asp:Label ID="Label48" runat="server" Text="OW"></asp:Label>
                               
                              </td>
                              <td > <asp:Label ID="Label49" runat="server" Text="IW"></asp:Label>
                          </td>
                                <td>
                                    <asp:Label ID="Label50" runat="server" Text="OW"></asp:Label>
                                
                              </td>
                            </tr>
                              <tr>
                                  <td style="background: transparent;border: none;"></td>
                             <td >
                            <asp:Label ID="lblSSC1IW" runat="server" Text=""></asp:Label>
                        </td>
                                  <td>
                             <asp:Label ID="lblSSC1OW" runat="server" Text=""></asp:Label>
                        </td>
                        <td >
                            <asp:Label ID="lblSSC2IW" runat="server" Text=""></asp:Label>
                        </td>
                                  <td>
                             <asp:Label ID="lblSSC2OW" runat="server" Text=""></asp:Label>
                        </td>
                        <td >
                            <asp:Label ID="lblSSC3IW" runat="server" Text=""></asp:Label></td>
                                  <td>
                             <asp:Label ID="lblSSC3OW" runat="server" Text=""></asp:Label>
                        </td>
                        <td>
                            <asp:Label ID="lblSSC4IW" runat="server" Text=""></asp:Label></td>
                                  <td>
                             <asp:Label ID="lblSSC4OW" runat="server" Text=""></asp:Label>
                        </td>
                        <td >
                            <asp:Label ID="lblSSC5IW" runat="server" Text=""></asp:Label></td>
                                  <td>
                             <asp:Label ID="lblSSC5OW" runat="server" Text=""></asp:Label>
                        </td>
                        <td>
                            <asp:Label ID="lblSSC6IW" runat="server" Text=""></asp:Label></td>
                                  <td>
                             <asp:Label ID="lblSSC6OW" runat="server" Text=""></asp:Label>
                        </td>
                        <td >
                            <asp:Label ID="lblSSC7IW" runat="server" Text=""></asp:Label></td>
                                  <td>
                             <asp:Label ID="lblSSC7OW" runat="server" Text=""></asp:Label>
                        </td>
                        <td >
                            <asp:Label ID="lblSSC8IW" runat="server" Text=""></asp:Label></td>
                                  <td>
                             <asp:Label ID="lblSSC8OW" runat="server" Text=""></asp:Label>
                        </td>

                         <td >
                            <asp:Label ID="lblSSC9IW" runat="server" Text=""></asp:Label></td>
                                  <td>
                             <asp:Label ID="lblSSC9OW" runat="server" Text=""></asp:Label>
                        </td>
                         <td>
                            <asp:Label ID="lblSSC10IW" runat="server" Text=""></asp:Label></td>
                                  <td>
                             <asp:Label ID="lblSSC10OW" runat="server" Text=""></asp:Label>
                        </td>
                        <td >
                            <asp:Label ID="lblSSC11IW" runat="server" Text=""></asp:Label></td>
                                  <td>
                             <asp:Label ID="lblSSC11OW" runat="server" Text=""></asp:Label>
                        </td>
                    </tr>
                    <tr>
                       <td style="background: transparent;border: none;"></td>
                        <td colspan="2">
                            <asp:Label ID="lblIO1" runat="server" Text=""></asp:Label>
                        </td>
                        <td colspan="2">
                            <asp:Label ID="lblIO2" runat="server" Text=""></asp:Label>
                        </td>
                        <td colspan="2">
                            <asp:Label ID="lblIO3" runat="server" Text=""></asp:Label>
                        </td>
                        <td colspan="2">
                            <asp:Label ID="lblIO4" runat="server" Text=""></asp:Label>
                        </td>
                        <td colspan="2">
                            <asp:Label ID="lblIO5" runat="server" Text=""></asp:Label>
                        </td>
                        <td colspan="2">
                            <asp:Label ID="lblIO6" runat="server" Text=""></asp:Label>
                        </td>
                        <td colspan="2">
                            <asp:Label ID="lblIO7" runat="server" Text=""></asp:Label>
                        </td>
                        <td colspan="2">
                            <asp:Label ID="lblIO8" runat="server" Text=""></asp:Label>
                        </td>
                         <td colspan="2">
                            <asp:Label ID="lblIO9" runat="server" Text=""></asp:Label>
                        </td>
                        <td colspan="2">
                            <asp:Label ID="lblI10" runat="server" Text=""></asp:Label>
                        </td>
                        <td colspan="2">
                            <asp:Label ID="lblI11" runat="server" Text=""></asp:Label>
                        </td>
                    </tr>
                          </tbody>
                        </table>
                      </div>
        <div class="table-responsive" runat="server" id="Table7">
                     
                        <table class="table">
                          <thead class=" text-primary">
                               <tr>

                      
                        <th  runat="server" id="td1">
                            <asp:Label ID="Label21" runat="server" Text="SSC1"></asp:Label>
                        </th>
                        <th  runat="server" id="td2">
                            <asp:Label ID="Label22" runat="server" Text="SSC2"></asp:Label>
                        </th>
                        <th  runat="server" id="td3">
                            <asp:Label ID="Label23" runat="server" Text="SSC3"></asp:Label>
                        </th>
                        <th  runat="server" id="td4">
                            <asp:Label ID="Label24" runat="server" Text="SSC4"></asp:Label>
                        </th>
                        <th runat="server" id="td5">
                            <asp:Label ID="Label25" runat="server" Text="SSC5"></asp:Label>
                        </th>
                        <th  runat="server" id="td6">
                            <asp:Label ID="Label26" runat="server" Text="SSC6"></asp:Label>
                        </th>
                        <th runat="server" id="td7">
                            <asp:Label ID="Label27" runat="server" Text="SSC7"></asp:Label>
                        </th>
                        <th runat="server" id="td8">
                            <asp:Label ID="Label28" runat="server" Text="SSC8"></asp:Label>
                        </th>
                        <th runat="server" id="td9">
                            <asp:Label ID="Label29" runat="server" Text="SSC9"></asp:Label>
                        </th>
                        <th runat="server" id="td10">
                            <asp:Label ID="Label30" runat="server" Text="SSC10"></asp:Label>
                        </th>
                        <th runat="server" id="td12">
                            <asp:Label ID="Label34" runat="server" Text="SSC11"></asp:Label>
                        </th>
                    </tr>
                               <tr>
                       
                        <td >
                            <asp:Label ID="lblRWTSSC1" Class="bmd-label-floating" runat="server" Text=""></asp:Label>
                        </td>
                        <td >
                            <asp:Label ID="lblRWTSSC2" Class="bmd-label-floating" runat="server" Text=""></asp:Label>
                        </td>
                        <td >
                            <asp:Label ID="lblRWTSSC3" Class="bmd-label-floating" runat="server" Text=""></asp:Label>
                        </td>
                        <td >
                            <asp:Label ID="lblRWTSSC4" Class="bmd-label-floating" runat="server" Text=""></asp:Label>
                        </td>
                        <td>
                            <asp:Label ID="lblRWTSSC5" Class="bmd-label-floating" runat="server" Text=""></asp:Label>
                        </td>
                        <td >
                            <asp:Label ID="lblRWTSSC6" Class="bmd-label-floating" runat="server" Text=""></asp:Label>
                        </td>
                        <td >
                            <asp:Label ID="lblRWTSSC7" Class="bmd-label-floating" runat="server" Text=""></asp:Label>
                        </td>
                        <td >
                            <asp:Label ID="lblRWTSSC8" Class="bmd-label-floating" runat="server" Text=""></asp:Label>
                        </td>
                         <td >
                            <asp:Label ID="lblRWTSSC9" Class="bmd-label-floating" runat="server" Text=""></asp:Label>
                        </td>
                        <td >
                            <asp:Label ID="lblRWTSSC10" Class="bmd-label-floating" runat="server" Text=""></asp:Label>
                        </td>
                        <td >
                            <asp:Label ID="lblRWTSSC11" Class="bmd-label-floating" runat="server" Text=""></asp:Label>
                        </td>
                    </tr>
                              </table>
            </div>
      <div id="tblSingle" runat="server" class="row" >
          <div class="col-sm-4"></div>
         
                  <table class="table"  style="width:400px;  text-align:center">
                          <thead class=" text-primary">
                    <tr>
                       
                        <th colspan="2" style="text-align:center">
                            <asp:Label runat="server"  ID="lblBranchname"></asp:Label>
                        </th>
                    </tr>

                    <tr>
                        <td style="width:200px " ><label Class="bmd-label-floating">IW</label> </td>
                        <td style="width:200px"><label Class="bmd-label-floating">OW</label></td>
                     


                    </tr>
                    <tr>
                      
                        <td  >
                            <asp:Label ID="lblSSCSIW" Class="bmd-label-floating" runat="server" Text="" ></asp:Label>
                           </td>
                        <td><asp:Label ID="lblSSCSOW" Class="bmd-label-floating" runat="server" ></asp:Label>
                           </td>
                        

                    </tr>
                    <tr>
                   
                        <td style="width:200px"><label>TotalCounts </label>
                            </td>
                        <td style="width:200px"><asp:Label runat="server" Class="bmd-label-floating" ID="lblTotalSingle"></asp:Label>
                           </td>
                       
                    </tr>
                </table>
          
          </div>
     
                <br />
          <div id="tblSingleRWT" runat="server" class="row" >

              <div class="col-sm-4"></div>
              <div>
                <table class="table" style="width:400px; ">
   <thead class=" text-primary">
                    <tr>
                        
                        <th style="text-align: center;">
                           <asp:Label runat="server" CssClass="" text="" ID="lblSSCName"></asp:Label>
                        </th>
                    </tr>
       
               </thead>
                    <tbody>
                    <tr>
                        
                        <td  style="text-align: center;"><asp:Label CssClass="bmd-label-floating" runat="server" ID="lblSSCNameRWT"></asp:Label>
                            &nbsp;</td>
                       
                    </tr>
                        </tbody>
                </table>
          </div>
              </div>

                     </div>
             
  <%--************End count DRS**************--%>


                          <%--Enddiv--%>
                         
                </div>
            </div>

    <div>

           </div>
       </div>

                </div>
          
           
           
                <div>
              
             
                   
              </div>
    
                  </div>
        </div>
       
               
            </div>
    
            <div>
               
            </div>
     

            
        

        
      

        <asp:Button ID="BtnCancel" runat="server" Text="Button" Style="display: none;" />
        <asp:Button ID="BtnOK" runat="server" Text="Button" Style="display: none;" />

   

    <div id="dialog" title="message" style="display: none;">
       
        <asp:Label ID="lblMsg" runat="server" Text=""></asp:Label>
    </div>

     <table id="Getdta" runat="server" autopostback="true" >
                <tr>
                    <td >
                    </td>
                </tr>
            </table>

       
                           <div style="visibility:hidden" class="col-sm-12">
                               
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
   
</asp:Content>
