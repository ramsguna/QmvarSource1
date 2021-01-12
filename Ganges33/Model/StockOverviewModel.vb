Imports System
Imports System.Collections.Generic
Imports System.Configuration
Namespace Ganges33.model
    <Serializable>
    Public Class StockOverviewModel
        Public Sub StockOverviewModel()
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
            ShipToBranchCode = String.Empty
            ShipToBranch = String.Empty
            PartsNo = String.Empty
            Description = String.Empty
            TotalStockQty = 0
            WarehouseStockQty = 0
            EngineerStockQty = 0
            Location1 = String.Empty
            Location2 = String.Empty
            Location3 = String.Empty
            Map = 0.00
            TotalStockValue = 0.00
            Status = String.Empty
            LatestModifyDate = String.Empty
            FileName = String.Empty
            SrcFileName = String.Empty
            strMonth = String.Empty

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
        Public Property UnqNo As String
        Public Property UploadUser As String
        Public Property UploadDate As String
        Public Property ShipToBranchCode As String
        Public Property ShipToBranch As String
        Public Property PartsNo As String
        Public Property Description As String
        Public Property TotalStockQty As Int16
        Public Property WarehouseStockQty As Int16
        Public Property EngineerStockQty As Int16
        Public Property Location1 As String
        Public Property Location2 As String
        Public Property Location3 As String
        Public Property Map As Decimal
        Public Property TotalStockValue As Decimal
        Public Property Status As String
        Public Property LatestModifyDate As String
        Public Property FileName As String
        Public Property SrcFileName As String
        Public Property strMonth As String
    End Class


End Namespace
