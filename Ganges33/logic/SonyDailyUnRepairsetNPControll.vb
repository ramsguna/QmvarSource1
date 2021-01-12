Imports Ganges33.Ganges33.dao
Imports Ganges33.Ganges33.model
Imports Ganges33.Ganges33.logic
Public Class SonyDailyUnRepairsetNPControll


    Public Function AddDailyUnRepairsetNPControll(ByVal csvData()() As String, queryParams As SonyDailyUnRepaipairsetNPModel) As Boolean
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
        sqlStr = "SELECT TOP 1 COUNTRY FROM SONY_DAILY_UNREPAIPAIRSET_NP "
        sqlStr = sqlStr & " WHERE DELFG = 0 AND SRC_FILE_NAME='" & queryParams.SRC_FILE_NAME & "'"
        'sqlStr = sqlStr & " AND SHIP_TO_BRANCH_CODE='" & queryParams.ShipToBranchCode & "'"
        dtSawDiscountExist = dbConn.GetDataSet(sqlStr)
        'if exist then need to update delfg=1 then insert the record as new
        If (dtSawDiscountExist Is Nothing) Or (dtSawDiscountExist.Rows.Count = 0) Then
            'isExist = 0
        Else 'The records is already exist, need to update DELFg=0
            ' isExist = 1
            sqlStr = "UPDATE SONY_DAILY_UNREPAIPAIRSET_NP SET DELFG=1  "
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
                sqlStr = "Insert into SONY_DAILY_UNREPAIPAIRSET_NP ("
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
                sqlStr = sqlStr & "SEQ, "
                sqlStr = sqlStr & "REGION, "
                sqlStr = sqlStr & "REGIONName, "
                sqlStr = sqlStr & "ASC_CODE, "
                sqlStr = sqlStr & "ASC_NAME, "
                sqlStr = sqlStr & "JOB_NO, "
                sqlStr = sqlStr & "CUSTOMER_GROUP, "
                sqlStr = sqlStr & "CUSTOMER_NAME, "
                sqlStr = sqlStr & "EMAIL, "
                sqlStr = sqlStr & "LINKMAN, "
                sqlStr = sqlStr & "PHONE, "
                sqlStr = sqlStr & "MOBILE, "
                sqlStr = sqlStr & "ADDRESS, "
                sqlStr = sqlStr & "CITY, "
                sqlStr = sqlStr & "PROVINCE, "
                sqlStr = sqlStr & "POST_CODE, "
                sqlStr = sqlStr & "WARRANTY_TYPE, "
                sqlStr = sqlStr & "WARRANTY_CATEGORY, "
                sqlStr = sqlStr & "SERVICE_TYPE, "
                sqlStr = sqlStr & "PRODUCT_CATEGORY_NAME, "
                sqlStr = sqlStr & "PRODUCT_SUB_CATEGORY_NAME, "
                sqlStr = sqlStr & "SET_MODEL, "
                sqlStr = sqlStr & "MODEL_NAME, "
                sqlStr = sqlStr & "SERIAL_NO, "
                sqlStr = sqlStr & "PURCHASED_SHOP, "
                sqlStr = sqlStr & "PURCHASED_DATE, "
                sqlStr = sqlStr & "RESERVATION_CREATE_DATE, "
                sqlStr = sqlStr & "APPOINTMENT_DATE, "
                sqlStr = sqlStr & "JOB_CREATE_DATE, "
                sqlStr = sqlStr & "PENDING_REPAIR_AGE, "
                sqlStr = sqlStr & "CUSTOMER_REQUIRE_DATE, "
                sqlStr = sqlStr & "JOB_STATUS, "
                sqlStr = sqlStr & "JOB_SUB_STATUS, "
                sqlStr = sqlStr & "TECHNICIAN, "
                sqlStr = sqlStr & "PART_NO, "
                sqlStr = sqlStr & "PART_DESC, "
                sqlStr = sqlStr & "QTY, "
                sqlStr = sqlStr & "PARTCHARGETYPE, "
                sqlStr = sqlStr & "FIRST_ESTIMATION_CREATE_DATE, "
                sqlStr = sqlStr & "LAST_ESTIMATION_DATE, "
                sqlStr = sqlStr & "LATEST_ESTIMATE_STATUS, "
                sqlStr = sqlStr & "PARTS_REQUEST_DATE, "
                sqlStr = sqlStr & "ASC_PO_NUMBER, "
                sqlStr = sqlStr & "ASC_PO_DATE, "
                sqlStr = sqlStr & "SAP_ORDER_NO, "
                sqlStr = sqlStr & "SAP_ORDER_DATE, "
                sqlStr = sqlStr & "PO_STATUS, "
                sqlStr = sqlStr & "NPC_SHIPPING_STATUS, "
                sqlStr = sqlStr & "PART_INVOICE_DATE, "
                sqlStr = sqlStr & "Part_Received_Date, "
                sqlStr = sqlStr & "CR90, "
                sqlStr = sqlStr & "LAST_STATUS_UPDATE_DATE, "
                sqlStr = sqlStr & "LAST_CONTACT_UPDATE_DATE, "
                sqlStr = sqlStr & "D4, "
                sqlStr = sqlStr & "D6, "
                sqlStr = sqlStr & "D6_DESC, "
                sqlStr = sqlStr & "D4_Desc, "
                sqlStr = sqlStr & "St_type, "
                sqlStr = sqlStr & "Part_6D, "
                sqlStr = sqlStr & "FIRST_ALLOCATION_DATE, "
                sqlStr = sqlStr & "LAST_ALLOCATION_DATE, "
                sqlStr = sqlStr & "BEGIN_REPAIR_DATE, "
                sqlStr = sqlStr & "SRPC_PO_No, "
                sqlStr = sqlStr & "Carton_NO, "
                sqlStr = sqlStr & "Call_Customer, "
                sqlStr = sqlStr & "Customer_Complaint, "
                sqlStr = sqlStr & "Symptom_Confirmed_By_Technician, "
                sqlStr = sqlStr & "Condition_of_Set, "
                sqlStr = sqlStr & "GUARANTEE_CODE, "




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
                sqlStr = sqlStr & "@SEQ, "
                sqlStr = sqlStr & "@REGION, "
                sqlStr = sqlStr & "@REGIONName, "
                sqlStr = sqlStr & "@ASC_CODE, "
                sqlStr = sqlStr & "@ASC_NAME, "
                sqlStr = sqlStr & "@JOB_NO, "
                sqlStr = sqlStr & "@CUSTOMER_GROUP, "
                sqlStr = sqlStr & "@CUSTOMER_NAME, "
                sqlStr = sqlStr & "@EMAIL, "
                sqlStr = sqlStr & "@LINKMAN, "
                sqlStr = sqlStr & "@PHONE, "
                sqlStr = sqlStr & "@MOBILE, "
                sqlStr = sqlStr & "@ADDRESS, "
                sqlStr = sqlStr & "@CITY, "
                sqlStr = sqlStr & "@PROVINCE, "
                sqlStr = sqlStr & "@POST_CODE, "
                sqlStr = sqlStr & "@WARRANTY_TYPE, "
                sqlStr = sqlStr & "@WARRANTY_CATEGORY, "
                sqlStr = sqlStr & "@SERVICE_TYPE, "
                sqlStr = sqlStr & "@PRODUCT_CATEGORY_NAME, "
                sqlStr = sqlStr & "@PRODUCT_SUB_CATEGORY_NAME, "
                sqlStr = sqlStr & "@SET_MODEL, "
                sqlStr = sqlStr & "@MODEL_NAME, "
                sqlStr = sqlStr & "@SERIAL_NO, "
                sqlStr = sqlStr & "@PURCHASED_SHOP, "
                sqlStr = sqlStr & "@PURCHASED_DATE, "
                sqlStr = sqlStr & "@RESERVATION_CREATE_DATE, "
                sqlStr = sqlStr & "@APPOINTMENT_DATE, "
                sqlStr = sqlStr & "@JOB_CREATE_DATE, "
                sqlStr = sqlStr & "@PENDING_REPAIR_AGE, "
                sqlStr = sqlStr & "@CUSTOMER_REQUIRE_DATE, "
                sqlStr = sqlStr & "@JOB_STATUS, "
                sqlStr = sqlStr & "@JOB_SUB_STATUS, "
                sqlStr = sqlStr & "@TECHNICIAN, "
                sqlStr = sqlStr & "@PART_NO, "
                sqlStr = sqlStr & "@PART_DESC, "
                sqlStr = sqlStr & "@QTY, "
                sqlStr = sqlStr & "@PARTCHARGETYPE, "
                sqlStr = sqlStr & "@FIRST_ESTIMATION_CREATE_DATE, "
                sqlStr = sqlStr & "@LAST_ESTIMATION_DATE, "
                sqlStr = sqlStr & "@LATEST_ESTIMATE_STATUS, "
                sqlStr = sqlStr & "@PARTS_REQUEST_DATE, "
                sqlStr = sqlStr & "@ASC_PO_NUMBER, "
                sqlStr = sqlStr & "@ASC_PO_DATE, "
                sqlStr = sqlStr & "@SAP_ORDER_NO, "
                sqlStr = sqlStr & "@SAP_ORDER_DATE, "
                sqlStr = sqlStr & "@PO_STATUS, "
                sqlStr = sqlStr & "@NPC_SHIPPING_STATUS, "
                sqlStr = sqlStr & "@PART_INVOICE_DATE, "
                sqlStr = sqlStr & "@Part_Received_Date, "
                sqlStr = sqlStr & "@CR90, "
                sqlStr = sqlStr & "@LAST_STATUS_UPDATE_DATE, "
                sqlStr = sqlStr & "@LAST_CONTACT_UPDATE_DATE, "
                sqlStr = sqlStr & "@D4, "
                sqlStr = sqlStr & "@D6, "
                sqlStr = sqlStr & "@D6_DESC, "
                sqlStr = sqlStr & "@D4_Desc, "
                sqlStr = sqlStr & "@St_type, "
                sqlStr = sqlStr & "@Part_6D, "
                sqlStr = sqlStr & "@FIRST_ALLOCATION_DATE, "
                sqlStr = sqlStr & "@LAST_ALLOCATION_DATE, "
                sqlStr = sqlStr & "@BEGIN_REPAIR_DATE, "
                sqlStr = sqlStr & "@SRPC_PO_No, "
                sqlStr = sqlStr & "@Carton_NO, "
                sqlStr = sqlStr & "@Call_Customer, "
                sqlStr = sqlStr & "@Customer_Complaint, "
                sqlStr = sqlStr & "@Symptom_Confirmed_By_Technician, "
                sqlStr = sqlStr & "@Condition_of_Set, "
                sqlStr = sqlStr & "@GUARANTEE_CODE, "


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
                dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@SEQ", csvData(i)(1)))
                dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@REGION", csvData(i)(2)))
                dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@REGIONName", csvData(i)(3)))
                dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@ASC_CODE", csvData(i)(4)))
                dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@ASC_NAME", csvData(i)(5)))
                dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@JOB_NO", csvData(i)(6)))
                dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@CUSTOMER_GROUP", csvData(i)(7)))
                dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@CUSTOMER_NAME", csvData(i)(8)))
                dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@EMAIL", csvData(i)(9)))
                dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@LINKMAN", csvData(i)(10)))
                dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@PHONE", csvData(i)(11)))
                dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@MOBILE", csvData(i)(12)))
                dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@ADDRESS", csvData(i)(13)))
                dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@CITY", csvData(i)(14)))
                dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@PROVINCE", csvData(i)(15)))
                dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@POST_CODE", csvData(i)(16)))
                dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@WARRANTY_TYPE", csvData(i)(17)))
                dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@WARRANTY_CATEGORY", csvData(i)(18)))
                dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@SERVICE_TYPE", csvData(i)(19)))
                dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@PRODUCT_CATEGORY_NAME", csvData(i)(20)))
                dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@PRODUCT_SUB_CATEGORY_NAME", csvData(i)(21)))
                dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@SET_MODEL", csvData(i)(22)))
                dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@MODEL_NAME", csvData(i)(23)))
                dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@SERIAL_NO", csvData(i)(24)))
                dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@PURCHASED_SHOP", csvData(i)(25)))
                dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@PURCHASED_DATE", csvData(i)(26)))
                dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@RESERVATION_CREATE_DATE", csvData(i)(27)))
                dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@APPOINTMENT_DATE", csvData(i)(28)))
                dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@JOB_CREATE_DATE", csvData(i)(29)))
                dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@PENDING_REPAIR_AGE", csvData(i)(30)))
                dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@CUSTOMER_REQUIRE_DATE", csvData(i)(31)))
                dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@JOB_STATUS", csvData(i)(32)))
                dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@JOB_SUB_STATUS", csvData(i)(33)))
                dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@TECHNICIAN", csvData(i)(34)))
                dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@PART_NO", csvData(i)(35)))
                dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@PART_DESC", csvData(i)(36)))
                dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@QTY", csvData(i)(37)))
                dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@PARTCHARGETYPE", csvData(i)(38)))
                dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@FIRST_ESTIMATION_CREATE_DATE", csvData(i)(39)))
                dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@LAST_ESTIMATION_DATE", csvData(i)(40)))
                dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@LATEST_ESTIMATE_STATUS", csvData(i)(41)))
                dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@PARTS_REQUEST_DATE", csvData(i)(42)))
                dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@ASC_PO_NUMBER", csvData(i)(43)))
                dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@ASC_PO_DATE", csvData(i)(44)))
                dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@SAP_ORDER_NO", csvData(i)(45)))
                dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@SAP_ORDER_DATE", csvData(i)(46)))
                dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@PO_STATUS", csvData(i)(47)))
                dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@NPC_SHIPPING_STATUS", csvData(i)(48)))
                dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@PART_INVOICE_DATE", csvData(i)(49)))
                dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@Part_Received_Date", csvData(i)(50)))
                dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@CR90", csvData(i)(51)))
                dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@LAST_STATUS_UPDATE_DATE", csvData(i)(52)))
                dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@LAST_CONTACT_UPDATE_DATE", csvData(i)(53)))
                dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@D4", csvData(i)(54)))
                dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@D6", csvData(i)(55)))
                dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@D6_DESC", csvData(i)(56)))
                dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@D4_Desc", csvData(i)(57)))
                dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@St_type", csvData(i)(58)))
                dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@Part_6D", csvData(i)(59)))
                dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@FIRST_ALLOCATION_DATE", csvData(i)(60)))
                dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@LAST_ALLOCATION_DATE", csvData(i)(61)))
                dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@BEGIN_REPAIR_DATE", csvData(i)(62)))
                dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@SRPC_PO_No", csvData(i)(63)))
                dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@Carton_NO", csvData(i)(64)))
                dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@Call_Customer", csvData(i)(65)))
                dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@Customer_Complaint", csvData(i)(66)))
                dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@Symptom_Confirmed_By_Technician", csvData(i)(67)))
                dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@Condition_of_Set", csvData(i)(68)))
                dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@GUARANTEE_CODE", csvData(i)(69)))



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

    Public Function SelectDailyUnRepairsetNPControll(ByVal queryParams As SonyDailyUnRepaipairsetNPModel) As DataTable
        Log4NetControl.ComInfoLogWrite(Log4NetControl.UserID)
        Dim dbConn As DBUtility = New DBUtility()
        Dim dt As DataTable = New DataTable()
        Dim sqlStr As String = "SELECT * "
        'sqlStr = sqlStr & "COUNTRY as 'Country',REGION1 as 'Region1',REGION2 as 'Region2',ASC_CODE as 'Asc Code',ASC_NAME as 'Asc Name',SERVICE_SHEET_NO as 'Service Sheet No',MODEL_CODE as 'Model Code',MODEL_NAME as 'Model Name',SERIAL_NO as 'Serial No',CREATE_DATE as 'Create Date', CUSTOMER_NAME as 'Customer Name',REPAIR_STATUS as 'Repair Status',TERMINATED_DATE as 'Terminated Date',STATUS_REMARKS as 'Status Remarks',ST_TYPE as 'St Type',CANCEL_REASON as 'Cancel Reason',REPAIR_CONTENTS as 'Repair Contents' "
        sqlStr = sqlStr & "FROM "
        sqlStr = sqlStr & "SONY_DAILY_UNREPAIPAIRSET_NP "
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
