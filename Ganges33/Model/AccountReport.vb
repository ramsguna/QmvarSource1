Public Class AccountReport
    Public Sub AccountReport()
        SeqID = 0
        SeqName = String.Empty
        Position = String.Empty
        Title = String.Empty
        Amount = Nothing
        SHIP_TO_BRANCH = String.Empty
        SHIP_TO_BRANCH_CODE = String.Empty
        Month = String.Empty
        Year = String.Empty
    End Sub


    Public Property SeqID As Int16
    Public Property SeqName As String
    Public Property Position As String
    Public Property Title As String
    Public Property Amount As Double?
    Public Property SHIP_TO_BRANCH_CODE As String
    Public Property SHIP_TO_BRANCH As String
    Public Property Month As String
    Public Property Year As String
End Class
