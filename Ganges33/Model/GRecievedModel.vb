Imports System
Imports System.Collections.Generic
Imports System.Configuration
Namespace Ganges33.model
    <Serializable>
    Public Class GRecievedModel
        Public Sub GRecievedModel()
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
            InvoiceDate = String.Empty
            InvoiceNo = String.Empty
            Items = 0
            TotalQty = 0
            TotalAmount = 0.00
            GrDate = String.Empty
            CreateBy = String.Empty
            GrStatus = String.Empty
            FileName = String.Empty
            SrcFileName = String.Empty


            DateFrom = String.Empty
            DateTo = String.Empty

            PoNo = String.Empty
            ItemNo = String.Empty
            Description = String.Empty
            OrderedParts = String.Empty
            ShippedParts = String.Empty
            DeliveredQty = String.Empty
            ConfirmedQty = String.Empty
            TotalStockQty = String.Empty
            Reason = String.Empty
            Location1 = String.Empty
            Location2 = String.Empty
            Location3 = String.Empty
            HsnCodeOfPartsReceived = String.Empty
            SupplyLocation = String.Empty
            DeliveryLocation = String.Empty
            Remarks = String.Empty






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
        Public Property InvoiceDate As String
        Public Property InvoiceNo As String
        Public Property Items As Int16
        Public Property TotalQty As Int16
        Public Property TotalAmount As Decimal
        Public Property GrDate As String
        Public Property CreateBy As String
        Public Property GrStatus As String
        Public Property FileName As String
        Public Property SrcFileName As String
        Public Property DateFrom As String
        Public Property DateTo As String
        Public Property PoNo As String
        Public Property ItemNo As String
        Public Property Description As String
        Public Property OrderedParts As String
        Public Property ShippedParts As String
        Public Property DeliveredQty As String
        Public Property ConfirmedQty As String
        Public Property TotalStockQty As String
        Public Property Reason As String
        Public Property Location1 As String
        Public Property Location2 As String
        Public Property Location3 As String
        Public Property HsnCodeOfPartsReceived As String
        Public Property SupplyLocation As String
        Public Property DeliveryLocation As String
        Public Property Remarks As String
    End Class
End Namespace

