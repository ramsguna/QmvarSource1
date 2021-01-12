
Imports System
Imports System.Collections.Generic
Imports System.Configuration
Namespace Ganges33.model

    <Serializable>
    Public Class CashOnSaleModel


        Public Sub CashOnSaleModel()
            CRTDT = String.Empty
            CRTCD = String.Empty
            UPDDT = String.Empty
            UPDCD = String.Empty
            UPDPG = String.Empty
            DELFG = String.Empty
            ServiceOrderNo = String.Empty
            CorpNumber = String.Empty
            CorpFlg = False
            CorpCollect = False
            CorpPo = String.Empty
            EditLog = String.Empty
            InvoiceDate = String.Empty
            PaymentDate = String.Empty
            Location = String.Empty
            ClaimNo = String.Empty

            DateFrom = String.Empty
            DateTo = String.Empty
            CustomerName = String.Empty
            TotalAmt = 0.00

            CorpNumbers = String.Empty
            CorpCollects = String.Empty
        End Sub

        Public Property CRTDT As String
        Public Property CRTCD As String
        Public Property UPDDT As String
        Public Property UPDCD As String
        Public Property UPDPG As String
        Public Property DELFG As String
        Public Property ServiceOrderNo As String
        Public Property CorpNumber As String
        Public Property CorpFlg As Boolean
        Public Property CorpCollect As Boolean
        Public Property CorpPo As String
        Public Property EditLog As String
        Public Property InvoiceDate As String
        Public Property PaymentDate As String
        Public Property Location As String
        Public Property ClaimNo As String

        Public Property DateFrom As String
        Public Property DateTo As String
        Public Property CustomerName As String
        Public Property TotalAmt As Decimal

        Public Property CorpNumbers As String
        Public Property CorpCollects As String
    End Class
End Namespace
