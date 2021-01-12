
Imports System
Imports System.Data
Imports System.Web.Security
Imports System.Collections.Generic
Imports Ganges33.Ganges33.dao
Imports Ganges33.Ganges33.model
Imports Ganges33.Ganges33.logic
Public Class ReturnCreditControl
    Public Function AddModifyReturnCredit(ByVal csvData()() As String, queryParams As ReturnCreditModel) As Boolean
        'Row 0 - Header 1
        '0 Ship-to-Branch-Code
        '1 Ship-to-Branch
        '2 Invoice Date
        '3 Invoice No
        '4 Local Invoice No
        '5 Delivery No
        '6 Items
        '7 Amount
        '8 SGST / UTGST
        '9 CGST
        '10 IGST
        '11 Cess
        '12 Tax
        '13 Total
        '14 GR Status

        Log4NetControl.ComInfoLogWrite(Log4NetControl.UserID)
        'Mandatory Column 14 from CSV
        Dim flag As Boolean = True
        If csvData(0).Length < 15 Then
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
        sqlStr = "SELECT TOP 1 INVOICE_NO,LOCAL_INVOICE_NO,DELIVERY_NO FROM RETURN_CREDIT "
        sqlStr = sqlStr & " WHERE DELFG = 0 AND SRC_FILE_NAME='" & queryParams.SrcFileName & "'"
        'sqlStr = sqlStr & " AND SHIP_TO_BRANCH_CODE='" & queryParams.ShipToBranchCode & "'"
        dtReturnCreditExist = dbConn.GetDataSet(sqlStr)
        'if exist then need to update delfg=1 then insert the record as new
        If (dtReturnCreditExist Is Nothing) Or (dtReturnCreditExist.Rows.Count = 0) Then
            'isExist = 0
        Else 'The records is already exist, need to update DELFg=0
            ' isExist = 1
            sqlStr = "UPDATE RETURN_CREDIT SET DELFG=1  "
            sqlStr = sqlStr & "WHERE DELFG=0 "
            sqlStr = sqlStr & "AND SRC_FILE_NAME = @SRC_FILE_NAME"
            'sqlStr = sqlStr & "AND SHIP_TO_BRANCH_CODE = @SHIP_TO_BRANCH_CODE "
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
                If i > 0 Then '0  Header

                    'If isExist = 1 Then
                    sqlStr = "Insert into RETURN_CREDIT ("
                    sqlStr = sqlStr & "CRTDT, "
                    sqlStr = sqlStr & "CRTCD, "
                    ' sqlStr = sqlStr & "UPDDT, "
                    sqlStr = sqlStr & "UPDCD, "
                    sqlStr = sqlStr & "UPDPG, "
                    sqlStr = sqlStr & "DELFG, "
                    '         sqlStr = sqlStr & "UNQ_NO, "
                    sqlStr = sqlStr & "UPLOAD_USER, "
                    sqlStr = sqlStr & "UPLOAD_DATE, "
                    sqlStr = sqlStr & "SHIP_TO_BRANCH_CODE, "
                    sqlStr = sqlStr & "SHIP_TO_BRANCH, "
                    sqlStr = sqlStr & "INVOICE_DATE, "
                    sqlStr = sqlStr & "INVOICE_NO, "
                    sqlStr = sqlStr & "LOCAL_INVOICE_NO, "
                    sqlStr = sqlStr & "DELIVERY_NO, "
                    sqlStr = sqlStr & "ITEMS, "
                    sqlStr = sqlStr & "AMOUNT, "
                    sqlStr = sqlStr & "SGST_UTGST, "
                    sqlStr = sqlStr & "CGST, "
                    sqlStr = sqlStr & "IGST, "
                    sqlStr = sqlStr & "CESS, "
                    sqlStr = sqlStr & "TAX, "
                    sqlStr = sqlStr & "TOTAL, "
                    sqlStr = sqlStr & "GR_STATUS, "
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
                    '          sqlStr = sqlStr & " (select max(UNQ_NO)+1 from RETURN_CREDIT ) , "
                    sqlStr = sqlStr & "@UPLOAD_USER, "
                    sqlStr = sqlStr & "@UPLOAD_DATE, "
                    sqlStr = sqlStr & "@SHIP_TO_BRANCH_CODE, "
                    sqlStr = sqlStr & "@SHIP_TO_BRANCH, "

                    sqlStr = sqlStr & "@INVOICE_DATE, "
                    sqlStr = sqlStr & "@INVOICE_NO, "
                    sqlStr = sqlStr & "@LOCAL_INVOICE_NO, "
                    sqlStr = sqlStr & "@DELIVERY_NO, "
                    sqlStr = sqlStr & "@ITEMS, "
                    sqlStr = sqlStr & "@AMOUNT, "
                    sqlStr = sqlStr & "@SGST_UTGST, "
                    sqlStr = sqlStr & "@CGST, "
                    sqlStr = sqlStr & "@IGST, "
                    sqlStr = sqlStr & "@CESS, "
                    sqlStr = sqlStr & "@TAX, "
                    sqlStr = sqlStr & "@TOTAL, "
                    sqlStr = sqlStr & "@GR_STATUS, "
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
                    dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@SHIP_TO_BRANCH_CODE", csvData(i)(0)))
                    dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@SHIP_TO_BRANCH", csvData(i)(1)))

                    dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@INVOICE_DATE", csvData(i)(2)))
                    dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@INVOICE_NO", csvData(i)(3)))
                    dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@LOCAL_INVOICE_NO", csvData(i)(4)))
                    dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@DELIVERY_NO", csvData(i)(5)))
                    dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@ITEMS", csvData(i)(6)))
                    dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@AMOUNT", csvData(i)(7)))
                    dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@SGST_UTGST", csvData(i)(8)))
                    dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@CGST", csvData(i)(9)))
                    dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@IGST", csvData(i)(10)))
                    dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@CESS", csvData(i)(11)))
                    'Remove - (negative) from begining
                    If Len(csvData(i)(12)) > 0 Then
                        If Left(csvData(i)(12), 1) = "-" Then
                            csvData(i)(12) = Right(csvData(i)(12), Len(csvData(i)(12)) - 1)
                        End If
                    End If
                    dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@TAX", csvData(i)(12)))
                    dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@TOTAL", csvData(i)(13)))
                    dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@GR_STATUS", csvData(i)(14)))
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


    Public Function SelectReturnCredit(ByVal queryParams As ReturnCreditModel) As DataTable
        Log4NetControl.ComInfoLogWrite(Log4NetControl.UserID)
        Dim dbConn As DBUtility = New DBUtility()
        Dim dt As DataTable = New DataTable()
        Dim sqlStr As String = "SELECT "
        'sqlStr = sqlStr & "DELFG,UNQ_NO,UPLOAD_USER,UPLOAD_DATE,SHIP_TO_BRANCH_CODE,SHIP_TO_BRANCH,INVOICE_DATE,INVOICE_NO,LOCAL_INVOICE_NO,DELIVERY_NO,ITEMS,AMOUNT,SGST_UTGST,CGST,IGST,CESS,TAX,TOTAL,GR_STATUS,FILE_NAME"
        sqlStr = sqlStr & "SHIP_TO_BRANCH_CODE as 'Ship-to-Branch-Code',SHIP_TO_BRANCH as 'Ship-to-Branch',INVOICE_DATE as 'Invoice Date',INVOICE_NO as 'Invoice No',LOCAL_INVOICE_NO as 'Local Invoice No',DELIVERY_NO as 'Delivery No',ITEMS as 'Items',AMOUNT as 'Amount',SGST_UTGST as 'SGST / UTGST',CGST as 'CGST',IGST as 'IGST',CESS as 'Cess',TAX as 'Tax',TOTAL as 'Total',GR_STATUS as 'GR Status' "
        sqlStr = sqlStr & "FROM "
        sqlStr = sqlStr & "RETURN_CREDIT "
        sqlStr = sqlStr & "WHERE "
        sqlStr = sqlStr & "DELFG=0 "
        If Not String.IsNullOrEmpty(queryParams.SrcFileName) Then
            sqlStr = sqlStr & "AND SRC_FILE_NAME = @SRC_FILE_NAME "
            dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@SRC_FILE_NAME", queryParams.SrcFileName))
        End If
        ''If (Not (String.IsNullOrEmpty(queryParams.DateFrom)) And (Not (String.IsNullOrEmpty(queryParams.DateTo)))) Then
        ''    sqlStr = sqlStr & "AND LEFT(CONVERT(VARCHAR, INVOICE_DATE, 111), 10) >= @DateFrom and LEFT(CONVERT(VARCHAR, INVOICE_DATE, 111), 10) <= @DateTo "
        ''    'sqlStr = sqlStr & "AND INVOICE_DATE >= @DateFrom and INVOICE_DATE <= @DateTo "
        ''    dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@DateFrom", queryParams.DateFrom))
        ''    dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@DateTo", queryParams.DateTo))
        ''ElseIf Not String.IsNullOrEmpty(queryParams.DateFrom) Then
        ''    'sqlStr = sqlStr & "AND LEFT(CONVERT(VARCHAR, INVOICE_DATE, 111), 10) = @DateFrom "
        ''    sqlStr = sqlStr & "AND INVOICE_DATE = @DateFrom "
        ''    dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@DateFrom", queryParams.DateFrom))
        ''ElseIf Not String.IsNullOrEmpty(queryParams.DateTo) Then
        ''    sqlStr = sqlStr & "AND INVOICE_DATE = @DateTo "
        ''    'sqlStr = sqlStr & "AND LEFT(CONVERT(VARCHAR, INVOICE_DATE, 111), 10) = @DateTo "
        ''    dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@DateTo", queryParams.DateTo))
        ''End If

        Dim _DataTable As DataTable = dbConn.GetDataSet(sqlStr)
        dbConn.CloseConnection()
        Return _DataTable
    End Function
End Class
