Public Class Analysis_Cash_Tracking_qg
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
            Dim userLevel As String = Session("user_level")
            Dim adminFlg As Boolean = Session("admin_Flg")
            Dim setShipname As String = Session("ship_Name")
            Dim shipCode As String = Session("ship_code")
            Dim userName As String = Session("user_Name")

            Dim clsCommon As New Class_common
            Dim dtNow As DateTime = clsCommon.dtIndia
            Dim workDay As Integer
            Dim workMonth As Integer
            Dim i As Integer

            '***表示の設定***
            lblLoc.Text = setShipname
            lblName.Text = userName
            TextDeposi.Enabled = False

            If (userLevel = "0" Or userLevel = "1" Or userLevel = "2" Or adminFlg = True) Then

                lblMonNow.Visible = False
                DropDownActiveMonth.Visible = True

                '**ActiveMonthのリスト設定
                '今月
                workMonth = Convert.ToInt32(dtNow.ToShortDateString.Substring(5, 2))
                DropDownActiveMonth.Items.Clear()

                '管理者が選択できる月を設定
                For i = 1 To workMonth
                    '1月から今月まで
                    DropDownActiveMonth.Items.Add(i)
                Next i

            Else
                '**ActiveMonthのラベル設定
                DropDownActiveMonth.Visible = False
                lblMonNow.Visible = True
                lblMonNow.Text = dtNow.ToShortDateString.Substring(5, 2)
            End If

            '■Open用リスト
            '***表示したいWarrantyリストの設定***
            DropDownWarranty.Items.Clear()

            With DropDownWarranty
                .Items.Add("ALL")
                .Items.Add("OOW")
                .Items.Add("IW")
                .Items.Add("Other")
            End With

            '***monthリストの設定***
            DropDownMonth.Items.Clear()

            With DropDownMonth
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

            '***dayリストの設定***
            DropDownDay.Items.Clear()

            '日
            For i = 1 To 31
                If i.ToString.Length = 1 Then
                    DropDownDay.Items.Add("0" & i.ToString())
                Else
                    DropDownDay.Items.Add(i.ToString())
                End If
            Next i

            DropDownYear.Items.Clear()
            With DropDownYear
                .Items.Add("2018")
                .Items.Add("2019")
                .Items.Add("2020")
                .Items.Add("2021")
                .Items.Add("2022")
                .Items.Add("2023")
                .Items.Add("2024")
                .Items.Add("2025")
                .Items.Add("2026")
                .Items.Add("2027")
                .Items.Add("2028")
                .Items.Add("2029")
                .Items.Add("2030")
            End With

            '■登録用リスト
            '***日付リストの設定***
            DropDownDay2.Items.Clear()

            '本日日付
            workDay = Convert.ToInt32(dtNow.ToShortDateString.Substring(8, 2))

            With DropDownDay2
                If (userLevel = "0" Or userLevel = "1" Or userLevel = "2" Or adminFlg = True) Then
                    '管理者が選択できる日付を設定
                    'デフォルトは１月なので、31日まで。
                    For i = 1 To 31
                        If i.ToString.Length = 1 Then
                            DropDownDay2.Items.Add("0" & i.ToString())
                        Else
                            DropDownDay2.Items.Add(i.ToString())
                        End If
                    Next i
                Else
                    '管理者以外が選択できる日付を設定
                    DropDownDay2.Items.Add(workDay.ToString("00"))
                End If

            End With

            '***Warrantyリストの設定***
            DropDownWarranty2.Items.Clear()

            With DropDownWarranty2
                .Items.Add("OOW")
                .Items.Add("IW")
                .Items.Add("Other")
                .Items.Add("deposit")
            End With

            '■input用リスト
            '***Card Typeリストの設定***
            DropDownCard.Items.Clear()

            'card名称取得
            Dim sqlStr As String = String.Empty
            Dim cardName() As String
            Dim dsM_group As New DataSet
            Dim errFlg As Integer

            sqlStr = "SELECT * FROM dbo.M_group WHERE DELFG = 0 AND group_no = '7';"
            dsM_group = DBCommon.Get_DS(sqlStr, errFlg)

            If errFlg = 1 Then
                Call showMsg("Failed to acquire card name.", "")
                Exit Sub
            End If

            If dsM_group IsNot Nothing Then

                If dsM_group.Tables(0).Rows.Count <> 0 Then

                    ReDim cardName(dsM_group.Tables(0).Rows.Count - 1)

                    For i = 0 To dsM_group.Tables(0).Rows.Count - 1

                        Dim dr1 As DataRow = dsM_group.Tables(0).Rows(i)

                        If dr1("item_1") IsNot DBNull.Value Then
                            cardName(i) = dr1("item_1")
                        End If

                    Next i

                End If

                With DropDownCard
                    .Items.Add("Select Card Type")
                    For i = 0 To cardName.Length - 1
                        .Items.Add(cardName(i))
                    Next i
                End With

            End If

        Else

            '***月日付の指定をセッション変数に設定***
            'send用
            Dim Warranty2 As String = DropDownWarranty2.Text
            Dim setDay2 As String = DropDownDay2.Text

            Session("set_Warranty2") = Warranty2
            Session("set_Day2") = setDay2

            'ActiveMonthの設定
            Dim setMon2 As String
            If DropDownActiveMonth.Visible = True Then
                setMon2 = DropDownActiveMonth.Text
                If setMon2.Length = 1 Then
                    setMon2 = "0" & setMon2
                End If
            Else
                setMon2 = lblMonNow.Text
            End If

            Session("set_Mon2") = setMon2

            'open用
            Dim Warranty As String = DropDownWarranty.Text
            Dim setMon As String = (DropDownMonth.SelectedIndex + 1).ToString("00")
            Dim setDay As String = DropDownDay.Text
            Dim setYear As String = DropDownYear.Text


            Session("set_Warranty") = Warranty
            Session("set_Month") = setMon
            Session("set_Day") = setDay
            Session("set_Year") = setYear

            '***金額表示の自動設定***
            Dim decimalChk As Decimal
            Dim intgerChk As Integer

            '**claimの自動/手動設定**
            '※discountのとき、最初の1回目のみ、自動設定で2回目以降は修正可能にする。
            'IWのとき
            If Warranty2 = "IW" Then
                TextClaim.Text = Trim(TextTotalAmount.Text)
            Else

                Dim cntDiscountInput As Integer = Session("cntDiscount_Input")

                '※discount時、お客様に請求する金額を設定(Total Amountからdiscount料金を引く)
                Dim tmpDiscount As Decimal
                Dim tmpAmount As Decimal

                If Trim(TextTotalAmount.Text) = "" Then
                    tmpAmount = 0.00
                Else
                    If Decimal.TryParse(Trim(TextTotalAmount.Text), decimalChk) = False Then
                        TextTotalAmount.BackColor = System.Drawing.Color.FromArgb(255, 0, 0)
                        Call showMsg("Please enter the amount. There is no decimal point.", "")
                    Else
                        tmpAmount = Convert.ToDecimal(TextTotalAmount.Text)
                    End If
                End If

                If Trim(TextDiscount.Text) = "" Then
                    tmpDiscount = 0.00
                Else
                    If Decimal.TryParse(Trim(TextDiscount.Text), decimalChk) = False Then
                        TextDiscount.BackColor = System.Drawing.Color.FromArgb(255, 0, 0)
                        Call showMsg("Please enter the amount. There is no decimal point.", "")
                    Else
                        tmpDiscount = Convert.ToDecimal(TextDiscount.Text)
                    End If

                    If tmpAmount <> 0.00 Then

                        cntDiscountInput = cntDiscountInput + 1
                        Session("cntDiscount_Input") = cntDiscountInput

                        If cntDiscountInput = 1 Then

                            'textClaim,TextClaimCreditとlblDisClaimの設定
                            lblDisClaim.Text = tmpAmount - tmpDiscount

                            If ChkCash.Checked = True And ChkCredit.Checked = False Then

                                TextClaim.Text = tmpAmount - tmpDiscount

                                Dim workDelimit() As String
                                workDelimit = Split(TextClaim.Text, ".")

                                If workDelimit.Length <> 1 Then
                                    lblDisClaim.Text = workDelimit(0)
                                    TextClaim.Text = workDelimit(0)
                                End If

                            End If

                            If ChkCash.Checked = False And ChkCredit.Checked = True Then
                                TextClaimCredit.Text = tmpAmount - tmpDiscount
                            End If

                        Else

                            'lblDisClaimの設定　※自動設定のみ
                            lblDisClaim.Text = tmpAmount - tmpDiscount

                            If ChkCash.Checked = True And ChkCredit.Checked = False Then

                                Dim workDelimit() As String
                                workDelimit = Split(lblDisClaim.Text, ".")

                                If workDelimit.Length <> 1 Then
                                    lblDisClaim.Text = workDelimit(0)
                                End If

                            End If

                        End If

                    End If

                End If

            End If

            '**changeの自動設定（お預かりした金額からclaimを引く）**
            Dim tmpDeposi As Integer
            Dim tmpClaim As Decimal

            If Trim(TextDeposi.Text) = "" Then
                tmpDeposi = 0
            Else
                If Int32.TryParse(Trim(TextDeposi.Text), intgerChk) = False Then
                    TextDeposi.BackColor = System.Drawing.Color.FromArgb(255, 0, 0)
                    Call showMsg("Please enter the amount.", "")
                Else
                    tmpDeposi = Convert.ToInt32(TextDeposi.Text)
                End If
            End If

            If Trim(TextClaim.Text) = "" Then
                tmpClaim = 0.00
            Else
                If Decimal.TryParse(Trim(TextClaim.Text), decimalChk) = False Then
                    TextClaim.BackColor = System.Drawing.Color.FromArgb(255, 0, 0)
                    Call showMsg("Please enter the amount. There is no decimal point.", "")
                Else
                    tmpClaim = Convert.ToDecimal(TextClaim.Text)
                End If
            End If

            If tmpDeposi <> 0 And tmpClaim <> 0.00 Then
                If tmpDeposi - tmpClaim <> 0 Or tmpDeposi - tmpClaim <> 0.00 Then
                    lblChange.Text = tmpDeposi - tmpClaim
                Else
                    lblChange.Text = "0"
                End If
            End If

            '***どのボタンが押下されたか確認***
            Dim BtnCancelChk As String = ""
            Dim BtnOKChk As String = ""

            'invoice amountとcollect amountに差異があるとき
            For Each s In Context.Request.Form.AllKeys
                If s.Contains("BtnCancel") Then
                    BtnCancelChk = "BtnCancelOn"
                    Exit For
                End If
            Next s

            If (BtnCancelChk = "BtnCancelOn") Then
                Call showMsg("Process canceled.", "")
                Exit Sub
            End If

            For Each s In Context.Request.Form.AllKeys
                If s.Contains("BtnOK") Then
                    BtnOKChk = "BtnOKOn"
                    Exit For
                End If
            Next s

            If (BtnOKChk = "BtnOKOn") Then
                touroku()
            End If

        End If

    End Sub

    Private Sub DropDownMonth_SelectedIndexChanged(sender As Object, e As EventArgs) Handles DropDownMonth.SelectedIndexChanged

        Dim endday As Integer
        Dim month As Integer

        If DropDownMonth.Text <> " " Then
            month = DropDownMonth.SelectedIndex
        End If

        Select Case month
            Case 0, 2, 4, 6, 7, 9, 11
                endday = 31
            Case 3, 5, 8, 10
                endday = 30
            Case 1
                endday = 29
            Case Else
                endday = 31
        End Select

        Dim indexSelected As Integer = DropDownDay.SelectedIndex
        DropDownDay.Items.Clear()

        For i As Integer = 1 To endday
            If i.ToString.Length = 1 Then
                DropDownDay.Items.Add("0" & i.ToString())
            Else
                DropDownDay.Items.Add(i.ToString())
            End If
        Next

        If indexSelected <= endday - 1 Then
            DropDownDay.SelectedIndex = indexSelected
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

    Protected Sub btnOpen_Click(sender As Object, e As ImageClickEventArgs) Handles btnOpen.Click

        Response.Redirect("Analysis_Cash_Tracking2.aspx")

    End Sub
    'sendボタン押下処理
    Protected Sub btnSend_Click(sender As Object, e As ImageClickEventArgs) Handles btnSend.Click

        '***セッション情報取得***
        Dim Warranty2 As String = Session("set_Warranty2")
        Dim setDay2 As String = Session("set_Day2")
        Dim setMon2 As String = Session("set_Mon2")
        Dim shipCode As String = Session("ship_code")
        Dim userid As String = Session("user_id")
        Dim userLevel As String = Session("user_level")
        Dim adminFlg As Boolean = Session("admin_Flg")

        If userid = "" Then
            Call showMsg("The session was cleared. Please login again.", "")
            Exit Sub
        End If

        Call reSetText()

        '***登録可能チェック***
        Dim errFlg As Integer
        Dim closeChk As String
        Dim strSQL As String

        If (userLevel = "0" Or userLevel = "1" Or userLevel = "2" Or adminFlg = True) Then
            '登録可能
        Else
            '締め処理が終わっているか確認
            strSQL = "SELECT * FROM dbo.M_ship_base WHERE DELFG = 0 AND ship_code = '" & shipCode & "';"

            Dim DT_M_ship_base As DataTable = DBCommon.ExecuteGetDT(strSQL, errFlg)

            If errFlg = 1 Then
                Call showMsg("Failed to acquire M_ship_base information.", "")
                Exit Sub
            End If

            If DT_M_ship_base IsNot Nothing Then

                If DT_M_ship_base.Rows(0)("open_time") IsNot DBNull.Value Then
                    closeChk = DT_M_ship_base.Rows(0)("open_time")
                End If

            End If

            If closeChk = "False" Then
                Call showMsg("The confirmation report has completed, so it can not be registered. ", "")
                Exit Sub
            End If

        End If

        '***入力チェック***
        '**payment**
        If Warranty2 <> "IW" Then
            If ChkCash.Checked = False And ChkCredit.Checked = False Then
                Call showMsg("Please check payment. ", "")
                Exit Sub
            End If
        End If

        '**Warranty毎の必須入力項目チェック**
        If Warranty2 = "OOW" Then

            If ChkFullDiscount.Checked = True Then
                If TextClaimNo.Text = "" Or TextCustomerName.Text = "" Or TextTotalAmount.Text = "" Then
                    Call textCheck(Warranty2)
                    Call showMsg("Please specify all items.", "")
                    Exit Sub
                End If
            Else
                If TextClaimNo.Text = "" Or TextCustomerName.Text = "" Or (TextClaim.Text = "" And TextClaimCredit.Text = "") Or TextTotalAmount.Text = "" Or (TextClaimCredit.Text = "" And ChkCredit.Checked = True) Or (TextPONo.Text = "" And ChkCredit.Checked = True) Or (DropDownCard.Text = "Select Card Type" And ChkCredit.Checked = True) Then
                    Call textCheck(Warranty2)
                    Call showMsg("Please specify all items.", "")
                    Exit Sub
                End If
            End If

        ElseIf Warranty2 = "IW" Then

            If TextClaimNo.Text = "" Or TextCustomerName.Text = "" Or TextTotalAmount.Text = "" Then
                Call textCheck(Warranty2)
                Call showMsg("Please specify the item.", "")
                Exit Sub
            End If

        ElseIf Warranty2 = "Other" Then

            If TextClaimNo.Text = "" Or (TextClaim.Text = "" And TextClaimCredit.Text = "") Or TextCustomerName.Text = "" Or TextTotalAmount.Text = "" Or (TextClaimCredit.Text = "" And ChkCredit.Checked = True) Or (TextPONo.Text = "" And ChkCredit.Checked = True) Or (DropDownCard.Text = "Select Card Type" And ChkCredit.Checked = True) Then
                Call textCheck(Warranty2)
                Call showMsg("Please specify the item.", "")
                Exit Sub
            End If

        ElseIf Warranty2 = "deposit" Then

            If TextClaim.Text = "" Then
                Call textCheck(Warranty2)
                Call showMsg("Please specify the item.", "")
                Exit Sub
            End If

        End If

        '****

        Dim decimalChk As Decimal
        '**amount欄の数値チェック**
        If TextTotalAmount.Enabled = True Then
            If Decimal.TryParse(Trim(TextTotalAmount.Text), decimalChk) = False Then
                TextTotalAmount.BackColor = System.Drawing.Color.FromArgb(255, 0, 0)
                Call showMsg("Please enter the amount.", "")
                Exit Sub
            End If
        End If

        If ChkFullDiscount.Checked = False Then

            '**claim欄の数値チェック**
            Dim intChk As Integer
            If (Warranty2 = "Other" And ChkCash.Checked = True) Or (Warranty2 = "OOW" And ChkCash.Checked = True) Then
                'パイサ不要の為、整数チェック
                If TextClaim.Enabled = True Then
                    If Integer.TryParse(Trim(TextClaim.Text), intChk) = False Then
                        TextClaim.BackColor = System.Drawing.Color.FromArgb(255, 0, 0)
                        Call showMsg("Please enter the amount. Paisa can not be specified.", "")
                        Exit Sub
                    End If
                End If
            End If

            'パイサのチェック
            If ChkCredit.Checked = True Then
                If TextClaimCredit.Enabled = True Then
                    If Decimal.TryParse(Trim(TextClaimCredit.Text), decimalChk) = False Then
                        TextClaimCredit.BackColor = System.Drawing.Color.FromArgb(255, 0, 0)
                        Call showMsg("Please enter the amount.", "")
                        Exit Sub
                    End If
                End If
            End If

            '**cash指定時のチェック**
            If ChkCash.Checked = True And Warranty2 <> "deposit" Then
                If TextDeposi.Text = "" Then
                    TextDeposi.BackColor = System.Drawing.Color.FromArgb(255, 0, 0)
                    Call showMsg("Please specify Deposit.", "")
                    Exit Sub
                Else
                    If TextDeposi.Enabled = True Then
                        'deposi欄の数値チェック
                        If Integer.TryParse(Trim(TextDeposi.Text), intChk) = False Then
                            TextDeposi.BackColor = System.Drawing.Color.FromArgb(255, 0, 0)
                            Call showMsg("Please enter the amount. Paisa can not be specified.", "")
                            Exit Sub
                        Else
                            If Convert.ToInt32(TextDeposi.Text) < Convert.ToInt32(TextClaim.Text) Then
                                TextDeposi.BackColor = System.Drawing.Color.FromArgb(255, 0, 0)
                                Call showMsg("The deposit amount is insufficient.", "")
                                Exit Sub
                            End If
                        End If
                    End If
                End If
            End If

        End If

        '**deposit(リストの選択の方)金額のチェック**
        If Warranty2 = "deposit" Then

            'open処理が始まっているか確認
            Dim clsSetCommon As New Class_common
            Dim dtNow As DateTime = clsSetCommon.dtIndia
            Dim clsSet As New Class_money
            Dim opentime As String = ""
            Dim closetime As String = ""
            Dim errMsg As String

            If clsSet.chkOpen(shipCode, opentime, closetime, errMsg) = False Then
                Call showMsg("Since OPEN processing has not been started, deposit processing can not be performed.", "")
                Exit Sub
            End If

            If errMsg <> "" Then
                Call showMsg(errMsg, "")
                Exit Sub
            End If

            'open処理の金額を取得　※確認者のチェックあり
            Dim openTotal As Decimal
            strSQL = "SELECT top 1 * FROM dbo.T_Reserve WHERE DELFG = 0 AND status = 'open' AND ship_code = '" & shipCode & "' "
            strSQL &= "AND (conf_user <> '' AND conf_user is not null) AND LEFT(CONVERT(VARCHAR, datetime, 111), 10) = '" & dtNow.ToShortDateString & "' "
            strSQL &= "ORDER BY datetime DESC;"

            Dim DT_T_Reserve As DataTable = DBCommon.ExecuteGetDT(strSQL, errFlg)

            If errFlg = 1 Then
                Call showMsg("From T_Reserve, failed to acquire amount of OPEN processing.", "")
                Exit Sub
            End If

            If DT_T_Reserve IsNot Nothing Then

                If DT_T_Reserve.Rows(0)("total") IsNot DBNull.Value Then
                    openTotal = DT_T_Reserve.Rows(0)("total")
                End If

            End If

            '登録済のdeposit金額を取得
            Dim sumDeposit As Decimal
            strSQL = "SELECT CONVERT(decimal(13,2),SUM(claim)) AS SUM_deposit FROM dbo.cash_track WHERE DELFG = 0 AND location = '" & shipCode & "' "
            strSQL &= "AND Warranty = 'deposit' AND FALSE = '0' AND LEFT(CONVERT(VARCHAR, invoice_date, 111), 10) = '" & dtNow.ToShortDateString.Substring(0, 5) & setMon2 & "/" & setDay2 & "'"

            Dim DT_cash_track As DataTable = DBCommon.ExecuteGetDT(strSQL, errFlg)

            If errFlg = 1 Then
                Call showMsg("Failed to acquire the deposit total amount from cash_track.", "")
                Exit Sub
            End If

            If DT_cash_track IsNot Nothing Then

                If DT_cash_track.Rows(0)("SUM_deposit") IsNot DBNull.Value Then
                    sumDeposit = DT_cash_track.Rows(0)("SUM_deposit")
                End If

            End If

            '登録済のdepositとこれから登録する金額の合計が、OPEN処理の金額を超えていないか確認
            Dim work As Decimal
            If TextClaim.Text <> "" Then
                work = Convert.ToDecimal(TextClaim.Text)
            End If

            work = sumDeposit + work

            If work > openTotal Then
                Call showMsg("It exceeds the limit amount.", "")
                Exit Sub
            End If

        End If

        '**FullDiscount/Discountチェック**
        Dim msg As String = Trim(TextMessage.Text)

        If ChkFullDiscount.Checked = True Then

            If Trim(TextClaim.Text) <> "" Then
                If Trim(TextClaim.Text) <> "0" Then
                    Call showMsg("Since the Discount is checked, you can not enter the amount in Claim.", "")
                    Exit Sub
                End If
            End If

            If ChkCredit.Checked = True Then
                Call showMsg("Since the Discount is checked, Credit can not be selected.", "")
                Exit Sub
            End If

            If Left(msg, 8) <> "discount" Then
                Call showMsg("There is no input of discount in the message field.", "")
                Exit Sub
            End If

        Else
            'discount欄に金額の設定あり
            If Trim(TextDiscount.Text) <> "" Then

                If Left(msg, 8) <> "discount" Then
                    Call showMsg("There is no input of discount in the message field.", "")
                    Exit Sub
                End If

                If TextDiscount.Text <> "0" Or TextDiscount.Text <> "0.00" Then

                    Dim workDiscount As Decimal
                    Dim workClaim As Decimal
                    Dim workCreditClaim As Decimal
                    Dim disClaim As Decimal

                    workDiscount = Trim(TextDiscount.Text)

                    If Trim(TextClaim.Text) <> "" Then
                        workClaim = Trim(TextClaim.Text)
                    Else
                        workClaim = 0
                    End If

                    If Trim(TextClaimCredit.Text) <> "" Then
                        workCreditClaim = Trim(TextClaimCredit.Text)
                    Else
                        workCreditClaim = 0
                    End If

                    disClaim = Trim(lblDisClaim.Text)

                    If ChkCash.Checked = True And ChkCredit.Checked = True Then
                        If disClaim <> (workClaim + workCreditClaim) Then
                            Call showMsg("Please check the amount after discount.", "")
                            Exit Sub
                        End If
                    Else
                        If ChkCash.Checked = True Then
                            If disClaim <> workClaim Then
                                Call showMsg("Please check the amount after discount.", "")
                                Exit Sub
                            End If
                        ElseIf ChkCredit.Checked = True Then
                            If disClaim <> workCreditClaim Then
                                Call showMsg("Please check the amount after discount.", "")
                                Exit Sub
                            End If
                        End If
                    End If

                End If

            End If

        End If

        '**invoice amount(total amount)とcollect amount(claim)の相違チェック**
        If ChkFullDiscount.Checked = False And Warranty2 <> "deposit" Then

            Dim tmpInvoice, tmpCollect, tmpCredit As Decimal

            If TextTotalAmount.Text <> "" Then
                tmpInvoice = Convert.ToDecimal(TextTotalAmount.Text)
            End If

            If TextClaim.Text <> "" Then
                tmpCollect = Convert.ToDecimal(TextClaim.Text)
            End If

            If TextClaimCredit.Text <> "" Then
                tmpCredit = Convert.ToDecimal(TextClaimCredit.Text)
            End If

            If tmpCollect <> 0 And tmpCredit <> 0 Then

                If tmpInvoice <> tmpCollect + tmpCredit Then
                    Call showMsg("The amount of money does not match the amount of the invoice amount.<br />Are you sure you want to register this way?<br />OK:Registration Cancel:Edit", "CancelMsg")
                    Exit Sub
                End If

            Else

                If tmpCollect <> 0 Then
                    If tmpInvoice <> tmpCollect Then
                        Call showMsg("The amount of money does not match the amount of the invoice amount.<br />Are you sure you want to register this way?<br />OK:Registration Cancel:Edit", "CancelMsg")
                        Exit Sub
                    End If
                End If

                If tmpCredit <> 0 Then
                    If tmpInvoice <> tmpCredit Then
                        Call showMsg("The amount of money does not match the amount of the invoice amount.<br />Are you sure you want to register this way?<br />OK:Registration Cancel:Edit", "CancelMsg")
                        Exit Sub
                    End If
                End If

            End If

        End If

        Call touroku()

    End Sub
    Protected Sub touroku()

        '***セッション情報取得***
        Dim Warranty2 As String = Session("set_Warranty2")
        Dim shipCode As String = Session("ship_code")
        Dim userid As String = Session("user_id")
        Dim userName As String = Session("user_Name")
        Dim setDay2 As String = Session("set_Day2")
        Dim setMon2 As String = Session("set_Mon2")

        If userid = "" Then
            Call showMsg("The session has expired. Please login again.", "")
            Exit Sub
        End If

        '***設定***
        Dim clsCommon As New Class_common
        Dim dtNow As DateTime = clsCommon.dtIndia
        Dim dt As DateTime
        Dim cashTrackData As Class_analysis.CASH_TRACK

        If DropDownCard.Text = "Select Card Type" Then
            cashTrackData.card_type = ""
        Else
            cashTrackData.card_type = DropDownCard.Text
        End If

        cashTrackData.claim_no = Trim(TextClaimNo.Text)
        cashTrackData.customer_name = Trim(TextCustomerName.Text)
        cashTrackData.card_number = Trim(TextPONo.Text)
        cashTrackData.message = Trim(TextMessage.Text)

        'セッション情報より設定
        cashTrackData.Warranty = Warranty2
        cashTrackData.input_user = userName
        cashTrackData.location = shipCode

        '請求日の設定
        Dim tmp As String = dtNow.ToShortDateString.Substring(0, 5) & setMon2 & "/" & setDay2
        If DateTime.TryParse(tmp, dt) Then
            cashTrackData.invoice_date = DateTime.Parse(tmp)
        End If

        'TotalAmountの設定
        If Trim(TextTotalAmount.Text) = "" Then
            cashTrackData.total_amount = 0.00
        Else
            cashTrackData.total_amount = Trim(TextTotalAmount.Text)
        End If

        'Deposiの設定
        If Trim(TextDeposi.Text) = "" Then
            cashTrackData.deposit = 0
        Else
            cashTrackData.deposit = Trim(TextDeposi.Text)
        End If

        'claimの設定
        If Warranty2 = "IW" Then
            cashTrackData.claim = cashTrackData.total_amount
        Else

            If Trim(TextClaim.Text) = "" Then
                cashTrackData.claim = 0.00
            Else
                cashTrackData.claim = Trim(TextClaim.Text)
            End If

            If Trim(TextClaimCredit.Text) = "" Then
                cashTrackData.claimCredit = 0.00
            Else
                cashTrackData.claimCredit = Trim(TextClaimCredit.Text)
            End If

        End If

        'payment/payment_kindの設定
        If ChkCash.Checked = True And ChkCredit.Checked Then
            cashTrackData.payment_kind = "1"
        Else
            If ChkCash.Checked = True Then
                cashTrackData.payment = "Cash"
            ElseIf ChkCredit.Checked = True Then
                cashTrackData.payment = "Credit"
            End If
        End If

        'discountの設定
        If ChkFullDiscount.Checked = True Then
            'fulldiscountの設定
            cashTrackData.discount = cashTrackData.total_amount
            cashTrackData.discount_after_amt = 0.00
            cashTrackData.claim = 0
            cashTrackData.full_discount = 1
        Else
            If Trim(TextDiscount.Text) = "" Then
                cashTrackData.discount = 0.00
            Else
                '通常discountの設定
                cashTrackData.discount = Trim(TextDiscount.Text)
                'cash
                If ChkCash.Checked = True Then
                    cashTrackData.discount_after_amt = cashTrackData.claim
                End If
                'credit
                If ChkCredit.Checked = True Then
                    cashTrackData.discount_after_amt_credit = cashTrackData.claimCredit
                End If
            End If
            'fulldiscount以外
            cashTrackData.full_discount = 0
        End If

        'changeの設定（お預かりした金額からclaimを引く）
        If cashTrackData.deposit <> 0 Then
            cashTrackData.change = cashTrackData.deposit - cashTrackData.claim
        End If

        '***通し番号取得（全ての拠点で）***
        Dim errFlg As Integer
        Dim sqlStr As String
        Dim dscash_track As New DataSet

        'Comment by Mohan on 2019 Feb 03
        'sqlStr = "SELECT max(count_no) AS count_no FROM dbo.cash_track WHERE DELFG = 0; "
        sqlStr = "SELECT max(count_no) AS count_no FROM dbo.cash_track "
        dscash_track = DBCommon.Get_DS(sqlStr, errFlg)

        If errFlg = 1 Then
            Call showMsg("Failed to acquire the serial number from cash_track.", "")
            Exit Sub
        End If

        If dscash_track IsNot Nothing Then

            If dscash_track.Tables(0).Rows.Count = 1 Then

                Dim dr As DataRow = dscash_track.Tables(0).Rows(0)

                If dr("count_no") IsNot DBNull.Value Then
                    cashTrackData.count_no = dr("count_no")
                    cashTrackData.count_no = cashTrackData.count_no + 1
                Else
                    cashTrackData.count_no = 1
                End If

            End If

        End If

        '***登録***
        Dim clsSet As New Class_analysis
        Call clsSet.setCashTrack(cashTrackData, userid, errFlg)

        If errFlg = 1 Then
            ListMsg.Items.Add("failed…retry   " & dtNow)
            Call showMsg("Failed to register cash_track.", "")
            Session("cntDiscount_Input") = Nothing
            Exit Sub
        Else
            ListMsg.Items.Add("count_no:" & cashTrackData.count_no & "   (new) success!  " & dtNow & "  claimNo:" & cashTrackData.claim_no & "  Total Amount:" & cashTrackData.total_amount & "  Card Type:" & cashTrackData.card_type)

            If Warranty2 = "deposit" Then
                Call showMsg("success! Please complete the remittance.", "")
            Else
                Call showMsg("Registration has been completed.", "")
            End If

        End If

        Call reSetText2()

    End Sub

    Protected Sub textCheck(ByVal Warranty2 As String)

        If Warranty2 = "OOW" Then

            If TextClaimNo.Text = "" Then
                TextClaimNo.BackColor = System.Drawing.Color.FromArgb(255, 0, 0)
            End If

            If TextCustomerName.Text = "" Then
                TextCustomerName.BackColor = System.Drawing.Color.FromArgb(255, 0, 0)
            End If

            If TextTotalAmount.Text = "" Then
                TextTotalAmount.BackColor = System.Drawing.Color.FromArgb(255, 0, 0)
            End If

            If ChkFullDiscount.Checked = False Then

                If ChkCash.Checked = True Then
                    If TextClaim.Text = "" Then
                        TextClaim.BackColor = System.Drawing.Color.FromArgb(255, 0, 0)
                    End If
                End If

                If ChkCredit.Checked = True Then

                    If TextClaimCredit.Text = "" Then
                        TextClaimCredit.BackColor = System.Drawing.Color.FromArgb(255, 0, 0)
                    End If

                    If TextPONo.Text = "" Then
                        TextPONo.BackColor = System.Drawing.Color.FromArgb(255, 0, 0)
                    End If

                    If DropDownCard.Text = "Select Card Type" Then
                        DropDownCard.BackColor = System.Drawing.Color.FromArgb(255, 0, 0)
                    End If

                End If

            End If

        ElseIf Warranty2 = "IW" Then

            If TextClaimNo.Text = "" Then
                TextClaimNo.BackColor = System.Drawing.Color.FromArgb(255, 0, 0)
            End If

            If TextCustomerName.Text = "" Then
                TextCustomerName.BackColor = System.Drawing.Color.FromArgb(255, 0, 0)
            End If

            If TextTotalAmount.Text = "" Then
                TextTotalAmount.BackColor = System.Drawing.Color.FromArgb(255, 0, 0)
            End If

        ElseIf Warranty2 = "Other" Then

            If TextClaimNo.Text = "" Then
                TextClaimNo.BackColor = System.Drawing.Color.FromArgb(255, 0, 0)
            End If

            If TextCustomerName.Text = "" Then
                TextCustomerName.BackColor = System.Drawing.Color.FromArgb(255, 0, 0)
            End If

            If TextTotalAmount.Text = "" Then
                TextTotalAmount.BackColor = System.Drawing.Color.FromArgb(255, 0, 0)
            End If

            If ChkCash.Checked = True Then
                If TextClaim.Text = "" Then
                    TextClaim.BackColor = System.Drawing.Color.FromArgb(255, 0, 0)
                End If
            End If

            If ChkCredit.Checked = True Then

                If TextClaimCredit.Text = "" Then
                    TextClaimCredit.BackColor = System.Drawing.Color.FromArgb(255, 0, 0)
                End If

                If TextPONo.Text = "" Then
                    TextPONo.BackColor = System.Drawing.Color.FromArgb(255, 0, 0)
                End If

                If DropDownCard.Text = "Select Card Type" Then
                    DropDownCard.BackColor = System.Drawing.Color.FromArgb(255, 0, 0)
                End If

            End If

        ElseIf Warranty2 = "deposit" Then

            If TextClaim.Text = "" Then
                TextClaim.BackColor = System.Drawing.Color.FromArgb(255, 0, 0)
            End If

        End If

    End Sub

    Protected Sub reSetText()

        TextDeposi.BackColor = System.Drawing.Color.FromArgb(255, 255, 255)
        TextClaimNo.BackColor = System.Drawing.Color.FromArgb(255, 255, 255)
        TextClaim.BackColor = System.Drawing.Color.FromArgb(255, 255, 255)
        TextClaimCredit.BackColor = System.Drawing.Color.FromArgb(255, 255, 255)
        TextCustomerName.BackColor = System.Drawing.Color.FromArgb(255, 255, 255)
        TextTotalAmount.BackColor = System.Drawing.Color.FromArgb(255, 255, 255)
        TextPONo.BackColor = System.Drawing.Color.FromArgb(255, 255, 255)
        DropDownCard.BackColor = System.Drawing.Color.FromArgb(255, 255, 255)

    End Sub
    '登録完了後のreset処理
    Protected Sub reSetText2()

        TextClaimNo.BackColor = System.Drawing.Color.FromArgb(255, 255, 255)
        TextClaim.BackColor = System.Drawing.Color.FromArgb(255, 255, 255)
        TextClaimCredit.BackColor = System.Drawing.Color.FromArgb(255, 255, 255)
        TextCustomerName.BackColor = System.Drawing.Color.FromArgb(255, 255, 255)
        TextTotalAmount.BackColor = System.Drawing.Color.FromArgb(255, 255, 255)
        TextPONo.BackColor = System.Drawing.Color.FromArgb(255, 255, 255)
        DropDownCard.BackColor = System.Drawing.Color.FromArgb(255, 255, 255)

        '項目の色変更
        lblClaimNo.BackColor = System.Drawing.Color.FromArgb(102, 153, 255)
        lblClaim.BackColor = System.Drawing.Color.FromArgb(102, 153, 255)
        lblCustomerName.BackColor = System.Drawing.Color.FromArgb(102, 153, 255)
        lblPayment.BackColor = System.Drawing.Color.FromArgb(102, 153, 255)
        lblTotalAmount.BackColor = System.Drawing.Color.FromArgb(102, 153, 255)
        lblPoNo.BackColor = System.Drawing.Color.FromArgb(102, 153, 255)
        lblCardType.BackColor = System.Drawing.Color.FromArgb(102, 153, 255)
        lblDeposit.BackColor = System.Drawing.Color.FromArgb(102, 153, 255)
        lblChangeKoumoku.BackColor = System.Drawing.Color.FromArgb(102, 153, 255)
        ChkFullDiscount.BackColor = System.Drawing.Color.FromArgb(102, 153, 255)
        lblDiscount.BackColor = System.Drawing.Color.FromArgb(102, 153, 255)

        TextMessage.Text = ""
        TextClaimNo.Text = ""
        TextClaim.Text = ""
        TextClaimCredit.Text = ""
        TextCustomerName.Text = ""
        TextTotalAmount.Text = ""
        TextPONo.Text = ""
        TextDeposi.Text = ""
        TextDiscount.Text = ""
        lblChange.Text = ""
        lblDisClaim.Text = ""

        DropDownCard.Text = "Select Card Type"
        DropDownWarranty2.Text = "OOW"
        DropDownCard.Enabled = True

        TextPONo.Enabled = True

        TextClaim.Enabled = True
        TextClaimCredit.Enabled = True
        TextDeposi.Enabled = True
        TextDiscount.Enabled = True

        TextTotalAmount.Enabled = True
        TextCustomerName.Enabled = True
        TextClaimNo.Enabled = True

        ChkFullDiscount.Checked = False
        ChkFullDiscount.Enabled = True

        ChkCash.Checked = False
        ChkCredit.Checked = False
        ChkCash.Enabled = True
        ChkCredit.Enabled = True

        Session("cntDiscount_Input") = Nothing
        Session("set_Day2") = Nothing
        Session("set_Mon2") = Nothing

    End Sub
    'Warranty選択時
    Private Sub DropDownWarranty2_SelectedIndexChanged(sender As Object, e As EventArgs) Handles DropDownWarranty2.SelectedIndexChanged

        Dim selectIndex As Integer = DropDownWarranty2.SelectedIndex

        Call reSetText()

        Select Case selectIndex

            Case 0
                '■OOW
                DropDownCard.Enabled = True
                TextClaim.Text = ""
                TextClaim.Enabled = True
                TextClaimCredit.Enabled = True
                TextTotalAmount.Text = ""
                TextTotalAmount.Enabled = True
                TextCustomerName.Text = ""
                TextCustomerName.Enabled = True
                TextClaimNo.Text = ""
                TextClaimNo.Enabled = True
                TextDiscount.Text = ""
                TextDiscount.Enabled = True
                ChkFullDiscount.Checked = False
                ChkFullDiscount.Enabled = True
                ChkCredit.Checked = False
                ChkCredit.Enabled = True
                ChkCash.Checked = False
                ChkCash.Enabled = True

                '項目の色変更
                lblClaimNo.BackColor = System.Drawing.Color.FromArgb(102, 153, 255)
                lblClaim.BackColor = System.Drawing.Color.FromArgb(102, 153, 255)
                lblCustomerName.BackColor = System.Drawing.Color.FromArgb(102, 153, 255)
                lblPayment.BackColor = System.Drawing.Color.FromArgb(102, 153, 255)
                lblTotalAmount.BackColor = System.Drawing.Color.FromArgb(102, 153, 255)
                lblPoNo.BackColor = System.Drawing.Color.FromArgb(102, 153, 255)
                lblCardType.BackColor = System.Drawing.Color.FromArgb(102, 153, 255)
                lblDeposit.BackColor = System.Drawing.Color.FromArgb(102, 153, 255)
                lblChangeKoumoku.BackColor = System.Drawing.Color.FromArgb(102, 153, 255)
                ChkFullDiscount.BackColor = System.Drawing.Color.FromArgb(102, 153, 255)
                lblDiscount.BackColor = System.Drawing.Color.FromArgb(102, 153, 255)

            Case 1
                '■IW
                TextTotalAmount.Text = ""
                TextTotalAmount.Enabled = True
                TextCustomerName.Text = ""
                TextCustomerName.Enabled = True
                TextClaimNo.Text = ""
                TextClaimNo.Enabled = True
                DropDownCard.Text = "Select Card Type"
                DropDownCard.Enabled = False
                TextDeposi.Text = ""
                TextDeposi.Enabled = False
                lblChange.Text = ""
                lblChange.Enabled = False
                TextPONo.Text = ""
                TextPONo.Enabled = False
                TextClaim.Text = Trim(TextTotalAmount.Text)
                TextClaim.Enabled = False
                TextClaimCredit.Text = ""
                TextClaimCredit.Enabled = False
                TextDiscount.Text = ""
                TextDiscount.Enabled = False
                lblDisClaim.Text = ""
                ChkFullDiscount.Checked = False
                ChkFullDiscount.Enabled = False
                ChkCredit.Checked = False
                ChkCredit.Enabled = False
                ChkCash.Checked = False
                ChkCash.Enabled = False

                '項目の色変更
                lblClaimNo.BackColor = System.Drawing.Color.FromArgb(51, 204, 153)
                lblClaim.BackColor = System.Drawing.Color.FromArgb(51, 204, 153)
                lblCustomerName.BackColor = System.Drawing.Color.FromArgb(51, 204, 153)
                lblPayment.BackColor = System.Drawing.Color.FromArgb(51, 204, 153)
                lblTotalAmount.BackColor = System.Drawing.Color.FromArgb(51, 204, 153)
                lblPoNo.BackColor = System.Drawing.Color.FromArgb(51, 204, 153)
                lblCardType.BackColor = System.Drawing.Color.FromArgb(51, 204, 153)
                lblDeposit.BackColor = System.Drawing.Color.FromArgb(51, 204, 153)
                lblChangeKoumoku.BackColor = System.Drawing.Color.FromArgb(51, 204, 153)
                ChkFullDiscount.BackColor = System.Drawing.Color.FromArgb(51, 204, 153)
                lblDiscount.BackColor = System.Drawing.Color.FromArgb(51, 204, 153)

            Case 2
                '■Other
                DropDownCard.Enabled = True
                TextClaim.Text = ""
                TextClaim.Enabled = True
                TextClaimCredit.Enabled = True
                TextTotalAmount.Text = ""
                TextTotalAmount.Enabled = True
                TextCustomerName.Text = ""
                TextCustomerName.Enabled = True
                TextClaimNo.Text = ""
                TextClaimNo.Enabled = True
                TextDiscount.Text = ""
                TextDiscount.Enabled = False
                lblDisClaim.Text = ""
                ChkFullDiscount.Checked = False
                ChkFullDiscount.Enabled = False
                ChkCredit.Checked = False
                ChkCredit.Enabled = True
                ChkCash.Checked = False
                ChkCash.Enabled = True

                '項目の色変更
                lblClaimNo.BackColor = System.Drawing.Color.FromArgb(204, 255, 0)
                lblClaim.BackColor = System.Drawing.Color.FromArgb(204, 255, 0)
                lblCustomerName.BackColor = System.Drawing.Color.FromArgb(204, 255, 0)
                lblPayment.BackColor = System.Drawing.Color.FromArgb(204, 255, 0)
                lblTotalAmount.BackColor = System.Drawing.Color.FromArgb(204, 255, 0)
                lblPoNo.BackColor = System.Drawing.Color.FromArgb(204, 255, 0)
                lblCardType.BackColor = System.Drawing.Color.FromArgb(204, 255, 0)
                lblDeposit.BackColor = System.Drawing.Color.FromArgb(204, 255, 0)
                lblChangeKoumoku.BackColor = System.Drawing.Color.FromArgb(204, 255, 0)
                ChkFullDiscount.BackColor = System.Drawing.Color.FromArgb(204, 255, 0)
                lblDiscount.BackColor = System.Drawing.Color.FromArgb(204, 255, 0)

            Case 3
                '■deposit
                Dim clsCommon As New Class_common
                Dim dtNow As DateTime = clsCommon.dtIndia

                '***セッション情報取得***
                Dim userName As String = Session("user_Name")
                Dim shipCode As String = Session("ship_code")
                Dim setDay2 As String = Session("set_Day2")
                Dim setMon2 As String = Session("set_Mon2")

                '***登録日付けを確認***（本日のみOK）
                Dim dt As DateTime
                Dim tmpStr As String = dtNow.ToShortDateString.Substring(0, 5) & setMon2 & "/" & setDay2
                Dim tmpDate As DateTime

                If DateTime.TryParse(tmpStr, dt) Then
                    tmpDate = DateTime.Parse(tmpStr)
                End If

                If tmpDate.ToShortDateString <> dtNow.ToShortDateString Then
                    Call showMsg("Deposit processing can be processed today's date.", "")
                    Call reSetText2()
                    Exit Sub
                End If

                '***deposit選択時、claim_noの最終番号を取得***
                Dim maxNo As Integer
                Dim sqlStr As String
                Dim dscash_track As New DataSet
                Dim errFlg As Integer

                sqlStr = "SELECT MAX(CONVERT(INT,(RIGHT(claim_no,LEN(claim_no)-11)))) AS MAX_ClaimNo FROM dbo.cash_track WHERE DELFG = 0 "
                sqlStr &= "AND Warranty = 'deposit' AND location = '" & shipCode & "' AND LEFT(CONVERT(VARCHAR, invoice_date, 111), 10) = '" & dtNow.ToShortDateString.Substring(0, 5) & setMon2 & "/" & setDay2 & "';"
                dscash_track = DBCommon.Get_DS(sqlStr, errFlg)

                If errFlg = 1 Then
                    Call showMsg("Failed to acquire the serial number from cash_track.", "")
                    Exit Sub
                End If

                If dscash_track IsNot Nothing Then

                    If dscash_track.Tables(0).Rows.Count = 1 Then

                        Dim dr As DataRow = dscash_track.Tables(0).Rows(0)

                        If dr("MAX_ClaimNo") IsNot DBNull.Value Then
                            maxNo = dr("MAX_ClaimNo")
                            maxNo = maxNo + 1
                        Else
                            maxNo = 1
                        End If

                    End If

                End If

                DropDownCard.Text = "Select Card Type"
                DropDownCard.Enabled = False
                TextDeposi.Text = ""
                TextDeposi.Enabled = False
                lblChange.Text = ""
                lblChange.Enabled = False
                TextPONo.Text = ""
                TextPONo.Enabled = False
                TextClaim.Text = ""
                TextClaimCredit.Text = ""
                TextClaimCredit.Enabled = False
                TextDiscount.Text = ""
                TextDiscount.Enabled = False
                lblDisClaim.Text = ""
                TextTotalAmount.Text = ""
                TextTotalAmount.Enabled = False
                TextCustomerName.Text = userName
                TextCustomerName.Enabled = False
                TextClaimNo.Text = dtNow.ToShortDateString & "_" & maxNo
                TextClaimNo.Enabled = False
                ChkFullDiscount.Checked = False
                ChkFullDiscount.Enabled = False
                ChkCredit.Checked = False
                ChkCredit.Enabled = False
                ChkCash.Checked = True
                ChkCash.Enabled = False

                '項目の色変更
                lblClaimNo.BackColor = System.Drawing.Color.FromArgb(255, 153, 102)
                lblClaim.BackColor = System.Drawing.Color.FromArgb(255, 153, 102)
                lblCustomerName.BackColor = System.Drawing.Color.FromArgb(255, 153, 102)
                lblPayment.BackColor = System.Drawing.Color.FromArgb(255, 153, 102)
                lblTotalAmount.BackColor = System.Drawing.Color.FromArgb(255, 153, 102)
                lblPoNo.BackColor = System.Drawing.Color.FromArgb(255, 153, 102)
                lblCardType.BackColor = System.Drawing.Color.FromArgb(255, 153, 102)
                lblDeposit.BackColor = System.Drawing.Color.FromArgb(255, 153, 102)
                lblChangeKoumoku.BackColor = System.Drawing.Color.FromArgb(255, 153, 102)
                ChkFullDiscount.BackColor = System.Drawing.Color.FromArgb(255, 153, 102)
                lblDiscount.BackColor = System.Drawing.Color.FromArgb(255, 153, 102)

        End Select

    End Sub

    Private Sub DropDownActiveMonth_SelectedIndexChanged(sender As Object, e As EventArgs) Handles DropDownActiveMonth.SelectedIndexChanged

        Dim endday As Integer
        Dim month As Integer

        If DropDownActiveMonth.Text <> " " Then
            month = DropDownActiveMonth.SelectedIndex
        End If

        '本日日付
        Dim clsSetCommon As New Class_common
        Dim dtNow As DateTime = clsSetCommon.dtIndia
        Dim workDay As Integer

        workDay = Convert.ToInt32(dtNow.ToShortDateString.Substring(8, 2))

        '今月
        Dim workMonth As Integer
        workMonth = Convert.ToInt32(dtNow.ToShortDateString.Substring(5, 2))

        '指定された月が今月
        If month + 1 = workMonth Then
            '本日まで
            endday = workDay
        Else
            '月末まで
            Select Case month
                Case 0, 2, 4, 6, 7, 9, 11
                    endday = 31
                Case 3, 5, 8, 10
                    endday = 30
                Case 1
                    endday = 29
                Case Else
                    endday = 31
            End Select
        End If

        Dim indexSelected As Integer = DropDownDay2.SelectedIndex
        DropDownDay2.Items.Clear()

        For i As Integer = 1 To endday
            If i.ToString.Length = 1 Then
                DropDownDay2.Items.Add("0" & i.ToString())
            Else
                DropDownDay2.Items.Add(i.ToString())
            End If
        Next

        If indexSelected <= endday - 1 Then
            DropDownDay2.SelectedIndex = indexSelected
        End If

    End Sub

    Protected Sub btnToday_Click(sender As Object, e As EventArgs) Handles btnToday.Click

        Dim clsSetCommon As New Class_common
        Dim dtNow As DateTime = clsSetCommon.dtIndia

        DropDownMonth.Items.Clear()
        DropDownMonth.Items.Add(dtNow.ToShortDateString.Substring(5, 2))

        DropDownDay.Items.Clear()
        DropDownDay.Items.Add(dtNow.ToShortDateString.Substring(8, 2))

        DropDownYear.Items.Clear()
        DropDownYear.Items.Add(dtNow.Year)

        'open用
        Dim setMon As String = DropDownMonth.Text
        Dim setDay As String = DropDownDay.Text
        Dim setYear As String = DropDownYear.Text

        Session("set_Month_today") = setMon
        Session("set_Day") = setDay
        Session("set_Year") = setYear

    End Sub

    Private Sub ChkFullDiscount_CheckedChanged(sender As Object, e As EventArgs) Handles ChkFullDiscount.CheckedChanged

        If ChkFullDiscount.Checked = True Then
            TextClaim.Text = ""
            TextClaim.Enabled = False
            TextClaimCredit.Text = ""
            TextClaimCredit.Enabled = False
            TextDeposi.Text = ""
            TextDeposi.Enabled = False
            TextDiscount.Text = ""
            TextDiscount.Enabled = False
            TextPONo.Text = ""
            TextPONo.Enabled = False
            DropDownCard.Text = "Select Card Type"
            DropDownCard.Enabled = False
            lblChange.Text = ""
            lblDisClaim.Text = ""
        Else
            TextClaim.Enabled = True
            TextDeposi.Enabled = True
            TextDiscount.Enabled = True
            If ChkCredit.Checked = True Then
                TextClaimCredit.Enabled = True
                TextPONo.Enabled = True
                DropDownCard.Enabled = True
            End If
        End If

    End Sub

    Private Sub TextDiscount_TextChanged(sender As Object, e As EventArgs) Handles TextDiscount.TextChanged

        Dim cntDiscountInput As Integer
        Session("cntDiscount_Input") = Nothing

        Dim tmpDiscount As Decimal

        If TextDiscount.Text <> "" Then
            tmpDiscount = Convert.ToDecimal(TextDiscount.Text)
        Else
            tmpDiscount = 0.00
        End If

        Dim tmpAmount As Decimal

        If TextTotalAmount.Text <> "" Then
            tmpAmount = Convert.ToDecimal(TextTotalAmount.Text)
        Else
            tmpAmount = 0.00
        End If

        cntDiscountInput = cntDiscountInput + 1
        Session("cntDiscount_Input") = cntDiscountInput

        If tmpAmount <> 0.00 Then

            If cntDiscountInput = 1 Then

                'textClaimとlblDisClaimの設定
                lblDisClaim.Text = tmpAmount - tmpDiscount

                If ChkCash.Checked = True And ChkCredit.Checked = False Then

                    TextClaim.Text = tmpAmount - tmpDiscount

                    Dim workDelimit() As String
                    workDelimit = Split(TextClaim.Text, ".")

                    If workDelimit.Length <> 1 Then
                        lblDisClaim.Text = workDelimit(0)
                        TextClaim.Text = workDelimit(0)
                    End If

                End If

                If ChkCash.Checked = False And ChkCredit.Checked = True Then
                    TextClaimCredit.Text = tmpAmount - tmpDiscount
                End If

            End If

        End If

    End Sub

    Private Sub ChkCash_CheckedChanged(sender As Object, e As EventArgs) Handles ChkCash.CheckedChanged

        If ChkCash.Checked = True Then

            If ChkFullDiscount.Checked = False Then
                TextClaim.Enabled = True
                TextDeposi.Enabled = True
                lblChange.Enabled = True
            End If

            'credit FALSE
            If ChkCredit.Checked = False Then
                TextClaimCredit.Text = ""
                TextClaimCredit.Enabled = False

                TextPONo.Text = ""
                TextPONo.Enabled = False

                DropDownCard.Text = "Select Card Type"
                DropDownCard.Enabled = False
            End If

        Else
            TextClaim.Text = ""
            TextClaim.Enabled = False

            TextDeposi.Text = ""
            TextDeposi.Enabled = False

            lblChange.Text = ""
            lblChange.Enabled = False
        End If

    End Sub

    Private Sub ChkCredit_CheckedChanged(sender As Object, e As EventArgs) Handles ChkCredit.CheckedChanged

        If ChkCredit.Checked = True Then
            TextClaimCredit.Enabled = True
            TextPONo.Enabled = True
            DropDownCard.Enabled = True

            'cash FALSE
            If ChkCash.Checked = False Then
                TextClaim.Text = ""
                TextClaim.Enabled = False

                TextDeposi.Text = ""
                TextDeposi.Enabled = False

                lblChange.Text = ""
                lblChange.Enabled = False
            End If

        Else
            TextClaimCredit.Text = ""
            TextClaimCredit.Enabled = False

            TextPONo.Text = ""
            TextPONo.Enabled = False

            DropDownCard.Text = "Select Card Type"
            DropDownCard.Enabled = False
        End If

    End Sub

End Class
