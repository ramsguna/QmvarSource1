Imports System
Imports System.Data
Imports System.Web.Security
Imports System.Collections.Generic
Imports Ganges33.Ganges33.dao
Imports Ganges33.Ganges33.model
Imports Ganges33.Ganges33.logic
Public Class SonyStocksControl
    Public Function AddModifyStocks(ByVal csvData()() As String, queryParams As SonyStocksModel) As Boolean
        Log4NetControl.ComInfoLogWrite(Log4NetControl.UserID)
        'Mandatory Column 14 from CSV
        Dim flag As Boolean = True
        If csvData(0).Length < 14 Then
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
        Dim dtTableExist As DataTable

        Dim isExist As Integer = 0
        '1st check PARTS_NO
        sqlStr = "SELECT TOP 1 LOCATION_ID FROM SONY_STOCKS "
        sqlStr = sqlStr & " WHERE DELFG = 0 AND SRC_FILE_NAME='" & queryParams.SRC_FILE_NAME & "'"
        'sqlStr = sqlStr & " AND SHIP_TO_BRANCH_CODE='" & queryParams.ShipToBranchCode & "'"
        dtTableExist = dbConn.GetDataSet(sqlStr)
        'if exist then need to update delfg=1 then insert the record as new
        If (dtTableExist Is Nothing) Or (dtTableExist.Rows.Count = 0) Then
            'isExist = 0
        Else 'The records is already exist, need to update DELFg=0
            ' isExist = 1
            sqlStr = "UPDATE SONY_STOCKS SET DELFG=1  "
            sqlStr = sqlStr & "WHERE DELFG=0 "
            sqlStr = sqlStr & "AND SRC_FILE_NAME = @SRC_FILE_NAME"
            'sqlStr = sqlStr & "AND SHIP_TO_BRANCH_CODE = @SHIP_TO_BRANCH_CODE "
            dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@SRC_FILE_NAME", queryParams.SRC_FILE_NAME))
            flag = dbConn.ExecSQL(sqlStr)
            dbConn.sqlCmd.Parameters.Clear()
            'If Error occurs then will store the flag as false
            If Not flag Then
                flagAll = False
            End If
        End If
        For i = 0 To csvData.Length - 1
            If i > 0 Then '0  Header
                'If isExist = 1 Then
                sqlStr = "Insert into SONY_STOCKS ("
                sqlStr = sqlStr & "CRTDT, "
                sqlStr = sqlStr & "CRTCD, "
                ' sqlStr = sqlStr & "UPDDT, "
                sqlStr = sqlStr & "UPDCD, "
                sqlStr = sqlStr & "UPDPG, "
                sqlStr = sqlStr & "DELFG, "
                '             sqlStr = sqlStr & "UNQ_NO, "
                sqlStr = sqlStr & "UPLOAD_USER, "
                sqlStr = sqlStr & "UPLOAD_DATE, "
                sqlStr = sqlStr & "SHIP_TO_BRANCH_CODE, "
                sqlStr = sqlStr & "SHIP_TO_BRANCH, "

                sqlStr = sqlStr & "SC_NAME, "
                sqlStr = sqlStr & "SC_CODE, "
                sqlStr = sqlStr & "REGION, "
                sqlStr = sqlStr & "PART_NO, "
                sqlStr = sqlStr & "PART_NAME_LOCATION, "
                sqlStr = sqlStr & "PART_NAME_ENGLISH, "
                sqlStr = sqlStr & "STOCK_ID, "
                sqlStr = sqlStr & "STORAGE_DATE, "
                sqlStr = sqlStr & "LOCATION_ID, "
                sqlStr = sqlStr & "SKU_TYPE, "
                sqlStr = sqlStr & "SKU_QTY, "
                sqlStr = sqlStr & "SN_ID, "
                sqlStr = sqlStr & "PRICE, "
                sqlStr = sqlStr & "OWNERSHIP, "
                sqlStr = sqlStr & "INVENTORY_TYPE, "
                sqlStr = sqlStr & "INVENTORY_STATUS, "
                sqlStr = sqlStr & "PART_NATION_LEVEL, "
                sqlStr = sqlStr & "MARK, "
                sqlStr = sqlStr & "PO_NO, "
                sqlStr = sqlStr & "PO_TYPE, "
                sqlStr = sqlStr & "PO_DATE, "
                sqlStr = sqlStr & "PART_CLAIM_ID, "


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
                '              sqlStr = sqlStr & " (select max(UNQ_NO)+1 from SAW_DISCOUNT) , "
                sqlStr = sqlStr & "@UPLOAD_USER, "
                sqlStr = sqlStr & "@UPLOAD_DATE, "
                sqlStr = sqlStr & "@SHIP_TO_BRANCH_CODE, "
                sqlStr = sqlStr & "@SHIP_TO_BRANCH, "

                sqlStr = sqlStr & "@SC_NAME, "
                sqlStr = sqlStr & "@SC_CODE, "
                sqlStr = sqlStr & "@REGION, "
                sqlStr = sqlStr & "@PART_NO, "
                sqlStr = sqlStr & "@PART_NAME_LOCATION, "
                sqlStr = sqlStr & "@PART_NAME_ENGLISH, "
                sqlStr = sqlStr & "@STOCK_ID, "
                sqlStr = sqlStr & "@STORAGE_DATE, "
                sqlStr = sqlStr & "@LOCATION_ID, "
                sqlStr = sqlStr & "@SKU_TYPE, "
                sqlStr = sqlStr & "@SKU_QTY, "
                sqlStr = sqlStr & "@SN_ID, "
                sqlStr = sqlStr & "@PRICE, "
                sqlStr = sqlStr & "@OWNERSHIP, "
                sqlStr = sqlStr & "@INVENTORY_TYPE, "
                sqlStr = sqlStr & "@INVENTORY_STATUS, "
                sqlStr = sqlStr & "@PART_NATION_LEVEL, "
                sqlStr = sqlStr & "@MARK, "
                sqlStr = sqlStr & "@PO_NO, "
                sqlStr = sqlStr & "@PO_TYPE, "
                sqlStr = sqlStr & "@PO_DATE, "
                sqlStr = sqlStr & "@PART_CLAIM_ID, "
                sqlStr = sqlStr & "@FILE_NAME, "
                sqlStr = sqlStr & "@SRC_FILE_NAME "
                sqlStr = sqlStr & " )"
                dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@CRTDT", dtNow))
                dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@CRTCD", queryParams.UserId))
                dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@UPDCD", "")) '?????????????????????????
                dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@UPDPG", queryParams.UPDPG))
                dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@DELFG", 0))
                dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@UPLOAD_USER", queryParams.UPLOAD_USER))
                dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@UPLOAD_DATE", dtNow))
                dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@SHIP_TO_BRANCH_CODE", queryParams.SHIP_TO_BRANCH_CODE))
                dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@SHIP_TO_BRANCH", queryParams.SHIP_TO_BRANCH))

                dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@SC_NAME", csvData(i)(0)))
                dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@SC_CODE", csvData(i)(1)))
                dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@REGION", csvData(i)(2)))
                dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@PART_NO", csvData(i)(3)))
                dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@PART_NAME_LOCATION", csvData(i)(4)))
                dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@PART_NAME_ENGLISH", csvData(i)(5)))
                dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@STOCK_ID", csvData(i)(6)))
                dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@STORAGE_DATE", csvData(i)(7)))
                dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@LOCATION_ID", csvData(i)(8)))
                dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@SKU_TYPE", csvData(i)(9)))
                dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@SKU_QTY", csvData(i)(10)))
                dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@SN_ID", csvData(i)(11)))
                dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@PRICE", csvData(i)(12)))
                dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@OWNERSHIP", csvData(i)(13)))
                dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@INVENTORY_TYPE", csvData(i)(14)))
                dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@INVENTORY_STATUS", csvData(i)(15)))
                dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@PART_NATION_LEVEL", csvData(i)(16)))
                dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@MARK", csvData(i)(17)))
                dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@PO_NO", csvData(i)(18)))
                dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@PO_TYPE", csvData(i)(19)))
                dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@PO_DATE", csvData(i)(20)))
                dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@PART_CLAIM_ID", csvData(i)(21)))


                dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@FILE_NAME", queryParams.FILE_NAME))
                dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@SRC_FILE_NAME", queryParams.SRC_FILE_NAME))

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


    Public Function SelectStocks(ByVal queryParams As SonyStocksModel) As DataTable
        Log4NetControl.ComInfoLogWrite(Log4NetControl.UserID)
        Dim dbConn As DBUtility = New DBUtility()
        Dim dt As DataTable = New DataTable()
        Dim sqlStr As String = "SELECT "
        sqlStr = sqlStr & "SC_NAME as 'SC Name',SC_CODE as 'SC Code',REGION as 'Region',PART_NO as 'Part No',PART_NAME_LOCATION as 'Part Name Location',PART_NAME_ENGLISH as 'Part Name English',STOCK_ID as 'Stock ID',STORAGE_DATE as 'Storage Date',LOCATION_ID as 'Location ID',SKU_TYPE as 'SKU Type',SKU_QTY as 'SKU QTY',SN_ID as 'S/N ID',PRICE as 'Price',OWNERSHIP as 'OwnerShip',INVENTORY_TYPE as 'Inventory Type',INVENTORY_STATUS as 'Inventory Status',PART_NATION_LEVEL as 'Part Nation Level',MARK as 'Mark',PO_NO as 'PO NO',PO_TYPE as 'PO TYPE',PO_DATE  as 'PO DATE',PART_CLAIM_ID as 'Part Claim ID' "
        sqlStr = sqlStr & "FROM "
        sqlStr = sqlStr & "SONY_STOCKS "
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
            sqlStr = sqlStr & "AND LEFT(CONVERT(VARCHAR, CRTDT, 111), 10) >= @DateFrom and LEFT(CONVERT(VARCHAR, CRTDT, 111), 10) <= @DateTo "
            'sqlStr = sqlStr & "AND INVOICE_DATE >= @DateFrom and INVOICE_DATE <= @DateTo "
            dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@DateFrom", queryParams.DateFrom))
            dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@DateTo", queryParams.DateTo))
        ElseIf Not String.IsNullOrEmpty(queryParams.DateFrom) Then
            'sqlStr = sqlStr & "AND LEFT(CONVERT(VARCHAR, INVOICE_DATE, 111), 10) = @DateFrom "
            sqlStr = sqlStr & "AND CRTDT = @DateFrom "
            dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@DateFrom", queryParams.DateFrom))
        ElseIf Not String.IsNullOrEmpty(queryParams.DateTo) Then
            sqlStr = sqlStr & "AND CRTDT = @DateTo "
            'sqlStr = sqlStr & "AND LEFT(CONVERT(VARCHAR, INVOICE_DATE, 111), 10) = @DateTo "
            dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@DateTo", queryParams.DateTo))
        End If

        Dim _DataTable As DataTable = dbConn.GetDataSet(sqlStr)
        dbConn.CloseConnection()
        Return _DataTable
    End Function
End Class
