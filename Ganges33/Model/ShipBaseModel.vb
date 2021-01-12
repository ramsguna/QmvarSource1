Imports System
Imports System.Collections.Generic
Namespace Ganges33.model
    ''' <summary>
    ''' Branch information set and get methods
    ''' </summary>
    <Serializable>
    Public Class ShipBaseModel
        Public Sub ShipBaseModel()
            CRTDT = String.Empty
            CRTCD = String.Empty
            UPDDT = String.Empty
            UPDCD = String.Empty
            UPDPG = String.Empty
            DELFG = String.Empty
            ShipName = String.Empty
            ShipInfo = String.Empty
            ShipManager = String.Empty
            ShipTel = String.Empty
            ShipAdd1 = String.Empty
            ShipAdd2 = String.Empty
            ShipAdd3 = String.Empty
            zip = String.Empty
            Email = String.Empty
            ShipService = String.Empty
            OpenTime = String.Empty
            CloseTime = String.Empty
            OpeningDate = String.Empty
            ClosingDate = String.Empty
            ShipCode = String.Empty
            ShipMark = String.Empty
            Item1 = String.Empty
            Item2 = String.Empty
            Mess1 = String.Empty
            Mess2 = String.Empty
            Mess3 = String.Empty
            RegiDeposit = String.Empty
            PoNo = String.Empty
            Inspection1Start = String.Empty
            Inspection1End = String.Empty
            Inspection2Start = String.Empty
            Inspection2End = String.Empty
            Inspection3Start = String.Empty
            Inspection3End = String.Empty
            OpenStart = String.Empty
            OpenEnd = String.Empty
            CloseStart = String.Empty
            CloseEnd = String.Empty

        End Sub

        Public Property CRTDT As String
        Public Property CRTCD As String
        Public Property UPDDT As String
        Public Property UPDCD As String
        Public Property UPDPG As String
        Public Property DELFG As String
        Public Property ShipName As String
        Public Property ShipInfo As String
        Public Property ShipManager As String
        Public Property ShipTel As String
        Public Property ShipAdd1 As String
        Public Property ShipAdd2 As String
        Public Property ShipAdd3 As String
        Public Property zip As String
        Public Property Email As String
        Public Property ShipService As String
        Public Property OpenTime As String
        Public Property CloseTime As String
        Public Property OpeningDate As String
        Public Property ClosingDate As String
        Public Property ShipCode As String
        Public Property ShipMark As String
        Public Property Item1 As String
        Public Property Item2 As String
        Public Property Mess1 As String
        Public Property Mess2 As String
        Public Property Mess3 As String
        Public Property RegiDeposit As String
        Public Property PoNo As String
        Public Property Inspection1Start As String
        Public Property Inspection1End As String
        Public Property Inspection2Start As String
        Public Property Inspection2End As String
        Public Property Inspection3Start As String
        Public Property Inspection3End As String
        Public Property OpenStart As String
        Public Property OpenEnd As String
        Public Property CloseStart As String
        Public Property CloseEnd As String
        Public Property AllBranchName() As String
        Public Property AllBranchCode() As String

    End Class
End Namespace