Imports System
Imports System.Data
Imports System.Web.Security
Imports System.Collections.Generic
Imports Ganges33.Ganges33.dao
Imports Ganges33.Ganges33.model
Imports Ganges33.Ganges33.logic

Public Class SonyRmsClaimInvoiceDetailControl
    Public Function AddModifyRmsClaimInvoiceDetail(ByVal csvData()() As String, queryParams As SonyRmsClaimInvoiceDetailModel) As Boolean
        Log4NetControl.ComInfoLogWrite(Log4NetControl.UserID)
        'Mandatory Column 24 from CSV
        Dim flag As Boolean = True
        If csvData(0).Length < 24 Then
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
        sqlStr = "SELECT TOP 1 SAPCODE FROM SONY_RMS_CLAIM_INVOICE_DETAIL "
        sqlStr = sqlStr & " WHERE DELFG = 0 AND SRC_FILE_NAME='" & queryParams.SRC_FILE_NAME & "'"
        'sqlStr = sqlStr & " AND SHIP_TO_BRANCH_CODE='" & queryParams.ShipToBranchCode & "'"
        dtTableExist = dbConn.GetDataSet(sqlStr)
        'if exist then need to update delfg=1 then insert the record as new
        If (dtTableExist Is Nothing) Or (dtTableExist.Rows.Count = 0) Then
            'isExist = 0
        Else 'The records is already exist, need to update DELFg=0
            ' isExist = 1
            sqlStr = "UPDATE SONY_RMS_CLAIM_INVOICE_DETAIL SET DELFG=1  "
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
                sqlStr = "Insert into SONY_RMS_CLAIM_INVOICE_DETAIL ("
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


                sqlStr = sqlStr & "SAPCODE, "
                sqlStr = sqlStr & "INVOICENUMBER, "
                sqlStr = sqlStr & "ORDER_NUMBER, "
                sqlStr = sqlStr & "MODEL_NAME, "
                sqlStr = sqlStr & "MODEL_DESCREPTION, "
                sqlStr = sqlStr & "IMEI_NO, "
                sqlStr = sqlStr & "HSNCODE, "
                sqlStr = sqlStr & "CLAIMCATEGORYTYPE, "
                sqlStr = sqlStr & "SERVICETYPE, "
                sqlStr = sqlStr & "SERVICECLAIMCATEGORY, "
                sqlStr = sqlStr & "BILLINGDATE, "
                sqlStr = sqlStr & "CUSTOMERINVOICENO, "
                sqlStr = sqlStr & "QUANTITY, "
                sqlStr = sqlStr & "CLAIMAMOUNT, "
                sqlStr = sqlStr & "CGST, "
                sqlStr = sqlStr & "CGSTAMOUNT, "
                sqlStr = sqlStr & "SGST, "
                sqlStr = sqlStr & "SGSTAMOUNT, "
                sqlStr = sqlStr & "IGST, "
                sqlStr = sqlStr & "IGSTAMOUNT, "
                sqlStr = sqlStr & "UTGST, "
                sqlStr = sqlStr & "UTGSTAMOUNT, "
                sqlStr = sqlStr & "CLAIMSTATUS, "
                sqlStr = sqlStr & "CLAIMCATEGORYTYPE1, "


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


                sqlStr = sqlStr & "@SAPCODE, "
                sqlStr = sqlStr & "@INVOICENUMBER, "
                sqlStr = sqlStr & "@ORDER_NUMBER, "
                sqlStr = sqlStr & "@MODEL_NAME, "
                sqlStr = sqlStr & "@MODEL_DESCREPTION, "
                sqlStr = sqlStr & "@IMEI_NO, "
                sqlStr = sqlStr & "@HSNCODE, "
                sqlStr = sqlStr & "@CLAIMCATEGORYTYPE, "
                sqlStr = sqlStr & "@SERVICETYPE, "
                sqlStr = sqlStr & "@SERVICECLAIMCATEGORY, "
                sqlStr = sqlStr & "@BILLINGDATE, "
                sqlStr = sqlStr & "@CUSTOMERINVOICENO, "
                sqlStr = sqlStr & "@QUANTITY, "
                sqlStr = sqlStr & "@CLAIMAMOUNT, "
                sqlStr = sqlStr & "@CGST, "
                sqlStr = sqlStr & "@CGSTAMOUNT, "
                sqlStr = sqlStr & "@SGST, "
                sqlStr = sqlStr & "@SGSTAMOUNT, "
                sqlStr = sqlStr & "@IGST, "
                sqlStr = sqlStr & "@IGSTAMOUNT, "
                sqlStr = sqlStr & "@UTGST, "
                sqlStr = sqlStr & "@UTGSTAMOUNT, "
                sqlStr = sqlStr & "@CLAIMSTATUS, "
                sqlStr = sqlStr & "@CLAIMCATEGORYTYPE1, "



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

                dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@SAPCODE", csvData(i)(0)))
                dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@INVOICENUMBER", csvData(i)(1)))
                dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@ORDER_NUMBER", csvData(i)(2)))
                dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@MODEL_NAME", csvData(i)(3)))
                dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@MODEL_DESCREPTION", csvData(i)(4)))
                dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@IMEI_NO", csvData(i)(5)))
                dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@HSNCODE", csvData(i)(6)))
                dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@CLAIMCATEGORYTYPE", csvData(i)(7)))
                dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@SERVICETYPE", csvData(i)(8)))
                dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@SERVICECLAIMCATEGORY", csvData(i)(9)))
                dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@BILLINGDATE", csvData(i)(10)))
                dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@CUSTOMERINVOICENO", csvData(i)(11)))
                dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@QUANTITY", csvData(i)(12)))
                dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@CLAIMAMOUNT", csvData(i)(13)))
                dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@CGST", csvData(i)(14)))
                dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@CGSTAMOUNT", csvData(i)(15)))
                dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@SGST", csvData(i)(16)))
                dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@SGSTAMOUNT", csvData(i)(17)))
                dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@IGST", csvData(i)(18)))
                dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@IGSTAMOUNT", csvData(i)(19)))
                dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@UTGST", csvData(i)(20)))
                dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@UTGSTAMOUNT", csvData(i)(21)))
                dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@CLAIMSTATUS", csvData(i)(22)))
                dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@CLAIMCATEGORYTYPE1", csvData(i)(23)))


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


    Public Function SelectRmsClaimInvoiceDetail(ByVal queryParams As SonyRmsClaimInvoiceDetailModel) As DataTable
        Log4NetControl.ComInfoLogWrite(Log4NetControl.UserID)
        Dim dbConn As DBUtility = New DBUtility()
        Dim dt As DataTable = New DataTable()
        Dim sqlStr As String = "SELECT "
        sqlStr = sqlStr & "SAPCODE as 'SAPCode'," ',SC_CODE as 'SC Code',SC_NAME as 'SC Name',VENDOR_NAME as 'Vendor Name',PO_NO as 'PO_NO',PART_NO as 'Part NO',PART_LOCATION_NAME as 'Part Location Name',PART_ENGLISH_NAME as 'Part English Name',ORDER_NUMBER as 'Order Number',QUANTITY_ON_THE_WAY as 'Quantity On The Way',MODEL_NO as 'Model No',RECEIPT_TYPE as 'Receipt Type',ORDER_DATE as 'Order Date',ORDER_STATUS as 'Order Status',ORDER_STATUS_DATE as 'Order Status Date',RECEIPT_NO as 'Receipt No',APPROVAL_NUMBER as 'Approval Number',MARK as 'Mark',ETD as 'ETD',ORDER_STATUS_2 as 'Order Status',REQUEST_BY as 'Request By',PRICE as 'Price',REQUESTTYPE as 'RequestType',RPOCREMARK as 'RpocRemark',SRPCPONO as 'SrpcPoNo',NPC_SELLING_PRICE as 'NPC_Selling_Price' "
        sqlStr = sqlStr & "INVOICENUMBER as 'InvoiceNumber',"
        sqlStr = sqlStr & "ORDER_NUMBER as 'order_number',"
        sqlStr = sqlStr & "MODEL_NAME as 'model_name',"
        sqlStr = sqlStr & "MODEL_DESCREPTION as 'model_descreption',"
        sqlStr = sqlStr & "IMEI_NO as 'imei_no',"
        sqlStr = sqlStr & "HSNCODE as 'HSNCode',"
        sqlStr = sqlStr & "CLAIMCATEGORYTYPE as 'ClaimCategoryType',"
        sqlStr = sqlStr & "SERVICETYPE as 'ServiceType',"
        sqlStr = sqlStr & "SERVICECLAIMCATEGORY as 'ServiceClaimCategory',"
        sqlStr = sqlStr & "BILLINGDATE as 'BillingDate',"
        sqlStr = sqlStr & "CUSTOMERINVOICENO as 'CustomerInvoiceNo',"
        sqlStr = sqlStr & "QUANTITY as 'quantity',"
        sqlStr = sqlStr & "CLAIMAMOUNT as 'ClaimAmount',"
        sqlStr = sqlStr & "CGST as 'CGST',"
        sqlStr = sqlStr & "CGSTAMOUNT as 'CGSTAmount',"
        sqlStr = sqlStr & "SGST as 'SGST',"
        sqlStr = sqlStr & "SGSTAMOUNT as 'SGSTAmount',"
        sqlStr = sqlStr & "IGST as 'IGST',"
        sqlStr = sqlStr & "IGSTAMOUNT as 'IGSTAmount',"
        sqlStr = sqlStr & "UTGST as 'UTGST',"
        sqlStr = sqlStr & "UTGSTAMOUNT as 'UTGSTAmount',"
        sqlStr = sqlStr & "CLAIMSTATUS as 'CLAIMSTATUS',"
        sqlStr = sqlStr & "CLAIMCATEGORYTYPE1 as 'ClaimCategoryType1' "




        sqlStr = sqlStr & "FROM "
        sqlStr = sqlStr & "SONY_RMS_CLAIM_INVOICE_DETAIL "
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
