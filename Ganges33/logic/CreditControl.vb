Imports System
Imports System.Data
Imports System.Web.Security
Imports System.Collections.Generic
Imports Ganges33.Ganges33.dao
Imports Ganges33.Ganges33.model
Imports Ganges33.Ganges33.logic
Public Class CreditControl
    Public Function Get_Credit_info(queryParams As CreditModel) As DataTable
        Dim Credit_Info_Model As CreditModel = New CreditModel()
        Log4NetControl.ComInfoLogWrite(Log4NetControl.UserID)
        Dim dbConn As DBUtility = New DBUtility()
        Dim sqlStr As String = "SELECT SB.ship_name BRANCH_NO,CREDIT_LIMIT,PER_DAY "
        sqlStr = sqlStr & " From M_ship_base SB "
        sqlStr = sqlStr & " Left Join R_CREDIT_I CI on SB.ship_name = CI.BRANCH_NO "
        sqlStr = sqlStr & " where SB.DELFG = 0 and sb.IsChildShip = 0 "

        If Not String.IsNullOrEmpty(queryParams.BRANCH_NO) Then
            sqlStr = sqlStr & "AND BRANCH_NO = @BRANCH_NO "
            dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@BRANCH_NO", queryParams.BRANCH_NO))
        End If
        Dim _DataTable As DataTable = dbConn.GetDataSet(sqlStr)
        dbConn.CloseConnection()
        Return _DataTable
    End Function
    Public Function Insert_Credit_info(queryParams As CreditModel) As Boolean
        Dim flag As Boolean
        Dim Credit_Info_Model As CreditModel = New CreditModel()
        Log4NetControl.ComInfoLogWrite(Log4NetControl.UserID)
        Dim dbConn As DBUtility = New DBUtility()
        Dim sqlStr As String = "update R_CREDIT_I set   "
        If Not String.IsNullOrEmpty(queryParams.CREDIT_LIMIT) And Not String.IsNullOrEmpty(queryParams.PER_DAY) Then
            sqlStr = sqlStr & " CREDIT_LIMIT = @CREDIT_LIMIT, "
            dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@CREDIT_LIMIT", queryParams.CREDIT_LIMIT))
            sqlStr = sqlStr & " PER_DAY = @PER_DAY "
            dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@PER_DAY", queryParams.PER_DAY))

        Else

            If Not String.IsNullOrEmpty(queryParams.CREDIT_LIMIT) Then
                sqlStr = sqlStr & " CREDIT_LIMIT = @CREDIT_LIMIT "
                dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@CREDIT_LIMIT", queryParams.CREDIT_LIMIT))
            End If

            If Not String.IsNullOrEmpty(queryParams.PER_DAY) Then
                sqlStr = sqlStr & " PER_DAY = @PER_DAY "
                dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@PER_DAY", queryParams.PER_DAY))
            End If

        End If
        If Not String.IsNullOrEmpty(queryParams.BRANCH_NO) Then
            sqlStr = sqlStr & "where BRANCH_NO = @BRANCH_NO "
            dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@BRANCH_NO", queryParams.BRANCH_NO))
        End If


        flag = dbConn.ExecSQL(sqlStr)
        dbConn.sqlCmd.Parameters.Clear()
        dbConn.CloseConnection()
        Return flag
    End Function

    Public Function Get_GSTIN(queryParams As CreditModel) As DataTable
        Dim Credit_Info_Model As CreditModel = New CreditModel()
        Log4NetControl.ComInfoLogWrite(Log4NetControl.UserID)
        Dim dbConn As DBUtility = New DBUtility()
        Dim sqlStr As String = "SELECT ship_Name,GSTIN From M_ship_base "

        If Not String.IsNullOrEmpty(queryParams.BRANCH_NO) Then
            sqlStr = sqlStr & "where ship_Name = @ship_Name "
            dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@ship_Name", queryParams.ship_Name))
        End If
        Dim _DataTable As DataTable = dbConn.GetDataSet(sqlStr)
        dbConn.CloseConnection()
        Return _DataTable
    End Function
    Public Function Insert_GSTIN(queryParams As CreditModel) As Boolean
        Dim flag As Boolean
        Dim Credit_Info_Model As CreditModel = New CreditModel()
        Log4NetControl.ComInfoLogWrite(Log4NetControl.UserID)
        Dim dbConn As DBUtility = New DBUtility()
        Dim sqlStr As String = "update M_ship_base set   "
        If Not String.IsNullOrEmpty(queryParams.GSTIN) Then
            sqlStr = sqlStr & " GSTIN = @GSTIN "
            dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@GSTIN", queryParams.GSTIN))
        End If

        If Not String.IsNullOrEmpty(queryParams.ship_Name) Then
            sqlStr = sqlStr & "where ship_Name = @ship_Name "
            dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@ship_Name", queryParams.ship_Name))
        End If

        flag = dbConn.ExecSQL(sqlStr)
        dbConn.sqlCmd.Parameters.Clear()
        dbConn.CloseConnection()
        Return flag
    End Function
End Class



