Imports System
Imports System.Data
Imports System.Web.Security
Imports System.Collections.Generic
Imports Ganges33.Ganges33.dao
Imports Ganges33.Ganges33.model
Imports Ganges33.Ganges33.logic
Public Class MoneyReserveControl

    ''' <summary>
    ''' Selecting the records from Served money 
    ''' </summary>
    ''' <param name="queryParams">Search condition</param>
    ''' <returns></returns>
    Public Function SelectStatusRec(ByVal queryParams As MoneyReserveModel) As DataTable
        Log4NetControl.ComInfoLogWrite(Log4NetControl.UserID)
        Dim dbConn As DBUtility = New DBUtility()
        Dim dt As DataTable = New DataTable()
        Dim sqlStr As String = "SELECT "
        sqlStr = sqlStr & "TOP 1 [status], "
        sqlStr = sqlStr & "[youser_name], "
        sqlStr = sqlStr & "[datetime], "
        sqlStr = sqlStr & "[M_2000],[M_1000],[M_500],[M_200],[M_100],[M_50],[M_20],[M_10], "
        sqlStr = sqlStr & "[Coin_10],[Coin_5],[Coin_2],[Coin_1], "
        sqlStr = sqlStr & "[total], "
        sqlStr = sqlStr & "[diff], "
        sqlStr = sqlStr & "[reserve], "
        sqlStr = sqlStr & "[ship_code], "
        sqlStr = sqlStr & "[mistake], "
        sqlStr = sqlStr & "[ip_address], "
        sqlStr = sqlStr & "[conf_user], "
        sqlStr = sqlStr & "[conf_datetime], "
        sqlStr = sqlStr & "[conf_ip] "
        sqlStr = sqlStr & "FROM "
        sqlStr = sqlStr & "T_Reserve "
        sqlStr = sqlStr & "WHERE "
        sqlStr = sqlStr & "DELFG=0 "
        ''''''''''' sqlStr = sqlStr & "AND STATUS='open' "
        If Not String.IsNullOrEmpty(queryParams.ShipCode) Then
            sqlStr = sqlStr & "AND ship_code = @ShipCode "
            dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@ShipCode", queryParams.ShipCode))
        End If
        If Not String.IsNullOrEmpty(queryParams.TDateTime) Then
            sqlStr = sqlStr & "AND (CONVERT(VARCHAR,datetime, 111)) = @TDateTime "
            dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@TDateTime", queryParams.TDateTime))
        End If
        If Not String.IsNullOrEmpty(queryParams.Status) Then
            sqlStr = sqlStr & "AND Status = @Status "
            dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@Status", queryParams.Status))
        End If
        If Not String.IsNullOrEmpty(queryParams.YouserName) Then
            sqlStr = sqlStr & "AND youser_name = @YouserName "
            dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@YouserName", queryParams.YouserName))
        End If

        sqlStr = sqlStr & "ORDER BY datetime DESC "
        dt = dbConn.GetDataSet(sqlStr)
        dbConn.CloseConnection()
        Return dt
    End Function

    Public Function SelectMistakeCount(ByVal queryParams As MoneyReserveModel) As Integer
        Log4NetControl.ComInfoLogWrite(Log4NetControl.UserID)
        Dim dbConn As DBUtility = New DBUtility()
        Dim dt As DataTable = New DataTable()
        Dim sqlStr As String = "SELECT "
        sqlStr = sqlStr & "COUNT(youser_name) as mistakecnt "
        sqlStr = sqlStr & "FROM "
        sqlStr = sqlStr & "T_Reserve "
        sqlStr = sqlStr & "WHERE "
        sqlStr = sqlStr & "DELFG = 0 AND conf_user is not null "
        If Not String.IsNullOrEmpty(queryParams.ShipCode) Then
            sqlStr = sqlStr & "AND ship_code = @ShipCode "
            dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@ShipCode", queryParams.ShipCode))
        End If
        If Not String.IsNullOrEmpty(queryParams.Status) Then
            sqlStr = sqlStr & "AND Status = @Status "
            dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@Status", queryParams.Status))
        End If
        If Not String.IsNullOrEmpty(queryParams.TDateTime) Then
            sqlStr = sqlStr & "AND (CONVERT(VARCHAR,datetime, 111)) = @TDateTime "
            dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@TDateTime", queryParams.TDateTime))
        End If
        If Not String.IsNullOrEmpty(queryParams.YouserName) Then
            sqlStr = sqlStr & "AND youser_name = @YouserName "
            dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@YouserName", queryParams.YouserName))
        End If
        dt = dbConn.GetDataSet(sqlStr)
        dbConn.CloseConnection()
        If String.IsNullOrEmpty(dt.Rows(0)("mistakecnt").ToString()) Then
            Return 0
        Else
            Return dt.Rows(0)("mistakecnt")
        End If
    End Function


    Public Function SelectTReserve(ByVal queryParams As MoneyReserveModel) As DataTable
        Log4NetControl.ComInfoLogWrite(Log4NetControl.UserID)
        Dim dbConn As DBUtility = New DBUtility()
        Dim dt As DataTable = New DataTable()
        Dim sqlStr As String = "SELECT "
        sqlStr = sqlStr & "[status], "
        sqlStr = sqlStr & "[youser_name], "
        sqlStr = sqlStr & "[datetime], "
        sqlStr = sqlStr & "[M_2000],[M_1000],[M_500],[M_200],[M_100],[M_50],[M_20],[M_10], "
        sqlStr = sqlStr & "[Coin_10],[Coin_5],[Coin_2],[Coin_1], "
        sqlStr = sqlStr & "[total], "
        sqlStr = sqlStr & "[diff], "
        sqlStr = sqlStr & "[reserve], "
        sqlStr = sqlStr & "[ship_code], "
        sqlStr = sqlStr & "[mistake], "
        sqlStr = sqlStr & "[ip_address], "
        sqlStr = sqlStr & "[conf_user], "
        sqlStr = sqlStr & "[conf_datetime], "
        sqlStr = sqlStr & "[conf_ip] "
        sqlStr = sqlStr & "FROM "
        sqlStr = sqlStr & "T_Reserve "
        sqlStr = sqlStr & "WHERE "
        sqlStr = sqlStr & "DELFG=0 "
        ''''''''''' sqlStr = sqlStr & "AND STATUS='open' "
        If Not String.IsNullOrEmpty(queryParams.ShipCode) Then
            sqlStr = sqlStr & "AND ship_code = @ShipCode "
            dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@ShipCode", queryParams.ShipCode))
        End If
        If Not String.IsNullOrEmpty(queryParams.TDateTime) Then
            sqlStr = sqlStr & "AND (CONVERT(VARCHAR,datetime, 111)) = @TDateTime "
            dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@TDateTime", queryParams.TDateTime))
        End If
        If Not String.IsNullOrEmpty(queryParams.Status) Then
            sqlStr = sqlStr & "AND Status = @Status "
            dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@Status", queryParams.Status))
        End If
        If Not String.IsNullOrEmpty(queryParams.UserName) Then
            sqlStr = sqlStr & "AND youser_name = @UserName "
            dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@UserName", queryParams.UserName))
        End If

        sqlStr = sqlStr & "ORDER BY datetime ASC "
        dt = dbConn.GetDataSet(sqlStr)
        dbConn.CloseConnection()
        Return dt
    End Function

    Public Function SelectTReserveBR(ByVal queryParams As MoneyReserveModel) As DataTable
        Log4NetControl.ComInfoLogWrite(Log4NetControl.UserID)
        Dim dbConn As DBUtility = New DBUtility()
        Dim dt As DataTable = New DataTable()
        Dim sqlStr As String = "SELECT "
        sqlStr = sqlStr & "[status], "
        sqlStr = sqlStr & "[youser_name], "
        sqlStr = sqlStr & "[datetime], "
        sqlStr = sqlStr & "[M_2000],[M_1000],[M_500],[M_200],[M_100],[M_50],[M_20],[M_10], "
        sqlStr = sqlStr & "[Coin_10],[Coin_5],[Coin_2],[Coin_1], "
        sqlStr = sqlStr & "[total], "
        sqlStr = sqlStr & "[diff], "
        sqlStr = sqlStr & "[reserve], "
        sqlStr = sqlStr & "[ship_code], "
        sqlStr = sqlStr & "[mistake], "
        sqlStr = sqlStr & "[ip_address], "
        sqlStr = sqlStr & "[conf_user], "
        sqlStr = sqlStr & "[conf_datetime], "
        sqlStr = sqlStr & "[conf_ip] "
        sqlStr = sqlStr & "FROM "
        sqlStr = sqlStr & "T_Reserve "
        sqlStr = sqlStr & "WHERE "
        sqlStr = sqlStr & "DELFG=0 and conf_user is not null "
        ''''''''''' sqlStr = sqlStr & "AND STATUS='open' "
        If Not String.IsNullOrEmpty(queryParams.ShipCode) Then
            sqlStr = sqlStr & "AND ship_code = @ShipCode "
            dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@ShipCode", queryParams.ShipCode))
        End If
        If Not String.IsNullOrEmpty(queryParams.TDateTime) Then
            sqlStr = sqlStr & "AND (CONVERT(VARCHAR,datetime, 111)) = @TDateTime "
            dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@TDateTime", queryParams.TDateTime))
        End If
        If Not String.IsNullOrEmpty(queryParams.Status) Then
            sqlStr = sqlStr & "AND Status = @Status "
            dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@Status", queryParams.Status))
        End If
        If Not String.IsNullOrEmpty(queryParams.UserName) Then
            sqlStr = sqlStr & "AND youser_name = @UserName "
            dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@UserName", queryParams.UserName))
        End If

        sqlStr = sqlStr & "ORDER BY unq desc "
        dt = dbConn.GetDataSet(sqlStr)
        dbConn.CloseConnection()
        Return dt
    End Function
    ''' <summary>
    ''' Check service center is open or not
    ''' </summary>
    ''' <param name="queryParams"></param>
    ''' <returns></returns>
    Public Function CheckOpen(ByVal queryParams As MoneyReserveModel) As Boolean
        Log4NetControl.ComInfoLogWrite(Log4NetControl.UserID)
        Dim dbConn As DBUtility = New DBUtility()
        Dim dt As DataTable = New DataTable()
        Dim sqlStr As String = "SELECT "
        sqlStr = sqlStr & "open_time "
        sqlStr = sqlStr & "FROM "
        sqlStr = sqlStr & "M_ship_base "
        sqlStr = sqlStr & "WHERE "
        If Not String.IsNullOrEmpty(queryParams.ShipCode) Then
            sqlStr = sqlStr & "ship_code = @ShipCode "
            dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@ShipCode", queryParams.ShipCode))
        End If
        dt = dbConn.GetDataSet(sqlStr)
        dbConn.CloseConnection()
        If dt.Rows(0)("open_time").ToString() = "True" Then
            Return True
        Else
            Return False
        End If
    End Function
    Public Function SelectShipBase(ByVal queryParams As String) As DataTable
        Log4NetControl.ComInfoLogWrite(Log4NetControl.UserID)
        Dim dbConn As DBUtility = New DBUtility()
        Dim dt As DataTable = New DataTable()
        Dim sqlStr As String = "SELECT "
        sqlStr = sqlStr & "regi_deposit, "
        sqlStr = sqlStr & "open_time "
        sqlStr = sqlStr & "FROM "
        sqlStr = sqlStr & "M_ship_base "
        sqlStr = sqlStr & "WHERE "
        If Not String.IsNullOrEmpty(queryParams) Then
            sqlStr = sqlStr & "ship_code = @ShipCode "
            dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@ShipCode", queryParams))
        End If
        dt = dbConn.GetDataSet(sqlStr)
        dbConn.CloseConnection()
        Return dt
    End Function

    Public Function UpdateConfirmUser(ByVal queryParams As MoneyReserveModel) As Boolean
        Log4NetControl.ComInfoLogWrite(Log4NetControl.UserID)

        'Dim DateTimeNow As DateTime = DateTime.Now
        'Dim dtNow As DateTime = DateTimeNow.AddMinutes(ConfigurationManager.AppSettings("TimeDiff"))
        'Dim DateTimeNow As DateTime = DateTime.Now
        'Dim dtNow As DateTime = DateTimeNow.AddMinutes(ConfigurationManager.AppSettings("TimeDiff"))

        Dim dbConn As DBUtility = New DBUtility()
        Dim dt As DataTable = New DataTable()
        Dim sqlStr As String = "SELECT "
        dbConn.sqlTrn = dbConn.sqlConn.BeginTransaction()
        dbConn.sqlCmd.Transaction = dbConn.sqlTrn

        sqlStr = sqlStr & "TOP 1 [unq] "
        sqlStr = sqlStr & "FROM "
        sqlStr = sqlStr & "T_Reserve "
        sqlStr = sqlStr & "WHERE "
        sqlStr = sqlStr & "DELFG=0 "
        If Not String.IsNullOrEmpty(queryParams.ShipCode) Then
            sqlStr = sqlStr & "AND ship_code = @ShipCode "
            dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@ShipCode", queryParams.ShipCode))
        End If
        If Not String.IsNullOrEmpty(queryParams.Status) Then
            sqlStr = sqlStr & "AND Status = @Status "
            dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@Status", queryParams.Status))
        End If
        If Not String.IsNullOrEmpty(queryParams.UserId) Then
            sqlStr = sqlStr & "AND CRTCD = @UserId "
            dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@UserId", queryParams.UserId))
        End If
        sqlStr = sqlStr & " AND (conf_user = '' OR conf_user IS NULL) "
        sqlStr = sqlStr & "ORDER BY datetime DESC "
        dt = dbConn.GetDataSet(sqlStr)
        If (dt.Rows.Count > 0) Then
            ' If dt.Rows(0)("unq").ToString() = "" Then
            'Update 
            sqlStr = "Update "
            sqlStr = sqlStr & "T_Reserve "
            sqlStr = sqlStr & "set "
            sqlStr = sqlStr & "UPDDT = @ConfDateTime, "
            sqlStr = sqlStr & "conf_datetime = @ConfDateTime, "
            dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@ConfDateTime", queryParams.ConfDateTime))
            If Not String.IsNullOrEmpty(queryParams.UserId) Then
                sqlStr = sqlStr & "UPDCD = @UserId, "
                'dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@UserId", queryParams.UserId))
            End If
            If Not String.IsNullOrEmpty(queryParams.ConfUser) Then
                sqlStr = sqlStr & "conf_user = @ConfUser, "
                dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@ConfUser", queryParams.ConfUser))
            End If
            If Not String.IsNullOrEmpty(queryParams.ConfIpAddress) Then
                sqlStr = sqlStr & "conf_ip = @ConfIpAddress "
                dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@ConfIpAddress", queryParams.ConfIpAddress))
            End If
            sqlStr = sqlStr & "WHERE "
            sqlStr = sqlStr & "unq = " & dt.Rows(0)("unq").ToString()
            Dim flag As Boolean = dbConn.ExecSQL(sqlStr)
            If flag Then
                'dbConn.sqlTrn.Commit()
            Else
                flag = False
                dbConn.sqlTrn.Rollback()
                Return flag
                Exit Function
            End If
            dbConn.sqlCmd.Parameters.Clear()
            'Update Closing Date
            sqlStr = ""
            If queryParams.Status = CommonConst.MONEY_STATUS1 Then 'If Open
                sqlStr = sqlStr & "UPDATE M_ship_base set "
                sqlStr = sqlStr & "opening_date = @OpeningDate, open_time='True' "
                sqlStr = sqlStr & " Where ship_code = @ShipCode"
                If Not String.IsNullOrEmpty(queryParams.ConfDateTime) Then
                    dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@OpeningDate", queryParams.ConfDateTime))
                End If
                If Not String.IsNullOrEmpty(queryParams.ShipCode) Then
                    dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@ShipCode", queryParams.ShipCode))
                End If
                flag = dbConn.ExecSQL(sqlStr)
            ElseIf queryParams.Status = CommonConst.MONEY_STATUS5 Then 'If Close
                sqlStr = sqlStr & "UPDATE M_ship_base set "
                'sqlStr = sqlStr & "closing_date = @ClosingDate, open_time='False' " 'Disable due to Previous Day closing purpose
                sqlStr = sqlStr & "closing_date = opening_date, open_time='False' "
                sqlStr = sqlStr & " Where ship_code = @ShipCode"
                'If Not String.IsNullOrEmpty(queryParams.ShipCode) Then 'Disable due to Previous Day closing purpose
                '    dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@ClosingDate", queryParams.ConfDateTime))
                'End If
                If Not String.IsNullOrEmpty(queryParams.ShipCode) Then
                    dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@ShipCode", queryParams.ShipCode))
                End If
                flag = dbConn.ExecSQL(sqlStr)
            End If



            If flag Then
                dbConn.sqlTrn.Commit()
            Else
                flag = False
                dbConn.sqlTrn.Rollback()
            End If
            dbConn.CloseConnection()
            If flag Then
                Return True
            Else
                Return False
            End If
        End If
        Return False

    End Function

    ''' <summary>
    ''' Update Open or Close time
    ''' </summary>
    ''' <param name="queryParams"></param>
    ''' <returns></returns>
    Public Function UpdateOpen(ByVal queryParams As MoneyReserveModel) As Boolean
        Log4NetControl.ComInfoLogWrite(Log4NetControl.UserID)
        Dim dbConn As DBUtility = New DBUtility()
        Dim dt As DataTable = New DataTable()
        Dim sqlStr As String = "UPDATE "
        sqlStr = sqlStr & "M_ship_base "
        sqlStr = sqlStr & "set "

        sqlStr = sqlStr & "UPDDT = @ConfDateTime, "
        dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@ConfDateTime", queryParams.ConfDateTime))
        If Not String.IsNullOrEmpty(queryParams.Status) Then
            If queryParams.Status = CommonConst.MONEY_STATUS1 Then
                sqlStr = sqlStr & "open_time = 'True', "
            ElseIf queryParams.Status = CommonConst.MONEY_STATUS5 Then
                sqlStr = sqlStr & "open_time = 'false', "
                If Not String.IsNullOrEmpty(queryParams.RegiDeposit) Then
                    sqlStr = sqlStr & "regi_deposit = @RegiDeposit, "
                    dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@RegiDeposit", queryParams.RegiDeposit))
                End If
            End If
        End If

        If Not String.IsNullOrEmpty(queryParams.UserId) Then
            sqlStr = sqlStr & "UPDCD = @UserId "
            dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@UserId", queryParams.UserId))
        End If

        sqlStr = sqlStr & "WHERE "
        sqlStr = sqlStr & "DELFG=0 "
        If Not String.IsNullOrEmpty(queryParams.ShipCode) Then
            sqlStr = sqlStr & "AND ship_code = @ShipCode "
            dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@ShipCode", queryParams.ShipCode))
        End If
        Dim flag As Boolean = dbConn.ExecSQL(sqlStr)
        dbConn.CloseConnection()
        If flag Then
            Return True
        Else
            Return False
        End If
    End Function

    Public Function SelectSurname(ByVal queryParams As MoneyReserveModel) As String
        Log4NetControl.ComInfoLogWrite(Log4NetControl.UserID)
        Dim dbConn As DBUtility = New DBUtility()
        Dim dt As DataTable = New DataTable()
        Dim sqlStr As String = "SELECT "
        sqlStr = sqlStr & "surname "
        sqlStr = sqlStr & "FROM "
        sqlStr = sqlStr & "M_USER_data "
        sqlStr = sqlStr & "WHERE "
        sqlStr = sqlStr & "DELFG=0 "
        If Not String.IsNullOrEmpty(queryParams.UserId) Then
            sqlStr = sqlStr & "AND user_id = @user_id "
            dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@user_id", queryParams.UserId))
        End If
        dt = dbConn.GetDataSet(sqlStr)
        dbConn.CloseConnection()
        Return dt.Rows(0)("surname").ToString()
    End Function



    Public Function SelectEmail(ByVal queryParams As MoneyReserveModel) As String
        Log4NetControl.ComInfoLogWrite(Log4NetControl.UserID)
        Dim dbConn As DBUtility = New DBUtility()
        Dim dt As DataTable = New DataTable()
        Dim sqlStr As String = "SELECT "
        sqlStr = sqlStr & "e_mail "
        sqlStr = sqlStr & "FROM "
        sqlStr = sqlStr & "M_USER_data "
        sqlStr = sqlStr & "WHERE "
        sqlStr = sqlStr & "DELFG=0 "
        If Not String.IsNullOrEmpty(queryParams.CRTCD) Then
            sqlStr = sqlStr & "AND user_id = @user_id "
            dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@user_id", queryParams.CRTCD))
        End If
        dt = dbConn.GetDataSet(sqlStr)
        dbConn.CloseConnection()
        Return dt.Rows(0)("e_mail").ToString()
    End Function


    ''' <summary>
    ''' Time comparison 
    ''' </summary>
    ''' <param name="queryParams"></param>
    ''' <returns></returns>
    Public Function SelectMShipBase(ByVal queryParams As String) As DataTable
        Log4NetControl.ComInfoLogWrite(Log4NetControl.UserID)
        Dim _ShipBaseModel As ShipBaseModel = New ShipBaseModel()
        Dim dbConn As DBUtility = New DBUtility()
        Dim dt As DataTable = New DataTable()
        Dim codeMaster As List(Of CodeMasterModel) = New List(Of CodeMasterModel)()
        Dim sqlStr As String = "SELECT "
        sqlStr = sqlStr & "open_start,open_end,inspection1_start,inspection1_end,inspection2_start,inspection2_end,inspection3_start,inspection3_end, "
        sqlStr = sqlStr & "opening_date, "
        sqlStr = sqlStr & "closing_date, "
        sqlStr = sqlStr & "open_time, "
        sqlStr = sqlStr & "close_time, "
        sqlStr = sqlStr & "regi_deposit, "
        sqlStr = sqlStr & "ship_service "
        sqlStr = sqlStr & "FROM "
        sqlStr = sqlStr & "M_ship_base "
        sqlStr = sqlStr & "WHERE "
        sqlStr = sqlStr & "DELFG = 0 "
        If Not String.IsNullOrEmpty(queryParams) Then
            sqlStr = sqlStr & "AND ship_code = @ShipCode "
            dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@ShipCode", queryParams))
        End If
        Dim _DataTable As DataTable = dbConn.GetDataSet(sqlStr)
        dbConn.CloseConnection()
        Return _DataTable
    End Function


    Public Function InsertTReserve(ByVal queryParams As MoneyReserveModel) As Boolean
        Log4NetControl.ComInfoLogWrite(Log4NetControl.UserID)
        Dim dbConn As DBUtility = New DBUtility()
        dbConn.sqlTrn = dbConn.sqlConn.BeginTransaction()
        dbConn.sqlCmd.Transaction = dbConn.sqlTrn
        Dim sqlStr As String = "INSERT INTO T_Reserve ("
        sqlStr = sqlStr & "CRTDT,"
        sqlStr = sqlStr & "CRTCD,"
        sqlStr = sqlStr & "UPDPG, "
        sqlStr = sqlStr & "DELFG, "
        sqlStr = sqlStr & "status, "
        sqlStr = sqlStr & "youser_name, "
        sqlStr = sqlStr & "datetime, "
        sqlStr = sqlStr & "M_2000, "
        sqlStr = sqlStr & "M_500, "
        sqlStr = sqlStr & "M_200, "
        sqlStr = sqlStr & "M_100, "
        sqlStr = sqlStr & "M_50, "
        sqlStr = sqlStr & "M_20, "
        sqlStr = sqlStr & "M_10, "
        sqlStr = sqlStr & "Coin_10, "
        sqlStr = sqlStr & "Coin_5, "
        sqlStr = sqlStr & "Coin_2, "
        sqlStr = sqlStr & "Coin_1, "
        sqlStr = sqlStr & "total, "
        sqlStr = sqlStr & "diff, "
        sqlStr = sqlStr & "reserve, "
        sqlStr = sqlStr & "ship_code, "
        sqlStr = sqlStr & "mistake, "
        sqlStr = sqlStr & "ip_address "
        sqlStr = sqlStr & ")"
        sqlStr = sqlStr & " VALUES ("
        sqlStr = sqlStr & "@CRTDT,"
        sqlStr = sqlStr & "@CRTCD,"
        sqlStr = sqlStr & "@UPDPG, "
        sqlStr = sqlStr & "@DELFG, "
        sqlStr = sqlStr & "@status, "
        sqlStr = sqlStr & "@youser_name, "
        sqlStr = sqlStr & "@datetime, "
        sqlStr = sqlStr & "@M_2000, "
        sqlStr = sqlStr & "@M_500, "
        sqlStr = sqlStr & "@M_200, "
        sqlStr = sqlStr & "@M_100, "
        sqlStr = sqlStr & "@M_50, "
        sqlStr = sqlStr & "@M_20, "
        sqlStr = sqlStr & "@M_10, "
        sqlStr = sqlStr & "@Coin_10, "
        sqlStr = sqlStr & "@Coin_5, "
        sqlStr = sqlStr & "@Coin_2, "
        sqlStr = sqlStr & "@Coin_1, "
        sqlStr = sqlStr & "@total, "
        sqlStr = sqlStr & "@diff, "
        sqlStr = sqlStr & "@reserve, "
        sqlStr = sqlStr & "@ship_code, "
        sqlStr = sqlStr & "@mistake, "
        sqlStr = sqlStr & "@ip_address "
        sqlStr = sqlStr & ")"
        dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@CRTDT", queryParams.CRTDT))
        dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@CRTCD", queryParams.CRTCD))
        dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@UPDPG", queryParams.UPDPG))
        dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@DELFG", queryParams.DELFG))
        dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@status", queryParams.Status))
        dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@youser_name", queryParams.YouserName))
        dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@datetime", queryParams.TDateTime))
        dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@M_2000", queryParams.M2000))
        dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@M_500", queryParams.M500))
        dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@M_200", queryParams.M200))
        dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@M_100", queryParams.M100))
        dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@M_50", queryParams.M50))
        dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@M_20", queryParams.M20))
        dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@M_10", queryParams.M10))
        dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@Coin_10", queryParams.C10))
        dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@Coin_5", queryParams.C5))
        dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@Coin_2", queryParams.C2))
        dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@Coin_1", queryParams.C1))
        dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@total", queryParams.Total))
        dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@diff", queryParams.Diff))
        dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@reserve", queryParams.Reserve))
        dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@ship_code", queryParams.ShipCode))
        dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@mistake", queryParams.Mistake))
        dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@ip_address", queryParams.IpAddress))
        Dim flag As Boolean = dbConn.ExecSQL(sqlStr)
        If flag Then
            dbConn.sqlTrn.Commit()
        Else
            flag = False
            dbConn.sqlTrn.Rollback()
        End If
        dbConn.CloseConnection()

        Return flag

    End Function

    Public Function SelectCashTrackClaim(ByVal queryParams As MoneyReserveModel) As Decimal
        Log4NetControl.ComInfoLogWrite(Log4NetControl.UserID)
        Dim dbConn As DBUtility = New DBUtility()
        Dim dt As DataTable = New DataTable()
        Dim sqlStr As String = "SELECT "
        sqlStr = sqlStr & "SUM(claim) AS amount "
        sqlStr = sqlStr & "FROM "
        sqlStr = sqlStr & "cash_track "
        sqlStr = sqlStr & "WHERE "
        sqlStr = sqlStr & "DELFG=0 "
        sqlStr = sqlStr & "AND payment = 'Cash' AND Warranty not in ('deposit') "
        If Not String.IsNullOrEmpty(queryParams.ShipCode) Then
            sqlStr = sqlStr & "AND location = @ShipCode "
            dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@ShipCode", queryParams.ShipCode))
        End If
        If Not String.IsNullOrEmpty(queryParams.TDateTime) Then
            sqlStr = sqlStr & "AND (CONVERT(VARCHAR,invoice_date, 111)) = @TDateTime "
            dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@TDateTime", queryParams.TDateTime))
        End If
        sqlStr = sqlStr & " AND FALSE = '0' "
        dt = dbConn.GetDataSet(sqlStr)
        dbConn.CloseConnection()
        If String.IsNullOrEmpty(dt.Rows(0)("amount").ToString()) Then
            Return 0.00
        Else
            Return dt.Rows(0)("amount")
        End If
    End Function

    Public Function SelectCashTrackBankDeposit(ByVal queryParams As MoneyReserveModel) As Decimal
        Log4NetControl.ComInfoLogWrite(Log4NetControl.UserID)
        Dim dbConn As DBUtility = New DBUtility()
        Dim dt As DataTable = New DataTable()
        Dim sqlStr As String = "SELECT "
        sqlStr = sqlStr & "SUM(claim) AS amount "
        sqlStr = sqlStr & "FROM "
        sqlStr = sqlStr & "cash_track "
        sqlStr = sqlStr & "WHERE "
        sqlStr = sqlStr & "DELFG=0 "
        sqlStr = sqlStr & "AND payment = 'Cash' "
        sqlStr = sqlStr & "AND Warranty = 'deposit' "
        If Not String.IsNullOrEmpty(queryParams.ShipCode) Then
            sqlStr = sqlStr & "AND location = @ShipCode "
            dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@ShipCode", queryParams.ShipCode))
        End If
        If Not String.IsNullOrEmpty(queryParams.TDateTime) Then
            sqlStr = sqlStr & "AND (CONVERT(VARCHAR,invoice_date, 111)) = @TDateTime "
            dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@TDateTime", queryParams.TDateTime))
        End If
        sqlStr = sqlStr & " AND FALSE = '0' "
        dt = dbConn.GetDataSet(sqlStr)
        dbConn.CloseConnection()
        If String.IsNullOrEmpty(dt.Rows(0)("amount").ToString()) Then
            Return 0.00
        Else
            Return dt.Rows(0)("amount")
        End If
    End Function

End Class
