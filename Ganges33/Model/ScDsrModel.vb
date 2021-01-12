Imports System
Imports System.Collections.Generic
Imports System.Configuration
Namespace Ganges33.model
    <Serializable>
    Public Class ScDsrModel
        Public Sub InvoiceUpdateModel()
            CRTDT = String.Empty
            CRTCD = String.Empty
            UPDDT = String.Empty
            UPDCD = String.Empty
            UPDPG = String.Empty
            DELFG = String.Empty

            UserName = String.Empty
            TDateTime = String.Empty
            UserId = String.Empty

            UnqNo = String.Empty
            UploadUser = String.Empty
            UploadDate = String.Empty
            ShipToBranchCode = String.Empty
            ShipToBranch = String.Empty


            Tfalg = String.Empty
            ServiceOrderNo = String.Empty
            LastUpdtUser = String.Empty
            BillinbUser = String.Empty
            BillingDate = String.Empty
            GoodsDeliveredDate = String.Empty
            BranchName = String.Empty
            Engineer = String.Empty
            Product1 = String.Empty
            Product2 = String.Empty
            IwLabor = String.Empty
            IwParts = String.Empty
            IwTransport = String.Empty
            IwOthers = String.Empty
            IwTax = String.Empty
            IwTotal = String.Empty
            OwLabor = String.Empty
            OwParts = String.Empty
            OwTransport = String.Empty
            OwOthers = String.Empty
            OwTax = String.Empty
            OwTotal = String.Empty
            UploadFileName = String.Empty
            UploadDate = String.Empty

            DateFrom = String.Empty
            DateTo = String.Empty
            Number = String.Empty


        End Sub

        Public Property CRTDT As String
        Public Property CRTCD As String
        Public Property UPDDT As String
        Public Property UPDCD As String
        Public Property UPDPG As String
        Public Property DELFG As String

        Public Property UserName As String
        Public Property TDateTime As String
        Public Property UserId As String
        Public Property UnqNo As String
        Public Property UploadUser As String

        Public Property ShipToBranchCode As String
        Public Property ShipToBranch As String

        Public Property Tfalg As String
        Public Property ServiceOrderNo As String
        Public Property LastUpdtUser As String
        Public Property BillinbUser As String
        Public Property BillingDate As String
        Public Property GoodsDeliveredDate As String
        Public Property BranchName As String
        Public Property Engineer As String
        Public Property Product1 As String
        Public Property Product2 As String
        Public Property IwLabor As String
        Public Property IwParts As String
        Public Property IwTransport As String
        Public Property IwOthers As String
        Public Property IwTax As String
        Public Property IwTotal As String
        Public Property OwLabor As String
        Public Property OwParts As String
        Public Property OwTransport As String
        Public Property OwOthers As String
        Public Property OwTax As String
        Public Property OwTotal As String
        Public Property UploadFileName As String
        Public Property UploadDate As String

        Public Property DateFrom As String
        Public Property DateTo As String

        Public Property Number As String
    End Class
End Namespace
