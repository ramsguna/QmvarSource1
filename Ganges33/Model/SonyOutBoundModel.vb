Imports System
Imports System.Collections.Generic
Imports System.Configuration
Namespace Ganges33.model
    <Serializable>
    Public Class SonyOutBoundModel
        Public Sub SonyOutBoundModel()
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
            REF_NUMBER = String.Empty
            OUTBOUND_DATE = String.Empty
            OUTBOUND_WAY = String.Empty
            REQUEST_BY = String.Empty
            PIC = String.Empty
            ACTUAL_SELLING_UNIT_PRICE = String.Empty
            OW_BASE_PRICE = String.Empty


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
        Public Property REF_NUMBER As String
        Public Property OUTBOUND_DATE As String
        Public Property OUTBOUND_WAY As String
        Public Property REQUEST_BY As String
        Public Property PIC As String
        Public Property ACTUAL_SELLING_UNIT_PRICE As String
        Public Property OW_BASE_PRICE As String

        Public Property STATUS As String
        Public Property FILE_NAME As String
        Public Property UserId As String
        Public Property SRC_FILE_NAME As String

        Public Property DateFrom As String
        Public Property DateTo As String
    End Class

End Namespace
