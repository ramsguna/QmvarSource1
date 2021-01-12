Imports System
Imports System.Collections.Generic
Namespace Ganges33.model
    ''' <summary>
    ''' User information set and get methods
    ''' </summary>
    <Serializable>
    Public Class UserInfoModel
        Public Sub UserInfoModel()
            CRTDT = String.Empty
            CRTCD = String.Empty
            UPDDT = String.Empty
            UPDCD = String.Empty
            UPDPG = String.Empty
            DELFG = String.Empty
            UserId = String.Empty
            Password = String.Empty
            EngId = String.Empty
            LastLogin = String.Empty
            AdminFlg = String.Empty
            UserLevel = String.Empty
            Ship1 = String.Empty
            Ship2 = String.Empty
            Ship3 = String.Empty
            Ship4 = String.Empty
            Ship5 = String.Empty
        End Sub

        Public Property CRTDT As String
        Public Property CRTCD As String
        Public Property UPDDT As String
        Public Property UPDCD As String
        Public Property UPDPG As String
        Public Property DELFG As String
        Public Property UserId As String
        Public Property Password As String
        Public Property EngId As String
        Public Property LastLogin As String
        Public Property AdminFlg As String
        Public Property UserLevel As String
        Public Property Ship1 As String
        Public Property Ship2 As String
        Public Property Ship3 As String
        Public Property Ship4 As String
        Public Property Ship5 As String

    End Class

End Namespace