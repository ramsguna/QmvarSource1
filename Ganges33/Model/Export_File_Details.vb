Imports Ganges33.Ganges33.dao
Imports Ganges33.Ganges33.model
Imports Ganges33.Ganges33.logic

Namespace Ganges33.model

    Public Class Export_File_Details


        'Public Property StoreLocation As String
        Public Property ExportFile As String
        Public Property SrcFileName As String
        Public Property Year As String
        Public Property Month As String
        Public Property DaySub As String
        Public Property DTSub As String
        Public Property GRSumDtl As String
        Public Property DateFrom As String
        Public Property DateTo As String
        Public Property UserId As String
        Public Property UserName As String
        Public Property ShipToBranchCode As String
        Public Property ShipToBranch As String
        Public Property FileName As String
        Public Property number As String
        Public Property dtImpDetails As DataTable
        'Public dat ExpDtl()
        '    Dim dt As New DataTable()
        'dt.Columns.AddRange(New DataColumn(2) {New DataColumn("Id", GetType(Integer)), New DataColumn("Name", GetType(String)), New DataColumn("Country", GetType(String))})
        'dt.Rows.Add(1, "John Hammond", "United States")
        'dt.Rows.Add(2, "Mudassar Khan", "India")
        'dt.Rows.Add(3, "Suzanne Mathews", "France")
        'dt.Rows.Add(4, "Robert Schidner", "Russia")


    End Class

    Public Class Export_File_Parameter_Details
        Public Property StoreName As String
        Public Property StoreValue As String
        Public Property ColumnName As String
        Public Property DateFrom As String
        Public Property DateTo As String
        Public Property IsActive As String
        Public Property CurrentUser As String
        Public Property UserLevel As String
        Public Property AdminFlag As String
        Public Property UserID As String
    End Class


End Namespace

