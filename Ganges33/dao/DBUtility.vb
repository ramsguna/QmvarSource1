Imports System
Imports System.Data
Imports System.Data.SqlClient
Imports System.Web.Configuration
Imports Ganges33.Ganges33.logic
Namespace Ganges33.dao
    ''' <summary>
    ''' Database Connectivity
    ''' </summary>
    Public Class DBUtility
        Public sqlConn As SqlConnection
        Public sqlCmd As SqlCommand
        Public sqlTrn As SqlTransaction

        Public Sub New()
            Log4NetControl.ComInfoLogWrite(Log4NetControl.UserID)
            Dim strConn As String = WebConfigurationManager.ConnectionStrings("cnstr").ToString()
            sqlConn = New SqlConnection(strConn)
            sqlConn.Open()
            sqlCmd = sqlConn.CreateCommand()
        End Sub

        Public Sub CloseConnection()
            Log4NetControl.ComInfoLogWrite(Log4NetControl.UserID)
            sqlConn.Close()
            sqlConn.Dispose()
        End Sub

        Public Function ExecSQL(ByVal strSql As String) As Boolean
            Log4NetControl.ComInfoLogWrite(Log4NetControl.UserID)
            sqlCmd.CommandText = strSql

            Try
                sqlCmd.ExecuteNonQuery()
            Catch ex As Exception
                Log4NetControl.ComErrorLogWrite(ex.ToString())
                Return False
            End Try

            Return True
        End Function

        Public Function ExecSQLRowAffected(ByVal strSql As String) As Integer
            Dim rowAffected As Integer = -99
            Log4NetControl.ComInfoLogWrite(Log4NetControl.UserID)
            sqlCmd.CommandText = strSql

            Try
                rowAffected = sqlCmd.ExecuteNonQuery()
            Catch ex As Exception
                Log4NetControl.ComErrorLogWrite(ex.ToString())
                Return rowAffected
            End Try

            Return rowAffected
        End Function

        Public Function ExecuteScalarSQL(ByVal strSql As String) As Integer
            Log4NetControl.ComInfoLogWrite(Log4NetControl.UserID)
            sqlCmd.CommandText = strSql
            Dim ID As Integer = 0

            Try
                ID = CInt(sqlCmd.ExecuteScalar())
            Catch ex As Exception
                Log4NetControl.ComErrorLogWrite(ex.ToString())
                ID = -99
            End Try

            Return ID
        End Function

        Public Function ExecuteScalarSQLStr(ByVal strSql As String) As String
            Log4NetControl.ComInfoLogWrite(Log4NetControl.UserID)
            sqlCmd.CommandText = strSql
            Dim StrTxt As String = String.Empty

            Try
                StrTxt = sqlCmd.ExecuteScalar().ToString()
            Catch ex As Exception
                Log4NetControl.ComErrorLogWrite(ex.ToString())
                StrTxt = "-99"
            End Try

            Return StrTxt
        End Function

        Public Function GetDataSet(ByVal strSql As String) As DataTable
            Log4NetControl.ComInfoLogWrite(Log4NetControl.UserID)
            Dim dt As DataTable = New DataTable()

            Try
                sqlCmd.CommandText = strSql
                Dim Datapter As SqlDataAdapter = New SqlDataAdapter(sqlCmd)
                Datapter.Fill(dt)
            Catch ex As Exception
                Log4NetControl.ComErrorLogWrite(ex.ToString())
                sqlConn.Close()
                Return Nothing
            End Try

            Return dt
        End Function

    End Class
End Namespace
