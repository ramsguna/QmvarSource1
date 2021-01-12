Public Class Money_Salse_show21
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        If IsPostBack = False Then

            '***セッションなしログインユーザの対応***
            Dim userid As String = Session("user_id")

            If userid = "" Then
                Response.Redirect("Login.aspx")
            End If

            '***セッション情報取得***
            'リストで選択された拠点情報
            Dim setShipName As String = Session("set_shipName")

            'Target Date
            Dim startDate As String = Session("start_Date")

            '***拠点コード取得***
            Dim clsSetCommon As New Class_common
            Dim shipCode As String
            Dim errMsg As String

            Call clsSetCommon.setShipCode(setShipName, shipCode, errMsg)

            If errMsg <> "" Then
                Call showMsg(errMsg)
                Exit Sub
            End If

            Dim shipCodeAll(0) As String
            shipCodeAll(0) = shipCode

            '***集計***
            Dim clsSet As New Class_money
            Dim setResult As Class_money.Aggregation

            '**表に表示する情報の取得**（cash_trackより売り上げ情報）
            clsSet.setSyukei(setResult, shipCode, startDate, errMsg)

            If errMsg <> "" Then
                Call showMsg(errMsg)
                Exit Sub
            End If

            lblLocation.Text = shipCode & " : " & setShipName

            '**集計結果表示　小数点以下は２桁まで**
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
            lblCashTotal.Text = clsSet.setINR2(setResult.cashTotal) & "INR"

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

            '■Bank Doposit
            '※cash total - deposit
            lblDeposit.Text = clsSet.setINR2(setResult.cashTotal - setResult.deposit) & "INR"

            '■お客様の製品と金銭のお預かりの集計
            lblDepositCusto.Text = clsSet.setINR2(setResult.customerTotalCash) & "INR"
            lblNumCusto.Text = setResult.CustomerTotalNumber

            '**リストに表示する情報の取得/表示**（T_Reserveよりレジ点検情報）
            Dim setResultList As Class_money.T_Reserve
            '1拠点確保
            Dim setRelultListALL(0) As Class_money.T_Reserve

            clsSet.setSyukeiList(setResultList, setRelultListALL, "", shipCodeAll, startDate, errMsg)

            If errMsg <> "" Then
                Call showMsg(errMsg)
                Exit Sub
            End If

            '１個目リスト
            For i = 0 To setRelultListALL.Length - 1

                If setRelultListALL(i).dataNo = 0 + 1 Then

                    '各ステータスの最新時刻取得用
                    Dim dtWork, dtWork2, dtWork3, dtWork4, dtWork5 As String

                    '各ステータスのdiffの判定結果取得用
                    Dim openResult, closeResult, ins1Result, ins2Result, ins3Result As String

                    Dim money_diff As Decimal

                    If setRelultListALL(0).open_maxDate <> "" Then

                        dtWork = DateTime.Parse(setRelultListALL(0).open_maxDate).ToShortTimeString

                        If setRelultListALL(0).open_diff = 0 Then
                            openResult = "OK"
                        Else
                            openResult = "NG"
                        End If

                    End If

                    If setRelultListALL(0).close_maxDate <> "" Then

                        dtWork2 = DateTime.Parse(setRelultListALL(0).close_maxDate).ToShortTimeString

                        If setRelultListALL(0).close_diff = 0 Then
                            closeResult = "OK"
                        Else
                            closeResult = "NG"
                        End If

                    End If

                    If setRelultListALL(0).ins1_maxDate <> "" Then

                        dtWork3 = DateTime.Parse(setRelultListALL(0).ins1_maxDate).ToShortTimeString

                        If setRelultListALL(0).ins1_diff = 0 Then
                            ins1Result = "OK"
                        Else
                            ins1Result = "NG"
                        End If

                    End If

                    If setRelultListALL(0).ins2_maxDate <> "" Then

                        dtWork4 = DateTime.Parse(setRelultListALL(0).ins2_maxDate).ToShortTimeString

                        If setRelultListALL(0).ins2_diff = 0 Then
                            ins2Result = "OK"
                        Else
                            ins2Result = "NG"
                        End If

                    End If

                    If setRelultListALL(0).ins3_maxDate <> "" Then

                        dtWork5 = DateTime.Parse(setRelultListALL(0).ins3_maxDate).ToShortTimeString

                        If setRelultListALL(0).ins3_diff = 0 Then
                            ins3Result = "OK"
                        Else
                            ins3Result = "NG"
                        End If

                    End If

                    List1Msg.Items.Add("Today's Result")

                    List1Msg.Items.Add("open time" & "　　　" & dtWork & "　" & openResult)

                    List1Msg.Items.Add("open youser" & "　" & setRelultListALL(0).open_youser_name)

                    List1Msg.Items.Add("close time" & "　　　" & dtWork2 & "　" & closeResult)

                    List1Msg.Items.Add("close youser" & "　" & setRelultListALL(0).close_youser_name)

                    List1Msg.Items.Add("inspection")

                    List1Msg.Items.Add("1st" & "　" & dtWork3 & "　" & ins1Result & "　" & setRelultListALL(0).ins1_youser_name)

                    List1Msg.Items.Add("2nd" & "　" & dtWork4 & "　" & ins2Result & "　" & setRelultListALL(0).ins2_youser_name)

                    List1Msg.Items.Add("3rd" & "　" & dtWork5 & "　" & ins3Result & "　" & setRelultListALL(0).ins3_youser_name)

                    If setRelultListALL(0).close_maxDate IsNot Nothing Then
                        money_diff = setRelultListALL(0).close_diff
                    Else
                        If setRelultListALL(0).ins3_maxDate IsNot Nothing Then
                            money_diff = setRelultListALL(0).ins3_diff
                        Else
                            If setRelultListALL(0).ins2_maxDate IsNot Nothing Then
                                money_diff = setRelultListALL(0).ins2_diff
                            Else
                                If setRelultListALL(0).ins1_maxDate IsNot Nothing Then
                                    money_diff = setRelultListALL(0).ins1_diff
                                Else
                                    If setRelultListALL(0).open_maxDate IsNot Nothing Then
                                        money_diff = setRelultListALL(0).open_diff
                                    End If
                                End If
                            End If
                        End If
                    End If

                    List1Msg.Items.Add("money diifference" & "　　" & money_diff & "INR")

                    List1Msg.Items.Add("Attend today number")

                    List1Msg.Items.Add("late")

                    List1Msg.Items.Add("absence")

                End If

            Next i

        End If

    End Sub

    Protected Sub showMsg(ByVal Msg As String)

        lblMsg.Text = Msg
        Dim sScript As String = "$(function () { $( ""#dialog"" ).dialog({width: 400, buttons: {""OK"": function () {$(this).dialog('close');}}});});"
        'JavaScriptの埋め込み
        ClientScript.RegisterStartupScript(Me.GetType(), "startup", sScript, True)

    End Sub

    Protected Sub btnBack_Click1(sender As Object, e As ImageClickEventArgs) Handles btnBack.Click

        Session("set_shipName") = Nothing
        Response.Redirect("Money_Salse.aspx")

    End Sub

End Class