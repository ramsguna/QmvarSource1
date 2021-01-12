<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Site4.Master" CodeBehind="Money_Salse.aspx.vb" Inherits="Ganges33.Money_Salse" %>
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
            background-size: contain;
            }
            .auto-style7 {
                height: 100%;
                background: rgba(255,255,255,0.8);
                /*opacity: 0.5;*/
            }

            .auto-style135 {
                width: 100%;
                height: 167px;
            }
            .auto-style136 {
                width: 150px;
            }
            .auto-style137 {
                width: 484px;
            }

        .auto-style138 {
        font-size: xx-large;
        font-family: "Meiryo UI";
    }
    .auto-style139 {
        font-family: "Meiryo UI";
    }
    .auto-style140 {
        width: 150px;
        height: 42px;
    }
    .auto-style141 {
        width: 484px;
        height: 42px;
    }
    .auto-style142 {
        height: 42px;
    }

            .auto-style186 {
                font-family: "Meiryo UI";
            }
            
        </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="auto-style6" style="background-image: url('pagewall_2/wall_money-2.png')">
       <div class="auto-style7">

           <table class="auto-style135">
               <tr>
                   <td class="auto-style136">&nbsp;</td>
                   <td class="auto-style137">&nbsp;</td>
                   <td>&nbsp;</td>
               </tr>
               <tr>
                   <td class="auto-style136">&nbsp;</td>
                   <td class="auto-style137" rowspan = "2">
                       <asp:Label ID="Label3" runat="server" CssClass="auto-style138" Text="Sales Report"></asp:Label>
                   </td>
                   <td>
                       <asp:Label ID="Label1" runat="server" Text="current Location :" CssClass="auto-style139"></asp:Label>
                   &nbsp;<asp:Label ID="lblShipInfo" runat="server" CssClass="auto-style139"></asp:Label>
                   </td>
               </tr>
               <tr>
                   <td class="auto-style136">&nbsp;</td>
                   <td>
                       <asp:Label ID="Label2" runat="server" Text="current username :" CssClass="auto-style139"></asp:Label>
                   &nbsp;<asp:Label ID="lblName" runat="server" CssClass="auto-style139"></asp:Label>
                   </td>
               </tr>
               <tr>
                   <td class="auto-style140"></td>
                   <td class="auto-style141">
                       <asp:Label ID="Label4" runat="server" CssClass="auto-style139" Text="Target store"></asp:Label>
&nbsp;<asp:DropDownList ID="DropListLocation" runat="server" CssClass="auto-style139" Height="28px" Width="259px">
                       </asp:DropDownList>
                   </td>
                   <td class="auto-style142">
                       &nbsp;</td>
               </tr>
               <tr>
                   <td class="auto-style140"></td>
                   <td class="auto-style141">
                       <asp:Label ID="Label5" runat="server" CssClass="auto-style139" Text="Target Date"></asp:Label>
&nbsp;<asp:TextBox ID="TextTargetDate" runat="server" CssClass="auto-style139" Height="16px" Width="133px"></asp:TextBox>
                   &nbsp;
                       <asp:Label ID="Label6" runat="server" CssClass="auto-style139" Text="yyyy/mm/dd"></asp:Label>
                   </td>
                   <td class="auto-style142">
                    <asp:ImageButton ID="btnSearch" runat="server" Height="29px" ImageUrl="~/icon/search.png" Width="81px" CssClass="auto-style186" />
                   </td>
               </tr>
           </table>

       </div>
    </div>

    <div id="dialog" title="message" style="display:none;"> >
       <asp:Label ID="lblMsg" runat="server" Text=""></asp:Label>
    </div>

</asp:Content>
