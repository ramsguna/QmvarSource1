

Public Class Login_normal
    Inherits System.Web.UI.Page
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        '初期処理
        If IsPostBack = False Then

            'セッション情報クリア
            Session.Clear()

        End If

    End Sub

    '送信ボタン押下処理
    Protected Sub BtnSubmit_Click(sender As Object, e As ImageClickEventArgs) Handles BtnSubmit.Click

        '***リストの初期化***
        DropListLocation.Items.Clear()

        '***入力値を設定***
        Dim userid As String = Trim(TextId.Text)
        Dim password As String = Trim(TextPass.Text)

        '***入力チェック***
        If (userid = "") Or (password = "") Then
            Call showMsg("Please Enter ID and Password.")
            Exit Sub
        End If

        '***最新の拠点情報取得***
        '全拠点名称
        Dim shipNameAll() As String
        Dim shipCodeAll() As String

        Dim sqlStr As String = "SELECT ship_name, ship_code FROM dbo.M_ship_base WHERE DELFG = 0 ORDER BY ship_code;"

        Dim dsMship As New DataSet
        Dim errFlg As Integer
        dsMship = DBCommon.Get_DS(sqlStr, errFlg)

        If errFlg = 1 Then
            Call showMsg("Failed to acquire the latest base information.")
            Exit Sub
        End If

        '拠点なしは処理終了
        If dsMship Is Nothing Then
            TextId.Text = ""
            TextPass.Text = ""
            TextId.Focus()
            Exit Sub
        Else
            If dsMship.Tables(0).Rows.Count <> 0 Then

                ReDim shipNameAll(dsMship.Tables(0).Rows.Count - 1)
                ReDim shipCodeAll(dsMship.Tables(0).Rows.Count - 1)

                '全拠点名称,コードの取得
                For i = 0 To dsMship.Tables(0).Rows.Count - 1
                    Dim dr As DataRow = dsMship.Tables(0).Rows(i)

                    If dr("ship_name") IsNot DBNull.Value Then
                        shipNameAll(i) = dr("ship_name")
                    End If

                    If dr("ship_code") IsNot DBNull.Value Then
                        shipCodeAll(i) = dr("ship_code")
                    End If
                Next i
            Else
                Call showMsg("There is no location information.")
                Exit Sub
            End If
        End If

        '***ユーザの拠点情報・ユーザレベル取得***
        Dim shipString As String
        Dim userLevel As String
        Dim adminFlg As Boolean
        Dim shipName() As String
        Dim ship() As String

        sqlStr = "SELECT ship_1, user_level, admin_flg FROM dbo.M_USER WHERE user_id = '" & userid & "' AND password = '" & password & "' AND DELFG = 0;"
        Dim dsM_USER As New DataSet
        dsM_USER = DBCommon.Get_DS(sqlStr, errFlg)

        If errFlg = 1 Then
            Call showMsg("Failed to acquire user's location, level information.")
            Exit Sub
        End If

        'ユーザ情報なし
        If dsM_USER Is Nothing Then
            TextId.Text = ""
            TextPass.Text = ""
            TextId.Focus()
            Call showMsg("User information not registered has been entered.")
            Exit Sub
        Else

            Dim dr1 As DataRow = dsM_USER.Tables(0).Rows(0)

            If dr1("ship_1") IsNot DBNull.Value Then
                ship = Split(dr1("ship_1"), ",")
            End If

            If dr1("user_level") IsNot DBNull.Value Then
                userLevel = dr1("user_level")
            End If

            If dr1("admin_flg") IsNot DBNull.Value Then
                adminFlg = dr1("admin_flg")
            End If
        End If

        If adminFlg = False Then

            '拠点コードカンマ区切りの設定
            For i = 0 To UBound(ship)
                If Trim(ship(i)) <> "" Or Trim(ship(i)) <> vbNullString Then
                    shipString = shipString & "'" & ship(i) & "',"
                End If
            Next i

            '拠点数確保
            ReDim shipName(UBound(ship))

            '末尾のカンマを除く
            shipString = Left(shipString, Len(shipString) - 1)

            '拠点名称取得SQL
            sqlStr = "SELECT ship_name FROM dbo.M_ship_base WHERE DELFG = 0 AND ship_code IN (" & shipString & ");"
            Dim dsMship2 As New DataSet
            dsMship2 = DBCommon.Get_DS(sqlStr, errFlg)

            If errFlg = 1 Then
                Call showMsg("Failed to acquire branch information.")
                Exit Sub
            End If

            If dsMship2 Is Nothing Then
                TextId.Text = ""
                TextPass.Text = ""
                TextId.Focus()
                Call showMsg("Could not found branch information.")
                Exit Sub
            Else
                For i = 0 To dsMship2.Tables(0).Rows.Count - 1
                    Dim dr As DataRow = dsMship2.Tables(0).Rows(i)
                    If dr("ship_name") IsNot DBNull.Value Then
                        shipName(i) = dr("ship_name")
                    End If
                Next i
            End If

            'コンボボックスに拠点名称を設定
            For i = 0 To UBound(shipName)
                With DropListLocation
                    If Trim(shipName(i)) <> "" Then
                        .Items.Add(shipName(i))
                    End If
                End With
            Next i

        Else
            'adminユーザは全拠点対象
            'コンボボックスに拠点名称を設定
            For i = 0 To UBound(shipNameAll)
                With DropListLocation
                    If Trim(shipNameAll(i)) <> "" Then
                        .Items.Add(shipNameAll(i))
                    End If
                End With
            Next i

        End If

        'セッション情報を設定
        Session("user_id") = userid
        Session("user_level") = userLevel
        Session("admin_Flg") = True
        Session("shipCode_All") = shipCodeAll

        If userLevel = "0" Or userLevel = "1" Or userLevel = "2" Or adminFlg = True Then
            Session("ship_name_list") = shipNameAll
        Else
            Session("ship_name_list") = shipName
        End If

    End Sub

    'ログインボタン押下処理
    Protected Sub BtnLogin_Click(sender As Object, e As ImageClickEventArgs) Handles BtnLogin.Click
        'セッション情報取得
        Dim userLevel As String = Session("user_level")
        Dim adminFlg As Boolean = Session("admin_Flg")
        '入力値を設定
        Dim userid As String = Trim(TextId.Text)
        Dim password As String = Trim(TextPass.Text)
        '入力チェック(初回ログイン：アカウント)
        If (Session("user_id") Is Nothing) Then
            If (userid = "") Then
                Call showMsg("Please Enter ID and Password.")
                Exit Sub
            End If
        End If
        'ドロップリストより選択された拠点名称
        Dim shipName As String = DropListLocation.SelectedValue.ToString()
        Dim clsSetCommon As New Class_common
        Dim shipCode As String = ""
        Dim shipMark As String = ""
        Dim errMsg As String

        If shipName.Length = 0 Then
            Call showMsg("Please Select Your Branch After Login.")
            Exit Sub
        Else
            Session("ship_Name") = shipName
            If userLevel = "9" Then
                '***********************
                ' Dim setShipname As String = Session("ship_Name")

                ' Dim userLevel As String = Session("user_level")
                ' Dim adminFlg As Boolean = Session("admin_Flg")
                '***拠点コード取得***
                'Dim clsSetCommon As New Class_common
                'Dim shipCode As String = ""
                'Dim shipMark As String = ""
                'Dim errMsg As String
                clsSetCommon.setShipCode(shipName, shipCode, errMsg, shipMark)
                If errMsg <> "" Then
                    Call showMsg(errMsg)
                    Exit Sub
                End If

                '***ユーザ名取得***
                Dim userName As String = ""
                clsSetCommon.setUserName(userid, userName, errMsg)
                If errMsg <> "" Then
                    Call showMsg(errMsg)
                    Exit Sub
                End If
                '***セッション設定***
                Session("ship_code") = shipCode
                Session("user_Name") = userName

                'Added by Mohan on 2019.02.13 for Corporate Master setup
                Session("ship_mark") = shipMark
                '********************
                Response.Redirect("Analysis_Report.aspx")
            ElseIf userLevel = "0" Or userLevel = "1" Or userLevel = "2" Or adminFlg = True Then
                'Added New on 2019.02.13 
                clsSetCommon.setShipCode(shipName, shipCode, errMsg, shipMark)
                If errMsg <> "" Then
                    Call showMsg(errMsg)
                    Exit Sub
                End If

                '***ユーザ名取得***
                Dim userName As String = ""
                clsSetCommon.setUserName(userid, userName, errMsg)
                If errMsg <> "" Then
                    Call showMsg(errMsg)
                    Exit Sub
                End If
                '***セッション設定***
                Session("ship_code") = shipCode
                Session("user_Name") = userName

                'Added by Mohan on 2019.02.13 for Corporate Master setup
                Session("ship_mark") = shipMark

                'End
                Response.Redirect("Menu.aspx")
            Else
                'Added on 2019.02.13
                clsSetCommon.setShipCode(shipName, shipCode, errMsg, shipMark)
                If errMsg <> "" Then
                    Call showMsg(errMsg)
                    Exit Sub
                End If

                '***ユーザ名取得***
                Dim userName As String = ""
                clsSetCommon.setUserName(userid, userName, errMsg)
                If errMsg <> "" Then
                    Call showMsg(errMsg)
                    Exit Sub
                End If
                '***セッション設定***
                Session("ship_code") = shipCode
                Session("user_Name") = userName

                'Added by Mohan on 2019.02.13 for Corporate Master setup
                Session("ship_mark") = shipMark

                'End
                Response.Redirect("Menu2.aspx")
            End If

        End If

    End Sub

    Protected Sub showMsg(ByVal Msg As String)

        lblMsg.Text = Msg
        Dim sScript As String = "$(function () { $( ""#dialog"" ).dialog({width: 400, buttons: {""OK"": function () {$(this).dialog('close');}}});});"
        'JavaScriptの埋め込み
        ClientScript.RegisterStartupScript(Me.GetType(), "startup", sScript, True)

    End Sub

End Class