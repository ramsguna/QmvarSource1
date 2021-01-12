<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Site5.Master" CodeBehind="Repair_Tools.aspx.vb" Inherits="Ganges33.Repair_Tools" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script src="http://ajax.googleapis.com/ajax/libs/jquery/1.7.0/jquery.min.js"></script>
    <link type="text/css" href="http://ajax.googleapis.com/ajax/libs/jqueryui/1/themes/start/jquery-ui.css" rel="stylesheet">
    <script type="text/javascript" src="http://ajax.googleapis.com/ajax/libs/jqueryui/1/jquery-ui.min.js"></script>
    <link type="text/css" href="CSS/Common/Repair_Tools.css" rel="stylesheet" />
       <style type="text/css">
        
            </style> 
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
     <div class="div-pagewall" style="background-image: url('pagewall_2/wall_repair-2.png')">
       <div class="div-entirepage"> 

           <table class="table">
               <tr>
                   <td class="td-image">
                    <asp:Image ID="Image3" runat="server" ImageUrl="~/icon/repair.png" />
                   </td>
                   <td class="td-repairtools">
                    <asp:Label ID="Label50" runat="server" CssClass="lbl-repairtools" Text="Repair Tools"></asp:Label>
                   </td>
                   <td class="td-blank-header-td-blank3-td-blank4-td-blank5"></td>
                   <td class="td-blank-header-td-blank3-td-blank4-td-blank5"></td>
                   <td class="td-blank-header-td-blank3-td-blank4-td-blank5"></td>
                   <td class="td-blank4-header"></td>
               </tr>
               <tr>
                   <td class="td-blank1-left"></td>
                   <td class="td-reissue">
                       <asp:Label ID="Label51" runat="server" CssClass="lbl-reissue-lbl-receptpdf-chbox1-chbox2-chbox3-td-po-no-lbl-modify-lbl-partsreg" Text="1.reissue"></asp:Label>
                   </td>
                   <td class="td-receptpdf-td-estipdf-td-workrpt">
                       <asp:CheckBox ID="CheckBox1" runat="server" CssClass="lbl-reissue-lbl-receptpdf-chbox1-chbox2-chbox3-td-po-no-lbl-modify-lbl-partsreg" Text="recept PDF" />
                   </td>
                   <td class="td-receptpdf-td-estipdf-td-workrpt">
                       <asp:CheckBox ID="CheckBox2" runat="server" CssClass="lbl-reissue-lbl-receptpdf-chbox1-chbox2-chbox3-td-po-no-lbl-modify-lbl-partsreg" Text="Esitimate PDF" />
                   </td>
                   <td class="td-receptpdf-td-estipdf-td-workrpt">
                       <asp:CheckBox ID="CheckBox3" runat="server" CssClass="lbl-reissue-lbl-receptpdf-chbox1-chbox2-chbox3-td-po-no-lbl-modify-lbl-partsreg" Text="Work Report PDF" />
                   </td>
                   <td class="td-blank1-right214"></td>
               </tr>
               <tr>
                   <td class="td-blank2-left-td-blank-partsrgt"></td>
                   <td align = "right">
                       <asp:Label ID="Label54" runat="server" Text="PO No." CssClass="lbl-reissue-lbl-receptpdf-chbox1-chbox2-chbox3-td-po-no-lbl-modify-lbl-partsreg"></asp:Label>
                   </td>
                   <td colspan = "2">
                       <asp:TextBox ID="TextBox1" runat="server"></asp:TextBox>
                   </td>
                   <td class="td-start1">
                    <asp:ImageButton ID="btnStart" runat="server" ImageUrl="~/icon/start.png" CssClass="image-start1-image-start2" />
                   </td>
                   <td class="td-blank2-right"></td>
               </tr>
               <tr>
                   <td class="td-blank3-left-td-blank4-left"></td>
                   <td class="td-mfycontents-td-partsreg">
                       <asp:Label ID="Label52" runat="server" CssClass="lbl-reissue-lbl-receptpdf-chbox1-chbox2-chbox3-td-po-no-lbl-modify-lbl-partsreg" Text="2.Modify Contents"></asp:Label>
                   </td>
                   <td class="td-blank1-td-blank2-td-blank3-td-image-start2"></td>
                   <td class="td-blank1-td-blank2-td-blank3-td-image-start2"></td>
                   <td class="td-blank1-td-blank2-td-blank3-td-image-start2"></td>
                   <td class="td-blank3-right"></td>
               </tr>
               <tr>
                   <td class="td-blank2-left-td-blank-partsrgt"></td>
                   <td class="td-mfycontents-td-partsreg">
                       <asp:Label ID="Label53" runat="server" CssClass="lbl-reissue-lbl-receptpdf-chbox1-chbox2-chbox3-td-po-no-lbl-modify-lbl-partsreg" Text="3.Parts Registration"></asp:Label>
                   </td>
                   <td colspan = "2" class="td-dropdown-partsrgt">
                       <asp:DropDownList ID="DropDownPartsRegistration" runat="server">
                       </asp:DropDownList>
                   </td>
                   <td class="td-blank1-td-blank2-td-blank3-td-image-start2">
                    <asp:ImageButton ID="btnStart2" runat="server" ImageUrl="~/icon/start.png" CssClass="image-start1-image-start2" />
                   </td>
                   <td class="td-blank-partsrgt"></td>
               </tr>
               <tr>
                   <td class="td-blank1">&nbsp;</td>
                   <td class="td-blank2">&nbsp;</td>                   
                   <td class="td-blank4">&nbsp;</td>
                   <td class="td-blank5">&nbsp;</td>
                   <td>&nbsp;</td>
               </tr>
           </table>

         </div>
      </div>
    
    <div id="dialog" title="message" style="display:none;"> >
        <asp:Label ID="lblMsg" runat="server" Text=""></asp:Label>
    </div>
</asp:Content>

