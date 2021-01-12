Imports System
Imports System.Collections.Generic
Imports System.Configuration

Namespace Ganges33.model
    <Serializable>
    Public Class CashOnSalesInvoiceCreateModel
        Public Sub CashOnSalesInvoiceCreateModel()
            CorpPoNo = String.Empty
            ClaimNo = String.Empty

        End Sub

        Public Property CorpPoNo As String
        Public Property ClaimNo As String

    End Class
End Namespace
