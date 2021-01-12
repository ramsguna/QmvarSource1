Imports System
Imports System.Collections.Generic
Imports System.Configuration
Namespace Ganges33.model
    <Serializable>
    Public Class InvoiceUpdateModel
        Public Sub InvoiceUpdateModel()
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

            UnqNo = String.Empty
            UploadUser = String.Empty
            UploadDate = String.Empty
            ShipToBranchCode = String.Empty
            ShipToBranch = String.Empty
            SamsungRefNo = String.Empty

            YourRefNo = String.Empty
            Model = String.Empty
            Serial = String.Empty
            Product = String.Empty
            Service = String.Empty
            DefactCode = String.Empty
            Currency = String.Empty

            Invoice = 0.00
            Labor = 0.00
            Parts = 0.00
            Freight = 0.00
            Other = 0.00
            Tax = 0.00

            FileName = String.Empty
            LaborInvoiceNo = String.Empty
            InvoiceDate = String.Empty
            ReportNumber = String.Empty
            SrcFileName = String.Empty

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
        Public Property Status As String
        Public Property UserName As String
        Public Property TDateTime As String
        Public Property UserId As String
        Public Property UnqNo As String
        Public Property UploadUser As String
        Public Property UploadDate As String
        Public Property ShipToBranchCode As String
        Public Property ShipToBranch As String
        Public Property SamsungRefNo As String
        Public Property YourRefNo As String
        Public Property Model As String
        Public Property Serial As String
        Public Property Product As String
        Public Property Service As String
        Public Property DefactCode As String
        Public Property Currency As String
        Public Property Invoice As Decimal
        Public Property Labor As Decimal
        Public Property Parts As Decimal
        Public Property Freight As Decimal
        Public Property Other As Decimal
        Public Property Tax As Decimal
        Public Property FileName As String
        Public Property LaborInvoiceNo As String
        Public Property InvoiceDate As String
        Public Property ReportNumber As String
        Public Property SrcFileName As String

        Public Property DateFrom As String
        Public Property DateTo As String

        Public Property Number As String

    End Class

End Namespace




