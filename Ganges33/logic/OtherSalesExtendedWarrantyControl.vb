Imports System
Imports System.Data
Imports System.Web.Security
Imports System.Collections.Generic
Imports Ganges33.Ganges33.dao
Imports Ganges33.Ganges33.model
Imports Ganges33.Ganges33.logic
Public Class OtherSalesExtendedWarrantyControl
    Public Function AddModifyOtherSalesExtendedWarranty(ByVal csvData()() As String, queryParams As OtherSalesExtendedWarrantyModel) As Boolean

        Log4NetControl.ComInfoLogWrite(Log4NetControl.UserID)
        'Mandatory Column 19 from CSV
        Dim flag As Boolean = True
        If csvData(0).Length < 20 Then
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
        sqlStr = "SELECT TOP 1 PO_NO FROM EXP_WTY "
        sqlStr = sqlStr & " WHERE DELFG = 0 AND SRC_FILE_NAME='" & queryParams.SrcFileName & "'"
        'sqlStr = sqlStr & " AND SHIP_TO_BRANCH_CODE='" & queryParams.ShipToBranchCode & "'"
        dtSawDiscountExist = dbConn.GetDataSet(sqlStr)
        'if exist then need to update delfg=1 then insert the record as new
        If (dtSawDiscountExist Is Nothing) Or (dtSawDiscountExist.Rows.Count = 0) Then
            'isExist = 0
        Else 'The records is already exist, need to update DELFg=0
            ' isExist = 1
            sqlStr = "UPDATE EXP_WTY SET DELFG=1  "
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
            If i > 1 Then '0  Header, 1 Header
                'If isExist = 1 Then
                sqlStr = "Insert into EXP_WTY ("
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

                sqlStr = sqlStr & "PO_NO, "
                sqlStr = sqlStr & "CUSTOMER_NAME, "
                sqlStr = sqlStr & "PACK_NO, "
                sqlStr = sqlStr & "UNIT_PRICE, "
                sqlStr = sqlStr & "RETAIL_PRICE, "
                sqlStr = sqlStr & "SGST_RATE, "
                sqlStr = sqlStr & "SGST_AMOUNT, "
                sqlStr = sqlStr & "CGST_RATE, "
                sqlStr = sqlStr & "CGST_AMOUNT, "
                sqlStr = sqlStr & "IGST_RATE, "
                sqlStr = sqlStr & "IGST_AMOUNT, "
                sqlStr = sqlStr & "CESS_RATE, "
                sqlStr = sqlStr & "CESS_AMOUNT, "
                sqlStr = sqlStr & "TOTAL_TAX, "
                sqlStr = sqlStr & "TOTAL_AMOUNT, "
                sqlStr = sqlStr & "NOTE, "
                sqlStr = sqlStr & "MODEL, "
                sqlStr = sqlStr & "SERIAL, "
                sqlStr = sqlStr & "PACK_DOP, "
                sqlStr = sqlStr & "BP_NO, "

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

                sqlStr = sqlStr & "@PO_NO, "
                sqlStr = sqlStr & "@CUSTOMER_NAME, "
                sqlStr = sqlStr & "@PACK_NO, "
                sqlStr = sqlStr & "@UNIT_PRICE, "
                sqlStr = sqlStr & "@RETAIL_PRICE, "
                sqlStr = sqlStr & "@SGST_RATE, "
                sqlStr = sqlStr & "@SGST_AMOUNT, "
                sqlStr = sqlStr & "@CGST_RATE, "
                sqlStr = sqlStr & "@CGST_AMOUNT, "
                sqlStr = sqlStr & "@IGST_RATE, "
                sqlStr = sqlStr & "@IGST_AMOUNT, "
                sqlStr = sqlStr & "@CESS_RATE, "
                sqlStr = sqlStr & "@CESS_AMOUNT, "
                sqlStr = sqlStr & "@TOTAL_TAX, "
                sqlStr = sqlStr & "@TOTAL_AMOUNT, "
                sqlStr = sqlStr & "@NOTE, "
                sqlStr = sqlStr & "@MODEL, "
                sqlStr = sqlStr & "@SERIAL, "
                sqlStr = sqlStr & "@PACK_DOP, "
                sqlStr = sqlStr & "@BP_NO, "

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

                dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@PO_NO", csvData(i)(0)))
                dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@CUSTOMER_NAME", csvData(i)(1)))
                dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@PACK_NO", csvData(i)(2)))
                dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@UNIT_PRICE", csvData(i)(3)))
                dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@RETAIL_PRICE", csvData(i)(4)))
                dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@SGST_RATE", csvData(i)(5)))
                dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@SGST_AMOUNT", csvData(i)(6)))
                dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@CGST_RATE", csvData(i)(7)))
                dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@CGST_AMOUNT", csvData(i)(8)))
                dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@IGST_RATE", csvData(i)(9)))
                dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@IGST_AMOUNT", csvData(i)(10)))
                dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@CESS_RATE", csvData(i)(11)))
                dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@CESS_AMOUNT", csvData(i)(12)))
                dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@TOTAL_TAX", csvData(i)(13)))
                dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@TOTAL_AMOUNT", csvData(i)(14)))
                dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@NOTE", csvData(i)(15)))
                dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@MODEL", csvData(i)(16)))
                dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@SERIAL", csvData(i)(17)))
                dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@PACK_DOP", csvData(i)(18)))
                dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@BP_NO", csvData(i)(19)))

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


    Public Function SelectOtherSalesExtendedWarranty(ByVal queryParams As OtherSalesExtendedWarrantyModel) As DataTable
        Log4NetControl.ComInfoLogWrite(Log4NetControl.UserID)
        Dim dbConn As DBUtility = New DBUtility()
        Dim dt As DataTable = New DataTable()
        Dim sqlStr As String = "SELECT "
        'sqlStr = sqlStr & "DELFG,UNQ_NO,UPLOAD_USER,UPLOAD_DATE,BRANCH_CODE,COMPANY,COUNTRY,SAW_NO,OBJECT_ID,MODEL_CODE,SERIAL_NO,IMEI,GRMS_REQ_NO,        REQ_CATEGORY, REQ_CATEGORY_DESC, REQ_TYPE, REQ_TYPE_DESC, REQ_DATE, STATUS, STATUS_DESC, ASC_CODE, REQUISTER, ZSAW_CURR, REQ_VALUR, REQ_AMT, REQ_COMMENT, CONF_VALUE, CONF_AMT, CONF_COMMENT, CONTRACTOR, EXCH_TYPE, EXCH_TYPE_DESC, EXCH_REASON, EXCH_REASON_DESC, EXCH_SUB_REASON, EXCH_SUB_REASON_DESC, INVOICE_AMT, EST_REPAIR_AMT, PART_NO, PART_DESC, PO_NO, EMPLOYEE, PRE_EMPLOYEE, CONF_USER, CONF_DATE, FILE_NAME"
        sqlStr = sqlStr & "PO_NO as 'PO No.',CUSTOMER_NAME as 'Customer Name',PACK_NO as 'Purchase Pack',UNIT_PRICE as 'Unit Price',RETAIL_PRICE as 'Retail Price',SGST_RATE as 'SGST Rate %',SGST_AMOUNT as 'SGST Amount',CGST_RATE as 'CGST Rate %',CGST_AMOUNT as 'CGST Amount', IGST_RATE as 'IGST Rate %', IGST_AMOUNT as 'IGST Amount', CESS_RATE as 'CESS Rate %',CESS_AMOUNT as 'CESS Amount', TOTAL_TAX as 'Total Tax', TOTAL_AMOUNT as 'Total Amount', NOTE as 'Note', MODEL as 'Model', SERIAL as 'Serial', PACK_DOP as 'N/A:(PACK/DOP)', BP_NO as 'BP No'  "
        sqlStr = sqlStr & "FROM "
        sqlStr = sqlStr & "EXP_WTY "
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
