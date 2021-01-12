Imports System
Imports System.Data
Imports System.Web.Security
Imports System.Collections.Generic
Imports Ganges33.Ganges33.dao
Imports Ganges33.Ganges33.model
Imports Ganges33.Ganges33.logic
Imports System.Globalization

Public Class AccountReportControl

    Public Function SelectAccountReport(ByVal queryParams As AccountReport) As DataTable
        Log4NetControl.ComInfoLogWrite(Log4NetControl.UserID)
        Dim dbConn As DBUtility = New DBUtility()
        Dim dtAccountDtl As DataTable = New DataTable()
        ' Dim lsidetbl As Int16 = 0
        'Dim rsidetbl As Int16 = 0
        Dim LeftTotal As Double = 0
        Dim RightTotal As Double = 0
        Dim PrevMonth As String
        Dim PrevYear As String
        Dim ClosingBalance As Double
        Dim ConAccountDtl As DataTable = New DataTable()
        If queryParams.Month = "01" Then
            PrevMonth = "12"
            PrevYear = (Convert.ToInt16(queryParams.Year) - 1).ToString()

        Else
            PrevMonth = (Convert.ToInt16(queryParams.Month) - 1)
            PrevYear = queryParams.Year
        End If

        Dim _DataTable As DataTable
        Dim rw As DataRow
        Dim column As DataColumn
        column = New DataColumn
        column.DataType = System.Type.GetType("System.Int32")
        column.ColumnName = "ID"
        column.AutoIncrementSeed = 1

        column.AutoIncrement = True
        column.Caption = "ID"
        column.ReadOnly = True
        column.Unique = True
        dtAccountDtl.Columns.Add(column)
        dtAccountDtl.Columns.Add("SeqID", GetType(System.Int16))
        dtAccountDtl.Columns.Add("SeqName")
        dtAccountDtl.Columns.Add("Position")
        dtAccountDtl.Columns.Add("Title")
        dtAccountDtl.Columns.Add("Amount", GetType(System.Double))
        Dim sqlStr As String = ""

        sqlStr = "Select CLOSING_BALANCE as Amount"
        sqlStr = sqlStr & " from S_LEDGER "
        sqlStr = sqlStr & " where DOC_NO Like 'Closing Balance%' "
        sqlStr = sqlStr & "  And DELFG = 0 "
        sqlStr = sqlStr & " and SHIP_TO_BRANCH = @SHIP_TO_BRANCH "
        sqlStr = sqlStr & " And MONTH(CONVERT(date, substring(SRC_FILE_NAME,len(SRC_FILE_NAME) -11,8))) = @Month "
        sqlStr = sqlStr & " And YEAR(CONVERT(date, substring(SRC_FILE_NAME,len(SRC_FILE_NAME) -11,8))) = @Year "

        dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@SHIP_TO_BRANCH", queryParams.SHIP_TO_BRANCH))
        dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@Month", queryParams.Month))
        dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@Year", queryParams.Year))
        _DataTable = dbConn.GetDataSet(sqlStr)
        If _DataTable.Rows.Count = 0 Then
            ClosingBalance = 0.00
        Else
            For Each row As DataRow In _DataTable.Rows
                ClosingBalance = row("Amount")
            Next row
        End If
        _DataTable.Clear()
        dbConn.sqlCmd.Parameters.Clear()

        sqlStr = ""
        sqlStr = "Select  '1' as SeqID, 'A' as SeqName, 'L' as Position,'Opening Balance' as 'Title'"
        sqlStr = sqlStr & " ,CLOSING_BALANCE as Amount "
        sqlStr = sqlStr & " from S_LEDGER "
        sqlStr = sqlStr & " where DOC_NO Like 'Closing Balance%' "
        sqlStr = sqlStr & "  And DELFG = 0 "
        sqlStr = sqlStr & " and SHIP_TO_BRANCH = @SHIP_TO_BRANCH "
        sqlStr = sqlStr & " And MONTH(CONVERT(date, substring(SRC_FILE_NAME,len(SRC_FILE_NAME) -11,8))) = @Month "
        sqlStr = sqlStr & " And YEAR(CONVERT(date, substring(SRC_FILE_NAME,len(SRC_FILE_NAME) -11,8))) = @Year "

        dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@SHIP_TO_BRANCH", queryParams.SHIP_TO_BRANCH))
        dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@Month", PrevMonth))
        dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@Year", PrevYear))
        _DataTable = dbConn.GetDataSet(sqlStr)
        If _DataTable.Rows.Count = 0 Then
            rw = dtAccountDtl.NewRow
            rw("SeqID") = 1
            rw("SeqName") = "A"
            rw("Position") = "L"
            rw("Title") = "Opening Balance"
            rw("Amount") = 0.00
            dtAccountDtl.Rows.Add(rw)
            LeftTotal += rw("Amount")
        Else
            ' dtAccountDtl.Merge(_DataTable)
            For Each row As DataRow In _DataTable.Rows
                rw = dtAccountDtl.NewRow
                rw("SeqID") = row("SeqID")
                rw("SeqName") = row("SeqName")
                rw("Position") = row("Position")
                rw("Title") = row("Title")
                rw("Amount") = row("Amount")
                LeftTotal += rw("Amount")
                dtAccountDtl.Rows.Add(rw)
            Next row

        End If
        _DataTable.Clear()
        dbConn.sqlCmd.Parameters.Clear()

        sqlStr = ""
        sqlStr = "Select '1' as SeqID, 'F' as SeqName, 'R' as Position,'Purchased' as 'Title',isnull(sum(total),0.00) Amount "
        sqlStr = sqlStr & " From PR_SUMMARY "
        sqlStr = sqlStr & " where DELFG = 0 "
        sqlStr = sqlStr & " and SHIP_TO_BRANCH_CODE = @SHIP_TO_BRANCH_CODE "
        sqlStr = sqlStr & " And MONTH(INVOICE_DATE) = @Month "
        sqlStr = sqlStr & " And YEAR(INVOICE_DATE) = @Year "

        dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@SHIP_TO_BRANCH_CODE", queryParams.SHIP_TO_BRANCH_CODE))
        dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@Month", queryParams.Month))
        dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@Year", queryParams.Year))
        ' Dim _DataTable1 As DataTable = dbConn.GetDataSet(sqlStr)
        _DataTable = dbConn.GetDataSet(sqlStr)
        If _DataTable.Rows.Count = 0 Then
            rw = dtAccountDtl.NewRow
            rw("SeqID") = 1
            rw("SeqName") = "F"
            rw("Position") = "R"
            rw("Title") = "Purchased"
            rw("Amount") = 0.00
            RightTotal += rw("Amount")
            dtAccountDtl.Rows.Add(rw)
        Else
            ' dtAccountDtl.Merge(_DataTable)
            For Each row As DataRow In _DataTable.Rows
                rw = dtAccountDtl.NewRow
                rw("SeqID") = row("SeqID")
                rw("SeqName") = row("SeqName")
                rw("Position") = row("Position")
                rw("Title") = row("Title")
                rw("Amount") = row("Amount")
                RightTotal += rw("Amount")
                dtAccountDtl.Rows.Add(rw)
            Next row

        End If
        _DataTable.Clear()
        dbConn.sqlCmd.Parameters.Clear()
        sqlStr = ""

        sqlStr = "Select '2' as SeqID, 'B' as SeqName, 'L' as Position,'Invoice Value' as 'Title' ,isnull(sum(Labor +Parts),0.00) Amount "
        sqlStr = sqlStr & " from Invoice_update "
        sqlStr = sqlStr & " where DELFG = 0 "
        sqlStr = sqlStr & " and number in ('C','EXC','OWC') "
        sqlStr = sqlStr & " And upload_Branch = @SHIP_TO_BRANCH "
        sqlStr = sqlStr & " And month(Invoice_date) = @Month "
        sqlStr = sqlStr & " And YEAR(Invoice_date) = @Year "

        dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@SHIP_TO_BRANCH", queryParams.SHIP_TO_BRANCH))
        dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@Month", queryParams.Month))
        dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@Year", queryParams.Year))
        _DataTable = dbConn.GetDataSet(sqlStr)
        If _DataTable.Rows.Count = 0 Then
            rw = dtAccountDtl.NewRow
            rw("SeqID") = 2
            rw("SeqName") = "B"
            rw("Position") = "L"
            rw("Title") = "Invoice Value"
            rw("Amount") = 0.00
            LeftTotal += rw("Amount")
            dtAccountDtl.Rows.Add(rw)
        Else
            ' dtAccountDtl.Merge(_DataTable)
            For Each row As DataRow In _DataTable.Rows
                rw = dtAccountDtl.NewRow
                rw("SeqID") = row("SeqID")
                rw("SeqName") = row("SeqName")
                rw("Position") = row("Position")
                rw("Title") = row("Title")
                rw("Amount") = row("Amount")
                LeftTotal += rw("Amount")
                dtAccountDtl.Rows.Add(rw)
            Next row

        End If
        _DataTable.Clear()
        dbConn.sqlCmd.Parameters.Clear()
        sqlStr = ""

        sqlStr = "Select '2' as SeqID, 'G' as SeqName, 'R' as Position,'Samsug to GSS Receive (BOI)'  as 'Title',isnull(sum(AMOUNT),0.00) Amount "
        sqlStr = sqlStr & " From BOI "
        sqlStr = sqlStr & " where DELFG = 0 "
        sqlStr = sqlStr & " And SHIP_TO_BRANCH = @SHIP_TO_BRANCH "
        sqlStr = sqlStr & " And month(PAYMENT_DATE) = @Month "
        sqlStr = sqlStr & " And YEAR(PAYMENT_DATE) = @Year "
        dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@SHIP_TO_BRANCH", queryParams.SHIP_TO_BRANCH))
        dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@Month", queryParams.Month))
        dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@Year", queryParams.Year))
        _DataTable = dbConn.GetDataSet(sqlStr)
        If _DataTable.Rows.Count = 0 Then
            rw = dtAccountDtl.NewRow
            rw("SeqID") = 2
            rw("SeqName") = "G"
            rw("Position") = "R"
            rw("Title") = "Samsug to GSS Receive (BOI)"
            rw("Amount") = 0.00
            RightTotal += rw("Amount")
            dtAccountDtl.Rows.Add(rw)
        Else
            ' dtAccountDtl.Merge(_DataTable)
            For Each row As DataRow In _DataTable.Rows
                rw = dtAccountDtl.NewRow
                rw("SeqID") = row("SeqID")
                rw("SeqName") = row("SeqName")
                rw("Position") = row("Position")
                rw("Title") = row("Title")
                rw("Amount") = row("Amount")
                RightTotal += rw("Amount")
                dtAccountDtl.Rows.Add(rw)
            Next row

        End If
        _DataTable.Clear()
        dbConn.sqlCmd.Parameters.Clear()
        sqlStr = ""
        'And case when len(SHIP_TO_BRANCH_CODE) = 10 then SHIP_TO_BRANCH_CODE else '000' + CONVERT(varchar,SHIP_TO_BRANCH_CODE) end  = '0001907621'
        sqlStr = "Select '3' as SeqID, 'C' as SeqName, 'L' as Position,'GSS Paid to Samsung' as 'Title', isnull(sum(total),0.00) Amount "
        sqlStr = sqlStr & " From G2S_PIAD "
        sqlStr = sqlStr & " where DELFG = 0 "
        'sqlStr = sqlStr & " And SHIP_TO_BRANCH_CODE = @SHIP_TO_BRANCH_CODE "
        sqlStr = sqlStr & " And case when len(SHIP_TO_BRANCH_CODE) = 10 then SHIP_TO_BRANCH_CODE else '000' + CONVERT(varchar,SHIP_TO_BRANCH_CODE) end = @SHIP_TO_BRANCH_CODE "
        sqlStr = sqlStr & " And month(PAID_DATE) = @Month "
        sqlStr = sqlStr & " And YEAR(PAID_DATE) = @Year "
        dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@SHIP_TO_BRANCH_CODE", queryParams.SHIP_TO_BRANCH_CODE))
        dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@Month", queryParams.Month))
        dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@Year", queryParams.Year))
        _DataTable = dbConn.GetDataSet(sqlStr)
        If _DataTable.Rows.Count = 0 Then
            rw = dtAccountDtl.NewRow
            rw("SeqID") = 3
            rw("SeqName") = "C"
            rw("Position") = "L"
            rw("Title") = "GSS Paid to Samsung"
            rw("Amount") = 0.00
            LeftTotal += rw("Amount")
            dtAccountDtl.Rows.Add(rw)
        Else
            ' dtAccountDtl.Merge(_DataTable)
            For Each row As DataRow In _DataTable.Rows
                rw = dtAccountDtl.NewRow
                rw("SeqID") = row("SeqID")
                rw("SeqName") = row("SeqName")
                rw("Position") = row("Position")
                rw("Title") = row("Title")
                rw("Amount") = row("Amount")
                LeftTotal += rw("Amount")
                dtAccountDtl.Rows.Add(rw)
            Next row

        End If
        _DataTable.Clear()
        dbConn.sqlCmd.Parameters.Clear()
        sqlStr = ""

        sqlStr = "Select '3' as SeqID, 'H' as SeqName, 'R' as Position,Text as 'Title',isnull(sum(DEBIT_AMOUNT),0.00) Amount "
        sqlStr = sqlStr & " From S_LEDGER "
        sqlStr = sqlStr & " where DELFG = 0 And DOC_TYPE = 'VV' And Text Like 'Part Price Difference%' "
        sqlStr = sqlStr & " And SHIP_TO_BRANCH = @SHIP_TO_BRANCH "
        sqlStr = sqlStr & " And MONTH(CONVERT(date, substring(SRC_FILE_NAME,len(SRC_FILE_NAME) -11,8))) =  @Month "
        sqlStr = sqlStr & " And YEAR(CONVERT(date, substring(SRC_FILE_NAME,len(SRC_FILE_NAME) -11,8))) = @Year group by Text"
        dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@SHIP_TO_BRANCH", queryParams.SHIP_TO_BRANCH))
        dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@Month", queryParams.Month))
        dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@Year", queryParams.Year))
        _DataTable = dbConn.GetDataSet(sqlStr)
        If _DataTable.Rows.Count = 0 Then
            'lsidetbl = 1
            rw = dtAccountDtl.NewRow
            rw("SeqID") = 3
            rw("SeqName") = "H"
            rw("Position") = "R"
            rw("Title") = "Part Price Difference"
            rw("Amount") = 0.00
            RightTotal += rw("Amount")
            dtAccountDtl.Rows.Add(rw)
        Else
            ' dtAccountDtl.Merge(_DataTable)
            For Each row As DataRow In _DataTable.Rows
                'lsidetbl += 1
                rw = dtAccountDtl.NewRow
                rw("SeqID") = row("SeqID")
                rw("SeqName") = row("SeqName")
                rw("Position") = row("Position")
                rw("Title") = row("Title")
                rw("Amount") = row("Amount")
                RightTotal += rw("Amount")
                dtAccountDtl.Rows.Add(rw)
            Next row

        End If

        'rw = dtAccountDtl.NewRow
        'rw("SeqID") = 3
        'rw("SeqName") = "H"
        'rw("Position") = "R"
        'rw("Title") = ""
        'rw("Amount") = 0.00
        'dtAccountDtl.Rows.Add(rw)

        _DataTable.Clear()
        dbConn.sqlCmd.Parameters.Clear()
        sqlStr = ""

        sqlStr = "select '4' as SeqID, 'D' as SeqName, 'L' as Position,'Return Credit'as 'Title', isnull(sum(TOTAL),0.00) Amount  "
        sqlStr = sqlStr & " from RETURN_CREDIT "
        sqlStr = sqlStr & " where DELFG = 0 "
        sqlStr = sqlStr & " And SHIP_TO_BRANCH_CODE = @SHIP_TO_BRANCH_CODE "
        sqlStr = sqlStr & " And MONTH(CONVERT(date, substring(SRC_FILE_NAME,len(SRC_FILE_NAME) -9,6)+'01')) = @Month "
        sqlStr = sqlStr & " And YEAR(CONVERT(date, substring(SRC_FILE_NAME,len(SRC_FILE_NAME) -9,6)+'01')) = @Year "
        dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@SHIP_TO_BRANCH_CODE", queryParams.SHIP_TO_BRANCH_CODE))
        dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@Month", queryParams.Month))
        dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@Year", queryParams.Year))
        _DataTable = dbConn.GetDataSet(sqlStr)
        If _DataTable.Rows.Count = 0 Then
            rw = dtAccountDtl.NewRow
            rw("SeqID") = 4
            rw("SeqName") = "D"
            rw("Position") = "L"
            rw("Title") = "Return Credit"
            rw("Amount") = 0.00
            LeftTotal += rw("Amount")
            dtAccountDtl.Rows.Add(rw)
        Else
            ' dtAccountDtl.Merge(_DataTable)
            For Each row As DataRow In _DataTable.Rows
                rw = dtAccountDtl.NewRow
                rw("SeqID") = row("SeqID")
                rw("SeqName") = row("SeqName")
                rw("Position") = row("Position")
                rw("Title") = row("Title")
                rw("Amount") = row("Amount")
                LeftTotal += rw("Amount")
                dtAccountDtl.Rows.Add(rw)
            Next row

        End If
        _DataTable.Clear()
        dbConn.sqlCmd.Parameters.Clear()
        sqlStr = ""

        sqlStr = "Select '5' as SeqID, 'E' as SeqName, 'L' as Position,Text as 'Title',isnull(sum(CREDIT_AMOUNT),0.00) Amount "
        sqlStr = sqlStr & " From S_LEDGER "
        sqlStr = sqlStr & " where DELFG = 0 And DOC_TYPE = 'VV' And Text Like 'Interest on Security Deposits%' "
        sqlStr = sqlStr & " And SHIP_TO_BRANCH = @SHIP_TO_BRANCH "
        sqlStr = sqlStr & " And MONTH(CONVERT(date, substring(SRC_FILE_NAME,len(SRC_FILE_NAME) -11,8))) =  @Month "
        sqlStr = sqlStr & " And YEAR(CONVERT(date, substring(SRC_FILE_NAME,len(SRC_FILE_NAME) -11,8))) = @Year group by Text"
        dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@SHIP_TO_BRANCH", queryParams.SHIP_TO_BRANCH))
        dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@Month", queryParams.Month))
        dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@Year", queryParams.Year))
        _DataTable = dbConn.GetDataSet(sqlStr)
        If _DataTable.Rows.Count = 0 Then
            ' rsidetbl = 1

            rw = dtAccountDtl.NewRow
            rw("SeqID") = 5
            rw("SeqName") = "E"
            rw("Position") = "L"
            rw("Title") = "Interest on Security Deposits"
            rw("Amount") = 0.00
            LeftTotal += rw("Amount")
            dtAccountDtl.Rows.Add(rw)
        Else
            ' dtAccountDtl.Merge(_DataTable)
            For Each row As DataRow In _DataTable.Rows
                'rsidetbl += 1
                rw = dtAccountDtl.NewRow
                rw("SeqID") = row("SeqID")
                rw("SeqName") = row("SeqName")
                rw("Position") = row("Position")
                rw("Title") = row("Title")
                rw("Amount") = row("Amount")
                LeftTotal += rw("Amount")
                dtAccountDtl.Rows.Add(rw)
            Next row

        End If

        Dim Lcnt = dtAccountDtl.Select("Position='L'").Count()
        Dim Rcnt = dtAccountDtl.Select("Position='R'").Count()

        If Lcnt > Rcnt Then
            For i = 1 To (Lcnt - Rcnt)
                rw = dtAccountDtl.NewRow
                rw("SeqID") = 3
                rw("SeqName") = "H"
                rw("Position") = "R"
                rw("Title") = ""
                'rw("Amount") = DBNull
                dtAccountDtl.Rows.Add(rw)
            Next

        End If
        If Rcnt > Lcnt Then
            For i = 1 To (Rcnt - Lcnt)
                rw = dtAccountDtl.NewRow
                rw("SeqID") = 5
                rw("SeqName") = "E"
                rw("Position") = "L"
                rw("Title") = ""
                ' rw("Amount") = Nothing
                dtAccountDtl.Rows.Add(rw)
            Next
        End If

        rw = dtAccountDtl.NewRow
        rw("SeqID") = 6
        rw("SeqName") = "I"
        rw("Position") = "L"
        rw("Title") = ""
        'rw("Amount") = LeftTotal
        dtAccountDtl.Rows.Add(rw)

        rw = dtAccountDtl.NewRow
        rw("SeqID") = 6
        rw("SeqName") = "J"
        rw("Position") = "R"
        rw("Title") = ""
        'rw("Amount") = RightTotal
        dtAccountDtl.Rows.Add(rw)

        rw = dtAccountDtl.NewRow
        rw("SeqID") = 7
        rw("SeqName") = "K"
        rw("Position") = "L"
        rw("Title") = ""
        rw("Amount") = LeftTotal
        dtAccountDtl.Rows.Add(rw)

        rw = dtAccountDtl.NewRow
        rw("SeqID") = 7
        rw("SeqName") = "L"
        rw("Position") = "R"
        rw("Title") = ""
        rw("Amount") = RightTotal
        dtAccountDtl.Rows.Add(rw)
        'rw = dtAccountDtl.NewRow
        'rw("SeqID") = 5
        'rw("SeqName") = "E"
        'rw("Position") = "L"
        'rw("Title") = ""
        'rw("Amount") = 0.00
        'dtAccountDtl.Rows.Add(rw)


        _DataTable.Clear()
        dbConn.sqlCmd.Parameters.Clear()
        sqlStr = ""
        dbConn.CloseConnection()
        ConAccountDtl = AccountReport(dtAccountDtl, queryParams.Month, queryParams.Year, queryParams.SHIP_TO_BRANCH, ClosingBalance)

        Return ConAccountDtl
    End Function
    Public Function AccountReport(ByVal dtAccountDtl As DataTable, Month As String, Year As String, Branch_Code As String, ClosingBalance As Double) As DataTable
        Log4NetControl.ComInfoLogWrite(Log4NetControl.UserID)
        Dim dtConsolAccountDtl As DataTable = New DataTable()
        dtConsolAccountDtl.Columns.Add("L")
        dtConsolAccountDtl.Columns.Add("LTitle")
        dtConsolAccountDtl.Columns.Add("LAmount")
        dtConsolAccountDtl.Columns.Add("R")
        dtConsolAccountDtl.Columns.Add("RTitle")
        dtConsolAccountDtl.Columns.Add("RAmount")
        Dim LsidefoundRows() As DataRow
        Dim RsidefoundRows() As DataRow
        Dim rw As DataRow
        Dim GssBalance As Double

        Dim Lcnt = dtAccountDtl.Select("Position='L'").Count()
        Dim Rcnt = dtAccountDtl.Select("Position='R'").Count()


        LsidefoundRows = dtAccountDtl.Select("Position='L'", "ID ASC")
        RsidefoundRows = dtAccountDtl.Select("Position='R'", "ID ASC")


        'rw = dtConsolAccountDtl.NewRow
        'rw("LSeqName") = ""
        'rw("LTitle") = ""
        'rw("LAmount") = ""
        'rw("RSeqName") = ""
        'rw("RTitle") = ""
        'rw("RAmount") = ""
        'dtConsolAccountDtl.Rows.Add(rw)

        'Dim MonthName As String = New DateTime(Convert.ToInt16(Year), Convert.ToInt16(Month), 1).ToString("MMM", CultureInfo.InvariantCulture)
        'rw = dtConsolAccountDtl.NewRow
        'rw("LSeqName") = ""
        'rw("LTitle") = Branch_Code + " " + MonthName + "-" + Year + " Accounting Report"
        'rw("LAmount") = ""
        'rw("RSeqName") = ""
        'rw("RTitle") = ""
        'rw("RAmount") = ""
        'dtConsolAccountDtl.Rows.Add(rw)
        'dtAccountDtl.Columns.Add("SeqID", GetType(System.Int16))
        'dtAccountDtl.Columns.Add("SeqName")
        'dtAccountDtl.Columns.Add("Position")
        'dtAccountDtl.Columns.Add("Title")
        'dtAccountDtl.Columns.Add("Amount", GetType(System.Double))
        'DataRow[] LsideRows
        For i = 0 To Lcnt - 1
            rw = dtConsolAccountDtl.NewRow

            rw("L") = ""
            rw("LTitle") = LsidefoundRows(i)("Title")
            rw("LAmount") = String.Format("{0:0.00}", LsidefoundRows(i)("Amount"))
            rw("R") = ""
            rw("RTitle") = RsidefoundRows(i)("Title")
            'rw("RAmount") = RsidefoundRows(i)("Amount")
            rw("RAmount") = String.Format("{0:0.00}", RsidefoundRows(i)("Amount"))
            'String.Format("{0:n4}", dNum)
            dtConsolAccountDtl.Rows.Add(rw)

            If i = Lcnt - 1 Then
                GssBalance = Convert.ToDouble(String.Format("{0:0.00}", LsidefoundRows(i)("Amount"))) - Convert.ToDouble(String.Format("{0:0.00}", RsidefoundRows(i)("Amount")))
            End If

        Next

        rw = dtConsolAccountDtl.NewRow
        rw("L") = ""
        rw("LTitle") = ""
        rw("LAmount") = ""
        rw("R") = ""
        rw("RTitle") = ""
        rw("RAmount") = ""
        dtConsolAccountDtl.Rows.Add(rw)

        rw = dtConsolAccountDtl.NewRow
        rw("L") = ""
        rw("LTitle") = ""
        rw("LAmount") = ""
        rw("R") = ""
        rw("RTitle") = ""
        rw("RAmount") = ""
        dtConsolAccountDtl.Rows.Add(rw)

        Dim currentDate As Date = Year + "/" + Month + "/01"
        Dim lastDayOfMo = Date.DaysInMonth(currentDate.Year, currentDate.Month)

        rw = dtConsolAccountDtl.NewRow
        rw("L") = ""
        rw("LTitle") = "GSS Balance " + Convert.ToString(lastDayOfMo) + "-" + Month + "-" + Year
        rw("LAmount") = String.Format("{0:0.00}", GssBalance)
        rw("R") = ""
        rw("RTitle") = ""
        rw("RAmount") = ""
        dtConsolAccountDtl.Rows.Add(rw)

        rw = dtConsolAccountDtl.NewRow
        rw("L") = ""
        rw("LTitle") = "SIEL Ledger Balance " + Convert.ToString(lastDayOfMo) + "-" + Month + "-" + Year
        rw("LAmount") = String.Format("{0:0.00}", ClosingBalance)
        rw("R") = ""
        rw("RTitle") = ""
        rw("RAmount") = ""
        dtConsolAccountDtl.Rows.Add(rw)

        rw = dtConsolAccountDtl.NewRow
        rw("L") = ""
        rw("LTitle") = "Difference - Labour Invoice TDS (2%)"
        rw("LAmount") = String.Format("{0:0.00}", (GssBalance - ClosingBalance))
        rw("R") = ""
        rw("RTitle") = ""
        rw("RAmount") = ""
        dtConsolAccountDtl.Rows.Add(rw)

        Return dtConsolAccountDtl
    End Function
End Class
