Imports System
Imports System.Collections.Generic
Imports System.Configuration
Namespace Ganges33.model
    <Serializable>
    Public Class SonyAscInvoiceDataModel
        Public Sub SonyAscInvoiceDataModel()
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

            SAP_CODE = String.Empty
            DEBIT_CUM_TAX_INVOICE_NO = String.Empty
            JOB_NO = String.Empty
            MATERIAL_CODE = String.Empty
            MATERIAL_DESC = String.Empty
            SN = String.Empty
            PART_NO = String.Empty
            PART_DESC = String.Empty
            HSN_CODE_SAC_CODE = String.Empty
            INVOICE_TYPE = String.Empty
            WARRANTY_TYPE = String.Empty
            SERVICE_TYPE = String.Empty
            SERVICE_CLAIM_CATEGORY = String.Empty
            BILLING_DATE = String.Empty
            CUSTOMER_INV_NO = String.Empty
            QTY = String.Empty
            PART_COST_PER_UNIT = String.Empty
            TOTAL_PARTS_COST = String.Empty
            CLAIM_AMOUNT = String.Empty
            CGST_PERCENTAGE = String.Empty
            CGST = String.Empty
            SGST_PERCENTAGE = String.Empty
            SGST = String.Empty
            IGST_PERCENTAGE = String.Empty
            IGST = String.Empty
            UGST_PERCENTAGE = String.Empty
            UGST = String.Empty





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

        Public Property SAP_CODE As String
        Public Property DEBIT_CUM_TAX_INVOICE_NO As String
        Public Property JOB_NO As String
        Public Property MATERIAL_CODE As String
        Public Property MATERIAL_DESC As String
        Public Property SN As String
        Public Property PART_NO As String
        Public Property PART_DESC As String
        Public Property HSN_CODE_SAC_CODE As String
        Public Property INVOICE_TYPE As String
        Public Property WARRANTY_TYPE As String
        Public Property SERVICE_TYPE As String
        Public Property SERVICE_CLAIM_CATEGORY As String
        Public Property BILLING_DATE As String
        Public Property CUSTOMER_INV_NO As String
        Public Property QTY As String
        Public Property PART_COST_PER_UNIT As String
        Public Property TOTAL_PARTS_COST As String
        Public Property CLAIM_AMOUNT As String
        Public Property CGST_PERCENTAGE As String
        Public Property CGST As String
        Public Property SGST_PERCENTAGE As String
        Public Property SGST As String
        Public Property IGST_PERCENTAGE As String
        Public Property IGST As String
        Public Property UGST_PERCENTAGE As String
        Public Property UGST As String


        Public Property STATUS As String
        Public Property FILE_NAME As String
        Public Property UserId As String
        Public Property SRC_FILE_NAME As String

        Public Property DateFrom As String
        Public Property DateTo As String

    End Class

End Namespace
