Imports System.IO
Imports System.Text
Imports System.Globalization
Imports Ganges33.Ganges33.model
Imports Ganges33.Ganges33.logic
Public Class Analysis_Export_New
    Inherits System.Web.UI.Page
    Dim _ShipBaseControl As ShipBaseControl = New ShipBaseControl()
    Private Property SortDirection As String
        Get
            Return IIf(ViewState("SortDirection") IsNot Nothing, Convert.ToString(ViewState("SortDirection")), "ASC")
        End Get
        Set(value As String)
            ViewState("SortDirection") = value
        End Set
    End Property
    Dim _ScDsrControl As ScDsrControl = New ScDsrControl()
    Dim exfiledtl As Export_File_DetailsControl = New Export_File_DetailsControl()
    Dim _Expflddtl As Export_File_Details = New Export_File_Details()

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        Dim scriptManager As ScriptManager = ScriptManager.GetCurrent(Me.Page)
        scriptManager.RegisterPostBackControl(Me.btnExport)
        scriptManager.RegisterPostBackControl(Me.btnSearch)

        'Session("Exportdtl") = ""
        If IsPostBack = False Then
            lblPagesize.Visible = False
            txtPageSize.Visible = False
            lblErrorMessage.Visible = False
            'Page.MaintainScrollPositionOnPostBack = True
            '***セッションなしログインユーザの対応***
            Dim userid As String = Session("user_id")
            If userid = "" Then
                Response.Redirect("Login.aspx")
            End If

            '***ログインユーザ情報表示***
            Dim setShipname As String = Session("ship_Name")
            Dim userName As String = Session("user_Name")
            Dim userLevel As String = Session("user_level")
            Dim adminFlg As Boolean = Session("admin_Flg")




            lblLoc.Text = setShipname
            lblName.Text = userName

            InitDropDownList()

            DefaultSearchSetting()

            '''''''***拠点名称の設定***
            ''''''DropListLocation.Items.Clear()
            ''''''If userLevel = "0" Or userLevel = "1" Or userLevel = "2" Or adminFlg = True Then
            ''''''    Dim shipName() As String
            ''''''    If Session("ship_name_list") IsNot Nothing Then
            ''''''        shipName = Session("ship_name_list")
            ''''''        With DropListLocation
            ''''''            .Items.Add("Select shipname")
            ''''''            For i = 0 To UBound(shipName)
            ''''''                If Trim(shipName(i)) <> "" Then
            ''''''                    .Items.Add(shipName(i))
            ''''''                End If
            ''''''            Next i
            ''''''        End With
            ''''''    End If

            ''''''Else
            ''''''    btnSend.Enabled = False
            ''''''End If

            ''''''***monthリストの設定***
            '''''DropDownMonth.Items.Clear()
            '''''    With DropDownMonth
            '''''    .Items.Add("Select the month")
            '''''    .Items.Add("January")
            '''''    .Items.Add("February")
            '''''    .Items.Add("March")
            '''''    .Items.Add("April")
            '''''    .Items.Add("May")
            '''''    .Items.Add("June")
            '''''    .Items.Add("July")
            '''''    .Items.Add("August")
            '''''    .Items.Add("September")
            '''''    .Items.Add("October")
            '''''    .Items.Add("November")
            '''''    .Items.Add("December")
            '''''End With

            '***exportFileリストの設定***
            DropDownExportFile.Items.Clear()
            With DropDownExportFile
                .Items.Add("Select Export Filename")
                .Items.Add("1.PL_Tracking_Sheet")
                .Items.Add("2.Sales_Register_from_GSPN_software_for_OOW")
                .Items.Add("3.Sales_Invoice_to_samsung_C_IW")
                .Items.Add("4.Sales_Invoiec_to_samsung_EXC_IW")
                .Items.Add("5.Sales_Register_from_GSPN_software_for_IW")
                .Items.Add("6.Sales_Register_from GSPN software_for_OthersSales")
                .Items.Add("7.SAW_Discount_Details")
                .Items.Add("8.Purchase_Register")
                .Items.Add("9.Final_Report")
            End With

        Else

            '***セッション設定***
            '月を指定
            Dim setMon As String = DropDownMonth.SelectedIndex.ToString("00")
            Dim setMonName As String = DropDownMonth.Text

            Session("set_Mon2") = setMon
            Session("set_MonName") = setMonName

            ''''''拠点を指定
            '''''Dim exportShipName As String = DropListLocation.Text
            '''''Session("export_shipName") = exportShipName

            'ダウンロードファイル種類を指定
            Dim exportFile As String = drpTaskExport.SelectedItem.Text
            Session("export_File") = exportFile

        End If

    End Sub
    Protected Sub OnPageIndexChanging(ByVal sender As Object, ByVal e As GridViewPageEventArgs)
        gvExportReport.PageIndex = e.NewPageIndex
        Dim dt As DataTable = CType(exfiledtl.ExportAnalysisDetails(Session("Exportinputdtl"), "R"), DataTable)
        Dim dv As DataView = New DataView(dt)
        If ViewState("SortExpression") Is Nothing Then
            dv.Sort = gvExportReport.Columns(0).SortExpression & " " & Me.SortDirection
        Else
            dv.Sort = ViewState("SortExpression") & " " & Me.SortDirection
        End If
        ' dv.Sort = "ServiceOrder_No " & Me.SortDirection
        gvExportReport.DataSource = dv
        gvExportReport.DataBind()
        '_ScDsrControl.ExportScDsr(Session("Exportinputdtl"))
        ' Me.BindGrid()
    End Sub

    Protected Sub OnSorting(ByVal sender As Object, ByVal e As GridViewSortEventArgs)
        If IsPostBack = True Then

            Dim dt As DataTable = CType(exfiledtl.ExportAnalysisDetails(Session("Exportinputdtl"), "R"), DataTable)
            If (Not (dt) Is Nothing) Then
                Dim dv As DataView = New DataView(dt)
                ViewState("SortExpression") = e.SortExpression
                'If (Not (e.SortDirection) Is Nothing) Then
                'Dim dv As DataView = dt.AsDataView
                Me.SortDirection = IIf(Me.SortDirection = "ASC", "DESC", "ASC")
                dv.Sort = e.SortExpression & " " & Me.SortDirection
                gvExportReport.DataSource = dv
                gvExportReport.DataBind()
                'Else
                '    gvExportReport.DataSource = dt
                'End If
                'dv.Sort = (e.SortExpression + (" " + ConvertSortDirectionToSql(e.SortDirection)))
                'gvExportReport.DataSource = dv
                'gvExportReport.DataBind()
            End If
        End If
        'https://www.aspsnippets.com/Articles/Ascending-Descending-Sorting-using-Columns-Header-in-ASPNet-GridView.aspx
        'https://www.codeproject.com/Articles/1195569/Angular-Data-Grid-with-Sorting-Filtering-Export-to
        'https://forums.asp.net/t/1412788.aspx?How+to+Export+GridView+To+Word+Excel+PDF+CSV+in+ASP+Net
        'Me.BindGrid(e.SortExpression)
        'Dim dt As DataTable = TryCast(gvExportReport.DataSource, DataTable)
        ' DataTable dt = gvExportReport.DataSource as DataTable

        'If Not dt Is Nothing Then
        '    If dt.Rows.Count > 0 Then
        '        Dim dv As DataView = TryCast(gvExportReport.DataSource, DataTable)(dt)
        '    End If
        'End If


        'DataView dv = New DataView(dt);
        '                dv.Sort = e.SortExpression + "" + ConvertSortDirectionToSql(e.SortDirection);
        '                GridView1.DataSource = dv;
        '                GridView1.DataBind();

    End Sub

    Public Sub GenerateExportDetails(ByVal repID As String)
        Dim dgColdtl As DataTable = exfiledtl.GetReportFieldDetails(drpTaskExport.SelectedValue)

        dgColdtl.DefaultView.Sort = "Id ASC"
        dgColdtl = dgColdtl.DefaultView.ToTable()
        gvExportReport.Columns.Clear()

        For i = 0 To dgColdtl.Rows.Count - 1

            If dgColdtl.Rows(i)(1).ToString().EndsWith("_Orig") = False Then
                Dim bfield As New BoundField()
                bfield.DataField = dgColdtl.Rows(i)(1)
                bfield.HeaderText = dgColdtl.Rows(i)(2)
                bfield.SortExpression = dgColdtl.Rows(i)(4)
                gvExportReport.Columns.Add(bfield)
            End If

            'gvExportReport.Columns.co
            'column.AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells
        Next

        'Dim Export_File_Details As exfiledtl = New Export_File_Details()

        'exfiledtl.FileName = strFileName
        'exfiledtl.ShipToBranch = DropListLocation.SelectedItem.Text
        'exfiledtl.ShipToBranchCode = DropListLocation.SelectedItem.Value
        'exfiledtl.ExportFile = drpTaskExport.SelectedValue
        'exfiledtl.UserId = userid
        'exfiledtl.UserName = userName
        'exfiledtl.DateFrom = _ScDsrModel.DateFrom
        'exfiledtl.DateTo = _ScDsrModel.DateTo


        Dim dtScDsrView As DataTable = exfiledtl.ExportAnalysisDetails(_Expflddtl, "R")
        'Session("Exportinputdtl") = dtScDsrView
        If Not dtScDsrView Is Nothing Then
            If dtScDsrView.Rows.Count > 0 Then
                txtPageSize.Visible = True
                lblPagesize.Visible = True
                txtPageSize.Text = 10
                gvExportReport.PageSize = Convert.ToInt16(txtPageSize.Text)
                gvExportReport.Visible = True
                gvExportReport.AllowSorting = True
                gvExportReport.PageIndex = 0
                gvExportReport.DataSource = dtScDsrView
                gvExportReport.DataBind()
                btnExport.Visible = True
                lbltotal.Text = "Total No of Records : " & dtScDsrView.Rows.Count
                lblTitle.Text = drpTaskExport.SelectedItem.Text
            Else
                gvExportReport.AllowSorting = False
                gvExportReport.DataBind()
                gvExportReport.Visible = False
                btnExport.Visible = False
                txtPageSize.Visible = False
                lblPagesize.Visible = False
            End If
        End If



        'lbltotal.Text = "Total No of Records : " & dtScDsrView.Rows.Count
        'lblTitle.Text = drpTaskExport.SelectedItem.Text
        'lblTitle.Width = drpTaskExport.SelectedItem.Text.Length + 100
        'gvExportReport.Visible = True
        'If dtScDsrView.Rows.Count = 0 Then
        ' Dim colIdx As Integer = 0
        'Do While (colIdx <gvExportReport.Columns.Count)
        'gvExportReport.AllowSorting = False
        'gvExportReport.DataBind()
        'gvExportReport.Visible = False
        'btnExport.Visible = False
        'txtPageSize.Visible = False
        'lblPagesize.Visible = False
        'gvExportReport.Columns(colIdx).SortMode = DataGridViewColumnSortMode.NotSortable
        'colIdx = (colIdx + 1)
        ' Loop


        '    dtScDsrView.Rows.Add(dtScDsrView.NewRow)
        '    gvExportReport.DataSource = dtScDsrView
        '    gvExportReport.DataBind()
        '    Dim totalcolums As Integer = gvExportReport.Rows(0).Cells.Count
        '    gvExportReport.Rows(0).Cells.Clear()
        '    gvExportReport.Rows(0).Cells..Add(New TableCell)
        '    gvExportReport.Rows(0).Cells(0).RowSpan = totalcolums
        '    'gvExportReport.Rows(0).Cells(0).Text = "No Record Found"
        'Else
        'txtPageSize.Visible = True
        'lblPagesize.Visible = True
        'txtPageSize.Text = 10
        'gvExportReport.PageSize = Convert.ToInt16(txtPageSize.Text)
        'gvExportReport.Visible = True
        'gvExportReport.AllowSorting = True
        'gvExportReport.PageIndex = 0
        'gvExportReport.DataBind()
        'btnExport.Visible = True

        'End If

    End Sub
    Private Function ConvertSortDirectionToSql(ByVal sortDirection As SortDirection) As String
        Dim newSortDirection As String = String.Empty
        Select Case (sortDirection)
            Case SortDirection.Ascending
                newSortDirection = "ASC"
            Case SortDirection.Descending
                newSortDirection = "DESC"
        End Select

        Return newSortDirection
    End Function
    Protected Sub btnSearch_Click(sender As Object, e As EventArgs) Handles btnSearch.Click
        Dim ShipName As String = Session("ship_Name")
        Dim shipCode As String = Session("ship_code")
        Dim userName As String = Session("user_Name")
        Dim userid As String = Session("user_id")
        Dim uploadShipname As String = DropListLocation.Text
        Dim uploadFilename As String = drpTaskExport.Text
        Dim listHistoryMsg() As String = Session("list_History_Msg")

        '***入力チェック***
        If ShipName = "" Then
            Call showMsg("The session was cleared. Please login again.", "")
            Exit Sub
        End If

        If DropListLocation.Text = "Select Branch" And drpTaskExport.SelectedValue <> "19B" Then
            Call showMsg("Please specify Target Branch.", "")
            Exit Sub
        End If

        If drpTaskExport.SelectedValue = "-1" Then
            Call showMsg("Please specify the file type", "")
            Exit Sub
        End If

        If (drpTaskExport.SelectedValue = "0") Or
            (drpTaskExport.SelectedValue = "17") Then
            'Comment on 20190828
            ''''''''''''''(drpTaskExport.SelectedValue = "1") Or
            ''(drpTaskExport.SelectedValue = "2.1") Or
            ''(drpTaskExport.SelectedValue = "2.2") Or
            ''(drpTaskExport.SelectedValue = "2.3") Or

            'Old functionaly process

            OldFunctionality()
            Exit Sub
        Else

            'Comment on 20190828
            If (drpTaskExport.SelectedValue = "1") Then

                Dim DtFrom As String = ""
                Dim DtTo As String = ""
                DtFrom = Trim(TextDateFrom.Text)
                DtTo = Trim(TextDateTo.Text)
                'Task = 0 then Month wise filter
                'Task = 1 then From & To
                If Trim(DtFrom) = "" And Trim(DtTo) = "" And DropDownMonth.SelectedValue = "0" Then
                    Call showMsg("Please specify either output period by month or date", "")
                    Exit Sub
                End If
                If Trim(DtFrom) <> "" And Trim(DtTo) <> "" And DropDownMonth.SelectedValue <> "0" Then
                    Call showMsg("Please specify either output period by month or date.", "")
                    Exit Sub
                End If
                If DropDownMonth.SelectedValue <> "0" Then
                    'Task = 1 'Assign From or To or both filter
                    If Len(Trim(DtFrom)) > 7 Or Len(Trim(DtTo)) > 7 Then
                        Call showMsg("Please specify either output period by month or date.", "")
                        Exit Sub
                    End If


                Else
                    ' Task = 1 'Assign From or To or both filter
                    If Len(Trim(DtFrom)) > 7 And Len(Trim(DtTo)) > 7 Then
                        Dim date1, date2 As Date
                        date1 = Date.Parse(TextDateFrom.Text)
                        date2 = Date.Parse(TextDateTo.Text)
                        If (DateTime.Compare(date1, date2) > 0) Then ' which means ("date1 > date2") 
                            Call showMsg("Please verify from date and to date", "")
                            Exit Sub
                        End If
                    End If
                    'Need to interchange 
                    'Allow for any one of From / To 
                    If Len(DtFrom) > 5 And DtTo = "" Then
                        DtTo = DtFrom
                    End If
                    If DtFrom = "" And Len(DtTo) > 5 Then
                        DtFrom = DtTo
                    End If
                End If
                'FromDate Verification 
                Dim dtTbl1, dtTbl2 As DateTime
                If (Trim(DtFrom) <> "") Then
                    If DateTime.TryParse(DtFrom, dtTbl1) Then
                        dtTbl1 = DateTime.Parse(Trim(DtFrom)).ToShortDateString
                    Else
                        Call showMsg("There is an error in the date specification", "")
                        Exit Sub
                    End If
                End If
                If (Trim(DtTo) <> "") Then
                    If DateTime.TryParse(DtTo, dtTbl2) Then
                        dtTbl2 = DateTime.Parse(Trim(DtTo)).ToShortDateString
                    Else
                        Call showMsg("There is an error in the date specification", "")
                        Exit Sub
                    End If
                End If

                'CommonControl.GetLastDayOfMonth(DropDownMonth.SelectedValue, DropDownYear.SelectedValue)
                '1st Find the records available on the specified by Month
                'Pass the paramters to Update in Datatabase
                Dim _ScDsrModel As ScDsrModel = New ScDsrModel()

                Dim strFileName As String = ""
                _ScDsrModel.UserId = userid
                _ScDsrModel.UserName = userName
                _ScDsrModel.ShipToBranchCode = DropListLocation.SelectedItem.Value
                _ScDsrModel.ShipToBranch = DropListLocation.SelectedItem.Text
                ''''''''''''''''''''   _DebitNoteModel.SrcFileName = _DebitNoteModel.ShipToBranch & "_DN" & DropDownYear.SelectedValue & DropDownMonth.SelectedValue & ".csv"



                If String.IsNullOrEmpty(DtFrom) Or String.IsNullOrEmpty(DtTo) Then ' Month format selected
                    'Need to find it by selected month
                    _ScDsrModel.DateFrom = DropDownYear.SelectedValue & "/" & DropDownMonth.SelectedValue & "/" & "01"
                    _ScDsrModel.DateTo = CommonControl.GetLastDayOfMonth(DropDownMonth.SelectedValue, DropDownYear.SelectedValue)
                    '' _InvoiceUpdateModel.SrcFileName = _InvoiceUpdateModel.ShipToBranch & "_DN" & DropDownYear.SelectedValue & DropDownMonth.SelectedValue & ".csv"
                    strFileName = _ScDsrModel.ShipToBranch & "_" & DropDownYear.SelectedValue & DropDownMonth.SelectedValue & ".csv"
                Else


                    'Verify Date Format 
                    Dim PositionStart As Integer = 0
                    If Len(Trim(DtFrom)) > 7 Then
                        PositionStart = InStr(1, DtFrom, "/")
                        If PositionStart = 0 Then ' There is no _ symbol in the file name
                            Call showMsg("Please verify date format (MM/DD/YYYY)", "")
                            Exit Sub
                        End If
                    End If
                    If Len(Trim(DtTo)) > 7 Then
                        PositionStart = InStr(1, DtTo, "/")
                        If PositionStart = 0 Then ' There is no _ symbol in the file name
                            Call showMsg("Please verify date format(MM/DD/YYYY)", "")
                            Exit Sub
                        End If
                    End If

                    'DateConversion
                    Dim DtConvFrom() As String
                    DtConvFrom = Split(DtFrom, "/")
                    If Len(DtConvFrom(0)) = 1 Then
                        DtConvFrom(0) = "0" & DtConvFrom(0)
                    End If
                    If Len(DtConvFrom(1)) = 1 Then
                        DtConvFrom(1) = "0" & DtConvFrom(1)
                    End If
                    _ScDsrModel.DateFrom = DtConvFrom(2) & "/" & DtConvFrom(0) & "/" & DtConvFrom(1)
                    'DateConversion
                    Dim DtConvTo() As String
                    DtConvTo = Split(DtTo, "/")
                    If Len(DtConvTo(0)) = 1 Then
                        DtConvTo(0) = "0" & DtConvTo(0)
                    End If
                    If Len(DtConvTo(1)) = 1 Then
                        DtConvTo(1) = "0" & DtConvTo(1)
                    End If
                    _ScDsrModel.DateTo = DtConvTo(2) & "/" & DtConvTo(0) & "/" & DtConvTo(1)
                    strFileName = _ScDsrModel.ShipToBranch & "_" & DtConvFrom(2) & DtConvFrom(0) & DtConvFrom(1) & "-" & DtConvTo(2) & DtConvTo(0) & DtConvTo(1) & ".csv"
                    ''  _GRecievedModel.SrcFileName = _GRecievedModel.ShipToBranch & "_GR" & DropDownYear.SelectedValue & DropDownMonth.SelectedValue & ".csv"
                End If

                _Expflddtl.FileName = strFileName
                _Expflddtl.ShipToBranch = DropListLocation.SelectedItem.Text
                _Expflddtl.ShipToBranchCode = DropListLocation.SelectedItem.Value
                _Expflddtl.ExportFile = drpTaskExport.SelectedValue
                _Expflddtl.UserId = userid
                _Expflddtl.UserName = userName
                _Expflddtl.DateFrom = _ScDsrModel.DateFrom
                _Expflddtl.DateTo = _ScDsrModel.DateTo

                Session("Exportinputdtl") = _Expflddtl
                ViewState("SortExpression") = Nothing
                ViewState("SortDirection") = Nothing
                GenerateExportDetails(drpTaskExport.SelectedValue)
                'If (dtScDsrView Is Nothing) Or (dtScDsrView.Rows.Count = 0) Then
                '    'If no Records
                '    Call showMsg("No records found for the month of " & DropDownMonth.SelectedItem.Text, "")
                '    Exit Sub
                'Else
                '    Response.ContentType = "text/csv"
                '    Response.AddHeader("Content-Disposition", "attachment;filename=" & strFileName)
                '    Response.AddHeader("Pragma", "no-cache")
                '    Response.AddHeader("Cache-Control", "no-cache")
                '    Dim myData As Byte() = CommonControl.csvBytesWriter(dtScDsrView)
                '    Response.BinaryWrite(myData)
                '    Response.Flush()
                '    Response.SuppressContent = True
                '    HttpContext.Current.ApplicationInstance.CompleteRequest()
                'End If









                'Modified on 20190801
                'Task No. 3A, 3B, 4, 5, 6,7,8,9,10,11,12,13,14,15,16,17
                'Dim srcFileName As String = ""
            ElseIf drpTaskExport.SelectedValue = "2" Then

                Dim DtFrom As String = ""
                Dim DtTo As String = ""
                DtFrom = Trim(TextDateFrom.Text)
                DtTo = Trim(TextDateTo.Text)
                'Task = 0 then Month wise filter
                'Task = 1 then From & To
                If Trim(DtFrom) = "" And Trim(DtTo) = "" And DropDownMonth.SelectedValue = "0" Then
                    Call showMsg("Please specify either output period by month or date", "")
                    Exit Sub
                End If
                If Trim(DtFrom) <> "" And Trim(DtTo) <> "" And DropDownMonth.SelectedValue <> "0" Then
                    Call showMsg("Please specify either output period by month or date.", "")
                    Exit Sub
                End If
                If DropDownMonth.SelectedValue <> "0" Then
                    'Task = 1 'Assign From or To or both filter
                    If Len(Trim(DtFrom)) > 7 Or Len(Trim(DtTo)) > 7 Then
                        Call showMsg("Please specify either output period by month or date.", "")
                        Exit Sub
                    End If


                Else
                    ' Task = 1 'Assign From or To or both filter
                    If Len(Trim(DtFrom)) > 7 And Len(Trim(DtTo)) > 7 Then
                        Dim date1, date2 As Date
                        date1 = Date.Parse(TextDateFrom.Text)
                        date2 = Date.Parse(TextDateTo.Text)
                        If (DateTime.Compare(date1, date2) > 0) Then ' which means ("date1 > date2") 
                            Call showMsg("Please verify from date and to date", "")
                            Exit Sub
                        End If
                    End If
                    'Need to interchange 
                    'Allow for any one of From / To 
                    If Len(DtFrom) > 5 And DtTo = "" Then
                        DtTo = DtFrom
                    End If
                    If DtFrom = "" And Len(DtTo) > 5 Then
                        DtFrom = DtTo
                    End If
                End If
                'FromDate Verification 
                Dim dtTbl1, dtTbl2 As DateTime
                If (Trim(DtFrom) <> "") Then
                    If DateTime.TryParse(DtFrom, dtTbl1) Then
                        dtTbl1 = DateTime.Parse(Trim(DtFrom)).ToShortDateString
                    Else
                        Call showMsg("There is an error in the date specification", "")
                        Exit Sub
                    End If
                End If
                If (Trim(DtTo) <> "") Then
                    If DateTime.TryParse(DtTo, dtTbl2) Then
                        dtTbl2 = DateTime.Parse(Trim(DtTo)).ToShortDateString
                    Else
                        Call showMsg("There is an error in the date specification", "")
                        Exit Sub
                    End If
                End If
                'CommonControl.GetLastDayOfMonth(DropDownMonth.SelectedValue, DropDownYear.SelectedValue)
                '1st Find the records available on the specified by Month
                'Pass the paramters to Update in Datatabase
                Dim _SalesReportModel As SalesReportModel = New SalesReportModel()
                Dim _SalesReportControl As SalesReportControl = New SalesReportControl()
                Dim strFileName As String = ""
                _SalesReportModel.UserId = userid
                _SalesReportModel.UserName = userName
                _SalesReportModel.ShipToBranchCode = DropListLocation.SelectedItem.Value
                _SalesReportModel.ShipToBranch = DropListLocation.SelectedItem.Text
                '''''''''''''''''''   _DebitNoteModel.SrcFileName = _DebitNoteModel.ShipToBranch & "_DN" & DropDownYear.SelectedValue & DropDownMonth.SelectedValue & ".csv"
                '''
                'Pass type to identify the data
                'Select Case drpTaskExport.SelectedValue
                '    Case "2.1"
                _SalesReportModel.SalesReportType = "Warranty"
                '    Case "2.2"
                '        _SalesReportModel.SalesReportType = "OutWarranty"
                '    Case "2.3"
                '        _SalesReportModel.SalesReportType = "OtherSales"
                'End Select

                If String.IsNullOrEmpty(DtFrom) Or String.IsNullOrEmpty(DtTo) Then ' Month format selected
                    'Need to find it by selected month
                    _SalesReportModel.DateFrom = DropDownYear.SelectedValue & "/" & DropDownMonth.SelectedValue & "/" & "01"
                    _SalesReportModel.DateTo = CommonControl.GetLastDayOfMonth(DropDownMonth.SelectedValue, DropDownYear.SelectedValue)
                    '' _InvoiceUpdateModel.SrcFileName = _InvoiceUpdateModel.ShipToBranch & "_DN" & DropDownYear.SelectedValue & DropDownMonth.SelectedValue & ".csv"
                    strFileName = _SalesReportModel.ShipToBranch & "_" & _SalesReportModel.Number & DropDownYear.SelectedValue & DropDownMonth.SelectedValue & ".csv"
                Else


                    'Verify Date Format 
                    Dim PositionStart As Integer = 0
                    If Len(Trim(DtFrom)) > 7 Then
                        PositionStart = InStr(1, DtFrom, "/")
                        If PositionStart = 0 Then ' There is no _ symbol in the file name
                            Call showMsg("Please verify date format (MM/DD/YYYY)", "")
                            Exit Sub
                        End If
                    End If
                    If Len(Trim(DtTo)) > 7 Then
                        PositionStart = InStr(1, DtTo, "/")
                        If PositionStart = 0 Then ' There is no _ symbol in the file name
                            Call showMsg("Please verify date format(MM/DD/YYYY)", "")
                            Exit Sub
                        End If
                    End If

                    'DateConversion
                    Dim DtConvFrom() As String
                    DtConvFrom = Split(DtFrom, "/")
                    If Len(DtConvFrom(0)) = 1 Then
                        DtConvFrom(0) = "0" & DtConvFrom(0)
                    End If
                    If Len(DtConvFrom(1)) = 1 Then
                        DtConvFrom(1) = "0" & DtConvFrom(1)
                    End If
                    _SalesReportModel.DateFrom = DtConvFrom(2) & "/" & DtConvFrom(0) & "/" & DtConvFrom(1)
                    'DateConversion
                    Dim DtConvTo() As String
                    DtConvTo = Split(DtTo, "/")
                    If Len(DtConvTo(0)) = 1 Then
                        DtConvTo(0) = "0" & DtConvTo(0)
                    End If
                    If Len(DtConvTo(1)) = 1 Then
                        DtConvTo(1) = "0" & DtConvTo(1)
                    End If
                    _SalesReportModel.DateTo = DtConvTo(2) & "/" & DtConvTo(0) & "/" & DtConvTo(1)
                    strFileName = _SalesReportModel.ShipToBranch & "_" & _SalesReportModel.Number & DtConvFrom(2) & DtConvFrom(0) & DtConvFrom(1) & "-" & DtConvTo(2) & DtConvTo(0) & DtConvTo(1) & ".csv"
                    ''  _GRecievedModel.SrcFileName = _GRecievedModel.ShipToBranch & "_GR" & DropDownYear.SelectedValue & DropDownMonth.SelectedValue & ".csv"
                End If

                'Dim dgColdtl As DataTable = _Expflddtl.GetReportFieldDetails(drpTaskExport.SelectedValue)

                _Expflddtl.FileName = strFileName
                _Expflddtl.ShipToBranch = DropListLocation.SelectedItem.Text
                _Expflddtl.ShipToBranchCode = DropListLocation.SelectedItem.Value
                _Expflddtl.ExportFile = drpTaskExport.SelectedValue
                _Expflddtl.UserId = userid
                _Expflddtl.UserName = userName
                _Expflddtl.DateFrom = _SalesReportModel.DateFrom
                _Expflddtl.DateTo = _SalesReportModel.DateTo

                Session("Exportinputdtl") = _Expflddtl

                ViewState("SortExpression") = Nothing
                ViewState("SortDirection") = Nothing
                GenerateExportDetails(drpTaskExport.SelectedValue)
                'GenerateExportDetails(drpTaskExport.SelectedValue)

                'Temporary..........If 
                ''''''''''''''''''''''''Please remove after 3C done the coding
                '=========================================================================
                '========================================================================
                ''''''''''''''''''''''''''''''''''''''''''''''
                '''''''''''''''''''''''''
                '''
                'If (drpTaskExport.SelectedValue = "2.1") Or (drpTaskExport.SelectedValue = "2.2") Then ''''Remove after 3C coding completed
                '    Dim dtSalesReportView As DataTable = _SalesReportControl.SelectSalesReport(_SalesReportModel)
                '    If (dtSalesReportView Is Nothing) Or (dtSalesReportView.Rows.Count = 0) Then
                '        'If no Records
                '        Call showMsg("No records found for the month of " & DropDownMonth.SelectedItem.Text, "")
                '        Exit Sub
                '    Else
                '        Response.ContentType = "text/csv"
                '        Response.AddHeader("Content-Disposition", "attachment;filename=" & strFileName)
                '        Response.AddHeader("Pragma", "no-cache")
                '        Response.AddHeader("Cache-Control", "no-cache")
                '        Dim myData As Byte() = CommonControl.csvBytesWriter(dtSalesReportView)
                '        Response.BinaryWrite(myData)
                '        Response.Flush()
                '        Response.SuppressContent = True
                '        HttpContext.Current.ApplicationInstance.CompleteRequest()
                '    End If
                'Else '''Remove after 3C completed
                '    ''''Call showMsg("Coming Soon.", "")
                '    ''''Exit Sub
                '    '''
                '    Dim dtSalesReportView As DataTable = _SalesReportControl.SelectSalesReport(_SalesReportModel)
                '    If (dtSalesReportView Is Nothing) Or (dtSalesReportView.Rows.Count = 0) Then
                '        'If no Records
                '        Call showMsg("No records found for the month of " & DropDownMonth.SelectedItem.Text, "")
                '        Exit Sub
                '    Else
                '        Response.ContentType = "text/csv"
                '        Response.AddHeader("Content-Disposition", "attachment;filename=" & strFileName)
                '        Response.AddHeader("Pragma", "no-cache")
                '        Response.AddHeader("Cache-Control", "no-cache")
                '        Dim myData As Byte() = CommonControl.csvBytesWriter(dtSalesReportView)
                '        Response.BinaryWrite(myData)
                '        Response.Flush()
                '        Response.SuppressContent = True
                '        HttpContext.Current.ApplicationInstance.CompleteRequest()
                '    End If


                'End If '''Remove after 3C completed


            ElseIf (drpTaskExport.SelectedValue = "3A") Or (drpTaskExport.SelectedValue = "3B") Or (drpTaskExport.SelectedValue = "4") Then
                '********** Task 3A Start
                Dim DtFrom As String = ""
                Dim DtTo As String = ""
                DtFrom = Trim(TextDateFrom.Text)
                DtTo = Trim(TextDateTo.Text)
                'Task = 0 then Month wise filter
                'Task = 1 then From & To
                If Trim(DtFrom) = "" And Trim(DtTo) = "" And DropDownMonth.SelectedValue = "0" Then
                    Call showMsg("Please specify either output period by month or date", "")
                    Exit Sub
                End If
                If Trim(DtFrom) <> "" And Trim(DtTo) <> "" And DropDownMonth.SelectedValue <> "0" Then
                    Call showMsg("Please specify either output period by month or date.", "")
                    Exit Sub
                End If
                If DropDownMonth.SelectedValue <> "0" Then
                    'Task = 1 'Assign From or To or both filter
                    If Len(Trim(DtFrom)) > 7 Or Len(Trim(DtTo)) > 7 Then
                        Call showMsg("Please specify either output period by month or date.", "")
                        Exit Sub
                    End If


                Else
                    ' Task = 1 'Assign From or To or both filter
                    If Len(Trim(DtFrom)) > 7 And Len(Trim(DtTo)) > 7 Then
                        Dim date1, date2 As Date
                        date1 = Date.Parse(TextDateFrom.Text)
                        date2 = Date.Parse(TextDateTo.Text)
                        If (DateTime.Compare(date1, date2) > 0) Then ' which means ("date1 > date2") 
                            Call showMsg("Please verify from date and to date", "")
                            Exit Sub
                        End If
                    End If
                    'Need to interchange 
                    'Allow for any one of From / To 
                    If Len(DtFrom) > 5 And DtTo = "" Then
                        DtTo = DtFrom
                    End If
                    If DtFrom = "" And Len(DtTo) > 5 Then
                        DtFrom = DtTo
                    End If
                End If
                'FromDate Verification 
                Dim dtTbl1, dtTbl2 As DateTime
                If (Trim(DtFrom) <> "") Then
                    If DateTime.TryParse(DtFrom, dtTbl1) Then
                        dtTbl1 = DateTime.Parse(Trim(DtFrom)).ToShortDateString
                    Else
                        Call showMsg("There is an error in the date specification", "")
                        Exit Sub
                    End If
                End If
                If (Trim(DtTo) <> "") Then
                    If DateTime.TryParse(DtTo, dtTbl2) Then
                        dtTbl2 = DateTime.Parse(Trim(DtTo)).ToShortDateString
                    Else
                        Call showMsg("There is an error in the date specification", "")
                        Exit Sub
                    End If
                End If

                'CommonControl.GetLastDayOfMonth(DropDownMonth.SelectedValue, DropDownYear.SelectedValue)
                '1st Find the records available on the specified by Month
                'Pass the paramters to Update in Datatabase
                Dim _InvoiceUpdateModel As InvoiceUpdateModel = New InvoiceUpdateModel()
                Dim _InvoiceUpdateControl As InvoiceUpdateControl = New InvoiceUpdateControl()
                Dim strFileName As String = ""
                _InvoiceUpdateModel.UserId = userid
                _InvoiceUpdateModel.UserName = userName
                _InvoiceUpdateModel.ShipToBranchCode = DropListLocation.SelectedItem.Value
                _InvoiceUpdateModel.ShipToBranch = DropListLocation.SelectedItem.Text
                '''''''''''''''''''   _DebitNoteModel.SrcFileName = _DebitNoteModel.ShipToBranch & "_DN" & DropDownYear.SelectedValue & DropDownMonth.SelectedValue & ".csv"
                '''
                'Pass Number to identify the data
                Dim strType As String = ""
                Select Case drpTaskExport.SelectedValue
                    Case "3A"
                        _InvoiceUpdateModel.Number = "C"
                        strType = "IW"
                    Case "3B"
                        _InvoiceUpdateModel.Number = "EXC"
                        strType = "IW"
                    Case "4"
                        _InvoiceUpdateModel.Number = "OWC"
                        strType = "OOW"
                End Select

                If String.IsNullOrEmpty(DtFrom) Or String.IsNullOrEmpty(DtTo) Then ' Month format selected
                    'Need to find it by selected month
                    _InvoiceUpdateModel.DateFrom = DropDownYear.SelectedValue & "/" & DropDownMonth.SelectedValue & "/" & "01"
                    _InvoiceUpdateModel.DateTo = CommonControl.GetLastDayOfMonth(DropDownMonth.SelectedValue, DropDownYear.SelectedValue)
                    '' _InvoiceUpdateModel.SrcFileName = _InvoiceUpdateModel.ShipToBranch & "_DN" & DropDownYear.SelectedValue & DropDownMonth.SelectedValue & ".csv"
                    strFileName = _InvoiceUpdateModel.ShipToBranch & "_" & _InvoiceUpdateModel.Number & DropDownYear.SelectedValue & DropDownMonth.SelectedValue & ".csv"
                Else


                    'Verify Date Format 
                    Dim PositionStart As Integer = 0
                    If Len(Trim(DtFrom)) > 7 Then
                        PositionStart = InStr(1, DtFrom, "/")
                        If PositionStart = 0 Then ' There is no _ symbol in the file name
                            Call showMsg("Please verify date format (MM/DD/YYYY)", "")
                            Exit Sub
                        End If
                    End If
                    If Len(Trim(DtTo)) > 7 Then
                        PositionStart = InStr(1, DtTo, "/")
                        If PositionStart = 0 Then ' There is no _ symbol in the file name
                            Call showMsg("Please verify date format(MM/DD/YYYY)", "")
                            Exit Sub
                        End If
                    End If

                    'DateConversion
                    Dim DtConvFrom() As String
                    DtConvFrom = Split(DtFrom, "/")
                    If Len(DtConvFrom(0)) = 1 Then
                        DtConvFrom(0) = "0" & DtConvFrom(0)
                    End If
                    If Len(DtConvFrom(1)) = 1 Then
                        DtConvFrom(1) = "0" & DtConvFrom(1)
                    End If
                    _InvoiceUpdateModel.DateFrom = DtConvFrom(2) & "/" & DtConvFrom(0) & "/" & DtConvFrom(1)
                    'DateConversion
                    Dim DtConvTo() As String
                    DtConvTo = Split(DtTo, "/")
                    If Len(DtConvTo(0)) = 1 Then
                        DtConvTo(0) = "0" & DtConvTo(0)
                    End If
                    If Len(DtConvTo(1)) = 1 Then
                        DtConvTo(1) = "0" & DtConvTo(1)
                    End If
                    _InvoiceUpdateModel.DateTo = DtConvTo(2) & "/" & DtConvTo(0) & "/" & DtConvTo(1)
                    strFileName = _InvoiceUpdateModel.ShipToBranch & "_" & _InvoiceUpdateModel.Number & DtConvFrom(2) & DtConvFrom(0) & DtConvFrom(1) & "-" & DtConvTo(2) & DtConvTo(0) & DtConvTo(1) & ".csv"
                    ''  _GRecievedModel.SrcFileName = _GRecievedModel.ShipToBranch & "_GR" & DropDownYear.SelectedValue & DropDownMonth.SelectedValue & ".csv"
                End If

                _Expflddtl.FileName = strFileName
                _Expflddtl.ShipToBranch = DropListLocation.SelectedItem.Text
                _Expflddtl.ShipToBranchCode = DropListLocation.SelectedItem.Value
                _Expflddtl.ExportFile = drpTaskExport.SelectedValue
                _Expflddtl.UserId = userid
                _Expflddtl.UserName = userName
                _Expflddtl.DateFrom = _InvoiceUpdateModel.DateFrom
                _Expflddtl.DateTo = _InvoiceUpdateModel.DateTo
                _Expflddtl.number = _InvoiceUpdateModel.Number
                Session("Exportinputdtl") = _Expflddtl

                ViewState("SortExpression") = Nothing
                ViewState("SortDirection") = Nothing
                GenerateExportDetails(drpTaskExport.SelectedValue)


                'Dim dtInvoiceUpdateView As DataTable = _InvoiceUpdateControl.SelectInvoiceUpdate(_InvoiceUpdateModel)
                'If (dtInvoiceUpdateView Is Nothing) Or (dtInvoiceUpdateView.Rows.Count = 0) Then
                '    'If no Records
                '    Call showMsg("No records found for the month of " & DropDownMonth.SelectedItem.Text, "")
                '    Exit Sub
                'Else
                '    Response.ContentType = "text/csv"
                '    Response.AddHeader("Content-Disposition", "attachment;filename=" & strFileName)
                '    Response.AddHeader("Pragma", "no-cache")
                '    Response.AddHeader("Cache-Control", "no-cache")
                '    Dim myData As Byte() = CommonControl.csvBytesWriter(dtInvoiceUpdateView)
                '    Response.BinaryWrite(myData)
                '    Response.Flush()
                '    Response.SuppressContent = True
                '    HttpContext.Current.ApplicationInstance.CompleteRequest()
                'End If
                '********** Task 3A End
                '************************************************************************
                'ElseIf drpTaskExport.SelectedValue = "-3" Then

                'ElseIf drpTaskExport.SelectedValue = "4" Then


            ElseIf drpTaskExport.SelectedValue = "5" Then

                '''''''''''Verify Month is selected
                ''''''''''If DropDownMonth.SelectedValue = "0" Then
                ''''''''''    Call showMsg("Please select the month", "")
                ''''''''''    Exit Sub
                ''''''''''End If
                '''''''''''CommonControl.GetLastDayOfMonth(DropDownMonth.SelectedValue, DropDownYear.SelectedValue)
                '''''''''''1st Find the records available on the specified by Month
                '''''''''''Pass the paramters to Update in Datatabase
                ''''''''''Dim _OtherUpdateModel As OtherUpdateModel = New OtherUpdateModel()
                ''''''''''Dim _OtherUpdateControl As OtherUpdateControl = New OtherUpdateControl()
                ''''''''''_OtherUpdateModel.UserId = userid
                ''''''''''_OtherUpdateModel.UserName = userName
                ''''''''''_OtherUpdateModel.ShipToBranchCode = DropListLocation.SelectedItem.Value
                ''''''''''_OtherUpdateModel.ShipToBranch = DropListLocation.SelectedItem.Text
                ''''''''''_OtherUpdateModel.SrcFileName = _OtherUpdateModel.ShipToBranch & "_OU" & DropDownYear.SelectedValue & DropDownMonth.SelectedValue & ".csv"
                '''''''''''_OtherUpdateModel.DateFrom = DropDownYear.SelectedValue & "/" & DropDownMonth.SelectedValue & "/01"
                '''''''''''_OtherUpdateModel.DateTo = CommonControl.GetLastDayOfMonth(DropDownMonth.SelectedValue, DropDownYear.SelectedValue)
                ''''''''''Dim dtOtherUpdateView As DataTable = _OtherUpdateControl.SelectOtherUpdate(_OtherUpdateModel)
                ''''''''''If (dtOtherUpdateView Is Nothing) Or (dtOtherUpdateView.Rows.Count = 0) Then
                ''''''''''    'If no Records
                ''''''''''    Call showMsg("No records found for the month of " & DropDownMonth.SelectedItem.Text, "")
                ''''''''''    Exit Sub
                ''''''''''Else
                ''''''''''    Response.ContentType = "text/csv"
                ''''''''''    Response.AddHeader("Content-Disposition", "attachment;filename=" & DropListLocation.SelectedItem.Text & "_OU" & DropDownYear.SelectedValue & DropDownMonth.SelectedValue & ".csv")
                ''''''''''    Response.AddHeader("Pragma", "no-cache")
                ''''''''''    Response.AddHeader("Cache-Control", "no-cache")
                ''''''''''    Dim myData As Byte() = CommonControl.csvBytesWriter(dtOtherUpdateView)
                ''''''''''    Response.BinaryWrite(myData)
                ''''''''''    Response.Flush()
                ''''''''''    Response.SuppressContent = True
                ''''''''''    HttpContext.Current.ApplicationInstance.CompleteRequest()

                ''''''''''End If
                Dim DtFrom As String = ""
                Dim DtTo As String = ""
                DtFrom = Trim(TextDateFrom.Text)
                DtTo = Trim(TextDateTo.Text)
                'Task = 0 then Month wise filter
                'Task = 1 then From & To
                If Trim(DtFrom) = "" And Trim(DtTo) = "" And DropDownMonth.SelectedValue = "0" Then
                    Call showMsg("Please specify either output period by month or date", "")
                    Exit Sub
                End If
                If Trim(DtFrom) <> "" And Trim(DtTo) <> "" And DropDownMonth.SelectedValue <> "0" Then
                    Call showMsg("Please specify either output period by month or date.", "")
                    Exit Sub
                End If
                If DropDownMonth.SelectedValue <> "0" Then
                    'Task = 1 'Assign From or To or both filter
                    If Len(Trim(DtFrom)) > 7 Or Len(Trim(DtTo)) > 7 Then
                        Call showMsg("Please specify either output period by month or date.", "")
                        Exit Sub
                    End If
                Else
                    ' Task = 1 'Assign From or To or both filter
                    If Len(Trim(DtFrom)) > 7 And Len(Trim(DtTo)) > 7 Then
                        Dim date1, date2 As Date
                        date1 = Date.Parse(TextDateFrom.Text)
                        date2 = Date.Parse(TextDateTo.Text)
                        If (DateTime.Compare(date1, date2) > 0) Then ' which means ("date1 > date2") 
                            Call showMsg("Please verify from date and to date", "")
                            Exit Sub
                        End If
                    End If
                    'Need to interchange 
                    'Allow for any one of From / To 
                    If Len(DtFrom) > 5 And DtTo = "" Then
                        DtTo = DtFrom
                    End If
                    If DtFrom = "" And Len(DtTo) > 5 Then
                        DtFrom = DtTo
                    End If
                End If


                '''Verify Month is selected
                ''If (Trim(TextDateFrom.Text) = "") And (Trim(TextDateTo.Text) = "") Then
                ''    If (DropDownMonth.SelectedValue = "0") Then
                ''        Call showMsg("Please select the month", "")
                ''        Exit Sub
                ''    End If
                ''End If
                'FromDate Verification 
                Dim dtTbl1, dtTbl2 As DateTime
                If (Trim(DtFrom) <> "") Then
                    If DateTime.TryParse(DtFrom, dtTbl1) Then
                        dtTbl1 = DateTime.Parse(Trim(DtFrom)).ToShortDateString
                    Else
                        Call showMsg("There is an error in the date specification", "")
                        Exit Sub
                    End If
                End If
                If (Trim(DtTo) <> "") Then
                    If DateTime.TryParse(DtTo, dtTbl2) Then
                        dtTbl2 = DateTime.Parse(Trim(DtTo)).ToShortDateString
                    Else
                        Call showMsg("There is an error in the date specification", "")
                        Exit Sub
                    End If
                End If

                'CommonControl.GetLastDayOfMonth(DropDownMonth.SelectedValue, DropDownYear.SelectedValue)
                '1st Find the records available on the specified by Month
                'Pass the paramters to Update in Datatabase
                Dim _DebitNoteModel As DebitNoteModel = New DebitNoteModel()
                Dim _DebitNoteControl As DebitNoteControl = New DebitNoteControl()
                Dim strFileName As String = ""
                _DebitNoteModel.UserId = userid
                _DebitNoteModel.UserName = userName
                _DebitNoteModel.ShipToBranchCode = DropListLocation.SelectedItem.Value
                _DebitNoteModel.ShipToBranch = DropListLocation.SelectedItem.Text
                '''''''''''''''''''   _DebitNoteModel.SrcFileName = _DebitNoteModel.ShipToBranch & "_DN" & DropDownYear.SelectedValue & DropDownMonth.SelectedValue & ".csv"

                If String.IsNullOrEmpty(DtFrom) Or String.IsNullOrEmpty(DtTo) Then ' Month format selected
                    'Need to find it by selected month
                    _DebitNoteModel.DateFrom = DropDownYear.SelectedValue & "/" & DropDownMonth.SelectedValue & "/" & "01"
                    _DebitNoteModel.DateTo = CommonControl.GetLastDayOfMonth(DropDownMonth.SelectedValue, DropDownYear.SelectedValue)
                    _DebitNoteModel.SrcFileName = _DebitNoteModel.ShipToBranch & "_DN" & DropDownYear.SelectedValue & DropDownMonth.SelectedValue & ".csv"
                    strFileName = _DebitNoteModel.SrcFileName
                Else

                    'Verify Date Format 
                    Dim PositionStart As Integer = 0
                    If Len(Trim(DtFrom)) > 7 Then
                        PositionStart = InStr(1, DtFrom, "/")
                        If PositionStart = 0 Then ' There is no _ symbol in the file name
                            Call showMsg("Please verify date format (MM/DD/YYYY)", "")
                            Exit Sub
                        End If
                    End If
                    If Len(Trim(DtTo)) > 7 Then
                        PositionStart = InStr(1, DtTo, "/")
                        If PositionStart = 0 Then ' There is no _ symbol in the file name
                            Call showMsg("Please verify date format(MM/DD/YYYY)", "")
                            Exit Sub
                        End If
                    End If

                    'DateConversion
                    Dim DtConvFrom() As String
                    DtConvFrom = Split(DtFrom, "/")
                    If Len(DtConvFrom(0)) = 1 Then
                        DtConvFrom(0) = "0" & DtConvFrom(0)
                    End If
                    If Len(DtConvFrom(1)) = 1 Then
                        DtConvFrom(1) = "0" & DtConvFrom(1)
                    End If
                    _DebitNoteModel.DateFrom = DtConvFrom(2) & "/" & DtConvFrom(0) & "/" & DtConvFrom(1)
                    'DateConversion
                    Dim DtConvTo() As String
                    DtConvTo = Split(DtTo, "/")
                    If Len(DtConvTo(0)) = 1 Then
                        DtConvTo(0) = "0" & DtConvTo(0)
                    End If
                    If Len(DtConvTo(1)) = 1 Then
                        DtConvTo(1) = "0" & DtConvTo(1)
                    End If
                    _DebitNoteModel.DateTo = DtConvTo(2) & "/" & DtConvTo(0) & "/" & DtConvTo(1)
                    strFileName = _DebitNoteModel.ShipToBranch & "_DB" & DtConvFrom(2) & DtConvFrom(0) & DtConvFrom(1) & "-" & DtConvTo(2) & DtConvTo(0) & DtConvTo(1) & ".csv"
                    ''  _GRecievedModel.SrcFileName = _GRecievedModel.ShipToBranch & "_GR" & DropDownYear.SelectedValue & DropDownMonth.SelectedValue & ".csv"
                End If



                '_OtherUpdateModel.DateFrom = DropDownYear.SelectedValue & "/" & DropDownMonth.SelectedValue & "/01"
                '_OtherUpdateModel.DateTo = CommonControl.GetLastDayOfMonth(DropDownMonth.SelectedValue, DropDownYear.SelectedValue)
                Dim dtDebitNoteView As DataTable = _DebitNoteControl.SelectDebitNote(_DebitNoteModel)
                If (dtDebitNoteView Is Nothing) Or (dtDebitNoteView.Rows.Count = 0) Then
                    'If no Records
                    Call showMsg("No records found for the month of " & DropDownMonth.SelectedItem.Text, "")
                    Exit Sub
                Else
                    Response.ContentType = "text/csv"
                    Response.AddHeader("Content-Disposition", "attachment;filename=" & DropListLocation.SelectedItem.Text & "_DN" & DropDownYear.SelectedValue & DropDownMonth.SelectedValue & ".csv")
                    Response.AddHeader("Pragma", "no-cache")
                    Response.AddHeader("Cache-Control", "no-cache")
                    Dim myData As Byte() = CommonControl.csvBytesWriter(dtDebitNoteView)
                    Response.BinaryWrite(myData)
                    Response.Flush()
                    Response.SuppressContent = True
                    HttpContext.Current.ApplicationInstance.CompleteRequest()
                End If




            ElseIf drpTaskExport.SelectedValue = "6" Then
                'Verify Month is selected
                If DropDownMonth.SelectedValue = "0" Then
                    Call showMsg("Please select the month", "")
                    Exit Sub
                End If
                If DropDownDaySub.SelectedValue = "0" Then
                    Call showMsg("Please select the day", "")
                    Exit Sub
                End If
                'Pass the paramters to Update in Datatabase
                Dim _PrSummaryModel As PrSummaryModel = New PrSummaryModel()
                Dim _PrSummaryControl As PrSummaryControl = New PrSummaryControl()
                _PrSummaryModel.UserId = userid
                _PrSummaryModel.UserName = userName
                _PrSummaryModel.ShipToBranchCode = DropListLocation.SelectedItem.Value
                _PrSummaryModel.ShipToBranch = DropListLocation.SelectedItem.Text
                _PrSummaryModel.SrcFileName = _PrSummaryModel.ShipToBranch & "_SM" & DropDownYear.SelectedValue & DropDownMonth.SelectedValue & DropDownDaySub.SelectedValue & ".csv"
                ' _PrSummaryModel.DateFrom = DropDownYear.SelectedValue & "/" & DropDownMonth.SelectedValue & "/01"
                '  _PrSummaryModel.DateTo = CommonControl.GetLastDayOfMonth(DropDownMonth.SelectedValue, DropDownYear.SelectedValue)

                _Expflddtl.FileName = _PrSummaryModel.SrcFileName
                _Expflddtl.SrcFileName = _PrSummaryModel.SrcFileName
                _Expflddtl.ShipToBranch = DropListLocation.SelectedItem.Text
                _Expflddtl.ShipToBranchCode = DropListLocation.SelectedItem.Value
                _Expflddtl.ExportFile = drpTaskExport.SelectedValue
                _Expflddtl.UserId = userid
                _Expflddtl.UserName = userName
                'exfiledtl.DateFrom = _InvoiceUpdateModel.DateFrom
                'exfiledtl.DateTo = _InvoiceUpdateModel.DateTo
                'exfiledtl.number = _InvoiceUpdateModel.Number
                Session("Exportinputdtl") = _Expflddtl

                ViewState("SortExpression") = Nothing
                ViewState("SortDirection") = Nothing
                GenerateExportDetails(drpTaskExport.SelectedValue)
                'Dim dtPrSummaryView As DataTable = _PrSummaryControl.SelectPrSummary(_PrSummaryModel)
                'If (dtPrSummaryView Is Nothing) Or (dtPrSummaryView.Rows.Count = 0) Then
                '    'If no Records
                '    Call showMsg("No records found for the month of " & DropDownMonth.SelectedItem.Text, "")
                '    Exit Sub
                'Else
                '    Response.ContentType = "text/csv"
                '    Response.AddHeader("Content-Disposition", "attachment;filename=" & _PrSummaryModel.SrcFileName)
                '    Response.AddHeader("Pragma", "no-cache")
                '    Response.AddHeader("Cache-Control", "no-cache")
                '    Dim myData As Byte() = CommonControl.csvBytesWriter(dtPrSummaryView)
                '    Response.BinaryWrite(myData)
                '    Response.Flush()
                '    Response.SuppressContent = True
                '    HttpContext.Current.ApplicationInstance.CompleteRequest()

                'End If

            ElseIf drpTaskExport.SelectedValue = "7" Then
                'Verify Month is selected
                If DropDownMonth.SelectedValue = "0" Then
                    Call showMsg("Please select the month", "")
                    Exit Sub
                End If
                If DropDownDaySub.SelectedValue = "0" Then
                    Call showMsg("Please select the day", "")
                    Exit Sub
                End If
                'If DropDownDTSub.SelectedValue = "0" Then VJ 2019/10/24
                '    Call showMsg("Please select the DT", "")
                '    Exit Sub
                'End If ' VJ 2019/10/24
                'Pass the paramters to Update in Datatabase
                Dim DTsub As String
                If DropDownDTSub.SelectedValue = "0" Then
                    DTsub = "DT1DT2"
                Else
                    DTsub = DropDownDTSub.SelectedValue
                End If

                Dim _PrDetailModel As PrDetailModel = New PrDetailModel()
                Dim _PrDetailControl As PrDetailControl = New PrDetailControl()
                _PrDetailModel.UserId = userid
                _PrDetailModel.UserName = userName
                _PrDetailModel.ShipToBranchCode = DropListLocation.SelectedItem.Value
                _PrDetailModel.ShipToBranch = DropListLocation.SelectedItem.Text
                _PrDetailModel.SrcFileName = _PrDetailModel.ShipToBranch & "_" & DTsub & "-" & DropDownYear.SelectedValue & DropDownMonth.SelectedValue & DropDownDaySub.SelectedValue & ".csv"


                _Expflddtl.FileName = _PrDetailModel.SrcFileName
                _Expflddtl.SrcFileName = _PrDetailModel.SrcFileName
                _Expflddtl.ShipToBranch = DropListLocation.SelectedItem.Text
                _Expflddtl.ShipToBranchCode = DropListLocation.SelectedItem.Value
                _Expflddtl.ExportFile = drpTaskExport.SelectedValue
                _Expflddtl.UserId = userid
                _Expflddtl.UserName = userName
                'exfiledtl.DateFrom = _InvoiceUpdateModel.DateFrom
                'exfiledtl.DateTo = _InvoiceUpdateModel.DateTo
                'exfiledtl.number = _InvoiceUpdateModel.Number
                Session("Exportinputdtl") = _Expflddtl

                ViewState("SortExpression") = Nothing
                ViewState("SortDirection") = Nothing
                GenerateExportDetails(drpTaskExport.SelectedValue)
                ' _PrSummaryModel.DateFrom = DropDownYear.SelectedValue & "/" & DropDownMonth.SelectedValue & "/01"
                '  _PrSummaryModel.DateTo = CommonControl.GetLastDayOfMonth(DropDownMonth.SelectedValue, DropDownYear.SelectedValue)
                'Dim dtPrDetailView As DataTable = _PrDetailControl.SelectPrDetail(_PrDetailModel)
                'If (dtPrDetailView Is Nothing) Or (dtPrDetailView.Rows.Count = 0) Then
                '    'If no Records
                '    Call showMsg("No records found for the month of " & DropDownMonth.SelectedItem.Text, "")
                '    Exit Sub
                'Else
                '    Response.ContentType = "text/csv"
                '    Response.AddHeader("Content-Disposition", "attachment;filename=" & _PrDetailModel.SrcFileName)
                '    Response.AddHeader("Pragma", "no-cache")
                '    Response.AddHeader("Cache-Control", "no-cache")
                '    Dim myData As Byte() = CommonControl.csvBytesWriter(dtPrDetailView)
                '    Response.BinaryWrite(myData)
                '    Response.Flush()
                '    Response.SuppressContent = True
                '    HttpContext.Current.ApplicationInstance.CompleteRequest()

                'End If
                '(drpTaskExport.SelectedValue = "3A") Or (drpTaskExport.SelectedValue = "3B") Or (drpTaskExport.SelectedValue = "4")
            ElseIf (drpTaskExport.SelectedValue = "8A") Or (drpTaskExport.SelectedValue = "8B") Then
                Dim Task As Integer = 0
                Dim DtFrom As String = ""
                Dim DtTo As String = ""

                DtFrom = Trim(TextDateFrom.Text)
                DtTo = Trim(TextDateTo.Text)

                'Task = 0 then Month wise filter
                'Task = 1 then From & To

                If Trim(DtFrom) = "" And Trim(DtTo) = "" And DropDownMonth.SelectedValue = "0" Then
                    Call showMsg("Please specify either output period by month or date", "")
                    Exit Sub
                End If

                If Trim(DtFrom) <> "" And Trim(DtTo) <> "" And DropDownMonth.SelectedValue <> "0" Then
                    Call showMsg("Please specify either output period by month or date.", "")
                    Exit Sub
                End If

                If DropDownMonth.SelectedValue <> "0" Then
                    Task = 1 'Assign From or To or both filter
                    If Len(Trim(DtFrom)) > 7 Or Len(Trim(DtTo)) > 7 Then
                        Call showMsg("Please specify either output period by month or date.", "")
                        Exit Sub
                    End If



                Else
                    Task = 1 'Assign From or To or both filter

                    If Len(Trim(DtFrom)) > 7 And Len(Trim(DtTo)) > 7 Then
                        Dim date1, date2 As Date
                        date1 = Date.Parse(TextDateFrom.Text)
                        date2 = Date.Parse(TextDateTo.Text)
                        If (DateTime.Compare(date1, date2) > 0) Then ' which means ("date1 > date2") 
                            Call showMsg("Please verify from date and to date", "")
                            Exit Sub
                        End If
                    End If

                    'Need to interchange 
                    'Allow for any one of From / To 
                    If Len(DtFrom) > 5 And DtTo = "" Then
                        DtTo = DtFrom
                    End If
                    If DtFrom = "" And Len(DtTo) > 5 Then
                        DtFrom = DtTo
                    End If

                End If


                '''Verify Month is selected
                ''If (Trim(TextDateFrom.Text) = "") And (Trim(TextDateTo.Text) = "") Then
                ''    If (DropDownMonth.SelectedValue = "0") Then
                ''        Call showMsg("Please select the month", "")
                ''        Exit Sub
                ''    End If
                ''End If
                'FromDate Verification 
                Dim dtTbl1, dtTbl2 As DateTime
                If (Trim(DtFrom) <> "") Then
                    If DateTime.TryParse(DtFrom, dtTbl1) Then
                        dtTbl1 = DateTime.Parse(Trim(DtFrom)).ToShortDateString
                    Else
                        Call showMsg("There is an error in the date specification", "")
                        Exit Sub
                    End If
                End If
                If (Trim(DtTo) <> "") Then
                    If DateTime.TryParse(DtTo, dtTbl2) Then
                        dtTbl2 = DateTime.Parse(Trim(DtTo)).ToShortDateString
                    Else
                        Call showMsg("There is an error in the date specification", "")
                        Exit Sub
                    End If
                End If



                'Pass the paramters to Update in Datatabase
                Dim _GRecievedModel As GRecievedModel = New GRecievedModel()
                Dim _GRecievedControl As GRecievedControl = New GRecievedControl()
                Dim strFileName As String = ""
                _GRecievedModel.UserId = userid
                _GRecievedModel.UserName = userName
                _GRecievedModel.ShipToBranchCode = DropListLocation.SelectedItem.Value
                _GRecievedModel.ShipToBranch = DropListLocation.SelectedItem.Text

                '_GRecievedModel.DateFrom = DropDownYear.SelectedValue & "/" & DropDownMonth.SelectedValue & "/01"
                '_GRecievedModel.DateTo = CommonControl.GetLastDayOfMonth(DropDownMonth.SelectedValue, DropDownYear.SelectedValue)

                'Assign File Name and Month 
                If drpTaskExport.SelectedItem.Value = "8A" Then
                    If String.IsNullOrEmpty(DtFrom) Or String.IsNullOrEmpty(DtTo) Then ' Month format selected
                        _GRecievedModel.SrcFileName = _GRecievedModel.ShipToBranch & "_GR" & DropDownYear.SelectedValue & DropDownMonth.SelectedValue & ".csv"
                        strFileName = _GRecievedModel.SrcFileName
                    Else
                        'DateConversion
                        Dim DtConvFrom() As String
                        DtConvFrom = Split(DtFrom, "/")
                        If Len(DtConvFrom(0)) = 1 Then
                            DtConvFrom(0) = "0" & DtConvFrom(0)
                        End If
                        If Len(DtConvFrom(1)) = 1 Then
                            DtConvFrom(1) = "0" & DtConvFrom(1)
                        End If
                        _GRecievedModel.DateFrom = DtConvFrom(2) & "/" & DtConvFrom(0) & "/" & DtConvFrom(1)
                        'DateConversion
                        Dim DtConvTo() As String
                        DtConvTo = Split(DtTo, "/")
                        If Len(DtConvTo(0)) = 1 Then
                            DtConvTo(0) = "0" & DtConvTo(0)
                        End If
                        If Len(DtConvTo(1)) = 1 Then
                            DtConvTo(1) = "0" & DtConvTo(1)
                        End If
                        _GRecievedModel.DateTo = DtConvTo(2) & "/" & DtConvTo(0) & "/" & DtConvTo(1)
                        strFileName = _GRecievedModel.ShipToBranch & "_GR" & DtConvFrom(2) & DtConvFrom(0) & DtConvFrom(1) & "-" & DtConvTo(2) & DtConvTo(0) & DtConvTo(1) & ".csv"
                        ''  _GRecievedModel.SrcFileName = _GRecievedModel.ShipToBranch & "_GR" & DropDownYear.SelectedValue & DropDownMonth.SelectedValue & ".csv"
                    End If
                ElseIf drpTaskExport.SelectedItem.Value = "8B" Then
                    If String.IsNullOrEmpty(DtFrom) Or String.IsNullOrEmpty(DtTo) Then ' Month format selected
                        _GRecievedModel.SrcFileName = _GRecievedModel.ShipToBranch & "_GD" & DropDownYear.SelectedValue & DropDownMonth.SelectedValue & ".csv"
                        strFileName = _GRecievedModel.SrcFileName
                    Else
                        'DateConversion
                        Dim DtConvFrom() As String
                        DtConvFrom = Split(DtFrom, "/")
                        If Len(DtConvFrom(0)) = 1 Then
                            DtConvFrom(0) = "0" & DtConvFrom(0)
                        End If
                        If Len(DtConvFrom(1)) = 1 Then
                            DtConvFrom(1) = "0" & DtConvFrom(1)
                        End If
                        _GRecievedModel.DateFrom = DtConvFrom(2) & "/" & DtConvFrom(0) & "/" & DtConvFrom(1)
                        'DateConversion
                        Dim DtConvTo() As String
                        DtConvTo = Split(DtTo, "/")
                        If Len(DtConvTo(0)) = 1 Then
                            DtConvTo(0) = "0" & DtConvTo(0)
                        End If
                        If Len(DtConvTo(1)) = 1 Then
                            DtConvTo(1) = "0" & DtConvTo(1)
                        End If
                        _GRecievedModel.DateTo = DtConvTo(2) & "/" & DtConvTo(0) & "/" & DtConvTo(1)
                        strFileName = _GRecievedModel.ShipToBranch & "_GD" & DtConvFrom(2) & DtConvFrom(0) & DtConvFrom(1) & "-" & DtConvTo(2) & DtConvTo(0) & DtConvTo(1) & ".csv"
                        ''  _GRecievedModel.SrcFileName = _GRecievedModel.ShipToBranch & "_GR" & DropDownYear.SelectedValue & DropDownMonth.SelectedValue & ".csv"
                    End If

                End If

                _Expflddtl.FileName = _GRecievedModel.SrcFileName
                _Expflddtl.SrcFileName = _GRecievedModel.SrcFileName
                _Expflddtl.ShipToBranch = DropListLocation.SelectedItem.Text
                _Expflddtl.ShipToBranchCode = DropListLocation.SelectedItem.Value
                _Expflddtl.ExportFile = drpTaskExport.SelectedValue
                _Expflddtl.UserId = userid
                _Expflddtl.UserName = userName
                _Expflddtl.DateFrom = _GRecievedModel.DateFrom
                _Expflddtl.DateTo = _GRecievedModel.DateTo
                'exfiledtl.number = _InvoiceUpdateModel.Number
                Session("Exportinputdtl") = _Expflddtl

                ViewState("SortExpression") = Nothing
                ViewState("SortDirection") = Nothing
                GenerateExportDetails(drpTaskExport.SelectedValue)

                'If DropDownGR.SelectedItem.Value = "GR" Then
                '    Dim dtGRecievedView As DataTable = _GRecievedControl.SelectGRecieved(_GRecievedModel)
                '    If (dtGRecievedView Is Nothing) Or (dtGRecievedView.Rows.Count = 0) Then
                '        'If no Records
                '        Call showMsg("No records found for the month of " & DropDownMonth.SelectedItem.Text, "")
                '        Exit Sub
                '    Else
                '        Dim dt As New DataTable()
                '        dt.Columns.Add("No", GetType(String))
                '        dt.Columns.Add("Invoice No/", GetType(String))
                '        dt.Columns.Add("Invoice Date/", GetType(String))
                '        dt.Columns.Add("Local Invoice No", GetType(String))
                '        dt.Columns.Add("Items", GetType(String))
                '        dt.Columns.Add("Total Qty", GetType(String))
                '        dt.Columns.Add("Total Amount", GetType(String))
                '        dt.Columns.Add("GR Date", GetType(String))
                '        dt.Columns.Add("Create By", GetType(String))
                '        dt.Columns.Add("G/R Status", GetType(String))
                '        ' dt.Rows.Add("No", "Invoice No/", "Invoice Date/", "Local Invoice No", "Items", "Total Qty", "Total Amount", "GR Date", "Create By", "G/R Status")
                '        dt.Rows.Add("", "Delivery No", "Delivery Date", "", "", "", "", "", "", "")
                '        For Each row As DataRow In dtGRecievedView.Rows
                '            dt.Rows.Add(row.Item(0), row.Item(1), row.Item(2), row.Item(3), row.Item(4), row.Item(5), row.Item(6), row.Item(7), row.Item(8), row.Item(9))
                '        Next row

                '        Response.ContentType = "text/csv"
                '        Response.AddHeader("Content-Disposition", "attachment;filename=" & strFileName)
                '        Response.AddHeader("Pragma", "no-cache")
                '        Response.AddHeader("Cache-Control", "no-cache")
                '        Dim myData As Byte() = CommonControl.csvBytesWriter(dt)
                '        Response.BinaryWrite(myData)
                '        Response.Flush()
                '        Response.SuppressContent = True
                '        HttpContext.Current.ApplicationInstance.CompleteRequest()
                '    End If
                'ElseIf DropDownGR.SelectedItem.Value = "GD" Then 'Added on 20190724 , Goods Received Details
                '    Dim dtGRecievedView As DataTable = _GRecievedControl.SelectGDRecieved(_GRecievedModel)
                '    If (dtGRecievedView Is Nothing) Or (dtGRecievedView.Rows.Count = 0) Then
                '        'If no Records
                '        Call showMsg("No records found for the month of " & DropDownMonth.SelectedItem.Text, "")
                '        Exit Sub
                '    Else
                '        Response.ContentType = "text/csv"
                '        Response.AddHeader("Content-Disposition", "attachment;filename=" & strFileName)
                '        Response.AddHeader("Pragma", "no-cache")
                '        Response.AddHeader("Cache-Control", "no-cache")
                '        Dim myData As Byte() = CommonControl.csvBytesWriter(dtGRecievedView)
                '        Response.BinaryWrite(myData)
                '        Response.Flush()
                '        Response.SuppressContent = True
                '        HttpContext.Current.ApplicationInstance.CompleteRequest()
                '    End If

                'End If
            ElseIf drpTaskExport.SelectedValue = "9" Then
                'Verify Month is selected
                If DropDownMonth.SelectedValue = "0" Then
                    Call showMsg("Please select the month", "")
                    Exit Sub
                End If
                'Pass the paramters to Update in Datatabase
                Dim _StockOverviewModel As StockOverviewModel = New StockOverviewModel()
                Dim _StockOverviewControl As StockOverviewControl = New StockOverviewControl()
                _StockOverviewModel.UserId = userid
                _StockOverviewModel.UserName = userName
                _StockOverviewModel.ShipToBranchCode = DropListLocation.SelectedItem.Value
                _StockOverviewModel.ShipToBranch = DropListLocation.SelectedItem.Text
                _StockOverviewModel.SrcFileName = _StockOverviewModel.ShipToBranch & "_SV" & DropDownYear.SelectedValue & DropDownMonth.SelectedValue & ".csv"

                _Expflddtl.FileName = _StockOverviewModel.SrcFileName
                _Expflddtl.SrcFileName = _StockOverviewModel.SrcFileName
                _Expflddtl.ShipToBranch = DropListLocation.SelectedItem.Text
                _Expflddtl.ShipToBranchCode = DropListLocation.SelectedItem.Value
                _Expflddtl.ExportFile = drpTaskExport.SelectedValue
                _Expflddtl.UserId = _StockOverviewModel.UserId
                _Expflddtl.UserName = _StockOverviewModel.UserName
                'exfiledtl.DateFrom = _GRecievedModel.DateFrom
                'exfiledtl.DateTo = _GRecievedModel.DateTo
                'exfiledtl.number = _InvoiceUpdateModel.Number
                Session("Exportinputdtl") = _Expflddtl

                ViewState("SortExpression") = Nothing
                ViewState("SortDirection") = Nothing
                GenerateExportDetails(drpTaskExport.SelectedValue)

                'Dim dtStockOverviewView As DataTable = _StockOverviewControl.SelectStockOverview(_StockOverviewModel)
                'If (dtStockOverviewView Is Nothing) Or (dtStockOverviewView.Rows.Count = 0) Then
                '    'If no Records
                '    Call showMsg("No records found for the month of " & DropDownMonth.SelectedItem.Text, "")
                '    Exit Sub
                'Else
                '    Response.ContentType = "text/csv"
                '    Response.AddHeader("Content-Disposition", "attachment;filename=" & _StockOverviewModel.SrcFileName)
                '    Response.AddHeader("Pragma", "no-cache")
                '    Response.AddHeader("Cache-Control", "no-cache")
                '    Dim myData As Byte() = CommonControl.csvBytesWriter(dtStockOverviewView)
                '    Response.BinaryWrite(myData)
                '    Response.Flush()
                '    Response.SuppressContent = True
                '    HttpContext.Current.ApplicationInstance.CompleteRequest()
                'End If
            ElseIf drpTaskExport.SelectedValue = "10" Then
                'Verify Month is selected
                If DropDownMonth.SelectedValue = "0" Then
                    Call showMsg("Please select the month", "")
                    Exit Sub
                End If
                'Pass the paramters to Update in Datatabase
                Dim _SawDiscountModel As SawDiscountModel = New SawDiscountModel()
                Dim _SawDiscountControl As SawDiscountControl = New SawDiscountControl()
                _SawDiscountModel.UserId = userid
                _SawDiscountModel.UserName = userName
                _SawDiscountModel.ShipToBranchCode = DropListLocation.SelectedItem.Value
                _SawDiscountModel.ShipToBranch = DropListLocation.SelectedItem.Text
                _SawDiscountModel.SrcFileName = _SawDiscountModel.ShipToBranch & "_SW" & DropDownYear.SelectedValue & DropDownMonth.SelectedValue & ".csv"

                _Expflddtl.FileName = _SawDiscountModel.SrcFileName
                _Expflddtl.SrcFileName = _SawDiscountModel.SrcFileName
                _Expflddtl.ShipToBranch = DropListLocation.SelectedItem.Text
                _Expflddtl.ShipToBranchCode = DropListLocation.SelectedItem.Value
                _Expflddtl.ExportFile = drpTaskExport.SelectedValue
                _Expflddtl.UserId = _SawDiscountModel.UserId
                _Expflddtl.UserName = _SawDiscountModel.UserName
                'exfiledtl.DateFrom = _GRecievedModel.DateFrom
                'exfiledtl.DateTo = _GRecievedModel.DateTo
                'exfiledtl.number = _InvoiceUpdateModel.Number
                Session("Exportinputdtl") = _Expflddtl

                ViewState("SortExpression") = Nothing
                ViewState("SortDirection") = Nothing
                GenerateExportDetails(drpTaskExport.SelectedValue)

                'Dim dtSawDiscountView As DataTable = _SawDiscountControl.SelectSawDiscount(_SawDiscountModel)
                'If (dtSawDiscountView Is Nothing) Or (dtSawDiscountView.Rows.Count = 0) Then
                '    'If no Records
                '    Call showMsg("No records found for the month of " & DropDownMonth.SelectedItem.Text, "")
                '    Exit Sub
                'Else
                '    Response.ContentType = "text/csv"
                '    Response.AddHeader("Content-Disposition", "attachment;filename=" & _SawDiscountModel.SrcFileName)
                '    Response.AddHeader("Pragma", "no-cache")
                '    Response.AddHeader("Cache-Control", "no-cache")
                '    Dim myData As Byte() = CommonControl.csvBytesWriter(dtSawDiscountView)
                '    Response.BinaryWrite(myData)
                '    Response.Flush()
                '    Response.SuppressContent = True
                '    HttpContext.Current.ApplicationInstance.CompleteRequest()
                'End If
            ElseIf drpTaskExport.SelectedValue = "11" Then
                'Verify Month is selected
                If DropDownMonth.SelectedValue = "0" Then
                    Call showMsg("Please select the month", "")
                    Exit Sub
                End If
                'Pass the paramters to Update in Datatabase
                Dim _PartsIoModel As PartsIoModel = New PartsIoModel()
                Dim _PartsIoControl As PartsIoControl = New PartsIoControl()
                _PartsIoModel.UserId = userid
                _PartsIoModel.UserName = userName
                _PartsIoModel.ShipToBranchCode = DropListLocation.SelectedItem.Value
                _PartsIoModel.ShipToBranch = DropListLocation.SelectedItem.Text
                _PartsIoModel.SrcFileName = _PartsIoModel.ShipToBranch & "_PIO" & DropDownYear.SelectedValue & DropDownMonth.SelectedValue & ".csv"
                '_GRecievedModel.DateFrom = DropDownYear.SelectedValue & "/" & DropDownMonth.SelectedValue & "/01"
                '_GRecievedModel.DateTo = CommonControl.GetLastDayOfMonth(DropDownMonth.SelectedValue, DropDownYear.SelectedValue)

                _Expflddtl.FileName = _PartsIoModel.SrcFileName
                _Expflddtl.SrcFileName = _PartsIoModel.SrcFileName
                _Expflddtl.ShipToBranch = DropListLocation.SelectedItem.Text
                _Expflddtl.ShipToBranchCode = DropListLocation.SelectedItem.Value
                _Expflddtl.ExportFile = drpTaskExport.SelectedValue
                _Expflddtl.UserId = _PartsIoModel.UserId
                _Expflddtl.UserName = _PartsIoModel.UserName
                'exfiledtl.DateFrom = _GRecievedModel.DateFrom
                'exfiledtl.DateTo = _GRecievedModel.DateTo
                'exfiledtl.number = _InvoiceUpdateModel.Number
                Session("Exportinputdtl") = _Expflddtl

                ViewState("SortExpression") = Nothing
                ViewState("SortDirection") = Nothing
                GenerateExportDetails(drpTaskExport.SelectedValue)

                'Dim dtPartsIoView As DataTable = _PartsIoControl.SelectPartsIo(_PartsIoModel)
                'If (dtPartsIoView Is Nothing) Or (dtPartsIoView.Rows.Count = 0) Then
                '    'If no Records
                '    Call showMsg("No records found for the month of " & DropDownMonth.SelectedItem.Text, "")
                '    Exit Sub
                'Else
                '    Dim dt As New DataTable()
                '    dt.Columns.Add("No", GetType(String))
                '    dt.Columns.Add("Branch", GetType(String))
                '    dt.Columns.Add("In/Out Date", GetType(String))
                '    dt.Columns.Add("Type", GetType(String))
                '    dt.Columns.Add("Type Description", GetType(String))
                '    dt.Columns.Add("Ref.Doc.No", GetType(String))
                '    dt.Columns.Add("Parts No", GetType(String))
                '    dt.Columns.Add("Description", GetType(String))
                '    dt.Columns.Add("Qty", GetType(String))
                '    dt.Columns.Add("MAP", GetType(String))
                '    dt.Columns.Add("Engineer Code", GetType(String))
                '    dt.Columns.Add("In/Out", GetType(String))
                '    dt.Columns.Add("Unit", GetType(String))
                '    ' dt.Rows.Add("No", "Branch", "In/Out Date", "Type", "Type Description", "Ref.Doc.No", "Parts No", "Description", "Qty", "MAP", "Engineer Code", "In/Out", "Unit")
                '    dt.Rows.Add("", "", "", "", "", "", "", "", "", "", "", "Warranty", "")
                '    For Each row As DataRow In dtPartsIoView.Rows
                '        dt.Rows.Add(row.Item(0), row.Item(1), row.Item(2), row.Item(3), row.Item(4), row.Item(5), row.Item(6), row.Item(7), row.Item(8), row.Item(9), row.Item(10), row.Item(11), row.Item(12))
                '    Next row
                '    Response.ContentType = "text/csv"
                '    'DropDownYear.SelectedValue & "_" & DropDownMonth.SelectedValue & "_" & DropDownDaySub.SelectedValue & "-" & DropDownDaySub.SelectedValue & "_" & _PartsIoModel.ShipToBranch & "_PIO" & ".csv"
                '    Response.AddHeader("Content-Disposition", "attachment;filename=" & DropDownYear.SelectedValue & "_" & DropDownMonth.SelectedValue & "_" & DropDownDaySub.SelectedValue & "-" & DropDownDaySub.SelectedValue & "_" & _PartsIoModel.ShipToBranch & "_PIO" & ".csv")
                '    Response.AddHeader("Pragma", "no-cache")
                '    Response.AddHeader("Cache-Control", "no-cache")
                '    Dim myData As Byte() = CommonControl.csvBytesWriter(dt)
                '    Response.BinaryWrite(myData)
                '    Response.Flush()
                '    Response.SuppressContent = True
                '    HttpContext.Current.ApplicationInstance.CompleteRequest()
                'End If
            ElseIf drpTaskExport.SelectedValue = "12" Then
                Call showMsg("Coming Soon...", "")
                Exit Sub
            ElseIf drpTaskExport.SelectedValue = "13" Then
                Call showMsg("Coming Soon...", "")
                Exit Sub
            ElseIf drpTaskExport.SelectedValue = "14" Then
                'Verify Month is selected
                If DropDownMonth.SelectedValue = "0" Then
                    Call showMsg("Please select the month", "")
                    Exit Sub
                End If
                'Pass the paramters to Update in Datatabase
                Dim _G2sPaidModel As G2sPaidModel = New G2sPaidModel()
                Dim _G2sPaidControl As G2sPaidControl = New G2sPaidControl()
                _G2sPaidModel.UserId = userid
                _G2sPaidModel.UserName = userName
                _G2sPaidModel.ShipToBranchCode = DropListLocation.SelectedItem.Value
                _G2sPaidModel.ShipToBranch = DropListLocation.SelectedItem.Text
                _G2sPaidModel.SrcFileName = _G2sPaidModel.ShipToBranch & "_GS" & DropDownYear.SelectedValue & DropDownMonth.SelectedValue & ".csv"
                '_G2sPaidModel.DateFrom = DropDownYear.SelectedValue & "/" & DropDownMonth.SelectedValue & "/01"
                '_G2sPaidModel.DateTo = CommonControl.GetLastDayOfMonth(DropDownMonth.SelectedValue, DropDownYear.SelectedValue)
                _Expflddtl.FileName = _G2sPaidModel.SrcFileName
                _Expflddtl.SrcFileName = _G2sPaidModel.SrcFileName
                _Expflddtl.ShipToBranch = DropListLocation.SelectedItem.Text
                _Expflddtl.ShipToBranchCode = DropListLocation.SelectedItem.Value
                _Expflddtl.ExportFile = drpTaskExport.SelectedValue
                _Expflddtl.UserId = _G2sPaidModel.UserId
                _Expflddtl.UserName = _G2sPaidModel.UserName
                'exfiledtl.DateFrom = _GRecievedModel.DateFrom
                'exfiledtl.DateTo = _GRecievedModel.DateTo
                'exfiledtl.number = _InvoiceUpdateModel.Number
                Session("Exportinputdtl") = _Expflddtl

                ViewState("SortExpression") = Nothing
                ViewState("SortDirection") = Nothing
                GenerateExportDetails(drpTaskExport.SelectedValue)

                'Dim dtG2sPaidView As DataTable = _G2sPaidControl.SelectG2sPaid(_G2sPaidModel)
                'If (dtG2sPaidView Is Nothing) Or (dtG2sPaidView.Rows.Count = 0) Then
                '    'If no Records
                '    Call showMsg("No records found for the month of " & DropDownMonth.SelectedItem.Text, "")
                '    Exit Sub
                'Else
                '    Response.ContentType = "text/csv"
                '    Response.AddHeader("Content-Disposition", "attachment;filename=" & DropListLocation.SelectedItem.Text & "_GS" & DropDownYear.SelectedValue & DropDownMonth.SelectedValue & ".csv")
                '    Response.AddHeader("Pragma", "no-cache")
                '    Response.AddHeader("Cache-Control", "no-cache")
                '    Dim myData As Byte() = CommonControl.csvBytesWriter(dtG2sPaidView)
                '    Response.BinaryWrite(myData)
                '    Response.Flush()
                '    Response.SuppressContent = True
                '    HttpContext.Current.ApplicationInstance.CompleteRequest()
                'End If
            ElseIf drpTaskExport.SelectedValue = "15" Then
                'Verify Month is selected
                If DropDownMonth.SelectedValue = "0" Then
                    Call showMsg("Please select the month", "")
                    Exit Sub
                End If
                'Pass the paramters to Update in Datatabase
                Dim _ReturnCreditModel As ReturnCreditModel = New ReturnCreditModel()
                Dim _ReturnCreditControl As ReturnCreditControl = New ReturnCreditControl()
                _ReturnCreditModel.UserId = userid
                _ReturnCreditModel.UserName = userName
                _ReturnCreditModel.ShipToBranchCode = DropListLocation.SelectedItem.Value
                _ReturnCreditModel.ShipToBranch = DropListLocation.SelectedItem.Text
                _ReturnCreditModel.SrcFileName = _ReturnCreditModel.ShipToBranch & "_RC" & DropDownYear.SelectedValue & DropDownMonth.SelectedValue & ".csv"
                '_ReturnCreditModel.DateFrom = DropDownYear.SelectedValue & "/" & DropDownMonth.SelectedValue & "/01"
                '_ReturnCreditModel.DateTo = CommonControl.GetLastDayOfMonth(DropDownMonth.SelectedValue, DropDownYear.SelectedValue)
                _Expflddtl.FileName = _ReturnCreditModel.SrcFileName
                _Expflddtl.SrcFileName = _ReturnCreditModel.SrcFileName
                _Expflddtl.ShipToBranch = DropListLocation.SelectedItem.Text
                _Expflddtl.ShipToBranchCode = DropListLocation.SelectedItem.Value
                _Expflddtl.ExportFile = drpTaskExport.SelectedValue
                _Expflddtl.UserId = _ReturnCreditModel.UserId
                _Expflddtl.UserName = _ReturnCreditModel.UserName
                'exfiledtl.DateFrom = _GRecievedModel.DateFrom
                'exfiledtl.DateTo = _GRecievedModel.DateTo
                'exfiledtl.number = _InvoiceUpdateModel.Number
                Session("Exportinputdtl") = _Expflddtl

                ViewState("SortExpression") = Nothing
                ViewState("SortDirection") = Nothing
                GenerateExportDetails(drpTaskExport.SelectedValue)

                'Dim dtReturnCreditView As DataTable = _ReturnCreditControl.SelectReturnCredit(_ReturnCreditModel)
                'If (dtReturnCreditView Is Nothing) Or (dtReturnCreditView.Rows.Count = 0) Then
                '    'If no Records
                '    Call showMsg("No records found for the month of " & DropDownMonth.SelectedItem.Text, "")
                '    Exit Sub
                'Else
                '    Response.ContentType = "text/csv"
                '    Response.AddHeader("Content-Disposition", "attachment;filename=" & _ReturnCreditModel.SrcFileName)
                '    Response.AddHeader("Pragma", "no-cache")
                '    Response.AddHeader("Cache-Control", "no-cache")
                '    Dim myData As Byte() = CommonControl.csvBytesWriter(dtReturnCreditView)
                '    Response.BinaryWrite(myData)
                '    Response.Flush()
                '    Response.SuppressContent = True
                '    HttpContext.Current.ApplicationInstance.CompleteRequest()
                'End If
            ElseIf drpTaskExport.SelectedValue = "16" Then
                'Verify Month is selected
                If DropDownMonth.SelectedValue = "0" Then
                    Call showMsg("Please select the month", "")
                    Exit Sub
                End If
                If DropDownDaySub.SelectedValue = "0" Then
                    Call showMsg("Please select the day", "")
                    Exit Sub
                End If
                'Pass the paramters to Update in Datatabase
                Dim _SLedgerModel As SLedgerModel = New SLedgerModel()
                Dim _SLedgerControl As SLedgerControl = New SLedgerControl()
                _SLedgerModel.UserId = userid
                _SLedgerModel.UserName = userName
                _SLedgerModel.ShipToBranchCode = DropListLocation.SelectedItem.Value
                _SLedgerModel.ShipToBranch = DropListLocation.SelectedItem.Text
                _SLedgerModel.SrcFileName = _SLedgerModel.ShipToBranch & "_LG" & DropDownYear.SelectedValue & DropDownMonth.SelectedValue & DropDownDaySub.SelectedValue & ".csv"
                _Expflddtl.FileName = _SLedgerModel.SrcFileName
                _Expflddtl.SrcFileName = _SLedgerModel.SrcFileName
                _Expflddtl.ShipToBranch = DropListLocation.SelectedItem.Text
                _Expflddtl.ShipToBranchCode = DropListLocation.SelectedItem.Value
                _Expflddtl.ExportFile = drpTaskExport.SelectedValue
                _Expflddtl.UserId = _SLedgerModel.UserId
                _Expflddtl.UserName = _SLedgerModel.UserName
                'exfiledtl.DateFrom = _GRecievedModel.DateFrom
                'exfiledtl.DateTo = _GRecievedModel.DateTo
                'exfiledtl.number = _InvoiceUpdateModel.Number
                Session("Exportinputdtl") = _Expflddtl

                ViewState("SortExpression") = Nothing
                ViewState("SortDirection") = Nothing
                GenerateExportDetails(drpTaskExport.SelectedValue)

                'Dim dtSLedgerView As DataTable = _SLedgerControl.SelectSLedger(_SLedgerModel)
                'If (dtSLedgerView Is Nothing) Or (dtSLedgerView.Rows.Count = 0) Then
                '    'If no Records
                '    Call showMsg("No records found for the month of " & DropDownMonth.SelectedItem.Text, "")
                '    Exit Sub
                'Else
                '    Response.ContentType = "text/csv"
                '    Response.AddHeader("Content-Disposition", "attachment;filename=" & _SLedgerModel.SrcFileName)
                '    Response.AddHeader("Pragma", "no-cache")
                '    Response.AddHeader("Cache-Control", "no-cache")
                '    Dim myData As Byte() = CommonControl.csvBytesWriter(dtSLedgerView)
                '    Response.BinaryWrite(myData)
                '    Response.Flush()
                '    Response.SuppressContent = True
                '    HttpContext.Current.ApplicationInstance.CompleteRequest()
                'End If
            ElseIf drpTaskExport.SelectedValue = "99" Then 'VJ 2019/10/14 Begin
            ElseIf drpTaskExport.SelectedValue = "18" Then 'VJ 2019/10/14 Begin
                'Verify Month is selected
                If DropDownMonth.SelectedValue = "0" Then
                    Call showMsg("Please select the month", "")
                    Exit Sub
                End If
                'Pass the paramters to Update in Datatabase
                Dim _DebitNoteRegisterModel As DebitNoteRegisterModel = New DebitNoteRegisterModel()
                Dim _DebitNoteRegisterControl As DebitNoteRegisterControl = New DebitNoteRegisterControl()
                _DebitNoteRegisterModel.UserId = userid
                _DebitNoteRegisterModel.UserName = userName
                _DebitNoteRegisterModel.ShipToBranchCode = DropListLocation.SelectedItem.Value
                _DebitNoteRegisterModel.ShipToBranch = DropListLocation.SelectedItem.Text
                _DebitNoteRegisterModel.SrcFileName = _DebitNoteRegisterModel.ShipToBranch & "_DN" & DropDownYear.SelectedValue & DropDownMonth.SelectedValue & ".csv"

                _Expflddtl.FileName = _DebitNoteRegisterModel.SrcFileName
                _Expflddtl.SrcFileName = _DebitNoteRegisterModel.SrcFileName
                _Expflddtl.ShipToBranch = DropListLocation.SelectedItem.Text
                _Expflddtl.ShipToBranchCode = DropListLocation.SelectedItem.Value
                _Expflddtl.ExportFile = drpTaskExport.SelectedValue
                _Expflddtl.UserId = _DebitNoteRegisterModel.UserId
                _Expflddtl.UserName = _DebitNoteRegisterModel.UserName
                'exfiledtl.DateFrom = _GRecievedModel.DateFrom
                'exfiledtl.DateTo = _GRecievedModel.DateTo
                'exfiledtl.number = _InvoiceUpdateModel.Number
                Session("Exportinputdtl") = _Expflddtl

                ViewState("SortExpression") = Nothing
                ViewState("SortDirection") = Nothing
                GenerateExportDetails(drpTaskExport.SelectedValue)


                '_ReturnCreditModel.DateFrom = DropDownYear.SelectedValue & "/" & DropDownMonth.SelectedValue & "/01"
                '_ReturnCreditModel.DateTo = CommonControl.GetLastDayOfMonth(DropDownMonth.SelectedValue, DropDownYear.SelectedValue)
                'Dim dtDebitNoteRegisterView As DataTable = _DebitNoteRegisterControl.SelectDebitNoteRegister(_DebitNoteRegisterModel)
                'If (dtDebitNoteRegisterView Is Nothing) Or (dtDebitNoteRegisterView.Rows.Count = 0) Then
                '    'If no Records
                '    Call showMsg("No records found for the year " & DropDownYear.SelectedValue & " and month of " & DropDownMonth.SelectedItem.Text, "")
                '    Exit Sub
                'Else
                '    ExportCSV(_DebitNoteRegisterModel.SrcFileName, dtDebitNoteRegisterView)
                '    'Response.ContentType = "text/csv"
                '    'Response.AddHeader("Content-Disposition", "attachment;filename=" & _DebitNoteRegisterModel.SrcFileName)
                '    'Response.AddHeader("Pragma", "no-cache")
                '    'Response.AddHeader("Cache-Control", "no-cache")
                '    'Dim myData As Byte() = CommonControl.csvBytesWriter(dtDebitNoteRegisterView)
                '    'Response.BinaryWrite(myData)
                '    'Response.Flush()
                '    'Response.SuppressContent = True
                '    'HttpContext.Current.ApplicationInstance.CompleteRequest()
                'End If
                'ElseIf drpTaskExport.SelectedValue = "18A" Then
                '    'Verify Month is selected
                '    If DropDownMonth.SelectedValue = "0" Then
                '        Call showMsg("Please select the month", "")
                '        Exit Sub
                '    End If
                '    'Pass the paramters to Update in Datatabase
                '    Dim _DebitNoteRegisterModel As DebitNoteRegisterModel = New DebitNoteRegisterModel()
                '    Dim _DebitNoteRegisterControl As DebitNoteRegisterControl = New DebitNoteRegisterControl()
                '    _DebitNoteRegisterModel.UserId = userid
                '    _DebitNoteRegisterModel.UserName = userName
                '    _DebitNoteRegisterModel.ShipToBranchCode = DropListLocation.SelectedItem.Value
                '    _DebitNoteRegisterModel.ShipToBranch = DropListLocation.SelectedItem.Text
                '    _DebitNoteRegisterModel.SrcFileName = _DebitNoteRegisterModel.ShipToBranch & "_DN" & DropDownYear.SelectedValue & DropDownMonth.SelectedValue & ".csv"
                '    '_ReturnCreditModel.DateFrom = DropDownYear.SelectedValue & "/" & DropDownMonth.SelectedValue & "/01"
                '    '_ReturnCreditModel.DateTo = CommonControl.GetLastDayOfMonth(DropDownMonth.SelectedValue, DropDownYear.SelectedValue)
                '    Dim dtDebitNoteRegisterView As DataTable = _DebitNoteRegisterControl.SelectDebitNoteRegisterImportData(_DebitNoteRegisterModel)
                '    If (dtDebitNoteRegisterView Is Nothing) Or (dtDebitNoteRegisterView.Rows.Count = 0) Then
                '        'If no Records
                '        Call showMsg("No records found for the year " & DropDownYear.SelectedValue & " and month of " & DropDownMonth.SelectedItem.Text, "")
                '        Exit Sub
                '    Else
                '        ExportCSV(_DebitNoteRegisterModel.SrcFileName, dtDebitNoteRegisterView)
                '        'Response.ContentType = "text/csv"
                '        'Response.AddHeader("Content-Disposition", "attachment;filename=" & _DebitNoteRegisterModel.SrcFileName)
                '        'Response.AddHeader("Pragma", "no-cache")
                '        'Response.AddHeader("Cache-Control", "no-cache")
                '        'Dim myData As Byte() = CommonControl.csvBytesWriter(dtDebitNoteRegisterView)
                '        'Response.BinaryWrite(myData)
                '        'Response.Flush()
                '        'Response.SuppressContent = True
                '        'HttpContext.Current.ApplicationInstance.CompleteRequest()
                '    End If
            ElseIf drpTaskExport.SelectedValue = "19" Then
                'Verify Month is selected
                If DropDownMonth.SelectedValue = "0" Then
                    Call showMsg("Please select the month", "")
                    Exit Sub
                End If
                'Pass the paramters to Update in Datatabase
                Dim _ServicePartsReturnModel As ServicePartsReturnModel = New ServicePartsReturnModel()
                Dim _ServicePartsReturnControl As ServicePartsReturnControl = New ServicePartsReturnControl()
                _ServicePartsReturnModel.UserId = userid
                _ServicePartsReturnModel.UserName = userName
                _ServicePartsReturnModel.ShipToBranchCode = DropListLocation.SelectedItem.Value
                _ServicePartsReturnModel.ShipToBranch = DropListLocation.SelectedItem.Text
                _ServicePartsReturnModel.SrcFileName = _ServicePartsReturnModel.ShipToBranch & "_PR" & DropDownYear.SelectedValue & DropDownMonth.SelectedValue & ".csv"
                '_ReturnCreditModel.DateFrom = DropDownYear.SelectedValue & "/" & DropDownMonth.SelectedValue & "/01"
                '_ReturnCreditModel.DateTo = CommonControl.GetLastDayOfMonth(DropDownMonth.SelectedValue, DropDownYear.SelectedValue)
                'Dim dtServicePartsReturnrView As DataTable = _ServicePartsReturnControl.SelectServicePartsReturn(_ServicePartsReturnModel)

                _Expflddtl.FileName = _ServicePartsReturnModel.SrcFileName
                _Expflddtl.SrcFileName = _ServicePartsReturnModel.SrcFileName
                _Expflddtl.ShipToBranch = DropListLocation.SelectedItem.Text
                _Expflddtl.ShipToBranchCode = DropListLocation.SelectedItem.Value
                _Expflddtl.ExportFile = drpTaskExport.SelectedValue
                _Expflddtl.UserId = _ServicePartsReturnModel.UserId
                _Expflddtl.UserName = _ServicePartsReturnModel.UserName
                'exfiledtl.DateFrom = _GRecievedModel.DateFrom
                'exfiledtl.DateTo = _GRecievedModel.DateTo
                'exfiledtl.number = _InvoiceUpdateModel.Number
                Session("Exportinputdtl") = _Expflddtl

                ViewState("SortExpression") = Nothing
                ViewState("SortDirection") = Nothing
                GenerateExportDetails(drpTaskExport.SelectedValue)

                'If (dtServicePartsReturnrView Is Nothing) Or (dtServicePartsReturnrView.Rows.Count = 0) Then
                '    'If no Records
                '    Call showMsg("No records found for the year " & DropDownYear.SelectedValue & " and month of " & DropDownMonth.SelectedItem.Text, "")
                '    Exit Sub
                'Else
                '    ExportCSV(_ServicePartsReturnModel.SrcFileName, dtServicePartsReturnrView)
                '    'Response.ContentType = "text/csv"
                '    'Response.AddHeader("Content-Disposition", "attachment;filename=" & _ServicePartsReturnModel.SrcFileName)
                '    'Response.AddHeader("Pragma", "no-cache")
                '    'Response.AddHeader("Cache-Control", "no-cache")
                '    'Dim myData As Byte() = CommonControl.csvBytesWriter(dtServicePartsReturnrView)
                '    'Response.BinaryWrite(myData)
                '    'Response.Flush()
                '    'Response.SuppressContent = True
                '    'HttpContext.Current.ApplicationInstance.CompleteRequest()
                'End If 'VJ 2019/10/14 End
            ElseIf drpTaskExport.SelectedValue = "19B" Then
                'Verify Month is selected
                If DropDownMonth.SelectedValue = "0" Then
                    Call showMsg("Please select the month", "")
                    Exit Sub
                End If
                'Pass the paramters to Update in Datatabase
                Dim _HSNCodeModel As HSNCodeModel = New HSNCodeModel()
                Dim _HSNCodeControl As HSNCodeControl = New HSNCodeControl()
                _HSNCodeModel.UserId = userid
                _HSNCodeModel.UserName = userName
                _HSNCodeModel.ShipToBranchCode = DropListLocation.SelectedItem.Value
                _HSNCodeModel.ShipToBranch = DropListLocation.SelectedItem.Text
                _HSNCodeModel.SrcFileName = "hnc-" & DropDownYear.SelectedValue & DropDownMonth.SelectedValue & ".csv"

                _Expflddtl.FileName = _HSNCodeModel.SrcFileName
                _Expflddtl.SrcFileName = _HSNCodeModel.SrcFileName
                _Expflddtl.ShipToBranch = DropListLocation.SelectedItem.Text
                _Expflddtl.ShipToBranchCode = DropListLocation.SelectedItem.Value
                _Expflddtl.ExportFile = drpTaskExport.SelectedValue
                _Expflddtl.UserId = _HSNCodeModel.UserId
                _Expflddtl.UserName = _HSNCodeModel.UserName
                'exfiledtl.DateFrom = _GRecievedModel.DateFrom
                'exfiledtl.DateTo = _GRecievedModel.DateTo
                'exfiledtl.number = _InvoiceUpdateModel.Number
                Session("Exportinputdtl") = _Expflddtl

                ViewState("SortExpression") = Nothing
                ViewState("SortDirection") = Nothing
                GenerateExportDetails(drpTaskExport.SelectedValue)

                'Dim dtHSNCoderView As DataTable = _HSNCodeControl.SelectHSNCode(_HSNCodeModel)
                'If (dtHSNCoderView Is Nothing) Or (dtHSNCoderView.Rows.Count = 0) Then
                '    'If no Records
                '    Call showMsg("No records found for the year " & DropDownYear.SelectedValue & " and month of " & DropDownMonth.SelectedItem.Text, "")
                '    Exit Sub
                'Else
                '    ExportCSV("Hsn-" & _HSNCodeModel.SrcFileName, dtHSNCoderView)

                'End If
            ElseIf drpTaskExport.SelectedValue = "20" Then
                'Verify Month is selected
                If DropDownMonth.SelectedValue = "0" Then
                    Call showMsg("Please select the month", "")
                    Exit Sub
                End If
                'Pass the paramters to Update in Datatabase
                Dim _OtherSalesExtendedWarrantyModel As OtherSalesExtendedWarrantyModel = New OtherSalesExtendedWarrantyModel()
                Dim _OtherSalesExtendedWarrantyControl As OtherSalesExtendedWarrantyControl = New OtherSalesExtendedWarrantyControl()
                _OtherSalesExtendedWarrantyModel.UserId = userid
                _OtherSalesExtendedWarrantyModel.UserName = userName
                _OtherSalesExtendedWarrantyModel.ShipToBranchCode = DropListLocation.SelectedItem.Value
                _OtherSalesExtendedWarrantyModel.ShipToBranch = DropListLocation.SelectedItem.Text
                _OtherSalesExtendedWarrantyModel.SrcFileName = _OtherSalesExtendedWarrantyModel.ShipToBranch & "_EW" & DropDownYear.SelectedValue & DropDownMonth.SelectedValue & ".csv"
                '_ReturnCreditModel.DateFrom = DropDownYear.SelectedValue & "/" & DropDownMonth.SelectedValue & "/01"
                '_ReturnCreditModel.DateTo = CommonControl.GetLastDayOfMonth(DropDownMonth.SelectedValue, DropDownYear.SelectedValue)
                'Dim dtSOtherSalesExtendedWarrantyView As DataTable = _OtherSalesExtendedWarrantyControl.SelectOtherSalesExtendedWarranty(_OtherSalesExtendedWarrantyModel)

                _Expflddtl.FileName = _OtherSalesExtendedWarrantyModel.SrcFileName
                _Expflddtl.SrcFileName = _OtherSalesExtendedWarrantyModel.SrcFileName
                _Expflddtl.ShipToBranch = DropListLocation.SelectedItem.Text
                _Expflddtl.ShipToBranchCode = DropListLocation.SelectedItem.Value
                _Expflddtl.ExportFile = drpTaskExport.SelectedValue
                _Expflddtl.UserId = _OtherSalesExtendedWarrantyModel.UserId
                _Expflddtl.UserName = _OtherSalesExtendedWarrantyModel.UserName
                'exfiledtl.DateFrom = _GRecievedModel.DateFrom
                'exfiledtl.DateTo = _GRecievedModel.DateTo
                'exfiledtl.number = _InvoiceUpdateModel.Number
                Session("Exportinputdtl") = _Expflddtl

                ViewState("SortExpression") = Nothing
                ViewState("SortDirection") = Nothing
                GenerateExportDetails(drpTaskExport.SelectedValue)

                'If (dtSOtherSalesExtendedWarrantyView Is Nothing) Or (dtSOtherSalesExtendedWarrantyView.Rows.Count = 0) Then
                '    'If no Records
                '    Call showMsg("No records found for the year " & DropDownYear.SelectedValue & " and month of " & DropDownMonth.SelectedItem.Text, "")
                '    Exit Sub
                'Else
                '    ExportCSV(_OtherSalesExtendedWarrantyModel.SrcFileName, dtSOtherSalesExtendedWarrantyView)
                'End If 'Added 20191114
            ElseIf drpTaskExport.SelectedValue = "21" Then

                If DropDownMonth.SelectedValue = "0" Then
                    Call showMsg("Please select the month", "")
                    Exit Sub
                End If
                'Pass the paramters to Update in Datatabase
                Dim _PoStatusModel As PoStatusModel = New PoStatusModel()
                Dim _PoStatusControl As PoStatusControl = New PoStatusControl()
                _PoStatusModel.UserId = userid
                _PoStatusModel.UserName = userName
                _PoStatusModel.ShipToBranchCode = DropListLocation.SelectedItem.Value
                _PoStatusModel.ShipToBranch = DropListLocation.SelectedItem.Text
                _PoStatusModel.SrcFileName = _PoStatusModel.ShipToBranch & "_PS" & DropDownYear.SelectedValue & DropDownMonth.SelectedValue & ".csv"
                '_ReturnCreditModel.DateFrom = DropDownYear.SelectedValue & "/" & DropDownMonth.SelectedValue & "/01"
                '_ReturnCreditModel.DateTo = CommonControl.GetLastDayOfMonth(DropDownMonth.SelectedValue, DropDownYear.SelectedValue)
                _Expflddtl.FileName = _PoStatusModel.SrcFileName
                _Expflddtl.SrcFileName = _PoStatusModel.SrcFileName
                _Expflddtl.ShipToBranch = DropListLocation.SelectedItem.Text
                _Expflddtl.ShipToBranchCode = DropListLocation.SelectedItem.Value
                _Expflddtl.ExportFile = drpTaskExport.SelectedValue
                _Expflddtl.UserId = _PoStatusModel.UserId
                _Expflddtl.UserName = _PoStatusModel.UserName
                'exfiledtl.DateFrom = _GRecievedModel.DateFrom
                'exfiledtl.DateTo = _GRecievedModel.DateTo
                'exfiledtl.number = _InvoiceUpdateModel.Number
                Session("Exportinputdtl") = _Expflddtl

                ViewState("SortExpression") = Nothing
                ViewState("SortDirection") = Nothing
                GenerateExportDetails(drpTaskExport.SelectedValue)

                'Dim dtPoStatusView As DataTable = _PoStatusControl.SelectPoStatus(_PoStatusModel)
                'If (dtPoStatusView Is Nothing) Or (dtPoStatusView.Rows.Count = 0) Then
                '    'If no Records
                '    Call showMsg("No records found for the year " & DropDownYear.SelectedValue & " and month of " & DropDownMonth.SelectedItem.Text, "")
                '    Exit Sub
                'Else
                '    ExportCSV(_PoStatusModel.SrcFileName, dtPoStatusView)
                'End If 'Added 20191114
            ElseIf drpTaskExport.SelectedValue = "23" Then
                'PO_Confirmation
                If DropDownMonth.SelectedValue = "0" Then
                    Call showMsg("Please select the month", "")
                    Exit Sub
                End If

                Dim Task As Integer = 0
                Dim DtFrom As String = ""
                Dim DtTo As String = ""
                Dim _POConfirmationModel As POConfirmationModel = New POConfirmationModel()
                'DateConversion
                _POConfirmationModel.DateFrom = DropDownYear.SelectedValue & "/" & DropDownMonth.SelectedValue & "/" & "01"
                _POConfirmationModel.DateTo = CommonControl.GetLastDayOfMonth(DropDownMonth.SelectedValue, DropDownYear.SelectedValue)

                _POConfirmationModel.UserId = userid
                _POConfirmationModel.UserName = userName
                _POConfirmationModel.SHIP_TO_BRANCH_CODE = DropListLocation.SelectedItem.Value
                _POConfirmationModel.SHIP_TO_BRANCH = DropListLocation.SelectedItem.Text
                _POConfirmationModel.SRC_FILE_NAME = _POConfirmationModel.SHIP_TO_BRANCH & "_PO" & DropDownYear.SelectedValue & DropDownMonth.SelectedValue & ".csv"
                '_ReturnCreditModel.DateFrom = DropDownYear.SelectedValue & "/" & DropDownMonth.SelectedValue & "/01"
                '_ReturnCreditModel.DateTo = CommonControl.GetLastDayOfMonth(DropDownMonth.SelectedValue, DropDownYear.SelectedValue)
                _Expflddtl.FileName = _POConfirmationModel.SRC_FILE_NAME
                _Expflddtl.SrcFileName = _POConfirmationModel.SRC_FILE_NAME
                _Expflddtl.ShipToBranch = DropListLocation.SelectedItem.Text
                _Expflddtl.ShipToBranchCode = DropListLocation.SelectedItem.Value
                _Expflddtl.ExportFile = drpTaskExport.SelectedValue
                _Expflddtl.UserId = _POConfirmationModel.UserId
                _Expflddtl.UserName = _POConfirmationModel.UserName
                _Expflddtl.DateFrom = _POConfirmationModel.DateFrom
                _Expflddtl.DateTo = _POConfirmationModel.DateTo
                'exfiledtl.number = _InvoiceUpdateModel.Number
                Session("Exportinputdtl") = _Expflddtl

                ViewState("SortExpression") = Nothing
                ViewState("SortDirection") = Nothing
                GenerateExportDetails(drpTaskExport.SelectedValue)
            End If
        End If

        'Page.MaintainScrollPositionOnPostBack = True
    End Sub
    Sub ExportCSV(ExportFileName As String, result As DataTable) 'VJ 2019/10/14 End
        Response.ContentType = "text/csv"
        Response.AddHeader("Content-Disposition", "attachment;filename=" & ExportFileName)
        Response.AddHeader("Pragma", "no-cache")
        Response.AddHeader("Cache-Control", "no-cache")
        Dim myData As Byte() = CommonControl.csvBytesWriter(result)
        Response.BinaryWrite(myData)
        Response.Flush()
        Response.SuppressContent = True
        HttpContext.Current.ApplicationInstance.CompleteRequest()
    End Sub
    Sub WeeklySetting()
        Dim dDate As DateTime = Date.Today
        'dDate.AddDays(-10)
        'DropDownDaySub.SelectedValue = dDate.AddDays(-10).Date

        DropDownMonth.SelectedValue = dDate.AddDays(-10).Month
        DropDownYear.SelectedValue = dDate.AddDays(-10).Year
        If dDate.AddDays(-10).Day >= 1 And dDate.AddDays(-10).Day <= 10 Then
            DropDownDaySub.SelectedValue = "01"
        ElseIf dDate.AddDays(-10).Day >= 11 And dDate.AddDays(-10).Day <= 20 Then
            DropDownDaySub.SelectedValue = "11"
        Else
            DropDownDaySub.SelectedValue = "21"
        End If
    End Sub
    Sub MonthlySetting()
        Dim dDate As DateTime = Date.Today
        Dim mon As String
        If dDate.AddDays(-30).Month.ToString().Length = 1 Then
            mon = "0" & dDate.AddDays(-30).Month.ToString()
        Else
            mon = dDate.AddDays(-30).Month.ToString()
        End If
        DropDownMonth.SelectedValue = mon
        DropDownYear.SelectedValue = dDate.AddDays(-30).Year
    End Sub
    Sub DefaultSearchSetting(ByVal Optional SearchType As String = "init")
        Dim dDate As DateTime = Date.Today
        If SearchType = "init" Then
            DropDownMonth.Visible = True
            DropDownYear.Visible = False
            DropDownDaySub.Visible = False
            DropDownDTSub.Visible = False
            TextDateFrom.Visible = False
            Label8.Visible = False
            TextDateTo.Visible = False
            Label7.Visible = False
            DropDownGR.Visible = False
            DropDownMonth.SelectedIndex = 0
            DropDownYear.SelectedIndex = 0
            DropDownDaySub.SelectedIndex = 0
            DropDownDTSub.SelectedIndex = 0
            TextDateFrom.Text = ""
            TextDateTo.Text = ""
        ElseIf SearchType = "old" Then
            DropDownMonth.Visible = True
            TextDateFrom.Visible = True
            Label8.Visible = True
            TextDateTo.Visible = True
            Label7.Visible = True
            DropDownYear.Visible = False
            DropDownDaySub.Visible = False
            DropDownDTSub.Visible = False
            DropDownGR.Visible = False
            TextDateFrom.Text = dDate.ToString("MM/dd/yyyy", CultureInfo.InvariantCulture)
            TextDateTo.Text = dDate.ToString("MM/dd/yyyy", CultureInfo.InvariantCulture)
            DropDownMonth.SelectedIndex = 0
            DropDownYear.SelectedIndex = 0
            DropDownDaySub.SelectedIndex = 0
            DropDownDTSub.SelectedIndex = 0
        ElseIf SearchType = "yyyymm" Then
            DropDownYear.Visible = True
            DropDownMonth.Visible = True
            DropDownDaySub.Visible = False
            DropDownDTSub.Visible = False
            TextDateFrom.Visible = False
            Label8.Visible = False
            TextDateTo.Visible = False
            Label7.Visible = False
            DropDownMonth.SelectedIndex = 0
            DropDownYear.SelectedIndex = 0
            DropDownDaySub.SelectedIndex = 0
            DropDownDTSub.SelectedIndex = 0
            TextDateFrom.Text = ""
            TextDateTo.Text = ""
            DropDownGR.Visible = False
        ElseIf SearchType = "yyyymmdd" Then
            DropDownYear.Visible = True
            DropDownMonth.Visible = True
            DropDownDaySub.Visible = True
            DropDownDTSub.Visible = False
            TextDateFrom.Visible = False
            Label8.Visible = False
            TextDateTo.Visible = False
            Label7.Visible = False
            DropDownMonth.SelectedIndex = 0
            DropDownYear.SelectedIndex = 0
            DropDownDaySub.SelectedIndex = 0
            DropDownDTSub.SelectedIndex = 0
            TextDateFrom.Text = ""
            TextDateTo.Text = ""
            DropDownGR.Visible = False
        ElseIf SearchType = "dtyyyymmdd" Then
            DropDownYear.Visible = True
            DropDownMonth.Visible = True
            DropDownDaySub.Visible = True
            DropDownDTSub.Visible = True
            TextDateFrom.Visible = False
            Label8.Visible = False
            TextDateTo.Visible = False
            Label7.Visible = False
            DropDownMonth.SelectedIndex = 0
            DropDownYear.SelectedIndex = 0
            DropDownDaySub.SelectedIndex = 0
            DropDownDTSub.SelectedIndex = 0
            TextDateFrom.Text = ""
            TextDateTo.Text = ""
            DropDownGR.Visible = False
        End If

    End Sub


    Protected Sub drpTaskExport_SelectedIndexChanged(sender As Object, e As EventArgs) Handles drpTaskExport.SelectedIndexChanged
        If drpTaskExport.SelectedValue = "-1" Then
            DefaultSearchSetting()
        ElseIf drpTaskExport.SelectedValue = "0" Then
            DefaultSearchSetting("old")

        ElseIf drpTaskExport.SelectedValue = "1" Then
            DefaultSearchSetting("old")

        ElseIf drpTaskExport.SelectedValue = "2" Then
            'Or
            '(drpTaskExport.SelectedValue = "2.2") Or
            ' (drpTaskExport.SelectedValue = "2.3") Then
            ''''DefaultSearchSetting("old")
            '''
            DefaultSearchSetting("yyyymm")
            TextDateFrom.Visible = True
            TextDateFrom.Text = ""
            Label8.Visible = True
            TextDateTo.Visible = True
            TextDateTo.Text = ""
            Label7.Visible = True
            MonthlySetting()
        ElseIf drpTaskExport.SelectedValue = "3A" Then
            DefaultSearchSetting("yyyymm")
            TextDateFrom.Visible = True
            TextDateFrom.Text = ""
            Label8.Visible = True
            TextDateTo.Visible = True
            TextDateTo.Text = ""
            Label7.Visible = True
            MonthlySetting()
        ElseIf drpTaskExport.SelectedValue = "3B" Then
            DefaultSearchSetting("yyyymm")
            TextDateFrom.Visible = True
            TextDateFrom.Text = ""
            Label8.Visible = True
            TextDateTo.Visible = True
            TextDateTo.Text = ""
            Label7.Visible = True
            MonthlySetting()
        ElseIf drpTaskExport.SelectedValue = "4" Then
            DefaultSearchSetting("yyyymm")
            TextDateFrom.Visible = True
            TextDateFrom.Text = ""
            Label8.Visible = True
            TextDateTo.Visible = True
            TextDateTo.Text = ""
            Label7.Visible = True
            MonthlySetting()
        ElseIf drpTaskExport.SelectedValue = "5" Then
            ''''''DefaultSearchSetting("yyyymm")
            '''

            DefaultSearchSetting("yyyymm")
            TextDateFrom.Visible = True
            TextDateFrom.Text = ""
            Label8.Visible = True
            TextDateTo.Visible = True
            TextDateTo.Text = ""
            Label7.Visible = True
        ElseIf drpTaskExport.SelectedValue = "6" Then
            DefaultSearchSetting("yyyymmdd")
            WeeklySetting()
        ElseIf drpTaskExport.SelectedValue = "7" Then
            DefaultSearchSetting("dtyyyymmdd")
            WeeklySetting()
        ElseIf (drpTaskExport.SelectedValue = "8A") Or (drpTaskExport.SelectedValue = "8B") Then
            DefaultSearchSetting("yyyymm")
            'DropDownGR.Visible = True
            TextDateFrom.Visible = True
            TextDateFrom.Text = ""
            Label8.Visible = True
            TextDateTo.Visible = True
            TextDateTo.Text = ""
            Label7.Visible = True
            MonthlySetting()
        ElseIf drpTaskExport.SelectedValue = "9" Then
            DefaultSearchSetting("yyyymm")
            MonthlySetting()
        ElseIf drpTaskExport.SelectedValue = "10" Then
            DefaultSearchSetting("yyyymm")
            MonthlySetting()
        ElseIf drpTaskExport.SelectedValue = "11" Then
            DefaultSearchSetting("yyyymm")
            MonthlySetting()
        ElseIf drpTaskExport.SelectedValue = "12" Then
            DefaultSearchSetting("init")
            Call showMsg("Coming Soon...", "")
            Exit Sub
        ElseIf drpTaskExport.SelectedValue = "13" Then
            DefaultSearchSetting("init")
            Call showMsg("Coming Soon...", "")
            Exit Sub
        ElseIf drpTaskExport.SelectedValue = "14" Then
            DefaultSearchSetting("yyyymm")
            MonthlySetting()
        ElseIf drpTaskExport.SelectedValue = "15" Then
            DefaultSearchSetting("yyyymm")
            MonthlySetting()
        ElseIf drpTaskExport.SelectedValue = "16" Then
            DefaultSearchSetting("yyyymmdd")
            WeeklySetting()
        ElseIf drpTaskExport.SelectedValue = "99" Then 'VJ 2019/10/14 Begin
            DefaultSearchSetting("old")
        ElseIf drpTaskExport.SelectedValue = "18" Then
            DefaultSearchSetting("yyyymm")
            MonthlySetting()
            'ElseIf drpTaskExport.SelectedValue = "18A" Then
            '    DefaultSearchSetting("yyyymm")
        ElseIf drpTaskExport.SelectedValue = "19" Then
            DefaultSearchSetting("yyyymm") 'VJ 2019/10/14 Begin
            MonthlySetting()
        ElseIf drpTaskExport.SelectedValue = "19B" Then
            DefaultSearchSetting("yyyymm") 'VJ 2019/10/18 Begin
            MonthlySetting()
        ElseIf drpTaskExport.SelectedValue = "20" Then
            DefaultSearchSetting("yyyymm") 'Added on 20191114
            MonthlySetting()
        ElseIf drpTaskExport.SelectedValue = "21" Then
            DefaultSearchSetting("yyyymm") 'Added on 20191114
            MonthlySetting()
        ElseIf drpTaskExport.SelectedValue = "23" Then
            DefaultSearchSetting("yyyymm") 'Added on 20200529
            MonthlySetting()
        End If
        'Dim dDate As DateTime = Date.Today

        'If TextDateFrom.Visible = True Then
        '    TextDateFrom.Text = dDate.ToString("MM/dd/yyyy", CultureInfo.InvariantCulture)
        'End If
        'If TextDateTo.Visible = True Then
        '    TextDateTo.Text = dDate.ToString("MM/dd/yyyy", CultureInfo.InvariantCulture)
        'End If
    End Sub




    Private Sub OldFunctionality()
        '***セッション情報取得***
        Dim userid As String = Session("user_id")
        Dim userName As String = Session("user_Name")
        Dim userLevel As String = Session("user_level")
        Dim adminFlg As Boolean = Session("admin_Flg")

        Dim setMon As String = Session("set_Mon2")
        Dim setMonName As String = Session("set_MonName")
        Dim exportFile As String = Session("export_File")
        Dim exportShipName As String = DropListLocation.SelectedItem.Text 'Session("export_shipName")

        Dim clsSetCommon As New Class_common
        Dim dtNow As DateTime = clsSetCommon.dtIndia

        If userid Is Nothing Then
            Call showMsg("The session has expired. Please login again.", "")
            Exit Sub
        End If

        Dim i, j As Integer

        '***入力チェック***
        If exportShipName = "Select Branch" Then
            Call showMsg("Please specify the branch name.", "")
            Exit Sub
        End If

        Dim dt As DateTime
        Dim dateFrom As String
        Dim dateTo As String
        Dim outputPath As String

        If TextDateFrom.Text <> "" Then
            If exportFile = "Sales Invoice to samsung ""C""" Or exportFile = "Sales Invoiec to samsung ""EXC""" Then
                Call showMsg("You can select only the month specification.", "")
                Exit Sub
            End If
            If DateTime.TryParse(TextDateFrom.Text, dt) Then
                dateFrom = DateTime.Parse(Trim(TextDateFrom.Text)).ToShortDateString
            Else
                Call showMsg("There is an error in the date specification", "")
                Exit Sub
            End If
        Else
            dateFrom = ""
        End If

        If TextDateTo.Text <> "" Then
            If exportFile = "Sales Invoice to samsung ""C""" Or exportFile = "Sales Invoiec to samsung ""EXC""" Then
                Call showMsg("You can select only the month specification.", "")
                Exit Sub
            End If
            If DateTime.TryParse(TextDateTo.Text, dt) Then
                dateTo = DateTime.Parse(Trim(TextDateTo.Text)).ToShortDateString
            Else
                Call showMsg("The date specification of To is incorrect.", "")
                Exit Sub
            End If
        Else
            dateTo = ""
        End If

        If dateFrom = "" And dateTo = "" And setMon = "00" Then
            Call showMsg("Please specify either output period by month or date", "")
            Exit Sub
        End If

        If dateFrom <> "" And dateTo <> "" And setMon <> "00" Then
            Call showMsg("Please specify either output period by month or date.", "")
            Exit Sub
        End If

        If setMon <> "00" Then
            If dateFrom <> "" Or dateTo <> "" Then
                Call showMsg("Please specify either output period by month or date.", "")
                Exit Sub
            End If
        End If

        If exportFile = "Select Export Filename" Then
            Call showMsg("Please specify the file type to be exported.", "")
            Exit Sub
        End If

        '***CSV出力処理***
        Dim clsSet As New Class_analysis
        Dim errFlg As Integer

        '■拠点コード取得
        Dim shipCode As String
        Dim errMsg As String

        Call clsSetCommon.setShipCode(exportShipName, shipCode, errMsg)

        If errMsg <> "" Then
            Call showMsg(errMsg, "")
            Exit Sub
        End If

        'CSVファイルの種類
        Dim kindExport As Integer = Left(exportFile, 1)

        ''Matching old Records
        ''<asp:ListItem Text = "0.PL_Tracking_Sheet" Value="0"></asp: ListItem>=> kindExport = 1
        '<asp:ListItem Text = "1.DailyRepairStatement" Value="1"></asp: ListItem>  --- ?? No functionality as of now?
        ''<asp:ListItem Text="2.Warranty Excel File" Value="2"></asp:ListItem>     --- ??  No functionality as of now
        ''<asp:ListItem Text = "3.Sales Register from GSPN IW" Value="3"></asp: ListItem>=> kindExport = 5
        ''<asp:ListItem Text = "4.Sales Register from GSPN OOW" Value="4"></asp: ListItem>=> kindExport = 2
        ''<asp:ListItem Text = "18.Sales Register from GSPN OOW" Value="4"></asp: ListItem>=> kindExport = 2

        If kindExport = "0" Then
            kindExport = "1"
        ElseIf kindExport = "1" Then
            'No funtionality as of now
            kindExport = -99
        ElseIf kindExport = "3" Then
            kindExport = "5"
        ElseIf kindExport = "4" Then
            kindExport = "2"
        ElseIf kindExport = "17" Then
            kindExport = "9"
        End If

        '■PL_Tracking_Sheet出力
        If kindExport = 1 Then
            '''''''Dim TmpRate As Decimal = 0.00
            '''''''If Decimal.TryParse(txtRate.Text, TmpRate) Then
            '''''''Else
            '''''''    Call showMsg("The rate is not valid ", "")
            '''''''    Exit Sub
            '''''''End If


            Try
                '***内容設定***
                '■Activity_reportデータ取得
                Dim activeData() As Class_analysis.ACTIVITY_REPORT
                Dim dsActivity_report As New DataSet
                Dim strSQL2 As String = ""

                If setMon <> "00" Then
                    '月指定
                    strSQL2 &= "SELECT (CONVERT(DATETIME, month + '/' + day)) as day ,  DATENAME(WEEKDAY,(CONVERT(DATETIME, month + '/' + day)) ) as youbi, "
                    strSQL2 &= "Customer_Visit, Call_Registerd, Repair_Completed,Goods_Delivered, Pending_Calls, Cancelled_Calls "
                    strSQL2 &= "FROM dbo.Activity_report WHERE Month = '" & Left(dtNow.ToShortDateString, 5) & setMon & "' AND location = '" & shipCode & "' "
                    strSQL2 &= "ORDER BY day;"
                Else
                    '日付指定
                    strSQL2 &= "SELECT (CONVERT(DATETIME, month + '/' + day)) as day ,  DATENAME(WEEKDAY,(CONVERT(DATETIME, month + '/' + day)) ) as youbi, "
                    strSQL2 &= "Customer_Visit, Call_Registerd, Repair_Completed,Goods_Delivered, Pending_Calls, Cancelled_Calls "
                    strSQL2 &= "FROM dbo.Activity_report WHERE location = '" & shipCode & "' "

                    If dateTo <> "" Then
                        If dateFrom <> "" Then
                            strSQL2 &= "AND (CONVERT(DATETIME, month + '/' + day)) <= CONVERT(DATETIME, '" & dateTo & "') "
                            strSQL2 &= "AND (CONVERT(DATETIME, month + '/' + day)) >= CONVERT(DATETIME, '" & dateFrom & "') "
                        Else
                            strSQL2 &= "AND (CONVERT(DATETIME, month + '/' + day)) <= CONVERT(DATETIME, '" & dateTo & "') "
                        End If
                    Else
                        If dateFrom <> "" Then
                            strSQL2 &= "AND (CONVERT(DATETIME, month + '/' + day)) >= CONVERT(DATETIME, '" & dateFrom & "') "
                        End If
                    End If

                    strSQL2 &= "ORDER BY day;"

                End If

                dsActivity_report = DBCommon.Get_DS(strSQL2, errFlg)

                If errFlg = 1 Then
                    Call showMsg("Failed to get Activity_report information.", "")
                    Exit Sub
                End If

                If dsActivity_report IsNot Nothing Then

                    If dsActivity_report.Tables(0).Rows.Count <> 0 Then

                        ReDim activeData(dsActivity_report.Tables(0).Rows.Count - 1)

                        Dim dr As DataRow

                        For i = 0 To dsActivity_report.Tables(0).Rows.Count - 1

                            dr = dsActivity_report.Tables(0).Rows(i)

                            Dim tmpDay As DateTime
                            Dim tmpMon As String
                            Dim tmpYear As String

                            If dr("day") IsNot DBNull.Value Then

                                '項目の日付をセット　例）2018/07/01は、01-Jul-2018　
                                tmpDay = dr("day")

                                'yyyy/mm/dd
                                activeData(i).day2 = tmpDay.ToShortDateString

                                'yyyy/mm/dd
                                activeData(i).day = tmpDay.ToShortDateString

                                'dd
                                activeData(i).day = activeData(i).day.Substring(8, 2)

                                'yyyy
                                tmpYear = activeData(i).day2.Substring(0, 4)

                                If setMonName <> "Select the month" Then

                                    '月指定は、ドロップリストで指定された月をセット
                                    'dd-mon-yyyy
                                    ''''''Comment on 20190917
                                    '''' activeData(i).day &= "-" & Left(setMonName, 3) & "-" & tmpYear
                                    tmpMon = activeData(i).day2.Substring(5, 2)
                                    Select Case Convert.ToInt32(tmpMon)
                                        Case 1
                                            activeData(i).day &= "-" & "Jan" & "-" & tmpYear
                                        Case 2
                                            activeData(i).day &= "-" & "Feb" & "-" & tmpYear
                                        Case 3
                                            activeData(i).day &= "-" & "Mar" & "-" & tmpYear
                                        Case 4
                                            activeData(i).day &= "-" & "Apr" & "-" & tmpYear
                                        Case 5
                                            activeData(i).day &= "-" & "May" & "-" & tmpYear
                                        Case 6
                                            activeData(i).day &= "-" & "Jun" & "-" & tmpYear
                                        Case 7
                                            activeData(i).day &= "-" & "Jul" & "-" & tmpYear
                                        Case 8
                                            activeData(i).day &= "-" & "Aug" & "-" & tmpYear
                                        Case 9
                                            activeData(i).day &= "-" & "Sep" & "-" & tmpYear
                                        Case 10
                                            activeData(i).day &= "-" & "Oct" & "-" & tmpYear
                                        Case 11
                                            activeData(i).day &= "-" & "Nov" & "-" & tmpYear
                                        Case 12
                                            activeData(i).day &= "-" & "Dec" & "-" & tmpYear
                                    End Select
                                Else

                                    '日付指定は、月を３文字列に変換してセット
                                    tmpMon = activeData(i).day2.Substring(5, 2)
                                    Select Case Convert.ToInt32(tmpMon)
                                        Case 1
                                            activeData(i).day &= "-" & "Jan" & "-" & tmpYear
                                        Case 2
                                            activeData(i).day &= "-" & "Feb" & "-" & tmpYear
                                        Case 3
                                            activeData(i).day &= "-" & "Mar" & "-" & tmpYear
                                        Case 4
                                            activeData(i).day &= "-" & "Apr" & "-" & tmpYear
                                        Case 5
                                            activeData(i).day &= "-" & "May" & "-" & tmpYear
                                        Case 6
                                            activeData(i).day &= "-" & "Jun" & "-" & tmpYear
                                        Case 7
                                            activeData(i).day &= "-" & "Jul" & "-" & tmpYear
                                        Case 8
                                            activeData(i).day &= "-" & "Aug" & "-" & tmpYear
                                        Case 9
                                            activeData(i).day &= "-" & "Sep" & "-" & tmpYear
                                        Case 10
                                            activeData(i).day &= "-" & "Oct" & "-" & tmpYear
                                        Case 11
                                            activeData(i).day &= "-" & "Nov" & "-" & tmpYear
                                        Case 12
                                            activeData(i).day &= "-" & "Dec" & "-" & tmpYear
                                    End Select
                                End If

                            End If

                            If dr("youbi") IsNot DBNull.Value Then
                                activeData(i).youbi = dr("youbi")
                            End If

                            If dr("Customer_Visit") IsNot DBNull.Value Then
                                activeData(i).Customer_Visit = dr("Customer_Visit")
                            End If

                            If dr("Call_Registerd") IsNot DBNull.Value Then
                                activeData(i).Call_Registerd = dr("Call_Registerd")
                            End If

                            If dr("Repair_Completed") IsNot DBNull.Value Then
                                activeData(i).Repair_Completed = dr("Repair_Completed")
                            End If

                            If dr("Goods_Delivered") IsNot DBNull.Value Then
                                activeData(i).Goods_Delivered = dr("Goods_Delivered")
                            End If

                            If dr("Pending_Calls") IsNot DBNull.Value Then
                                activeData(i).Pending_Calls = dr("Pending_Calls")
                            End If

                        Next i

                    End If

                Else
                    Call showMsg("There is no corresponding Activity_report information.", "")
                    Exit Sub
                End If

                '***出力順にセット***
                '■Activity_reportデータ出力
                'Modified by Mohan . 2019 07 09 - Request sent by Shimada san

                '項目名設定
                Dim csvContents = New List(Of String)
                Dim rowWork1 As String = exportShipName & ","
                Dim rowWork2 As String = " ,"
                Dim rowWork3 As String = "Customer Visit,"
                Dim rowWork4 As String = "Call Registered,"
                Dim rowWork5 As String = "Repair Completed,"
                Dim rowWork6 As String = "Goods Delivered,"
                Dim rowWork7 As String = "Pending,"

                Dim Total_Customer_Visit As Integer
                '''Dim Average_Customer_Visit As Integer
                Dim Total_Call_Registerd As Integer
                '''''Dim Average_Call_Registerd As Integer
                Dim Total_Repair_Completed As Integer
                ''''Dim Average_Repair_Completed As Integer
                Dim Total_Goods_Delivered As Integer
                ''''Dim Average_Goods_Delivered As Integer
                Dim Total_Pending_Calls As Integer
                ''''Dim Average_Pending_Calls As Integer


                '日付項目分処理
                For i = 0 To dsActivity_report.Tables(0).Rows.Count - 1
                    rowWork1 &= activeData(i).day & ","
                    rowWork2 &= activeData(i).youbi & ","
                    rowWork3 &= activeData(i).Customer_Visit & ","
                    rowWork4 &= activeData(i).Call_Registerd & ","
                    rowWork5 &= activeData(i).Repair_Completed & ","
                    rowWork6 &= activeData(i).Goods_Delivered & ","
                    rowWork7 &= activeData(i).Pending_Calls & ","
                    Total_Customer_Visit = Total_Customer_Visit + activeData(i).Customer_Visit
                    Total_Call_Registerd = Total_Call_Registerd + activeData(i).Call_Registerd
                    Total_Repair_Completed = Total_Repair_Completed + activeData(i).Repair_Completed
                    Total_Goods_Delivered = Total_Goods_Delivered + activeData(i).Goods_Delivered
                    Total_Pending_Calls = Total_Pending_Calls + activeData(i).Pending_Calls
                Next i

                ''''Average_Customer_Visit = Total_Customer_Visit / dsActivity_report.Tables(0).Rows.Count
                ''''Average_Call_Registerd = Total_Call_Registerd / dsActivity_report.Tables(0).Rows.Count
                ''''Average_Repair_Completed = Total_Repair_Completed / dsActivity_report.Tables(0).Rows.Count
                ''''Average_Goods_Delivered = Total_Goods_Delivered / dsActivity_report.Tables(0).Rows.Count
                ''''Average_Pending_Calls = Total_Pending_Calls / dsActivity_report.Tables(0).Rows.Count

                '項目
                rowWork1 &= "Total, " & exportShipName

                '曜日
                rowWork2 &= " , "
                rowWork3 &= Total_Customer_Visit & ",Customer Visit"
                rowWork4 &= Total_Call_Registerd & ",Call Registered"
                rowWork5 &= Total_Repair_Completed & ",Repair Completed"
                rowWork6 &= Total_Goods_Delivered & ",Goods Delivered"
                rowWork7 &= Total_Pending_Calls & ",Pending"

                csvContents.Add(rowWork1)
                csvContents.Add(rowWork2)
                csvContents.Add(rowWork3)
                csvContents.Add(rowWork4)
                csvContents.Add(rowWork5)
                csvContents.Add(rowWork6)
                csvContents.Add(rowWork7)

                '■SC_DSR_infoデータ取得
                Dim totalDailyStatementRepartData() As Class_analysis.DAILYSTATEMENTREPART
                Dim dsSC_DSR_info As New DataSet
                Dim strSQL3 As String = ""
                'VJ - 2020-04-16 SSC parent child Comment this code not used
                'Dim ServiceCenterName As String
                ''DropListLocation.SelectedItem.Text
                'If DropListLocation.SelectedItem.Text = "SSC1" Then
                '    ServiceCenterName = "SSC1','SSC2"
                'Else
                '    ServiceCenterName = exportShipName
                'End If

                'VJ - 2020-04-16 SSC parent child Add
                Dim shipName As String = "'" & DropListLocation.SelectedItem.Text & "'"
                Dim ischild As Boolean = False
                Dim isParent As Boolean = False


                Dim shipdtlParent As DataRow() = _ShipBaseControl.SelectShipBaseDetails().Select("ship_name = " & shipName & " AND IsChildShip = 1 AND DELFG = 0")
                Dim shipdtlChild As DataRow() = _ShipBaseControl.SelectShipBaseDetails().Select("Parent_Ship_Name = " & shipName & " AND IsChildShip = 1 AND DELFG = 0")
                'Dim shipname As String
                If (shipdtlParent.Count() > 0) Then
                    ischild = True
                    'For Each row As DataRow In shipdtlParent
                    '    shipName &= ",'" & row.Item("Parent_Ship_Name").ToString() & "'"
                    '    ' DataTable.ImportRow(row)
                    'Next
                End If
                If (shipdtlChild.Count() > 0) Then
                    isParent = True
                    For Each row As DataRow In shipdtlChild
                        shipName &= ",'" & row.Item("ship_name").ToString() & "'"
                        'DataTable.ImportRow(row)
                    Next

                End If

                If setMon <> "00" Then
                    '月指定
                    strSQL3 &= "SELECT * "
                    'Comment add on 20190916
                    'VJ - 2020-04-16 SSC parent child Modified
                    'VJ 2019/11/05 PL Tracking Sheet SAW Discount(parts) SSC2 data include in SSC1 and SSC2 data set to null Remaining Branch use Existing functionality 
                    'strSQL3 &= ",(select sum(LABOR) from Invoice_update where upload_Branch='" & exportShipName & "'  and DELFG = 0 and LEFT(CONVERT(VARCHAR, INVOICE_DATE, 111), 10)=LEFT(CONVERT(VARCHAR, dbo.SC_DSR_info.Billing_date, 111), 10)) as 'labor', (select case when '" & ServiceCenterName.Replace("','", ",") & "'= 'SSC2' then null else sum(parts) end from Invoice_update where upload_Branch IN ('" & ServiceCenterName & "')  and DELFG = 0 and LEFT(CONVERT(VARCHAR, INVOICE_DATE, 111), 10)=LEFT(CONVERT(VARCHAR, dbo.SC_DSR_info.Billing_date, 111), 10)) as 'parts' "
                    'If DropListLocation.SelectedItem.Text = "SSC1" Then
                    If isParent = True Then
                        'strSQL3 &= ",(select sum(LABOR) from Invoice_update where upload_Branch='" & exportShipName & "'  and DELFG = 0 and LEFT(CONVERT(VARCHAR, INVOICE_DATE, 111), 10)=LEFT(CONVERT(VARCHAR, dbo.SC_DSR_info.Billing_date, 111), 10)) as 'labor', (select sum(parts) from Invoice_update where upload_Branch IN ('SSC1','SSC2')  and DELFG = 0 and LEFT(CONVERT(VARCHAR, INVOICE_DATE, 111), 10)=LEFT(CONVERT(VARCHAR, dbo.SC_DSR_info.Billing_date, 111), 10)) as 'parts' "
                        strSQL3 &= ",(select sum(LABOR) from Invoice_update where upload_Branch='" & exportShipName & "'  and DELFG = 0 and LEFT(CONVERT(VARCHAR, INVOICE_DATE, 111), 10)=LEFT(CONVERT(VARCHAR, dbo.SC_DSR_info.Billing_date, 111), 10)) as 'labor', (select sum(parts) from Invoice_update where upload_Branch IN (" & shipName & ")  and DELFG = 0 and LEFT(CONVERT(VARCHAR, INVOICE_DATE, 111), 10)=LEFT(CONVERT(VARCHAR, dbo.SC_DSR_info.Billing_date, 111), 10)) as 'parts' "
                        ' ElseIf DropListLocation.SelectedItem.Text = "SSC2" Then
                    ElseIf ischild = True Then

                        strSQL3 &= ",(select sum(LABOR) from Invoice_update where upload_Branch='" & exportShipName & "'  and DELFG = 0 and LEFT(CONVERT(VARCHAR, INVOICE_DATE, 111), 10)=LEFT(CONVERT(VARCHAR, dbo.SC_DSR_info.Billing_date, 111), 10)) as 'labor', null as 'parts' "
                    Else
                        strSQL3 &= ",(select sum(LABOR) from Invoice_update where upload_Branch='" & exportShipName & "'  and DELFG = 0 and LEFT(CONVERT(VARCHAR, INVOICE_DATE, 111), 10)=LEFT(CONVERT(VARCHAR, dbo.SC_DSR_info.Billing_date, 111), 10)) as 'labor', (select sum(parts) from Invoice_update where upload_Branch='" & exportShipName & "'  and DELFG = 0 and LEFT(CONVERT(VARCHAR, INVOICE_DATE, 111), 10)=LEFT(CONVERT(VARCHAR, dbo.SC_DSR_info.Billing_date, 111), 10)) as 'parts' "
                    End If
                    'Original
                    'strSQL3 &= ",(select sum(LABOR) from Invoice_update where upload_Branch='" & exportShipName & "'  and DELFG = 0 and LEFT(CONVERT(VARCHAR, INVOICE_DATE, 111), 10)=LEFT(CONVERT(VARCHAR, dbo.SC_DSR_info.Billing_date, 111), 10)) as 'labor', (select sum(parts) from Invoice_update where upload_Branch='" & exportShipName & "'  and DELFG = 0 and LEFT(CONVERT(VARCHAR, INVOICE_DATE, 111), 10)=LEFT(CONVERT(VARCHAR, dbo.SC_DSR_info.Billing_date, 111), 10)) as 'parts' "
                    strSQL3 &= " FROM dbo.SC_DSR_info "
                    strSQL3 &= "WHERE DELFG = 0 And Branch_name = '" & exportShipName & "' AND LEFT(CONVERT(VARCHAR, Billing_date,111),7) = '" & Left(dtNow.ToShortDateString, 5) & setMon & "';"
                Else
                    '日付指定
                    strSQL3 &= "SELECT * "
                    'VJ 2019/11/05 PL Tracking Sheet SAW Discount(parts) SSC2 data include in SSC1 and SSC2 data set to null Remaining Branch use Existing functionality
                    'VJ - 2020-04-16 SSC parent child Modified

                    'If DropListLocation.SelectedItem.Text = "SSC1" Then
                    '    strSQL3 &= ",(select sum(LABOR) from Invoice_update where upload_Branch='" & exportShipName & "'  and DELFG = 0 and LEFT(CONVERT(VARCHAR, INVOICE_DATE, 111), 10)=LEFT(CONVERT(VARCHAR, dbo.SC_DSR_info.Billing_date, 111), 10)) as 'labor', (select sum(parts) from Invoice_update where upload_Branch IN ('SSC1','SSC2')  and DELFG = 0 and LEFT(CONVERT(VARCHAR, INVOICE_DATE, 111), 10)=LEFT(CONVERT(VARCHAR, dbo.SC_DSR_info.Billing_date, 111), 10)) as 'parts' "
                    'ElseIf DropListLocation.SelectedItem.Text = "SSC2" Then
                    '    strSQL3 &= ",(select sum(LABOR) from Invoice_update where upload_Branch='" & exportShipName & "'  and DELFG = 0 and LEFT(CONVERT(VARCHAR, INVOICE_DATE, 111), 10)=LEFT(CONVERT(VARCHAR, dbo.SC_DSR_info.Billing_date, 111), 10)) as 'labor', null as 'parts' "
                    If isParent = True Then
                        strSQL3 &= ",(select sum(LABOR) from Invoice_update where upload_Branch='" & exportShipName & "'  and DELFG = 0 and LEFT(CONVERT(VARCHAR, INVOICE_DATE, 111), 10)=LEFT(CONVERT(VARCHAR, dbo.SC_DSR_info.Billing_date, 111), 10)) as 'labor', (select sum(parts) from Invoice_update where upload_Branch IN (" & shipName & ")  and DELFG = 0 and LEFT(CONVERT(VARCHAR, INVOICE_DATE, 111), 10)=LEFT(CONVERT(VARCHAR, dbo.SC_DSR_info.Billing_date, 111), 10)) as 'parts' "
                    ElseIf ischild = True Then
                        strSQL3 &= ",(select sum(LABOR) from Invoice_update where upload_Branch='" & exportShipName & "'  and DELFG = 0 and LEFT(CONVERT(VARCHAR, INVOICE_DATE, 111), 10)=LEFT(CONVERT(VARCHAR, dbo.SC_DSR_info.Billing_date, 111), 10)) as 'labor', null as 'parts' "
                    Else
                        strSQL3 &= ",(select sum(LABOR) from Invoice_update where upload_Branch='" & exportShipName & "'  and DELFG = 0 and LEFT(CONVERT(VARCHAR, INVOICE_DATE, 111), 10)=LEFT(CONVERT(VARCHAR, dbo.SC_DSR_info.Billing_date, 111), 10)) as 'labor', (select sum(parts) from Invoice_update where upload_Branch='" & exportShipName & "'  and DELFG = 0 and LEFT(CONVERT(VARCHAR, INVOICE_DATE, 111), 10)=LEFT(CONVERT(VARCHAR, dbo.SC_DSR_info.Billing_date, 111), 10)) as 'parts' "
                    End If
                    'Original
                    'strSQL3 &= ",(select sum(LABOR) from Invoice_update where upload_Branch='" & exportShipName & "'  and DELFG = 0 and LEFT(CONVERT(VARCHAR, INVOICE_DATE, 111), 10)=LEFT(CONVERT(VARCHAR, dbo.SC_DSR_info.Billing_date, 111), 10)) as 'labor', (select sum(parts) from Invoice_update where upload_Branch='" & exportShipName & "'  and DELFG = 0 and LEFT(CONVERT(VARCHAR, INVOICE_DATE, 111), 10)=LEFT(CONVERT(VARCHAR, dbo.SC_DSR_info.Billing_date, 111), 10)) as 'parts' "
                    strSQL3 &= " FROM dbo.SC_DSR_info "
                    strSQL3 &= "WHERE DELFG = 0 And Branch_name = '" & exportShipName & "' "

                    If dateTo <> "" Then
                        If dateFrom <> "" Then
                            strSQL3 &= "AND LEFT(CONVERT(VARCHAR, Billing_date, 111), 10) <= '" & dateTo & "' "
                            strSQL3 &= "AND LEFT(CONVERT(VARCHAR, Billing_date, 111), 10) >= '" & dateFrom & "';"
                        Else
                            strSQL3 &= "AND LEFT(CONVERT(VARCHAR, Billing_date, 111), 10) <= '" & dateTo & "';"
                        End If
                    Else
                        If dateFrom <> "" Then
                            strSQL3 &= "AND LEFT(CONVERT(VARCHAR, Billing_date, 111), 10) >= '" & dateFrom & "';"
                        End If
                    End If

                End If

                dsSC_DSR_info = DBCommon.Get_DS(strSQL3, errFlg)

                If errFlg = 1 Then
                    Call showMsg("Failed to get information on SC_DSR_info.", "")
                    Exit Sub
                End If

                If dsSC_DSR_info IsNot Nothing Then

                    If dsSC_DSR_info.Tables(0).Rows.Count <> 0 Then

                        ReDim totalDailyStatementRepartData(dsSC_DSR_info.Tables(0).Rows.Count - 1)

                        Dim dr As DataRow

                        For i = 0 To dsSC_DSR_info.Tables(0).Rows.Count - 1

                            dr = dsSC_DSR_info.Tables(0).Rows(i)

                            Dim tmpDay As DateTime

                            If dr("Billing_date") IsNot DBNull.Value Then
                                tmpDay = dr("Billing_date")
                                totalDailyStatementRepartData(i).Billing_date = tmpDay.ToShortDateString
                            End If

                            '①
                            If dr("IW_goods_total") IsNot DBNull.Value Then
                                totalDailyStatementRepartData(i).IW_goods_total = dr("IW_goods_total")
                            End If

                            '②
                            If dr("OW_goods_total") IsNot DBNull.Value Then
                                totalDailyStatementRepartData(i).OW_goods_total = dr("OW_goods_total")
                            End If

                            '③
                            If dr("IW_Labor_total") IsNot DBNull.Value Then
                                totalDailyStatementRepartData(i).IW_Labor = dr("IW_Labor_total")
                            End If

                            '④
                            If dr("IW_Parts_total") IsNot DBNull.Value Then
                                totalDailyStatementRepartData(i).IW_Parts = dr("IW_Parts_total")
                            End If

                            'Added on 20190905
                            If dr("IW_Transport_total") IsNot DBNull.Value Then
                                totalDailyStatementRepartData(i).IW_Transport = dr("IW_Transport_total")
                            End If
                            If dr("IW_Others_total") IsNot DBNull.Value Then
                                totalDailyStatementRepartData(i).IW_Others = dr("IW_Others_total")
                            End If

                            '⑤
                            If dr("IW_Tax_total") IsNot DBNull.Value Then
                                totalDailyStatementRepartData(i).IW_Tax = dr("IW_Tax_total")
                            End If

                            '⑥
                            If dr("IW_Total_total") IsNot DBNull.Value Then
                                totalDailyStatementRepartData(i).IW_total = dr("IW_Total_total")
                            End If

                            '⑦
                            If dr("OW_Labor_total") IsNot DBNull.Value Then
                                totalDailyStatementRepartData(i).OW_Labor = dr("OW_Labor_total")
                            End If

                            '⑧
                            If dr("OW_Parts_total") IsNot DBNull.Value Then
                                totalDailyStatementRepartData(i).OW_Parts = dr("OW_Parts_total")
                            End If

                            If dr("OW_Transport_total") IsNot DBNull.Value Then
                                totalDailyStatementRepartData(i).OW_Transport = dr("OW_Transport_total")
                            End If
                            If dr("OW_Others_total") IsNot DBNull.Value Then
                                totalDailyStatementRepartData(i).OW_Others = dr("OW_Others_total")
                            End If



                            '⑨
                            If dr("OW_Tax_total") IsNot DBNull.Value Then
                                totalDailyStatementRepartData(i).OW_Tax = dr("OW_Tax_total")
                            End If

                            '⑩
                            If dr("OW_Total_total") IsNot DBNull.Value Then
                                totalDailyStatementRepartData(i).OW_total = dr("OW_Total_total")
                            End If


                            If dr("labor") IsNot DBNull.Value Then
                                totalDailyStatementRepartData(i).Labor = dr("labor")
                            End If
                            If dr("parts") IsNot DBNull.Value Then
                                totalDailyStatementRepartData(i).Parts = dr("parts")
                            End If

                        Next i

                    End If

                    '■dailystatementrepartデータ出力
                    '項目名設定
                    ''   Dim rowWork8 As String = ","
                    Dim rowWork9 As String = "Warranty (Number),"        '①+②
                    Dim rowWork10 As String = "In Warranty (Number),"   '①
                    Dim rowWork11 As String = "Out Warranty (Number),"   '②

                    Dim rowWork12 As String = "In Warranty (Amount),"    '③+④　
                    Dim rowWork13 As String = "In Warranty (Labour),"     '③
                    Dim rowWork14 As String = "In Warranty (Parts),"      '④
                    Dim rowWork15 As String = "In Warranty (Transport),"      '④
                    Dim rowWork16 As String = "In Warranty (Others),"      '④

                    Dim rowWork17 As String = "Out Warranty (Amount),"    '⑦+⑧
                    Dim rowWork18 As String = "Out Warranty (Labour),"    '⑦
                    Dim rowWork19 As String = "Out Warranty (Parts),"     '⑧
                    Dim rowWork20 As String = "Out Warranty (Transport),"    '⑦
                    Dim rowWork21 As String = "Out Warranty (Others),"     '⑧

                    Dim rowWork22 As String = "SAW Discount (Labour),"
                    Dim rowWork23 As String = "SAW Discount (Parts),"
                    Dim rowWork24 As String = "Out Warranty (Labour) w/SAW Disc,"
                    Dim rowWork25 As String = "Out Warranty (Parts) w/SAW Disc,"

                    Dim rowWork26 As String = "Revenue without Tax,"
                    Dim rowWork27 As String = "IW parts consumed,"
                    Dim rowWork28 As String = "Total parts consumed,"
                    Dim rowWork29 As String = "Prime Cost Total,"
                    Dim rowWork30 As String = "Gross Profit,"
                    Dim rowWork31 As String = ","


                    Dim WarrantyNumber, WarrantyNumber1 As Decimal
                    Dim InWarrantyNumber As Decimal
                    Dim OutWarrantyNumber As Decimal
                    Dim InWarrantyAmount, InWarrantyAmount1 As Decimal
                    Dim InWarrantyLabour As Decimal
                    Dim InWarrantyParts As Decimal
                    Dim InWarrantyTransport As Decimal
                    Dim InWarrantyOthers As Decimal
                    Dim OutWarrantyAmount, OutWarrantyAmount1 As Decimal
                    Dim OutWarrantyLabour As Decimal
                    Dim OutWarrantyParts As Decimal
                    Dim OutWarrantyTransport As Decimal
                    Dim OutWarrantyOthers As Decimal
                    Dim SAWDiscountLabour As Decimal
                    Dim SAWDiscountParts As Decimal
                    Dim OutWarrantyLabourwSAWDisc, OutWarrantyLabourwSAWDisc1 As Decimal
                    Dim OutWarrantyPartswSAWDisc, OutWarrantyPartswSAWDisc1 As Decimal
                    Dim RevenuewithoutTax, RevenuewithoutTax1 As Decimal
                    Dim IWpartsconsumed, IWpartsconsumed1 As Decimal
                    Dim TotalPartsConsumed, TotalPartsConsumed1 As Decimal
                    Dim PrimeCostTotal, PrimeCostTotal1 As Decimal
                    Dim GrossProfit, GrossProfit1 As Decimal


                    Dim dailyCount As Integer

                    '日付項目分処理
                    For i = 0 To dsActivity_report.Tables(0).Rows.Count - 1

                        For j = 0 To dsSC_DSR_info.Tables(0).Rows.Count - 1

                            If activeData(i).day2 = totalDailyStatementRepartData(j).Billing_date Then

                                dailyCount = dailyCount + 1

                                'count
                                WarrantyNumber1 = totalDailyStatementRepartData(j).IW_goods_total + totalDailyStatementRepartData(j).OW_goods_total
                                rowWork9 &= WarrantyNumber1
                                WarrantyNumber = WarrantyNumber + WarrantyNumber1

                                rowWork10 &= totalDailyStatementRepartData(j).IW_goods_total
                                InWarrantyNumber = InWarrantyNumber + totalDailyStatementRepartData(j).IW_goods_total

                                rowWork11 &= totalDailyStatementRepartData(j).OW_goods_total
                                OutWarrantyNumber = OutWarrantyNumber + totalDailyStatementRepartData(j).OW_goods_total


                                'money
                                InWarrantyAmount1 = totalDailyStatementRepartData(j).IW_Labor + totalDailyStatementRepartData(j).IW_Transport + totalDailyStatementRepartData(j).IW_Others
                                rowWork12 &= InWarrantyAmount1
                                InWarrantyAmount = InWarrantyAmount + InWarrantyAmount1

                                rowWork13 &= totalDailyStatementRepartData(j).IW_Labor
                                InWarrantyLabour = InWarrantyLabour + totalDailyStatementRepartData(j).IW_Labor

                                rowWork14 &= totalDailyStatementRepartData(j).IW_Parts
                                InWarrantyParts = InWarrantyParts + totalDailyStatementRepartData(j).IW_Parts

                                rowWork15 &= totalDailyStatementRepartData(j).IW_Transport
                                InWarrantyTransport = InWarrantyTransport + totalDailyStatementRepartData(j).IW_Transport

                                rowWork16 &= totalDailyStatementRepartData(j).IW_Others
                                InWarrantyOthers = InWarrantyOthers + totalDailyStatementRepartData(j).IW_Others

                                OutWarrantyAmount1 = totalDailyStatementRepartData(j).OW_Labor + totalDailyStatementRepartData(j).OW_Parts + totalDailyStatementRepartData(j).OW_Transport + totalDailyStatementRepartData(j).OW_Others + totalDailyStatementRepartData(j).Labor + totalDailyStatementRepartData(j).Parts
                                rowWork17 &= OutWarrantyAmount1
                                OutWarrantyAmount = OutWarrantyAmount + OutWarrantyAmount1

                                rowWork18 &= totalDailyStatementRepartData(j).OW_Labor
                                OutWarrantyLabour = OutWarrantyLabour + totalDailyStatementRepartData(j).OW_Labor

                                rowWork19 &= totalDailyStatementRepartData(j).OW_Parts
                                OutWarrantyParts = OutWarrantyParts + totalDailyStatementRepartData(j).OW_Parts

                                rowWork20 &= totalDailyStatementRepartData(j).OW_Transport
                                OutWarrantyTransport = OutWarrantyTransport + totalDailyStatementRepartData(j).OW_Transport

                                rowWork21 &= totalDailyStatementRepartData(j).OW_Others
                                OutWarrantyOthers = OutWarrantyOthers + totalDailyStatementRepartData(j).OW_Others

                                rowWork22 &= totalDailyStatementRepartData(j).Labor
                                SAWDiscountLabour = SAWDiscountLabour + totalDailyStatementRepartData(j).Labor

                                rowWork23 &= totalDailyStatementRepartData(j).Parts
                                SAWDiscountParts = SAWDiscountParts + totalDailyStatementRepartData(j).Parts

                                OutWarrantyLabourwSAWDisc1 = totalDailyStatementRepartData(j).OW_Labor + totalDailyStatementRepartData(j).Labor
                                rowWork24 &= OutWarrantyLabourwSAWDisc1
                                OutWarrantyLabourwSAWDisc = OutWarrantyLabourwSAWDisc + OutWarrantyLabourwSAWDisc1

                                OutWarrantyPartswSAWDisc1 = totalDailyStatementRepartData(j).OW_Parts + totalDailyStatementRepartData(j).Parts
                                rowWork25 &= OutWarrantyPartswSAWDisc1
                                OutWarrantyPartswSAWDisc = OutWarrantyPartswSAWDisc + OutWarrantyPartswSAWDisc1

                                RevenuewithoutTax1 = InWarrantyAmount1 + OutWarrantyAmount1
                                rowWork26 &= RevenuewithoutTax1
                                RevenuewithoutTax = RevenuewithoutTax + RevenuewithoutTax1

                                IWpartsconsumed1 = totalDailyStatementRepartData(j).IW_Parts * 0.88
                                rowWork27 &= IWpartsconsumed1
                                IWpartsconsumed = IWpartsconsumed + IWpartsconsumed1


                                TotalPartsConsumed1 = (totalDailyStatementRepartData(j).IW_Parts + OutWarrantyPartswSAWDisc1) * 0.88
                                rowWork28 &= TotalPartsConsumed1
                                TotalPartsConsumed = TotalPartsConsumed + TotalPartsConsumed1

                                PrimeCostTotal1 = TotalPartsConsumed1 - IWpartsconsumed1
                                rowWork29 &= PrimeCostTotal1
                                PrimeCostTotal = PrimeCostTotal + PrimeCostTotal1

                                GrossProfit1 = RevenuewithoutTax1 - PrimeCostTotal1
                                rowWork30 &= GrossProfit1
                                GrossProfit = GrossProfit + GrossProfit1

                                rowWork31 &= (GrossProfit1 / RevenuewithoutTax1) * 100
                                rowWork31 &= "%"

                            End If

                        Next j

                        rowWork9 &= ","
                        rowWork10 &= ","
                        rowWork11 &= ","
                        rowWork12 &= ","
                        rowWork13 &= ","
                        rowWork14 &= ","
                        rowWork15 &= ","
                        rowWork16 &= ","
                        rowWork17 &= ","
                        rowWork18 &= ","
                        rowWork19 &= ","
                        rowWork20 &= ","
                        rowWork21 &= ","

                        rowWork22 &= ","
                        rowWork23 &= ","
                        rowWork24 &= ","
                        rowWork25 &= ","
                        rowWork26 &= ","
                        rowWork27 &= ","
                        rowWork28 &= ","
                        rowWork29 &= ","
                        rowWork30 &= ","
                        rowWork31 &= ","



                    Next i



                    rowWork9 &= WarrantyNumber & ",Warranty (Number)"
                    rowWork10 &= InWarrantyNumber & ",In Warranty (Number)"
                    rowWork11 &= OutWarrantyNumber & ",Out Warranty (Number)"
                    rowWork12 &= InWarrantyAmount & ",In Warranty (Amount)"
                    rowWork13 &= InWarrantyLabour & ",In Warranty (Labour)"
                    rowWork14 &= InWarrantyParts & ",In Warranty (Parts)"
                    rowWork15 &= InWarrantyTransport & ",In Warranty (Transport)"
                    rowWork16 &= InWarrantyOthers & ",In Warranty (Others)"

                    rowWork17 &= OutWarrantyAmount & ",Out Warranty (Amount)"
                    rowWork18 &= OutWarrantyLabour & ",Out Warranty (Labour)"
                    rowWork19 &= OutWarrantyParts & ",Out Warranty (Parts)"
                    rowWork20 &= OutWarrantyTransport & ",Out Warranty (Transport)"
                    rowWork21 &= OutWarrantyOthers & ",Out Warranty (Others)"
                    rowWork22 &= SAWDiscountLabour & ",SAW Discount (Labour)"
                    rowWork23 &= SAWDiscountParts & ",SAW Discount (Parts)"
                    rowWork24 &= OutWarrantyLabourwSAWDisc & ",Out Warranty (Labour) w/SAW Disc"
                    rowWork25 &= OutWarrantyPartswSAWDisc & ",Out Warranty (Parts) w/SAW Disc"
                    rowWork26 &= RevenuewithoutTax & ",Revenue without Tax"
                    rowWork27 &= IWpartsconsumed & ",IW parts consumed"
                    rowWork28 &= TotalPartsConsumed & ",Total parts consumed"
                    rowWork29 &= PrimeCostTotal & ",Prime Cost Total"
                    rowWork30 &= GrossProfit & ",Gross Profit"
                    rowWork31 &= (GrossProfit / RevenuewithoutTax) * 100 & "%, "




                    ' csvContents.Add(rowWork8)
                    csvContents.Add(rowWork9)
                    csvContents.Add(rowWork10)
                    csvContents.Add(rowWork11)
                    csvContents.Add(rowWork12)
                    csvContents.Add(rowWork13)
                    csvContents.Add(rowWork14)
                    csvContents.Add(rowWork15)
                    csvContents.Add(rowWork16)
                    csvContents.Add(rowWork17)
                    csvContents.Add(rowWork18)
                    csvContents.Add(rowWork19)
                    csvContents.Add(rowWork20)
                    csvContents.Add(rowWork21)

                    csvContents.Add(rowWork22)
                    csvContents.Add(rowWork23)
                    csvContents.Add(rowWork24)
                    csvContents.Add(rowWork25)
                    csvContents.Add(rowWork26)
                    csvContents.Add(rowWork27)
                    csvContents.Add(rowWork28)
                    csvContents.Add(rowWork29)
                    csvContents.Add(rowWork30)
                    csvContents.Add(rowWork31)




                    'csvContents.Add(rowWork22)
                    ''Modified
                    'csvContents.Add(rowWork23)
                    ''Added Gross Profit
                    'csvContents.Add(rowWork24)
                    ''Added Gross Profit2
                    'csvContents.Add(rowWork25)

                Else
                    csvContents.Add("There is no corresponding SC_DSR_info information.")
                End If

                'ファイル名、パスの設定
                Dim csvFileName As String

                dateFrom = Replace(dateFrom, "/", "")
                dateTo = Replace(dateTo, "/", "")

                'exportFile名の頭、数値を除く
                exportFile = Right(exportFile, exportFile.Length - 2)

                If setMon = "00" Then
                    If dateTo <> "" And dateFrom <> "" Then
                        csvFileName = exportFile & "_ " & exportShipName & "_" & dateFrom & "_" & dateTo & ".csv"
                    Else
                        If dateTo <> "" Then
                            csvFileName = exportFile & "_ " & exportShipName & "_" & dateTo & ".csv"
                        End If
                        If dateFrom <> "" Then
                            csvFileName = exportFile & "_ " & exportShipName & "_" & dateFrom & ".csv"
                        End If
                    End If
                Else
                    '月指定のとき
                    csvFileName = exportFile & "_ " & exportShipName & "_" & dtNow.ToString("yyyy") & "_" & setMon & ".csv"
                End If

                outputPath = clsSet.CsvFilePass & csvFileName

                Using writer As New StreamWriter(outputPath, False, Encoding.Default)
                    writer.WriteLine(String.Join(Environment.NewLine, csvContents))
                End Using

                'ファイルの内容をバイト配列にすべて読み込む 
                Dim Buffer As Byte() = System.IO.File.ReadAllBytes(outputPath)

                'サーバに保存されたCSVファイルを削除
                '※Response.End以降、ファイル削除処理ができないため、ファイルのダウンロードではなく、一旦ファイルの内容を
                '上記、Bufferに保存し、ダウンロード処理を行う。

                If outputPath <> "" Then
                    If System.IO.File.Exists(outputPath) = True Then
                        System.IO.File.Delete(outputPath)
                    End If
                End If

                ' ダウンロード
                Response.ContentType = "application/text/csv"
                Response.AddHeader("Content-Disposition", "attachment; filename=" & csvFileName)
                Response.Flush()
                'Response.Write("<b>File Contents: </b>")
                Response.BinaryWrite(Buffer)
                'Response.WriteFile(outputPath)
                Response.End()

            Catch ex As System.Threading.ThreadAbortException
                'Response.End()の呼び出しによりエラーメッセージを出力しないようにする

            Catch ex As Exception
                Call showMsg("CSV output processing failed.", "")
            End Try

        ElseIf kindExport = 2 Then

            'SalesRegister_OOW
            '部品情報を登録して、出力対象情報を取得
            Dim dsWtyExcel As New DataSet
            Dim wtyData() As Class_analysis.WTY_EXCEL
            Call clsSet.setWtyExcelDown(dsWtyExcel, wtyData, userid, userName, shipCode, exportShipName, errFlg, setMon, dateFrom, dateTo)

            If dsWtyExcel Is Nothing Then
                Call showMsg("There is no output target of SalesRegister_OOW.", "")
                Exit Sub
            End If

            If errFlg = 1 Then
                Call showMsg("SalesRegister_OOW information acquisition failed.", "")
                Exit Sub
            End If

            Try
                'ファイル名、パスの設定
                Dim csvFileName As String

                dateFrom = Replace(dateFrom, "/", "")
                dateTo = Replace(dateTo, "/", "")

                'exportFile名の頭、数値を除く
                exportFile = Right(exportFile, exportFile.Length - 2)

                If setMon = "00" Then
                    If dateTo <> "" And dateFrom <> "" Then
                        csvFileName = exportShipName & "_Sales_OOW" & "_" & dateFrom & "_" & dateTo & ".csv"
                    Else
                        If dateTo <> "" Then
                            csvFileName = exportShipName & "_Sales_OOW" & "_" & dateTo & ".csv"
                        End If
                        If dateFrom <> "" Then
                            csvFileName = exportShipName & "_Sales_OOW" & "_" & dateFrom & ".csv"
                        End If
                    End If
                Else
                    '月指定のとき
                    csvFileName = exportShipName & "_Sales_OOW" & "_" & dtNow.ToString("yyyy") & "_" & setMon & ".csv"
                End If

                outputPath = clsSet.CsvFilePass & csvFileName

                '項目名設定
                Dim csvContents = New List(Of String)(New String() {exportShipName & "-Sales Register Out warranty"})
                'Comment on 20190809
                '''               Dim rowWork1 As String = "ASC Code,Branch Code,ASC Claim No,Samsung Claim No,Service Type,Consumer Name,Consumer Addr1,Consumer Addr2,Consumer Telephone,Consumer Fax,Postal Code,Model,Serial No,IMEI No,Defect Type,Condition,Symptom,Defect Code,Repair Code,Defect Desc,Repair Description,Purchase Date,Repair Received Date,Completed Date,Delivery Date,Production Date,Labor Amount Ⅰ,Parts Amount Ⅰ,Tax,Total Invoice Amount Ⅰ,Remark,Tr No,Tr Type,Status,Engineer,Collection Point,Collection Point Name,Location-1,Part-1,Qty-1,Unit Price-1,Doc Num-1,Matrial Serial-1,Location-2,Part-2,Qty-2,Unit Price-2,Doc Num-2,Matrial Serial-2,Location-3,Part-3,Qty-3,Unit Price-3,Doc Num-3,Matrial Serial-3,Location-4,Part-4,Qty-4,Unit Price-4,Doc Num-4,Matrial Serial-4,Location-5,Part-5,Qty-5,Unit Price-5,Doc Num-5,Matrial Serial-5,Location-6,Part-6,Qty-6,Unit Price-6,Doc Num-6,Matrial Serial-6,Location-7,Part-7,Qty-7,Unit Price-7,Doc Num-7,Matrial Serial-7,Location-8,Part-8,Qty-8,Unit Price-8,Doc Num-8,Matrial Serial-8,Location-9,Part-9,Qty-9,Unit Price-9,Doc Num-9,Matrial Serial-9,Location-10,Part-10,Qty-10,Unit Price-10,Doc Num-10,Matrial Serial-10,Location-11,Part-11,Qty-11,Unit Price-11,Doc Num-11,Matrial Serial-11,Location-12,Part-12,Qty-12,Unit Price-12,Doc Num-12,Matrial Serial-12,Location-13,Part-13,Qty-13,Unit Price-13,Doc Num-13,Matrial Serial-13,Location-14,Part-14,Qty-14,Unit Price-14,Doc Num-14,Matrial Serial-14,Location-15,Part-15,Qty-15,Unit Price-15,Doc Num-15,Matrial Serial-15"
                Dim rowWork1 As String = "ASC Code,Branch Code,ASC Claim No,Parts_invoice_No,Labor_Invoice_No,Samsung Claim No,Service Type,Consumer Name,Consumer Addr1,Consumer Addr2,Consumer Telephone,Consumer Fax,Postal Code,Model,Serial No,IMEI No,Defect Type,Condition,Symptom,Defect Code,Repair Code,Defect Desc,Repair Description,Purchase Date,Repair Received Date,Completed Date,Delivery Date,Production Date,Labor Amount Ⅰ,Parts Amount Ⅰ,Freight, Other, Parts_SGST, Parts_UTGST, Parts_CGST, Parts_IGST, Parts_Cess, SGST, UTGST, CGST, IGST, Cess, Total Invoice Amount Ⅰ,Remark,Tr No,Tr Type,Status,Engineer,Collection Point,Collection Point Name,Location-1,Part-1,Qty-1,Unit Price-1,Doc Num-1,Matrial Serial-1,Location-2,Part-2,Qty-2,Unit Price-2,Doc Num-2,Matrial Serial-2,Location-3,Part-3,Qty-3,Unit Price-3,Doc Num-3,Matrial Serial-3,Location-4,Part-4,Qty-4,Unit Price-4,Doc Num-4,Matrial Serial-4,Location-5,Part-5,Qty-5,Unit Price-5,Doc Num-5,Matrial Serial-5,Location-6,Part-6,Qty-6,Unit Price-6,Doc Num-6,Matrial Serial-6,Location-7,Part-7,Qty-7,Unit Price-7,Doc Num-7,Matrial Serial-7,Location-8,Part-8,Qty-8,Unit Price-8,Doc Num-8,Matrial Serial-8,Location-9,Part-9,Qty-9,Unit Price-9,Doc Num-9,Matrial Serial-9,Location-10,Part-10,Qty-10,Unit Price-10,Doc Num-10,Matrial Serial-10,Location-11,Part-11,Qty-11,Unit Price-11,Doc Num-11,Matrial Serial-11,Location-12,Part-12,Qty-12,Unit Price-12,Doc Num-12,Matrial Serial-12,Location-13,Part-13,Qty-13,Unit Price-13,Doc Num-13,Matrial Serial-13,Location-14,Part-14,Qty-14,Unit Price-14,Doc Num-14,Matrial Serial-14,Location-15,Part-15,Qty-15,Unit Price-15,Doc Num-15,Matrial Serial-15"
                csvContents.Add(rowWork1)

                For i = 0 To wtyData.Length - 1
                    csvContents.Add(wtyData(i).ASC_Code & "," & wtyData(i).Branch_Code & "," & wtyData(i).ASC_Claim_No & "," & wtyData(i).Samsung_Claim_No & "," & wtyData(i).Service_Type & "," & wtyData(i).Consumer_Name & "," & wtyData(i).Consumer_Addr1 & "," & wtyData(i).Consumer_Addr2 & "," & wtyData(i).Consumer_Telephone & "," & wtyData(i).Consumer_Fax & "," & wtyData(i).Postal_Code & "," & wtyData(i).Model & "," & wtyData(i).Serial_No & "," & wtyData(i).IMEI_No & "," & wtyData(i).Defect_Type & "," & wtyData(i).Condition & "," & wtyData(i).Symptom & "," & wtyData(i).Defect_Code & "," & wtyData(i).Repair_Code & "," & wtyData(i).Defect_Desc & "," & wtyData(i).Repair_Description & "," & wtyData(i).Purchase_Date & "," & wtyData(i).Repair_Received_Date & "," & wtyData(i).Completed_Date & "," & wtyData(i).Delivery_Date & "," & wtyData(i).Production_Date & "," & wtyData(i).Labor_Amount & "," & wtyData(i).Parts_Amount & "," & wtyData(i).Tax & "," & wtyData(i).Total_Invoice_Amount & "," & wtyData(i).Remark & "," & wtyData(i).Tr_No & "," & wtyData(i).Tr_Type & "," & wtyData(i).Status & "," & wtyData(i).Engineer & "," & wtyData(i).Collection_Point & "," & wtyData(i).Collection_Point_Name & "," & wtyData(i).Location_1 & "," & wtyData(i).Part_1 & "," & wtyData(i).Qty_1 & "," & wtyData(i).Unit_Price_1 & "," & wtyData(i).Doc_Num_1 & "," & wtyData(i).Matrial_Serial_1 & "," & wtyData(i).Location_2 & "," & wtyData(i).Part_2 & "," & wtyData(i).Qty_2 & "," & wtyData(i).Unit_Price_2 & "," & wtyData(i).Doc_Num_2 & "," & wtyData(i).Matrial_Serial_2 & "," & wtyData(i).Location_3 & "," & wtyData(i).Part_3 & "," & wtyData(i).Qty_3 & "," & wtyData(i).Unit_Price_3 & "," & wtyData(i).Doc_Num_3 & "," & wtyData(i).Matrial_Serial_3 & "," & wtyData(i).Location_4 & "," & wtyData(i).Part_4 & "," & wtyData(i).Qty_4 & "," & wtyData(i).Unit_Price_4 & "," & wtyData(i).Doc_Num_4 & "," & wtyData(i).Matrial_Serial_4 & "," & wtyData(i).Location_5 & "," & wtyData(i).Part_5 & "," & wtyData(i).Qty_5 & "," & wtyData(i).Unit_Price_5 & "," & wtyData(i).Doc_Num_5 & "," & wtyData(i).Matrial_Serial_5 & "," & wtyData(i).Location_6 & "," & wtyData(i).Part_6 & "," & wtyData(i).Qty_6 & "," & wtyData(i).Unit_Price_6 & "," & wtyData(i).Doc_Num_6 & "," & wtyData(i).Matrial_Serial_6 & "," & wtyData(i).Location_7 & "," & wtyData(i).Part_7 & "," & wtyData(i).Qty_7 & "," & wtyData(i).Unit_Price_7 & "," & wtyData(i).Doc_Num_7 & "," & wtyData(i).Matrial_Serial_7 & "," & wtyData(i).Location_8 & "," & wtyData(i).Part_8 & "," & wtyData(i).Qty_8 & "," & wtyData(i).Unit_Price_8 & "," & wtyData(i).Doc_Num_8 & "," & wtyData(i).Matrial_Serial_8 & "," & wtyData(i).Location_9 & "," & wtyData(i).Part_9 & "," & wtyData(i).Qty_9 & "," & wtyData(i).Unit_Price_9 & "," & wtyData(i).Doc_Num_9 & "," & wtyData(i).Matrial_Serial_9 & "," & wtyData(i).Location_10 & "," & wtyData(i).Part_10 & "," & wtyData(i).Qty_10 & "," & wtyData(i).Unit_Price_10 & "," & wtyData(i).Doc_Num_10 & "," & wtyData(i).Matrial_Serial_10 & "," & wtyData(i).Location_11 & "," & wtyData(i).Part_11 & "," & wtyData(i).Qty_11 & "," & wtyData(i).Unit_Price_11 & "," & wtyData(i).Doc_Num_11 & "," & wtyData(i).Matrial_Serial_11 & "," & wtyData(i).Location_12 & "," & wtyData(i).Part_12 & "," & wtyData(i).Qty_12 & "," & wtyData(i).Unit_Price_12 & "," & wtyData(i).Doc_Num_12 & "," & wtyData(i).Matrial_Serial_12 & "," & wtyData(i).Location_13 & "," & wtyData(i).Part_13 & "," & wtyData(i).Qty_13 & "," & wtyData(i).Unit_Price_13 & "," & wtyData(i).Doc_Num_13 & "," & wtyData(i).Matrial_Serial_13 & "," & wtyData(i).Location_14 & "," & wtyData(i).Part_14 & "," & wtyData(i).Qty_14 & "," & wtyData(i).Unit_Price_14 & "," & wtyData(i).Doc_Num_14 & "," & wtyData(i).Matrial_Serial_14 & "," & wtyData(i).Location_15 & "," & wtyData(i).Part_15 & "," & wtyData(i).Qty_15 & "," & wtyData(i).Unit_Price_15 & "," & wtyData(i).Doc_Num_15 & "," & wtyData(i).Matrial_Serial_15)
                Next i

                Using writer As New StreamWriter(outputPath, False, Encoding.Default)
                    writer.WriteLine(String.Join(Environment.NewLine, csvContents))
                End Using

                'ファイルの内容をバイト配列にすべて読み込む 
                Dim Buffer As Byte() = System.IO.File.ReadAllBytes(outputPath)

                'サーバに保存されたCSVファイルを削除
                '※Response.End以降、ファイル削除処理ができないため、ファイルのダウンロードではなく、一旦ファイルの内容を
                '上記、Bufferに保存し、ダウンロード処理を行う。

                If outputPath <> "" Then
                    If System.IO.File.Exists(outputPath) = True Then
                        System.IO.File.Delete(outputPath)
                    End If
                End If

                ' ダウンロード
                Response.ContentType = "application/text/csv"
                Response.AddHeader("Content-Disposition", "attachment; filename=" & csvFileName)
                Response.Flush()
                'Response.Write("<b>File Contents: </b>")
                Response.BinaryWrite(Buffer)
                'Response.WriteFile(outputPath)
                Response.End()

            Catch ex As System.Threading.ThreadAbortException
                'Response.End()の呼び出しによりエラーメッセージを出力しないようにする

            Catch ex As Exception
                Call showMsg("CSV output processing failed.", "")
            End Try

        ElseIf kindExport = 3 Then

            'Sales Invoice to samsung C
            '出力対象情報を取得
            Dim invoiceData() As Class_analysis.INVOICE
            Call clsSet.setInvoice(invoiceData, exportShipName, errFlg, setMon, "C")

            If invoiceData Is Nothing Then
                Call showMsg("Sales Invoice to samsung There is no output target of C.", "")
                Exit Sub
            End If

            If errFlg = 1 Then
                Call showMsg("InvoiceUpdate information acquisition failed.", "")
                Exit Sub
            End If

            Try
                'ファイル名、パスの設定

                'exportFile名の頭、数値を除く
                exportFile = Right(exportFile, exportFile.Length - 2)

                Dim csvFileName As String = "Sales_Inwarranty_C_" & exportShipName & "_" & dtNow.ToString("yyyy") & "_" & setMon & ".csv"

                outputPath = clsSet.CsvFilePass & csvFileName

                '項目名設定
                Dim rowWork1 As String = "Samsung Ref No,Your Ref No,Model,Serial,Product,Service,Defect Code,Currency,Invoice,Labor,Parts,Freight,Other,Tax,Parts_Invoice_No,Labpr_Invoice_No,Invoice Date"
                Dim csvContents = New List(Of String)(New String() {rowWork1})

                For i = 0 To invoiceData.Length - 1
                    csvContents.Add(invoiceData(i).samsung_Ref_No & "," & invoiceData(i).Your_Ref_No & "," & invoiceData(i).Model & "," & invoiceData(i).Serial & "," & invoiceData(i).Product & "," & invoiceData(i).Serivice & "," & invoiceData(i).Defect_Code & "," & invoiceData(i).Currency & "," & invoiceData(i).Invoice & "," & invoiceData(i).Labor & "," & invoiceData(i).Parts & "," & invoiceData(i).Felight & "," & invoiceData(i).Other & "," & invoiceData(i).Tax & "," & invoiceData(i).Parts_invoice_No & "," & invoiceData(i).Labor_Invoice_No & "," & invoiceData(i).Invoice_date)
                Next i

                Using writer As New StreamWriter(outputPath, False, Encoding.Default)
                    writer.WriteLine(String.Join(Environment.NewLine, csvContents))
                End Using

                'ファイルの内容をバイト配列にすべて読み込む 
                Dim Buffer As Byte() = System.IO.File.ReadAllBytes(outputPath)

                'サーバに保存されたCSVファイルを削除
                '※Response.End以降、ファイル削除処理ができないため、ファイルのダウンロードではなく、一旦ファイルの内容を
                '上記、Bufferに保存し、ダウンロード処理を行う。

                If outputPath <> "" Then
                    If System.IO.File.Exists(outputPath) = True Then
                        System.IO.File.Delete(outputPath)
                    End If
                End If

                ' ダウンロード
                Response.ContentType = "application/text/csv"
                Response.AddHeader("Content-Disposition", "attachment; filename=" & csvFileName)
                Response.Flush()
                'Response.Write("<b>File Contents: </b>")
                Response.BinaryWrite(Buffer)
                'Response.WriteFile(outputPath)
                Response.End()

            Catch ex As System.Threading.ThreadAbortException
                'Response.End()の呼び出しによりエラーメッセージを出力しないようにする

            Catch ex As Exception
                Call showMsg("CSV output processing failed.", "")
            End Try

        ElseIf kindExport = 4 Then

            'Sales Invoiec to samsung EXC
            '出力対象情報を取得
            Dim invoiceData() As Class_analysis.INVOICE
            Call clsSet.setInvoice(invoiceData, exportShipName, errFlg, setMon, "EXC")

            If invoiceData Is Nothing Then
                Call showMsg("Sales Invoiec to samsung There is no output target of ", "")
                Exit Sub
            End If

            If errFlg = 1 Then
                Call showMsg("InvoiceUpdate information acquisition failed.", "")
                Exit Sub
            End If

            Try
                'ファイル名、パスの設定

                'exportFile名の頭、数値を除く
                exportFile = Right(exportFile, exportFile.Length - 2)

                Dim csvFileName As String = "Sales_Inwarranty_EXC_" & exportShipName & "_" & dtNow.ToString("yyyy") & "_" & setMon & ".csv"
                outputPath = clsSet.CsvFilePass & csvFileName


                '項目名設定
                Dim rowWork1 As String = "Samsung Ref No,Your Ref No,Model,Serial,Product,Service,Defect Code,Currency,Invoice,Labor,Parts,Freight,Other,Tax,Parts_Invoice_No,Labpr_Invoice_No,Invoice Date"
                Dim csvContents = New List(Of String)(New String() {rowWork1})

                For i = 0 To invoiceData.Length - 1
                    csvContents.Add(invoiceData(i).samsung_Ref_No & "," & invoiceData(i).Your_Ref_No & "," & invoiceData(i).Model & "," & invoiceData(i).Serial & "," & invoiceData(i).Product & "," & invoiceData(i).Serivice & "," & invoiceData(i).Defect_Code & "," & invoiceData(i).Currency & "," & invoiceData(i).Invoice & "," & invoiceData(i).Labor & "," & invoiceData(i).Parts & "," & invoiceData(i).Felight & "," & invoiceData(i).Other & "," & invoiceData(i).Tax & "," & invoiceData(i).Parts_invoice_No & "," & invoiceData(i).Labor_Invoice_No & "," & invoiceData(i).Invoice_date)
                Next i

                Using writer As New StreamWriter(outputPath, False, Encoding.Default)
                    writer.WriteLine(String.Join(Environment.NewLine, csvContents))
                End Using

                'ファイルの内容をバイト配列にすべて読み込む 
                Dim Buffer As Byte() = System.IO.File.ReadAllBytes(outputPath)

                'サーバに保存されたCSVファイルを削除
                '※Response.End以降、ファイル削除処理ができないため、ファイルのダウンロードではなく、一旦ファイルの内容を
                '上記、Bufferに保存し、ダウンロード処理を行う。

                If outputPath <> "" Then
                    If System.IO.File.Exists(outputPath) = True Then
                        System.IO.File.Delete(outputPath)
                    End If
                End If

                ' ダウンロード
                Response.ContentType = "application/text/csv"
                Response.AddHeader("Content-Disposition", "attachment; filename=" & csvFileName)
                Response.Flush()
                'Response.Write("<b>File Contents: </b>")
                Response.BinaryWrite(Buffer)
                'Response.WriteFile(outputPath)
                Response.End()

            Catch ex As System.Threading.ThreadAbortException
                'Response.End()の呼び出しによりエラーメッセージを出力しないようにする

            Catch ex As Exception
                Call showMsg("CSV output processing failed.", "")
            End Try

        ElseIf kindExport = 8 Then

            'Purchase_Register
            '出力対象情報を取得
            Dim purchaseData2() As Class_analysis.BILLINGINFODETAIL  '出力用　
            Call clsSet.setPurchaseRegister(purchaseData2, exportShipName, shipCode, errFlg, setMon, dateFrom, dateTo)

            If purchaseData2 Is Nothing Then
                Call showMsg("There is no output target of Purchase_Register.", "")
                Exit Sub
            End If

            If errFlg = 1 Then
                Call showMsg("Failed to get good_recived information.", "")
                Exit Sub
            End If

            Try
                'ファイル名、パスの設定
                Dim csvFileName As String

                dateFrom = Replace(dateFrom, "/", "")
                dateTo = Replace(dateTo, "/", "")

                'exportFile名の頭、数値を除く
                exportFile = Right(exportFile, exportFile.Length - 2)

                If setMon = "00" Then
                    If dateTo <> "" And dateFrom <> "" Then
                        csvFileName = "Purchase_Register_" & exportShipName & "_" & dateFrom & "_" & dateTo & ".csv"
                    Else
                        If dateTo <> "" Then
                            csvFileName = "Purchase_Register_" & exportShipName & "_" & dateTo & ".csv"
                        End If
                        If dateFrom <> "" Then
                            csvFileName = "Purchase_Register_" & exportShipName & "_" & dateFrom & ".csv"
                        End If
                    End If
                Else
                    '月指定のとき
                    csvFileName = "Purchase_Register_" & exportShipName & "_" & dtNow.ToString("yyyy") & "_" & setMon & ".csv"
                End If

                outputPath = clsSet.CsvFilePass & csvFileName

                '項目名設定
                Dim rowWork1 As String = "Ship-to-Branch-Code,Ship-to-Branch,Invoice Date,G/R Date,Invoice No,Local Invoice No,Delivery No,Items,Amount,SGST / UTGST,CGST,IGST,Cess,Tax,Total,GR Status"

                Dim csvContents = New List(Of String)(New String() {rowWork1})

                For i = 0 To purchaseData2.Length - 1
                    csvContents.Add(purchaseData2(i).Branch_Code & "," & purchaseData2(i).Ship_Branch & "," & purchaseData2(i).delivery_date & "," & purchaseData2(i).GR_Date & "," & purchaseData2(i).Invoice_No & "," & purchaseData2(i).local_invoice_No & "," & purchaseData2(i).Delivery_No & "," & purchaseData2(i).Items & "," & purchaseData2(i).SumAmount & "," & purchaseData2(i).SumSGST_UTGST & "," & purchaseData2(i).SumCGST & "," & purchaseData2(i).SumIGST & "," & purchaseData2(i).SumCess & "," & purchaseData2(i).SumTax & "," & purchaseData2(i).SumTotal & "," & purchaseData2(i).GR_Status)
                Next i

                Using writer As New StreamWriter(outputPath, False, Encoding.Default)
                    writer.WriteLine(String.Join(Environment.NewLine, csvContents))
                End Using

                'ファイルの内容をバイト配列にすべて読み込む 
                Dim Buffer As Byte() = System.IO.File.ReadAllBytes(outputPath)

                'サーバに保存されたCSVファイルを削除
                '※Response.End以降、ファイル削除処理ができないため、ファイルのダウンロードではなく、一旦ファイルの内容を
                '上記、Bufferに保存し、ダウンロード処理を行う。

                If outputPath <> "" Then
                    If System.IO.File.Exists(outputPath) = True Then
                        System.IO.File.Delete(outputPath)
                    End If
                End If

                ' ダウンロード
                Response.ContentType = "application/text/csv"
                Response.AddHeader("Content-Disposition", "attachment; filename=" & csvFileName)
                Response.Flush()
                'Response.Write("<b>File Contents: </b>")
                Response.BinaryWrite(Buffer)
                'Response.WriteFile(outputPath)
                Response.End()

            Catch ex As System.Threading.ThreadAbortException
                'Response.End()の呼び出しによりエラーメッセージを出力しないようにする

            Catch ex As Exception
                Call showMsg("CSV output processing failed.", "")
            End Try

        ElseIf kindExport = 5 Then

            'SalesRegister_IW
            '出力対象情報を取得
            Dim wtyDataIW() As Class_analysis.WTY_EXCEL

            Call clsSet.setSalesRegister_IW_OTHER(wtyDataIW, exportShipName, shipCode, userid, userName, errFlg, setMon, dateFrom, dateTo, "IW")

            If wtyDataIW Is Nothing Then
                Call showMsg("There is no output target of SalesRegister_IW.", "")
                Exit Sub
            End If

            If errFlg = 1 Then
                Call showMsg("Failed to get information on SalesRegister_IW.", "")
                Exit Sub
            End If

            Try
                'ファイル名、パスの設定
                Dim csvFileName As String

                dateFrom = Replace(dateFrom, "/", "")
                dateTo = Replace(dateTo, "/", "")

                'exportFile名の頭、数値を除く
                exportFile = Right(exportFile, exportFile.Length - 2)

                If setMon = "00" Then
                    If dateTo <> "" And dateFrom <> "" Then
                        csvFileName = "Sales_Register_GSPIN_IW_" & exportShipName & "_" & dateFrom & "_" & dateTo & ".csv"
                    Else
                        If dateTo <> "" Then
                            csvFileName = "Sales_Register_GSPIN_IW_" & exportShipName & "_" & dateTo & ".csv"
                        End If
                        If dateFrom <> "" Then
                            csvFileName = "Sales_Register_GSPIN_IW_" & exportShipName & "_" & dateFrom & ".csv"
                        End If
                    End If
                Else
                    '月指定のとき
                    csvFileName = "Sales_Register_GSPIN_IW_" & exportShipName & "_" & dtNow.ToString("yyyy") & "_" & setMon & ".csv"
                End If

                outputPath = clsSet.CsvFilePass & csvFileName

                '項目名設定
                Dim rowWork1 As String = "ASC Code,Branch Code,ASC Claim No,Part Invoice No,Labour Invoice No,Samsung Claim No,Service Type,Consumer Name,Consumer Addr1,Consumer Addr2,Consumer Telephone,Consumer Fax,Postal Code,Model,Serial No,IMEI No,Defect Type,Condition,Symptom,Defect Code,Repair Code,Defect Desc,Repair Description,Purchase Date,Repair Received Date,Completed Date,Delivery Date,Production Date,Labor Amount Ⅰ,Parts Amount Ⅰ,Total Invoice Amount Ⅰ,Freight,Other,Parts SGST,Parts UTGST,Parts CGST,Parts IGST,Parts Cess,SGST,UTGST,CGST,IGST,Cess,Remark,Tr No,Tr Type,Status,Engineer,Collection Point,Collection Point Name,Location-1,Part-1,Qty-1,Unit Price-1,Doc Num-1,Matrial Serial-1,Location-2,Part-2,Qty-2,Unit Price-2,Doc Num-2,Matrial Serial-2,Location-3,Part-3,Qty-3,Unit Price-3,Doc Num-3,Matrial Serial-3,Location-4,Part-4,Qty-4,Unit Price-4,Doc Num-4,Matrial Serial-4,Location-5,Part-5,Qty-5,Unit Price-5,Doc Num-5,Matrial Serial-5,Location-6,Part-6,Qty-6,Unit Price-6,Doc Num-6,Matrial Serial-6,Location-7,Part-7,Qty-7,Unit Price-7,Doc Num-7,Matrial Serial-7,Location-8,Part-8,Qty-8,Unit Price-8,Doc Num-8,Matrial Serial-8,Location-9,Part-9,Qty-9,Unit Price-9,Doc Num-9,Matrial Serial-9,Location-10,Part-10,Qty-10,Unit Price-10,Doc Num-10,Matrial Serial-10,Location-11,Part-11,Qty-11,Unit Price-11,Doc Num-11,Matrial Serial-11,Location-12,Part-12,Qty-12,Unit Price-12,Doc Num-12,Matrial Serial-12,Location-13,Part-13,Qty-13,Unit Price-13,Doc Num-13,Matrial Serial-13,Location-14,Part-14,Qty-14,Unit Price-14,Doc Num-14,Matrial Serial-14,Location-15,Part-15,Qty-15,Unit Price-15,Doc Num-15,Matrial Serial-15"

                Dim csvContents = New List(Of String)(New String() {rowWork1})

                For i = 0 To wtyDataIW.Length - 1
                    csvContents.Add(wtyDataIW(i).ASC_Code & "," & wtyDataIW(i).Branch_Code & "," & wtyDataIW(i).ASC_Claim_No & "," & wtyDataIW(i).Parts_invoice_No & "," & wtyDataIW(i).Labor_Invoice_No & "," & wtyDataIW(i).Samsung_Claim_No & "," & wtyDataIW(i).Service_Type & "," & wtyDataIW(i).Consumer_Name & "," & wtyDataIW(i).Consumer_Addr1 & "," & wtyDataIW(i).Consumer_Addr2 & "," & wtyDataIW(i).Consumer_Telephone & "," & wtyDataIW(i).Consumer_Fax & "," & wtyDataIW(i).Postal_Code & "," & wtyDataIW(i).Model & "," & wtyDataIW(i).Serial_No & "," & wtyDataIW(i).IMEI_No & "," & wtyDataIW(i).Defect_Type & "," & wtyDataIW(i).Condition & "," & wtyDataIW(i).Symptom & "," & wtyDataIW(i).Defect_Code & "," & wtyDataIW(i).Repair_Code & "," & wtyDataIW(i).Defect_Desc & "," & wtyDataIW(i).Repair_Description & "," & wtyDataIW(i).Purchase_Date & "," & wtyDataIW(i).Repair_Received_Date & "," & wtyDataIW(i).Completed_Date & "," & wtyDataIW(i).Delivery_Date & "," & wtyDataIW(i).Production_Date & "," & wtyDataIW(i).Labor & "," & wtyDataIW(i).Parts & "," & wtyDataIW(i).Invoice & "," & wtyDataIW(i).Freight & "," & wtyDataIW(i).Other & "," & wtyDataIW(i).Parts_SGST & "," & wtyDataIW(i).Parts_UTGST & "," & wtyDataIW(i).Parts_CGST & "," & wtyDataIW(i).Parts_IGST & "," & wtyDataIW(i).Parts_Cess & "," & wtyDataIW(i).SGST & "," & wtyDataIW(i).UTGST & "," & wtyDataIW(i).CGST & "," & wtyDataIW(i).IGST & "," & wtyDataIW(i).Cess & "," & wtyDataIW(i).Remark & "," & wtyDataIW(i).Tr_No & "," & wtyDataIW(i).Tr_Type & "," & wtyDataIW(i).Status & "," & wtyDataIW(i).Engineer & "," & wtyDataIW(i).Collection_Point & "," & wtyDataIW(i).Collection_Point_Name & "," & wtyDataIW(i).Location_1 & "," & wtyDataIW(i).Part_1 & "," & wtyDataIW(i).Qty_1 & "," & wtyDataIW(i).Unit_Price_1 & "," & wtyDataIW(i).Doc_Num_1 & "," & wtyDataIW(i).Matrial_Serial_1 & "," & wtyDataIW(i).Location_2 & "," & wtyDataIW(i).Part_2 & "," & wtyDataIW(i).Qty_2 & "," & wtyDataIW(i).Unit_Price_2 & "," & wtyDataIW(i).Doc_Num_2 & "," & wtyDataIW(i).Matrial_Serial_2 & "," & wtyDataIW(i).Location_3 & "," & wtyDataIW(i).Part_3 & "," & wtyDataIW(i).Qty_3 & "," & wtyDataIW(i).Unit_Price_3 & "," & wtyDataIW(i).Doc_Num_3 & "," & wtyDataIW(i).Matrial_Serial_3 & "," & wtyDataIW(i).Location_4 & "," & wtyDataIW(i).Part_4 & "," & wtyDataIW(i).Qty_4 & "," & wtyDataIW(i).Unit_Price_4 & "," & wtyDataIW(i).Doc_Num_4 & "," & wtyDataIW(i).Matrial_Serial_4 & "," & wtyDataIW(i).Location_5 & "," & wtyDataIW(i).Part_5 & "," & wtyDataIW(i).Qty_5 & "," & wtyDataIW(i).Unit_Price_5 & "," & wtyDataIW(i).Doc_Num_5 & "," & wtyDataIW(i).Matrial_Serial_5 & "," & wtyDataIW(i).Location_6 & "," & wtyDataIW(i).Part_6 & "," & wtyDataIW(i).Qty_6 & "," & wtyDataIW(i).Unit_Price_6 & "," & wtyDataIW(i).Doc_Num_6 & "," & wtyDataIW(i).Matrial_Serial_6 & "," & wtyDataIW(i).Location_7 & "," & wtyDataIW(i).Part_7 & "," & wtyDataIW(i).Qty_7 & "," & wtyDataIW(i).Unit_Price_7 & "," & wtyDataIW(i).Doc_Num_7 & "," & wtyDataIW(i).Matrial_Serial_7 & "," & wtyDataIW(i).Location_8 & "," & wtyDataIW(i).Part_8 & "," & wtyDataIW(i).Qty_8 & "," & wtyDataIW(i).Unit_Price_8 & "," & wtyDataIW(i).Doc_Num_8 & "," & wtyDataIW(i).Matrial_Serial_8 & "," & wtyDataIW(i).Location_9 & "," & wtyDataIW(i).Part_9 & "," & wtyDataIW(i).Qty_9 & "," & wtyDataIW(i).Unit_Price_9 & "," & wtyDataIW(i).Doc_Num_9 & "," & wtyDataIW(i).Matrial_Serial_9 & "," & wtyDataIW(i).Location_10 & "," & wtyDataIW(i).Part_10 & "," & wtyDataIW(i).Qty_10 & "," & wtyDataIW(i).Unit_Price_10 & "," & wtyDataIW(i).Doc_Num_10 & "," & wtyDataIW(i).Matrial_Serial_10 & "," & wtyDataIW(i).Location_11 & "," & wtyDataIW(i).Part_11 & "," & wtyDataIW(i).Qty_11 & "," & wtyDataIW(i).Unit_Price_11 & "," & wtyDataIW(i).Doc_Num_11 & "," & wtyDataIW(i).Matrial_Serial_11 & "," & wtyDataIW(i).Location_12 & "," & wtyDataIW(i).Part_12 & "," & wtyDataIW(i).Qty_12 & "," & wtyDataIW(i).Unit_Price_12 & "," & wtyDataIW(i).Doc_Num_12 & "," & wtyDataIW(i).Matrial_Serial_12 & "," & wtyDataIW(i).Location_13 & "," & wtyDataIW(i).Part_13 & "," & wtyDataIW(i).Qty_13 & "," & wtyDataIW(i).Unit_Price_13 & "," & wtyDataIW(i).Doc_Num_13 & "," & wtyDataIW(i).Matrial_Serial_13 & "," & wtyDataIW(i).Location_14 & "," & wtyDataIW(i).Part_14 & "," & wtyDataIW(i).Qty_14 & "," & wtyDataIW(i).Unit_Price_14 & "," & wtyDataIW(i).Doc_Num_14 & "," & wtyDataIW(i).Matrial_Serial_14 & "," & wtyDataIW(i).Location_15 & "," & wtyDataIW(i).Part_15 & "," & wtyDataIW(i).Qty_15 & "," & wtyDataIW(i).Unit_Price_15 & "," & wtyDataIW(i).Doc_Num_15 & "," & wtyDataIW(i).Matrial_Serial_15)
                Next i

                Using writer As New StreamWriter(outputPath, False, Encoding.Default)
                    writer.WriteLine(String.Join(Environment.NewLine, csvContents))
                End Using

                'ファイルの内容をバイト配列にすべて読み込む 
                Dim Buffer As Byte() = System.IO.File.ReadAllBytes(outputPath)

                'サーバに保存されたCSVファイルを削除
                '※Response.End以降、ファイル削除処理ができないため、ファイルのダウンロードではなく、一旦ファイルの内容を
                '上記、Bufferに保存し、ダウンロード処理を行う。

                If outputPath <> "" Then
                    If System.IO.File.Exists(outputPath) = True Then
                        System.IO.File.Delete(outputPath)
                    End If
                End If

                ' ダウンロード
                Response.ContentType = "application/text/csv"
                Response.AddHeader("Content-Disposition", "attachment; filename=" & csvFileName)
                Response.Flush()
                'Response.Write("<b>File Contents: </b>")
                Response.BinaryWrite(Buffer)
                'Response.WriteFile(outputPath)
                Response.End()

            Catch ex As System.Threading.ThreadAbortException
                'Response.End()の呼び出しによりエラーメッセージを出力しないようにする

            Catch ex As Exception
                Call showMsg("CSV出力処理に失敗しました。", "")
            End Try

        ElseIf kindExport = 6 Then

            'Other_Sales_GSPIN
            '出力対象情報を取得
            Dim wtyDataOther() As Class_analysis.WTY_EXCEL

            Call clsSet.setSalesRegister_IW_OTHER(wtyDataOther, exportShipName, shipCode, userid, userName, errFlg, setMon, dateFrom, dateTo, "OTHER")

            If wtyDataOther Is Nothing Then
                Call showMsg("There is no output target of Other_Sales_GSPIN.", "")
                Exit Sub
            End If

            If errFlg = 1 Then
                Call showMsg("Failed to get information on Other_Sales_GSPIN.", "")
                Exit Sub
            End If

            Try
                'ファイル名、パスの設定
                Dim csvFileName As String

                dateFrom = Replace(dateFrom, "/", "")
                dateTo = Replace(dateTo, "/", "")

                'exportFile名の頭、数値を除く
                exportFile = Right(exportFile, exportFile.Length - 2)

                If setMon = "00" Then
                    If dateTo <> "" And dateFrom <> "" Then
                        csvFileName = "Other_Sales_GSPIN_" & exportShipName & "_" & dateFrom & "_" & dateTo & ".csv"
                    Else
                        If dateTo <> "" Then
                            csvFileName = "Other_Sales_GSPIN_" & exportShipName & "_" & dateTo & ".csv"
                        End If
                        If dateFrom <> "" Then
                            csvFileName = "Other_Sales_GSPIN_" & exportShipName & "_" & dateFrom & ".csv"
                        End If
                    End If
                Else
                    '月指定のとき
                    csvFileName = "Other_Sales_GSPIN_" & exportShipName & "_" & dtNow.ToString("yyyy") & "_" & setMon & ".csv"
                End If

                outputPath = clsSet.CsvFilePass & csvFileName

                '項目名設定
                Dim rowWork1 As String = "ASC Code,Branch Code,ASC Claim No,Part Invoice No,Labour Invoice No,Samsung Claim No,Service Type,Consumer Name,Consumer Addr1,Consumer Addr2,Consumer Telephone,Consumer Fax,Postal Code,Model,Serial No,IMEI No,Defect Type,Condition,Symptom,Defect Code,Repair Code,Defect Desc,Repair Description,Purchase Date,Repair Received Date,Completed Date,Delivery Date,Production Date,Labor Amount Ⅰ,Parts Amount Ⅰ,Total Invoice Amount Ⅰ,Remark,Tr No,Tr Type,Status,Engineer,Collection Point,Collection Point Name,Location-1,Part-1,Qty-1,Unit Price-1,Doc Num-1,Matrial Serial-1,Location-2,Part-2,Qty-2,Unit Price-2,Doc Num-2,Matrial Serial-2,Location-3,Part-3,Qty-3,Unit Price-3,Doc Num-3,Matrial Serial-3,Location-4,Part-4,Qty-4,Unit Price-4,Doc Num-4,Matrial Serial-4,Location-5,Part-5,Qty-5,Unit Price-5,Doc Num-5,Matrial Serial-5,Location-6,Part-6,Qty-6,Unit Price-6,Doc Num-6,Matrial Serial-6,Location-7,Part-7,Qty-7,Unit Price-7,Doc Num-7,Matrial Serial-7,Location-8,Part-8,Qty-8,Unit Price-8,Doc Num-8,Matrial Serial-8,Location-9,Part-9,Qty-9,Unit Price-9,Doc Num-9,Matrial Serial-9,Location-10,Part-10,Qty-10,Unit Price-10,Doc Num-10,Matrial Serial-10,Location-11,Part-11,Qty-11,Unit Price-11,Doc Num-11,Matrial Serial-11,Location-12,Part-12,Qty-12,Unit Price-12,Doc Num-12,Matrial Serial-12,Location-13,Part-13,Qty-13,Unit Price-13,Doc Num-13,Matrial Serial-13,Location-14,Part-14,Qty-14,Unit Price-14,Doc Num-14,Matrial Serial-14,Location-15,Part-15,Qty-15,Unit Price-15,Doc Num-15,Matrial Serial-15"

                Dim csvContents = New List(Of String)(New String() {rowWork1})

                For i = 0 To wtyDataOther.Length - 1
                    csvContents.Add(wtyDataOther(i).ASC_Code & "," & wtyDataOther(i).Branch_Code & "," & wtyDataOther(i).ASC_Claim_No & "," & wtyDataOther(i).Parts_invoice_No & "," & wtyDataOther(i).Labor_Invoice_No & "," & wtyDataOther(i).Samsung_Claim_No & "," & wtyDataOther(i).Service_Type & "," & wtyDataOther(i).Consumer_Name & "," & wtyDataOther(i).Consumer_Addr1 & "," & wtyDataOther(i).Consumer_Addr2 & "," & wtyDataOther(i).Consumer_Telephone & "," & wtyDataOther(i).Consumer_Fax & "," & wtyDataOther(i).Postal_Code & "," & wtyDataOther(i).Model & "," & wtyDataOther(i).Serial_No & "," & wtyDataOther(i).IMEI_No & "," & wtyDataOther(i).Defect_Type & "," & wtyDataOther(i).Condition & "," & wtyDataOther(i).Symptom & "," & wtyDataOther(i).Defect_Code & "," & wtyDataOther(i).Repair_Code & "," & wtyDataOther(i).Defect_Desc & "," & wtyDataOther(i).Repair_Description & "," & wtyDataOther(i).Purchase_Date & "," & wtyDataOther(i).Repair_Received_Date & "," & wtyDataOther(i).Completed_Date & "," & wtyDataOther(i).Delivery_Date & "," & wtyDataOther(i).Production_Date & "," & wtyDataOther(i).Labor & "," & wtyDataOther(i).Parts & "," & wtyDataOther(i).Invoice & "," & wtyDataOther(i).Remark & "," & wtyDataOther(i).Tr_No & "," & wtyDataOther(i).Tr_Type & "," & wtyDataOther(i).Status & "," & wtyDataOther(i).Engineer & "," & wtyDataOther(i).Collection_Point & "," & wtyDataOther(i).Collection_Point_Name & "," & wtyDataOther(i).Location_1 & "," & wtyDataOther(i).Part_1 & "," & wtyDataOther(i).Qty_1 & "," & wtyDataOther(i).Unit_Price_1 & "," & wtyDataOther(i).Doc_Num_1 & "," & wtyDataOther(i).Matrial_Serial_1 & "," & wtyDataOther(i).Location_2 & "," & wtyDataOther(i).Part_2 & "," & wtyDataOther(i).Qty_2 & "," & wtyDataOther(i).Unit_Price_2 & "," & wtyDataOther(i).Doc_Num_2 & "," & wtyDataOther(i).Matrial_Serial_2 & "," & wtyDataOther(i).Location_3 & "," & wtyDataOther(i).Part_3 & "," & wtyDataOther(i).Qty_3 & "," & wtyDataOther(i).Unit_Price_3 & "," & wtyDataOther(i).Doc_Num_3 & "," & wtyDataOther(i).Matrial_Serial_3 & "," & wtyDataOther(i).Location_4 & "," & wtyDataOther(i).Part_4 & "," & wtyDataOther(i).Qty_4 & "," & wtyDataOther(i).Unit_Price_4 & "," & wtyDataOther(i).Doc_Num_4 & "," & wtyDataOther(i).Matrial_Serial_4 & "," & wtyDataOther(i).Location_5 & "," & wtyDataOther(i).Part_5 & "," & wtyDataOther(i).Qty_5 & "," & wtyDataOther(i).Unit_Price_5 & "," & wtyDataOther(i).Doc_Num_5 & "," & wtyDataOther(i).Matrial_Serial_5 & "," & wtyDataOther(i).Location_6 & "," & wtyDataOther(i).Part_6 & "," & wtyDataOther(i).Qty_6 & "," & wtyDataOther(i).Unit_Price_6 & "," & wtyDataOther(i).Doc_Num_6 & "," & wtyDataOther(i).Matrial_Serial_6 & "," & wtyDataOther(i).Location_7 & "," & wtyDataOther(i).Part_7 & "," & wtyDataOther(i).Qty_7 & "," & wtyDataOther(i).Unit_Price_7 & "," & wtyDataOther(i).Doc_Num_7 & "," & wtyDataOther(i).Matrial_Serial_7 & "," & wtyDataOther(i).Location_8 & "," & wtyDataOther(i).Part_8 & "," & wtyDataOther(i).Qty_8 & "," & wtyDataOther(i).Unit_Price_8 & "," & wtyDataOther(i).Doc_Num_8 & "," & wtyDataOther(i).Matrial_Serial_8 & "," & wtyDataOther(i).Location_9 & "," & wtyDataOther(i).Part_9 & "," & wtyDataOther(i).Qty_9 & "," & wtyDataOther(i).Unit_Price_9 & "," & wtyDataOther(i).Doc_Num_9 & "," & wtyDataOther(i).Matrial_Serial_9 & "," & wtyDataOther(i).Location_10 & "," & wtyDataOther(i).Part_10 & "," & wtyDataOther(i).Qty_10 & "," & wtyDataOther(i).Unit_Price_10 & "," & wtyDataOther(i).Doc_Num_10 & "," & wtyDataOther(i).Matrial_Serial_10 & "," & wtyDataOther(i).Location_11 & "," & wtyDataOther(i).Part_11 & "," & wtyDataOther(i).Qty_11 & "," & wtyDataOther(i).Unit_Price_11 & "," & wtyDataOther(i).Doc_Num_11 & "," & wtyDataOther(i).Matrial_Serial_11 & "," & wtyDataOther(i).Location_12 & "," & wtyDataOther(i).Part_12 & "," & wtyDataOther(i).Qty_12 & "," & wtyDataOther(i).Unit_Price_12 & "," & wtyDataOther(i).Doc_Num_12 & "," & wtyDataOther(i).Matrial_Serial_12 & "," & wtyDataOther(i).Location_13 & "," & wtyDataOther(i).Part_13 & "," & wtyDataOther(i).Qty_13 & "," & wtyDataOther(i).Unit_Price_13 & "," & wtyDataOther(i).Doc_Num_13 & "," & wtyDataOther(i).Matrial_Serial_13 & "," & wtyDataOther(i).Location_14 & "," & wtyDataOther(i).Part_14 & "," & wtyDataOther(i).Qty_14 & "," & wtyDataOther(i).Unit_Price_14 & "," & wtyDataOther(i).Doc_Num_14 & "," & wtyDataOther(i).Matrial_Serial_14 & "," & wtyDataOther(i).Location_15 & "," & wtyDataOther(i).Part_15 & "," & wtyDataOther(i).Qty_15 & "," & wtyDataOther(i).Unit_Price_15 & "," & wtyDataOther(i).Doc_Num_15 & "," & wtyDataOther(i).Matrial_Serial_15)
                Next i

                Using writer As New StreamWriter(outputPath, False, Encoding.Default)
                    writer.WriteLine(String.Join(Environment.NewLine, csvContents))
                End Using

                'ファイルの内容をバイト配列にすべて読み込む 
                Dim Buffer As Byte() = System.IO.File.ReadAllBytes(outputPath)

                'サーバに保存されたCSVファイルを削除
                '※Response.End以降、ファイル削除処理ができないため、ファイルのダウンロードではなく、一旦ファイルの内容を
                '上記、Bufferに保存し、ダウンロード処理を行う。

                If outputPath <> "" Then
                    If System.IO.File.Exists(outputPath) = True Then
                        System.IO.File.Delete(outputPath)
                    End If
                End If

                ' ダウンロード
                Response.ContentType = "application/text/csv"
                Response.AddHeader("Content-Disposition", "attachment; filename=" & csvFileName)
                Response.Flush()
                'Response.Write("<b>File Contents: </b>")
                Response.BinaryWrite(Buffer)
                'Response.WriteFile(outputPath)
                Response.End()

            Catch ex As System.Threading.ThreadAbortException
                'Response.End()の呼び出しによりエラーメッセージを出力しないようにする

            Catch ex As Exception
                Call showMsg("CSV output processing failed.", "")
            End Try

        ElseIf kindExport = 7 Then

            'SAW Discount Details
            '出力対象情報を取得
            Dim wtyDataOther() As Class_analysis.WTY_EXCEL

            Call clsSet.setSAW_Discount_Details(wtyDataOther, exportShipName, shipCode, userid, userName, errFlg, setMon, dateFrom, dateTo)

            If wtyDataOther Is Nothing Then
                Call showMsg("No SAW Discount Details output target.", "")
                Exit Sub
            End If

            If errFlg = 1 Then
                Call showMsg("SAW Discount Details information acquisition failed.", "")
                Exit Sub
            End If

            Try
                'ファイル名、パスの設定
                Dim csvFileName As String

                dateFrom = Replace(dateFrom, "/", "")
                dateTo = Replace(dateTo, "/", "")

                'exportFile名の頭、数値を除く
                exportFile = Right(exportFile, exportFile.Length - 2)

                If setMon = "00" Then
                    If dateTo <> "" And dateFrom <> "" Then
                        csvFileName = "SAW_Discount_Details_" & exportShipName & "_" & dateFrom & "_" & dateTo & ".csv"
                    Else
                        If dateTo <> "" Then
                            csvFileName = "SAW_Discount_Details_" & exportShipName & "_" & dateTo & ".csv"
                        End If
                        If dateFrom <> "" Then
                            csvFileName = "SAW_Discount_Details_" & exportShipName & "_" & dateFrom & ".csv"
                        End If
                    End If
                Else
                    '月指定のとき
                    csvFileName = "SAW_Discount_Details_" & exportShipName & "_" & dtNow.ToString("yyyy") & "_" & setMon & ".csv"
                End If

                outputPath = clsSet.CsvFilePass & csvFileName

                '項目名設定
                Dim rowWork1 As String = "Branch Code,ASC Claim No,Model Name,Part Amount (Retail Price) with out tax,SAW Discount Parts Amount,SAW Discount Labor Amount,Invoice Amount collected from Cus,Part Invoice No,Labour Invoice No,SAW Remarks"

                Dim csvContents = New List(Of String)(New String() {rowWork1})

                For i = 0 To wtyDataOther.Length - 1
                    csvContents.Add(exportShipName & "," & wtyDataOther(i).ASC_Claim_No & "," & wtyDataOther(i).Model & ",,,,," & wtyDataOther(i).Parts_invoice_No & "," & wtyDataOther(i).Labor_Invoice_No & ",")
                Next i

                Using writer As New StreamWriter(outputPath, False, Encoding.Default)
                    writer.WriteLine(String.Join(Environment.NewLine, csvContents))
                End Using

                'ファイルの内容をバイト配列にすべて読み込む 
                Dim Buffer As Byte() = System.IO.File.ReadAllBytes(outputPath)

                'サーバに保存されたCSVファイルを削除
                '※Response.End以降、ファイル削除処理ができないため、ファイルのダウンロードではなく、一旦ファイルの内容を
                '上記、Bufferに保存し、ダウンロード処理を行う。

                If outputPath <> "" Then
                    If System.IO.File.Exists(outputPath) = True Then
                        System.IO.File.Delete(outputPath)
                    End If
                End If

                ' ダウンロード
                Response.ContentType = "application/text/csv"
                Response.AddHeader("Content-Disposition", "attachment; filename=" & csvFileName)
                Response.Flush()
                'Response.Write("<b>File Contents: </b>")
                Response.BinaryWrite(Buffer)
                'Response.WriteFile(outputPath)
                Response.End()

            Catch ex As System.Threading.ThreadAbortException
                'Response.End()の呼び出しによりエラーメッセージを出力しないようにする

            Catch ex As Exception
                Call showMsg("CSV output processing failed.", "")
            End Try

        ElseIf kindExport = 9 Then

            'Final_Report
            '***InvoiceNo_Final最終番号取得***
            Dim InvoiceMax As String = ""
            Dim InvoiceNoMax As Long
            Dim strSQL4 As String = "SELECT MAX(RIGHT(InvoiceNo_Final,7)) AS InvoiceNo_Final_Max FROM dbo.Wty_Excel "
            strSQL4 &= "WHERE DELFG = 0 AND Branch_Code = '" & shipCode & "';"
            Dim DT_WtyExcel As DataTable
            DT_WtyExcel = DBCommon.ExecuteGetDT(strSQL4, errFlg)

            If errFlg = 1 Then
                Call showMsg("Failed to acquire po information.", "")
                Exit Sub
            End If

            If DT_WtyExcel IsNot Nothing Then
                If DT_WtyExcel.Rows(0)("InvoiceNo_Final_Max") IsNot DBNull.Value Then
                    InvoiceMax = DT_WtyExcel.Rows(0)("InvoiceNo_Final_Max")
                    InvoiceNoMax = Convert.ToInt64(InvoiceMax)
                    InvoiceNoMax = InvoiceNoMax + 1
                End If
            End If

            '***出力対象取得***
            Dim dsWtyExcel As New DataSet
            Dim wtyData() As Class_analysis.WTY_EXCEL

            Call clsSet.setFinalReport(dsWtyExcel, wtyData, userid, userName, shipCode, exportShipName, errFlg, setMon, dateFrom, dateTo, InvoiceNoMax)

            If dsWtyExcel Is Nothing Then
                Call showMsg("There is no output target of Final_Report.", "")
                Exit Sub
            End If

            If errFlg = 1 Then
                Call showMsg("Failed to get information on Final_Report.", "")
                Exit Sub
            End If

            Try
                '***ファイル名、パスの設定***
                Dim csvFileName As String

                dateFrom = Replace(dateFrom, "/", "")
                dateTo = Replace(dateTo, "/", "")

                'exportFile名の頭、数値を除く
                exportFile = Right(exportFile, exportFile.Length - 2)

                If setMon = "00" Then
                    If dateTo <> "" And dateFrom <> "" Then
                        csvFileName = exportShipName & "_Final_Report" & "_" & dateFrom & "_" & dateTo & ".csv"
                    Else
                        If dateTo <> "" Then
                            csvFileName = exportShipName & "_Final_Report" & "_" & dateTo & ".csv"
                        End If
                        If dateFrom <> "" Then
                            csvFileName = exportShipName & "_Final_Report" & "_" & dateFrom & ".csv"
                        End If
                    End If
                Else
                    '月指定のとき
                    csvFileName = exportShipName & "_Final_Report" & "_" & dtNow.ToString("yyyy") & "_" & setMon & ".csv"
                End If

                outputPath = clsSet.CsvFilePass & csvFileName

                '***項目名設定***
                Dim csvContents = New List(Of String)(New String() {exportShipName & "-Final_Report"})
                Dim rowWork1 As String = "ASC Claim No,Invocie Number,Delivery Date,Payment Method,Part-1,Qty-1,Unit Price-1,Sum of Labor Amount,Sum of Parts Amount,SGST Payable,CGST Payable,IGST Payable,Sum of Tax Amount,Sum of Total Invoice Amount"

                csvContents.Add(rowWork1)

                '***部品情報設定***
                For i = 0 To wtyData.Length - 1

                    ReDim Preserve wtyData(i).partsInfo(15)
                    wtyData(i).partsInfo(0).PartsName = wtyData(i).Part_1
                    wtyData(i).partsInfo(0).Qty = wtyData(i).Qty_1
                    wtyData(i).partsInfo(0).UnitPrice = wtyData(i).Unit_Price_1

                    wtyData(i).partsInfo(1).PartsName = wtyData(i).Part_2
                    wtyData(i).partsInfo(1).Qty = wtyData(i).Qty_2
                    wtyData(i).partsInfo(1).UnitPrice = wtyData(i).Unit_Price_2

                    wtyData(i).partsInfo(2).PartsName = wtyData(i).Part_3
                    wtyData(i).partsInfo(2).Qty = wtyData(i).Qty_3
                    wtyData(i).partsInfo(2).UnitPrice = wtyData(i).Unit_Price_3

                    wtyData(i).partsInfo(3).PartsName = wtyData(i).Part_4
                    wtyData(i).partsInfo(3).Qty = wtyData(i).Qty_4
                    wtyData(i).partsInfo(3).UnitPrice = wtyData(i).Unit_Price_4

                    wtyData(i).partsInfo(4).PartsName = wtyData(i).Part_5
                    wtyData(i).partsInfo(4).Qty = wtyData(i).Qty_5
                    wtyData(i).partsInfo(4).UnitPrice = wtyData(i).Unit_Price_5

                    '部品種類の個数を取得(部品5までは、6以降を確認しない)
                    If wtyData(i).Part_6 = "" Then

                        If wtyData(i).Part_1 = "" Then
                            wtyData(i).partsCount = 0
                        Else
                            If wtyData(i).Part_2 = "" Then
                                wtyData(i).partsCount = 1
                            Else
                                If wtyData(i).Part_3 = "" Then
                                    wtyData(i).partsCount = 2
                                Else
                                    If wtyData(i).Part_4 = "" Then
                                        wtyData(i).partsCount = 3
                                    Else
                                        If wtyData(i).Part_5 = "" Then
                                            wtyData(i).partsCount = 4
                                        Else
                                            If wtyData(i).Part_6 = "" Then
                                                wtyData(i).partsCount = 5
                                            End If
                                        End If
                                    End If
                                End If
                            End If
                        End If
                    Else

                        wtyData(i).partsInfo(5).PartsName = wtyData(i).Part_6
                        wtyData(i).partsInfo(5).Qty = wtyData(i).Qty_6
                        wtyData(i).partsInfo(5).UnitPrice = wtyData(i).Unit_Price_6

                        wtyData(i).partsInfo(6).PartsName = wtyData(i).Part_7
                        wtyData(i).partsInfo(6).Qty = wtyData(i).Qty_7
                        wtyData(i).partsInfo(6).UnitPrice = wtyData(i).Unit_Price_7

                        wtyData(i).partsInfo(7).PartsName = wtyData(i).Part_8
                        wtyData(i).partsInfo(7).Qty = wtyData(i).Qty_8
                        wtyData(i).partsInfo(7).UnitPrice = wtyData(i).Unit_Price_8

                        wtyData(i).partsInfo(8).PartsName = wtyData(i).Part_9
                        wtyData(i).partsInfo(8).Qty = wtyData(i).Qty_9
                        wtyData(i).partsInfo(8).UnitPrice = wtyData(i).Unit_Price_9

                        wtyData(i).partsInfo(9).PartsName = wtyData(i).Part_10
                        wtyData(i).partsInfo(9).Qty = wtyData(i).Qty_10
                        wtyData(i).partsInfo(9).UnitPrice = wtyData(i).Unit_Price_10

                        wtyData(i).partsInfo(10).PartsName = wtyData(i).Part_11
                        wtyData(i).partsInfo(10).Qty = wtyData(i).Qty_11
                        wtyData(i).partsInfo(10).UnitPrice = wtyData(i).Unit_Price_11

                        wtyData(i).partsInfo(11).PartsName = wtyData(i).Part_12
                        wtyData(i).partsInfo(11).Qty = wtyData(i).Qty_12
                        wtyData(i).partsInfo(11).UnitPrice = wtyData(i).Unit_Price_12

                        wtyData(i).partsInfo(12).PartsName = wtyData(i).Part_13
                        wtyData(i).partsInfo(12).Qty = wtyData(i).Qty_13
                        wtyData(i).partsInfo(12).UnitPrice = wtyData(i).Unit_Price_13

                        wtyData(i).partsInfo(13).PartsName = wtyData(i).Part_14
                        wtyData(i).partsInfo(13).Qty = wtyData(i).Qty_14
                        wtyData(i).partsInfo(13).UnitPrice = wtyData(i).Unit_Price_14

                        wtyData(i).partsInfo(14).PartsName = wtyData(i).Part_15
                        wtyData(i).partsInfo(14).Qty = wtyData(i).Qty_15
                        wtyData(i).partsInfo(14).UnitPrice = wtyData(i).Unit_Price_15

                        '部品6以降
                        If wtyData(i).Part_7 = "" Then
                            wtyData(i).partsCount = 6
                        Else
                            If wtyData(i).Part_8 = "" Then
                                wtyData(i).partsCount = 7
                            Else
                                If wtyData(i).Part_9 = "" Then
                                    wtyData(i).partsCount = 8
                                Else
                                    If wtyData(i).Part_10 = "" Then
                                        wtyData(i).partsCount = 9
                                    Else
                                        If wtyData(i).Part_11 = "" Then
                                            wtyData(i).partsCount = 10
                                        Else
                                            If wtyData(i).Part_12 = "" Then
                                                wtyData(i).partsCount = 11
                                            Else
                                                If wtyData(i).Part_13 = "" Then
                                                    wtyData(i).partsCount = 12
                                                Else
                                                    If wtyData(i).Part_14 = "" Then
                                                        wtyData(i).partsCount = 13
                                                    Else
                                                        If wtyData(i).Part_15 = "" Then
                                                            wtyData(i).partsCount = 14
                                                        Else
                                                            wtyData(i).partsCount = 15
                                                        End If
                                                    End If
                                                End If
                                            End If
                                        End If
                                    End If
                                End If
                            End If
                        End If
                    End If
                Next i

                '***CSV出力***
                For i = 0 To wtyData.Length - 1

                    '合計
                    'wtyData(i).Sum_Total_Invoice_Amount = wtyData(i).Labor_Amount + wtyData(i).Parts_Amount + wtyData(i).Sum_Tax_Amount

                    'InvoiceNo_Final 000の表記外す
                    Dim tmp() As String = Split(wtyData(i).InvoiceNo_Final, "-")
                    Dim tmp2 As Integer

                    If tmp.Length = 2 Then
                        tmp2 = tmp(1)
                        wtyData(i).InvoiceNo_Final = tmp(0) & "-" & tmp2.ToString
                    End If

                    '最初の１行目、parts1と合計情報
                    If wtyData(i).partsCount <> 0 Then
                        'csvContents.Add(wtyData(i).ASC_Claim_No & "," & wtyData(i).InvoiceNo_Final & "," & wtyData(i).Delivery_Date & "," & wtyData(i).Part_1 & "," & wtyData(i).Qty_1 & "," & wtyData(i).Unit_Price_1 & "," & wtyData(i).Labor_Amount & "," & wtyData(i).Parts_Amount & "," & wtyData(i).SGST_Payable & "," & wtyData(i).CGST_Payable & "," & wtyData(i).IGST_Payable & "," & wtyData(i).Sum_Tax_Amount & "," & wtyData(i).Sum_Total_Invoice_Amount)
                        csvContents.Add(wtyData(i).ASC_Claim_No & "," & wtyData(i).InvoiceNo_Final & "," & wtyData(i).Delivery_Date & "," & wtyData(i).payment & "," & wtyData(i).Part_1 & "," & wtyData(i).Qty_1 & "," & wtyData(i).Unit_Price_1 & "," & wtyData(i).OW_Labor & "," & wtyData(i).OW_Parts & "," & wtyData(i).SGST_Payable & "," & wtyData(i).CGST_Payable & "," & wtyData(i).IGST_Payable & "," & wtyData(i).Sum_Tax_Amount & "," & wtyData(i).OW_total)
                    End If

                    If wtyData(i).partsCount = 1 Then
                        'parts情報最後の行に、作業費を設定
                        csvContents.Add(wtyData(i).ASC_Claim_No & "," & wtyData(i).InvoiceNo_Final & "," & wtyData(i).Delivery_Date & "," & wtyData(i).payment & ",Labour Charges,," & wtyData(i).OW_Labor)
                    Else
                        For j = 1 To wtyData(i).partsCount
                            '各行に部品毎情報を設定
                            If j = wtyData(i).partsCount Then
                                csvContents.Add(wtyData(i).ASC_Claim_No & "," & wtyData(i).InvoiceNo_Final & "," & wtyData(i).Delivery_Date & "," & wtyData(i).payment & ",Labour Charges,," & wtyData(i).OW_Labor)
                            Else
                                csvContents.Add(wtyData(i).ASC_Claim_No & "," & wtyData(i).InvoiceNo_Final & "," & wtyData(i).Delivery_Date & "," & wtyData(i).payment & "," & wtyData(i).partsInfo(j).PartsName & "," & wtyData(i).partsInfo(j).Qty & "," & wtyData(i).partsInfo(j).UnitPrice)
                            End If
                        Next j
                    End If

                Next i

                Using writer As New StreamWriter(outputPath, False, Encoding.Default)
                    writer.WriteLine(String.Join(Environment.NewLine, csvContents))
                End Using

                'ファイルの内容をバイト配列にすべて読み込む 
                Dim Buffer As Byte() = System.IO.File.ReadAllBytes(outputPath)

                'サーバに保存されたCSVファイルを削除
                '※Response.End以降、ファイル削除処理ができないため、ファイルのダウンロードではなく、一旦ファイルの内容を
                '上記、Bufferに保存し、ダウンロード処理を行う。

                If outputPath <> "" Then
                    If System.IO.File.Exists(outputPath) = True Then
                        System.IO.File.Delete(outputPath)
                    End If
                End If

                ' ダウンロード
                Response.ContentType = "application/text/csv"
                Response.AddHeader("Content-Disposition", "attachment; filename=" & csvFileName)
                Response.Flush()
                'Response.Write("<b>File Contents: </b>")
                Response.BinaryWrite(Buffer)
                'Response.WriteFile(outputPath)
                Response.End()

            Catch ex As System.Threading.ThreadAbortException
                'Response.End()の呼び出しによりエラーメッセージを出力しないようにする

            Catch ex As Exception
                If outputPath <> "" Then
                    If System.IO.File.Exists(outputPath) = True Then
                        System.IO.File.Delete(outputPath)
                    End If
                End If
                Call showMsg("CSV output processing failed.", "")
            End Try

        End If
    End Sub
    Protected Sub showMsg(ByVal Msg As String, ByVal msgChk As String)

        lblMsg.Text = Msg
        Dim sScript As String

        If msgChk = "CancelMsg" Then
            'OKとキャンセルボタン
            sScript = "$(function () {$(""#dialog"" ).dialog({width: 400,buttons:{""OK"": function () {$(this).dialog('close');$('[id$=""BtnOK""]').click();},""CANCEL"": function () {$(this).dialog('close');$('[id$=""BtnCancel""]').click();}}});});"
        Else
            'OKボタンのみ
            sScript = "$(function () { $( ""#dialog"" ).dialog({width: 400, buttons: {""OK"": function () {$(this).dialog('close');}}});});"
        End If

        'JavaScriptの埋め込み
        ClientScript.RegisterStartupScript(Me.GetType(), "startup", sScript, True)

    End Sub

    ''' <summary>
    ''' Loading All branches
    ''' </summary>
    Private Sub InitDropDownList()
        Log4NetControl.ComInfoLogWrite(Session("UserName"))
        Dim userName As String = Session("user_id") 'Session("user_Name")
        'Clear the branch location
        DropListLocation.Items.Clear()
        'For store the branch codes in array
        Dim shipCodeAll() As String
        'Verify entered user and password correct or not with the database
        Dim _UserInfoModel As UserInfoModel = New UserInfoModel()
        Dim _UserInfoControl As UserInfoControl = New UserInfoControl()
        _UserInfoModel.UserId = userName
        '_UserInfoModel.Password = TextPass.Text.ToString().Trim()
        Dim UserInfoList As List(Of UserInfoModel) = _UserInfoControl.SelectUserInfo(_UserInfoModel)
        'User Doesn't exist
        If UserInfoList Is Nothing OrElse UserInfoList.Count = 0 Then
            Call showMsg("The username / password incorrect. Please try again", "")
            Exit Sub
        End If
        'Loading All Branch Codes and stored in a array for the session
        Dim _ShipBaseControl As ShipBaseControl = New ShipBaseControl()
        Dim dt As DataTable = _ShipBaseControl.SelectBranchCode()
        ReDim shipCodeAll(dt.Rows.Count - 1)
        Dim i As Integer = 0
        For Each dr As DataRow In dt.Rows
            If dr("ship_code") IsNot DBNull.Value Then
                shipCodeAll(i) = dr("ship_code")
            End If
            i = i + 1
        Next
        Log4NetControl.ComInfoLogWrite(Session("UserName"))
        Dim codeMasterControl As CodeMasterControl = New CodeMasterControl()
        'QryFlag 
        'QryFlag 1 - # Specific Filter
        'QryFlag 2 - # All records
        Dim QryFlag As Integer = 1 'Specific Records
        If (UserInfoList(0).UserLevel = CommonConst.UserLevel0) Or
                        (UserInfoList(0).UserLevel = CommonConst.UserLevel1) Or
                        (UserInfoList(0).UserLevel = CommonConst.UserLevel2) Or
                (UserInfoList(0).AdminFlg = True) Then
            QryFlag = 2
        End If
        Dim codeMasterList As List(Of CodeMasterModel) = codeMasterControl.SelectBranchMaster(QryFlag, "'" & UserInfoList(0).Ship1.Replace(",", "','") & "'")

        ' Dim codeMasterControl As CodeMasterControl = New CodeMasterControl()
        'Loading branch name and code in the dropdown list
        '  Dim codeMasterList As List(Of CodeMasterModel) = codeMasterControl.SelectBranchMaster()
        Dim codeMaster1 As CodeMasterModel = New CodeMasterModel()
        codeMaster1.CodeValue = "Select Branch"
        codeMaster1.CodeDispValue = "Select Branch"
        codeMasterList.Insert(0, codeMaster1)
        Dim codeMaster2 As CodeMasterModel = New CodeMasterModel()
        '''''codeMaster2.CodeValue = "ALL"
        '''''codeMaster2.CodeDispValue = "ALL"
        '''''codeMasterList.Insert(1, codeMaster2)

        Me.DropListLocation.DataSource = codeMasterList
        Me.DropListLocation.DataTextField = "CodeDispValue"
        Me.DropListLocation.DataValueField = "CodeValue"
        Me.DropListLocation.DataBind()
    End Sub

    Protected Sub btnExport_Click(sender As Object, e As EventArgs) Handles btnExport.Click

        'Session("Exportinputdtl") = exfiledtl

        Dim dtScDsrView As DataTable = exfiledtl.ExportAnalysisDetails(Session("Exportinputdtl"), "E")

        Dim name(dtScDsrView.Columns.Count) As String
        Dim i As Integer = 0
        For Each column As DataColumn In dtScDsrView.Columns
            If column.ColumnName.EndsWith("_Orig") = True Then
                name(i) = column.ColumnName
                i += 1
                'dtScDsrView.Columns.Remove(name(i))
            End If

        Next

        For Each ColName As String In name
            If ColName IsNot Nothing Then
                dtScDsrView.Columns.Contains(ColName)
                dtScDsrView.Columns.Remove(ColName)
            End If
        Next
        'dtScDsrView.Columns.Remove("".EndsWith("_Orig"))
        ExportCSV(CType(Session("Exportinputdtl"), Export_File_Details).FileName, dtScDsrView)
        'Response.ContentType = "text/csv"

    End Sub


    Protected Sub txtPageSize_TextChanged(sender As Object, e As EventArgs) Handles txtPageSize.TextChanged

        Dim intValue As Integer
        If Integer.TryParse(txtPageSize.Text, intValue) AndAlso intValue > 0 AndAlso intValue <= 9999 Then
            lblErrorMessage.Visible = False

            Dim dt As DataTable = CType(exfiledtl.ExportAnalysisDetails(Session("Exportinputdtl"), "R"), DataTable)
            Dim dv As DataView = New DataView(dt)
            If ViewState("SortExpression") Is Nothing Then
                dv.Sort = gvExportReport.Columns(0).SortExpression & " " & Me.SortDirection
            Else
                dv.Sort = ViewState("SortExpression") & " " & Me.SortDirection
            End If
            ' dv.Sort = "ServiceOrder_No " & Me.SortDirection
            txtPageSize.Text = Convert.ToInt16(txtPageSize.Text)
            gvExportReport.PageSize = Convert.ToInt16(txtPageSize.Text)
            gvExportReport.PageIndex = 0
            gvExportReport.DataSource = dv
            gvExportReport.DataBind()
            'MessageBox.Show("Thank You, your rating was " & txtPageSize.Text)
        Else
            lblErrorMessage.Visible = True
            txtPageSize.Text = gvExportReport.PageSize

            'MessageBox.Show("Please Enter a Number from 1 to 10")
        End If

        'lblPagesize.Visible = True

        'txtPageSize.Visible = True

    End Sub



    'Comment on 20190904
    'Backup 
    'Private Sub OldFunctionality()
    '    '***セッション情報取得***
    '    Dim userid As String = Session("user_id")
    '    Dim userName As String = Session("user_Name")
    '    Dim userLevel As String = Session("user_level")
    '    Dim adminFlg As Boolean = Session("admin_Flg")

    '    Dim setMon As String = Session("set_Mon2")
    '    Dim setMonName As String = Session("set_MonName")
    '    Dim exportFile As String = Session("export_File")
    '    Dim exportShipName As String = DropListLocation.SelectedItem.Text 'Session("export_shipName")

    '    Dim clsSetCommon As New Class_common
    '    Dim dtNow As DateTime = clsSetCommon.dtIndia

    '    If userid Is Nothing Then
    '        Call showMsg("The session has expired. Please login again.", "")
    '        Exit Sub
    '    End If

    '    Dim i, j As Integer

    '    '***入力チェック***
    '    If exportShipName = "Select Branch" Then
    '        Call showMsg("Please specify the branch name.", "")
    '        Exit Sub
    '    End If

    '    Dim dt As DateTime
    '    Dim dateFrom As String
    '    Dim dateTo As String
    '    Dim outputPath As String

    '    If TextDateFrom.Text <> "" Then
    '        If exportFile = "Sales Invoice to samsung ""C""" Or exportFile = "Sales Invoiec to samsung ""EXC""" Then
    '            Call showMsg("You can select only the month specification.", "")
    '            Exit Sub
    '        End If
    '        If DateTime.TryParse(TextDateFrom.Text, dt) Then
    '            dateFrom = DateTime.Parse(Trim(TextDateFrom.Text)).ToShortDateString
    '        Else
    '            Call showMsg("There is an error in the date specification", "")
    '            Exit Sub
    '        End If
    '    Else
    '        dateFrom = ""
    '    End If

    '    If TextDateTo.Text <> "" Then
    '        If exportFile = "Sales Invoice to samsung ""C""" Or exportFile = "Sales Invoiec to samsung ""EXC""" Then
    '            Call showMsg("You can select only the month specification.", "")
    '            Exit Sub
    '        End If
    '        If DateTime.TryParse(TextDateTo.Text, dt) Then
    '            dateTo = DateTime.Parse(Trim(TextDateTo.Text)).ToShortDateString
    '        Else
    '            Call showMsg("The date specification of To is incorrect.", "")
    '            Exit Sub
    '        End If
    '    Else
    '        dateTo = ""
    '    End If

    '    If dateFrom = "" And dateTo = "" And setMon = "00" Then
    '        Call showMsg("Please specify either output period by month or date", "")
    '        Exit Sub
    '    End If

    '    If dateFrom <> "" And dateTo <> "" And setMon <> "00" Then
    '        Call showMsg("Please specify either output period by month or date.", "")
    '        Exit Sub
    '    End If

    '    If setMon <> "00" Then
    '        If dateFrom <> "" Or dateTo <> "" Then
    '            Call showMsg("Please specify either output period by month or date.", "")
    '            Exit Sub
    '        End If
    '    End If

    '    If exportFile = "Select Export Filename" Then
    '        Call showMsg("Please specify the file type to be exported.", "")
    '        Exit Sub
    '    End If

    '    '***CSV出力処理***
    '    Dim clsSet As New Class_analysis
    '    Dim errFlg As Integer

    '    '■拠点コード取得
    '    Dim shipCode As String
    '    Dim errMsg As String

    '    Call clsSetCommon.setShipCode(exportShipName, shipCode, errMsg)

    '    If errMsg <> "" Then
    '        Call showMsg(errMsg, "")
    '        Exit Sub
    '    End If

    '    'CSVファイルの種類
    '    Dim kindExport As Integer = Left(exportFile, 1)

    '    ''Matching old Records
    '    ''<asp:ListItem Text = "0.PL_Tracking_Sheet" Value="0"></asp: ListItem>=> kindExport = 1
    '    '<asp:ListItem Text = "1.DailyRepairStatement" Value="1"></asp: ListItem>  --- ?? No functionality as of now?
    '    ''<asp:ListItem Text="2.Warranty Excel File" Value="2"></asp:ListItem>     --- ??  No functionality as of now
    '    ''<asp:ListItem Text = "3.Sales Register from GSPN IW" Value="3"></asp: ListItem>=> kindExport = 5
    '    ''<asp:ListItem Text = "4.Sales Register from GSPN OOW" Value="4"></asp: ListItem>=> kindExport = 2
    '    ''<asp:ListItem Text = "18.Sales Register from GSPN OOW" Value="4"></asp: ListItem>=> kindExport = 2

    '    If kindExport = "0" Then
    '        kindExport = "1"
    '    ElseIf kindExport = "1" Then
    '        'No funtionality as of now
    '        kindExport = -99
    '    ElseIf kindExport = "3" Then
    '        kindExport = "5"
    '    ElseIf kindExport = "4" Then
    '        kindExport = "2"
    '    ElseIf kindExport = "17" Then
    '        kindExport = "9"
    '    End If

    '    '■PL_Tracking_Sheet出力
    '    If kindExport = 1 Then
    '        Dim TmpRate As Decimal = 0.00
    '        If Decimal.TryParse(txtRate.Text, TmpRate) Then
    '        Else
    '            Call showMsg("The rate is not valid ", "")
    '            Exit Sub
    '        End If


    '        Try
    '            '***内容設定***
    '            '■Activity_reportデータ取得
    '            Dim activeData() As Class_analysis.ACTIVITY_REPORT
    '            Dim dsActivity_report As New DataSet
    '            Dim strSQL2 As String = ""

    '            If setMon <> "00" Then
    '                '月指定
    '                strSQL2 &= "SELECT (CONVERT(DATETIME, month + '/' + day)) as day ,  DATENAME(WEEKDAY,(CONVERT(DATETIME, month + '/' + day)) ) as youbi, "
    '                strSQL2 &= "Customer_Visit, Call_Registerd, Repair_Completed,Goods_Delivered, Pending_Calls, Cancelled_Calls "
    '                strSQL2 &= "FROM dbo.Activity_report WHERE Month = '" & Left(dtNow.ToShortDateString, 5) & setMon & "' AND location = '" & shipCode & "' "
    '                strSQL2 &= "ORDER BY day;"
    '            Else
    '                '日付指定
    '                strSQL2 &= "SELECT (CONVERT(DATETIME, month + '/' + day)) as day ,  DATENAME(WEEKDAY,(CONVERT(DATETIME, month + '/' + day)) ) as youbi, "
    '                strSQL2 &= "Customer_Visit, Call_Registerd, Repair_Completed,Goods_Delivered, Pending_Calls, Cancelled_Calls "
    '                strSQL2 &= "FROM dbo.Activity_report WHERE location = '" & shipCode & "' "

    '                If dateTo <> "" Then
    '                    If dateFrom <> "" Then
    '                        strSQL2 &= "AND (CONVERT(DATETIME, month + '/' + day)) <= CONVERT(DATETIME, '" & dateTo & "') "
    '                        strSQL2 &= "AND (CONVERT(DATETIME, month + '/' + day)) >= CONVERT(DATETIME, '" & dateFrom & "') "
    '                    Else
    '                        strSQL2 &= "AND (CONVERT(DATETIME, month + '/' + day)) <= CONVERT(DATETIME, '" & dateTo & "') "
    '                    End If
    '                Else
    '                    If dateFrom <> "" Then
    '                        strSQL2 &= "AND (CONVERT(DATETIME, month + '/' + day)) >= CONVERT(DATETIME, '" & dateFrom & "') "
    '                    End If
    '                End If

    '                strSQL2 &= "ORDER BY day;"

    '            End If

    '            dsActivity_report = DBCommon.Get_DS(strSQL2, errFlg)

    '            If errFlg = 1 Then
    '                Call showMsg("Failed to get Activity_report information.", "")
    '                Exit Sub
    '            End If

    '            If dsActivity_report IsNot Nothing Then

    '                If dsActivity_report.Tables(0).Rows.Count <> 0 Then

    '                    ReDim activeData(dsActivity_report.Tables(0).Rows.Count - 1)

    '                    Dim dr As DataRow

    '                    For i = 0 To dsActivity_report.Tables(0).Rows.Count - 1

    '                        dr = dsActivity_report.Tables(0).Rows(i)

    '                        Dim tmpDay As DateTime
    '                        Dim tmpMon As String
    '                        Dim tmpYear As String

    '                        If dr("day") IsNot DBNull.Value Then

    '                            '項目の日付をセット　例）2018/07/01は、01-Jul-2018　
    '                            tmpDay = dr("day")

    '                            'yyyy/mm/dd
    '                            activeData(i).day2 = tmpDay.ToShortDateString

    '                            'yyyy/mm/dd
    '                            activeData(i).day = tmpDay.ToShortDateString

    '                            'dd
    '                            activeData(i).day = activeData(i).day.Substring(8, 2)

    '                            'yyyy
    '                            tmpYear = activeData(i).day2.Substring(0, 4)

    '                            If setMonName <> "Select the month" Then

    '                                '月指定は、ドロップリストで指定された月をセット
    '                                'dd-mon-yyyy
    '                                activeData(i).day &= "-" & Left(setMonName, 3) & "-" & tmpYear
    '                            Else

    '                                '日付指定は、月を３文字列に変換してセット
    '                                tmpMon = activeData(i).day2.Substring(5, 2)
    '                                Select Case Convert.ToInt32(tmpMon)
    '                                    Case 1
    '                                        activeData(i).day &= "-" & "Jan" & "-" & tmpYear
    '                                    Case 2
    '                                        activeData(i).day &= "-" & "Feb" & "-" & tmpYear
    '                                    Case 3
    '                                        activeData(i).day &= "-" & "Mar" & "-" & tmpYear
    '                                    Case 4
    '                                        activeData(i).day &= "-" & "Apr" & "-" & tmpYear
    '                                    Case 5
    '                                        activeData(i).day &= "-" & "May" & "-" & tmpYear
    '                                    Case 6
    '                                        activeData(i).day &= "-" & "Jun" & "-" & tmpYear
    '                                    Case 7
    '                                        activeData(i).day &= "-" & "Jul" & "-" & tmpYear
    '                                    Case 8
    '                                        activeData(i).day &= "-" & "Aug" & "-" & tmpYear
    '                                    Case 9
    '                                        activeData(i).day &= "-" & "Sep" & "-" & tmpYear
    '                                    Case 10
    '                                        activeData(i).day &= "-" & "Oct" & "-" & tmpYear
    '                                    Case 11
    '                                        activeData(i).day &= "-" & "Nov" & "-" & tmpYear
    '                                    Case 12
    '                                        activeData(i).day &= "-" & "Dec" & "-" & tmpYear
    '                                End Select
    '                            End If

    '                        End If

    '                        If dr("youbi") IsNot DBNull.Value Then
    '                            activeData(i).youbi = dr("youbi")
    '                        End If

    '                        If dr("Customer_Visit") IsNot DBNull.Value Then
    '                            activeData(i).Customer_Visit = dr("Customer_Visit")
    '                        End If

    '                        If dr("Call_Registerd") IsNot DBNull.Value Then
    '                            activeData(i).Call_Registerd = dr("Call_Registerd")
    '                        End If

    '                        If dr("Repair_Completed") IsNot DBNull.Value Then
    '                            activeData(i).Repair_Completed = dr("Repair_Completed")
    '                        End If

    '                        If dr("Goods_Delivered") IsNot DBNull.Value Then
    '                            activeData(i).Goods_Delivered = dr("Goods_Delivered")
    '                        End If

    '                        If dr("Pending_Calls") IsNot DBNull.Value Then
    '                            activeData(i).Pending_Calls = dr("Pending_Calls")
    '                        End If

    '                    Next i

    '                End If

    '            Else
    '                Call showMsg("There is no corresponding Activity_report information.", "")
    '                Exit Sub
    '            End If

    '            '***出力順にセット***
    '            '■Activity_reportデータ出力
    '            'Modified by Mohan . 2019 07 09 - Request sent by Shimada san

    '            '項目名設定
    '            Dim csvContents = New List(Of String)(New String() {"Activity_repot," & txtRate.Text & "%"})
    '            Dim rowWork1 As String = exportShipName & ","
    '            Dim rowWork2 As String = " ,"
    '            Dim rowWork3 As String = "Customer Visit①,"
    '            Dim rowWork4 As String = "Call Registered②,"
    '            Dim rowWork5 As String = "Repair Completed③,"
    '            Dim rowWork6 As String = "Goods Delivered④,"
    '            Dim rowWork7 As String = "Pending⑤,"

    '            Dim Total_Customer_Visit As Integer
    '            Dim Average_Customer_Visit As Integer
    '            Dim Total_Call_Registerd As Integer
    '            Dim Average_Call_Registerd As Integer
    '            Dim Total_Repair_Completed As Integer
    '            Dim Average_Repair_Completed As Integer
    '            Dim Total_Goods_Delivered As Integer
    '            Dim Average_Goods_Delivered As Integer
    '            Dim Total_Pending_Calls As Integer
    '            Dim Average_Pending_Calls As Integer


    '            '日付項目分処理
    '            For i = 0 To dsActivity_report.Tables(0).Rows.Count - 1
    '                rowWork1 &= activeData(i).day & ","
    '                rowWork2 &= activeData(i).youbi & ","
    '                rowWork3 &= activeData(i).Customer_Visit & ","
    '                rowWork4 &= activeData(i).Call_Registerd & ","
    '                rowWork5 &= activeData(i).Repair_Completed & ","
    '                rowWork6 &= activeData(i).Goods_Delivered & ","
    '                rowWork7 &= activeData(i).Pending_Calls & ","
    '                Total_Customer_Visit = Total_Customer_Visit + activeData(i).Customer_Visit
    '                Total_Call_Registerd = Total_Call_Registerd + activeData(i).Call_Registerd
    '                Total_Repair_Completed = Total_Repair_Completed + activeData(i).Repair_Completed
    '                Total_Goods_Delivered = Total_Goods_Delivered + activeData(i).Goods_Delivered
    '                Total_Pending_Calls = Total_Pending_Calls + activeData(i).Pending_Calls
    '            Next i

    '            Average_Customer_Visit = Total_Customer_Visit / dsActivity_report.Tables(0).Rows.Count
    '            Average_Call_Registerd = Total_Call_Registerd / dsActivity_report.Tables(0).Rows.Count
    '            Average_Repair_Completed = Total_Repair_Completed / dsActivity_report.Tables(0).Rows.Count
    '            Average_Goods_Delivered = Total_Goods_Delivered / dsActivity_report.Tables(0).Rows.Count
    '            Average_Pending_Calls = Total_Pending_Calls / dsActivity_report.Tables(0).Rows.Count

    '            '項目
    '            rowWork1 &= "Total,Average"

    '            '曜日
    '            rowWork2 &= " , "
    '            rowWork3 &= Total_Customer_Visit & "," & Average_Customer_Visit
    '            rowWork4 &= Total_Call_Registerd & "," & Average_Call_Registerd
    '            rowWork5 &= Total_Repair_Completed & "," & Average_Repair_Completed
    '            rowWork6 &= Total_Goods_Delivered & "," & Average_Goods_Delivered
    '            rowWork7 &= Total_Pending_Calls & "," & Average_Pending_Calls

    '            csvContents.Add(rowWork1)
    '            csvContents.Add(rowWork2)
    '            csvContents.Add(rowWork3)
    '            csvContents.Add(rowWork4)
    '            csvContents.Add(rowWork5)
    '            csvContents.Add(rowWork6)
    '            csvContents.Add(rowWork7)

    '            '■SC_DSR_infoデータ取得
    '            Dim totalDailyStatementRepartData() As Class_analysis.DAILYSTATEMENTREPART
    '            Dim dsSC_DSR_info As New DataSet
    '            Dim strSQL3 As String = ""

    '            If setMon <> "00" Then
    '                '月指定
    '                strSQL3 &= "SELECT * FROM dbo.SC_DSR_info "
    '                strSQL3 &= "WHERE DELFG = 0 And Branch_name = '" & exportShipName & "' AND LEFT(CONVERT(VARCHAR, Billing_date,111),7) = '" & Left(dtNow.ToShortDateString, 5) & setMon & "';"
    '            Else
    '                '日付指定
    '                strSQL3 &= "SELECT * FROM dbo.SC_DSR_info "
    '                strSQL3 &= "WHERE DELFG = 0 And Branch_name = '" & exportShipName & "' "

    '                If dateTo <> "" Then
    '                    If dateFrom <> "" Then
    '                        strSQL3 &= "AND LEFT(CONVERT(VARCHAR, Billing_date, 111), 10) <= '" & dateTo & "' "
    '                        strSQL3 &= "AND LEFT(CONVERT(VARCHAR, Billing_date, 111), 10) >= '" & dateFrom & "';"
    '                    Else
    '                        strSQL3 &= "AND LEFT(CONVERT(VARCHAR, Billing_date, 111), 10) <= '" & dateTo & "';"
    '                    End If
    '                Else
    '                    If dateFrom <> "" Then
    '                        strSQL3 &= "AND LEFT(CONVERT(VARCHAR, Billing_date, 111), 10) >= '" & dateFrom & "';"
    '                    End If
    '                End If

    '            End If

    '            dsSC_DSR_info = DBCommon.Get_DS(strSQL3, errFlg)

    '            If errFlg = 1 Then
    '                Call showMsg("Failed to get information on SC_DSR_info.", "")
    '                Exit Sub
    '            End If

    '            If dsSC_DSR_info IsNot Nothing Then

    '                If dsSC_DSR_info.Tables(0).Rows.Count <> 0 Then

    '                    ReDim totalDailyStatementRepartData(dsSC_DSR_info.Tables(0).Rows.Count - 1)

    '                    Dim dr As DataRow

    '                    For i = 0 To dsSC_DSR_info.Tables(0).Rows.Count - 1

    '                        dr = dsSC_DSR_info.Tables(0).Rows(i)

    '                        Dim tmpDay As DateTime

    '                        If dr("Billing_date") IsNot DBNull.Value Then
    '                            tmpDay = dr("Billing_date")
    '                            totalDailyStatementRepartData(i).Billing_date = tmpDay.ToShortDateString
    '                        End If

    '                        '①
    '                        If dr("IW_goods_total") IsNot DBNull.Value Then
    '                            totalDailyStatementRepartData(i).IW_goods_total = dr("IW_goods_total")
    '                        End If

    '                        '②
    '                        If dr("OW_goods_total") IsNot DBNull.Value Then
    '                            totalDailyStatementRepartData(i).OW_goods_total = dr("OW_goods_total")
    '                        End If

    '                        '③
    '                        If dr("IW_Labor_total") IsNot DBNull.Value Then
    '                            totalDailyStatementRepartData(i).IW_Labor = dr("IW_Labor_total")
    '                        End If

    '                        '④
    '                        If dr("IW_Parts_total") IsNot DBNull.Value Then
    '                            totalDailyStatementRepartData(i).IW_Parts = dr("IW_Parts_total")
    '                        End If

    '                        '⑤
    '                        If dr("IW_Tax_total") IsNot DBNull.Value Then
    '                            totalDailyStatementRepartData(i).IW_Tax = dr("IW_Tax_total")
    '                        End If

    '                        '⑥
    '                        If dr("IW_Total_total") IsNot DBNull.Value Then
    '                            totalDailyStatementRepartData(i).IW_total = dr("IW_Total_total")
    '                        End If

    '                        '⑦
    '                        If dr("OW_Labor_total") IsNot DBNull.Value Then
    '                            totalDailyStatementRepartData(i).OW_Labor = dr("OW_Labor_total")
    '                        End If

    '                        '⑧
    '                        If dr("OW_Parts_total") IsNot DBNull.Value Then
    '                            totalDailyStatementRepartData(i).OW_Parts = dr("OW_Parts_total")
    '                        End If

    '                        '⑨
    '                        If dr("OW_Tax_total") IsNot DBNull.Value Then
    '                            totalDailyStatementRepartData(i).OW_Tax = dr("OW_Tax_total")
    '                        End If

    '                        '⑩
    '                        If dr("OW_Total_total") IsNot DBNull.Value Then
    '                            totalDailyStatementRepartData(i).OW_total = dr("OW_Total_total")
    '                        End If

    '                    Next i

    '                End If

    '                '■dailystatementrepartデータ出力
    '                '項目名設定
    '                Dim rowWork8 As String = "DailyStatementRepart,"
    '                Dim rowWork9 As String = "Warranty (Number),"        '①+②
    '                Dim rowWork10 As String = "In Warranty (Number),"   '①
    '                Dim rowWork11 As String = "Out Warranty (Number),"   '②
    '                Dim rowWork12 As String = "In Warranty (Amount),"    '③+④　
    '                Dim rowWork13 As String = "In Warranty (Labour),"     '③
    '                Dim rowWork14 As String = "In Warranty (Parts),"      '④
    '                Dim rowWork15 As String = "Tax In Warranty,"     '⑤
    '                Dim rowWork16 As String = "Total Amount In Warranty," '⑥=③+④+⑤
    '                Dim rowWork17 As String = "Out Warranty (Amount),"    '⑦+⑧
    '                Dim rowWork18 As String = "Out Warranty (Labour),"    '⑦
    '                Dim rowWork19 As String = "Out Warranty (Parts),"     '⑧
    '                Dim rowWork20 As String = "Tax Out Warranty,"         '⑨
    '                Dim rowWork21 As String = "Total Amount Out Warranty," '⑩=⑦+⑧+⑨
    '                Dim rowWork22 As String = "Revenue without Tax,"   '③+④+⑦+⑧
    '                Dim rowWork23 As String = "Cost of Revenue (Asumption),"
    '                Dim rowWork24 As String = "Gross Profit,"
    '                'Added 20190717
    '                Dim rowWork25 As String = "Gross Profit 2,"


    '                'count集計用
    '                Dim Total_IW_OW_goods_total As Integer
    '                Dim Average_IW_OW_goods_total As Integer
    '                Dim Total_IW_goods_total As Integer
    '                Dim Total_OW_goods_total As Integer
    '                Dim Average_IW_goods_total As Integer
    '                Dim Average_OW_goods_total As Integer

    '                'money集計用
    '                Dim Total_IW_Labor_Parts As Decimal
    '                Dim Total_IW_Labor As Decimal
    '                Dim Total_IW_Parts As Decimal
    '                Dim Total_IW_Tax As Decimal
    '                Dim Total_IW_total As Decimal

    '                Dim Total_OW_Labor_Parts As Decimal
    '                Dim Total_OW_Labor As Decimal
    '                Dim Total_OW_Parts As Decimal
    '                Dim Total_OW_Tax As Decimal
    '                Dim Total_OW_total As Decimal

    '                Dim Total_Revenue_without_Tax As Decimal

    '                Dim Average_IW_Labor_Parts As Decimal
    '                Dim Average_IW_Labor As Decimal
    '                Dim Average_IW_Parts As Decimal
    '                Dim Average_IW_Tax As Decimal
    '                Dim Average_IW_total As Decimal

    '                Dim Average_OW_Labor_Parts As Decimal
    '                Dim Average_OW_Labor As Decimal
    '                Dim Average_OW_Parts As Decimal
    '                Dim Average_OW_Tax As Decimal
    '                Dim Average_OW_total As Decimal

    '                Dim CostOfRevenue As Decimal
    '                Dim RevenueWithoutTax As Decimal
    '                Dim GrossProfit As Decimal
    '                Dim GrossProfit2 As Decimal

    '                Dim TotalCostOfRevenue As Decimal
    '                Dim TotalGrossProfit As Decimal
    '                Dim TotalGrossProfit2 As Decimal


    '                'Dim TotalRevenueWithoutTax As Decimal

    '                Dim Average_Revenue_without_Tax As Decimal
    '                Dim dailyCount As Integer

    '                '日付項目分処理
    '                For i = 0 To dsActivity_report.Tables(0).Rows.Count - 1

    '                    For j = 0 To dsSC_DSR_info.Tables(0).Rows.Count - 1

    '                        If activeData(i).day2 = totalDailyStatementRepartData(j).Billing_date Then

    '                            dailyCount = dailyCount + 1

    '                            'count
    '                            rowWork9 &= totalDailyStatementRepartData(j).IW_goods_total + totalDailyStatementRepartData(j).OW_goods_total
    '                            rowWork10 &= totalDailyStatementRepartData(j).IW_goods_total
    '                            rowWork11 &= totalDailyStatementRepartData(j).OW_goods_total

    '                            'money
    '                            rowWork12 &= totalDailyStatementRepartData(j).IW_Labor + totalDailyStatementRepartData(j).IW_Parts
    '                            rowWork13 &= totalDailyStatementRepartData(j).IW_Labor

    '                            rowWork14 &= totalDailyStatementRepartData(j).IW_Parts
    '                            rowWork15 &= totalDailyStatementRepartData(j).IW_Tax
    '                            rowWork16 &= totalDailyStatementRepartData(j).IW_total
    '                            rowWork17 &= totalDailyStatementRepartData(j).OW_Labor + totalDailyStatementRepartData(j).OW_Parts
    '                            rowWork18 &= totalDailyStatementRepartData(j).OW_Labor
    '                            rowWork19 &= totalDailyStatementRepartData(j).OW_Parts
    '                            rowWork20 &= totalDailyStatementRepartData(j).OW_Tax
    '                            rowWork21 &= totalDailyStatementRepartData(j).OW_total
    '                            RevenueWithoutTax = totalDailyStatementRepartData(j).IW_Labor + totalDailyStatementRepartData(j).IW_Parts + totalDailyStatementRepartData(j).OW_Labor + totalDailyStatementRepartData(j).OW_Parts
    '                            rowWork22 &= RevenueWithoutTax
    '                            'Added rowWork23
    '                            ''=B20*(1-$B$1)となります。
    '                            CostOfRevenue = totalDailyStatementRepartData(j).OW_Parts * (1 - (txtRate.Text / 100))
    '                            TotalCostOfRevenue = TotalCostOfRevenue + CostOfRevenue
    '                            rowWork23 &= CostOfRevenue

    '                            GrossProfit = RevenueWithoutTax - CostOfRevenue
    '                            TotalGrossProfit = TotalGrossProfit + GrossProfit
    '                            rowWork24 &= GrossProfit

    '                            GrossProfit2 = (RevenueWithoutTax - totalDailyStatementRepartData(j).IW_Parts) - CostOfRevenue
    '                            TotalGrossProfit2 = TotalGrossProfit2 + GrossProfit2
    '                            rowWork25 &= GrossProfit2

    '                            'count合計
    '                            Total_IW_OW_goods_total = Total_IW_OW_goods_total + (totalDailyStatementRepartData(j).IW_goods_total + totalDailyStatementRepartData(j).OW_goods_total)
    '                            Total_IW_goods_total = Total_IW_goods_total + totalDailyStatementRepartData(j).IW_goods_total
    '                            Total_OW_goods_total = Total_OW_goods_total + totalDailyStatementRepartData(j).OW_goods_total

    '                            'money合計
    '                            Total_IW_Labor_Parts = Total_IW_Labor_Parts + (totalDailyStatementRepartData(j).IW_Labor + totalDailyStatementRepartData(j).IW_Parts)
    '                            Total_IW_Labor = Total_IW_Labor + totalDailyStatementRepartData(j).IW_Labor
    '                            Total_IW_Parts = Total_IW_Parts + totalDailyStatementRepartData(j).IW_Parts
    '                            Total_IW_Tax = Total_IW_Tax + totalDailyStatementRepartData(j).IW_Tax
    '                            Total_IW_total = Total_IW_total + totalDailyStatementRepartData(j).IW_total

    '                            Total_OW_Labor_Parts = Total_OW_Labor_Parts + (totalDailyStatementRepartData(j).OW_Labor + totalDailyStatementRepartData(j).OW_Parts)
    '                            Total_OW_Labor = Total_OW_Labor + totalDailyStatementRepartData(j).OW_Labor

    '                            Total_OW_Parts = Total_OW_Parts + totalDailyStatementRepartData(j).OW_Parts

    '                            Total_OW_Tax = Total_OW_Tax + totalDailyStatementRepartData(j).OW_Tax
    '                            Total_OW_total = Total_OW_total + totalDailyStatementRepartData(j).OW_total
    '                            Total_Revenue_without_Tax = Total_Revenue_without_Tax + (totalDailyStatementRepartData(j).IW_Labor + totalDailyStatementRepartData(j).IW_Parts + totalDailyStatementRepartData(j).OW_Labor + totalDailyStatementRepartData(j).OW_Parts)

    '                        End If

    '                    Next j

    '                    rowWork9 &= ","
    '                    rowWork10 &= ","
    '                    rowWork11 &= ","
    '                    rowWork12 &= ","
    '                    rowWork13 &= ","
    '                    rowWork14 &= ","
    '                    rowWork15 &= ","
    '                    rowWork16 &= ","
    '                    rowWork17 &= ","
    '                    rowWork18 &= ","
    '                    rowWork19 &= ","
    '                    rowWork20 &= ","
    '                    rowWork21 &= ","
    '                    rowWork22 &= ","
    '                    rowWork23 &= ","
    '                    rowWork24 &= ","
    '                    rowWork25 &= ","

    '                Next i

    '                'count平均
    '                Average_IW_OW_goods_total = Total_IW_OW_goods_total / dailyCount
    '                Average_IW_goods_total = Total_IW_goods_total / dailyCount
    '                Average_OW_goods_total = Total_OW_goods_total / dailyCount

    '                'money平均
    '                Average_IW_Labor_Parts = Total_IW_Labor_Parts / dailyCount
    '                Average_IW_Labor = Total_IW_Labor / dailyCount
    '                Average_IW_Parts = Total_IW_Parts / dailyCount
    '                Average_IW_Tax = Total_IW_Tax / dailyCount
    '                Average_IW_total = Total_IW_total / dailyCount

    '                Average_OW_Labor_Parts = Total_OW_Labor_Parts / dailyCount
    '                Average_OW_Labor = Total_OW_Labor / dailyCount
    '                'Added 2019 07 09
    '                Average_OW_Parts = Total_OW_Parts / dailyCount
    '                Average_OW_Tax = Total_OW_Tax / dailyCount
    '                Average_OW_total = Total_OW_total / dailyCount

    '                Average_Revenue_without_Tax = Total_Revenue_without_Tax / dailyCount

    '                rowWork9 &= Total_IW_OW_goods_total & "," & Average_IW_OW_goods_total
    '                rowWork10 &= Total_IW_goods_total & "," & Average_IW_goods_total
    '                rowWork11 &= Total_OW_goods_total & "," & Average_OW_goods_total
    '                rowWork12 &= Total_IW_Labor_Parts & "," & Average_IW_Labor_Parts
    '                rowWork13 &= Total_IW_Labor & "," & Average_IW_Labor
    '                rowWork14 &= Total_IW_Parts & "," & Average_IW_Parts
    '                rowWork15 &= Total_IW_Tax & "," & Average_IW_Tax
    '                rowWork16 &= Total_IW_total & "," & Average_IW_total
    '                rowWork17 &= Total_OW_Labor_Parts & "," & Average_OW_Labor_Parts
    '                rowWork18 &= Total_OW_Labor & "," & Average_OW_Labor

    '                rowWork19 &= Total_OW_Parts & "," & Average_OW_Parts
    '                rowWork20 &= Total_OW_Tax & "," & Average_OW_Tax
    '                rowWork21 &= Total_OW_total & "," & Average_OW_total
    '                rowWork22 &= Total_Revenue_without_Tax & "," & Average_Revenue_without_Tax
    '                rowWork23 &= TotalCostOfRevenue & "," & TotalCostOfRevenue / dailyCount
    '                rowWork24 &= TotalGrossProfit & "," & TotalGrossProfit / dailyCount
    '                rowWork25 &= TotalGrossProfit2 & "," & TotalGrossProfit2 / dailyCount


    '                csvContents.Add(rowWork8)
    '                csvContents.Add(rowWork9)
    '                csvContents.Add(rowWork10)
    '                csvContents.Add(rowWork11)
    '                csvContents.Add(rowWork12)
    '                csvContents.Add(rowWork13)
    '                csvContents.Add(rowWork14)
    '                csvContents.Add(rowWork15)
    '                csvContents.Add(rowWork16)
    '                csvContents.Add(rowWork17)
    '                csvContents.Add(rowWork18)
    '                csvContents.Add(rowWork19)
    '                csvContents.Add(rowWork20)
    '                csvContents.Add(rowWork21)
    '                csvContents.Add(rowWork22)
    '                'Modified
    '                csvContents.Add(rowWork23)
    '                'Added Gross Profit
    '                csvContents.Add(rowWork24)
    '                'Added Gross Profit2
    '                csvContents.Add(rowWork25)

    '            Else
    '                csvContents.Add("There is no corresponding SC_DSR_info information.")
    '            End If

    '            'ファイル名、パスの設定
    '            Dim csvFileName As String

    '            dateFrom = Replace(dateFrom, "/", "")
    '            dateTo = Replace(dateTo, "/", "")

    '            'exportFile名の頭、数値を除く
    '            exportFile = Right(exportFile, exportFile.Length - 2)

    '            If setMon = "00" Then
    '                If dateTo <> "" And dateFrom <> "" Then
    '                    csvFileName = exportFile & "_ " & exportShipName & "_" & dateFrom & "_" & dateTo & ".csv"
    '                Else
    '                    If dateTo <> "" Then
    '                        csvFileName = exportFile & "_ " & exportShipName & "_" & dateTo & ".csv"
    '                    End If
    '                    If dateFrom <> "" Then
    '                        csvFileName = exportFile & "_ " & exportShipName & "_" & dateFrom & ".csv"
    '                    End If
    '                End If
    '            Else
    '                '月指定のとき
    '                csvFileName = exportFile & "_ " & exportShipName & "_" & dtNow.ToString("yyyy") & "_" & setMon & ".csv"
    '            End If

    '            outputPath = clsSet.CsvFilePass & csvFileName

    '            Using writer As New StreamWriter(outputPath, False, Encoding.Default)
    '                writer.WriteLine(String.Join(Environment.NewLine, csvContents))
    '            End Using

    '            'ファイルの内容をバイト配列にすべて読み込む 
    '            Dim Buffer As Byte() = System.IO.File.ReadAllBytes(outputPath)

    '            'サーバに保存されたCSVファイルを削除
    '            '※Response.End以降、ファイル削除処理ができないため、ファイルのダウンロードではなく、一旦ファイルの内容を
    '            '上記、Bufferに保存し、ダウンロード処理を行う。

    '            If outputPath <> "" Then
    '                If System.IO.File.Exists(outputPath) = True Then
    '                    System.IO.File.Delete(outputPath)
    '                End If
    '            End If

    '            ' ダウンロード
    '            Response.ContentType = "application/text/csv"
    '            Response.AddHeader("Content-Disposition", "attachment; filename=" & csvFileName)
    '            Response.Flush()
    '            'Response.Write("<b>File Contents: </b>")
    '            Response.BinaryWrite(Buffer)
    '            'Response.WriteFile(outputPath)
    '            Response.End()

    '        Catch ex As System.Threading.ThreadAbortException
    '            'Response.End()の呼び出しによりエラーメッセージを出力しないようにする

    '        Catch ex As Exception
    '            Call showMsg("CSV output processing failed.", "")
    '        End Try

    '    ElseIf kindExport = 2 Then

    '        'SalesRegister_OOW
    '        '部品情報を登録して、出力対象情報を取得
    '        Dim dsWtyExcel As New DataSet
    '        Dim wtyData() As Class_analysis.WTY_EXCEL
    '        Call clsSet.setWtyExcelDown(dsWtyExcel, wtyData, userid, userName, shipCode, exportShipName, errFlg, setMon, dateFrom, dateTo)

    '        If dsWtyExcel Is Nothing Then
    '            Call showMsg("There is no output target of SalesRegister_OOW.", "")
    '            Exit Sub
    '        End If

    '        If errFlg = 1 Then
    '            Call showMsg("SalesRegister_OOW information acquisition failed.", "")
    '            Exit Sub
    '        End If

    '        Try
    '            'ファイル名、パスの設定
    '            Dim csvFileName As String

    '            dateFrom = Replace(dateFrom, "/", "")
    '            dateTo = Replace(dateTo, "/", "")

    '            'exportFile名の頭、数値を除く
    '            exportFile = Right(exportFile, exportFile.Length - 2)

    '            If setMon = "00" Then
    '                If dateTo <> "" And dateFrom <> "" Then
    '                    csvFileName = exportShipName & "_Sales_OOW" & "_" & dateFrom & "_" & dateTo & ".csv"
    '                Else
    '                    If dateTo <> "" Then
    '                        csvFileName = exportShipName & "_Sales_OOW" & "_" & dateTo & ".csv"
    '                    End If
    '                    If dateFrom <> "" Then
    '                        csvFileName = exportShipName & "_Sales_OOW" & "_" & dateFrom & ".csv"
    '                    End If
    '                End If
    '            Else
    '                '月指定のとき
    '                csvFileName = exportShipName & "_Sales_OOW" & "_" & dtNow.ToString("yyyy") & "_" & setMon & ".csv"
    '            End If

    '            outputPath = clsSet.CsvFilePass & csvFileName

    '            '項目名設定
    '            Dim csvContents = New List(Of String)(New String() {exportShipName & "-Sales Register Out warranty"})
    '            'Comment on 20190809
    '            '''               Dim rowWork1 As String = "ASC Code,Branch Code,ASC Claim No,Samsung Claim No,Service Type,Consumer Name,Consumer Addr1,Consumer Addr2,Consumer Telephone,Consumer Fax,Postal Code,Model,Serial No,IMEI No,Defect Type,Condition,Symptom,Defect Code,Repair Code,Defect Desc,Repair Description,Purchase Date,Repair Received Date,Completed Date,Delivery Date,Production Date,Labor Amount Ⅰ,Parts Amount Ⅰ,Tax,Total Invoice Amount Ⅰ,Remark,Tr No,Tr Type,Status,Engineer,Collection Point,Collection Point Name,Location-1,Part-1,Qty-1,Unit Price-1,Doc Num-1,Matrial Serial-1,Location-2,Part-2,Qty-2,Unit Price-2,Doc Num-2,Matrial Serial-2,Location-3,Part-3,Qty-3,Unit Price-3,Doc Num-3,Matrial Serial-3,Location-4,Part-4,Qty-4,Unit Price-4,Doc Num-4,Matrial Serial-4,Location-5,Part-5,Qty-5,Unit Price-5,Doc Num-5,Matrial Serial-5,Location-6,Part-6,Qty-6,Unit Price-6,Doc Num-6,Matrial Serial-6,Location-7,Part-7,Qty-7,Unit Price-7,Doc Num-7,Matrial Serial-7,Location-8,Part-8,Qty-8,Unit Price-8,Doc Num-8,Matrial Serial-8,Location-9,Part-9,Qty-9,Unit Price-9,Doc Num-9,Matrial Serial-9,Location-10,Part-10,Qty-10,Unit Price-10,Doc Num-10,Matrial Serial-10,Location-11,Part-11,Qty-11,Unit Price-11,Doc Num-11,Matrial Serial-11,Location-12,Part-12,Qty-12,Unit Price-12,Doc Num-12,Matrial Serial-12,Location-13,Part-13,Qty-13,Unit Price-13,Doc Num-13,Matrial Serial-13,Location-14,Part-14,Qty-14,Unit Price-14,Doc Num-14,Matrial Serial-14,Location-15,Part-15,Qty-15,Unit Price-15,Doc Num-15,Matrial Serial-15"
    '            Dim rowWork1 As String = "ASC Code,Branch Code,ASC Claim No,Parts_invoice_No,Labor_Invoice_No,Samsung Claim No,Service Type,Consumer Name,Consumer Addr1,Consumer Addr2,Consumer Telephone,Consumer Fax,Postal Code,Model,Serial No,IMEI No,Defect Type,Condition,Symptom,Defect Code,Repair Code,Defect Desc,Repair Description,Purchase Date,Repair Received Date,Completed Date,Delivery Date,Production Date,Labor Amount Ⅰ,Parts Amount Ⅰ,Freight, Other, Parts_SGST, Parts_UTGST, Parts_CGST, Parts_IGST, Parts_Cess, SGST, UTGST, CGST, IGST, Cess, Total Invoice Amount Ⅰ,Remark,Tr No,Tr Type,Status,Engineer,Collection Point,Collection Point Name,Location-1,Part-1,Qty-1,Unit Price-1,Doc Num-1,Matrial Serial-1,Location-2,Part-2,Qty-2,Unit Price-2,Doc Num-2,Matrial Serial-2,Location-3,Part-3,Qty-3,Unit Price-3,Doc Num-3,Matrial Serial-3,Location-4,Part-4,Qty-4,Unit Price-4,Doc Num-4,Matrial Serial-4,Location-5,Part-5,Qty-5,Unit Price-5,Doc Num-5,Matrial Serial-5,Location-6,Part-6,Qty-6,Unit Price-6,Doc Num-6,Matrial Serial-6,Location-7,Part-7,Qty-7,Unit Price-7,Doc Num-7,Matrial Serial-7,Location-8,Part-8,Qty-8,Unit Price-8,Doc Num-8,Matrial Serial-8,Location-9,Part-9,Qty-9,Unit Price-9,Doc Num-9,Matrial Serial-9,Location-10,Part-10,Qty-10,Unit Price-10,Doc Num-10,Matrial Serial-10,Location-11,Part-11,Qty-11,Unit Price-11,Doc Num-11,Matrial Serial-11,Location-12,Part-12,Qty-12,Unit Price-12,Doc Num-12,Matrial Serial-12,Location-13,Part-13,Qty-13,Unit Price-13,Doc Num-13,Matrial Serial-13,Location-14,Part-14,Qty-14,Unit Price-14,Doc Num-14,Matrial Serial-14,Location-15,Part-15,Qty-15,Unit Price-15,Doc Num-15,Matrial Serial-15"
    '            csvContents.Add(rowWork1)

    '            For i = 0 To wtyData.Length - 1
    '                csvContents.Add(wtyData(i).ASC_Code & "," & wtyData(i).Branch_Code & "," & wtyData(i).ASC_Claim_No & "," & wtyData(i).Samsung_Claim_No & "," & wtyData(i).Service_Type & "," & wtyData(i).Consumer_Name & "," & wtyData(i).Consumer_Addr1 & "," & wtyData(i).Consumer_Addr2 & "," & wtyData(i).Consumer_Telephone & "," & wtyData(i).Consumer_Fax & "," & wtyData(i).Postal_Code & "," & wtyData(i).Model & "," & wtyData(i).Serial_No & "," & wtyData(i).IMEI_No & "," & wtyData(i).Defect_Type & "," & wtyData(i).Condition & "," & wtyData(i).Symptom & "," & wtyData(i).Defect_Code & "," & wtyData(i).Repair_Code & "," & wtyData(i).Defect_Desc & "," & wtyData(i).Repair_Description & "," & wtyData(i).Purchase_Date & "," & wtyData(i).Repair_Received_Date & "," & wtyData(i).Completed_Date & "," & wtyData(i).Delivery_Date & "," & wtyData(i).Production_Date & "," & wtyData(i).Labor_Amount & "," & wtyData(i).Parts_Amount & "," & wtyData(i).Tax & "," & wtyData(i).Total_Invoice_Amount & "," & wtyData(i).Remark & "," & wtyData(i).Tr_No & "," & wtyData(i).Tr_Type & "," & wtyData(i).Status & "," & wtyData(i).Engineer & "," & wtyData(i).Collection_Point & "," & wtyData(i).Collection_Point_Name & "," & wtyData(i).Location_1 & "," & wtyData(i).Part_1 & "," & wtyData(i).Qty_1 & "," & wtyData(i).Unit_Price_1 & "," & wtyData(i).Doc_Num_1 & "," & wtyData(i).Matrial_Serial_1 & "," & wtyData(i).Location_2 & "," & wtyData(i).Part_2 & "," & wtyData(i).Qty_2 & "," & wtyData(i).Unit_Price_2 & "," & wtyData(i).Doc_Num_2 & "," & wtyData(i).Matrial_Serial_2 & "," & wtyData(i).Location_3 & "," & wtyData(i).Part_3 & "," & wtyData(i).Qty_3 & "," & wtyData(i).Unit_Price_3 & "," & wtyData(i).Doc_Num_3 & "," & wtyData(i).Matrial_Serial_3 & "," & wtyData(i).Location_4 & "," & wtyData(i).Part_4 & "," & wtyData(i).Qty_4 & "," & wtyData(i).Unit_Price_4 & "," & wtyData(i).Doc_Num_4 & "," & wtyData(i).Matrial_Serial_4 & "," & wtyData(i).Location_5 & "," & wtyData(i).Part_5 & "," & wtyData(i).Qty_5 & "," & wtyData(i).Unit_Price_5 & "," & wtyData(i).Doc_Num_5 & "," & wtyData(i).Matrial_Serial_5 & "," & wtyData(i).Location_6 & "," & wtyData(i).Part_6 & "," & wtyData(i).Qty_6 & "," & wtyData(i).Unit_Price_6 & "," & wtyData(i).Doc_Num_6 & "," & wtyData(i).Matrial_Serial_6 & "," & wtyData(i).Location_7 & "," & wtyData(i).Part_7 & "," & wtyData(i).Qty_7 & "," & wtyData(i).Unit_Price_7 & "," & wtyData(i).Doc_Num_7 & "," & wtyData(i).Matrial_Serial_7 & "," & wtyData(i).Location_8 & "," & wtyData(i).Part_8 & "," & wtyData(i).Qty_8 & "," & wtyData(i).Unit_Price_8 & "," & wtyData(i).Doc_Num_8 & "," & wtyData(i).Matrial_Serial_8 & "," & wtyData(i).Location_9 & "," & wtyData(i).Part_9 & "," & wtyData(i).Qty_9 & "," & wtyData(i).Unit_Price_9 & "," & wtyData(i).Doc_Num_9 & "," & wtyData(i).Matrial_Serial_9 & "," & wtyData(i).Location_10 & "," & wtyData(i).Part_10 & "," & wtyData(i).Qty_10 & "," & wtyData(i).Unit_Price_10 & "," & wtyData(i).Doc_Num_10 & "," & wtyData(i).Matrial_Serial_10 & "," & wtyData(i).Location_11 & "," & wtyData(i).Part_11 & "," & wtyData(i).Qty_11 & "," & wtyData(i).Unit_Price_11 & "," & wtyData(i).Doc_Num_11 & "," & wtyData(i).Matrial_Serial_11 & "," & wtyData(i).Location_12 & "," & wtyData(i).Part_12 & "," & wtyData(i).Qty_12 & "," & wtyData(i).Unit_Price_12 & "," & wtyData(i).Doc_Num_12 & "," & wtyData(i).Matrial_Serial_12 & "," & wtyData(i).Location_13 & "," & wtyData(i).Part_13 & "," & wtyData(i).Qty_13 & "," & wtyData(i).Unit_Price_13 & "," & wtyData(i).Doc_Num_13 & "," & wtyData(i).Matrial_Serial_13 & "," & wtyData(i).Location_14 & "," & wtyData(i).Part_14 & "," & wtyData(i).Qty_14 & "," & wtyData(i).Unit_Price_14 & "," & wtyData(i).Doc_Num_14 & "," & wtyData(i).Matrial_Serial_14 & "," & wtyData(i).Location_15 & "," & wtyData(i).Part_15 & "," & wtyData(i).Qty_15 & "," & wtyData(i).Unit_Price_15 & "," & wtyData(i).Doc_Num_15 & "," & wtyData(i).Matrial_Serial_15)
    '            Next i

    '            Using writer As New StreamWriter(outputPath, False, Encoding.Default)
    '                writer.WriteLine(String.Join(Environment.NewLine, csvContents))
    '            End Using

    '            'ファイルの内容をバイト配列にすべて読み込む 
    '            Dim Buffer As Byte() = System.IO.File.ReadAllBytes(outputPath)

    '            'サーバに保存されたCSVファイルを削除
    '            '※Response.End以降、ファイル削除処理ができないため、ファイルのダウンロードではなく、一旦ファイルの内容を
    '            '上記、Bufferに保存し、ダウンロード処理を行う。

    '            If outputPath <> "" Then
    '                If System.IO.File.Exists(outputPath) = True Then
    '                    System.IO.File.Delete(outputPath)
    '                End If
    '            End If

    '            ' ダウンロード
    '            Response.ContentType = "application/text/csv"
    '            Response.AddHeader("Content-Disposition", "attachment; filename=" & csvFileName)
    '            Response.Flush()
    '            'Response.Write("<b>File Contents: </b>")
    '            Response.BinaryWrite(Buffer)
    '            'Response.WriteFile(outputPath)
    '            Response.End()

    '        Catch ex As System.Threading.ThreadAbortException
    '            'Response.End()の呼び出しによりエラーメッセージを出力しないようにする

    '        Catch ex As Exception
    '            Call showMsg("CSV output processing failed.", "")
    '        End Try

    '    ElseIf kindExport = 3 Then

    '        'Sales Invoice to samsung C
    '        '出力対象情報を取得
    '        Dim invoiceData() As Class_analysis.INVOICE
    '        Call clsSet.setInvoice(invoiceData, exportShipName, errFlg, setMon, "C")

    '        If invoiceData Is Nothing Then
    '            Call showMsg("Sales Invoice to samsung There is no output target of C.", "")
    '            Exit Sub
    '        End If

    '        If errFlg = 1 Then
    '            Call showMsg("InvoiceUpdate information acquisition failed.", "")
    '            Exit Sub
    '        End If

    '        Try
    '            'ファイル名、パスの設定

    '            'exportFile名の頭、数値を除く
    '            exportFile = Right(exportFile, exportFile.Length - 2)

    '            Dim csvFileName As String = "Sales_Inwarranty_C_" & exportShipName & "_" & dtNow.ToString("yyyy") & "_" & setMon & ".csv"

    '            outputPath = clsSet.CsvFilePass & csvFileName

    '            '項目名設定
    '            Dim rowWork1 As String = "Samsung Ref No,Your Ref No,Model,Serial,Product,Service,Defect Code,Currency,Invoice,Labor,Parts,Freight,Other,Tax,Parts_Invoice_No,Labpr_Invoice_No,Invoice Date"
    '            Dim csvContents = New List(Of String)(New String() {rowWork1})

    '            For i = 0 To invoiceData.Length - 1
    '                csvContents.Add(invoiceData(i).samsung_Ref_No & "," & invoiceData(i).Your_Ref_No & "," & invoiceData(i).Model & "," & invoiceData(i).Serial & "," & invoiceData(i).Product & "," & invoiceData(i).Serivice & "," & invoiceData(i).Defect_Code & "," & invoiceData(i).Currency & "," & invoiceData(i).Invoice & "," & invoiceData(i).Labor & "," & invoiceData(i).Parts & "," & invoiceData(i).Felight & "," & invoiceData(i).Other & "," & invoiceData(i).Tax & "," & invoiceData(i).Parts_invoice_No & "," & invoiceData(i).Labor_Invoice_No & "," & invoiceData(i).Invoice_date)
    '            Next i

    '            Using writer As New StreamWriter(outputPath, False, Encoding.Default)
    '                writer.WriteLine(String.Join(Environment.NewLine, csvContents))
    '            End Using

    '            'ファイルの内容をバイト配列にすべて読み込む 
    '            Dim Buffer As Byte() = System.IO.File.ReadAllBytes(outputPath)

    '            'サーバに保存されたCSVファイルを削除
    '            '※Response.End以降、ファイル削除処理ができないため、ファイルのダウンロードではなく、一旦ファイルの内容を
    '            '上記、Bufferに保存し、ダウンロード処理を行う。

    '            If outputPath <> "" Then
    '                If System.IO.File.Exists(outputPath) = True Then
    '                    System.IO.File.Delete(outputPath)
    '                End If
    '            End If

    '            ' ダウンロード
    '            Response.ContentType = "application/text/csv"
    '            Response.AddHeader("Content-Disposition", "attachment; filename=" & csvFileName)
    '            Response.Flush()
    '            'Response.Write("<b>File Contents: </b>")
    '            Response.BinaryWrite(Buffer)
    '            'Response.WriteFile(outputPath)
    '            Response.End()

    '        Catch ex As System.Threading.ThreadAbortException
    '            'Response.End()の呼び出しによりエラーメッセージを出力しないようにする

    '        Catch ex As Exception
    '            Call showMsg("CSV output processing failed.", "")
    '        End Try

    '    ElseIf kindExport = 4 Then

    '        'Sales Invoiec to samsung EXC
    '        '出力対象情報を取得
    '        Dim invoiceData() As Class_analysis.INVOICE
    '        Call clsSet.setInvoice(invoiceData, exportShipName, errFlg, setMon, "EXC")

    '        If invoiceData Is Nothing Then
    '            Call showMsg("Sales Invoiec to samsung There is no output target of ", "")
    '            Exit Sub
    '        End If

    '        If errFlg = 1 Then
    '            Call showMsg("InvoiceUpdate information acquisition failed.", "")
    '            Exit Sub
    '        End If

    '        Try
    '            'ファイル名、パスの設定

    '            'exportFile名の頭、数値を除く
    '            exportFile = Right(exportFile, exportFile.Length - 2)

    '            Dim csvFileName As String = "Sales_Inwarranty_EXC_" & exportShipName & "_" & dtNow.ToString("yyyy") & "_" & setMon & ".csv"
    '            outputPath = clsSet.CsvFilePass & csvFileName


    '            '項目名設定
    '            Dim rowWork1 As String = "Samsung Ref No,Your Ref No,Model,Serial,Product,Service,Defect Code,Currency,Invoice,Labor,Parts,Freight,Other,Tax,Parts_Invoice_No,Labpr_Invoice_No,Invoice Date"
    '            Dim csvContents = New List(Of String)(New String() {rowWork1})

    '            For i = 0 To invoiceData.Length - 1
    '                csvContents.Add(invoiceData(i).samsung_Ref_No & "," & invoiceData(i).Your_Ref_No & "," & invoiceData(i).Model & "," & invoiceData(i).Serial & "," & invoiceData(i).Product & "," & invoiceData(i).Serivice & "," & invoiceData(i).Defect_Code & "," & invoiceData(i).Currency & "," & invoiceData(i).Invoice & "," & invoiceData(i).Labor & "," & invoiceData(i).Parts & "," & invoiceData(i).Felight & "," & invoiceData(i).Other & "," & invoiceData(i).Tax & "," & invoiceData(i).Parts_invoice_No & "," & invoiceData(i).Labor_Invoice_No & "," & invoiceData(i).Invoice_date)
    '            Next i

    '            Using writer As New StreamWriter(outputPath, False, Encoding.Default)
    '                writer.WriteLine(String.Join(Environment.NewLine, csvContents))
    '            End Using

    '            'ファイルの内容をバイト配列にすべて読み込む 
    '            Dim Buffer As Byte() = System.IO.File.ReadAllBytes(outputPath)

    '            'サーバに保存されたCSVファイルを削除
    '            '※Response.End以降、ファイル削除処理ができないため、ファイルのダウンロードではなく、一旦ファイルの内容を
    '            '上記、Bufferに保存し、ダウンロード処理を行う。

    '            If outputPath <> "" Then
    '                If System.IO.File.Exists(outputPath) = True Then
    '                    System.IO.File.Delete(outputPath)
    '                End If
    '            End If

    '            ' ダウンロード
    '            Response.ContentType = "application/text/csv"
    '            Response.AddHeader("Content-Disposition", "attachment; filename=" & csvFileName)
    '            Response.Flush()
    '            'Response.Write("<b>File Contents: </b>")
    '            Response.BinaryWrite(Buffer)
    '            'Response.WriteFile(outputPath)
    '            Response.End()

    '        Catch ex As System.Threading.ThreadAbortException
    '            'Response.End()の呼び出しによりエラーメッセージを出力しないようにする

    '        Catch ex As Exception
    '            Call showMsg("CSV output processing failed.", "")
    '        End Try

    '    ElseIf kindExport = 8 Then

    '        'Purchase_Register
    '        '出力対象情報を取得
    '        Dim purchaseData2() As Class_analysis.BILLINGINFODETAIL  '出力用　
    '        Call clsSet.setPurchaseRegister(purchaseData2, exportShipName, shipCode, errFlg, setMon, dateFrom, dateTo)

    '        If purchaseData2 Is Nothing Then
    '            Call showMsg("There is no output target of Purchase_Register.", "")
    '            Exit Sub
    '        End If

    '        If errFlg = 1 Then
    '            Call showMsg("Failed to get good_recived information.", "")
    '            Exit Sub
    '        End If

    '        Try
    '            'ファイル名、パスの設定
    '            Dim csvFileName As String

    '            dateFrom = Replace(dateFrom, "/", "")
    '            dateTo = Replace(dateTo, "/", "")

    '            'exportFile名の頭、数値を除く
    '            exportFile = Right(exportFile, exportFile.Length - 2)

    '            If setMon = "00" Then
    '                If dateTo <> "" And dateFrom <> "" Then
    '                    csvFileName = "Purchase_Register_" & exportShipName & "_" & dateFrom & "_" & dateTo & ".csv"
    '                Else
    '                    If dateTo <> "" Then
    '                        csvFileName = "Purchase_Register_" & exportShipName & "_" & dateTo & ".csv"
    '                    End If
    '                    If dateFrom <> "" Then
    '                        csvFileName = "Purchase_Register_" & exportShipName & "_" & dateFrom & ".csv"
    '                    End If
    '                End If
    '            Else
    '                '月指定のとき
    '                csvFileName = "Purchase_Register_" & exportShipName & "_" & dtNow.ToString("yyyy") & "_" & setMon & ".csv"
    '            End If

    '            outputPath = clsSet.CsvFilePass & csvFileName

    '            '項目名設定
    '            Dim rowWork1 As String = "Ship-to-Branch-Code,Ship-to-Branch,Invoice Date,G/R Date,Invoice No,Local Invoice No,Delivery No,Items,Amount,SGST / UTGST,CGST,IGST,Cess,Tax,Total,GR Status"

    '            Dim csvContents = New List(Of String)(New String() {rowWork1})

    '            For i = 0 To purchaseData2.Length - 1
    '                csvContents.Add(purchaseData2(i).Branch_Code & "," & purchaseData2(i).Ship_Branch & "," & purchaseData2(i).delivery_date & "," & purchaseData2(i).GR_Date & "," & purchaseData2(i).Invoice_No & "," & purchaseData2(i).local_invoice_No & "," & purchaseData2(i).Delivery_No & "," & purchaseData2(i).Items & "," & purchaseData2(i).SumAmount & "," & purchaseData2(i).SumSGST_UTGST & "," & purchaseData2(i).SumCGST & "," & purchaseData2(i).SumIGST & "," & purchaseData2(i).SumCess & "," & purchaseData2(i).SumTax & "," & purchaseData2(i).SumTotal & "," & purchaseData2(i).GR_Status)
    '            Next i

    '            Using writer As New StreamWriter(outputPath, False, Encoding.Default)
    '                writer.WriteLine(String.Join(Environment.NewLine, csvContents))
    '            End Using

    '            'ファイルの内容をバイト配列にすべて読み込む 
    '            Dim Buffer As Byte() = System.IO.File.ReadAllBytes(outputPath)

    '            'サーバに保存されたCSVファイルを削除
    '            '※Response.End以降、ファイル削除処理ができないため、ファイルのダウンロードではなく、一旦ファイルの内容を
    '            '上記、Bufferに保存し、ダウンロード処理を行う。

    '            If outputPath <> "" Then
    '                If System.IO.File.Exists(outputPath) = True Then
    '                    System.IO.File.Delete(outputPath)
    '                End If
    '            End If

    '            ' ダウンロード
    '            Response.ContentType = "application/text/csv"
    '            Response.AddHeader("Content-Disposition", "attachment; filename=" & csvFileName)
    '            Response.Flush()
    '            'Response.Write("<b>File Contents: </b>")
    '            Response.BinaryWrite(Buffer)
    '            'Response.WriteFile(outputPath)
    '            Response.End()

    '        Catch ex As System.Threading.ThreadAbortException
    '            'Response.End()の呼び出しによりエラーメッセージを出力しないようにする

    '        Catch ex As Exception
    '            Call showMsg("CSV output processing failed.", "")
    '        End Try

    '    ElseIf kindExport = 5 Then

    '        'SalesRegister_IW
    '        '出力対象情報を取得
    '        Dim wtyDataIW() As Class_analysis.WTY_EXCEL

    '        Call clsSet.setSalesRegister_IW_OTHER(wtyDataIW, exportShipName, shipCode, userid, userName, errFlg, setMon, dateFrom, dateTo, "IW")

    '        If wtyDataIW Is Nothing Then
    '            Call showMsg("There is no output target of SalesRegister_IW.", "")
    '            Exit Sub
    '        End If

    '        If errFlg = 1 Then
    '            Call showMsg("Failed to get information on SalesRegister_IW.", "")
    '            Exit Sub
    '        End If

    '        Try
    '            'ファイル名、パスの設定
    '            Dim csvFileName As String

    '            dateFrom = Replace(dateFrom, "/", "")
    '            dateTo = Replace(dateTo, "/", "")

    '            'exportFile名の頭、数値を除く
    '            exportFile = Right(exportFile, exportFile.Length - 2)

    '            If setMon = "00" Then
    '                If dateTo <> "" And dateFrom <> "" Then
    '                    csvFileName = "Sales_Register_GSPIN_IW_" & exportShipName & "_" & dateFrom & "_" & dateTo & ".csv"
    '                Else
    '                    If dateTo <> "" Then
    '                        csvFileName = "Sales_Register_GSPIN_IW_" & exportShipName & "_" & dateTo & ".csv"
    '                    End If
    '                    If dateFrom <> "" Then
    '                        csvFileName = "Sales_Register_GSPIN_IW_" & exportShipName & "_" & dateFrom & ".csv"
    '                    End If
    '                End If
    '            Else
    '                '月指定のとき
    '                csvFileName = "Sales_Register_GSPIN_IW_" & exportShipName & "_" & dtNow.ToString("yyyy") & "_" & setMon & ".csv"
    '            End If

    '            outputPath = clsSet.CsvFilePass & csvFileName

    '            '項目名設定
    '            Dim rowWork1 As String = "ASC Code,Branch Code,ASC Claim No,Part Invoice No,Labour Invoice No,Samsung Claim No,Service Type,Consumer Name,Consumer Addr1,Consumer Addr2,Consumer Telephone,Consumer Fax,Postal Code,Model,Serial No,IMEI No,Defect Type,Condition,Symptom,Defect Code,Repair Code,Defect Desc,Repair Description,Purchase Date,Repair Received Date,Completed Date,Delivery Date,Production Date,Labor Amount Ⅰ,Parts Amount Ⅰ,Total Invoice Amount Ⅰ,Freight,Other,Parts SGST,Parts UTGST,Parts CGST,Parts IGST,Parts Cess,SGST,UTGST,CGST,IGST,Cess,Remark,Tr No,Tr Type,Status,Engineer,Collection Point,Collection Point Name,Location-1,Part-1,Qty-1,Unit Price-1,Doc Num-1,Matrial Serial-1,Location-2,Part-2,Qty-2,Unit Price-2,Doc Num-2,Matrial Serial-2,Location-3,Part-3,Qty-3,Unit Price-3,Doc Num-3,Matrial Serial-3,Location-4,Part-4,Qty-4,Unit Price-4,Doc Num-4,Matrial Serial-4,Location-5,Part-5,Qty-5,Unit Price-5,Doc Num-5,Matrial Serial-5,Location-6,Part-6,Qty-6,Unit Price-6,Doc Num-6,Matrial Serial-6,Location-7,Part-7,Qty-7,Unit Price-7,Doc Num-7,Matrial Serial-7,Location-8,Part-8,Qty-8,Unit Price-8,Doc Num-8,Matrial Serial-8,Location-9,Part-9,Qty-9,Unit Price-9,Doc Num-9,Matrial Serial-9,Location-10,Part-10,Qty-10,Unit Price-10,Doc Num-10,Matrial Serial-10,Location-11,Part-11,Qty-11,Unit Price-11,Doc Num-11,Matrial Serial-11,Location-12,Part-12,Qty-12,Unit Price-12,Doc Num-12,Matrial Serial-12,Location-13,Part-13,Qty-13,Unit Price-13,Doc Num-13,Matrial Serial-13,Location-14,Part-14,Qty-14,Unit Price-14,Doc Num-14,Matrial Serial-14,Location-15,Part-15,Qty-15,Unit Price-15,Doc Num-15,Matrial Serial-15"

    '            Dim csvContents = New List(Of String)(New String() {rowWork1})

    '            For i = 0 To wtyDataIW.Length - 1
    '                csvContents.Add(wtyDataIW(i).ASC_Code & "," & wtyDataIW(i).Branch_Code & "," & wtyDataIW(i).ASC_Claim_No & "," & wtyDataIW(i).Parts_invoice_No & "," & wtyDataIW(i).Labor_Invoice_No & "," & wtyDataIW(i).Samsung_Claim_No & "," & wtyDataIW(i).Service_Type & "," & wtyDataIW(i).Consumer_Name & "," & wtyDataIW(i).Consumer_Addr1 & "," & wtyDataIW(i).Consumer_Addr2 & "," & wtyDataIW(i).Consumer_Telephone & "," & wtyDataIW(i).Consumer_Fax & "," & wtyDataIW(i).Postal_Code & "," & wtyDataIW(i).Model & "," & wtyDataIW(i).Serial_No & "," & wtyDataIW(i).IMEI_No & "," & wtyDataIW(i).Defect_Type & "," & wtyDataIW(i).Condition & "," & wtyDataIW(i).Symptom & "," & wtyDataIW(i).Defect_Code & "," & wtyDataIW(i).Repair_Code & "," & wtyDataIW(i).Defect_Desc & "," & wtyDataIW(i).Repair_Description & "," & wtyDataIW(i).Purchase_Date & "," & wtyDataIW(i).Repair_Received_Date & "," & wtyDataIW(i).Completed_Date & "," & wtyDataIW(i).Delivery_Date & "," & wtyDataIW(i).Production_Date & "," & wtyDataIW(i).Labor & "," & wtyDataIW(i).Parts & "," & wtyDataIW(i).Invoice & "," & wtyDataIW(i).Freight & "," & wtyDataIW(i).Other & "," & wtyDataIW(i).Parts_SGST & "," & wtyDataIW(i).Parts_UTGST & "," & wtyDataIW(i).Parts_CGST & "," & wtyDataIW(i).Parts_IGST & "," & wtyDataIW(i).Parts_Cess & "," & wtyDataIW(i).SGST & "," & wtyDataIW(i).UTGST & "," & wtyDataIW(i).CGST & "," & wtyDataIW(i).IGST & "," & wtyDataIW(i).Cess & "," & wtyDataIW(i).Remark & "," & wtyDataIW(i).Tr_No & "," & wtyDataIW(i).Tr_Type & "," & wtyDataIW(i).Status & "," & wtyDataIW(i).Engineer & "," & wtyDataIW(i).Collection_Point & "," & wtyDataIW(i).Collection_Point_Name & "," & wtyDataIW(i).Location_1 & "," & wtyDataIW(i).Part_1 & "," & wtyDataIW(i).Qty_1 & "," & wtyDataIW(i).Unit_Price_1 & "," & wtyDataIW(i).Doc_Num_1 & "," & wtyDataIW(i).Matrial_Serial_1 & "," & wtyDataIW(i).Location_2 & "," & wtyDataIW(i).Part_2 & "," & wtyDataIW(i).Qty_2 & "," & wtyDataIW(i).Unit_Price_2 & "," & wtyDataIW(i).Doc_Num_2 & "," & wtyDataIW(i).Matrial_Serial_2 & "," & wtyDataIW(i).Location_3 & "," & wtyDataIW(i).Part_3 & "," & wtyDataIW(i).Qty_3 & "," & wtyDataIW(i).Unit_Price_3 & "," & wtyDataIW(i).Doc_Num_3 & "," & wtyDataIW(i).Matrial_Serial_3 & "," & wtyDataIW(i).Location_4 & "," & wtyDataIW(i).Part_4 & "," & wtyDataIW(i).Qty_4 & "," & wtyDataIW(i).Unit_Price_4 & "," & wtyDataIW(i).Doc_Num_4 & "," & wtyDataIW(i).Matrial_Serial_4 & "," & wtyDataIW(i).Location_5 & "," & wtyDataIW(i).Part_5 & "," & wtyDataIW(i).Qty_5 & "," & wtyDataIW(i).Unit_Price_5 & "," & wtyDataIW(i).Doc_Num_5 & "," & wtyDataIW(i).Matrial_Serial_5 & "," & wtyDataIW(i).Location_6 & "," & wtyDataIW(i).Part_6 & "," & wtyDataIW(i).Qty_6 & "," & wtyDataIW(i).Unit_Price_6 & "," & wtyDataIW(i).Doc_Num_6 & "," & wtyDataIW(i).Matrial_Serial_6 & "," & wtyDataIW(i).Location_7 & "," & wtyDataIW(i).Part_7 & "," & wtyDataIW(i).Qty_7 & "," & wtyDataIW(i).Unit_Price_7 & "," & wtyDataIW(i).Doc_Num_7 & "," & wtyDataIW(i).Matrial_Serial_7 & "," & wtyDataIW(i).Location_8 & "," & wtyDataIW(i).Part_8 & "," & wtyDataIW(i).Qty_8 & "," & wtyDataIW(i).Unit_Price_8 & "," & wtyDataIW(i).Doc_Num_8 & "," & wtyDataIW(i).Matrial_Serial_8 & "," & wtyDataIW(i).Location_9 & "," & wtyDataIW(i).Part_9 & "," & wtyDataIW(i).Qty_9 & "," & wtyDataIW(i).Unit_Price_9 & "," & wtyDataIW(i).Doc_Num_9 & "," & wtyDataIW(i).Matrial_Serial_9 & "," & wtyDataIW(i).Location_10 & "," & wtyDataIW(i).Part_10 & "," & wtyDataIW(i).Qty_10 & "," & wtyDataIW(i).Unit_Price_10 & "," & wtyDataIW(i).Doc_Num_10 & "," & wtyDataIW(i).Matrial_Serial_10 & "," & wtyDataIW(i).Location_11 & "," & wtyDataIW(i).Part_11 & "," & wtyDataIW(i).Qty_11 & "," & wtyDataIW(i).Unit_Price_11 & "," & wtyDataIW(i).Doc_Num_11 & "," & wtyDataIW(i).Matrial_Serial_11 & "," & wtyDataIW(i).Location_12 & "," & wtyDataIW(i).Part_12 & "," & wtyDataIW(i).Qty_12 & "," & wtyDataIW(i).Unit_Price_12 & "," & wtyDataIW(i).Doc_Num_12 & "," & wtyDataIW(i).Matrial_Serial_12 & "," & wtyDataIW(i).Location_13 & "," & wtyDataIW(i).Part_13 & "," & wtyDataIW(i).Qty_13 & "," & wtyDataIW(i).Unit_Price_13 & "," & wtyDataIW(i).Doc_Num_13 & "," & wtyDataIW(i).Matrial_Serial_13 & "," & wtyDataIW(i).Location_14 & "," & wtyDataIW(i).Part_14 & "," & wtyDataIW(i).Qty_14 & "," & wtyDataIW(i).Unit_Price_14 & "," & wtyDataIW(i).Doc_Num_14 & "," & wtyDataIW(i).Matrial_Serial_14 & "," & wtyDataIW(i).Location_15 & "," & wtyDataIW(i).Part_15 & "," & wtyDataIW(i).Qty_15 & "," & wtyDataIW(i).Unit_Price_15 & "," & wtyDataIW(i).Doc_Num_15 & "," & wtyDataIW(i).Matrial_Serial_15)
    '            Next i

    '            Using writer As New StreamWriter(outputPath, False, Encoding.Default)
    '                writer.WriteLine(String.Join(Environment.NewLine, csvContents))
    '            End Using

    '            'ファイルの内容をバイト配列にすべて読み込む 
    '            Dim Buffer As Byte() = System.IO.File.ReadAllBytes(outputPath)

    '            'サーバに保存されたCSVファイルを削除
    '            '※Response.End以降、ファイル削除処理ができないため、ファイルのダウンロードではなく、一旦ファイルの内容を
    '            '上記、Bufferに保存し、ダウンロード処理を行う。

    '            If outputPath <> "" Then
    '                If System.IO.File.Exists(outputPath) = True Then
    '                    System.IO.File.Delete(outputPath)
    '                End If
    '            End If

    '            ' ダウンロード
    '            Response.ContentType = "application/text/csv"
    '            Response.AddHeader("Content-Disposition", "attachment; filename=" & csvFileName)
    '            Response.Flush()
    '            'Response.Write("<b>File Contents: </b>")
    '            Response.BinaryWrite(Buffer)
    '            'Response.WriteFile(outputPath)
    '            Response.End()

    '        Catch ex As System.Threading.ThreadAbortException
    '            'Response.End()の呼び出しによりエラーメッセージを出力しないようにする

    '        Catch ex As Exception
    '            Call showMsg("CSV出力処理に失敗しました。", "")
    '        End Try

    '    ElseIf kindExport = 6 Then

    '        'Other_Sales_GSPIN
    '        '出力対象情報を取得
    '        Dim wtyDataOther() As Class_analysis.WTY_EXCEL

    '        Call clsSet.setSalesRegister_IW_OTHER(wtyDataOther, exportShipName, shipCode, userid, userName, errFlg, setMon, dateFrom, dateTo, "OTHER")

    '        If wtyDataOther Is Nothing Then
    '            Call showMsg("There is no output target of Other_Sales_GSPIN.", "")
    '            Exit Sub
    '        End If

    '        If errFlg = 1 Then
    '            Call showMsg("Failed to get information on Other_Sales_GSPIN.", "")
    '            Exit Sub
    '        End If

    '        Try
    '            'ファイル名、パスの設定
    '            Dim csvFileName As String

    '            dateFrom = Replace(dateFrom, "/", "")
    '            dateTo = Replace(dateTo, "/", "")

    '            'exportFile名の頭、数値を除く
    '            exportFile = Right(exportFile, exportFile.Length - 2)

    '            If setMon = "00" Then
    '                If dateTo <> "" And dateFrom <> "" Then
    '                    csvFileName = "Other_Sales_GSPIN_" & exportShipName & "_" & dateFrom & "_" & dateTo & ".csv"
    '                Else
    '                    If dateTo <> "" Then
    '                        csvFileName = "Other_Sales_GSPIN_" & exportShipName & "_" & dateTo & ".csv"
    '                    End If
    '                    If dateFrom <> "" Then
    '                        csvFileName = "Other_Sales_GSPIN_" & exportShipName & "_" & dateFrom & ".csv"
    '                    End If
    '                End If
    '            Else
    '                '月指定のとき
    '                csvFileName = "Other_Sales_GSPIN_" & exportShipName & "_" & dtNow.ToString("yyyy") & "_" & setMon & ".csv"
    '            End If

    '            outputPath = clsSet.CsvFilePass & csvFileName

    '            '項目名設定
    '            Dim rowWork1 As String = "ASC Code,Branch Code,ASC Claim No,Part Invoice No,Labour Invoice No,Samsung Claim No,Service Type,Consumer Name,Consumer Addr1,Consumer Addr2,Consumer Telephone,Consumer Fax,Postal Code,Model,Serial No,IMEI No,Defect Type,Condition,Symptom,Defect Code,Repair Code,Defect Desc,Repair Description,Purchase Date,Repair Received Date,Completed Date,Delivery Date,Production Date,Labor Amount Ⅰ,Parts Amount Ⅰ,Total Invoice Amount Ⅰ,Remark,Tr No,Tr Type,Status,Engineer,Collection Point,Collection Point Name,Location-1,Part-1,Qty-1,Unit Price-1,Doc Num-1,Matrial Serial-1,Location-2,Part-2,Qty-2,Unit Price-2,Doc Num-2,Matrial Serial-2,Location-3,Part-3,Qty-3,Unit Price-3,Doc Num-3,Matrial Serial-3,Location-4,Part-4,Qty-4,Unit Price-4,Doc Num-4,Matrial Serial-4,Location-5,Part-5,Qty-5,Unit Price-5,Doc Num-5,Matrial Serial-5,Location-6,Part-6,Qty-6,Unit Price-6,Doc Num-6,Matrial Serial-6,Location-7,Part-7,Qty-7,Unit Price-7,Doc Num-7,Matrial Serial-7,Location-8,Part-8,Qty-8,Unit Price-8,Doc Num-8,Matrial Serial-8,Location-9,Part-9,Qty-9,Unit Price-9,Doc Num-9,Matrial Serial-9,Location-10,Part-10,Qty-10,Unit Price-10,Doc Num-10,Matrial Serial-10,Location-11,Part-11,Qty-11,Unit Price-11,Doc Num-11,Matrial Serial-11,Location-12,Part-12,Qty-12,Unit Price-12,Doc Num-12,Matrial Serial-12,Location-13,Part-13,Qty-13,Unit Price-13,Doc Num-13,Matrial Serial-13,Location-14,Part-14,Qty-14,Unit Price-14,Doc Num-14,Matrial Serial-14,Location-15,Part-15,Qty-15,Unit Price-15,Doc Num-15,Matrial Serial-15"

    '            Dim csvContents = New List(Of String)(New String() {rowWork1})

    '            For i = 0 To wtyDataOther.Length - 1
    '                csvContents.Add(wtyDataOther(i).ASC_Code & "," & wtyDataOther(i).Branch_Code & "," & wtyDataOther(i).ASC_Claim_No & "," & wtyDataOther(i).Parts_invoice_No & "," & wtyDataOther(i).Labor_Invoice_No & "," & wtyDataOther(i).Samsung_Claim_No & "," & wtyDataOther(i).Service_Type & "," & wtyDataOther(i).Consumer_Name & "," & wtyDataOther(i).Consumer_Addr1 & "," & wtyDataOther(i).Consumer_Addr2 & "," & wtyDataOther(i).Consumer_Telephone & "," & wtyDataOther(i).Consumer_Fax & "," & wtyDataOther(i).Postal_Code & "," & wtyDataOther(i).Model & "," & wtyDataOther(i).Serial_No & "," & wtyDataOther(i).IMEI_No & "," & wtyDataOther(i).Defect_Type & "," & wtyDataOther(i).Condition & "," & wtyDataOther(i).Symptom & "," & wtyDataOther(i).Defect_Code & "," & wtyDataOther(i).Repair_Code & "," & wtyDataOther(i).Defect_Desc & "," & wtyDataOther(i).Repair_Description & "," & wtyDataOther(i).Purchase_Date & "," & wtyDataOther(i).Repair_Received_Date & "," & wtyDataOther(i).Completed_Date & "," & wtyDataOther(i).Delivery_Date & "," & wtyDataOther(i).Production_Date & "," & wtyDataOther(i).Labor & "," & wtyDataOther(i).Parts & "," & wtyDataOther(i).Invoice & "," & wtyDataOther(i).Remark & "," & wtyDataOther(i).Tr_No & "," & wtyDataOther(i).Tr_Type & "," & wtyDataOther(i).Status & "," & wtyDataOther(i).Engineer & "," & wtyDataOther(i).Collection_Point & "," & wtyDataOther(i).Collection_Point_Name & "," & wtyDataOther(i).Location_1 & "," & wtyDataOther(i).Part_1 & "," & wtyDataOther(i).Qty_1 & "," & wtyDataOther(i).Unit_Price_1 & "," & wtyDataOther(i).Doc_Num_1 & "," & wtyDataOther(i).Matrial_Serial_1 & "," & wtyDataOther(i).Location_2 & "," & wtyDataOther(i).Part_2 & "," & wtyDataOther(i).Qty_2 & "," & wtyDataOther(i).Unit_Price_2 & "," & wtyDataOther(i).Doc_Num_2 & "," & wtyDataOther(i).Matrial_Serial_2 & "," & wtyDataOther(i).Location_3 & "," & wtyDataOther(i).Part_3 & "," & wtyDataOther(i).Qty_3 & "," & wtyDataOther(i).Unit_Price_3 & "," & wtyDataOther(i).Doc_Num_3 & "," & wtyDataOther(i).Matrial_Serial_3 & "," & wtyDataOther(i).Location_4 & "," & wtyDataOther(i).Part_4 & "," & wtyDataOther(i).Qty_4 & "," & wtyDataOther(i).Unit_Price_4 & "," & wtyDataOther(i).Doc_Num_4 & "," & wtyDataOther(i).Matrial_Serial_4 & "," & wtyDataOther(i).Location_5 & "," & wtyDataOther(i).Part_5 & "," & wtyDataOther(i).Qty_5 & "," & wtyDataOther(i).Unit_Price_5 & "," & wtyDataOther(i).Doc_Num_5 & "," & wtyDataOther(i).Matrial_Serial_5 & "," & wtyDataOther(i).Location_6 & "," & wtyDataOther(i).Part_6 & "," & wtyDataOther(i).Qty_6 & "," & wtyDataOther(i).Unit_Price_6 & "," & wtyDataOther(i).Doc_Num_6 & "," & wtyDataOther(i).Matrial_Serial_6 & "," & wtyDataOther(i).Location_7 & "," & wtyDataOther(i).Part_7 & "," & wtyDataOther(i).Qty_7 & "," & wtyDataOther(i).Unit_Price_7 & "," & wtyDataOther(i).Doc_Num_7 & "," & wtyDataOther(i).Matrial_Serial_7 & "," & wtyDataOther(i).Location_8 & "," & wtyDataOther(i).Part_8 & "," & wtyDataOther(i).Qty_8 & "," & wtyDataOther(i).Unit_Price_8 & "," & wtyDataOther(i).Doc_Num_8 & "," & wtyDataOther(i).Matrial_Serial_8 & "," & wtyDataOther(i).Location_9 & "," & wtyDataOther(i).Part_9 & "," & wtyDataOther(i).Qty_9 & "," & wtyDataOther(i).Unit_Price_9 & "," & wtyDataOther(i).Doc_Num_9 & "," & wtyDataOther(i).Matrial_Serial_9 & "," & wtyDataOther(i).Location_10 & "," & wtyDataOther(i).Part_10 & "," & wtyDataOther(i).Qty_10 & "," & wtyDataOther(i).Unit_Price_10 & "," & wtyDataOther(i).Doc_Num_10 & "," & wtyDataOther(i).Matrial_Serial_10 & "," & wtyDataOther(i).Location_11 & "," & wtyDataOther(i).Part_11 & "," & wtyDataOther(i).Qty_11 & "," & wtyDataOther(i).Unit_Price_11 & "," & wtyDataOther(i).Doc_Num_11 & "," & wtyDataOther(i).Matrial_Serial_11 & "," & wtyDataOther(i).Location_12 & "," & wtyDataOther(i).Part_12 & "," & wtyDataOther(i).Qty_12 & "," & wtyDataOther(i).Unit_Price_12 & "," & wtyDataOther(i).Doc_Num_12 & "," & wtyDataOther(i).Matrial_Serial_12 & "," & wtyDataOther(i).Location_13 & "," & wtyDataOther(i).Part_13 & "," & wtyDataOther(i).Qty_13 & "," & wtyDataOther(i).Unit_Price_13 & "," & wtyDataOther(i).Doc_Num_13 & "," & wtyDataOther(i).Matrial_Serial_13 & "," & wtyDataOther(i).Location_14 & "," & wtyDataOther(i).Part_14 & "," & wtyDataOther(i).Qty_14 & "," & wtyDataOther(i).Unit_Price_14 & "," & wtyDataOther(i).Doc_Num_14 & "," & wtyDataOther(i).Matrial_Serial_14 & "," & wtyDataOther(i).Location_15 & "," & wtyDataOther(i).Part_15 & "," & wtyDataOther(i).Qty_15 & "," & wtyDataOther(i).Unit_Price_15 & "," & wtyDataOther(i).Doc_Num_15 & "," & wtyDataOther(i).Matrial_Serial_15)
    '            Next i

    '            Using writer As New StreamWriter(outputPath, False, Encoding.Default)
    '                writer.WriteLine(String.Join(Environment.NewLine, csvContents))
    '            End Using

    '            'ファイルの内容をバイト配列にすべて読み込む 
    '            Dim Buffer As Byte() = System.IO.File.ReadAllBytes(outputPath)

    '            'サーバに保存されたCSVファイルを削除
    '            '※Response.End以降、ファイル削除処理ができないため、ファイルのダウンロードではなく、一旦ファイルの内容を
    '            '上記、Bufferに保存し、ダウンロード処理を行う。

    '            If outputPath <> "" Then
    '                If System.IO.File.Exists(outputPath) = True Then
    '                    System.IO.File.Delete(outputPath)
    '                End If
    '            End If

    '            ' ダウンロード
    '            Response.ContentType = "application/text/csv"
    '            Response.AddHeader("Content-Disposition", "attachment; filename=" & csvFileName)
    '            Response.Flush()
    '            'Response.Write("<b>File Contents: </b>")
    '            Response.BinaryWrite(Buffer)
    '            'Response.WriteFile(outputPath)
    '            Response.End()

    '        Catch ex As System.Threading.ThreadAbortException
    '            'Response.End()の呼び出しによりエラーメッセージを出力しないようにする

    '        Catch ex As Exception
    '            Call showMsg("CSV output processing failed.", "")
    '        End Try

    '    ElseIf kindExport = 7 Then

    '        'SAW Discount Details
    '        '出力対象情報を取得
    '        Dim wtyDataOther() As Class_analysis.WTY_EXCEL

    '        Call clsSet.setSAW_Discount_Details(wtyDataOther, exportShipName, shipCode, userid, userName, errFlg, setMon, dateFrom, dateTo)

    '        If wtyDataOther Is Nothing Then
    '            Call showMsg("No SAW Discount Details output target.", "")
    '            Exit Sub
    '        End If

    '        If errFlg = 1 Then
    '            Call showMsg("SAW Discount Details information acquisition failed.", "")
    '            Exit Sub
    '        End If

    '        Try
    '            'ファイル名、パスの設定
    '            Dim csvFileName As String

    '            dateFrom = Replace(dateFrom, "/", "")
    '            dateTo = Replace(dateTo, "/", "")

    '            'exportFile名の頭、数値を除く
    '            exportFile = Right(exportFile, exportFile.Length - 2)

    '            If setMon = "00" Then
    '                If dateTo <> "" And dateFrom <> "" Then
    '                    csvFileName = "SAW_Discount_Details_" & exportShipName & "_" & dateFrom & "_" & dateTo & ".csv"
    '                Else
    '                    If dateTo <> "" Then
    '                        csvFileName = "SAW_Discount_Details_" & exportShipName & "_" & dateTo & ".csv"
    '                    End If
    '                    If dateFrom <> "" Then
    '                        csvFileName = "SAW_Discount_Details_" & exportShipName & "_" & dateFrom & ".csv"
    '                    End If
    '                End If
    '            Else
    '                '月指定のとき
    '                csvFileName = "SAW_Discount_Details_" & exportShipName & "_" & dtNow.ToString("yyyy") & "_" & setMon & ".csv"
    '            End If

    '            outputPath = clsSet.CsvFilePass & csvFileName

    '            '項目名設定
    '            Dim rowWork1 As String = "Branch Code,ASC Claim No,Model Name,Part Amount (Retail Price) with out tax,SAW Discount Parts Amount,SAW Discount Labor Amount,Invoice Amount collected from Cus,Part Invoice No,Labour Invoice No,SAW Remarks"

    '            Dim csvContents = New List(Of String)(New String() {rowWork1})

    '            For i = 0 To wtyDataOther.Length - 1
    '                csvContents.Add(exportShipName & "," & wtyDataOther(i).ASC_Claim_No & "," & wtyDataOther(i).Model & ",,,,," & wtyDataOther(i).Parts_invoice_No & "," & wtyDataOther(i).Labor_Invoice_No & ",")
    '            Next i

    '            Using writer As New StreamWriter(outputPath, False, Encoding.Default)
    '                writer.WriteLine(String.Join(Environment.NewLine, csvContents))
    '            End Using

    '            'ファイルの内容をバイト配列にすべて読み込む 
    '            Dim Buffer As Byte() = System.IO.File.ReadAllBytes(outputPath)

    '            'サーバに保存されたCSVファイルを削除
    '            '※Response.End以降、ファイル削除処理ができないため、ファイルのダウンロードではなく、一旦ファイルの内容を
    '            '上記、Bufferに保存し、ダウンロード処理を行う。

    '            If outputPath <> "" Then
    '                If System.IO.File.Exists(outputPath) = True Then
    '                    System.IO.File.Delete(outputPath)
    '                End If
    '            End If

    '            ' ダウンロード
    '            Response.ContentType = "application/text/csv"
    '            Response.AddHeader("Content-Disposition", "attachment; filename=" & csvFileName)
    '            Response.Flush()
    '            'Response.Write("<b>File Contents: </b>")
    '            Response.BinaryWrite(Buffer)
    '            'Response.WriteFile(outputPath)
    '            Response.End()

    '        Catch ex As System.Threading.ThreadAbortException
    '            'Response.End()の呼び出しによりエラーメッセージを出力しないようにする

    '        Catch ex As Exception
    '            Call showMsg("CSV output processing failed.", "")
    '        End Try

    '    ElseIf kindExport = 9 Then

    '        'Final_Report
    '        '***InvoiceNo_Final最終番号取得***
    '        Dim InvoiceMax As String = ""
    '        Dim InvoiceNoMax As Long
    '        Dim strSQL4 As String = "SELECT MAX(RIGHT(InvoiceNo_Final,7)) AS InvoiceNo_Final_Max FROM dbo.Wty_Excel "
    '        strSQL4 &= "WHERE DELFG = 0 AND Branch_Code = '" & shipCode & "';"
    '        Dim DT_WtyExcel As DataTable
    '        DT_WtyExcel = DBCommon.ExecuteGetDT(strSQL4, errFlg)

    '        If errFlg = 1 Then
    '            Call showMsg("Failed to acquire po information.", "")
    '            Exit Sub
    '        End If

    '        If DT_WtyExcel IsNot Nothing Then
    '            If DT_WtyExcel.Rows(0)("InvoiceNo_Final_Max") IsNot DBNull.Value Then
    '                InvoiceMax = DT_WtyExcel.Rows(0)("InvoiceNo_Final_Max")
    '                InvoiceNoMax = Convert.ToInt64(InvoiceMax)
    '                InvoiceNoMax = InvoiceNoMax + 1
    '            End If
    '        End If

    '        '***出力対象取得***
    '        Dim dsWtyExcel As New DataSet
    '        Dim wtyData() As Class_analysis.WTY_EXCEL

    '        Call clsSet.setFinalReport(dsWtyExcel, wtyData, userid, userName, shipCode, exportShipName, errFlg, setMon, dateFrom, dateTo, InvoiceNoMax)

    '        If dsWtyExcel Is Nothing Then
    '            Call showMsg("There is no output target of Final_Report.", "")
    '            Exit Sub
    '        End If

    '        If errFlg = 1 Then
    '            Call showMsg("Failed to get information on Final_Report.", "")
    '            Exit Sub
    '        End If

    '        Try
    '            '***ファイル名、パスの設定***
    '            Dim csvFileName As String

    '            dateFrom = Replace(dateFrom, "/", "")
    '            dateTo = Replace(dateTo, "/", "")

    '            'exportFile名の頭、数値を除く
    '            exportFile = Right(exportFile, exportFile.Length - 2)

    '            If setMon = "00" Then
    '                If dateTo <> "" And dateFrom <> "" Then
    '                    csvFileName = exportShipName & "_Final_Report" & "_" & dateFrom & "_" & dateTo & ".csv"
    '                Else
    '                    If dateTo <> "" Then
    '                        csvFileName = exportShipName & "_Final_Report" & "_" & dateTo & ".csv"
    '                    End If
    '                    If dateFrom <> "" Then
    '                        csvFileName = exportShipName & "_Final_Report" & "_" & dateFrom & ".csv"
    '                    End If
    '                End If
    '            Else
    '                '月指定のとき
    '                csvFileName = exportShipName & "_Final_Report" & "_" & dtNow.ToString("yyyy") & "_" & setMon & ".csv"
    '            End If

    '            outputPath = clsSet.CsvFilePass & csvFileName

    '            '***項目名設定***
    '            Dim csvContents = New List(Of String)(New String() {exportShipName & "-Final_Report"})
    '            Dim rowWork1 As String = "ASC Claim No,Invocie Number,Delivery Date,Payment Method,Part-1,Qty-1,Unit Price-1,Sum of Labor Amount,Sum of Parts Amount,SGST Payable,CGST Payable,IGST Payable,Sum of Tax Amount,Sum of Total Invoice Amount"

    '            csvContents.Add(rowWork1)

    '            '***部品情報設定***
    '            For i = 0 To wtyData.Length - 1

    '                ReDim Preserve wtyData(i).partsInfo(15)
    '                wtyData(i).partsInfo(0).PartsName = wtyData(i).Part_1
    '                wtyData(i).partsInfo(0).Qty = wtyData(i).Qty_1
    '                wtyData(i).partsInfo(0).UnitPrice = wtyData(i).Unit_Price_1

    '                wtyData(i).partsInfo(1).PartsName = wtyData(i).Part_2
    '                wtyData(i).partsInfo(1).Qty = wtyData(i).Qty_2
    '                wtyData(i).partsInfo(1).UnitPrice = wtyData(i).Unit_Price_2

    '                wtyData(i).partsInfo(2).PartsName = wtyData(i).Part_3
    '                wtyData(i).partsInfo(2).Qty = wtyData(i).Qty_3
    '                wtyData(i).partsInfo(2).UnitPrice = wtyData(i).Unit_Price_3

    '                wtyData(i).partsInfo(3).PartsName = wtyData(i).Part_4
    '                wtyData(i).partsInfo(3).Qty = wtyData(i).Qty_4
    '                wtyData(i).partsInfo(3).UnitPrice = wtyData(i).Unit_Price_4

    '                wtyData(i).partsInfo(4).PartsName = wtyData(i).Part_5
    '                wtyData(i).partsInfo(4).Qty = wtyData(i).Qty_5
    '                wtyData(i).partsInfo(4).UnitPrice = wtyData(i).Unit_Price_5

    '                '部品種類の個数を取得(部品5までは、6以降を確認しない)
    '                If wtyData(i).Part_6 = "" Then

    '                    If wtyData(i).Part_1 = "" Then
    '                        wtyData(i).partsCount = 0
    '                    Else
    '                        If wtyData(i).Part_2 = "" Then
    '                            wtyData(i).partsCount = 1
    '                        Else
    '                            If wtyData(i).Part_3 = "" Then
    '                                wtyData(i).partsCount = 2
    '                            Else
    '                                If wtyData(i).Part_4 = "" Then
    '                                    wtyData(i).partsCount = 3
    '                                Else
    '                                    If wtyData(i).Part_5 = "" Then
    '                                        wtyData(i).partsCount = 4
    '                                    Else
    '                                        If wtyData(i).Part_6 = "" Then
    '                                            wtyData(i).partsCount = 5
    '                                        End If
    '                                    End If
    '                                End If
    '                            End If
    '                        End If
    '                    End If
    '                Else

    '                    wtyData(i).partsInfo(5).PartsName = wtyData(i).Part_6
    '                    wtyData(i).partsInfo(5).Qty = wtyData(i).Qty_6
    '                    wtyData(i).partsInfo(5).UnitPrice = wtyData(i).Unit_Price_6

    '                    wtyData(i).partsInfo(6).PartsName = wtyData(i).Part_7
    '                    wtyData(i).partsInfo(6).Qty = wtyData(i).Qty_7
    '                    wtyData(i).partsInfo(6).UnitPrice = wtyData(i).Unit_Price_7

    '                    wtyData(i).partsInfo(7).PartsName = wtyData(i).Part_8
    '                    wtyData(i).partsInfo(7).Qty = wtyData(i).Qty_8
    '                    wtyData(i).partsInfo(7).UnitPrice = wtyData(i).Unit_Price_8

    '                    wtyData(i).partsInfo(8).PartsName = wtyData(i).Part_9
    '                    wtyData(i).partsInfo(8).Qty = wtyData(i).Qty_9
    '                    wtyData(i).partsInfo(8).UnitPrice = wtyData(i).Unit_Price_9

    '                    wtyData(i).partsInfo(9).PartsName = wtyData(i).Part_10
    '                    wtyData(i).partsInfo(9).Qty = wtyData(i).Qty_10
    '                    wtyData(i).partsInfo(9).UnitPrice = wtyData(i).Unit_Price_10

    '                    wtyData(i).partsInfo(10).PartsName = wtyData(i).Part_11
    '                    wtyData(i).partsInfo(10).Qty = wtyData(i).Qty_11
    '                    wtyData(i).partsInfo(10).UnitPrice = wtyData(i).Unit_Price_11

    '                    wtyData(i).partsInfo(11).PartsName = wtyData(i).Part_12
    '                    wtyData(i).partsInfo(11).Qty = wtyData(i).Qty_12
    '                    wtyData(i).partsInfo(11).UnitPrice = wtyData(i).Unit_Price_12

    '                    wtyData(i).partsInfo(12).PartsName = wtyData(i).Part_13
    '                    wtyData(i).partsInfo(12).Qty = wtyData(i).Qty_13
    '                    wtyData(i).partsInfo(12).UnitPrice = wtyData(i).Unit_Price_13

    '                    wtyData(i).partsInfo(13).PartsName = wtyData(i).Part_14
    '                    wtyData(i).partsInfo(13).Qty = wtyData(i).Qty_14
    '                    wtyData(i).partsInfo(13).UnitPrice = wtyData(i).Unit_Price_14

    '                    wtyData(i).partsInfo(14).PartsName = wtyData(i).Part_15
    '                    wtyData(i).partsInfo(14).Qty = wtyData(i).Qty_15
    '                    wtyData(i).partsInfo(14).UnitPrice = wtyData(i).Unit_Price_15

    '                    '部品6以降
    '                    If wtyData(i).Part_7 = "" Then
    '                        wtyData(i).partsCount = 6
    '                    Else
    '                        If wtyData(i).Part_8 = "" Then
    '                            wtyData(i).partsCount = 7
    '                        Else
    '                            If wtyData(i).Part_9 = "" Then
    '                                wtyData(i).partsCount = 8
    '                            Else
    '                                If wtyData(i).Part_10 = "" Then
    '                                    wtyData(i).partsCount = 9
    '                                Else
    '                                    If wtyData(i).Part_11 = "" Then
    '                                        wtyData(i).partsCount = 10
    '                                    Else
    '                                        If wtyData(i).Part_12 = "" Then
    '                                            wtyData(i).partsCount = 11
    '                                        Else
    '                                            If wtyData(i).Part_13 = "" Then
    '                                                wtyData(i).partsCount = 12
    '                                            Else
    '                                                If wtyData(i).Part_14 = "" Then
    '                                                    wtyData(i).partsCount = 13
    '                                                Else
    '                                                    If wtyData(i).Part_15 = "" Then
    '                                                        wtyData(i).partsCount = 14
    '                                                    Else
    '                                                        wtyData(i).partsCount = 15
    '                                                    End If
    '                                                End If
    '                                            End If
    '                                        End If
    '                                    End If
    '                                End If
    '                            End If
    '                        End If
    '                    End If
    '                End If
    '            Next i

    '            '***CSV出力***
    '            For i = 0 To wtyData.Length - 1

    '                '合計
    '                'wtyData(i).Sum_Total_Invoice_Amount = wtyData(i).Labor_Amount + wtyData(i).Parts_Amount + wtyData(i).Sum_Tax_Amount

    '                'InvoiceNo_Final 000の表記外す
    '                Dim tmp() As String = Split(wtyData(i).InvoiceNo_Final, "-")
    '                Dim tmp2 As Integer

    '                If tmp.Length = 2 Then
    '                    tmp2 = tmp(1)
    '                    wtyData(i).InvoiceNo_Final = tmp(0) & "-" & tmp2.ToString
    '                End If

    '                '最初の１行目、parts1と合計情報
    '                If wtyData(i).partsCount <> 0 Then
    '                    'csvContents.Add(wtyData(i).ASC_Claim_No & "," & wtyData(i).InvoiceNo_Final & "," & wtyData(i).Delivery_Date & "," & wtyData(i).Part_1 & "," & wtyData(i).Qty_1 & "," & wtyData(i).Unit_Price_1 & "," & wtyData(i).Labor_Amount & "," & wtyData(i).Parts_Amount & "," & wtyData(i).SGST_Payable & "," & wtyData(i).CGST_Payable & "," & wtyData(i).IGST_Payable & "," & wtyData(i).Sum_Tax_Amount & "," & wtyData(i).Sum_Total_Invoice_Amount)
    '                    csvContents.Add(wtyData(i).ASC_Claim_No & "," & wtyData(i).InvoiceNo_Final & "," & wtyData(i).Delivery_Date & "," & wtyData(i).payment & "," & wtyData(i).Part_1 & "," & wtyData(i).Qty_1 & "," & wtyData(i).Unit_Price_1 & "," & wtyData(i).OW_Labor & "," & wtyData(i).OW_Parts & "," & wtyData(i).SGST_Payable & "," & wtyData(i).CGST_Payable & "," & wtyData(i).IGST_Payable & "," & wtyData(i).Sum_Tax_Amount & "," & wtyData(i).OW_total)
    '                End If

    '                If wtyData(i).partsCount = 1 Then
    '                    'parts情報最後の行に、作業費を設定
    '                    csvContents.Add(wtyData(i).ASC_Claim_No & "," & wtyData(i).InvoiceNo_Final & "," & wtyData(i).Delivery_Date & "," & wtyData(i).payment & ",Labour Charges,," & wtyData(i).OW_Labor)
    '                Else
    '                    For j = 1 To wtyData(i).partsCount
    '                        '各行に部品毎情報を設定
    '                        If j = wtyData(i).partsCount Then
    '                            csvContents.Add(wtyData(i).ASC_Claim_No & "," & wtyData(i).InvoiceNo_Final & "," & wtyData(i).Delivery_Date & "," & wtyData(i).payment & ",Labour Charges,," & wtyData(i).OW_Labor)
    '                        Else
    '                            csvContents.Add(wtyData(i).ASC_Claim_No & "," & wtyData(i).InvoiceNo_Final & "," & wtyData(i).Delivery_Date & "," & wtyData(i).payment & "," & wtyData(i).partsInfo(j).PartsName & "," & wtyData(i).partsInfo(j).Qty & "," & wtyData(i).partsInfo(j).UnitPrice)
    '                        End If
    '                    Next j
    '                End If

    '            Next i

    '            Using writer As New StreamWriter(outputPath, False, Encoding.Default)
    '                writer.WriteLine(String.Join(Environment.NewLine, csvContents))
    '            End Using

    '            'ファイルの内容をバイト配列にすべて読み込む 
    '            Dim Buffer As Byte() = System.IO.File.ReadAllBytes(outputPath)

    '            'サーバに保存されたCSVファイルを削除
    '            '※Response.End以降、ファイル削除処理ができないため、ファイルのダウンロードではなく、一旦ファイルの内容を
    '            '上記、Bufferに保存し、ダウンロード処理を行う。

    '            If outputPath <> "" Then
    '                If System.IO.File.Exists(outputPath) = True Then
    '                    System.IO.File.Delete(outputPath)
    '                End If
    '            End If

    '            ' ダウンロード
    '            Response.ContentType = "application/text/csv"
    '            Response.AddHeader("Content-Disposition", "attachment; filename=" & csvFileName)
    '            Response.Flush()
    '            'Response.Write("<b>File Contents: </b>")
    '            Response.BinaryWrite(Buffer)
    '            'Response.WriteFile(outputPath)
    '            Response.End()

    '        Catch ex As System.Threading.ThreadAbortException
    '            'Response.End()の呼び出しによりエラーメッセージを出力しないようにする

    '        Catch ex As Exception
    '            If outputPath <> "" Then
    '                If System.IO.File.Exists(outputPath) = True Then
    '                    System.IO.File.Delete(outputPath)
    '                End If
    '            End If
    '            Call showMsg("CSV output processing failed.", "")
    '        End Try

    '    End If
    'End Sub
End Class