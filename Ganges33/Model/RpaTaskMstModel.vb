Imports System
Imports System.Collections.Generic
Imports System.Configuration
Namespace Ganges33.model
    <Serializable>
    Public Class RpaTaskMstModel
        Public Sub RpaTaskMstModel()
            CRTDT = String.Empty
            CRTCD = String.Empty
            UPDDT = String.Empty
            UPDCD = String.Empty
            UPDPG = String.Empty
            DELFG = String.Empty
            TaskId = String.Empty
            TaskName = String.Empty
            SourceFile = String.Empty
            Path = String.Empty
            TestStatus = String.Empty
            Status = String.Empty
            RunDuration = String.Empty
            IpAddress = String.Empty
        End Sub

        Public Property CRTDT As String
        Public Property CRTCD As String
        Public Property UPDDT As String
        Public Property UPDCD As String
        Public Property UPDPG As String
        Public Property DELFG As String
        Public Property TaskId As String
        Public Property TaskName As String
        Public Property SourceFile As String
        Public Property Path As String
        Public Property TestStatus As String
        Public Property Status As String
        Public Property RunDuration As String
        Public Property IpAddress As String
    End Class

End Namespace