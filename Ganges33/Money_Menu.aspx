<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Site4.Master" CodeBehind="Money_Menu.aspx.vb" Inherits="Ganges33.Money_Menu" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script src="http://ajax.googleapis.com/ajax/libs/jquery/1.7.0/jquery.min.js"></script>
    <link type="text/css" href="http://ajax.googleapis.com/ajax/libs/jqueryui/1/themes/start/jquery-ui.css" rel="stylesheet">
    <script type="text/javascript" src="http://ajax.googleapis.com/ajax/libs/jqueryui/1/jquery-ui.min.js"></script>
    <link type="text/css" href="CSS/Common/Money_Menu.css" rel="stylesheet" />    
    <style type="text/css">
       

        </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="div-bg-image" style="background-image: url('pagewall_2/wall_money-2.png')">
        <div class="div-entirepage">
          <table class="tabl-menus" align ="left">
            <tr>  
                <td class="td-image-btn-changereverse-td-image-btn-Aggregation-td-image-btn-control">
                    <asp:ImageButton ID="btnChange" runat="server" ImageUrl="~/icon/M_changereserve.png" CssClass="btn-image-changereserve-btn-image-aggregation-btn-image-control" />
                </td><td class="td-lbl-changereserve-td-lbl-aggregation-td-lbl-control">
                    <asp:Label ID="Label5" runat="server" Text="Change Reserve" CssClass="lbl-txt-changereserve-lbl-txt-aggregation-lbl-txt-control"></asp:Label>
                </td>
            </tr>
            <tr>
                <td class="td-image-btn-changereverse-td-image-btn-Aggregation-td-image-btn-control">
                    <asp:ImageButton ID="btnAggregation" runat="server" ImageUrl="~/icon/M_inspection.png" CssClass="btn-image-changereserve-btn-image-aggregation-btn-image-control" />
                </td><td class="td-lbl-changereserve-td-lbl-aggregation-td-lbl-control">
                    <asp:Label ID="Label6" runat="server" Text="Date&amp;Time aggregation" CssClass="lbl-txt-changereserve-lbl-txt-aggregation-lbl-txt-control"></asp:Label>
                </td>
            </tr>
            <tr>
                <td class="td-image-btn-changereverse-td-image-btn-Aggregation-td-image-btn-control">
                    <asp:ImageButton ID="btnControl" runat="server" ImageUrl="~/icon/M_control.png" CssClass="btn-image-changereserve-btn-image-aggregation-btn-image-control" />
                </td><td class="td-lbl-changereserve-td-lbl-aggregation-td-lbl-control">
                    <asp:Label ID="Label7" runat="server" Text="Sales Control" CssClass="lbl-txt-changereserve-lbl-txt-aggregation-lbl-txt-control"></asp:Label>
                </td>
            </tr>
            <tr>
                <td class="td-blank1">
                    &nbsp;</td><td class="td-blank2">
                    &nbsp;</td>
            </tr>
        </table>
        <table aligin ="right"><tr><td class="td-news">News</td></tr></table>
        <table aligin ="right" class="tabl-news">
            <tr>
                <td class="td-lbl-news" align ="top">
                    <asp:Label ID="lblNews" runat="server" BorderStyle="Solid"  CssClass="lbl-txt-news"></asp:Label>
                </td>
            </tr>
        </table>
      </div>
    </div>
    <div id="dialog" title="message" style="display:none;"> >
        <asp:Label ID="lblMsg" runat="server" Text=""></asp:Label>
    </div>

</asp:Content>
