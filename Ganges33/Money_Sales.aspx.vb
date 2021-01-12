Imports Ganges33.Ganges33.model
Imports Ganges33.Ganges33.logic

Public Class Money_Sales
    Inherits System.Web.UI.Page
    '一覧の行数
    Private Const ROWCOUNT As Integer = 5
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            InitDropDownList()
            divHead.Visible = False
            divDetails.Visible = False
        End If
    End Sub


    Protected Sub btnSearch_Click(sender As Object, e As ImageClickEventArgs) Handles btnSearch.Click
        Log4NetControl.ComInfoLogWrite(Session("UserName"))



        ''***セッション情報取得***
        Dim shipCode As String = Session("ship_code")

        If DropListLocation.SelectedItem.Value = "Select Branch" Then
            Call showMsg("Select the branch from the list")
            Exit Sub
        ElseIf DropListLocation.SelectedItem.Value = "ALL" Then
            '0 check entered date or not
            If TextCompleteDateFrom.Text.Trim = "" Then
                Call showMsg("Provide the repair completed date")
                Exit Sub
            End If

            '1st Load all centers aggregation reports
            MoneySalesAllAggregation(TextCompleteDateFrom.Text, "")
            divHead.Visible = True
            divDetails.Visible = True
            GridViewBind(TextCompleteDateFrom.Text)
        Else
            '0 check entered date or not
            If TextCompleteDateFrom.Text.Trim = "" Then
                Call showMsg("Provide the repair completed date")
                Exit Sub
            End If
            'Individual Service center
            divHead.Visible = True
            divDetails.Visible = False
            MoneySalesAllAggregation(TextCompleteDateFrom.Text, DropListLocation.SelectedItem.Value)
        End If
    End Sub

    Protected Sub grvSalesReport_PageIndexChanging(ByVal sender As Object, ByVal e As GridViewPageEventArgs)
        Log4NetControl.ComInfoLogWrite(Session("UserName"))
        grvSalesReport.PageIndex = e.NewPageIndex
        GridViewBind(TextCompleteDateFrom.Text)
    End Sub


    Protected Sub GridViewBind(ByVal Optional SearchDate As String = "")
        Dim codeMasterControl As CodeMasterControl = New CodeMasterControl()
        Dim dtBranchAll As List(Of CodeMasterModel) = codeMasterControl.SelectBranchMaster()
        If dtBranchAll Is Nothing Then
            ViewState("dtBranchAllCount") = 0
        Else
            ViewState("dtBranchAllCount") = dtBranchAll.Count
        End If
        grvSalesReport.DataSource = dtBranchAll
        ViewState("dtBranchAll") = dtBranchAll
        grvSalesReport.DataBind()
        Dim intRecordIndex As Integer = grvSalesReport.Rows.Count * grvSalesReport.PageIndex
        For i As Integer = 0 To (If(CInt(ViewState("dtBranchAllCount")) > (grvSalesReport.Rows.Count * (grvSalesReport.PageIndex + 1)), grvSalesReport.Rows.Count, CInt(ViewState("dtBranchAllCount")) - intRecordIndex)) - 1
            Dim lblBranchCode As Label = CType(grvSalesReport.Rows(i).FindControl("lblBranchCode"), Label)
            Dim lblBranchName As Label = CType(grvSalesReport.Rows(i).FindControl("lblBranchName"), Label)

            Dim lblIWCnt1 As Label = CType(grvSalesReport.Rows(i).FindControl("lblIWCnt1"), Label)
            Dim lblOOWCashCnt1 As Label = CType(grvSalesReport.Rows(i).FindControl("lblOOWCashCnt1"), Label)
            Dim lblOOWCardCnt1 As Label = CType(grvSalesReport.Rows(i).FindControl("lblOOWCardCnt1"), Label)
            Dim lblOtherCashCnt1 As Label = CType(grvSalesReport.Rows(i).FindControl("lblOtherCashCnt1"), Label)
            Dim lblOtherCardCnt1 As Label = CType(grvSalesReport.Rows(i).FindControl("lblOtherCardCnt1"), Label)
            Dim lblIWNoTax1 As Label = CType(grvSalesReport.Rows(i).FindControl("lblIWNoTax1"), Label)
            Dim lblOWCashNoTax1 As Label = CType(grvSalesReport.Rows(i).FindControl("lblOWCashNoTax1"), Label)
            Dim lblOWCardNoTax1 As Label = CType(grvSalesReport.Rows(i).FindControl("lblOWCardNoTax1"), Label)
            Dim lblOtCashNoTax1 As Label = CType(grvSalesReport.Rows(i).FindControl("lblOtCashNoTax1"), Label)
            Dim lblOtCardNoTax1 As Label = CType(grvSalesReport.Rows(i).FindControl("lblOtCardNoTax1"), Label)
            Dim lblIWSum1 As Label = CType(grvSalesReport.Rows(i).FindControl("lblIWSum1"), Label)
            Dim lblOOWCashSum1 As Label = CType(grvSalesReport.Rows(i).FindControl("lblOOWCashSum1"), Label)
            Dim lblOOWCardSum1 As Label = CType(grvSalesReport.Rows(i).FindControl("lblOOWCardSum1"), Label)
            Dim lblOtherCashSum1 As Label = CType(grvSalesReport.Rows(i).FindControl("lblOtherCashSum1"), Label)
            Dim lblOtherCardSum1 As Label = CType(grvSalesReport.Rows(i).FindControl("lblOtherCardSum1"), Label)
            Dim lblIWNoTaxClaim1 As Label = CType(grvSalesReport.Rows(i).FindControl("lblIWNoTaxClaim1"), Label)
            Dim lblOWCashNoTaxClaim1 As Label = CType(grvSalesReport.Rows(i).FindControl("lblOWCashNoTaxClaim1"), Label)
            Dim lblOWCardNoTaxClaim1 As Label = CType(grvSalesReport.Rows(i).FindControl("lblOWCardNoTaxClaim1"), Label)
            Dim lblOtCashNoTaxClaim1 As Label = CType(grvSalesReport.Rows(i).FindControl("lblOtCashNoTaxClaim1"), Label)
            Dim lblOtCardNoTaxClaim1 As Label = CType(grvSalesReport.Rows(i).FindControl("lblOtCardNoTaxClaim1"), Label)
            Dim lblIWSumClaim1 As Label = CType(grvSalesReport.Rows(i).FindControl("lblIWSumClaim1"), Label)
            Dim lblOOWCashSumClaim1 As Label = CType(grvSalesReport.Rows(i).FindControl("lblOOWCashSumClaim1"), Label)
            Dim lblOOWCardSumClaim1 As Label = CType(grvSalesReport.Rows(i).FindControl("lblOOWCardSumClaim1"), Label)
            Dim lblOtherCashSumClaim1 As Label = CType(grvSalesReport.Rows(i).FindControl("lblOtherCashSumClaim1"), Label)
            Dim lblOtherCardSumClaim1 As Label = CType(grvSalesReport.Rows(i).FindControl("lblOtherCardSumClaim1"), Label)
            Dim lblCashTotal1 As Label = CType(grvSalesReport.Rows(i).FindControl("lblCashTotal1"), Label)
            Dim lblSales1 As Label = CType(grvSalesReport.Rows(i).FindControl("lblSales1"), Label)
            Dim lblDepositCusto1 As Label = CType(grvSalesReport.Rows(i).FindControl("lblDepositCusto1"), Label)
            Dim lblNumCusto1 As Label = CType(grvSalesReport.Rows(i).FindControl("lblNumCusto1"), Label)
            Dim lblDeposit1 As Label = CType(grvSalesReport.Rows(i).FindControl("lblDeposit1"), Label)
            Dim lblCardTotal1 As Label = CType(grvSalesReport.Rows(i).FindControl("lblCardTotal1"), Label)


            If lblBranchName.Text <> "" Then

                'Check Money Agreegation Count
                Dim _MoneyAggregationModel As MoneyAggregationModel = New MoneyAggregationModel()
                Dim _MoneyAggregationControl As MoneyAggregationControl = New MoneyAggregationControl()
                _MoneyAggregationModel.InvoiceDate = TextCompleteDateFrom.Text
                _MoneyAggregationModel.Location = lblBranchCode.Text
                '_MoneyAggregationModel.PaymentKind = 1

                Dim MoneyAggregationCheck As String = _MoneyAggregationControl.SelectMoneyAggregationCheck(_MoneyAggregationModel)
                'if No Datas found, then show the error message to the user
                If MoneyAggregationCheck = "0" Then
                    lblIWCnt1.Text = "0.00"
                    lblOOWCashCnt1.Text = "0.00"
                    lblOOWCardCnt1.Text = "0.00"
                    lblOtherCashCnt1.Text = "0.00"
                    lblOtherCardCnt1.Text = "0.00"
                    lblIWNoTax1.Text = "0.00"
                    lblOWCashNoTax1.Text = "0.00"
                    lblOWCardNoTax1.Text = "0.00"
                    lblOtCashNoTax1.Text = "0.00"
                    lblOtCardNoTax1.Text = "0.00"
                    lblIWSum1.Text = "0.00"
                    lblOOWCashSum1.Text = "0.00"
                    lblOOWCardSum1.Text = "0.00"
                    lblOtherCashSum1.Text = "0.00"
                    lblOtherCardSum1.Text = "0.00"
                    lblIWNoTaxClaim1.Text = "0.00"
                    lblOWCashNoTaxClaim1.Text = "0.00"
                    lblOWCardNoTaxClaim1.Text = "0.00"
                    lblOtCashNoTaxClaim1.Text = "0.00"
                    lblOtCardNoTaxClaim1.Text = "0.00"
                    lblIWSumClaim1.Text = "0.00"
                    lblOOWCashSumClaim1.Text = "0.00"
                    lblOOWCardSumClaim1.Text = "0.00"
                    lblOtherCashSumClaim1.Text = "0.00"
                    lblOtherCardSumClaim1.Text = "0.00"
                    lblCashTotal1.Text = "0.00"
                    lblSales1.Text = "0.00"
                    lblDepositCusto1.Text = "0.00"
                    lblNumCusto1.Text = "0.00"
                    lblDeposit1.Text = "0.00"
                    'Call showMsg("There is no records found")
                    'Exit Sub
                Else


                    'To Aggregate process
                    Dim lstMoneyAggregationModel As List(Of MoneyAggregationModel) = New List(Of MoneyAggregationModel)()
                    lstMoneyAggregationModel = _MoneyAggregationControl.SelectMoneyAggregation(_MoneyAggregationModel)
                    If lstMoneyAggregationModel IsNot Nothing AndAlso lstMoneyAggregationModel.Count <> 0 Then
                        '#Sales =>sales(cash) + sales(card)+IW+other+GST 
                        If String.IsNullOrEmpty(lstMoneyAggregationModel(0).Sales) Then
                            lstMoneyAggregationModel(0).Sales = 0.00
                        End If
                        'Need to Add Sales + Customer Deposit from other Table, So Added end of the coding
                        ''''''''''''lblSales1.Text = CommonControl.setINR(lstMoneyAggregationModel(0).Sales)

                        'Inward Amount
                        If String.IsNullOrEmpty(lstMoneyAggregationModel(0).InWardAmt) Then
                            lstMoneyAggregationModel(0).InWardAmt = 0.00
                            lstMoneyAggregationModel(0).InWardCnt = 0
                        End If
                        lblIWSum1.Text = CommonControl.setINR(lstMoneyAggregationModel(0).InWardAmt) 'CommonControl.setINR(lstMoneyAggregationModel(0).DiscountAmt.ToString()) & "INR"
                        lblIWNoTax1.Text = CommonControl.setINR(lstMoneyAggregationModel(0).InWardAmt / 1.18)
                        lblIWCnt1.Text = lstMoneyAggregationModel(0).InWardCnt.ToString()

                        'Outward Cash Amount
                        If String.IsNullOrEmpty(lstMoneyAggregationModel(0).OutWardCashAmt) Then
                            lstMoneyAggregationModel(0).OutWardCashAmt = 0.00
                            lstMoneyAggregationModel(0).OutWardCashCnt = 0
                        End If
                        lblOOWCashSum1.Text = CommonControl.setINR(lstMoneyAggregationModel(0).OutWardCashAmt)
                        lblOWCashNoTax1.Text = CommonControl.setINR(lstMoneyAggregationModel(0).OutWardCashAmt / 1.18)
                        lblOOWCashCnt1.Text = lstMoneyAggregationModel(0).OutWardCashCnt.ToString()

                        'Outward Card Amount
                        If String.IsNullOrEmpty(lstMoneyAggregationModel(0).OutWardCardAmt) Then
                            lstMoneyAggregationModel(0).OutWardCardAmt = 0.00
                            lstMoneyAggregationModel(0).OutWardCardCnt = 0
                        End If
                        lblOOWCardSum1.Text = CommonControl.setINR(lstMoneyAggregationModel(0).OutWardCardAmt)
                        lblOWCardNoTax1.Text = CommonControl.setINR(lstMoneyAggregationModel(0).OutWardCardAmt / 1.18)
                        lblOOWCardCnt1.Text = lstMoneyAggregationModel(0).OutWardCardCnt.ToString()

                        'Other Cash Amount
                        If String.IsNullOrEmpty(lstMoneyAggregationModel(0).OtherCashAmt) Then
                            lstMoneyAggregationModel(0).OtherCashAmt = 0.00
                            lstMoneyAggregationModel(0).OtherCashCnt = 0
                        End If
                        lblOtherCashSum1.Text = CommonControl.setINR(lstMoneyAggregationModel(0).OtherCashAmt)
                        lblOtCashNoTax1.Text = CommonControl.setINR(lstMoneyAggregationModel(0).OtherCashAmt / 1.18)
                        lblOtherCashCnt1.Text = lstMoneyAggregationModel(0).OtherCashCnt.ToString()

                        'Other Card Amount
                        If String.IsNullOrEmpty(lstMoneyAggregationModel(0).OtherCardAmt) Then
                            lstMoneyAggregationModel(0).OtherCardAmt = 0.00
                            lstMoneyAggregationModel(0).OtherCardCnt = 0
                        End If
                        lblOtherCardSum1.Text = CommonControl.setINR(lstMoneyAggregationModel(0).OtherCardAmt)
                        lblOtCardNoTax1.Text = CommonControl.setINR(lstMoneyAggregationModel(0).OtherCardAmt / 1.18)
                        lblOtherCardCnt1.Text = lstMoneyAggregationModel(0).OtherCardCnt.ToString()

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
                        '''''''''''''''''''''''lblReserve.Text = lstMoneyAggregationModel(0).OpeningCash
                        'Find Customer Advance / Advance amount by Customer
                        '#Customer Advance / Custody
                        Dim dtCustAdvance As DataTable = _MoneyAggregationControl.SelectCustomerAdvance(_MoneyAggregationModel)
                        If dtCustAdvance.Rows(0)("count") = 0 Then ' if count Zero then Advance amount will be zero
                            lstMoneyAggregationModel(0).AdvanceAmt = 0.00
                            lstMoneyAggregationModel(0).AdvanceCnt = 0
                        Else
                            lstMoneyAggregationModel(0).AdvanceAmt = dtCustAdvance.Rows(0)("AdvanceAmt")
                            lstMoneyAggregationModel(0).AdvanceCnt = dtCustAdvance.Rows(0)("count")
                        End If
                        '#Number of Advance
                        lblDepositCusto1.Text = CommonControl.setINR(lstMoneyAggregationModel(0).AdvanceAmt)
                        lblNumCusto1.Text = lstMoneyAggregationModel(0).AdvanceCnt ' No of Customer paid for advance

                        'Actually Cash collected from the customer without discount
                        '#Bank Deposit
                        If String.IsNullOrEmpty(lstMoneyAggregationModel(0).BankDeposit) Then
                            lstMoneyAggregationModel(0).BankDeposit = 0.00
                        End If
                        lblDeposit1.Text = CommonControl.setINR(lstMoneyAggregationModel(0).BankDeposit)

                        'Inward Claim Amount 
                        If String.IsNullOrEmpty(lstMoneyAggregationModel(0).InWardClaimAmt) Then
                            lstMoneyAggregationModel(0).InWardClaimAmt = 0.00
                        End If
                        lblIWSumClaim1.Text = CommonControl.setINR(lstMoneyAggregationModel(0).InWardClaimAmt)
                        lblIWNoTaxClaim1.Text = CommonControl.setINR(lstMoneyAggregationModel(0).InWardClaimAmt / 1.18)

                        'Outward Cash Claim amount
                        If String.IsNullOrEmpty(lstMoneyAggregationModel(0).OutWardCashClaimAmt) Then
                            lstMoneyAggregationModel(0).OutWardCashClaimAmt = 0.00
                        End If
                        lblOOWCashSumClaim1.Text = CommonControl.setINR(lstMoneyAggregationModel(0).OutWardCashClaimAmt)
                        lblOWCashNoTaxClaim1.Text = CommonControl.setINR(lstMoneyAggregationModel(0).OutWardCashClaimAmt / 1.18)

                        'Outward Card Claim amount
                        If String.IsNullOrEmpty(lstMoneyAggregationModel(0).OutWardCardClaimAmt) Then
                            lstMoneyAggregationModel(0).OutWardCardClaimAmt = 0.00
                        End If
                        lblOOWCardSumClaim1.Text = CommonControl.setINR(lstMoneyAggregationModel(0).OutWardCardClaimAmt)
                        lblOWCardNoTaxClaim1.Text = CommonControl.setINR(lstMoneyAggregationModel(0).OutWardCardClaimAmt / 1.18)

                        'Other Cash Claim amount
                        If String.IsNullOrEmpty(lstMoneyAggregationModel(0).OtherCashClaimAmt) Then
                            lstMoneyAggregationModel(0).OtherCashClaimAmt = 0.00
                        End If
                        lblOtherCashSumClaim1.Text = CommonControl.setINR(lstMoneyAggregationModel(0).OtherCashClaimAmt)
                        lblOtCashNoTaxClaim1.Text = CommonControl.setINR(lstMoneyAggregationModel(0).OtherCashClaimAmt / 1.18)

                        'Other Card Claim amount
                        If String.IsNullOrEmpty(lstMoneyAggregationModel(0).OtherCardClaimAmt) Then
                            lstMoneyAggregationModel(0).OtherCardClaimAmt = 0.00
                        End If
                        lblOtherCardSumClaim1.Text = CommonControl.setINR(lstMoneyAggregationModel(0).OtherCardClaimAmt)
                        lblOtCardNoTaxClaim1.Text = CommonControl.setINR(lstMoneyAggregationModel(0).OtherCardClaimAmt / 1.18)
                    End If
                    'Add Sales + Customer Deposit for the sales 
                    lblSales1.Text = CommonControl.setINR(lstMoneyAggregationModel(0).Sales)
                    'Cash Total = Opening Cash in Hand + Cash collected either IW/OOW/OTHER 
                    '#cash total =>open deposit + sales(cash) => lstMoneyAggregationModel(0).OpeningCash +
                    lblCashTotal1.Text = CommonControl.setINR((lstMoneyAggregationModel(0).OpeningCash + lstMoneyAggregationModel(0).OutWardCashAmt + lstMoneyAggregationModel(0).OtherCashAmt) - (lstMoneyAggregationModel(0).DiscountAmtCash + lstMoneyAggregationModel(0).BankDeposit))
                    'Card Total Amount
                    '#Card Total => OOW Card + Other Card
                    lblCardTotal1.Text = CommonControl.setINR(lstMoneyAggregationModel(0).OutWardCardAmt + lstMoneyAggregationModel(0).OtherCardAmt - lstMoneyAggregationModel(0).DiscountAmtCard)

                End If

                'Activities
                Dim lstMsg As ListBox = CType(grvSalesReport.Rows(i).FindControl("lstMsg"), ListBox)
                lstMsg.Items.Add("Today's Result")
                lstMsg.Items.Add("Open Time") '& "　　" & dtWork & "　" & openResult)
                lstMsg.Items.Add("Open User") ' & "　" & setRelultListALL(showNo).open_youser_name)
                lstMsg.Items.Add("Close Time") '& "　　" & dtWork2 & "　" & closeResult)
                lstMsg.Items.Add("Close User") ' & "　" & setRelultListALL(showNo).close_youser_name)
                lstMsg.Items.Add("Inspection")
                lstMsg.Items.Add("1st") ' & "　" & dtWork3 & "　" & ins1Result & "　" & setRelultListALL(showNo).ins1_youser_name)
                lstMsg.Items.Add("2nd") ' & "　" & dtWork4 & "　" & ins2Result & "　" & setRelultListALL(showNo).ins2_youser_name)
                lstMsg.Items.Add("3rd") ' & "　" & dtWork5 & "　" & ins3Result & "　" & setRelultListALL(showNo).ins3_youser_name)
                'If money_diff.ToString <> "0" Then
                '    lstMsg.Items.Add("money diifference" & "　　" & money_diff & "INR")
                'Else
                lstMsg.Items.Add("Money Diifference")
                'End If
                lstMsg.Items.Add("Attend Today Number")
                lstMsg.Items.Add("Late")
                lstMsg.Items.Add("Absence")
            End If
        Next

    End Sub
    ''' <summary>
    ''' 
    ''' </summary>
    ''' <param name="SearchDate"></param>
    Sub MoneySalesAllAggregation(ByVal Optional SearchDate As String = "", ByVal Optional BranchCode As String = "")
        'Check Money Agreegation Count
        Dim _MoneyAggregationModel As MoneyAggregationModel = New MoneyAggregationModel()
        Dim _MoneyAggregationControl As MoneyAggregationControl = New MoneyAggregationControl()
        If Not (SearchDate = "") Then
            _MoneyAggregationModel.InvoiceDate = SearchDate
        End If
        If Not (BranchCode = "") Then
            _MoneyAggregationModel.Location = BranchCode
        End If
        ' _MoneyAggregationModel.PaymentKind = 1
        Dim MoneyAggregationCheck As String = _MoneyAggregationControl.SelectMoneyAggregationCheck(_MoneyAggregationModel)
        'if No Datas found, then show the error message to the user
        If MoneyAggregationCheck = "0" Then
            Call showMsg("There is no records found")
            Exit Sub
            'Else
            '    lblCount.Text = MoneyAggregationCheck
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
            '''''''''''lblSales.Text = CommonControl.setINR(lstMoneyAggregationModel(0).Sales)

            'Inward Amount
            If String.IsNullOrEmpty(lstMoneyAggregationModel(0).InWardAmt) Then
                lstMoneyAggregationModel(0).InWardAmt = 0.00
                lstMoneyAggregationModel(0).InWardCnt = 0
            End If
            lblIWSum.Text = CommonControl.setINR(lstMoneyAggregationModel(0).InWardAmt) 'CommonControl.setINR(lstMoneyAggregationModel(0).DiscountAmt.ToString()) & "INR"
            lblIWNoTax.Text = CommonControl.setINR(lstMoneyAggregationModel(0).InWardAmt / 1.18)
            lblIWCnt.Text = lstMoneyAggregationModel(0).InWardCnt.ToString()

            'Outward Cash Amount
            If String.IsNullOrEmpty(lstMoneyAggregationModel(0).OutWardCashAmt) Then
                lstMoneyAggregationModel(0).OutWardCashAmt = 0.00
                lstMoneyAggregationModel(0).OutWardCashCnt = 0
            End If
            lblOOWCashSum.Text = CommonControl.setINR(lstMoneyAggregationModel(0).OutWardCashAmt)
            lblOWCashNoTax.Text = CommonControl.setINR(lstMoneyAggregationModel(0).OutWardCashAmt / 1.18)
            lblOOWCashCnt.Text = lstMoneyAggregationModel(0).OutWardCashCnt.ToString()

            'Outward Card Amount
            If String.IsNullOrEmpty(lstMoneyAggregationModel(0).OutWardCardAmt) Then
                lstMoneyAggregationModel(0).OutWardCardAmt = 0.00
                lstMoneyAggregationModel(0).OutWardCardCnt = 0
            End If
            lblOOWCardSum.Text = CommonControl.setINR(lstMoneyAggregationModel(0).OutWardCardAmt)
            lblOWCardNoTax.Text = CommonControl.setINR(lstMoneyAggregationModel(0).OutWardCardAmt / 1.18)
            lblOOWCardCnt.Text = lstMoneyAggregationModel(0).OutWardCardCnt.ToString()

            'Other Cash Amount
            If String.IsNullOrEmpty(lstMoneyAggregationModel(0).OtherCashAmt) Then
                lstMoneyAggregationModel(0).OtherCashAmt = 0.00
                lstMoneyAggregationModel(0).OtherCashCnt = 0
            End If
            lblOtherCashSum.Text = CommonControl.setINR(lstMoneyAggregationModel(0).OtherCashAmt)
            lblOtCashNoTax.Text = CommonControl.setINR(lstMoneyAggregationModel(0).OtherCashAmt / 1.18)
            lblOtherCashCnt.Text = lstMoneyAggregationModel(0).OtherCashCnt.ToString()

            'Other Card Amount
            If String.IsNullOrEmpty(lstMoneyAggregationModel(0).OtherCardAmt) Then
                lstMoneyAggregationModel(0).OtherCardAmt = 0.00
                lstMoneyAggregationModel(0).OtherCardCnt = 0
            End If
            lblOtherCardSum.Text = CommonControl.setINR(lstMoneyAggregationModel(0).OtherCardAmt)
            lblOtCardNoTax.Text = CommonControl.setINR(lstMoneyAggregationModel(0).OtherCardAmt / 1.18)
            lblOtherCardCnt.Text = lstMoneyAggregationModel(0).OtherCardCnt.ToString()

            'Daily Openening Cash / Advance for cash counter by GSS
            'Find Opening Balance in the counter of starting of the day. 
            '#Open Cash 
            _MoneyAggregationModel.SearchDate = SearchDate
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
            '''''''''''''''''''''''lblReserve.Text = lstMoneyAggregationModel(0).OpeningCash
            'Find Customer Advance / Advance amount by Customer
            '#Customer Advance / Custody
            Dim dtCustAdvance As DataTable = _MoneyAggregationControl.SelectCustomerAdvance(_MoneyAggregationModel)
            If (dtCustAdvance Is Nothing) Or (dtCustAdvance.Rows(0)("count") = 0) Then ' if count Zero then Advance amount will be zero
                lstMoneyAggregationModel(0).AdvanceAmt = 0.00
                lstMoneyAggregationModel(0).AdvanceCnt = 0
            Else
                lstMoneyAggregationModel(0).AdvanceAmt = dtCustAdvance.Rows(0)("AdvanceAmt")
                lstMoneyAggregationModel(0).AdvanceCnt = dtCustAdvance.Rows(0)("count")
            End If
            lblDepositCusto.Text = CommonControl.setINR(lstMoneyAggregationModel(0).AdvanceAmt)
            lblNumCusto.Text = lstMoneyAggregationModel(0).AdvanceCnt ' No of Customer paid for advance

            'Actually Cash collected from the customer
            '#Deposit to Bank
            If String.IsNullOrEmpty(lstMoneyAggregationModel(0).BankDeposit) Then
                lstMoneyAggregationModel(0).BankDeposit = 0.00
            End If
            lblDeposit.Text = CommonControl.setINR(lstMoneyAggregationModel(0).BankDeposit)

            'Inward Claim Amount 
            If String.IsNullOrEmpty(lstMoneyAggregationModel(0).InWardClaimAmt) Then
                lstMoneyAggregationModel(0).InWardClaimAmt = 0.00
            End If
            lblIWSumClaim.Text = CommonControl.setINR(lstMoneyAggregationModel(0).InWardClaimAmt)
            lblIWNoTaxClaim.Text = CommonControl.setINR(lstMoneyAggregationModel(0).InWardClaimAmt / 1.18)
            'Outward Cash Claim amount
            If String.IsNullOrEmpty(lstMoneyAggregationModel(0).OutWardCashClaimAmt) Then
                lstMoneyAggregationModel(0).OutWardCashClaimAmt = 0.00
            End If
            lblOOWCashSumClaim.Text = CommonControl.setINR(lstMoneyAggregationModel(0).OutWardCashClaimAmt)
            lblOWCashNoTaxClaim.Text = CommonControl.setINR(lstMoneyAggregationModel(0).OutWardCashClaimAmt / 1.18)
            'Outward Card Claim amount
            If String.IsNullOrEmpty(lstMoneyAggregationModel(0).OutWardCardClaimAmt) Then
                lstMoneyAggregationModel(0).OutWardCardClaimAmt = 0.00
            End If
            lblOOWCardSumClaim.Text = CommonControl.setINR(lstMoneyAggregationModel(0).OutWardCardClaimAmt)
            lblOWCardNoTaxClaim.Text = CommonControl.setINR(lstMoneyAggregationModel(0).OutWardCardClaimAmt / 1.18)
            'Other Cash Claim amount
            If String.IsNullOrEmpty(lstMoneyAggregationModel(0).OtherCashClaimAmt) Then
                lstMoneyAggregationModel(0).OtherCashClaimAmt = 0.00
            End If
            lblOtherCashSumClaim.Text = CommonControl.setINR(lstMoneyAggregationModel(0).OtherCashClaimAmt)
            lblOtCashNoTaxClaim.Text = CommonControl.setINR(lstMoneyAggregationModel(0).OtherCashClaimAmt / 1.18)
            'Other Card Claim amount
            If String.IsNullOrEmpty(lstMoneyAggregationModel(0).OtherCardClaimAmt) Then
                lstMoneyAggregationModel(0).OtherCardClaimAmt = 0.00
            End If
            lblOtherCardSumClaim.Text = CommonControl.setINR(lstMoneyAggregationModel(0).OtherCardClaimAmt)
            lblOtCardNoTaxClaim.Text = CommonControl.setINR(lstMoneyAggregationModel(0).OtherCardClaimAmt / 1.18)
        End If
        'Add Sales + Customer Deposit for the sales 
        lblSales.Text = CommonControl.setINR(lstMoneyAggregationModel(0).Sales)

        'Cash Total = Opening Cash in Hand + Cash collected either IW/OOW/OTHER 
        '#cash total =>sales(cash) =>Removed lstMoneyAggregationModel(0).OpeningCash +
        lblCashTotal.Text = CommonControl.setINR((lstMoneyAggregationModel(0).OpeningCash + lstMoneyAggregationModel(0).OutWardCashAmt + lstMoneyAggregationModel(0).OtherCashAmt) - (lstMoneyAggregationModel(0).DiscountAmtCash + lstMoneyAggregationModel(0).BankDeposit))
        'Card Total Amount
        '#Card Total => OOW Card + Other Card
        lblCardTotal.Text = CommonControl.setINR(lstMoneyAggregationModel(0).OutWardCardAmt + lstMoneyAggregationModel(0).OtherCardAmt - lstMoneyAggregationModel(0).DiscountAmtCard)
    End Sub
    ''' <summary>
    ''' Show the message to the user
    ''' </summary>
    ''' <param name="Msg"></param>
    Protected Sub showMsg(ByVal Msg As String)
        lblMsg.Text = Msg
        Dim sScript As String = "$(function () { $( ""#dialog"" ).dialog({width: 400, buttons: {""OK"": function () {$(this).dialog('close');}}});});"
        'JavaScriptの埋め込み
        ClientScript.RegisterStartupScript(Me.GetType(), "startup", sScript, True)
    End Sub

    ''' <summary>
    ''' Loading All branches
    ''' </summary>
    Private Sub InitDropDownList()
        Log4NetControl.ComInfoLogWrite(Session("UserName"))
        Dim userName As String = Session("user_id") 'Session("user_Name")
        'Clear the branch location
        DropListLocation.Items.Clear()
        'For store the branch codes in array
        Dim shipCodeAll() As String
        'Verify entered user and password correct or not with the database
        Dim _UserInfoModel As UserInfoModel = New UserInfoModel()
        Dim _UserInfoControl As UserInfoControl = New UserInfoControl()
        _UserInfoModel.UserId = userName
        '_UserInfoModel.Password = TextPass.Text.ToString().Trim()
        Dim UserInfoList As List(Of UserInfoModel) = _UserInfoControl.SelectUserInfo(_UserInfoModel)
        'User Doesn't exist
        If UserInfoList Is Nothing OrElse UserInfoList.Count = 0 Then
            Call showMsg("The username / password incorrect. Please try again")
            Exit Sub
        End If
        'Loading All Branch Codes and stored in a array for the session
        Dim _ShipBaseControl As ShipBaseControl = New ShipBaseControl()
        Dim dt As DataTable = _ShipBaseControl.SelectBranchCode()
        ReDim shipCodeAll(dt.Rows.Count - 1)
        Dim i As Integer = 0
        For Each dr As DataRow In dt.Rows
            If dr("ship_code") IsNot DBNull.Value Then
                shipCodeAll(i) = dr("ship_code")
            End If
            i = i + 1
        Next
        Log4NetControl.ComInfoLogWrite(Session("UserName"))
        Dim codeMasterControl As CodeMasterControl = New CodeMasterControl()
        'QryFlag 
        'QryFlag 1 - # Specific Filter
        'QryFlag 2 - # All records
        Dim QryFlag As Integer = 1 'Specific Records
        If (UserInfoList(0).UserLevel = CommonConst.UserLevel0) Or
                        (UserInfoList(0).UserLevel = CommonConst.UserLevel1) Or
                        (UserInfoList(0).UserLevel = CommonConst.UserLevel2) Or
                (UserInfoList(0).AdminFlg = True) Then
            QryFlag = 2
        End If
        Dim codeMasterList As List(Of CodeMasterModel) = codeMasterControl.SelectBranchMaster(QryFlag, "'" & UserInfoList(0).Ship1.Replace(",", "','") & "'")



        Log4NetControl.ComInfoLogWrite(Session("UserName"))
        ' Dim codeMasterControl As CodeMasterControl = New CodeMasterControl()
        'Loading branch name and code in the dropdown list
        '  Dim codeMasterList As List(Of CodeMasterModel) = codeMasterControl.SelectBranchMaster()
        Dim codeMaster1 As CodeMasterModel = New CodeMasterModel()
        codeMaster1.CodeValue = "Select Branch"
        codeMaster1.CodeDispValue = "Select Branch"
        codeMasterList.Insert(0, codeMaster1)
        'Only Userleve 0 - 2  
        If (UserInfoList(0).UserLevel = CommonConst.UserLevel0) Or
                        (UserInfoList(0).UserLevel = CommonConst.UserLevel1) Or
                        (UserInfoList(0).UserLevel = CommonConst.UserLevel2) Or
                (UserInfoList(0).AdminFlg = True) Then
            Dim codeMaster2 As CodeMasterModel = New CodeMasterModel()
            codeMaster2.CodeValue = "ALL"
            codeMaster2.CodeDispValue = "ALL"
            codeMasterList.Insert(1, codeMaster2)
        End If



        Me.DropListLocation.DataSource = codeMasterList
        Me.DropListLocation.DataTextField = "CodeDispValue"
        Me.DropListLocation.DataValueField = "CodeValue"
        Me.DropListLocation.DataBind()
    End Sub

End Class