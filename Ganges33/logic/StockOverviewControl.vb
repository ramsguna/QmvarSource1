Imports System
Imports System.Data
Imports System.Web.Security
Imports System.Collections.Generic
Imports Ganges33.Ganges33.dao
Imports Ganges33.Ganges33.model
Imports Ganges33.Ganges33.logic
Public Class StockOverviewControl
    Public Function AddModifyStockOverview(ByVal csvData()() As String, queryParams As StockOverviewModel) As Boolean


        Log4NetControl.ComInfoLogWrite(Log4NetControl.UserID)
        'Mandatory Column 12 from CSV
        Dim flag As Boolean = True
        If csvData(0).Length < 13 Then
            Return False
        End If


        '      Log4NetControl.ComInfoLogWrite(Log4NetControl.UserID)
        Dim DateTimeNow As DateTime = DateTime.Now
        Dim dtNow As DateTime = DateTimeNow.AddMinutes(ConfigurationManager.AppSettings("TimeDiff"))
        Dim dbConn As DBUtility = New DBUtility()
        Dim dt As DataTable = New DataTable()
        dbConn.sqlTrn = dbConn.sqlConn.BeginTransaction()
        dbConn.sqlCmd.Transaction = dbConn.sqlTrn
        '       Dim flag As Boolean = True
        Dim flagAll As Boolean = True
        Dim sqlStr As String = ""
        Dim dtStockOverviewExist As DataTable

        Dim isExist As Integer = 0
        '1st check PARTS_NO
        sqlStr = "SELECT TOP 1 PARTS_NO FROM STOCK_OVERVIEW "
        sqlStr = sqlStr & " WHERE DELFG = 0 AND SRC_FILE_NAME='" & queryParams.SrcFileName & "'"
        'sqlStr = sqlStr & " AND SHIP_TO_BRANCH_CODE='" & queryParams.ShipToBranchCode & "'"
        dtStockOverviewExist = dbConn.GetDataSet(sqlStr)
        'if exist then need to update delfg=1 then insert the record as new
        If (dtStockOverviewExist Is Nothing) Or (dtStockOverviewExist.Rows.Count = 0) Then
            'isExist = 0
        Else 'The records is already exist, need to update DELFg=0
            ' isExist = 1
            sqlStr = "UPDATE STOCK_OVERVIEW SET DELFG=1  "
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

                    'If isExist = 1 Then
                    sqlStr = "Insert into STOCK_OVERVIEW ("
                    sqlStr = sqlStr & "CRTDT, "
                    sqlStr = sqlStr & "CRTCD, "
                    ' sqlStr = sqlStr & "UPDDT, "
                    sqlStr = sqlStr & "UPDCD, "
                    sqlStr = sqlStr & "UPDPG, "
                    sqlStr = sqlStr & "DELFG, "
                    '           sqlStr = sqlStr & "UNQ_NO, "
                    sqlStr = sqlStr & "UPLOAD_USER, "
                    sqlStr = sqlStr & "UPLOAD_DATE, "

                    sqlStr = sqlStr & "SHIP_TO_BRANCH_CODE, "
                    sqlStr = sqlStr & "SHIP_TO_BRANCH, "
                    sqlStr = sqlStr & "PARTS_NO, "
                    sqlStr = sqlStr & "DESCRIPTION, "
                    sqlStr = sqlStr & "TOTAL_STOCK_QTY, "
                    sqlStr = sqlStr & "WAREHOUSE_STOCK_QTY, "
                    sqlStr = sqlStr & "ENGINEER_STOCK_QTY, "
                    sqlStr = sqlStr & "LOCATION1, "
                    sqlStr = sqlStr & "LOCATION2, "
                    sqlStr = sqlStr & "LOCATION3, "
                    sqlStr = sqlStr & "MAP, "
                    sqlStr = sqlStr & "TOTAL_STOCK_VALUE, "
                    sqlStr = sqlStr & "STATUS, "
                    sqlStr = sqlStr & "LATEST_MODIFY_DATE, "
                    sqlStr = sqlStr & "MONTH, "
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
                    '          sqlStr = sqlStr & " (select max(UNQ_NO)+1 from STOCK_OVERVIEW) , "
                    sqlStr = sqlStr & "@UPLOAD_USER, "
                    sqlStr = sqlStr & "@UPLOAD_DATE, "
                    sqlStr = sqlStr & "@SHIP_TO_BRANCH_CODE, "
                    sqlStr = sqlStr & "@SHIP_TO_BRANCH, "

                    sqlStr = sqlStr & "@PARTS_NO, "
                    sqlStr = sqlStr & "@DESCRIPTION, "
                    sqlStr = sqlStr & "@TOTAL_STOCK_QTY, "
                    sqlStr = sqlStr & "@WAREHOUSE_STOCK_QTY, "
                    sqlStr = sqlStr & "@ENGINEER_STOCK_QTY, "
                    sqlStr = sqlStr & "@LOCATION1, "
                    sqlStr = sqlStr & "@LOCATION2, "
                    sqlStr = sqlStr & "@LOCATION3, "
                    sqlStr = sqlStr & "@MAP, "
                    sqlStr = sqlStr & "@TOTAL_STOCK_VALUE, "
                    sqlStr = sqlStr & "@STATUS, "
                    sqlStr = sqlStr & "@LATEST_MODIFY_DATE, "
                    sqlStr = sqlStr & "@MONTH, "
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
                    dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@SHIP_TO_BRANCH_CODE", csvData(i)(0)))
                    dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@SHIP_TO_BRANCH", queryParams.ShipToBranch))

                    dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@PARTS_NO", csvData(i)(1)))
                    dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@DESCRIPTION", csvData(i)(2)))
                    dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@TOTAL_STOCK_QTY", csvData(i)(3)))
                    dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@WAREHOUSE_STOCK_QTY", csvData(i)(4)))
                    dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@ENGINEER_STOCK_QTY", csvData(i)(5)))
                    dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@LOCATION1", csvData(i)(6)))
                    dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@LOCATION2", csvData(i)(7)))
                    dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@LOCATION3", csvData(i)(8)))
                    dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@MAP", csvData(i)(9)))
                    dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@TOTAL_STOCK_VALUE", csvData(i)(10)))
                    dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@STATUS", csvData(i)(11)))
                    dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@LATEST_MODIFY_DATE", csvData(i)(12)))
                    dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@MONTH", queryParams.strMonth))
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



    Public Function AddModifyStockOverviewCount(ByVal csvData()() As String, queryParams As StockOverviewModel) As Boolean


        Log4NetControl.ComInfoLogWrite(Log4NetControl.UserID)
        'Mandatory Column 11 from CSV
        Dim flag As Boolean = True
        If csvData(0).Length < 12 Then
            Return False
        End If


        '      Log4NetControl.ComInfoLogWrite(Log4NetControl.UserID)
        Dim DateTimeNow As DateTime = DateTime.Now
        Dim dtNow As DateTime = DateTimeNow.AddMinutes(ConfigurationManager.AppSettings("TimeDiff"))
        Dim dbConn As DBUtility = New DBUtility()
        Dim dt As DataTable = New DataTable()
        dbConn.sqlTrn = dbConn.sqlConn.BeginTransaction()
        dbConn.sqlCmd.Transaction = dbConn.sqlTrn
        '       Dim flag As Boolean = True
        Dim flagAll As Boolean = True
        Dim sqlStr As String = ""
        Dim dtStockOverviewExist As DataTable

        Dim isExist As Integer = 0
        '1st check PARTS_NO
        sqlStr = "SELECT TOP 1 PARTS_NO FROM STOCK_OVERVIEW "
        sqlStr = sqlStr & " WHERE DELFG = 0 AND SRC_FILE_NAME='" & queryParams.SrcFileName & "'"
        'sqlStr = sqlStr & " AND SHIP_TO_BRANCH_CODE='" & queryParams.ShipToBranchCode & "'"
        dtStockOverviewExist = dbConn.GetDataSet(sqlStr)
        'if exist then need to update delfg=1 then insert the record as new
        If (dtStockOverviewExist Is Nothing) Or (dtStockOverviewExist.Rows.Count = 0) Then
            flagAll = False
            Return False
        Else 'The records is already exist, So can update the stock quantiy
            flagAll = True
        End If




        Dim intRtotalStockQty As Int16
        Dim intShelfStockQty As Int16
        Dim intEgOtherStockQty As Int16



        'If the record is exist
        If flagAll Then
            For i = 0 To csvData.Length - 1
                If i > 0 Then '0  Header

                    'Convert String to int 

                    Try
                        intRtotalStockQty = Integer.Parse(csvData(i)(3))
                    Catch ex As Exception
                        intRtotalStockQty = 0
                    End Try

                    Try
                        intEgOtherStockQty = Integer.Parse(csvData(i)(4))
                    Catch ex As Exception
                        intEgOtherStockQty = 0
                    End Try

                    intRtotalStockQty = intRtotalStockQty + intEgOtherStockQty

                    sqlStr = "Update STOCK_OVERVIEW  "
                    sqlStr = sqlStr & "SET "
                    'This update need to do in insert the record
                    'sqlStr = sqlStr & "MONTH= @MONTH, "
                    sqlStr = sqlStr & "UPDDT= @UPDDT, "
                    sqlStr = sqlStr & "UPDCD= @UPDCD, "
                    sqlStr = sqlStr & "R_TOTAL_STOCK_QTY= @R_TOTAL_STOCK_QTY, "
                    sqlStr = sqlStr & "SHELF_STOCK_QTY= @SHELF_STOCK_QTY, "
                    sqlStr = sqlStr & "EG_OTHER_STOCK_QTY= @EG_OTHER_STOCK_QTY "
                    sqlStr = sqlStr & "WHERE DELFG = 0 AND PARTS_NO=@PARTS_NO AND MONTH=@MONTH"
                    'This update need to do in insert the record
                    dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@UPDDT", queryParams.UPDDT))
                    dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@UPDCD", queryParams.UPDCD))
                    dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@MONTH", queryParams.strMonth))
                    dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@R_TOTAL_STOCK_QTY", intRtotalStockQty))
                    dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@SHELF_STOCK_QTY", csvData(i)(3)))
                    dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@EG_OTHER_STOCK_QTY", csvData(i)(4)))
                    dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@PARTS_NO", csvData(i)(0)))
                    'dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@SRC_FILE_NAME", queryParams.SrcFileName))


                    '''''''If isExist = 1 Then
                    ''''''sqlStr = "Insert into STOCK_OVERVIEW ("
                    ''''''sqlStr = sqlStr & "CRTDT, "
                    ''''''sqlStr = sqlStr & "CRTCD, "
                    ''''''' sqlStr = sqlStr & "UPDDT, "
                    ''''''sqlStr = sqlStr & "UPDCD, "
                    ''''''sqlStr = sqlStr & "UPDPG, "
                    ''''''sqlStr = sqlStr & "DELFG, "
                    '''''''           sqlStr = sqlStr & "UNQ_NO, "
                    ''''''sqlStr = sqlStr & "UPLOAD_USER, "
                    ''''''sqlStr = sqlStr & "UPLOAD_DATE, "

                    ''''''sqlStr = sqlStr & "SHIP_TO_BRANCH_CODE, "
                    ''''''sqlStr = sqlStr & "SHIP_TO_BRANCH, "
                    ''''''sqlStr = sqlStr & "PARTS_NO, "
                    ''''''sqlStr = sqlStr & "DESCRIPTION, "
                    ''''''sqlStr = sqlStr & "TOTAL_STOCK_QTY, "
                    ''''''sqlStr = sqlStr & "WAREHOUSE_STOCK_QTY, "
                    ''''''sqlStr = sqlStr & "ENGINEER_STOCK_QTY, "
                    ''''''sqlStr = sqlStr & "LOCATION1, "
                    ''''''sqlStr = sqlStr & "LOCATION2, "
                    ''''''sqlStr = sqlStr & "LOCATION3, "
                    ''''''sqlStr = sqlStr & "MAP, "
                    ''''''sqlStr = sqlStr & "TOTAL_STOCK_VALUE, "
                    ''''''sqlStr = sqlStr & "STATUS, "
                    ''''''sqlStr = sqlStr & "LATEST_MODIFY_DATE, "
                    ''''''sqlStr = sqlStr & "FILE_NAME, "
                    ''''''sqlStr = sqlStr & "SRC_FILE_NAME "
                    ''''''sqlStr = sqlStr & " ) "
                    ''''''sqlStr = sqlStr & " values ( "
                    ''''''sqlStr = sqlStr & "@CRTDT, "
                    ''''''sqlStr = sqlStr & "@CRTCD, "
                    '''''''sqlStr = sqlStr & "@UPDDT, "
                    ''''''sqlStr = sqlStr & "@UPDCD, "
                    ''''''sqlStr = sqlStr & "@UPDPG, "
                    ''''''sqlStr = sqlStr & "@DELFG, "
                    '''''''          sqlStr = sqlStr & " (select max(UNQ_NO)+1 from STOCK_OVERVIEW) , "
                    ''''''sqlStr = sqlStr & "@UPLOAD_USER, "
                    ''''''sqlStr = sqlStr & "@UPLOAD_DATE, "
                    ''''''sqlStr = sqlStr & "@SHIP_TO_BRANCH_CODE, "
                    ''''''sqlStr = sqlStr & "@SHIP_TO_BRANCH, "

                    ''''''sqlStr = sqlStr & "@PARTS_NO, "
                    ''''''sqlStr = sqlStr & "@DESCRIPTION, "
                    ''''''sqlStr = sqlStr & "@TOTAL_STOCK_QTY, "
                    ''''''sqlStr = sqlStr & "@WAREHOUSE_STOCK_QTY, "
                    ''''''sqlStr = sqlStr & "@ENGINEER_STOCK_QTY, "
                    ''''''sqlStr = sqlStr & "@LOCATION1, "
                    ''''''sqlStr = sqlStr & "@LOCATION2, "
                    ''''''sqlStr = sqlStr & "@LOCATION3, "
                    ''''''sqlStr = sqlStr & "@MAP, "
                    ''''''sqlStr = sqlStr & "@TOTAL_STOCK_VALUE, "
                    ''''''sqlStr = sqlStr & "@STATUS, "
                    ''''''sqlStr = sqlStr & "@LATEST_MODIFY_DATE, "
                    ''''''sqlStr = sqlStr & "@FILE_NAME, "
                    ''''''sqlStr = sqlStr & "@SRC_FILE_NAME "
                    ''''''sqlStr = sqlStr & " )"
                    ''''''dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@CRTDT", dtNow))
                    ''''''dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@CRTCD", queryParams.UserId))
                    ''''''dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@UPDCD", "")) '?????????????????????????
                    ''''''dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@UPDPG", queryParams.UPDPG))
                    ''''''dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@DELFG", 0))
                    ''''''dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@UPLOAD_USER", queryParams.UploadUser))
                    ''''''dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@UPLOAD_DATE", dtNow))
                    ''''''dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@SHIP_TO_BRANCH_CODE", csvData(i)(0)))
                    ''''''dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@SHIP_TO_BRANCH", queryParams.ShipToBranch))

                    ''''''dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@PARTS_NO", csvData(i)(1)))
                    ''''''dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@DESCRIPTION", csvData(i)(2)))
                    ''''''dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@TOTAL_STOCK_QTY", csvData(i)(3)))
                    ''''''dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@WAREHOUSE_STOCK_QTY", csvData(i)(4)))
                    ''''''dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@ENGINEER_STOCK_QTY", csvData(i)(5)))
                    ''''''dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@LOCATION1", csvData(i)(6)))
                    ''''''dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@LOCATION2", csvData(i)(7)))
                    ''''''dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@LOCATION3", csvData(i)(8)))
                    ''''''dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@MAP", csvData(i)(9)))
                    ''''''dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@TOTAL_STOCK_VALUE", csvData(i)(10)))
                    ''''''dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@STATUS", csvData(i)(11)))
                    ''''''dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@LATEST_MODIFY_DATE", csvData(i)(12)))
                    ''''''dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@FILE_NAME", queryParams.FileName))
                    ''''''dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@SRC_FILE_NAME", queryParams.SrcFileName))

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

    Public Function SelectStockOverview(ByVal queryParams As StockOverviewModel) As DataTable
        Log4NetControl.ComInfoLogWrite(Log4NetControl.UserID)
        Dim dbConn As DBUtility = New DBUtility()
        Dim dt As DataTable = New DataTable()
        Dim sqlStr As String = "SELECT "
        'sqlStr = sqlStr & "DELFG,UNQ_NO,UPLOAD_USER,UPLOAD_DATE,BRANCH,PARTS_NO,DESCRIPTION,TOTAL_STOCK_QTY,WAREHOUSE_STOCK_QTY,ENGINEER_STOCK_QTY,LOCATION1,LOCATION2,LOCATION3,MAP,TOTAL_STOCK_VALUE,STATUS,LATEST_MODIFY_DATE,FILE_NAME"
        sqlStr = sqlStr & "SHIP_TO_BRANCH_CODE as 'Branch',PARTS_NO as 'Parts No',DESCRIPTION as 'Description',TOTAL_STOCK_QTY as 'Total Stock Qty',WAREHOUSE_STOCK_QTY as 'Warehouse Stock Qty',ENGINEER_STOCK_QTY as 'Engineer Stock Qty',LOCATION1 as 'Location1',LOCATION2 as 'Location2',LOCATION3 as 'Location3',MAP,TOTAL_STOCK_VALUE as 'Total Stock Value',STATUS,LEFT(CONVERT(VARCHAR, LATEST_MODIFY_DATE, 112), 10) as 'Latest modify date' "
        sqlStr = sqlStr & "FROM "
        sqlStr = sqlStr & "STOCK_OVERVIEW "
        sqlStr = sqlStr & "WHERE "
        sqlStr = sqlStr & "DELFG=0 "
        If Not String.IsNullOrEmpty(queryParams.SrcFileName) Then
            sqlStr = sqlStr & "AND SRC_FILE_NAME = @SRC_FILE_NAME "
            dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@SRC_FILE_NAME", queryParams.SrcFileName))
        End If
        Dim _DataTable As DataTable = dbConn.GetDataSet(sqlStr)
        dbConn.CloseConnection()
        Return _DataTable
    End Function
    Public Function SelectStockOverviewCount(ByVal queryParams As StockOverviewModel) As DataTable
        Log4NetControl.ComInfoLogWrite(Log4NetControl.UserID)
        Dim dbConn As DBUtility = New DBUtility()
        Dim dt As DataTable = New DataTable()
        Dim sqlStr As String = "SELECT "
        'sqlStr = sqlStr & "DELFG,UNQ_NO,UPLOAD_USER,UPLOAD_DATE,BRANCH,PARTS_NO,DESCRIPTION,TOTAL_STOCK_QTY,WAREHOUSE_STOCK_QTY,ENGINEER_STOCK_QTY,LOCATION1,LOCATION2,LOCATION3,MAP,TOTAL_STOCK_VALUE,STATUS,LATEST_MODIFY_DATE,FILE_NAME"
        sqlStr = sqlStr & "PARTS_NO as 'Parts No',DESCRIPTION as 'Description','' as 'Total Stock Qty','' as 'Warehouse Stock Qty','' as 'Engineer Stock Qty',LOCATION1 as 'Location1',LOCATION2 as 'Location2',LOCATION3 as 'Location3',MAP,TOTAL_STOCK_VALUE as 'Total Stock Value',STATUS,LEFT(CONVERT(VARCHAR, LATEST_MODIFY_DATE, 112), 10) as 'Latest modify date' "
        sqlStr = sqlStr & "FROM "
        sqlStr = sqlStr & "STOCK_OVERVIEW "
        sqlStr = sqlStr & "WHERE "
        sqlStr = sqlStr & "DELFG=0 "
        If Not String.IsNullOrEmpty(queryParams.SrcFileName) Then
            sqlStr = sqlStr & "AND SRC_FILE_NAME = @SRC_FILE_NAME "
            dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@SRC_FILE_NAME", queryParams.SrcFileName))
        End If
        Dim _DataTable As DataTable = dbConn.GetDataSet(sqlStr)
        dbConn.CloseConnection()
        Return _DataTable
    End Function

    Public Function SelectStockOverviewCountDisplay(ByVal queryParams As StockOverviewModel) As DataTable
        '''''''Log4NetControl.ComInfoLogWrite(Log4NetControl.UserID)
        '''''''Dim dbConn As DBUtility = New DBUtility()
        '''''''Dim dt As DataTable = New DataTable()
        '''''''Dim sqlStr As String = "SELECT "
        '''''''sqlStr = sqlStr & " count(*) as 'TOTAL_RECORDS' "
        '''''''sqlStr = sqlStr & " ,sum([TOTAL_STOCK_QTY]) as 'TOTAL_STOCK_QTY' "
        '''''''sqlStr = sqlStr & " ,sum([WAREHOUSE_STOCK_QTY]) as 'WAREHOUSE_STOCK_QTY' "
        '''''''sqlStr = sqlStr & " ,sum([ENGINEER_STOCK_QTY]) as 'ENGINEER_STOCK_QTY' "
        '''''''sqlStr = sqlStr & " ,sum([R_TOTAL_STOCK_QTY]) as 'R_TOTAL_STOCK_QTY' "
        '''''''sqlStr = sqlStr & " ,sum([SHELF_STOCK_QTY]) as 'SHELF_STOCK_QTY' "
        '''''''sqlStr = sqlStr & " ,sum([EG_OTHER_STOCK_QTY]) as 'EG_OTHER_STOCK_QTY' "
        '''''''sqlStr = sqlStr & " ,IsNull((select TOP 1 LATEST_MODIFY_DATE FROM [dbo].[STOCK_OVERVIEW] WHERE DELFG=0 AND SHIP_TO_BRANCH_CODE=  @SHIP_TO_BRANCH_CODE AND MONTH = @MONTH),'') AS LATEST_MODIFY_DATE "
        '''''''sqlStr = sqlStr & " ,IsNull((select TOP 1 UPDCD FROM [dbo].[STOCK_OVERVIEW] WHERE DELFG=0 AND SHIP_TO_BRANCH_CODE= @SHIP_TO_BRANCH_CODE AND MONTH = @MONTH),'') AS UPDCD "
        '''''''sqlStr = sqlStr & "FROM "
        '''''''sqlStr = sqlStr & "STOCK_OVERVIEW "
        '''''''sqlStr = sqlStr & "WHERE "
        '''''''sqlStr = sqlStr & "DELFG=0 "
        '''''''If Not String.IsNullOrEmpty(queryParams.ShipToBranchCode) Then
        '''''''    sqlStr = sqlStr & "AND SHIP_TO_BRANCH_CODE = @SHIP_TO_BRANCH_CODE "
        '''''''    dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@SHIP_TO_BRANCH_CODE", queryParams.ShipToBranchCode))
        '''''''End If
        '''''''If Not String.IsNullOrEmpty(queryParams.strMonth) Then
        '''''''    sqlStr = sqlStr & "AND MONTH = @MONTH "
        '''''''    dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@MONTH", queryParams.strMonth))
        '''''''End If
        '''''''Dim _DataTable As DataTable = dbConn.GetDataSet(sqlStr)
        '''''''dbConn.CloseConnection()
        '''''''Return _DataTable
        '''
        Log4NetControl.ComInfoLogWrite(Log4NetControl.UserID)
        Dim dbConn As DBUtility = New DBUtility()
        Dim dt As DataTable = New DataTable()
        Dim sqlStr As String = "SELECT "

        sqlStr = sqlStr & "TOTAL_STOCK_QTY  "
        sqlStr = sqlStr & ",WAREHOUSE_STOCK_QTY "
        sqlStr = sqlStr & ",ENGINEER_STOCK_QTY "
        sqlStr = sqlStr & ",(SHELF_STOCK_QTY + EG_OTHER_STOCK_QTY) - TOTAL_STOCK_QTY as 'DIFFERENCE' "
        sqlStr = sqlStr & ",R_TOTAL_STOCK_QTY "
        sqlStr = sqlStr & ",SHELF_STOCK_QTY "
        sqlStr = sqlStr & ",EG_OTHER_STOCK_QTY "
        sqlStr = sqlStr & ",LATEST_MODIFY_DATE "
        sqlStr = sqlStr & ",UPDCD "
        sqlStr = sqlStr & "FROM "
        sqlStr = sqlStr & "STOCK_OVERVIEW "
        sqlStr = sqlStr & "WHERE "
        sqlStr = sqlStr & "DELFG=0 "
        If Not String.IsNullOrEmpty(queryParams.ShipToBranchCode) Then
            sqlStr = sqlStr & "AND SHIP_TO_BRANCH_CODE = @SHIP_TO_BRANCH_CODE "
            dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@SHIP_TO_BRANCH_CODE", queryParams.ShipToBranchCode))
        End If
        If Not String.IsNullOrEmpty(queryParams.strMonth) Then
            sqlStr = sqlStr & "AND MONTH = @MONTH "
            dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@MONTH", queryParams.strMonth))
        End If
        Dim _DataTable As DataTable = dbConn.GetDataSet(sqlStr)
        dbConn.CloseConnection()
        Return _DataTable
    End Function

    Public Function SelectStockOverviewCountExport(ByVal queryParams As StockOverviewModel) As DataTable
        Log4NetControl.ComInfoLogWrite(Log4NetControl.UserID)
        Dim dbConn As DBUtility = New DBUtility()
        Dim dt As DataTable = New DataTable()
        Dim sqlStr As String = "SELECT "
        sqlStr = sqlStr & " [SHIP_TO_BRANCH_CODE] "
        sqlStr = sqlStr & " ,[PARTS_NO] "
        sqlStr = sqlStr & " ,[DESCRIPTION] "
        sqlStr = sqlStr & " ,[TOTAL_STOCK_QTY]"
        sqlStr = sqlStr & " ,[WAREHOUSE_STOCK_QTY] "
        sqlStr = sqlStr & " ,[ENGINEER_STOCK_QTY] "
        sqlStr = sqlStr & " ,(SHELF_STOCK_QTY + EG_OTHER_STOCK_QTY) - TOTAL_STOCK_QTY as 'DIFFERENCE' "
        sqlStr = sqlStr & " ,[R_TOTAL_STOCK_QTY] AS 'count total' "
        sqlStr = sqlStr & " ,[SHELF_STOCK_QTY] AS 'shelf' "
        sqlStr = sqlStr & " ,[EG_OTHER_STOCK_QTY] AS 'eg-hold' "
        sqlStr = sqlStr & " ,[LOCATION1] "
        sqlStr = sqlStr & " ,[LOCATION2] "
        sqlStr = sqlStr & " ,[LOCATION3] "
        sqlStr = sqlStr & " ,[MAP] "
        sqlStr = sqlStr & " ,[TOTAL_STOCK_VALUE] "
        sqlStr = sqlStr & " ,[STATUS] "
        sqlStr = sqlStr & " ,[LATEST_MODIFY_DATE] "
        sqlStr = sqlStr & "  ,[MONTH] "
        sqlStr = sqlStr & "FROM "
        sqlStr = sqlStr & "STOCK_OVERVIEW "
        sqlStr = sqlStr & "WHERE "
        sqlStr = sqlStr & "DELFG=0 "
        If Not String.IsNullOrEmpty(queryParams.ShipToBranchCode) Then
            sqlStr = sqlStr & "AND SHIP_TO_BRANCH_CODE = @SHIP_TO_BRANCH_CODE "
            dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@SHIP_TO_BRANCH_CODE", queryParams.ShipToBranchCode))
        End If
        If Not String.IsNullOrEmpty(queryParams.strMonth) Then
            sqlStr = sqlStr & "AND MONTH = @MONTH "
            dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@MONTH", queryParams.strMonth))
        End If
        Dim _DataTable As DataTable = dbConn.GetDataSet(sqlStr)
        dbConn.CloseConnection()
        Return _DataTable
    End Function

End Class
