Imports System.IO
Imports System.Text
Public Class Repair_Tools
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        If IsPostBack = False Then

            '***セッションなしログインユーザの対応***
            Dim userid As String = Session("user_id")
            If userid = "" Then
                Response.Redirect("Login.aspx")
            End If

            '***リストの設定***
            DropDownPartsRegistration.Items.Clear()
            With DropDownPartsRegistration
                .Items.Add("Select Parts Registration")
                .Items.Add("Create")
                .Items.Add("Modify")
                .Items.Add("List Dwonload")
            End With

        Else

            '***セッション設定***
            Dim partsRegistration As String = DropDownPartsRegistration.Text
            Session("parts_Registration") = partsRegistration

        End If

    End Sub

    Protected Sub btnStart2_Click(sender As Object, e As ImageClickEventArgs) Handles btnStart2.Click

        '***セッション情報取得***
        Dim partsRegistration As String = Session("parts_Registration")
        Dim shipCode As String = Session("ship_code")
        Dim userName As String = Session("user_Name")

        '***チェック***
        If partsRegistration = "Select Parts Registration" Then
            Call showMsg("Please select a list.", "")
            Exit Sub
        End If

        '***新規/更新処理***
        If partsRegistration = "Create" Or partsRegistration = "Modify" Then
            Response.Redirect("Repair_Parts_Registration_Create.aspx")
        End If

        '***DL処理***
        If partsRegistration = "List Dwonload" Then

            Dim partsMasterData() As Class_repair.M_PARTS
            Dim clsSet As New Class_repair
            Dim clsSetCommon As New Class_common
            Dim errFlg As Integer
            Dim dtNow As DateTime = clsSetCommon.dtIndia

            clsSet.get_M_PARTS(partsMasterData, shipCode, errFlg)

            If errFlg = 1 Then
                Call showMsg("Failed to get information on parts master.", "")
                Exit Sub
            End If

            Try
                'ファイル名、パスの設定
                Dim csvFileName As String = "M_PARTS" & ".csv"
                Dim outputPath As String = clsSet.ExportCsvFilePass & csvFileName

                '項目名設定
                Dim rowWork As String = "Parts List Download " & dtNow.ToString & " download user " & userName
                Dim csvContents = New List(Of String)(New String() {rowWork})

                Dim rowWork1 As String = "CRTDT,CRTCD,UPDDT,UPDCD,UPDPG,DELFG,maker,parts_no,parts_name,loc_1,loc_2,loc_3,unit_price,first_prc,end_prc,ship_code,eos_code,category,g_unit_price,comoensation,techfee_paid,techfee_guarantee,unit_flg,product_name,Assing_type,parts_flg"
                csvContents.Add(rowWork1)

                For i = 0 To partsMasterData.Length - 1

                    Dim unit_flg As Boolean

                    If partsMasterData(i).unit_flg = 0 Then
                        unit_flg = False
                    Else
                        unit_flg = True
                    End If

                    Dim parts_flg As Boolean

                    If partsMasterData(i).parts_flg = 0 Then
                        parts_flg = False
                    Else
                        parts_flg = True
                    End If

                    csvContents.Add(partsMasterData(i).CRTDT & "," & partsMasterData(i).CRTCD & "," & partsMasterData(i).UPDDT & "," & partsMasterData(i).UPDCD & "," & partsMasterData(i).UPDPG & "," & partsMasterData(i).DELFG & ",""" & partsMasterData(i).maker & """,""" & partsMasterData(i).parts_no & """,""" & partsMasterData(i).parts_name & """,""" & partsMasterData(i).loc_1 & """,""" & partsMasterData(i).loc_2 & """,""" & partsMasterData(i).loc_3 & """,""" & partsMasterData(i).unit_price & """,""" & partsMasterData(i).first_prc & """,""" & partsMasterData(i).end_prc & """,""" & partsMasterData(i).ship_code & """,""" & partsMasterData(i).eos_code & """,""" & partsMasterData(i).category & """,""" & partsMasterData(i).g_unit_price & """,""" & partsMasterData(i).comoensation & """,""" & partsMasterData(i).techfee_paid & """,""" & partsMasterData(i).techfee_guarantee & """,""" & unit_flg & """,""" & partsMasterData(i).product_name & """,""" & partsMasterData(i).Assing_type & """,""" & parts_flg & """,")

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

        End If

    End Sub

    Protected Sub showMsg(ByVal Msg As String, ByVal msgChk As String)

        lblMsg.Text = Msg
        Dim sScript As String

        If msgChk = "CancelMsg" Then
            'OKとキャンセルボタン
            sScript = "$(function () {$(""#dialog"" ).dialog({width: 400,buttons:{""OK"": function () {$(this).dialog('close');$('[id$=""BtnOK""]').click();},""CANCEL"": function () {$(this).dialog('close');$('[id$=""BtnCancel""]').click();}}});});"
        ElseIf msgChk = "CancelMsg2" Then
            'OKとキャンセルボタン 
            sScript = "$(function () {$(""#dialog"" ).dialog({width: 400,buttons:{""OK"": function () {$(this).dialog('close');$('[id$=""Btn2OK""]').click();},""CANCEL"": function () {$(this).dialog('close');$('[id$=""BtnCancel""]').click();}}});});"
        Else
            'OKボタンのみ
            sScript = "$(function () { $( ""#dialog"" ).dialog({width: 400, buttons: {""OK"": function () {$(this).dialog('close');}}});});"
        End If

        'JavaScriptの埋め込み
        ClientScript.RegisterStartupScript(Me.GetType(), "startup", sScript, True)

    End Sub

End Class