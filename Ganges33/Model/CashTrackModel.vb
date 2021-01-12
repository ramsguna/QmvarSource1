
Imports System
Imports System.Collections.Generic
Imports System.Configuration
Namespace Ganges33.model
    <Serializable>
    Public Class CashTrackModel
        Public Sub CashTrackModel()
            CRTDT = String.Empty
            CRTCD = String.Empty
            UPDDT = String.Empty
            UPDCD = String.Empty
            UPDPG = String.Empty
            DELFG = String.Empty
            Status = String.Empty
            UserName = String.Empty
            TDateTime = String.Empty
            UserId = String.Empty
            ServiceOrderNo = String.Empty
            LastUpdatedUser = String.Empty
            BillingUser = String.Empty
            BillingDate = String.Empty
            GoodsDeliveredDate = String.Empty
            BranchName = String.Empty
            Engineer = String.Empty
            EngineerName = String.Empty
            Product = String.Empty
            ProductType = String.Empty
            InLabor = String.Empty
            InParts = String.Empty
            InTransport = String.Empty
            InOthers = String.Empty
            InTax = String.Empty
            InTotal = String.Empty
            OutLabor = String.Empty
            OutParts = String.Empty
            OutTransport = String.Empty
            OutOthers = String.Empty
            OutTax = String.Empty
            OutTotal = String.Empty
            FileName = String.Empty
            Location = String.Empty
            IsCard = False
            InComplete = False
            MoneyType = 0
            CashAmt = 0.00
            CardAmt = 0.00
            Discount = 0
            Claim = 0.00
            ClaimCard = 0.00
            Deposit = 0.00
            DiscountAfterAmount = 0.00
            DiscountAfterCredit = 0.00
            DateType = String.Empty

        End Sub
        Public Property CRTDT As String
        Public Property CRTCD As String
        Public Property UPDDT As String
        Public Property UPDCD As String
        Public Property UPDPG As String
        Public Property DELFG As String
        Public Property Status As String
        Public Property UserName As String
        Public Property TDateTime As String
        Public Property UserId As String
        Public Property ServiceOrderNo As String
        Public Property LastUpdatedUser As String
        Public Property BillingUser As String
        Public Property BillingDate As String
        Public Property GoodsDeliveredDate As String
        Public Property BranchName As String
        Public Property Engineer As String
        Public Property EngineerName As String
        Public Property Product As String
        Public Property ProductType As String
        Public Property InLabor As String
        Public Property InParts As String
        Public Property InTransport As String
        Public Property InOthers As String
        Public Property InTax As String
        Public Property InTotal As String
        Public Property OutLabor As String
        Public Property OutParts As String
        Public Property OutTransport As String
        Public Property OutOthers As String
        Public Property OutTax As String
        Public Property OutTotal As String
        Public Property FileName As String
        Public Property Location As String
        Public Property IsCard As Boolean

        Public Property InComplete As Integer

        Public Property MoneyType As Integer
        Public Property CashAmt As Decimal
        Public Property CardAmt As Decimal
        Public Property Discount As Decimal
        Public Property Claim As Decimal
        Public Property ClaimCard As Decimal
        Public Property Deposit As Decimal
        Public Property DiscountAfterAmount As Decimal
        Public Property DiscountAfterCredit As Decimal
        Public Property DateType As String

    End Class
End Namespace
