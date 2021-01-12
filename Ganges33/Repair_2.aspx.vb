Imports System.Text
Imports System.IO
Public Class Repair_2aspx
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        '***初期処理***
        If IsPostBack = False Then

            '***セッションなしログインユーザの対応***
            Dim userid As String = Session("user_id")
            If userid = "" Then
                Response.Redirect("Login.aspx")
            End If

            If Session("Data_DT_SERCH") Is Nothing Then
                btnDownLoad.Enabled = False
            Else
                grd_info.DataSource = Session("Data_DT_SERCH")
                grd_info.DataBind()
            End If

        End If

    End Sub

    Protected Sub btnSearch_Click(sender As Object, e As ImageClickEventArgs) Handles btnSearch.Click

        '***セッション情報取得***
        Dim userid As String = Session("user_id")
        Dim shipCode As String = Session("ship_code")
        Dim userLevel As String = Session("user_level")

        If userid Is Nothing Then
            Call showMsg("The session has expired. Please login again.")
            Exit Sub
        End If

        '日付チェック
        Dim dt As DateTime
        Dim PurchaseDate As String
        If TextPurchase_Date.Text <> "" Then
            If DateTime.TryParse(TextPurchase_Date.Text, dt) Then
                'PurchaseDate = DateTime.Parse(Trim(TextPurchase_Date.Text)).ToShortDateString
                PurchaseDate = Trim(TextPurchase_Date.Text)
            Else
                Call showMsg("The purchase date is invalid")
                Exit Sub
            End If
        Else
            PurchaseDate = ""
        End If

        Dim RepairRecivedDate As String
        If TextRepair_Recived_Date.Text <> "" Then
            If DateTime.TryParse(TextRepair_Recived_Date.Text, dt) Then
                'RepairRecivedDate = DateTime.Parse(Trim(TextRepair_Recived_Date.Text)).ToShortDateString
                RepairRecivedDate = Trim(TextRepair_Recived_Date.Text)
            Else
                Call showMsg("The repair received date is invalid")
                Exit Sub
            End If
        Else
            RepairRecivedDate = ""
        End If

        Dim CompleteDate As String
        If TextComplete_Date.Text <> "" Then
            If DateTime.TryParse(TextComplete_Date.Text, dt) Then
                CompleteDate = DateTime.Parse(Trim(TextComplete_Date.Text)).ToShortDateString
                'CompleteDate = Trim(TextComplete_Date.Text)
            Else
                Call showMsg("The complete date is invalid")
                Exit Sub
            End If
        Else
            CompleteDate = ""
        End If

        '***SQL文作成***
        Dim sqlkey As String = ""

        If TextPO_NO.Text <> "" Then
            sqlkey &= "A.po_no LIKE '%" & Trim(TextPO_NO.Text) & "%' AND "
        End If

        If TextASC_Claim_No.Text <> "" Then
            sqlkey &= "A.ASC_Claim_No LIKE '%" & Trim(TextASC_Claim_No.Text) & "%' AND "
        End If

        If TextSamsung_Claim_No.Text <> "" Then
            sqlkey &= "A.Samsung_Claim_No LIKE '%" & Trim(TextSamsung_Claim_No.Text) & "%' AND "
        End If

        If TextEngineer.Text <> "" Then
            sqlkey &= "A.Engineer LIKE '%" & Trim(TextEngineer.Text) & "%' AND "
        End If

        If TextSerial_No.Text <> "" Then
            sqlkey &= "A.Serial_No LIKE '%" & Trim(TextSerial_No.Text) & "%' AND "
        End If

        If TextModel.Text <> "" Then
            sqlkey &= "A.Model LIKE '%" & Trim(TextModel.Text) & "%' AND "
        End If

        If TextIMEI.Text <> "" Then
            sqlkey &= "A.IMEI_No LIKE '%" & Trim(TextIMEI.Text) & "%' AND "
        End If

        If TextConsumer_name.Text <> "" Then
            sqlkey &= "A.Consumer_Name LIKE '%" & Trim(TextConsumer_name.Text) & "%' AND "
        End If

        If TextTelephon.Text <> "" Then
            sqlkey &= "A.Consumer_Telephone LIKE '%" & Trim(TextTelephon.Text) & "%' AND "
        End If

        If TextTracking_No.Text <> "" Then
            sqlkey &= "A.Tr_No LIKE '%" & Trim(TextTracking_No.Text) & "%' AND "
        End If

        If PurchaseDate <> "" Then
            sqlkey &= "CONVERT(VARCHAR, A.Purchase_Date, 111) LIKE '%" & PurchaseDate & "%' AND "
        End If

        If RepairRecivedDate <> "" Then
            sqlkey &= "CONVERT(VARCHAR, A.Repair_Received_Date, 111) LIKE '%" & RepairRecivedDate & "%' AND "
        End If

        If CompleteDate <> "" Then
            sqlkey &= "CONVERT(VARCHAR, A.Completed_Date, 111) LIKE '%" & CompleteDate & "%' AND "
        End If

        '最後のANDを削除
        If sqlkey <> "" Then
            sqlkey = Left(sqlkey, Len(sqlkey) - 4)
        End If

        Dim keyPartNo As String = ""

        If TextParts_No1.Text <> "" Then
            keyPartNo &= "(A.Part_1 Like '%" & Trim(TextParts_No1.Text) & "%' OR A.Part_2 LIKE '%" & Trim(TextParts_No1.Text) & "%' OR A.Part_3 Like '%" & Trim(TextParts_No1.Text) & "%' OR "
            keyPartNo &= "A.Part_4 Like '%" & Trim(TextParts_No1.Text) & "%' OR A.Part_5 LIKE '%" & Trim(TextParts_No1.Text) & "%' OR A.Part_6 Like '%" & Trim(TextParts_No1.Text) & "%' OR "
            keyPartNo &= "A.Part_7 Like '%" & Trim(TextParts_No1.Text) & "%' OR A.Part_8 LIKE '%" & Trim(TextParts_No1.Text) & "%' OR A.Part_9 Like '%" & Trim(TextParts_No1.Text) & "%' OR "
            keyPartNo &= "A.Part_10 Like '%" & Trim(TextParts_No1.Text) & "%' OR A.Part_11 LIKE '%" & Trim(TextParts_No1.Text) & "%' OR A.Part_12 Like '%" & Trim(TextParts_No1.Text) & "%' OR "
            keyPartNo &= "A.Part_13 Like '%" & Trim(TextParts_No1.Text) & "%' OR A.Part_14 LIKE '%" & Trim(TextParts_No1.Text) & "%' OR A.Part_15 Like '%" & Trim(TextParts_No1.Text) & "%') AND "
        End If

        If TextParts_No2.Text <> "" Then
            keyPartNo &= "(A.Part_1 Like '%" & Trim(TextParts_No2.Text) & "%' OR A.Part_2 LIKE '%" & Trim(TextParts_No2.Text) & "%' OR A.Part_3 Like '%" & Trim(TextParts_No2.Text) & "%' OR "
            keyPartNo &= "A.Part_4 Like '%" & Trim(TextParts_No2.Text) & "%' OR A.Part_5 LIKE '%" & Trim(TextParts_No2.Text) & "%' OR A.Part_6 Like '%" & Trim(TextParts_No2.Text) & "%' OR "
            keyPartNo &= "A.Part_7 Like '%" & Trim(TextParts_No2.Text) & "%' OR A.Part_8 LIKE '%" & Trim(TextParts_No2.Text) & "%' OR A.Part_9 Like '%" & Trim(TextParts_No2.Text) & "%' OR "
            keyPartNo &= "A.Part_10 Like '%" & Trim(TextParts_No2.Text) & "%' OR A.Part_11 LIKE '%" & Trim(TextParts_No2.Text) & "%' OR A.Part_12 Like '%" & Trim(TextParts_No2.Text) & "%' OR "
            keyPartNo &= "A.Part_13 Like '%" & Trim(TextParts_No2.Text) & "%' OR A.Part_14 LIKE '%" & Trim(TextParts_No2.Text) & "%' OR A.Part_15 Like '%" & Trim(TextParts_No2.Text) & "%') AND "
        End If

        If TextParts_No3.Text <> "" Then
            keyPartNo &= "(A.Part_1 Like '%" & Trim(TextParts_No3.Text) & "%' OR A.Part_2 LIKE '%" & Trim(TextParts_No3.Text) & "%' OR A.Part_3 Like '%" & Trim(TextParts_No3.Text) & "%' OR "
            keyPartNo &= "A.Part_4 Like '%" & Trim(TextParts_No3.Text) & "%' OR A.Part_5 LIKE '%" & Trim(TextParts_No3.Text) & "%' OR A.Part_6 Like '%" & Trim(TextParts_No3.Text) & "%' OR "
            keyPartNo &= "A.Part_7 Like '%" & Trim(TextParts_No3.Text) & "%' OR A.Part_8 LIKE '%" & Trim(TextParts_No3.Text) & "%' OR A.Part_9 Like '%" & Trim(TextParts_No3.Text) & "%' OR "
            keyPartNo &= "A.Part_10 Like '%" & Trim(TextParts_No3.Text) & "%' OR A.Part_11 LIKE '%" & Trim(TextParts_No3.Text) & "%' OR A.Part_12 Like '%" & Trim(TextParts_No3.Text) & "%' OR "
            keyPartNo &= "A.Part_13 Like '%" & Trim(TextParts_No3.Text) & "%' OR A.Part_14 LIKE '%" & Trim(TextParts_No3.Text) & "%' OR A.Part_15 Like '%" & Trim(TextParts_No3.Text) & "%') AND "
        End If

        If TextParts_No4.Text <> "" Then
            keyPartNo &= "(A.Part_1 Like '%" & Trim(TextParts_No4.Text) & "%' OR A.Part_2 LIKE '%" & Trim(TextParts_No4.Text) & "%' OR A.Part_3 Like '%" & Trim(TextParts_No4.Text) & "%' OR "
            keyPartNo &= "A.Part_4 Like '%" & Trim(TextParts_No4.Text) & "%' OR A.Part_5 LIKE '%" & Trim(TextParts_No4.Text) & "%' OR A.Part_6 Like '%" & Trim(TextParts_No4.Text) & "%' OR "
            keyPartNo &= "A.Part_7 Like '%" & Trim(TextParts_No4.Text) & "%' OR A.Part_8 LIKE '%" & Trim(TextParts_No4.Text) & "%' OR A.Part_9 Like '%" & Trim(TextParts_No4.Text) & "%' OR "
            keyPartNo &= "A.Part_10 Like '%" & Trim(TextParts_No4.Text) & "%' OR A.Part_11 LIKE '%" & Trim(TextParts_No4.Text) & "%' OR A.Part_12 Like '%" & Trim(TextParts_No4.Text) & "%' OR "
            keyPartNo &= "A.Part_13 Like '%" & Trim(TextParts_No4.Text) & "%' OR A.Part_14 LIKE '%" & Trim(TextParts_No4.Text) & "%' OR A.Part_15 Like '%" & Trim(TextParts_No4.Text) & "%') AND "
        End If

        If TextParts_No5.Text <> "" Then
            keyPartNo &= "(A.Part_1 Like '%" & Trim(TextParts_No5.Text) & "%' OR A.Part_2 LIKE '%" & Trim(TextParts_No5.Text) & "%' OR A.Part_3 Like '%" & Trim(TextParts_No5.Text) & "%' OR "
            keyPartNo &= "A.Part_4 Like '%" & Trim(TextParts_No5.Text) & "%' OR A.Part_5 LIKE '%" & Trim(TextParts_No5.Text) & "%' OR A.Part_6 Like '%" & Trim(TextParts_No5.Text) & "%' OR "
            keyPartNo &= "A.Part_7 Like '%" & Trim(TextParts_No5.Text) & "%' OR A.Part_8 LIKE '%" & Trim(TextParts_No5.Text) & "%' OR A.Part_9 Like '%" & Trim(TextParts_No5.Text) & "%' OR "
            keyPartNo &= "A.Part_10 Like '%" & Trim(TextParts_No5.Text) & "%' OR A.Part_11 LIKE '%" & Trim(TextParts_No5.Text) & "%' OR A.Part_12 Like '%" & Trim(TextParts_No5.Text) & "%' OR "
            keyPartNo &= "A.Part_13 Like '%" & Trim(TextParts_No5.Text) & "%' OR A.Part_14 LIKE '%" & Trim(TextParts_No5.Text) & "%' OR A.Part_15 Like '%" & Trim(TextParts_No5.Text) & "%') AND "
        End If

        '最後のANDを削除
        If keyPartNo <> "" Then
            keyPartNo = Left(keyPartNo, Len(keyPartNo) - 4)
        End If

        If sqlkey = "" And keyPartNo = "" Then
            Call showMsg("Please enter search item.")
            grd_info.DataSource = Nothing
            grd_info.DataBind()
            Exit Sub
        End If

        Dim setSQL As String = ""
        Dim setSQL2 As String = ""
        Dim strSQL As String = ""
        Dim koumoku As String = ""
        Dim koumokuAll As String
        koumoku = "SELECT A.po_no, A.ASC_Claim_No, A.Samsung_Claim_No, A.Engineer, A.Serial_No, A.Model, A.IMEI_No, A.Consumer_Name, A.Consumer_Telephone, CONVERT(VARCHAR, A.Purchase_Date,111) AS Purchase_Date, CONVERT(VARCHAR, A.Repair_Received_Date, 111) AS Repair_Received_Date, CONVERT(VARCHAR, A.Completed_Date, 111) AS Completed_Date, A.Tr_No "
        koumokuAll = "SELECT A.* "
        strSQL &= "FROM dbo.T_repair1 A"
        strSQL &= ",dbo.M_ship_base B "
        strSQL &= "WHERE A.DELFG = 0 AND A.Branch_Code = '" & shipCode & "' "
        strSQL &= "AND LEFT(A.po_no, 2) = B.PO_no AND "

        If keyPartNo <> "" Then
            strSQL &= keyPartNo
            If sqlkey <> "" Then
                strSQL &= "AND " & sqlkey
            End If
        Else
            strSQL &= sqlkey
        End If

        strSQL &= "ORDER BY A.po_no;"

        'select文設定
        setSQL = koumoku & strSQL
        setSQL2 = koumokuAll & strSQL

        '***GRIDVIEWへ反映 ***
        If strSQL <> "" Then
            Dim errFlg As Integer
            Dim DT_SERCH As DataTable = DBCommon.ExecuteGetDT(setSQL, errFlg)

            If errFlg = 1 Then
                Call showMsg("Failed to acquire search information.")
                Exit Sub
            End If

            If DT_SERCH IsNot Nothing Then
                grd_info.DataSource = DT_SERCH
                grd_info.DataBind()
                '次ページ反映ように取得しておく
                Session("Data_DT_SERCH") = DT_SERCH
                'exportデータ用として取得しておく
                Session("set_SQL2") = setSQL2
                btnDownLoad.Enabled = True
            Else
                grd_info.DataSource = Nothing
                grd_info.DataBind()
                Call showMsg("There was no search result.")
                Exit Sub
            End If

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
    'GRIDVIEWのdetailボタンクリック処理
    Private Sub grd_info_RowCommand(sender As Object, e As GridViewCommandEventArgs) Handles grd_info.RowCommand

        'コマンド名が“detail”の場合にのみ処理
        If e.CommandName = "Detail" Then

            'クリック行のインデックス取得
            Dim Index As Integer = Convert.ToInt32(e.CommandArgument)

            '該当行取得
            Dim gridRow As GridViewRow = grd_info.Rows(Index)

            'PO番号取得
            Dim poNo As String = ""
            poNo = gridRow.Cells(1).Text

            Session("po_No") = poNo

            Response.Redirect("Repair_1_Detail.aspx")

        End If

    End Sub
    'ダウンロードボタン押下処理
    Protected Sub btnDownLoad_Click(sender As Object, e As ImageClickEventArgs) Handles btnDownLoad.Click

        'セッション情報を取得
        Dim strSQL As String = Session("set_SQL2")

        If strSQL <> "" Then

            Dim errFlg As Integer
            Dim clsSet As New Class_repair
            Dim dsT_repair1 As New DataSet

            dsT_repair1 = DBCommon.Get_DS(strSQL, errFlg)

            If errFlg = 1 Then
                Call showMsg("Failed to acquire T_repair 1.")
                Exit Sub
            End If

            If dsT_repair1 Is Nothing Then
                Call showMsg("There is no data in T_repair 1. Cancel processing.")
                Exit Sub
            Else

                Dim clsSetInv As New Class_inv
                Dim csvFileName As String = DateTime.Now.ToString("yyyyMMddHHmmss") & ".csv"
                Dim outputPath As String = clsSet.ExportCsvFilePass & csvFileName

                Session("csv_FileName") = outputPath

                Dim buf As New StringBuilder

                Call clsSet.exportData(dsT_repair1, buf)

                File.Delete(outputPath)

                Using writer As New StreamWriter(outputPath, False, Encoding.Default)
                    writer.WriteLine(String.Join(Environment.NewLine, buf))
                End Using

                ' ダウンロード
                Response.ContentType = "application/octet-stream"
                Response.AddHeader("Content-Disposition", "attachment; filename=" & csvFileName)
                Response.Flush()
                Response.WriteFile(outputPath)
                Response.End()

            End If

        End If

        Session("set_SQL2") = Nothing

    End Sub

End Class