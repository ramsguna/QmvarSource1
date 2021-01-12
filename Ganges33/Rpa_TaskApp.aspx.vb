Imports System.Security.Cryptography
Imports System.IO
Imports System.Text

Imports Ganges33.Ganges33.model
Imports Ganges33.Ganges33.logic

Public Class Rpa_TaskApp
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then

            '***セッションなしログインユーザの対応***
            Dim userid As String = Session("user_id")
            If userid = "" Then
                Response.Redirect("Login.aspx")
            End If

            BindGridview()
        End If
    End Sub

    Protected Sub btnUser_Click(sender As Object, e As EventArgs) Handles btnUser.Click
        divMailManagement.Visible = False
        divGspn.Visible = True

    End Sub
    Protected Sub gvDetails_PageIndexChanging(ByVal sender As Object, ByVal e As GridViewPageEventArgs)
        gvDetails.PageIndex = e.NewPageIndex
        BindGridview()
    End Sub

    Protected Sub BindGridview()
        ' Dim _RpaStatusModel As RpaStatusModel = New RpaStatusModel()
        Dim _RpaTaskAppControl As RpaTaskAppControl = New RpaTaskAppControl()


        Dim dtRpaTaskApp As DataTable = _RpaTaskAppControl.SelectRpaTaskApp()
        If (dtRpaTaskApp Is Nothing) Or (dtRpaTaskApp.Rows.Count = 0) Then
            gvDetails.DataSource = Nothing
            gvDetails.DataBind()
            gvDetails.Visible = False
            lblInfo.Text = "<font color=red>SSC is not defined</font>"
        Else
            gvDetails.DataSource = dtRpaTaskApp
            gvDetails.DataBind()
            gvDetails.Visible = True
            lblInfo.Text = ""
        End If


    End Sub

    Protected Sub btnUploadPrg_Click(sender As Object, e As EventArgs) Handles btnUploadPrg.Click

    End Sub

    Protected Sub btnUpDateGspnPwd_Click(sender As Object, e As EventArgs) Handles btnUpDateGspnPwd.Click
        'SJOhB6T0aHsvW3SmU/tlLA==
        'lblInfo.Text = Decrypt("SJOhB6T0aHsvW3SmU/tlLA==")


        Dim strSsc As String = ""
        lblInfo.Text = ""
        Dim chkSelect As Boolean = False
        Dim RpaClient As List(Of RpaClientUserModel) = New List(Of RpaClientUserModel)()
        Dim i As Integer = 0
        For Each gvrow As GridViewRow In gvDetails.Rows
            Dim checkbox = TryCast(gvrow.FindControl("chkChangePwd"), CheckBox)
            If checkbox.Checked Then
                chkSelect = True
                Dim Rpa As RpaClientUserModel = New RpaClientUserModel()
                Dim lblShipCode = TryCast(gvrow.FindControl("lblShipCode"), Label)
                Dim lblShipName = TryCast(gvrow.FindControl("lblShipName"), Label)
                Dim txtClientUsername = CType(gvDetails.Rows(i).FindControl("txtClientUsername"), TextBox)
                Dim txtPassword = CType(gvDetails.Rows(i).FindControl("txtPassword"), TextBox) 'TryCast(gvrow.FindControl("txtPassword"), TextBox)
                Rpa.ShipCode = lblShipCode.Text
                Rpa.ShipName = lblShipName.Text
                Rpa.GspnUserName = txtClientUsername.Text
                Rpa.GspnPwd = txtPassword.Text

                RpaClient.Add(Rpa)
            End If
            i = i + 1
        Next


        If chkSelect Then
            Dim _RpaStatusModel As RpaStatusModel = New RpaStatusModel()
            Dim _RpaTaskAppControl As RpaTaskAppControl = New RpaTaskAppControl()
            chkSelect = _RpaTaskAppControl.UpdateRpaClientUser(RpaClient)

            If chkSelect Then
                lblInfo.Text = "Successfully Updated..."
            End If
        End If
    End Sub

    Protected Sub btnMailManagement_Click(sender As Object, e As EventArgs) Handles btnMailManagement.Click
        divGspn.Visible = False
        divMailManagement.Visible = True
        BindData()
    End Sub


    Protected Sub gvRPAMail_RowCommand(ByVal sender As Object, ByVal e As GridViewCommandEventArgs)
        Dim _RpaEmailModel As RpaEmailModel = New RpaEmailModel()
        Dim _RpaEmailControl As RpaEmailControl = New RpaEmailControl()
        Dim dbTranStatus As Boolean = False
        Dim strSsc As String = ""

        If e.CommandName.Equals("ADD") Then

            'Update to Database
            Dim txtAddEmailType As TextBox = CType(gvRPAMail.FooterRow.FindControl("txtAddEmailType"), TextBox)
            Dim txtAddSmtp As TextBox = CType(gvRPAMail.FooterRow.FindControl("txtAddSmtp"), TextBox)
            Dim txtAddSmtpPort As TextBox = CType(gvRPAMail.FooterRow.FindControl("txtAddSmtpPort"), TextBox)
            Dim txtAddSmtpSslEnable As TextBox = CType(gvRPAMail.FooterRow.FindControl("txtAddSmtpSslEnable"), TextBox)
            Dim txtAddSmtpCredentialsUserName As TextBox = CType(gvRPAMail.FooterRow.FindControl("txtAddSmtpCredentialsUserName"), TextBox)
            Dim txtAddSmtpCredentialsUserPassword As TextBox = CType(gvRPAMail.FooterRow.FindControl("txtAddSmtpCredentialsUserPassword"), TextBox)
            Dim txtAddSender As TextBox = CType(gvRPAMail.FooterRow.FindControl("txtAddSender"), TextBox)
            Dim txtAddEmailTo As TextBox = CType(gvRPAMail.FooterRow.FindControl("txtAddEmailTo"), TextBox)
            Dim txtAddEmailCc As TextBox = CType(gvRPAMail.FooterRow.FindControl("txtAddEmailCc"), TextBox)
            Dim txtAddEmailBcc As TextBox = CType(gvRPAMail.FooterRow.FindControl("txtAddEmailBcc"), TextBox)
            Dim drpAddStatus As DropDownList = CType(gvRPAMail.FooterRow.FindControl("drpAddStatus"), DropDownList)

            Dim chkLstAddSsc As CheckBoxList = CType(gvRPAMail.FooterRow.FindControl("chkLstAddSsc"), CheckBoxList)
            Dim IsSelect As Boolean = False
            strSsc = ""
            For i As Integer = 0 To chkLstAddSsc.Items.Count - 1
                If chkLstAddSsc.Items(i).Selected Then
                    IsSelect = True
                    strSsc = strSsc & chkLstAddSsc.Items(i).Text & ","
                End If
            Next
            If IsSelect Then
                strSsc = Left(strSsc, Len(strSsc) - 1)
            Else
                Call showMsg("Please Select SSC!!! ", "")
                Exit Sub
            End If


            strSsc = UCase(Trim(strSsc))
            txtAddEmailType.Text = UCase(txtAddEmailType.Text)
            _RpaEmailModel.EmailType = txtAddEmailType.Text
            Dim dtEmail As DataTable = _RpaEmailControl.SelectRpaEmail(_RpaEmailModel)
            If dtEmail.Rows.Count > 0 Then
                Call showMsg(txtAddEmailType.Text & " Email Type Is Already Exist!!! ", "")
                Exit Sub
            End If
            If (Trim(txtAddEmailType.Text <> "")) Then

                'New Task to Database
                Dim userid As String = Session("user_id")
                _RpaEmailModel.UserId = userid
                _RpaEmailModel.UPDPG = "Rpa_TaskApp.aspx"
                _RpaEmailModel.EmailType = txtAddEmailType.Text
                _RpaEmailModel.Source = strSsc
                _RpaEmailModel.Smtp = txtAddSmtp.Text
                _RpaEmailModel.SmtpPort = txtAddSmtpPort.Text
                _RpaEmailModel.SmtpSslEnable = txtAddSmtpSslEnable.Text
                _RpaEmailModel.SmtpCredentialsUserName = txtAddSmtpCredentialsUserName.Text
                _RpaEmailModel.SmtpCredentialsUserPassword = txtAddSmtpCredentialsUserPassword.Text
                _RpaEmailModel.Sender = txtAddSender.Text
                _RpaEmailModel.EmailTo = txtAddEmailTo.Text
                _RpaEmailModel.EmailCc = txtAddEmailCc.Text
                _RpaEmailModel.EmailBcc = txtAddEmailBcc.Text
                _RpaEmailModel.Status = drpAddStatus.SelectedItem.Text
                _RpaEmailModel.IpAddress = System.Web.HttpContext.Current.Request.UserHostAddress
                dbTranStatus = _RpaEmailControl.AddEmail(_RpaEmailModel)


                BindData()
            End If




        End If
    End Sub

    Protected Sub BindData()
        Dim _RpaEmailModel As RpaEmailModel = New RpaEmailModel()
        Dim _RpaEmailControl As RpaEmailControl = New RpaEmailControl()
        Dim FromTable As DataTable = _RpaEmailControl.SelectRpaEmail(_RpaEmailModel)
        If FromTable.Rows.Count > 0 Then

            gvRPAMail.DataSource = FromTable
            gvRPAMail.DataBind()
            'Add New Task - ByDefault Select All The Checkbox
            Dim chkLstAddSsc As CheckBoxList = CType(gvRPAMail.FooterRow.FindControl("chkLstAddSsc"), CheckBoxList)
            For Each item As ListItem In chkLstAddSsc.Items
                item.Selected = True
            Next

            Dim strSource As String = ""
            Dim text As String = ""

            For Each gvr As GridViewRow In gvRPAMail.Rows
                Dim lblSource As Label = gvr.FindControl("lblSource")
                If lblSource Is Nothing Then 'If Edit Mode
                    Dim txtEditSource As TextBox = gvr.FindControl("txtEditSource")
                    strSource = txtEditSource.Text
                    strSource = strSource & ","
                    Dim chkLstSsc As CheckBoxList = gvr.FindControl("chkLstEditSsc")
                    For i As Integer = 0 To chkLstSsc.Items.Count - 1
                        If strSource.Contains(chkLstSsc.Items(i).Text & ",") Then
                            chkLstSsc.Items(i).Selected = True
                        End If
                    Next
                    Dim txtEditStatus As TextBox = gvr.FindControl("txtEditStatus")
                    Dim drpEditStatus As DropDownList = gvr.FindControl("drpEditStatus")
                    text = txtEditStatus.Text
                    Dim item = drpEditStatus.Items.FindByText(text)
                    If item IsNot Nothing Then item.Selected = True

                Else 'If page load
                    strSource = lblSource.Text
                    strSource = strSource & ","
                    Dim strSrcScc As String = ""
                    Dim chkLstSsc As CheckBoxList = gvr.FindControl("chkLstSsc")
                    For i As Integer = 0 To chkLstSsc.Items.Count - 1
                        chkLstSsc.Items(i).Enabled = False
                        strSrcScc = chkLstSsc.Items(i).Text & ","
                        If strSource.Contains(strSrcScc) Then
                            chkLstSsc.Items(i).Selected = True
                        End If
                    Next
                    'For Task Name Dropdownlist
                    Dim lblStatus As Label = gvr.FindControl("lblStatus")
                    Dim drpStatus As DropDownList = gvr.FindControl("drpStatus")
                    text = lblStatus.Text
                    Dim item = drpStatus.Items.FindByText(text)
                    If item IsNot Nothing Then item.Selected = True


                End If
            Next
        Else
            FromTable.Rows.Add(FromTable.NewRow())
            gvRPAMail.DataSource = FromTable
            gvRPAMail.DataBind()
            Dim TotalColumns As Integer = gvRPAMail.Rows(0).Cells.Count
            gvRPAMail.Rows(0).Cells.Clear()
            gvRPAMail.Rows(0).Cells.Add(New TableCell())
            gvRPAMail.Rows(0).Cells(0).ColumnSpan = TotalColumns
            gvRPAMail.Rows(0).Cells(0).Text = "No records Found"

            Dim chkLstAddSsc As CheckBoxList = CType(gvRPAMail.FooterRow.FindControl("chkLstAddSsc"), CheckBoxList)
            For Each item As ListItem In chkLstAddSsc.Items
                item.Selected = True
            Next
        End If
    End Sub
    Protected Sub gvRPAMail_RowUpdating(ByVal sender As Object, ByVal e As GridViewUpdateEventArgs)
        'Variable Declartion


        Dim lblEditEmailId As Label = CType(gvRPAMail.Rows(e.RowIndex).FindControl("lblEditEmailId"), Label)
        Dim txtEditEmailType As TextBox = CType(gvRPAMail.Rows(e.RowIndex).FindControl("txtEditEmailType"), TextBox)
        Dim txtEditSmtp As TextBox = CType(gvRPAMail.Rows(e.RowIndex).FindControl("txtEditSmtp"), TextBox)
        Dim txtEditSmtpPort As TextBox = CType(gvRPAMail.Rows(e.RowIndex).FindControl("txtEditSmtpPort"), TextBox)
        Dim txtEditSmtpSslEnable As TextBox = CType(gvRPAMail.Rows(e.RowIndex).FindControl("txtEditSmtpSslEnable"), TextBox)
        Dim txtEditSmtpCredentialsUserName As TextBox = CType(gvRPAMail.Rows(e.RowIndex).FindControl("txtEditSmtpCredentialsUserName"), TextBox)
        Dim txtEditSmtpCredentialsUserPassword As TextBox = CType(gvRPAMail.Rows(e.RowIndex).FindControl("txtEditSmtpCredentialsUserPassword"), TextBox)
        Dim txtEditSender As TextBox = CType(gvRPAMail.Rows(e.RowIndex).FindControl("txtEditSender"), TextBox)
        Dim txtEditEmailTo As TextBox = CType(gvRPAMail.Rows(e.RowIndex).FindControl("txtEditEmailTo"), TextBox)
        Dim txtEditEmailCc As TextBox = CType(gvRPAMail.Rows(e.RowIndex).FindControl("txtEditEmailCc"), TextBox)
        Dim txtEditEmailBcc As TextBox = CType(gvRPAMail.Rows(e.RowIndex).FindControl("txtEditEmailBcc"), TextBox)

        Dim drpEditStatus As DropDownList = CType(gvRPAMail.Rows(e.RowIndex).FindControl("drpEditStatus"), DropDownList)

        Dim strSsc As String = ""

        Dim chkLstEditSsc As CheckBoxList = CType(gvRPAMail.Rows(e.RowIndex).FindControl("chkLstEditSsc"), CheckBoxList)


        Dim IsSelect As Boolean = False
        strSsc = ""
        For i As Integer = 0 To chkLstEditSsc.Items.Count - 1
            If chkLstEditSsc.Items(i).Selected Then
                IsSelect = True
                strSsc = strSsc & chkLstEditSsc.Items(i).Text & ","
            End If
        Next
        If IsSelect Then
            strSsc = Left(strSsc, Len(strSsc) - 1)
        Else

            Exit Sub
        End If


        strSsc = UCase(Trim(strSsc))
        txtEditEmailType.Text = UCase(txtEditEmailType.Text)

        If (Trim(txtEditEmailType.Text <> "")) Then
            'Initialize
            Dim _RpaEmailModel As RpaEmailModel = New RpaEmailModel()
            Dim _RpaEmailControl As RpaEmailControl = New RpaEmailControl()
            Dim dbTranStatus As Boolean = False
            'Update to Database
            'New Task to Database
            Dim userid As String = Session("user_id")
            _RpaEmailModel.UserId = userid
            _RpaEmailModel.EmailId = lblEditEmailId.Text
            _RpaEmailModel.EmailType = txtEditEmailType.Text
            _RpaEmailModel.Smtp = txtEditSmtp.Text
            _RpaEmailModel.SmtpPort = txtEditSmtpPort.Text
            _RpaEmailModel.SmtpSslEnable = txtEditSmtpSslEnable.Text
            _RpaEmailModel.SmtpCredentialsUserName = txtEditSmtpCredentialsUserName.Text
            _RpaEmailModel.SmtpCredentialsUserPassword = txtEditSmtpCredentialsUserPassword.Text
            _RpaEmailModel.Sender = txtEditSender.Text
            _RpaEmailModel.EmailTo = txtEditEmailTo.Text
            _RpaEmailModel.EmailCc = txtEditEmailCc.Text
            _RpaEmailModel.EmailBcc = txtEditEmailBcc.Text
            _RpaEmailModel.Source = strSsc
            _RpaEmailModel.Status = drpEditStatus.SelectedItem.Text
            'Update Start /end Status to db
            dbTranStatus = _RpaEmailControl.RpaEmailUpdate(_RpaEmailModel)


        End If

        gvRPAMail.EditIndex = -1
        BindData()
    End Sub
    Protected Sub gvRPAMail_RowCancelingEdit(ByVal sender As Object, ByVal e As GridViewCancelEditEventArgs)
        gvRPAMail.EditIndex = -1
        BindData()
    End Sub

    Protected Sub gvRPAMail_RowEditing(ByVal sender As Object, ByVal e As GridViewEditEventArgs)
        gvRPAMail.EditIndex = e.NewEditIndex
        BindData()
    End Sub
    Public Function SelectSSC() As DataTable
        Dim _RpaTaskAppControl As RpaTaskAppControl = New RpaTaskAppControl()
        Dim dtTaskApp As DataTable = _RpaTaskAppControl.SelectRpaTaskApp()
        Return dtTaskApp
    End Function
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

    Protected Sub gvRPAMail_RowDeleting(ByVal sender As Object, ByVal e As GridViewDeleteEventArgs)
        Dim lblEmailId As Label = CType(gvRPAMail.Rows(e.RowIndex).FindControl("lblEmailId"), Label)
        Dim _RpaEmailModel As RpaEmailModel = New RpaEmailModel()
        Dim _RpaEmailControl As RpaEmailControl = New RpaEmailControl()
        Dim dbTranStatus As Boolean = False
        'Update to Database
        _RpaEmailModel.EmailId = lblEmailId.Text
        'Update Start /end Status to db
        dbTranStatus = _RpaEmailControl.RpaEmailDelete(_RpaEmailModel)
        BindData()
    End Sub
End Class