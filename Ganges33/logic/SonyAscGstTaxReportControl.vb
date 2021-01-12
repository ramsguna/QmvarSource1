Imports System
Imports System.Data
Imports System.Web.Security
Imports System.Collections.Generic
Imports Ganges33.Ganges33.dao
Imports Ganges33.Ganges33.model
Imports Ganges33.Ganges33.logic

Public Class SonyAscGstTaxReportControl
    Public Function AddModifyAscGstTaxReport(ByVal csvData()() As String, queryParams As SonyAscGstTaxReportModel) As Boolean
        Log4NetControl.ComInfoLogWrite(Log4NetControl.UserID)
        'Mandatory Column 66 from CSV
        Dim flag As Boolean = True
        If csvData(0).Length < 66 Then
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
        sqlStr = "SELECT TOP 1 NO FROM SONY_ASC_GST_TAX_REPORT "
        sqlStr = sqlStr & " WHERE DELFG = 0 AND SRC_FILE_NAME='" & queryParams.SRC_FILE_NAME & "'"
        'sqlStr = sqlStr & " AND SHIP_TO_BRANCH_CODE='" & queryParams.ShipToBranchCode & "'"
        dtTableExist = dbConn.GetDataSet(sqlStr)
        'if exist then need to update delfg=1 then insert the record as new
        If (dtTableExist Is Nothing) Or (dtTableExist.Rows.Count = 0) Then
            'isExist = 0
        Else 'The records is already exist, need to update DELFg=0
            ' isExist = 1
            sqlStr = "UPDATE SONY_ASC_GST_TAX_REPORT SET DELFG=1  "
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
                sqlStr = "Insert into SONY_ASC_GST_TAX_REPORT ("
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


                sqlStr = sqlStr & "NO, "
                sqlStr = sqlStr & "REGION, "
                sqlStr = sqlStr & "SC_NAME, "
                sqlStr = sqlStr & "ASC_GSTIN, "
                sqlStr = sqlStr & "ASC_CODE, "
                sqlStr = sqlStr & "JOB_NO, "
                sqlStr = sqlStr & "CUSTOMER_ID, "
                sqlStr = sqlStr & "CUSTOMER_NAME, "
                sqlStr = sqlStr & "CUSTOMER_GSTIN, "
                sqlStr = sqlStr & "PLACE_OF_SUPPLY, "
                sqlStr = sqlStr & "MODEL_CODE, "
                sqlStr = sqlStr & "MODEL_NAME, "
                sqlStr = sqlStr & "PART_NUMBER, "
                sqlStr = sqlStr & "PART_DISC, "
                sqlStr = sqlStr & "PART_QTY, "
                sqlStr = sqlStr & "HSN_SAC_CODE, "
                sqlStr = sqlStr & "COLLECTED_BY, "
                sqlStr = sqlStr & "PAYMENT_MODE, "
                sqlStr = sqlStr & "INVOICE_NO, "
                sqlStr = sqlStr & "COLLECT_DATE, "
                sqlStr = sqlStr & "PART_FEE, "
                sqlStr = sqlStr & "LABOR_FEE, "
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
                sqlStr = sqlStr & "LABOR_DISCOUNT, "
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
                sqlStr = sqlStr & "NET_PART_LABOR_FEE_TAXABLE_VALUE, "
                sqlStr = sqlStr & "PART_SGST_TAX_RATE, "
                sqlStr = sqlStr & "PART_SGST_TAX, "
                sqlStr = sqlStr & "PART_CGST_TAX_RATE, "
                sqlStr = sqlStr & "PART_CGST_TAX, "
                sqlStr = sqlStr & "PART_IGST_TAX_RATE, "
                sqlStr = sqlStr & "PART_IGST_TAX, "
                sqlStr = sqlStr & "PART_UTGST_TAX_RATE, "
                sqlStr = sqlStr & "PART_UTGST_TAX, "
                sqlStr = sqlStr & "PART_CESS_TAX_RATE, "
                sqlStr = sqlStr & "PART_CESS_TAX, "
                sqlStr = sqlStr & "SERVICE_SGST_TAX_RATE, "
                sqlStr = sqlStr & "SERVICE_SGST_TAX, "
                sqlStr = sqlStr & "SERVICE_CGST_TAX_RATE, "
                sqlStr = sqlStr & "SERVICE_CGST_TAX, "
                sqlStr = sqlStr & "SERVICE_IGST_TAX_RATE, "
                sqlStr = sqlStr & "SERVICE_IGST_TAX, "
                sqlStr = sqlStr & "SERVICE_UTGST_TAX_RATE, "
                sqlStr = sqlStr & "SERVICE_UTGST_TAX, "
                sqlStr = sqlStr & "SERVICE_CESS_TAX_RATE, "
                sqlStr = sqlStr & "SERVICE_CESS_TAX, "
                sqlStr = sqlStr & "NET_PAYABLE_TOTAL_INVOICE_AMOUNT, "



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


                sqlStr = sqlStr & "@NO, "
                sqlStr = sqlStr & "@REGION, "
                sqlStr = sqlStr & "@SC_NAME, "
                sqlStr = sqlStr & "@ASC_GSTIN, "
                sqlStr = sqlStr & "@ASC_CODE, "
                sqlStr = sqlStr & "@JOB_NO, "
                sqlStr = sqlStr & "@CUSTOMER_ID, "
                sqlStr = sqlStr & "@CUSTOMER_NAME, "
                sqlStr = sqlStr & "@CUSTOMER_GSTIN, "
                sqlStr = sqlStr & "@PLACE_OF_SUPPLY, "
                sqlStr = sqlStr & "@MODEL_CODE, "
                sqlStr = sqlStr & "@MODEL_NAME, "
                sqlStr = sqlStr & "@PART_NUMBER, "
                sqlStr = sqlStr & "@PART_DISC, "
                sqlStr = sqlStr & "@PART_QTY, "
                sqlStr = sqlStr & "@HSN_SAC_CODE, "
                sqlStr = sqlStr & "@COLLECTED_BY, "
                sqlStr = sqlStr & "@PAYMENT_MODE, "
                sqlStr = sqlStr & "@INVOICE_NO, "
                sqlStr = sqlStr & "@COLLECT_DATE, "
                sqlStr = sqlStr & "@PART_FEE, "
                sqlStr = sqlStr & "@LABOR_FEE, "
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
                sqlStr = sqlStr & "@LABOR_DISCOUNT, "
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
                sqlStr = sqlStr & "@NET_PART_LABOR_FEE_TAXABLE_VALUE, "
                sqlStr = sqlStr & "@PART_SGST_TAX_RATE, "
                sqlStr = sqlStr & "@PART_SGST_TAX, "
                sqlStr = sqlStr & "@PART_CGST_TAX_RATE, "
                sqlStr = sqlStr & "@PART_CGST_TAX, "
                sqlStr = sqlStr & "@PART_IGST_TAX_RATE, "
                sqlStr = sqlStr & "@PART_IGST_TAX, "
                sqlStr = sqlStr & "@PART_UTGST_TAX_RATE, "
                sqlStr = sqlStr & "@PART_UTGST_TAX, "
                sqlStr = sqlStr & "@PART_CESS_TAX_RATE, "
                sqlStr = sqlStr & "@PART_CESS_TAX, "
                sqlStr = sqlStr & "@SERVICE_SGST_TAX_RATE, "
                sqlStr = sqlStr & "@SERVICE_SGST_TAX, "
                sqlStr = sqlStr & "@SERVICE_CGST_TAX_RATE, "
                sqlStr = sqlStr & "@SERVICE_CGST_TAX, "
                sqlStr = sqlStr & "@SERVICE_IGST_TAX_RATE, "
                sqlStr = sqlStr & "@SERVICE_IGST_TAX, "
                sqlStr = sqlStr & "@SERVICE_UTGST_TAX_RATE, "
                sqlStr = sqlStr & "@SERVICE_UTGST_TAX, "
                sqlStr = sqlStr & "@SERVICE_CESS_TAX_RATE, "
                sqlStr = sqlStr & "@SERVICE_CESS_TAX, "
                sqlStr = sqlStr & "@NET_PAYABLE_TOTAL_INVOICE_AMOUNT, "




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

                dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@NO", csvData(i)(0)))
                dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@REGION", csvData(i)(1)))
                dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@SC_NAME", csvData(i)(2)))
                dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@ASC_GSTIN", csvData(i)(3)))
                dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@ASC_CODE", csvData(i)(4)))
                dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@JOB_NO", csvData(i)(5)))
                dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@CUSTOMER_ID", csvData(i)(6)))
                dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@CUSTOMER_NAME", csvData(i)(7)))
                dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@CUSTOMER_GSTIN", csvData(i)(8)))
                dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@PLACE_OF_SUPPLY", csvData(i)(9)))
                dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@MODEL_CODE", csvData(i)(10)))
                dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@MODEL_NAME", csvData(i)(11)))
                dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@PART_NUMBER", csvData(i)(12)))
                dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@PART_DISC", csvData(i)(13)))
                dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@PART_QTY", csvData(i)(14)))
                dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@HSN_SAC_CODE", csvData(i)(15)))
                dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@COLLECTED_BY", csvData(i)(16)))
                dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@PAYMENT_MODE", csvData(i)(17)))
                dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@INVOICE_NO", csvData(i)(18)))
                dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@COLLECT_DATE", csvData(i)(19)))
                dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@PART_FEE", csvData(i)(20)))
                dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@LABOR_FEE", csvData(i)(21)))
                dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@INSPECTION_FEE", csvData(i)(22)))
                dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@HANDLING_FEE", csvData(i)(23)))
                dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@TRANSPORT_FEE", csvData(i)(24)))
                dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@HOMESERVICE_FEE", csvData(i)(25)))
                dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@LONGDISTANCE_FEE", csvData(i)(26)))
                dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@TRAVELALLOWANCE_FEE", csvData(i)(27)))
                dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@DA_FEE", csvData(i)(28)))
                dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@INSTALLATION_FEE", csvData(i)(29)))
                dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@ECALL_CHARGE", csvData(i)(30)))
                dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@DEMO_CHARGE", csvData(i)(31)))
                dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@PART_DISCOUNT", csvData(i)(32)))
                dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@LABOR_DISCOUNT", csvData(i)(33)))
                dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@INSPECTION_DISCOUNT", csvData(i)(34)))
                dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@HANDLING_DISCOUNT", csvData(i)(35)))
                dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@HOMESERVICE_DISCOUNT", csvData(i)(36)))
                dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@TRANSPORT_DISCOUNT", csvData(i)(37)))
                dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@LONGDISTANCE_DISCOUNT", csvData(i)(38)))
                dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@TRAVELALLOWANCE_DISCOUNT", csvData(i)(39)))
                dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@DA_DISCOUNT", csvData(i)(40)))
                dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@INSTALLATION_DISCOUNT", csvData(i)(41)))
                dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@DEMO_DISCOUNT", csvData(i)(42)))
                dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@ECALL_DISCOUNT", csvData(i)(43)))
                dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@NET_PART_LABOR_FEE_TAXABLE_VALUE", csvData(i)(44)))
                dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@PART_SGST_TAX_RATE", csvData(i)(45)))
                dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@PART_SGST_TAX", csvData(i)(46)))
                dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@PART_CGST_TAX_RATE", csvData(i)(47)))
                dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@PART_CGST_TAX", csvData(i)(48)))
                dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@PART_IGST_TAX_RATE", csvData(i)(49)))
                dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@PART_IGST_TAX", csvData(i)(50)))
                dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@PART_UTGST_TAX_RATE", csvData(i)(51)))
                dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@PART_UTGST_TAX", csvData(i)(52)))
                dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@PART_CESS_TAX_RATE", csvData(i)(53)))
                dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@PART_CESS_TAX", csvData(i)(54)))
                dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@SERVICE_SGST_TAX_RATE", csvData(i)(55)))
                dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@SERVICE_SGST_TAX", csvData(i)(56)))
                dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@SERVICE_CGST_TAX_RATE", csvData(i)(57)))
                dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@SERVICE_CGST_TAX", csvData(i)(58)))
                dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@SERVICE_IGST_TAX_RATE", csvData(i)(59)))
                dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@SERVICE_IGST_TAX", csvData(i)(60)))
                dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@SERVICE_UTGST_TAX_RATE", csvData(i)(61)))
                dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@SERVICE_UTGST_TAX", csvData(i)(62)))
                dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@SERVICE_CESS_TAX_RATE", csvData(i)(63)))
                dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@SERVICE_CESS_TAX", csvData(i)(64)))
                dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@NET_PAYABLE_TOTAL_INVOICE_AMOUNT", csvData(i)(65)))

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


    Public Function SelectAscGstTaxReport(ByVal queryParams As SonyAscGstTaxReportModel) As DataTable
        Log4NetControl.ComInfoLogWrite(Log4NetControl.UserID)
        Dim dbConn As DBUtility = New DBUtility()
        Dim dt As DataTable = New DataTable()
        Dim sqlStr As String = "SELECT "
        sqlStr = sqlStr & "NO as 'No.'," 'SC_CODE as 'SC Code',SC_NAME as 'SC Name',VENDOR_NAME as 'Vendor Name',PO_NO as 'PO_NO',PART_NO as 'Part NO',PART_LOCATION_NAME as 'Part Location Name',PART_ENGLISH_NAME as 'Part English Name',ORDER_NUMBER as 'Order Number',QUANTITY_ON_THE_WAY as 'Quantity On The Way',MODEL_NO as 'Model No',RECEIPT_TYPE as 'Receipt Type',ORDER_DATE as 'Order Date',ORDER_STATUS as 'Order Status',ORDER_STATUS_DATE as 'Order Status Date',RECEIPT_NO as 'Receipt No',APPROVAL_NUMBER as 'Approval Number',MARK as 'Mark',ETD as 'ETD',ORDER_STATUS_2 as 'Order Status',REQUEST_BY as 'Request By',PRICE as 'Price',REQUESTTYPE as 'RequestType',RPOCREMARK as 'RpocRemark',SRPCPONO as 'SrpcPoNo',NPC_SELLING_PRICE as 'NPC_Selling_Price' "
        sqlStr = sqlStr & "REGION as 'Region',"
        sqlStr = sqlStr & "SC_NAME as 'SC Name',"
        sqlStr = sqlStr & "ASC_GSTIN as 'ASC GSTIN',"
        sqlStr = sqlStr & "ASC_CODE as 'Asc Code',"
        sqlStr = sqlStr & "JOB_NO as 'Job No',"
        sqlStr = sqlStr & "CUSTOMER_ID as 'Customer Id',"
        sqlStr = sqlStr & "CUSTOMER_NAME as 'Customer Name',"
        sqlStr = sqlStr & "CUSTOMER_GSTIN as 'Customer GSTIN',"
        sqlStr = sqlStr & "PLACE_OF_SUPPLY as 'Place Of Supply',"
        sqlStr = sqlStr & "MODEL_CODE as 'Model Code',"
        sqlStr = sqlStr & "MODEL_NAME as 'Model Name',"
        sqlStr = sqlStr & "PART_NUMBER as 'Part Number',"
        sqlStr = sqlStr & "PART_DISC as 'Part Disc',"
        sqlStr = sqlStr & "PART_QTY as 'Part Qty',"
        sqlStr = sqlStr & "HSN_SAC_CODE as 'HSN/SAC CODE',"
        sqlStr = sqlStr & "COLLECTED_BY as 'Collected By',"
        sqlStr = sqlStr & "PAYMENT_MODE as 'Payment Mode',"
        sqlStr = sqlStr & "INVOICE_NO as 'Invoice No',"
        sqlStr = sqlStr & "COLLECT_DATE as 'Collect Date',"
        sqlStr = sqlStr & "PART_FEE as 'Part Fee',"
        sqlStr = sqlStr & "LABOR_FEE as 'Labor Fee',"
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
        sqlStr = sqlStr & "LABOR_DISCOUNT as 'Labor Discount',"
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
        sqlStr = sqlStr & "NET_PART_LABOR_FEE_TAXABLE_VALUE as 'Net Part/Labor Fee(Taxable Value)',"
        sqlStr = sqlStr & "PART_SGST_TAX_RATE as 'Part SGST Tax Rate',"
        sqlStr = sqlStr & "PART_SGST_TAX as 'Part SGST Tax',"
        sqlStr = sqlStr & "PART_CGST_TAX_RATE as 'Part CGST Tax Rate',"
        sqlStr = sqlStr & "PART_CGST_TAX as 'Part CGST Tax',"
        sqlStr = sqlStr & "PART_IGST_TAX_RATE as 'Part IGST Tax Rate',"
        sqlStr = sqlStr & "PART_IGST_TAX as 'Part IGST Tax',"
        sqlStr = sqlStr & "PART_UTGST_TAX as 'Part UTGST Tax Rate',"
        sqlStr = sqlStr & "PART_CESS_TAX_RATE as 'Part Cess tax Rate',"
        sqlStr = sqlStr & "PART_CESS_TAX as 'Part Cess Tax',"
        sqlStr = sqlStr & "SERVICE_SGST_TAX_RATE as 'Service SGST Tax Rate',"
        sqlStr = sqlStr & "SERVICE_SGST_TAX as 'Service SGST Tax',"
        sqlStr = sqlStr & "SERVICE_CGST_TAX_RATE as 'Service CGST Tax Rate',"
        sqlStr = sqlStr & "SERVICE_CGST_TAX as 'Service CGST Tax',"
        sqlStr = sqlStr & "SERVICE_IGST_TAX_RATE as 'Service IGST Tax Rate',"
        sqlStr = sqlStr & "SERVICE_IGST_TAX as 'Service IGST Tax',"
        sqlStr = sqlStr & "SERVICE_UTGST_TAX_RATE as 'Service UTGST Tax Rate',"
        sqlStr = sqlStr & "SERVICE_UTGST_TAX as 'Service UTGST Tax',"
        sqlStr = sqlStr & "SERVICE_CESS_TAX_RATE as 'Service Cess Tax rate',"
        sqlStr = sqlStr & "SERVICE_CESS_TAX as 'Service Cess Tax',"
        sqlStr = sqlStr & "NET_PAYABLE_TOTAL_INVOICE_AMOUNT as 'Net Payable (Total Invoice Amount)' "

        sqlStr = sqlStr & "FROM "
        sqlStr = sqlStr & "SONY_ASC_GST_TAX_REPORT "
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
