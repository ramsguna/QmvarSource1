Imports System
Imports System.Data
Imports System.Web.Security
Imports System.Collections.Generic
Imports System.Security.Cryptography
Imports System.IO
Imports System.Text
Imports Ganges33.Ganges33.dao
Imports Ganges33.Ganges33.model
Imports Ganges33.Ganges33.logic
Public Class RpaTaskAppControl
    Public Function SelectRpaTaskApp() As DataTable
        Log4NetControl.ComInfoLogWrite(Log4NetControl.UserID)
        Dim dbConn As DBUtility = New DBUtility()
        Dim dt As DataTable = New DataTable()
        Dim sqlStr As String = "SELECT "
        sqlStr = sqlStr & " ship_name, ship_code, RpaClientUserId, RpaClientPwd "
        sqlStr = sqlStr & "FROM "
        sqlStr = sqlStr & "M_ship_base WHERE DELFG=0 AND ship_name like 'SSC%' or ship_name like 'SID%'"

        Dim _DataTable As DataTable = dbConn.GetDataSet(sqlStr)
        dbConn.CloseConnection()
        Return _DataTable

    End Function


    Public Function UpdateRpaClientUser(RpaClient As List(Of RpaClientUserModel)) As Boolean
        Log4NetControl.ComInfoLogWrite(Log4NetControl.UserID)

        Dim sqlStr As String = ""
        Dim flag As Boolean = True
        Dim flagAll As Boolean = True

        Dim dbConn As DBUtility = New DBUtility()
        Dim dt As DataTable = New DataTable()
        dbConn.sqlTrn = dbConn.sqlConn.BeginTransaction()
        dbConn.sqlCmd.Transaction = dbConn.sqlTrn

        Dim i As Integer = 0
        Dim strEncryPwd As String = ""

        For i = 0 To RpaClient.Count - 1

            sqlStr = "Update M_ship_base set  "
            If Not String.IsNullOrEmpty(RpaClient.Item(i).GspnUserName) Then
                sqlStr = sqlStr & "RpaClientUserId = @GspnUserName, "
                dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@GspnUserName", RpaClient.Item(i).GspnUserName))
            End If
            If Not String.IsNullOrEmpty(RpaClient.Item(i).GspnPwd) Then
                strEncryPwd = Encrypt(RpaClient.Item(i).GspnPwd)
                sqlStr = sqlStr & "RpaClientPwd = @GspnPwd "
                dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@GspnPwd", strEncryPwd))
            End If
            sqlStr = sqlStr & "WHERE "
            If Not String.IsNullOrEmpty(RpaClient.Item(i).ShipCode) Then
                sqlStr = sqlStr & "ship_code = @ShipCode "
                dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@ShipCode", RpaClient.Item(i).ShipCode))
            End If
            If Not String.IsNullOrEmpty(RpaClient.Item(i).ShipName) Then
                sqlStr = sqlStr & "AND ship_name = @ShipName "
                dbConn.sqlCmd.Parameters.Add(CommonControl.GetNullableParameter("@ShipName", RpaClient.Item(i).ShipName))
            End If
            flag = dbConn.ExecSQL(sqlStr)
            dbConn.sqlCmd.Parameters.Clear()
            'If Error occurs then will store the flag as false
            If Not flag Then
                flagAll = False
                Exit For
            End If

        Next

        If flagAll Then
            flag = True
            dbConn.sqlTrn.Commit()
        Else
            flag = False
            dbConn.sqlTrn.Rollback()
        End If
        dbConn.CloseConnection()
        Return flag
    End Function

    ' Keys required for Symmetric encryption / decryption
    Dim rijnKey As Byte() = {&H1, &H2, &H3, &H4, &H5, &H6, &H7, &H8, &H9, &H10, &H11, &H12, &H13, &H14, &H15, &H16}
    Dim rijnIV As Byte() = {&H1, &H2, &H3, &H4, &H5, &H6, &H7, &H8, &H9, &H10, &H11, &H12, &H13, &H14, &H15, &H16}
    Function Decrypt(S As String)
        If S = "" Then
            Return S
        End If
        ' Turn the cipherText into a ByteArray from Base64
        Dim cipherText As Byte()
        Try
            ' Replace any + that will lead to the error
            cipherText = Convert.FromBase64String(S.Replace("GSSJ20200513GSSJ", "+"))  '=>>>>   "BIN00101011BIN"
        Catch ex As Exception
            ' There is a problem with the string, perhaps it has bad base64 padding
            Return S
        End Try
        'Creates the default implementation, which is RijndaelManaged.
        Dim rijn As SymmetricAlgorithm = SymmetricAlgorithm.Create()
        Try
            ' Create the streams used for decryption.
            Using msDecrypt As New MemoryStream(cipherText)
                Using csDecrypt As New CryptoStream(msDecrypt, rijn.CreateDecryptor(rijnKey, rijnIV), CryptoStreamMode.Read)
                    Using srDecrypt As New StreamReader(csDecrypt)
                        ' Read the decrypted bytes from the decrypting stream and place them in a string.
                        S = srDecrypt.ReadToEnd()
                    End Using
                End Using
            End Using
        Catch E As CryptographicException
            Return S
        End Try
        Return S
    End Function

    Function Encrypt(S As String)
        'Creates the default implementation, which is RijndaelManaged.
        Dim rijn As SymmetricAlgorithm = SymmetricAlgorithm.Create()
        Dim encrypted() As Byte
        Using msEncrypt As New MemoryStream()
            Dim csEncrypt As New CryptoStream(msEncrypt, rijn.CreateEncryptor(rijnKey, rijnIV), CryptoStreamMode.Write)
            Using swEncrypt As New StreamWriter(csEncrypt)
                'Write all data to the stream.
                swEncrypt.Write(S)
            End Using
            encrypted = msEncrypt.ToArray()
        End Using
        ' You cannot convert the byte to a string or you will get strange characters so base64 encode the string
        ' Replace any + that will lead to the error
        Return Convert.ToBase64String(encrypted).Replace("+", "GSSJ20200513GSSJ")  '===>>>>  "BIN00101011BIN"
    End Function

End Class
