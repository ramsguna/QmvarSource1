Imports System.Globalization
Imports System.IO
Imports System.Text
Imports Ganges33.Ganges33.model
Imports Ganges33.Ganges33.logic
Imports System

Public Class Analysis_Upload_Details
    Inherits System.Web.UI.Page
    Dim cc As CommonControl = New CommonControl()
    Dim msg As String
    Dim expfiledtl As Export_File_DetailsControl = New Export_File_DetailsControl()
    Dim exfiledtl As Export_File_DetailsControl = New Export_File_DetailsControl()
    Dim _Expflddtl As Export_File_Details = New Export_File_Details()

    Private Property SortDirection As String
        Get
            Return IIf(ViewState("SortDirection") IsNot Nothing, Convert.ToString(ViewState("SortDirection")), "ASC")
        End Get
        Set(value As String)
            ViewState("SortDirection") = value
        End Set
    End Property



    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try

            Dim scriptManager As ScriptManager = ScriptManager.GetCurrent(Me.Page)
            scriptManager.RegisterPostBackControl(Me.btnExport)


            Dim prevPage As String


            If IsPostBack = False Then

                Dim setShipname As String = Session("ship_Name")
                Dim userName As String = Session("user_Name")
                Dim userLevel As String = Session("user_level")
                Dim adminFlg As Boolean = Session("admin_Flg")

                lblLoc.Text = setShipname
                lblName.Text = userName

                lblErrorMessage.Visible = False
                If (Not (Me.Page.Request.UrlReferrer) Is Nothing) Then
                    prevPage = Request.UrlReferrer.AbsolutePath.ToString()
                End If
                'Comment on 20200221
                '''''If Not prevPage.ToUpper() = "/Analysis_Upload_Summary.aspx".ToUpper() Then
                '''''    Session("ExportTaskInputDtl") = Nothing
                '''''    Response.Redirect("Login.aspx")
                '''''End If

                hdnStoreID.Value = Request.QueryString("Store_ID").ToString()
                hdnSeqNo.Value = Convert.ToInt16(Request.QueryString("Seq").ToString())
                lblLocationValue.Text = "<b>Target Store: </b>" & hdnStoreID.Value
                lblDateFrom.Text = "<b>From Date(YYYY-MM-DD) :</b>" & CType(Session("ExportTaskInputDtl"), Export_File_Parameter_Details).DateFrom
                lblDateTo.Text = "<b>To Date(YYYY-MM-DD):</b>" & CType(Session("ExportTaskInputDtl"), Export_File_Parameter_Details).DateTo
                lblActive.Text = "<b>Active :</b>" & IIf(CType(Session("ExportTaskInputDtl"), Export_File_Parameter_Details).IsActive = "0", "Yes", "False")

                '***セッションなしログインユーザの対応***

                'If userLevel = "0" Or userLevel = "1" Or userLevel = "2" Or adminFlg = True Then
                '    If CType(Session("ExportTaskInputDtl"), Export_File_Parametr_Details).CurrentUser.ToUpper() = "True".ToUpper() Then
                '        chkIndividual.Visible = True
                '        chkIndividual.Enabled = False
                '        chkIndividual.Checked = True
                '    Else
                '        chkIndividual.Visible = True
                '        chkIndividual.Enabled = False
                '        chkIndividual.Checked = False
                '    End If
                'Else
                '    chkIndividual.Visible = False
                'End If

                Dim userid As String = Session("user_id")

                If userid = "" Then
                    Response.Write("<script language=javascript>this.window.opener = null;window.open('','_self'); alert('Session Expired'); window.close();   </script>")
                    'Response.Redirect("Login.aspx")
                End If



                ViewState("SortExpression") = Nothing
                ViewState("SortDirection") = Nothing

                Dim colval As String = "Id=" & hdnSeqNo.Value
                Dim rows = exfiledtl.GetImportTableDetails().Select(colval)
                If Not IsNothing(rows) Then
                    lblTitle.Text = rows(0)("Id").ToString() & ". " & rows(0)("Description").ToString()
                End If

                Dim comctrl As CommonControl = New CommonControl()
                Dim ssclist As List(Of CodeMasterModel) = New List(Of CodeMasterModel)
                ssclist = comctrl.InitDropDownList(userName, userid).FindAll(Function(p) p.CodeDispValue = hdnStoreID.Value)
                hdnStoreIDValue.Value = ssclist(0).CodeValue

                GenerateExportDetails(rows(0)("Rep_Exp_Seq").ToString(), rows(0)("Table_Name").ToString(), hdnStoreID.Value, hdnStoreIDValue.Value, hdnSeqNo.Value)

            End If
        Catch ex As Exception
            lblMsg.Text = ex.Message
            msg = cc.showMsg("")
            ClientScript.RegisterStartupScript(Me.GetType(), "startup", msg, True)
            Exit Sub
        End Try



    End Sub

    Protected Sub OnPageIndexChanging(ByVal sender As Object, ByVal e As GridViewPageEventArgs)
        Try
            gvExportReport.PageIndex = e.NewPageIndex

            Dim dt As DataTable = CType(exfiledtl.GetImportFileSummaryDetails(Session("ExportTaskInputDtl"), "R", hdnStoreID.Value, hdnStoreIDValue.Value, hdnSeqNo.Value), DataTable)
            Dim dv As DataView = New DataView(dt)
            If ViewState("SortExpression") Is Nothing Then
                dv.Sort = gvExportReport.Columns(0).SortExpression & " " & Me.SortDirection
            Else
                dv.Sort = ViewState("SortExpression") & " " & Me.SortDirection
            End If

            gvExportReport.DataSource = dv
            gvExportReport.DataBind()
        Catch ex As Exception
            lblMsg.Text = ex.Message
            msg = cc.showMsg("")
            ClientScript.RegisterStartupScript(Me.GetType(), "startup", msg, True)
            Exit Sub
        End Try


    End Sub
    Protected Sub OnSorting(ByVal sender As Object, ByVal e As GridViewSortEventArgs)
        Try
            If IsPostBack = True Then


                Dim dt As DataTable = CType(exfiledtl.GetImportFileSummaryDetails(Session("ExportTaskInputDtl"), "R", hdnStoreID.Value, hdnStoreIDValue.Value, hdnSeqNo.Value), DataTable)
                If (Not (dt) Is Nothing) Then
                    Dim dv As DataView = New DataView(dt)
                    ViewState("SortExpression") = e.SortExpression
                    Me.SortDirection = IIf(Me.SortDirection = "ASC", "DESC", "ASC")
                    dv.Sort = e.SortExpression & " " & Me.SortDirection
                    gvExportReport.DataSource = dv
                    gvExportReport.DataBind()
                End If
            End If
        Catch ex As Exception
            lblMsg.Text = ex.Message
            msg = cc.showMsg("")
            ClientScript.RegisterStartupScript(Me.GetType(), "startup", msg, True)
            Exit Sub
        End Try
    End Sub
    Public Sub GenerateExportDetails(ByVal repID As String, ByVal Table_Name As String, ByVal Store_ID As String, ByVal Store_ID_Name As String, ByVal Seq As Integer)


        Dim dgColdtl As DataTable = exfiledtl.GetReportFieldDetails(repID)
        Dim userLevel As String = Session("user_level")
        Dim adminFlg As Boolean = Session("admin_Flg")

        Dim dr As DataRow = dgColdtl.NewRow
        dr("Id") = 5
        dr("DataField") = "CRTDT"
        dr("HeaderText") = "Created Date"
        dr("Column_Name") = ""
        dr("SortExpression") = "CRTDT"
        dr("Table_Name") = Table_Name
        dr("Rep_Seq") = dgColdtl.Rows(0)("Rep_Seq")


        dgColdtl.Rows.InsertAt(dr, 0)


        dr = dgColdtl.NewRow
        dr("Id") = 6
        dr("DataField") = "CRTCD"
        dr("HeaderText") = "Created User"
        dr("Column_Name") = ""
        dr("SortExpression") = "CRTCD"
        dr("Table_Name") = Table_Name
        dr("Rep_Seq") = dgColdtl.Rows(0)("Rep_Seq")
        dgColdtl.Rows.InsertAt(dr, 1)




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
        Next

        Dim dtScDsrView As DataTable = exfiledtl.GetImportFileSummaryDetails(Session("ExportTaskInputDtl"), "R", Store_ID, Store_ID_Name, Seq)

        gvExportReport.DataSource = dtScDsrView

        lbltotal.Text = "<b>Total No of Records : </b>" & dtScDsrView.Rows.Count

        If dtScDsrView.Rows.Count = 0 Then

            gvExportReport.AllowSorting = False
            gvExportReport.DataBind()
            gvExportReport.Visible = False
            btnExport.Visible = False
            txtPageSize.Visible = False
            lblPagesize.Visible = False
        Else
            txtPageSize.Visible = True
            lblPagesize.Visible = True
            txtPageSize.Text = 10
            gvExportReport.PageSize = Convert.ToInt16(txtPageSize.Text)
            gvExportReport.Visible = True
            gvExportReport.AllowSorting = True
            gvExportReport.PageIndex = 0
            gvExportReport.DataBind()

            If userLevel = "0" Or userLevel = "1" Or userLevel = "2" Or adminFlg = True Then
                btnExport.Visible = True
            End If


        End If

    End Sub
    Protected Sub txtPageSize_TextChanged(sender As Object, e As EventArgs) Handles txtPageSize.TextChanged

        Dim intValue As Integer
        If Integer.TryParse(txtPageSize.Text, intValue) AndAlso intValue > 0 AndAlso intValue <= 9999 Then
            lblErrorMessage.Visible = False


            Dim dt As DataTable = CType(exfiledtl.GetImportFileSummaryDetails(Session("ExportTaskInputDtl"), "R", hdnStoreID.Value, hdnStoreIDValue.Value, hdnSeqNo.Value), DataTable)
            Dim dv As DataView = New DataView(dt)
            If ViewState("SortExpression") Is Nothing Then
                dv.Sort = gvExportReport.Columns(0).SortExpression & " " & Me.SortDirection
            Else
                dv.Sort = ViewState("SortExpression") & " " & Me.SortDirection
            End If
            txtPageSize.Text = Convert.ToInt16(txtPageSize.Text)
            gvExportReport.PageSize = Convert.ToInt16(txtPageSize.Text)
            gvExportReport.PageIndex = 0
            gvExportReport.DataSource = dv
            gvExportReport.DataBind()
        Else
            lblErrorMessage.Visible = True
            txtPageSize.Text = gvExportReport.PageSize

        End If

    End Sub

    Protected Sub btnExport_Click(sender As Object, e As EventArgs) Handles btnExport.Click

        Dim dtScDsrView As DataTable = exfiledtl.GetImportFileSummaryDetails(Session("ExportTaskInputDtl"), "R", hdnStoreID.Value, hdnStoreIDValue.Value, hdnSeqNo.Value)
        Dim name(dtScDsrView.Columns.Count) As String
        Dim i As Integer = 0
        For Each column As DataColumn In dtScDsrView.Columns
            If column.ColumnName.EndsWith("_Orig") = True Then
                name(i) = column.ColumnName
                i += 1
            End If

        Next

        For Each ColName As String In name
            If ColName IsNot Nothing Then
                dtScDsrView.Columns.Contains(ColName)
                dtScDsrView.Columns.Remove(ColName)
            End If
        Next
        ExportCSV("Import_" & hdnStoreID.Value & "-" & lblTitle.Text & ".csv", dtScDsrView)
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

End Class