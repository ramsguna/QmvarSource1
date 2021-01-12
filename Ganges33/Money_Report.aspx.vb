Imports System.IO
Imports System.Text
Imports System.Net.Mail

Public Class Money_Report
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        If IsPostBack = False Then

            '***セッションなしログインユーザの対応***
            Dim userid As String = Session("user_id")
            If userid = "" Then
                Response.Redirect("Login.aspx")
            End If

            btnSend.Enabled = False

        End If

    End Sub

    Protected Sub btnStart_Click(sender As Object, e As ImageClickEventArgs) Handles btnStart.Click

        '***セッション情報取得***
        Dim userName As String = Session("user_Name")
        Dim shipCode As String = Session("ship_code")

        If Session("user_id") Is Nothing Then
            Call showMsg("The session is clear. Please login again.")
            Exit Sub
        End If

        '***時間とログイン名を表示****
        Dim clsCommon As New Class_common
        Dim dtNow As DateTime = clsCommon.dtIndia

        lblRecord.Text = dtNow.ToString("yyyyMMddHHmmss")
        lblYousername.Text = userName

        Session("Record_Date") = lblRecord.Text

        Call reSet()

        '***集計***
        Dim clsSet As New Class_money
        Dim setResult As Class_money.Aggregation
        Dim startDate As String = dtNow.ToShortDateString
        Dim errMsg As String = ""

        clsSet.setSyukei(setResult, shipCode, startDate, errMsg)

        If errMsg <> "" Then
            Call showMsg(errMsg)
            Exit Sub
        End If

        '■インポート済データの件数
        '件数
        lblCount.Text = setResult.Count

        '■金種毎合計金額

        '◇GSPN
        'IW（taxin）
        lblIWSum0.Text = clsSet.setINR2(setResult.IWTaxIn)
        'IW（notax）
        lblIWNoTax.Text = clsSet.setINR2(setResult.IWTaxIn / 1.18)

        '◇claim
        'IW（taxin）
        lblIWSum1.Text = clsSet.setINR2(setResult.IWSum)
        'IW（notax）
        lblIWNoTax0.Text = clsSet.setINR2(setResult.IWSum / 1.18)

        '◇GSPN
        'OOWのdenomi毎amount合計値をセット(tax込み)
        lblOOWCardSum0.Text = clsSet.setINR2(setResult.OOWCardTaxIn)
        lblOOWCashSum0.Text = clsSet.setINR2(setResult.OOWCashTaxIn)

        'otherのdenomi毎amount合計値をセット（tax込み)
        lblOtherCardSum0.Text = clsSet.setINR2(setResult.otherCardTaxIn)
        lblOtherCashSum0.Text = clsSet.setINR2(setResult.otherCashTaxIn)

        'OOWのdenomi毎amount合計値をセット(tax抜き)
        lblOWCardNoTax.Text = clsSet.setINR2(setResult.OOWCardTaxIn / 1.18)
        lblOWCashNoTax.Text = clsSet.setINR2(setResult.OOWCashTaxIn / 1.18)

        'otherのdenomi毎amount合計値をセット（tax抜き)
        lblOtCardNoTax.Text = clsSet.setINR2(setResult.otherCardTaxIn / 1.18)
        lblOtCashNoTax.Text = clsSet.setINR2(setResult.otherCashTaxIn / 1.18)

        '◇claim
        'OOWのdenomi毎amount合計値をセット(tax込み)
        lblOOWCardSum1.Text = clsSet.setINR2(setResult.OOWCardSum)
        lblOOWCashSum1.Text = clsSet.setINR2(setResult.OOWCashSum)

        'otherのdenomi毎amount合計値をセット（tax込み)
        lblOtherCardSum1.Text = clsSet.setINR2(setResult.otherCardSum)
        lblOtherCashSum1.Text = clsSet.setINR2(setResult.otherCashSum)

        'OOWのdenomi毎amount合計値をセット(tax抜き)
        lblOWCardNoTax0.Text = clsSet.setINR2(setResult.OOWCardSum / 1.18)
        lblOWCashNoTax0.Text = clsSet.setINR2(setResult.OOWCashSum / 1.18)

        'otherのdenomi毎amount合計値をセット（tax抜き)
        lblOtCardNoTax0.Text = clsSet.setINR2(setResult.otherCardSum / 1.18)
        lblOtCashNoTax0.Text = clsSet.setINR2(setResult.otherCashSum / 1.18)

        '■金種(IW,other(card),other(cash),out of warranty(card),out of warranty(cash))毎件数
        'warranty,denomi毎件数値をセット
        lblIWCnt.Text = setResult.IWCnt
        lblOtherCardCnt.Text = setResult.otherCardCnt + setResult.otherCashCreditCount
        lblOtherCashCnt.Text = setResult.otherCashCnt + setResult.otherCashCreditCount
        lblOOWCardCnt.Text = setResult.OOWCardCnt + setResult.OOWCashCreditCount
        lblOOWCashCnt.Text = setResult.OOWCashCnt + setResult.OOWCashCreditCount

        '■cash total(open dposit + sales(cash))
        '※準備金＋CLOSE処理のtotal金額
        lblCashTotal.Text = clsSet.setINR(setResult.cashTotal) & "INR"

        '■sales(sales(cash) + sales(card)+IW+other+GST)
        Dim tmp2 As Decimal

        If lblIWSum1.Text <> "" Then
            tmp2 = tmp2 + Convert.ToDecimal(lblIWSum1.Text)
        End If

        If lblOOWCardSum1.Text <> "" Then
            tmp2 = tmp2 + Convert.ToDecimal(lblOOWCardSum1.Text)
        End If

        If lblOOWCashSum1.Text <> "" Then
            tmp2 = tmp2 + Convert.ToDecimal(lblOOWCashSum1.Text)
        End If

        If lblOtherCardSum1.Text <> "" Then
            tmp2 = tmp2 + Convert.ToDecimal(lblOtherCardSum1.Text)
        End If

        If lblOtherCashSum1.Text <> "" Then
            tmp2 = tmp2 + Convert.ToDecimal(lblOtherCashSum1.Text)
        End If

        lblSales.Text = clsSet.setINR2(tmp2) & "INR"

        'Modified by India on 2018-12-28
        '■Bank Doposit
        'lblDeposit.Text = clsSet.setINR2(setResult.cashTotal - setResult.deposit) & "INR"

        '■Bank Doposit
        lblDeposit.Text = clsSet.setINR2(setResult.cashTotal - setResult.deposit) & "INR"



        ''■お客様の製品と金銭のお預かりの集計
        'lblDepositCusto.Text = clsSet.setINR2(setResult.customerTotalCash) & "INR"
        'lblNumCusto.Text = setResult.CustomerTotalNumber

        '■discount
        lblDisNum.Text = setResult.discountNum + setResult.fullDiscountNum
        lblDisAmount.Text = clsSet.setINR2(setResult.discountAmount + setResult.fullDiscountAmount) & "INR"

        '***セッション設定***
        Session("set_Result") = setResult

        '***コントロール***
        btnSend.Enabled = True

    End Sub

    Protected Sub btnSend_Click(sender As Object, e As ImageClickEventArgs) Handles btnSend.Click

        '***セッション情報取得***
        Dim userid As String = Session("user_id")
        Dim userName As String = Session("user_Name")
        Dim shipCode As String = Session("ship_code")
        Dim shipName As String = Session("ship_Name")
        Dim recordDate As String = Session("Record_Date")
        Dim clsSet As New Class_money
        Dim setResult As Class_money.Aggregation = Session("set_Result")

        If Session("user_id") Is Nothing Then
            Call showMsg("The session is clear. Please login again.")
            Exit Sub
        End If

        '***本日の開店／閉店処理終了確認***
        Dim SyoriFlg As Boolean = False
        Dim errMsg As String
        Dim reserveData As Class_money.T_Reserve

        'OPEN処理終了確認
        clsSet.chkSyoriOpenClose("open", shipCode, SyoriFlg, reserveData, errMsg)

        If errMsg <> "" Then
            Call showMsg(errMsg)
            Exit Sub
        End If

        Dim startStr As String
        If SyoriFlg = True Then
            startStr = reserveData.datetime.ToShortTimeString
        Else
            Call showMsg("Since the OpenClose processing has not been completed, it can not be completed.")
            Exit Sub
        End If

        SyoriFlg = False
        reserveData = Nothing

        'CLOSE処理終了確認  
        clsSet.chkSyoriOpenClose("close", shipCode, SyoriFlg, reserveData, errMsg)

        If errMsg <> "" Then
            Call showMsg(errMsg)
            Exit Sub
        End If

        Dim endStr As String
        If SyoriFlg = True Then
            endStr = reserveData.datetime.ToShortTimeString
        Else
            Call showMsg("Close processing has not been completed, so completion report can not be made.")
            Exit Sub
        End If

        '***締め登録***
        Dim errFlg As Integer
        clsSet.chkSyoriOpenClose2("close", userid, shipCode, errFlg)
        If errFlg = 1 Then
            Call showMsg("The registration to ship_base is failed.")
            Exit Sub
        End If

        '***メール送信処理 ***
        'メールアドレスの取得
        Dim clsSetCommon As New Class_common
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
            msg.Subject = "**[Sales Report]**" & recordDate & "_" & shipName

            ' 本文
            Dim clsCommon As New Class_common
            Dim dtNow As DateTime = clsCommon.dtIndia

            msg.Body &= "-----Sales Report -----" & vbCrLf
            msg.Body &= dtNow.ToString & vbCrLf
            msg.Body &= "Report User : " & userName & vbCrLf
            msg.Body &= "start time " & startStr & " ----- " & "end time" & endStr & vbCrLf
            msg.Body &= "Today's Result of " & shipName & vbCrLf
            msg.Body &= "IW" & vbCrLf
            msg.Body &= "number : " & setResult.IWCnt & vbCrLf
            msg.Body &= "amount(notax) : " & clsSet.setINR2(setResult.IWSum / 1.18) & "INR" & vbCrLf
            msg.Body &= "amount(taxin) : " & clsSet.setINR2(setResult.IWSum) & "INR" & vbCrLf
            msg.Body &= "OOW" & vbCrLf
            msg.Body &= "number : " & setResult.OOWCardCnt + setResult.OOWCashCnt + setResult.OOWCashCreditCount & vbCrLf
            msg.Body &= "amount(notax) : " & clsSet.setINR2((setResult.OOWCardSum / 1.18) + (setResult.OOWCashSum / 1.18)) & "INR" & vbCrLf
            msg.Body &= "amount(taxin) : " & clsSet.setINR2(setResult.OOWCardSum + setResult.OOWCashSum) & "INR" & vbCrLf
            msg.Body &= "OTHER" & vbCrLf
            msg.Body &= "number : " & setResult.otherCardCnt + setResult.otherCashCnt + setResult.otherCashCreditCount & vbCrLf
            msg.Body &= "amount(notax) : " & clsSet.setINR2((setResult.otherCardSum / 1.18) + (setResult.otherCashSum / 1.18)) & "INR" & vbCrLf
            msg.Body &= "amount(taxin) : " & clsSet.setINR2(setResult.otherCardSum + setResult.otherCashSum) & "INR" & vbCrLf
            msg.Body &= "Full Discount : " & setResult.fullDiscountNum & "/" & clsSet.setINR2(setResult.fullDiscountAmount) & "INR" & vbCrLf
            msg.Body &= "Discount : " & setResult.discountNum & "/" & clsSet.setINR2(setResult.discountAmount) & "INR" & vbCrLf
            msg.Body &= "cash total : " & clsSet.setINR(setResult.cashTotal) & "INR" & vbCrLf
            msg.Body &= "sales total : " & clsSet.setINR2(setResult.IWSum + setResult.OOWCardSum + setResult.OOWCashSum + setResult.otherCardSum + setResult.otherCashSum) & "INR" & vbCrLf

            'SMTPサーバーなどの設定
            smtp.Host = clsSetCommon.SMTPSERVER
            smtp.DeliveryMethod = System.Net.Mail.SmtpDeliveryMethod.Network

            'メッセージを送信
            smtp.Send(msg)

            smtp.Dispose()
            msg.Dispose()

            Call showMsg("Sales Report has completed.")

        Catch ex As Exception
            Call showMsg(ex.Message)
        Finally
            Session("set_Result") = Nothing
            Session("Record_Date") = Nothing
        End Try

    End Sub

    Protected Sub showMsg(ByVal Msg As String)

        lblMsg.Text = Msg
        Dim sScript As String = "$(function () { $( ""#dialog"" ).dialog({width: 400, buttons: {""OK"": function () {$(this).dialog('close');}}});});"
        'JavaScriptの埋め込み
        ClientScript.RegisterStartupScript(Me.GetType(), "startup", sScript, True)

    End Sub

    Protected Sub reSet()

        lblCount.Text = ""
        lblIWSum0.Text = ""
        lblIWNoTax.Text = ""
        lblIWSum1.Text = ""
        lblIWNoTax0.Text = ""
        lblOOWCardSum0.Text = ""
        lblOOWCashSum0.Text = ""
        lblOtherCardSum0.Text = ""
        lblOtherCashSum0.Text = ""
        lblOWCardNoTax.Text = ""
        lblOWCashNoTax.Text = ""
        lblOtCardNoTax.Text = ""
        lblOtCashNoTax.Text = ""
        lblOOWCardSum1.Text = ""
        lblOOWCashSum1.Text = ""
        lblOtherCardSum1.Text = ""
        lblOtherCashSum1.Text = ""
        lblOWCardNoTax0.Text = ""
        lblOWCashNoTax0.Text = ""
        lblOtCardNoTax0.Text = ""
        lblOtCashNoTax0.Text = ""
        lblIWCnt.Text = ""
        lblOtherCardCnt.Text = ""
        lblOtherCashCnt.Text = ""
        lblOOWCardCnt.Text = ""
        lblOOWCashCnt.Text = ""
        lblCashTotal.Text = ""
        lblSales.Text = ""
        lblDeposit.Text = ""

    End Sub

End Class