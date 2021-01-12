Imports System
Imports System.Data
Imports System.Web.Security
Imports System.Collections.Generic
Imports Ganges33.Ganges33.dao
Imports Ganges33.Ganges33.model
Imports Ganges33.Ganges33.logic
Public Class ActivityReportControl
    Public Function AddModifyActivityReport(ByVal csvData()() As String, queryParams As ActivityReportModel) As Boolean
        Log4NetControl.ComInfoLogWrite(Log4NetControl.UserID)
        'Mandatory Column 8 from CSV
        Dim flag As Boolean = True
        If csvData(0).Length < 9 Then
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
        Dim dtSawDiscountExist As DataTable

        Dim isExist As Integer = 0
        '1st check PARTS_NO
        sqlStr = "SELECT TOP 1 unq FROM Activity_report "
        sqlStr = sqlStr & " WHERE DELFG = 0 AND SRC_FILE_NAME='" & queryParams.SRC_FILE_NAME & "'"
        'sqlStr = sqlStr & " AND SHIP_TO_BRANCH_CODE='" & queryParams.ShipToBranchCode & "'"
        dtSawDiscountExist = dbConn.GetDataSet(sqlStr)
        'if exist then need to update delfg=1 then insert the record as new
        If (dtSawDiscountExist Is Nothing) Or (dtSawDiscountExist.Rows.Count = 0) Then
            'isExist = 0
        Else 'The records is already exist, need to update DELFg=0
            ' isExist = 1
            sqlStr = "UPDATE Activity_report SET DELFG=1  "
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
        For i = 2 To csvData.Length - 1
            If i > 0 Then '0  Header
                'If isExist = 1 Then
                sqlStr = "Insert into Activity_report ("
                sqlStr = sqlStr & "CRTDT, "
                sqlStr = sqlStr & "CRTCD, "
                ' sqlStr = sqlStr & "UPDDT, "
                sqlStr = sqlStr & "UPDCD, "
                sqlStr = sqlStr & "UPDPG, "
                sqlStr = sqlStr & "DELFG, "
                '             sqlStr = sqlStr & "UNQ_NO, "
                sqlStr = sqlStr & "update_user, "
                sqlStr = sqlStr & "update_datetime, "
                sqlStr = sqlStr & "SHIP_TO_BRANCH_CODE, "
                sqlStr = sqlStr & "SHIP_TO_BRANCH, "

                sqlStr = sqlStr & "month, "
                sqlStr = sqlStr & "day, "
                sqlStr = sqlStr & "note, "
                sqlStr = sqlStr & "Customer_Visit, "
                sqlStr = sqlStr & "Call_Registerd, "
                sqlStr = sqlStr & "Repair_Completed, "
                sqlStr = sqlStr & "Goods_Delivered, "
                sqlStr = sqlStr & "Pending_Calls, "
                sqlStr = sqlStr & "Cancelled_Calls, "
                sqlStr = sqlStr & "location, "
                sqlStr = sqlStr & "FILE_NAME, "
                sqlStr = sqlStr & "SRC_FILE_NAME, "
                sqlStr = sqlStr & "DATE_TIME_RPA_CREATE, "
                sqlStr = sqlStr & "UPLOAD_USER, "
                sqlStr = sqlStr & "UPLOAD_DATE "
                sqlStr = sqlStr & " ) "
                sqlStr = sqlStr & " values ( "
                sqlStr = sqlStr & "@CRTDT, "
                sqlStr = sqlStr & "@CRTCD, "
                'sqlStr = sqlStr & "@UPDDT, "
                sqlStr = sqlStr & "@UPDCD, "
                sqlStr = sqlStr & "@UPDPG, "
                sqlStr = sqlStr & "@DELFG, "
                '              sqlStr = sqlStr & " (select max(UNQ_NO)+1 from SAW_DISCOUNT) , "
                sqlStr = sqlStr & "@update_user, "
                sqlStr = sqlStr & "@update_datetime, "
                sqlStr = sqlStr & "@SHIP_TO_BRANCH_CODE, "
                sqlStr = sqlStr & "@SHIP_TO_BRANCH, "

                sqlStr = sqlStr & "@month, "
                sqlStr = sqlStr & "@day, "
                sqlStr = sqlStr & "@note, "
                sqlStr = sqlStr & "@Customer_Visit, "
                sqlStr = sqlStr & "@Call_Registerd, "
                sqlStr = sqlStr & "@Repair_Completed, "
                sqlStr = sqlStr & "@Goods_Delivered, "
                sqlStr = sqlStr & "@Pending_Calls, "
                sqlStr = sqlStr & "@Cancelled_Calls, "
                sqlStr = sqlStr & "@location, "

                sqlStr = sqlStr & "@FILE_NAME, "
                sqlStr = sqlStr & "@SRC_FILE_NAME, "
                sqlStr = sqlStr & "@DATE_TIME_RPA_CREATE, "
                sqlStr = sqlStr & "@UPLOAD_USER, "
                sqlStr = sqlStr & "@UPLOAD_DATE "
                sqlStr = sqlStr & " )"
                dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@CRTDT", dtNow))
                dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@CRTCD", queryParams.UserId))
                dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@UPDCD", "")) '?????????????????????????
                dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@UPDPG", queryParams.UPDPG))
                dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@DELFG", 0))
                'dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@UPLOAD_USER", queryParams.UploadUser))
                'dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@UPLOAD_DATE", dtNow))

                'The branch code is comes with file name as well as it will be selected the branch in dropdownlist
                'So we did not used csvData(i)(3) 'ship to' from the CSV file
                dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@SHIP_TO_BRANCH_CODE", queryParams.SHIP_TO_BRANCH_CODE))
                dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@SHIP_TO_BRANCH", queryParams.SHIP_TO_BRANCH))
                dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@update_user", queryParams.UpdateUser))
                dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@update_datetime", dtNow))

                dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@month", csvData(i)(0)))
                dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@day", csvData(i)(1)))
                dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@note", csvData(i)(9)))
                dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@Customer_Visit", csvData(i)(3)))
                dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@Call_Registerd", csvData(i)(4)))
                dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@Repair_Completed", csvData(i)(5)))
                dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@Goods_Delivered", csvData(i)(6)))
                dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@Pending_Calls", csvData(i)(7)))
                dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@Cancelled_Calls", csvData(i)(8)))
                dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@location", csvData(i)(2)))




                dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@FILE_NAME", queryParams.FILE_NAME))
                dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@SRC_FILE_NAME", queryParams.SRC_FILE_NAME))
                dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@DATE_TIME_RPA_CREATE", queryParams.DATE_TIME_RPA_CREATE))
                dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@UPLOAD_USER", queryParams.UPLOAD_DATE))
                dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@UPLOAD_DATE", dtNow))

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


    Public Function SelectActivityReport(ByVal queryParams As ActivityReportModel) As DataTable
        Log4NetControl.ComInfoLogWrite(Log4NetControl.UserID)
        Dim dbConn As DBUtility = New DBUtility()
        Dim dt As DataTable = New DataTable()
        Dim sqlStr As String = "SELECT "
        sqlStr = sqlStr & " DATE_TIME_RPA_CREATE as 'Date Time RPA Create',,month,day,note,	LastUpdt_user as 'Last Updated User',	Billinb_user as 'Billing User',	LEFT(CONVERT(VARCHAR, Billing_date, 103), 10) as 'Billing Date',	GoodsDelivered_date as 'Goods Delivered Date',	Branch_name as 'Branch Name',	Engineer,		Product_1,	Product_2,	IW_Labor,	IW_Parts,	IW_Transport,	IW_Others,	IW_Tax,	IW_total,	OW_Labor,	OW_Parts,	OW_Transport,	OW_Others,	OW_Tax	OW_total  "
        sqlStr = sqlStr & "FROM "
        sqlStr = sqlStr & "Activity_report "
        sqlStr = sqlStr & "WHERE "
        sqlStr = sqlStr & "DELFG=0 "

        'If Not String.IsNullOrEmpty(queryParams.ShipToBranch) Then
        '    sqlStr = sqlStr & "AND Branch_name = @ShipToBranch  "
        '    dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@ShipToBranch", queryParams.ShipToBranch))
        'End If

        'If (Not (String.IsNullOrEmpty(queryParams.DateFrom)) And (Not (String.IsNullOrEmpty(queryParams.DateTo)))) Then
        '    sqlStr = sqlStr & "AND LEFT(CONVERT(VARCHAR, Billing_date, 111), 10) >= @DateFrom and LEFT(CONVERT(VARCHAR, Billing_date, 111), 10) <= @DateTo "
        '    'sqlStr = sqlStr & "AND INVOICE_DATE >= @DateFrom and INVOICE_DATE <= @DateTo "
        '    dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@DateFrom", queryParams.DateFrom))
        '    dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@DateTo", queryParams.DateTo))
        'ElseIf Not String.IsNullOrEmpty(queryParams.DateFrom) Then
        '    'sqlStr = sqlStr & "AND LEFT(CONVERT(VARCHAR, INVOICE_DATE, 111), 10) = @DateFrom "
        '    sqlStr = sqlStr & "AND Billing_date = @DateFrom "
        '    dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@DateFrom", queryParams.DateFrom))
        'ElseIf Not String.IsNullOrEmpty(queryParams.DateTo) Then
        '    sqlStr = sqlStr & "AND Billing_date = @DateTo "
        '    'sqlStr = sqlStr & "AND LEFT(CONVERT(VARCHAR, INVOICE_DATE, 111), 10) = @DateTo "
        '    dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@DateTo", queryParams.DateTo))
        'End If


        Dim _DataTable As DataTable = dbConn.GetDataSet(sqlStr)
        dbConn.CloseConnection()
        Return _DataTable
    End Function

    Public Function GetAnalysis_ReportDate(queryParams As ActivityReportModel) As DataTable

        Dim Analysis_ReportModel As ActivityReportModel = New ActivityReportModel()
        Log4NetControl.ComInfoLogWrite(Log4NetControl.UserID)
        Dim dbConn As DBUtility = New DBUtility()
        Dim dt As DataTable = New DataTable()


        Dim sqlStr As String = "SELECT  @ServiceCentre as ServiceCentre ,month,day,location,Customer_Visit,Call_Registerd,Repair_Completed,Goods_Delivered,Pending_Calls,  "

        sqlStr = sqlStr & "Cancelled_Calls,note,@SRCFIlE as SRC_FILE_NAME FROM Activity_report "
        sqlStr = sqlStr & "WHERE DELFG='0' "
        dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@ServiceCentre", queryParams.ServiceCentre))
        dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@SRCFIlE", queryParams.SRC_FILE_NAME))

        If Not String.IsNullOrEmpty(queryParams.location) Then
            sqlStr = sqlStr & "and location = @location "
            dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@location", queryParams.location))
        End If
        If Not String.IsNullOrEmpty(queryParams.FromDay) And Not String.IsNullOrEmpty(queryParams.ToDay) And Not String.IsNullOrEmpty(queryParams.FromMonth) And Not String.IsNullOrEmpty(queryParams.ToMonth) Then
            sqlStr = sqlStr & "And day between @DateFrom And @DateTo And month between @MonFrom And @MonTo "
            dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@DateFrom", queryParams.FromDay))
            dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@DateTo", queryParams.ToDay))
            dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@MonFrom", queryParams.FromMonth))
            dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@MonTo", queryParams.ToMonth))
        End If
        If Not String.IsNullOrEmpty(queryParams.Day) Then
            sqlStr = sqlStr & "and day = @day "
            dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@day", queryParams.Day))
        End If
        If Not String.IsNullOrEmpty(queryParams.Month) Then
            sqlStr = sqlStr & "and month = @month "
            dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@month", queryParams.Month))
        End If
        Dim _DataTable As DataTable = dbConn.GetDataSet(sqlStr)
        dbConn.CloseConnection()
        Return _DataTable

    End Function

End Class
