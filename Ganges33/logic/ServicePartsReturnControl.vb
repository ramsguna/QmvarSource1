'VJ 2019/10/10
Imports System
Imports System.Data
Imports System.Web.Security
Imports System.Collections.Generic
Imports Ganges33.Ganges33.dao
Imports Ganges33.Ganges33.model
Imports Ganges33.Ganges33.logic
Public Class ServicePartsReturnControl
    Public Function AddModifyServicePartsReturn(ByVal csvData()() As String, queryParams As ServicePartsReturnModel) As Boolean
        'Row 0 - Header 1
        '0 No
        '1 Credit Req. No
        '2 Status
        '3 Requested Date
        '4 Asc.Ref.No
        '5 Reason
        '6 Service Order
        '7 Engineer
        '8 Billing No
        '9 Amount
        '10 Title
        '11 Plant
        '12 Return Tracking No
        '13 Symptom
        '14 Pickup Date
        '15 Return S/O

        Log4NetControl.ComInfoLogWrite(Log4NetControl.UserID)
        'Mandatory Column 14 from CSV
        Dim flag As Boolean = True
        If csvData(0).Length < 16 Then
            Return False
        End If

        Dim DateTimeNow As DateTime = DateTime.Now
        Dim dtNow As DateTime = DateTimeNow.AddMinutes(ConfigurationManager.AppSettings("TimeDiff"))
        Dim dbConn As DBUtility = New DBUtility()
        Dim dt As DataTable = New DataTable()
        dbConn.sqlTrn = dbConn.sqlConn.BeginTransaction()
        dbConn.sqlCmd.Transaction = dbConn.sqlTrn

        Dim flagAll As Boolean = True
        Dim sqlStr As String = ""
        Dim dtReturnCreditExist As DataTable

        Dim isExist As Integer = 0
        '1st check INVOICE_NO,LOCAL_INVOICE_NO, DELIVERY_NO exist in the table 
        sqlStr = "SELECT TOP 1 CREDIT_REQ_NO FROM SP_RETURN "
        sqlStr = sqlStr & " WHERE DELFG = 0 AND SRC_FILE_NAME='" & queryParams.SrcFileName & "'"
        'sqlStr = sqlStr & " AND SHIP_TO_BRANCH_CODE='" & queryParams.ShipToBranchCode & "'"
        dtReturnCreditExist = dbConn.GetDataSet(sqlStr)
        'if exist then need to update delfg=1 then insert the record as new
        If (dtReturnCreditExist Is Nothing) Or (dtReturnCreditExist.Rows.Count = 0) Then
            'isExist = 0
        Else 'The records is already exist, need to update DELFg=0
            ' isExist = 1
            sqlStr = "UPDATE SP_RETURN SET DELFG=1 "
            sqlStr = sqlStr & " WHERE DELFG=0 "
            sqlStr = sqlStr & "AND SRC_FILE_NAME = @SRC_FILE_NAME"
            'sqlStr = sqlStr & "AND SHIP_TO_BRANCH_CODE = @SHIP_TO_BRANCH_CODE "
            'dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@UPDCD", queryParams.UploadUser))
            'dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@UPDDT", dtNow))
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
                    sqlStr = "Insert into SP_RETURN ("
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
                    sqlStr = sqlStr & "CREDIT_REQ_NO, "
                    sqlStr = sqlStr & "STATUS, "
                    sqlStr = sqlStr & "REQUEST_DATE, "
                    sqlStr = sqlStr & "ASC_REF_NO, "
                    sqlStr = sqlStr & "REASON, "
                    sqlStr = sqlStr & "SERVICE_ORDER, "
                    sqlStr = sqlStr & "ENGINEER, "
                    sqlStr = sqlStr & "BILLING_NO, "
                    sqlStr = sqlStr & "AMOUNT, "
                    sqlStr = sqlStr & "TITLE, "
                    sqlStr = sqlStr & "PLANT, "
                    sqlStr = sqlStr & "RETURN_TRACKING_NO, "
                    sqlStr = sqlStr & "SYMPTOM, "
                    sqlStr = sqlStr & "PICKUP_DATE, "
                    sqlStr = sqlStr & "RETURN_SO, "
                    sqlStr = sqlStr & "FILE_NAME, "
                    sqlStr = sqlStr & "SRC_FILE_NAME "
                    sqlStr = sqlStr & " ) "
                    sqlStr = sqlStr & " values ( "
                    sqlStr = sqlStr & "@CRTDT, "
                    sqlStr = sqlStr & "@CRTCD, "
                    sqlStr = sqlStr & "@UPDCD, "
                    sqlStr = sqlStr & "@UPDPG, "
                    sqlStr = sqlStr & "@DELFG, "
                    sqlStr = sqlStr & "@UPLOAD_USER, "
                    sqlStr = sqlStr & "@UPLOAD_DATE, "
                    sqlStr = sqlStr & "@SHIP_TO_BRANCH_CODE, "
                    sqlStr = sqlStr & "@SHIP_TO_BRANCH, "
                    sqlStr = sqlStr & "@NO, "
                    sqlStr = sqlStr & "@CREDIT_REQ_NO, "
                    sqlStr = sqlStr & "@STATUS, "
                    sqlStr = sqlStr & "@REQUEST_DATE, "
                    sqlStr = sqlStr & "@ASC_REF_NO, "
                    sqlStr = sqlStr & "@REASON, "
                    sqlStr = sqlStr & "@SERVICE_ORDER, "
                    sqlStr = sqlStr & "@ENGINEER, "
                    sqlStr = sqlStr & "@BILLING_NO, "
                    sqlStr = sqlStr & "@AMOUNT, "
                    sqlStr = sqlStr & "@TITLE, "
                    sqlStr = sqlStr & "@PLANT, "
                    sqlStr = sqlStr & "@RETURN_TRACKING_NO, "
                    sqlStr = sqlStr & "@SYMPTOM, "
                    sqlStr = sqlStr & "@PICKUP_DATE, "
                    sqlStr = sqlStr & "@RETURN_SO, "
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
                    dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@CREDIT_REQ_NO", csvData(i)(1)))
                    dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@STATUS", csvData(i)(2)))
                    dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@REQUEST_DATE", csvData(i)(3)))
                    dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@ASC_REF_NO", csvData(i)(4)))
                    dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@REASON", csvData(i)(5)))
                    dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@SERVICE_ORDER", csvData(i)(6)))
                    dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@ENGINEER", csvData(i)(7)))
                    dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@BILLING_NO", csvData(i)(8)))
                    dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@AMOUNT", csvData(i)(9)))
                    dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@TITLE", csvData(i)(10)))
                    dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@PLANT", csvData(i)(11)))
                    dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@RETURN_TRACKING_NO", csvData(i)(12)))
                    dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@SYMPTOM", csvData(i)(13)))
                    dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@PICKUP_DATE", csvData(i)(14)))
                    dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@RETURN_SO", csvData(i)(15)))
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


    Public Function SelectServicePartsReturn(ByVal queryParams As ServicePartsReturnModel) As DataTable
        Log4NetControl.ComInfoLogWrite(Log4NetControl.UserID)
        Dim dbConn As DBUtility = New DBUtility()
        Dim dt As DataTable = New DataTable()
        Dim sqlStr As String = "SELECT "
        'sqlStr = sqlStr & "DELFG,UNQ_NO,UPLOAD_USER,UPLOAD_DATE,SHIP_TO_BRANCH_CODE,SHIP_TO_BRANCH,INVOICE_DATE,INVOICE_NO,LOCAL_INVOICE_NO,DELIVERY_NO,ITEMS,AMOUNT,SGST_UTGST,CGST,IGST,CESS,TAX,TOTAL,GR_STATUS,FILE_NAME"
        sqlStr = sqlStr & "No 'No',CREDIT_REQ_NO 'Credit Req. No',STATUS 'Status',replace(convert(varchar(10), REQUEST_DATE, 101),'/','.') 'Requested Date', ASC_REF_NO 'Asc.Ref.No',"
        sqlStr = sqlStr & "REASON 'Reason',SERVICE_ORDER 'Service Order',ENGINEER 'Engineer',BILLING_NO 'Billing No',AMOUNT 'Amount', "
        sqlStr = sqlStr & "TITLE 'Title',PLANT 'Plant',RETURN_TRACKING_NO 'Return Tracking No',SYMPTOM 'Symptom',PICKUP_DATE 'Pickup Date',RETURN_SO 'Return S/O' "
        sqlStr = sqlStr & "FROM "
        sqlStr = sqlStr & "SP_RETURN "
        sqlStr = sqlStr & "WHERE "
        sqlStr = sqlStr & "DELFG=0 "
        If Not String.IsNullOrEmpty(queryParams.SrcFileName) Then
            sqlStr = sqlStr & "AND SRC_FILE_NAME = @SRC_FILE_NAME Order By No"
            dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@SRC_FILE_NAME", queryParams.SrcFileName))
        End If
        Dim _DataTable As DataTable = dbConn.GetDataSet(sqlStr)
        dbConn.CloseConnection()
        Return _DataTable
    End Function
End Class
