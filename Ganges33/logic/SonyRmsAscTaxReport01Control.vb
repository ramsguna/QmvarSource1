
Imports System
Imports System.Data
Imports System.Web.Security
Imports System.Collections.Generic
Imports Ganges33.Ganges33.dao
Imports Ganges33.Ganges33.model
Imports Ganges33.Ganges33.logic
Public Class SonyRmsAscTaxReport01Control
    Public Function AddModifyRmsAscTaxReport01(ByVal csvData()() As String, queryParams As SonyRmsAscTaxReport01Model) As Boolean
        Log4NetControl.ComInfoLogWrite(Log4NetControl.UserID)
        'Mandatory Column 34 from CSV
        Dim flag As Boolean = True
        If csvData(0).Length < 34 Then
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
        sqlStr = "SELECT TOP 1 REGION FROM SONY_RMS_ASC_TAX_REPORT_01 "
        sqlStr = sqlStr & " WHERE DELFG = 0 AND SRC_FILE_NAME='" & queryParams.SRC_FILE_NAME & "'"
        'sqlStr = sqlStr & " AND SHIP_TO_BRANCH_CODE='" & queryParams.ShipToBranchCode & "'"
        dtTableExist = dbConn.GetDataSet(sqlStr)
        'if exist then need to update delfg=1 then insert the record as new
        If (dtTableExist Is Nothing) Or (dtTableExist.Rows.Count = 0) Then
            'isExist = 0
        Else 'The records is already exist, need to update DELFg=0
            ' isExist = 1
            sqlStr = "UPDATE SONY_RMS_ASC_TAX_REPORT_01 SET DELFG=1  "
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
                sqlStr = "Insert into SONY_RMS_ASC_TAX_REPORT_01 ("
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
                sqlStr = sqlStr & "STATE, "
                sqlStr = sqlStr & "CITY, "
                sqlStr = sqlStr & "ASC_CODE, "
                sqlStr = sqlStr & "GSTN_ID_OF_ASC, "
                sqlStr = sqlStr & "PLACE_OF_SUPPLY, "
                sqlStr = sqlStr & "ASC1, "
                sqlStr = sqlStr & "JOB_NO, "
                sqlStr = sqlStr & "CUSTOMER_ID, "
                sqlStr = sqlStr & "CUSTOMER_NAME, "
                sqlStr = sqlStr & "GSTN_ID_OF_CUSTOMER, "
                sqlStr = sqlStr & "PRODUCT_TYPE, "
                sqlStr = sqlStr & "MODEL_NAME, "
                sqlStr = sqlStr & "WARRANTY_STATUS, "
                sqlStr = sqlStr & "COLLECTED_BY, "
                sqlStr = sqlStr & "INVOICE_DATE, "
                sqlStr = sqlStr & "INVOICE_NO, "
                sqlStr = sqlStr & "PART_FEE, "
                sqlStr = sqlStr & "LABOUR_FEE, "
                sqlStr = sqlStr & "PART_DISCOUNT, "
                sqlStr = sqlStr & "LABOUR_DISCOUNT, "
                sqlStr = sqlStr & "NET_PART_FEE, "
                sqlStr = sqlStr & "NET_OTHER_FEE, "
                sqlStr = sqlStr & "SGST_TAX, "
                sqlStr = sqlStr & "SGST_VALUE, "
                sqlStr = sqlStr & "CGST_TAX, "
                sqlStr = sqlStr & "CGST_VALUE, "
                sqlStr = sqlStr & "UGST_TAX, "
                sqlStr = sqlStr & "UGST_VALUE, "
                sqlStr = sqlStr & "IGST_TAX, "
                sqlStr = sqlStr & "IGST_VALUE, "
                sqlStr = sqlStr & "KERALA_FLOOD_CESS_TAX, "
                sqlStr = sqlStr & "KERALA_FLOOD_CESS_VALUE, "
                sqlStr = sqlStr & "NET_PAYABLE, "


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
                sqlStr = sqlStr & "@STATE, "
                sqlStr = sqlStr & "@CITY, "
                sqlStr = sqlStr & "@ASC_CODE, "
                sqlStr = sqlStr & "@GSTN_ID_OF_ASC, "
                sqlStr = sqlStr & "@PLACE_OF_SUPPLY, "
                sqlStr = sqlStr & "@ASC1, "
                sqlStr = sqlStr & "@JOB_NO, "
                sqlStr = sqlStr & "@CUSTOMER_ID, "
                sqlStr = sqlStr & "@CUSTOMER_NAME, "
                sqlStr = sqlStr & "@GSTN_ID_OF_CUSTOMER, "
                sqlStr = sqlStr & "@PRODUCT_TYPE, "
                sqlStr = sqlStr & "@MODEL_NAME, "
                sqlStr = sqlStr & "@WARRANTY_STATUS, "
                sqlStr = sqlStr & "@COLLECTED_BY, "
                sqlStr = sqlStr & "@INVOICE_DATE, "
                sqlStr = sqlStr & "@INVOICE_NO, "
                sqlStr = sqlStr & "@PART_FEE, "
                sqlStr = sqlStr & "@LABOUR_FEE, "
                sqlStr = sqlStr & "@PART_DISCOUNT, "
                sqlStr = sqlStr & "@LABOUR_DISCOUNT, "
                sqlStr = sqlStr & "@NET_PART_FEE, "
                sqlStr = sqlStr & "@NET_OTHER_FEE, "
                sqlStr = sqlStr & "@SGST_TAX, "
                sqlStr = sqlStr & "@SGST_VALUE, "
                sqlStr = sqlStr & "@CGST_TAX, "
                sqlStr = sqlStr & "@CGST_VALUE, "
                sqlStr = sqlStr & "@UGST_TAX, "
                sqlStr = sqlStr & "@UGST_VALUE, "
                sqlStr = sqlStr & "@IGST_TAX, "
                sqlStr = sqlStr & "@IGST_VALUE, "
                sqlStr = sqlStr & "@KERALA_FLOOD_CESS_TAX, "
                sqlStr = sqlStr & "@KERALA_FLOOD_CESS_VALUE, "
                sqlStr = sqlStr & "@NET_PAYABLE, "



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
                dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@STATE", csvData(i)(1)))
                dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@CITY", csvData(i)(2)))
                dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@ASC_CODE", csvData(i)(3)))
                dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@GSTN_ID_OF_ASC", csvData(i)(4)))
                dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@PLACE_OF_SUPPLY", csvData(i)(5)))
                dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@ASC1", csvData(i)(6)))
                dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@JOB_NO", csvData(i)(7)))
                dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@CUSTOMER_ID", csvData(i)(8)))
                dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@CUSTOMER_NAME", csvData(i)(9)))
                dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@GSTN_ID_OF_CUSTOMER", csvData(i)(10)))
                dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@PRODUCT_TYPE", csvData(i)(11)))
                dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@MODEL_NAME", csvData(i)(12)))
                dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@WARRANTY_STATUS", csvData(i)(13)))
                dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@COLLECTED_BY", csvData(i)(14)))
                dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@INVOICE_DATE", csvData(i)(15)))
                dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@INVOICE_NO", csvData(i)(16)))
                dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@PART_FEE", csvData(i)(17)))
                dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@LABOUR_FEE", csvData(i)(18)))
                dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@PART_DISCOUNT", csvData(i)(19)))
                dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@LABOUR_DISCOUNT", csvData(i)(20)))
                dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@NET_PART_FEE", csvData(i)(21)))
                dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@NET_OTHER_FEE", csvData(i)(22)))
                dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@SGST_TAX", csvData(i)(23)))
                dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@SGST_VALUE", csvData(i)(24)))
                dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@CGST_TAX", csvData(i)(25)))
                dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@CGST_VALUE", csvData(i)(26)))
                dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@UGST_TAX", csvData(i)(27)))
                dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@UGST_VALUE", csvData(i)(28)))
                dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@IGST_TAX", csvData(i)(29)))
                dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@IGST_VALUE", csvData(i)(30)))
                dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@KERALA_FLOOD_CESS_TAX", csvData(i)(31)))
                dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@KERALA_FLOOD_CESS_VALUE", csvData(i)(32)))
                dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@NET_PAYABLE", csvData(i)(33)))


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


    Public Function SelectRmsAscTaxReport01(ByVal queryParams As SonyRmsAscTaxReport01Model) As DataTable
        Log4NetControl.ComInfoLogWrite(Log4NetControl.UserID)
        Dim dbConn As DBUtility = New DBUtility()
        Dim dt As DataTable = New DataTable()
        Dim sqlStr As String = "SELECT "
        sqlStr = sqlStr & "REGION as 'Region'," ',SC_CODE as 'SC Code',SC_NAME as 'SC Name',VENDOR_NAME as 'Vendor Name',PO_NO as 'PO_NO',PART_NO as 'Part NO',PART_LOCATION_NAME as 'Part Location Name',PART_ENGLISH_NAME as 'Part English Name',ORDER_NUMBER as 'Order Number',QUANTITY_ON_THE_WAY as 'Quantity On The Way',MODEL_NO as 'Model No',RECEIPT_TYPE as 'Receipt Type',ORDER_DATE as 'Order Date',ORDER_STATUS as 'Order Status',ORDER_STATUS_DATE as 'Order Status Date',RECEIPT_NO as 'Receipt No',APPROVAL_NUMBER as 'Approval Number',MARK as 'Mark',ETD as 'ETD',ORDER_STATUS_2 as 'Order Status',REQUEST_BY as 'Request By',PRICE as 'Price',REQUESTTYPE as 'RequestType',RPOCREMARK as 'RpocRemark',SRPCPONO as 'SrpcPoNo',NPC_SELLING_PRICE as 'NPC_Selling_Price' "
        sqlStr = sqlStr & "STATE as 'State',"
        sqlStr = sqlStr & "CITY as 'City',"
        sqlStr = sqlStr & "ASC_CODE as 'ASC Code',"
        sqlStr = sqlStr & "GSTN_ID_OF_ASC as 'GSTN ID Of ASC',"
        sqlStr = sqlStr & "PLACE_OF_SUPPLY as 'Place of supply',"
        sqlStr = sqlStr & "ASC1 as 'ASC',"
        sqlStr = sqlStr & "JOB_NO as 'Job No.',"
        sqlStr = sqlStr & "CUSTOMER_ID as 'Customer ID',"
        sqlStr = sqlStr & "CUSTOMER_NAME as 'Customer Name',"
        sqlStr = sqlStr & "GSTN_ID_OF_CUSTOMER as 'GSTN ID of customer',"
        sqlStr = sqlStr & "PRODUCT_TYPE as 'Product Type',"
        sqlStr = sqlStr & "MODEL_NAME as 'Model Name',"
        sqlStr = sqlStr & "WARRANTY_STATUS as 'Warranty Status',"
        sqlStr = sqlStr & "COLLECTED_BY as 'Collected By',"
        sqlStr = sqlStr & "INVOICE_DATE as 'Invoice Date',"
        sqlStr = sqlStr & "INVOICE_NO as 'Invoice No.',"
        sqlStr = sqlStr & "PART_FEE as 'Part Fee',"
        sqlStr = sqlStr & "LABOUR_FEE as 'Labour Fee',"
        sqlStr = sqlStr & "PART_DISCOUNT as 'Part Discount',"
        sqlStr = sqlStr & "LABOUR_DISCOUNT as 'Labour Discount',"
        sqlStr = sqlStr & "NET_PART_FEE as 'Net Part Fee',"
        sqlStr = sqlStr & "NET_OTHER_FEE as 'Net Other Fee',"
        sqlStr = sqlStr & "SGST_TAX as 'SGST Tax',"
        sqlStr = sqlStr & "SGST_VALUE as 'SGST Value',"
        sqlStr = sqlStr & "CGST_TAX as 'CGST Tax',"
        sqlStr = sqlStr & "CGST_VALUE as 'CGST Value',"
        sqlStr = sqlStr & "UGST_TAX as 'UGST Tax',"
        sqlStr = sqlStr & "UGST_VALUE as 'UGST Value',"
        sqlStr = sqlStr & "IGST_TAX as 'IGST Tax',"
        sqlStr = sqlStr & "IGST_VALUE as 'IGST Value',"
        sqlStr = sqlStr & "KERALA_FLOOD_CESS_TAX as 'Kerala Flood Cess Tax',"
        sqlStr = sqlStr & "KERALA_FLOOD_CESS_VALUE as 'Kerala Flood Cess Value',"
        sqlStr = sqlStr & "NET_PAYABLE as 'Net Payable' "

        sqlStr = sqlStr & "FROM "
        sqlStr = sqlStr & "SONY_RMS_ASC_TAX_REPORT_01 "
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
