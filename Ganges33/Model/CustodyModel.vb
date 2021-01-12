Imports System
Imports System.Collections.Generic
Imports System.Configuration
Namespace Ganges33.model
    <Serializable>
    Public Class CustodyModel

        Public Sub CashOnSaleModel()
            CRTDT = String.Empty
            CRTCD = String.Empty
            UPDDT = String.Empty
            UPDCD = String.Empty
            UPDPG = String.Empty
            DELFG = String.Empty

            KeepNo = String.Empty
            CustomerName = String.Empty
            CustomerTel = String.Empty
            SamsungClaimNo = String.Empty
            ProductName = String.Empty
            Cash = 0.00
            ShipCode = String.Empty
            TakeOut = String.Empty

            UpdateUser = String.Empty
            UserId = String.Empty
            UserName = String.Empty

        End Sub

        Public Property CRTDT As String
        Public Property CRTCD As String
        Public Property UPDDT As String
        Public Property UPDCD As String
        Public Property UPDPG As String
        Public Property DELFG As String

        Public Property KeepNo As String
        Public Property CustomerName As String
        Public Property CustomerTel As String
        Public Property SamsungClaimNo As String
        Public Property ProductName As String
        Public Property Cash As Decimal
        Public Property ShipCode As String
        Public Property TakeOut As Int16

        Public Property UpdateUser As String
        Public Property UserId As String
        Public Property UserName As String
    End Class
End Namespace

