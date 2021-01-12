Public Class Analysis_Refresh
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        If IsPostBack = False Then

            '***セッションなしログインユーザの対応***
            Dim userid As String = Session("user_id")
            If userid = "" Then
                Response.Redirect("Login.aspx")
            End If

            '***ログインユーザ情報表示***
            Dim shipName As String = Session("ship_Name")
            Dim userName As String = Session("user_Name")
            Dim userLevel As String = Session("user_level")
            Dim adminFlg As Boolean = Session("admin_Flg")

            lblLoc.Text = shipName
            lblName.Text = userName

            Dim i As Integer
            '***拠点名称のリストの設定***
            DropListLocation.Items.Clear()
            If userLevel = "0" Or userLevel = "1" Or userLevel = "2" Or adminFlg = True Then
                Dim shipNameAll() As String
                If Session("ship_name_list") IsNot Nothing Then
                    shipNameAll = Session("ship_name_list")
                    With DropListLocation
                        .Items.Add("Select shipname")
                        For i = 0 To UBound(shipNameAll)
                            If Trim(shipNameAll(i)) <> "" Then
                                .Items.Add(shipNameAll(i))
                            End If
                        Next i
                    End With
                End If
                shipName = ""
            Else
                DropListLocation.Items.Add(shipName)
            End If

            '***monthリストの設定***
            DropDownMonth.Items.Clear()
            With DropDownMonth
                .Items.Add("Select the month")
                .Items.Add("January")
                .Items.Add("February")
                .Items.Add("March")
                .Items.Add("April")
                .Items.Add("May")
                .Items.Add("June")
                .Items.Add("July")
                .Items.Add("August")
                .Items.Add("September")
                .Items.Add("October")
                .Items.Add("November")
                .Items.Add("December")
            End With

            '***exportFileリストの設定***
            DropDownTargetData.Items.Clear()
            With DropDownTargetData
                .Items.Add("Select data to delete")
                .Items.Add("1.DailyStatement")
                .Items.Add("2.WarrantyExcelFile")
                '          .Items.Add("3.BillingInfo")
            End With

            '***削除履歴の表示***
            Call showDelData()

        Else

            Dim BtnCancelChk As String = ""
            Dim BtnOK2Chk As String = ""
            Dim errFlg As Integer

            '***どのボタンが押下されたか確認***
            '削除処理キャンセルボタン
            For Each s In Context.Request.Form.AllKeys
                If s.Contains("BtnCancel") Then
                    BtnCancelChk = "BtnCancelOn"
                    Exit For
                End If
            Next s

            '削除処理OKボタン
            For Each s In Context.Request.Form.AllKeys
                If s.Contains("Btn2OK") Then
                    BtnOK2Chk = "BtnOKOn2"
                    Exit For
                End If
            Next s

            '***削除処理確認ダイアログでキャンセルボタン押下処理***
            If (BtnCancelChk = "BtnCancelOn") Then
                Call showMsg("The process has been aborted.", "")
            End If

            '***削除処理***
            If (BtnOK2Chk = "BtnOKOn2") Then
                Call deleteSyori()
            End If

            '***セッション設定***
            '月を指定
            Dim setMon As String = DropDownMonth.SelectedIndex.ToString("00")
            Dim setMonName As String = DropDownMonth.Text

            Session("set_Mon2") = setMon
            Session("set_MonName") = setMonName

            '拠点を指定
            Dim targetShipName As String = DropListLocation.Text
            Session("target_ShipName") = targetShipName

            'ダウンロードファイル種類を指定
            Dim targetDelData As String = DropDownTargetData.Text
            Session("target_DelData") = targetDelData

        End If

    End Sub

    Protected Sub btnSend_Click(sender As Object, e As EventArgs) Handles btnSend.Click

        '***セッション情報取得（リストで選択された情報）***
        Dim targetShipName As String = Session("target_ShipName")
        Dim targetDelData As String = Session("target_DelData")
        Dim setMon As String = Session("set_Mon2")

        '***入力チェック***
        If targetShipName = "Select shipname" Then
            Call showMsg("Please specify the branch name", "")
            Exit Sub
        End If

        Dim dt As DateTime
        Dim dateFrom As String
        Dim dateTo As String

        If TextDateFrom.Text <> "" Then
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
            If DateTime.TryParse(TextDateTo.Text, dt) Then
                dateTo = DateTime.Parse(Trim(TextDateTo.Text)).ToShortDateString
            Else
                Call showMsg("The date specification of To is incorrect.", "")
                Exit Sub
            End If
        Else
            dateTo = ""
        End If

        If setMon = "00" Then
            '月の指定なし、日付の指定あること
            If dateFrom = "" And dateTo = "" Then
                Call showMsg("Please specify either output period in month or date (FROM / TO)", "")
                Exit Sub
            Else
                'FROM,TOの両方に日付指定があること
                If dateFrom <> "" And dateTo <> "" Then
                Else
                    Call showMsg("Please specify the date From and To", "")
                    Exit Sub
                End If
            End If
        Else
            '月の指定あり、日付の指定はないこと
            If dateFrom <> "" Or dateTo <> "" Then
                Call showMsg("Please specify either output period in month or date (FROM / TO)", "")
                Exit Sub
            End If
        End If

        'From,Toの指定が逆
        If dateFrom > dateTo Then
            Call showMsg("The specifications of From and To are opposite.", "")
            Exit Sub
        End If

        If targetDelData = "Select data to delete" Then
            Call showMsg("Please specify the data to be deleted.", "")
            Exit Sub
        End If

        '***セッション設定***
        Session("date_To") = dateTo
        Session("date_From") = dateFrom

        '***メッセージ設定***
        If setMon <> "00" Then
            Call showMsg(targetShipName & "<br/>" & targetDelData & "<br/> Mon: " & setMon & "<br/>" & " Are you sure you want to delete ", "CancelMsg2")
        Else
            If dateFrom <> "" And dateTo <> "" Then
                Call showMsg(targetShipName & "<br/>" & targetDelData & "<br/>" & dateFrom & " ～ " & dateTo & "<br/>" & " Are you sure you want to delete ", "CancelMsg2")
            End If
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

    Protected Sub Btn2OK_Click(sender As Object, e As EventArgs) Handles Btn2OK.Click

    End Sub

    Protected Sub BtnCancel_Click(sender As Object, e As EventArgs) Handles BtnCancel.Click

    End Sub
    '削除処理
    Protected Sub deleteSyori()

        '***セッション情報取得***
        Dim userid As String = Session("user_id")
        Dim userName As String = Session("user_Name")
        Dim userLevel As String = Session("user_level")
        Dim adminFlg As Boolean = Session("admin_Flg")

        Dim targetShipName As String = Session("target_ShipName")
        Dim targetDelData As String = Session("target_DelData")
        Dim setMon As String = Session("set_Mon2")
        Dim setMonName As String = Session("set_MonName")

        Dim dateTo As String = Session("date_To")
        Dim dateFrom As String = Session("date_From")

        If userid Is Nothing Then
            Call showMsg("The session has expired. Please login again.", "")
            Exit Sub
        End If

        '***拠点コード取得***
        Dim clsSetCommon As New Class_common
        Dim setShipCode As String
        Dim errMsg As String

        Call clsSetCommon.setShipCode(targetShipName, setShipCode, errMsg)

        If errMsg <> "" Then
            Call showMsg(errMsg, "")
            Exit Sub
        End If

        '***削除処理***
        Dim clsSet As New Class_analysis
        Dim tourokuFlg As Integer
        Dim errFlg As Integer

        clsSet.setRefresh(setMon, dateTo, dateFrom, targetShipName, targetDelData, userid, userName, setShipCode, errFlg, tourokuFlg)

        If errFlg = 1 Then
            Call showMsg("Deletion processing failed.", "")
            Exit Sub
        End If

        If tourokuFlg = 1 Then
            Call showMsg("The deletion process is completed.", "")
        Else
            Call showMsg("There was no subject for deletion.", "")
            Exit Sub
        End If

        '***削除履歴の表示***
        Call showDelData()

    End Sub
    '削除履歴の表示
    Protected Sub showDelData()

        Dim userLevel As String = Session("user_level")
        Dim adminFlg As Boolean = Session("admin_Flg")
        Dim shipName As String = Session("ship_Name")

        Dim delLogData() As Class_analysis.DELETE_LOG
        Dim clsSet As New Class_analysis
        Dim errFlg As Integer

        If userLevel = "0" Or userLevel = "1" Or userLevel = "2" Or adminFlg = True Then
            shipName = ""
        End If

        Call clsSet.set_deleteLog(delLogData, shipName, errFlg)

        If errFlg = 1 Then
            Call showMsg("Failed to display history of deleted data.", "")
            Exit Sub
        End If

        If delLogData IsNot Nothing Then
            ListHistory.Items.Clear()
            For i = 0 To delLogData.Length - 1
                ListHistory.Items.Add(delLogData(i).del_user & ",　" & delLogData(i).del_datetime & ",　" & delLogData(i).del_location & ",　" & delLogData(i).del_data)
            Next i
        End If

    End Sub

End Class