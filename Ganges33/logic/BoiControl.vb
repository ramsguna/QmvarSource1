Imports System
Imports System.Data
Imports System.Web.Security
Imports System.Collections.Generic
Imports Ganges33.Ganges33.dao
Imports Ganges33.Ganges33.model
Imports Ganges33.Ganges33.logic
''' <summary>
''' 24.Samsung to GSS paid(BOI)
''' </summary>
Public Class BoiControl
    ''' <summary>
    ''' Adding BOI
    ''' </summary>
    ''' <param name="queryParams"></param>
    ''' <returns></returns>
    Public Function AddBoi(queryParams As BoiModel) As Boolean
        Log4NetControl.ComInfoLogWrite(Log4NetControl.UserID)
        Dim DateTimeNow As DateTime = DateTime.Now
        Dim dtNow As DateTime = DateTimeNow.AddMinutes(ConfigurationManager.AppSettings("TimeDiff"))
        Dim sqlStr As String = ""
        Dim flag As Boolean = True
        Dim dbConn As DBUtility = New DBUtility()
        Dim dt As DataTable = New DataTable()
        dbConn.sqlTrn = dbConn.sqlConn.BeginTransaction()
        dbConn.sqlCmd.Transaction = dbConn.sqlTrn


        sqlStr = "SELECT TOP 1 UNQ_NO FROM BOI "
        sqlStr = sqlStr & "WHERE DELFG=0 "
        sqlStr = sqlStr & "AND AR_NO = @AR_NO "
        sqlStr = sqlStr & "AND SHIP_TO_BRANCH_CODE = @SHIP_TO_BRANCH_CODE "
        'sqlStr = sqlStr & "AND PAYMENT_DATE = @PAYMENT_DATE"
        sqlStr = sqlStr & "AND LEFT(CONVERT(VARCHAR, PAYMENT_DATE, 101), 10) = @PAYMENT_DATE "
        'sqlStr = sqlStr & "AND SHIP_TO_BRANCH_CODE = @SHIP_TO_BRANCH_CODE "
        dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@PAYMENT_DATE", queryParams.PAYMENT_DATE))
        dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@AMOUNT", queryParams.AMOUNT))
        dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@AR_NO", queryParams.AR_NO))
        dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@UPDDT", dtNow))
        dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@UPDCD", queryParams.UserId))
        dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@SHIP_TO_BRANCH_CODE", queryParams.SHIP_TO_BRANCH_CODE))

        dt = dbConn.GetDataSet(sqlStr)
        dbConn.sqlCmd.Parameters.Clear()
        'if exist then need to update delfg=1 then insert the record as new
        If (dt Is Nothing) Or (dt.Rows.Count = 0) Then
            sqlStr = "Insert into BOI ("
            sqlStr = sqlStr & "CRTDT, "
            sqlStr = sqlStr & "CRTCD, "
            sqlStr = sqlStr & "UPDCD, "
            sqlStr = sqlStr & "UPDPG, "
            sqlStr = sqlStr & "DELFG, "
            sqlStr = sqlStr & "SHIP_TO_BRANCH_CODE, "
            sqlStr = sqlStr & "SHIP_TO_BRANCH, "
            sqlStr = sqlStr & "PAYMENT_DATE, "
            sqlStr = sqlStr & "AMOUNT, "
            sqlStr = sqlStr & "DOC_NO, "
            sqlStr = sqlStr & "AR_NO "
            sqlStr = sqlStr & " ) "
            sqlStr = sqlStr & " values ( "
            sqlStr = sqlStr & "@CRTDT, "
            sqlStr = sqlStr & "@CRTCD, "
            sqlStr = sqlStr & "@UPDCD, "
            sqlStr = sqlStr & "@UPDPG, "
            sqlStr = sqlStr & "@DELFG, "
            sqlStr = sqlStr & "@SHIP_TO_BRANCH_CODE, "
            sqlStr = sqlStr & "@SHIP_TO_BRANCH, "
            sqlStr = sqlStr & "@PAYMENT_DATE, "
            sqlStr = sqlStr & "@AMOUNT, "
            sqlStr = sqlStr & "@DOC_NO, "
            sqlStr = sqlStr & "@AR_NO "
            sqlStr = sqlStr & " ) "
            dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@CRTDT", dtNow))
            dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@CRTCD", queryParams.UserId))
            dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@UPDCD", "")) '?????????????????????????
            dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@UPDPG", queryParams.UPDPG))
            dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@DELFG", 0))
            dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@SHIP_TO_BRANCH_CODE", queryParams.SHIP_TO_BRANCH_CODE))
            dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@SHIP_TO_BRANCH", queryParams.SHIP_TO_BRANCH))
            dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@PAYMENT_DATE", queryParams.PAYMENT_DATE))
            dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@AMOUNT", queryParams.AMOUNT))
            dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@DOC_NO", queryParams.DOC_NO))
            dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@Ar_NO", queryParams.AR_NO))

            flag = dbConn.ExecSQL(sqlStr)
            dbConn.sqlCmd.Parameters.Clear()
        Else 'The records is already exist, need to update DELFg=0
            ' isExist = 1
            sqlStr = "UPDATE BOI SET PAYMENT_DATE= @PAYMENT_DATE, AMOUNT=@AMOUNT, UPDDT=@UPDDT, UPDCD=@UPDCD  "
            sqlStr = sqlStr & "WHERE DELFG=0 "
            sqlStr = sqlStr & "AND AR_NO = @AR_NO "
            sqlStr = sqlStr & "AND SHIP_TO_BRANCH_CODE = @SHIP_TO_BRANCH_CODE "
            'sqlStr = sqlStr & "AND PAYMENT_DATE = @PAYMENT_DATE"
            sqlStr = sqlStr & "AND LEFT(CONVERT(VARCHAR, PAYMENT_DATE, 101), 10) = @PAYMENT_DATE "
            'sqlStr = sqlStr & "AND SHIP_TO_BRANCH_CODE = @SHIP_TO_BRANCH_CODE "
            dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@PAYMENT_DATE", queryParams.PAYMENT_DATE))
            dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@AMOUNT", queryParams.AMOUNT))
            dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@AR_NO", queryParams.AR_NO))
            dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@UPDDT", dtNow))
            dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@UPDCD", queryParams.UserId))
            dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@SHIP_TO_BRANCH_CODE", queryParams.SHIP_TO_BRANCH_CODE))

            flag = dbConn.ExecSQL(sqlStr)
            dbConn.sqlCmd.Parameters.Clear()
        End If




        If flag Then
            dbConn.sqlTrn.Commit()
        Else
            dbConn.sqlTrn.Rollback()
        End If
        dbConn.CloseConnection()
        Return flag
    End Function

    Public Function SelectBoi(ByVal queryParams As BoiModel) As DataTable
        Log4NetControl.ComInfoLogWrite(Log4NetControl.UserID)
        Dim dbConn As DBUtility = New DBUtility()
        Dim dt As DataTable = New DataTable()
        Dim sqlStr As String = "SELECT "
        sqlStr = sqlStr & "SHIP_TO_BRANCH as 'Branch',PAYMENT_DATE as 'PAYMENT_DATE',AMOUNT as 'AMOUNT',AR_NO as 'AR_NO'  "
        sqlStr = sqlStr & "FROM "
        sqlStr = sqlStr & "BOI "
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
            sqlStr = sqlStr & "AND LEFT(CONVERT(VARCHAR, PAYMENT_DATE, 111), 10) >= @DateFrom and LEFT(CONVERT(VARCHAR, PAYMENT_DATE, 111), 10) <= @DateTo "
            'sqlStr = sqlStr & "AND INVOICE_DATE >= @DateFrom and INVOICE_DATE <= @DateTo "
            dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@DateFrom", queryParams.DateFrom))
            dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@DateTo", queryParams.DateTo))
        ElseIf Not String.IsNullOrEmpty(queryParams.DateFrom) Then
            'sqlStr = sqlStr & "AND LEFT(CONVERT(VARCHAR, INVOICE_DATE, 111), 10) = @DateFrom "
            sqlStr = sqlStr & "AND PAYMENT_DATE = @DateFrom "
            dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@DateFrom", queryParams.DateFrom))
        ElseIf Not String.IsNullOrEmpty(queryParams.DateTo) Then
            sqlStr = sqlStr & "AND PAYMENT_DATE = @DateTo "
            'sqlStr = sqlStr & "AND LEFT(CONVERT(VARCHAR, INVOICE_DATE, 111), 10) = @DateTo "
            dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@DateTo", queryParams.DateTo))
        End If

        Dim _DataTable As DataTable = dbConn.GetDataSet(sqlStr)
        dbConn.CloseConnection()
        Return _DataTable
    End Function
End Class
