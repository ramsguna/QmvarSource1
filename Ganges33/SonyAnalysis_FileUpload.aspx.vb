Imports System.Globalization
Imports System.IO
Imports Ganges33.Ganges33.model
Imports Ganges33.Ganges33.logic
Public Class SonyAnalysis_FileUpload
    Inherits System.Web.UI.Page
    Private csvData()() As String
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        If IsPostBack = False Then

            '***セッションなしログインユーザの対応***
            Dim userid As String = Session("user_id")
            If userid = "" Then
                Response.Redirect("Login.aspx")
            End If

            Dim setShipname As String = Session("ship_Name")
            Dim userName As String = Session("user_Name")
            Dim userLevel As String = Session("user_level")
            Dim adminFlg As Boolean = Session("admin_Flg")

            lblLoc.Text = setShipname
            lblName.Text = userName

            ''''''''***拠点名称の設定***
            DropListLocation.Items.Clear()

            DefaultSearchSetting()

            'Default Year
            DropDownYear.SelectedValue = Now.Year
            'Default Month
            DropDownMonth.SelectedValue = DateTime.Now.AddMonths(-1).Month 'Now.Month


            '''''''If userLevel = "0" Or userLevel = "1" Or userLevel = "2" Or adminFlg = True Then
            '''''''    Dim shipName() As String
            '''''''    If Session("ship_name_list") IsNot Nothing Then
            '''''''        shipName = Session("ship_name_list")
            '''''''        With DropListLocation
            '''''''            .Items.Add("Select shipname")
            '''''''            For i = 0 To UBound(shipName)
            '''''''                If Trim(shipName(i)) <> "" Then
            '''''''                    .Items.Add(shipName(i))
            '''''''                End If
            '''''''            Next i
            '''''''        End With
            '''''''    End If
            '''''''Else
            '''''''    DropListLocation.Items.Add(setShipname)
            '''''''End If
            InitDropDownList()



            ''***アップロードファイル名の設定***
            'DropListUploadFile.Items.Clear()

            'With DropListUploadFile
            '    .Items.Add("Select upload Filename")
            '    .Items.Add("1.DailyStatementReport")
            '    .Items.Add("2.Warranty Excel File")
            '    .Items.Add("3.Invoice Update C")
            '    .Items.Add("4.Invoice Update EXC")
            '    .Items.Add("5.Good Recived")
            '    .Items.Add("6.Billing Info")
            '    .Items.Add("7.test")
            'End With

            '***ラジオボタン処理設定***
            RadioBtnDD.Checked = False
            RadioBtnMM.Checked = False

        Else

            '***セッション設定***
            ' Session("upload_Shipname") = DropListLocation.Text
            ' Session("upload_Filename") = DropListUploadFile.Text

        End If

    End Sub

    Protected Sub btnUpload_Click(sender As Object, e As ImageClickEventArgs) Handles btnUpload.Click
        '''''***セッション情報取得***
        ''''Dim ShipName As String = Session("ship_Name")
        ''''Dim shipCode As String = Session("ship_code")
        ''''Dim userName As String = Session("user_Name")
        ''''Dim userid As String = Session("user_id")
        ''''Dim uploadShipname As String = Session("upload_Shipname")
        ''''Dim uploadFilename As String = Session("upload_Filename")
        ''''Dim listHistoryMsg() As String = Session("list_History_Msg")
        '''''***入力チェック***
        ''''If ShipName = "" Then
        ''''    Call showMsg("The session was cleared. Please login again.", "")
        ''''    Exit Sub
        ''''End If
        ''''If DropListLocation.Text = "Select shipname" Then
        ''''    Call showMsg("Please specify Upload Branch.", "")
        ''''    Exit Sub
        ''''End If
        ''''If DropListUploadFile.Text = "Select upload Filename" Then
        ''''    Call showMsg("Please specify Upload File.", "")
        ''''    Exit Sub
        ''''End If
        '''''***拠点コード取得***
        ''''Dim clsSetCommon As New Class_common
        ''''Dim setShipCode As String
        ''''Dim errMsg As String
        ''''Call clsSetCommon.setShipCode(uploadShipname, setShipCode, errMsg)
        ''''If errMsg <> "" Then
        ''''    Call showMsg(errMsg, "")
        ''''    Exit Sub
        ''''End If
        '''''***CSVファイルの種類の設定***
        ''''Dim kindCsv As Integer = Left(uploadFilename, 1)
        ''''If kindCsv = 1 Or kindCsv = 5 Then
        ''''    If RadioBtnDD.Checked = False And RadioBtnMM.Checked = False Then
        ''''        Call showMsg("Please specify the date type of Invoice Date.", "")
        ''''        Exit Sub
        ''''    End If
        ''''End If
        '''''***daily種類の設定***
        '''''Invoice Dateの日付型がmから始まる：mm/dd/yyyy　又は　dから始まる：dd/mm/yyyy かを確認
        ''''Dim dailyKind As String
        ''''If RadioBtnDD.Checked = True Then
        ''''    'dから始まる
        ''''    dailyKind = "DD"
        ''''Else
        ''''    dailyKind = "MM"
        ''''End If
        '''''***ファイルがアップロードされていれば***
        ''''If FileUploadAnalysis.HasFile = True Then
        ''''    Try
        ''''        '***インポートファイル(CSV)情報の取得***
        ''''        Dim FileName As String
        ''''        Dim clsSet As New Class_analysis
        ''''        Dim invoiceData As Class_analysis.INVOICE
        ''''        Dim csvChk As String = "True"
        ''''        '項目数
        ''''        Dim colLen As Integer
        ''''        'ファイル名取得 
        ''''        FileName = FileUploadAnalysis.FileName
        ''''        If FileName <> "" Then
        ''''            Session("File_Name") = FileName
        ''''            '■***アップロード可能チェック***
        ''''            '**共通チェック**
        ''''            Dim FileNameChk() As String
        ''''            FileNameChk = Split(FileName, "_")
        ''''            '※画面からの指定拠点名とファイル名の拠点が一致するか
        ''''            If FileNameChk.Length <> 1 Then
        ''''                Dim tmp As String = FileNameChk(0)
        ''''                If Left(tmp, 3) <> "SSC" Then
        ''''                    Call showMsg("File name does not begin with SSC. please confirm.", "")
        ''''                    Exit Sub
        ''''                End If
        ''''                If tmp <> uploadShipname Then
        ''''                    Call showMsg("You can not upload at the specified location.", "")
        ''''                    Exit Sub
        ''''                End If
        ''''            Else
        ''''                Call showMsg("Can not upload. Please check the file name.", "")
        ''''                Exit Sub
        ''''            End If
        ''''            '**3,4のケース**
        ''''            '入力チェックと設定
        ''''            If kindCsv = 3 Or kindCsv = 4 Then
        ''''                Dim dt As DateTime
        ''''                If TextPartsInvoiceNo.Text = "" Then
        ''''                    Call showMsg("Please enter Parts InvoiceNo.", "")
        ''''                    Exit Sub
        ''''                Else
        ''''                    invoiceData.Parts_invoice_No = Trim(TextPartsInvoiceNo.Text)
        ''''                End If
        ''''                If TextLaborInvoiceNo.Text = "" Then
        ''''                    Call showMsg("Please enter Labor InvoiceNo.", "")
        ''''                    Exit Sub
        ''''                Else
        ''''                    invoiceData.Labor_Invoice_No = Trim(TextLaborInvoiceNo.Text)
        ''''                End If
        ''''                If TextInvoiceDate.Text <> "" Then
        ''''                    If DateTime.TryParse(TextInvoiceDate.Text, dt) Then
        ''''                        invoiceData.Invoice_date = DateTime.Parse(Trim(TextInvoiceDate.Text))
        ''''                    Else
        ''''                        Call showMsg("The date specification of InvoiceDate is incorrect.", "")
        ''''                        Exit Sub
        ''''                    End If
        ''''                Else
        ''''                    Call showMsg("Please enter Invoice Date.", "")
        ''''                    Exit Sub
        ''''                End If
        ''''                If kindCsv = 3 Then
        ''''                    invoiceData.number = "C"
        ''''                Else
        ''''                    invoiceData.number = "EXC"
        ''''                End If
        ''''            End If
        ''''            'サーバの指定パスに保存
        ''''            FileUploadAnalysis.SaveAs(clsSet.savePass & FileName)
        ''''            '■***CSVデータ取得***
        ''''            Dim errFlg As Integer
        ''''            If kindCsv <> 7 Then
        ''''                csvData = clsSet.getCsvData(clsSet.savePass & FileName, colLen, errFlg, csvChk, kindCsv)
        ''''            Else
        ''''                csvData = clsSet.getCsvData2(clsSet.savePass & FileName, colLen, errFlg, csvChk, kindCsv)
        ''''            End If
        ''''            If System.IO.File.Exists(clsSet.savePass & FileName) = True Then
        ''''                System.IO.File.Delete(clsSet.savePass & FileName)
        ''''            End If
        ''''            If errFlg = 1 Then
        ''''                Call showMsg("Failed to acquire the import file.", "")
        ''''                Exit Sub
        ''''            End If
        ''''            If csvData Is Nothing Then
        ''''                Call showMsg("There was no data of the import file.", "")
        ''''                Exit Sub
        ''''            End If
        ''''            If csvChk = "False" Then
        ''''                Call showMsg("Importing can not be performed because the header information of the specified file is invalid.", "")
        ''''                Exit Sub
        ''''            End If
        ''''            '■***CSVデータの登録***
        ''''            Dim dtNow As DateTime = clsSetCommon.dtIndia
        ''''            Dim tmpHHMM As String = Replace(dtNow.ToString("HH:mm"), ":", "")
        ''''            Dim tmpYYYYMMDD As String = Replace(dtNow.ToShortDateString, "/", "")
        ''''            Dim setFileName As String = FileName & "_" & tmpYYYYMMDD & "_" & tmpHHMM
        ''''            Select Case kindCsv
        ''''                Case 1
        ''''                    '■■DailyReport
        ''''                    '■***ファイル内容チェック***
        ''''                    '日付変換(ddmmyyyy⇒mmddyyyy)してから日付型のチェック
        ''''                    '□BillingDate
        ''''                    If csvData(2)(3) = "" Then
        ''''                        Call showMsg("Not found the BillingDate. please confirm the file.", "")
        ''''                        Exit Sub
        ''''                    End If
        ''''                    Dim BillingDateStr As String = csvData(2)(3)
        ''''                    If BillingDateStr.Length <> 10 Then
        ''''                        Call showMsg("The date type ((dd.mm.yyyy) or (mm.dd.yyyy)) of BillingDate is incorrect. please confirm the file.", "")
        ''''                        Exit Sub
        ''''                    End If

        ''''                    If RadioBtnDD.Checked = True Then
        ''''                        Dim tmpMon As String = BillingDateStr.Substring(3, 2)
        ''''                        Dim tmpDay As String = BillingDateStr.Substring(0, 2)
        ''''                        Dim tmpYear As String = BillingDateStr.Substring(6, 4)
        ''''                        BillingDateStr = tmpMon & "." & tmpDay & "." & tmpYear
        ''''                    End If

        ''''                    Dim BillingDate As DateTime
        ''''                    If DateTime.TryParse(BillingDateStr, BillingDate) Then
        ''''                        BillingDate = BillingDateStr
        ''''                    Else
        ''''                        Call showMsg("The date type of BillingDate is incorrect. please confirm the file.", "")
        ''''                        Exit Sub
        ''''                    End If

        ''''                    '□GoodsDeliveredDate
        ''''                    Dim GoodsDeliveredDateStr As String = csvData(2)(4)

        ''''                    If GoodsDeliveredDateStr <> "" Then
        ''''                        If GoodsDeliveredDateStr.Length <> 10 Then
        ''''                            Call showMsg("The description of Goods Delivered Date is not 10 digits. please confirm the file.", "")
        ''''                            Exit Sub
        ''''                        End If
        ''''                    Else
        ''''                        Call showMsg("There is no mention of Goods Delivered Date. please confirm the file.", "")
        ''''                        Exit Sub
        ''''                    End If

        ''''                    '■***登録***
        ''''                    Dim billingDateAll() As String
        ''''                    Call clsSet.setCsvData(csvData, userid, userName, uploadShipname, setShipCode, errFlg, dailyKind, billingDateAll, setFileName)

        ''''                    If errFlg = 1 Then
        ''''                        Call showMsg("Failed to register the data of the import file.", "")
        ''''                        Exit Sub
        ''''                    Else
        ''''                        Session("billingDate_All") = billingDateAll
        ''''                        Call showMsg("Data registration of the import file is completed.", "")

        ''''                    End If

        ''''                Case 2
        ''''                    '■■WtyExcelDown

        ''''                    Dim tourokuFlg As Integer
        ''''                    Call clsSet.setCsvDataWtyExcelDown(csvData, userid, userName, errFlg, tourokuFlg, uploadShipname, setShipCode, setFileName)

        ''''                    If errFlg = 1 Then
        ''''                        Call showMsg("Failed to register the data of the import file.", "")
        ''''                    Else
        ''''                        If tourokuFlg = 1 Then
        ''''                            Call showMsg("Data registration of the import file is completed.", "")
        ''''                        Else
        ''''                            Call showMsg("There was no subject to register.", "")
        ''''                        End If
        ''''                    End If

        ''''                Case 3, 4
        ''''                    '■■WtyListbyReportNo

        ''''                    Call clsSet.setCsvInvoiceUpdate(csvData, userid, userName, errFlg, uploadShipname, invoiceData, setFileName)

        ''''                    If errFlg = 1 Then
        ''''                        Call showMsg("Failed to register the data of the import file.", "")
        ''''                    Else
        ''''                        Call showMsg("Data registration of the import file is completed.", "")
        ''''                    End If

        ''''                Case 5
        ''''                    '■■good_recived
        ''''                    '日付変換(ddmmyyyy⇒mmddyyyy)してから日付型のチェック
        ''''                    '□delivery_date
        ''''                    If csvData(2)(2) = "" Then
        ''''                        Call showMsg("Not found the delivery date. please confirm", "")
        ''''                        Exit Sub
        ''''                    End If

        ''''                    Dim deliveryDateStr As String = csvData(2)(2)

        ''''                    If deliveryDateStr.Length <> 10 Then
        ''''                        Call showMsg("The date type of delivery_date ((dd.mm.yyyy) or (mm.dd.yyyy)) is incorrect. please confirm the file.", "")
        ''''                        Exit Sub
        ''''                    End If

        ''''                    If RadioBtnDD.Checked = True Then
        ''''                        Dim tmpMon As String = deliveryDateStr.Substring(3, 2)
        ''''                        Dim tmpDay As String = deliveryDateStr.Substring(0, 2)
        ''''                        Dim tmpYear As String = deliveryDateStr.Substring(6, 4)
        ''''                        deliveryDateStr = tmpMon & "." & tmpDay & "." & tmpYear
        ''''                    End If

        ''''                    Dim deliveryDate As DateTime
        ''''                    If DateTime.TryParse(deliveryDateStr, deliveryDate) Then
        ''''                        deliveryDate = deliveryDateStr
        ''''                    Else
        ''''                        Call showMsg("The date type of Billing Date is incorrect. please confirm the file.", "")
        ''''                        Exit Sub
        ''''                    End If

        ''''                    Dim deliveryDateAll() As String
        ''''                    Call clsSet.setCsvGoodRecived(csvData, userid, userName, errFlg, uploadShipname, dailyKind, deliveryDateAll, setFileName)

        ''''                    If errFlg = 1 Then
        ''''                        Call showMsg("Failed to register the data of the import file.", "")
        ''''                    Else
        ''''                        Session("deliveryDate_All") = deliveryDateAll
        ''''                        Call showMsg("Data registration of the import file is completed.", "")
        ''''                    End If

        ''''                Case 6
        ''''                    '■■Billing_Info

        ''''                    '■***ファイル内容チェック***
        ''''                    '□BillingDate
        ''''                    If csvData(1)(2) = "" Then
        ''''                        Call showMsg("Not found the Billing Date. please confirm the file.", "")
        ''''                        Exit Sub
        ''''                    End If

        ''''                    Dim BillingDateStr As String = csvData(1)(2)

        ''''                    If BillingDateStr.Length <> 8 Then
        ''''                        Call showMsg("The date type (yyyymmdd) of Billing Date is incorrect. please confirm the file.", "")
        ''''                        Exit Sub
        ''''                    End If

        ''''                    Dim tmpMon As String = BillingDateStr.Substring(4, 2)
        ''''                    Dim tmpDay As String = BillingDateStr.Substring(6, 2)
        ''''                    Dim tmpYear As String = BillingDateStr.Substring(0, 4)

        ''''                    BillingDateStr = tmpYear & "/" & tmpMon & "/" & tmpDay

        ''''                    Dim BillingDate As DateTime

        ''''                    If DateTime.TryParse(BillingDateStr, BillingDate) Then
        ''''                    Else
        ''''                        Call showMsg("The date type (yyyymmdd) of Billing Date is incorrect. please confirm the file.", "")
        ''''                        Exit Sub
        ''''                    End If

        ''''                    '■***登録***
        ''''                    Call clsSet.setCsvBillingInfo(csvData, userid, userName, errFlg, uploadShipname, setFileName)

        ''''                    If errFlg = 1 Then
        ''''                        Call showMsg("Failed to register the data of the import file.", "")
        ''''                    Else
        ''''                        Call showMsg("Data registration of the import file is completed.", "")
        ''''                    End If

        ''''            End Select

        ''''        End If

        ''''    Catch ex As Exception
        ''''        Call showMsg("Data Import Error Please retry.", "")
        ''''    End Try

        ''''Else
        ''''    Call showMsg("Please specify the import file.", "")
        ''''End If

        ''''Dim ShipName As String = Session("ship_Name")
        ''''Dim shipCode As String = Session("ship_code")
        ''''Dim userName As String = Session("user_Name")
        ''''Dim userid As String = Session("user_id")
        ''''Dim uploadShipname As String = Session("upload_Shipname")
        ''''Dim uploadFilename As String = Session("upload_Filename")

        '***********************************************
        'New
        '**********************************************
        '***セッション情報取得***
        Dim ShipName As String = Session("ship_Name")
        Dim shipCode As String = Session("ship_code")
        Dim userName As String = Session("user_Name")
        Dim userid As String = Session("user_id")
        Dim uploadShipname As String = DropListLocation.SelectedItem.Text
        Dim kindCsv As Integer

        'Dim uploadShipname As String = DropListLocation.Text
        'Comment and Modified on 20190801
        Dim uploadFilename As String = ""
        If drpTask.SelectedValue = "-3" Then 'Assign 3 hence modified as 3A & 3B 
            kindCsv = 3
            uploadFilename = 3
        Else
            uploadFilename = drpTask.Text
        End If
        ''''Dim uploadFilename As String = drpTask.Text
        Dim listHistoryMsg() As String = Session("list_History_Msg")

        '***入力チェック***
        If ShipName = "" Then
            Call showMsg("The session was cleared. Please login again.", "")
            Exit Sub
        End If

        If DropListLocation.Text = "Select Branch" Then
            Call showMsg("Please specify Upload Branch.", "")
            Exit Sub
        End If

        If drpTask.SelectedValue = "0" Then
            Call showMsg("Please specify the file type", "")
            Exit Sub
        End If
        '**************************************************************
        '
        'Task No.1, 2, 3, 4
        '
        '***********************************************
        If (drpTask.SelectedValue = "1") Or (drpTask.SelectedValue = "2") Or (drpTask.SelectedValue = "3") Or (drpTask.SelectedValue = "-3") Or (drpTask.SelectedValue = "4") Then
            '***拠点コード取得***
            Dim clsSetCommon As New Class_common
            Dim setShipCode As String
            Dim errMsg As String
            Call clsSetCommon.setShipCode(uploadShipname, setShipCode, errMsg)
            If errMsg <> "" Then
                Call showMsg(errMsg, "")
                Exit Sub
            End If
            '***CSVファイルの種類の設定***
            'Dim kindCsv As Integer
            'The following Code has changed due to keep the old Functionality 
            '    .Items.Add("Select upload Filename")
            '    .Items.Add("1.DailyStatementReport")
            '    .Items.Add("2.Warranty Excel File")
            '    .Items.Add("3.Invoice Update C")
            '    .Items.Add("4.Invoice Update EXC")
            '    .Items.Add("5.Good Recived")  - Changed 8th Place
            '    .Items.Add("6.Billing Info")   - Not Known
            '    .Items.Add("7.test")

            If drpTask.SelectedValue = "8" Then 'Assign 5 
                kindCsv = 5
            Else
                kindCsv = Left(uploadFilename, 1)
            End If



            If kindCsv = 1 Or kindCsv = 5 Then
                If RadioBtnDD.Checked = False And RadioBtnMM.Checked = False Then
                    Call showMsg("Please specify the date type of Invoice Date.", "")
                    Exit Sub
                End If
            End If
            '***daily種類の設定***
            'Invoice Dateの日付型がmから始まる：mm/dd/yyyy　又は　dから始まる：dd/mm/yyyy かを確認
            Dim dailyKind As String
            If RadioBtnDD.Checked = True Then
                'dから始まる
                dailyKind = "DD"
            Else
                dailyKind = "MM"
            End If
            '***ファイルがアップロードされていれば***
            If FileUploadAnalysis.HasFile = True Then
                Try
                    '***インポートファイル(CSV)情報の取得***
                    Dim FileName As String
                    Dim clsSet As New Class_analysis
                    Dim invoiceData As Class_analysis.INVOICE
                    Dim csvChk As String = "True"
                    '項目数
                    Dim colLen As Integer
                    'ファイル名取得 
                    FileName = FileUploadAnalysis.FileName
                    If FileName <> "" Then
                        Session("File_Name") = FileName

                        '■***アップロード可能チェック***

                        '**共通チェック**
                        Dim FileNameChk() As String
                        FileNameChk = Split(FileName, "_")

                        'Comment on 20190801 modified as per new request
                        If (kindCsv = "3" Or kindCsv = "4") Then

                            'File Name validation 
                            Select Case Left(drpTask.SelectedItem.Text, 2)
                                Case "3A"
                                    invoiceData.number = "C"
                                    If Len(FileName) > 2 Then
                                        If Not (UCase(Left(FileName, 1)) = "C") Then
                                            Call showMsg("The csv file name is Invalid", "")
                                            Exit Sub
                                        End If
                                    Else
                                        Call showMsg("The csv file name is Invalid", "")
                                        Exit Sub
                                    End If
                                Case "3B"
                                    invoiceData.number = "EXC"
                                    If Len(FileName) > 4 Then
                                        If Not (UCase(Left(FileName, 3)) = "EXC") Then
                                            Call showMsg("The csv file name is Invalid", "")
                                            Exit Sub
                                        End If
                                    Else
                                        Call showMsg("The csv file name is Invalid", "")
                                        Exit Sub
                                    End If
                                Case "4."
                                    invoiceData.number = "OWC"
                                    If Len(FileName) > 4 Then
                                        If Not (UCase(Left(FileName, 3)) = "OWC") Then
                                            Call showMsg("The csv file name is Invalid", "")
                                            Exit Sub
                                        End If
                                    Else
                                        Call showMsg("The csv file name is Invalid", "")
                                        Exit Sub
                                    End If
                            End Select

                            'Verify TextPartsInvoiceNo and TextLaborInvoiceNo must start with SSC
                            'For TextPartsInvoiceNo
                            'Comment  on 20190830
                            ''''''''If Len(TextPartsInvoiceNo.Text) > 5 Then
                            ''''''''    If Not (UCase(Left(TextPartsInvoiceNo.Text, 3)) = "SSC") Then
                            ''''''''        Call showMsg("The parts invoice no is Invalid", "")
                            ''''''''        Exit Sub
                            ''''''''    End If
                            ''''''''Else
                            ''''''''    Call showMsg("The parts invoice no is Invalid", "")
                            ''''''''    Exit Sub
                            ''''''''End If
                            '''''''''For TextLaborInvoiceNo
                            ''''''''If Len(TextLaborInvoiceNo.Text) > 5 Then
                            ''''''''    If Not (UCase(Left(TextLaborInvoiceNo.Text, 3)) = "SSC") Then
                            ''''''''        Call showMsg("The labour invoice no is Invalid", "")
                            ''''''''        Exit Sub
                            ''''''''    End If
                            ''''''''Else
                            ''''''''    Call showMsg("The labour invoice no is Invalid", "")
                            ''''''''    Exit Sub
                            ''''''''End If



                            '''''''''''''''''''''''Verify Update Labor Invoice No. start with 19-20 until next March
                            ''''''''''''''''''''''Dim dt As DateTime
                            ''''''''''''''''''''''If TextInvoiceDate.Text <> "" Then
                            ''''''''''''''''''''''        If DateTime.TryParse(TextInvoiceDate.Text, dt) Then
                            ''''''''''''''''''''''            If Trim(TextLaborInvoiceNo.Text) = "" Then
                            ''''''''''''''''''''''                Call showMsg("The labour invoice no is Invalid", "")
                            ''''''''''''''''''''''                Exit Sub
                            ''''''''''''''''''''''            Else
                            ''''''''''''''''''''''                Dim strAccountYear As String = ""
                            ''''''''''''''''''''''                If Len(TextLaborInvoiceNo.Text) > 4 Then
                            ''''''''''''''''''''''                    'Verify the current account year
                            ''''''''''''''''''''''                    strAccountYear = Left(TextLaborInvoiceNo.Text, 5)
                            ''''''''''''''''''''''                    'DateConversion
                            ''''''''''''''''''''''                    Dim DtConv() As String
                            ''''''''''''''''''''''                    DtConv = Split(TextInvoiceDate.Text, "/")
                            ''''''''''''''''''''''                    If (Len(DtConv(0)) > 0) And (Len(DtConv(2)) = 4) Then
                            ''''''''''''''''''''''                        Dim intYear As Integer = 0
                            ''''''''''''''''''''''                        Dim strYear As String = ""
                            ''''''''''''''''''''''                        intYear = Right(TextInvoiceDate.Text, 2)
                            ''''''''''''''''''''''                        strYear = intYear & "-" & intYear + 1
                            ''''''''''''''''''''''                        If Not (strAccountYear = strYear) Then
                            ''''''''''''''''''''''                            Call showMsg("The labour invoice no is Invalid", "")
                            ''''''''''''''''''''''                            Exit Sub
                            ''''''''''''''''''''''                        End If
                            ''''''''''''''''''''''                    End If

                            ''''''''''''''''''''''                Else
                            ''''''''''''''''''''''                    Call showMsg("The labour invoice no is Invalid", "")
                            ''''''''''''''''''''''                    Exit Sub
                            ''''''''''''''''''''''                End If
                            ''''''''''''''''''''''            End If

                            ''''''''''''''''''''''        Else
                            ''''''''''''''''''''''            Call showMsg("The date specification of InvoiceDate is incorrect.", "")
                            ''''''''''''''''''''''            Exit Sub
                            ''''''''''''''''''''''        End If
                            ''''''''''''''''''''''    Else
                            ''''''''''''''''''''''        Call showMsg("Please enter Invoice Date.", "")
                            ''''''''''''''''''''''        Exit Sub
                            ''''''''''''''''''''''    End If

                        Else 'Other than 3A, 3B & 4
                            '※画面からの指定拠点名とファイル名の拠点が一致するか
                            If FileNameChk.Length <> 1 Then
                                Dim tmp As String = FileNameChk(0)
                                If Left(tmp, 3) <> "SSC" Then
                                    Call showMsg("File name does not begin with SSC. please confirm.", "")
                                    Exit Sub
                                End If
                                If tmp <> uploadShipname Then
                                    Call showMsg("You can not upload at the specified location.", "")
                                    Exit Sub
                                End If
                            Else
                                Call showMsg("Can not upload. Please check the file name.", "")
                                Exit Sub
                            End If
                        End If

                        '**3,4のケース**
                        '入力チェックと設定
                        If kindCsv = 3 Or kindCsv = 4 Then

                            Dim dt As DateTime

                            If TextPartsInvoiceNo.Text = "" Then
                                Call showMsg("Please enter Parts InvoiceNo.", "")
                                Exit Sub
                            Else
                                invoiceData.Parts_invoice_No = Trim(TextPartsInvoiceNo.Text)
                            End If

                            If TextLaborInvoiceNo.Text = "" Then
                                Call showMsg("Please enter Labor InvoiceNo.", "")
                                Exit Sub
                            Else
                                invoiceData.Labor_Invoice_No = Trim(TextLaborInvoiceNo.Text)
                            End If

                            If TextInvoiceDate.Text <> "" Then
                                If DateTime.TryParse(TextInvoiceDate.Text, dt) Then
                                    invoiceData.Invoice_date = DateTime.Parse(Trim(TextInvoiceDate.Text))
                                Else
                                    Call showMsg("The date specification of InvoiceDate is incorrect.", "")
                                    Exit Sub
                                End If
                            Else
                                Call showMsg("Please enter Invoice Date.", "")
                                Exit Sub
                            End If

                            'Comment on 20190801 as per new request 
                            '''''''If kindCsv = 3 Then
                            '''''''    invoiceData.number = "C"
                            '''''''Else
                            '''''''    invoiceData.number = "EXC"
                            '''''''End If
                            ''''''''''''''''''Select Case Left(drpTask.SelectedItem.Text, 2)
                            ''''''''''''''''''    Case "3A"
                            ''''''''''''''''''        invoiceData.number = "C"
                            ''''''''''''''''''    Case "3B"
                            ''''''''''''''''''        invoiceData.number = "EXC"
                            ''''''''''''''''''    Case "4."
                            ''''''''''''''''''        invoiceData.number = "OOW"
                            ''''''''''''''''''End Select


                        End If


                        'サーバの指定パスに保存
                        FileUploadAnalysis.SaveAs(clsSet.savePass & FileName)

                        '■***CSVデータ取得***
                        Dim errFlg As Integer

                        If kindCsv <> 7 Then
                            csvData = clsSet.getCsvData(clsSet.savePass & FileName, colLen, errFlg, csvChk, kindCsv)
                        Else
                            csvData = clsSet.getCsvData2(clsSet.savePass & FileName, colLen, errFlg, csvChk, kindCsv)
                        End If

                        If System.IO.File.Exists(clsSet.savePass & FileName) = True Then
                            System.IO.File.Delete(clsSet.savePass & FileName)
                        End If

                        If errFlg = 1 Then
                            Call showMsg("Failed to acquire the import file.", "")
                            Exit Sub
                        End If

                        If csvData Is Nothing Then
                            Call showMsg("There was no data of the import file.", "")
                            Exit Sub
                        End If

                        If csvChk = "False" Then
                            Call showMsg("Importing can not be performed because the header information of the specified file is invalid.", "")
                            Exit Sub
                        End If

                        '■***CSVデータの登録***
                        Dim dtNow As DateTime = clsSetCommon.dtIndia
                        Dim tmpHHMM As String = Replace(dtNow.ToString("HH:mm"), ":", "")
                        Dim tmpYYYYMMDD As String = Replace(dtNow.ToShortDateString, "/", "")

                        Dim setFileName As String = FileName & "_" & tmpYYYYMMDD & "_" & tmpHHMM

                        Select Case kindCsv

                            Case 1
                                '■■DailyReport

                                '■***ファイル内容チェック***
                                '日付変換(ddmmyyyy⇒mmddyyyy)してから日付型のチェック
                                '□BillingDate
                                If csvData(2)(3) = "" Then
                                    Call showMsg("Not found the BillingDate. please confirm the file.", "")
                                    Exit Sub
                                End If

                                Dim BillingDateStr As String = csvData(2)(3)

                                If BillingDateStr.Length <> 10 Then
                                    Call showMsg("The date type ((dd.mm.yyyy) or (mm.dd.yyyy)) of BillingDate is incorrect. please confirm the file.", "")
                                    Exit Sub
                                End If

                                If RadioBtnDD.Checked = True Then
                                    Dim tmpMon As String = BillingDateStr.Substring(3, 2)
                                    Dim tmpDay As String = BillingDateStr.Substring(0, 2)
                                    Dim tmpYear As String = BillingDateStr.Substring(6, 4)
                                    BillingDateStr = tmpMon & "." & tmpDay & "." & tmpYear
                                End If

                                Dim BillingDate As DateTime
                                If DateTime.TryParse(BillingDateStr, BillingDate) Then
                                    BillingDate = BillingDateStr
                                Else
                                    Call showMsg("The date type of BillingDate is incorrect. please confirm the file.", "")
                                    Exit Sub
                                End If

                                '□GoodsDeliveredDate
                                Dim GoodsDeliveredDateStr As String = csvData(2)(4)

                                If GoodsDeliveredDateStr <> "" Then
                                    If GoodsDeliveredDateStr.Length <> 10 Then
                                        Call showMsg("The description of Goods Delivered Date is not 10 digits. please confirm the file.", "")
                                        Exit Sub
                                    End If
                                Else
                                    Call showMsg("There is no mention of Goods Delivered Date. please confirm the file.", "")
                                    Exit Sub
                                End If

                                '■***登録***
                                Dim billingDateAll() As String
                                Call clsSet.setCsvData(csvData, userid, userName, uploadShipname, setShipCode, errFlg, dailyKind, billingDateAll, setFileName)

                                If errFlg = 1 Then
                                    Call showMsg("Failed to register the data of the import file.", "")
                                    Exit Sub
                                Else
                                    Session("billingDate_All") = billingDateAll
                                    Call showMsg("Data registration of the import file is completed.", "")

                                End If

                            Case 2
                                '■■WtyExcelDown

                                Dim tourokuFlg As Integer
                                Call clsSet.setCsvDataWtyExcelDown(csvData, userid, userName, errFlg, tourokuFlg, uploadShipname, setShipCode, setFileName)

                                If errFlg = 1 Then
                                    Call showMsg("Failed to register the data of the import file.", "")
                                Else
                                    If tourokuFlg = 1 Then
                                        Call showMsg("Data registration of the import file is completed.", "")
                                    Else
                                        Call showMsg("There was no subject to register.", "")
                                    End If
                                End If

                            Case 3, 4
                                '■■WtyListbyReportNo

                                Call clsSet.setCsvInvoiceUpdate(csvData, userid, userName, errFlg, uploadShipname, invoiceData, setFileName)

                                If errFlg = 1 Then
                                    'Comment on 20190801 
                                    '''Call showMsg("Failed to register the data of the import file.", "")
                                    '''
                                    Call showMsg("Status: Upload Failed <br><br>  Please check the content of file ", "")
                                    Exit Sub
                                Else
                                    'Comment on 20190801 
                                    '''''Call showMsg("Data registration of the import file is completed.", "")
                                    '''
                                    Call showMsg("Status: Upload Success <br><br> The file has been uploaded", "")
                                    Exit Sub
                                End If

                            Case 5
                                '■■good_recived
                                '日付変換(ddmmyyyy⇒mmddyyyy)してから日付型のチェック
                                '□delivery_date
                                If csvData(2)(2) = "" Then
                                    Call showMsg("Not found the delivery date. please confirm", "")
                                    Exit Sub
                                End If

                                Dim deliveryDateStr As String = csvData(2)(2)

                                If deliveryDateStr.Length <> 10 Then
                                    Call showMsg("The date type of delivery_date ((dd.mm.yyyy) or (mm.dd.yyyy)) is incorrect. please confirm the file.", "")
                                    Exit Sub
                                End If

                                If RadioBtnDD.Checked = True Then
                                    Dim tmpMon As String = deliveryDateStr.Substring(3, 2)
                                    Dim tmpDay As String = deliveryDateStr.Substring(0, 2)
                                    Dim tmpYear As String = deliveryDateStr.Substring(6, 4)
                                    deliveryDateStr = tmpMon & "." & tmpDay & "." & tmpYear
                                End If

                                Dim deliveryDate As DateTime
                                If DateTime.TryParse(deliveryDateStr, deliveryDate) Then
                                    deliveryDate = deliveryDateStr
                                Else
                                    Call showMsg("The date type of Billing Date is incorrect. please confirm the file.", "")
                                    Exit Sub
                                End If

                                Dim deliveryDateAll() As String
                                Call clsSet.setCsvGoodRecived(csvData, userid, userName, errFlg, uploadShipname, dailyKind, deliveryDateAll, setFileName)

                                If errFlg = 1 Then
                                    Call showMsg("Failed to register the data of the import file.", "")
                                Else
                                    Session("deliveryDate_All") = deliveryDateAll
                                    Call showMsg("Data registration of the import file is completed.", "")
                                End If

                            Case 6
                                '■■Billing_Info

                                '■***ファイル内容チェック***
                                '□BillingDate
                                If csvData(1)(2) = "" Then
                                    Call showMsg("Not found the Billing Date. please confirm the file.", "")
                                    Exit Sub
                                End If

                                Dim BillingDateStr As String = csvData(1)(2)

                                If BillingDateStr.Length <> 8 Then
                                    Call showMsg("The date type (yyyymmdd) of Billing Date is incorrect. please confirm the file.", "")
                                    Exit Sub
                                End If

                                Dim tmpMon As String = BillingDateStr.Substring(4, 2)
                                Dim tmpDay As String = BillingDateStr.Substring(6, 2)
                                Dim tmpYear As String = BillingDateStr.Substring(0, 4)

                                BillingDateStr = tmpYear & "/" & tmpMon & "/" & tmpDay

                                Dim BillingDate As DateTime

                                If DateTime.TryParse(BillingDateStr, BillingDate) Then
                                Else
                                    Call showMsg("The date type (yyyymmdd) of Billing Date is incorrect. please confirm the file.", "")
                                    Exit Sub
                                End If

                                '■***登録***
                                Call clsSet.setCsvBillingInfo(csvData, userid, userName, errFlg, uploadShipname, setFileName)

                                If errFlg = 1 Then
                                    'Comment on 20190801 
                                    '''Call showMsg("Failed to register the data of the import file.", "")
                                    Call showMsg("Status: Upload Failed <br><br>  Please check the content of file ", "")
                                    Exit Sub
                                Else
                                    'Commment on 2090801
                                    'Call showMsg("Data registration of the import file is completed.", "")
                                    Call showMsg("Status: Upload Success <br><br> The file has been uploaded", "")
                                    Exit Sub
                                End If
                        End Select

                    End If

                Catch ex As Exception
                    Call showMsg("Data Import Error Please retry.", "")
                End Try

            Else
                Call showMsg("Please specify the import file.", "")
            End If
            '**************************************************************
            '
            'Task No.5, 6,7,8,9,10,11,12,13,14,15,16
            '
            '***********************************************
        Else
            'Task No.5, 6,7,8,9,10,11,12,13,14,15,16
            If (drpTask.SelectedValue = "12") Or (drpTask.SelectedValue = "13") Then
                Call showMsg("Coming Soon...", "")
                Exit Sub
            End If

            'If File not uploaded then show the error msg to the user
            '24 - no need to file upload
            If Not drpTask.SelectedValue = "24" Then
                If Not FileUploadAnalysis.HasFile Then
                    Call showMsg("File Not Uploaded!!!", "")
                    Exit Sub
                End If
            End If
            'Find CSV file 
            Dim strExtension As String = System.IO.Path.GetExtension(FileUploadAnalysis.FileName)
            '24 - no need to file upload
            If Not drpTask.SelectedValue = "24" Then
                If Not (strExtension = ".csv") Then
                    Call showMsg("Uploaded file must be CSV", "")
                    Exit Sub
                End If
            End If

            'Service Center Start with
            Dim PositionStart As Integer = 0
            Dim strBranchName As String = ""
            Dim strTmp As String = ""

            If drpTask.SelectedValue = "5" Then
                'Comment on 20190730 as per new request
                '''''*********************************
                '''''Item5 Invoice Update Other
                '''''*********************************
                '''''Verify FileName Rules Followed
                ''''Dim strFileName As String = System.IO.Path.GetFileNameWithoutExtension(FileUploadAnalysis.FileName)
                ''''Dim strSrcFileName As String = System.IO.Path.GetFileName(FileUploadAnalysis.FileName)
                '''''Verify that file name must more than 6 characters
                ''''If Len(strFileName) < 10 Then
                ''''    Call showMsg("The csv file name is Invalid", "")
                ''''    Exit Sub
                ''''End If
                '''''Split the branch name in File Name
                ''''PositionStart = InStr(1, strSrcFileName, "_")
                ''''If PositionStart = 0 Then ' There is no _ symbol in the file name
                ''''    Call showMsg("The csv file name is Invalid", "")
                ''''    Exit Sub
                ''''Else
                ''''    strBranchName = Left(strSrcFileName, PositionStart - 1)
                ''''    If Not (UCase(DropListLocation.SelectedItem.Text) = UCase(strBranchName)) Then
                ''''        Call showMsg("The selected branch and file name is not matched", "")
                ''''        Exit Sub
                ''''    End If
                ''''End If
                '''''Middle ID in file name
                ''''PositionStart = InStr(1, strSrcFileName, "_")
                ''''If PositionStart = 0 Then ' There is no _ symbol in the file name
                ''''    Call showMsg("The csv file name is invalid ", "")
                ''''    Exit Sub
                ''''Else
                ''''    strTmp = Right(strSrcFileName, Len(strSrcFileName) - PositionStart)
                ''''    strTmp = Left(strTmp, 2)
                ''''    If (UCase(strTmp) = "OU") Then
                ''''    Else
                ''''        Call showMsg("The csv file name is invalid", "")
                ''''        Exit Sub
                ''''    End If
                ''''End If

                ''''Dim strMonth As String = Right(strFileName, 2)
                ''''Dim strYear As String = Mid(strFileName, Len(strFileName) - 5, 4)
                '''''Verify integer
                ''''Dim numChk As Integer = 0
                ''''If Integer.TryParse(strMonth, numChk) = False Then
                ''''    Call showMsg("The csv file name is Invalid", "")
                ''''    Exit Sub
                ''''End If
                '''''Maximum Allowed 12 Months
                ''''If numChk > 12 Then
                ''''    Call showMsg("The csv file name is Invalid", "")
                ''''    Exit Sub
                ''''End If
                ''''If Integer.TryParse(strYear, numChk) = False Then
                ''''    Call showMsg("The csv file name is Invalid", "")
                ''''    Exit Sub
                ''''End If
                '''''Allowed Greater than 2018
                ''''If numChk < 2018 Then
                ''''    Call showMsg("The csv file name is Invalid", "")
                ''''    Exit Sub
                ''''End If
                '''''Move the file to temporary folder
                ''''If Not Directory.Exists(ConfigurationManager.AppSettings("RootDir") & "\" & DropListLocation.Text & "\" & userName & "\" & "upload") Then
                ''''    Directory.CreateDirectory(ConfigurationManager.AppSettings("RootDir") & "\" & DropListLocation.Text & "\" & userName & "\" & "upload")
                ''''End If
                '''''Assign File Name
                ''''Dim dirPath As String = ConfigurationManager.AppSettings("RootDir") & "\" & DropListLocation.Text & "\" & userName & "\" & "upload"
                ''''Dim fileName As String = strFileName & "_" & DateTime.Now.ToString("yyyyMMddHHmmssfff") & strExtension
                ''''Dim filenameFullPath As String = dirPath & "\" & fileName
                '''''Save the file in temporary folder
                ''''FileUploadAnalysis.SaveAs(filenameFullPath)
                '''''Update to Database
                ''''Item5(filenameFullPath, fileName, userid, userName, strSrcFileName)
                '*********************************
                'Item5 Debit Note
                '*********************************
                'Verify FileName Rules Followed
                Dim strFileName As String = System.IO.Path.GetFileNameWithoutExtension(FileUploadAnalysis.FileName)
                Dim strSrcFileName As String = System.IO.Path.GetFileName(FileUploadAnalysis.FileName)

                If Len(strSrcFileName) > 2 Then
                    If Not (UCase(Left(strSrcFileName, 1)) = "C") Then
                        Call showMsg("The csv file name is Invalid", "")
                        Exit Sub
                    End If
                Else
                    Call showMsg("The csv file name is Invalid", "")
                    Exit Sub
                End If

                'Verify Update Labor Invoice No. start with 19-20 until next March
                Dim dt As DateTime
                If TextInvoiceDate.Text <> "" Then
                    If DateTime.TryParse(TextInvoiceDate.Text, dt) Then
                        If Trim(TextLaborInvoiceNo.Text) = "" Then
                            Call showMsg("The labour invoice no is Invalid", "")
                            Exit Sub
                        Else
                            Dim strAccountYear As String = ""
                            If Len(TextLaborInvoiceNo.Text) > 4 Then
                                'Verify the current account year
                                strAccountYear = Left(TextLaborInvoiceNo.Text, 5)
                                'DateConversion
                                Dim DtConv() As String
                                DtConv = Split(TextInvoiceDate.Text, "/")
                                If (Len(DtConv(0)) > 0) And (Len(DtConv(2)) = 4) Then
                                    Dim intYear As Integer = 0
                                    Dim strYear As String = ""
                                    intYear = Right(TextInvoiceDate.Text, 2)
                                    strYear = intYear & "-" & intYear + 1
                                    If Not (strAccountYear = strYear) Then
                                        Call showMsg("The labour invoice no is Invalid", "")
                                        Exit Sub
                                    End If
                                End If

                            Else
                                Call showMsg("The labour invoice no is Invalid", "")
                                Exit Sub
                            End If
                        End If

                    Else
                        Call showMsg("The date specification of InvoiceDate is incorrect.", "")
                        Exit Sub
                    End If
                Else
                    Call showMsg("Please enter Invoice Date.", "")
                    Exit Sub
                End If



                Dim ReportNo As String = ""
                If Len(strSrcFileName) > 4 Then
                    ReportNo = Left(strSrcFileName, Len(strSrcFileName) - 4)
                End If


                'Move the file to temporary folder
                If Not Directory.Exists(ConfigurationManager.AppSettings("RootDir") & "\" & DropListLocation.Text & "\" & userName & "\" & "upload") Then
                    Directory.CreateDirectory(ConfigurationManager.AppSettings("RootDir") & "\" & DropListLocation.Text & "\" & userName & "\" & "upload")
                End If
                'Assign File Name
                Dim dirPath As String = ConfigurationManager.AppSettings("RootDir") & "\" & DropListLocation.Text & "\" & userName & "\" & "upload"
                Dim fileName As String = strFileName & "_" & DateTime.Now.ToString("yyyyMMddHHmmssfff") & strExtension
                Dim filenameFullPath As String = dirPath & "\" & fileName
                'Save the file in temporary folder
                FileUploadAnalysis.SaveAs(filenameFullPath)
                'Comment on 20190730 as per new request
                '''''Update to Database
                ''''Item5(filenameFullPath, fileName, userid, userName, strSrcFileName)
                'Update to Database
                Item5_1(filenameFullPath, fileName, userid, userName, strSrcFileName, ReportNo)
            ElseIf drpTask.SelectedValue = "6" Then
                '*********************************
                'Item6 Purchase Register Summary
                '*********************************
                'Verify FileName Rules Followed
                Dim strFileName As String = System.IO.Path.GetFileNameWithoutExtension(FileUploadAnalysis.FileName)
                Dim strSrcFileName As String = System.IO.Path.GetFileName(FileUploadAnalysis.FileName)
                'Verify that file name must more than 6 characters
                If Len(strFileName) < 13 Then
                    Call showMsg("The csv file name is invalid ", "")
                    Exit Sub
                End If
                'Split the branch name in File Name
                PositionStart = InStr(1, strSrcFileName, "_")
                If PositionStart = 0 Then ' There is no _ symbol in the file name
                    Call showMsg("The csv file name is Invalid", "")
                    Exit Sub
                Else
                    strBranchName = Left(strSrcFileName, PositionStart - 1)
                    If Not (UCase(DropListLocation.SelectedItem.Text) = UCase(strBranchName)) Then
                        Call showMsg("The selected branch and file name is not matched", "")
                        Exit Sub
                    End If
                End If
                'Middle ID in file name
                PositionStart = InStr(1, strSrcFileName, "_")
                If PositionStart = 0 Then ' There is no _ symbol in the file name
                    Call showMsg("The csv file name is invalid ", "")
                    Exit Sub
                Else
                    strTmp = Right(strSrcFileName, Len(strSrcFileName) - PositionStart)
                    strTmp = Left(strTmp, 2)
                    If (UCase(strTmp) = "SM") Then
                    Else
                        Call showMsg("The csv file name is invalid", "")
                        Exit Sub
                    End If
                End If


                Dim strDay As String = Right(strFileName, 2)
                Dim strMonth As String = Mid(strFileName, Len(strFileName) - 3, 2)
                Dim strYear As String = Mid(strFileName, Len(strFileName) - 7, 4)

                'Verify integer
                Dim numChk As Integer = 0
                If Integer.TryParse(strDay, numChk) = False Then
                    Call showMsg("The csv file name is invalid ", "")
                    Exit Sub
                End If
                'Allowed 01/11/21 
                If Not (numChk = 1 Or numChk = 11 Or numChk = 21) Then
                    Call showMsg("The csv file name is invalid ", "")
                    Exit Sub
                End If

                If Integer.TryParse(strMonth, numChk) = False Then
                    Call showMsg("The csv file name is invalid ", "")
                    Exit Sub
                End If
                'Maximum Allowed 12 Months
                If numChk > 12 Then
                    Call showMsg("The csv file name is invalid ", "")
                    Exit Sub
                End If
                If Integer.TryParse(strYear, numChk) = False Then
                    Call showMsg("The csv file name is invalid ", "")
                    Exit Sub
                End If
                'Allowed Greater than 2018
                If numChk < 2018 Then
                    Call showMsg("The csv file name is invalid ", "")
                    Exit Sub
                End If
                'Move the file to temporary folder
                If Not Directory.Exists(ConfigurationManager.AppSettings("RootDir") & "\" & DropListLocation.Text & "\" & userName & "\" & "upload") Then
                    Directory.CreateDirectory(ConfigurationManager.AppSettings("RootDir") & "\" & DropListLocation.Text & "\" & userName & "\" & "upload")
                End If
                'Assign File Name
                Dim dirPath As String = ConfigurationManager.AppSettings("RootDir") & "\" & DropListLocation.Text & "\" & userName & "\" & "upload"
                Dim fileName As String = strFileName & "_" & DateTime.Now.ToString("yyyyMMddHHmmssfff") & strExtension
                Dim filenameFullPath As String = dirPath & "\" & fileName
                'Save the file in temporary folder
                FileUploadAnalysis.SaveAs(filenameFullPath)
                'Update to Database
                Item6(filenameFullPath, fileName, userid, userName, strSrcFileName)
            ElseIf drpTask.SelectedValue = "7" Then
                '*********************************
                'Item7 Purchase Register Detail
                '*********************************
                'Verify FileName Rules Followed
                Dim strFileName As String = System.IO.Path.GetFileNameWithoutExtension(FileUploadAnalysis.FileName)
                Dim strSrcFileName As String = System.IO.Path.GetFileName(FileUploadAnalysis.FileName)

                'Verify that file name must more than 6 characters
                If Len(strFileName) < 15 Then
                    Call showMsg("The csv file name is invalid ", "")
                    Exit Sub
                End If

                PositionStart = InStr(1, strSrcFileName, "_")
                If PositionStart = 0 Then ' There is no _ symbol in the file name
                    Call showMsg("The csv file name is invalid ", "")
                    Exit Sub
                Else
                    strBranchName = Left(strSrcFileName, PositionStart - 1)
                    If Not (UCase(DropListLocation.SelectedItem.Text) = UCase(strBranchName)) Then
                        Call showMsg("The selected branch and file name is not matched", "")
                        Exit Sub
                    End If
                End If

                PositionStart = InStr(1, strSrcFileName, "-")
                If PositionStart = 0 Then ' There is no _ symbol in the file name
                    Call showMsg("The csv file name is invalid ", "")
                    Exit Sub
                Else
                    strTmp = Left(strSrcFileName, PositionStart - 1)
                    strTmp = Right(strTmp, 3)
                    If (UCase(strTmp) = "DT1") Or (UCase(strTmp) = "DT2") Then
                    Else
                        Call showMsg("The csv file name is invalid", "")
                        Exit Sub
                    End If
                End If

                Dim strDay As String = Right(strFileName, 2)
                Dim strMonth As String = Mid(strFileName, Len(strFileName) - 3, 2)
                Dim strYear As String = Mid(strFileName, Len(strFileName) - 7, 4)

                'Verify integer
                Dim numChk As Integer = 0
                If Integer.TryParse(strDay, numChk) = False Then
                    Call showMsg("The csv file name is invalid ", "")
                    Exit Sub
                End If
                'Allowed 01/11/21 
                If Not (numChk = 1 Or numChk = 11 Or numChk = 21) Then
                    Call showMsg("The csv file name is invalid ", "")
                    Exit Sub
                End If

                If Integer.TryParse(strMonth, numChk) = False Then
                    Call showMsg("The csv file name is invalid ", "")
                    Exit Sub
                End If
                'Maximum Allowed 12 Months
                If numChk > 12 Then
                    Call showMsg("The csv file name is invalid ", "")
                    Exit Sub
                End If
                If Integer.TryParse(strYear, numChk) = False Then
                    Call showMsg("The csv file name is invalid ", "")
                    Exit Sub
                End If
                'Allowed Greater than 2018
                If numChk < 2018 Then
                    Call showMsg("The csv file name is invalid ", "")
                    Exit Sub
                End If
                'Move the file to temporary folder
                If Not Directory.Exists(ConfigurationManager.AppSettings("RootDir") & "\" & DropListLocation.Text & "\" & userName & "\" & "upload") Then
                    Directory.CreateDirectory(ConfigurationManager.AppSettings("RootDir") & "\" & DropListLocation.Text & "\" & userName & "\" & "upload")
                End If
                'Assign File Name
                Dim dirPath As String = ConfigurationManager.AppSettings("RootDir") & "\" & DropListLocation.Text & "\" & userName & "\" & "upload"
                Dim fileName As String = strFileName & "_" & DateTime.Now.ToString("yyyyMMddHHmmssfff") & strExtension
                Dim filenameFullPath As String = dirPath & "\" & fileName
                'Save the file in temporary folder
                FileUploadAnalysis.SaveAs(filenameFullPath)
                'Update to Database
                Item7(filenameFullPath, fileName, userid, userName, strSrcFileName)
            ElseIf drpTask.SelectedValue = "8" Then
                '*********************************
                'Item8 Good Receiving
                '*********************************
                'Verify FileName Rules Followed
                Dim strFileName As String = System.IO.Path.GetFileNameWithoutExtension(FileUploadAnalysis.FileName)
                Dim strSrcFileName As String = System.IO.Path.GetFileName(FileUploadAnalysis.FileName)


                'Added 20191114
                'File name format has been changed from YYYYMM to YYYYMMDD
                PositionStart = InStr(1, strSrcFileName, "_")
                Dim strFileFmt1 As String = ""
                Dim strFileFmt2 As String = ""
                If PositionStart = 0 Then ' There is no _ symbol in the file name
                    Call showMsg("The csv file name is invalid", "")
                    Exit Sub
                Else
                    strFileFmt1 = Right(strSrcFileName, Len(strSrcFileName) - PositionStart)
                    strFileFmt2 = Mid(strFileFmt1, 3, Len(strFileFmt1) - 6)
                End If
                'Decide to exuecute validation of old or new functionality
                If Len(strFileFmt2) = 8 Then 'YYYYMMDD ==>New Fileformat Valdidation
                    '*************************************************
                    'New File Name Format Validation
                    'Begin
                    'Verify that file name must more than 6 characters
                    If Len(strFileName) < 13 Then
                        Call showMsg("The csv file name is invalid", "")
                        Exit Sub
                    End If

                    'Split the branch name in File Name
                    PositionStart = InStr(1, strSrcFileName, "_")
                    If PositionStart = 0 Then ' There is no _ symbol in the file name
                        Call showMsg("The csv file name is invalid", "")
                        Exit Sub
                    Else
                        strBranchName = Left(strSrcFileName, PositionStart - 1)
                        If Not (UCase(DropListLocation.SelectedItem.Text) = UCase(strBranchName)) Then
                            Call showMsg("The selected branch and file name is not matched", "")
                            Exit Sub
                        End If
                    End If
                    'Middle ID in file name
                    PositionStart = InStr(1, strSrcFileName, "_")
                    If PositionStart = 0 Then ' There is no _ symbol in the file name
                        Call showMsg("The csv file name is invalid ", "")
                        Exit Sub
                    Else
                        strTmp = Right(strSrcFileName, Len(strSrcFileName) - PositionStart)
                        strTmp = Left(strTmp, 2)
                        'Modified on 20190724
                        If (UCase(strTmp) = "GR") Or (UCase(strTmp) = "GD") Then
                        Else
                            Call showMsg("The csv file name is invalid", "")
                            Exit Sub
                        End If
                    End If


                    Dim strDay As String = Right(strFileName, 2)
                    Dim strMonth As String = Mid(strFileName, Len(strFileName) - 3, 2)
                    Dim strYear As String = Mid(strFileName, Len(strFileName) - 7, 4)

                    'Verify integer
                    Dim numChk As Integer = 0
                    If Integer.TryParse(strDay, numChk) = False Then
                        Call showMsg("The csv file name is invalid", "")
                        Exit Sub
                    End If
                    'Allowed 01/11/21 
                    If Not (numChk < 32) Then
                        Call showMsg("The csv file name is invalid", "")
                        Exit Sub
                    End If

                    If Integer.TryParse(strMonth, numChk) = False Then
                        Call showMsg("The csv file name is invalid", "")
                        Exit Sub
                    End If
                    'Maximum Allowed 12 Months
                    If numChk > 12 Then
                        Call showMsg("The csv file name is invalid", "")
                        Exit Sub
                    End If
                    If Integer.TryParse(strYear, numChk) = False Then
                        Call showMsg("The csv file name is invalid", "")
                        Exit Sub
                    End If
                    'Allowed Greater than 2018
                    If numChk < 2018 Then
                        Call showMsg("The csv file name is invalid", "")
                        Exit Sub
                    End If
                    'End  New Filename Format
                    '***************************************************


                Else  'YYYYMM ==>Old File name format validation
                    '*************************************************
                    'Old File Name Format Validation
                    'Begin
                    'Verify that file name must more than 6 characters
                    If Len(strFileName) < 10 Then
                        Call showMsg("The csv file name is invalid", "")
                        Exit Sub
                    End If

                    'Split the branch name in File Name
                    PositionStart = InStr(1, strSrcFileName, "_")
                    If PositionStart = 0 Then ' There is no _ symbol in the file name
                        Call showMsg("The csv file name is invalid", "")
                        Exit Sub
                    Else
                        strBranchName = Left(strSrcFileName, PositionStart - 1)
                        If Not (UCase(DropListLocation.SelectedItem.Text) = UCase(strBranchName)) Then
                            Call showMsg("The selected branch and file name is not matched", "")
                            Exit Sub
                        End If
                    End If
                    'Middle ID in file name
                    PositionStart = InStr(1, strSrcFileName, "_")
                    If PositionStart = 0 Then ' There is no _ symbol in the file name
                        Call showMsg("The csv file name is invalid ", "")
                        Exit Sub
                    Else
                        strTmp = Right(strSrcFileName, Len(strSrcFileName) - PositionStart)
                        strTmp = Left(strTmp, 2)
                        'Modified on 20190724
                        If (UCase(strTmp) = "GR") Or (UCase(strTmp) = "GD") Then
                        Else
                            Call showMsg("The csv file name is invalid", "")
                            Exit Sub
                        End If
                    End If


                    'Validate Year and Month - Maximum Length 6
                    'PositionStart = InStr(1, strSrcFileName, "_") + 2 THEN YYYYMM
                    If (PositionStart + 2 + 6) <> (Len(strSrcFileName) - 4) Then
                        Call showMsg("The csv file name is invalid ", "")
                        Exit Sub
                    End If

                    Dim strMonth As String = Right(strFileName, 2)
                    Dim strYear As String = Mid(strFileName, Len(strFileName) - 5, 4)
                    'Verify integer
                    Dim numChk As Integer = 0
                    If Integer.TryParse(strMonth, numChk) = False Then
                        Call showMsg("The csv file name is invalid", "")
                        Exit Sub
                    End If
                    'Maximum Allowed 12 Months
                    If numChk > 12 Then
                        Call showMsg("The csv file name is invalid", "")
                        Exit Sub
                    End If
                    If Integer.TryParse(strYear, numChk) = False Then
                        Call showMsg("The csv file name is invalid", "")
                        Exit Sub
                    End If
                    'Allowed Greater than 2018
                    If numChk < 2018 Then
                        Call showMsg("The csv file name is invalid", "")
                        Exit Sub
                    End If
                    'End  Old Filename Format
                    '***************************************************
                End If






                'Move the file to temporary folder
                If Not Directory.Exists(ConfigurationManager.AppSettings("RootDir") & "\" & DropListLocation.Text & "\" & userName & "\" & "upload") Then
                    Directory.CreateDirectory(ConfigurationManager.AppSettings("RootDir") & "\" & DropListLocation.Text & "\" & userName & "\" & "upload")
                End If
                'Assign File Name
                Dim dirPath As String = ConfigurationManager.AppSettings("RootDir") & "\" & DropListLocation.Text & "\" & userName & "\" & "upload"
                Dim fileName As String = strFileName & "_" & DateTime.Now.ToString("yyyyMMddHHmmssfff") & strExtension
                Dim filenameFullPath As String = dirPath & "\" & fileName
                'Save the file in temporary folder
                FileUploadAnalysis.SaveAs(filenameFullPath)
                'Update to Database
                If (UCase(strTmp) = "GR") Then
                    Item8(filenameFullPath, fileName, userid, userName, strSrcFileName)
                ElseIf (UCase(strTmp) = "GD") Then
                    Item8_1(filenameFullPath, fileName, userid, userName, strSrcFileName)
                End If

            ElseIf drpTask.SelectedValue = "9" Then
                '*********************************
                'Item9 Stock OverView
                '*********************************
                'Verify FileName Rules Followed
                Dim strFileName As String = System.IO.Path.GetFileNameWithoutExtension(FileUploadAnalysis.FileName)
                Dim strSrcFileName As String = System.IO.Path.GetFileName(FileUploadAnalysis.FileName)

                'Verify that file name must more than 6 characters
                If Len(strFileName) < 10 Then
                    Call showMsg("CSV File Name is Invalid in End of (yyyymm)", "")
                    Exit Sub
                End If

                'Split the branch name in File Name
                PositionStart = InStr(1, strSrcFileName, "_")
                If PositionStart = 0 Then ' There is no _ symbol in the file name
                    Call showMsg("The csv file name is invalid", "")
                    Exit Sub
                Else
                    strBranchName = Left(strSrcFileName, PositionStart - 1)
                    If Not (UCase(DropListLocation.SelectedItem.Text) = UCase(strBranchName)) Then
                        Call showMsg("The selected branch and file name is not matched", "")
                        Exit Sub
                    End If
                End If
                'Middle ID in file name
                PositionStart = InStr(1, strSrcFileName, "_")
                If PositionStart = 0 Then ' There is no _ symbol in the file name
                    Call showMsg("The csv file name is invalid ", "")
                    Exit Sub
                Else
                    strTmp = Right(strSrcFileName, Len(strSrcFileName) - PositionStart)
                    strTmp = Left(strTmp, 2)
                    If (UCase(strTmp) = "SV") Then
                    Else
                        Call showMsg("The csv file name is invalid", "")
                        Exit Sub
                    End If
                End If


                Dim strMonth As String = Right(strFileName, 2)
                Dim strYear As String = Mid(strFileName, Len(strFileName) - 5, 4)
                'Verify integer
                Dim numChk As Integer = 0
                If Integer.TryParse(strMonth, numChk) = False Then
                    Call showMsg("CSV File Name is Invalid in End of (yyyymm)", "")
                    Exit Sub
                End If
                'Maximum Allowed 12 Months
                If numChk > 12 Then
                    Call showMsg("CSV File Name is Invalid in End of (yyyymm)", "")
                    Exit Sub
                End If
                If Integer.TryParse(strYear, numChk) = False Then
                    Call showMsg("CSV File Name is Invalid in End of (yyyymm)", "")
                    Exit Sub
                End If
                'Allowed Greater than 2018
                If numChk < 2018 Then
                    Call showMsg("CSV File Name is Invalid in End of (yyyymm)", "")
                    Exit Sub
                End If
                'Move the file to temporary folder
                If Not Directory.Exists(ConfigurationManager.AppSettings("RootDir") & "\" & DropListLocation.Text & "\" & userName & "\" & "upload") Then
                    Directory.CreateDirectory(ConfigurationManager.AppSettings("RootDir") & "\" & DropListLocation.Text & "\" & userName & "\" & "upload")
                End If
                'Assign File Name
                Dim dirPath As String = ConfigurationManager.AppSettings("RootDir") & "\" & DropListLocation.Text & "\" & userName & "\" & "upload"
                Dim fileName As String = strFileName & "_" & DateTime.Now.ToString("yyyyMMddHHmmssfff") & strExtension
                Dim filenameFullPath As String = dirPath & "\" & fileName

                'Save the file in temporary folder
                FileUploadAnalysis.SaveAs(filenameFullPath)
                'Update to Database
                Item9(filenameFullPath, fileName, userid, userName, strSrcFileName, strYear & "/" & strMonth)

            ElseIf drpTask.SelectedValue = "9A" Then
                '*********************************
                'Item9A Stock OverView Count
                '*********************************
                'Verify FileName Rules Followed
                Dim strFileName As String = System.IO.Path.GetFileNameWithoutExtension(FileUploadAnalysis.FileName)
                Dim strSrcFileName As String = System.IO.Path.GetFileName(FileUploadAnalysis.FileName)

                'Verify that file name must more than 6 characters
                If Len(strFileName) < 11 Then
                    Call showMsg("CSV File Name is Invalid in End of (yyyymmc)", "")
                    Exit Sub
                End If

                'Split the branch name in File Name
                PositionStart = InStr(1, strSrcFileName, "_")
                If PositionStart = 0 Then ' There is no _ symbol in the file name
                    Call showMsg("The csv file name is invalid", "")
                    Exit Sub
                Else
                    strBranchName = Left(strSrcFileName, PositionStart - 1)
                    If Not (UCase(DropListLocation.SelectedItem.Text) = UCase(strBranchName)) Then
                        Call showMsg("The selected branch and file name is not matched", "")
                        Exit Sub
                    End If
                End If
                'Middle ID in file name
                PositionStart = InStr(1, strSrcFileName, "_")
                If PositionStart = 0 Then ' There is no _ symbol in the file name
                    Call showMsg("The csv file name is invalid ", "")
                    Exit Sub
                Else
                    strTmp = Right(strSrcFileName, Len(strSrcFileName) - PositionStart)
                    strTmp = Left(strTmp, 2)
                    If (UCase(strTmp) = "SV") Then
                    Else
                        Call showMsg("The csv file name is invalid", "")
                        Exit Sub
                    End If

                    PositionStart = InStr(1, UCase(strSrcFileName), "C.CSV")
                    If PositionStart = 0 Then ' There is no _ symbol in the file name
                        Call showMsg("The csv file name is invalid", "")
                        Exit Sub
                    End If
                End If

                Dim strFileName1 As String = ""
                strFileName1 = Left(strFileName, Len(strFileName) - 1)
                Dim strMonth As String = Right(strFileName1, 2)
                Dim strYear As String = Mid(strFileName1, Len(strFileName1) - 5, 4)
                'Verify integer
                Dim numChk As Integer = 0
                If Integer.TryParse(strMonth, numChk) = False Then
                    Call showMsg("CSV File Name is Invalid in End of (yyyymm)", "")
                    Exit Sub
                End If
                'Maximum Allowed 12 Months
                If numChk > 12 Then
                    Call showMsg("CSV File Name is Invalid in End of (yyyymm)", "")
                    Exit Sub
                End If
                If Integer.TryParse(strYear, numChk) = False Then
                    Call showMsg("CSV File Name is Invalid in End of (yyyymm)", "")
                    Exit Sub
                End If
                'Allowed Greater than 2018
                If numChk < 2018 Then
                    Call showMsg("CSV File Name is Invalid in End of (yyyymm)", "")
                    Exit Sub
                End If
                'Move the file to temporary folder
                If Not Directory.Exists(ConfigurationManager.AppSettings("RootDir") & "\" & DropListLocation.Text & "\" & userName & "\" & "upload") Then
                    Directory.CreateDirectory(ConfigurationManager.AppSettings("RootDir") & "\" & DropListLocation.Text & "\" & userName & "\" & "upload")
                End If
                'Assign File Name
                Dim dirPath As String = ConfigurationManager.AppSettings("RootDir") & "\" & DropListLocation.Text & "\" & userName & "\" & "upload"
                Dim fileName As String = strFileName & "_" & DateTime.Now.ToString("yyyyMMddHHmmssfff") & strExtension
                Dim filenameFullPath As String = dirPath & "\" & fileName
                'Save the file in temporary folder
                FileUploadAnalysis.SaveAs(filenameFullPath)

                'Remove C.csv to .csv
                strSrcFileName = Replace(strSrcFileName, "c.csv", ".csv")
                strSrcFileName = Replace(strSrcFileName, "C.csv", ".csv")
                'Update to Database
                Item9A(filenameFullPath, fileName, userid, userName, strSrcFileName, DropDownYear.SelectedValue & "/" & DropDownMonth.SelectedValue)


            ElseIf drpTask.SelectedValue = "10" Then
                '*********************************
                'Item10 SAW_DISCOUNT
                '*********************************
                'Verify FileName Rules Followed
                Dim strFileName As String = System.IO.Path.GetFileNameWithoutExtension(FileUploadAnalysis.FileName)
                Dim strSrcFileName As String = System.IO.Path.GetFileName(FileUploadAnalysis.FileName)
                'Verify that file name must more than 6 characters
                If Len(strFileName) < 10 Then
                    Call showMsg("The csv file name is invalid", "")
                    Exit Sub
                End If
                'Split the branch name in File Name
                PositionStart = InStr(1, strSrcFileName, "_")
                If PositionStart = 0 Then ' There is no _ symbol in the file name
                    Call showMsg("The csv file name is invalid", "")
                    Exit Sub
                Else
                    strBranchName = Left(strSrcFileName, PositionStart - 1)
                    If Not (UCase(DropListLocation.SelectedItem.Text) = UCase(strBranchName)) Then
                        Call showMsg("The selected branch and file name is not matched", "")
                        Exit Sub
                    End If
                End If
                'Middle ID in file name
                PositionStart = InStr(1, strSrcFileName, "_")
                If PositionStart = 0 Then ' There is no _ symbol in the file name
                    Call showMsg("The csv file name is invalid ", "")
                    Exit Sub
                Else
                    strTmp = Right(strSrcFileName, Len(strSrcFileName) - PositionStart)
                    strTmp = Left(strTmp, 2)
                    If (UCase(strTmp) = "SW") Then
                    Else
                        Call showMsg("The csv file name is invalid", "")
                        Exit Sub
                    End If
                End If
                Dim strMonth As String = Right(strFileName, 2)
                Dim strYear As String = Mid(strFileName, Len(strFileName) - 5, 4)
                'Verify integer
                Dim numChk As Integer = 0
                If Integer.TryParse(strMonth, numChk) = False Then
                    Call showMsg("The csv file name is invalid", "")
                    Exit Sub
                End If
                'Maximum Allowed 12 Months
                If numChk > 12 Then
                    Call showMsg("The csv file name is invalid", "")
                    Exit Sub
                End If
                If Integer.TryParse(strYear, numChk) = False Then
                    Call showMsg("The csv file name is invalid", "")
                    Exit Sub
                End If
                'Allowed Greater than 2018
                If numChk < 2018 Then
                    Call showMsg("The csv file name is invalid", "")
                    Exit Sub
                End If
                'Move the file to temporary folder
                If Not Directory.Exists(ConfigurationManager.AppSettings("RootDir") & "\" & DropListLocation.Text & "\" & userName & "\" & "upload") Then
                    Directory.CreateDirectory(ConfigurationManager.AppSettings("RootDir") & "\" & DropListLocation.Text & "\" & userName & "\" & "upload")
                End If
                'Assign File Name
                Dim dirPath As String = ConfigurationManager.AppSettings("RootDir") & "\" & DropListLocation.Text & "\" & userName & "\" & "upload"
                Dim fileName As String = strFileName & "_" & DateTime.Now.ToString("yyyyMMddHHmmssfff") & strExtension
                Dim filenameFullPath As String = dirPath & "\" & fileName
                'Save the file in temporary folder
                FileUploadAnalysis.SaveAs(filenameFullPath)
                'Update to Database
                Item10(filenameFullPath, fileName, userid, userName, strSrcFileName)
            ElseIf drpTask.SelectedValue = "11" Then
                '*********************************
                'Item11 Parts in&out History
                '*********************************
                'Verify FileName Rules Followed
                Dim strFileName As String = System.IO.Path.GetFileNameWithoutExtension(FileUploadAnalysis.FileName)
                Dim strSrcFileName As String = System.IO.Path.GetFileName(FileUploadAnalysis.FileName)
                'Verify that file name must more than 6 characters
                If Len(strFileName) < 12 Then
                    Call showMsg("The csv file name is invalid", "")
                    Exit Sub
                End If
                'Split the branch name in File Name
                PositionStart = InStr(1, strSrcFileName, "_")
                If PositionStart = 0 Then ' There is no _ symbol in the file name
                    Call showMsg("The csv file name is invalid", "")
                    Exit Sub
                Else
                    strBranchName = Left(strSrcFileName, PositionStart - 1)
                    If Not (UCase(DropListLocation.SelectedItem.Text) = UCase(strBranchName)) Then
                        Call showMsg("The selected branch and file name is not matched", "")
                        Exit Sub
                    End If
                End If
                'Middle ID in file name
                PositionStart = InStr(1, strSrcFileName, "_")
                If PositionStart = 0 Then ' There is no _ symbol in the file name
                    Call showMsg("The csv file name is invalid ", "")
                    Exit Sub
                Else
                    strTmp = Right(strSrcFileName, Len(strSrcFileName) - PositionStart)
                    strTmp = Left(strTmp, 3)
                    If (UCase(strTmp) = "PIO") Then
                    Else
                        Call showMsg("The csv file name is invalid", "")
                        Exit Sub
                    End If
                End If
                Dim strMonth As String = Right(strFileName, 2)
                Dim strYear As String = Mid(strFileName, Len(strFileName) - 5, 4)

                'Verify integer
                Dim numChk As Integer = 0
                If Integer.TryParse(strMonth, numChk) = False Then
                    Call showMsg("The csv file name is invalid", "")
                    Exit Sub
                End If
                'Maximum Allowed 12 Months
                If numChk > 12 Then
                    Call showMsg("The csv file name is invalid", "")
                    Exit Sub
                End If
                If Integer.TryParse(strYear, numChk) = False Then
                    Call showMsg("The csv file name is invalid", "")
                    Exit Sub
                End If
                'Allowed Greater than 2018
                If numChk < 2018 Then
                    Call showMsg("The csv file name is invalid", "")
                    Exit Sub
                End If
                'Move the file to temporary folder
                If Not Directory.Exists(ConfigurationManager.AppSettings("RootDir") & "\" & DropListLocation.Text & "\" & userName & "\" & "upload") Then
                    Directory.CreateDirectory(ConfigurationManager.AppSettings("RootDir") & "\" & DropListLocation.Text & "\" & userName & "\" & "upload")
                End If
                'Assign File Name
                Dim dirPath As String = ConfigurationManager.AppSettings("RootDir") & "\" & DropListLocation.Text & "\" & userName & "\" & "upload"
                Dim fileName As String = strFileName & "_" & DateTime.Now.ToString("yyyyMMddHHmmssfff") & strExtension
                Dim filenameFullPath As String = dirPath & "\" & fileName
                'Save the file in temporary folder
                FileUploadAnalysis.SaveAs(filenameFullPath)
                'Update to Database
                Item11(filenameFullPath, fileName, userid, userName, strSrcFileName)
            ElseIf drpTask.SelectedValue = "12" Then
                '*********************************
                'Item11 Parts in&out History
                '*********************************
            ElseIf drpTask.SelectedValue = "13" Then
                '*********************************
                'Item11 Parts in&out History
                '*********************************

            ElseIf drpTask.SelectedValue = "14" Then
                '*********************************
                'Item14 GSS Paid to Samsung
                '*********************************
                'Verify FileName Rules Followed
                Dim strFileName As String = System.IO.Path.GetFileNameWithoutExtension(FileUploadAnalysis.FileName)
                Dim strSrcFileName As String = System.IO.Path.GetFileName(FileUploadAnalysis.FileName)
                'Verify that file name must more than 6 characters
                If Len(strFileName) < 10 Then
                    Call showMsg("The csv file name is invalid ", "")
                    Exit Sub
                End If
                'Split the branch name in File Name
                PositionStart = InStr(1, strSrcFileName, "_")
                If PositionStart = 0 Then ' There is no _ symbol in the file name
                    Call showMsg("The csv file name is invalid", "")
                    Exit Sub
                Else
                    strBranchName = Left(strSrcFileName, PositionStart - 1)
                    If Not (UCase(DropListLocation.SelectedItem.Text) = UCase(strBranchName)) Then
                        Call showMsg("The selected branch and file name is not matched", "")
                        Exit Sub
                    End If
                End If
                'Middle ID in file name
                PositionStart = InStr(1, strSrcFileName, "_")
                If PositionStart = 0 Then ' There is no _ symbol in the file name
                    Call showMsg("The csv file name is invalid ", "")
                    Exit Sub
                Else
                    strTmp = Right(strSrcFileName, Len(strSrcFileName) - PositionStart)
                    strTmp = Left(strTmp, 2)
                    If (UCase(strTmp) = "GS") Then
                    Else
                        Call showMsg("The csv file name is invalid", "")
                        Exit Sub
                    End If
                End If

                Dim strMonth As String = Right(strFileName, 2)
                Dim strYear As String = Mid(strFileName, Len(strFileName) - 5, 4)
                'Verify integer
                Dim numChk As Integer = 0
                If Integer.TryParse(strMonth, numChk) = False Then
                    Call showMsg("The csv file name is invalid ", "")
                    Exit Sub
                End If
                'Maximum Allowed 12 Months
                If numChk > 12 Then
                    Call showMsg("The csv file name is invalid ", "")
                    Exit Sub
                End If
                If Integer.TryParse(strYear, numChk) = False Then
                    Call showMsg("The csv file name is invalid ", "")
                    Exit Sub
                End If
                'Allowed Greater than 2018
                If numChk < 2018 Then
                    Call showMsg("The csv file name is invalid ", "")
                    Exit Sub
                End If
                'Move the file to temporary folder
                If Not Directory.Exists(ConfigurationManager.AppSettings("RootDir") & "\" & DropListLocation.Text & "\" & userName & "\" & "upload") Then
                    Directory.CreateDirectory(ConfigurationManager.AppSettings("RootDir") & "\" & DropListLocation.Text & "\" & userName & "\" & "upload")
                End If
                'Assign File Name
                Dim dirPath As String = ConfigurationManager.AppSettings("RootDir") & "\" & DropListLocation.Text & "\" & userName & "\" & "upload"
                Dim fileName As String = strFileName & "_" & DateTime.Now.ToString("yyyyMMddHHmmssfff") & strExtension
                Dim filenameFullPath As String = dirPath & "\" & fileName
                'Save the file in temporary folder
                FileUploadAnalysis.SaveAs(filenameFullPath)
                'Update to Database
                Item14(filenameFullPath, fileName, userid, userName, strSrcFileName)
            ElseIf drpTask.SelectedValue = "15" Then
                '*********************************
                'Item15 RETURN Credits
                '*********************************
                'Verify FileName Rules Followed
                Dim strFileName As String = System.IO.Path.GetFileNameWithoutExtension(FileUploadAnalysis.FileName)
                Dim strSrcFileName As String = System.IO.Path.GetFileName(FileUploadAnalysis.FileName)

                'Verify that file name must more than 6 characters
                If Len(strFileName) < 10 Then
                    Call showMsg("The csv file name is invalid", "")
                    Exit Sub
                End If

                PositionStart = InStr(1, strSrcFileName, "_")
                If PositionStart = 0 Then ' There is no _ symbol in the file name
                    Call showMsg("The csv file name is invalid", "")
                    Exit Sub
                Else
                    strBranchName = Left(strSrcFileName, PositionStart - 1)
                    If Not (UCase(DropListLocation.SelectedItem.Text) = UCase(strBranchName)) Then
                        Call showMsg("The selected branch and file name is not matched", "")
                        Exit Sub
                    End If
                End If
                'Middle ID in file name
                PositionStart = InStr(1, strSrcFileName, "_")
                If PositionStart = 0 Then ' There is no _ symbol in the file name
                    Call showMsg("The csv file name is invalid ", "")
                    Exit Sub
                Else
                    strTmp = Right(strSrcFileName, Len(strSrcFileName) - PositionStart)
                    strTmp = Left(strTmp, 2)
                    If (UCase(strTmp) = "RC") Then
                    Else
                        Call showMsg("The csv file name is invalid", "")
                        Exit Sub
                    End If
                End If


                Dim strMonth As String = Right(strFileName, 2)
                Dim strYear As String = Mid(strFileName, Len(strFileName) - 5, 4)
                'Verify integer
                Dim numChk As Integer = 0
                If Integer.TryParse(strMonth, numChk) = False Then
                    Call showMsg("The csv file name is invalid", "")
                    Exit Sub
                End If
                'Maximum Allowed 12 Months
                If numChk > 12 Then
                    Call showMsg("The csv file name is invalid", "")
                    Exit Sub
                End If
                If Integer.TryParse(strYear, numChk) = False Then
                    Call showMsg("The csv file name is invalid", "")
                    Exit Sub
                End If
                'Allowed Greater than 2018
                If numChk < 2018 Then
                    Call showMsg("The csv file name is invalid", "")
                    Exit Sub
                End If
                'Move the file to temporary folder
                If Not Directory.Exists(ConfigurationManager.AppSettings("RootDir") & "\" & DropListLocation.Text & "\" & userName & "\" & "upload") Then
                    Directory.CreateDirectory(ConfigurationManager.AppSettings("RootDir") & "\" & DropListLocation.Text & "\" & userName & "\" & "upload")
                End If
                'Assign File Name
                Dim dirPath As String = ConfigurationManager.AppSettings("RootDir") & "\" & DropListLocation.Text & "\" & userName & "\" & "upload"
                Dim fileName As String = strFileName & "_" & DateTime.Now.ToString("yyyyMMddHHmmssfff") & strExtension
                Dim filenameFullPath As String = dirPath & "\" & fileName
                'Save the file in temporary folder
                FileUploadAnalysis.SaveAs(filenameFullPath)
                'Update to Database
                Item15(filenameFullPath, fileName, userid, userName, strSrcFileName)
            ElseIf drpTask.SelectedValue = "16" Then
                '*********************************
                'Item16 Samsung Ledger
                '*********************************
                'Verify FileName Rules Followed
                Dim strFileName As String = System.IO.Path.GetFileNameWithoutExtension(FileUploadAnalysis.FileName)
                Dim strSrcFileName As String = System.IO.Path.GetFileName(FileUploadAnalysis.FileName)
                'Verify that file name must more than 6 characters
                If Len(strFileName) < 13 Then
                    Call showMsg("The csv file name is invalid", "")
                    Exit Sub
                End If

                'Split the branch name in File Name
                PositionStart = InStr(1, strSrcFileName, "_")
                If PositionStart = 0 Then ' There is no _ symbol in the file name
                    Call showMsg("The csv file name is Invalid", "")
                    Exit Sub
                Else
                    strBranchName = Left(strSrcFileName, PositionStart - 1)
                    If Not (UCase(DropListLocation.SelectedItem.Text) = UCase(strBranchName)) Then
                        Call showMsg("The selected branch and file name is not matched", "")
                        Exit Sub
                    End If
                End If
                'Middle ID in file name
                PositionStart = InStr(1, strSrcFileName, "_")
                If PositionStart = 0 Then ' There is no _ symbol in the file name
                    Call showMsg("The csv file name is invalid ", "")
                    Exit Sub
                Else
                    strTmp = Right(strSrcFileName, Len(strSrcFileName) - PositionStart)
                    strTmp = Left(strTmp, 2)
                    If (UCase(strTmp) = "LG") Then
                    Else
                        Call showMsg("The csv file name is invalid", "")
                        Exit Sub
                    End If
                End If


                Dim strDay As String = Right(strFileName, 2)
                Dim strMonth As String = Mid(strFileName, Len(strFileName) - 3, 2)
                Dim strYear As String = Mid(strFileName, Len(strFileName) - 7, 4)

                'Verify integer
                Dim numChk As Integer = 0
                If Integer.TryParse(strDay, numChk) = False Then
                    Call showMsg("The csv file name is invalid", "")
                    Exit Sub
                End If
                'Allowed 01/11/21 
                If Not (numChk = 1 Or numChk = 11 Or numChk = 21) Then
                    Call showMsg("The csv file name is invalid", "")
                    Exit Sub
                End If

                If Integer.TryParse(strMonth, numChk) = False Then
                    Call showMsg("The csv file name is invalid", "")
                    Exit Sub
                End If
                'Maximum Allowed 12 Months
                If numChk > 12 Then
                    Call showMsg("The csv file name is invalid", "")
                    Exit Sub
                End If
                If Integer.TryParse(strYear, numChk) = False Then
                    Call showMsg("The csv file name is invalid", "")
                    Exit Sub
                End If
                'Allowed Greater than 2018
                If numChk < 2018 Then
                    Call showMsg("The csv file name is invalid", "")
                    Exit Sub
                End If
                'Move the file to temporary folder
                If Not Directory.Exists(ConfigurationManager.AppSettings("RootDir") & "\" & DropListLocation.Text & "\" & userName & "\" & "upload") Then
                    Directory.CreateDirectory(ConfigurationManager.AppSettings("RootDir") & "\" & DropListLocation.Text & "\" & userName & "\" & "upload")
                End If
                'Assign File Name
                Dim dirPath As String = ConfigurationManager.AppSettings("RootDir") & "\" & DropListLocation.Text & "\" & userName & "\" & "upload"
                Dim fileName As String = strFileName & "_" & DateTime.Now.ToString("yyyyMMddHHmmssfff") & strExtension
                Dim filenameFullPath As String = dirPath & "\" & fileName
                'Save the file in temporary folder
                FileUploadAnalysis.SaveAs(filenameFullPath)
                'Update to Database
                Item16(filenameFullPath, fileName, userid, userName, strSrcFileName)
            ElseIf (drpTask.SelectedValue = "17") Or (drpTask.SelectedValue = "18") Then
                '*********************************
                'ServicePartReturn & Debit Note Register
                '*********************************
                'Verify FileName Rules Followed
                Dim strFileName As String = System.IO.Path.GetFileNameWithoutExtension(FileUploadAnalysis.FileName)
                Dim strSrcFileName As String = System.IO.Path.GetFileName(FileUploadAnalysis.FileName)

                'Verify that file name must more than 6 characters
                If Len(strFileName) < 10 Then
                    Call showMsg("The csv file name is invalid", "")
                    Exit Sub
                End If

                PositionStart = InStr(1, strSrcFileName, "_")
                If PositionStart = 0 Then ' There is no _ symbol in the file name
                    Call showMsg("The csv file name is invalid", "")
                    Exit Sub
                Else
                    strBranchName = Left(strSrcFileName, PositionStart - 1)
                    If Not (UCase(DropListLocation.SelectedItem.Text) = UCase(strBranchName)) Then
                        Call showMsg("The selected branch and file name is not matched", "")
                        Exit Sub
                    End If
                End If
                'Middle ID in file name
                PositionStart = InStr(1, strSrcFileName, "_")
                If PositionStart = 0 Then ' There is no _ symbol in the file name
                    Call showMsg("The csv file name is invalid ", "")
                    Exit Sub
                Else
                    strTmp = Right(strSrcFileName, Len(strSrcFileName) - PositionStart)
                    strTmp = Left(strTmp, 2)
                    If drpTask.SelectedValue = "17" Then

                        If (UCase(strTmp) = "PR") Then
                        Else
                            Call showMsg("The csv file name is invalid", "")
                            Exit Sub
                        End If
                    ElseIf drpTask.SelectedValue = "18" Then
                        If (UCase(strTmp) = "DN") Then
                        Else
                            Call showMsg("The csv file name is invalid", "")
                            Exit Sub
                        End If

                    End If

                End If

                Dim strMonth As String = Right(strFileName, 2)
                Dim strYear As String = Mid(strFileName, Len(strFileName) - 5, 4)
                'Verify integer
                Dim numChk As Integer = 0
                If Integer.TryParse(strMonth, numChk) = False Then
                    Call showMsg("The csv file name is invalid", "")
                    Exit Sub
                End If
                'Maximum Allowed 12 Months
                If numChk > 12 Then
                    Call showMsg("The csv file name is invalid", "")
                    Exit Sub
                End If
                If Integer.TryParse(strYear, numChk) = False Then
                    Call showMsg("The csv file name is invalid", "")
                    Exit Sub
                End If
                'Allowed Greater than 2018
                If numChk < 2018 Then
                    Call showMsg("The csv file name is invalid", "")
                    Exit Sub
                End If
                'Move the file to temporary folder
                If Not Directory.Exists(ConfigurationManager.AppSettings("RootDir") & "\" & DropListLocation.Text & "\" & userName & "\" & "upload") Then
                    Directory.CreateDirectory(ConfigurationManager.AppSettings("RootDir") & "\" & DropListLocation.Text & "\" & userName & "\" & "upload")
                End If
                'Assign File Name
                Dim dirPath As String = ConfigurationManager.AppSettings("RootDir") & "\" & DropListLocation.Text & "\" & userName & "\" & "upload"
                Dim fileName As String = strFileName & "_" & DateTime.Now.ToString("yyyyMMddHHmmssfff") & strExtension
                Dim filenameFullPath As String = dirPath & "\" & fileName
                'Save the file in temporary folder
                FileUploadAnalysis.SaveAs(filenameFullPath)
                'Update to Database
                If drpTask.SelectedValue = "17" Then
                    ServicepartReturn(filenameFullPath, fileName, userid, userName, strSrcFileName)
                ElseIf drpTask.SelectedValue = "18" Then
                    DebitNoteRegister(filenameFullPath, fileName, userid, userName, strSrcFileName)
                End If

                'VJ 2019/10/10 End
                'ElseIf drpTask.SelectedValue = "19A" Then 'VJ 2019/10/17
                '    '*********************************
                '    'HSN Code with PR Details
                '    '*********************************
                '    'Verify FileName Rules Followed
                '    Dim strFileName As String = System.IO.Path.GetFileNameWithoutExtension(FileUploadAnalysis.FileName)
                '    Dim strSrcFileName As String = System.IO.Path.GetFileName(FileUploadAnalysis.FileName)

                '    'Verify that file name must more than 6 characters
                '    If Len(strFileName) <> 10 Then
                '        Call showMsg("The csv file name is invalid ", "")
                '        Exit Sub
                '    End If

                '    PositionStart = InStr(1, strSrcFileName, "-")
                '    If PositionStart = 0 Then ' There is no _ symbol in the file name
                '        Call showMsg("The csv file name is invalid ", "")
                '        Exit Sub
                '    Else
                '        strBranchName = Left(strSrcFileName, PositionStart - 1)
                '        If Not (UCase(strBranchName) = "HSN") Then
                '            Call showMsg("The selected file name is not valid", "")
                '            Exit Sub
                '        End If
                '    End If

                '    'Dim strDay As String = Right(strFileName, 2)
                '    'Dim strMonth As String = Right((strFileName, Len(strFileName) - 3, 2))
                '    Dim strMonth As String = Right(strFileName, 2)
                '    Dim strYear As String = Mid(strFileName, Len(strFileName) - 5, 4)
                '    'Verify integer
                '    Dim numChk As Integer = 0
                '    'Allowed 01/11/21 
                '    If Integer.TryParse(strMonth, numChk) = False Then
                '        Call showMsg("The csv file name is invalid ", "")
                '        Exit Sub
                '    End If
                '    'Maximum Allowed 12 Months
                '    If numChk > 12 Then
                '        Call showMsg("The csv file name is invalid ", "")
                '        Exit Sub
                '    End If
                '    If Integer.TryParse(strYear, numChk) = False Then
                '        Call showMsg("The csv file name is invalid ", "")
                '        Exit Sub
                '    End If
                '    'Allowed Greater than 2018
                '    If numChk < 2018 Then
                '        Call showMsg("The csv file name is invalid ", "")
                '        Exit Sub
                '    End If
                '    'Move the file to temporary folder
                '    If Not Directory.Exists(ConfigurationManager.AppSettings("RootDir") & "\" & DropListLocation.Text & "\" & userName & "\" & "upload") Then
                '        Directory.CreateDirectory(ConfigurationManager.AppSettings("RootDir") & "\" & DropListLocation.Text & "\" & userName & "\" & "upload")
                '    End If
                '    'Assign File Name
                '    Dim dirPath As String = ConfigurationManager.AppSettings("RootDir") & "\" & DropListLocation.Text & "\" & userName & "\" & "upload"
                '    Dim fileName As String = strFileName & "_" & DateTime.Now.ToString("yyyyMMddHHmmssfff") & strExtension
                '    Dim filenameFullPath As String = dirPath & "\" & fileName
                '    'Save the file in temporary folder
                '    FileUploadAnalysis.SaveAs(filenameFullPath)
                '    'Update to Database
                '    HSNCodeWithPRDetail(filenameFullPath, fileName, userid, userName, strSrcFileName)
            ElseIf drpTask.SelectedValue = "19B" Then 'VJ 2019/10/15
                '*********************************
                'HSN Code Detail
                '*********************************
                'Verify FileName Rules Followed
                Dim strFileName As String = System.IO.Path.GetFileNameWithoutExtension(FileUploadAnalysis.FileName)
                Dim strSrcFileName As String = System.IO.Path.GetFileName(FileUploadAnalysis.FileName)

                'Verify that file name must more than 6 characters
                If Len(strFileName) <> 10 Then
                    Call showMsg("The csv file name is invalid ", "")
                    Exit Sub
                End If

                PositionStart = InStr(1, strSrcFileName, "-")
                If PositionStart = 0 Then ' There is no _ symbol in the file name
                    Call showMsg("The csv file name is invalid ", "")
                    Exit Sub
                Else
                    strBranchName = Left(strSrcFileName, PositionStart - 1)
                    If Not (UCase(strBranchName) = "HNC") Then
                        Call showMsg("The selected file name is not valid", "")
                        Exit Sub
                    End If
                End If

                'Dim strDay As String = Right(strFileName, 2)
                'Dim strMonth As String = Right((strFileName, Len(strFileName) - 3, 2))
                Dim strMonth As String = Right(strFileName, 2)
                Dim strYear As String = Mid(strFileName, Len(strFileName) - 5, 4)
                'Verify integer
                Dim numChk As Integer = 0
                'Allowed 01/11/21 
                If Integer.TryParse(strMonth, numChk) = False Then
                    Call showMsg("The csv file name is invalid ", "")
                    Exit Sub
                End If
                'Maximum Allowed 12 Months
                If numChk > 12 Then
                    Call showMsg("The csv file name is invalid ", "")
                    Exit Sub
                End If
                If Integer.TryParse(strYear, numChk) = False Then
                    Call showMsg("The csv file name is invalid ", "")
                    Exit Sub
                End If
                'Allowed Greater than 2018
                If numChk < 2018 Then
                    Call showMsg("The csv file name is invalid ", "")
                    Exit Sub
                End If
                'Move the file to temporary folder
                If Not Directory.Exists(ConfigurationManager.AppSettings("RootDir") & "\" & DropListLocation.Text & "\" & userName & "\" & "upload") Then
                    Directory.CreateDirectory(ConfigurationManager.AppSettings("RootDir") & "\" & DropListLocation.Text & "\" & userName & "\" & "upload")
                End If
                'Assign File Name
                Dim dirPath As String = ConfigurationManager.AppSettings("RootDir") & "\" & DropListLocation.Text & "\" & userName & "\" & "upload"
                Dim fileName As String = strFileName & "_" & DateTime.Now.ToString("yyyyMMddHHmmssfff") & strExtension
                Dim filenameFullPath As String = dirPath & "\" & fileName
                'Save the file in temporary folder
                FileUploadAnalysis.SaveAs(filenameFullPath)
                'Update to Database
                HSNCode(filenameFullPath, fileName, userid, userName, strSrcFileName)
            ElseIf drpTask.SelectedValue = "20" Then
                '*********************************
                'Item20 OtherSalesExtendedWarranty
                '*********************************
                'Verify FileName Rules Followed
                Dim strFileName As String = System.IO.Path.GetFileNameWithoutExtension(FileUploadAnalysis.FileName)
                Dim strSrcFileName As String = System.IO.Path.GetFileName(FileUploadAnalysis.FileName)
                'Verify that file name must more than 6 characters
                If Len(strFileName) < 10 Then
                    Call showMsg("The csv file name is invalid", "")
                    Exit Sub
                End If
                'Split the branch name in File Name
                PositionStart = InStr(1, strSrcFileName, "_")
                If PositionStart = 0 Then ' There is no _ symbol in the file name
                    Call showMsg("The csv file name is invalid", "")
                    Exit Sub
                Else
                    strBranchName = Left(strSrcFileName, PositionStart - 1)
                    If Not (UCase(DropListLocation.SelectedItem.Text) = UCase(strBranchName)) Then
                        Call showMsg("The selected branch and file name is not matched", "")
                        Exit Sub
                    End If
                End If
                'Middle ID in file name
                PositionStart = InStr(1, strSrcFileName, "_")
                If PositionStart = 0 Then ' There is no _ symbol in the file name
                    Call showMsg("The csv file name is invalid ", "")
                    Exit Sub
                Else
                    strTmp = Right(strSrcFileName, Len(strSrcFileName) - PositionStart)
                    strTmp = Left(strTmp, 2)
                    If (UCase(strTmp) = "EW") Then
                    Else
                        Call showMsg("The csv file name is invalid", "")
                        Exit Sub
                    End If
                End If
                Dim strMonth As String = Right(strFileName, 2)
                Dim strYear As String = Mid(strFileName, Len(strFileName) - 5, 4)
                'Verify integer
                Dim numChk As Integer = 0
                If Integer.TryParse(strMonth, numChk) = False Then
                    Call showMsg("The csv file name is invalid", "")
                    Exit Sub
                End If
                'Maximum Allowed 12 Months
                If numChk > 12 Then
                    Call showMsg("The csv file name is invalid", "")
                    Exit Sub
                End If
                If Integer.TryParse(strYear, numChk) = False Then
                    Call showMsg("The csv file name is invalid", "")
                    Exit Sub
                End If
                'Allowed Greater than 2018
                If numChk < 2018 Then
                    Call showMsg("The csv file name is invalid", "")
                    Exit Sub
                End If
                'Move the file to temporary folder
                If Not Directory.Exists(ConfigurationManager.AppSettings("RootDir") & "\" & DropListLocation.Text & "\" & userName & "\" & "upload") Then
                    Directory.CreateDirectory(ConfigurationManager.AppSettings("RootDir") & "\" & DropListLocation.Text & "\" & userName & "\" & "upload")
                End If
                'Assign File Name
                Dim dirPath As String = ConfigurationManager.AppSettings("RootDir") & "\" & DropListLocation.Text & "\" & userName & "\" & "upload"
                Dim fileName As String = strFileName & "_" & DateTime.Now.ToString("yyyyMMddHHmmssfff") & strExtension
                Dim filenameFullPath As String = dirPath & "\" & fileName
                'Save the file in temporary folder
                FileUploadAnalysis.SaveAs(filenameFullPath)
                'Update to Database
                Item20(filenameFullPath, fileName, userid, userName, strSrcFileName)

            ElseIf drpTask.SelectedValue = "21" Then
                '*********************************
                'Item21 PO Status
                '*********************************
                'Verify FileName Rules Followed
                Dim strFileName As String = System.IO.Path.GetFileNameWithoutExtension(FileUploadAnalysis.FileName)
                Dim strSrcFileName As String = System.IO.Path.GetFileName(FileUploadAnalysis.FileName)
                'Verify that file name must more than 6 characters
                If Len(strFileName) < 10 Then
                    Call showMsg("The csv file name is invalid", "")
                    Exit Sub
                End If
                'Split the branch name in File Name
                PositionStart = InStr(1, strSrcFileName, "_")
                If PositionStart = 0 Then ' There is no _ symbol in the file name
                    Call showMsg("The csv file name is invalid", "")
                    Exit Sub
                Else
                    strBranchName = Left(strSrcFileName, PositionStart - 1)
                    If Not (UCase(DropListLocation.SelectedItem.Text) = UCase(strBranchName)) Then
                        Call showMsg("The selected branch and file name is not matched", "")
                        Exit Sub
                    End If
                End If
                'Middle ID in file name
                PositionStart = InStr(1, strSrcFileName, "_")
                If PositionStart = 0 Then ' There is no _ symbol in the file name
                    Call showMsg("The csv file name is invalid ", "")
                    Exit Sub
                Else
                    strTmp = Right(strSrcFileName, Len(strSrcFileName) - PositionStart)
                    strTmp = Left(strTmp, 2)
                    If (UCase(strTmp) = "PS") Then
                    Else
                        Call showMsg("The csv file name is invalid", "")
                        Exit Sub
                    End If
                End If
                Dim strMonth As String = Right(strFileName, 2)
                Dim strYear As String = Mid(strFileName, Len(strFileName) - 5, 4)
                'Verify integer
                Dim numChk As Integer = 0
                If Integer.TryParse(strMonth, numChk) = False Then
                    Call showMsg("The csv file name is invalid", "")
                    Exit Sub
                End If
                'Maximum Allowed 12 Months
                If numChk > 12 Then
                    Call showMsg("The csv file name is invalid", "")
                    Exit Sub
                End If
                If Integer.TryParse(strYear, numChk) = False Then
                    Call showMsg("The csv file name is invalid", "")
                    Exit Sub
                End If
                'Allowed Greater than 2018
                If numChk < 2018 Then
                    Call showMsg("The csv file name is invalid", "")
                    Exit Sub
                End If
                'Move the file to temporary folder
                If Not Directory.Exists(ConfigurationManager.AppSettings("RootDir") & "\" & DropListLocation.Text & "\" & userName & "\" & "upload") Then
                    Directory.CreateDirectory(ConfigurationManager.AppSettings("RootDir") & "\" & DropListLocation.Text & "\" & userName & "\" & "upload")
                End If
                'Assign File Name
                Dim dirPath As String = ConfigurationManager.AppSettings("RootDir") & "\" & DropListLocation.Text & "\" & userName & "\" & "upload"
                Dim fileName As String = strFileName & "_" & DateTime.Now.ToString("yyyyMMddHHmmssfff") & strExtension
                Dim filenameFullPath As String = dirPath & "\" & fileName
                'Save the file in temporary folder
                FileUploadAnalysis.SaveAs(filenameFullPath)
                'Update to Database
                Item21(filenameFullPath, fileName, userid, userName, strSrcFileName)
            ElseIf drpTask.SelectedValue = "22" Then
                '*********************************
                'Item22 ActivityReport
                '*********************************
                'Verify FileName Rules Followed
                Dim strFileName As String = System.IO.Path.GetFileNameWithoutExtension(FileUploadAnalysis.FileName)
                Dim strSrcFileName As String = System.IO.Path.GetFileName(FileUploadAnalysis.FileName)
                'Verify that file name must more than 6 characters
                If Len(strFileName) <> 15 Then
                    Call showMsg("The csv file name is invalid ", "")
                    Exit Sub
                End If
                'Split the branch name in File Name
                PositionStart = InStr(1, strSrcFileName, "_")
                If PositionStart = 0 Then ' There is no _ symbol in the file name
                    Call showMsg("The csv file name is Invalid", "")
                    Exit Sub
                Else
                    strBranchName = Left(strSrcFileName, PositionStart - 1)
                    If Not (UCase(DropListLocation.SelectedItem.Text) = UCase(strBranchName)) Then
                        Call showMsg("The selected branch and file name is not matched", "")
                        Exit Sub
                    End If
                End If
                'Middle ID in file name
                PositionStart = InStr(1, strSrcFileName, "_")
                If PositionStart = 0 Then ' There is no _ symbol in the file name
                    Call showMsg("The csv file name is invalid ", "")
                    Exit Sub
                Else
                    strTmp = Right(strSrcFileName, Len(strSrcFileName) - PositionStart)
                    strTmp = Left(strTmp, 2)
                    If (UCase(strTmp) = "AR") Then
                    Else
                        Call showMsg("The csv file name is invalid", "")
                        Exit Sub
                    End If
                End If


                Dim strDay As String = Right(strFileName, 2)
                Dim strMonth As String = Mid(strFileName, Len(strFileName) - 3, 2)
                Dim strYear As String = Mid(strFileName, Len(strFileName) - 7, 4)

                'Verify integer
                Dim numChk As Integer = 0
                If Integer.TryParse(strDay, numChk) = False Then
                    Call showMsg("The csv file name is invalid ", "")
                    Exit Sub
                End If
                ''''''Allowed 01/11/21 
                '''''If Not (numChk = 1 Or numChk = 11 Or numChk = 21) Then
                '''''    Call showMsg("The csv file name is invalid ", "")
                '''''    Exit Sub
                '''''End If

                If Integer.TryParse(strMonth, numChk) = False Then
                    Call showMsg("The csv file name is invalid ", "")
                    Exit Sub
                End If
                'Maximum Allowed 12 Months
                If numChk > 12 Then
                    Call showMsg("The csv file name is invalid ", "")
                    Exit Sub
                End If
                If Integer.TryParse(strYear, numChk) = False Then
                    Call showMsg("The csv file name is invalid ", "")
                    Exit Sub
                End If
                'Allowed Greater than 2018
                If numChk < 2018 Then
                    Call showMsg("The csv file name is invalid ", "")
                    Exit Sub
                End If

                Dim chkValidDate
                chkValidDate = IsDate(strMonth & "/" & strDay & "/" & strYear)

                If chkValidDate = False Then
                    Call showMsg("The csv file date is invalid ", "")
                    Exit Sub
                End If
                'Move the file to temporary folder
                If Not Directory.Exists(ConfigurationManager.AppSettings("RootDir") & "\" & DropListLocation.Text & "\" & userName & "\" & "upload") Then
                    Directory.CreateDirectory(ConfigurationManager.AppSettings("RootDir") & "\" & DropListLocation.Text & "\" & userName & "\" & "upload")
                End If
                'Assign File Name
                Dim dirPath As String = ConfigurationManager.AppSettings("RootDir") & "\" & DropListLocation.Text & "\" & userName & "\" & "upload"
                Dim fileName As String = strFileName & "_" & DateTime.Now.ToString("yyyyMMddHHmmssfff") & strExtension
                Dim filenameFullPath As String = dirPath & "\" & fileName
                'Save the file in temporary folder
                FileUploadAnalysis.SaveAs(filenameFullPath)
                'Update to Database
                Item22(filenameFullPath, fileName, userid, userName, strSrcFileName)

            ElseIf drpTask.SelectedValue = "23" Then

                'Item23 PODC
                '*********************************
                'Verify FileName Rules Followed
                Dim strFileName As String = System.IO.Path.GetFileNameWithoutExtension(FileUploadAnalysis.FileName)
                Dim strSrcFileName As String = System.IO.Path.GetFileName(FileUploadAnalysis.FileName)
                'Verify that file name must more than 6 characters
                If Len(strFileName) < 13 Then
                    Call showMsg("The csv file name is invalid ", "")
                    Exit Sub
                End If
                'Split the branch name in File Name
                PositionStart = InStr(1, strSrcFileName, "_")
                If PositionStart = 0 Then ' There is no _ symbol in the file name
                    Call showMsg("The csv file name is Invalid", "")
                    Exit Sub
                Else
                    strBranchName = Left(strSrcFileName, PositionStart - 1)
                    If Not (UCase(DropListLocation.SelectedItem.Text) = UCase(strBranchName)) Then
                        Call showMsg("The selected branch and file name is not matched", "")
                        Exit Sub
                    End If
                End If
                'Middle ID in file name
                PositionStart = InStr(1, strSrcFileName, "_")
                If PositionStart = 0 Then ' There is no _ symbol in the file name
                    Call showMsg("The csv file name is invalid ", "")
                    Exit Sub
                Else
                    strTmp = Right(strSrcFileName, Len(strSrcFileName) - PositionStart)
                    strTmp = Left(strTmp, 2)
                    If (UCase(strTmp) = "DC") Then
                    Else
                        Call showMsg("The csv file name is invalid", "")
                        Exit Sub
                    End If
                End If


                Dim strDay As String = Right(strFileName, 2)
                Dim strMonth As String = Mid(strFileName, Len(strFileName) - 3, 2)
                Dim strYear As String = Mid(strFileName, Len(strFileName) - 7, 4)

                'Verify integer
                Dim numChk As Integer = 0
                If Integer.TryParse(strDay, numChk) = False Then
                    Call showMsg("The csv file name is invalid ", "")
                    Exit Sub
                End If
                ''Allowed 01/11/21 
                'If Not (numChk = 1 Or numChk = 11 Or numChk = 21) Then
                '    Call showMsg("The csv file name is invalid ", "")
                '    Exit Sub
                'End If

                If Integer.TryParse(strMonth, numChk) = False Then
                    Call showMsg("The csv file name is invalid ", "")
                    Exit Sub
                End If
                'Maximum Allowed 12 Months
                If numChk > 12 Then
                    Call showMsg("The csv file name is invalid ", "")
                    Exit Sub
                End If
                If Integer.TryParse(strYear, numChk) = False Then
                    Call showMsg("The csv file name is invalid ", "")
                    Exit Sub
                End If
                'Allowed Greater than 2018
                If numChk < 2018 Then
                    Call showMsg("The csv file name is invalid ", "")
                    Exit Sub
                End If
                'Move the file to temporary folder
                If Not Directory.Exists(ConfigurationManager.AppSettings("RootDir") & "\" & DropListLocation.Text & "\" & userName & "\" & "upload") Then
                    Directory.CreateDirectory(ConfigurationManager.AppSettings("RootDir") & "\" & DropListLocation.Text & "\" & userName & "\" & "upload")
                End If
                'Assign File Name
                Dim dirPath As String = ConfigurationManager.AppSettings("RootDir") & "\" & DropListLocation.Text & "\" & userName & "\" & "upload"
                Dim fileName As String = strFileName & "_" & DateTime.Now.ToString("yyyyMMddHHmmssfff") & strExtension
                Dim filenameFullPath As String = dirPath & "\" & fileName
                'Save the file in temporary folder
                FileUploadAnalysis.SaveAs(filenameFullPath)
                'Update to Database
                Item23(filenameFullPath, fileName, userid, userName, strSrcFileName)
            ElseIf drpTask.SelectedValue = "24" Then
                'Item24 24.Samsung to GSS paid (BOI)
                '*********************************
                'Validate Date
                If Len(txtDate.Text) < 10 Then
                    Call showMsg("Date is invalid ", "")
                    Exit Sub
                End If
                'Validate Amount
                If Len(txtAmount.Text) < 1 Then
                    Call showMsg("Amount is invalid ", "")
                    Exit Sub
                End If
                If Not (CommonControl.isNumberDR(txtAmount.Text)) Then
                    Call showMsg("Amount is invalid ", "")
                    Exit Sub
                End If

                'AR No
                If Len(txtArNo.Text) < 1 Then
                    Call showMsg("Advice Reference No is invalid ", "")
                    Exit Sub
                End If
                'Pass the paramters to Update in Datatabase
                Dim _BoiModel As BoiModel = New BoiModel()
                Dim _BoiControl As BoiControl = New BoiControl()
                _BoiModel.UPDPG = "Analysis_FileUpload.vb"
                _BoiModel.UserId = userid
                _BoiModel.SHIP_TO_BRANCH_CODE = DropListLocation.SelectedItem.Value
                _BoiModel.SHIP_TO_BRANCH = DropListLocation.SelectedItem.Text
                _BoiModel.PAYMENT_DATE = txtDate.Text
                _BoiModel.DOC_NO = ""
                _BoiModel.AR_NO = txtArNo.Text
                _BoiModel.AMOUNT = txtAmount.Text
                'Update to db
                Dim blBoi As Boolean = _BoiControl.AddBoi(_BoiModel)
                'If Error Raised then show the errors
                If Not blBoi Then
                    Call showMsg("Status: Update Failed!!! ", "")
                    Exit Sub
                Else
                    txtArNo.Text = ""
                    txtDate.Text = ""
                    txtAmount.Text = ""
                    Call showMsg("Status: Successfully Updated", "")
                End If
            ElseIf drpTask.SelectedValue = "101" Then
                'Item101.In Bound 
                'Item23 PODC
                '*********************************
                'Verify FileName Rules Followed
                Dim strFileName As String = System.IO.Path.GetFileNameWithoutExtension(FileUploadAnalysis.FileName)
                Dim strSrcFileName As String = System.IO.Path.GetFileName(FileUploadAnalysis.FileName)
                'Verify that file name must more than 6 characters
                If Len(strFileName) < 13 Then
                    Call showMsg("The csv file name is invalid ", "")
                    Exit Sub
                End If
                'Split the branch name in File Name
                PositionStart = InStr(1, strSrcFileName, "_")
                If PositionStart = 0 Then ' There is no _ symbol in the file name
                    Call showMsg("The csv file name is Invalid", "")
                    Exit Sub
                Else
                    strBranchName = Left(strSrcFileName, PositionStart - 1)
                    If Not (UCase(DropListLocation.SelectedItem.Text) = UCase(strBranchName)) Then
                        Call showMsg("The selected branch and file name is not matched", "")
                        Exit Sub
                    End If
                End If
                'Middle ID in file name
                PositionStart = InStr(1, strSrcFileName, "_")
                If PositionStart = 0 Then ' There is no _ symbol in the file name
                    Call showMsg("The csv file name is invalid ", "")
                    Exit Sub
                Else
                    strTmp = Right(strSrcFileName, Len(strSrcFileName) - PositionStart)
                    strTmp = Left(strTmp, 2)
                    If (UCase(strTmp) = "IB") Then
                    Else
                        Call showMsg("The csv file name is invalid", "")
                        Exit Sub
                    End If
                End If


                Dim strDay As String = Right(strFileName, 2)
                Dim strMonth As String = Mid(strFileName, Len(strFileName) - 3, 2)
                Dim strYear As String = Mid(strFileName, Len(strFileName) - 7, 4)

                'Verify integer
                Dim numChk As Integer = 0
                If Integer.TryParse(strDay, numChk) = False Then
                    Call showMsg("The csv file name is invalid ", "")
                    Exit Sub
                End If
                ''Allowed 01/11/21 
                'If Not (numChk = 1 Or numChk = 11 Or numChk = 21) Then
                '    Call showMsg("The csv file name is invalid ", "")
                '    Exit Sub
                'End If

                If Integer.TryParse(strMonth, numChk) = False Then
                    Call showMsg("The csv file name is invalid ", "")
                    Exit Sub
                End If
                'Maximum Allowed 12 Months
                If numChk > 12 Then
                    Call showMsg("The csv file name is invalid ", "")
                    Exit Sub
                End If
                If Integer.TryParse(strYear, numChk) = False Then
                    Call showMsg("The csv file name is invalid ", "")
                    Exit Sub
                End If
                'Allowed Greater than 2018
                If numChk < 2018 Then
                    Call showMsg("The csv file name is invalid ", "")
                    Exit Sub
                End If
                'Move the file to temporary folder
                If Not Directory.Exists(ConfigurationManager.AppSettings("RootDir") & "\" & DropListLocation.Text & "\" & userName & "\" & "upload") Then
                    Directory.CreateDirectory(ConfigurationManager.AppSettings("RootDir") & "\" & DropListLocation.Text & "\" & userName & "\" & "upload")
                End If
                'Assign File Name
                Dim dirPath As String = ConfigurationManager.AppSettings("RootDir") & "\" & DropListLocation.Text & "\" & userName & "\" & "upload"
                Dim fileName As String = strFileName & "_" & DateTime.Now.ToString("yyyyMMddHHmmssfff") & strExtension
                Dim filenameFullPath As String = dirPath & "\" & fileName
                'Save the file in temporary folder
                FileUploadAnalysis.SaveAs(filenameFullPath)
                'Update to Database
                Item101(filenameFullPath, fileName, userid, userName, strSrcFileName)
            ElseIf drpTask.SelectedValue = "102" Then
                'Item102.Out Bound 
                '*********************************
                'Verify FileName Rules Followed
                Dim strFileName As String = System.IO.Path.GetFileNameWithoutExtension(FileUploadAnalysis.FileName)
                Dim strSrcFileName As String = System.IO.Path.GetFileName(FileUploadAnalysis.FileName)
                'Verify that file name must more than 6 characters
                If Len(strFileName) < 13 Then
                    Call showMsg("The csv file name is invalid ", "")
                    Exit Sub
                End If
                'Split the branch name in File Name
                PositionStart = InStr(1, strSrcFileName, "_")
                If PositionStart = 0 Then ' There is no _ symbol in the file name
                    Call showMsg("The csv file name is Invalid", "")
                    Exit Sub
                Else
                    strBranchName = Left(strSrcFileName, PositionStart - 1)
                    If Not (UCase(DropListLocation.SelectedItem.Text) = UCase(strBranchName)) Then
                        Call showMsg("The selected branch and file name is not matched", "")
                        Exit Sub
                    End If
                End If
                'Middle ID in file name
                PositionStart = InStr(1, strSrcFileName, "_")
                If PositionStart = 0 Then ' There is no _ symbol in the file name
                    Call showMsg("The csv file name is invalid ", "")
                    Exit Sub
                Else
                    strTmp = Right(strSrcFileName, Len(strSrcFileName) - PositionStart)
                    strTmp = Left(strTmp, 2)
                    If (UCase(strTmp) = "OB") Then
                    Else
                        Call showMsg("The csv file name is invalid", "")
                        Exit Sub
                    End If
                End If


                Dim strDay As String = Right(strFileName, 2)
                Dim strMonth As String = Mid(strFileName, Len(strFileName) - 3, 2)
                Dim strYear As String = Mid(strFileName, Len(strFileName) - 7, 4)

                'Verify integer
                Dim numChk As Integer = 0
                If Integer.TryParse(strDay, numChk) = False Then
                    Call showMsg("The csv file name is invalid ", "")
                    Exit Sub
                End If
                ''Allowed 01/11/21 
                'If Not (numChk = 1 Or numChk = 11 Or numChk = 21) Then
                '    Call showMsg("The csv file name is invalid ", "")
                '    Exit Sub
                'End If

                If Integer.TryParse(strMonth, numChk) = False Then
                    Call showMsg("The csv file name is invalid ", "")
                    Exit Sub
                End If
                'Maximum Allowed 12 Months
                If numChk > 12 Then
                    Call showMsg("The csv file name is invalid ", "")
                    Exit Sub
                End If
                If Integer.TryParse(strYear, numChk) = False Then
                    Call showMsg("The csv file name is invalid ", "")
                    Exit Sub
                End If
                'Allowed Greater than 2018
                If numChk < 2018 Then
                    Call showMsg("The csv file name is invalid ", "")
                    Exit Sub
                End If
                'Move the file to temporary folder
                If Not Directory.Exists(ConfigurationManager.AppSettings("RootDir") & "\" & DropListLocation.Text & "\" & userName & "\" & "upload") Then
                    Directory.CreateDirectory(ConfigurationManager.AppSettings("RootDir") & "\" & DropListLocation.Text & "\" & userName & "\" & "upload")
                End If
                'Assign File Name
                Dim dirPath As String = ConfigurationManager.AppSettings("RootDir") & "\" & DropListLocation.Text & "\" & userName & "\" & "upload"
                Dim fileName As String = strFileName & "_" & DateTime.Now.ToString("yyyyMMddHHmmssfff") & strExtension
                Dim filenameFullPath As String = dirPath & "\" & fileName
                'Save the file in temporary folder
                FileUploadAnalysis.SaveAs(filenameFullPath)
                'Update to Database
                Item102(filenameFullPath, fileName, userid, userName, strSrcFileName)
            ElseIf drpTask.SelectedValue = "103" Then
                'Item103. Part ORder
                '*********************************
                'Verify FileName Rules Followed
                Dim strFileName As String = System.IO.Path.GetFileNameWithoutExtension(FileUploadAnalysis.FileName)
                Dim strSrcFileName As String = System.IO.Path.GetFileName(FileUploadAnalysis.FileName)
                'Verify that file name must more than 6 characters
                If Len(strFileName) < 13 Then
                    Call showMsg("The csv file name is invalid ", "")
                    Exit Sub
                End If
                'Split the branch name in File Name
                PositionStart = InStr(1, strSrcFileName, "_")
                If PositionStart = 0 Then ' There is no _ symbol in the file name
                    Call showMsg("The csv file name is Invalid", "")
                    Exit Sub
                Else
                    strBranchName = Left(strSrcFileName, PositionStart - 1)
                    If Not (UCase(DropListLocation.SelectedItem.Text) = UCase(strBranchName)) Then
                        Call showMsg("The selected branch and file name is not matched", "")
                        Exit Sub
                    End If
                End If
                'Middle ID in file name
                PositionStart = InStr(1, strSrcFileName, "_")
                If PositionStart = 0 Then ' There is no _ symbol in the file name
                    Call showMsg("The csv file name is invalid ", "")
                    Exit Sub
                Else
                    strTmp = Right(strSrcFileName, Len(strSrcFileName) - PositionStart)
                    strTmp = Left(strTmp, 2)
                    If (UCase(strTmp) = "PO") Then
                    Else
                        Call showMsg("The csv file name is invalid", "")
                        Exit Sub
                    End If
                End If


                Dim strDay As String = Right(strFileName, 2)
                Dim strMonth As String = Mid(strFileName, Len(strFileName) - 3, 2)
                Dim strYear As String = Mid(strFileName, Len(strFileName) - 7, 4)

                'Verify integer
                Dim numChk As Integer = 0
                If Integer.TryParse(strDay, numChk) = False Then
                    Call showMsg("The csv file name is invalid ", "")
                    Exit Sub
                End If
                ''Allowed 01/11/21 
                'If Not (numChk = 1 Or numChk = 11 Or numChk = 21) Then
                '    Call showMsg("The csv file name is invalid ", "")
                '    Exit Sub
                'End If

                If Integer.TryParse(strMonth, numChk) = False Then
                    Call showMsg("The csv file name is invalid ", "")
                    Exit Sub
                End If
                'Maximum Allowed 12 Months
                If numChk > 12 Then
                    Call showMsg("The csv file name is invalid ", "")
                    Exit Sub
                End If
                If Integer.TryParse(strYear, numChk) = False Then
                    Call showMsg("The csv file name is invalid ", "")
                    Exit Sub
                End If
                'Allowed Greater than 2018
                If numChk < 2018 Then
                    Call showMsg("The csv file name is invalid ", "")
                    Exit Sub
                End If
                'Move the file to temporary folder
                If Not Directory.Exists(ConfigurationManager.AppSettings("RootDir") & "\" & DropListLocation.Text & "\" & userName & "\" & "upload") Then
                    Directory.CreateDirectory(ConfigurationManager.AppSettings("RootDir") & "\" & DropListLocation.Text & "\" & userName & "\" & "upload")
                End If
                'Assign File Name
                Dim dirPath As String = ConfigurationManager.AppSettings("RootDir") & "\" & DropListLocation.Text & "\" & userName & "\" & "upload"
                Dim fileName As String = strFileName & "_" & DateTime.Now.ToString("yyyyMMddHHmmssfff") & strExtension
                Dim filenameFullPath As String = dirPath & "\" & fileName
                'Save the file in temporary folder
                FileUploadAnalysis.SaveAs(filenameFullPath)
                'Update to Database
                Item103(filenameFullPath, fileName, userid, userName, strSrcFileName)
            ElseIf drpTask.SelectedValue = "104" Then
                'Item103. RPSI Inquiry
                '*********************************
                'Verify FileName Rules Followed
                Dim strFileName As String = System.IO.Path.GetFileNameWithoutExtension(FileUploadAnalysis.FileName)
                Dim strSrcFileName As String = System.IO.Path.GetFileName(FileUploadAnalysis.FileName)
                'Verify that file name must more than 6 characters
                If Len(strFileName) < 13 Then
                    Call showMsg("The csv file name is invalid ", "")
                    Exit Sub
                End If
                'Split the branch name in File Name
                PositionStart = InStr(1, strSrcFileName, "_")
                If PositionStart = 0 Then ' There is no _ symbol in the file name
                    Call showMsg("The csv file name is Invalid", "")
                    Exit Sub
                Else
                    strBranchName = Left(strSrcFileName, PositionStart - 1)
                    If Not (UCase(DropListLocation.SelectedItem.Text) = UCase(strBranchName)) Then
                        Call showMsg("The selected branch and file name is not matched", "")
                        Exit Sub
                    End If
                End If
                'Middle ID in file name
                PositionStart = InStr(1, strSrcFileName, "_")
                If PositionStart = 0 Then ' There is no _ symbol in the file name
                    Call showMsg("The csv file name is invalid ", "")
                    Exit Sub
                Else
                    strTmp = Right(strSrcFileName, Len(strSrcFileName) - PositionStart)
                    strTmp = Left(strTmp, 2)
                    If (UCase(strTmp) = "RI") Then
                    Else
                        Call showMsg("The csv file name is invalid", "")
                        Exit Sub
                    End If
                End If


                Dim strDay As String = Right(strFileName, 2)
                Dim strMonth As String = Mid(strFileName, Len(strFileName) - 3, 2)
                Dim strYear As String = Mid(strFileName, Len(strFileName) - 7, 4)

                'Verify integer
                Dim numChk As Integer = 0
                If Integer.TryParse(strDay, numChk) = False Then
                    Call showMsg("The csv file name is invalid ", "")
                    Exit Sub
                End If
                ''Allowed 01/11/21 
                'If Not (numChk = 1 Or numChk = 11 Or numChk = 21) Then
                '    Call showMsg("The csv file name is invalid ", "")
                '    Exit Sub
                'End If

                If Integer.TryParse(strMonth, numChk) = False Then
                    Call showMsg("The csv file name is invalid ", "")
                    Exit Sub
                End If
                'Maximum Allowed 12 Months
                If numChk > 12 Then
                    Call showMsg("The csv file name is invalid ", "")
                    Exit Sub
                End If
                If Integer.TryParse(strYear, numChk) = False Then
                    Call showMsg("The csv file name is invalid ", "")
                    Exit Sub
                End If
                'Allowed Greater than 2018
                If numChk < 2018 Then
                    Call showMsg("The csv file name is invalid ", "")
                    Exit Sub
                End If
                'Move the file to temporary folder
                If Not Directory.Exists(ConfigurationManager.AppSettings("RootDir") & "\" & DropListLocation.Text & "\" & userName & "\" & "upload") Then
                    Directory.CreateDirectory(ConfigurationManager.AppSettings("RootDir") & "\" & DropListLocation.Text & "\" & userName & "\" & "upload")
                End If
                'Assign File Name
                Dim dirPath As String = ConfigurationManager.AppSettings("RootDir") & "\" & DropListLocation.Text & "\" & userName & "\" & "upload"
                Dim fileName As String = strFileName & "_" & DateTime.Now.ToString("yyyyMMddHHmmssfff") & strExtension
                Dim filenameFullPath As String = dirPath & "\" & fileName
                'Save the file in temporary folder
                FileUploadAnalysis.SaveAs(filenameFullPath)
                'Update to Database
                Item104(filenameFullPath, fileName, userid, userName, strSrcFileName)
            ElseIf drpTask.SelectedValue = "105" Then
                'Item103. Stock Report
                '*********************************
                'Verify FileName Rules Followed
                Dim strFileName As String = System.IO.Path.GetFileNameWithoutExtension(FileUploadAnalysis.FileName)
                Dim strSrcFileName As String = System.IO.Path.GetFileName(FileUploadAnalysis.FileName)
                'Verify that file name must more than 6 characters
                If Len(strFileName) < 13 Then
                    Call showMsg("The csv file name is invalid ", "")
                    Exit Sub
                End If
                'Split the branch name in File Name
                PositionStart = InStr(1, strSrcFileName, "_")
                If PositionStart = 0 Then ' There is no _ symbol in the file name
                    Call showMsg("The csv file name is Invalid", "")
                    Exit Sub
                Else
                    strBranchName = Left(strSrcFileName, PositionStart - 1)
                    If Not (UCase(DropListLocation.SelectedItem.Text) = UCase(strBranchName)) Then
                        Call showMsg("The selected branch and file name is not matched", "")
                        Exit Sub
                    End If
                End If
                'Middle ID in file name
                PositionStart = InStr(1, strSrcFileName, "_")
                If PositionStart = 0 Then ' There is no _ symbol in the file name
                    Call showMsg("The csv file name is invalid ", "")
                    Exit Sub
                Else
                    strTmp = Right(strSrcFileName, Len(strSrcFileName) - PositionStart)
                    strTmp = Left(strTmp, 2)
                    If (UCase(strTmp) = "SR") Then
                    Else
                        Call showMsg("The csv file name is invalid", "")
                        Exit Sub
                    End If
                End If


                Dim strDay As String = Right(strFileName, 2)
                Dim strMonth As String = Mid(strFileName, Len(strFileName) - 3, 2)
                Dim strYear As String = Mid(strFileName, Len(strFileName) - 7, 4)

                'Verify integer
                Dim numChk As Integer = 0
                If Integer.TryParse(strDay, numChk) = False Then
                    Call showMsg("The csv file name is invalid ", "")
                    Exit Sub
                End If
                ''Allowed 01/11/21 
                'If Not (numChk = 1 Or numChk = 11 Or numChk = 21) Then
                '    Call showMsg("The csv file name is invalid ", "")
                '    Exit Sub
                'End If

                If Integer.TryParse(strMonth, numChk) = False Then
                    Call showMsg("The csv file name is invalid ", "")
                    Exit Sub
                End If
                'Maximum Allowed 12 Months
                If numChk > 12 Then
                    Call showMsg("The csv file name is invalid ", "")
                    Exit Sub
                End If
                If Integer.TryParse(strYear, numChk) = False Then
                    Call showMsg("The csv file name is invalid ", "")
                    Exit Sub
                End If
                'Allowed Greater than 2018
                If numChk < 2018 Then
                    Call showMsg("The csv file name is invalid ", "")
                    Exit Sub
                End If
                'Move the file to temporary folder
                If Not Directory.Exists(ConfigurationManager.AppSettings("RootDir") & "\" & DropListLocation.Text & "\" & userName & "\" & "upload") Then
                    Directory.CreateDirectory(ConfigurationManager.AppSettings("RootDir") & "\" & DropListLocation.Text & "\" & userName & "\" & "upload")
                End If
                'Assign File Name
                Dim dirPath As String = ConfigurationManager.AppSettings("RootDir") & "\" & DropListLocation.Text & "\" & userName & "\" & "upload"
                Dim fileName As String = strFileName & "_" & DateTime.Now.ToString("yyyyMMddHHmmssfff") & strExtension
                Dim filenameFullPath As String = dirPath & "\" & fileName
                'Save the file in temporary folder
                FileUploadAnalysis.SaveAs(filenameFullPath)
                'Update to Database
                Item105(filenameFullPath, fileName, userid, userName, strSrcFileName)
            ElseIf drpTask.SelectedValue = "106" Then
                'Item106. SONY_B2B_DATE_WISE_SALES
                '*********************************
                'Verify FileName Rules Followed
                Dim strFileName As String = System.IO.Path.GetFileNameWithoutExtension(FileUploadAnalysis.FileName)
                Dim strSrcFileName As String = System.IO.Path.GetFileName(FileUploadAnalysis.FileName)
                'Verify that file name must more than 6 characters
                If Len(strFileName) < 13 Then
                    Call showMsg("The csv file name is invalid ", "")
                    Exit Sub
                End If
                'Split the branch name in File Name
                PositionStart = InStr(1, strSrcFileName, "_")
                If PositionStart = 0 Then ' There is no _ symbol in the file name
                    Call showMsg("The csv file name is Invalid", "")
                    Exit Sub
                Else
                    strBranchName = Left(strSrcFileName, PositionStart - 1)
                    If Not (UCase(DropListLocation.SelectedItem.Text) = UCase(strBranchName)) Then
                        Call showMsg("The selected branch and file name is not matched", "")
                        Exit Sub
                    End If
                End If
                'Middle ID in file name
                PositionStart = InStr(1, strSrcFileName, "_")
                If PositionStart = 0 Then ' There is no _ symbol in the file name
                    Call showMsg("The csv file name is invalid ", "")
                    Exit Sub
                Else
                    strTmp = Right(strSrcFileName, Len(strSrcFileName) - PositionStart)
                    strTmp = Left(strTmp, 2)
                    If (UCase(strTmp) = "SR") Then
                    Else
                        Call showMsg("The csv file name is invalid", "")
                        Exit Sub
                    End If
                End If


                Dim strDay As String = Right(strFileName, 2)
                Dim strMonth As String = Mid(strFileName, Len(strFileName) - 3, 2)
                Dim strYear As String = Mid(strFileName, Len(strFileName) - 7, 4)

                'Verify integer
                Dim numChk As Integer = 0
                If Integer.TryParse(strDay, numChk) = False Then
                    Call showMsg("The csv file name is invalid ", "")
                    Exit Sub
                End If
                ''Allowed 01/11/21 
                'If Not (numChk = 1 Or numChk = 11 Or numChk = 21) Then
                '    Call showMsg("The csv file name is invalid ", "")
                '    Exit Sub
                'End If

                If Integer.TryParse(strMonth, numChk) = False Then
                    Call showMsg("The csv file name is invalid ", "")
                    Exit Sub
                End If
                'Maximum Allowed 12 Months
                If numChk > 12 Then
                    Call showMsg("The csv file name is invalid ", "")
                    Exit Sub
                End If
                If Integer.TryParse(strYear, numChk) = False Then
                    Call showMsg("The csv file name is invalid ", "")
                    Exit Sub
                End If
                'Allowed Greater than 2018
                If numChk < 2018 Then
                    Call showMsg("The csv file name is invalid ", "")
                    Exit Sub
                End If
                'Move the file to temporary folder
                If Not Directory.Exists(ConfigurationManager.AppSettings("RootDir") & "\" & DropListLocation.Text & "\" & userName & "\" & "upload") Then
                    Directory.CreateDirectory(ConfigurationManager.AppSettings("RootDir") & "\" & DropListLocation.Text & "\" & userName & "\" & "upload")
                End If
                'Assign File Name
                Dim dirPath As String = ConfigurationManager.AppSettings("RootDir") & "\" & DropListLocation.Text & "\" & userName & "\" & "upload"
                Dim fileName As String = strFileName & "_" & DateTime.Now.ToString("yyyyMMddHHmmssfff") & strExtension
                Dim filenameFullPath As String = dirPath & "\" & fileName
                'Save the file in temporary folder
                FileUploadAnalysis.SaveAs(filenameFullPath)
                'Update to Database
                Item106(filenameFullPath, fileName, userid, userName, strSrcFileName)
            ElseIf drpTask.SelectedValue = "107" Then
                'Item107. SONY_B2B_STOCK_AVAILABLE
                '*********************************
                'Verify FileName Rules Followed
                Dim strFileName As String = System.IO.Path.GetFileNameWithoutExtension(FileUploadAnalysis.FileName)
                Dim strSrcFileName As String = System.IO.Path.GetFileName(FileUploadAnalysis.FileName)
                'Verify that file name must more than 6 characters
                If Len(strFileName) < 13 Then
                    Call showMsg("The csv file name is invalid ", "")
                    Exit Sub
                End If
                'Split the branch name in File Name
                PositionStart = InStr(1, strSrcFileName, "_")
                If PositionStart = 0 Then ' There is no _ symbol in the file name
                    Call showMsg("The csv file name is Invalid", "")
                    Exit Sub
                Else
                    strBranchName = Left(strSrcFileName, PositionStart - 1)
                    If Not (UCase(DropListLocation.SelectedItem.Text) = UCase(strBranchName)) Then
                        Call showMsg("The selected branch and file name is not matched", "")
                        Exit Sub
                    End If
                End If
                'Middle ID in file name
                PositionStart = InStr(1, strSrcFileName, "_")
                If PositionStart = 0 Then ' There is no _ symbol in the file name
                    Call showMsg("The csv file name is invalid ", "")
                    Exit Sub
                Else
                    strTmp = Right(strSrcFileName, Len(strSrcFileName) - PositionStart)
                    strTmp = Left(strTmp, 2)
                    If (UCase(strTmp) = "SA") Then
                    Else
                        Call showMsg("The csv file name is invalid", "")
                        Exit Sub
                    End If
                End If


                Dim strDay As String = Right(strFileName, 2)
                Dim strMonth As String = Mid(strFileName, Len(strFileName) - 3, 2)
                Dim strYear As String = Mid(strFileName, Len(strFileName) - 7, 4)

                'Verify integer
                Dim numChk As Integer = 0
                If Integer.TryParse(strDay, numChk) = False Then
                    Call showMsg("The csv file name is invalid ", "")
                    Exit Sub
                End If
                ''Allowed 01/11/21 
                'If Not (numChk = 1 Or numChk = 11 Or numChk = 21) Then
                '    Call showMsg("The csv file name is invalid ", "")
                '    Exit Sub
                'End If

                If Integer.TryParse(strMonth, numChk) = False Then
                    Call showMsg("The csv file name is invalid ", "")
                    Exit Sub
                End If
                'Maximum Allowed 12 Months
                If numChk > 12 Then
                    Call showMsg("The csv file name is invalid ", "")
                    Exit Sub
                End If
                If Integer.TryParse(strYear, numChk) = False Then
                    Call showMsg("The csv file name is invalid ", "")
                    Exit Sub
                End If
                'Allowed Greater than 2018
                If numChk < 2018 Then
                    Call showMsg("The csv file name is invalid ", "")
                    Exit Sub
                End If
                'Move the file to temporary folder
                If Not Directory.Exists(ConfigurationManager.AppSettings("RootDir") & "\" & DropListLocation.Text & "\" & userName & "\" & "upload") Then
                    Directory.CreateDirectory(ConfigurationManager.AppSettings("RootDir") & "\" & DropListLocation.Text & "\" & userName & "\" & "upload")
                End If
                'Assign File Name
                Dim dirPath As String = ConfigurationManager.AppSettings("RootDir") & "\" & DropListLocation.Text & "\" & userName & "\" & "upload"
                Dim fileName As String = strFileName & "_" & DateTime.Now.ToString("yyyyMMddHHmmssfff") & strExtension
                Dim filenameFullPath As String = dirPath & "\" & fileName
                'Save the file in temporary folder
                FileUploadAnalysis.SaveAs(filenameFullPath)
                'Update to Database
                Item107(filenameFullPath, fileName, userid, userName, strSrcFileName)
            ElseIf drpTask.SelectedValue = "108" Then
                'Item108. AscGstTaxReport
                '*********************************
                'Verify FileName Rules Followed
                Dim strFileName As String = System.IO.Path.GetFileNameWithoutExtension(FileUploadAnalysis.FileName)
                Dim strSrcFileName As String = System.IO.Path.GetFileName(FileUploadAnalysis.FileName)
                'Verify that file name must more than 6 characters
                If Len(strFileName) < 13 Then
                    Call showMsg("The csv file name is invalid ", "")
                    Exit Sub
                End If
                'Split the branch name in File Name
                PositionStart = InStr(1, strSrcFileName, "_")
                If PositionStart = 0 Then ' There is no _ symbol in the file name
                    Call showMsg("The csv file name is Invalid", "")
                    Exit Sub
                Else
                    strBranchName = Left(strSrcFileName, PositionStart - 1)
                    If Not (UCase(DropListLocation.SelectedItem.Text) = UCase(strBranchName)) Then
                        Call showMsg("The selected branch and file name is not matched", "")
                        Exit Sub
                    End If
                End If
                'Middle ID in file name
                PositionStart = InStr(1, strSrcFileName, "_")
                If PositionStart = 0 Then ' There is no _ symbol in the file name
                    Call showMsg("The csv file name is invalid ", "")
                    Exit Sub
                Else
                    strTmp = Right(strSrcFileName, Len(strSrcFileName) - PositionStart)
                    strTmp = Left(strTmp, 2)
                    If (UCase(strTmp) = "AT") Then
                    Else
                        Call showMsg("The csv file name is invalid", "")
                        Exit Sub
                    End If
                End If


                Dim strDay As String = Right(strFileName, 2)
                Dim strMonth As String = Mid(strFileName, Len(strFileName) - 3, 2)
                Dim strYear As String = Mid(strFileName, Len(strFileName) - 7, 4)

                'Verify integer
                Dim numChk As Integer = 0
                If Integer.TryParse(strDay, numChk) = False Then
                    Call showMsg("The csv file name is invalid ", "")
                    Exit Sub
                End If
                ''Allowed 01/11/21 
                'If Not (numChk = 1 Or numChk = 11 Or numChk = 21) Then
                '    Call showMsg("The csv file name is invalid ", "")
                '    Exit Sub
                'End If

                If Integer.TryParse(strMonth, numChk) = False Then
                    Call showMsg("The csv file name is invalid ", "")
                    Exit Sub
                End If
                'Maximum Allowed 12 Months
                If numChk > 12 Then
                    Call showMsg("The csv file name is invalid ", "")
                    Exit Sub
                End If
                If Integer.TryParse(strYear, numChk) = False Then
                    Call showMsg("The csv file name is invalid ", "")
                    Exit Sub
                End If
                'Allowed Greater than 2018
                If numChk < 2018 Then
                    Call showMsg("The csv file name is invalid ", "")
                    Exit Sub
                End If
                'Move the file to temporary folder
                If Not Directory.Exists(ConfigurationManager.AppSettings("RootDir") & "\" & DropListLocation.Text & "\" & userName & "\" & "upload") Then
                    Directory.CreateDirectory(ConfigurationManager.AppSettings("RootDir") & "\" & DropListLocation.Text & "\" & userName & "\" & "upload")
                End If
                'Assign File Name
                Dim dirPath As String = ConfigurationManager.AppSettings("RootDir") & "\" & DropListLocation.Text & "\" & userName & "\" & "upload"
                Dim fileName As String = strFileName & "_" & DateTime.Now.ToString("yyyyMMddHHmmssfff") & strExtension
                Dim filenameFullPath As String = dirPath & "\" & fileName
                'Save the file in temporary folder
                FileUploadAnalysis.SaveAs(filenameFullPath)
                'Update to Database
                Item108(filenameFullPath, fileName, userid, userName, strSrcFileName)
            ElseIf drpTask.SelectedValue = "109" Then
                'Item109. ASCTaxReport
                '*********************************
                'Verify FileName Rules Followed
                Dim strFileName As String = System.IO.Path.GetFileNameWithoutExtension(FileUploadAnalysis.FileName)
                Dim strSrcFileName As String = System.IO.Path.GetFileName(FileUploadAnalysis.FileName)
                'Verify that file name must more than 6 characters
                If Len(strFileName) < 13 Then
                    Call showMsg("The csv file name is invalid ", "")
                    Exit Sub
                End If
                'Split the branch name in File Name
                PositionStart = InStr(1, strSrcFileName, "_")
                If PositionStart = 0 Then ' There is no _ symbol in the file name
                    Call showMsg("The csv file name is Invalid", "")
                    Exit Sub
                Else
                    strBranchName = Left(strSrcFileName, PositionStart - 1)
                    If Not (UCase(DropListLocation.SelectedItem.Text) = UCase(strBranchName)) Then
                        Call showMsg("The selected branch and file name is not matched", "")
                        Exit Sub
                    End If
                End If
                'Middle ID in file name
                PositionStart = InStr(1, strSrcFileName, "_")
                If PositionStart = 0 Then ' There is no _ symbol in the file name
                    Call showMsg("The csv file name is invalid ", "")
                    Exit Sub
                Else
                    strTmp = Right(strSrcFileName, Len(strSrcFileName) - PositionStart)
                    strTmp = Left(strTmp, 2)
                    If (UCase(strTmp) = "AT") Then
                    Else
                        Call showMsg("The csv file name is invalid", "")
                        Exit Sub
                    End If
                End If


                Dim strDay As String = Right(strFileName, 2)
                Dim strMonth As String = Mid(strFileName, Len(strFileName) - 3, 2)
                Dim strYear As String = Mid(strFileName, Len(strFileName) - 7, 4)

                'Verify integer
                Dim numChk As Integer = 0
                If Integer.TryParse(strDay, numChk) = False Then
                    Call showMsg("The csv file name is invalid ", "")
                    Exit Sub
                End If
                ''Allowed 01/11/21 
                'If Not (numChk = 1 Or numChk = 11 Or numChk = 21) Then
                '    Call showMsg("The csv file name is invalid ", "")
                '    Exit Sub
                'End If

                If Integer.TryParse(strMonth, numChk) = False Then
                    Call showMsg("The csv file name is invalid ", "")
                    Exit Sub
                End If
                'Maximum Allowed 12 Months
                If numChk > 12 Then
                    Call showMsg("The csv file name is invalid ", "")
                    Exit Sub
                End If
                If Integer.TryParse(strYear, numChk) = False Then
                    Call showMsg("The csv file name is invalid ", "")
                    Exit Sub
                End If
                'Allowed Greater than 2018
                If numChk < 2018 Then
                    Call showMsg("The csv file name is invalid ", "")
                    Exit Sub
                End If
                'Move the file to temporary folder
                If Not Directory.Exists(ConfigurationManager.AppSettings("RootDir") & "\" & DropListLocation.Text & "\" & userName & "\" & "upload") Then
                    Directory.CreateDirectory(ConfigurationManager.AppSettings("RootDir") & "\" & DropListLocation.Text & "\" & userName & "\" & "upload")
                End If
                'Assign File Name
                Dim dirPath As String = ConfigurationManager.AppSettings("RootDir") & "\" & DropListLocation.Text & "\" & userName & "\" & "upload"
                Dim fileName As String = strFileName & "_" & DateTime.Now.ToString("yyyyMMddHHmmssfff") & strExtension
                Dim filenameFullPath As String = dirPath & "\" & fileName
                'Save the file in temporary folder
                FileUploadAnalysis.SaveAs(filenameFullPath)
                'Update to Database
                Item109(filenameFullPath, fileName, userid, userName, strSrcFileName)
            ElseIf drpTask.SelectedValue = "110" Then
                'Item110. RMS ASC_Tax_Report
                '*********************************
                'Verify FileName Rules Followed
                Dim strFileName As String = System.IO.Path.GetFileNameWithoutExtension(FileUploadAnalysis.FileName)
                Dim strSrcFileName As String = System.IO.Path.GetFileName(FileUploadAnalysis.FileName)
                'Verify that file name must more than 6 characters
                If Len(strFileName) < 13 Then
                    Call showMsg("The csv file name is invalid ", "")
                    Exit Sub
                End If
                'Split the branch name in File Name
                PositionStart = InStr(1, strSrcFileName, "_")
                If PositionStart = 0 Then ' There is no _ symbol in the file name
                    Call showMsg("The csv file name is Invalid", "")
                    Exit Sub
                Else
                    strBranchName = Left(strSrcFileName, PositionStart - 1)
                    If Not (UCase(DropListLocation.SelectedItem.Text) = UCase(strBranchName)) Then
                        Call showMsg("The selected branch and file name is not matched", "")
                        Exit Sub
                    End If
                End If
                'Middle ID in file name
                PositionStart = InStr(1, strSrcFileName, "_")
                If PositionStart = 0 Then ' There is no _ symbol in the file name
                    Call showMsg("The csv file name is invalid ", "")
                    Exit Sub
                Else
                    strTmp = Right(strSrcFileName, Len(strSrcFileName) - PositionStart)
                    strTmp = Left(strTmp, 2)
                    If (UCase(strTmp) = "RA") Then
                    Else
                        Call showMsg("The csv file name is invalid", "")
                        Exit Sub
                    End If
                End If


                Dim strDay As String = Right(strFileName, 2)
                Dim strMonth As String = Mid(strFileName, Len(strFileName) - 3, 2)
                Dim strYear As String = Mid(strFileName, Len(strFileName) - 7, 4)

                'Verify integer
                Dim numChk As Integer = 0
                If Integer.TryParse(strDay, numChk) = False Then
                    Call showMsg("The csv file name is invalid ", "")
                    Exit Sub
                End If
                ''Allowed 01/11/21 
                'If Not (numChk = 1 Or numChk = 11 Or numChk = 21) Then
                '    Call showMsg("The csv file name is invalid ", "")
                '    Exit Sub
                'End If

                If Integer.TryParse(strMonth, numChk) = False Then
                    Call showMsg("The csv file name is invalid ", "")
                    Exit Sub
                End If
                'Maximum Allowed 12 Months
                If numChk > 12 Then
                    Call showMsg("The csv file name is invalid ", "")
                    Exit Sub
                End If
                If Integer.TryParse(strYear, numChk) = False Then
                    Call showMsg("The csv file name is invalid ", "")
                    Exit Sub
                End If
                'Allowed Greater than 2018
                If numChk < 2018 Then
                    Call showMsg("The csv file name is invalid ", "")
                    Exit Sub
                End If
                'Move the file to temporary folder
                If Not Directory.Exists(ConfigurationManager.AppSettings("RootDir") & "\" & DropListLocation.Text & "\" & userName & "\" & "upload") Then
                    Directory.CreateDirectory(ConfigurationManager.AppSettings("RootDir") & "\" & DropListLocation.Text & "\" & userName & "\" & "upload")
                End If
                'Assign File Name
                Dim dirPath As String = ConfigurationManager.AppSettings("RootDir") & "\" & DropListLocation.Text & "\" & userName & "\" & "upload"
                Dim fileName As String = strFileName & "_" & DateTime.Now.ToString("yyyyMMddHHmmssfff") & strExtension
                Dim filenameFullPath As String = dirPath & "\" & fileName
                'Save the file in temporary folder
                FileUploadAnalysis.SaveAs(filenameFullPath)
                'Update to Database
                Item110(filenameFullPath, fileName, userid, userName, strSrcFileName)
            ElseIf drpTask.SelectedValue = "111" Then
                'Item111. ClaimInvoiceDetail
                '*********************************
                'Verify FileName Rules Followed
                Dim strFileName As String = System.IO.Path.GetFileNameWithoutExtension(FileUploadAnalysis.FileName)
                Dim strSrcFileName As String = System.IO.Path.GetFileName(FileUploadAnalysis.FileName)
                'Verify that file name must more than 6 characters
                If Len(strFileName) < 13 Then
                    Call showMsg("The csv file name is invalid ", "")
                    Exit Sub
                End If
                'Split the branch name in File Name
                PositionStart = InStr(1, strSrcFileName, "_")
                If PositionStart = 0 Then ' There is no _ symbol in the file name
                    Call showMsg("The csv file name is Invalid", "")
                    Exit Sub
                Else
                    strBranchName = Left(strSrcFileName, PositionStart - 1)
                    If Not (UCase(DropListLocation.SelectedItem.Text) = UCase(strBranchName)) Then
                        Call showMsg("The selected branch and file name is not matched", "")
                        Exit Sub
                    End If
                End If
                'Middle ID in file name
                PositionStart = InStr(1, strSrcFileName, "_")
                If PositionStart = 0 Then ' There is no _ symbol in the file name
                    Call showMsg("The csv file name is invalid ", "")
                    Exit Sub
                Else
                    strTmp = Right(strSrcFileName, Len(strSrcFileName) - PositionStart)
                    strTmp = Left(strTmp, 2)
                    If (UCase(strTmp) = "RC") Then
                    Else
                        Call showMsg("The csv file name is invalid", "")
                        Exit Sub
                    End If
                End If


                Dim strDay As String = Right(strFileName, 2)
                Dim strMonth As String = Mid(strFileName, Len(strFileName) - 3, 2)
                Dim strYear As String = Mid(strFileName, Len(strFileName) - 7, 4)

                'Verify integer
                Dim numChk As Integer = 0
                If Integer.TryParse(strDay, numChk) = False Then
                    Call showMsg("The csv file name is invalid ", "")
                    Exit Sub
                End If
                ''Allowed 01/11/21 
                'If Not (numChk = 1 Or numChk = 11 Or numChk = 21) Then
                '    Call showMsg("The csv file name is invalid ", "")
                '    Exit Sub
                'End If

                If Integer.TryParse(strMonth, numChk) = False Then
                    Call showMsg("The csv file name is invalid ", "")
                    Exit Sub
                End If
                'Maximum Allowed 12 Months
                If numChk > 12 Then
                    Call showMsg("The csv file name is invalid ", "")
                    Exit Sub
                End If
                If Integer.TryParse(strYear, numChk) = False Then
                    Call showMsg("The csv file name is invalid ", "")
                    Exit Sub
                End If
                'Allowed Greater than 2018
                If numChk < 2018 Then
                    Call showMsg("The csv file name is invalid ", "")
                    Exit Sub
                End If
                'Move the file to temporary folder
                If Not Directory.Exists(ConfigurationManager.AppSettings("RootDir") & "\" & DropListLocation.Text & "\" & userName & "\" & "upload") Then
                    Directory.CreateDirectory(ConfigurationManager.AppSettings("RootDir") & "\" & DropListLocation.Text & "\" & userName & "\" & "upload")
                End If
                'Assign File Name
                Dim dirPath As String = ConfigurationManager.AppSettings("RootDir") & "\" & DropListLocation.Text & "\" & userName & "\" & "upload"
                Dim fileName As String = strFileName & "_" & DateTime.Now.ToString("yyyyMMddHHmmssfff") & strExtension
                Dim filenameFullPath As String = dirPath & "\" & fileName
                'Save the file in temporary folder
                FileUploadAnalysis.SaveAs(filenameFullPath)
                'Update to Database
                Item111(filenameFullPath, fileName, userid, userName, strSrcFileName)

            ElseIf drpTask.SelectedValue = "112" Then
                'Item112 ASC Invoice Data
                '*********************************
                'Verify FileName Rules Followed
                Dim strFileName As String = System.IO.Path.GetFileNameWithoutExtension(FileUploadAnalysis.FileName)
                Dim strSrcFileName As String = System.IO.Path.GetFileName(FileUploadAnalysis.FileName)
                'Verify that file name must more than 6 characters
                If Len(strFileName) < 13 Then
                    Call showMsg("The csv file name is invalid ", "")
                    Exit Sub
                End If
                'Split the branch name in File Name
                PositionStart = InStr(1, strSrcFileName, "_")
                If PositionStart = 0 Then ' There is no _ symbol in the file name
                    Call showMsg("The csv file name is Invalid", "")
                    Exit Sub
                Else
                    strBranchName = Left(strSrcFileName, PositionStart - 1)
                    If Not (UCase(DropListLocation.SelectedItem.Text) = UCase(strBranchName)) Then
                        Call showMsg("The selected branch and file name is not matched", "")
                        Exit Sub
                    End If
                End If
                'Middle ID in file name
                PositionStart = InStr(1, strSrcFileName, "_")
                If PositionStart = 0 Then ' There is no _ symbol in the file name
                    Call showMsg("The csv file name is invalid ", "")
                    Exit Sub
                Else
                    strTmp = Right(strSrcFileName, Len(strSrcFileName) - PositionStart)
                    strTmp = Left(strTmp, 2)
                    If (UCase(strTmp) = "AI") Then
                    Else
                        Call showMsg("The csv file name is invalid", "")
                        Exit Sub
                    End If
                End If


                Dim strDay As String = Right(strFileName, 2)
                Dim strMonth As String = Mid(strFileName, Len(strFileName) - 3, 2)
                Dim strYear As String = Mid(strFileName, Len(strFileName) - 7, 4)

                'Verify integer
                Dim numChk As Integer = 0
                If Integer.TryParse(strDay, numChk) = False Then
                    Call showMsg("The csv file name is invalid ", "")
                    Exit Sub
                End If
                ''Allowed 01/11/21 
                'If Not (numChk = 1 Or numChk = 11 Or numChk = 21) Then
                '    Call showMsg("The csv file name is invalid ", "")
                '    Exit Sub
                'End If

                If Integer.TryParse(strMonth, numChk) = False Then
                    Call showMsg("The csv file name is invalid ", "")
                    Exit Sub
                End If
                'Maximum Allowed 12 Months
                If numChk > 12 Then
                    Call showMsg("The csv file name is invalid ", "")
                    Exit Sub
                End If
                If Integer.TryParse(strYear, numChk) = False Then
                    Call showMsg("The csv file name is invalid ", "")
                    Exit Sub
                End If
                'Allowed Greater than 2018
                If numChk < 2018 Then
                    Call showMsg("The csv file name is invalid ", "")
                    Exit Sub
                End If
                'Move the file to temporary folder
                If Not Directory.Exists(ConfigurationManager.AppSettings("RootDir") & "\" & DropListLocation.Text & "\" & userName & "\" & "upload") Then
                    Directory.CreateDirectory(ConfigurationManager.AppSettings("RootDir") & "\" & DropListLocation.Text & "\" & userName & "\" & "upload")
                End If
                'Assign File Name
                Dim dirPath As String = ConfigurationManager.AppSettings("RootDir") & "\" & DropListLocation.Text & "\" & userName & "\" & "upload"
                Dim fileName As String = strFileName & "_" & DateTime.Now.ToString("yyyyMMddHHmmssfff") & strExtension
                Dim filenameFullPath As String = dirPath & "\" & fileName
                'Save the file in temporary folder
                FileUploadAnalysis.SaveAs(filenameFullPath)
                'Update to Database
                Item112(filenameFullPath, fileName, userid, userName, strSrcFileName)

                'Added for Daily Abandon

            ElseIf drpTask.SelectedValue = "113" Then
                'Item113. Daily Abandon
                '*********************************
                'Verify FileName Rules Followed
                Dim strFileName As String = System.IO.Path.GetFileNameWithoutExtension(FileUploadAnalysis.FileName)
                Dim strSrcFileName As String = System.IO.Path.GetFileName(FileUploadAnalysis.FileName)
                'Verify that file name must more than 6 characters
                If Len(strFileName) < 13 Then
                    Call showMsg("The csv file name is invalid ", "")
                    Exit Sub
                End If
                'Split the branch name in File Name
                PositionStart = InStr(1, strSrcFileName, "_")
                If PositionStart = 0 Then ' There is no _ symbol in the file name
                    Call showMsg("The csv file name is Invalid", "")
                    Exit Sub
                Else
                    strBranchName = Left(strSrcFileName, PositionStart - 1)
                    If Not (UCase(DropListLocation.SelectedItem.Text) = UCase(strBranchName)) Then
                        Call showMsg("The selected branch and file name is not matched", "")
                        Exit Sub
                    End If
                End If
                'Middle ID in file name
                PositionStart = InStr(1, strSrcFileName, "_")
                If PositionStart = 0 Then ' There is no _ symbol in the file name
                    Call showMsg("The csv file name is invalid ", "")
                    Exit Sub
                Else
                    strTmp = Right(strSrcFileName, Len(strSrcFileName) - PositionStart)
                    strTmp = Left(strTmp, 2)
                    If (UCase(strTmp) = "DA") Then
                    Else
                        Call showMsg("The csv file name is invalid", "")
                        Exit Sub
                    End If
                End If


                Dim strDay As String = Right(strFileName, 2)
                Dim strMonth As String = Mid(strFileName, Len(strFileName) - 3, 2)
                Dim strYear As String = Mid(strFileName, Len(strFileName) - 7, 4)

                'Verify integer
                Dim numChk As Integer = 0
                If Integer.TryParse(strDay, numChk) = False Then
                    Call showMsg("The csv file name is invalid ", "")
                    Exit Sub
                End If
                ''Allowed 01/11/21 
                'If Not (numChk = 1 Or numChk = 11 Or numChk = 21) Then
                '    Call showMsg("The csv file name is invalid ", "")
                '    Exit Sub
                'End If

                If Integer.TryParse(strMonth, numChk) = False Then
                    Call showMsg("The csv file name is invalid ", "")
                    Exit Sub
                End If
                'Maximum Allowed 12 Months
                If numChk > 12 Then
                    Call showMsg("The csv file name is invalid ", "")
                    Exit Sub
                End If
                If Integer.TryParse(strYear, numChk) = False Then
                    Call showMsg("The csv file name is invalid ", "")
                    Exit Sub
                End If
                'Allowed Greater than 2018
                If numChk < 2018 Then
                    Call showMsg("The csv file name is invalid ", "")
                    Exit Sub
                End If
                'Move the file to temporary folder
                If Not Directory.Exists(ConfigurationManager.AppSettings("RootDir") & "\" & DropListLocation.Text & "\" & userName & "\" & "upload") Then
                    Directory.CreateDirectory(ConfigurationManager.AppSettings("RootDir") & "\" & DropListLocation.Text & "\" & userName & "\" & "upload")
                End If
                'Assign File Name
                Dim dirPath As String = ConfigurationManager.AppSettings("RootDir") & "\" & DropListLocation.Text & "\" & userName & "\" & "upload"
                Dim fileName As String = strFileName & "_" & DateTime.Now.ToString("yyyyMMddHHmmssfff") & strExtension
                Dim filenameFullPath As String = dirPath & "\" & fileName
                'Save the file in temporary folder
                FileUploadAnalysis.SaveAs(filenameFullPath)
                'Update to Database
                Item113(filenameFullPath, fileName, userid, userName, strSrcFileName)

                'Added for SonyDailyAccKPIRpt
            ElseIf drpTask.SelectedValue = "114" Then
                'Item113. Daily Abandon
                '*********************************
                'Verify FileName Rules Followed
                Dim strFileName As String = System.IO.Path.GetFileNameWithoutExtension(FileUploadAnalysis.FileName)
                Dim strSrcFileName As String = System.IO.Path.GetFileName(FileUploadAnalysis.FileName)
                'Verify that file name must more than 6 characters
                If Len(strFileName) < 13 Then
                    Call showMsg("The csv file name is invalid ", "")
                    Exit Sub
                End If
                'Split the branch name in File Name
                PositionStart = InStr(1, strSrcFileName, "_")
                If PositionStart = 0 Then ' There is no _ symbol in the file name
                    Call showMsg("The csv file name is Invalid", "")
                    Exit Sub
                Else
                    strBranchName = Left(strSrcFileName, PositionStart - 1)
                    If Not (UCase(DropListLocation.SelectedItem.Text) = UCase(strBranchName)) Then
                        Call showMsg("The selected branch and file name is not matched", "")
                        Exit Sub
                    End If
                End If
                'Middle ID in file name
                PositionStart = InStr(1, strSrcFileName, "_")
                If PositionStart = 0 Then ' There is no _ symbol in the file name
                    Call showMsg("The csv file name is invalid ", "")
                    Exit Sub
                Else
                    strTmp = Right(strSrcFileName, Len(strSrcFileName) - PositionStart)
                    strTmp = Left(strTmp, 2)
                    If (UCase(strTmp) = "DA") Then
                    Else
                        Call showMsg("The csv file name is invalid", "")
                        Exit Sub
                    End If
                End If


                Dim strDay As String = Right(strFileName, 2)
                Dim strMonth As String = Mid(strFileName, Len(strFileName) - 3, 2)
                Dim strYear As String = Mid(strFileName, Len(strFileName) - 7, 4)

                'Verify integer
                Dim numChk As Integer = 0
                If Integer.TryParse(strDay, numChk) = False Then
                    Call showMsg("The csv file name is invalid ", "")
                    Exit Sub
                End If
                ''Allowed 01/11/21 
                'If Not (numChk = 1 Or numChk = 11 Or numChk = 21) Then
                '    Call showMsg("The csv file name is invalid ", "")
                '    Exit Sub
                'End If

                If Integer.TryParse(strMonth, numChk) = False Then
                    Call showMsg("The csv file name is invalid ", "")
                    Exit Sub
                End If
                'Maximum Allowed 12 Months
                If numChk > 12 Then
                    Call showMsg("The csv file name is invalid ", "")
                    Exit Sub
                End If
                If Integer.TryParse(strYear, numChk) = False Then
                    Call showMsg("The csv file name is invalid ", "")
                    Exit Sub
                End If
                'Allowed Greater than 2018
                If numChk < 2018 Then
                    Call showMsg("The csv file name is invalid ", "")
                    Exit Sub
                End If
                'Move the file to temporary folder
                If Not Directory.Exists(ConfigurationManager.AppSettings("RootDir") & "\" & DropListLocation.Text & "\" & userName & "\" & "upload") Then
                    Directory.CreateDirectory(ConfigurationManager.AppSettings("RootDir") & "\" & DropListLocation.Text & "\" & userName & "\" & "upload")
                End If
                'Assign File Name
                Dim dirPath As String = ConfigurationManager.AppSettings("RootDir") & "\" & DropListLocation.Text & "\" & userName & "\" & "upload"
                Dim fileName As String = strFileName & "_" & DateTime.Now.ToString("yyyyMMddHHmmssfff") & strExtension
                Dim filenameFullPath As String = dirPath & "\" & fileName
                'Save the file in temporary folder
                FileUploadAnalysis.SaveAs(filenameFullPath)
                'Update to Database
                Item114(filenameFullPath, fileName, userid, userName, strSrcFileName)

                'Added for SonyDaily_ASCPartsUsed
            ElseIf drpTask.SelectedValue = "115" Then
                'Item113. Daily Abandon
                '*********************************
                'Verify FileName Rules Followed
                Dim strFileName As String = System.IO.Path.GetFileNameWithoutExtension(FileUploadAnalysis.FileName)
                Dim strSrcFileName As String = System.IO.Path.GetFileName(FileUploadAnalysis.FileName)
                'Verify that file name must more than 6 characters
                If Len(strFileName) < 13 Then
                    Call showMsg("The csv file name is invalid ", "")
                    Exit Sub
                End If
                'Split the branch name in File Name
                PositionStart = InStr(1, strSrcFileName, "_")
                If PositionStart = 0 Then ' There is no _ symbol in the file name
                    Call showMsg("The csv file name is Invalid", "")
                    Exit Sub
                Else
                    strBranchName = Left(strSrcFileName, PositionStart - 1)
                    If Not (UCase(DropListLocation.SelectedItem.Text) = UCase(strBranchName)) Then
                        Call showMsg("The selected branch and file name is not matched", "")
                        Exit Sub
                    End If
                End If
                'Middle ID in file name
                PositionStart = InStr(1, strSrcFileName, "_")
                If PositionStart = 0 Then ' There is no _ symbol in the file name
                    Call showMsg("The csv file name is invalid ", "")
                    Exit Sub
                Else
                    strTmp = Right(strSrcFileName, Len(strSrcFileName) - PositionStart)
                    strTmp = Left(strTmp, 2)
                    If (UCase(strTmp) = "DA") Then
                    Else
                        Call showMsg("The csv file name is invalid", "")
                        Exit Sub
                    End If
                End If


                Dim strDay As String = Right(strFileName, 2)
                Dim strMonth As String = Mid(strFileName, Len(strFileName) - 3, 2)
                Dim strYear As String = Mid(strFileName, Len(strFileName) - 7, 4)

                'Verify integer
                Dim numChk As Integer = 0
                If Integer.TryParse(strDay, numChk) = False Then
                    Call showMsg("The csv file name is invalid ", "")
                    Exit Sub
                End If
                ''Allowed 01/11/21 
                'If Not (numChk = 1 Or numChk = 11 Or numChk = 21) Then
                '    Call showMsg("The csv file name is invalid ", "")
                '    Exit Sub
                'End If

                If Integer.TryParse(strMonth, numChk) = False Then
                    Call showMsg("The csv file name is invalid ", "")
                    Exit Sub
                End If
                'Maximum Allowed 12 Months
                If numChk > 12 Then
                    Call showMsg("The csv file name is invalid ", "")
                    Exit Sub
                End If
                If Integer.TryParse(strYear, numChk) = False Then
                    Call showMsg("The csv file name is invalid ", "")
                    Exit Sub
                End If
                'Allowed Greater than 2018
                If numChk < 2018 Then
                    Call showMsg("The csv file name is invalid ", "")
                    Exit Sub
                End If
                'Move the file to temporary folder
                If Not Directory.Exists(ConfigurationManager.AppSettings("RootDir") & "\" & DropListLocation.Text & "\" & userName & "\" & "upload") Then
                    Directory.CreateDirectory(ConfigurationManager.AppSettings("RootDir") & "\" & DropListLocation.Text & "\" & userName & "\" & "upload")
                End If
                'Assign File Name
                Dim dirPath As String = ConfigurationManager.AppSettings("RootDir") & "\" & DropListLocation.Text & "\" & userName & "\" & "upload"
                Dim fileName As String = strFileName & "_" & DateTime.Now.ToString("yyyyMMddHHmmssfff") & strExtension
                Dim filenameFullPath As String = dirPath & "\" & fileName
                'Save the file in temporary folder
                FileUploadAnalysis.SaveAs(filenameFullPath)
                'Update to Database
                Item115(filenameFullPath, fileName, userid, userName, strSrcFileName)

                'Added for Daily Claim


            ElseIf drpTask.SelectedValue = "116" Then
                'Item113. Daily Abandon
                '*********************************
                'Verify FileName Rules Followed
                Dim strFileName As String = System.IO.Path.GetFileNameWithoutExtension(FileUploadAnalysis.FileName)
                Dim strSrcFileName As String = System.IO.Path.GetFileName(FileUploadAnalysis.FileName)
                'Verify that file name must more than 6 characters
                If Len(strFileName) < 13 Then
                    Call showMsg("The csv file name is invalid ", "")
                    Exit Sub
                End If
                'Split the branch name in File Name
                PositionStart = InStr(1, strSrcFileName, "_")
                If PositionStart = 0 Then ' There is no _ symbol in the file name
                    Call showMsg("The csv file name is Invalid", "")
                    Exit Sub
                Else
                    strBranchName = Left(strSrcFileName, PositionStart - 1)
                    If Not (UCase(DropListLocation.SelectedItem.Text) = UCase(strBranchName)) Then
                        Call showMsg("The selected branch and file name is not matched", "")
                        Exit Sub
                    End If
                End If
                'Middle ID in file name
                PositionStart = InStr(1, strSrcFileName, "_")
                If PositionStart = 0 Then ' There is no _ symbol in the file name
                    Call showMsg("The csv file name is invalid ", "")
                    Exit Sub
                Else
                    strTmp = Right(strSrcFileName, Len(strSrcFileName) - PositionStart)
                    strTmp = Left(strTmp, 2)
                    If (UCase(strTmp) = "DA") Then
                    Else
                        Call showMsg("The csv file name is invalid", "")
                        Exit Sub
                    End If
                End If


                Dim strDay As String = Right(strFileName, 2)
                Dim strMonth As String = Mid(strFileName, Len(strFileName) - 3, 2)
                Dim strYear As String = Mid(strFileName, Len(strFileName) - 7, 4)

                'Verify integer
                Dim numChk As Integer = 0
                If Integer.TryParse(strDay, numChk) = False Then
                    Call showMsg("The csv file name is invalid ", "")
                    Exit Sub
                End If
                ''Allowed 01/11/21 
                'If Not (numChk = 1 Or numChk = 11 Or numChk = 21) Then
                '    Call showMsg("The csv file name is invalid ", "")
                '    Exit Sub
                'End If

                If Integer.TryParse(strMonth, numChk) = False Then
                    Call showMsg("The csv file name is invalid ", "")
                    Exit Sub
                End If
                'Maximum Allowed 12 Months
                If numChk > 12 Then
                    Call showMsg("The csv file name is invalid ", "")
                    Exit Sub
                End If
                If Integer.TryParse(strYear, numChk) = False Then
                    Call showMsg("The csv file name is invalid ", "")
                    Exit Sub
                End If
                'Allowed Greater than 2018
                If numChk < 2018 Then
                    Call showMsg("The csv file name is invalid ", "")
                    Exit Sub
                End If
                'Move the file to temporary folder
                If Not Directory.Exists(ConfigurationManager.AppSettings("RootDir") & "\" & DropListLocation.Text & "\" & userName & "\" & "upload") Then
                    Directory.CreateDirectory(ConfigurationManager.AppSettings("RootDir") & "\" & DropListLocation.Text & "\" & userName & "\" & "upload")
                End If
                'Assign File Name
                Dim dirPath As String = ConfigurationManager.AppSettings("RootDir") & "\" & DropListLocation.Text & "\" & userName & "\" & "upload"
                Dim fileName As String = strFileName & "_" & DateTime.Now.ToString("yyyyMMddHHmmssfff") & strExtension
                Dim filenameFullPath As String = dirPath & "\" & fileName
                'Save the file in temporary folder
                FileUploadAnalysis.SaveAs(filenameFullPath)
                'Update to Database
                Item116(filenameFullPath, fileName, userid, userName, strSrcFileName)


                'Added for SonyDaily Delivered

            ElseIf drpTask.SelectedValue = "117" Then

                'Verify FileName Rules Followed
                Dim strFileName As String = System.IO.Path.GetFileNameWithoutExtension(FileUploadAnalysis.FileName)
                Dim strSrcFileName As String = System.IO.Path.GetFileName(FileUploadAnalysis.FileName)
                'Verify that file name must more than 6 characters
                If Len(strFileName) < 13 Then
                    Call showMsg("The csv file name is invalid ", "")
                    Exit Sub
                End If
                'Split the branch name in File Name
                PositionStart = InStr(1, strSrcFileName, "_")
                If PositionStart = 0 Then ' There is no _ symbol in the file name
                    Call showMsg("The csv file name is Invalid", "")
                    Exit Sub
                Else
                    strBranchName = Left(strSrcFileName, PositionStart - 1)
                    If Not (UCase(DropListLocation.SelectedItem.Text) = UCase(strBranchName)) Then
                        Call showMsg("The selected branch and file name is not matched", "")
                        Exit Sub
                    End If
                End If
                'Middle ID in file name
                PositionStart = InStr(1, strSrcFileName, "_")
                If PositionStart = 0 Then ' There is no _ symbol in the file name
                    Call showMsg("The csv file name is invalid ", "")
                    Exit Sub
                Else
                    strTmp = Right(strSrcFileName, Len(strSrcFileName) - PositionStart)
                    strTmp = Left(strTmp, 2)
                    If (UCase(strTmp) = "DA") Then
                    Else
                        Call showMsg("The csv file name is invalid", "")
                        Exit Sub
                    End If
                End If


                Dim strDay As String = Right(strFileName, 2)
                Dim strMonth As String = Mid(strFileName, Len(strFileName) - 3, 2)
                Dim strYear As String = Mid(strFileName, Len(strFileName) - 7, 4)

                'Verify integer
                Dim numChk As Integer = 0
                If Integer.TryParse(strDay, numChk) = False Then
                    Call showMsg("The csv file name is invalid ", "")
                    Exit Sub
                End If
                ''Allowed 01/11/21 
                'If Not (numChk = 1 Or numChk = 11 Or numChk = 21) Then
                '    Call showMsg("The csv file name is invalid ", "")
                '    Exit Sub
                'End If

                If Integer.TryParse(strMonth, numChk) = False Then
                    Call showMsg("The csv file name is invalid ", "")
                    Exit Sub
                End If
                'Maximum Allowed 12 Months
                If numChk > 12 Then
                    Call showMsg("The csv file name is invalid ", "")
                    Exit Sub
                End If
                If Integer.TryParse(strYear, numChk) = False Then
                    Call showMsg("The csv file name is invalid ", "")
                    Exit Sub
                End If
                'Allowed Greater than 2018
                If numChk < 2018 Then
                    Call showMsg("The csv file name is invalid ", "")
                    Exit Sub
                End If
                'Move the file to temporary folder
                If Not Directory.Exists(ConfigurationManager.AppSettings("RootDir") & "\" & DropListLocation.Text & "\" & userName & "\" & "upload") Then
                    Directory.CreateDirectory(ConfigurationManager.AppSettings("RootDir") & "\" & DropListLocation.Text & "\" & userName & "\" & "upload")
                End If
                'Assign File Name
                Dim dirPath As String = ConfigurationManager.AppSettings("RootDir") & "\" & DropListLocation.Text & "\" & userName & "\" & "upload"
                Dim fileName As String = strFileName & "_" & DateTime.Now.ToString("yyyyMMddHHmmssfff") & strExtension
                Dim filenameFullPath As String = dirPath & "\" & fileName
                'Save the file in temporary folder
                FileUploadAnalysis.SaveAs(filenameFullPath)
                'Update to Database
                Item117(filenameFullPath, fileName, userid, userName, strSrcFileName)


                'Added for SonydailyOSReservation

            ElseIf drpTask.SelectedValue = "118" Then

                'Verify FileName Rules Followed
                Dim strFileName As String = System.IO.Path.GetFileNameWithoutExtension(FileUploadAnalysis.FileName)
                Dim strSrcFileName As String = System.IO.Path.GetFileName(FileUploadAnalysis.FileName)
                'Verify that file name must more than 6 characters
                If Len(strFileName) < 13 Then
                    Call showMsg("The csv file name is invalid ", "")
                    Exit Sub
                End If
                'Split the branch name in File Name
                PositionStart = InStr(1, strSrcFileName, "_")
                If PositionStart = 0 Then ' There is no _ symbol in the file name
                    Call showMsg("The csv file name is Invalid", "")
                    Exit Sub
                Else
                    strBranchName = Left(strSrcFileName, PositionStart - 1)
                    If Not (UCase(DropListLocation.SelectedItem.Text) = UCase(strBranchName)) Then
                        Call showMsg("The selected branch and file name is not matched", "")
                        Exit Sub
                    End If
                End If
                'Middle ID in file name
                PositionStart = InStr(1, strSrcFileName, "_")
                If PositionStart = 0 Then ' There is no _ symbol in the file name
                    Call showMsg("The csv file name is invalid ", "")
                    Exit Sub
                Else
                    strTmp = Right(strSrcFileName, Len(strSrcFileName) - PositionStart)
                    strTmp = Left(strTmp, 2)
                    If (UCase(strTmp) = "DA") Then
                    Else
                        Call showMsg("The csv file name is invalid", "")
                        Exit Sub
                    End If
                End If


                Dim strDay As String = Right(strFileName, 2)
                Dim strMonth As String = Mid(strFileName, Len(strFileName) - 3, 2)
                Dim strYear As String = Mid(strFileName, Len(strFileName) - 7, 4)

                'Verify integer
                Dim numChk As Integer = 0
                If Integer.TryParse(strDay, numChk) = False Then
                    Call showMsg("The csv file name is invalid ", "")
                    Exit Sub
                End If
                ''Allowed 01/11/21 
                'If Not (numChk = 1 Or numChk = 11 Or numChk = 21) Then
                '    Call showMsg("The csv file name is invalid ", "")
                '    Exit Sub
                'End If

                If Integer.TryParse(strMonth, numChk) = False Then
                    Call showMsg("The csv file name is invalid ", "")
                    Exit Sub
                End If
                'Maximum Allowed 12 Months
                If numChk > 12 Then
                    Call showMsg("The csv file name is invalid ", "")
                    Exit Sub
                End If
                If Integer.TryParse(strYear, numChk) = False Then
                    Call showMsg("The csv file name is invalid ", "")
                    Exit Sub
                End If
                'Allowed Greater than 2018
                If numChk < 2018 Then
                    Call showMsg("The csv file name is invalid ", "")
                    Exit Sub
                End If
                'Move the file to temporary folder
                If Not Directory.Exists(ConfigurationManager.AppSettings("RootDir") & "\" & DropListLocation.Text & "\" & userName & "\" & "upload") Then
                    Directory.CreateDirectory(ConfigurationManager.AppSettings("RootDir") & "\" & DropListLocation.Text & "\" & userName & "\" & "upload")
                End If
                'Assign File Name
                Dim dirPath As String = ConfigurationManager.AppSettings("RootDir") & "\" & DropListLocation.Text & "\" & userName & "\" & "upload"
                Dim fileName As String = strFileName & "_" & DateTime.Now.ToString("yyyyMMddHHmmssfff") & strExtension
                Dim filenameFullPath As String = dirPath & "\" & fileName
                'Save the file in temporary folder
                FileUploadAnalysis.SaveAs(filenameFullPath)
                'Update to Database
                Item118(filenameFullPath, fileName, userid, userName, strSrcFileName)

                'Added for SonyDaily Receiveset
            ElseIf drpTask.SelectedValue = "119" Then

                'Verify FileName Rules Followed
                Dim strFileName As String = System.IO.Path.GetFileNameWithoutExtension(FileUploadAnalysis.FileName)
                Dim strSrcFileName As String = System.IO.Path.GetFileName(FileUploadAnalysis.FileName)
                'Verify that file name must more than 6 characters
                If Len(strFileName) < 13 Then
                    Call showMsg("The csv file name is invalid ", "")
                    Exit Sub
                End If
                'Split the branch name in File Name
                PositionStart = InStr(1, strSrcFileName, "_")
                If PositionStart = 0 Then ' There is no _ symbol in the file name
                    Call showMsg("The csv file name is Invalid", "")
                    Exit Sub
                Else
                    strBranchName = Left(strSrcFileName, PositionStart - 1)
                    If Not (UCase(DropListLocation.SelectedItem.Text) = UCase(strBranchName)) Then
                        Call showMsg("The selected branch and file name is not matched", "")
                        Exit Sub
                    End If
                End If
                'Middle ID in file name
                PositionStart = InStr(1, strSrcFileName, "_")
                If PositionStart = 0 Then ' There is no _ symbol in the file name
                    Call showMsg("The csv file name is invalid ", "")
                    Exit Sub
                Else
                    strTmp = Right(strSrcFileName, Len(strSrcFileName) - PositionStart)
                    strTmp = Left(strTmp, 2)
                    If (UCase(strTmp) = "DA") Then
                    Else
                        Call showMsg("The csv file name is invalid", "")
                        Exit Sub
                    End If
                End If


                Dim strDay As String = Right(strFileName, 2)
                Dim strMonth As String = Mid(strFileName, Len(strFileName) - 3, 2)
                Dim strYear As String = Mid(strFileName, Len(strFileName) - 7, 4)

                'Verify integer
                Dim numChk As Integer = 0
                If Integer.TryParse(strDay, numChk) = False Then
                    Call showMsg("The csv file name is invalid ", "")
                    Exit Sub
                End If
                ''Allowed 01/11/21 
                'If Not (numChk = 1 Or numChk = 11 Or numChk = 21) Then
                '    Call showMsg("The csv file name is invalid ", "")
                '    Exit Sub
                'End If

                If Integer.TryParse(strMonth, numChk) = False Then
                    Call showMsg("The csv file name is invalid ", "")
                    Exit Sub
                End If
                'Maximum Allowed 12 Months
                If numChk > 12 Then
                    Call showMsg("The csv file name is invalid ", "")
                    Exit Sub
                End If
                If Integer.TryParse(strYear, numChk) = False Then
                    Call showMsg("The csv file name is invalid ", "")
                    Exit Sub
                End If
                'Allowed Greater than 2018
                If numChk < 2018 Then
                    Call showMsg("The csv file name is invalid ", "")
                    Exit Sub
                End If
                'Move the file to temporary folder
                If Not Directory.Exists(ConfigurationManager.AppSettings("RootDir") & "\" & DropListLocation.Text & "\" & userName & "\" & "upload") Then
                    Directory.CreateDirectory(ConfigurationManager.AppSettings("RootDir") & "\" & DropListLocation.Text & "\" & userName & "\" & "upload")
                End If
                'Assign File Name
                Dim dirPath As String = ConfigurationManager.AppSettings("RootDir") & "\" & DropListLocation.Text & "\" & userName & "\" & "upload"
                Dim fileName As String = strFileName & "_" & DateTime.Now.ToString("yyyyMMddHHmmssfff") & strExtension
                Dim filenameFullPath As String = dirPath & "\" & fileName
                'Save the file in temporary folder
                FileUploadAnalysis.SaveAs(filenameFullPath)
                'Update to Database
                Item119(filenameFullPath, fileName, userid, userName, strSrcFileName)


            ElseIf drpTask.SelectedValue = "120" Then

                'Verify FileName Rules Followed
                Dim strFileName As String = System.IO.Path.GetFileNameWithoutExtension(FileUploadAnalysis.FileName)
                Dim strSrcFileName As String = System.IO.Path.GetFileName(FileUploadAnalysis.FileName)
                'Verify that file name must more than 6 characters
                If Len(strFileName) < 13 Then
                    Call showMsg("The csv file name is invalid ", "")
                    Exit Sub
                End If
                'Split the branch name in File Name
                PositionStart = InStr(1, strSrcFileName, "_")
                If PositionStart = 0 Then ' There is no _ symbol in the file name
                    Call showMsg("The csv file name is Invalid", "")
                    Exit Sub
                Else
                    strBranchName = Left(strSrcFileName, PositionStart - 1)
                    If Not (UCase(DropListLocation.SelectedItem.Text) = UCase(strBranchName)) Then
                        Call showMsg("The selected branch and file name is not matched", "")
                        Exit Sub
                    End If
                End If
                'Middle ID in file name
                PositionStart = InStr(1, strSrcFileName, "_")
                If PositionStart = 0 Then ' There is no _ symbol in the file name
                    Call showMsg("The csv file name is invalid ", "")
                    Exit Sub
                Else
                    strTmp = Right(strSrcFileName, Len(strSrcFileName) - PositionStart)
                    strTmp = Left(strTmp, 2)
                    If (UCase(strTmp) = "OS") Then
                    Else
                        Call showMsg("The csv file name is invalid", "")
                        Exit Sub
                    End If
                End If


                Dim strDay As String = Right(strFileName, 2)
                Dim strMonth As String = Mid(strFileName, Len(strFileName) - 3, 2)
                Dim strYear As String = Mid(strFileName, Len(strFileName) - 7, 4)

                'Verify integer
                Dim numChk As Integer = 0
                If Integer.TryParse(strDay, numChk) = False Then
                    Call showMsg("The csv file name is invalid ", "")
                    Exit Sub
                End If
                ''Allowed 01/11/21 
                'If Not (numChk = 1 Or numChk = 11 Or numChk = 21) Then
                '    Call showMsg("The csv file name is invalid ", "")
                '    Exit Sub
                'End If

                If Integer.TryParse(strMonth, numChk) = False Then
                    Call showMsg("The csv file name is invalid ", "")
                    Exit Sub
                End If
                'Maximum Allowed 12 Months
                If numChk > 12 Then
                    Call showMsg("The csv file name is invalid ", "")
                    Exit Sub
                End If
                If Integer.TryParse(strYear, numChk) = False Then
                    Call showMsg("The csv file name is invalid ", "")
                    Exit Sub
                End If
                'Allowed Greater than 2018
                If numChk < 2018 Then
                    Call showMsg("The csv file name is invalid ", "")
                    Exit Sub
                End If
                'Move the file to temporary folder
                If Not Directory.Exists(ConfigurationManager.AppSettings("RootDir") & "\" & DropListLocation.Text & "\" & userName & "\" & "upload") Then
                    Directory.CreateDirectory(ConfigurationManager.AppSettings("RootDir") & "\" & DropListLocation.Text & "\" & userName & "\" & "upload")
                End If
                'Assign File Name
                Dim dirPath As String = ConfigurationManager.AppSettings("RootDir") & "\" & DropListLocation.Text & "\" & userName & "\" & "upload"
                Dim fileName As String = strFileName & "_" & DateTime.Now.ToString("yyyyMMddHHmmssfff") & strExtension
                Dim filenameFullPath As String = dirPath & "\" & fileName
                'Save the file in temporary folder
                FileUploadAnalysis.SaveAs(filenameFullPath)
                'Update to Database
                Item120(filenameFullPath, fileName, userid, userName, strSrcFileName)


                'SONY_Acct_stmt
            ElseIf drpTask.SelectedValue = "121" Then

                'Verify FileName Rules Followed
                Dim strFileName As String = System.IO.Path.GetFileNameWithoutExtension(FileUploadAnalysis.FileName)
                Dim strSrcFileName As String = System.IO.Path.GetFileName(FileUploadAnalysis.FileName)
                'Verify that file name must more than 6 characters
                If Len(strFileName) < 13 Then
                    Call showMsg("The csv file name is invalid ", "")
                    Exit Sub
                End If
                'Split the branch name in File Name
                PositionStart = InStr(1, strSrcFileName, "_")
                If PositionStart = 0 Then ' There is no _ symbol in the file name
                    Call showMsg("The csv file name is Invalid", "")
                    Exit Sub
                Else
                    strBranchName = Left(strSrcFileName, PositionStart - 1)
                    If Not (UCase(DropListLocation.SelectedItem.Text) = UCase(strBranchName)) Then
                        Call showMsg("The selected branch and file name is not matched", "")
                        Exit Sub
                    End If
                End If
                'Middle ID in file name
                PositionStart = InStr(1, strSrcFileName, "_")
                If PositionStart = 0 Then ' There is no _ symbol in the file name
                    Call showMsg("The csv file name is invalid ", "")
                    Exit Sub
                Else
                    strTmp = Right(strSrcFileName, Len(strSrcFileName) - PositionStart)
                    strTmp = Left(strTmp, 2)
                    If (UCase(strTmp) = "AS") Then
                    Else
                        Call showMsg("The csv file name is invalid", "")
                        Exit Sub
                    End If
                End If


                Dim strDay As String = Right(strFileName, 2)
                Dim strMonth As String = Mid(strFileName, Len(strFileName) - 3, 2)
                Dim strYear As String = Mid(strFileName, Len(strFileName) - 7, 4)

                'Verify integer
                Dim numChk As Integer = 0
                If Integer.TryParse(strDay, numChk) = False Then
                    Call showMsg("The csv file name is invalid ", "")
                    Exit Sub
                End If
                ''Allowed 01/11/21 
                'If Not (numChk = 1 Or numChk = 11 Or numChk = 21) Then
                '    Call showMsg("The csv file name is invalid ", "")
                '    Exit Sub
                'End If

                If Integer.TryParse(strMonth, numChk) = False Then
                    Call showMsg("The csv file name is invalid ", "")
                    Exit Sub
                End If
                'Maximum Allowed 12 Months
                If numChk > 12 Then
                    Call showMsg("The csv file name is invalid ", "")
                    Exit Sub
                End If
                If Integer.TryParse(strYear, numChk) = False Then
                    Call showMsg("The csv file name is invalid ", "")
                    Exit Sub
                End If
                'Allowed Greater than 2018
                If numChk < 2018 Then
                    Call showMsg("The csv file name is invalid ", "")
                    Exit Sub
                End If
                'Move the file to temporary folder
                If Not Directory.Exists(ConfigurationManager.AppSettings("RootDir") & "\" & DropListLocation.Text & "\" & userName & "\" & "upload") Then
                    Directory.CreateDirectory(ConfigurationManager.AppSettings("RootDir") & "\" & DropListLocation.Text & "\" & userName & "\" & "upload")
                End If
                'Assign File Name
                Dim dirPath As String = ConfigurationManager.AppSettings("RootDir") & "\" & DropListLocation.Text & "\" & userName & "\" & "upload"
                Dim fileName As String = strFileName & "_" & DateTime.Now.ToString("yyyyMMddHHmmssfff") & strExtension
                Dim filenameFullPath As String = dirPath & "\" & fileName
                'Save the file in temporary folder
                FileUploadAnalysis.SaveAs(filenameFullPath)
                'Update to Database
                Item121(filenameFullPath, fileName, userid, userName, strSrcFileName)


                'Daily RepairSet_NP

            ElseIf drpTask.SelectedValue = "122" Then

                'Verify FileName Rules Followed
                Dim strFileName As String = System.IO.Path.GetFileNameWithoutExtension(FileUploadAnalysis.FileName)
                Dim strSrcFileName As String = System.IO.Path.GetFileName(FileUploadAnalysis.FileName)
                'Verify that file name must more than 6 characters
                If Len(strFileName) < 13 Then
                    Call showMsg("The csv file name is invalid ", "")
                    Exit Sub
                End If
                'Split the branch name in File Name
                PositionStart = InStr(1, strSrcFileName, "_")
                If PositionStart = 0 Then ' There is no _ symbol in the file name
                    Call showMsg("The csv file name is Invalid", "")
                    Exit Sub
                Else
                    strBranchName = Left(strSrcFileName, PositionStart - 1)
                    If Not (UCase(DropListLocation.SelectedItem.Text) = UCase(strBranchName)) Then
                        Call showMsg("The selected branch and file name is not matched", "")
                        Exit Sub
                    End If
                End If
                'Middle ID in file name
                PositionStart = InStr(1, strSrcFileName, "_")
                If PositionStart = 0 Then ' There is no _ symbol in the file name
                    Call showMsg("The csv file name is invalid ", "")
                    Exit Sub
                Else
                    strTmp = Right(strSrcFileName, Len(strSrcFileName) - PositionStart)
                    strTmp = Left(strTmp, 2)
                    If (UCase(strTmp) = "DR") Then
                    Else
                        Call showMsg("The csv file name is invalid", "")
                        Exit Sub
                    End If
                End If


                Dim strDay As String = Right(strFileName, 2)
                Dim strMonth As String = Mid(strFileName, Len(strFileName) - 3, 2)
                Dim strYear As String = Mid(strFileName, Len(strFileName) - 7, 4)

                'Verify integer
                Dim numChk As Integer = 0
                If Integer.TryParse(strDay, numChk) = False Then
                    Call showMsg("The csv file name is invalid ", "")
                    Exit Sub
                End If
                ''Allowed 01/11/21 
                'If Not (numChk = 1 Or numChk = 11 Or numChk = 21) Then
                '    Call showMsg("The csv file name is invalid ", "")
                '    Exit Sub
                'End If

                If Integer.TryParse(strMonth, numChk) = False Then
                    Call showMsg("The csv file name is invalid ", "")
                    Exit Sub
                End If
                'Maximum Allowed 12 Months
                If numChk > 12 Then
                    Call showMsg("The csv file name is invalid ", "")
                    Exit Sub
                End If
                If Integer.TryParse(strYear, numChk) = False Then
                    Call showMsg("The csv file name is invalid ", "")
                    Exit Sub
                End If
                'Allowed Greater than 2018
                If numChk < 2018 Then
                    Call showMsg("The csv file name is invalid ", "")
                    Exit Sub
                End If
                'Move the file to temporary folder
                If Not Directory.Exists(ConfigurationManager.AppSettings("RootDir") & "\" & DropListLocation.Text & "\" & userName & "\" & "upload") Then
                    Directory.CreateDirectory(ConfigurationManager.AppSettings("RootDir") & "\" & DropListLocation.Text & "\" & userName & "\" & "upload")
                End If
                'Assign File Name
                Dim dirPath As String = ConfigurationManager.AppSettings("RootDir") & "\" & DropListLocation.Text & "\" & userName & "\" & "upload"
                Dim fileName As String = strFileName & "_" & DateTime.Now.ToString("yyyyMMddHHmmssfff") & strExtension
                Dim filenameFullPath As String = dirPath & "\" & fileName
                'Save the file in temporary folder
                FileUploadAnalysis.SaveAs(filenameFullPath)
                'Update to Database
                Item122(filenameFullPath, fileName, userid, userName, strSrcFileName)


                'SONY_DAILY_UNDELIVEREDSET
            ElseIf drpTask.SelectedValue = "123" Then

                'Verify FileName Rules Followed
                Dim strFileName As String = System.IO.Path.GetFileNameWithoutExtension(FileUploadAnalysis.FileName)
                Dim strSrcFileName As String = System.IO.Path.GetFileName(FileUploadAnalysis.FileName)
                'Verify that file name must more than 6 characters
                If Len(strFileName) < 13 Then
                    Call showMsg("The csv file name is invalid ", "")
                    Exit Sub
                End If
                'Split the branch name in File Name
                PositionStart = InStr(1, strSrcFileName, "_")
                If PositionStart = 0 Then ' There is no _ symbol in the file name
                    Call showMsg("The csv file name is Invalid", "")
                    Exit Sub
                Else
                    strBranchName = Left(strSrcFileName, PositionStart - 1)
                    If Not (UCase(DropListLocation.SelectedItem.Text) = UCase(strBranchName)) Then
                        Call showMsg("The selected branch and file name is not matched", "")
                        Exit Sub
                    End If
                End If
                'Middle ID in file name
                PositionStart = InStr(1, strSrcFileName, "_")
                If PositionStart = 0 Then ' There is no _ symbol in the file name
                    Call showMsg("The csv file name is invalid ", "")
                    Exit Sub
                Else
                    strTmp = Right(strSrcFileName, Len(strSrcFileName) - PositionStart)
                    strTmp = Left(strTmp, 2)
                    If (UCase(strTmp) = "UD") Then
                    Else
                        Call showMsg("The csv file name is invalid", "")
                        Exit Sub
                    End If
                End If


                Dim strDay As String = Right(strFileName, 2)
                Dim strMonth As String = Mid(strFileName, Len(strFileName) - 3, 2)
                Dim strYear As String = Mid(strFileName, Len(strFileName) - 7, 4)

                'Verify integer
                Dim numChk As Integer = 0
                If Integer.TryParse(strDay, numChk) = False Then
                    Call showMsg("The csv file name is invalid ", "")
                    Exit Sub
                End If
                ''Allowed 01/11/21 
                'If Not (numChk = 1 Or numChk = 11 Or numChk = 21) Then
                '    Call showMsg("The csv file name is invalid ", "")
                '    Exit Sub
                'End If

                If Integer.TryParse(strMonth, numChk) = False Then
                    Call showMsg("The csv file name is invalid ", "")
                    Exit Sub
                End If
                'Maximum Allowed 12 Months
                If numChk > 12 Then
                    Call showMsg("The csv file name is invalid ", "")
                    Exit Sub
                End If
                If Integer.TryParse(strYear, numChk) = False Then
                    Call showMsg("The csv file name is invalid ", "")
                    Exit Sub
                End If
                'Allowed Greater than 2018
                If numChk < 2018 Then
                    Call showMsg("The csv file name is invalid ", "")
                    Exit Sub
                End If
                'Move the file to temporary folder
                If Not Directory.Exists(ConfigurationManager.AppSettings("RootDir") & "\" & DropListLocation.Text & "\" & userName & "\" & "upload") Then
                    Directory.CreateDirectory(ConfigurationManager.AppSettings("RootDir") & "\" & DropListLocation.Text & "\" & userName & "\" & "upload")
                End If
                'Assign File Name
                Dim dirPath As String = ConfigurationManager.AppSettings("RootDir") & "\" & DropListLocation.Text & "\" & userName & "\" & "upload"
                Dim fileName As String = strFileName & "_" & DateTime.Now.ToString("yyyyMMddHHmmssfff") & strExtension
                Dim filenameFullPath As String = dirPath & "\" & fileName
                'Save the file in temporary folder
                FileUploadAnalysis.SaveAs(filenameFullPath)
                'Update to Database
                Item123(filenameFullPath, fileName, userid, userName, strSrcFileName)



                ' daily_UnRepaipairset_NP

            ElseIf drpTask.SelectedValue = "124" Then

                'Verify FileName Rules Followed
                Dim strFileName As String = System.IO.Path.GetFileNameWithoutExtension(FileUploadAnalysis.FileName)
                Dim strSrcFileName As String = System.IO.Path.GetFileName(FileUploadAnalysis.FileName)
                'Verify that file name must more than 6 characters
                If Len(strFileName) < 13 Then
                    Call showMsg("The csv file name is invalid ", "")
                    Exit Sub
                End If
                'Split the branch name in File Name
                PositionStart = InStr(1, strSrcFileName, "_")
                If PositionStart = 0 Then ' There is no _ symbol in the file name
                    Call showMsg("The csv file name is Invalid", "")
                    Exit Sub
                Else
                    strBranchName = Left(strSrcFileName, PositionStart - 1)
                    If Not (UCase(DropListLocation.SelectedItem.Text) = UCase(strBranchName)) Then
                        Call showMsg("The selected branch and file name is not matched", "")
                        Exit Sub
                    End If
                End If
                'Middle ID in file name
                PositionStart = InStr(1, strSrcFileName, "_")
                If PositionStart = 0 Then ' There is no _ symbol in the file name
                    Call showMsg("The csv file name is invalid ", "")
                    Exit Sub
                Else
                    strTmp = Right(strSrcFileName, Len(strSrcFileName) - PositionStart)
                    strTmp = Left(strTmp, 2)
                    If (UCase(strTmp) = "UR") Then
                    Else
                        Call showMsg("The csv file name is invalid", "")
                        Exit Sub
                    End If
                End If


                Dim strDay As String = Right(strFileName, 2)
                Dim strMonth As String = Mid(strFileName, Len(strFileName) - 3, 2)
                Dim strYear As String = Mid(strFileName, Len(strFileName) - 7, 4)

                'Verify integer
                Dim numChk As Integer = 0
                If Integer.TryParse(strDay, numChk) = False Then
                    Call showMsg("The csv file name is invalid ", "")
                    Exit Sub
                End If
                ''Allowed 01/11/21 
                'If Not (numChk = 1 Or numChk = 11 Or numChk = 21) Then
                '    Call showMsg("The csv file name is invalid ", "")
                '    Exit Sub
                'End If

                If Integer.TryParse(strMonth, numChk) = False Then
                    Call showMsg("The csv file name is invalid ", "")
                    Exit Sub
                End If
                'Maximum Allowed 12 Months
                If numChk > 12 Then
                    Call showMsg("The csv file name is invalid ", "")
                    Exit Sub
                End If
                If Integer.TryParse(strYear, numChk) = False Then
                    Call showMsg("The csv file name is invalid ", "")
                    Exit Sub
                End If
                'Allowed Greater than 2018
                If numChk < 2018 Then
                    Call showMsg("The csv file name is invalid ", "")
                    Exit Sub
                End If
                'Move the file to temporary folder
                If Not Directory.Exists(ConfigurationManager.AppSettings("RootDir") & "\" & DropListLocation.Text & "\" & userName & "\" & "upload") Then
                    Directory.CreateDirectory(ConfigurationManager.AppSettings("RootDir") & "\" & DropListLocation.Text & "\" & userName & "\" & "upload")
                End If
                'Assign File Name
                Dim dirPath As String = ConfigurationManager.AppSettings("RootDir") & "\" & DropListLocation.Text & "\" & userName & "\" & "upload"
                Dim fileName As String = strFileName & "_" & DateTime.Now.ToString("yyyyMMddHHmmssfff") & strExtension
                Dim filenameFullPath As String = dirPath & "\" & fileName
                'Save the file in temporary folder
                FileUploadAnalysis.SaveAs(filenameFullPath)
                'Update to Database
                Item124(filenameFullPath, fileName, userid, userName, strSrcFileName)



                'Added Monthly Reservationvation

            ElseIf drpTask.SelectedValue = "125" Then
                'Item125.
                '*********************************
                'Verify FileName Rules Followed
                'Verify FileName Rules Followed
                Dim strFileName As String = System.IO.Path.GetFileNameWithoutExtension(FileUploadAnalysis.FileName)
                Dim strSrcFileName As String = System.IO.Path.GetFileName(FileUploadAnalysis.FileName)

                'Verify that file name must more than 6 characters
                If Len(strFileName) < 10 Then
                    Call showMsg("CSV File Name is Invalid in End of (yyyymm)", "")
                    Exit Sub
                End If

                'Split the branch name in File Name
                PositionStart = InStr(1, strSrcFileName, "_")
                If PositionStart = 0 Then ' There is no _ symbol in the file name
                    Call showMsg("The csv file name is invalid", "")
                    Exit Sub
                Else
                    strBranchName = Left(strSrcFileName, PositionStart - 1)
                    If Not (UCase(DropListLocation.SelectedItem.Text) = UCase(strBranchName)) Then
                        Call showMsg("The selected branch and file name is not matched", "")
                        Exit Sub
                    End If
                End If
                'Middle ID in file name
                PositionStart = InStr(1, strSrcFileName, "_")
                If PositionStart = 0 Then ' There is no _ symbol in the file name
                    Call showMsg("The csv file name is invalid ", "")
                    Exit Sub
                Else
                    strTmp = Right(strSrcFileName, Len(strSrcFileName) - PositionStart)
                    strTmp = Left(strTmp, 2)
                    If (UCase(strTmp) = "RE") Then
                    Else
                        Call showMsg("The csv file name is invalid", "")
                        Exit Sub
                    End If
                End If


                Dim strMonth As String = Right(strFileName, 2)
                Dim strYear As String = Mid(strFileName, Len(strFileName) - 5, 4)
                'Verify integer
                Dim numChk As Integer = 0
                If Integer.TryParse(strMonth, numChk) = False Then
                    Call showMsg("CSV File Name is Invalid in End of (yyyymm)", "")
                    Exit Sub
                End If
                'Maximum Allowed 12 Months
                If numChk > 12 Then
                    Call showMsg("CSV File Name is Invalid in End of (yyyymm)", "")
                    Exit Sub
                End If
                If Integer.TryParse(strYear, numChk) = False Then
                    Call showMsg("CSV File Name is Invalid in End of (yyyymm)", "")
                    Exit Sub
                End If
                'Allowed Greater than 2018
                If numChk < 2018 Then
                    Call showMsg("CSV File Name is Invalid in End of (yyyymm)", "")
                    Exit Sub
                End If
                'Move the file to temporary folder
                If Not Directory.Exists(ConfigurationManager.AppSettings("RootDir") & "\" & DropListLocation.Text & "\" & userName & "\" & "upload") Then
                    Directory.CreateDirectory(ConfigurationManager.AppSettings("RootDir") & "\" & DropListLocation.Text & "\" & userName & "\" & "upload")
                End If
                'Assign File Name
                Dim dirPath As String = ConfigurationManager.AppSettings("RootDir") & "\" & DropListLocation.Text & "\" & userName & "\" & "upload"
                Dim fileName As String = strFileName & "_" & DateTime.Now.ToString("yyyyMMddHHmmssfff") & strExtension
                Dim filenameFullPath As String = dirPath & "\" & fileName
                'Save the file in temporary folder
                FileUploadAnalysis.SaveAs(filenameFullPath)
                'Update to Database
                Item125(filenameFullPath, fileName, userid, userName, strSrcFileName)

                'SONY_MONTHLY_REPAIRSET

            ElseIf drpTask.SelectedValue = "126" Then
                'Item135. MONTHLY REPAIRSET
                '*********************************

                '*********************************
                'Verify FileName Rules Followed
                Dim strFileName As String = System.IO.Path.GetFileNameWithoutExtension(FileUploadAnalysis.FileName)
                Dim strSrcFileName As String = System.IO.Path.GetFileName(FileUploadAnalysis.FileName)

                'Verify that file name must more than 6 characters
                If Len(strFileName) < 10 Then
                    Call showMsg("CSV File Name is Invalid in End of (yyyymm)", "")
                    Exit Sub
                End If

                'Split the branch name in File Name
                PositionStart = InStr(1, strSrcFileName, "_")
                If PositionStart = 0 Then ' There is no _ symbol in the file name
                    Call showMsg("The csv file name is invalid", "")
                    Exit Sub
                Else
                    strBranchName = Left(strSrcFileName, PositionStart - 1)
                    If Not (UCase(DropListLocation.SelectedItem.Text) = UCase(strBranchName)) Then
                        Call showMsg("The selected branch and file name is not matched", "")
                        Exit Sub
                    End If
                End If
                'Middle ID in file name
                PositionStart = InStr(1, strSrcFileName, "_")
                If PositionStart = 0 Then ' There is no _ symbol in the file name
                    Call showMsg("The csv file name is invalid ", "")
                    Exit Sub
                Else
                    strTmp = Right(strSrcFileName, Len(strSrcFileName) - PositionStart)
                    strTmp = Left(strTmp, 2)
                    If (UCase(strTmp) = "RP") Then
                    Else
                        Call showMsg("The csv file name is invalid", "")
                        Exit Sub
                    End If
                End If


                Dim strMonth As String = Right(strFileName, 2)
                Dim strYear As String = Mid(strFileName, Len(strFileName) - 5, 4)
                'Verify integer
                Dim numChk As Integer = 0
                If Integer.TryParse(strMonth, numChk) = False Then
                    Call showMsg("CSV File Name is Invalid in End of (yyyymm)", "")
                    Exit Sub
                End If
                'Maximum Allowed 12 Months
                If numChk > 12 Then
                    Call showMsg("CSV File Name is Invalid in End of (yyyymm)", "")
                    Exit Sub
                End If
                If Integer.TryParse(strYear, numChk) = False Then
                    Call showMsg("CSV File Name is Invalid in End of (yyyymm)", "")
                    Exit Sub
                End If
                'Allowed Greater than 2018
                If numChk < 2018 Then
                    Call showMsg("CSV File Name is Invalid in End of (yyyymm)", "")
                    Exit Sub
                End If
                'Move the file to temporary folder
                If Not Directory.Exists(ConfigurationManager.AppSettings("RootDir") & "\" & DropListLocation.Text & "\" & userName & "\" & "upload") Then
                    Directory.CreateDirectory(ConfigurationManager.AppSettings("RootDir") & "\" & DropListLocation.Text & "\" & userName & "\" & "upload")
                End If
                'Assign File Name
                Dim dirPath As String = ConfigurationManager.AppSettings("RootDir") & "\" & DropListLocation.Text & "\" & userName & "\" & "upload"
                Dim fileName As String = strFileName & "_" & DateTime.Now.ToString("yyyyMMddHHmmssfff") & strExtension
                Dim filenameFullPath As String = dirPath & "\" & fileName
                'Save the file in temporary folder
                FileUploadAnalysis.SaveAs(filenameFullPath)
                'Update to Database
                Item126(filenameFullPath, fileName, userid, userName, strSrcFileName)

                'Monthly Abandon

            ElseIf drpTask.SelectedValue = "127" Then
                'Item135. MONTHLY Abandon
                '*********************************
                'Item9 Stock OverView
                '*********************************
                'Verify FileName Rules Followed
                Dim strFileName As String = System.IO.Path.GetFileNameWithoutExtension(FileUploadAnalysis.FileName)
                Dim strSrcFileName As String = System.IO.Path.GetFileName(FileUploadAnalysis.FileName)

                'Verify that file name must more than 6 characters
                If Len(strFileName) < 10 Then
                    Call showMsg("CSV File Name is Invalid in End of (yyyymm)", "")
                    Exit Sub
                End If

                'Split the branch name in File Name
                PositionStart = InStr(1, strSrcFileName, "_")
                If PositionStart = 0 Then ' There is no _ symbol in the file name
                    Call showMsg("The csv file name is invalid", "")
                    Exit Sub
                Else
                    strBranchName = Left(strSrcFileName, PositionStart - 1)
                    If Not (UCase(DropListLocation.SelectedItem.Text) = UCase(strBranchName)) Then
                        Call showMsg("The selected branch and file name is not matched", "")
                        Exit Sub
                    End If
                End If
                'Middle ID in file name
                PositionStart = InStr(1, strSrcFileName, "_")
                If PositionStart = 0 Then ' There is no _ symbol in the file name
                    Call showMsg("The csv file name is invalid ", "")
                    Exit Sub
                Else
                    strTmp = Right(strSrcFileName, Len(strSrcFileName) - PositionStart)
                    strTmp = Left(strTmp, 2)
                    If (UCase(strTmp) = "MA") Then
                    Else
                        Call showMsg("The csv file name is invalid", "")
                        Exit Sub
                    End If
                End If


                Dim strMonth As String = Right(strFileName, 2)
                Dim strYear As String = Mid(strFileName, Len(strFileName) - 5, 4)
                'Verify integer
                Dim numChk As Integer = 0
                If Integer.TryParse(strMonth, numChk) = False Then
                    Call showMsg("CSV File Name is Invalid in End of (yyyymm)", "")
                    Exit Sub
                End If
                'Maximum Allowed 12 Months
                If numChk > 12 Then
                    Call showMsg("CSV File Name is Invalid in End of (yyyymm)", "")
                    Exit Sub
                End If
                If Integer.TryParse(strYear, numChk) = False Then
                    Call showMsg("CSV File Name is Invalid in End of (yyyymm)", "")
                    Exit Sub
                End If
                'Allowed Greater than 2018
                If numChk < 2018 Then
                    Call showMsg("CSV File Name is Invalid in End of (yyyymm)", "")
                    Exit Sub
                End If
                'Move the file to temporary folder
                If Not Directory.Exists(ConfigurationManager.AppSettings("RootDir") & "\" & DropListLocation.Text & "\" & userName & "\" & "upload") Then
                    Directory.CreateDirectory(ConfigurationManager.AppSettings("RootDir") & "\" & DropListLocation.Text & "\" & userName & "\" & "upload")
                End If
                'Assign File Name
                Dim dirPath As String = ConfigurationManager.AppSettings("RootDir") & "\" & DropListLocation.Text & "\" & userName & "\" & "upload"
                Dim fileName As String = strFileName & "_" & DateTime.Now.ToString("yyyyMMddHHmmssfff") & strExtension
                Dim filenameFullPath As String = dirPath & "\" & fileName
                'Save the file in temporary folder
                FileUploadAnalysis.SaveAs(filenameFullPath)
                'Update to Database
                Item127(filenameFullPath, fileName, userid, userName, strSrcFileName)



                'Added for SonyMonthlySOMCClaim
            ElseIf drpTask.SelectedValue = "128" Then

                'Verify fileName Rules Followed
                Dim strFileName As String = System.IO.Path.GetFileNameWithoutExtension(FileUploadAnalysis.FileName)
                Dim strSrcFileName As String = System.IO.Path.GetFileName(FileUploadAnalysis.FileName)

                'Verify that file name must more than 6 characters
                If Len(strFileName) < 10 Then
                    Call showMsg("CSV File Name is Invalid in End of (yyyymm)", "")
                    Exit Sub
                End If

                'Split the branch name in File Name
                PositionStart = InStr(1, strSrcFileName, "_")
                If PositionStart = 0 Then ' There is no _ symbol in the file name
                    Call showMsg("The csv file name is invalid", "")
                    Exit Sub
                Else
                    strBranchName = Left(strSrcFileName, PositionStart - 1)
                    If Not (UCase(DropListLocation.SelectedItem.Text) = UCase(strBranchName)) Then
                        Call showMsg("The selected branch and file name is not matched", "")
                        Exit Sub
                    End If
                End If
                'Middle ID in file name
                PositionStart = InStr(1, strSrcFileName, "_")
                If PositionStart = 0 Then ' There is no _ symbol in the file name
                    Call showMsg("The csv file name is invalid ", "")
                    Exit Sub
                Else
                    strTmp = Right(strSrcFileName, Len(strSrcFileName) - PositionStart)
                    strTmp = Left(strTmp, 2)
                    If (UCase(strTmp) = "MO") Then
                    Else
                        Call showMsg("The csv file name is invalid", "")
                        Exit Sub
                    End If
                End If


                Dim strMonth As String = Right(strFileName, 2)
                Dim strYear As String = Mid(strFileName, Len(strFileName) - 5, 4)
                'Verify integer
                Dim numChk As Integer = 0
                If Integer.TryParse(strMonth, numChk) = False Then
                    Call showMsg("CSV File Name is Invalid in End of (yyyymm)", "")
                    Exit Sub
                End If
                'Maximum Allowed 12 Months
                If numChk > 12 Then
                    Call showMsg("CSV File Name is Invalid in End of (yyyymm)", "")
                    Exit Sub
                End If
                If Integer.TryParse(strYear, numChk) = False Then
                    Call showMsg("CSV File Name is Invalid in End of (yyyymm)", "")
                    Exit Sub
                End If
                'Allowed Greater than 2018
                If numChk < 2018 Then
                    Call showMsg("CSV File Name is Invalid in End of (yyyymm)", "")
                    Exit Sub
                End If
                'Move the file to temporary folder
                If Not Directory.Exists(ConfigurationManager.AppSettings("RootDir") & "\" & DropListLocation.Text & "\" & userName & "\" & "upload") Then
                    Directory.CreateDirectory(ConfigurationManager.AppSettings("RootDir") & "\" & DropListLocation.Text & "\" & userName & "\" & "upload")
                End If
                'Assign File Name
                Dim dirPath As String = ConfigurationManager.AppSettings("RootDir") & "\" & DropListLocation.Text & "\" & userName & "\" & "upload"
                Dim fileName As String = strFileName & "_" & DateTime.Now.ToString("yyyyMMddHHmmssfff") & strExtension
                Dim filenameFullPath As String = dirPath & "\" & fileName
                'Save the file in temporary folder
                FileUploadAnalysis.SaveAs(filenameFullPath)
                'Update to Database
                Item128(filenameFullPath, fileName, userid, userName, strSrcFileName)


            End If

        End If


    End Sub

    Sub DefaultSearchSetting(ByVal Optional SearchType As String = "init")
        If SearchType = "init" Then
            TextPartsInvoiceNo.Enabled = False
            TextLaborInvoiceNo.Enabled = False
            TextInvoiceDate.Enabled = False
            txtDate.Enabled = False
            txtAmount.Enabled = False
            RadioBtnMM.Enabled = False
            RadioBtnDD.Enabled = False
            DropDownMonth.Visible = False
            DropDownYear.Visible = False
            txtArNo.Enabled = False


        ElseIf SearchType = "old" Then
            TextPartsInvoiceNo.Enabled = True
            TextLaborInvoiceNo.Enabled = True
            TextInvoiceDate.Enabled = True
            txtDate.Enabled = True
            txtAmount.Enabled = True
            RadioBtnMM.Enabled = True
            RadioBtnDD.Enabled = True
            'Comment on 20190801 
            txtDate.Enabled = False
            txtAmount.Enabled = False
            DropDownMonth.Visible = False
            DropDownYear.Visible = False
            txtArNo.Enabled = False

        ElseIf SearchType = "Warrenty" Then
            TextPartsInvoiceNo.Enabled = True
            TextLaborInvoiceNo.Enabled = True
            TextInvoiceDate.Text = "" 'VJ 2019/10/20
            TextInvoiceDate.Enabled = False 'VJ 2019/10/20
            txtDate.Enabled = True
            txtAmount.Enabled = True
            RadioBtnMM.Enabled = True
            RadioBtnDD.Enabled = True
            'Comment on 20190801 
            txtDate.Enabled = False
            txtAmount.Enabled = False
            DropDownMonth.Visible = False
            DropDownYear.Visible = False
            txtArNo.Enabled = False

        ElseIf SearchType = "05" Then
            TextLaborInvoiceNo.Enabled = True
            TextInvoiceDate.Enabled = True
            txtAmount.Enabled = False
            TextPartsInvoiceNo.Enabled = False
            txtDate.Enabled = False
            RadioBtnMM.Enabled = False
            RadioBtnDD.Enabled = False
            DropDownMonth.Visible = False
            DropDownYear.Visible = False
            txtArNo.Enabled = False

        ElseIf SearchType = "yyyymm" Then
            TextPartsInvoiceNo.Enabled = False
            TextLaborInvoiceNo.Enabled = False
            TextInvoiceDate.Enabled = False
            txtDate.Enabled = False
            txtAmount.Enabled = False
            RadioBtnMM.Enabled = False
            RadioBtnDD.Enabled = False
            DropDownMonth.Visible = False
            DropDownYear.Visible = False
            txtArNo.Enabled = False

        ElseIf SearchType = "boi" Then
            TextPartsInvoiceNo.Enabled = False
            TextLaborInvoiceNo.Enabled = False
            TextInvoiceDate.Enabled = False
            txtDate.Enabled = True
            txtAmount.Enabled = True
            RadioBtnMM.Enabled = False
            RadioBtnDD.Enabled = False
            DropDownMonth.Visible = False
            DropDownYear.Visible = False
            txtArNo.Enabled = True
        End If

    End Sub
    'Protected Sub drpTask_SelectedIndexChanged(sender As Object, e As EventArgs) Handles drpTask.SelectedIndexChanged
    Protected Sub OnSelectedIndexChanged(sender As Object, e As EventArgs)
        If drpTask.SelectedValue = "0" Then
            DefaultSearchSetting()
        ElseIf drpTask.SelectedValue = "1" Then
            DefaultSearchSetting("old")
        ElseIf drpTask.SelectedValue = "2" Then
            DefaultSearchSetting("Warrenty") 'VJ 2019/10/21
        ElseIf drpTask.SelectedValue = "3" Then
            DefaultSearchSetting("old")
            'Comment on 20190801
            RadioBtnMM.Enabled = False
            RadioBtnDD.Enabled = False
        ElseIf drpTask.SelectedValue = "-3" Then
            DefaultSearchSetting("old")
            RadioBtnMM.Enabled = False
            RadioBtnDD.Enabled = False
        ElseIf drpTask.SelectedValue = "4" Then
            DefaultSearchSetting("old")
            'Comment on 20190801
            RadioBtnMM.Enabled = False
            RadioBtnDD.Enabled = False
        ElseIf drpTask.SelectedValue = "5" Then
            DefaultSearchSetting("05")
        ElseIf drpTask.SelectedValue = "6" Then
            DefaultSearchSetting("yyyymm")
        ElseIf drpTask.SelectedValue = "7" Then
            DefaultSearchSetting("yyyymm")
        ElseIf drpTask.SelectedValue = "8" Then
            DefaultSearchSetting("yyyymm")
        ElseIf drpTask.SelectedValue = "9" Then
            DefaultSearchSetting("yyyymm")
        ElseIf drpTask.SelectedValue = "9A" Then
            DefaultSearchSetting("yyyymm")
            DropDownMonth.Visible = True
            DropDownYear.Visible = True

        ElseIf drpTask.SelectedValue = "10" Then
            DefaultSearchSetting("yyyymm")
        ElseIf drpTask.SelectedValue = "11" Then
            DefaultSearchSetting("yyyymm")
        ElseIf drpTask.SelectedValue = "12" Then
            DefaultSearchSetting("yyyymm")
        ElseIf drpTask.SelectedValue = "13" Then
            DefaultSearchSetting("init")
            Call showMsg("Coming Soon...", "")
            Exit Sub
        ElseIf drpTask.SelectedValue = "14" Then
            DefaultSearchSetting("yyyymm")
        ElseIf drpTask.SelectedValue = "15" Then
            DefaultSearchSetting("yyyymm")
        ElseIf drpTask.SelectedValue = "16" Then
            DefaultSearchSetting("yyyymm")
        ElseIf drpTask.SelectedValue = "17" Then 'VJ 2019/10/10
            DefaultSearchSetting("yyyymm")
        ElseIf drpTask.SelectedValue = "18" Then
            DefaultSearchSetting("yyyymm") 'VJ 2019/10/10
        ElseIf drpTask.SelectedValue = "19" Then
            DefaultSearchSetting("init")
            Call showMsg("Coming Soon...", "")
            Exit Sub
        ElseIf drpTask.SelectedValue = "19A" Then
            DefaultSearchSetting("yyyymm") 'VJ 2019/10/15
        ElseIf drpTask.SelectedValue = "19B" Then
            DefaultSearchSetting("yyyymm")
        ElseIf drpTask.SelectedValue = "23" Then
            DefaultSearchSetting("yyyymm")
        ElseIf drpTask.SelectedValue = "24" Then
            DefaultSearchSetting("boi")
            'ElseIf drpTask.SelectedValue = "103" Then
            '    DefaultSearchSetting("init")
            '    Call showMsg("Coming Soon...", "")
            '    Exit Sub
            'ElseIf drpTask.SelectedValue = "103" Then
            '    DefaultSearchSetting("init")
            '    Call showMsg("Coming Soon...", "")
            '    Exit Sub
            'ElseIf drpTask.SelectedValue = "104" Then
            '    DefaultSearchSetting("init")
            '    Call showMsg("Coming Soon...", "")
            '    Exit Sub
            'ElseIf drpTask.SelectedValue = "105" Then
            '    DefaultSearchSetting("init")
            '    Call showMsg("Coming Soon...", "")
            '    Exit Sub
            'ElseIf drpTask.SelectedValue = "106" Then
            '    DefaultSearchSetting("init")
            '    Call showMsg("Coming Soon...", "")
            '    Exit Sub
            'ElseIf drpTask.SelectedValue = "107" Then
            '    DefaultSearchSetting("init")
            '    Call showMsg("Coming Soon...", "")
            '    Exit Sub
            'ElseIf drpTask.SelectedValue = "108" Then
            '    DefaultSearchSetting("init")
            '    Call showMsg("Coming Soon...", "")
            '    Exit Sub
            'ElseIf drpTask.SelectedValue = "109" Then
            '    DefaultSearchSetting("init")
            '    Call showMsg("Coming Soon...", "")
            '    Exit Sub
            'ElseIf drpTask.SelectedValue = "110" Then
            '    DefaultSearchSetting("init")
            '    Call showMsg("Coming Soon...", "")
            '    Exit Sub
            'ElseIf drpTask.SelectedValue = "111" Then
            '    DefaultSearchSetting("init")
            '    Call showMsg("Coming Soon...", "")
            '    Exit Sub
            'ElseIf drpTask.SelectedValue = "112" Then
            '    DefaultSearchSetting("init")
            '    Call showMsg("Coming Soon...", "")
            '    Exit Sub





        End If
    End Sub

    'Private Sub Item5(filenameFullPath As String, fileName As String, userid As String, userName As String, strSrcFileName As String)
    '    'Read the content from stored file
    '    Dim cReader
    '    Dim textLines As New List(Of String())
    '    Dim strArr()() As String
    '    Try
    '        'StreamReader の新しいインスタンスを生成する
    '        cReader = New System.IO.StreamReader(filenameFullPath, System.Text.Encoding.Default)
    '        Dim rowCnt As Integer
    '        '読み込んだものを追加で格納
    '        Dim cols() As String
    '        Dim colsHead() As String 'ヘッダ１行目
    '        '   Dim colsHead2() As String 'ヘッダ２行目
    '        Dim colsValues() As String
    '        While (cReader.Peek() >= 0)
    '            'ファイルを1 行ずつ読み込む
    '            Dim stBuffer As String = cReader.ReadLine()
    '            rowCnt = rowCnt + 1
    '            '『"』の位置取得
    '            Dim pos As Integer = 0
    '            '全ての『"』の位置情報をセット
    '            Dim posSet() As Integer
    '            Dim cnt As Integer = 0
    '            Dim key As String = """"
    '            Dim tmp As String = ""
    '            Dim tmpAfter As String = ""
    '            Dim workPos As Integer
    '            Dim workLenStr As Integer
    '            While True
    '                If rowCnt = 1 Then
    '                    colsHead = Split(stBuffer, ",")
    '                    Exit While ' Assign 1st header, then close
    '                Else
    '                    colsValues = Split(stBuffer, ",")
    '                End If
    '                pos = stBuffer.IndexOf(key, pos)
    '                If pos = -1 Then
    '                    Exit While
    '                Else
    '                    ReDim Preserve posSet(cnt)
    '                    posSet(cnt) = pos
    '                    cnt = cnt + 1
    '                    pos = pos + 1
    '                    If cnt Mod 2 = 0 Then
    '                        '2の倍数になったら(""で囲まれた文字列取得できたら"Then)その文字列の最初の位置と長さを取得
    '                        workPos = posSet(cnt - 2)
    '                        workLenStr = (posSet(cnt - 1) - posSet(cnt - 2)) + 1
    '                        tmp = stBuffer.Substring(workPos, workLenStr)
    '                        '『,』を外す
    '                        tmpAfter = Replace(tmp, ",", "")
    '                        '『"』を外す
    '                        tmpAfter = Replace(tmpAfter, """", "")
    '                        'stBufferを置換
    '                        stBuffer = Replace(stBuffer, tmp, tmpAfter)
    '                        pos = 0
    '                    End If
    '                End If
    '            End While
    '            cols = Split(stBuffer, ",")
    '            If (rowCnt <> 1) And (rowCnt <> 2) Then
    '                'Insert into table 
    '            End If
    '            If UCase(cols(0)) = "TOTAL" Then
    '                Exit While ' Assign 2nd header, then close
    '            End If
    '            textLines.Add(cols)
    '            'csvファイル種類切り分けのため、ヘッダの項目名取得しておく
    '        End While
    '    Catch ex As Exception
    '        cReader.Close()
    '    Finally
    '        cReader.Close()
    '    End Try
    '    strArr = textLines.ToArray
    '    'Verify that if the records / data not formated, show the error information to the user
    '    If strArr.Count < 2 Then
    '        Call showMsg("Status: Upload Failed <br><br> The CSV file is not valid format!!!", "")
    '        Exit Sub
    '    End If
    '    'Pass the paramters to Update in Datatabase
    '    Dim _OtherUpdateModel As OtherUpdateModel = New OtherUpdateModel()
    '    Dim _OtherUpdateControl As OtherUpdateControl = New OtherUpdateControl()
    '    _OtherUpdateModel.UPDPG = "Analysis_FileUpload.aspx"
    '    _OtherUpdateModel.FileName = fileName
    '    _OtherUpdateModel.SrcFileName = strSrcFileName
    '    _OtherUpdateModel.UserId = userid
    '    _OtherUpdateModel.UserName = userName
    '    _OtherUpdateModel.ShipToBranchCode = DropListLocation.SelectedItem.Value
    '    _OtherUpdateModel.ShipToBranch = DropListLocation.SelectedItem.Text
    '    _OtherUpdateModel.LaborInvoiceNo = TextLaborInvoiceNo.Text
    '    _OtherUpdateModel.InvoiceDate = TextInvoiceDate.Text
    '    _OtherUpdateModel.Total = txtAmount.Text

    '    'Upload to csv file to database for Other Update (GSPN to MultiVendor)
    '    Dim blStatus As Boolean = _OtherUpdateControl.AddModifyOtherUpdate(strArr, _OtherUpdateModel)
    '    'If Error Raised then show the errors
    '    If Not blStatus Then
    '        Call showMsg("Status: Upload Failed <br><br>  Please check the content of file ", "")
    '        Exit Sub
    '    Else
    '        Call showMsg("Status: Upload Success <br><br> The file has been uploaded", "")
    '    End If

    'End Sub
    Private Sub Item5_1(filenameFullPath As String, fileName As String, userid As String, userName As String, strSrcFileName As String, ReportNo As String)
        'Read the content from stored file
        Dim cReader
        Dim textLines As New List(Of String())
        Dim strArr()() As String
        Try
            'StreamReader の新しいインスタンスを生成する
            cReader = New System.IO.StreamReader(filenameFullPath, System.Text.Encoding.Default)
            Dim rowCnt As Integer
            '読み込んだものを追加で格納
            Dim cols() As String
            Dim colsHead() As String 'ヘッダ１行目
            '   Dim colsHead2() As String 'ヘッダ２行目
            Dim colsValues() As String
            While (cReader.Peek() >= 0)

                'ファイルを1 行ずつ読み込む
                Dim stBuffer As String = cReader.ReadLine()
                rowCnt = rowCnt + 1
                '『"』の位置取得
                Dim pos As Integer = 0
                '全ての『"』の位置情報をセット
                Dim posSet() As Integer
                Dim cnt As Integer = 0
                Dim key As String = """"
                Dim tmp As String = ""
                Dim tmpAfter As String = ""
                Dim workPos As Integer
                Dim workLenStr As Integer
                While True
                    If rowCnt = 1 Then
                        colsHead = Split(stBuffer, ",")
                        Exit While ' Assign 1st header, then close
                    Else
                        colsValues = Split(stBuffer, ",")
                    End If
                    pos = stBuffer.IndexOf(key, pos)
                    If pos = -1 Then
                        Exit While
                    Else
                        ReDim Preserve posSet(cnt)
                        posSet(cnt) = pos
                        cnt = cnt + 1
                        pos = pos + 1
                        If cnt Mod 2 = 0 Then
                            '2の倍数になったら(""で囲まれた文字列取得できたら"Then)その文字列の最初の位置と長さを取得
                            workPos = posSet(cnt - 2)
                            workLenStr = (posSet(cnt - 1) - posSet(cnt - 2)) + 1
                            tmp = stBuffer.Substring(workPos, workLenStr)
                            '『,』を外す
                            tmpAfter = Replace(tmp, ",", "")
                            '『"』を外す
                            tmpAfter = Replace(tmpAfter, """", "")
                            'stBufferを置換
                            stBuffer = Replace(stBuffer, tmp, tmpAfter)
                            pos = 0
                        End If
                    End If
                End While
                cols = Split(stBuffer, ",")
                If (rowCnt <> 1) And (rowCnt <> 2) Then
                    'Insert into table 
                End If
                If UCase(cols(0)) = "TOTAL" Then
                    Exit While ' Assign 2nd header, then close
                End If
                textLines.Add(cols)
                'csvファイル種類切り分けのため、ヘッダの項目名取得しておく
            End While
        Catch ex As Exception
            cReader.Close()
        Finally
            cReader.Close()
        End Try
        strArr = textLines.ToArray
        'Verify that if the records / data not formated, show the error information to the user
        If strArr.Count < 2 Then
            Call showMsg("Status: Upload Failed <br><br> The CSV file is not valid format!!!", "")
            Exit Sub
        End If
        'Comment on 20190730 as per new request
        ''''''Pass the paramters to Update in Datatabase
        '''''Dim _OtherUpdateModel As OtherUpdateModel = New OtherUpdateModel()
        '''''Dim _OtherUpdateControl As OtherUpdateControl = New OtherUpdateControl()
        '''''_OtherUpdateModel.UPDPG = "Analysis_FileUpload.aspx"
        '''''_OtherUpdateModel.FileName = fileName
        '''''_OtherUpdateModel.SrcFileName = strSrcFileName
        '''''_OtherUpdateModel.UserId = userid
        '''''_OtherUpdateModel.UserName = userName
        '''''_OtherUpdateModel.ShipToBranchCode = DropListLocation.SelectedItem.Value
        '''''_OtherUpdateModel.ShipToBranch = DropListLocation.SelectedItem.Text
        '''''_OtherUpdateModel.LaborInvoiceNo = TextLaborInvoiceNo.Text
        '''''_OtherUpdateModel.InvoiceDate = TextInvoiceDate.Text
        '''''_OtherUpdateModel.Total = txtAmount.Text

        ''''''Upload to csv file to database for Other Update (GSPN to MultiVendor)
        '''''Dim blStatus As Boolean = _OtherUpdateControl.AddModifyOtherUpdate(strArr, _OtherUpdateModel)
        ''''''If Error Raised then show the errors
        '''''If Not blStatus Then
        '''''    Call showMsg("Status: Upload Failed <br><br>  Please check the content of file ", "")
        '''''    Exit Sub
        '''''Else
        '''''    Call showMsg("Status: Upload Success <br><br> The file has been uploaded", "")
        '''''End If

        'Pass the paramters to Update in Datatabase
        Dim _DebitNoteModel As DebitNoteModel = New DebitNoteModel()
        Dim _DebitNoteControl As DebitNoteControl = New DebitNoteControl()
        _DebitNoteModel.UPDPG = "Analysis_FileUpload.aspx"
        _DebitNoteModel.FileName = fileName
        _DebitNoteModel.SrcFileName = strSrcFileName
        _DebitNoteModel.UserId = userid
        _DebitNoteModel.UserName = userName
        _DebitNoteModel.ShipToBranchCode = DropListLocation.SelectedItem.Value
        _DebitNoteModel.ShipToBranch = DropListLocation.SelectedItem.Text
        _DebitNoteModel.LaborInvoiceNo = TextLaborInvoiceNo.Text
        _DebitNoteModel.InvoiceDate = TextInvoiceDate.Text
        _DebitNoteModel.ReportNumber = ReportNo 'Don't know i/o

        'Upload to csv file to database for Other Update (GSPN to MultiVendor)
        Dim blStatus As Boolean = _DebitNoteControl.AddModifyDebitNote(strArr, _DebitNoteModel)
        'If Error Raised then show the errors
        If Not blStatus Then
            Call showMsg("Status: Upload Failed <br><br>  Please check the content of file ", "")
            Exit Sub
        Else
            Call showMsg("Status: Upload Success <br><br> The file has been uploaded", "")
        End If

    End Sub

    Private Sub Item23(filenameFullPath As String, fileName As String, userid As String, userName As String, strSrcFileName As String)
        'Read the content from stored file
        Dim cReader
        Dim textLines As New List(Of String())
        Dim strArr()() As String
        Try
            'StreamReader
            cReader = New System.IO.StreamReader(filenameFullPath, System.Text.Encoding.Default)
            Dim rowCnt As Integer

            Dim cols() As String
            Dim colsHead() As String 'ヘッダ１行目

            Dim colsValues() As String
            While (cReader.Peek() >= 0)

                Dim stBuffer As String = cReader.ReadLine()
                rowCnt = rowCnt + 1

                Dim pos As Integer = 0

                Dim posSet() As Integer
                Dim cnt As Integer = 0
                Dim key As String = """"
                Dim tmp As String = ""
                Dim tmpAfter As String = ""
                Dim workPos As Integer
                Dim workLenStr As Integer
                While True
                    If rowCnt = 1 Then
                        colsHead = Split(stBuffer, ",")
                        Exit While ' Assign 1st header, then close
                    Else
                        colsValues = Split(stBuffer, ",")
                    End If
                    pos = stBuffer.IndexOf(key, pos)
                    If pos = -1 Then
                        Exit While
                    Else
                        ReDim Preserve posSet(cnt)
                        posSet(cnt) = pos
                        cnt = cnt + 1
                        pos = pos + 1
                        If cnt Mod 2 = 0 Then

                            workPos = posSet(cnt - 2)
                            workLenStr = (posSet(cnt - 1) - posSet(cnt - 2)) + 1
                            tmp = stBuffer.Substring(workPos, workLenStr)
                            '『,』を外す
                            tmpAfter = Replace(tmp, ",", "")
                            '『"』を外す
                            tmpAfter = Replace(tmpAfter, """", "")
                            'stBuffer
                            stBuffer = Replace(stBuffer, tmp, tmpAfter)
                            pos = 0
                        End If
                    End If
                End While
                cols = Split(stBuffer, ",")
                If (rowCnt <> 1) And (rowCnt <> 2) Then
                    'Insert into table 
                End If
                'If UCase(cols(0)) = "TOTAL" Then
                '    Exit While ' Assign 2nd header, then close
                'End If
                textLines.Add(cols)

            End While
        Catch ex As Exception
            cReader.Close()
        Finally
            cReader.Close()
        End Try
        strArr = textLines.ToArray
        'Verify that if the records / data not formated, show the error information to the user
        If strArr.Count < 2 Then
            Call showMsg("Status: Upload Failed <br><br> The CSV file is not valid format!!!", "")
            Exit Sub
        End If
        'Pass the paramters to Update in Datatabase
        Dim _PoDcModel As PoDcModel = New PoDcModel()
        Dim _PoDcControl As PoDcControl = New PoDcControl()
        _PoDcModel.UPDPG = "Class.Analysis.vb"
        _PoDcModel.FILE_NAME = fileName
        _PoDcModel.SRC_FILE_NAME = strSrcFileName
        _PoDcModel.UserId = userid
        _PoDcModel.UPLOAD_USER = userName
        _PoDcModel.SHIP_TO_BRANCH_CODE = DropListLocation.SelectedItem.Value
        _PoDcModel.SHIP_TO_BRANCH = DropListLocation.SelectedItem.Text


        'Upload to csv file to database for Other Update (GSPN to MultiVendor)
        Dim blStatus As Boolean = _PoDcControl.AddUploadSummary(strArr, _PoDcModel)
        'If Error Raised then show the errors
        If Not blStatus Then
            Call showMsg("Status: Upload Failed <br><br>  Please check the content of file ", "")
            Exit Sub
        Else
            Call showMsg("Status: Upload Success <br><br> The file has been uploaded", "")
        End If

    End Sub




    Private Sub Item101(filenameFullPath As String, fileName As String, userid As String, userName As String, strSrcFileName As String)
        'Read the content from stored file
        Dim cReader
        Dim textLines As New List(Of String())
        Dim strArr()() As String
        Try
            'StreamReader
            cReader = New System.IO.StreamReader(filenameFullPath, System.Text.Encoding.Default)
            Dim rowCnt As Integer

            Dim cols() As String
            Dim colsHead() As String 'ヘッダ１行目

            Dim colsValues() As String
            While (cReader.Peek() >= 0)

                Dim stBuffer As String = cReader.ReadLine()
                rowCnt = rowCnt + 1

                Dim pos As Integer = 0

                Dim posSet() As Integer
                Dim cnt As Integer = 0
                Dim key As String = """"
                Dim tmp As String = ""
                Dim tmpAfter As String = ""
                Dim workPos As Integer
                Dim workLenStr As Integer
                While True
                    If rowCnt = 1 Then
                        colsHead = Split(stBuffer, ",")
                        Exit While ' Assign 1st header, then close
                    Else
                        colsValues = Split(stBuffer, ",")
                    End If
                    pos = stBuffer.IndexOf(key, pos)
                    If pos = -1 Then
                        Exit While
                    Else
                        ReDim Preserve posSet(cnt)
                        posSet(cnt) = pos
                        cnt = cnt + 1
                        pos = pos + 1
                        If cnt Mod 2 = 0 Then

                            workPos = posSet(cnt - 2)
                            workLenStr = (posSet(cnt - 1) - posSet(cnt - 2)) + 1
                            tmp = stBuffer.Substring(workPos, workLenStr)
                            '『,』を外す
                            tmpAfter = Replace(tmp, ",", "")
                            '『"』を外す
                            tmpAfter = Replace(tmpAfter, """", "")
                            'stBuffer
                            stBuffer = Replace(stBuffer, tmp, tmpAfter)
                            pos = 0
                        End If
                    End If
                End While
                cols = Split(stBuffer, ",")
                If (rowCnt <> 1) And (rowCnt <> 2) Then
                    'Insert into table 
                End If
                'If UCase(cols(0)) = "TOTAL" Then
                '    Exit While ' Assign 2nd header, then close
                'End If
                textLines.Add(cols)

            End While
        Catch ex As Exception
            cReader.Close()
        Finally
            cReader.Close()
        End Try
        strArr = textLines.ToArray
        'Verify that if the records / data not formated, show the error information to the user
        If strArr.Count < 2 Then
            Call showMsg("Status: Upload Failed <br><br> The CSV file is not valid format!!!", "")
            Exit Sub
        End If
        'Pass the paramters to Update in Datatabase
        Dim _SonyInBoundModel As SonyInBoundModel = New SonyInBoundModel()
        Dim _SonyInBoundControl As SonyInBoundControl = New SonyInBoundControl()
        _SonyInBoundModel.UPDPG = "Class.Analysis.vb"
        _SonyInBoundModel.FILE_NAME = fileName
        _SonyInBoundModel.SRC_FILE_NAME = strSrcFileName
        _SonyInBoundModel.UserId = userid
        _SonyInBoundModel.UPLOAD_USER = userName
        _SonyInBoundModel.SHIP_TO_BRANCH_CODE = DropListLocation.SelectedItem.Value
        _SonyInBoundModel.SHIP_TO_BRANCH = DropListLocation.SelectedItem.Text


        'Upload to csv file to database for Other Update (GSPN to MultiVendor)
        Dim blStatus As Boolean = _SonyInBoundControl.AddModifyInBound(strArr, _SonyInBoundModel)
        'If Error Raised then show the errors
        If Not blStatus Then
            Call showMsg("Status: Upload Failed <br><br>  Please check the content of file ", "")
            Exit Sub
        Else
            Call showMsg("Status: Upload Success <br><br> The file has been uploaded", "")
        End If

    End Sub


    Private Sub Item102(filenameFullPath As String, fileName As String, userid As String, userName As String, strSrcFileName As String)
        'Read the content from stored file
        Dim cReader
        Dim textLines As New List(Of String())
        Dim strArr()() As String
        Try
            'StreamReader
            cReader = New System.IO.StreamReader(filenameFullPath, System.Text.Encoding.Default)
            Dim rowCnt As Integer

            Dim cols() As String
            Dim colsHead() As String 'ヘッダ１行目

            Dim colsValues() As String
            While (cReader.Peek() >= 0)

                Dim stBuffer As String = cReader.ReadLine()
                rowCnt = rowCnt + 1

                Dim pos As Integer = 0

                Dim posSet() As Integer
                Dim cnt As Integer = 0
                Dim key As String = """"
                Dim tmp As String = ""
                Dim tmpAfter As String = ""
                Dim workPos As Integer
                Dim workLenStr As Integer
                While True
                    If rowCnt = 1 Then
                        colsHead = Split(stBuffer, ",")
                        Exit While ' Assign 1st header, then close
                    Else
                        colsValues = Split(stBuffer, ",")
                    End If
                    pos = stBuffer.IndexOf(key, pos)
                    If pos = -1 Then
                        Exit While
                    Else
                        ReDim Preserve posSet(cnt)
                        posSet(cnt) = pos
                        cnt = cnt + 1
                        pos = pos + 1
                        If cnt Mod 2 = 0 Then

                            workPos = posSet(cnt - 2)
                            workLenStr = (posSet(cnt - 1) - posSet(cnt - 2)) + 1
                            tmp = stBuffer.Substring(workPos, workLenStr)
                            '『,』を外す
                            tmpAfter = Replace(tmp, ",", "")
                            '『"』を外す
                            tmpAfter = Replace(tmpAfter, """", "")
                            'stBuffer
                            stBuffer = Replace(stBuffer, tmp, tmpAfter)
                            pos = 0
                        End If
                    End If
                End While
                cols = Split(stBuffer, ",")
                If (rowCnt <> 1) And (rowCnt <> 2) Then
                    'Insert into table 
                End If
                'If UCase(cols(0)) = "TOTAL" Then
                '    Exit While ' Assign 2nd header, then close
                'End If
                textLines.Add(cols)

            End While
        Catch ex As Exception
            cReader.Close()
        Finally
            cReader.Close()
        End Try
        strArr = textLines.ToArray
        'Verify that if the records / data not formated, show the error information to the user
        If strArr.Count < 2 Then
            Call showMsg("Status: Upload Failed <br><br> The CSV file is not valid format!!!", "")
            Exit Sub
        End If
        'Pass the paramters to Update in Datatabase
        Dim _SonyOutBoundModel As SonyOutBoundModel = New SonyOutBoundModel()
        Dim _SonyOutBoundControl As SonyOutBoundControl = New SonyOutBoundControl()
        _SonyOutBoundModel.UPDPG = "Class.Analysis.vb"
        _SonyOutBoundModel.FILE_NAME = fileName
        _SonyOutBoundModel.SRC_FILE_NAME = strSrcFileName
        _SonyOutBoundModel.UserId = userid
        _SonyOutBoundModel.UPLOAD_USER = userName
        _SonyOutBoundModel.SHIP_TO_BRANCH_CODE = DropListLocation.SelectedItem.Value
        _SonyOutBoundModel.SHIP_TO_BRANCH = DropListLocation.SelectedItem.Text


        'Upload to csv file to database for Other Update (GSPN to MultiVendor)
        Dim blStatus As Boolean = _SonyOutBoundControl.AddModifyOutBound(strArr, _SonyOutBoundModel)
        'If Error Raised then show the errors
        If Not blStatus Then
            Call showMsg("Status: Upload Failed <br><br>  Please check the content of file ", "")
            Exit Sub
        Else
            Call showMsg("Status: Upload Success <br><br> The file has been uploaded", "")
        End If

    End Sub




    Private Sub Item103(filenameFullPath As String, fileName As String, userid As String, userName As String, strSrcFileName As String)
        'Read the content from stored file
        Dim cReader
        Dim textLines As New List(Of String())
        Dim strArr()() As String
        Try
            'StreamReader
            cReader = New System.IO.StreamReader(filenameFullPath, System.Text.Encoding.Default)
            Dim rowCnt As Integer

            Dim cols() As String
            Dim colsHead() As String 'ヘッダ１行目

            Dim colsValues() As String
            While (cReader.Peek() >= 0)

                Dim stBuffer As String = cReader.ReadLine()
                rowCnt = rowCnt + 1

                Dim pos As Integer = 0

                Dim posSet() As Integer
                Dim cnt As Integer = 0
                Dim key As String = """"
                Dim tmp As String = ""
                Dim tmpAfter As String = ""
                Dim workPos As Integer
                Dim workLenStr As Integer
                While True
                    If rowCnt = 1 Then
                        colsHead = Split(stBuffer, ",")
                        Exit While ' Assign 1st header, then close
                    Else
                        colsValues = Split(stBuffer, ",")
                    End If
                    pos = stBuffer.IndexOf(key, pos)
                    If pos = -1 Then
                        Exit While
                    Else
                        ReDim Preserve posSet(cnt)
                        posSet(cnt) = pos
                        cnt = cnt + 1
                        pos = pos + 1
                        If cnt Mod 2 = 0 Then

                            workPos = posSet(cnt - 2)
                            workLenStr = (posSet(cnt - 1) - posSet(cnt - 2)) + 1
                            tmp = stBuffer.Substring(workPos, workLenStr)
                            '『,』を外す
                            tmpAfter = Replace(tmp, ",", "")
                            '『"』を外す
                            tmpAfter = Replace(tmpAfter, """", "")
                            'stBuffer
                            stBuffer = Replace(stBuffer, tmp, tmpAfter)
                            pos = 0
                        End If
                    End If
                End While
                cols = Split(stBuffer, ",")
                If (rowCnt <> 1) And (rowCnt <> 2) Then
                    'Insert into table 
                End If
                'If UCase(cols(0)) = "TOTAL" Then
                '    Exit While ' Assign 2nd header, then close
                'End If
                textLines.Add(cols)

            End While
        Catch ex As Exception
            cReader.Close()
        Finally
            cReader.Close()
        End Try
        strArr = textLines.ToArray
        'Verify that if the records / data not formated, show the error information to the user
        If strArr.Count < 2 Then
            Call showMsg("Status: Upload Failed <br><br> The CSV file is not valid format!!!", "")
            Exit Sub
        End If
        'Pass the paramters to Update in Datatabase
        Dim _SonyPartOrderModel As SonyPartOrderModel = New SonyPartOrderModel()
        Dim _SonyPartOrderControl As SonyPartOrderControl = New SonyPartOrderControl()
        _SonyPartOrderModel.UPDPG = "Class.Analysis.vb"
        _SonyPartOrderModel.FILE_NAME = fileName
        _SonyPartOrderModel.SRC_FILE_NAME = strSrcFileName
        _SonyPartOrderModel.UserId = userid
        _SonyPartOrderModel.UPLOAD_USER = userName
        _SonyPartOrderModel.SHIP_TO_BRANCH_CODE = DropListLocation.SelectedItem.Value
        _SonyPartOrderModel.SHIP_TO_BRANCH = DropListLocation.SelectedItem.Text


        'Upload to csv file to database for Other Update (GSPN to MultiVendor)
        Dim blStatus As Boolean = _SonyPartOrderControl.AddModifyPartOrder(strArr, _SonyPartOrderModel)
        'If Error Raised then show the errors
        If Not blStatus Then
            Call showMsg("Status: Upload Failed <br><br>  Please check the content of file ", "")
            Exit Sub
        Else
            Call showMsg("Status: Upload Success <br><br> The file has been uploaded", "")
        End If

    End Sub

    Private Sub Item104(filenameFullPath As String, fileName As String, userid As String, userName As String, strSrcFileName As String)
        'Read the content from stored file
        Dim cReader
        Dim textLines As New List(Of String())
        Dim strArr()() As String
        Try
            'StreamReader
            cReader = New System.IO.StreamReader(filenameFullPath, System.Text.Encoding.Default)
            Dim rowCnt As Integer

            Dim cols() As String
            Dim colsHead() As String 'ヘッダ１行目

            Dim colsValues() As String
            While (cReader.Peek() >= 0)

                Dim stBuffer As String = cReader.ReadLine()
                rowCnt = rowCnt + 1

                Dim pos As Integer = 0

                Dim posSet() As Integer
                Dim cnt As Integer = 0
                Dim key As String = """"
                Dim tmp As String = ""
                Dim tmpAfter As String = ""
                Dim workPos As Integer
                Dim workLenStr As Integer
                While True
                    If rowCnt = 1 Then
                        colsHead = Split(stBuffer, ",")
                        Exit While ' Assign 1st header, then close
                    Else
                        colsValues = Split(stBuffer, ",")
                    End If
                    pos = stBuffer.IndexOf(key, pos)
                    If pos = -1 Then
                        Exit While
                    Else
                        ReDim Preserve posSet(cnt)
                        posSet(cnt) = pos
                        cnt = cnt + 1
                        pos = pos + 1
                        If cnt Mod 2 = 0 Then

                            workPos = posSet(cnt - 2)
                            workLenStr = (posSet(cnt - 1) - posSet(cnt - 2)) + 1
                            tmp = stBuffer.Substring(workPos, workLenStr)
                            '『,』を外す
                            tmpAfter = Replace(tmp, ",", "")
                            '『"』を外す
                            tmpAfter = Replace(tmpAfter, """", "")
                            'stBuffer
                            stBuffer = Replace(stBuffer, tmp, tmpAfter)
                            pos = 0
                        End If
                    End If
                End While
                cols = Split(stBuffer, ",")
                If (rowCnt <> 1) And (rowCnt <> 2) Then
                    'Insert into table 
                End If
                'If UCase(cols(0)) = "TOTAL" Then
                '    Exit While ' Assign 2nd header, then close
                'End If
                textLines.Add(cols)

            End While
        Catch ex As Exception
            cReader.Close()
        Finally
            cReader.Close()
        End Try
        strArr = textLines.ToArray
        'Verify that if the records / data not formated, show the error information to the user
        If strArr.Count < 2 Then
            Call showMsg("Status: Upload Failed <br><br> The CSV file is not valid format!!!", "")
            Exit Sub
        End If
        'Pass the paramters to Update in Datatabase
        Dim _SonyRpsiInquiryModel As SonyRpsiInquiryModel = New SonyRpsiInquiryModel()
        Dim _SonyRpsiInquiryControl As SonyRpsiInquiryControl = New SonyRpsiInquiryControl()
        _SonyRpsiInquiryModel.UPDPG = "Class.Analysis.vb"
        _SonyRpsiInquiryModel.FILE_NAME = fileName
        _SonyRpsiInquiryModel.SRC_FILE_NAME = strSrcFileName
        _SonyRpsiInquiryModel.UserId = userid
        _SonyRpsiInquiryModel.UPLOAD_USER = userName
        _SonyRpsiInquiryModel.SHIP_TO_BRANCH_CODE = DropListLocation.SelectedItem.Value
        _SonyRpsiInquiryModel.SHIP_TO_BRANCH = DropListLocation.SelectedItem.Text


        'Upload to csv file to database for Other Update (GSPN to MultiVendor)
        Dim blStatus As Boolean = _SonyRpsiInquiryControl.AddModifyRpsiInquiry(strArr, _SonyRpsiInquiryModel)
        'If Error Raised then show the errors
        If Not blStatus Then
            Call showMsg("Status: Upload Failed <br><br>  Please check the content of file ", "")
            Exit Sub
        Else
            Call showMsg("Status: Upload Success <br><br> The file has been uploaded", "")
        End If

    End Sub


    Private Sub Item105(filenameFullPath As String, fileName As String, userid As String, userName As String, strSrcFileName As String)
        'Read the content from stored file
        Dim cReader
        Dim textLines As New List(Of String())
        Dim strArr()() As String
        Try
            'StreamReader
            cReader = New System.IO.StreamReader(filenameFullPath, System.Text.Encoding.Default)
            Dim rowCnt As Integer

            Dim cols() As String
            Dim colsHead() As String 'ヘッダ１行目

            Dim colsValues() As String
            While (cReader.Peek() >= 0)

                Dim stBuffer As String = cReader.ReadLine()
                rowCnt = rowCnt + 1

                Dim pos As Integer = 0

                Dim posSet() As Integer
                Dim cnt As Integer = 0
                Dim key As String = """"
                Dim tmp As String = ""
                Dim tmpAfter As String = ""
                Dim workPos As Integer
                Dim workLenStr As Integer
                While True
                    If rowCnt = 1 Then
                        colsHead = Split(stBuffer, ",")
                        Exit While ' Assign 1st header, then close
                    Else
                        colsValues = Split(stBuffer, ",")
                    End If
                    pos = stBuffer.IndexOf(key, pos)
                    If pos = -1 Then
                        Exit While
                    Else
                        ReDim Preserve posSet(cnt)
                        posSet(cnt) = pos
                        cnt = cnt + 1
                        pos = pos + 1
                        If cnt Mod 2 = 0 Then

                            workPos = posSet(cnt - 2)
                            workLenStr = (posSet(cnt - 1) - posSet(cnt - 2)) + 1
                            tmp = stBuffer.Substring(workPos, workLenStr)
                            '『,』を外す
                            tmpAfter = Replace(tmp, ",", "")
                            '『"』を外す
                            tmpAfter = Replace(tmpAfter, """", "")
                            'stBuffer
                            stBuffer = Replace(stBuffer, tmp, tmpAfter)
                            pos = 0
                        End If
                    End If
                End While
                cols = Split(stBuffer, ",")
                If (rowCnt <> 1) And (rowCnt <> 2) Then
                    'Insert into table 
                End If
                'If UCase(cols(0)) = "TOTAL" Then
                '    Exit While ' Assign 2nd header, then close
                'End If
                textLines.Add(cols)

            End While
        Catch ex As Exception
            cReader.Close()
        Finally
            cReader.Close()
        End Try
        strArr = textLines.ToArray
        'Verify that if the records / data not formated, show the error information to the user
        If strArr.Count < 2 Then
            Call showMsg("Status: Upload Failed <br><br> The CSV file is not valid format!!!", "")
            Exit Sub
        End If
        'Pass the paramters to Update in Datatabase
        Dim _SonyStocksModel As SonyStocksModel = New SonyStocksModel()
        Dim _SonyStocksControl As SonyStocksControl = New SonyStocksControl()
        _SonyStocksModel.UPDPG = "Class.Analysis.vb"
        _SonyStocksModel.FILE_NAME = fileName
        _SonyStocksModel.SRC_FILE_NAME = strSrcFileName
        _SonyStocksModel.UserId = userid
        _SonyStocksModel.UPLOAD_USER = userName
        _SonyStocksModel.SHIP_TO_BRANCH_CODE = DropListLocation.SelectedItem.Value
        _SonyStocksModel.SHIP_TO_BRANCH = DropListLocation.SelectedItem.Text


        'Upload to csv file to database for Other Update (GSPN to MultiVendor)
        Dim blStatus As Boolean = _SonyStocksControl.AddModifyStocks(strArr, _SonyStocksModel)
        'If Error Raised then show the errors
        If Not blStatus Then
            Call showMsg("Status: Upload Failed <br><br>  Please check the content of file ", "")
            Exit Sub
        Else
            Call showMsg("Status: Upload Success <br><br> The file has been uploaded", "")
        End If

    End Sub



    Private Sub Item106(filenameFullPath As String, fileName As String, userid As String, userName As String, strSrcFileName As String)
        'Read the content from stored file
        Dim cReader
        Dim textLines As New List(Of String())
        Dim strArr()() As String
        Try
            'StreamReader
            cReader = New System.IO.StreamReader(filenameFullPath, System.Text.Encoding.Default)
            Dim rowCnt As Integer

            Dim cols() As String
            Dim colsHead() As String 'ヘッダ１行目

            Dim colsValues() As String
            While (cReader.Peek() >= 0)

                Dim stBuffer As String = cReader.ReadLine()
                rowCnt = rowCnt + 1

                Dim pos As Integer = 0

                Dim posSet() As Integer
                Dim cnt As Integer = 0
                Dim key As String = """"
                Dim tmp As String = ""
                Dim tmpAfter As String = ""
                Dim workPos As Integer
                Dim workLenStr As Integer
                While True
                    If rowCnt = 1 Then
                        colsHead = Split(stBuffer, ",")
                        Exit While ' Assign 1st header, then close
                    Else
                        colsValues = Split(stBuffer, ",")
                    End If
                    pos = stBuffer.IndexOf(key, pos)
                    If pos = -1 Then
                        Exit While
                    Else
                        ReDim Preserve posSet(cnt)
                        posSet(cnt) = pos
                        cnt = cnt + 1
                        pos = pos + 1
                        If cnt Mod 2 = 0 Then

                            workPos = posSet(cnt - 2)
                            workLenStr = (posSet(cnt - 1) - posSet(cnt - 2)) + 1
                            tmp = stBuffer.Substring(workPos, workLenStr)
                            '『,』を外す
                            tmpAfter = Replace(tmp, ",", "")
                            '『"』を外す
                            tmpAfter = Replace(tmpAfter, """", "")
                            'stBuffer
                            stBuffer = Replace(stBuffer, tmp, tmpAfter)
                            pos = 0
                        End If
                    End If
                End While
                cols = Split(stBuffer, ",")
                If (rowCnt <> 1) And (rowCnt <> 2) Then
                    'Insert into table 
                End If
                'If UCase(cols(0)) = "TOTAL" Then
                '    Exit While ' Assign 2nd header, then close
                'End If
                textLines.Add(cols)

            End While
        Catch ex As Exception
            cReader.Close()
        Finally
            cReader.Close()
        End Try
        strArr = textLines.ToArray
        'Verify that if the records / data not formated, show the error information to the user
        If strArr.Count < 2 Then
            Call showMsg("Status: Upload Failed <br><br> The CSV file is not valid format!!!", "")
            Exit Sub
        End If
        'Pass the paramters to Update in Datatabase
        Dim _SonyB2bDateWiseSalesModel As SonyB2bDateWiseSalesModel = New SonyB2bDateWiseSalesModel()
        Dim _SonyB2bDateWiseSalesControl As SonyB2bDateWiseSalesControl = New SonyB2bDateWiseSalesControl()
        _SonyB2bDateWiseSalesModel.UPDPG = "Class.Analysis.vb"
        _SonyB2bDateWiseSalesModel.FILE_NAME = fileName
        _SonyB2bDateWiseSalesModel.SRC_FILE_NAME = strSrcFileName
        _SonyB2bDateWiseSalesModel.UserId = userid
        _SonyB2bDateWiseSalesModel.UPLOAD_USER = userName
        _SonyB2bDateWiseSalesModel.SHIP_TO_BRANCH_CODE = DropListLocation.SelectedItem.Value
        _SonyB2bDateWiseSalesModel.SHIP_TO_BRANCH = DropListLocation.SelectedItem.Text


        'Upload to csv file to database for Other Update (GSPN to MultiVendor)
        Dim blStatus As Boolean = _SonyB2bDateWiseSalesControl.AddModifyB2bDateWiseSales(strArr, _SonyB2bDateWiseSalesModel)
        'If Error Raised then show the errors
        If Not blStatus Then
            Call showMsg("Status: Upload Failed <br><br>  Please check the content of file ", "")
            Exit Sub
        Else
            Call showMsg("Status: Upload Success <br><br> The file has been uploaded", "")
        End If

    End Sub


    Private Sub Item107(filenameFullPath As String, fileName As String, userid As String, userName As String, strSrcFileName As String)
        'Read the content from stored file
        Dim cReader
        Dim textLines As New List(Of String())
        Dim strArr()() As String
        Try
            'StreamReader
            cReader = New System.IO.StreamReader(filenameFullPath, System.Text.Encoding.Default)
            Dim rowCnt As Integer

            Dim cols() As String
            Dim colsHead() As String 'ヘッダ１行目

            Dim colsValues() As String
            While (cReader.Peek() >= 0)

                Dim stBuffer As String = cReader.ReadLine()
                rowCnt = rowCnt + 1

                Dim pos As Integer = 0

                Dim posSet() As Integer
                Dim cnt As Integer = 0
                Dim key As String = """"
                Dim tmp As String = ""
                Dim tmpAfter As String = ""
                Dim workPos As Integer
                Dim workLenStr As Integer
                While True
                    If rowCnt = 1 Then
                        colsHead = Split(stBuffer, ",")
                        Exit While ' Assign 1st header, then close
                    Else
                        colsValues = Split(stBuffer, ",")
                    End If
                    pos = stBuffer.IndexOf(key, pos)
                    If pos = -1 Then
                        Exit While
                    Else
                        ReDim Preserve posSet(cnt)
                        posSet(cnt) = pos
                        cnt = cnt + 1
                        pos = pos + 1
                        If cnt Mod 2 = 0 Then

                            workPos = posSet(cnt - 2)
                            workLenStr = (posSet(cnt - 1) - posSet(cnt - 2)) + 1
                            tmp = stBuffer.Substring(workPos, workLenStr)
                            '『,』を外す
                            tmpAfter = Replace(tmp, ",", "")
                            '『"』を外す
                            tmpAfter = Replace(tmpAfter, """", "")
                            'stBuffer
                            stBuffer = Replace(stBuffer, tmp, tmpAfter)
                            pos = 0
                        End If
                    End If
                End While
                cols = Split(stBuffer, ",")
                If (rowCnt <> 1) And (rowCnt <> 2) Then
                    'Insert into table 
                End If
                'If UCase(cols(0)) = "TOTAL" Then
                '    Exit While ' Assign 2nd header, then close
                'End If
                textLines.Add(cols)

            End While
        Catch ex As Exception
            cReader.Close()
        Finally
            cReader.Close()
        End Try
        strArr = textLines.ToArray
        'Verify that if the records / data not formated, show the error information to the user
        If strArr.Count < 2 Then
            Call showMsg("Status: Upload Failed <br><br> The CSV file is not valid format!!!", "")
            Exit Sub
        End If
        'Pass the paramters to Update in Datatabase
        Dim _SonyB2bStockAvailableModel As SonyB2bStockAvailableModel = New SonyB2bStockAvailableModel()
        Dim _SonyB2bStockAvailableControl As SonyB2bStockAvailableControl = New SonyB2bStockAvailableControl()
        _SonyB2bStockAvailableModel.UPDPG = "Class.Analysis.vb"
        _SonyB2bStockAvailableModel.FILE_NAME = fileName
        _SonyB2bStockAvailableModel.SRC_FILE_NAME = strSrcFileName
        _SonyB2bStockAvailableModel.UserId = userid
        _SonyB2bStockAvailableModel.UPLOAD_USER = userName
        _SonyB2bStockAvailableModel.SHIP_TO_BRANCH_CODE = DropListLocation.SelectedItem.Value
        _SonyB2bStockAvailableModel.SHIP_TO_BRANCH = DropListLocation.SelectedItem.Text


        'Upload to csv file to database for Other Update (GSPN to MultiVendor)
        Dim blStatus As Boolean = _SonyB2bStockAvailableControl.AddModifyB2bStockAvailable(strArr, _SonyB2bStockAvailableModel)
        'If Error Raised then show the errors
        If Not blStatus Then
            Call showMsg("Status: Upload Failed <br><br>  Please check the content of file ", "")
            Exit Sub
        Else
            Call showMsg("Status: Upload Success <br><br> The file has been uploaded", "")
        End If

    End Sub



    Private Sub Item108(filenameFullPath As String, fileName As String, userid As String, userName As String, strSrcFileName As String)
        'Read the content from stored file
        Dim cReader
        Dim textLines As New List(Of String())
        Dim strArr()() As String
        Try
            'StreamReader
            cReader = New System.IO.StreamReader(filenameFullPath, System.Text.Encoding.Default)
            Dim rowCnt As Integer

            Dim cols() As String
            Dim colsHead() As String 'ヘッダ１行目

            Dim colsValues() As String
            While (cReader.Peek() >= 0)

                Dim stBuffer As String = cReader.ReadLine()
                rowCnt = rowCnt + 1

                Dim pos As Integer = 0

                Dim posSet() As Integer
                Dim cnt As Integer = 0
                Dim key As String = """"
                Dim tmp As String = ""
                Dim tmpAfter As String = ""
                Dim workPos As Integer
                Dim workLenStr As Integer
                While True
                    If rowCnt = 1 Then
                        colsHead = Split(stBuffer, ",")
                        Exit While ' Assign 1st header, then close
                    Else
                        colsValues = Split(stBuffer, ",")
                    End If
                    pos = stBuffer.IndexOf(key, pos)
                    If pos = -1 Then
                        Exit While
                    Else
                        ReDim Preserve posSet(cnt)
                        posSet(cnt) = pos
                        cnt = cnt + 1
                        pos = pos + 1
                        If cnt Mod 2 = 0 Then

                            workPos = posSet(cnt - 2)
                            workLenStr = (posSet(cnt - 1) - posSet(cnt - 2)) + 1
                            tmp = stBuffer.Substring(workPos, workLenStr)
                            '『,』を外す
                            tmpAfter = Replace(tmp, ",", "")
                            '『"』を外す
                            tmpAfter = Replace(tmpAfter, """", "")
                            'stBuffer
                            stBuffer = Replace(stBuffer, tmp, tmpAfter)
                            pos = 0
                        End If
                    End If
                End While
                cols = Split(stBuffer, ",")
                If (rowCnt <> 1) And (rowCnt <> 2) Then
                    'Insert into table 
                End If
                'If UCase(cols(0)) = "TOTAL" Then
                '    Exit While ' Assign 2nd header, then close
                'End If
                textLines.Add(cols)

            End While
        Catch ex As Exception
            cReader.Close()
        Finally
            cReader.Close()
        End Try
        strArr = textLines.ToArray
        'Verify that if the records / data not formated, show the error information to the user
        If strArr.Count < 2 Then
            Call showMsg("Status: Upload Failed <br><br> The CSV file is not valid format!!!", "")
            Exit Sub
        End If
        'Pass the paramters to Update in Datatabase
        Dim _SonyAscGstTaxReportModel As SonyAscGstTaxReportModel = New SonyAscGstTaxReportModel()
        Dim _SonyAscGstTaxReportControl As SonyAscGstTaxReportControl = New SonyAscGstTaxReportControl()
        _SonyAscGstTaxReportModel.UPDPG = "Class.Analysis.vb"
        _SonyAscGstTaxReportModel.FILE_NAME = fileName
        _SonyAscGstTaxReportModel.SRC_FILE_NAME = strSrcFileName
        _SonyAscGstTaxReportModel.UserId = userid
        _SonyAscGstTaxReportModel.UPLOAD_USER = userName
        _SonyAscGstTaxReportModel.SHIP_TO_BRANCH_CODE = DropListLocation.SelectedItem.Value
        _SonyAscGstTaxReportModel.SHIP_TO_BRANCH = DropListLocation.SelectedItem.Text


        'Upload to csv file to database for Other Update (GSPN to MultiVendor)
        Dim blStatus As Boolean = _SonyAscGstTaxReportControl.AddModifyAscGstTaxReport(strArr, _SonyAscGstTaxReportModel)
        'If Error Raised then show the errors
        If Not blStatus Then
            Call showMsg("Status: Upload Failed <br><br>  Please check the content of file ", "")
            Exit Sub
        Else
            Call showMsg("Status: Upload Success <br><br> The file has been uploaded", "")
        End If

    End Sub


    Private Sub Item109(filenameFullPath As String, fileName As String, userid As String, userName As String, strSrcFileName As String)
        'Read the content from stored file
        Dim cReader
        Dim textLines As New List(Of String())
        Dim strArr()() As String
        Try
            'StreamReader
            cReader = New System.IO.StreamReader(filenameFullPath, System.Text.Encoding.Default)
            Dim rowCnt As Integer

            Dim cols() As String
            Dim colsHead() As String 'ヘッダ１行目

            Dim colsValues() As String
            While (cReader.Peek() >= 0)

                Dim stBuffer As String = cReader.ReadLine()
                rowCnt = rowCnt + 1

                Dim pos As Integer = 0

                Dim posSet() As Integer
                Dim cnt As Integer = 0
                Dim key As String = """"
                Dim tmp As String = ""
                Dim tmpAfter As String = ""
                Dim workPos As Integer
                Dim workLenStr As Integer
                While True
                    If rowCnt = 1 Then
                        colsHead = Split(stBuffer, ",")
                        Exit While ' Assign 1st header, then close
                    Else
                        colsValues = Split(stBuffer, ",")
                    End If
                    pos = stBuffer.IndexOf(key, pos)
                    If pos = -1 Then
                        Exit While
                    Else
                        ReDim Preserve posSet(cnt)
                        posSet(cnt) = pos
                        cnt = cnt + 1
                        pos = pos + 1
                        If cnt Mod 2 = 0 Then

                            workPos = posSet(cnt - 2)
                            workLenStr = (posSet(cnt - 1) - posSet(cnt - 2)) + 1
                            tmp = stBuffer.Substring(workPos, workLenStr)
                            '『,』を外す
                            tmpAfter = Replace(tmp, ",", "")
                            '『"』を外す
                            tmpAfter = Replace(tmpAfter, """", "")
                            'stBuffer
                            stBuffer = Replace(stBuffer, tmp, tmpAfter)
                            pos = 0
                        End If
                    End If
                End While
                cols = Split(stBuffer, ",")
                If (rowCnt <> 1) And (rowCnt <> 2) Then
                    'Insert into table 
                End If
                'If UCase(cols(0)) = "TOTAL" Then
                '    Exit While ' Assign 2nd header, then close
                'End If
                textLines.Add(cols)

            End While
        Catch ex As Exception
            cReader.Close()
        Finally
            cReader.Close()
        End Try
        strArr = textLines.ToArray
        'Verify that if the records / data not formated, show the error information to the user
        If strArr.Count < 2 Then
            Call showMsg("Status: Upload Failed <br><br> The CSV file is not valid format!!!", "")
            Exit Sub
        End If
        'Pass the paramters to Update in Datatabase
        Dim _SonyAscTaxReportModel As SonyAscTaxReportModel = New SonyAscTaxReportModel()
        Dim _SonyAscTaxReportControl As SonyAscTaxReportControl = New SonyAscTaxReportControl()
        _SonyAscTaxReportModel.UPDPG = "Class.Analysis.vb"
        _SonyAscTaxReportModel.FILE_NAME = fileName
        _SonyAscTaxReportModel.SRC_FILE_NAME = strSrcFileName
        _SonyAscTaxReportModel.UserId = userid
        _SonyAscTaxReportModel.UPLOAD_USER = userName
        _SonyAscTaxReportModel.SHIP_TO_BRANCH_CODE = DropListLocation.SelectedItem.Value
        _SonyAscTaxReportModel.SHIP_TO_BRANCH = DropListLocation.SelectedItem.Text


        'Upload to csv file to database for Other Update (GSPN to MultiVendor)
        Dim blStatus As Boolean = _SonyAscTaxReportControl.AddModifyAscTaxReport(strArr, _SonyAscTaxReportModel)
        'If Error Raised then show the errors
        If Not blStatus Then
            Call showMsg("Status: Upload Failed <br><br>  Please check the content of file ", "")
            Exit Sub
        Else
            Call showMsg("Status: Upload Success <br><br> The file has been uploaded", "")
        End If

    End Sub


    Private Sub Item110(filenameFullPath As String, fileName As String, userid As String, userName As String, strSrcFileName As String)
        'Read the content from stored file
        Dim cReader
        Dim textLines As New List(Of String())
        Dim strArr()() As String
        Try
            'StreamReader
            cReader = New System.IO.StreamReader(filenameFullPath, System.Text.Encoding.Default)
            Dim rowCnt As Integer

            Dim cols() As String
            Dim colsHead() As String 'ヘッダ１行目

            Dim colsValues() As String
            While (cReader.Peek() >= 0)

                Dim stBuffer As String = cReader.ReadLine()
                rowCnt = rowCnt + 1

                Dim pos As Integer = 0

                Dim posSet() As Integer
                Dim cnt As Integer = 0
                Dim key As String = """"
                Dim tmp As String = ""
                Dim tmpAfter As String = ""
                Dim workPos As Integer
                Dim workLenStr As Integer
                While True
                    If rowCnt = 1 Then
                        colsHead = Split(stBuffer, ",")
                        Exit While ' Assign 1st header, then close
                    Else
                        colsValues = Split(stBuffer, ",")
                    End If
                    pos = stBuffer.IndexOf(key, pos)
                    If pos = -1 Then
                        Exit While
                    Else
                        ReDim Preserve posSet(cnt)
                        posSet(cnt) = pos
                        cnt = cnt + 1
                        pos = pos + 1
                        If cnt Mod 2 = 0 Then

                            workPos = posSet(cnt - 2)
                            workLenStr = (posSet(cnt - 1) - posSet(cnt - 2)) + 1
                            tmp = stBuffer.Substring(workPos, workLenStr)
                            '『,』を外す
                            tmpAfter = Replace(tmp, ",", "")
                            '『"』を外す
                            tmpAfter = Replace(tmpAfter, """", "")
                            'stBuffer
                            stBuffer = Replace(stBuffer, tmp, tmpAfter)
                            pos = 0
                        End If
                    End If
                End While
                cols = Split(stBuffer, ",")
                If (rowCnt <> 1) And (rowCnt <> 2) Then
                    'Insert into table 
                End If
                'If UCase(cols(0)) = "TOTAL" Then
                '    Exit While ' Assign 2nd header, then close
                'End If
                textLines.Add(cols)

            End While
        Catch ex As Exception
            cReader.Close()
        Finally
            cReader.Close()
        End Try
        strArr = textLines.ToArray
        'Verify that if the records / data not formated, show the error information to the user
        If strArr.Count < 2 Then
            Call showMsg("Status: Upload Failed <br><br> The CSV file is not valid format!!!", "")
            Exit Sub
        End If
        'Pass the paramters to Update in Datatabase
        Dim _SonyRmsAscTaxReport01Model As SonyRmsAscTaxReport01Model = New SonyRmsAscTaxReport01Model()
        Dim _SonyRmsAscTaxReport01Control As SonyRmsAscTaxReport01Control = New SonyRmsAscTaxReport01Control()
        _SonyRmsAscTaxReport01Model.UPDPG = "Class.Analysis.vb"
        _SonyRmsAscTaxReport01Model.FILE_NAME = fileName
        _SonyRmsAscTaxReport01Model.SRC_FILE_NAME = strSrcFileName
        _SonyRmsAscTaxReport01Model.UserId = userid
        _SonyRmsAscTaxReport01Model.UPLOAD_USER = userName
        _SonyRmsAscTaxReport01Model.SHIP_TO_BRANCH_CODE = DropListLocation.SelectedItem.Value
        _SonyRmsAscTaxReport01Model.SHIP_TO_BRANCH = DropListLocation.SelectedItem.Text


        'Upload to csv file to database for Other Update (GSPN to MultiVendor)
        Dim blStatus As Boolean = _SonyRmsAscTaxReport01Control.AddModifyRmsAscTaxReport01(strArr, _SonyRmsAscTaxReport01Model)
        'If Error Raised then show the errors
        If Not blStatus Then
            Call showMsg("Status: Upload Failed <br><br>  Please check the content of file ", "")
            Exit Sub
        Else
            Call showMsg("Status: Upload Success <br><br> The file has been uploaded", "")
        End If

    End Sub


    Private Sub Item111(filenameFullPath As String, fileName As String, userid As String, userName As String, strSrcFileName As String)
        'Read the content from stored file
        Dim cReader
        Dim textLines As New List(Of String())
        Dim strArr()() As String
        Try
            'StreamReader
            cReader = New System.IO.StreamReader(filenameFullPath, System.Text.Encoding.Default)
            Dim rowCnt As Integer

            Dim cols() As String
            Dim colsHead() As String 'ヘッダ１行目

            Dim colsValues() As String
            While (cReader.Peek() >= 0)

                Dim stBuffer As String = cReader.ReadLine()
                rowCnt = rowCnt + 1

                Dim pos As Integer = 0

                Dim posSet() As Integer
                Dim cnt As Integer = 0
                Dim key As String = """"
                Dim tmp As String = ""
                Dim tmpAfter As String = ""
                Dim workPos As Integer
                Dim workLenStr As Integer
                While True
                    If rowCnt = 1 Then
                        colsHead = Split(stBuffer, ",")
                        Exit While ' Assign 1st header, then close
                    Else
                        colsValues = Split(stBuffer, ",")
                    End If
                    pos = stBuffer.IndexOf(key, pos)
                    If pos = -1 Then
                        Exit While
                    Else
                        ReDim Preserve posSet(cnt)
                        posSet(cnt) = pos
                        cnt = cnt + 1
                        pos = pos + 1
                        If cnt Mod 2 = 0 Then

                            workPos = posSet(cnt - 2)
                            workLenStr = (posSet(cnt - 1) - posSet(cnt - 2)) + 1
                            tmp = stBuffer.Substring(workPos, workLenStr)
                            '『,』を外す
                            tmpAfter = Replace(tmp, ",", "")
                            '『"』を外す
                            tmpAfter = Replace(tmpAfter, """", "")
                            'stBuffer
                            stBuffer = Replace(stBuffer, tmp, tmpAfter)
                            pos = 0
                        End If
                    End If
                End While
                cols = Split(stBuffer, ",")
                If (rowCnt <> 1) And (rowCnt <> 2) Then
                    'Insert into table 
                End If
                'If UCase(cols(0)) = "TOTAL" Then
                '    Exit While ' Assign 2nd header, then close
                'End If
                textLines.Add(cols)

            End While
        Catch ex As Exception
            cReader.Close()
        Finally
            cReader.Close()
        End Try
        strArr = textLines.ToArray
        'Verify that if the records / data not formated, show the error information to the user
        If strArr.Count < 2 Then
            Call showMsg("Status: Upload Failed <br><br> The CSV file is not valid format!!!", "")
            Exit Sub
        End If
        'Pass the paramters to Update in Datatabase
        Dim _SonyRmsClaimInvoiceDetailModel As SonyRmsClaimInvoiceDetailModel = New SonyRmsClaimInvoiceDetailModel()
        Dim _SonyRmsClaimInvoiceDetailControl As SonyRmsClaimInvoiceDetailControl = New SonyRmsClaimInvoiceDetailControl()
        _SonyRmsClaimInvoiceDetailModel.UPDPG = "Class.Analysis.vb"
        _SonyRmsClaimInvoiceDetailModel.FILE_NAME = fileName
        _SonyRmsClaimInvoiceDetailModel.SRC_FILE_NAME = strSrcFileName
        _SonyRmsClaimInvoiceDetailModel.UserId = userid
        _SonyRmsClaimInvoiceDetailModel.UPLOAD_USER = userName
        _SonyRmsClaimInvoiceDetailModel.SHIP_TO_BRANCH_CODE = DropListLocation.SelectedItem.Value
        _SonyRmsClaimInvoiceDetailModel.SHIP_TO_BRANCH = DropListLocation.SelectedItem.Text


        'Upload to csv file to database for Other Update (GSPN to MultiVendor)
        Dim blStatus As Boolean = _SonyRmsClaimInvoiceDetailControl.AddModifyRmsClaimInvoiceDetail(strArr, _SonyRmsClaimInvoiceDetailModel)
        'If Error Raised then show the errors
        If Not blStatus Then
            Call showMsg("Status: Upload Failed <br><br>  Please check the content of file ", "")
            Exit Sub
        Else
            Call showMsg("Status: Upload Success <br><br> The file has been uploaded", "")
        End If

    End Sub


    Private Sub Item112(filenameFullPath As String, fileName As String, userid As String, userName As String, strSrcFileName As String)
        'Read the content from stored file
        Dim cReader
        Dim textLines As New List(Of String())
        Dim strArr()() As String
        Try
            'StreamReader
            cReader = New System.IO.StreamReader(filenameFullPath, System.Text.Encoding.Default)
            Dim rowCnt As Integer

            Dim cols() As String
            Dim colsHead() As String 'ヘッダ１行目

            Dim colsValues() As String
            While (cReader.Peek() >= 0)

                Dim stBuffer As String = cReader.ReadLine()
                rowCnt = rowCnt + 1

                Dim pos As Integer = 0

                Dim posSet() As Integer
                Dim cnt As Integer = 0
                Dim key As String = """"
                Dim tmp As String = ""
                Dim tmpAfter As String = ""
                Dim workPos As Integer
                Dim workLenStr As Integer
                While True
                    If rowCnt = 1 Then
                        colsHead = Split(stBuffer, ",")
                        Exit While ' Assign 1st header, then close
                    Else
                        colsValues = Split(stBuffer, ",")
                    End If
                    pos = stBuffer.IndexOf(key, pos)
                    If pos = -1 Then
                        Exit While
                    Else
                        ReDim Preserve posSet(cnt)
                        posSet(cnt) = pos
                        cnt = cnt + 1
                        pos = pos + 1
                        If cnt Mod 2 = 0 Then

                            workPos = posSet(cnt - 2)
                            workLenStr = (posSet(cnt - 1) - posSet(cnt - 2)) + 1
                            tmp = stBuffer.Substring(workPos, workLenStr)
                            '『,』を外す
                            tmpAfter = Replace(tmp, ",", "")
                            '『"』を外す
                            tmpAfter = Replace(tmpAfter, """", "")
                            'stBuffer
                            stBuffer = Replace(stBuffer, tmp, tmpAfter)
                            pos = 0
                        End If
                    End If
                End While
                cols = Split(stBuffer, ",")
                If (rowCnt <> 1) And (rowCnt <> 2) Then
                    'Insert into table 
                End If
                'If UCase(cols(0)) = "TOTAL" Then
                '    Exit While ' Assign 2nd header, then close
                'End If
                textLines.Add(cols)

            End While
        Catch ex As Exception
            cReader.Close()
        Finally
            cReader.Close()
        End Try
        strArr = textLines.ToArray
        'Verify that if the records / data not formated, show the error information to the user
        If strArr.Count < 2 Then
            Call showMsg("Status: Upload Failed <br><br> The CSV file is not valid format!!!", "")
            Exit Sub
        End If
        'Pass the paramters to Update in Datatabase
        Dim _SonyAscInvoiceDataModel As SonyAscInvoiceDataModel = New SonyAscInvoiceDataModel()
        Dim _SonyAscInvoiceDataControl As SonyAscInvoiceDataControl = New SonyAscInvoiceDataControl()
        _SonyAscInvoiceDataModel.UPDPG = "Class.Analysis.vb"
        _SonyAscInvoiceDataModel.FILE_NAME = fileName
        _SonyAscInvoiceDataModel.SRC_FILE_NAME = strSrcFileName
        _SonyAscInvoiceDataModel.UserId = userid
        _SonyAscInvoiceDataModel.UPLOAD_USER = userName
        _SonyAscInvoiceDataModel.SHIP_TO_BRANCH_CODE = DropListLocation.SelectedItem.Value
        _SonyAscInvoiceDataModel.SHIP_TO_BRANCH = DropListLocation.SelectedItem.Text


        'Upload to csv file to database for Other Update (GSPN to MultiVendor)
        Dim blStatus As Boolean = _SonyAscInvoiceDataControl.AddModifyAscInvoiceData(strArr, _SonyAscInvoiceDataModel)
        'If Error Raised then show the errors
        If Not blStatus Then
            Call showMsg("Status: Upload Failed <br><br>  Please check the content of file ", "")
            Exit Sub
        Else
            Call showMsg("Status: Upload Success <br><br> The file has been uploaded", "")
        End If

    End Sub

    'Added for SonyDailyAbandon

    Private Sub Item113(filenameFullPath As String, fileName As String, userid As String, userName As String, strSrcFileName As String)
        'Read the content from stored file
        Dim cReader
        Dim textLines As New List(Of String())
        Dim strArr()() As String
        Try
            'StreamReader
            cReader = New System.IO.StreamReader(filenameFullPath, System.Text.Encoding.Default)
            Dim rowCnt As Integer

            Dim cols() As String
            Dim colsHead() As String 'ヘッダ１行目

            Dim colsValues() As String
            While (cReader.Peek() >= 0)

                Dim stBuffer As String = cReader.ReadLine()
                rowCnt = rowCnt + 1

                Dim pos As Integer = 0

                Dim posSet() As Integer
                Dim cnt As Integer = 0
                Dim key As String = """"
                Dim tmp As String = ""
                Dim tmpAfter As String = ""
                Dim workPos As Integer
                Dim workLenStr As Integer
                While True
                    If rowCnt = 1 Then
                        colsHead = Split(stBuffer, ",")
                        Exit While ' Assign 1st header, then close
                    Else
                        colsValues = Split(stBuffer, ",")
                    End If
                    pos = stBuffer.IndexOf(key, pos)
                    If pos = -1 Then
                        Exit While
                    Else
                        ReDim Preserve posSet(cnt)
                        posSet(cnt) = pos
                        cnt = cnt + 1
                        pos = pos + 1
                        If cnt Mod 2 = 0 Then

                            workPos = posSet(cnt - 2)
                            workLenStr = (posSet(cnt - 1) - posSet(cnt - 2)) + 1
                            tmp = stBuffer.Substring(workPos, workLenStr)
                            '『,』を外す
                            tmpAfter = Replace(tmp, ",", "")
                            '『"』を外す
                            tmpAfter = Replace(tmpAfter, """", "")
                            'stBuffer
                            stBuffer = Replace(stBuffer, tmp, tmpAfter)
                            pos = 0
                        End If
                    End If
                End While
                cols = Split(stBuffer, ",")
                If (rowCnt <> 1) And (rowCnt <> 2) Then
                    'Insert into table 
                End If
                'If UCase(cols(0)) = "TOTAL" Then
                '    Exit While ' Assign 2nd header, then close
                'End If
                textLines.Add(cols)

            End While
        Catch ex As Exception
            cReader.Close()
        Finally
            cReader.Close()
        End Try
        strArr = textLines.ToArray
        'Verify that if the records / data not formated, show the error information to the user
        If strArr.Count < 2 Then
            Call showMsg("Status: Upload Failed <br><br> The CSV file is not valid format!!!", "")
            Exit Sub
        End If
        'Pass the paramters to Update in Datatabase
        Dim _SonyDailyAbandonModel As SonyDailyAbandonModel = New SonyDailyAbandonModel()
        Dim _SonyDailyAbandonControl As SonyDailyAbandonControl = New SonyDailyAbandonControl()
        _SonyDailyAbandonModel.UPDPG = "Class.Analysis.vb"
        _SonyDailyAbandonModel.FILE_NAME = fileName
        _SonyDailyAbandonModel.SRC_FILE_NAME = strSrcFileName
        _SonyDailyAbandonModel.UserId = userid
        _SonyDailyAbandonModel.UPLOAD_USER = userName
        _SonyDailyAbandonModel.SHIP_TO_BRANCH_CODE = DropListLocation.SelectedItem.Value
        _SonyDailyAbandonModel.SHIP_TO_BRANCH = DropListLocation.SelectedItem.Text


        'Upload to csv file to database for Other Update (GSPN to MultiVendor)
        Dim blStatus As Boolean = _SonyDailyAbandonControl.AddModifyDailyAbandon(strArr, _SonyDailyAbandonModel)
        'If Error Raised then show the errors
        If Not blStatus Then
            Call showMsg("Status: Upload Failed <br><br>  Please check the content of file ", "")
            Exit Sub
        Else
            Call showMsg("Status: Upload Success <br><br> The file has been uploaded", "")
        End If

    End Sub

    'Added for Sony Daily Accmulated Report

    Private Sub Item114(filenameFullPath As String, fileName As String, userid As String, userName As String, strSrcFileName As String)
        'Read the content from stored file
        Dim cReader
        Dim textLines As New List(Of String())
        Dim strArr()() As String
        Try
            'StreamReader
            cReader = New System.IO.StreamReader(filenameFullPath, System.Text.Encoding.Default)
            Dim rowCnt As Integer

            Dim cols() As String
            Dim colsHead() As String 'ヘッダ１行目

            Dim colsValues() As String
            While (cReader.Peek() >= 0)

                Dim stBuffer As String = cReader.ReadLine()
                rowCnt = rowCnt + 1

                Dim pos As Integer = 0

                Dim posSet() As Integer
                Dim cnt As Integer = 0
                Dim key As String = """"
                Dim tmp As String = ""
                Dim tmpAfter As String = ""
                Dim workPos As Integer
                Dim workLenStr As Integer
                While True
                    If rowCnt = 1 Then
                        colsHead = Split(stBuffer, ",")
                        Exit While ' Assign 1st header, then close
                    Else
                        colsValues = Split(stBuffer, ",")
                    End If
                    pos = stBuffer.IndexOf(key, pos)
                    If pos = -1 Then
                        Exit While
                    Else
                        ReDim Preserve posSet(cnt)
                        posSet(cnt) = pos
                        cnt = cnt + 1
                        pos = pos + 1
                        If cnt Mod 2 = 0 Then

                            workPos = posSet(cnt - 2)
                            workLenStr = (posSet(cnt - 1) - posSet(cnt - 2)) + 1
                            tmp = stBuffer.Substring(workPos, workLenStr)
                            '『,』を外す
                            tmpAfter = Replace(tmp, ",", "")
                            '『"』を外す
                            tmpAfter = Replace(tmpAfter, """", "")
                            'stBuffer
                            stBuffer = Replace(stBuffer, tmp, tmpAfter)
                            pos = 0
                        End If
                    End If
                End While
                cols = Split(stBuffer, ",")
                If (rowCnt <> 1) And (rowCnt <> 2) Then
                    'Insert into table 
                End If
                'If UCase(cols(0)) = "TOTAL" Then
                '    Exit While ' Assign 2nd header, then close
                'End If
                textLines.Add(cols)

            End While
        Catch ex As Exception
            cReader.Close()
        Finally
            cReader.Close()
        End Try
        strArr = textLines.ToArray
        'Verify that if the records / data not formated, show the error information to the user
        If strArr.Count < 2 Then
            Call showMsg("Status: Upload Failed <br><br> The CSV file is not valid format!!!", "")
            Exit Sub
        End If
        'Pass the paramters to Update in Datatabase
        Dim _SonyDailyAccumulatedKPIReportModel As SonyDailyAccumulatedKPIReportModel = New SonyDailyAccumulatedKPIReportModel()
        Dim _SonyDailyAccumulatedKPIReportControl As SonyDailyAccumulatedKPIReportControl = New SonyDailyAccumulatedKPIReportControl()
        _SonyDailyAccumulatedKPIReportModel.UPDPG = "Class.Analysis.vb"
        _SonyDailyAccumulatedKPIReportModel.FILE_NAME = fileName
        _SonyDailyAccumulatedKPIReportModel.SRC_FILE_NAME = strSrcFileName
        _SonyDailyAccumulatedKPIReportModel.UserId = userid
        _SonyDailyAccumulatedKPIReportModel.UPLOAD_USER = userName
        _SonyDailyAccumulatedKPIReportModel.SHIP_TO_BRANCH_CODE = DropListLocation.SelectedItem.Value
        _SonyDailyAccumulatedKPIReportModel.SHIP_TO_BRANCH = DropListLocation.SelectedItem.Text


        'Upload to csv file to database for Other Update (GSPN to MultiVendor)
        Dim blStatus As Boolean = _SonyDailyAccumulatedKPIReportControl.AddModifyDailyAccumulatedKPIReport(strArr, _SonyDailyAccumulatedKPIReportModel)
        'If Error Raised then show the errors
        If Not blStatus Then
            Call showMsg("Status: Upload Failed <br><br>  Please check the content of file ", "")
            Exit Sub
        Else
            Call showMsg("Status: Upload Success <br><br> The file has been uploaded", "")
        End If

    End Sub

    'Added For SonyDaily_ASCPartsUsed
    Private Sub Item115(filenameFullPath As String, fileName As String, userid As String, userName As String, strSrcFileName As String)
        'Read the content from stored file
        Dim cReader
        Dim textLines As New List(Of String())
        Dim strArr()() As String
        Try
            'StreamReader
            cReader = New System.IO.StreamReader(filenameFullPath, System.Text.Encoding.Default)
            Dim rowCnt As Integer

            Dim cols() As String
            Dim colsHead() As String 'ヘッダ１行目

            Dim colsValues() As String
            While (cReader.Peek() >= 0)

                Dim stBuffer As String = cReader.ReadLine()
                rowCnt = rowCnt + 1

                Dim pos As Integer = 0

                Dim posSet() As Integer
                Dim cnt As Integer = 0
                Dim key As String = """"
                Dim tmp As String = ""
                Dim tmpAfter As String = ""
                Dim workPos As Integer
                Dim workLenStr As Integer
                While True
                    If rowCnt = 1 Then
                        colsHead = Split(stBuffer, ",")
                        Exit While ' Assign 1st header, then close
                    Else
                        colsValues = Split(stBuffer, ",")
                    End If
                    pos = stBuffer.IndexOf(key, pos)
                    If pos = -1 Then
                        Exit While
                    Else
                        ReDim Preserve posSet(cnt)
                        posSet(cnt) = pos
                        cnt = cnt + 1
                        pos = pos + 1
                        If cnt Mod 2 = 0 Then

                            workPos = posSet(cnt - 2)
                            workLenStr = (posSet(cnt - 1) - posSet(cnt - 2)) + 1
                            tmp = stBuffer.Substring(workPos, workLenStr)
                            '『,』を外す
                            tmpAfter = Replace(tmp, ",", "")
                            '『"』を外す
                            tmpAfter = Replace(tmpAfter, """", "")
                            'stBuffer
                            stBuffer = Replace(stBuffer, tmp, tmpAfter)
                            pos = 0
                        End If
                    End If
                End While
                cols = Split(stBuffer, ",")
                If (rowCnt <> 1) And (rowCnt <> 2) Then
                    'Insert into table 
                End If
                'If UCase(cols(0)) = "TOTAL" Then
                '    Exit While ' Assign 2nd header, then close
                'End If
                textLines.Add(cols)

            End While
        Catch ex As Exception
            cReader.Close()
        Finally
            cReader.Close()
        End Try
        strArr = textLines.ToArray
        'Verify that if the records / data not formated, show the error information to the user
        If strArr.Count < 2 Then
            Call showMsg("Status: Upload Failed <br><br> The CSV file is not valid format!!!", "")
            Exit Sub
        End If
        'Pass the paramters to Update in Datatabase
        Dim _SonyDailyASCPartsUsedModel As SonyDailyASCPartsUsedModel = New SonyDailyASCPartsUsedModel()
        Dim _SonyDailyASCPartsUsedControl As SonyDailyASCPartsUsedControl = New SonyDailyASCPartsUsedControl()
        _SonyDailyASCPartsUsedModel.UPDPG = "Class.Analysis.vb"
        _SonyDailyASCPartsUsedModel.FILE_NAME = fileName
        _SonyDailyASCPartsUsedModel.SRC_FILE_NAME = strSrcFileName
        _SonyDailyASCPartsUsedModel.UserId = userid
        _SonyDailyASCPartsUsedModel.UPLOAD_USER = userName
        _SonyDailyASCPartsUsedModel.SHIP_TO_BRANCH_CODE = DropListLocation.SelectedItem.Value
        _SonyDailyASCPartsUsedModel.SHIP_TO_BRANCH = DropListLocation.SelectedItem.Text


        'Upload to csv file to database for Other Update (GSPN to MultiVendor)
        Dim blStatus As Boolean = _SonyDailyASCPartsUsedControl.AddModifyDailyASCPartsUsed(strArr, _SonyDailyASCPartsUsedModel)
        'If Error Raised then show the errors
        If Not blStatus Then
            Call showMsg("Status: Upload Failed <br><br>  Please check the content of file ", "")
            Exit Sub
        Else
            Call showMsg("Status: Upload Success <br><br> The file has been uploaded", "")
        End If

    End Sub

    'Added for Daily Claim
    Private Sub Item116(filenameFullPath As String, fileName As String, userid As String, userName As String, strSrcFileName As String)
        'Read the content from stored file
        Dim cReader
        Dim textLines As New List(Of String())
        Dim strArr()() As String
        Try
            'StreamReader
            cReader = New System.IO.StreamReader(filenameFullPath, System.Text.Encoding.Default)
            Dim rowCnt As Integer

            Dim cols() As String
            Dim colsHead() As String 'ヘッダ１行目

            Dim colsValues() As String
            While (cReader.Peek() >= 0)

                Dim stBuffer As String = cReader.ReadLine()
                rowCnt = rowCnt + 1

                Dim pos As Integer = 0

                Dim posSet() As Integer
                Dim cnt As Integer = 0
                Dim key As String = """"
                Dim tmp As String = ""
                Dim tmpAfter As String = ""
                Dim workPos As Integer
                Dim workLenStr As Integer
                While True
                    If rowCnt = 1 Then
                        colsHead = Split(stBuffer, ",")
                        Exit While ' Assign 1st header, then close
                    Else
                        colsValues = Split(stBuffer, ",")
                    End If
                    pos = stBuffer.IndexOf(key, pos)
                    If pos = -1 Then
                        Exit While
                    Else
                        ReDim Preserve posSet(cnt)
                        posSet(cnt) = pos
                        cnt = cnt + 1
                        pos = pos + 1
                        If cnt Mod 2 = 0 Then

                            workPos = posSet(cnt - 2)
                            workLenStr = (posSet(cnt - 1) - posSet(cnt - 2)) + 1
                            tmp = stBuffer.Substring(workPos, workLenStr)
                            '『,』を外す
                            tmpAfter = Replace(tmp, ",", "")
                            '『"』を外す
                            tmpAfter = Replace(tmpAfter, """", "")
                            'stBuffer
                            stBuffer = Replace(stBuffer, tmp, tmpAfter)
                            pos = 0
                        End If
                    End If
                End While
                cols = Split(stBuffer, ",")
                If (rowCnt <> 1) And (rowCnt <> 2) Then
                    'Insert into table 
                End If
                'If UCase(cols(0)) = "TOTAL" Then
                '    Exit While ' Assign 2nd header, then close
                'End If
                textLines.Add(cols)

            End While
        Catch ex As Exception
            cReader.Close()
        Finally
            cReader.Close()
        End Try
        strArr = textLines.ToArray
        'Verify that if the records / data not formated, show the error information to the user
        If strArr.Count < 2 Then
            Call showMsg("Status: Upload Failed <br><br> The CSV file is not valid format!!!", "")
            Exit Sub
        End If
        'Pass the paramters to Update in Datatabase
        Dim _SonyDailyClaimModel As SonyDailyClaimModel = New SonyDailyClaimModel()
        Dim _SonyDailyClaimControl As SonyDailyClaimControl = New SonyDailyClaimControl()
        _SonyDailyClaimModel.UPDPG = "Class.Analysis.vb"
        _SonyDailyClaimModel.FILE_NAME = fileName
        _SonyDailyClaimModel.SRC_FILE_NAME = strSrcFileName
        _SonyDailyClaimModel.UserId = userid
        _SonyDailyClaimModel.UPLOAD_USER = userName
        _SonyDailyClaimModel.SHIP_TO_BRANCH_CODE = DropListLocation.SelectedItem.Value
        _SonyDailyClaimModel.SHIP_TO_BRANCH = DropListLocation.SelectedItem.Text


        'Upload to csv file to database for Other Update (GSPN to MultiVendor)
        Dim blStatus As Boolean = _SonyDailyClaimControl.AddModifyDailyClaim(strArr, _SonyDailyClaimModel)
        'If Error Raised then show the errors
        If Not blStatus Then
            Call showMsg("Status: Upload Failed <br><br>  Please check the content of file ", "")
            Exit Sub
        Else
            Call showMsg("Status: Upload Success <br><br> The file has been uploaded", "")
        End If

    End Sub

    'Added for Daily Delivered
    Private Sub Item117(filenameFullPath As String, fileName As String, userid As String, userName As String, strSrcFileName As String)
        'Read the content from stored file
        Dim cReader
        Dim textLines As New List(Of String())
        Dim strArr()() As String
        Try
            'StreamReader
            cReader = New System.IO.StreamReader(filenameFullPath, System.Text.Encoding.Default)
            Dim rowCnt As Integer

            Dim cols() As String
            Dim colsHead() As String 'ヘッダ１行目

            Dim colsValues() As String
            While (cReader.Peek() >= 0)

                Dim stBuffer As String = cReader.ReadLine()
                rowCnt = rowCnt + 1

                Dim pos As Integer = 0

                Dim posSet() As Integer
                Dim cnt As Integer = 0
                Dim key As String = """"
                Dim tmp As String = ""
                Dim tmpAfter As String = ""
                Dim workPos As Integer
                Dim workLenStr As Integer
                While True
                    If rowCnt = 1 Then
                        colsHead = Split(stBuffer, ",")
                        Exit While ' Assign 1st header, then close
                    Else
                        colsValues = Split(stBuffer, ",")
                    End If
                    pos = stBuffer.IndexOf(key, pos)
                    If pos = -1 Then
                        Exit While
                    Else
                        ReDim Preserve posSet(cnt)
                        posSet(cnt) = pos
                        cnt = cnt + 1
                        pos = pos + 1
                        If cnt Mod 2 = 0 Then

                            workPos = posSet(cnt - 2)
                            workLenStr = (posSet(cnt - 1) - posSet(cnt - 2)) + 1
                            tmp = stBuffer.Substring(workPos, workLenStr)
                            '『,』を外す
                            tmpAfter = Replace(tmp, ",", "")
                            '『"』を外す
                            tmpAfter = Replace(tmpAfter, """", "")
                            'stBuffer
                            stBuffer = Replace(stBuffer, tmp, tmpAfter)
                            pos = 0
                        End If
                    End If
                End While
                cols = Split(stBuffer, ",")
                If (rowCnt <> 1) And (rowCnt <> 2) Then
                    'Insert into table 
                End If
                'If UCase(cols(0)) = "TOTAL" Then
                '    Exit While ' Assign 2nd header, then close
                'End If
                textLines.Add(cols)

            End While
        Catch ex As Exception
            cReader.Close()
        Finally
            cReader.Close()
        End Try
        strArr = textLines.ToArray
        'Verify that if the records / data not formated, show the error information to the user
        If strArr.Count < 2 Then
            Call showMsg("Status: Upload Failed <br><br> The CSV file is not valid format!!!", "")
            Exit Sub
        End If
        'Pass the paramters to Update in Datatabase
        Dim _SonyDailyDeliveredModel As SonyDailyDeliveredModel = New SonyDailyDeliveredModel()
        Dim _SonyDailyDeliveredControl As SonyDailyDeliveredControl = New SonyDailyDeliveredControl()
        _SonyDailyDeliveredModel.UPDPG = "Class.Analysis.vb"
        _SonyDailyDeliveredModel.FILE_NAME = fileName
        _SonyDailyDeliveredModel.SRC_FILE_NAME = strSrcFileName
        _SonyDailyDeliveredModel.UserId = userid
        _SonyDailyDeliveredModel.UPLOAD_USER = userName
        _SonyDailyDeliveredModel.SHIP_TO_BRANCH_CODE = DropListLocation.SelectedItem.Value
        _SonyDailyDeliveredModel.SHIP_TO_BRANCH = DropListLocation.SelectedItem.Text


        'Upload to csv file to database for Other Update (GSPN to MultiVendor)
        Dim blStatus As Boolean = _SonyDailyDeliveredControl.AddModifyDailyDelivered(strArr, _SonyDailyDeliveredModel)
        'If Error Raised then show the errors
        If Not blStatus Then
            Call showMsg("Status: Upload Failed <br><br>  Please check the content of file ", "")
            Exit Sub
        Else
            Call showMsg("Status: Upload Success <br><br> The file has been uploaded", "")
        End If

    End Sub

    'Added for sonydailyOSReservaion
    Private Sub Item118(filenameFullPath As String, fileName As String, userid As String, userName As String, strSrcFileName As String)
        'Read the content from stored file
        Dim cReader
        Dim textLines As New List(Of String())
        Dim strArr()() As String
        Try
            'StreamReader
            cReader = New System.IO.StreamReader(filenameFullPath, System.Text.Encoding.Default)
            Dim rowCnt As Integer

            Dim cols() As String
            Dim colsHead() As String 'ヘッダ１行目

            Dim colsValues() As String
            While (cReader.Peek() >= 0)

                Dim stBuffer As String = cReader.ReadLine()
                rowCnt = rowCnt + 1

                Dim pos As Integer = 0

                Dim posSet() As Integer
                Dim cnt As Integer = 0
                Dim key As String = """"
                Dim tmp As String = ""
                Dim tmpAfter As String = ""
                Dim workPos As Integer
                Dim workLenStr As Integer
                While True
                    If rowCnt = 1 Then
                        colsHead = Split(stBuffer, ",")
                        Exit While ' Assign 1st header, then close
                    Else
                        colsValues = Split(stBuffer, ",")
                    End If
                    pos = stBuffer.IndexOf(key, pos)
                    If pos = -1 Then
                        Exit While
                    Else
                        ReDim Preserve posSet(cnt)
                        posSet(cnt) = pos
                        cnt = cnt + 1
                        pos = pos + 1
                        If cnt Mod 2 = 0 Then

                            workPos = posSet(cnt - 2)
                            workLenStr = (posSet(cnt - 1) - posSet(cnt - 2)) + 1
                            tmp = stBuffer.Substring(workPos, workLenStr)
                            '『,』を外す
                            tmpAfter = Replace(tmp, ",", "")
                            '『"』を外す
                            tmpAfter = Replace(tmpAfter, """", "")
                            'stBuffer
                            stBuffer = Replace(stBuffer, tmp, tmpAfter)
                            pos = 0
                        End If
                    End If
                End While
                cols = Split(stBuffer, ",")
                If (rowCnt <> 1) And (rowCnt <> 2) Then
                    'Insert into table 
                End If
                'If UCase(cols(0)) = "TOTAL" Then
                '    Exit While ' Assign 2nd header, then close
                'End If
                textLines.Add(cols)

            End While
        Catch ex As Exception
            cReader.Close()
        Finally
            cReader.Close()
        End Try
        strArr = textLines.ToArray
        'Verify that if the records / data not formated, show the error information to the user
        If strArr.Count < 2 Then
            Call showMsg("Status: Upload Failed <br><br> The CSV file is not valid format!!!", "")
            Exit Sub
        End If
        'Pass the paramters to Update in Datatabase
        Dim _SonyDailyOSReservationModel As SonyDailyOSReservationModel = New SonyDailyOSReservationModel()
        Dim _SonyDailyOSReservationControl As SonyDailyOSReservationControl = New SonyDailyOSReservationControl()
        _SonyDailyOSReservationModel.UPDPG = "Class.Analysis.vb"
        _SonyDailyOSReservationModel.FILE_NAME = fileName
        _SonyDailyOSReservationModel.SRC_FILE_NAME = strSrcFileName
        _SonyDailyOSReservationModel.UserId = userid
        _SonyDailyOSReservationModel.UPLOAD_USER = userName
        _SonyDailyOSReservationModel.SHIP_TO_BRANCH_CODE = DropListLocation.SelectedItem.Value
        _SonyDailyOSReservationModel.SHIP_TO_BRANCH = DropListLocation.SelectedItem.Text


        'Upload to csv file to database for Other Update (GSPN to MultiVendor)
        Dim blStatus As Boolean = _SonyDailyOSReservationControl.AddModifyDailyOSReservation(strArr, _SonyDailyOSReservationModel)
        'If Error Raised then show the errors
        If Not blStatus Then
            Call showMsg("Status: Upload Failed <br><br>  Please check the content of file ", "")
            Exit Sub
        Else
            Call showMsg("Status: Upload Success <br><br> The file has been uploaded", "")
        End If

    End Sub


    'Added for sonyDailyReceiveset
    Private Sub Item119(filenameFullPath As String, fileName As String, userid As String, userName As String, strSrcFileName As String)
        'Read the content from stored file
        Dim cReader
        Dim textLines As New List(Of String())
        Dim strArr()() As String
        Try
            'StreamReader
            cReader = New System.IO.StreamReader(filenameFullPath, System.Text.Encoding.Default)
            Dim rowCnt As Integer

            Dim cols() As String
            Dim colsHead() As String 'ヘッダ１行目

            Dim colsValues() As String
            While (cReader.Peek() >= 0)

                Dim stBuffer As String = cReader.ReadLine()
                rowCnt = rowCnt + 1

                Dim pos As Integer = 0

                Dim posSet() As Integer
                Dim cnt As Integer = 0
                Dim key As String = """"
                Dim tmp As String = ""
                Dim tmpAfter As String = ""
                Dim workPos As Integer
                Dim workLenStr As Integer
                While True
                    If rowCnt = 1 Then
                        colsHead = Split(stBuffer, ",")
                        Exit While ' Assign 1st header, then close
                    Else
                        colsValues = Split(stBuffer, ",")
                    End If
                    pos = stBuffer.IndexOf(key, pos)
                    If pos = -1 Then
                        Exit While
                    Else
                        ReDim Preserve posSet(cnt)
                        posSet(cnt) = pos
                        cnt = cnt + 1
                        pos = pos + 1
                        If cnt Mod 2 = 0 Then

                            workPos = posSet(cnt - 2)
                            workLenStr = (posSet(cnt - 1) - posSet(cnt - 2)) + 1
                            tmp = stBuffer.Substring(workPos, workLenStr)
                            '『,』を外す
                            tmpAfter = Replace(tmp, ",", "")
                            '『"』を外す
                            tmpAfter = Replace(tmpAfter, """", "")
                            'stBuffer
                            stBuffer = Replace(stBuffer, tmp, tmpAfter)
                            pos = 0
                        End If
                    End If
                End While
                cols = Split(stBuffer, ",")
                If (rowCnt <> 1) And (rowCnt <> 2) Then
                    'Insert into table 
                End If
                'If UCase(cols(0)) = "TOTAL" Then
                '    Exit While ' Assign 2nd header, then close
                'End If
                textLines.Add(cols)

            End While
        Catch ex As Exception
            cReader.Close()
        Finally
            cReader.Close()
        End Try
        strArr = textLines.ToArray
        'Verify that if the records / data not formated, show the error information to the user
        If strArr.Count < 2 Then
            Call showMsg("Status: Upload Failed <br><br> The CSV file is not valid format!!!", "")
            Exit Sub
        End If
        'Pass the paramters to Update in Datatabase
        Dim _SonyDailyReceivesetModel As SonyDailyReceivesetModel = New SonyDailyReceivesetModel()
        Dim _SonyDailyReceivesetControl As SonyDailyReceivesetControl = New SonyDailyReceivesetControl()
        _SonyDailyReceivesetModel.UPDPG = "Class.Analysis.vb"
        _SonyDailyReceivesetModel.FILE_NAME = fileName
        _SonyDailyReceivesetModel.SRC_FILE_NAME = strSrcFileName
        _SonyDailyReceivesetModel.UserId = userid
        _SonyDailyReceivesetModel.UPLOAD_USER = userName
        _SonyDailyReceivesetModel.SHIP_TO_BRANCH_CODE = DropListLocation.SelectedItem.Value
        _SonyDailyReceivesetModel.SHIP_TO_BRANCH = DropListLocation.SelectedItem.Text


        'Upload to csv file to database for Other Update (GSPN to MultiVendor)
        Dim blStatus As Boolean = _SonyDailyReceivesetControl.AddModifyDailyReceiveset(strArr, _SonyDailyReceivesetModel)
        'If Error Raised then show the errors
        If Not blStatus Then
            Call showMsg("Status: Upload Failed <br><br>  Please check the content of file ", "")
            Exit Sub
        Else
            Call showMsg("Status: Upload Success <br><br> The file has been uploaded", "")
        End If

    End Sub

    'Added for Daily_OS_specialtreatment

    Private Sub Item120(filenameFullPath As String, fileName As String, userid As String, userName As String, strSrcFileName As String)
        'Read the content from stored file
        Dim cReader
        Dim textLines As New List(Of String())
        Dim strArr()() As String
        Try
            'StreamReader
            cReader = New System.IO.StreamReader(filenameFullPath, System.Text.Encoding.Default)
            Dim rowCnt As Integer

            Dim cols() As String
            Dim colsHead() As String 'ヘッダ１行目

            Dim colsValues() As String
            While (cReader.Peek() >= 0)

                Dim stBuffer As String = cReader.ReadLine()
                rowCnt = rowCnt + 1

                Dim pos As Integer = 0

                Dim posSet() As Integer
                Dim cnt As Integer = 0
                Dim key As String = """"
                Dim tmp As String = ""
                Dim tmpAfter As String = ""
                Dim workPos As Integer
                Dim workLenStr As Integer
                While True
                    If rowCnt = 1 Then
                        colsHead = Split(stBuffer, ",")
                        Exit While ' Assign 1st header, then close
                    Else
                        colsValues = Split(stBuffer, ",")
                    End If
                    pos = stBuffer.IndexOf(key, pos)
                    If pos = -1 Then
                        Exit While
                    Else
                        ReDim Preserve posSet(cnt)
                        posSet(cnt) = pos
                        cnt = cnt + 1
                        pos = pos + 1
                        If cnt Mod 2 = 0 Then

                            workPos = posSet(cnt - 2)
                            workLenStr = (posSet(cnt - 1) - posSet(cnt - 2)) + 1
                            tmp = stBuffer.Substring(workPos, workLenStr)
                            '『,』を外す
                            tmpAfter = Replace(tmp, ",", "")
                            '『"』を外す
                            tmpAfter = Replace(tmpAfter, """", "")
                            'stBuffer
                            stBuffer = Replace(stBuffer, tmp, tmpAfter)
                            pos = 0
                        End If
                    End If
                End While
                cols = Split(stBuffer, ",")
                If (rowCnt <> 1) And (rowCnt <> 2) Then
                    'Insert into table 
                End If
                'If UCase(cols(0)) = "TOTAL" Then
                '    Exit While ' Assign 2nd header, then close
                'End If
                textLines.Add(cols)

            End While
        Catch ex As Exception
            cReader.Close()
        Finally
            cReader.Close()
        End Try
        strArr = textLines.ToArray
        'Verify that if the records / data not formated, show the error information to the user
        If strArr.Count < 2 Then
            Call showMsg("Status: Upload Failed <br><br> The CSV file is not valid format!!!", "")
            Exit Sub
        End If
        'Pass the paramters to Update in Datatabase
        Dim _SonyDaily_OS_specialtreatmentModel As SonyDailyOsSpecialtreatmentModel = New SonyDailyOsSpecialtreatmentModel()
        Dim _SonyDaily_OS_specialtreatmentcontrol As SonyDailyOsSpecialtreatmentControl = New SonyDailyOsSpecialtreatmentControl()
        _SonyDaily_OS_specialtreatmentModel.UPDPG = "Class.Analysis.vb"
        _SonyDaily_OS_specialtreatmentModel.FILE_NAME = fileName
        _SonyDaily_OS_specialtreatmentModel.SRC_FILE_NAME = strSrcFileName
        _SonyDaily_OS_specialtreatmentModel.UserId = userid
        _SonyDaily_OS_specialtreatmentModel.UPLOAD_USER = userName
        _SonyDaily_OS_specialtreatmentModel.SHIP_TO_BRANCH_CODE = DropListLocation.SelectedItem.Value
        _SonyDaily_OS_specialtreatmentModel.SHIP_TO_BRANCH = DropListLocation.SelectedItem.Text


        'Upload to csv file to database for Other Update (GSPN to MultiVendor)
        Dim blStatus As Boolean = _SonyDaily_OS_specialtreatmentcontrol.AddDailyOSSpecialtreatment(strArr, _SonyDaily_OS_specialtreatmentModel)
        'If Error Raised then show the errors
        If Not blStatus Then
            Call showMsg("Status: Upload Failed <br><br>  Please check the content of file ", "")
            Exit Sub
        Else
            Call showMsg("Status: Upload Success <br><br> The file has been uploaded", "")
        End If

    End Sub

    'Added for Daily_Acct Stmt

    Private Sub Item121(filenameFullPath As String, fileName As String, userid As String, userName As String, strSrcFileName As String)
        'Read the content from stored file
        Dim cReader
        Dim textLines As New List(Of String())
        Dim strArr()() As String
        Try
            'StreamReader
            cReader = New System.IO.StreamReader(filenameFullPath, System.Text.Encoding.Default)
            Dim rowCnt As Integer

            Dim cols() As String
            Dim colsHead() As String 'ヘッダ１行目

            Dim colsValues() As String
            While (cReader.Peek() >= 0)

                Dim stBuffer As String = cReader.ReadLine()
                rowCnt = rowCnt + 1

                Dim pos As Integer = 0

                Dim posSet() As Integer
                Dim cnt As Integer = 0
                Dim key As String = """"
                Dim tmp As String = ""
                Dim tmpAfter As String = ""
                Dim workPos As Integer
                Dim workLenStr As Integer
                While True
                    If rowCnt = 1 Then
                        colsHead = Split(stBuffer, ",")
                        Exit While ' Assign 1st header, then close
                    Else
                        colsValues = Split(stBuffer, ",")
                    End If
                    pos = stBuffer.IndexOf(key, pos)
                    If pos = -1 Then
                        Exit While
                    Else
                        ReDim Preserve posSet(cnt)
                        posSet(cnt) = pos
                        cnt = cnt + 1
                        pos = pos + 1
                        If cnt Mod 2 = 0 Then

                            workPos = posSet(cnt - 2)
                            workLenStr = (posSet(cnt - 1) - posSet(cnt - 2)) + 1
                            tmp = stBuffer.Substring(workPos, workLenStr)
                            '『,』を外す
                            tmpAfter = Replace(tmp, ",", "")
                            '『"』を外す
                            tmpAfter = Replace(tmpAfter, """", "")
                            'stBuffer
                            stBuffer = Replace(stBuffer, tmp, tmpAfter)
                            pos = 0
                        End If
                    End If
                End While
                cols = Split(stBuffer, ",")
                If (rowCnt <> 1) And (rowCnt <> 2) Then
                    'Insert into table 
                End If
                'If UCase(cols(0)) = "TOTAL" Then
                '    Exit While ' Assign 2nd header, then close
                'End If
                textLines.Add(cols)

            End While
        Catch ex As Exception
            cReader.Close()
        Finally
            cReader.Close()
        End Try
        strArr = textLines.ToArray
        'Verify that if the records / data not formated, show the error information to the user
        If strArr.Count < 2 Then
            Call showMsg("Status: Upload Failed <br><br> The CSV file is not valid format!!!", "")
            Exit Sub
        End If
        'Pass the paramters to Update in Datatabase
        Dim _Acct_stmtModel As SonyAcctStmtModel = New SonyAcctStmtModel()
        Dim _Acct_stmtControl As SonyAcctStmtControl = New SonyAcctStmtControl()
        _Acct_stmtModel.UPDPG = "Class.Analysis.vb"
        _Acct_stmtModel.FILE_NAME = fileName
        _Acct_stmtModel.SRC_FILE_NAME = strSrcFileName
        _Acct_stmtModel.UserId = userid
        _Acct_stmtModel.UPLOAD_USER = userName
        _Acct_stmtModel.SHIP_TO_BRANCH_CODE = DropListLocation.SelectedItem.Value
        _Acct_stmtModel.SHIP_TO_BRANCH = DropListLocation.SelectedItem.Text


        'Upload to csv file to database for Other Update (GSPN to MultiVendor)
        Dim blStatus As Boolean = _Acct_stmtControl.AddModifyAcct_stmt(strArr, _Acct_stmtModel)
        'If Error Raised then show the errors
        If Not blStatus Then
            Call showMsg("Status: Upload Failed <br><br>  Please check the content of file ", "")
            Exit Sub
        Else
            Call showMsg("Status: Upload Success <br><br> The file has been uploaded", "")
        End If

    End Sub


    'Added for Daily_RepairsetSet_NP

    Private Sub Item122(filenameFullPath As String, fileName As String, userid As String, userName As String, strSrcFileName As String)
        'Read the content from stored file
        Dim cReader
        Dim textLines As New List(Of String())
        Dim strArr()() As String
        Try
            'StreamReader
            cReader = New System.IO.StreamReader(filenameFullPath, System.Text.Encoding.Default)
            Dim rowCnt As Integer

            Dim cols() As String
            Dim colsHead() As String 'ヘッダ１行目

            Dim colsValues() As String
            While (cReader.Peek() >= 0)

                Dim stBuffer As String = cReader.ReadLine()
                rowCnt = rowCnt + 1

                Dim pos As Integer = 0

                Dim posSet() As Integer
                Dim cnt As Integer = 0
                Dim key As String = """"
                Dim tmp As String = ""
                Dim tmpAfter As String = ""
                Dim workPos As Integer
                Dim workLenStr As Integer
                While True
                    If rowCnt = 1 Then
                        colsHead = Split(stBuffer, ",")
                        Exit While ' Assign 1st header, then close
                    Else
                        colsValues = Split(stBuffer, ",")
                    End If
                    pos = stBuffer.IndexOf(key, pos)
                    If pos = -1 Then
                        Exit While
                    Else
                        ReDim Preserve posSet(cnt)
                        posSet(cnt) = pos
                        cnt = cnt + 1
                        pos = pos + 1
                        If cnt Mod 2 = 0 Then

                            workPos = posSet(cnt - 2)
                            workLenStr = (posSet(cnt - 1) - posSet(cnt - 2)) + 1
                            tmp = stBuffer.Substring(workPos, workLenStr)
                            '『,』を外す
                            tmpAfter = Replace(tmp, ",", "")
                            '『"』を外す
                            tmpAfter = Replace(tmpAfter, """", "")
                            'stBuffer
                            stBuffer = Replace(stBuffer, tmp, tmpAfter)
                            pos = 0
                        End If
                    End If
                End While
                cols = Split(stBuffer, ",")
                If (rowCnt <> 1) And (rowCnt <> 2) Then
                    'Insert into table 
                End If
                'If UCase(cols(0)) = "TOTAL" Then
                '    Exit While ' Assign 2nd header, then close
                'End If
                textLines.Add(cols)

            End While
        Catch ex As Exception
            cReader.Close()
        Finally
            cReader.Close()
        End Try
        strArr = textLines.ToArray
        'Verify that if the records / data not formated, show the error information to the user
        If strArr.Count < 2 Then
            Call showMsg("Status: Upload Failed <br><br> The CSV file is not valid format!!!", "")
            Exit Sub
        End If
        'Pass the paramters to Update in Datatabase
        Dim _Daily_RepairsetSet_NPModel As SonyDailyRepairsetSetNPModel = New SonyDailyRepairsetSetNPModel()
        Dim _Daily_RepairsetSet_NPControl As SonyDailyRepairsetSetNPControl = New SonyDailyRepairsetSetNPControl()
        _Daily_RepairsetSet_NPModel.UPDPG = "Class.Analysis.vb"
        _Daily_RepairsetSet_NPModel.FILE_NAME = fileName
        _Daily_RepairsetSet_NPModel.SRC_FILE_NAME = strSrcFileName
        _Daily_RepairsetSet_NPModel.UserId = userid
        _Daily_RepairsetSet_NPModel.UPLOAD_USER = userName
        _Daily_RepairsetSet_NPModel.SHIP_TO_BRANCH_CODE = DropListLocation.SelectedItem.Value
        _Daily_RepairsetSet_NPModel.SHIP_TO_BRANCH = DropListLocation.SelectedItem.Text


        'Upload to csv file to database for Other Update (GSPN to MultiVendor)
        Dim blStatus As Boolean = _Daily_RepairsetSet_NPControl.AddModifyDailyRepairsetSetNP(strArr, _Daily_RepairsetSet_NPModel)
        'If Error Raised then show the errors
        If Not blStatus Then
            Call showMsg("Status: Upload Failed <br><br>  Please check the content of file ", "")
            Exit Sub
        Else
            Call showMsg("Status: Upload Success <br><br> The file has been uploaded", "")
        End If

    End Sub



    'Added for Daily_UnDeliveredSet

    Private Sub Item123(filenameFullPath As String, fileName As String, userid As String, userName As String, strSrcFileName As String)
        'Read the content from stored file
        Dim cReader
        Dim textLines As New List(Of String())
        Dim strArr()() As String
        Try
            'StreamReader
            cReader = New System.IO.StreamReader(filenameFullPath, System.Text.Encoding.Default)
            Dim rowCnt As Integer

            Dim cols() As String
            Dim colsHead() As String 'ヘッダ１行目

            Dim colsValues() As String
            While (cReader.Peek() >= 0)

                Dim stBuffer As String = cReader.ReadLine()
                rowCnt = rowCnt + 1

                Dim pos As Integer = 0

                Dim posSet() As Integer
                Dim cnt As Integer = 0
                Dim key As String = """"
                Dim tmp As String = ""
                Dim tmpAfter As String = ""
                Dim workPos As Integer
                Dim workLenStr As Integer
                While True
                    If rowCnt = 1 Then
                        colsHead = Split(stBuffer, ",")
                        Exit While ' Assign 1st header, then close
                    Else
                        colsValues = Split(stBuffer, ",")
                    End If
                    pos = stBuffer.IndexOf(key, pos)
                    If pos = -1 Then
                        Exit While
                    Else
                        ReDim Preserve posSet(cnt)
                        posSet(cnt) = pos
                        cnt = cnt + 1
                        pos = pos + 1
                        If cnt Mod 2 = 0 Then

                            workPos = posSet(cnt - 2)
                            workLenStr = (posSet(cnt - 1) - posSet(cnt - 2)) + 1
                            tmp = stBuffer.Substring(workPos, workLenStr)
                            '『,』を外す
                            tmpAfter = Replace(tmp, ",", "")
                            '『"』を外す
                            tmpAfter = Replace(tmpAfter, """", "")
                            'stBuffer
                            stBuffer = Replace(stBuffer, tmp, tmpAfter)
                            pos = 0
                        End If
                    End If
                End While
                cols = Split(stBuffer, ",")
                If (rowCnt <> 1) And (rowCnt <> 2) Then
                    'Insert into table 
                End If
                'If UCase(cols(0)) = "TOTAL" Then
                '    Exit While ' Assign 2nd header, then close
                'End If
                textLines.Add(cols)

            End While
        Catch ex As Exception
            cReader.Close()
        Finally
            cReader.Close()
        End Try
        strArr = textLines.ToArray
        'Verify that if the records / data not formated, show the error information to the user
        If strArr.Count < 2 Then
            Call showMsg("Status: Upload Failed <br><br> The CSV file is not valid format!!!", "")
            Exit Sub
        End If
        'Pass the paramters to Update in Datatabase
        Dim _Sony_daily_UndeliveredSetModel As SonyDailyUndeliveredSetModel = New SonyDailyUndeliveredSetModel()
        Dim _SonydailyUndeliveredSetcontroll As SonydailyUndeliveredSetcontroll = New SonydailyUndeliveredSetcontroll()
        _Sony_daily_UndeliveredSetModel.UPDPG = "Class.Analysis.vb"
        _Sony_daily_UndeliveredSetModel.FILE_NAME = fileName
        _Sony_daily_UndeliveredSetModel.SRC_FILE_NAME = strSrcFileName
        _Sony_daily_UndeliveredSetModel.UserId = userid
        _Sony_daily_UndeliveredSetModel.UPLOAD_USER = userName
        _Sony_daily_UndeliveredSetModel.SHIP_TO_BRANCH_CODE = DropListLocation.SelectedItem.Value
        _Sony_daily_UndeliveredSetModel.SHIP_TO_BRANCH = DropListLocation.SelectedItem.Text


        'Upload to csv file to database for Other Update (GSPN to MultiVendor)
        Dim blStatus As Boolean = _SonydailyUndeliveredSetcontroll.AddSonydailyUndeliveredSetcontroll(strArr, _Sony_daily_UndeliveredSetModel)
        'If Error Raised then show the errors
        If Not blStatus Then
            Call showMsg("Status: Upload Failed <br><br>  Please check the content of file ", "")
            Exit Sub
        Else
            Call showMsg("Status: Upload Success <br><br> The file has been uploaded", "")
        End If

    End Sub


    'Added for Daily_UnRepaipairset_NP

    Private Sub Item124(filenameFullPath As String, fileName As String, userid As String, userName As String, strSrcFileName As String)
        'Read the content from stored file
        Dim cReader
        Dim textLines As New List(Of String())
        Dim strArr()() As String
        Try
            'StreamReader
            cReader = New System.IO.StreamReader(filenameFullPath, System.Text.Encoding.Default)
            Dim rowCnt As Integer

            Dim cols() As String
            Dim colsHead() As String 'ヘッダ１行目

            Dim colsValues() As String
            While (cReader.Peek() >= 0)

                Dim stBuffer As String = cReader.ReadLine()
                rowCnt = rowCnt + 1

                Dim pos As Integer = 0

                Dim posSet() As Integer
                Dim cnt As Integer = 0
                Dim key As String = """"
                Dim tmp As String = ""
                Dim tmpAfter As String = ""
                Dim workPos As Integer
                Dim workLenStr As Integer
                While True
                    If rowCnt = 1 Then
                        colsHead = Split(stBuffer, ",")
                        Exit While ' Assign 1st header, then close
                    Else
                        colsValues = Split(stBuffer, ",")
                    End If
                    pos = stBuffer.IndexOf(key, pos)
                    If pos = -1 Then
                        Exit While
                    Else
                        ReDim Preserve posSet(cnt)
                        posSet(cnt) = pos
                        cnt = cnt + 1
                        pos = pos + 1
                        If cnt Mod 2 = 0 Then

                            workPos = posSet(cnt - 2)
                            workLenStr = (posSet(cnt - 1) - posSet(cnt - 2)) + 1
                            tmp = stBuffer.Substring(workPos, workLenStr)
                            '『,』を外す
                            tmpAfter = Replace(tmp, ",", "")
                            '『"』を外す
                            tmpAfter = Replace(tmpAfter, """", "")
                            'stBuffer
                            stBuffer = Replace(stBuffer, tmp, tmpAfter)
                            pos = 0
                        End If
                    End If
                End While
                cols = Split(stBuffer, ",")
                If (rowCnt <> 1) And (rowCnt <> 2) Then
                    'Insert into table 
                End If
                'If UCase(cols(0)) = "TOTAL" Then
                '    Exit While ' Assign 2nd header, then close
                'End If
                textLines.Add(cols)

            End While
        Catch ex As Exception
            cReader.Close()
        Finally
            cReader.Close()
        End Try
        strArr = textLines.ToArray
        'Verify that if the records / data not formated, show the error information to the user
        If strArr.Count < 2 Then
            Call showMsg("Status: Upload Failed <br><br> The CSV file is not valid format!!!", "")
            Exit Sub
        End If
        'Pass the paramters to Update in Datatabase
        Dim _SonyDailyUnRepaipairsetNPModel As SonyDailyUnRepaipairsetNPModel = New SonyDailyUnRepaipairsetNPModel()
        Dim _SonyDailyUnRepairsetNPControll As SonyDailyUnRepairsetNPControll = New SonyDailyUnRepairsetNPControll()
        _SonyDailyUnRepaipairsetNPModel.UPDPG = "Class.Analysis.vb"
        _SonyDailyUnRepaipairsetNPModel.FILE_NAME = fileName
        _SonyDailyUnRepaipairsetNPModel.SRC_FILE_NAME = strSrcFileName
        _SonyDailyUnRepaipairsetNPModel.UserId = userid
        _SonyDailyUnRepaipairsetNPModel.UPLOAD_USER = userName
        _SonyDailyUnRepaipairsetNPModel.SHIP_TO_BRANCH_CODE = DropListLocation.SelectedItem.Value
        _SonyDailyUnRepaipairsetNPModel.SHIP_TO_BRANCH = DropListLocation.SelectedItem.Text


        'Upload to csv file to database for Other Update (GSPN to MultiVendor)
        Dim blStatus As Boolean = _SonyDailyUnRepairsetNPControll.AddDailyUnRepairsetNPControll(strArr, _SonyDailyUnRepaipairsetNPModel)
        'If Error Raised then show the errors
        If Not blStatus Then
            Call showMsg("Status: Upload Failed <br><br>  Please check the content of file ", "")
            Exit Sub
        Else
            Call showMsg("Status: Upload Success <br><br> The file has been uploaded", "")
        End If

    End Sub

    'Added for Monthly_Reservationvation

    Private Sub Item125(filenameFullPath As String, fileName As String, userid As String, userName As String, strSrcFileName As String)
        'Read the content from stored file
        Dim cReader
        Dim textLines As New List(Of String())
        Dim strArr()() As String
        Try
            'StreamReader
            cReader = New System.IO.StreamReader(filenameFullPath, System.Text.Encoding.Default)
            Dim rowCnt As Integer

            Dim cols() As String
            Dim colsHead() As String 'ヘッダ１行目

            Dim colsValues() As String
            While (cReader.Peek() >= 0)

                Dim stBuffer As String = cReader.ReadLine()
                rowCnt = rowCnt + 1

                Dim pos As Integer = 0

                Dim posSet() As Integer
                Dim cnt As Integer = 0
                Dim key As String = """"
                Dim tmp As String = ""
                Dim tmpAfter As String = ""
                Dim workPos As Integer
                Dim workLenStr As Integer
                While True
                    If rowCnt = 1 Then
                        colsHead = Split(stBuffer, ",")
                        Exit While ' Assign 1st header, then close
                    Else
                        colsValues = Split(stBuffer, ",")
                    End If
                    pos = stBuffer.IndexOf(key, pos)
                    If pos = -1 Then
                        Exit While
                    Else
                        ReDim Preserve posSet(cnt)
                        posSet(cnt) = pos
                        cnt = cnt + 1
                        pos = pos + 1
                        If cnt Mod 2 = 0 Then

                            workPos = posSet(cnt - 2)
                            workLenStr = (posSet(cnt - 1) - posSet(cnt - 2)) + 1
                            tmp = stBuffer.Substring(workPos, workLenStr)
                            '『,』を外す
                            tmpAfter = Replace(tmp, ",", "")
                            '『"』を外す
                            tmpAfter = Replace(tmpAfter, """", "")
                            'stBuffer
                            stBuffer = Replace(stBuffer, tmp, tmpAfter)
                            pos = 0
                        End If
                    End If
                End While
                cols = Split(stBuffer, ",")
                If (rowCnt <> 1) And (rowCnt <> 2) Then
                    'Insert into table 
                End If
                'If UCase(cols(0)) = "TOTAL" Then
                '    Exit While ' Assign 2nd header, then close
                'End If
                textLines.Add(cols)

            End While
        Catch ex As Exception
            cReader.Close()
        Finally
            cReader.Close()
        End Try
        strArr = textLines.ToArray
        'Verify that if the records / data not formated, show the error information to the user
        If strArr.Count < 2 Then
            Call showMsg("Status: Upload Failed <br><br> The CSV file is not valid format!!!", "")
            Exit Sub
        End If
        'Pass the paramters to Update in Datatabase
        Dim _SonyMonthlyReservationvationModel As SonyMonthlyReservationvationModel = New SonyMonthlyReservationvationModel()
        Dim _SonyMonthlyReservationvationControl As SonyMonthlyReservationvationControl = New SonyMonthlyReservationvationControl()
        _SonyMonthlyReservationvationModel.UPDPG = "Class.Analysis.vb"
        _SonyMonthlyReservationvationModel.FILE_NAME = fileName
        _SonyMonthlyReservationvationModel.SRC_FILE_NAME = strSrcFileName
        _SonyMonthlyReservationvationModel.UserId = userid
        _SonyMonthlyReservationvationModel.UPLOAD_USER = userName
        _SonyMonthlyReservationvationModel.SHIP_TO_BRANCH_CODE = DropListLocation.SelectedItem.Value
        _SonyMonthlyReservationvationModel.SHIP_TO_BRANCH = DropListLocation.SelectedItem.Text


        'Upload to csv file to database for Other Update (GSPN to MultiVendor)
        Dim blStatus As Boolean = _SonyMonthlyReservationvationControl.AddSonyMonthlyReservationvation(strArr, _SonyMonthlyReservationvationModel)
        'If Error Raised then show the errors
        If Not blStatus Then
            Call showMsg("Status: Upload Failed <br><br>  Please check the content of file ", "")
            Exit Sub
        Else
            Call showMsg("Status: Upload Success <br><br> The file has been uploaded", "")
        End If

    End Sub



    Private Sub Item126(filenameFullPath As String, fileName As String, userid As String, userName As String, strSrcFileName As String)
        'Read the content from stored file
        Dim cReader
        Dim textLines As New List(Of String())
        Dim strArr()() As String
        Try
            'StreamReader
            cReader = New System.IO.StreamReader(filenameFullPath, System.Text.Encoding.Default)
            Dim rowCnt As Integer

            Dim cols() As String
            Dim colsHead() As String 'ヘッダ１行目

            Dim colsValues() As String
            While (cReader.Peek() >= 0)

                Dim stBuffer As String = cReader.ReadLine()
                rowCnt = rowCnt + 1

                Dim pos As Integer = 0

                Dim posSet() As Integer
                Dim cnt As Integer = 0
                Dim key As String = """"
                Dim tmp As String = ""
                Dim tmpAfter As String = ""
                Dim workPos As Integer
                Dim workLenStr As Integer
                While True
                    If rowCnt = 1 Then
                        colsHead = Split(stBuffer, ",")
                        Exit While ' Assign 1st header, then close
                    Else
                        colsValues = Split(stBuffer, ",")
                    End If
                    pos = stBuffer.IndexOf(key, pos)
                    If pos = -1 Then
                        Exit While
                    Else
                        ReDim Preserve posSet(cnt)
                        posSet(cnt) = pos
                        cnt = cnt + 1
                        pos = pos + 1
                        If cnt Mod 2 = 0 Then

                            workPos = posSet(cnt - 2)
                            workLenStr = (posSet(cnt - 1) - posSet(cnt - 2)) + 1
                            tmp = stBuffer.Substring(workPos, workLenStr)
                            '『,』を外す
                            tmpAfter = Replace(tmp, ",", "")
                            '『"』を外す
                            tmpAfter = Replace(tmpAfter, """", "")
                            'stBuffer
                            stBuffer = Replace(stBuffer, tmp, tmpAfter)
                            pos = 0
                        End If
                    End If
                End While
                cols = Split(stBuffer, ",")
                If (rowCnt <> 1) And (rowCnt <> 2) Then
                    'Insert into table 
                End If
                'If UCase(cols(0)) = "TOTAL" Then
                '    Exit While ' Assign 2nd header, then close
                'End If
                textLines.Add(cols)

            End While
        Catch ex As Exception
            cReader.Close()
        Finally
            cReader.Close()
        End Try
        strArr = textLines.ToArray
        'Verify that if the records / data not formated, show the error information to the user
        If strArr.Count < 2 Then
            Call showMsg("Status: Upload Failed <br><br> The CSV file is not valid format!!!", "")
            Exit Sub
        End If
        'Pass the paramters to Update in Datatabase
        Dim _SonyMonthlyRepairsetModel As SonyMonthlyRepairsetModel = New SonyMonthlyRepairsetModel()
        Dim _SonyMonthlyRepairsetControl As SonyMonthlyRepairsetControl = New SonyMonthlyRepairsetControl()
        _SonyMonthlyRepairsetModel.UPDPG = "Class.Analysis.vb"
        _SonyMonthlyRepairsetModel.FILE_NAME = fileName
        _SonyMonthlyRepairsetModel.SRC_FILE_NAME = strSrcFileName
        _SonyMonthlyRepairsetModel.UserId = userid
        _SonyMonthlyRepairsetModel.UPLOAD_USER = userName
        _SonyMonthlyRepairsetModel.SHIP_TO_BRANCH_CODE = DropListLocation.SelectedItem.Value
        _SonyMonthlyRepairsetModel.SHIP_TO_BRANCH = DropListLocation.SelectedItem.Text


        'Upload to csv file to database for Other Update (GSPN to MultiVendor)
        Dim blStatus As Boolean = _SonyMonthlyRepairsetControl.AddSonyMonthlyRepairset(strArr, _SonyMonthlyRepairsetModel)
        'If Error Raised then show the errors
        If Not blStatus Then
            Call showMsg("Status: Upload Failed <br><br>  Please check the content of file ", "")
            Exit Sub
        Else
            Call showMsg("Status: Upload Success <br><br> The file has been uploaded", "")
        End If

    End Sub



    Private Sub Item127(filenameFullPath As String, fileName As String, userid As String, userName As String, strSrcFileName As String)
        'Read the content from stored file
        Dim cReader
        Dim textLines As New List(Of String())
        Dim strArr()() As String
        Try
            'StreamReader
            cReader = New System.IO.StreamReader(filenameFullPath, System.Text.Encoding.Default)
            Dim rowCnt As Integer

            Dim cols() As String
            Dim colsHead() As String 'ヘッダ１行目

            Dim colsValues() As String
            While (cReader.Peek() >= 0)

                Dim stBuffer As String = cReader.ReadLine()
                rowCnt = rowCnt + 1

                Dim pos As Integer = 0

                Dim posSet() As Integer
                Dim cnt As Integer = 0
                Dim key As String = """"
                Dim tmp As String = ""
                Dim tmpAfter As String = ""
                Dim workPos As Integer
                Dim workLenStr As Integer
                While True
                    If rowCnt = 1 Then
                        colsHead = Split(stBuffer, ",")
                        Exit While ' Assign 1st header, then close
                    Else
                        colsValues = Split(stBuffer, ",")
                    End If
                    pos = stBuffer.IndexOf(key, pos)
                    If pos = -1 Then
                        Exit While
                    Else
                        ReDim Preserve posSet(cnt)
                        posSet(cnt) = pos
                        cnt = cnt + 1
                        pos = pos + 1
                        If cnt Mod 2 = 0 Then

                            workPos = posSet(cnt - 2)
                            workLenStr = (posSet(cnt - 1) - posSet(cnt - 2)) + 1
                            tmp = stBuffer.Substring(workPos, workLenStr)
                            '『,』を外す
                            tmpAfter = Replace(tmp, ",", "")
                            '『"』を外す
                            tmpAfter = Replace(tmpAfter, """", "")
                            'stBuffer
                            stBuffer = Replace(stBuffer, tmp, tmpAfter)
                            pos = 0
                        End If
                    End If
                End While
                cols = Split(stBuffer, ",")
                If (rowCnt <> 1) And (rowCnt <> 2) Then
                    'Insert into table 
                End If
                'If UCase(cols(0)) = "TOTAL" Then
                '    Exit While ' Assign 2nd header, then close
                'End If
                textLines.Add(cols)

            End While
        Catch ex As Exception
            cReader.Close()
        Finally
            cReader.Close()
        End Try
        strArr = textLines.ToArray
        'Verify that if the records / data not formated, show the error information to the user
        If strArr.Count < 2 Then
            Call showMsg("Status: Upload Failed <br><br> The CSV file is not valid format!!!", "")
            Exit Sub
        End If
        'Pass the paramters to Update in Datatabase
        Dim _SonyMonthlyAbandonModel As SonyMonthlyAbandonModel = New SonyMonthlyAbandonModel()
        Dim _SonyMonthlyAbandonControl As SonyMonthlyAbandonControl = New SonyMonthlyAbandonControl()
        _SonyMonthlyAbandonModel.UPDPG = "Class.Analysis.vb"
        _SonyMonthlyAbandonModel.FILE_NAME = fileName
        _SonyMonthlyAbandonModel.SRC_FILE_NAME = strSrcFileName
        _SonyMonthlyAbandonModel.UserId = userid
        _SonyMonthlyAbandonModel.UPLOAD_USER = userName
        _SonyMonthlyAbandonModel.SHIP_TO_BRANCH_CODE = DropListLocation.SelectedItem.Value
        _SonyMonthlyAbandonModel.SHIP_TO_BRANCH = DropListLocation.SelectedItem.Text


        'Upload to csv file to database for Other Update (GSPN to MultiVendor)
        Dim blStatus As Boolean = _SonyMonthlyAbandonControl.AddSonyMonthlyAbandon(strArr, _SonyMonthlyAbandonModel)
        'If Error Raised then show the errors
        If Not blStatus Then
            Call showMsg("Status: Upload Failed <br><br>  Please check the content of file ", "")
            Exit Sub
        Else
            Call showMsg("Status: Upload Success <br><br> The file has been uploaded", "")
        End If

    End Sub



    'Added for SonyMonthlySOMCClaim

    Private Sub Item128(filenameFullPath As String, fileName As String, userid As String, userName As String, strSrcFileName As String)
        'Read the content from stored file
        Dim cReader
        Dim textLines As New List(Of String())
        Dim strArr()() As String
        Try
            'StreamReader
            cReader = New System.IO.StreamReader(filenameFullPath, System.Text.Encoding.Default)
            Dim rowCnt As Integer

            Dim cols() As String
            Dim colsHead() As String 'ヘッダ１行目

            Dim colsValues() As String
            While (cReader.Peek() >= 0)

                Dim stBuffer As String = cReader.ReadLine()
                rowCnt = rowCnt + 1

                Dim pos As Integer = 0

                Dim posSet() As Integer
                Dim cnt As Integer = 0
                Dim key As String = """"
                Dim tmp As String = ""
                Dim tmpAfter As String = ""
                Dim workPos As Integer
                Dim workLenStr As Integer
                While True
                    If rowCnt = 1 Then
                        colsHead = Split(stBuffer, ",")
                        Exit While ' Assign 1st header, then close
                    Else
                        colsValues = Split(stBuffer, ",")
                    End If
                    pos = stBuffer.IndexOf(key, pos)
                    If pos = -1 Then
                        Exit While
                    Else
                        ReDim Preserve posSet(cnt)
                        posSet(cnt) = pos
                        cnt = cnt + 1
                        pos = pos + 1
                        If cnt Mod 2 = 0 Then

                            workPos = posSet(cnt - 2)
                            workLenStr = (posSet(cnt - 1) - posSet(cnt - 2)) + 1
                            tmp = stBuffer.Substring(workPos, workLenStr)
                            '『,』を外す
                            tmpAfter = Replace(tmp, ",", "")
                            '『"』を外す
                            tmpAfter = Replace(tmpAfter, """", "")
                            'stBuffer
                            stBuffer = Replace(stBuffer, tmp, tmpAfter)
                            pos = 0
                        End If
                    End If
                End While
                cols = Split(stBuffer, ",")
                If (rowCnt <> 1) And (rowCnt <> 2) Then
                    'Insert into table 
                End If
                'If UCase(cols(0)) = "TOTAL" Then
                '    Exit While ' Assign 2nd header, then close
                'End If
                textLines.Add(cols)

            End While
        Catch ex As Exception
            cReader.Close()
        Finally
            cReader.Close()
        End Try
        strArr = textLines.ToArray
        'Verify that if the records / data not formated, show the error information to the user
        If strArr.Count < 2 Then
            Call showMsg("Status: Upload Failed <br><br> The CSV file is not valid format!!!", "")
            Exit Sub
        End If
        'Pass the paramters to Update in Datatabase
        Dim _SonyMonthlySOMCClaimModel As SonyMonthlySOMCClaimModel = New SonyMonthlySOMCClaimModel()
        Dim _SonyMonthlySOMCClaimControl As SonyMonthlySOMCClaimControl = New SonyMonthlySOMCClaimControl()
        _SonyMonthlySOMCClaimModel.UPDPG = "Class.Analysis.vb"
        _SonyMonthlySOMCClaimModel.FILE_NAME = fileName
        _SonyMonthlySOMCClaimModel.SRC_FILE_NAME = strSrcFileName
        _SonyMonthlySOMCClaimModel.UserId = userid
        _SonyMonthlySOMCClaimModel.UPLOAD_USER = userName
        _SonyMonthlySOMCClaimModel.SHIP_TO_BRANCH_CODE = DropListLocation.SelectedItem.Value
        _SonyMonthlySOMCClaimModel.SHIP_TO_BRANCH = DropListLocation.SelectedItem.Text


        'Upload to csv file to database for Other Update (GSPN to MultiVendor)
        Dim blStatus As Boolean = _SonyMonthlySOMCClaimControl.AddModifyMonthlySOMCClaim(strArr, _SonyMonthlySOMCClaimModel)
        'If Error Raised then show the errors
        If Not blStatus Then
            Call showMsg("Status: Upload Failed <br><br>  Please check the content of file ", "")
            Exit Sub
        Else
            Call showMsg("Status: Upload Success <br><br> The file has been uploaded", "")
        End If

    End Sub



    Private Sub Item6(filenameFullPath As String, fileName As String, userid As String, userName As String, strSrcFileName As String)
        'Read the content from stored file
        Dim cReader
        Dim textLines As New List(Of String())
        Dim strArr()() As String
        Try
            'StreamReader の新しいインスタンスを生成する
            cReader = New System.IO.StreamReader(filenameFullPath, System.Text.Encoding.Default)
            Dim rowCnt As Integer
            '読み込んだものを追加で格納
            Dim cols() As String
            Dim colsHead() As String 'ヘッダ１行目
            '   Dim colsHead2() As String 'ヘッダ２行目
            Dim colsValues() As String
            While (cReader.Peek() >= 0)
                'ファイルを1 行ずつ読み込む
                Dim stBuffer As String = cReader.ReadLine()
                rowCnt = rowCnt + 1
                '『"』の位置取得
                Dim pos As Integer = 0
                '全ての『"』の位置情報をセット
                Dim posSet() As Integer
                Dim cnt As Integer = 0
                Dim key As String = """"
                Dim tmp As String = ""
                Dim tmpAfter As String = ""
                Dim workPos As Integer
                Dim workLenStr As Integer
                While True
                    If rowCnt = 1 Then
                        colsHead = Split(stBuffer, ",")
                        Exit While ' Assign 1st header, then close
                    Else
                        colsValues = Split(stBuffer, ",")
                    End If
                    pos = stBuffer.IndexOf(key, pos)
                    If pos = -1 Then
                        Exit While
                    Else
                        ReDim Preserve posSet(cnt)
                        posSet(cnt) = pos
                        cnt = cnt + 1
                        pos = pos + 1
                        If cnt Mod 2 = 0 Then
                            '2の倍数になったら(""で囲まれた文字列取得できたら"Then)その文字列の最初の位置と長さを取得
                            workPos = posSet(cnt - 2)
                            workLenStr = (posSet(cnt - 1) - posSet(cnt - 2)) + 1
                            tmp = stBuffer.Substring(workPos, workLenStr)
                            '『,』を外す
                            tmpAfter = Replace(tmp, ",", "")
                            '『"』を外す
                            tmpAfter = Replace(tmpAfter, """", "")
                            'stBufferを置換
                            stBuffer = Replace(stBuffer, tmp, tmpAfter)
                            pos = 0
                        End If
                    End If
                End While
                cols = Split(stBuffer, ",")
                If (rowCnt <> 1) And (rowCnt <> 2) Then
                    'Insert into table 
                End If
                If UCase(cols(0)) = "TOTAL" Then
                    Exit While ' Assign 2nd header, then close
                End If
                textLines.Add(cols)
                'csvファイル種類切り分けのため、ヘッダの項目名取得しておく
            End While
        Catch ex As Exception
            cReader.Close()
        Finally
            cReader.Close()
        End Try
        strArr = textLines.ToArray
        'Verify that if the records / data not formated, show the error information to the user
        If strArr.Count < 2 Then
            Call showMsg("Status: Upload Failed <br><br> The CSV file is not valid format!!!", "")
            Exit Sub
        End If
        'Pass the paramters to Update in Datatabase
        Dim _PrSummaryModel As PrSummaryModel = New PrSummaryModel()
        Dim _PrSummaryControl As PrSummaryControl = New PrSummaryControl()
        _PrSummaryModel.UPDPG = "Analysis_FileUpload.aspx"
        _PrSummaryModel.FileName = fileName
        _PrSummaryModel.SrcFileName = strSrcFileName
        _PrSummaryModel.UserId = userid
        _PrSummaryModel.UserName = userName
        _PrSummaryModel.ShipToBranchCode = DropListLocation.SelectedItem.Value
        _PrSummaryModel.ShipToBranch = DropListLocation.SelectedItem.Text
        '       _PrSummaryModel.LaborInvoiceNo = TextLaborInvoiceNo.Text
        '_PrSummaryModel.InvoiceDate = TextInvoiceDate.Text
        '_PrSummaryModel.Total = txtAmount.Text

        'Upload to csv file to database for Other Update (GSPN to MultiVendor)
        Dim blStatus As Boolean = _PrSummaryControl.AddModifyPrSummary(strArr, _PrSummaryModel)
        'If Error Raised then show the errors
        If Not blStatus Then
            Call showMsg("Status: Upload Failed <br><br>  Please check the content of file ", "")
            Exit Sub
        Else
            Call showMsg("Status: Upload Success <br><br> The file has been uploaded", "")
        End If

    End Sub

    Private Sub Item7(filenameFullPath As String, fileName As String, userid As String, userName As String, strSrcFileName As String)
        'Read the content from stored file
        Dim cReader
        Dim textLines As New List(Of String())
        Dim strArr()() As String
        Try
            'StreamReader の新しいインスタンスを生成する
            cReader = New System.IO.StreamReader(filenameFullPath, System.Text.Encoding.Default)
            Dim rowCnt As Integer
            '読み込んだものを追加で格納
            Dim cols() As String
            Dim colsHead() As String 'ヘッダ１行目
            '   Dim colsHead2() As String 'ヘッダ２行目
            Dim colsValues() As String
            While (cReader.Peek() >= 0)
                'ファイルを1 行ずつ読み込む
                Dim stBuffer As String = cReader.ReadLine()
                rowCnt = rowCnt + 1
                '『"』の位置取得
                Dim pos As Integer = 0
                '全ての『"』の位置情報をセット
                Dim posSet() As Integer
                Dim cnt As Integer = 0
                Dim key As String = """"
                Dim tmp As String = ""
                Dim tmpAfter As String = ""
                Dim workPos As Integer
                Dim workLenStr As Integer
                While True
                    If rowCnt = 1 Then
                        colsHead = Split(stBuffer, ",")
                        Exit While ' Assign 1st header, then close
                    Else
                        colsValues = Split(stBuffer, ",")
                    End If
                    pos = stBuffer.IndexOf(key, pos)
                    If pos = -1 Then
                        Exit While
                    Else
                        ReDim Preserve posSet(cnt)
                        posSet(cnt) = pos
                        cnt = cnt + 1
                        pos = pos + 1
                        If cnt Mod 2 = 0 Then
                            '2の倍数になったら(""で囲まれた文字列取得できたら"Then)その文字列の最初の位置と長さを取得
                            workPos = posSet(cnt - 2)
                            workLenStr = (posSet(cnt - 1) - posSet(cnt - 2)) + 1
                            tmp = stBuffer.Substring(workPos, workLenStr)
                            '『,』を外す
                            tmpAfter = Replace(tmp, ",", "")
                            '『"』を外す
                            tmpAfter = Replace(tmpAfter, """", "")
                            'stBufferを置換
                            stBuffer = Replace(stBuffer, tmp, tmpAfter)
                            pos = 0
                        End If
                    End If
                End While
                cols = Split(stBuffer, ",")
                If (rowCnt <> 1) And (rowCnt <> 2) Then
                    'Insert into table 
                End If
                If UCase(cols(0)) = "TOTAL" Then
                    Exit While ' Assign 2nd header, then close
                End If
                textLines.Add(cols)
                'csvファイル種類切り分けのため、ヘッダの項目名取得しておく
            End While
        Catch ex As Exception
            cReader.Close()
        Finally
            cReader.Close()
        End Try
        strArr = textLines.ToArray
        'Verify that if the records / data not formated, show the error information to the user
        If strArr.Count < 2 Then
            Call showMsg("Status: Upload Failed <br><br> The CSV file is not valid format!!!", "")
            Exit Sub
        End If
        'Pass the paramters to Update in Datatabase
        Dim _PrDetailModel As PrDetailModel = New PrDetailModel()
        Dim _PrDetailControl As PrDetailControl = New PrDetailControl()
        _PrDetailModel.UPDPG = "Analysis_FileUpload.aspx"
        _PrDetailModel.FileName = fileName
        _PrDetailModel.SrcFileName = strSrcFileName
        _PrDetailModel.UserId = userid
        _PrDetailModel.UserName = userName
        _PrDetailModel.ShipToBranchCode = DropListLocation.SelectedItem.Value
        _PrDetailModel.ShipToBranch = DropListLocation.SelectedItem.Text

        '       _PrSummaryModel.LaborInvoiceNo = TextLaborInvoiceNo.Text
        ' _PrDetailModel.InvoiceDate = TextInvoiceDate.Text
        ' _PrDetailModel.Total = txtAmount.Text

        'Upload to csv file to database for Other Update (GSPN to MultiVendor)
        Dim blStatus As Boolean = _PrDetailControl.AddModifyPrDetail(strArr, _PrDetailModel)
        'If Error Raised then show the errors
        If Not blStatus Then
            Call showMsg("Status: Upload Failed <br><br>  Please check the content of file ", "")
            Exit Sub
        Else
            Call showMsg("Status: Upload Success <br><br> The file has been uploaded", "")
        End If

    End Sub
    Private Sub Item8(filenameFullPath As String, fileName As String, userid As String, userName As String, strSrcFileName As String)
        'Read the content from stored file
        Dim cReader
        Dim textLines As New List(Of String())
        Dim strArr()() As String
        Try
            'StreamReader の新しいインスタンスを生成する
            cReader = New System.IO.StreamReader(filenameFullPath, System.Text.Encoding.Default)
            Dim rowCnt As Integer
            '読み込んだものを追加で格納
            Dim cols() As String
            Dim colsHead() As String 'ヘッダ１行目
            '   Dim colsHead2() As String 'ヘッダ２行目
            Dim colsValues() As String
            While (cReader.Peek() >= 0)
                'ファイルを1 行ずつ読み込む
                Dim stBuffer As String = cReader.ReadLine()
                rowCnt = rowCnt + 1
                '『"』の位置取得
                Dim pos As Integer = 0
                '全ての『"』の位置情報をセット
                Dim posSet() As Integer
                Dim cnt As Integer = 0
                Dim key As String = """"
                Dim tmp As String = ""
                Dim tmpAfter As String = ""
                Dim workPos As Integer
                Dim workLenStr As Integer
                While True
                    If rowCnt = 1 Then
                        colsHead = Split(stBuffer, ",")
                        Exit While ' Assign 1st header, then close
                    Else
                        colsValues = Split(stBuffer, ",")
                    End If
                    pos = stBuffer.IndexOf(key, pos)
                    If pos = -1 Then
                        Exit While
                    Else
                        ReDim Preserve posSet(cnt)
                        posSet(cnt) = pos
                        cnt = cnt + 1
                        pos = pos + 1
                        If cnt Mod 2 = 0 Then
                            '2の倍数になったら(""で囲まれた文字列取得できたら"Then)その文字列の最初の位置と長さを取得
                            workPos = posSet(cnt - 2)
                            workLenStr = (posSet(cnt - 1) - posSet(cnt - 2)) + 1
                            tmp = stBuffer.Substring(workPos, workLenStr)
                            '『,』を外す
                            tmpAfter = Replace(tmp, ",", "")
                            '『"』を外す
                            tmpAfter = Replace(tmpAfter, """", "")
                            'stBufferを置換
                            stBuffer = Replace(stBuffer, tmp, tmpAfter)
                            pos = 0
                        End If
                    End If
                End While
                cols = Split(stBuffer, ",")
                If (rowCnt <> 1) And (rowCnt <> 2) Then
                    'Insert into table 
                End If
                If UCase(cols(0)) = "TOTAL" Then
                    Exit While ' Assign 2nd header, then close
                End If
                textLines.Add(cols)
                'csvファイル種類切り分けのため、ヘッダの項目名取得しておく
            End While
        Catch ex As Exception
            cReader.Close()
        Finally
            cReader.Close()
        End Try
        strArr = textLines.ToArray
        'Verify that if the records / data not formated, show the error information to the user
        If strArr.Count < 2 Then
            Call showMsg("Status: Upload Failed <br><br> The CSV file is not valid format!!!", "")
            Exit Sub
        End If
        'Pass the paramters to Update in Datatabase
        Dim _GRecievedModel As GRecievedModel = New GRecievedModel()
        Dim _GRecievedControl As GRecievedControl = New GRecievedControl()
        _GRecievedModel.UPDPG = "Analysis_FileUpload.aspx"
        _GRecievedModel.FileName = fileName
        _GRecievedModel.SrcFileName = strSrcFileName
        _GRecievedModel.UserId = userid
        _GRecievedModel.UserName = userName
        _GRecievedModel.ShipToBranchCode = DropListLocation.SelectedItem.Value
        _GRecievedModel.ShipToBranch = DropListLocation.SelectedItem.Text
        Dim blStatus As Boolean = _GRecievedControl.AddModifyGRecieved(strArr, _GRecievedModel)
        'If Error Raised then show the errors
        If Not blStatus Then
            Call showMsg("Status: Upload Failed <br><br>  Please check the content of file ", "")
            Exit Sub
        Else
            Call showMsg("Status: Upload Success <br><br> The file has been uploaded", "")
        End If

    End Sub




    Private Sub Item8_1(filenameFullPath As String, fileName As String, userid As String, userName As String, strSrcFileName As String)
        'Read the content from stored file
        Dim cReader
        Dim textLines As New List(Of String())
        Dim strArr()() As String
        Try
            'StreamReader の新しいインスタンスを生成する
            cReader = New System.IO.StreamReader(filenameFullPath, System.Text.Encoding.Default)
            Dim rowCnt As Integer
            '読み込んだものを追加で格納
            Dim cols() As String
            Dim colsHead() As String 'ヘッダ１行目
            '   Dim colsHead2() As String 'ヘッダ２行目
            Dim colsValues() As String
            While (cReader.Peek() >= 0)
                'ファイルを1 行ずつ読み込む
                Dim stBuffer As String = cReader.ReadLine()
                rowCnt = rowCnt + 1
                '『"』の位置取得
                Dim pos As Integer = 0
                '全ての『"』の位置情報をセット
                Dim posSet() As Integer
                Dim cnt As Integer = 0
                Dim key As String = """"
                Dim tmp As String = ""
                Dim tmpAfter As String = ""
                Dim workPos As Integer
                Dim workLenStr As Integer
                While True
                    If rowCnt = 1 Then
                        colsHead = Split(stBuffer, ",")
                        Exit While ' Assign 1st header, then close
                    Else
                        colsValues = Split(stBuffer, ",")
                    End If
                    pos = stBuffer.IndexOf(key, pos)
                    If pos = -1 Then
                        Exit While
                    Else
                        ReDim Preserve posSet(cnt)
                        posSet(cnt) = pos
                        cnt = cnt + 1
                        pos = pos + 1
                        If cnt Mod 2 = 0 Then
                            '2の倍数になったら(""で囲まれた文字列取得できたら"Then)その文字列の最初の位置と長さを取得
                            workPos = posSet(cnt - 2)
                            workLenStr = (posSet(cnt - 1) - posSet(cnt - 2)) + 1
                            tmp = stBuffer.Substring(workPos, workLenStr)
                            '『,』を外す
                            tmpAfter = Replace(tmp, ",", "")
                            '『"』を外す
                            tmpAfter = Replace(tmpAfter, """", "")
                            'stBufferを置換
                            stBuffer = Replace(stBuffer, tmp, tmpAfter)
                            pos = 0
                        End If
                    End If
                End While
                cols = Split(stBuffer, ",")
                If (rowCnt <> 1) And (rowCnt <> 2) Then
                    'Insert into table 
                End If
                If UCase(cols(0)) = "TOTAL" Then
                    Exit While ' Assign 2nd header, then close
                End If
                textLines.Add(cols)
                'csvファイル種類切り分けのため、ヘッダの項目名取得しておく
            End While
        Catch ex As Exception
            cReader.Close()
        Finally
            cReader.Close()
        End Try
        strArr = textLines.ToArray
        'Verify that if the records / data not formated, show the error information to the user
        If strArr.Count < 1 Then
            Call showMsg("Status: Upload Failed <br><br> The CSV file is not valid format!!!", "")
            Exit Sub
        End If
        'Pass the paramters to Update in Datatabase
        Dim _GRecievedModel As GRecievedModel = New GRecievedModel()
        Dim _GRecievedControl As GRecievedControl = New GRecievedControl()
        _GRecievedModel.UPDPG = "Analysis_FileUpload.aspx"
        _GRecievedModel.FileName = fileName
        _GRecievedModel.SrcFileName = strSrcFileName
        _GRecievedModel.UserId = userid
        _GRecievedModel.UserName = userName
        _GRecievedModel.ShipToBranchCode = DropListLocation.SelectedItem.Value
        _GRecievedModel.ShipToBranch = DropListLocation.SelectedItem.Text
        Dim blStatus As Boolean = _GRecievedControl.AddModifyGDRecieved(strArr, _GRecievedModel)
        'If Error Raised then show the errors
        If Not blStatus Then
            Call showMsg("Status: Upload Failed <br><br>  Please check the content of file / Invoice summary doesnot contain", "")
            Exit Sub
        Else
            Call showMsg("Status: Upload Success <br><br> The file has been uploaded", "")
        End If

    End Sub




    Private Sub Item9(filenameFullPath As String, fileName As String, userid As String, userName As String, strSrcFileName As String, strMonth As String)
        'Read the content from stored file
        Dim cReader
        Dim textLines As New List(Of String())
        Dim strArr()() As String
        Try
            'StreamReader の新しいインスタンスを生成する
            cReader = New System.IO.StreamReader(filenameFullPath, System.Text.Encoding.Default)
            Dim rowCnt As Integer
            '読み込んだものを追加で格納
            Dim cols() As String
            Dim colsHead() As String 'ヘッダ１行目
            '   Dim colsHead2() As String 'ヘッダ２行目
            Dim colsValues() As String
            While (cReader.Peek() >= 0)
                'ファイルを1 行ずつ読み込む
                Dim stBuffer As String = cReader.ReadLine()
                rowCnt = rowCnt + 1
                '『"』の位置取得
                Dim pos As Integer = 0
                '全ての『"』の位置情報をセット
                Dim posSet() As Integer
                Dim cnt As Integer = 0
                Dim key As String = """"
                Dim tmp As String = ""
                Dim tmpAfter As String = ""
                Dim workPos As Integer
                Dim workLenStr As Integer
                While True
                    If rowCnt = 1 Then
                        colsHead = Split(stBuffer, ",")
                        Exit While ' Assign 1st header, then close
                    Else
                        colsValues = Split(stBuffer, ",")
                    End If
                    pos = stBuffer.IndexOf(key, pos)
                    If pos = -1 Then
                        Exit While
                    Else
                        ReDim Preserve posSet(cnt)
                        posSet(cnt) = pos
                        cnt = cnt + 1
                        pos = pos + 1
                        If cnt Mod 2 = 0 Then
                            '2の倍数になったら(""で囲まれた文字列取得できたら"Then)その文字列の最初の位置と長さを取得
                            workPos = posSet(cnt - 2)
                            workLenStr = (posSet(cnt - 1) - posSet(cnt - 2)) + 1
                            tmp = stBuffer.Substring(workPos, workLenStr)
                            '『,』を外す
                            tmpAfter = Replace(tmp, ",", "")
                            '『"』を外す
                            tmpAfter = Replace(tmpAfter, """", "")
                            'stBufferを置換
                            stBuffer = Replace(stBuffer, tmp, tmpAfter)
                            pos = 0
                        End If
                    End If
                End While
                cols = Split(stBuffer, ",")
                If (rowCnt <> 1) And (rowCnt <> 2) Then
                    'Insert into table 
                End If
                If UCase(cols(0)) = "TOTAL" Then
                    Exit While ' Assign 2nd header, then close
                End If
                textLines.Add(cols)
                'csvファイル種類切り分けのため、ヘッダの項目名取得しておく
            End While
        Catch ex As Exception
            cReader.Close()
        Finally
            cReader.Close()
        End Try
        strArr = textLines.ToArray
        'Verify that if the records / data not formated, show the error information to the user
        If strArr.Count < 2 Then
            Call showMsg("Status: Upload Failed <br><br> The CSV file is not valid format!!!", "")
            Exit Sub
        End If
        'Pass the paramters to Update in Datatabase
        Dim _StockOverviewModel As StockOverviewModel = New StockOverviewModel()
        Dim _StockOverviewControl As StockOverviewControl = New StockOverviewControl()
        _StockOverviewModel.UPDPG = "Analysis_FileUpload.aspx"
        _StockOverviewModel.FileName = fileName
        _StockOverviewModel.SrcFileName = strSrcFileName
        _StockOverviewModel.UserId = userid
        _StockOverviewModel.UserName = userName
        _StockOverviewModel.UploadUser = userName
        _StockOverviewModel.ShipToBranchCode = DropListLocation.SelectedItem.Value
        _StockOverviewModel.ShipToBranch = DropListLocation.SelectedItem.Text
        _StockOverviewModel.strMonth = strMonth
        Dim blStatus As Boolean = _StockOverviewControl.AddModifyStockOverview(strArr, _StockOverviewModel)
        'If Error Raised then show the errors
        If Not blStatus Then
            Call showMsg("Status: Upload Failed <br><br>  Please check the content of file ", "")
            Exit Sub
        Else
            Call showMsg("Status: Upload Success <br><br> The file has been uploaded", "")
        End If

    End Sub

    Private Sub Item9A(filenameFullPath As String, fileName As String, userid As String, userName As String, strSrcFileName As String, strMonth As String)
        'Read the content from stored file
        Dim cReader
        Dim textLines As New List(Of String())
        Dim strArr()() As String
        Try
            'StreamReader の新しいインスタンスを生成する
            cReader = New System.IO.StreamReader(filenameFullPath, System.Text.Encoding.Default)
            Dim rowCnt As Integer
            '読み込んだものを追加で格納
            Dim cols() As String
            Dim colsHead() As String 'ヘッダ１行目
            '   Dim colsHead2() As String 'ヘッダ２行目
            Dim colsValues() As String
            While (cReader.Peek() >= 0)
                'ファイルを1 行ずつ読み込む
                Dim stBuffer As String = cReader.ReadLine()
                rowCnt = rowCnt + 1
                '『"』の位置取得
                Dim pos As Integer = 0
                '全ての『"』の位置情報をセット
                Dim posSet() As Integer
                Dim cnt As Integer = 0
                Dim key As String = """"
                Dim tmp As String = ""
                Dim tmpAfter As String = ""
                Dim workPos As Integer
                Dim workLenStr As Integer
                While True
                    If rowCnt = 1 Then
                        colsHead = Split(stBuffer, ",")
                        Exit While ' Assign 1st header, then close
                    Else
                        colsValues = Split(stBuffer, ",")
                    End If
                    pos = stBuffer.IndexOf(key, pos)
                    If pos = -1 Then
                        Exit While
                    Else
                        ReDim Preserve posSet(cnt)
                        posSet(cnt) = pos
                        cnt = cnt + 1
                        pos = pos + 1
                        If cnt Mod 2 = 0 Then
                            '2の倍数になったら(""で囲まれた文字列取得できたら"Then)その文字列の最初の位置と長さを取得
                            workPos = posSet(cnt - 2)
                            workLenStr = (posSet(cnt - 1) - posSet(cnt - 2)) + 1
                            tmp = stBuffer.Substring(workPos, workLenStr)
                            '『,』を外す
                            tmpAfter = Replace(tmp, ",", "")
                            '『"』を外す
                            tmpAfter = Replace(tmpAfter, """", "")
                            'stBufferを置換
                            stBuffer = Replace(stBuffer, tmp, tmpAfter)
                            pos = 0
                        End If
                    End If
                End While
                cols = Split(stBuffer, ",")
                If (rowCnt <> 1) And (rowCnt <> 2) Then
                    'Insert into table 
                End If
                If UCase(cols(0)) = "TOTAL" Then
                    Exit While ' Assign 2nd header, then close
                End If
                textLines.Add(cols)
                'csvファイル種類切り分けのため、ヘッダの項目名取得しておく
            End While
        Catch ex As Exception
            cReader.Close()
        Finally
            cReader.Close()
        End Try
        strArr = textLines.ToArray
        'Verify that if the records / data not formated, show the error information to the user
        If strArr.Count < 2 Then
            Call showMsg("Status: Upload Failed <br><br> The CSV file is not valid format!!!", "")
            Exit Sub
        End If
        'Pass the paramters to Update in Datatabase
        Dim _StockOverviewModel As StockOverviewModel = New StockOverviewModel()
        Dim _StockOverviewControl As StockOverviewControl = New StockOverviewControl()
        _StockOverviewModel.UPDDT = Now
        _StockOverviewModel.UPDCD = userid
        _StockOverviewModel.UPDPG = "Analysis_FileUpload.aspx"
        _StockOverviewModel.FileName = fileName
        _StockOverviewModel.SrcFileName = strSrcFileName
        _StockOverviewModel.UserId = userid
        _StockOverviewModel.UserName = userName
        _StockOverviewModel.UploadUser = userName
        _StockOverviewModel.ShipToBranchCode = DropListLocation.SelectedItem.Value
        _StockOverviewModel.ShipToBranch = DropListLocation.SelectedItem.Text
        _StockOverviewModel.strMonth = strMonth


        Dim blStatus As Boolean = _StockOverviewControl.AddModifyStockOverviewCount(strArr, _StockOverviewModel)
        'If Error Raised then show the errors
        If Not blStatus Then
            Call showMsg("Status: Upload Failed <br><br>  Please check the content of file ", "")
            Exit Sub
        Else
            Call showMsg("Status: Upload Success <br><br> The file has been uploaded", "")
        End If

    End Sub

    Private Sub Item10(filenameFullPath As String, fileName As String, userid As String, userName As String, strSrcFileName As String)
        'Read the content from stored file
        Dim cReader
        Dim textLines As New List(Of String())
        Dim strArr()() As String
        Try
            'StreamReader の新しいインスタンスを生成する
            cReader = New System.IO.StreamReader(filenameFullPath, System.Text.Encoding.Default)
            Dim rowCnt As Integer
            '読み込んだものを追加で格納
            Dim cols() As String
            Dim colsHead() As String 'ヘッダ１行目
            '   Dim colsHead2() As String 'ヘッダ２行目
            Dim colsValues() As String
            While (cReader.Peek() >= 0)
                'ファイルを1 行ずつ読み込む
                Dim stBuffer As String = cReader.ReadLine()
                rowCnt = rowCnt + 1
                '『"』の位置取得
                Dim pos As Integer = 0
                '全ての『"』の位置情報をセット
                Dim posSet() As Integer
                Dim cnt As Integer = 0
                Dim key As String = """"
                Dim tmp As String = ""
                Dim tmpAfter As String = ""
                Dim workPos As Integer
                Dim workLenStr As Integer
                While True
                    If rowCnt = 1 Then
                        colsHead = Split(stBuffer, ",")
                        Exit While ' Assign 1st header, then close
                    Else
                        colsValues = Split(stBuffer, ",")
                    End If
                    pos = stBuffer.IndexOf(key, pos)
                    If pos = -1 Then
                        Exit While
                    Else
                        ReDim Preserve posSet(cnt)
                        posSet(cnt) = pos
                        cnt = cnt + 1
                        pos = pos + 1
                        If cnt Mod 2 = 0 Then
                            '2の倍数になったら(""で囲まれた文字列取得できたら"Then)その文字列の最初の位置と長さを取得
                            workPos = posSet(cnt - 2)
                            workLenStr = (posSet(cnt - 1) - posSet(cnt - 2)) + 1
                            tmp = stBuffer.Substring(workPos, workLenStr)
                            '『,』を外す
                            tmpAfter = Replace(tmp, ",", "")
                            '『"』を外す
                            tmpAfter = Replace(tmpAfter, """", "")
                            'stBufferを置換
                            stBuffer = Replace(stBuffer, tmp, tmpAfter)
                            pos = 0
                        End If
                    End If
                End While
                cols = Split(stBuffer, ",")
                If (rowCnt <> 1) And (rowCnt <> 2) Then
                    'Insert into table 
                End If
                If UCase(cols(0)) = "TOTAL" Then
                    Exit While ' Assign 2nd header, then close
                End If
                textLines.Add(cols)
                'csvファイル種類切り分けのため、ヘッダの項目名取得しておく
            End While
        Catch ex As Exception
            cReader.Close()
        Finally
            cReader.Close()
        End Try
        strArr = textLines.ToArray
        'Verify that if the records / data not formated, show the error information to the user
        If strArr.Count < 2 Then
            Call showMsg("Status: Upload Failed <br><br> The CSV file is not valid format!!!", "")
            Exit Sub
        End If
        'Pass the paramters to Update in Datatabase
        Dim _SawDiscountModel As SawDiscountModel = New SawDiscountModel()
        Dim _SawDiscountControl As SawDiscountControl = New SawDiscountControl()
        _SawDiscountModel.UPDPG = "Analysis_FileUpload.aspx"
        _SawDiscountModel.FileName = fileName
        _SawDiscountModel.SrcFileName = strSrcFileName
        _SawDiscountModel.UserId = userid
        _SawDiscountModel.UserName = userName
        _SawDiscountModel.UploadUser = userName
        _SawDiscountModel.ShipToBranchCode = DropListLocation.SelectedItem.Value
        _SawDiscountModel.ShipToBranch = DropListLocation.SelectedItem.Text
        Dim blStatus As Boolean = _SawDiscountControl.AddModifySawDiscount(strArr, _SawDiscountModel)
        'If Error Raised then show the errors
        If Not blStatus Then
            Call showMsg("Status: Upload Failed <br><br>  Please check the content of file ", "")
            Exit Sub
        Else
            Call showMsg("Status: Upload Success <br><br> The file has been uploaded", "")
        End If

    End Sub

    Private Sub Item11(filenameFullPath As String, fileName As String, userid As String, userName As String, strSrcFileName As String)
        'Read the content from stored file
        Dim cReader
        Dim textLines As New List(Of String())
        Dim strArr()() As String
        Try
            'StreamReader の新しいインスタンスを生成する
            cReader = New System.IO.StreamReader(filenameFullPath, System.Text.Encoding.Default)
            Dim rowCnt As Integer
            '読み込んだものを追加で格納
            Dim cols() As String
            Dim colsHead() As String 'ヘッダ１行目
            '   Dim colsHead2() As String 'ヘッダ２行目
            Dim colsValues() As String
            While (cReader.Peek() >= 0)
                'ファイルを1 行ずつ読み込む
                Dim stBuffer As String = cReader.ReadLine()
                rowCnt = rowCnt + 1
                '『"』の位置取得
                Dim pos As Integer = 0
                '全ての『"』の位置情報をセット
                Dim posSet() As Integer
                Dim cnt As Integer = 0
                Dim key As String = """"
                Dim tmp As String = ""
                Dim tmpAfter As String = ""
                Dim workPos As Integer
                Dim workLenStr As Integer
                While True
                    If rowCnt = 1 Then
                        colsHead = Split(stBuffer, ",")
                        Exit While ' Assign 1st header, then close
                    Else
                        colsValues = Split(stBuffer, ",")
                    End If
                    pos = stBuffer.IndexOf(key, pos)
                    If pos = -1 Then
                        Exit While
                    Else
                        ReDim Preserve posSet(cnt)
                        posSet(cnt) = pos
                        cnt = cnt + 1
                        pos = pos + 1
                        If cnt Mod 2 = 0 Then
                            '2の倍数になったら(""で囲まれた文字列取得できたら"Then)その文字列の最初の位置と長さを取得
                            workPos = posSet(cnt - 2)
                            workLenStr = (posSet(cnt - 1) - posSet(cnt - 2)) + 1
                            tmp = stBuffer.Substring(workPos, workLenStr)
                            '『,』を外す
                            tmpAfter = Replace(tmp, ",", "")
                            '『"』を外す
                            tmpAfter = Replace(tmpAfter, """", "")
                            'stBufferを置換
                            stBuffer = Replace(stBuffer, tmp, tmpAfter)
                            pos = 0
                        End If
                    End If
                End While
                cols = Split(stBuffer, ",")
                If (rowCnt <> 1) And (rowCnt <> 2) Then
                    'Insert into table 
                End If
                If UCase(cols(0)) = "TOTAL" Then
                    Exit While ' Assign 2nd header, then close
                End If
                textLines.Add(cols)
                'csvファイル種類切り分けのため、ヘッダの項目名取得しておく
            End While
        Catch ex As Exception
            cReader.Close()
        Finally
            cReader.Close()
        End Try
        strArr = textLines.ToArray



        'Verify that if the records / data not formated, show the error information to the user
        If strArr.Count < 2 Then
            Call showMsg("Status: Upload Failed <br><br> The CSV file is not valid format!!!", "")
            Exit Sub
        End If
        'Pass the paramters to Update in Datatabase
        Dim _PartsIoModel As PartsIoModel = New PartsIoModel()
        Dim _PartsIoControl As PartsIoControl = New PartsIoControl()
        _PartsIoModel.UPDPG = "Analysis_FileUpload.aspx"
        _PartsIoModel.FileName = fileName
        _PartsIoModel.SrcFileName = strSrcFileName
        _PartsIoModel.UserId = userid
        _PartsIoModel.UserName = userName
        _PartsIoModel.ShipToBranchCode = DropListLocation.SelectedItem.Value
        _PartsIoModel.ShipToBranch = DropListLocation.SelectedItem.Text
        Dim blStatus As Boolean = _PartsIoControl.AddModifyPartsIo(strArr, _PartsIoModel)
        'If Error Raised then show the errors
        If Not blStatus Then
            Call showMsg("Status: Upload Failed <br><br>  Please check the content of file ", "")
            Exit Sub
        Else
            Call showMsg("Status: Upload Success <br><br> The file has been uploaded", "")
        End If

    End Sub





    Private Sub Item14(filenameFullPath As String, fileName As String, userid As String, userName As String, strSrcFileName As String)
        'Read the content from stored file
        Dim cReader
        Dim textLines As New List(Of String())
        Dim strArr()() As String
        Try
            'StreamReader の新しいインスタンスを生成する
            cReader = New System.IO.StreamReader(filenameFullPath, System.Text.Encoding.Default)
            Dim rowCnt As Integer
            '読み込んだものを追加で格納
            Dim cols() As String
            Dim colsHead() As String 'ヘッダ１行目
            '   Dim colsHead2() As String 'ヘッダ２行目
            Dim colsValues() As String
            While (cReader.Peek() >= 0)
                'ファイルを1 行ずつ読み込む
                Dim stBuffer As String = cReader.ReadLine()
                rowCnt = rowCnt + 1
                '『"』の位置取得
                Dim pos As Integer = 0
                '全ての『"』の位置情報をセット
                Dim posSet() As Integer
                Dim cnt As Integer = 0
                Dim key As String = """"
                Dim tmp As String = ""
                Dim tmpAfter As String = ""
                Dim workPos As Integer
                Dim workLenStr As Integer
                While True
                    If rowCnt = 1 Then
                        colsHead = Split(stBuffer, ",")
                        Exit While ' Assign 1st header, then close
                    Else
                        colsValues = Split(stBuffer, ",")
                    End If
                    pos = stBuffer.IndexOf(key, pos)
                    If pos = -1 Then
                        Exit While
                    Else
                        ReDim Preserve posSet(cnt)
                        posSet(cnt) = pos
                        cnt = cnt + 1
                        pos = pos + 1
                        If cnt Mod 2 = 0 Then
                            '2の倍数になったら(""で囲まれた文字列取得できたら"Then)その文字列の最初の位置と長さを取得
                            workPos = posSet(cnt - 2)
                            workLenStr = (posSet(cnt - 1) - posSet(cnt - 2)) + 1
                            tmp = stBuffer.Substring(workPos, workLenStr)
                            '『,』を外す
                            tmpAfter = Replace(tmp, ",", "")
                            '『"』を外す
                            tmpAfter = Replace(tmpAfter, """", "")
                            'stBufferを置換
                            stBuffer = Replace(stBuffer, tmp, tmpAfter)
                            pos = 0
                        End If
                    End If
                End While
                cols = Split(stBuffer, ",")
                If (rowCnt <> 1) And (rowCnt <> 2) Then
                    'Insert into table 
                End If
                If UCase(cols(0)) = "TOTAL" Then
                    Exit While ' Assign 2nd header, then close
                End If
                textLines.Add(cols)
                'csvファイル種類切り分けのため、ヘッダの項目名取得しておく
            End While
        Catch ex As Exception
            cReader.Close()
        Finally
            cReader.Close()
        End Try
        strArr = textLines.ToArray
        'Verify that if the records / data not formated, show the error information to the user
        If strArr.Count < 2 Then
            Call showMsg("Status: Upload Failed <br><br> The CSV file is not valid format!!!", "")
            Exit Sub
        End If
        'Pass the paramters to Update in Datatabase
        Dim _G2sPaidModel As G2sPaidModel = New G2sPaidModel()
        Dim _G2sPaidControl As G2sPaidControl = New G2sPaidControl()
        _G2sPaidModel.UPDPG = "Analysis_FileUpload.aspx"
        _G2sPaidModel.FileName = fileName
        _G2sPaidModel.SrcFileName = strSrcFileName
        _G2sPaidModel.UserId = userid
        _G2sPaidModel.UserName = userName
        _G2sPaidModel.UploadUser = userName
        _G2sPaidModel.ShipToBranchCode = DropListLocation.SelectedItem.Value
        _G2sPaidModel.ShipToBranch = DropListLocation.SelectedItem.Text
        Dim blStatus As Boolean = _G2sPaidControl.AddModifyG2sPaid(strArr, _G2sPaidModel)
        'If Error Raised then show the errors
        If Not blStatus Then
            Call showMsg("Status: Upload Failed <br><br>  Please check the content of file ", "")
            Exit Sub
        Else
            Call showMsg("Status: Upload Success <br><br> The file has been uploaded", "")
        End If

    End Sub

    Private Sub Item15(filenameFullPath As String, fileName As String, userid As String, userName As String, strSrcFileName As String)
        'Read the content from stored file
        Dim cReader
        Dim textLines As New List(Of String())
        Dim strArr()() As String
        Try
            'StreamReader の新しいインスタンスを生成する
            cReader = New System.IO.StreamReader(filenameFullPath, System.Text.Encoding.Default)
            Dim rowCnt As Integer
            '読み込んだものを追加で格納
            Dim cols() As String
            Dim colsHead() As String 'ヘッダ１行目
            '   Dim colsHead2() As String 'ヘッダ２行目
            Dim colsValues() As String
            While (cReader.Peek() >= 0)
                'ファイルを1 行ずつ読み込む
                Dim stBuffer As String = cReader.ReadLine()
                rowCnt = rowCnt + 1
                '『"』の位置取得
                Dim pos As Integer = 0
                '全ての『"』の位置情報をセット
                Dim posSet() As Integer
                Dim cnt As Integer = 0
                Dim key As String = """"
                Dim tmp As String = ""
                Dim tmpAfter As String = ""
                Dim workPos As Integer
                Dim workLenStr As Integer
                While True
                    If rowCnt = 1 Then
                        colsHead = Split(stBuffer, ",")
                        Exit While ' Assign 1st header, then close
                    Else
                        colsValues = Split(stBuffer, ",")
                    End If
                    pos = stBuffer.IndexOf(key, pos)
                    If pos = -1 Then
                        Exit While
                    Else
                        ReDim Preserve posSet(cnt)
                        posSet(cnt) = pos
                        cnt = cnt + 1
                        pos = pos + 1
                        If cnt Mod 2 = 0 Then
                            '2の倍数になったら(""で囲まれた文字列取得できたら"Then)その文字列の最初の位置と長さを取得
                            workPos = posSet(cnt - 2)
                            workLenStr = (posSet(cnt - 1) - posSet(cnt - 2)) + 1
                            tmp = stBuffer.Substring(workPos, workLenStr)
                            '『,』を外す
                            tmpAfter = Replace(tmp, ",", "")
                            '『"』を外す
                            tmpAfter = Replace(tmpAfter, """", "")
                            'stBufferを置換
                            stBuffer = Replace(stBuffer, tmp, tmpAfter)
                            pos = 0
                        End If
                    End If
                End While
                cols = Split(stBuffer, ",")
                If (rowCnt <> 1) And (rowCnt <> 2) Then
                    'Insert into table 
                End If
                If UCase(cols(0)) = "TOTAL" Then
                    Exit While ' Assign 2nd header, then close
                End If
                textLines.Add(cols)
                'csvファイル種類切り分けのため、ヘッダの項目名取得しておく
            End While
        Catch ex As Exception
            cReader.Close()
        Finally
            cReader.Close()
        End Try
        strArr = textLines.ToArray
        'Verify that if the records / data not formated, show the error information to the user
        If strArr.Count < 2 Then
            Call showMsg("Status: Upload Failed <br><br> The CSV file is not valid format!!!", "")
            Exit Sub
        End If
        'Pass the paramters to Update in Datatabase
        Dim _ReturnCreditModel As ReturnCreditModel = New ReturnCreditModel()
        Dim _ReturnCreditControl As ReturnCreditControl = New ReturnCreditControl()
        _ReturnCreditModel.UPDPG = "Analysis_FileUpload.aspx"
        _ReturnCreditModel.FileName = fileName
        _ReturnCreditModel.SrcFileName = strSrcFileName
        _ReturnCreditModel.UserId = userid
        _ReturnCreditModel.UserName = userName
        _ReturnCreditModel.UploadUser = userName
        _ReturnCreditModel.ShipToBranchCode = DropListLocation.SelectedItem.Value
        _ReturnCreditModel.ShipToBranch = DropListLocation.SelectedItem.Text
        Dim blStatus As Boolean = _ReturnCreditControl.AddModifyReturnCredit(strArr, _ReturnCreditModel)
        'If Error Raised then show the errors
        If Not blStatus Then
            Call showMsg("Status: Upload Failed <br><br>  Please check the content of file ", "")
            Exit Sub
        Else
            Call showMsg("Status: Upload Success <br><br> The file has been uploaded", "")
        End If

    End Sub

    Private Sub Item16(filenameFullPath As String, fileName As String, userid As String, userName As String, strSrcFileName As String)
        'Read the content from stored file
        Dim cReader
        Dim textLines As New List(Of String())
        Dim strArr()() As String
        Try
            'StreamReader の新しいインスタンスを生成する
            cReader = New System.IO.StreamReader(filenameFullPath, System.Text.Encoding.Default)
            Dim rowCnt As Integer
            '読み込んだものを追加で格納
            Dim cols() As String
            Dim colsHead() As String 'ヘッダ１行目
            '   Dim colsHead2() As String 'ヘッダ２行目
            Dim colsValues() As String
            While (cReader.Peek() >= 0)
                'ファイルを1 行ずつ読み込む
                Dim stBuffer As String = cReader.ReadLine()
                rowCnt = rowCnt + 1
                '『"』の位置取得
                Dim pos As Integer = 0
                '全ての『"』の位置情報をセット
                Dim posSet() As Integer
                Dim cnt As Integer = 0
                Dim key As String = """"
                Dim tmp As String = ""
                Dim tmpAfter As String = ""
                Dim workPos As Integer
                Dim workLenStr As Integer
                While True
                    If rowCnt = 1 Then
                        colsHead = Split(stBuffer, ",")
                        Exit While ' Assign 1st header, then close
                    Else
                        colsValues = Split(stBuffer, ",")
                    End If
                    pos = stBuffer.IndexOf(key, pos)
                    If pos = -1 Then
                        Exit While
                    Else
                        ReDim Preserve posSet(cnt)
                        posSet(cnt) = pos
                        cnt = cnt + 1
                        pos = pos + 1
                        If cnt Mod 2 = 0 Then
                            '2の倍数になったら(""で囲まれた文字列取得できたら"Then)その文字列の最初の位置と長さを取得
                            workPos = posSet(cnt - 2)
                            workLenStr = (posSet(cnt - 1) - posSet(cnt - 2)) + 1
                            tmp = stBuffer.Substring(workPos, workLenStr)
                            '『,』を外す
                            tmpAfter = Replace(tmp, ",", "")
                            '『"』を外す
                            tmpAfter = Replace(tmpAfter, """", "")
                            'stBufferを置換
                            stBuffer = Replace(stBuffer, tmp, tmpAfter)
                            pos = 0
                        End If
                    End If
                End While
                cols = Split(stBuffer, ",")
                If (rowCnt <> 1) And (rowCnt <> 2) Then
                    'Insert into table 
                End If
                If UCase(cols(0)) = "TOTAL" Then
                    Exit While ' Assign 2nd header, then close
                End If
                textLines.Add(cols)
                'csvファイル種類切り分けのため、ヘッダの項目名取得しておく
            End While
        Catch ex As Exception
            cReader.Close()
        Finally
            cReader.Close()
        End Try
        strArr = textLines.ToArray
        'Verify that if the records / data not formated, show the error information to the user
        If strArr.Count < 2 Then
            Call showMsg("Status: Upload Failed <br><br> The CSV file is not valid format!!!", "")
            Exit Sub
        End If
        'Pass the paramters to Update in Datatabase
        Dim _SLedgerModel As SLedgerModel = New SLedgerModel()
        Dim _SLedgerControl As SLedgerControl = New SLedgerControl()
        _SLedgerModel.UPDPG = "Analysis_FileUpload.aspx"
        _SLedgerModel.FileName = fileName
        _SLedgerModel.SrcFileName = strSrcFileName
        _SLedgerModel.UserId = userid
        _SLedgerModel.UserName = userName
        _SLedgerModel.UploadUser = userName
        _SLedgerModel.ShipToBranchCode = DropListLocation.SelectedItem.Value
        _SLedgerModel.ShipToBranch = DropListLocation.SelectedItem.Text
        Dim blStatus As Boolean = _SLedgerControl.AddModifySLedger(strArr, _SLedgerModel)
        'If Error Raised then show the errors
        If Not blStatus Then
            Call showMsg("Status: Upload Failed <br><br>  Please check the content of file ", "")
            Exit Sub
        Else
            Call showMsg("Status: Upload Success <br><br> The file has been uploaded", "")
        End If

    End Sub

    'VJ 2019/10/10 Start
    Private Sub ServicepartReturn(filenameFullPath As String, fileName As String, userid As String, userName As String, strSrcFileName As String)
        'Read the content from stored file
        Dim cReader
        Dim textLines As New List(Of String())
        Dim strArr()() As String
        Try
            'StreamReader の新しいインスタンスを生成する
            cReader = New System.IO.StreamReader(filenameFullPath, System.Text.Encoding.Default)
            Dim rowCnt As Integer
            '読み込んだものを追加で格納
            Dim cols() As String
            Dim colsHead() As String 'ヘッダ１行目
            '   Dim colsHead2() As String 'ヘッダ２行目
            Dim colsValues() As String
            While (cReader.Peek() >= 0)
                'ファイルを1 行ずつ読み込む
                Dim stBuffer As String = cReader.ReadLine()
                rowCnt = rowCnt + 1
                '『"』の位置取得
                Dim pos As Integer = 0
                '全ての『"』の位置情報をセット
                Dim posSet() As Integer
                Dim cnt As Integer = 0
                Dim key As String = """"
                Dim tmp As String = ""
                Dim tmpAfter As String = ""
                Dim workPos As Integer
                Dim workLenStr As Integer
                While True
                    If rowCnt = 1 Then
                        colsHead = Split(stBuffer, ",")
                        Exit While ' Assign 1st header, then close
                    Else
                        colsValues = Split(stBuffer, ",")
                    End If
                    pos = stBuffer.IndexOf(key, pos)
                    If pos = -1 Then
                        Exit While
                    Else
                        ReDim Preserve posSet(cnt)
                        posSet(cnt) = pos
                        cnt = cnt + 1
                        pos = pos + 1
                        If cnt Mod 2 = 0 Then
                            '2の倍数になったら(""で囲まれた文字列取得できたら"Then)その文字列の最初の位置と長さを取得
                            workPos = posSet(cnt - 2)
                            workLenStr = (posSet(cnt - 1) - posSet(cnt - 2)) + 1
                            tmp = stBuffer.Substring(workPos, workLenStr)
                            '『,』を外す
                            tmpAfter = Replace(tmp, ",", "")
                            '『"』を外す
                            tmpAfter = Replace(tmpAfter, """", "")
                            'stBufferを置換
                            stBuffer = Replace(stBuffer, tmp, tmpAfter)
                            pos = 0
                        End If
                    End If
                End While
                cols = Split(stBuffer, ",")
                If (rowCnt <> 1) And (rowCnt <> 2) Then
                    'Insert into table 
                End If
                'If UCase(cols(0)) = "TOTAL" Then
                '    Exit While ' Assign 2nd header, then close
                'End If
                textLines.Add(cols)
                'csvファイル種類切り分けのため、ヘッダの項目名取得しておく
            End While
        Catch ex As Exception
            cReader.Close()
        Finally
            cReader.Close()
        End Try
        strArr = textLines.ToArray
        'Verify that if the records / data not formated, show the error information to the user
        If strArr.Count < 2 Then
            Call showMsg("Status: Upload Failed <br><br> The CSV file is not valid format!!!", "")
            Exit Sub
        End If
        Dim blStatus As Boolean = False
        'Pass the paramters to Update in Datatabase
        Dim _ServicePartReturnModel As ServicePartsReturnModel = New ServicePartsReturnModel()
        Dim _ServicepartReturnControl As ServicePartsReturnControl = New ServicePartsReturnControl()
        _ServicePartReturnModel.UPDPG = "Analysis_FileUpload.aspx"
        _ServicePartReturnModel.FileName = fileName
        _ServicePartReturnModel.SrcFileName = strSrcFileName
        _ServicePartReturnModel.UserId = userid
        _ServicePartReturnModel.UserName = userName
        _ServicePartReturnModel.UploadUser = userName
        _ServicePartReturnModel.ShipToBranchCode = DropListLocation.SelectedItem.Value
        _ServicePartReturnModel.ShipToBranch = DropListLocation.SelectedItem.Text
        blStatus = _ServicepartReturnControl.AddModifyServicePartsReturn(strArr, _ServicePartReturnModel)
        'If Error Raised then show the errors
        If Not blStatus Then
            Call showMsg("Status: Upload Failed <br><br>  Please check the content of file ", "")
            Exit Sub
        Else
            Call showMsg("Status: Upload Success <br><br> The file has been uploaded", "")
        End If

    End Sub
    Private Sub DebitNoteRegister(filenameFullPath As String, fileName As String, userid As String, userName As String, strSrcFileName As String)
        'Read the content from stored file
        Dim cReader
        Dim textLines As New List(Of String())
        Dim strArr()() As String
        Try
            'StreamReader の新しいインスタンスを生成する
            cReader = New System.IO.StreamReader(filenameFullPath, System.Text.Encoding.Default)
            Dim rowCnt As Integer
            '読み込んだものを追加で格納
            Dim cols() As String
            Dim colsHead() As String 'ヘッダ１行目
            '   Dim colsHead2() As String 'ヘッダ２行目
            Dim colsValues() As String
            While (cReader.Peek() >= 0)
                'ファイルを1 行ずつ読み込む
                Dim stBuffer As String = cReader.ReadLine()
                rowCnt = rowCnt + 1
                '『"』の位置取得
                Dim pos As Integer = 0
                '全ての『"』の位置情報をセット
                Dim posSet() As Integer
                Dim cnt As Integer = 0
                Dim key As String = """"
                Dim tmp As String = ""
                Dim tmpAfter As String = ""
                Dim workPos As Integer
                Dim workLenStr As Integer
                While True
                    If rowCnt = 2 Then
                        colsHead = Split(stBuffer, ",")
                        Exit While ' Assign 1st header, then close
                    Else
                        colsValues = Split(stBuffer, ",")
                    End If
                    pos = stBuffer.IndexOf(key, pos)
                    If pos = -1 Then
                        Exit While
                    Else
                        ReDim Preserve posSet(cnt)
                        posSet(cnt) = pos
                        cnt = cnt + 1
                        pos = pos + 1
                        If cnt Mod 2 = 0 Then
                            '2の倍数になったら(""で囲まれた文字列取得できたら"Then)その文字列の最初の位置と長さを取得
                            workPos = posSet(cnt - 2)
                            workLenStr = (posSet(cnt - 1) - posSet(cnt - 2)) + 1
                            tmp = stBuffer.Substring(workPos, workLenStr)
                            '『,』を外す
                            tmpAfter = Replace(tmp, ",", "")
                            '『"』を外す
                            tmpAfter = Replace(tmpAfter, """", "")
                            'stBufferを置換
                            stBuffer = Replace(stBuffer, tmp, tmpAfter)
                            pos = 0
                        End If
                    End If
                End While
                cols = Split(stBuffer, ",")
                If (rowCnt <> 1) And (rowCnt <> 2) Then
                    'Insert into table 
                End If
                'If UCase(cols(0)) = "TOTAL" Then
                '    Exit While ' Assign 2nd header, then close
                'End If
                textLines.Add(cols)
                'csvファイル種類切り分けのため、ヘッダの項目名取得しておく
            End While
        Catch ex As Exception
            cReader.Close()
        Finally
            cReader.Close()
        End Try
        strArr = textLines.ToArray
        'Verify that if the records / data not formated, show the error information to the user
        If strArr.Count < 2 Then
            Call showMsg("Status: Upload Failed <br><br> The CSV file is not valid format!!!", "")
            Exit Sub
        End If
        Dim blStatus As Boolean = False
        'Pass the paramters to Update in Datatabase
        Dim _DebitNoteRegisterModel As DebitNoteRegisterModel = New DebitNoteRegisterModel()
        Dim _DebitNoteRegisterControl As DebitNoteRegisterControl = New DebitNoteRegisterControl()
        _DebitNoteRegisterModel.UPDPG = "Analysis_FileUpload.aspx"
        _DebitNoteRegisterModel.FileName = fileName
        _DebitNoteRegisterModel.SrcFileName = strSrcFileName
        _DebitNoteRegisterModel.UserId = userid
        _DebitNoteRegisterModel.UserName = userName
        _DebitNoteRegisterModel.UploadUser = userName
        _DebitNoteRegisterModel.ShipToBranchCode = DropListLocation.SelectedItem.Value
        _DebitNoteRegisterModel.ShipToBranch = DropListLocation.SelectedItem.Text
        blStatus = _DebitNoteRegisterControl.AddModifyDebitNoteRegister(strArr, _DebitNoteRegisterModel)
        'If Error Raised then show the errors
        If Not blStatus Then
            Call showMsg("Status: Upload Failed <br><br>  Please check the content of file ", "")
            Exit Sub
        Else
            Call showMsg("Status: Upload Success <br><br> The file has been uploaded", "")
        End If

    End Sub
    'VJ 2019/10/10 End
    'VJ 2019/10/15 Begin
    Private Sub HSNCode(filenameFullPath As String, fileName As String, userid As String, userName As String, strSrcFileName As String)
        'Read the HSNCode from stored file
        Dim cReader
        Dim textLines As New List(Of String())
        Dim strArr()() As String
        Try
            'StreamReader の新しいインスタンスを生成する
            cReader = New System.IO.StreamReader(filenameFullPath, System.Text.Encoding.Default)
            Dim rowCnt As Integer
            '読み込んだものを追加で格納
            Dim cols() As String
            Dim colsHead() As String 'ヘッダ１行目
            '   Dim colsHead2() As String 'ヘッダ２行目
            Dim colsValues() As String
            While (cReader.Peek() >= 0)
                'ファイルを1 行ずつ読み込む
                Dim stBuffer As String = cReader.ReadLine()
                rowCnt = rowCnt + 1
                '『"』の位置取得
                Dim pos As Integer = 0
                '全ての『"』の位置情報をセット
                Dim posSet() As Integer
                Dim cnt As Integer = 0
                Dim key As String = """"
                Dim tmp As String = ""
                Dim tmpAfter As String = ""
                Dim workPos As Integer
                Dim workLenStr As Integer
                While True
                    If rowCnt = 1 Then
                        colsHead = Split(stBuffer, ",")
                        Exit While ' Assign 1st header, then close
                    Else
                        colsValues = Split(stBuffer, ",")
                    End If
                    pos = stBuffer.IndexOf(key, pos)
                    If pos = -1 Then
                        Exit While
                    Else
                        ReDim Preserve posSet(cnt)
                        posSet(cnt) = pos
                        cnt = cnt + 1
                        pos = pos + 1
                        If cnt Mod 2 = 0 Then
                            '2の倍数になったら(""で囲まれた文字列取得できたら"Then)その文字列の最初の位置と長さを取得
                            workPos = posSet(cnt - 2)
                            workLenStr = (posSet(cnt - 1) - posSet(cnt - 2)) + 1
                            tmp = stBuffer.Substring(workPos, workLenStr)
                            '『,』を外す
                            tmpAfter = Replace(tmp, ",", "")
                            '『"』を外す
                            tmpAfter = Replace(tmpAfter, """", "")
                            'stBufferを置換
                            stBuffer = Replace(stBuffer, tmp, tmpAfter)
                            pos = 0
                        End If
                    End If
                End While
                cols = Split(stBuffer, ",")
                If (rowCnt <> 1) And (rowCnt <> 2) Then
                    'Insert into table 
                End If
                'If UCase(cols(0)) = "TOTAL" Then
                '    Exit While ' Assign 2nd header, then close
                'End If
                textLines.Add(cols)
                'csvファイル種類切り分けのため、ヘッダの項目名取得しておく
            End While
        Catch ex As Exception
            cReader.Close()
        Finally
            cReader.Close()
        End Try
        strArr = textLines.ToArray
        'Verify that if the records / data not formated, show the error information to the user
        If strArr.Count < 2 Then
            Call showMsg("Status: Upload Failed <br><br> The CSV file is not valid format!!!", "")
            Exit Sub
        End If
        Dim blStatus As Boolean = False
        'Pass the paramters to Update in Datatabase
        Dim _HSNCodeModel As HSNCodeModel = New HSNCodeModel()
        Dim _HSNCodeControl As HSNCodeControl = New HSNCodeControl()
        _HSNCodeModel.UPDPG = "Analysis_FileUpload.aspx"
        _HSNCodeModel.FileName = fileName
        _HSNCodeModel.SrcFileName = strSrcFileName
        _HSNCodeModel.UserId = userid
        _HSNCodeModel.UserName = userName
        _HSNCodeModel.UploadUser = userName
        _HSNCodeModel.ShipToBranchCode = DropListLocation.SelectedItem.Value
        _HSNCodeModel.ShipToBranch = DropListLocation.SelectedItem.Text
        blStatus = _HSNCodeControl.AddModifyHSNCode(strArr, _HSNCodeModel)
        'If Error Raised then show the errors
        'blStatus = _HSNCodeControl.UpdateHSNCodePRDetails(_HSNCodeModel)
        If Not blStatus Then
            Call showMsg("Status: Upload Failed <br><br>  Please check the content of file ", "")
            Exit Sub
        Else
            Call showMsg("Status: Upload Success <br><br> The file has been uploaded", "")
        End If

    End Sub 'VJ 2019/10/15 End
    Private Sub HSNCodeWithPRDetail(filenameFullPath As String, fileName As String, userid As String, userName As String, strSrcFileName As String)
        'Read the content from stored file
        Dim cReader
        Dim textLines As New List(Of String())
        Dim strArr()() As String
        Try
            'StreamReader の新しいインスタンスを生成する
            cReader = New System.IO.StreamReader(filenameFullPath, System.Text.Encoding.Default)
            Dim rowCnt As Integer
            '読み込んだものを追加で格納
            Dim cols() As String
            Dim colsHead() As String 'ヘッダ１行目
            '   Dim colsHead2() As String 'ヘッダ２行目
            Dim colsValues() As String
            While (cReader.Peek() >= 0)
                'ファイルを1 行ずつ読み込む
                Dim stBuffer As String = cReader.ReadLine()
                rowCnt = rowCnt + 1
                '『"』の位置取得
                Dim pos As Integer = 0
                '全ての『"』の位置情報をセット
                Dim posSet() As Integer
                Dim cnt As Integer = 0
                Dim key As String = """"
                Dim tmp As String = ""
                Dim tmpAfter As String = ""
                Dim workPos As Integer
                Dim workLenStr As Integer
                While True
                    If rowCnt = 1 Then
                        colsHead = Split(stBuffer, ",")
                        Exit While ' Assign 1st header, then close
                    Else
                        colsValues = Split(stBuffer, ",")
                    End If
                    pos = stBuffer.IndexOf(key, pos)
                    If pos = -1 Then
                        Exit While
                    Else
                        ReDim Preserve posSet(cnt)
                        posSet(cnt) = pos
                        cnt = cnt + 1
                        pos = pos + 1
                        If cnt Mod 2 = 0 Then
                            '2の倍数になったら(""で囲まれた文字列取得できたら"Then)その文字列の最初の位置と長さを取得
                            workPos = posSet(cnt - 2)
                            workLenStr = (posSet(cnt - 1) - posSet(cnt - 2)) + 1
                            tmp = stBuffer.Substring(workPos, workLenStr)
                            '『,』を外す
                            tmpAfter = Replace(tmp, ",", "")
                            '『"』を外す
                            tmpAfter = Replace(tmpAfter, """", "")
                            'stBufferを置換
                            stBuffer = Replace(stBuffer, tmp, tmpAfter)
                            pos = 0
                        End If
                    End If
                End While
                cols = Split(stBuffer, ",")
                If (rowCnt <> 1) And (rowCnt <> 2) Then
                    'Insert into table 
                End If
                'If UCase(cols(0)) = "TOTAL" Then
                '    Exit While ' Assign 2nd header, then close
                'End If
                textLines.Add(cols)
                'csvファイル種類切り分けのため、ヘッダの項目名取得しておく
            End While
        Catch ex As Exception
            cReader.Close()
        Finally
            cReader.Close()
        End Try
        strArr = textLines.ToArray
        'Verify that if the records / data not formated, show the error information to the user
        If strArr.Count < 2 Then
            Call showMsg("Status: Upload Failed <br><br> The CSV file is not valid format!!!", "")
            Exit Sub
        End If
        Dim blStatus As Boolean = False
        'Pass the paramters to Update in Datatabase
        Dim _PrDetailModel As PrDetailModel = New PrDetailModel()
        Dim _PrDetailControl As PrDetailControl = New PrDetailControl()
        _PrDetailModel.UPDPG = "Analysis_FileUpload.aspx"
        _PrDetailModel.FileName = fileName
        _PrDetailModel.SrcFileName = strSrcFileName
        _PrDetailModel.UserId = userid
        _PrDetailModel.UserName = userName
        _PrDetailModel.UploadUser = userName
        _PrDetailModel.ShipToBranchCode = DropListLocation.SelectedItem.Value
        _PrDetailModel.ShipToBranch = DropListLocation.SelectedItem.Text
        blStatus = _PrDetailControl.UpdateHSNCodeandPRDetail(strArr, _PrDetailModel)
        'blStatus = _PrDetailControl.UpdatePrHSNDetail(strArr, _PrDetailModel)
        'If Error Raised then show the errors
        'blStatus = _HSNCodeControl.UpdateHSNCodePRDetails(_HSNCodeModel)
        If Not blStatus Then
            Call showMsg("Status: Upload Failed <br><br>  Please check the content of file ", "")
            Exit Sub
        Else
            Call showMsg("Status: Upload Success <br><br> The file has been uploaded", "")
        End If

    End Sub 'VJ 2019/10/17 End

    Private Sub Item20(filenameFullPath As String, fileName As String, userid As String, userName As String, strSrcFileName As String)
        'Read the content from stored file
        Dim cReader
        Dim textLines As New List(Of String())
        Dim strArr()() As String
        Try
            'StreamReader の新しいインスタンスを生成する
            cReader = New System.IO.StreamReader(filenameFullPath, System.Text.Encoding.Default)
            Dim rowCnt As Integer
            '読み込んだものを追加で格納
            Dim cols() As String
            Dim colsHead() As String 'ヘッダ１行目
            '   Dim colsHead2() As String 'ヘッダ２行目
            Dim colsValues() As String
            While (cReader.Peek() >= 0)
                'ファイルを1 行ずつ読み込む
                Dim stBuffer As String = cReader.ReadLine()
                rowCnt = rowCnt + 1
                '『"』の位置取得
                Dim pos As Integer = 0
                '全ての『"』の位置情報をセット
                Dim posSet() As Integer
                Dim cnt As Integer = 0
                Dim key As String = """"
                Dim tmp As String = ""
                Dim tmpAfter As String = ""
                Dim workPos As Integer
                Dim workLenStr As Integer
                While True
                    If rowCnt = 1 Then
                        colsHead = Split(stBuffer, ",")
                        Exit While ' Assign 1st header, then close
                    Else
                        colsValues = Split(stBuffer, ",")
                    End If
                    pos = stBuffer.IndexOf(key, pos)
                    If pos = -1 Then
                        Exit While
                    Else
                        ReDim Preserve posSet(cnt)
                        posSet(cnt) = pos
                        cnt = cnt + 1
                        pos = pos + 1
                        If cnt Mod 2 = 0 Then
                            '2の倍数になったら(""で囲まれた文字列取得できたら"Then)その文字列の最初の位置と長さを取得
                            workPos = posSet(cnt - 2)
                            workLenStr = (posSet(cnt - 1) - posSet(cnt - 2)) + 1
                            tmp = stBuffer.Substring(workPos, workLenStr)
                            '『,』を外す
                            tmpAfter = Replace(tmp, ",", "")
                            '『"』を外す
                            tmpAfter = Replace(tmpAfter, """", "")
                            'stBufferを置換
                            stBuffer = Replace(stBuffer, tmp, tmpAfter)
                            pos = 0
                        End If
                    End If
                End While
                cols = Split(stBuffer, ",")
                If (rowCnt <> 1) And (rowCnt <> 2) Then
                    'Insert into table 
                End If
                If UCase(cols(0)) = "TOTAL" Then
                    Exit While ' Assign 2nd header, then close
                End If
                textLines.Add(cols)
                'csvファイル種類切り分けのため、ヘッダの項目名取得しておく
            End While
        Catch ex As Exception
            cReader.Close()
        Finally
            cReader.Close()
        End Try
        strArr = textLines.ToArray
        'Verify that if the records / data not formated, show the error information to the user
        If strArr.Count < 2 Then
            Call showMsg("Status: Upload Failed <br><br> The CSV file is not valid format!!!", "")
            Exit Sub
        End If
        'Pass the paramters to Update in Datatabase
        Dim _OtherSalesExtendedWarrantyModel As OtherSalesExtendedWarrantyModel = New OtherSalesExtendedWarrantyModel()
        Dim _OtherSalesExtendedWarrantyControl As OtherSalesExtendedWarrantyControl = New OtherSalesExtendedWarrantyControl()
        _OtherSalesExtendedWarrantyModel.UPDPG = "Analysis_FileUpload.aspx"
        _OtherSalesExtendedWarrantyModel.FileName = fileName
        _OtherSalesExtendedWarrantyModel.SrcFileName = strSrcFileName
        _OtherSalesExtendedWarrantyModel.UserId = userid
        _OtherSalesExtendedWarrantyModel.UserName = userName
        _OtherSalesExtendedWarrantyModel.UploadUser = userName
        _OtherSalesExtendedWarrantyModel.ShipToBranchCode = DropListLocation.SelectedItem.Value
        _OtherSalesExtendedWarrantyModel.ShipToBranch = DropListLocation.SelectedItem.Text
        Dim blStatus As Boolean = _OtherSalesExtendedWarrantyControl.AddModifyOtherSalesExtendedWarranty(strArr, _OtherSalesExtendedWarrantyModel)
        'If Error Raised then show the errors
        If Not blStatus Then
            Call showMsg("Status: Upload Failed <br><br>  Please check the content of file ", "")
            Exit Sub
        Else
            Call showMsg("Status: Upload Success <br><br> The file has been uploaded", "")
        End If

    End Sub
    Private Sub Item21(filenameFullPath As String, fileName As String, userid As String, userName As String, strSrcFileName As String)
        'Read the content from stored file
        Dim cReader
        Dim textLines As New List(Of String())
        Dim strArr()() As String
        Try
            'StreamReader の新しいインスタンスを生成する
            cReader = New System.IO.StreamReader(filenameFullPath, System.Text.Encoding.Default)
            Dim rowCnt As Integer
            '読み込んだものを追加で格納
            Dim cols() As String
            Dim colsHead() As String 'ヘッダ１行目
            '   Dim colsHead2() As String 'ヘッダ２行目
            Dim colsValues() As String
            While (cReader.Peek() >= 0)
                'ファイルを1 行ずつ読み込む
                Dim stBuffer As String = cReader.ReadLine()
                rowCnt = rowCnt + 1
                '『"』の位置取得
                Dim pos As Integer = 0
                '全ての『"』の位置情報をセット
                Dim posSet() As Integer
                Dim cnt As Integer = 0
                Dim key As String = """"
                Dim tmp As String = ""
                Dim tmpAfter As String = ""
                Dim workPos As Integer
                Dim workLenStr As Integer
                While True
                    If rowCnt = 1 Then
                        colsHead = Split(stBuffer, ",")
                        Exit While ' Assign 1st header, then close
                    Else
                        colsValues = Split(stBuffer, ",")
                    End If
                    pos = stBuffer.IndexOf(key, pos)
                    If pos = -1 Then
                        Exit While
                    Else
                        ReDim Preserve posSet(cnt)
                        posSet(cnt) = pos
                        cnt = cnt + 1
                        pos = pos + 1
                        If cnt Mod 2 = 0 Then
                            '2の倍数になったら(""で囲まれた文字列取得できたら"Then)その文字列の最初の位置と長さを取得
                            workPos = posSet(cnt - 2)
                            workLenStr = (posSet(cnt - 1) - posSet(cnt - 2)) + 1
                            tmp = stBuffer.Substring(workPos, workLenStr)
                            '『,』を外す
                            tmpAfter = Replace(tmp, ",", "")
                            '『"』を外す
                            tmpAfter = Replace(tmpAfter, """", "")
                            'stBufferを置換
                            stBuffer = Replace(stBuffer, tmp, tmpAfter)
                            pos = 0
                        End If
                    End If
                End While
                cols = Split(stBuffer, ",")
                If (rowCnt <> 1) And (rowCnt <> 2) Then
                    'Insert into table 
                End If
                If UCase(cols(0)) = "TOTAL" Then
                    Exit While ' Assign 2nd header, then close
                End If
                textLines.Add(cols)
                'csvファイル種類切り分けのため、ヘッダの項目名取得しておく
            End While
        Catch ex As Exception
            cReader.Close()
        Finally
            cReader.Close()
        End Try
        strArr = textLines.ToArray
        'Verify that if the records / data not formated, show the error information to the user
        If strArr.Count < 2 Then
            Call showMsg("Status: Upload Failed <br><br> The CSV file is not valid format!!!", "")
            Exit Sub
        End If
        'Pass the paramters to Update in Datatabase
        Dim _PoStatusModel As PoStatusModel = New PoStatusModel()
        Dim _PoStatusControl As PoStatusControl = New PoStatusControl()
        _PoStatusModel.UPDPG = "Analysis_FileUpload.aspx"
        _PoStatusModel.FileName = fileName
        _PoStatusModel.SrcFileName = strSrcFileName
        _PoStatusModel.UserId = userid
        _PoStatusModel.UserName = userName
        _PoStatusModel.UploadUser = userName
        _PoStatusModel.ShipToBranchCode = DropListLocation.SelectedItem.Value
        _PoStatusModel.ShipToBranch = DropListLocation.SelectedItem.Text
        Dim blStatus As Boolean = _PoStatusControl.AddModifyPoStatus(strArr, _PoStatusModel)
        'If Error Raised then show the errors
        If Not blStatus Then
            Call showMsg("Status: Upload Failed <br><br>  Please check the content of file ", "")
            Exit Sub
        Else
            Call showMsg("Status: Upload Success <br><br> The file has been uploaded", "")
        End If

    End Sub






    Private Sub Item22(filenameFullPath As String, fileName As String, userid As String, userName As String, strSrcFileName As String)
        'Read the content from stored file
        Dim cReader
        Dim textLines As New List(Of String())
        Dim strArr()() As String

        'Info received from the CSV file
        Dim csvSscName As String = ""
        Dim csvDate As String = ""

        Try
            'StreamReader の新しいインスタンスを生成する
            cReader = New System.IO.StreamReader(filenameFullPath, System.Text.Encoding.Default)
            Dim rowCnt As Integer
            '読み込んだものを追加で格納
            Dim cols() As String
            Dim colsHead() As String 'ヘッダ１行目
            '   Dim colsHead2() As String 'ヘッダ２行目
            Dim colsValues() As String
            While (cReader.Peek() >= 0)
                'ファイルを1 行ずつ読み込む
                Dim stBuffer As String = cReader.ReadLine()
                rowCnt = rowCnt + 1
                '『"』の位置取得
                Dim pos As Integer = 0
                '全ての『"』の位置情報をセット
                Dim posSet() As Integer
                Dim cnt As Integer = 0
                Dim key As String = """"
                Dim tmp As String = ""
                Dim tmpAfter As String = ""
                Dim workPos As Integer
                Dim workLenStr As Integer
                While True
                    If rowCnt = 1 Then
                        colsHead = Split(stBuffer, ",")
                        csvSscName = colsHead(0)
                        csvDate = colsHead(1)
                        rowCnt = rowCnt + 1
                        Exit While ' Assign 1st header, SSC1	2020/2/10 then close
                    ElseIf rowCnt = 2 Then
                        Exit While  ' Assign 2nd header, month	day	note	Customer_Visit	Call_Registered	Goods_Delivered	Pending_Calls	Cancelled_Calls	location then close
                    Else
                        colsValues = Split(stBuffer, ",")
                    End If
                    pos = stBuffer.IndexOf(key, pos)
                    If pos = -1 Then
                        Exit While
                    Else
                        ReDim Preserve posSet(cnt)
                        posSet(cnt) = pos
                        cnt = cnt + 1
                        pos = pos + 1
                        If cnt Mod 2 = 0 Then
                            '2の倍数になったら(""で囲まれた文字列取得できたら"Then)その文字列の最初の位置と長さを取得
                            workPos = posSet(cnt - 2)
                            workLenStr = (posSet(cnt - 1) - posSet(cnt - 2)) + 1
                            tmp = stBuffer.Substring(workPos, workLenStr)
                            '『,』を外す
                            tmpAfter = Replace(tmp, ",", "")
                            '『"』を外す
                            tmpAfter = Replace(tmpAfter, """", "")
                            'stBufferを置換
                            stBuffer = Replace(stBuffer, tmp, tmpAfter)
                            pos = 0
                        End If
                    End If
                End While
                cols = Split(stBuffer, ",")
                If (rowCnt <> 1) And (rowCnt <> 2) Then
                    'Insert into table 
                End If
                If UCase(cols(0)) = "TOTAL" Then
                    Exit While ' Assign 2nd header, then close
                End If
                textLines.Add(cols)
                'csvファイル種類切り分けのため、ヘッダの項目名取得しておく
            End While
        Catch ex As Exception
            cReader.Close()
        Finally
            cReader.Close()
        End Try
        strArr = textLines.ToArray
        'Verify that if the records / data not formated, show the error information to the user
        If strArr.Count < 2 Then
            Call showMsg("Status: Upload Failed <br><br> The CSV file is not valid format!!!", "")
            Exit Sub
        End If
        'Pass the paramters to Update in Datatabase
        Dim _ActivityReportModel As ActivityReportModel = New ActivityReportModel()
        Dim _ActivityReportControl As ActivityReportControl = New ActivityReportControl()
        _ActivityReportModel.UPDPG = "Analysis_FileUpload.aspx"
        _ActivityReportModel.FILE_NAME = fileName
        _ActivityReportModel.SRC_FILE_NAME = strSrcFileName
        _ActivityReportModel.UserId = userid
        _ActivityReportModel.DATE_TIME_RPA_CREATE = csvDate
        _ActivityReportModel.UPLOAD_USER = userName
        _ActivityReportModel.SHIP_TO_BRANCH_CODE = DropListLocation.SelectedItem.Value
        _ActivityReportModel.SHIP_TO_BRANCH = DropListLocation.SelectedItem.Text
        Dim blStatus As Boolean = _ActivityReportControl.AddModifyActivityReport(strArr, _ActivityReportModel)
        'If Error Raised then show the errors
        If Not blStatus Then
            Call showMsg("Status: Upload Failed <br><br>  Please check the content of file ", "")
            Exit Sub
        Else
            Call showMsg("Status: Upload Success <br><br> The file has been uploaded", "")
        End If

    End Sub





    Protected Sub showMsg(ByVal Msg As String, ByVal msgChk As String)

        Dim FileName As String = Session("File_Name")
        Dim billingDateAll() As String = Session("billingDate_All")
        Dim deliveryDateAll() As String = Session("deliveryDate_All")

        'Array.Sort(billingDateAll)
        'Array.Sort(deliveryDateAll)

        Dim clsSetCommon As New Class_common
        Dim dtnow As DateTime = clsSetCommon.dtIndia

        ListMsg.Items.Clear()

        'メッセージ欄にメッセージ記載
        ListMsg.Items.Add(Msg & FileName & "  " & dtnow)

        'メッセージ履歴欄にメッセージ記載
        '※履歴の為メッセージは残す
        ListHistory.Items.Add(Msg & FileName & "  " & dtnow)

        '登録された請求日期間を記載
        '※請求日に誤りがないか確認するもの
        If billingDateAll IsNot Nothing Then
            If billingDateAll.Length = 1 Then
                ListHistory.Items.Add("Registered data is " & billingDateAll(0) & " ")
            Else
                ListHistory.Items.Add("Registered data is " & billingDateAll(0) & "～" & billingDateAll(billingDateAll.Length - 1) & " ")
            End If
        End If

        If deliveryDateAll IsNot Nothing Then
            If deliveryDateAll.Length = 1 Then
                ListHistory.Items.Add("Registered data is " & deliveryDateAll(0) & " ")
            Else
                ListHistory.Items.Add("Registered data is " & deliveryDateAll(0) & "～" & deliveryDateAll(deliveryDateAll.Length - 1) & " ")
            End If
        End If

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

        Session("File_Name") = Nothing
        Session("billingDate_All") = Nothing
        Session("deliveryDate_All") = Nothing

    End Sub

    ''Protected Sub btnback_Click(sender As Object, e As ImageClickEventArgs) Handles btnback.Click

    ''    Response.Redirect("Menu.aspx")

    ''End Sub

    Private Sub RadioBtnDD_CheckedChanged(sender As Object, e As EventArgs) Handles RadioBtnDD.CheckedChanged

        If RadioBtnDD.Checked = True Then
            RadioBtnMM.Checked = False
            FileUploadAnalysis.Enabled = True
        End If

    End Sub

    Private Sub RadioBtnMM_CheckedChanged(sender As Object, e As EventArgs) Handles RadioBtnMM.CheckedChanged

        If RadioBtnMM.Checked = True Then
            RadioBtnDD.Checked = False
            FileUploadAnalysis.Enabled = True
        End If

    End Sub

    '''''Private Sub DropListUploadFile_SelectedIndexChanged(sender As Object, e As EventArgs) Handles DropListUploadFile.SelectedIndexChanged
    '''''    Dim selectIndex As Integer = DropListUploadFile.SelectedIndex
    '''''    Select Case selectIndex
    '''''        Case 1, 5
    '''''            FileUploadAnalysis.Enabled = False
    '''''            TextPartsInvoiceNo.Text = ""
    '''''            TextLaborInvoiceNo.Text = ""
    '''''            TextInvoiceDate.Text = ""
    '''''            TextPartsInvoiceNo.Enabled = False
    '''''            TextLaborInvoiceNo.Enabled = False
    '''''            TextInvoiceDate.Enabled = False

    '''''            RadioBtnMM.Checked = False
    '''''            RadioBtnDD.Checked = False
    '''''            RadioBtnMM.Enabled = True
    '''''            RadioBtnDD.Enabled = True

    '''''        Case 2, 6

    '''''            FileUploadAnalysis.Enabled = True

    '''''            RadioBtnMM.Checked = False
    '''''            RadioBtnDD.Checked = False
    '''''            RadioBtnMM.Enabled = False
    '''''            RadioBtnDD.Enabled = False

    '''''            TextPartsInvoiceNo.Text = ""
    '''''            TextLaborInvoiceNo.Text = ""
    '''''            TextInvoiceDate.Text = ""
    '''''            TextPartsInvoiceNo.Enabled = False
    '''''            TextLaborInvoiceNo.Enabled = False
    '''''            TextInvoiceDate.Enabled = False

    '''''        Case 3, 4

    '''''            FileUploadAnalysis.Enabled = True

    '''''            RadioBtnMM.Checked = False
    '''''            RadioBtnDD.Checked = False
    '''''            RadioBtnMM.Enabled = False
    '''''            RadioBtnDD.Enabled = False

    '''''            TextPartsInvoiceNo.Text = ""
    '''''            TextLaborInvoiceNo.Text = ""
    '''''            TextInvoiceDate.Text = ""
    '''''            TextPartsInvoiceNo.Enabled = True
    '''''            TextLaborInvoiceNo.Enabled = True
    '''''            TextInvoiceDate.Enabled = True

    '''''    End Select



    '''''End Sub


    ''' <summary>
    ''' Loading All branches
    ''' </summary>
    Private Sub InitDropDownList()
        Log4NetControl.ComInfoLogWrite(Session("UserName"))
        Dim userName As String = Session("user_id") 'Session("user_Name")
        'Clear the branch location
        DropListLocation.Items.Clear()
        'For store the branch codes in array
        Dim shipCodeAll() As String
        'Verify entered user and password correct or not with the database
        Dim _UserInfoModel As UserInfoModel = New UserInfoModel()
        Dim _UserInfoControl As UserInfoControl = New UserInfoControl()
        _UserInfoModel.UserId = userName
        '_UserInfoModel.Password = TextPass.Text.ToString().Trim()
        Dim UserInfoList As List(Of UserInfoModel) = _UserInfoControl.SelectUserInfo(_UserInfoModel)
        'User Doesn't exist
        If UserInfoList Is Nothing OrElse UserInfoList.Count = 0 Then
            Call showMsg("The username / password incorrect. Please try again", "")
            Exit Sub
        End If
        'Loading All Branch Codes and stored in a array for the session
        Dim _ShipBaseControl As ShipBaseControl = New ShipBaseControl()
        Dim dt As DataTable = _ShipBaseControl.SelectBranchCode()
        ReDim shipCodeAll(dt.Rows.Count - 1)
        Dim i As Integer = 0
        For Each dr As DataRow In dt.Rows
            If dr("ship_code") IsNot DBNull.Value Then
                shipCodeAll(i) = dr("ship_code")
            End If
            i = i + 1
        Next
        Log4NetControl.ComInfoLogWrite(Session("UserName"))
        Dim codeMasterControl As CodeMasterControl = New CodeMasterControl()
        'QryFlag 
        'QryFlag 1 - # Specific Filter
        'QryFlag 2 - # All records
        Dim QryFlag As Integer = 1 'Specific Records
        If (UserInfoList(0).UserLevel = CommonConst.UserLevel0) Or
                        (UserInfoList(0).UserLevel = CommonConst.UserLevel1) Or
                        (UserInfoList(0).UserLevel = CommonConst.UserLevel2) Or
                (UserInfoList(0).AdminFlg = True) Then
            QryFlag = 2
        End If
        Dim codeMasterList As List(Of CodeMasterModel) = codeMasterControl.SelectBranchMasterSony(QryFlag, "'" & UserInfoList(0).Ship1.Replace(",", "','") & "'")

        ' Dim codeMasterControl As CodeMasterControl = New CodeMasterControl()
        'Loading branch name and code in the dropdown list
        '  Dim codeMasterList As List(Of CodeMasterModel) = codeMasterControl.SelectBranchMaster()
        Dim codeMaster1 As CodeMasterModel = New CodeMasterModel()
        codeMaster1.CodeValue = "Select Branch"
        codeMaster1.CodeDispValue = "Select Branch"
        codeMasterList.Insert(0, codeMaster1)
        Dim codeMaster2 As CodeMasterModel = New CodeMasterModel()
        '''''codeMaster2.CodeValue = "ALL"
        '''''codeMaster2.CodeDispValue = "ALL"
        '''''codeMasterList.Insert(1, codeMaster2)

        Me.DropListLocation.DataSource = codeMasterList
        Me.DropListLocation.DataTextField = "CodeDispValue"
        Me.DropListLocation.DataValueField = "CodeValue"
        Me.DropListLocation.DataBind()
    End Sub



    '''''''''Protected Sub btnSend_Click(sender As Object, e As ImageClickEventArgs) Handles btnSend.Click
    '''''''''    '***セッション情報取得***
    '''''''''    Dim ShipName As String = Session("ship_Name")
    '''''''''    Dim shipCode As String = Session("ship_code")
    '''''''''    Dim userName As String = Session("user_Name")
    '''''''''    Dim userid As String = Session("user_id")
    '''''''''    Dim uploadShipname As String = Session("upload_Shipname")
    '''''''''    Dim uploadFilename As String = Session("upload_Filename")
    '''''''''    Dim listHistoryMsg() As String = Session("list_History_Msg")
    '''''''''    '***入力チェック***
    '''''''''    If ShipName = "" Then
    '''''''''        Call showMsg("The session was cleared. Please login again.", "")
    '''''''''        Exit Sub
    '''''''''    End If
    '''''''''    If DropListLocation.Text = "Select shipname" Then
    '''''''''        Call showMsg("Please specify Upload Branch.", "")
    '''''''''        Exit Sub
    '''''''''    End If
    '''''''''    If DropListUploadFile.Text = "Select upload Filename" Then
    '''''''''        Call showMsg("Please specify Upload File.", "")
    '''''''''        Exit Sub
    '''''''''    End If
    '''''''''    '***拠点コード取得***
    '''''''''    Dim clsSetCommon As New Class_common
    '''''''''    Dim setShipCode As String
    '''''''''    Dim errMsg As String
    '''''''''    Call clsSetCommon.setShipCode(uploadShipname, setShipCode, errMsg)
    '''''''''    If errMsg <> "" Then
    '''''''''        Call showMsg(errMsg, "")
    '''''''''        Exit Sub
    '''''''''    End If
    '''''''''    '***CSVファイルの種類の設定***
    '''''''''    Dim kindCsv As Integer = Left(uploadFilename, 1)
    '''''''''    If kindCsv = 1 Or kindCsv = 5 Then
    '''''''''        If RadioBtnDD.Checked = False And RadioBtnMM.Checked = False Then
    '''''''''            Call showMsg("Please specify the date type of Invoice Date.", "")
    '''''''''            Exit Sub
    '''''''''        End If
    '''''''''    End If
    '''''''''    '***daily種類の設定***
    '''''''''    'Invoice Dateの日付型がmから始まる：mm/dd/yyyy　又は　dから始まる：dd/mm/yyyy かを確認
    '''''''''    Dim dailyKind As String
    '''''''''    If RadioBtnDD.Checked = True Then
    '''''''''        'dから始まる
    '''''''''        dailyKind = "DD"
    '''''''''    Else
    '''''''''        dailyKind = "MM"
    '''''''''    End If
    '''''''''    '***ファイルがアップロードされていれば***
    '''''''''    If FileUploadAnalysis.HasFile = True Then
    '''''''''        Try
    '''''''''            '***インポートファイル(CSV)情報の取得***
    '''''''''            Dim FileName As String
    '''''''''            Dim clsSet As New Class_analysis
    '''''''''            Dim invoiceData As Class_analysis.INVOICE
    '''''''''            Dim csvChk As String = "True"
    '''''''''            '項目数
    '''''''''            Dim colLen As Integer
    '''''''''            'ファイル名取得 
    '''''''''            FileName = FileUploadAnalysis.FileName
    '''''''''            If FileName <> "" Then
    '''''''''                Session("File_Name") = FileName
    '''''''''                '■***アップロード可能チェック***
    '''''''''                '**共通チェック**
    '''''''''                Dim FileNameChk() As String
    '''''''''                FileNameChk = Split(FileName, "_")
    '''''''''                '※画面からの指定拠点名とファイル名の拠点が一致するか
    '''''''''                If FileNameChk.Length <> 1 Then
    '''''''''                    Dim tmp As String = FileNameChk(0)
    '''''''''                    If Left(tmp, 3) <> "SSC" Then
    '''''''''                        Call showMsg("File name does not begin with SSC. please confirm.", "")
    '''''''''                        Exit Sub
    '''''''''                    End If
    '''''''''                    If tmp <> uploadShipname Then
    '''''''''                        Call showMsg("You can not upload at the specified location.", "")
    '''''''''                        Exit Sub
    '''''''''                    End If
    '''''''''                Else
    '''''''''                    Call showMsg("Can not upload. Please check the file name.", "")
    '''''''''                    Exit Sub
    '''''''''                End If
    '''''''''                '**3,4のケース**
    '''''''''                '入力チェックと設定
    '''''''''                If kindCsv = 3 Or kindCsv = 4 Then
    '''''''''                    Dim dt As DateTime
    '''''''''                    If TextPartsInvoiceNo.Text = "" Then
    '''''''''                        Call showMsg("Please enter Parts InvoiceNo.", "")
    '''''''''                        Exit Sub
    '''''''''                    Else
    '''''''''                        invoiceData.Parts_invoice_No = Trim(TextPartsInvoiceNo.Text)
    '''''''''                    End If
    '''''''''                    If TextLaborInvoiceNo.Text = "" Then
    '''''''''                        Call showMsg("Please enter Labor InvoiceNo.", "")
    '''''''''                        Exit Sub
    '''''''''                    Else
    '''''''''                        invoiceData.Labor_Invoice_No = Trim(TextLaborInvoiceNo.Text)
    '''''''''                    End If
    '''''''''                    If TextInvoiceDate.Text <> "" Then
    '''''''''                        If DateTime.TryParse(TextInvoiceDate.Text, dt) Then
    '''''''''                            invoiceData.Invoice_date = DateTime.Parse(Trim(TextInvoiceDate.Text))
    '''''''''                        Else
    '''''''''                            Call showMsg("The date specification of InvoiceDate is incorrect.", "")
    '''''''''                            Exit Sub
    '''''''''                        End If
    '''''''''                    Else
    '''''''''                        Call showMsg("Please enter Invoice Date.", "")
    '''''''''                        Exit Sub
    '''''''''                    End If
    '''''''''                    If kindCsv = 3 Then
    '''''''''                        invoiceData.number = "C"
    '''''''''                    Else
    '''''''''                        invoiceData.number = "EXC"
    '''''''''                    End If
    '''''''''                End If
    '''''''''                'サーバの指定パスに保存
    '''''''''                FileUploadAnalysis.SaveAs(clsSet.savePass & FileName)
    '''''''''                '■***CSVデータ取得***
    '''''''''                Dim errFlg As Integer
    '''''''''                If kindCsv <> 7 Then
    '''''''''                    csvData = clsSet.getCsvData(clsSet.savePass & FileName, colLen, errFlg, csvChk, kindCsv)
    '''''''''                Else
    '''''''''                    csvData = clsSet.getCsvData2(clsSet.savePass & FileName, colLen, errFlg, csvChk, kindCsv)
    '''''''''                End If
    '''''''''                If System.IO.File.Exists(clsSet.savePass & FileName) = True Then
    '''''''''                    System.IO.File.Delete(clsSet.savePass & FileName)
    '''''''''                End If
    '''''''''                If errFlg = 1 Then
    '''''''''                    Call showMsg("Failed to acquire the import file.", "")
    '''''''''                    Exit Sub
    '''''''''                End If
    '''''''''                If csvData Is Nothing Then
    '''''''''                    Call showMsg("There was no data of the import file.", "")
    '''''''''                    Exit Sub
    '''''''''                End If
    '''''''''                If csvChk = "False" Then
    '''''''''                    Call showMsg("Importing can not be performed because the header information of the specified file is invalid.", "")
    '''''''''                    Exit Sub
    '''''''''                End If
    '''''''''                '■***CSVデータの登録***
    '''''''''                Dim dtNow As DateTime = clsSetCommon.dtIndia
    '''''''''                Dim tmpHHMM As String = Replace(dtNow.ToString("HH:mm"), ":", "")
    '''''''''                Dim tmpYYYYMMDD As String = Replace(dtNow.ToShortDateString, "/", "")
    '''''''''                Dim setFileName As String = FileName & "_" & tmpYYYYMMDD & "_" & tmpHHMM
    '''''''''                Select Case kindCsv
    '''''''''                    Case 1
    '''''''''                        '■■DailyReport
    '''''''''                        '■***ファイル内容チェック***
    '''''''''                        '日付変換(ddmmyyyy⇒mmddyyyy)してから日付型のチェック
    '''''''''                        '□BillingDate
    '''''''''                        If csvData(2)(3) = "" Then
    '''''''''                            Call showMsg("Not found the BillingDate. please confirm the file.", "")
    '''''''''                            Exit Sub
    '''''''''                        End If
    '''''''''                        Dim BillingDateStr As String = csvData(2)(3)
    '''''''''                        If BillingDateStr.Length <> 10 Then
    '''''''''                            Call showMsg("The date type ((dd.mm.yyyy) or (mm.dd.yyyy)) of BillingDate is incorrect. please confirm the file.", "")
    '''''''''                            Exit Sub
    '''''''''                        End If

    '''''''''                        If RadioBtnDD.Checked = True Then
    '''''''''                            Dim tmpMon As String = BillingDateStr.Substring(3, 2)
    '''''''''                            Dim tmpDay As String = BillingDateStr.Substring(0, 2)
    '''''''''                            Dim tmpYear As String = BillingDateStr.Substring(6, 4)
    '''''''''                            BillingDateStr = tmpMon & "." & tmpDay & "." & tmpYear
    '''''''''                        End If

    '''''''''                        Dim BillingDate As DateTime
    '''''''''                        If DateTime.TryParse(BillingDateStr, BillingDate) Then
    '''''''''                            BillingDate = BillingDateStr
    '''''''''                        Else
    '''''''''                            Call showMsg("The date type of BillingDate is incorrect. please confirm the file.", "")
    '''''''''                            Exit Sub
    '''''''''                        End If

    '''''''''                        '□GoodsDeliveredDate
    '''''''''                        Dim GoodsDeliveredDateStr As String = csvData(2)(4)

    '''''''''                        If GoodsDeliveredDateStr <> "" Then
    '''''''''                            If GoodsDeliveredDateStr.Length <> 10 Then
    '''''''''                                Call showMsg("The description of Goods Delivered Date is not 10 digits. please confirm the file.", "")
    '''''''''                                Exit Sub
    '''''''''                            End If
    '''''''''                        Else
    '''''''''                            Call showMsg("There is no mention of Goods Delivered Date. please confirm the file.", "")
    '''''''''                            Exit Sub
    '''''''''                        End If

    '''''''''                        '■***登録***
    '''''''''                        Dim billingDateAll() As String
    '''''''''                        Call clsSet.setCsvData(csvData, userid, userName, uploadShipname, setShipCode, errFlg, dailyKind, billingDateAll, setFileName)

    '''''''''                        If errFlg = 1 Then
    '''''''''                            Call showMsg("Failed to register the data of the import file.", "")
    '''''''''                            Exit Sub
    '''''''''                        Else
    '''''''''                            Session("billingDate_All") = billingDateAll
    '''''''''                            Call showMsg("Data registration of the import file is completed.", "")

    '''''''''                        End If

    '''''''''                    Case 2
    '''''''''                        '■■WtyExcelDown

    '''''''''                        Dim tourokuFlg As Integer
    '''''''''                        Call clsSet.setCsvDataWtyExcelDown(csvData, userid, userName, errFlg, tourokuFlg, uploadShipname, setShipCode, setFileName)

    '''''''''                        If errFlg = 1 Then
    '''''''''                            Call showMsg("Failed to register the data of the import file.", "")
    '''''''''                        Else
    '''''''''                            If tourokuFlg = 1 Then
    '''''''''                                Call showMsg("Data registration of the import file is completed.", "")
    '''''''''                            Else
    '''''''''                                Call showMsg("There was no subject to register.", "")
    '''''''''                            End If
    '''''''''                        End If

    '''''''''                    Case 3, 4
    '''''''''                        '■■WtyListbyReportNo

    '''''''''                        Call clsSet.setCsvInvoiceUpdate(csvData, userid, userName, errFlg, uploadShipname, invoiceData, setFileName)

    '''''''''                        If errFlg = 1 Then
    '''''''''                            Call showMsg("Failed to register the data of the import file.", "")
    '''''''''                        Else
    '''''''''                            Call showMsg("Data registration of the import file is completed.", "")
    '''''''''                        End If

    '''''''''                    Case 5
    '''''''''                        '■■good_recived
    '''''''''                        '日付変換(ddmmyyyy⇒mmddyyyy)してから日付型のチェック
    '''''''''                        '□delivery_date
    '''''''''                        If csvData(2)(2) = "" Then
    '''''''''                            Call showMsg("Not found the delivery date. please confirm", "")
    '''''''''                            Exit Sub
    '''''''''                        End If

    '''''''''                        Dim deliveryDateStr As String = csvData(2)(2)

    '''''''''                        If deliveryDateStr.Length <> 10 Then
    '''''''''                            Call showMsg("The date type of delivery_date ((dd.mm.yyyy) or (mm.dd.yyyy)) is incorrect. please confirm the file.", "")
    '''''''''                            Exit Sub
    '''''''''                        End If

    '''''''''                        If RadioBtnDD.Checked = True Then
    '''''''''                            Dim tmpMon As String = deliveryDateStr.Substring(3, 2)
    '''''''''                            Dim tmpDay As String = deliveryDateStr.Substring(0, 2)
    '''''''''                            Dim tmpYear As String = deliveryDateStr.Substring(6, 4)
    '''''''''                            deliveryDateStr = tmpMon & "." & tmpDay & "." & tmpYear
    '''''''''                        End If

    '''''''''                        Dim deliveryDate As DateTime
    '''''''''                        If DateTime.TryParse(deliveryDateStr, deliveryDate) Then
    '''''''''                            deliveryDate = deliveryDateStr
    '''''''''                        Else
    '''''''''                            Call showMsg("The date type of Billing Date is incorrect. please confirm the file.", "")
    '''''''''                            Exit Sub
    '''''''''                        End If

    '''''''''                        Dim deliveryDateAll() As String
    '''''''''                        Call clsSet.setCsvGoodRecived(csvData, userid, userName, errFlg, uploadShipname, dailyKind, deliveryDateAll, setFileName)

    '''''''''                        If errFlg = 1 Then
    '''''''''                            Call showMsg("Failed to register the data of the import file.", "")
    '''''''''                        Else
    '''''''''                            Session("deliveryDate_All") = deliveryDateAll
    '''''''''                            Call showMsg("Data registration of the import file is completed.", "")
    '''''''''                        End If

    '''''''''                    Case 6
    '''''''''                        '■■Billing_Info

    '''''''''                        '■***ファイル内容チェック***
    '''''''''                        '□BillingDate
    '''''''''                        If csvData(1)(2) = "" Then
    '''''''''                            Call showMsg("Not found the Billing Date. please confirm the file.", "")
    '''''''''                            Exit Sub
    '''''''''                        End If

    '''''''''                        Dim BillingDateStr As String = csvData(1)(2)

    '''''''''                        If BillingDateStr.Length <> 8 Then
    '''''''''                            Call showMsg("The date type (yyyymmdd) of Billing Date is incorrect. please confirm the file.", "")
    '''''''''                            Exit Sub
    '''''''''                        End If

    '''''''''                        Dim tmpMon As String = BillingDateStr.Substring(4, 2)
    '''''''''                        Dim tmpDay As String = BillingDateStr.Substring(6, 2)
    '''''''''                        Dim tmpYear As String = BillingDateStr.Substring(0, 4)

    '''''''''                        BillingDateStr = tmpYear & "/" & tmpMon & "/" & tmpDay

    '''''''''                        Dim BillingDate As DateTime

    '''''''''                        If DateTime.TryParse(BillingDateStr, BillingDate) Then
    '''''''''                        Else
    '''''''''                            Call showMsg("The date type (yyyymmdd) of Billing Date is incorrect. please confirm the file.", "")
    '''''''''                            Exit Sub
    '''''''''                        End If

    '''''''''                        '■***登録***
    '''''''''                        Call clsSet.setCsvBillingInfo(csvData, userid, userName, errFlg, uploadShipname, setFileName)

    '''''''''                        If errFlg = 1 Then
    '''''''''                            Call showMsg("Failed to register the data of the import file.", "")
    '''''''''                        Else
    '''''''''                            Call showMsg("Data registration of the import file is completed.", "")
    '''''''''                        End If

    '''''''''                End Select

    '''''''''            End If

    '''''''''        Catch ex As Exception
    '''''''''            Call showMsg("Data Import Error Please retry.", "")
    '''''''''        End Try

    '''''''''    Else
    '''''''''        Call showMsg("Please specify the import file.", "")
    '''''''''    End If

    '''''''''End Sub

End Class