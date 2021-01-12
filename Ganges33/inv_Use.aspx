<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Site3.Master" CodeBehind="inv_Use.aspx.vb" Inherits="Ganges33.inv_Use" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script src="http://ajax.googleapis.com/ajax/libs/jquery/1.7.0/jquery.min.js"></script>
    <link href="CSS/Common/Inv_Use.css" rel="stylesheet" />
    <link type="text/css" href="http://ajax.googleapis.com/ajax/libs/jqueryui/1/themes/start/jquery-ui.css" rel="stylesheet">
    <script type="text/javascript" src="http://ajax.googleapis.com/ajax/libs/jqueryui/1/jquery-ui.min.js"></script>
    
        </asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="div-entire-tbl"  style="background-image: url('pagewall_2/wall_inventry-2.png')">
       <div class="div-use-tbl "> 
        <table class="tbl-use">
            <tr>
                <td class="td-blank1"></td>
                <td class="td-blank2"></td>
                <td class="td-blank3"></td>
                <td class="td-blank4"></td>
                <td class="td-blank5"></td>
            </tr>
            <tr>
                <td class="td-blank6"></td>
                <td class="td-lbl-serial-txtbox-item">
                    <asp:Label ID="Label1" runat="server" Text="serial used" CssClass="font-comman"></asp:Label>
                </td>
                <td class="td-blank6"></td>
                <td class="td-blank7"></td>
                <td class="td-blank8"></td>
            </tr>
            <tr>
                <td class="td-lbl-item-Qty">
                    <asp:Label ID="Label2" runat="server" Text="item no" CssClass="font-comman"></asp:Label>
                </td>
                <td class="td-lbl-serial">
                    <asp:TextBox ID="TextItemSerial" runat="server" Width="235px" CssClass="font-comman"></asp:TextBox>
                </td>
                <td class="td-blank6"></td>
                <td class="td-blank7"></td>
                <td class="td-blank8"></td>
            </tr>
            <tr>
                <td class="td-blank6"></td>
                <td class="td-blank7"></td>
                <td class="td-blank8"></td>
                <td class="td-chkbok-common-emty-txtbox">
                    <asp:CheckBox ID="ChkCarryOut" runat="server" AutoPostBack="True" CssClass="font-comman" />
                    <span class="font-comman">&nbsp;&nbsp;
                    </span>
                    <asp:Label ID="Label10" runat="server" CssClass="lbl-chkbox" Text="carry out from use"></asp:Label>
                </td>
                <td class="td-blank8"></td>
            </tr>
            <tr>
                <td class="td-blank6"></td>
                <td class="td-blank7"></td>
                <td class="td-blank8"></td>
                <td class="td-chkbok-common-emty-txtbox">
                    <asp:CheckBox ID="ChkUnusedBack" runat="server" AutoPostBack="True" CssClass="font-comman" />
                    <span class="font-comman">&nbsp;&nbsp;
                    </span>
                    <asp:Label ID="Label11" runat="server" CssClass="lbl-chkbox" Text="unused back"></asp:Label>
                </td>
                <td class="td-blank8"></td>
            </tr>
            <tr>
                <td class="td-blank6"></td>
                <td class="td-blank7"></td>
                <td class="td-blank8"></td>
                <td class="td-chkbok-common-emty-txtbox">
                    <asp:CheckBox ID="ChkReturnAfterUsed" runat="server" AutoPostBack="True" CssClass="font-comman" />
                    <span class="font-comman">&nbsp;&nbsp;
                    </span>
                    <asp:Label ID="Label12" runat="server" CssClass="lbl-chkbox" Text="Return after used"></asp:Label>
                </td>
                <td class="td-blank8"></td>
            </tr>
            <tr>
                <td class="td-blank6"></td>
                <td class="td-lbl-serial">
                    <asp:Label ID="Label3" runat="server" Text="serial unused" CssClass="font-comman"></asp:Label>
                </td>
                <td class="td-blank6"></td>
                <td class="td-blank8">
                    <asp:Label ID="Label13" runat="server" Text="Assigned to PO no" CssClass="font-comman"></asp:Label>
                </td>
                <td class="td-blank8"></td>
            </tr>
            <tr>
                <td class="td-lbl-item-Qty">
                    <asp:Label ID="Label4" runat="server" Text="item no" CssClass="font-comman"></asp:Label>
                </td>
                <td class="td-lbl-serial-txtbox-item">
                    <asp:TextBox ID="TextItem" runat="server" Width="235px" CssClass="font-comman"></asp:TextBox>
                </td>
                <td class="td-blank6"></td>
                <td class="td-chkbok-common-emty-txtbox">
                    <asp:TextBox ID="TextRepairNo" runat="server" Width="235px" CssClass="font-comman"></asp:TextBox>
                </td>
                <td class="td-blank8"></td>
            </tr>
            <tr>
                <td class="td-lbl-item-Qty">
                    <asp:Label ID="Label5" runat="server" Text="Qty" CssClass="font-comman"></asp:Label>
                </td>
                <td class="td-lbl-serial">
                    <asp:TextBox ID="TextQty" runat="server" Width="58px" CssClass="font-comman"></asp:TextBox>
                </td>
                <td class="td-blank1"></td>
                <td class="td-blank7">
                    &nbsp;</td>
                <td class="td-blank8"></td>
            </tr>
            <tr>
                <td></td>
                <td></td>
                <td></td>
                <td align ="right" class="td-blank10">
                    &nbsp;</td>
                <td>
                    <asp:ImageButton ID="btnSend" runat="server" Height="59px" ImageUrl="~/icon/send.png" Width="163px" CssClass="font-comman" />
                </td>
            </tr>
        </table>
      </div>
    </div>
    <div id="dialog" title="message" style="display:none;"> >
        <asp:Label ID="lblMsg" runat="server" Text=""></asp:Label>
    </div>

</asp:Content>
