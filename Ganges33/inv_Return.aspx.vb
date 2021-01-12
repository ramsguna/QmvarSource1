Imports System.Data.SqlClient
Public Class inv_Return
    Inherits System.Web.UI.Page
    Private Structure CSV_File
        Dim parts_no As String
        Dim qty As String
    End Structure
    Private CSV() As CSV_File
    'ロード処理
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        If IsPostBack = False Then

            '***セッションなしログインユーザの対応***
            Dim userid As String = Session("user_id")
            If userid = "" Then
                Response.Redirect("Login.aspx")
            End If

            '***表示コントロール***
            btnImport.Enabled = False
            btnClose.Enabled = False
            FileUploadInv.Enabled = False
            btnCsv.Enabled = False
            Textitem.Enabled = False
            TextQty.Enabled = False
            lblCurrentReturn.Visible = False
            lblCurrentDelivery.Visible = False

        Else

            '***キャンセルボタン押下処理***
            'キャンセルボタン押下処理
            '(Closeボタン押下されたら、以下処理は実施しない。)
            Dim s As String
            Dim btnCloseChk As String = ""
            Dim BtnCancelChk As String = ""

            For Each s In Context.Request.Form.AllKeys
                If s.Contains("btnClose") Then
                    btnCloseChk = "btnCloseOn"
                    Exit For
                End If
            Next s

            For Each s In Context.Request.Form.AllKeys
                If s.Contains("BtnCancel") Then
                    BtnCancelChk = "BtnCancelOn"
                    Exit For
                End If
            Next s

            If (btnCloseChk <> "btnCloseOn") And (BtnCancelChk = "BtnCancelOn") Then

                'Dim errCancelMsg As String = Page.Request.QueryString.Get("errCancelMsg")
                'Dim errLastMsg As String = Page.Request.QueryString.Get("errLastMsg")
                Dim item As String = Session("item_No")
                Dim itemChk() As String = Session("item_Chk")
                Dim fileChk() As String = Session("file_Chk")
                Dim FileName As String = Session("file_Name")

                'If (errCancelMsg = "false") Or (errLastMsg = "false") Then

                'キャンセル処理共通***
                'History欄削除
                Dim delRow As Integer = ListHistory.Items.Count - 1
                If delRow >= 0 Then
                    ListHistory.Items.RemoveAt(delRow)
                End If

                '***バーコードでインポート時のキャンセル処理***
                'アイテム重複チェック用配列の該当アイテムをNULLに設定
                If itemChk IsNot Nothing Then
                    If itemChk(UBound(itemChk)) = item Then
                        itemChk(UBound(itemChk)) = ""
                    End If
                End If

                '■T_ReturnParts 'データセット削除
                If Session("ds_copy_return") IsNot Nothing Then
                    Dim dsCopy2_return As New DataSet
                    Dim dr As DataRow
                    dsCopy2_return = Session("ds_copy_return")
                    dr = dsCopy2_return.Tables(0).Rows(dsCopy2_return.Tables(0).Rows.Count - 1)
                    dr.Delete()
                    Session("ds_copy_return") = dsCopy2_return
                End If

                '***csvでインポート時のキャンセル処理***
                'csvファイル重複チェック用配列の該当ファイル名をNULLに設定
                If fileChk IsNot Nothing Then
                    If fileChk(UBound(fileChk)) = FileName Then
                        fileChk(UBound(fileChk)) = ""
                    End If
                End If

                '■T_inParts 'データセット削除
                If Session("ds_copy_serial") IsNot Nothing Then

                    Dim dsCopy2_serial As New DataSet
                    dsCopy2_serial = Session("ds_copy_serial")

                    For i = 0 To dsCopy2_serial.Tables(0).Rows.Count - 1
                        If dsCopy2_serial.Tables(0).Rows.Count <> 0 And i < dsCopy2_serial.Tables(0).Rows.Count Then
                            Dim dr As DataRow = dsCopy2_serial.Tables(0).Rows(i)
                            If dr("file_name") = FileName Then
                                dr.Delete()
                                i = i - 1
                            End If
                        End If
                    Next i
                    Session("ds_copy_serial") = dsCopy2_serial
                    End If

                'アイテムとステータスの構造体の該当データ(キャンセル)を削除(ステータスにcancelを設定)
                'ステータス変更アイテムの配列の該当データ(キャンセル)にNULLを設定
                If Session("parts_Status") IsNot Nothing Then

                    Dim partsStatus() As Class_inv.STATUS_Item
                    partsStatus = Session("parts_Status")

                    Dim updateItem() As String
                    Dim updateCnt As Integer
                    If Session("update_item") IsNot Nothing Then
                        updateItem = Session("update_item")
                        updateCnt = UBound(updateItem)
                    Else
                        updateCnt = -1
                       End If

                    For i = 0 To UBound(partsStatus)
                        If partsStatus(i).filename = FileName Then
                            partsStatus(i).status = "cancel"
                            For j = 0 To updateCnt
                                If updateItem(j) = partsStatus(i).item Then
                                    updateItem(j) = ""
                                End If
                            Next
                        End If
                    Next

                    Session("parts_Status") = partsStatus
                    Session("update_item") = updateItem

                End If

                'キャンセル処理共通***
                '■後始末
                ' 次のポストバックでこのルートを通らないようにするために
                ' キャンセル処理のために追加したGET パラメータ(errCancelMsg)をaction属性から除去する
                Dim formid As String = Me.Form.ClientID
                Dim cScript As String = formid + ".action = ""inv_Return.aspx"";"
                'JavaScriptの埋め込み（画面表示前に実行させる）
                ClientScript.RegisterClientScriptBlock(Me.GetType(), "clientscript", cScript, True)

                'セッション設定
                Session("item_Chk") = itemChk
                Session("file_Chk") = fileChk

                'End If

            End If

        End If

    End Sub
    'startボタン押下処理
    Protected Sub BtnStart_Click(sender As Object, e As ImageClickEventArgs) Handles BtnStart.Click

        '***入力チェック***
        Dim msg As String = String.Empty

        '配送番号
        Dim deliveryNo As String = Trim(TextDeliveryNo.Text)

        '承認番号
        Dim approvalNo As String = Trim(TextApprovalNo.Text)

        'インポートの種類,コントロール制御
        Dim kindImport As String = ""

        If chkUseSerial.Checked = True Then
            kindImport = "useSerial"
            FileUploadInv.Enabled = True
            btnCsv.Enabled = True
        ElseIf chkUnUseSerial.Checked = True Then
            kindImport = "unUseSerial"
            btnImport.Enabled = True
            Textitem.Enabled = True
            TextQty.Enabled = True
        Else
            msg = "Please select the type of import."
            Call showMsg(msg, "")
            Exit Sub
        End If

        If deliveryNo = "" Then
            msg = "Please enter your shipping number."
            Call showMsg(msg, "")
            Exit Sub
        End If

        If approvalNo = "" Then
            msg = "Please enter your authorization number."
            Call showMsg(msg, "")
            Exit Sub
        End If

        '***画面項目等の制御***
        lblCurrentReturn.Visible = True
        lblCurrentDelivery.Visible = True
        BtnStart.Enabled = False
        chkUseSerial.Enabled = False
        chkUnUseSerial.Enabled = False

        '***セッション情報取得***
        Dim shipCode As String
        If Session("ship_code") IsNot Nothing Then
            shipCode = Session("ship_code")
        Else
            msg = "The session has expired. Please login again."
            Call showMsg(msg, "")
            Exit Sub
        End If

        '***設定***
        Dim dtNow As DateTime = DateTime.Now

        '返却番号
        Dim returnNo As String
        Dim tmpUpdate As DateTime
        Dim tmpReturnNo As String
        Dim tmpCnt As Integer
        Dim errFlg As Integer
        Dim strSQL = "SELECT top 1 * FROM T_ReturnParts WHERE DELFG = 0 AND ship_code = '" & shipCode & "' ORDER BY return_no desc;"
        Dim DT_ReturnParts As DataTable = DBCommon.ExecuteGetDT(strSQL, errFlg)

        If errFlg = 1 Then
            Call showMsg("Failed to acquire information from the DB.", "")
            Exit Sub
        End If

        If DT_ReturnParts IsNot Nothing Then

            If DT_ReturnParts.Rows(0)("UPDDT") IsNot DBNull.Value Then
                tmpUpdate = DT_ReturnParts.Rows(0)("UPDDT")
            End If

            If DT_ReturnParts.Rows(0)("UPDDT") IsNot DBNull.Value Then
                tmpReturnNo = DT_ReturnParts.Rows(0)("return_no")
            End If

        End If

        If tmpUpdate.ToString("yyyyMMdd") = dtNow.ToString("yyyyMMdd") Then
            tmpCnt = Right(tmpReturnNo, 2)
            tmpCnt = tmpCnt + 1
            returnNo = shipCode & dtNow.ToString("yyyyMMdd") & tmpCnt.ToString("00")
        Else
            returnNo = shipCode & dtNow.ToString("yyyyMMdd") & "01"
        End If

        'セッション情報設定
        Session("return_No") = returnNo
        Session("delivery_No") = deliveryNo
        Session("approval_No") = approvalNo
        Session("dt_now") = dtNow

        '***表示***
        lblReturnNnumber.Text = returnNo
        lblDeliveryNumber.Text = deliveryNo

    End Sub
    'importボタン押下処理
    Protected Sub btnImport_Click(sender As Object, e As ImageClickEventArgs) Handles btnImport.Click

        '***セッション情報取得***
        Dim userid As String = Session("user_id")
        Dim shipCode As String = Session("ship_code")
        Dim returnNo As String = Session("return_No")
        Dim deliveryNo As String = Session("delivery_No")
        Dim approvalNo As String = Session("approval_No")
        Dim pushCount As Integer = Session("push_Count")
        Dim dtNow As DateTime = Session("dt_now")
        Dim itemChk() As String = Session("item_Chk")

        Dim dsCopy2_return As New DataSet
        If Session("ds_copy_return") IsNot Nothing Then
            dsCopy2_return = Session("ds_copy_return")
        End If

        '***importボタン押下数(部品の種類)カウント***
        pushCount = pushCount + 1

        '***入力チェック***
        Dim item As String = Trim(Textitem.Text)
        Dim qty As String = Trim(TextQty.Text)
        Dim msg As String = String.Empty

        If userid Is Nothing Then
            msg = "The session has expired. Please login again."
            Call showMsg(msg, "")
            Exit Sub
        End If

        If returnNo = "" Or returnNo Is Nothing Then
            msg = "Please choose a return number."
            Call showMsg(msg, "")
            Exit Sub
        End If

        If item = "" Then
            msg = "Bar code is not loaded."
            Call showMsg(msg, "")
            Exit Sub
        End If

        If qty = "" Then
            msg = "Please enter the number."
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

        Dim errFlg As Integer

        '***アイテムと個数の有効性チェック***
        Dim tmpPartsUse As Integer
        Dim strSQL = "SELECT * FROM T_inParts_2 WHERE DELFG = 0 AND parts_no = '" & item & "' AND ship_code = '" & shipCode & "';"

        Dim DT_T_inParts_2 As DataTable = DBCommon.ExecuteGetDT(strSQL, errFlg)

        If errFlg = 1 Then
            Call reSetSession()
            If pushCount <> 1 Then
                Call ReSetAll()
            End If
            Session("all_Kinditem") = 0
            Session("all_Qty") = 0
            Session("parts_Status") = Nothing
            Call showMsg("Failed to acquire information from the DB.", "")
            Exit Sub
        End If

        If DT_T_inParts_2 Is Nothing Then
            Call showMsg("Because it is an item that is not being used, processing will be canceled.", "")
            Exit Sub
        Else
            '個数の有効性チェック
            If DT_T_inParts_2.Rows(0)("parts_use") IsNot DBNull.Value Then
                tmpPartsUse = DT_T_inParts_2.Rows(0)("parts_use")
            Else
                tmpPartsUse = 0
            End If

            If Convert.ToInt32(qty) > tmpPartsUse Then
                Call showMsg("Please check the stock number. Cancel processing.", "")
                Exit Sub
            End If

            'インポート中に超えたか確認
            If Session("ds_copy_return") IsNot Nothing Then
                If dsCopy2_return.Tables(0).Rows.Count <> 0 Then
                    For i = 0 To dsCopy2_return.Tables(0).Rows.Count - 1
                        Dim dr As DataRow = dsCopy2_return.Tables(0).Rows(i)
                        Dim tmp As Integer
                        If dr("parts_no") = item Then
                            If dr("number") IsNot Nothing Then
                                tmp = tmp + dr("number")
                                If tmp + Convert.ToInt32(qty) > tmpPartsUse Then
                                    Call showMsg("Please check the stock number. Cancel processing.", "")
                                    Exit Sub
                                End If
                            End If
                        End If
                    Next i
                End If
            End If
        End If

        '***部品番号の重複チェック***
        Dim sameItemFlg As Integer
        If pushCount <> 1 Then
            For i = 0 To UBound(itemChk)
                If itemChk(i) = item Then
                    sameItemFlg = 1
                    msg = "The registration of the same part is more than the second time. Are you sure?"
                    Call showMsg(msg, "CancelMsg")
                    Exit For
                End If
            Next
        End If

        '***メーカ情報取得***
        Dim strSQL2 = "SELECT * FROM dbo.M_PARTS WHERE DELFG = 0 AND parts_no = '" & item & "' AND parts_flg = 0 AND ship_code = '" & shipCode & "';"
        Dim maker As String

        Dim DT_M_PARTS As DataTable = DBCommon.ExecuteGetDT(strSQL2, errFlg)

        If errFlg = 1 Then
            Call reSetSession()
            If pushCount <> 1 Then
                Call ReSetAll()
            End If
            Session("all_Kinditem") = 0
            Session("all_Qty") = 0
            Session("parts_Status") = Nothing
            Call showMsg("Failed to acquire information from the DB.", "")
            Exit Sub
        End If

        If DT_M_PARTS IsNot Nothing Then
            If DT_M_PARTS.Rows(0)("maker") IsNot DBNull.Value Then
                maker = DT_M_PARTS.Rows(0)("maker")
            End If
        End If

        '***画面項目等の制御***
        btnClose.Enabled = True
        BtnStart.Enabled = False

        ReDim Preserve itemChk(pushCount - 1)
        itemChk(pushCount - 1) = item

        '***History欄へ設定***
        ListHistory.Items.Add("item no:" & item & "  " & "qty:" & qty & "  " & dtNow)

        '***データセットにitemの返却内容を設定***
        'コネクションを取得
        Dim con As SqlConnection = New SqlConnection(ConfigurationManager.ConnectionStrings("cnstr").ConnectionString)
        con.Open()
        Try
            '■T_ReturnParts空テーブル取得
            Dim select_sql2 As String = ""
            select_sql2 &= "SELECT * FROM dbo.T_ReturnParts WHERE parts_no IS NULL;"

            Dim sqlSelect2 As New SqlCommand(select_sql2, con)
            Dim Adapter2 As New SqlDataAdapter(sqlSelect2)
            Dim ds2 As New DataSet
            Adapter2.Fill(ds2)
            '新規DR取得
            Dim dr2 As DataRow = ds2.Tables(0).NewRow
            '登録内容設定
            dr2("CRTDT") = Now
            dr2("CRTCD") = userid
            dr2("UPDDT") = Now
            dr2("UPDCD") = userid
            dr2("UPDPG") = "systemName"
            dr2("DELFG") = 0
            dr2("maker") = maker
            dr2("parts_no") = item
            dr2("start_time") = dtNow
            dr2("close_time") = dtNow
            dr2("number") = qty
            dr2("deli_no") = deliveryNo
            dr2("ss_return") = approvalNo
            dr2("ship_code") = shipCode
            dr2("return_no") = returnNo

            '新規DRをDataTableに追加
            ds2.Tables(0).Rows.Add(dr2)

            '入荷作業１回目
            If pushCount = 1 Then

                'データセットのコピー(値なし)
                Dim dsCopy_return As New DataSet
                dsCopy_return = ds2.Clone
                Dim customerTable_return As DataTable = dsCopy_return.Tables(0)
                Dim drCopy_return As DataRow = ds2.Tables(0).Rows(0)

                '新しいデータセットに入荷情報をコピー
                customerTable_return.ImportRow(drCopy_return)
                Session("ds_copy_return") = dsCopy_return

            Else

                Dim customerTable_return As DataTable = dsCopy2_return.Tables(0)
                Dim drCopy_return As DataRow = ds2.Tables(0).Rows(0)

                'セッション管理されているデータセットに入荷情報を追加コピー
                customerTable_return.ImportRow(drCopy_return)
                Session("ds_copy_return") = dsCopy2_return

            End If

            If sameItemFlg = 1 Then
                Call showMsg(msg, "CancelMsg")
            Else
                Call showMsg("success!!", "LastMsg")
            End If

            'セッション設定
            Session("item_No") = item
            Session("push_Count") = pushCount
            Session("item_Chk") = itemChk
            Session("approval_No") = approvalNo

        Catch ex As Exception
            Call reSetSession()
            Call ReSetAll()
            Session("all_Kinditem") = 0
            Session("all_Qty") = 0
            Session("parts_Status") = Nothing
            Call showMsg("Data Import Error Please retry.", "")
        Finally
            'DB接続クローズ
            If con.State <> ConnectionState.Closed Then
                con.Close()
            End If
        End Try

    End Sub
    'closeボタン押下処理
    Protected Sub btnClose_Click(sender As Object, e As ImageClickEventArgs) Handles btnClose.Click

        '***セッション情報を取得***
        Dim returnNo As String = Session("return_No")
        Dim shipCode As String = Session("ship_code")

        Dim dsCopy2_return As New DataSet
        If Session("ds_copy_return") IsNot Nothing Then
            dsCopy2_return = Session("ds_copy_return")
        End If

        Dim dsCopy_tantai As New DataSet
        If Session("ds_copy_serial") IsNot Nothing Then
            dsCopy_tantai = Session("ds_copy_serial")
        End If

        Dim updateItem() As String
        If Session("update_item") IsNot Nothing Then
            updateItem = Session("update_item")
        End If

        If Session("ds_copy_serial") IsNot Nothing And updateItem Is Nothing Then
            Call showMsg("変更対象はありません。", "")
            Session("parts_Status") = Nothing
            Call ReSetAll()
            Call reSetSession()
            Exit Sub
        End If

        '最終画面の集計用
        Dim allKindItem As Integer
        Dim allQty As Integer

        Dim i, j As Integer

        '***画面項目等の制御***
        BtnStart.Enabled = True

        '***設定***
        Dim dtNowClose As DateTime = DateTime.Now

        '***DB反映***
        'コネクションを取得
        Dim con As SqlConnection = New SqlConnection(ConfigurationManager.ConnectionStrings("cnstr").ConnectionString)
        con.Open()

        'トランザクション開始＆コネクションオープン
        Dim trn As SqlTransaction = con.BeginTransaction(IsolationLevel.ReadCommitted)

        Try
            'バーコードで読み込まれた返却インポート処理    
            If Session("ds_copy_return") IsNot Nothing Then

                If dsCopy2_return.Tables(0).Rows.Count <> 0 Then

                    Dim partsNo() As String
                    Dim partsNumber() As String
                    ReDim partsNo(dsCopy2_return.Tables(0).Rows.Count - 1)
                    ReDim partsNumber(dsCopy2_return.Tables(0).Rows.Count - 1)

                    'クローズボタン押下時の時間設定
                    '返却対象のアイテムと個数を取得
                    Dim dr As DataRow
                    For i = 0 To dsCopy2_return.Tables(0).Rows.Count - 1
                        dr = dsCopy2_return.Tables(0).Rows(i)
                        dr("close_time") = dtNowClose
                        partsNo(i) = dr("parts_no")
                        partsNumber(i) = dr("number")
                    Next i

                    '■T_ReturnPartsへ反映  
                    '空テーブル取得
                    Dim select_sql0 As String = ""
                    select_sql0 &= "SELECT * FROM dbo.T_ReturnParts WHERE parts_no IS NULL;"

                    Dim sqlSelect0 As New SqlCommand(select_sql0, con, trn)
                    Dim Adapter0 As New SqlDataAdapter(sqlSelect0)
                    Dim Builder0 As New SqlCommandBuilder(Adapter0)

                    Adapter0.Fill(dsCopy2_return)
                    Adapter0.Update(dsCopy2_return)

                    '■集計
                    Dim chkTmpItem() As String 'ユニークのアイテムを設定
                    Dim cnt, cnt2, m As Integer

                    '件数取得
                    For i = 0 To UBound(partsNumber)
                        allQty = allQty + partsNumber(i)
                    Next i

                    'アイテム種類数取得
                    For i = 0 To UBound(partsNo)

                        cnt2 = 0

                        If i = 0 Then
                            ReDim Preserve chkTmpItem(cnt)
                            chkTmpItem(cnt) = partsNo(i)
                            cnt = cnt + 1
                        Else

                            'アイテムの重複確認
                            For m = 0 To UBound(chkTmpItem)
                                If partsNo(i) = chkTmpItem(m) Then
                                    cnt2 = cnt2 + 1
                                    Exit For
                                End If
                            Next m

                            '重複なければ、chkTmpItemにアイテムを設定
                            If cnt2 = 0 Then
                                ReDim Preserve chkTmpItem(cnt)
                                chkTmpItem(cnt) = partsNo(i)
                                cnt = cnt + 1
                            End If

                        End If

                    Next i

                    'アイテム種類数の設定
                    If chkTmpItem IsNot Nothing Then
                        allKindItem = chkTmpItem.Length
                    End If

                    '■T_inParts_2へ反映（返却個数の設定）
                    '既存の返却個数を取得して設定
                    For i = 0 To dsCopy2_return.Tables(0).Rows.Count - 1

                        'If partsNo(i) <> "" Then

                        Dim select_sql2 As String = ""
                        Dim tmp As String = ""
                        Dim tmp2 As String = ""
                        select_sql2 &= "SELECT * FROM dbo.T_inParts_2 WHERE ship_code = '" & shipCode & "' AND parts_no = '" & partsNo(i) & "';"

                        Dim sqlSelect2 As New SqlCommand(select_sql2, con, trn)
                        Dim Adapter2 As New SqlDataAdapter(sqlSelect2)
                        Dim Builder2 As New SqlCommandBuilder(Adapter2)
                        Dim ds2 As New DataSet
                        Adapter2.Fill(ds2)

                        '新規DR取得
                        If ds2.Tables(0).Rows.Count = 1 Then
                            Dim dr2 As DataRow = ds2.Tables(0).Rows(0)

                            '使用済個数処理
                            If dr2("parts_use") IsNot DBNull.Value Then
                                tmp2 = dr2("parts_use")
                                dr2("parts_use") = Convert.ToInt32(tmp2) - Convert.ToInt32(partsNumber(i))
                            Else
                                dr2("parts_use") = 0 - Convert.ToInt32(partsNumber(i))
                            End If

                            '返却個数処理
                            If dr2("parts_return") IsNot DBNull.Value Then
                                tmp = dr2("parts_return")
                                dr2("parts_return") = Convert.ToInt32(tmp) + Convert.ToInt32(partsNumber(i))
                            Else
                                dr2("parts_return") = 0 + Convert.ToInt32(partsNumber(i))
                            End If

                            '更新時間
                            dr2("UPDDT") = dtNowClose

                            '更新
                            Adapter2.Update(ds2)
                        End If

                        'End If

                    Next i

                End If

            End If

            'CSVファイルで読み込まれた返却インポート処理
            If Session("ds_copy_serial") IsNot Nothing Then

                If dsCopy_tantai.Tables(0).Rows.Count <> 0 Then

                    'クローズボタン押下時の時間設定
                    Dim dr As DataRow
                    For i = 0 To dsCopy_tantai.Tables(0).Rows.Count - 1
                        dr = dsCopy_tantai.Tables(0).Rows(i)
                        dr("close_time") = dtNowClose
                        dr("number") = 1
                    Next i

                    '■T_ReturnParts(単体管理)へ反映
                    '空テーブル取得
                    Dim select_sql1 As String = ""
                    select_sql1 &= "SELECT * FROM dbo.T_ReturnParts WHERE parts_no IS NULL;"

                    Dim sqlSelect1 As New SqlCommand(select_sql1, con, trn)
                    Dim Adapter1 As New SqlDataAdapter(sqlSelect1)
                    Dim Builder1 As New SqlCommandBuilder(Adapter1)

                    Adapter1.Fill(dsCopy_tantai)
                    Adapter1.Update(dsCopy_tantai)

                    '■T_ReturnPartsへ反映（ステータス変更処理）
                    For i = 0 To UBound(updateItem)

                        If updateItem(i) <> "" Then

                            Dim select_sql2 As String = ""
                            select_sql2 &= "SELECT * FROM dbo.T_inParts WHERE ship_code = '" & shipCode & "' AND parts_serial = '" & updateItem(i) & "';"

                            Dim sqlSelect2 As New SqlCommand(select_sql2, con, trn)
                            Dim Adapter2 As New SqlDataAdapter(sqlSelect2)
                            Dim Builder2 As New SqlCommandBuilder(Adapter2)
                            Dim ds2 As New DataSet
                            Adapter2.Fill(ds2)

                            If ds2.Tables(0).Rows.Count = 1 Then
                                '新規DR取得
                                Dim dr2 As DataRow = ds2.Tables(0).Rows(0)

                                '返却ステータス
                                dr2("parts_status") = "4"

                                '更新時間
                                dr2("UPDDT") = dtNowClose

                                '更新
                                Adapter2.Update(ds2)
                            End If

                        End If

                    Next i

                    '■集計 ★後で関数にした方が良いかも。
                    Dim chkTmpSerial() As String   'ユニークのシリアルを設定
                    Dim chkTmpItem() As String     'ユニークのアイテムを設定 
                    Dim csvTmpItem() As String     '返却対象のシリアルのアイテム（シリアルよりハイフンを外して設定） 
                    Dim cnt, cnt2, m As Integer

                    For i = 0 To UBound(updateItem)

                        If updateItem(i) <> "" Then

                            cnt2 = 0

                            If i = 0 Then
                                ReDim Preserve chkTmpSerial(cnt)
                                chkTmpSerial(cnt) = updateItem(i)
                                cnt = cnt + 1
                            Else

                                'アイテムの重複確認
                                If chkTmpSerial IsNot Nothing Then
                                    For m = 0 To UBound(chkTmpSerial)
                                        If updateItem(i) = chkTmpSerial(m) Then
                                            cnt2 = cnt2 + 1
                                            Exit For
                                        End If
                                    Next m
                                End If

                                '重複なければ、chkTmpItemにアイテムを設定
                                If cnt2 = 0 Then
                                    ReDim Preserve chkTmpSerial(cnt)
                                    chkTmpSerial(cnt) = updateItem(i)
                                    cnt = cnt + 1
                                End If

                            End If

                        End If

                    Next i

                    'シリアル件数取得
                    allQty = chkTmpSerial.Length

                    'アイテム名取得 ★後で関数
                    Dim tmpItemDelimiter() As String
                    For i = 0 To UBound(chkTmpSerial)
                        tmpItemDelimiter = Split(chkTmpSerial(i), "-")
                        ReDim Preserve csvTmpItem(i)
                        csvTmpItem(i) = tmpItemDelimiter(0)
                    Next i

                    'アイテム種類数取得
                    cnt = 0
                    cnt2 = 0
                    m = 0
                    For i = 0 To UBound(csvTmpItem)

                        cnt2 = 0

                        If i = 0 Then
                            ReDim Preserve chkTmpItem(cnt)
                            chkTmpItem(cnt) = csvTmpItem(i)
                            cnt = cnt + 1
                        Else

                            'アイテムの重複確認
                            For m = 0 To UBound(chkTmpItem)
                                If csvTmpItem(i) = chkTmpItem(m) Then
                                    cnt2 = cnt2 + 1
                                    Exit For
                                End If
                            Next m

                            '重複なければ、chkTmpItemにアイテムを設定
                            If cnt2 = 0 Then
                                ReDim Preserve chkTmpItem(cnt)
                                chkTmpItem(cnt) = csvTmpItem(i)
                                cnt = cnt + 1
                            End If

                        End If

                    Next i

                    allKindItem = chkTmpItem.Length

                End If

            End If

            trn.Commit()

        Catch ex As Exception
            trn.Rollback()
            Session("all_Kinditem") = 0
            Session("all_Qty") = 0
            Session("parts_Status") = Nothing
            Call reSetSession()
            Call ReSetAll()
            Call showMsg("Data Import Error Please retry.", "")
            Exit sub
        Finally
            'DB接続クローズ
            If con.State <> ConnectionState.Closed Then
                con.Close()
            End If
        End Try

        If allKindItem <> 0 Or allQty <> 0 Then
            Session("all_Kinditem") = allKindItem
            Session("all_Qty") = allQty
            Call reSetSession()
            Response.Redirect("Msg_inv_Return.aspx")
        Else
            Session("parts_Status") = Nothing
            Call reSetSession()
            Call ReSetAll()
            Call showMsg("There was no subject to return.", "")
        End If

    End Sub
    'セッションのリセット
    Protected Sub reSetSession()

        Session("ds_copy_serial") = Nothing
        Session("ds_copy_return") = Nothing
        Session("update_item") = Nothing
        Session("push_Count") = 0
        Session("push_CsvCount") = 0
        Session("item_Chk") = Nothing
        Session("up_Cnt") = 0
        Session("all_CsvRowCount") = 0
        Session("file_Name") = Nothing
        Session("item_No") = Nothing
        Session("file_Chk") = Nothing

    End Sub
    'メッセージ出力
    Protected Sub showMsg(ByVal Msg As String, ByVal msgChk As String)

        'Dim formid As String = Me.Form.ClientID

        '' 確認ダイアログを出力するスクリプト
        '' POST先は自分自身(****.aspx)

        Dim sScript As String

        If msgChk = "LastMsg" Then
            'sScript = "if(confirm(""" + Msg + """)){ " +
            '                               formid + ".method = ""post"";" +
            '                               formid + ".action = ""inv_Return.aspx?errLastMsg=true"";" +
            '                               formid + ".submit();" +
            '           "}else{" +
            '                               formid + ".method = ""post"";" +
            '                               formid + ".action = ""inv_Return.aspx?errLastMsg=false"";" +
            '                               formid + ".submit();" +
            '           "}"
            sScript = "$(function() {$( ""#dialog"" ).dialog({width: 400,buttons:{""OK"": function(){$(this).dialog('close');},""CANCEL"": function (){$(this).dialog('close');$('[id$=""BtnCancel""]').click();}}});});"
        ElseIf msgChk = "CancelMsg" Then
            'sScript = "if(confirm(""" + Msg + """)){ " +
            '                               formid + ".method = ""post"";" +
            '                               formid + ".action = ""inv_Return.aspx?errCancelMsg=true"";" +
            '                               formid + ".submit();" +
            '           "}else{" +
            '                               formid + ".method = ""post"";" +
            '                               formid + ".action = ""inv_Return.aspx?errCancelMsg=false"";" +
            '                               formid + ".submit();" +
            '           "}"
            sScript = "$(function() {$( ""#dialog"" ).dialog({width: 400,buttons:{""OK"": function(){$(this).dialog('close');},""CANCEL"": function (){$(this).dialog('close');$('[id$=""BtnCancel""]').click();}}});});"
        Else
            'sScript = "if(alert(""" + Msg + """)){ " +
            '                               formid + ".method = ""post"";" +
            '                               formid + ".action = ""inv_Return.aspx?errMsg=true"";" +
            '                               formid + ".submit();" +
            '           "}"
            'OKのみのダイアログ
            sScript = "$(function () { $( ""#dialog"" ).dialog({width: 400, buttons: {""OK"": function () {$(this).dialog('close');}}});});"
        End If

        lblMsg.Text = Msg

        'JavaScriptの埋め込み
        ClientScript.RegisterStartupScript(Me.GetType(), "startup", sScript, True)

    End Sub
    'UnUseSerialにチェック切り替え
    Private Sub chkUnUseSerial_CheckedChanged(sender As Object, e As EventArgs) Handles chkUnUseSerial.CheckedChanged

        If chkUnUseSerial.Checked = True Then
            chkUseSerial.Checked = False
            btnImport.Enabled = True
            Textitem.Enabled = True
            TextQty.Enabled = True
            FileUploadInv.Enabled = False
            btnCsv.Enabled = False
        Else
            btnImport.Enabled = False
            Textitem.Text = ""
            TextQty.Text = ""
            Textitem.Enabled = False
            TextQty.Enabled = False
        End If

    End Sub
    'UseSerialにチェック切り替え
    Private Sub chkUseSerial_CheckedChanged(sender As Object, e As EventArgs) Handles chkUseSerial.CheckedChanged

        If chkUseSerial.Checked = True Then
            chkUnUseSerial.Checked = False
            FileUploadInv.Enabled = True
            btnCsv.Enabled = True
            btnImport.Enabled = False
            Textitem.Text = ""
            TextQty.Text = ""
            Textitem.Enabled = False
            TextQty.Enabled = False
        Else
            FileUploadInv.Enabled = False
            btnCsv.Enabled = False
        End If

    End Sub
    '部品単体管理のインポートボタン押下処理
    Protected Sub btnCsv_Click(sender As Object, e As ImageClickEventArgs) Handles btnCsv.Click

        Dim Msg As String = ""

        Dim errFlg As Integer

        'ファイルがアップロードされていれば
        If FileUploadInv.HasFile = True Then

            '***セッション情報取得***
            Dim userid As String = Session("user_id")
            Dim shipCode As String = Session("ship_code")
            Dim returnNo As String = Session("return_No")
            Dim deliveryNo As String = Session("delivery_No")
            Dim approvalNo As String = Session("approval_No")
            Dim dtNow As DateTime = Session("dt_now")
            Dim pushCsvCount = Session("push_CsvCount")
            Dim upCnt As Integer = Session("up_Cnt")
            Dim allCsvRowCount As Integer = Session("all_CsvRowCount")
            Dim fileChk() As String = Session("file_Chk")


            Dim partsStatus() As Class_inv.STATUS_Item
            If Session("parts_Status") IsNot Nothing Then
                partsStatus = Session("parts_Status")
            End If

            Dim updateItem() As String
            If Session("update_item") IsNot Nothing Then
                updateItem = Session("update_item")
            End If

            Dim dsCopy2_serial As New DataSet
            If Session("ds_copy_serial") IsNot Nothing Then
                dsCopy2_serial = Session("ds_copy_serial")
            End If

            '***入力チェック***
            If returnNo = "" Or returnNo Is Nothing Then
                Msg = "Please choose a return number."
                Call showMsg(Msg, "")
                Exit Sub
            End If

            If userid Is Nothing Then
                Msg = "The session has expired. Please login again."
                Call showMsg(Msg, "")
                Exit Sub
            End If

            '***カウント情報***
            'importボタン押下数(csvファイル取込回数)カウント
            pushCsvCount = pushCsvCount + 1

            '***画面項目等の制御***
            btnClose.Enabled = True
            BtnStart.Enabled = False

            '***インポートファイル(CSV)情報の取得***
            Dim FileName As String
            Dim fileNo As Integer = FreeFile()
            Dim csvRowCnt As Integer
            Dim i As Integer
            Dim colsHead(0) As String
            Dim clsSet As New Class_inv
            Dim delRow As Integer
            'サーバ側で保存するパス
            Dim savePass As String = "C:\inv\CSV\"

            Try
                'ファイル名取得 
                FileName = FileUploadInv.FileName & "_" & pushCsvCount

                'サーバの指定パスに保存
                FileUploadInv.SaveAs(savePass & FileName)

                If FileName <> "" Then

                    'インポートファイルの重複チェック
                    Dim sameItemFlg As Integer
                    If pushCsvCount <> 1 Then
                        For i = 0 To UBound(fileChk)
                            If fileChk(i) = FileName Then
                                Msg = "The same file name can not be specified. Processing was canceled"
                                Call showMsg(Msg, "")
                                Exit Sub
                            End If
                        Next
                    End If

                    ReDim Preserve fileChk(pushCsvCount - 1)
                    fileChk(pushCsvCount - 1) = FileName

                    'History欄へ設定
                    ListHistory.Items.Add("csv name:" & FileName)

                    FileOpen(fileNo, savePass & FileName, OpenMode.Input)

                    Do Until EOF(fileNo) 'ファイルの最後までループ 
                        ReDim Preserve CSV(csvRowCnt)
                        Input(fileNo, CSV(csvRowCnt).parts_no)
                        'Input(fileNo, CSV(csvRowCnt).qty)
                        If csvRowCnt = 0 And colsHead(0) = "" Then
                            colsHead(0) = CSV(csvRowCnt).parts_no
                            'ヘッダ確認
                            If clsSet.chkHead(colsHead, "Return") = False Then
                                delRow = ListHistory.Items.Count - 1
                                If delRow >= 0 Then
                                    ListHistory.Items.RemoveAt(delRow)
                                End If
                                Call showMsg("There is an error in header information of csv. Please check that the file to be imported is correct.", "")
                                Exit Sub
                            Else
                                csvRowCnt = -1
                            End If
                        End If
                        csvRowCnt += 1
                    Loop
                    FileClose(fileNo)
                End If

                If System.IO.File.Exists(clsSet.saveCsvPass & FileName) = True Then
                    System.IO.File.Delete(clsSet.saveCsvPass & FileName)
                End If

            Catch ex As Exception
                Call showMsg("Failed to read csv", "")
                delRow = ListHistory.Items.Count - 1
                If delRow >= 0 Then
                    ListHistory.Items.RemoveAt(delRow)
                End If
                Exit Sub
            End Try

            '***アイテムとステータスを取得***
            '※アイテムの存在チェック含む
            '※アイテムなければ、"noItem"、ステータスがなければ、0をwork用配列（partsStatus）に設定
            For i = 0 To csvRowCnt - 1
                ReDim Preserve partsStatus(i + allCsvRowCount)
                Dim strSQL As String = ""
                strSQL = "SELECT * FROM T_inParts WHERE DELFG = 0 AND ship_code = '" & shipCode & "' AND parts_serial = '" & CSV(i).parts_no & "';"
                Dim DT_T_inParts As DataTable = DBCommon.ExecuteGetDT(strSQL, errFlg)

                If errFlg = 1 Then
                    Call ReSetAll()
                    Call reSetSession()
                    Session("all_Kinditem") = 0
                    Session("all_Qty") = 0
                    Session("parts_Status") = Nothing
                    Call showMsg("Failed to acquire information from the DB.", "")
                    Exit Sub
                End If

                If DT_T_inParts IsNot Nothing Then
                    If DT_T_inParts.Rows(0)("parts_status") Is DBNull.Value Then
                        partsStatus(i + allCsvRowCount).item = CSV(i).parts_no
                        partsStatus(i + allCsvRowCount).status = "0"
                        partsStatus(i + allCsvRowCount).filename = FileName
                    Else
                        partsStatus(i + allCsvRowCount).item = CSV(i).parts_no
                        partsStatus(i + allCsvRowCount).status = DT_T_inParts.Rows(0)("parts_status")
                        partsStatus(i + allCsvRowCount).filename = FileName
                    End If
                Else
                    partsStatus(i + allCsvRowCount).item = CSV(i).parts_no
                    partsStatus(i + allCsvRowCount).status = "noItem"
                    partsStatus(i + allCsvRowCount).filename = FileName
                End If

                'メーカ情報を取得
                Dim itemName As String
                Dim itemNameSplit() As String
                itemNameSplit = Split(CSV(i).parts_no, "-")
                If itemNameSplit(0) <> "" Then
                    Dim strSQL2 As String = "SELECT * FROM dbo.M_PARTS WHERE DELFG = 0 AND ship_code = '" & shipCode & "' AND parts_no = '" & itemNameSplit(0) & "' AND parts_flg = 1;"
                    Dim DT_M_PARTS As DataTable = DBCommon.ExecuteGetDT(strSQL2, errFlg)

                    If errFlg = 1 Then
                        Call ReSetAll()
                        Call reSetSession()
                        Session("all_Kinditem") = 0
                        Session("all_Qty") = 0
                        Session("parts_Status") = Nothing
                        Call showMsg("Failed to acquire information from the DB.", "")
                        Exit Sub
                    End If

                    If DT_M_PARTS IsNot Nothing Then
                        If DT_M_PARTS.Rows(0)("maker") IsNot DBNull.Value Then
                            partsStatus(i + allCsvRowCount).maker = DT_M_PARTS.Rows(0)("maker")
                        End If
                    End If
                End If

            Next i

            '***データセットにitem(部品)内容を設定***
            'コネクションを取得
            Dim con As SqlConnection = New SqlConnection(ConfigurationManager.ConnectionStrings("cnstr").ConnectionString)
            con.Open()

            Try
                '■T_ReturnParts空テーブル取得
                Dim select_sql1 As String = ""
                select_sql1 &= "SELECT * FROM dbo.T_ReturnParts WHERE parts_no IS NULL;"
                Dim sqlSelect1 As New SqlCommand(select_sql1, con)
                Dim Adapter1 As New SqlDataAdapter(sqlSelect1)
                Dim ds1 As New DataSet

                Adapter1.Fill(ds1)

                For i = 0 To csvRowCnt - 1
                    If partsStatus(i + allCsvRowCount).status = "2" Then
                        ReDim Preserve updateItem(upCnt)
                        updateItem(upCnt) = CSV(i).parts_no
                        '新規DR取得
                        Dim dr1 As DataRow = ds1.Tables(0).NewRow
                        '登録内容設定
                        dr1("CRTDT") = Now
                        dr1("CRTCD") = userid
                        dr1("UPDDT") = Now
                        dr1("UPDCD") = userid
                        dr1("UPDPG") = "systemName"
                        dr1("DELFG") = 0
                        dr1("maker") = partsStatus(i + allCsvRowCount).maker
                        dr1("parts_no") = CSV(i).parts_no
                        dr1("start_time") = dtNow
                        dr1("close_time") = dtNow
                        dr1("number") = 1
                        dr1("deli_no") = deliveryNo
                        dr1("ss_return") = approvalNo
                        dr1("ship_code") = shipCode
                        dr1("return_no") = returnNo
                        dr1("file_name") = FileName
                        '新規DRをDataTableに追加
                        ds1.Tables(0).Rows.Add(dr1)
                        upCnt = upCnt + 1
                    End If
                Next i

                '入荷CSVインポート作業１回目
                If pushCsvCount = 1 Then

                    'データセットのコピー(値なし)
                    Dim dsCopy_serial As New DataSet
                    dsCopy_serial = ds1.Clone
                    Dim customerTable_serial As DataTable = dsCopy_serial.Tables(0)

                    For i = 0 To ds1.Tables(0).Rows.Count - 1
                        '新しいデータセットに1回目の入荷情報をコピー
                        Dim drCopy_serial As DataRow = ds1.Tables(0).Rows(i)
                        customerTable_serial.ImportRow(drCopy_serial)
                    Next i
                    Session("ds_copy_serial") = dsCopy_serial

                Else

                    Dim customerTable_serial As DataTable = dsCopy2_serial.Tables(0)

                    For i = 0 To ds1.Tables(0).Rows.Count - 1
                        'セッション管理されているデータセットに入荷情報を追加コピー
                        Dim drCopy_serial As DataRow = ds1.Tables(0).Rows(i)
                        customerTable_serial.ImportRow(drCopy_serial)
                    Next i
                    Session("ds_copy_serial") = dsCopy2_serial

                End If

                '***セッション情報を設定***
                Session("update_Item") = updateItem
                Session("up_Cnt") = upCnt
                Session("file_Name") = FileName
                Session("push_CsvCount") = pushCsvCount
                Session("parts_Status") = partsStatus
                Session("all_CsvRowCount") = allCsvRowCount + csvRowCnt
                Session("file_Chk") = fileChk

                Call showMsg("success!!", "LastMsg")

            Catch ex As Exception
                Call ReSetAll()
                Call reSetSession()
                Session("all_Kinditem") = 0
                Session("all_Qty") = 0
                Session("parts_Status") = Nothing
                Call showMsg("Data Import Error Please retry.", "")
            Finally
                'DB接続クローズ
                If con.State <> ConnectionState.Closed Then
                    con.Close()
                End If
            End Try

        Else
            Msg = "Please specify the import file."
            Call showMsg(Msg, "")
            Exit Sub
        End If
    End Sub

    Private Sub ReSetAll()

        '再度スタートボタン押下できる状態
        chkUseSerial.Enabled = True
        chkUnUseSerial.Enabled = True
        btnClose.Enabled = False
        BtnStart.Enabled = True
        TextDeliveryNo.Text = ""
        TextApprovalNo.Text = ""
        lblReturnNnumber.Text = ""
        lblDeliveryNumber.Text = ""
        lblCurrentReturn.Visible = False
        lblCurrentDelivery.Visible = False
        ListHistory.Items.Clear()

    End Sub

    Private Sub BtnCancel_Click(sender As Object, e As EventArgs) Handles BtnCancel.Click


    End Sub
End Class