<%@ Page Language="vb" AutoEventWireup="false" MasterPageFile="~/Site3.Master"  CodeBehind="inv_corporation_regi.aspx.vb" Inherits="Ganges33.inv_corporation_regi" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="ajaxToolkit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script src="http://ajax.googleapis.com/ajax/libs/jquery/1.7.0/jquery.min.js"></script>   
    <link type="text/css" href="http://ajax.googleapis.com/ajax/libs/jqueryui/1/themes/start/jquery-ui.css" rel="stylesheet">
    <link href="CSS/Common/Inv-corporator_registeration.css" rel="stylesheet" />
    <script type="text/javascript" src="http://ajax.googleapis.com/ajax/libs/jqueryui/1/jquery-ui.min.js"></script>
   
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
      <asp:ScriptManager ID="ScriptManager2" runat="server"></asp:ScriptManager>
    <div class="pgBackGround" style="background-image: url('pagewall_2/wall_inventry-2.png')">
     <div class="divTableHeader">
    
    <table class="tblMenuT" align ="left" id="tblMenu" runat="server">
        <tr>
            <td>
               
            </td>
            <td>
                
            </td>
            <td>
                &nbsp;</td>
        </tr>
          <tr>
            <td  style="text-align:center" class="rowHeader">
               <asp:ImageButton ID="imgCorporateRegistration" runat="server" Height="40px" ImageUrl="~/icon/CorporateRegistration.png" Width="250px"  />
             
            </td>
            <td>
                   <asp:ImageButton ID="imgCashOnSaleAdd" runat="server" Height="40px" ImageUrl="~/icon/btnCashOnSaleAdd.png" Width="250px"  />
            </td>
              <td>
                   <asp:ImageButton ID="imgCreateInvoice" runat="server" Height="40px" ImageUrl="~/icon/btnCreateInvoice.png" Width="250px"  />
            </td>
               
 </tr>
             <tr>
            <td  style="text-align:center" class="rowHeader">
               <asp:ImageButton ID="imgCorporateEdit" runat="server" Height="40px" ImageUrl="~/icon/CorporateEdit.png" Width="250px"  />
            </td>
             <td>
                   <asp:ImageButton ID="imgCashOnSaleEdit" runat="server" Height="40px" ImageUrl="~/icon/btnCashOnSaleEdit.png" Width="250px"  />
            </td>
                 <td>
                   <asp:ImageButton ID="imgConfirmPayment" runat="server" Height="40px" ImageUrl="~/icon/btnConfirmPayment.png" Width="250px"  />
            </td>
 </tr>  
                   <tr>
            <td  style="text-align:center" class="rowHeader">
               <asp:ImageButton ID="imgCorporateInformation" runat="server" Height="40px" ImageUrl="~/icon/CorporateInformation.png" Width="250px"  />
            </td>
                             <td>
            <asp:ImageButton ID="imgCashOnSaleSearch" runat="server" Height="40px" ImageUrl="~/icon/btnCashOnSaleSearch.png" Width="250px"  />
            </td>
 <td>
<asp:ImageButton ID="imgCashOnSaleInvoiceDownload" runat="server" Height="40px" ImageUrl="~/icon/btnDownloadInvoice.png" Width="250px"  />
            </td>
 </tr>  
                   <tr>
            <td  style="text-align:center" class="rowHeader">
               <asp:ImageButton ID="imgCorporateSearch" runat="server" Height="40px" ImageUrl="~/icon/CorporateSearch.png" Width="250px"  />
            </td>
              <td></td>
               <td> <asp:ImageButton ID="imgMultipleSheet" runat="server" Height="40px" ImageUrl="~/icon/btnMultipleSheets.png" Width="250px"  /></td>
 </tr>  

    </table>

<!-- Corporate Registration -->
 <table class="tblContent1" align ="left" id="tblCorporateRegi" runat="server">
        <tr>
            <td>
               
            </td>
             <td >
               
            </td>
            <td ></td>
    
            <td style="text-align:center">
           <asp:ImageButton ID="imgBack1" runat="server" Height="40px" ImageUrl="~/icon/btnback.png" Width="100px"  />

            </td>
        </tr>
      <tr>
          <td>
              <asp:ImageButton ID="btnCorpAddTitle" runat="server" Height="108px" ImageUrl="~/icon/inventory.png" Width="108px" />
          </td>
          <td colspan="3">
              <asp:Label ID="lblTitle1" runat="server" CssClass="title" Text="Corporate Registration"></asp:Label>
           </td>
          
           
          
        </tr>
     <tr>
         <td colspan="4"><br /></td>
     </tr>
      <tr>
         <td>
            </td>
            <td>
                Corporate Name
            </td>
          <td style="text-align:left">
               <asp:TextBox ID="txtCorporateNameAdd" runat="server" />
          </td>
          <td>
         </td>
    </tr>
          <tr>
         <td>
            </td>
            <td>
                Contact Person
            </td>
          <td style="text-align:left">
               <asp:TextBox ID="txtCorporatePersonAdd" runat="server" />
          </td>
          <td>
         </td>
    </tr>
       <tr>
         <td>
            </td>
            <td>
                Address Line1
            </td>
          <td style="text-align:left">
               <asp:TextBox ID="txtAddressLine1Add" runat="server" />
          </td>
          <td>
         </td>
    </tr>
           <tr>
         <td>
            </td>
            <td>
                Address Line2
            </td>
          <td style="text-align:left">
               <asp:TextBox ID="txtAddressLine2Add" runat="server" />
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
          <td style="text-align:left">
               <asp:TextBox ID="txtTelephoneAdd" runat="server" />
          </td>
          <td>
         </td>
    </tr>
            <tr>
         <td>
            </td>
            <td>
                Fax
            </td>
          <td style="text-align:left">
               <asp:TextBox ID="txtFaxAdd" runat="server" />
          </td>
          <td>
         </td>
    </tr>
             <tr>
         <td>
            </td>
            <td>
                Zip
            </td>
          <td style="text-align:left">
               <asp:TextBox ID="txtZipAdd" runat="server" />
          </td>
          <td>
         </td>
    </tr>
             <tr>
         <td>
            </td>
            <td style="vertical-align:top">
                Responsible Branch
            </td>
          <td style="text-align:left">
               
                               
              <asp:ListBox ID="lstListLocationAdd" runat="server" Height="149px" SelectionMode="Multiple" Width="250px">
          
        </asp:ListBox>

          </td>
          <td>
         </td>
    </tr>
                 <tr>
         <td>
            </td>
            <td>
                
            </td>
          <td style="text-align:left">
               <asp:TextBox ID="txtBranch1Add" runat="server" Visible="false" />
          </td>
          <td>
         </td>
    </tr>
                 <tr>
         <td>
            </td>
            <td>
                 Close Date
            </td>
          <td style="text-align:left">
               <asp:TextBox ID="txtClosedDateAdd" runat="server" />
          </td>
          <td>
         </td>
    </tr>
                      <tr>
         <td>
            </td>
            <td>
                 Payment Date
            </td>
          <td style="text-align:left">
               <asp:TextBox ID="txtPaymentAdd" runat="server" />
          </td>
          <td>
         </td>
    </tr>
                     <tr>
         <td>
            </td>
            <td>
                 Email
            </td>
          <td style="text-align:left">
               <asp:TextBox ID="txtEmailAdd" runat="server" />
          </td>
          <td>
         </td>
    </tr>
                          <tr>
         <td>
            </td>
            <td>
                 Bank Name
            </td>
          <td style="text-align:left">
               <asp:TextBox ID="txtBankNameAdd" runat="server" />
          </td>
          <td>
         </td>
    </tr>
                          <tr>
         <td>
            </td>
            <td>
               Casa Account
            </td>
          <td style="text-align:left">
               <asp:TextBox ID="txtAccountAdd" runat="server" />
          </td>
          <td>
         </td>
    </tr>
                        <tr>
         <td>
            </td>
            <td>
               Casa Account Number
            </td>
          <td style="text-align:left">
               <asp:TextBox ID="txtAccountNumberAdd" runat="server" />
          </td>
          <td>
             
                   <asp:dropdownlist runat="server" ID="drpCaSaAdd" RepeatDirection="Horizontal"  RepeatLayout="Flow" CssClass="labels" Width="100px">
                          <asp:ListItem Text="CA" Value="CA" Selected="True"></asp:ListItem>
                          <asp:ListItem Text="SA" Value="SA"></asp:ListItem>
                        </asp:dropdownlist> <br />
         </td>
    </tr>

                          <tr>
         <td>
            </td>
            <td>
             
            </td>
          <td >
              
          </td>
          <td style="text-align:center">
              <asp:ImageButton ID="imgSubmit" runat="server" Height="40px" ImageUrl="~/icon/btnsubmit.png" Width="100px"  />
         </td>
    </tr>
    </table>

<!-- Corporate Edit -->
 <table class="tblContent1" align ="left" id="tblCorporateEdit" runat="server">

      <tr>
            <td >
               
            </td>
             <td >
               
            </td>
            <td ></td>
    
            <td style="text-align:center">
           <asp:ImageButton ID="imgBack2" runat="server" Height="40px" ImageUrl="~/icon/btnback.png" Width="100px"  />

            </td>
        </tr>
      <tr>
          <td>
              <asp:ImageButton ID="btnCorpEditTitle" runat="server" Height="108px" ImageUrl="~/icon/inventory.png" Width="108px" />
          </td>
          <td colspan="3">
             <asp:Label ID="lblTitle2" runat="server" CssClass="title" Text="Corporate Edit"></asp:Label>
           </td>
          
           
          
        </tr>
   
     <tr>
         <td colspan="4"><br /></td>
     </tr>
        <tr>
         <td colspan="4"><asp:label ID="lblInfoMsgEdit" runat="server"></asp:label></td>
     </tr>
     <tr>
         <td></td>
         <td>Corporate No</td><td><asp:TextBox ID="txtCorpNumberEdit" runat="server"></asp:TextBox></td>
         <td><asp:ImageButton ID="imgSearchCorpEdit" runat="server" Height="40px" ImageUrl="~/icon/btnsearch.png" Width="100px"  /></td>
     </tr>
     </table>
<table id="tblCorporateEditResult" runat="server" visible="false"  class="tblContent1" >
    <tr>
            <td width="200px;">
               
            </td>
             <td width="130px;">
               
            </td>
            <td width="200px;"></td>
    
            <td style="text-align:center">
           

            </td>
        </tr>
      <tr>
         <td>
            </td>
            <td>
                Corporate Name
            </td>
          <td style="text-align:left">
               <asp:TextBox ID="txtCorporateNameEdit" runat="server" />
          </td>
          <td>
         </td>
    </tr>
          <tr>
         <td>
            </td>
            <td>
                Contact Person
            </td>
          <td style="text-align:left">
               <asp:TextBox ID="txtCorporatePersonEdit" runat="server" />
          </td>
          <td>
         </td>
    </tr>
       <tr>
         <td>
            </td>
            <td>
                Address Line1
            </td>
          <td style="text-align:left">
               <asp:TextBox ID="txtAddressLine1Edit" runat="server" />
          </td>
          <td>
         </td>
    </tr>
           <tr>
         <td>
            </td>
            <td>
                Address Line2
            </td>
          <td style="text-align:left">
               <asp:TextBox ID="txtAddressLine2Edit" runat="server" />
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
          <td style="text-align:left">
               <asp:TextBox ID="txtTelephoneEdit" runat="server" />
          </td>
          <td>
         </td>
    </tr>
            <tr>
         <td>
            </td>
            <td>
                Fax
            </td>
          <td style="text-align:left">
               <asp:TextBox ID="txtFaxEdit" runat="server" />
          </td>
          <td>
         </td>
    </tr>
             <tr>
         <td>
            </td>
            <td>
                Zip
            </td>
          <td style="text-align:left">
               <asp:TextBox ID="txtZipEdit" runat="server" />
          </td>
          <td>
         </td>
    </tr>
             <tr>
         <td>
            </td>
             <td style="vertical-align:top">
                Responsible Branch
            </td>
          <td style="text-align:left">
                            <asp:ListBox ID="lstListLocationEdit" runat="server" Height="149px" SelectionMode="Multiple" Width="250px">
          
        </asp:ListBox>
          </td>
          <td>
         </td>
    </tr>
                 <tr>
         <td>
            </td>
            <td>
                
            </td>
          <td style="text-align:left">
               <asp:TextBox ID="txtBranch1Edit" runat="server"  Visible="false"/>
          </td>
          <td>
         </td>
    </tr>
                 <tr>
         <td>
            </td>
            <td>
                 Close Date
            </td>
          <td style="text-align:left">
               <asp:TextBox ID="txtClosedDateEdit" runat="server" />
          </td>
          <td>
         </td>
    </tr>
                      <tr>
         <td>
            </td>
            <td>
                 Payment Date
            </td>
          <td style="text-align:left">
               <asp:TextBox ID="txtPaymentEdit" runat="server" />
          </td>
          <td>
         </td>
    </tr>
                     <tr>
         <td>
            </td>
            <td>
                 Email
            </td>
          <td style="text-align:left">
               <asp:TextBox ID="txtEmailEdit" runat="server" />
          </td>
          <td>
         </td>
    </tr>
                          <tr>
         <td>
            </td>
            <td>
                 Bank Name
            </td>
          <td style="text-align:left">
               <asp:TextBox ID="txtBankNameEdit" runat="server" />
          </td>
          <td>
         </td>
    </tr>
                          <tr>
         <td>
            </td>
            <td>
               Casa Account
            </td>
          <td style="text-align:left">
               <asp:TextBox ID="txtAccountEdit" runat="server" />
          </td>
          <td>
         </td>
    </tr>
                        <tr>
         <td>
            </td>
            <td>
               Casa Account Number
            </td>
          <td style="text-align:left">
               <asp:TextBox ID="txtAccountNumberEdit" runat="server" />
          </td>
          <td>
                        <asp:dropdownlist runat="server" ID="drpCaSaEdit" RepeatDirection="Horizontal"  RepeatLayout="Flow" CssClass="labels" Width="100px">
                          <asp:ListItem Text="CA" Value="CA" ></asp:ListItem>
                          <asp:ListItem Text="SA" Value="SA"></asp:ListItem>
                        </asp:dropdownlist>
         </td>
    </tr>

       

    <tr>
         <td>
            </td>
            <td>
             
            </td>
          <td >
              
          </td>
          <td style="text-align:center">
               <asp:ImageButton ID="imgEdit" runat="server" Height="40px" ImageUrl="~/icon/btnupdate.png" Width="100px"  />
         </td>
    </tr>





    </table>

<!-- Corporate Information -->
 <table class="tblContent1" align ="left" id="tblCorporateInformation" runat="server">
       <tr>
            <td width="200px;">
               
            </td>
             <td width="130px;">
               
            </td>
            <td width="200px;"></td>
    
            <td style="text-align:center">
           <asp:ImageButton ID="imgBack3" runat="server" Height="40px" ImageUrl="~/icon/btnback.png" Width="100px"  />

            </td>
        </tr>
      <tr>
          <td>
              <asp:ImageButton ID="btnCorpViewTitle" runat="server" Height="108px" ImageUrl="~/icon/inventory.png" Width="108px" />
          </td>
          <td colspan="3">
               <asp:Label ID="lblTitle3" runat="server" CssClass="title" Text="Corporate Information"></asp:Label>
           </td>
           </tr>
     <tr>
         <td colspan="4"><br /></td>
     </tr>
          <tr>
         <td colspan="4"><asp:label ID="lblInfoMsgView" runat="server"></asp:label></td>
     </tr>
     <tr>
         <td></td>
         <td>Corporate No</td><td><asp:TextBox ID="txtCorpNumberView" runat="server"></asp:TextBox></td>
         <td><asp:ImageButton ID="imgSearchCorpView" runat="server" Height="40px" ImageUrl="~/icon/btnsearch.png" Width="100px"  /></td>
     </tr>

       </table>
<table id="tblCorporateViewResult" runat="server" visible="false"  class="tblContent1" >

     <tr>
            <td width="200px;">
               
            </td>
             <td width="130px;">
               
            </td>
            <td width="200px;"></td>
    
            <td style="text-align:center">
           

            </td>
        </tr>
      <tr>
         <td>
            </td>
            <td>
                Corporate Name
            </td>
          <td style="text-align:left">
               <asp:TextBox ID="txtCorporateNameView" runat="server" Enabled="false" />
          </td>
          <td>
         </td>
    </tr>
          <tr>
         <td>
            </td>
            <td>
                Contact Person
            </td>
          <td style="text-align:left">
               <asp:TextBox ID="txtCorporatePersonView" runat="server"  Enabled="false"/>
          </td>
          <td>
         </td>
    </tr>
       <tr>
         <td>
            </td>
            <td>
                Address Line1
            </td>
          <td style="text-align:left">
               <asp:TextBox ID="txtAddressLine1View" runat="server" Enabled="false" />
          </td>
          <td>
         </td>
    </tr>
           <tr>
         <td>
            </td>
            <td>
                Address Line2
            </td>
          <td style="text-align:left">
               <asp:TextBox ID="txtAddressLine2View" runat="server" Enabled="false" />
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
          <td style="text-align:left">
               <asp:TextBox ID="txtTelephoneView" runat="server" Enabled="false" />
          </td>
          <td>
         </td>
    </tr>
            <tr>
         <td>
            </td>
            <td>
                Fax
            </td>
          <td style="text-align:left">
               <asp:TextBox ID="txtFaxView" runat="server" Enabled="false"/>
          </td>
          <td>
         </td>
    </tr>
             <tr>
         <td>
            </td>
            <td>
                Zip
            </td>
          <td style="text-align:left">
               <asp:TextBox ID="txtZipView" runat="server" Enabled="false"/>
          </td>
          <td>
         </td>
    </tr>
             <tr>
         <td>
            </td>
             <td style="vertical-align:top">
                Responsible Branch
            </td>
          <td style="text-align:left">
                                       <asp:ListBox ID="lstListLocationView" runat="server" Height="149px" SelectionMode="Multiple" Width="250px" Enabled="false">
          
        </asp:ListBox>
          </td>
          <td>
         </td>
    </tr>
                 <tr>
         <td>
            </td>
            <td>
                
            </td>
          <td style="text-align:left">
               <asp:TextBox ID="txtBranch1View" runat="server" Enabled="false" Visible="false" />
          </td>
          <td>
         </td>
    </tr>
                 <tr>
         <td>
            </td>
            <td>
                 Close Date
            </td>
          <td style="text-align:left">
               <asp:TextBox ID="txtClosedDateView" runat="server" Enabled="false" />
          </td>
          <td>
         </td>
    </tr>
                      <tr>
         <td>
            </td>
            <td>
                 Payment Date
            </td>
          <td style="text-align:left">
               <asp:TextBox ID="txtPaymentView" runat="server" Enabled="false" />
          </td>
          <td>
         </td>
    </tr>
                     <tr>
         <td>
            </td>
            <td>
                 Email
            </td>
          <td style="text-align:left">
               <asp:TextBox ID="txtEmailView" runat="server" Enabled="false" />
          </td>
          <td>
         </td>
    </tr>
                          <tr>
         <td>
            </td>
            <td>
                 Bank Name
            </td>
          <td style="text-align:left">
               <asp:TextBox ID="txtBankNameView" runat="server" Enabled="false" />
          </td>
          <td>
         </td>
    </tr>
                          <tr>
         <td>
            </td>
            <td>
               Casa Account
            </td>
          <td style="text-align:left">
               <asp:TextBox ID="txtAccountView" runat="server" Enabled="false" />
          </td>
          <td>
         </td>
    </tr>
                        <tr>
         <td>
            </td>
            <td>
               Casa Account Number
            </td>
          <td style="text-align:left">
               <asp:TextBox ID="txtAccountNumberView" runat="server" Enabled="false" />
          </td>
          <td>
                 <asp:dropdownlist runat="server" ID="drpCaSaView" RepeatDirection="Horizontal"  RepeatLayout="Flow" CssClass="labels" Width="100px" Enabled="false">
                          <asp:ListItem Text="CA" Value="CA" Selected="True"></asp:ListItem>
                          <asp:ListItem Text="SA" Value="SA"></asp:ListItem>
                        </asp:dropdownlist>
         </td>
    </tr>

                          <tr>
         <td>
            </td>
            <td>
             
            </td>
          <td style="text-align:left">
               
          </td>
          <td>
            
         </td>
    </tr>




    </table>
    

<!-- Corporate Search -->
 <table class="tblContent1" align ="left" id="tblCorporateSearch" runat="server">
       
           <tr>
            <td width="200px;">
               
            </td>
             <td width="130px;">
               
            </td>
            <td width="200px;"></td>
    
            <td style="text-align:center">
           <asp:ImageButton ID="imgBack4" runat="server" Height="40px" ImageUrl="~/icon/btnback.png" Width="100px"  />

            </td>
        </tr>
     <tr>
          <td>
              <asp:ImageButton ID="btnCorpSearchTitle" runat="server" Height="108px" ImageUrl="~/icon/inventory.png" Width="108px" />
          </td>
          <td colspan="3">
                <asp:Label ID="lblTitle4" runat="server" CssClass="title" Text="Corporate Search"></asp:Label>
           </td>
          
           
          
        </tr>

     <tr>
         <td colspan="4"><br /></td>
     </tr>
     <tr>
         <td>
             Corporate Name
         </td>
         <td>
             <asp:textbox ID="txtCorporateNameSearch" runat="server" ></asp:textbox>
         </td>
         <td>
             Email
         </td>
         <td>
             <asp:TextBox ID="txtEmailSearch" runat="server" ></asp:TextBox>
         </td>
     </tr>

         <tr>
         <td>
             Corporate Person
         </td>
         <td>
             <asp:textbox ID="txtPersonSearch" runat="server" ></asp:textbox>
         </td>
         <td>
             Telephone
         </td>
         <td>
             <asp:TextBox ID="txtPhoneSearch" runat="server" ></asp:TextBox>
         </td>
     </tr>
         <tr>
         <td>
             Zip Code
         </td>
         <td>
             <asp:textbox ID="txtZipCodeSarch" runat="server" ></asp:textbox>
         </td>
         <td>
            Registration From (yyyy/mm/dd)
         </td>
         <td>
             <asp:TextBox ID="txtMonthFromSearch" runat="server" ></asp:TextBox>
              <ajaxToolkit:CalendarExtender ID="txtMonthFromSearchCalendar" Format="yyyy/MM/dd" runat="server" BehaviorID="txtMonthFromSearchCalendar" TargetControlID="txtMonthFromSearch" PopupPosition="Left">
                    </ajaxToolkit:CalendarExtender>
             ~ To 
              <asp:TextBox ID="txtMonthToSearch" runat="server" ></asp:TextBox>
               <ajaxToolkit:CalendarExtender ID="txtMonthToSearchCalendar" Format="yyyy/MM/dd" runat="server" BehaviorID="txtMonthToSearchCalendar" TargetControlID="txtMonthToSearch" PopupPosition="Left">
                    </ajaxToolkit:CalendarExtender>
         </td>
     </tr>
     <tr>
         <td></td>
         <td></td><td></td>
         <td style="text-align:center">   <asp:ImageButton ID="imgSearch" runat="server" Height="40px" ImageUrl="~/icon/btnsearch.png" Width="100px"  /></td>
     </tr>




     <tr>
         <td colspan="4" >
              <asp:GridView ID="gvCorpInfo" runat="server" Width="99%" AllowPaging="true"
                PageSize="3" AutoGenerateColumns="False" EmptyDataText="Data Is Empty" BackColor="White"
                BorderColor="Black" OnPageIndexChanging="gvCorpInfo_PageIndexChanging"   OnRowCreated="gvCorpInfo_RowCreated" BorderStyle="Double" EnableViewState="true" 
               >
                <Columns>
                      <asp:TemplateField HeaderText="Corp No">
                        <ItemTemplate>
                            
                            <asp:LinkButton ID="lnkCorporateNo" runat="server" Width="75px" Text='<%# Eval("corp_number")  %>'></asp:LinkButton>
                        </ItemTemplate>
                        <HeaderStyle HorizontalAlign="Center" />
                        <ItemStyle HorizontalAlign="Center" Width="11%" />
                    </asp:TemplateField>
                      <asp:TemplateField HeaderText="Corporate Name">
                        <ItemTemplate >
                             <asp:Label ID="lblCorporateName"  runat="server" Text='<%# Eval("corp_name")  %>' ></asp:Label>
                        </ItemTemplate>
                        <HeaderStyle HorizontalAlign="Center" />
                        <ItemStyle HorizontalAlign="Left" Width="20%" />
                    </asp:TemplateField>
                         <asp:TemplateField HeaderText="Email">
                        <ItemTemplate>
                             <asp:Label ID="lblEmail"  runat="server" Text='<%# Eval("corp_email")  %>' ></asp:Label>
                        </ItemTemplate>
                        <HeaderStyle HorizontalAlign="Center" />
                        <ItemStyle HorizontalAlign="Center" Width="10%" />
                    </asp:TemplateField>
                     <asp:TemplateField HeaderText="Telephone"  >
                         
                        <ItemTemplate>
                            <asp:Label ID="lblTelephone"  runat="server" Text='<%# Eval("corp_tel")  %>'></asp:Label>
                        </ItemTemplate>
                        <HeaderStyle HorizontalAlign="Right" />
                        <ItemStyle HorizontalAlign="Right" Width="10%" />
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
     </table>
     <table id="tblSearchView" runat="server" visible="false" class="tblContent1" >
         <tr>
            <td width="200px;">
               
            </td>
             <td width="130px;">
               
            </td>
            <td width="200px;"></td>
    
            <td style="text-align:center">
           

            </td>
        </tr>
            <tr>
          <td>
             
          </td>
          <td colspan="3">
                <asp:Label ID="lblTitle5" runat="server" Text="<b>Corporate Information:</b>"></asp:Label>
           </td>
        </tr>
           <tr>
         <td>
            </td>
            <td>
                Corporate Name
            </td>
          <td style="text-align:left">
               <asp:TextBox ID="txtCorporateNameSearchView" runat="server" Enabled="false" />
          </td>
          <td>
         </td>
    </tr>
          <tr>
         <td>
            </td>
            <td>
                Contact Person
            </td>
          <td style="text-align:left">
               <asp:TextBox ID="txtCorporatePersonSearchView" runat="server"  Enabled="false"/>
          </td>
          <td>
         </td>
    </tr>
       <tr>
         <td>
            </td>
            <td>
                Address Line1
            </td>
          <td style="text-align:left">
               <asp:TextBox ID="txtAddressLine1SearchView" runat="server" Enabled="false" />
          </td>
          <td>
         </td>
    </tr>
           <tr>
         <td>
            </td>
            <td>
                Address Line2
            </td>
          <td style="text-align:left">
               <asp:TextBox ID="txtAddressLine2SearchView" runat="server" Enabled="false" />
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
          <td style="text-align:left">
               <asp:TextBox ID="txtTelephoneSearchView" runat="server" Enabled="false" />
          </td>
          <td>
         </td>
    </tr>
            <tr>
         <td>
            </td>
            <td>
                Fax
            </td>
          <td style="text-align:left">
               <asp:TextBox ID="txtFaxSearchView" runat="server" Enabled="false"/>
          </td>
          <td>
         </td>
    </tr>
             <tr>
         <td>
            </td>
            <td>
                Zip
            </td>
          <td style="text-align:left">
               <asp:TextBox ID="txtZipSearchView" runat="server" Enabled="false"/>
          </td>
          <td>
         </td>
    </tr>
             <tr>
         <td>
            </td>
            <td>
                Responsible Branch
            </td>
          <td style="text-align:left">
                                       <asp:ListBox ID="lstListLocationSearchView" runat="server" Height="149px" SelectionMode="Multiple" Width="250px" Enabled="false">
          
        </asp:ListBox>
          </td>
          <td>
         </td>
    </tr>
                 <tr>
         <td>
            </td>
            <td>
                
            </td>
          <td style="text-align:left">
               <asp:TextBox ID="txtBranch1SearchView" runat="server" Enabled="false" Visible="false" />
          </td>
          <td>
         </td>
    </tr>
                 <tr>
         <td>
            </td>
            <td>
                 Close Date
            </td>
          <td style="text-align:left">
               <asp:TextBox ID="txtClosedDateSearchView" runat="server" Enabled="false" />
          </td>
          <td>
         </td>
    </tr>
                      <tr>
         <td>
            </td>
            <td>
                 Payment Date
            </td>
          <td style="text-align:left">
               <asp:TextBox ID="txtPaymentSearchView" runat="server" Enabled="false" />
          </td>
          <td>
         </td>
    </tr>
                     <tr>
         <td>
            </td>
            <td>
                 Email
            </td>
          <td style="text-align:left">
               <asp:TextBox ID="txtEmailSearchView" runat="server" Enabled="false" />
          </td>
          <td>
         </td>
    </tr>
                          <tr>
         <td>
            </td>
            <td>
                 Bank Name
            </td>
          <td style="text-align:left">
               <asp:TextBox ID="txtBankNameSearchView" runat="server" Enabled="false" />
          </td>
          <td>
         </td>
    </tr>
                          <tr>
         <td>
            </td>
            <td>
               Casa Account
            </td>
          <td style="text-align:left">
               <asp:TextBox ID="txtAccountSearchView" runat="server" Enabled="false" />
          </td>
          <td>
         </td>
    </tr>
                        <tr>
         <td>
            </td>
            <td>
               Casa Account Number
            </td>
          <td style="text-align:left">
               <asp:TextBox ID="txtAccountNumberSearchView" runat="server" Enabled="false" />
          </td>
          <td>
                 <asp:dropdownlist runat="server" ID="drpCaSaSearchView" RepeatDirection="Horizontal"  RepeatLayout="Flow" CssClass="labels" Width="100px" Enabled="false">
                          <asp:ListItem Text="CA" Value="CA" Selected="True"></asp:ListItem>
                          <asp:ListItem Text="SA" Value="SA"></asp:ListItem>
                        </asp:dropdownlist>
         </td>
    </tr>

     </table>



<!-- Cash On Sale Add-->
 <table class="tblContent1" align ="left" id="tblCashOnSaleAdd" runat="server">
       
           <tr>
            <td width="200px;">
               
            </td>
             <td width="130px;">
               
            </td>
            <td width="200px;"></td>
    
            <td style="text-align:center">
           <asp:ImageButton ID="imgBack5" runat="server" Height="40px" ImageUrl="~/icon/btnback.png" Width="100px"  />

            </td>
        </tr>
     <tr>
          <td>
              <asp:ImageButton ID="btnCashOnSaleTitle" runat="server" Height="108px" ImageUrl="~/icon/inventory.png" Width="108px" />
          </td>
          <td colspan="3">
                <asp:Label ID="lblTitle6" runat="server" CssClass="title" Text="Cash On Sale"></asp:Label>
           </td>
         
        </tr>

     <tr>
         <td colspan="4"><br /></td>
     </tr>
     <tr>
        <td style="vertical-align:top;text-align:right">
             Corporate 
         </td>
         <td style="vertical-align:top">
              <asp:dropdownlist runat="server" ID="drpCashOnSaleCorp" RepeatDirection="Horizontal"  RepeatLayout="Flow" CssClass="labels" Width="100px">
              </asp:dropdownlist>
         </td>
         <td style="vertical-align:top;text-align:right">
             ASC Claim No<br /> (Service Order No)
         </td>
         <td>
                <asp:ListBox ID="lstClaimNo" runat="server" Height="149px" SelectionMode="Multiple" Width="250px" >
          
        </asp:ListBox>
         </td>
     </tr>

 
 <tr>
         <td></td>
         <td></td><td></td>
         <td style="text-align:center">   <asp:ImageButton ID="imgCashOnSaleAssign" runat="server" Height="40px" ImageUrl="~/icon/btnassign.png" Width="100px"  /></td>
     </tr>
 </table>


<!-- Cash On Sale Edit-->
<table class="tblContent1" align ="left" id="tblCashOnSaleEdit" runat="server">

      <tr>
            <td width="200px;">
               
            </td>
             <td width="130px;">
               
            </td>
            <td width="200px;"></td>
    
            <td style="text-align:center">
           <asp:ImageButton ID="imgBack6" runat="server" Height="40px" ImageUrl="~/icon/btnback.png" Width="100px"  />

            </td>
        </tr>
      <tr>
          <td>
              <asp:ImageButton ID="btnCashOnSaleEditTitle" runat="server" Height="108px" ImageUrl="~/icon/inventory.png" Width="108px" />
          </td>
          <td colspan="3">
             <asp:Label ID="lblTitle7" runat="server" CssClass="title" Text="Cash On Sale Edit"></asp:Label>
           </td>
          
           
          
        </tr>
   
     <tr>
         <td colspan="4"><br /></td>
     </tr>
        <tr>
         <td colspan="4"></td>
     </tr>
     <tr>
         <td></td>
         <td>Claim No (ASC / Service Order No)</td><td><asp:TextBox ID="txtClaimNoSearch" runat="server"></asp:TextBox></td>
         <td><asp:ImageButton ID="imgSearchCashOnSaleEdit" runat="server" Height="40px" ImageUrl="~/icon/btnsearch.png" Width="100px"  /></td>
     </tr>
     </table>

 <table id="tblCashOnSaleEditView" runat="server" visible="false" class="tblContent1" >
         <tr>
            <td width="200px;">
               
            </td>
             <td width="130px;" style="text-align:left">
               
            </td>
            <td width="200px;" ></td>
    
            <td style="text-align:center">
           

            </td>
        </tr>
     <tr>
        <td>
           
         </td>
         <td>  ASC Claim No
             
          </td>
         <td><asp:Label ID="lblAscClaimNoCashOnSaleEdit" runat="server" ></asp:Label>
           </td>
         <td>
          </td>
     </tr>

<tr>
        <td>
             
         </td>
         <td>Corporate No
             
           </td>
         <td style="text-align:left">
           <asp:Label ID="lblCorpNoCashOnSaleEdit" runat="server" ></asp:Label>
         </td>
         <td>
           
         </td>
     </tr>
<tr>
        <td>
            
         </td>
         <td>
              Corporate Name
              
         </td>
         <td>
           <asp:Label ID="lblCorpNameCashOnSaleEdit" runat="server"></asp:Label>
         </td>
         <td>
           
         </td>
     </tr>
<tr>
        <td>
             
         </td>
         <td>Cash Collected
            
             
         </td>
         <td>
            <asp:Label ID="lblCashCollectedCashOnSaleEdit" runat="server" ></asp:Label>
         </td>
         <td>
           
         </td>
     </tr>
     <tr>
            <td width="200px;">
               <br /><br />
            </td>
             <td width="130px;">
               
            </td>
            <td width="200px;"></td>
    
            <td style="text-align:center">
           

            </td>
        </tr>

     <tr>
        <td style="vertical-align:top;text-align:right">
            
         </td>
         <td style="vertical-align:top"> Corporate 
              <asp:dropdownlist runat="server" ID="drpCashOnSaleCorpEdit" RepeatDirection="Horizontal"  RepeatLayout="Flow" CssClass="labels" Width="100px">
              </asp:dropdownlist>
         </td>
         <td style="vertical-align:top;text-align:right">
          
         </td>
         <td>
                  Cash Collected  <asp:dropdownlist runat="server" ID="drpCashCollected" RepeatDirection="Horizontal"  RepeatLayout="Flow" CssClass="labels" Width="100px">
                          <asp:ListItem Text="" Value="" Selected="True"></asp:ListItem>
                          <asp:ListItem Text="Yes" Value="Yes"></asp:ListItem>
                          <asp:ListItem Text="No" Value="No" ></asp:ListItem>
                        </asp:dropdownlist>
          
   
         </td>
     </tr>
      <tr>
        <td style="vertical-align:top;text-align:right">
           
         </td>
         <td style="vertical-align:top">  User Name:&nbsp;
          <asp:TextBox ID="txtUserName" runat="server"></asp:TextBox>
         </td>
         <td style="vertical-align:top;">
            Password:&nbsp; <asp:TextBox ID="txtPassword" runat="server" TextMode="Password" Height="19px"></asp:TextBox>

         </td>
         <td>
           
   
         </td>
     </tr>

      <tr>
         <td></td>
         <td></td><td></td>
         <td style="text-align:center">   <asp:ImageButton ID="imgCashOnSaleEditUpdate" runat="server" Height="40px" ImageUrl="~/icon/btnupdate.png"  Width="100px"  /></td>
     </tr>

</table>



<!-- Cash On Sale Search -->
<table class="tblContent1" align ="left" id="tblCashOnSaleSearch" runat="server">

      <tr>
            <td width="200px;">
               
            </td>
             <td width="130px;">
               
            </td>
            <td width="200px;"></td>
    
            <td style="text-align:center">
           <asp:ImageButton ID="imgBack7" runat="server" Height="40px" ImageUrl="~/icon/btnback.png" Width="100px"  />

            </td>
        </tr>
      <tr>
          <td>
              <asp:ImageButton ID="btnCashOnSaleSearchTitle" runat="server" Height="108px" ImageUrl="~/icon/inventory.png" Width="108px" />
          </td>
          <td colspan="3">
             <asp:Label ID="lblTitle8" runat="server" CssClass="title" Text="Cash On Sale Search"></asp:Label>
           </td>
          
           
          
        </tr>
   
     <tr>
         <td colspan="4"><br /></td>
     </tr>
        <tr>
         <td colspan="4"></td>
     </tr>
     <tr>
         <td>Date</td>
         <td><asp:TextBox id="txtCashOnSaleSearchFrom" runat="server" Width="80px"></asp:TextBox>
             <ajaxToolkit:CalendarExtender ID="txtCashOnSaleSearchFromCal" Format="yyyy/MM/dd" runat="server" BehaviorID="txtCashOnSaleSearchFromCal" TargetControlID="txtCashOnSaleSearchFrom" PopupPosition="Left">
                    </ajaxToolkit:CalendarExtender>

~ <asp:TextBox ID="txtCashOnSaleSearchTo" runat="server"  Width="80px"></asp:TextBox>
 <ajaxToolkit:CalendarExtender ID="txtCashOnSaleSearchToCal" Format="yyyy/MM/dd" runat="server" BehaviorID="txtCashOnSaleSearchToCal" TargetControlID="txtCashOnSaleSearchTo" PopupPosition="Left">
                    </ajaxToolkit:CalendarExtender>

         </td>
	<td>Customer Name:</td>
         <td><asp:TextBox ID="txtCashOnSaleSearchCustomerName" runat="server"></asp:TextBox></td>
     </tr>
      <tr>
         <td>ASC Claim No</td>
         <td><asp:TextBox ID="txtCashOnSaleSearchAscClaimNo" runat="server"></asp:TextBox></td>
	<td>Total Amount</td>
         <td><asp:TextBox ID="txtCashOnSaleTotAmt" runat="server"></asp:TextBox></td>
     </tr>
      <tr>
         <td>Corp Number</td>
         <td><asp:TextBox ID="txtCashOnSaleSearchCorpNumber" runat="server"></asp:TextBox></td>
	<td>Cash Collected</td>
         <td>
   <asp:dropdownlist runat="server" ID="drpCashOnSearchCollected" RepeatDirection="Horizontal"  RepeatLayout="Flow" CssClass="labels" Width="100px">
                          <asp:ListItem Text="" Value="" Selected="True"></asp:ListItem>
                          <asp:ListItem Text="Yes" Value="Yes" ></asp:ListItem>
                          <asp:ListItem Text="No" Value="No" ></asp:ListItem>
                        </asp:dropdownlist>

         </td>
     </tr>
        <tr>
         <td>Corp Name</td>
         <td>  <asp:dropdownlist runat="server" ID="drpCashOnSaleCorpSearch" RepeatDirection="Horizontal"  RepeatLayout="Flow" CssClass="labels" Width="100px">
              </asp:dropdownlist></td>
	<td></td>
         <td></td>
     </tr>


 <tr>
         <td></td>
         <td></td>
<td></td>
         <td><asp:ImageButton ID="imgSearchCashOnSaleSearch" runat="server" Height="40px" ImageUrl="~/icon/btnsearch.png" Width="100px"  /></td>
     </tr>

 </table>
<table id="tblCashOnSaleSearchView" runat="server" visible="false" class="tblContent1" >
         <tr>
            <td width="200px;">
               
            </td>
             <td width="130px;" style="text-align:left">
               
            </td>
            <td width="200px;" ></td>
    
            <td style="text-align:center">
           

            </td>
        </tr>
   <tr>
         <td></td>
         <td></td><td></td>
         <td style="text-align:right">   <asp:ImageButton ID="imgCashOnSaleSearchDownload" runat="server" Height="40px" ImageUrl="~/icon/btnDownload.png"  Width="100px"  /></td>
     </tr>

<tr>
        <td colspan="4">
                  <asp:GridView ID="gvCashOnSaleInfo" runat="server" Width="99%" AllowPaging="true"
                PageSize="10" AutoGenerateColumns="False" EmptyDataText="Data Is Empty" BackColor="White"
                BorderColor="Black" BorderStyle="Double" EnableViewState="true"  OnPageIndexChanging="gvCashOnSaleInfo_PageIndexChanging" 
               >
                <Columns>
                  
                      <asp:TemplateField HeaderText="Corporate No">
                        <ItemTemplate >
                             <asp:Label ID="lblCorporateNumber"  runat="server" Text='<%# Eval("corp_number")  %>' ></asp:Label>
                        </ItemTemplate>
                        <HeaderStyle HorizontalAlign="Center" />
                        <ItemStyle HorizontalAlign="Left" Width="20%" />
                    </asp:TemplateField>
                         <asp:TemplateField HeaderText="Date">
                        <ItemTemplate>
                             <asp:Label ID="lblDate"  runat="server" Text='<%# Eval("invoice_date")  %>' ></asp:Label>
                        </ItemTemplate>
                        <HeaderStyle HorizontalAlign="Center" />
                        <ItemStyle HorizontalAlign="Center" Width="10%" />
                    </asp:TemplateField>
                     <asp:TemplateField HeaderText="Claim No"  >
                        <ItemTemplate>
                            <asp:Label ID="lblClaimNo"  runat="server" Text='<%# Eval("claim_no")  %>'></asp:Label>
                        </ItemTemplate>
                        <HeaderStyle HorizontalAlign="Left" />
                        <ItemStyle HorizontalAlign="Left" Width="10%" />
                    </asp:TemplateField>
                     <asp:TemplateField HeaderText="Corp Name"  >
                        <ItemTemplate>
                            <asp:Label ID="lblCorpName"  runat="server" Text='<%# Eval("corp_name")  %>'></asp:Label>
                        </ItemTemplate>
                        <HeaderStyle HorizontalAlign="Left" />
                        <ItemStyle HorizontalAlign="Left" Width="10%" />
                    </asp:TemplateField>
                      <asp:TemplateField HeaderText="Customer Name"  >
                        <ItemTemplate>
                            <asp:Label ID="lblCustomerName"  runat="server" Text='<%# Eval("customer_name")  %>'></asp:Label>
                        </ItemTemplate>
                        <HeaderStyle HorizontalAlign="Left" />
                        <ItemStyle HorizontalAlign="Left" Width="10%" />
                    </asp:TemplateField>
                       <asp:TemplateField HeaderText="Amount"  >
                        <ItemTemplate>
                            <asp:Label ID="lblAmount"  runat="server" Text='<%# Eval("total_amount")  %>'></asp:Label>
                        </ItemTemplate>
                        <HeaderStyle HorizontalAlign="Right" />
                        <ItemStyle HorizontalAlign="Right" Width="10%" />
                    </asp:TemplateField>
                         <asp:TemplateField HeaderText="Collected"  >
                        <ItemTemplate>
                            <asp:Label ID="lblCollected"  runat="server" Text='<%# Eval("corp_collect")  %>'></asp:Label>
                        </ItemTemplate>
                        <HeaderStyle HorizontalAlign="Right" />
                        <ItemStyle HorizontalAlign="Right" Width="10%" />
                    </asp:TemplateField>
                             <asp:TemplateField HeaderText="Invoice"  >
                        <ItemTemplate>
                            <asp:Label ID="lblInvoiceDate"  runat="server" Text='<%# Eval("invoicedate")  %>'></asp:Label>
                        </ItemTemplate>
                        <HeaderStyle HorizontalAlign="Right" />
                        <ItemStyle HorizontalAlign="Right" Width="10%" />
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


</table>

  

 <!-- Create Invoice & Search -->

<table class="tblContent1" align ="left" id="tblCreateInvoiceSearch" runat="server">

      <tr>
            <td width="200px;">
               
            </td>
             <td width="130px;">
               
            </td>
            <td width="200px;"></td>
    
            <td style="text-align:center">
           <asp:ImageButton ID="imgBack8" runat="server" Height="40px" ImageUrl="~/icon/btnback.png" Width="100px"  />

            </td>
        </tr>
      <tr>
          <td>
              <asp:ImageButton ID="btnCreateInvoiceSearchTitle" runat="server" Height="108px" ImageUrl="~/icon/inventory.png" Width="108px" />
          </td>
          <td colspan="3">
             <asp:Label ID="lblTitle9" runat="server" CssClass="title" Text="Create Invoice"></asp:Label>
           </td>
          
           
          
        </tr>
   
   
  <tr>
         <td colspan="4"><br /></td>
     </tr>
        <tr>
         <td colspan="4"></td>
     </tr>

 <tr>
         <td> Select Corporation </td>
	 <td>
          <asp:dropdownlist runat="server" ID="drpCreateInvoiceCorp" RepeatDirection="Horizontal"  RepeatLayout="Flow" CssClass="labels" Width="100px">
              </asp:dropdownlist>
	 </td>
	<td></td>
<td></td>
     </tr>
        

     <tr>
         <td>Billing Date</td>
         <td><asp:TextBox id="txtCreateInvoiceFrom" runat="server" Width="80px"></asp:TextBox>
             <ajaxToolkit:CalendarExtender ID="txtCreateInvoiceFromCal" Format="yyyy/MM/dd" runat="server" BehaviorID="txtCreateInvoiceFromCal" TargetControlID="txtCreateInvoiceFrom" PopupPosition="Left">
                    </ajaxToolkit:CalendarExtender>

~ <asp:TextBox ID="txtCreateInvoiceTo" runat="server"  Width="80px"></asp:TextBox>
 <ajaxToolkit:CalendarExtender ID="txtCreateInvoiceCal" Format="yyyy/MM/dd" runat="server" BehaviorID="txtCreateInvoiceCal" TargetControlID="txtCreateInvoiceTo" PopupPosition="Left">
                    </ajaxToolkit:CalendarExtender>
         </td>
	<td></td>
         <td></td>
     </tr>
 
 <tr>
         <td></td>
         <td></td>
<td></td>
         <td><asp:ImageButton ID="imgCreateInvoiceSearch" runat="server" Height="40px" ImageUrl="~/icon/btnsearch.png" Width="100px"  /></td>
     </tr>

 </table>

<table id="tblCreateInvoiceSearchView" runat="server" visible="false" class="tblContent1" >
         <tr>
            <td width="200px;">
               
            </td>
             <td width="130px;" style="text-align:left">
               
            </td>
            <td width="200px;" ></td>
    
            <td style="text-align:center">
           

            </td>
        </tr>
   <tr>
         <td></td>
         <td></td><td></td>
         <td style="text-align:right">   <asp:ImageButton ID="imgCashOnSaleInvoiceCreate" runat="server" Height="40px" ImageUrl="~/icon/btnCreateInvoice.png"  Width="100px"  /></td>
     </tr>

<tr>
    <td></td>
    <td></td>
        <td colspan="2">
                  <asp:GridView ID="gvCreateInvoice" runat="server" Width="70%" AllowPaging="true"
                PageSize="10" AutoGenerateColumns="False" EmptyDataText="Data Is Empty" BackColor="White"
                BorderColor="Black" BorderStyle="Double" EnableViewState="true" OnPageIndexChanging="gvCreateInvoice_PageIndexChanging"               >
                <Columns>
                  
                      <asp:TemplateField HeaderText="Po No">
                        <ItemTemplate >
                             <asp:Label ID="lblCorpPoNo"  runat="server" Text='<%# Eval("CorpPoNo")  %>' ></asp:Label>
                        </ItemTemplate>
                        <HeaderStyle HorizontalAlign="Center" />
                        <ItemStyle HorizontalAlign="Left" Width="5%" />
                    </asp:TemplateField>
                      <asp:TemplateField HeaderText="Claim No"  >
                        <ItemTemplate>
                            <asp:Label ID="lblClaimNo"  runat="server" Text='<%# Eval("claim_no")  %>'></asp:Label>
                        </ItemTemplate>
                        <HeaderStyle HorizontalAlign="Left" />
                        <ItemStyle HorizontalAlign="Left" Width="5%" />
                    </asp:TemplateField>
                     <asp:TemplateField HeaderText="Collect Amount"  >
                        <ItemTemplate>
                            <asp:Label ID="lblTotalAmount"  runat="server" Text='<%# Eval("total_amount")  %>'></asp:Label>
                        </ItemTemplate>
                        <HeaderStyle HorizontalAlign="Left" />
                        <ItemStyle HorizontalAlign="Left" Width="5%" />
                    </asp:TemplateField>
                      <asp:TemplateField HeaderText="Not Covered"  >
                        <ItemTemplate>
                            <asp:CheckBox ID="chkSelectClaim" runat="server" />
                           
                        </ItemTemplate>
                        <HeaderStyle HorizontalAlign="Left" />
                        <ItemStyle HorizontalAlign="Left" Width="5%" />
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


</table>

<asp:Button ID="BtnCancel" runat="server" Text="Button" style="display:none;"/>
 </div>
</div>

<div id="dialog" title="message" style="display:none;"> >
    <asp:Label ID="lblMsg" runat="server" Text=""></asp:Label>
</div>

<%--    <script type="text/javascript">  ASP側に埋め込み済
        $(function () {
            $("#dialog" ).dialog({
                width: 400,
                buttons:
                {
                    "OK": function () {
                        $(this).dialog('close');
                    },
                    "CANCEL": function () {
                        $(this).dialog('close');
                        $('[id$="BtnCancel"]').click();
                    }
                }
            });
        });    
    </script>--%>

</asp:Content>

