'VJ 2019/10/11
Imports System
Imports System.Collections.Generic
Imports System.Configuration
Namespace Ganges33.model
    <Serializable>
    Public Class DebitNoteRegisterModel
        Public Sub DebitNoteRegisterModel()
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
            MonthUnqNo = 0
            DebitNoteDate = String.Empty
            BillingDate = String.Empty
            RefInvoiceDate = String.Empty
            CreditReqNo = String.Empty
            PartCode = String.Empty
            PurchaseRefNo = String.Empty
            VoucherRef = String.Empty
            Quantity = 0
            Rate = 0
            ShipToBranchCode = String.Empty
            ShipToBranch = String.Empty
            FileName = String.Empty
            SrcFileName = String.Empty
        End Sub
        Public Property CRTDT As String
        Public Property CRTCD As String
        Public Property UPDDT As String
        Public Property UPDCD As String
        Public Property UPDPG As String
        Public Property DELFG As String
        Public Property UnqNo As String
        Public Property Status As String
        Public Property UserName As String
        Public Property TDateTime As String
        Public Property UserId As String
        Public Property UploadUser As String
        Public Property UploadDate As String
        Public Property MonthUnqNo As Int16
        Public Property DebitNoteDate As String
        Public Property BillingDate As String
        Public Property RefInvoiceDate As String
        Public Property CreditReqNo As String
        Public Property PartCode As String
        Public Property PurchaseRefNo As String
        Public Property VoucherRef As String
        Public Property Quantity As Int16
        Public Property Rate As Decimal
        Public Property ShipToBranchCode As String
        Public Property ShipToBranch As String
        Public Property FileName As String
        Public Property SrcFileName As String
    End Class
End Namespace