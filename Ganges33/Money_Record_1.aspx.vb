Imports System.Data.SqlClient
Public Class Money_Record_1
    Inherits System.Web.UI.Page
    Private csvData()() As String
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        '初期表示
        If IsPostBack = False Then

            '***セッションなしログインユーザの対応***
            Dim userid As String = Session("user_id")
            If userid = "" Then
                Response.Redirect("Login.aspx")
            End If

            lblDate.Visible = False
            lblName.Visible = False
            btnSend.Enabled = False

        Else

            Dim BtnCancelChk As String = ""
            Dim BtnOKChk As String = ""

            For Each s In Context.Request.Form.AllKeys
                If s.Contains("BtnCancel") Then
                    BtnCancelChk = "BtnCancelOn"
                    Exit For
                End If
            Next s

            For Each s In Context.Request.Form.AllKeys
                If s.Contains("BtnOK") Then
                    BtnOKChk = "BtnOKOn"
                    Exit For
                End If
            Next s

            '***上書き登録のダイアログでキャンセルボタン押下処理***
            If (BtnCancelChk = "BtnCancelOn") Then
                Call showMsg("Process canceled.", "")
                Call reSetSession()
            End If

            '***上書き登録のダイアログでOKボタン押下処理***
            If (BtnOKChk = "BtnOKOn") Then

                Dim clsSet As New Class_money

                'セッション情報取得
                Dim userid As String = Session("user_id")
                Dim shipCode As String = Session("ship_code")
                Dim userLevel As String = Session("user_level")
                Dim poM As String = Session("po_M")
                Dim poNoMax As Long = Session("poNo_Max")

                '画面からの入力情報
                Dim addData As Class_money.ADD_DATA
                If Session("add_Data") IsNot Nothing Then
                    addData = Session("add_Data")
                End If

                'データ登録
                Dim errFlg As Integer
                Dim errMsg As String = ""
                Dim tourokuFlg As Integer = -1

                Call clsSet.setCsvData(csvData, errFlg, addData, Nothing, userid, shipCode, poM, poNoMax, "", userLevel, tourokuFlg)

                If errFlg = 1 Then
                    Call reSetSession()
                    Call showMsg("Data registration failed.", "")
                    Exit Sub
                Else
                    '***PDF出力処理***
                    Call clsSet.outPDF(addData, Nothing, errMsg, errFlg)
                    If errMsg <> "" Then
                        Call reSetSession()
                        Call showMsg(errMsg, "")
                        Exit Sub
                    Else
                        Call showMsg("Data registration and PDF output are completed.", "")
                        Call reSetSession()
                    End If
                End If

            End If

        End If

    End Sub
    'Startボタン押下処理
    Protected Sub btnStart_Click(sender As Object, e As ImageClickEventArgs) Handles btnStart.Click

        '***セッション情報取得***
        Dim userid As String = Session("user_id")
        Dim shipCode As String = Session("ship_code")

        If Session("user_id") Is Nothing Then
            Call showMsg("The session is clear. Please login again.", "")
            Exit Sub
        End If

        '***リスト等の初期化****
        DropListCounter.Items.Clear()
        DropListRepair.Items.Clear()
        DropListDenomination.Items.Clear()

        reSet()

        '***ユーザ情報取得  時間とログイン名を表示****
        Dim dsUser As New DataSet
        Dim dsUser2 As New DataSet
        Dim dsUser3 As New DataSet
        Dim errFlg As Integer
        Dim sqlStr As String = ""

        sqlStr = "SELECT * FROM dbo.M_USER_data WHERE DELFG = 0 AND user_id = '" & userid & "'"
        dsUser = DBCommon.Get_DS(sqlStr, errFlg)

        If errFlg = 1 Then
            Call showMsg("Failed to acquire user information.", "")
            Exit Sub
        End If

        If dsUser Is Nothing Then
            Call showMsg("The user doesn't have permission to access. Cancelled the processing", "")
            Exit Sub
        Else

            Dim dr As DataRow = dsUser.Tables(0).Rows(0)

            If dr("surname") IsNot DBNull.Value Then
                lblName.Visible = True
                'ログイン名表示
                lblYousername.Text = dr("surname")
            End If

        End If

        '時間を表示
        Dim dtNow As DateTime = DateTime.Now
        lblDate.Visible = True
        lblRecord.Text = dtNow.ToString("yyyyMMddHHmmss")

        '***poの管理番号が入力されているか確認***
        Dim poNo As String = Trim(textPo.Text)
        If poNo = "" Then

            '***カウンター受付担当者をリストにセット***
            Call setListCounter(shipCode, "")

            '***修理受付担当者をリストにセット***
            Call setListRepair(shipCode, "")

            '***金種名称をリストにセット***
            With DropListDenomination
                .Items.Add("select Denomination")
                .Items.Add("cash")
                .Items.Add("card")
                .Items.Add("no claim")
                .Items.Add("resept only")
            End With

        Else
            '***管理番号に紐づく情報を表示***
            Dim dsT_repair1 As New DataSet
            Dim clsSet As New Class_money

            sqlStr = "SELECT * FROM dbo.T_repair1 WHERE DELFG = 0 AND Branch_Code = '" & shipCode & "' AND po_no = '" & poNo & "' AND asc_c_num <> '' ;"
            dsT_repair1 = DBCommon.Get_DS(sqlStr, errFlg)

            If errFlg = 1 Then
                Call showMsg("Failed to acquire po information.", "")
                Exit Sub
            End If

            If dsT_repair1 IsNot Nothing Then

                If dsT_repair1.Tables(0).Rows.Count = 1 Then

                    Dim dr As DataRow = dsT_repair1.Tables(0).Rows(0)

                    If dr("rec_datetime") IsNot DBNull.Value Then
                        Dim recDatetime As DateTime
                        recDatetime = dr("rec_datetime")
                        textReceptYMD.Text = recDatetime.ToShortDateString
                        textReceptH.Text = Replace((Left(recDatetime.ToShortTimeString, 2)), ":", "")
                        textReceptM.Text = Replace((Right(recDatetime.ToShortTimeString, 2)), ":", "")
                    End If

                    If dr("close_datetime") IsNot DBNull.Value Then
                        Dim closeDatetime As DateTime
                        closeDatetime = dr("close_datetime")
                        textCloseYMD.Text = closeDatetime.ToShortDateString
                        textCloseH.Text = Replace((Left(closeDatetime.ToShortTimeString, 2)), ":", "")
                        textCloseM.Text = Replace((Right(closeDatetime.ToShortTimeString, 2)), ":", "")
                    End If

                    If dr("rpt_counter") IsNot DBNull.Value Then
                        Dim rptCounter As String = ""
                        rptCounter = dr("rpt_counter")

                        'DBの設定内容を一番最初に表示
                        DropListCounter.Items.Add(rptCounter)

                        'リストに追加して選択可能にする
                        Call setListCounter(shipCode, rptCounter)
                    End If

                    If dr("rpt_repair") IsNot DBNull.Value Then
                        Dim rptRepair As String = ""
                        rptRepair = dr("rpt_repair")

                        'DBの設定内容を一番最初に表示
                        DropListRepair.Items.Add(rptRepair)

                        'リストに追加して選択可能にする
                        Call setListRepair(shipCode, rptRepair)
                    End If

                    If dr("denomi") IsNot DBNull.Value Then
                        Dim denomi As String = ""
                        denomi = dr("denomi")
                        With DropListDenomination
                            .Items.Add(denomi)
                            If denomi <> "cash" Then
                                .Items.Add("cash")
                            End If
                            If denomi <> "card" Then
                                .Items.Add("card")
                            End If
                            If denomi <> "no claim" Then
                                .Items.Add("no claim")
                            End If
                            If denomi <> "resept only" Then
                                .Items.Add("resept only")
                            End If
                        End With
                    End If

                    If dr("amount") IsNot DBNull.Value Then
                        Dim amount As String = ""
                        amount = dr("amount")
                        textAmount.Text = clsSet.setINR(amount)
                    End If

                    If dr("comment") IsNot DBNull.Value Then
                        TextComment.Text = dr("comment")
                    End If

                    If dr("asc_c_num") IsNot DBNull.Value Then
                        textASCClaimNo.Text = dr("asc_c_num")
                    End If

                    If dr("sam_c_num") IsNot DBNull.Value Then
                        textSamsungClaimNo.Text = dr("sam_c_num")
                    End If

                End If

            End If

        End If

        '***コントロール制御***
        btnSend.Enabled = True

    End Sub

    Protected Sub showMsg(ByVal Msg As String, ByVal msgChk As String)

        lblMsg.Text = Msg
        Dim sScript As String

        If msgChk = "CancelMsg" Then
            'OKとキャンセルボタン
            sScript = "$(function () {$(""#dialog"" ).dialog({width: 400,buttons:{""OK"": function () {$(this).dialog('close');$('[id$=""BtnOK""]').click();},""CANCEL"": function () {$(this).dialog('close');$('[id$=""BtnCancel""]').click();}}});});"
        Else
            'OKボタンのみ
            sScript = "$(function () { $( ""#dialog"" ).dialog({width: 400, buttons: {""OK"": function () {$(this).dialog('close');}}});});"
        End If

        'JavaScriptの埋め込み
        ClientScript.RegisterStartupScript(Me.GetType(), "startup", sScript, True)

    End Sub

    Protected Sub btnCsv_Click(sender As Object, e As ImageClickEventArgs) Handles btnCsv.Click

        'ファイルがアップロードされていれば
        If FileUploadGSPN.HasFile = True Then

            Try
                '***インポートファイル(CSV)情報の取得***
                Dim FileName As String
                Dim clsSet As New Class_money
                Dim errFlg As Integer
                Dim csvChk As String = "True"
                'サーバ側で保存するパス
                Dim savePass As String = "C:\money\CSV\"

                'CSVファイルの種類
                Dim kindCsv As String

                If CheckGSPNImport.Checked = True Then
                    kindCsv = "GSPNData"
                Else
                    kindCsv = "INPUTData"
                End If

                '項目数
                Dim colLen As Integer

                'ファイル名取得 
                FileName = FileUploadGSPN.FileName

                'サーバの指定パスに保存
                FileUploadGSPN.SaveAs(savePass & FileName)

                If FileName <> "" Then

                    'CSVデータ取得
                    csvData = clsSet.getCsvData(savePass & FileName, colLen, errFlg, csvChk, kindCsv)

                    If System.IO.File.Exists(clsSet.saveCsvPass & FileName) = True Then
                        System.IO.File.Delete(clsSet.saveCsvPass & FileName)
                    End If

                    If errFlg = 1 Then
                        Call showMsg("Failed to acquire the import file.", "")
                        Exit Sub
                    End If

                    If csvData Is Nothing Then
                        Call showMsg("There was no data of the import file.", "")
                        Exit Sub
                    Else
                        Call showMsg("Acquisition of the import file is completed.", "")
                        btnSend.Enabled = True
                    End If

                    If CheckGSPNImport.Checked = True Then
                        If colLen <> clsSet.GSPNCol Then
                            Call showMsg("It is not a file for GSPN.", "")
                            Exit Sub
                        End If
                    Else
                        If colLen <> clsSet.InputCol Then
                            Call showMsg("It is not a file for input data.", "")
                            Exit Sub
                        End If
                    End If

                    If csvChk = "False" Then
                        Call showMsg("Importing can not be performed because the header information of the specified file is invalid.", "")
                        Exit Sub
                    End If

                    Session("csv_Data") = csvData
                    Session("kind_CSV") = kindCsv

                End If

            Catch ex As Exception
                Call reSetSession()
                Call showMsg("Data Import Error Please retry.", "")
            End Try

        Else
            Call showMsg("Please specify the import file.", "")
        End If

    End Sub
    'Sendボタン押下処理
    Protected Sub btnSend_Click(sender As Object, e As ImageClickEventArgs) Handles btnSend.Click

        '***セッション情報取得***
        If Session("csv_Data") IsNot Nothing Then
            csvData = Session("csv_Data")
        End If

        Dim userid As String = Session("user_id")
        Dim shipCode As String = Session("ship_code")
        Dim kindCsv As String = Session("kind_CSV")
        Dim userLevel As String = Session("user_level")

        If userid Is Nothing Then
            Call showMsg("The session has expired. Please login again.", "")
            Exit Sub
        End If

        Dim clsSet As New Class_money

        '画面からの入力情報
        Dim addData As Class_money.ADD_DATA

        '追加情報（CSVよりインポート）
        Dim addDataPdf() As Class_money.ADD_DATA
        Dim errFlg As Integer
        Dim errFlgDt As Integer
        Dim errMsg As String = ""

        '***メーカ情報取得***
        Dim poM As String = ""
        Dim strSQL As String = "SELECT * FROM dbo.M_ship_base WHERE DELFG = 0 AND ship_code = '" & shipCode & "';"
        Dim DT_M_ship_base As DataTable = DBCommon.ExecuteGetDT(strSQL, errFlgDt)

        If errFlgDt = 1 Then
            Call reSet()
            Call reSetSession()
            Call showMsg("Failed to acquire manufacturer information.", "")
            Exit Sub
        End If

        If DT_M_ship_base IsNot Nothing Then
            If DT_M_ship_base.Rows(0)("PO_no") IsNot DBNull.Value Then
                poM = DT_M_ship_base.Rows(0)("PO_no")
            End If
        End If

        '***po最終番号取得***
        Dim po As String = ""
        Dim poMax As String = ""
        Dim poNoMax As Long
        Dim strSQL2 As String = "SELECT MAX(RIGHT(po_no,8)) AS po_no_max FROM dbo.T_repair1;"
        Dim DT_T_repair1 As DataTable = DBCommon.ExecuteGetDT(strSQL2, errFlgDt)

        If errFlgDt = 1 Then
            Call reSet()
            Call reSetSession()
            Call showMsg("Failed to acquire po information.", "")
            Exit Sub
        End If

        If DT_T_repair1 IsNot Nothing Then
            If DT_T_repair1.Rows(0)("po_no_max") IsNot DBNull.Value Then
                poMax = DT_T_repair1.Rows(0)("po_no_max")
                poNoMax = Convert.ToInt64(poMax)
                poNoMax = poNoMax + 1
            End If
        End If

        '***入力画面からの登録処理***
        Dim tourokuFlg As Integer = -1
        If Session("csv_Data") Is Nothing Then

            '日付のチェック
            Dim ReceptYMD As String = Trim(textReceptYMD.Text)
            Dim CloseYMD As String = Trim(textCloseYMD.Text)
            Call chkDate(ReceptYMD, CloseYMD, errMsg)
            If errMsg <> "" Then
                Call reSetSession()
                Call showMsg(errMsg, "")
                Exit Sub
            End If

            'ClaimNoのチェック
            Dim SamsungClaimNo As String = Trim(textSamsungClaimNo.Text)
            Dim ASCClaimNo As String = Trim(textASCClaimNo.Text)

            If SamsungClaimNo = "" Then
                Call reSetSession()
                Call showMsg("Please enter Samsung Claim No.", "")
                Exit Sub
            End If

            If ASCClaimNo = "" Then
                Call reSetSession()
                Call showMsg("Please enter ASC Claim No.", "")
                Exit Sub
            End If

            '入力値設定
            Dim amount As String
            If Trim(textAmount.Text) = "" Then
                amount = "0"
            Else
                amount = Trim(textAmount.Text)
            End If

            Dim comment As String = Trim(TextComment.Text)
            Dim rptCounter As String
            Dim rptRepair As String
            Dim denomi As String

            If DropListCounter.Text = "select Counter Receptionist" Then
                rptCounter = ""
            Else
                rptCounter = DropListCounter.Text
            End If

            If DropListRepair.Text = "select RepairReceptionist" Then
                rptRepair = ""
            Else
                rptRepair = DropListRepair.Text
            End If

            If DropListDenomination.Text = "select Denomination" Then
                denomi = ""
            Else
                denomi = DropListDenomination.Text
            End If

            '追加データを構造体にセット
            addData.rec_datetime = ReceptYMD
            addData.close_datetime = CloseYMD
            addData.rec_yuser = userid
            addData.rpt_counter = rptCounter
            addData.rpt_repair = rptRepair
            addData.denomi = denomi
            addData.amount = amount
            addData.asc_c_num = ASCClaimNo
            addData.sam_c_num = SamsungClaimNo
            addData.comment = comment

            'Claim_Noのデータ取得
            Dim strSql3 As String = "Select top 1 * FROM dbo.T_repair1 WHERE DELFG = 0 And Branch_Code = '" & shipCode & "' AND ASC_Claim_No = '" & ASCClaimNo & "' AND Samsung_Claim_No = '" & SamsungClaimNo & "';"
            DT_T_repair1 = DBCommon.ExecuteGetDT(strSql3, errFlgDt)

            If errFlgDt = 1 Then
                Call showMsg("Failed to acquire Claim No data of T_repair 1.", "")
                Call reSetSession()
                Exit Sub
            End If

            If DT_T_repair1 IsNot Nothing Then

                Dim poNo As String = ""
                Dim chkCompletedDate As DateTime
                Dim dtNow As DateTime = DateTime.Now

                If DT_T_repair1.Rows(0)("Completed_Date") IsNot DBNull.Value Then

                    '変更登録可能かチェック
                    If DT_T_repair1.Rows(0)("asc_c_num") IsNot DBNull.Value And (userLevel <> "1") Then
                        Call showMsg("There is description on the completion date. Change registration is not possible.", "")
                        Call reSetSession()
                        Exit Sub
                    End If

                    '管理者権限であっても、完了日が今月でない場合は、変更登録不可
                    chkCompletedDate = DT_T_repair1.Rows(0)("Completed_Date")
                    If DT_T_repair1.Rows(0)("asc_c_num") IsNot DBNull.Value And (userLevel = "1") And (Left(chkCompletedDate.ToShortDateString, 7) <> Left(dtNow.ToShortDateString, 7)) Then
                        Call showMsg("Because completion date is not this month, change registration is not possible.", "")
                        Call reSetSession()
                        Exit Sub
                    End If

                    If DT_T_repair1.Rows(0)("po_no") IsNot DBNull.Value Then
                        poNo = DT_T_repair1.Rows(0)("po_no")
                    End If

                    '上書き登録か確認 
                    If DT_T_repair1.Rows(0)("ASC_Claim_No") IsNot DBNull.Value Then

                        '登録用のセッション設定
                        Session("add_Data") = addData
                        Session("poNo_Max") = poNoMax
                        Session("po_M") = poM
                        showMsg("PO:" & poNo & "Exist. Do you want to continue?", "CancelMsg")
                        Exit Sub

                    End If

                End If

            End If

            '登録処理
            Call clsSet.setCsvData(csvData, errFlg, addData, Nothing, userid, shipCode, poM, poNoMax, kindCsv, userLevel, tourokuFlg)
            If errFlg = 1 Then
                Call showMsg("Data registration failed.", "")
                Call reSetSession()
                Exit Sub
            Else
                '***PDF出力処理***
                Call clsSet.outPDF(addData, Nothing, errMsg, errFlg)
                If errMsg <> "" Then
                    Call showMsg(errMsg, "")
                    Call reSetSession()
                    Exit Sub
                Else
                    Call showMsg("Data registration and PDF output are completed.", "")
                End If
            End If
        Else
            '***インポートからの登録処理***
            '入力用データCSVをインポート
            If kindCsv = "INPUTData" Then
                ReDim addDataPdf(csvData.Length - 1)
                Call clsSet.setCsvData(csvData, errFlg, Nothing, addDataPdf, userid, shipCode, poM, poNoMax, kindCsv, userLevel, tourokuFlg)
                If errFlg = 1 Then
                    Call showMsg("Data registration failed.", "")
                    Call reSetSession()
                    Exit Sub
                Else
                    '***PDF出力処理***
                    If tourokuFlg = 1 Then
                        Call clsSet.outPDF(addData, addDataPdf, errMsg, errFlg)
                        If errMsg <> "" Then
                            Call showMsg(errMsg, "")
                            Call reSetSession()
                            Exit Sub
                        Else
                            Call showMsg("Data registration and PDF output are completed.", "")
                        End If
                    Else
                        Call showMsg("There was no registration information.", "")
                    End If
                End If
            Else
                'GSPN用データCSVをインポート
                Call clsSet.setCsvData(csvData, errFlg, Nothing, Nothing, userid, shipCode, poM, poNoMax, kindCsv, userLevel, tourokuFlg)
                If errFlg = 1 Then
                    Call showMsg("Data registration failed.", "")
                    Call reSetSession()
                    Exit Sub
                Else
                    If tourokuFlg = 1 Then
                        Call showMsg("Data registration is complete.", "")
                    Else
                        Call showMsg("There was no registration information.", "")
                    End If
                End If
            End If
        End If

        '***後処理***
        Call reSet()
        Call reSetSession()

    End Sub
    '日付チェック
    Protected Sub chkDate(ByRef ReceptYMD As String, ByRef CloseYMD As String, ByRef errMsg As String)

        Dim ReceptH As String = textReceptH.Text
        Dim ReceptM As String = textReceptM.Text
        Dim CloseH As String = textCloseH.Text
        Dim CloseM As String = textCloseM.Text
        Dim dt As DateTime

        ReceptYMD = Trim(textReceptYMD.Text)
        CloseYMD = Trim(textCloseYMD.Text)

        If ReceptYMD = "" Then
            errMsg = "Please enter the date and time of the Recept."
            Exit Sub
        End If

        If CloseYMD = "" Then
            errMsg = "Please enter the date of Close."
            Exit Sub
        End If

        If ReceptH = "" Then
            ReceptH = "0"
        End If

        If ReceptM = "" Then
            ReceptM = "0"
        End If

        If CloseH = "" Then
            CloseH = "0"
        End If

        If CloseM = "" Then
            CloseM = "0"
        End If

        ReceptYMD = ReceptYMD & " " & ReceptH & ":" & ReceptM
        CloseYMD = CloseYMD & " " & CloseH & ":" & CloseM

        If DateTime.TryParse(ReceptYMD, dt) Then
        Else
            errMsg = "The receipt date format should be YYYY/MM/DD"
            Exit Sub
        End If

        If DateTime.TryParse(CloseYMD, dt) Then
        Else
            errMsg = "The close date format should be YYYY/MM/DD"
            Exit Sub
        End If

    End Sub

    Protected Sub reSet()

        lblDate.Visible = False
        lblName.Visible = False
        lblRecord.Text = ""
        lblYousername.Text = ""
        textReceptYMD.Text = ""
        textReceptH.Text = ""
        textReceptM.Text = ""
        textCloseYMD.Text = ""
        textCloseH.Text = ""
        textCloseM.Text = ""
        textAmount.Text = ""
        textASCClaimNo.Text = ""
        textSamsungClaimNo.Text = ""
        TextComment.Text = ""
        DropListCounter.Items.Clear()
        DropListRepair.Items.Clear()
        DropListDenomination.Items.Clear()

        btnSend.Enabled = False
        btnStart.Enabled = True

        CheckGSPNImport.Checked = False

    End Sub

    Protected Sub reSetSession()

        Session("csv_Data") = Nothing
        Session("kind_CSV") = Nothing
        Session("add_Data") = Nothing
        Session("poNo_Max") = Nothing
        Session("po_M") = Nothing

    End Sub

    'jqueryのダイアログでキャンセルボタン押下時にクリック処理される。
    '実際は使用しないボタン(ASP.net)なので非表示。
    'ダイアログでキャンセルボタン押下⇒ASP.netのキャンセルボタンクリック⇒サーバからの応答ありでロードへ⇒ロードでキャンセル処理
    Protected Sub BtnCancel_Click(sender As Object, e As EventArgs) Handles BtnCancel.Click

    End Sub

    'jqueryのダイアログでOKボタン押下時にクリック処理される。
    '※キャンセルボタンもある場合のOK処理
    '実際は使用しないボタン(ASP.net)なので非表示。
    'ダイアログでOKボタン押下⇒ASP.netのOKボタンクリック⇒サーバからの応答ありでロードへ⇒ロードでOK処理
    Protected Sub BtnOK_Click(sender As Object, e As EventArgs) Handles BtnOK.Click

    End Sub
    'カウンター受付担当者をリストにセット
    Protected Sub setListCounter(ByVal shipCode As String, ByVal surName As String)

        Dim sqlStr As String
        Dim errFlg As Integer
        Dim dsUser2 As New DataSet

        sqlStr = ""
        sqlStr &= "SELECT A.* FROM dbo.M_USER_data A "
        sqlStr &= "LEFT JOIN dbo.M_USER B "
        sqlStr &= "ON A.user_id = B.user_id "
        sqlStr &= "WHERE A.DELFG = 0 "
        sqlStr &= "AND A.position like '%4%' "
        sqlStr &= "AND ship_1 like '%" & shipCode & "%';"

        dsUser2 = DBCommon.Get_DS(sqlStr, errFlg)
        If errFlg = 1 Then
            Call showMsg("Failed to acquire the contact receptionist from the information", "")
            Exit Sub
        End If

        If surName = "" Then
            If dsUser2 Is Nothing Then
                DropListCounter.Items.Add("no data")
            Else
                'コンボボックスに拠点のカウンター受付担当者名を設定
                DropListCounter.Items.Add("selsect Counter Receptionist")
                For i = 0 To dsUser2.Tables(0).Rows.Count - 1
                    Dim dr As DataRow = dsUser2.Tables(0).Rows(i)
                    With DropListCounter
                        If dr("surname") IsNot DBNull.Value Then
                            .Items.Add(dr("surname"))
                        End If
                    End With
                Next i
            End If
        Else
            For i = 0 To dsUser2.Tables(0).Rows.Count - 1
                Dim dr As DataRow = dsUser2.Tables(0).Rows(i)
                With DropListCounter
                    If dr("surname") IsNot DBNull.Value Then
                        If surName <> dr("surname") Then
                            .Items.Add(dr("surname"))
                        End If
                    End If
                End With
            Next i
        End If

    End Sub
    '修理受付担当者をリストにセット
    Protected Sub setListRepair(ByVal shipCode As String, ByVal surName As String)

        Dim sqlStr As String
        Dim errFlg As Integer
        Dim dsUser3 As New DataSet

        sqlStr = ""
        sqlStr &= "SELECT A.* FROM dbo.M_USER_data A "
        sqlStr &= "LEFT JOIN dbo.M_USER B "
        sqlStr &= "ON A.user_id = B.user_id "
        sqlStr &= "WHERE A.DELFG = 0 "
        sqlStr &= "AND A.position like '%5%' "
        sqlStr &= "AND ship_1 like '%" & shipCode & "%';"

        dsUser3 = DBCommon.Get_DS(sqlStr, errFlg)
        If errFlg = 1 Then
            Call showMsg("Failed to acquire the repair receptionist from the site.", "")
            Exit Sub
        End If

        If surName = "" Then
            If dsUser3 Is Nothing Then
                DropListRepair.Items.Add("no data")
            Else
                'コンボボックスに拠点の修理受付担当者名を設定
                DropListRepair.Items.Add("select RepairReceptionist")
                For i = 0 To dsUser3.Tables(0).Rows.Count - 1
                    Dim dr As DataRow = dsUser3.Tables(0).Rows(i)
                    With DropListRepair
                        If dr("surname") IsNot DBNull.Value Then
                            .Items.Add(dr("surname"))
                        End If
                    End With
                Next i
            End If
        Else
            For i = 0 To dsUser3.Tables(0).Rows.Count - 1
                Dim dr As DataRow = dsUser3.Tables(0).Rows(i)
                With DropListRepair
                    If dr("surname") IsNot DBNull.Value Then
                        If surName <> dr("surname") Then
                            .Items.Add(dr("surname"))
                        End If
                    End If
                End With
            Next i
        End If

    End Sub

End Class