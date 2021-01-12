Imports System
Imports System.Collections.Generic
Imports System.Configuration
Namespace Ganges33.model
    <Serializable>
    Public Class RpaStatusModel
        Public Sub RpaStatusModel()
            CRTDT = String.Empty
            CRTCD = String.Empty
            UPDDT = String.Empty
            UPDCD = String.Empty
            UPDPG = String.Empty
            DELFG = String.Empty
            DateFrom = String.Empty
            DateTo = String.Empty
            ProcessId = String.Empty
            TaskName = String.Empty
            SchedulerName = String.Empty
        End Sub

        Public Property CRTDT As String
        Public Property CRTCD As String
        Public Property UPDDT As String
        Public Property UPDCD As String
        Public Property UPDPG As String
        Public Property DELFG As String
        Public Property DateFrom As String
        Public Property DateTo As String
        Public Property ProcessId As String
        Public Property TaskName As String
        Public Property SchedulerName As String

    End Class
End Namespace
