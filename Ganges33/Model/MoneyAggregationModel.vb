Imports System
Imports System.Collections.Generic
Imports System.Configuration
Namespace Ganges33.model
    <Serializable>
    Public Class MoneyAggregationModel

        Public Sub MoneyAggregationModel()
            InWardCnt = 0
            OutWardCashCnt = 0
            OutWardCardCnt = 0
            OtherCashCnt = 0
            OtherCardCnt = 0
            DepositCashCnt = 0
            DepositCardCnt = 0

            InWardAmt = 0.00
            OutWardCashAmt = 0.00
            OutWardCardAmt = 0.00
            OtherCashAmt = 0.00
            OtherCardAmt = 0.00
            DepositCashAmt = 0.00
            DepositCardAmt = 0.00

            InWardClaimAmt = 0.00
            OutWardCashClaimAmt = 0.00
            OutWardCardClaimAmt = 0.00
            OtherCashClaimAmt = 0.00
            OtherCardClaimAmt = 0.00

            OpeningCash = 0.00
            BankDeposit = 0.00
            CashTotal = 0.00
            Sales = 0.00

            AdvanceAmt = 0.00
            AdvanceCnt = 0.00
            DiscountAmt = 0.00
            DiscountCnt = 0.00
            FullDiscountAmt = 0.00
            FullDiscountCnt = 0.00

            InvoiceDate = String.Empty
            SearchDate = String.Empty
            Location = String.Empty
            PaymentKind = String.Empty

            CashCollected = 0.00
            DiscountAmtCash = 0.00
            DiscountAmtCard = 0.00

            CoporateAmtCash = 0.00

        End Sub


        Public Property InWardCnt As Integer
        Public Property OutWardCashCnt As Integer
        Public Property OutWardCardCnt As Integer
        Public Property OtherCashCnt As Integer
        Public Property OtherCardCnt As Integer
        Public Property DepositCashCnt As Integer
        Public Property DepositCardCnt As Integer

        Public Property InWardAmt As Decimal
        Public Property OutWardCashAmt As Decimal
        Public Property OutWardCardAmt As Decimal
        Public Property OtherCashAmt As Decimal
        Public Property OtherCardAmt As Decimal
        Public Property DepositCashAmt As Decimal
        Public Property DepositCardAmt As Decimal

        Public Property InWardClaimAmt As Decimal
        Public Property OutWardCashClaimAmt As Decimal
        Public Property OutWardCardClaimAmt As Decimal
        Public Property OtherCashClaimAmt As Decimal
        Public Property OtherCardClaimAmt As Decimal

        Public Property OpeningCash As Decimal
        Public Property BankDeposit As Decimal
        Public Property CashTotal As Decimal
        Public Property Sales As Decimal

        Public Property AdvanceAmt As Decimal
        Public Property AdvanceCnt As Decimal
        Public Property DiscountAmt As Decimal
        Public Property DiscountCnt As Decimal

        Public Property InvoiceDate As String
        Public Property SearchDate As String
        Public Property Location As String
        Public Property PaymentKind As String

        Public Property CashCollected As Decimal ' Other than discount

        Public Property FullDiscountAmt As Decimal
        Public Property FullDiscountCnt As Decimal

        Public Property DiscountAmtCash As Decimal
        Public Property DiscountAmtCard As Decimal

        Public Property CoporateAmtCash As Decimal

    End Class
End Namespace