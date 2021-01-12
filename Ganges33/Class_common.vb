Public Class Class_common

    Dim dt As DateTime = DateTime.Now

    'インドの時刻　※日本のサーバ時刻より210分の時差を考慮して設定
    Public dtIndia = dt.AddMinutes(-210)


    'SMTPサーバ名
    Public SMTPSERVER As String = "gssltd.co.jp"

    'メール送信先
    Public toMailAddPdf As String = "sys-admin@quickgarage.co.in"
    Public toMailAddPdf2 As String = "umekita.makiko@gssltd.co.jp"
    Public toMailAddPdf3 As String = "shimada.kazuma@gssltd.co.jp"

    '****************************************************
    '処理：setMail
    '引数概要： ユーザのメールアドレスを取得
    '引数  userid     　
    '　　　userMail　　戻り値　メールアドレス　　
    '　　　errMsg      戻り値　エラーのメッセージ内容
    '****************************************************
    Public Sub setMail(ByVal userid As String, ByRef userMail As String, ByRef errMsg As String)

        Dim strSQL = "SELECT e_mail FROM M_USER_data WHERE user_id = '" & userid & "' AND DELFG = 0;"
        Dim errFlg As Integer
        Dim DT_M_USER_data As DataTable = DBCommon.ExecuteGetDT(strSQL, errFlg)

        If errFlg = 1 Then
            errMsg = "Failed to get mailAdrress."
            Exit Sub
        End If

        If DT_M_USER_data IsNot Nothing Then
            userMail = DT_M_USER_data.Rows(0)("e_mail")
            If userMail = "" Then
                errMsg = "The mail address information is missing."
                Exit Sub
            End If
        Else
            errMsg = "The mail address information is missing."
            Exit Sub
        End If

    End Sub

    '****************************************************
    '処理：setUserName
    '引数概要： ユーザの名称を取得
    '引数  userid     　
    '　　　username　　戻り値　ユーザ名称　　
    '　　　errMsg      戻り値　エラーのメッセージ内容
    '****************************************************
    Public Sub setUserName(ByVal userid As String, ByRef username As String, ByRef errMsg As String)

        Dim strSQL = "SELECT * FROM dbo.M_USER_data WHERE DELFG = 0 AND user_id = '" & userid & "';"
        Dim errFlg As Integer
        Dim DT_M_USER_data As DataTable = DBCommon.ExecuteGetDT(strSQL, errFlg)

        If errFlg = 1 Then
            errMsg = "Acquisition of user name is failed."
            Exit Sub
        End If

        If DT_M_USER_data IsNot Nothing Then
            username = DT_M_USER_data.Rows(0)("surname")
            If username = "" Then
                errMsg = "The surname information is missing."
                Exit Sub
            End If
        Else
            errMsg = "The surname information is missing."
            Exit Sub
        End If

    End Sub

    '****************************************************
    '処理：setShipCode
    '引数概要： 拠点コードを取得
    '引数  setShipName　 ※リストで選択された拠点名称等    　
    '　　　shipCode　　　戻り値　拠点コード　　
    '　　　errMsg      　戻り値　エラーのメッセージ内容
    '****************************************************
    Public Sub setShipCode(ByVal setShipName As String, ByRef shipCode As String, ByRef errMsg As String)

        Dim strSQL = "SELECT * FROM dbo.M_ship_base WHERE DELFG = 0 AND ship_name = '" & setShipName & "';"
        Dim errFlg As Integer
        Dim DT_M_ship_base As DataTable = DBCommon.ExecuteGetDT(strSQL, errFlg)

        If errFlg = 1 Then
            errMsg = "Failed to acquire M_ship_base information."
            Exit Sub
        End If

        If DT_M_ship_base IsNot Nothing Then
            If DT_M_ship_base.Rows(0)("ship_code") IsNot DBNull.Value Then
                shipCode = DT_M_ship_base.Rows(0)("ship_code")
            End If
        Else
            errMsg = "The branch code could not found"
            Exit Sub
        End If

    End Sub
    Public Sub setShipCode(ByVal setShipName As String, ByRef shipCode As String, ByRef errMsg As String, ByRef shipMark As String)

        Dim strSQL = "SELECT * FROM dbo.M_ship_base WHERE DELFG = 0 AND ship_name = '" & setShipName & "';"
        Dim errFlg As Integer
        Dim DT_M_ship_base As DataTable = DBCommon.ExecuteGetDT(strSQL, errFlg)

        If errFlg = 1 Then
            errMsg = "Failed to acquire M_ship_base information."
            Exit Sub
        End If

        If DT_M_ship_base IsNot Nothing Then
            If DT_M_ship_base.Rows(0)("ship_code") IsNot DBNull.Value Then
                shipCode = DT_M_ship_base.Rows(0)("ship_code")
            End If
            If DT_M_ship_base.Rows(0)("ship_code") IsNot DBNull.Value Then
                shipMark = DT_M_ship_base.Rows(0)("ship_mark")
            End If
        Else
            errMsg = "The branch code could not found"
            Exit Sub
        End If

    End Sub

    '****************************************************
    '処理：setSystemName
    '引数概要： system名称を取得
    '引数  shipCode    　
    '　　　systemName　　戻り値　システム名称　　
    '　　　errMsg        戻り値　エラーのメッセージ内容
    '****************************************************
    Public Sub setSystemName(ByVal shipCode As String, ByRef systemName As String, ByRef errMsg As String)

        Dim strSQL = "SELECT * FROM dbo.M_ship_base WHERE DELFG = 0 AND ship_code = '" & shipCode & "';"
        Dim errFlg As Integer
        Dim DT_M_ship_base As DataTable = DBCommon.ExecuteGetDT(strSQL, errFlg)

        If errFlg = 1 Then
            errMsg = "The failed to acquire the information"
            Exit Sub
        End If

        If DT_M_ship_base IsNot Nothing Then

            If DT_M_ship_base.Rows(0)("PO_no") IsNot DBNull.Value Then

                If DT_M_ship_base.Rows(0)("PO_no") = "SS" Then
                    systemName = "SAMSUNG"
                ElseIf DT_M_ship_base.Rows(0)("PO_no") = "QS" Then
                    systemName = "QGS"
                Else
                    systemName = ""
                End If

            End If

        Else
            errMsg = "The system information does not exist from the corresponding branch code."
            Exit Sub
        End If

    End Sub

End Class
