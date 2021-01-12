Imports System.Globalization
Imports System.IO
Imports System.Text
Imports Ganges33.Ganges33.model
Imports Ganges33.Ganges33.logic
Public Class Analysis_Upload_Summary
    Inherits System.Web.UI.Page
    Dim cc As CommonControl = New CommonControl()
    Dim msg As String
    Dim expfiledtl As Export_File_DetailsControl = New Export_File_DetailsControl()
    Dim expTaskParmdtl As Export_File_Parameter_Details = New Export_File_Parameter_Details()
    Private Property dtImpDetails As DataTable
        Get
            Return ViewState("dtImpDetails")
        End Get
        Set(value As DataTable)
            ViewState("dtImpDetails") = value
        End Set
    End Property
    Private Property dtExpfrom As String
        Get
            Return ViewState("dtExpfrom")
        End Get
        Set(value As String)
            ViewState("dtExpfrom") = value
        End Set
    End Property
    Private Property dtExpto As String
        Get
            Return ViewState("dtExpto")
        End Get
        Set(value As String)
            ViewState("dtExpto") = value
        End Set
    End Property
    Private Property ActiveFlag As String
        Get
            Return ViewState("ActiveFlag")
        End Get
        Set(value As String)
            ViewState("ActiveFlag") = value
        End Set
    End Property



    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try

            Dim scriptManager As ScriptManager = ScriptManager.GetCurrent(Me.Page)
            scriptManager.RegisterPostBackControl(Me.btnExport)
            scriptManager.RegisterPostBackControl(Me.btnSearch)

            If IsPostBack = False Then

                '***セッションなしログインユーザの対応***
                Dim userid As String = Session("user_id")

                If userid = "" Then
                    Response.Redirect("Login.aspx")
                End If

                Dim setShipname As String = Session("ship_Name")
                Dim userName As String = Session("user_Name")
                Dim userLevel As String = Session("user_level")
                Dim adminFlg As Boolean = Session("admin_Flg")

                'If userLevel = "0" Or userLevel = "1" Or userLevel = "2" Or adminFlg = True Then
                '    chkIndividual.Visible = True
                'End If

                lblLoc.Text = setShipname
                lblName.Text = userName

                'CommonControl comctrl 
                Dim comctrl As CommonControl = New CommonControl()
                ''''''''***拠点名称の設定***

                Dim ssclist As List(Of CodeMasterModel) = New List(Of CodeMasterModel)
                ssclist = comctrl.InitDropDownList(userName, userid)
                ssclist.RemoveAt(0)
                lstLocation.DataSource = ssclist
                lstLocation.DataTextField = "CodeDispValue"
                lstLocation.DataValueField = "CodeValue"
                lstLocation.DataBind()

                Dim dDate As DateTime = Date.Today
                Dim expdtl As Export_File_DetailsControl = New Export_File_DetailsControl()
                Dim dtimpdtl As DataTable = New DataTable()
                dtimpdtl = expdtl.GetImportTableDetails()

                TextDateFrom.Text = dDate.ToString("MM/dd/yyyy", CultureInfo.InvariantCulture)

                TextDateTo.Text = dDate.ToString("MM/dd/yyyy", CultureInfo.InvariantCulture)
                Dim sslSelect As Boolean = False

                If System.Configuration.ConfigurationManager.AppSettings.AllKeys.Contains("ImpStroreDtl") Then
                    ' Do Something
                    Dim SSCDtlAll As String = ConfigurationManager.AppSettings("ImpStroreDtl")

                    Dim SSCDtlInd As String() = SSCDtlAll.Split(New Char() {","c})

                    ' Use For Each loop over words and display them

                    Dim SSC As String
                    For Each SSC In SSCDtlInd
                        For Each item As ListItem In lstLocation.Items
                            If item.Text.ToUpper() = SSC.ToUpper() Then
                                item.Selected = True
                                sslSelect = True
                            End If
                        Next
                    Next

                    If sslSelect = True Then
                        btnSearch_Click(btnSearch, Nothing)
                    End If

                    ' Do Something Else
                End If
                'Response.Redirect("~/Analysis_Upload_Details.aspx?Store_ID= SSC1&Seq=1", False)
                'Analysis_Upload_Details.aspx?Store_ID= {0}&Seq={1}

            End If
        Catch ex As Exception
            lblMsg.Text = ex.Message
            msg = cc.showMsg("")
            ClientScript.RegisterStartupScript(Me.GetType(), "startup", msg, True)
            Exit Sub
        End Try



    End Sub


    Protected Sub btnSearch_Click(sender As Object, e As EventArgs) Handles btnSearch.Click
        Try
            Dim ShipName As String = Session("ship_Name")
            Dim shipCode As String = Session("ship_code")
            Dim userName As String = Session("user_Name")
            Dim userid As String = Session("user_id")
            Dim userLevel As String = Session("user_level")
            Dim adminFlg As Boolean = Session("admin_Flg")
            Dim messagetxt As String = ""
            Dim messageValue As String = ""
            Dim chkdate As Int16 = 0
            Dim columnName As String = "CRTDT"
            Session("ExportTaskInputDtl") = Nothing
            '***入力チェック***
            If ShipName = "" Then
                lblMsg.Text = "The session was cleared. Please login again."
                msg = cc.showMsg("")
                ClientScript.RegisterStartupScript(Me.GetType(), "startup", msg, True)
                Exit Sub
            End If

            For Each item As ListItem In lstLocation.Items
                If item.Selected Then
                    messagetxt += "'" + item.Text + "',"
                    messageValue += "'" + CInt(item.Value).ToString() + "',"
                End If
            Next
            If messagetxt = "" Then
                lblMsg.Text = "Please specify Target Branch."
                msg = cc.showMsg("")
                ClientScript.RegisterStartupScript(Me.GetType(), "startup", msg, True)
                Exit Sub
            End If

            messagetxt = Left(messagetxt, messagetxt.Length - 1)
            messageValue = Left(messageValue, messageValue.Length - 1)

            Dim DtFrom As String = ""
            Dim DtTo As String = ""
            DtFrom = Trim(TextDateFrom.Text)
            DtTo = Trim(TextDateTo.Text)

            Dim dtTbl1, dtTbl2 As DateTime
            If (Trim(DtFrom) <> "") Then
                If DateTime.TryParse(DtFrom, dtTbl1) Then
                    dtTbl1 = DateTime.Parse(Trim(DtFrom)).ToShortDateString
                Else
                    lblMsg.Text = "There is an error in the from date specification."
                    msg = cc.showMsg("")
                    ClientScript.RegisterStartupScript(Me.GetType(), "startup", msg, True)
                    Exit Sub
                End If
            Else
                chkdate = chkdate + 1
            End If

            If (Trim(DtTo) <> "") Then
                If DateTime.TryParse(DtTo, dtTbl2) Then
                    dtTbl2 = DateTime.Parse(Trim(DtTo)).ToShortDateString
                Else
                    lblMsg.Text = "There is an error in the to date specification."
                    msg = cc.showMsg("")
                    ClientScript.RegisterStartupScript(Me.GetType(), "startup", msg, True)
                    'Call showMsg("There is an error in the date specification", "")
                    Exit Sub
                End If
            Else
                chkdate = chkdate + 1

            End If

            If (Trim(DtFrom) <> "" And Trim(DtTo) <> "") Then
                If Len(Trim(DtFrom)) > 7 And Len(Trim(DtTo)) > 7 Then
                    Dim date1, date2 As Date
                    date1 = Date.Parse(TextDateFrom.Text)
                    date2 = Date.Parse(TextDateTo.Text)
                    If (DateTime.Compare(date1, date2) > 0) Then ' which means ("date1 > date2") 

                        lblMsg.Text = "Please verify from date and to date."
                        msg = cc.showMsg("")
                        ClientScript.RegisterStartupScript(Me.GetType(), "startup", msg, True)
                        Exit Sub
                    End If
                End If
            End If

            If chkdate = 2 Then
                lblMsg.Text = "Please select from date or to date."
                msg = cc.showMsg("")
                ClientScript.RegisterStartupScript(Me.GetType(), "startup", msg, True)
                Exit Sub
            End If

            If Trim(DtFrom) <> "" And Trim(DtTo) = "" Then
                DtTo = DtFrom
                TextDateTo.Text = DtFrom
            End If
            If Trim(DtFrom) = "" And Trim(DtTo) <> "" Then
                DtFrom = DtTo
                TextDateFrom.Text = DtTo
            End If

            DtFrom = CDate(DtFrom).ToString("yyyy-MM-dd")
            DtTo = CDate(DtTo).ToString("yyyy-MM-dd")

            Dim cuser As Boolean = False
            'If userLevel = "0" Or userLevel = "1" Or userLevel = "2" Or adminFlg = True Then
            '    If chkIndividual.Visible = True And chkIndividual.Checked = True Then
            '        cuser = True
            '    Else
            '        cuser = False
            '    End If
            'Else
            '    cuser = False
            'End If

            Dim dtimpdtl As DataTable =
            expfiledtl.GetImportFileSummary(messageValue, messagetxt, columnName, DtFrom, DtTo,
                                                   drpStatus.SelectedItem.Value, cuser, userLevel, adminFlg, userid)



            hdnStoreValue.Value = messageValue
            hdnStoreName.Value = messagetxt
            hdnColumnName.Value = columnName
            hdnDateFrom.Value = DtFrom
            hdnDateTo.Value = DtTo
            hdnActiveFlag.Value = drpStatus.SelectedItem.Value
            hdnCurrentUser.Value = cuser
            hdnUserLevel.Value = userLevel
            hdnAdminFlag.Value = adminFlg
            hdnUserId.Value = userid

            expTaskParmdtl.StoreValue = messageValue
            expTaskParmdtl.StoreName = messagetxt
            expTaskParmdtl.ColumnName = columnName
            expTaskParmdtl.DateFrom = DtFrom
            expTaskParmdtl.DateTo = DtTo
            expTaskParmdtl.IsActive = drpStatus.SelectedItem.Value
            expTaskParmdtl.CurrentUser = cuser
            expTaskParmdtl.UserLevel = userLevel
            expTaskParmdtl.AdminFlag = adminFlg
            expTaskParmdtl.UserID = userid


            Session("ExportTaskInputDtl") = expTaskParmdtl

            Dim dvimpdtl As DataView = New DataView(dtimpdtl)

            ViewState("dtImpDetails") = dvimpdtl.Table
            Dim dtScCode As DataTable = New DataTable()

            dtScCode = dvimpdtl.Table.DefaultView.ToTable(True, "Target_Store")

            Dim dt_clone As DataTable = New DataTable
            dt_clone = dvimpdtl.Table.Clone

            For Each row As DataRow In dtScCode.Rows
                Dim drNewRow As DataRow = dt_clone.NewRow

                drNewRow("Seq") = 0
                drNewRow("Target_Store") = row.Item("Target_Store")
                drNewRow("Report_Name") = row.Item("Target_Store")
                drNewRow("Cnt") = 0
                dt_clone.Rows.Add(drNewRow)
                Dim drs() As DataRow = ViewState("dtImpDetails").Select(("Target_Store = '" & row.Item("Target_Store") & "'"))
                For Each dr As DataRow In drs
                    dt_clone.Rows.Add(dr.ItemArray)
                Next

            Next row


            gvExportReport.DataSource = dt_clone
            gvExportReport.DataBind()
            ChangeExportDetails()

            ' Convert Gridview to data table
            Dim dt As DataTable = New DataTable
            Dim i As Integer = 0
            Do While (i < gvExportReport.Columns.Count)
                dt.Columns.Add(("column" + i.ToString))
                i = (i + 1)
            Loop

            For Each row As GridViewRow In gvExportReport.Rows
                Dim dr As DataRow = dt.NewRow
                Dim j As Integer = 0
                Do While (j < gvExportReport.Columns.Count)
                    If j = 3 Then
                        If row.Cells(j).Controls.Count > 0 Then
                            dr(("column" + j.ToString)) = DirectCast(row.Cells(j).Controls.Item(0), System.Web.UI.WebControls.HyperLink).[Text]
                        Else
                            dr(("column" + j.ToString)) = row.Cells(j).Text
                        End If


                    Else
                        dr(("column" + j.ToString)) = row.Cells(j).Text
                    End If

                    j = (j + 1)
                Loop

                dt.Rows.Add(dr)
            Next

            ViewState("ActiveFlag") = If(drpStatus.SelectedItem.Value = 0, "Active", "InActive")
            If dvimpdtl.Table.Rows.Count = 0 Then
                lblReccount.Text = "No Record Available"
                lblReccount.Visible = True
                btnExport.Visible = False
                ViewState("dtExpfrom") = String.Empty
                ViewState("dtExpto") = String.Empty
            Else
                lblReccount.Text = ""
                lblReccount.Visible = False
                btnExport.Visible = True
                ViewState("dtExpfrom") = DtFrom
                ViewState("dtExpto") = DtTo
            End If


        Catch ex As Exception
            lblMsg.Text = ex.Message
            Dim msg As String = cc.showMsg("")
            ClientScript.RegisterStartupScript(Me.GetType(), "startup", msg, True)
            Exit Sub
        End Try
    End Sub


    Protected Sub ChangeExportDetails()

        'Dim TST As String
        'Dim loc_123 As String = "loc_123"
        For counter As Integer = 0 To gvExportReport.Rows.Count - 1
            If gvExportReport.Rows(counter).Cells(1).Text = "0" Then
                gvExportReport.Rows(counter).Cells(1).Text = gvExportReport.Rows(counter).Cells(2).Text
                gvExportReport.Rows(counter).Cells(2).Text = ""
                gvExportReport.Rows(counter).Cells(3).Text = ""
                'gvExportReport.Rows(counter).Cells(1).ColumnSpan = 6
                gvExportReport.Rows(counter).Font.Bold = True
                gvExportReport.Rows(counter).ForeColor = Drawing.Color.Black
                gvExportReport.Rows(counter).Cells(1).BorderColor = Drawing.Color.FromArgb(185, 183, 184)
                gvExportReport.Rows(counter).Cells(2).BorderColor = Drawing.Color.FromArgb(185, 183, 184)
                gvExportReport.Rows(counter).Cells(3).BorderColor = Drawing.Color.FromArgb(185, 183, 184)
                gvExportReport.Rows(counter).Cells(4).BorderColor = Drawing.Color.FromArgb(185, 183, 184)
                'gvExportReport.Rows(counter).Cells(2).Visible = False
                'gvExportReport.Rows(counter).BorderWidth = False
                'gvExportReport.Rows(counter).Cells(3).Visible = False
                'gvExportReport.Rows(counter).Cells(4).Visible = False
                'gvExportReport.Rows(counter).Cells(5).Visible = False
                'gvExportReport.Rows(counter).Cells(6).Visible = False
                gvExportReport.Rows(counter).BackColor = Drawing.Color.FromArgb(185, 183, 184)

            End If

        Next

    End Sub





    Protected Sub btnExport_Click(sender As Object, e As EventArgs) Handles btnExport.Click


        ExportCSV("Import_Summary_Details.csv", dtImpDetails)

    End Sub
    Sub ExportCSV(ExportFileName As String, result As DataTable) 'VJ 2019/10/14 End


        result.Columns.Add("Seq No", GetType(String))
        result.Columns.Add("No of Records", GetType(String))

        For Each row As DataRow In result.Rows


            row(6) = row(0).ToString()
            row(7) = row(3).ToString()


        Next

        result.Columns("Target_Store").SetOrdinal(0)
        result.Columns("Seq No").SetOrdinal(1)
        result.Columns("Report_Name").SetOrdinal(2)

        result.Columns("No of Records").SetOrdinal(3)


        result.Columns.Remove("Seq")
        result.Columns.Remove("Cnt")
        result.Columns("Target_Store").ColumnName = "Target Store"

        result.Columns("Report_Name").ColumnName = "Report Name"
        result.Columns("Create_Date_From").ColumnName = "Create Date From"
        result.Columns("Create_Date_To").ColumnName = "Create Date To"


        Dim dr As DataRow = result.NewRow

        dr("Report Name") = "Create Date"
        dr("Create Date From") = hdnDateFrom.Value
        dr("Create Date To") = hdnDateTo.Value

        result.Rows.InsertAt(dr, 0)


        dr = result.NewRow
        dr("Seq No") = "Active Flag"
        dr("Report Name") = If(hdnActiveFlag.Value = 0, "Active", "InActive")
        result.Rows.InsertAt(dr, 1)

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


End Class