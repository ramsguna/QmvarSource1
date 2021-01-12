Imports System
Imports System.Collections.Generic
Imports System.Configuration
Namespace Ganges33.model
    <Serializable>
    Public Class SLedgerModel
        Public Sub SLedgerModel()
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
            ShipToBranchCode = String.Empty
            ShipToBranch = String.Empty
            UploadUser = String.Empty
            UploadDate = String.Empty
            DocNo = String.Empty
            Items = 0
            DocumentDate = String.Empty
            PostingDate = String.Empty
            DocType = String.Empty
            InvoiceNo = String.Empty
            Curr = String.Empty
            DebitAmount = 0.00
            CreditAmount = 0.00
            ClosingBalance = 0.00
            Assignment = String.Empty
            Text = String.Empty
            Ba = String.Empty
            Segment = String.Empty
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
        Public Property ShipToBranchCode As String
        Public Property ShipToBranch As String
        Public Property UploadUser As String
        Public Property UploadDate As String
        Public Property DocNo As String
        Public Property Items As Int16
        Public Property DocumentDate As String
        Public Property PostingDate As String
        Public Property DocType As String
        Public Property InvoiceNo As String
        Public Property Curr As String
        Public Property DebitAmount As Decimal
        Public Property CreditAmount As Decimal
        Public Property ClosingBalance As Decimal
        Public Property Assignment As String
        Public Property Text As String
        Public Property Ba As String
        Public Property Segment As String
        Public Property FileName As String
        Public Property SrcFileName As String

    End Class
End Namespace
