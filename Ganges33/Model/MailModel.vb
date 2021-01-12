Imports System
Imports System.Collections.Generic
Imports System.Configuration
Namespace Ganges33.model
    ''' <summary>
    ''' Mail Compose Set & Get Methods
    ''' </summary>
    <Serializable>
    Public Class MailModel
        Public Sub New()
            FromMail = ConfigurationManager.AppSettings("Mail")
            ToMail = New List(Of String)()
            FileAttachment = New List(Of String)()
        End Sub
        Public Property FromMail As String
        Public Property ToMail As List(Of String)
        Public Property TitleMail As String
        Public Property BodyMail As String
        Public Property FileAttachment As List(Of String)
        Public Property SmtpMail As String
        Public Property PortMail As Integer
        Public Property CredentialsMail As String
        Public Property CredentialsPassword As String
        Public Property EnableSslMail As Boolean
    End Class
End Namespace
