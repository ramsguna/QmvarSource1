Imports System
Imports System.Collections.Generic
Imports System.Configuration
Namespace Ganges33.model
    <Serializable>
    Public Class SonyDailyASCPartsUsedModel
        Public Sub SonyDailyASCPartsUsedModel()
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


            COUNTRY = String.Empty
            REGION = String.Empty
            ASC_CODE = String.Empty
            ASC_NAME = String.Empty
            JOB_NO = String.Empty
            WARRANTY_TYPE = String.Empty
            PART_CONSUMED = String.Empty
            PART_DESC = String.Empty
            QUANTITY = String.Empty
            PART_CONSUMPTION_DATE = String.Empty
            MODEL_NO = String.Empty
            S6D_CODE = String.Empty
            NPC_SALES_PRICE = String.Empty
            MAIN_CAT = String.Empty
            ST_TYPE = String.Empty
            TRANSFER_JOB_NO = String.Empty
            CUSTOMER_PART_PRICE = String.Empty



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

        Public Property COUNTRY As String
        Public Property REGION As String
        Public Property ASC_CODE As String
        Public Property ASC_NAME As String
        Public Property JOB_NO As String
        Public Property WARRANTY_TYPE As String
        Public Property PART_CONSUMED As String
        Public Property PART_DESC As String
        Public Property QUANTITY As String
        Public Property PART_CONSUMPTION_DATE As String
        Public Property MODEL_NO As String
        Public Property S6D_CODE As String
        Public Property NPC_SALES_PRICE As String
        Public Property MAIN_CAT As String
        Public Property ST_TYPE As String
        Public Property TRANSFER_JOB_NO As String
        Public Property CUSTOMER_PART_PRICE As String


        Public Property STATUS As String
        Public Property FILE_NAME As String
        Public Property UserId As String
        Public Property SRC_FILE_NAME As String

        Public Property DateFrom As String
        Public Property DateTo As String

    End Class
End Namespace

