<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Site2.Master" CodeBehind="Menu.aspx.vb" Inherits="Ganges33.Menu" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script src="http://ajax.googleapis.com/ajax/libs/jquery/1.7.0/jquery.min.js"></script>
    <link type="text/css" href="http://ajax.googleapis.com/ajax/libs/jqueryui/1/themes/start/jquery-ui.css" rel="stylesheet">
    <script type="text/javascript" src="http://ajax.googleapis.com/ajax/libs/jqueryui/1/jquery-ui.min.js"></script>
    <style type="text/css">
        .auto-style6 {
            z-index: 1;
            left: 57px;
            top: 70px;
            position: absolute;
            height: 615px;
            width: 1250px;
        }
        .auto-style7 {
            width: 100%;
            height: 554px;
        }
        .auto-style17 {
            width: 142px;
            height: 56px;
        }
        .auto-style27 {
            width: 142px;
            height: 101px;
        }
        .auto-style36 {
            font-family: "Meiryo UI";
            font-size: x-large;
        }
        .auto-style38 {
            width: 183px;
            height: 56px;
            font-weight: bold;
        }
        .auto-style41 {
            width: 215px;
            height: 56px;
            font-weight: bold;
        }
        .auto-style45 {
            width: 183px;
            height: 101px;
            font-weight: bold;
        }
        .auto-style46 {
            width: 215px;
            height: 101px;
            font-weight: bold;
        }
        .auto-style47 {
            width: 142px;
            height: 79px;
        }
        .auto-style48 {
            width: 183px;
            height: 79px;
            font-weight: bold;
        }
        .auto-style49 {
            width: 215px;
            height: 79px;
            font-weight: bold;
        }
        .auto-style50 {
            height: 79px;
        }
        .auto-style51 {
            width: 142px;
            height: 84px;
        }
        .auto-style53 {
            width: 142px;
            height: 38px;
        }
        .auto-style54 {
            width: 183px;
            height: 38px;
            font-weight: bold;
        }
        .auto-style55 {
            width: 215px;
            height: 38px;
            font-weight: bold;
        }
        .auto-style57 {
            margin-top: 0px;
        }
        .auto-style58 {
            font-family: "Meiryo UI";
        }
        </style>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="auto-style6">
       

        <table class="auto-style7">
            <tr>
                <td class="auto-style47">
                    <asp:ImageButton ID="btnRepair" runat="server" Height="108px" ImageUrl="~/icon/repair.png" Width="108px" />
                </td>
                <td class="auto-style48">
                    <asp:Label ID="Label1" runat="server" CssClass="auto-style36" Text="Repair"></asp:Label>
                </td>
                <td class="auto-style47">
                    <asp:ImageButton ID="btnPerson" runat="server" Height="108px" ImageUrl="~/icon/person.png" />
                </td>
                <td class="auto-style49">
                    <asp:Label ID="lblPerson" runat="server" CssClass="auto-style36" Text="Person"></asp:Label>
                </td>
                <td class="auto-style50" rowspan = "5">
                    <asp:Label ID="Label7" runat="server" Text="News" CssClass="auto-style58"></asp:Label>　<br />
                    <asp:TextBox ID="TextBox1" runat="server" Height="519px" TextMode="MultiLine" Width="505px"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td class="auto-style27">
                    <asp:ImageButton ID="btnInventory" runat="server" Height="108px" ImageUrl="~/icon/inventory.png" Width="108px" />
                </td>
                <td class="auto-style45">
                    <asp:Label ID="Label2" runat="server" CssClass="auto-style36" Text="Inventory"></asp:Label>
                </td>
                <td class="auto-style27">
                    <asp:ImageButton ID="btnAnalysis" runat="server" Height="108px" ImageUrl="~/icon/analysis.png" Width="108px" />
                </td>
                <td class="auto-style46">
                    <asp:Label ID="Label6" runat="server" CssClass="auto-style36" Text="Analysis"></asp:Label>
                </td>
            </tr>
            <tr>
                <td class="auto-style17">
                    <asp:ImageButton ID="btnMoney" runat="server" Height="108px" ImageUrl="~/icon/money.png" Width="108px" />
                </td>
                <td class="auto-style38">
                    <asp:Label ID="Label3" runat="server" CssClass="auto-style36" Text="Money"></asp:Label>
                </td>
                <td class="auto-style17">
                    <asp:ImageButton ID="btnSystem" runat="server" Height="108px" ImageUrl="~/icon/system.png" />
                </td>
                <td class="auto-style41">
                    <asp:Label ID="lblSystem" runat="server" CssClass="auto-style36" Text="System"></asp:Label>
                </td>
            </tr>
            <tr>
                <td class="auto-style53">
                    <asp:ImageButton ID="BtnDailyReport" runat="server" Height="108px" ImageUrl="~/icon/daylyreport.png" Width="108px" />
                </td>
                <td class="auto-style54">
                    <asp:Label ID="Label4" runat="server" CssClass="auto-style36" Text="Daily Report"></asp:Label>
                </td>
                <td class="auto-style53">
                    <asp:ImageButton ID="BtnMVAR" runat="server" Height="108px" ImageUrl="~/icon/nmvar.png" />
                </td>
                <td class="auto-style55">
                    <asp:Label ID="lblnMVAR" runat="server" CssClass="auto-style36" Text="nMVAR connect"></asp:Label>
                </td>
            </tr>
          


            <tr>
                <td class="auto-style51" colspan = "4">
                    <asp:Label ID="Label5" runat="server" Text="Information" CssClass="auto-style58"></asp:Label> <br />
                    <asp:TextBox ID="TextTodayMsg" runat="server" Height="65px" Width="684px" CssClass="auto-style57" TextMode="MultiLine" ReadOnly="True"></asp:TextBox>
                </td>
            </tr>
        </table>
       

    </div>

    <div id="dialog" title="message" style="display:none;"> >
        <asp:Label ID="lblMsg" runat="server" Text=""></asp:Label>
    </div>

</asp:Content>
