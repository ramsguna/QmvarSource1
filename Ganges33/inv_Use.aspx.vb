Imports System.Data.SqlClient

Public Class inv_Use
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        If IsPostBack = False Then

            '***セッションなしログインユーザの対応***
            Dim userid As String = Session("user_id")
            If userid = "" Then
                Response.Redirect("Login.aspx")
            End If

        End If

    End Sub
    'sendボタン押下処理
    Protected Sub btnSend_Click(sender As Object, e As ImageClickEventArgs) Handles btnSend.Click

        '***セッション情報取得***
        Dim userid As String = Session("user_id")
        Dim shipCode As String = Session("ship_code")

        '***入力チェック***
        Dim msg As String = String.Empty

        Dim itemSerial As String = Trim(TextItemSerial.Text)
        Dim item As String = Trim(TextItem.Text)
        Dim qty As String = Trim(TextQty.Text)
        Dim repairNo As String = Trim(TextRepairNo.Text)

        If userid Is Nothing Then
            msg = "The session has expired. Please login again."
            Call showMsg(msg, "")
            Exit Sub
        End If

        If itemSerial = "" And item = "" Then
            msg = "Enter part number with serial or without serial."
            Call showMsg(msg, "")
            Exit Sub
        End If

        If item <> "" And qty = "" Then
            msg = "Please enter the number of parts."
            Call showMsg(msg, "")
            Exit Sub
        End If

        '数値以外
        Dim num As Integer
        If Int32.TryParse(qty, num) = False Then
            msg = "Please enter the number."
            Call showMsg(msg, "")
            Exit Sub
        End If

        If itemSerial <> "" And item <> "" Then
            msg = "You can not enter at the same time. Please input separately."
            Call showMsg(msg, "")
            Call ReSet()
            Exit Sub
        End If

        If ChkCarryOut.Checked = False And ChkUnusedBack.Checked = False And ChkReturnAfterUsed.Checked = False Then
            msg = "Please check."
            Call showMsg(msg, "")
            Exit Sub
        End If

        Dim status As String
        If ChkCarryOut.Checked = True Then
            status = "3"
        ElseIf ChkUnusedBack.Checked = True Then
            status = "1"
        ElseIf ChkReturnAfterUsed.Checked = True Then
            status = "2"
        End If

        If status = "2" And repairNo = "" Then
            msg = "Please enter the repair number."
            Call showMsg(msg, "")
            Exit Sub
        End If

        '***ステータス設定***
        'コネクションを取得
        Dim con As SqlConnection = New SqlConnection(ConfigurationManager.ConnectionStrings("cnstr").ConnectionString)
        con.Open()

        'トランザクション開始＆コネクションオープン
        Dim trn As SqlTransaction = con.BeginTransaction(IsolationLevel.ReadCommitted)

        Try

            '■シリアル番号あり
            If itemSerial <> "" Then

                '検索用クエリ文字列
                Dim select_sql1 As String = "SELECT * FROM dbo.T_inParts WHERE parts_serial = '" & itemSerial & "' AND ship_code = '" & shipCode & "';"

                'DB検索結果処理
                Dim sqlSelect1 As New SqlCommand(select_sql1, con, trn)
                Dim Adapter1 As New SqlDataAdapter(sqlSelect1)
                Dim Builder1 As New SqlCommandBuilder(Adapter1)
                Dim ds1 As New DataSet
                Dim dr1 As DataRow
                Dim preStatus As String
                Adapter1.Fill(ds1)

                If ds1.Tables(0).Rows.Count <> 0 Then

                    dr1 = ds1.Tables(0).Rows(0)
                    If dr1("parts_status") IsNot DBNull.Value Then
                        preStatus = dr1("parts_status")
                    End If

                    If preStatus = status Then
                        msg = "The current status is the same as the one selected."
                        Call showMsg(msg, "")
                        Exit Sub
                    End If

                    'ステータス変更チェック(※登録済のステータスでケース分け)
                    Select Case preStatus

                        '1:DB上は、未使用　在庫
                        Case 1
                            If status = "2" Then
                                msg = "It does not appear to have been taken out. It is not subject to return."
                                Call showMsg(msg, "")
                                Exit Sub
                            End If

                        '2:DB上は、使用済　未返却
                        Case 2
                            If status = "1" Then
                                msg = "You can not change to the selected status."
                                Call showMsg(msg, "")
                                Exit Sub
                            End If

                        '3:DB上は、持出中
                        Case 3
                            If status = "1" Then
                                msg = "You can not change to the selected status."
                                Call showMsg(msg, "")
                                Exit Sub
                            End If

                        '4:DB上は、返却済
                        Case 4
                            If status = "1" Or status = "2" Or status = "3" Then
                                msg = "You can not change to the selected status."
                                Call showMsg(msg, "")
                                Exit Sub
                            End If
                    End Select

                    'ステータス変更
                    dr1 = ds1.Tables(0).Rows(0)
                    If dr1("parts_status") IsNot DBNull.Value Then
                        dr1("parts_status") = status
                    End If

                    'repair_noを登録
                    If status = "2" Then
                        dr1("repair_no") = repairNo
                    End If

                    '更新
                    Adapter1.Update(ds1)
                    trn.Commit()
                    msg = "status changed successfully!" & "\n" & "Serial no:" & itemSerial & "\n" & "Status:" & status

                Else
                    msg = "There is no change in status."
                    Call showMsg(msg, "")
                    Exit Sub
                End If

            Else

                '■シリアル番号なし

                '検索用クエリ文字列
                Dim select_sql1 As String = "SELECT * FROM dbo.T_inParts_2 WHERE parts_no = '" & item & "' AND ship_code = '" & shipCode & "';"

                'DB検索結果処理
                Dim sqlSelect1 As New SqlCommand(select_sql1, con, trn)
                Dim Adapter1 As New SqlDataAdapter(sqlSelect1)
                Dim Builder1 As New SqlCommandBuilder(Adapter1)
                Dim ds1 As New DataSet
                Dim dr1 As DataRow
                Dim prePartsUnuse As Integer
                Dim prePartsUse As Integer
                Dim prePartsOtw As Integer
                Dim prePartsReturn As Integer

                Adapter1.Fill(ds1)

                If ds1.Tables(0).Rows.Count <> 0 Then

                    dr1 = ds1.Tables(0).Rows(0)

                    If dr1("parts_unuse") IsNot DBNull.Value Then
                        prePartsUnuse = dr1("parts_unuse")
                    End If

                    If dr1("parts_use") IsNot DBNull.Value Then
                        prePartsUse = dr1("parts_use")
                    End If

                    If dr1("parts_otw") IsNot DBNull.Value Then
                        prePartsOtw = dr1("parts_otw")
                    End If

                    If dr1("parts_return") IsNot DBNull.Value Then
                        prePartsReturn = dr1("parts_return")
                    End If

                    'チェックの処理ステータスでケース分け
                    Select Case status

                        Case 1
                            'unused back（未使用戻し）
                            If prePartsOtw = 0 Then
                                msg = "It does not appear to have been taken out. It is not subject to return."
                                Call showMsg(msg, "")
                                Exit Sub
                            End If
                            If qty > prePartsOtw Then
                                msg = "Please check the number of returned items. The number of books returned will be negative.　"
                                Call showMsg(msg, "")
                                Exit Sub
                            End If
                            prePartsUnuse = prePartsUnuse + qty
                            dr1("parts_unuse") = prePartsUnuse

                            prePartsOtw = prePartsOtw - qty
                            dr1("parts_otw") = prePartsOtw

                        Case 2
                            'Return after used（使用後戻し⇨返却対象）
                            If prePartsOtw = 0 Then
                                msg = "It does not appear to have been taken out. It is not subject to return."
                                Call showMsg(msg, "")
                                Exit Sub
                            End If
                            If qty > prePartsOtw Then
                                msg = "Please check the number of returned items. The number of books returned will be negative.　"
                                Call showMsg(msg, "")
                                Exit Sub
                            End If
                            prePartsUse = prePartsUse + qty
                            dr1("parts_use") = prePartsUse

                            prePartsOtw = prePartsOtw - qty
                            dr1("parts_otw") = prePartsOtw

                        Case 3
                            'carry out from use（未使用持ち出し）
                            If prePartsUnuse < qty Then
                                msg = "You can not bring out more than stock quantity."
                                Call showMsg(msg, "")
                                Exit Sub
                            End If
                            prePartsUnuse = prePartsUnuse - qty
                            dr1("parts_unuse") = prePartsUnuse

                            prePartsOtw = prePartsOtw + qty
                            dr1("parts_otw") = prePartsOtw

                    End Select

                    '更新
                    Adapter1.Update(ds1)
                    trn.Commit()
                    msg = "status changed successfully!"

                Else
                    msg = "There is no change in status. Please check the part information。"
                    Call showMsg(msg, "")
                    Exit Sub
                End If

            End If

            Call showMsg(msg, "LastMsg")

        Catch ex As Exception
            trn.Rollback()
            Call showMsg("DB part status setting failed.", "")
        Finally
            'DB接続クローズ
            If con.State <> ConnectionState.Closed Then
                con.Close()
            End If
            Call ReSet()
        End Try

    End Sub

    Protected Sub showMsg(ByVal Msg As String, ByVal msgChk As String)

        'Dim formid As String = Me.Form.ClientID

        '' 確認ダイアログを出力するスクリプト
        '' POST先は自分自身(****.aspx)

        'Dim sScript As String

        'If msgChk = "LastMsg" Then
        '    sScript = "if(alert(""" + Msg + """)){ " +
        'formid + ".method = ""post"";" +
        '                                formid + ".action = ""inv_Use.aspx?errLastMsg=true"";" +
        '                                formid + ".submit();" +
        '                            "}"
        'Else
        '    sScript = "if(alert(""" + Msg + """)){ " +
        'formid + ".method = ""post"";" +
        '                                formid + ".action = ""inv_Use.aspx?errMsg=true"";" +
        '                                formid + ".submit();" +
        '                            "}"
        'End If

        lblMsg.Text = Msg
        Dim sScript As String = "$(function () { $( ""#dialog"" ).dialog({width: 400, buttons: {""OK"": function () {$(this).dialog('close');}}});});"
        'JavaScriptの埋め込み
        ClientScript.RegisterStartupScript(Me.GetType(), "startup", sScript, True)

    End Sub

    Private Sub ChkCarryOut_CheckedChanged(sender As Object, e As EventArgs) Handles ChkCarryOut.CheckedChanged

        If ChkCarryOut.Checked = True Then
            ChkReturnAfterUsed.Checked = False
            ChkUnusedBack.Checked = False
        End If

    End Sub

    Private Sub ChkReturnAfterUsed_CheckedChanged(sender As Object, e As EventArgs) Handles ChkReturnAfterUsed.CheckedChanged

        If ChkReturnAfterUsed.Checked = True Then
            ChkCarryOut.Checked = False
            ChkUnusedBack.Checked = False
        End If

    End Sub

    Private Sub ChkUnusedBack_CheckedChanged(sender As Object, e As EventArgs) Handles ChkUnusedBack.CheckedChanged

        If ChkUnusedBack.Checked = True Then
            ChkCarryOut.Checked = False
            ChkReturnAfterUsed.Checked = False
        End If

    End Sub
    '画面表示コントロールのリセット
    Private Sub ReSet()

        TextItemSerial.Text = ""
        TextItem.Text = ""
        TextQty.Text = ""
        TextRepairNo.Text = ""
        ChkCarryOut.Checked = False
        ChkUnusedBack.Checked = False
        ChkReturnAfterUsed.Checked = False

    End Sub

End Class