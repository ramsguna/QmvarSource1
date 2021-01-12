Public Class Analysis_Recovery
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        If IsPostBack = False Then

            '***セッションなしログインユーザの対応***
            Dim userid As String = Session("user_id")
            If userid = "" Then
                Response.Redirect("Login.aspx")
            End If

            Dim userLevel As String = Session("user_level")
            Dim adminFlg As Boolean = Session("admin_Flg")

            '***拠点名称の設定***
            DropListLocation.Items.Clear()

            If userLevel = "0" Or userLevel = "1" Or userLevel = "2" Or adminFlg = True Then
                Dim shipName() As String
                If Session("ship_name_list") IsNot Nothing Then
                    shipName = Session("ship_name_list")
                    With DropListLocation
                        .Items.Add("Select shipname")
                        For i = 0 To UBound(shipName)
                            If Trim(shipName(i)) <> "" Then
                                .Items.Add(shipName(i))
                            End If
                        Next i
                    End With
                End If
            Else
                Call showMsg("The user doesn't have permission to access...", "")
                Exit Sub
            End If

            '***アップロードファイル名の設定***
            DropListUploadFile.Items.Clear()

            With DropListUploadFile
                .Items.Add("Select upload Filename")
                .Items.Add("1.DailyStatementReport")
                .Items.Add("2.Warranty Excel File")
                .Items.Add("3.Invoice Update C")
                .Items.Add("4.Invoice Update EXC")
                .Items.Add("5.Good Recived")
                .Items.Add("6.Billing Info")
            End With

        Else

            '***セッション設定***
            Session("upload_Shipname") = DropListLocation.Text
            Session("upload_Filename") = DropListUploadFile.Text

        End If

    End Sub

    Protected Sub btnSearch_Click(sender As Object, e As ImageClickEventArgs) Handles btnSearch.Click

        '***セッション情報取得***
        Dim userid As String = Session("user_id")

        If userid Is Nothing Then
            Call showMsg("The session has expired. Please login again.", "")
            Exit Sub
        End If

        Dim uploadShipname As String = Session("upload_Shipname")

        Dim uploadFilename As String = Session("upload_Filename")

        '日付チェック
        Dim dt As DateTime
        Dim FromUpDate As String
        If TextFromUpDate.Text <> "" Then
            If DateTime.TryParse(TextFromUpDate.Text, dt) Then
                FromUpDate = DateTime.Parse(Trim(TextFromUpDate.Text)).ToShortDateString
            Else
                Call showMsg("There is an error in the date specification ", "")
                Exit Sub
            End If
        Else
            FromUpDate = ""
        End If

        Dim ToUpDate As String
        If TextToUpDate.Text <> "" Then
            If DateTime.TryParse(TextToUpDate.Text, dt) Then
                ToUpDate = DateTime.Parse(Trim(TextToUpDate.Text)).ToShortDateString
            Else
                Call showMsg("There is an error in the date specification", "")
                Exit Sub
            End If
        Else
            ToUpDate = ""
        End If

        If ToUpDate <> "" And FromUpDate <> "" Then
            If ToUpDate < FromUpDate Then
                Call showMsg("The TO and FROM date specifications are reversed.", "")
                Exit Sub
            End If
        End If

        If ToUpDate = "" And FromUpDate = "" Then
            Call showMsg("Please specify upload period.", "")
            Exit Sub
        End If

        If uploadFilename = "Select upload Filename" Then
            Call showMsg("Please specify the upload file name", "")
            Exit Sub
        End If

        If uploadShipname = "Select shipname" Then
            Call showMsg("Please specify the branch name", "")
            Exit Sub
        End If

        '***セッション設定***
        Session("From_UpDate") = FromUpDate
        Session("To_UpDate") = ToUpDate

        If Session("upload_Shipname") IsNot Nothing Then
            Response.Redirect("Analysis_Recovery2.aspx")
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

End Class