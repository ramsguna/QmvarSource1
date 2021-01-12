Imports System
Imports System.Data
Imports System.Web.Security
Imports System.Collections.Generic
Imports Ganges33.Ganges33.dao
Imports Ganges33.Ganges33.model
Imports Ganges33.Ganges33.logic

Public Class InvoiceUpdateControl


    Public Function SelectInvoiceUpdate(ByVal queryParams As InvoiceUpdateModel) As DataTable
        Log4NetControl.ComInfoLogWrite(Log4NetControl.UserID)
        Dim dbConn As DBUtility = New DBUtility()
        Dim dt As DataTable = New DataTable()
        Dim sqlStr As String = "SELECT "
        'Comment Need to discuss about getting ASC code value from 
        sqlStr = sqlStr & "(SELECT TOP 1 SHIP_CODE FROM M_ship_base WHERE ship_name=upload_Branch) AS 'ASC Code', "
        'sqlStr = sqlStr & "(SELECT TOP 1 ASC_CODE FROM Wty_Excel WHERE Samsung_Claim_No=samsung_Ref_No) As 'ASC_CODE', "

        sqlStr = sqlStr & " upload_Branch as 'Branch', "
        'For only 4. Deliver Date
        If UCase(queryParams.Number) = "OWC" Then
            sqlStr = sqlStr & "(SELECT TOP 1 LEFT(CONVERT(VARCHAR, Delivery_Date,111),10) FROM Wty_Excel WHERE Samsung_Claim_No=samsung_Ref_No) AS 'Delivery Date',"
        End If
        sqlStr = sqlStr & " SAMSUNG_REF_NO as 'Samsung Ref No',YOUR_REF_NO as 'Your Ref No',MODEL as 'Model',SERIAL as 'Serial',PRODUCT as 'Product',Serivice as 'Service',Defect_Code as 'Defect Code',CURRENCY as 'Currency',INVOICE as 'Invoice', LABOR as 'Labor',PARTS as 'Parts',Felight as 'Freight',OTHER as 'Other',TAX as 'Tax', Parts_invoice_No as 'Parts Invoice No', Labor_Invoice_No as 'Labor Invoice No', LEFT(CONVERT(VARCHAR, INVOICE_DATE, 111), 10) as 'Invoice Date' "
        sqlStr = sqlStr & "FROM "
        sqlStr = sqlStr & "Invoice_update "
        sqlStr = sqlStr & "WHERE "
        sqlStr = sqlStr & "DELFG=0 "

        If Not String.IsNullOrEmpty(queryParams.Number) Then
            sqlStr = sqlStr & "AND number = @number  "
            dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@number", queryParams.Number))
        End If

        If (Not (String.IsNullOrEmpty(queryParams.DateFrom)) And (Not (String.IsNullOrEmpty(queryParams.DateTo)))) Then
            sqlStr = sqlStr & "AND LEFT(CONVERT(VARCHAR, INVOICE_DATE, 111), 10) >= @DateFrom and LEFT(CONVERT(VARCHAR, INVOICE_DATE, 111), 10) <= @DateTo "
            'sqlStr = sqlStr & "AND INVOICE_DATE >= @DateFrom and INVOICE_DATE <= @DateTo "
            dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@DateFrom", queryParams.DateFrom))
            dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@DateTo", queryParams.DateTo))
        ElseIf Not String.IsNullOrEmpty(queryParams.DateFrom) Then
            'sqlStr = sqlStr & "AND LEFT(CONVERT(VARCHAR, INVOICE_DATE, 111), 10) = @DateFrom "
            sqlStr = sqlStr & "AND INVOICE_DATE = @DateFrom "
            dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@DateFrom", queryParams.DateFrom))
        ElseIf Not String.IsNullOrEmpty(queryParams.DateTo) Then
            sqlStr = sqlStr & "AND INVOICE_DATE = @DateTo "
            'sqlStr = sqlStr & "AND LEFT(CONVERT(VARCHAR, INVOICE_DATE, 111), 10) = @DateTo "
            dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@DateTo", queryParams.DateTo))
        End If


        Dim _DataTable As DataTable = dbConn.GetDataSet(sqlStr)
        dbConn.CloseConnection()
        Return _DataTable
    End Function
End Class
