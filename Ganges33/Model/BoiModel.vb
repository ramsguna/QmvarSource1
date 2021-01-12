Imports System
Imports System.Collections.Generic
Imports System.Configuration
Namespace Ganges33.model
    <Serializable>
    Public Class BoiModel
        Public Sub BoiModel()
            CRTDT = String.Empty
            CRTCD = String.Empty
            UPDDT = String.Empty
            UPDCD = String.Empty
            UPDPG = String.Empty
            DELFG = String.Empty

            SHIP_TO_BRANCH_CODE = String.Empty
            PAYMENT_DATE = String.Empty
            AMOUNT = String.Empty
            SHIP_TO = String.Empty
            DOC_NO = String.Empty
            UserId = String.Empty
            AR_NO = String.Empty
            DateFrom = String.Empty
            DateTo = String.Empty
            FileName = String.Empty
        End Sub


        Public Property CRTDT As String
        Public Property CRTCD As String
        Public Property UPDDT As String
        Public Property UPDCD As String
        Public Property UPDPG As String
        Public Property DELFG As String

        Public Property SHIP_TO_BRANCH_CODE As String
        Public Property SHIP_TO_BRANCH As String
        Public Property PAYMENT_DATE As String
        Public Property AMOUNT As String
        Public Property SHIP_TO As String
        Public Property DOC_NO As String
        Public Property AR_NO As String
        Public Property UserId As String
        Public Property DateFrom As String
        Public Property DateTo As String
        Public Property FileName As String
    End Class
End Namespace
