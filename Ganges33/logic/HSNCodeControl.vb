'VJ 2019/10/15
Imports System
Imports System.Data
Imports System.Web.Security
Imports System.Collections.Generic
Imports System.Globalization
Imports Ganges33.Ganges33.dao
Imports Ganges33.Ganges33.model
Imports Ganges33.Ganges33.logic
Public Class HSNCodeControl
    Public Function AddModifyHSNCode(ByVal csvData()() As String, queryParams As HSNCodeModel) As Boolean

        Log4NetControl.ComInfoLogWrite(Log4NetControl.UserID)
        'Mandatory Column 14 from CSV
        Dim flag As Boolean = True
        If csvData(0).Length < 6 Then
            Return False
        End If

        Dim DateTimeNow As DateTime = DateTime.Now
        Dim dtNow As DateTime = DateTimeNow.AddMinutes(ConfigurationManager.AppSettings("TimeDiff"))
        Dim dbConn As DBUtility = New DBUtility()
        Dim dt As DataTable = New DataTable()
        dbConn.sqlTrn = dbConn.sqlConn.BeginTransaction()
        dbConn.sqlCmd.Transaction = dbConn.sqlTrn

        Dim flagAll As Boolean = True
        Dim sqlStr As String = ""
        Dim dtReturnCreditExist As DataTable

        Dim isExist As Integer = 0
        '1st check INVOICE_NO,LOCAL_INVOICE_NO, DELIVERY_NO exist in the table 
        'sqlStr = "SELECT TOP 1 DATA_NO FROM HSN_CODE "
        'sqlStr = sqlStr & " WHERE DELFG = 0 AND SRC_FILE_NAME='" & queryParams.SrcFileName & "'"
        'sqlStr = sqlStr & " AND SHIP_TO_BRANCH_CODE='" & queryParams.ShipToBranchCode & "'"
        'dtReturnCreditExist = dbConn.GetDataSet(sqlStr)
        'if exist then need to update delfg=1 then insert the record as new
        'If (dtReturnCreditExist Is Nothing) Or (dtReturnCreditExist.Rows.Count = 0) Then
        'isExist = 0
        'Else 'The records is already exist, need to update DELFg=0
        ' isExist = 1
        sqlStr = "UPDATE HSN_CODE SET DELFG=1 "
        sqlStr = sqlStr & " WHERE DELFG=0 "
        sqlStr = sqlStr & "AND SRC_FILE_NAME = @SRC_FILE_NAME" 'AND SHIP_TO_BRANCH_CODE=@SHIP_TO_BRANCH_CODE
        'sqlStr = sqlStr & "AND SHIP_TO_BRANCH_CODE = @SHIP_TO_BRANCH_CODE "
        'dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@UPDCD", queryParams.UploadUser))
        'dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@UPDDT", dtNow))
        dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@SRC_FILE_NAME", queryParams.SrcFileName))
        ' dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@SHIP_TO_BRANCH_CODE", queryParams.ShipToBranchCode))
        flag = dbConn.ExecSQL(sqlStr)
        dbConn.sqlCmd.Parameters.Clear()
        'If Error occurs then will store the flag as false
        If Not flag Then
            flagAll = False
        End If
        'End If
        'If there is no error
        If flagAll Then
            For i = 0 To csvData.Length - 1
                If i > 0 Then '0-9  Header 
                    If i = 14 Or i = 21 Or i = 23 Or i = 24 Or i = 25 Or i = 27 Then
                        Dim test As String
                        test = "Alert"
                    End If
                    'If isExist = 1 Then
                    sqlStr = "Insert into HSN_CODE ("
                        sqlStr = sqlStr & "CRTDT, "
                        sqlStr = sqlStr & "CRTCD, "
                        sqlStr = sqlStr & "UPDCD, "
                        sqlStr = sqlStr & "UPDPG, "
                        sqlStr = sqlStr & "DELFG, "
                        sqlStr = sqlStr & "UPLOAD_USER, "
                        sqlStr = sqlStr & "UPLOAD_DATE, "
                        sqlStr = sqlStr & "SHIP_TO_BRANCH, "
                        sqlStr = sqlStr & "SHIP_TO_BRANCH_CODE, "
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
                        sqlStr = sqlStr & "@SHIP_TO_BRANCH, "
                        sqlStr = sqlStr & "@SHIP_TO_BRANCH_CODE, "
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
                        dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@PART_NO", csvData(i)(0)))
                        dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@HSN_CODE", csvData(i)(1)))
                        dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@AMOUNT", String.Empty))
                        dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@INTEGRATED_TAX", csvData(i)(2).Replace("%", "")))
                        dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@CENTRAL_TAX", csvData(i)(3).Replace("%", "")))
                        dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@STATE_TAX", csvData(i)(4).Replace("%", "")))
                        dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@CESS", csvData(i)(5).Replace("%", "")))
                        dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@HSN_CODE_IMPORT_DATE", queryParams.SrcFileName.Replace("hnc-", "").Replace(".csv", "") & "01"))
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
        End If



        If flagAll Then
            flag = True
            dbConn.sqlTrn.Commit()
        Else
            flag = False
            dbConn.sqlTrn.Rollback()
        End If
        dbConn.CloseConnection()
        'Code commented for update PR_Detail VJ 2019/10/17
        'If flag = True Then
        '    flag = UpdateHSNCodePRDetails(queryParams)
        'End If
        'Code commented for update PR_Detail VJ 2019/10/17
        Return flag


    End Function
    Public Function UpdateHSNCodePRDetails(queryParams As HSNCodeModel) As Boolean

        Log4NetControl.ComInfoLogWrite(Log4NetControl.UserID)
        'Mandatory Column 14 from CSV
        Dim flag As Boolean = True


        Dim DateTimeNow As DateTime = DateTime.Now
        Dim dtNow As DateTime = DateTimeNow.AddMinutes(ConfigurationManager.AppSettings("TimeDiff"))
        Dim dbConn As DBUtility = New DBUtility()
        Dim dt As DataTable = New DataTable()
        dbConn.sqlTrn = dbConn.sqlConn.BeginTransaction()
        dbConn.sqlCmd.Transaction = dbConn.sqlTrn

        'Dim flagAll As Boolean = True
        Dim sqlStr As String = ""
        Dim dtReturnCreditExist As DataTable

        Dim isExist As Integer = 0


        ';WITH cte AS
        '(
        '    SELECT 
        '		   PART_NO,HSN_CODE,HSN_CODE_IMPORT_DATE,SRC_FILE_NAME,
        '		   rn = ROW_NUMBER() OVER (PARTITION BY PART_NO ORDER BY convert(datetime,replace(replace(replace(SRC_FILE_NAME,'hsn-',''),'hnc-',''),'.csv','')+'01') DESC)
        '		   FROM HSN_CODE where DELFG = 0 and PART_NO 
        '		   in (select PART_NO FROM HSN_CODE 
        '		   where  SRC_FILE_NAME = 'hnc-201908.csv' and 
        '		   DELFG=0)

        ')
        'SELECT *
        'FROM cte
        'WHERE rn = 1


        ' Update Lastest HSN code in PR_Detail table using import HSN file
        '      		;WITH cte AS
        '(
        '   Select Case
        '         PART_NO, HSN_CODE,
        '         rn = ROW_NUMBER() OVER (PARTITION BY PART_NO ORDER BY convert(datetime, Replace(Replace(SRC_FILE_NAME,'hnc-',''),'.csv','')+'01') DESC)
        '         From HSN_CODE Where DELFG = 0 And PART_NO 
        '   in (select PART_NO FROM HSN_CODE where SRC_FILE_NAME = 'hnc-201908.csv' and DELFG=0)		
        ') 
        'update PR
        'set PR.HSN_CODE = CTE.HSN_CODE 
        'From PR_DETAIL PR 
        'INNER Join CTE ON PR.PART_NO = CTE.PART_NO
        '      where CTE.rn = 1 And PR.DELFG = 0

        sqlStr = "With CTE "
        sqlStr = sqlStr & " As "
        sqlStr = sqlStr & " ( "
        sqlStr = sqlStr & " select PART_NO,HSN_CODE,rn = ROW_NUMBER() OVER (PARTITION BY PART_NO ORDER BY convert(datetime, Replace(RIGHT(SRC_FILE_NAME, LEN(SRC_FILE_NAME) - 4),'.csv','')+'01') DESC) "
        sqlStr = sqlStr & "FROM HSN_CODE where DELFG = 0 and PART_NO in (select PART_NO FROM HSN_CODE where SRC_FILE_NAME = @SRC_FILE_NAME AND DELFG=0)"
        sqlStr = sqlStr & " ) update PR "
        sqlStr = sqlStr & " set PR.HSN_CODE = CTE.HSN_CODE,PR.UPDDT=@UPDDT,PR.UPDCD=@UPDCD "
        sqlStr = sqlStr & " FROM PR_DETAIL PR "
        sqlStr = sqlStr & " INNER JOIN CTE ON PR.PART_NO = CTE.PART_NO "
        sqlStr = sqlStr & " where CTE.rn =1 AND PR.DELFG = 0	"



        If Not String.IsNullOrEmpty(queryParams.SrcFileName) Then
            dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@SRC_FILE_NAME", queryParams.SrcFileName))
            dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@UPDDT", dtNow))
            dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@UPDCD", queryParams.UserId))
        End If
        flag = dbConn.ExecSQL(sqlStr)

        If flag Then
            dbConn.sqlTrn.Commit()
        Else
            dbConn.sqlTrn.Rollback()
        End If
        ' Dim _DataTable As DataTable = dbConn.GetDataSet(sqlStr)
        dbConn.CloseConnection()

        Return flag
    End Function




    Public Function SelectHSNCode(ByVal queryParams As HSNCodeModel) As DataTable
        Log4NetControl.ComInfoLogWrite(Log4NetControl.UserID)
        Dim PositionStart As Integer
        'Dim strBranchName As String
        Dim GSTIN As String
        GSTIN = String.Empty

        PositionStart = InStr(1, queryParams.SrcFileName, "_")
        'strBranchName = Left(queryParams.SrcFileName, PositionStart - 1)
        'GSTIN = ConfigurationManager.AppSettings("GSTIN" & strBranchName)

        'Dim inputfile As String = "hsn-" & queryParams.SrcFileName & "','hnc-" & queryParams.SrcFileName
        Dim inputfile As String = "hnc-" & queryParams.SrcFileName
        '        ;WITH cte AS
        '(
        '   Select Case*,
        '         ROW_NUMBER() OVER 
        '		 (PARTITION BY PART_NO ORDER BY convert(datetime,replace(replace(SRC_FILE_NAME,'hnc-',''),'.csv','')+'01') DESC) AS rn
        '   From [HSN_CODE] Where DELFG = 0
        ')
        'Select Case PART_NO,HSN_CODE
        'From cte
        'Where rn = 1
        Dim dbConn As DBUtility = New DBUtility()
        Dim dt As DataTable = New DataTable()
        Dim sqlStr As String = "Select "
        'sqlStr = sqlStr & "DELFG,UNQ_NO,UPLOAD_USER,UPLOAD_DATE,SHIP_TO_BRANCH_CODE,SHIP_TO_BRANCH,INVOICE_DATE,INVOICE_NO,LOCAL_INVOICE_NO,DELIVERY_NO,ITEMS,AMOUNT,SGST_UTGST,CGST,IGST,CESS,TAX,TOTAL,GR_STATUS,FILE_NAME"
        sqlStr = sqlStr & "PART_NO 'Part No',HSN_CODE 'HSN Code',AMOUNT 'Amount',INTEGRATED_TAX 'Integrated Tax', "
        sqlStr = sqlStr & "CENTRAL_TAX 'Central Tax',STATE_TAX 'State Tax',CESS 'Cess' "
        'sqlStr = sqlStr & " from HSN_CODE where DELFG=0 And SHIP_TO_BRANCH_CODE = @SHIP_TO_BRANCH_CODE and SRC_FILE_NAME in ('" & inputfile & "')"
        sqlStr = sqlStr & " from HSN_CODE where DELFG=0 And SRC_FILE_NAME = '" & inputfile & "'"
        sqlStr = sqlStr & " Order by PART_NO , Convert(datetime, Replace(RIGHT(SRC_FILE_NAME, LEN(SRC_FILE_NAME) - 4),'.csv','')+'01') desc"

        If Not String.IsNullOrEmpty(queryParams.SrcFileName) Then
            dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@SRC_FILE_NAME", inputfile))
            'dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@SHIP_TO_BRANCH_CODE", queryParams.ShipToBranchCode))
        End If
        Dim _DataTable As DataTable = dbConn.GetDataSet(sqlStr)
        dbConn.CloseConnection()
        Return _DataTable
    End Function


End Class
