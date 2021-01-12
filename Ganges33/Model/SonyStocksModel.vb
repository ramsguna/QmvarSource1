Imports System
Imports System.Collections.Generic
Imports System.Configuration
Namespace Ganges33.model
    <Serializable>
    Public Class SonyStocksModel
        Public Sub SonyStocksModel()
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


            SC_NAME = String.Empty
            SC_CODE = String.Empty
            REGION = String.Empty
            PART_NO = String.Empty
            PART_NAME_LOCATION = String.Empty
            PART_NAME_ENGLISH = String.Empty
            STOCK_ID = String.Empty
            STORAGE_DATE = String.Empty
            LOCATION_ID = String.Empty
            SKU_TYPE = String.Empty
            SKU_QTY = String.Empty
            SN_ID = String.Empty
            PRICE = String.Empty
            OWNERSHIP = String.Empty
            INVENTORY_TYPE = String.Empty
            INVENTORY_STATUS = String.Empty
            PART_NATION_LEVEL = String.Empty
            MARK = String.Empty
            PO_NO = String.Empty
            PO_TYPE = String.Empty
            PO_DATE = String.Empty
            PART_CLAIM_ID = String.Empty

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


        Public Property SC_NAME As String
        Public Property SC_CODE As String
        Public Property REGION As String
        Public Property PART_NO As String
        Public Property PART_NAME_LOCATION As String
        Public Property PART_NAME_ENGLISH As String
        Public Property STOCK_ID As String
        Public Property STORAGE_DATE As String
        Public Property LOCATION_ID As String
        Public Property SKU_TYPE As String
        Public Property SKU_QTY As String
        Public Property SN_ID As String
        Public Property PRICE As String
        Public Property OWNERSHIP As String
        Public Property INVENTORY_TYPE As String
        Public Property INVENTORY_STATUS As String
        Public Property PART_NATION_LEVEL As String
        Public Property MARK As String
        Public Property PO_NO As String
        Public Property PO_TYPE As String
        Public Property PO_DATE As String
        Public Property PART_CLAIM_ID As String


        Public Property STATUS As String
        Public Property FILE_NAME As String
        Public Property UserId As String
        Public Property SRC_FILE_NAME As String

        Public Property DateFrom As String
        Public Property DateTo As String

    End Class

End Namespace
