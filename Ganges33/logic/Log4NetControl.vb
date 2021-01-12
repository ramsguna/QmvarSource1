Imports log4net
Imports System.Reflection
Imports System.Diagnostics
Namespace Ganges33.logic
    ''' <summary>
    ''' User activity and error capture log task
    ''' </summary>
    Public Class Log4NetControl
        Public Shared UserID As String = String.Empty

        Public Shared Sub ComInfoLogWrite(ByVal userID As Object)
            Dim testAs As String = ""
            Try
                Dim log As ILog = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType)
                Dim m As MethodBase = (New StackTrace(False)).GetFrame(1).GetMethod()
                log.Info(System.Web.HttpContext.Current.Request.UserHostAddress & ": " & m.DeclaringType.Name & "." & m.Name & " UserName:" & (If(userID IsNot Nothing, userID.ToString(), String.Empty)))
            Catch ex As Exception
                testAs = ex.Message
            End Try
        End Sub

        Public Shared Sub ComWarnLogWrite(ByVal userID As Object, ByVal Optional msg As String = "")
            Try
                Dim log As ILog = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType)
                Dim m As MethodBase = (New StackTrace(False)).GetFrame(1).GetMethod()
                log.Info(System.Web.HttpContext.Current.Request.UserHostAddress & ": " & m.DeclaringType.Name & "." & m.Name & " UserName:" & (If(userID IsNot Nothing, userID.ToString(), String.Empty)))
                log.WarnFormat("{0}.{1} UserName:{2} Message:{3}", m.DeclaringType.Name, m.Name, (If(userID IsNot Nothing, userID.ToString(), String.Empty)), msg)
            Catch
            End Try
        End Sub

        Public Shared Sub ComErrorLogWrite(ByVal msg As String)
            Try
                Dim log As ILog = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType)
                Dim m As MethodBase = (New StackTrace(False)).GetFrame(1).GetMethod()
                log.ErrorFormat("{0}.{1} Message:{2} ", m.DeclaringType.Name, m.Name, msg)
            Catch
            End Try
        End Sub
    End Class
End Namespace
