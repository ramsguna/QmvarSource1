Namespace Ganges33.model
    ''' <summary>
    ''' Pass status to one to other
    ''' </summary>
    <Serializable>
    Public Class StatusModel
        Public Sub StatusModel()
            Status = 0
            Message = String.Empty
            Diff = String.Empty
            MistakeCount = String.Empty
            RegiDeposit = String.Empty
            Total = 0.00
            TDateTime = String.Empty
            UserName = String.Empty
            YouserName = String.Empty
            LastUpdated = String.Empty
            IpAddress = String.Empty
            Id = String.Empty
        End Sub
        Public Property Status As Integer
        Public Property Message As String
        Public Property Diff As String
        Public Property MistakeCount As String
        Public Property RegiDeposit As String
        Public Property Total As Decimal
        Public Property TDateTime As String
        Public Property UserName As String
        Public Property YouserName As String
        Public Property LastUpdated As String
        Public Property IpAddress As String
        Public Property OpeningCash As Decimal
        Public Property Id As String

    End Class
End Namespace