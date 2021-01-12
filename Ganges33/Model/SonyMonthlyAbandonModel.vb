Imports System
Imports System.Collections.Generic
Imports System.Configuration
Namespace Ganges33.model
    <Serializable>
    Public Class SonyMonthlyAbandonModel
        Public Sub SonyMonthlyAbandonModel()
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
            REGION_1 = String.Empty

            ASC_CODE = String.Empty
            ASC_NAME = String.Empty
            SERVICE_SHEET_NO = String.Empty
            MODEL_CODE = String.Empty
            MODEL_NAME = String.Empty
            SERIAL_NO = String.Empty
            CREATE_DATE = String.Empty
            CUSTOMER_NAME = String.Empty
            REPAIR_STATUS = String.Empty
            TERMINATED_DATE = String.Empty
            STATUS_REMARKS = String.Empty
            ST_TYPE = String.Empty
            CANCEL_REASON = String.Empty
            REPAIR_CONTENTS = String.Empty


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
        Public Property REGION_1 As String
        'Public Property ASC_CODE As String
        Public Property ASC_CODE As String
        Public Property ASC_NAME As String
        Public Property SERVICE_SHEET_NO As String
        Public Property MODEL_CODE As String
        Public Property MODEL_NAME As String
        Public Property SERIAL_NO As String
        Public Property CREATE_DATE As String
        Public Property CUSTOMER_NAME As String
        Public Property REPAIR_STATUS As String
        Public Property TERMINATED_DATE As String
        Public Property STATUS_REMARKS As String
        Public Property ST_TYPE As String
        Public Property CANCEL_REASON As String
        Public Property REPAIR_CONTENTS As String


        Public Property STATUS As String
        Public Property FILE_NAME As String
        Public Property UserId As String
        Public Property SRC_FILE_NAME As String

        Public Property DateFrom As String
        Public Property DateTo As String

    End Class
End Namespace
