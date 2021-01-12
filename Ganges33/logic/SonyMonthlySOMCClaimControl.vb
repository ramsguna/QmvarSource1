Imports System
Imports System.Data
Imports System.Web.Security
Imports System.Collections.Generic
Imports Ganges33.Ganges33.dao
Imports Ganges33.Ganges33.model
Imports Ganges33.Ganges33.logic


Public Class SonyMonthlySOMCClaimControl

    Public Function AddModifyMonthlySOMCClaim(ByVal csvData()() As String, queryParams As SonyMonthlySOMCClaimModel) As Boolean

        Log4NetControl.ComInfoLogWrite(Log4NetControl.UserID)
        'Mandatory Column 39 from CSV
        Dim flag As Boolean = True
        If csvData(0).Length < 52 Then
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
        '1st check PARTS_NO
        sqlStr = "SELECT TOP 1 COUNTRY FROM SONY_MONTHLY_SOMC_CLAIM "
        sqlStr = sqlStr & " WHERE DELFG = 0 AND SRC_FILE_NAME='" & queryParams.SRC_FILE_NAME & "'"
        'sqlStr = sqlStr & " AND SHIP_TO_BRANCH_CODE='" & queryParams.ShipToBranchCode & "'"
        dtSawDiscountExist = dbConn.GetDataSet(sqlStr)
        'if exist then need to update delfg=1 then insert the record as new
        If (dtSawDiscountExist Is Nothing) Or (dtSawDiscountExist.Rows.Count = 0) Then
            'isExist = 0
        Else 'The records is already exist, need to update DELFg=0
            ' isExist = 1
            sqlStr = "UPDATE SONY_MONTHLY_SOMC_CLAIM SET DELFG=1  "
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
                sqlStr = "Insert into SONY_MONTHLY_SOMC_CLAIM ("
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
                sqlStr = sqlStr & "MODEL_NO, "
                sqlStr = sqlStr & "MODEL_NAME, "
                sqlStr = sqlStr & "SS_IN, "
                sqlStr = sqlStr & "SS_OUT, "
                sqlStr = sqlStr & "RECEIVED_DATE, "
                sqlStr = sqlStr & "JOB_COLLECT_DATE, "
                sqlStr = sqlStr & "ASC_APPLY_DATE, "
                sqlStr = sqlStr & "SERVICE_TYPE, "
                sqlStr = sqlStr & "PURCHASED_DATE, "
                sqlStr = sqlStr & "WARRANTY_TYPE, "
                sqlStr = sqlStr & "WARRANTY_CATEGORY, "
                sqlStr = sqlStr & "TECHNICIAN, "
                sqlStr = sqlStr & "BEGIN_REPAIR_DATE, "
                sqlStr = sqlStr & "REPAIR_COMPLETED_DATE, "
                sqlStr = sqlStr & "REPAIR_RETURNED_DATE, "
                sqlStr = sqlStr & "PART_CODE, "
                sqlStr = sqlStr & "PART_DESC, "
                sqlStr = sqlStr & "REPAIR_QTY, "
                sqlStr = sqlStr & "PART_UNIT_PRICE, "
                sqlStr = sqlStr & "SYMPTOM_CODE, "
                sqlStr = sqlStr & "SYMPTOM_DESCRIPTION, "
                sqlStr = sqlStr & "DEFECT_CODE, "
                sqlStr = sqlStr & "SECTION_CODE, "
                sqlStr = sqlStr & "REPAIR_CODE, "
                sqlStr = sqlStr & "FEE_TYPE, "
                sqlStr = sqlStr & "CLAIM_AMOUNT, "
                sqlStr = sqlStr & "TAX_AMOUNT, "
                sqlStr = sqlStr & "CLAIM_STATUS, "
                sqlStr = sqlStr & "CLAIM_STATUS_DATE, "
                sqlStr = sqlStr & "RECEIVER, "
                sqlStr = sqlStr & "QC_OWNER, "
                sqlStr = sqlStr & "COLLECTED_PERSON, "
                sqlStr = sqlStr & "D6D_CODE, "
                sqlStr = sqlStr & "D4D_CODE, "
                sqlStr = sqlStr & "PART_COST, "
                sqlStr = sqlStr & "REPAIR_LEVEL, "
                sqlStr = sqlStr & "CLAIM_STATUS_GROUP, "
                sqlStr = sqlStr & "KPI_FLAG, "
                sqlStr = sqlStr & "VENDOR_CODE, "
                sqlStr = sqlStr & "RR90, "
                sqlStr = sqlStr & "REPAIR_TAT, "
                sqlStr = sqlStr & "REPAIR_ACTION, "
                sqlStr = sqlStr & "CUSTOMER_COMPLAINT, "







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
                sqlStr = sqlStr & "@MODEL_NO, "
                sqlStr = sqlStr & "@MODEL_NAME, "
                sqlStr = sqlStr & "@SS_IN, "
                sqlStr = sqlStr & "@SS_OUT, "
                sqlStr = sqlStr & "@RECEIVED_DATE, "
                sqlStr = sqlStr & "@JOB_COLLECT_DATE, "
                sqlStr = sqlStr & "@ASC_APPLY_DATE, "
                sqlStr = sqlStr & "@SERVICE_TYPE, "
                sqlStr = sqlStr & "@PURCHASED_DATE, "
                sqlStr = sqlStr & "@WARRANTY_TYPE, "
                sqlStr = sqlStr & "@WARRANTY_CATEGORY, "
                sqlStr = sqlStr & "@TECHNICIAN, "
                sqlStr = sqlStr & "@BEGIN_REPAIR_DATE, "
                sqlStr = sqlStr & "@REPAIR_COMPLETED_DATE, "
                sqlStr = sqlStr & "@REPAIR_RETURNED_DATE, "
                sqlStr = sqlStr & "@PART_CODE, "
                sqlStr = sqlStr & "@PART_DESC, "
                sqlStr = sqlStr & "@REPAIR_QTY, "
                sqlStr = sqlStr & "@PART_UNIT_PRICE, "
                sqlStr = sqlStr & "@SYMPTOM_CODE, "
                sqlStr = sqlStr & "@SYMPTOM_DESCRIPTION, "
                sqlStr = sqlStr & "@DEFECT_CODE, "
                sqlStr = sqlStr & "@SECTION_CODE, "
                sqlStr = sqlStr & "@REPAIR_CODE, "
                sqlStr = sqlStr & "@FEE_TYPE, "
                sqlStr = sqlStr & "@CLAIM_AMOUNT, "
                sqlStr = sqlStr & "@TAX_AMOUNT, "
                sqlStr = sqlStr & "@CLAIM_STATUS, "
                sqlStr = sqlStr & "@CLAIM_STATUS_DATE, "
                sqlStr = sqlStr & "@RECEIVER, "
                sqlStr = sqlStr & "@QC_OWNER, "
                sqlStr = sqlStr & "@COLLECTED_PERSON, "
                sqlStr = sqlStr & "@D6D_CODE, "
                sqlStr = sqlStr & "@D4D_CODE, "
                sqlStr = sqlStr & "@PART_COST, "
                sqlStr = sqlStr & "@REPAIR_LEVEL, "
                sqlStr = sqlStr & "@CLAIM_STATUS_GROUP, "
                sqlStr = sqlStr & "@KPI_FLAG, "
                sqlStr = sqlStr & "@VENDOR_CODE, "
                sqlStr = sqlStr & "@RR90, "
                sqlStr = sqlStr & "@REPAIR_TAT, "
                sqlStr = sqlStr & "@REPAIR_ACTION, "
                sqlStr = sqlStr & "@CUSTOMER_COMPLAINT, "





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
                dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@MODEL_NO", csvData(i)(9)))
                dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@MODEL_NAME", csvData(i)(10)))
                dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@SS_IN", csvData(i)(11)))
                dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@SS_OUT", csvData(i)(12)))
                dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@RECEIVED_DATE", csvData(i)(13)))
                dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@JOB_COLLECT_DATE", csvData(i)(14)))
                dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@ASC_APPLY_DATE", csvData(i)(15)))
                dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@SERVICE_TYPE", csvData(i)(16)))
                dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@PURCHASED_DATE", csvData(i)(17)))
                dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@WARRANTY_TYPE", csvData(i)(18)))
                dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@WARRANTY_CATEGORY", csvData(i)(19)))
                dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@TECHNICIAN", csvData(i)(20)))
                dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@BEGIN_REPAIR_DATE", csvData(i)(21)))
                dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@REPAIR_COMPLETED_DATE", csvData(i)(22)))
                dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@REPAIR_RETURNED_DATE", csvData(i)(23)))
                dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@PART_CODE", csvData(i)(24)))
                dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@PART_DESC", csvData(i)(25)))
                dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@REPAIR_QTY", csvData(i)(26)))
                dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@PART_UNIT_PRICE", csvData(i)(27)))
                dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@SYMPTOM_CODE", csvData(i)(28)))
                dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@SYMPTOM_DESCRIPTION", csvData(i)(29)))
                dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@DEFECT_CODE", csvData(i)(30)))
                dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@SECTION_CODE", csvData(i)(31)))
                dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@REPAIR_CODE", csvData(i)(32)))
                dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@FEE_TYPE", csvData(i)(33)))
                dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@CLAIM_AMOUNT", csvData(i)(34)))
                dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@TAX_AMOUNT", csvData(i)(35)))
                dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@CLAIM_STATUS", csvData(i)(36)))
                dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@CLAIM_STATUS_DATE", csvData(i)(37)))
                dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@RECEIVER", csvData(i)(38)))
                dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@QC_OWNER", csvData(i)(39)))
                dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@COLLECTED_PERSON", csvData(i)(40)))
                dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@D6D_CODE", csvData(i)(41)))
                dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@D4D_CODE", csvData(i)(42)))
                dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@PART_COST", csvData(i)(43)))
                dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@REPAIR_LEVEL", csvData(i)(44)))
                dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@CLAIM_STATUS_GROUP", csvData(i)(45)))
                dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@KPI_FLAG", csvData(i)(46)))
                dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@VENDOR_CODE", csvData(i)(47)))
                dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@RR90", csvData(i)(48)))
                dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@REPAIR_TAT", csvData(i)(49)))
                dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@REPAIR_ACTION", csvData(i)(50)))
                dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@CUSTOMER_COMPLAINT", csvData(i)(51)))




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


    Public Function SelectMonthlySOMCClaim(ByVal queryParams As SonyMonthlySOMCClaimModel) As DataTable
        Log4NetControl.ComInfoLogWrite(Log4NetControl.UserID)
        Dim dbConn As DBUtility = New DBUtility()
        Dim dt As DataTable = New DataTable()

        Dim sqlStr As String = "SELECT "
        sqlStr = sqlStr & "COUNTRY as 'Country', "
        sqlStr = sqlStr & "REGION as 'Region', "
        sqlStr = sqlStr & "REGION_NAME as 'Region_Name', "
        sqlStr = sqlStr & "ASC_CODE as 'ASC Code', "
        sqlStr = sqlStr & "ASC_NAME as 'ASC Name', "
        sqlStr = sqlStr & "JOB_NUMBER as 'Job Number', "
        sqlStr = sqlStr & "SEQ as 'Seq', "
        sqlStr = sqlStr & "PRODUCT_CATEGORY as 'Product Category', "
        sqlStr = sqlStr & "PRODUCT_SUB_CATEGORY as 'Product Sub Category', "
        sqlStr = sqlStr & "MODEL_NO as 'Model NO', "
        sqlStr = sqlStr & "MODEL_NAME as 'Model Name', "
        sqlStr = sqlStr & "SS_IN as 'SS IN', "
        sqlStr = sqlStr & "SS_OUT as 'SS OUT', "
        sqlStr = sqlStr & "RECEIVED_DATE as 'Received Date', "
        sqlStr = sqlStr & "JOB_COLLECT_DATE as 'Job Collect Date', "
        sqlStr = sqlStr & "ASC_APPLY_DATE as 'ASC Apply Date', "
        sqlStr = sqlStr & "SERVICE_TYPE as 'Service Type', "
        sqlStr = sqlStr & "PURCHASED_DATE as 'Purchased Date', "
        sqlStr = sqlStr & "WARRANTY_TYPE as 'Warranty Type', "
        sqlStr = sqlStr & "WARRANTY_CATEGORY as 'Warranty Category', "
        sqlStr = sqlStr & "TECHNICIAN as 'Technician', "
        sqlStr = sqlStr & "BEGIN_REPAIR_DATE as 'Begin Repair Date', "
        sqlStr = sqlStr & "REPAIR_COMPLETED_DATE as 'Repair Completed Date', "
        sqlStr = sqlStr & "REPAIR_RETURNED_DATE as 'Repair Returned Date', "
        sqlStr = sqlStr & "PART_CODE as 'Part Code', "
        sqlStr = sqlStr & "PART_DESC as 'Part Desc', "
        sqlStr = sqlStr & "REPAIR_QTY as 'Repair Qty', "
        sqlStr = sqlStr & "PART_UNIT_PRICE as 'Part Unit Price', "
        sqlStr = sqlStr & "SYMPTOM_CODE as 'Symptom Code', "
        sqlStr = sqlStr & "SYMPTOM_DESCRIPTION as 'Symptom Description', "
        sqlStr = sqlStr & "DEFECT_CODE as 'Defect Code', "
        sqlStr = sqlStr & "SECTION_CODE as 'Section Code', "
        sqlStr = sqlStr & "REPAIR_CODE as 'Repair Code', "
        sqlStr = sqlStr & "FEE_TYPE as 'Fee Type', "
        sqlStr = sqlStr & "CLAIM_AMOUNT as 'Claim Amount', "
        sqlStr = sqlStr & "TAX_AMOUNT as 'Tax Amount', "
        sqlStr = sqlStr & "CLAIM_STATUS as 'Claim Status', "
        sqlStr = sqlStr & "CLAIM_STATUS_DATE as 'Claim Status Date', "
        sqlStr = sqlStr & "RECEIVER as 'Receiver', "
        sqlStr = sqlStr & "QC_OWNER as 'QC Owner', "
        sqlStr = sqlStr & "COLLECTED_PERSON as 'Collected Person', "
        sqlStr = sqlStr & "D6D_CODE as '6D Code', "
        sqlStr = sqlStr & "D4D_CODE as '4D Code', "
        sqlStr = sqlStr & "PART_COST as 'Part Cost', "
        sqlStr = sqlStr & "REPAIR_LEVEL as 'Repair Level', "
        sqlStr = sqlStr & "CLAIM_STATUS_GROUP as 'Claim Status Group', "
        sqlStr = sqlStr & "KPI_FLAG as 'KPI_Flag', "
        sqlStr = sqlStr & "VENDOR_CODE as 'Vendor_Code', "
        sqlStr = sqlStr & "RR90 as 'RR90', "
        sqlStr = sqlStr & "REPAIR_TAT as 'Repair TAT', "
        sqlStr = sqlStr & "REPAIR_ACTION as 'Repair Action/Technician Remarks', "
        sqlStr = sqlStr & "CUSTOMER_COMPLAINT as 'Customer Complaint' "

        sqlStr = sqlStr & "FROM "
        sqlStr = sqlStr & "SONY_MONTHLY_SOMC_CLAIM "
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

