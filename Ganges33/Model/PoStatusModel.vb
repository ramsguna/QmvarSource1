Imports System
Imports System.Collections.Generic
Imports System.Configuration
Namespace Ganges33.model
    <Serializable>
    Public Class PoStatusModel
        Public Sub PoStatusModel()
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
            PoDate = String.Empty
            PoNo = String.Empty
            ConfNo = String.Empty
            ItemNo = String.Empty
            Qty = String.Empty
            Amount = 0.00
            Status = String.Empty
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
        Public Property PoDate As String
        Public Property PoNo As String
        Public Property ConfNo As String
        Public Property ItemNo As String
        Public Property Qty As String
        Public Property Amount As Decimal
        Public Property Status As String
        Public Property FileName As String
        Public Property SrcFileName As String
    End Class
End Namespace

