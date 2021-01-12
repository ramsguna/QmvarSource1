Public Class Repair_Parts_Registration_Create
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        If IsPostBack = False Then

            '***セッションなしログインユーザの対応***
            Dim userid As String = Session("user_id")
            If userid = "" Then
                Response.Redirect("Login.aspx")
            End If

            '***セッション情報取得***
            Dim userName As String = Session("user_Name")
            Dim shipCode As String = Session("ship_code")
            Dim partsRegistration As String = Session("parts_Registration")

            '***時間とログイン名を表示****
            Dim clsCommon As New Class_common
            Dim dtNow As DateTime = clsCommon.dtIndia

            lblRecord.Text = dtNow.ToString("yyyyMMddHHmmss")
            lblYousername.Text = userName
            lblShipCode.Text = shipCode

            '***コントロール表示等の制御***
            If partsRegistration = "Create" Then
                lblModify.Visible = False
                TextPartsNumber.Visible = False
                btnStart2.Visible = False
            End If

        End If

    End Sub

    Protected Sub showMsg(ByVal Msg As String, ByVal msgChk As String)

        lblMsg.Text = Msg
        Dim sScript As String

        If msgChk = "CancelMsg" Then
            'OKとキャンセルボタン
            sScript = "$(function () {$(""#dialog"" ).dialog({width: 400,buttons:{""OK"": function () {$(this).dialog('close');$('[id$=""BtnOK""]').click();},""CANCEL"": function () {$(this).dialog('close');$('[id$=""BtnCancel""]').click();}}});});"
        ElseIf msgChk = "CancelMsg2" Then
            'OKとキャンセルボタン 
            sScript = "$(function () {$(""#dialog"" ).dialog({width: 400,buttons:{""OK"": function () {$(this).dialog('close');$('[id$=""Btn2OK""]').click();},""CANCEL"": function () {$(this).dialog('close');$('[id$=""BtnCancel""]').click();}}});});"
        Else
            'OKボタンのみ
            sScript = "$(function () { $( ""#dialog"" ).dialog({width: 400, buttons: {""OK"": function () {$(this).dialog('close');}}});});"
        End If

        'JavaScriptの埋め込み
        ClientScript.RegisterStartupScript(Me.GetType(), "startup", sScript, True)

    End Sub

    Protected Sub btnStart_Click(sender As Object, e As ImageClickEventArgs) Handles btnStart.Click

        '***セッション情報取得***
        Dim userid As String = Session("user_id")
        Dim shipCode As String = Session("ship_code")
        Dim partsRegistration As String = Session("parts_Registration")

        Call reSetText()

        '***入力チェック***
        Dim errMsg As String
        Call inputChk(errMsg)

        If errMsg <> "" Then
            Call showMsg(errMsg, "")
            Exit Sub
        End If

        '***設定***
        Dim partsMasterData As Class_repair.M_PARTS
        partsMasterData.ship_code = shipCode
        Call inputSet(partsMasterData)

        '■新規登録
        Dim errFlg As Integer
        Dim clsSet As New Class_repair
        If partsRegistration = "Create" Then

            '***登録済のデータか確認***
            Dim DT_M_PARTS As DataTable
            Dim strSQL As String = "SELECT * FROM dbo.M_PARTS WHERE parts_no = '" & partsMasterData.parts_no & "' AND DELFG = 0 and ship_code = '" & shipCode & "';"

            DT_M_PARTS = DBCommon.ExecuteGetDT(strSQL, errFlg)

            If errFlg = 1 Then
                Call showMsg("Failed to get information from M_PARTS. > ", "")
                Exit Sub
            End If

            If DT_M_PARTS IsNot Nothing Then
                Call showMsg("It is a registered part number", "")
                Exit Sub
            End If

            '***登録***
            Call clsSet.insert_M_PARTS(partsMasterData, userid, errFlg)

            If errFlg = 1 Then
                Call showMsg("Failed to register master of parts.", "")
                Exit Sub
            Else
                Call showMsg("success!!<br/>Parts Number " & partsMasterData.parts_no & "<br/>Registration has been completed.", "")
            End If

        ElseIf partsRegistration = "Modify" Then

            '更新
            Call clsSet.update_M_PARTS(partsMasterData, userid, errFlg)

            If errFlg = 1 Then
                Call showMsg("Failed to register master of parts.", "")
                Exit Sub
            Else
                Call showMsg("success!!<br/>Parts Number " & partsMasterData.parts_no & "<br/>Registration has been completed.", "")
                showData(partsMasterData.ship_code, partsMasterData.parts_no, errMsg)
                If errMsg <> "" Then
                    Call showMsg("Failed to display updated data.", "")
                    Exit Sub
                End If
            End If

        End If

    End Sub

    Protected Sub inputChk(ByRef errMsg As String)

        '必須項目チェック
        If Trim(TextMaker.Text) = "" Or Trim(TextPartsNo.Text) = "" Or Trim(TextPartsName.Text) = "" Or Trim(TextLoc1.Text) = "" Or Trim(TextUnitPrice.Text) = "" Or Trim(TextCategory.Text) = "" Or Trim(TextGUnitPrice.Text) = "" Then
            Call textChk()
            errMsg = "※Fill in the required items."
            Exit Sub
        End If

        If ChkNo.Checked = False And ChkYes.Checked = False Then
            ChkYes.Focus()
            errMsg = "Please check with or without serial."
            Exit Sub
        End If

        If Chktangible.Checked = False And ChkIntangible.Checked = False Then
            Chktangible.Focus()
            errMsg = "Please check unit_flg."
            Exit Sub
        End If

        '金額チェック
        Dim decimalChk As Decimal
        If Decimal.TryParse(Trim(TextUnitPrice.Text), decimalChk) = False Then
            TextUnitPrice.BackColor = System.Drawing.Color.FromArgb(255, 0, 0)
            errMsg = "Please enter the amount."
            Exit Sub
        End If

        If Decimal.TryParse(Trim(TextGUnitPrice.Text), decimalChk) = False Then
            TextGUnitPrice.BackColor = System.Drawing.Color.FromArgb(255, 0, 0)
            errMsg = "Please enter the amount."
            Exit Sub
        End If

        If Trim(TextComoensation.Text) <> "" Then
            If Decimal.TryParse(Trim(TextComoensation.Text), decimalChk) = False Then
                TextComoensation.BackColor = System.Drawing.Color.FromArgb(255, 0, 0)
                errMsg = "Please enter the amount."
                Exit Sub
            End If
        End If

        If Trim(TextTechfeePaid.Text) <> "" Then
            If Decimal.TryParse(Trim(TextTechfeePaid.Text), decimalChk) = False Then
                TextTechfeePaid.BackColor = System.Drawing.Color.FromArgb(255, 0, 0)
                errMsg = "Please enter the amount."
                Exit Sub
            End If
        End If

        If Trim(TextTechfeeGuarantee.Text) <> "" Then
            If Decimal.TryParse(Trim(TextTechfeeGuarantee.Text), decimalChk) = False Then
                TextTechfeeGuarantee.BackColor = System.Drawing.Color.FromArgb(255, 0, 0)
                errMsg = "Please enter the amount."
                Exit Sub
            End If
        End If

        '桁数チェック
        Dim work As String = Trim(TextCategory.Text)
        If work.Length > 3 Then
            TextCategory.BackColor = System.Drawing.Color.FromArgb(255, 0, 0)
            errMsg = "Up to 3 digits can be entered for Category."
            Exit Sub
        End If

    End Sub

    Protected Sub textChk()

        If Trim(TextMaker.Text) = "" Then
            TextMaker.BackColor = System.Drawing.Color.FromArgb(255, 0, 0)
        End If

        If Trim(TextPartsNo.Text) = "" Then
            TextPartsNo.BackColor = System.Drawing.Color.FromArgb(255, 0, 0)
        End If

        If Trim(TextPartsName.Text) = "" Then
            TextPartsName.BackColor = System.Drawing.Color.FromArgb(255, 0, 0)
        End If

        If Trim(TextLoc1.Text) = "" Then
            TextLoc1.BackColor = System.Drawing.Color.FromArgb(255, 0, 0)
        End If

        If Trim(TextUnitPrice.Text) = "" Then
            TextUnitPrice.BackColor = System.Drawing.Color.FromArgb(255, 0, 0)
        End If

        If Trim(TextCategory.Text) = "" Then
            TextCategory.BackColor = System.Drawing.Color.FromArgb(255, 0, 0)
        End If

        If Trim(TextGUnitPrice.Text) = "" Then
            TextGUnitPrice.BackColor = System.Drawing.Color.FromArgb(255, 0, 0)
        End If

    End Sub

    Protected Sub inputSet(ByRef partsMasterData As Class_repair.M_PARTS)

        partsMasterData.maker = Trim(TextMaker.Text)
        partsMasterData.parts_no = Trim(TextPartsNo.Text)
        partsMasterData.parts_name = Trim(TextPartsName.Text)
        partsMasterData.loc_1 = Trim(TextLoc1.Text)
        partsMasterData.loc_2 = Trim(TextLoc2.Text)
        partsMasterData.loc_3 = Trim(TextLoc3.Text)
        partsMasterData.category = Trim(TextCategory.Text)
        partsMasterData.product_name = Trim(TextProductName.Text)
        partsMasterData.Assing_type = Trim(TextAssingType.Text)

        '金額の設定
        If Trim(TextUnitPrice.Text) = "" Then
            partsMasterData.unit_price = 0.00
        Else
            partsMasterData.unit_price = Trim(TextUnitPrice.Text)
        End If

        If Trim(TextGUnitPrice.Text) = "" Then
            partsMasterData.g_unit_price = 0.00
        Else
            partsMasterData.g_unit_price = Trim(TextGUnitPrice.Text)
        End If

        If Trim(TextComoensation.Text) = "" Then
            partsMasterData.comoensation = 0.00
        Else
            partsMasterData.comoensation = Trim(TextComoensation.Text)
        End If

        If Trim(TextTechfeePaid.Text) = "" Then
            partsMasterData.techfee_paid = 0.00
        Else
            partsMasterData.techfee_paid = Trim(TextTechfeePaid.Text)
        End If

        If Trim(TextTechfeeGuarantee.Text) = "" Then
            partsMasterData.techfee_guarantee = 0.00
        Else
            partsMasterData.techfee_guarantee = Trim(TextTechfeeGuarantee.Text)
        End If

        '必須以外の項目がNULLの場合
        If partsMasterData.loc_2 = "" Then
            partsMasterData.loc_2 = partsMasterData.loc_1
        End If

        If partsMasterData.loc_3 = "" Then
            partsMasterData.loc_3 = partsMasterData.loc_1
        End If

        If Trim(TextComoensation.Text) = "" Then
            partsMasterData.comoensation = partsMasterData.g_unit_price
        End If

        If Trim(TextTechfeePaid.Text) = "" Then
            partsMasterData.techfee_paid = partsMasterData.g_unit_price
        End If

        If Trim(TextTechfeeGuarantee.Text) = "" Then
            partsMasterData.techfee_guarantee = partsMasterData.g_unit_price
        End If

        If partsMasterData.product_name = "" Then
            partsMasterData.product_name = "n/a"
        End If

        If partsMasterData.Assing_type = "" Then
            partsMasterData.Assing_type = "n/a"
        End If

        If Chktangible.Checked = True Then
            partsMasterData.unit_flg = 1
        End If

        If ChkYes.Checked = True Then
            partsMasterData.parts_flg = 1
        End If

    End Sub

    Protected Sub reSetText()

        TextUnitPrice.BackColor = System.Drawing.Color.FromArgb(255, 255, 255)
        TextGUnitPrice.BackColor = System.Drawing.Color.FromArgb(255, 255, 255)
        TextComoensation.BackColor = System.Drawing.Color.FromArgb(255, 255, 255)
        TextTechfeePaid.BackColor = System.Drawing.Color.FromArgb(255, 255, 255)
        TextTechfeeGuarantee.BackColor = System.Drawing.Color.FromArgb(255, 255, 255)
        TextMaker.BackColor = System.Drawing.Color.FromArgb(255, 255, 255)
        TextPartsNo.BackColor = System.Drawing.Color.FromArgb(255, 255, 255)
        TextPartsName.BackColor = System.Drawing.Color.FromArgb(255, 255, 255)
        TextLoc1.BackColor = System.Drawing.Color.FromArgb(255, 255, 255)
        TextUnitPrice.BackColor = System.Drawing.Color.FromArgb(255, 255, 255)
        TextCategory.BackColor = System.Drawing.Color.FromArgb(255, 255, 255)
        TextGUnitPrice.BackColor = System.Drawing.Color.FromArgb(255, 255, 255)

    End Sub

    Protected Sub btnStart2_Click(sender As Object, e As ImageClickEventArgs) Handles btnStart2.Click

        '***セッション情報取得***
        Dim shipCode As String = Session("ship_code")

        '***入力チェック***
        Dim partsNumber As String = Trim(TextPartsNumber.Text)
        If Trim(TextPartsNumber.Text) = "" Then
            Call showMsg("Please enter part number.", "")
            Exit Sub
        End If

        '***リセット***
        Call reSet()

        '***DBを参照して表示***
        Dim errMsg As String
        Call showData(shipCode, partsNumber, errMsg)

        If errMsg <> "" Then
            Call showMsg(errMsg, "")
            Exit Sub
        End If

    End Sub

    Protected Sub showData(ByVal shipCode As String, ByVal partsNumber As String, ByRef errMsg As String)

        Dim clsSetMoney As New Class_money
        Dim dsM_PARTS As New DataSet
        Dim errFlg As Integer

        Dim strSQL As String = "SELECT * FROM dbo.M_PARTS WHERE DELFG = 0 AND ship_code = '" & shipCode & "' AND parts_no = '" & partsNumber & "';"
        dsM_PARTS = DBCommon.Get_DS(strSQL, errFlg)

        If errFlg = 1 Then
            errMsg = "Failed to acquire part information from M_PARTS."
            Exit Sub
        End If

        If dsM_PARTS IsNot Nothing Then

            If dsM_PARTS.Tables(0).Rows.Count = 1 Then

                Dim dr As DataRow = dsM_PARTS.Tables(0).Rows(0)

                If dr("maker") IsNot DBNull.Value Then
                    Dim maker As String = dr("maker")
                    TextMaker.Text = maker
                End If

                If dr("parts_no") IsNot DBNull.Value Then
                    Dim parts_no As String = dr("parts_no")
                    TextPartsNo.Text = parts_no
                End If

                If dr("parts_name") IsNot DBNull.Value Then
                    Dim parts_name As String = dr("parts_name")
                    TextPartsName.Text = parts_name
                End If

                If dr("loc_1") IsNot DBNull.Value Then
                    Dim loc_1 As String = dr("loc_1")
                    TextLoc1.Text = loc_1
                End If

                If dr("loc_2") IsNot DBNull.Value Then
                    Dim loc_2 As String = dr("loc_2")
                    TextLoc2.Text = loc_2
                End If

                If dr("loc_3") IsNot DBNull.Value Then
                    Dim loc_3 As String = dr("loc_3")
                    TextLoc3.Text = loc_3
                End If

                If dr("unit_price") IsNot DBNull.Value Then
                    Dim unit_price As String = dr("unit_price")
                    TextUnitPrice.Text = clsSetMoney.setINR(unit_price)
                End If

                If dr("category") IsNot DBNull.Value Then
                    Dim category As String = dr("category")
                    TextCategory.Text = category
                End If

                If dr("g_unit_price") IsNot DBNull.Value Then
                    Dim g_unit_price As String = dr("g_unit_price")
                    TextGUnitPrice.Text = clsSetMoney.setINR(g_unit_price)
                End If

                If dr("comoensation") IsNot DBNull.Value Then
                    Dim comoensation As String = dr("comoensation")
                    TextComoensation.Text = clsSetMoney.setINR(comoensation)
                End If

                If dr("techfee_paid") IsNot DBNull.Value Then
                    Dim techfee_paid As String = dr("techfee_paid")
                    TextTechfeePaid.Text = clsSetMoney.setINR(techfee_paid)
                End If

                If dr("techfee_guarantee") IsNot DBNull.Value Then
                    Dim techfee_guarantee As String = dr("techfee_guarantee")
                    TextTechfeeGuarantee.Text = clsSetMoney.setINR(techfee_guarantee)
                End If

                If dr("product_name") IsNot DBNull.Value Then
                    Dim product_name As String = dr("product_name")
                    TextProductName.Text = product_name
                End If

                If dr("Assing_type") IsNot DBNull.Value Then
                    Dim Assing_type As String = dr("Assing_type")
                    TextAssingType.Text = Assing_type
                End If

                If dr("unit_flg") IsNot DBNull.Value Then
                    Dim unit_flg As Boolean = dr("unit_flg")
                    If unit_flg = True Then
                        Chktangible.Checked = True
                    Else
                        ChkIntangible.Checked = True
                    End If
                End If

                If dr("parts_flg") IsNot DBNull.Value Then
                    Dim parts_flg As Boolean = dr("parts_flg")
                    If parts_flg = True Then
                        ChkYes.Checked = True
                    Else
                        ChkNo.Checked = True
                    End If
                End If

            End If

        Else
            errMsg = "There is no corresponding parts information."
            Exit Sub
        End If

    End Sub

    Protected Sub reSet()

        TextMaker.Text = ""
        TextPartsNo.Text = ""
        TextPartsName.Text = ""
        TextLoc1.Text = ""
        TextLoc2.Text = ""
        TextLoc3.Text = ""
        TextUnitPrice.Text = ""
        TextCategory.Text = ""
        TextGUnitPrice.Text = ""
        TextComoensation.Text = ""
        TextTechfeePaid.Text = ""
        TextTechfeeGuarantee.Text = ""
        TextProductName.Text = ""
        TextAssingType.Text = ""
        Chktangible.Checked = False
        ChkIntangible.Checked = False
        ChkYes.Checked = False
        ChkNo.Checked = False

    End Sub

    Private Sub Chktangible_CheckedChanged(sender As Object, e As EventArgs) Handles Chktangible.CheckedChanged

        If Chktangible.Checked = True Then
            ChkIntangible.Checked = False
        End If

    End Sub

    Private Sub ChkIntangible_CheckedChanged(sender As Object, e As EventArgs) Handles ChkIntangible.CheckedChanged

        If ChkIntangible.Checked = True Then
            Chktangible.Checked = False
        End If

    End Sub

    Private Sub ChkYes_CheckedChanged(sender As Object, e As EventArgs) Handles ChkYes.CheckedChanged

        If ChkYes.Checked = True Then
            ChkNo.Checked = False
        End If

    End Sub

    Private Sub ChkNo_CheckedChanged(sender As Object, e As EventArgs) Handles ChkNo.CheckedChanged

        If ChkNo.Checked = True Then
            ChkYes.Checked = False
        End If

    End Sub

End Class
