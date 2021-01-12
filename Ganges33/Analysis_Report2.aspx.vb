Imports System.IO
Imports System.Text
Public Class Analysis_Report2
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        '***初期処理***
        If IsPostBack = False Then

            '***セッションなしログインユーザの対応***
            Dim userid As String = Session("user_id")
            If userid = "" Then
                Response.Redirect("Login.aspx")
            End If

            '***セッション取得***
            Dim shipCode As String = Session("ship_code")
            Dim setMon As String = Session("set_Mon")
            Dim setYear As String = Session("set_Year")
            ' Session("set_Year")

            '***表示の設定***
            '本日月
            lblMonNow.Text = (Convert.ToInt32(setMon) + 1).ToString("00")

            '***GRIDVIEWへ反映 ***
            '対象年月の設定
            Dim clsSetCommon As New Class_common
            Dim dtNow As DateTime = clsSetCommon.dtIndia
            'Comment by Mohan 
            'Dim monDate As String = Left(dtNow.ToShortDateString, 4) & "/" & Trim(lblMonNow.Text)
            Dim monDate As String = setYear & "/" & Trim(lblMonNow.Text)

            Dim strSQL As String = "SET LANGUAGE us_english;SELECT day + '  ' +   datename(weekday,(convert(datetime, month + '/' + day)) ) as day, Customer_Visit, Call_Registerd, Repair_Completed,Goods_Delivered, Pending_Calls, Cancelled_Calls "
            strSQL &= "FROM dbo.Activity_report WHERE DELFG = 0 AND Month = '" & monDate & "' AND location = '" & shipCode & "' "
            strSQL &= "ORDER BY day;"

            If strSQL <> "" Then
                Dim errFlg As Integer
                Dim DT_SERCH As DataTable = DBCommon.ExecuteGetDT(strSQL, errFlg)

                If errFlg = 1 Then
                    Call showMsg("Failed to acquire search information.")
                    Exit Sub
                End If

                If DT_SERCH IsNot Nothing Then
                    GridInfo.DataSource = DT_SERCH
                    GridInfo.DataBind()
                    '次ページ反映ように取得しておく
                    Session("Data_DT_SERCH") = DT_SERCH
                    btnDown.Visible = True
                Else
                    GridInfo.DataSource = Nothing
                    GridInfo.DataBind()
                    btnDown.Visible = False
                End If

            End If

        End If

    End Sub

    Protected Sub btnBack_Click(sender As Object, e As EventArgs) Handles btnBack.Click

        Response.Redirect("Analysis_Report.aspx")

    End Sub

    Protected Sub showMsg(ByVal Msg As String)

        lblMsg.Text = Msg
        Dim sScript As String = "$(function () { $( ""#dialog"" ).dialog({width: 400, buttons: {""OK"": function () {$(this).dialog('close');}}});});"
        'JavaScriptの埋め込み
        ClientScript.RegisterStartupScript(Me.GetType(), "startup", sScript, True)

    End Sub
    'DLボタン押下処理
    Protected Sub btnDown_Click(sender As Object, e As EventArgs) Handles btnDown.Click

        '***セッション取得***
        Dim adminFlg As Boolean = Session("admin_Flg")
        Dim userLevel As String = Session("user_level")
        Dim setShipname As String = Session("ship_Name")
        Dim shipCode As String = Session("ship_code")
        Dim setMon As String = Session("set_Mon")
        Dim setYear As String = Session("set_Year")

        '***DL処理***
        Dim errFlg As Integer
        Dim clsSet As New Class_analysis
        Dim dsActivity_report As New DataSet
        Dim activityReportData() As Class_analysis.ACTIVITY_REPORT
        Dim activityReportDataSum As Class_analysis.ACTIVITY_REPORT

        '**データ取得**
        '対象年月の設定
        Dim clsSetCommon As New Class_common
        Dim dtNow As DateTime = clsSetCommon.dtIndia
        setMon = (Convert.ToInt32(setMon) + 1).ToString("00")
        'Comment by Mohan
        'Dim monDate As String = Left(dtNow.ToShortDateString, 4) & "/" & setMon
        Dim monDate As String = setYear & "/" & setMon

        Call clsSet.exportDataActivityReport(dsActivity_report, activityReportData, activityReportDataSum, monDate, shipCode, errFlg)

        If errFlg = 1 Then
            Call showMsg("Acquisition of DL data failed")
            Exit Sub
        End If

        If dsActivity_report Is Nothing Then
            Call showMsg("There is no DL data. Cancel processing.")
            Exit Sub
        Else

            Try
                'ファイル名設定
                Dim csvFileName As String = "Activity_report_" & setMon & ".csv"
                Dim outputPath As String = clsSet.CsvFilePass & csvFileName

                '項目名設定
                Dim rowWork1 As String = setShipname & " Result"

                Dim csvContents = New List(Of String)(New String() {rowWork1})

                Dim rowWork2 As String = "output date," & dtNow.ToShortDateString

                Dim rowWork3 As String = "Customer Visit," & activityReportDataSum.Customer_Visit

                Dim rowWork4 As String = "Repair Completed," & activityReportDataSum.Repair_Completed

                '一覧の項目
                Dim rowWork5 As String = "Branch,Date,Customer Visit,Call Registered,Repair Completed,Goods Delivered,Pending Calls,Cancelled Calls,note"

                csvContents.Add(rowWork2)
                csvContents.Add(rowWork3)
                csvContents.Add(rowWork4)
                csvContents.Add(rowWork5)

                '一覧の設定
                For i = 0 To activityReportData.Length - 1
                    csvContents.Add(activityReportData(i).location & "," & activityReportData(i).month & "/" & activityReportData(i).day & "," & activityReportData(i).Customer_Visit & "," & activityReportData(i).Call_Registerd & "," & activityReportData(i).Repair_Completed & "," & activityReportData(i).Goods_Delivered & "," & activityReportData(i).Pending_Calls & "," & activityReportData(i).Cancelled_Calls & "," & activityReportData(i).note)
                Next i

                Dim rowWork6 As String
                Dim rowWork7 As String = ",," & activityReportDataSum.Customer_Visit & "," & activityReportDataSum.Call_Registerd & "," & activityReportDataSum.Repair_Completed & "," & activityReportDataSum.Goods_Delivered & "," & activityReportDataSum.Pending_Calls & "," & activityReportDataSum.Cancelled_Calls

                csvContents.Add(rowWork6)
                csvContents.Add(rowWork7)

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
                Call showMsg("Failed  to download prcess.")
            End Try

        End If

    End Sub

End Class