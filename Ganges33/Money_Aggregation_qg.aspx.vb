

Imports Ganges33.Ganges33.model
Imports Ganges33.Ganges33.logic

Public Class Money_Aggregation_qg
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

        If TextCompleteDateFrom.Text.Trim = "" Then
            Call showMsg("Provide the repair completed date")
            IntializeTextBox()
            Exit Sub
        End If

        '***セッション情報取得***
        Dim shipCode As String = Session("ship_code")

        'Check Money Agreegation Count
        Dim _MoneyAggregationModel As MoneyAggregationModel = New MoneyAggregationModel()
        Dim _MoneyAggregationControl As MoneyAggregationControl = New MoneyAggregationControl()
        _MoneyAggregationModel.InvoiceDate = TextCompleteDateFrom.Text
        _MoneyAggregationModel.Location = shipCode
        ' _MoneyAggregationModel.PaymentKind = 1

        Dim MoneyAggregationCheck As String = _MoneyAggregationControl.SelectMoneyAggregationCheck(_MoneyAggregationModel)
        'if No Datas found, then show the error message to the user
        If MoneyAggregationCheck = "0" Then
            Call showMsg("There is no transaction found on " + TextCompleteDateFrom.Text)
            lblCount.Text = "" ' 
            Exit Sub
        Else
            lblCount.Text = MoneyAggregationCheck
        End If
        'To Aggregate process
        Dim lstMoneyAggregationModel As List(Of MoneyAggregationModel) = New List(Of MoneyAggregationModel)()
        lstMoneyAggregationModel = _MoneyAggregationControl.SelectMoneyAggregation(_MoneyAggregationModel)
        If lstMoneyAggregationModel IsNot Nothing AndAlso lstMoneyAggregationModel.Count <> 0 Then
            '#Sales =>sales(cash) + sales(card)+IW+other+GST 
            If String.IsNullOrEmpty(lstMoneyAggregationModel(0).Sales) Then
                lstMoneyAggregationModel(0).Sales = 0.00
            End If
            'Need to Add Sales + Customer Deposit from other Table, So Added end of the coding
            '''''''''''lblSales.Text = lstMoneyAggregationModel(0).Sales.ToString()
            '#Discount Number 
            '#Discount Amount
            If String.IsNullOrEmpty(lstMoneyAggregationModel(0).DiscountAmt) Then
                lstMoneyAggregationModel(0).DiscountAmt = 0.00
                lstMoneyAggregationModel(0).DiscountCnt = 0
            End If
            lblDisAmount.Text = CommonControl.setINR(lstMoneyAggregationModel(0).DiscountAmt.ToString()) 'CommonControl.setINR(lstMoneyAggregationModel(0).DiscountAmt.ToString()) & "INR"
            lblDisNum.Text = lstMoneyAggregationModel(0).DiscountCnt.ToString

            'Inward Amount
            If String.IsNullOrEmpty(lstMoneyAggregationModel(0).InWardAmt) Then
                lstMoneyAggregationModel(0).InWardAmt = 0.00
                lstMoneyAggregationModel(0).InWardCnt = 0
            End If
            lblIWSum.Text = CommonControl.setINR(lstMoneyAggregationModel(0).InWardAmt.ToString()) 'CommonControl.setINR(lstMoneyAggregationModel(0).DiscountAmt.ToString()) & "INR"
            lblIWCnt.Text = lstMoneyAggregationModel(0).InWardCnt.ToString()

            'Outward Cash Amount
            If String.IsNullOrEmpty(lstMoneyAggregationModel(0).OutWardCashAmt) Then
                lstMoneyAggregationModel(0).OutWardCashAmt = 0.00
                lstMoneyAggregationModel(0).OutWardCashCnt = 0
            End If
            lblOOWCashSum.Text = CommonControl.setINR(lstMoneyAggregationModel(0).OutWardCashAmt.ToString())
            lblOOWCashCnt.Text = lstMoneyAggregationModel(0).OutWardCashCnt.ToString()

            'Outward Card Amount
            If String.IsNullOrEmpty(lstMoneyAggregationModel(0).OutWardCardAmt) Then
                lstMoneyAggregationModel(0).OutWardCardAmt = 0.00
                lstMoneyAggregationModel(0).OutWardCardCnt = 0
            End If
            lblOOWCardSum.Text = CommonControl.setINR(lstMoneyAggregationModel(0).OutWardCardAmt.ToString())
            lblOOWCardCnt.Text = lstMoneyAggregationModel(0).OutWardCardCnt.ToString()

            'Other Cash Amount
            If String.IsNullOrEmpty(lstMoneyAggregationModel(0).OtherCashAmt) Then
                lstMoneyAggregationModel(0).OtherCashAmt = 0.00
                lstMoneyAggregationModel(0).OtherCashCnt = 0
            End If
            lblOtherCashSum.Text = CommonControl.setINR(lstMoneyAggregationModel(0).OtherCashAmt.ToString())
            lblOtherCashCnt.Text = lstMoneyAggregationModel(0).OtherCashCnt.ToString()

            'Other Card Amount
            If String.IsNullOrEmpty(lstMoneyAggregationModel(0).OtherCardAmt) Then
                lstMoneyAggregationModel(0).OtherCardAmt = 0.00
                lstMoneyAggregationModel(0).OtherCardCnt = 0
            End If
            lblOtherCardSum.Text = CommonControl.setINR(lstMoneyAggregationModel(0).OtherCardAmt.ToString())
            lblOtherCardCnt.Text = lstMoneyAggregationModel(0).OtherCardCnt.ToString()

            'Daily Openening Cash / Advance for cash counter by GSS
            'Find Opening Balance in the counter of starting of the day. 
            '#Open Cash 
            _MoneyAggregationModel.SearchDate = TextCompleteDateFrom.Text
            Dim dtOpeningBal As DataTable = _MoneyAggregationControl.SelectOpeningBalance(_MoneyAggregationModel)
            If (dtOpeningBal Is Nothing) Or (dtOpeningBal.Rows.Count = 0) Then
                lstMoneyAggregationModel(0).OpeningCash = 0.00
            Else
                If String.IsNullOrEmpty(dtOpeningBal.Rows(0)("total")) Then
                    lstMoneyAggregationModel(0).OpeningCash = 0.00
                Else
                    lstMoneyAggregationModel(0).OpeningCash = dtOpeningBal.Rows(0)("total")
                End If
            End If
            lblReserve.Text = CommonControl.setINR(lstMoneyAggregationModel(0).OpeningCash)
            'Find Customer Advance / Advance amount by Customer
            '#Customer Advance 
            Dim dtCustAdvance As DataTable = _MoneyAggregationControl.SelectCustomerAdvance(_MoneyAggregationModel)
            If dtCustAdvance.Rows(0)("count") = 0 Then ' if count Zero then Advance amount will be zero
                lstMoneyAggregationModel(0).AdvanceAmt = 0.00
                lstMoneyAggregationModel(0).AdvanceCnt = 0
            Else
                lstMoneyAggregationModel(0).AdvanceAmt = dtCustAdvance.Rows(0)("AdvanceAmt")
                lstMoneyAggregationModel(0).AdvanceCnt = dtCustAdvance.Rows(0)("count")
            End If
            lblDepositCusto.Text = CommonControl.setINR(lstMoneyAggregationModel(0).AdvanceAmt)
            '#Number of Advance
            lblNumCusto.Text = lstMoneyAggregationModel(0).AdvanceCnt ' No of Customer paid for advance

            'Actually Cash collected from the customer without discount
            '#Deposit
            If String.IsNullOrEmpty(lstMoneyAggregationModel(0).BankDeposit) Then
                lstMoneyAggregationModel(0).BankDeposit = 0.00
            End If
            lblDeposit.Text = CommonControl.setINR(lstMoneyAggregationModel(0).BankDeposit.ToString())

            'Inward Claim Amount 
            If String.IsNullOrEmpty(lstMoneyAggregationModel(0).InWardClaimAmt) Then
                lstMoneyAggregationModel(0).InWardClaimAmt = 0.00
            End If
            lblIWSumClaim.Text = CommonControl.setINR(lstMoneyAggregationModel(0).InWardClaimAmt.ToString())
            'Outward Cash Claim amount
            If String.IsNullOrEmpty(lstMoneyAggregationModel(0).OutWardCashClaimAmt) Then
                lstMoneyAggregationModel(0).OutWardCashClaimAmt = 0.00
            End If
            lblOOWCashSumClaim.Text = CommonControl.setINR(lstMoneyAggregationModel(0).OutWardCashClaimAmt.ToString())
            'Outward Card Claim amount
            If String.IsNullOrEmpty(lstMoneyAggregationModel(0).OutWardCardClaimAmt) Then
                lstMoneyAggregationModel(0).OutWardCardClaimAmt = 0.00
            End If
            lblOOWCardSumClaim.Text = CommonControl.setINR(lstMoneyAggregationModel(0).OutWardCardClaimAmt.ToString())
            'Other Cash Claim amount
            If String.IsNullOrEmpty(lstMoneyAggregationModel(0).OtherCashClaimAmt) Then
                lstMoneyAggregationModel(0).OtherCashClaimAmt = 0.00
            End If
            lblOtherCashSumClaim.Text = CommonControl.setINR(lstMoneyAggregationModel(0).OtherCashClaimAmt.ToString())
            'Other Card Claim amount
            If String.IsNullOrEmpty(lstMoneyAggregationModel(0).OtherCardClaimAmt) Then
                lstMoneyAggregationModel(0).OtherCardClaimAmt = 0.00
            End If
            lblOtherCardSumClaim.Text = CommonControl.setINR(lstMoneyAggregationModel(0).OtherCardClaimAmt.ToString())

        End If
        'Add Sales + Customer Deposit for the sales 
        lblSales.Text = CommonControl.setINR(lstMoneyAggregationModel(0).Sales)

        'Cash Total = Opening Cash in Hand + Cash collected either IW/OOW/OTHER 
        '#cash total =>open deposit + sales(cash)
        'Deduct
        lblCashTotal.Text = CommonControl.setINR((lstMoneyAggregationModel(0).OpeningCash + lstMoneyAggregationModel(0).OutWardCashAmt + lstMoneyAggregationModel(0).OtherCashAmt) - (lstMoneyAggregationModel(0).DiscountAmtCash + lstMoneyAggregationModel(0).BankDeposit))

    End Sub

    Protected Sub btnToday_Click(sender As Object, e As EventArgs) Handles btnToday.Click

        Dim clsSetCommon As New Class_common
        Dim dtNow As DateTime = clsSetCommon.dtIndia

        TextCompleteDateFrom.Text = dtNow.ToShortDateString

    End Sub

    Protected Sub IntializeTextBox()

        lblIWSum.Text = ""
        lblOOWCardSum.Text = ""
        lblOOWCashSum.Text = ""
        lblOtherCardSum.Text = ""
        lblOtherCashSum.Text = ""

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