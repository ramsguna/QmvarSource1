<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Site5.Master" CodeBehind="Repair_1.aspx.vb" Inherits="Ganges33.Repair_1" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="ajaxToolkit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script src="http://ajax.googleapis.com/ajax/libs/jquery/1.7.0/jquery.min.js"></script>
    <link type="text/css" href="http://ajax.googleapis.com/ajax/libs/jqueryui/1/themes/start/jquery-ui.css" rel="stylesheet">
    <script type="text/javascript" src="http://ajax.googleapis.com/ajax/libs/jqueryui/1/jquery-ui.min.js"></script>
    <link type="text/css" href="CSS/Common/Repair_1.css" rel="stylesheet" />  
    <style type="text/css">
       
        </style>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="div-pagewall" style="background-image: url('pagewall_2/wall_repair-2.png')">
       <div class="div-header"> 
        <table class="td-tbl">
            <tr>
                <td class="td-image">
                    <asp:Image ID="Image3" runat="server" Height="108px" ImageUrl="~/icon/repair.png" />
                </td>
                <td class="td-repairsearch-td-blank2-td-blank3-td-blank4">
                    <asp:Label ID="Label50" runat="server" CssClass="lbl-txt-repairsearch" Text="Repair Search"></asp:Label>
                </td>
                <td class="td-blank1"></td>
                <td class="td-repairsearch-td-blank2-td-blank3-td-blank4"></td>
                <td class="td-repairsearch-td-blank2-td-blank3-td-blank4"></td>
                <td class="td-repairsearch-td-blank2-td-blank3-td-blank4"></td>
            </tr>
            <tr>
                <td class="td-po-no-td-ascclaim-td-imei-td-partno1-td-partsno4-td-trackingno">
                    <asp:Label ID="Label1" runat="server" Text="PO No." CssClass="lbl-po-no-textbox-po-no-lbl-serialno-textbox-serialno-lbl-purchasedate-lbl-from1-textbox1-lbl-to"></asp:Label>
                </td>
                <td class="td-textbox">
                    <asp:TextBox ID="TextPO_NO" runat="server" Height="19px" Width="242px" CssClass="lbl-po-no-textbox-po-no-lbl-serialno-textbox-serialno-lbl-purchasedate-lbl-from1-textbox1-lbl-to"></asp:TextBox>
                </td>
                <td class="td-serialno-td-model-td-blank-td-partsno2-td-partsno5-td-engineer">
                    <asp:Label ID="Label6" runat="server" Text="Serial No" CssClass="lbl-po-no-textbox-po-no-lbl-serialno-textbox-serialno-lbl-purchasedate-lbl-from1-textbox1-lbl-to"></asp:Label>
                </td>
                <td class="td-textbox">
                    <asp:TextBox ID="TextSerial_No" runat="server" Height="19px" Width="242px" CssClass="lbl-po-no-textbox-po-no-lbl-serialno-textbox-serialno-lbl-purchasedate-lbl-from1-textbox1-lbl-to"></asp:TextBox>
                </td>
                <td class="td-purchasedate-td-rrdate-td-completedate-td-partsno3-td-consumername-td-telph">
                    <asp:Label ID="Label11" runat="server" Text="Purchase Date" CssClass="lbl-po-no-textbox-po-no-lbl-serialno-textbox-serialno-lbl-purchasedate-lbl-from1-textbox1-lbl-to"></asp:Label>
                </td>
                <td class="td-from-td-textbox">
                    <asp:Label ID="Label16" runat="server" Text="From" CssClass="lbl-po-no-textbox-po-no-lbl-serialno-textbox-serialno-lbl-purchasedate-lbl-from1-textbox1-lbl-to"></asp:Label>
                    <asp:ScriptManager ID="ScriptManager1" runat="server">
                    </asp:ScriptManager>
                    <asp:TextBox ID="TextPurchase_Date_From" runat="server" Height="19px" Width="83px" CssClass="textbox1-161"></asp:TextBox>
                    <ajaxToolkit:CalendarExtender ID="TextPurchase_Date_From_CalendarExtender" runat="server" BehaviorID="TextPurchase_Date_CalendarExtender" TargetControlID="TextPurchase_Date_From"　PopupPosition="left">
                    </ajaxToolkit:CalendarExtender>
                    <asp:Label ID="Label24" runat="server" Text="To" CssClass="lbl-po-no-textbox-po-no-lbl-serialno-textbox-serialno-lbl-purchasedate-lbl-from1-textbox1-lbl-to"></asp:Label>
                    <asp:TextBox ID="TextPurchase_Date_To" runat="server" Height="19px" Width="83px"></asp:TextBox>
                    <ajaxToolkit:CalendarExtender ID="TextPurchase_Date_To_CalendarExtender" runat="server" BehaviorID="TextBox3_CalendarExtender" TargetControlID="TextPurchase_Date_To" />
                </td>
            </tr>
            <tr>
                <td class="td-po-no-td-ascclaim-td-imei-td-partno1-td-partsno4-td-trackingno">
                    <asp:Label ID="Label2" runat="server" Text="ASC Claim No" CssClass="lbl-ascclaimno-textbox-ascclaimno-lbl-model-textbox-model-lbl-rrdate-lbl-from2-textbox2-lbl-to"></asp:Label>
                </td>
                <td class="td-textbox">
                    <asp:TextBox ID="TextASC_Claim_No" runat="server" Height="19px" Width="242px" CssClass="lbl-ascclaimno-textbox-ascclaimno-lbl-model-textbox-model-lbl-rrdate-lbl-from2-textbox2-lbl-to"></asp:TextBox>
                </td>
                <td class="td-serialno-td-model-td-blank-td-partsno2-td-partsno5-td-engineer">
                    <asp:Label ID="Label7" runat="server" Text="Model" CssClass="lbl-ascclaimno-textbox-ascclaimno-lbl-model-textbox-model-lbl-rrdate-lbl-from2-textbox2-lbl-to"></asp:Label>
                </td>
                <td class="td-textbox">
                    <asp:TextBox ID="TextModel" runat="server" Height="19px" Width="242px" CssClass="lbl-ascclaimno-textbox-ascclaimno-lbl-model-textbox-model-lbl-rrdate-lbl-from2-textbox2-lbl-to"></asp:TextBox>
                </td>
                <td class="td-purchasedate-td-rrdate-td-completedate-td-partsno3-td-consumername-td-telph">
                    <asp:Label ID="Label12" runat="server" Text="Repair Recived Date" CssClass="lbl-ascclaimno-textbox-ascclaimno-lbl-model-textbox-model-lbl-rrdate-lbl-from2-textbox2-lbl-to"></asp:Label>
                </td>
                <td class="td-from-td-textbox">
                    <asp:Label ID="Label5" runat="server" Text="From" CssClass="lbl-ascclaimno-textbox-ascclaimno-lbl-model-textbox-model-lbl-rrdate-lbl-from2-textbox2-lbl-to"></asp:Label>
                    <asp:TextBox ID="TextRepair_Recived_Date_From" runat="server" Height="19px" Width="83px" CssClass="lbl-ascclaimno-textbox-ascclaimno-lbl-model-textbox-model-lbl-rrdate-lbl-from2-textbox2-lbl-to"></asp:TextBox>
                    <ajaxToolkit:CalendarExtender ID="TextRepair_Recived_Date_From_CalendarExtender" runat="server" BehaviorID="TextRepair_Recived_Date_CalendarExtender" TargetControlID="TextRepair_Recived_Date_From" PopupPosition="left">
                    </ajaxToolkit:CalendarExtender>
                    <asp:Label ID="Label10" runat="server" Text="To" CssClass="lbl-ascclaimno-textbox-ascclaimno-lbl-model-textbox-model-lbl-rrdate-lbl-from2-textbox2-lbl-to"></asp:Label>
                    <asp:TextBox ID="TextRepair_Recived_Date_To" runat="server" Height="19px" Width="83px"></asp:TextBox>
                    <ajaxToolkit:CalendarExtender ID="TextRepair_Recived_Date_To_CalendarExtender" runat="server" BehaviorID="TextBox1_CalendarExtender" TargetControlID="TextRepair_Recived_Date_To" />
                </td>
            </tr>
            <tr>
                <td class="td-po-no-td-ascclaim-td-imei-td-partno1-td-partsno4-td-trackingno">
                    <asp:Label ID="Label51" runat="server" Text="IMEI" CssClass="lbl-imei-lbl-completedate-lbl-from3-textbox3-lbl-to-lbl-partsno-lbl-consumername-lbl-trackingno-lbl-engineer-lbl-telph-images"></asp:Label>
                </td>
                <td class="td-textbox">
                    <asp:TextBox ID="TextIMEI" runat="server"  CssClass="textbox-imei-partsno-partsno3-partsno4-partsno5-consumername-trackingno-telph"></asp:TextBox>
                </td>
                <td class="td-serialno-td-model-td-blank-td-partsno2-td-partsno5-td-engineer">
                    &nbsp;</td>
                <td class="td-textbox">
                    &nbsp;</td>
                <td class="td-purchasedate-td-rrdate-td-completedate-td-partsno3-td-consumername-td-telph">
                    <asp:Label ID="Label13" runat="server" Text="Complete Date" CssClass="lbl-imei-lbl-completedate-lbl-from3-textbox3-lbl-to-lbl-partsno-lbl-consumername-lbl-trackingno-lbl-engineer-lbl-telph-images"></asp:Label>
                </td>
                <td class="td-from-td-textbox">
                    <asp:Label ID="Label14" runat="server" Text="From" CssClass="lbl-imei-lbl-completedate-lbl-from3-textbox3-lbl-to-lbl-partsno-lbl-consumername-lbl-trackingno-lbl-engineer-lbl-telph-images"></asp:Label>
                    <asp:TextBox ID="TextComplete_Date_From" runat="server" Height="19px" Width="83px" CssClass="lbl-imei-lbl-completedate-lbl-from3-textbox3-lbl-to-lbl-partsno-lbl-consumername-lbl-trackingno-lbl-engineer-lbl-telph-images"></asp:TextBox>
                    <ajaxToolkit:CalendarExtender ID="TextComplete_Date_From_CalendarExtender" runat="server" BehaviorID="TextComplete_Date_CalendarExtender" TargetControlID="TextComplete_Date_From" PopupPosition="left">
                    </ajaxToolkit:CalendarExtender>
                    <asp:Label ID="Label15" runat="server" Text="To" CssClass="lbl-imei-lbl-completedate-lbl-from3-textbox3-lbl-to-lbl-partsno-lbl-consumername-lbl-trackingno-lbl-engineer-lbl-telph-images"></asp:Label>
                    <asp:TextBox ID="TextComplete_Date_To" runat="server" Height="19px" Width="84px"></asp:TextBox>
                    <ajaxToolkit:CalendarExtender ID="TextComplete_Date_To_CalendarExtender" runat="server" BehaviorID="TextBox2_CalendarExtender" TargetControlID="TextComplete_Date_To" />
                </td>
            </tr>
            <tr>
                <td class="td-po-no-td-ascclaim-td-imei-td-partno1-td-partsno4-td-trackingno">
                    <asp:Label ID="Label4" runat="server" Text="Parts No 1" CssClass="lbl-imei-lbl-completedate-lbl-from3-textbox3-lbl-to-lbl-partsno-lbl-consumername-lbl-trackingno-lbl-engineer-lbl-telph-images"></asp:Label>
                </td>
                <td class="td-textbox">
                    <asp:TextBox ID="TextParts_No1" runat="server" CssClass="textbox-imei-partsno-partsno3-partsno4-partsno5-consumername-trackingno-telph"></asp:TextBox>
                </td>
                <td class="td-serialno-td-model-td-blank-td-partsno2-td-partsno5-td-engineer">
                    <asp:Label ID="Label9" runat="server" Text="Parts No 2" CssClass="lbl-imei-lbl-completedate-lbl-from3-textbox3-lbl-to-lbl-partsno-lbl-consumername-lbl-trackingno-lbl-engineer-lbl-telph-images"></asp:Label>
                </td>
                <td class="td-textbox">
                    <asp:TextBox ID="TextParts_No2" runat="server" Height="19px" Width="242px" CssClass="textbox-imei-partsno-partsno3-partsno4-partsno5-consumername-trackingno-telph"></asp:TextBox>
                </td>
                <td class="td-purchasedate-td-rrdate-td-completedate-td-partsno3-td-consumername-td-telph">
                    <asp:Label ID="Label21" runat="server" Text="Parts No 3" CssClass="lbl-imei-lbl-completedate-lbl-from3-textbox3-lbl-to-lbl-partsno-lbl-consumername-lbl-trackingno-lbl-engineer-lbl-telph-images"></asp:Label>
                </td>
                <td class="td-from-td-textbox">
                    <asp:TextBox ID="TextParts_No3" runat="server" Height="19px" Width="242px" CssClass="textbox-imei-partsno-partsno3-partsno4-partsno5-consumername-trackingno-telph"></asp:TextBox>
                </td>
            </tr>
             <tr>
                <td class="td-po-no-td-ascclaim-td-imei-td-partno1-td-partsno4-td-trackingno">
                    <asp:Label ID="Label22" runat="server" Text="Parts No 4" CssClass="lbl-imei-lbl-completedate-lbl-from3-textbox3-lbl-to-lbl-partsno-lbl-consumername-lbl-trackingno-lbl-engineer-lbl-telph-images"></asp:Label>
                </td>
                <td class="td-textbox">
                    <asp:TextBox ID="TextParts_No4" runat="server" Height="19px" Width="242px" CssClass="textbox-imei-partsno-partsno3-partsno4-partsno5-consumername-trackingno-telph"></asp:TextBox>
                </td>
                <td class="td-serialno-td-model-td-blank-td-partsno2-td-partsno5-td-engineer">
                    <asp:Label ID="Label23" runat="server" Text="Parts No 5" CssClass="lbl-imei-lbl-completedate-lbl-from3-textbox3-lbl-to-lbl-partsno-lbl-consumername-lbl-trackingno-lbl-engineer-lbl-telph-images"></asp:Label>
                </td>
                <td class="td-textbox">
                    <asp:TextBox ID="TextParts_No5" runat="server" Height="19px" Width="242px" CssClass="textbox-imei-partsno-partsno3-partsno4-partsno5-consumername-trackingno-telph"></asp:TextBox>
                </td>
                <td class="td-purchasedate-td-rrdate-td-completedate-td-partsno3-td-consumername-td-telph">
                    <asp:Label ID="Label17" runat="server" Text="Consumer name" CssClass="lbl-imei-lbl-completedate-lbl-from3-textbox3-lbl-to-lbl-partsno-lbl-consumername-lbl-trackingno-lbl-engineer-lbl-telph-images"></asp:Label>
                </td>
                <td class="td-from-td-textbox">
                    <asp:TextBox ID="TextConsumer_name" runat="server" Height="19px" Width="242px" CssClass="textbox-imei-partsno-partsno3-partsno4-partsno5-consumername-trackingno-telph"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td class="td-po-no-td-ascclaim-td-imei-td-partno1-td-partsno4-td-trackingno">
                    <asp:Label ID="Label20" runat="server" Text="Tracking No" CssClass="lbl-imei-lbl-completedate-lbl-from3-textbox3-lbl-to-lbl-partsno-lbl-consumername-lbl-trackingno-lbl-engineer-lbl-telph-images"></asp:Label>
                </td>
                <td class="td-textbox">
                    <asp:TextBox ID="TextTracking_No" runat="server" Height="19px" Width="242px" CssClass="textbox-imei-partsno-partsno3-partsno4-partsno5-consumername-trackingno-telph"></asp:TextBox>
                </td>
                <td class="td-serialno-td-model-td-blank-td-partsno2-td-partsno5-td-engineer">
                    <asp:Label ID="Label19" runat="server" Text="Engineer" CssClass="lbl-imei-lbl-completedate-lbl-from3-textbox3-lbl-to-lbl-partsno-lbl-consumername-lbl-trackingno-lbl-engineer-lbl-telph-images"></asp:Label>
                </td>
                <td class="td-textbox">
                    <asp:TextBox ID="TextEngineer" runat="server" Height="19px" Width="242px" CssClass="textbox-engineer-161"></asp:TextBox>
                </td>
                <td class="td-purchasedate-td-rrdate-td-completedate-td-partsno3-td-consumername-td-telph">
                    <asp:Label ID="Label18" runat="server" Text="Telephone" CssClass="lbl-imei-lbl-completedate-lbl-from3-textbox3-lbl-to-lbl-partsno-lbl-consumername-lbl-trackingno-lbl-engineer-lbl-telph-images"></asp:Label>
                </td>
                <td class="td-from-td-textbox">
                    <asp:TextBox ID="TextTelephon" runat="server" Height="19px" Width="242px" CssClass="textbox-imei-partsno-partsno3-partsno4-partsno5-consumername-trackingno-telph"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td class="td-blank1">&nbsp;</td>
                <td class="td-blank2">&nbsp;</td>
                <td class="td-blank3">&nbsp;</td>
                <td class="td-blank4">
                    &nbsp;</td>
                <td class="td-download">
                    <asp:ImageButton ID="btnDownLoad" runat="server" Height="29px" ImageUrl="~/icon/download.png" Width="81px" CssClass="lbl-imei-lbl-completedate-lbl-from3-textbox3-lbl-to-lbl-partsno-lbl-consumername-lbl-trackingno-lbl-engineer-lbl-telph-images" />
                </td>
                <td class="td-search" align = "right">
                    <asp:ImageButton ID="btnSearch" runat="server" Height="29px" ImageUrl="~/icon/search.png" Width="81px" CssClass="lbl-imei-lbl-completedate-lbl-from3-textbox3-lbl-to-lbl-partsno-lbl-consumername-lbl-trackingno-lbl-engineer-lbl-telph-images" />
                </td>
            </tr>
        </table>
            <div class="div-grid">
                <asp:GridView ID="grd_info" runat="server" CssClass="gridinfo" Width="1237px" AllowPaging="True" Height="16px" PageSize="7">
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
