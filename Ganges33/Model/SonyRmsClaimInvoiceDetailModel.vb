Imports System
Imports System.Collections.Generic
Imports System.Configuration
Namespace Ganges33.model
    <Serializable>
    Public Class SonyRmsClaimInvoiceDetailModel
        Public Sub SonyRmsClaimInvoiceDetailModel()
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

            SAPCODE = String.Empty
            INVOICENUMBER = String.Empty
            ORDER_NUMBER = String.Empty
            MODEL_NAME = String.Empty
            MODEL_DESCREPTION = String.Empty
            IMEI_NO = String.Empty
            HSNCODE = String.Empty
            CLAIMCATEGORYTYPE = String.Empty
            SERVICETYPE = String.Empty
            SERVICECLAIMCATEGORY = String.Empty
            BILLINGDATE = String.Empty
            CUSTOMERINVOICENO = String.Empty
            QUANTITY = String.Empty
            CLAIMAMOUNT = String.Empty
            CGST = String.Empty
            CGSTAMOUNT = String.Empty
            SGST = String.Empty
            SGSTAMOUNT = String.Empty
            IGST = String.Empty
            IGSTAMOUNT = String.Empty
            UTGST = String.Empty
            UTGSTAMOUNT = String.Empty
            CLAIMSTATUS = String.Empty
            CLAIMCATEGORYTYPE1 = String.Empty


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

        Public Property SAPCODE As String
        Public Property INVOICENUMBER As String
        Public Property ORDER_NUMBER As String
        Public Property MODEL_NAME As String
        Public Property MODEL_DESCREPTION As String
        Public Property IMEI_NO As String
        Public Property HSNCODE As String
        Public Property CLAIMCATEGORYTYPE As String
        Public Property SERVICETYPE As String
        Public Property SERVICECLAIMCATEGORY As String
        Public Property BILLINGDATE As String
        Public Property CUSTOMERINVOICENO As String
        Public Property QUANTITY As String
        Public Property CLAIMAMOUNT As String
        Public Property CGST As String
        Public Property CGSTAMOUNT As String
        Public Property SGST As String
        Public Property SGSTAMOUNT As String
        Public Property IGST As String
        Public Property IGSTAMOUNT As String
        Public Property UTGST As String
        Public Property UTGSTAMOUNT As String
        Public Property CLAIMSTATUS As String
        Public Property CLAIMCATEGORYTYPE1 As String


        Public Property STATUS As String
        Public Property FILE_NAME As String
        Public Property UserId As String
        Public Property SRC_FILE_NAME As String

        Public Property DateFrom As String
        Public Property DateTo As String

    End Class

End Namespace
