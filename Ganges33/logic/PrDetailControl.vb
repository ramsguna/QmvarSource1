Imports System
Imports System.Data
Imports System.Web.Security
Imports System.Collections.Generic
Imports Ganges33.Ganges33.dao
Imports Ganges33.Ganges33.model
Imports Ganges33.Ganges33.logic
Public Class PrDetailControl
    Public Function AddModifyPrDetail(ByVal csvData()() As String, queryParams As PrDetailModel) As Boolean
        'Row 0 - Header 1
        '0 Ship-to-Branch-Code
        '1 Ship-to-Branch
        '2 Billing Date
        '3 Invoice No
        '4 Item No
        '5 Delivery No
        '6 P/O No
        '7 P/O Type code
        '8 Part No
        '9 Billing Qty
        '10 Amount
        '11 SGST / UTGST
        '12 CGST
        '13 IGST
        '14 Cess
        '15 Tax
        '16 Core Flag
        '17 Total

        Log4NetControl.ComInfoLogWrite(Log4NetControl.UserID)
        'Mandatory Column 17 from CSV
        Dim flag As Boolean = True
        If csvData(0).Length < 18 Then
            Return False
        End If


        '      Log4NetControl.ComInfoLogWrite(Log4NetControl.UserID)
        Dim DateTimeNow As DateTime = DateTime.Now
        Dim dtNow As DateTime = DateTimeNow.AddMinutes(ConfigurationManager.AppSettings("TimeDiff"))
        Dim dbConn As DBUtility = New DBUtility()
        Dim dt As DataTable = New DataTable()
        dbConn.sqlTrn = dbConn.sqlConn.BeginTransaction()
        dbConn.sqlCmd.Transaction = dbConn.sqlTrn
        '      Dim flag As Boolean = True
        Dim flagAll As Boolean = True
        Dim sqlStr As String = ""
        'Dim HSNCode As String = "" VJ 2019/10/18
        Dim dtUpdateOtherExist As DataTable

        Dim isExist As Integer = 0
        '1st check INVOICE_NO,DELIVERY_NO exist in the table 
        sqlStr = "SELECT TOP 1 INVOICE_NO,DELIVERY_NO FROM PR_DETAIL "
        sqlStr = sqlStr & " WHERE DELFG = 0  AND SRC_FILE_NAME='" & queryParams.SrcFileName & "'"
        ' sqlStr = sqlStr & " AND SHIP_TO_BRANCH_CODE='" & queryParams.ShipToBranchCode & "'"

        'sqlStr = sqlStr & " WHERE DELFG = 0 AND INVOICE_NO='" & csvData(i)(3) & "' and DELIVERY_NO='" & csvData(i)(5) & "'"
        'sqlStr = sqlStr & " and PO_NO='" & csvData(i)(6) & "'"
        'sqlStr = sqlStr & " and PO_TYPE='" & csvData(i)(7) & "'"
        'sqlStr = sqlStr & " and PART_NO='" & csvData(i)(8) & "'"

        dtUpdateOtherExist = dbConn.GetDataSet(sqlStr)
        'if exist then need to update delfg=1 then insert the record as new
        If (dtUpdateOtherExist Is Nothing) Or (dtUpdateOtherExist.Rows.Count = 0) Then
            'isExist = 0
        Else 'The records is already exist, need to update DELFg=0
            ' isExist = 1
            sqlStr = "UPDATE PR_DETAIL SET DELFG=1  "
            sqlStr = sqlStr & "WHERE DELFG=0 "
            sqlStr = sqlStr & "AND SRC_FILE_NAME = @SRC_FILE_NAME"
            dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@SRC_FILE_NAME", queryParams.SrcFileName))

            ''sqlStr = sqlStr & "WHERE DELFG=0 AND "
            ''sqlStr = sqlStr & "INVOICE_NO = @INVOICE_NO AND DELIVERY_NO = @DELIVERY_NO AND PO_NO = @PO_NO AND PO_TYPE = @PO_TYPE AND PART_NO = @PART_NO"
            ''dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@INVOICE_NO", csvData(i)(3)))
            ''dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@DELIVERY_NO", csvData(i)(5)))
            ''dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@PO_NO", csvData(i)(6)))
            ''dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@PO_TYPE", csvData(i)(7)))
            ''dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@PART_NO", csvData(i)(8)))

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

                    'VJ 2019/10/18 Begin
                    'HSNCode = ""
                    'sqlStr = ""
                    'sqlStr = "select top 1 PART_NO,HSN_CODE, "
                    'sqlStr = sqlStr & "Convert(datetime, Replace(RIGHT(SRC_FILE_NAME, LEN(SRC_FILE_NAME) - 4),'.csv','')+'01') 'Import_Date'"
                    'sqlStr = sqlStr & " From hsn_code where delfg=0 and PART_NO=@PART_NO"
                    'sqlStr = sqlStr & " Order by Convert(datetime, Replace(RIGHT(SRC_FILE_NAME, LEN(SRC_FILE_NAME) - 4),'.csv','')+'01') desc"
                    ''dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@PART_NO", csvData(i)(8)))
                    'dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@PART_NO", csvData(i)(8)))
                    'dt = dbConn.GetDataSet(sqlStr)
                    'dbConn.sqlCmd.Parameters.Clear()
                    'sqlStr = ""

                    'If dt.Rows.Count > 0 Then
                    '    HSNCode = dt.Select()(0)(1)
                    'End If
                    'VJ 2019/10/18 End
                    'HSNCode
                    'If isExist = 1 Then
                    sqlStr = "Insert into PR_DETAIL ("
                    sqlStr = sqlStr & "CRTDT, "
                    sqlStr = sqlStr & "CRTCD, "
                    ' sqlStr = sqlStr & "UPDDT, "
                    sqlStr = sqlStr & "UPDCD, "
                    sqlStr = sqlStr & "UPDPG, "
                    sqlStr = sqlStr & "DELFG, "
                    '        sqlStr = sqlStr & "UNQ_NO, "
                    sqlStr = sqlStr & "UPLOAD_USER, "
                    sqlStr = sqlStr & "UPLOAD_DATE, "
                    sqlStr = sqlStr & "SHIP_TO_BRANCH_CODE, "
                    sqlStr = sqlStr & "SHIP_TO_BRANCH, "

                    sqlStr = sqlStr & "BILLING_DATE, "
                    sqlStr = sqlStr & "INVOICE_NO, "
                    sqlStr = sqlStr & "ITEM_NO, "
                    sqlStr = sqlStr & "DELIVERY_NO, "
                    sqlStr = sqlStr & "PO_NO, "
                    sqlStr = sqlStr & "PO_TYPE, "
                    sqlStr = sqlStr & "PART_NO, "
                    sqlStr = sqlStr & "BILLING_QTY, "
                    sqlStr = sqlStr & "AMOUNT, "
                    sqlStr = sqlStr & "SGST_UTGST, "
                    sqlStr = sqlStr & "CGST, "
                    sqlStr = sqlStr & "IGST, "
                    sqlStr = sqlStr & "CESS, "
                    sqlStr = sqlStr & "TAX, "
                    sqlStr = sqlStr & "CORE_FLAG, "
                    sqlStr = sqlStr & "TOTAL, "
                    sqlStr = sqlStr & "FILE_NAME, "
                    sqlStr = sqlStr & "SRC_FILE_NAME "
                    'qlStr = sqlStr & "HSN_CODE " VJ 2019/10/18
                    sqlStr = sqlStr & " ) "
                    sqlStr = sqlStr & " values ( "
                    sqlStr = sqlStr & "@CRTDT, "
                    sqlStr = sqlStr & "@CRTCD, "
                    'sqlStr = sqlStr & "@UPDDT, "
                    sqlStr = sqlStr & "@UPDCD, "
                    sqlStr = sqlStr & "@UPDPG, "
                    sqlStr = sqlStr & "@DELFG, "
                    '           sqlStr = sqlStr & " (select max(UNQ_NO)+1 from PR_DETAIL) , "
                    sqlStr = sqlStr & "@UPLOAD_USER, "
                    sqlStr = sqlStr & "@UPLOAD_DATE, "
                    sqlStr = sqlStr & "@SHIP_TO_BRANCH_CODE, "
                    sqlStr = sqlStr & "@SHIP_TO_BRANCH, "

                    sqlStr = sqlStr & "@BILLING_DATE, "
                    sqlStr = sqlStr & "@INVOICE_NO, "
                    sqlStr = sqlStr & "@ITEM_NO, "
                    sqlStr = sqlStr & "@DELIVERY_NO, "
                    sqlStr = sqlStr & "@PO_NO, "
                    sqlStr = sqlStr & "@PO_TYPE, "
                    sqlStr = sqlStr & "@PART_NO, "
                    sqlStr = sqlStr & "@BILLING_QTY, "
                    sqlStr = sqlStr & "@AMOUNT, "
                    sqlStr = sqlStr & "@SGST_UTGST, "
                    sqlStr = sqlStr & "@CGST, "
                    sqlStr = sqlStr & "@IGST, "
                    sqlStr = sqlStr & "@CESS, "
                    sqlStr = sqlStr & "@TAX, "
                    sqlStr = sqlStr & "@CORE_FLAG, "
                    sqlStr = sqlStr & "@TOTAL, "
                    sqlStr = sqlStr & "@FILE_NAME, "
                    sqlStr = sqlStr & "@SRC_FILE_NAME "
                    'sqlStr = sqlStr & "@HSN_CODE " VJ 2019/10/18
                    sqlStr = sqlStr & " )"
                    dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@CRTDT", dtNow))
                    dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@CRTCD", queryParams.UserId))
                    dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@UPDCD", "")) '?????????????????????????
                    dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@UPDPG", queryParams.UPDPG))
                    dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@DELFG", 0))
                    dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@UPLOAD_USER", queryParams.UploadUser))
                    dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@UPLOAD_DATE", dtNow))
                    dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@SHIP_TO_BRANCH_CODE", csvData(i)(0)))
                    dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@SHIP_TO_BRANCH", csvData(i)(1)))

                    dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@BILLING_DATE", csvData(i)(2)))
                    dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@INVOICE_NO", csvData(i)(3)))
                    dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@ITEM_NO", csvData(i)(4)))
                    dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@DELIVERY_NO", csvData(i)(5)))
                    dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@PO_NO", csvData(i)(6)))
                    dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@PO_TYPE", csvData(i)(7)))
                    dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@PART_NO", csvData(i)(8)))

                    Dim numChk As Integer = 0
                    If Integer.TryParse(Val(csvData(i)(9)), numChk) = False Then
                        flagAll = False
                        Exit For
                    End If
                    dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@BILLING_QTY", numChk))
                    dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@AMOUNT", csvData(i)(10)))
                    dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@SGST_UTGST", csvData(i)(11)))
                    dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@CGST", csvData(i)(12)))
                    dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@IGST", csvData(i)(13)))
                    dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@CESS", csvData(i)(14)))
                    dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@TAX", csvData(i)(15)))
                    dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@CORE_FLAG", csvData(i)(16)))
                    dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@TOTAL", csvData(i)(17)))
                    dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@FILE_NAME", queryParams.FileName))
                    dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@SRC_FILE_NAME", queryParams.SrcFileName))
                    'dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@HSN_CODE", HSNCode)) VJ 2019/10/18
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

    Public Function SelectPrDetail(ByVal queryParams As PrDetailModel) As DataTable
        Log4NetControl.ComInfoLogWrite(Log4NetControl.UserID)
        Dim dbConn As DBUtility = New DBUtility()
        Dim dt As DataTable = New DataTable()
        Dim inputfile As String = ""
        If queryParams.SrcFileName.Contains("DT1DT2") Then
            inputfile = queryParams.SrcFileName.Replace("DT2", "") & "','" & queryParams.SrcFileName.Replace("DT1", "")
        Else
            inputfile = queryParams.SrcFileName
        End If

        Dim sqlStr As String = "WITH cte AS "
        'sqlStr = sqlStr & "DELFG,UNQ_NO,UPLOAD_USER,UPLOAD_DATE,SHIP_TO_BRANCH_CODE,SHIP_TO_BRANCH,BILLING_DATE,INVOICE_NO,ITEM_NO,DELIVERY_NO,PO_NO,PO_TYPE,PART_NO,BILLING_QTY,AMOUNT,SGST_UTGST,CGST,IGST,CESS,TAX,CORE_FLAG,TOTAL,FILE_NAME "
        sqlStr = sqlStr & " ( "
        sqlStr = sqlStr & " SELECT PART_NO,HSN_CODE, "
        sqlStr = sqlStr & " ROW_NUMBER() OVER (PARTITION BY PART_NO "
        sqlStr = sqlStr & " ORDER BY "
        sqlStr = sqlStr & " convert(datetime, Replace(RIGHT(SRC_FILE_NAME, LEN(SRC_FILE_NAME) - 4),'.csv','')+'01') DESC,CRTDT DESC) AS rn " 'VJ 2019/10/18
        sqlStr = sqlStr & " FROM HSN_CODE where DELFG = 0 "
        sqlStr = sqlStr & " ) "
        sqlStr = sqlStr & " SELECT PR.SHIP_TO_BRANCH_CODE as 'Ship-to-Branch-Code',PR.SHIP_TO_BRANCH as 'Ship-to-Branch', "
        sqlStr = sqlStr & " LEFT(CONVERT(VARCHAR, BILLING_DATE, 112), 10) as 'Billing Date', "
        sqlStr = sqlStr & " INVOICE_NO as 'Invoice No',ITEM_NO as 'Item No', "
        sqlStr = sqlStr & " DELIVERY_NO as 'Delivery No',PO_NO as 'P/O No',PO_TYPE as 'P/O Type code', "
        sqlStr = sqlStr & " PR.PART_NO as 'Part No',BILLING_QTY as 'Billing Qty', PR.AMOUNT as 'Amount', "
        sqlStr = sqlStr & " SGST_UTGST as 'SGST / UTGST',CGST as 'CGST',IGST as 'IGST', "
        sqlStr = sqlStr & " PR.CESS as 'Cess',TAX as 'Tax' ,CORE_FLAG as 'Core Flag',TOTAL as 'Total', "
        sqlStr = sqlStr & " CTE.HSN_CODE as 'HSN Code' "
        sqlStr = sqlStr & " FROM PR_DETAIL PR "
        sqlStr = sqlStr & " LEFT JOIN cte on PR.PART_NO = cte.PART_NO and rn = 1 "
        sqlStr = sqlStr & " where PR.DELFG = 0 "
        If Not String.IsNullOrEmpty(queryParams.SrcFileName) Then
            sqlStr = sqlStr & " AND SRC_FILE_NAME IN ('" & inputfile & "')"
            dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@SRC_FILE_NAME", inputfile))
        End If

        Dim _DataTable As DataTable = dbConn.GetDataSet(sqlStr)
        dbConn.CloseConnection()
        Return _DataTable
    End Function

    Public Function UpdateHSNCodeandPRDetail(ByVal csvData()() As String, queryParams As PrDetailModel) As Boolean 'VJ 2019/10/18
        'Row 0 - Header 1
        '0 Ship-to-Branch-Code
        '1 Ship-to-Branch
        '2 Billing Date
        '3 Invoice No
        '4 Item No
        '5 Delivery No
        '6 P/O No
        '7 P/O Type code
        '8 Part No
        '9 Billing Qty
        '10 Amount
        '11 SGST / UTGST
        '12 CGST
        '13 IGST
        '14 Cess
        '15 Tax
        '16 Core Flag
        '17 Total

        Log4NetControl.ComInfoLogWrite(Log4NetControl.UserID)

        Dim _HSNCodeControl As HSNCodeControl = New HSNCodeControl()
        Dim HSNQueryParams As HSNCodeModel = New HSNCodeModel()
        HSNQueryParams.SrcFileName = queryParams.SrcFileName
        HSNQueryParams.UserId = queryParams.UserId
        Dim rowcnt As Int16 = 0
        'Mandatory Column 17 from CSV
        Dim flag As Boolean = True
        If csvData(0).Length < 19 Then
            Return False
        End If


        '      Log4NetControl.ComInfoLogWrite(Log4NetControl.UserID)
        Dim DateTimeNow As DateTime = DateTime.Now
        Dim dtNow As DateTime = DateTimeNow.AddMinutes(ConfigurationManager.AppSettings("TimeDiff"))
        Dim dbConn As DBUtility = New DBUtility()
        Dim dt As DataTable = New DataTable()
        dbConn.sqlTrn = dbConn.sqlConn.BeginTransaction()
        dbConn.sqlCmd.Transaction = dbConn.sqlTrn
        '      Dim flag As Boolean = True
        Dim flagAll As Boolean = True
        Dim sqlStr As String = ""
        Dim dtImpData As DataTable = New DataTable()

        dtImpData.Columns.Add("Part_Num")
        dtImpData.Columns.Add("HSN_Code")
        'dtImpData.Columns.Add("SHIP_TO_BRANCH_CODE")

        Dim isExist As Integer = 0

        For i = 0 To csvData.Length - 1
            If i > 0 Then '0  Header
                Dim drow As DataRow = dtImpData.NewRow()
                If csvData(i)(0) = queryParams.ShipToBranchCode Then
                    drow("Part_Num") = csvData(i)(8)
                    drow("HSN_Code") = csvData(i)(18)
                    dtImpData.Rows.Add(drow)
                End If
            End If
        Next
        rowcnt = dtImpData.Rows.Count

        If dtImpData.Rows.Count > 0 Then


            ' Query the original data table returnung the columns Col1 and Col2, distinct collection
            Dim results = (From row In dtImpData.AsEnumerable()
                           Select Part_Num = row.Field(Of String)("Part_Num"), HSN_Code = row.Field(Of String)("HSN_Code")
               ).Distinct().ToList()



            sqlStr = "UPDATE HSN_CODE SET DELFG=1  "
            sqlStr = sqlStr & "WHERE DELFG=0 "
            sqlStr = sqlStr & "AND SRC_FILE_NAME = @SRC_FILE_NAME AND SHIP_TO_BRANCH_CODE =@SHIP_TO_BRANCH_CODE"
            dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@SRC_FILE_NAME", queryParams.SrcFileName))
            dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@SHIP_TO_BRANCH_CODE", queryParams.ShipToBranchCode))


            ''sqlStr = sqlStr & "WHERE DELFG=0 AND "
            ''sqlStr = sqlStr & "INVOICE_NO = @INVOICE_NO AND DELIVERY_NO = @DELIVERY_NO AND PO_NO = @PO_NO AND PO_TYPE = @PO_TYPE AND PART_NO = @PART_NO"
            ''dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@INVOICE_NO", csvData(i)(3)))
            ''dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@DELIVERY_NO", csvData(i)(5)))
            ''dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@PO_NO", csvData(i)(6)))
            ''dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@PO_TYPE", csvData(i)(7)))
            ''dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@PART_NO", csvData(i)(8)))

            flag = dbConn.ExecSQL(sqlStr)
            dbConn.sqlCmd.Parameters.Clear()
            'If Error occurs then will store the flag as false
            If Not flag Then
                flagAll = False
            End If
            'End If
            'If there is no error
            If flagAll Then


                For Each dataRow In results
                    'If isExist = 1 Then
                    sqlStr = "Insert into HSN_CODE ("
                    sqlStr = sqlStr & "CRTDT, "
                    sqlStr = sqlStr & "CRTCD, "
                    sqlStr = sqlStr & "UPDCD, "
                    sqlStr = sqlStr & "UPDPG, "
                    sqlStr = sqlStr & "DELFG, "
                    sqlStr = sqlStr & "UPLOAD_USER, "
                    sqlStr = sqlStr & "UPLOAD_DATE, "
                    sqlStr = sqlStr & "SHIP_TO_BRANCH_CODE, "
                    sqlStr = sqlStr & "SHIP_TO_BRANCH, "
                    sqlStr = sqlStr & "PART_NO, "
                    sqlStr = sqlStr & "HSN_CODE, "
                    sqlStr = sqlStr & "AMOUNT, "
                    sqlStr = sqlStr & "INTEGRATED_TAX, "
                    sqlStr = sqlStr & "CENTRAL_TAX, "
                    sqlStr = sqlStr & "STATE_TAX, "
                    sqlStr = sqlStr & "CESS, "
                    sqlStr = sqlStr & "HSN_CODE_IMPORT_DATE, "
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
                    sqlStr = sqlStr & "@PART_NO, "
                    sqlStr = sqlStr & "@HSN_CODE, "
                    sqlStr = sqlStr & "@AMOUNT, "
                    sqlStr = sqlStr & "@INTEGRATED_TAX, "
                    sqlStr = sqlStr & "@CENTRAL_TAX, "
                    sqlStr = sqlStr & "@STATE_TAX, "
                    sqlStr = sqlStr & "@CESS, "
                    sqlStr = sqlStr & "@HSN_CODE_IMPORT_DATE, "
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
                    dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@PART_NO", dataRow.Part_Num))
                    dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@HSN_CODE", dataRow.HSN_Code))
                    dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@AMOUNT", String.Empty))
                    dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@INTEGRATED_TAX", String.Empty))
                    dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@CENTRAL_TAX", String.Empty))
                    dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@STATE_TAX", String.Empty))
                    dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@CESS", String.Empty))
                    dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@HSN_CODE_IMPORT_DATE", queryParams.SrcFileName.Replace("hsn-", "").Replace(".csv", "") & "01"))
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
        End If

        If rowcnt = 0 Then

            flag = False

        End If

        If flag = True Then
            'flag = _HSNCodeControl.UpdateHSNCodePRDetails(HSNQueryParams)
            flag = UpdatePrHSNDetail(csvData, queryParams)
        End If

        Return flag
    End Function


    Public Function UpdatePrHSNDetail(csvData()() As String, queryParams As PrDetailModel) As Boolean 'VJ 2019/10/18
        'Row 0 - Header 1
        '0 Ship-to-Branch-Code
        '1 Ship-to-Branch
        '2 Billing Date
        '3 Invoice No
        '4 Item No
        '5 Delivery No
        '6 P/O No
        '7 P/O Type code
        '8 Part No
        '9 Billing Qty
        '10 Amount
        '11 SGST / UTGST
        '12 CGST
        '13 IGST
        '14 Cess
        '15 Tax
        '16 Core Flag
        '17 Total

        Log4NetControl.ComInfoLogWrite(Log4NetControl.UserID)
        'Mandatory Column 17 from CSV
        Dim flag As Boolean = True
        If csvData(0).Length < 18 Then
            Return False
        End If


        '      Log4NetControl.ComInfoLogWrite(Log4NetControl.UserID)
        Dim DateTimeNow As DateTime = DateTime.Now
        Dim dtNow As DateTime = DateTimeNow.AddMinutes(ConfigurationManager.AppSettings("TimeDiff"))
        Dim dbConn As DBUtility = New DBUtility()
        Dim dt As DataTable = New DataTable()
        dbConn.sqlTrn = dbConn.sqlConn.BeginTransaction()
        dbConn.sqlCmd.Transaction = dbConn.sqlTrn
        '      Dim flag As Boolean = True
        Dim flagAll As Boolean = True
        Dim sqlStr As String = ""
        Dim SqlStrPN As String = ""
        Dim HSNCode As String = ""
        Dim updfile As String = queryParams.SrcFileName.Replace("hsn-", "").Replace(".csv", "") & "01"
        Dim dtupdprdhsncode As DateTime = updfile.Substring(4, 2) & "/01/" & updfile.Substring(0, 4)


        Dim dtUpdateOtherExist As DataTable

        Dim isExist As Integer = 0


        'If there is no error
        If flagAll Then
            For i = 0 To csvData.Length - 1
                If i > 0 Then '0  Header
                    If csvData(i)(0) = queryParams.ShipToBranchCode Then
                        HSNCode = ""
                        sqlStr = ""
                        sqlStr = "select top 1 PART_NO,HSN_CODE, "
                        sqlStr = sqlStr & "Convert(datetime, Replace(RIGHT(SRC_FILE_NAME, LEN(SRC_FILE_NAME) - 4),'.csv','')+'01') 'Import_Date'"
                        sqlStr = sqlStr & " From hsn_code where delfg=0 and PART_NO=@PART_NO AND SHIP_TO_BRANCH_CODE=@SHIP_TO_BRANCH_CODE"
                        sqlStr = sqlStr & " Order by Convert(datetime, Replace(RIGHT(SRC_FILE_NAME, LEN(SRC_FILE_NAME) - 4),'.csv','')+'01') desc"
                        dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@PART_NO", csvData(i)(8)))
                        dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@SHIP_TO_BRANCH_CODE", csvData(i)(0)))
                        dt = dbConn.GetDataSet(sqlStr)
                        dbConn.sqlCmd.Parameters.Clear()
                        sqlStr = ""
                        HSNCode = ""

                        If dt.Rows.Count > 0 Then
                            Dim dtprdhsncode As DateTime = Convert.ToString(dt.Select()(0)(2))
                            If dtprdhsncode < dtupdprdhsncode Then
                                HSNCode = csvData(i)(18)
                            Else
                                HSNCode = dt.Select()(0)(1)
                            End If
                        Else
                            HSNCode = csvData(i)(18)
                        End If

                        sqlStr = "Update PR_DETAIL Set HSN_CODE=@HSN_CODE,UPDDT=@UPDDT,UPDCD=@UPDCD  "
                        sqlStr = sqlStr & "Where DELFG=0 And SHIP_TO_BRANCH_CODE = @SHIP_TO_BRANCH_CODE "
                        sqlStr = sqlStr & "And BILLING_DATE = @BILLING_DATE And INVOICE_NO =@INVOICE_NO "
                        sqlStr = sqlStr & "And ITEM_NO = @ITEM_NO And DELIVERY_NO =@DELIVERY_NO "
                        sqlStr = sqlStr & "And PO_NO = @PO_NO And PART_NO =@PART_NO "



                        dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@HSN_CODE", HSNCode))
                        dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@UPDDT", dtNow))
                        dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@UPDCD", queryParams.UserId))
                        dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@SHIP_TO_BRANCH_CODE", csvData(i)(0)))
                        dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@BILLING_DATE", csvData(i)(2)))
                        dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@INVOICE_NO", csvData(i)(3)))
                        dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@ITEM_NO", csvData(i)(4)))
                        dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@DELIVERY_NO", csvData(i)(5)))
                        dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@PO_NO", csvData(i)(6)))
                        dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@PO_TYPE", csvData(i)(7)))
                        dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@PART_NO", csvData(i)(8)))



                        flag = dbConn.ExecSQL(sqlStr)
                        dbConn.sqlCmd.Parameters.Clear()
                        'If Error occurs then will store the flag as false
                        If Not flag Then
                            flagAll = False
                            Exit For
                        End If
                    End If
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
End Class
