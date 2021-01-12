Imports System
Imports System.Collections.Generic
Imports System.Configuration
Namespace Ganges33.model
    <Serializable>
    Public Class SonyInBoundModel
        Public Sub SonyInBoundModel()
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


            PART_NO = String.Empty
            PART_DESC = String.Empty
            QUANTITY = String.Empty
            COST = String.Empty
            STOCK_ID = String.Empty
            LOCATION_ID = String.Empty
            INBOUND_DATE = String.Empty
            REQUESTED_BY = String.Empty
            PIC = String.Empty
            FORMNO = String.Empty
            REF_NUMBER = String.Empty


            STATUS = String.Empty
            FILE_NAME = String.Empty
            UserId = String.Empty
            SRC_FILE_NAME = String.Empty

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

        Public Property PART_NO As String
        Public Property PART_DESC As String
        Public Property QUANTITY As String
        Public Property COST As String
        Public Property STOCK_ID As String
        Public Property LOCATION_ID As String
        Public Property INBOUND_DATE As String
        Public Property REQUESTED_BY As String
        Public Property PIC As String
        Public Property FORMNO As String
        Public Property REF_NUMBER As String

        Public Property STATUS As String
        Public Property FILE_NAME As String
        Public Property UserId As String
        Public Property SRC_FILE_NAME As String

        Public Property DateFrom As String
        Public Property DateTo As String

    End Class

End Namespace
