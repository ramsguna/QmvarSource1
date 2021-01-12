Imports System
Imports System.Data
Imports System.Web.Security
Imports System.Collections.Generic
Imports Ganges33.Ganges33.dao
Imports Ganges33.Ganges33.model
Imports Ganges33.Ganges33.logic

Public Class POControl
    Public Function POPurchaseAmount(ByVal queryParams As POModel) As DataTable
        Log4NetControl.ComInfoLogWrite(Log4NetControl.UserID)
        Dim dbConn As DBUtility = New DBUtility()
        Dim dt As DataTable = New DataTable()
        Dim sqlStr As String = "SELECT "
        sqlStr = sqlStr & "  LEFT(CONVERT(VARCHAR, PO_Date, 111), 10) as 'PO_Date',ISNULL(sum(Amount),0) as Amount from PO_DC where DELFG=0 AND STATUS='Completed' AND "
        If Not String.IsNullOrEmpty(queryParams.ShipToBranch) Then
            sqlStr = sqlStr & " SHIP_TO_BRANCH = @ShipToBranch "
            dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@ShipToBranch", queryParams.ShipToBranch))
        End If
        If (Not (String.IsNullOrEmpty(queryParams.DateFrom)) And (Not (String.IsNullOrEmpty(queryParams.DateTo)))) Then
            sqlStr = sqlStr & " AND LEFT(CONVERT(VARCHAR, PO_Date, 111), 10) >= @DateFrom and LEFT(CONVERT(VARCHAR, PO_Date, 111), 10) <= @DateTo "
            dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@DateFrom", queryParams.DateFrom))
            dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@DateTo", queryParams.DateTo))
        ElseIf Not String.IsNullOrEmpty(queryParams.DateFrom) Then
            sqlStr = sqlStr & " AND PO_Date = @DateFrom "
            dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@DateFrom", queryParams.DateFrom))
        ElseIf Not String.IsNullOrEmpty(queryParams.DateTo) Then
            sqlStr = sqlStr & " AND PO_Date = @DateTo "
            dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@DateTo", queryParams.DateTo))
        End If
        'sqlStr = sqlStr & " AND  STATUS='Completed' GROUP BY LEFT(CONVERT(VARCHAR, PO_Date, 111), 10) order by PO_Date; "
        sqlStr = sqlStr & "  GROUP BY LEFT(CONVERT(VARCHAR, PO_Date, 111), 10) order by PO_Date; "
        Dim _DataTable As DataTable = dbConn.GetDataSet(sqlStr)
        dbConn.CloseConnection()
        Return _DataTable
    End Function

    Public Function POGDParts(ByVal queryParams As POModel, iwow As String) As DataTable
        Log4NetControl.ComInfoLogWrite(Log4NetControl.UserID)
        Dim dbConn As DBUtility = New DBUtility()
        Dim dt As DataTable = New DataTable()
        Dim sqlStr As String = "SELECT "
        sqlStr = sqlStr & "LEFT(CONVERT(VARCHAR, Billing_date, 111), 10) as 'Billing_date' ,"
        sqlStr = sqlStr & "  (ISNULL(SUM(IW_Parts),0) +ISNULL( SUM(OW_Parts),0) * " + iwow + ") AS TOTALIWOW   FROM SC_DSR WHERE DELFG=0 AND ServiceOrder_No !='Total' AND GoodsDelivered_date !='0000/00/00' AND   "

        ''sqlStr = sqlStr & " ISNULL(SUM(IW_Parts),0) AS TotalIW, ISNULL( SUM(OW_Parts),0) AS TotalOW, ISNULL( SUM(OW_Parts),0) *.88 AS TotalOWMOD, (ISNULL(SUM(IW_Parts),0) +ISNULL( SUM(OW_Parts),0) *.88) AS TOTALIWOW   FROM SC_DSR WHERE DELFG=0 AND  "


        'sqlStr = sqlStr & " ISNULL(SUM(IW_Parts),0) AS TotalIW, ISNULL( SUM(OW_Parts),0) AS TotalOW FROM SC_DSR WHERE DELFG=0 AND  "

        If Not String.IsNullOrEmpty(queryParams.ShipToBranch) Then
            sqlStr = sqlStr & " Branch_name = @ShipToBranch "
            dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@ShipToBranch", queryParams.ShipToBranch))
        End If
        If (Not (String.IsNullOrEmpty(queryParams.DateFrom)) And (Not (String.IsNullOrEmpty(queryParams.DateTo)))) Then
            sqlStr = sqlStr & " AND LEFT(CONVERT(VARCHAR, Billing_date, 111), 10) >= @DateFrom and LEFT(CONVERT(VARCHAR, Billing_date, 111), 10) <= @DateTo "
            dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@DateFrom", queryParams.DateFrom))
            dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@DateTo", queryParams.DateTo))
        ElseIf Not String.IsNullOrEmpty(queryParams.DateFrom) Then
            sqlStr = sqlStr & " AND Billing_date = @DateFrom "
            dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@DateFrom", queryParams.DateFrom))
        ElseIf Not String.IsNullOrEmpty(queryParams.DateTo) Then
            sqlStr = sqlStr & " AND Billing_date = @DateTo "
            dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@DateTo", queryParams.DateTo))
        End If
        sqlStr = sqlStr & "  GROUP BY LEFT(CONVERT(VARCHAR, Billing_date, 111), 10) order by Billing_date; "
        Dim _DataTable As DataTable = dbConn.GetDataSet(sqlStr)
        dbConn.CloseConnection()
        Return _DataTable
    End Function


    Public Function POReturn(ByVal queryParams As POModel) As DataTable
        Log4NetControl.ComInfoLogWrite(Log4NetControl.UserID)
        Dim dbConn As DBUtility = New DBUtility()
        Dim dt As DataTable = New DataTable()
        Dim sqlStr As String = "SELECT "
        sqlStr = sqlStr & "LEFT(CONVERT(VARCHAR, REQUEST_DATE, 111), 10) as 'REQUEST_DATE' ,"
        sqlStr = sqlStr & " ISNULL( SUM(AMOUNT),0) AS Amount FROM SP_RETURN WHERE DELFG=0 AND  "

        If Not String.IsNullOrEmpty(queryParams.ShipToBranch) Then
            sqlStr = sqlStr & " SHIP_TO_BRANCH = @ShipToBranch "
            dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@ShipToBranch", queryParams.ShipToBranch))
        End If
        If (Not (String.IsNullOrEmpty(queryParams.DateFrom)) And (Not (String.IsNullOrEmpty(queryParams.DateTo)))) Then
            sqlStr = sqlStr & " AND LEFT(CONVERT(VARCHAR, REQUEST_DATE, 111), 10) >= @DateFrom and LEFT(CONVERT(VARCHAR, REQUEST_DATE, 111), 10) <= @DateTo "
            dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@DateFrom", queryParams.DateFrom))
            dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@DateTo", queryParams.DateTo))
        ElseIf Not String.IsNullOrEmpty(queryParams.DateFrom) Then
            sqlStr = sqlStr & " AND Billing_date = @DateFrom "
            dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@DateFrom", queryParams.DateFrom))
        ElseIf Not String.IsNullOrEmpty(queryParams.DateTo) Then
            sqlStr = sqlStr & " AND Billing_date = @DateTo "
            dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@DateTo", queryParams.DateTo))
        End If
        sqlStr = sqlStr & "  GROUP BY LEFT(CONVERT(VARCHAR, REQUEST_DATE, 111), 10) order by REQUEST_DATE; "
        Dim _DataTable As DataTable = dbConn.GetDataSet(sqlStr)
        dbConn.CloseConnection()
        Return _DataTable
    End Function

    Public Function POInventoryBegining(ByVal queryParams As POModel) As DataTable
        Log4NetControl.ComInfoLogWrite(Log4NetControl.UserID)
        Dim dbConn As DBUtility = New DBUtility()
        Dim dt As DataTable = New DataTable()
        Dim DtConvTo() As String
        Dim finalDate As String
        DtConvTo = Split(queryParams.DateFrom, "/")
        finalDate = DtConvTo(0) & "/" & DtConvTo(1) & "/01"
        Dim oDate As DateTime = Convert.ToDateTime(finalDate)
        Dim dtPreviousMonth As DateTime = Convert.ToDateTime(oDate).AddMonths(-1).ToString("yyyy/MM/dd")
        Dim mon As String = DateAndTime.Month(dtPreviousMonth.ToString)
        Dim year As String = DateAndTime.Year(dtPreviousMonth.ToString)
        Dim Frmday As String = DateAndTime.Day(dtPreviousMonth.ToString)
        If mon.Length = 1 Then
            mon = "0" & mon
        Else
            mon = mon
        End If

        finalDate = year & "/" & mon

        Dim sqlStr As String = "SELECT "
        sqlStr = sqlStr & " ISNULL(SUM(TOTAL_STOCK_VALUE),0) AS TOTAL_STOCK_VALUE FROM STOCK_OVERVIEW WHERE DELFG=0 AND "
        If Not String.IsNullOrEmpty(queryParams.ShipToBranch) Then
            sqlStr = sqlStr & " SHIP_TO_BRANCH = @ShipToBranch "
            dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@ShipToBranch", queryParams.ShipToBranch))
        End If
        sqlStr = sqlStr & " AND MONTH = @finalDate "
        dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@finalDate", finalDate))
        sqlStr = sqlStr & " AND  DELFG=0 "
        Dim _DataTable As DataTable = dbConn.GetDataSet(sqlStr)
        dbConn.CloseConnection()
        Return _DataTable
    End Function

    Public Function POInventoryEnding(ByVal queryParams As POModel) As DataTable
        Log4NetControl.ComInfoLogWrite(Log4NetControl.UserID)
        Dim dbConn As DBUtility = New DBUtility()
        Dim dt As DataTable = New DataTable()
        Dim DtConvTo() As String
        Dim finalDate As String
        DtConvTo = Split(queryParams.DateFrom, "/")
        finalDate = DtConvTo(0) & "/" & DtConvTo(1)

        Dim sqlStr As String = "SELECT "

        sqlStr = sqlStr & " SUM(TOTAL_STOCK_VALUE) AS TOTAL_STOCK_VALUE FROM STOCK_OVERVIEW WHERE DELFG=0 AND  "
        If Not String.IsNullOrEmpty(queryParams.ShipToBranch) Then
            sqlStr = sqlStr & " SHIP_TO_BRANCH = @ShipToBranch "
            dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@ShipToBranch", queryParams.ShipToBranch))
        End If
        sqlStr = sqlStr & " AND MONTH = @finalDate "
        dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@finalDate", finalDate))
        sqlStr = sqlStr & " AND  DELFG=0 "
        Dim _DataTable As DataTable = dbConn.GetDataSet(sqlStr)
        dbConn.CloseConnection()
        Return _DataTable
    End Function

    Public Function POPayment(ByVal queryParams As POModel) As DataTable
        Log4NetControl.ComInfoLogWrite(Log4NetControl.UserID)
        Dim dbConn As DBUtility = New DBUtility()
        Dim dt As DataTable = New DataTable()
        Dim sqlStr As String = "SELECT "
        sqlStr = sqlStr & "LEFT(CONVERT(VARCHAR, TARGET_DATE, 111), 10) as 'TARGET_DATE' ,"
        sqlStr = sqlStr & " ISNULL(SUM(VALUE),0) AS Amount FROM payment_value WHERE DELFG=0 AND  "

        If Not String.IsNullOrEmpty(queryParams.ShipToBranch) Then
            sqlStr = sqlStr & " SHIP_TO_BRANCH = @ShipToBranch "
            dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@ShipToBranch", queryParams.ShipToBranch))
        End If
        If (Not (String.IsNullOrEmpty(queryParams.DateFrom)) And (Not (String.IsNullOrEmpty(queryParams.DateTo)))) Then
            sqlStr = sqlStr & " AND LEFT(CONVERT(VARCHAR, TARGET_DATE, 111), 10) >= @DateFrom and LEFT(CONVERT(VARCHAR, TARGET_DATE, 111), 10) <= @DateTo "
            dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@DateFrom", queryParams.DateFrom))
            dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@DateTo", queryParams.DateTo))
        ElseIf Not String.IsNullOrEmpty(queryParams.DateFrom) Then
            sqlStr = sqlStr & " AND TARGET_DATE = @DateFrom "
            dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@DateFrom", queryParams.DateFrom))
        ElseIf Not String.IsNullOrEmpty(queryParams.DateTo) Then
            sqlStr = sqlStr & " AND TARGET_DATE = @DateTo "
            dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@DateTo", queryParams.DateTo))
        End If
        sqlStr = sqlStr & "  GROUP BY LEFT(CONVERT(VARCHAR, TARGET_DATE, 111), 10) order by TARGET_DATE; "
        Dim _DataTable As DataTable = dbConn.GetDataSet(sqlStr)
        dbConn.CloseConnection()
        Return _DataTable
    End Function

    Public Function POCollections(ByVal queryParams As POModel) As DataTable
        Log4NetControl.ComInfoLogWrite(Log4NetControl.UserID)
        Dim dbConn As DBUtility = New DBUtility()
        Dim dt As DataTable = New DataTable()
        Dim sqlStr As String = "SELECT "
        sqlStr = sqlStr & "LEFT(CONVERT(VARCHAR, TARGET_DATE, 111), 10) as 'TARGET_DATE' ,"
        sqlStr = sqlStr & "ISNULL(SUM(DEPOSIT),0) AS DEPOSIT,ISNULL(SUM(SALES),0) AS SALES FROM COLLECTION WHERE DELFG=0 AND  "

        If Not String.IsNullOrEmpty(queryParams.ShipToBranch) Then
            sqlStr = sqlStr & " SHIP_TO_BRANCH = @ShipToBranch "
            dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@ShipToBranch", queryParams.ShipToBranch))
        End If
        If (Not (String.IsNullOrEmpty(queryParams.DateFrom)) And (Not (String.IsNullOrEmpty(queryParams.DateTo)))) Then
            sqlStr = sqlStr & " AND LEFT(CONVERT(VARCHAR, TARGET_DATE, 111), 10) >= @DateFrom and LEFT(CONVERT(VARCHAR, TARGET_DATE, 111), 10) <= @DateTo "
            dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@DateFrom", queryParams.DateFrom))
            dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@DateTo", queryParams.DateTo))
        ElseIf Not String.IsNullOrEmpty(queryParams.DateFrom) Then
            sqlStr = sqlStr & " AND TARGET_DATE = @DateFrom "
            dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@DateFrom", queryParams.DateFrom))
        ElseIf Not String.IsNullOrEmpty(queryParams.DateTo) Then
            sqlStr = sqlStr & " AND TARGET_DATE = @DateTo "
            dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@DateTo", queryParams.DateTo))
        End If
        sqlStr = sqlStr & "  GROUP BY LEFT(CONVERT(VARCHAR, TARGET_DATE, 111), 10) order by TARGET_DATE; "
        Dim _DataTable As DataTable = dbConn.GetDataSet(sqlStr)
        dbConn.CloseConnection()
        Return _DataTable
    End Function



    Public Function POBillingInformation(ByVal queryParams As POModel) As DataTable
        Log4NetControl.ComInfoLogWrite(Log4NetControl.UserID)
        Dim dbConn As DBUtility = New DBUtility()
        Dim dt As DataTable = New DataTable()
        Dim sqlStr As String = "SELECT "
        sqlStr = sqlStr & "LEFT(CONVERT(VARCHAR, INVOICE_DATE, 111), 10) as 'INVOICE_DATE' ,"
        sqlStr = sqlStr & "ISNULL(SUM(Amount),0) AS Daily_Amount FROM PR_SUMMARY WHERE DELFG=0 AND GR_STATUS='Y' AND "

        If Not String.IsNullOrEmpty(queryParams.ShipToBranchCode) Then
            sqlStr = sqlStr & " SHIP_TO_BRANCH_CODE = @ShipToBranch "
            dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@ShipToBranch", queryParams.ShipToBranchCode))
        End If
        If (Not (String.IsNullOrEmpty(queryParams.DateFrom)) And (Not (String.IsNullOrEmpty(queryParams.DateTo)))) Then
            sqlStr = sqlStr & " AND LEFT(CONVERT(VARCHAR, INVOICE_DATE, 111), 10) >= @DateFrom and LEFT(CONVERT(VARCHAR, INVOICE_DATE, 111), 10) <= @DateTo "
            dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@DateFrom", queryParams.DateFrom))
            dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@DateTo", queryParams.DateTo))
        ElseIf Not String.IsNullOrEmpty(queryParams.DateFrom) Then
            sqlStr = sqlStr & " AND INVOICE_DATE = @DateFrom "
            dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@DateFrom", queryParams.DateFrom))
        ElseIf Not String.IsNullOrEmpty(queryParams.DateTo) Then
            sqlStr = sqlStr & " AND INVOICE_DATE = @DateTo "
            dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@DateTo", queryParams.DateTo))
        End If
        sqlStr = sqlStr & "  GROUP BY LEFT(CONVERT(VARCHAR, INVOICE_DATE, 111), 10) order by INVOICE_DATE; "
        Dim _DataTable As DataTable = dbConn.GetDataSet(sqlStr)
        dbConn.CloseConnection()
        Return _DataTable
    End Function
End Class
