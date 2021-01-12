Imports Ganges33.Ganges33.dao
Imports Ganges33.Ganges33.model
Imports Ganges33.Ganges33.logic
Imports System.Data

Public Class Export_File_DetailsControl


    Public Function GetImportTableDetails() As DataTable
        Dim dtImport As New DataTable
        dtImport.Columns.AddRange(New DataColumn(4) {New DataColumn("Id", GetType(Integer)), New DataColumn("Table_Name",
                                  GetType(String)), New DataColumn("Description", GetType(String)),
                                  New DataColumn("Others", GetType(String)), New DataColumn("Rep_Exp_Seq", GetType(String))})

        dtImport.Rows.Add(1, "SC_DSR", "Daily Statement Report", "", "1")
        dtImport.Rows.Add(2, "WTY_EXCEL", "Warranty Excel File", "", "2")
        dtImport.Rows.Add(3, "INVOICE_UPDATE", "Sales Invoice to Samsung_C IW", "C", "3A")
        dtImport.Rows.Add(4, "INVOICE_UPDATE", "Sales Invoice to Samsung_EXC IW", "EXC", "3A")
        dtImport.Rows.Add(5, "INVOICE_UPDATE", "Invoice Update OOW", "OWC", "3A")
        dtImport.Rows.Add(6, "PR_SUMMARY", "Purchase Register summary", "", "6")
        dtImport.Rows.Add(7, "PR_DETAIL", "Purchase Register Detail", "", "7")
        dtImport.Rows.Add(8, "G_RECEIVED", "Good Receiving Summary", "", "8A")
        dtImport.Rows.Add(9, "GD_RECEIVED", "Good Receiving Detail", "", "8B")
        dtImport.Rows.Add(10, "STOCK_OVERVIEW", "Stock OverView", "", "9")
        dtImport.Rows.Add(11, "SAW_DISCOUNT ", "SAW Discount", "", "10")
        dtImport.Rows.Add(12, "PARTS_IO", "Parts In and Out History", "", "11")
        dtImport.Rows.Add(13, "G2S_PIAD", "GSS Paid to Samsung", "", "14")
        dtImport.Rows.Add(14, "RETURN_CREDIT", "Return Credit", "", "15")
        dtImport.Rows.Add(15, "S_LEDGER", "Samsung Ledger", "", "16")
        dtImport.Rows.Add(16, "SP_RETURN", "Service Parts Return", "", "19")
        dtImport.Rows.Add(17, "DEBITNOTE_REGISTER", "Debit Note Register", "", "18")
        dtImport.Rows.Add(18, "HSN_CODE", "HSN Code", "", "19B")
        dtImport.Rows.Add(19, "EXP_WTY", "Other Sales Extended Warranty", "", "20")
        dtImport.Rows.Add(20, "PO_STATUS", "PO Status", "", "21")
        dtImport.Rows.Add(21, "Activity_Report", "Activity Report", "", "22")
        dtImport.Rows.Add(22, "PO_DC", "PO Confirmation", "", "23")
        Return dtImport
    End Function

    Public Function GetImportFileSummary(ByVal sscCode As String, sscName As String, cName As String, CFromdate As String,
                                                CTodate As String, Flag As String, IsCurrentUser As Boolean, userLevel As String,
                                                adminFlag As Boolean, userID As String) As DataTable
        Try
            Dim dbConn As DBUtility = New DBUtility()
            Dim dtImportSumDtl As New DataTable

            CFromdate = CFromdate & " 00:00:00.000"
            CTodate = CTodate & " 23:59:59.999"

            dtImportSumDtl.Columns.AddRange(New DataColumn(5) {New DataColumn("Seq", GetType(Integer)),
                                            New DataColumn("Target_Store", GetType(String)),
                                            New DataColumn("Report_Name", GetType(String)),
                                  New DataColumn("Cnt", GetType(Integer)),
                                  New DataColumn("Create_Date_From", GetType(DateTime)),
                                  New DataColumn("Create_Date_To", GetType(DateTime))})

            For Each row As DataRow In GetImportTableDetails().Rows
                Dim sqlStr As String = "SELECT "
                sqlStr = sqlStr & ""

                If row.Item(0) = 1 Then
                    sqlStr = sqlStr & "" & row.Item(0) & " As Seq,Ltrim(Rtrim(Branch_Name)) As Target_Store,'" & row.Item(2) & "' As Report_Name, "
                    'sqlStr = sqlStr & " count(*) Count From " & row.Item(1) & " where  ((CONVERT(DATE, " & cName & ") between  "
                    sqlStr = sqlStr & " count(*) Cnt,Min(case when isnull(upddt,'') = '' then crtdt else upddt end) As 'Create_Date_From', "
                    sqlStr = sqlStr & "Max(case when isnull(upddt,'') = '' then crtdt else upddt end) As 'Create_Date_To' "
                    sqlStr = sqlStr & "From " & row.Item(1) & " where  ((" & cName & " between  "
                    sqlStr = sqlStr & "'" & CFromdate & "' and '" & CTodate & "'"
                    If IsCurrentUser = True Then
                        sqlStr = sqlStr & "  and CRTCD = '" & userID & "'"
                    End If
                    'sqlStr = sqlStr & ") or (CONVERT(DATE, CRTDT) between '" & CFromdate & "' and '" & CTodate & "'"
                    sqlStr = sqlStr & ") or (UPDDT between '" & CFromdate & "' and '" & CTodate & "'"
                    If IsCurrentUser = True Then
                        sqlStr = sqlStr & "  and UPDCD = '" & userID & "'"
                    End If
                    sqlStr = sqlStr & ")) and Branch_name in (" & sscName & ") "

                    sqlStr = sqlStr & "and DELFG in (" & Flag & ") group by Branch_name"

                ElseIf row.Item(0) = 2 Then
                    sqlStr = sqlStr & "" & row.Item(0) & " As Seq,Ltrim(Rtrim(Upload_Branch)) As Target_Store,'" & row.Item(2) & "' As Report_Name, "
                    'sqlStr = sqlStr & " count(*) Count From " & row.Item(1) & " where  ((CONVERT(DATE, " & cName & ") between  "
                    sqlStr = sqlStr & " count(*) Cnt,Min(case when isnull(upddt,'') = '' then crtdt else upddt end) As 'Create_Date_From', "
                    sqlStr = sqlStr & "Max(case when isnull(upddt,'') = '' then crtdt else upddt end) As 'Create_Date_To' "
                    sqlStr = sqlStr & " From " & row.Item(1) & " where  (( " & cName & " between  "
                    sqlStr = sqlStr & "'" & CFromdate & "' and '" & CTodate & "'"
                    If IsCurrentUser = True Then
                        sqlStr = sqlStr & "  and CRTCD = '" & userID & "'"
                    End If
                    'sqlStr = sqlStr & ") or (CONVERT(DATE, CRTDT) between '" & CFromdate & "' and '" & CTodate & "'"
                    sqlStr = sqlStr & ") or (UPDDT between '" & CFromdate & "' and '" & CTodate & "'"
                    If IsCurrentUser = True Then
                        sqlStr = sqlStr & "  and UPDCD = '" & userID & "'"
                    End If
                    sqlStr = sqlStr & ")) and Upload_Branch in (" & sscName & ") "

                    sqlStr = sqlStr & "and DELFG in (" & Flag & ") group by Upload_Branch"

                ElseIf row.Item(0) = 3 Or row.Item(0) = 4 Or row.Item(0) = 5 Then
                    sqlStr = sqlStr & "" & row.Item(0) & " As Seq,Ltrim(Rtrim(Upload_Branch)) As Target_Store,'" & row.Item(2) & "' As Report_Name, "
                    'sqlStr = sqlStr & " count(*) Count From " & row.Item(1) & " where  ((CONVERT(DATE, " & cName & ") between  "
                    sqlStr = sqlStr & " count(*) Cnt,Min(case when isnull(upddt,'') = '' then crtdt else upddt end) As 'Create_Date_From', "
                    sqlStr = sqlStr & "Max(case when isnull(upddt,'') = '' then crtdt else upddt end) As 'Create_Date_To' "
                    sqlStr = sqlStr & " From " & row.Item(1) & " where number ='" & row.Item(3) & "' "
                    sqlStr = sqlStr & "And (( " & cName & " between  "
                    sqlStr = sqlStr & "'" & CFromdate & "' and '" & CTodate & "'"
                    If IsCurrentUser = True Then
                        sqlStr = sqlStr & "  and CRTCD = '" & userID & "'"
                    End If
                    'sqlStr = sqlStr & ") or (CONVERT(DATE, CRTDT) between '" & CFromdate & "' and '" & CTodate & "'"
                    sqlStr = sqlStr & ") or (UPDDT between '" & CFromdate & "' and '" & CTodate & "'"
                    If IsCurrentUser = True Then
                        sqlStr = sqlStr & "  and UPDCD = '" & userID & "'"
                    End If
                    sqlStr = sqlStr & ")) and Upload_Branch in (" & sscName & ") "

                    sqlStr = sqlStr & "and DELFG in (" & Flag & ") group by Upload_Branch"

                ElseIf row.Item(0) = 6 Or row.Item(0) = 7 Or row.Item(0) = 13 Or row.Item(0) = 14 Or row.Item(0) = 15 Then
                    sqlStr = sqlStr & "" & row.Item(0) & " As Seq,Ltrim(Rtrim(ship_name)) As Target_Store,'" & row.Item(2) & "' As Report_Name, "
                    'sqlStr = sqlStr & " count(*) Count From " & row.Item(1) & " where  ((CONVERT(DATE, " & cName & ") between  "
                    sqlStr = sqlStr & " count(*) Cnt,Min(" & row.Item(1) & ".crtdt) As 'Create_Date_From', "
                    sqlStr = sqlStr & "Max(" & row.Item(1) & ".crtdt) As 'Create_Date_To' From " & row.Item(1) & ""
                    sqlStr = sqlStr & " Left Join M_ship_base on convert(int,M_ship_base.ship_code) = convert(int," & row.Item(1) & ""
                    sqlStr = sqlStr & ".SHIP_TO_BRANCH_CODE) where " & row.Item(1) & "." & cName & " between  "
                    sqlStr = sqlStr & "'" & CFromdate & "' and '" & CTodate & "'"
                    If IsCurrentUser = True Then
                        sqlStr = sqlStr & "  and CRTCD = '" & userID & "'"
                    End If
                    'sqlStr = sqlStr & ") or (CONVERT(DATE, CRTDT) between '" & CFromdate & "' and '" & CTodate & "'"
                    'sqlStr = sqlStr & ") or (UPDDT between '" & CFromdate & "' and '" & CTodate & "'"
                    ' If IsCurrentUser = True Then
                    'sqlStr = sqlStr & "  and UPDCD = '" & userID & "'"
                    'End If
                    'sqlStr = sqlStr & " and convert(int," & row.Item(1) & "." & sscCode & ") "
                    sqlStr = sqlStr & " and convert(int," & row.Item(1) & ".SHIP_TO_BRANCH_CODE) In (" & sscCode & ") "
                    sqlStr = sqlStr & "And " & row.Item(1) & ".DELFG In (" & Flag & ") group by ship_name"

                ElseIf row.Item(0) = 8 Or row.Item(0) = 9 Or row.Item(0) = 10 Or row.Item(0) = 11 Or
                    row.Item(0) = 12 Or row.Item(0) = 16 Or row.Item(0) = 17 Or
                     row.Item(0) = 18 Or row.Item(0) = 19 Or row.Item(0) = 20 Then

                    sqlStr = sqlStr & "" & row.Item(0) & " As Seq,Ltrim(Rtrim(Ship_to_Branch)) As Target_Store,'" & row.Item(2) & "' As Report_Name, "
                    'sqlStr = sqlStr & " count(*) Count From " & row.Item(1) & " where  ((CONVERT(DATE, " & cName & ") between  "
                    sqlStr = sqlStr & " count(*) Cnt,Min(crtdt) As 'Create_Date_From',Max(crtdt) As 'Create_Date_To' "
                    sqlStr = sqlStr & "From " & row.Item(1) & " where " & cName & " between  "
                    sqlStr = sqlStr & "'" & CFromdate & "' and '" & CTodate & "'"
                    If IsCurrentUser = True Then
                        sqlStr = sqlStr & "  and CRTCD = '" & userID & "'"
                    End If
                    'sqlStr = sqlStr & ") or (CONVERT(DATE, CRTDT) between '" & CFromdate & "' and '" & CTodate & "'"
                    'sqlStr = sqlStr & ") or (UPDDT between '" & CFromdate & "' and '" & CTodate & "'"
                    'If IsCurrentUser = True Then
                    '    sqlStr = sqlStr & "  and UPDCD = '" & userID & "'"
                    'End If
                    sqlStr = sqlStr & " and Ship_to_Branch in (" & sscName & ") "

                    sqlStr = sqlStr & "and DELFG in (" & Flag & ") group by Ship_to_Branch"

                    'bharathiraja-1
                ElseIf row.Item(0) = 21 Then
                    sqlStr = sqlStr & "" & row.Item(0) & " As Seq,Ltrim(Rtrim(ship_name)) As Target_Store,'" & row.Item(2) & "' As Report_Name, "
                    sqlStr = sqlStr & " count(*) Cnt,Min(" & row.Item(1) & ".crtdt) As 'Create_Date_From', "
                    sqlStr = sqlStr & "Max(" & row.Item(1) & ".crtdt) As 'Create_Date_To' From " & row.Item(1) & ""
                    sqlStr = sqlStr & " Left Join M_ship_base on convert(int,M_ship_base.ship_code) = convert(int," & row.Item(1) & ""

                    sqlStr = sqlStr & ".location) where " & row.Item(1) & "." & cName & " between  "
                    sqlStr = sqlStr & "'" & CFromdate & "' and '" & CTodate & "'"
                    If IsCurrentUser = True Then
                        sqlStr = sqlStr & "  and CRTCD = '" & userID & "'"
                    End If
                    sqlStr = sqlStr & " and convert(int," & row.Item(1) & ".location) In (" & sscCode & ") "
                    sqlStr = sqlStr & "And " & row.Item(1) & ".DELFG In (" & Flag & ") group by ship_name"

                    'bharathiraja-POC-1
                ElseIf row.Item(0) = 22 Then
                    sqlStr = sqlStr & "" & row.Item(0) & " As Seq,Ltrim(Rtrim(Ship_to_Branch)) As Target_Store,'" & row.Item(2) & "' As Report_Name, "
                    sqlStr = sqlStr & " count(*) Cnt,Min(crtdt) As 'Create_Date_From',Max(crtdt) As 'Create_Date_To' "
                    sqlStr = sqlStr & "From " & row.Item(1) & " where " & cName & " between  "
                    sqlStr = sqlStr & "'" & CFromdate & "' and '" & CTodate & "'"
                    If IsCurrentUser = True Then
                        sqlStr = sqlStr & "  and CRTCD = '" & userID & "'"
                    End If

                    sqlStr = sqlStr & " and Ship_to_Branch in (" & sscName & ") "

                    sqlStr = sqlStr & "and DELFG in (" & Flag & ") group by Ship_to_Branch"
                End If







                Dim _DataTable As DataTable = dbConn.GetDataSet(sqlStr)
                If _DataTable IsNot Nothing Then
                    'dtImportSumDtl.a = _DataTable.Copy()
                    dtImportSumDtl.Merge(_DataTable)
                    'dtImportSumDtl = _DataTable.Copy()
                End If
                'sqlStr = sqlStr & " '" & row.Item(0) & "'As Seq,"
                'For Each column As DataColumn In GetImportTableDetails().Columns
                'Console.WriteLine(row(column))
                'Next column
            Next row
            dbConn.CloseConnection()
            'For Each (DataRow)
            '        foreach(DataRow, row, in, dr_art_line_2.Rows);
            '{
            '    QuantityInIssueUnit_value = Convert.ToInt32(row, "columnname")
            '}
            Dim dvimpdtl As New DataView(dtImportSumDtl)
            'DataView dv = New DataView(dtImportSumDtl)
            dvimpdtl.Sort = "Target_Store ASC, Seq ASC"

            'dtImportSumDtl.DefaultView.Sort = "Target_Store ASC ,Seq ASC "
            'Return dtImportSumDtl
            Return dvimpdtl.ToTable()
        Catch ex As Exception
            Dim msg As String = ex.Message
            'lblMsg.Text = ex.Message
            'Dim msg As String = cc.showMsg("")
            'ClientScript.RegisterStartupScript(Me.GetType(), "startup", msg, True)
            'Exit Sub
        End Try

    End Function

    Public Function GetImportFileSummaryDetails(ByVal queryParams As Export_File_Parameter_Details, Type As String, Store_ID As String, Store_ID_Name As String, Seq As Integer) As DataTable
        '(ByVal sscCode As String, sscName As String, cName As String, CFromdate As String,
        '                                        CTodate As String, Flag As String, IsCurrentUser As Boolean, userLevel As String,
        '                                        adminFlag As Boolean, userID As String) As DataTable


        Try
            Dim dbConn As DBUtility = New DBUtility()
            Dim dtImportSumDtl As New DataTable
            Dim CFromdate As String
            Dim CTodate As String
            Dim cName As String
            Dim IsCurrentUser As Boolean
            Dim userID As String
            Dim Flag As String
            Dim sscCode As String
            Dim sscName As String
            Dim TableName As String
            Dim Rep_Exp_Seq As String
            Dim Others As String
            'CType(Session("Exportinputdtl"), Export_File_Parametr_Details).FileName
            CFromdate = queryParams.DateFrom & " 00:00:00.000"
            CTodate = queryParams.DateTo & " 23:59:59.999"
            cName = queryParams.ColumnName
            IsCurrentUser = queryParams.CurrentUser
            userID = queryParams.UserID
            Flag = queryParams.IsActive
            sscName = Store_ID
            sscCode = Store_ID_Name

            Dim colval As String = "Id=" & Seq
            Dim rows = GetImportTableDetails().Select(colval)
            If Not IsNothing(rows) Then
                TableName = rows(0)("Table_Name").ToString()
                Rep_Exp_Seq = rows(0)("Rep_Exp_Seq").ToString()
                Others = rows(0)("Others").ToString()
                'lblTitle.Text = rows(0)("Description").ToString()
            End If

            Dim dt As DataTable = New DataTable()
            Dim qryfield As String
            qryfield = ""

            Dim dgColdtl As DataTable = GetReportFieldDetails(Rep_Exp_Seq)

            'Dim dr As DataRow = dgColdtl.NewRow
            ''Create New Row
            ''dr("Target Store") = "From Date"
            ''dr("Seq No") = hdnDateFrom.Value
            'dr("Id") = 5
            'dr("DataField") = "CRTDT"
            'dr("HeaderText") = "Created Date"
            'dr("Column_Name") = ""
            'dr("SortExpression") = "CRTDT"
            'dr("Table_Name") = TableName
            'dr("Rep_Seq") = dgColdtl.Rows(0)("Rep_Seq")

            ''dr("No of Records") = hdnDateTo.Value
            '' Set Column Value
            'dgColdtl.Rows.InsertAt(dr, 0)

            ''dr.Delete()
            'dr = dgColdtl.NewRow
            'dr("Id") = 6
            'dr("DataField") = "CRTCD"
            'dr("HeaderText") = "Created User"
            'dr("Column_Name") = ""
            'dr("SortExpression") = "CRTCD"
            'dr("Table_Name") = TableName
            'dr("Rep_Seq") = dgColdtl.Rows(0)("Rep_Seq")
            ''dr("Seq No") = "Active Flag"
            ''dr("Report Name") = If(hdnActiveFlag.Value = 0, "Active", "InActive")
            'dgColdtl.Rows.InsertAt(dr, 1)

            dgColdtl.DefaultView.Sort = "Id ASC"
            dgColdtl = dgColdtl.DefaultView.ToTable()




            For i = 0 To dgColdtl.Rows.Count - 1
                If Type = "R" Then
                    If dgColdtl.Rows(i)(3) = "" Then
                        qryfield = qryfield & dgColdtl.Rows(i)(1) & ","
                    Else
                        qryfield = qryfield & dgColdtl.Rows(i)(3) & " As " & dgColdtl.Rows(i)(1) & ","
                    End If
                ElseIf Type = "E" Then
                    If dgColdtl.Rows(i)(1).ToString().EndsWith("_Orig") = False Then
                        If dgColdtl.Rows(i)(3) = "" Then
                            qryfield = qryfield & dgColdtl.Rows(i)(1) & " As """ & dgColdtl.Rows(i)(2) & ""","
                        Else
                            qryfield = qryfield & dgColdtl.Rows(i)(3) & " As """ & dgColdtl.Rows(i)(2) & ""","
                        End If
                    End If
                End If
                'dtExport.Rows.Add(210, "Repair_Description", "Repair Description", "", "Repair_Description", "WTY_EXCEL", "2")
                'dtExport.Rows.Add(220, "Purchase_Date", "Purchase Date", "CONVERT(VARCHAR(8), Purchase_Date, 112)", "Purchase_Date_Orig", "WTY_EXCEL", "2")
                'Dim bfield As New BoundField()
                'bfield.DataField = dgColdtl.Rows(i)(1)
                'bfield.HeaderText = dgColdtl.Rows(i)(2)
                'bfield.SortExpression = dgColdtl.Rows(i)(4)
            Next
            qryfield = qryfield.Remove(qryfield.Length - 1)

            'dtImportSumDtl.Columns.AddRange(New DataColumn(5) {New DataColumn("Seq", GetType(Integer)),
            '                                New DataColumn("Target_Store", GetType(String)),
            '                                New DataColumn("Report_Name", GetType(String)),
            '                      New DataColumn("Cnt", GetType(Integer)),
            '                      New DataColumn("Create_Date_From", GetType(DateTime)),
            '                      New DataColumn("Create_Date_To", GetType(DateTime))})

            'For Each row As DataRow In GetImportTableDetails().Rows
            Dim sqlStr As String = "SELECT "
            sqlStr = sqlStr & ""



            'Dim sqlStr As String = "SELECT " & qryfield
            'sqlStr = sqlStr & " ServiceOrder_No,LastUpdt_user,Billinb_user,LEFT(CONVERT(VARCHAR, Billing_date, 103), 10) Billing_date,"
            'sqlStr = sqlStr & "GoodsDelivered_date,Branch_name,Engineer,Product_1 'Product_Code',Product_2 'Product_Name',IW_Labor,IW_Parts,IW_Transport,IW_Others,IW_Tax,IW_total,OW_Labor,"
            'sqlStr = sqlStr & "OW_Parts,OW_Transport,OW_Others,OW_Tax,OW_total,Upload_FileName  "
            'sqlStr = sqlStr & " FROM " & dgColdtl.Rows(1)(5) & " WHERE DELFG=0 "

            If Seq = 1 Then
                sqlStr = sqlStr & "case when isnull(UPDDT,'') = '' then CRTDT else UPDDT end As CRTDT,case when isnull(UPDCD,'') = '' then CRTCD else UPDCD end As CRTCD," & qryfield
                'sqlStr = sqlStr & " count(*) Count From " & row.Item(1) & " where  ((CONVERT(DATE, " & cName & ") between  "
                'sqlStr = sqlStr & " count(*) Cnt,Min(case when isnull(upddt,'') = '' then crtdt else upddt end) As 'Create_Date_From', "
                'sqlStr = sqlStr & "Max(case when isnull(upddt,'') = '' then crtdt else upddt end) As 'Create_Date_To' "
                sqlStr = sqlStr & " From " & TableName & " where  ((" & cName & " between  "
                sqlStr = sqlStr & "'" & CFromdate & "' and '" & CTodate & "'"
                If IsCurrentUser = True Then
                    sqlStr = sqlStr & "  and CRTCD = '" & userID & "'"
                End If
                'sqlStr = sqlStr & ") or (CONVERT(DATE, CRTDT) between '" & CFromdate & "' and '" & CTodate & "'"
                sqlStr = sqlStr & ") or (UPDDT between '" & CFromdate & "' and '" & CTodate & "'"
                If IsCurrentUser = True Then
                    sqlStr = sqlStr & "  and UPDCD = '" & userID & "'"
                End If
                sqlStr = sqlStr & ")) and Branch_name in ('" & sscName & "') "

                sqlStr = sqlStr & "and DELFG in (" & Flag & ") Order by CRTDT"

            ElseIf Seq = 2 Then
                sqlStr = sqlStr & "case when isnull(UPDDT,'') = '' then CRTDT else UPDDT end As CRTDT,case when isnull(UPDCD,'') = '' then CRTCD else UPDCD end As CRTCD," & qryfield
                'sqlStr = sqlStr & " count(*) Count From " & row.Item(1) & " where  ((CONVERT(DATE, " & cName & ") between  "
                'sqlStr = sqlStr & " count(*) Cnt,Min(case when isnull(upddt,'') = '' then crtdt else upddt end) As 'Create_Date_From', "
                'sqlStr = sqlStr & "Max(case when isnull(upddt,'') = '' then crtdt else upddt end) As 'Create_Date_To' "
                sqlStr = sqlStr & " From " & TableName & " where  (( " & cName & " between  "
                sqlStr = sqlStr & "'" & CFromdate & "' and '" & CTodate & "'"
                If IsCurrentUser = True Then
                    sqlStr = sqlStr & "  and CRTCD = '" & userID & "'"
                End If
                'sqlStr = sqlStr & ") or (CONVERT(DATE, CRTDT) between '" & CFromdate & "' and '" & CTodate & "'"
                sqlStr = sqlStr & ") or (UPDDT between '" & CFromdate & "' and '" & CTodate & "'"
                If IsCurrentUser = True Then
                    sqlStr = sqlStr & "  and UPDCD = '" & userID & "'"
                End If
                sqlStr = sqlStr & ")) and Upload_Branch in ('" & sscName & "') "

                sqlStr = sqlStr & "and DELFG in (" & Flag & ")"

            ElseIf Seq = 3 Or Seq = 4 Or Seq = 5 Then
                sqlStr = sqlStr & "case when isnull(UPDDT,'') = '' then CRTDT else UPDDT end As CRTDT,case when isnull(UPDCD,'') = '' then CRTCD else UPDCD end As CRTCD," & qryfield
                'sqlStr = sqlStr & " count(*) Count From " & row.Item(1) & " where  ((CONVERT(DATE, " & cName & ") between  "
                'sqlStr = sqlStr & " count(*) Cnt,Min(case when isnull(upddt,'') = '' then crtdt else upddt end) As 'Create_Date_From', "
                'sqlStr = sqlStr & "Max(case when isnull(upddt,'') = '' then crtdt else upddt end) As 'Create_Date_To' "
                sqlStr = sqlStr & " From " & TableName & " where number ='" & Others & "' "
                sqlStr = sqlStr & "And (( " & cName & " between  "
                sqlStr = sqlStr & "'" & CFromdate & "' and '" & CTodate & "'"
                If IsCurrentUser = True Then
                    sqlStr = sqlStr & "  and CRTCD = '" & userID & "'"
                End If
                'sqlStr = sqlStr & ") or (CONVERT(DATE, CRTDT) between '" & CFromdate & "' and '" & CTodate & "'"
                sqlStr = sqlStr & ") or (UPDDT between '" & CFromdate & "' and '" & CTodate & "'"
                If IsCurrentUser = True Then
                    sqlStr = sqlStr & "  and UPDCD = '" & userID & "'"
                End If
                sqlStr = sqlStr & ")) and Upload_Branch in ('" & sscName & "') "

                sqlStr = sqlStr & "and DELFG in (" & Flag & ")"

            ElseIf Seq = 6 Or Seq = 7 Or Seq = 13 Or Seq = 14 Or Seq = 15 Then
                sqlStr = sqlStr & " " & TableName & ".CRTDT," & TableName & ".CRTCD," & qryfield
                'sqlStr = sqlStr & " count(*) Count From " & row.Item(1) & " where  ((CONVERT(DATE, " & cName & ") between  "
                'sqlStr = sqlStr & " count(*) Cnt,Min(crtdt) As 'Create_Date_From',Max(crtdt) As 'Create_Date_To' "
                sqlStr = sqlStr & " From " & TableName & ""
                sqlStr = sqlStr & " Left Join M_ship_base on convert(int,M_ship_base.ship_code) = convert(int," & TableName & ""
                sqlStr = sqlStr & ".SHIP_TO_BRANCH_CODE) where " & TableName & "." & cName & " between  "
                sqlStr = sqlStr & "'" & CFromdate & "' and '" & CTodate & "'"
                If IsCurrentUser = True Then
                    sqlStr = sqlStr & "  and CRTCD = '" & userID & "'"
                End If
                'sqlStr = sqlStr & ") or (CONVERT(DATE, CRTDT) between '" & CFromdate & "' and '" & CTodate & "'"
                'sqlStr = sqlStr & ") or (UPDDT between '" & CFromdate & "' and '" & CTodate & "'"
                ' If IsCurrentUser = True Then
                'sqlStr = sqlStr & "  and UPDCD = '" & userID & "'"
                'End If
                'sqlStr = sqlStr & " and convert(int," & row.Item(1) & "." & sscCode & ") "
                sqlStr = sqlStr & " and convert(int," & TableName & ".SHIP_TO_BRANCH_CODE) In ('" & sscCode & "') "
                sqlStr = sqlStr & "And " & TableName & ".DELFG In (" & Flag & ")"

                If Seq = 7 Then
                    sqlStr = sqlStr.Replace("PO_NO", "PR_DETAIL.PO_NO")
                End If
            ElseIf Seq = 8 Or Seq = 9 Or Seq = 10 Or Seq = 11 Or
                    Seq = 12 Or Seq = 16 Or Seq = 17 Or
                     Seq = 18 Or Seq = 19 Or Seq = 20 Then
                sqlStr = sqlStr & "CRTDT,CRTCD," & qryfield
                'sqlStr = sqlStr & " count(*) Count From " & row.Item(1) & " where  ((CONVERT(DATE, " & cName & ") between  "
                'sqlStr = sqlStr & " count(*) Cnt,Min(crtdt) As 'Create_Date_From',Max(crtdt) As 'Create_Date_To' "
                sqlStr = sqlStr & " From " & TableName & " where " & cName & " between  "
                sqlStr = sqlStr & "'" & CFromdate & "' and '" & CTodate & "'"
                If IsCurrentUser = True Then
                    sqlStr = sqlStr & "  and CRTCD = '" & userID & "'"
                End If
                'sqlStr = sqlStr & ") or (CONVERT(DATE, CRTDT) between '" & CFromdate & "' and '" & CTodate & "'"
                'sqlStr = sqlStr & ") or (UPDDT between '" & CFromdate & "' and '" & CTodate & "'"
                'If IsCurrentUser = True Then
                '    sqlStr = sqlStr & "  and UPDCD = '" & userID & "'"
                'End If
                sqlStr = sqlStr & " and Ship_to_Branch in ('" & sscName & "') "

                sqlStr = sqlStr & "and DELFG in (" & Flag & ")"

                'bharathiraja-3
            ElseIf Seq = 21 Then
                sqlStr = ""
                sqlStr = "SET LANGUAGE us_english; SELECT "
                sqlStr = sqlStr & " " & TableName & ".CRTDT," & TableName & ".CRTCD," & qryfield
                sqlStr = sqlStr & " From " & TableName & ""
                sqlStr = sqlStr & " Left Join M_ship_base on convert(int,M_ship_base.ship_code) = convert(int," & TableName & ""
                sqlStr = sqlStr & ".location) where " & TableName & "." & cName & " between  "
                sqlStr = sqlStr & "'" & CFromdate & "' and '" & CTodate & "'"
                If IsCurrentUser = True Then
                    sqlStr = sqlStr & "  and CRTCD = '" & userID & "'"
                End If
                sqlStr = sqlStr & " and convert(int," & TableName & ".location) In ('" & sscCode & "') "
                sqlStr = sqlStr & "And " & TableName & ".DELFG In (" & Flag & ")"
                'bharathiraja-PO CONFIRMATION-3
            ElseIf Seq = 22 Then
                sqlStr = sqlStr & "CRTDT,CRTCD," & qryfield
                sqlStr = sqlStr & " From " & TableName & " where " & cName & " between  "
                sqlStr = sqlStr & "'" & CFromdate & "' and '" & CTodate & "'"
                If IsCurrentUser = True Then
                    sqlStr = sqlStr & "  and CRTCD = '" & userID & "'"
                End If
                sqlStr = sqlStr & " and Ship_to_Branch in ('" & sscName & "') "

                sqlStr = sqlStr & "and DELFG in (" & Flag & ")"
            End If

            Dim _DataTable As DataTable = dbConn.GetDataSet(sqlStr)

            If _DataTable IsNot Nothing Then
                'dtImportSumDtl.a = _DataTable.Copy()

                dtImportSumDtl.Merge(_DataTable)




                'dtImportSumDtl = _DataTable.Copy()
            End If

            'sqlStr = sqlStr & " '" & row.Item(0) & "'As Seq,"
            'For Each column As DataColumn In GetImportTableDetails().Columns
            'Console.WriteLine(row(column))
            'Next column
            'Next row
            dbConn.CloseConnection()
            'For Each (DataRow)
            '        foreach(DataRow, row, in, dr_art_line_2.Rows);
            '{
            '    QuantityInIssueUnit_value = Convert.ToInt32(row, "columnname")
            '}
            Dim dvimpdtl As New DataView(dtImportSumDtl)
            'DataView dv = New DataView(dtImportSumDtl)
            dvimpdtl.Sort = "CRTDT DESC"

            'dtImportSumDtl.DefaultView.Sort = "Target_Store ASC ,Seq ASC "
            'Return dtImportSumDtl
            Return dvimpdtl.ToTable()
        Catch ex As Exception
            Dim msg As String = ex.Message
            'lblMsg.Text = ex.Message
            'Dim msg As String = cc.showMsg("")
            'ClientScript.RegisterStartupScript(Me.GetType(), "startup", msg, True)
            'Exit Sub
        End Try

    End Function

    Public Function GetReportFieldDetails(ByVal RepID As String) As DataTable
        Dim dtExport As New DataTable()

        dtExport.Columns.AddRange(New DataColumn(6) {New DataColumn("Id", GetType(Integer)), New DataColumn("DataField", GetType(String)),
                                  New DataColumn("HeaderText", GetType(String)), New DataColumn("Column_Name", GetType(String)),
                                  New DataColumn("SortExpression", GetType(String)), New DataColumn("Table_Name", GetType(String)),
                                  New DataColumn("Rep_Seq", GetType(String))})
        If RepID = "1" Then

            dtExport.Rows.Add(10, "ServiceOrder_No", "Service Order No", "", "ServiceOrder_No", "SC_DSR", "1")
            dtExport.Rows.Add(20, "LastUpdt_user", "Last Updated User", "", "LastUpdt_user", "SC_DSR", "1")
            dtExport.Rows.Add(30, "Billinb_user", "Billing User", "", "Billinb_user", "SC_DSR", "1")
            dtExport.Rows.Add(40, "Billing_date", "Billing Date", "LEFT(CONVERT(VARCHAR, Billing_date, 103), 10)", "Billing_date_Orig", "SC_DSR", "1")
            dtExport.Rows.Add(45, "Billing_date_Orig", "Billing date Orig", "Billing_date", "Billing_date_Orig", "SC_DSR", "1")
            dtExport.Rows.Add(50, "GoodsDelivered_date", "Goods Delivered Date", "", "GoodsDelivered_date_Orig", "SC_DSR", "1")
            dtExport.Rows.Add(55, "GoodsDelivered_date_Orig", "GoodsDelivered date Orig", "convert (datetime, case when ISDATE(GoodsDelivered_date) = 1 then GoodsDelivered_date else '1900-01-01 00:00:00.000' end)", "GoodsDelivered_date_Orig", "SC_DSR", "1")
            dtExport.Rows.Add(60, "Branch_name", "Branch Name", "", "Branch_name", "SC_DSR", "1")
            dtExport.Rows.Add(70, "Engineer", "Engineer", "", "Engineer", "SC_DSR", "1")
            dtExport.Rows.Add(80, "Product_Code", "Product Code", "Product_1", "Product_Code", "SC_DSR", "1")
            dtExport.Rows.Add(90, "Product_Name", "Product Name", "Product_2", "Product_Name", "SC_DSR", "1")
            dtExport.Rows.Add(100, "IW_Labor", "IW_Labor", "CAST(IW_Labor AS decimal(12,2))", "IW_Labor", "SC_DSR", "1")
            dtExport.Rows.Add(110, "IW_Parts", "IW_Parts", "CAST(IW_Parts AS decimal(12,2))", "IW_Parts", "SC_DSR", "1")
            dtExport.Rows.Add(120, "IW_Transport", "IW_Transport", "CAST(IW_Transport AS decimal(12,2))", "IW_Transport", "SC_DSR", "1")
            dtExport.Rows.Add(130, "IW_Others", "IW_Others", "CAST(IW_Others AS decimal(12,2))", "IW_Others", "SC_DSR", "1")
            dtExport.Rows.Add(140, "IW_Tax", "IW_Tax", "CAST(IW_Tax AS decimal(12,2))", "IW_Tax", "SC_DSR", "1")
            dtExport.Rows.Add(150, "IW_total", "IW_total", "CAST(IW_total AS decimal(12,2))", "IW_total", "SC_DSR", "1")
            dtExport.Rows.Add(160, "OW_Labor", "OW_Labor", "CAST(OW_Labor AS decimal(12,2))", "OW_Labor", "SC_DSR", "1")
            dtExport.Rows.Add(170, "OW_Parts", "OW_Parts", "CAST(OW_Parts AS decimal(12,2))", "OW_Parts", "SC_DSR", "1")
            dtExport.Rows.Add(180, "OW_Transport", "OW_Transport", "CAST(OW_Transport AS decimal(12,2))", "OW_Transport", "SC_DSR", "1")
            dtExport.Rows.Add(190, "OW_Others", "OW_Others", "CAST(OW_Others AS decimal(12,2))", "OW_Others", "SC_DSR", "1")
            dtExport.Rows.Add(200, "OW_total", "OW_total", "CAST(OW_total AS decimal(12,2))", "OW_total", "SC_DSR", "1")
            'dtExport.Rows.Add(210, "Upload_FileName", "Upload_FileName", "", "Upload_FileName", "SC_DSR", "1")
        ElseIf RepID = "2" Then
            dtExport.Rows.Add(10, "ASC_Code", "ASC Code", "", "ASC_Code", "WTY_EXCEL", "2")
            dtExport.Rows.Add(20, "Branch_Code", "Branch Code", "", "Branch_Code", "WTY_EXCEL", "2")
            dtExport.Rows.Add(30, "ASC_Claim_No", "ASC Claim No", "", "ASC_Claim_No", "WTY_EXCEL", "2")
            dtExport.Rows.Add(40, "Samsung_Claim_No", "Samsung Claim No", "", "Samsung_Claim_No", "WTY_EXCEL", "2")
            dtExport.Rows.Add(50, "Service_Type", "Service Type", "", "Service_Type", "WTY_EXCEL", "2")
            dtExport.Rows.Add(60, "Consumer_Name", "Consumer Name", "", "Consumer_Name", "WTY_EXCEL", "2")
            dtExport.Rows.Add(70, "Consumer_Addr1", "Consumer Addr1", "", "Consumer_Addr1", "WTY_EXCEL", "2")
            dtExport.Rows.Add(80, "Consumer_Addr2", "Consumer Addr2", "", "Consumer_Addr2", "WTY_EXCEL", "2")
            dtExport.Rows.Add(90, "Consumer_Telephone", "Consumer Telephone", "", "Consumer_Telephone", "WTY_EXCEL", "2")
            dtExport.Rows.Add(100, "Consumer_Fax", "Consumer Fax", "", "Consumer_Fax", "WTY_EXCEL", "2")
            dtExport.Rows.Add(110, "Postal_Code", "Postal Code", "", "Postal_Code", "WTY_EXCEL", "2")
            dtExport.Rows.Add(120, "Model", "Model", "", "Model", "WTY_EXCEL", "2")
            dtExport.Rows.Add(130, "Serial_No", "Serial No", "", "Serial_No", "WTY_EXCEL", "2")
            dtExport.Rows.Add(140, "IMEI_No", "IMEI No", "", "IMEI_No", "WTY_EXCEL", "2")
            dtExport.Rows.Add(150, "Defect_Type", "Defect Type", "", "Defect_Type", "WTY_EXCEL", "2")
            dtExport.Rows.Add(160, "Condition", "Condition", "", "Condition", "WTY_EXCEL", "2")
            dtExport.Rows.Add(170, "Symptom", "Symptom", "", "Symptom", "WTY_EXCEL", "2")
            dtExport.Rows.Add(180, "Defect_Code", "Defect Code", "", "Defect_Code", "WTY_EXCEL", "2")
            dtExport.Rows.Add(190, "Repair_Code", "Repair Code", "", "Repair_Code", "WTY_EXCEL", "2")
            dtExport.Rows.Add(200, "Defect_Desc", "Defect Desc", "", "Defect_Desc", "WTY_EXCEL", "2")
            dtExport.Rows.Add(210, "Repair_Description", "Repair Description", "", "Repair_Description", "WTY_EXCEL", "2")
            dtExport.Rows.Add(220, "Purchase_Date", "Purchase Date", "CONVERT(VARCHAR(8), Purchase_Date, 112)", "Purchase_Date_Orig", "WTY_EXCEL", "2")
            dtExport.Rows.Add(225, "Purchase_Date_Orig", "Purchase Date Orig", "Purchase_Date", "Purchase_Date_Orig", "WTY_EXCEL", "2")
            dtExport.Rows.Add(230, "Repair_Received_Date", "Repair Received Date", "CONVERT(VARCHAR(8), Repair_Received_Date, 112)", "Repair_Received_Date_Orig", "WTY_EXCEL", "2")
            dtExport.Rows.Add(235, "Repair_Received_Date_Orig", "Repair Received Date Orig", "Repair_Received_Date", "Repair_Received_Date_Orig", "WTY_EXCEL", "2")
            dtExport.Rows.Add(240, "Completed_Date", "Completed Date", "CONVERT(VARCHAR(8), Completed_Date, 112)", "Completed_Date_Orig", "WTY_EXCEL", "2")
            dtExport.Rows.Add(245, "Completed_Date_Orig", "Completed Date Orig", "Completed_Date", "Completed_Date_Orig", "", "")
            dtExport.Rows.Add(250, "Delivery_Date", "Delivery Date", "CONVERT(VARCHAR(8), Delivery_Date, 112)", "Delivery_Date_Orig", "WTY_EXCEL", "2")
            dtExport.Rows.Add(255, "Delivery_Date_Orig", "Delivery Date Orig", "Delivery_Date", "Delivery_Date_Orig", "WTY_EXCEL", "2")
            dtExport.Rows.Add(260, "Production_Date", "Production Date", "CONVERT(VARCHAR(8), Production_Date, 112)", "Production_Date_Orig", "WTY_EXCEL", "2")
            dtExport.Rows.Add(265, "Production_Date_Orig", "Production Date Orig", "Production_Date", "Production_Date_Orig", "", "")
            'dtExport.Rows.Add(270, "Labor_Amount_I", "", "CAST(Labor_Amount_I AS decimal(12,2))", "", "WTY_EXCEL", "2")
            dtExport.Rows.Add(280, "Labor_Amount", "Labor Amount", "CAST(Labor_Amount AS decimal(12,2))", "Labor_Amount", "WTY_EXCEL", "2")
            'dtExport.Rows.Add(290, "Parts_Amount_I", "", "CAST(Parts_Amount_I AS decimal(12,2))", "", "WTY_EXCEL", "2")
            dtExport.Rows.Add(300, "Parts_Amount", "Parts Amount", "CAST(Parts_Amount AS decimal(12,2))", "Parts_Amount", "WTY_EXCEL", "2")
            dtExport.Rows.Add(310, "Freight", "Freight", "CAST(Freight AS decimal(12,2))", "Freight", "WTY_EXCEL", "2")
            dtExport.Rows.Add(320, "Other", "Other", "CAST(Other AS decimal(12,2))", "Other", "WTY_EXCEL", "2")
            dtExport.Rows.Add(330, "Parts_SGST", "Parts SGST", "CAST(Parts_SGST AS decimal(12,2))", "Parts_SGST", "WTY_EXCEL", "2")
            dtExport.Rows.Add(340, "Parts_UTGST", "Parts UTGST", "CAST(Parts_UTGST AS decimal(12,2))", "Parts_UTGST", "WTY_EXCEL", "2")
            dtExport.Rows.Add(350, "Parts_CGST", "Parts CGST", "CAST(Parts_CGST AS decimal(12,2))", "Parts_CGST", "WTY_EXCEL", "2")
            dtExport.Rows.Add(360, "Parts_IGST", "Parts IGST", "CAST(Parts_IGST AS decimal(12,2))", "Parts_IGST", "WTY_EXCEL", "2")
            dtExport.Rows.Add(370, "Parts_Cess", "Parts Cess", "CAST(Parts_Cess AS decimal(12,2))", "Parts_Cess", "WTY_EXCEL", "2")
            dtExport.Rows.Add(380, "SGST", "SGST", "CAST(SGST AS decimal(12,2))", "SGST", "WTY_EXCEL", "2")
            dtExport.Rows.Add(390, "UTGST", "UTGST", "CAST(UTGST AS decimal(12,2))", "UTGST", "WTY_EXCEL", "2")
            dtExport.Rows.Add(400, "CGST", "CGST", "CAST(CGST AS decimal(12,2))", "CGST", "WTY_EXCEL", "2")
            dtExport.Rows.Add(410, "IGST", "IGST", "CAST(IGST AS decimal(12,2))", "IGST", "WTY_EXCEL", "2")
            dtExport.Rows.Add(420, "Cess", "Cess", "CAST(Cess AS decimal(12,2))", "Cess", "WTY_EXCEL", "2")
            'dtExport.Rows.Add(430, "Total_Invoice_Amount_I", "", "CAST(Total_Invoice_Amount_I AS decimal(12,2))", "", "WTY_EXCEL", "2")
            dtExport.Rows.Add(440, "Total_Invoice_Amount", "Total Invoice Amount", "CAST(Total_Invoice_Amount AS decimal(12,2))", "Total_Invoice_Amount", "WTY_EXCEL", "2")
            'dtExport.Rows.Add(450, "InvoiceNo_Final", "", "", "", "WTY_EXCEL", "2")
            'dtExport.Rows.Add(460, "SGST_total", "", "CAST(SGST_total AS decimal(12,2))", "", "WTY_EXCEL", "2")
            'dtExport.Rows.Add(470, "CGST_total", "", "CAST(CGST_total AS decimal(12,2))", "", "WTY_EXCEL", "2")
            'dtExport.Rows.Add(480, "IGST_total", "", "CAST(IGST_total AS decimal(12,2))", "", "WTY_EXCEL", "2")
            'dtExport.Rows.Add(490, "tax_total", "", "CAST(tax_total AS decimal(12,2))", "", "WTY_EXCEL", "2")
            dtExport.Rows.Add(500, "Remark", "Remark", "", "Remark", "WTY_EXCEL", "2")
            dtExport.Rows.Add(510, "Tr_No", "Tr No", "", "Tr_No", "WTY_EXCEL", "2")
            dtExport.Rows.Add(520, "Tr_Type", "Tr Type", "", "Tr_Type", "WTY_EXCEL", "2")
            dtExport.Rows.Add(530, "Status", "Status", "", "Status", "WTY_EXCEL", "2")
            dtExport.Rows.Add(540, "Engineer", "Engineer", "", "Engineer", "WTY_EXCEL", "2")
            dtExport.Rows.Add(550, "Collection_Point", "Collection Point", "", "Collection_Point", "WTY_EXCEL", "2")
            dtExport.Rows.Add(560, "Collection_Point_Name", "Collection Point Name", "", "Collection_Point_Name", "WTY_EXCEL", "2")
            dtExport.Rows.Add(570, "Location_1", "Location-1", "", "Location_1", "WTY_EXCEL", "2")
            dtExport.Rows.Add(580, "Part_1", "Part-1", "", "Part_1", "WTY_EXCEL", "2")
            dtExport.Rows.Add(590, "Qty_1", "Qty-1", "", "Qty_1", "WTY_EXCEL", "2")
            dtExport.Rows.Add(600, "Unit_Price_1", "Unit Price-1", "CAST(Unit_Price_1 AS decimal(12,2))", "Unit_Price_1", "WTY_EXCEL", "2")
            dtExport.Rows.Add(610, "Doc_Num_1", "Doc Num-1", "", "Doc_Num_1", "WTY_EXCEL", "2")
            dtExport.Rows.Add(620, "Matrial_Serial_1", "Matrial Serial-1", "", "Matrial_Serial_1", "WTY_EXCEL", "2")
            dtExport.Rows.Add(630, "Location_2", "Location-2", "", "Location_2", "WTY_EXCEL", "2")
            dtExport.Rows.Add(640, "Part_2", "Part-2", "", "Part_2", "WTY_EXCEL", "2")
            dtExport.Rows.Add(650, "Qty_2", "Qty-2", "", "Qty_2", "WTY_EXCEL", "2")
            dtExport.Rows.Add(660, "Unit_Price_2", "Unit Price-2", "CAST(Unit_Price_2 AS decimal(12,2))", "Unit_Price_2", "WTY_EXCEL", "2")
            dtExport.Rows.Add(670, "Doc_Num_2", "Doc Num-2", "", "Doc_Num_2", "WTY_EXCEL", "2")
            dtExport.Rows.Add(680, "Matrial_Serial_2", "Matrial Serial-2", "", "Matrial_Serial_2", "WTY_EXCEL", "2")
            dtExport.Rows.Add(690, "Location_3", "Location-3", "", "Location_3", "WTY_EXCEL", "2")
            dtExport.Rows.Add(700, "Part_3", "Part-3", "", "Part_3", "WTY_EXCEL", "2")
            dtExport.Rows.Add(710, "Qty_3", "Qty-3", "", "Qty_3", "WTY_EXCEL", "2")
            dtExport.Rows.Add(720, "Unit_Price_3", "Unit Price-3", "CAST(Unit_Price_3 AS decimal(12,2))", "Unit_Price_3", "WTY_EXCEL", "2")
            dtExport.Rows.Add(730, "Doc_Num_3", "Doc Num-3", "", "Doc_Num_3", "WTY_EXCEL", "2")
            dtExport.Rows.Add(740, "Matrial_Serial_3", "Matrial Serial-3", "", "Matrial_Serial_3", "WTY_EXCEL", "2")
            dtExport.Rows.Add(750, "Location_4", "Location-4", "", "Location_4", "WTY_EXCEL", "2")
            dtExport.Rows.Add(760, "Part_4", "Part-4", "", "Part_4", "WTY_EXCEL", "2")
            dtExport.Rows.Add(770, "Qty_4", "Qty-4", "", "Qty_4", "WTY_EXCEL", "2")
            dtExport.Rows.Add(780, "Unit_Price_4", "Unit Price-4", "CAST(Unit_Price_4 AS decimal(12,2))", "Unit_Price_4", "WTY_EXCEL", "2")
            dtExport.Rows.Add(790, "Doc_Num_4", "Doc Num-4", "", "Doc_Num_4", "WTY_EXCEL", "2")
            dtExport.Rows.Add(800, "Matrial_Serial_4", "Matrial Serial-4", "", "Matrial_Serial_4", "WTY_EXCEL", "2")
            dtExport.Rows.Add(810, "Location_5", "Location-5", "", "Location_5", "WTY_EXCEL", "2")
            dtExport.Rows.Add(820, "Part_5", "Part-5", "", "Part_5", "WTY_EXCEL", "2")
            dtExport.Rows.Add(830, "Qty_5", "Qty-5", "", "Qty_5", "WTY_EXCEL", "2")
            dtExport.Rows.Add(840, "Unit_Price_5", "Unit Price-5", "CAST(Unit_Price_5 AS decimal(12,2))", "Unit_Price_5", "WTY_EXCEL", "2")
            dtExport.Rows.Add(850, "Doc_Num_5", "Doc Num-5", "", "Doc_Num_5", "WTY_EXCEL", "2")
            dtExport.Rows.Add(860, "Matrial_Serial_5", "Matrial Serial-5", "", "Matrial_Serial_5", "WTY_EXCEL", "2")
            dtExport.Rows.Add(870, "Location_6", "Location-6", "", "Location_6", "WTY_EXCEL", "2")
            dtExport.Rows.Add(880, "Part_6", "Part-6", "", "Part_6", "WTY_EXCEL", "2")
            dtExport.Rows.Add(890, "Qty_6", "Qty-6", "", "Qty_6", "WTY_EXCEL", "2")
            dtExport.Rows.Add(900, "Unit_Price_6", "Unit Price-6", "CAST(Unit_Price_6 AS decimal(12,2))", "Unit_Price_6", "WTY_EXCEL", "2")
            dtExport.Rows.Add(910, "Doc_Num_6", "Doc Num-6", "", "Doc_Num_6", "WTY_EXCEL", "2")
            dtExport.Rows.Add(920, "Matrial_Serial_6", "Matrial Serial-6", "", "Matrial_Serial_6", "WTY_EXCEL", "2")
            dtExport.Rows.Add(930, "Location_7", "Location-7", "", "Location_7", "WTY_EXCEL", "2")
            dtExport.Rows.Add(940, "Part_7", "Part-7", "", "Part_7", "WTY_EXCEL", "2")
            dtExport.Rows.Add(950, "Qty_7", "Qty-7", "", "Qty_7", "WTY_EXCEL", "2")
            dtExport.Rows.Add(960, "Unit_Price_7", "Unit Price-7", "CAST(Unit_Price_7 AS decimal(12,2))", "Unit_Price_7", "WTY_EXCEL", "2")
            dtExport.Rows.Add(970, "Doc_Num_7", "Doc Num-7", "", "Doc_Num_7", "WTY_EXCEL", "2")
            dtExport.Rows.Add(980, "Matrial_Serial_7", "Matrial Serial-7", "", "Matrial_Serial_7", "WTY_EXCEL", "2")
            dtExport.Rows.Add(990, "Location_8", "Location-8", "", "Location_8", "WTY_EXCEL", "2")
            dtExport.Rows.Add(1000, "Part_8", "Part-8", "", "Part_8", "WTY_EXCEL", "2")
            dtExport.Rows.Add(1010, "Qty_8", "Qty-8", "", "Qty_8", "WTY_EXCEL", "2")
            dtExport.Rows.Add(1020, "Unit_Price_8", "Unit Price-8", "CAST(Unit_Price_8 AS decimal(12,2))", "Unit_Price_8", "WTY_EXCEL", "2")
            dtExport.Rows.Add(1030, "Doc_Num_8", "Doc Num-8", "", "Doc_Num_8", "WTY_EXCEL", "2")
            dtExport.Rows.Add(1040, "Matrial_Serial_8", "Matrial Serial-8", "", "Matrial_Serial_8", "WTY_EXCEL", "2")
            dtExport.Rows.Add(1050, "Location_9", "Location-9", "", "Location_9", "WTY_EXCEL", "2")
            dtExport.Rows.Add(1060, "Part_9", "Part-9", "", "Part_9", "WTY_EXCEL", "2")
            dtExport.Rows.Add(1070, "Qty_9", "Qty-9", "", "Qty_9", "WTY_EXCEL", "2")
            dtExport.Rows.Add(1080, "Unit_Price_9", "Unit Price-9", "CAST(Unit_Price_9 AS decimal(12,2))", "Unit_Price_9", "WTY_EXCEL", "2")
            dtExport.Rows.Add(1090, "Doc_Num_9", "Doc Num-9", "", "Doc_Num_9", "WTY_EXCEL", "2")
            dtExport.Rows.Add(1100, "Matrial_Serial_9", "Matrial Serial-9", "", "Matrial_Serial_9", "WTY_EXCEL", "2")
            dtExport.Rows.Add(1110, "Location_10", "Location-10", "", "Location_10", "WTY_EXCEL", "2")
            dtExport.Rows.Add(1120, "Part_10", "Part-10", "", "Part_10", "WTY_EXCEL", "2")
            dtExport.Rows.Add(1130, "Qty_10", "Qty-10", "", "Qty_10", "WTY_EXCEL", "2")
            dtExport.Rows.Add(1140, "Unit_Price_10", "Unit Price-10", "CAST(Unit_Price_10 AS decimal(12,2))", "Unit_Price_10", "WTY_EXCEL", "2")
            dtExport.Rows.Add(1150, "Doc_Num_10", "Doc Num-10", "", "Doc_Num_10", "WTY_EXCEL", "2")
            dtExport.Rows.Add(1160, "Matrial_Serial_10", "Matrial Serial-10", "", "Matrial_Serial_10", "WTY_EXCEL", "2")
            dtExport.Rows.Add(1170, "Location_11", "Location-11", "", "Location_11", "WTY_EXCEL", "2")
            dtExport.Rows.Add(1180, "Part_11", "Part-11", "", "Part_11", "WTY_EXCEL", "2")
            dtExport.Rows.Add(1190, "Qty_11", "Qty-11", "", "Qty_11", "WTY_EXCEL", "2")
            dtExport.Rows.Add(1200, "Unit_Price_11", "Unit Price-11", "CAST(Unit_Price_11 AS decimal(12,2))", "Unit_Price_11", "WTY_EXCEL", "2")
            dtExport.Rows.Add(1210, "Doc_Num_11", "Doc Num-11", "", "Doc_Num_11", "WTY_EXCEL", "2")
            dtExport.Rows.Add(1220, "Matrial_Serial_11", "Matrial Serial-11", "", "Matrial_Serial_11", "WTY_EXCEL", "2")
            dtExport.Rows.Add(1230, "Location_12", "Location-12", "", "Location_12", "WTY_EXCEL", "2")
            dtExport.Rows.Add(1240, "Part_12", "Part-12", "", "Part_12", "WTY_EXCEL", "2")
            dtExport.Rows.Add(1250, "Qty_12", "Qty-12", "", "Qty_12", "WTY_EXCEL", "2")
            dtExport.Rows.Add(1260, "Unit_Price_12", "Unit Price-12", "CAST(Unit_Price_12 AS decimal(12,2))", "Unit_Price_12", "WTY_EXCEL", "2")
            dtExport.Rows.Add(1270, "Doc_Num_12", "Doc Num-12", "", "Doc_Num_12", "WTY_EXCEL", "2")
            dtExport.Rows.Add(1280, "Matrial_Serial_12", "Matrial Serial-12", "", "Matrial_Serial_12", "WTY_EXCEL", "2")
            dtExport.Rows.Add(1290, "Location_13", "Location-13", "", "Location_13", "WTY_EXCEL", "2")
            dtExport.Rows.Add(1300, "Part_13", "Part-13", "", "Part_13", "WTY_EXCEL", "2")
            dtExport.Rows.Add(1310, "Qty_13", "Qty-13", "", "Qty_13", "WTY_EXCEL", "2")
            dtExport.Rows.Add(1320, "Unit_Price_13", "Unit Price-13", "CAST(Unit_Price_13 AS decimal(12,2))", "Unit_Price_13", "WTY_EXCEL", "2")
            dtExport.Rows.Add(1330, "Doc_Num_13", "Doc Num-13", "", "Doc_Num_13", "WTY_EXCEL", "2")
            dtExport.Rows.Add(1340, "Matrial_Serial_13", "Matrial Serial-13", "", "Matrial_Serial_13", "WTY_EXCEL", "2")
            dtExport.Rows.Add(1350, "Location_14", "Location-14", "", "Location_14", "WTY_EXCEL", "2")
            dtExport.Rows.Add(1360, "Part_14", "Part-14", "", "Part_14", "WTY_EXCEL", "2")
            dtExport.Rows.Add(1370, "Qty_14", "Qty-14", "", "Qty_14", "WTY_EXCEL", "2")
            dtExport.Rows.Add(1380, "Unit_Price_14", "Unit Price-14", "CAST(Unit_Price_14 AS decimal(12,2))", "Unit_Price_14", "WTY_EXCEL", "2")
            dtExport.Rows.Add(1390, "Doc_Num_14", "Doc Num-14", "", "Doc_Num_14", "WTY_EXCEL", "2")
            dtExport.Rows.Add(1400, "Matrial_Serial_14", "Matrial Serial-14", "", "Matrial_Serial_14", "WTY_EXCEL", "2")
            dtExport.Rows.Add(1410, "Location_15", "Location-15", "", "Location_15", "WTY_EXCEL", "2")
            dtExport.Rows.Add(1420, "Part_15", "Part-15", "", "Part_15", "WTY_EXCEL", "2")
            dtExport.Rows.Add(1430, "Qty_15", "Qty-15", "", "Qty_15", "WTY_EXCEL", "2")
            dtExport.Rows.Add(1440, "Unit_Price_15", "Unit Price-15", "CAST(Unit_Price_15 AS decimal(12,2))", "Unit_Price_15", "WTY_EXCEL", "2")
            dtExport.Rows.Add(1450, "Doc_Num_15", "Doc Num-15", "", "Doc_Num_15", "WTY_EXCEL", "2")
            dtExport.Rows.Add(1460, "Matrial_Serial_15", "Matrial Serial-15", "", "Matrial_Serial_15", "WTY_EXCEL", "2")
        ElseIf RepID = "3A" Or RepID = "3B" Or RepID = "4" Then
            dtExport.Rows.Add(10, "samsung_Ref_No", "Samsung Ref No", "", "samsung_Ref_No", "invoice_update", "3A")
            dtExport.Rows.Add(20, "Your_Ref_No", "Your Ref No", "", "Your_Ref_No", "invoice_update", "3A")
            dtExport.Rows.Add(30, "Model", "Model", "", "Model", "invoice_update", "3A")
            dtExport.Rows.Add(40, "Serial", "Serial", "", "Serial", "invoice_update", "3A")
            dtExport.Rows.Add(50, "Product", "Product", "", "Product", "invoice_update", "3A")
            dtExport.Rows.Add(60, "Serivice", "Service", "", "Serivice", "invoice_update", "3A")
            dtExport.Rows.Add(70, "Defect_Code", "Defect Code", "", "Defect_Code", "invoice_update", "3A")
            dtExport.Rows.Add(80, "Currency", "Currency", "", "Currency", "invoice_update", "3A")
            dtExport.Rows.Add(90, "Invoice", "Invoice", "CAST(Invoice AS decimal(12,2))", "Invoice", "invoice_update", "3A")
            dtExport.Rows.Add(100, "Labor", "Labor", "CAST(Labor AS decimal(12,2))", "Labor", "invoice_update", "3A")
            dtExport.Rows.Add(110, "Parts", "Parts", "CAST(Parts AS decimal(12,2))", "Parts", "invoice_update", "3A")
            dtExport.Rows.Add(120, "Felight", "Freight", "CAST(Felight AS decimal(12,2))", "Felight", "invoice_update", "3A")
            dtExport.Rows.Add(130, "Other", "Other", "CAST(Other AS decimal(12,2))", "Other", "invoice_update", "3A")
            dtExport.Rows.Add(140, "Tax", "Tax", "CAST(Tax AS decimal(12,2))", "Tax", "invoice_update", "3A")
        ElseIf RepID = "6" Then
            dtExport.Rows.Add(10, "SHIP_TO_BRANCH_CODE", "Ship-to-Branch-Code", "", "SHIP_TO_BRANCH_CODE", "PR_SUMMARY", "6")
            dtExport.Rows.Add(20, "SHIP_TO_BRANCH", "Ship-to-Branch", "", "SHIP_TO_BRANCH", "PR_SUMMARY", "6")
            dtExport.Rows.Add(30, "INVOICE_DATE", "Invoice Date", "CONVERT(VARCHAR(8), INVOICE_DATE, 112)", "INVOICE_DATE_Orig", "PR_SUMMARY", "6")
            dtExport.Rows.Add(35, "INVOICE_DATE_Orig", "Invoice Date Orig", "INVOICE_DATE", "INVOICE_DATE_Orig", "PR_SUMMARY", "6")
            dtExport.Rows.Add(40, "INVOICE_NO", "Invoice No", "", "INVOICE_NO", "PR_SUMMARY", "6")
            dtExport.Rows.Add(50, "LOCAL_INVOICE_NO", "Local Invoice No", "", "LOCAL_INVOICE_NO", "PR_SUMMARY", "6")
            dtExport.Rows.Add(60, "DELIVERY_NO", "Delivery No", "", "DELIVERY_NO", "PR_SUMMARY", "6")
            dtExport.Rows.Add(70, "ITEMS", "Items", "", "ITEMS", "PR_SUMMARY", "6")
            dtExport.Rows.Add(80, "AMOUNT", "Amount", "", "AMOUNT", "PR_SUMMARY", "6")
            dtExport.Rows.Add(90, "SGST_UTGST", "SGST / UTGST", "", "SGST_UTGST", "PR_SUMMARY", "6")
            dtExport.Rows.Add(100, "CGST", "CGST", "", "CGST", "PR_SUMMARY", "6")
            dtExport.Rows.Add(110, "IGST", "IGST", "", "IGST", "PR_SUMMARY", "6")
            dtExport.Rows.Add(120, "CESS", "Cess", "", "CESS", "PR_SUMMARY", "6")
            dtExport.Rows.Add(130, "TAX", "Tax", "", "TAX", "PR_SUMMARY", "6")
            dtExport.Rows.Add(140, "TOTAL", "Total", "", "TOTAL", "PR_SUMMARY", "6")
            dtExport.Rows.Add(150, "GR_STATUS", "GR Status", "", "GR_STATUS", "PR_SUMMARY", "6")
        ElseIf RepID = "7" Then
            dtExport.Rows.Add(10, "SHIP_TO_BRANCH_CODE", "Ship-to-Branch-Code", "", "SHIP_TO_BRANCH_CODE", "PR_DETAIL", "7")
            dtExport.Rows.Add(20, "SHIP_TO_BRANCH", "Ship-to-Branch", "", "SHIP_TO_BRANCH", "PR_DETAIL", "7")
            dtExport.Rows.Add(30, "BILLING_DATE", "Billing Date", "CONVERT(VARCHAR(8), BILLING_DATE, 112)", "BILLING_DATE_Orig", "PR_DETAIL", "7")
            dtExport.Rows.Add(35, "BILLING_DATE_Orig", "BILLING_DATE ORIG", "BILLING_DATE", "BILLING_DATE_Orig", "PR_DETAIL", "7")
            dtExport.Rows.Add(40, "INVOICE_NO", "Invoice No", "", "INVOICE_NO", "PR_DETAIL", "7")
            dtExport.Rows.Add(50, "ITEM_NO", "Item No", "", "ITEM_NO", "PR_DETAIL", "7")
            dtExport.Rows.Add(60, "DELIVERY_NO", "Delivery No", "", "DELIVERY_NO", "PR_DETAIL", "7")
            dtExport.Rows.Add(70, "PO_NO", "P/O No", "", "PO_NO", "PR_DETAIL", "7")
            dtExport.Rows.Add(80, "PO_TYPE", "P/O Type code", "", "PO_TYPE", "PR_DETAIL", "7")
            dtExport.Rows.Add(90, "PART_NO", "Part No", "", "PART_NO", "PR_DETAIL", "7")
            dtExport.Rows.Add(100, "BILLING_QTY", "Billing Qty", "", "BILLING_QTY", "PR_DETAIL", "7")
            dtExport.Rows.Add(110, "AMOUNT", "Amount", "", "AMOUNT", "PR_DETAIL", "7")
            dtExport.Rows.Add(120, "SGST_UTGST", "SGST / UTGST", "", "SGST_UTGST", "PR_DETAIL", "7")
            dtExport.Rows.Add(130, "CGST", "CGST", "", "CGST", "PR_DETAIL", "7")
            dtExport.Rows.Add(140, "IGST", "IGST", "", "IGST", "PR_DETAIL", "7")
            dtExport.Rows.Add(150, "CESS", "Cess", "", "CESS", "PR_DETAIL", "7")
            dtExport.Rows.Add(160, "TAX", "Tax", "", "TAX", "PR_DETAIL", "7")
            dtExport.Rows.Add(170, "CORE_FLAG", "Core Flag", "", "CORE_FLAG", "PR_DETAIL", "7")
            dtExport.Rows.Add(180, "TOTAL", "Total", "", "TOTAL", "PR_DETAIL", "7")
        ElseIf RepID = "8A" Then
            dtExport.Rows.Add(10, "NO", "No", "", "NO", "G_RECEIVED", "8A")
            dtExport.Rows.Add(20, "INVOICE_NO", "Invoice No", "", "INVOICE_NO", "G_RECEIVED", "8A")
            dtExport.Rows.Add(30, "INVOICE_DATE", "Invoice Date", "replace(CONVERT(VARCHAR(10), INVOICE_DATE, 101),'/','.')", "INVOICE_DATE_Orig", "G_RECEIVED", "8A")
            dtExport.Rows.Add(35, "INVOICE_DATE_Orig", "INVOICE DATE ORIG", "INVOICE_DATE", "INVOICE_DATE_Orig", "G_RECEIVED", "8A")
            dtExport.Rows.Add(40, "LOCAL_INVOICE_NO", "Local Invoice No", "", "LOCAL_INVOICE_NO", "G_RECEIVED", "8A")
            dtExport.Rows.Add(50, "ITEMS", "Items", "", "ITEMS", "G_RECEIVED", "8A")
            dtExport.Rows.Add(60, "TOTAL_QTY", "Total Qty", "", "TOTAL_QTY", "G_RECEIVED", "8A")
            dtExport.Rows.Add(70, "TOTAL_AMOUNT", "Total Amount", "", "TOTAL_AMOUNT", "G_RECEIVED", "8A")
            dtExport.Rows.Add(80, "GR_DATE", "GR Date", "replace(CONVERT(VARCHAR(10), GR_DATE, 101),'/','.')", "GR_DATE_Orig", "G_RECEIVED", "8A")
            dtExport.Rows.Add(85, "GR_DATE_Orig", "GR Date ORIG", "GR_DATE", "GR_DATE_Orig", "G_RECEIVED", "8A")
            dtExport.Rows.Add(90, "CREATE_BY", "Create By", "", "CREATE_BY", "G_RECEIVED", "8A")
            dtExport.Rows.Add(100, "GR_STATUS", "G/R Status", "", "GR_STATUS", "G_RECEIVED", "8A")
        ElseIf RepID = "8B" Then
            dtExport.Rows.Add(10, "NO", "No", "", "NO", "GD_RECEIVED", "8B")
            dtExport.Rows.Add(20, "INVOICE_NO", "Invoice No", "", "INVOICE_NO", "GD_RECEIVED", "8B")
            dtExport.Rows.Add(30, "PO_NO", "PO NO", "", "PO_NO", "GD_RECEIVED", "8B")
            dtExport.Rows.Add(40, "ITEM_NO", "Item No", "", "ITEM_NO", "GD_RECEIVED", "8B")
            dtExport.Rows.Add(50, "DESCRIPTION", "Description", "", "DESCRIPTION", "GD_RECEIVED", "8B")
            dtExport.Rows.Add(60, "ORDERED_PARTS", "Ordered Parts", "", "ORDERED_PARTS", "GD_RECEIVED", "8B")
            dtExport.Rows.Add(70, "SHIPPED_PARTS", "Shipped Parts", "", "SHIPPED_PARTS", "GD_RECEIVED", "8B")
            dtExport.Rows.Add(80, "DELIVERED_QTY", "Delivered Qty", "", "DELIVERED_QTY", "GD_RECEIVED", "8B")
            dtExport.Rows.Add(90, "CONFIRMED_QTY", "Confirmed Qty", "", "CONFIRMED_QTY", "GD_RECEIVED", "8B")
            dtExport.Rows.Add(100, "TOTAL_STOCK_QTY", "Total Stock Qty", "", "TOTAL_STOCK_QTY", "GD_RECEIVED", "8B")
            dtExport.Rows.Add(110, "REASON", "Reason", "", "REASON", "GD_RECEIVED", "8B")
            dtExport.Rows.Add(120, "LOCATION1", "Location1", "", "LOCATION1", "GD_RECEIVED", "8B")
            dtExport.Rows.Add(130, "LOCATION2", "Location2", "", "LOCATION2", "GD_RECEIVED", "8B")
            dtExport.Rows.Add(140, "LOCATION3", "Location3", "", "LOCATION3", "GD_RECEIVED", "8B")
            dtExport.Rows.Add(150, "HSN_CODE_OF_PARTS_RECEIVED", "HSN Code of Parts Received", "", "HSN_CODE_OF_PARTS_RECEIVED", "GD_RECEIVED", "8B")
            dtExport.Rows.Add(160, "SUPPLY_LOCATION", "Supply Location", "", "SUPPLY_LOCATION", "GD_RECEIVED", "8B")
            dtExport.Rows.Add(170, "DELIVERY_LOCATION", "Delivery Location", "", "DELIVERY_LOCATION", "GD_RECEIVED", "8B")
            dtExport.Rows.Add(180, "REMARKS", "Remarks", "", "REMARKS", "GD_RECEIVED", "8B")
        ElseIf RepID = "9" Then
            dtExport.Rows.Add(10, "SHIP_TO_BRANCH", "Branch", "", "SHIP_TO_BRANCH", "STOCK_OVERVIEW", "9")
            dtExport.Rows.Add(20, "PARTS_NO", "Parts No", "", "PARTS_NO", "STOCK_OVERVIEW", "9")
            dtExport.Rows.Add(30, "DESCRIPTION", "Description", "", "DESCRIPTION", "STOCK_OVERVIEW", "9")
            dtExport.Rows.Add(40, "TOTAL_STOCK_QTY", "Total Stock Qty", "", "TOTAL_STOCK_QTY", "STOCK_OVERVIEW", "9")
            dtExport.Rows.Add(50, "WAREHOUSE_STOCK_QTY", "Warehouse Stock Qty", "", "WAREHOUSE_STOCK_QTY", "STOCK_OVERVIEW", "9")
            dtExport.Rows.Add(60, "ENGINEER_STOCK_QTY", "Engineer Stock Qty", "", "ENGINEER_STOCK_QTY", "STOCK_OVERVIEW", "9")
            dtExport.Rows.Add(70, "LOCATION1", "Location1", "", "LOCATION1", "STOCK_OVERVIEW", "9")
            dtExport.Rows.Add(80, "LOCATION2", "Location2", "", "LOCATION2", "STOCK_OVERVIEW", "9")
            dtExport.Rows.Add(90, "LOCATION3", "Location3", "", "LOCATION3", "STOCK_OVERVIEW", "9")
            dtExport.Rows.Add(100, "MAP", "MAP", "", "MAP", "STOCK_OVERVIEW", "9")
            dtExport.Rows.Add(110, "TOTAL_STOCK_VALUE", "Total Stock Value", "", "TOTAL_STOCK_VALUE", "STOCK_OVERVIEW", "9")
            dtExport.Rows.Add(120, "STATUS", "Status", "", "STATUS", "STOCK_OVERVIEW", "9")
            dtExport.Rows.Add(130, "LATEST_MODIFY_DATE", "Latest modify date", "CONVERT(VARCHAR(8), LATEST_MODIFY_DATE, 112)", "LATEST_MODIFY_DATE_Orig", "STOCK_OVERVIEW", "9")
            dtExport.Rows.Add(135, "LATEST_MODIFY_DATE_Orig", "Latest modify date Orig", "LATEST_MODIFY_DATE", "LATEST_MODIFY_DATE_Orig", "STOCK_OVERVIEW", "9")
        ElseIf RepID = "10" Then
            dtExport.Rows.Add(10, "COMPANY", "COMPANY", "", "COMPANY", "SAW_DISCOUNT", "10")
            dtExport.Rows.Add(20, "COUNTRY", "COUNTRY", "", "COUNTRY", "SAW_DISCOUNT", "10")
            dtExport.Rows.Add(30, "SAW_NO", "SAW NO", "", "SAW_NO", "SAW_DISCOUNT", "10")
            dtExport.Rows.Add(40, "OBJECT_ID", "OBJECT ID", "", "OBJECT_ID", "SAW_DISCOUNT", "10")
            dtExport.Rows.Add(50, "MODEL_CODE", "MODEL CODE", "", "MODEL_CODE", "SAW_DISCOUNT", "10")
            dtExport.Rows.Add(60, "SERIAL_NO", "SERIAL NO", "", "SERIAL_NO", "SAW_DISCOUNT", "10")
            dtExport.Rows.Add(70, "IMEI", "IMEI", "", "IMEI", "SAW_DISCOUNT", "10")
            dtExport.Rows.Add(80, "GRMS_REQ_NO", "GRMS REQ NO", "", "GRMS_REQ_NO", "SAW_DISCOUNT", "10")
            dtExport.Rows.Add(90, "REQ_CATEGORY", "REQ CATEGORY", "", "REQ_CATEGORY", "SAW_DISCOUNT", "10")
            dtExport.Rows.Add(100, "REQ_CATEGORY_DESC", "REQ CATEGORY DESC", "", "REQ_CATEGORY_DESC", "SAW_DISCOUNT", "10")
            dtExport.Rows.Add(110, "REQ_TYPE", "REQ TYPE", "", "REQ_TYPE", "SAW_DISCOUNT", "10")
            dtExport.Rows.Add(120, "REQ_TYPE_DESC", "REQ TYPE DESC", "", "REQ_TYPE_DESC", "SAW_DISCOUNT", "10")
            dtExport.Rows.Add(130, "REQ_DATE", "REQ DATE", "replace(CONVERT(VARCHAR(10), REQ_DATE, 101),'/','.')", "REQ_DATE_Orig", "SAW_DISCOUNT", "10")
            dtExport.Rows.Add(135, "REQ_DATE_Orig", "REQ DATE ORIG", "REQ_DATE", "REQ_DATE_Orig", "SAW_DISCOUNT", "10")
            dtExport.Rows.Add(140, "STATUS", "STATUS", "", "STATUS", "SAW_DISCOUNT", "10")
            dtExport.Rows.Add(150, "STATUS_DESC", "STATUS DESC", "", "STATUS_DESC", "SAW_DISCOUNT", "10")
            dtExport.Rows.Add(160, "ASC_CODE", "ASC CODE", "", "ASC_CODE", "SAW_DISCOUNT", "10")
            dtExport.Rows.Add(170, "REQUISTER", "REQUESTER", "", "REQUISTER", "SAW_DISCOUNT", "10")
            dtExport.Rows.Add(180, "ZSAW_CURR", "ZSAW CURR", "", "ZSAW_CURR", "SAW_DISCOUNT", "10")
            dtExport.Rows.Add(190, "REQ_VALUR", "REQ VALUE", "", "REQ_VALUR", "SAW_DISCOUNT", "10")
            dtExport.Rows.Add(200, "REQ_AMT", "REQ AMT", "", "REQ_AMT", "SAW_DISCOUNT", "10")
            dtExport.Rows.Add(210, "REQ_COMMENT", "REQ COMMENT", "", "REQ_COMMENT", "SAW_DISCOUNT", "10")
            dtExport.Rows.Add(220, "CONF_VALUE", "CONF VALUE", "", "CONF_VALUE", "SAW_DISCOUNT", "10")
            dtExport.Rows.Add(230, "CONF_AMT", "CONF AMT", "", "CONF_AMT", "SAW_DISCOUNT", "10")
            dtExport.Rows.Add(240, "CONF_COMMENT", "CONF COMMENT", "", "CONF_COMMENT", "SAW_DISCOUNT", "10")
            dtExport.Rows.Add(250, "CONTRACTOR", "CONTRACTOR", "", "CONTRACTOR", "SAW_DISCOUNT", "10")
            dtExport.Rows.Add(260, "EXCH_TYPE", "EXCH TYPE", "", "EXCH_TYPE", "SAW_DISCOUNT", "10")
            dtExport.Rows.Add(270, "EXCH_TYPE_DESC", "EXCH TYPE DESC", "", "EXCH_TYPE_DESC", "SAW_DISCOUNT", "10")
            dtExport.Rows.Add(280, "EXCH_REASON", "EXCH REASON", "", "EXCH_REASON", "SAW_DISCOUNT", "10")
            dtExport.Rows.Add(290, "EXCH_REASON_DESC", "EXCH REASON DESC", "", "EXCH_REASON_DESC", "SAW_DISCOUNT", "10")
            dtExport.Rows.Add(300, "EXCH_SUB_REASON", "EXCH SUB REASON", "", "EXCH_SUB_REASON", "SAW_DISCOUNT", "10")
            dtExport.Rows.Add(310, "EXCH_SUB_REASON_DESC", "EXCH SUB REASON DESC", "", "EXCH_SUB_REASON_DESC", "SAW_DISCOUNT", "10")
            dtExport.Rows.Add(320, "INVOICE_AMT", "INVOICE AMT", "", "INVOICE_AMT", "SAW_DISCOUNT", "10")
            dtExport.Rows.Add(330, "EST_REPAIR_AMT", "EST REPAIR AMT", "", "EST_REPAIR_AMT", "SAW_DISCOUNT", "10")
            dtExport.Rows.Add(340, "PART_NO", "PART NO", "", "PART_NO", "SAW_DISCOUNT", "10")
            dtExport.Rows.Add(350, "PART_DESC", "PART DESC", "", "PART_DESC", "SAW_DISCOUNT", "10")
            dtExport.Rows.Add(360, "PO_NO", "PO NO", "", "PO_NO", "SAW_DISCOUNT", "10")
            dtExport.Rows.Add(370, "EMPLOYEE", "EMPLOYEE", "", "EMPLOYEE", "SAW_DISCOUNT", "10")
            dtExport.Rows.Add(380, "PRE_EMPLOYEE", "PRE EMPLOYEE", "", "PRE_EMPLOYEE", "SAW_DISCOUNT", "10")
            dtExport.Rows.Add(390, "CONF_USER", "CONF USER", "", "CONF_USER", "SAW_DISCOUNT", "10")
            dtExport.Rows.Add(400, "CONF_DATE", "CONF DATE", "replace(CONVERT(VARCHAR(10), CONF_DATE, 101),'/','.')", "CONF_DATE_Orig", "SAW_DISCOUNT", "10")
            dtExport.Rows.Add(405, "CONF_DATE_Orig", "CONF DATE ORIG", "CONF_DATE", "CONF_DATE_Orig", "SAW_DISCOUNT", "10")
        ElseIf RepID = "11" Then
            dtExport.Rows.Add(10, "NO", "No", "", "NO", "PARTS_IO", "11")
            dtExport.Rows.Add(20, "SHIP_TO_BRANCH_CODE", "Branch", "", "SHIP_TO_BRANCH_CODE", "PARTS_IO", "11")
            dtExport.Rows.Add(30, "IN_OUT_DATE", "In/Out Date", "", "IN_OUT_DATE_Orig", "PARTS_IO", "11")
            dtExport.Rows.Add(35, "IN_OUT_DATE_Orig", "In/Out Date Orig", "case when IN_OUT_DATE Like '%.%' then CONVERT(DATETIME,replace(IN_OUT_DATE,'.','/'),101) Else Convert(DateTime, IN_OUT_DATE, 103) End", "IN_OUT_DATE_Orig", "PARTS_IO", "11")
            dtExport.Rows.Add(40, "TYPE", "Type", "", "TYPE", "PARTS_IO", "11")
            dtExport.Rows.Add(50, "TYPE_DESCRIPTION", "Type Description", "", "TYPE_DESCRIPTION", "PARTS_IO", "11")
            dtExport.Rows.Add(60, "REF_DOC_NO", "Ref.Doc.No", "", "REF_DOC_NO", "PARTS_IO", "11")
            dtExport.Rows.Add(70, "PARTS_NO", "Parts No", "", "PARTS_NO", "PARTS_IO", "11")
            dtExport.Rows.Add(80, "DESCRIPTION", "Description", "", "DESCRIPTION", "PARTS_IO", "11")
            dtExport.Rows.Add(90, "QTY", "Qty", "", "QTY", "PARTS_IO", "11")
            dtExport.Rows.Add(100, "MAP", "MAP", "", "MAP", "PARTS_IO", "11")
            dtExport.Rows.Add(110, "ENGINEER_CODE", "Engineer Code", "", "ENGINEER_CODE", "PARTS_IO", "11")
            dtExport.Rows.Add(120, "IN_OUT_WARRANTY", "In/Out Warranty", "", "IN_OUT_WARRANTY", "PARTS_IO", "11")
            dtExport.Rows.Add(130, "UNIT", "Unit", "", "UNIT", "PARTS_IO", "11")
        ElseIf RepID = "14" Then
            dtExport.Rows.Add(10, "SHIP_TO_BRANCH_CODE", "Ship-to-Branch-Code", "", "SHIP_TO_BRANCH_CODE", "G2S_PIAD", "14")
            dtExport.Rows.Add(20, "SHIP_TO_BRANCH", "Ship-to-Branch", "", "SHIP_TO_BRANCH", "G2S_PIAD", "14")
            dtExport.Rows.Add(30, "INVOICE_DATE", "Invoice Date", "CONVERT(VARCHAR(8), INVOICE_DATE, 112)", "INVOICE_DATE_Orig", "G2S_PIAD", "14")
            dtExport.Rows.Add(35, "INVOICE_DATE_Orig", "Invoice Date Orig", "INVOICE_DATE", "INVOICE_DATE_Orig", "G2S_PIAD", "14")
            dtExport.Rows.Add(40, "INVOICE_NO", "Invoice No", "", "INVOICE_NO", "G2S_PIAD", "14")
            dtExport.Rows.Add(50, "LOCAL_INVOICE_NO", "Local Invoice No", "", "LOCAL_INVOICE_NO", "G2S_PIAD", "14")
            dtExport.Rows.Add(60, "DELIVERY_NO", "Delivery No", "", "DELIVERY_NO", "G2S_PIAD", "14")
            dtExport.Rows.Add(70, "ITEMS", "Items", "", "ITEMS", "G2S_PIAD", "14")
            dtExport.Rows.Add(80, "AMOUNT", "Amount", "", "AMOUNT", "G2S_PIAD", "14")
            dtExport.Rows.Add(90, "SGST_UTGST", "SGST / UTGST", "", "SGST_UTGST", "G2S_PIAD", "14")
            dtExport.Rows.Add(100, "CGST", "CGST", "", "CGST", "G2S_PIAD", "14")
            dtExport.Rows.Add(110, "IGST", "IGST", "", "IGST", "G2S_PIAD", "14")
            dtExport.Rows.Add(120, "CESS", "Cess", "", "CESS", "G2S_PIAD", "14")
            dtExport.Rows.Add(130, "TAX", "Tax", "", "TAX", "G2S_PIAD", "14")
            dtExport.Rows.Add(140, "TOTAL", "Total", "", "TOTAL", "G2S_PIAD", "14")
            dtExport.Rows.Add(150, "GR_STATUS", "GR Status", "", "GR_STATUS", "G2S_PIAD", "14")
            dtExport.Rows.Add(160, "PAID_STATUS", "Paid status", "", "PAID_STATUS", "G2S_PIAD", "14")
            dtExport.Rows.Add(170, "PAID_DATE", "Paid date", "replace(CONVERT(VARCHAR(10), PAID_DATE, 103),'/','.')", "PAID_DATE_Orig", "G2S_PIAD", "14")
            dtExport.Rows.Add(175, "PAID_DATE_Orig", "Paid date Orig", "PAID_DATE", "PAID_DATE_Orig", "G2S_PIAD", "14")
            dtExport.Rows.Add(180, "PAYMENT_REQUEST_DATE", "Payment request  date", "replace(CONVERT(VARCHAR(10), PAYMENT_REQUEST_DATE, 103),'/','.')", "PAYMENT_REQUEST_DATE_Orig", "G2S_PIAD", "14")
            dtExport.Rows.Add(185, "PAYMENT_REQUEST_DATE_Orig", "Payment request  date Orig", "PAYMENT_REQUEST_DATE", "PAYMENT_REQUEST_DATE_Orig", "G2S_PIAD", "14")
        ElseIf RepID = "15" Then
            dtExport.Rows.Add(10, "SHIP_TO_BRANCH_CODE", "Ship-to-Branch-Code", "", "SHIP_TO_BRANCH_CODE", "RETURN_CREDIT", "15")
            dtExport.Rows.Add(20, "SHIP_TO_BRANCH", "Ship-to-Branch", "", "SHIP_TO_BRANCH", "RETURN_CREDIT", "15")
            dtExport.Rows.Add(30, "INVOICE_DATE", "Invoice Date", "CONVERT(VARCHAR(8), INVOICE_DATE, 112)", "INVOICE_DATE_Orig", "RETURN_CREDIT", "15")
            dtExport.Rows.Add(35, "INVOICE_DATE_Orig", "Invoice Date Orig", "INVOICE_DATE", "INVOICE_DATE_Orig", "RETURN_CREDIT", "15")
            dtExport.Rows.Add(40, "INVOICE_NO", "Invoice No", "", "INVOICE_NO", "RETURN_CREDIT", "15")
            dtExport.Rows.Add(50, "LOCAL_INVOICE_NO", "Local Invoice No", "", "LOCAL_INVOICE_NO", "RETURN_CREDIT", "15")
            dtExport.Rows.Add(60, "DELIVERY_NO", "Delivery No", "", "DELIVERY_NO", "RETURN_CREDIT", "15")
            dtExport.Rows.Add(70, "ITEMS", "Items", "", "ITEMS", "RETURN_CREDIT", "15")
            dtExport.Rows.Add(80, "AMOUNT", "Amount", "", "AMOUNT", "RETURN_CREDIT", "15")
            dtExport.Rows.Add(90, "SGST_UTGST", "SGST / UTGST", "", "SGST_UTGST", "RETURN_CREDIT", "15")
            dtExport.Rows.Add(100, "CGST", "CGST", "", "CGST", "RETURN_CREDIT", "15")
            dtExport.Rows.Add(110, "IGST", "IGST", "", "IGST", "RETURN_CREDIT", "15")
            dtExport.Rows.Add(120, "CESS", "Cess", "", "CESS", "RETURN_CREDIT", "15")
            dtExport.Rows.Add(130, "TAX", "Tax", "", "TAX", "RETURN_CREDIT", "15")
            dtExport.Rows.Add(140, "TOTAL", "Total", "", "TOTAL", "RETURN_CREDIT", "15")
            dtExport.Rows.Add(150, "GR_STATUS", "GR Status", "", "GR_STATUS", "RETURN_CREDIT", "15")
        ElseIf RepID = "16" Then
            dtExport.Rows.Add(10, "DOC_NO", "Doc.No.", "", "DOC_NO", "S_LEDGER", "16")
            dtExport.Rows.Add(20, "ITEMS", "Item", "", "ITEMS", "S_LEDGER", "16")
            dtExport.Rows.Add(30, "DOCUMENT_DATE", "Document Date", "replace(CONVERT(VARCHAR(10), DOCUMENT_DATE, 126),'/','.')", "DOCUMENT_DATE_Orig", "S_LEDGER", "16")
            dtExport.Rows.Add(35, "DOCUMENT_DATE_Orig", "Document Date Orig", "DOCUMENT_DATE", "DOCUMENT_DATE_Orig", "S_LEDGER", "16")
            dtExport.Rows.Add(40, "POSTING_DATE", "Posting Date", "replace(CONVERT(VARCHAR(10), POSTING_DATE, 126),'/','.')", "POSTING_DATE_Orig", "S_LEDGER", "16")
            dtExport.Rows.Add(45, "POSTING_DATE_Orig", "Posting Date Orig", "POSTING_DATE", "POSTING_DATE_Orig", "S_LEDGER", "16")
            dtExport.Rows.Add(50, "DOC_TYPE", "Doc. Type", "", "DOC_TYPE", "S_LEDGER", "16")
            dtExport.Rows.Add(60, "INVOICE_NO", "Invoice No.", "", "INVOICE_NO", "S_LEDGER", "16")
            dtExport.Rows.Add(70, "CURR", "Curr.", "", "CURR", "S_LEDGER", "16")
            dtExport.Rows.Add(80, "DEBIT_AMOUNT", "Debit Amount", "", "DEBIT_AMOUNT", "S_LEDGER", "16")
            dtExport.Rows.Add(90, "CREDIT_AMOUNT", "Credit Amount", "", "CREDIT_AMOUNT", "S_LEDGER", "16")
            dtExport.Rows.Add(100, "CLOSING_BALANCE", "Closing Balance", "", "CLOSING_BALANCE", "S_LEDGER", "16")
            dtExport.Rows.Add(110, "ASSIGNMENT", "Assignment", "", "ASSIGNMENT", "S_LEDGER", "16")
            dtExport.Rows.Add(120, "TEXT", "Text", "", "TEXT", "S_LEDGER", "16")
            dtExport.Rows.Add(130, "BA", "BA", "", "BA", "S_LEDGER", "16")
            dtExport.Rows.Add(140, "SEGMENT", "Segment", "", "SEGMENT", "S_LEDGER", "16")
        ElseIf RepID = "19" Then
            dtExport.Rows.Add(10, "NO", "No", "", "NO", "SP_RETURN", "17")
            dtExport.Rows.Add(20, "CREDIT_REQ_NO", "Credit Req. No", "", "CREDIT_REQ_NO", "SP_RETURN", "17")
            dtExport.Rows.Add(30, "STATUS", "Status", "", "STATUS", "SP_RETURN", "17")
            dtExport.Rows.Add(40, "REQUEST_DATE", "Requested Date", "replace(CONVERT(VARCHAR(10), REQUEST_DATE, 101),'/','.')", "REQUEST_DATE_Orig", "SP_RETURN", "17")
            dtExport.Rows.Add(45, "REQUEST_DATE_Orig", "Requested Date Orig", "REQUEST_DATE", "REQUEST_DATE_Orig", "SP_RETURN", "17")
            dtExport.Rows.Add(50, "ASC_REF_NO", "Asc.Ref.No", "", "ASC_REF_NO", "SP_RETURN", "17")
            dtExport.Rows.Add(60, "REASON", "Reason", "", "REASON", "SP_RETURN", "17")
            dtExport.Rows.Add(70, "SERVICE_ORDER", "Service Order", "", "SERVICE_ORDER", "SP_RETURN", "17")
            dtExport.Rows.Add(80, "ENGINEER", "Engineer", "", "ENGINEER", "SP_RETURN", "17")
            dtExport.Rows.Add(90, "BILLING_NO", "Billing No", "", "BILLING_NO", "SP_RETURN", "17")
            dtExport.Rows.Add(100, "AMOUNT", "Amount", "", "AMOUNT", "SP_RETURN", "17")
            dtExport.Rows.Add(110, "TITLE", "Title", "", "TITLE", "SP_RETURN", "17")
            dtExport.Rows.Add(120, "PLANT", "Plant", "", "PLANT", "SP_RETURN", "17")
            dtExport.Rows.Add(130, "RETURN_TRACKING_NO", "Return Tracking No", "", "RETURN_TRACKING_NO", "SP_RETURN", "17")
            dtExport.Rows.Add(140, "SYMPTOM", "Symptom", "", "SYMPTOM", "SP_RETURN", "17")
            dtExport.Rows.Add(150, "PICKUP_DATE", "Pickup Date", "PICKUP_DATE", "PICKUP_DATE_Orig", "SP_RETURN", "17")
            dtExport.Rows.Add(155, "PICKUP_DATE_Orig", "Pickup Date Orig", "case when PICKUP_DATE= '00.00.0000' then null else  CONVERT(datetime, PICKUP_DATE, 101) end", "PICKUP_DATE_Orig", "SP_RETURN", "17")
            dtExport.Rows.Add(160, "RETURN_SO", "Return S/O", "", "RETURN_SO", "SP_RETURN", "17")
        ElseIf RepID = "18" Then
            dtExport.Rows.Add(10, "MONTH_UNQ_NO", "unq no", "", "MONTH_UNQ_NO", "DEBITNOTE_REGISTER", "18")
            dtExport.Rows.Add(13, "REQUEST_DATE", "Request Date", "Replace(Replace(convert(varchar(10), ISNULL(REQUEST_DATE,''), 101),'/','.'),'01.01.1900','')", "REQUEST_DATE_Orig", "DEBITNOTE_REGISTER", "18")
            dtExport.Rows.Add(14, "REQUEST_DATE_Orig", "Request Date Orig", "REQUEST_DATE", "REQUEST_DATE_Orig", "DEBITNOTE_REGISTER", "18")
            dtExport.Rows.Add(20, "DEBIT_NOTE_DATE", "Debitnote Date", "Replace(Replace(convert(varchar(10), ISNULL(DEBIT_NOTE_DATE,''), 101),'/','.'),'01.01.1900','')", "DEBIT_NOTE_DATE_Orig", "DEBITNOTE_REGISTER", "18")
            dtExport.Rows.Add(25, "DEBIT_NOTE_DATE_Orig", "Debitnote Date Orig", "DEBIT_NOTE_DATE", "DEBIT_NOTE_DATE_Orig", "DEBITNOTE_REGISTER", "18")
            dtExport.Rows.Add(30, "BILLING_DATE", "Billing Date", "Replace(Replace(convert(varchar(10), ISNULL(BILLING_DATE,''), 101),'/','.'),'01.01.1900','')", "BILLING_DATE_Orig", "DEBITNOTE_REGISTER", "18")
            dtExport.Rows.Add(35, "BILLING_DATE_Orig", "Billing Date Orig", "BILLING_DATE", "BILLING_DATE_Orig", "DEBITNOTE_REGISTER", "18")
            dtExport.Rows.Add(40, "REFINVOICE_DATE", "RefInvoice Date", "Replace(Replace(convert(varchar(10), ISNULL(REFINVOICE_DATE,''), 101),'/','.'),'01.01.1900','')", "REFINVOICE_DATE_Orig", "DEBITNOTE_REGISTER", "18")
            dtExport.Rows.Add(45, "REFINVOICE_DATE_Orig", "RefInvoice Date Orig", "REFINVOICE_DATE", "REFINVOICE_DATE_Orig", "DEBITNOTE_REGISTER", "18")
            dtExport.Rows.Add(50, "PART_CODE", "Part Code", "", "PART_CODE", "DEBITNOTE_REGISTER", "18")
            dtExport.Rows.Add(55, "PARTICULARS", "Particulars(Description)", "", "PARTICULARS", "DEBITNOTE_REGISTER", "18")
            dtExport.Rows.Add(60, "PURCHASE_R_NO", "DebitNote / Purchase Return No", "", "PURCHASE_R_NO", "DEBITNOTE_REGISTER", "18")
            dtExport.Rows.Add(70, "VOUCHER_REF", "Voucher Ref", "", "VOUCHER_REF", "DEBITNOTE_REGISTER", "18")
            dtExport.Rows.Add(75, "GSTIN_UIN", "GSTIN/UIN", "", "GSTIN_UIN", "DEBITNOTE_REGISTER", "18")
            dtExport.Rows.Add(80, "QUANTITY", "Quantity", "", "QUANTITY", "DEBITNOTE_REGISTER", "18")
            dtExport.Rows.Add(90, "RATE", "Rate", "", "RATE", "DEBITNOTE_REGISTER", "18")
            dtExport.Rows.Add(100, "GROSS_TOTAL", "Gross Total", "", "GROSS_TOTAL", "DEBITNOTE_REGISTER", "18")
            dtExport.Rows.Add(110, "SSC_PURCHASE", "SSC xPurchase", "", "SSC_PURCHASE", "DEBITNOTE_REGISTER", "18")
            dtExport.Rows.Add(120, "SGST_RECEIVE_VALUE", "SGSTReceivavle9%", "", "SGST_RECEIVE_VALUE", "DEBITNOTE_REGISTER", "18")
            dtExport.Rows.Add(130, "CGST_RECEIVE_VALUE", "CGSTRecievable9%", "", "CGST_RECEIVE_VALUE", "DEBITNOTE_REGISTER", "18")

            '/*
            'sqlStr = sqlStr & "MONTH_UNQ_NO 'Unq No',"
            'sqlStr = sqlStr & "Replace(convert(varchar(10), ISNULL(REQUEST_DATE,''), 101),'/','.') 'Request Date',"
            'sqlStr = sqlStr & "Replace(convert(varchar(10), ISNULL(DEBIT_NOTE_DATE,''), 101),'/','.') 'Debitnote Date',"
            'sqlStr = sqlStr & "Replace(convert(varchar(10), ISNULL(BILLING_DATE,''), 101),'/','.') 'Billing Date',"
            'sqlStr = sqlStr & "Replace(convert(varchar(10), ISNULL(REFINVOICE_DATE,''), 101),'/','.') 'RefInvoice Date',"
            'sqlStr = sqlStr & "ORIGINAL_INVOICE_REF 'Original Invoice Ref',"
            'sqlStr = sqlStr & "PART_CODE 'Part Code',PARTICULARS 'Particulars(Description)',PURCHASE_R_NO 'DebitNote/Purchase Return No',"
            'sqlStr = sqlStr & "VOUCHER_REF 'Voucher Ref',GSTIN_UIN 'GSTIN/UIN',QUANTITY 'Quantity',RATE 'Rate',"
            'sqlStr = sqlStr & "GROSS_TOTAL 'Gross Total',SSC_PURCHASE 'SSC xPurchase',SGST_RECEIVE_VALUE 'SGSTReceivavle9%',CGST_RECEIVE_VALUE 'CGSTRecievable9%'"
            '*/
        ElseIf RepID = "19B" Then
            dtExport.Rows.Add(10, "PART_NO", "parts_no", "", "PART_NO", "HSN_CODE", "19B")
            dtExport.Rows.Add(20, "HSN_CODE", "hsn_code", "", "HSN_CODE", "HSN_CODE", "19B")
            dtExport.Rows.Add(30, "INTEGRATED_TAX", "integrated_tax", "", "INTEGRATED_TAX", "HSN_CODE", "19B")
            dtExport.Rows.Add(40, "CENTRAL_TAX", "central_tax", "", "CENTRAL_TAX", "HSN_CODE", "19B")
            dtExport.Rows.Add(50, "STATE_TAX", "state_tax", "", "STATE_TAX", "HSN_CODE", "19B")
            dtExport.Rows.Add(60, "CESS", "cess", "", "CESS", "HSN_CODE", "19B")
        ElseIf RepID = "20" Then
            dtExport.Rows.Add(10, "PO_NO", "PO No.", "", "PO_NO", "EXP_WTY", "20")
            dtExport.Rows.Add(20, "CUSTOMER_NAME", "Customer Name", "", "CUSTOMER_NAME", "EXP_WTY", "20")
            dtExport.Rows.Add(30, "PACK_NO", "Purchase Pack", "", "PACK_NO", "EXP_WTY", "20")
            dtExport.Rows.Add(40, "UNIT_PRICE", "Unit Price ", "", "UNIT_PRICE", "EXP_WTY", "20")
            dtExport.Rows.Add(50, "RETAIL_PRICE", "Retail Price ", "", "RETAIL_PRICE", "EXP_WTY", "20")
            dtExport.Rows.Add(60, "SGST_RATE", "SGST Rate %", "", "SGST_RATE", "EXP_WTY", "20")
            dtExport.Rows.Add(70, "SGST_AMOUNT", "SGST Amount", "", "SGST_AMOUNT", "EXP_WTY", "20")
            dtExport.Rows.Add(80, "CGST_RATE", "CGST Rate %", "", "CGST_RATE", "EXP_WTY", "20")
            dtExport.Rows.Add(90, "CGST_AMOUNT", "CGST Amount", "", "CGST_AMOUNT", "EXP_WTY", "20")
            dtExport.Rows.Add(100, "IGST_RATE", "IGST Rate %", "", "IGST_RATE", "EXP_WTY", "20")
            dtExport.Rows.Add(110, "IGST_AMOUNT", "IGST Amount", "", "IGST_AMOUNT", "EXP_WTY", "20")
            dtExport.Rows.Add(120, "CESS_RATE", "CESS Rate %", "", "CESS_RATE", "EXP_WTY", "20")
            dtExport.Rows.Add(130, "CESS_AMOUNT", "CESS Amount", "", "CESS_AMOUNT", "EXP_WTY", "20")
            dtExport.Rows.Add(140, "TOTAL_TAX", "Total Tax ", "", "TOTAL_TAX", "EXP_WTY", "20")
            dtExport.Rows.Add(150, "TOTAL_AMOUNT", "Total Amount ", "", "TOTAL_AMOUNT", "EXP_WTY", "20")
            dtExport.Rows.Add(160, "NOTE", "Note", "", "NOTE", "EXP_WTY", "20")
            dtExport.Rows.Add(170, "MODEL", "Model ", "", "MODEL", "EXP_WTY", "20")
            dtExport.Rows.Add(180, "SERIAL", "Serial ", "", "SERIAL", "EXP_WTY", "20")
            dtExport.Rows.Add(190, "PACK_DOP", "N/A:(PACK/DOP) ", "", "PACK_DOP", "EXP_WTY", "20")
            dtExport.Rows.Add(200, "BP_NO", "BP No ", "", "BP_NO", "EXP_WTY", "20")
        ElseIf RepID = "21" Then
            dtExport.Rows.Add(10, "NO", "No", "", "NO", "PO_Status", "21")
            dtExport.Rows.Add(20, "PO_DATE", "P/O Date", "replace(CONVERT(VARCHAR(10), PO_DATE, 101),'/','.')", "PO_DATE_Orig", "PO_Status", "21")
            dtExport.Rows.Add(25, "PO_DATE_Orig", "P/O Date Orig", "PO_DATE", "PO_DATE_Orig", "PO_Status", "21")
            dtExport.Rows.Add(30, "PO_NO", "P/O No", "", "PO_NO", "PO_Status", "21")
            dtExport.Rows.Add(40, "SHIP_TO", "Ship-to", "", "SHIP_TO", "PO_Status", "21")
            dtExport.Rows.Add(50, "CONF_NO", "Confirmation No", "", "CONF_NO", "PO_Status", "21")
            dtExport.Rows.Add(60, "ITEM_NO", "Item No", "", "ITEM_NO", "PO_Status", "21")
            dtExport.Rows.Add(70, "QTY", "Qty", "", "QTY", "PO_Status", "21")
            dtExport.Rows.Add(80, "AMOUNT", "Amount", "", "AMOUNT", "PO_Status", "21")
            dtExport.Rows.Add(90, "STATUS", "Status", "", "STATUS", "PO_Status", "21")
            'bharathiraja-2 
        ElseIf RepID = "22" Then
            dtExport.Rows.Add(10, "location", "Ship Code", "Activity_Report.location", "Ship Code", "Activity_report", "22")
            dtExport.Rows.Add(20, "ship_name", "Ship Name", "M_ship_base.ship_name", "Ship Name", "Activity_report", "22")
            dtExport.Rows.Add(30, "day", "Day", "Activity_Report.day + '  ' +   datename(weekday,(convert(datetime, month + '/' + day)) )", "Day", "Activity_report", "22")
            dtExport.Rows.Add(40, "Customer_Visit", "Customer Visit", "Activity_Report.Customer_Visit", "Customer_Visit", "Activity_report", "22")
            dtExport.Rows.Add(50, "Call_Registerd", "Call Registerd", "Activity_Report.Call_Registerd", "Call_Registerd", "Activity_report", "22")
            dtExport.Rows.Add(60, "Repair_Completed", "Repair Completed", "Activity_Report.Repair_Completed", "Repair_Completed", "Activity_report", "22")
            dtExport.Rows.Add(70, "Goods_Delivered", "Goods Delivered", "Activity_Report.Goods_Delivered", "Goods_Delivered", "Activity_report", "22")
            dtExport.Rows.Add(80, "Pending_Calls", "Pending Calls", "Activity_Report.Pending_Calls", "Pending_Calls", "Activity_report", "22")
            dtExport.Rows.Add(90, "Cancelled_Calls", "Cancelled Calls", "Activity_Report.Cancelled_Calls", "Cancelled_Calls", "Activity_report", "22")
            'dtExport.Rows.Add(30, "BILLING_DATE", "Billing Date", "CONVERT(VARCHAR(8), BILLING_DATE, 112)", "BILLING_DATE_Orig", "Activity_report", "22")
        ElseIf RepID = "23" Then
            dtExport.Rows.Add(10, "UNQ_NO", "UNQ_NO", "", "UNQ_NO", "PO_DC", "23")
            dtExport.Rows.Add(10, "UPLOAD_USER", "UPLOAD_USER", "", "UPLOAD_USER", "PO_DC", "23")
            dtExport.Rows.Add(10, "UPLOAD_DATE", "UPLOAD_DATE", "", "UPLOAD_DATE", "PO_DC", "23")
            dtExport.Rows.Add(10, "SHIP_TO_BRANCH_CODE", "SHIP_TO_BRANCH_CODE", "", "SHIP_TO_BRANCH_CODE", "PO_DC", "23")
            dtExport.Rows.Add(10, "SHIP_TO_BRANCH", "SHIP_TO_BRANCH", "", "SHIP_TO_BRANCH", "PO_DC", "23")
            dtExport.Rows.Add(10, "PO_DATE", "PO_DATE", "", "PO_DATE", "PO_DC", "23")
            dtExport.Rows.Add(10, "PO_NO", "PO_NO", "", "PO_NO", "PO_DC", "23")
            dtExport.Rows.Add(10, "SHIP_TO", "SHIP_TO", "", "SHIP_TO", "PO_DC", "23")
            dtExport.Rows.Add(10, "CONFIRMATION_NO", "CONFIRMATION_NO", "", "CONFIRMATION_NO", "PO_DC", "23")
            dtExport.Rows.Add(10, "ITEM_NO", "ITEM_NO", "", "ITEM_NO", "PO_DC", "23")
            dtExport.Rows.Add(10, "QTY", "QTY", "", "QTY", "PO_DC", "23")
            dtExport.Rows.Add(10, "AMOUNT", "AMOUNT", "", "AMOUNT", "PO_DC", "23")
            dtExport.Rows.Add(10, "STATUS", "STATUS", "", "STATUS", "PO_DC", "23")
            dtExport.Rows.Add(10, "FILE_NAME", "FILE_NAME", "", "FILE_NAME", "PO_DC", "23")
            dtExport.Rows.Add(10, "SRC_FILE_NAME", "SRC_FILE_NAME", "", "SRC_FILE_NAME", "PO_DC", "23")
        End If

        Return dtExport
    End Function


    'as per requirement need to change-bharathiraja
    Public Function ExportAnalysisDetails(ByVal queryParams As Export_File_Details, Type As String) As DataTable
        Log4NetControl.ComInfoLogWrite(Log4NetControl.UserID)
        Dim dbConn As DBUtility = New DBUtility()
        Dim dt As DataTable = New DataTable()
        Dim qryfield As String
        qryfield = ""

        Dim dgColdtl As DataTable = GetReportFieldDetails(queryParams.ExportFile)

        dgColdtl.DefaultView.Sort = "Id ASC"
        dgColdtl = dgColdtl.DefaultView.ToTable()
        For i = 0 To dgColdtl.Rows.Count - 1
            If Type = "R" Then
                If dgColdtl.Rows(i)(3) = "" Then
                    qryfield = qryfield & dgColdtl.Rows(i)(1) & ","
                Else
                    qryfield = qryfield & dgColdtl.Rows(i)(3) & " As " & dgColdtl.Rows(i)(1) & ","
                End If
            ElseIf Type = "E" Then
                If dgColdtl.Rows(i)(1).ToString().EndsWith("_Orig") = False Then
                    If dgColdtl.Rows(i)(3) = "" Then
                        qryfield = qryfield & dgColdtl.Rows(i)(1) & " As """ & dgColdtl.Rows(i)(2) & ""","
                    Else
                        qryfield = qryfield & dgColdtl.Rows(i)(3) & " As """ & dgColdtl.Rows(i)(2) & ""","
                    End If
                End If
            End If
            'dtExport.Rows.Add(210, "Repair_Description", "Repair Description", "", "Repair_Description", "WTY_EXCEL", "2")
            'dtExport.Rows.Add(220, "Purchase_Date", "Purchase Date", "CONVERT(VARCHAR(8), Purchase_Date, 112)", "Purchase_Date_Orig", "WTY_EXCEL", "2")
            'Dim bfield As New BoundField()
            'bfield.DataField = dgColdtl.Rows(i)(1)
            'bfield.HeaderText = dgColdtl.Rows(i)(2)
            'bfield.SortExpression = dgColdtl.Rows(i)(4)
        Next
        qryfield = qryfield.Remove(qryfield.Length - 1)

        Dim sqlStr As String = "SELECT " & qryfield
        'sqlStr = sqlStr & " ServiceOrder_No,LastUpdt_user,Billinb_user,LEFT(CONVERT(VARCHAR, Billing_date, 103), 10) Billing_date,"
        'sqlStr = sqlStr & "GoodsDelivered_date,Branch_name,Engineer,Product_1 'Product_Code',Product_2 'Product_Name',IW_Labor,IW_Parts,IW_Transport,IW_Others,IW_Tax,IW_total,OW_Labor,"
        'sqlStr = sqlStr & "OW_Parts,OW_Transport,OW_Others,OW_Tax,OW_total,Upload_FileName  "
        sqlStr = sqlStr & " FROM " & dgColdtl.Rows(1)(5) & " WHERE DELFG=0 "
        'sqlStr = sqlStr & " " & SC_DSR& " "
        'sqlStr = sqlStr & " WHERE "
        'sqlStr = sqlStr & " DELFG=0 "

        If queryParams.ExportFile = "1" Then
            sqlStr = sqlStr & "AND Branch_name = @ShipToBranch  "
            sqlStr = sqlStr & "AND LEFT(CONVERT(VARCHAR, Billing_date, 111), 10) >= @DateFrom and LEFT(CONVERT(VARCHAR, Billing_date, 111), 10) <= @DateTo "
            dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@ShipToBranch", queryParams.ShipToBranch))
            dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@DateFrom", queryParams.DateFrom))
            dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@DateTo", queryParams.DateTo))

        ElseIf queryParams.ExportFile = "2" Then
            sqlStr = sqlStr & "AND Wty_Excel.Branch_Code = @ShipToBranchCode "
            sqlStr = sqlStr & "AND LEFT(CONVERT(VARCHAR, Wty_Excel.Delivery_Date, 111), 10) >= @DateFrom and LEFT(CONVERT(VARCHAR, Wty_Excel.Delivery_Date, 111), 10) <= @DateTo "
            dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@ShipToBranchCode", queryParams.ShipToBranchCode))
            dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@DateFrom", queryParams.DateFrom))
            dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@DateTo", queryParams.DateTo))

        ElseIf queryParams.ExportFile = "3A" Or queryParams.ExportFile = "3B" Or queryParams.ExportFile = "4" Then
            sqlStr = sqlStr & "AND upload_Branch = @upload_Branch  "
            sqlStr = sqlStr & "AND number = @number  "
            sqlStr = sqlStr & "AND LEFT(CONVERT(VARCHAR, INVOICE_DATE, 111), 10) >= @DateFrom and LEFT(CONVERT(VARCHAR, INVOICE_DATE, 111), 10) <= @DateTo "
            dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@upload_Branch", queryParams.ShipToBranch))
            dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@number", queryParams.number))
            dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@DateFrom", queryParams.DateFrom))
            dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@DateTo", queryParams.DateTo))
        ElseIf queryParams.ExportFile = "6" Then
            sqlStr = sqlStr & "AND SRC_FILE_NAME = @SRC_FILE_NAME  "
            dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@SRC_FILE_NAME", queryParams.SrcFileName))
        ElseIf queryParams.ExportFile = "7" Then
            Dim inputfile As String = ""
            If queryParams.SrcFileName.Contains("DT1DT2") Then
                inputfile = queryParams.SrcFileName.Replace("DT2", "") & "','" & queryParams.SrcFileName.Replace("DT1", "")
            Else
                inputfile = queryParams.SrcFileName
            End If

            sqlStr = sqlStr & " AND SRC_FILE_NAME IN ('" & inputfile & "')"
            dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@SRC_FILE_NAME", inputfile))
        ElseIf queryParams.ExportFile = "8A" Then
            If Not String.IsNullOrEmpty(queryParams.SrcFileName) Then
                sqlStr = sqlStr & "AND SRC_FILE_NAME = @SRC_FILE_NAME "
                dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@SRC_FILE_NAME", queryParams.SrcFileName))

            Else
                sqlStr = sqlStr & "AND SHIP_TO_BRANCH_CODE =@SHIP_TO_BRANCH_CODE "
                sqlStr = sqlStr & " And INVOICE_DATE  Between  @DateFrom and @DateTo "
                dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@SHIP_TO_BRANCH_CODE", queryParams.ShipToBranchCode))
                dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@DateFrom", queryParams.DateFrom))
                dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@DateTo", queryParams.DateTo))
            End If
        ElseIf queryParams.ExportFile = "8B" Then
            If Not String.IsNullOrEmpty(queryParams.SrcFileName) Then
                sqlStr = sqlStr & "AND SRC_FILE_NAME = @SRC_FILE_NAME "
                dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@SRC_FILE_NAME", queryParams.SrcFileName))

            Else
                sqlStr = sqlStr & "AND SHIP_TO_BRANCH_CODE =@SHIP_TO_BRANCH_CODE "
                sqlStr = sqlStr & " And INVOICE_DATE  Between  @DateFrom and @DateTo "
                dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@SHIP_TO_BRANCH_CODE", queryParams.ShipToBranchCode))
                dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@DateFrom", queryParams.DateFrom))
                dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@DateTo", queryParams.DateTo))
            End If
        ElseIf (queryParams.ExportFile = "9") Or (queryParams.ExportFile = "10") Or (queryParams.ExportFile = "11") Or
            (queryParams.ExportFile = "14") Or (queryParams.ExportFile = "15") Or (queryParams.ExportFile = "16") Or
            (queryParams.ExportFile = "18") Or (queryParams.ExportFile = "19") Or (queryParams.ExportFile = "19B") Or
            (queryParams.ExportFile = "20") Or (queryParams.ExportFile = "21") Then
            sqlStr = sqlStr & "AND SRC_FILE_NAME = @SRC_FILE_NAME "
            dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@SRC_FILE_NAME", queryParams.SrcFileName))

            'bharathiraja-5
        ElseIf (queryParams.ExportFile = "22") Then
            If Not String.IsNullOrEmpty(queryParams.SrcFileName) Then
                sqlStr = sqlStr.Replace("M_ship_base.ship_name As ship_name,", "SHIP_TO_BRANCH as ship_name,")
                sqlStr = sqlStr & " AND MONTH = " & "'" & queryParams.DateFrom & "'"
                dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@month", queryParams.DateFrom))

                sqlStr = sqlStr & "AND SRC_FILE_NAME = @SRC_FILE_NAME "
                dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@SRC_FILE_NAME", queryParams.SrcFileName))
            Else
                sqlStr = sqlStr.Replace("M_ship_base.ship_name As ship_name,", "SHIP_TO_BRANCH as ship_name,")
                sqlStr = sqlStr & " AND MONTH " & queryParams.DateFrom
                dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@month", queryParams.DateFrom))
            End If

            'bharathiraja -PO-CONFIRMATION-3
        ElseIf (queryParams.ExportFile = "23") Then
            sqlStr = sqlStr & "AND SHIP_TO_BRANCH = @ShipToBranch  "
            sqlStr = sqlStr & "AND LEFT(CONVERT(VARCHAR, CRTDT, 111), 10) >= @DateFrom and LEFT(CONVERT(VARCHAR, CRTDT, 111), 10) <= @DateTo "
            dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@ShipToBranch", queryParams.ShipToBranch))
            dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@DateFrom", queryParams.DateFrom))
            dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@DateTo", queryParams.DateTo))
        End If
        Dim _DataTable As DataTable = dbConn.GetDataSet(sqlStr)
        dbConn.CloseConnection()
        Return _DataTable
    End Function

End Class
