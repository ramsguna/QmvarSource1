Public Class Money_Syukei
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        If IsPostBack = False Then

            '***セッションなしログインユーザの対応***
            Dim userid As String = Session("user_id")
            If userid = "" Then
                Response.Redirect("Login.aspx")
            End If

        End If

    End Sub
    Protected Sub showMsg(ByVal Msg As String)

        lblMsg.Text = Msg
        Dim sScript As String = "$(function () { $( ""#dialog"" ).dialog({width: 400, buttons: {""OK"": function () {$(this).dialog('close');}}});});"
        'JavaScriptの埋め込み
        ClientScript.RegisterStartupScript(Me.GetType(), "startup", sScript, True)

    End Sub
    'sendボタン押下処理
    Protected Sub btnSend_Click(sender As Object, e As ImageClickEventArgs) Handles btnSend.Click

        '***セッション情報取得***
        Dim shipCode As String = Session("ship_code")

        If shipCode Is Nothing Then
            Call showMsg("The session has expired. Please login again.")
            Exit Sub
        End If

        '***日付チェック***
        Dim dt As DateTime
        Dim startDate As String
        If TextCompleteDateFrom.Text <> "" Then
            If DateTime.TryParse(TextCompleteDateFrom.Text, dt) Then
                startDate = DateTime.Parse(Trim(TextCompleteDateFrom.Text)).ToShortDateString
            Else
                Call showMsg("The complete date is invalid")
                Exit Sub
            End If
        Else
            startDate = ""
        End If

        If TextCompleteDateFrom.Text = "" Then
            Call showMsg("The complete date is invalid")
            Exit Sub
        End If

        Call reSet()

        '***集計***
        Dim clsSet As New Class_money
        Dim setResult As Class_money.Aggregation
        Dim errMsg As String = ""

        clsSet.setSyukei(setResult, shipCode, startDate, errMsg)

        If errMsg <> "" Then
            Call showMsg(errMsg)
            Exit Sub
        End If

        '***集計結果表示　小数点以下は２桁まで***
        '■インポート済データの件数
        lblCount.Text = setResult.Count

        '■金種毎合計金額（taxin）
        'IW
        'GSPN
        lblIWSum0.Text = clsSet.setINR(setResult.IWTaxIn)
        'claim
        lblIWSum1.Text = clsSet.setINR(setResult.IWSum)

        'OOWのdenomi毎amount合計値をセット(tax込み)
        'GSPN
        lblOOWCardSum0.Text = clsSet.setINR(setResult.OOWCardTaxIn)
        lblOOWCashSum0.Text = clsSet.setINR(setResult.OOWCashTaxIn)
        'claim
        lblOOWCardSum1.Text = clsSet.setINR(setResult.OOWCardSum)
        lblOOWCashSum1.Text = clsSet.setINR(setResult.OOWCashSum)

        'otherのdenomi毎amount合計値をセット（tax込み)
        'GSPN
        lblOtherCardSum0.Text = clsSet.setINR(setResult.otherCardTaxIn)
        lblOtherCashSum0.Text = clsSet.setINR(setResult.otherCashTaxIn)
        'claim
        lblOtherCardSum1.Text = clsSet.setINR(setResult.otherCardSum)
        lblOtherCashSum1.Text = clsSet.setINR(setResult.otherCashSum)

        '■金種(IW,other(card),other(cash),out of warranty(card),out of warranty(cash))毎件数
        'warranty,denomi毎件数値をセット
        lblIWCnt.Text = setResult.IWCnt
        lblOtherCardCnt.Text = setResult.otherCardCnt + setResult.otherCashCreditCount
        lblOtherCashCnt.Text = setResult.otherCashCnt + setResult.otherCashCreditCount
        lblOOWCardCnt.Text = setResult.OOWCardCnt + setResult.OOWCashCreditCount
        lblOOWCashCnt.Text = setResult.OOWCashCnt + setResult.OOWCashCreditCount

        '■準備金の設定
        lblReserve.Text = clsSet.setINR(setResult.reserve) & "INR"
        lblDeposit.Text = clsSet.setINR(setResult.deposit) & "INR"

        '■cash total(open dposit + sales(cash))
        '※準備金＋CLOSE処理(ORレジ点検最新)のtotal金額
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
        lblSales.Text = clsSet.setINR(tmp2.ToString) & "INR"

        '■お客様の製品と金銭のお預かりの集計
        lblDepositCusto.Text = clsSet.setINR2(setResult.customerTotalCash) & "INR"
        lblNumCusto.Text = setResult.CustomerTotalNumber

        '■discount
        lblDisNum.Text = setResult.discountNum + setResult.fullDiscountNum
        lblDisAmount.Text = clsSet.setINR2(setResult.discountAmount + setResult.fullDiscountAmount) & "INR"

    End Sub

    Protected Sub btnToday_Click(sender As Object, e As EventArgs) Handles btnToday.Click

        Dim clsSetCommon As New Class_common
        Dim dtNow As DateTime = clsSetCommon.dtIndia

        TextCompleteDateFrom.Text = dtNow.ToShortDateString

    End Sub

    Protected Sub reSet()

        lblIWSum0.Text = ""
        lblOOWCardSum0.Text = ""
        lblOOWCashSum0.Text = ""
        lblOtherCardSum0.Text = ""
        lblOtherCashSum0.Text = ""

        lblIWCnt.Text = ""
        lblOOWCardCnt.Text = ""
        lblOOWCashCnt.Text = ""
        lblOtherCardCnt.Text = ""
        lblOtherCashCnt.Text = ""

        lblReserve.Text = ""
        lblDeposit.Text = ""

        lblDepositCusto.Text = ""
        lblNumCusto.Text = ""

    End Sub

End Class