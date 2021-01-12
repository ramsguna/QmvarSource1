Imports System
Imports System.Collections.Generic
Imports System.Configuration
Namespace Ganges33.model
    <Serializable>
    Public Class ReturnCreditModel
        Public Sub ReturnCreditModel()
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
            InvoiceDate = String.Empty
            InvoiceNo = String.Empty
            LocalInvoiceNo = String.Empty
            DeliveryNo = String.Empty
            Items = 0
            Amount = 0.00
            SgstUtgst = 0.00
            Cgst = 0.00
            Igst = 0.00
            Cess = 0.00
            Tax = 0.00
            Total = 0.00
            GrStatus = String.Empty
            FileName = String.Empty
            SrcFileName = String.Empty

            DateFrom = String.Empty
            DateTo = String.Empty

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
        Public Property UploadUser As String
        Public Property UploadDate As String
        Public Property ShipToBranchCode As String
        Public Property ShipToBranch As String
        Public Property InvoiceDate As String
        Public Property InvoiceNo As String
        Public Property LocalInvoiceNo As String
        Public Property DeliveryNo As String
        Public Property Items As Int16
        Public Property Amount As Decimal
        Public Property SgstUtgst As Decimal
        Public Property Cgst As Decimal
        Public Property Igst As Decimal
        Public Property Cess As Decimal
        Public Property Tax As Decimal
        Public Property Total As Decimal
        Public Property GrStatus As String
        Public Property FileName As String
        Public Property SrcFileName As String
        Public Property DateFrom As String
        Public Property DateTo As String
    End Class
End Namespace

