Imports System
Imports System.Collections.Generic
Imports System.Configuration
Namespace Ganges33.model
    <Serializable>
    Public Class RpaOnOffModel
        Public Sub RpaOnOffModel()
            CRTDT = String.Empty
            CRTCD = String.Empty
            UPDDT = String.Empty
            UPDCD = String.Empty
            UPDPG = String.Empty
            DELFG = String.Empty

            ShipCode = String.Empty
            ShipName = String.Empty
            RpaClientUserId = String.Empty
            RpaClientPwd = String.Empty
        End Sub

        Public Property CRTDT As String
        Public Property CRTCD As String
        Public Property UPDDT As String
        Public Property UPDCD As String
        Public Property UPDPG As String
        Public Property DELFG As String

        Public Property ShipCode As String
        Public Property ShipName As String
        Public Property RpaClientUserId As String
        Public Property RpaClientPwd As String
    End Class
End Namespace