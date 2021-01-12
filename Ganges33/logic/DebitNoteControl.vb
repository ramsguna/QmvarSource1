Imports System
Imports System.Data
Imports System.Web.Security
Imports System.Collections.Generic
Imports Ganges33.Ganges33.dao
Imports Ganges33.Ganges33.model
Imports Ganges33.Ganges33.logic

Public Class DebitNoteControl


    Public Function AddModifyDebitNote(ByVal csvData()() As String, queryParams As DebitNoteModel) As Boolean
        'Row 0 - Header 1
        '0 Samsung Ref No
        '1 Your Ref No
        '2 Model
        '3 Serial
        '4 Product
        '5 Service
        '6 Defect Code
        '7 Currency
        '8 Invoice
        '9 Labor
        '10 Parts
        '11 Freight
        '12 Other
        '13 Tax

        Log4NetControl.ComInfoLogWrite(Log4NetControl.UserID)
        'Mandatory Column 13 from CSV
        Dim flag As Boolean = True
        If csvData(0).Length < 14 Then
            Return False
        End If

        '       Log4NetControl.ComInfoLogWrite(Log4NetControl.UserID)
        Dim DateTimeNow As DateTime = DateTime.Now
        Dim dtNow As DateTime = DateTimeNow.AddMinutes(ConfigurationManager.AppSettings("TimeDiff"))
        Dim dbConn As DBUtility = New DBUtility()
        Dim dt As DataTable = New DataTable()
        dbConn.sqlTrn = dbConn.sqlConn.BeginTransaction()
        dbConn.sqlCmd.Transaction = dbConn.sqlTrn
        '        Dim flag As Boolean = True
        Dim flagAll As Boolean = True
        Dim sqlStr As String = ""
        Dim dtUpdateOtherExist As DataTable

        Dim isExist As Integer = 0

        '1st check SAMSUNG_REF_NO,YOUR_REF_NO exist in the table 
        sqlStr = "SELECT TOP 1 SAMSUNG_REF_NO,YOUR_REF_NO FROM DEBIT_NOTE "
        sqlStr = sqlStr & " WHERE DELFG = 0 AND SRC_FILE_NAME='" & queryParams.SrcFileName & "'"
        'sqlStr = sqlStr & " AND SHIP_TO_BRANCH_CODE='" & queryParams.ShipToBranchCode & "'"
        'sqlStr = sqlStr & " WHERE DELFG = 0 AND SAMSUNG_REF_NO='" & csvData(i)(0) & "' and YOUR_REF_NO='" & csvData(i)(1) & "'"
        dtUpdateOtherExist = dbConn.GetDataSet(sqlStr)
        'if exist then need to update delfg=1 then insert the record as new
        If (dtUpdateOtherExist Is Nothing) Or (dtUpdateOtherExist.Rows.Count = 0) Then
            'isExist = 0
        Else 'The records is already exist, need to update DELFg=0
            ' isExist = 1
            sqlStr = "UPDATE DEBIT_NOTE SET DELFG=1  "
            sqlStr = sqlStr & "WHERE DELFG=0 "
            sqlStr = sqlStr & "AND SRC_FILE_NAME = @SRC_FILE_NAME"
            'sqlStr = sqlStr & "AND SHIP_TO_BRANCH_CODE = @SHIP_TO_BRANCH_CODE "
            dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@SRC_FILE_NAME", queryParams.SrcFileName))
            ' dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@SHIP_TO_BRANCH_CODE", queryParams.ShipToBranchCode))
            'sqlStr = sqlStr & "SAMSUNG_REF_NO = @SAMSUNG_REF_NO AND YOUR_REF_NO = @YOUR_REF_NO"
            'dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@SAMSUNG_REF_NO", csvData(i)(0))) 'SAMSUNG_REF_NO
            'dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@YOUR_REF_NO", csvData(i)(1))) 'YOUR_REF_NO
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
                    sqlStr = "Insert into DEBIT_NOTE ("
                    sqlStr = sqlStr & "CRTDT, "
                    sqlStr = sqlStr & "CRTCD, "
                    ' sqlStr = sqlStr & "UPDDT, "
                    sqlStr = sqlStr & "UPDCD, "
                    sqlStr = sqlStr & "UPDPG, "
                    sqlStr = sqlStr & "DELFG, "
                    '  sqlStr = sqlStr & "UNQ_NO, "
                    sqlStr = sqlStr & "UPLOAD_USER, "
                    sqlStr = sqlStr & "UPLOAD_DATE, "
                    sqlStr = sqlStr & "SHIP_TO_BRANCH_CODE, "
                    sqlStr = sqlStr & "SHIP_TO_BRANCH, "
                    sqlStr = sqlStr & "SAMSUNG_REF_NO, "
                    sqlStr = sqlStr & "YOUR_REF_NO, "
                    sqlStr = sqlStr & "MODEL, "
                    sqlStr = sqlStr & "SERIAL, "
                    sqlStr = sqlStr & "PRODUCT, "
                    sqlStr = sqlStr & "SERVICE, "
                    sqlStr = sqlStr & "DEFACT_CODE, "
                    sqlStr = sqlStr & "CURRENCY, "
                    sqlStr = sqlStr & "INVOICE, "
                    sqlStr = sqlStr & "LABOR, "
                    sqlStr = sqlStr & "PARTS, "
                    sqlStr = sqlStr & "FREIGHT, "
                    sqlStr = sqlStr & "OTHER, "
                    sqlStr = sqlStr & "TAX, "
                    sqlStr = sqlStr & "FILE_NAME, "
                    sqlStr = sqlStr & "LABOR_INVOICE_NO, "
                    sqlStr = sqlStr & "INVOICE_DATE, "
                    sqlStr = sqlStr & "REPORTNUMBER, "
                    sqlStr = sqlStr & "SRC_FILE_NAME "
                    sqlStr = sqlStr & " ) "
                    sqlStr = sqlStr & " values ( "
                    sqlStr = sqlStr & "@CRTDT, "
                    sqlStr = sqlStr & "@CRTCD, "
                    'sqlStr = sqlStr & "@UPDDT, "
                    sqlStr = sqlStr & "@UPDCD, "
                    sqlStr = sqlStr & "@UPDPG, "
                    sqlStr = sqlStr & "@DELFG, "
                    '          sqlStr = sqlStr & " (select max(UNQ_NO)+1 from OTHER_UPDATE) , "
                    sqlStr = sqlStr & "@UPLOAD_USER, "
                    sqlStr = sqlStr & "@UPLOAD_DATE, "
                    sqlStr = sqlStr & "@SHIP_TO_BRANCH_CODE, "
                    sqlStr = sqlStr & "@SHIP_TO_BRANCH, "
                    sqlStr = sqlStr & "@SAMSUNG_REF_NO, "
                    sqlStr = sqlStr & "@YOUR_REF_NO, "
                    sqlStr = sqlStr & "@MODEL, "
                    sqlStr = sqlStr & "@SERIAL, "
                    sqlStr = sqlStr & "@PRODUCT, "
                    sqlStr = sqlStr & "@SERVICE, "
                    sqlStr = sqlStr & "@DEFACT_CODE, "
                    sqlStr = sqlStr & "@CURRENCY, "
                    sqlStr = sqlStr & "@INVOICE, "
                    sqlStr = sqlStr & "@LABOR, "
                    sqlStr = sqlStr & "@PARTS, "
                    sqlStr = sqlStr & "@FREIGHT, "
                    sqlStr = sqlStr & "@OTHER, "
                    sqlStr = sqlStr & "@TAX, "
                    sqlStr = sqlStr & "@FILE_NAME, "
                    sqlStr = sqlStr & "@LABOR_INVOICE_NO, "
                    sqlStr = sqlStr & "@INVOICE_DATE, "
                    sqlStr = sqlStr & "@REPORTNUMBER, "
                    sqlStr = sqlStr & "@SRC_FILE_NAME )"
                    dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@CRTDT", dtNow))
                    dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@CRTCD", queryParams.UserId))
                    dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@UPDCD", "")) '?????????????????????????
                    dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@UPDPG", queryParams.UPDPG))
                    dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@DELFG", 0))
                    dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@UPLOAD_USER", queryParams.UploadUser))
                    dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@UPLOAD_DATE", dtNow))
                    dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@SHIP_TO_BRANCH_CODE", queryParams.ShipToBranchCode))
                    dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@SHIP_TO_BRANCH", queryParams.ShipToBranch))
                    dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@SAMSUNG_REF_NO", csvData(i)(0)))
                    dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@YOUR_REF_NO", csvData(i)(1)))
                    dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@MODEL", csvData(i)(2)))
                    dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@SERIAL", csvData(i)(3)))
                    dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@PRODUCT", csvData(i)(4)))
                    dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@SERVICE", csvData(i)(5)))
                    dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@DEFACT_CODE", csvData(i)(6)))
                    dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@CURRENCY", csvData(i)(7)))
                    'Remove - (negative) from begining
                    If Len(csvData(i)(8)) > 0 Then
                        If Left(csvData(i)(8), 1) = "-" Then
                            csvData(i)(8) = Right(csvData(i)(8), Len(csvData(i)(8)) - 1)
                        End If
                    End If


                    dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@INVOICE", csvData(i)(8)))
                    dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@LABOR", csvData(i)(9)))
                    dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@PARTS", csvData(i)(10)))
                    dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@FREIGHT", csvData(i)(11)))
                    'Remove - (negative) from begining
                    If Len(csvData(i)(12)) > 0 Then
                        If Left(csvData(i)(12), 1) = "-" Then
                            csvData(i)(12) = Right(csvData(i)(12), Len(csvData(i)(12)) - 1)
                        End If
                    End If
                    dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@OTHER", csvData(i)(12)))
                    dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@TAX", csvData(i)(13)))
                    dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@FILE_NAME", queryParams.FileName))
                    dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@LABOR_INVOICE_NO", queryParams.LaborInvoiceNo))
                    dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@INVOICE_DATE", queryParams.InvoiceDate))
                    dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@REPORTNUMBER", queryParams.ReportNumber))
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
    Public Function SelectDebitNote(ByVal queryParams As DebitNoteModel) As DataTable
        Log4NetControl.ComInfoLogWrite(Log4NetControl.UserID)
        Dim dbConn As DBUtility = New DBUtility()
        Dim dt As DataTable = New DataTable()
        Dim sqlStr As String = "SELECT "
        sqlStr = sqlStr & " SHIP_TO_BRANCH_CODE as 'ASC Code', "
        sqlStr = sqlStr & " SHIP_TO_BRANCH as 'Branch', "
        'sqlStr = sqlStr & "DELFG,UNQ_NO,UPLOAD_USER,UPLOAD_DATE,SHIP_TO_BRANCH_CODE,SHIP_TO_BRANCH,SAMSUNG_REF_NO,YOUR_REF_NO,MODEL,SERIAL,PRODUCT,SERVICE,DEFACT_CODE,CURRENCY,INVOICE, LABOR,PARTS,FREIGHT,OTHER,TAX,TOTAL,FILE_NAME,LABOR_INVOICE_NO,LEFT(CONVERT(VARCHAR, INVOICE_DATE, 111), 10) as INVOICE_DATE "
        sqlStr = sqlStr & "SAMSUNG_REF_NO as 'Samsung Ref No',YOUR_REF_NO as 'Your Ref No',MODEL as 'Model',SERIAL as 'Serial',PRODUCT as 'Product',SERVICE as 'Service',DEFACT_CODE as 'Defect Code',CURRENCY as 'Currency',INVOICE as 'Invoice', LABOR as 'Labor',PARTS as 'Parts',FREIGHT as 'Freight',OTHER as 'Other',TAX as 'Tax',LABOR_INVOICE_NO as 'LABOR_INVOICE_NO', LEFT(CONVERT(VARCHAR, INVOICE_DATE, 111), 10) as 'Invoice Date'  "
        sqlStr = sqlStr & "FROM "
        sqlStr = sqlStr & "DEBIT_NOTE "
        sqlStr = sqlStr & "WHERE "
        sqlStr = sqlStr & "DELFG=0 "
        'If Not String.IsNullOrEmpty(queryParams.SrcFileName) Then
        '    sqlStr = sqlStr & "AND SRC_FILE_NAME = @SRC_FILE_NAME "
        '    dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@SRC_FILE_NAME", queryParams.SrcFileName))
        'End If
        If (Not (String.IsNullOrEmpty(queryParams.DateFrom)) And (Not (String.IsNullOrEmpty(queryParams.DateTo)))) Then
            sqlStr = sqlStr & "AND LEFT(CONVERT(VARCHAR, INVOICE_DATE, 111), 10) >= @DateFrom and LEFT(CONVERT(VARCHAR, INVOICE_DATE, 111), 10) <= @DateTo "
            'sqlStr = sqlStr & "AND INVOICE_DATE >= @DateFrom and INVOICE_DATE <= @DateTo "
            dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@DateFrom", queryParams.DateFrom))
            dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@DateTo", queryParams.DateTo))
        ElseIf Not String.IsNullOrEmpty(queryParams.DateFrom) Then
            'sqlStr = sqlStr & "AND LEFT(CONVERT(VARCHAR, INVOICE_DATE, 111), 10) = @DateFrom "
            sqlStr = sqlStr & "AND INVOICE_DATE = @DateFrom "
            dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@DateFrom", queryParams.DateFrom))
        ElseIf Not String.IsNullOrEmpty(queryParams.DateTo) Then
            sqlStr = sqlStr & "AND INVOICE_DATE = @DateTo "
            'sqlStr = sqlStr & "AND LEFT(CONVERT(VARCHAR, INVOICE_DATE, 111), 10) = @DateTo "
            dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@DateTo", queryParams.DateTo))
        End If


        Dim _DataTable As DataTable = dbConn.GetDataSet(sqlStr)
        dbConn.CloseConnection()
        Return _DataTable
    End Function
End Class
