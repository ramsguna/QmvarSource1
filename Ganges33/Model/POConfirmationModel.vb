﻿Imports System
Imports System.Collections.Generic
Imports System.Configuration
Namespace Ganges33.model
    <Serializable>
    Public Class POConfirmationModel
        Public Sub POConfirmationModel()
            CRTDT = String.Empty
            CRTCD = String.Empty
            UPDDT = String.Empty
            UPDCD = String.Empty
            UPDPG = String.Empty
            DELFG = String.Empty

            UserName = String.Empty
            'TDateTime = String.Empty
            UserId = String.Empty
            UNQ_NO = String.Empty

            'UnqNo = String.Empty
            UPLOAD_USER = String.Empty
            UPLOAD_DATE = String.Empty
            SHIP_TO_BRANCH_CODE = String.Empty
            SHIP_TO_BRANCH = String.Empty
            PO_DATE = String.Empty
            PO_NO = String.Empty
            SHIP_TO = String.Empty

            CONFIRMATION_NO = String.Empty
            ITEM_NO = String.Empty
            QTY = String.Empty
            AMOUNT = String.Empty


            FILE_NAME = String.Empty
            Status = String.Empty
            SRC_FILE_NAME = String.Empty

            'UploadUser = String.Empty
            'UploadDate = String.Empty
            'ShipToBranchCode = String.Empty
            'ShipToBranch = String.Empty
            'SamsungRefNo = String.Empty

            'YourRefNo = String.Empty
            'Model = String.Empty
            'Serial = String.Empty
            'Product = String.Empty
            'Service = String.Empty
            'DefactCode = String.Empty
            'Currency = String.Empty

            'Invoice = 0.00
            'Labor = 0.00
            'Parts = 0.00
            'Freight = 0.00
            'Other = 0.00
            'Tax = 0.00

            'FileName = String.Empty
            'LaborInvoiceNo = String.Empty
            'InvoiceDate = String.Empty
            'ReportNumber = String.Empty
            'SrcFileName = String.Empty

            DateFrom = String.Empty
            DateTo = String.Empty

            'Number = String.Empty


            'SalesReportType = String.Empty
        End Sub
        Public Property CRTDT As String
        Public Property CRTCD As String
        Public Property UPDDT As String
        Public Property UPDCD As String
        Public Property UPDPG As String
        Public Property DELFG As String

        Public Property UserName As String
        'Public Property TDateTime As String

        Public Property UserId As String
        Public Property UNQ_NO As String
        Public Property UPLOAD_USER As String
        Public Property UPLOAD_DATE As String
        Public Property SHIP_TO_BRANCH_CODE As String
        Public Property SHIP_TO_BRANCH As String
        Public Property PO_DATE As String
        Public Property PO_NO As String
        Public Property SHIP_TO As String
        Public Property CONFIRMATION_NO As String
        Public Property ITEM_NO As String
        Public Property QTY As String
        Public Property AMOUNT As String

        Public Property FILE_NAME As String
        Public Property Status As String

        Public Property SRC_FILE_NAME As String


        Public Property DateFrom As String
        Public Property DateTo As String




    End Class



End Namespace

