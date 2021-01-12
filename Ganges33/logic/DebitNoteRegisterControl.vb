'VJ 2019/10/11
Imports System
Imports System.Data
Imports System.Web.Security
Imports System.Collections.Generic
Imports System.Globalization
Imports Ganges33.Ganges33.dao
Imports Ganges33.Ganges33.model
Imports Ganges33.Ganges33.logic
Public Class DebitNoteRegisterControl
    Public Function AddModifyDebitNoteRegister(ByVal csvData()() As String, queryParams As DebitNoteRegisterModel) As Boolean
        'Table Name : DEBITNOTE_REGISTER
        'Row 0 - Header 1
        'Index  Csv Field                           Table field
        '0      unq no                              MONTH_UNQ_NO
        '1      Debitnote Date                      DEBIT_NOTE_DATE
        '2      Billing Date                        BILLING_DATE
        '3      RefInvoice Date                     REFINVOICE_DATE
        '4      Part Code                           PART_CODE
        '5      DebitNote/Purchase Return No        PURCHASE_R_NO
        '6      Voucher Ref                         VOUCHER_REF
        '7      Quantity                            QUANTITY
        '8      Rate                                RATE


        Log4NetControl.ComInfoLogWrite(Log4NetControl.UserID)
        'Mandatory Column 14 from CSV
        Dim flag As Boolean = True
        If csvData(0).Length < 16 Then
            Return False
        End If

        Dim DateTimeNow As DateTime = DateTime.Now
        Dim dtNow As DateTime = DateTimeNow.AddMinutes(ConfigurationManager.AppSettings("TimeDiff"))
        Dim dbConn As DBUtility = New DBUtility()
        Dim dt As DataTable = New DataTable()
        dbConn.sqlTrn = dbConn.sqlConn.BeginTransaction()
        dbConn.sqlCmd.Transaction = dbConn.sqlTrn

        Dim flagAll As Boolean = True
        Dim sqlStr As String = ""
        Dim dtReturnCreditExist As DataTable

        Dim isExist As Integer = 0
        '1st check INVOICE_NO,LOCAL_INVOICE_NO, DELIVERY_NO exist in the table 
        sqlStr = "SELECT TOP 1 MONTH_UNQ_NO FROM DEBITNOTE_REGISTER "
        sqlStr = sqlStr & " WHERE DELFG = 0 AND SRC_FILE_NAME='" & queryParams.SrcFileName & "'"
        'sqlStr = sqlStr & " AND SHIP_TO_BRANCH_CODE='" & queryParams.ShipToBranchCode & "'"
        dtReturnCreditExist = dbConn.GetDataSet(sqlStr)
        'if exist then need to update delfg=1 then insert the record as new
        If (dtReturnCreditExist Is Nothing) Or (dtReturnCreditExist.Rows.Count = 0) Then
            'isExist = 0
        Else 'The records is already exist, need to update DELFg=0
            ' isExist = 1
            sqlStr = "UPDATE DEBITNOTE_REGISTER SET DELFG=1 "
            sqlStr = sqlStr & " WHERE DELFG=0 "
            sqlStr = sqlStr & "AND SRC_FILE_NAME = @SRC_FILE_NAME"
            'sqlStr = sqlStr & "AND SHIP_TO_BRANCH_CODE = @SHIP_TO_BRANCH_CODE "
            'dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@UPDCD", queryParams.UploadUser))
            'dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@UPDDT", dtNow))
            dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@SRC_FILE_NAME", queryParams.SrcFileName))
            flag = dbConn.ExecSQL(sqlStr)
            dbConn.sqlCmd.Parameters.Clear()
            'If Error occurs then will store the flag as false
            If Not flag Then
                flagAll = False
            End If
        End If
        'If there is no error
        If flagAll Then
            For i = 0 To csvData.Length - 1
                If i > 1 Then '0-9  Header

                    'If isExist = 1 Then
                    sqlStr = "Insert into DEBITNOTE_REGISTER ("
                    sqlStr = sqlStr & "CRTDT, "
                    sqlStr = sqlStr & "CRTCD, "
                    sqlStr = sqlStr & "UPDCD, "
                    sqlStr = sqlStr & "UPDPG, "
                    sqlStr = sqlStr & "DELFG, "
                    sqlStr = sqlStr & "UPLOAD_USER, "
                    sqlStr = sqlStr & "UPLOAD_DATE, "
                    sqlStr = sqlStr & "SHIP_TO_BRANCH_CODE, "
                    sqlStr = sqlStr & "SHIP_TO_BRANCH, "
                    sqlStr = sqlStr & "MONTH_UNQ_NO, "
                    sqlStr = sqlStr & "REQUEST_DATE, "
                    sqlStr = sqlStr & "DEBIT_NOTE_DATE, "
                    sqlStr = sqlStr & "BILLING_DATE, "
                    sqlStr = sqlStr & "REFINVOICE_DATE, "
                    sqlStr = sqlStr & "ORIGINAL_INVOICE_REF, "
                    sqlStr = sqlStr & "PART_CODE, "
                    sqlStr = sqlStr & "PARTICULARS, "
                    sqlStr = sqlStr & "PURCHASE_R_NO, "
                    sqlStr = sqlStr & "VOUCHER_REF, "
                    sqlStr = sqlStr & "GSTIN_UIN, "
                    sqlStr = sqlStr & "QUANTITY, "
                    sqlStr = sqlStr & "RATE, "
                    sqlStr = sqlStr & "GROSS_TOTAL, "
                    sqlStr = sqlStr & "SSC_PURCHASE, "
                    sqlStr = sqlStr & "SGST_RECEIVE_VALUE, "
                    sqlStr = sqlStr & "CGST_RECEIVE_VALUE, "
                    sqlStr = sqlStr & "FILE_NAME, "
                    sqlStr = sqlStr & "SRC_FILE_NAME "
                    sqlStr = sqlStr & " ) "
                    sqlStr = sqlStr & " values ( "
                    sqlStr = sqlStr & "@CRTDT, "
                    sqlStr = sqlStr & "@CRTCD, "
                    sqlStr = sqlStr & "@UPDCD, "
                    sqlStr = sqlStr & "@UPDPG, "
                    sqlStr = sqlStr & "@DELFG, "
                    sqlStr = sqlStr & "@UPLOAD_USER, "
                    sqlStr = sqlStr & "@UPLOAD_DATE, "
                    sqlStr = sqlStr & "@SHIP_TO_BRANCH_CODE, "
                    sqlStr = sqlStr & "@SHIP_TO_BRANCH, "
                    sqlStr = sqlStr & "@MONTH_UNQ_NO, "
                    sqlStr = sqlStr & "@REQUEST_DATE, "
                    sqlStr = sqlStr & "@DEBIT_NOTE_DATE, "
                    sqlStr = sqlStr & "@BILLING_DATE, "
                    sqlStr = sqlStr & "@REFINVOICE_DATE, "
                    sqlStr = sqlStr & "@ORIGINAL_INVOICE_REF, "
                    sqlStr = sqlStr & "@PART_CODE, "
                    sqlStr = sqlStr & "@PARTICULARS, "
                    sqlStr = sqlStr & "@PURCHASE_R_NO, "
                    sqlStr = sqlStr & "@VOUCHER_REF, "
                    sqlStr = sqlStr & "@GSTIN_UIN, "
                    sqlStr = sqlStr & "@QUANTITY, "
                    sqlStr = sqlStr & "@RATE, "
                    sqlStr = sqlStr & "@GROSS_TOTAL, "
                    sqlStr = sqlStr & "@SSC_PURCHASE, "
                    sqlStr = sqlStr & "@SGST_RECEIVE_VALUE, "
                    sqlStr = sqlStr & "@CGST_RECEIVE_VALUE, "
                    sqlStr = sqlStr & "@FILE_NAME, "
                    sqlStr = sqlStr & "@SRC_FILE_NAME "
                    sqlStr = sqlStr & " )"
                    dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@CRTDT", dtNow))
                    dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@CRTCD", queryParams.UserId))
                    dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@UPDCD", "")) '?????????????????????????
                    dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@UPDPG", queryParams.UPDPG))
                    dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@DELFG", 0))
                    dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@UPLOAD_USER", queryParams.UploadUser))
                    dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@UPLOAD_DATE", dtNow))
                    dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@SHIP_TO_BRANCH_CODE", queryParams.ShipToBranchCode))
                    dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@SHIP_TO_BRANCH", queryParams.ShipToBranch))
                    dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@MONTH_UNQ_NO", csvData(i)(0)))
                    dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@REQUEST_DATE", csvData(i)(1)))
                    'dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@DEBIT_NOTE_DATE", csvData(i)(2).Substring(6, 4) + "" + csvData(i)(2).Substring(3, 2) + "" + csvData(i)(2).Substring(0, 2)))
                    dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@DEBIT_NOTE_DATE", csvData(i)(2)))
                    dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@BILLING_DATE", csvData(i)(3)))
                    dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@REFINVOICE_DATE", csvData(i)(4)))
                    'dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@REFINVOICE_DATE", csvData(i)(4).Substring(6, 4) + "" + csvData(i)(4).Substring(3, 2) + "" + csvData(i)(4).Substring(0, 2)))
                    dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@ORIGINAL_INVOICE_REF", csvData(i)(5)))
                    dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@PART_CODE", csvData(i)(6)))
                    dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@PARTICULARS", csvData(i)(7)))
                    dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@PURCHASE_R_NO", csvData(i)(8)))
                    dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@VOUCHER_REF", csvData(i)(9)))
                    dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@GSTIN_UIN", csvData(i)(10)))
                    dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@QUANTITY", csvData(i)(11)))
                    dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@RATE", csvData(i)(12)))
                    dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@GROSS_TOTAL", csvData(i)(13)))
                    dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@SSC_PURCHASE", csvData(i)(14)))
                    dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@SGST_RECEIVE_VALUE", csvData(i)(15)))
                    dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@CGST_RECEIVE_VALUE", csvData(i)(16)))
                    dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@FILE_NAME", queryParams.FileName))
                    dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@SRC_FILE_NAME", queryParams.SrcFileName))
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
        End If

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


    Public Function SelectDebitNoteRegister(ByVal queryParams As DebitNoteRegisterModel) As DataTable
        Log4NetControl.ComInfoLogWrite(Log4NetControl.UserID)
        Dim PositionStart As Integer
        Dim strBranchName As String
        Dim GSTIN As String
        GSTIN = String.Empty

        PositionStart = InStr(1, queryParams.SrcFileName, "_")
        strBranchName = Left(queryParams.SrcFileName, PositionStart - 1)
        GSTIN = ConfigurationManager.AppSettings("GSTIN" & strBranchName)

        Dim dbConn As DBUtility = New DBUtility()
        Dim dt As DataTable = New DataTable()
        Dim sqlStr As String = "SELECT "
        'sqlStr = sqlStr & "DELFG,UNQ_NO,UPLOAD_USER,UPLOAD_DATE,SHIP_TO_BRANCH_CODE,SHIP_TO_BRANCH,INVOICE_DATE,INVOICE_NO,LOCAL_INVOICE_NO,DELIVERY_NO,ITEMS,AMOUNT,SGST_UTGST,CGST,IGST,CESS,TAX,TOTAL,GR_STATUS,FILE_NAME"
        sqlStr = sqlStr & "DN.MONTH_UNQ_NO 'Unq No',REPLACE(LEFT(CONVERT(VARCHAR,  SP.REQUEST_DATE, 101), 10),'/','.') 'Request Date',"
        sqlStr = sqlStr & "REPLACE(LEFT(CONVERT(VARCHAR,  DN.DEBIT_NOTE_DATE, 101), 10),'/','.') 'DebitNote Date',"
        sqlStr = sqlStr & "REPLACE(LEFT(CONVERT(VARCHAR,  DN.BILLING_DATE, 101), 10),'/','.') 'Billing Date',"
        sqlStr = sqlStr & "REPLACE(LEFT(CONVERT(VARCHAR,  DN.REFINVOICE_DATE, 101), 10),'/','.') 'RefInvoice Date',"
        sqlStr = sqlStr & "SP.BILLING_NO 'Original Invoice Ref',DN.PART_CODE 'Part Code',SP.TITLE 'Particulars (Description)',"
        sqlStr = sqlStr & "SP.RETURN_SO 'Debit Note / Purchase Return No',SP.CREDIT_REQ_NO 'Voucher Ref','" & GSTIN & "' 'GSTIN/UIN',"
        sqlStr = sqlStr & "DN.QUANTITY 'Quantity',DN.RATE 'Rate',(DN.QUANTITY*DN.RATE) + Round((DN.QUANTITY*DN.RATE * 9)/100,2) + Round((DN.QUANTITY*DN.RATE * 9)/100,2) 'Gross Total',"
        sqlStr = sqlStr & "(DN.QUANTITY*DN.RATE) 'SSC X Purchase',Round(((DN.QUANTITY*DN.RATE) *9)/100,2) 'SGST Receivable 9%',"
        sqlStr = sqlStr & "Round(((DN.QUANTITY*DN.RATE) *9)/100,2) 'CGST Receivable 9%'"
        sqlStr = sqlStr & " From DEBITNOTE_REGISTER DN"
        sqlStr = sqlStr & " JOIN SP_RETURN SP ON CONVERT(decimal(18,0), SP.RETURN_SO) = CONVERT(decimal(18,0), DN.PURCHASE_R_NO) "
        sqlStr = sqlStr & " AND CONVERT(decimal(18,0), SP.CREDIT_REQ_NO) = CONVERT(decimal(18,0), DN.VOUCHER_REF)"
        sqlStr = sqlStr & " where DN.DELFG = 0 AND SP.DELFG=0"
        If Not String.IsNullOrEmpty(queryParams.SrcFileName) Then
            sqlStr = sqlStr & " AND DN.SRC_FILE_NAME = @SRC_FILE_NAME ORDER BY MONTH_UNQ_NO"
            dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@SRC_FILE_NAME", queryParams.SrcFileName))
        End If
        Dim _DataTable As DataTable = dbConn.GetDataSet(sqlStr)
        dbConn.CloseConnection()
        Return _DataTable
    End Function
    Public Function SelectDebitNoteRegisterImportData(ByVal queryParams As DebitNoteRegisterModel) As DataTable
        Log4NetControl.ComInfoLogWrite(Log4NetControl.UserID)
        Dim dbConn As DBUtility = New DBUtility()
        Dim dt As DataTable = New DataTable()
        Dim sqlStr As String = "SELECT "
        'sqlStr = sqlStr & "DELFG,UNQ_NO,UPLOAD_USER,UPLOAD_DATE,SHIP_TO_BRANCH_CODE,SHIP_TO_BRANCH,INVOICE_DATE,INVOICE_NO,LOCAL_INVOICE_NO,DELIVERY_NO,ITEMS,AMOUNT,SGST_UTGST,CGST,IGST,CESS,TAX,TOTAL,GR_STATUS,FILE_NAME"
        sqlStr = sqlStr & "MONTH_UNQ_NO 'Unq No',"
        sqlStr = sqlStr & "Replace(Replace(convert(varchar(10), ISNULL(REQUEST_DATE,''), 101),'/','.'),'01.01.1900','') 'Request Date',"
        sqlStr = sqlStr & "Replace(Replace(convert(varchar(10), ISNULL(DEBIT_NOTE_DATE,''), 101),'/','.'),'01.01.1900','') 'Debitnote Date',"
        sqlStr = sqlStr & "Replace(Replace(convert(varchar(10), ISNULL(BILLING_DATE,''), 101),'/','.'),'01.01.1900','') 'Billing Date',"
        sqlStr = sqlStr & "Replace(Replace(convert(varchar(10), ISNULL(REFINVOICE_DATE,''), 101),'/','.'),'01.01.1900','') 'RefInvoice Date',"
        sqlStr = sqlStr & "ORIGINAL_INVOICE_REF 'Original Invoice Ref',"
        sqlStr = sqlStr & "PART_CODE 'Part Code',PARTICULARS 'Particulars(Description)',PURCHASE_R_NO 'DebitNote/Purchase Return No',"
        sqlStr = sqlStr & "VOUCHER_REF 'Voucher Ref',GSTIN_UIN 'GSTIN/UIN',QUANTITY 'Quantity',RATE 'Rate',"
        sqlStr = sqlStr & "GROSS_TOTAL 'Gross Total',SSC_PURCHASE 'SSC xPurchase',SGST_RECEIVE_VALUE 'SGSTReceivavle9%',CGST_RECEIVE_VALUE 'CGSTRecievable9%'"
        sqlStr = sqlStr & " From DEBITNOTE_REGISTER"
        sqlStr = sqlStr & " where DELFG = 0"
        If Not String.IsNullOrEmpty(queryParams.SrcFileName) Then
            sqlStr = sqlStr & " AND SRC_FILE_NAME = @SRC_FILE_NAME ORDER BY MONTH_UNQ_NO"
            dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@SRC_FILE_NAME", queryParams.SrcFileName))
        End If
        Dim _DataTable As DataTable = dbConn.GetDataSet(sqlStr)
        dbConn.CloseConnection()
        Return _DataTable
    End Function

End Class
