Public Class Class_inv

    'シリアルあり入荷で登録失敗アイテム
    Public Structure ERROR_Item
        Dim fileName As String
        Dim item As String
    End Structure

    'シリアルあり返却でアイテムのステータス
    Public Structure STATUS_Item
        Dim item As String
        Dim status As String
        Dim filename As String
        Dim maker As String
    End Structure

    'サーバ側に保存するパス
    Public saveCsvPass As String = "C:\inv\CSV\"
    Public savePDFPass As String = "C:\inv\PDF\"

    'PDF出力場所
    Public mailFilePass As String = "C:\test\pdf\"

    'CSV出力場所
    Public msgMailCsvFilePass As String = "C:\test\CSV\"

    '****************************************************
    '処理：CSVファイルのヘッダ情報を確認
    '引数：colsHead()   　読み込んだCSVファイルのヘッダ情報　
    '　　　csvKind  　　　CSVファイルの種類名称　Arrival: Return:  　
    '返却：FALSE header⇒NG　 TRRUE　header⇒OK 　
    '****************************************************
    Public Function chkHead(ByVal colsHead() As String, ByVal csvKind As String) As Boolean

        If csvKind = "Arrival" Then

            If colsHead(0) <> "parts_no" Then
                Return False
            End If

            If colsHead(1) <> "qty" Then
                Return False
            End If

            If colsHead(2) <> "loc_1" Then
                Return False
            End If

            If colsHead(3) <> "loc_2" Then
                Return False
            End If

            If colsHead(4) <> "loc_3" Then
                Return False
            End If

        ElseIf csvkind = "Return" Then

            If colsHead(0) <> "parts_serial" Then
                Return False
            End If

        ElseIf csvkind = "Report1" Then

            If colsHead(0) <> "unq" Then
                Return False
            End If

            If colsHead(1) <> "parts_serial" Then
                Return False
            End If

        ElseIf csvkind = "Report2" Then

            If colsHead(0) <> "unq" Then
                Return False
            End If

            If colsHead(1) <> "parts_no" Then
                Return False
            End If

            If colsHead(2) <> "qty" Then
                Return False
            End If

        End If

        Return True

    End Function

End Class
