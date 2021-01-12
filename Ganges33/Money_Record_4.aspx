<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Site5.Master" CodeBehind="Money_Record_4.aspx.vb" Inherits="Ganges33.Money_Record_4" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script src="http://ajax.googleapis.com/ajax/libs/jquery/1.7.0/jquery.min.js"></script>
 <link href="CSS/Common/Money-Record-4.css" rel="stylesheet" />
    <link type="text/css" href="http://ajax.googleapis.com/ajax/libs/jqueryui/1/themes/start/jquery-ui.css" rel="stylesheet">
    <script type="text/javascript" src="http://ajax.googleapis.com/ajax/libs/jqueryui/1/jquery-ui.min.js"></script>
    
               
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="div-entire-tbl"　id = "ichiKotei" style="background-image: url('pagewall_2/wall_money-2.png')" >
    <div class="div-money-record4">
        <table class="tbl-money-record4">
            <tr>
                <td class="td-btn-start">
                    <asp:ImageButton ID="btnStart" runat="server" cssclass="btn-return-start-send-back-calculation" ImageUrl="~/icon/start.png"  />
                </td>
                <td class="td-date-time">
                    <asp:Label ID="lblMode" runat="server" Text="Reading mode" CssClass="lbl-readingmode"></asp:Label>
                    <asp:Label ID="lblDate" runat="server" Text="Record date&amp;time : " CssClass="lbl-font-common"></asp:Label>
                    <asp:Label ID="lblRecord" runat="server" CssClass="td-lbl-font-common"></asp:Label><br class="td-lbl-font-common" />
                    <asp:Label ID="Label6" runat="server" Text="yousername: " CssClass="lbl-font-common"></asp:Label>
                    <asp:Label ID="lblName" runat="server" CssClass="td-lbl-font-common"></asp:Label>
                </td>
                <td class="td-btn-return" align = "right">
                    <asp:Button ID="Return" runat="server" Text="Return" BackColor="#CCFF99" CssClass="btn-return" />;
                    <%--<asp:Label ID="Label80" runat="server" Text="Po" CssClass="td-lbl-font-common"></asp:Label>--%>
                </td>
                <td class="td-lbl-po">
                    <asp:Label ID="Label80" runat="server" Text="PO : " CssClass="btn-return"></asp:Label>
                    <asp:TextBox ID="TextPo" runat="server" cssclass="txtbox-po"></asp:TextBox>
                    <br /><br />
                    <asp:Label ID="Label11" runat="server" Text="reception date : " CssClass="lbl-font-common">

                    </asp:Label><asp:Label ID="lblReceptionDate" runat="server" CssClass="lbl-font-common"></asp:Label>
                </td>
            </tr>
            <tr>
                <td class="td-warranty-to-command">
                    <asp:Label ID="Label39" runat="server" Text="warranty" CssClass="lbl-font-common"></asp:Label>
                </td>
                <td class="td-txt-box-warranty-to-command">
                    <asp:DropDownList ID="DropListWarranty" runat="server" cssclass="txtbox-warranty-recept-customer-state-email-telephone-tax">
                    </asp:DropDownList>
                </td>
                <td class="td-blank-lbl-producttype-to-description"></td>
                <td class="td-blank-lbl-producttype-to-description"></td>
            </tr>
            <tr>
                <td class="td-warranty-to-command">
                    <asp:Label ID="Label5" runat="server" Text="Recept D&amp;T" CssClass="lbl-font-common"></asp:Label>
                </td>
                <td class="td-txt-box-warranty-to-command">
                    <asp:TextBox ID="TextRepair_Received_Date" runat="server" cssclass="txtbox-warranty-recept-customer-state-email-telephone-tax"></asp:TextBox>
                </td>
                <td class="td-blank-lbl-producttype-to-description">
                   <asp:Label ID="Label9" runat="server" Text="Product type" CssClass="lbl-font-common"></asp:Label>
                </td>
                <td class="td-txtbox-producttype-imei-serial-marker-model">
                    <asp:DropDownList ID="DropListProductType" runat="server" cssclass="txtbox-product-serial-marker-imei-model">
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td class="td-warranty-to-command">
                    <asp:Label ID="Label2" runat="server" Text="Customer name" CssClass="lbl-font-common"></asp:Label>
                </td>
                <td class="td-txt-box-warranty-to-command">
                    <asp:TextBox ID="TextConsumer_Name" runat="server" cssclass="txtbox-warranty-recept-customer-state-email-telephone-tax"></asp:TextBox>
                </td>
                <td class="td-blank-lbl-producttype-to-description"><asp:Label ID="Label8" runat="server" Text="Serial No" CssClass="lbl-font-common"></asp:Label>
                </td>
                <td class="td-txtbox-producttype-imei-serial-marker-model">
                    <asp:TextBox ID="TextSerial_No" runat="server" cssclass="txtbox-product-serial-marker-imei-model"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td class="td-warranty-to-command">
                    <asp:Label ID="Label10" runat="server" Text="Postal Code" CssClass="lbl-font-common"></asp:Label>
                </td>
                <td class="td-txt-box-warranty-to-command">
                    <asp:TextBox ID="TextPostal_Code2" runat="server" CssClass="txtbox-postalcode" ></asp:TextBox>
                    <asp:Button ID="btnPostal" runat="server" Height="29px" Text="〒" Width="81px" BackColor="#CCFF99" />
                </td>
                <td class="td-blank-lbl-producttype-to-description"></td>
                <td class="td-blank-lbl-producttype-to-description"></td>
            </tr>
            <tr>
                <td class="td-warranty-to-command">
                    <asp:Label ID="Label1" runat="server" Text="state" CssClass="lbl-font-common"></asp:Label>
                </td>
                <td class="td-txt-box-warranty-to-command">
                    <asp:DropDownList ID="DropListState" runat="server" cssclass="txtbox-warranty-recept-customer-state-email-telephone-tax">
                    </asp:DropDownList>
                </td>
                <td class="td-blank-lbl-producttype-to-description"></td>
                <td class="td-blank-lbl-producttype-to-description"></td>
            </tr>

            <tr>
                <td class="td-warranty-to-command">
                    <asp:Label ID="Label3" runat="server" Text="Customer Addr1" CssClass="lbl-font-common"></asp:Label>
                </td>
                <td class="td-txt-box-warranty-to-command" rowspan = "2">
                    <asp:TextBox ID="TextConsumer_Addr1" runat="server" cssclass="txtbox-customer-address-comments" Rows="2" TextMode="MultiLine" MaxLength="200" TabIndex="3"></asp:TextBox>
                </td>
                <td class="td-blank-lbl-producttype-to-description" >
                    <asp:Label ID="Label75" runat="server" Text="IMEI number" CssClass="lbl-font-common"></asp:Label>
                </td>
                <td class="td-txtbox-producttype-imei-serial-marker-model">
                    <asp:TextBox ID="TextIMEI_No" runat="server" cssclass="txtbox-product-serial-marker-imei-model"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td class="td-warranty-to-command">
                    </td>
                <td class="td-blank-lbl-producttype-to-description">
                    <asp:Label ID="Label76" runat="server" Text="Maker" CssClass="lbl-font-common"></asp:Label>
                </td>
                <td class="td-txtbox-producttype-imei-serial-marker-model">
                    <asp:TextBox ID="TextMaker" runat="server" cssclass="txtbox-product-serial-marker-imei-model"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td class="td-warranty-to-command">
                    <asp:Label ID="Label4" runat="server" Text="Customer Addr2" CssClass="lbl-font-common"></asp:Label>
                </td>
                <td class="td-txt-box-warranty-to-command" rowspan = "2">
                    <asp:TextBox ID="TextConsumer_Addr2" runat="server" cssclass="txtbox-customer-address-comments" Rows="2" TextMode="MultiLine" MaxLength="200"></asp:TextBox>
                </td>
                <td class="td-blank-lbl-producttype-to-description" >
                    <asp:Label ID="Label81" runat="server" Text="Model" CssClass="lbl-font-common"></asp:Label>
                </td>
                <td class="td-txtbox-producttype-imei-serial-marker-model">
                    <asp:TextBox ID="TextModel" runat="server" cssclass="txtbox-product-serial-marker-imei-model"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td class="td-warranty-to-command">
                    </td>
                <td class="td-blank-lbl-producttype-to-description">
                   <asp:Label ID="Label125" runat="server" Text="Repair Description" CssClass="lbl-font-common"></asp:Label>
                   </td>
                <td class="td-txtbox-producttype-imei-serial-marker-model" rowspan = "8" >
                    <asp:TextBox ID="TextRepair_Description" runat="server" CssClass="txtbox-command" TextMode="MultiLine" ></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td class="td-warranty-to-command">
                    <asp:Label ID="Label7" runat="server" Text="Customer e-mail" CssClass="lbl-font-common"></asp:Label>
                </td>
                <td class="td-txt-box-warranty-to-command">
                    <asp:TextBox ID="TextCustomer_mail_address" runat="server" cssclass="txtbox-warranty-recept-customer-state-email-telephone-tax"></asp:TextBox>
                </td>
                <td class="td-blank-lbl-producttype-to-description">
                  </td>
            </tr>
            <tr>
                <td class="td-warranty-to-command">
                    <asp:Label ID="Label37" runat="server" Text="Customer Telephone" CssClass="lbl-font-common"></asp:Label>
                </td>
                <td class="td-txt-box-warranty-to-command">
                    <asp:TextBox ID="TextConsumer_Telephone" runat="server" cssclass="txtbox-warranty-recept-customer-state-email-telephone-tax"></asp:TextBox>
                </td>
                <td class="td-blank-lbl-producttype-to-description">
                    </td>
            </tr>
            <tr>
                <td class="td-warranty-to-command">
                    <asp:Label ID="Label86" runat="server" Text="Customer Fax" CssClass="lbl-font-common"></asp:Label>
                </td>
                <td  class="td-txt-box-warranty-to-command" >
                    <asp:TextBox ID="TextConsumer_Fax" runat="server" cssclass="txtbox-warranty-recept-customer-state-email-telephone-tax"></asp:TextBox>
                </td>
                <td class="td-blank-lbl-producttype-to-description">
                   </td>
            </tr>
            <tr>
                <td class="td-warranty-to-command">
                    <asp:Label ID="Label87" runat="server" CssClass="td-lbl-font-common" Text="Comment1"></asp:Label>
                </td>
                <td class="td-txt-box-warranty-to-command" rowspan = "4"> 
                    <asp:TextBox ID="TextComment" runat="server" cssclass="txtbox-customer-address-comments" TextMode="MultiLine"></asp:TextBox>
                </td>
             </tr>        </table>
        <br />
        <br />
        <table class="auto-style301">
             <tr>
                <td class="auto-style358">
                </td>
                <td class="auto-style380">
                    <asp:Label ID="Label98" runat="server" CssClass="auto-style174" Text="Parts No"></asp:Label>
                 </td>
                <td class="auto-style360">
                    <asp:Label ID="Label99" runat="server" CssClass="auto-style174" Text="qty"></asp:Label>
                 </td>
                <td class="auto-style396">
                    <asp:Label ID="Label100" runat="server" CssClass="auto-style174" Text="cost"></asp:Label>
                 </td>
                 <td class="auto-style462"></td>
                <td class="auto-style396">
                    <asp:Label ID="Label101" runat="server" CssClass="auto-style174" Text="salesprice"></asp:Label>
                 </td>
                <td class="auto-style396">
                    <asp:Label ID="Label102" runat="server" CssClass="auto-style174" Text="SGST"></asp:Label>
                 </td>
                <td class="auto-style454">
                    <asp:Label ID="Label103" runat="server" CssClass="auto-style174" Text="CGST"></asp:Label>
                 </td>
                <td class="auto-style396">
                    <asp:Label ID="Label104" runat="server" CssClass="auto-style174" Text="IGST"></asp:Label>
                 </td>
                <td class="auto-style396">
                    <asp:Label ID="Label105" runat="server" CssClass="auto-style174" Text="total"></asp:Label>
                 </td>
                <td class="auto-style367"></td>
            </tr>
            <tr>
                <td class="auto-style386">
                    <asp:Label ID="Label88" runat="server" CssClass="auto-style174" Text="①Labor Amount"></asp:Label>
                </td>
                <td class="auto-style387">
                    <asp:TextBox ID="TextLaborAmount" runat="server" Height="21px" Width="213px" CssClass="auto-style174"></asp:TextBox>
                </td>
                <td class="auto-style388">
                    <asp:TextBox ID="TextQtyLabor" runat="server" Height="21px" Width="53px" CssClass="auto-style174"></asp:TextBox>
                </td>
                <td class="auto-style409">
                    <asp:Label ID="lblPriceLabor" runat="server" CssClass="auto-style174"></asp:Label>
                </td>
                <td class="auto-style462"></td>
                <td class="auto-style409">
                    <asp:Label ID="lblGPriceLa" runat="server" CssClass="auto-style174"></asp:Label>
                </td>
                <td class="auto-style410">
                    <asp:Label ID="lblUTSGTLa" runat="server" CssClass="auto-style174"></asp:Label>
                </td>
                <td class="auto-style455">
                    <asp:Label ID="lblCGSTLa" runat="server" CssClass="auto-style174"></asp:Label>
                </td>
                <td class="auto-style410">
                    <asp:Label ID="lblIGSTLa" runat="server" CssClass="auto-style174"></asp:Label>
                </td>
                <td class="auto-style411">
                    <asp:Label ID="lblSum1" runat="server" CssClass="auto-style174"></asp:Label>
                </td>
                <td class="auto-style439"></td>
                <td class="auto-style390">
                    <asp:Label ID="Label106" runat="server" CssClass="auto-style174" Text="delivery date"></asp:Label>
                </td>
            </tr>
            <tr>
                <td class="auto-style391"></td>
                <td class="auto-style392"></td>
                <td class="auto-style393"></td>
                <td class="auto-style398"></td>
                <td class="auto-style462"></td>
                <td class="auto-style398"></td>
                <td class="auto-style398"></td>
                <td class="auto-style456"></td>
                <td class="auto-style398"></td>
                <td class="auto-style398"></td>
                <td class="auto-style439"></td>
                <td class="auto-style395">
                    <asp:TextBox ID="TextDelivery_Date" runat="server" CssClass="auto-style174"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td class="auto-style386">
                    <asp:Label ID="Label89" runat="server" CssClass="auto-style174" Text="②Parts 1"></asp:Label>
                </td>
                <td class="auto-style387">
                    <asp:TextBox ID="TextParts1" runat="server" Height="21px" Width="213px" CssClass="auto-style174"></asp:TextBox>
                </td>
                <td class="auto-style388">
                    <asp:TextBox ID="TextQtyParts1" runat="server" Height="21px" Width="53px" CssClass="auto-style174"></asp:TextBox>
                </td>
                <td class="auto-style409">
                    <asp:Label ID="lblPricePa1" runat="server" CssClass="auto-style174"></asp:Label>
                </td>
                <td class="auto-style462"></td>
                <td class="auto-style409">
                    <asp:Label ID="lblGPricePa1" runat="server" CssClass="auto-style174"></asp:Label>
                </td>
                <td class="auto-style410">
                    <asp:Label ID="lblp1SGST" runat="server" CssClass="auto-style174"></asp:Label>
                </td>
                <td class="auto-style455">
                    <asp:Label ID="lblp1CGST" runat="server" CssClass="auto-style174"></asp:Label>
                </td>
                <td class="auto-style410">
                    <asp:Label ID="lblp1IGST" runat="server" CssClass="auto-style174"></asp:Label>
                </td>
                <td class="auto-style411">
                    <asp:Label ID="lblSum2_1" runat="server" CssClass="auto-style174"></asp:Label>
                </td>
                <td class="auto-style453"></td>
                <td class="auto-style390">
                    <asp:Label ID="Label74" runat="server" CssClass="auto-style174" Text="Complete date"></asp:Label>
                </td>
            </tr>
            <tr>
                <td class="auto-style386">
                    <asp:Label ID="Label90" runat="server" CssClass="auto-style174" Text="②Parts 2"></asp:Label>
                </td>
                <td class="auto-style387">
                    <asp:TextBox ID="TextParts2" runat="server" Height="21px" Width="213px" CssClass="auto-style174"></asp:TextBox>
                </td>
                <td class="auto-style388">
                    <asp:TextBox ID="TextQtyParts2" runat="server" Height="21px" Width="53px" CssClass="auto-style174"></asp:TextBox>
                </td>
                <td class="auto-style409">
                    <asp:Label ID="lblPricePa2" runat="server" CssClass="auto-style174"></asp:Label>
                </td>
                <td class="auto-style462"></td>
                <td class="auto-style409">
                    <asp:Label ID="lblGPricePa2" runat="server" CssClass="auto-style174"></asp:Label>
                </td>
                <td class="auto-style410">
                    <asp:Label ID="lblp2SGST" runat="server" CssClass="auto-style174"></asp:Label>
                </td>
                <td class="auto-style455">
                    <asp:Label ID="lblp2CGST" runat="server" CssClass="auto-style174"></asp:Label>
                </td>
                <td class="auto-style410">
                    <asp:Label ID="lblp2IGST" runat="server" CssClass="auto-style174"></asp:Label>
                </td>
                <td class="auto-style411">
                    <asp:Label ID="lblSum2_2" runat="server" CssClass="auto-style174"></asp:Label>
                </td>
                <td class="auto-style453"></td>
                <td class="auto-style390">
                    <asp:TextBox ID="TextCompleted_Date" runat="server" CssClass="auto-style174"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td class="auto-style386">
                    <asp:Label ID="Label91" runat="server" CssClass="auto-style174" Text="②Parts 3"></asp:Label>
                </td>
                <td class="auto-style387">
                    <asp:TextBox ID="TextParts3" runat="server" Height="21px" Width="213px" CssClass="auto-style174"></asp:TextBox>
                </td>
                <td class="auto-style388">
                    <asp:TextBox ID="TextQtyParts3" runat="server" Height="21px" Width="53px" CssClass="auto-style174"></asp:TextBox>
                </td>
                <td class="auto-style409">
                    <asp:Label ID="lblPricePa3" runat="server" CssClass="auto-style174"></asp:Label>
                </td>
                <td class="auto-style462"></td>
                <td class="auto-style409">
                    <asp:Label ID="lblGPricePa3" runat="server" CssClass="auto-style174"></asp:Label>
                </td>
                <td class="auto-style410">
                    <asp:Label ID="lblp3SGST" runat="server" CssClass="auto-style174"></asp:Label>
                </td>
                <td class="auto-style455">
                    <asp:Label ID="lblp3CGST" runat="server" CssClass="auto-style174"></asp:Label>
                </td>
                <td class="auto-style410">
                    <asp:Label ID="lblp3IGST" runat="server" CssClass="auto-style174"></asp:Label>
                </td>
                <td class="auto-style411">
                    <asp:Label ID="lblSum2_3" runat="server" CssClass="auto-style174"></asp:Label>
                </td>
                <td class="auto-style453"></td>
                <td class="auto-style390">
                    <asp:Label ID="Label109" runat="server" CssClass="auto-style174" Text="rec datetime"></asp:Label>
                </td>
            </tr>
            <tr>
                <td class="auto-style386">
                    <asp:Label ID="Label92" runat="server" CssClass="auto-style174" Text="②Parts 4"></asp:Label>
                </td>
                <td class="auto-style387">
                    <asp:TextBox ID="TextParts4" runat="server" Height="21px" Width="213px" CssClass="auto-style174"></asp:TextBox>
                </td>
                <td class="auto-style388">
                    <asp:TextBox ID="TextQtyParts4" runat="server" Height="21px" Width="53px" CssClass="auto-style174"></asp:TextBox>
                </td>
                <td class="auto-style409">
                    <asp:Label ID="lblPricePa4" runat="server" CssClass="auto-style174"></asp:Label>
                </td>
                <td class="auto-style462"></td>
                <td class="auto-style409">
                    <asp:Label ID="lblGPricePa4" runat="server" CssClass="auto-style174"></asp:Label>
                </td>
                <td class="auto-style444">
                    <asp:Label ID="lblp4SGST" runat="server"></asp:Label>
                </td>
                <td class="auto-style457">
                    <asp:Label ID="lblp4CGST" runat="server"></asp:Label>
                </td>
                <td class="auto-style444">
                    <asp:Label ID="lblp4IGST" runat="server"></asp:Label>
                </td>
                <td class="auto-style443">
                    <asp:Label ID="lblSum2_4" runat="server"></asp:Label>
                </td>
                <td class="auto-style453"></td>
                <td class="auto-style390">
                    <asp:TextBox ID="TextRec_datetime" runat="server" CssClass="auto-style174">
</asp:TextBox>
                </td>
            </tr>
            <tr>
                <td class="auto-style386">
                    <asp:Label ID="Label93" runat="server" CssClass="auto-style174" Text="②Parts 5"></asp:Label>
                </td>
                <td class="auto-style387">
                    <asp:TextBox ID="TextParts5" runat="server" Height="21px" Width="213px" CssClass="auto-style174"></asp:TextBox>
                </td>
                <td class="auto-style388">
                    <asp:TextBox ID="TextQtyParts5" runat="server" Height="21px" Width="53px" CssClass="auto-style174"></asp:TextBox>
                </td>
                <td class="auto-style409">
                    <asp:Label ID="lblPricePa5" runat="server" CssClass="auto-style174"></asp:Label>
                </td>
                <td class="auto-style471"></td>
                <td class="auto-style409">
                    <asp:Label ID="lblGPricePa5" runat="server" CssClass="auto-style174"></asp:Label>
                </td>
                <td class="auto-style410">
                    <asp:Label ID="lblp5SGST" runat="server" CssClass="auto-style174"></asp:Label>
                </td>
                <td class="auto-style455">
                    <asp:Label ID="lblp5CGST" runat="server" CssClass="auto-style174"></asp:Label>
                </td>
                <td class="auto-style410">
                    <asp:Label ID="lblp5IGST" runat="server" CssClass="auto-style174"></asp:Label>
                </td>
                <td class="auto-style411">
                    <asp:Label ID="lblSum2_5" runat="server" CssClass="auto-style174"></asp:Label>
                </td>
                <td class="auto-style453"></td>
                <td class="auto-style390">
                    <asp:Label ID="Label122" runat="server" CssClass="auto-style174" Text="Denomination"></asp:Label>
                </td>
            </tr>
            <tr>
                <td class="auto-style404">
                    <asp:Label ID="Label95" runat="server" CssClass="auto-style174" Text="③parts amount"></asp:Label>
                </td>
                <td class="auto-style405"></td>
                <td class="auto-style435">
                    <asp:Label ID="lblQtySum1" runat="server" CssClass="auto-style174" Text="labl"></asp:Label>
                </td>
                <td class="auto-style434">
                    <asp:Label ID="lblPartsSum" runat="server" CssClass="auto-style174"></asp:Label>
                </td>
                <td class="auto-style463"></td>
                <td class="auto-style434">
                    <asp:Label ID="lblGPartsSum" runat="server" CssClass="auto-style174"></asp:Label>
                </td>
                <td class="auto-style434">
                    <asp:Label ID="lblUTSGTPa" runat="server" CssClass="auto-style174"></asp:Label>
                </td>
                <td class="auto-style459">
                    <asp:Label ID="lblCGSTPa" runat="server" CssClass="auto-style174"></asp:Label>
                </td>
                <td class="auto-style434">
                    <asp:Label ID="lblIGSTPa" runat="server" CssClass="auto-style174"></asp:Label>
                </td>
                <td class="auto-style434">
                    <asp:Label ID="lblSumPa" runat="server" CssClass="auto-style174"></asp:Label>
                </td>
                <td class="auto-style440"></td>
                <td class="auto-style408">
                    <asp:DropDownList ID="DropListDenomination" runat="server" Height="25px" Width="165px" CssClass="auto-style174">
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td class="auto-style400">
                    <asp:Label ID="Label96" runat="server" CssClass="auto-style174" Text="④shipment"></asp:Label>
                </td>
                <td class="auto-style401">
                    <asp:TextBox ID="TextShipMent" runat="server" Height="21px" Width="213px"></asp:TextBox>
                </td>
                <td class="auto-style402">
                    <asp:TextBox ID="TextQtyShipMent" runat="server" Height="21px" Width="53px" CssClass="auto-style174"></asp:TextBox>
                </td>
                <td class="auto-style464">
                    <asp:Label ID="lblPriceShip" runat="server" CssClass="auto-style174"></asp:Label>
                </td>
                <td class="auto-style465"></td>
                <td class="auto-style464">
                    <asp:Label ID="lblGPriceShip" runat="server" CssClass="auto-style174"></asp:Label>
                </td>
                <td class="auto-style466">
                    <asp:Label ID="lblsSGST" runat="server" CssClass="auto-style174"></asp:Label>
                </td>
                <td class="auto-style467">
                    <asp:Label ID="lblsCGST" runat="server" CssClass="auto-style174"></asp:Label>
                </td>
                <td class="auto-style466">
                    <asp:Label ID="lblsIGST" runat="server" CssClass="auto-style174"></asp:Label>
                </td>
                <td class="auto-style468">
                    <asp:Label ID="lblSumS" runat="server" CssClass="auto-style174"></asp:Label>
                </td>
                <td class="auto-style469"></td>
                <td class="auto-style470">
                    <asp:Label ID="Label121" runat="server" CssClass="auto-style174" Text="rec user"></asp:Label>
                </td>
            </tr>
            <tr>
                <td class="auto-style386">
                    <asp:Label ID="Label94" runat="server" CssClass="auto-style174" Text="④other"></asp:Label>
                </td>
                <td class="auto-style387">
                    <asp:TextBox ID="TextOther" runat="server" Height="21px" Width="213px" CssClass="auto-style174"></asp:TextBox>
                </td>
                <td class="auto-style388">
                    <asp:TextBox ID="TextQtyOther" runat="server" Height="21px" Width="53px" CssClass="auto-style174"></asp:TextBox>
                </td>
                <td class="auto-style409">
                    <asp:Label ID="lblPriceOther" runat="server" CssClass="auto-style174"></asp:Label>
                </td>
                <td class="auto-style462"></td>
                <td class="auto-style409">
                    <asp:Label ID="lblGPriceOther" runat="server" CssClass="auto-style174"></asp:Label>
                </td>
                <td class="auto-style410">
                    <asp:Label ID="lbloSGST" runat="server" CssClass="auto-style174"></asp:Label>
                </td>
                <td class="auto-style455">
                    <asp:Label ID="lbloCGST" runat="server" CssClass="auto-style174"></asp:Label>
                </td>
                <td class="auto-style410">
                    <asp:Label ID="lbloIGST" runat="server" CssClass="auto-style174"></asp:Label>
                </td>
                <td class="auto-style411">
                    <asp:Label ID="lblSumO" runat="server" CssClass="auto-style174"></asp:Label>
                </td>
                <td class="auto-style453"></td>
                <td class="auto-style390">
                    <asp:TextBox ID="TextRec_yuser" runat="server" CssClass="auto-style174"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td class="auto-style431">
                    <asp:Label ID="Label123" runat="server" CssClass="auto-style174" Text="④other amount"></asp:Label>
                &nbsp;</td>
                <td class="auto-style405"></td>
                <td class="auto-style435">
                    <asp:Label ID="lblQtySum2" runat="server" CssClass="auto-style174" Text="labl"></asp:Label>
                </td>
                <td class="auto-style434">
                    <asp:Label ID="lblOtherSum" runat="server" CssClass="auto-style174"></asp:Label>
                </td>
                <td class="auto-style462"></td>
                <td class="auto-style434">
                    <asp:Label ID="lblGOtherSum" runat="server" CssClass="auto-style174"></asp:Label>
                </td>
                <td class="auto-style434">
                    <asp:Label ID="lblUTSGTO" runat="server" CssClass="auto-style174"></asp:Label>
                </td>
                <td class="auto-style459">
                    <asp:Label ID="lblCGSTO" runat="server" CssClass="auto-style174"></asp:Label>
                </td>
                <td class="auto-style434">
                    <asp:Label ID="lblIGSTO" runat="server" CssClass="auto-style174"></asp:Label>
                </td>
                <td class="auto-style434">
                    <asp:Label ID="lblSum4" runat="server" CssClass="auto-style174"></asp:Label>
                </td>
                <td class="auto-style440"></td>
                <td class="auto-style408">
                    &nbsp;</td>
            </tr>
            <tr>
                <td class="auto-style424">
                    <asp:Label ID="Label97" runat="server" CssClass="auto-style174" Text="total"></asp:Label>
                </td>
                <td class="auto-style425"></td>
                <td class="auto-style426"></td>
                <td class="auto-style428">
                    <asp:Label ID="lblSumCost" runat="server" CssClass="auto-style174"></asp:Label>
                </td>
                <td class="auto-style462"></td>
                <td class="auto-style428">
                    <asp:Label ID="lblSumSales" runat="server" CssClass="auto-style174"></asp:Label>
                </td>
                <td class="auto-style429">
                    <asp:Label ID="lblSumSGST" runat="server" CssClass="auto-style174"></asp:Label>
                </td>
                <td class="auto-style458">
                    <asp:Label ID="lblSumCGST" runat="server" CssClass="auto-style174"></asp:Label>
                </td>
                <td class="auto-style429">
                    <asp:Label ID="lblSumIGST" runat="server" CssClass="auto-style174"></asp:Label>
                </td>
                <td class="auto-style430">
                    <asp:Label ID="lblTotal" runat="server" CssClass="auto-style174"></asp:Label>
                </td>
                <td class="auto-style427"></td>
                <td class="auto-style415" rowspan = "3">
                    <asp:CheckBox ID="ChkEsdtimate" runat="server" CssClass="auto-style174" Text="Estimate" />
                </td>
            </tr>
            <tr>
                <td class="auto-style350"></td>
                <td class="auto-style383"></td>
                <td class="auto-style324"></td>
                <td class="auto-style419"></td>
                <td class="auto-style462"></td>
                <td class="auto-style419"></td>
                <td class="auto-style419"></td>
                <td class="auto-style460"></td>
                <td class="auto-style419"></td>
                <td class="auto-style419"></td>
                <td class="auto-style441"></td>
            </tr>
            <tr>
                <td class="auto-style400"></td>
                <td class="auto-style401">
                    <asp:Label ID="Label120" runat="server" Text="Comment2" CssClass="auto-style437"></asp:Label>
                </td>
                <td class="auto-style402"></td>
                <td colspan = "3" align = "right">
                    <asp:Label ID="Label124" runat="server" Text="Billing amount" CssClass="auto-style174"></asp:Label>
                </td>
                <td></td>
                <td class="auto-style403" colspan = "2" align = "right">
                    <asp:Label ID="lblTotalAmount" runat="server" CssClass="auto-style174"></asp:Label>
                </td>
                <td class="auto-style418">
                    <asp:Label ID="Label119" runat="server" CssClass="auto-style174" Text="INR"></asp:Label>
                </td>
                <td class="auto-style439"></td>
            </tr>
            <tr>
                <td class="auto-style420"></td>
                <td class="auto-style421" colspan = "4" rowspan = "2">
                    <asp:TextBox ID="TextComment2" runat="server" Height="102px" Width="497px" CssClass="auto-style174" TextMode="MultiLine" AutoPostBack="True" MaxLength="5" Rows="3"></asp:TextBox>
                </td>
                <td class="auto-style422"></td>
                <td class="auto-style461"></td>
                <td></td>
                <td class="auto-style422"></td>
                <td class="auto-style422"></td>
                <td class="auto-style442"></td>
                <td class="auto-style423">
                    <asp:Button ID="btnCalculation" runat="server" BackColor="#6699FF" CssClass="auto-style282" Height="29px" Text="Calculation" Width="81px" BorderStyle="Inset" Font-Bold="True" Font-Size="X-Large" style="font-family: 'Meiryo UI'; font-size: small;" />
                </td>
            </tr>
            <tr>
                <td class="auto-style350"></td>
                <%--<%@ Page MaintainScrollPositionOnPostback="true" %>--%>
                <td class="auto-style419" colspan = "2">&nbsp;</td>
                <td class="auto-style460" colspan = "2">&nbsp;</td>
                <td class="auto-style419">
                    <asp:ImageButton ID="btnBack" runat="server" Height="29px" ImageUrl="~/icon/back.png" Width="81px" /></td>
                <td class="auto-style441"></td>
                <td>
                    <asp:ImageButton ID="btnSend" runat="server" Height="29px" ImageUrl="~/icon/send.png" Width="81px" />&nbsp;
                </td>
            </tr>
        </table>
        <asp:Button ID="BtnCancel" runat="server" Text="Button" style="display:none;" />
        <asp:Button ID="BtnOK" runat="server" Text="Button" style="display:none;"  />
        <asp:Button ID="Btn2OK" runat="server" Text="Button2" style="display:none;"  />
        <asp:Button ID="BtnLastOK" runat="server" Text="last" style="display:none;"  />
        <asp:Button ID="Btn2LastOK" runat="server" Text="last" style="display:none;"  />
    </div>
  </div>

  <div id="dialog" title="message" style="display:none;"> >
        <asp:Label ID="lblMsg" runat="server" Text=""></asp:Label>
  </div>
    
    <script type="text/javascript">  ASP側に埋め込み済
        var scrollPosition = document.getElementById("area").scrollTop;
        // スクロール要素の高さ
        var scrollHeight = document.getElementById("area").scrollHeight;

        //$(function () {
        //    $("#dialog" ).dialog({
        //        width: 400,
        //        buttons:
        //        {
        //            "OK": function () {
        //                $(this).dialog('close');
        //            },
        //            "CANCEL": function () {
        //                $(this).dialog('close');
        //                $('[id$="BtnCancel"]').click();
        //            }
        //        }
        //    });
        //});    
    </script>
  
</asp:Content>
