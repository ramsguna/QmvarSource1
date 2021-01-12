Public Class Msg_inv_Arrival
    Inherits System.Web.UI.Page
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        'Dim pdfFileName As String = Session("pdf_FileName")
        Dim arrivalNo As String = Session("arrival_No")
        Dim allKindItem As Integer = Session("all_Kinditem")
        Dim allQty As Integer = Session("all_Qty")
        Dim errFlg As Integer = Session("err_Flg")
        Dim msgString As String = ""
        Dim msgString2 As String = ""
        Dim errorItem() As Class_inv.ERROR_Item
        If Session("error_Item") IsNot Nothing Then
            errorItem = Session("error_Item")
        End If

        lblMsg.Visible = False
        lblMsg2.Visible = False

        'シリアルありの入荷で、登録できなかったアイテムのメッセージ出力
        If errorItem IsNot Nothing Then
            lblMsg.Visible = True

            Dim errCnt As Integer
            For i = 0 To UBound(errorItem)
                If errorItem(i).fileName <> "cancel" Then
                    msgString &= errorItem(i).fileName & ":" & errorItem(i).item & " "
                    errCnt = errCnt + 1
                End If
            Next i

            lblMsgContent.Text = errCnt & "件" & "<br>" & msgString & "<br>"

        End If

        'バーコードPDF出力失敗
        If errFlg = 1 Then
            lblMsg2.Visible = True
        End If

        '入荷番号
        lblArrivalNo.Text = arrivalNo

        'バーコード読み込みで入荷作業
        lblAllParts.Text = allQty
        lblKindParts.Text = allKindItem

        Session("err_Flg") = 0
        Session("all_Kinditem") = 0
        Session("all_Qty") = 0
        Session("arrival_No") = Nothing
        Session("error_Item") = Nothing

        'Call FileDownload(pdfFileName, "application/pdf")

    End Sub
    'backボタン押下処理
    Protected Sub btnBack_Click(sender As Object, e As ImageClickEventArgs) Handles btnBack.Click

        Response.Redirect("inv_Arrival.aspx")

    End Sub

    Protected Sub FileDownload(FileName As String, MimeType As String)

        Dim clsSet As New Class_inv

        Try
            '相対パスから物理ファイルパス取得
            'Dim FilePath As String = MapPath(String.Format("./pdf/{0}", FileName))

            'ダウンロードするファイル名
            Dim dlFileName As String

            'ファイル名が日本語の場合を考慮したダウンロードファイル名を作成
            If Request.Browser.Browser = "IE" Then
                'IEの場合はファイル名をURLエンコード
                dlFileName = HttpUtility.UrlEncode(FileName)
            Else
                'IE以外の場合はそのままでOK
                dlFileName = FileName
            End If

            Dim FilePath As String = clsSet.savePDFPass & FileName

            'Response情報クリア
            Response.ClearContent()

            'バッファリング
            Response.Buffer = True

            'HTTPヘッダー情報・MIMEタイプ設定
            Response.AddHeader("Content-Disposition", String.Format("attachment;filename={0}", dlFileName))
            Response.ContentType = MimeType

            'ファイルを書き出し
            Response.WriteFile(FilePath)
            Response.Flush()
            Response.End()
            'Response.End()の呼び出しによりエラーメッセージを出力しないようにする

        Catch ex As Exception
            'Call showMsg("バーコードPDFのダウンロードに失敗しました。", "") ★後で、ダイアログか画面へ表示するようにする。
        End Try

    End Sub

End Class