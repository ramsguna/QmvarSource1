Imports System
Imports System.Collections.Generic
Imports System.Configuration
Namespace Ganges33.model
    <Serializable>
    Public Class SonyB2bDateWiseSalesModel
        Public Sub SonyB2bDateWiseSalesModel()
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

            SNO = String.Empty
            MODEL_NAME = String.Empty
            MODEL_CODE = String.Empty
            MODEL_SR_NO = String.Empty
            PRODUCT_PURCHASE_DATE = String.Empty
            EW_CARD_NO = String.Empty
            EW_SALE_DATE = String.Empty
            CUSTOMER_NAME = String.Empty
            REMARK = String.Empty
            WARRANTY_TYPE = String.Empty


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

        Public Property SNO As String
        Public Property MODEL_NAME As String
        Public Property MODEL_CODE As String
        Public Property MODEL_SR_NO As String
        Public Property PRODUCT_PURCHASE_DATE As String
        Public Property EW_CARD_NO As String
        Public Property EW_SALE_DATE As String
        Public Property CUSTOMER_NAME As String
        Public Property REMARK As String
        Public Property WARRANTY_TYPE As String


        Public Property STATUS As String
        Public Property FILE_NAME As String
        Public Property UserId As String
        Public Property SRC_FILE_NAME As String

        Public Property DateFrom As String
        Public Property DateTo As String

    End Class

End Namespace
