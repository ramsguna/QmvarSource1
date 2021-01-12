<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Site6.Master" CodeBehind="Repair_2.aspx.vb" Inherits="Ganges33.Repair_2aspx" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="ajaxToolkit" %>
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
                height: 221px;
            }
            .auto-style141 {
                width: 250px;
                font-family: "Meiryo UI";
            }
            .auto-style142 {
                width: 158px;
            }
            .auto-style149 {
                width: 255px;
            }
            .auto-style156 {
                width: 158px;
                height: 30px;
            }
            .auto-style157 {
                width: 250px;
                height: 30px;
            }
            .auto-style158 {
                width: 255px;
                height: 30px;
            }
            .auto-style159 {
                width: 1250px;
                height: 265px;
                overflow-x: scroll;
            }
            .auto-style160 {
                margin-top: 0px;
            }
            .auto-style161 {
                font-family: "Meiryo UI";
            }
            .auto-style162 {
                width: 158px;
                font-family: "Meiryo UI";
            }
        </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="auto-style6" style="background-image: url('pagewall_2/wall_repair-2.png')">
       <div class="auto-style7"> 
        <table class="auto-style135">
            <tr>
                <td class="auto-style156">
                    <asp:Label ID="Label1" runat="server" Text="PO No." CssClass="auto-style161"></asp:Label>
                </td>
                <td class="auto-style157">
                    <asp:TextBox ID="TextPO_NO" runat="server" Height="19px" Width="242px" CssClass="auto-style161"></asp:TextBox>
                </td>
                <td class="auto-style156">
                    <asp:Label ID="Label6" runat="server" Text="Serial No" CssClass="auto-style161"></asp:Label>
                </td>
                <td class="auto-style157">
                    <asp:TextBox ID="TextSerial_No" runat="server" Height="19px" Width="242px" CssClass="auto-style161"></asp:TextBox>
                </td>
                <td class="auto-style156">
                    <asp:Label ID="Label11" runat="server" Text="Purchase Date" CssClass="auto-style161"></asp:Label>
                </td>
                <td class="auto-style158">
                    <asp:ScriptManager ID="ScriptManager1" runat="server">
                    </asp:ScriptManager>
                     <asp:TextBox ID="TextPurchase_Date" runat="server" Height="19px" Width="242px" CssClass="auto-style161"></asp:TextBox>
                    <ajaxToolkit:CalendarExtender ID="TextPurchase_Date_CalendarExtender" runat="server" BehaviorID="TextPurchase_Date_CalendarExtender" TargetControlID="TextPurchase_Date" PopupPosition="Right">
                    </ajaxToolkit:CalendarExtender>
               </td>
            </tr>
            <tr>
                <td class="auto-style156">
                    <asp:Label ID="Label2" runat="server" Text="ASC Claim No" CssClass="auto-style161"></asp:Label>
                </td>
                <td class="auto-style157">
                    <asp:TextBox ID="TextASC_Claim_No" runat="server" Height="19px" Width="242px" CssClass="auto-style161"></asp:TextBox>
                </td>
                <td class="auto-style156">
                    <asp:Label ID="Label7" runat="server" Text="Model" CssClass="auto-style161"></asp:Label>
                </td>
                <td class="auto-style157">
                    <asp:TextBox ID="TextModel" runat="server" Height="19px" Width="242px" CssClass="auto-style161"></asp:TextBox>
                </td>
                <td class="auto-style156">
                    <asp:Label ID="Label12" runat="server" Text="Repair Recived Date" CssClass="auto-style161"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="TextRepair_Recived_Date" runat="server" Height="19px" Width="242px" CssClass="auto-style161"></asp:TextBox>
                    <ajaxToolkit:CalendarExtender ID="TextRepair_Recived_Date_CalendarExtender" runat="server" BehaviorID="TextRepair_Recived_Date_CalendarExtender" TargetControlID="TextRepair_Recived_Date" PopupPosition="Right">
                    </ajaxToolkit:CalendarExtender>
                </td>
            </tr>
            <tr>
                <td class="auto-style156">
                    <asp:Label ID="Label3" runat="server" Text="Samsung Claim No" CssClass="auto-style161"></asp:Label>
                </td>
                <td class="auto-style157">
                    <asp:TextBox ID="TextSamsung_Claim_No" runat="server" Height="19px" Width="242px" CssClass="auto-style161"></asp:TextBox>
                </td>
                <td class="auto-style156">
                    <asp:Label ID="Label8" runat="server" Text="IMEI" CssClass="auto-style161"></asp:Label>
                </td>
                <td class="auto-style157">
                    <asp:TextBox ID="TextIMEI" runat="server" Height="19px" Width="242px" CssClass="auto-style161"></asp:TextBox>
                </td>
                <td class="auto-style156">
                    <asp:Label ID="Label13" runat="server" Text="Complete Date" CssClass="auto-style161"></asp:Label>
                </td>
                <td class="auto-style158">
                    <asp:TextBox ID="TextComplete_Date" runat="server" Height="19px" Width="242px" CssClass="auto-style161"></asp:TextBox>
                    <ajaxToolkit:CalendarExtender ID="TextComplete_Date_CalendarExtender" runat="server" BehaviorID="TextComplete_Date_CalendarExtender" TargetControlID="TextComplete_Date" PopupPosition="Right">
                    </ajaxToolkit:CalendarExtender>
                </td>
            </tr>
            <tr>
                <td class="auto-style156">
                    <asp:Label ID="Label4" runat="server" Text="Parts No 1" CssClass="auto-style161"></asp:Label>
                </td>
                <td class="auto-style157">
                    <asp:TextBox ID="TextParts_No1" runat="server" Height="19px" Width="242px" CssClass="auto-style161"></asp:TextBox>
                </td>
                <td class="auto-style156">
                    <asp:Label ID="Label9" runat="server" Text="Parts No 2" CssClass="auto-style161"></asp:Label>
                </td>
                <td class="auto-style157">
                    <asp:TextBox ID="TextParts_No2" runat="server" Height="19px" Width="242px" CssClass="auto-style161"></asp:TextBox>
                </td>
                <td class="auto-style156">
                    <asp:Label ID="Label21" runat="server" Text="Parts No 3" CssClass="auto-style161"></asp:Label>
                </td>
                <td class="auto-style158">
                    <asp:TextBox ID="TextParts_No3" runat="server" Height="19px" Width="242px" CssClass="auto-style161"></asp:TextBox>
                </td>
            </tr>
             <tr>
                <td class="auto-style156">
                    <asp:Label ID="Label22" runat="server" Text="Parts No 4" CssClass="auto-style161"></asp:Label>
                </td>
                <td class="auto-style157">
                    <asp:TextBox ID="TextParts_No4" runat="server" Height="19px" Width="242px" CssClass="auto-style161"></asp:TextBox>
                </td>
                <td class="auto-style156">
                    <asp:Label ID="Label23" runat="server" Text="Parts No 5" CssClass="auto-style161"></asp:Label>
                </td>
                <td class="auto-style157">
                    <asp:TextBox ID="TextParts_No5" runat="server" Height="19px" Width="242px" CssClass="auto-style161"></asp:TextBox>
                </td>
                <td class="auto-style156">
                    <asp:Label ID="Label17" runat="server" Text="Consumer name" CssClass="auto-style161"></asp:Label>
                </td>
                <td class="auto-style158">
                    <asp:TextBox ID="TextConsumer_name" runat="server" Height="19px" Width="242px" CssClass="auto-style161"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td class="auto-style156">
                    <asp:Label ID="Label20" runat="server" Text="Tracking No" CssClass="auto-style161"></asp:Label>
                </td>
                <td class="auto-style157">
                    <asp:TextBox ID="TextTracking_No" runat="server" Height="19px" Width="242px" CssClass="auto-style161"></asp:TextBox>
                </td>
                <td class="auto-style156">
                    <asp:Label ID="Label19" runat="server" Text="Engineer" CssClass="auto-style161"></asp:Label>
                </td>
                <td class="auto-style157">
                    <asp:TextBox ID="TextEngineer" runat="server" Height="19px" Width="242px" CssClass="auto-style161"></asp:TextBox>
                </td>
                <td class="auto-style156">
                    <asp:Label ID="Label18" runat="server" Text="telephon" CssClass="auto-style161"></asp:Label>
                </td>
                <td class="auto-style158">
                    <asp:TextBox ID="TextTelephon" runat="server" Height="19px" Width="242px" CssClass="auto-style161"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td class="auto-style162">&nbsp;</td>
                <td class="auto-style141">&nbsp;</td>
                <td class="auto-style162">&nbsp;</td>
                <td class="auto-style141">
                    &nbsp;</td>
                <td class="auto-style142">
                    <asp:ImageButton ID="btnDownLoad" runat="server" Height="29px" ImageUrl="~/icon/download.png" Width="81px" CssClass="auto-style161" />
                </td>
                <td class="auto-style149" align = "right">
                    <asp:ImageButton ID="btnSearch" runat="server" Height="29px" ImageUrl="~/icon/search.png" Width="81px" CssClass="auto-style161" />
                </td>
            </tr>
        </table>
            <div class="auto-style159">
                <asp:GridView ID="grd_info" runat="server" CssClass="auto-style160" Width="1237px" AllowPaging="True" Height="16px" PageSize="7">
                    <Columns>
                        <asp:ButtonField CommandName="Detail" HeaderText="Detail" ShowHeader="True" Text="Detail" />
                    </Columns>
                    <HeaderStyle BackColor="#00FFCC" Height="30px" />
                    <RowStyle BackColor="#E8FFCA" Height="25px" Wrap="False" />
                </asp:GridView>
            </div>
         </div>
      </div>
    
    <div id="dialog" title="message" style="display:none;"> >
        <asp:Label ID="lblMsg" runat="server" Text=""></asp:Label>
    </div>
</asp:Content>
