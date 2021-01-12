Imports System.IO
Imports System.Text
Imports System.Net.Mail
Imports System.Data.SqlClient
Imports System.Globalization
Public Class Money_Reserve1
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        Dim clsSet As New Class_money
        Dim errMsg As String = ""

        '***セッションなしログインユーザの対応***
        Dim userid As String = Session("user_id")
        If userid = "" Then
            Response.Redirect("Login.aspx")
        End If

        'セッション取得
        Dim shipCode As String = Session("ship_code")

        If shipCode Is Nothing Then
            Call showMsg("The session has expired. Please login again.")
            Exit Sub
        End If

        '初期表示
        If IsPostBack = False Then

            '***テキスト等制御***
            TextM2000Sum.Enabled = False
            TextM500Sum.Enabled = False
            TextM200Sum.Enabled = False
            TextM100Sum.Enabled = False
            TextM50Sum.Enabled = False
            TextM20Sum.Enabled = False
            TextM10Sum.Enabled = False
            TextCoin10Sum.Enabled = False
            TextCoin5Sum.Enabled = False
            TextCoin2Sum.Enabled = False
            TextCoin1Sum.Enabled = False
            TextTotal.Enabled = False
            TextDiff.Enabled = False
            lblLastMsg.Visible = False

            '***処理ステータスをリストにセット***
            With DropListStatus
                .Items.Clear()
                .Items.Add("select processing")
                .Items.Add("open")
                .Items.Add("inspection_1st")
                .Items.Add("inspection_2nd")
                .Items.Add("inspection_3rd")
                .Items.Add("close")
            End With

            lblmistake0.Visible = False
            lblmistake1.Visible = False
            lblmistake2.Visible = False
            lblmistake3.Visible = False
            lblmistake4.Visible = False
            lblUpdateShow.Visible = False
            lblIPShow.Visible = False
            lblUpdateShow2.Visible = False
            lblIPShow2.Visible = False

            btnSend2.Enabled = False
            TextID.Enabled = False
            TextPASS.Enabled = False

            Call showData()

            Call showdata2()

        Else
            '***処理ステータスをセット***
            Dim syoriStatus As String = DropListStatus.Text
            Session("syori_Status") = syoriStatus
        End If

    End Sub

    Protected Sub btnSend_Click(sender As Object, e As ImageClickEventArgs) Handles btnSend.Click

        '***各ステータスの終了確認情報取得***
        Call showData()

        '***セッション取得***
        Dim userid As String = Session("user_id")
        Dim username As String = Session("user_Name")
        Dim shipCode As String = Session("ship_code")
        Dim userLevel As String = Session("user_level")
        Dim adminFlg As Boolean = Session("admin_Flg")
        Dim syoriStatus As String = Session("syori_Status")

        If userid Is Nothing Then
            Call showMsg("The session has expired. Please login again.")
            Exit Sub
        End If

        '確認者のチェック完了済の処理完了フラグ
        Dim openSyoriFlg As Boolean
        openSyoriFlg = Session("open_SyoriFlg")

        Dim ins1SyoriFlg As Boolean
        ins1SyoriFlg = Session("ins1_SyoriFlg")

        Dim ins2SyoriFlg As Boolean
        ins2SyoriFlg = Session("ins2_SyoriFlg")

        Dim ins3SyoriFlg As Boolean
        ins3SyoriFlg = Session("ins3_SyoriFlg")

        Dim closeSyoriFlg As Boolean
        closeSyoriFlg = Session("close_SyoriFlg")

        '確認者のチェックなしの処理完了フラグ
        Dim ins1SyoriFlg2 As Boolean
        ins1SyoriFlg2 = Session("ins1_SyoriFlg2")

        Dim ins2SyoriFlg2 As Boolean
        ins2SyoriFlg2 = Session("ins2_SyoriFlg2")

        Dim ins3SyoriFlg2 As Boolean
        ins3SyoriFlg2 = Session("ins3_SyoriFlg2")

        Dim closeSyoriFlg2 As Boolean
        closeSyoriFlg2 = Session("close_SyoriFlg2")

        Dim clsSetCommon As New Class_common
        Dim clsSet As New Class_money
        Dim errMsg As String = ""
        Dim regiDeposi As String = ""
        Dim diff As String = ""
        Dim firstTime As String
        Dim mistakeCount As Integer
        Dim syoriStartTime As String
        Dim syoriEndTime As String

        Dim ci As New System.Globalization.CultureInfo("en-US")
        Dim dtNow As DateTime = clsSetCommon.dtIndia

        '***OPEN/CLOSE処理実施可能確認***
        Dim opentime As String = ""
        Dim closetime As String = ""
        If clsSet.chkOpen(shipCode, opentime, closetime, errMsg) = False Then
            If errMsg <> "" Then
                Call showMsg(errMsg)
                Exit Sub
            End If
        End If

        If syoriStatus = "open" Then

            '***OPEN処理***
            'レジ点検が完了しているか確認
            If ins1SyoriFlg2 = True Or ins2SyoriFlg2 = True Or ins3SyoriFlg2 = True Then
                Call showMsg("After register check, opening process can not be done. Cancel processing.")
                Exit Sub
            End If

            If ins1SyoriFlg = True Or ins2SyoriFlg = True Or ins3SyoriFlg = True Then
                Call showMsg("After register check, opening process can not be done. Cancel processing.")
                Exit Sub
            End If

            'open処理可能時間を取得
            Call clsSet.syoriOkTime(shipCode, "open", syoriStartTime, syoriEndTime, errMsg)
            If errMsg <> "" Then
                Call showMsg(errMsg)
                Exit Sub
            End If

            If dtNow.ToString("HH:mm") >= syoriStartTime And dtNow.ToString("HH:mm") <= syoriEndTime Then

                If errMsg <> "" Then
                    lblDispStatu.Text = ""
                    Call showMsg(errMsg)
                    Exit Sub
                End If

                '集計
                Call syukei("open", shipCode, userid, username, userLevel, adminFlg, errMsg, diff, regiDeposi, firstTime, mistakeCount)

                If errMsg <> "" Then
                    Call showMsg(errMsg)
                    Exit Sub
                End If

                'ログインユーザ名セット
                lblName.Text = username

                '時間のセット
                lblTime.Text = dtNow.ToString("tt", ci) & dtNow.ToShortTimeString

                'mistakecount
                If mistakeCount >= 1 Then
                    lblmistake0.Visible = True
                    lblmistakeOpen.Text = mistakeCount
                End If

                '表示色設定
                Call setResult("open", diff)

                '準備金
                lblRegiDeposi.Text = regiDeposi & "INR"

                '確認者チェック
                lblCIns1.Text = "Not Confirmation"
                lblCIns1.ForeColor = System.Drawing.Color.FromArgb(255, 0, 0)
                lblCIns1_.Text = ""

            Else
                Call showMsg("Processing will be canceled because the specified time has passed.")
                Exit Sub
            End If

        ElseIf (syoriStatus = "inspection_1st") Or (syoriStatus = "inspection_2nd") Or (syoriStatus = "inspection_3rd") Then

            '***営業中レジ点検処理***
            '開店処理が終わっているかチェック
            If openSyoriFlg = False Then
                Call showMsg("Since opening processing has not been completed, processing is canceled.")
                Exit Sub
            End If

            '閉店処理が終わっているかチェック
            If closeSyoriFlg = True Then
                Call showMsg("Since closing processing has been completed, processing is canceled.")
                Exit Sub
            End If

            'CLOSE処理が作業中（確認者のチェックが未）かチェック
            If closeSyoriFlg2 = True Then
                Call showMsg("Because CLOSE processing is in progress, it can not be processed.")
                Exit Sub
            End If

            '営業中のレジ点検 １回目
            If syoriStatus = "inspection_1st" Then

                '２回目以降のレジ点検が完了しているかチェック
                If ins2SyoriFlg = True Or ins3SyoriFlg = True Then
                    Call showMsg("We can not process it because the checkout of the register after the second time has been completed.")
                    Exit Sub
                End If

                '２回目以降のレジ点検が作業中（確認者のチェックが未）かチェック
                If ins2SyoriFlg2 = True Or ins3SyoriFlg2 = True Then
                    Call showMsg("It is not possible to process because the second and subsequent checkouts are in process.")
                    Exit Sub
                End If

                'レジ点検 １回目の処理可能時間を取得
                Call clsSet.syoriOkTime(shipCode, "inspection1", syoriStartTime, syoriEndTime, errMsg)

                If errMsg <> "" Then
                    Call showMsg(errMsg)
                    Exit Sub
                End If

                If dtNow.ToString("HH:mm") >= syoriStartTime And dtNow.ToString("HH:mm") <= syoriEndTime Then

                    '集計
                    diff = ""
                    firstTime = ""
                    mistakeCount = 0
                    Call syukei("inspection1", shipCode, userid, username, userLevel, adminFlg, errMsg, diff, "", firstTime, mistakeCount)

                    If errMsg <> "" Then
                        Call showMsg(errMsg)
                        Exit Sub
                    End If

                    '結果表示
                    Call setResult("inspection1", diff)

                    lblDiff0.Text = diff & "INR"

                    'ログインユーザ名セット
                    lblName0.Text = username

                    '時間のセット
                    lblTime0.Text = dtNow.ToString("tt", ci) & dtNow.ToShortTimeString

                    'mistakecount
                    If mistakeCount >= 1 Then
                        lblmistake1.Visible = True
                        lblmistakeIns1.Text = mistakeCount
                    End If

                    '確認者チェック
                    lblCIns1.Text = "Not Confirmation"
                    lblCIns1.ForeColor = System.Drawing.Color.FromArgb(255, 0, 0)
                    lblCIns1_.Text = ""

                Else
                    Call showMsg("Now is not the time the first checkout of the day to be done.")
                    Exit Sub
                End If

            ElseIf syoriStatus = "inspection_2nd" Then

                '３回目のレジ点検が完了しているかチェック
                If ins3SyoriFlg = True Then
                    Call showMsg("Processing can not be done because the third checkout has been completed.")
                    Exit Sub
                End If

                '３回目のレジ点検が作業中（確認者のチェックが未）かチェック
                If ins3SyoriFlg2 = True Then
                    Call showMsg("We can not process because the third checkout check is in process.")
                    Exit Sub
                End If

                '1回目のレジ点検が終わっているかチェック
                If ins1SyoriFlg = False Then
                    Call showMsg("Since the first checkout of the register has not been completed, processing will be canceled.")
                    Exit Sub
                End If

                'レジ点検 ２回目の処理可能時間を取得
                Call clsSet.syoriOkTime(shipCode, "inspection2", syoriStartTime, syoriEndTime, errMsg)

                If errMsg <> "" Then
                    Call showMsg(errMsg)
                    Exit Sub
                End If

                '営業中のレジ点検 ２回目
                If dtNow.ToString("HH:mm") >= syoriStartTime And dtNow.ToString("HH:mm") <= syoriEndTime Then

                    '集計
                    diff = ""
                    firstTime = ""
                    mistakeCount = 0
                    Call syukei("inspection2", shipCode, userid, username, userLevel, adminFlg, errMsg, diff, "", firstTime, mistakeCount)
                    If errMsg <> "" Then
                        Call showMsg(errMsg)
                        Exit Sub
                    End If

                    '結果表示
                    Call setResult("inspection2", diff)

                    lblDiff1.Text = diff & "INR"

                    'ログインユーザ名セット
                    lblName1.Text = username

                    '時間のセット
                    lblTime1.Text = dtNow.ToString("tt", ci) & dtNow.ToShortTimeString

                    'mistakecount
                    If mistakeCount >= 1 Then
                        lblmistake2.Visible = True
                        lblmistakeIns2.Text = mistakeCount
                    End If

                    '確認者チェック
                    lblCIns2.Text = "Not Confirmation"
                    lblCIns2.ForeColor = System.Drawing.Color.FromArgb(255, 0, 0)
                    lblCIns2_.Text = ""

                Else
                    Call showMsg("Now is not the time the second checkout of the day to be done.")
                    Exit Sub
                End If

            ElseIf syoriStatus = "inspection_3rd" Then

                '2回目のレジ点検が終わっているかチェック
                If ins2SyoriFlg = False Then
                    Call showMsg("Because the second check of the register has not been completed, we will stop processing.")
                    Exit Sub
                End If

                'レジ点検 ３回目の処理可能時間を取得
                Call clsSet.syoriOkTime(shipCode, "inspection3", syoriStartTime, syoriEndTime, errMsg)

                If errMsg <> "" Then
                    Call showMsg(errMsg)
                    Exit Sub
                End If

                '営業中のレジ点検 ３回目
                If dtNow.ToString("HH:mm") >= syoriStartTime And dtNow.ToString("HH:mm") <= syoriEndTime Then

                    '集計
                    diff = ""
                    firstTime = ""
                    mistakeCount = 0
                    Call syukei("inspection3", shipCode, userid, username, userLevel, adminFlg, errMsg, diff, "", firstTime, mistakeCount)

                    If errMsg <> "" Then
                        Call showMsg(errMsg)
                        Exit Sub
                    End If

                    '結果表示
                    Call setResult("inspection3", diff)

                    lblDiff2.Text = diff & "INR"

                    'ログインユーザ名セット
                    lblName2.Text = username

                    '時間のセット
                    lblTime2.Text = dtNow.ToString("tt", ci) & dtNow.ToShortTimeString

                    'mistakecount
                    If mistakeCount >= 1 Then
                        lblmistake3.Visible = True
                        lblmistakeIns3.Text = mistakeCount
                    End If

                    '確認者チェック
                    lblCIns3.Text = "Not Confirmation"
                    lblCIns3.ForeColor = System.Drawing.Color.FromArgb(255, 0, 0)
                    lblCIns3_.Text = ""

                Else
                    Call showMsg("Now is not the time the third checkout of the day to be done")
                    Exit Sub
                End If

            End If

        ElseIf syoriStatus = "close" Then

            '***CLOSE処理***
            '**処理可能チェック**
            '開店処理終わっているか確認
            If openSyoriFlg = False Then
                Call showMsg("Since opening processing has not been completed, processing is canceled.")
                Exit Sub
            End If

            'レジ点検は、1回以上完了しているか確認
            If ins1SyoriFlg = False And ins2SyoriFlg = False And ins3SyoriFlg = False Then
                Call showMsg("There is no Credit Report!!")
                Exit Sub
            End If

            '点検中のままでないか確認（確認者のチェックなし）
            If ins1SyoriFlg2 = True And ins1SyoriFlg = False Then
                Call showMsg("The first check is ongoing.")
                Exit Sub
            End If

            If ins2SyoriFlg2 = True And ins2SyoriFlg = False Then
                Call showMsg("The second check is ongoing.")
                Exit Sub
            End If

            If ins3SyoriFlg2 = True And ins3SyoriFlg = False Then
                Call showMsg("The third check is ongoing.")
                Exit Sub
            End If

            '締め処理が完了しているか確認 ※reportが終了しているとCLOSE処理不可
            Dim closeChk As String
            If (userLevel = "0" Or userLevel = "1" Or userLevel = "2" Or adminFlg = True) Then
            Else
                Dim errFlg As Integer

                Dim strSQL As String = "SELECT * FROM dbo.M_ship_base WHERE DELFG = 0 AND ship_code = '" & shipCode & "';"

                Dim DT_M_ship_base As DataTable = DBCommon.ExecuteGetDT(strSQL, errFlg)

                If errFlg = 1 Then
                    Call showMsg("Failed to acquire information from M_ship_base (confirmation of completion of confirmation report).")
                    Exit Sub
                End If

                If DT_M_ship_base IsNot Nothing Then

                    If DT_M_ship_base.Rows(0)("open_time") IsNot DBNull.Value Then
                        closeChk = DT_M_ship_base.Rows(0)("open_time")
                    End If

                End If

                If closeChk = "False" Then
                    Call showMsg("The confirmation report has already completed, so CLOSE processing can not be performed. ")
                    Exit Sub
                End If

            End If

            '**集計**
            diff = ""
            firstTime = ""
            mistakeCount = 0

            Call syukei("close", shipCode, userid, username, userLevel, adminFlg, errMsg, diff, "", firstTime, mistakeCount)

            If errMsg <> "" Then
                Call showMsg(errMsg)
                Exit Sub
            End If

            '**結果表示**
            Call setResult("close", diff)

            lblDiff3.Text = diff & "INR"

            'ログインユーザ名セット
            lblName3.Text = username

            '時間のセット
            lblTime3.Text = dtNow.ToString("tt", ci) & dtNow.ToShortTimeString

            'mistakecount
            If mistakeCount >= 1 Then
                lblmistake4.Visible = True
                lblmistakeClose.Text = mistakeCount
            End If

            '確認者チェック
            lblConfirmOut.Text = "Not Confirmation"
            lblConfirmOut.ForeColor = System.Drawing.Color.FromArgb(255, 0, 0)
            lblConfirmOut2.Text = ""

            lblLastMsg.Visible = False

        ElseIf syoriStatus = "select processing" Then
                Call showMsg("Please select command.")
            Exit Sub
        End If

        '***コントロール制御***
        '登録後リスト選択不可
        DropListStatus.Enabled = False

        '確認者の登録OK
        btnSend2.Enabled = True
        TextID.Enabled = True
        TextPASS.Enabled = True

    End Sub

    Protected Sub showMsg(ByVal Msg As String)

        lblMsg.Text = Msg
        Dim sScript As String = "$(function () { $( ""#dialog"" ).dialog({width: 400, buttons: {""OK"": function () {$(this).dialog('close');}}});});"
        'JavaScriptの埋め込み
        ClientScript.RegisterStartupScript(Me.GetType(), "startup", sScript, True)

    End Sub

    Protected Sub fixedText()

        TextM2000Sum.Enabled = False
        TextM2000Cnt.Enabled = False
        TextM500Sum.Enabled = False
        TextM500Cnt.Enabled = False
        TextM200Sum.Enabled = False
        TextM200Cnt.Enabled = False
        TextM100Sum.Enabled = False
        TextM100Cnt.Enabled = False
        TextM50Sum.Enabled = False
        TextM50Cnt.Enabled = False
        TextM20Sum.Enabled = False
        TextM20Cnt.Enabled = False
        TextM10Sum.Enabled = False
        TextM10Cnt.Enabled = False
        TextCoin10Sum.Enabled = False
        TextCoin10Cnt.Enabled = False
        TextCoin5Sum.Enabled = False
        TextCoin5Cnt.Enabled = False
        TextCoin2Sum.Enabled = False
        TextCoin2Cnt.Enabled = False
        TextCoin1Sum.Enabled = False
        TextCoin1Cnt.Enabled = False
        TextTotal.Enabled = False
        TextDiff.Enabled = False

    End Sub
    '****************************************************
    '開店・閉店・営業中のレジ点検処理
    '引数 ：status        ①open ②inspection ③close
    '       shipCode　　  ログイン拠点コード
    '       userid　　　  ログインユーザID
    '       username      ログインユーザ名
    '       userLevel     ユーザレベル
    '       adminFlg      管理者権限
    '       errMsg        戻り値   エラー内容　　  
    '       diff          戻り値   ①open:準備金－入力金額（レジ内チェック金額）
    '                              ②inspection③close:計上売上金額（cash amount）－　入力金額（レジ内チェック金額）－準備金
    '       regiDeposit   戻り値   準備金
    '       firstTime     戻り値   最初に入力（sendボタン押下）したときのtime
    '       mistakeCount  戻り値   sendボタン押下回数
    '****************************************************
    Protected Sub syukei(ByVal status As String, ByVal shipCode As String, ByVal userid As String, ByVal username As String, ByVal userLevel As String, ByVal adminFlg As Boolean, ByRef errMsg As String, ByRef diff As String, ByRef regiDeposit As String, ByRef firstTime As String, ByRef mistakeCount As Integer)

        '***共通処理***
        Dim errFlg As Integer
        Dim clsSet As New Class_money
        Dim clsSetCommon As New Class_common
        Dim numChk As Integer
        Dim dtNow As DateTime = clsSetCommon.dtIndia

        Dim i2000Cnt As Integer
        Dim i500Cnt As Integer
        Dim i200Cnt As Integer
        Dim i100Cnt As Integer
        Dim i50Cnt As Integer
        Dim i20Cnt As Integer
        Dim i10Cnt As Integer
        Dim i10CoinCnt As Integer
        Dim i5CoinCnt As Integer
        Dim i2CoinCnt As Integer
        Dim i1CoinCnt As Integer

        '数値チェックと設定
        If TextM2000Cnt.Text = "" Then
            i2000Cnt = 0
        Else
            If Integer.TryParse(TextM2000Cnt.Text, numChk) = False Then
                errMsg = "Please enter a numerical value."
                TextM2000Cnt.Focus()
                Exit Sub
            Else
                If TextM2000Cnt.Text.Length > 4 Then
                    errMsg = "Up to 4 digits can be entered."
                    TextM2000Cnt.Focus()
                    Exit Sub
                Else
                    i2000Cnt = Convert.ToInt32(TextM2000Cnt.Text)
                End If
            End If

            If Left(TextM2000Cnt.Text, 1) = "-" Then
                errMsg = "Please do not input minus."
                TextM2000Cnt.Focus()
                Exit Sub
            End If
        End If

        If TextM500Cnt.Text = "" Then
            i500Cnt = 0
        Else
            If Integer.TryParse(TextM500Cnt.Text, numChk) = False Then
                errMsg = "Please enter a numerical value."
                TextM500Cnt.Focus()
                Exit Sub
            Else
                If TextM500Cnt.Text.Length > 4 Then
                    errMsg = "Up to 4 digits can be entered."
                    TextM500Cnt.Focus()
                    Exit Sub
                Else
                    i500Cnt = Convert.ToInt32(TextM500Cnt.Text)
                End If
            End If

            If Left(TextM500Cnt.Text, 1) = "-" Then
                errMsg = "Please do not input minus."
                TextM500Cnt.Focus()
                Exit Sub
            End If
        End If

        If TextM200Cnt.Text = "" Then
            i200Cnt = 0
        Else
            If Integer.TryParse(TextM200Cnt.Text, numChk) = False Then
                errMsg = "Please enter a numerical value."
                TextM200Cnt.Focus()
                Exit Sub
            Else
                If TextM200Cnt.Text.Length > 4 Then
                    errMsg = "Up to 4 digits can be entered."
                    TextM200Cnt.Focus()
                    Exit Sub
                Else
                    i200Cnt = Convert.ToInt32(TextM200Cnt.Text)
                End If
            End If

            If Left(TextM200Cnt.Text, 1) = "-" Then
                errMsg = "Please do not input minus."
                TextM200Cnt.Focus()
                Exit Sub
            End If
        End If

        If TextM100Cnt.Text = "" Then
            i100Cnt = 0
        Else
            If Integer.TryParse(TextM100Cnt.Text, numChk) = False Then
                errMsg = "Please enter a numerical value."
                TextM100Cnt.Focus()
                Exit Sub
            Else
                If TextM100Cnt.Text.Length > 4 Then
                    errMsg = "Up to 4 digits can be entered."
                    TextM100Cnt.Focus()
                    Exit Sub
                Else
                    i100Cnt = Convert.ToInt32(TextM100Cnt.Text)
                End If
            End If

            If Left(TextM100Cnt.Text, 1) = "-" Then
                errMsg = "Please do not input minus."
                TextM100Cnt.Focus()
                Exit Sub
            End If
        End If

        If TextM50Cnt.Text = "" Then
            i50Cnt = 0
        Else
            If Integer.TryParse(TextM50Cnt.Text, numChk) = False Then
                errMsg = "Please enter a numerical value."
                TextM50Cnt.Focus()
                Exit Sub
            Else
                If TextM50Cnt.Text.Length > 4 Then
                    errMsg = "Up to 4 digits can be entered."
                    TextM50Cnt.Focus()
                    Exit Sub
                Else
                    i50Cnt = Convert.ToInt32(TextM50Cnt.Text)
                End If
            End If

            If Left(TextM50Cnt.Text, 1) = "-" Then
                errMsg = "Please do not input minus."
                TextM50Cnt.Focus()
                Exit Sub
            End If
        End If

        If TextM20Cnt.Text = "" Then
            i20Cnt = 0
        Else
            If Integer.TryParse(TextM20Cnt.Text, numChk) = False Then
                errMsg = "Please enter a numerical value."
                TextM20Cnt.Focus()
                Exit Sub
            Else
                If TextM20Cnt.Text.Length > 4 Then
                    errMsg = "Up to 4 digits can be entered."
                    TextM20Cnt.Focus()
                    Exit Sub
                Else
                    i20Cnt = Convert.ToInt32(TextM20Cnt.Text)
                End If
            End If

            If Left(TextM20Cnt.Text, 1) = "-" Then
                errMsg = "Please do not input minus."
                TextM20Cnt.Focus()
                Exit Sub
            End If
        End If

        If TextM10Cnt.Text = "" Then
            i10Cnt = 0
        Else
            If Integer.TryParse(TextM10Cnt.Text, numChk) = False Then
                errMsg = "Please enter a numerical value."
                TextM10Cnt.Focus()
                Exit Sub
            Else
                If TextM10Cnt.Text.Length > 4 Then
                    errMsg = "Up to 4 digits can be entered."
                    TextM10Cnt.Focus()
                    Exit Sub
                Else
                    i10Cnt = Convert.ToInt32(TextM10Cnt.Text)
                End If
            End If

            If Left(TextM10Cnt.Text, 1) = "-" Then
                errMsg = "Please do not input minus."
                TextM10Cnt.Focus()
                Exit Sub
            End If
        End If

        If TextCoin10Cnt.Text = "" Then
            i10CoinCnt = 0
        Else
            If Integer.TryParse(TextCoin10Cnt.Text, numChk) = False Then
                errMsg = "Please enter a numerical value."
                TextCoin10Cnt.Focus()
                Exit Sub
            Else
                If TextCoin10Cnt.Text.Length > 4 Then
                    errMsg = "Up to 4 digits can be entered."
                    TextCoin10Cnt.Focus()
                    Exit Sub
                Else
                    i10CoinCnt = Convert.ToInt32(TextCoin10Cnt.Text)
                End If
            End If

            If Left(TextCoin10Cnt.Text, 1) = "-" Then
                errMsg = "Please do not input minus."
                TextCoin10Cnt.Focus()
                Exit Sub
            End If
        End If

        If TextCoin5Cnt.Text = "" Then
            i5CoinCnt = 0
        Else
            If Integer.TryParse(TextCoin5Cnt.Text, numChk) = False Then
                errMsg = "Please enter a numerical value."
                TextCoin5Cnt.Focus()
                Exit Sub
            Else
                If TextCoin5Cnt.Text.Length > 4 Then
                    errMsg = "Up to 4 digits can be entered."
                    TextCoin5Cnt.Focus()
                    Exit Sub
                Else
                    i5CoinCnt = Convert.ToInt32(TextCoin5Cnt.Text)
                End If
            End If

            If Left(TextCoin5Cnt.Text, 1) = "-" Then
                errMsg = "Please do not input minus."
                TextCoin5Cnt.Focus()
                Exit Sub
            End If
        End If

        If TextCoin2Cnt.Text = "" Then
            i2CoinCnt = 0
        Else
            If Integer.TryParse(TextCoin2Cnt.Text, numChk) = False Then
                errMsg = "Please enter a numerical value."
                TextCoin2Cnt.Focus()
                Exit Sub
            Else
                i2CoinCnt = Convert.ToInt32(TextCoin2Cnt.Text)
            End If

            If Left(TextCoin2Cnt.Text, 1) = "-" Then
                errMsg = "Please do not input minus."
                TextCoin2Cnt.Focus()
                Exit Sub
            End If
        End If

        If TextCoin1Cnt.Text = "" Then
            i1CoinCnt = 0
        Else
            If Integer.TryParse(TextCoin1Cnt.Text, numChk) = False Then
                errMsg = "Please enter a numerical value."
                TextCoin1Cnt.Focus()
                Exit Sub
            Else
                If TextCoin1Cnt.Text.Length > 4 Then
                    errMsg = "Up to 4 digits can be entered."
                    TextCoin1Cnt.Focus()
                    Exit Sub
                Else
                    i1CoinCnt = Convert.ToInt32(TextCoin1Cnt.Text)
                End If
            End If

            If Left(TextCoin1Cnt.Text, 1) = "-" Then
                errMsg = "Please do not input minus."
                TextCoin1Cnt.Focus()
                Exit Sub
            End If
        End If

        Dim l2000Sum As Long
        Dim l500Sum As Long
        Dim l200Sum As Long
        Dim l100Sum As Long
        Dim l50Sum As Long
        Dim l20Sum As Long
        Dim l10Sum As Long
        Dim l10CoinSum As Long
        Dim l5CoinSum As Long
        Dim l2CoinSum As Long
        Dim l1CoinSum As Long
        Dim allSum As Long

        l2000Sum = 2000 * i2000Cnt
        l500Sum = 500 * i500Cnt
        l200Sum = 200 * i200Cnt
        l100Sum = 100 * i100Cnt
        l50Sum = 50 * i50Cnt
        l20Sum = 20 * i20Cnt
        l10Sum = 10 * i10Cnt
        l10CoinSum = 10 * i10CoinCnt
        l5CoinSum = 5 * i5CoinCnt
        l2CoinSum = 2 * i2CoinCnt
        l1CoinSum = 1 * i1CoinCnt

        '入力値合計
        allSum &= l2000Sum + l500Sum + l200Sum + l100Sum + l50Sum + l20Sum + l10Sum + l10CoinSum + l5CoinSum + l2CoinSum + l1CoinSum

        If allSum = 0 Then
            errMsg = "Please enter the counted number. Cancel processing."
            Exit Sub
        End If

        '基準額取得
        Dim strSQL = "SELECT * FROM dbo.M_ship_base WHERE DELFG = 0 AND ship_code = '" & shipCode & "';"

        Dim DT_M_ship_base As DataTable = DBCommon.ExecuteGetDT(strSQL, errFlg)

        If errFlg = 1 Then
            errMsg = "Failed to acquire base amount from M_ship_base."
            Exit Sub
        End If

        If DT_M_ship_base IsNot Nothing Then
            If DT_M_ship_base.Rows(0)("regi_deposit") IsNot DBNull.Value Then
                regiDeposit = DT_M_ship_base.Rows(0)("regi_deposit")
            End If
        End If

        If regiDeposit = "" Then
            errMsg = "Failed to acquire reserved amount. Cancel processing."
            Exit Sub
        End If

        'sendボタン押下回数取得
        Dim strSQL5 As String = "SELECT COUNT(*) AS sendCount FROM dbo.T_Reserve WHERE DELFG = 0 AND youser_name = '" & username & "' "
        strSQL5 &= "AND ship_code = '" & shipCode & "' "
        strSQL5 &= "AND status = '" & status & "' "
        strSQL5 &= "AND LEFT(CONVERT(VARCHAR, datetime, 111), 10) = '" & dtNow.ToShortDateString & "';"

        Dim DT_T_Reserve As DataTable = DBCommon.ExecuteGetDT(strSQL5, errFlg)

        If errFlg = 1 Then
            errMsg = "Failed to acquire the registration count (send button press count) from T_Reserve."
            Exit Sub
        End If

        If DT_T_Reserve IsNot Nothing Then
            If DT_T_Reserve.Rows(0)("sendCount") IsNot DBNull.Value Then
                mistakeCount = DT_T_Reserve.Rows(0)("sendCount")
            End If
        End If

        'mistakeCountが１回以上であれば、初回登録datetimeを取得
        If mistakeCount >= 1 Then

            Dim datetimeWork As DateTime
            Dim strSQL6 As String = "SELECT datetime FROM dbo.T_Reserve WHERE DELFG = 0 AND youser_name = '" & username & "' "
            strSQL6 &= "AND ship_code = '" & shipCode & "' "
            strSQL6 &= "AND status = '" & status & "' "
            strSQL6 &= "AND LEFT(CONVERT(VARCHAR, datetime, 111), 10) = '" & dtNow.ToShortDateString & "' "
            strSQL6 &= "ORDER BY datetime;"

            DT_T_Reserve = DBCommon.ExecuteGetDT(strSQL6, errFlg)

            If errFlg = 1 Then
                errMsg = "Failed to acquire datetime for initial registration from T_Reserve."
                Exit Sub
            End If

            If DT_T_Reserve IsNot Nothing Then
                If DT_T_Reserve.Rows(0)("datetime") IsNot DBNull.Value Then
                    datetimeWork = DT_T_Reserve.Rows(0)("datetime")
                End If
            End If

            If userLevel = "0" Or userLevel = "1" Or userLevel = "2" Or adminFlg = True Then
            Else
                If DateDiff("n", datetimeWork, dtNow) > 30 Then
                    errMsg = "Failed to register because it exceeded 30 minutes from the first registration, it can not be registered."
                    Call fixedText()
                    btnSend.Enabled = False
                    Exit Sub
                End If
            End If

        End If

        '***status毎処理***
        If status = "open" Then

            '***OPEN処理***
            diff = clsSet.setINR((Convert.ToInt64(regiDeposit) - allSum).ToString)

        ElseIf (status = "close") Or (status = "inspection1") Or (status = "inspection2") Or (status = "inspection3") Then

            '***CLOSE処理/INSPECTION処理***
            Dim strSQL3 = "SELECT SUM(claim) AS amount FROM dbo.cash_track WHERE DELFG = 0 AND payment = 'Cash' AND location = '" & shipCode & "' AND LEFT(CONVERT(VARCHAR, invoice_date, 111), 10) = '" & dtNow.ToShortDateString & "' AND FALSE = '0';"

            '**cash_trackからの売り上げ金額を取得**
            Dim amount As Decimal

            Dim DT_cash_track As DataTable = DBCommon.ExecuteGetDT(strSQL3, errFlg)

            If errFlg = 1 Then
                errMsg = "Failed to acquire the total value of amount failed."
                Exit Sub
            End If

            If DT_cash_track IsNot Nothing Then
                If DT_cash_track.Rows(0)("amount") IsNot DBNull.Value Then
                    amount = DT_cash_track.Rows(0)("amount")
                End If
            End If

            If Convert.ToDecimal(allSum) < Convert.ToDecimal(regiDeposit) Then
                errMsg = "It became more negative than reserve. Please check again."
                Exit Sub
            End If

            '**OPEN処理の準備金を取得**
            '※ship_baseからの準備金と違う（必ずしも一致しているとは限らないため。）
            '***本日の開店処理終了確認***
            Dim reserveData As Class_money.T_Reserve
            Dim SyoriFlg As Boolean = False

            'OPEN処理終了確認
            clsSet.chkSyoriOpenClose("open", shipCode, SyoriFlg, reserveData, errMsg)

            If errMsg <> "" Then
                Exit Sub
            End If

            '**diff設定**
            'diff = clsSet.setINR((amount - (Convert.ToDecimal(allSum) - Convert.ToDecimal(regiDeposit))).ToString)
            diff = clsSet.setINR((amount - (Convert.ToDecimal(allSum) - reserveData.total)).ToString)

        End If



        '***共通結果表示***
        TextM2000Sum.Text = l2000Sum.ToString
        TextM500Sum.Text = l500Sum.ToString
        TextM200Sum.Text = l200Sum.ToString
        TextM100Sum.Text = l100Sum.ToString
        TextM50Sum.Text = l50Sum.ToString
        TextM20Sum.Text = l20Sum.ToString
        TextM10Sum.Text = l10Sum.ToString
        TextCoin10Sum.Text = l10CoinSum.ToString
        TextCoin5Sum.Text = l5CoinSum.ToString
        TextCoin2Sum.Text = l2CoinSum.ToString
        TextCoin1Sum.Text = l1CoinSum.ToString
        TextTotal.Text = allSum.ToString
        TextDiff.Visible = True
        TextDiff.Text = diff

        '***登録処理***
        'コネクションを取得
        Dim con As SqlConnection = New SqlConnection(ConfigurationManager.ConnectionStrings("cnstr").ConnectionString)
        con.Open()

        'トランザクション開始＆コネクションオープン
        Dim trn As SqlTransaction = con.BeginTransaction(IsolationLevel.ReadCommitted)

        Try
            Dim ipAdress As String = System.Web.HttpContext.Current.Request.UserHostAddress

            '■T_Reserve空テーブル取得
            Dim select_sql1 As String = ""
            select_sql1 &= "SELECT * FROM dbo.T_Reserve WHERE youser_name IS NULL;"
            Dim sqlSelect1 As New SqlCommand(select_sql1, con, trn)
            Dim Adapter1 As New SqlDataAdapter(sqlSelect1)
            Dim Builder1 As New SqlCommandBuilder(Adapter1)
            Dim ds1 As New DataSet

            Adapter1.Fill(ds1)

            '金種処理データ登録
            '新規DR取得
            Dim dr1 As DataRow = ds1.Tables(0).NewRow

            '登録内容設定
            dr1("CRTDT") = dtNow
            dr1("CRTCD") = userid
            'dr1("UPDDT") = dtNow
            'dr1("UPDCD") = userid
            dr1("UPDPG") = "Money_Reserve.aspx"
            dr1("DELFG") = 0
            dr1("status") = status
            dr1("youser_name") = username
            dr1("datetime") = dtNow
            dr1("M_2000") = l2000Sum.ToString
            dr1("M_500") = l500Sum.ToString
            dr1("M_200") = l200Sum.ToString
            dr1("M_100") = l100Sum.ToString
            dr1("M_50") = l50Sum.ToString
            dr1("M_20") = l20Sum.ToString
            dr1("M_10") = l10Sum.ToString
            dr1("Coin_10") = l10CoinSum.ToString
            dr1("Coin_5") = l5CoinSum.ToString
            dr1("Coin_2") = l2CoinSum.ToString
            dr1("Coin_1") = l1CoinSum.ToString
            dr1("total") = allSum.ToString
            dr1("diff") = diff
            dr1("reserve") = regiDeposit
            dr1("ship_code") = shipCode

            If Convert.ToDecimal(diff) = 0.00 Then
                dr1("mistake") = 1
            Else
                dr1("mistake") = 0
            End If

            dr1("ip_address") = ipAdress

            ds1.Tables(0).Rows.Add(dr1)

            '更新
            Adapter1.Update(ds1)

            trn.Commit()

            '登録完了後、IPアドレスと登録時間を表示
            lblUpdateShow.Visible = True
            lblIPShow.Visible = True
            lblIP.Text = ipAdress
            lblLastupdate.Text = dtNow.ToString

        Catch ex As Exception
            trn.Rollback()
            errMsg = "The denomination registration process failed."
        Finally
            'DB接続クローズ
            If con.State <> ConnectionState.Closed Then
                con.Close()
            End If
        End Try

    End Sub

    Protected Sub setResult(ByVal status As String, ByVal diff As String)

        If status = "open" Then

            If diff = "0" Or diff = "0.00" Then
                lblName.ForeColor = System.Drawing.Color.FromArgb(0, 0, 0)
                lblTime.ForeColor = System.Drawing.Color.FromArgb(0, 0, 0)
                lblRegiDeposi.ForeColor = System.Drawing.Color.FromArgb(0, 0, 0)
                lblResult.ForeColor = System.Drawing.Color.FromArgb(0, 0, 0)
                lblmistake0.ForeColor = System.Drawing.Color.FromArgb(0, 0, 0)
                lblmistakeOpen.ForeColor = System.Drawing.Color.FromArgb(0, 0, 0)
                lblResult.Text = "OK"
            Else
                '赤色
                lblName.ForeColor = System.Drawing.Color.FromArgb(255, 0, 0)
                lblTime.ForeColor = System.Drawing.Color.FromArgb(255, 0, 0)
                lblRegiDeposi.ForeColor = System.Drawing.Color.FromArgb(255, 0, 0)
                lblResult.ForeColor = System.Drawing.Color.FromArgb(255, 0, 0)
                lblmistake0.ForeColor = System.Drawing.Color.FromArgb(255, 0, 0)
                lblmistakeOpen.ForeColor = System.Drawing.Color.FromArgb(255, 0, 0)
                lblResult.Text = "mistake"
            End If

        ElseIf status = "inspection1" Then

            If diff = "0" Or diff = "0.00" Then
                lblDiff0.ForeColor = System.Drawing.Color.FromArgb(0, 0, 0)
                lblName0.ForeColor = System.Drawing.Color.FromArgb(0, 0, 0)
                lblTime0.ForeColor = System.Drawing.Color.FromArgb(0, 0, 0)
                lblResultIns1.ForeColor = System.Drawing.Color.FromArgb(0, 0, 0)
                lblResultIns1.Text = "OK"
                lblmistake1.ForeColor = System.Drawing.Color.FromArgb(0, 0, 0)
                lblmistakeIns1.ForeColor = System.Drawing.Color.FromArgb(0, 0, 0)
            Else
                '赤色
                lblDiff0.ForeColor = System.Drawing.Color.FromArgb(255, 0, 0)
                lblName0.ForeColor = System.Drawing.Color.FromArgb(255, 0, 0)
                lblTime0.ForeColor = System.Drawing.Color.FromArgb(255, 0, 0)
                lblResultIns1.ForeColor = System.Drawing.Color.FromArgb(255, 0, 0)
                lblResultIns1.Text = "mistake"
                lblmistake1.ForeColor = System.Drawing.Color.FromArgb(255, 0, 0)
                lblmistakeIns1.ForeColor = System.Drawing.Color.FromArgb(255, 0, 0)
            End If

        ElseIf status = "inspection2" Then

            If diff = "0" Or diff = "0.00" Then
                lblDiff1.ForeColor = System.Drawing.Color.FromArgb(0, 0, 0)
                lblName1.ForeColor = System.Drawing.Color.FromArgb(0, 0, 0)
                lblTime1.ForeColor = System.Drawing.Color.FromArgb(0, 0, 0)
                lblResultIns2.ForeColor = System.Drawing.Color.FromArgb(0, 0, 0)
                lblResultIns2.Text = "OK"
                lblmistake2.ForeColor = System.Drawing.Color.FromArgb(0, 0, 0)
                lblmistakeIns2.ForeColor = System.Drawing.Color.FromArgb(0, 0, 0)
            Else
                '赤色
                lblDiff1.ForeColor = System.Drawing.Color.FromArgb(255, 0, 0)
                lblName1.ForeColor = System.Drawing.Color.FromArgb(255, 0, 0)
                lblTime1.ForeColor = System.Drawing.Color.FromArgb(255, 0, 0)
                lblResultIns2.ForeColor = System.Drawing.Color.FromArgb(255, 0, 0)
                lblResultIns2.Text = "mistake"
                lblmistake2.ForeColor = System.Drawing.Color.FromArgb(255, 0, 0)
                lblmistakeIns2.ForeColor = System.Drawing.Color.FromArgb(255, 0, 0)
            End If

        ElseIf status = "inspection3" Then

            If diff = "0" Or diff = "0.00" Then
                lblDiff2.ForeColor = System.Drawing.Color.FromArgb(0, 0, 0)
                lblName2.ForeColor = System.Drawing.Color.FromArgb(0, 0, 0)
                lblTime2.ForeColor = System.Drawing.Color.FromArgb(0, 0, 0)
                lblResultIns3.ForeColor = System.Drawing.Color.FromArgb(0, 0, 0)
                lblResultIns3.Text = "OK"
                lblmistake3.ForeColor = System.Drawing.Color.FromArgb(0, 0, 0)
                lblmistakeIns3.ForeColor = System.Drawing.Color.FromArgb(0, 0, 0)
            Else
                '赤色
                lblDiff2.ForeColor = System.Drawing.Color.FromArgb(255, 0, 0)
                lblName2.ForeColor = System.Drawing.Color.FromArgb(255, 0, 0)
                lblTime2.ForeColor = System.Drawing.Color.FromArgb(255, 0, 0)
                lblResultIns3.ForeColor = System.Drawing.Color.FromArgb(255, 0, 0)
                lblResultIns3.Text = "mistake"
                lblmistake3.ForeColor = System.Drawing.Color.FromArgb(255, 0, 0)
                lblmistakeIns3.ForeColor = System.Drawing.Color.FromArgb(255, 0, 0)
            End If

        ElseIf status = "close" Then

            If diff = "0" Or diff = "0.00" Then
                lblDiff3.ForeColor = System.Drawing.Color.FromArgb(0, 0, 0)
                lblName3.ForeColor = System.Drawing.Color.FromArgb(0, 0, 0)
                lblTime3.ForeColor = System.Drawing.Color.FromArgb(0, 0, 0)
                lblResultClose.ForeColor = System.Drawing.Color.FromArgb(0, 0, 0)
                lblResultClose.Text = "OK"
                lblmistake4.ForeColor = System.Drawing.Color.FromArgb(0, 0, 0)
                lblmistakeClose.ForeColor = System.Drawing.Color.FromArgb(0, 0, 0)
            Else
                '赤色
                lblDiff3.ForeColor = System.Drawing.Color.FromArgb(255, 0, 0)
                lblName3.ForeColor = System.Drawing.Color.FromArgb(255, 0, 0)
                lblTime3.ForeColor = System.Drawing.Color.FromArgb(255, 0, 0)
                lblResultClose.ForeColor = System.Drawing.Color.FromArgb(255, 0, 0)
                lblResultClose.Text = "mistake"
                lblmistake4.ForeColor = System.Drawing.Color.FromArgb(255, 0, 0)
                lblmistakeClose.ForeColor = System.Drawing.Color.FromArgb(255, 0, 0)
            End If

        End If

    End Sub
    '入力チェック回数のカウント処理
    Protected Sub chkMistakeCnt(ByVal status As String, ByVal shipCode As String, ByVal userid As String, ByRef mistakeCount As Integer, ByRef errMsg As String)

        Dim clsSetCommon As New Class_common
        Dim dtNow As DateTime = clsSetCommon.dtIndia

        'ログインユーザ名取得
        Dim username As String
        clsSetCommon.setUserName(userid, username, errMsg)

        If errMsg <> "" Then
            errMsg = errMsg & "（msitake count時処理）"
            Exit Sub
        End If

        'sendボタン押下回数取得
        Dim errFlg As Integer
        Dim strSQL As String = "SELECT COUNT(*) AS sendCount FROM dbo.T_Reserve WHERE DELFG = 0 AND youser_name = '" & username & "' "
        strSQL &= "AND ship_code = '" & shipCode & "' "
        strSQL &= "AND status = '" & status & "' "
        strSQL &= "AND LEFT(CONVERT(VARCHAR, datetime, 111), 10) = '" & dtNow.ToShortDateString & "';"

        Dim DT_T_Reserve As DataTable = DBCommon.ExecuteGetDT(strSQL, errFlg)

        If errFlg = 1 Then
            errMsg = "Failed to acquire the registration count (send button press count) from T_Reserve."
            Exit Sub
        End If

        If DT_T_Reserve IsNot Nothing Then
            If DT_T_Reserve.Rows(0)("sendCount") IsNot DBNull.Value Then
                mistakeCount = DT_T_Reserve.Rows(0)("sendCount")
            End If
        End If

    End Sub

    '確認者の確認完了の登録
    Protected Sub btnSend2_Click(sender As Object, e As ImageClickEventArgs) Handles btnSend2.Click

        '***セッション取得***
        Dim userid As String = Session("user_id")
        Dim username As String = Session("user_Name")
        Dim shipCode As String = Session("ship_code")
        Dim shipName As String = Session("ship_Name")

        '***入力チェック***
        Dim userIDConfirm As String

        If TextID.Text = "" Then
            Call showMsg("Please enter your ID.")
            Exit Sub
        Else
            userIDConfirm = Trim(TextID.Text)
        End If

        Dim userPASSConfirm As String
        If TextPASS.Text = "" Then
            Call showMsg("Please enter the PASSWORD.")
            Exit Sub
        Else
            userPASSConfirm = Trim(TextPASS.Text)
        End If

        Dim hostGrobalIP As String
        If lblIP.Text = "" Then
            Call showMsg("It seems that the first Open check has not finishied.")
            Exit Sub
        Else
            hostGrobalIP = Trim(lblIP.Text)
        End If

        '***確認者のID,PASS確認***
        Dim errFlg As Integer
        Dim sqlStr As String

        sqlStr = "SELECT * FROM dbo.M_USER WHERE user_id = '" & userIDConfirm & "' AND password = '" & userPASSConfirm & "' AND DELFG = 0;"

        Dim dsM_USER As New DataSet
        dsM_USER = DBCommon.Get_DS(sqlStr, errFlg)

        If errFlg = 1 Then
            Call showMsg("Failed to acquire Confirmation user from M_USER.")
            Exit Sub
        End If

        'ユーザ情報なし
        If dsM_USER Is Nothing Then
            TextID.Text = ""
            TextPASS.Text = ""
            TextID.Focus()
            Call showMsg("An unregistered Confirmation user has been entered.")
            Exit Sub
        Else

            Dim dr1 As DataRow = dsM_USER.Tables(0).Rows(0)

            Dim ship() As String
            Dim userLevelConfirm As String
            Dim adminFlgConfirm As Boolean

            If dr1("ship_1") IsNot DBNull.Value Then
                ship = Split(dr1("ship_1"), ",")
            End If

            If dr1("user_level") IsNot DBNull.Value Then
                userLevelConfirm = dr1("user_level")
            End If

            If dr1("admin_flg") IsNot DBNull.Value Then
                adminFlgConfirm = dr1("admin_flg")
            End If

            '権限がなければ、拠点のチェック
            If userLevelConfirm = "0" Or userLevelConfirm = "1" Or userLevelConfirm = "2" Or adminFlgConfirm = True Then
            Else
                Dim hitFlg As Integer
                If ship IsNot Nothing Then
                    For i = 0 To ship.Length - 1
                        If shipCode = ship(i) Then
                            hitFlg = 1
                            Exit For
                        End If
                    Next i
                    If hitFlg = 0 Then
                        Call showMsg("Since the login user and the site are different, confirmation registration is not possible.")
                        Exit Sub
                    End If
                End If
            End If

        End If

        'ログインユーザと確認者の同一チェック
        If userid = userIDConfirm Then
            Call showMsg("Since login user and confirmer ID are the same, processing is canceled.")
            Exit Sub
        End If

        ''***ステータスチェック***
        Dim syoriStatus As String = DropListStatus.Text

        '***確認情報の登録***
        Dim clsSet As New Class_money

        If syoriStatus = "inspection_1st" Then
            syoriStatus = "inspection1"
        ElseIf syoriStatus = "inspection_2nd" Then
            syoriStatus = "inspection2"
        ElseIf syoriStatus = "inspection_3rd" Then
            syoriStatus = "inspection3"
        End If

        Dim confirmTime As DateTime
        clsSet.setConfirmData(syoriStatus, userIDConfirm, hostGrobalIP, userid, shipCode, confirmTime, errFlg)

        Dim clsSetCommon As New Class_common
        Dim dtNow As DateTime = clsSetCommon.dtIndia

        If errFlg = 1 Then
            Call showMsg("Failed to register Confirmation.")
            Exit Sub
        Else
            Call showMsg("Registration of Confirmation is completed.")
        End If

        '***Open処理開始登録***
        If syoriStatus = "open" Then
            clsSet.chkSyoriOpenClose2("open", userid, shipCode, errFlg)
            If errFlg = 1 Then
                Call showMsg("Failed to open processing start registration to ship_base failed.")
                Exit Sub
            End If

            'opening storeの表示
            Call showdata2()
        End If

        '***確認者確認完了の表示***
        '確認者の名称取得
        Dim userNameConfirm As String = ""
        Dim errMsg As String
        clsSetCommon.setUserName(userIDConfirm, userNameConfirm, errMsg)

        If errMsg <> "" Then
            Call showMsg(errMsg)
            Exit Sub
        End If

        '表示
        Dim ci As New System.Globalization.CultureInfo("en-US")

        If syoriStatus = "open" Then

            lblConfirm.Text = ""
            lblConfirm2.Text = "Confirmation " & userNameConfirm & " " & confirmTime.ToString("tt", ci) & confirmTime.ToShortTimeString
            lblConfirm2.ForeColor = System.Drawing.Color.FromArgb(51, 204, 51)

        ElseIf syoriStatus = "inspection1" Then

            lblCIns1.Text = ""
            lblCIns1_.Text = "Confirmation " & userIDConfirm & " " & confirmTime.ToString("tt", ci) & confirmTime.ToShortTimeString
            lblCIns1_.ForeColor = System.Drawing.Color.FromArgb(51, 204, 51)

        ElseIf syoriStatus = "inspection2" Then

            lblCIns2.Text = ""
            lblCIns2_.Text = "Confirmation " & userIDConfirm & " " & confirmTime.ToString("tt", ci) & confirmTime.ToShortTimeString
            lblCIns2_.ForeColor = System.Drawing.Color.FromArgb(51, 204, 51)

        ElseIf syoriStatus = "inspection3" Then

            lblCIns3.Text = ""
            lblCIns3_.Text = "Confirmation " & userIDConfirm & " " & confirmTime.ToString("tt", ci) & confirmTime.ToShortTimeString
            lblCIns3_.ForeColor = System.Drawing.Color.FromArgb(51, 204, 51)

        ElseIf syoriStatus = "close" Then

            lblConfirmOut.Text = ""
            lblConfirmOut2.Text = "Confirmation " & userNameConfirm & " " & confirmTime.ToString("tt", ci) & confirmTime.ToShortTimeString
            lblConfirmOut2.ForeColor = System.Drawing.Color.FromArgb(51, 204, 51)

            '完了メッセージ
            If Trim(lblDiff3.Text) = "0INR" Or Trim(lblDiff3.Text) = "0.00INR" Then
                lblLastMsg.Visible = True
            Else
                lblLastMsg.Visible = False
            End If

        End If

        lblUpdateShow2.Visible = True
        lblLastupdate2.Text = confirmTime.ToString
        lblIP2.Visible = True
        lblIP2.Text = hostGrobalIP

        '***メール送信処理 ***
        'メールアドレスの取得
        Dim userMail As String
        clsSetCommon.setMail(userid, userMail, errMsg)

        If errMsg <> "" Then
            Call showMsg(errMsg)
            Exit Sub
        End If

        Dim smtp As New SmtpClient()
        Dim msg As New MailMessage()

        Try
            '送信元
            msg.From = New System.Net.Mail.MailAddress(userMail)

            '送信先
            msg.To.Add(New System.Net.Mail.MailAddress(clsSetCommon.toMailAddPdf))
            msg.To.Add(New System.Net.Mail.MailAddress(clsSetCommon.toMailAddPdf2))
            msg.To.Add(New System.Net.Mail.MailAddress(clsSetCommon.toMailAddPdf3))

            ' 件名
            msg.Subject = "**[" & syoriStatus & " SSC]** " & dtNow.ToShortDateString & "_" & shipName

            ' 本文
            msg.Body &= "-----" & syoriStatus & " SSC Report -----" & vbCrLf
            msg.Body &= dtNow.ToString & vbCrLf
            msg.Body &= "money count user : " & username & vbCrLf
            msg.Body &= "Confirmation user : " & userNameConfirm & vbCrLf
            msg.Body &= "Lastupdate : " & lblLastupdate.Text & vbCrLf
            msg.Body &= "Confirm Lastupdate : " & lblLastupdate2.Text & vbCrLf

            If syoriStatus = "open" Then

                msg.Body &= "Open Deposit : " & lblResult.Text & "  " & lblName.Text & " " & lblTime.Text & " " & lblRegiDeposi.Text & " " & lblConfirm2.Text

            ElseIf syoriStatus = "inspection1" Then

                msg.Body &= syoriStatus & " : " & lblResultIns1.Text & "  " & lblName0.Text & " " & lblTime0.Text & " " & lblDiff0.Text & " " & lblCIns1_.Text

            ElseIf syoriStatus = "inspection2" Then

                msg.Body &= syoriStatus & " : " & lblResultIns2.Text & "  " & lblName1.Text & " " & lblTime1.Text & " " & lblDiff1.Text & " " & lblCIns2_.Text

            ElseIf syoriStatus = "inspection3" Then

                msg.Body &= syoriStatus & " : " & lblResultIns3.Text & "  " & lblName2.Text & " " & lblTime2.Text & " " & lblDiff2.Text & " " & lblCIns3_.Text

            ElseIf syoriStatus = "close" Then

                msg.Body &= syoriStatus & " : " & lblResultClose.Text & "  " & lblName3.Text & " " & lblTime3.Text & " " & lblDiff3.Text & " " & lblConfirmOut2.Text

            End If

            'SMTPサーバーなどの設定
            smtp.Host = clsSetCommon.SMTPSERVER
            smtp.DeliveryMethod = System.Net.Mail.SmtpDeliveryMethod.Network

            'メッセージを送信
            smtp.Send(msg)

            smtp.Dispose()
            msg.Dispose()

            Call showMsg("The " & syoriStatus & " SSC Report has been completed.")
        Catch ex As Exception
            Call showMsg(ex.Message)
        End Try

        btnSend2.Enabled = False
        btnSend.Enabled = False

    End Sub
    '最新のレジ点検チェック状況を表示
    Protected Sub showData()

        Dim clsSet As New Class_money
        Dim errMsg As String = ""

        '***セッション情報取得***
        Dim userid As String = Session("user_id")
        Dim shipCode As String = Session("ship_code")

        '***本日の開店処理終了確認***
        Dim reserveData As Class_money.T_Reserve
        Dim SyoriFlg As Boolean = False   '確認者のチェックあり
        Dim mistakeCount As Integer
        Dim ci As New System.Globalization.CultureInfo("en-US")

        'OPEN処理終了確認
        clsSet.chkSyoriOpenClose("open", shipCode, SyoriFlg, reserveData, errMsg)

        If errMsg <> "" Then
            Call showMsg(errMsg)
            Exit Sub
        End If

        'sendボタン押下回数取得
        Call chkMistakeCnt("open", shipCode, userid, mistakeCount, errMsg)

        If errMsg <> "" Then
            Call showMsg(errMsg)
            Exit Sub
        End If


        'OPEN処理の結果表示
        'If SyoriFlg = True Then
        If reserveData.youser_name <> "" Then
            lblName.Text = reserveData.youser_name
            lblTime.Text = reserveData.datetime.ToString("tt", ci) & reserveData.datetime.ToShortTimeString
            lblRegiDeposi.Text = reserveData.reserve & "INR"

            '※mistakeのカウントは、正誤に関わらず、2回以上登録している場合に表示（例　登録1回目はmistake0:表示なし　登録2回目はmistake1:表示あり ）
            If mistakeCount >= 2 Then
                lblmistake0.Visible = True
                lblmistakeOpen.Text = mistakeCount - 1
            End If

            Call setResult("open", reserveData.diff)

            '確認者チェック
            If reserveData.conf_user = "" Then
                lblConfirm.Text = "Not Confirmation"
                lblConfirm.ForeColor = System.Drawing.Color.FromArgb(255, 0, 0)
            Else
                '確認者の名称取得
                Dim confUserName As String = ""
                Dim clsSetCommon As New Class_common
                clsSetCommon.setUserName(reserveData.conf_user, confUserName, errMsg)

                If errMsg <> "" Then
                    Call showMsg(errMsg)
                    Exit Sub
                End If

                lblConfirm2.Text = "Confirmation " & confUserName & " " & reserveData.conf_datetime.ToString("tt", ci) & reserveData.conf_datetime.ToShortTimeString
                lblConfirm2.ForeColor = System.Drawing.Color.FromArgb(51, 204, 51)
            End If

            '金種情報
            Call setDenomination(reserveData)

        Else
            lblResult.Text = "Untreated"
        End If

        Session("open_SyoriFlg") = SyoriFlg

        '***本日のINSPECTION１回目処理終了確認***
        Dim SyoriFlg2 As Boolean = False '確認者のチェックなし
        SyoriFlg = False
        reserveData = Nothing
        mistakeCount = 0

        '１回目レジ点検処理終了確認
        clsSet.chkSyoriOpenClose("inspection1", shipCode, SyoriFlg, reserveData, errMsg)

        If errMsg <> "" Then
            Call showMsg(errMsg)
            Exit Sub
        End If

        'sendボタン押下回数取得
        Call chkMistakeCnt("inspection1", shipCode, userid, mistakeCount, errMsg)

        If errMsg <> "" Then
            Call showMsg(errMsg)
            Exit Sub
        End If

        '１回目inspectionの結果表示
        If reserveData.youser_name <> "" Then
            'If SyoriFlg = True Then

            '結果表示
            Call setResult("inspection1", reserveData.diff)

            lblDiff0.Text = reserveData.diff & "INR"

            lblName0.Text = reserveData.youser_name

            '時間のセット
            lblTime0.Text = reserveData.datetime.ToString("tt", ci) & reserveData.datetime.ToShortTimeString

            'ログイン者のsendボタン押下数（初回登録は除く）
            If mistakeCount >= 2 Then
                lblmistake1.Visible = True
                lblmistakeIns1.Text = mistakeCount - 1
            End If

            '確認者チェック
            If reserveData.conf_user = "" Then
                lblCIns1.Text = "Not Confirmation"
                lblCIns1.ForeColor = System.Drawing.Color.FromArgb(255, 0, 0)
            Else
                ''確認者の名称取得
                'Dim confUserName As String = ""
                'Dim clsSetCommon As New Class_common
                'clsSetCommon.setUserName(reserveData.conf_user, confUserName, errMsg)

                'If errMsg <> "" Then
                '    Call showMsg(errMsg)
                '    Exit Sub
                'End If
                '名称が長いと改行してしまうの、ユーザIDを設定。もし他に良い案があれば。。
                lblCIns1_.Text = "Confirmation " & reserveData.conf_user & " " & reserveData.conf_datetime.ToString("tt", ci) & reserveData.conf_datetime.ToShortTimeString
                lblCIns1_.ForeColor = System.Drawing.Color.FromArgb(51, 204, 51)
            End If

            '金種情報
            Call setDenomination(reserveData)

            SyoriFlg2 = True
            Session("ins1_SyoriFlg2") = SyoriFlg2

        Else
            lblResultIns1.Text = "Untreated"
        End If

        Session("ins1_SyoriFlg") = SyoriFlg

        '***本日のINSPECTION２回目処理終了確認***
        reserveData = Nothing
        SyoriFlg = False
        SyoriFlg2 = False
        mistakeCount = 0

        '２回目レジ点検処理終了確認
        clsSet.chkSyoriOpenClose("inspection2", shipCode, SyoriFlg, reserveData, errMsg)

        If errMsg <> "" Then
            Call showMsg(errMsg)
            Exit Sub
        End If

        'sendボタン押下回数取得
        Call chkMistakeCnt("inspection2", shipCode, userid, mistakeCount, errMsg)

        If errMsg <> "" Then
            Call showMsg(errMsg)
            Exit Sub
        End If

        '２回目inspectionの結果表示
        'If SyoriFlg = True Then
        If reserveData.youser_name <> "" Then

            '結果表示
            Call setResult("inspection2", reserveData.diff)

            lblDiff1.Text = reserveData.diff & "INR"

            lblName1.Text = reserveData.youser_name

            '時間のセット
            lblTime1.Text = reserveData.datetime.ToString("tt", ci) & reserveData.datetime.ToShortTimeString

            'ログイン者のsendボタン押下数（初回登録は除く）
            If mistakeCount >= 2 Then
                lblmistake2.Visible = True
                lblmistakeIns2.Text = mistakeCount - 1
            End If

            '確認者チェック
            If reserveData.conf_user = "" Then
                lblCIns2.Text = "Not Confirmation"
                lblCIns2.ForeColor = System.Drawing.Color.FromArgb(255, 0, 0)
            Else
                ''確認者の名称取得
                'Dim confUserName As String = ""
                'Dim clsSetCommon As New Class_common
                'clsSetCommon.setUserName(reserveData.conf_user, confUserName, errMsg)

                'If errMsg <> "" Then
                '    Call showMsg(errMsg)
                '    Exit Sub
                'End If
                '名称が長いと改行してしまうの、ユーザIDを設定。もし他に良い案があれば。。
                lblCIns2_.Text = "Confirmation " & reserveData.conf_user & " " & reserveData.conf_datetime.ToString("tt", ci) & reserveData.conf_datetime.ToShortTimeString
                lblCIns2_.ForeColor = System.Drawing.Color.FromArgb(51, 204, 51)
            End If

            '金種情報
            Call setDenomination(reserveData)

            SyoriFlg2 = True
            Session("ins2_SyoriFlg2") = SyoriFlg2

        Else
            lblResultIns2.Text = "Untreated"
        End If

        Session("ins2_SyoriFlg") = SyoriFlg

        '***本日のINSPECTION３回目処理終了確認***
        reserveData = Nothing
        SyoriFlg = False
        SyoriFlg2 = False
        mistakeCount = 0

        '３回目レジ点検処理終了確認
        clsSet.chkSyoriOpenClose("inspection3", shipCode, SyoriFlg, reserveData, errMsg)

        If errMsg <> "" Then
            Call showMsg(errMsg)
            Exit Sub
        End If

        'sendボタン押下回数取得
        Call chkMistakeCnt("inspection3", shipCode, userid, mistakeCount, errMsg)

        If errMsg <> "" Then
            Call showMsg(errMsg)
            Exit Sub
        End If

        '３回目inspectionの結果表示
        'If SyoriFlg = True Then
        If reserveData.youser_name <> "" Then

            '結果表示
            Call setResult("inspection3", reserveData.diff)

            lblDiff2.Text = reserveData.diff & "INR"

            lblName2.Text = reserveData.youser_name

            '時間のセット
            lblTime2.Text = reserveData.datetime.ToString("tt", ci) & reserveData.datetime.ToShortTimeString

            'ログイン者のsendボタン押下数（初回登録は除く）
            If mistakeCount >= 2 Then
                lblmistake3.Visible = True
                lblmistakeIns3.Text = mistakeCount - 1
            End If

            '確認者チェック
            If reserveData.conf_user = "" Then
                lblCIns3.Text = "Not Confirmation"
                lblCIns3.ForeColor = System.Drawing.Color.FromArgb(255, 0, 0)
            Else
                ''確認者の名称取得
                'Dim confUserName As String = ""
                'Dim clsSetCommon As New Class_common
                'clsSetCommon.setUserName(reserveData.conf_user, confUserName, errMsg)

                'If errMsg <> "" Then
                '    Call showMsg(errMsg)
                '    Exit Sub
                'End If
                '名称が長いと改行してしまうの、ユーザIDを設定。もし他に良い案があれば。。
                lblCIns3_.Text = "Confirmation " & reserveData.conf_user & " " & reserveData.conf_datetime.ToString("tt", ci) & reserveData.conf_datetime.ToShortTimeString
                lblCIns3_.ForeColor = System.Drawing.Color.FromArgb(51, 204, 51)
            End If

            '金種情報
            Call setDenomination(reserveData)

            SyoriFlg2 = True
            Session("ins3_SyoriFlg2") = SyoriFlg2

        Else
            lblResultIns3.Text = "Untreated"
        End If

        Session("ins3_SyoriFlg") = SyoriFlg

        '***本日の閉店処理終了確認***
        reserveData = Nothing
        SyoriFlg = False
        SyoriFlg2 = False
        mistakeCount = 0

        'CLOSE処理終了確認
        clsSet.chkSyoriOpenClose("close", shipCode, SyoriFlg, reserveData, errMsg)

        If errMsg <> "" Then
            Call showMsg(errMsg)
            Exit Sub
        End If

        'sendボタン押下回数取得
        Call chkMistakeCnt("close", shipCode, userid, mistakeCount, errMsg)

        If errMsg <> "" Then
            Call showMsg(errMsg)
            Exit Sub
        End If

        'If SyoriFlg = True Then
        If reserveData.youser_name <> "" Then

            '結果表示
            Call setResult("close", reserveData.diff)

            lblDiff3.Text = reserveData.diff & "INR"

            lblName3.Text = reserveData.youser_name

            '時間のセット
            lblTime3.Text = reserveData.datetime.ToString("tt", ci) & reserveData.datetime.ToShortTimeString

            'ログイン者のsendボタン押下数（初回登録は除く）
            If mistakeCount >= 2 Then
                lblmistake4.Visible = True
                lblmistakeClose.Text = mistakeCount - 1
            End If

            '確認者チェック
            If reserveData.conf_user = "" Then
                lblConfirmOut.Text = "Not Confirmation"
                lblConfirmOut.ForeColor = System.Drawing.Color.FromArgb(255, 0, 0)
            Else
                ''確認者の名称取得
                'Dim confUserName As String = ""
                'Dim clsSetCommon As New Class_common
                'clsSetCommon.setUserName(reserveData.conf_user, confUserName, errMsg)

                'If errMsg <> "" Then
                '    Call showMsg(errMsg)
                '    Exit Sub
                'End If

                lblConfirmOut2.Text = "Confirmation " & reserveData.conf_user & " " & reserveData.conf_datetime.ToString("tt", ci) & reserveData.conf_datetime.ToShortTimeString
                lblConfirmOut2.ForeColor = System.Drawing.Color.FromArgb(51, 204, 51)
            End If

            '金種情報
            Call setDenomination(reserveData)

            SyoriFlg2 = True
            Session("close_SyoriFlg2") = SyoriFlg2

        Else
            lblResultClose.Text = "Untreated"
        End If

        If SyoriFlg = True Then

            '完了メッセージ
            If reserveData.diff = "0" Or reserveData.diff = "0.00" Then
                lblLastMsg.Visible = True
            Else
                lblLastMsg.Visible = False
            End If

        End If

        Session("close_SyoriFlg") = SyoriFlg

    End Sub

    Protected Sub showdata2()

        Dim shipCode As String = Session("ship_code")

        '***OPEN or CLOSEの表示
        Dim clsSet As New Class_money
        Dim opentime As String = ""
        Dim closetime As String = ""
        Dim errMsg As String

        If clsSet.chkOpen(shipCode, opentime, closetime, errMsg) = False Then
            lblDispStatu.Text = "closed"
        Else
            lblDispStatu.Text = "opening store"
        End If

        If errMsg <> "" Then
            lblDispStatu.Text = ""
            Call showMsg(errMsg)
            Exit Sub
        End If

    End Sub

    Protected Sub setDenomination(ByVal reserveData As Class_money.T_Reserve)

        TextM2000Sum.Text = reserveData.M_2000
        TextM500Sum.Text = reserveData.M_500
        TextM200Sum.Text = reserveData.M_200
        TextM100Sum.Text = reserveData.M_100
        TextM50Sum.Text = reserveData.M_50
        TextM20Sum.Text = reserveData.M_20
        TextM10Sum.Text = reserveData.M_10
        TextCoin10Sum.Text = reserveData.Coin_10
        TextCoin5Sum.Text = reserveData.Coin_5
        TextCoin2Sum.Text = reserveData.Coin_2
        TextCoin1Sum.Text = reserveData.Coin_1
        TextTotal.Text = reserveData.total
        TextDiff.Text = reserveData.diff

    End Sub

End Class

