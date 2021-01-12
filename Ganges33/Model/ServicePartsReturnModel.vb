'VJ 2019/10/10
Imports System
Imports System.Collections.Generic
Imports System.Configuration
Namespace Ganges33.model
    <Serializable>
    Public Class ServicePartsReturnModel
        Public Sub ServicePartsReturnModel()
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
            UploadUser = String.Empty
            UploadDate = String.Empty
            ShipToBranchCode = String.Empty
            ShipToBranch = String.Empty
            No = 0
            CreditReqNo = String.Empty
            SPStatus = String.Empty
            RequestDate = String.Empty
            ASCRefNo = String.Empty
            Reason = String.Empty
            ServiceOrder = String.Empty
            Engineer = String.Empty
            BillingNo = String.Empty
            Amount = 0.00
            Title = String.Empty
            Plant = String.Empty
            ReturnTrackingNo = String.Empty
            Symptom = String.Empty
            PickupDate = String.Empty
            ReturnSo = String.Empty
            FileName = String.Empty
            SrcFileName = String.Empty
        End Sub
        Public Property CRTDT As String
        Public Property CRTCD As String
        Public Property UPDDT As String
        Public Property UPDCD As String
        Public Property UPDPG As String
        Public Property DELFG As String
        Public Property UnqNo As String
        Public Property Status As String
        Public Property UserName As String
        Public Property TDateTime As String
        Public Property UserId As String
        Public Property UploadUser As String
        Public Property UploadDate As String
        Public Property ShipToBranchCode As String
        Public Property ShipToBranch As String
        Public Property No As Int16
        Public Property CreditReqNo As String
        Public Property SPStatus As String
        Public Property RequestDate As String
        Public Property ASCRefNo As String
        Public Property Reason As String
        Public Property ServiceOrder As String
        Public Property Engineer As String
        Public Property BillingNo As String
        Public Property Amount As Decimal
        Public Property Title As String
        Public Property Plant As String
        Public Property ReturnTrackingNo As String
        Public Property Symptom As String
        Public Property PickupDate As String
        Public Property ReturnSo As String
        Public Property FileName As String
        Public Property SrcFileName As String
    End Class
End Namespace

