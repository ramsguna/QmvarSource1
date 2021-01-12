Imports System
Imports System.Collections.Generic
Imports System.Configuration

Namespace Ganges33.model
    <Serializable>
    Public Class CustodyKeepModel
        Public Sub CustodyKeepModel()
            KeepNo = String.Empty
            UpdateUser = String.Empty
        End Sub
        Public Property KeepNo As String
        Public Property UpdateUser As String
    End Class

End Namespace
