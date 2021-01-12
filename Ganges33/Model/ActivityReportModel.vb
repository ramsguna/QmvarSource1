Imports System
Imports System.Collections.Generic
Imports System.Configuration
Namespace Ganges33.model
    <Serializable>
    Public Class ActivityReportModel
        Public Sub ActivityReportModel()
            CRTDT = String.Empty
            CRTCD = String.Empty
            UPDDT = String.Empty
            UPDCD = String.Empty
            UPDPG = String.Empty
            DELFG = String.Empty

            UserId = String.Empty
            UserName = String.Empty
            MonthYear = String.Empty
            UPLOAD_USER = String.Empty
            UPLOAD_DATE = String.Empty
            SHIP_TO_BRANCH_CODE = String.Empty
            SHIP_TO_BRANCH = String.Empty

            UpdateUser = String.Empty
            UpdateDatetime = String.Empty
            Month = String.Empty
            Day = String.Empty
            Note = String.Empty
            CustomerVisit = 0
            CallRegisterd = 0
            RepairCompleted = 0
            GoodsDelivered = 0
            PendingCalls = 0
            CancelledCalls = 0
            location = String.Empty
            FILE_NAME = String.Empty
            SRC_FILE_NAME = String.Empty
            DATE_TIME_RPA_CREATE = String.Empty


        End Sub


        Public Property CRTDT As String
        Public Property CRTCD As String
        Public Property UPDDT As String
        Public Property UPDCD As String
        Public Property UPDPG As String
        Public Property DELFG As String

        Public Property UserId As String
        Public Property UserName As String
        Public Property MonthYear As String

        Public Property UPLOAD_USER As String
        Public Property UPLOAD_DATE As String
        Public Property SHIP_TO_BRANCH_CODE As String
        Public Property SHIP_TO_BRANCH As String

        Public Property UpdateUser As String
        Public Property UpdateDatetime As String
        Public Property Month As String
        Public Property Day As String
        Public Property Note As String
        Public Property CustomerVisit As Integer
        Public Property CallRegisterd As Integer
        Public Property RepairCompleted As Integer
        Public Property GoodsDelivered As Integer
        Public Property PendingCalls As Integer
        Public Property CancelledCalls As Integer
        Public Property location As String
        Public Property FILE_NAME As String
        Public Property SRC_FILE_NAME As String
        Public Property DATE_TIME_RPA_CREATE As String

        Public Property FromDay As String
        Public Property FromMonth As String
        Public Property ToDay As String
        Public Property ToMonth As String
        Public Property ServiceCentre As String


    End Class

End Namespace

