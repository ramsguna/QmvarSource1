Imports System
Imports System.Configuration
Imports System.Net.Mail
Imports Ganges33.Ganges33.model
Namespace Ganges33.logic
    ''' <summary>
    ''' Email Compose
    ''' </summary>
    Public Class MailControl
        Public Function SendMail(ByVal mailInfo As MailModel) As Boolean
            Dim mailMessage As MailMessage = New MailMessage()
            Dim smtpClient As SmtpClient = New SmtpClient()
            Try
                mailMessage.From = New MailAddress(mailInfo.FromMail)
                For i As Integer = 0 To mailInfo.ToMail.Count - 1
                    mailMessage.[To].Add(mailInfo.ToMail(i).ToString())
                Next
                mailMessage.Subject = mailInfo.TitleMail
                mailMessage.IsBodyHtml = False
                mailMessage.Body = mailInfo.BodyMail
                If mailInfo.FileAttachment IsNot Nothing Then
                    For i As Integer = 0 To mailInfo.FileAttachment.Count - 1
                        mailMessage.Attachments.Add(New Attachment(mailInfo.FileAttachment(i).ToString()))
                    Next
                End If
                If Not String.IsNullOrEmpty(mailInfo.SmtpMail) Then
                    smtpClient.Host = mailInfo.SmtpMail
                Else
                    smtpClient.Host = ConfigurationManager.AppSettings("Smtp")
                End If
                If mailInfo.PortMail <> 0 Then
                    smtpClient.Port = mailInfo.PortMail
                Else
                    smtpClient.Port = Integer.Parse(ConfigurationManager.AppSettings("SmtpPort"))
                End If
                smtpClient.UseDefaultCredentials = False
                If (mailInfo.CredentialsMail IsNot Nothing) AndAlso (mailInfo.CredentialsPassword IsNot Nothing) Then
                    smtpClient.Credentials = New System.Net.NetworkCredential(mailInfo.CredentialsMail, mailInfo.CredentialsPassword)
                Else
                    smtpClient.Credentials = New System.Net.NetworkCredential(ConfigurationManager.AppSettings("Mail"), ConfigurationManager.AppSettings("Password"))
                End If
                smtpClient.EnableSsl = True
                smtpClient.Timeout = 30000
                smtpClient.Send(mailMessage)
            Catch ex As Exception
                Return False
            End Try
            Return True
        End Function
    End Class
End Namespace