Imports System
Imports System.Data
Imports System.Web.Security
Imports System.Collections.Generic
Imports Ganges33.Ganges33.dao
Imports Ganges33.Ganges33.model
Imports Ganges33.Ganges33.logic
Imports System.Data.SqlClient

Public Class PrSummaryControl
    Public Function AddModifyPrSummaryOld(ByVal csvData()() As String, queryParams As PrSummaryModel) As Boolean
        'Row 0 - Header 1
        '0 Ship-to-Branch-Code
        '1 Ship-to-Branch
        '2 Invoice Date
        '3 Invoice No
        '4 Local Invoice No
        '5 Delivery No
        '6 Items
        '7 Amount
        '8 SGST / UTGST
        '9 CGST
        '10 IGST
        '11 Cess
        '12 Tax
        '13 Total
        '14 GR Status

        Log4NetControl.ComInfoLogWrite(Log4NetControl.UserID)
        'Mandatory Column 14 from CSV
        Dim flag As Boolean = True
        If csvData(0).Length < 15 Then
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
        Dim dtUpdateOtherExist As DataTable

        Dim isExist As Integer = 0


        '1st check INVOICE_NO,LOCAL_INVOICE_NO, DELIVERY_NO exist in the table 
        sqlStr = "SELECT TOP 1 INVOICE_NO,LOCAL_INVOICE_NO,DELIVERY_NO FROM PR_SUMMARY "
        sqlStr = sqlStr & " WHERE DELFG = 0  AND SRC_FILE_NAME='" & queryParams.SrcFileName & "'"
        ' sqlStr = sqlStr & " AND SHIP_TO_BRANCH_CODE='" & queryParams.ShipToBranchCode & "'"
        'sqlStr = sqlStr & " WHERE DELFG = 0 AND INVOICE_NO='" & csvData(i)(3) & "' and LOCAL_INVOICE_NO='" & csvData(i)(4) & "' and DELIVERY_NO='" & csvData(i)(5) & "'"
        dtUpdateOtherExist = dbConn.GetDataSet(sqlStr)
        'if exist then need to update delfg=1 then insert the record as new
        If (dtUpdateOtherExist Is Nothing) Or (dtUpdateOtherExist.Rows.Count = 0) Then
            'isExist = 0
        Else 'The records is already exist, need to update DELFg=0
            ' isExist = 1
            sqlStr = "UPDATE PR_SUMMARY SET DELFG=1  "
            sqlStr = sqlStr & "WHERE DELFG=0 "
            sqlStr = sqlStr & "AND SRC_FILE_NAME = @SRC_FILE_NAME"
            'sqlStr = sqlStr & "AND SHIP_TO_BRANCH_CODE = @SHIP_TO_BRANCH_CODE "
            dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@SRC_FILE_NAME", queryParams.SrcFileName))
            'dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@SHIP_TO_BRANCH_CODE", queryParams.ShipToBranchCode))

            ''sqlStr = sqlStr & "WHERE DELFG=0 AND "
            ''sqlStr = sqlStr & "INVOICE_NO = @INVOICE_NO AND LOCAL_INVOICE_NO = @LOCAL_INVOICE_NO AND DELIVERY_NO = @DELIVERY_NO"
            ''dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@INVOICE_NO", csvData(i)(3))) 'INVOICE_NO
            ''dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@LOCAL_INVOICE_NO", csvData(i)(4))) 'LOCAL_INVOICE_NO
            ''dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@DELIVERY_NO", csvData(i)(5))) 'DELIVERY_NO

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

                    'If isExist = 1 Then
                    sqlStr = "Insert into PR_SUMMARY ("
                    sqlStr = sqlStr & "CRTDT, "
                    sqlStr = sqlStr & "CRTCD, "
                    ' sqlStr = sqlStr & "UPDDT, "
                    sqlStr = sqlStr & "UPDCD, "
                    sqlStr = sqlStr & "UPDPG, "
                    sqlStr = sqlStr & "DELFG, "
                    ' sqlStr = sqlStr & "UNQ_NO, "
                    sqlStr = sqlStr & "UPLOAD_USER, "
                    sqlStr = sqlStr & "UPLOAD_DATE, "
                    sqlStr = sqlStr & "SHIP_TO_BRANCH_CODE, "
                    sqlStr = sqlStr & "SHIP_TO_BRANCH, "

                    sqlStr = sqlStr & "INVOICE_DATE, "
                    sqlStr = sqlStr & "INVOICE_NO, "
                    sqlStr = sqlStr & "LOCAL_INVOICE_NO, "
                    sqlStr = sqlStr & "DELIVERY_NO, "
                    sqlStr = sqlStr & "ITEMS, "
                    sqlStr = sqlStr & "AMOUNT, "
                    sqlStr = sqlStr & "SGST_UTGST, "
                    sqlStr = sqlStr & "CGST, "
                    sqlStr = sqlStr & "IGST, "
                    sqlStr = sqlStr & "CESS, "
                    sqlStr = sqlStr & "TAX, "
                    sqlStr = sqlStr & "TOTAL, "
                    sqlStr = sqlStr & "GR_STATUS, "
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
                    '      sqlStr = sqlStr & " (select max(UNQ_NO)+1 from PR_SUMMARY) , "
                    sqlStr = sqlStr & "@UPLOAD_USER, "
                    sqlStr = sqlStr & "@UPLOAD_DATE, "
                    sqlStr = sqlStr & "@SHIP_TO_BRANCH_CODE, "
                    sqlStr = sqlStr & "@SHIP_TO_BRANCH, "

                    sqlStr = sqlStr & "@INVOICE_DATE, "
                    sqlStr = sqlStr & "@INVOICE_NO, "
                    sqlStr = sqlStr & "@LOCAL_INVOICE_NO, "
                    sqlStr = sqlStr & "@DELIVERY_NO, "
                    sqlStr = sqlStr & "@ITEMS, "
                    sqlStr = sqlStr & "@AMOUNT, "
                    sqlStr = sqlStr & "@SGST_UTGST, "
                    sqlStr = sqlStr & "@CGST, "
                    sqlStr = sqlStr & "@IGST, "
                    sqlStr = sqlStr & "@CESS, "
                    sqlStr = sqlStr & "@TAX, "
                    sqlStr = sqlStr & "@TOTAL, "
                    sqlStr = sqlStr & "@GR_STATUS, "
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
                    dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@SHIP_TO_BRANCH_CODE", csvData(i)(0)))
                    dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@SHIP_TO_BRANCH", csvData(i)(1)))

                    dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@INVOICE_DATE", csvData(i)(2)))
                    dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@INVOICE_NO", csvData(i)(3)))
                    dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@LOCAL_INVOICE_NO", csvData(i)(4)))
                    dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@DELIVERY_NO", csvData(i)(5)))
                    dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@ITEMS", csvData(i)(6)))
                    dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@AMOUNT", csvData(i)(7)))
                    dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@SGST_UTGST", csvData(i)(8)))
                    dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@CGST", csvData(i)(9)))
                    dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@IGST", csvData(i)(10)))
                    dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@CESS", csvData(i)(11)))
                    dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@TAX", csvData(i)(12)))
                    dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@TOTAL", csvData(i)(13)))
                    dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@GR_STATUS", csvData(i)(14)))
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
        Return flag
    End Function



    Public Function AddModifyPrSummary(ByVal csvData()() As String, queryParams As PrSummaryModel) As Boolean
        Log4NetControl.ComInfoLogWrite(Log4NetControl.UserID)
        'Return Status 
        Dim flag As Boolean = False
        'Ship Code
        Dim strShipCode As String = ""
        Try
            'Reading ShipCode from database to Datatable 
            Dim _ShipBaseControl As ShipBaseControl = New ShipBaseControl()
            Dim dtShipCode As DataTable = _ShipBaseControl.SelectBranchCode()

            Dim blShipCodeExist As Boolean = False
            Dim strDate As String = ""

            'Define Database 
            Dim DateTimeNow As DateTime = DateTime.Now
            Dim dtNow As DateTime = DateTimeNow.AddMinutes(ConfigurationManager.AppSettings("TimeDiff"))
            Dim con As SqlConnection = New SqlConnection(ConfigurationManager.ConnectionStrings("cnstr").ConnectionString)
            'Open Database

            con.Open()
            Dim trn As SqlTransaction = con.BeginTransaction(IsolationLevel.ReadCommitted)
            Try
                Dim newData As Integer
                Dim add As Integer
                For i = 0 To csvData.Length - 1
                    If i <> 0 Then
                        blShipCodeExist = False
                        ''Ship Code
                        strShipCode = CommonControl.ConvertShipCode(csvData(i)(0))
                        blShipCodeExist = CommonControl.CheckDataTable(dtShipCode, Function(x) x("ship_code") = strShipCode)

                        If blShipCodeExist = False Then
                            Log4NetControl.ComErrorLogWrite(strShipCode & " is not exist")
                            flag = False
                            trn.Rollback()
                            Return flag
                            Exit Function
                        End If

                        'Bydefault Assing Zero
                        add = 0
                            newData = 0
                            'Query 
                            'If Record is exit then it will update otherwise it will create new records
                            Dim select_sql1 As String = ""
                            select_sql1 = "SELECT * FROM PR_SUMMARY WHERE DELFG = 0 "
                            select_sql1 &= "AND SHIP_TO_BRANCH_CODE = '" & strShipCode & "' "
                            select_sql1 &= "AND DELIVERY_NO = '" & csvData(i)(5) & "' "

                            'DataAdpater and DataSet Define
                            Dim sqlSelect1 As New SqlCommand(select_sql1, con, trn)
                            Dim Adapter1 As New SqlDataAdapter(sqlSelect1)
                            Dim Builder1 As New SqlCommandBuilder(Adapter1)
                            Dim ds1 As New DataSet
                            Dim dr1 As DataRow
                            Adapter1.Fill(ds1)
                            'If Record exist update otherwise insert as new record
                            If ds1.Tables(0).Rows.Count = 1 Then
                                dr1 = ds1.Tables(0).Rows(0)
                                add = 1
                            Else
                                dr1 = ds1.Tables(0).NewRow
                                newData = 1
                            End If

                            If newData = 1 Then
                                dr1("CRTDT") = dtNow
                                dr1("CRTCD") = queryParams.UserId
                            End If

                            If add = 1 Then
                                dr1("UPDDT") = dtNow
                                dr1("UPDCD") = queryParams.UserId
                            End If


                        dr1("UPDPG") = "PrSummaryControl.vb"
                        dr1("DELFG") = 0
                            dr1("UPLOAD_USER") = queryParams.UploadUser
                            dr1("UPLOAD_DATE") = dtNow

                            dr1("SHIP_TO_BRANCH_CODE") = strShipCode ''csvData(i)(0)
                            dr1("SHIP_TO_BRANCH") = csvData(i)(1)

                        strDate = Left(csvData(i)(2), 4) & "/" & Mid(csvData(i)(2), 5, 2) & "/" & Right(csvData(i)(2), 2)

                        dr1("INVOICE_DATE") = strDate
                        dr1("INVOICE_NO") = csvData(i)(3)
                            dr1("LOCAL_INVOICE_NO") = csvData(i)(4)
                            dr1("DELIVERY_NO") = csvData(i)(5)
                            dr1("ITEMS") = csvData(i)(6)
                            dr1("AMOUNT") = csvData(i)(7)
                            dr1("SGST_UTGST") = csvData(i)(8)
                            dr1("CGST") = csvData(i)(9)
                            dr1("IGST") = csvData(i)(10)
                            dr1("CESS") = csvData(i)(11)
                            dr1("TAX") = csvData(i)(12)
                            dr1("TOTAL") = csvData(i)(13)
                            dr1("GR_STATUS") = csvData(i)(14)
                            dr1("FILE_NAME") = queryParams.FileName
                            dr1("SRC_FILE_NAME") = queryParams.SrcFileName

                            If newData = 1 Then
                                ds1.Tables(0).Rows.Add(dr1)
                            End If
                            Adapter1.Update(ds1)
                        End If
                Next i

                trn.Commit()
                'Transaction Successful, return true
                flag = True
            Catch ex As Exception
                trn.Rollback()
                Log4NetControl.ComErrorLogWrite(ex.ToString())
                'Failure, return as false
                flag = False
            Finally
                If con.State <> ConnectionState.Closed Then
                    con.Close()
                End If
            End Try

        Catch ex As Exception
            flag = False
            Log4NetControl.ComErrorLogWrite(ex.ToString())
        End Try
        'Return Transaction Status 
        Return flag
    End Function



    Public Function SelectPrSummary(ByVal queryParams As PrSummaryModel) As DataTable
        Log4NetControl.ComInfoLogWrite(Log4NetControl.UserID)
        Dim dbConn As DBUtility = New DBUtility()
        Dim dt As DataTable = New DataTable()
        Dim sqlStr As String = "SELECT "
        'sqlStr = sqlStr & "DELFG,UPLOAD_USER,UPLOAD_DATE,SHIP_TO_BRANCH_CODE,SHIP_TO_BRANCH,INVOICE_DATE,INVOICE_NO,LOCAL_INVOICE_NO,DELIVERY_NO,ITEMS,AMOUNT,SGST_UTGST,CGST,IGST,CESS,TAX,TOTAL,GR_STATUS,FILE_NAME "
        sqlStr = sqlStr & "SHIP_TO_BRANCH_CODE as 'Ship-to-Branch-Code',SHIP_TO_BRANCH as 'Ship-to-Branch',LEFT(CONVERT(VARCHAR, INVOICE_DATE, 112), 10) as 'Invoice Date',INVOICE_NO as 'Invoice No',LOCAL_INVOICE_NO as 'Local Invoice No',DELIVERY_NO as 'Delivery No',ITEMS as 'Items',AMOUNT as 'Amount',SGST_UTGST as 'SGST / UTGST',CGST as 'CGST',IGST as 'IGST',CESS as 'Cess',TAX as 'Tax',TOTAL as 'Total',GR_STATUS as 'GR Status' "
        sqlStr = sqlStr & "FROM "
        sqlStr = sqlStr & "PR_SUMMARY "
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
