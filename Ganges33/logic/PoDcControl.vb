Imports System
Imports System.Data
Imports System.Web.Security
Imports System.Collections.Generic
Imports Ganges33.Ganges33.dao
Imports Ganges33.Ganges33.model
Imports Ganges33.Ganges33.logic
Imports System.Data.SqlClient
Public Class PoDcControl
    ''' <summary>
    ''' PO_NO is key
    ''' </summary>
    ''' <param name="csvData"></param>
    ''' <param name="queryParams"></param>
    ''' <returns></returns>
    Public Function AddUploadSummary(ByVal csvData()() As String, queryParams As PoDcModel) As Boolean
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

            Dim PoDate As String = ""

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
                        ''Ship Code Verification
                        blShipCodeExist = False
                        ''Ship Code
                        strShipCode = CommonControl.ConvertShipCode(csvData(i)(3))
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
                        select_sql1 = "SELECT * FROM PO_DC WHERE DELFG = 0 "
                        select_sql1 &= "AND SHIP_TO = '" & strShipCode & "' "
                        select_sql1 &= "AND CONFIRMATION_NO = '" & csvData(i)(4) & "' "
                        'Log4NetControl.ComErrorLogWrite(select_sql1)

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

                        dr1("UPDPG") = "PoDrControl.vb"
                        dr1("DELFG") = 0
                        dr1("UPLOAD_USER") = queryParams.UPLOAD_USER
                        dr1("UPLOAD_DATE") = dtNow
                        dr1("SHIP_TO_BRANCH_CODE") = queryParams.SHIP_TO_BRANCH_CODE
                        dr1("SHIP_TO_BRANCH") = queryParams.SHIP_TO_BRANCH

                        ''If value IsNot Nothing AndAlso value.ToString() <> String.Empty Then
                        PoDate = Trim(csvData(i)(1))
                        If Not (PoDate = "") Then
                            dr1("PO_DATE") = PoDate
                        End If

                        dr1("PO_NO") = csvData(i)(2)
                        dr1("SHIP_TO") = strShipCode
                        dr1("CONFIRMATION_NO") = csvData(i)(4)
                        dr1("ITEM_NO") = csvData(i)(5)
                        dr1("QTY") = csvData(i)(6)
                        dr1("AMOUNT") = csvData(i)(7)
                        dr1("STATUS") = csvData(i)(8)
                        dr1("FILE_NAME") = queryParams.FILE_NAME
                        dr1("SRC_FILE_NAME") = queryParams.SRC_FILE_NAME
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



    Public Function AddUploadSummaryOLd(ByVal csvData()() As String, queryParams As PoDcModel) As Boolean

        Log4NetControl.ComInfoLogWrite(Log4NetControl.UserID)
        ' Mandatory Column 8 from CSV
        Dim flag As Boolean = True
        If csvData(0).Length < 8 Then
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


        '1st check PO_NO exist in the table 
        sqlStr = "SELECT TOP 1 PO_NO FROM PO_DC "
        sqlStr = sqlStr & " WHERE DELFG = 0  AND SRC_FILE_NAME='" & queryParams.SRC_FILE_NAME & "'"
        dtUpdateOtherExist = dbConn.GetDataSet(sqlStr)
        'if exist then need to update delfg=1 then insert the record as new
        If (dtUpdateOtherExist Is Nothing) Or (dtUpdateOtherExist.Rows.Count = 0) Then
            'isExist = 0
        Else 'The records is already exist, need to update DELFg=0
            ' isExist = 1
            sqlStr = "UPDATE PO_DC SET DELFG=1  "
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

        'If there is no error
        If flagAll Then
            For i = 0 To csvData.Length - 1
                If i > 0 Then '0  Header

                    'If isExist = 1 Then
                    sqlStr = "Insert into PO_DC ("
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

                    sqlStr = sqlStr & "PO_DATE, "
                    sqlStr = sqlStr & "PO_NO, "
                    sqlStr = sqlStr & "SHIP_TO, "
                    sqlStr = sqlStr & "CONFIRMATION_NO, "
                    sqlStr = sqlStr & "ITEM_NO, "
                    sqlStr = sqlStr & "QTY, "
                    sqlStr = sqlStr & "AMOUNT, "
                    sqlStr = sqlStr & "STATUS, "

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

                    sqlStr = sqlStr & "@PO_DATE, "
                    sqlStr = sqlStr & "@PO_NO, "
                    sqlStr = sqlStr & "@SHIP_TO, "
                    sqlStr = sqlStr & "@CONFIRMATION_NO, "
                    sqlStr = sqlStr & "@ITEM_NO, "
                    sqlStr = sqlStr & "@QTY, "
                    sqlStr = sqlStr & "@AMOUNT, "
                    sqlStr = sqlStr & "@STATUS, "

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
                    'dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@No", csvData(i)(0)))
                    dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@PO_DATE", csvData(i)(1)))
                    'dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@PO_NO", csvData(i)(3)))
                    'dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@SHIP_TO_BRANCH", csvData(i)(4)))
                    'dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@PO_DATE", csvData(i)(5)))
                    dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@PO_NO", csvData(i)(2)))
                    dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@SHIP_TO", csvData(i)(3)))

                    dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@CONFIRMATION_NO", csvData(i)(4)))
                    dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@ITEM_NO", csvData(i)(5)))
                    dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@QTY", csvData(i)(6)))
                    dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@AMOUNT", csvData(i)(7)))
                    dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@STATUS", csvData(i)(8)))

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
