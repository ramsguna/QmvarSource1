Imports System
Imports System.Collections.Generic
Imports System.Configuration
Namespace Ganges33.model
    <Serializable>
    Public Class SonyAcctStmtModel
        Public Sub SonyAcctStmtModel()
            CRTDT = String.Empty
            CRTCD = String.Empty
            UPDDT = String.Empty
            UPDCD = String.Empty
            UPDPG = String.Empty
            DELFG = String.Empty
            UNQ_NO = String.Empty
            UPLOAD_USER = String.Empty
            UPLOAD_DATE = String.Empty
            SHIP_TO_BRANCH_CODE = String.Empty
            SHIP_TO_BRANCH = String.Empty


            POSTING_DATE = String.Empty
            DOCUMENT_NO = String.Empty
            REFERENCE_DOC = String.Empty
            DEBIT = String.Empty
            CREDIT = String.Empty
            CASH_DISC_CREDIT = String.Empty
            BALANCE = String.Empty
            DESCRIPTION = String.Empty


            FILE_NAME = String.Empty
            SRC_FILE_NAME = String.Empty
            DATE_TIME_RPA_CREATE = String.Empty
            UserId = String.Empty

        End Sub


        Public Property CRTDT As String
        Public Property CRTCD As String
        Public Property UPDDT As String
        Public Property UPDCD As String
        Public Property UPDPG As String
        Public Property DELFG As String
        Public Property UNQ_NO As String
        Public Property UPLOAD_USER As String
        Public Property UPLOAD_DATE As String
        Public Property SHIP_TO_BRANCH_CODE As String
        Public Property SHIP_TO_BRANCH As String

        Public Property POSTING_DATE As String
        Public Property DOCUMENT_NO As String
        Public Property REFERENCE_DOC As String

        Public Property DEBIT As String
        Public Property CREDIT As String
        Public Property CASH_DISC_CREDIT As String
        Public Property BALANCE As String

        Public Property DESCRIPTION As String

        Public Property FILE_NAME As String
        Public Property SRC_FILE_NAME As String
        Public Property DATE_TIME_RPA_CREATE As String
        Public Property UserId As String
        Public Property DateFrom As String
        Public Property DateTo As String

        Public Property FromDay As String
        Public Property FromMonth As String
        Public Property ToDay As String
        Public Property ToMonth As String
        Public Property ServiceCentre As String


    End Class

End Namespace

