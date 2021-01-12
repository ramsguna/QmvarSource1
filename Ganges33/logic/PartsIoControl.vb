Imports System
Imports System.Data
Imports System.Web.Security
Imports System.Collections.Generic
Imports Ganges33.Ganges33.dao
Imports Ganges33.Ganges33.model
Imports Ganges33.Ganges33.logic

Public Class PartsIoControl
    Public Function AddModifyPartsIo(ByVal csvData()() As String, queryParams As PartsIoModel) As Boolean
        'Row 0 - Header1 and 1 - Header2
        '0 No
        '1 Branch
        '2 In/Out Date
        '3 Type
        '4 Type Description
        '5 Ref.Doc.No
        '6 Parts No
        '7 Description
        '8 Qty
        '9 MAP
        '10 Engineer Code
        '11 In/Out , Warranty
        '12 Unit

        Log4NetControl.ComInfoLogWrite(Log4NetControl.UserID)
        'Mandatory Column 12 from CSV
        Dim flag As Boolean = True
        If csvData(0).Length < 13 Then
            Return False
        End If

        Log4NetControl.ComInfoLogWrite(Log4NetControl.UserID & " - PartsIoCtr ")

        '      Log4NetControl.ComInfoLogWrite(Log4NetControl.UserID)
        Dim DateTimeNow As DateTime = DateTime.Now
        Dim dtNow As DateTime = DateTimeNow.AddMinutes(ConfigurationManager.AppSettings("TimeDiff"))
        Dim dbConn As DBUtility = New DBUtility()
        Dim dt As DataTable = New DataTable()
        dbConn.sqlTrn = dbConn.sqlConn.BeginTransaction()
        dbConn.sqlCmd.Transaction = dbConn.sqlTrn
        '       Dim flag As Boolean = True
        Dim flagAll As Boolean = True
        Dim sqlStr As String = ""
        Dim dtPartsIoExist As DataTable

        Dim isExist As Integer = 0
        '1st check INVOICE_NO,LOCAL_INVOICE_NO exist in the table 
        sqlStr = "SELECT TOP 1 PARTS_NO FROM PARTS_IO "
        sqlStr = sqlStr & " WHERE DELFG = 0 AND SRC_FILE_NAME='" & queryParams.SrcFileName & "'"
        'sqlStr = sqlStr & " AND SHIP_TO_BRANCH_CODE='" & queryParams.ShipToBranchCode & "'"
        dtPartsIoExist = dbConn.GetDataSet(sqlStr)
        'if exist then need to update delfg=1 then insert the record as new
        If (dtPartsIoExist Is Nothing) Or (dtPartsIoExist.Rows.Count = 0) Then
            'isExist = 0
        Else 'The records is already exist, need to update DELFg=0
            ' isExist = 1
            sqlStr = "UPDATE PARTS_IO SET DELFG=1  "
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
        'If there is no error
        If flagAll Then
            For i = 0 To csvData.Length - 1
                If i > 1 Then '0  Header, 1 Header
                    'If isExist = 1 Then
                    sqlStr = "Insert into PARTS_IO ("
                    sqlStr = sqlStr & "CRTDT, "
                    sqlStr = sqlStr & "CRTCD, "
                    ' sqlStr = sqlStr & "UPDDT, "
                    sqlStr = sqlStr & "UPDCD, "
                    sqlStr = sqlStr & "UPDPG, "
                    sqlStr = sqlStr & "DELFG, "
                    '         sqlStr = sqlStr & "UNQ_NO, "
                    sqlStr = sqlStr & "UPLOAD_USER, "
                    sqlStr = sqlStr & "UPLOAD_DATE, "
                    sqlStr = sqlStr & "NO, "
                    sqlStr = sqlStr & "SHIP_TO_BRANCH_CODE, "
                    sqlStr = sqlStr & "SHIP_TO_BRANCH, "
                    sqlStr = sqlStr & "IN_OUT_DATE, "
                    sqlStr = sqlStr & "TYPE, "
                    sqlStr = sqlStr & "TYPE_DESCRIPTION, "
                    sqlStr = sqlStr & "REF_DOC_NO, "
                    sqlStr = sqlStr & "PARTS_NO, "
                    sqlStr = sqlStr & "DESCRIPTION, "
                    sqlStr = sqlStr & "QTY, "
                    sqlStr = sqlStr & "MAP, "
                    sqlStr = sqlStr & "ENGINEER_CODE, "
                    sqlStr = sqlStr & "IN_OUT_WARRANTY, "
                    sqlStr = sqlStr & "UNIT, "
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
                    '         sqlStr = sqlStr & " (select max(UNQ_NO)+1 from G_RECEIVED) , "
                    sqlStr = sqlStr & "@UPLOAD_USER, "
                    sqlStr = sqlStr & "@UPLOAD_DATE, "
                    sqlStr = sqlStr & "@NO, "
                    sqlStr = sqlStr & "@SHIP_TO_BRANCH_CODE, "
                    sqlStr = sqlStr & "@SHIP_TO_BRANCH, "
                    sqlStr = sqlStr & "@IN_OUT_DATE, "
                    sqlStr = sqlStr & "@TYPE, "
                    sqlStr = sqlStr & "@TYPE_DESCRIPTION, "
                    sqlStr = sqlStr & "@REF_DOC_NO, "
                    sqlStr = sqlStr & "@PARTS_NO, "
                    sqlStr = sqlStr & "@DESCRIPTION, "
                    sqlStr = sqlStr & "@QTY, "
                    sqlStr = sqlStr & "@MAP, "
                    sqlStr = sqlStr & "@ENGINEER_CODE, "
                    sqlStr = sqlStr & "@IN_OUT_WARRANTY, "
                    sqlStr = sqlStr & "@UNIT, "
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
                    dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@NO", csvData(i)(0)))
                    dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@SHIP_TO_BRANCH_CODE", csvData(i)(1)))
                    dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@SHIP_TO_BRANCH", queryParams.ShipToBranch))

                    dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@IN_OUT_DATE", csvData(i)(2)))
                    dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@TYPE", csvData(i)(3)))
                    dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@TYPE_DESCRIPTION", csvData(i)(4)))
                    dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@REF_DOC_NO", csvData(i)(5)))
                    dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@PARTS_NO", csvData(i)(6)))
                    dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@DESCRIPTION", csvData(i)(7)))
                    dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@QTY", csvData(i)(8)))
                    dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@MAP", csvData(i)(9)))
                    dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@ENGINEER_CODE", csvData(i)(10)))
                    dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@IN_OUT_WARRANTY", csvData(i)(11)))
                    dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@UNIT", csvData(i)(12)))
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

        'Comment on 20190829
        '2nd Way Updation
        '''''''Log4NetControl.ComInfoLogWrite(Log4NetControl.UserID)
        '''''''Dim DateTimeNow As DateTime = DateTime.Now
        '''''''Dim dtNow As DateTime = DateTimeNow.AddMinutes(ConfigurationManager.AppSettings("TimeDiff"))
        '''''''Dim dbConn As DBUtility = New DBUtility()
        '''''''Dim dt As DataTable = New DataTable()
        '''''''dbConn.sqlTrn = dbConn.sqlConn.BeginTransaction(IsolationLevel.ReadCommitted)
        '''''''dbConn.sqlCmd.Transaction = dbConn.sqlTrn
        '''''''Dim flag As Boolean = True


        '''''''Dim con As SqlConnection = New SqlConnection(ConfigurationManager.ConnectionStrings("cnstrTmp").ConnectionString)
        '''''''con.Open()        '
        '''''''Dim trn As SqlTransaction = con.BeginTransaction(IsolationLevel.ReadCommitted)
        '''''''Dim dt As DateTime
        '''''''Dim csvData()() As String
        '''''''csvData = strArr
        '''''''Try
        '''''''    For i = 0 To csvData.Length - 1
        '''''''        If i <> 0 Then
        '''''''            Dim newData As Integer = -1
        '''''''            Dim add As Integer = -1
        '''''''            Dim select_sql1 As String = ""
        '''''''            select_sql1 = "SELECT * FROM tbl_DomesticWbgtMaxAvg WHERE  "
        '''''''            select_sql1 &= " Region = N'" & csvData(i)(0) & "' "
        '''''''            select_sql1 &= "AND PromotionBureau = N'" & csvData(i)(1) & "' "
        '''''''            select_sql1 &= "AND Prefectures = N'" & csvData(i)(2) & "' "
        '''''''            select_sql1 &= "AND StationNameKanji = N'" & csvData(i)(3) & "' "
        '''''''            select_sql1 &= "AND ReadingHiragana = N'" & csvData(i)(4) & "' "
        '''''''            Dim sqlSelect1 As New SqlCommand(select_sql1, con, trn)
        '''''''            Dim Adapter1 As New SqlDataAdapter(sqlSelect1)
        '''''''            Dim Builder1 As New SqlCommandBuilder(Adapter1)
        '''''''            Dim ds1 As New DataSet
        '''''''            Dim dr1 As DataRow
        '''''''            Adapter1.Fill(ds1)
        '''''''            If ds1.Tables(0).Rows.Count >= 1 Then
        '''''''                dr1 = ds1.Tables(0).Rows(0)
        '''''''                add = 1
        '''''''            Else
        '''''''                dr1 = ds1.Tables(0).NewRow
        '''''''                newData = 1
        '''''''            End If
        '''''''            If newData = 1 Then
        '''''''                dr1("CreateDate") = Now.Date
        '''''''                dr1("UpdateDate") = Now.Date
        '''''''                dr1("Region") = csvData(i)(0)
        '''''''                dr1("PromotionBureau") = csvData(i)(1)
        '''''''                dr1("Prefectures") = csvData(i)(2)
        '''''''                dr1("StationNameKanji") = csvData(i)(3)
        '''''''                dr1("ReadingHiragana") = csvData(i)(4)
        '''''''                dr1("DelFlag") = 0
        '''''''            End If
        '''''''            If add = 1 Then
        '''''''                dr1("UpdateDate") = Now.Date
        '''''''            End If
        '''''''            dr1("Max4") = csvData(i)(5)
        '''''''            dr1("Max5") = csvData(i)(6)
        '''''''            dr1("Max6") = csvData(i)(7)
        '''''''            dr1("Max7") = csvData(i)(8)
        '''''''            dr1("Max8") = csvData(i)(9)
        '''''''            dr1("Max9") = csvData(i)(10)
        '''''''            dr1("Max10") = csvData(i)(11)
        '''''''            dr1("MaxMax") = csvData(i)(12)
        '''''''            dr1("Avg4") = csvData(i)(13)
        '''''''            dr1("Avg5") = csvData(i)(14)
        '''''''            dr1("Avg6") = csvData(i)(15)
        '''''''            dr1("Avg7") = csvData(i)(16)
        '''''''            dr1("Avg8") = csvData(i)(17)
        '''''''            dr1("Avg9") = csvData(i)(18)
        '''''''            dr1("Avg10") = csvData(i)(19)
        '''''''            dr1("AvgMax") = csvData(i)(20)
        '''''''            '
        '''''''            If newData = 1 Then
        '''''''                ds1.Tables(0).Rows.Add(dr1)
        '''''''            End If
        '''''''            Adapter1.Update(ds1)
        '''''''        End If
        '''''''    Next i
        '''''''    trn.Commit()
        '''''''    Status = 0
        '''''''Catch ex As Exception
        '''''''    Status = 2
        '''''''    trn.Rollback()
        '''''''    lblInfo.Text = "<br>Satus: <font color=red>Failed <br>Error: " & ex.Message & " <br> OR <br>Please verify the datas in the CSV file..."
        '''''''Finally
        '''''''    If con.State <> ConnectionState.Closed Then
        '''''''        con.Close()
        '''''''    End If
        '''''''End Try

        ''''''''Unidentified - Ex Data's are matched with table column type
        '''''''If Status = 1 Then
        '''''''    lblInfo.Text = "<br>Status: <font color=red>Please verify the datas in the CSV file...<br>"
        '''''''ElseIf Status = 0 Then
        '''''''    lblInfo.Text = "<br>Status: <font color=Green>Successfully Updated...<br>"
        '''''''End If






    End Function

    Public Function SelectPartsIo(ByVal queryParams As PartsIoModel) As DataTable
        Log4NetControl.ComInfoLogWrite(Log4NetControl.UserID)
        Dim dbConn As DBUtility = New DBUtility()
        Dim dt As DataTable = New DataTable()
        Dim sqlStr As String = "SELECT "
        'sqlStr = sqlStr & "DELFG,UNQ_NO,UPLOAD_USER,UPLOAD_DATE,NO,SHIP_TO_BRANCH_CODE,SHIP_TO_BRANCH,IN_OUT_DATE,TYPE,TYPE_DESCRIPTION,REF_DOC_NO,PARTS_NO,DESCRIPTION,QTY,MAP,ENGINEER_CODE,IN_OUT_WARRANTY,UNIT,FILE_NAME,SRC_FILE_NAME"
        sqlStr = sqlStr & "NO,SHIP_TO_BRANCH_CODE as 'Branch',IN_OUT_DATE as 'In/Out Date',TYPE as 'Type',TYPE_DESCRIPTION as 'Type Description',REF_DOC_NO as 'Ref.Doc.No',PARTS_NO as 'Parts No',DESCRIPTION as 'Description',QTY as 'Qty',MAP as 'MAP',ENGINEER_CODE as 'Engineer Code',IN_OUT_WARRANTY as 'In/Out/Warranty',UNIT as 'Unit'"
        sqlStr = sqlStr & "FROM "
        sqlStr = sqlStr & "PARTS_IO "
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
