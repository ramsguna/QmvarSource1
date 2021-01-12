Imports System.IO
Imports System.Text
Imports System.Net.Mail
Imports System.Web.Configuration

Imports Ganges33.Ganges33.model
Imports Ganges33.Ganges33.logic

Public Class Money_Report_qg
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
        Dim userName As String = Session("user_Name")
        Dim shipCode As String = Session("ship_code")
        Dim dt As DateTime = DateTime.Now
        Dim dtNow As DateTime = dt.AddMinutes(ConfigurationManager.AppSettings("TimeDiff"))
        lblRecord.Text = dtNow.ToString("yyyyMMddHHmmss")
        lblYousername.Text = userName

        Session("Record_Date") = lblRecord.Text
        hidDate.Value = dtNow.ToShortDateString

        'Check Money Agreegation Count
        Dim _MoneyAggregationModel As MoneyAggregationModel = New MoneyAggregationModel()
        Dim _MoneyAggregationControl As MoneyAggregationControl = New MoneyAggregationControl()
        _MoneyAggregationModel.InvoiceDate = dtNow.ToShortDateString
        _MoneyAggregationModel.Location = shipCode
        ' _MoneyAggregationModel.PaymentKind = 1

        Dim MoneyAggregationCheck As String = _MoneyAggregationControl.SelectMoneyAggregationCheck(_MoneyAggregationModel)
        'if No Datas found, then show the error message to the user
        If MoneyAggregationCheck = "0" Then
            Call showMsg("There is no transaction found on " + dtNow.ToShortDateString)
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
            ''''''''''''lblSales.Text = CommonControl.setINR(lstMoneyAggregationModel(0).Sales)
            '#Discount Number 
            '#Discount Amount
            If String.IsNullOrEmpty(lstMoneyAggregationModel(0).DiscountAmt) Then
                lstMoneyAggregationModel(0).DiscountAmt = 0.00
                lstMoneyAggregationModel(0).DiscountCnt = 0
            End If
            lblDisAmount.Text = CommonControl.setINR(lstMoneyAggregationModel(0).DiscountAmt) 'CommonControl.setINR(lstMoneyAggregationModel(0).DiscountAmt.ToString()) & "INR"
            lblDisNum.Text = lstMoneyAggregationModel(0).DiscountCnt.ToString
            'Store in Hidden Fields
            '#Full Discount Number 
            '#Full Discount Amount
            If String.IsNullOrEmpty(lstMoneyAggregationModel(0).FullDiscountAmt) Then
                lstMoneyAggregationModel(0).FullDiscountAmt = 0.00
                lstMoneyAggregationModel(0).FullDiscountCnt = 0
            End If
            hidFullDiscAmt.Value = CommonControl.setINR(lstMoneyAggregationModel(0).FullDiscountAmt) 'CommonControl.setINR(lstMoneyAggregationModel(0).DiscountAmt.ToString()) & "INR"
            hidFullDiscCnt.Value = lstMoneyAggregationModel(0).FullDiscountCnt.ToString

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
            _MoneyAggregationModel.SearchDate = dtNow.ToShortDateString
            Dim dtOpeningBal As DataTable = _MoneyAggregationControl.SelectOpeningBalance(_MoneyAggregationModel)
            If (dtOpeningBal Is Nothing) Or (dtOpeningBal.Rows.Count = 0) Then
                lstMoneyAggregationModel(0).OpeningCash = 0.00
            Else
                If String.IsNullOrEmpty(dtOpeningBal.Rows(0)("total")) Then
                    lstMoneyAggregationModel(0).OpeningCash = 0.00
                Else
                    lstMoneyAggregationModel(0).OpeningCash = dtOpeningBal.Rows(0)("total")
                End If
                If Not String.IsNullOrEmpty(dtOpeningBal.Rows(0)("datetime")) Then
                    hidOpeningDt.Value = dtOpeningBal.Rows(0)("datetime")
                End If

            End If
            '''''''''''''''''''''''lblReserve.Text = lstMoneyAggregationModel(0).OpeningCash
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
            '''''''''''''''''''''lblDepositCusto.Text = lstMoneyAggregationModel(0).AdvanceAmt
            '#Number of Advance
            '''''''''''''''''''lblNumCusto.Text = lstMoneyAggregationModel(0).AdvanceCnt ' No of Customer paid for advance

            'Actually Cash collected from the customer without discount
            '#Deposit
            If String.IsNullOrEmpty(lstMoneyAggregationModel(0).BankDeposit) Then
                lstMoneyAggregationModel(0).BankDeposit = 0.00
            End If
            lblDeposit.Text = CommonControl.setINR(lstMoneyAggregationModel(0).BankDeposit)

            'Inward Claim Amount 
            If String.IsNullOrEmpty(lstMoneyAggregationModel(0).InWardClaimAmt) Then
                lstMoneyAggregationModel(0).InWardClaimAmt = 0.00
            End If
            lblIWNoTaxClaim.Text = CommonControl.setINR(lstMoneyAggregationModel(0).InWardClaimAmt)
            lblIWSumClaim.Text = CommonControl.setINR(lstMoneyAggregationModel(0).InWardClaimAmt / 1.18)
            'Outward Cash Claim amount
            If String.IsNullOrEmpty(lstMoneyAggregationModel(0).OutWardCashClaimAmt) Then
                lstMoneyAggregationModel(0).OutWardCashClaimAmt = 0.00
            End If
            lblOWCashNoTaxClaim.Text = CommonControl.setINR(lstMoneyAggregationModel(0).OutWardCashClaimAmt)
            lblOOWCashSumClaim.Text = CommonControl.setINR(lstMoneyAggregationModel(0).OutWardCashClaimAmt / 1.18)
            'Outward Card Claim amount
            If String.IsNullOrEmpty(lstMoneyAggregationModel(0).OutWardCardClaimAmt) Then
                lstMoneyAggregationModel(0).OutWardCardClaimAmt = 0.00
            End If
            lblOWCardNoTaxClaim.Text = CommonControl.setINR(lstMoneyAggregationModel(0).OutWardCardClaimAmt)
            lblOOWCardSumClaim.Text = CommonControl.setINR(lstMoneyAggregationModel(0).OutWardCardClaimAmt / 1.18)
            'Other Cash Claim amount
            If String.IsNullOrEmpty(lstMoneyAggregationModel(0).OtherCashClaimAmt) Then
                lstMoneyAggregationModel(0).OtherCashClaimAmt = 0.00
            End If
            lblOtCashNoTaxClaim.Text = CommonControl.setINR(lstMoneyAggregationModel(0).OtherCashClaimAmt)
            lblOtherCashSumClaim.Text = CommonControl.setINR(lstMoneyAggregationModel(0).OtherCashClaimAmt / 1.18)
            'Other Card Claim amount
            If String.IsNullOrEmpty(lstMoneyAggregationModel(0).OtherCardClaimAmt) Then
                lstMoneyAggregationModel(0).OtherCardClaimAmt = 0.00
            End If
            lblOtCardNoTaxClaim.Text = CommonControl.setINR(lstMoneyAggregationModel(0).OtherCardClaimAmt)
            lblOtherCardSumClaim.Text = CommonControl.setINR(lstMoneyAggregationModel(0).OtherCardClaimAmt / 1.18)

        End If

        'Add Sales + Customer Deposit for the sales 
        lblSales.Text = CommonControl.setINR(lstMoneyAggregationModel(0).Sales)

        'Cash Total = Opening Cash in Hand + Cash collected either IW/OOW/OTHER 
        '#cash total =>open deposit + sales(cash)
        lblCashTotal.Text = CommonControl.setINR((lstMoneyAggregationModel(0).OpeningCash + lstMoneyAggregationModel(0).OutWardCashAmt + lstMoneyAggregationModel(0).OtherCashAmt) - (lstMoneyAggregationModel(0).DiscountAmtCash + lstMoneyAggregationModel(0).BankDeposit))
        btnSend.Enabled = True

    End Sub

    Protected Sub btnSend_Click(sender As Object, e As ImageClickEventArgs) Handles btnSend.Click
        'Dim userid As String = Session("user_id")
        Dim userName As String = Session("user_Name")
        Dim shipCode As String = Session("ship_code")
        Dim shipName As String = Session("ship_Name")

        'Check Money Agreegation Count
        Dim _MoneyAggregationModel As MoneyAggregationModel = New MoneyAggregationModel()
        Dim _MoneyAggregationControl As MoneyAggregationControl = New MoneyAggregationControl()
        Dim dtNow As DateTime = hidDate.Value
        _MoneyAggregationModel.SearchDate = dtNow.ToShortDateString
        _MoneyAggregationModel.Location = shipCode
        Dim dtClosingChk As DataTable = _MoneyAggregationControl.SelectClosingCheck(_MoneyAggregationModel)
        If dtClosingChk Is Nothing Then
            Call showMsg("Since the OpenClose processing has not been completed, it can not be completed.")
            Exit Sub
        End If

        If ConfigurationManager.AppSettings("EmailSend") = "true" Then
            'Dim dtNow As DateTime = clsCommon.dtIndia
            ' Dim dtNow As DateTime = DateTime.Now.AddDays(-1) 'CommonConst.dtIndia
            Dim mailStatus As Boolean = False
            Try
                Dim mailParams As MailModel = New MailModel()
                Dim mailBLL As MailControl = New MailControl()
                mailParams.FromMail = ConfigurationManager.AppSettings("Mail")
                Dim lstMail As List(Of String) = New List(Of String)()
                lstMail.Add(ConfigurationManager.AppSettings("ToEmail1"))
                lstMail.Add(ConfigurationManager.AppSettings("ToEmail2"))
                lstMail.Add(ConfigurationManager.AppSettings("ToEmail3"))
                lstMail.Add(ConfigurationManager.AppSettings("ToEmail4"))
                mailParams.ToMail = lstMail
                mailParams.TitleMail = "**[Sales Report]**  " & hidDate.Value & "  _  " & shipName
                Dim body As String = String.Empty
                'body = body & " Line 1" & Environment.NewLine
                'body = body & " Line 2" & Environment.NewLine

                body &= "-----Sales Report -----" & vbCrLf
                body &= dtNow.ToString & vbCrLf
                body &= "Report User : " & userName & vbCrLf
                body &= "Start Time : " & hidOpeningDt.Value & " ----- " & "End Time :" & dtClosingChk.Rows(0)("datetime") & vbCrLf
                body &= "Today's Result of " & shipName & vbCrLf
                body &= "IW" & vbCrLf
                body &= "Number : " & lblCount.Text & vbCrLf
                body &= "Amount(notax) : " & lblIWNoTax.Text & " INR" & vbCrLf
                body &= "Amount(taxin) : " & lblIWSum.Text & " INR" & vbCrLf
                body &= "OOW" & vbCrLf
                body &= "Number : " & CInt(Val(lblOOWCashCnt.Text)) + CInt(Val(lblOOWCardCnt.Text)) & vbCrLf
                body &= "Amount(notax) : " & CDec(Val(lblOWCashNoTax.Text)) + CDec(Val(lblOWCardNoTax.Text)) & " INR" & vbCrLf
                body &= "Amount(taxin) : " & CDec(Val(lblOOWCashSum.Text)) + CDec(Val(lblOOWCardSum.Text)) & " INR" & vbCrLf
                body &= "OTHER" & vbCrLf
                body &= "Number : " & CInt(Val(lblOtherCashCnt.Text)) + CInt(Val(lblOtherCardCnt.Text)) & vbCrLf
                body &= "Amount(notax) : " & CDec(Val(lblOtCashNoTax.Text)) + CDec(Val(lblOtCardNoTax.Text)) & " INR" & vbCrLf
                body &= "Amount(taxin) : " & lblOtherCashSum.Text + lblOtherCardSum.Text & "INR" & vbCrLf
                body &= "Full Discount : " & hidFullDiscCnt.Value & "/" & hidFullDiscAmt.Value & " INR" & vbCrLf
                body &= "Discount : " & lblDisNum.Text & "/" & lblDisAmount.Text & " INR" & vbCrLf
                body &= "Cash Total : " & lblCashTotal.Text & " INR" & vbCrLf
                body &= "Sales Total : " & lblSales.Text & " INR" & vbCrLf
                mailParams.BodyMail = body
                mailParams.SmtpMail = ConfigurationManager.AppSettings("Smtp")
                mailParams.PortMail = Integer.Parse(ConfigurationManager.AppSettings("SmtpPort"))
                mailParams.EnableSslMail = True
                mailParams.CredentialsMail = ConfigurationManager.AppSettings("Mail")
                mailParams.CredentialsPassword = ConfigurationManager.AppSettings("Password")
                Dim flag As Boolean = mailBLL.SendMail(mailParams)
                If flag Then
                    mailStatus = True
                Else
                    mailStatus = False
                End If
            Catch ex As Exception
                mailStatus = False
            End Try
            If mailStatus Then
                Call showMsg("The Email has been sent successfully")
            Else
                Call showMsg("Failed to send Email")
            End If
        End If
    End Sub

    Protected Sub showMsg(ByVal Msg As String)

        lblMsg.Text = Msg
        Dim sScript As String = "$(function () { $( ""#dialog"" ).dialog({width: 400, buttons: {""OK"": function () {$(this).dialog('close');}}});});"
        'JavaScriptの埋め込み
        ClientScript.RegisterStartupScript(Me.GetType(), "startup", sScript, True)

    End Sub

    Protected Sub reSet()
        lblCount.Text = ""
        lblIWSum.Text = ""
        lblIWNoTax.Text = ""
        lblIWNoTaxClaim.Text = ""
        lblIWSumClaim.Text = ""
        lblOOWCardSum.Text = ""
        lblOOWCashSum.Text = ""
        lblOtherCardSum.Text = ""
        lblOtherCashSum.Text = ""
        lblOWCardNoTax.Text = ""
        lblOWCashNoTax.Text = ""
        lblOtCardNoTax.Text = ""
        lblOtCashNoTax.Text = ""
        lblOWCardNoTaxClaim.Text = ""
        lblOWCashNoTaxClaim.Text = ""
        lblOtCardNoTaxClaim.Text = ""
        lblOtCashNoTaxClaim.Text = ""
        lblOOWCardSumClaim.Text = ""
        lblOOWCashSumClaim.Text = ""
        lblOtherCardSumClaim.Text = ""
        lblOtherCashSumClaim.Text = ""
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