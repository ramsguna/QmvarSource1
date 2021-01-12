<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Site5.Master" CodeBehind="Repair_1_Detail.aspx.vb" Inherits="Ganges33.Repair_1_Detail" %>
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
            height: 898px;
            width: 1267px;
            background-size: contain;
            /*opacity: 0.5;*/
            }
            .auto-style7 {
                height: 100%;
                background: rgba(255,255,255,0.8);
                /*opacity: 0.5;*/
            }
            .auto-style135 {
                width: 100%;
                border-collapse: collapse; 
            }
            .auto-style136 {
                height: 13px;
                width: 199px;
                border: 1px solid black;
            }
            .auto-style137 {
                height: 13px;
                width: 13px;
            }
            .auto-style139 {
                height: 13px;
                width: 199px;
            }
            .auto-style145 {
                border-left: 1px solid black;
                border-right: 1px solid black;
                border-top: 1px solid black;
                height: 13px;
                width: 229px;
                border-bottom: 1px none black;
            }
            .auto-style146 {
                height: 13px;
                width: 149px;
            }
            .auto-style147 {
                border-left: 1px solid black;
                border-right: 1px none black;
                border-top: 1px solid black;
                border-bottom: 1px none black;
                height: 13px;
                width: 149px;
            }
            .auto-style148 {
                border-left: 1px solid black;
                border-right: 1px none black;
                border-top: 1px none black;
                border-bottom: 1px none black;
                height: 13px;
                width: 149px;
            }
            .auto-style149 {
                border-left: 1px solid black;
                border-right: 1px none black;
                border-top: 1px none black;
                border-bottom: 1px solid black;
                height: 13px;
                width: 149px;
            }
            .auto-style150 {
                height: 13px;
                width: 119px;
            }
            .auto-style151 {
                border-left: 1px solid black;
                border-right: 1px none black;
                border-top: 1px solid black;
                border-bottom: 1px none black;
                height: 13px;
                width: 119px;
            }
            .auto-style152 {
                border-left: 1px solid black;
                border-right: 1px none black;
                border-top: 1px none black;
                border-bottom: 1px none black;
                height: 13px;
                width: 119px;
            }
            .auto-style153 {
                border-left: 1px solid black;
                border-right: 1px none black;
                border-top: 1px none black;
                border-bottom: 1px solid black;
                height: 13px;
                width: 119px;
            }
            .auto-style154 {
                height: 13px;
                width: 135px;
            }
            .auto-style155 {
                border-left: 1px solid black;
                border-right: 1px none black;
                border-top: 1px solid black;
                border-bottom: 1px none black;
                height: 13px;
                width: 135px;
            }
            .auto-style156 {
                border-left: 1px solid black;
                border-right: 1px none black;
                border-top: 1px none black;
                border-bottom: 1px none black;
                height: 13px;
                width: 135px;
            }
            .auto-style157 {
                border-left: 1px solid black;
                border-right: 1px none black;
                border-top: 1px none black;
                border-bottom: 1px solid black;
                height: 13px;
                width: 135px;
            }
            .auto-style158 {
                height: 13px;
                width: 229px;
            }
            .auto-style159 {
                height: 13px;
                width: 229px;
                border: 1px solid black;
            }
            .auto-style160 {
                height: 13px;
                width: 199px;
                border: 1px solid black;
                border-top: 1px none black;
            }
            .auto-style162 {
                border: 1px solid black;
            }
            .auto-style163 {
                border: 1px solid black;
                border-top: 1px none black;
                border-bottom: 1px none black;
            }
            .auto-style164 {
                border-left: 1px solid black;
                border-right: 1px solid black;
                border-top: 1px solid black;
                border-bottom: 1px none black;
                height: 13px;
            }
            .auto-style165 {
                border: 1px solid black;
                border-top: 1px none black;
            }
            .auto-style166 {
                border: 1px solid black;
                height: 13px;
            }
            .auto-style167 {
                height: 13px;
            }
            .auto-style168 {
                font-family: "Meiryo UI";
            }
            .auto-style169 {
                border-left: 1px solid black;
                border-right: 1px solid black;
                border-top: 1px none black;
                border-bottom: 1px none black;
                font-family: "Meiryo UI";
            }
            .auto-style170 {
                border-left: 1px solid black;
                border-right: 1px solid black;
                border-top: 1px solid black;
                border-bottom: 1px none black;
                height: 13px;
                width: 135px;
            }
            .auto-style171 {
                border-left: 1px solid black;
                border-right: 1px solid black;
                border-top: 1px none black;
                border-bottom: 1px none black;
                width: 135px;
            }
            .auto-style172 {
                border-left: 1px solid black;
                border-right: 1px solid black;
                border-bottom: 1px solid black;
                border-top: 1px none black;
                width: 135px;
            }
            .auto-style173 {
                width: 135px;
            }
            .auto-style174 {
                border: 1px solid black;
                font-family: "Meiryo UI";
            }
            .auto-style175 {
                border: 1px solid black;
                border-top: 1px none black;
                border-bottom: 2px solid black;
            }
            .auto-style176 {
                border: 1px solid black;
                border-bottom: 2px solid black;
            }
            .auto-style177 {
                border-left: 1px solid black;
                border-right: 1px solid black;
                border-top: 1px none black;
                border-bottom: 2px solid black;
                width: 135px;
            }
            .auto-style178 {
                border-left: 1px solid black;
                border-right: 1px solid black;
                border-top: 1px none black;
                border-bottom: 2px solid black;
                font-family: "Meiryo UI";
            }
            </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="auto-style6" style="background-image: url('pagewall_2/wall_repair-2.png')">
        <div class="auto-style7">
         <table class="auto-style135" cellspacing="0">
            <tr>
                <td class="auto-style146" align ="right">
                    <asp:Label ID="Label35" runat="server" Text="PO No : " CssClass="auto-style168"></asp:Label>
                </td>
                <td class="auto-style139">
                    <asp:Label ID="lblPo_No" runat="server" CssClass="auto-style168"></asp:Label>
                </td>
                <td class="auto-style137"></td>
                <td class="auto-style150" align ="right">
                    <asp:Label ID="Label36" runat="server" Text="Engineer : " CssClass="auto-style168"></asp:Label>
                </td>
                <td class="auto-style158" colspan = "3">
                    <asp:Label ID="lblEngineer" runat="server" CssClass="auto-style168"></asp:Label>
                </td>
                <td class="auto-style139" rowspan = "2">
                    <asp:ImageButton ID="btnSearch" runat="server" Height="29px" ImageUrl="~/icon/back.png" Width="81px" CssClass="auto-style168" />
                </td>
            </tr>
            <tr>
                <td class="auto-style146" align ="right">
                <asp:Label ID="lblDate" runat="server" Text="Record date&amp;time : " CssClass="auto-style168"></asp:Label>    
                </td>
                <td class="auto-style139">
                    <asp:Label ID="lblRecord" runat="server" CssClass="auto-style168"></asp:Label>
                </td>
                <td class="auto-style137"></td>
                <td class="auto-style150" align = "right">
                    <asp:Label ID="lblName" runat="server" Text="yousername : " CssClass="auto-style168"></asp:Label>
               </td>
                <td class="auto-style158" colspan = "3">
                <asp:Label ID="lblYousername" runat="server" CssClass="auto-style168"></asp:Label>    
                </td>
                <%--<td class="auto-style139"></td>--%>
            </tr>
            <tr>
                <td class="auto-style147">
                    <asp:Label ID="Label9" runat="server" Text="ASC Claim No" CssClass="auto-style168"></asp:Label><span class="auto-style168">:</span></td>
                <td class="auto-style136">
                    <asp:Label ID="lblASC_Claim_No" runat="server" CssClass="auto-style168"></asp:Label>
                </td>
                <td class="auto-style137"></td>
                <td class="auto-style151">
                    <asp:Label ID="lbl" runat="server" Text="Consumer Name" CssClass="auto-style168"></asp:Label>
                </td>
                <td class="auto-style145" rowspan = "2">
                    <asp:Label ID="lblConsumer_Name" runat="server" CssClass="auto-style168"></asp:Label>
                </td>
                <td class="auto-style137"></td>
                <td class="auto-style155">
                    <asp:Label ID="Label26" runat="server" Text="Model" CssClass="auto-style168"></asp:Label>
                </td>
                <td class="auto-style136">
                    <asp:Label ID="lblModel" runat="server" CssClass="auto-style168"></asp:Label>
                </td>
            </tr>
            <tr>             
                <td class="auto-style148">
                    <asp:Label ID="Label2" runat="server" Text="Samsung Claim No" CssClass="auto-style168"></asp:Label>
                </td>
                <td class="auto-style136">
                    <asp:Label ID="lblSamsung_Claim_No" runat="server" CssClass="auto-style168"></asp:Label>
                </td>
                <td class="auto-style137"></td>
                <td class="auto-style152"></td>
                <%--<td class="auto-style144"></td>--%>
                <td class="auto-style137"></td>
                <td class="auto-style156">
                    <asp:Label ID="Label27" runat="server" Text="Serial No" CssClass="auto-style168"></asp:Label>
                </td>
                <td class="auto-style136">
                    <asp:Label ID="lblSerialNo" runat="server" CssClass="auto-style168"></asp:Label>
                </td>
            </tr>
            <tr>             
                <td class="auto-style148">
                    <asp:Label ID="Label3" runat="server" Text="Service type" CssClass="auto-style168"></asp:Label>
                </td>
                <td class="auto-style136">
                    <asp:Label ID="lblService_type" runat="server" CssClass="auto-style168"></asp:Label>
                </td>
                <td class="auto-style137"></td>
                <td class="auto-style152">
                    <asp:Label ID="Label41" runat="server" Text="Address 1" CssClass="auto-style168"></asp:Label>
                </td>
                <td class="auto-style145" rowspan ="2">
                    <asp:Label ID="lblAddress1" runat="server" Height="13px" CssClass="auto-style168"></asp:Label>
                </td>
                <td class="auto-style137"></td>
                <td class="auto-style156">
                    <asp:Label ID="Label28" runat="server" Text="IMEI" CssClass="auto-style168"></asp:Label>
                </td>
                <td class="auto-style136">
                    <asp:Label ID="lblIMEI" runat="server" CssClass="auto-style168"></asp:Label>
                </td>
            </tr>
            <tr>
                <td class="auto-style148">
                    <asp:Label ID="Label4" runat="server" Text="Purchase Date" CssClass="auto-style168"></asp:Label>
                </td>
                <td class="auto-style136">
                    <asp:Label ID="lblPurchase_Date" runat="server" CssClass="auto-style168"></asp:Label>
                </td>
                <td class="auto-style137"></td>
                <td class="auto-style152"></td>
                <%--<td class="auto-style144"></td>--%>
                <td class="auto-style137"></td>
                <td class="auto-style156">
                    <asp:Label ID="Label29" runat="server" Text="Defect Type" CssClass="auto-style168"></asp:Label>
                </td>
                <td class="auto-style136">
                    <asp:Label ID="lblDefectType" runat="server" CssClass="auto-style168"></asp:Label>
                </td>
            </tr>
            <tr>
                <td class="auto-style148">
                    <asp:Label ID="Label5" runat="server" Text="Repair Recived Date" CssClass="auto-style168"></asp:Label>
                </td>
                <td class="auto-style136">
                    <asp:Label ID="lblRepair_Recived_Date" runat="server" CssClass="auto-style168"></asp:Label>
                </td>
                <td class="auto-style137"></td>
                <td class="auto-style152">
                    <asp:Label ID="Label40" runat="server" Text="Address 2" CssClass="auto-style168"></asp:Label>
                </td>
                <td class="auto-style145" rowspan = "2">
                    <asp:Label ID="lblAddress2" runat="server" CssClass="auto-style168"></asp:Label>
                </td>
                <td class="auto-style137"></td>
                <td class="auto-style156">
                    <asp:Label ID="Label30" runat="server" Text="Condition" CssClass="auto-style168"></asp:Label>
                </td>
                <td class="auto-style136">
                    <asp:Label ID="lblCondition" runat="server" CssClass="auto-style168"></asp:Label>
                </td>
            </tr>
            <tr>
                <td class="auto-style148">
                    <asp:Label ID="Label6" runat="server" Text="Complete Date" CssClass="auto-style168"></asp:Label>
                </td>
                <td class="auto-style136">
                    <asp:Label ID="lblComplete_Date" runat="server" CssClass="auto-style168"></asp:Label>
                </td>
                <td class="auto-style137"></td>
                <td class="auto-style152"></td>
                <%--<td class="auto-style144"></td>--%>
                <td class="auto-style137"></td>
                <td class="auto-style156">
                    <asp:Label ID="Label31" runat="server" Text="Symptom" CssClass="auto-style168"></asp:Label>
                </td>
                <td class="auto-style136">
                    <asp:Label ID="lblSymptom" runat="server" CssClass="auto-style168"></asp:Label>
                </td>
            </tr>
            <tr>
                <td class="auto-style148">
                    <asp:Label ID="Label7" runat="server" Text="Delivery Date" CssClass="auto-style168"></asp:Label>
                </td>
                <td class="auto-style136">
                    <asp:Label ID="lblDelivery_Date" runat="server" CssClass="auto-style168"></asp:Label>
                </td>
                <td class="auto-style137"></td>
                <td class="auto-style152">
                    <asp:Label ID="Label39" runat="server" Text="telephone" CssClass="auto-style168"></asp:Label>
                </td>
                <td class="auto-style159">
                    <asp:Label ID="lblTelephone" runat="server" CssClass="auto-style168"></asp:Label>
                </td>
                <td class="auto-style137"></td>
                <td class="auto-style156">
                    <asp:Label ID="Label32" runat="server" Text="Defect code" CssClass="auto-style168"></asp:Label>
                </td>
                <td class="auto-style136">
                    <asp:Label ID="lblDefectCode" runat="server" CssClass="auto-style168"></asp:Label>
                </td>
            </tr>
            <tr>
                <td class="auto-style149">
                    <asp:Label ID="Label25" runat="server" Text="Production Date" CssClass="auto-style168"></asp:Label>
                </td>
                <td class="auto-style136">
                    <asp:Label ID="lblProduction_Date" runat="server" CssClass="auto-style168"></asp:Label>
                </td>
                <td class="auto-style137"></td>
                <td class="auto-style152">
                    <asp:Label ID="Label38" runat="server" Text="FAX" CssClass="auto-style168"></asp:Label>
                </td>
                <td class="auto-style159">
                    <asp:Label ID="lblFax" runat="server" CssClass="auto-style168"></asp:Label>
                </td>
                <td class="auto-style137"></td>
                <td class="auto-style156">
                    <asp:Label ID="Label33" runat="server" Text="Repair code" CssClass="auto-style168"></asp:Label>
                </td>
                <td class="auto-style136">
                    <asp:Label ID="lblRepairCode" runat="server" CssClass="auto-style168"></asp:Label>
                </td>
            </tr>
            <tr>
                <td class="auto-style146"></td>
                <td class="auto-style139"></td>
                <td class="auto-style137"></td>
                <td class="auto-style153">
                    <asp:Label ID="Label42" runat="server" Text="postal code" CssClass="auto-style168"></asp:Label>
                </td>
                <td class="auto-style159">
                    <asp:Label ID="lblPostal_code" runat="server" CssClass="auto-style168"></asp:Label>
                </td>
                <td class="auto-style137"></td>
                <td class="auto-style156">
                    <asp:Label ID="Label43" runat="server" Text="Repair Description" CssClass="auto-style168"></asp:Label>
                </td>
                <td class="auto-style160" rowspan = "5">
                    <asp:Label ID="lblRepairDescription" runat="server" CssClass="auto-style168"></asp:Label>
                </td>
            </tr>
            <tr>
                <td class="auto-style146"></td>
                <td class="auto-style139"></td>
                <td class="auto-style137"></td>
                <td></td>
                <td></td>
                <td class="auto-style137"></td>
                <td class="auto-style156"></td>
                <%--<td class="auto-style160"></td>--%>
            </tr>
             <tr>
                 <td class="auto-style167"></td>
                 <td class="auto-style167"></td>
                 <td class="auto-style167"></td>
                 <td class="auto-style167"></td>
                 <td class="auto-style167"></td>
                 <td class="auto-style167"></td>
                 <td class="auto-style156"></td>
             </tr>
             <tr>
                 <td class="auto-style167"></td>
                 <td class="auto-style167"></td>
                 <td class="auto-style167"></td>
                 <td class="auto-style167"></td>
                 <td class="auto-style167"></td>
                 <td class="auto-style167"></td>
                 <td class="auto-style156"></td>
             </tr>
             <tr>
                 <td class="auto-style167"></td>
                 <td class="auto-style167"></td>
                 <td class="auto-style167"></td>
                 <td class="auto-style167"></td>
                 <td class="auto-style167"></td>
                 <td class="auto-style167"></td>
                 <td class="auto-style157"></td>
             </tr>
             <tr>
                 <td class="auto-style167"></td>
                 <td class="auto-style167"></td>
                 <td class="auto-style167"></td>
                 <td class="auto-style167"></td>
                 <td class="auto-style167"></td>
                 <td class="auto-style167"></td>
                 <td class="auto-style154"></td>
                 <td class="auto-style167"></td>
             </tr>
             <tr>
                 <td class="auto-style164">
                     <asp:Label ID="Label44" runat="server" Text="Purchase Date" CssClass="auto-style168"></asp:Label>
                 </td>
                 <td class="auto-style166">
                     <asp:Label ID="lblPurchaseDate" runat="server" CssClass="auto-style168"></asp:Label>
                 </td>
                 <td class="auto-style137"></td>
                 <td class="auto-style164">
                     <asp:Label ID="Label59" runat="server" Text="Location-1" CssClass="auto-style168"></asp:Label>
                 </td>
                 <td class="auto-style166">
                     <asp:Label ID="lblLocation1" runat="server" CssClass="auto-style168"></asp:Label>
                 </td>
                 <td class="auto-style137"></td>
                 <td class="auto-style170">
                     <asp:Label ID="Label71" runat="server" Text="Location-2" CssClass="auto-style168"></asp:Label>
                 </td>
                 <td class="auto-style166">
                     <asp:Label ID="lblLocation2" runat="server" CssClass="auto-style168"></asp:Label>
                 </td>
             </tr>
             <tr>
                 <td class="auto-style163">
                     <asp:Label ID="Label45" runat="server" Text="Repair Recived Date" CssClass="auto-style168"></asp:Label>
                 </td>
                 <td class="auto-style162">
                     <asp:Label ID="lblRepairRecivedDate" runat="server" CssClass="auto-style168"></asp:Label>
                 </td>
                 <td class="auto-style137"></td>
                 <td class="auto-style163">
                     <asp:Label ID="Label60" runat="server" Text="Parts-1" CssClass="auto-style168"></asp:Label>
                 </td>
                 <td class="auto-style162">
                     <asp:Label ID="lblParts1" runat="server" CssClass="auto-style168"></asp:Label>
                 </td>
                 <td class="auto-style137"></td>
                 <td class="auto-style171">
                     <asp:Label ID="Label72" runat="server" Text="Parts-2" CssClass="auto-style168"></asp:Label>
                 </td>
                 <td class="auto-style162">
                     <asp:Label ID="lblParts2" runat="server" CssClass="auto-style168"></asp:Label>
                 </td>
             </tr>
             <tr>
                 <td class="auto-style163">
                     <asp:Label ID="Label46" runat="server" Text="Complete Date" CssClass="auto-style168"></asp:Label>
                 </td>
                 <td class="auto-style162">
                     <asp:Label ID="lblCompleteDate" runat="server" CssClass="auto-style168"></asp:Label>
                 </td>
                 <td class="auto-style137"></td>
                 <td class="auto-style163">
                     <asp:Label ID="Label61" runat="server" Text="QTY" CssClass="auto-style168"></asp:Label>
                 </td>
                 <td class="auto-style162">
                     <asp:Label ID="lblQTY1" runat="server" CssClass="auto-style168"></asp:Label>
                 </td>
                 <td class="auto-style137"></td>
                 <td class="auto-style171">
                     <asp:Label ID="Label73" runat="server" Text="QTY" CssClass="auto-style168"></asp:Label>
                 </td>
                 <td class="auto-style162">
                     <asp:Label ID="lblQTY2" runat="server" CssClass="auto-style168"></asp:Label>
                 </td>
             </tr>
             <tr>
                 <td class="auto-style163">
                     <asp:Label ID="Label47" runat="server" Text="Delivery Date" CssClass="auto-style168"></asp:Label>
                 </td>
                 <td class="auto-style162">
                     <asp:Label ID="lblDeliveryDate" runat="server" CssClass="auto-style168"></asp:Label>
                 </td>
                 <td class="auto-style137"></td>
                 <td class="auto-style163">
                     <asp:Label ID="Label62" runat="server" Text="Unit Price" CssClass="auto-style168"></asp:Label>
                 </td>
                 <td class="auto-style162">
                     <asp:Label ID="lblUnitPrice1" runat="server" CssClass="auto-style168"></asp:Label>
                 </td>
                 <td class="auto-style137"></td>
                 <td class="auto-style171">
                     <asp:Label ID="Label74" runat="server" Text="Unit Price" CssClass="auto-style168"></asp:Label>
                 </td>
                 <td class="auto-style162">
                     <asp:Label ID="lblUnitPrice2" runat="server" CssClass="auto-style168"></asp:Label>
                 </td>
             </tr>
             <tr>
                 <td class="auto-style163">
                     <asp:Label ID="Label48" runat="server" Text="Production Date" CssClass="auto-style168"></asp:Label>
                 </td>
                 <td class="auto-style162">
                     <asp:Label ID="lblProductionDate" runat="server" CssClass="auto-style168"></asp:Label>
                 </td>
                 <td class="auto-style137"></td>
                 <td class="auto-style175"></td>
                 <td class="auto-style176"></td>
                 <td class="auto-style137"></td>
                 <td class="auto-style177"></td>
                 <td class="auto-style176"></td>
             </tr>
             <tr>
                 <td class="auto-style163">
                     <asp:Label ID="Label49" runat="server" Text="Labor Amount" CssClass="auto-style168"></asp:Label>
                 </td>
                 <td class="auto-style162">
                     <asp:Label ID="lblLaborAmount" runat="server" CssClass="auto-style168"></asp:Label>
                 </td>
                 <td class="auto-style137"></td>
                 <td class="auto-style163">
                     <asp:Label ID="Label65" runat="server" Text="Location-3" CssClass="auto-style168"></asp:Label>
                 </td>
                 <td class="auto-style166">
                     <asp:Label ID="lblLocation3" runat="server" CssClass="auto-style168"></asp:Label>
                 </td>
                 <td class="auto-style137"></td>
                 <td class="auto-style171">
                     <asp:Label ID="Label75" runat="server" Text="Location-4" CssClass="auto-style168"></asp:Label>
                 </td>
                 <td class="auto-style166">
                     <asp:Label ID="lblLocation4" runat="server" CssClass="auto-style168"></asp:Label>
                 </td>
             </tr>
             <tr>
                 <td class="auto-style163">
                     <asp:Label ID="PartsAmount" runat="server" CssClass="auto-style168">PartsAmount</asp:Label>
                 </td>
                 <td class="auto-style162">
                     <asp:Label ID="lblPartsAmount" runat="server" CssClass="auto-style168"></asp:Label>
                 </td>
                 <td class="auto-style137"></td>
                 <td class="auto-style163">
                     <asp:Label ID="Label63" runat="server" Text="Parts-3" CssClass="auto-style168"></asp:Label>
                 </td>
                 <td class="auto-style162">
                     <asp:Label ID="lblParts3" runat="server" CssClass="auto-style168"></asp:Label>
                 </td>
                 <td class="auto-style137"></td>
                 <td class="auto-style171">
                     <asp:Label ID="Label76" runat="server" Text="Parts-4" CssClass="auto-style168"></asp:Label>
                 </td>
                 <td class="auto-style162">
                     <asp:Label ID="lblParts4" runat="server" CssClass="auto-style168"></asp:Label>
                 </td>
             </tr>
             <tr>
                 <td class="auto-style163">
                     <asp:Label ID="Label51" runat="server" Text="Parts SGST" CssClass="auto-style168"></asp:Label>
                 </td>
                 <td class="auto-style162">
                     <asp:Label ID="lblPartsSGST" runat="server" CssClass="auto-style168"></asp:Label>
                 </td>
                 <td class="auto-style137"></td>
                 <td class="auto-style163">
                     <asp:Label ID="Label64" runat="server" Text="QTY" CssClass="auto-style168"></asp:Label>
                 </td>
                 <td class="auto-style162">
                     <asp:Label ID="lblQTY3" runat="server" CssClass="auto-style168"></asp:Label>
                 </td>
                 <td class="auto-style137"></td>
                 <td class="auto-style171">
                     <asp:Label ID="Label77" runat="server" Text="QTY" CssClass="auto-style168"></asp:Label>
                 </td>
                 <td class="auto-style162">
                     <asp:Label ID="lblQTY4" runat="server" CssClass="auto-style168"></asp:Label>
                 </td>
             </tr>
             <tr>
                 <td class="auto-style163">
                     <asp:Label ID="Label52" runat="server" Text="Parts CGST" CssClass="auto-style168"></asp:Label>
                 </td>
                 <td class="auto-style162">
                     <asp:Label ID="lblPartsCGST" runat="server" CssClass="auto-style168"></asp:Label>
                 </td>
                 <td class="auto-style137"></td>
                 <td class="auto-style163">
                     <asp:Label ID="Label66" runat="server" Text="Unit Price" CssClass="auto-style168"></asp:Label>
                 </td>
                 <td class="auto-style162">
                     <asp:Label ID="lblUnitPrice3" runat="server" CssClass="auto-style168"></asp:Label>
                 </td>
                 <td class="auto-style137"></td>
                 <td class="auto-style171">
                     <asp:Label ID="Label78" runat="server" Text="Unit Price" CssClass="auto-style168"></asp:Label>
                 </td>
                 <td class="auto-style162">
                     <asp:Label ID="lblUnitPrice4" runat="server" CssClass="auto-style168"></asp:Label>
                 </td>
             </tr>
             <tr>
                 <td class="auto-style163">
                     <asp:Label ID="Label53" runat="server" Text="SGST" CssClass="auto-style168"></asp:Label>
                 </td>
                 <td class="auto-style162">
                     <asp:Label ID="lblSGST" runat="server" CssClass="auto-style168"></asp:Label>
                 </td>
                 <td class="auto-style137"></td>
                 <td class="auto-style175"></td>
                 <td class="auto-style176"></td>
                 <td class="auto-style137"></td>
                 <td class="auto-style177"></td>
                 <td class="auto-style176"></td>
             </tr>
             <tr>
                 <td class="auto-style163">
                     <asp:Label ID="Label54" runat="server" Text="CGST" CssClass="auto-style168"></asp:Label>
                 </td>
                 <td class="auto-style162">
                     <asp:Label ID="lblCGST" runat="server" CssClass="auto-style168"></asp:Label>
                 </td>
                 <td class="auto-style137"></td>
                 <td class="auto-style163">
                     <asp:Label ID="Label67" runat="server" Text="Location-5" CssClass="auto-style168"></asp:Label>
                 </td>
                 <td class="auto-style166">
                     <asp:Label ID="lblLocation5" runat="server" CssClass="auto-style168"></asp:Label>
                 </td>
                 <td class="auto-style137"></td>
                 <td class="auto-style171">
                     <asp:Label ID="Label79" runat="server" Text="Location-6" CssClass="auto-style168"></asp:Label>
                 </td>
                 <td class="auto-style166">
                     <asp:Label ID="lblLocation6" runat="server" CssClass="auto-style168"></asp:Label>
                 </td>
             </tr>
             <tr>
                 <td class="auto-style163">
                     <asp:Label ID="Label55" runat="server" Text="Invoice Amount" CssClass="auto-style168"></asp:Label>
                 </td>
                 <td class="auto-style162">
                     <asp:Label ID="lblInvoiceAmount" runat="server" CssClass="auto-style168"></asp:Label>
                 </td>
                 <td class="auto-style137"></td>
                 <td class="auto-style163">
                     <asp:Label ID="Label68" runat="server" Text="Parts-5" CssClass="auto-style168"></asp:Label>
                 </td>
                 <td class="auto-style162">
                     <asp:Label ID="lblParts5" runat="server" CssClass="auto-style168"></asp:Label>
                 </td>
                 <td class="auto-style137"></td>
                 <td class="auto-style171">
                     <asp:Label ID="Label80" runat="server" Text="Parts-6" CssClass="auto-style168"></asp:Label>
                 </td>
                 <td class="auto-style162">
                     <asp:Label ID="lblParts6" runat="server" CssClass="auto-style168"></asp:Label>
                 </td>
             </tr>
             <tr>
                 <td class="auto-style163">
                     <asp:Label ID="Label56" runat="server" Text="Remark" CssClass="auto-style168"></asp:Label>
                 </td>
                 <td class="auto-style162">
                     <asp:Label ID="lblRemark" runat="server" CssClass="auto-style168"></asp:Label>
                 </td>
                 <td class="auto-style137"></td>
                 <td class="auto-style163">
                     <asp:Label ID="Label69" runat="server" Text="QTY" CssClass="auto-style168"></asp:Label>
                 </td>
                 <td class="auto-style162">
                     <asp:Label ID="lblQTY5" runat="server" CssClass="auto-style168"></asp:Label>
                 </td>
                 <td class="auto-style137"></td>
                 <td class="auto-style171">
                     <asp:Label ID="Label81" runat="server" Text="QTY" CssClass="auto-style168"></asp:Label>
                 </td>
                 <td class="auto-style162">
                     <asp:Label ID="lblQTY6" runat="server" CssClass="auto-style168"></asp:Label>
                 </td>
             </tr>
             <tr>
                 <td class="auto-style163">
                     <asp:Label ID="Label57" runat="server" Text="Tracking No" CssClass="auto-style168"></asp:Label>
                 </td>
                 <td class="auto-style162">
                     <asp:Label ID="lblTrackingNo" runat="server" CssClass="auto-style168"></asp:Label>
                 </td>
                 <td class="auto-style137"></td>
                 <td class="auto-style165">
                     <asp:Label ID="Label70" runat="server" Text="Unit Price" CssClass="auto-style168"></asp:Label>
                 </td>
                 <td class="auto-style162">
                     <asp:Label ID="lblUnitPrice5" runat="server" CssClass="auto-style168"></asp:Label>
                 </td>
                 <td class="auto-style137"></td>
                 <td class="auto-style172">
                     <asp:Label ID="Label82" runat="server" Text="Unit Price" CssClass="auto-style168"></asp:Label>
                 </td>
                 <td class="auto-style162">
                     <asp:Label ID="lblUnitPrice6" runat="server" CssClass="auto-style168"></asp:Label>
                 </td>
             </tr>
             <tr>
                 <td class="auto-style165">
                     <asp:Label ID="Label58" runat="server" Text="Status" CssClass="auto-style168"></asp:Label>
                 </td>
                 <td class="auto-style162">
                     <asp:Label ID="lblStatus" runat="server" CssClass="auto-style168"></asp:Label>
                 </td>
                 <td class="auto-style137"></td>
                 <td></td>
                 <td></td>
                 <td class="auto-style137"></td>
                 <td class="auto-style173"></td>
                 <td></td>
             </tr>
             <tr>
                 <td class="auto-style167"></td>
                 <td class="auto-style167"></td>
                 <td class="auto-style167"></td>
                 <td class="auto-style167"></td>
                 <td class="auto-style167"></td>
                 <td class="auto-style167"></td>
                 <td class="auto-style154"></td>
                 <td class="auto-style167"></td>
             </tr>
             <tr>
                 <td class="auto-style164">
                     <asp:Label ID="Label117" runat="server" Text="Location-7" CssClass="auto-style168"></asp:Label>
                 </td>
                 <td class="auto-style166">
                     <asp:Label ID="lblLocation7" runat="server" CssClass="auto-style168"></asp:Label>
                 </td>
                 <td class="auto-style137"></td>
                 <td class="auto-style164">
                     <asp:Label ID="Label10" runat="server" Text="Location-8" CssClass="auto-style168"></asp:Label>
                 </td>
                 <td class="auto-style166">
                     <asp:Label ID="lblLocation8" runat="server" CssClass="auto-style168"></asp:Label>
                 </td>
                 <td class="auto-style137"></td>
                 <td class="auto-style170">
                     <asp:Label ID="Label11" runat="server" Text="Location-9" CssClass="auto-style168"></asp:Label>
                 </td>
                 <td class="auto-style166">
                     <asp:Label ID="lblLocation9" runat="server" CssClass="auto-style168"></asp:Label>
                 </td>
             </tr>
             <tr>
                 <td class="auto-style163">
                     <asp:Label ID="Label118" runat="server" Text="Parts-7" CssClass="auto-style168"></asp:Label>
                 </td>
                 <td class="auto-style162">
                     <asp:Label ID="lblParts7" runat="server" CssClass="auto-style168"></asp:Label>
                 </td>
                 <td class="auto-style137"></td>
                 <td class="auto-style163">
                     <asp:Label ID="Label14" runat="server" Text="Parts-8" CssClass="auto-style168"></asp:Label>
                 </td>
                 <td class="auto-style162">
                     <asp:Label ID="lblParts8" runat="server" CssClass="auto-style168"></asp:Label>
                 </td>
                 <td class="auto-style137"></td>
                 <td class="auto-style171">
                     <asp:Label ID="Label15" runat="server" Text="Parts-9" CssClass="auto-style168"></asp:Label>
                 </td>
                 <td class="auto-style162">
                     <asp:Label ID="lblParts9" runat="server" CssClass="auto-style168"></asp:Label>
                 </td>
             </tr>
             <tr>
                 <td class="auto-style163">
                     <asp:Label ID="Label119" runat="server" Text="QTY" CssClass="auto-style168"></asp:Label>
                 </td>
                 <td class="auto-style162">
                     <asp:Label ID="lblQTY7" runat="server" CssClass="auto-style168"></asp:Label>
                 </td>
                 <td class="auto-style137"></td>
                 <td class="auto-style163">
                     <asp:Label ID="Label18" runat="server" Text="QTY" CssClass="auto-style168"></asp:Label>
                 </td>
                 <td class="auto-style162">
                     <asp:Label ID="lblQTY8" runat="server" CssClass="auto-style168"></asp:Label>
                 </td>
                 <td class="auto-style137"></td>
                 <td class="auto-style171">
                     <asp:Label ID="Label19" runat="server" Text="QTY" CssClass="auto-style168"></asp:Label>
                 </td>
                 <td class="auto-style162">
                     <asp:Label ID="lblQTY9" runat="server" CssClass="auto-style168"></asp:Label>
                 </td>
             </tr>
             <tr>
                 <td class="auto-style163">
                     <asp:Label ID="Label120" runat="server" Text="Unit Price" CssClass="auto-style168"></asp:Label>
                 </td>
                 <td class="auto-style162">
                     <asp:Label ID="lblUnitPrice7" runat="server" CssClass="auto-style168"></asp:Label>
                 </td>
                 <td class="auto-style137"></td>
                 <td class="auto-style163">
                     <asp:Label ID="Label22" runat="server" Text="Unit Price" CssClass="auto-style168"></asp:Label>
                 </td>
                 <td class="auto-style162">
                     <asp:Label ID="lblUnitPrice8" runat="server" CssClass="auto-style168"></asp:Label>
                 </td>
                 <td class="auto-style137"></td>
                 <td class="auto-style171">
                     <asp:Label ID="Label23" runat="server" Text="Unit Price" CssClass="auto-style168"></asp:Label>
                 </td>
                 <td class="auto-style162">
                     <asp:Label ID="lblUnitPrice9" runat="server" CssClass="auto-style168"></asp:Label>
                 </td>
             </tr>
             <tr>
                 <td class="auto-style178"></td>
                 <td class="auto-style176"></td>
                 <td class="auto-style137"></td>
                 <td class="auto-style175"></td>
                 <td class="auto-style176"></td>
                 <td class="auto-style137"></td>
                 <td class="auto-style177"></td>
                 <td class="auto-style176"></td>
             </tr>
             <tr>
                 <td class="auto-style163">
                     <asp:Label ID="Label121" runat="server" Text="Location-10" CssClass="auto-style168"></asp:Label>
                 </td>
                 <td class="auto-style166">
                     <asp:Label ID="lblLocation10" runat="server" CssClass="auto-style168"></asp:Label>
                 </td>
                 <td class="auto-style137"></td>
                 <td class="auto-style163">
                     <asp:Label ID="Label84" runat="server" Text="Location-11" CssClass="auto-style168"></asp:Label>
                 </td>
                 <td class="auto-style166">
                     <asp:Label ID="lblLocation11" runat="server" CssClass="auto-style168"></asp:Label>
                 </td>
                 <td class="auto-style137"></td>
                 <td class="auto-style171">
                     <asp:Label ID="Label85" runat="server" Text="Location-12" CssClass="auto-style168"></asp:Label>
                 </td>
                 <td class="auto-style166">
                     <asp:Label ID="lblLocation12" runat="server" CssClass="auto-style168"></asp:Label>
                 </td>
             </tr>
             <tr>
                 <td class="auto-style163">
                     <asp:Label ID="Label122" runat="server" Text="Parts-10" CssClass="auto-style168"></asp:Label>
                 </td>
                 <td class="auto-style162">
                     <asp:Label ID="lblParts10" runat="server" CssClass="auto-style168"></asp:Label>
                 </td>
                 <td class="auto-style137"></td>
                 <td class="auto-style163">
                     <asp:Label ID="Label88" runat="server" Text="Parts-11" CssClass="auto-style168"></asp:Label>
                 </td>
                 <td class="auto-style162">
                     <asp:Label ID="lblParts11" runat="server" CssClass="auto-style168"></asp:Label>
                 </td>
                 <td class="auto-style137"></td>
                 <td class="auto-style171">
                     <asp:Label ID="Label89" runat="server" Text="Parts-12" CssClass="auto-style168"></asp:Label>
                 </td>
                 <td class="auto-style162">
                     <asp:Label ID="lblParts12" runat="server" CssClass="auto-style168"></asp:Label>
                 </td>
             </tr>
             <tr>
                 <td class="auto-style163">
                     <asp:Label ID="Label123" runat="server" Text="QTY" CssClass="auto-style168"></asp:Label>
                 </td>
                 <td class="auto-style162">
                     <asp:Label ID="lblQTY10" runat="server" CssClass="auto-style168"></asp:Label>
                 </td>
                 <td class="auto-style137"></td>
                 <td class="auto-style163">
                     <asp:Label ID="Label92" runat="server" Text="QTY" CssClass="auto-style168"></asp:Label>
                 </td>
                 <td class="auto-style162">
                     <asp:Label ID="lblQTY11" runat="server" CssClass="auto-style168"></asp:Label>
                 </td>
                 <td class="auto-style137"></td>
                 <td class="auto-style171">
                     <asp:Label ID="Label93" runat="server" Text="QTY" CssClass="auto-style168"></asp:Label>
                 </td>
                 <td class="auto-style162">
                     <asp:Label ID="lblQTY12" runat="server" CssClass="auto-style168"></asp:Label>
                 </td>
             </tr>
             <tr>
                 <td class="auto-style163">
                     <asp:Label ID="Label124" runat="server" Text="Unit Price" CssClass="auto-style168"></asp:Label>
                 </td>
                 <td class="auto-style162">
                     <asp:Label ID="lblUnitPrice10" runat="server" CssClass="auto-style168"></asp:Label>
                 </td>
                 <td class="auto-style137"></td>
                 <td class="auto-style163">
                     <asp:Label ID="Label96" runat="server" Text="Unit Price" CssClass="auto-style168"></asp:Label>
                 </td>
                 <td class="auto-style162">
                     <asp:Label ID="lblUnitPrice11" runat="server" CssClass="auto-style168"></asp:Label>
                 </td>
                 <td class="auto-style137"></td>
                 <td class="auto-style171">
                     <asp:Label ID="Label97" runat="server" Text="Unit Price" CssClass="auto-style168"></asp:Label>
                 </td>
                 <td class="auto-style162">
                     <asp:Label ID="lblUnitPrice12" runat="server" CssClass="auto-style168"></asp:Label>
                 </td>
             </tr>
             <tr>
                 <td class="auto-style175"></td>
                 <td class="auto-style176"></td>
                 <td class="auto-style137"></td>
                 <td class="auto-style175"></td>
                 <td class="auto-style176"></td>
                 <td class="auto-style137"></td>
                 <td class="auto-style177"></td>
                 <td class="auto-style176"></td>
             </tr>
             <tr>
                 <td class="auto-style163">
                     <asp:Label ID="Label125" runat="server" Text="Location-13" CssClass="auto-style168"></asp:Label>
                 </td>
                 <td class="auto-style166">
                     <asp:Label ID="lblLocation13" runat="server" CssClass="auto-style168"></asp:Label>
                 </td>
                 <td class="auto-style137"></td>
                 <td class="auto-style163">
                     <asp:Label ID="Label102" runat="server" Text="Location-14" CssClass="auto-style168"></asp:Label>
                 </td>
                 <td class="auto-style166">
                     <asp:Label ID="lblLocation14" runat="server" CssClass="auto-style168"></asp:Label>
                 </td>
                 <td class="auto-style137"></td>
                 <td class="auto-style171">
                     <asp:Label ID="Label103" runat="server" Text="Location-15" CssClass="auto-style168"></asp:Label>
                 </td>
                 <td class="auto-style166">
                     <asp:Label ID="lblLocation15" runat="server" CssClass="auto-style168"></asp:Label>
                 </td>
             </tr>
             <tr>
                 <td class="auto-style163">
                     <asp:Label ID="Label126" runat="server" Text="Parts-13" CssClass="auto-style168"></asp:Label>
                 </td>
                 <td class="auto-style162">
                     <asp:Label ID="lblParts13" runat="server" CssClass="auto-style168"></asp:Label>
                 </td>
                 <td class="auto-style137"></td>
                 <td class="auto-style163">
                     <asp:Label ID="Label106" runat="server" Text="Parts-14" CssClass="auto-style168"></asp:Label>
                 </td>
                 <td class="auto-style162">
                     <asp:Label ID="lblParts14" runat="server" CssClass="auto-style168"></asp:Label>
                 </td>
                 <td class="auto-style137"></td>
                 <td class="auto-style171">
                     <asp:Label ID="Label107" runat="server" Text="Parts-15" CssClass="auto-style168"></asp:Label>
                 </td>
                 <td class="auto-style162">
                     <asp:Label ID="lblParts15" runat="server" CssClass="auto-style168"></asp:Label>
                 </td>
             </tr>
             <tr>
                 <td class="auto-style163">
                     <asp:Label ID="Label127" runat="server" Text="QTY" CssClass="auto-style168"></asp:Label>
                 </td>
                 <td class="auto-style162">
                     <asp:Label ID="lblQTY13" runat="server" CssClass="auto-style168"></asp:Label>
                 </td>
                 <td class="auto-style137"></td>
                 <td class="auto-style163">
                     <asp:Label ID="Label110" runat="server" Text="QTY" CssClass="auto-style168"></asp:Label>
                 </td>
                 <td class="auto-style162">
                     <asp:Label ID="lblQTY14" runat="server" CssClass="auto-style168"></asp:Label>
                 </td>
                 <td class="auto-style137"></td>
                 <td class="auto-style171">
                     <asp:Label ID="Label111" runat="server" Text="QTY" CssClass="auto-style168"></asp:Label>
                 </td>
                 <td class="auto-style162">
                     <asp:Label ID="lblQTY15" runat="server" CssClass="auto-style168"></asp:Label>
                 </td>
             </tr>
             <tr>
                 <td class="auto-style165">
                     <asp:Label ID="Label116" runat="server" Text="Unit Price" CssClass="auto-style168"></asp:Label>
                 </td>
                 <td class="auto-style162">
                     <asp:Label ID="lblUnitPrice13" runat="server" CssClass="auto-style168"></asp:Label>
                 </td>
                 <td class="auto-style137"></td>
                 <td class="auto-style165">
                     <asp:Label ID="Label114" runat="server" Text="Unit Price" CssClass="auto-style168"></asp:Label>
                 </td>
                 <td class="auto-style162">
                     <asp:Label ID="lblUnitPrice14" runat="server" CssClass="auto-style168"></asp:Label>
                 </td>
                 <td class="auto-style137"></td>
                 <td class="auto-style172">
                     <asp:Label ID="Label115" runat="server" Text="Unit Price" CssClass="auto-style168"></asp:Label>
                 </td>
                 <td class="auto-style162">
                     <asp:Label ID="lblUnitPrice15" runat="server" CssClass="auto-style168"></asp:Label>
                 </td>
             </tr>
         </table>
         <br />
      </div>  
    </div>
    <div id="dialog" title="message" style="display:none;"> >
        <asp:Label ID="lblMsg" runat="server" Text=""></asp:Label>
    </div>
</asp:Content>
