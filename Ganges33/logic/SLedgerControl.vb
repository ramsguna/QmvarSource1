Imports System
Imports System.Data
Imports System.Web.Security
Imports System.Collections.Generic
Imports Ganges33.Ganges33.dao
Imports Ganges33.Ganges33.model
Imports Ganges33.Ganges33.logic

Public Class SLedgerControl
    Public Function AddModifySLedger(ByVal csvData()() As String, queryParams As SLedgerModel) As Boolean
        'Row 0 - Header1 and 1 - Header2
        '2 Doc.No.
        '3 Item
        '4 Document Date
        '5 Posting Date
        '6 Doc. Type
        '7 Invoice No.
        '8 Curr.
        '9 Debit Amount
        '10 Credit Amount
        '11 Closing Balance
        '12 Assignment
        '13 Text
        '14 BA
        '15 Segment

        Log4NetControl.ComInfoLogWrite(Log4NetControl.UserID)
        'Mandatory Column 13 from CSV
        Dim flag As Boolean = True
        If csvData(0).Length < 14 Then
            Return False
        End If

        '    Log4NetControl.ComInfoLogWrite(Log4NetControl.UserID)
        Dim DateTimeNow As DateTime = DateTime.Now
        Dim dtNow As DateTime = DateTimeNow.AddMinutes(ConfigurationManager.AppSettings("TimeDiff"))
        Dim dbConn As DBUtility = New DBUtility()
        Dim dt As DataTable = New DataTable()
        dbConn.sqlTrn = dbConn.sqlConn.BeginTransaction()
        dbConn.sqlCmd.Transaction = dbConn.sqlTrn
        '   Dim flag As Boolean = True
        Dim flagAll As Boolean = True
        Dim sqlStr As String = ""
        Dim dtSLedgerExist As DataTable

        Dim isExist As Integer = 0
        '1st check INVOICE_NO exist in the table 
        sqlStr = "SELECT TOP 1 INVOICE_NO FROM S_LEDGER "
        sqlStr = sqlStr & " WHERE DELFG = 0 AND SRC_FILE_NAME='" & queryParams.SrcFileName & "'"
        'sqlStr = sqlStr & " AND SHIP_TO_BRANCH_CODE='" & queryParams.ShipToBranchCode & "'""
        dtSLedgerExist = dbConn.GetDataSet(sqlStr)
        'if exist then need to update delfg=1 then insert the record as new
        If (dtSLedgerExist Is Nothing) Or (dtSLedgerExist.Rows.Count = 0) Then
            'isExist = 0
        Else 'The records is already exist, need to update DELFg=0
            ' isExist = 1
            sqlStr = "UPDATE S_LEDGER SET DELFG=1  "
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
                If i > 1 Then '0  Header1 and 1 Header2
                    'If isExist = 1 Then
                    sqlStr = "Insert into S_LEDGER ("
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

                    sqlStr = sqlStr & "DOC_NO, "
                    sqlStr = sqlStr & "ITEMS, "
                    sqlStr = sqlStr & "DOCUMENT_DATE, "
                    sqlStr = sqlStr & "POSTING_DATE, "
                    sqlStr = sqlStr & "DOC_TYPE, "
                    sqlStr = sqlStr & "INVOICE_NO, "
                    sqlStr = sqlStr & "CURR, "
                    sqlStr = sqlStr & "DEBIT_AMOUNT, "
                    sqlStr = sqlStr & "CREDIT_AMOUNT, "
                    sqlStr = sqlStr & "CLOSING_BALANCE, "
                    sqlStr = sqlStr & "ASSIGNMENT, "
                    sqlStr = sqlStr & "TEXT, "
                    sqlStr = sqlStr & "BA, "
                    sqlStr = sqlStr & "SEGMENT, "
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
                    '               sqlStr = sqlStr & " (select max(UNQ_NO)+1 from S_LEDGER ) , "
                    sqlStr = sqlStr & "@UPLOAD_USER, "
                    sqlStr = sqlStr & "@UPLOAD_DATE, "
                    sqlStr = sqlStr & "@SHIP_TO_BRANCH_CODE, "
                    sqlStr = sqlStr & "@SHIP_TO_BRANCH, "
                    sqlStr = sqlStr & "@DOC_NO, "
                    sqlStr = sqlStr & "@ITEMS, "
                    sqlStr = sqlStr & "@DOCUMENT_DATE, "
                    sqlStr = sqlStr & "@POSTING_DATE, "
                    sqlStr = sqlStr & "@DOC_TYPE, "
                    sqlStr = sqlStr & "@INVOICE_NO, "
                    sqlStr = sqlStr & "@CURR, "
                    sqlStr = sqlStr & "@DEBIT_AMOUNT, "
                    sqlStr = sqlStr & "@CREDIT_AMOUNT, "
                    sqlStr = sqlStr & "@CLOSING_BALANCE, "
                    sqlStr = sqlStr & "@ASSIGNMENT, "
                    sqlStr = sqlStr & "@TEXT, "
                    sqlStr = sqlStr & "@BA, "
                    sqlStr = sqlStr & "@SEGMENT, "
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

                    dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@DOC_NO", csvData(i)(0)))
                    dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@ITEMS", csvData(i)(1)))
                    dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@DOCUMENT_DATE", csvData(i)(2)))
                    dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@POSTING_DATE", csvData(i)(3)))
                    dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@DOC_TYPE", csvData(i)(4)))
                    dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@INVOICE_NO", csvData(i)(5)))
                    dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@CURR", csvData(i)(6)))
                    dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@DEBIT_AMOUNT", csvData(i)(7)))
                    dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@CREDIT_AMOUNT", csvData(i)(8)))
                    'Remove Closing  end of the digit -
                    If Len(csvData(i)(9)) > 0 Then
                        If Right(csvData(i)(9), 1) = "-" Then
                            csvData(i)(9) = Left(csvData(i)(9), Len(csvData(i)(9)) - 1)
                        End If
                    End If
                    dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@CLOSING_BALANCE", csvData(i)(9)))
                    dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@ASSIGNMENT", csvData(i)(10)))
                    dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@TEXT", csvData(i)(11)))
                    dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@BA", csvData(i)(12)))
                    dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@SEGMENT", csvData(i)(13)))
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

    Public Function SelectSLedger(ByVal queryParams As SLedgerModel) As DataTable
        Log4NetControl.ComInfoLogWrite(Log4NetControl.UserID)
        Dim dbConn As DBUtility = New DBUtility()
        Dim dt As DataTable = New DataTable()
        Dim sqlStr As String = "SELECT "
        'sqlStr = sqlStr & "DELFG,UNQ_NO,UPLOAD_USER,UPLOAD_DATE,DOC_NO,ITEMS,DOCUMENT_DATE,POSTING_DATE,DOC_TYPE,INVOICE_NO,CURR,DEBIT_AMOUNT,CREDIT_AMOUNT,CLOSING_BALANCE,ASSIGNMENT,TEXT,BA,SEGMENT,BRANCH_CODE,FILE_NAME"
        sqlStr = sqlStr & "DOC_NO as 'Doc.No.',ITEMS as 'Item',REPLACE(LEFT(CONVERT(VARCHAR,  DOCUMENT_DATE, 102), 10),'/','.') as 'Document Date', REPLACE(LEFT(CONVERT(VARCHAR,  POSTING_DATE, 102), 10),'/','.') as 'Posting Date',DOC_TYPE as 'Doc. Type',INVOICE_NO as 'Invoice No.',CURR as 'Curr.',DEBIT_AMOUNT as 'Debit Amount',CREDIT_AMOUNT as 'Credit Amount',CLOSING_BALANCE as 'Closing Balance',ASSIGNMENT as 'Assignment',TEXT as 'Text',BA as 'BA',SEGMENT as 'Segment' "
        sqlStr = sqlStr & "FROM "
        sqlStr = sqlStr & "S_LEDGER "
        sqlStr = sqlStr & "WHERE "
        sqlStr = sqlStr & "DELFG=0 "
        If Not String.IsNullOrEmpty(queryParams.SrcFileName) Then
            sqlStr = sqlStr & "AND SRC_FILE_NAME = @SRC_FILE_NAME "
            dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@SRC_FILE_NAME", queryParams.SrcFileName))
        End If


        Dim _DataTable As DataTable = dbConn.GetDataSet(sqlStr)
        dbConn.CloseConnection()
        Return _DataTable
    End Function
End Class
