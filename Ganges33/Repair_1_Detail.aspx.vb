Public Class Repair_1_Detail
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        '***初期処理***
        If IsPostBack = False Then

            '***セッションなしログインユーザの対応***
            Dim userid As String = Session("user_id")
            If userid = "" Then
                Response.Redirect("Login.aspx")
            End If

            Dim poNo As String = Session("po_No")
            Dim userName As String = Session("user_Name")
            Dim shipCode As String = Session("ship_code")

            '***時間とログイン名を表示****
            '時間を表示
            Dim clsSetCommon As New Class_common
            Dim dtNow As DateTime = clsSetCommon.dtIndia

            lblRecord.Text = dtNow.ToString("yyyyMMddHHmmss")
            lblYousername.Text = userName

            'poの管理番号表示
            lblPo_No.Text = poNo

            '***詳細情報を表示 ***
            Dim clsSet As New Class_money
            Dim clsSet2 As New Class_repair

            Dim strSQL As String
            Dim errFlg As Integer

            strSQL = "SELECT * FROM dbo.T_repair1 WHERE DELFG = 0 AND po_no = '" & poNo & "' AND Branch_Code = '" & shipCode & "';"

            Dim DT_T_repair1 As DataTable = DBCommon.ExecuteGetDT(strSQL, errFlg)

            If errFlg = 1 Then
                Call showMsg("Failed to acquire T_repair 1 information.")
                Exit Sub
            End If

            If DT_T_repair1 IsNot Nothing Then

                If DT_T_repair1.Rows(0)("Engineer") IsNot DBNull.Value Then
                    lblEngineer.Text = DT_T_repair1.Rows(0)("Engineer")
                End If

                If DT_T_repair1.Rows(0)("ASC_Claim_No") IsNot DBNull.Value Then
                    lblASC_Claim_No.Text = DT_T_repair1.Rows(0)("ASC_Claim_No")
                End If

                If DT_T_repair1.Rows(0)("Samsung_Claim_No") IsNot DBNull.Value Then
                    lblSamsung_Claim_No.Text = DT_T_repair1.Rows(0)("Samsung_Claim_No")
                End If

                If DT_T_repair1.Rows(0)("Service_Type") IsNot DBNull.Value Then
                    lblService_type.Text = DT_T_repair1.Rows(0)("Service_Type")
                End If

                If DT_T_repair1.Rows(0)("Purchase_Date") IsNot DBNull.Value Then
                    lblPurchase_Date.Text = DT_T_repair1.Rows(0)("Purchase_Date")
                End If

                If DT_T_repair1.Rows(0)("Repair_Received_Date") IsNot DBNull.Value Then
                    lblRepair_Recived_Date.Text = DT_T_repair1.Rows(0)("Repair_Received_Date")
                End If

                If DT_T_repair1.Rows(0)("Consumer_Name") IsNot DBNull.Value Then
                    lblConsumer_Name.Text = DT_T_repair1.Rows(0)("Consumer_Name")
                End If

                If DT_T_repair1.Rows(0)("Consumer_Addr1") IsNot DBNull.Value Then
                    Dim setStrLabel As String = ""
                    clsSet2.labelSet(DT_T_repair1.Rows(0)("Consumer_Addr1"), setStrLabel, 2)
                    lblAddress1.Text = setStrLabel
                End If

                If DT_T_repair1.Rows(0)("Consumer_Addr2") IsNot DBNull.Value Then
                    Dim setStrLabel As String = ""
                    clsSet2.labelSet(DT_T_repair1.Rows(0)("Consumer_Addr2"), setStrLabel, 2)
                    lblAddress2.Text = setStrLabel
                End If

                If DT_T_repair1.Rows(0)("Consumer_Fax") IsNot DBNull.Value Then
                    lblFax.Text = DT_T_repair1.Rows(0)("Consumer_Fax")
                End If

                If DT_T_repair1.Rows(0)("Postal_Code") IsNot DBNull.Value Then
                    lblPostal_code.Text = DT_T_repair1.Rows(0)("Postal_Code")
                End If

                If DT_T_repair1.Rows(0)("Model") IsNot DBNull.Value Then
                    lblModel.Text = DT_T_repair1.Rows(0)("Model")
                End If

                If DT_T_repair1.Rows(0)("Serial_No") IsNot DBNull.Value Then
                    lblSerialNo.Text = DT_T_repair1.Rows(0)("Serial_No")
                End If

                If DT_T_repair1.Rows(0)("IMEI_No") IsNot DBNull.Value Then
                    lblIMEI.Text = DT_T_repair1.Rows(0)("IMEI_No")
                End If

                If DT_T_repair1.Rows(0)("Defect_Type") IsNot DBNull.Value Then
                    lblDefectType.Text = DT_T_repair1.Rows(0)("Defect_Type")
                End If

                If DT_T_repair1.Rows(0)("Condition") IsNot DBNull.Value Then
                    lblCondition.Text = DT_T_repair1.Rows(0)("Condition")
                End If

                If DT_T_repair1.Rows(0)("Symptom") IsNot DBNull.Value Then
                    lblSymptom.Text = DT_T_repair1.Rows(0)("Symptom")
                End If

                If DT_T_repair1.Rows(0)("Defect_Code") IsNot DBNull.Value Then
                    lblDefectCode.Text = DT_T_repair1.Rows(0)("Defect_Code")
                End If

                If DT_T_repair1.Rows(0)("Repair_Code") IsNot DBNull.Value Then
                    lblRepairCode.Text = DT_T_repair1.Rows(0)("Repair_Code")
                End If

                If DT_T_repair1.Rows(0)("Repair_Description") IsNot DBNull.Value Then
                    Dim setStrLabel As String = ""
                    clsSet2.labelSet(DT_T_repair1.Rows(0)("Repair_Description"), setStrLabel, 10)
                    lblRepairDescription.Text = setStrLabel
                End If

                If DT_T_repair1.Rows(0)("Purchase_Date") IsNot DBNull.Value Then
                    lblPurchaseDate.Text = DT_T_repair1.Rows(0)("Purchase_Date")
                End If

                If DT_T_repair1.Rows(0)("Repair_Received_Date") IsNot DBNull.Value Then
                    lblRepairRecivedDate.Text = DT_T_repair1.Rows(0)("Repair_Received_Date")
                End If

                If DT_T_repair1.Rows(0)("Completed_Date") IsNot DBNull.Value Then
                    lblCompleteDate.Text = DT_T_repair1.Rows(0)("Completed_Date")
                End If

                If DT_T_repair1.Rows(0)("Delivery_Date") IsNot DBNull.Value Then
                    lblDeliveryDate.Text = DT_T_repair1.Rows(0)("Delivery_Date")
                End If

                If DT_T_repair1.Rows(0)("Production_Date") IsNot DBNull.Value Then
                    lblProductionDate.Text = DT_T_repair1.Rows(0)("Production_Date")
                End If

                If DT_T_repair1.Rows(0)("Labor_Amount") IsNot DBNull.Value Then
                    lblLaborAmount.Text = clsSet.setINR(DT_T_repair1.Rows(0)("Labor_Amount"))
                End If

                If DT_T_repair1.Rows(0)("Parts_Amount") IsNot DBNull.Value Then
                    lblPartsAmount.Text = clsSet.setINR(DT_T_repair1.Rows(0)("Parts_Amount"))
                End If

                If DT_T_repair1.Rows(0)("Parts_SGST") IsNot DBNull.Value Then
                    lblPartsSGST.Text = clsSet.setINR(DT_T_repair1.Rows(0)("Parts_SGST"))
                End If

                If DT_T_repair1.Rows(0)("Parts_CGST") IsNot DBNull.Value Then
                    lblPartsCGST.Text = clsSet.setINR(DT_T_repair1.Rows(0)("Parts_CGST"))
                End If

                If DT_T_repair1.Rows(0)("SGST") IsNot DBNull.Value Then
                    lblSGST.Text = clsSet.setINR(DT_T_repair1.Rows(0)("SGST"))
                End If

                If DT_T_repair1.Rows(0)("CGST") IsNot DBNull.Value Then
                    lblCGST.Text = clsSet.setINR(DT_T_repair1.Rows(0)("CGST"))
                End If

                If DT_T_repair1.Rows(0)("Total_Invoice_Amount") IsNot DBNull.Value Then
                    lblInvoiceAmount.Text = clsSet.setINR(DT_T_repair1.Rows(0)("Total_Invoice_Amount"))
                End If

                If DT_T_repair1.Rows(0)("Remark") IsNot DBNull.Value Then
                    lblRemark.Text = DT_T_repair1.Rows(0)("Remark")
                End If

                If DT_T_repair1.Rows(0)("Tr_No") IsNot DBNull.Value Then
                    lblTrackingNo.Text = DT_T_repair1.Rows(0)("Tr_No")
                End If

                If DT_T_repair1.Rows(0)("Status") IsNot DBNull.Value Then
                    lblStatus.Text = DT_T_repair1.Rows(0)("Status")
                End If

                If DT_T_repair1.Rows(0)("Location_1") IsNot DBNull.Value Then
                    lblLocation1.Text = DT_T_repair1.Rows(0)("Location_1")
                End If

                If DT_T_repair1.Rows(0)("Part_1") IsNot DBNull.Value Then
                    lblParts1.Text = DT_T_repair1.Rows(0)("Part_1")
                End If

                If DT_T_repair1.Rows(0)("Qty_1") IsNot DBNull.Value Then
                    lblQTY1.Text = DT_T_repair1.Rows(0)("Qty_1")
                End If

                If DT_T_repair1.Rows(0)("Unit_Price_1") IsNot DBNull.Value Then
                    lblUnitPrice1.Text = DT_T_repair1.Rows(0)("Unit_Price_1")
                End If

                If DT_T_repair1.Rows(0)("Location_2") IsNot DBNull.Value Then
                    lblLocation2.Text = DT_T_repair1.Rows(0)("Location_2")
                End If

                If DT_T_repair1.Rows(0)("Part_2") IsNot DBNull.Value Then
                    lblParts2.Text = DT_T_repair1.Rows(0)("Part_2")
                End If

                If DT_T_repair1.Rows(0)("Qty_2") IsNot DBNull.Value Then
                    lblQTY2.Text = DT_T_repair1.Rows(0)("Qty_2")
                End If

                If DT_T_repair1.Rows(0)("Unit_Price_2") IsNot DBNull.Value Then
                    lblUnitPrice2.Text = DT_T_repair1.Rows(0)("Unit_Price_2")
                End If

                If DT_T_repair1.Rows(0)("Location_3") IsNot DBNull.Value Then
                    lblLocation3.Text = DT_T_repair1.Rows(0)("Location_3")
                End If

                If DT_T_repair1.Rows(0)("Part_3") IsNot DBNull.Value Then
                    lblParts3.Text = DT_T_repair1.Rows(0)("Part_3")
                End If

                If DT_T_repair1.Rows(0)("Qty_3") IsNot DBNull.Value Then
                    lblQTY3.Text = DT_T_repair1.Rows(0)("Qty_3")
                End If

                If DT_T_repair1.Rows(0)("Unit_Price_3") IsNot DBNull.Value Then
                    lblUnitPrice3.Text = DT_T_repair1.Rows(0)("Unit_Price_3")
                End If

                If DT_T_repair1.Rows(0)("Location_4") IsNot DBNull.Value Then
                    lblLocation4.Text = DT_T_repair1.Rows(0)("Location_4")
                End If

                If DT_T_repair1.Rows(0)("Part_4") IsNot DBNull.Value Then
                    lblParts4.Text = DT_T_repair1.Rows(0)("Part_4")
                End If

                If DT_T_repair1.Rows(0)("Qty_4") IsNot DBNull.Value Then
                    lblQTY4.Text = DT_T_repair1.Rows(0)("Qty_4")
                End If

                If DT_T_repair1.Rows(0)("Unit_Price_4") IsNot DBNull.Value Then
                    lblUnitPrice4.Text = DT_T_repair1.Rows(0)("Unit_Price_4")
                End If

                If DT_T_repair1.Rows(0)("Location_5") IsNot DBNull.Value Then
                    lblLocation5.Text = DT_T_repair1.Rows(0)("Location_5")
                End If

                If DT_T_repair1.Rows(0)("Part_5") IsNot DBNull.Value Then
                    lblParts5.Text = DT_T_repair1.Rows(0)("Part_5")
                End If

                If DT_T_repair1.Rows(0)("Qty_5") IsNot DBNull.Value Then
                    lblQTY5.Text = DT_T_repair1.Rows(0)("Qty_5")
                End If

                If DT_T_repair1.Rows(0)("Unit_Price_5") IsNot DBNull.Value Then
                    lblUnitPrice5.Text = DT_T_repair1.Rows(0)("Unit_Price_5")
                End If

                If DT_T_repair1.Rows(0)("Location_6") IsNot DBNull.Value Then
                    lblLocation6.Text = DT_T_repair1.Rows(0)("Location_6")
                End If

                If DT_T_repair1.Rows(0)("Part_6") IsNot DBNull.Value Then
                    lblParts6.Text = DT_T_repair1.Rows(0)("Part_6")
                End If

                If DT_T_repair1.Rows(0)("Qty_6") IsNot DBNull.Value Then
                    lblQTY6.Text = DT_T_repair1.Rows(0)("Qty_6")
                End If

                If DT_T_repair1.Rows(0)("Unit_Price_6") IsNot DBNull.Value Then
                    lblUnitPrice6.Text = DT_T_repair1.Rows(0)("Unit_Price_6")
                End If

                If DT_T_repair1.Rows(0)("Location_7") IsNot DBNull.Value Then
                    lblLocation7.Text = DT_T_repair1.Rows(0)("Location_7")
                End If

                If DT_T_repair1.Rows(0)("Part_7") IsNot DBNull.Value Then
                    lblParts7.Text = DT_T_repair1.Rows(0)("Part_7")
                End If

                If DT_T_repair1.Rows(0)("Qty_7") IsNot DBNull.Value Then
                    lblQTY7.Text = DT_T_repair1.Rows(0)("Qty_7")
                End If

                If DT_T_repair1.Rows(0)("Unit_Price_7") IsNot DBNull.Value Then
                    lblUnitPrice7.Text = DT_T_repair1.Rows(0)("Unit_Price_7")
                End If

                If DT_T_repair1.Rows(0)("Location_8") IsNot DBNull.Value Then
                    lblLocation8.Text = DT_T_repair1.Rows(0)("Location_8")
                End If

                If DT_T_repair1.Rows(0)("Part_8") IsNot DBNull.Value Then
                    lblParts8.Text = DT_T_repair1.Rows(0)("Part_8")
                End If

                If DT_T_repair1.Rows(0)("Qty_8") IsNot DBNull.Value Then
                    lblQTY8.Text = DT_T_repair1.Rows(0)("Qty_8")
                End If

                If DT_T_repair1.Rows(0)("Unit_Price_8") IsNot DBNull.Value Then
                    lblUnitPrice8.Text = DT_T_repair1.Rows(0)("Unit_Price_8")
                End If

                If DT_T_repair1.Rows(0)("Location_9") IsNot DBNull.Value Then
                    lblLocation9.Text = DT_T_repair1.Rows(0)("Location_9")
                End If

                If DT_T_repair1.Rows(0)("Part_9") IsNot DBNull.Value Then
                    lblParts9.Text = DT_T_repair1.Rows(0)("Part_9")
                End If

                If DT_T_repair1.Rows(0)("Qty_9") IsNot DBNull.Value Then
                    lblQTY9.Text = DT_T_repair1.Rows(0)("Qty_9")
                End If

                If DT_T_repair1.Rows(0)("Unit_Price_9") IsNot DBNull.Value Then
                    lblUnitPrice9.Text = DT_T_repair1.Rows(0)("Unit_Price_9")
                End If

                If DT_T_repair1.Rows(0)("Location_10") IsNot DBNull.Value Then
                    lblLocation10.Text = DT_T_repair1.Rows(0)("Location_10")
                End If

                If DT_T_repair1.Rows(0)("Part_10") IsNot DBNull.Value Then
                    lblParts10.Text = DT_T_repair1.Rows(0)("Part_10")
                End If

                If DT_T_repair1.Rows(0)("Qty_10") IsNot DBNull.Value Then
                    lblQTY10.Text = DT_T_repair1.Rows(0)("Qty_10")
                End If

                If DT_T_repair1.Rows(0)("Unit_Price_10") IsNot DBNull.Value Then
                    lblUnitPrice10.Text = DT_T_repair1.Rows(0)("Unit_Price_10")
                End If

                If DT_T_repair1.Rows(0)("Location_11") IsNot DBNull.Value Then
                    lblLocation11.Text = DT_T_repair1.Rows(0)("Location_11")
                End If

                If DT_T_repair1.Rows(0)("Part_11") IsNot DBNull.Value Then
                    lblParts11.Text = DT_T_repair1.Rows(0)("Part_11")
                End If

                If DT_T_repair1.Rows(0)("Qty_11") IsNot DBNull.Value Then
                    lblQTY11.Text = DT_T_repair1.Rows(0)("Qty_11")
                End If

                If DT_T_repair1.Rows(0)("Unit_Price_11") IsNot DBNull.Value Then
                    lblUnitPrice11.Text = DT_T_repair1.Rows(0)("Unit_Price_11")
                End If

                If DT_T_repair1.Rows(0)("Location_12") IsNot DBNull.Value Then
                    lblLocation12.Text = DT_T_repair1.Rows(0)("Location_12")
                End If

                If DT_T_repair1.Rows(0)("Part_12") IsNot DBNull.Value Then
                    lblParts12.Text = DT_T_repair1.Rows(0)("Part_12")
                End If

                If DT_T_repair1.Rows(0)("Qty_12") IsNot DBNull.Value Then
                    lblQTY12.Text = DT_T_repair1.Rows(0)("Qty_12")
                End If

                If DT_T_repair1.Rows(0)("Unit_Price_12") IsNot DBNull.Value Then
                    lblUnitPrice12.Text = DT_T_repair1.Rows(0)("Unit_Price_12")
                End If

                If DT_T_repair1.Rows(0)("Location_13") IsNot DBNull.Value Then
                    lblLocation13.Text = DT_T_repair1.Rows(0)("Location_13")
                End If

                If DT_T_repair1.Rows(0)("Part_13") IsNot DBNull.Value Then
                    lblParts13.Text = DT_T_repair1.Rows(0)("Part_13")
                End If

                If DT_T_repair1.Rows(0)("Qty_13") IsNot DBNull.Value Then
                    lblQTY13.Text = DT_T_repair1.Rows(0)("Qty_13")
                End If

                If DT_T_repair1.Rows(0)("Unit_Price_13") IsNot DBNull.Value Then
                    lblUnitPrice13.Text = DT_T_repair1.Rows(0)("Unit_Price_13")
                End If

                If DT_T_repair1.Rows(0)("Location_14") IsNot DBNull.Value Then
                    lblLocation14.Text = DT_T_repair1.Rows(0)("Location_14")
                End If

                If DT_T_repair1.Rows(0)("Part_14") IsNot DBNull.Value Then
                    lblParts14.Text = DT_T_repair1.Rows(0)("Part_14")
                End If

                If DT_T_repair1.Rows(0)("Qty_14") IsNot DBNull.Value Then
                    lblQTY14.Text = DT_T_repair1.Rows(0)("Qty_14")
                End If

                If DT_T_repair1.Rows(0)("Unit_Price_14") IsNot DBNull.Value Then
                    lblUnitPrice14.Text = DT_T_repair1.Rows(0)("Unit_Price_14")
                End If

                If DT_T_repair1.Rows(0)("Location_15") IsNot DBNull.Value Then
                    lblLocation15.Text = DT_T_repair1.Rows(0)("Location_15")
                End If

                If DT_T_repair1.Rows(0)("Part_15") IsNot DBNull.Value Then
                    lblParts15.Text = DT_T_repair1.Rows(0)("Part_15")
                End If

                If DT_T_repair1.Rows(0)("Qty_15") IsNot DBNull.Value Then
                    lblQTY15.Text = DT_T_repair1.Rows(0)("Qty_15")
                End If

                If DT_T_repair1.Rows(0)("Unit_Price_15") IsNot DBNull.Value Then
                    lblUnitPrice15.Text = DT_T_repair1.Rows(0)("Unit_Price_15")
                End If

            End If

        End If

    End Sub

    Protected Sub showMsg(ByVal Msg As String)

        'Dim formid As String = Me.Form.ClientID

        '' 確認ダイアログを出力するスクリプト
        '' POST先は自分自身(inv_Search.aspx)
        'Dim sScript As String = "if(alert(""" + Msg + """)){ " +
        'formid + ".method = ""post"";" +
        '                                formid + ".action = ""inv_Search.aspx?errMsg=true"";" +
        '                                formid + ".submit();" +
        '                            "}"

        lblMsg.Text = Msg
        Dim sScript As String = "$(function () { $( ""#dialog"" ).dialog({width: 400, buttons: {""OK"": function () {$(this).dialog('close');}}});});"
        'JavaScriptの埋め込み
        ClientScript.RegisterStartupScript(Me.GetType(), "startup", sScript, True)

    End Sub

    Protected Sub btnSearch_Click(sender As Object, e As ImageClickEventArgs) Handles btnSearch.Click

        Response.Redirect("Repair_1.aspx")

    End Sub

End Class