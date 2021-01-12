Imports System
Imports System.Collections.Generic
Imports System.Configuration
Namespace Ganges33.model
    <Serializable>
    Public Class PrDetailModel
        Public Sub PrDetailModel()
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
            UploadUser = String.Empty
            UploadDate = String.Empty
            ShipToBranchCode = String.Empty
            ShipToBranch = String.Empty
            BillingDate = String.Empty
            InvoiceNo = String.Empty
            ItemNo = String.Empty
            DeliveryNo = String.Empty
            PoNo = String.Empty
            PoType = String.Empty
            PartNo = String.Empty
            HSNCode = String.Empty 'VJ 2019/10/17
            BillingQty = 0
            Amount = 0.00
            SgstUtgst = 0.00
            Cgst = 0.00
            Igst = 0.00
            Cess = 0.00
            Tax = 0.00
            CoreFlag = String.Empty
            Total = 0.00
            FileName = String.Empty
            SrcFileName = String.Empty
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
        Public Property UnqNo As String
        Public Property UploadUser As String
        Public Property UploadDate As String
        Public Property ShipToBranchCode As String
        Public Property ShipToBranch As String
        Public Property BillingDate As String
        Public Property InvoiceNo As String
        Public Property ItemNo As String
        Public Property DeliveryNo As String
        Public Property PoNo As String
        Public Property PoType As String
        Public Property PartNo As String
        Public Property HSNCode As String 'VJ 2019/10/17
        Public Property BillingQty As Int16
        Public Property Amount As Decimal
        Public Property SgstUtgst As Decimal
        Public Property Cgst As Decimal
        Public Property Igst As Decimal
        Public Property Cess As Decimal
        Public Property Tax As Decimal
        Public Property CoreFlag As String
        Public Property Total As Decimal
        Public Property FileName As String
        Public Property SrcFileName As String

    End Class
End Namespace
