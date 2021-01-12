Imports System.Drawing
Imports System.IO
Imports System.Text
Public Class Analysis_Cash_Tracking2
    Inherits System.Web.UI.Page

    Private Structure DELCHK
        Dim No As String
        Dim PaymentMethod As String
        Dim CardType As String
    End Structure

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        '***初期処理***
        If IsPostBack = False Then

            '***セッションなしログインユーザの対応***
            Dim userid As String = Session("user_id")
            If userid = "" Then
                Response.Redirect("Login.aspx")
            End If

            Call showData()

        End If

    End Sub
    Protected Sub showData()

        '***セッション取得***
        Dim userLevel As String = Session("user_level")
        Dim adminFlg As Boolean = Session("admin_Flg")
        Dim setShipname As String = Session("ship_Name")
        Dim shipCode As String = Session("ship_code")

        Dim Warranty As String = Session("set_Warranty")

        Dim setMon As String
        If Session("set_Month_today") IsNot Nothing Then
            'todayボタン押下時の月を取得
            setMon = Session("set_Month_today")
            Session("set_Month") = setMon
            Session("set_Month_today") = Nothing
        Else
            setMon = Session("set_Month")
        End If

        Dim setDay As String = Session("set_Day")
        Dim setYear As String = Session("set_Year")

        '***表示の設定***
        '表示させたい請求月日
        lblMonNow.Text = setMon
        lblDay.Text = setDay

        'DLボタン
        btnDown.Visible = False

        '削除ボタン
        If (userLevel = "0" Or userLevel = "1" Or userLevel = "2" Or userLevel = "3" Or adminFlg = True) Then
            btnSend.Enabled = True
        Else
            btnSend.Enabled = False
        End If

        '***GRIDVIEWへ反映 ***
        '請求日の設定
        Dim clsSetCommon As New Class_common
        Dim dtNow As DateTime = clsSetCommon.dtIndia
        'Comment by Mohan
        'Dim invoiceDate As String = Left(dtNow.ToShortDateString, 4) & "/" & setMon & "/" & setDay
        Dim invoiceDate As String = setYear & "/" & setMon & "/" & setDay

        'SQL設定
        Dim strSQL As String
        If Warranty = "ALL" Then
            strSQL = "SELECT FALSE, count_no AS No, Warranty, claim_no AS 'Service Order No' , customer_name AS 'Customer Name', CASE WHEN payment_kind IS NULL then payment WHEN payment_kind = '1' OR payment_kind = '2' THEN 'Cash/Credit' ELSE payment END 'Payment Method', CONVERT(decimal(13,2),total_amount) AS 'Invoice Amount', CASE WHEN payment = 'Credit' then CONVERT(decimal(13,2),claim_card) WHEN payment = 'Cash' then CONVERT(decimal(13,2),claim)  ELSE CONVERT(decimal(13,2),claim) END AS 'Collected Amount', CONVERT(decimal(13,2),discount) AS 'Discount', full_discount AS full_discount, card_number AS 'Autholization Code', card_type AS 'Card Type' "
            strSQL &= "FROM dbo.cash_track WHERE DELFG = 0 AND location = '" & shipCode & "' AND LEFT(CONVERT(VARCHAR, invoice_date,111),10) = '" & invoiceDate & "' "
            strSQL &= "ORDER BY claim_no, count_no;"
        Else
            strSQL = "SELECT FALSE, count_no AS No, Warranty, claim_no AS 'Service Order No' , customer_name AS 'Customer Name', CASE WHEN payment_kind IS NULL then payment WHEN payment_kind = '1' OR payment_kind = '2' THEN 'Cash/Credit' ELSE payment END 'Payment Method', CONVERT(decimal(13,2),total_amount) AS 'Invoice Amount',  CASE WHEN payment = 'Credit' then CONVERT(decimal(13,2),claim_card) WHEN payment = 'Cash' then CONVERT(decimal(13,2),claim)  ELSE CONVERT(decimal(13,2),claim) END AS 'Collected Amount', CONVERT(decimal(13,2),discount) AS 'Discount', full_discount AS full_discount, card_number AS 'Autholization Code', card_type AS 'Card Type' "
            strSQL &= "FROM dbo.cash_track WHERE DELFG = 0 AND location = '" & shipCode & "' AND LEFT(CONVERT(VARCHAR, invoice_date,111),10) = '" & invoiceDate & "' AND Warranty = '" & Warranty & "' "
            strSQL &= "ORDER BY claim_no, count_no;"
        End If

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
                '削除処理/次ページ用に取得しておく
                Session("Data_DT_SERCH") = DT_SERCH
                'DLボタンの設定
                If (userLevel = "0" Or userLevel = "1" Or userLevel = "2" Or userLevel = "3") Then
                    btnDown.Visible = True
                End If
            Else
                GridInfo.DataSource = Nothing
                GridInfo.DataBind()
                btnSend.Visible = False
            End If

        End If

        '***集計***
        Dim clsSetMoney As New Class_money
        Dim setResult As Class_money.Aggregation
        Dim errMsg As String = ""

        clsSetMoney.setSyukei(setResult, shipCode, invoiceDate, errMsg)

        If errMsg <> "" Then
            Call showMsg(errMsg)
            Exit Sub
        Else
            Session("set_Result") = setResult
        End If

        '表示
        If Warranty = "ALL" Then
            lblCount.Text = setResult.Count
            lblCount0.Text = setResult.Count
            lblIW.Text = setResult.IWCnt
            lblIW0.Text = setResult.IWCnt
            lblOOW.Text = setResult.OOWCardCnt + setResult.OOWCashCnt + setResult.OOWCashCreditCount
            lblOOW0.Text = setResult.OOWCardCnt + setResult.OOWCashCnt + setResult.OOWCashCreditCount
            lblOther.Text = setResult.otherCardCnt + setResult.otherCashCnt + setResult.otherCashCreditCount
            lblOther0.Text = setResult.otherCardCnt + setResult.otherCashCnt + setResult.otherCashCreditCount
            lblCard.Text = setResult.OOWCardCnt + setResult.otherCardCnt + setResult.OOWCashCreditCount + setResult.otherCashCreditCount
            lblCard0.Text = setResult.OOWCardCnt + setResult.otherCardCnt + setResult.OOWCashCreditCount + setResult.otherCashCreditCount
            lblCash.Text = setResult.OOWCashCnt + setResult.otherCashCnt + setResult.OOWCashCreditCount + setResult.otherCashCreditCount
            lblCash0.Text = setResult.OOWCashCnt + setResult.otherCashCnt + setResult.OOWCashCreditCount + setResult.otherCashCreditCount
            lblAmount.Text = clsSetMoney.setINR2(setResult.IWTaxIn + setResult.OOWCardTaxIn + setResult.OOWCashTaxIn + setResult.otherCardTaxIn + setResult.otherCashTaxIn) & "INR"
            lblAmount0.Text = clsSetMoney.setINR2(setResult.IWSum + setResult.OOWCardSum + setResult.OOWCashSum + setResult.otherCardSum + setResult.otherCashSum) & "INR" 'claim
            lblIWAmount.Text = clsSetMoney.setINR2(setResult.IWTaxIn) & "INR"
            lblIWAmount0.Text = clsSetMoney.setINR2(setResult.IWSum) & "INR" 'claim
            lblOOWAmount.Text = clsSetMoney.setINR2(setResult.OOWCardTaxIn + setResult.OOWCashTaxIn) & "INR"
            lblOOWAmount0.Text = clsSetMoney.setINR2(setResult.OOWCardSum + setResult.OOWCashSum) & "INR" 'claim
            lblOtherAmount.Text = clsSetMoney.setINR2(setResult.otherCardTaxIn + setResult.otherCashTaxIn) & "INR"
            lblOtherAmount0.Text = clsSetMoney.setINR2(setResult.otherCardSum + setResult.otherCashSum) & "INR" 'claim
        ElseIf Warranty = "OOW" Then
            lblCount.Text = setResult.OOWCardCnt + setResult.OOWCashCnt + setResult.OOWCashCreditCount
            lblCount0.Text = setResult.OOWCardCnt + setResult.OOWCashCnt + setResult.OOWCashCreditCount
            lblIW.Text = ""
            lblIW0.Text = ""
            lblOOW.Text = setResult.OOWCardCnt + setResult.OOWCashCnt + setResult.OOWCashCreditCount
            lblOOW0.Text = setResult.OOWCardCnt + setResult.OOWCashCnt + setResult.OOWCashCreditCount
            lblOther.Text = ""
            lblOther0.Text = ""
            lblCard.Text = setResult.OOWCardCnt + setResult.OOWCashCreditCount
            lblCard0.Text = setResult.OOWCardCnt + setResult.OOWCashCreditCount
            lblCash.Text = setResult.OOWCashCnt + setResult.OOWCashCreditCount
            lblCash0.Text = setResult.OOWCashCnt + setResult.OOWCashCreditCount
            lblAmount.Text = clsSetMoney.setINR2(setResult.OOWCardTaxIn + setResult.OOWCashTaxIn) & "INR"
            lblAmount0.Text = clsSetMoney.setINR2(setResult.OOWCardSum + setResult.OOWCashSum) & "INR" 'calim
            lblIWAmount.Text = ""
            lblIWAmount0.Text = "" 'claim
            lblOOWAmount.Text = clsSetMoney.setINR2(setResult.OOWCardTaxIn + setResult.OOWCashTaxIn) & "INR"
            lblOOWAmount0.Text = clsSetMoney.setINR2(setResult.OOWCardSum + setResult.OOWCashSum) & "INR" 'claim
            lblOtherAmount.Text = ""
            lblOtherAmount0.Text = "" 'claim
        ElseIf Warranty = "IW" Then
            lblCount.Text = setResult.IWCnt
            lblCount0.Text = setResult.IWCnt
            lblIW.Text = setResult.IWCnt
            lblIW0.Text = setResult.IWCnt
            lblOOW.Text = ""
            lblOOW0.Text = ""
            lblOther.Text = ""
            lblOther0.Text = ""
            lblCard.Text = ""
            lblCard0.Text = ""
            lblCash.Text = ""
            lblCash0.Text = ""
            lblAmount.Text = clsSetMoney.setINR2(setResult.IWTaxIn) & "INR"
            lblAmount0.Text = clsSetMoney.setINR2(setResult.IWSum) & "INR"
            lblIWAmount.Text = clsSetMoney.setINR2(setResult.IWTaxIn) & "INR"
            lblIWAmount0.Text = clsSetMoney.setINR2(setResult.IWSum) & "INR" 'claim
            lblOOWAmount.Text = ""
            lblOOWAmount0.Text = "" 'claim
            lblOtherAmount.Text = ""
            lblOtherAmount0.Text = "" 'claim
        ElseIf Warranty = "Other" Then
            lblCount.Text = setResult.otherCardCnt + setResult.otherCashCnt + setResult.otherCashCreditCount
            lblCount0.Text = setResult.otherCardCnt + setResult.otherCashCnt + setResult.otherCashCreditCount
            lblIW.Text = ""
            lblIW0.Text = ""
            lblOOW.Text = ""
            lblOOW0.Text = ""
            lblOther.Text = setResult.otherCardCnt + setResult.otherCashCnt + setResult.otherCashCreditCount
            lblOther0.Text = setResult.otherCardCnt + setResult.otherCashCnt + setResult.otherCashCreditCount
            lblCard.Text = setResult.otherCardCnt + setResult.otherCashCreditCount
            lblCard0.Text = setResult.otherCardCnt + setResult.otherCashCreditCount
            lblCash.Text = setResult.otherCashCnt + setResult.otherCashCreditCount
            lblCash0.Text = setResult.otherCashCnt + setResult.otherCashCreditCount
            lblAmount.Text = clsSetMoney.setINR2(setResult.otherCardTaxIn + setResult.otherCashTaxIn) & "INR"
            lblAmount0.Text = clsSetMoney.setINR2(setResult.otherCardSum + setResult.otherCashSum) & "INR"
            lblIWAmount.Text = ""
            lblIWAmount0.Text = "" 'claim
            lblOOWAmount.Text = ""
            lblOOWAmount0.Text = "" 'claim
            lblOtherAmount.Text = clsSetMoney.setINR2(setResult.otherCardTaxIn + setResult.otherCashTaxIn) & "INR"
            lblOtherAmount0.Text = clsSetMoney.setINR2(setResult.otherCardSum + setResult.otherCashSum) & "INR" 'claim
        End If

    End Sub

    Protected Sub showMsg(ByVal Msg As String)

        lblMsg.Text = Msg
        Dim sScript As String = "$(function () { $( ""#dialog"" ).dialog({width: 400, buttons: {""OK"": function () {$(this).dialog('close');}}});});"
        'JavaScriptの埋め込み
        ClientScript.RegisterStartupScript(Me.GetType(), "startup", sScript, True)

    End Sub

    Protected Sub btnBack_Click(sender As Object, e As ImageClickEventArgs) Handles btnBack.Click

        Response.Redirect("Analysis_Cash_Tracking_qg.aspx")

    End Sub
    'downloadボタン押下処理
    Protected Sub btnDown_Click(sender As Object, e As ImageClickEventArgs) Handles btnDown.Click

        '***セッション情報を取得***
        Dim Warranty As String = Session("set_Warranty")
        Dim setMon As String = Session("set_Month")
        Dim setDay As String = Session("set_Day")
        Dim setYear As String = Session("set_Year")
        Dim setShipname As String = Session("ship_Name")
        Dim shipCode As String = Session("ship_code")

        '集計結果
        Dim setResult As Class_money.Aggregation = Session("set_Result")

        '***DL処理***
        Dim clsSetCommon As New Class_common
        Dim dtNow As DateTime = clsSetCommon.dtIndia
        Dim errFlg As Integer
        Dim clsSet As New Class_analysis
        Dim dscash_track As New DataSet
        Dim cashTrackData() As Class_analysis.CASH_TRACK

        '請求日の設定
        Dim invoiceDate As String
        invoiceDate = setYear & "/" & setMon & "/" & setDay

        Call clsSet.exportData(dscash_track, cashTrackData, Warranty, shipCode, invoiceDate, errFlg)

        If errFlg = 1 Then
            Call showMsg("Failed to acquire DL data from cash_track.")
            Exit Sub
        End If

        If dscash_track Is Nothing Then
            Call showMsg("There is no DL data. Cancel processing.")
            Exit Sub
        Else

            Try
                'ファイル名設定
                Dim csvFileName As String = Warranty & "_" & Left(dtNow.ToShortDateString, 4) & setMon & setDay & ".csv"
                Dim outputPath As String = clsSet.CsvFilePass & csvFileName

                '項目名設定
                Dim rowWork1 As String = setShipname & " Result"

                Dim csvContents = New List(Of String)(New String() {rowWork1})

                Dim rowWork2 As String = "Buisiness Day," & Left(dtNow.ToShortDateString, 4) & "/" & setMon & "/" & setDay

                '入力件数
                Dim rowWork3 As String = "Number of Entries"

                Dim rowWork4, rowWork5, rowWork6, rowWork7, rowWork9, rowWork10 As String

                If Warranty = "ALL" Then

                    rowWork4 = "Type,IW,OOW,other"

                    rowWork5 = "," & setResult.IWCnt & "," & setResult.OOWCardCnt + setResult.OOWCashCnt + setResult.OOWCashCreditCount & "," & setResult.otherCardCnt + setResult.otherCashCnt + setResult.otherCashCreditCount

                    '請求合計
                    rowWork6 = "Billing Total," & setResult.IWSum + setResult.OOWCardSum + setResult.OOWCashSum + setResult.otherCardSum + setResult.otherCashSum & "INR" & ",cash," & setResult.OOWCashCnt + setResult.otherCashCnt + setResult.OOWCashCreditCount + setResult.otherCashCreditCount & "," & setResult.OOWCashSum + setResult.otherCashSum & "INR"

                    rowWork7 = "," & ",Credit Card," & setResult.OOWCardCnt + setResult.otherCardCnt + setResult.OOWCashCreditCount + setResult.otherCashCreditCount & "," & setResult.OOWCardSum + setResult.otherCardSum & "INR"

                    '預かり金合計
                    rowWork9 = "total cash recived," & setResult.OOWDeposit + setResult.otherDeposit & "INR"

                    rowWork10 = "total Balance," & setResult.OOWChange + setResult.otherChange & "INR"

                ElseIf Warranty = "OOW" Then

                    rowWork4 = "Type,OOW"

                    rowWork5 = "," & setResult.OOWCardCnt + setResult.OOWCashCnt + setResult.OOWCashCreditCount

                    rowWork6 = "Billing Total," & setResult.OOWCardSum + setResult.OOWCashSum & "INR" & ",cash," & setResult.OOWCashCnt + setResult.OOWCashCreditCount & "," & setResult.OOWCashSum & "INR"

                    rowWork7 = "," & ",Credit Card," & setResult.OOWCardCnt + setResult.OOWCashCreditCount & "," & setResult.OOWCardSum & "INR"

                    rowWork9 = "total cash recived," & setResult.OOWDeposit & "INR"

                    rowWork10 = "total Balance," & setResult.OOWChange & "INR"

                ElseIf Warranty = "IW" Then

                    rowWork4 = "Type,IW"

                    rowWork5 = "," & setResult.IWCnt

                    rowWork6 = "Billing Total," & setResult.IWSum & "INR"

                    rowWork7 = ""

                ElseIf Warranty = "Other" Then

                    rowWork4 = "Type,other"

                    rowWork5 = "," & setResult.otherCardCnt + setResult.otherCashCnt + setResult.otherCashCreditCount

                    rowWork6 = "Billing Total," & setResult.otherCardSum + setResult.otherCashSum & "INR" & ",cash," & setResult.otherCashCnt + setResult.otherCashCreditCount & "," & setResult.otherCashSum & "INR"

                    rowWork7 = "," & ",Credit Card," & setResult.otherCardCnt + setResult.otherCashCreditCount & "," & setResult.otherCardSum & "INR"

                    rowWork9 = "total cash recived," & setResult.otherDeposit & "INR"

                    rowWork10 = "total Balance," & setResult.otherChange & "INR"

                End If

                Dim rowWork8 As String = ""

                Dim rowWork11 As String = ""

                '一覧の項目
                Dim rowWork12 As String = "Delete Flg,Branch Name,Warranty,Date time,No,Service Order No,Customer Name,payment_kind,Payment Method,Total Amount,collect amount cash,collect amount card,Autholization Code,Card Type,cash recived,Balance,discount,full_discount,message,input user"

                csvContents.Add(rowWork2)
                csvContents.Add(rowWork3)
                csvContents.Add(rowWork4)
                csvContents.Add(rowWork5)
                csvContents.Add(rowWork6)

                If Warranty <> "IW" Then
                    csvContents.Add(rowWork7)
                End If

                csvContents.Add(rowWork8)

                If Warranty <> "IW" Then
                    csvContents.Add(rowWork9)
                    csvContents.Add(rowWork10)
                End If

                csvContents.Add(rowWork11)
                csvContents.Add(rowWork12)

                '一覧の設定
                For i = 0 To cashTrackData.Length - 1

                    If cashTrackData(i).delFlg = "1" Then
                        cashTrackData(i).delFlg = "NG"
                    Else
                        cashTrackData(i).delFlg = "OK"
                    End If

                    Dim fullDiscount As Boolean
                    If cashTrackData(i).full_discount = -1 Then
                        fullDiscount = True
                    Else
                        fullDiscount = False
                    End If

                    csvContents.Add(cashTrackData(i).delFlg & "," & cashTrackData(i).location & "," & cashTrackData(i).Warranty & "," & cashTrackData(i).invoice_date.ToShortDateString & "," & cashTrackData(i).count_no & "," & cashTrackData(i).claim_no & "," & cashTrackData(i).customer_name & "," & cashTrackData(i).payment_kind & "," & cashTrackData(i).payment & "," & cashTrackData(i).total_amount & "," & cashTrackData(i).claim & "," & cashTrackData(i).claimCredit & "," & cashTrackData(i).card_number & "," & cashTrackData(i).card_type & "," & cashTrackData(i).deposit & "," & cashTrackData(i).change & "," & cashTrackData(i).discount & "," & fullDiscount & "," & cashTrackData(i).message & "," & cashTrackData(i).input_user)

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
                Call showMsg("Failed  to download prcess.")
            End Try

        End If

    End Sub
    '削除処理
    Protected Sub btnSend_Click(sender As Object, e As ImageClickEventArgs) Handles btnSend.Click

        '***セッション情報取得***
        Dim userid As String = Session("user_id")
        Dim shipCode As String = Session("ship_code")
        Dim setMon As String = Session("set_Month")
        Dim setDay As String = Session("set_Day")
        Dim setYear As String = Session("set_Year")

        If userid = "" Then
            Call showMsg("The session has expired. Please login again.")
            Exit Sub
        End If

        '***チェック対象取得処理***
        'GridView情報を取得
        GridInfo.DataSource = Session("Data_DT_SERCH")

        Dim delKey As String
        Dim NoCount As Integer
        Dim delChkData() As DELCHK

        '削除対象のアップロードファイル名を取得
        '※チェックボックス追加のデザイナーズ画面での処理
        '⇒TemplateFieldを追加しGridViewのスマートタグでテンプレートの編集をクリック
        '表示されたスマートタグパネルにおけるItemTemplateにツールボックスからCheckBoxをドラッグして配置
        For Each row As GridViewRow In GridInfo.Rows
            If CType(row.FindControl("chkDel"), CheckBox).Checked Then
                'ユニークのNOを取得
                delKey &= "'" & row.Cells(2).Text & "',"
                If row.Cells(6).Text = "Cash/Credit" Then
                    ReDim Preserve delChkData(NoCount)
                    delChkData(NoCount).No = row.Cells(2).Text
                    delChkData(NoCount).PaymentMethod = row.Cells(6).Text
                    delChkData(NoCount).CardType = row.Cells(12).Text
                    NoCount = NoCount + 1
                End If
            End If
        Next

        If delKey = "" Then
            Call showMsg("Please check to be deleted.")
            Exit Sub
        End If

        'Cash/Creditの場合、対でチェックが入っているか確認
        If delChkData IsNot Nothing Then

            '偶数チェック
            If delChkData.Length Mod 2 <> 0 Then
                Call showMsg("If Payment Method is Cash / Credit, check with pair.")
                Exit Sub
            End If

            '対のチェック
            '※NOの差が1で、小さいNOの方がcashのデータであること
            For i = 0 To delChkData.Length - 1

                'iが奇数のとき対確認
                If i Mod 2 <> 0 Then
                    If Convert.ToInt32(delChkData(i - 1).No) - Convert.ToInt32(delChkData(i).No) <> -1 Then
                        Call showMsg("If Payment Method is Cash / Credit, check with pair.")
                        Exit Sub
                    Else
                        '対の最初の登録はcashのため、カードタイプに記載があれば、エラー
                        If delChkData(i - 1).CardType <> "&nbsp;" Then
                            Call showMsg("If Payment Method is Cash / Credit, check with pair.")
                            Exit Sub
                        End If
                    End If

                End If

            Next i

        End If

        '最後のカンマ外す
        delKey = Left(delKey, delKey.Length - 1)

        '***削除登録***
        Dim clsSet As New Class_analysis
        Dim errFlg As Integer
        Dim clsSetCommon As New Class_common
        Dim dtNow As DateTime = clsSetCommon.dtIndia

        '請求日の設定
        Dim invoiceDate As String
        invoiceDate = setYear & "/" & setMon & "/" & setDay

        '削除フラグの設定
        Call clsSet.setCashTrack2(delKey, invoiceDate, shipCode, userid, errFlg)

        If errFlg = 1 Then
            Call showMsg("An attempt to delete an unnecessary claim number failed.")
            Exit Sub
        Else
            Call showMsg("The deletion process is completed.")
        End If

        Call showData()

    End Sub

    'GridViewの行セルの色を設定（削除対象の行セルにグレーを設定）
    Private Sub GridInfo_RowDataBound(sender As Object, e As GridViewRowEventArgs) Handles GridInfo.RowDataBound

        Try

            If GridInfo.DataSource IsNot Nothing Then

                '題名は除く
                If (e.Row.Cells(1).Text) <> "FALSE" Then

                    If (e.Row.Cells(1).Text) <> "&nbsp;" Then

                        Dim intChk As Integer

                        '削除対象はグレー色のセルに設定
                        If Integer.TryParse(Trim(e.Row.Cells(1).Text), intChk) = True Then

                            Dim num As Integer = Integer.Parse(e.Row.Cells(1).Text)

                            If num = 1 Then
                                e.Row.BackColor = Color.LightGray
                                e.Row.ForeColor = Color.Gray
                            End If

                        End If

                    End If

                End If

            End If
        Catch ex As System.ArgumentOutOfRangeException
            '次ページのデータ確認時のエラーをスルーさせる
        Catch ex As Exception
            Call showMsg(ex.Message & " Cell setting to be deleted failed")
        End Try

    End Sub

    Private Sub GridInfo_PageIndexChanging(sender As Object, e As GridViewPageEventArgs) Handles GridInfo.PageIndexChanging

        GridInfo.PageIndex = e.NewPageIndex
        GridInfo.DataSource = Session("Data_DT_SERCH")
        GridInfo.DataBind()

    End Sub
End Class



