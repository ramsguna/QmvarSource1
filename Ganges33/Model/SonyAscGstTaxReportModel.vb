Imports System
Imports System.Collections.Generic
Imports System.Configuration
Namespace Ganges33.model
    <Serializable>
    Public Class SonyAscGstTaxReportModel
        Public Sub SonyPartOrderModel()
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

            NO = String.Empty
            REGION = String.Empty
            SC_NAME = String.Empty
            ASC_GSTIN = String.Empty
            ASC_CODE = String.Empty
            JOB_NO = String.Empty
            CUSTOMER_ID = String.Empty
            CUSTOMER_NAME = String.Empty
            CUSTOMER_GSTIN = String.Empty
            PLACE_OF_SUPPLY = String.Empty
            MODEL_CODE = String.Empty
            MODEL_NAME = String.Empty
            PART_NUMBER = String.Empty
            PART_DISC = String.Empty
            PART_QTY = String.Empty
            HSN_SAC_CODE = String.Empty
            COLLECTED_BY = String.Empty
            PAYMENT_MODE = String.Empty
            INVOICE_NO = String.Empty
            COLLECT_DATE = String.Empty
            PART_FEE = String.Empty
            LABOR_FEE = String.Empty
            INSPECTION_FEE = String.Empty
            HANDLING_FEE = String.Empty
            TRANSPORT_FEE = String.Empty
            HOMESERVICE_FEE = String.Empty
            LONGDISTANCE_FEE = String.Empty
            TRAVELALLOWANCE_FEE = String.Empty
            DA_FEE = String.Empty
            INSTALLATION_FEE = String.Empty
            ECALL_CHARGE = String.Empty
            DEMO_CHARGE = String.Empty
            PART_DISCOUNT = String.Empty
            LABOR_DISCOUNT = String.Empty
            INSPECTION_DISCOUNT = String.Empty
            HANDLING_DISCOUNT = String.Empty
            HOMESERVICE_DISCOUNT = String.Empty
            TRANSPORT_DISCOUNT = String.Empty
            LONGDISTANCE_DISCOUNT = String.Empty
            TRAVELALLOWANCE_DISCOUNT = String.Empty
            DA_DISCOUNT = String.Empty
            INSTALLATION_DISCOUNT = String.Empty
            DEMO_DISCOUNT = String.Empty
            ECALL_DISCOUNT = String.Empty
            NET_PART_LABOR_FEE_TAXABLE_VALUE = String.Empty
            PART_SGST_TAX_RATE = String.Empty
            PART_SGST_TAX = String.Empty
            PART_CGST_TAX_RATE = String.Empty
            PART_CGST_TAX = String.Empty
            PART_IGST_TAX_RATE = String.Empty
            PART_IGST_TAX = String.Empty
            PART_UTGST_TAX_RATE = String.Empty
            PART_UTGST_TAX = String.Empty
            PART_CESS_TAX_RATE = String.Empty
            PART_CESS_TAX = String.Empty
            SERVICE_SGST_TAX_RATE = String.Empty
            SERVICE_SGST_TAX = String.Empty
            SERVICE_CGST_TAX_RATE = String.Empty
            SERVICE_CGST_TAX = String.Empty
            SERVICE_IGST_TAX_RATE = String.Empty
            SERVICE_IGST_TAX = String.Empty
            SERVICE_UTGST_TAX_RATE = String.Empty
            SERVICE_UTGST_TAX = String.Empty
            SERVICE_CESS_TAX_RATE = String.Empty
            SERVICE_CESS_TAX = String.Empty
            NET_PAYABLE_TOTAL_INVOICE_AMOUNT = String.Empty



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

        Public Property NO As String
        Public Property REGION As String
        Public Property SC_NAME As String
        Public Property ASC_GSTIN As String
        Public Property ASC_CODE As String
        Public Property JOB_NO As String
        Public Property CUSTOMER_ID As String
        Public Property CUSTOMER_NAME As String
        Public Property CUSTOMER_GSTIN As String
        Public Property PLACE_OF_SUPPLY As String
        Public Property MODEL_CODE As String
        Public Property MODEL_NAME As String
        Public Property PART_NUMBER As String
        Public Property PART_DISC As String
        Public Property PART_QTY As String
        Public Property HSN_SAC_CODE As String
        Public Property COLLECTED_BY As String
        Public Property PAYMENT_MODE As String
        Public Property INVOICE_NO As String
        Public Property COLLECT_DATE As String
        Public Property PART_FEE As String
        Public Property LABOR_FEE As String
        Public Property INSPECTION_FEE As String
        Public Property HANDLING_FEE As String
        Public Property TRANSPORT_FEE As String
        Public Property HOMESERVICE_FEE As String
        Public Property LONGDISTANCE_FEE As String
        Public Property TRAVELALLOWANCE_FEE As String
        Public Property DA_FEE As String
        Public Property INSTALLATION_FEE As String
        Public Property ECALL_CHARGE As String
        Public Property DEMO_CHARGE As String
        Public Property PART_DISCOUNT As String
        Public Property LABOR_DISCOUNT As String
        Public Property INSPECTION_DISCOUNT As String
        Public Property HANDLING_DISCOUNT As String
        Public Property HOMESERVICE_DISCOUNT As String
        Public Property TRANSPORT_DISCOUNT As String
        Public Property LONGDISTANCE_DISCOUNT As String
        Public Property TRAVELALLOWANCE_DISCOUNT As String
        Public Property DA_DISCOUNT As String
        Public Property INSTALLATION_DISCOUNT As String
        Public Property DEMO_DISCOUNT As String
        Public Property ECALL_DISCOUNT As String
        Public Property NET_PART_LABOR_FEE_TAXABLE_VALUE As String
        Public Property PART_SGST_TAX_RATE As String
        Public Property PART_SGST_TAX As String
        Public Property PART_CGST_TAX_RATE As String
        Public Property PART_CGST_TAX As String
        Public Property PART_IGST_TAX_RATE As String
        Public Property PART_IGST_TAX As String
        Public Property PART_UTGST_TAX_RATE As String
        Public Property PART_UTGST_TAX As String
        Public Property PART_CESS_TAX_RATE As String
        Public Property PART_CESS_TAX As String
        Public Property SERVICE_SGST_TAX_RATE As String
        Public Property SERVICE_SGST_TAX As String
        Public Property SERVICE_CGST_TAX_RATE As String
        Public Property SERVICE_CGST_TAX As String
        Public Property SERVICE_IGST_TAX_RATE As String
        Public Property SERVICE_IGST_TAX As String
        Public Property SERVICE_UTGST_TAX_RATE As String
        Public Property SERVICE_UTGST_TAX As String
        Public Property SERVICE_CESS_TAX_RATE As String
        Public Property SERVICE_CESS_TAX As String
        Public Property NET_PAYABLE_TOTAL_INVOICE_AMOUNT As String



        Public Property STATUS As String
        Public Property FILE_NAME As String
        Public Property UserId As String
        Public Property SRC_FILE_NAME As String

        Public Property DateFrom As String
        Public Property DateTo As String

    End Class

End Namespace
