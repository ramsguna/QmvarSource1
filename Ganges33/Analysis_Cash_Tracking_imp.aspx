<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="Analysis_Cash_Tracking_imp.aspx.vb"  MasterPageFile="~/Site4.Master"  Inherits="Ganges33.Analysis_Cash_Tracking_imp" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="ajaxToolkit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
     <script src="http://ajax.googleapis.com/ajax/libs/jquery/1.7.0/jquery.min.js"></script>
    <link type="text/css" href="http://ajax.googleapis.com/ajax/libs/jqueryui/1/themes/start/jquery-ui.css" rel="stylesheet">
    <script type="text/javascript" src="http://ajax.googleapis.com/ajax/libs/jqueryui/1/jquery-ui.min.js"></script>
        <style type="text/css">
        .auto-style6 {
            z-index: 1;
            left: 38px;
            top: 122px;
            position: absolute;
            height: 724px;
            width: 1243px;
            background-size: contain;
            }
            .auto-style7 {
                height: 100%;
                background: rgba(255,255,255,0.8);
                /*opacity: 0.5;*/
            }
            .auto-style135 {
                width: 100%;
                height: 134px;
            }
            .auto-style147 {
                width: 460px;
            }
            .auto-style193 {
                font-family: "Meiryo UI";
                font-size: xx-large;
            }
            .auto-style367 {
                height: 22px;
            }
            .auto-style370 {
                height: 22px;
                width: 156px;
            }
            .auto-style378 {
                vertical-align: top;
            }
            .auto-style379 {
                height: 23px;
            }
            .auto-style380 {
                width: 76px;
            }
            </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="auto-style6" style="background-image: url('pagewall_2/wall_analysis-2.png')">
      <div class="auto-style7">
            <br />

            <table class="auto-style135">
              <tr>
                  <td class="auto-style378" rowspan = "3" >
                      <asp:ImageButton ID="btnMoney" runat="server" Height="108px" ImageUrl="~/icon/money.png" Width="108px" />
                  </td>
                  <td rowspan = "2" class="auto-style380">
                      &nbsp;</td>
                  <td class="auto-style147" rowspan ="2" >
                      <asp:Label ID="Label5" runat="server" CssClass="auto-style193" Text="Daily Cash Tracking Report"></asp:Label>
                  </td>
                  <td class="auto-style370"></td>
                  <td colspan =" 2" class="auto-style367">
                      <asp:Label ID="Label2" runat="server" Text="current username : " CssClass="auto-style186"></asp:Label>
                      <asp:Label ID="lblName" runat="server" CssClass="auto-style186"></asp:Label>
                      <br />
                  </td>
              </tr>
             <tr>
                  <td class="auto-style188"></td>
                  <td class="auto-style189" colspan = "2">
                      <asp:Label ID="Label1" runat="server" Text="current location : " CssClass="auto-style186"></asp:Label>
                      <asp:Label ID="lblLoc" runat="server" CssClass="auto-style186"></asp:Label>
                  </td>
                  <td class="auto-style190"></td>
                  <td class="auto-style191">
                      &nbsp;&nbsp;&nbsp;
                      </td>
                  <td class="auto-style192"></td>
              </tr>
              <tr>
                  <td class="auto-style380"></td>
                  <td class="auto-style189" colspan = "2">
                      <br />
                  </td>
                  <td class="auto-style190"></td>
                  <td class="auto-style191"></td>
                  <td class="auto-style192"></td>
              </tr>
              <tr>
                  <td class="auto-style150"></td>
                  <td class="auto-style151" colspan = "2" rowspan = "2">
                      <asp:Label ID="Label4" runat="server" CssClass="auto-style186" Text="Upload Branch"></asp:Label>
                     
          
                      <asp:DropDownList ID="DropListLocation"  AutoPostBack="true" runat="server" CssClass="auto-style186" Height="33px" Width="303px"  OnSelectedIndexChanged="DropListLocation_SelectedIndexChanged">
                      </asp:DropDownList>
            
                      <br /><br />
                      <asp:Label ID="Label10" runat="server" Text="Invoice Date" CssClass="auto-style186"></asp:Label> 
                      <asp:RadioButtonList runat="server" ID="rbtnDate" RepeatDirection="Horizontal"  RepeatLayout="Flow" CssClass="labels">
                          <asp:ListItem Text="dd/mm/yyyy" Value="DD" Selected="True"></asp:ListItem>
                          <asp:ListItem Text="mm/dd/yyyy" Value="MM"></asp:ListItem>
                        </asp:RadioButtonList> <br />
                      &nbsp;&nbsp;&nbsp
                      <span class="auto-style186">&nbsp;&nbsp;</span>&nbsp;&nbsp;<br /><br />
                      <asp:FileUpload ID="FileUploadAnalysis" runat="server" Height="33px" Width="447px" CssClass="auto-style186"/>
                  </td>
                  <td class="auto-style181"></td>
                  <td class="auto-style168">
                     
                      <asp:ImageButton ID="btnOpen" runat="server" Height="29px" ImageUrl="~/icon/manual-entry.png" Width="150px" CssClass="auto-style186" />
                      <br class="auto-style186" /><br />
                      &nbsp;&nbsp;
                      <br class="auto-style186" /><br />
                      &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp;&nbsp;
                      &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; <br />
                  </td>
                  <td class="auto-style154"></td>
              </tr>
              <tr>
                  <td class="auto-style155"></td>
<%--                  <td  colspan = "2">
                      <asp:Label ID="Label10" runat="server" Text="Invoice Date" CssClass="auto-style186"></asp:Label> &nbsp;&nbsp;&nbsp
                      <asp:RadioButton ID="RadioBtnMM" runat="server" CssClass="auto-style186" AutoPostBack="True" Text="mm/dd/yyyy" /><span class="auto-style186">&nbsp;&nbsp;</span>&nbsp;&nbsp;<asp:RadioButton ID="RadioBtnDD" runat="server" AutoPostBack="True" CssClass="auto-style186" Text="dd/mm/yyyy" />
                  </td>--%>
                  <td class="auto-style182">
                      <asp:ImageButton ID="btnSend" runat="server" Height="29px" ImageUrl="~/icon/send.png" Width="81px" CssClass="auto-style186" />
                  </td>
                  <td align = "right" >
                      &nbsp;</td>
                  <td class="auto-style159"></td>
              </tr>

               <tr>
                  <td class="auto-style161" colspan = "6"> <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
                       <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
                      <asp:GridView ID="gvCashTrack" runat="server" Width="99%" AllowPaging="true"
                PageSize="50" AutoGenerateColumns="False" EmptyDataText="Data Is Empty" BackColor="White"
                BorderColor="Black" OnPageIndexChanging="grvCashTracking_PageIndexChanging" BorderStyle="Double" EnableViewState="true" 
               >
                <Columns>
                      <asp:TemplateField HeaderText="Service Order No">
                        <ItemTemplate>
                             <asp:Label ID="lblServiceOrderNo"  Text='<%# Eval("claim_no")  %>' runat="server"></asp:Label>
                            <asp:HiddenField ID="hidBranch" runat="server" Value='<%# Eval("location") %>'  />
                        </ItemTemplate>
                        <HeaderStyle HorizontalAlign="Center" />
                        <ItemStyle HorizontalAlign="Center" Width="11%" />
                    </asp:TemplateField>
                      <asp:TemplateField HeaderText="Engineer">
                        <ItemTemplate >
                             <asp:Label ID="lblEngineer"  Text='<%# Eval("input_user")  %>' runat="server"></asp:Label>
                        </ItemTemplate>
                        <HeaderStyle HorizontalAlign="Center" />
                        <ItemStyle HorizontalAlign="Left" Width="20%" />
                    </asp:TemplateField>
                         <asp:TemplateField HeaderText="Billing Date">
                        <ItemTemplate>
                             <asp:Label ID="lblGoodsDelivered" Text='<%# Eval("invoice_date")  %>' runat="server"></asp:Label>
                        </ItemTemplate>
                        <HeaderStyle HorizontalAlign="Center" />
                        <ItemStyle HorizontalAlign="Center" Width="10%" />
                    </asp:TemplateField>
                     <asp:TemplateField HeaderText="Invoice Amount">
                         
                        <ItemTemplate>
                             <asp:Label ID="lblCollectAmt"  Text='<%# Eval("total_amount")  %>'  runat="server"></asp:Label>
                        </ItemTemplate>
                        <HeaderStyle HorizontalAlign="Right" />
                        <ItemStyle HorizontalAlign="Right" Width="10%" />
                    </asp:TemplateField>
                           <asp:TemplateField HeaderText="Type ( Cash / Card / Cash & Card )">
                        <ItemTemplate>
                            <table style="width:100%"><tr><td style="width:140px;">
                            <asp:DropDownList ID="drpType1" runat="server" onselectedindexchanged="drpType1_SelectedIndexChanged" AutoPostBack="true" Width="100px">
                                <asp:ListItem >Cash</asp:ListItem>
                                 <asp:ListItem >Card</asp:ListItem>
                                <asp:ListItem >Cash & Card</asp:ListItem>
                            </asp:DropDownList>
                                </td><td>
                            <asp:Label ID="lblCash" runat="server" Text="Cash" Visible="false" ></asp:Label>
                                     </td><td>
                             <asp:TextBox ID="txtCash" runat="server" Width="100px" Visible="false" ></asp:TextBox>
                                          </td><td>
                                    <asp:Label ID="lblCard" runat="server" Text="Card" Visible="false" ></asp:Label>
                                               </td><td>
                             <asp:TextBox ID="txtCard" runat="server" Width="100px" Visible="false" ></asp:TextBox>
                                                    </td>
                                </tr></table>
                        </ItemTemplate>
                        <HeaderStyle HorizontalAlign="Center" />
                        <ItemStyle HorizontalAlign="Center" Width="30%" />
                    </asp:TemplateField>

                  
                     <asp:TemplateField HeaderText="Discount">
                        <ItemTemplate>
                           <asp:TextBox ID="txtDiscount" runat="server"  Width="100px" ></asp:TextBox>
                       </ItemTemplate>
                        <HeaderStyle HorizontalAlign="Center" />
                        <ItemStyle HorizontalAlign="Center" Width="10%" />
                    </asp:TemplateField>
                        <asp:TemplateField HeaderText="Check Only Incomplete">
                        <ItemTemplate>
                           <asp:CheckBox ID="chkIncomplete" runat="server" ></asp:CheckBox>
                       </ItemTemplate>
                        <HeaderStyle HorizontalAlign="Center" />
                        <ItemStyle HorizontalAlign="Center" Width="10%" />
                    </asp:TemplateField>
                </Columns>
               <PagerSettings FirstPageText="" LastPageText="" NextPageText="" 
            PreviousPageText="" Position="Top" >
          </PagerSettings>
                <HeaderStyle BackColor="LightBlue" Font-Bold="True" ForeColor="Black" />
                <PagerStyle HorizontalAlign="Left" />
            </asp:GridView>
              </ContentTemplate>

    </asp:UpdatePanel><br />
                      &nbsp;  &nbsp;  &nbsp;  &nbsp;&nbsp;  &nbsp;  &nbsp;  &nbsp;
                       &nbsp;  &nbsp;  &nbsp;  &nbsp;&nbsp;  &nbsp;  &nbsp;  &nbsp;
                       &nbsp;  &nbsp;  &nbsp;  &nbsp;&nbsp;  &nbsp;  &nbsp;  &nbsp;
                       &nbsp;  &nbsp;  &nbsp;  &nbsp;&nbsp;  &nbsp;  &nbsp;  &nbsp;
                      &nbsp;  &nbsp;  &nbsp;  &nbsp;&nbsp;  &nbsp;  &nbsp;  &nbsp;
                      &nbsp;  &nbsp;  &nbsp;  &nbsp;&nbsp;  
                      
                     
                      <asp:Label ID="lblTotalCount"  runat="server"></asp:Label> 
                      &nbsp;  &nbsp;  &nbsp;  &nbsp;
                      <asp:Label ID="lblInvoiceAmt" runat="server"></asp:Label>
                  </td>
                 
                  
              </tr>
            <tr>
                  <td class="auto-style161" colspan = "6" style="text-align:right">
                    
                                            <asp:ImageButton ID="imgUpdateCashTrack" runat="server" Height="29px" ImageUrl="~/icon/update-cash-tracking.png" Width="150px" CssClass="auto-style186" />

                </td>
            </tr>
<!--                Test begin -->
    
                <tr>
                  <td class="auto-style379" colspan = "2">
                     <asp:Label ID="lblOtherTitle" Text="Other" runat="server"></asp:Label> 
                </td>
           <td class="auto-style379" colspan = "2">
                <asp:Label ID="lblOther"     runat="server"></asp:Label>
                </td>
                    <td class="auto-style379" colspan = "2">
                        </td>
            </tr>
      <tr>
                  <td class="auto-style161" colspan = "2">
                     <asp:Label ID="lblBankDepositTitle" runat="server" Text="Bank Deposit"></asp:Label>
                </td>
           <td class="auto-style161" colspan = "2">
                <asp:Label ID="lblDeposit"     runat="server"></asp:Label>
                </td>
            <td class="auto-style379" colspan = "2">
                        </td>
            </tr>
 <tr>
                  <td class="auto-style161" colspan = "2">
                      <asp:Label ID="lblTodayOpeningCashTitle" runat="server" Text="Today Opening Cash"></asp:Label>
                      
                </td>
           <td class="auto-style161" colspan = "2">
                <asp:Label ID="lblReserve"     runat="server"></asp:Label>
                  </td>
       <td class="auto-style379" colspan = "2">
                        </td>
            </tr>
                <tr>
                  <td class="auto-style161" colspan = "2">
                      <asp:Label ID="lblTotalSalesTitle" runat="server" Text="Total Sales"></asp:Label>
                     
                </td>
           <td class="auto-style161" colspan = "2">
                <asp:Label ID="lblSales"     runat="server"></asp:Label>
                    </td>
                      <td class="auto-style379" colspan = "2">
                        </td>
            </tr>  <!-- Test End -->
              <tr>
                  <td class="auto-style160"></td>
                  <td class="auto-style161" colspan = "3">
                      <asp:Label ID="Label3" runat="server" Text="Message" CssClass="auto-style186"></asp:Label> <br />
                      <asp:ListBox ID="ListMsg" runat="server" Height="130px" Width="555px" CssClass="auto-style186"></asp:ListBox>
                  </td>
                  <td class="auto-style177">
                      <asp:Label ID="Label6" runat="server" Text="History" CssClass="auto-style186"></asp:Label> <br />
                      <asp:ListBox ID="ListHistory" runat="server" Height="130px" Width="560px" CssClass="auto-style186"></asp:ListBox>
                  </td>
                  <td class="auto-style164"></td>
              </tr>
              <tr>
                  <td class="auto-style136">&nbsp;</td>
                  <td class="auto-style380">&nbsp;</td>
                  <td class="auto-style173">&nbsp;</td>
                  <td class="auto-style184">&nbsp;</td>
                  <td class="auto-style185">
                  </td>
                  <td>&nbsp;</td>
              </tr>
          </table>
        
        <asp:Button ID="BtnCancel" runat="server" Text="Button" style="display:none;"/>
        <asp:Button ID="BtnOK" runat="server" Text="Button" style="display:none;" />
      </div>
   </div>

    <div id="dialog" title="message" style="display:none;"> >
        <asp:Label ID="lblMsg" runat="server" Text=""></asp:Label>
    </div>

</asp:Content>
