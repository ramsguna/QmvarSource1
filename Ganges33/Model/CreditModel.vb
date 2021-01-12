Imports System
Imports System.Collections.Generic
Imports System.Configuration
Public Class CreditModel

    Public Sub credit_Info()
        BRANCH_NO = String.Empty
        CREDIT_LIMIT = String.Empty
        PER_DAY = String.Empty
    End Sub
    Public Sub GST()
        ship_Name = String.Empty
        GSTIN = String.Empty

    End Sub
    Public Property BRANCH_NO As String
    Public Property CREDIT_LIMIT As String
    Public Property PER_DAY As String
    Public Property ship_Name As String
    Public Property GSTIN As String
End Class

