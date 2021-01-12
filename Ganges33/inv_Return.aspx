<%@ Page Title="QMVQR-InvReturn" Language="vb" AutoEventWireup="false" MasterPageFile="~/Site3.Master" CodeBehind="inv_Return.aspx.vb" Inherits="Ganges33.inv_Return" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script src="http://ajax.googleapis.com/ajax/libs/jquery/1.7.0/jquery.min.js"></script>
    <link type="text/css" href="http://ajax.googleapis.com/ajax/libs/jqueryui/1/themes/start/jquery-ui.css" rel="stylesheet">
    <script type="text/javascript" src="http://ajax.googleapis.com/ajax/libs/jqueryui/1/jquery-ui.min.js"></script>     
     <link type="text/css" href="CSS/Common/inv_Return.css" rel="stylesheet" /> 

    <style type="text/css">
               
 </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="div-EntirePage-background-image" style="background-image: url('pagewall_2/wall_inventry-2.png')">
     <div class="div-EntirePage">
    <table class="div-table-EntirePage" align ="left">
        <tr>
            <td class="td-Checkbox-UseSerial">
                <asp:CheckBox ID="chkUseSerial" runat="server" AutoPostBack="True" CssClass="fontFamily" />
                <asp:Label ID="Label18" runat="server" Text="Use Serial" CssClass="fontFamily"></asp:Label>
            </td>
            <td class="td-ChkUseSerial-lblUnuseSerial">
                <asp:CheckBox ID="chkUnUseSerial" runat="server" AutoPostBack="True" CssClass="fontFamily" />
                <asp:Label ID="Label19" runat="server" Text="Unuse Serial" CssClass="fontFamily"></asp:Label>
            </td>
            <td class="td-Blank1"></td>
        </tr>
        <tr>
            <td class="td-lblDeleviryNo-SSReturnNo">
                <asp:Label ID="Label1" runat="server" Text="Delivery no" CssClass="lbl-DeleveryNo"></asp:Label>
            </td>
            <td class="td-textDeliveryNo-textApprovalNo">
                <asp:TextBox ID="TextDeliveryNo" runat="server" CssClass="txt-DeleveryNo-approvalNo-Textitem"></asp:TextBox>
            </td>
            <td class="td-Blank2-ImageButton"></td>
        </tr>
        <tr>
            <td class="td-Blank3">
                &nbsp;</td>
            <td class="td-Blank4">
                &nbsp;</td>
            <td class="td-Blank2-ImageButton" rowspan ="2">
                <asp:ImageButton ID="BtnStart" runat="server" ImageUrl="~/icon/start.png" CssClass="img-Button-Start" />
            </td>
        </tr>
        <tr>
            <td class="td-lblDeleviryNo-SSReturnNo">
                <asp:Label ID="Label3" runat="server" Text="SS Return no" CssClass="fontFamily"></asp:Label>
            </td>
            <td class="td-textDeliveryNo-textApprovalNo">
                <asp:TextBox ID="TextApprovalNo" runat="server" CssClass="txt-DeleveryNo-approvalNo-Textitem"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td class="td-Blank5-Blank6">&nbsp;</td>
            <td class="td-Blank7">&nbsp;</td>
            <td class="td-Blank5-Blank6"></td>
        </tr>
        <tr>
            <td class="td-Currentreturnnumber">
                <asp:Label ID="lblCurrentReturn" runat="server" Text="Current return number:" CssClass="lbl-Current-returnnumber-DeliveryNo"></asp:Label>
            </td>
            <td class="td-labelReturnNumber">
                <asp:Label ID="lblReturnNnumber" runat="server" CssClass="fontFamily"></asp:Label>
            </td>
            <td class="td-Blank8"></td>
        </tr>
        <tr>
            <td class="td-lblCurrentDelivery">
                <asp:Label ID="lblCurrentDelivery" runat="server" Text="Current delivery number: " CssClass="lbl-Current-returnnumber-DeliveryNo"></asp:Label>
            </td>
            <td class="td-lbl-DeliveryNumber">
                <asp:Label ID="lblDeliveryNumber" runat="server" CssClass="fontFamily"></asp:Label>
            </td>
            <td class="td-Blank9"></td>
        </tr>
        <tr>
            <td class="td-Blank10"></td>
            <td class="td-Blank11"></td>
            <td class="td-Blank12"></td>
        </tr>
        <tr>
            <td class="td-lbl-CSVInport">
                <asp:Label ID="Label15" runat="server" Text="CSV Inport" CssClass="fontFamily"></asp:Label>
            </td>
            <td class="td-FileUploadInv" colspan ="2">
                <asp:FileUpload ID="FileUploadInv" runat="server" CssClass="FileUploadInv" />
                <asp:ImageButton ID="btnCsv" runat="server" ImageUrl="~/icon/folder-13.png" CssClass="btnCsv" />
            </td>
        </tr>
        <tr>
            <td class="td-Blank13"></td>
            <td class="td-Blank14"></td>
            <td class="td-Blank15"></td>
        </tr>
        <tr>
            <td class="td-lbl-Item">
                <asp:Label ID="Label16" runat="server" Text="Item" CssClass="fontFamily"></asp:Label>
            </td>
            <td >
                <asp:TextBox ID="Textitem" runat="server" CssClass="txt-DeleveryNo-approvalNo-Textitem"></asp:TextBox>
            </td>
            <td class="td-ImageButton" rowspan ="2">
                <asp:ImageButton ID="btnImport" runat="server" ImageUrl="~/icon/import.png" CssClass="btnImport" />
            </td>
        </tr>
        <tr>
            <td class="td-label-Qty">
                <asp:Label ID="Label17" runat="server" Text="Qty" CssClass="fontFamily"></asp:Label>
            </td>
            <td >
                <asp:TextBox ID="TextQty" runat="server" CssClass="textbox-Qty"></asp:TextBox>
            </td>
        </tr>
    </table>
    <table class="table-SpecificPage">
        <tr>
            <td class="td-Blank16"></td>
        </tr>
    </table>
    <table class="table-ListHistory-btnClose" >
        <tr>
            <td class="td-listbox">
                <asp:ListBox ID="ListHistory" runat="server" CssClass="listbox-History"></asp:ListBox>
            </td>
        </tr>
        <tr>
            <td align ="right" class="td-btnClose">
                <asp:ImageButton ID="btnClose" runat="server" ImageUrl="~/icon/close.png" CssClass="Img-Button-Close" />
            </td>
        </tr>
    </table>
<asp:Button ID="BtnCancel" runat="server" Text="Button" style="display:none;"/>
  </div>
 </div>
<div id="dialog" title="message" style="display:none;">
    <asp:Label ID="lblMsg" runat="server" Text=""></asp:Label>
</div>
</asp:Content>
