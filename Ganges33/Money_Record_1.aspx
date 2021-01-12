<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Site6.Master" CodeBehind="Money_Record_1.aspx.vb" Inherits="Ganges33.Money_Record_1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script src="http://ajax.googleapis.com/ajax/libs/jquery/1.7.0/jquery.min.js"></script>
    <link href="CSS/Common/Monney_Record1.css" rel="stylesheet" />
    <link type="text/css" href="http://ajax.googleapis.com/ajax/libs/jqueryui/1/themes/start/jquery-ui.css" rel="stylesheet">
    <script type="text/javascript" src="http://ajax.googleapis.com/ajax/libs/jqueryui/1/jquery-ui.min.js"></script>
        
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="div-Entrie-tbl" style="background-image: url('pagewall_2/wall_money-2.png')">
        <div class="div-Money-tbl">
        <br />
        <br />
        <table class="tbl_Money">
            <tr>
                <td class="Btn-image-start-send-Export">
                    <asp:ImageButton  ID="btnStart"  runat="server" imageurl="icon/start.png" CSSclass="Btn-image-start-send-Export" Height="59px" Width="163px" />
                </td>
                <td  colspan ="5">
                    <asp:Label ID="lblDate" runat="server" Text="Record date&amp;time : " CssClass="lbl-common"></asp:Label>
                    <asp:Label ID="lblRecord" runat="server" CssClass="lbl-common"></asp:Label><br />
                    <span class="lbl-common"></span><asp:Label ID="lblName" runat="server" Text="yousername: " CssClass="lbl-common"></asp:Label>
                    <asp:Label ID="lblYousername" runat="server" CssClass="lbl-common"></asp:Label>
                </td>
                <td >
                    <asp:Label ID="Label18" runat="server" Text="PO" CssClass="lbl-common"></asp:Label><span class="lbl-common">&nbsp;
                    </span>
                    <asp:TextBox ID="textPo" runat="server"  CssClass="lbl-common" Height="30px" Width="224px"></asp:TextBox>
                </td>    
            </tr>
            <tr>
                <td class="td-Start-Recept_Dt_txtbox-Close_Dt_txtbox-Counter-Repair">
                    <p style="language:ja;margin-top:0pt;margin-bottom:0pt;margin-left:0in;text-indent:0in">
                        <asp:Label ID="Label4" runat="server" Text="Recept D&amp;T" CssClass="lbl-common"></asp:Label>
                    </p>
                </td>
                <td class="td-Start-Recept_Dt_txtbox-Close_Dt_txtbox-Counter-Repair">
                    <asp:TextBox ID="textReceptYMD" runat="server"  CssClass="txtbox1-Recept-close-Account-ASC-smg " ></asp:TextBox>
                    </td>
                <td align ="right">
                    <asp:TextBox ID="textReceptH" runat="server"  CssClass="txtbox2-Recept-close " ></asp:TextBox>
                    <span >:</span></td>
                <td class="td-Recept_txtbox1_2-Close_txtbox1_2">
                     <asp:TextBox ID="textReceptM" runat="server" CssClass="txtbox2-Recept-close "   ></asp:TextBox>
                </td>
                <td class="td-Blank1">
                    <p style="language:ja;margin-top:0pt;margin-bottom:0pt;margin-left:0in;text-indent:0in">
                        &nbsp;</p>
                </td>
                <td class="td-Denomination-Amount-Asc-clim-Samsung">
                        <asp:Label ID="Label13" runat="server" Text="Denomination" CssClass="lbl-common"></asp:Label>
                    </td>
                <td class="td-denomination_txtbox-Amt_txtbox-ASC_txtbox-Smg_Climp_No">
                    <asp:DropDownList ID="DropListDenomination" runat="server" cssclass="Drpdowntxtbox-Counter-Repair-Recep">
                    </asp:DropDownList>
                </td>
          
            </tr>
            <tr>
                <td class="td-Start-Recept_Dt_txtbox-Close_Dt_txtbox-Counter-Repair">
                    <p style="language:ja;margin-top:0pt;margin-bottom:0pt;margin-left:0in;text-indent:0in">
                        <asp:Label ID="Label2" runat="server" Text="close D&amp;T" CssClass="lbl-common"></asp:Label>
                    </p>
                </td>
                <td class="td-Start-Recept_Dt_txtbox-Close_Dt_txtbox-Counter-Repair">
                    <asp:TextBox ID="textCloseYMD" runat="server"  CssClass="txtbox1-Recept-close-Account-ASC-smg "  ></asp:TextBox>
                    </td>
                <td align ="right">
                    <asp:TextBox ID="textCloseH" runat="server" CssClass="txtbox2-Recept-close"></asp:TextBox>
                    <span >:</span></td>
                <td class="td-Recept_txtbox1_2-Close_txtbox1_2">
                    <asp:TextBox ID="textCloseM" runat="server" CssClass="txtbox2-Recept-close"  ></asp:TextBox>
                </td>
                <td class="td-Blank1">
                    </td>
                <td class="td-Denomination-Amount-Asc-clim-Samsung">
                    <asp:Label ID="Label14" runat="server" Text="Amount" CssClass="lbl-common"></asp:Label>
                </td>
                <td class="td-denomination_txtbox-Amt_txtbox-ASC_txtbox-Smg_Climp_No">
                    <asp:TextBox ID="textAmount" runat="server" cssClass="txtbox1-Recept-close-Account-ASC-smg " Width="269px" ></asp:TextBox>
                </td>
            </tr>
             <tr>
                 <td class="td-Blank2" >
                 </td>
             </tr>

            <tr>
                <td class="td-Start-Recept_Dt_txtbox-Close_Dt_txtbox-Counter-Repair">
                    <asp:Label ID="Label3" runat="server" Text="Counter Receptionist" CssClass="lbl-common"></asp:Label>
                </td>
                <td class="td-blank1" colspan ="3">
                    <asp:DropDownList ID="DropListCounter" runat="server" CssClass="Drpdowntxtbox-Counter-Repair-Recep" />
                   
                </td>
                <td class="td-Blank1"></td>
                <td class="td-Denomination-Amount-Asc-clim-Samsung">
                    <asp:Label ID="Label15" runat="server" Text="ASC Claim No." CssClass="lbl-common"></asp:Label>
                </td>
                <td class="td-denomination_txtbox-Amt_txtbox-ASC_txtbox-Smg_Climp_No">
                    <asp:TextBox ID="textASCClaimNo" runat="server"  CssClass="txtbox1-Recept-close-Account-ASC-smg " Width="268px" ></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td class="td-Start-Recept_Dt_txtbox-Close_Dt_txtbox-Counter-Repair">
                    <p style="language:ja;margin-top:0pt;margin-bottom:0pt;margin-left:0in;text-indent:0in">
                        <asp:Label ID="Label5" runat="server" Text="RepairReceptionist" CssClass="lbl-common"></asp:Label>
                    </p>
                </td>
                <td class="td-blank1" colspan ="3">
                    <asp:DropDownList ID="DropListRepair" runat="server" CssClass="Drpdowntxtbox-Counter-Repair-Recep">
                    </asp:DropDownList>
                </td>
                <td class="td-Blank1"></td>
                <td class="td-Denomination-Amount-Asc-clim-Samsung">
                    <asp:Label ID="lblSamsungClaimNo" runat="server" Text="Samsung Claim No." CssClass="lbl-common"></asp:Label>
                </td>
                <td class="td-denomination_txtbox-Amt_txtbox-ASC_txtbox-Smg_Climp_No">
                    <asp:TextBox ID="textSamsungClaimNo" runat="server" CssClass="txtbox1-Recept-close-Account-ASC-smg " Width="265px"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td class="td-DataImport" colspan ="2">
                    <asp:Label ID="Label1" runat="server" Text="Data Import" CssClass="lbl-Data-import"></asp:Label>
                    <asp:CheckBox ID="CheckGSPNImport" runat="server" CssClass="lbl-common" />
                    <asp:Label ID="Label17" runat="server" Text="GSPN Data" CssClass="lbl-common"></asp:Label><br class="lbl-common" />
                    <asp:FileUpload ID="FileUploadGSPN" runat="server"  CssClass="lbl-common" />
                </td>
                <td class="td-Import_btn" colspan ="2">
                    <br />
                    <asp:ImageButton ID="btnCsv" runat="server" ImageUrl="~/icon/import.png" CssClass="Btn-image-start-send-Export" Height="59px" Width="163px" />
                </td>
                <td class="td-Blank1"></td>
                <td >
                    <asp:Label ID="Label16" runat="server" Text=" Comment" CssClass="lbl-common"></asp:Label>
                </td>
                <td >
                    <asp:TextBox ID="TextComment" runat="server"  TextMode="MultiLine" CssClass="lbl-common" Height="96px" Width="349px"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td align ="right" colspan ="7" >
                    <asp:ImageButton ID="btnSend" runat="server"  ImageUrl="~/icon/send.png" CssClass="Btn-image-start-send-Export" Height="59px" Width="163px" />
                </td>
            </tr>
        </table>
         <asp:Button ID="BtnCancel" runat="server" Text="Button" style="display:none;"/>
        <asp:Button ID="BtnOK" runat="server" Text="Button" style="display:none;" />
      </div>
    </div>

    <div id="dialog" title="message" style="display:none;"> >
        <asp:Label ID="lblMsg" runat="server" Text=""></asp:Label>
    </div>

    <%-- <script type="text/javascript">  ASP側に埋め込み済
        $(function () {
            $("#dialog" ).dialog({
                width: 400,
                buttons:
                {
                    "OK": function () {
                        $(this).dialog('close');
                        $('[id$="BtnOK"]').click();
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