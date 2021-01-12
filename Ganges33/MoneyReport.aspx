<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Site4.Master" CodeBehind="MoneyReport.aspx.vb" Inherits="Ganges33.MoneyReport" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
        <script src="http://ajax.googleapis.com/ajax/libs/jquery/1.7.0/jquery.min.js"></script>
    <link type="text/css" href="http://ajax.googleapis.com/ajax/libs/jqueryui/1/themes/start/jquery-ui.css" rel="stylesheet">
    <script type="text/javascript" src="http://ajax.googleapis.com/ajax/libs/jqueryui/1/jquery-ui.min.js"></script>
        <style type="text/css">
        .auto-style6 {
            z-index: 1;
            left: 40px;
            top: 118px;
            position: absolute;
            height: 512px;
            width: 1250px;
            background-size: contain;
            }
            .auto-style7 {
                height: 100%;
                background: rgba(255,255,255,0.8);
                /*opacity: 0.5;*/
            }
            .auto-style135 {
            }
            .auto-style136 {
                height: 38px;
                width: 89px;
            }
            .auto-style137 {
                height: 53px;
                width: 89px;
            }
            .auto-style150 {
                height: 24px;
                width: 89px;
            }
            .auto-style154 {
                height: 20px;
                width: 89px;
            }
            .auto-style155 {
                width: 89px;
            }
            .auto-style216 {
                height: 38px;
                width: 120px;
            }
            .auto-style217 {
                height: 53px;
                width: 120px;
            }
            .auto-style218 {
                width: 120px;
                height: 24px;
            }
            .auto-style219 {
                width: 120px;
            }
            .auto-style220 {
                width: 120px;
                height: 20px;
            }
            .auto-style221 {
                height: 38px;
                width: 116px;
            }
            .auto-style222 {
                height: 53px;
                width: 116px;
            }
            .auto-style223 {
                width: 116px;
                height: 24px;
            }
            .auto-style224 {
                width: 116px;
            }
            .auto-style225 {
                width: 116px;
                height: 20px;
            }
            .auto-style266 {
                height: 38px;
                width: 148px;
            }
            .auto-style267 {
                height: 53px;
                width: 148px;
            }
            .auto-style268 {
                width: 148px;
                height: 24px;
            }
            .auto-style269 {
                width: 148px;
            }
            .auto-style270 {
                width: 148px;
                height: 20px;
            }
            .auto-style271 {
                height: 38px;
                width: 153px;
            }
            .auto-style272 {
                height: 53px;
                width: 153px;
            }
            .auto-style273 {
                width: 153px;
                height: 24px;
            }
            .auto-style274 {
                width: 153px;
            }
            .auto-style275 {
                width: 153px;
                height: 20px;
            }
            .auto-style276 {
                height: 38px;
                width: 161px;
            }
            .auto-style277 {
                height: 53px;
                width: 161px;
            }
            .auto-style278 {
                width: 161px;
                height: 24px;
            }
            .auto-style279 {
                width: 161px;
            }
            .auto-style280 {
                width: 161px;
                height: 20px;
            }
            .auto-style281 {
                height: 38px;
                width: 160px;
            }
            .auto-style282 {
                height: 53px;
                width: 160px;
            }
            .auto-style283 {
                width: 160px;
                height: 24px;
            }
            .auto-style284 {
                width: 160px;
            }
            .auto-style285 {
                width: 160px;
                height: 20px;
            }
            .auto-style286 {
                height: 38px;
                width: 156px;
            }
            .auto-style287 {
                height: 53px;
                width: 156px;
            }
            .auto-style288 {
                width: 156px;
                height: 24px;
            }
            .auto-style289 {
                width: 156px;
            }
            .auto-style290 {
                width: 156px;
                height: 20px;
            }
          </style> 
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
  <div class="auto-style6" style="background-image: url('pagewall_2/wall_money-2.png')">
    <div class="auto-style7">

       
        <table class="auto-style135">
            <tr>
                <td class="auto-style271"></td>
                <td class="auto-style276"></td>
                <td class="auto-style266"></td>
                <td class="auto-style281"></td>
                <td class="auto-style286"></td>
                <td class="auto-style216"></td>
                <td class="auto-style221"></td>
                <td class="auto-style136"></td>
            </tr>
            <tr>
                <td class="auto-style272"></td>
                <td class="auto-style277"></td>
                <td class="auto-style267"></td>
                <td class="auto-style282"></td>
                <td class="auto-style287"></td>
                <td class="auto-style217"></td>
                <td class="auto-style222"></td>
                <td class="auto-style137"></td>
            </tr>
            <tr>
                <td class="auto-style273"></td>
                <td class="auto-style278"></td>
                <td class="auto-style268"></td>
                <td class="auto-style283"></td>
                <td class="auto-style288"></td>
                <td class="auto-style218"></td>
                <td class="auto-style223"></td>
                <td class="auto-style150"></td>
            </tr>
            <tr>
                <td class="auto-style274">&nbsp;</td>
                <td class="auto-style279">&nbsp;</td>
                <td class="auto-style269">&nbsp;</td>
                <td class="auto-style284">&nbsp;</td>
                <td class="auto-style289">&nbsp;</td>
                <td class="auto-style219">&nbsp;</td>
                <td class="auto-style224">&nbsp;</td>
                <td class="auto-style155">&nbsp;</td>
            </tr>
            <tr>
                <td class="auto-style274">&nbsp;</td>
                <td class="auto-style279">&nbsp;</td>
                <td class="auto-style269">&nbsp;</td>
                <td class="auto-style284">&nbsp;</td>
                <td class="auto-style289">&nbsp;</td>
                <td class="auto-style219">&nbsp;</td>
                <td class="auto-style224">&nbsp;</td>
                <td class="auto-style155">&nbsp;</td>
            </tr>
            <tr>
                <td class="auto-style274">&nbsp;</td>
                <td class="auto-style279">&nbsp;</td>
                <td class="auto-style269">&nbsp;</td>
                <td class="auto-style284">&nbsp;</td>
                <td class="auto-style289">&nbsp;</td>
                <td class="auto-style219">&nbsp;</td>
                <td class="auto-style224">&nbsp;</td>
                <td class="auto-style155">&nbsp;</td>
            </tr>
            <tr>
                <td class="auto-style274">&nbsp;</td>
                <td class="auto-style279">&nbsp;</td>
                <td class="auto-style269">&nbsp;</td>
                <td class="auto-style284">&nbsp;</td>
                <td class="auto-style289">&nbsp;</td>
                <td class="auto-style219">&nbsp;</td>
                <td class="auto-style224">&nbsp;</td>
                <td class="auto-style155">&nbsp;</td>
            </tr>
            <tr>
                <td class="auto-style274">&nbsp;</td>
                <td class="auto-style279">&nbsp;</td>
                <td class="auto-style269">&nbsp;</td>
                <td class="auto-style284">&nbsp;</td>
                <td class="auto-style289">&nbsp;</td>
                <td class="auto-style219">&nbsp;</td>
                <td class="auto-style224">&nbsp;</td>
                <td class="auto-style155">&nbsp;</td>
            </tr>
            <tr>
                <td class="auto-style274">&nbsp;</td>
                <td class="auto-style279">&nbsp;</td>
                <td class="auto-style269">&nbsp;</td>
                <td class="auto-style284">&nbsp;</td>
                <td class="auto-style289">&nbsp;</td>
                <td class="auto-style219">&nbsp;</td>
                <td class="auto-style224">&nbsp;</td>
                <td class="auto-style155">&nbsp;</td>
            </tr>
            <tr>
                <td class="auto-style275"></td>
                <td class="auto-style280"></td>
                <td class="auto-style270"></td>
                <td class="auto-style285"></td>
                <td class="auto-style290"></td>
                <td class="auto-style220"></td>
                <td class="auto-style225"></td>
                <td class="auto-style154"></td>
            </tr>
            <tr>
                <td class="auto-style274">&nbsp;</td>
                <td class="auto-style279">&nbsp;</td>
                <td class="auto-style269">&nbsp;</td>
                <td class="auto-style284">&nbsp;</td>
                <td class="auto-style289">&nbsp;</td>
                <td class="auto-style219">&nbsp;</td>
                <td class="auto-style224">&nbsp;</td>
                <td class="auto-style155">&nbsp;</td>
            </tr>
            <tr>
                <td class="auto-style274">&nbsp;</td>
                <td class="auto-style279">&nbsp;</td>
                <td class="auto-style269">&nbsp;</td>
                <td class="auto-style284">&nbsp;</td>
                <td class="auto-style289">&nbsp;</td>
                <td class="auto-style219">&nbsp;</td>
                <td class="auto-style224">&nbsp;</td>
                <td class="auto-style155">&nbsp;</td>
            </tr>
        </table>

       
    </div>
  </div>
  <div id="dialog" title="message" style="display:none;"> >
        <asp:Label ID="lblMsg" runat="server" Text=""></asp:Label>
  </div>

</asp:Content>
