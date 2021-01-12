Imports System
Imports System.Data
Imports System.Web.Security
Imports System.Collections.Generic
Imports Ganges33.Ganges33.dao
Imports Ganges33.Ganges33.model
Imports Ganges33.Ganges33.logic


Public Class SonyMonthlyRepairsetControl
    Public Function AddSonyMonthlyRepairset(ByVal csvData()() As String, queryParams As SonyMonthlyRepairsetModel) As Boolean
        Log4NetControl.ComInfoLogWrite(Log4NetControl.UserID)

        Dim flag As Boolean = True
        If csvData(0).Length < 77 Then
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
        Dim dtMonthlyRepairset As DataTable

        Dim isExist As Integer = 0
        '1st check COUNTRY
        sqlStr = "SELECT TOP 1 COUNTRY FROM SONY_MONTHLY_REPAIRSET "
        sqlStr = sqlStr & " WHERE DELFG = 0 AND SRC_FILE_NAME='" & queryParams.SRC_FILE_NAME & "'"
        'sqlStr = sqlStr & " AND SHIP_TO_BRANCH_CODE='" & queryParams.ShipToBranchCode & "'"
        dtMonthlyRepairset = dbConn.GetDataSet(sqlStr)
        'if exist then need to update delfg=1 then insert the record as new
        If (dtMonthlyRepairset Is Nothing) Or (dtMonthlyRepairset.Rows.Count = 0) Then
            'isExist = 0
        Else 'The records is already exist, need to update DELFg=0
            ' isExist = 1
            sqlStr = "UPDATE SONY_MONTHLY_REPAIRSET SET DELFG=1  "
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
                sqlStr = "Insert into SONY_MONTHLY_REPAIRSET ("
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
                sqlStr = sqlStr & "CITY_NAME, "
                sqlStr = sqlStr & "PURCHASED_DATE, "
                sqlStr = sqlStr & "WARRANTY_TYPE, "
                sqlStr = sqlStr & "WARRANTY_CATEGORY, "
                sqlStr = sqlStr & "WARRANTY_CARD_NO, "
                sqlStr = sqlStr & "WARRANTY_CARD_TYPE, "
                sqlStr = sqlStr & "TECHNICIAN, "
                sqlStr = sqlStr & "RESERVATION_CREATE_DATE, "
                sqlStr = sqlStr & "JOB_CREATE_DATE, "
                sqlStr = sqlStr & "ALLOCATION_DATE, "
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
                sqlStr = sqlStr & "IN_OUT_DATE, "
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
                sqlStr = sqlStr & "PART_6D, "
                sqlStr = sqlStr & "VENDOR_PART_PRICE, "
                sqlStr = sqlStr & "FIRST_ESTIMATION_CREATE_DATE, "
                sqlStr = sqlStr & "LAST_ESTIMATION_DATE, "
                sqlStr = sqlStr & "LATEST_ESTIMATE_STATUS, "
                sqlStr = sqlStr & "PARTS_REQUEST_DATE, "
                sqlStr = sqlStr & "ASC_PO_NUMBER, "
                sqlStr = sqlStr & "ASC_PO_DATE, "
                sqlStr = sqlStr & "LAST_STATUS_UPDATE_DATE, "
                sqlStr = sqlStr & "ST_TYPE, "
                sqlStr = sqlStr & "TECHNICIAN_REMARKS, "
                sqlStr = sqlStr & "PART_ALLOCATED, "
                sqlStr = sqlStr & "IRIS_LINE_TRANSFER_FLAG, "
                sqlStr = sqlStr & "CONVERTTOJOB_IN_MAPP, "
                sqlStr = sqlStr & "COMPLETED_IN_MAPP, "
                sqlStr = sqlStr & "DELIVER_IN_MAPP, "


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
                sqlStr = sqlStr & "@CITY_NAME, "

                sqlStr = sqlStr & "@PURCHASED_DATE, "
                sqlStr = sqlStr & "@WARRANTY_TYPE, "
                sqlStr = sqlStr & "@WARRANTY_CATEGORY, "
                sqlStr = sqlStr & "@WARRANTY_CARD_NO, "
                sqlStr = sqlStr & "@WARRANTY_CARD_TYPE, "
                sqlStr = sqlStr & "@TECHNICIAN, "
                sqlStr = sqlStr & "@RESERVATION_CREATE_DATE, "
                sqlStr = sqlStr & "@JOB_CREATE_DATE, "
                sqlStr = sqlStr & "@ALLOCATION_DATE, "
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
                sqlStr = sqlStr & "@IN_OUT_DATE, "
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
                sqlStr = sqlStr & "@PART_6D, "
                sqlStr = sqlStr & "@VENDOR_PART_PRICE, "
                sqlStr = sqlStr & "@FIRST_ESTIMATION_CREATE_DATE, "
                sqlStr = sqlStr & "@LAST_ESTIMATION_DATE, "
                sqlStr = sqlStr & "@LATEST_ESTIMATE_STATUS, "
                sqlStr = sqlStr & "@PARTS_REQUEST_DATE, "
                sqlStr = sqlStr & "@ASC_PO_NUMBER, "
                sqlStr = sqlStr & "@ASC_PO_DATE, "

                sqlStr = sqlStr & "@LAST_STATUS_UPDATE_DATE, "
                sqlStr = sqlStr & "@ST_TYPE, "
                sqlStr = sqlStr & "@TECHNICIAN_REMARKS, "
                sqlStr = sqlStr & "@PART_ALLOCATED, "
                sqlStr = sqlStr & "@IRIS_LINE_TRANSFER_FLAG, "
                sqlStr = sqlStr & "@CONVERTTOJOB_IN_MAPP, "
                sqlStr = sqlStr & "@COMPLETED_IN_MAPP, "
                sqlStr = sqlStr & "@DELIVER_IN_MAPP, "



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
                dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@ASC_CODE", csvData(i)(2)))
                dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@ASC_NAME", csvData(i)(3)))
                dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@JOB_NUMBER", csvData(i)(4)))
                dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@SEQ", csvData(i)(5)))
                dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@PRODUCT_CATEGORY", csvData(i)(6)))
                dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@PRODUCT_SUB_CATEGORY", csvData(i)(7)))
                dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@MODEL_CODE", csvData(i)(8)))

                dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@MODEL_NAME", csvData(i)(9)))
                dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@SERIAL_NO", csvData(i)(10)))
                dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@SERVICE_TYPE", csvData(i)(11)))
                dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@TRANSFER_FLAG", csvData(i)(12)))
                dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@TRANSFER_JOB_NO", csvData(i)(13)))
                dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@GUARANTEE_CODE", csvData(i)(14)))
                dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@CUSTOMER_GROUP", csvData(i)(15)))
                dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@CITY_NAME", csvData(i)(16)))
                dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@PURCHASED_DATE", csvData(i)(17)))

                dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@WARRANTY_TYPE", csvData(i)(18)))
                dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@WARRANTY_CATEGORY", csvData(i)(19)))
                dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@WARRANTY_CARD_NO", csvData(i)(20)))
                dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@WARRANTY_CARD_TYPE", csvData(i)(21)))
                dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@TECHNICIAN", csvData(i)(22)))
                dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@RESERVATION_CREATE_DATE", csvData(i)(23)))
                dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@JOB_CREATE_DATE", csvData(i)(24)))
                dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@ALLOCATION_DATE", csvData(i)(25)))
                dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@BEGIN_REPAIR_DATE", csvData(i)(26)))
                dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@REPAIR_COMPLETED_DATE", csvData(i)(27)))
                dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@REPAIR_RETURNED_DATE", csvData(i)(28)))
                dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@PART_CODE", csvData(i)(29)))
                dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@PART_DESC", csvData(i)(30)))
                dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@REPAIR_QTY", csvData(i)(31)))
                dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@PART_UNIT_PRICE", csvData(i)(32)))
                dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@PO_NO", csvData(i)(33)))
                dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@PO_CREATE_DATE", csvData(i)(34)))
                dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@SHIPPED_DATE", csvData(i)(35)))
                dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@IN_OUT_DATE", csvData(i)(36)))
                dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@SYMPTOM_CODE", csvData(i)(37)))
                dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@SECTION_CODE", csvData(i)(38)))
                dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@DEFECT_CODE", csvData(i)(39)))
                dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@REPAIR_CODE", csvData(i)(40)))
                dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@REPAIR_LEVEL", csvData(i)(41)))
                dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@REPAIR_FEE_TYPE", csvData(i)(42)))


                dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@PART_FEE", csvData(i)(43)))
                dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@INSPECTION_FEE", csvData(i)(44)))
                dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@HANDLING_FEE", csvData(i)(45)))
                dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@LABOR_FEE", csvData(i)(46)))
                dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@HOME_SERVICE_FEE", csvData(i)(47)))
                dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@LONG_FEE", csvData(i)(48)))
                dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@INSTALL_FEE", csvData(i)(49)))
                dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@TOTAL_AMOUNT_OF_ACCOUNT_PAYABLE", csvData(i)(50)))
                dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@ACCOUNT_PAYABLE_BY_CUSTOMER", csvData(i)(51)))
                dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@SONY_NEEDS_TO_PAY", csvData(i)(52)))
                dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@ASC_PAY", csvData(i)(53)))
                dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@CR90", csvData(i)(54)))
                dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@RR90", csvData(i)(55)))
                dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@REPAIR_TAT", csvData(i)(56)))
                dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@D4D", csvData(i)(57)))
                dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@MODEL_6D", csvData(i)(58)))
                dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@D6D_DESC", csvData(i)(59)))
                dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@NPRR", csvData(i)(60)))
                dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@PART_6D", csvData(i)(61)))
                dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@VENDOR_PART_PRICE", csvData(i)(62)))
                dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@FIRST_ESTIMATION_CREATE_DATE", csvData(i)(63)))
                dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@LAST_ESTIMATION_DATE", csvData(i)(64)))
                dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@LATEST_ESTIMATE_STATUS", csvData(i)(65)))
                dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@PARTS_REQUEST_DATE", csvData(i)(66)))
                dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@ASC_PO_NUMBER", csvData(i)(67)))


                dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@ASC_PO_DATE", csvData(i)(68)))
                dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@LAST_STATUS_UPDATE_DATE", csvData(i)(69)))
                dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@ST_TYPE", csvData(i)(70)))
                dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@TECHNICIAN_REMARKS", csvData(i)(71)))
                dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@PART_ALLOCATED", csvData(i)(72)))
                dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@IRIS_LINE_TRANSFER_FLAG", csvData(i)(73)))
                dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@CONVERTTOJOB_IN_MAPP", csvData(i)(74)))
                dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@COMPLETED_IN_MAPP", csvData(i)(75)))
                dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@DELIVER_IN_MAPP", csvData(i)(76)))

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


    Public Function SelectSonyMonthlyRepairset(ByVal queryParams As SonyMonthlyRepairsetModel) As DataTable
        Log4NetControl.ComInfoLogWrite(Log4NetControl.UserID)
        Dim dbConn As DBUtility = New DBUtility()
        Dim dt As DataTable = New DataTable()
        Dim sqlStr As String = "SELECT "

        sqlStr = sqlStr & "COUNTRY as 'Country',"
        sqlStr = sqlStr & "REGION as 'Region',"
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
        sqlStr = sqlStr & "TRANSFER_JOB_NO as 'TransferJobNo',"
        sqlStr = sqlStr & "GUARANTEE_CODE as 'GuaranteeCode',"
        sqlStr = sqlStr & "CUSTOMER_GROUP as 'CustomerGroup',"
        sqlStr = sqlStr & "CITY_NAME as 'CityName',"
        sqlStr = sqlStr & "PURCHASED_DATE as 'PurchasedDate',"
        sqlStr = sqlStr & "WARRANTY_TYPE as 'WarrantyType',"
        sqlStr = sqlStr & "WARRANTY_CATEGORY as 'WarrantyCatogery',"
        sqlStr = sqlStr & "WARRANTY_CARD_NO as 'WarrantyCardNo',"
        sqlStr = sqlStr & "WARRANTY_CARD_TYPE as 'WarrantyCardType', "

        sqlStr = sqlStr & "TECHNICIAN as 'Technician',"
        sqlStr = sqlStr & "RESERVATION_CREATE_DATE as 'ReservationCreateDate',"
        sqlStr = sqlStr & "JOB_CREATE_DATE as 'JobCreateDate',"
        sqlStr = sqlStr & "ALLOCATION_DATE as 'AllocationDate',"
        sqlStr = sqlStr & "BEGIN_REPAIR_DATE as 'BeginRepairDate',"
        sqlStr = sqlStr & "REPAIR_COMPLETED_DATE as 'RepairCompletedDate',"
        sqlStr = sqlStr & "REPAIR_RETURNED_DATE as 'RepairRetuenedDate',"
        sqlStr = sqlStr & "PART_CODE as 'PartCode',"
        sqlStr = sqlStr & "PART_DESC as 'PartDesc',"
        sqlStr = sqlStr & "REPAIR_QTY as 'RepairQty',"
        sqlStr = sqlStr & "PART_UNIT_PRICE as 'PartUnitPrice',"
        sqlStr = sqlStr & "PO_NO as 'PoNo',"
        sqlStr = sqlStr & "PO_CREATE_DATE as 'PoCreateDate',"
        sqlStr = sqlStr & "SHIPPED_DATE as 'ShippedDate',"
        sqlStr = sqlStr & "IN_OUT_DATE as 'InOutDate',"
        sqlStr = sqlStr & "SYMPTOM_CODE as 'SymtomCode',"
        sqlStr = sqlStr & "SECTION_CODE as 'SectionCode' ,"
        sqlStr = sqlStr & "DEFECT_CODE as 'DefectCode', "



        sqlStr = sqlStr & "REPAIR_CODE as 'RepairCode',"
        sqlStr = sqlStr & "REPAIR_LEVEL as 'RepairLevel',"
        sqlStr = sqlStr & "REPAIR_FEE_TYPE as 'RepairFeeType',"
        sqlStr = sqlStr & "PART_FEE as 'PartFee',"
        sqlStr = sqlStr & "INSPECTION_FEE as 'InspectionFee',"
        sqlStr = sqlStr & "HANDLING_FEE as 'HandlingFee',"
        sqlStr = sqlStr & "LABOR_FEE as 'LoaborFee',"
        sqlStr = sqlStr & "HOME_SERVICE_FEE as 'HomeServiceFee',"
        sqlStr = sqlStr & "LONG_FEE as 'LongFee',"
        sqlStr = sqlStr & "INSTALL_FEE as 'InstallFee',"
        sqlStr = sqlStr & "TOTAL_AMOUNT_OF_ACCOUNT_PAYABLE as 'TotalAmountOfAccountPayable',"
        sqlStr = sqlStr & "ACCOUNT_PAYABLE_BY_CUSTOMER as 'AccountPayableByCustomer',"
        sqlStr = sqlStr & "SONY_NEEDS_TO_PAY as 'SonyNeedsToPay',"
        sqlStr = sqlStr & "ASC_PAY as 'AscPay',"
        sqlStr = sqlStr & "CR90 as 'Cr90',"
        sqlStr = sqlStr & "RR90 as 'Rr90',"
        sqlStr = sqlStr & "REPAIR_TAT as 'RepairTat',"
        sqlStr = sqlStr & "D4D as '4D',"
        sqlStr = sqlStr & "MODEL_6D as 'Model6D',"
        sqlStr = sqlStr & "D6D_DESC as '6DDesc',"
        sqlStr = sqlStr & "NPRR as 'NPRR',"
        sqlStr = sqlStr & "PART_6D as 'Part6d', "

        sqlStr = sqlStr & "VENDOR_PART_PRICE as 'VendorPartPrice',"
        sqlStr = sqlStr & "FIRST_ESTIMATION_CREATE_DATE as 'FirstEstimationCreateDate',"
        sqlStr = sqlStr & "LAST_ESTIMATION_DATE as 'LatestEstimationDate',"
        sqlStr = sqlStr & "LATEST_ESTIMATE_STATUS as 'LatestEstimateStatus',"
        sqlStr = sqlStr & "PARTS_REQUEST_DATE as 'PartsRequestDate',"
        sqlStr = sqlStr & "ASC_PO_NUMBER as 'AscPoNo',"
        sqlStr = sqlStr & "ASC_PO_DATE as 'AscPoDate',"
        sqlStr = sqlStr & "LAST_STATUS_UPDATE_DATE as 'LastStatusUpdateDate',"
        sqlStr = sqlStr & "ST_TYPE as 'StType',"
        sqlStr = sqlStr & "TECHNICIAN_REMARKS as 'TechnicionRemarks',"
        sqlStr = sqlStr & "PART_ALLOCATED as 'PartAllocated',"
        sqlStr = sqlStr & "IRIS_LINE_TRANSFER_FLAG as 'IrisLineTransferFlag',"
        sqlStr = sqlStr & "CONVERTTOJOB_IN_MAPP as 'ConverttojobInMapp',"
        sqlStr = sqlStr & "COMPLETED_IN_MAPP as 'CompletedInMapp',"


        sqlStr = sqlStr & "DELIVER_IN_MAPP as 'DeliverInMapp' "


        sqlStr = sqlStr & "FROM "
        sqlStr = sqlStr & "SONY_MONTHLY_REPAIRSET "
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
