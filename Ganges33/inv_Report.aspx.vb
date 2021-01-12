Imports iTextSharp.text
Imports iTextSharp.text.pdf
Imports System.Data.SqlClient
Imports System.IO
Imports System.Text
Imports System.Net.Mail
Imports iFont = iTextSharp.text.Font
Public Class inv_report2
    Inherits System.Web.UI.Page
    Private Structure CSV_File
        Dim unq As String
        Dim parts_no As String
        Dim parts_serial As String
        Dim qty As String
        Dim name As String
    End Structure
    Private Structure Touroku_Data
        Dim parts_no As String
        Dim parts_serial As String
        Dim qty As Integer
        Dim parts_unuse As Integer
        Dim status As String
        Dim name As String
        Dim ship_code As String
    End Structure
    'インポート在庫データ
    Private CSV() As CSV_File
    '登録済の在庫データ
    Private tourokuData() As Touroku_Data
    '現在庫出力内容
    Private tourokuDataExport() As Touroku_Data
    'mistakeの報告内容
    Private reportData() As Touroku_Data
    Private clsSet As New Class_inv
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        '初期処理
        If IsPostBack = False Then

            '***セッションなしログインユーザの対応***
            Dim userid As String = Session("user_id")
            If userid = "" Then
                Response.Redirect("Login.aspx")
            End If

            '***インポート後に処理する***
            btnDifference.Enabled = False
            btnStockOutput.Enabled = False
        End If

    End Sub

    Private Sub chkUnUseSerial_CheckedChanged(sender As Object, e As EventArgs) Handles chkUnUseSerial.CheckedChanged

        If chkUnUseSerial.Checked = True Then
            chkUseSerial.Checked = False
            FileUploadInv.Enabled = True
            btnCsv.Enabled = True
        End If

    End Sub

    Private Sub chkUseSerial_CheckedChanged(sender As Object, e As EventArgs) Handles chkUseSerial.CheckedChanged

        If chkUseSerial.Checked = True Then
            chkUnUseSerial.Checked = False
            FileUploadInv.Enabled = True
            btnCsv.Enabled = True
        End If

    End Sub
    'バーコードチェック後のインポート処理
    Protected Sub btnCsv_Click(sender As Object, e As ImageClickEventArgs) Handles btnCsv.Click

        '***入力チェック***
        If chkUseSerial.Checked = False And chkUnUseSerial.Checked = False Then
            Call showMsg("Please check if there is a serial.")
            Exit Sub
        End If

        'ファイルがアップロードされていれば
        If FileUploadInv.HasFile = True Then

            '***インポートファイル(CSV)情報の取得***
            Dim FileName As String
            Dim fileNo As Integer = FreeFile()
            Dim csvRowCnt As Integer
            Dim colsHead(1) As String
            Dim colsHead2(2) As String
            Dim clsSet As New Class_inv
            'サーバ側で保存するパス
            Dim savePass As String = "C:\inv\CSV\"

            Try
                'ファイル名取得 
                FileName = FileUploadInv.FileName

                'サーバの指定パスに保存
                FileUploadInv.SaveAs(savePass & FileName)

                If FileName <> "" Then

                    FileOpen(fileNo, savePass & FileName, OpenMode.Input)

                    'シリアルあり
                    If chkUseSerial.Checked = True Then

                        Do Until EOF(fileNo) 'ファイルの最後までループ 
                            ReDim Preserve CSV(csvRowCnt)
                            Input(fileNo, CSV(csvRowCnt).unq)
                            Input(fileNo, CSV(csvRowCnt).parts_serial)
                            If csvRowCnt = 0 And colsHead(0) = "" Then
                                colsHead(0) = CSV(csvRowCnt).unq
                                colsHead(1) = CSV(csvRowCnt).parts_serial
                                'ヘッダ確認
                                If clsSet.chkHead(colsHead, "Report1") = False Then
                                    Call showMsg("There is an error in header information of csv. Please check that the file to be imported is correct.")
                                    Exit Sub
                                Else
                                    csvRowCnt = -1
                                End If
                            End If
                            csvRowCnt += 1
                        Loop

                    End If

                    'シリアルなし
                    If chkUnUseSerial.Checked = True Then

                        Do Until EOF(fileNo) 'ファイルの最後までループ 
                            ReDim Preserve CSV(csvRowCnt)
                            Input(fileNo, CSV(csvRowCnt).unq)
                            Input(fileNo, CSV(csvRowCnt).parts_no)
                            Input(fileNo, CSV(csvRowCnt).qty)
                            If csvRowCnt = 0 And colsHead2(0) = "" Then
                                colsHead2(0) = CSV(csvRowCnt).unq
                                colsHead2(1) = CSV(csvRowCnt).parts_no
                                colsHead2(2) = CSV(csvRowCnt).qty
                                'ヘッダ確認
                                If clsSet.chkHead(colsHead2, "Report2") = False Then
                                    Call showMsg("There is an error in header information of csv. Please check that the file to be imported is correct.")
                                    Exit Sub
                                Else
                                    csvRowCnt = -1
                                End If
                            End If
                            csvRowCnt += 1
                        Loop

                    End If

                    FileClose(fileNo)

                End If

                Call showMsg("success!! <br/>Proceed to the report in order. Please log off again.")
                btnDifference.Enabled = True
                Session("CSV_DATA") = CSV

                chkUseSerial.Enabled = False
                chkUnUseSerial.Enabled = False

            Catch ex As Exception
                Call showMsg("Data Import Error please retry<br/>Please check the file path and contents of the file.<br/>Please close the file.")
            End Try

            If System.IO.File.Exists(savePass & FileName) = True Then
                System.IO.File.Delete(savePass & FileName)
            End If

        Else
            Call showMsg("Please specify the import file.")
        End If

    End Sub

    Protected Sub showMsg(ByVal Msg As String)

        'Dim formid As String = Me.Form.ClientID

        '' 確認ダイアログを出力するスクリプト
        '' POST先は自分自身(inv_Report.aspx)
        'Dim sScript As String = "if(alert(""" + Msg + """)){ " +
        '                                formid + ".method = ""post"";" +
        '                                formid + ".action = ""inv_Report.aspx?errMsg=true"";" +
        '                                formid + ".submit();" +
        '                         "}"
        lblMsg.Text = Msg
        Dim sScript As String = "$(function () { $( ""#dialog"" ).dialog({width: 400, buttons: {""OK"": function () {$(this).dialog('close');}}});});"
        'JavaScriptの埋め込み
        ClientScript.RegisterStartupScript(Me.GetType(), "startup", sScript, True)

    End Sub
    '在庫差異確認処理(PDF出力)
    Protected Sub btnDifference_Click(sender As Object, e As ImageClickEventArgs) Handles btnDifference.Click

        '***セッション情報取得***
        If Session("CSV_DATA") IsNot Nothing Then
            CSV = Session("CSV_DATA")
        End If

        If CSV Is Nothing Then
            Call showMsg("Please specify the import file.")
            Exit Sub
        End If

        Dim userid As String = Session("user_id")

        If userid Is Nothing Then
            Call showMsg("The session has expired. Please login again.")
            Exit Sub
        End If

        Dim shipCode As String = Session("ship_code")

        'DBからのデータ取得失敗確認
        Dim errFlg As Integer

        '***帳簿(DB)の在庫情報を取得***
        'シリアルあり
        If chkUseSerial.Checked = True Then

            For i = 0 To UBound(CSV)

                '部品番号取得
                Dim tmpItemDelimiter() As String
                tmpItemDelimiter = Split(CSV(i).parts_serial, "-")
                CSV(i).parts_no = tmpItemDelimiter(0)

                'parts_name取得SQL
                Dim strSQL = "SELECT parts_name FROM M_PARTS WHERE DELFG = 0 AND parts_no = '" & CSV(i).parts_no & "' AND ship_code = '" & shipCode & "';"

                Dim DT_M_PARTS As DataTable = DBCommon.ExecuteGetDT(strSQL, errFlg)

                If errFlg = 1 Then
                    Call reSetSession()
                    Call showMsg("Failed to acquire information from the DB.")
                    Exit Sub
                End If

                If DT_M_PARTS IsNot Nothing Then
                    If DT_M_PARTS.Rows(0)("parts_name") IsNot DBNull.Value Then
                        CSV(i).name = DT_M_PARTS.Rows(0)("parts_name")
                    End If
                End If

                '在庫状況取得SQL
                Dim strSQL2 As String
                'シリアルあり
                strSQL2 = "SELECT parts_serial, parts_status FROM dbo.T_inParts WHERE DELFG = 0 AND parts_serial = '" & CSV(i).parts_serial & "' AND ship_code = '" & shipCode & "';"

                Dim DT_T_inParts_2 As DataTable = DBCommon.ExecuteGetDT(strSQL2, errFlg)

                If errFlg = 1 Then
                    Call reSetSession()
                    Call showMsg("Failed to acquire information from the DB.")
                    Exit Sub
                End If

                If DT_T_inParts_2 IsNot Nothing Then

                    ReDim Preserve tourokuData(i)

                    tourokuData(i).status = DT_T_inParts_2.Rows(0)("parts_status")

                    Select Case tourokuData(i).status

                        Case 1
                            tourokuData(i).status = "OK"
                        Case 2
                            tourokuData(i).status = "NG"
                        Case 3
                            tourokuData(i).status = "OK"
                        Case 4
                            tourokuData(i).status = "NG"
                        Case Else
                            tourokuData(i).status = "在庫不明"
                    End Select

                    tourokuData(i).parts_no = CSV(i).parts_no
                    tourokuData(i).parts_serial = CSV(i).parts_serial
                Else
                    ReDim Preserve tourokuData(i)
                    tourokuData(i).parts_no = ""
                    '登録されていないシリアルIDだが、報告用に誤ったデータをセットしておく。
                    tourokuData(i).parts_serial = CSV(i).parts_serial
                End If

            Next i

        End If

        'シリアルなし
        If chkUnUseSerial.Checked = True Then

            For i = 0 To UBound(CSV)

                'parts_name取得SQL
                Dim strSQL = "SELECT parts_name FROM M_PARTS WHERE DELFG = 0 AND parts_no = '" & CSV(i).parts_no & "'" & "AND ship_code = '" & shipCode & "';"

                Dim DT_M_PARTS As DataTable = DBCommon.ExecuteGetDT(strSQL, errFlg)

                If errFlg = 1 Then
                    Call reSetSession()
                    Call showMsg("Failed to acquire information from the DB.")
                    Exit Sub
                End If

                If DT_M_PARTS IsNot Nothing Then
                    If DT_M_PARTS.Rows(0)("parts_name") IsNot DBNull.Value Then
                        CSV(i).name = DT_M_PARTS.Rows(0)("parts_name")
                    End If
                End If

                '在庫状況取得SQL
                Dim strSQL2 As String

                strSQL2 = "SELECT parts_no, parts_unuse FROM dbo.T_inParts_2 WHERE DELFG = 0 AND parts_no = '" & CSV(i).parts_no & "'" & "AND ship_code = '" & shipCode & "';"

                Dim DT_T_inParts_2 As DataTable = DBCommon.ExecuteGetDT(strSQL2, errFlg)

                If errFlg = 1 Then
                    Call reSetSession()
                    Call showMsg("Failed to acquire information from the DB.")
                    Exit Sub
                End If

                If DT_T_inParts_2 IsNot Nothing Then
                    ReDim Preserve tourokuData(i)
                    tourokuData(i).parts_unuse = DT_T_inParts_2.Rows(0)("parts_unuse")
                    tourokuData(i).parts_no = CSV(i).parts_no
                Else
                    ReDim Preserve tourokuData(i)
                    tourokuData(i).parts_no = ""
                End If

            Next i

        End If

        Session("tourokuSData") = tourokuData

        '***PDF出力処理***
        Dim doc As Document
        Dim fileStream As FileStream
        Dim pdfWriter As PdfWriter
        Dim pdfFileName As String = DateTime.Now.ToString("yyyyMMddHHmmss") & ".pdf"
        Dim fnt3 As New Font(BaseFont.CreateFont("c:\windows\fonts\msgothic.ttc,2", BaseFont.IDENTITY_H, True), 20, iTextSharp.text.Font.ITALIC Or iTextSharp.text.Font.UNDERLINE)

        Try
            'FileStreamを生成 
            fileStream = New FileStream(clsSet.mailFilePass & pdfFileName, FileMode.Create)

            'Documentを生成 
            doc = New Document(PageSize.A4)

            'PdfWriter生成 
            pdfWriter = PdfWriter.GetInstance(doc, fileStream)

            'Documentのオープン 
            doc.Open()

            'シリアルあり
            If chkUseSerial.Checked = True Then

                '項目の書き出し
                doc.Add(New Paragraph("unq,serial no,pats no,parts name,Difference", fnt3))

                '在庫差分情報の書き出し
                For i = 0 To UBound(CSV)

                    If tourokuData(i).parts_no = "" Then
                        doc.Add(New Paragraph(CSV(i).unq & "," & CSV(i).parts_serial & "," & ",Parts not in the book"))
                    Else
                        doc.Add(New Paragraph(CSV(i).unq & "," & CSV(i).parts_serial & "," & CSV(i).parts_no & "," & CSV(i).name & "," & tourokuData(i).status))
                    End If

                Next i

            End If

            'シリアルなし
            If chkUnUseSerial.Checked = True Then

                '項目の書き出し
                doc.Add(New Paragraph("unq,parts no,parts name,qty,real inventry,books inventry,Difference(real inventry - books inventry),result"))

                '在庫差分情報の書き出し
                For i = 0 To UBound(CSV)

                    If tourokuData(i).parts_no = "" Then
                        doc.Add(New Paragraph(CSV(i).unq & "," & CSV(i).parts_no & ",Parts not in the book" & ",NG"))
                        tourokuData(i).status = "NG"
                    Else
                        If (CSV(i).qty - tourokuData(i).parts_unuse) <> 0 Then
                            doc.Add(New Paragraph(CSV(i).unq & "," & CSV(i).parts_no & "," & CSV(i).name & "," & CSV(i).qty & "," & tourokuData(i).parts_unuse & "," & CSV(i).qty - tourokuData(i).parts_unuse & ",NG"))
                            tourokuData(i).status = "NG"
                        Else
                            doc.Add(New Paragraph(CSV(i).unq & "," & CSV(i).parts_no & "," & CSV(i).name & "," & CSV(i).qty & "," & tourokuData(i).parts_unuse & "," & CSV(i).qty - tourokuData(i).parts_unuse & ",OK"))
                            tourokuData(i).status = "OK"
                        End If
                    End If

                Next i

            End If

            'クローズ 
            doc.Close()

        Catch ex As Exception
            Call showMsg("The PDF output for confirming the difference failed.")
            Exit Sub
        End Try

        '***メール送信処理***
        'メールアドレスの取得
        Dim clsSetCommom As New Class_common
        Dim userMail As String
        Dim errMsg As String

        clsSetCommom.setMail(userid, userMail, errMsg)

        If errMsg <> "" Then
            Call showMsg(errMsg)
            Exit Sub
        Else
            Session("user_Mail") = userMail
        End If

        Try
            '件名
            Dim smtp As New SmtpClient()
            Dim msg As New MailMessage()
            Dim attach As New System.Net.Mail.Attachment(clsSet.mailFilePass & pdfFileName)

            msg.Attachments.Add(attach)

            '送信元
            msg.From = New MailAddress(userMail)

            '送信先
            msg.To.Add(New MailAddress(clsSetCommom.toMailAddPdf))

            ' 件名
            msg.Subject = "Check inventory difference"

            ' 本文
            msg.Body &= "Please check the attachment of the email." & vbCrLf

            'メッセージを送信
            smtp.Host = clsSetCommom.SMTPSERVER
            smtp.Send(msg)

        Catch ex As Exception
            Call showMsg("you failed to send mail.")
            Exit Sub
        End Try

        Call showMsg("success!!<br/>" & clsSet.mailFilePass & pdfFileName & "をご確認ください。<br/>メール送信しました。")
        btnStockOutput.Enabled = True
        btnCsv.Enabled = False

    End Sub
    '現在庫出力処理
    Protected Sub btnStockOutput_Click(sender As Object, e As ImageClickEventArgs) Handles btnStockOutput.Click

        '***セッション情報取得***
        Dim userid As String = Session("user_id")
        Dim shipCode As String = Session("ship_code")
        Dim userLevel As String = Session("user_level")

        '***入力チェック***
        If chkUseSerial.Checked = False And chkUnUseSerial.Checked = False Then
            Call showMsg("Please check whether it is serial or not.")
            Exit Sub
        End If

        If userid Is Nothing Then
            Call showMsg("The session has expired. Please login again.")
            Exit Sub
        End If

        If userLevel <> "1" Then
            Call showMsg("You do not have administrator privileges.")
            Exit Sub
        End If

        '***現在庫取得***
        Dim dsStock As New DataSet
        Dim errFlg As Integer = 0

        'シリアルあり
        If chkUseSerial.Checked = True Then
            dsStock = DBCommon.Get_DSStock(1, errFlg, shipCode)
            If errFlg = 1 Then
                Call showMsg("Failed to acquire inventory.")
                Exit Sub
            End If
        End If

        'シリアルなし
        If chkUnUseSerial.Checked = True Then
            dsStock = DBCommon.Get_DSStock(2, errFlg, shipCode)
            If errFlg = 1 Then
                Call showMsg("Failed to acquire inventory.")
                Exit Sub
            End If
        End If

        If dsStock IsNot Nothing Then

            If dsStock.Tables(0).Rows.Count <> 0 Then

                ReDim tourokuDataExport(dsStock.Tables(0).Rows.Count - 1)

                For i = 0 To dsStock.Tables(0).Rows.Count - 1

                    Dim dr As DataRow = dsStock.Tables(0).Rows(i)

                    If dr("parts_no") IsNot DBNull.Value Then
                        tourokuDataExport(i).parts_no = dr("parts_no")
                    End If

                    If dr("parts_name") IsNot DBNull.Value Then
                        tourokuDataExport(i).name = dr("parts_name")
                    End If

                    If dr("ship_code") IsNot DBNull.Value Then
                        tourokuDataExport(i).ship_code = dr("ship_code")
                    End If

                    'シリアルあり
                    If chkUseSerial.Checked = True Then
                        If dr("parts_serial") IsNot DBNull.Value Then
                            tourokuDataExport(i).parts_serial = dr("parts_serial")
                        End If
                    End If

                    'シリアルなし
                    If chkUnUseSerial.Checked = True Then
                        If dr("parts_unuse") IsNot DBNull.Value Then
                            tourokuDataExport(i).parts_unuse = dr("parts_unuse")
                        End If
                    End If

                Next i

            End If

        End If

        '***CSV出力処理***
        Try

            Dim csvFileName As String = DateTime.Now.ToString("yyyyMMddHHmmss") & ".csv"
            Dim outputPath As String = clsSet.msgMailCsvFilePass & csvFileName

            'シリアルあり
            If chkUseSerial.Checked = True Then

                '項目名設定
                Dim csvContents = New List(Of String)(New String() {"unq,serial,parts no,parts name,branch"})

                '内容設定
                If tourokuDataExport IsNot Nothing Then
                    For i = 0 To UBound(tourokuDataExport)
                        csvContents.Add(i + 1 & "," & tourokuDataExport(i).parts_serial & "," & tourokuDataExport(i).parts_no & ",""" & tourokuDataExport(i).name & """," & tourokuDataExport(i).ship_code)
                    Next i
                Else
                    csvContents.Add("There is no stock data.")
                End If

                Using writer As New StreamWriter(outputPath, False, Encoding.Default)
                    writer.WriteLine(String.Join(Environment.NewLine, csvContents))
                End Using

                'If System.IO.File.Exists(outputPath) = True Then
                '    System.IO.File.Delete(outputPath)
                'End If

            End If

            'シリアルなし
            If chkUnUseSerial.Checked = True Then

                '項目名設定
                Dim csvContents = New List(Of String)(New String() {"unq,parts no,parts name,qty,branch"})

                '内容設定
                If tourokuDataExport IsNot Nothing Then
                    For i = 0 To UBound(tourokuDataExport)
                        csvContents.Add(i + 1 & "," & tourokuDataExport(i).parts_no & ",""" & tourokuDataExport(i).name & """," & tourokuDataExport(i).parts_unuse & "," & tourokuDataExport(i).ship_code)
                    Next i
                Else
                    csvContents.Add("There is no stock data.")
                End If

                Using writer As New StreamWriter(outputPath, False, Encoding.Default)
                    writer.WriteLine(String.Join(Environment.NewLine, csvContents))
                End Using

            End If

            btnInventoryReport.Enabled = True

            ' ダウンロード
            Response.ContentType = "application/octet-stream"
            Response.AddHeader("Content-Disposition", "attachment; filename=" & csvFileName)
            Response.Flush()
            Response.WriteFile(outputPath)
            Response.End()

        Catch ex As Exception
            Call showMsg("CSV output processing failed.")
        End Try

    End Sub
    '棚卸報告処理
    Protected Sub btnInventoryReport_Click(sender As Object, e As ImageClickEventArgs) Handles btnInventoryReport.Click

        'セッション情報取得
        Dim CSV() As CSV_File
        If Session("CSV_DATA") IsNot Nothing Then
            CSV = Session("CSV_DATA")
        Else
            Call showMsg("Import information is missing.")
            Exit Sub
        End If

        Dim tourokuData() As Touroku_Data
        If Session("tourokuSData") IsNot Nothing Then
            tourokuData = Session("tourokuSData")
        Else
            Call showMsg("Stock difference confirmation is not completed.")
            Exit Sub
        End If

        Dim userMail As String = Session("user_Mail")

        Dim shipCode As String = Session("ship_code")

        If shipCode Is Nothing Then
            Call showMsg("The session has expired. Please login again.")
            Exit Sub
        End If

        '***集計***
        Dim i, j, m As Integer
        Dim stockAll As Integer
        Dim correct As Integer = 0
        Dim dsStock As New DataSet
        Dim errFlg As Integer = 0
        Dim mistake As Integer = 0

        'シリアルあり
        If chkUseSerial.Checked = True Then

            '現在庫情報取得
            dsStock = DBCommon.Get_DSStock(1, errFlg, shipCode)
            If errFlg = 1 Then
                Call showMsg("Failed to acquire inventory.")
                Call reSet()
                Exit Sub
            End If

            If dsStock IsNot Nothing Then

                '在庫数
                stockAll = dsStock.Tables(0).Rows.Count

                'correct数
                If tourokuData IsNot Nothing Then
                    For i = 0 To UBound(tourokuData)
                        If tourokuData(i).status = "OK" Then
                            correct = correct + 1
                        End If
                    Next
                End If

                'mistake数
                mistake = stockAll - correct
            End If

        End If

        'シリアルなし
        If chkUnUseSerial.Checked = True Then

            '現在庫情報取得
            dsStock = DBCommon.Get_DSStock(2, errFlg, shipCode)
            If errFlg = 1 Then
                Call showMsg("Failed to acquire inventory.")
                Call reSet()
                Exit Sub
            End If

            If dsStock IsNot Nothing Then
                '在庫数
                Dim tmp As Integer = 0
                For i = 0 To dsStock.Tables(0).Rows.Count - 1
                    Dim dr As DataRow = dsStock.Tables(0).Rows(i)
                    If dr("parts_unuse") IsNot DBNull.Value Then
                        tmp = dr("parts_unuse")
                        stockAll = stockAll + tmp
                    End If
                Next i

                'correct数
                tmp = 0
                For i = 0 To UBound(CSV)
                    If tourokuData(i).status = "OK" Then
                        tmp = CSV(i).qty
                        correct = correct + tmp
                    End If
                Next

                'mistake数
                mistake = stockAll - correct

            End If

        End If

        '***シリアルあり　mistake報告用設定***
        If chkUseSerial.Checked = True Then

            'コネクションを取得
            Dim con As SqlConnection = New SqlConnection(ConfigurationManager.ConnectionStrings("cnstr").ConnectionString)
            con.Open()

            Try
                'シリアルあり （在庫なしのステータス2:使用済/未返却　4：返却済）
                Dim sqlStr As String = ""
                sqlStr &= "SELECT A.parts_serial, A.parts_no, A.parts_status "
                sqlStr &= "FROM T_inParts A "
                sqlStr &= "WHERE A.DELFG = 0 "
                sqlStr &= "AND A.parts_status IN('2','4') "

                        Dim sqlSelect As New SqlCommand(sqlStr, con)
                Dim Adapter As New SqlDataAdapter(sqlSelect)
                Dim Builder As New SqlCommandBuilder(Adapter)
                Dim ds As New DataSet
                Adapter.Fill(ds)

                'mistake報告用の内容を設定
                If ds IsNot Nothing Then
                    If ds.Tables(0).Rows.Count <> 0 Then
                        m = 0
                        If tourokuData IsNot Nothing Then
                            For i = 0 To UBound(tourokuData)
                                If tourokuData(i).parts_no = "" Then
                                    ReDim Preserve reportData(m)
                                    reportData(m).parts_no = tourokuData(i).parts_serial & " is an ID that is not registered."
                                    m = m + 1
                                Else
                                    For j = 0 To ds.Tables(0).Rows.Count - 1
                                        Dim dr As DataRow = ds.Tables(0).Rows(j)
                                        If tourokuData(i).parts_serial = dr("parts_serial") Then
                                            If tourokuData(i).status = "NG" Then
                                                ReDim Preserve reportData(m)
                                                If dr("parts_no") IsNot DBNull.Value Then
                                                    reportData(m).parts_no = dr("parts_no")
                                                End If
                                                If dr("parts_serial") IsNot DBNull.Value Then
                                                    reportData(m).parts_serial = dr("parts_serial")
                                                End If
                                                If dr("parts_status") IsNot DBNull.Value Then
                                                    reportData(m).status = dr("parts_status")
                                                End If
                                                m = m + 1
                                            End If
                                        End If
                                    Next j
                                End If
                            Next i
                        End If
                    End If
                End If

            Catch ex As Exception
                Call showMsg("Data acquisition of CSV output failed.")
                Call reSet()
                If con.State <> ConnectionState.Closed Then
                    con.Close()
                End If
                Exit Sub
            Finally
                'DB接続クローズ
                If con.State <> ConnectionState.Closed Then
                    con.Close()
                End If
            End Try
        End If

        '***シリアルなし mistake報告用設定***
        If chkUnUseSerial.Checked = True Then
            m = 0
            If tourokuData IsNot Nothing Then
                For i = 0 To UBound(tourokuData)
                    If tourokuData(i).parts_no = "" Then
                        ReDim Preserve reportData(m)
                        reportData(m).parts_no = CSV(i).parts_no & " is an ID that is not registered."
                        m = m + 1
                    Else
                        If tourokuData(i).status = "NG" Then
                            For j = 0 To UBound(CSV)
                                If tourokuData(i).parts_no = CSV(j).parts_no Then
                                    ReDim Preserve reportData(m)
                                    reportData(m).parts_no = tourokuData(i).parts_no
                                    reportData(m).parts_unuse = tourokuData(i).parts_unuse
                                    reportData(m).qty = CSV(j).qty
                                    m = m + 1
                                End If
                            Next j
                        End If
                    End If
                Next i
            End If
        End If

        '***メール送信処理 ***
        Dim smtp As New SmtpClient()
        Dim msg As New MailMessage()
        Dim clsSetCommon As New Class_common

        Try
            '送信元
            msg.From = New MailAddress(userMail)

            '送信先
            msg.To.Add(New MailAddress(clsSetCommon.toMailAddPdf))

            ' 件名
            msg.Subject = "mistake breakdown"

            ' 本文
            msg.Body &= "stockAll:" & stockAll & " "
            msg.Body &= "correct:" & correct & " "
            msg.Body &= "mistake:" & mistake & vbCrLf
            msg.Body &= "mistake breakdown" & vbCrLf

            If m = 0 Then
                Call showMsg("There was no records found.")
            Else
                'シリアルあり
                If chkUseSerial.Checked = True Then
                    If reportData IsNot Nothing Then
                        For i = 0 To UBound(reportData)
                            If i = 0 Then
                                msg.Body &= "parts no, serial no, D_status, A_status" & vbCrLf
                                msg.Body &= reportData(i).parts_no & "," & reportData(i).parts_serial & "," & reportData(i).status & ",1" & vbCrLf
                            Else
                                msg.Body &= reportData(i).parts_no & "," & reportData(i).parts_serial & "," & reportData(i).status & ",1" & vbCrLf
                            End If
                        Next i
                    Else
                        msg.Body &= "no list available."
                    End If
                End If

                'シリアルなし
                If chkUnUseSerial.Checked = True Then
                    If reportData IsNot Nothing Then
                        For i = 0 To UBound(reportData)
                            If i = 0 Then
                                msg.Body &= "parts no, D_stock, A_stock" & vbCrLf
                                msg.Body &= reportData(i).parts_no & "," & reportData(i).parts_unuse & "," & reportData(i).qty & vbCrLf
                            Else
                                msg.Body &= reportData(i).parts_no & "," & reportData(i).parts_unuse & "," & reportData(i).qty & vbCrLf
                            End If
                        Next i
                    Else
                        msg.Body &= "no list available."
                    End If
                End If
            End If

            'メッセージを送信
            smtp.Host = clsSetCommon.SMTPSERVER
            smtp.Send(msg)
            Call showMsg("The inventory report has been completed.")
        Catch ex As Exception
            Call showMsg("you failed to send mail.")
            Call reSet()
            Exit Sub
        End Try

        chkUseSerial.Enabled = True
        chkUnUseSerial.Enabled = True

        Call reSetSession()

    End Sub

    Protected Sub reSet()

        chkUseSerial.Enabled = False
        chkUnUseSerial.Enabled = False
        Call reSetSession()

    End Sub

    Protected Sub reSetSession()

        Session("CSV_DATA") = Nothing
        Session("tourokuSData") = Nothing
        Session("user_Mail") = Nothing

    End Sub


End Class