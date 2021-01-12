Imports System
Imports System.Collections.Generic
Imports System.Configuration
Namespace Ganges33.model
    <Serializable>
    Public Class SonyRpsiInquiryModel
        Public Sub SonyRpsiInquiryModel()
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

            COURIER_DOCKET_NO = String.Empty
            PO_NUMBER = String.Empty
            ORDERED_PART_NO = String.Empty
            SHIPPING_PART_SN = String.Empty
            PART_QTY = String.Empty
            PO_COST = String.Empty
            TOTAL_ORDER_PRICE = String.Empty
            RECEIVE_STATUS = String.Empty
            SHIPPING_DATE = String.Empty
            RECEIVE_DATE = String.Empty
            INVOICE_ID = String.Empty
            CARTON_ID = String.Empty
            CARTON_NO = String.Empty

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

        Public Property COURIER_DOCKET_NO As String
        Public Property PO_NUMBER As String
        Public Property ORDERED_PART_NO As String
        Public Property SHIPPING_PART_SN As String
        Public Property PART_QTY As String
        Public Property PO_COST As String
        Public Property TOTAL_ORDER_PRICE As String
        Public Property RECEIVE_STATUS As String
        Public Property SHIPPING_DATE As String
        Public Property RECEIVE_DATE As String
        Public Property INVOICE_ID As String
        Public Property CARTON_ID As String
        Public Property CARTON_NO As String


        Public Property STATUS As String
        Public Property FILE_NAME As String
        Public Property UserId As String
        Public Property SRC_FILE_NAME As String

        Public Property DateFrom As String
        Public Property DateTo As String

    End Class

End Namespace
