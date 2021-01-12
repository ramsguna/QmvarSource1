Imports System
Imports System.Data
Imports System.Web.Security
Imports System.Collections.Generic
Imports Ganges33.Ganges33.dao
Imports Ganges33.Ganges33.model
Imports Ganges33.Ganges33.logic


Public Class SonyDailyDeliveredControl

    Public Function AddModifyDailyDelivered(ByVal csvData()() As String, queryParams As SonyDailyDeliveredModel) As Boolean
        Log4NetControl.ComInfoLogWrite(Log4NetControl.UserID)
        'Mandatory Column 39 from CSV
        Dim flag As Boolean = True
        If csvData(0).Length < 44 Then
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
        sqlStr = "SELECT TOP 1 COUNTRY FROM SONY_DAILY_DELIVERED "
        sqlStr = sqlStr & " WHERE DELFG = 0 AND SRC_FILE_NAME='" & queryParams.SRC_FILE_NAME & "'"
        'sqlStr = sqlStr & " AND SHIP_TO_BRANCH_CODE='" & queryParams.ShipToBranchCode & "'"
        dtSawDiscountExist = dbConn.GetDataSet(sqlStr)
        'if exist then need to update delfg=1 then insert the record as new
        If (dtSawDiscountExist Is Nothing) Or (dtSawDiscountExist.Rows.Count = 0) Then
            'isExist = 0
        Else 'The records is already exist, need to update DELFg=0
            ' isExist = 1
            sqlStr = "UPDATE SONY_DAILY_DELIVERED SET DELFG=1  "
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
                sqlStr = "Insert into SONY_DAILY_DELIVERED ("
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
                sqlStr = sqlStr & "ASC_LEVEL, "
                sqlStr = sqlStr & "JOB_ID, "
                sqlStr = sqlStr & "CUSTOMER_GROUP, "
                sqlStr = sqlStr & "MANUFACTURE, "
                sqlStr = sqlStr & "WARRANTY_TYPE, "
                sqlStr = sqlStr & "WARRANTY_CATEGORY, "
                sqlStr = sqlStr & "SERVICE_TYPE, "
                sqlStr = sqlStr & "PRODUCT_CATEGORY_NAME, "
                sqlStr = sqlStr & "PRODUCT_SUB_CATEGORY_NAME, "
                sqlStr = sqlStr & "SET_MODEL, "
                sqlStr = sqlStr & "MODEL_NAME, "
                sqlStr = sqlStr & "SERIAL_NO, "
                sqlStr = sqlStr & "DEALER_NAME, "
                sqlStr = sqlStr & "PURCHASED_DATE, "
                sqlStr = sqlStr & "RESERVATION_CREATE_DATE, "
                sqlStr = sqlStr & "APPOINTMENT_DATE, "
                sqlStr = sqlStr & "BOOKED_IN__DATE, "
                sqlStr = sqlStr & "REPAIR_COMPLETED__DATE, "
                sqlStr = sqlStr & "REPAIR_STATUS, "
                sqlStr = sqlStr & "COLLECT_DATE, "
                sqlStr = sqlStr & "RECEPTIONIST, "
                sqlStr = sqlStr & "TECHNICIAN, "
                sqlStr = sqlStr & "ONSITE_PEOPLE, "
                sqlStr = sqlStr & "DISPATCHED_BY, "
                sqlStr = sqlStr & "LOT_NO, "
                sqlStr = sqlStr & "WARRANTY_NO, "
                sqlStr = sqlStr & "REPAIR_LEVEL, "
                sqlStr = sqlStr & "TOTAL_AMOUNT_OF_ACCOUNT_PAYABLE, "
                sqlStr = sqlStr & "ACCOUNT_PAYABLE_BY_CUSTOMER, "
                sqlStr = sqlStr & "SONY_NEEDS_TO_PAY, "
                sqlStr = sqlStr & "ACCOUNT_PAYABLE_BY_ASC, "
                sqlStr = sqlStr & "CUSTOMER_NAME, "
                sqlStr = sqlStr & "CONTACT_NO, "
                sqlStr = sqlStr & "MOBILE_NO, "
                sqlStr = sqlStr & "CITY, "
                sqlStr = sqlStr & "STATE, "
                sqlStr = sqlStr & "POSTAL_CODE, "
                sqlStr = sqlStr & "CONVERTTOJOB_IN_MAPP, "
                sqlStr = sqlStr & "COMPLETED_IN_MAPP, "
                sqlStr = sqlStr & "DELIVER_IN_MAPP_IN, "




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
                sqlStr = sqlStr & "@ASC_LEVEL, "
                sqlStr = sqlStr & "@JOB_ID, "
                sqlStr = sqlStr & "@CUSTOMER_GROUP, "
                sqlStr = sqlStr & "@MANUFACTURE, "
                sqlStr = sqlStr & "@WARRANTY_TYPE, "
                sqlStr = sqlStr & "@WARRANTY_CATEGORY, "
                sqlStr = sqlStr & "@SERVICE_TYPE, "
                sqlStr = sqlStr & "@PRODUCT_CATEGORY_NAME, "
                sqlStr = sqlStr & "@PRODUCT_SUB_CATEGORY_NAME, "
                sqlStr = sqlStr & "@SET_MODEL, "
                sqlStr = sqlStr & "@MODEL_NAME, "
                sqlStr = sqlStr & "@SERIAL_NO, "
                sqlStr = sqlStr & "@DEALER_NAME, "
                sqlStr = sqlStr & "@PURCHASED_DATE, "
                sqlStr = sqlStr & "@RESERVATION_CREATE_DATE, "
                sqlStr = sqlStr & "@APPOINTMENT_DATE, "
                sqlStr = sqlStr & "@BOOKED_IN__DATE, "
                sqlStr = sqlStr & "@REPAIR_COMPLETED__DATE, "
                sqlStr = sqlStr & "@REPAIR_STATUS, "
                sqlStr = sqlStr & "@COLLECT_DATE, "
                sqlStr = sqlStr & "@RECEPTIONIST, "
                sqlStr = sqlStr & "@TECHNICIAN, "
                sqlStr = sqlStr & "@ONSITE_PEOPLE, "
                sqlStr = sqlStr & "@DISPATCHED_BY, "
                sqlStr = sqlStr & "@LOT_NO, "
                sqlStr = sqlStr & "@WARRANTY_NO, "
                sqlStr = sqlStr & "@REPAIR_LEVEL, "
                sqlStr = sqlStr & "@TOTAL_AMOUNT_OF_ACCOUNT_PAYABLE, "
                sqlStr = sqlStr & "@ACCOUNT_PAYABLE_BY_CUSTOMER, "
                sqlStr = sqlStr & "@SONY_NEEDS_TO_PAY, "
                sqlStr = sqlStr & "@ACCOUNT_PAYABLE_BY_ASC, "
                sqlStr = sqlStr & "@CUSTOMER_NAME, "
                sqlStr = sqlStr & "@CONTACT_NO, "
                sqlStr = sqlStr & "@MOBILE_NO, "
                sqlStr = sqlStr & "@CITY, "
                sqlStr = sqlStr & "@STATE, "
                sqlStr = sqlStr & "@POSTAL_CODE, "
                sqlStr = sqlStr & "@CONVERTTOJOB_IN_MAPP, "
                sqlStr = sqlStr & "@COMPLETED_IN_MAPP, "
                sqlStr = sqlStr & "@DELIVER_IN_MAPP_IN, "


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
                dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@ASC_LEVEL", csvData(i)(4)))
                dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@JOB_ID", csvData(i)(5)))
                dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@CUSTOMER_GROUP", csvData(i)(6)))
                dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@MANUFACTURE", csvData(i)(7)))
                dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@WARRANTY_TYPE", csvData(i)(8)))
                dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@WARRANTY_CATEGORY", csvData(i)(9)))
                dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@SERVICE_TYPE", csvData(i)(10)))
                dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@PRODUCT_CATEGORY_NAME", csvData(i)(11)))
                dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@PRODUCT_SUB_CATEGORY_NAME", csvData(i)(12)))
                dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@SET_MODEL", csvData(i)(13)))
                dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@MODEL_NAME", csvData(i)(14)))
                dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@SERIAL_NO", csvData(i)(15)))
                dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@DEALER_NAME", csvData(i)(16)))
                dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@PURCHASED_DATE", csvData(i)(17)))
                dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@RESERVATION_CREATE_DATE", csvData(i)(18)))
                dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@APPOINTMENT_DATE", csvData(i)(19)))
                dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@BOOKED_IN__DATE", csvData(i)(20)))
                dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@REPAIR_COMPLETED__DATE", csvData(i)(21)))
                dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@REPAIR_STATUS", csvData(i)(22)))
                dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@COLLECT_DATE", csvData(i)(23)))
                dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@RECEPTIONIST", csvData(i)(24)))
                dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@TECHNICIAN", csvData(i)(25)))
                dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@ONSITE_PEOPLE", csvData(i)(26)))
                dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@DISPATCHED_BY", csvData(i)(27)))
                dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@LOT_NO", csvData(i)(28)))
                dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@WARRANTY_NO", csvData(i)(29)))
                dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@REPAIR_LEVEL", csvData(i)(30)))
                dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@TOTAL_AMOUNT_OF_ACCOUNT_PAYABLE", csvData(i)(31)))
                dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@ACCOUNT_PAYABLE_BY_CUSTOMER", csvData(i)(32)))
                dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@SONY_NEEDS_TO_PAY", csvData(i)(33)))
                dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@ACCOUNT_PAYABLE_BY_ASC", csvData(i)(34)))
                dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@CUSTOMER_NAME", csvData(i)(35)))
                dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@CONTACT_NO", csvData(i)(36)))
                dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@MOBILE_NO", csvData(i)(37)))
                dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@CITY", csvData(i)(38)))
                dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@STATE", csvData(i)(39)))
                dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@POSTAL_CODE", csvData(i)(40)))
                dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@CONVERTTOJOB_IN_MAPP", csvData(i)(41)))
                dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@COMPLETED_IN_MAPP", csvData(i)(42)))
                dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@DELIVER_IN_MAPP_IN", csvData(i)(43)))


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


    Public Function SelectDailyDelivered(ByVal queryParams As SonyDailyDeliveredModel) As DataTable
        Log4NetControl.ComInfoLogWrite(Log4NetControl.UserID)
        Dim dbConn As DBUtility = New DBUtility()
        Dim dt As DataTable = New DataTable()
        Dim sqlStr As String = "SELECT "

        sqlStr = sqlStr & "COUNTRY as 'Country', "
        sqlStr = sqlStr & "REGION as 'Region', "
        sqlStr = sqlStr & "ASC_CODE as 'ASC Code', "
        sqlStr = sqlStr & "ASC_NAME as 'ASC Name', "
        sqlStr = sqlStr & "ASC_LEVEL as 'ASC Level', "
        sqlStr = sqlStr & "JOB_ID as 'Job ID', "
        sqlStr = sqlStr & "CUSTOMER_GROUP as 'Customer Group', "
        sqlStr = sqlStr & "MANUFACTURE as 'Manufacture', "
        sqlStr = sqlStr & "WARRANTY_TYPE as 'Warranty Type', "
        sqlStr = sqlStr & "WARRANTY_CATEGORY as 'Warranty Category', "
        sqlStr = sqlStr & "SERVICE_TYPE as 'Service Type', "
        sqlStr = sqlStr & "PRODUCT_CATEGORY_NAME as 'Product Category Name',"
        sqlStr = sqlStr & "PRODUCT_SUB_CATEGORY_NAME as 'Product Sub Category Name',"
        sqlStr = sqlStr & "SET_MODEL as 'Set Model', "
        sqlStr = sqlStr & "MODEL_NAME as 'Model Name', "
        sqlStr = sqlStr & "SERIAL_NO as 'Serial No', "
        sqlStr = sqlStr & "DEALER_NAME as 'Dealer Name', "
        sqlStr = sqlStr & "PURCHASED_DATE as 'Purchased Date', "
        sqlStr = sqlStr & "RESERVATION_CREATE_DATE as 'Reservation Create Date', "
        sqlStr = sqlStr & "APPOINTMENT_DATE as 'Appointment Date', "
        sqlStr = sqlStr & "BOOKED_IN__DATE as 'Booked In Date', "
        sqlStr = sqlStr & "REPAIR_COMPLETED__DATE as 'Repair Completed Date', "
        sqlStr = sqlStr & "REPAIR_STATUS as 'Repair Status', "
        sqlStr = sqlStr & "COLLECT_DATE as 'Collect Date', "
        sqlStr = sqlStr & "RECEPTIONIST as 'Receptionist', "
        sqlStr = sqlStr & "TECHNICIAN as 'Technician', "
        sqlStr = sqlStr & "ONSITE_PEOPLE as 'Onsite People', "
        sqlStr = sqlStr & "DISPATCHED_BY as 'Dispatched By', "
        sqlStr = sqlStr & "LOT_NO as 'Lot No', "
        sqlStr = sqlStr & "WARRANTY_NO as 'Warranty No', "
        sqlStr = sqlStr & "REPAIR_LEVEL as 'Repair Level', "
        sqlStr = sqlStr & "TOTAL_AMOUNT_OF_ACCOUNT_PAYABLE as 'Total Amount of Account Payable', "
        sqlStr = sqlStr & "ACCOUNT_PAYABLE_BY_CUSTOMER as'Account Payable By Customer', "
        sqlStr = sqlStr & "SONY_NEEDS_TO_PAY as 'Sony Needs To Pay', "
        sqlStr = sqlStr & "ACCOUNT_PAYABLE_BY_ASC as 'Account Payable By ASC', "
        sqlStr = sqlStr & "CUSTOMER_NAME as 'Customer Name', "
        sqlStr = sqlStr & "CONTACT_NO as 'Contact No', "
        sqlStr = sqlStr & "MOBILE_NO as 'Mobile No', "
        sqlStr = sqlStr & "CITY as 'City', "
        sqlStr = sqlStr & "STATE as 'State', "
        sqlStr = sqlStr & "POSTAL_CODE as 'Postal Code', "
        sqlStr = sqlStr & "CONVERTTOJOB_IN_MAPP as 'ConvertToJob_in_MAPP', "
        sqlStr = sqlStr & "COMPLETED_IN_MAPP as 'Completed_in_MAPP', "
        sqlStr = sqlStr & "DELIVER_IN_MAPP_IN as 'Deliver_in_Mapp In' "



        sqlStr = sqlStr & "FROM "
        sqlStr = sqlStr & "SONY_DAILY_DELIVERED "
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


