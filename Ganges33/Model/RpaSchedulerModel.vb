Imports System
Imports System.Collections.Generic
Imports System.Configuration
Namespace Ganges33.model
    <Serializable>
    Public Class RpaSchedulerModel

        Public Sub RpaSchedulerModel()
            CRTDT = String.Empty
            CRTCD = String.Empty
            UPDDT = String.Empty
            UPDCD = String.Empty
            UPDPG = String.Empty
            DELFG = String.Empty

            Status = String.Empty
            LastRunDateTime = String.Empty
            LastRunStatus = String.Empty
            TaskName = String.Empty
            SchedulerName = String.Empty
            RpaSchId = String.Empty

            TaskSource = String.Empty
            StartDateTime = String.Empty
            EndDateTime = String.Empty
            RecurringType = String.Empty
            RepeatTime = String.Empty
            BatchFile = String.Empty


        End Sub

        Public Property CRTDT As String
        Public Property CRTCD As String
        Public Property UPDDT As String
        Public Property UPDCD As String
        Public Property UPDPG As String
        Public Property DELFG As String

        Public Property Status As String
        Public Property LastRunDateTime As String
        Public Property LastRunStatus As String
        Public Property TaskName As String
        Public Property SchedulerName As String
        Public Property RpaSchId As String

        Public Property TaskSource As String
        Public Property StartDateTime As String
        Public Property EndDateTime As String
        Public Property RecurringType As String
        Public Property RepeatTime As String
        Public Property BatchFile As String

    End Class
End Namespace

