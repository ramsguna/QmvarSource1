Public Class Money_Record_3_aspx
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        '初期表示
        If IsPostBack = False Then

            '***セッションなしログインユーザの対応***
            Dim userid As String = Session("user_id")
            If userid = "" Then
                Response.Redirect("Login.aspx")
            End If

            BtnComplate.Visible = False
            btnSend.Enabled = False
            lblToApply.Text = "※Please specify the state."

        Else
            Dim BtnPdfChk As String = ""
            For Each s In Context.Request.Form.AllKeys
                If s.Contains("BtnPdf") Then
                    BtnPdfChk = "LastPdf"
                    Exit For
                End If
            Next s

            '***登録OK後見積書作成***
            If BtnPdfChk = "LastPdf" Then
                Dim pdfFileName As String = Session("pdf_FileName")
                Call reSetSession()
                Call FileDownload(pdfFileName, "application/pdf")
            End If

        End If

    End Sub
    'startボタン押下処理
    Protected Sub btnStart_Click(sender As Object, e As ImageClickEventArgs) Handles btnStart.Click

        '***セッション情報取得***
        Dim userid As String = Session("user_id")
        Dim shipCode As String = Session("ship_code")
        Dim userName As String = Session("user_Name")

        If Session("user_id") Is Nothing Then
            Call showMsg("The session is clear. Please login again.", "")
            Exit Sub
        End If

        '***リスト等の初期化****
        Call reSet()

        '***ユーザ情報取得  時間とログイン名を表示****
        'ログイン名表示
        lblYousername.Text = userName

        '時間を表示
        Dim clsSetCommon As New Class_common
        Dim dtNow As DateTime = clsSetCommon.dtIndia
        lblRecord.Text = dtNow.ToString("yyyyMMddHHmmss")

        '***メーカ情報取得***（QGS or サムスン）
        Dim errFlgDt As Integer
        Dim poM As String = ""
        Dim strSQL As String = "SELECT * FROM dbo.M_ship_base WHERE DELFG = 0 AND ship_code = '" & shipCode & "';"
        Dim DT_M_ship_base As DataTable = DBCommon.ExecuteGetDT(strSQL, errFlgDt)

        If errFlgDt = 1 Then
            Call showMsg("Failed to acquire manufacturer information.", "")
            Exit Sub
        End If

        If DT_M_ship_base IsNot Nothing Then
            If DT_M_ship_base.Rows(0)("PO_no") IsNot DBNull.Value Then
                poM = DT_M_ship_base.Rows(0)("PO_no")
            End If
        End If

        '***po最終番号取得***
        Dim poMaxStr As String = ""
        Dim poNoMax As Long
        Dim strSQL2 As String = "SELECT MAX(RIGHT(po_no,8)) AS po_no_max FROM dbo.T_repair1;"
        Dim DT_T_repair1 As DataTable
        DT_T_repair1 = DBCommon.ExecuteGetDT(strSQL2, errFlgDt)

        If errFlgDt = 1 Then
            Call showMsg("Failed to acquire po information.", "")
            Exit Sub
        End If

        If DT_T_repair1 IsNot Nothing Then
            If DT_T_repair1.Rows(0)("po_no_max") IsNot DBNull.Value Then
                poMaxStr = DT_T_repair1.Rows(0)("po_no_max")
                poNoMax = Convert.ToInt64(poMaxStr)
                poNoMax = poNoMax + 1
            End If
        End If

        '***ProductTypeをリストにセット***
        Dim errMsg As String
        Call setListProductType(poM, "", errMsg)
        If errMsg <> "" Then
            Call showMsg(errMsg, "")
            Exit Sub
        End If

        '***全てのstate情報をリストにセット***
        Dim errFlg As Integer
        Call setListPostal("", "", errMsg)
        If errMsg <> "" Then
            Call showMsg(errMsg, "")
            Exit Sub
        End If

        Dim poNo As String = Trim(TextPo.Text)

        If poNo = "" Then
            'po(管理番号)を表示
            TextPo.Text = poM & poNoMax.ToString("00000000")
            BtnComplate.Visible = False
        Else
            '***管理番号に紐づく情報を表示***
            Dim dsT_repair1 As New DataSet
            Dim strSQL3 As String = "SELECT * FROM dbo.T_repair1 WHERE DELFG = 0 AND Branch_Code = '" & shipCode & "' AND po_no = '" & poNo & "';"
            dsT_repair1 = DBCommon.Get_DS(strSQL3, errFlg)

            If errFlg = 1 Then
                Call showMsg("Failed to acquire po information from T_repair 1.", "")
                Exit Sub
            End If

            If dsT_repair1 IsNot Nothing Then

                If dsT_repair1.Tables(0).Rows.Count = 1 Then

                    Dim dr As DataRow = dsT_repair1.Tables(0).Rows(0)

                    If dr("comment") IsNot DBNull.Value Then
                        Dim comment As String = dr("comment")
                        TextComment.Text = comment
                    End If

                    If dr("Consumer_Name") IsNot DBNull.Value Then
                        Dim ConsumerName As String = dr("Consumer_Name")
                        TextConsumer_Name.Text = ConsumerName
                    End If

                    If dr("Consumer_Addr1") IsNot DBNull.Value Then
                        Dim ConsumerAddr1 As String = dr("Consumer_Addr1")
                        TextConsumer_Addr1.Text = ConsumerAddr1
                    End If

                    If dr("Consumer_Addr2") IsNot DBNull.Value Then
                        Dim ConsumerAddr2 As String = dr("Consumer_Addr2")
                        TextConsumer_Addr2.Text = ConsumerAddr2
                    End If

                    If dr("Consumer_MailAddress") IsNot DBNull.Value Then
                        Dim Customer_MailAddress As String = dr("Consumer_MailAddress")
                        TextCustomer_mail_address.Text = Customer_MailAddress
                    End If

                    If dr("Consumer_Telephone") IsNot DBNull.Value Then
                        Dim ConsumerTelephone As String = dr("Consumer_Telephone")
                        TextConsumer_Telephone.Text = ConsumerTelephone
                    End If

                    If dr("Consumer_Fax") IsNot DBNull.Value Then
                        Dim ConsumerFax As String = dr("Consumer_Fax")
                        TextConsumer_Fax.Text = ConsumerFax
                    End If

                    If dr("Postal_Code") IsNot DBNull.Value Then
                        Dim PostalCode As String = dr("Postal_Code")
                        TextPostal_Code.Text = PostalCode
                        Call setListPostal(PostalCode, "", errMsg)
                        If errMsg <> "" Then
                            Call showMsg(errMsg, "")
                            Call reSet()
                            Exit Sub
                        End If
                    End If

                    If dr("State_Name") IsNot DBNull.Value Then
                        Dim StateName As String = dr("State_Name")
                        Call setListPostal("", StateName, errMsg)
                        If errMsg <> "" Then
                            Call showMsg(errMsg, "")
                            Call reSet()
                            Exit Sub
                        End If
                    End If

                    If dr("Model") IsNot DBNull.Value Then
                        Dim Model As String = dr("Model")
                        TextModel.Text = Model
                    End If

                    If dr("Serial_No") IsNot DBNull.Value Then
                        Dim SerialNo As String = dr("Serial_No")
                        TextSerial_No.Text = SerialNo
                    End If

                    If dr("IMEI_No") IsNot DBNull.Value Then
                        Dim IMEI_No As String = dr("IMEI_No")
                        TextIMEI_No.Text = IMEI_No
                    End If

                    If dr("Repair_Description") IsNot DBNull.Value Then
                        Dim RepairDescription As String = dr("Repair_Description")
                        TextRepair_Description.Text = RepairDescription
                    End If

                    If dr("Repair_Received_Date") IsNot DBNull.Value Then
                        Dim RepairReceivedDate As DateTime = dr("Repair_Received_Date")
                        TextRepair_Received_Date.Text = RepairReceivedDate.ToShortDateString
                    End If

                    If dr("Maker") IsNot DBNull.Value Then
                        Dim Maker As String = dr("Maker")
                        TextMaker.Text = Maker
                    End If

                    If dr("Product_Type") IsNot DBNull.Value Then
                        Dim ProductType As String = dr("Product_Type")
                        DropListProductType.Items.Clear()
                        Call setListProductType(poM, ProductType, errMsg)
                        If errMsg <> "" Then
                            Call showMsg(errMsg, "")
                            Call reSet()
                            Exit Sub
                        End If
                    End If
                    BtnComplate.Visible = True
                End If
            Else
                If poNo <> poM & poNoMax.ToString("00000000") Then
                    Call showMsg("po情報が存在しません。", "")
                    BtnComplate.Visible = False
                    Exit Sub
                End If
            End If
        End If

        '***セッション設定***
        Session("poNo_Max") = poNoMax
        Session("po_No_NoGSPN") = poNo
        Session("po_M") = poM

        '***コントロール制御***
        btnSend.Enabled = True

    End Sub

    Protected Sub showMsg(ByVal Msg As String, ByVal msgChk As String)

        lblMsg.Text = Msg
        Dim sScript As String

        If msgChk = "CancelMsg" Then
            'OKとキャンセルボタン
            sScript = "$(function () {$(""#dialog"" ).dialog({width: 400,buttons:{""OK"": function () {$(this).dialog('close');$('[id$=""BtnOK""]').click();},""CANCEL"": function () {$(this).dialog('close');$('[id$=""BtnCancel""]').click();}}});});"
        ElseIf msgChk = "BtnPdf" Then
            'OKボタンのみ　完了OK ⇒受付シート作成
            sScript = "$(function () {$(""#dialog"" ).dialog({width: 400,buttons:{""OK"": function () {$(this).dialog('close');$('[id$=""BtnPdf""]').click();}}});});"
        Else
            'OKボタンのみ
            sScript = "$(function () { $( ""#dialog"" ).dialog({width: 400, buttons: {""OK"": function () {$(this).dialog('close');}}});});"
        End If

        'JavaScriptの埋め込み
        ClientScript.RegisterStartupScript(Me.GetType(), "startup", sScript, True)

    End Sub
    '完了画面へ遷移
    Protected Sub BtnComplate_Click(sender As Object, e As EventArgs) Handles BtnComplate.Click

        Response.Redirect("Money_Record_4.aspx")

    End Sub

    Protected Sub reSet()

        TextComment.Text = ""
        TextConsumer_Name.Text = ""
        TextConsumer_Addr1.Text = ""
        TextConsumer_Addr2.Text = ""
        TextConsumer_Telephone.Text = ""
        TextCustomer_mail_address.Text = ""
        TextConsumer_Fax.Text = ""
        TextPostal_Code.Text = ""
        TextModel.Text = ""
        TextSerial_No.Text = ""
        TextIMEI_No.Text = ""
        TextRepair_Description.Text = ""
        TextRepair_Received_Date.Text = ""
        TextMaker.Text = ""
        lblToApply.Text = ""
        DropListState.Items.Clear()
        DropListProductType.Items.Clear()
        BtnComplate.Visible = False
        lblToApply.Text = "※Please specify the state."

    End Sub
    'sendボタン押下処理
    Protected Sub btnSend_Click(sender As Object, e As ImageClickEventArgs) Handles btnSend.Click

        '***セッション情報取得***
        Dim userid As String = Session("user_id")
        Dim userName As String = Session("user_Name")
        Dim shipCode As String = Session("ship_code")
        Dim shipName As String = Session("ship_name")
        Dim userLevel As String = Session("user_level")
        Dim adminFlg As Boolean = Session("admin_Flg")
        Dim poNoMax As Long = Session("poNo_Max")
        Dim poM As String = Session("po_M")

        If userid Is Nothing Then
            Call showMsg("The session has expired. Please login again.", "")
            Exit Sub
        End If

        Dim levelFlg As String
        If userLevel = "0" Or userLevel = "1" Or userLevel = "2" Or adminFlg = True Then
            levelFlg = "1"
        End If

        '***入力チェック/設定***
        Dim clsSet As New Class_money
        Dim otherData As Class_money.OTHER_DATA
        Dim autoChk As Integer

        Dim ConsumerName As String = Trim(TextConsumer_Name.Text)
        If ConsumerName = "" Then
            Call showMsg("Please enter Consumer Name.", "")
            Exit Sub
        Else
            '##指定あり、自動登録
            otherData.Consumer_Name = ConsumerName
            If Left(otherData.Consumer_Name, 2) = "##" Then
                autoChk = 1
            End If
        End If

        Dim SerialNo As String = Trim(TextSerial_No.Text)
        If SerialNo = "" Then
            Call showMsg("Please enter Serial No.", "")
            Exit Sub
        Else
            otherData.Serial_No = SerialNo
        End If

        If DropListProductType.Text = "select product type" Or DropListProductType.Text = "" Then
            Call showMsg("Please select product type.", "")
            Exit Sub
        Else
            otherData.Product_Type = DropListProductType.Text
        End If

        'post番号チェック
        Dim postCord As String = Trim(Left(TextPostal_Code.Text, 2))
        Dim stateName As String = DropListState.Text
        Dim errMsg As String = ""
        If autoChk = 0 Then
            If clsSet.chkPostCord(postCord, stateName, errMsg) = False Then
                If errMsg <> "" Then
                    Call showMsg(errMsg, "")
                    Exit Sub
                End If
                Call showMsg("Zip code and name do not match. Please press the mail button.", "")
                Exit Sub
            End If
        End If

        '日付チェック
        Dim dt As DateTime
        If DateTime.TryParse(Trim(TextRepair_Received_Date.Text), dt) Then
            otherData.Repair_Received_Date = DateTime.Parse(TextRepair_Received_Date.Text)
        Else
            If Trim(TextRepair_Received_Date.Text) <> "" Then
                Call showMsg("The received date format should be YYYY/MM/DD", "")
                Exit Sub
            Else
                otherData.Repair_Received_Date = ""
            End If
        End If

        'お客様情報確認　※#指定あり：既存の登録情報より登録
        If autoChk = 1 Then
            Dim errFlg As Integer
            otherData.Consumer_Name = Right(otherData.Consumer_Name, otherData.Consumer_Name.Length - 2)
            Call clsSet.set_ConsumerInfo(otherData, shipCode, errFlg)
            If errFlg = 1 Then
                Call showMsg("The customer information is not registered", "")
                Exit Sub
            End If
        Else
            otherData.Consumer_Addr1 = Trim(TextConsumer_Addr1.Text)
            otherData.Consumer_Addr2 = Trim(TextConsumer_Addr2.Text)
            otherData.Customer_mail_address = Trim(TextCustomer_mail_address.Text)
            otherData.Consumer_Telephone = Trim(TextConsumer_Telephone.Text)
            otherData.Consumer_Fax = Trim(TextConsumer_Fax.Text)
            otherData.State_Name = stateName
            otherData.Postal_Code = postCord
        End If

        otherData.rec_yuser = userName
        otherData.comment = Trim(TextComment.Text)
        otherData.Model = Trim(TextModel.Text)
        otherData.IMEI_No = Trim(TextIMEI_No.Text)
        otherData.Maker = Trim(TextMaker.Text)
        otherData.po_no = Trim(TextPo.Text)
        otherData.Repair_Description = Trim(TextRepair_Description.Text)

        otherData.Repair_Description = Trim(TextRepair_Description.Text)

        '***店舗情報を設定***
        Dim errFlgDt As Integer
        Dim DT_M_ship_base As DataTable

        Dim strSQL2 As String = "SELECT * FROM dbo.M_ship_base WHERE DELFG = 0 and ship_code = '" & shipCode & "';"

        DT_M_ship_base = DBCommon.ExecuteGetDT(strSQL2, errFlgDt)

        If errFlgDt = 1 Then
            Call showMsg("Failed to acquire information from M_ship_base.", "")
            Exit Sub
        End If

        If DT_M_ship_base IsNot Nothing Then

            If DT_M_ship_base.Rows(0)("ship_add1") IsNot DBNull.Value Then
                otherData.Ship_Addr1 = DT_M_ship_base.Rows(0)("ship_add1")
            End If

            If DT_M_ship_base.Rows(0)("e_mail") IsNot DBNull.Value Then
                otherData.Ship_Mail = DT_M_ship_base.Rows(0)("e_mail")
            End If

            If DT_M_ship_base.Rows(0)("ship_tel") IsNot DBNull.Value Then
                otherData.Ship_Tel = DT_M_ship_base.Rows(0)("ship_tel")
            End If

            If DT_M_ship_base.Rows(0)("GSTIN") IsNot DBNull.Value Then
                otherData.GSTIN = DT_M_ship_base.Rows(0)("GSTIN")
            End If

            If DT_M_ship_base.Rows(0)("ship_name") IsNot DBNull.Value Then
                otherData.Ship_name = DT_M_ship_base.Rows(0)("ship_name")
            End If

        End If

        '***登録済のデータか確認***
        Dim DT_T_repair1 As DataTable
        Dim strSQL As String = "SELECT * FROM dbo.T_repair1 WHERE po_no = '" & otherData.po_no & "' AND DELFG = 0;"
        '※主キーがpo_noのみのため。
        'Dim strSQL As String = "SELECT * FROM dbo.T_repair1 WHERE po_no = '" & otherData.po_no & "' AND DELFG = 0 and Branch_Code = '" & shipCode & "';"

        DT_T_repair1 = DBCommon.ExecuteGetDT(strSQL, errFlgDt)

        If errFlgDt = 1 Then
            Call reSetSession()
            Call reSet()
            btnSend.Enabled = False
            Call showMsg("Failed to get information from T_repair 1.<br />Please check again after pressing the start button. ", "")
            Exit Sub
        End If

        If DT_T_repair1 Is Nothing Then

            '***データ登録***
            Dim errFlg As Integer
            Dim tourokuFlg As Integer = -1

            Call clsSet.setCsvData2(Nothing, errFlg, otherData, Nothing, userid, shipCode, poM, poNoMax, "", levelFlg, tourokuFlg)

            If errFlg = 1 Then
                Call reSetSession()
                Call reSet()
                btnSend.Enabled = False
                Call showMsg("Failed to register for reception <br />Please check again after pressing the start button.", "")
                Exit Sub
            Else
                '***PDF出力処理***
                Dim clsSetCommon As New Class_common
                Dim dtNow As DateTime = clsSetCommon.dtIndia
                Dim pdfFileName As String = dtNow.ToString("yyyyMMddHHmmss") & "_Receptionist.pdf"
                Dim FilePath As String = clsSet.savePDFPass & pdfFileName
                Call clsSet.ReceptionistPDF(otherData, pdfFileName, errFlg, shipName)
                If errFlg = 1 Then
                    'サーバに保存されたファイルを削除
                    If FilePath <> "" Then
                        If System.IO.File.Exists(FilePath) = True Then
                            System.IO.File.Delete(FilePath)
                        End If
                    End If
                    Call reSetSession()
                    Call reSet()
                    btnSend.Enabled = False
                    Call showMsg("PDF output is failed. " & "<br />Please check again after pressing the start button.", "")
                    Exit Sub
                Else
                    'メッセージ出力後、load処理で、受付シートファイルをダウンロード
                    Session("pdf_FileName") = pdfFileName
                    Call showMsg("Data registration is complete.<br />Please download the reception sheet.", "BtnPdf")
                End If
            End If
        Else
            Call showMsg(otherData.po_no & " : Since it has already been accepted, it can not be registered.", "")
            Exit Sub
        End If

        '***後処理***
        Call reSetSession()

    End Sub

    Protected Sub reSetSession()

        Session("poNo_Max") = Nothing
        Session("po_M") = Nothing
        Session("po_No_NoGSPN") = Nothing

    End Sub
    'ProductTypeをリストにセット
    Protected Sub setListProductType(ByVal poM As String, ByVal ProductType As String, ByRef errMsg As String)

        Dim errFlg As Integer
        Dim dsMgroup As New DataSet
        Dim sqlStr As String

        sqlStr = "SELECT * FROM dbo.M_group WHERE DELFG = 0 AND group_no = '4' AND item_1 = '" & poM & "';"
        dsMgroup = DBCommon.Get_DS(sqlStr, errFlg)

        If errFlg = 1 Then
            errMsg = "Failed to acquire M_group information."
            Exit Sub
        End If

        If ProductType = "" Then

            If dsMgroup Is Nothing Then
                errMsg = "Manufacturer information does not exist"
                Exit Sub
            Else

                If poM <> "SS" Then
                    DropListProductType.Items.Add("select product type")
                End If

                If dsMgroup.Tables(0).Rows.Count <> 0 Then
                    For i = 0 To dsMgroup.Tables(0).Rows.Count - 1
                        Dim dr As DataRow = dsMgroup.Tables(0).Rows(i)
                        With DropListProductType
                            If dr("item_2") IsNot DBNull.Value Then
                                .Items.Add(dr("item_2"))
                            End If
                        End With
                    Next i
                End If

            End If

        Else

            DropListProductType.Items.Add(ProductType)

            If poM <> "SS" Then
                If dsMgroup.Tables(0).Rows.Count <> 0 Then
                    For i = 0 To dsMgroup.Tables(0).Rows.Count - 1
                        Dim dr As DataRow = dsMgroup.Tables(0).Rows(i)
                        With DropListProductType
                            If dr("item_2") IsNot DBNull.Value Then
                                If ProductType <> dr("item_2") Then
                                    .Items.Add(dr("item_2"))
                                End If
                            End If
                        End With
                    Next i
                End If
            End If

        End If

    End Sub
    '郵便番号よりstate情報取得
    Protected Sub btnPostal_Click(sender As Object, e As EventArgs) Handles btnPostal.Click

        Dim PostalCode As String = Trim(TextPostal_Code.Text)
        Dim strSQL As String = ""
        Dim errMsg As String = ""
        Dim errFlg As Integer

        DropListState.Items.Clear()
        lblToApply.Text = "※Please specify the state."

        If PostalCode = "" Then

            Call setListPostal("", "", errMsg)
            If errMsg <> "" Then
                Call showMsg(errMsg, "")
                Exit Sub
            End If

        Else

            PostalCode = Left(PostalCode, 2)

            strSQL = "SELECT * FROM dbo.M_Postal WHERE zip_code = '" & PostalCode & "';"

            Dim DT_M_Postal As DataTable = DBCommon.ExecuteGetDT(strSQL, errFlg)

            If errFlg = 1 Then
                Call showMsg("DB(M_Posta)Failed to get the information", "")
                Exit Sub
            End If

            If DT_M_Postal IsNot Nothing Then

                If DT_M_Postal.Rows(0)("state") IsNot DBNull.Value Then

                    DropListState.Items.Add(DT_M_Postal.Rows(0)("state"))

                    Call chkApply(DT_M_Postal.Rows(0)("state"), errMsg)

                    If errMsg <> "" Then
                        btnSend.Enabled = False
                        Call showMsg(errMsg, "")
                        Exit Sub
                    End If

                    If DT_M_Postal.Rows(0)("state") = "" Then
                        Call showMsg("The state name for the code does not exist.", "")
                        Exit Sub
                    End If

                    TextPostal_Code.Text = PostalCode

                Else
                    Call showMsg("The state of the specified code does not exist.", "")
                    Exit Sub
                End If
            Else
                Call showMsg("The specified code does not exist.", "")
                Exit Sub
            End If

        End If

    End Sub
    '州情報をリストにセット
    Protected Sub setListPostal(ByVal posCord As String, ByVal stateName As String, ByRef errMsg As String)

        DropListState.Items.Clear()

        Dim strSQL As String = "SELECT DISTINCT state FROM dbo.M_Postal;"
        Dim errFlg As Integer
        Dim dsM_Postal As New DataSet
        dsM_Postal = DBCommon.Get_DS(strSQL, errFlg)

        If errFlg = 1 Then
            errMsg = "DB(M_Posta)Failed to get information from."
            Exit Sub
        End If

        Dim errMsg2 As String

        If posCord = "" Then

            If stateName = "" Then

                '■郵便情報登録なし：全ての州を表示
                If dsM_Postal IsNot Nothing Then
                    DropListState.Items.Add("select state")
                    If dsM_Postal.Tables(0).Rows.Count <> 0 Then
                        For i = 0 To dsM_Postal.Tables(0).Rows.Count - 1
                            Dim dr As DataRow = dsM_Postal.Tables(0).Rows(i)
                            With DropListState
                                .Items.Add(dr("state"))
                            End With
                        Next i
                    End If
                Else
                    errMsg = "Postal information does not exist."
                    Exit Sub
                End If

            Else

                '■州名称のみあり：1行目に、登録済の州を表示　２行目以下は全ての州を表示
                If dsM_Postal IsNot Nothing Then
                    DropListState.Items.Add(stateName)
                    Call chkApply(stateName, errMsg2)
                    If errMsg2 <> "" Then
                        errMsg = errMsg2
                        Exit Sub
                    End If
                    If dsM_Postal.Tables(0).Rows.Count <> 0 Then
                        For i = 0 To dsM_Postal.Tables(0).Rows.Count - 1
                            Dim dr As DataRow = dsM_Postal.Tables(0).Rows(i)
                            With DropListState
                                If stateName <> dr("state") Then
                                    .Items.Add(dr("state"))
                                End If
                            End With
                        Next i
                    End If
                Else
                    errMsg = "Postal information does not exist."
                    Exit Sub
                End If

            End If

        Else

            '■コード情報のみあり：'1行目に、登録済の州を表示　２行目以下は全ての州を表示
            If stateName = "" Then

                posCord = Left(posCord, 2)

                strSQL = "SELECT * FROM dbo.M_Postal WHERE zip_code = '" & posCord & "';"

                Dim DT_M_Postal As DataTable = DBCommon.ExecuteGetDT(strSQL, errFlg)

                If errFlg = 1 Then
                    errMsg = "DB(M_Posta) Failed to get the information"
                    Exit Sub
                End If

                If DT_M_Postal IsNot Nothing Then

                    If DT_M_Postal.Rows(0)("state") IsNot DBNull.Value Then
                        DropListState.Items.Add(DT_M_Postal.Rows(0)("state"))
                        Call chkApply(DT_M_Postal.Rows(0)("state"), errMsg2)
                        If errMsg2 <> "" Then
                            errMsg = errMsg2
                            Exit Sub
                        End If
                    Else
                        errMsg = "The state of the specified code does not exist."
                        Exit Sub
                    End If

                    If dsM_Postal.Tables(0).Rows.Count <> 0 Then
                        For i = 0 To dsM_Postal.Tables(0).Rows.Count - 1
                            Dim dr As DataRow = dsM_Postal.Tables(0).Rows(i)
                            With DropListState
                                If DT_M_Postal.Rows(0)("state") <> dr("state") Then
                                    .Items.Add(dr("state"))
                                End If
                            End With
                        Next i
                    End If
                Else
                    errMsg = "The specified code does not exist."
                End If

            End If

        End If

    End Sub
    '税金種別をチェック
    Protected Sub chkApply(ByVal PostalName As String, ByRef errMsg As String)

        '***セッション情報取得***
        Dim userid As String = Session("user_id")
        Dim shipCode As String = Session("ship_code")

        If shipCode Is Nothing Then
            errMsg = "The session has expired. Please login again."
            Exit Sub
        End If

        '***画面右側に州の種別を表示(SGST CGST/IGST)***
        '州名の表示があり
        If PostalName <> "" Then

            Dim strSQL As String = "Select * FROM dbo.M_ship_base WHERE DELFG = 0 And ship_code = '" & shipCode & "';"
            Dim errFlg As Integer
            Dim DT_M_ship_base As DataTable = DBCommon.ExecuteGetDT(strSQL, errFlg)

            If errFlg = 1 Then
                errMsg = "DB(M_ship_base)Failed to get information"
                Exit Sub
            End If

            Dim item_2 As String '拠点の州名称
            If DT_M_ship_base IsNot Nothing Then
                If DT_M_ship_base.Rows(0)("item_2") IsNot DBNull.Value Then
                    '拠点の州名称
                    item_2 = DT_M_ship_base.Rows(0)("item_2")
                Else
                    errMsg = "Since the state name of the base does not exist, tax type can not be confirmed."
                    Exit Sub
                End If
            Else
                errMsg = "Because there is no location information, tax type can not be confirmed."
                Exit Sub
            End If

            lblToApply.Text = ""

            If item_2 = "" Then
                errMsg = "Since the state name of the base does not exist, tax type can not be confirmed."
                Exit Sub
            End If

            If PostalName = item_2 Then
                lblToApply.Text = "SGST CGST"
            Else
                lblToApply.Text = "IGST"
            End If

        End If

    End Sub
    '州を選択時処理
    Private Sub DropListState_TextChanged(sender As Object, e As EventArgs) Handles DropListState.TextChanged

        Dim PostalName As String = DropListState.Text
        Dim errMsg As String

        chkApply(PostalName, errMsg)
        If errMsg <> "" Then
            btnSend.Enabled = False
            Call showMsg(errMsg, "")
            Exit Sub
        End If

    End Sub

    Protected Sub BtnPdf_Click(sender As Object, e As EventArgs) Handles BtnPdf.Click

    End Sub

    Protected Sub FileDownload(FileName As String, MimeType As String)

        Dim clsSet As New Class_money

        Dim FilePath As String

        'ダウンロードするファイル名
        Dim dlFileName As String

        If Request.Browser.Browser = "IE" Then
            'IEの場合はファイル名をURLエンコード
            dlFileName = HttpUtility.UrlEncode(FileName)
        Else
            'IE以外の場合はそのままでOK
            dlFileName = FileName
        End If

        FilePath = clsSet.savePDFPass & FileName

        Try

            'ファイルの内容をバイト配列にすべて読み込む 
            Dim Buffer As Byte() = System.IO.File.ReadAllBytes(FilePath)

            'サーバに保存されたCSVファイルを削除
            '※Response.End以降、ファイル削除処理ができないため、一旦ファイルの内容を
            '上記、Bufferに保存してから削除し、ダウンロード処理を行う。

            If FilePath <> "" Then
                If System.IO.File.Exists(FilePath) = True Then
                    System.IO.File.Delete(FilePath)
                End If
            End If

            'ダウンロード処理
            'Response情報クリア
            Response.ClearContent()

            'バッファリング
            Response.Buffer = True

            'HTTPヘッダー情報・MIMEタイプ設定
            Response.AddHeader("Content-Disposition", String.Format("attachment;filename={0}", dlFileName))
            Response.ContentType = MimeType

            'ファイルを書き出し
            Response.BinaryWrite(Buffer)
            'Response.WriteFile(FilePath)
            Response.Flush()
            Response.End()

        Catch ex As System.Threading.ThreadAbortException
            'Response.End()の呼び出しによりエラーメッセージを出力しないようにする

        Catch ex As Exception
            If FilePath <> "" Then
                If System.IO.File.Exists(FilePath) = True Then
                    System.IO.File.Delete(FilePath)
                End If
            End If
            Call showMsg("Failed to download ", "")
        End Try

    End Sub

    Protected Sub btnToday_Click(sender As Object, e As EventArgs) Handles btnToday.Click

        Dim clsSetCommon As New Class_common
        Dim dtNow As DateTime = clsSetCommon.dtIndia

        TextRepair_Received_Date.Text = dtNow.ToShortDateString

    End Sub

End Class