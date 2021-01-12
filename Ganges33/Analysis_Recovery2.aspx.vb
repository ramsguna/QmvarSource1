Imports System.Data.SqlClient
Public Class Analysis_Recovery2
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        If IsPostBack = False Then

            '***セッションなしログインユーザの対応***
            Dim userid As String = Session("user_id")
            If userid = "" Then
                Response.Redirect("Login.aspx")
            End If

            '***セッション取得***
            Dim FromUpDate As String = Session("From_UpDate")
            Dim ToUpDate As String = Session("To_UpDate")
            Dim uploadShipname As String = Session("upload_Shipname")
            Dim uploadFilename As String = Session("upload_Filename")

            '***表示***
            lblUploadData.Text = uploadFilename
            lblBranch.Text = uploadShipname
            lblFromdate.Text = FromUpDate
            lblTodate.Text = ToUpDate

            '***SQL文作成***
            Dim tblNo As Integer = Left(uploadFilename, 1)
            Dim tblName As String
            Dim branchKey As String

            'テーブル名指定
            Select Case tblNo
                Case 1
                    tblName = "SC_DSR"
                    branchKey = "AND Branch_name = '" & uploadShipname & "' AND ServiceOrder_No <> 'Total' "
                    'tblName = "SC_DSR_info"
                    'branchKey = "AND Branch_name = '" & uploadShipname & "' "
                Case 2
                    tblName = "Wty_Excel"
                    branchKey = "AND upload_Branch = '" & uploadShipname & "' "
                Case 3
                    tblName = "Invoice_update"
                    branchKey = "AND upload_Branch = '" & uploadShipname & "' AND number = 'C' "
                Case 4
                    tblName = "Invoice_update"
                    branchKey = "AND upload_Branch = '" & uploadShipname & "' AND number = 'EXC' "
                Case 5
                    tblName = "good_recived"
                    branchKey = "AND upload_Branch = '" & uploadShipname & "' "
                Case 6
                    tblName = "Billing_Info"
                    branchKey = "AND upload_Branch = '" & uploadShipname & "' "
            End Select

            Dim strSQL As String = ""

            'strSQL &= "SELECT CRTCD, left(upload_date,16) AS upload_date, Upload_FileName FROM dbo." & tblName & " WHERE DELFG = 0 "
            strSQL &= "SELECT CRTCD, Upload_FileName, COUNT(*) AS number FROM dbo." & tblName & " WHERE DELFG = 0 "
            strSQL &= branchKey

            If ToUpDate <> "" Then
                If FromUpDate <> "" Then
                    strSQL &= "AND LEFT(CONVERT(VARCHAR, upload_date,111),10) <= '" & ToUpDate & "' "
                    strSQL &= "AND LEFT(CONVERT(VARCHAR, upload_date,111),10) >= '" & FromUpDate & "' "
                Else
                    strSQL &= "AND LEFT(CONVERT(VARCHAR, upload_date,111),10) <= '" & ToUpDate & "' "
                End If
            Else
                If FromUpDate <> "" Then
                    strSQL &= "AND LEFT(CONVERT(VARCHAR, upload_date,111),10) >= '" & FromUpDate & "' "
                End If
            End If

            'strSQL &= "GROUP BY CRTCD, left(upload_date,16), Upload_FileName;"
            strSQL &= "GROUP BY CRTCD, Upload_FileName;"

            '***GRIDVIEWへ反映 ***
            If strSQL <> "" Then

                Dim errFlg As Integer

                Dim DT_SERCH As DataTable = DBCommon.ExecuteGetDT(strSQL, errFlg)

                If errFlg = 1 Then
                    Call showMsg("Failed to acquire search information.", "")
                    Exit Sub
                End If

                If DT_SERCH IsNot Nothing Then

                    grd_info.DataSource = DT_SERCH

                    grd_info.DataBind()

                    '次ページ反映ように取得しておく
                    Session("Data_DT_SERCH") = grd_info.DataSource

                    btnStart.Visible = False

                Else
                    grd_info.DataSource = Nothing

                    grd_info.DataBind()

                    Call showMsg("There was no relevant data.", "")

                    btnStart.Visible = False

                End If

            End If

        End If

    End Sub

    Protected Sub btnStart_Click(sender As Object, e As ImageClickEventArgs) Handles btnStart.Click

        'セッション情報取得
        Dim uploadShipname As String = Session("upload_Shipname")

        Dim uploadFilename As String = Session("upload_Filename")

        Dim userid As String = Session("user_id")

        Dim userName As String = Session("user_Name")

        If userid = "" Then
            Call showMsg("The session has expired. Please login again.", "")
            Exit Sub
        End If

        grd_info.DataSource = Session("Data_DT_SERCH")

        Dim delKey As String

        '削除対象のアップロードファイル名を取得
        '※チェックボックス追加のデザイナーズ画面での処理
        '⇒TemplateFieldを追加しGridViewのスマートタグでテンプレートの編集をクリック
        '表示されたスマートタグパネルにおけるItemTemplateにツールボックスからCheckBoxをドラッグして配置
        For Each row As GridViewRow In grd_info.Rows
            If CType(row.FindControl("CheckBox1"), CheckBox).Checked Then
                delKey &= "'" & row.Cells(2).Text & "',"
            End If
        Next

        If delKey = "" Then
            Call showMsg("Please check to be deleted.", "")
            Exit Sub
        End If

        '最後のカンマ外す
        delKey = Left(delKey, delKey.Length - 1)

        Dim clsSet As New Class_analysis
        Dim errFlg As Integer

        '削除処理
        Call clsSet.setRecovery(delKey, uploadShipname, errFlg, uploadFilename, userid, userName)

        If errFlg = 1 Then
            Call showMsg("Deletion processing failed.", "")
            Exit Sub
        Else

            Call showMsg("The deletion process is completed.", "")
        End If

    End Sub

    Private Sub grd_info_PageIndexChanging(sender As Object, e As GridViewPageEventArgs) Handles grd_info.PageIndexChanging

        grd_info.PageIndex = e.NewPageIndex
        grd_info.DataSource = Session("Data_DT_SERCH")
        grd_info.DataBind()

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

    Protected Sub btnBack_Click(sender As Object, e As ImageClickEventArgs) Handles btnBack.Click

        Response.Redirect("Analysis_Recovery.aspx")

    End Sub

End Class