Imports System
Imports System.Collections.Generic
Imports System.Configuration
Namespace Ganges33.model
    <Serializable>
    Public Class PartsIoModel
        Public Sub PartsIoModel()
            CRTDT = String.Empty
            CRTCD = String.Empty
            UPDDT = String.Empty
            UPDCD = String.Empty
            UPDPG = String.Empty
            DELFG = String.Empty
            Status = String.Empty
            UserName = String.Empty
            TDateTime = String.Empty
            UserId = String.Empty
            UnqNo = String.Empty
            UploadUser = String.Empty
            UploadDate = String.Empty
            No = 0
            ShipToBranchCode = String.Empty
            ShipToBranch = String.Empty
            InOutDate = String.Empty
            Type = 0
            TypeDescription = String.Empty
            RefDocNo = String.Empty
            PartsNo = String.Empty
            Description = String.Empty
            Qty = 0
            Map = String.Empty
            EngineerCode = String.Empty
            InOutWarranty = String.Empty
            Unit = String.Empty
            FileName = String.Empty
            SrcFileName = String.Empty
        End Sub
        Public Property CRTDT As String
        Public Property CRTCD As String
        Public Property UPDDT As String
        Public Property UPDCD As String
        Public Property UPDPG As String
        Public Property DELFG As String
        Public Property Status As String
        Public Property UserName As String
        Public Property TDateTime As String
        Public Property UserId As String
        Public Property UnqNo As String
        Public Property UploadUser As String
        Public Property UploadDate As String
        Public Property No As Int16
        Public Property ShipToBranchCode As String
        Public Property ShipToBranch As String
        Public Property InOutDate As String
        Public Property Type As Int16
        Public Property TypeDescription As String
        Public Property RefDocNo As String
        Public Property PartsNo As String
        Public Property Description As String
        Public Property Qty As Int16
        Public Property Map As Decimal
        Public Property EngineerCode As String
        Public Property InOutWarranty As String
        Public Property Unit As String
        Public Property FileName As String
        Public Property SrcFileName As String
    End Class

End Namespace

