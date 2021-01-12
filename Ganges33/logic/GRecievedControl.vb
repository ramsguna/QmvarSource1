Imports System
Imports System.Data
Imports System.Web.Security
Imports System.Collections.Generic
Imports Ganges33.Ganges33.dao
Imports Ganges33.Ganges33.model
Imports Ganges33.Ganges33.logic
Public Class GRecievedControl
    Public Function AddModifyGRecieved(ByVal csvData()() As String, queryParams As GRecievedModel) As Boolean
        'Row 0 - Header1 and 1 - Header2
        '0 No
        '1 Invoice No/ Delivery No
        '2 Invoice Date/ Delivery Date
        '3 Local Invoice No
        '4 Items
        '5 Total Qty
        '6 Total Amount
        '7 GR Date
        '8 Create By
        '9 G/R Status
        Log4NetControl.ComInfoLogWrite(Log4NetControl.UserID)
        'Mandatory Column 9 from CSV
        Dim flag As Boolean = True
        If csvData(0).Length < 10 Then
            Return False
        End If

        '       Log4NetControl.ComInfoLogWrite(Log4NetControl.UserID)
        Dim DateTimeNow As DateTime = DateTime.Now
        Dim dtNow As DateTime = DateTimeNow.AddMinutes(ConfigurationManager.AppSettings("TimeDiff"))
        Dim dbConn As DBUtility = New DBUtility()
        Dim dt As DataTable = New DataTable()
        dbConn.sqlTrn = dbConn.sqlConn.BeginTransaction()
        dbConn.sqlCmd.Transaction = dbConn.sqlTrn
        '       Dim flag As Boolean = True
        Dim flagAll As Boolean = True
        Dim sqlStr As String = ""
        Dim dtUpdateOtherExist As DataTable

        Dim isExist As Integer = 0
        '1st check INVOICE_NO,LOCAL_INVOICE_NO exist in the table 
        sqlStr = "SELECT TOP 1 INVOICE_NO,LOCAL_INVOICE_NO FROM G_RECEIVED "
        sqlStr = sqlStr & " WHERE DELFG = 0 AND SRC_FILE_NAME='" & queryParams.SrcFileName & "'"
        'sqlStr = sqlStr & " AND SHIP_TO_BRANCH_CODE='" & queryParams.ShipToBranchCode & "'"
        dtUpdateOtherExist = dbConn.GetDataSet(sqlStr)
        'if exist then need to update delfg=1 then insert the record as new
        If (dtUpdateOtherExist Is Nothing) Or (dtUpdateOtherExist.Rows.Count = 0) Then
            'isExist = 0
        Else 'The records is already exist, need to update DELFg=0
            ' isExist = 1
            sqlStr = "UPDATE G_RECEIVED SET DELFG=1  "
            sqlStr = sqlStr & "WHERE DELFG=0 "
            sqlStr = sqlStr & "AND SRC_FILE_NAME = @SRC_FILE_NAME"
            'sqlStr = sqlStr & "AND SHIP_TO_BRANCH_CODE = @SHIP_TO_BRANCH_CODE "
            dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@SRC_FILE_NAME", queryParams.SrcFileName))
            flag = dbConn.ExecSQL(sqlStr)
            dbConn.sqlCmd.Parameters.Clear()
            'If Error occurs then will store the flag as false
            If Not flag Then
                flagAll = False
            End If
        End If
        'If there is no error
        If flagAll Then
            For i = 0 To csvData.Length - 1
                If i > 1 Then '0  Header, 1 Header
                    'If isExist = 1 Then
                    sqlStr = "Insert into G_RECEIVED ("
                    sqlStr = sqlStr & "CRTDT, "
                    sqlStr = sqlStr & "CRTCD, "
                    ' sqlStr = sqlStr & "UPDDT, "
                    sqlStr = sqlStr & "UPDCD, "
                    sqlStr = sqlStr & "UPDPG, "
                    sqlStr = sqlStr & "DELFG, "
                    '         sqlStr = sqlStr & "UNQ_NO, "
                    sqlStr = sqlStr & "UPLOAD_USER, "
                    sqlStr = sqlStr & "UPLOAD_DATE, "
                    sqlStr = sqlStr & "SHIP_TO_BRANCH_CODE, "
                    sqlStr = sqlStr & "SHIP_TO_BRANCH, "
                    sqlStr = sqlStr & "NO, "
                    sqlStr = sqlStr & "INVOICE_NO, "
                    sqlStr = sqlStr & "INVOICE_DATE, "
                    sqlStr = sqlStr & "LOCAL_INVOICE_NO, "
                    sqlStr = sqlStr & "ITEMS, "
                    sqlStr = sqlStr & "TOTAL_QTY, "
                    sqlStr = sqlStr & "TOTAL_AMOUNT, "
                    sqlStr = sqlStr & "GR_DATE, "
                    sqlStr = sqlStr & "CREATE_BY, "
                    sqlStr = sqlStr & "GR_STATUS, "
                    sqlStr = sqlStr & "FILE_NAME, "
                    sqlStr = sqlStr & "SRC_FILE_NAME "
                    sqlStr = sqlStr & " ) "
                    sqlStr = sqlStr & " values ( "
                    sqlStr = sqlStr & "@CRTDT, "
                    sqlStr = sqlStr & "@CRTCD, "
                    'sqlStr = sqlStr & "@UPDDT, "
                    sqlStr = sqlStr & "@UPDCD, "
                    sqlStr = sqlStr & "@UPDPG, "
                    sqlStr = sqlStr & "@DELFG, "
                    '         sqlStr = sqlStr & " (select max(UNQ_NO)+1 from G_RECEIVED) , "
                    sqlStr = sqlStr & "@UPLOAD_USER, "
                    sqlStr = sqlStr & "@UPLOAD_DATE, "
                    sqlStr = sqlStr & "@SHIP_TO_BRANCH_CODE, "
                    sqlStr = sqlStr & "@SHIP_TO_BRANCH, "
                    sqlStr = sqlStr & "@NO, "
                    sqlStr = sqlStr & "@INVOICE_NO, "
                    sqlStr = sqlStr & "@INVOICE_DATE, "
                    sqlStr = sqlStr & "@LOCAL_INVOICE_NO, "
                    sqlStr = sqlStr & "@ITEMS, "
                    sqlStr = sqlStr & "@TOTAL_QTY, "
                    sqlStr = sqlStr & "@TOTAL_AMOUNT, "
                    sqlStr = sqlStr & "@GR_DATE, "
                    sqlStr = sqlStr & "@CREATE_BY, "
                    sqlStr = sqlStr & "@GR_STATUS, "
                    sqlStr = sqlStr & "@FILE_NAME, "
                    sqlStr = sqlStr & "@SRC_FILE_NAME "
                    sqlStr = sqlStr & " )"
                    dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@CRTDT", dtNow))
                    dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@CRTCD", queryParams.UserId))
                    dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@UPDCD", "")) '?????????????????????????
                    dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@UPDPG", queryParams.UPDPG))
                    dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@DELFG", 0))
                    dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@UPLOAD_USER", queryParams.UploadUser))
                    dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@UPLOAD_DATE", dtNow))
                    dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@SHIP_TO_BRANCH_CODE", queryParams.ShipToBranchCode))
                    dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@SHIP_TO_BRANCH", queryParams.ShipToBranch))
                    dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@NO", csvData(i)(0)))
                    dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@INVOICE_NO", csvData(i)(1)))
                    dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@INVOICE_DATE", csvData(i)(2)))
                    dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@LOCAL_INVOICE_NO", csvData(i)(3)))
                    dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@ITEMS", csvData(i)(4)))
                    dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@TOTAL_QTY", csvData(i)(5)))
                    dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@TOTAL_AMOUNT", csvData(i)(6)))
                    dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@GR_DATE", csvData(i)(7)))
                    dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@CREATE_BY", csvData(i)(8)))
                    dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@GR_STATUS", csvData(i)(9)))
                    dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@FILE_NAME", queryParams.FileName))
                    dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@SRC_FILE_NAME", queryParams.SrcFileName))

                    flag = dbConn.ExecSQL(sqlStr)
                    dbConn.sqlCmd.Parameters.Clear()
                    'If Error occurs then will store the flag as false
                    If Not flag Then
                        flagAll = False
                        Exit For
                    End If
                    'End If
                End If 'Other than header - End
            Next
        End If
        If flagAll Then
            flag = True
            dbConn.sqlTrn.Commit()
        Else
            flag = False
            dbConn.sqlTrn.Rollback()
        End If
        dbConn.CloseConnection()
        Return flag
    End Function

    Public Function AddModifyGDRecieved(ByVal csvData()() As String, queryParams As GRecievedModel) As Boolean
        '*********************************
        'Added on 20190724
        '*********************************
        'Row 0 - Header1 
        '0 No
        '1 Invoice No
        '2 PO NO
        '3 Item No
        '4 Description
        '5 Ordered Parts
        '6 Shipped Parts
        '7 Delivered Qty
        '8 Confirmed Qty
        '9 Total Stock Qty
        '10 Reason
        '11 Location1
        '12 Location2
        '13 Location3
        '14 HSN Code of Parts Received
        '15 Supply Location
        '16 Delivery Location
        '17 Remarks

        Log4NetControl.ComInfoLogWrite(Log4NetControl.UserID)
        'Mandatory Column 17 from CSV
        Dim flag As Boolean = True
        If csvData(0).Length < 18 Then
            Return False
        End If

        ' Log4NetControl.ComInfoLogWrite(Log4NetControl.UserID)
        Dim DateTimeNow As DateTime = DateTime.Now
        Dim dtNow As DateTime = DateTimeNow.AddMinutes(ConfigurationManager.AppSettings("TimeDiff"))
        Dim dbConn As DBUtility = New DBUtility()
        Dim dt As DataTable = New DataTable()
        dbConn.sqlTrn = dbConn.sqlConn.BeginTransaction()
        dbConn.sqlCmd.Transaction = dbConn.sqlTrn
        '    Dim flag As Boolean = True
        Dim flagAll As Boolean = True
        Dim sqlStr As String = ""
        Dim dtUpdateOtherExist As DataTable
        Dim isExist As Integer = 0
        Dim InvoiceDate As String = ""
        Dim GrStatus As String = ""
        '1st check INVOICE_NO  exist in the table 
        sqlStr = "SELECT TOP 1 INVOICE_NO FROM GD_RECEIVED "
        sqlStr = sqlStr & " WHERE DELFG = 0 AND SRC_FILE_NAME='" & queryParams.SrcFileName & "'"
        'sqlStr = sqlStr & " AND SHIP_TO_BRANCH_CODE='" & queryParams.ShipToBranchCode & "'"
        dtUpdateOtherExist = dbConn.GetDataSet(sqlStr)
        'if exist then need to update delfg=1 then insert the record as new
        If (dtUpdateOtherExist Is Nothing) Or (dtUpdateOtherExist.Rows.Count = 0) Then
            'isExist = 0
        Else 'The records is already exist, need to update DELFg=0
            ' isExist = 1
            sqlStr = "UPDATE GD_RECEIVED SET DELFG=1  "
            sqlStr = sqlStr & "WHERE DELFG=0 "
            sqlStr = sqlStr & "AND SRC_FILE_NAME = @SRC_FILE_NAME"
            'sqlStr = sqlStr & "AND SHIP_TO_BRANCH_CODE = @SHIP_TO_BRANCH_CODE "
            dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@SRC_FILE_NAME", queryParams.SrcFileName))
            flag = dbConn.ExecSQL(sqlStr)
            dbConn.sqlCmd.Parameters.Clear()
            'If Error occurs then will store the flag as false
            If Not flag Then
                flagAll = False
            End If
        End If

        'If there is no error
        If flagAll Then
            For i = 0 To csvData.Length - 1
                If i > 0 Then '0  Header
                    'Get the Invoice Date
                    'If not found then upload cann't be completed, show the error message
                    sqlStr = "SELECT INVOICE_DATE,GR_STATUS FROM G_RECEIVED   "
                    sqlStr = sqlStr & "WHERE DELFG=0 "
                    sqlStr = sqlStr & "AND INVOICE_NO = @INVOICE_NO"
                    'sqlStr = sqlStr & "AND SHIP_TO_BRANCH_CODE = @SHIP_TO_BRANCH_CODE "
                    dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@INVOICE_NO", csvData(i)(1)))
                    dtUpdateOtherExist = dbConn.GetDataSet(sqlStr)
                    If (dtUpdateOtherExist Is Nothing) Or (dtUpdateOtherExist.Rows.Count = 0) Then
                        'If there is no invoice number found, then raise the error
                        flagAll = False
                        Exit For
                    Else
                        'Update the invoice number  & GR Status
                        InvoiceDate = dtUpdateOtherExist.Rows(0)("INVOICE_DATE").ToString()
                        GrStatus = dtUpdateOtherExist.Rows(0)("GR_STATUS").ToString()
                    End If
                    dbConn.sqlCmd.Parameters.Clear()
                    'If isExist = 1 Then
                    sqlStr = "Insert into GD_RECEIVED ("
                    sqlStr = sqlStr & "CRTDT, "
                    sqlStr = sqlStr & "CRTCD, "
                    ' sqlStr = sqlStr & "UPDDT, "
                    sqlStr = sqlStr & "UPDCD, "
                    sqlStr = sqlStr & "UPDPG, "
                    sqlStr = sqlStr & "DELFG, "
                    '         sqlStr = sqlStr & "UNQ_NO, "
                    sqlStr = sqlStr & "UPLOAD_USER, "
                    sqlStr = sqlStr & "UPLOAD_DATE, "
                    sqlStr = sqlStr & "SHIP_TO_BRANCH_CODE, "
                    sqlStr = sqlStr & "SHIP_TO_BRANCH, "
                    sqlStr = sqlStr & "NO, "
                    sqlStr = sqlStr & "INVOICE_NO, "
                    sqlStr = sqlStr & "PO_NO, "
                    sqlStr = sqlStr & "ITEM_NO, "
                    sqlStr = sqlStr & "DESCRIPTION, "
                    sqlStr = sqlStr & "ORDERED_PARTS, "
                    sqlStr = sqlStr & "SHIPPED_PARTS, "
                    sqlStr = sqlStr & "DELIVERED_QTY, "
                    sqlStr = sqlStr & "CONFIRMED_QTY, "
                    sqlStr = sqlStr & "TOTAL_STOCK_QTY, "
                    sqlStr = sqlStr & "REASON, "
                    sqlStr = sqlStr & "LOCATION1, "
                    sqlStr = sqlStr & "LOCATION2, "
                    sqlStr = sqlStr & "LOCATION3, "
                    sqlStr = sqlStr & "HSN_CODE_OF_PARTS_RECEIVED, "
                    sqlStr = sqlStr & "SUPPLY_LOCATION, "
                    sqlStr = sqlStr & "DELIVERY_LOCATION, "
                    sqlStr = sqlStr & "REMARKS, "
                    sqlStr = sqlStr & "FILE_NAME, "
                    sqlStr = sqlStr & "SRC_FILE_NAME, "
                    sqlStr = sqlStr & "INVOICE_DATE, "
                    sqlStr = sqlStr & "GR_STATUS "
                    sqlStr = sqlStr & " ) "
                    sqlStr = sqlStr & " values ( "
                    sqlStr = sqlStr & "@CRTDT, "
                    sqlStr = sqlStr & "@CRTCD, "
                    'sqlStr = sqlStr & "@UPDDT, "
                    sqlStr = sqlStr & "@UPDCD, "
                    sqlStr = sqlStr & "@UPDPG, "
                    sqlStr = sqlStr & "@DELFG, "
                    '         sqlStr = sqlStr & " (select max(UNQ_NO)+1 from G_RECEIVED) , "
                    sqlStr = sqlStr & "@UPLOAD_USER, "
                    sqlStr = sqlStr & "@UPLOAD_DATE, "
                    sqlStr = sqlStr & "@SHIP_TO_BRANCH_CODE, "
                    sqlStr = sqlStr & "@SHIP_TO_BRANCH, "
                    sqlStr = sqlStr & "@NO, "
                    sqlStr = sqlStr & "@INVOICE_NO, "
                    sqlStr = sqlStr & "@PO_NO, "
                    sqlStr = sqlStr & "@ITEM_NO, "
                    sqlStr = sqlStr & "@DESCRIPTION, "
                    sqlStr = sqlStr & "@ORDERED_PARTS, "
                    sqlStr = sqlStr & "@SHIPPED_PARTS, "
                    sqlStr = sqlStr & "@DELIVERED_QTY, "
                    sqlStr = sqlStr & "@CONFIRMED_QTY, "
                    sqlStr = sqlStr & "@TOTAL_STOCK_QTY, "
                    sqlStr = sqlStr & "@REASON, "
                    sqlStr = sqlStr & "@LOCATION1, "
                    sqlStr = sqlStr & "@LOCATION2, "
                    sqlStr = sqlStr & "@LOCATION3, "
                    sqlStr = sqlStr & "@HSN_CODE_OF_PARTS_RECEIVED, "
                    sqlStr = sqlStr & "@SUPPLY_LOCATION, "
                    sqlStr = sqlStr & "@DELIVERY_LOCATION, "
                    sqlStr = sqlStr & "@REMARKS, "
                    sqlStr = sqlStr & "@FILE_NAME, "
                    sqlStr = sqlStr & "@SRC_FILE_NAME, "
                    sqlStr = sqlStr & "@INVOICE_DATE, "
                    sqlStr = sqlStr & "@GR_STATUS "
                    sqlStr = sqlStr & " )"
                    dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@CRTDT", dtNow))
                    dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@CRTCD", queryParams.UserId))
                    dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@UPDCD", "")) '?????????????????????????
                    dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@UPDPG", queryParams.UPDPG))
                    dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@DELFG", 0))
                    dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@UPLOAD_USER", queryParams.UploadUser))
                    dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@UPLOAD_DATE", dtNow))
                    dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@SHIP_TO_BRANCH_CODE", queryParams.ShipToBranchCode))
                    dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@SHIP_TO_BRANCH", queryParams.ShipToBranch))
                    dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@NO", csvData(i)(0)))
                    dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@INVOICE_NO", csvData(i)(1)))
                    dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@PO_NO", csvData(i)(2)))
                    dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@ITEM_NO", csvData(i)(3)))
                    dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@DESCRIPTION", csvData(i)(4)))
                    dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@ORDERED_PARTS", csvData(i)(5)))
                    dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@SHIPPED_PARTS", csvData(i)(6)))
                    dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@DELIVERED_QTY", csvData(i)(7)))
                    dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@CONFIRMED_QTY", csvData(i)(8)))
                    dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@TOTAL_STOCK_QTY", csvData(i)(9)))
                    dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@REASON", csvData(i)(10)))
                    dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@LOCATION1", csvData(i)(11)))
                    dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@LOCATION2", csvData(i)(12)))
                    dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@LOCATION3", csvData(i)(13)))
                    dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@HSN_CODE_OF_PARTS_RECEIVED", csvData(i)(14)))
                    dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@SUPPLY_LOCATION", csvData(i)(15)))
                    dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@DELIVERY_LOCATION", csvData(i)(16)))
                    dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@REMARKS", csvData(i)(17)))
                    dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@FILE_NAME", queryParams.FileName))
                    dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@SRC_FILE_NAME", queryParams.SrcFileName))
                    dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@INVOICE_DATE", InvoiceDate))
                    dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@GR_STATUS", GrStatus))
                    flag = dbConn.ExecSQL(sqlStr)
                    dbConn.sqlCmd.Parameters.Clear()
                    'If Error occurs then will store the flag as false
                    If Not flag Then
                        flagAll = False
                        Exit For
                    End If
                    'End If
                End If 'Other than header - End
            Next
        End If
        If flagAll Then
            flag = True
            dbConn.sqlTrn.Commit()
        Else
            flag = False
            dbConn.sqlTrn.Rollback()
        End If
        dbConn.CloseConnection()
        Return flag
    End Function

    Public Function SelectGRecieved(ByVal queryParams As GRecievedModel) As DataTable
        Log4NetControl.ComInfoLogWrite(Log4NetControl.UserID)
        Dim dbConn As DBUtility = New DBUtility()
        Dim dt As DataTable = New DataTable()
        Dim sqlStr As String = "SELECT "
        'sqlStr = sqlStr & "DELFG,UNQ_NO,UPLOAD_USER,UPLOAD_DATE,SHIP_TO_BRANCH,NO,INVOICE_NO,INVOICE_DATE,LOCAL_INVOICE_NO,ITEMS,TOTAL_QTY,TOTAL_AMOUNT,GR_DATE,CREATE_BY,GR_STATUS,FILE_NAME"
        sqlStr = sqlStr & "NO,INVOICE_NO as 'Invoice No/Delivery No',REPLACE(LEFT(CONVERT(VARCHAR, INVOICE_DATE, 101), 10),'/','.') as 'Invoice Date/Delivery Date',LOCAL_INVOICE_NO as 'Local Invoice No',ITEMS as 'Items',TOTAL_QTY as 'Total Qty',TOTAL_AMOUNT as 'Total Amount',REPLACE(LEFT(CONVERT(VARCHAR, INVOICE_DATE, 101), 10),'/','.') as 'GR Date',CREATE_BY as 'Create By',GR_STATUS as 'G/R Status' "
        sqlStr = sqlStr & "FROM "
        sqlStr = sqlStr & "G_RECEIVED "
        sqlStr = sqlStr & "WHERE "
        sqlStr = sqlStr & "DELFG=0 "
        If Not String.IsNullOrEmpty(queryParams.SrcFileName) Then
            sqlStr = sqlStr & "AND SRC_FILE_NAME = @SRC_FILE_NAME "
            dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@SRC_FILE_NAME", queryParams.SrcFileName))
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
        sqlStr = sqlStr & "  order by UNQ_NO "


        Dim _DataTable As DataTable = dbConn.GetDataSet(sqlStr)
        dbConn.CloseConnection()
        Return _DataTable
    End Function
    Public Function SelectGDRecieved(ByVal queryParams As GRecievedModel) As DataTable
        Log4NetControl.ComInfoLogWrite(Log4NetControl.UserID)
        Dim dbConn As DBUtility = New DBUtility()
        Dim dt As DataTable = New DataTable()
        Dim sqlStr As String = "SELECT "
        sqlStr = sqlStr & "UNQ_NO as 'Db No',No,INVOICE_NO as 'Invoice No',REPLACE(LEFT(CONVERT(VARCHAR, INVOICE_DATE, 101), 10),'/','.') as 'Invoice Date',PO_NO as 'PO NO',ITEM_NO as 'Item No',DESCRIPTION as 'Description',ORDERED_PARTS as 'Ordered Parts',SHIPPED_PARTS as 'Shipped Parts',DELIVERED_QTY as 'Delivered Qty',CONFIRMED_QTY as 'Confirmed Qty',TOTAL_STOCK_QTY as 'Total Stock Qty',REASON as 'Reason',LOCATION1 as 'Location1',LOCATION2 as 'Location2',LOCATION3 as 'Location3',HSN_CODE_OF_PARTS_RECEIVED as 'HSN Code of Parts Received',SUPPLY_LOCATION as 'Supply Location',DELIVERY_LOCATION as 'Delivery Location',REMARKS as 'Remarks' "
        sqlStr = sqlStr & "FROM "
        sqlStr = sqlStr & "GD_RECEIVED "
        sqlStr = sqlStr & "WHERE "
        sqlStr = sqlStr & "DELFG=0 "
        If Not String.IsNullOrEmpty(queryParams.SrcFileName) Then
            sqlStr = sqlStr & "AND SRC_FILE_NAME = @SRC_FILE_NAME "
            dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@SRC_FILE_NAME", queryParams.SrcFileName))
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

        sqlStr = sqlStr & "  order by UNQ_NO "

        Dim _DataTable As DataTable = dbConn.GetDataSet(sqlStr)
        dbConn.CloseConnection()
        Return _DataTable
    End Function
End Class
