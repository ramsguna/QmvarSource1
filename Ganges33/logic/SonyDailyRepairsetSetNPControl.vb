Imports System
Imports System.Data
Imports System.Web.Security
Imports System.Collections.Generic
Imports Ganges33.Ganges33.dao
Imports Ganges33.Ganges33.model
Imports Ganges33.Ganges33.logic


Public Class SonyDailyRepairsetSetNPControl
    Public Function AddModifyDailyRepairsetSetNP(ByVal csvData()() As String, queryParams As SonyDailyRepairsetSetNPModel) As Boolean
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
        sqlStr = "SELECT TOP 1 COUNTRY FROM SONY_DAILY_REPAIRSETSET_NP "
        sqlStr = sqlStr & " WHERE DELFG = 0 AND SRC_FILE_NAME='" & queryParams.SRC_FILE_NAME & "'"
        'sqlStr = sqlStr & " AND SHIP_TO_BRANCH_CODE='" & queryParams.ShipToBranchCode & "'"
        dtSawDiscountExist = dbConn.GetDataSet(sqlStr)
        'if exist then need to update delfg=1 then insert the record as new
        If (dtSawDiscountExist Is Nothing) Or (dtSawDiscountExist.Rows.Count = 0) Then
            'isExist = 0
        Else 'The records is already exist, need to update DELFg=0
            ' isExist = 1
            sqlStr = "UPDATE SONY_DAILY_REPAIRSETSET_NP SET DELFG=1  "
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
                sqlStr = "Insert into SONY_DAILY_REPAIRSETSET_NP ("
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
                sqlStr = sqlStr & "ASC_CODE, "
                sqlStr = sqlStr & "ASC_NAME, "
                sqlStr = sqlStr & "JOB_NUMBER, "
                sqlStr = sqlStr & "SEQ, "
                sqlStr = sqlStr & "PRODUCT_CATEGORY, "
                sqlStr = sqlStr & "PRODUCT_SUB_CATEGORY, "
                sqlStr = sqlStr & "MODEL_CODE, "
                sqlStr = sqlStr & "MODEL_NAME, "
                sqlStr = sqlStr & "SERIAL_NO, "
                sqlStr = sqlStr & "SERVICE_TYPE, "
                sqlStr = sqlStr & "TRANSFER_FLAG, "
                sqlStr = sqlStr & "TRANSFER_JOB_NO, "
                sqlStr = sqlStr & "GUARANTEE_CODE, "
                sqlStr = sqlStr & "CUSTOMER_GROUP, "
                sqlStr = sqlStr & "CUSTOMER_NAME, "
                sqlStr = sqlStr & "EMAIL, "
                sqlStr = sqlStr & "LINKMAN, "
                sqlStr = sqlStr & "PHONE, "
                sqlStr = sqlStr & "MOBIL, "
                sqlStr = sqlStr & "ADDRES, "
                sqlStr = sqlStr & "CITY, "
                sqlStr = sqlStr & "PROVINCE, "
                sqlStr = sqlStr & "POST_CODE, "
                sqlStr = sqlStr & "PURCHASED_DATE, "
                sqlStr = sqlStr & "WARRANTY_TYPE, "
                sqlStr = sqlStr & "WARRANTY_CATEGORY, "
                sqlStr = sqlStr & "WARRANTY_CARD_NO, "
                sqlStr = sqlStr & "WARRANTY_CARD_TYPE, "
                sqlStr = sqlStr & "TECHNICIAN, "
                sqlStr = sqlStr & "RESERVATION_CREATE_DATE, "
                sqlStr = sqlStr & "JOB_CREATE_DATE, "
                sqlStr = sqlStr & "FIRST_ALLOCATION_DATE, "
                sqlStr = sqlStr & "BEGIN_REPAIR_DATE, "
                sqlStr = sqlStr & "REPAIR_COMPLETED_DATE, "
                sqlStr = sqlStr & "REPAIR_RETURNED_DATE, "
                sqlStr = sqlStr & "PART_CODE, "
                sqlStr = sqlStr & "PART_DESC, "
                sqlStr = sqlStr & "REPAIR_QTY, "
                sqlStr = sqlStr & "PART_UNIT_PRICE, "
                sqlStr = sqlStr & "PO_NO, "
                sqlStr = sqlStr & "PO_CREATE_DATE, "
                sqlStr = sqlStr & "SHIPPED_DATE, "
                sqlStr = sqlStr & "PARTS_RECEIVED_DATE, "
                sqlStr = sqlStr & "SYMPTOM_CODE, "
                sqlStr = sqlStr & "SECTION_CODE, "
                sqlStr = sqlStr & "DEFECT_CODE, "
                sqlStr = sqlStr & "REPAIR_CODE, "
                sqlStr = sqlStr & "REPAIR_LEVEL, "
                sqlStr = sqlStr & "REPAIR_FEE_TYPE, "
                sqlStr = sqlStr & "PART_FEE, "
                sqlStr = sqlStr & "INSPECTION_FEE, "
                sqlStr = sqlStr & "HANDLING_FEE, "
                sqlStr = sqlStr & "LABOR_FEE, "
                sqlStr = sqlStr & "HOME_SERVICE_FEE, "
                sqlStr = sqlStr & "LONG_FEE, "
                sqlStr = sqlStr & "INSTALL_FEE, "
                sqlStr = sqlStr & "TOTAL_AMOUNT_OF_ACCOUNT_PAYABLE, "
                sqlStr = sqlStr & "ACCOUNT_PAYABLE_BY_CUSTOMER, "
                sqlStr = sqlStr & "SONY_NEEDS_TO_PAY, "
                sqlStr = sqlStr & "ASC_PAY, "
                sqlStr = sqlStr & "CR90, "
                sqlStr = sqlStr & "RR90, "
                sqlStr = sqlStr & "REPAIR_TAT, "
                sqlStr = sqlStr & "D4D, "
                sqlStr = sqlStr & "MODEL_6D, "
                sqlStr = sqlStr & "D6D_DESC, "
                sqlStr = sqlStr & "NPRR, "
                sqlStr = sqlStr & "REPAIR_ACTION_TECHNICIAN_REMARKS, "
                sqlStr = sqlStr & "ST_TYPE, "
                sqlStr = sqlStr & "LAST_ALLOCATION_DATE, "
                sqlStr = sqlStr & "VENDOR_PART_PRICE, "
                sqlStr = sqlStr & "FIRST_ESTIMATION_CREATE_DATE, "
                sqlStr = sqlStr & "LAST_ESTIMATION_DATE, "
                sqlStr = sqlStr & "ESTIMATION_TAT, "
                sqlStr = sqlStr & "LATEST_ESTIMATE_STATUS, "
                sqlStr = sqlStr & "PARTS_REQUEST_DATE, "
                sqlStr = sqlStr & "PARTS_WAITING_TAT, "
                sqlStr = sqlStr & "LAST_STATUS_UPDATE_DATE, "
                sqlStr = sqlStr & "CUSTOMER_COMPLAINT, "
                sqlStr = sqlStr & "SYMPTOM_CONFIRMED_BY_TECHNICIAN, "
                sqlStr = sqlStr & "IRIS_LINE_TRANSFER_FLAG, "
                sqlStr = sqlStr & "CCC_ID, "
                sqlStr = sqlStr & "ASSIGNED_BY, "
                sqlStr = sqlStr & "CONDITION_CODE, "
                sqlStr = sqlStr & "PART_6D, "
                sqlStr = sqlStr & "CONVERTTOJOB_IN_MAPP, "
                sqlStr = sqlStr & "COMPLETED_IN_MAPP, "
                sqlStr = sqlStr & "DELIVER_IN_MAPP, "
                sqlStr = sqlStr & "RESERVE_ID, "
                sqlStr = sqlStr & "CAUSED_BY_CUSTOMER, "
                sqlStr = sqlStr & "INTERNAL_MESSAGE, "
                sqlStr = sqlStr & "ADJUSTMENT_FEE, "
                sqlStr = sqlStr & "DA_FEE, "
                sqlStr = sqlStr & "FIT_UNFIT_FEE, "
                sqlStr = sqlStr & "MU_FEE, "
                sqlStr = sqlStr & "TRAVEL_ALLOWANCE_FEE, "
                sqlStr = sqlStr & "REGION2, "
                sqlStr = sqlStr & "UPDATED_BY_CCC_USER, "



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
                sqlStr = sqlStr & "@ASC_CODE, "
                sqlStr = sqlStr & "@ASC_NAME, "
                sqlStr = sqlStr & "@JOB_NUMBER, "
                sqlStr = sqlStr & "@SEQ, "
                sqlStr = sqlStr & "@PRODUCT_CATEGORY, "
                sqlStr = sqlStr & "@PRODUCT_SUB_CATEGORY, "
                sqlStr = sqlStr & "@MODEL_CODE, "
                sqlStr = sqlStr & "@MODEL_NAME, "
                sqlStr = sqlStr & "@SERIAL_NO, "
                sqlStr = sqlStr & "@SERVICE_TYPE, "
                sqlStr = sqlStr & "@TRANSFER_FLAG, "
                sqlStr = sqlStr & "@TRANSFER_JOB_NO, "
                sqlStr = sqlStr & "@GUARANTEE_CODE, "
                sqlStr = sqlStr & "@CUSTOMER_GROUP, "
                sqlStr = sqlStr & "@CUSTOMER_NAME, "
                sqlStr = sqlStr & "@EMAIL, "
                sqlStr = sqlStr & "@LINKMAN, "
                sqlStr = sqlStr & "@PHONE, "
                sqlStr = sqlStr & "@MOBIL, "
                sqlStr = sqlStr & "@ADDRES, "
                sqlStr = sqlStr & "@CITY, "
                sqlStr = sqlStr & "@PROVINCE, "
                sqlStr = sqlStr & "@POST_CODE, "
                sqlStr = sqlStr & "@PURCHASED_DATE, "
                sqlStr = sqlStr & "@WARRANTY_TYPE, "
                sqlStr = sqlStr & "@WARRANTY_CATEGORY, "
                sqlStr = sqlStr & "@WARRANTY_CARD_NO, "
                sqlStr = sqlStr & "@WARRANTY_CARD_TYPE, "
                sqlStr = sqlStr & "@TECHNICIAN, "
                sqlStr = sqlStr & "@RESERVATION_CREATE_DATE, "
                sqlStr = sqlStr & "@JOB_CREATE_DATE, "
                sqlStr = sqlStr & "@FIRST_ALLOCATION_DATE, "
                sqlStr = sqlStr & "@BEGIN_REPAIR_DATE, "
                sqlStr = sqlStr & "@REPAIR_COMPLETED_DATE, "
                sqlStr = sqlStr & "@REPAIR_RETURNED_DATE, "
                sqlStr = sqlStr & "@PART_CODE, "
                sqlStr = sqlStr & "@PART_DESC, "
                sqlStr = sqlStr & "@REPAIR_QTY, "
                sqlStr = sqlStr & "@PART_UNIT_PRICE, "
                sqlStr = sqlStr & "@PO_NO, "
                sqlStr = sqlStr & "@PO_CREATE_DATE, "
                sqlStr = sqlStr & "@SHIPPED_DATE, "
                sqlStr = sqlStr & "@PARTS_RECEIVED_DATE, "
                sqlStr = sqlStr & "@SYMPTOM_CODE, "
                sqlStr = sqlStr & "@SECTION_CODE, "
                sqlStr = sqlStr & "@DEFECT_CODE, "
                sqlStr = sqlStr & "@REPAIR_CODE, "
                sqlStr = sqlStr & "@REPAIR_LEVEL, "
                sqlStr = sqlStr & "@REPAIR_FEE_TYPE, "
                sqlStr = sqlStr & "@PART_FEE, "
                sqlStr = sqlStr & "@INSPECTION_FEE, "
                sqlStr = sqlStr & "@HANDLING_FEE, "
                sqlStr = sqlStr & "@LABOR_FEE, "
                sqlStr = sqlStr & "@HOME_SERVICE_FEE, "
                sqlStr = sqlStr & "@LONG_FEE, "
                sqlStr = sqlStr & "@INSTALL_FEE, "
                sqlStr = sqlStr & "@TOTAL_AMOUNT_OF_ACCOUNT_PAYABLE, "
                sqlStr = sqlStr & "@ACCOUNT_PAYABLE_BY_CUSTOMER, "
                sqlStr = sqlStr & "@SONY_NEEDS_TO_PAY, "
                sqlStr = sqlStr & "@ASC_PAY, "
                sqlStr = sqlStr & "@CR90, "
                sqlStr = sqlStr & "@RR90, "
                sqlStr = sqlStr & "@REPAIR_TAT, "
                sqlStr = sqlStr & "@D4D, "
                sqlStr = sqlStr & "@MODEL_6D, "
                sqlStr = sqlStr & "@D6D_DESC, "
                sqlStr = sqlStr & "@NPRR, "
                sqlStr = sqlStr & "@REPAIR_ACTION_TECHNICIAN_REMARKS, "
                sqlStr = sqlStr & "@ST_TYPE, "
                sqlStr = sqlStr & "@LAST_ALLOCATION_DATE, "
                sqlStr = sqlStr & "@VENDOR_PART_PRICE, "
                sqlStr = sqlStr & "@FIRST_ESTIMATION_CREATE_DATE, "
                sqlStr = sqlStr & "@LAST_ESTIMATION_DATE, "
                sqlStr = sqlStr & "@ESTIMATION_TAT, "
                sqlStr = sqlStr & "@LATEST_ESTIMATE_STATUS, "
                sqlStr = sqlStr & "@PARTS_REQUEST_DATE, "
                sqlStr = sqlStr & "@PARTS_WAITING_TAT, "
                sqlStr = sqlStr & "@LAST_STATUS_UPDATE_DATE, "
                sqlStr = sqlStr & "@CUSTOMER_COMPLAINT, "
                sqlStr = sqlStr & "@SYMPTOM_CONFIRMED_BY_TECHNICIAN, "
                sqlStr = sqlStr & "@IRIS_LINE_TRANSFER_FLAG, "
                sqlStr = sqlStr & "@CCC_ID, "
                sqlStr = sqlStr & "@ASSIGNED_BY, "
                sqlStr = sqlStr & "@CONDITION_CODE, "
                sqlStr = sqlStr & "@PART_6D, "
                sqlStr = sqlStr & "@CONVERTTOJOB_IN_MAPP, "
                sqlStr = sqlStr & "@COMPLETED_IN_MAPP, "
                sqlStr = sqlStr & "@DELIVER_IN_MAPP, "
                sqlStr = sqlStr & "@RESERVE_ID, "
                sqlStr = sqlStr & "@CAUSED_BY_CUSTOMER, "
                sqlStr = sqlStr & "@INTERNAL_MESSAGE, "
                sqlStr = sqlStr & "@ADJUSTMENT_FEE, "
                sqlStr = sqlStr & "@DA_FEE, "
                sqlStr = sqlStr & "@FIT_UNFIT_FEE, "
                sqlStr = sqlStr & "@MU_FEE, "
                sqlStr = sqlStr & "@TRAVEL_ALLOWANCE_FEE, "
                sqlStr = sqlStr & "@REGION2, "
                sqlStr = sqlStr & "@UPDATED_BY_CCC_USER, "


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
                dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@ASC_CODE", csvData(i)(3)))
                dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@ASC_NAME", csvData(i)(4)))
                dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@JOB_NUMBER", csvData(i)(5)))
                dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@SEQ", csvData(i)(6)))
                dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@PRODUCT_CATEGORY", csvData(i)(7)))
                dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@PRODUCT_SUB_CATEGORY", csvData(i)(8)))
                dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@MODEL_CODE", csvData(i)(9)))
                dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@MODEL_NAME", csvData(i)(10)))
                dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@SERIAL_NO", csvData(i)(11)))
                dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@SERVICE_TYPE", csvData(i)(12)))
                dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@TRANSFER_FLAG", csvData(i)(13)))
                dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@TRANSFER_JOB_NO", csvData(i)(14)))
                dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@GUARANTEE_CODE", csvData(i)(15)))
                dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@CUSTOMER_GROUP", csvData(i)(16)))
                dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@CUSTOMER_NAME", csvData(i)(17)))
                dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@EMAIL", csvData(i)(18)))
                dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@LINKMAN", csvData(i)(19)))
                dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@PHONE", csvData(i)(20)))
                dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@MOBIL", csvData(i)(21)))
                dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@ADDRES", csvData(i)(22)))
                dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@CITY", csvData(i)(23)))
                dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@PROVINCE", csvData(i)(24)))
                dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@POST_CODE", csvData(i)(25)))
                dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@PURCHASED_DATE", csvData(i)(26)))
                dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@WARRANTY_TYPE", csvData(i)(27)))
                dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@WARRANTY_CATEGORY", csvData(i)(28)))
                dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@WARRANTY_CARD_NO", csvData(i)(29)))
                dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@WARRANTY_CARD_TYPE", csvData(i)(30)))
                dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@TECHNICIAN", csvData(i)(31)))
                dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@RESERVATION_CREATE_DATE", csvData(i)(32)))
                dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@JOB_CREATE_DATE", csvData(i)(33)))
                dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@FIRST_ALLOCATION_DATE", csvData(i)(34)))
                dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@BEGIN_REPAIR_DATE", csvData(i)(35)))
                dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@REPAIR_COMPLETED_DATE", csvData(i)(36)))
                dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@REPAIR_RETURNED_DATE", csvData(i)(37)))
                dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@PART_CODE", csvData(i)(38)))
                dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@PART_DESC", csvData(i)(39)))
                dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@REPAIR_QTY", csvData(i)(40)))
                dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@PART_UNIT_PRICE", csvData(i)(41)))
                dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@PO_NO", csvData(i)(42)))
                dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@PO_CREATE_DATE", csvData(i)(43)))
                dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@SHIPPED_DATE", csvData(i)(44)))
                dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@PARTS_RECEIVED_DATE", csvData(i)(45)))
                dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@SYMPTOM_CODE", csvData(i)(46)))
                dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@SECTION_CODE", csvData(i)(47)))
                dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@DEFECT_CODE", csvData(i)(48)))
                dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@REPAIR_CODE", csvData(i)(49)))
                dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@REPAIR_LEVEL", csvData(i)(50)))
                dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@REPAIR_FEE_TYPE", csvData(i)(51)))
                dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@PART_FEE", csvData(i)(52)))
                dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@INSPECTION_FEE", csvData(i)(53)))
                dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@HANDLING_FEE", csvData(i)(54)))
                dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@LABOR_FEE", csvData(i)(55)))
                dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@HOME_SERVICE_FEE", csvData(i)(56)))
                dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@LONG_FEE", csvData(i)(57)))
                dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@INSTALL_FEE", csvData(i)(58)))
                dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@TOTAL_AMOUNT_OF_ACCOUNT_PAYABLE", csvData(i)(59)))
                dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@ACCOUNT_PAYABLE_BY_CUSTOMER", csvData(i)(60)))
                dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@SONY_NEEDS_TO_PAY", csvData(i)(61)))
                dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@ASC_PAY", csvData(i)(62)))
                dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@CR90", csvData(i)(63)))
                dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@RR90", csvData(i)(64)))
                dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@REPAIR_TAT", csvData(i)(65)))
                dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@D4D", csvData(i)(66)))
                dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@MODEL_6D", csvData(i)(67)))
                dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@D6D_DESC", csvData(i)(68)))
                dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@NPRR", csvData(i)(69)))
                dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@REPAIR_ACTION_TECHNICIAN_REMARKS", csvData(i)(70)))
                dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@ST_TYPE", csvData(i)(71)))
                dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@LAST_ALLOCATION_DATE", csvData(i)(72)))
                dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@VENDOR_PART_PRICE", csvData(i)(73)))
                dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@FIRST_ESTIMATION_CREATE_DATE", csvData(i)(74)))
                dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@LAST_ESTIMATION_DATE", csvData(i)(75)))
                dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@ESTIMATION_TAT", csvData(i)(76)))
                dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@LATEST_ESTIMATE_STATUS", csvData(i)(77)))
                dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@PARTS_REQUEST_DATE", csvData(i)(78)))
                dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@PARTS_WAITING_TAT", csvData(i)(79)))
                dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@LAST_STATUS_UPDATE_DATE", csvData(i)(80)))
                dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@CUSTOMER_COMPLAINT", csvData(i)(81)))
                dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@SYMPTOM_CONFIRMED_BY_TECHNICIAN", csvData(i)(82)))
                dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@IRIS_LINE_TRANSFER_FLAG", csvData(i)(83)))
                dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@CCC_ID", csvData(i)(84)))
                dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@ASSIGNED_BY", csvData(i)(85)))
                dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@CONDITION_CODE", csvData(i)(86)))
                dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@PART_6D", csvData(i)(87)))
                dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@CONVERTTOJOB_IN_MAPP", csvData(i)(88)))
                dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@COMPLETED_IN_MAPP", csvData(i)(89)))
                dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@DELIVER_IN_MAPP", csvData(i)(90)))
                dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@RESERVE_ID", csvData(i)(91)))
                dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@CAUSED_BY_CUSTOMER", csvData(i)(92)))
                dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@INTERNAL_MESSAGE", csvData(i)(93)))
                dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@ADJUSTMENT_FEE", csvData(i)(94)))
                dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@DA_FEE", csvData(i)(95)))
                dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@FIT_UNFIT_FEE", csvData(i)(96)))
                dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@MU_FEE", csvData(i)(97)))
                dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@TRAVEL_ALLOWANCE_FEE", csvData(i)(98)))
                dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@REGION2", csvData(i)(99)))
                dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@UPDATED_BY_CCC_USER", csvData(i)(100)))


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


    Public Function SelectDailyRepairsetSetNP(ByVal queryParams As SonyDailyRepairsetSetNPModel) As DataTable
        Log4NetControl.ComInfoLogWrite(Log4NetControl.UserID)
        Dim dbConn As DBUtility = New DBUtility()
        Dim dt As DataTable = New DataTable()
        Dim sqlStr As String = "SELECT "
        sqlStr = sqlStr & "COUNTRY as 'Country',"
        sqlStr = sqlStr & "REGION as 'Region',"
        sqlStr = sqlStr & "REGION_NAME as 'RegionName',"
        sqlStr = sqlStr & "ASC_CODE as 'AscCode',"
        sqlStr = sqlStr & "ASC_NAME as 'AscName',"
        sqlStr = sqlStr & "JOB_NUMBER as 'JobNumber',"
        sqlStr = sqlStr & "SEQ as 'Seq',"
        sqlStr = sqlStr & "PRODUCT_CATEGORY as 'ProductCategory',"
        sqlStr = sqlStr & "PRODUCT_SUB_CATEGORY as 'ProductSubCategory',"
        sqlStr = sqlStr & "MODEL_CODE as 'ModelCode',"
        sqlStr = sqlStr & "MODEL_NAME as 'ModelName',"
        sqlStr = sqlStr & "SERIAL_NO as 'SerialNo',"
        sqlStr = sqlStr & "SERVICE_TYPE as 'ServiceType',"
        sqlStr = sqlStr & "TRANSFER_FLAG as 'TransferFlag',"
        sqlStr = sqlStr & "TRANSFER_JOB_NO as 'transferJobNo',"
        sqlStr = sqlStr & "GUARANTEE_CODE as 'GuaranteeCode',"
        sqlStr = sqlStr & "CUSTOMER_GROUP as 'CustomerGroup',"
        sqlStr = sqlStr & "CUSTOMER_NAME as 'CustomerName',"
        sqlStr = sqlStr & "EMAIL as 'Email',"
        sqlStr = sqlStr & "LINKMAN as 'LinkMan',"
        sqlStr = sqlStr & "PHONE as 'Phone', "
        sqlStr = sqlStr & "MOBIL as 'Mobile',"
        sqlStr = sqlStr & "ADDRES as 'Addres',"
        sqlStr = sqlStr & "CITY as 'City',"
        sqlStr = sqlStr & "PROVINCE as 'Province',"
        sqlStr = sqlStr & "POST_CODE as 'PostCode',"
        sqlStr = sqlStr & "PURCHASED_DATE as 'PurchasedDate',"
        sqlStr = sqlStr & "WARRANTY_TYPE as 'WarrantyCategory',"
        sqlStr = sqlStr & "WARRANTY_CATEGORY as 'WarrantyCategory',"
        sqlStr = sqlStr & "WARRANTY_CARD_NO as 'WarrantyCardNo',"
        sqlStr = sqlStr & "WARRANTY_CARD_TYPE as 'WarrantyCardType',"
        sqlStr = sqlStr & "TECHNICIAN as 'Technician',"
        sqlStr = sqlStr & "RESERVATION_CREATE_DATE as 'ReservationCreateDate',"
        sqlStr = sqlStr & "JOB_CREATE_DATE as 'JobCreateDate',"
        sqlStr = sqlStr & "FIRST_ALLOCATION_DATE as 'FirstAllocationDate',"
        sqlStr = sqlStr & "BEGIN_REPAIR_DATE as 'BeginRepairDate',"
        sqlStr = sqlStr & "REPAIR_COMPLETED_DATE as 'RepairCompletedDate',"
        sqlStr = sqlStr & "REPAIR_RETURNED_DATE as 'RepairReturnedDate' ,"
        sqlStr = sqlStr & "PART_CODE as 'PartCode' ,"
        sqlStr = sqlStr & "PART_DESC as 'PartDesc', "
        sqlStr = sqlStr & "REPAIR_QTY as 'RepairQty' ,"
        sqlStr = sqlStr & "PART_UNIT_PRICE as 'PartUnitPrice',"
        sqlStr = sqlStr & "PO_NO as 'PONO',"
        sqlStr = sqlStr & "PO_CREATE_DATE as 'POCreateDate',"
        sqlStr = sqlStr & "SHIPPED_DATE as 'Shipped Date',"
        sqlStr = sqlStr & "PARTS_RECEIVED_DATE as 'PartsRervicedDate',"
        sqlStr = sqlStr & "SYMPTOM_CODE as 'SymptomCode',"
        sqlStr = sqlStr & "SECTION_CODE as 'SectionCode',"
        sqlStr = sqlStr & "DEFECT_CODE as 'DefectCode',"
        sqlStr = sqlStr & "REPAIR_CODE as 'RepairCode',"
        sqlStr = sqlStr & "REPAIR_LEVEL as 'RepairLevel',"
        sqlStr = sqlStr & "REPAIR_FEE_TYPE as 'RepairFeeType',"
        sqlStr = sqlStr & "PART_FEE as 'PartFee',"
        sqlStr = sqlStr & "INSPECTION_FEE as 'InspectionFee',"
        sqlStr = sqlStr & "HANDLING_FEE as 'HandlingFee',"
        sqlStr = sqlStr & "LABOR_FEE as 'LaborFee' ,"
        sqlStr = sqlStr & "HOME_SERVICE_FEE as 'HomeServiceFee' ,"
        sqlStr = sqlStr & "LONG_FEE as 'Long fee', "
        sqlStr = sqlStr & "INSTALL_FEE as 'InstallFee' ,"
        sqlStr = sqlStr & "TOTAL_AMOUNT_OF_ACCOUNT_PAYABLE as 'TotalAmountOfAccountPayable',"
        sqlStr = sqlStr & "ACCOUNT_PAYABLE_BY_CUSTOMER as 'AccountPayableByCustomer',"
        sqlStr = sqlStr & "SONY_NEEDS_TO_PAY as 'SonyNeedsToPay',"
        sqlStr = sqlStr & "ASC_PAY as 'ASCpay',"
        sqlStr = sqlStr & "CR90 as 'CR90',"
        sqlStr = sqlStr & "RR90 as 'RR90',"
        sqlStr = sqlStr & "REPAIR_TAT as 'RepairTat',"
        sqlStr = sqlStr & "D4D as '4D',"
        sqlStr = sqlStr & "MODEL_6D as 'MODEL6D',"
        sqlStr = sqlStr & "D6D_DESC as '6DDesc',"
        sqlStr = sqlStr & "NPRR as 'Nprr',"
        sqlStr = sqlStr & "REPAIR_ACTION_TECHNICIAN_REMARKS as 'RepairAction/TechnicianRemarks',"
        sqlStr = sqlStr & "ST_TYPE as 'St_type' ,"
        sqlStr = sqlStr & "LAST_ALLOCATION_DATE as 'LastAllocationDate' ,"
        sqlStr = sqlStr & "VENDOR_PART_PRICE as 'vendorPartPrice', "
        sqlStr = sqlStr & "FIRST_ESTIMATION_CREATE_DATE as 'FirstEstimationCreateDate' ,"
        sqlStr = sqlStr & "LAST_ESTIMATION_DATE as 'LastEstimationDate',"
        sqlStr = sqlStr & "ESTIMATION_TAT as 'EstimationTat',"
        sqlStr = sqlStr & "LATEST_ESTIMATE_STATUS as 'LatestEstimateStatus',"
        sqlStr = sqlStr & "PARTS_REQUEST_DATE as 'PartsRequestDate',"
        sqlStr = sqlStr & "PARTS_WAITING_TAT as 'PartsWaitingTat',"
        sqlStr = sqlStr & "LAST_STATUS_UPDATE_DATE as 'LastStatusUpdateDate',"
        sqlStr = sqlStr & "CUSTOMER_COMPLAINT as 'CustomerComplaint',"
        sqlStr = sqlStr & "SYMPTOM_CONFIRMED_BY_TECHNICIAN as 'SymptomConfirmedByTechnician',"
        sqlStr = sqlStr & "IRIS_LINE_TRANSFER_FLAG as 'IrisLineTransferflag',"
        sqlStr = sqlStr & "CCC_ID as 'CccId',"
        sqlStr = sqlStr & "ASSIGNED_BY as 'Assignedby',"
        sqlStr = sqlStr & "CONDITION_CODE as 'ConditionCode',"
        sqlStr = sqlStr & "PART_6D as 'Part6D',"
        sqlStr = sqlStr & "CONVERTTOJOB_IN_MAPP as 'ConvertToJobinMapp',"
        sqlStr = sqlStr & "COMPLETED_IN_MAPP as 'CompletedinMapp' ,"
        sqlStr = sqlStr & "DELIVER_IN_MAPP as 'DeliverInMapp' ,"
        sqlStr = sqlStr & "RESERVE_ID as 'ReserveId', "
        sqlStr = sqlStr & "CAUSED_BY_CUSTOMER as 'CausedByCustomer' ,"
        sqlStr = sqlStr & "INTERNAL_MESSAGE as 'InternalMessage',"
        sqlStr = sqlStr & "ADJUSTMENT_FEE as 'AdjustmentFee',"
        sqlStr = sqlStr & "DA_FEE as 'DaFee',"
        sqlStr = sqlStr & "FIT_UNFIT_FEE as 'Fit/UnfitFee',"
        sqlStr = sqlStr & "MU_FEE as 'MuFee' ,"
        sqlStr = sqlStr & "TRAVEL_ALLOWANCE_FEE as 'TravelAllowanceFee' ,"
        sqlStr = sqlStr & "REGION2 as 'Region', "
        sqlStr = sqlStr & "UPDATED_BY_CCC_USER as 'UpdatedbyCCCUser' "

        sqlStr = sqlStr & "FROM "
        sqlStr = sqlStr & "SONY_DAILY_REPAIRSETSET_NP "


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
