Imports System
Imports System.Data
Imports System.Web.Security
Imports System.Collections.Generic
Imports Ganges33.Ganges33.dao
Imports Ganges33.Ganges33.model
Imports Ganges33.Ganges33.logic


Public Class SonyMonthlyReservationvationControl
    Public Function AddSonyMonthlyReservationvation(ByVal csvData()() As String, queryParams As SonyMonthlyReservationvationModel) As Boolean
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
        Dim dtMonthlyReservationvation As DataTable

        Dim isExist As Integer = 0
        '1st check COUNTRY
        sqlStr = "SELECT TOP 1 COUNTRY FROM SONY_MONTHLY_RESERVATIONVATION "
        sqlStr = sqlStr & " WHERE DELFG = 0 AND SRC_FILE_NAME='" & queryParams.SRC_FILE_NAME & "'"
        'sqlStr = sqlStr & " AND SHIP_TO_BRANCH_CODE='" & queryParams.ShipToBranchCode & "'"
        dtMonthlyReservationvation = dbConn.GetDataSet(sqlStr)
        'if exist then need to update delfg=1 then insert the record as new
        If (dtMonthlyReservationvation Is Nothing) Or (dtMonthlyReservationvation.Rows.Count = 0) Then
            'isExist = 0
        Else 'The records is already exist, need to update DELFg=0
            ' isExist = 1
            sqlStr = "UPDATE SONY_MONTHLY_RESERVATIONVATION SET DELFG=1  "
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
                sqlStr = "Insert into SONY_MONTHLY_RESERVATIONVATION ("
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
                sqlStr = sqlStr & "SOURCE, "
                sqlStr = sqlStr & "CCC_ID, "
                sqlStr = sqlStr & "RESERVATION_ID, "
                sqlStr = sqlStr & "RESERVE_ID, "
                sqlStr = sqlStr & "RESERVE_SUB_ID, "
                sqlStr = sqlStr & "MODEL_CODE, "
                sqlStr = sqlStr & "MODEL_NAME, "
                sqlStr = sqlStr & "SERIAL_NO, "
                sqlStr = sqlStr & "JOB_TYPE, "
                sqlStr = sqlStr & "SERVICE_TYPE, "
                sqlStr = sqlStr & "PURCHASE_DATE, "
                sqlStr = sqlStr & "DEARLER_NAME, "


                sqlStr = sqlStr & "CUSTOMER_COMPLAINT, "
                sqlStr = sqlStr & "REMARKS, "
                sqlStr = sqlStr & "RESERVE_CREATE_DATE, "
                sqlStr = sqlStr & "CUSTOMER_REQDATE, "
                sqlStr = sqlStr & "APPOINTMENT_DATE, "
                sqlStr = sqlStr & "APPOINTMENT_TIME, "
                sqlStr = sqlStr & "ST_TYPE, "
                sqlStr = sqlStr & "RESPONSE_TIME, "
                sqlStr = sqlStr & "CUSTOMER_NAME, "
                sqlStr = sqlStr & "STATUS_NAME, "
                sqlStr = sqlStr & "TECHNICIAN, "

                sqlStr = sqlStr & "ASSIGN_BY, "
                sqlStr = sqlStr & "JOB_NO, "
                sqlStr = sqlStr & "JOB_DATE, "
                sqlStr = sqlStr & "LAST_CONTACT_DATE, "
                sqlStr = sqlStr & "LAST_CONTACT_REMARKS, "
                sqlStr = sqlStr & "POST_CODE, "
                sqlStr = sqlStr & "RESERVATION_UPDATE_DATE, "
                sqlStr = sqlStr & "SUBJECT, "
                sqlStr = sqlStr & "RESERVE_AGE, "
                sqlStr = sqlStr & "APPOINTMENT_AGE, "
                sqlStr = sqlStr & "LINKMAN, "

                sqlStr = sqlStr & "UNIT, "
                sqlStr = sqlStr & "ADDRESS, "
                sqlStr = sqlStr & "PHONE, "
                sqlStr = sqlStr & "MOBILE, "


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
                sqlStr = sqlStr & "@SOURCE, "
                sqlStr = sqlStr & "@CCC_ID, "
                sqlStr = sqlStr & "@RESERVATION_ID, "
                sqlStr = sqlStr & "@RESERVE_ID, "
                sqlStr = sqlStr & "@RESERVE_SUB_ID, "
                sqlStr = sqlStr & "@MODEL_CODE, "
                sqlStr = sqlStr & "@MODEL_NAME, "
                sqlStr = sqlStr & "@SERIAL_NO, "
                sqlStr = sqlStr & "@JOB_TYPE, "
                sqlStr = sqlStr & "@SERVICE_TYPE, "
                sqlStr = sqlStr & "@PURCHASE_DATE, "
                sqlStr = sqlStr & "@DEARLER_NAME, "

                sqlStr = sqlStr & "@CUSTOMER_COMPLAINT, "
                sqlStr = sqlStr & "@REMARKS, "
                sqlStr = sqlStr & "@RESERVE_CREATE_DATE, "
                sqlStr = sqlStr & "@CUSTOMER_REQDATE, "
                sqlStr = sqlStr & "@APPOINTMENT_DATE, "
                sqlStr = sqlStr & "@APPOINTMENT_TIME, "
                sqlStr = sqlStr & "@ST_TYPE, "
                sqlStr = sqlStr & "@RESPONSE_TIME, "
                sqlStr = sqlStr & "@CUSTOMER_NAME, "
                sqlStr = sqlStr & "@STATUS_NAME, "
                sqlStr = sqlStr & "@TECHNICIAN, "
                sqlStr = sqlStr & "@ASSIGN_BY, "
                sqlStr = sqlStr & "@JOB_NO, "
                sqlStr = sqlStr & "@JOB_DATE, "
                sqlStr = sqlStr & "@LAST_CONTACT_DATE, "
                sqlStr = sqlStr & "@LAST_CONTACT_REMARKS, "
                sqlStr = sqlStr & "@POST_CODE, "

                sqlStr = sqlStr & "@RESERVATION_UPDATE_DATE, "
                sqlStr = sqlStr & "@SUBJECT, "
                sqlStr = sqlStr & "@RESERVE_AGE, "
                sqlStr = sqlStr & "@APPOINTMENT_AGE, "
                sqlStr = sqlStr & "@LINKMAN, "
                sqlStr = sqlStr & "@UNIT, "
                sqlStr = sqlStr & "@ADDRESS, "
                sqlStr = sqlStr & "@PHONE, "
                sqlStr = sqlStr & "@MOBILE, "

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
                dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@SOURCE", csvData(i)(5)))
                dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@CCC_ID", csvData(i)(6)))
                dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@RESERVATION_ID", csvData(i)(7)))
                dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@RESERVE_ID", csvData(i)(8)))

                dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@RESERVE_SUB_ID", csvData(i)(9)))
                dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@MODEL_CODE", csvData(i)(10)))
                dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@MODEL_NAME", csvData(i)(11)))
                dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@SERIAL_NO", csvData(i)(12)))
                dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@JOB_TYPE", csvData(i)(13)))
                dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@SERVICE_TYPE", csvData(i)(14)))
                dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@PURCHASE_DATE", csvData(i)(15)))
                dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@DEARLER_NAME", csvData(i)(16)))
                dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@CUSTOMER_COMPLAINT", csvData(i)(17)))

                dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@REMARKS", csvData(i)(18)))
                dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@RESERVE_CREATE_DATE", csvData(i)(19)))
                dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@CUSTOMER_REQDATE", csvData(i)(20)))
                dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@APPOINTMENT_DATE", csvData(i)(21)))
                dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@APPOINTMENT_TIME", csvData(i)(22)))
                dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@ST_TYPE", csvData(i)(23)))
                dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@RESPONSE_TIME", csvData(i)(24)))
                dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@CUSTOMER_NAME", csvData(i)(25)))
                dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@STATUS_NAME", csvData(i)(26)))
                dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@TECHNICIAN", csvData(i)(27)))
                dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@ASSIGN_BY", csvData(i)(28)))
                dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@JOB_NO", csvData(i)(29)))
                dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@JOB_DATE", csvData(i)(30)))
                dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@LAST_CONTACT_DATE", csvData(i)(31)))
                dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@LAST_CONTACT_REMARKS", csvData(i)(32)))
                dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@POST_CODE", csvData(i)(33)))
                dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@RESERVATION_UPDATE_DATE", csvData(i)(34)))

                dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@SUBJECT", csvData(i)(35)))
                dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@RESERVE_AGE", csvData(i)(36)))
                dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@APPOINTMENT_AGE", csvData(i)(37)))
                dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@LINKMAN", csvData(i)(38)))
                dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@UNIT", csvData(i)(39)))
                dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@ADDRESS", csvData(i)(40)))
                dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@PHONE", csvData(i)(41)))
                dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@MOBILE", csvData(i)(42)))

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


    Public Function SelectSonyMonthlyReservationvation(ByVal queryParams As SonyMonthlyReservationvationModel) As DataTable
        Log4NetControl.ComInfoLogWrite(Log4NetControl.UserID)
        Dim dbConn As DBUtility = New DBUtility()
        Dim dt As DataTable = New DataTable()
        Dim sqlStr As String = "SELECT "

        sqlStr = sqlStr & "COUNTRY as 'Country',"
        sqlStr = sqlStr & "REGION as 'Region',"
        sqlStr = sqlStr & "REGION_NAME as 'RegionName',"
        sqlStr = sqlStr & "ASC_CODE as 'AscCode',"
        sqlStr = sqlStr & "ASC_NAME as 'AscName',"
        sqlStr = sqlStr & "SOURCE as 'Source',"
        sqlStr = sqlStr & "CCC_ID as 'CccId',"
        sqlStr = sqlStr & "RESERVATION_ID as 'ReservationId',"
        sqlStr = sqlStr & "RESERVE_ID as 'ReserveId',"
        sqlStr = sqlStr & "RESERVE_SUB_ID as 'ReserveSubId',"
        sqlStr = sqlStr & "MODEL_CODE as 'ModelCode',"
        sqlStr = sqlStr & "MODEL_NAME as 'ModelName',"
        sqlStr = sqlStr & "SERIAL_NO as 'SerialNo',"
        sqlStr = sqlStr & "JOB_TYPE as 'JobTypt',"
        sqlStr = sqlStr & "SERVICE_TYPE as 'ServiceType',"
        sqlStr = sqlStr & "PURCHASE_DATE as 'PurcheseDate',"
        sqlStr = sqlStr & "DEARLER_NAME as 'DeclareName',"
        sqlStr = sqlStr & "CUSTOMER_COMPLAINT as 'CustomerComplaint',"
        sqlStr = sqlStr & "REMARKS as 'Remarks',"
        sqlStr = sqlStr & "RESERVE_CREATE_DATE as 'ReserveCreateDate',"
        sqlStr = sqlStr & "CUSTOMER_REQDATE as 'CustomerReqDate',"
        sqlStr = sqlStr & "APPOINTMENT_DATE as 'AppoinmentDate',"
        sqlStr = sqlStr & "APPOINTMENT_TIME as 'AppoinmentTime', "

        sqlStr = sqlStr & "ST_TYPE as 'StType',"
        sqlStr = sqlStr & "RESPONSE_TIME as 'ResponseTime',"
        sqlStr = sqlStr & "CUSTOMER_NAME as 'CustomerName',"
        sqlStr = sqlStr & "STATUS_NAME as 'StatusName',"
        sqlStr = sqlStr & "TECHNICIAN as 'Technicion',"
        sqlStr = sqlStr & "JOB_NO as 'JobNo',"
        sqlStr = sqlStr & "JOB_DATE as 'Job_Date',"
        sqlStr = sqlStr & "LAST_CONTACT_DATE as 'LastContactDate',"
        sqlStr = sqlStr & "LAST_CONTACT_REMARKS as 'LastContactRemarks',"
        sqlStr = sqlStr & "POST_CODE as 'Post_Code',"
        sqlStr = sqlStr & "RESERVATION_UPDATE_DATE as 'ReservationUpdateDate',"
        sqlStr = sqlStr & "SUBJECT as 'Subject',"
        sqlStr = sqlStr & "RESERVE_AGE as 'ReserveAge',"
        sqlStr = sqlStr & "APPOINTMENT_AGE as 'AppointmentAge',"
        sqlStr = sqlStr & "LINKMAN as 'LinkMan',"
        sqlStr = sqlStr & "ADDRESS as 'Address',"
        sqlStr = sqlStr & "PHONE as 'Phone' ,"
        sqlStr = sqlStr & "MOBILE as 'Mobile' "


        sqlStr = sqlStr & "FROM "
        sqlStr = sqlStr & "SONY_MONTHLY_RESERVATIONVATION "
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
