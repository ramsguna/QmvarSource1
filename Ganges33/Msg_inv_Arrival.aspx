<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Site3Msg.Master" CodeBehind="Msg_inv_Arrival.aspx.vb" Inherits="Ganges33.Msg_inv_Arrival" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
  <link type="text/css" href="CSS/Common/Msg_inv_Arrival.css" rel="stylesheet" />
    <style type="text/css">
       
    </style>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="div-entirepage">
        <table class="tbl-entirepage">
            <tr>
                <td class="td-textbox1">
                    <asp:Label ID="Label1" runat="server" Text="Number of registered parts：" CssClass="lbl-txt-numberofregiteredparts-lbl-txt-label1-lbl-registered part number-lbl-label2-lbl-msgcontent-lbl-msg2"></asp:Label>
                    <asp:Label ID="lblAllParts" runat="server" Text="Label" CssClass="lbl-txt-numberofregiteredparts-lbl-txt-label1-lbl-registered part number-lbl-label2-lbl-msgcontent-lbl-msg2"></asp:Label><br />
                    <asp:Label ID="Label3" runat="server" Text="Registered part number：" CssClass="lbl-txt-numberofregiteredparts-lbl-txt-label1-lbl-registered part number-lbl-label2-lbl-msgcontent-lbl-msg2"></asp:Label>
                    <asp:Label ID="lblKindParts" runat="server" Text="Label" CssClass="lbl-txt-numberofregiteredparts-lbl-txt-label1-lbl-registered part number-lbl-label2-lbl-msgcontent-lbl-msg2"></asp:Label><br />
                    <asp:Label ID="lblMsg" runat="server" Text="マスタに存在しないアイテムあり：" CssClass="lbl-txt-numberofregiteredparts-lbl-txt-label1-lbl-registered part number-lbl-label2-lbl-msgcontent-lbl-msg2"></asp:Label>
                    <asp:Label ID="lblMsgContent" runat="server" CssClass="lbl-txt-numberofregiteredparts-lbl-txt-label1-lbl-registered part number-lbl-label2-lbl-msgcontent-lbl-msg2"></asp:Label><br />
                    <asp:Label ID="lblMsg2" runat="server" Text="バーコードPDF出力に失敗しました。" CssClass="lbl-txt-numberofregiteredparts-lbl-txt-label1-lbl-registered part number-lbl-label2-lbl-msgcontent-lbl-msg2"></asp:Label>               
                </td>
            </tr>
            <tr>
                <td align ="center" class="td-textbox2">
                    <asp:Label ID="Label5" runat="server" Text="Parts have been delivered successfully." CssClass="lbl-txt-msg-lbl-txt-msg-lbl-txt-label"></asp:Label><br />
                    <asp:Label ID="Label2" runat="server" Text="arrival number : " CssClass="lbl-txt-msg-lbl-txt-msg-lbl-txt-label"></asp:Label><asp:Label ID="lblArrivalNo" runat="server" Text="Label" CssClass="lbl-txt-msg-lbl-txt-msg-lbl-txt-label"></asp:Label>
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
