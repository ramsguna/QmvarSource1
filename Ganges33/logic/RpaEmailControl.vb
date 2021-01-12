Imports Ganges33.Ganges33.dao
Imports Ganges33.Ganges33.model
Imports Ganges33.Ganges33.logic
Public Class RpaEmailControl
    Public Function SelectRpaEmail(ByVal queryParams As RpaEmailModel) As DataTable
        Log4NetControl.ComInfoLogWrite(Log4NetControl.UserID)
        Dim dbConn As DBUtility = New DBUtility()
        Dim dt As DataTable = New DataTable()
        Dim sqlStr As String = "SELECT "
        sqlStr = sqlStr & " * "
        sqlStr = sqlStr & "FROM "
        sqlStr = sqlStr & "RPA_EMAIL_MST WHERE (DELFG IS NULL OR DELFG=0)  "
        If Not String.IsNullOrEmpty(queryParams.EmailType) Then
            sqlStr = sqlStr & "AND EMAIL_TYPE=@EmailType "
            dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@EmailType", queryParams.EmailType))
        End If
        Dim _DataTable As DataTable = dbConn.GetDataSet(sqlStr)
        dbConn.CloseConnection()
        Return _DataTable

    End Function

    Public Function RpaEmailUpdate(ByVal queryParams As RpaEmailModel) As Boolean
        Log4NetControl.ComInfoLogWrite(Log4NetControl.UserID)
        Dim flag As Boolean = True
        Dim DateTimeNow As DateTime = DateTime.Now
        Dim dtNow As DateTime = DateTimeNow.AddMinutes(ConfigurationManager.AppSettings("TimeDiff"))

        Dim dbConn As DBUtility = New DBUtility()
        Dim dt As DataTable = New DataTable()
        Dim sqlStr As String = "UPDATE RPA_EMAIL_MST "
        sqlStr = sqlStr & "set  "

        '  If Not String.IsNullOrEmpty(queryParams.UPDCD) Then
        sqlStr = sqlStr & "UPDCD = @UPDCD, "
            dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@UPDCD", queryParams.UserId))
        ' End If

        ' If Not String.IsNullOrEmpty(queryParams.UPDDT) Then
        sqlStr = sqlStr & "UPDDT = @UPDDT, "
            dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@UPDDT", dtNow))
        ' End If

        If Not String.IsNullOrEmpty(queryParams.EmailType) Then
            sqlStr = sqlStr & "EMAIL_TYPE = @EmailType, "
            dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@EmailType", queryParams.EmailType))
        End If

        If Not String.IsNullOrEmpty(queryParams.Smtp) Then
            sqlStr = sqlStr & "SMTP = @Smtp, "
            dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@Smtp", queryParams.Smtp))
        End If

        If Not String.IsNullOrEmpty(queryParams.SmtpPort) Then
            sqlStr = sqlStr & "SMTP_PORT = @SmtpPort, "
            dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@SmtpPort", queryParams.SmtpPort))
        End If

        If Not String.IsNullOrEmpty(queryParams.SmtpSslEnable) Then
            sqlStr = sqlStr & "SMTP_SSL_ENABLE = @SmtpSslEnable, "
            dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@SmtpSslEnable", queryParams.SmtpSslEnable))
        End If

        If Not String.IsNullOrEmpty(queryParams.SmtpCredentialsUserName) Then
            sqlStr = sqlStr & "SMTP_CREDENTIALS_USER_NAME = @SmtpCredentialsUserName, "
            dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@SmtpCredentialsUserName", queryParams.SmtpCredentialsUserName))
        End If


        If Not String.IsNullOrEmpty(queryParams.SmtpCredentialsUserPassword) Then
            sqlStr = sqlStr & "SMTP_CREDENTIALS_USER_PASSWORD = @SmtpCredentialsUserPassword, "
            dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@SmtpCredentialsUserPassword", queryParams.SmtpCredentialsUserPassword))
        End If


        If Not String.IsNullOrEmpty(queryParams.Sender) Then
            sqlStr = sqlStr & "SENDER = @Sender, "
            dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@Sender", queryParams.Sender))
        End If

        If Not String.IsNullOrEmpty(queryParams.EmailTo) Then
            sqlStr = sqlStr & "EMAIL_TO = @EmailTo, "
            dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@EmailTo", queryParams.EmailTo))
        End If

        'Email cc and bcc is not mandatory
        ' If Not String.IsNullOrEmpty(queryParams.EmailCc) Then
        sqlStr = sqlStr & "EMAIL_CC = @EmailCc, "
            dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@EmailCc", queryParams.EmailCc))
        '  End If

        ' If Not String.IsNullOrEmpty(queryParams.EmailBcc) Then
        sqlStr = sqlStr & "EMAIL_BCC = @EmailBcc, "
            dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@EmailBcc", queryParams.EmailBcc))
        ' End If

        If Not String.IsNullOrEmpty(queryParams.Status) Then
            sqlStr = sqlStr & "STATUS = @Status, "
            dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@Status", queryParams.Status))
        End If

        If Not String.IsNullOrEmpty(queryParams.Source) Then
            sqlStr = sqlStr & "SOURCE = @Source "
            dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@Source", queryParams.Source))
        End If

        If Not String.IsNullOrEmpty(queryParams.EmailId) Then
            sqlStr = sqlStr & "WHERE EMAILID = @EmailId "
            dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@EmailId", queryParams.EmailId))
        End If

        flag = dbConn.ExecSQL(sqlStr)
        dbConn.CloseConnection()
        Return flag
    End Function

    Public Function AddEmail(queryParams As RpaEmailModel) As Boolean
        Dim sqlStr As String = ""
        Dim flag As Boolean = True
        Dim DateTimeNow As DateTime = DateTime.Now
        Dim dtNow As DateTime = DateTimeNow.AddMinutes(ConfigurationManager.AppSettings("TimeDiff"))

        Dim dbConn As DBUtility = New DBUtility()
        Dim dt As DataTable = New DataTable()
        dbConn.sqlTrn = dbConn.sqlConn.BeginTransaction()
        dbConn.sqlCmd.Transaction = dbConn.sqlTrn
        sqlStr = "Insert into RPA_EMAIL_MST ("
        sqlStr = sqlStr & "CRTDT, "
        sqlStr = sqlStr & "CRTCD, "
        ' sqlStr = sqlStr & "UPDDT, "
        sqlStr = sqlStr & "UPDCD, "
        sqlStr = sqlStr & "UPDPG, "
        sqlStr = sqlStr & "DELFG, "

        sqlStr = sqlStr & "EMAIL_TYPE, "
        sqlStr = sqlStr & "SMTP, "
        sqlStr = sqlStr & "SMTP_PORT, "
        sqlStr = sqlStr & "SMTP_SSL_ENABLE, "
        sqlStr = sqlStr & "SMTP_CREDENTIALS_USER_NAME, "
        sqlStr = sqlStr & "SMTP_CREDENTIALS_USER_PASSWORD, "
        sqlStr = sqlStr & "SENDER, "
        sqlStr = sqlStr & "EMAIL_TO, "
        sqlStr = sqlStr & "EMAIL_CC, "
        sqlStr = sqlStr & "EMAIL_BCC, "
        sqlStr = sqlStr & "SOURCE, "
        sqlStr = sqlStr & "STATUS, "
        sqlStr = sqlStr & "IP_ADDRESS "
        sqlStr = sqlStr & " ) "
        sqlStr = sqlStr & " values ( "
        sqlStr = sqlStr & "@CRTDT, "
        sqlStr = sqlStr & "@CRTCD, "
        'sqlStr = sqlStr & "@UPDDT, "
        sqlStr = sqlStr & "@UPDCD, "
        sqlStr = sqlStr & "@UPDPG, "
        sqlStr = sqlStr & "@DELFG, "

        sqlStr = sqlStr & "@EmailType, "
        sqlStr = sqlStr & "@Smtp, "
        sqlStr = sqlStr & "@SmtpPort, "
        sqlStr = sqlStr & "@SmtpSslEnable, "
        sqlStr = sqlStr & "@SmtpCredentialsUserName, "
        sqlStr = sqlStr & "@SmtpCredentialsUserPassword, "
        sqlStr = sqlStr & "@Sender,"
        sqlStr = sqlStr & "@EmailTo,"
        sqlStr = sqlStr & "@EmailCc,"
        sqlStr = sqlStr & "@EmailBcc,"
        sqlStr = sqlStr & "@Source,"
        sqlStr = sqlStr & "@Status,"
        sqlStr = sqlStr & "@IpAddress"
        sqlStr = sqlStr & " ) "
        dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@CRTDT", dtNow))
        dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@CRTCD", queryParams.UserId))
        dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@UPDCD", "")) '?????????????????????????
        dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@UPDPG", queryParams.UPDPG))
        dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@DELFG", 0))

        dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@EmailType", queryParams.EmailType))
        dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@Smtp", queryParams.Smtp))
        dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@SmtpPort", queryParams.SmtpPort))
        dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@SmtpSslEnable", queryParams.SmtpSslEnable))
        dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@SmtpCredentialsUserName", queryParams.SmtpCredentialsUserName))
        dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@SmtpCredentialsUserPassword", queryParams.SmtpCredentialsUserPassword))
        dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@Sender", queryParams.Sender))
        dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@EmailTo", queryParams.EmailTo))
        dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@EmailCc", queryParams.EmailCc))
        dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@EmailBcc", queryParams.EmailBcc))
        dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@Source", queryParams.Source))
        dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@Status", queryParams.Status))
        dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@IpAddress", queryParams.IpAddress))

        flag = dbConn.ExecSQL(sqlStr)
        dbConn.sqlCmd.Parameters.Clear()
        If flag Then
            dbConn.sqlTrn.Commit()
        Else
            dbConn.sqlTrn.Rollback()
        End If
        dbConn.CloseConnection()
        Return flag
    End Function

    Public Function RpaEmailDelete(ByVal queryParams As RpaEmailModel) As Boolean
        Log4NetControl.ComInfoLogWrite(Log4NetControl.UserID)
        Dim flag As Boolean = True
        Dim dbConn As DBUtility = New DBUtility()
        Dim dt As DataTable = New DataTable()
        Dim sqlStr As String = "DELETE FROM RPA_EMAIL_MST WHERE EMAILID=@EmailId"

        If Not String.IsNullOrEmpty(queryParams.EmailId) Then
            dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@EmailId", queryParams.EmailId))
        End If
        flag = dbConn.ExecSQL(sqlStr)
        dbConn.CloseConnection()
        Return flag
    End Function
End Class
