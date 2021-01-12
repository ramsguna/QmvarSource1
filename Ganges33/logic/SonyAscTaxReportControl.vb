Imports System
Imports System.Data
Imports System.Web.Security
Imports System.Collections.Generic
Imports Ganges33.Ganges33.dao
Imports Ganges33.Ganges33.model
Imports Ganges33.Ganges33.logic

Public Class SonyAscTaxReportControl
    Public Function AddModifyAscTaxReport(ByVal csvData()() As String, queryParams As SonyAscTaxReportModel) As Boolean
        Log4NetControl.ComInfoLogWrite(Log4NetControl.UserID)
        'Mandatory Column 39 from CSV
        Dim flag As Boolean = True
        If csvData(0).Length < 5 Then
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
        sqlStr = "SELECT TOP 1 REGION FROM SONY_ASC_TAX_REPORT "
        sqlStr = sqlStr & " WHERE DELFG = 0 AND SRC_FILE_NAME='" & queryParams.SRC_FILE_NAME & "'"
        'sqlStr = sqlStr & " AND SHIP_TO_BRANCH_CODE='" & queryParams.ShipToBranchCode & "'"
        dtTableExist = dbConn.GetDataSet(sqlStr)
        'if exist then need to update delfg=1 then insert the record as new
        If (dtTableExist Is Nothing) Or (dtTableExist.Rows.Count = 0) Then
            'isExist = 0
        Else 'The records is already exist, need to update DELFg=0
            ' isExist = 1
            sqlStr = "UPDATE SONY_ASC_TAX_REPORT SET DELFG=1  "
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
                sqlStr = "Insert into SONY_ASC_TAX_REPORT ("
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


                sqlStr = sqlStr & "REGION, "
                sqlStr = sqlStr & "SC_NAME, "
                sqlStr = sqlStr & "ASC_CODE, "
                sqlStr = sqlStr & "JOB_NO, "
                sqlStr = sqlStr & "CUSTOMER_ID, "
                sqlStr = sqlStr & "CUSTOMER_NAME, "
                sqlStr = sqlStr & "MODEL_CODE, "
                sqlStr = sqlStr & "MODEL_NAME, "
                sqlStr = sqlStr & "COLLECTED_BY, "
                sqlStr = sqlStr & "PAYMENT_MODE, "
                sqlStr = sqlStr & "INVOICE_NO, "
                sqlStr = sqlStr & "COLLECT_DATE, "
                sqlStr = sqlStr & "PART_FEE, "
                sqlStr = sqlStr & "LABOUR_FEE, "
                sqlStr = sqlStr & "INSPECTION_FEE, "
                sqlStr = sqlStr & "HANDLING_FEE, "
                sqlStr = sqlStr & "TRANSPORT_FEE, "
                sqlStr = sqlStr & "HOMESERVICE_FEE, "
                sqlStr = sqlStr & "LONGDISTANCE_FEE, "
                sqlStr = sqlStr & "TRAVELALLOWANCE_FEE, "
                sqlStr = sqlStr & "DA_FEE, "
                sqlStr = sqlStr & "INSTALLATION_FEE, "
                sqlStr = sqlStr & "ECALL_CHARGE, "
                sqlStr = sqlStr & "DEMO_CHARGE, "
                sqlStr = sqlStr & "PART_DISCOUNT, "
                sqlStr = sqlStr & "LABOUR_DISCOUNT, "
                sqlStr = sqlStr & "INSPECTION_DISCOUNT, "
                sqlStr = sqlStr & "HANDLING_DISCOUNT, "
                sqlStr = sqlStr & "HOMESERVICE_DISCOUNT, "
                sqlStr = sqlStr & "TRANSPORT_DISCOUNT, "
                sqlStr = sqlStr & "LONGDISTANCE_DISCOUNT, "
                sqlStr = sqlStr & "TRAVELALLOWANCE_DISCOUNT, "
                sqlStr = sqlStr & "DA_DISCOUNT, "
                sqlStr = sqlStr & "INSTALLATION_DISCOUNT, "
                sqlStr = sqlStr & "DEMO_DISCOUNT, "
                sqlStr = sqlStr & "ECALL_DISCOUNT, "
                sqlStr = sqlStr & "TOTAL_ACTUAL_PART_FEE, "
                sqlStr = sqlStr & "TOTAL_ACTUAL_OTHER_FEE, "
                sqlStr = sqlStr & "TOTAL_PART_FEE_DISCOUNT_BY_SC, "
                sqlStr = sqlStr & "TOTAL_PART_FEE_DISCOUNT_BY_SONY, "
                sqlStr = sqlStr & "TOTAL_OTHER_FEE_DISCOUNT_BY_SC, "
                sqlStr = sqlStr & "TOTAL_OTHER_FEE_DISCOUNT_BY_SONY, "
                sqlStr = sqlStr & "NET_PART_FEE, "
                sqlStr = sqlStr & "NET_OTHER_FEE, "
                sqlStr = sqlStr & "SERVICE_TAX, "
                sqlStr = sqlStr & "ECESS_SERVICE_TAX, "
                sqlStr = sqlStr & "ED_CESS_TAX, "
                sqlStr = sqlStr & "VAT_TAX, "
                sqlStr = sqlStr & "VAT_TAX_RATE, "
                sqlStr = sqlStr & "ADD_VAT_TAX, "
                sqlStr = sqlStr & "ADD_VAT_TAX_RATE, "
                sqlStr = sqlStr & "SURCHARGE_TAX, "
                sqlStr = sqlStr & "SURCHARGE_TAX_RATE, "
                sqlStr = sqlStr & "CESS_TAX, "
                sqlStr = sqlStr & "CESS_TAX_RATE, "
                sqlStr = sqlStr & "NET_PAYABLE, "
                sqlStr = sqlStr & "SWACHH_BHARAT_CESS, "
                sqlStr = sqlStr & "KRISHI_KALYAN_CESS, "



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


                sqlStr = sqlStr & "@REGION, "
                sqlStr = sqlStr & "@SC_NAME, "
                sqlStr = sqlStr & "@ASC_CODE, "
                sqlStr = sqlStr & "@JOB_NO, "
                sqlStr = sqlStr & "@CUSTOMER_ID, "
                sqlStr = sqlStr & "@CUSTOMER_NAME, "
                sqlStr = sqlStr & "@MODEL_CODE, "
                sqlStr = sqlStr & "@MODEL_NAME, "
                sqlStr = sqlStr & "@COLLECTED_BY, "
                sqlStr = sqlStr & "@PAYMENT_MODE, "
                sqlStr = sqlStr & "@INVOICE_NO, "
                sqlStr = sqlStr & "@COLLECT_DATE, "
                sqlStr = sqlStr & "@PART_FEE, "
                sqlStr = sqlStr & "@LABOUR_FEE, "
                sqlStr = sqlStr & "@INSPECTION_FEE, "
                sqlStr = sqlStr & "@HANDLING_FEE, "
                sqlStr = sqlStr & "@TRANSPORT_FEE, "
                sqlStr = sqlStr & "@HOMESERVICE_FEE, "
                sqlStr = sqlStr & "@LONGDISTANCE_FEE, "
                sqlStr = sqlStr & "@TRAVELALLOWANCE_FEE, "
                sqlStr = sqlStr & "@DA_FEE, "
                sqlStr = sqlStr & "@INSTALLATION_FEE, "
                sqlStr = sqlStr & "@ECALL_CHARGE, "
                sqlStr = sqlStr & "@DEMO_CHARGE, "
                sqlStr = sqlStr & "@PART_DISCOUNT, "
                sqlStr = sqlStr & "@LABOUR_DISCOUNT, "
                sqlStr = sqlStr & "@INSPECTION_DISCOUNT, "
                sqlStr = sqlStr & "@HANDLING_DISCOUNT, "
                sqlStr = sqlStr & "@HOMESERVICE_DISCOUNT, "
                sqlStr = sqlStr & "@TRANSPORT_DISCOUNT, "
                sqlStr = sqlStr & "@LONGDISTANCE_DISCOUNT, "
                sqlStr = sqlStr & "@TRAVELALLOWANCE_DISCOUNT, "
                sqlStr = sqlStr & "@DA_DISCOUNT, "
                sqlStr = sqlStr & "@INSTALLATION_DISCOUNT, "
                sqlStr = sqlStr & "@DEMO_DISCOUNT, "
                sqlStr = sqlStr & "@ECALL_DISCOUNT, "
                sqlStr = sqlStr & "@TOTAL_ACTUAL_PART_FEE, "
                sqlStr = sqlStr & "@TOTAL_ACTUAL_OTHER_FEE, "
                sqlStr = sqlStr & "@TOTAL_PART_FEE_DISCOUNT_BY_SC, "
                sqlStr = sqlStr & "@TOTAL_PART_FEE_DISCOUNT_BY_SONY, "
                sqlStr = sqlStr & "@TOTAL_OTHER_FEE_DISCOUNT_BY_SC, "
                sqlStr = sqlStr & "@TOTAL_OTHER_FEE_DISCOUNT_BY_SONY, "
                sqlStr = sqlStr & "@NET_PART_FEE, "
                sqlStr = sqlStr & "@NET_OTHER_FEE, "
                sqlStr = sqlStr & "@SERVICE_TAX, "
                sqlStr = sqlStr & "@ECESS_SERVICE_TAX, "
                sqlStr = sqlStr & "@ED_CESS_TAX, "
                sqlStr = sqlStr & "@VAT_TAX, "
                sqlStr = sqlStr & "@VAT_TAX_RATE, "
                sqlStr = sqlStr & "@ADD_VAT_TAX, "
                sqlStr = sqlStr & "@ADD_VAT_TAX_RATE, "
                sqlStr = sqlStr & "@SURCHARGE_TAX, "
                sqlStr = sqlStr & "@SURCHARGE_TAX_RATE, "
                sqlStr = sqlStr & "@CESS_TAX, "
                sqlStr = sqlStr & "@CESS_TAX_RATE, "
                sqlStr = sqlStr & "@NET_PAYABLE, "
                sqlStr = sqlStr & "@SWACHH_BHARAT_CESS, "
                sqlStr = sqlStr & "@KRISHI_KALYAN_CESS, "



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

                dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@REGION", csvData(i)(0)))
                dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@SC_NAME", csvData(i)(1)))
                dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@ASC_CODE", csvData(i)(2)))
                dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@JOB_NO", csvData(i)(3)))
                dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@CUSTOMER_ID", csvData(i)(4)))
                dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@CUSTOMER_NAME", csvData(i)(5)))
                dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@MODEL_CODE", csvData(i)(6)))
                dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@MODEL_NAME", csvData(i)(7)))
                dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@COLLECTED_BY", csvData(i)(8)))
                dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@PAYMENT_MODE", csvData(i)(9)))
                dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@INVOICE_NO", csvData(i)(10)))
                dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@COLLECT_DATE", csvData(i)(11)))
                dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@PART_FEE", csvData(i)(12)))
                dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@LABOUR_FEE", csvData(i)(13)))
                dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@INSPECTION_FEE", csvData(i)(14)))
                dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@HANDLING_FEE", csvData(i)(15)))
                dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@TRANSPORT_FEE", csvData(i)(16)))
                dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@HOMESERVICE_FEE", csvData(i)(17)))
                dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@LONGDISTANCE_FEE", csvData(i)(18)))
                dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@TRAVELALLOWANCE_FEE", csvData(i)(19)))
                dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@DA_FEE", csvData(i)(20)))
                dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@INSTALLATION_FEE", csvData(i)(21)))
                dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@ECALL_CHARGE", csvData(i)(22)))
                dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@DEMO_CHARGE", csvData(i)(23)))
                dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@PART_DISCOUNT", csvData(i)(24)))
                dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@LABOUR_DISCOUNT", csvData(i)(25)))
                dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@INSPECTION_DISCOUNT", csvData(i)(26)))
                dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@HANDLING_DISCOUNT", csvData(i)(27)))
                dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@HOMESERVICE_DISCOUNT", csvData(i)(28)))
                dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@TRANSPORT_DISCOUNT", csvData(i)(29)))
                dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@LONGDISTANCE_DISCOUNT", csvData(i)(30)))
                dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@TRAVELALLOWANCE_DISCOUNT", csvData(i)(31)))
                dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@DA_DISCOUNT", csvData(i)(32)))
                dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@INSTALLATION_DISCOUNT", csvData(i)(33)))
                dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@DEMO_DISCOUNT", csvData(i)(34)))
                dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@ECALL_DISCOUNT", csvData(i)(35)))
                dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@TOTAL_ACTUAL_PART_FEE", csvData(i)(36)))
                dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@TOTAL_ACTUAL_OTHER_FEE", csvData(i)(37)))
                dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@TOTAL_PART_FEE_DISCOUNT_BY_SC", csvData(i)(38)))
                dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@TOTAL_PART_FEE_DISCOUNT_BY_SONY", csvData(i)(39)))
                dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@TOTAL_OTHER_FEE_DISCOUNT_BY_SC", csvData(i)(40)))
                dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@TOTAL_OTHER_FEE_DISCOUNT_BY_SONY", csvData(i)(41)))
                dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@NET_PART_FEE", csvData(i)(42)))
                dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@NET_OTHER_FEE", csvData(i)(43)))
                dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@SERVICE_TAX", csvData(i)(44)))
                dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@ECESS_SERVICE_TAX", csvData(i)(45)))
                dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@ED_CESS_TAX", csvData(i)(46)))
                dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@VAT_TAX", csvData(i)(47)))
                dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@VAT_TAX_RATE", csvData(i)(48)))
                dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@ADD_VAT_TAX", csvData(i)(49)))
                dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@ADD_VAT_TAX_RATE", csvData(i)(50)))
                dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@SURCHARGE_TAX", csvData(i)(51)))
                dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@SURCHARGE_TAX_RATE", csvData(i)(52)))
                dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@CESS_TAX", csvData(i)(53)))
                dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@CESS_TAX_RATE", csvData(i)(54)))
                dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@NET_PAYABLE", csvData(i)(55)))
                dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@SWACHH_BHARAT_CESS", csvData(i)(56)))
                dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@KRISHI_KALYAN_CESS", csvData(i)(57)))


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


    Public Function SelectAscTaxReport(ByVal queryParams As SonyAscTaxReportModel) As DataTable
        Log4NetControl.ComInfoLogWrite(Log4NetControl.UserID)
        Dim dbConn As DBUtility = New DBUtility()
        Dim dt As DataTable = New DataTable()
        Dim sqlStr As String = "SELECT "
        sqlStr = sqlStr & "REGION as 'Region'," ',SC_CODE as 'SC Code',SC_NAME as 'SC Name',VENDOR_NAME as 'Vendor Name',PO_NO as 'PO_NO',PART_NO as 'Part NO',PART_LOCATION_NAME as 'Part Location Name',PART_ENGLISH_NAME as 'Part English Name',ORDER_NUMBER as 'Order Number',QUANTITY_ON_THE_WAY as 'Quantity On The Way',MODEL_NO as 'Model No',RECEIPT_TYPE as 'Receipt Type',ORDER_DATE as 'Order Date',ORDER_STATUS as 'Order Status',ORDER_STATUS_DATE as 'Order Status Date',RECEIPT_NO as 'Receipt No',APPROVAL_NUMBER as 'Approval Number',MARK as 'Mark',ETD as 'ETD',ORDER_STATUS_2 as 'Order Status',REQUEST_BY as 'Request By',PRICE as 'Price',REQUESTTYPE as 'RequestType',RPOCREMARK as 'RpocRemark',SRPCPONO as 'SrpcPoNo',NPC_SELLING_PRICE as 'NPC_Selling_Price' "
        sqlStr = sqlStr & "SC_NAME as 'SC Name',"
        sqlStr = sqlStr & "ASC_CODE as 'Asc Code',"
        sqlStr = sqlStr & "JOB_NO as 'Job No',"
        sqlStr = sqlStr & "CUSTOMER_ID as 'Customer Id',"
        sqlStr = sqlStr & "CUSTOMER_NAME as 'Customer Name',"
        sqlStr = sqlStr & "MODEL_CODE as 'Model Code',"
        sqlStr = sqlStr & "MODEL_NAME as 'Model Name',"
        sqlStr = sqlStr & "COLLECTED_BY as 'Collected By',"
        sqlStr = sqlStr & "PAYMENT_MODE as 'Payment Mode',"
        sqlStr = sqlStr & "INVOICE_NO as 'Invoice No',"
        sqlStr = sqlStr & "COLLECT_DATE as 'Collect Date',"
        sqlStr = sqlStr & "PART_FEE as 'Part Fee',"
        sqlStr = sqlStr & "LABOUR_FEE as 'Labour Fee',"
        sqlStr = sqlStr & "INSPECTION_FEE as 'Inspection Fee',"
        sqlStr = sqlStr & "HANDLING_FEE as 'Handling Fee',"
        sqlStr = sqlStr & "TRANSPORT_FEE as 'Transport Fee',"
        sqlStr = sqlStr & "HOMESERVICE_FEE as 'Homeservice Fee',"
        sqlStr = sqlStr & "LONGDISTANCE_FEE as 'Longdistance Fee',"
        sqlStr = sqlStr & "TRAVELALLOWANCE_FEE as 'Travelallowance Fee',"
        sqlStr = sqlStr & "DA_FEE as 'DA Fee',"
        sqlStr = sqlStr & "INSTALLATION_FEE as 'Installation Fee',"
        sqlStr = sqlStr & "ECALL_CHARGE as 'Ecall charge',"
        sqlStr = sqlStr & "DEMO_CHARGE as 'Demo Charge',"
        sqlStr = sqlStr & "PART_DISCOUNT as 'Part Discount',"
        sqlStr = sqlStr & "LABOUR_DISCOUNT as 'Labour Discount',"
        sqlStr = sqlStr & "INSPECTION_DISCOUNT as 'Inspection Discount',"
        sqlStr = sqlStr & "HANDLING_DISCOUNT as 'Handling Discount',"
        sqlStr = sqlStr & "HOMESERVICE_DISCOUNT as 'Homeservice Discount',"
        sqlStr = sqlStr & "TRANSPORT_DISCOUNT as 'Transport Discount',"
        sqlStr = sqlStr & "LONGDISTANCE_DISCOUNT as 'Longdistance Discount',"
        sqlStr = sqlStr & "TRAVELALLOWANCE_DISCOUNT as 'Travelallowance Discount',"
        sqlStr = sqlStr & "DA_DISCOUNT as 'DA Discount',"
        sqlStr = sqlStr & "INSTALLATION_DISCOUNT as 'Installation Discount',"
        sqlStr = sqlStr & "DEMO_DISCOUNT as 'Demo Discount',"
        sqlStr = sqlStr & "ECALL_DISCOUNT as 'ECall Discount',"
        sqlStr = sqlStr & "TOTAL_ACTUAL_PART_FEE as 'Total Actual Part Fee',"
        sqlStr = sqlStr & "TOTAL_ACTUAL_OTHER_FEE as 'Total Actual Other Fee',"
        sqlStr = sqlStr & "TOTAL_PART_FEE_DISCOUNT_BY_SC as 'Total Part Fee Discount By SC',"
        sqlStr = sqlStr & "TOTAL_PART_FEE_DISCOUNT_BY_SONY as 'TOTAL_PART_FEE_DISCOUNT_BY_SONY',"
        sqlStr = sqlStr & "TOTAL_OTHER_FEE_DISCOUNT_BY_SC as 'TOTAL_OTHER_FEE_DISCOUNT_BY_SC',"
        sqlStr = sqlStr & "TOTAL_OTHER_FEE_DISCOUNT_BY_SONY as 'TOTAL_OTHER_FEE_DISCOUNT_BY_SONY',"
        sqlStr = sqlStr & "NET_PART_FEE as 'NET_PART_FEE',"
        sqlStr = sqlStr & "NET_OTHER_FEE as 'NET_OTHER_FEE',"
        sqlStr = sqlStr & "SERVICE_TAX as 'SERVICE_TAX',"
        sqlStr = sqlStr & "ECESS_SERVICE_TAX as 'ECESS_SERVICE_TAX',"
        sqlStr = sqlStr & "ED_CESS_TAX as 'ED_CESS_TAX',"
        sqlStr = sqlStr & "VAT_TAX as 'VAT_TAX',"
        sqlStr = sqlStr & "VAT_TAX_RATE as 'VAT_TAX_RATE',"
        sqlStr = sqlStr & "ADD_VAT_TAX as 'ADD_VAT_TAX',"
        sqlStr = sqlStr & "ADD_VAT_TAX_RATE as 'ADD_VAT_TAX_RATE',"
        sqlStr = sqlStr & "SURCHARGE_TAX as 'SURCHARGE_TAX',"
        sqlStr = sqlStr & "SURCHARGE_TAX_RATE as 'SURCHARGE_TAX_RATE',"
        sqlStr = sqlStr & "CESS_TAX as 'CESS_TAX',"
        sqlStr = sqlStr & "CESS_TAX_RATE as 'CESS_TAX_RATE',"
        sqlStr = sqlStr & "NET_PAYABLE as 'NET_PAYABLE',"
        sqlStr = sqlStr & "SWACHH_BHARAT_CESS as 'SWACHH_BHARAT_CESS',"
        sqlStr = sqlStr & "KRISHI_KALYAN_CESS as 'KRISHI_KALYAN_CESS' "


        sqlStr = sqlStr & "FROM "
        sqlStr = sqlStr & "SONY_ASC_TAX_REPORT "
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
