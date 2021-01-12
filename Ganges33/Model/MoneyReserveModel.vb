Imports System
Imports System.Collections.Generic
Imports System.Configuration
Namespace Ganges33.model
    <Serializable>
    Public Class MoneyReserveModel
        Public Sub MoneyReserveModel()
            CRTDT = String.Empty
            CRTCD = String.Empty
            UPDDT = String.Empty
            UPDCD = String.Empty
            UPDPG = String.Empty
            DELFG = String.Empty
            Status = String.Empty
            UserName = String.Empty
            YouserName = String.Empty
            TDateTime = String.Empty
            M2000 = 0
            M1000 = 0
            M500 = 0
            M200 = 0
            M100 = 0
            M50 = 0
            M20 = 0
            M10 = 0
            C10 = 0
            C5 = 0
            C2 = 0
            C1 = 0
            Total = 0.00
            Diff = 0.00
            Reserve = 0.00
            ShipCode = String.Empty
            Mistake = 0
            IpAddress = String.Empty
            ConfUser = String.Empty
            ConfDateTime = String.Empty
            ConfIpAddress = String.Empty
            RegiDeposit = String.Empty
            UserLevel = String.Empty
            AdminFlg = String.Empty
            UserId = String.Empty
            OpenDate = String.Empty
            ClosingDate = String.Empty
            OpenTime = String.Empty
            ClosingTime = String.Empty
            PreviousDateClosing = False
            BRType = String.Empty
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
        Public Property M2000 As Integer
        Public Property M1000 As Integer
        Public Property M500 As Integer
        Public Property M200 As Integer
        Public Property M100 As Integer
        Public Property M50 As Integer
        Public Property M20 As Integer
        Public Property M10 As Integer
        Public Property C10 As Integer
        Public Property C5 As Integer
        Public Property C2 As Integer
        Public Property C1 As Integer
        Public Property Total As Decimal
        Public Property Diff As Decimal
        Public Property Reserve As Decimal
        Public Property ShipCode As String
        Public Property Mistake As Integer
        Public Property IpAddress As String
        Public Property ConfUser As String
        Public Property ConfDateTime As String
        Public Property ConfIpAddress As String
        Public Property RegiDeposit As String
        Public Property UserLevel As String
        Public Property AdminFlg As String
        Public Property UserId As String
        Public Property YouserName As String
        Public Property OpenDate As String
        Public Property ClosingDate As String
        Public Property OpenTime As String
        Public Property ClosingTime As String
        Public Property PreviousDateClosing As Boolean
        Public Property BRType As String
    End Class
End Namespace
