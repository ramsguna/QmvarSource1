Imports System
Imports System.Collections.Generic
Imports System.Configuration
Namespace Ganges33.model
    <Serializable>
    Public Class SonyMonthlyRepairsetModel
        Public Sub SonyMonthlyRepairsetModel()
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
            JOB_NUMBER = String.Empty
            SEQ = String.Empty
            PRODUCT_CATEGORY = String.Empty
            PRODUCT_SUB_CATEGORY = String.Empty
            MODEL_CODE = String.Empty
            MODEL_NAME = String.Empty
            SERIAL_NO = String.Empty
            SERVICE_TYPE = String.Empty
            TRANSFER_FLAG = String.Empty
            TRANSFER_JOB_NO = String.Empty
            GUARANTEE_CODE = String.Empty
            CUSTOMER_GROUP = String.Empty
            CITY_NAME = String.Empty
            PURCHASED_DATE = String.Empty
            WARRANTY_TYPE = String.Empty
            WARRANTY_CATEGORY = String.Empty
            WARRANTY_CARD_NO = String.Empty
            WARRANTY_CARD_TYPE = String.Empty
            TECHNICIAN = String.Empty
            RESERVATION_CREATE_DATE = String.Empty
            JOB_CREATE_DATE = String.Empty
            ALLOCATION_DATE = String.Empty
            BEGIN_REPAIR_DATE = String.Empty
            REPAIR_COMPLETED_DATE = String.Empty
            REPAIR_RETURNED_DATE = String.Empty
            PART_CODE = String.Empty
            PART_DESC = String.Empty
            REPAIR_QTY = String.Empty
            PART_UNIT_PRICE = String.Empty
            PO_NO = String.Empty
            PO_CREATE_DATE = String.Empty
            SHIPPED_DATE = String.Empty
            IN_OUT_DATE = String.Empty
            SYMPTOM_CODE = String.Empty
            SECTION_CODE = String.Empty
            DEFECT_CODE = String.Empty
            REPAIR_CODE = String.Empty
            REPAIR_LEVEL = String.Empty
            REPAIR_FEE_TYPE = String.Empty
            PART_FEE = String.Empty
            INSPECTION_FEE = String.Empty
            HANDLING_FEE = String.Empty
            LABOR_FEE = String.Empty
            HOME_SERVICE_FEE = String.Empty
            LONG_FEE = String.Empty
            INSTALL_FEE = String.Empty
            TOTAL_AMOUNT_OF_ACCOUNT_PAYABLE = String.Empty
            ACCOUNT_PAYABLE_BY_CUSTOMER = String.Empty
            SONY_NEEDS_TO_PAY = String.Empty
            ASC_PAY = String.Empty
            CR90 = String.Empty
            RR90 = String.Empty
            REPAIR_TAT = String.Empty
            D4D = String.Empty
            MODEL_6D = String.Empty
            D6D_DESC = String.Empty
            NPRR = String.Empty
            PART_6D = String.Empty
            VENDOR_PART_PRICE = String.Empty
            FIRST_ESTIMATION_CREATE_DATE = String.Empty
            LAST_ESTIMATION_DATE = String.Empty
            LATEST_ESTIMATE_STATUS = String.Empty
            PARTS_REQUEST_DATE = String.Empty
            ASC_PO_NUMBER = String.Empty
            ASC_PO_DATE = String.Empty
            LAST_STATUS_UPDATE_DATE = String.Empty
            ST_TYPE = String.Empty
            TECHNICIAN_REMARKS = String.Empty
            PART_ALLOCATED = String.Empty
            IRIS_LINE_TRANSFER_FLAG = String.Empty
            CONVERTTOJOB_IN_MAPP = String.Empty
            COMPLETED_IN_MAPP = String.Empty
            DELIVER_IN_MAPP = String.Empty


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
        'Public Property ASC_CODE As String
        Public Property ASC_NAME As String
        Public Property JOB_NUMBER As String
        Public Property SEQ As String
        Public Property PRODUCT_CATEGORY As String
        Public Property PRODUCT_SUB_CATEGORY As String
        Public Property MODEL_CODE As String
        Public Property MODEL_NAME As String
        Public Property SERIAL_NO As String
        Public Property SERVICE_TYPE As String
        Public Property TRANSFER_FLAG As String
        Public Property TRANSFER_JOB_NO As String
        Public Property GUARANTEE_CODE As String
        Public Property CUSTOMER_GROUP As String
        Public Property CITY_NAME As String
        Public Property PURCHASED_DATE As String
        Public Property WARRANTY_TYPE As String
        Public Property WARRANTY_CATEGORY As String
        Public Property WARRANTY_CARD_NO As String
        Public Property WARRANTY_CARD_TYPE As String
        Public Property TECHNICIAN As String
        Public Property RESERVATION_CREATE_DATE As String
        Public Property JOB_CREATE_DATE As String
        Public Property ALLOCATION_DATE As String
        Public Property BEGIN_REPAIR_DATE As String
        Public Property REPAIR_COMPLETED_DATE As String
        Public Property REPAIR_RETURNED_DATE As String
        Public Property PART_CODE As String
        Public Property PART_DESC As String
        Public Property REPAIR_QTY As String
        Public Property PART_UNIT_PRICE As String
        Public Property PO_NO As String
        Public Property PO_CREATE_DATE As String
        Public Property SHIPPED_DATE As String
        Public Property IN_OUT_DATE As String
        Public Property SYMPTOM_CODE As String
        Public Property SECTION_CODE As String
        Public Property DEFECT_CODE As String
        Public Property REPAIR_CODE As String
        Public Property REPAIR_LEVEL As String
        Public Property REPAIR_FEE_TYPE As String


        Public Property PART_FEE As String
        Public Property INSPECTION_FEE As String
        Public Property HANDLING_FEE As String
        Public Property LABOR_FEE As String
        Public Property HOME_SERVICE_FEE As String
        Public Property LONG_FEE As String
        Public Property INSTALL_FEE As String
        Public Property TOTAL_AMOUNT_OF_ACCOUNT_PAYABLE As String
        Public Property ACCOUNT_PAYABLE_BY_CUSTOMER As String
        Public Property SONY_NEEDS_TO_PAY As String
        Public Property ASC_PAY As String
        Public Property CR90 As String
        Public Property RR90 As String
        Public Property REPAIR_TAT As String

        Public Property D4D As String

        Public Property MODEL_6D As String

        Public Property D6D_DESC As String
        Public Property NPRR As String
        Public Property PART_6D As String
        Public Property VENDOR_PART_PRICE As String
        Public Property FIRST_ESTIMATION_CREATE_DATE As String
        Public Property LAST_ESTIMATION_DATE As String
        Public Property LATEST_ESTIMATE_STATUS As String
        Public Property PARTS_REQUEST_DATE As String
        Public Property ASC_PO_NUMBER As String
        Public Property ASC_PO_DATE As String
        Public Property LAST_STATUS_UPDATE_DATE As String

        Public Property ST_TYPE As String
        Public Property TECHNICIAN_REMARKS As String
        Public Property PART_ALLOCATED As String
        Public Property IRIS_LINE_TRANSFER_FLAG As String
        Public Property CONVERTTOJOB_IN_MAPP As String
        Public Property COMPLETED_IN_MAPP As String
        Public Property DELIVER_IN_MAPP As String



        Public Property STATUS As String
        Public Property FILE_NAME As String
        Public Property UserId As String
        Public Property SRC_FILE_NAME As String

        Public Property DateFrom As String
        Public Property DateTo As String

    End Class

End Namespace
