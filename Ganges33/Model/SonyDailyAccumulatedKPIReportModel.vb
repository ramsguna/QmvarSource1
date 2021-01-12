Imports System
Imports System.Collections.Generic
Imports System.Configuration
Namespace Ganges33.model
    <Serializable>
    Public Class SonyDailyAccumulatedKPIReportModel

        Public Sub SonyDailyAccumulatedKPIReportModel()
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
            REGION_NAME = String.Empty
            CITY = String.Empty
            UNIT_CODE = String.Empty
            ORGANIZATION_NAME = String.Empty
            ASI_NAME = String.Empty
            PRODUCT_CATEGORY_NAME = String.Empty
            TOTAL_INCOMING = String.Empty
            TOTAL_CANCEL_JOBS = String.Empty
            TOTAL_REPAIR_JOBS = String.Empty
            TOTAL_PRODUCTIVE_REPAIRS = String.Empty
            TAT_1D = String.Empty
            TAT_2D = String.Empty
            TAT_4D = String.Empty
            TAT_7D = String.Empty
            TAT_13D = String.Empty
            TAT_MORE_13D = String.Empty
            CR90 = String.Empty
            RR90 = String.Empty
            AVG_REPAIR_DAY = String.Empty



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
        Public Property REGION_NAME As String
        Public Property CITY As String
        Public Property UNIT_CODE As String
        Public Property ORGANIZATION_NAME As String
        Public Property ASI_NAME As String
        Public Property PRODUCT_CATEGORY_NAME As String
        Public Property TOTAL_INCOMING As String
        Public Property TOTAL_CANCEL_JOBS As String
        Public Property TOTAL_REPAIR_JOBS As String
        Public Property TOTAL_PRODUCTIVE_REPAIRS As String
        Public Property TAT_1D As String
        Public Property TAT_2D As String
        Public Property TAT_4D As String
        Public Property TAT_7D As String
        Public Property TAT_13D As String
        Public Property TAT_MORE_13D As String
        Public Property CR90 As String
        Public Property RR90 As String
        Public Property AVG_REPAIR_DAY As String



        Public Property STATUS As String
        Public Property FILE_NAME As String
        Public Property UserId As String
        Public Property SRC_FILE_NAME As String

        Public Property DateFrom As String
        Public Property DateTo As String


    End Class

End Namespace
