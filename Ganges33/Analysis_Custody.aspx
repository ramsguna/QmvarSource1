<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Site4.Master" CodeBehind="Analysis_Custody.aspx.vb" Inherits="Ganges33.Analysis_Custody" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script src="http://ajax.googleapis.com/ajax/libs/jquery/1.7.0/jquery.min.js"></script>
    <link href="CSS/Common/Analysis_castody.css" rel="stylesheet" />
    <link type="text/css" href="http://ajax.googleapis.com/ajax/libs/jqueryui/1/themes/start/jquery-ui.css" rel="stylesheet">
    <script type="text/javascript" src="http://ajax.googleapis.com/ajax/libs/jqueryui/1/jquery-ui.min.js"></script>
    </asp:Content>
       <asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="div-entire-tbl" style="background-image: url('pagewall_2/wall_analysis-2.png')">
      <div class="div-analysis_custody-tbl">
     
          <table class="tbl-cash-advance">
              <tr>
                  <td class="td-blank1"></td>
                  <td class="td-btn-img" rowspan = "3">
                      <asp:ImageButton ID="ImgBtnMoney" runat="server" Height="108px" ImageUrl="~/icon/money.png" Width="108px" />
                  </td>
                  <td rowspan = "3" colspan = "2">
                      <asp:Label ID="lblHeader" runat="server" Text="Cash Advance " CssClass="lbl-cash-advance"></asp:Label>
                  </td>       
                    
             
              </tr>

              </table>

               <table class="tbl-customers details" id="tblCustodayAdd" runat="server">
       
              <tr>
                  <td class="td-blank2"></td>
                  <td  class="td-lbl-customers-detail">
                      <asp:Label ID="Label5" runat="server" Text="Customer Name" CssClass="lbl-font-comman"></asp:Label>
                  </td>
                  <td class="td-txtbox-customers-datail" >
                      <asp:TextBox ID="TextCustomerName" runat="server" Height="16px" Width="390px" CssClass="lbl-font-comman"></asp:TextBox>
                  </td>
                  
                  <td class="td-blank3" colspan = "2"></td>
                  <td class="td-blank4" ></td>
              </tr>
              <tr>
                  <td class="td-blank3"></td>
                  <td  class="td-lbl-customers-detail">
                      <asp:Label ID="Label6" runat="server" Text="Customer Tel No." CssClass="lbl-font-comman"></asp:Label>
                  </td>
                  <td class="td-txtbox-customers-detail" >
                      <asp:TextBox ID="TextCustomerTelNo" runat="server" Height="16px" Width="390px" CssClass="lbl-font-comman"></asp:TextBox>
                  </td>
                  <td class="td-blank3" colspan = "2">
                      &nbsp;</td>
                  <td class="td-blank4"></td>
                  <td ></td>
              </tr>
              <tr>
                  <td class="td-blank2"></td>
                  <td  class="td-lbl-customers-detail">
                      <asp:Label ID="Label7" runat="server" Text="Sumsung Claim No." CssClass="lbl-font-comman"></asp:Label>
                  </td>
                  <td class="td-txtbox-customers-detail" >
                      <asp:TextBox ID="TextSumsungClaimNo" runat="server" Height="16px" Width="390px" CssClass="lbl-font-comman"></asp:TextBox>
                  </td>
                  <td class="td-blank3" colspan = "2">
                      </td>
                  <td class="td-blank4"></td>
                  <td ></td>
              </tr>
              <tr>
                  <td class="td-blank2"></td>
                  <td  class="td-lbl-customers-detail">
                      <asp:Label ID="Label8" runat="server" Text="Products Name" CssClass="lbl-font-comman"></asp:Label>
                  </td>
                  <td class="td-txtbox-customers-detail" >
                      <asp:TextBox ID="TextProductsName" runat="server" Height="16px" Width="390px" CssClass="lbl-font-comman"></asp:TextBox>
                  </td>
                  <td class="td-blank3" colspan = "2">
                      </td>
                  <td class="td-blank4"  ></td>
                  <td ></td>
              </tr>
              <tr>
                  <td class="td-blank2"></td>
                  <td class="td-lbl-customers-detail" >
                      <asp:Label ID="Label9" runat="server" Text="Cash" CssClass="lbl-font-comman"></asp:Label>
                  </td>
                  <td class="td-txtbox-customers-detail">
                      <asp:TextBox ID="TextCash" runat="server" Height="16px" Width="390px" CssClass="lbl-font-comman"></asp:TextBox>
                  </td>
                  <td colspan = "2">
                      <asp:Label ID="Label19" runat="server" CssClass="lbl-font-comman" Text="INR"></asp:Label>
                  </td>
                  <td >
                      &nbsp;</td>
                  <td ></td>
              </tr>
              <tr>
                  <td class="td-blank2">&nbsp;</td>
                  <td class="td-lbl-customers-detail" >
                       <asp:Label ID="Label20" runat="server" Text="Advance Ref No." CssClass="lbl-font-comman" ></asp:Label>
                      </td>
                  <td class="td-txtbox-customers-detail">
                      <asp:TextBox ID="txtKeepNo" runat="server" Height="16px" Width="390px" CssClass="lbl-font-comman" ></asp:TextBox>
                  </td>
                  <td colspan = "2">
                      &nbsp;<&nbsp;</td>
                  <td >
                    <asp:ImageButton ID="imgCustodyAdd" runat="server" Height="40px" ImageUrl="~/icon/btnSubmitCashAdvance.png" Width="181px" CssClass="lbl-font-comman" />
                      <br />
                      <asp:ImageButton ID="imgSearch" runat="server" Height="40px" ImageUrl="~/icon/btnsearch.png" Width="100px"  />
                  </td>
                  <td >&nbsp;</td>
              </tr>
          </table>


       <!-- Custody Search -->
 <table class="tblContent1" align ="left" id="tblCustodySearch" runat="server">
       
           <tr>
            <td >
               
            </td>
             <td >
               
            </td>
            <td></td>
    
            <td style="text-align: right;" >
           <asp:ImageButton ID="imgBack" runat="server" Height="40px" ImageUrl="~/icon/btnback.png" Width="100px"  />

            </td>
        </tr>
     

     <tr>
         <td colspan="4"><br /></td>
     </tr>
     <tr>
         <td>
            
         </td>
         <td>
             Advance Ref No
                  </td>
         <td>
              <asp:textbox ID="txtAdvanceRefNo" runat="server" ></asp:textbox>
         </td>
         <td>
           
         </td>
     </tr>
     <tr>
         <td>
            
         </td>
         <td>
             Name
         </td>
         <td>
              <asp:textbox ID="txtName" runat="server" ></asp:textbox>
         </td>
         <td>
           
         </td>
     </tr>
       <tr>
         <td>
            
         </td>
         <td>
             Telephone
         </td>
         <td>
              <asp:textbox ID="txtTelephone" runat="server" ></asp:textbox>
         </td>
         <td>
           
         </td>
     </tr>
     
     <tr>
         <td></td>
         <td></td>
         <td>
              <asp:ImageButton ID="imgCustodySearch" runat="server" Height="40px" ImageUrl="~/icon/btnsearch.png" Width="100px"  />
         </td>
         <td style="text-align: left;"  >  </td>
     </tr>
     <tr>
         <td colspan="4" >
              <asp:GridView ID="gvCustodyInfo" runat="server" Width="99%" AllowPaging="true"
                PageSize="3" AutoGenerateColumns="False" EmptyDataText="Data Is Empty" BackColor="White"
                BorderColor="Black"   OnPageIndexChanging="gvCustodyInfo_PageIndexChanging"   BorderStyle="Double" EnableViewState="true"  >
                <Columns>
                      <asp:TemplateField HeaderText="Take Out">
                        <ItemTemplate>
                               <asp:HiddenField ID="hidTakeOut" runat="server" Value='<%# Eval("takeout") %>' />
                            <asp:CheckBox ID="chkTakeOut" runat="server" />
                          </ItemTemplate>
                        <HeaderStyle HorizontalAlign="Center" />
                        <ItemStyle HorizontalAlign="Center" Width="8%" />
                    </asp:TemplateField>
                      <asp:TemplateField HeaderText="Customer Name">
                        <ItemTemplate >
                             <asp:Label ID="lblCustomerName"  runat="server" Text='<%# Eval("customer_name")  %>' ></asp:Label>
                        </ItemTemplate>
                        <HeaderStyle HorizontalAlign="Center" />
                        <ItemStyle HorizontalAlign="Left" Width="20%" />
                    </asp:TemplateField>
                      <asp:TemplateField HeaderText="Telephone">
                        <ItemTemplate >
                             <asp:Label ID="lblTelephone"  runat="server" Text='<%# Eval("customer_tel")  %>' ></asp:Label>
                        </ItemTemplate>
                        <HeaderStyle HorizontalAlign="Center" />
                        <ItemStyle HorizontalAlign="Left" Width="15%" />
                    </asp:TemplateField>
                        <asp:TemplateField HeaderText="Samsung Claim No">
                        <ItemTemplate >
                             <asp:Label ID="lblSamsungClaimNo"  runat="server" Text='<%# Eval("samsung_claim_no")  %>' ></asp:Label>
                        </ItemTemplate>
                        <HeaderStyle HorizontalAlign="Center" />
                        <ItemStyle HorizontalAlign="Left" Width="15%" />
                    </asp:TemplateField>
                          <asp:TemplateField HeaderText="Product Name">
                        <ItemTemplate >
                             <asp:Label ID="lblProductName"  runat="server" Text='<%# Eval("product_name")  %>' ></asp:Label>
                        </ItemTemplate>
                        <HeaderStyle HorizontalAlign="Center" />
                        <ItemStyle HorizontalAlign="Left" Width="20%" />
                    </asp:TemplateField>
                             <asp:TemplateField HeaderText="Cash">
                        <ItemTemplate >
                             <asp:Label ID="lblCash"  runat="server" Text='<%# Eval("cash", "{0:0.00}")  %>' ></asp:Label>
                        </ItemTemplate>
                        <HeaderStyle HorizontalAlign="Center" />
                        <ItemStyle HorizontalAlign="Left"  Width="9%" />
                    </asp:TemplateField>
                               <asp:TemplateField HeaderText="Advance Ref No.">
                        <ItemTemplate >
                             <asp:Label ID="lblAdvanceRefNo"  runat="server" Text='<%# Eval("keep_no")  %>' ></asp:Label>
                        </ItemTemplate>
                        <HeaderStyle HorizontalAlign="Center" />
                        <ItemStyle HorizontalAlign="Left" Width="25%" />
                    </asp:TemplateField>
                                 <asp:TemplateField HeaderText="In">
                        <ItemTemplate >
                             <asp:Label ID="lblInDate"  runat="server" Text='<%# Eval("CRTDT", "{0:dd/MM/yyyy}")  %>' ></asp:Label>
                        </ItemTemplate>
                        <HeaderStyle HorizontalAlign="Center" />
                        <ItemStyle HorizontalAlign="Left" Width="25%" />
                    </asp:TemplateField>
                               <asp:TemplateField HeaderText="Out">
                        <ItemTemplate >
                             <asp:Label ID="lblOutDate"  runat="server" Text='<%# Eval("UPDDT", "{0:dd/MM/yyyy}")  %>' ></asp:Label>
                        </ItemTemplate>
                        <HeaderStyle HorizontalAlign="Center" />
                        <ItemStyle HorizontalAlign="Left" Width="25%" />
                    </asp:TemplateField>
                </Columns>
               <PagerSettings FirstPageText="" LastPageText="" NextPageText="" 
            PreviousPageText="" Position="Top" >
          </PagerSettings>
                <HeaderStyle BackColor="LightBlue" Font-Bold="True" ForeColor="Black" />
                <PagerStyle HorizontalAlign="Left" />
            </asp:GridView>
         </td>
     </tr>
      <tr>
         <td></td>
         <td></td>
         <td>
             
         </td>
         <td style="text-align: right;"   > <asp:ImageButton ID="imgUpdate" runat="server" Height="40px" ImageUrl="~/icon/btnUpdate.png" Width="100px" Visible="false"  />  </td>
     </tr>
     </table>
    

          

      </div>
    </div>

    <div id="dialog" title="message" style="display:none;"> >
        <asp:Label ID="lblMsg" runat="server" Text=""></asp:Label>
    </div>

</asp:Content>
