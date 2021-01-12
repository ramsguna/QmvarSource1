Imports System
Imports System.Data
Imports System.Web.Security
Imports System.Collections.Generic
Imports Ganges33.Ganges33.dao
Imports Ganges33.Ganges33.model
Imports Ganges33.Ganges33.logic

Imports System.Reflection


Public Class SalesReportControl
    Public Function SelectSalesReport(ByVal queryParams As SalesReportModel) As DataTable
        Log4NetControl.ComInfoLogWrite(Log4NetControl.UserID)
        Dim dbConn As DBUtility = New DBUtility()
        Dim dt As DataTable = New DataTable()
        Dim sqlStr As String = "SELECT "

        Select Case queryParams.SalesReportType
            Case "InWarranty"
                sqlStr = sqlStr & " Wty_Excel.ASC_Code, Wty_Excel.Branch_Code, Wty_Excel.ASC_Claim_No, Invoice_update.Parts_invoice_No, Invoice_update.Labor_Invoice_No, Wty_Excel.Samsung_Claim_No, Wty_Excel.Service_Type, Wty_Excel.Consumer_Name, Wty_Excel.Consumer_Addr1, Wty_Excel.Consumer_Addr2, Wty_Excel.Consumer_Telephone, Wty_Excel.Consumer_Fax, Wty_Excel.Postal_Code, Wty_Excel.Model, Wty_Excel.Serial_No, Wty_Excel.IMEI_No, Wty_Excel.Defect_Type, Wty_Excel.Condition, Wty_Excel.Symptom, Wty_Excel.Defect_Code, Wty_Excel.Repair_Code, Wty_Excel.Defect_Desc, Wty_Excel.Repair_Description, Wty_Excel.Purchase_Date, Wty_Excel.Repair_Received_Date, Wty_Excel.Completed_Date, Wty_Excel.Delivery_Date, Wty_Excel.Production_Date, Invoice_update.Labor as 'Labor Amount', Invoice_update.Parts as 'Part Amount', Wty_Excel.Freight, Wty_Excel.Other, Wty_Excel.Parts_SGST, Wty_Excel.Parts_UTGST,Wty_Excel.Parts_CGST, Wty_Excel.Parts_IGST, Wty_Excel.Parts_Cess, Wty_Excel.SGST, Wty_Excel.UTGST, Wty_Excel.CGST, Wty_Excel.IGST, Wty_Excel.Cess, Invoice_update.Invoice as 'Total Invoice Amount', Wty_Excel.Remark, Wty_Excel.Tr_No, Wty_Excel.Tr_Type,  Wty_Excel.Status, Wty_Excel.Engineer, Wty_Excel.Collection_Point, Wty_Excel.Collection_Point_Name, Wty_Excel.Location_1, Wty_Excel.Part_1, Wty_Excel.Qty_1, Wty_Excel.Unit_Price_1, Wty_Excel.Doc_Num_1,  Wty_Excel.Matrial_Serial_1, Wty_Excel.Location_2, Wty_Excel.Part_2, Wty_Excel.Qty_2, Wty_Excel.Unit_Price_2, Wty_Excel.Doc_Num_2, Wty_Excel.Matrial_Serial_2, Wty_Excel.Location_3, Wty_Excel.Part_3,   Wty_Excel.Qty_3, Wty_Excel.Unit_Price_3, Wty_Excel.Doc_Num_3, Wty_Excel.Matrial_Serial_3, Wty_Excel.Location_4, Wty_Excel.Part_4, Wty_Excel.Qty_4, Wty_Excel.Unit_Price_4, Wty_Excel.Doc_Num_4,   Wty_Excel.Matrial_Serial_4, Wty_Excel.Location_5, Wty_Excel.Part_5, Wty_Excel.Qty_5, Wty_Excel.Unit_Price_5, Wty_Excel.Doc_Num_5, Wty_Excel.Matrial_Serial_5, Wty_Excel.Location_6, Wty_Excel.Part_6,  Wty_Excel.Qty_6, Wty_Excel.Unit_Price_6, Wty_Excel.Doc_Num_6, Wty_Excel.Matrial_Serial_6, Wty_Excel.Location_7, Wty_Excel.Part_7, Wty_Excel.Qty_7, Wty_Excel.Unit_Price_7, Wty_Excel.Doc_Num_7,   Wty_Excel.Matrial_Serial_7, Wty_Excel.Location_8, Wty_Excel.Part_8, Wty_Excel.Qty_8, Wty_Excel.Unit_Price_8, Wty_Excel.Doc_Num_8, Wty_Excel.Matrial_Serial_8, Wty_Excel.Location_9, Wty_Excel.Part_9,  Wty_Excel.Qty_9, Wty_Excel.Unit_Price_9, Wty_Excel.Doc_Num_9, Wty_Excel.Matrial_Serial_9, Wty_Excel.Location_10, Wty_Excel.Part_10, Wty_Excel.Qty_10, Wty_Excel.Unit_Price_10, Wty_Excel.Doc_Num_10,   Wty_Excel.Matrial_Serial_10, Wty_Excel.Location_11, Wty_Excel.Part_11, Wty_Excel.Qty_11, Wty_Excel.Unit_Price_11, Wty_Excel.Doc_Num_11, Wty_Excel.Matrial_Serial_11, Wty_Excel.Location_12, Wty_Excel.Part_12,   Wty_Excel.Qty_12, Wty_Excel.Unit_Price_12, Wty_Excel.Doc_Num_12, Wty_Excel.Matrial_Serial_12, Wty_Excel.Location_13, Wty_Excel.Part_13, Wty_Excel.Qty_13, Wty_Excel.Unit_Price_13, Wty_Excel.Doc_Num_13, Wty_Excel.Matrial_Serial_13, Wty_Excel.Location_14, Wty_Excel.Part_14, Wty_Excel.Qty_14, Wty_Excel.Unit_Price_14, Wty_Excel.Doc_Num_14, Wty_Excel.Matrial_Serial_14, Wty_Excel.Location_15, Wty_Excel.Part_15,Wty_Excel.Qty_15, Wty_Excel.Unit_Price_15, Wty_Excel.Doc_Num_15, Wty_Excel.Matrial_Serial_15 "
                sqlStr = sqlStr & " FROM            Wty_Excel INNER JOIN "
                sqlStr = sqlStr & "  Invoice_update ON Wty_Excel.ASC_Claim_No = Invoice_update.Your_Ref_No "
                sqlStr = sqlStr & "WHERE "
                sqlStr = sqlStr & "Wty_Excel.DELFG=0 "
                sqlStr = sqlStr & "AND Wty_Excel.Status  in ('10','20') "

                If Not String.IsNullOrEmpty(queryParams.ShipToBranchCode) Then
                    sqlStr = sqlStr & "AND Wty_Excel.Branch_Code = @ShipToBranchCode "
                    dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@ShipToBranchCode", queryParams.ShipToBranchCode))
                End If

                If (Not (String.IsNullOrEmpty(queryParams.DateFrom)) And (Not (String.IsNullOrEmpty(queryParams.DateTo)))) Then
                    sqlStr = sqlStr & "AND LEFT(CONVERT(VARCHAR, Wty_Excel.Delivery_Date, 111), 10) >= @DateFrom and LEFT(CONVERT(VARCHAR, Wty_Excel.Delivery_Date, 111), 10) <= @DateTo "
                    'sqlStr = sqlStr & "AND INVOICE_DATE >= @DateFrom and INVOICE_DATE <= @DateTo "
                    dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@DateFrom", queryParams.DateFrom))
                    dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@DateTo", queryParams.DateTo))
                ElseIf Not String.IsNullOrEmpty(queryParams.DateFrom) Then
                    'sqlStr = sqlStr & "AND LEFT(CONVERT(VARCHAR, INVOICE_DATE, 111), 10) = @DateFrom "
                    sqlStr = sqlStr & "AND Wty_Excel.Delivery_Date = @DateFrom "
                    dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@DateFrom", queryParams.DateFrom))
                ElseIf Not String.IsNullOrEmpty(queryParams.DateTo) Then
                    sqlStr = sqlStr & "AND Wty_Excel.Delivery_Date = @DateTo "
                    'sqlStr = sqlStr & "AND LEFT(CONVERT(VARCHAR, INVOICE_DATE, 111), 10) = @DateTo "
                    dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@DateTo", queryParams.DateTo))
                End If
            Case "OutWarranty"
                sqlStr = sqlStr & "   Wty_Excel.ASC_Code, Wty_Excel.Branch_Code, Wty_Excel.ASC_Claim_No, Invoice_update.Parts_invoice_No, Invoice_update.Labor_Invoice_No, Wty_Excel.Samsung_Claim_No, Wty_Excel.Service_Type,Wty_Excel.Consumer_Name, Wty_Excel.Consumer_Addr1, Wty_Excel.Consumer_Addr2, Wty_Excel.Consumer_Telephone, Wty_Excel.Consumer_Fax, Wty_Excel.Postal_Code, Wty_Excel.Model, Wty_Excel.Serial_No, Wty_Excel.IMEI_No, Wty_Excel.Defect_Type, Wty_Excel.Condition, Wty_Excel.Symptom, Wty_Excel.Defect_Code, Wty_Excel.Repair_Code, Wty_Excel.Defect_Desc, Wty_Excel.Repair_Description, Wty_Excel.Purchase_Date, Wty_Excel.Repair_Received_Date, Wty_Excel.Completed_Date, Wty_Excel.Delivery_Date, Wty_Excel.Production_Date, SC_DSR.OW_Labor AS 'Labor Amount', SC_DSR.OW_Parts AS 'Part Amount', Wty_Excel.Freight,Wty_Excel.Other, Wty_Excel.Parts_SGST, Wty_Excel.Parts_UTGST, Wty_Excel.Parts_CGST, Wty_Excel.Parts_IGST, Wty_Excel.Parts_Cess, Wty_Excel.SGST, Wty_Excel.UTGST, Wty_Excel.CGST, Wty_Excel.IGST, Wty_Excel.Cess, SC_DSR.OW_total AS 'Total Invoice Amount', Wty_Excel.Remark, Wty_Excel.Tr_No, Wty_Excel.Tr_Type, Wty_Excel.Status, Wty_Excel.Engineer, Wty_Excel.Collection_Point,Wty_Excel.Collection_Point_Name, Wty_Excel.Location_1, Wty_Excel.Part_1, Wty_Excel.Qty_1, Wty_Excel.Unit_Price_1, Wty_Excel.Doc_Num_1, Wty_Excel.Matrial_Serial_1, Wty_Excel.Location_2, Wty_Excel.Part_2,Wty_Excel.Qty_2, Wty_Excel.Unit_Price_2, Wty_Excel.Doc_Num_2, Wty_Excel.Matrial_Serial_2, Wty_Excel.Location_3, Wty_Excel.Part_3, Wty_Excel.Qty_3, Wty_Excel.Unit_Price_3, Wty_Excel.Doc_Num_3, Wty_Excel.Matrial_Serial_3, Wty_Excel.Location_4, Wty_Excel.Part_4, Wty_Excel.Qty_4, Wty_Excel.Unit_Price_4, Wty_Excel.Doc_Num_4, Wty_Excel.Matrial_Serial_4, Wty_Excel.Location_5, Wty_Excel.Part_5,Wty_Excel.Qty_5, Wty_Excel.Unit_Price_5, Wty_Excel.Doc_Num_5, Wty_Excel.Matrial_Serial_5, Wty_Excel.Location_6, Wty_Excel.Part_6, Wty_Excel.Qty_6, Wty_Excel.Unit_Price_6, Wty_Excel.Doc_Num_6,  Wty_Excel.Matrial_Serial_6, Wty_Excel.Location_7, Wty_Excel.Part_7, Wty_Excel.Qty_7, Wty_Excel.Unit_Price_7, Wty_Excel.Doc_Num_7, Wty_Excel.Matrial_Serial_7, Wty_Excel.Location_8, Wty_Excel.Part_8,  Wty_Excel.Qty_8, Wty_Excel.Unit_Price_8, Wty_Excel.Doc_Num_8, Wty_Excel.Matrial_Serial_8, Wty_Excel.Location_9, Wty_Excel.Part_9, Wty_Excel.Qty_9, Wty_Excel.Unit_Price_9, Wty_Excel.Doc_Num_9, Wty_Excel.Matrial_Serial_9, Wty_Excel.Location_10, Wty_Excel.Part_10, Wty_Excel.Qty_10, Wty_Excel.Unit_Price_10, Wty_Excel.Doc_Num_10, Wty_Excel.Matrial_Serial_10, Wty_Excel.Location_11, Wty_Excel.Part_11, Wty_Excel.Qty_11, Wty_Excel.Unit_Price_11, Wty_Excel.Doc_Num_11, Wty_Excel.Matrial_Serial_11, Wty_Excel.Location_12, Wty_Excel.Part_12, Wty_Excel.Qty_12, Wty_Excel.Unit_Price_12, Wty_Excel.Doc_Num_12,Wty_Excel.Matrial_Serial_12, Wty_Excel.Location_13, Wty_Excel.Part_13, Wty_Excel.Qty_13, Wty_Excel.Unit_Price_13, Wty_Excel.Doc_Num_13, Wty_Excel.Matrial_Serial_13, Wty_Excel.Location_14, Wty_Excel.Part_14,  Wty_Excel.Qty_14, Wty_Excel.Unit_Price_14, Wty_Excel.Doc_Num_14, Wty_Excel.Matrial_Serial_14, Wty_Excel.Location_15, Wty_Excel.Part_15, Wty_Excel.Qty_15, Wty_Excel.Unit_Price_15, Wty_Excel.Doc_Num_15,  Wty_Excel.Matrial_Serial_15 "
                sqlStr = sqlStr & " FROM            Wty_Excel INNER JOIN "
                sqlStr = sqlStr & "  Invoice_update ON Wty_Excel.ASC_Claim_No = Invoice_update.Your_Ref_No INNER JOIN"
                sqlStr = sqlStr & "  SC_DSR ON Wty_Excel.ASC_Claim_No = SC_DSR.ServiceOrder_No "
                sqlStr = sqlStr & "WHERE "
                sqlStr = sqlStr & "Wty_Excel.DELFG=0 "
                sqlStr = sqlStr & "AND Wty_Excel.Status  in ('80') "

                If Not String.IsNullOrEmpty(queryParams.ShipToBranchCode) Then
                    sqlStr = sqlStr & "AND Wty_Excel.Branch_Code = @ShipToBranchCode "
                    dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@ShipToBranchCode", queryParams.ShipToBranchCode))
                End If

                If (Not (String.IsNullOrEmpty(queryParams.DateFrom)) And (Not (String.IsNullOrEmpty(queryParams.DateTo)))) Then
                    sqlStr = sqlStr & "AND LEFT(CONVERT(VARCHAR, Wty_Excel.Delivery_Date, 111), 10) >= @DateFrom and LEFT(CONVERT(VARCHAR, Wty_Excel.Delivery_Date, 111), 10) <= @DateTo "
                    'sqlStr = sqlStr & "AND INVOICE_DATE >= @DateFrom and INVOICE_DATE <= @DateTo "
                    dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@DateFrom", queryParams.DateFrom))
                    dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@DateTo", queryParams.DateTo))
                ElseIf Not String.IsNullOrEmpty(queryParams.DateFrom) Then
                    'sqlStr = sqlStr & "AND LEFT(CONVERT(VARCHAR, INVOICE_DATE, 111), 10) = @DateFrom "
                    sqlStr = sqlStr & "AND Wty_Excel.Delivery_Date = @DateFrom "
                    dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@DateFrom", queryParams.DateFrom))
                ElseIf Not String.IsNullOrEmpty(queryParams.DateTo) Then
                    sqlStr = sqlStr & "AND Wty_Excel.Delivery_Date = @DateTo "
                    'sqlStr = sqlStr & "AND LEFT(CONVERT(VARCHAR, INVOICE_DATE, 111), 10) = @DateTo "
                    dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@DateTo", queryParams.DateTo))
                End If

            Case "OtherSales"
                sqlStr = sqlStr & " 
                         DISTINCT Wty_Excel.ASC_Code, Wty_Excel.Branch_Code, Wty_Excel.ASC_Claim_No, Wty_Excel.Samsung_Claim_No, Wty_Excel.Service_Type, Wty_Excel.Consumer_Name, Wty_Excel.Consumer_Addr1, Wty_Excel.Consumer_Addr2, 
                         Wty_Excel.Consumer_Telephone, Wty_Excel.Consumer_Fax, Wty_Excel.Postal_Code, Wty_Excel.Model, Wty_Excel.Serial_No, Wty_Excel.IMEI_No, Wty_Excel.Defect_Type, Wty_Excel.Condition, Wty_Excel.Symptom, 
                         Wty_Excel.Defect_Code, Wty_Excel.Repair_Code, Wty_Excel.Defect_Desc, Wty_Excel.Repair_Description, Wty_Excel.Purchase_Date, Wty_Excel.Repair_Received_Date, Wty_Excel.Completed_Date, 
                         Wty_Excel.Delivery_Date, Wty_Excel.Production_Date, Wty_Excel.Freight, Wty_Excel.Other, Wty_Excel.Parts_SGST, Wty_Excel.Parts_UTGST, Wty_Excel.Parts_CGST, Wty_Excel.Parts_IGST, Wty_Excel.Parts_Cess, 
                         Wty_Excel.SGST, Wty_Excel.UTGST, Wty_Excel.CGST, Wty_Excel.IGST, Wty_Excel.Cess, Wty_Excel.Remark, Wty_Excel.Tr_No, Wty_Excel.Tr_Type, Wty_Excel.Status, Wty_Excel.Engineer, Wty_Excel.Collection_Point, 
                         Wty_Excel.Collection_Point_Name, Wty_Excel.Location_1, Wty_Excel.Part_1, Wty_Excel.Qty_1, Wty_Excel.Unit_Price_1, Wty_Excel.Doc_Num_1, Wty_Excel.Matrial_Serial_1, Wty_Excel.Location_2, Wty_Excel.Part_2, 
                         Wty_Excel.Qty_2, Wty_Excel.Unit_Price_2, Wty_Excel.Doc_Num_2, Wty_Excel.Matrial_Serial_2, Wty_Excel.Location_3, Wty_Excel.Part_3, Wty_Excel.Qty_3, Wty_Excel.Unit_Price_3, Wty_Excel.Doc_Num_3, 
                         Wty_Excel.Matrial_Serial_3, Wty_Excel.Location_4, Wty_Excel.Part_4, Wty_Excel.Qty_4, Wty_Excel.Unit_Price_4, Wty_Excel.Doc_Num_4, Wty_Excel.Matrial_Serial_4, Wty_Excel.Location_5, Wty_Excel.Part_5, 
                         Wty_Excel.Qty_5, Wty_Excel.Unit_Price_5, Wty_Excel.Doc_Num_5, Wty_Excel.Matrial_Serial_5, Wty_Excel.Location_6, Wty_Excel.Part_6, Wty_Excel.Qty_6, Wty_Excel.Unit_Price_6, Wty_Excel.Doc_Num_6, 
                         Wty_Excel.Matrial_Serial_6, Wty_Excel.Location_7, Wty_Excel.Part_7, Wty_Excel.Qty_7, Wty_Excel.Unit_Price_7, Wty_Excel.Doc_Num_7, Wty_Excel.Matrial_Serial_7, Wty_Excel.Location_8, Wty_Excel.Part_8, 
                         Wty_Excel.Qty_8, Wty_Excel.Unit_Price_8, Wty_Excel.Doc_Num_8, Wty_Excel.Matrial_Serial_8, Wty_Excel.Location_9, Wty_Excel.Part_9, Wty_Excel.Qty_9, Wty_Excel.Unit_Price_9, Wty_Excel.Doc_Num_9, 
                         Wty_Excel.Matrial_Serial_9, Wty_Excel.Location_10, Wty_Excel.Part_10, Wty_Excel.Qty_10, Wty_Excel.Unit_Price_10, Wty_Excel.Doc_Num_10, Wty_Excel.Matrial_Serial_10, Wty_Excel.Location_11, Wty_Excel.Part_11, 
                         Wty_Excel.Qty_11, Wty_Excel.Unit_Price_11, Wty_Excel.Doc_Num_11, Wty_Excel.Matrial_Serial_11, Wty_Excel.Location_12, Wty_Excel.Part_12, Wty_Excel.Qty_12, Wty_Excel.Unit_Price_12, Wty_Excel.Doc_Num_12, 
                         Wty_Excel.Matrial_Serial_12, Wty_Excel.Location_13, Wty_Excel.Part_13, Wty_Excel.Qty_13, Wty_Excel.Unit_Price_13, Wty_Excel.Doc_Num_13, Wty_Excel.Matrial_Serial_13, Wty_Excel.Location_14, Wty_Excel.Part_14, 
                         Wty_Excel.Qty_14, Wty_Excel.Unit_Price_14, Wty_Excel.Doc_Num_14, Wty_Excel.Matrial_Serial_14, Wty_Excel.Location_15, Wty_Excel.Part_15, Wty_Excel.Qty_15, Wty_Excel.Unit_Price_15, Wty_Excel.Doc_Num_15, 
                         Wty_Excel.Matrial_Serial_15, 
                        SAW_DISCOUNT.STATUS_DESC, Invoice_update.Parts_invoice_No, Invoice_update.Labor_Invoice_No, 
                        SAW_DISCOUNT.REQ_COMMENT, SAW_DISCOUNT.MODEL_CODE,
                        Invoice_update.Labor, Invoice_update.Parts  "

                sqlStr = sqlStr & "FROM            Wty_Excel INNER JOIN "
                sqlStr = sqlStr & "SAW_DISCOUNT ON Wty_Excel.ASC_Claim_No = SAW_DISCOUNT.OBJECT_ID "
                sqlStr = sqlStr & "INNER JOIN  "
                sqlStr = sqlStr & "Invoice_update ON Wty_Excel.ASC_Claim_No = Invoice_update.Your_Ref_No  "
                sqlStr = sqlStr & "WHERE Wty_Excel.DELFG=0 "
                sqlStr = sqlStr & "AND lower(SAW_DISCOUNT.STATUS_DESC)='approved' "


                If Not String.IsNullOrEmpty(queryParams.ShipToBranchCode) Then
                    sqlStr = sqlStr & "AND Wty_Excel.Branch_Code = @ShipToBranchCode "
                    dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@ShipToBranchCode", queryParams.ShipToBranchCode))
                End If

                If (Not (String.IsNullOrEmpty(queryParams.DateFrom)) And (Not (String.IsNullOrEmpty(queryParams.DateTo)))) Then
                    sqlStr = sqlStr & "AND LEFT(CONVERT(VARCHAR, Wty_Excel.Delivery_Date, 111), 10) >= @DateFrom and LEFT(CONVERT(VARCHAR, Wty_Excel.Delivery_Date, 111), 10) <= @DateTo "
                    'sqlStr = sqlStr & "AND INVOICE_DATE >= @DateFrom and INVOICE_DATE <= @DateTo "
                    dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@DateFrom", queryParams.DateFrom))
                    dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@DateTo", queryParams.DateTo))
                ElseIf Not String.IsNullOrEmpty(queryParams.DateFrom) Then
                    'sqlStr = sqlStr & "AND LEFT(CONVERT(VARCHAR, INVOICE_DATE, 111), 10) = @DateFrom "
                    sqlStr = sqlStr & "AND Wty_Excel.Delivery_Date = @DateFrom "
                    dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@DateFrom", queryParams.DateFrom))
                ElseIf Not String.IsNullOrEmpty(queryParams.DateTo) Then
                    sqlStr = sqlStr & "AND Wty_Excel.Delivery_Date = @DateTo "
                    'sqlStr = sqlStr & "AND LEFT(CONVERT(VARCHAR, INVOICE_DATE, 111), 10) = @DateTo "
                    dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@DateTo", queryParams.DateTo))
                End If
                dt = dbConn.GetDataSet(sqlStr)
                dbConn.CloseConnection()
                Dim _datWarrantyCsvList As List(Of WarrantyCsvModel) = New List(Of WarrantyCsvModel)()
                '1st
                ''----------------------------- LABOR : 0 Original : 350.00 FREIGHT : 0 Original : 0.00 PARTS : 111.74 Original : 6320.25 OTHER : 0 Original : 0.00 ----------------------------- FOC Approval for Customer VOC
                ''  UPDATE [dbo].[SAW_DISCOUNT] SET [REQ_COMMENT]='----------------------------- LABOR : 0 Original : 350.00 FREIGHT : 0 Original : 0.00 PARTS : 111.74 Original : 6320.25 OTHER : 0 Original : 0.00 ----------------------------- FOC Approval for Customer VOC' WHERE MODEL_CODE='SM-G973FZWDINS'
                '2nd
                ''----------------------------- LABOR : 350 Original : 350.00 FREIGHT : 0 Original : 0.00 PARTS : 0 Original : 6320.25 OTHER : 0 Original : 0.00 ----------------------------- FOC Approval for Customer VOC
                ''  UPDATE [dbo].[SAW_DISCOUNT] SET [REQ_COMMENT]='----------------------------- LABOR : 350 Original : 350.00 FREIGHT : 0 Original : 0.00 PARTS : 0 Original : 6320.25 OTHER : 0 Original : 0.00 ----------------------------- FOC Approval for Customer VOC' WHERE MODEL_CODE='SM-G973FZWDINS'
                Dim strParts As String = ""
                Dim intLocation As Int16 = 0
                Dim numChk As Decimal = 0
                Dim tmpStrValue As String = "" 'For remove the negative symbol
                If dt IsNot Nothing AndAlso dt.Rows.Count <> 0 Then
                    For i As Integer = 0 To dt.Rows.Count - 1
                        Dim datWarrantyCsv As WarrantyCsvModel = New WarrantyCsvModel()
                        datWarrantyCsv.AscCode = dt.Rows(i)("ASC_Code").ToString()
                        datWarrantyCsv.BranchCode = dt.Rows(i)("Branch_Code").ToString()
                        datWarrantyCsv.AscClaimNo = dt.Rows(i)("ASC_Claim_No").ToString()
                        datWarrantyCsv.ModelName = dt.Rows(i)("MODEL_CODE").ToString()
                        tmpStrValue = dt.Rows(i)("Parts").ToString()
                        datWarrantyCsv.SawDiscountPartsAmount = tmpStrValue.Replace("-", "")
                        tmpStrValue = dt.Rows(i)("Labor").ToString()
                        datWarrantyCsv.SawDiscountLaborAmount = tmpStrValue.Replace("-", "")
                        strParts = dt.Rows(i)("REQ_COMMENT").ToString()
                        intLocation = InStr(strParts, "PARTS :")
                        If intLocation > 0 Then
                            ''Assign and Delimiting
                            strParts = Trim(Right(strParts, Len(strParts) - (intLocation) - 6))
                            intLocation = InStr(strParts, " ")
                            strParts = Trim(Left(strParts, intLocation))
                            'Need to check the Retail price is valid number or characters
                            If Decimal.TryParse(strParts, numChk) = True Then
                                datWarrantyCsv.PartAmountRetailPriceWithoutTax = numChk
                            Else 'If not found valida retail price
                                datWarrantyCsv.PartAmountRetailPriceWithoutTax = 0.00
                            End If
                        Else
                            datWarrantyCsv.PartAmountRetailPriceWithoutTax = 0.00
                        End If
                        datWarrantyCsv.InvoiceAmountCollectedFromCust = datWarrantyCsv.PartAmountRetailPriceWithoutTax - datWarrantyCsv.SawDiscountPartsAmount
                        datWarrantyCsv.PartInvoiceNo = dt.Rows(i)("Parts_invoice_No").ToString()
                        datWarrantyCsv.LabourInvoiceNo = dt.Rows(i)("Labor_Invoice_No").ToString()
                        'There is no specification, so keep empty as of now
                        datWarrantyCsv.SawRemarks = ""
                        _datWarrantyCsvList.Add(datWarrantyCsv)
                    Next
                End If
                ''convert List to datatable
                Return ListToDataTable(_datWarrantyCsvList)
        End Select
        Dim _DataTable As DataTable = dbConn.GetDataSet(sqlStr)
        dbConn.CloseConnection()
        Return _DataTable
    End Function



    Public Shared Function ListToDataTable(Of T)(ByVal list As List(Of T)) As DataTable
        Dim dt As DataTable = New DataTable()

        For Each info As PropertyInfo In GetType(T).GetProperties()
            dt.Columns.Add(New DataColumn(info.Name, info.PropertyType))
        Next

        For Each t1 As T In list
            Dim row As DataRow = dt.NewRow()
            For Each info As PropertyInfo In GetType(T).GetProperties()
                row(info.Name) = info.GetValue(t1, Nothing)
            Next
            dt.Rows.Add(row)
        Next

        dt.Columns(0).ColumnName = "ASC Code"
        dt.Columns(1).ColumnName = "Branch Code"
        dt.Columns(2).ColumnName = "ASC Claim No"
        dt.Columns(3).ColumnName = "Model Name"
        dt.Columns(4).ColumnName = "Part Amount (Retail Price) with out tax"
        dt.Columns(5).ColumnName = "SAW Discount Parts Amount"
        dt.Columns(6).ColumnName = "SAW Discount Labor Amount"
        dt.Columns(7).ColumnName = "Invoice Amount collected from Cus"
        dt.Columns(8).ColumnName = "Part Invoice No"
        dt.Columns(8).ColumnName = "Labour Invoice No"
        dt.Columns(8).ColumnName = "SAW Remarks"

        Return dt
    End Function
    ''''''''''''''''
    '''Backup
    '''
    ''''''Case "OtherSales"
    ''''''     sqlStr = sqlStr & "
    ''''''             Wty_Excel.ASC_Code, Wty_Excel.Branch_Code, Wty_Excel.ASC_Claim_No, Wty_Excel.Samsung_Claim_No, Wty_Excel.Service_Type, Wty_Excel.Consumer_Name, Wty_Excel.Consumer_Addr1, Wty_Excel.Consumer_Addr2, Wty_Excel.Consumer_Telephone, 
    ''''''              Wty_Excel.Consumer_Fax, Wty_Excel.Postal_Code, Wty_Excel.Model, Wty_Excel.Serial_No, Wty_Excel.IMEI_No, Wty_Excel.Defect_Type, Wty_Excel.Condition, Wty_Excel.Symptom, Wty_Excel.Defect_Code, 
    ''''''              Wty_Excel.Repair_Code, Wty_Excel.Defect_Desc, Wty_Excel.Repair_Description, Wty_Excel.Purchase_Date, Wty_Excel.Repair_Received_Date, Wty_Excel.Completed_Date, Wty_Excel.Delivery_Date, 
    ''''''              Wty_Excel.Production_Date, Wty_Excel.Freight, Wty_Excel.Other, Wty_Excel.Parts_SGST, Wty_Excel.Parts_UTGST, Wty_Excel.Parts_CGST, Wty_Excel.Parts_IGST, Wty_Excel.Parts_Cess, Wty_Excel.SGST, 
    ''''''              Wty_Excel.UTGST, Wty_Excel.CGST, Wty_Excel.IGST, Wty_Excel.Cess, Wty_Excel.Remark, Wty_Excel.Tr_No, Wty_Excel.Tr_Type, Wty_Excel.Status, Wty_Excel.Engineer, Wty_Excel.Collection_Point, 
    ''''''              Wty_Excel.Collection_Point_Name, Wty_Excel.Location_1, Wty_Excel.Part_1, Wty_Excel.Qty_1, Wty_Excel.Unit_Price_1, Wty_Excel.Doc_Num_1, Wty_Excel.Matrial_Serial_1, Wty_Excel.Location_2, Wty_Excel.Part_2, 
    ''''''              Wty_Excel.Qty_2, Wty_Excel.Unit_Price_2, Wty_Excel.Doc_Num_2, Wty_Excel.Matrial_Serial_2, Wty_Excel.Location_3, Wty_Excel.Part_3, Wty_Excel.Qty_3, Wty_Excel.Unit_Price_3, Wty_Excel.Doc_Num_3, 
    ''''''              Wty_Excel.Matrial_Serial_3, Wty_Excel.Location_4, Wty_Excel.Part_4, Wty_Excel.Qty_4, Wty_Excel.Unit_Price_4, Wty_Excel.Doc_Num_4, Wty_Excel.Matrial_Serial_4, Wty_Excel.Location_5, Wty_Excel.Part_5, 
    ''''''              Wty_Excel.Qty_5, Wty_Excel.Unit_Price_5, Wty_Excel.Doc_Num_5, Wty_Excel.Matrial_Serial_5, Wty_Excel.Location_6, Wty_Excel.Part_6, Wty_Excel.Qty_6, Wty_Excel.Unit_Price_6, Wty_Excel.Doc_Num_6, 
    ''''''              Wty_Excel.Matrial_Serial_6, Wty_Excel.Location_7, Wty_Excel.Part_7, Wty_Excel.Qty_7, Wty_Excel.Unit_Price_7, Wty_Excel.Doc_Num_7, Wty_Excel.Matrial_Serial_7, Wty_Excel.Location_8, Wty_Excel.Part_8, 
    ''''''              Wty_Excel.Qty_8, Wty_Excel.Unit_Price_8, Wty_Excel.Doc_Num_8, Wty_Excel.Matrial_Serial_8, Wty_Excel.Location_9, Wty_Excel.Part_9, Wty_Excel.Qty_9, Wty_Excel.Unit_Price_9, Wty_Excel.Doc_Num_9, 
    ''''''              Wty_Excel.Matrial_Serial_9, Wty_Excel.Location_10, Wty_Excel.Part_10, Wty_Excel.Qty_10, Wty_Excel.Unit_Price_10, Wty_Excel.Doc_Num_10, Wty_Excel.Matrial_Serial_10, Wty_Excel.Location_11, Wty_Excel.Part_11, 
    ''''''              Wty_Excel.Qty_11, Wty_Excel.Unit_Price_11, Wty_Excel.Doc_Num_11, Wty_Excel.Matrial_Serial_11, Wty_Excel.Location_12, Wty_Excel.Part_12, Wty_Excel.Qty_12, Wty_Excel.Unit_Price_12, Wty_Excel.Doc_Num_12, 
    ''''''              Wty_Excel.Matrial_Serial_12, Wty_Excel.Location_13, Wty_Excel.Part_13, Wty_Excel.Qty_13, Wty_Excel.Unit_Price_13, Wty_Excel.Doc_Num_13, Wty_Excel.Matrial_Serial_13, Wty_Excel.Location_14, Wty_Excel.Part_14, 
    ''''''              Wty_Excel.Qty_14, Wty_Excel.Unit_Price_14, Wty_Excel.Doc_Num_14, Wty_Excel.Matrial_Serial_14, Wty_Excel.Location_15, Wty_Excel.Part_15, Wty_Excel.Qty_15, Wty_Excel.Unit_Price_15, Wty_Excel.Doc_Num_15, 
    ''''''              Wty_Excel.Matrial_Serial_15 "

    ''''''     sqlStr = sqlStr & "FROM            Wty_Excel INNER JOIN "
    ''''''     sqlStr = sqlStr & "SAW_DISCOUNT ON Wty_Excel.ASC_Claim_No = SAW_DISCOUNT.OBJECT_ID "
    ''''''     sqlStr = sqlStr & "WHERE Wty_Excel.DELFG=0 "
    ''''''     sqlStr = sqlStr & "AND lower(SAW_DISCOUNT.STATUS_DESC)='approved' "


    ''''''     If Not String.IsNullOrEmpty(queryParams.ShipToBranchCode) Then
    ''''''         sqlStr = sqlStr & "AND Wty_Excel.Branch_Code = @ShipToBranchCode "
    ''''''         dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@ShipToBranchCode", queryParams.ShipToBranchCode))
    ''''''     End If

    ''''''     If (Not (String.IsNullOrEmpty(queryParams.DateFrom)) And (Not (String.IsNullOrEmpty(queryParams.DateTo)))) Then
    ''''''         sqlStr = sqlStr & "AND LEFT(CONVERT(VARCHAR, Wty_Excel.Delivery_Date, 111), 10) >= @DateFrom and LEFT(CONVERT(VARCHAR, Wty_Excel.Delivery_Date, 111), 10) <= @DateTo "
    ''''''         'sqlStr = sqlStr & "AND INVOICE_DATE >= @DateFrom and INVOICE_DATE <= @DateTo "
    ''''''         dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@DateFrom", queryParams.DateFrom))
    ''''''         dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@DateTo", queryParams.DateTo))
    ''''''     ElseIf Not String.IsNullOrEmpty(queryParams.DateFrom) Then
    ''''''         'sqlStr = sqlStr & "AND LEFT(CONVERT(VARCHAR, INVOICE_DATE, 111), 10) = @DateFrom "
    ''''''         sqlStr = sqlStr & "AND Wty_Excel.Delivery_Date = @DateFrom "
    ''''''         dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@DateFrom", queryParams.DateFrom))
    ''''''     ElseIf Not String.IsNullOrEmpty(queryParams.DateTo) Then
    ''''''         sqlStr = sqlStr & "AND Wty_Excel.Delivery_Date = @DateTo "
    ''''''         'sqlStr = sqlStr & "AND LEFT(CONVERT(VARCHAR, INVOICE_DATE, 111), 10) = @DateTo "
    ''''''         dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@DateTo", queryParams.DateTo))
    ''''''     End If
    ''''''     dt = dbConn.GetDataSet(sqlStr)
    ''''''     dbConn.CloseConnection()

    ''''''     Dim _datWarrantyCsvList As List(Of WarrantyCsvModel) = New List(Of WarrantyCsvModel)()

    ''''''     If dt IsNot Nothing AndAlso dt.Rows.Count <> 0 Then
    ''''''         For i As Integer = 0 To dt.Rows.Count - 1
    ''''''             Dim datWarrantyCsv As WarrantyCsvModel = New WarrantyCsvModel()
    ''''''             datWarrantyCsv.ASC_Claim_No = dt.Rows(i)("ASC_Claim_No").ToString()
    ''''''             datWarrantyCsv.Branch_Code = dt.Rows(i)("Branch_Code").ToString()
    ''''''             ' datWarrantyCsv.Parts_invoice_No = dt.Rows(i)("Parts_invoice_No").ToString()
    ''''''             ' datWarrantyCsv.Labor_Invoice_No = dt.Rows(i)("Labor_Invoice_No").ToString()
    ''''''             datWarrantyCsv.Samsung_Claim_No = dt.Rows(i)("Samsung_Claim_No").ToString()
    ''''''             datWarrantyCsv.Service_Type = dt.Rows(i)("Service_Type").ToString()
    ''''''             datWarrantyCsv.Consumer_Name = dt.Rows(i)("Consumer_Name").ToString()
    ''''''             datWarrantyCsv.Consumer_Addr1 = dt.Rows(i)("Consumer_Addr1").ToString()
    ''''''             datWarrantyCsv.Consumer_Addr2 = dt.Rows(i)("Consumer_Addr2").ToString()
    ''''''             datWarrantyCsv.Consumer_Telephone = dt.Rows(i)("Consumer_Telephone").ToString()
    ''''''             datWarrantyCsv.Consumer_Fax = dt.Rows(i)("Consumer_Fax").ToString()
    ''''''             datWarrantyCsv.Postal_Code = dt.Rows(i)("Postal_Code").ToString()
    ''''''             datWarrantyCsv.Model = dt.Rows(i)("Model").ToString()
    ''''''             datWarrantyCsv.Serial_No = dt.Rows(i)("Serial_No").ToString()
    ''''''             datWarrantyCsv.IMEI_No = dt.Rows(i)("IMEI_No").ToString()
    ''''''             datWarrantyCsv.Defect_Type = dt.Rows(i)("Defect_Type").ToString()
    ''''''             datWarrantyCsv.Condition = dt.Rows(i)("Condition").ToString()
    ''''''             datWarrantyCsv.Symptom = dt.Rows(i)("Symptom").ToString()
    ''''''             datWarrantyCsv.Defect_Code = dt.Rows(i)("Defect_Code").ToString()
    ''''''             datWarrantyCsv.Repair_Code = dt.Rows(i)("Repair_Code").ToString()
    ''''''             datWarrantyCsv.Defect_Desc = dt.Rows(i)("Defect_Desc").ToString()
    ''''''             datWarrantyCsv.Repair_Description = dt.Rows(i)("Repair_Description").ToString()
    ''''''             datWarrantyCsv.Purchase_Date = dt.Rows(i)("Purchase_Date").ToString()
    ''''''             datWarrantyCsv.Repair_Received_Date = dt.Rows(i)("Repair_Received_Date").ToString()
    ''''''             datWarrantyCsv.Completed_Date = dt.Rows(i)("Completed_Date").ToString()
    ''''''             datWarrantyCsv.Delivery_Date = dt.Rows(i)("Delivery_Date").ToString()

    ''''''             datWarrantyCsv.Production_Date = dt.Rows(i)("Production_Date").ToString()
    ''''''             '  datWarrantyCsv.OW_Labor = dt.Rows(i)("OW_Labor").ToString()
    ''''''             '   datWarrantyCsv.OW_Parts = dt.Rows(i)("OW_Parts").ToString()
    ''''''             datWarrantyCsv.Freight = dt.Rows(i)("Freight").ToString()
    ''''''             datWarrantyCsv.Other = dt.Rows(i)("Other").ToString()
    ''''''             datWarrantyCsv.Parts_SGST = dt.Rows(i)("Parts_SGST").ToString()
    ''''''             datWarrantyCsv.Parts_UTGST = dt.Rows(i)("Parts_UTGST").ToString()
    ''''''             datWarrantyCsv.Parts_CGST = dt.Rows(i)("Parts_CGST").ToString()
    ''''''             datWarrantyCsv.Parts_IGST = dt.Rows(i)("Parts_IGST").ToString()
    ''''''             datWarrantyCsv.Parts_Cess = dt.Rows(i)("Parts_Cess").ToString()
    ''''''             datWarrantyCsv.SGST = dt.Rows(i)("SGST").ToString()
    ''''''             datWarrantyCsv.UTGST = dt.Rows(i)("UTGST").ToString()
    ''''''             datWarrantyCsv.CGST = dt.Rows(i)("CGST").ToString()
    ''''''             datWarrantyCsv.IGST = dt.Rows(i)("IGST").ToString()
    ''''''             datWarrantyCsv.Cess = dt.Rows(i)("Cess").ToString()
    ''''''             '          datWarrantyCsv.OW_total = dt.Rows(i)("OW_total").ToString()
    ''''''             datWarrantyCsv.Remark = dt.Rows(i)("Remark").ToString()
    ''''''             datWarrantyCsv.Tr_No = dt.Rows(i)("Tr_No").ToString()
    ''''''             datWarrantyCsv.Tr_Type = dt.Rows(i)("Tr_Type").ToString()
    ''''''             datWarrantyCsv.Status = dt.Rows(i)("Status").ToString()
    ''''''             datWarrantyCsv.Engineer = dt.Rows(i)("Engineer").ToString()
    ''''''             datWarrantyCsv.Collection_Point = dt.Rows(i)("Collection_Point").ToString()
    ''''''             datWarrantyCsv.Collection_Point_Name = dt.Rows(i)("Collection_Point_Name").ToString()
    ''''''             datWarrantyCsv.Location_1 = dt.Rows(i)("Location_1").ToString()
    ''''''             datWarrantyCsv.Part_1 = dt.Rows(i)("Part_1").ToString()
    ''''''             datWarrantyCsv.Qty_1 = dt.Rows(i)("Qty_1").ToString()


    ''''''             datWarrantyCsv.Unit_Price_1 = dt.Rows(i)("Unit_Price_1").ToString()
    ''''''             datWarrantyCsv.Doc_Num_1 = dt.Rows(i)("Doc_Num_1").ToString()
    ''''''             datWarrantyCsv.Matrial_Serial_1 = dt.Rows(i)("Matrial_Serial_1").ToString()
    ''''''             datWarrantyCsv.Location_2 = dt.Rows(i)("Location_2").ToString()
    ''''''             datWarrantyCsv.Part_2 = dt.Rows(i)("Part_2").ToString()
    ''''''             datWarrantyCsv.Qty_2 = dt.Rows(i)("Qty_2").ToString()
    ''''''             datWarrantyCsv.Unit_Price_2 = dt.Rows(i)("Unit_Price_2").ToString()
    ''''''             datWarrantyCsv.Doc_Num_2 = dt.Rows(i)("Doc_Num_2").ToString()
    ''''''             datWarrantyCsv.Matrial_Serial_2 = dt.Rows(i)("Matrial_Serial_2").ToString()
    ''''''             datWarrantyCsv.Location_3 = dt.Rows(i)("Location_3").ToString()
    ''''''             datWarrantyCsv.Part_3 = dt.Rows(i)("Part_3").ToString()
    ''''''             datWarrantyCsv.Qty_3 = dt.Rows(i)("Qty_3").ToString()
    ''''''             datWarrantyCsv.Unit_Price_3 = dt.Rows(i)("Unit_Price_3").ToString()
    ''''''             datWarrantyCsv.Doc_Num_3 = dt.Rows(i)("Doc_Num_3").ToString()
    ''''''             datWarrantyCsv.Matrial_Serial_3 = dt.Rows(i)("Matrial_Serial_3").ToString()
    ''''''             datWarrantyCsv.Location_4 = dt.Rows(i)("Location_4").ToString()
    ''''''             datWarrantyCsv.Part_4 = dt.Rows(i)("Part_4").ToString()
    ''''''             datWarrantyCsv.Qty_4 = dt.Rows(i)("Qty_4").ToString()
    ''''''             datWarrantyCsv.Unit_Price_4 = dt.Rows(i)("Unit_Price_4").ToString()
    ''''''             datWarrantyCsv.Doc_Num_4 = dt.Rows(i)("Doc_Num_4").ToString()
    ''''''             datWarrantyCsv.Matrial_Serial_4 = dt.Rows(i)("Matrial_Serial_4").ToString()
    ''''''             datWarrantyCsv.Location_5 = dt.Rows(i)("Location_5").ToString()
    ''''''             datWarrantyCsv.Part_5 = dt.Rows(i)("Part_5").ToString()
    ''''''             datWarrantyCsv.Qty_5 = dt.Rows(i)("Qty_5").ToString()
    ''''''             datWarrantyCsv.Unit_Price_5 = dt.Rows(i)("Unit_Price_5").ToString()
    ''''''             datWarrantyCsv.Doc_Num_5 = dt.Rows(i)("Doc_Num_5").ToString()

    ''''''             datWarrantyCsv.Matrial_Serial_5 = dt.Rows(i)("Matrial_Serial_5").ToString()
    ''''''             datWarrantyCsv.Location_6 = dt.Rows(i)("Location_6").ToString()
    ''''''             datWarrantyCsv.Part_6 = dt.Rows(i)("Part_6").ToString()
    ''''''             datWarrantyCsv.Qty_6 = dt.Rows(i)("Qty_6").ToString()
    ''''''             datWarrantyCsv.Unit_Price_6 = dt.Rows(i)("Unit_Price_6").ToString()
    ''''''             datWarrantyCsv.Doc_Num_6 = dt.Rows(i)("Doc_Num_6").ToString()
    ''''''             datWarrantyCsv.Matrial_Serial_6 = dt.Rows(i)("Matrial_Serial_6").ToString()
    ''''''             datWarrantyCsv.Location_7 = dt.Rows(i)("Location_7").ToString()
    ''''''             datWarrantyCsv.Part_7 = dt.Rows(i)("Part_7").ToString()
    ''''''             datWarrantyCsv.Qty_7 = dt.Rows(i)("Qty_7").ToString()
    ''''''             datWarrantyCsv.Unit_Price_7 = dt.Rows(i)("Unit_Price_7").ToString()
    ''''''             datWarrantyCsv.Doc_Num_7 = dt.Rows(i)("Doc_Num_7").ToString()
    ''''''             datWarrantyCsv.Matrial_Serial_7 = dt.Rows(i)("Matrial_Serial_7").ToString()
    ''''''             datWarrantyCsv.Location_8 = dt.Rows(i)("Location_8").ToString()
    ''''''             datWarrantyCsv.Part_8 = dt.Rows(i)("Part_8").ToString()
    ''''''             datWarrantyCsv.Qty_8 = dt.Rows(i)("Qty_8").ToString()
    ''''''             datWarrantyCsv.Unit_Price_8 = dt.Rows(i)("Unit_Price_8").ToString()
    ''''''             datWarrantyCsv.Doc_Num_8 = dt.Rows(i)("Doc_Num_8").ToString()
    ''''''             datWarrantyCsv.Matrial_Serial_8 = dt.Rows(i)("Matrial_Serial_8").ToString()
    ''''''             datWarrantyCsv.Location_9 = dt.Rows(i)("Location_9").ToString()
    ''''''             datWarrantyCsv.Part_9 = dt.Rows(i)("Part_9").ToString()
    ''''''             datWarrantyCsv.Qty_9 = dt.Rows(i)("Qty_9").ToString()
    ''''''             datWarrantyCsv.Unit_Price_9 = dt.Rows(i)("Unit_Price_9").ToString()
    ''''''             datWarrantyCsv.Doc_Num_9 = dt.Rows(i)("Doc_Num_9").ToString()
    ''''''             datWarrantyCsv.Matrial_Serial_9 = dt.Rows(i)("Matrial_Serial_9").ToString()
    ''''''             datWarrantyCsv.Location_10 = dt.Rows(i)("Location_10").ToString()


    ''''''             datWarrantyCsv.Part_10 = dt.Rows(i)("Part_10").ToString()
    ''''''             datWarrantyCsv.Qty_10 = dt.Rows(i)("Qty_10").ToString()
    ''''''             datWarrantyCsv.Unit_Price_10 = dt.Rows(i)("Unit_Price_10").ToString()
    ''''''             datWarrantyCsv.Doc_Num_10 = dt.Rows(i)("Doc_Num_10").ToString()
    ''''''             datWarrantyCsv.Matrial_Serial_10 = dt.Rows(i)("Matrial_Serial_10").ToString()
    ''''''             datWarrantyCsv.Location_11 = dt.Rows(i)("Location_11").ToString()
    ''''''             datWarrantyCsv.Part_11 = dt.Rows(i)("Part_11").ToString()
    ''''''             datWarrantyCsv.Qty_11 = dt.Rows(i)("Qty_11").ToString()
    ''''''             datWarrantyCsv.Unit_Price_11 = dt.Rows(i)("Unit_Price_11").ToString()
    ''''''             datWarrantyCsv.Doc_Num_11 = dt.Rows(i)("Doc_Num_11").ToString()
    ''''''             datWarrantyCsv.Matrial_Serial_11 = dt.Rows(i)("Matrial_Serial_11").ToString()
    ''''''             datWarrantyCsv.Location_12 = dt.Rows(i)("Location_12").ToString()
    ''''''             datWarrantyCsv.Part_12 = dt.Rows(i)("Part_12").ToString()
    ''''''             datWarrantyCsv.Qty_12 = dt.Rows(i)("Qty_12").ToString()
    ''''''             datWarrantyCsv.Unit_Price_12 = dt.Rows(i)("Unit_Price_12").ToString()
    ''''''             datWarrantyCsv.Doc_Num_12 = dt.Rows(i)("Doc_Num_12").ToString()
    ''''''             datWarrantyCsv.Matrial_Serial_12 = dt.Rows(i)("Matrial_Serial_12").ToString()
    ''''''             datWarrantyCsv.Location_13 = dt.Rows(i)("Location_13").ToString()
    ''''''             datWarrantyCsv.Part_13 = dt.Rows(i)("Part_13").ToString()
    ''''''             datWarrantyCsv.Qty_13 = dt.Rows(i)("Qty_13").ToString()
    ''''''             datWarrantyCsv.Unit_Price_13 = dt.Rows(i)("Unit_Price_13").ToString()
    ''''''             datWarrantyCsv.Doc_Num_13 = dt.Rows(i)("Doc_Num_13").ToString()
    ''''''             datWarrantyCsv.Matrial_Serial_13 = dt.Rows(i)("Matrial_Serial_13").ToString()
    ''''''             datWarrantyCsv.Location_14 = dt.Rows(i)("Location_14").ToString()
    ''''''             datWarrantyCsv.Part_14 = dt.Rows(i)("Part_14").ToString()
    ''''''             datWarrantyCsv.Qty_14 = dt.Rows(i)("Qty_14").ToString()



    ''''''             datWarrantyCsv.Unit_Price_14 = dt.Rows(i)("Unit_Price_14").ToString()
    ''''''             datWarrantyCsv.Doc_Num_14 = dt.Rows(i)("Doc_Num_14").ToString()
    ''''''             datWarrantyCsv.Matrial_Serial_14 = dt.Rows(i)("Matrial_Serial_14").ToString()
    ''''''             datWarrantyCsv.Location_15 = dt.Rows(i)("Location_15").ToString()
    ''''''             datWarrantyCsv.Part_15 = dt.Rows(i)("Part_15").ToString()
    ''''''             datWarrantyCsv.Qty_15 = dt.Rows(i)("Qty_15").ToString()
    ''''''             datWarrantyCsv.Unit_Price_15 = dt.Rows(i)("Unit_Price_15").ToString()
    ''''''             datWarrantyCsv.Doc_Num_15 = dt.Rows(i)("Doc_Num_15").ToString()
    ''''''             datWarrantyCsv.Matrial_Serial_15 = dt.Rows(i)("Matrial_Serial_15").ToString()






    ''''''             _datWarrantyCsvList.Add(datWarrantyCsv)
End Class
