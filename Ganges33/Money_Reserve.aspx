<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Site4.Master" CodeBehind="Money_Reserve.aspx.vb" Inherits="Ganges33.Money_Reserve1" %>
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
            height: 526px;
            width: 1255px;
            background-size: 100% auto;
            }
            .auto-style7 {
                height: 100%;
                background: rgba(255,255,255,0.8);
                /*opacity: 0.5;*/
            }
            .auto-style222 {
                font-size: large;
                font-family: "Meiryo UI";
                font-weight: bold;
            }
            .auto-style223 {
                width: 179px;
                font-family: "Meiryo UI";
                font-size: large;
                font-weight: bold;
            }
            .auto-style224 {
                font-family: "Meiryo UI";
            }
            .auto-style225 {
                width: 100%;
            }
            .auto-style228 {
                width: 41px;
                height: 20px;
            }
            .auto-style231 {
                width: 41px;
                height: 24px;
            }
            .auto-style240 {
                width: 41px;
                height: 20px;
                font-family: "Meiryo UI";
            }
            .auto-style241 {
                width: 103px;
                font-family: "Meiryo UI";
                height: 13px;
            }
            .auto-style248 {
                height: 17px;
                width: 103px;
            }
            .auto-style249 {
                width: 41px;
                height: 13px;
            }
            .auto-style250 {
                width: 72px;
                height: 13px;
            }
            .auto-style251 {
                height: 13px;
                width: 168px;
            }
            .auto-style252 {
                height: 13px;
            }
            .auto-style253 {
                width: 41px;
                height: 22px;
            }
            .auto-style254 {
                width: 72px;
                height: 22px;
            }
            .auto-style255 {
                height: 22px;
                width: 103px;
            }
            .auto-style256 {
                height: 22px;
                width: 168px;
            }
            .auto-style257 {
                height: 22px;
            }
            .auto-style229 {
                font-size: large;
                font-family: "Meiryo UI";
            }
            .auto-style258 {
                width: 72px;
                font-family: "Meiryo UI";
                font-size: large;
            }
            .auto-style259 {
                width: 41px;
                height: 17px;
            }
            .auto-style260 {
                width: 72px;
                height: 17px;
            }
            .auto-style261 {
                height: 17px;
                width: 168px;
            }
            .auto-style262 {
                height: 17px;
            }
            .auto-style263 {
                font-family: "Meiryo UI";
                font-size: small;
            }
            </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="auto-style6" style="background-image: url('pagewall_2/wall_money-2.png')">
      <div class="auto-style7">
        <asp:Label ID="Label1" runat="server" CssClass="auto-style223" Text="Status: "></asp:Label>
        <asp:Label ID="lblDispStatu" runat="server" CssClass="auto-style222"></asp:Label>
        <br />
        <asp:DropDownList ID="DropListStatus" runat="server" Height="32px" Width="307px" CssClass="auto-style224"></asp:DropDownList>
        <br /> 
            <table class="auto-style225">
              <tr>
                  <td class="auto-style259">
                      <asp:Label ID="Label2" runat="server" CssClass="auto-style224" Text="bill"></asp:Label>
                  </td>
                  <td class="auto-style260"></td>
                  <td class="auto-style248"></td>
                  <td class="auto-style261"></td>
                  <td class="auto-style262"></td>
              </tr>
              <tr>
                  <td class="auto-style253"></td>
                  <td class="auto-style254">
                      <asp:Label ID="Label3" runat="server" CssClass="auto-style224" Text="2000"></asp:Label>
                  </td>
                  <td class="auto-style255">
                      <asp:TextBox ID="TextM2000Cnt" runat="server" CssClass="auto-style224" Height="18px" Width="73px"></asp:TextBox>
                  </td>
                  <td class="auto-style256">
                      <asp:TextBox ID="TextM2000Sum" runat="server" Height="18px"></asp:TextBox>
                  </td>
                  <td class="auto-style257">
                    <strong class="auto-style231">
                    <asp:Label ID="Label18" runat="server" CssClass="auto-style258" Text="Open Deposit"></asp:Label>
                    </strong>

                  </td>
              </tr>
              <tr>
                  <td class="auto-style253"></td>
                  <td class="auto-style254">
                      <asp:Label ID="Label4" runat="server" CssClass="auto-style224" Text="500"></asp:Label>
                  </td>
                  <td class="auto-style255">
                      <asp:TextBox ID="TextM500Cnt" runat="server" CssClass="auto-style224" Height="18px" Width="73px"></asp:TextBox>
                  </td>
                  <td class="auto-style256">
                      <asp:TextBox ID="TextM500Sum" runat="server" Height="18px"></asp:TextBox>
                  </td>
                  <td class="auto-style257">
                    <asp:Label ID="lblResult" runat="server" CssClass="auto-style224" ForeColor="Black" ></asp:Label>&nbsp;&nbsp;
                    <asp:Label ID="lblName" runat="server" CssClass="auto-style240"></asp:Label>&nbsp;&nbsp;
                    <asp:Label ID="lblTime" runat="server" CssClass="auto-style240"></asp:Label>&nbsp;&nbsp;
                    <asp:Label ID="lblRegiDeposi" runat="server" CssClass="auto-style240"></asp:Label>&nbsp;&nbsp;
                    <asp:Label ID="lblmistake0" runat="server" CssClass="auto-style240">mistake : </asp:Label>
                    <asp:Label ID="lblmistakeOpen" runat="server" CssClass="auto-style240"></asp:Label>&nbsp;&nbsp;
                      <asp:Label ID="lblConfirm" runat="server" CssClass="auto-style224"></asp:Label>
                  </td>
              </tr>
              <tr>
                  <td class="auto-style253"></td>
                  <td class="auto-style254">
                      <asp:Label ID="Label5" runat="server" CssClass="auto-style224" Text="200"></asp:Label>
                  </td>
                  <td class="auto-style255">
                      <asp:TextBox ID="TextM200Cnt" runat="server" CssClass="auto-style224" Height="18px" Width="73px"></asp:TextBox>
                  </td>
                  <td class="auto-style256">
                      <asp:TextBox ID="TextM200Sum" runat="server" Height="18px"></asp:TextBox>
                  </td>
                  <td class="auto-style257">
                      <asp:Label ID="lblConfirm2" runat="server" CssClass="auto-style224"></asp:Label>
                  </td>
              </tr>
              <tr>
                  <td class="auto-style253"></td>
                  <td class="auto-style254">
                      <asp:Label ID="Label6" runat="server" CssClass="auto-style224" Text="100"></asp:Label>
                  </td>
                  <td class="auto-style255">
                      <asp:TextBox ID="TextM100Cnt" runat="server" CssClass="auto-style224" Height="18px" Width="73px"></asp:TextBox>
                  </td>
                  <td class="auto-style256">
                      <asp:TextBox ID="TextM100Sum" runat="server" Height="18px"></asp:TextBox>
                  </td>
                  <td class="auto-style257">
                    <strong class="auto-style231">
                    <asp:Label ID="Label19" runat="server" CssClass="auto-style258" Text="Credit Report"></asp:Label>
                    </strong>
                  </td>
              </tr>
              <tr>
                  <td class="auto-style253"></td>
                  <td class="auto-style254">
                      <asp:Label ID="Label7" runat="server" CssClass="auto-style224" Text="50"></asp:Label>
                  </td>
                  <td class="auto-style255">
                      <asp:TextBox ID="TextM50Cnt" runat="server" CssClass="auto-style224" Height="18px" Width="73px"></asp:TextBox>
                  </td>
                  <td class="auto-style256">
                      <asp:TextBox ID="TextM50Sum" runat="server" Height="18px"></asp:TextBox>
                  </td>
                  <td class="auto-style257">
                    <asp:Label ID="lblResultIns1" runat="server" CssClass="auto-style229"></asp:Label>&nbsp;&nbsp;
                    <asp:Label ID="lblName0" runat="server" CssClass="auto-style240"></asp:Label>&nbsp;&nbsp;
                    <asp:Label ID="lblTime0" runat="server" CssClass="auto-style240"></asp:Label>&nbsp;&nbsp;
                    <asp:Label ID="lblDiff0" runat="server" CssClass="auto-style240"></asp:Label>&nbsp;&nbsp;
                    <asp:Label ID="lblmistake1" runat="server" CssClass="auto-style240">mistake : </asp:Label>
                    <asp:Label ID="lblmistakeIns1" runat="server" CssClass="auto-style240"></asp:Label>&nbsp;&nbsp;
                    <asp:Label ID="lblCIns1" runat="server" CssClass="auto-style224"></asp:Label>&nbsp;&nbsp;
                    <asp:Label ID="lblCIns1_" runat="server" CssClass="auto-style224"></asp:Label>
                  </td>
              </tr>
              <tr>
                  <td class="auto-style253"></td>
                  <td class="auto-style254">
                      <asp:Label ID="Label8" runat="server" CssClass="auto-style224" Text="20"></asp:Label>
                  </td>
                  <td class="auto-style255">
                      <asp:TextBox ID="TextM20Cnt" runat="server" CssClass="auto-style224" Height="18px" Width="73px"></asp:TextBox>
                  </td>
                  <td class="auto-style256">
                      <asp:TextBox ID="TextM20Sum" runat="server" Height="18px"></asp:TextBox>
                  </td>
                  <td class="auto-style257">
                    <asp:Label ID="lblResultIns2" runat="server" CssClass="auto-style229"></asp:Label>&nbsp;&nbsp;
                    <asp:Label ID="lblName1" runat="server" CssClass="auto-style240"></asp:Label>&nbsp;&nbsp;
                    <asp:Label ID="lblTime1" runat="server" CssClass="auto-style240"></asp:Label>&nbsp;&nbsp;
                    <asp:Label ID="lblDiff1" runat="server" CssClass="auto-style240"></asp:Label>&nbsp;&nbsp;
                    <asp:Label ID="lblmistake2" runat="server" CssClass="auto-style240">mistake : </asp:Label>
                    <asp:Label ID="lblmistakeIns2" runat="server" CssClass="auto-style240"></asp:Label>&nbsp;&nbsp;
                    <asp:Label ID="lblCIns2" runat="server" CssClass="auto-style224"></asp:Label>
                  &nbsp;&nbsp;
                    <asp:Label ID="lblCIns2_" runat="server" CssClass="auto-style224"></asp:Label>
                  </td>
              </tr>
              <tr>
                  <td class="auto-style253"></td>
                  <td class="auto-style254">
                      <asp:Label ID="Label9" runat="server" CssClass="auto-style224" Text="10"></asp:Label>
                  </td>
                  <td class="auto-style255">
                      <asp:TextBox ID="TextM10Cnt" runat="server" CssClass="auto-style224" Height="18px" Width="73px"></asp:TextBox>
                  </td>
                  <td class="auto-style256">
                      <asp:TextBox ID="TextM10Sum" runat="server" Height="18px"></asp:TextBox>
                  </td>
                  <td class="auto-style257">
                    <asp:Label ID="lblResultIns3" runat="server" CssClass="auto-style229"></asp:Label>&nbsp;&nbsp;
                    <asp:Label ID="lblName2" runat="server" CssClass="auto-style240"></asp:Label>&nbsp;&nbsp;
                    <asp:Label ID="lblTime2" runat="server" CssClass="auto-style240"></asp:Label>&nbsp;&nbsp;
                    <asp:Label ID="lblDiff2" runat="server" CssClass="auto-style240"></asp:Label>&nbsp;&nbsp;
                    <asp:Label ID="lblmistake3" runat="server" CssClass="auto-style240">mistake : </asp:Label>
                    <asp:Label ID="lblmistakeIns3" runat="server" CssClass="auto-style240"></asp:Label>&nbsp;&nbsp;
                    <asp:Label ID="lblCIns3" runat="server" CssClass="auto-style224"></asp:Label>&nbsp;&nbsp;
                    <asp:Label ID="lblCIns3_" runat="server" CssClass="auto-style224"></asp:Label>
                  </td>
              </tr>
              <tr>
                  <td class="auto-style249">
                      <asp:Label ID="Label10" runat="server" CssClass="auto-style224" Text="coin"></asp:Label>
                  </td>
                  <td class="auto-style250"></td>
                  <td class="auto-style241"></td>
                  <td class="auto-style251"></td>
                  <td class="auto-style252"></td>
              </tr>
              <tr>
                  <td class="auto-style253"></td>
                  <td class="auto-style254">
                      <asp:Label ID="Label13" runat="server" CssClass="auto-style224" Text="10"></asp:Label>
                  </td>
                  <td class="auto-style255">
                      <asp:TextBox ID="TextCoin10Cnt" runat="server" CssClass="auto-style224" Height="18px" Width="73px"></asp:TextBox>
                  </td>
                  <td class="auto-style256">
                      <asp:TextBox ID="TextCoin10Sum" runat="server" Height="18px"></asp:TextBox>
                  </td>
                  <td class="auto-style257">
                     <strong class="auto-style231">
                     <asp:Label ID="Label20" runat="server" CssClass="auto-style258" Text="Checkout"></asp:Label>
                     </strong>
                  </td>
              </tr>
              <tr>
                  <td class="auto-style253"></td>
                  <td class="auto-style254">
                      <asp:Label ID="Label14" runat="server" CssClass="auto-style224" Text="5"></asp:Label>
                  </td>
                  <td class="auto-style255">
                      <asp:TextBox ID="TextCoin5Cnt" runat="server" CssClass="auto-style224" Height="18px" Width="73px"></asp:TextBox>
                  </td>
                  <td class="auto-style256">
                      <asp:TextBox ID="TextCoin5Sum" runat="server" Height="18px"></asp:TextBox>
                  </td>
                  <td class="auto-style257">
                    <asp:Label ID="lblResultClose" runat="server" CssClass="auto-style229"></asp:Label>&nbsp;&nbsp;
                    <asp:Label ID="lblName3" runat="server" CssClass="auto-style240"></asp:Label>&nbsp;&nbsp;
                    <asp:Label ID="lblTime3" runat="server" CssClass="auto-style240"></asp:Label>&nbsp;&nbsp;
                    <asp:Label ID="lblDiff3" runat="server" CssClass="auto-style240"></asp:Label>&nbsp;&nbsp;
                    <asp:Label ID="lblmistake4" runat="server" CssClass="auto-style240">mistake : </asp:Label>
                    <asp:Label ID="lblmistakeClose" runat="server" CssClass="auto-style240"></asp:Label>&nbsp;&nbsp;
                    <asp:Label ID="lblConfirmOut" runat="server" CssClass="auto-style224"></asp:Label>
                  </td>
              </tr>
              <tr>
                  <td class="auto-style253"></td>
                  <td class="auto-style254">
                      <asp:Label ID="Label15" runat="server" CssClass="auto-style224" Text="2"></asp:Label>
                  </td>
                  <td class="auto-style255">
                      <asp:TextBox ID="TextCoin2Cnt" runat="server" CssClass="auto-style224" Height="18px" Width="73px"></asp:TextBox>
                  </td>
                  <td class="auto-style256">
                      <asp:TextBox ID="TextCoin2Sum" runat="server" Height="18px"></asp:TextBox>
                  </td>
                  <td class="auto-style257">
                      <asp:Label ID="lblConfirmOut2" runat="server" CssClass="auto-style224"></asp:Label>
                  </td>
              </tr>
              <tr>
                  <td class="auto-style253"></td>
                  <td class="auto-style254">1</td>
                  <td class="auto-style255">
                      <asp:TextBox ID="TextCoin1Cnt" runat="server" CssClass="auto-style224" Height="18px" Width="73px"></asp:TextBox>
                  </td>
                  <td class="auto-style256">
                      <asp:TextBox ID="TextCoin1Sum" runat="server" Height="18px"></asp:TextBox>
                  </td>
                  <td class="auto-style257">
                    <asp:Label ID="lblLastMsg" runat="server" Text="Deposit complete!!" CssClass="auto-style240"></asp:Label>
                  </td>
              </tr>
              <tr>
                  <td class="auto-style253"></td>
                  <td class="auto-style254"></td>
                  <td class="auto-style255">
                    <asp:Label ID="Label16" runat="server" Text="total" CssClass="auto-style240"></asp:Label>
                  </td>
                  <td class="auto-style256">
                      <asp:TextBox ID="TextTotal" runat="server" Height="18px" Width="148px"></asp:TextBox>
                  </td>
                  <td class="auto-style257">
                    <asp:ImageButton ID="btnSend" runat="server" Height="29px" ImageUrl="~/icon/send.png" Width="81px" CssClass="auto-style228" />
                  </td>
              </tr>
              <tr>
                  <td class="auto-style253"></td>
                  <td class="auto-style254"></td>
                  <td class="auto-style255">
                    <asp:Label ID="Label17" runat="server" Text="difference" CssClass="auto-style240"></asp:Label>
                  </td>
                  <td class="auto-style256">
                      <asp:TextBox ID="TextDiff" runat="server" Height="18px" Width="148px"></asp:TextBox>
                  </td>
                  <td class="auto-style257">
                      <asp:Label ID="Label21" runat="server" CssClass="auto-style263" Text="Confirmation user"></asp:Label>&nbsp;&nbsp;
                      <asp:Label ID="Label22" runat="server" CssClass="auto-style263" Text="ID"></asp:Label>&nbsp;&nbsp;
                      <asp:TextBox ID="TextID" runat="server"></asp:TextBox>&nbsp;&nbsp;
                      <asp:Label ID="Label12" runat="server" CssClass="auto-style263" Text="PASS"></asp:Label>&nbsp;&nbsp;
                      <asp:TextBox ID="TextPASS" runat="server" TextMode="Password" Height="19px"></asp:TextBox>&nbsp;&nbsp;
                      <asp:ImageButton ID="btnSend2" runat="server" Height="29px" ImageUrl="~/icon/send.png" Width="81px" CssClass="auto-style228" />
                  </td>
              </tr>
              <tr>
                  <td class="auto-style257"></td>
                  <td class="auto-style257"></td>
                  <td class="auto-style257"></td>
                  <td class="auto-style257"></td>
                  <td class="auto-style257">
                 <asp:Label ID="lblUpdateShow" runat="server" Text="Lastupdate : " CssClass="auto-style240"></asp:Label>
                 <asp:Label ID="lblLastupdate" runat="server" CssClass="auto-style240"></asp:Label>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                 <asp:Label ID="lblIPShow" runat="server" CssClass="auto-style240" Text="IP : "></asp:Label>
                 <asp:Label ID="lblIP" runat="server" CssClass="auto-style240"></asp:Label>&nbsp;&nbsp;
                 &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                  </td>
              </tr>
              <tr>
                  <td class="auto-style257"></td>
                  <td class="auto-style257"></td>
                  <td class="auto-style257"></td>
                  <td class="auto-style257"></td>
                  <td class="auto-style257">
                 <asp:Label ID="lblUpdateShow2" runat="server" Text="Confirm Lastupdate : " CssClass="auto-style240"></asp:Label>
                 <asp:Label ID="lblLastupdate2" runat="server" CssClass="auto-style240"></asp:Label>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                 <asp:Label ID="lblIPShow2" runat="server" CssClass="auto-style240" Text="IP : "></asp:Label>
                 <asp:Label ID="lblIP2" runat="server" CssClass="auto-style240"></asp:Label>
                      &nbsp;&nbsp;
                 &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                  </td>
              </tr>
          </table>
       
      </div> 
    </div>
    <div id="dialog" title="message" style="display:none;"> <span class="auto-style228">>
        </span>
        <asp:Label ID="lblMsg" runat="server" Text="" CssClass="auto-style228"></asp:Label>
    </div>

</asp:Content>
