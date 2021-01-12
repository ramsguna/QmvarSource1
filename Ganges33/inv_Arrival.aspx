<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Site3.Master" CodeBehind="inv_Arrival.aspx.vb" Inherits="Ganges33.inv_Arrival" %>
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
            height: 512px;
            width: 1255px;
            background-size: 100% auto;
            }
            .auto-style7 {
                height: 100%;
                background: rgba(255,255,255,0.8);
                /*opacity: 0.5;*/
            }
        .auto-style60 {
            width: 47%;
            height: 368px;
        }
        .auto-style62 {
            width: 877px;
            height: 20px;
            text-align: center;
        }
        .auto-style64 {
            width: 159px;
            height: 20px;
        }
        .auto-style76 {
            height: 50px;
            width: 374px;
            text-align: left;
        }
        .auto-style77 {
            height: 50px;
            width: 236px;
        }
        .auto-style79 {
            height: 25px;
            width: 374px;
        }
        .auto-style80 {
            height: 25px;
            width: 236px;
        }
        .auto-style88 {
            height: 22px;
            width: 374px;
        }
        .auto-style89 {
            height: 22px;
            width: 236px;
        }
        .auto-style91 {
            height: 23px;
            width: 374px;
        }
        .auto-style92 {
            height: 23px;
            width: 236px;
        }
        .auto-style95 {
            height: 28px;
            width: 374px;
        }
        .auto-style96 {
            height: 28px;
            width: 236px;
        }
        .auto-style100 {
            height: 32px;
        }
        .auto-style101 {
            height: 45px;
        }
        .auto-style103 {
            float: left;
            height: 454px;
            text-align:right;
        }
        .auto-style104 {
            float: left;
            height: 460px;
            width: 124px;
        }
        .auto-style105 {
            height: 59px;
        }
        .auto-style106 {
            height: 45px;
            width: 374px;
        }
        .auto-style116 {
            height: 464px;
        }
        .auto-style117 {
            height: 50px;
            width: 877px;
            text-align: center;
        }
        .auto-style119 {
            height: 22px;
            width: 877px;
        }
        .auto-style120 {
            height: 23px;
            width: 877px;
        }
        .auto-style121 {
            height: 28px;
            width: 877px;
        }
        .auto-style122 {
            height: 45px;
            width: 877px;
        }
        .auto-style123 {
            height: 32px;
            width: 877px;
            text-align: center;
        }
        .auto-style124 {
            height: 44px;
            width: 877px;
            text-align: center;
        }
        .auto-style125 {
            height: 25px;
            width: 877px;
            text-align: center;
        }
        .auto-style126 {
            height: 407px;
        }
        .auto-style127 {
            margin-top: 0px;
            font-family: "Meiryo UI";
        }
        .auto-style129 {
            width: 190px;
        }
        .auto-style131 {
            width: 291px;
            height: 39px;
            border-collapse: collapse;
            border-spacing: 0;
        }
        .auto-style132 {
            width: 374px;
        }
        .auto-style133 {
            font-family: "Meiryo UI";
        }
        .auto-style134 {
            font-family: "Meiryo UI";
            font-size: small;
        }
        .auto-style135 {
            height: 22px;
            width: 374px;
            font-family: "Meiryo UI";
        }
        .auto-style136 {
            height: 22px;
            width: 877px;
            font-family: "Meiryo UI";
        }
        .auto-style137 {
            height: 25px;
            width: 374px;
            font-family: "Meiryo UI";
        }
        .auto-style138 {
            height: 25px;
            width: 877px;
            text-align: center;
            font-family: "Meiryo UI";
        }
       </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="auto-style6" style="background-image: url('pagewall_2/wall_inventry-2.png')">
     <div class="auto-style7">
    <table class="auto-style60" align ="left">
        <tr>
            <td class="auto-style117">
                <asp:CheckBox ID="chkUseSerial" runat="server" AutoPostBack="True" CssClass="auto-style133" />
                <asp:Label ID="Label18" runat="server" Text="use Serial" CssClass="auto-style133"></asp:Label>
            </td>
            <td class="auto-style76">
                <asp:CheckBox ID="chkUnUseSerial" runat="server" AutoPostBack="True" CssClass="auto-style133" />
                <asp:Label ID="Label19" runat="server" Text="unuse Serial" CssClass="auto-style133"></asp:Label>
            </td>
            <td class="auto-style77">
                &nbsp;</td>
        </tr>
        <tr>
            <td class="auto-style125">
                <asp:Label ID="Label1" runat="server" Text="Delivery no" Width="134px" CssClass="auto-style133"></asp:Label>
            </td>
            <td class="auto-style79">
                <asp:TextBox ID="TextDeliveryNo" runat="server" Width="206px" CssClass="auto-style133"></asp:TextBox>
            </td>
            <td class="auto-style80"></td>
        </tr>
        <tr>
            <td class="auto-style138">
                &nbsp;</td>
            <td class="auto-style137">
                &nbsp;</td>
            <td class="auto-style80" rowspan ="2">
                <asp:ImageButton ID="BtnStart" runat="server" Height="59px" ImageUrl="~/icon/start.png" Width="163px" />
            </td>
        </tr>
        <tr>
            <td class="auto-style125">
                <asp:Label ID="Label2" runat="server" Text="invoice no" CssClass="auto-style133"></asp:Label>
            </td>
            <td class="auto-style79">
                <asp:TextBox ID="TextInvoiceNo" runat="server" Width="206px" CssClass="auto-style133"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td class="auto-style136">&nbsp;</td>
            <td class="auto-style135">&nbsp;</td>
            <td class="auto-style89"></td>
        </tr>
        <tr>
            <td class="auto-style120">
                <asp:Label ID="lblCurrentArrival" runat="server" Text="Current arrival number:" CssClass="auto-style134"></asp:Label>
            </td>
            <td class="auto-style91">
                <asp:Label ID="lblArrivalNnumber" runat="server" CssClass="auto-style133"></asp:Label>
            </td>
            <td class="auto-style92"></td>
        </tr>
        <tr>
            <td class="auto-style121">
                <asp:Label ID="lblCurrentDelivery" runat="server" Text="Current delivery number: " CssClass="auto-style134"></asp:Label>
            </td>
            <td class="auto-style95">
                <asp:Label ID="lblDeliveryNumber" runat="server" CssClass="auto-style133"></asp:Label>
            </td>
            <td class="auto-style96"></td>
        </tr>
        <tr>
            <td class="auto-style119"></td>
            <td class="auto-style88"></td>
            <td class="auto-style89"></td>
        </tr>
        <tr>
            <td class="auto-style62">
                <asp:Label ID="Label15" runat="server" Text="CSV Inport" CssClass="auto-style133"></asp:Label>
            </td>
            <td class="auto-style64" colspan ="2">
                <asp:FileUpload ID="FileUploadInv" runat="server" Width="319px" CssClass="auto-style133" />
                <asp:ImageButton ID="btnCsv" runat="server" Height="29px" ImageUrl="~/icon/folder-13.png" Width="36px" CssClass="auto-style133" />
            </td>
        </tr>
        <tr>
            <td class="auto-style122"></td>
            <td class="auto-style106"></td>
            <td class="auto-style101"></td>
        </tr>
        <tr>
            <td class="auto-style123">
                <asp:Label ID="Label16" runat="server" Text="item" CssClass="auto-style133"></asp:Label>
            </td>
            <td class="auto-style132" >
                <asp:TextBox ID="Textitem" runat="server" Width="206px" CssClass="auto-style133"></asp:TextBox>
            </td>
            <td class="auto-style100" rowspan ="2">
                <asp:ImageButton ID="btnImport" runat="server" Height="59px" ImageUrl="~/icon/import.png" Width="163px" />
            </td>
        </tr>
        <tr>
            <td class="auto-style124">
                <asp:Label ID="Label17" runat="server" Text="Qty" CssClass="auto-style133"></asp:Label>
            </td>
            <td class="auto-style132" >
                <asp:TextBox ID="TextQty" runat="server" Width="58px" CssClass="auto-style133"></asp:TextBox>
            </td>
        </tr>
    </table>
    <table class="auto-style104">
        <tr>
            <td class="auto-style116"></td>
        </tr>
    </table>
    <table class="auto-style103" >
        <tr>
            <td class="auto-style126">
                <asp:ListBox ID="ListHistory" runat="server" Height="379px" Width="511px" CssClass="auto-style127"></asp:ListBox>
            </td>
        </tr>
        <tr>
            <td align ="right" class="auto-style105">
                <asp:ImageButton ID="btnClose" runat="server" Height="59px" ImageUrl="~/icon/close.png" Width="163px" />
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
