Imports System
Imports System.Collections.Generic
Imports System.Configuration
Namespace Ganges33.model
    <Serializable>
    Public Class RpaEmailModel
        Public Sub RpaEmailModel()
            CRTDT = String.Empty
            CRTCD = String.Empty
            UPDDT = String.Empty
            UPDCD = String.Empty
            UPDPG = String.Empty
            DELFG = String.Empty

            UserId = String.Empty
            EmailId = String.Empty
            EmailType = String.Empty
            Smtp = String.Empty
            SmtpPort = String.Empty
            SmtpSslEnable = String.Empty
            SmtpCredentialsUserName = String.Empty
            SmtpCredentialsUserPassword = String.Empty

            Sender = String.Empty
            EmailTo = String.Empty
            EmailCc = String.Empty
            EmailBcc = String.Empty
            Source = String.Empty
            Status = String.Empty
            IpAddress = String.Empty


        End Sub

        Public Property CRTDT As String
        Public Property CRTCD As String
        Public Property UPDDT As String
        Public Property UPDCD As String
        Public Property UPDPG As String
        Public Property DELFG As String
        Public Property UserId As String

        Public Property EmailId As String
        Public Property EmailType As String
        Public Property Smtp As String
        Public Property SmtpPort As String
        Public Property SmtpSslEnable As String
        Public Property SmtpCredentialsUserName As String
        Public Property SmtpCredentialsUserPassword As String

        Public Property Sender As String
        Public Property EmailTo As String
        Public Property EmailCc As String
        Public Property EmailBcc As String
        Public Property Source As String
        Public Property Status As String

        Public Property IpAddress As String


    End Class
End Namespace
