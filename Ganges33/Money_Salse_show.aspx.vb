Public Class Money_Salse_show1
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        '各ページで表示する表の数
        Const showDataCount As Integer = 5

        If IsPostBack = False Then

            '***セッションなしログインユーザの対応***
            Dim userid As String = Session("user_id")

            If userid = "" Then
                Response.Redirect("Login.aspx")
            End If

            '***セッション情報取得***
            'Target Date
            Dim startDate As String = Session("start_Date")

            '全ての拠点情報
            Dim shipName() As String = Session("ship_name_list")
            Dim shipCodeAll() As String = Session("shipCode_All")

            Dim pageNo As Integer = Session("page_NO")

            Dim btnNextCnt As Integer = Session("btnNext_Cnt")

            Dim btnStartCnt As Integer = Session("btnStart_Cnt")

            Dim lastShowNo As Integer = Session("last_ShowNO")

            '***全拠点コードのカンマ区切り設定***
            Dim shipAllString As String
            For i = 0 To shipCodeAll.Length - 1
                If Trim(shipCodeAll(i)) <> "" Or Trim(shipCodeAll(i)) <> vbNullString Then
                    shipAllString = shipAllString & "'" & shipCodeAll(i) & "',"
                End If
            Next i

            '末尾のカンマを除く
            shipAllString = Left(shipAllString, Len(shipAllString) - 1)

            '***集計***
            Dim clsSet As New Class_money

            '拠点毎で集計結果取得用
            Dim setResultALL(shipCodeAll.Length - 1) As Class_money.Aggregation
            Dim setRelultListALL(shipCodeAll.Length - 1) As Class_money.T_Reserve

            '拠点指定で集計結果取得用
            Dim setResult As Class_money.Aggregation
            Dim setResultList As Class_money.T_Reserve

            Dim errMsg As String = ""

            '**表に表示する情報の取得（cash_trackより売り上げ情報）**
            clsSet.setSyukei2(setResult, setResultALL, shipAllString, shipCodeAll, startDate, errMsg)

            If errMsg <> "" Then
                Call showMsg(errMsg)
                Exit Sub
            End If

            '**リストに表示する情報の取得（T_Reserveよりレジ点検情報）**
            clsSet.setSyukeiList(setResultList, setRelultListALL, shipAllString, shipCodeAll, startDate, errMsg)

            If errMsg <> "" Then
                Call showMsg(errMsg)
                Exit Sub
            End If

            ''**表示**
            '■■全件■■
            '■金種毎合計金額
            'IW（taxin）
            lblIWSum0.Text = clsSet.setINR2(setResult.IWTaxIn)

            'IW（notax）
            lblIWNoTax.Text = clsSet.setINR2(setResult.IWTaxIn / 1.18)

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

            '□□claim□□
            'IW（taxin）
            lblIWClaim.Text = clsSet.setINR2(setResult.IWSum)

            'IW（notax）
            lblIWNoTaxCla.Text = clsSet.setINR2(setResult.IWSum / 1.18)

            'OOWのdenomi毎amount合計値をセット(tax込み)
            lblOWCardClaim.Text = clsSet.setINR2(setResult.OOWCardSum)
            lblOWCashClaim.Text = clsSet.setINR2(setResult.OOWCashSum)

            'otherのdenomi毎amount合計値をセット（tax込み)
            lblOtCardClaim.Text = clsSet.setINR2(setResult.otherCardSum)
            lblOtCashClaim.Text = clsSet.setINR2(setResult.otherCashSum)

            'OOWのdenomi毎amount合計値をセット(tax抜き)
            lblOWCardNoTaxCla.Text = clsSet.setINR2(setResult.OOWCardSum / 1.18)
            lblOWCashNoTaxCla.Text = clsSet.setINR2(setResult.OOWCashSum / 1.18)

            'otherのdenomi毎amount合計値をセット（tax抜き)
            lblOtCardNoTaxCla.Text = clsSet.setINR2(setResult.otherCardSum / 1.18)
            lblOtCashNoTaxCla.Text = clsSet.setINR2(setResult.otherCashSum / 1.18)

            '■金種(IW,other(card),other(cash),out of warranty(card),out of warranty(cash))毎件数
            'warranty,denomi毎件数値をセット
            lblIWCnt.Text = setResult.IWCnt
            lblOtherCardCnt.Text = setResult.otherCardCnt + setResult.otherCashCreditCount
            lblOtherCashCnt.Text = setResult.otherCashCnt + setResult.otherCashCreditCount
            lblOOWCardCnt.Text = setResult.OOWCardCnt + setResult.OOWCashCreditCount
            lblOOWCashCnt.Text = setResult.OOWCashCnt + setResult.OOWCashCreditCount

            '■cash total(open dposit + sales(cash))
            '※拠点毎の最新レジチェック時のtotal金額の合計
            lblCashTotal.Text = clsSet.setINR2(setResult.cashTotal) & "INR"

            '■bank deposit
            '※cash total - deposit
            lblDeposit.Text = clsSet.setINR2(setResult.cashTotal - setResult.deposit) & "INR"

            '■sales(sales(cash) + sales(card)+IW+other+GST)
            Dim tmp2 As Decimal

            If lblIWClaim.Text <> "" Then
                tmp2 = tmp2 + Convert.ToDecimal(lblIWClaim.Text)
            End If

            If lblOWCardClaim.Text <> "" Then
                tmp2 = tmp2 + Convert.ToDecimal(lblOWCardClaim.Text)
            End If

            If lblOWCashClaim.Text <> "" Then
                tmp2 = tmp2 + Convert.ToDecimal(lblOWCashClaim.Text)
            End If

            If lblOtCardClaim.Text <> "" Then
                tmp2 = tmp2 + Convert.ToDecimal(lblOtCardClaim.Text)
            End If

            If lblOtCashClaim.Text <> "" Then
                tmp2 = tmp2 + Convert.ToDecimal(lblOtCashClaim.Text)
            End If

            lblSales.Text = clsSet.setINR(tmp2.ToString) & "INR"

            Dim shipCountAll As Integer = shipCodeAll.Length

            '■お客様の製品と金銭のお預かりの集計
            lblDepositCusto.Text = clsSet.setINR2(setResult.customerTotalCash) & "INR"
            lblNumCusto.Text = setResult.CustomerTotalNumber

            '■■表示するページ数の設定■■
            'setResultALL配列の番号を設定
            Dim showNo As Integer = -1

            If pageNo = 0 Then

                If shipCountAll <= showDataCount Then
                    '拠点数が、１頁に表示する表の個数以下の場合
                    pageNo = 1
                Else
                    If shipCountAll Mod showDataCount = 0 Then
                        pageNo = shipCountAll \ showDataCount
                    Else
                        pageNo = (shipCountAll \ showDataCount) + 1
                    End If
                End If

                Session("page_NO") = pageNo

                showNo = 0

            Else
                'startpageボタン押下あり
                If btnStartCnt <> 0 Then
                    showNo = 0
                    btnStartCnt = 0
                    Session("btnStart_Cnt") = btnStartCnt
                    Session("btnNext_Cnt") = 0
                Else
                    '次ページボタン押下あり
                    If btnNextCnt < pageNo Then
                        showNo = btnNextCnt * showDataCount
                        Call showMsg(btnNextCnt + 1 & " Page")
                    Else
                        If lastShowNo = 0 Then
                            showNo = (btnNextCnt - 1) * showDataCount
                        Else
                            showNo = Session("last_ShowNO")
                        End If
                        Session("last_ShowNO") = showNo
                        Call showMsg("Last Page")
                    End If
                End If

            End If

            '■■拠点毎■■
            '１個目リスト
            For i = showNo To setRelultListALL.Length - 1

                If setRelultListALL(i).dataNo = showNo + 1 Then

                    Dim dtWork, dtWork2, dtWork3, dtWork4, dtWork5 As String

                    Dim openResult, closeResult, ins1Result, ins2Result, ins3Result As String

                    Dim money_diff As Decimal

                    If setRelultListALL(showNo).open_maxDate <> "" Then

                        dtWork = DateTime.Parse(setRelultListALL(showNo).open_maxDate).ToShortTimeString

                        If setRelultListALL(showNo).open_diff = 0 Then
                            openResult = "OK"
                        Else
                            openResult = "NG"
                        End If

                    End If

                    If setRelultListALL(showNo).close_maxDate <> "" Then

                        dtWork2 = DateTime.Parse(setRelultListALL(showNo).close_maxDate).ToShortTimeString

                        If setRelultListALL(showNo).close_diff = 0 Then
                            closeResult = "OK"
                        Else
                            closeResult = "NG"
                        End If

                    End If

                    If setRelultListALL(showNo).ins1_maxDate <> "" Then

                        dtWork3 = DateTime.Parse(setRelultListALL(showNo).ins1_maxDate).ToShortTimeString

                        If setRelultListALL(showNo).ins1_diff = 0 Then
                            ins1Result = "OK"
                        Else
                            ins1Result = "NG"
                        End If

                    End If

                    If setRelultListALL(showNo).ins2_maxDate <> "" Then

                        dtWork4 = DateTime.Parse(setRelultListALL(showNo).ins2_maxDate).ToShortTimeString

                        If setRelultListALL(showNo).ins2_diff = 0 Then
                            ins2Result = "OK"
                        Else
                            ins2Result = "NG"
                        End If

                    End If

                    If setRelultListALL(showNo).ins3_maxDate <> "" Then

                        dtWork5 = DateTime.Parse(setRelultListALL(showNo).ins3_maxDate).ToShortTimeString

                        If setRelultListALL(showNo).ins3_diff = 0 Then
                            ins3Result = "OK"
                        Else
                            ins3Result = "NG"
                        End If

                    End If

                    If setRelultListALL(showNo).close_maxDate IsNot Nothing Then
                        money_diff = setRelultListALL(showNo).close_diff
                    Else
                        If setRelultListALL(showNo).ins3_maxDate IsNot Nothing Then
                            money_diff = setRelultListALL(showNo).ins3_diff
                        Else
                            If setRelultListALL(showNo).ins2_maxDate IsNot Nothing Then
                                money_diff = setRelultListALL(showNo).ins2_diff
                            Else
                                If setRelultListALL(showNo).ins1_maxDate IsNot Nothing Then
                                    money_diff = setRelultListALL(showNo).ins1_diff
                                Else
                                    If setRelultListALL(showNo).open_maxDate IsNot Nothing Then
                                        money_diff = setRelultListALL(showNo).open_diff
                                    End If
                                End If
                            End If
                        End If
                    End If

                    List1Msg.Items.Add("Today's Result")

                    List1Msg.Items.Add("open time" & "　　" & dtWork & "　" & openResult)

                    List1Msg.Items.Add("open youser" & "　" & setRelultListALL(showNo).open_youser_name)

                    List1Msg.Items.Add("close time" & "　　" & dtWork2 & "　" & closeResult)

                    List1Msg.Items.Add("close youser" & "　" & setRelultListALL(showNo).close_youser_name)

                    List1Msg.Items.Add("inspection")

                    List1Msg.Items.Add("1st" & "　" & dtWork3 & "　" & ins1Result & "　" & setRelultListALL(showNo).ins1_youser_name)

                    List1Msg.Items.Add("2nd" & "　" & dtWork4 & "　" & ins2Result & "　" & setRelultListALL(showNo).ins2_youser_name)

                    List1Msg.Items.Add("3rd" & "　" & dtWork5 & "　" & ins3Result & "　" & setRelultListALL(showNo).ins3_youser_name)

                    If money_diff.ToString <> "0" Then
                        List1Msg.Items.Add("money diifference" & "　　" & money_diff & "INR")
                    Else
                        List1Msg.Items.Add("money diifference")
                    End If

                    List1Msg.Items.Add("Attend today number")

                    List1Msg.Items.Add("late")

                    List1Msg.Items.Add("absence")

                ElseIf setRelultListALL(i).dataNo = showNo + 2 Then
                    '２個目リスト

                    Dim dtWork, dtWork2, dtWork3, dtWork4, dtWork5 As String

                    Dim openResult, closeResult, ins1Result, ins2Result, ins3Result As String

                    Dim money_diff As Decimal

                    If setRelultListALL(showNo + 1).open_maxDate <> "" Then

                        dtWork = DateTime.Parse(setRelultListALL(showNo + 1).open_maxDate).ToShortTimeString

                        If setRelultListALL(showNo + 1).open_diff = 0 Then
                            openResult = "OK"
                        Else
                            openResult = "NG"
                        End If

                    End If

                    If setRelultListALL(showNo + 1).close_maxDate <> "" Then

                        dtWork2 = DateTime.Parse(setRelultListALL(showNo + 1).close_maxDate).ToShortTimeString

                        If setRelultListALL(showNo + 1).close_diff = 0 Then
                            closeResult = "OK"
                        Else
                            closeResult = "NG"
                        End If

                    End If

                    If setRelultListALL(showNo + 1).ins1_maxDate <> "" Then

                        dtWork3 = DateTime.Parse(setRelultListALL(showNo + 1).ins1_maxDate).ToShortTimeString

                        If setRelultListALL(showNo + 1).ins1_diff = 0 Then
                            ins1Result = "OK"
                        Else
                            ins1Result = "NG"
                        End If

                    End If

                    If setRelultListALL(showNo + 1).ins2_maxDate <> "" Then

                        dtWork4 = DateTime.Parse(setRelultListALL(showNo + 1).ins2_maxDate).ToShortTimeString

                        If setRelultListALL(showNo + 1).ins2_diff = 0 Then
                            ins2Result = "OK"
                        Else
                            ins2Result = "NG"
                        End If

                    End If

                    If setRelultListALL(showNo + 1).ins3_maxDate <> "" Then

                        dtWork5 = DateTime.Parse(setRelultListALL(showNo + 1).ins3_maxDate).ToShortTimeString

                        If setRelultListALL(showNo + 1).ins3_diff = 0 Then
                            ins3Result = "OK"
                        Else
                            ins3Result = "NG"
                        End If

                    End If

                    If setRelultListALL(showNo + 1).close_maxDate IsNot Nothing Then
                        money_diff = setRelultListALL(showNo + 1).close_diff
                    Else
                        If setRelultListALL(showNo + 1).ins3_maxDate IsNot Nothing Then
                            money_diff = setRelultListALL(showNo + 1).ins3_diff
                        Else
                            If setRelultListALL(showNo + 1).ins2_maxDate IsNot Nothing Then
                                money_diff = setRelultListALL(showNo + 1).ins2_diff
                            Else
                                If setRelultListALL(showNo + 1).ins1_maxDate IsNot Nothing Then
                                    money_diff = setRelultListALL(showNo + 1).ins1_diff
                                Else
                                    If setRelultListALL(showNo + 1).open_maxDate IsNot Nothing Then
                                        money_diff = setRelultListALL(showNo + 1).open_diff
                                    End If
                                End If
                            End If
                        End If
                    End If

                    List2Msg.Items.Add("Today's Result")

                    List2Msg.Items.Add("open time " & "　　" & dtWork & "　" & openResult)

                    List2Msg.Items.Add("open youser " & "　" & setRelultListALL(showNo + 1).open_youser_name)

                    List2Msg.Items.Add("close time" & "　　" & dtWork2 & "　" & closeResult)

                    List2Msg.Items.Add("close youser" & "　" & setRelultListALL(showNo + 1).close_youser_name)

                    List2Msg.Items.Add("inspection")

                    List2Msg.Items.Add("1st" & "　" & dtWork3 & "　" & ins1Result & "　" & setRelultListALL(showNo + 1).ins1_youser_name)

                    List2Msg.Items.Add("2nd" & "　" & dtWork4 & "　" & ins2Result & "　" & setRelultListALL(showNo + 1).ins2_youser_name)

                    List2Msg.Items.Add("3rd" & "　" & dtWork5 & "　" & ins3Result & "　" & setRelultListALL(showNo + 1).ins3_youser_name)

                    If money_diff.ToString <> "0" Then
                        List2Msg.Items.Add("money diifference" & "　　" & money_diff & "INR")
                    Else
                        List2Msg.Items.Add("money diifference")
                    End If

                    List2Msg.Items.Add("Attend today number")

                    List2Msg.Items.Add("late")

                    List2Msg.Items.Add("absence")

                ElseIf setRelultListALL(i).dataNo = showNo + 3 Then
                    '３個目リスト

                    Dim dtWork, dtWork2, dtWork3, dtWork4, dtWork5 As String

                    Dim openResult, closeResult, ins1Result, ins2Result, ins3Result As String

                    Dim money_diff As Decimal

                    If setRelultListALL(showNo + 2).open_maxDate <> "" Then

                        dtWork = DateTime.Parse(setRelultListALL(showNo + 2).open_maxDate).ToShortTimeString

                        If setRelultListALL(showNo + 2).open_diff = 0 Then
                            openResult = "OK"
                        Else
                            openResult = "NG"
                        End If

                    End If

                    If setRelultListALL(showNo + 2).close_maxDate <> "" Then

                        dtWork2 = DateTime.Parse(setRelultListALL(showNo + 2).close_maxDate).ToShortTimeString

                        If setRelultListALL(showNo + 2).close_diff = 0 Then
                            closeResult = "OK"
                        Else
                            closeResult = "NG"
                        End If

                    End If

                    If setRelultListALL(showNo + 2).ins1_maxDate <> "" Then

                        dtWork3 = DateTime.Parse(setRelultListALL(showNo + 2).ins1_maxDate).ToShortTimeString

                        If setRelultListALL(showNo + 2).ins1_diff = 0 Then
                            ins1Result = "OK"
                        Else
                            ins1Result = "NG"
                        End If

                    End If

                    If setRelultListALL(showNo + 2).ins2_maxDate <> "" Then

                        dtWork4 = DateTime.Parse(setRelultListALL(showNo + 2).ins2_maxDate).ToShortTimeString

                        If setRelultListALL(showNo + 2).ins2_diff = 0 Then
                            ins2Result = "OK"
                        Else
                            ins2Result = "NG"
                        End If

                    End If

                    If setRelultListALL(showNo + 2).ins3_maxDate <> "" Then

                        dtWork5 = DateTime.Parse(setRelultListALL(showNo + 2).ins3_maxDate).ToShortTimeString

                        If setRelultListALL(showNo + 2).ins3_diff = 0 Then
                            ins3Result = "OK"
                        Else
                            ins3Result = "NG"
                        End If

                    End If

                    If setRelultListALL(showNo + 2).close_maxDate IsNot Nothing Then
                        money_diff = setRelultListALL(showNo + 2).close_diff
                    Else
                        If setRelultListALL(showNo + 2).ins3_maxDate IsNot Nothing Then
                            money_diff = setRelultListALL(showNo + 2).ins3_diff
                        Else
                            If setRelultListALL(showNo + 2).ins2_maxDate IsNot Nothing Then
                                money_diff = setRelultListALL(showNo + 2).ins2_diff
                            Else
                                If setRelultListALL(showNo + 2).ins1_maxDate IsNot Nothing Then
                                    money_diff = setRelultListALL(showNo + 2).ins1_diff
                                Else
                                    If setRelultListALL(showNo + 2).open_maxDate IsNot Nothing Then
                                        money_diff = setRelultListALL(showNo + 2).open_diff
                                    End If
                                End If
                            End If
                        End If
                    End If

                    List3Msg.Items.Add("Today's Result")

                    List3Msg.Items.Add("open time " & "　　" & dtWork & "　" & openResult)

                    List3Msg.Items.Add("open youser " & "　" & setRelultListALL(showNo + 2).open_youser_name)

                    List3Msg.Items.Add("close time" & "　　" & dtWork2 & "　" & closeResult)

                    List3Msg.Items.Add("close youser" & "　" & setRelultListALL(showNo + 2).close_youser_name)

                    List3Msg.Items.Add("inspection")

                    List3Msg.Items.Add("1st" & "　" & dtWork3 & "　" & ins1Result & "　" & setRelultListALL(showNo + 2).ins1_youser_name)

                    List3Msg.Items.Add("2nd" & "　" & dtWork4 & "　" & ins2Result & "　" & setRelultListALL(showNo + 2).ins2_youser_name)

                    List3Msg.Items.Add("3rd" & "　" & dtWork5 & "　" & ins3Result & "　" & setRelultListALL(showNo + 2).ins3_youser_name)

                    If money_diff.ToString <> "0" Then
                        List3Msg.Items.Add("money diifference" & "　　" & money_diff & "INR")
                    Else
                        List3Msg.Items.Add("money diifference")
                    End If

                    List3Msg.Items.Add("Attend today number")

                    List3Msg.Items.Add("late")

                    List3Msg.Items.Add("absence")

                ElseIf setRelultListALL(i).dataNo = showNo + 4 Then
                    '４個目リスト

                    Dim dtWork, dtWork2, dtWork3, dtWork4, dtWork5 As String

                    Dim openResult, closeResult, ins1Result, ins2Result, ins3Result As String

                    Dim money_diff As Decimal

                    If setRelultListALL(showNo + 3).open_maxDate <> "" Then

                        dtWork = DateTime.Parse(setRelultListALL(showNo + 3).open_maxDate).ToShortTimeString

                        If setRelultListALL(showNo + 3).open_diff = 0 Then
                            openResult = "OK"
                        Else
                            openResult = "NG"
                        End If

                    End If

                    If setRelultListALL(showNo + 3).close_maxDate <> "" Then

                        dtWork2 = DateTime.Parse(setRelultListALL(showNo + 3).close_maxDate).ToShortTimeString

                        If setRelultListALL(showNo + 3).close_diff = 0 Then
                            closeResult = "OK"
                        Else
                            closeResult = "NG"
                        End If

                    End If

                    If setRelultListALL(showNo + 3).ins1_maxDate <> "" Then

                        dtWork3 = DateTime.Parse(setRelultListALL(showNo + 3).ins1_maxDate).ToShortTimeString

                        If setRelultListALL(showNo + 3).ins1_diff = 0 Then
                            ins1Result = "OK"
                        Else
                            ins1Result = "NG"
                        End If

                    End If

                    If setRelultListALL(showNo + 3).ins2_maxDate <> "" Then

                        dtWork4 = DateTime.Parse(setRelultListALL(showNo + 3).ins2_maxDate).ToShortTimeString

                        If setRelultListALL(showNo + 3).ins2_diff = 0 Then
                            ins2Result = "OK"
                        Else
                            ins2Result = "NG"
                        End If

                    End If

                    If setRelultListALL(showNo + 3).ins3_maxDate <> "" Then

                        dtWork5 = DateTime.Parse(setRelultListALL(showNo + 3).ins3_maxDate).ToShortTimeString

                        If setRelultListALL(showNo + 3).ins3_diff = 0 Then
                            ins3Result = "OK"
                        Else
                            ins3Result = "NG"
                        End If

                    End If

                    If setRelultListALL(showNo + 3).close_maxDate IsNot Nothing Then
                        money_diff = setRelultListALL(showNo + 3).close_diff
                    Else
                        If setRelultListALL(showNo + 3).ins3_maxDate IsNot Nothing Then
                            money_diff = setRelultListALL(showNo + 3).ins3_diff
                        Else
                            If setRelultListALL(showNo + 3).ins2_maxDate IsNot Nothing Then
                                money_diff = setRelultListALL(showNo + 3).ins2_diff
                            Else
                                If setRelultListALL(showNo + 3).ins1_maxDate IsNot Nothing Then
                                    money_diff = setRelultListALL(showNo + 3).ins1_diff
                                Else
                                    If setRelultListALL(showNo + 3).open_maxDate IsNot Nothing Then
                                        money_diff = setRelultListALL(showNo + 3).open_diff
                                    End If
                                End If
                            End If
                        End If
                    End If

                    List4Msg.Items.Add("Today's Result")

                    List4Msg.Items.Add("open time " & "　　" & dtWork & "　" & openResult)

                    List4Msg.Items.Add("open youser " & "　" & setRelultListALL(showNo + 3).open_youser_name)

                    List4Msg.Items.Add("close time" & "　　" & dtWork2 & "　" & closeResult)

                    List4Msg.Items.Add("close youser" & "　" & setRelultListALL(showNo + 3).close_youser_name)

                    List4Msg.Items.Add("inspection")

                    List4Msg.Items.Add("1st" & "　" & dtWork3 & "　" & ins1Result & "　" & setRelultListALL(showNo + 3).ins1_youser_name)

                    List4Msg.Items.Add("2nd" & "　" & dtWork4 & "　" & ins2Result & "　" & setRelultListALL(showNo + 3).ins2_youser_name)

                    List4Msg.Items.Add("3rd" & "　" & dtWork5 & "　" & ins3Result & "　" & setRelultListALL(showNo + 3).ins3_youser_name)

                    If money_diff.ToString <> "0" Then
                        List4Msg.Items.Add("money diifference" & "　　" & money_diff & "INR")
                    Else
                        List4Msg.Items.Add("money diifference")
                    End If

                    List4Msg.Items.Add("Attend today number")

                    List4Msg.Items.Add("late")

                    List4Msg.Items.Add("absence")

                ElseIf setRelultListALL(i).dataNo = showNo + 5 Then
                    '５個目リスト

                    Dim dtWork, dtWork2, dtWork3, dtWork4, dtWork5 As String

                    Dim openResult, closeResult, ins1Result, ins2Result, ins3Result As String

                    Dim money_diff As Decimal

                    If setRelultListALL(showNo + 4).open_maxDate <> "" Then

                        dtWork = DateTime.Parse(setRelultListALL(showNo + 4).open_maxDate).ToShortTimeString

                        If setRelultListALL(showNo + 4).open_diff = 0 Then
                            openResult = "OK"
                        Else
                            openResult = "NG"
                        End If

                    End If

                    If setRelultListALL(showNo + 4).close_maxDate <> "" Then

                        dtWork2 = DateTime.Parse(setRelultListALL(showNo + 4).close_maxDate).ToShortTimeString

                        If setRelultListALL(showNo + 4).close_diff = 0 Then
                            closeResult = "OK"
                        Else
                            closeResult = "NG"
                        End If

                    End If

                    If setRelultListALL(showNo + 4).ins1_maxDate <> "" Then

                        dtWork3 = DateTime.Parse(setRelultListALL(showNo + 4).ins1_maxDate).ToShortTimeString

                        If setRelultListALL(showNo + 4).ins1_diff = 0 Then
                            ins1Result = "OK"
                        Else
                            ins1Result = "NG"
                        End If

                    End If

                    If setRelultListALL(showNo + 4).ins2_maxDate <> "" Then

                        dtWork4 = DateTime.Parse(setRelultListALL(showNo + 4).ins2_maxDate).ToShortTimeString

                        If setRelultListALL(showNo + 4).ins2_diff = 0 Then
                            ins2Result = "OK"
                        Else
                            ins2Result = "NG"
                        End If

                    End If

                    If setRelultListALL(showNo + 4).ins3_maxDate <> "" Then

                        dtWork5 = DateTime.Parse(setRelultListALL(showNo + 4).ins3_maxDate).ToShortTimeString

                        If setRelultListALL(showNo + 4).ins3_diff = 0 Then
                            ins3Result = "OK"
                        Else
                            ins3Result = "NG"
                        End If

                    End If

                    If setRelultListALL(showNo + 4).close_maxDate IsNot Nothing Then
                        money_diff = setRelultListALL(showNo + 4).close_diff
                    Else
                        If setRelultListALL(showNo + 4).ins3_maxDate IsNot Nothing Then
                            money_diff = setRelultListALL(showNo + 4).ins3_diff
                        Else
                            If setRelultListALL(showNo + 4).ins2_maxDate IsNot Nothing Then
                                money_diff = setRelultListALL(showNo + 4).ins2_diff
                            Else
                                If setRelultListALL(showNo + 4).ins1_maxDate IsNot Nothing Then
                                    money_diff = setRelultListALL(showNo + 4).ins1_diff
                                Else
                                    If setRelultListALL(showNo + 4).open_maxDate IsNot Nothing Then
                                        money_diff = setRelultListALL(showNo + 4).open_diff
                                    End If
                                End If
                            End If
                        End If
                    End If

                    List5Msg.Items.Add("Today's Result")

                    List5Msg.Items.Add("open time " & "　　" & dtWork & "　" & openResult)

                    List5Msg.Items.Add("open youser " & "　" & setRelultListALL(showNo + 4).open_youser_name)

                    List5Msg.Items.Add("close time" & "　　" & dtWork2 & "　" & closeResult)

                    List5Msg.Items.Add("close youser" & "　" & setRelultListALL(showNo + 4).close_youser_name)

                    List5Msg.Items.Add("inspection")

                    List5Msg.Items.Add("1st" & "　" & dtWork3 & "　" & ins1Result & "　" & setRelultListALL(showNo + 4).ins1_youser_name)

                    List5Msg.Items.Add("2nd" & "　" & dtWork4 & "　" & ins2Result & "　" & setRelultListALL(showNo + 4).ins2_youser_name)

                    List5Msg.Items.Add("3rd" & "　" & dtWork5 & "　" & ins3Result & "　" & setRelultListALL(showNo + 4).ins3_youser_name)

                    If money_diff.ToString <> "0" Then
                        List5Msg.Items.Add("money diifference" & "　　" & money_diff & "INR")
                    Else
                        List5Msg.Items.Add("money diifference")
                    End If

                    List5Msg.Items.Add("Attend today number")

                    List5Msg.Items.Add("late")

                    List5Msg.Items.Add("absence")

                End If

            Next i

            '■■拠点毎■■
            '１個目表
            For i = showNo To setResultALL.Length - 1

                If setResultALL(i).dataNo = showNo + 1 Then

                    '拠点コード
                    lblLocation1.Text = setResultALL(showNo).shipCode

                    'IWの合計値をセット
                    'taxin
                    lblIWSum1.Text = clsSet.setINR2(setResultALL(showNo).IWTaxIn)

                    'notax
                    lblIWNoTax1.Text = clsSet.setINR2(setResultALL(showNo).IWTaxIn / 1.18)

                    'OOWのdenomi毎amount合計値をセット(tax込み)
                    lblOOWCardSum1.Text = clsSet.setINR2(setResultALL(showNo).OOWCardTaxIn)
                    lblOOWCashSum1.Text = clsSet.setINR2(setResultALL(showNo).OOWCashTaxIn)

                    'otherのdenomi毎amount合計値をセット（tax込み)
                    lblOtherCardSum1.Text = clsSet.setINR2(setResultALL(showNo).otherCardTaxIn)
                    lblOtherCashSum1.Text = clsSet.setINR2(setResultALL(showNo).otherCashTaxIn)

                    'OOWのdenomi毎amount合計値をセット(tax抜き)
                    lblOWCardNoTax1.Text = clsSet.setINR2(setResultALL(showNo).OOWCardTaxIn / 1.18)
                    lblOWCashNoTax1.Text = clsSet.setINR2(setResultALL(showNo).OOWCashTaxIn / 1.18)

                    'otherのdenomi毎amount合計値をセット（tax抜き)
                    lblOtCardNoTax1.Text = clsSet.setINR2(setResultALL(showNo).otherCardTaxIn / 1.18)
                    lblOtCashNoTax1.Text = clsSet.setINR2(setResultALL(showNo).otherCashTaxIn / 1.18)

                    '□□claim□□
                    'IWの合計値をセット
                    'taxin
                    lblIWClaim1.Text = clsSet.setINR2(setResultALL(showNo).IWSum)

                    'notax
                    lblIWNoTaxCla1.Text = clsSet.setINR2(setResultALL(showNo).IWSum / 1.18)

                    'OOWのdenomi毎amount合計値をセット(tax込み)
                    lblOWCardClaim1.Text = clsSet.setINR2(setResultALL(showNo).OOWCardSum)
                    lblOWCashClaim1.Text = clsSet.setINR2(setResultALL(showNo).OOWCashSum)

                    'otherのdenomi毎amount合計値をセット（tax込み)
                    lblOtCardClaim1.Text = clsSet.setINR2(setResultALL(showNo).otherCardSum)
                    lblOtCashClaim1.Text = clsSet.setINR2(setResultALL(showNo).otherCashSum)

                    'OOWのdenomi毎amount合計値をセット(tax抜き)
                    lblOWCardNoTaxCla1.Text = clsSet.setINR2(setResultALL(showNo).OOWCardSum / 1.18)
                    lblOWCashNoTaxCla1.Text = clsSet.setINR2(setResultALL(showNo).OOWCashSum / 1.18)

                    'otherのdenomi毎amount合計値をセット（tax抜き)
                    lblOtCardNoTaxCla1.Text = clsSet.setINR2(setResultALL(showNo).otherCardSum / 1.18)
                    lblOtCashNoTaxCla1.Text = clsSet.setINR2(setResultALL(showNo).otherCashSum / 1.18)

                    '■金種(IW,other(card),other(cash),out of warranty(card),out of warranty(cash))毎件数
                    'warranty,denomi毎件数値をセット
                    lblIWCnt1.Text = setResultALL(showNo).IWCnt
                    lblOtherCardCnt1.Text = setResultALL(showNo).otherCardCnt + setResultALL(showNo).otherCashCreditCount
                    lblOtherCashCnt1.Text = setResultALL(showNo).otherCashCnt + setResultALL(showNo).otherCashCreditCount
                    lblOOWCardCnt1.Text = setResultALL(showNo).OOWCardCnt + setResultALL(showNo).OOWCashCreditCount
                    lblOOWCashCnt1.Text = setResultALL(showNo).OOWCashCnt + setResultALL(showNo).OOWCashCreditCount

                    '■cash total(open dposit + sales(cash))
                    '※拠点毎の最新レジチェック時のtotal金額の合計
                    lblCashTotal1.Text = clsSet.setINR2(setResultALL(showNo).cashTotal) & "INR"

                    '■sales(sales(cash) + sales(card)+IW+other+GST)

                    tmp2 = 0

                    If lblIWClaim1.Text <> "" Then
                        tmp2 = tmp2 + Convert.ToDecimal(lblIWClaim1.Text)
                    End If

                    If lblOWCardClaim1.Text <> "" Then
                        tmp2 = tmp2 + Convert.ToDecimal(lblOWCardClaim1.Text)
                    End If

                    If lblOWCashClaim1.Text <> "" Then
                        tmp2 = tmp2 + Convert.ToDecimal(lblOWCashClaim1.Text)
                    End If

                    If lblOtCardClaim1.Text <> "" Then
                        tmp2 = tmp2 + Convert.ToDecimal(lblOtCardClaim1.Text)
                    End If

                    If lblOtCashClaim1.Text <> "" Then
                        tmp2 = tmp2 + Convert.ToDecimal(lblOtCashClaim1.Text)
                    End If

                    lblSales1.Text = clsSet.setINR(tmp2.ToString) & "INR"

                    '■お客様の製品と金銭のお預かりの集計
                    lblDepositCusto1.Text = clsSet.setINR2(setResultALL(showNo).customerTotalCash) & "INR"
                    lblNumCusto1.Text = setResultALL(showNo).CustomerTotalNumber

                    '■bank deposit
                    '※cash total - deposit
                    lblDeposit1.Text = clsSet.setINR2(setResultALL(showNo).cashTotal - setResultALL(showNo).deposit) & "INR"

                ElseIf setResultALL(i).dataNo = showNo + 2 Then
                    '２個目表

                    '拠点コード
                    lblLocation2.Text = setResultALL(showNo + 1).shipCode

                    'IWの合計値をセット
                    'taxin
                    lblIWSum2.Text = clsSet.setINR2(setResultALL(showNo + 1).IWTaxIn)

                    'notax
                    lblIWNoTax2.Text = clsSet.setINR2(setResultALL(showNo + 1).IWTaxIn / 1.18)

                    'OOWのdenomi毎amount合計値をセット(tax込み)
                    lblOOWCardSum2.Text = clsSet.setINR2(setResultALL(showNo + 1).OOWCardTaxIn)
                    lblOOWCashSum2.Text = clsSet.setINR2(setResultALL(showNo + 1).OOWCashTaxIn)

                    'otherのdenomi毎amount合計値をセット（tax込み)
                    lblOtherCardSum2.Text = clsSet.setINR2(setResultALL(showNo + 1).otherCardTaxIn)
                    lblOtherCashSum2.Text = clsSet.setINR2(setResultALL(showNo + 1).otherCashTaxIn)

                    'OOWのdenomi毎amount合計値をセット(tax抜き)
                    lblOWCardNoTax2.Text = clsSet.setINR2(setResultALL(showNo + 1).OOWCardTaxIn / 1.18)
                    lblOWCashNoTax2.Text = clsSet.setINR2(setResultALL(showNo + 1).OOWCashTaxIn / 1.18)

                    'otherのdenomi毎amount合計値をセット（tax抜き)
                    lblOtCardNoTax2.Text = clsSet.setINR2(setResultALL(showNo + 1).otherCardTaxIn / 1.18)
                    lblOtCashNoTax2.Text = clsSet.setINR2(setResultALL(showNo + 1).otherCashTaxIn / 1.18)

                    '□□claim□□
                    'IWの合計値をセット
                    'taxin
                    lblIWClaim2.Text = clsSet.setINR2(setResultALL(showNo + 1).IWSum)

                    'notax
                    lblIWNoTaxCla2.Text = clsSet.setINR2(setResultALL(showNo + 1).IWSum / 1.18)

                    'OOWのdenomi毎amount合計値をセット(tax込み)
                    lblOWCardClaim2.Text = clsSet.setINR2(setResultALL(showNo + 1).OOWCardSum)
                    lblOWCashClaim2.Text = clsSet.setINR2(setResultALL(showNo + 1).OOWCashSum)

                    'otherのdenomi毎amount合計値をセット（tax込み)
                    lblOtCardClaim2.Text = clsSet.setINR2(setResultALL(showNo + 1).otherCardSum)
                    lblOtCashClaim2.Text = clsSet.setINR2(setResultALL(showNo + 1).otherCashSum)

                    'OOWのdenomi毎amount合計値をセット(tax抜き)
                    lblOWCardNoTaxCla2.Text = clsSet.setINR2(setResultALL(showNo + 1).OOWCardSum / 1.18)
                    lblOWCashNoTaxCla2.Text = clsSet.setINR2(setResultALL(showNo + 1).OOWCashSum / 1.18)

                    'otherのdenomi毎amount合計値をセット（tax抜き)
                    lblOtCardNoTaxCla2.Text = clsSet.setINR2(setResultALL(showNo + 1).otherCardSum / 1.18)
                    lblOtCashNoTaxCla2.Text = clsSet.setINR2(setResultALL(showNo + 1).otherCashSum / 1.18)

                    '■金種(IW,other(card),other(cash),out of warranty(card),out of warranty(cash))毎件数
                    'warranty,denomi毎件数値をセット
                    lblIWCnt2.Text = setResultALL(showNo + 1).IWCnt
                    lblOtherCardCnt2.Text = setResultALL(showNo + 1).otherCardCnt + setResultALL(showNo + 1).otherCashCreditCount
                    lblOtherCashCnt2.Text = setResultALL(showNo + 1).otherCashCnt + setResultALL(showNo + 1).otherCashCreditCount
                    lblOOWCardCnt2.Text = setResultALL(showNo + 1).OOWCardCnt + setResultALL(showNo + 1).OOWCashCreditCount
                    lblOOWCashCnt2.Text = setResultALL(showNo + 1).OOWCashCnt + setResultALL(showNo + 1).OOWCashCreditCount

                    '■cash total(open dposit + sales(cash))
                    '※拠点毎の最新レジチェック時のtotal金額の合計
                    lblCashTotal2.Text = clsSet.setINR2(setResultALL(showNo + 1).cashTotal) & "INR"

                    '■sales(sales(cash) + sales(card)+IW+other+GST)

                    tmp2 = 0

                    If lblIWClaim2.Text <> "" Then
                        tmp2 = tmp2 + Convert.ToDecimal(lblIWClaim2.Text)
                    End If

                    If lblOWCardClaim2.Text <> "" Then
                        tmp2 = tmp2 + Convert.ToDecimal(lblOWCardClaim2.Text)
                    End If

                    If lblOWCashClaim2.Text <> "" Then
                        tmp2 = tmp2 + Convert.ToDecimal(lblOWCashClaim2.Text)
                    End If

                    If lblOtCardClaim2.Text <> "" Then
                        tmp2 = tmp2 + Convert.ToDecimal(lblOtCardClaim2.Text)
                    End If

                    If lblOtCashClaim2.Text <> "" Then
                        tmp2 = tmp2 + Convert.ToDecimal(lblOtCashClaim2.Text)
                    End If

                    lblSales2.Text = clsSet.setINR(tmp2.ToString) & "INR"

                    '■お客様の製品と金銭のお預かりの集計
                    lblDepositCusto2.Text = clsSet.setINR2(setResultALL(showNo + 1).customerTotalCash) & "INR"
                    lblNumCusto2.Text = setResultALL(showNo + 1).CustomerTotalNumber

                    '■bank deposit
                    '※cash total - deposit
                    lblDeposit2.Text = clsSet.setINR2(setResultALL(showNo + 1).cashTotal - setResultALL(showNo + 1).deposit) & "INR"

                ElseIf setResultALL(i).dataNo = showNo + 3 Then
                    '３個目表

                    '拠点コード
                    lblLocation3.Text = setResultALL(showNo + 2).shipCode

                    'IWの合計値をセット
                    'taxin
                    lblIWSum3.Text = clsSet.setINR2(setResultALL(showNo + 2).IWTaxIn)

                    'notax
                    lblIWNoTax3.Text = clsSet.setINR2(setResultALL(showNo + 2).IWTaxIn / 1.18)

                    'OOWのdenomi毎amount合計値をセット(tax込み)
                    lblOOWCardSum3.Text = clsSet.setINR2(setResultALL(showNo + 2).OOWCardTaxIn)
                    lblOOWCashSum3.Text = clsSet.setINR2(setResultALL(showNo + 2).OOWCashTaxIn)

                    'otherのdenomi毎amount合計値をセット（tax込み)
                    lblOtherCardSum3.Text = clsSet.setINR2(setResultALL(showNo + 2).otherCardTaxIn)
                    lblOtherCashSum3.Text = clsSet.setINR2(setResultALL(showNo + 2).otherCashTaxIn)

                    'OOWのdenomi毎amount合計値をセット(tax抜き)
                    lblOWCardNoTax3.Text = clsSet.setINR2(setResultALL(showNo + 2).OOWCardTaxIn / 1.18)
                    lblOWCashNoTax3.Text = clsSet.setINR2(setResultALL(showNo + 2).OOWCashTaxIn / 1.18)

                    'otherのdenomi毎amount合計値をセット（tax抜き)
                    lblOtCardNoTax3.Text = clsSet.setINR2(setResultALL(showNo + 2).otherCardTaxIn / 1.18)
                    lblOtCashNoTax3.Text = clsSet.setINR2(setResultALL(showNo + 2).otherCashTaxIn / 1.18)

                    '□□claim□□
                    'IWの合計値をセット
                    'taxin
                    lblIWClaim3.Text = clsSet.setINR2(setResultALL(showNo + 2).IWSum)

                    'notax
                    lblIWNoTaxCla3.Text = clsSet.setINR2(setResultALL(showNo + 2).IWSum / 1.18)

                    'OOWのdenomi毎amount合計値をセット(tax込み)
                    lblOWCardClaim3.Text = clsSet.setINR2(setResultALL(showNo + 2).OOWCardSum)
                    lblOWCashClaim3.Text = clsSet.setINR2(setResultALL(showNo + 2).OOWCashSum)

                    'otherのdenomi毎amount合計値をセット（tax込み)
                    lblOtCardClaim3.Text = clsSet.setINR2(setResultALL(showNo + 2).otherCardSum)
                    lblOtCashClaim3.Text = clsSet.setINR2(setResultALL(showNo + 2).otherCashSum)

                    'OOWのdenomi毎amount合計値をセット(tax抜き)
                    lblOWCardNoTaxCla3.Text = clsSet.setINR2(setResultALL(showNo + 2).OOWCardSum / 1.18)
                    lblOWCashNoTaxCla3.Text = clsSet.setINR2(setResultALL(showNo + 2).OOWCashSum / 1.18)

                    'otherのdenomi毎amount合計値をセット（tax抜き)
                    lblOtCardNoTaxCla3.Text = clsSet.setINR2(setResultALL(showNo + 2).otherCardSum / 1.18)
                    lblOtCashNoTaxCla3.Text = clsSet.setINR2(setResultALL(showNo + 2).otherCashSum / 1.18)

                    '■金種(IW,other(card),other(cash),out of warranty(card),out of warranty(cash))毎件数
                    'warranty,denomi毎件数値をセット
                    lblIWCnt3.Text = setResultALL(showNo + 2).IWCnt
                    lblOtherCardCnt3.Text = setResultALL(showNo + 2).otherCardCnt + setResultALL(showNo + 2).otherCashCreditCount
                    lblOtherCashCnt3.Text = setResultALL(showNo + 2).otherCashCnt + setResultALL(showNo + 2).otherCashCreditCount
                    lblOOWCardCnt3.Text = setResultALL(showNo + 2).OOWCardCnt + setResultALL(showNo + 2).OOWCashCreditCount
                    lblOOWCashCnt3.Text = setResultALL(showNo + 2).OOWCashCnt + setResultALL(showNo + 2).OOWCashCreditCount

                    '■cash total(open dposit + sales(cash))
                    '※拠点毎の最新レジチェック時のtotal金額の合計
                    lblCashTotal3.Text = clsSet.setINR2(setResultALL(showNo + 2).cashTotal) & "INR"

                    '■sales(sales(cash) + sales(card)+IW+other+GST)

                    tmp2 = 0

                    If lblIWClaim3.Text <> "" Then
                        tmp2 = tmp2 + Convert.ToDecimal(lblIWClaim3.Text)
                    End If

                    If lblOWCardClaim3.Text <> "" Then
                        tmp2 = tmp2 + Convert.ToDecimal(lblOWCardClaim3.Text)
                    End If

                    If lblOWCashClaim3.Text <> "" Then
                        tmp2 = tmp2 + Convert.ToDecimal(lblOWCashClaim3.Text)
                    End If

                    If lblOtCardClaim3.Text <> "" Then
                        tmp2 = tmp2 + Convert.ToDecimal(lblOtCardClaim3.Text)
                    End If

                    If lblOtCashClaim3.Text <> "" Then
                        tmp2 = tmp2 + Convert.ToDecimal(lblOtCashClaim3.Text)
                    End If

                    lblSales3.Text = clsSet.setINR(tmp2.ToString) & "INR"

                    '■お客様の製品と金銭のお預かりの集計
                    lblDepositCusto3.Text = clsSet.setINR2(setResultALL(showNo + 2).customerTotalCash) & "INR"
                    lblNumCusto3.Text = setResultALL(showNo + 2).CustomerTotalNumber

                    '■bank deposit
                    '※cash total - deposit
                    lblDeposit3.Text = clsSet.setINR2(setResultALL(showNo + 2).cashTotal - setResultALL(showNo + 2).deposit) & "INR"

                ElseIf setResultALL(i).dataNo = showNo + 4 Then
                    '４個目表

                    '拠点コード
                    lblLocation4.Text = setResultALL(showNo + 3).shipCode

                    'IWの合計値をセット
                    'taxin
                    lblIWSum4.Text = clsSet.setINR2(setResultALL(showNo + 3).IWTaxIn)

                    'notax
                    lblIWNoTax4.Text = clsSet.setINR2(setResultALL(showNo + 3).IWTaxIn / 1.18)

                    'OOWのdenomi毎amount合計値をセット(tax込み)
                    lblOOWCardSum4.Text = clsSet.setINR2(setResultALL(showNo + 3).OOWCardTaxIn)
                    lblOOWCashSum4.Text = clsSet.setINR2(setResultALL(showNo + 3).OOWCashTaxIn)

                    'otherのdenomi毎amount合計値をセット（tax込み)
                    lblOtherCardSum4.Text = clsSet.setINR2(setResultALL(showNo + 3).otherCardTaxIn)
                    lblOtherCashSum4.Text = clsSet.setINR2(setResultALL(showNo + 3).otherCashTaxIn)

                    'OOWのdenomi毎amount合計値をセット(tax抜き)
                    lblOWCardNoTax4.Text = clsSet.setINR2(setResultALL(showNo + 3).OOWCardTaxIn / 1.18)
                    lblOWCashNoTax4.Text = clsSet.setINR2(setResultALL(showNo + 3).OOWCashTaxIn / 1.18)

                    'otherのdenomi毎amount合計値をセット（tax抜き)
                    lblOtCardNoTax4.Text = clsSet.setINR2(setResultALL(showNo + 3).otherCardTaxIn / 1.18)
                    lblOtCashNoTax4.Text = clsSet.setINR2(setResultALL(showNo + 3).otherCashTaxIn / 1.18)

                    '□□claim□□
                    'IWの合計値をセット
                    'taxin
                    lblIWClaim4.Text = clsSet.setINR2(setResultALL(showNo + 3).IWSum)

                    'notax
                    lblIWNoTaxCla4.Text = clsSet.setINR2(setResultALL(showNo + 3).IWSum / 1.18)

                    'OOWのdenomi毎amount合計値をセット(tax込み)
                    lblOWCardClaim4.Text = clsSet.setINR2(setResultALL(showNo + 3).OOWCardSum)
                    lblOWCashClaim4.Text = clsSet.setINR2(setResultALL(showNo + 3).OOWCashSum)

                    'otherのdenomi毎amount合計値をセット（tax込み)
                    lblOtCardClaim4.Text = clsSet.setINR2(setResultALL(showNo + 3).otherCardSum)
                    lblOtCashClaim4.Text = clsSet.setINR2(setResultALL(showNo + 3).otherCashSum)

                    'OOWのdenomi毎amount合計値をセット(tax抜き)
                    lblOWCardNoTaxCla4.Text = clsSet.setINR2(setResultALL(showNo + 3).OOWCardSum / 1.18)
                    lblOWCashNoTaxCla4.Text = clsSet.setINR2(setResultALL(showNo + 3).OOWCashSum / 1.18)

                    'otherのdenomi毎amount合計値をセット（tax抜き)
                    lblOtCardNoTaxCla4.Text = clsSet.setINR2(setResultALL(showNo + 3).otherCardSum / 1.18)
                    lblOtCashNoTaxCla4.Text = clsSet.setINR2(setResultALL(showNo + 3).otherCashSum / 1.18)

                    '■金種(IW,other(card),other(cash),out of warranty(card),out of warranty(cash))毎件数
                    'warranty,denomi毎件数値をセット
                    lblIWCnt4.Text = setResultALL(showNo + 3).IWCnt
                    lblOtherCardCnt4.Text = setResultALL(showNo + 3).otherCardCnt + setResultALL(showNo + 3).otherCashCreditCount
                    lblOtherCashCnt4.Text = setResultALL(showNo + 3).otherCashCnt + setResultALL(showNo + 3).otherCashCreditCount
                    lblOOWCardCnt4.Text = setResultALL(showNo + 3).OOWCardCnt + setResultALL(showNo + 3).OOWCashCreditCount
                    lblOOWCashCnt4.Text = setResultALL(showNo + 3).OOWCashCnt + setResultALL(showNo + 3).OOWCashCreditCount

                    '■cash total(open dposit + sales(cash))
                    '※拠点毎の最新レジチェック時のtotal金額の合計
                    lblCashTotal4.Text = clsSet.setINR2(setResultALL(showNo + 3).cashTotal) & "INR"

                    '■sales(sales(cash) + sales(card)+IW+other+GST)

                    tmp2 = 0

                    If lblIWClaim4.Text <> "" Then
                        tmp2 = tmp2 + Convert.ToDecimal(lblIWClaim4.Text)
                    End If

                    If lblOWCardClaim4.Text <> "" Then
                        tmp2 = tmp2 + Convert.ToDecimal(lblOWCardClaim4.Text)
                    End If

                    If lblOWCashClaim4.Text <> "" Then
                        tmp2 = tmp2 + Convert.ToDecimal(lblOWCashClaim4.Text)
                    End If

                    If lblOtCardClaim4.Text <> "" Then
                        tmp2 = tmp2 + Convert.ToDecimal(lblOtCardClaim4.Text)
                    End If

                    If lblOtCashClaim4.Text <> "" Then
                        tmp2 = tmp2 + Convert.ToDecimal(lblOtCashClaim4.Text)
                    End If

                    lblSales4.Text = clsSet.setINR(tmp2.ToString) & "INR"

                    '■お客様の製品と金銭のお預かりの集計
                    lblDepositCusto4.Text = clsSet.setINR2(setResultALL(showNo + 3).customerTotalCash) & "INR"
                    lblNumCusto4.Text = setResultALL(showNo + 3).CustomerTotalNumber

                    '■bank deposit
                    '※cash total - deposit
                    lblDeposit4.Text = clsSet.setINR2(setResultALL(showNo + 3).cashTotal - setResultALL(showNo + 3).deposit) & "INR"

                ElseIf setResultALL(i).dataNo = showNo + 5 Then
                    '５個目表

                    '拠点コード
                    lblLocation5.Text = setResultALL(showNo + 4).shipCode

                    'IWの合計値をセット
                    'taxin
                    lblIWSum5.Text = clsSet.setINR2(setResultALL(showNo + 4).IWTaxIn)

                    'notax
                    lblIWNoTax5.Text = clsSet.setINR2(setResultALL(showNo + 4).IWTaxIn / 1.18)

                    'OOWのdenomi毎amount合計値をセット(tax込み)
                    lblOOWCardSum5.Text = clsSet.setINR2(setResultALL(showNo + 4).OOWCardTaxIn)
                    lblOOWCashSum5.Text = clsSet.setINR2(setResultALL(showNo + 4).OOWCashTaxIn)

                    'otherのdenomi毎amount合計値をセット（tax込み)
                    lblOtherCardSum5.Text = clsSet.setINR2(setResultALL(showNo + 4).otherCardTaxIn)
                    lblOtherCashSum5.Text = clsSet.setINR2(setResultALL(showNo + 4).otherCashTaxIn)

                    'OOWのdenomi毎amount合計値をセット(tax抜き)
                    lblOWCardNoTax5.Text = clsSet.setINR2(setResultALL(showNo + 4).OOWCardTaxIn / 1.18)
                    lblOWCashNoTax5.Text = clsSet.setINR2(setResultALL(showNo + 4).OOWCashTaxIn / 1.18)

                    'otherのdenomi毎amount合計値をセット（tax抜き)
                    lblOtCardNoTax5.Text = clsSet.setINR2(setResultALL(showNo + 4).otherCardTaxIn / 1.18)
                    lblOtCashNoTax5.Text = clsSet.setINR2(setResultALL(showNo + 4).otherCashTaxIn / 1.18)

                    '□□claim□□
                    'IWの合計値をセット
                    'taxin
                    lblIWClaim5.Text = clsSet.setINR2(setResultALL(showNo + 4).IWSum)

                    'notax
                    lblIWNoTaxCla5.Text = clsSet.setINR2(setResultALL(showNo + 4).IWSum / 1.18)

                    'OOWのdenomi毎amount合計値をセット(tax込み)
                    lblOWCardClaim5.Text = clsSet.setINR2(setResultALL(showNo + 4).OOWCardSum)
                    lblOWCashClaim5.Text = clsSet.setINR2(setResultALL(showNo + 4).OOWCashSum)

                    'otherのdenomi毎amount合計値をセット（tax込み)
                    lblOtCardClaim5.Text = clsSet.setINR2(setResultALL(showNo + 4).otherCardSum)
                    lblOtCashClaim5.Text = clsSet.setINR2(setResultALL(showNo + 4).otherCashSum)

                    'OOWのdenomi毎amount合計値をセット(tax抜き)
                    lblOWCardNoTaxCla5.Text = clsSet.setINR2(setResultALL(showNo + 4).OOWCardSum / 1.18)
                    lblOWCashNoTaxCla5.Text = clsSet.setINR2(setResultALL(showNo + 4).OOWCashSum / 1.18)

                    'otherのdenomi毎amount合計値をセット（tax抜き)
                    lblOtCardNoTaxCla5.Text = clsSet.setINR2(setResultALL(showNo + 4).otherCardSum / 1.18)
                    lblOtCashNoTaxCla5.Text = clsSet.setINR2(setResultALL(showNo + 4).otherCashSum / 1.18)

                    '■金種(IW,other(card),other(cash),out of warranty(card),out of warranty(cash))毎件数
                    'warranty,denomi毎件数値をセット
                    lblIWCnt5.Text = setResultALL(showNo + 4).IWCnt
                    lblOtherCardCnt5.Text = setResultALL(showNo + 4).otherCardCnt + setResultALL(showNo + 4).otherCashCreditCount
                    lblOtherCashCnt5.Text = setResultALL(showNo + 4).otherCashCnt + setResultALL(showNo + 4).otherCashCreditCount
                    lblOOWCardCnt5.Text = setResultALL(showNo + 4).OOWCardCnt + setResultALL(showNo + 4).OOWCashCreditCount
                    lblOOWCashCnt5.Text = setResultALL(showNo + 4).OOWCashCnt + setResultALL(showNo + 4).OOWCashCreditCount

                    '■cash total(open dposit + sales(cash))
                    '※拠点毎の最新レジチェック時のtotal金額の合計
                    lblCashTotal5.Text = clsSet.setINR2(setResultALL(showNo + 4).cashTotal) & "INR"

                    '■sales(sales(cash) + sales(card)+IW+other+GST)

                    tmp2 = 0

                    If lblIWClaim5.Text <> "" Then
                        tmp2 = tmp2 + Convert.ToDecimal(lblIWClaim5.Text)
                    End If

                    If lblOWCardClaim5.Text <> "" Then
                        tmp2 = tmp2 + Convert.ToDecimal(lblOWCardClaim5.Text)
                    End If

                    If lblOWCashClaim5.Text <> "" Then
                        tmp2 = tmp2 + Convert.ToDecimal(lblOWCashClaim5.Text)
                    End If

                    If lblOtCardClaim5.Text <> "" Then
                        tmp2 = tmp2 + Convert.ToDecimal(lblOtCardClaim5.Text)
                    End If

                    If lblOtCashClaim5.Text <> "" Then
                        tmp2 = tmp2 + Convert.ToDecimal(lblOtCashClaim5.Text)
                    End If

                    lblSales5.Text = clsSet.setINR(tmp2.ToString) & "INR"

                    '■お客様の製品と金銭のお預かりの集計
                    lblDepositCusto5.Text = clsSet.setINR2(setResultALL(showNo + 4).customerTotalCash) & "INR"
                    lblNumCusto5.Text = setResultALL(showNo + 4).CustomerTotalNumber

                    '■bank deposit
                    '※cash total - deposit
                    lblDeposit5.Text = clsSet.setINR2(setResultALL(showNo + 4).cashTotal - setResultALL(showNo + 4).deposit) & "INR"

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

    Protected Sub btnBack_Click(sender As Object, e As ImageClickEventArgs) Handles btnBack.Click

        'セッション開放
        Session("page_NO") = Nothing
        Session("btnNext_Cnt") = Nothing
        Session("btnStart_Cnt") = Nothing
        Session("last_ShowNO") = Nothing
        Session("start_Date") = Nothing

        Response.Redirect("Money_Salse.aspx")

    End Sub

    Protected Sub btnNextPage_Click(sender As Object, e As EventArgs) Handles btnNextPage.Click

        Dim btnNextCnt As Integer = Session("btnNext_Cnt")

        btnNextCnt = btnNextCnt + 1

        Session("btnNext_Cnt") = btnNextCnt

        Response.Redirect("Money_Salse_show.aspx")

    End Sub

    Protected Sub BtnStartPage_Click(sender As Object, e As EventArgs) Handles BtnStartPage.Click

        Dim btnStartCnt As Integer = Session("btnStart_Cnt")

        btnStartCnt = btnStartCnt + 1

        Session("btnStart_Cnt") = btnStartCnt

        Response.Redirect("Money_Salse_show.aspx")

    End Sub

End Class