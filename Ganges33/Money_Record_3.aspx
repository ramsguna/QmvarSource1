<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Site5.Master" CodeBehind="Money_Record_3.aspx.vb" Inherits="Ganges33.Money_Record_3_aspx" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script src="http://ajax.googleapis.com/ajax/libs/jquery/1.7.0/jquery.min.js"></script>
    <link href="CSS/Common/money-record-3.css" rel="stylesheet" />
    <link type="text/css" href="http://ajax.googleapis.com/ajax/libs/jqueryui/1/themes/start/jquery-ui.css" rel="stylesheet">
    <script type="text/javascript" src="http://ajax.googleapis.com/ajax/libs/jqueryui/1/jquery-ui.min.js"></script>
              
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="div-entire-tbl" style="background-image: url('pagewall_2/wall_repair-2.png')">
    <div class="div-money-record3">
        <table class="tbl-money-record3">
            <tr>
                <td class="td-btn-money " rowspan = "2">
                    <asp:Image ID="Image3" runat="server" Height="81px" ImageUrl="~/icon/repair.png" Width="81px" />
                </td>
                <td class="td-lbl-repair-btn-start">
                    <asp:Label ID="Label50" runat="server" CssClass="lbl-large-font" Text="Repair Input"></asp:Label>
                </td>
                <td class="td-blank1"></td>
                <td class="td-lbl-repair-btn-start">
                    <asp:ImageButton ID="btnStart" runat="server" ImageUrl="~/icon/start.png"  CssClass="btn-img-start-send-today-postal-code" />
                </td>
            </tr>
            <tr>
                <td class="td-records-date-time">
                    <asp:Label ID="lblDate" runat="server" Text="Record date&amp;time : " CssClass="lbl-font-common"></asp:Label>
                    <asp:Label ID="lblRecord" runat="server" CssClass="lbl-font-common"></asp:Label><br />
                    <asp:Label ID="lblName" runat="server" Text="user name: " CssClass="lbl-font-common"></asp:Label>
                    <asp:Label ID="lblYousername" runat="server" CssClass="lbl-font-common"></asp:Label>
                </td>
                <td class="td-btn-go-exit" align ="right" >
                    <asp:Button ID="BtnComplate" runat="server" BackColor="#CCFF99" CssClass="lbl-go-exit" ForeColor="Black"  Text="go edit screen"  />
                    <asp:Label ID="Label38" runat="server" Text="Po" CssClass="lbl-font-common"></asp:Label>
                </td>
                <td class="td-txtbox-po-serial-imei--model">
                    <asp:TextBox ID="TextPo" runat="server"  CssClass="txtbox-product-serial-imei-marker-model"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td class="td-common1">
                    <asp:Label ID="Label39" runat="server" Text="Recept D&amp;T" CssClass="lbl-font-common"></asp:Label>
                </td>
                <td class="td-txtbox-recept-postal">
                    <asp:TextBox ID="TextRepair_Received_Date" runat="server" CssClass="txtbox-recept-postal"></asp:TextBox>
                    <asp:Button ID="btnToday" runat="server" Text="today"  BackColor="#CCFF99" CssClass="btn-img-start-send-today-postal-code"/>
                </td>
                <td class="td-product">
                    <span class="lbl-font-common">&nbsp; &nbsp;&nbsp;</span><asp:Label ID="Label40" runat="server" Text="Product type" CssClass="lbl-font-common"></asp:Label>
                </td>
                <td class="td-drpdown">
                    <asp:DropDownList ID="DropListProductType" runat="server"  CssClass="txtbox-product-serial-imei-marker-model">
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td class="td-common1">
                    <asp:Label ID="Label2" runat="server" Text="Customer name" CssClass="lbl-font-common"></asp:Label>
                </td>
                <td class="td-txtbox-customers">
                    <asp:TextBox ID="TextConsumer_Name" runat="server" CssClass="txtbox-customer-state-address-telephone-fax"></asp:TextBox>
                </td>
                <td class="td-txtbox-po-serial-imei--model">
                    <span class="lbl-font-common">&nbsp;&nbsp;&nbsp;</span><asp:Label ID="Label8" runat="server" Text="Serial number" CssClass="lbl-font-common"></asp:Label>
                </td>
                <td >
                    <asp:TextBox ID="TextSerial_No" runat="server"  CssClass="txtbox-product-serial-imei-marker-model"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td class="td-common1">
                    <asp:Label ID="Label7" runat="server" Text="Postal Code" CssClass="lbl-font-common"></asp:Label>
                </td>
                <td class="td-txtbox-recept-postal">
                    <asp:TextBox ID="TextPostal_Code" runat="server" CssClass="txtbox-recept-postal"></asp:TextBox>
                    <asp:Button ID="btnPostal" runat="server" Text="〒"  BackColor="#CCFF99" CssClass="btn-img-start-send-today-postal-code" />
                </td>
                <td class="td-txtbox-po-serial-imei--model">
                    <span class="lbl-font-common">&nbsp;&nbsp;&nbsp;</span><asp:Label ID="Label11" runat="server" Text="IMEI number" CssClass="lbl-font-common"></asp:Label>
                </td>
                <td class="td-txtbox-po-serial-imei--model">
                    <asp:TextBox ID="TextIMEI_No" runat="server"  CssClass="txtbox-product-serial-imei-marker-model"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td class="td-common1">
                    <asp:Label ID="Label1" runat="server" Text="state" CssClass="lbl-font-common"></asp:Label>
                </td>
                <td class="td-drpdown">
                    <asp:DropDownList ID="DropListState" runat="server" CssClass="txtbox-customer-state-address-telephone-fax" AutoPostBack="True" >
                    </asp:DropDownList>
                </td>
                <td class="td-txtbox-po-serial-imei--model">
                    <span class="lbl-font-common">&nbsp;&nbsp;&nbsp;</span><asp:Label ID="Label49" runat="server" Text="To Apply" CssClass="lbl-font-common"></asp:Label>
                </td>
                <td class="td-txtbox-po-serial-imei--model">
                    <asp:Label ID="lblToApply" runat="server" CssClass="lbl-font-common"></asp:Label>
                </td>
            </tr>
            <tr>
                <td class="td-common1" rowspan = "2">
                    <asp:Label ID="Label44" runat="server" Text="Customer Addr1" CssClass="lbl-font-common"></asp:Label>
                </td>
                <td class="td-drpdown" rowspan = "2">
                    <asp:TextBox ID="TextConsumer_Addr1" runat="server" Rows="2" TextMode="MultiLine" CssClass="txtbox-customers-address1_2"></asp:TextBox>
                </td>
                <td class="td-txtbox-po-serial-imei--model">
                    <span class="lbl-font-common"></span><asp:Label ID="Label45" runat="server" Text="Maker" CssClass="lbl-font-common"></asp:Label>
                </td>
                <td class="td-txtbox-po-serial-imei--model">
                    <asp:TextBox ID="TextMaker" runat="server"  CssClass="txtbox-product-serial-imei-marker-model"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td class="td-txtbox-po-serial-imei--model">
                    <span class="lbl-font-common">&nbsp;&nbsp;&nbsp;</span><asp:Label ID="Label46" runat="server" Text="Model" CssClass="lbl-font-common"></asp:Label>
                </td>
                <td class="td-txtbox-po-serial-imei--model">
                    <asp:TextBox ID="TextModel" runat="server"  CssClass="txtbox-product-serial-imei-marker-model"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td class="td-common1" rowspan = "2">
                    <asp:Label ID="Label4" runat="server" Text="Customer Addr2" CssClass="lbl-font-common"></asp:Label>
                </td>
                <td class="td-drpdown" rowspan = "2">
                    <asp:TextBox ID="TextConsumer_Addr2" runat="server"  Rows="2" TextMode="MultiLine" CssClass="txtbox-customers-address1_2"></asp:TextBox>
                </td>
                <td class="td-product">
                    <span class="lbl-font-common">&nbsp;&nbsp;&nbsp;</span><asp:Label ID="Label47" runat="server" Text="Repair Description" CssClass="lbl-font-common"></asp:Label>
                </td>
                <td class="td-drpdown" rowspan = "5">
                    <asp:TextBox ID="TextRepair_Description" runat="server" CssClass="txtbox-repair-description" TextMode="MultiLine" Height="92px" Width="344px" ></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td class="ts-blank1">
                   </td>
            </tr>
            <tr>
                <td class="td-common1">
                    <asp:Label ID="Label43" runat="server" Text="Customer mail address" CssClass="lbl-font-common"></asp:Label>
                </td>
                <td class="td-txtbox-customers">
                    <asp:TextBox ID="TextCustomer_mail_address" runat="server" CssClass="txtbox-customer-state-address-telephone-fax"></asp:TextBox>
                </td>
                <td class="td-blank1">
                    &nbsp;&nbsp;&nbsp;</td>
            </tr>
            <tr>
                <td class="td-common1">
                    <asp:Label ID="Label3" runat="server" Text="Customer Telephone" CssClass="lbl-font-common"></asp:Label>
                </td>
                <td class="td-txtbox-customers">
                    <asp:TextBox ID="TextConsumer_Telephone" runat="server" CssClass="txtbox-customer-state-address-telephone-fax"></asp:TextBox>
                </td>
                <td class="td-blank1">
                   </td>
            </tr>
            <tr>
                <td class="td-common1">
                    <asp:Label ID="Label42" runat="server" Text="Customer Fax" CssClass="lbl-font-common"></asp:Label>
                </td>
                <td class="td-txtbox-customers">
                    <asp:TextBox ID="TextConsumer_Fax" runat="server" CssClass="txtbox-customer-state-address-telephone-fax"></asp:TextBox>
                </td>
                <td class="td-blank1"></td>
            </tr>
            <tr>
                <td class="td-txtbox">
                    <asp:Label ID="Label48" runat="server" Text="Comment" CssClass="lbl-font-common"></asp:Label>
                </td>
                <td class="td-blank1"></td>
                <td class="td-blank1"></td>
                <td class="td-blank1"></td>
            </tr>
            <tr>
                <td colspan = "2">
                    <asp:TextBox ID="TextComment" runat="server" TextMode="MultiLine" CssClass="txtbox-command"></asp:TextBox>
                </td>
                <td></td>
                <td align = "right" valign = "bottom">
                     <asp:ImageButton ID="btnSend" runat="server" ImageUrl="~/icon/send.png"  CssClass="btn-img-start-send-today-postal-code" />
                </td>
            </tr>
        </table>
        <asp:Button ID="BtnCancel" runat="server" Text="Button" style="display:none;" />
        <asp:Button ID="BtnOK" runat="server" Text="Button" style="display:none;"  />
        <asp:Button ID="BtnPdf" runat="server" Text="last" style="display:none;"  />
    </div>
</div>
  <div id="dialog" title="message" style="display:none;"> >
        <asp:Label ID="lblMsg" runat="server" Text=""></asp:Label>
  </div>
</asp:Content>
