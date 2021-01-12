Imports System.Data.SqlClient
Public Class DBCommon

    ''' <summary>
    ''' 引数のSQL文を実行して結果のDataTablを取得
    ''' SELECT用
    ''' （トランザクション不可）
    ''' </summary>
    ''' <param name="query"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared Function ExecuteGetDT(ByVal query As String, ByRef errFlg As Integer) As DataTable

        Dim cmd As New SqlCommand

        'コネクションを取得して設定
        Dim con As SqlConnection = New SqlConnection(ConfigurationManager.ConnectionStrings("cnstr").ConnectionString)
        Dim dt As New DataTable

        Try
            cmd.CommandText = query
            cmd.Connection = con
            con.Open()
            Dim myDr As SqlDataReader = cmd.ExecuteReader
            dt.Load(myDr)
        Catch ex As Exception
            errFlg = 1
            '例外処理
            'Log.Error("実行SQL:[" & query & "]" & vbCrLf, ex)
            'Throw ex
        Finally
            If con.State = ConnectionState.Open Then
                con.Close()
            End If
            cmd.Dispose()
        End Try

        If dt.Rows.Count > 0 Then
            Return dt
        Else
            Return Nothing
        End If

    End Function
    '****************************************************
    '処理：現在庫情報取得
    '引数：serialFlg 1:シリアルあり　2:シリアルなし
    '　　　errFlg 戻り値　0:正常　1:異常
    '****************************************************
    Public Shared Function Get_DSStock(ByVal serialFlg As Integer, ByRef errFlg As Integer, ByVal shipCode As String) As DataSet

        'コネクションを取得
        Dim con As SqlConnection = New SqlConnection(ConfigurationManager.ConnectionStrings("cnstr").ConnectionString)
        con.Open()

        Dim dsStock As New DataSet

        Try
            Dim sqlStr As String = ""

            'シリアルあり （在庫ありのステータス1:未使用　3：持出中）
            If serialFlg = "1" Then
                sqlStr &= "SELECT A.parts_serial, A.parts_no, B.parts_name, A.ship_code "
                sqlStr &= "FROM T_inParts A "
                sqlStr &= "LEFT JOIN M_PARTS B ON A.parts_no = B.parts_no "
                sqlStr &= "WHERE A.DELFG = 0 "
                sqlStr &= "AND A.ship_code = '" & shipCode & "' "
                sqlStr &= "AND A.parts_status IN('1','3') ORDER BY A.parts_no;"
            End If

            'シリアルなし  (parts_unuseの未使用数を確認）
            If serialFlg = "2" Then
                sqlStr &= "SELECT A.parts_no, B.parts_name, A.parts_unuse, A.ship_code "
                sqlStr &= "FROM T_inParts_2 A "
                sqlStr &= "LEFT JOIN M_PARTS B ON A.parts_no = B.parts_no "
                sqlStr &= "WHERE A.DELFG = 0 "
                sqlStr &= "AND A.ship_code = '" & shipCode & "' "
                sqlStr &= "And A.parts_unuse IS NOT NULL "
                sqlStr &= "And A.parts_unuse <> 0 ORDER BY A.parts_no;"
            End If

            Dim sqlSelect As New SqlCommand(sqlStr, con)
            Dim Adapter As New SqlDataAdapter(sqlSelect)
            Dim Builder As New SqlCommandBuilder(Adapter)
            Adapter.Fill(dsStock)

        Catch ex As Exception
            errFlg = 1
            Exit Function
        Finally
            'DB接続クローズ
            If con.State <> ConnectionState.Closed Then
                con.Close()
            End If
        End Try

        If dsStock.Tables(0).Rows.Count > 0 Then
            Return dsStock
        Else
            Return Nothing
        End If

    End Function
    '****************************************************
    '処理：引数のSQL文を実行して結果のDataSetを取得
    '引数：sqlStr データ取得SQL
    '　　　errFlg 戻り値　0:正常　1:異常
    '****************************************************
    Public Shared Function Get_DS(ByVal sqlStr As String, ByRef errFlg As Integer) As DataSet

        'コネクションを取得
        Dim con As SqlConnection = New SqlConnection(ConfigurationManager.ConnectionStrings("cnstr").ConnectionString)
        con.Open()

        Dim dsData As New DataSet

        Try
            Dim sqlSelect As New SqlCommand(sqlStr, con)
            Dim Adapter As New SqlDataAdapter(sqlSelect)
            Dim Builder As New SqlCommandBuilder(Adapter)
            Adapter.Fill(dsData)

        Catch ex As Exception
            errFlg = 1
            Exit Function
        Finally

            'DB接続クローズ
            If con.State <> ConnectionState.Closed Then
                con.Close()
            End If

        End Try

        If dsData.Tables(0).Rows.Count > 0 Then
            Return dsData
        Else
            Return Nothing
        End If

    End Function

End Class
