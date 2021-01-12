Imports System.Globalization
Imports System.IO
Imports Ganges33.Ganges33.model
Imports Ganges33.Ganges33.logic

Public Class Analysis_Cash_Tracking_imp
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

            '***拠点名称の設定***
            DropListLocation.Items.Clear()

            'If userLevel = "0" Or userLevel = "1" Or userLevel = "2" Or adminFlg = True Then
            '    Dim shipName() As String
            '    If Session("ship_name_list") IsNot Nothing Then
            '        shipName = Session("ship_name_list")
            '        With DropListLocation
            '            .Items.Add("Select Branch")
            '            For i = 0 To UBound(shipName)
            '                If Trim(shipName(i)) <> "" Then
            '                    .Items.Add(shipName(i))
            '                End If
            '            Next i
            '        End With
            '    End If
            'Else
            '    DropListLocation.Items.Add(setShipname)
            'End If

            InitDropDownList()

            ''''''''LoadCumlativeAccounts()

            'Load all incomplete
            GridViewBind(setShipname)

        Else

            '***セッション設定***
            Session("upload_Shipname") = DropListLocation.Text


        End If

    End Sub

    Protected Sub btnSend_Click(sender As Object, e As ImageClickEventArgs) Handles btnSend.Click
        Dim userName As String = Session("user_Name")
        Dim userid As String = Session("user_id")
        'Verify that location has been selected or not
        If DropListLocation.Text = "Select Branch" Then
            Call showMsg("Select the Branch", "")
            Exit Sub
        End If
        'If File not uploaded then show the error msg to the user
        If Not FileUploadAnalysis.HasFile Then
            Call showMsg("File Not Uploaded!!!", "")
            Exit Sub
        End If
        'Find CSV file 
        Dim strExtension As String = System.IO.Path.GetExtension(FileUploadAnalysis.FileName)
        If Not (strExtension = ".csv") Then
            Call showMsg("Uploaded file must be CSV", "")
            Exit Sub
        End If
        'Move the file to temporary folder
        If Not Directory.Exists(ConfigurationManager.AppSettings("RootDir") & "\" & userName & "\" & DropListLocation.Text) Then
            Directory.CreateDirectory(ConfigurationManager.AppSettings("RootDir") & "\" & userName & "\" & DropListLocation.Text)
        End If
        'Assign File Name
        Dim dirPath As String = ConfigurationManager.AppSettings("RootDir") & "\" & userName & "\" & DropListLocation.Text
        Dim fileName As String = DateTime.Now.ToString("yyyyMMddHHmmssfff") & strExtension
        Dim filenameFullPath As String = dirPath & "\" & fileName
        'Save the file in temporary folder
        FileUploadAnalysis.SaveAs(filenameFullPath)
        'Temporary Table 
        ' Create new DataTable instance.
        Dim table As New DataTable
        ' Create four typed columns in the DataTable.
        table.Columns.Add("BilingDate", GetType(Date))
        Dim tmpDate As String = ""
        'Read the content from stored file
        Dim cReader
        Dim textLines As New List(Of String())
        Dim strArr()() As String
        'Always True
        Dim isValid As Boolean = True
        Dim isAbnormal As Boolean = False
        Dim isAbnormalAll As Boolean = False
        Dim isDateNull As Boolean = False
        Dim strAbnormal As String = ""


        Try
            'StreamReader の新しいインスタンスを生成する
            cReader = New System.IO.StreamReader(filenameFullPath, System.Text.Encoding.Default)

            Dim rowCnt As Integer

            '読み込んだものを追加で格納
            Dim cols() As String
            Dim colsHead() As String 'ヘッダ１行目
            Dim colsHead2() As String 'ヘッダ２行目
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
                    isDateNull = False  'intialize as false
                    isAbnormal = False 'intialize as false

                    If rowCnt = 1 Then
                        colsHead = Split(stBuffer, ",")
                        Exit While ' Assign 1st header, then close
                    ElseIf rowCnt = 2 Then
                        colsHead2 = Split(stBuffer, ",")
                        Exit While ' Assign 2nd header, then close
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

                If (rowCnt <> 1) And (rowCnt <> 2) Then 'No need to check header1/header2
                    ''''''''''''''''''''''' If tmpDate <> cols(3) Then ---->No Need now 
                    ''Need to check dd/MM/yyyy / mm/DD/yyyy
                    Dim strDate As String = cols(3) ' Billing Date
                        Dim strDate1 As String = cols(4) ' Goods Delivered Date
                        If rbtnDate.SelectedValue = "DD" Then
                            strDate = strDate.Replace(".", "/")
                            strDate = DateTime.ParseExact(strDate, "dd/MM/yyyy", Nothing).ToString("yyyy/MM/dd")
                            'For Goods Delivered
                            If strDate1 <> "00.00.0000" Then ' No Need to Convert
                                strDate1 = strDate1.Replace(".", "/")
                                strDate1 = DateTime.ParseExact(strDate1, "dd/MM/yyyy", Nothing).ToString("yyyy/MM/dd")
                            End If
                        Else
                            strDate = strDate.Replace(".", "/")
                            strDate = DateTime.ParseExact(strDate, "MM/dd/yyyy", Nothing).ToString("yyyy/MM/dd")
                            'For Goods Delivered
                            If strDate1 <> "00.00.0000" Then ' No Need to Convert
                                strDate1 = strDate1.Replace(".", "/")
                                strDate1 = DateTime.ParseExact(strDate1, "MM/dd/yyyy", Nothing).ToString("yyyy/MM/dd")
                            End If
                        End If

                        If strDate = "00.00.0000" Then
                            Call showMsg("Status: Upload Failed <br><br> The billing date is Invalid <br><br> Billing Date: " & cols(3) & "  !!!", "")
                            Exit Sub
                        End If
                    If strDate1 = "00.00.0000" Then
                        isDateNull = True
                        ''Call showMsg("Status: Upload Failed <br><br> The billing date is Invalid <br><br> Delivery Date: " & cols(4) & "  !!!", "")
                        ''Exit Sub
                    End If

                    If Not isDateNull Then
                        Dim dt As Date 'Billing Date
                        Dim dt1 As Date 'Goods Delivered
                        If Date.TryParse(strDate, dt) Then
                            table.Rows.Add(strDate)
                            ''''''''''''''''''''If strDate1 <> "00.00.0000" Then 'When there is no deliverey date, then no need to check it  
                            If Date.TryParse(strDate1, dt1) Then
                                    'Condition / Logic 1: BillingDate = GoodsDelivered
                                    If dt = dt1 Then
                                        'OK
                                    ElseIf dt > dt1 Then 'Billing Date is Small then Goods Delivered
                                        'Allowed Ex. 03.12.2019 > 03.11.2019

                                    ElseIf dt < dt1 Then 'Biiling Date is big then Goods Delivered
                                    'Not Allowed Exc. 03.12.2019 < 03.13.2019
                                    'NG: This scenario not possible. Tomorrow can not deliver
                                    ''''Call showMsg("Status: Upload Failed <br><br> The billing date of Service Order No. " & cols(0) & " has future Delivery Date. <br><br> Billing Date: " & cols(3) & " and Delivery Date: " & cols(4) & " !!!", "")
                                    ''''Exit Sub
                                    '''
                                    isAbnormal = True
                                    isAbnormalAll = True
                                    strAbnormal = strAbnormal & " Service Order No. " & cols(0) & "<br>" '" Billing Date: " & cols(3) & " and Delivery Date: " & cols(4) & "<br>"
                                End If
                                Else
                                    Call showMsg("Status: Upload Failed <br><br> The Delivery Date is not valid format (" & cols(4) & ") !!!", "")
                                    Exit Sub
                                End If
                                '''''''''''''''''''''''End If

                            Else
                            Call showMsg("Status: Upload Failed <br><br> The Billing Date is not valid format (" & cols(3) & ") !!!", "")
                            Exit Sub
                        End If
                    End If
                    ''''''''''''''''''''' tmpDate = cols(3) 'No Need now

                    ''''''''Dim dt As Date 'Billing Date
                    ''''''''Dim dt1 As Date 'Goods Delivered
                    ''''''''If Date.TryParse(strDate, dt) Then
                    ''''''''    table.Rows.Add(strDate)
                    ''''''''    If strDate1 <> "00.00.0000" Then 'When there is no deliverey date, then no need to check it  
                    ''''''''        If Date.TryParse(strDate1, dt1) Then
                    ''''''''            'Condition / Logic 1: BillingDate = GoodsDelivered
                    ''''''''            If dt = dt1 Then
                    ''''''''            ElseIf dt < dt1 Then 'Billing Date is Small then Goods Delivered
                    ''''''''            ElseIf dt > dt1 Then 'Biiling Date is big then Goods Delivered
                    ''''''''                'Not Allowed to 
                    ''''''''                Call showMsg("Status: Upload Failed <br><br> The billing date of Service Order No. " & cols(0) & " has surpassed the Delivery Date/Repair date. <br><br> Billing Date: " & cols(3) & " and Delivery Date: " & cols(4) & " !!!", "")
                    ''''''''                Exit Sub
                    ''''''''            End If
                    ''''''''        Else
                    ''''''''            Call showMsg("Status: Upload Failed <br><br> The Delivery Date is not valid format (" & cols(4) & ") !!!", "")
                    ''''''''            Exit Sub
                    ''''''''        End If
                    ''''''''    End If

                    ''''''''Else
                    ''''''''    Call showMsg("Status: Upload Failed <br><br> The Billing Date is not valid format (" & cols(3) & ") !!!", "")
                    ''''''''    Exit Sub
                    ''''''''End If
                    ''''''''tmpDate = cols(3)
                    '''''''''''''''''''''''''' End If ---No Need now
                End If

                'If cols(4) Then

                If isDateNull Or isAbnormal Then
                    isDateNull = False  'For Next Items Add, So again intialize as false
                    isAbnormal = False
                Else
                    textLines.Add(cols)
                End If

                'csvファイル種類切り分けのため、ヘッダの項目名取得しておく


            End While

        Catch ex As Exception
            cReader.Close()
            Call showMsg("Status: Upload Failed <br><br> Date Format is invalid!!!", "")
            Exit Sub
        Finally

            cReader.Close()
        End Try

        'Open Status Check
        If UCase(ConfigurationManager.AppSettings("UploadOpenStatus")) = "TRUE" Then
            For Each row As DataRow In table.Rows
                If row("BilingDate") IsNot DBNull.Value Then
                    Dim _MoneyReserveModel As MoneyReserveModel = New MoneyReserveModel()
                    Dim _MoneyReserveControl As MoneyReserveControl = New MoneyReserveControl()
                    _MoneyReserveModel.TDateTime = row("BilingDate")
                    _MoneyReserveModel.ShipCode = DropListLocation.SelectedItem.Value
                    _MoneyReserveModel.UserId = userid
                    '_MoneyReserveModel.UserName = userName
                    _MoneyReserveModel.Status = CommonConst.MONEY_STATUS1
                    Dim tblBR As DataTable = _MoneyReserveControl.SelectTReserveBR(_MoneyReserveModel)
                    If tblBR.Rows.Count <= 0 Then
                        Call showMsg("Status: Upload Failed <br><br> The cash counter is not open ( " & _MoneyReserveModel.TDateTime & " ) ", "")
                        Exit Sub
                    End If
                End If
            Next
        End If
        'Closed Status Check
        If UCase(ConfigurationManager.AppSettings("UploadCloseStatus")) = "TRUE" Then
            For Each row As DataRow In table.Rows
                If row("BilingDate") IsNot DBNull.Value Then
                    Dim _MoneyReserveModel As MoneyReserveModel = New MoneyReserveModel()
                    Dim _MoneyReserveControl As MoneyReserveControl = New MoneyReserveControl()
                    _MoneyReserveModel.TDateTime = row("BilingDate")
                    _MoneyReserveModel.ShipCode = DropListLocation.SelectedItem.Value
                    _MoneyReserveModel.UserId = userid
                    '_MoneyReserveModel.UserName = userName
                    _MoneyReserveModel.Status = CommonConst.MONEY_STATUS5
                    Dim tblBR As DataTable = _MoneyReserveControl.SelectTReserveBR(_MoneyReserveModel)
                    If tblBR.Rows.Count > 0 Then
                        Call showMsg("Status: Upload Failed <br><br> The cash counter is already closed ( " & _MoneyReserveModel.TDateTime & " ) ", "")
                        Exit Sub
                    End If
                End If
            Next
        End If


        strArr = textLines.ToArray
        'Verify that if the records / data not formated, show the error information to the user
        If strArr.Count < 3 Then
            Call showMsg("Status: Upload Failed <br><br> The CSV file is not valid format!!!", "")
            Exit Sub
        End If


        'Return strArr

        'Pass the paramters to Update in Datatabase
        Dim _CashTrackModel As CashTrackModel = New CashTrackModel()
        Dim _CashTrackControl As CashTrackControl = New CashTrackControl()

        _CashTrackModel.FileName = fileName
        _CashTrackModel.UserId = userid
        _CashTrackModel.UserName = userName
        _CashTrackModel.Location = DropListLocation.SelectedItem.Value
        _CashTrackModel.DateType = rbtnDate.SelectedValue
        'import to csv file to database of cash tracking (GSPN to MultiVendor)
        'インポート に CSV ファイル データベース の 現金 
        Dim blStatus As Boolean = _CashTrackControl.UpdateAutoCashTrack(strArr, _CashTrackModel)
        Dim strInfo As String = ""
        'If Error Raised then show the errors
        'もし エラー 上げた  それから  見せる の エラー
        If Not blStatus Then
            strInfo = "Status: Upload Failed <br><br>  Please check the content of file <br><br>The following service order has future Delivery Date.<br>"
            If isAbnormalAll Then
                strInfo = strInfo & "<br><br>The following service order has future Delivery Date.<br>"
                strInfo = strInfo & strAbnormal
            End If
            '''''''''''''''Call showMsg("Status: Upload Failed <br><br>  Please check the content of file ", "")
            Call showMsg(strInfo, "")
            Exit Sub
        Else
            strInfo = "Status: Upload Success <br><br> The file has been uploaded, please update the transaction type "
            If isAbnormalAll Then
                strInfo = strInfo & "<br><br>The following service order has future Delivery Date.<br>"
                strInfo = strInfo & strAbnormal
            End If
            Call showMsg(strInfo, "")
            ''''            Call showMsg("Status: Upload Success <br><br> The file has been uploaded, please update the transaction type ", "")
        End If


        GridViewBind()



    End Sub




    Protected Sub showMsg(ByVal Msg As String, ByVal msgChk As String)

        Dim FileName As String = Session("File_Name")
        Dim billingDateAll() As String = Session("billingDate_All")
        Dim deliveryDateAll() As String = Session("deliveryDate_All")

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


    Protected Sub GridViewBind(ByVal Optional Location As String = "")
        Dim _CashTrackModel As CashTrackModel = New CashTrackModel()
        Dim _CashTrackControl As CashTrackControl = New CashTrackControl()
        If Len(Location) > 0 Then
            _CashTrackModel.Location = Location
        Else
            _CashTrackModel.Location = DropListLocation.SelectedItem.Value
        End If

        Dim dtCashTrack As DataTable = _CashTrackControl.SelectCashTrackIncomplete(_CashTrackModel)
        If dtCashTrack Is Nothing Then
            ViewState("dtCashTrackCount") = 0
        Else
            ViewState("dtCashTrackCount") = dtCashTrack.Rows.Count
        End If
        gvCashTrack.DataSource = dtCashTrack
        ViewState("dtCashTrackCount") = dtCashTrack
        gvCashTrack.DataBind()


        If gvCashTrack.Rows.Count = 0 Then
            'Disable Update process 
            gvCashTrack.Visible = False
            'BtnUpdateCashTrack.Visible = False
            imgUpdateCashTrack.Visible = False
        Else
            'Enable Update Process
            gvCashTrack.Visible = True
            imgUpdateCashTrack.Visible = True
        End If


        LoadCumlativeAccounts()

        '''''''''''If TextCompleteDateFrom.Text.Trim = "" Then
        '''''''''''    Call showMsg("Provide the repair completed date")
        '''''''''''    IntializeTextBox()
        '''''''''''    Exit Sub
        '''''''''''End If

        '''''''''''***セッション情報取得***
        ''''''''''Dim shipCode As String = Session("ship_code")





    End Sub

    Protected Sub imgUpdateCashTrack_Click(sender As Object, e As EventArgs) Handles imgUpdateCashTrack.Click
        'Read
        '"SELECT CRTDT,CRTCD,UPDDT,UPDCD,UPDPG,DELFG,claim_no,invoice_date,Invoice_No,customer_name,Warranty,payment,payment_kind,total_amount,input_user,location,card_number,card_type,deposit,change,count_no,message,FALSE,claim,claim_card,full_discount,discount,discount_after_amt,discount_after_amt_credit,incomplete FROM cash_track "
        '
        '1st 
        'Verify that location has been selected or not
        If DropListLocation.Text = "Select Branch" Then
            Call showMsg("Select the Branch", "")
            Exit Sub
        End If
        'Grid Row Count 
        If Me.gvCashTrack.Rows.Count = 0 Then
            Call showMsg("There is no records to update it", "")
            Exit Sub
        End If
        Dim chkIncomplete As CheckBox
        Dim drpType1 As DropDownList
        Dim txtCash As TextBox
        Dim txtCard As TextBox
        Dim txtDiscount As TextBox
        Dim lblCollectAmt As Label

        'Decimal validation 
        For Each target As GridViewRow In Me.gvCashTrack.Rows
            chkIncomplete = CType(target.FindControl("chkIncomplete"), CheckBox)
            drpType1 = CType(target.FindControl("drpType1"), DropDownList)
            txtCash = CType(target.FindControl("txtCash"), TextBox)
            txtCard = CType(target.FindControl("txtCard"), TextBox)
            txtDiscount = CType(target.FindControl("txtDiscount"), TextBox)
            lblCollectAmt = CType(target.FindControl("lblCollectAmt"), Label)

            If Not chkIncomplete.Checked Then
                If drpType1.SelectedValue = "Cash & Card" Then
                    'For Numeric validation
                    If Not (CommonControl.isNumberCT_CC(txtCash.Text) And
                             CommonControl.isNumberCT_CC(txtCard.Text)) Then
                        Call showMsg("The Cash / Card Amount Is invalid...")
                        Exit Sub
                    End If
                    'For Verify Discount is bigger than invoice amount  
                    If Val(lblCollectAmt.Text) < (Val(txtCash.Text) + Val(txtCard.Text) + Val(txtDiscount.Text)) Then
                        Call showMsg("Discount is bigger than invoice amount  ...")
                        Exit Sub
                    End If
                ElseIf drpType1.SelectedValue = "Cash" Then
                    'For Verify Discount is bigger than invoice amount  
                    If Val(lblCollectAmt.Text) < Val(txtDiscount.Text) Then
                        Call showMsg("Discount is bigger than invoice amount  ...")
                        Exit Sub
                    End If
                ElseIf drpType1.SelectedValue = "Card" Then
                    'For Verify Discount is bigger than invoice amount  
                    If Val(lblCollectAmt.Text) < Val(txtDiscount.Text) Then
                        Call showMsg("Discount is bigger than invoice amount  ...")
                        Exit Sub
                    End If
                End If
            End If
            If Not (CommonControl.isNumberDR(txtDiscount.Text)) Then
                Call showMsg("The Discount Is invalid...")
                Exit Sub
            End If
            'Validation for Amount


        Next

        Dim lstModel As List(Of CashTrackModel) = New List(Of CashTrackModel)()
        Dim chkBoxSelect As Integer = 0
        Dim _CashTrackControl As CashTrackControl = New CashTrackControl()
        For Each target As GridViewRow In Me.gvCashTrack.Rows
            Dim lblServiceOrderNo As Label = CType(target.FindControl("lblServiceOrderNo"), Label)
            lblCollectAmt = CType(target.FindControl("lblCollectAmt"), Label)

            Dim chkCard As CheckBox = CType(target.FindControl("chkCard"), CheckBox)
            txtDiscount = CType(target.FindControl("txtDiscount"), TextBox)
            chkIncomplete = CType(target.FindControl("chkIncomplete"), CheckBox)
            Dim location As HiddenField = CType(target.FindControl("location"), HiddenField)
            If Not String.IsNullOrEmpty(lblServiceOrderNo.Text) Then

                drpType1 = CType(target.FindControl("drpType1"), DropDownList)
                txtCash = CType(target.FindControl("txtCash"), TextBox)
                txtCard = CType(target.FindControl("txtCard"), TextBox)

                'Only for update is required
                'のみ にとって 更新 です 必須
                '''If Not chkIncomplete.Checked Then

                '''End If

                chkBoxSelect = 1
                Dim targetModel As CashTrackModel = New CashTrackModel()
                targetModel.ServiceOrderNo = lblServiceOrderNo.Text
                targetModel.Location = DropListLocation.Text
                'Need to calculate discount
                targetModel.OutTotal = lblCollectAmt.Text
                If txtDiscount.Text = "" Then
                    targetModel.Discount = 0
                Else
                    targetModel.Discount = txtDiscount.Text
                End If

                If drpType1.SelectedValue = "Cash" Then
                    targetModel.MoneyType = 1
                    'Total Amount - Discount Ex. 1000 - 600 = 400 
                    targetModel.Deposit = targetModel.OutTotal - targetModel.Discount
                    targetModel.Claim = targetModel.Deposit
                    targetModel.ClaimCard = 0
                    targetModel.DiscountAfterAmount = targetModel.Deposit
                    targetModel.DiscountAfterCredit = 0
                    'targetModel.CashAmt = txtCash.Text
                ElseIf drpType1.SelectedValue = "Card" Then
                    targetModel.MoneyType = 2
                    'Total Amount - Discount Ex. 1000 - 600 = 400 
                    targetModel.Deposit = 0
                    targetModel.Claim = 0
                    targetModel.ClaimCard = targetModel.OutTotal - targetModel.Discount
                    targetModel.DiscountAfterAmount = 0
                    targetModel.DiscountAfterCredit = targetModel.ClaimCard
                    '  targetModel.CardAmt = txtCard.Text
                ElseIf drpType1.SelectedValue = "Cash & Card" Then
                    targetModel.MoneyType = 3
                    targetModel.CashAmt = txtCash.Text
                    targetModel.CardAmt = txtCard.Text

                    'Total Amount - Discount Ex. 1000 - (300 cash + 100 Card)- 600 Discount = 400 
                    targetModel.Deposit = targetModel.CashAmt 'targetModel.OutTotal - targetModel.Discount
                    targetModel.Claim = targetModel.Deposit
                    targetModel.ClaimCard = targetModel.CardAmt
                    targetModel.DiscountAfterAmount = targetModel.Deposit
                    targetModel.ClaimCard = txtCard.Text
                    targetModel.DiscountAfterCredit = targetModel.ClaimCard



                End If
                'Modified on 2019.02.20
                If chkIncomplete.Checked Then
                    targetModel.InComplete = 1
                Else
                    targetModel.InComplete = 0
                End If


                lstModel.Add(targetModel)
                'End If
            Else
                Exit For
            End If
        Next

        'If selected then update to database
        'もし 選択された それから 更新 に データベース
        If chkBoxSelect = 1 Then
            If _CashTrackControl.UpdateCashTrackIncomplete(lstModel) Then
                Call showMsg("Successfully Updated", "")
                GridViewBind()
            Else
                Call showMsg("Failed to upload it", "")
            End If
        End If
    End Sub
    Protected Sub drpType1_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs)
        Log4NetControl.ComInfoLogWrite(Session("UserName"))
        Dim ddl As DropDownList = CType(sender, DropDownList)
        Dim row As GridViewRow = CType(ddl.Parent.Parent, GridViewRow)
        Dim idx As Integer = row.RowIndex
        Dim lblCash As Label = CType(row.Cells(0).FindControl("lblCash"), Label)
        Dim txtCash As TextBox = CType(row.Cells(0).FindControl("txtCash"), TextBox)
        Dim lblCard As Label = CType(row.Cells(0).FindControl("lblCard"), Label)
        Dim txtCard As TextBox = CType(row.Cells(0).FindControl("txtCard"), TextBox)

        If ddl.SelectedItem.Text = "Cash & Card" Then
            lblCash.Visible = True
            txtCash.Visible = True
            lblCard.Visible = True
            txtCard.Visible = True
        Else
            lblCash.Visible = False
            txtCash.Visible = False
            lblCard.Visible = False
            txtCard.Visible = False
        End If



    End Sub

    Protected Sub btnOpen_Click(sender As Object, e As ImageClickEventArgs) Handles btnOpen.Click
        Response.Redirect("Analysis_Cash_Tracking_qg.aspx")
    End Sub

    Protected Sub showMsg(ByVal Msg As String)
        lblMsg.Text = Msg
        Dim sScript As String = "$(function () { $( ""#dialog"" ).dialog({width: 400, buttons: {""OK"": function () {$(this).dialog('close');}}});});"
        'JavaScriptの埋め込み
        ClientScript.RegisterStartupScript(Me.GetType(), "startup", sScript, True)

    End Sub

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
            Call showMsg("The username / password incorrect. Please try again")
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
        Dim codeMasterList As List(Of CodeMasterModel) = codeMasterControl.SelectBranchMaster(QryFlag, "'" & UserInfoList(0).Ship1.Replace(",", "','") & "'")

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

    Protected Sub DropListLocation_SelectedIndexChanged(sender As Object, e As EventArgs) Handles DropListLocation.SelectedIndexChanged
        GridViewBind()


        ''ViewState("dtCashTrackCount")
        '        If Not (ViewState("dtCashTrackCount") IsNot Nothing AndAlso ViewState("dtCashTrackCount").ToString() <> String.Empty) Then




    End Sub

    Public Sub LoadCumlativeAccounts()
        'Search Date
        Dim SearchDate As String = ""
        SearchDate = DateTime.Now.ToString("yyyy/MM/dd") 'SearchDate.ToString("yyyy/MM/dd") 'DateTime.ParseExact(SearchDate, "yyyy/MM/dd", Nothing).ToString("yyyy/MM/dd")
        'Check Money Agreegation Count
        Dim _MoneyAggregationModel As MoneyAggregationModel = New MoneyAggregationModel()
        Dim _MoneyAggregationControl As MoneyAggregationControl = New MoneyAggregationControl()
        _MoneyAggregationModel.InvoiceDate = SearchDate
        _MoneyAggregationModel.Location = DropListLocation.SelectedItem.Value
        ' _MoneyAggregationModel.PaymentKind = 1

        Dim MoneyAggregationCheck As String = _MoneyAggregationControl.SelectMoneyAggregationCheck(_MoneyAggregationModel)
        'To Aggregate process
        Dim lstMoneyAggregationModel As List(Of MoneyAggregationModel) = New List(Of MoneyAggregationModel)()
        lstMoneyAggregationModel = _MoneyAggregationControl.SelectMoneyAggregation(_MoneyAggregationModel)
        If lstMoneyAggregationModel IsNot Nothing AndAlso lstMoneyAggregationModel.Count <> 0 Then
            '#Sales =>sales(cash) + sales(card)+IW+other+GST 
            If String.IsNullOrEmpty(lstMoneyAggregationModel(0).Sales) Then
                lstMoneyAggregationModel(0).Sales = 0.00
            End If
            'Need to Add Sales + Customer Deposit from other Table, So Added end of the coding
            '''''''''''lblSales.Text = lstMoneyAggregationModel(0).Sales.ToString()
            '#Discount Number 
            '#Discount Amount
            If String.IsNullOrEmpty(lstMoneyAggregationModel(0).DiscountAmt) Then
                lstMoneyAggregationModel(0).DiscountAmt = 0.00
                lstMoneyAggregationModel(0).DiscountCnt = 0
            End If
            ' lblDisAmount.Text = CommonControl.setINR(lstMoneyAggregationModel(0).DiscountAmt.ToString()) 'CommonControl.setINR(lstMoneyAggregationModel(0).DiscountAmt.ToString()) & "INR"
            'lblDisNum.Text = lstMoneyAggregationModel(0).DiscountCnt.ToString

            'Inward Amount
            If String.IsNullOrEmpty(lstMoneyAggregationModel(0).InWardAmt) Then
                lstMoneyAggregationModel(0).InWardAmt = 0.00
                lstMoneyAggregationModel(0).InWardCnt = 0
            End If
            'lblIWSum.Text = CommonControl.setINR(lstMoneyAggregationModel(0).InWardAmt.ToString()) 'CommonControl.setINR(lstMoneyAggregationModel(0).DiscountAmt.ToString()) & "INR"
            'lblIWCnt.Text = lstMoneyAggregationModel(0).InWardCnt.ToString()

            'Outward Cash Amount
            If String.IsNullOrEmpty(lstMoneyAggregationModel(0).OutWardCashAmt) Then
                lstMoneyAggregationModel(0).OutWardCashAmt = 0.00
                lstMoneyAggregationModel(0).OutWardCashCnt = 0
            End If
            ' lblOOWCashSum.Text = CommonControl.setINR(lstMoneyAggregationModel(0).OutWardCashAmt.ToString())
            'lblOOWCashCnt.Text = lstMoneyAggregationModel(0).OutWardCashCnt.ToString()

            'Outward Card Amount
            If String.IsNullOrEmpty(lstMoneyAggregationModel(0).OutWardCardAmt) Then
                lstMoneyAggregationModel(0).OutWardCardAmt = 0.00
                lstMoneyAggregationModel(0).OutWardCardCnt = 0
            End If
            ' lblOOWCardSum.Text = CommonControl.setINR(lstMoneyAggregationModel(0).OutWardCardAmt.ToString())
            'lblOOWCardCnt.Text = lstMoneyAggregationModel(0).OutWardCardCnt.ToString()

            'Other Cash Amount
            If String.IsNullOrEmpty(lstMoneyAggregationModel(0).OtherCashAmt) Then
                lstMoneyAggregationModel(0).OtherCashAmt = 0.00
                lstMoneyAggregationModel(0).OtherCashCnt = 0
            End If
            'lblOtherCashSum.Text = CommonControl.setINR(lstMoneyAggregationModel(0).OtherCashAmt.ToString())
            'lblOtherCashCnt.Text = lstMoneyAggregationModel(0).OtherCashCnt.ToString()

            'Other Card Amount
            If String.IsNullOrEmpty(lstMoneyAggregationModel(0).OtherCardAmt) Then
                lstMoneyAggregationModel(0).OtherCardAmt = 0.00
                lstMoneyAggregationModel(0).OtherCardCnt = 0
            End If
            'lblOtherCardSum.Text = CommonControl.setINR(lstMoneyAggregationModel(0).OtherCardAmt.ToString())
            'lblOtherCardCnt.Text = lstMoneyAggregationModel(0).OtherCardCnt.ToString()

            'Daily Openening Cash / Advance for cash counter by GSS
            'Find Opening Balance in the counter of starting of the day. 
            '#Open Cash 
            _MoneyAggregationModel.SearchDate = SearchDate
            Dim dtOpeningBal As DataTable = _MoneyAggregationControl.SelectOpeningBalance(_MoneyAggregationModel)
            If (dtOpeningBal Is Nothing) Or (dtOpeningBal.Rows.Count = 0) Then
                lstMoneyAggregationModel(0).OpeningCash = 0.00
            Else
                If String.IsNullOrEmpty(dtOpeningBal.Rows(0)("total")) Then
                    lstMoneyAggregationModel(0).OpeningCash = 0.00
                Else
                    lstMoneyAggregationModel(0).OpeningCash = dtOpeningBal.Rows(0)("total")
                End If
            End If
            lblReserve.Text = CommonControl.setINR(lstMoneyAggregationModel(0).OpeningCash)
            'Find Customer Advance / Advance amount by Customer
            '#Customer Advance 
            '''''''Dim dtCustAdvance As DataTable = _MoneyAggregationControl.SelectCustomerAdvance(_MoneyAggregationModel)
            '''''''If dtCustAdvance.Rows(0)("count") = 0 Then ' if count Zero then Advance amount will be zero
            '''''''    lstMoneyAggregationModel(0).AdvanceAmt = 0.00
            '''''''    lstMoneyAggregationModel(0).AdvanceCnt = 0
            '''''''Else
            '''''''    lstMoneyAggregationModel(0).AdvanceAmt = dtCustAdvance.Rows(0)("AdvanceAmt")
            '''''''    lstMoneyAggregationModel(0).AdvanceCnt = dtCustAdvance.Rows(0)("count")
            '''''''End If
            'lblDepositCusto.Text = CommonControl.setINR(lstMoneyAggregationModel(0).AdvanceAmt)
            '#Number of Advance
            'lblNumCusto.Text = lstMoneyAggregationModel(0).AdvanceCnt ' No of Customer paid for advance

            'Actually Cash collected from the customer without discount
            '#Deposit
            If String.IsNullOrEmpty(lstMoneyAggregationModel(0).BankDeposit) Then
                lstMoneyAggregationModel(0).BankDeposit = 0.00
            End If
            lblDeposit.Text = CommonControl.setINR(lstMoneyAggregationModel(0).BankDeposit.ToString())

            'Inward Claim Amount 
            If String.IsNullOrEmpty(lstMoneyAggregationModel(0).InWardClaimAmt) Then
                lstMoneyAggregationModel(0).InWardClaimAmt = 0.00
            End If
            ' lblIWSumClaim.Text = CommonControl.setINR(lstMoneyAggregationModel(0).InWardClaimAmt.ToString())
            'Outward Cash Claim amount
            If String.IsNullOrEmpty(lstMoneyAggregationModel(0).OutWardCashClaimAmt) Then
                lstMoneyAggregationModel(0).OutWardCashClaimAmt = 0.00
            End If
            'lblOOWCashSumClaim.Text = CommonControl.setINR(lstMoneyAggregationModel(0).OutWardCashClaimAmt.ToString())
            'Outward Card Claim amount
            If String.IsNullOrEmpty(lstMoneyAggregationModel(0).OutWardCardClaimAmt) Then
                lstMoneyAggregationModel(0).OutWardCardClaimAmt = 0.00
            End If
            'lblOOWCardSumClaim.Text = CommonControl.setINR(lstMoneyAggregationModel(0).OutWardCardClaimAmt.ToString())
            'Other Cash Claim amount
            If String.IsNullOrEmpty(lstMoneyAggregationModel(0).OtherCashClaimAmt) Then
                lstMoneyAggregationModel(0).OtherCashClaimAmt = 0.00
            End If
            'lblOtherCashSumClaim.Text = CommonControl.setINR(lstMoneyAggregationModel(0).OtherCashClaimAmt.ToString())
            'Other Card Claim amount
            If String.IsNullOrEmpty(lstMoneyAggregationModel(0).OtherCardClaimAmt) Then
                lstMoneyAggregationModel(0).OtherCardClaimAmt = 0.00
            End If
            'lblOtherCardSumClaim.Text = CommonControl.setINR(lstMoneyAggregationModel(0).OtherCardClaimAmt.ToString())

        End If
        'Add Sales + Customer Deposit for the sales 
        lblSales.Text = CommonControl.setINR(lstMoneyAggregationModel(0).Sales)

        'Cash Total = Opening Cash in Hand + Cash collected either IW/OOW/OTHER 
        '#cash total =>open deposit + sales(cash)
        'Deduct
        'lblCashTotal.Text = CommonControl.setINR((lstMoneyAggregationModel(0).OpeningCash + lstMoneyAggregationModel(0).OutWardCashAmt + lstMoneyAggregationModel(0).OtherCashAmt) - (lstMoneyAggregationModel(0).DiscountAmtCash + lstMoneyAggregationModel(0).BankDeposit))
        lblOther.Text = CommonControl.setINR((lstMoneyAggregationModel(0).OpeningCash + lstMoneyAggregationModel(0).OutWardCashAmt + lstMoneyAggregationModel(0).OtherCashAmt) - (lstMoneyAggregationModel(0).DiscountAmtCash + lstMoneyAggregationModel(0).BankDeposit))
        If Not (ViewState("dtCashTrackCount") Is Nothing) Then
            Dim dtCashTrack As DataTable = ViewState("dtCashTrackCount")
            lblTotalCount.Text = "Total Count: " & dtCashTrack.Rows.Count
            Dim sum As Object = dtCashTrack.Compute("Sum(total_amount)", "total_amount IS NOT NULL")
            If sum IsNot DBNull.Value Then
                'Console.WriteLine(CDec(sum))
                lblInvoiceAmt.Text = "Amount: " & CommonControl.setINR(CDec(sum))
            Else
                'Console.WriteLine("Nothing to sum")
                lblInvoiceAmt.Text = "Amount: 0.00"
            End If
        End If
        'For updatepanel not updating, so need to do it here
        If gvCashTrack.Rows.Count = 0 Then
            lblTotalCount.Visible = False
            lblInvoiceAmt.Visible = False
            'lblOtherTitle.Visible = False
            'lblOther.Visible = False
            'lblBankDepositTitle.Visible = False
            'lblDeposit.Visible = False
            'lblTodayOpeningCashTitle.Visible = False
            'lblReserve.Visible = False
            'lblTotalSalesTitle.Visible = False
            'lblSales.Visible = False

        Else
            lblTotalCount.Visible = True
            lblInvoiceAmt.Visible = True
            lblOtherTitle.Visible = True
            lblOther.Visible = True
            lblBankDepositTitle.Visible = True
            lblDeposit.Visible = True
            lblTodayOpeningCashTitle.Visible = True
            lblReserve.Visible = True
            lblTotalSalesTitle.Visible = True
            lblSales.Visible = True
        End If
    End Sub

    Protected Sub grvCashTracking_PageIndexChanging(ByVal sender As Object, ByVal e As GridViewPageEventArgs)
        Log4NetControl.ComInfoLogWrite(Session("UserName"))
        gvCashTrack.PageIndex = e.NewPageIndex
        GridViewBind()
    End Sub
End Class