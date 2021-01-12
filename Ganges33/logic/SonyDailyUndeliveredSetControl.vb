Imports Ganges33.Ganges33.dao
Imports Ganges33.Ganges33.model
Imports Ganges33.Ganges33.logic
Public Class SonydailyUndeliveredSetcontroll
    Public Function AddSonydailyUndeliveredSetcontroll(ByVal csvData()() As String, queryParams As SonyDailyUndeliveredSetModel) As Boolean
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
        sqlStr = "SELECT TOP 1 COUNTRY FROM SONY_DAILY_UNDELIVEREDSET "
        sqlStr = sqlStr & " WHERE DELFG = 0 AND SRC_FILE_NAME='" & queryParams.SRC_FILE_NAME & "'"
        'sqlStr = sqlStr & " AND SHIP_TO_BRANCH_CODE='" & queryParams.ShipToBranchCode & "'"
        dtSawDiscountExist = dbConn.GetDataSet(sqlStr)
        'if exist then need to update delfg=1 then insert the record as new
        If (dtSawDiscountExist Is Nothing) Or (dtSawDiscountExist.Rows.Count = 0) Then
            'isExist = 0
        Else 'The records is already exist, need to update DELFg=0
            ' isExist = 1
            sqlStr = "UPDATE SONY_DAILY_UNDELIVEREDSET SET DELFG=1  "
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
                sqlStr = "Insert into SONY_DAILY_UNDELIVEREDSET ("
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
                sqlStr = sqlStr & "ASCCODE, "
                sqlStr = sqlStr & "ORG_NAME, "
                sqlStr = sqlStr & "SERVICE_SHEET_NO, "
                sqlStr = sqlStr & "CUSTOMGRP_NAME, "
                sqlStr = sqlStr & "WARRANTY_TYPE_NAME, "
                sqlStr = sqlStr & "WARRANTY_CATEGORY_NAME, "
                sqlStr = sqlStr & "SERVICE_TYPE, "
                sqlStr = sqlStr & "PRODUCT_CAT, "
                sqlStr = sqlStr & "SUB_CAT, "
                sqlStr = sqlStr & "MODEL_CODE, "
                sqlStr = sqlStr & "MODEL_NAME, "
                sqlStr = sqlStr & "SERIAL_NO, "
                sqlStr = sqlStr & "CREATE_DATE, "
                sqlStr = sqlStr & "REPAIR_COMPLETE_DATE, "
                sqlStr = sqlStr & "PENDING_DELIVERY_AGE, "
                sqlStr = sqlStr & "PART_FEE, "
                sqlStr = sqlStr & "LABOUR_FEE, "
                sqlStr = sqlStr & "INSPECTION_FEE, "
                sqlStr = sqlStr & "HANDLING_FEE, "
                sqlStr = sqlStr & "TRANSPORT_FEE, "
                sqlStr = sqlStr & "HOMESERVICE_FEE, "
                sqlStr = sqlStr & "LONGDISTANCE_FEE, "
                sqlStr = sqlStr & "TRAVELALLOWANCE_FEE, "
                sqlStr = sqlStr & "DA_FEE, "
                sqlStr = sqlStr & "DEMO_CHARGE, "
                sqlStr = sqlStr & "INSTALLATION_FEE, "
                sqlStr = sqlStr & "ECALL_CHARGE, "
                sqlStr = sqlStr & "COMBAT_FEE, "
                sqlStr = sqlStr & "ACCOUNT_RECEIVABLE, "
                sqlStr = sqlStr & "CUSTOM_PAY, "
                sqlStr = sqlStr & "SONY_PAY, "
                sqlStr = sqlStr & "ASC_PAY, "
                sqlStr = sqlStr & "CUSTOMER_NAME, "
                sqlStr = sqlStr & "ADDRESS, "
                sqlStr = sqlStr & "CITY_NAME, "
                sqlStr = sqlStr & "PHONE, "
                sqlStr = sqlStr & "MOBILE, "
                sqlStr = sqlStr & "FAX, "
                sqlStr = sqlStr & "EMAIL, "
                sqlStr = sqlStr & "REPAIR_MAN_NAME, "
                sqlStr = sqlStr & "REPAIR_STATUS, "
                sqlStr = sqlStr & "LAST_CONTACT_UPDATE_DATE, "
                sqlStr = sqlStr & "LAST_CONTACT_UPDATE_CONTENTS, "
                sqlStr = sqlStr & "BIN_LOCATION, "
                sqlStr = sqlStr & "POST_CODE, "
                sqlStr = sqlStr & "St_type, "
                sqlStr = sqlStr & "Model_6D, "
                sqlStr = sqlStr & "convertToJob_in_MAPP, "
                sqlStr = sqlStr & "completed_in_MAPP, "
                sqlStr = sqlStr & "deliver_in_MAPPCRLF, "




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
                sqlStr = sqlStr & "@ASCCODE, "
                sqlStr = sqlStr & "@ORG_NAME, "
                sqlStr = sqlStr & "@SERVICE_SHEET_NO, "
                sqlStr = sqlStr & "@CUSTOMGRP_NAME, "
                sqlStr = sqlStr & "@WARRANTY_TYPE_NAME, "
                sqlStr = sqlStr & "@WARRANTY_CATEGORY_NAME, "
                sqlStr = sqlStr & "@SERVICE_TYPE, "
                sqlStr = sqlStr & "@PRODUCT_CAT, "
                sqlStr = sqlStr & "@SUB_CAT, "
                sqlStr = sqlStr & "@MODEL_CODE, "
                sqlStr = sqlStr & "@MODEL_NAME, "
                sqlStr = sqlStr & "@SERIAL_NO, "
                sqlStr = sqlStr & "@CREATE_DATE, "
                sqlStr = sqlStr & "@REPAIR_COMPLETE_DATE, "
                sqlStr = sqlStr & "@PENDING_DELIVERY_AGE, "
                sqlStr = sqlStr & "@PART_FEE, "
                sqlStr = sqlStr & "@LABOUR_FEE, "
                sqlStr = sqlStr & "@INSPECTION_FEE, "
                sqlStr = sqlStr & "@HANDLING_FEE, "
                sqlStr = sqlStr & "@TRANSPORT_FEE, "
                sqlStr = sqlStr & "@HOMESERVICE_FEE, "
                sqlStr = sqlStr & "@LONGDISTANCE_FEE, "
                sqlStr = sqlStr & "@TRAVELALLOWANCE_FEE, "
                sqlStr = sqlStr & "@DA_FEE, "
                sqlStr = sqlStr & "@DEMO_CHARGE, "
                sqlStr = sqlStr & "@INSTALLATION_FEE, "
                sqlStr = sqlStr & "@ECALL_CHARGE, "
                sqlStr = sqlStr & "@COMBAT_FEE, "
                sqlStr = sqlStr & "@ACCOUNT_RECEIVABLE, "
                sqlStr = sqlStr & "@CUSTOM_PAY, "
                sqlStr = sqlStr & "@SONY_PAY, "
                sqlStr = sqlStr & "@ASC_PAY, "
                sqlStr = sqlStr & "@CUSTOMER_NAME, "
                sqlStr = sqlStr & "@ADDRESS, "
                sqlStr = sqlStr & "@CITY_NAME, "
                sqlStr = sqlStr & "@PHONE, "
                sqlStr = sqlStr & "@MOBILE, "
                sqlStr = sqlStr & "@FAX, "
                sqlStr = sqlStr & "@EMAIL, "
                sqlStr = sqlStr & "@REPAIR_MAN_NAME, "
                sqlStr = sqlStr & "@REPAIR_STATUS, "
                sqlStr = sqlStr & "@LAST_CONTACT_UPDATE_DATE, "
                sqlStr = sqlStr & "@LAST_CONTACT_UPDATE_CONTENTS, "
                sqlStr = sqlStr & "@BIN_LOCATION, "
                sqlStr = sqlStr & "@POST_CODE, "
                sqlStr = sqlStr & "@St_type, "
                sqlStr = sqlStr & "@Model_6D, "
                sqlStr = sqlStr & "@convertToJob_in_MAPP, "
                sqlStr = sqlStr & "@completed_in_MAPP, "
                sqlStr = sqlStr & "@deliver_in_MAPPCRLF, "


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
                dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@ASCCODE", csvData(i)(3)))
                dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@ORG_NAME", csvData(i)(4)))
                dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@SERVICE_SHEET_NO", csvData(i)(5)))
                dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@CUSTOMGRP_NAME", csvData(i)(6)))
                dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@WARRANTY_TYPE_NAME", csvData(i)(7)))
                dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@WARRANTY_CATEGORY_NAME", csvData(i)(8)))
                dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@SERVICE_TYPE", csvData(i)(9)))
                dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@PRODUCT_CAT", csvData(i)(10)))
                dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@SUB_CAT", csvData(i)(11)))
                dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@MODEL_CODE", csvData(i)(12)))
                dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@MODEL_NAME", csvData(i)(13)))
                dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@SERIAL_NO", csvData(i)(14)))
                dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@CREATE_DATE", csvData(i)(15)))
                dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@REPAIR_COMPLETE_DATE", csvData(i)(16)))
                dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@PENDING_DELIVERY_AGE", csvData(i)(17)))
                dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@PART_FEE", csvData(i)(18)))
                dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@LABOUR_FEE", csvData(i)(9)))
                dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@INSPECTION_FEE", csvData(i)(20)))
                dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@HANDLING_FEE", csvData(i)(21)))
                dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@TRANSPORT_FEE", csvData(i)(22)))
                dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@HOMESERVICE_FEE", csvData(i)(23)))
                dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@LONGDISTANCE_FEE", csvData(i)(24)))
                dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@TRAVELALLOWANCE_FEE", csvData(i)(25)))
                dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@DA_FEE", csvData(i)(26)))
                dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@DEMO_CHARGE", csvData(i)(27)))
                dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@INSTALLATION_FEE", csvData(i)(28)))
                dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@ECALL_CHARGE", csvData(i)(29)))
                dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@COMBAT_FEE", csvData(i)(30)))
                dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@ACCOUNT_RECEIVABLE", csvData(i)(31)))
                dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@CUSTOM_PAY", csvData(i)(32)))
                dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@SONY_PAY", csvData(i)(33)))
                dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@ASC_PAY", csvData(i)(34)))
                dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@CUSTOMER_NAME", csvData(i)(35)))
                dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@ADDRESS", csvData(i)(36)))
                dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@CITY_NAME", csvData(i)(37)))
                dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@PHONE", csvData(i)(38)))
                dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@MOBILE", csvData(i)(39)))
                dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@FAX", csvData(i)(40)))
                dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@EMAIL", csvData(i)(41)))
                dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@REPAIR_MAN_NAME", csvData(i)(42)))
                dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@REPAIR_STATUS", csvData(i)(43)))
                dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@LAST_CONTACT_UPDATE_DATE", csvData(i)(44)))
                dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@LAST_CONTACT_UPDATE_CONTENTS", csvData(i)(45)))
                dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@BIN_LOCATION", csvData(i)(46)))
                dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@POST_CODE", csvData(i)(47)))
                dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@St_type", csvData(i)(48)))
                dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@Model_6D", csvData(i)(49)))
                dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@convertToJob_in_MAPP", csvData(i)(50)))
                dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@completed_in_MAPP", csvData(i)(51)))
                dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@deliver_in_MAPPCRLF", csvData(i)(51)))

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
    Public Function SelectDailyUnRepairsetControll(ByVal queryParams As SonyDailyUndeliveredSetModel) As DataTable
        Log4NetControl.ComInfoLogWrite(Log4NetControl.UserID)
        Dim dbConn As DBUtility = New DBUtility()
        Dim dt As DataTable = New DataTable()
        Dim sqlStr As String = "SELECT * "
        sqlStr = sqlStr & "FROM "
        sqlStr = sqlStr & "SONY_DAILY_UNDELIVEREDSET "
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
