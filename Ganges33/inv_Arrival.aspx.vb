Imports iTextSharp.text
Imports iTextSharp.text.pdf
Imports iFont = iTextSharp.text.Font
Imports System.Data.SqlClient
Imports System.IO
Imports System.Text
Imports System.Net.Mail
Public Class inv_Arrival
    Inherits System.Web.UI.Page
    Private Structure CSV_File
        Dim parts_no As String
        Dim qty As String
        Dim loc_1 As String
        Dim loc_2 As String
        Dim loc_3 As String
        Dim parts_name As String
        Dim maker As String
    End Structure
    Private CSV() As CSV_File
    Private Structure KosuKanri
        Dim parts_no As String
        Dim parts_name As String
        Dim qty As String
    End Structure
    'シリアルなしの部品番号を個数分セット
    Private barCode() As KosuKanri
    'シリアルなしの既存部品番号と個数をセット（※新規アイテムでも、入荷作業時２回目以降の部品を含む。）
    Private KosuUpdate() As KosuKanri
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
            lblCurrentArrival.Visible = False
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

            If (btnCloseChk <> "btnCloseOn") And (BtnCancelchk = "BtnCancelOn") Then

                'Dim errCancelMsg As String = Page.Request.QueryString.Get("errCancelMsg")
                'Dim errLastMsg As String = Page.Request.QueryString.Get("errLastMsg")
                Dim item As String = Session("item_No")
                Dim itemChk() As String = Session("item_Chk")
                Dim FileName As String = Session("file_Name")
                Dim errorItem() As Class_inv.ERROR_Item
                If Session("error_Item") IsNot Nothing Then
                    errorItem = Session("error_Item")
                End If

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

                '■T_inParts_arrival 'データセット削除
                If Session("ds_copy") IsNot Nothing Then
                    Dim dsCopy2 As New DataSet
                    Dim dr As DataRow
                    dsCopy2 = Session("ds_copy")
                    dr = dsCopy2.Tables(0).Rows(dsCopy2.Tables(0).Rows.Count - 1)
                    dr.Delete()
                    Session("ds_copy") = dsCopy2
                End If

                '■T_inParts_2 'データセット削除 又はアイテムと個数の紐付け用構造体の該当データをNULLに設定
                Dim dr_kosu As DataRow
                Dim del_flg As Integer

                If Session("Kosu_Update") IsNot Nothing Then
                    KosuUpdate = Session("Kosu_Update")
                    '同じアイテムがあればアイテムと個数の紐付け配列の該当データ削除
                    If KosuUpdate(UBound(KosuUpdate)).parts_no = item Then
                        KosuUpdate(UBound(KosuUpdate)).parts_no = ""
                        KosuUpdate(UBound(KosuUpdate)).qty = ""
                        KosuUpdate(UBound(KosuUpdate)).parts_name = ""
                        del_flg = 1
                    End If
                End If

                If Session("ds_copy_kosu") IsNot Nothing Then
                    Dim dsCopy2_kosu As New DataSet
                    dsCopy2_kosu = Session("ds_copy_kosu")
                    If del_flg <> 1 Then
                        dr_kosu = dsCopy2_kosu.Tables(0).Rows(dsCopy2_kosu.Tables(0).Rows.Count - 1)
                        If dr_kosu("parts_no") = item Then
                            dr_kosu.Delete()
                        End If
                    End If
                    Session("ds_copy_kosu") = dsCopy2_kosu
                End If

                '***csvでインポート時のキャンセル処理***
                If errorItem IsNot Nothing Then
                    For i = 0 To UBound(errorItem)
                        If errorItem(i).fileName = FileName Then
                            errorItem(i).fileName = "cancel"
                        End If
                    Next i
                End If

                '■T_inParts_arrival 'データセット削除
                If Session("ds_copy_serial_arrival") IsNot Nothing Then

                    Dim dsCopy2_serial_arrival As New DataSet
                    dsCopy2_serial_arrival = Session("ds_copy_serial_arrival")
                    For i = 0 To dsCopy2_serial_arrival.Tables(0).Rows.Count - 1
                        If dsCopy2_serial_arrival.Tables(0).Rows.Count <> 0 And i < dsCopy2_serial_arrival.Tables(0).Rows.Count Then
                            Dim dr As DataRow = dsCopy2_serial_arrival.Tables(0).Rows(i)
                            If dr("file_name") = FileName Then
                                dr.Delete()
                                i = i - 1
                            End If
                        End If
                    Next i
                    Session("ds_copy_serial_arrival") = dsCopy2_serial_arrival

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

                'キャンセル処理共通***
                '■後始末
                ' 次のポストバックでこのルートを通らないようにするために
                ' キャンセル処理のために追加したGET パラメータ(errCancelMsg)をaction属性から除去する
                Dim formid As String = Me.Form.ClientID
                Dim cScript As String = formid + ".action = ""inv_Arrival.aspx"";"
                'JavaScriptの埋め込み（画面表示前に実行させる）
                ClientScript.RegisterClientScriptBlock(Me.GetType(), "clientscript", cScript, True)

                'セッション設定
                Session("Kosu_Update") = KosuUpdate
                Session("item_Chk") = itemChk
                Session("error_Item") = errorItem

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
        '請求番号
        Dim invoiceNo As String = Trim(TextInvoiceNo.Text)
        'PO番号
        'Dim poNo As String = Trim(TextPONo.Text)

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
            msg = "Please enter your delivery number."
            Call showMsg(msg, "")
            Exit Sub
        End If

        If invoiceNo = "" Then
            msg = "Please enter your billing number."
            Call showMsg(msg, "")
            Exit Sub
        End If

        '***画面項目等の制御***
        lblCurrentArrival.Visible = True
        lblCurrentDelivery.Visible = True
        BtnStart.Enabled = False
        chkUseSerial.Enabled = False
        chkUnUseSerial.Enabled = False

        '***設定***
        Dim dtNow As DateTime = DateTime.Now

        '入荷番号
        Dim arrivalNo As String = dtNow.ToString("yyyyMMddHHmmss")

        'セッション情報
        Session("arrival_No") = arrivalNo
        Session("delivery_No") = deliveryNo
        Session("invoice_No") = invoiceNo
        'Session("po_No") = poNo
        Session("dt_now") = dtNow

        '***表示***
        lblArrivalNnumber.Text = arrivalNo
        lblDeliveryNumber.Text = deliveryNo

    End Sub
    'importボタン押下処理
    Protected Sub btnImport_Click(sender As Object, e As ImageClickEventArgs) Handles btnImport.Click

        '***セッション情報取得***
        Dim userid As String = Session("user_id")
        Dim shipCode As String = Session("ship_code")
        Dim arrivalNo As String = Session("arrival_No")
        Dim deliveryNo As String = Session("delivery_No")
        Dim invoiceNo As String = Session("invoice_No")
        'Dim poNo As String = Session("po_no")
        Dim pushCount As Integer = Session("push_Count")
        'startボタン押下時のtime
        Dim dtNow As DateTime = Session("dt_now")
        Dim qtyCountKosu As Integer = Session("qtyCount_Kosu")

        If Session("Kosu_Update") IsNot Nothing Then
            KosuUpdate = Session("Kosu_Update")
        End If

        Dim dsCopy2 As New DataSet
        If Session("ds_copy") IsNot Nothing Then
            dsCopy2 = Session("ds_copy")
        End If

        Dim dsCopy2_kosu As New DataSet
        If Session("ds_copy_kosu") IsNot Nothing Then
            dsCopy2_kosu = Session("ds_copy_kosu")
        End If

        Dim dsCopy2_kosu_qty As New DataSet
        If Session("ds_copy_kosu_qty") IsNot Nothing Then
            dsCopy2_kosu_qty = Session("ds_copy_kosu_qty")
        End If

        Dim itemChk() As String
        If Session("item_Chk") IsNot Nothing Then
            itemChk = Session("item_Chk")
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

        If arrivalNo = "" Or arrivalNo Is Nothing Then
            msg = "Please select an arrival number."
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

        '***画面項目等の制御***
        btnClose.Enabled = True
        BtnStart.Enabled = False

        '***各設定値取得***
        '部品の棚情報取得
        Dim strSQL = "SELECT * FROM dbo.M_PARTS WHERE parts_no = '" & item & "' AND parts_flg = 0 AND DELFG = 0 AND ship_code = '" & shipCode & "';"
        Dim loc_1 As String
        Dim loc_2 As String
        Dim loc_3 As String
        Dim itemName As String
        Dim maker As String
        Dim errFlg As Integer

        Dim DT_M_PARTS As DataTable = DBCommon.ExecuteGetDT(strSQL, errFlg)

        If errFlg = 1 Then
            Call reSetSession()
            Session("all_Kinditem") = 0
            Session("all_Qty") = 0
            Session("err_Flg") = Nothing
            Session("error_Item") = Nothing
            If pushCount <> 1 Then
                Call reSetAll()
            End If
            Call showMsg("Failed to acquire information from the DB.", "")
            Exit Sub
        End If

        If DT_M_PARTS IsNot Nothing Then

            If DT_M_PARTS.Rows(0)("loc_1") IsNot DBNull.Value Then
                loc_1 = DT_M_PARTS.Rows(0)("loc_1")
            End If

            If DT_M_PARTS.Rows(0)("loc_2") IsNot DBNull.Value Then
                loc_2 = DT_M_PARTS.Rows(0)("loc_2")
            End If

            If DT_M_PARTS.Rows(0)("loc_3") IsNot DBNull.Value Then
                loc_3 = DT_M_PARTS.Rows(0)("loc_3")
            End If

            If DT_M_PARTS.Rows(0)("parts_name") IsNot DBNull.Value Then
                itemName = DT_M_PARTS.Rows(0)("parts_name")
            End If

            If DT_M_PARTS.Rows(0)("maker") IsNot DBNull.Value Then
                maker = DT_M_PARTS.Rows(0)("maker")
            End If
        Else
            Call showMsg("Processing stops because the location can not be acquired. Please check the part number.", "")
            '続きの登録可能の為、セッション残す
            Exit Sub
        End If

        ReDim Preserve itemChk(pushCount - 1)
        itemChk(pushCount - 1) = item

        '***アイテムの存在チェック***
        strSQL = "SELECT * FROM dbo.T_inParts_2 WHERE parts_no = '" & item & "';"
        Dim DT_T_inParts_2 As DataTable = DBCommon.ExecuteGetDT(strSQL, errFlg)

        If errFlg = 1 Then
            Call reSetSession()
            Session("all_Kinditem") = 0
            Session("all_Qty") = 0
            Session("err_Flg") = Nothing
            Session("error_Item") = Nothing
            If pushCount <> 1 Then
                Call reSetAll()
            End If
            Call showMsg("Failed to acquire information from the DB.", "")
            Exit Sub
        End If

        '***History欄へ設定***
        ListHistory.Items.Add("item no:" & item & "  " & "qty:" & qty & "  " & dtNow)

        '***データセットにitem内容を設定***
        'コネクションを取得
        Dim con As SqlConnection = New SqlConnection(ConfigurationManager.ConnectionStrings("cnstr").ConnectionString)
        con.Open()

        Try
            '■T_inParts_arrival空テーブル取得
            Dim select_sql1 As String = ""
            select_sql1 &= "SELECT * FROM dbo.T_inParts_arrival WHERE unq IS NULL;"
            Dim sqlSelect1 As New SqlCommand(select_sql1, con)
            Dim Adapter1 As New SqlDataAdapter(sqlSelect1)
            Dim ds1 As New DataSet

            Adapter1.Fill(ds1)

            '新規DR取得
            Dim dr1 As DataRow = ds1.Tables(0).NewRow

            '登録内容設定
            dr1("CRTDT") = Now
            dr1("CRTCD") = userid
            dr1("UPDDT") = Now
            dr1("UPDCD") = userid
            dr1("UPDPG") = "systemName"
            dr1("DELFG") = 0
            dr1("maker") = maker
            dr1("arrival_no") = arrivalNo
            dr1("parts_no") = item
            dr1("deli_date") = dtNow
            dr1("parts_serial") = "test"
            dr1("deli_no") = deliveryNo
            dr1("invo_no") = invoiceNo
            dr1("ship_code") = shipCode

            '新規DRをDataTableに追加
            ds1.Tables(0).Rows.Add(dr1)

            '入荷作業１回目
            If pushCount = 1 Then

                '■T_inParts_arrival
                'データセットのコピー(値なし)
                Dim dsCopy As New DataSet
                dsCopy = ds1.Clone
                Dim customerTable As DataTable = dsCopy.Tables(0)
                Dim drCopy As DataRow = ds1.Tables(0).Rows(0)

                '新しいデータセットに入荷情報をコピー
                customerTable.ImportRow(drCopy)
                Session("ds_copy") = dsCopy

            Else

                '■T_inParts_arrival
                Dim customerTable As DataTable = dsCopy2.Tables(0)
                Dim drCopy As DataRow = ds1.Tables(0).Rows(0)

                'セッション管理されているデータセットに入荷情報を追加コピー
                customerTable.ImportRow(drCopy)
                Session("ds_copy") = dsCopy2

            End If

            '新規アイテムは、データセットに追加
            If DT_T_inParts_2 Is Nothing Then

                '■T_inParts_2空テーブル取得
                Dim select_sql2 As String = ""
                select_sql2 &= "SELECT * FROM dbo.T_inParts_2 WHERE arrival_no IS NULL;"

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
                dr2("deli_date") = dtNow
                dr2("qty") = Convert.ToInt32(qty)
                dr2("parts_unuse") = Convert.ToInt32(qty)
                dr2("deli_no") = deliveryNo
                dr2("invo_no") = invoiceNo
                'dr2("po_no") = poNo
                dr2("ship_code") = shipCode
                dr2("arrival_no") = arrivalNo
                dr2("loc_1") = loc_1
                dr2("loc_2") = loc_2
                dr2("loc_3") = loc_3
                dr2("parts_name") = itemName

                '新規DRをDataTableに追加
                ds2.Tables(0).Rows.Add(dr2)

                '入荷作業１回目
                If Session("ds_copy_kosu") Is Nothing Then

                    '■T_inParts_2(個数管理)
                    'データセットのコピー(値なし)
                    Dim dsCopy_kosu As New DataSet
                    dsCopy_kosu = ds2.Clone
                    Dim customerTable_kosu As DataTable = dsCopy_kosu.Tables(0)
                    Dim drCopy_kosu As DataRow = ds2.Tables(0).Rows(0)

                    '新しいデータセットに入荷情報をコピー
                    customerTable_kosu.ImportRow(drCopy_kosu)
                    Session("ds_copy_kosu") = dsCopy_kosu

                Else

                    '■T_inParts_2(個数管理)

                    '新規アイテムでインポート済か確認（データセットに反映済か）
                    Dim itemWChk As Integer = 0
                    For i = 0 To dsCopy2_kosu.Tables(0).Rows.Count - 1
                        Dim dr3 As DataRow = dsCopy2_kosu.Tables(0).Rows(i)
                        If dr3("parts_no") = item Then
                            itemWChk = 1
                            Exit For
                        End If
                    Next i

                    'セッション管理されているデータセットに入荷情報を追加コピー
                    If itemWChk <> 1 Then
                        Dim customerTable_kosu As DataTable = dsCopy2_kosu.Tables(0)
                        Dim drCopy_kosu As DataRow = ds2.Tables(0).Rows(0)
                        customerTable_kosu.ImportRow(drCopy_kosu)
                        Session("ds_copy_kosu") = dsCopy2_kosu
                    Else
                        '新規アイテムでインポート済であれば、データセットではなくて配列（アイテムとupdateする個数の紐づけ）に保存
                        ReDim Preserve KosuUpdate(qtyCountKosu)
                        KosuUpdate(qtyCountKosu).parts_no = item
                        KosuUpdate(qtyCountKosu).qty = qty
                        KosuUpdate(qtyCountKosu).parts_name = itemName
                        qtyCountKosu = qtyCountKosu + 1
                    End If

                End If

            Else
                '既存アイテムは配列（アイテムとupdateする個数の紐づけ）に保存
                ReDim Preserve KosuUpdate(qtyCountKosu)
                KosuUpdate(qtyCountKosu).parts_no = item
                KosuUpdate(qtyCountKosu).qty = qty
                KosuUpdate(qtyCountKosu).parts_name = itemName
                qtyCountKosu = qtyCountKosu + 1

            End If

            If sameItemFlg = 1 Then
                Call showMsg(msg, "CancelMsg")
            Else
                Call showMsg("success!!", "LastMsg")
            End If

            '***セッション設定***
            Session("item_No") = item
            Session("qty_No") = qty
            Session("push_Count") = pushCount
            Session("item_Chk") = itemChk
            Session("qtyCount_Kosu") = qtyCountKosu
            Session("Kosu_Update") = KosuUpdate

        Catch ex As Exception
            Call reSetSession()
            Session("all_Kinditem") = 0
            Session("all_Qty") = 0
            Session("err_Flg") = Nothing
            Session("error_Item") = Nothing
            Call reSetAll()
            Call showMsg("Data Import Error Please retry.", "")
        Finally
            'DB接続クローズ
            If con.State <> ConnectionState.Closed Then
                con.Close()
            End If
        End Try

    End Sub
    'メッセージ出力
    Protected Sub showMsg(ByVal Msg As String, ByVal msgChk As String)

        'Dim formid As String = Me.Form.ClientID

        ' 確認ダイアログを出力するスクリプト
        ' POST先は自分自身(****.aspx)

        Dim sScript As String

        If msgChk = "LastMsg" Then
            'sScript = "if(confirm(""" + Msg + """)){ " +
            '                               formid + ".method = ""post"";" +
            '                               formid + ".action = ""inv_Arrival.aspx?errLastMsg=true"";" +
            '                               formid + ".submit();" +
            '           "}else{" +
            '                               formid + ".method = ""post"";" +
            '                               formid + ".action = ""inv_Arrival.aspx?errLastMsg=false"";" +
            '                               formid + ".submit();" +
            '           "}"
            sScript = "$(function() {$( ""#dialog"" ).dialog({width: 400,buttons:{""OK"": function(){$(this).dialog('close');},""CANCEL"": function (){$(this).dialog('close');$('[id$=""BtnCancel""]').click();}}});});"
        ElseIf msgChk = "CancelMsg" Then
            'sScript = "if(confirm(""" + Msg + """)){ " +
            '                               formid + ".method = ""post"";" +
            '                               formid + ".action = ""inv_Arrival.aspx?errCancelMsg=true"";" +
            '                               formid + ".submit();" +
            '           "}else{" +
            '                               formid + ".method = ""post"";" +
            '                               formid + ".action = ""inv_Arrival.aspx?errCancelMsg=false"";" +
            '                               formid + ".submit();" +
            '           "}"
            '$(function() {$( ""#dialog"" ).dialog({width: 400,buttons: {""OK"": function(){$(this).dialog('close');},""CANCEL"": function () {$(this).dialog('close');$('[id$="BtnCancel"]').click();}}});});
            sScript = "$(function() {$( ""#dialog"" ).dialog({width: 400,buttons:{""OK"": function(){$(this).dialog('close');},""CANCEL"": function (){$(this).dialog('close');$('[id$=""BtnCancel""]').click();}}});});"
        Else
            'sScript = "if(alert(""" + Msg + """)){ " +
            '                               formid + ".method = ""post"";" +
            '                               formid + ".action = ""inv_Arrival.aspx?errMsg=true"";" +
            '                               formid + ".submit();" +
            '           "}"
            'OKボタンのみのダイアログ
            sScript = "$(function () { $( ""#dialog"" ).dialog({width: 400, buttons: {""OK"": function () {$(this).dialog('close');}}});});"
        End If

        lblMsg.Text = Msg

        'JavaScriptの埋め込み
        ClientScript.RegisterStartupScript(Me.GetType(), "startup", sScript, True)

    End Sub
    'closeボタン押下処理
    Protected Sub btnClose_Click(sender As Object, e As ImageClickEventArgs) Handles btnClose.Click

        '***セッション情報を取得***
        Dim arrivalNo As String = Session("arrival_No")

        Dim dsCopy2 As New DataSet
        If Session("ds_copy") IsNot Nothing Then
            dsCopy2 = Session("ds_copy")
        End If

        Dim dsCopy2_kosu As New DataSet
        If Session("ds_copy_kosu") IsNot Nothing Then
            dsCopy2_kosu = Session("ds_copy_kosu")
        End If

        Dim dsCopy_tantai As New DataSet
        If Session("ds_copy_serial") IsNot Nothing Then
            dsCopy_tantai = Session("ds_copy_serial")
        End If

        Dim dsCopy_tantai_arrival As New DataSet
        If Session("ds_copy_serial_arrival") IsNot Nothing Then
            dsCopy_tantai_arrival = Session("ds_copy_serial_arrival")
        End If

        If Session("Kosu_Update") IsNot Nothing Then
            KosuUpdate = Session("Kosu_Update")
        End If

        '最終画面の集計用
        Dim allKindItem As Integer
        Dim allQty As Integer
        Dim partsNo() As String '新規アイテム名(※アイテム種類カウントの重複チェック用)
        Dim chkTmpItem() As String 'ユニークのアイテムを設定

        '***画面項目等の制御***
        BtnStart.Enabled = True

        '***設定***
        Dim dtNowClose As DateTime = DateTime.Now
        Dim errFlg As Integer = 0  'バーコードPDF出力失敗フラグ

        Dim i, j, m As Integer

        '***入荷情報を反映***
        'コネクションを取得
        Dim con As SqlConnection = New SqlConnection(ConfigurationManager.ConnectionStrings("cnstr").ConnectionString)
        con.Open()

        'トランザクション開始＆コネクションオープン
        Dim trn As SqlTransaction = con.BeginTransaction(IsolationLevel.ReadCommitted)

        Try
            '■T_inParts_arrival(バーコードより読み込み)へ反映
            If Session("ds_copy") IsNot Nothing Then

                If dsCopy2.Tables(0).Rows.Count <> 0 Then
                    '空テーブル取得
                    Dim select_sql1 As String = ""
                    select_sql1 &= "SELECT * FROM dbo.T_inParts_arrival WHERE unq IS NULL;"

                    Dim sqlSelect1 As New SqlCommand(select_sql1, con, trn)
                    Dim Adapter1 As New SqlDataAdapter(sqlSelect1)
                    Dim Builder1 As New SqlCommandBuilder(Adapter1)

                    Adapter1.Fill(dsCopy2)
                    Adapter1.Update(dsCopy2)
                End If

            End If

            '■T_inParts_2(個数管理)へ反映 新規アイテム追加　
            If Session("ds_copy_kosu") IsNot Nothing Then

                If dsCopy2_kosu.Tables(0).Rows.Count <> 0 Then

                    '空テーブル取得
                    Dim select_sql2 As String = ""
                    select_sql2 &= "SELECT * FROM dbo.T_inParts_2 WHERE arrival_no IS NULL;"

                    Dim sqlSelect2 As New SqlCommand(select_sql2, con, trn)
                    Dim Adapter2 As New SqlDataAdapter(sqlSelect2)
                    Dim Builder2 As New SqlCommandBuilder(Adapter2)

                    Adapter2.Fill(dsCopy2_kosu)
                    Adapter2.Update(dsCopy2_kosu)

                    '個数、アイテム名の取得
                    allKindItem = dsCopy2_kosu.Tables(0).Rows.Count
                    For i = 0 To dsCopy2_kosu.Tables(0).Rows.Count - 1
                        '件数
                        Dim dr2 As DataRow = dsCopy2_kosu.Tables(0).Rows(i)
                        allQty = allQty + dr2("qty")
                        'アイテム名
                        ReDim Preserve partsNo(i)
                        partsNo(i) = dr2("parts_no")
                    Next i

                End If

            End If

            '■T_inParts_2(個数管理)へ反映 既存アイテムへ個数追加
            If Session("Kosu_Update") IsNot Nothing Then

                '既存の返却個数を取得して設定
                For i = 0 To UBound(KosuUpdate)

                    '※インポートキャンセル時はNULLが設定されているため、確認する。
                    If KosuUpdate(i).parts_no <> "" Then

                        Dim select_sql0 As String = ""
                        Dim tmp As String = ""
                        Dim tmp2 As String = ""
                        select_sql0 &= "SELECT * FROM dbo.T_inParts_2 WHERE parts_no = '" & KosuUpdate(i).parts_no & "';"

                        Dim sqlSelect0 As New SqlCommand(select_sql0, con, trn)
                        Dim Adapter0 As New SqlDataAdapter(sqlSelect0)
                        Dim Builder0 As New SqlCommandBuilder(Adapter0)
                        Dim ds0 As New DataSet
                        Adapter0.Fill(ds0)

                        '新規DR取得
                        If ds0.Tables(0).Rows.Count = 1 Then

                            Dim dr0 As DataRow = ds0.Tables(0).Rows(0)

                            If dr0("qty") IsNot DBNull.Value Then
                                tmp = dr0("qty")
                            End If

                            If dr0("parts_unuse") IsNot DBNull.Value Then
                                tmp2 = dr0("parts_unuse")
                            End If

                            dr0("qty") = Convert.ToInt32(KosuUpdate(i).qty) + Convert.ToInt32(tmp)

                            dr0("parts_unuse") = Convert.ToInt32(KosuUpdate(i).qty) + Convert.ToInt32(tmp2)

                            dr0("UPDDT") = dtNowClose

                            '更新
                            Adapter0.Update(ds0)

                            '件数取得
                            allQty = allQty + KosuUpdate(i).qty

                        Else
                            'ここにくることはないはず。
                            Call showMsg("Failed to additionally register number", "")
                            Call reSetAll()
                            Call reSetSession()
                            Exit Sub
                        End If

                    End If

                Next i

                'アイテム種類の取得
                Dim cnt As Integer
                Dim cnt2 As Integer

                For i = 0 To UBound(KosuUpdate)

                    If KosuUpdate(i).parts_no <> "" Then

                        cnt2 = 0

                        If i = 0 Then
                            ReDim Preserve chkTmpItem(cnt)
                            chkTmpItem(cnt) = KosuUpdate(i).parts_no
                            cnt = cnt + 1
                        Else

                            'アイテムの重複確認
                            If chkTmpItem IsNot Nothing Then
                                For m = 0 To UBound(chkTmpItem)
                                    If KosuUpdate(i).parts_no = chkTmpItem(m) Then
                                        cnt2 = cnt2 + 1
                                        Exit For
                                    End If
                                Next m
                            End If

                            '重複なければ、chkTmpItemにアイテムを設定
                            If cnt2 = 0 Then
                                ReDim Preserve chkTmpItem(cnt)
                                chkTmpItem(cnt) = KosuUpdate(i).parts_no
                                cnt = cnt + 1
                            End If

                        End If

                    End If

                Next i

                '新規アイテムとの重複確認
                cnt = 0
                If partsNo IsNot Nothing Then
                    For i = 0 To UBound(partsNo)
                        For j = 0 To UBound(chkTmpItem)
                            If partsNo(i) = chkTmpItem(j) Then
                                cnt = cnt + 1
                                Exit For
                            End If
                        Next j
                    Next i
                End If

                'アイテム種類数の設定
                If chkTmpItem IsNot Nothing Then
                    allKindItem = allKindItem + chkTmpItem.Length - cnt
                End If

            End If

            '■T_inParts(単体管理)へ反映
            If Session("ds_copy_serial") IsNot Nothing Then

                If dsCopy_tantai.Tables(0).Rows.Count <> 0 Then

                    '総個数取得
                    allQty = dsCopy_tantai.Tables(0).Rows.Count

                    '空テーブル取得
                    Dim select_sql3 As String = ""
                    select_sql3 &= "SELECT * FROM dbo.T_inParts WHERE parts_serial IS NULL;"

                    Dim sqlSelect3 As New SqlCommand(select_sql3, con, trn)
                    Dim Adapter3 As New SqlDataAdapter(sqlSelect3)
                    Dim Builder3 As New SqlCommandBuilder(Adapter3)

                    Adapter3.Fill(dsCopy_tantai)
                    Adapter3.Update(dsCopy_tantai)

                    'アイテム種類の取得
                    Dim cnt As Integer
                    Dim cnt2 As Integer

                    For i = 0 To dsCopy_tantai.Tables(0).Rows.Count - 1

                        Dim dr As DataRow = dsCopy_tantai.Tables(0).Rows(i)

                        cnt2 = 0

                        If i = 0 Then
                            ReDim Preserve chkTmpItem(cnt)
                            chkTmpItem(cnt) = dr("parts_no")
                            cnt = cnt + 1

                        Else

                            'アイテムの重複確認
                            If chkTmpItem IsNot Nothing Then
                                For m = 0 To UBound(chkTmpItem)
                                    If dr("parts_no") = chkTmpItem(m) Then
                                        cnt2 = cnt2 + 1
                                        Exit For
                                    End If
                                Next m
                            End If

                            '重複なければ、chkTmpItemにアイテムを設定
                            If cnt2 = 0 Then
                                ReDim Preserve chkTmpItem(cnt)
                                chkTmpItem(cnt) = dr("parts_no")
                                cnt = cnt + 1
                            End If

                        End If

                    Next i

                    'アイテム種類数の設定
                    If chkTmpItem IsNot Nothing Then
                        allKindItem = chkTmpItem.Length
                    End If

                End If

            End If

            '■T_inParts_arrival(CSVより読み込み)へ反映
            If Session("ds_copy_serial_arrival") IsNot Nothing Then

                If dsCopy_tantai_arrival.Tables(0).Rows.Count <> 0 Then

                    '空テーブル取得
                    Dim select_sql4 As String = ""
                    select_sql4 &= "SELECT * FROM dbo.T_inParts_arrival WHERE unq IS NULL;"

                    Dim sqlSelect4 As New SqlCommand(select_sql4, con, trn)
                    Dim Adapter4 As New SqlDataAdapter(sqlSelect4)
                    Dim Builder4 As New SqlCommandBuilder(Adapter4)

                    Adapter4.Fill(dsCopy_tantai_arrival)
                    Adapter4.Update(dsCopy_tantai_arrival)

                End If

            End If

            trn.Commit()

        Catch ex As Exception
            trn.Rollback()
            Session("all_Kinditem") = 0
            Session("all_Qty") = 0
            Session("error_Item") = Nothing
            Call reSetAll()
            Call reSetSession()
            Call showMsg("Data Import Error Please retry.", "")
            Exit Sub
        Finally
            'DB接続クローズ
            If con.State <> ConnectionState.Closed Then
                con.Close()
            End If
        End Try

        '***結果処理***
        If allKindItem <> 0 Or allQty <> 0 Then

            'PDF出力ファイル名
            Dim pdfFileName As String = DateTime.Now.ToString("yyyyMMddHHmmss") & "_barcode.pdf"

            'シリアルなし
            If chkUnUseSerial.Checked = True Then

                '個数分の部品番号と名称をbarCode構造体にセット
                Dim tmpId As String = ""
                Dim tmpQty As String = ""
                Dim tmpName As String = ""
                Dim cnt As Integer

                'データセット（新規アイテムのみ）
                If Session("ds_copy_kosu") IsNot Nothing Then

                    If dsCopy2_kosu.Tables(0).Rows.Count <> 0 Then

                        cnt = 0
                        For i = 0 To dsCopy2_kosu.Tables(0).Rows.Count - 1
                            Dim dr = dsCopy2_kosu.Tables(0).Rows(i)
                            tmpId = dr("parts_no")
                            tmpName = dr("parts_name")

                            If dr("qty") IsNot Nothing Then
                                tmpQty = dr("qty")
                            Else
                                tmpQty = 0
                            End If

                            For j = 0 To tmpQty - 1
                                ReDim Preserve barCode(cnt)
                                barCode(cnt).parts_no = tmpId
                                barCode(cnt).parts_name = tmpName
                                cnt = cnt + 1
                            Next j
                        Next i

                    End If

                End If

                '構造体（既存アイテムと、入荷作業時の新規アイテム２回目以上のデータ）
                If KosuUpdate IsNot Nothing Then

                    For i = 0 To UBound(KosuUpdate)

                        If KosuUpdate(i).parts_no <> "" Then

                            tmpId = KosuUpdate(i).parts_no
                            tmpName = KosuUpdate(i).parts_name

                            For j = 0 To KosuUpdate(i).qty - 1
                                ReDim Preserve barCode(cnt)
                                barCode(cnt).parts_no = tmpId
                                barCode(cnt).parts_name = tmpName
                                cnt = cnt + 1
                            Next j

                        End If

                    Next i

                End If

                'バーコードのPDF出力
                Call barCodePdf_Set(Nothing, pdfFileName, errFlg)

            End If

            'シリアルあり
            If chkUseSerial.Checked = True Then

                If Session("ds_copy_serial") IsNot Nothing Then

                    'バーコードのPDF出力
                    Call barCodePdf_Set(dsCopy_tantai, pdfFileName, errFlg)

                End If

            End If

            'セッション設定
            Session("all_Kinditem") = allKindItem
            Session("all_Qty") = allQty
            Session("err_Flg") = errFlg
            'Session("pdf_FileName") = pdfFileName

            Call reSetSession()

            '結果報告画面へ
            Response.Redirect("Msg_inv_Arrival.aspx")

        Else
            ListHistory.Items.Clear()
            Session("error_Item") = Nothing
            Call reSetSession()
            Call showMsg("There was no arrival target.", "")
        End If

    End Sub
    'バーコードのPDF出力処理
    Protected Sub barCodePdf_Set(ByRef dsCopy_tantai As DataSet, ByVal pdfFileName As String, ByRef err As Integer)

        '***PDF出力処理***
        Dim clsSet As New Class_inv
        Dim doc As Document
        Dim fileStream As FileStream
        Dim pdfWriter As PdfWriter

        Try
            'フォントの設定
            Dim fnt As New Font(BaseFont.CreateFont("c:\windows\fonts\msgothic.ttc,1", BaseFont.IDENTITY_H, True), 12) '部品番号
            Dim fnt2 As New Font(BaseFont.CreateFont("c:\windows\fonts\CODE39.ttf", BaseFont.IDENTITY_H, True), 12) 'バーコード
            Dim fnt3 As New Font(BaseFont.CreateFont("c:\windows\fonts\msgothic.ttc,1", BaseFont.IDENTITY_H, True), 7) '部品名称

            'FileStreamを生成 
            fileStream = New FileStream(clsSet.savePDFPass & pdfFileName, FileMode.Create)

            'Documentを生成 
            doc = New Document(PageSize.A4)

            'PdfWriter生成 
            pdfWriter = PdfWriter.GetInstance(doc, fileStream)

            'Documentのオープン 
            doc.Open()

            'PDF表示行列設定（★名称が逆～）
            Const pdfRow As Integer = 3
            Const pdfCol As Integer = 16

            Dim pcb As PdfContentByte = pdfWriter.DirectContent

            'テーブル作成
            Dim table As PdfPTable
            table = New PdfPTable(pdfRow)
            table.TotalWidth = 700

            '列の設定（３列）
            Dim widths As Single()
            widths = New Single() {1.0F, 1.0F, 1.0F}
            table.SetWidths(widths)

            'セル作成
            Dim cell As PdfPCell

            'テーブルの位置情報設定(位置を指定したいときに使用する。)
            'Dim column As ColumnText
            'column = New ColumnText(pcb)
            'Dim posX As Integer
            'Dim posY As Integer
            'Dim width = table.TotalWidth
            'Dim height = table.TotalHeight

            'Dim leftX As Integer
            'Dim topY As Integer
            'Dim bottomY As Integer
            'Dim rightX As Integer

            '(leftX, bottomY, rightX, topY)
            '左下を起点に、順にX始点、Y始点、X終点、Y終点

            Dim tmp(pdfRow - 1) As String  'シリアルNO
            Dim tmp3(pdfRow - 1) As String 'シリアル名称
            Dim tmp2 As String = "" 'バーコード
            Dim dr As DataRow
            Dim rowCount As Integer　'該当バーコード件数以内、項目数の最大倍数（※例119件であれば、117件（３×３９））
            Dim rowCount2 As Integer '該当バーコード件数
            Dim i, j As Integer

            'シリアルあり
            If chkUseSerial.Checked Then
                If dsCopy_tantai.Tables(0).Rows.Count = 0 Then
                    Exit Sub
                Else
                    rowCount = (dsCopy_tantai.Tables(0).Rows.Count \ pdfRow) * pdfRow
                    rowCount2 = dsCopy_tantai.Tables(0).Rows.Count
                End If
            End If

            'シリアルなし
            If chkUnUseSerial.Checked Then
                If barCode.Length = 0 Then
                    Exit Sub
                Else
                    rowCount = (barCode.Length \ pdfRow) * pdfRow
                    rowCount2 = barCode.Length
                End If
            End If

            Dim cnt As Integer

            '該当件数以内、項目数の最大倍数まで処理
            For i = 0 To rowCount - 1

                'シリアルあり
                If chkUseSerial.Checked Then
                    dr = dsCopy_tantai.Tables(0).Rows(i)
                    tmp(cnt) = dr("parts_serial")
                    tmp3(cnt) = dr("parts_name")
                    tmp2 = "*" & dr("parts_serial") & "*"
                End If

                'シリアルなし
                If chkUnUseSerial.Checked Then
                    tmp(cnt) = barCode(i).parts_no
                    tmp3(cnt) = barCode(i).parts_name
                    tmp2 = "*" & barCode(i).parts_no & "*"
                End If

                'バーコード設定
                cell = New PdfPCell(New Paragraph(tmp2, fnt2))
                cell.FixedHeight = 16.0F
                cell.BorderWidthBottom = iTextSharp.text.Rectangle.NO_BORDER
                table.AddCell(cell)
                cnt = cnt + 1

                If cnt = pdfRow Then

                    '列数分、部品番号を設定
                    For j = 0 To UBound(tmp)
                        cell = New PdfPCell(New Paragraph(tmp(j), fnt))
                        cell.FixedHeight = 16.0F
                        cell.BorderWidthTop = iTextSharp.text.Rectangle.NO_BORDER
                        cell.BorderWidthBottom = iTextSharp.text.Rectangle.NO_BORDER
                        table.AddCell(cell)
                    Next j

                    '列数分、部品名称を設定
                    If j - 1 = UBound(tmp) Then
                        For j = 0 To UBound(tmp)
                            cell = New PdfPCell(New Paragraph(tmp3(j), fnt3))
                            cell.FixedHeight = 16.0F
                            cell.BorderWidthTop = iTextSharp.text.Rectangle.NO_BORDER
                            table.AddCell(cell)
                        Next j
                    End If
                    cnt = 0

                End If

                '1ページ目のデータが埋まったところで改行処理
                If (i >= (pdfRow * pdfCol - 1)) And ((i + 1) Mod (pdfRow * pdfCol) = 0) Then

                    '改ページ処理 
                    'テーブルの位置範囲を指定
                    'table.HorizontalAlignment = Element.ALIGN_LEFT
                    'column = New ColumnText(pcb)
                    'posX = 20
                    'posY = 830
                    'width = table.TotalWidth
                    'height = table.TotalHeight

                    'leftX = posX
                    'topY = posY
                    'bottomY = topY - height
                    'rightX = leftX + width

                    'column.SetSimpleColumn(leftX, bottomY, rightX, topY)
                    'column.AddElement(table)
                    'column.Go()

                    doc.Add(table)
                    doc.NewPage()

                    'PDFテーブル再設定
                    table = New PdfPTable(pdfRow)
                    table.TotalWidth = 700
                    widths = New Single() {1.0F, 1.0F, 1.0F}
                    table.SetWidths(widths)

                End If

            Next i

            '端数行分処理(最後の行が全て埋まらない場合、ブランクを設定)
            If rowCount2 Mod pdfRow <> 0 Then

                For i = 0 To pdfRow - 1

                    'バーコード設定
                    If i < (rowCount2 Mod pdfRow) Then

                        'シリアルあり
                        If chkUseSerial.Checked Then
                            dr = dsCopy_tantai.Tables(0).Rows(rowCount2 - (rowCount2 Mod pdfRow) + i)
                            tmp(i) = dr("parts_serial")
                            tmp3(i) = dr("parts_name")
                            tmp2 = "*" & dr("parts_serial") & "*"
                        End If

                        'シリアルなし
                        If chkUnUseSerial.Checked Then
                            tmp(i) = barCode(rowCount2 - (rowCount2 Mod pdfRow) + i).parts_no
                            tmp3(i) = barCode(rowCount2 - (rowCount2 Mod pdfRow) + i).parts_name
                            tmp2 = "*" & barCode(rowCount2 - (rowCount2 Mod pdfRow) + i).parts_no & "*"
                        End If

                        cell = New PdfPCell(New Paragraph(tmp2, fnt2))
                        cell.FixedHeight = 16.0F
                        cell.BorderWidthBottom = iTextSharp.text.Rectangle.NO_BORDER
                        table.AddCell(cell)

                    Else

                        '枠線のみ
                        cell = New PdfPCell(New Paragraph("", fnt))
                        cell.FixedHeight = 16.0F
                        cell.BorderWidthBottom = iTextSharp.text.Rectangle.NO_BORDER
                        table.AddCell(cell)
                    End If

                Next i

                For i = 0 To pdfRow - 1

                    '部品番号設定
                    If i < (rowCount2 Mod pdfRow) Then
                        cell = New PdfPCell(New Paragraph(tmp(i), fnt))
                        cell.FixedHeight = 16.0F
                        cell.BorderWidthTop = iTextSharp.text.Rectangle.NO_BORDER
                        cell.BorderWidthBottom = iTextSharp.text.Rectangle.NO_BORDER
                        table.AddCell(cell)
                    Else

                        '枠線のみ
                        cell = New PdfPCell(New Paragraph("", fnt))
                        cell.FixedHeight = 16.0F
                        cell.BorderWidthTop = iTextSharp.text.Rectangle.NO_BORDER
                        cell.BorderWidthBottom = iTextSharp.text.Rectangle.NO_BORDER
                        table.AddCell(cell)
                    End If

                Next i

                For i = 0 To pdfRow - 1

                    '部品名称設定
                    If i < (rowCount2 Mod pdfRow) Then
                        cell = New PdfPCell(New Paragraph(tmp3(i), fnt3))
                        cell.FixedHeight = 16.0F
                        cell.BorderWidthTop = iTextSharp.text.Rectangle.NO_BORDER
                        table.AddCell(cell)
                    Else

                        '枠線のみ
                        cell = New PdfPCell(New Paragraph("", fnt))
                        cell.FixedHeight = 16.0F
                        cell.BorderWidthTop = iTextSharp.text.Rectangle.NO_BORDER
                        table.AddCell(cell)
                    End If

                Next i

            End If

            doc.Add(table)

            'テーブルの位置範囲を指定
            'table.HorizontalAlignment = Element.ALIGN_LEFT
            'column = New ColumnText(pcb)
            'posX = 20
            'posY = 830
            'width = table.TotalWidth
            'height = table.TotalHeight

            'leftX = posX
            'topY = posY
            'bottomY = topY - height
            'rightX = leftX + width

            'column.SetSimpleColumn(leftX, bottomY, rightX, topY)
            'column.AddElement(table)
            'column.Go()

            'クローズ 
            doc.Close()

        Catch ex As Exception
            err = 1
        End Try

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
            Dim arrivalNo As String = Session("arrival_No")
            Dim deliveryNo As String = Session("delivery_No")
            Dim invoiceNo As String = Session("invoice_No")
            'Dim poNo As String = Session("po_no")
            Dim dtNow As DateTime = Session("dt_now")
            Dim pushCsvCount As Integer = Session("push_CsvCount")
            Dim serialCnt As Integer = Session("serial_Cnt")
            Dim serialCntArrival As Integer = Session("serial_CntArrival")
            Dim serialMax As Integer = Session("serial_Max")

            Dim dsCopy2_serial As New DataSet
            If Session("ds_copy_serial") IsNot Nothing Then
                dsCopy2_serial = Session("ds_copy_serial")
            End If

            Dim dsCopy2_serial_arrival As New DataSet
            If Session("ds_copy_serial_arrival") IsNot Nothing Then
                dsCopy2_serial_arrival = Session("ds_copy_serial_arrival")
            End If

            Dim errorItem() As Class_inv.ERROR_Item
            If Session("error_Item") IsNot Nothing Then
                errorItem = Session("error_Item")
            End If

            '***入力チェック***
            If arrivalNo = "" Or arrivalNo Is Nothing Then
                Msg = "Please select an arrival number."
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
            Dim i, j, m As Integer
            Dim colsHead(4) As String
            Dim clsSet As New Class_inv
            Dim delRow As Integer

            Try
                'ローカルファイルのフルパス取得 
                'Full_Pass = FileUploadInv.PostedFile.FileName

                'ファイル名取得
                FileName = FileUploadInv.FileName & "_" & pushCsvCount

                If FileName <> "" Then

                    'ファイル名取得
                    'FileName = System.IO.Path.GetFileName(Full_Pass) & "_" & pushCsvCount

                    'サーバの指定パスに保存
                    FileUploadInv.SaveAs(clsSet.saveCsvPass & FileName)

                    'History欄へ設定
                    ListHistory.Items.Add("csv name:" & FileName)

                    FileOpen(fileNo, clsSet.saveCsvPass & FileName, OpenMode.Input)
                    'FileOpen(fileNo, Full_Pass, OpenMode.Input)

                    Do Until EOF(fileNo) 'ファイルの最後までループ 
                        ReDim Preserve CSV(csvRowCnt)
                        Input(fileNo, CSV(csvRowCnt).parts_no)
                        Input(fileNo, CSV(csvRowCnt).qty)
                        Input(fileNo, CSV(csvRowCnt).loc_1)
                        Input(fileNo, CSV(csvRowCnt).loc_2)
                        Input(fileNo, CSV(csvRowCnt).loc_3)
                        If csvRowCnt = 0 And colsHead(0) = "" Then
                            colsHead(0) = CSV(csvRowCnt).parts_no
                            colsHead(1) = CSV(csvRowCnt).qty
                            colsHead(2) = CSV(csvRowCnt).loc_1
                            colsHead(3) = CSV(csvRowCnt).loc_2
                            colsHead(4) = CSV(csvRowCnt).loc_3
                            'ヘッダ確認
                            If clsSet.chkHead(colsHead, "Arrival") = False Then
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

            '***アイテムの有効チェック***
            If errorItem Is Nothing Then
                m = 0
            Else
                m = errorItem.Length
            End If

            For i = 0 To UBound(CSV)

                Dim strSQL As String = "SELECT * FROM dbo.M_PARTS WHERE parts_no = '" & CSV(i).parts_no & "' AND parts_flg = 1 AND DELFG = 0 AND ship_code = '" & shipCode & "';"

                Dim DT_M_PARTS As DataTable = DBCommon.ExecuteGetDT(strSQL, errFlg)

                If errFlg = 1 Then
                    Call reSetSession()
                    Session("all_Kinditem") = 0
                    Session("all_Qty") = 0
                    Session("err_Flg") = Nothing
                    Session("error_Item") = Nothing
                    Call reSetAll()
                    Call showMsg("Failed to acquire information from the DB.", "")
                    Exit Sub
                End If

                If DT_M_PARTS IsNot Nothing Then
                    CSV(i).parts_no = CSV(i).parts_no
                    CSV(i).qty = CSV(i).qty
                    CSV(i).loc_1 = CSV(i).loc_1
                    CSV(i).loc_2 = CSV(i).loc_2
                    CSV(i).loc_3 = CSV(i).loc_3

                    If DT_M_PARTS.Rows(0)("parts_name") IsNot DBNull.Value Then
                        CSV(i).parts_name = DT_M_PARTS.Rows(0)("parts_name")
                    End If

                    If DT_M_PARTS.Rows(0)("maker") IsNot DBNull.Value Then
                        CSV(i).maker = DT_M_PARTS.Rows(0)("maker")
                    End If

                Else
                    'masterに存在しないアイテムのCSV構造体は、NULLを設定しておく
                    ReDim Preserve errorItem(m)
                    errorItem(m).item = CSV(i).parts_no
                    errorItem(m).fileName = FileName
                    m = m + 1
                    CSV(i).parts_no = ""
                    CSV(i).qty = ""
                    CSV(i).loc_1 = ""
                    CSV(i).loc_2 = ""
                    CSV(i).loc_3 = ""
                    CSV(i).parts_name = ""
                    CSV(i).maker = ""
                End If

            Next i

            '最終シリアル番号の確認と設定
            '入荷CSVインポート作業１回目のときのみ
            Dim partsSerialMax As String = ""
            If pushCsvCount = 1 Then
                Dim strSQL2 As String = "SELECT MAX(RIGHT(parts_serial,5)) AS parts_serial_max FROM dbo.T_inParts;"
                Dim DT_T_inParts As DataTable = DBCommon.ExecuteGetDT(strSQL2, errFlg)

                If errFlg = 1 Then
                    Call reSetSession()
                    Session("all_Kinditem") = 0
                    Session("all_Qty") = 0
                    Session("err_Flg") = Nothing
                    Session("error_Item") = Nothing
                    Call reSetAll()
                    Call showMsg("Failed to acquire information from the DB.", "")
                    Exit Sub
                End If

                If DT_T_inParts IsNot Nothing Then
                    If DT_T_inParts.Rows(0)("parts_serial_max") IsNot DBNull.Value Then
                        partsSerialMax = DT_T_inParts.Rows(0)("parts_serial_max")
                        serialMax = Convert.ToInt32(partsSerialMax)
                        serialMax = serialMax + 1
                    End If
                End If
            End If

            '***データセットにitem(部品)内容を設定***
            'コネクションを取得
            Dim con As SqlConnection = New SqlConnection(ConfigurationManager.ConnectionStrings("cnstr").ConnectionString)
            con.Open()

            Try
                '■T_inParts空テーブル取得
                Dim select_sql1 As String = ""
                select_sql1 &= "SELECT * FROM dbo.T_inParts WHERE parts_serial IS NULL;"
                Dim sqlSelect1 As New SqlCommand(select_sql1, con)
                Dim Adapter1 As New SqlDataAdapter(sqlSelect1)
                Dim ds1 As New DataSet

                Adapter1.Fill(ds1)

                For i = 0 To CSV.Length - 1
                    If CSV(i).parts_no <> "" Then
                        If CSV(i).qty <> "" Then
                            For j = 0 To CSV(i).qty - 1
                                '新規DR取得
                                Dim dr1 As DataRow = ds1.Tables(0).NewRow
                                '登録内容設定
                                dr1("CRTDT") = Now
                                dr1("CRTCD") = userid
                                dr1("UPDDT") = Now
                                dr1("UPDCD") = userid
                                dr1("UPDPG") = "systemName"
                                dr1("DELFG") = 0
                                dr1("maker") = CSV(i).maker
                                dr1("parts_no") = CSV(i).parts_no
                                dr1("deli_date") = dtNow
                                '個数分シリアルNO付与
                                dr1("parts_serial") = CSV(i).parts_no & "-" & (serialCnt + serialMax).ToString("00000")
                                dr1("deli_no") = deliveryNo
                                dr1("invo_no") = invoiceNo
                                dr1("loc_1") = CSV(i).loc_1
                                dr1("loc_2") = CSV(i).loc_2
                                dr1("loc_3") = CSV(i).loc_3
                                dr1("parts_name") = CSV(i).parts_name
                                'dr1("po_no") = poNo
                                dr1("arrival_no") = arrivalNo
                                dr1("gspn") = True
                                dr1("ship_code") = shipCode
                                dr1("file_name") = FileName
                                dr1("parts_status") = "1"
                                '新規DRをDataTableに追加
                                ds1.Tables(0).Rows.Add(dr1)
                                serialCnt = serialCnt + 1
                            Next j
                        End If
                    End If
                Next i

                '■T_inParts_arrival空テーブル取得
                Dim select_sql2 As String = ""
                select_sql2 &= "SELECT * FROM dbo.T_inParts_arrival WHERE unq IS NULL;"
                Dim sqlSelect2 As New SqlCommand(select_sql2, con)
                Dim Adapter2 As New SqlDataAdapter(sqlSelect2)
                Dim ds2 As New DataSet

                Adapter2.Fill(ds2)

                For i = 0 To CSV.Length - 1
                    If CSV(i).parts_no <> "" Then
                        If CSV(i).qty <> "" Then
                            For j = 0 To CSV(i).qty - 1
                                '新規DR取得
                                Dim dr2 As DataRow = ds2.Tables(0).NewRow
                                '登録内容設定
                                dr2("CRTDT") = Now
                                dr2("CRTCD") = userid
                                dr2("UPDDT") = Now
                                dr2("UPDCD") = userid
                                dr2("UPDPG") = "systemName"
                                dr2("DELFG") = 0
                                dr2("maker") = CSV(i).maker
                                dr2("arrival_no") = arrivalNo
                                dr2("parts_no") = CSV(i).parts_no
                                dr2("deli_date") = dtNow
                                '個数分シリアルNO付与
                                dr2("parts_serial") = CSV(i).parts_no & "-" & (serialCntArrival + serialMax).ToString("00000")
                                dr2("deli_no") = deliveryNo
                                dr2("invo_no") = invoiceNo
                                dr2("ship_code") = shipCode
                                dr2("file_name") = FileName
                                '新規DRをDataTableに追加
                                ds2.Tables(0).Rows.Add(dr2)
                                serialCntArrival = serialCntArrival + 1
                            Next j
                        End If
                    End If
                Next i

                '入荷CSVインポート作業１回目
                If pushCsvCount = 1 Then

                    '■T_inPart
                    'データセットのコピー(値なし)
                    Dim dsCopy_serial As New DataSet
                    dsCopy_serial = ds1.Clone
                    Dim customerTable_serial As DataTable = dsCopy_serial.Tables(0)

                    '新しいデータセットに1回目の入荷情報をコピー
                    For i = 0 To ds1.Tables(0).Rows.Count - 1
                        Dim drCopy_serial As DataRow = ds1.Tables(0).Rows(i)
                        customerTable_serial.ImportRow(drCopy_serial)
                    Next i
                    Session("ds_copy_serial") = dsCopy_serial

                    '■T_inParts_arrival
                    'データセットのコピー(値なし)
                    Dim dsCopy_serial_arrival As New DataSet
                    dsCopy_serial_arrival = ds2.Clone
                    Dim customerTable_serial_arrival As DataTable = dsCopy_serial_arrival.Tables(0)

                    '新しいデータセットに入荷情報をコピー
                    For i = 0 To ds2.Tables(0).Rows.Count - 1
                        Dim drCopy_serial_arrival As DataRow = ds2.Tables(0).Rows(i)
                        customerTable_serial_arrival.ImportRow(drCopy_serial_arrival)
                    Next i
                    Session("ds_copy_serial_arrival") = dsCopy_serial_arrival

                Else

                    '■T_inPart
                    Dim customerTable_serial As DataTable = dsCopy2_serial.Tables(0)
                    'セッション管理されているデータセットに入荷情報を追加コピー
                    For i = 0 To ds1.Tables(0).Rows.Count - 1
                        Dim drCopy_serial As DataRow = ds1.Tables(0).Rows(i)
                        customerTable_serial.ImportRow(drCopy_serial)
                    Next i
                    Session("ds_copy_serial") = dsCopy2_serial

                    '■T_inParts_arrival
                    Dim customerTable_serial_arrival As DataTable = dsCopy2_serial_arrival.Tables(0)
                    'セッション管理されているデータセットに入荷情報を追加コピー
                    For i = 0 To ds2.Tables(0).Rows.Count - 1
                        Dim drCopy_serial_arrival As DataRow = ds2.Tables(0).Rows(i)
                        customerTable_serial_arrival.ImportRow(drCopy_serial_arrival)
                    Next i
                    Session("ds_copy_serial_arrival") = dsCopy2_serial_arrival

                End If

                Call showMsg("success!!", "LastMsg")

                '***セッション情報を設定***
                Session("push_CsvCount") = pushCsvCount
                Session("file_Name") = FileName
                Session("error_Item") = errorItem
                Session("serial_Cnt") = serialCnt
                Session("serial_CntArrival") = serialCntArrival
                Session("serial_Max") = serialMax

            Catch ex As Exception
                Call reSetSession()
                Session("all_Kinditem") = 0
                Session("all_Qty") = 0
                Session("err_Flg") = Nothing
                Session("error_Item") = Nothing
                Call reSetAll()
                Call showMsg("Data Import Error Please retry.", "")
                Exit Sub
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
    '再度スタートボタン押下できる状態
    Protected Sub reSetAll()

        BtnStart.Enabled = True
        chkUseSerial.Enabled = True
        chkUnUseSerial.Enabled = True

        btnClose.Enabled = False
        lblCurrentArrival.Visible = False
        lblCurrentDelivery.Visible = False
        TextDeliveryNo.Text = ""
        'TextPONo.Text = ""
        TextInvoiceNo.Text = ""
        lblArrivalNnumber.Text = ""
        lblDeliveryNumber.Text = ""
        ListHistory.Items.Clear()

    End Sub
    'セッション開放
    Protected Sub reSetSession()

        Session("push_Count") = 0
        Session("push_CsvCount") = 0
        Session("serial_Cnt") = 0
        Session("serial_CntArrival") = 0
        Session("serial_Max") = 0
        Session("ds_copy") = Nothing
        Session("ds_copy_kosu") = Nothing
        Session("ds_copy_serial") = Nothing
        Session("ds_copy_serial_arrival") = Nothing
        Session("qtyCount_Kosu") = 0
        Session("Kosu_Update") = Nothing
        Session("item_Chk") = Nothing
        Session("file_Name") = Nothing
        Session("item_No") = Nothing

    End Sub
    'jqueryのダイアログでキャンセルボタン押下時にクリック処理される。
    '実際は使用しないボタン(ASP.net)なので非表示。
    'ダイアログでキャンセルボタン押下⇒ASP.netのキャンセルボタンクリック⇒サーバからの応答ありでロードへ⇒ロードでキャンセル処理
    Protected Sub BtnCancel_Click(sender As Object, e As EventArgs) Handles BtnCancel.Click

    End Sub

End Class


