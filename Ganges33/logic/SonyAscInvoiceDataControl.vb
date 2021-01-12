Imports System
Imports System.Data
Imports System.Web.Security
Imports System.Collections.Generic
Imports Ganges33.Ganges33.dao
Imports Ganges33.Ganges33.model
Imports Ganges33.Ganges33.logic


Public Class SonyAscInvoiceDataControl
    Public Function AddModifyAscInvoiceData(ByVal csvData()() As String, queryParams As SonyAscInvoiceDataModel) As Boolean
        Log4NetControl.ComInfoLogWrite(Log4NetControl.UserID)
        'Mandatory Column 39 from CSV
        Dim flag As Boolean = True
        If csvData(0).Length < 5 Then
            Return False
        End If

        '       Log4NetControl.ComInfoLogWrite(Log4NetControl.UserID)
        Dim DateTimeNow As DateTime = DateTime.Now
        Dim dtNow As DateTime = DateTimeNow.AddMinutes(ConfigurationManager.AppSettings("TimeDiff"))
        Dim dbConn As DBUtility = New DBUtility()
        Dim dt As DataTable = New DataTable()
        dbConn.sqlTrn = dbConn.sqlConn.BeginTransaction()
        dbConn.sqlCmd.Transaction = dbConn.sqlTrn
        '       Dim flag As Boolean = True
        Dim flagAll As Boolean = True
        Dim sqlStr As String = ""
        Dim dtTableExist As DataTable

        Dim isExist As Integer = 0
        '1st check PARTS_NO
        sqlStr = "SELECT TOP 1 SAP_CODE FROM SONY_ASC_INVOICE_DATA "
        sqlStr = sqlStr & " WHERE DELFG = 0 AND SRC_FILE_NAME='" & queryParams.SRC_FILE_NAME & "'"
        'sqlStr = sqlStr & " AND SHIP_TO_BRANCH_CODE='" & queryParams.ShipToBranchCode & "'"
        dtTableExist = dbConn.GetDataSet(sqlStr)
        'if exist then need to update delfg=1 then insert the record as new
        If (dtTableExist Is Nothing) Or (dtTableExist.Rows.Count = 0) Then
            'isExist = 0
        Else 'The records is already exist, need to update DELFg=0
            ' isExist = 1
            sqlStr = "UPDATE SONY_ASC_INVOICE_DATA SET DELFG=1  "
            sqlStr = sqlStr & "WHERE DELFG=0 "
            sqlStr = sqlStr & "AND SRC_FILE_NAME = @SRC_FILE_NAME"
            'sqlStr = sqlStr & "AND SHIP_TO_BRANCH_CODE = @SHIP_TO_BRANCH_CODE "
            dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@SRC_FILE_NAME", queryParams.SRC_FILE_NAME))
            flag = dbConn.ExecSQL(sqlStr)
            dbConn.sqlCmd.Parameters.Clear()
            'If Error occurs then will store the flag as false
            If Not flag Then
                flagAll = False
            End If
        End If
        For i = 0 To csvData.Length - 1
            If i > 0 Then '0  Header
                'If isExist = 1 Then
                sqlStr = "Insert into SONY_ASC_INVOICE_DATA ("
                sqlStr = sqlStr & "CRTDT, "
                sqlStr = sqlStr & "CRTCD, "
                ' sqlStr = sqlStr & "UPDDT, "
                sqlStr = sqlStr & "UPDCD, "
                sqlStr = sqlStr & "UPDPG, "
                sqlStr = sqlStr & "DELFG, "
                '             sqlStr = sqlStr & "UNQ_NO, "
                sqlStr = sqlStr & "UPLOAD_USER, "
                sqlStr = sqlStr & "UPLOAD_DATE, "
                sqlStr = sqlStr & "SHIP_TO_BRANCH_CODE, "
                sqlStr = sqlStr & "SHIP_TO_BRANCH, "


                sqlStr = sqlStr & "SAP_CODE, "
                sqlStr = sqlStr & "DEBIT_CUM_TAX_INVOICE_NO, "
                sqlStr = sqlStr & "JOB_NO, "
                sqlStr = sqlStr & "MATERIAL_CODE, "
                sqlStr = sqlStr & "MATERIAL_DESC, "
                sqlStr = sqlStr & "SN, "
                sqlStr = sqlStr & "PART_NO, "
                sqlStr = sqlStr & "PART_DESC, "
                sqlStr = sqlStr & "HSN_CODE_SAC_CODE, "
                sqlStr = sqlStr & "INVOICE_TYPE, "
                sqlStr = sqlStr & "WARRANTY_TYPE, "
                sqlStr = sqlStr & "SERVICE_TYPE, "
                sqlStr = sqlStr & "SERVICE_CLAIM_CATEGORY, "
                sqlStr = sqlStr & "BILLING_DATE, "
                sqlStr = sqlStr & "CUSTOMER_INV_NO, "
                sqlStr = sqlStr & "QTY, "
                sqlStr = sqlStr & "PART_COST_PER_UNIT, "
                sqlStr = sqlStr & "TOTAL_PARTS_COST, "
                sqlStr = sqlStr & "CLAIM_AMOUNT, "
                sqlStr = sqlStr & "CGST_PERCENTAGE, "
                sqlStr = sqlStr & "CGST, "
                sqlStr = sqlStr & "SGST_PERCENTAGE, "
                sqlStr = sqlStr & "SGST, "
                sqlStr = sqlStr & "IGST_PERCENTAGE, "
                sqlStr = sqlStr & "IGST, "
                sqlStr = sqlStr & "UGST_PERCENTAGE, "
                sqlStr = sqlStr & "UGST, "


                sqlStr = sqlStr & "FILE_NAME, "
                sqlStr = sqlStr & "SRC_FILE_NAME "
                sqlStr = sqlStr & " ) "
                sqlStr = sqlStr & " values ( "
                sqlStr = sqlStr & "@CRTDT, "
                sqlStr = sqlStr & "@CRTCD, "
                'sqlStr = sqlStr & "@UPDDT, "
                sqlStr = sqlStr & "@UPDCD, "
                sqlStr = sqlStr & "@UPDPG, "
                sqlStr = sqlStr & "@DELFG, "
                '              sqlStr = sqlStr & " (select max(UNQ_NO)+1 from SAW_DISCOUNT) , "
                sqlStr = sqlStr & "@UPLOAD_USER, "
                sqlStr = sqlStr & "@UPLOAD_DATE, "
                sqlStr = sqlStr & "@SHIP_TO_BRANCH_CODE, "
                sqlStr = sqlStr & "@SHIP_TO_BRANCH, "


                sqlStr = sqlStr & "@SAP_CODE, "
                sqlStr = sqlStr & "@DEBIT_CUM_TAX_INVOICE_NO, "
                sqlStr = sqlStr & "@JOB_NO, "
                sqlStr = sqlStr & "@MATERIAL_CODE, "
                sqlStr = sqlStr & "@MATERIAL_DESC, "
                sqlStr = sqlStr & "@SN, "
                sqlStr = sqlStr & "@PART_NO, "
                sqlStr = sqlStr & "@PART_DESC, "
                sqlStr = sqlStr & "@HSN_CODE_SAC_CODE, "
                sqlStr = sqlStr & "@INVOICE_TYPE, "
                sqlStr = sqlStr & "@WARRANTY_TYPE, "
                sqlStr = sqlStr & "@SERVICE_TYPE, "
                sqlStr = sqlStr & "@SERVICE_CLAIM_CATEGORY, "
                sqlStr = sqlStr & "@BILLING_DATE, "
                sqlStr = sqlStr & "@CUSTOMER_INV_NO, "
                sqlStr = sqlStr & "@QTY, "
                sqlStr = sqlStr & "@PART_COST_PER_UNIT, "
                sqlStr = sqlStr & "@TOTAL_PARTS_COST, "
                sqlStr = sqlStr & "@CLAIM_AMOUNT, "
                sqlStr = sqlStr & "@CGST_PERCENTAGE, "
                sqlStr = sqlStr & "@CGST, "
                sqlStr = sqlStr & "@SGST_PERCENTAGE, "
                sqlStr = sqlStr & "@SGST, "
                sqlStr = sqlStr & "@IGST_PERCENTAGE, "
                sqlStr = sqlStr & "@IGST, "
                sqlStr = sqlStr & "@UGST_PERCENTAGE, "
                sqlStr = sqlStr & "@UGST, "



                sqlStr = sqlStr & "@FILE_NAME, "
                sqlStr = sqlStr & "@SRC_FILE_NAME "
                sqlStr = sqlStr & " )"
                dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@CRTDT", dtNow))
                dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@CRTCD", queryParams.UserId))
                dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@UPDCD", "")) '?????????????????????????
                dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@UPDPG", queryParams.UPDPG))
                dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@DELFG", 0))
                dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@UPLOAD_USER", queryParams.UPLOAD_USER))
                dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@UPLOAD_DATE", dtNow))
                dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@SHIP_TO_BRANCH_CODE", queryParams.SHIP_TO_BRANCH_CODE))
                dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@SHIP_TO_BRANCH", queryParams.SHIP_TO_BRANCH))

                dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@SAP_CODE", csvData(i)(0)))
                dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@DEBIT_CUM_TAX_INVOICE_NO", csvData(i)(1)))
                dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@JOB_NO", csvData(i)(2)))
                dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@MATERIAL_CODE", csvData(i)(3)))
                dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@MATERIAL_DESC", csvData(i)(4)))
                dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@SN", csvData(i)(5)))
                dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@PART_NO", csvData(i)(6)))
                dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@PART_DESC", csvData(i)(7)))
                dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@HSN_CODE_SAC_CODE", csvData(i)(8)))
                dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@INVOICE_TYPE", csvData(i)(9)))
                dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@WARRANTY_TYPE", csvData(i)(10)))
                dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@SERVICE_TYPE", csvData(i)(11)))
                dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@SERVICE_CLAIM_CATEGORY", csvData(i)(12)))
                dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@BILLING_DATE", csvData(i)(13)))
                dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@CUSTOMER_INV_NO", csvData(i)(14)))
                dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@QTY", csvData(i)(15)))
                dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@PART_COST_PER_UNIT", csvData(i)(16)))
                dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@TOTAL_PARTS_COST", csvData(i)(17)))
                dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@CLAIM_AMOUNT", csvData(i)(18)))
                dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@CGST_PERCENTAGE", csvData(i)(19)))
                dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@CGST", csvData(i)(20)))
                dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@SGST_PERCENTAGE", csvData(i)(21)))
                dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@SGST", csvData(i)(22)))
                dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@IGST_PERCENTAGE", csvData(i)(23)))
                dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@IGST", csvData(i)(24)))
                dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@UGST_PERCENTAGE", csvData(i)(25)))
                dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@UGST", csvData(i)(26)))


                dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@FILE_NAME", queryParams.FILE_NAME))
                dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@SRC_FILE_NAME", queryParams.SRC_FILE_NAME))

                flag = dbConn.ExecSQL(sqlStr)
                dbConn.sqlCmd.Parameters.Clear()
                'If Error occurs then will store the flag as false
                If Not flag Then
                    flagAll = False
                    Exit For
                End If
                'End If
            End If 'Other than header - End
        Next
        If flagAll Then
            flag = True
            dbConn.sqlTrn.Commit()
        Else
            flag = False
            dbConn.sqlTrn.Rollback()
        End If
        dbConn.CloseConnection()
        Return flag
    End Function


    Public Function SelectAscInvoiceData(ByVal queryParams As SonyAscInvoiceDataModel) As DataTable
        Log4NetControl.ComInfoLogWrite(Log4NetControl.UserID)
        Dim dbConn As DBUtility = New DBUtility()
        Dim dt As DataTable = New DataTable()
        Dim sqlStr As String = "SELECT "
        sqlStr = sqlStr & "SAP_CODE as 'SAP Code'," 'SC_CODE as 'SC Code',SC_NAME as 'SC Name',VENDOR_NAME as 'Vendor Name',PO_NO as 'PO_NO',PART_NO as 'Part NO',PART_LOCATION_NAME as 'Part Location Name',PART_ENGLISH_NAME as 'Part English Name',ORDER_NUMBER as 'Order Number',QUANTITY_ON_THE_WAY as 'Quantity On The Way',MODEL_NO as 'Model No',RECEIPT_TYPE as 'Receipt Type',ORDER_DATE as 'Order Date',ORDER_STATUS as 'Order Status',ORDER_STATUS_DATE as 'Order Status Date',RECEIPT_NO as 'Receipt No',APPROVAL_NUMBER as 'Approval Number',MARK as 'Mark',ETD as 'ETD',ORDER_STATUS_2 as 'Order Status',REQUEST_BY as 'Request By',PRICE as 'Price',REQUESTTYPE as 'RequestType',RPOCREMARK as 'RpocRemark',SRPCPONO as 'SrpcPoNo',NPC_SELLING_PRICE as 'NPC_Selling_Price' "
        sqlStr = sqlStr & "DEBIT_CUM_TAX_INVOICE_NO as 'Debit Cum Tax Invoice No',"
        sqlStr = sqlStr & "JOB_NO as 'Job No',"
        sqlStr = sqlStr & "MATERIAL_CODE as 'Material Code',"
        sqlStr = sqlStr & "MATERIAL_DESC as 'Material Desc',"
        sqlStr = sqlStr & "SN as 'S/N',"
        sqlStr = sqlStr & "PART_NO as 'Part No',"
        sqlStr = sqlStr & "PART_DESC as 'Part Desc',"
        sqlStr = sqlStr & "HSN_CODE_SAC_CODE as 'HSN Code/SAC Code',"
        sqlStr = sqlStr & "INVOICE_TYPE as 'Invoice Type',"
        sqlStr = sqlStr & "WARRANTY_TYPE as 'Warranty Type',"
        sqlStr = sqlStr & "SERVICE_TYPE as 'Service Type',"
        sqlStr = sqlStr & "SERVICE_CLAIM_CATEGORY as 'Service Claim Category',"
        sqlStr = sqlStr & "BILLING_DATE as 'Billing Date',"
        sqlStr = sqlStr & "CUSTOMER_INV_NO as 'Customer Inv No',"
        sqlStr = sqlStr & "QTY as 'Qty',"
        sqlStr = sqlStr & "PART_COST_PER_UNIT as 'Part Cost per unit',"
        sqlStr = sqlStr & "TOTAL_PARTS_COST as 'Total parts cost',"
        sqlStr = sqlStr & "CLAIM_AMOUNT as 'Claim Amount',"
        sqlStr = sqlStr & "CGST_PERCENTAGE as 'CGST%',"
        sqlStr = sqlStr & "CGST as 'CGST',"
        sqlStr = sqlStr & "SGST_PERCENTAGE as 'SGST%',"
        sqlStr = sqlStr & "SGST as 'SGST',"
        sqlStr = sqlStr & "IGST_PERCENTAGE as 'IGST%',"
        sqlStr = sqlStr & "IGST as 'IGST',"
        sqlStr = sqlStr & "UGST_PERCENTAGE as 'UGST%',"
        sqlStr = sqlStr & "UGST as 'UGST' "

        sqlStr = sqlStr & "FROM "
        sqlStr = sqlStr & "SONY_ASC_INVOICE_DATA "
        sqlStr = sqlStr & "WHERE "
        sqlStr = sqlStr & "DELFG=0 "

        If Not String.IsNullOrEmpty(queryParams.SHIP_TO_BRANCH_CODE) Then
            sqlStr = sqlStr & "AND SHIP_TO_BRANCH_CODE = @SHIP_TO_BRANCH_CODE "
            dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@SHIP_TO_BRANCH_CODE", queryParams.SHIP_TO_BRANCH_CODE))
        End If

        If Not String.IsNullOrEmpty(queryParams.SHIP_TO_BRANCH) Then
            sqlStr = sqlStr & "AND SHIP_TO_BRANCH = @SHIP_TO_BRANCH "
            dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@SHIP_TO_BRANCH", queryParams.SHIP_TO_BRANCH))
        End If

        If (Not (String.IsNullOrEmpty(queryParams.DateFrom)) And (Not (String.IsNullOrEmpty(queryParams.DateTo)))) Then
            sqlStr = sqlStr & "AND LEFT(CONVERT(VARCHAR, CRTDT, 111), 10) >= @DateFrom and LEFT(CONVERT(VARCHAR, CRTDT, 111), 10) <= @DateTo "
            'sqlStr = sqlStr & "AND INVOICE_DATE >= @DateFrom and INVOICE_DATE <= @DateTo "
            dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@DateFrom", queryParams.DateFrom))
            dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@DateTo", queryParams.DateTo))
        ElseIf Not String.IsNullOrEmpty(queryParams.DateFrom) Then
            'sqlStr = sqlStr & "AND LEFT(CONVERT(VARCHAR, INVOICE_DATE, 111), 10) = @DateFrom "
            sqlStr = sqlStr & "AND CRTDT = @DateFrom "
            dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@DateFrom", queryParams.DateFrom))
        ElseIf Not String.IsNullOrEmpty(queryParams.DateTo) Then
            sqlStr = sqlStr & "AND CRTDT = @DateTo "
            'sqlStr = sqlStr & "AND LEFT(CONVERT(VARCHAR, INVOICE_DATE, 111), 10) = @DateTo "
            dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@DateTo", queryParams.DateTo))
        End If

        Dim _DataTable As DataTable = dbConn.GetDataSet(sqlStr)
        dbConn.CloseConnection()
        Return _DataTable
    End Function
End Class
