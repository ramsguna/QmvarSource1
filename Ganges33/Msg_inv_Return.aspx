<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Site3msg.Master" CodeBehind="Msg_inv_Return.aspx.vb" Inherits="Ganges33.Msg_inv_Return" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
  <link type="text/css" href="CSS/Common/Msg-inv-Return.css" rel="stylesheet" />
    <style type="text/css">
       
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="div-entirepage">
        <table class="tbl-entirepage">
            <tr>
                <td>
                    <asp:Label ID="Label1" runat="server" Text="Number of registered parts：" CssClass="lbl-txt-numberofregisteredparts"></asp:Label>
                    <asp:Label ID="lblAllParts" runat="server" Text="Label" CssClass="lbl-txt-label1"></asp:Label><br />
                    <asp:Label ID="Label3" runat="server" Text="Registered part number：" CssClass="lbl-txt-registerpartnumber"></asp:Label>
                    <asp:Label ID="lblKindParts" runat="server" Text="Label" CssClass="lbl=txt-label"></asp:Label><br />
                    <asp:Label ID="lblMsg" runat="server" Text="返却対象でないアイテムあり：" CssClass="lbl-txt-msg"></asp:Label>
                    <asp:Label ID="lblMsgContent" runat="server" CssClass="lbl-txt-msgcontent"></asp:Label>
                </td>
            </tr>
            <tr>
                <td align ="center" class="td-center">
                    <asp:Label ID="Label5" runat="server" Text="Parts have been delivered successfully." CssClass="lbl-txt-msg-lbl-txt-GSSreturnnumber-lbl-txt-label"></asp:Label><br />
                    <asp:Label ID="Label2" runat="server" Text="GSS return number : " CssClass="lbl-txt-msg-lbl-txt-GSSreturnnumber-lbl-txt-label"></asp:Label><asp:Label ID="lblReturnNo" runat="server" Text="Label" CssClass="lbl-txt-msg-lbl-txt-GSSreturnnumber-lbl-txt-label"></asp:Label>
                </td>
            </tr>
            <tr>
                <td class="td-textbox3"></td>
            </tr>
            <tr>
                <td class="td-textbox4"></td>
            </tr>
            <tr>
                <td align ="right">
                    <asp:ImageButton ID="btnBack" runat="server" Height="62px" ImageUrl="~/icon/back.png" Width="177px" />
                </td>
            </tr>
        </table>
        <br />
        <br />
        <br />
    </div>
</asp:Content>
