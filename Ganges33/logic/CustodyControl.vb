Imports System
Imports System.Data
Imports System.Web.Security
Imports System.Collections.Generic
Imports Ganges33.Ganges33.dao
Imports Ganges33.Ganges33.model
Imports Ganges33.Ganges33.logic

Public Class CustodyControl
    Public Function AddCashAdvance(queryParams As CustodyModel) As Boolean
        Dim DateTimeNow As DateTime = DateTime.Now
        Dim dtNow As DateTime = DateTimeNow.AddMinutes(ConfigurationManager.AppSettings("TimeDiff"))
        Dim sqlStr As String = ""
        Dim flag As Boolean = True

        Dim dbConn As DBUtility = New DBUtility()
        Dim dt As DataTable = New DataTable()
        dbConn.sqlTrn = dbConn.sqlConn.BeginTransaction()
        dbConn.sqlCmd.Transaction = dbConn.sqlTrn
        sqlStr = "Insert into custody ("
        sqlStr = sqlStr & "CRTDT, "
        sqlStr = sqlStr & "CRTCD, "
        sqlStr = sqlStr & "UPDPG, "
        sqlStr = sqlStr & "DELFG, "
        sqlStr = sqlStr & "keep_no, "
        sqlStr = sqlStr & "customer_name, "
        sqlStr = sqlStr & "customer_tel, "
        sqlStr = sqlStr & "samsung_claim_no, "
        sqlStr = sqlStr & "product_name, "
        sqlStr = sqlStr & "cash, "
        sqlStr = sqlStr & "ship_code, "
        sqlStr = sqlStr & "takeout "
        sqlStr = sqlStr & " ) "
        sqlStr = sqlStr & " values ( "
        sqlStr = sqlStr & "@CRTDT, "
        sqlStr = sqlStr & "@CRTCD, "
        '    sqlStr = sqlStr & "@UPDDT, "
        'sqlStr = sqlStr & "@UPDCD, "
        sqlStr = sqlStr & "@UPDPG, "
        sqlStr = sqlStr & "@DELFG, "
        sqlStr = sqlStr & "@KeepNo, "
        sqlStr = sqlStr & "@CustomerName, "
        sqlStr = sqlStr & "@CustomerTel, "
        sqlStr = sqlStr & "@SamsungClaimNo, "
        sqlStr = sqlStr & "@ProductName, "
        sqlStr = sqlStr & "@Cash, "
        sqlStr = sqlStr & "@ShipCode, "
        sqlStr = sqlStr & "@TakeOut "
        sqlStr = sqlStr & " ) "

        dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@CRTDT", dtNow))
        dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@CRTCD", queryParams.UserId))
        '    dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@UPDDT", queryParams.UserId))
        ' dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@UPDCD", queryParams.UserId))

        dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@UPDPG", queryParams.UPDPG))
        dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@DELFG", 0))
        dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@KeepNo", queryParams.KeepNo))
        dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@CustomerName", queryParams.CustomerName))
        dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@CustomerTel", queryParams.CustomerTel))
        dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@SamsungClaimNo", queryParams.SamsungClaimNo))
        dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@ProductName", queryParams.ProductName))
        dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@Cash", queryParams.Cash))
        dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@ShipCode", queryParams.ShipCode))
        dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@TakeOut", queryParams.TakeOut))

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

    Public Function SelectCustodyCheck(ByVal queryParams As CustodyModel) As String
        Log4NetControl.ComInfoLogWrite(Log4NetControl.UserID)
        Dim dbConn As DBUtility = New DBUtility()
        Dim dt As DataTable = New DataTable()
        Dim sqlStr As String = "SELECT "
        sqlStr = sqlStr & "COUNT(*) AS CustodyExistCnt "
        sqlStr = sqlStr & "FROM "
        sqlStr = sqlStr & "custody "
        sqlStr = sqlStr & "WHERE "
        sqlStr = sqlStr & "DELFG=0 "
        If Not String.IsNullOrEmpty(queryParams.KeepNo) Then
            sqlStr = sqlStr & "AND keep_no = @KeepNo "
            dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@KeepNo", queryParams.KeepNo))
        End If
        If Not String.IsNullOrEmpty(queryParams.CustomerName) Then
            sqlStr = sqlStr & "AND customer_name = @CustomerName "
            dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@CustomerName", queryParams.CustomerName))
        End If
        If Not String.IsNullOrEmpty(queryParams.CustomerTel) Then
            sqlStr = sqlStr & "AND customer_tel = @CustomerTel "
            dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@CustomerTel", queryParams.CustomerTel))
        End If
        dt = dbConn.GetDataSet(sqlStr)
        dbConn.CloseConnection()
        Return dt.Rows(0)("CustodyExistCnt").ToString()
    End Function

    Public Function SelectCustodyDetails(ByVal queryParams As CustodyModel) As DataTable
        Log4NetControl.ComInfoLogWrite(Log4NetControl.UserID)
        Dim dbConn As DBUtility = New DBUtility()
        Dim sqlStr As String = "SELECT "
        sqlStr = sqlStr & "CRTDT, "
        sqlStr = sqlStr & "CRTCD, "
        sqlStr = sqlStr & "UPDDT, "
        sqlStr = sqlStr & "UPDCD, "
        sqlStr = sqlStr & "UPDPG, "
        sqlStr = sqlStr & "DELFG, "
        sqlStr = sqlStr & "keep_no, "
        sqlStr = sqlStr & "customer_name, "
        sqlStr = sqlStr & "customer_tel, "
        sqlStr = sqlStr & "samsung_claim_no, "
        sqlStr = sqlStr & "product_name, "
        sqlStr = sqlStr & "cash, "
        sqlStr = sqlStr & "ship_code, "
        sqlStr = sqlStr & "takeout "
        sqlStr = sqlStr & "FROM "
        sqlStr = sqlStr & "custody "
        sqlStr = sqlStr & "WHERE "
        sqlStr = sqlStr & "DELFG=0 "
        If Not String.IsNullOrEmpty(queryParams.KeepNo) Then
            sqlStr = sqlStr & "AND keep_no = @KeepNo "
            dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@KeepNo", queryParams.KeepNo))
        End If
        If Not String.IsNullOrEmpty(queryParams.CustomerName) Then
            sqlStr = sqlStr & "AND customer_name = @CustomerName "
            dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@CustomerName", queryParams.CustomerName))
        End If
        If Not String.IsNullOrEmpty(queryParams.CustomerTel) Then
            sqlStr = sqlStr & "AND customer_tel = @CustomerTel "
            dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@CustomerTel", queryParams.CustomerTel))
        End If
        Dim _DataTable As DataTable = dbConn.GetDataSet(sqlStr)
        dbConn.CloseConnection()
        Return _DataTable
    End Function

    Public Function UpdateCashTakeOut(ByVal lstCustodyKeepModel As List(Of CustodyKeepModel)) As Boolean
        Dim dbConn As DBUtility = New DBUtility()
        dbConn.sqlTrn = dbConn.sqlConn.BeginTransaction()
        Dim rowIndex As Integer = 0
        Dim DateTimeNow As DateTime = DateTime.Now
        Dim dtNow As DateTime = DateTimeNow.AddMinutes(ConfigurationManager.AppSettings("TimeDiff"))

        For Each target As CustodyKeepModel In lstCustodyKeepModel
            rowIndex = rowIndex + 1
            dbConn.sqlCmd.Transaction = dbConn.sqlTrn
            Dim sqlStr As String = "" '
            sqlStr = "UPDATE custody 
						SET
		                   takeout = 0
                           ,  UPDDT = @UPDDT{0}
                          ,  UPDCD = @UPDCD{0}
						WHERE
						  keep_no = @keep_no{0} 
							"
            sqlStr = String.Format(sqlStr, rowIndex)
            dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter(String.Format("@keep_no{0}", rowIndex), target.KeepNo))
            dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter(String.Format("@UPDDT{0}", rowIndex), dtNow.Date))
            dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter(String.Format("@UPDCD{0}", rowIndex), target.UpdateUser))
            sqlStr = String.Format(sqlStr, rowIndex)

            Dim updateFlg As Boolean = dbConn.ExecSQL(sqlStr)
            If Not updateFlg Then
                dbConn.sqlTrn.Rollback()
                Return False
            End If

        Next

        dbConn.sqlTrn.Commit()
        dbConn.CloseConnection()
        Return True
    End Function
End Class
