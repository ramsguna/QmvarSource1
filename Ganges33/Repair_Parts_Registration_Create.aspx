<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Site5.Master" CodeBehind="Repair_Parts_Registration_Create.aspx.vb" Inherits="Ganges33.Repair_Parts_Registration_Create" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script src="http://ajax.googleapis.com/ajax/libs/jquery/1.7.0/jquery.min.js"></script>
    <link href="CSS/Common/Repair_parts_Registeration_Create.css" rel="stylesheet" />
    <link type="text/css" href="http://ajax.googleapis.com/ajax/libs/jqueryui/1/themes/start/jquery-ui.css" rel="stylesheet">
    <script type="text/javascript" src="http://ajax.googleapis.com/ajax/libs/jqueryui/1/jquery-ui.min.js"></script>
        </asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
      <div class="div-entire-tbl" style="background-image: url('pagewall_2/wall_repair-2.png')">
       <div class="div-money-parts-registeration">

           <table class="tbl-money-parts-registeration">
               <tr>
                   <td class="td-btn-repair" rowspan = "4">
                    <asp:Image ID="Image3" runat="server" Height="108px" ImageUrl="~/icon/repair.png" Width="108px" />
                   </td>
                   <td colspan = "2" class="td-lbl-parts-registeration">
                    <asp:Label ID="Label50" runat="server" CssClass="lbl-large-font" Text="Parts Registration Create"></asp:Label>
                   </td>
                   <td colspan = "3" class="td-lbl-parts-registeration-date-time">
                    <asp:Label ID="lblDate" runat="server" Text="Record date&amp;time : " CssClass="lbl-font-common" ></asp:Label>
                    <asp:Label ID="lblRecord" runat="server" CssClass="lbl-font-common" ></asp:Label>
                    <asp:Label ID="lblName" runat="server" Text="user name: " CssClass="lbl-font-common" ></asp:Label>
                    <asp:Label ID="lblYousername" runat="server" CssClass="lbl-font-common" ></asp:Label><br />
                    <asp:Label ID="lblModify" runat="server" Text="Modify Parts Number" CssClass="lbl-font-common"></asp:Label>
                   
                       <asp:TextBox ID="TextPartsNumber" runat="server"></asp:TextBox>

                    <asp:ImageButton ID="btnStart2" runat="server"  ImageUrl="~/icon/start.png"  CssClass="btn-start" />
                   </td>
               </tr>
               <tr>
                   <td class="td-lbl-shipcode-to-partsflg">
                       <asp:Label ID="Label1" runat="server" CssClass="lbl-font-common" Text="ship_code"></asp:Label>
                   </td>
                   <td class="td-lbl-shipcode-txtbox-marker-parts-locs-unit-category-commensation-commensation-tecfee-assing ">
                       <asp:Label ID="lblShipCode" runat="server" CssClass="lbl-font-common"></asp:Label>
                   </td>
                   <td class="td-blank2"></td>
                   <td class="td-blank2"></td>
                   <td class="td-blank3"></td>
               </tr>
               <tr>
                   <td class="td-lbl-shipcode-to-partsflg">
                       <asp:Label ID="Label3" runat="server" CssClass="lbl-font-common" Text="*maker"></asp:Label>
                   </td>
                   <td class="td-lbl-shipcode-txtbox-marker-parts-locs-unit-category-commensation-commensation-tecfee-assing ">
                       <asp:TextBox ID="TextMaker" runat="server" Cssclass="txtbox-marker-parts-locs-unit-category-commensation-commensation-tecfee-assing" ></asp:TextBox>
                   </td>
                   <td class="td-blank1"></td>
                   <td></td>
                   <td></td>
               </tr>
               <tr>
                   <td class="td-lbl-shipcode-to-partsflg">
                       <asp:Label ID="Label4" runat="server" CssClass="lbl-font-common" Text="*parts_no"></asp:Label>
                   </td>
                   <td class="td-lbl-shipcode-txtbox-marker-parts-locs-unit-category-commensation-commensation-tecfee-assing ">
                       <asp:TextBox ID="TextPartsNo" runat="server" Cssclass="txtbox-marker-parts-locs-unit-category-commensation-commensation-tecfee-assing"></asp:TextBox>
                   </td>
                   <td class="td-blank2"></td>
                   <td></td>
                   <td></td>
               </tr>
               <tr>
                   <td class="td-blank1"></td>
                   <td class="td-lbl-shipcode-to-partsflg">
                       <asp:Label ID="Label5" runat="server" CssClass="lbl-font-common" Text="*parts_name"></asp:Label>
                   </td>
                   <td colspan = "2" class="td-txbox-partsname-productname-chkbox-units-parts">
                       <asp:TextBox ID="TextPartsName" runat="server" cssclass="txbox-partsname-productname-chkbox-units-parts"></asp:TextBox>
                   </td>
                   <td class="td-blank2"></td>
                   <td class="td-blank2"></td>
               </tr>
               <tr>
                   <td class="td-blank1"></td>
                   <td class="td-lbl-shipcode-to-partsflg">
                       <asp:Label ID="Label6" runat="server" CssClass="lbl-font-common" Text="*loc_1"></asp:Label>
                   </td>
                   <td class="td-lbl-shipcode-txtbox-marker-parts-locs-unit-category-commensation-commensation-tecfee-assing ">
                       <asp:TextBox ID="TextLoc1" runat="server" Cssclass="txtbox-marker-parts-locs-unit-category-commensation-commensation-tecfee-assing"></asp:TextBox>
                   </td>
                   <td class="td-blank2"></td>
                   <td></td>
                   <td></td>
               </tr>
               <tr>
                   <td class="td-blank1"></td>
                   <td class="td-lbl-shipcode-to-partsflg">
                       <asp:Label ID="Label7" runat="server" CssClass="lbl-font-common" Text="loc_2"></asp:Label>
                   </td>
                   <td class="td-lbl-shipcode-txtbox-marker-parts-locs-unit-category-commensation-commensation-tecfee-assing ">
                       <asp:TextBox ID="TextLoc2" runat="server" Cssclass="txtbox-marker-parts-locs-unit-category-commensation-commensation-tecfee-assing"></asp:TextBox>
                   </td>
                   <td class="td-blank2"></td>
                   <td></td>
                   <td></td>
               </tr>
               <tr>
                   <td class="td-blank1"></td>
                   <td class="td-lbl-shipcode-to-partsflg">
                       <asp:Label ID="Label8" runat="server" CssClass="lbl-font-common" Text="loc_3"></asp:Label>
                   </td>
                   <td class="td-lbl-shipcode-txtbox-marker-parts-locs-unit-category-commensation-commensation-tecfee-assing ">
                       <asp:TextBox ID="TextLoc3" runat="server" Cssclass="txtbox-marker-parts-locs-unit-category-commensation-commensation-tecfee-assing"></asp:TextBox>
                   </td>
                   <td class="td-blank2"></td>
                   <td></td>
                   <td></td>
               </tr>
               <tr>
                   <td class="td-blank1"></td>
                   <td class="td-lbl-shipcode-to-partsflg">
                       <asp:Label ID="Label9" runat="server" CssClass="lbl-font-common" Text="*unit_price"></asp:Label>
                   </td>
                   <td class="td-lbl-shipcode-txtbox-marker-parts-locs-unit-category-commensation-commensation-tecfee-assing ">
                       <asp:TextBox ID="TextUnitPrice" runat="server" Cssclass="txtbox-marker-parts-locs-unit-category-commensation-commensation-tecfee-assing"></asp:TextBox>
                   </td>
                   <td class="td-blank1"></td>
                   <td></td>
                   <td></td>
               </tr>
               <tr>
                   <td class="td-blank1"></td>
                   <td class="td-lbl-shipcode-to-partsflg">
                       <asp:Label ID="Label10" runat="server" CssClass="lbl-font-common" Text="*category"></asp:Label>
                   </td>
                   <td class="td-lbl-shipcode-txtbox-marker-parts-locs-unit-category-commensation-commensation-tecfee-assing ">
                       <asp:TextBox ID="TextCategory" runat="server" Cssclass="txtbox-marker-parts-locs-unit-category-commensation-commensation-tecfee-assing"></asp:TextBox>
                   </td>
                   <td class="td-blank2"></td>
                   <td class="td-blank2"></td>
                   <td class="td-blank2"></td>
               </tr>
               <tr>
                   <td class="td-blank1"></td>
                   <td class="td-lbl-shipcode-to-partsflg">
                       <asp:Label ID="Label11" runat="server" CssClass="lbl-font-common" Text="*g_unit_price"></asp:Label>
                   </td>
                   <td class="td-lbl-shipcode-txtbox-marker-parts-locs-unit-category-commensation-commensation-tecfee-assing ">
                       <asp:TextBox ID="TextGUnitPrice" runat="server" Cssclass="txtbox-marker-parts-locs-unit-category-commensation-commensation-tecfee-assing"></asp:TextBox>
                   </td>
                   <td class="td-blank2"></td>
                   <td></td>
                   <td></td>
               </tr>
               <tr>
                   <td class="td-blank1"></td>
                   <td class="td-lbl-shipcode-to-partsflg">
                       <asp:Label ID="Label12" runat="server" CssClass="lbl-font-common" Text="comoensation"></asp:Label>
                   </td>
                   <td class="td-lbl-shipcode-txtbox-marker-parts-locs-unit-category-commensation-commensation-tecfee-assing ">
                       <asp:TextBox ID="TextComoensation" runat="server" Cssclass="txtbox-marker-parts-locs-unit-category-commensation-commensation-tecfee-assing"></asp:TextBox>
                   </td>
                   <td class="td-blank2"></td>
                   <td></td>
                   <td></td>
               </tr>
               <tr>
                   <td class="td-blank1"></td>
                   <td class="td-lbl-shipcode-to-partsflg">
                       <asp:Label ID="Label13" runat="server" CssClass="lbl-font-common" Text="techfee_paid"></asp:Label>
                   </td>
                   <td class="td-lbl-shipcode-txtbox-marker-parts-locs-unit-category-commensation-commensation-tecfee-assing ">
                       <asp:TextBox ID="TextTechfeePaid" runat="server" Cssclass="txtbox-marker-parts-locs-unit-category-commensation-commensation-tecfee-assing"></asp:TextBox>
                   </td>
                   <td class="td-blank2">&nbsp;</td>
                   <td></td>
                   <td></td>
               </tr>
               <tr>
                   <td class="td-blank1"></td>
                   <td class="td-lbl-shipcode-to-partsflg">
                       <asp:Label ID="Label14" runat="server" CssClass="lbl-font-common" Text="techfee_guarantee"></asp:Label>
                   </td>
                   <td class="td-lbl-shipcode-txtbox-marker-parts-locs-unit-category-commensation-commensation-tecfee-assing ">
                       <asp:TextBox ID="TextTechfeeGuarantee" runat="server" Cssclass="txtbox-marker-parts-locs-unit-category-commensation-commensation-tecfee-assing"></asp:TextBox>
                   </td>
                   <td class="td-blank2"></td>
                   <td></td>
                   <td></td>
               </tr>
               <tr>
                   <td class="td-blank1"></td>
                   <td class="td-lbl-shipcode-to-partsflg">
                       <asp:Label ID="Label15" runat="server" CssClass="lbl-font-common" Text="*unit_flg"></asp:Label>
                   </td>
                   <td colspan = "2" >
                       <asp:CheckBox ID="Chktangible" runat="server" CssClass="lbl-font-common" Text="tangible" AutoPostBack="True" />
                       &nbsp;&nbsp;
                       <asp:CheckBox ID="ChkIntangible" runat="server" CssClass="lbl-font-common" Text="intangible" AutoPostBack="True" />
                   </td>
                   <td></td>
                   <td></td>
               </tr>
               <tr>
                   <td class="td-blank1"></td>
                   <td class="td-lbl-shipcode-to-partsflg">
                       <asp:Label ID="Label16" runat="server" CssClass="lbl-font-common" Text="product_name"></asp:Label>
                   </td>
                   <td colspan = "2">
                       <asp:TextBox ID="TextProductName" runat="server" cssclass="txbox-partsname-productname-chkbox-units-parts" ></asp:TextBox>
                   </td>
                   <td></td>
                   <td></td>
               </tr>
               <tr>
                   <td class="td-blank1"></td>
                   <td class="td-lbl-shipcode-to-partsflg">
                       <asp:Label ID="Label17" runat="server" CssClass="lbl-font-common" Text="Assing_type"></asp:Label>
                   </td>
                   <td class="td-lbl-shipcode-txtbox-marker-parts-locs-unit-category-commensation-commensation-tecfee-assing ">
                       <asp:TextBox ID="TextAssingType" runat="server" Cssclass="txtbox-marker-parts-locs-unit-category-commensation-commensation-tecfee-assing"></asp:TextBox>
                   </td>
                   <td class="td-blank2"></td>
                   <td></td>
                   <td></td>
               </tr>
               <tr>
                   <td class="td-blank1"></td>
                   <td class="td-lbl-shipcode-to-partsflg">
                       <asp:Label ID="Label18" runat="server" CssClass="lbl-font-common" Text="*parts_flg"></asp:Label>
                   </td>
                   <td colspan = "2">
                       <asp:Label ID="Label51" runat="server" CssClass="lbl-font-common" Text="serial No"></asp:Label>
                      
                       <asp:CheckBox ID="ChkYes" runat="server" CssClass="lbl-font-common" Text="Yes" AutoPostBack="True" />
                    
                       <asp:CheckBox ID="ChkNo" runat="server" CssClass="lbl-font-common" Text="No" AutoPostBack="True" />
                   </td>
                   <td>
                    <asp:ImageButton ID="btnStart" runat="server" ImageUrl="~/icon/start.png" CssClass="btn-start" />
                   </td>
                   <td></td>
               </tr>
               <tr>
                   <td class="td-blank1"></td>
                   <td class="td-blank2"></td>
                   <td class="td-blank2"></td>
                   <td class="td-blank2"></td>
                   <td></td>
                   <td></td>
               </tr>
           </table>

       </div>
      </div>
    
    <div id="dialog" title="message" style="display:none;"> >
        <asp:Label ID="lblMsg" runat="server" Text=""></asp:Label>
    </div>  

</asp:Content>
