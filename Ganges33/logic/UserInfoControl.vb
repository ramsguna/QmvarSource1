Imports System
Imports System.Data
Imports System.Web.Security
Imports System.Collections.Generic
Imports Ganges33.Ganges33.dao
Imports Ganges33.Ganges33.model
Imports Ganges33.Ganges33.logic
Public Class UserInfoControl
    ''' <summary>
    ''' Get user information
    ''' </summary>
    ''' <param name="queryParams"></param>
    ''' <returns>User information</returns>
    Public Function SelectUserInfo(ByVal queryParams As UserInfoModel) As List(Of UserInfoModel)
        Log4NetControl.ComInfoLogWrite(Log4NetControl.UserID)
        Dim dbConn As DBUtility = New DBUtility()
        Dim sqlStr As String = "SELECT "
        sqlStr = sqlStr & "CRTDT,"
        sqlStr = sqlStr & "CRTCD,"
        sqlStr = sqlStr & "UPDDT,"
        sqlStr = sqlStr & "UPDCD,"
        sqlStr = sqlStr & "UPDPG,"
        sqlStr = sqlStr & "DELFG,"
        sqlStr = sqlStr & "user_id,"
        sqlStr = sqlStr & "password,"
        sqlStr = sqlStr & "eng_id,"
        sqlStr = sqlStr & "last_login,"
        sqlStr = sqlStr & "admin_flg,"
        sqlStr = sqlStr & "user_level,"
        sqlStr = sqlStr & "ship_1,"
        sqlStr = sqlStr & "ship_2,"
        sqlStr = sqlStr & "ship_3,"
        sqlStr = sqlStr & "ship_4,"
        sqlStr = sqlStr & "ship_5 "
        sqlStr = sqlStr & "FROM M_USER "
        sqlStr = sqlStr & "WHERE "
        sqlStr = sqlStr & "DELFG='false' "
        'User id has the value then pass the parameter to the query
        If Not String.IsNullOrEmpty(queryParams.UserId) Then
            sqlStr = sqlStr & "AND user_id = @UserId "
            dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@UserId", queryParams.UserId))
        End If
        'Password exist then pass the value to the query
        If Not String.IsNullOrEmpty(queryParams.Password) Then
            sqlStr = sqlStr & "AND Password = @Password "
            dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@Password", queryParams.Password))
        End If
        'Initialise the object of UserInfoModel
        Dim _UserInfoModel As List(Of UserInfoModel) = New List(Of UserInfoModel)()
        Dim dt As DataTable = New DataTable()
        dt = dbConn.GetDataSet(sqlStr)
        dbConn.CloseConnection()
        If dt Is Nothing Then Return _UserInfoModel
        'Add the information to the object
        For Each row As DataRow In dt.Rows
            Dim userInfo As UserInfoModel = New UserInfoModel()
            userInfo.CRTDT = If(row("CRTDT") Is DBNull.Value, String.Empty, String.Format("{0:yyyy/MM/dd}", Convert.ToDateTime(row("CRTDT")))) 'If(row("CRTDT") = DBNull.Value, String.Empty, String.Format("{0:yyyy/MM/dd}", Convert.ToDateTime(row("CRTDT"))))
            userInfo.CRTCD = row("CRTCD").ToString().Trim()
            userInfo.UPDDT = If(row("UPDDT") Is DBNull.Value, String.Empty, String.Format("{0:yyyy/MM/dd}", Convert.ToDateTime(row("UPDDT"))))
            userInfo.UPDCD = row("UPDCD").ToString().Trim()
            userInfo.UPDPG = row("UPDPG").ToString().Trim()
            userInfo.DELFG = row("DELFG").ToString().Trim()
            userInfo.UserId = row("user_id").ToString().Trim()
            userInfo.Password = row("password").ToString().Trim()
            userInfo.EngId = row("eng_id").ToString().Trim()
            userInfo.LastLogin = row("last_login").ToString().Trim()
            userInfo.AdminFlg = row("admin_flg").ToString().Trim()
            userInfo.UserLevel = row("user_level").ToString().Trim()
            userInfo.Ship1 = row("ship_1").ToString().Trim()
            userInfo.Ship2 = row("ship_2").ToString().Trim()
            userInfo.Ship3 = row("ship_3").ToString().Trim()
            userInfo.Ship4 = row("ship_4").ToString().Trim()
            userInfo.Ship5 = row("ship_5").ToString().Trim()
            _UserInfoModel.Add(userInfo)
        Next
        Return _UserInfoModel
    End Function


End Class
