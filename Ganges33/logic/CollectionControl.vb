Imports System
Imports System.Data
Imports System.Web.Security
Imports System.Collections.Generic
Imports Ganges33.Ganges33.dao
Imports Ganges33.Ganges33.model
Imports Ganges33.Ganges33.logic


Public Class CollectionControl
    Public Function CollectionInsert(ByVal queryParams As CollectionModel) As Boolean

        Log4NetControl.ComInfoLogWrite(Log4NetControl.UserID)
        Dim DateTimeNow As DateTime = DateTime.Now
        Dim dtNow As DateTime = DateTimeNow.AddMinutes(ConfigurationManager.AppSettings("TimeDiff"))

        Dim dbConn As DBUtility = New DBUtility()
        Dim dt As DataTable = New DataTable()
        Dim flag As Boolean = False
        Dim sqlStr As String = "INSERT "
        sqlStr = sqlStr & " INTO "
        sqlStr = sqlStr & "COLLECTION ("
        sqlStr = sqlStr & "CRTDT, "
        sqlStr = sqlStr & "CRTCD, "
        'sqlStr = sqlStr & "UPDDT, "
        'sqlStr = sqlStr & "UPDCD, "
        sqlStr = sqlStr & "UPDPG, "
        sqlStr = sqlStr & "DELFG, "
        'sqlStr = sqlStr & "UPLOAD_USER, "
        'sqlStr = sqlStr & "UPLOAD_DATE, "
        sqlStr = sqlStr & "SHIP_TO_BRANCH_CODE, "
        sqlStr = sqlStr & "SHIP_TO_BRANCH, "
        sqlStr = sqlStr & "TARGET_DATE, "
        sqlStr = sqlStr & "DEPOSIT, "
        sqlStr = sqlStr & "SALES "
        'sqlStr = sqlStr & "FILE_NAME "
        'sqlStr = sqlStr & "SRC_FILE_NAME "
        sqlStr = sqlStr & " ) "

        sqlStr = sqlStr & " values ( "
        sqlStr = sqlStr & "@CRTDT, "
        sqlStr = sqlStr & "@CRTCD, "
        'sqlStr = sqlStr & "@UPDDT, "
        'sqlStr = sqlStr & "@UPDCD, "
        sqlStr = sqlStr & "@UPDPG, "
        sqlStr = sqlStr & "@DELFG, "
        'sqlStr = sqlStr & "@UPLOAD_USER, "
        'sqlStr = sqlStr & "@UPLOAD_DATE, "


        sqlStr = sqlStr & "@SHIP_TO_BRANCH_CODE, "
        sqlStr = sqlStr & "@SHIP_TO_BRANCH, "
        sqlStr = sqlStr & "@TARGET_DATE, "
        sqlStr = sqlStr & "@DEPOSIT, "
        sqlStr = sqlStr & "@SALES "
        'sqlStr = sqlStr & "@FILE_NAME "
        'sqlStr = sqlStr & "@SRC_FILE_NAME "
        sqlStr = sqlStr & " )"

        dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@CRTDT", dtNow))
        dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@CRTCD", queryParams.UserId))
        dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@UPDPG", queryParams.UPDPG))
        dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@DELFG", 0))
        dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@SHIP_TO_BRANCH_CODE", queryParams.SHIP_TO_BRANCH_CODE))
        dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@SHIP_TO_BRANCH", queryParams.SHIP_TO_BRANCH))
        dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@TARGET_DATE", queryParams.TARGET_DATE))
        dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@DEPOSIT", queryParams.DEPOSIT))
        dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@SALES", queryParams.SALES))


        flag = dbConn.ExecSQL(sqlStr)
        dbConn.sqlCmd.Parameters.Clear()
        dbConn.CloseConnection()
        Return flag

    End Function
End Class
