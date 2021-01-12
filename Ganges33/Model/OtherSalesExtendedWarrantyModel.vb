Imports System
Imports System.Collections.Generic
Imports System.Configuration
Namespace Ganges33.model
    <Serializable>
    Public Class OtherSalesExtendedWarrantyModel
        Public Sub OtherSalesExtendedWarrantyModel()
            CRTDT = String.Empty
            CRTCD = String.Empty
            UPDDT = String.Empty
            UPDCD = String.Empty
            UPDPG = String.Empty
            DELFG = String.Empty
            UserName = String.Empty
            TDateTime = String.Empty
            UserId = String.Empty
            UploadUser = String.Empty
            UploadDate = String.Empty
            ShipToBranchCode = String.Empty
            ShipToBranch = String.Empty
            PoNo = String.Empty
            CustomerName = String.Empty
            PackNo = String.Empty
            UnitPrice = 0.00
            RetailPrice = 0.00
            SGSTRate = String.Empty
            SGSTAmount = 0.00
            CGSTRate = String.Empty
            CGSTAmount = 0.00
            IGSTRate = String.Empty
            IGSTAmount = 0.00
            CessRate = String.Empty
            CessAmount = 0.00
            TotalTax = 0.00
            TotalAmount = 0.00
            Note = String.Empty
            Model = String.Empty
            Serial = String.Empty
            PackDop = String.Empty
            BpNo = String.Empty
            BpNo = String.Empty
            FileName = String.Empty
            SrcFileName = String.Empty
        End Sub


        Public Property CRTDT As String
        Public Property CRTCD As String
        Public Property UPDDT As String
        Public Property UPDCD As String
        Public Property UPDPG As String
        Public Property DELFG As String
        Public Property UserName As String
        Public Property TDateTime As String
        Public Property UserId As String
        Public Property UploadUser As String
        Public Property UploadDate As String
        Public Property ShipToBranchCode As String
        Public Property ShipToBranch As String
        Public Property PoNo As String
        Public Property CustomerName As String
        Public Property PackNo As String
        Public Property UnitPrice As String
        Public Property RetailPrice As Decimal
        Public Property SGSTRate As String
        Public Property SGSTAmount As Decimal
        Public Property CGSTRate As String
        Public Property CGSTAmount As Decimal
        Public Property IGSTRate As String
        Public Property IGSTAmount As Decimal
        Public Property CessRate As String
        Public Property CessAmount As Decimal
        Public Property TotalTax As Decimal
        Public Property TotalAmount As Decimal
        Public Property Note As String
        Public Property Model As String
        Public Property Serial As String
        Public Property PackDop As String
        Public Property BpNo As String
        Public Property FileName As String
        Public Property SrcFileName As String

    End Class

End Namespace
