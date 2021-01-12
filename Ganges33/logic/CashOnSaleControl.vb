Imports System
Imports System.Data
Imports System.Web.Security
Imports System.Collections.Generic
Imports Ganges33.Ganges33.dao
Imports Ganges33.Ganges33.model
Imports Ganges33.Ganges33.logic

Public Class CashOnSaleControl

    Public Function SelectCashOnSalePending(ByVal Optional QryFlag As Int16 = 0, ByVal Optional queryParam As String = "0", ByVal Optional orderBy As String = "ASC") As List(Of CodeMasterModel)
        Log4NetControl.ComInfoLogWrite(Log4NetControl.UserID)
        Dim dbConn As DBUtility = New DBUtility()
        Dim dt As DataTable = New DataTable()
        Dim codeMaster As List(Of CodeMasterModel) = New List(Of CodeMasterModel)()
        Dim sqlStr As String = "SELECT claim_no,location from cash_track "
        sqlStr = sqlStr & "WHERE DELFG = 0 AND Warranty='OOW' AND payment='Cash' AND incomplete='1' AND (corp_flg='0' OR corp_flg is null) "
        If QryFlag = 1 Then
            sqlStr = sqlStr & "AND  claim_no IN ( " & queryParam & ") order by claim_no " & orderBy
        ElseIf QryFlag = 2 Then
            sqlStr = sqlStr & " order by claim_no " & orderBy
        End If
        '   Could not add parameter, the datas are pulling from database. Can use directly with query
        '   dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@branch", queryParam))
        dt = dbConn.GetDataSet(sqlStr)
        dbConn.CloseConnection()
        For Each row As DataRow In dt.Rows
            Dim cm As CodeMasterModel = New CodeMasterModel()
            cm.CodeDispValue = row("claim_no").ToString()
            cm.CodeValue = row("location").ToString()
            codeMaster.Add(cm)
        Next
        Return codeMaster
    End Function



    Public Function AddCashOnSale(queryParams As CashOnSaleModel) As Boolean
        Dim DateTimeNow As DateTime = DateTime.Now
        Dim dtNow As DateTime = DateTimeNow.AddMinutes(ConfigurationManager.AppSettings("TimeDiff"))
        Dim sqlStr As String = ""
        Dim flag As Boolean = True

        Dim dbConn As DBUtility = New DBUtility()
        Dim dt As DataTable = New DataTable()
        dbConn.sqlTrn = dbConn.sqlConn.BeginTransaction()
        dbConn.sqlCmd.Transaction = dbConn.sqlTrn

        sqlStr = "UPDATE cash_track "
        sqlStr = sqlStr & "SET "
        sqlStr = sqlStr & "corp_flg= @CorpFlg,"
        sqlStr = sqlStr & "corp_collect=@CorpCollect, "
        sqlStr = sqlStr & "corp_PO=@CorpPo, "
        sqlStr = sqlStr & "corp_number=@CorpNumber, "
        ' sqlStr = sqlStr & "edit_log=@EditLog, "
        sqlStr = sqlStr & "invoicedate=@InvoiceDate, "
        sqlStr = sqlStr & "payment_date=@PaymentDate "
        sqlStr = sqlStr & "where claim_no=@ClaimNo "
        sqlStr = sqlStr & "AND location=@location "

        dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@CorpFlg", queryParams.CorpFlg))
        dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@CorpCollect", queryParams.CorpCollect))
        dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@CorpPo", queryParams.CorpPo))
        dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@CorpNumber", queryParams.CorpNumber))
        '   dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@EditLog", dtNow))
        dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@InvoiceDate", queryParams.InvoiceDate))
        dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@PaymentDate", queryParams.InvoiceDate))
        dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@ClaimNo", queryParams.ClaimNo))
        dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@location", queryParams.Location))

        flag = dbConn.ExecSQL(sqlStr)
        dbConn.sqlCmd.Parameters.Clear()

        If flag Then
            dbConn.sqlTrn.Commit()
        Else
            dbConn.sqlTrn.Rollback()
        End If
        dbConn.CloseConnection()

        Return flag
    End Function


    Public Function SelectClaimNoCheck(ByVal queryParams As CashOnSaleModel) As String
        Log4NetControl.ComInfoLogWrite(Log4NetControl.UserID)
        Dim dbConn As DBUtility = New DBUtility()
        Dim dt As DataTable = New DataTable()
        Dim sqlStr As String = "SELECT "
        sqlStr = sqlStr & "COUNT(*) AS CashOnSaleExistCnt "
        sqlStr = sqlStr & "FROM "
        sqlStr = sqlStr & "cash_track "
        sqlStr = sqlStr & "WHERE "
        sqlStr = sqlStr & "DELFG=0 AND Warranty='OOW' AND payment='Cash' AND incomplete='1' "
        If Not String.IsNullOrEmpty(queryParams.ClaimNo) Then
            sqlStr = sqlStr & "AND claim_no = @ClaimNo "
            dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@ClaimNo", queryParams.ClaimNo))
        End If
        dt = dbConn.GetDataSet(sqlStr)
        dbConn.CloseConnection()
        Return dt.Rows(0)("CashOnSaleExistCnt").ToString()
    End Function


    Public Function SelectClaimDetails(ByVal queryParams As CashOnSaleModel) As DataTable
        Log4NetControl.ComInfoLogWrite(Log4NetControl.UserID)
        Dim dbConn As DBUtility = New DBUtility()
        Dim sqlStr As String = "SELECT "
        sqlStr = sqlStr & "claim_no, "
        sqlStr = sqlStr & "corp_number, "
        sqlStr = sqlStr & "(select corp_name from  M_CORP where corp_number=cash_track.corp_number) as corp_name, "
        sqlStr = sqlStr & "corp_collect "
        sqlStr = sqlStr & "from "
        sqlStr = sqlStr & "cash_track "
        sqlStr = sqlStr & "WHERE "
        sqlStr = sqlStr & "DELFG=0 "
        sqlStr = sqlStr & "AND Warranty='OOW' AND payment='Cash' AND incomplete='1' "

        If Not String.IsNullOrEmpty(queryParams.ClaimNo) Then
            sqlStr = sqlStr & "AND claim_no = @ClaimNo "
            dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@ClaimNo", queryParams.ClaimNo))
        End If
        Dim _DataTable As DataTable = dbConn.GetDataSet(sqlStr)
        dbConn.CloseConnection()
        Return _DataTable
    End Function


    Public Function UpdateCashOnSale(queryParams As CashOnSaleModel) As Boolean
        Dim DateTimeNow As DateTime = DateTime.Now
        Dim dtNow As DateTime = DateTimeNow.AddMinutes(ConfigurationManager.AppSettings("TimeDiff"))
        Dim sqlStr As String = ""
        Dim flag As Boolean = True

        Dim dbConn As DBUtility = New DBUtility()
        Dim dt As DataTable = New DataTable()
        dbConn.sqlTrn = dbConn.sqlConn.BeginTransaction()
        dbConn.sqlCmd.Transaction = dbConn.sqlTrn

        sqlStr = "UPDATE cash_track "
        sqlStr = sqlStr & "SET "
        ' sqlStr = sqlStr & "corp_flg= @CorpFlg,"
        If Not String.IsNullOrEmpty(queryParams.CorpCollects) Then 'Only When from No to Yes
            sqlStr = sqlStr & "corp_collect=@CorpCollect, "
            dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@CorpCollect", queryParams.CorpCollect))
        End If
        'sqlStr = sqlStr & "corp_PO=@CorpPo, "
        sqlStr = sqlStr & "corp_number=@CorpNumber, "
        sqlStr = sqlStr & "edit_log=@EditLog "
        ' sqlStr = sqlStr & "invoicedate=@InvoiceDate, "
        'sqlStr = sqlStr & "payment_date=@PaymentDate "
        sqlStr = sqlStr & "where claim_no=@ClaimNo "
        '  sqlStr = sqlStr & "AND location=@location "

        '       dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@CorpFlg", queryParams.CorpFlg))
        '       dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@CorpCollect", queryParams.CorpCollect))
        '       dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@CorpPo", queryParams.CorpPo))
        dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@CorpNumber", queryParams.CorpNumber))
        dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@EditLog", queryParams.EditLog))
        ''       dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@InvoiceDate", queryParams.InvoiceDate))
        '   dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@PaymentDate", dtNow))
        dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@ClaimNo", queryParams.ClaimNo))
        '        dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@location", queryParams.Location))

        flag = dbConn.ExecSQL(sqlStr)
        dbConn.sqlCmd.Parameters.Clear()

        If flag Then
            dbConn.sqlTrn.Commit()
        Else
            dbConn.sqlTrn.Rollback()
        End If
        dbConn.CloseConnection()

        Return flag
    End Function

    Public Function SelectCashOnSearchCheck(ByVal queryParams As CashOnSaleModel) As String
        Log4NetControl.ComInfoLogWrite(Log4NetControl.UserID)
        Dim dbConn As DBUtility = New DBUtility()
        Dim dt As DataTable = New DataTable()
        Dim sqlStr As String = "SELECT "
        sqlStr = sqlStr & "COUNT(*) AS CashOnSaleSearchExistCnt "
        sqlStr = sqlStr & "FROM "
        sqlStr = sqlStr & "cash_track "
        sqlStr = sqlStr & "WHERE "
        'sqlStr = sqlStr & "DELFG=0 AND Warranty='OOW' AND payment='Cash' AND incomplete='0' "
        sqlStr = sqlStr & "DELFG=0 AND Warranty='OOW' AND payment='Cash' AND incomplete='1' "
        If Not String.IsNullOrEmpty(queryParams.ClaimNo) Then
            sqlStr = sqlStr & "AND claim_no = @ClaimNo "
            dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@ClaimNo", queryParams.ClaimNo))
        End If
        If Not String.IsNullOrEmpty(queryParams.TotalAmt) Then
            If queryParams.TotalAmt > 0 Then
                sqlStr = sqlStr & "AND total_amount = @TotalAmt "
                dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@TotalAmt", queryParams.TotalAmt))
            End If
        End If
        If Not String.IsNullOrEmpty(queryParams.CorpNumbers) Then
            sqlStr = sqlStr & "AND corp_number in " & queryParams.CorpNumbers & " "
            'dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@CorpNumbers", queryParams.CorpNumbers))
        End If
        If Not String.IsNullOrEmpty(queryParams.CorpCollects) Then
            If Not String.IsNullOrEmpty(queryParams.CorpCollect) Then
                sqlStr = sqlStr & "AND corp_collect = @CorpCollect "
                dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@CorpCollect", queryParams.CorpCollect))
            End If
        End If
        If Not String.IsNullOrEmpty(queryParams.CustomerName) Then
            sqlStr = sqlStr & "AND customer_name = @CustomerName "
            dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@CustomerName", queryParams.CustomerName))
        End If

        If (Not (String.IsNullOrEmpty(queryParams.DateFrom)) And (Not (String.IsNullOrEmpty(queryParams.DateTo)))) Then
            sqlStr = sqlStr & "AND LEFT(CONVERT(VARCHAR, invoice_date, 111), 10) >= @DateFrom and LEFT(CONVERT(VARCHAR, invoice_date, 111), 10) <= @DateTo "
            dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@DateFrom", queryParams.DateFrom))
            dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@DateTo", queryParams.DateTo))
        ElseIf Not String.IsNullOrEmpty(queryParams.DateFrom) Then
            sqlStr = sqlStr & "AND LEFT(CONVERT(VARCHAR, invoice_date, 111), 10) = @DateFrom "
            dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@DateFrom", queryParams.DateFrom))
        ElseIf Not String.IsNullOrEmpty(queryParams.DateTo) Then
            sqlStr = sqlStr & "AND LEFT(CONVERT(VARCHAR, invoice_date, 111), 10) = @DateTo "
            dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@DateTo", queryParams.DateTo))
        End If


        dt = dbConn.GetDataSet(sqlStr)
        dbConn.CloseConnection()
        Return dt.Rows(0)("CashOnSaleSearchExistCnt").ToString()
    End Function

    Public Function SelectCashOnSearchInfo(ByVal queryParams As CashOnSaleModel) As DataTable
        Log4NetControl.ComInfoLogWrite(Log4NetControl.UserID)
        Dim dbConn As DBUtility = New DBUtility()
        Dim dt As DataTable = New DataTable()
        Dim sqlStr As String = "SELECT "
        sqlStr = sqlStr & "corp_number,LEFT(CONVERT(VARCHAR, invoice_date, 111), 10) as invoice_date,claim_no,(select corp_name from  M_CORP where corp_number=cash_track.corp_number) as corp_name,customer_name,total_amount,CASE WHEN corp_collect = 1 THEN 'Yes' ELSE 'No' END AS corp_collect,invoicedate "
        sqlStr = sqlStr & "FROM "
        sqlStr = sqlStr & "cash_track "
        sqlStr = sqlStr & "WHERE "
        'sqlStr = sqlStr & "DELFG=0 AND Warranty='OOW' AND payment='Cash' AND incomplete='0' "
        sqlStr = sqlStr & "DELFG=0 AND Warranty='OOW' AND payment='Cash' AND incomplete='1' "
        If Not String.IsNullOrEmpty(queryParams.ClaimNo) Then
            sqlStr = sqlStr & "AND claim_no = @ClaimNo "
            dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@ClaimNo", queryParams.ClaimNo))
        End If
        If Not String.IsNullOrEmpty(queryParams.TotalAmt) Then
            If queryParams.TotalAmt > 0 Then
                sqlStr = sqlStr & "AND total_amount = @TotalAmt "
                dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@TotalAmt", queryParams.TotalAmt))
            End If
        End If
        If Not String.IsNullOrEmpty(queryParams.CorpNumbers) Then
            sqlStr = sqlStr & "AND corp_number in " & queryParams.CorpNumbers & " "
            'dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@CorpNumbers", queryParams.CorpNumbers))
        End If
        If Not String.IsNullOrEmpty(queryParams.CorpCollects) Then
            If Not String.IsNullOrEmpty(queryParams.CorpCollect) Then
                sqlStr = sqlStr & "AND corp_collect = @CorpCollect "
                dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@CorpCollect", queryParams.CorpCollect))
            End If
        End If

        If Not String.IsNullOrEmpty(queryParams.CustomerName) Then
            sqlStr = sqlStr & "AND customer_name = @CustomerName "
            dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@CustomerName", queryParams.CustomerName))
        End If

        If (Not (String.IsNullOrEmpty(queryParams.DateFrom)) And (Not (String.IsNullOrEmpty(queryParams.DateTo)))) Then
            sqlStr = sqlStr & "AND LEFT(CONVERT(VARCHAR, invoice_date, 111), 10) >= @DateFrom and LEFT(CONVERT(VARCHAR, invoice_date, 111), 10) <= @DateTo "
            dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@DateFrom", queryParams.DateFrom))
            dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@DateTo", queryParams.DateTo))
        ElseIf Not String.IsNullOrEmpty(queryParams.DateFrom) Then
            sqlStr = sqlStr & "AND LEFT(CONVERT(VARCHAR, invoice_date, 111), 10) = @DateFrom "
            dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@DateFrom", queryParams.DateFrom))
        ElseIf Not String.IsNullOrEmpty(queryParams.DateTo) Then
            sqlStr = sqlStr & "AND LEFT(CONVERT(VARCHAR, invoice_date, 111), 10) = @DateTo "
            dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@DateTo", queryParams.DateTo))
        End If

        Dim _DataTable As DataTable = dbConn.GetDataSet(sqlStr)
        dbConn.CloseConnection()
        Return _DataTable
    End Function


End Class
