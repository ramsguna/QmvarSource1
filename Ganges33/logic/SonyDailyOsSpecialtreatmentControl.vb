Imports Ganges33.Ganges33.dao
Imports Ganges33.Ganges33.model
Imports Ganges33.Ganges33.logic
Public Class SonyDailyOsSpecialtreatmentControl

    Public Function AddDailyOSSpecialtreatment(ByVal csvData()() As String, queryParams As SonyDailyOsSpecialtreatmentModel) As Boolean
        Log4NetControl.ComInfoLogWrite(Log4NetControl.UserID)

        Dim flag As Boolean = True
        If csvData(0).Length < 17 Then
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
        '1st check COUNTRY
        sqlStr = "SELECT TOP 1 COUNTRY FROM SONY_DAILY_OS_SPECIALTREATMENT "
        sqlStr = sqlStr & " WHERE DELFG = 0 AND SRC_FILE_NAME='" & queryParams.SRC_FILE_NAME & "'"
        'sqlStr = sqlStr & " AND SHIP_TO_BRANCH_CODE='" & queryParams.ShipToBranchCode & "'"
        dtSawDiscountExist = dbConn.GetDataSet(sqlStr)
        'if exist then need to update delfg=1 then insert the record as new
        If (dtSawDiscountExist Is Nothing) Or (dtSawDiscountExist.Rows.Count = 0) Then
            'isExist = 0
        Else 'The records is already exist, need to update DELFg=0
            ' isExist = 1
            sqlStr = "UPDATE SONY_DAILY_OS_SPECIALTREATMENT SET DELFG=1  "
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
                sqlStr = "Insert into SONY_DAILY_OS_SPECIALTREATMENT ("
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

                sqlStr = sqlStr & "COUNTRY, "
                sqlStr = sqlStr & "REGION, "
                sqlStr = sqlStr & "REGION_NAME, "
                sqlStr = sqlStr & "UNIT_CODE, "
                sqlStr = sqlStr & "ORGANIZATION_NAME, "
                sqlStr = sqlStr & "SERVICE_SHEET_NO, "
                sqlStr = sqlStr & "PURCHASE_DATE, "
                sqlStr = sqlStr & "RECEIVED_DATE, "
                sqlStr = sqlStr & "WARRANTY_TYPE, "
                sqlStr = sqlStr & "WARRANTY_CATEGORY, "
                sqlStr = sqlStr & "PRODUCT_CATEGORY, "
                sqlStr = sqlStr & "PRODUCT_SUB_CATEGORY, "
                sqlStr = sqlStr & "MODEL_CODE, "
                sqlStr = sqlStr & "MODEL_DESC, "
                sqlStr = sqlStr & "SERIAL_NO, "
                sqlStr = sqlStr & "APPLY_REMARK, "
                sqlStr = sqlStr & "FEE_TYPE, "
                sqlStr = sqlStr & "PART_CODE, "
                sqlStr = sqlStr & "PART_DESC, "
                sqlStr = sqlStr & "ACCOUNT_RECEIVABLE, "
                sqlStr = sqlStr & "DISCOUNT_FEE, "
                sqlStr = sqlStr & "USER_PAY, "
                sqlStr = sqlStr & "SONY_PAY, "
                sqlStr = sqlStr & "APPLY_FEE, "
                sqlStr = sqlStr & "APPOVER_DATE, "
                sqlStr = sqlStr & "JOB_STATUS, "
                sqlStr = sqlStr & "COLLECTION_DATE, "
                sqlStr = sqlStr & "CUSTOMER_NAME, "
                sqlStr = sqlStr & "ST_TYPE, "
                sqlStr = sqlStr & "Apply_Date, "
                sqlStr = sqlStr & "Approve_status, "
                sqlStr = sqlStr & "Remark, "
                sqlStr = sqlStr & "Approval_result, "






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


                sqlStr = sqlStr & "@COUNTRY, "
                sqlStr = sqlStr & "@REGION, "
                sqlStr = sqlStr & "@REGION_NAME, "
                sqlStr = sqlStr & "@UNIT_CODE, "
                sqlStr = sqlStr & "@ORGANIZATION_NAME, "
                sqlStr = sqlStr & "@SERVICE_SHEET_NO, "
                sqlStr = sqlStr & "@PURCHASE_DATE, "
                sqlStr = sqlStr & "@RECEIVED_DATE, "
                sqlStr = sqlStr & "@WARRANTY_TYPE, "
                sqlStr = sqlStr & "@WARRANTY_CATEGORY, "
                sqlStr = sqlStr & "@PRODUCT_CATEGORY, "
                sqlStr = sqlStr & "@PRODUCT_SUB_CATEGORY, "
                sqlStr = sqlStr & "@MODEL_CODE, "
                sqlStr = sqlStr & "@MODEL_DESC, "
                sqlStr = sqlStr & "@SERIAL_NO, "
                sqlStr = sqlStr & "@APPLY_REMARK, "
                sqlStr = sqlStr & "@FEE_TYPE, "
                sqlStr = sqlStr & "@PART_CODE, "
                sqlStr = sqlStr & "@PART_DESC, "
                sqlStr = sqlStr & "@ACCOUNT_RECEIVABLE, "
                sqlStr = sqlStr & "@DISCOUNT_FEE, "
                sqlStr = sqlStr & "@USER_PAY, "
                sqlStr = sqlStr & "@SONY_PAY, "
                sqlStr = sqlStr & "@APPLY_FEE, "
                sqlStr = sqlStr & "@APPOVER_DATE, "
                sqlStr = sqlStr & "@JOB_STATUS, "
                sqlStr = sqlStr & "@COLLECTION_DATE, "
                sqlStr = sqlStr & "@CUSTOMER_NAME, "
                sqlStr = sqlStr & "@ST_TYPE, "
                sqlStr = sqlStr & "@Apply_Date, "
                sqlStr = sqlStr & "@Approve_status, "
                sqlStr = sqlStr & "@Remark, "
                sqlStr = sqlStr & "@Approval_result, "




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

                dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@COUNTRY", csvData(i)(0)))
                dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@REGION", csvData(i)(1)))
                dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@REGION_NAME", csvData(i)(2)))
                dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@UNIT_CODE", csvData(i)(3)))
                dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@ORGANIZATION_NAME", csvData(i)(4)))
                dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@SERVICE_SHEET_NO", csvData(i)(5)))
                dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@PURCHASE_DATE", csvData(i)(6)))
                dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@RECEIVED_DATE", csvData(i)(7)))
                dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@WARRANTY_TYPE", csvData(i)(8)))
                dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@WARRANTY_CATEGORY", csvData(i)(9)))
                dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@PRODUCT_CATEGORY", csvData(i)(10)))
                dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@PRODUCT_SUB_CATEGORY", csvData(i)(11)))
                dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@MODEL_CODE", csvData(i)(12)))
                dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@MODEL_DESC", csvData(i)(13)))
                dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@SERIAL_NO", csvData(i)(14)))
                dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@APPLY_REMARK", csvData(i)(15)))
                dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@FEE_TYPE", csvData(i)(16)))
                dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@PART_CODE", csvData(i)(17)))
                dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@PART_DESC", csvData(i)(18)))
                dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@ACCOUNT_RECEIVABLE", csvData(i)(19)))
                dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@DISCOUNT_FEE", csvData(i)(20)))
                dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@USER_PAY", csvData(i)(21)))
                dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@SONY_PAY", csvData(i)(22)))
                dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@APPLY_FEE", csvData(i)(23)))
                dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@APPOVER_DATE", csvData(i)(24)))
                dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@JOB_STATUS", csvData(i)(25)))
                dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@COLLECTION_DATE", csvData(i)(26)))
                dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@CUSTOMER_NAME", csvData(i)(27)))
                dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@ST_TYPE", csvData(i)(28)))
                dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@Apply_Date", csvData(i)(29)))
                dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@Approve_status", csvData(i)(30)))
                dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@Remark", csvData(i)(31)))
                dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@Approval_result", csvData(i)(32)))



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

    Public Function SelectSony_Daily_OS_specialtreatment(ByVal queryParams As SonyDailyOsSpecialtreatmentModel) As DataTable
        Log4NetControl.ComInfoLogWrite(Log4NetControl.UserID)
        Dim dbConn As DBUtility = New DBUtility()
        Dim dt As DataTable = New DataTable()
        Dim sqlStr As String = "SELECT * "
        'sqlStr = sqlStr & "COUNTRY as 'Country',REGION1 as 'Region1',REGION2 as 'Region2',ASC_CODE as 'Asc Code',ASC_NAME as 'Asc Name',SERVICE_SHEET_NO as 'Service Sheet No',MODEL_CODE as 'Model Code',MODEL_NAME as 'Model Name',SERIAL_NO as 'Serial No',CREATE_DATE as 'Create Date', CUSTOMER_NAME as 'Customer Name',REPAIR_STATUS as 'Repair Status',TERMINATED_DATE as 'Terminated Date',STATUS_REMARKS as 'Status Remarks',ST_TYPE as 'St Type',CANCEL_REASON as 'Cancel Reason',REPAIR_CONTENTS as 'Repair Contents' "
        sqlStr = sqlStr & "FROM "
        sqlStr = sqlStr & "SONY_DAILY_OS_SPECIALTREATMENT "
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
