Imports System
Imports System.Data
Imports System.Web.Security
Imports System.Collections.Generic
Imports Ganges33.Ganges33.dao
Imports Ganges33.Ganges33.model
Imports Ganges33.Ganges33.logic
Public Class PaymentValueControl

    Public Function PaymentValueInsert(ByVal queryParams As PaymentValueModel) As Boolean

        Log4NetControl.ComInfoLogWrite(Log4NetControl.UserID)
        Dim DateTimeNow As DateTime = DateTime.Now
        Dim dtNow As DateTime = DateTimeNow.AddMinutes(ConfigurationManager.AppSettings("TimeDiff"))

        Dim dbConn As DBUtility = New DBUtility()
        Dim dt As DataTable = New DataTable()
        Dim flag As Boolean = False
        Dim sqlStr As String = "INSERT "
        sqlStr = sqlStr & " INTO "
        sqlStr = sqlStr & "PAYMENT_VALUE ("
        sqlStr = sqlStr & "CRTDT, "
        sqlStr = sqlStr & "CRTCD, "
        sqlStr = sqlStr & "UPDPG, "
        sqlStr = sqlStr & "DELFG, "
        sqlStr = sqlStr & "SHIP_TO_BRANCH_CODE, "
        sqlStr = sqlStr & "SHIP_TO_BRANCH, "
        sqlStr = sqlStr & "VALUE, "
        sqlStr = sqlStr & "TARGET_DATE "
        sqlStr = sqlStr & " ) "

        sqlStr = sqlStr & " values ( "
        sqlStr = sqlStr & "@CRTDT, "
        sqlStr = sqlStr & "@CRTCD, "
        sqlStr = sqlStr & "@UPDPG, "
        sqlStr = sqlStr & "@DELFG, "
        sqlStr = sqlStr & "@SHIP_TO_BRANCH_CODE, "
        sqlStr = sqlStr & "@SHIP_TO_BRANCH, "
        sqlStr = sqlStr & "@VALUE, "
        sqlStr = sqlStr & "@TARGET_DATE "
        sqlStr = sqlStr & " )"

        dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@CRTDT", dtNow))
        dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@CRTCD", queryParams.UserId))
        dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@UPDPG", queryParams.UPDPG))
        dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@DELFG", 0))
        dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@SHIP_TO_BRANCH_CODE", queryParams.SHIP_TO_BRANCH_CODE))
        dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@SHIP_TO_BRANCH", queryParams.SHIP_TO_BRANCH))
        dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@VALUE", queryParams.VALUE))
        dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@TARGET_DATE", queryParams.TARGET_DATE))

        flag = dbConn.ExecSQL(sqlStr)
        dbConn.sqlCmd.Parameters.Clear()
        dbConn.CloseConnection()
        Return flag

    End Function


    Public Function ShowPaymentValueGrid() As DataTable

        Log4NetControl.ComInfoLogWrite(Log4NetControl.UserID)
        Dim DateTimeNow As DateTime = DateTime.Now
        Dim dtNow As DateTime = DateTimeNow.AddMinutes(ConfigurationManager.AppSettings("TimeDiff"))

        Dim dbConn As DBUtility = New DBUtility()
        Dim dt As DataTable = New DataTable()
        Dim flag As Boolean = False
        Dim sqlStr As String = "SELECT "
        sqlStr = sqlStr & " top 30 unq_no, crtdt as CREATED_DATE,SHIP_TO_BRANCH as TargetSSC,value as PaymentAmount,FORMAT(Target_Date, 'yyyy/MM/dd') as Target_Date from PAYMENT_VALUE where delfg=0 order by TARGET_DATE desc "
        Dim _DataTable As DataTable = dbConn.GetDataSet(sqlStr)
        dbConn.CloseConnection()
        Return _DataTable

    End Function

    Public Function UpdatePaymentValueFromGrid(ByVal queryParams As PaymentValueModel) As Boolean

        Log4NetControl.ComInfoLogWrite(Log4NetControl.UserID)
        Dim DateTimeNow As DateTime = DateTime.Now
        Dim dtNow As DateTime = DateTimeNow.AddMinutes(ConfigurationManager.AppSettings("TimeDiff"))

        Dim dbConn As DBUtility = New DBUtility()
        Dim dt As DataTable = New DataTable()
        Dim flag As Boolean = False
        Dim sqlStr As String = "UPDATE "
        sqlStr = sqlStr & " PAYMENT_VALUE "
        sqlStr = sqlStr & "SET "
        sqlStr = sqlStr & "CRTDT =@CRTCD, "
        sqlStr = sqlStr & "SHIP_TO_BRANCH=@SHIP_TO_BRANCH, "
        sqlStr = sqlStr & "VALUE=@VALUE, "
        sqlStr = sqlStr & "TARGET_DATE=@TARGET_DATE "
        sqlStr = sqlStr & " where unq_no= @UNQ_NO and  DELFG=0  "



        dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@UNQ_NO", queryParams.UNQ_NO))
        dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@CRTCD", queryParams.CRTCD))

        dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@SHIP_TO_BRANCH", queryParams.SHIP_TO_BRANCH))
        dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@VALUE", queryParams.VALUE))

        dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@TARGET_DATE", queryParams.TARGET_DATE))

        dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@DELFG", 0))

        flag = dbConn.ExecSQL(sqlStr)
        dbConn.sqlCmd.Parameters.Clear()
        dbConn.CloseConnection()
        Return flag

    End Function
End Class
