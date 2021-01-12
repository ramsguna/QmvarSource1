Public Class inv_Search
    Inherits System.Web.UI.Page

    Private lastDate As Date
    Private firstDate As Date

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        If IsPostBack = False Then

            '***セッションなしログインユーザの対応***
            Dim userid As String = Session("user_id")
            If userid = "" Then
                Response.Redirect("Login.aspx")
            End If

            '***表示コントロール***
            CheckDefault.Checked = True

        End If

    End Sub

    Protected Sub BtnStart_Click(sender As Object, e As ImageClickEventArgs) Handles BtnStart.Click

        '***セッション情報取得***
        Dim userid As String = Session("user_id")
        Dim shipCode As String = Session("ship_code")

        If userid Is Nothing Then
            Call showMsg("The session has expired. Please login again.")
            Exit Sub
        End If

        Dim errFlg As Integer
        Dim startDate As String
        Dim endDate As String
        Dim dt As DateTime

        '***入力値取得 ***
        Dim arrivalNo As String = Trim(TextArrivalNumber.Text)
        Dim partsNo As String = Trim(TextPartsNnumber.Text)
        Dim delivaryNo As String = Trim(TextDeliveryNumber.Text)
        'Dim poNo As String = Trim(TextPONumber.Text)
        Dim invoiceNo As String = Trim(TextInvoiceNumber.Text)
        Dim serialNo As String = Trim(TextSerialNumber.Text)
        Dim returnNo As String = Trim(TextReturnNo.Text)
        Dim returnDeliveryNo As String = Trim(TextReturnDeliveryNo.Text)
        Dim partsStatus As String = Trim(ddlPartsStatus.Text)
        Dim test1 As String = Trim(TextScriptStartDate.Text)
        Dim test2 As String = Trim(TextScriptEndDate.Text)

        Select Case partsStatus
            Case "unuse Stock"
                partsStatus = "1"
            Case "Warehouse Stock"
                partsStatus = "2"
            Case "Engineer Stock"
                partsStatus = "3"
            Case "Return"
                partsStatus = "4"
            Case Else
                partsStatus = ""
        End Select

        '***検索項目チェック***
        Dim searchChk As Integer = 0
        If arrivalNo = "" And delivaryNo = "" And delivaryNo = "" And invoiceNo = "" And returnNo = "" And returnDeliveryNo = "" Then
            searchChk = 1
        End If

        '***入力チェック***
        If chkUseSerial.Checked = False And chkUnUseSerial.Checked = False Then
            Call showMsg("Please check it with serial or without serial.")
            reSetSession()
            Exit Sub
        End If

        If partsStatus <> "" And serialNo = "" And partsNo = "" Then
            Call showMsg("Please specify serial number or part number.")
            reSetSession()
            Exit Sub
        End If

        '日付チェック
        If TextScriptStartDate.Text <> "" Then
            If DateTime.TryParse(TextScriptStartDate.Text, dt) Then
                firstDate = DateTime.Parse(Trim(TextScriptStartDate.Text))
                startDate = firstDate.ToShortDateString
            Else
                Call showMsg("There is an error in specifying the start date.")
                reSetSession()
                Exit Sub
            End If
        Else
            startDate = ""
        End If

        If TextScriptEndDate.Text <> "" Then
            If DateTime.TryParse(TextScriptEndDate.Text, dt) Then
                lastDate = DateTime.Parse(Trim(TextScriptEndDate.Text))
                endDate = lastDate.ToShortDateString
            Else
                Call showMsg("There is an error in the end date specification.")
                reSetSession()
                Exit Sub
            End If
        Else
            endDate = ""
        End If

        If startDate <> "" Or endDate <> "" Then

            If searchChk = 1 And partsStatus = "" And serialNo = "" And partsNo = "" Then
                reSetSession()
                Call showMsg("Please specify items other than date.")
                Exit Sub
            End If

        End If

        '***SQL設定***
        Dim sqlKey1 As String = ""
        Dim strSQL As String = ""
        Dim strSQL2 As String = ""

        Dim strStatus As String = "CASE WHEN A.parts_status = 1 THEN 'unuse Stock Qty' WHEN A.parts_status = 2 THEN 'Warehouse Stock Qty' WHEN A.parts_status = 3 THEN 'Engineer Stock Qty' WHEN A.parts_status = 4 THEN 'Return Qty' END AS parts_status"

        '■シリアル番号で検索 (単体 or ＋部品番号 or +ステータス)
        If serialNo <> "" And searchChk = 1 And startDate = "" And endDate = "" Then

            strSQL = ""

            strSQL &= "SELECT A.parts_no, C.parts_name, A.parts_serial, A.loc_1, A.loc_2, A.loc_3, " & strStatus & ", D.ship_name, IsNull(B.return_no,'N/A') AS return_no, A.deli_date "
            strSQL &= "FROM dbo.T_inParts A "
            strSQL &= "LEFT JOIN dbo.T_ReturnParts B ON A.parts_serial = B.parts_no "
            strSQL &= "LEFT JOIN dbo.M_PARTS C ON A.parts_no = C.parts_no "
            strSQL &= "LEFT JOIN dbo.M_ship_base D ON A.ship_code = D.ship_code "
            strSQL &= "WHERE A.DELFG = 0 "
            strSQL &= "AND A.parts_serial LIKE '%" & serialNo & "%' "

            If partsNo <> "" Then
                strSQL &= "AND A.parts_no LIKE '%" & partsNo & "%' "
            End If

            If partsStatus <> "" Then
                strSQL &= "AND A.parts_status = '" & partsStatus & "' "
            End If

        End If

        '■部品番号の単体検索
        If partsNo <> "" And searchChk = 1 And serialNo = "" And startDate = "" And endDate = "" Then

            strSQL = ""

            'シリアルなし
            If chkUnUseSerial.Checked = True Then
                strSQL &= "SELECT A.parts_no, A.parts_name, A.loc_1, A.loc_2, A.loc_3, A.unit_price, A.g_unit_price, A.Assing_type, IsNull(B.parts_unuse, 0) AS parts_unuse, IsNull(B.parts_use, 0) AS parts_use, IsNull(B.parts_otw, 0) AS parts_otw, IsNull((B.parts_unuse + B.parts_use + B.parts_otw), 0) AS StockTotal "
                strSQL &= "FROM dbo.M_PARTS A "
                strSQL &= "LEFT JOIN dbo.T_inParts_2 B ON A.parts_no = B.parts_no "
                strSQL &= "WHERE A.DELFG = 0 "
                strSQL &= "AND A.parts_flg = 0 "
                strSQL &= "AND A.ship_code = '" & shipCode & "' "
                strSQL &= "AND A.parts_no LIKE '%" & partsNo & "%';"
            End If

            'シリアルあり
            If chkUseSerial.Checked = True Then
                strSQL &= "SELECT A.parts_no, A.parts_name, A.loc_1, A.loc_2, A.loc_3, A.unit_price, A.g_unit_price, A.Assing_type "
                strSQL &= "FROM dbo.M_PARTS A "
                strSQL &= "WHERE A.DELFG = 0 "
                strSQL &= "AND A.parts_flg = 1 "
                strSQL &= "AND A.ship_code = '" & shipCode & "' "
                strSQL &= "AND A.parts_no LIKE '%" & partsNo & "%';"
            End If

        End If

        '■共通の一覧表示(全ての項目の複合検索、部品番号（シリアル含む）以外の項目の単体検索）
        '入荷番号で検索
        Dim sqlKey As String = ""
        Dim sqlKey2 As String = ""

        If arrivalNo <> "" Then
            sqlKey &= "AND A.arrival_no LIKE '%" & arrivalNo & "%' "
        End If

        If invoiceNo <> "" Then
            sqlKey &= "AND A.invo_no LIKE '%" & invoiceNo & "%' "
        End If

        'If poNo <> "" Then
        'sqlKey &= "AND A.po_no LIKE '%" & poNo & "%' "
        'End If

        If delivaryNo <> "" Then
            sqlKey &= "AND A.deli_no LIKE '%" & delivaryNo & "%' "
        End If

        If returnNo <> "" Then
            sqlKey &= "AND B.ss_return LIKE '%" & returnNo & "%' "
        End If

        If returnDeliveryNo <> "" Then
            sqlKey &= "AND B.deli_no LIKE '%" & returnDeliveryNo & "%' "
        End If

        If endDate <> "" Then
            If startDate <> "" Then
                sqlKey &= "AND (CONVERT(VARCHAR, A.deli_date, 111)) <= CONVERT(DATETIME, '" & endDate & "') "
                sqlKey &= "AND (CONVERT(VARCHAR, A.deli_date, 111)) >= CONVERT(DATETIME, '" & startDate & "') "
            Else
                sqlKey &= "AND (CONVERT(VARCHAR, A.deli_date, 111)) <= CONVERT(DATETIME, '" & endDate & "') "
            End If
        Else
            If startDate <> "" Then
                sqlKey &= "AND (CONVERT(VARCHAR, A.deli_date, 111)) >= CONVERT(DATETIME, '" & startDate & "') "
            End If
        End If

        If serialNo <> "" Then
            sqlKey2 &= "AND A.parts_serial LIKE '%" & serialNo & "%' "
        End If

        If partsNo <> "" Then
            sqlKey2 &= "AND A.parts_no LIKE '%" & partsNo & "%' "
        End If

        If partsStatus <> "" Then
            sqlKey2 &= "AND A.parts_status = '" & partsStatus & "' "
        End If

        If sqlKey <> "" Then

            strSQL2 = ""

            'シリアルあり
            If chkUseSerial.Checked = True Then

                strSQL2 &= "SELECT A.arrival_no, A.invo_no, CONVERT(VARCHAR, A.deli_date,111) AS deli_date, A.deli_no, A.UPDCD, " & strStatus & ", B.ss_return, B.deli_no AS rtn_delivery_no, B.UPDCD AS person_out, CONVERT(VARCHAR, B.UPDDT, 111) AS rtn_date, A.parts_no, A.parts_serial, A.ship_code "
                strSQL2 &= "FROM dbo.T_inParts A "
                strSQL2 &= "LEFT JOIN T_ReturnParts B "
                strSQL2 &= "ON A.parts_serial = B.parts_no "
                strSQL2 &= "WHERE A.DELFG = 0 AND A.ship_code = '" & shipCode & "' "
                strSQL2 &= sqlKey
                strSQL2 &= sqlKey2
            End If

            'シリアルなし
            If chkUnUseSerial.Checked = True Then

                strSQL2 &= "SELECT A.arrival_no, A.invo_no, CONVERT(VARCHAR, A.deli_date,111) AS deli_date, A.deli_no, IsNull(A.parts_unuse, 0) AS parts_unuse, IsNull(A.parts_use, 0) AS parts_use, IsNull(A.parts_otw, 0) AS parts_otw, (A.parts_unuse + A.parts_use + A.parts_otw) AS StockTotal, A.UPDCD, B.ss_return, B.deli_no AS rtn_delivery_no, B.UPDCD AS person_out, CONVERT(VARCHAR, B.UPDDT, 111) AS rtn_date, A.parts_no, A.ship_code "
                strSQL2 &= "FROM dbo.T_inParts_2 A "
                strSQL2 &= "LEFT JOIN T_ReturnParts B "
                strSQL2 &= "ON A.parts_no = B.parts_no "
                strSQL2 &= "WHERE A.DELFG = 0 AND A.ship_code = '" & shipCode & "' "
                strSQL2 &= sqlKey
                strSQL2 &= sqlKey2
            End If

        End If

        If strSQL <> "" Then

            Dim DT_SERCH As DataTable = DBCommon.ExecuteGetDT(strSQL, errFlg)

            If errFlg = 1 Then
                Call showMsg("Failed to acquire search information.")
                Exit Sub
            End If

            If DT_SERCH IsNot Nothing Then
                grd_info.DataSource = DT_SERCH
                grd_info.DataBind()
                grd_info.Columns(0).Visible = False
                '次ページ反映ように取得しておく
                Session("Data_DT_SERCH") = DT_SERCH
            Else
                grd_info.DataSource = Nothing
                grd_info.DataBind()
                Call showMsg("There was no search result.")
                Exit Sub
            End If

        Else

            If strSQL = "" And strSQL2 <> "" Then

                Dim DT_SERCH As DataTable = DBCommon.ExecuteGetDT(strSQL2, errFlg)

                If DT_SERCH IsNot Nothing Then
                    grd_info.DataSource = DT_SERCH
                    Dim test As String = grd_info.Columns(0).HeaderText
                    grd_info.DataBind()
                    grd_info.Columns(0).Visible = True
                    '次ページ反映ように取得しておく
                    Session("Data_DT_SERCH") = DT_SERCH
                Else
                    grd_info.DataSource = Nothing
                    grd_info.DataBind()
                    Call showMsg("There was no search result.")
                    Exit Sub
                End If

            Else
                Call showMsg("Please specify search conditions.")
                Exit Sub
            End If

        End If

        Session("first_Date") = Nothing
        Session("last_Date") = Nothing

    End Sub

    Private Sub chkUnUseSerial_CheckedChanged(sender As Object, e As EventArgs) Handles chkUnUseSerial.CheckedChanged

        If chkUnUseSerial.Checked = True Then
            chkUseSerial.Checked = False
            TextSerialNumber.Text = ""
            TextSerialNumber.Enabled = False
            TextPartsNnumber.Enabled = True
            ddlPartsStatus.Enabled = False
            ddlPartsStatus.Text = "selectStatus"
        End If

    End Sub

    Private Sub chkUseSerial_CheckedChanged(sender As Object, e As EventArgs) Handles chkUseSerial.CheckedChanged

        If chkUseSerial.Checked = True Then
            chkUnUseSerial.Checked = False
            TextSerialNumber.Enabled = True
            ddlPartsStatus.Enabled = True
        End If

    End Sub

    Protected Sub showMsg(ByVal Msg As String)

        'Dim formid As String = Me.Form.ClientID

        '' 確認ダイアログを出力するスクリプト
        '' POST先は自分自身(inv_Search.aspx)
        'Dim sScript As String = "if(alert(""" + Msg + """)){ " +
        'formid + ".method = ""post"";" +
        '                                formid + ".action = ""inv_Search.aspx?errMsg=true"";" +
        '                                formid + ".submit();" +
        '                            "}"

        lblMsg.Text = Msg
        Dim sScript As String = "$(function () { $( ""#dialog"" ).dialog({width: 400, buttons: {""OK"": function () {$(this).dialog('close');}}});});"
        'JavaScriptの埋め込み
        ClientScript.RegisterStartupScript(Me.GetType(), "startup", sScript, True)

    End Sub

    Private Sub grd_info_PageIndexChanging(sender As Object, e As GridViewPageEventArgs) Handles grd_info.PageIndexChanging

        grd_info.PageIndex = e.NewPageIndex
        grd_info.DataSource = Session("Data_DT_SERCH")
        grd_info.DataBind()

    End Sub

    Private Sub reSetSession()

        Session("first_Date") = Nothing
        Session("last_Date") = Nothing

    End Sub

    '部品番号詳細リンクボタン押下処理
    Private Sub grd_info_RowCommand(sender As Object, e As GridViewCommandEventArgs) Handles grd_info.RowCommand

        Dim shipCode As String = Session("ship_code")

        If shipCode Is Nothing Then
            Call showMsg("The session has expired. Please login again.")
            Exit Sub
        End If

        'コマンド名が“detail”の場合にのみ処理
        If e.CommandName = "Detail" Then

            'クリック行のインデックス取得
            Dim Index As Integer = Convert.ToInt32(e.CommandArgument)

            '該当行取得
            Dim gridRow As GridViewRow = grd_info.Rows(Index)

            '部品番号取得
            Dim partsNo As String = ""

            Dim strSQL As String = ""

            'シリアルなし
            If chkUnUseSerial.Checked = True Then
                partsNo = gridRow.Cells(14).Text
                strSQL &= "SELECT A.parts_no, A.parts_name, A.loc_1, A.loc_2, A.loc_3, A.unit_price, A.g_unit_price, A.Assing_type, IsNull(B.parts_unuse, 0) AS parts_unuse, IsNull(B.parts_use, 0) AS parts_use, IsNull(B.parts_otw, 0) AS parts_otw, IsNull((B.parts_unuse + B.parts_use + B.parts_otw), 0) AS StockTotal "
                strSQL &= "FROM dbo.M_PARTS A "
                strSQL &= "LEFT JOIN dbo.T_inParts_2 B ON A.parts_no = B.parts_no "
                strSQL &= "WHERE A.DELFG = 0 "
                strSQL &= "AND A.ship_code = '" & shipCode & "' "
                strSQL &= "AND A.parts_flg = 0 "
                strSQL &= "AND A.parts_no = '" & partsNo & "' "
            End If

            'シリアルあり
            If chkUseSerial.Checked = True Then
                partsNo = gridRow.Cells(11).Text
                strSQL &= "SELECT A.parts_no, A.parts_name, A.loc_1, A.loc_2, A.loc_3, A.unit_price, A.g_unit_price, A.Assing_type "
                strSQL &= "FROM dbo.M_PARTS A "
                strSQL &= "WHERE A.DELFG = 0 "
                strSQL &= "AND A.ship_code = '" & shipCode & "' "
                strSQL &= "AND A.parts_flg = 1 "
                strSQL &= "AND A.parts_no = '" & partsNo & "' "
            End If

            If strSQL <> "" Then

                Dim errFlg As Integer

                Dim DT_SERCH As DataTable = DBCommon.ExecuteGetDT(strSQL, errflg)

                If errFlg = 1 Then
                    Call showMsg("Failed to acquire search information.")
                    Exit Sub
                End If

                If DT_SERCH IsNot Nothing Then
                    grd_info.DataSource = DT_SERCH
                    grd_info.DataBind()
                    grd_info.Columns(0).Visible = False
                    '次ページ反映ように取得しておく
                    Session("Data_DT_SERCH") = DT_SERCH
                Else
                    grd_info.DataSource = Nothing
                    grd_info.DataBind()
                    Call showMsg("There was no search result.")
                    Exit Sub
                End If

            End If

        End If

    End Sub

    Protected Sub CheckDefault_CheckedChanged(sender As Object, e As EventArgs) Handles CheckDefault.CheckedChanged

        If CheckDefault.Checked = True Then
            Check20Page.Checked = False
            Check100Page.Checked = False
            grd_info.PageSize = 10
            If Session("Data_DT_SERCH") IsNot Nothing Then
                grd_info.DataSource = Session("Data_DT_SERCH")
                grd_info.DataBind()
            End If
        End If

    End Sub

    Protected Sub Check20Page_CheckedChanged(sender As Object, e As EventArgs) Handles Check20Page.CheckedChanged

        If Check20Page.Checked = True Then
            CheckDefault.Checked = False
            Check100Page.Checked = False
            grd_info.PageSize = 30
            If Session("Data_DT_SERCH") IsNot Nothing Then
                grd_info.DataSource = Session("Data_DT_SERCH")
                grd_info.DataBind()
            End If
        End If

    End Sub

    Protected Sub Check100Page_CheckedChanged(sender As Object, e As EventArgs) Handles Check100Page.CheckedChanged

        If Check100Page.Checked = True Then
            CheckDefault.Checked = False
            Check20Page.Checked = False
            grd_info.PageSize = 50
            If Session("Data_DT_SERCH") IsNot Nothing Then
                grd_info.DataSource = Session("Data_DT_SERCH")
                grd_info.DataBind()
            End If
        End If

    End Sub
End Class