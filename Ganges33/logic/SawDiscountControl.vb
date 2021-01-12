Imports System
Imports System.Data
Imports System.Web.Security
Imports System.Collections.Generic
Imports Ganges33.Ganges33.dao
Imports Ganges33.Ganges33.model
Imports Ganges33.Ganges33.logic

Public Class SawDiscountControl
    Public Function AddModifySawDiscount(ByVal csvData()() As String, queryParams As SawDiscountModel) As Boolean


        Log4NetControl.ComInfoLogWrite(Log4NetControl.UserID)
        'Mandatory Column 39 from CSV
        Dim flag As Boolean = True
        If csvData(0).Length < 40 Then
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
        sqlStr = "SELECT TOP 1 SAW_NO FROM SAW_DISCOUNT "
        sqlStr = sqlStr & " WHERE DELFG = 0 AND SRC_FILE_NAME='" & queryParams.SrcFileName & "'"
        'sqlStr = sqlStr & " AND SHIP_TO_BRANCH_CODE='" & queryParams.ShipToBranchCode & "'"
        dtSawDiscountExist = dbConn.GetDataSet(sqlStr)
        'if exist then need to update delfg=1 then insert the record as new
        If (dtSawDiscountExist Is Nothing) Or (dtSawDiscountExist.Rows.Count = 0) Then
            'isExist = 0
        Else 'The records is already exist, need to update DELFg=0
            ' isExist = 1
            sqlStr = "UPDATE SAW_DISCOUNT SET DELFG=1  "
            sqlStr = sqlStr & "WHERE DELFG=0 "
            sqlStr = sqlStr & "AND SRC_FILE_NAME = @SRC_FILE_NAME"
            'sqlStr = sqlStr & "AND SHIP_TO_BRANCH_CODE = @SHIP_TO_BRANCH_CODE "
            dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@SRC_FILE_NAME", queryParams.SrcFileName))
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
                sqlStr = "Insert into SAW_DISCOUNT ("
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
                sqlStr = sqlStr & "COMPANY, "
                sqlStr = sqlStr & "COUNTRY, "
                sqlStr = sqlStr & "SAW_NO, "
                sqlStr = sqlStr & "OBJECT_ID, "
                sqlStr = sqlStr & "MODEL_CODE, "
                sqlStr = sqlStr & "SERIAL_NO, "
                sqlStr = sqlStr & "IMEI, "
                sqlStr = sqlStr & "GRMS_REQ_NO, "
                sqlStr = sqlStr & "REQ_CATEGORY, "
                sqlStr = sqlStr & "REQ_CATEGORY_DESC, "
                sqlStr = sqlStr & "REQ_TYPE, "
                sqlStr = sqlStr & "REQ_TYPE_DESC, "
                sqlStr = sqlStr & "REQ_DATE, "
                sqlStr = sqlStr & "STATUS, "
                sqlStr = sqlStr & "STATUS_DESC, "
                sqlStr = sqlStr & "ASC_CODE, "
                sqlStr = sqlStr & "REQUISTER, "
                sqlStr = sqlStr & "ZSAW_CURR, "
                sqlStr = sqlStr & "REQ_VALUR, "
                sqlStr = sqlStr & "REQ_AMT, "
                sqlStr = sqlStr & "REQ_COMMENT, "
                sqlStr = sqlStr & "CONF_VALUE, "
                sqlStr = sqlStr & "CONF_AMT, "
                sqlStr = sqlStr & "CONF_COMMENT, "
                sqlStr = sqlStr & "CONTRACTOR, "
                sqlStr = sqlStr & "EXCH_TYPE, "
                sqlStr = sqlStr & "EXCH_TYPE_DESC, "
                sqlStr = sqlStr & "EXCH_REASON, "
                sqlStr = sqlStr & "EXCH_REASON_DESC, "
                sqlStr = sqlStr & "EXCH_SUB_REASON, "
                sqlStr = sqlStr & "EXCH_SUB_REASON_DESC, "
                sqlStr = sqlStr & "INVOICE_AMT, "
                sqlStr = sqlStr & "EST_REPAIR_AMT, "
                sqlStr = sqlStr & "PART_NO, "
                sqlStr = sqlStr & "PART_DESC, "
                sqlStr = sqlStr & "PO_NO, "
                sqlStr = sqlStr & "EMPLOYEE, "
                sqlStr = sqlStr & "PRE_EMPLOYEE, "
                sqlStr = sqlStr & "CONF_USER, "
                sqlStr = sqlStr & "CONF_DATE, "
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
                sqlStr = sqlStr & "@COMPANY, "
                sqlStr = sqlStr & "@COUNTRY, "
                sqlStr = sqlStr & "@SAW_NO, "
                sqlStr = sqlStr & "@OBJECT_ID, "
                sqlStr = sqlStr & "@MODEL_CODE, "
                sqlStr = sqlStr & "@SERIAL_NO, "
                sqlStr = sqlStr & "@IMEI, "
                sqlStr = sqlStr & "@GRMS_REQ_NO, "
                sqlStr = sqlStr & "@REQ_CATEGORY, "
                sqlStr = sqlStr & "@REQ_CATEGORY_DESC, "
                sqlStr = sqlStr & "@REQ_TYPE, "
                sqlStr = sqlStr & "@REQ_TYPE_DESC, "
                sqlStr = sqlStr & "@REQ_DATE, "
                sqlStr = sqlStr & "@STATUS, "
                sqlStr = sqlStr & "@STATUS_DESC, "
                sqlStr = sqlStr & "@ASC_CODE, "
                sqlStr = sqlStr & "@REQUISTER, "
                sqlStr = sqlStr & "@ZSAW_CURR, "
                sqlStr = sqlStr & "@REQ_VALUR, "
                sqlStr = sqlStr & "@REQ_AMT, "
                sqlStr = sqlStr & "@REQ_COMMENT, "
                sqlStr = sqlStr & "@CONF_VALUE, "
                sqlStr = sqlStr & "@CONF_AMT, "
                sqlStr = sqlStr & "@CONF_COMMENT, "
                sqlStr = sqlStr & "@CONTRACTOR, "
                sqlStr = sqlStr & "@EXCH_TYPE, "
                sqlStr = sqlStr & "@EXCH_TYPE_DESC, "
                sqlStr = sqlStr & "@EXCH_REASON, "
                sqlStr = sqlStr & "@EXCH_REASON_DESC, "
                sqlStr = sqlStr & "@EXCH_SUB_REASON, "
                sqlStr = sqlStr & "@EXCH_SUB_REASON_DESC, "
                sqlStr = sqlStr & "@INVOICE_AMT, "
                sqlStr = sqlStr & "@EST_REPAIR_AMT, "
                sqlStr = sqlStr & "@PART_NO, "
                sqlStr = sqlStr & "@PART_DESC, "
                sqlStr = sqlStr & "@PO_NO, "
                sqlStr = sqlStr & "@EMPLOYEE, "
                sqlStr = sqlStr & "@PRE_EMPLOYEE, "
                sqlStr = sqlStr & "@CONF_USER, "
                sqlStr = sqlStr & "@CONF_DATE, "
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

                dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@COMPANY", csvData(i)(0)))
                dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@COUNTRY", csvData(i)(1)))
                dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@SAW_NO", csvData(i)(2)))
                dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@OBJECT_ID", csvData(i)(3)))
                dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@MODEL_CODE", csvData(i)(4)))
                dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@SERIAL_NO", csvData(i)(5)))
                dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@IMEI", csvData(i)(6)))
                dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@GRMS_REQ_NO", csvData(i)(7)))
                dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@REQ_CATEGORY", csvData(i)(8)))
                dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@REQ_CATEGORY_DESC", csvData(i)(9)))
                dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@REQ_TYPE", csvData(i)(10)))
                dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@REQ_TYPE_DESC", csvData(i)(11)))
                dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@REQ_DATE", csvData(i)(12)))
                dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@STATUS", csvData(i)(13)))
                dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@STATUS_DESC", csvData(i)(14)))
                dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@ASC_CODE", csvData(i)(15)))
                dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@REQUISTER", csvData(i)(16)))
                dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@ZSAW_CURR", csvData(i)(17)))
                dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@REQ_VALUR", csvData(i)(18)))
                dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@REQ_AMT", csvData(i)(19)))
                dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@REQ_COMMENT", csvData(i)(20)))
                dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@CONF_VALUE", csvData(i)(21)))
                dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@CONF_AMT", csvData(i)(22)))
                dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@CONF_COMMENT", csvData(i)(23)))
                dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@CONTRACTOR", csvData(i)(24)))
                dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@EXCH_TYPE", csvData(i)(25)))
                dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@EXCH_TYPE_DESC", csvData(i)(26)))
                dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@EXCH_REASON", csvData(i)(27)))
                dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@EXCH_REASON_DESC", csvData(i)(28)))
                dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@EXCH_SUB_REASON", csvData(i)(29)))
                dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@EXCH_SUB_REASON_DESC", csvData(i)(30)))
                dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@INVOICE_AMT", csvData(i)(31)))
                dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@EST_REPAIR_AMT", csvData(i)(32)))
                dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@PART_NO", csvData(i)(33)))
                dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@PART_DESC", csvData(i)(34)))
                dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@PO_NO", csvData(i)(35)))
                dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@EMPLOYEE", csvData(i)(36)))
                dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@PRE_EMPLOYEE", csvData(i)(37)))
                dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@CONF_USER", csvData(i)(38)))
                dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@CONF_DATE", csvData(i)(39)))
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


    Public Function SelectSawDiscount(ByVal queryParams As SawDiscountModel) As DataTable
        Log4NetControl.ComInfoLogWrite(Log4NetControl.UserID)
        Dim dbConn As DBUtility = New DBUtility()
        Dim dt As DataTable = New DataTable()
        Dim sqlStr As String = "SELECT "
        'sqlStr = sqlStr & "DELFG,UNQ_NO,UPLOAD_USER,UPLOAD_DATE,BRANCH_CODE,COMPANY,COUNTRY,SAW_NO,OBJECT_ID,MODEL_CODE,SERIAL_NO,IMEI,GRMS_REQ_NO,        REQ_CATEGORY, REQ_CATEGORY_DESC, REQ_TYPE, REQ_TYPE_DESC, REQ_DATE, STATUS, STATUS_DESC, ASC_CODE, REQUISTER, ZSAW_CURR, REQ_VALUR, REQ_AMT, REQ_COMMENT, CONF_VALUE, CONF_AMT, CONF_COMMENT, CONTRACTOR, EXCH_TYPE, EXCH_TYPE_DESC, EXCH_REASON, EXCH_REASON_DESC, EXCH_SUB_REASON, EXCH_SUB_REASON_DESC, INVOICE_AMT, EST_REPAIR_AMT, PART_NO, PART_DESC, PO_NO, EMPLOYEE, PRE_EMPLOYEE, CONF_USER, CONF_DATE, FILE_NAME"
        sqlStr = sqlStr & "COMPANY,COUNTRY,SAW_NO as 'SAW NO',OBJECT_ID as 'OBJECT ID',MODEL_CODE as 'MODEL CODE',SERIAL_NO as 'SERIAL NO',IMEI,GRMS_REQ_NO as 'GRMS REQ NO',REQ_CATEGORY as 'REQ CATEGORY', REQ_CATEGORY_DESC as 'REQ CATEGORY DESC', REQ_TYPE as 'REQ TYPE', REQ_TYPE_DESC as 'REQ TYPE DESC',REPLACE(LEFT(CONVERT(VARCHAR,  REQ_DATE, 101), 10),'/','.') as 'REQ DATE', STATUS, STATUS_DESC as 'STATUS DESC', ASC_CODE as 'ASC CODE', REQUISTER, ZSAW_CURR as 'ZSAW CURR', REQ_VALUR as 'REQ VALUE', REQ_AMT as 'REQ AMT', REQ_COMMENT as 'REQ COMMENT', CONF_VALUE as 'CONF VALUE', CONF_AMT as 'CONF AMT', CONF_COMMENT as 'CONF COMMENT', CONTRACTOR, EXCH_TYPE as 'EXCH TYPE', EXCH_TYPE_DESC as 'EXCH TYPE DESC', EXCH_REASON as 'EXCH REASON', EXCH_REASON_DESC as 'EXCH REASON DESC', EXCH_SUB_REASON as 'EXCH SUB REASON', EXCH_SUB_REASON_DESC as 'EXCH SUB REASON DESC', INVOICE_AMT as 'INVOICE AMT', EST_REPAIR_AMT as 'EST REPAIR AMT', PART_NO as 'PART NO', PART_DESC as 'PART DESC', PO_NO as 'PO NO', EMPLOYEE, PRE_EMPLOYEE as 'PRE EMPLOYEE', CONF_USER,REPLACE(LEFT(CONVERT(VARCHAR, CONF_DATE, 101), 10),'/','.') as 'CONF DATE'  "
        sqlStr = sqlStr & "FROM "
        sqlStr = sqlStr & "SAW_DISCOUNT "
        sqlStr = sqlStr & "WHERE "
        sqlStr = sqlStr & "DELFG=0 "
        If Not String.IsNullOrEmpty(queryParams.SrcFileName) Then
            sqlStr = sqlStr & "AND SRC_FILE_NAME = @SRC_FILE_NAME "
            dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@SRC_FILE_NAME", queryParams.SrcFileName))
        End If

        Dim _DataTable As DataTable = dbConn.GetDataSet(sqlStr)
        dbConn.CloseConnection()
        Return _DataTable
    End Function
End Class
