Imports System
Imports System.Data
Imports System.Web.Security
Imports System.Collections.Generic
Imports Ganges33.Ganges33.dao
Imports Ganges33.Ganges33.model
Imports Ganges33.Ganges33.logic

Public Class SonyDailyClaimControl
    Public Function AddModifyDailyClaim(ByVal csvData()() As String, queryParams As SonyDailyClaimModel) As Boolean
        Log4NetControl.ComInfoLogWrite(Log4NetControl.UserID)
        'Mandatory Column 39 from CSV
        Dim flag As Boolean = True
        If csvData(0).Length < 46 Then
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
        sqlStr = "SELECT TOP 1 COUNTRY FROM SONY_DAILY_CLAIM "
        sqlStr = sqlStr & " WHERE DELFG = 0 AND SRC_FILE_NAME='" & queryParams.SRC_FILE_NAME & "'"
        'sqlStr = sqlStr & " AND SHIP_TO_BRANCH_CODE='" & queryParams.ShipToBranchCode & "'"
        dtSawDiscountExist = dbConn.GetDataSet(sqlStr)
        'if exist then need to update delfg=1 then insert the record as new
        If (dtSawDiscountExist Is Nothing) Or (dtSawDiscountExist.Rows.Count = 0) Then
            'isExist = 0
        Else 'The records is already exist, need to update DELFg=0
            ' isExist = 1
            sqlStr = "UPDATE SONY_DAILY_CLAIM SET DELFG=1  "
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
                sqlStr = "Insert into SONY_DAILY_CLAIM ("
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
                sqlStr = sqlStr & "JOB_NO, "
                sqlStr = sqlStr & "MODEL_NO, "
                sqlStr = sqlStr & "MODEL_NAME, "
                sqlStr = sqlStr & "SERIAL_NO, "
                sqlStr = sqlStr & "WARRANTY_CARD_NO, "
                sqlStr = sqlStr & "INVOICE_NO, "
                sqlStr = sqlStr & "RECEIVED_DATE, "
                sqlStr = sqlStr & "JOBCOLLECT_DATE, "
                sqlStr = sqlStr & "ASCAPPLY_DATE, "
                sqlStr = sqlStr & "JOB_TYPE, "
                sqlStr = sqlStr & "WARRANTY_CATEGORY, "
                sqlStr = sqlStr & "SERVICE_TYPE, "
                sqlStr = sqlStr & "SPONSOR, "
                sqlStr = sqlStr & "FEE_TYPE, "
                sqlStr = sqlStr & "PART_RETURN, "
                sqlStr = sqlStr & "CLAIM_AMOUNT, "
                sqlStr = sqlStr & "TAX_AMOUNT, "
                sqlStr = sqlStr & "CLAIM_STATUS, "
                sqlStr = sqlStr & "CLAIM_STATUS_DATE, "
                sqlStr = sqlStr & "ASC_MARK, "
                sqlStr = sqlStr & "BC_MARK, "
                sqlStr = sqlStr & "HQ_CLAIM_MARK, "
                sqlStr = sqlStr & "SPONSER_MARK, "
                sqlStr = sqlStr & "RECEIVER, "
                sqlStr = sqlStr & "TECHNICIAN, "
                sqlStr = sqlStr & "ONSITE_PERSON, "
                sqlStr = sqlStr & "QC_OWNER, "
                sqlStr = sqlStr & "COLLECTED_PERSON, "
                sqlStr = sqlStr & "D6D_CODE, "
                sqlStr = sqlStr & "PRODUCT_CATEGORY, "
                sqlStr = sqlStr & "D4D_CODE, "
                sqlStr = sqlStr & "PART_COST, "
                sqlStr = sqlStr & "REPAIR_LEVEL, "
                sqlStr = sqlStr & "REPAIR_CODE, "
                sqlStr = sqlStr & "ST_TYPE, "
                sqlStr = sqlStr & "CLAIM_STATUS_GROUP, "
                sqlStr = sqlStr & "KPI_FLAG, "
                sqlStr = sqlStr & "TRANSFER_REPAIR_NO, "
                sqlStr = sqlStr & "SEND_BY_INTERFACE_FLAG, "
                sqlStr = sqlStr & "SEND_BY_INTERACE_DATE, "
                sqlStr = sqlStr & "VENDOR_CODE, "





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
                sqlStr = sqlStr & "@JOB_NO, "
                sqlStr = sqlStr & "@MODEL_NO, "
                sqlStr = sqlStr & "@MODEL_NAME, "
                sqlStr = sqlStr & "@SERIAL_NO, "
                sqlStr = sqlStr & "@WARRANTY_CARD_NO, "
                sqlStr = sqlStr & "@INVOICE_NO, "
                sqlStr = sqlStr & "@RECEIVED_DATE, "
                sqlStr = sqlStr & "@JOBCOLLECT_DATE, "
                sqlStr = sqlStr & "@ASCAPPLY_DATE, "
                sqlStr = sqlStr & "@JOB_TYPE, "
                sqlStr = sqlStr & "@WARRANTY_CATEGORY, "
                sqlStr = sqlStr & "@SERVICE_TYPE, "
                sqlStr = sqlStr & "@SPONSOR, "
                sqlStr = sqlStr & "@FEE_TYPE, "
                sqlStr = sqlStr & "@PART_RETURN, "
                sqlStr = sqlStr & "@CLAIM_AMOUNT, "
                sqlStr = sqlStr & "@TAX_AMOUNT, "
                sqlStr = sqlStr & "@CLAIM_STATUS, "
                sqlStr = sqlStr & "@CLAIM_STATUS_DATE, "
                sqlStr = sqlStr & "@ASC_MARK, "
                sqlStr = sqlStr & "@BC_MARK, "
                sqlStr = sqlStr & "@HQ_CLAIM_MARK, "
                sqlStr = sqlStr & "@SPONSER_MARK, "
                sqlStr = sqlStr & "@RECEIVER, "
                sqlStr = sqlStr & "@TECHNICIAN, "
                sqlStr = sqlStr & "@ONSITE_PERSON, "
                sqlStr = sqlStr & "@QC_OWNER, "
                sqlStr = sqlStr & "@COLLECTED_PERSON, "
                sqlStr = sqlStr & "@D6D_CODE, "
                sqlStr = sqlStr & "@PRODUCT_CATEGORY, "
                sqlStr = sqlStr & "@D4D_CODE, "
                sqlStr = sqlStr & "@PART_COST, "
                sqlStr = sqlStr & "@REPAIR_LEVEL, "
                sqlStr = sqlStr & "@REPAIR_CODE, "
                sqlStr = sqlStr & "@ST_TYPE, "
                sqlStr = sqlStr & "@CLAIM_STATUS_GROUP, "
                sqlStr = sqlStr & "@KPI_FLAG, "
                sqlStr = sqlStr & "@TRANSFER_REPAIR_NO, "
                sqlStr = sqlStr & "@SEND_BY_INTERFACE_FLAG, "
                sqlStr = sqlStr & "@SEND_BY_INTERACE_DATE, "
                sqlStr = sqlStr & "@VENDOR_CODE, "



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
                dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@JOB_NO", csvData(i)(5)))
                dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@MODEL_NO", csvData(i)(6)))
                dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@MODEL_NAME", csvData(i)(7)))
                dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@SERIAL_NO", csvData(i)(8)))
                dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@WARRANTY_CARD_NO", csvData(i)(9)))
                dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@INVOICE_NO", csvData(i)(10)))
                dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@RECEIVED_DATE", csvData(i)(11)))
                dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@JOBCOLLECT_DATE", csvData(i)(12)))
                dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@ASCAPPLY_DATE", csvData(i)(13)))
                dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@JOB_TYPE", csvData(i)(14)))
                dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@WARRANTY_CATEGORY", csvData(i)(15)))
                dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@SERVICE_TYPE", csvData(i)(16)))
                dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@SPONSOR", csvData(i)(17)))
                dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@FEE_TYPE", csvData(i)(18)))
                dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@PART_RETURN", csvData(i)(19)))
                dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@CLAIM_AMOUNT", csvData(i)(20)))
                dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@TAX_AMOUNT", csvData(i)(21)))
                dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@CLAIM_STATUS", csvData(i)(22)))
                dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@CLAIM_STATUS_DATE", csvData(i)(23)))
                dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@ASC_MARK", csvData(i)(24)))
                dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@BC_MARK", csvData(i)(25)))
                dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@HQ_CLAIM_MARK", csvData(i)(26)))
                dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@SPONSER_MARK", csvData(i)(27)))
                dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@RECEIVER", csvData(i)(28)))
                dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@TECHNICIAN", csvData(i)(29)))
                dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@ONSITE_PERSON", csvData(i)(30)))
                dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@QC_OWNER", csvData(i)(31)))
                dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@COLLECTED_PERSON", csvData(i)(32)))
                dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@D6D_CODE", csvData(i)(33)))
                dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@PRODUCT_CATEGORY", csvData(i)(34)))
                dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@D4D_CODE", csvData(i)(35)))
                dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@PART_COST", csvData(i)(36)))
                dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@REPAIR_LEVEL", csvData(i)(37)))
                dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@REPAIR_CODE", csvData(i)(38)))
                dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@ST_TYPE", csvData(i)(39)))
                dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@CLAIM_STATUS_GROUP", csvData(i)(40)))
                dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@KPI_FLAG", csvData(i)(41)))
                dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@TRANSFER_REPAIR_NO", csvData(i)(42)))
                dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@SEND_BY_INTERFACE_FLAG", csvData(i)(43)))
                dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@SEND_BY_INTERACE_DATE", csvData(i)(44)))
                dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@VENDOR_CODE", csvData(i)(45)))


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


    Public Function SelectDailyClaim(ByVal queryParams As SonyDailyClaimModel) As DataTable
        Log4NetControl.ComInfoLogWrite(Log4NetControl.UserID)
        Dim dbConn As DBUtility = New DBUtility()
        Dim dt As DataTable = New DataTable()

        Dim sqlStr As String = "SELECT "
        sqlStr = sqlStr & "COUNTRY as 'Country',"
        sqlStr = sqlStr & "REGION as 'Region',"
        sqlStr = sqlStr & "REGION_NAME as 'RegionName',"
        sqlStr = sqlStr & "ASC_CODE as 'ASC Code',"
        sqlStr = sqlStr & "ASC_NAME as 'ASC Name',"
        sqlStr = sqlStr & "JOB_NO as 'Job No',"
        sqlStr = sqlStr & "MODEL_NO as 'Model No',"
        sqlStr = sqlStr & "MODEL_NAME as 'Model Name',"
        sqlStr = sqlStr & "SERIAL_NO as 'Serial No',"
        sqlStr = sqlStr & "WARRANTY_CARD_NO as 'Warranty Card No',"
        sqlStr = sqlStr & "INVOICE_NO as 'Invoice No',"
        sqlStr = sqlStr & "RECEIVED_DATE as 'Received Date',"
        sqlStr = sqlStr & "JOBCOLLECT_DATE as 'Job Collect date',"
        sqlStr = sqlStr & "ASCAPPLY_DATE as 'ASC Apply Date',"
        sqlStr = sqlStr & "JOB_TYPE as'Job Type',"
        sqlStr = sqlStr & "WARRANTY_CATEGORY as 'Warranty Category',"
        sqlStr = sqlStr & "SERVICE_TYPE as 'Service Type', "
        sqlStr = sqlStr & "SPONSOR as 'Sponsor', "
        sqlStr = sqlStr & "FEE_TYPE as 'Fee Type', "
        sqlStr = sqlStr & "PART_RETURN as 'Part Return', "
        sqlStr = sqlStr & "CLAIM_AMOUNT as 'Claim Amount', "
        sqlStr = sqlStr & "TAX_AMOUNT as 'Tax Amount', "
        sqlStr = sqlStr & "CLAIM_STATUS as 'Claim Status', "
        sqlStr = sqlStr & "CLAIM_STATUS_DATE as 'Claim Status Date', "
        sqlStr = sqlStr & "ASC_MARK as 'ASC Mark', "
        sqlStr = sqlStr & "BC_MARK as 'BC Mark', "
        sqlStr = sqlStr & "HQ_CLAIM_MARK as 'HQ Claim Mark', "
        sqlStr = sqlStr & "SPONSER_MARK as 'Sponser Mark', "
        sqlStr = sqlStr & "RECEIVER as 'Receiver', "
        sqlStr = sqlStr & "TECHNICIAN as 'Technician', "
        sqlStr = sqlStr & "ONSITE_PERSON as 'Onsite Person', "
        sqlStr = sqlStr & "QC_OWNER as 'QC Owner', "
        sqlStr = sqlStr & "COLLECTED_PERSON as 'Collected Person', "
        sqlStr = sqlStr & "D6D_CODE as '6D Code', "
        sqlStr = sqlStr & "PRODUCT_CATEGORY as 'Product Category', "
        sqlStr = sqlStr & "D4D_CODE as '4D Code', "
        sqlStr = sqlStr & "PART_COST as 'Part Cost', "
        sqlStr = sqlStr & "REPAIR_LEVEL as 'Repair Level', "
        sqlStr = sqlStr & "REPAIR_CODE as 'Repair Code', "
        sqlStr = sqlStr & "ST_TYPE as 'St_type', "
        sqlStr = sqlStr & "CLAIM_STATUS_GROUP as 'ClaimStatusGroup', "
        sqlStr = sqlStr & "KPI_FLAG as 'KPI Flag', "
        sqlStr = sqlStr & "TRANSFER_REPAIR_NO as 'Transfer Repair No', "
        sqlStr = sqlStr & "SEND_BY_INTERFACE_FLAG as 'Send By Interface Flag', "
        sqlStr = sqlStr & "SEND_BY_INTERACE_DATE as 'Send By Interface Date', "
        sqlStr = sqlStr & "VENDOR_CODE as 'Vendor Code' "



        sqlStr = sqlStr & "FROM "
        sqlStr = sqlStr & "SONY_DAILY_CLAIM "
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
