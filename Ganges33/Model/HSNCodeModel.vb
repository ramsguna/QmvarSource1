'VJ 2019/10/15
Imports System
Imports System.Collections.Generic
Imports System.Configuration
Namespace Ganges33.model
    <Serializable>
    Public Class HSNCodeModel
        Public Sub HSNCodeModel()
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
            PartNo = String.Empty
            HsnCode = String.Empty
            Amount = 0.00
            Cgst = 0.00
            Sgst = 0.00
            Igst = 0.00
            Cess = 0.00
            Tax = 0.00
            FileName = String.Empty
            SrcFileName = String.Empty
        End Sub
        Public Property CRTDT As String
        Public Property CRTCD As String
        Public Property UPDDT As String
        Public Property UPDCD As String
        Public Property UPDPG As String
        Public Property DELFG As String
        Public Property DataNo As Int16
        Public Property UserName As String
        Public Property TDateTime As String
        Public Property UserId As String
        Public Property UploadUser As String
        Public Property UploadDate As String
        Public Property PartNo As String
        Public Property HsnCode As String
        Public Property Amount As Decimal
        Public Property Cgst As Decimal
        Public Property Sgst As Decimal
        Public Property Igst As Decimal
        Public Property Cess As Decimal
        Public Property Tax As Decimal
        Public Property FileName As String
        Public Property SrcFileName As String
        Public Property ShipToBranchCode As String
        Public Property ShipToBranch As String
    End Class
End Namespace