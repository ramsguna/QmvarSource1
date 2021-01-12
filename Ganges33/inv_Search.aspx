<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Site3.Master" CodeBehind="inv_Search.aspx.vb" Inherits="Ganges33.inv_Search" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="ajaxToolkit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script src="http://ajax.googleapis.com/ajax/libs/jquery/1.7.0/jquery.min.js"></script>
    <link href="CSS/Common/Inv_search.css" rel="stylesheet" />
    <link type="text/css" href="http://ajax.googleapis.com/ajax/libs/jqueryui/1/themes/start/jquery-ui.css" rel="stylesheet">
    <script type="text/javascript" src="http://ajax.googleapis.com/ajax/libs/jqueryui/1/jquery-ui.min.js"></script>
 </asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="div-Entire-tbl" style="background-image: url('pagewall_2/wall_inventry-2.png');">
        <div class="div-search-tbl">
           <table class="tbl-search-tbl">
                <tr>
                    <td class="td-blank1"></td>
                    <td class="td-chk-use-serial">
                        <asp:CheckBox ID="chkUseSerial" runat="server" AutoPostBack="True" CssClass="td-btn-Imgae " />
                        <asp:Label ID="Label18" runat="server" Text="use Serial" CssClass="td-btn-Imgae "></asp:Label>
                     </td>
                    <td class="td-chk-unuseserial">
                        <asp:CheckBox ID="chkUnUseSerial" runat="server" AutoPostBack="True" CssClass="td-btn-Imgae " />
                        <asp:Label ID="Label19" runat="server" Text="unuse Serial" CssClass="td-btn-Imgae "></asp:Label>
                    </td>
                    <td class="td-blank2"></td>
                    <td class="td-blank3"></td>
                </tr>
                <tr>
                <td class="td-blank1"></td>
                <td class="td-lbl-delivery-date">
                    <asp:Label ID="Label1" runat="server" Text="Delivery Date" CssClass="td-btn-Imgae "></asp:Label>
                </td>
                    <td class="td-txtbox-from">
                        <span class="td-btn-Imgae ">From</span>
                        <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
                        <asp:TextBox ID="TextScriptStartDate" runat="server" CssClass="td-btn-Imgae "></asp:TextBox>
                        <ajaxToolkit:CalendarExtender ID="TextScriptStartDate_CalendarExtender" runat="server"  BehaviorID="TextBox1_CalendarExtender" TargetControlID="TextScriptStartDate" PopupPosition="Left">
                        </ajaxToolkit:CalendarExtender>
                        <span class="td-btn-Imgae ">
                        <%--<ajaxToolkit:CalendarExtender ID="CalendarExtender1" runat="server" CssClass="ajax__calendar" TargetControlID="***" PopupButtonID="***Image" />--%>
                        </span>
                    </td>
                    <td class="td-txtbox-to">
                        <span class="td-btn-Imgae ">To
                        </span>
                        <asp:TextBox ID="TextScriptEndDate" runat="server" CssClass="td-btn-Imgae "></asp:TextBox>
                        <ajaxToolkit:CalendarExtender ID="TextScriptEndDate_CalendarExtender" runat="server" BehaviorID="TextBox2_CalendarExtender" TargetControlID="TextScriptEndDate" PopupPosition="Right">
                        </ajaxToolkit:CalendarExtender>
                    </td>
                    <td class="td-blank4"></td>
                    <td class="td-btn-start">
                        <asp:ImageButton ID="btnStart" runat="server" Height="59px" Width="163px" CssClass="btn-img" ImageUrl="~/icon/start.png" />
                    </td>
                </tr>
                <br />        
            </table>
        <table class="tbl-parts-No_to_status">
            <tr>
                <td class="td-blank5"></td>
                <td class="td-lbl-parts-No_to_status">
                    <asp:Label ID="Label20" runat="server" Text="Parts number" CssClass="td-btn-Imgae "></asp:Label>
                </td>
                <td class="td-txtbox-parts-No_to_status">
                    <asp:TextBox ID="TextPartsNnumber" runat="server" CssClass="td-btn-Imgae "></asp:TextBox>
                </td>
                <td class="td-lbl-parts-No_to_status">
                    <asp:Label ID="Label21" runat="server" Text="arrival number" CssClass="td-btn-Imgae "></asp:Label>
                </td>
                <td class="ttd-txtbox-parts-No_to_status">
                    <asp:TextBox ID="TextArrivalNumber" runat="server" CssClass="td-btn-Imgae "></asp:TextBox>
                </td>
                <td class="td-lbl-parts-No_to_status">
                    <asp:Label ID="Label22" runat="server" Text="Delivery number" CssClass="td-btn-Imgae "></asp:Label>
                </td>
                <td class="td-txtbox-parts-No_to_status">
                    <asp:TextBox ID="TextDeliveryNumber" runat="server" CssClass="td-btn-Imgae "></asp:TextBox>
                </td>
            </tr>
             <tr>
                <td class="td-blank5"></td>
                <td class="td-lbl-parts-No_to_status">
                    <asp:Label ID="Label23" runat="server" Text="Serial number" CssClass="td-btn-Imgae "></asp:Label>
                 </td>
                <td class="td-txtbox-parts-No_to_status">
                    <asp:TextBox ID="TextSerialNumber" runat="server" CssClass="td-btn-Imgae "></asp:TextBox>
                 </td>
                <td class="td-lbl-parts-No_to_status">
                    <asp:Label ID="Label24" runat="server" Text="Invoice number" CssClass="td-btn-Imgae "></asp:Label>
                 </td>
                <td class="td-txtbox-parts-No_to_status">
                    <asp:TextBox ID="TextInvoiceNumber" runat="server" CssClass="td-btn-Imgae "></asp:TextBox>
                 </td>
                <td class="td-lbl-parts-No_to_status">
                    <asp:Label ID="Label25" runat="server" Text="Return Delivery no." CssClass="td-btn-Imgae "></asp:Label>
                 </td>
                <td>
                    <asp:TextBox ID="TextReturnDeliveryNo" runat="server" CssClass="td-btn-Imgae "></asp:TextBox>
                 </td>
            </tr>
            <tr>
                <td class="td-blank5"></td>
                <td class="td-lbl-parts-No_to_status">
                    <asp:Label ID="Label26" runat="server" Text="Return number" CssClass="td-btn-Imgae "></asp:Label>
                </td>
                <td class="td-txtbox-parts-No_to_status">
                    <asp:TextBox ID="TextReturnNo" runat="server" CssClass="td-btn-Imgae "></asp:TextBox>
                </td>
                <td class="td-lbl-parts-No_to_status">
                    <asp:Label ID="Label27" runat="server" Text="PO number" CssClass="td-btn-Imgae "></asp:Label>
                </td>
                <td class="td-txtbox-parts-No_to_status">
                    <asp:TextBox ID="TextPONumber" runat="server" CssClass="td-btn-Imgae "></asp:TextBox>
                </td>
                <td class="td-lbl-parts-No_to_status">
                    <asp:Label ID="Label28" runat="server" Text="parts status" CssClass="td-btn-Imgae "></asp:Label>
                </td>
                <td>
                    <asp:DropDownList ID="ddlPartsStatus" runat="server" Height="16px" Width="119px" CssClass="td-btn-Imgae ">
                        <asp:ListItem>selectStatus</asp:ListItem>
                        <asp:ListItem>unuse Stock</asp:ListItem>
                        <asp:ListItem>Warehouse Stock</asp:ListItem>
                        <asp:ListItem>Engineer Stock</asp:ListItem>
                        <asp:ListItem>Return</asp:ListItem>
                    </asp:DropDownList>
                </td>
            </tr>
        </table>
        <br />
        <table>
            <tr>
                <td><asp:CheckBox ID="CheckDefault" runat="server" AutoPostBack="True" CssClass="td-btn-Imgae " /><asp:Label ID="Label2" runat="server" Text="Default" CssClass="td-btn-Imgae "></asp:Label>
                    <asp:CheckBox ID="Check20Page" runat="server" AutoPostBack="True" CssClass="td-btn-Imgae " /><asp:Label ID="Label3" runat="server" Text="20Page" CssClass="td-btn-Imgae "></asp:Label>
                    <asp:CheckBox ID="Check100Page" runat="server" AutoPostBack="True" CssClass="td-btn-Imgae " /> <asp:Label ID="Label4" runat="server" Text="100Page" CssClass="td-btn-Imgae "></asp:Label></td>
            </tr>
        </table>
           <div >
                <table>
                    <tr>
                        <td class="td-btn-Imgae ">&nbsp; </td>
                        <td>
                            <asp:GridView ID="grd_info" runat="server" AllowPaging="True" EmptyDataText="検索対象がありませんでした。" Width="1237px" CssClass="Font-hidden" >
                                <Columns>
                                    <asp:ButtonField CommandName="Detail" HeaderText="Detail" ShowHeader="True" Text="partsNo" />
                                </Columns>
                                <HeaderStyle BackColor="#66FFFF" />
                                <RowStyle Height="25px" Wrap="False"/>
                            </asp:GridView>
                        </td>
                    </tr>
                </table>
           </div>
        </div>
    </div>
    
    <div id="dialog" title="message" style="display:none;"> >
        <asp:Label ID="lblMsg" runat="server" Text=""></asp:Label>
    </div>

</asp:Content>
