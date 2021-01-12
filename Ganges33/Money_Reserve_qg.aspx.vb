Imports System.IO
Imports System.Text
Imports System.Net.Mail
Imports System.Data.SqlClient
Imports System.Web.Configuration
Imports System.Globalization
Imports Ganges33.Ganges33.model
Imports Ganges33.Ganges33.logic

Public Class Money_Reserve_qg
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        '***セッションなしログインユーザの対応***
        Dim userid As String = Session("user_id")
        'セッション取得
        Dim shipCode As String = Session("ship_code")
        If userid = "" Then
            Call showMsg("The session has expired. Please login again.")
            Response.Redirect("Login.aspx")
            Exit Sub
        End If
        '初期表示
        If IsPostBack = False Then
            'Intialize dropdown
            IntiaizeMoneyStatus()
            'Load Default settings
            LoadDefault()
            Call showData()
            ''''''''''Call showdata2()
        End If

    End Sub
    '最新のレジ点検チェック状況を表示
    Protected Sub showData()
        'Define status objects
        Dim dtStatus As StatusModel = New StatusModel()
        Dim userid As String = Session("user_id")
        Dim shipCode As String = Session("ship_code")
        Dim username As String = Session("user_Name")
        Dim userLevel As String = Session("user_level")
        Dim adminFlg As Boolean = Session("admin_Flg")
        Dim ci As New System.Globalization.CultureInfo("en-US")
        Dim DateTimeNow As DateTime = DateTime.Now
        Dim dtNow As DateTime = DateTimeNow.AddMinutes(ConfigurationManager.AppSettings("TimeDiff"))
        'Check Money Agreegation Count
        Dim _MoneyReserveModel As MoneyReserveModel = New MoneyReserveModel()
        Dim _MoneyReserveControl As MoneyReserveControl = New MoneyReserveControl()
        _MoneyReserveModel.ShipCode = shipCode
        _MoneyReserveModel.TDateTime = dtNow.ToShortDateString
        _MoneyReserveModel.ShipCode = shipCode
        _MoneyReserveModel.UserId = userid
        _MoneyReserveModel.UserName = username
        _MoneyReserveModel.UserLevel = userLevel
        _MoneyReserveModel.AdminFlg = adminFlg
        Dim Surname As String = _MoneyReserveControl.SelectSurname(_MoneyReserveModel)
        _MoneyReserveModel.YouserName = Surname
        ''''''''''''''''''''''''''''
        'Open
        '''''''''''''''''''''''''''''''''
        _MoneyReserveModel.Status = CommonConst.MONEY_STATUS1 'Open
        Dim tblRecMONEY_STATUS1 As DataTable = _MoneyReserveControl.SelectStatusRec(_MoneyReserveModel)
        ''''''''''''If count 0 then opening the day operation 
        '''''''''''If Not (tblRecMONEY_STATUS1.Rows.Count = 0) Then
        '''''''''''    'Passs username parameter to find out count
        '''''''''''    _MoneyReserveModel.YouserName = Surname
        '''''''''''    tblRecMONEY_STATUS1 = _MoneyReserveControl.SelectStatusRec(_MoneyReserveModel)
        '''''''''''    dtStatus.MistakeCount = tblRecMONEY_STATUS1.Rows.Count
        '''''''''''End If
        '   Dim tblCntMONEY_STATUS1 As DataTable = _MoneyReserveControl.SelectStatusRec(_MoneyReserveModel)


        dtStatus.MistakeCount = tblRecMONEY_STATUS1.Rows.Count
        If (tblRecMONEY_STATUS1 Is Nothing) Or (tblRecMONEY_STATUS1.Rows.Count = 0) Then
            lblResult.Text = "Untreated"
        Else
            If (tblRecMONEY_STATUS1.Rows(0)("youser_name") <> "") Then
                lblName.Text = tblRecMONEY_STATUS1.Rows(0)("youser_name")

                Dim dtTime1 As New DateTime
                dtTime1 = tblRecMONEY_STATUS1.Rows(0)("datetime")
                lblTime.Text = dtTime1 '.ToString("tt", ci) & dtTime1.ToShortDateString
                lblRegiDeposi.Text = tblRecMONEY_STATUS1.Rows(0)("reserve") & " INR"
                'mistakeCount = tblRecMONEY_STATUS1.Rows.Count
                If dtStatus.MistakeCount >= 2 Then
                    lblmistake0.Visible = True
                    lblmistakeOpen.Text = dtStatus.MistakeCount - 1
                End If
                'Setting Difference
                'Open
                setResult(CommonConst.MONEY_STATUS1, tblRecMONEY_STATUS1.Rows(0)("diff"))
                If String.IsNullOrEmpty(tblRecMONEY_STATUS1.Rows(0)("conf_user").ToString()) Then
                    tblRecMONEY_STATUS1.Rows(0)("conf_user") = ""
                End If

                If tblRecMONEY_STATUS1.Rows(0)("conf_user") = "" Then
                    lblConfirm.Text = "Not Confirmation"
                    lblConfirm.ForeColor = System.Drawing.Color.FromArgb(255, 0, 0)
                Else

                    dtTime1 = tblRecMONEY_STATUS1.Rows(0)("conf_datetime")
                    lblConfirm2.Text = "Confirmation " & tblRecMONEY_STATUS1.Rows(0)("conf_user") & " " & dtTime1 '.ToString("tt", ci) & dtTime1.ToShortDateString
                    lblConfirm2.ForeColor = System.Drawing.Color.FromArgb(51, 204, 51)
                End If
                '' Call setDenomination(reserveData)
            Else
                lblResult.Text = "Untreated"
            End If
        End If
        ''''''''''''''''''''''''''''
        'Inspection1
        '''''''''''''''''''''''''''''''''
        _MoneyReserveModel.Status = CommonConst.MONEY_STATUS2 'Inspection1
        tblRecMONEY_STATUS1 = _MoneyReserveControl.SelectStatusRec(_MoneyReserveModel)
        Surname = _MoneyReserveControl.SelectSurname(_MoneyReserveModel)
        'If count 0 then opening the day operation 
        If Not (tblRecMONEY_STATUS1.Rows.Count = 0) Then
            'Passs username parameter to find out count
            _MoneyReserveModel.YouserName = Surname
            tblRecMONEY_STATUS1 = _MoneyReserveControl.SelectStatusRec(_MoneyReserveModel)
            dtStatus.MistakeCount = tblRecMONEY_STATUS1.Rows.Count
        End If
        '   Dim tblCntMONEY_STATUS1 As DataTable = _MoneyReserveControl.SelectStatusRec(_MoneyReserveModel)
        If (tblRecMONEY_STATUS1 Is Nothing) Or (tblRecMONEY_STATUS1.Rows.Count = 0) Then
            lblResultIns1.Text = "Untreated"
        Else
            If (tblRecMONEY_STATUS1.Rows(0)("youser_name") <> "") Then
                lblDiff0.Text = tblRecMONEY_STATUS1.Rows(0)("diff") & " INR"
                lblName0.Text = tblRecMONEY_STATUS1.Rows(0)("youser_name")

                Dim dtTime1 As New DateTime
                dtTime1 = tblRecMONEY_STATUS1.Rows(0)("datetime")
                lblTime0.Text = dtTime1 '.ToString("tt", ci) & dtTime1.ToShortDateString
                lblRegiDeposi.Text = tblRecMONEY_STATUS1.Rows(0)("reserve") & " INR"
                'mistakeCount = tblRecMONEY_STATUS1.Rows.Count
                If dtStatus.MistakeCount >= 2 Then
                    lblmistake1.Visible = True
                    lblmistakeIns1.Text = dtStatus.MistakeCount - 1
                End If

                'Setting Difference
                'Inspection1
                setResult(CommonConst.MONEY_STATUS2, tblRecMONEY_STATUS1.Rows(0)("diff"))

                If String.IsNullOrEmpty(tblRecMONEY_STATUS1.Rows(0)("conf_user").ToString()) Then
                    tblRecMONEY_STATUS1.Rows(0)("conf_user") = ""
                End If

                If tblRecMONEY_STATUS1.Rows(0)("conf_user") = "" Then
                    lblCIns1.Text = "Not Confirmation"
                    lblCIns1.ForeColor = System.Drawing.Color.FromArgb(255, 0, 0)
                Else

                    dtTime1 = tblRecMONEY_STATUS1.Rows(0)("conf_datetime")
                    lblCIns1_.Text = "Confirmation " & tblRecMONEY_STATUS1.Rows(0)("conf_user") & " " & dtTime1 '.ToString("tt", ci) & dtTime1.ToShortDateString
                    lblCIns1_.ForeColor = System.Drawing.Color.FromArgb(51, 204, 51)
                End If
                '' Call setDenomination(reserveData)
            Else
                lblResultIns1.Text = "Untreated"
            End If
        End If
        ''''''''''''''''''''''''''''
        'Inspection2
        '''''''''''''''''''''''''''''''''
        _MoneyReserveModel.Status = CommonConst.MONEY_STATUS3 'Inspection2
        tblRecMONEY_STATUS1 = _MoneyReserveControl.SelectStatusRec(_MoneyReserveModel)
        Surname = _MoneyReserveControl.SelectSurname(_MoneyReserveModel)
        'If count 0 then opening the day operation 
        If Not (tblRecMONEY_STATUS1.Rows.Count = 0) Then
            'Passs username parameter to find out count
            _MoneyReserveModel.YouserName = Surname
            tblRecMONEY_STATUS1 = _MoneyReserveControl.SelectStatusRec(_MoneyReserveModel)
            dtStatus.MistakeCount = tblRecMONEY_STATUS1.Rows.Count
        End If
        '   Dim tblCntMONEY_STATUS1 As DataTable = _MoneyReserveControl.SelectStatusRec(_MoneyReserveModel)
        If (tblRecMONEY_STATUS1 Is Nothing) Or (tblRecMONEY_STATUS1.Rows.Count = 0) Then
            lblResultIns2.Text = "Untreated"
        Else
            If (tblRecMONEY_STATUS1.Rows(0)("youser_name") <> "") Then
                lblDiff1.Text = tblRecMONEY_STATUS1.Rows(0)("diff") & " INR"
                lblName1.Text = tblRecMONEY_STATUS1.Rows(0)("youser_name")

                Dim dtTime1 As New DateTime
                dtTime1 = tblRecMONEY_STATUS1.Rows(0)("datetime")
                lblTime1.Text = dtTime1 '.ToString("tt", ci) & dtTime1.ToShortDateString
                ''''''''''''''''''''''''''''''''''''' lblRegiDeposi.Text = tblRecMONEY_STATUS1.Rows(0)("reserve") & "INR"
                'mistakeCount = tblRecMONEY_STATUS1.Rows.Count
                If dtStatus.MistakeCount >= 2 Then
                    lblmistake2.Visible = True
                    lblmistakeIns2.Text = dtStatus.MistakeCount - 1
                End If
                'Setting Difference
                'Inspection2
                setResult(CommonConst.MONEY_STATUS3, tblRecMONEY_STATUS1.Rows(0)("diff"))
                If String.IsNullOrEmpty(tblRecMONEY_STATUS1.Rows(0)("conf_user").ToString()) Then
                    tblRecMONEY_STATUS1.Rows(0)("conf_user") = ""
                End If

                If tblRecMONEY_STATUS1.Rows(0)("conf_user") = "" Then
                    lblCIns2.Text = "Not Confirmation"
                    lblCIns2.ForeColor = System.Drawing.Color.FromArgb(255, 0, 0)
                Else

                    dtTime1 = tblRecMONEY_STATUS1.Rows(0)("conf_datetime")
                    lblCIns2_.Text = "Confirmation " & tblRecMONEY_STATUS1.Rows(0)("conf_user") & " " & dtTime1 '.ToString("tt", ci) & dtTime1.ToShortDateString
                    lblCIns2_.ForeColor = System.Drawing.Color.FromArgb(51, 204, 51)
                End If
                '' Call setDenomination(reserveData)
            Else
                lblResultIns2.Text = "Untreated"
            End If
        End If
        ''''''''''''''''''''''''''''
        'Inspection3
        '''''''''''''''''''''''''''''''''
        _MoneyReserveModel.Status = CommonConst.MONEY_STATUS4 'Inspection3
        tblRecMONEY_STATUS1 = _MoneyReserveControl.SelectStatusRec(_MoneyReserveModel)
        Surname = _MoneyReserveControl.SelectSurname(_MoneyReserveModel)
        'If count 0 then opening the day operation 
        If Not (tblRecMONEY_STATUS1.Rows.Count = 0) Then
            'Passs username parameter to find out count
            _MoneyReserveModel.YouserName = Surname
            tblRecMONEY_STATUS1 = _MoneyReserveControl.SelectStatusRec(_MoneyReserveModel)
            dtStatus.MistakeCount = tblRecMONEY_STATUS1.Rows.Count
        End If
        '   Dim tblCntMONEY_STATUS1 As DataTable = _MoneyReserveControl.SelectStatusRec(_MoneyReserveModel)
        If (tblRecMONEY_STATUS1 Is Nothing) Or (tblRecMONEY_STATUS1.Rows.Count = 0) Then
            lblResultIns3.Text = "Untreated"
        Else
            If (tblRecMONEY_STATUS1.Rows(0)("youser_name") <> "") Then
                lblDiff2.Text = tblRecMONEY_STATUS1.Rows(0)("diff") & " INR"
                lblName2.Text = tblRecMONEY_STATUS1.Rows(0)("youser_name")

                Dim dtTime1 As New DateTime
                dtTime1 = tblRecMONEY_STATUS1.Rows(0)("datetime")
                lblTime2.Text = dtTime1 '.ToString("tt", ci) & dtTime1.ToShortDateString
                ''''''''''''''''''''''''''''''''''''' lblRegiDeposi.Text = tblRecMONEY_STATUS1.Rows(0)("reserve") & "INR"
                'mistakeCount = tblRecMONEY_STATUS1.Rows.Count
                If dtStatus.MistakeCount >= 2 Then
                    lblmistake3.Visible = True
                    lblmistakeIns3.Text = dtStatus.MistakeCount - 1
                End If
                'Setting Difference
                'Inspection3
                setResult(CommonConst.MONEY_STATUS4, tblRecMONEY_STATUS1.Rows(0)("diff"))
                If String.IsNullOrEmpty(tblRecMONEY_STATUS1.Rows(0)("conf_user").ToString()) Then
                    tblRecMONEY_STATUS1.Rows(0)("conf_user") = ""
                End If

                If tblRecMONEY_STATUS1.Rows(0)("conf_user") = "" Then
                    lblCIns3.Text = "Not Confirmation"
                    lblCIns3.ForeColor = System.Drawing.Color.FromArgb(255, 0, 0)
                Else
                    dtTime1 = tblRecMONEY_STATUS1.Rows(0)("conf_datetime")
                    lblCIns3_.Text = "Confirmation " & tblRecMONEY_STATUS1.Rows(0)("conf_user") & " " & dtTime1 '.ToString("tt", ci) & dtTime1.ToShortDateString
                    lblCIns3_.ForeColor = System.Drawing.Color.FromArgb(51, 204, 51)
                End If
                '' Call setDenomination(reserveData)
            Else
                lblResultIns3.Text = "Untreated"
            End If
        End If
        ''''''''''''''''''''''''''''
        'Close
        '''''''''''''''''''''''''''''''''
        _MoneyReserveModel.Status = CommonConst.MONEY_STATUS5 'Close
        tblRecMONEY_STATUS1 = _MoneyReserveControl.SelectStatusRec(_MoneyReserveModel)
        Surname = _MoneyReserveControl.SelectSurname(_MoneyReserveModel)

        'If count 0 then opening the day operation 
        If Not (tblRecMONEY_STATUS1.Rows.Count = 0) Then
            'Passs username parameter to find out count
            _MoneyReserveModel.YouserName = Surname
            tblRecMONEY_STATUS1 = _MoneyReserveControl.SelectStatusRec(_MoneyReserveModel)
            dtStatus.MistakeCount = tblRecMONEY_STATUS1.Rows.Count
        End If
        If (tblRecMONEY_STATUS1 Is Nothing) Or (tblRecMONEY_STATUS1.Rows.Count = 0) Then
            lblResultClose.Text = "Untreated"
        Else
            If (tblRecMONEY_STATUS1.Rows(0)("youser_name") <> "") Then
                lblDiff3.Text = tblRecMONEY_STATUS1.Rows(0)("diff") & " INR"
                lblName3.Text = tblRecMONEY_STATUS1.Rows(0)("youser_name")

                Dim dtTime1 As New DateTime
                dtTime1 = tblRecMONEY_STATUS1.Rows(0)("datetime")
                lblTime3.Text = dtTime1 '.ToString("tt", ci) & dtTime1.ToShortDateString
                ''''''''''''''''''''''''''''''''''''' lblRegiDeposi.Text = tblRecMONEY_STATUS1.Rows(0)("reserve") & "INR"
                'mistakeCount = tblRecMONEY_STATUS1.Rows.Count
                If dtStatus.MistakeCount >= 2 Then
                    lblmistake4.Visible = True
                    lblmistakeClose.Text = dtStatus.MistakeCount - 1
                End If

                'Setting Difference
                'Inspection3
                setResult(CommonConst.MONEY_STATUS5, tblRecMONEY_STATUS1.Rows(0)("diff"))

                If String.IsNullOrEmpty(tblRecMONEY_STATUS1.Rows(0)("conf_user").ToString()) Then
                    tblRecMONEY_STATUS1.Rows(0)("conf_user") = ""
                End If

                If tblRecMONEY_STATUS1.Rows(0)("conf_user") = "" Then
                    lblConfirmOut.Text = "Not Confirmation"
                    lblConfirmOut.ForeColor = System.Drawing.Color.FromArgb(255, 0, 0)
                Else
                    dtTime1 = tblRecMONEY_STATUS1.Rows(0)("conf_datetime")
                    lblConfirmOut2.Text = "Confirmation " & tblRecMONEY_STATUS1.Rows(0)("conf_user") & " " & dtTime1 '.ToString("tt", ci) & dtTime1.ToShortDateString
                    lblConfirmOut2.ForeColor = System.Drawing.Color.FromArgb(51, 204, 51)
                End If
                '' Call setDenomination(reserveData)
            Else
                lblResultClose.Text = "Untreated"
            End If
        End If
    End Sub
    Protected Sub btnSend_Click(sender As Object, e As ImageClickEventArgs) Handles btnSend.Click
        Dim shipCode As String = Session("ship_code")
        'Status - Selected from the dropdown list
        If DropListStatus.Text = "Select processing" Then
            Call showMsg("Select the transaction status from the list")
            Exit Sub
        End If
        'Empty not allowed 
        'Get Branch Type 
        Dim _MoneyReserveModel As MoneyReserveModel = New MoneyReserveModel()
        Dim _MoneyReserveControl As MoneyReserveControl = New MoneyReserveControl()
        Dim tblT As DataTable = _MoneyReserveControl.SelectMShipBase(shipCode)
        If tblT.Rows.Count > 0 Then
            If tblT.Rows(0)("ship_service") IsNot DBNull.Value Then
                _MoneyReserveModel.BRType = tblT.Rows(0)("ship_service")
            End If
        End If
        If Not (_MoneyReserveModel.BRType = "DR") Then 'Not neccessary for Data Recovery
            If (TextM2000Cnt.Text = "") And
                (TextM500Cnt.Text = "") And
                (TextM200Cnt.Text = "") And
                (TextM100Cnt.Text = "") And
                (TextM50Cnt.Text = "") And
                (TextM20Cnt.Text = "") And
                (TextM10Cnt.Text = "") And
                 (TextCoin10Cnt.Text = "") And
               (TextCoin5Cnt.Text = "") And
                  (TextCoin2Cnt.Text = "") And
                (TextCoin1Cnt.Text = "") Then
                Call showMsg("Please enter atleast one cash count")
                Exit Sub
            End If
        End If
        'Numeric Validation Check 
        If (_MoneyReserveModel.BRType = "DR") Then 'For Data Recovery
            If Not (CommonControl.isNumberDR(TextM2000Cnt.Text) And
                CommonControl.isNumberDR(TextM500Cnt.Text) And
                CommonControl.isNumberDR(TextM200Cnt.Text) And
                 CommonControl.isNumberDR(TextM100Cnt.Text) And
                  CommonControl.isNumberDR(TextM50Cnt.Text) And
                   CommonControl.isNumberDR(TextM20Cnt.Text) And
                    CommonControl.isNumberDR(TextM10Cnt.Text) And
                    CommonControl.isNumberDR(TextCoin10Cnt.Text) And
                     CommonControl.isNumberDR(TextCoin5Cnt.Text) And
                      CommonControl.isNumberDR(TextCoin2Cnt.Text) And
                        CommonControl.isNumberDR(TextCoin1Cnt.Text)) Then
                Call showMsg("The Cash count Is invalid...")
                Exit Sub
            End If
        Else 'For Service Center
            If Not (CommonControl.isNumber(TextM2000Cnt.Text) And
                 CommonControl.isNumber(TextM500Cnt.Text) And
                 CommonControl.isNumber(TextM200Cnt.Text) And
                  CommonControl.isNumber(TextM100Cnt.Text) And
                   CommonControl.isNumber(TextM50Cnt.Text) And
                    CommonControl.isNumber(TextM20Cnt.Text) And
                     CommonControl.isNumber(TextM10Cnt.Text) And
                     CommonControl.isNumber(TextCoin10Cnt.Text) And
                      CommonControl.isNumber(TextCoin5Cnt.Text) And
                       CommonControl.isNumber(TextCoin2Cnt.Text) And
                         CommonControl.isNumber(TextCoin1Cnt.Text)) Then
                Call showMsg("The Cash count Is invalid...")
                Exit Sub
            End If
        End If

        '
        Dim userid As String = Session("user_id")
        Dim username As String = Session("user_Name")
        Dim userLevel As String = Session("user_level")
        Dim adminFlg As Boolean = Session("admin_Flg")
        Dim ci As New System.Globalization.CultureInfo("en-US")
        Dim DateTimeNow As DateTime = DateTime.Now
        Dim dtNow As DateTime = DateTimeNow.AddMinutes(ConfigurationManager.AppSettings("TimeDiff"))
        'Define status objects
        Dim dtStatus As StatusModel = New StatusModel()
        'Check Money Agreegation Count

        _MoneyReserveModel.ShipCode = shipCode
        _MoneyReserveModel.TDateTime = dtNow.ToShortDateString
        _MoneyReserveModel.ShipCode = shipCode
        _MoneyReserveModel.UserId = userid
        _MoneyReserveModel.UserName = username
        _MoneyReserveModel.UserLevel = userLevel
        _MoneyReserveModel.AdminFlg = adminFlg
        _MoneyReserveModel.PreviousDateClosing = False
        '''''''''''''''''''''''''''''''''''''''''''''''''
        'Check Business Rule (BR) to proceed further
        ''''''''''''''''''''''''''''''''''''''''''''''''
        'Dropdown status must be valid
        ''''''''''''''''''''''''''''''''''''''''''''''''''
        '''
        '1st Open - then check before it was closed
        '2nd Inspection1 - it must be open
        '3rd Inspection2 - it must be open and Inspection1
        '4th Inspection3 - it must be open, Inspection1, Inspection2 
        '5th Close - before it must be opened 
        '****1st Check Current / Today Transaction exist in db
        Dim tblBR As DataTable = _MoneyReserveControl.SelectTReserveBR(_MoneyReserveModel)
        Dim brOpen As Int16 = 0, brInspection1 As Int16 = 0, brInspection2 As Int16 = 0, brInspection3 As Int16 = 0, brClose As Int16 = 0
        For Each row As DataRow In tblBR.Rows
            If row("status") = CommonConst.MONEY_STATUS1 Then
                'If selected same transaction again then exist
                If DropListStatus.Text = CommonConst.MONEY_STATUS1 Then
                    Call showMsg("The Openening cash already done, Cann't do again...")
                    Exit Sub
                End If
                brOpen = 1
            ElseIf row("status") = CommonConst.MONEY_STATUS2 Then
                'If selected same transaction again then exist
                If DropListStatus.Text = CommonConst.MONEY_STATUS2 Then
                    Call showMsg("The Inspection1 cash already done, Cann't do again...")
                    Exit Sub
                End If
                brInspection1 = 1
            ElseIf row("status") = CommonConst.MONEY_STATUS3 Then
                'If selected same transaction again then exist
                If DropListStatus.Text = CommonConst.MONEY_STATUS3 Then
                    Call showMsg("The Inspection2 cash already done, Cann't do again...")
                    Exit Sub
                End If
                brInspection2 = 1
            ElseIf row("status") = CommonConst.MONEY_STATUS4 Then
                'If selected same transaction again then exist
                If DropListStatus.Text = CommonConst.MONEY_STATUS4 Then
                    Call showMsg("The Inspection3 cash already done, Cann't do again...")
                    Exit Sub
                End If
                brInspection3 = 1
            ElseIf row("status") = CommonConst.MONEY_STATUS5 Then
                'If selected same transaction again then exist
                If DropListStatus.Text = CommonConst.MONEY_STATUS5 Then
                    Call showMsg("The Close cash already done, Cann't do again...")
                    Exit Sub
                End If
                brClose = 1
            End If
        Next
        '******2nd Check Last transaction of branch - Open / Close time & Open / Close Date
        'Getting Branch Master Day Open and Close from M_ship_base
        Dim DayStart As String = "", DayEnd As String = ""
        Dim RegiDeposit As String = "", OpenTime As String = "False"
        tblT = _MoneyReserveControl.SelectMShipBase(shipCode)
        Dim ClosingDate As DateTime = DateTime.Now.Date
        Dim CurrentDate As DateTime = DateTime.Now.Date
        Dim OpeningDate As DateTime = DateTime.Now.Date
        If tblT.Rows.Count > 0 Then
            If tblT.Rows(0)("open_start") IsNot DBNull.Value Then
                DayStart = tblT.Rows(0)("open_start")
            Else
                DayStart = "00:00"
            End If

            If tblT.Rows(0)("open_end") IsNot DBNull.Value Then
                DayEnd = tblT.Rows(0)("open_end")
            Else
                DayEnd = "24:00"
            End If
            If tblT.Rows(0)("regi_deposit") IsNot DBNull.Value Then
                RegiDeposit = tblT.Rows(0)("regi_deposit")
            End If
            If tblT.Rows(0)("closing_date") IsNot DBNull.Value Then
                ClosingDate = tblT.Rows(0)("closing_date")
            Else
                ClosingDate = CurrentDate
            End If
            If tblT.Rows(0)("opening_date") IsNot DBNull.Value Then
                OpeningDate = tblT.Rows(0)("opening_date")
            End If
            If tblT.Rows(0)("open_time") IsNot DBNull.Value Then
                OpenTime = tblT.Rows(0)("open_time")
            Else
                OpenTime = "False"
            End If
        End If
        'Before Date is closed or not, need to check 1..4 Status, MONEY_STATUS5 allowed hence need to close the previous working days
        If DropListStatus.Text <> CommonConst.MONEY_STATUS5 Then
            If (OpenTime.Trim = "True") And (OpeningDate <> dtNow.ToShortDateString) Then
                Call showMsg("The Store already Open, Need to close the before day (" & OpeningDate & ") transaction ...")
                Exit Sub
            End If
        End If

        'Priority Check
        Select Case DropListStatus.Text
            Case CommonConst.MONEY_STATUS1
                'This scenario not reach always. hence already checked
                If Not ((brOpen = 0) And (brInspection1 = 0) And (brInspection2 = 0) And (brInspection3 = 0) And (brClose = 0)) Then
                    Call showMsg("The Open transaction Cann't do again...")
                    Exit Sub
                End If

            Case CommonConst.MONEY_STATUS2

                'Priority Check for inspection1
                If Not ((brOpen = 1) And (brInspection1 = 0) And (brInspection2 = 0) And (brInspection3 = 0) And (brClose = 0)) Then
                    Call showMsg("The inspection1 transaction Cann't do...")
                    Exit Sub
                End If
            Case CommonConst.MONEY_STATUS3
                'Priority Check for inspection2
                If Not ((brOpen = 1) And (brInspection1 = 1) And (brInspection2 = 0) And (brInspection3 = 0) And (brClose = 0)) Then
                    Call showMsg("The inspection2 transaction Cann't do...")
                    Exit Sub
                End If
            Case CommonConst.MONEY_STATUS4
                'Priority Check for inspection3
                'If ((blOpen = True) And (blInspection1 = True) And (blInspection2 = True)) And ((blInspection3 = False) And (blClose = False)) Then
                If Not ((brOpen = 1) And (brInspection1 = 1) And (brInspection2 = 1) And (brInspection3 = 0) And (brClose = 0)) Then
                    Call showMsg("The inspection3 transaction Cann't do...")
                    Exit Sub
                End If

            Case CommonConst.MONEY_STATUS5
                'Priority Check for close
                If Not (OpenTime.Trim = "True") Then  'If not Previous Day Closing
                    If Not (brOpen = 1) Then
                        Call showMsg("The close transaction Cann't do...")
                        Exit Sub
                    End If
                Else
                    'If prevous working day not closed then next day morning can close
                    _MoneyReserveModel.PreviousDateClosing = True
                    _MoneyReserveModel.OpenDate = OpeningDate
                End If
        End Select
        'Surname
        Dim userNameConfirm As String = _MoneyReserveControl.SelectSurname(_MoneyReserveModel)
        _MoneyReserveModel.YouserName = userNameConfirm
        ''''''''''''''''''''''''''''
        'Open
        '''''''''''''''''''''''''''''''
        If DropListStatus.Text = CommonConst.MONEY_STATUS1 Then
            _MoneyReserveModel.Status = CommonConst.MONEY_STATUS1
            'Add there is no transaction exist
            'Time comparison

            If dtNow.ToString("HH:mm") >= DayStart And dtNow.ToString("HH:mm") <= DayEnd Then
                dtStatus = InsertTReserve(_MoneyReserveModel) 'Insert Transaction 
                If dtStatus.Status = 2 Then 'Transaction status 
                    Call showMsg(dtStatus.Message)
                    Exit Sub
                End If
                Call setResult(CommonConst.MONEY_STATUS1, dtStatus.Diff)
                ''''''''''''''''''''''''''lblDiff0.Text = dtStatus.Diff & "INR"
                'ログインユーザ名セット
                lblName.Text = username
                '時間のセット
                lblTime.Text = dtNow
                'mistakecount
                If dtStatus.MistakeCount >= 1 Then
                    lblmistake0.Visible = True
                    lblmistakeOpen.Text = dtStatus.MistakeCount
                End If
                '確認者チェック
                lblConfirm.Text = "Not Confirmation"
                lblConfirm.ForeColor = System.Drawing.Color.FromArgb(255, 0, 0)
                'Display to User 
                lblLastupdate.Text = "Last Updated: " & dtStatus.LastUpdated
                lblIP.Text = "IP: " & dtStatus.IpAddress

            Else
                Call showMsg("Now is not the time the first checkout of the day to be done.")
                Exit Sub
            End If
            ''''''''''''''''''''''''''''
            'Inspection1
            '''''''''''''''''''''''''''''''
        ElseIf DropListStatus.Text = CommonConst.MONEY_STATUS2 Then
            _MoneyReserveModel.Status = CommonConst.MONEY_STATUS2
            'Add there is no transaction exist
            'Time comparison

            If dtNow.ToString("HH:mm") >= DayStart And dtNow.ToString("HH:mm") <= DayEnd Then
                '''''''''''''''''
                'Insert Transaction 
                dtStatus = InsertTReserve(_MoneyReserveModel)
                If dtStatus.Status = 2 Then
                    Call showMsg(dtStatus.Message)
                    Exit Sub
                End If
                Call setResult(CommonConst.MONEY_STATUS2, dtStatus.Diff)
                lblDiff0.Text = dtStatus.Diff & " INR"
                'ログインユーザ名セット
                lblName0.Text = username
                '時間のセット
                lblTime0.Text = dtNow
                'mistakecount
                If dtStatus.MistakeCount >= 1 Then
                    lblmistake1.Visible = True
                    lblmistakeIns1.Text = dtStatus.MistakeCount
                End If
                '確認者チェック
                lblCIns1.Text = "Not Confirmation"
                lblCIns1.ForeColor = System.Drawing.Color.FromArgb(255, 0, 0)
                lblCIns1_.Text = ""
                'Display to User 
                lblLastupdate.Text = "Last Updated: " & dtStatus.LastUpdated
                lblIP.Text = "IP: " & dtStatus.IpAddress
            Else
                Call showMsg("Now is not the time the first checkout of the day to be done.")
                Exit Sub
            End If
            ''''''''''''''''''''''
            'Inspection2
            ''''''''''''''''''''''
        ElseIf DropListStatus.Text = CommonConst.MONEY_STATUS3 Then
            _MoneyReserveModel.Status = CommonConst.MONEY_STATUS3
            'Add there is no transaction exist
            'Time comparison

            If dtNow.ToString("HH:mm") >= DayStart And dtNow.ToString("HH:mm") <= DayEnd Then
                '''''''''''''''''
                'Insert Transaction 
                dtStatus = InsertTReserve(_MoneyReserveModel)
                If dtStatus.Status = 2 Then
                    Call showMsg(dtStatus.Message)
                    Exit Sub
                End If
                Call setResult(CommonConst.MONEY_STATUS3, dtStatus.Diff)
                lblDiff1.Text = dtStatus.Diff & " INR"
                'ログインユーザ名セット
                lblName1.Text = username
                '時間のセット
                lblTime1.Text = dtNow
                'mistakecount
                If dtStatus.MistakeCount >= 1 Then
                    lblmistake2.Visible = True
                    lblmistakeIns2.Text = dtStatus.MistakeCount
                End If
                '確認者チェック
                lblCIns2.Text = "Not Confirmation"
                lblCIns2.ForeColor = System.Drawing.Color.FromArgb(255, 0, 0)
                lblCIns2_.Text = ""
                'Display to User 
                lblLastupdate.Text = "Last Updated: " & dtStatus.LastUpdated
                lblIP.Text = "IP: " & dtStatus.IpAddress
            Else
                Call showMsg("Now is not the time the first checkout of the day to be done.")
                Exit Sub
            End If
            ''''''''''''''''''''''
            'Inspection3
            ''''''''''''''''''''''
        ElseIf DropListStatus.Text = CommonConst.MONEY_STATUS4 Then
            _MoneyReserveModel.Status = CommonConst.MONEY_STATUS4
            'Add there is no transaction exist
            'Time comparison

            If dtNow.ToString("HH:mm") >= DayStart And dtNow.ToString("HH:mm") <= DayEnd Then
                '''''''''''''''''
                'Insert Transaction 
                dtStatus = InsertTReserve(_MoneyReserveModel)
                If dtStatus.Status = 2 Then
                    Call showMsg(dtStatus.Message)
                    Exit Sub
                End If
                Call setResult(CommonConst.MONEY_STATUS4, dtStatus.Diff)
                lblDiff2.Text = dtStatus.Diff & " INR"
                'ログインユーザ名セット
                lblName2.Text = username
                '時間のセット
                lblTime2.Text = dtNow
                'mistakecount
                If dtStatus.MistakeCount >= 1 Then
                    lblmistake3.Visible = True
                    lblmistakeIns3.Text = dtStatus.MistakeCount
                End If
                '確認者チェック
                lblCIns3.Text = "Not Confirmation"
                lblCIns3.ForeColor = System.Drawing.Color.FromArgb(255, 0, 0)
                lblCIns3_.Text = ""
                'Display to User 
                lblLastupdate.Text = "Last Updated: " & dtStatus.LastUpdated
                lblIP.Text = "IP: " & dtStatus.IpAddress
            Else
                Call showMsg("Now is not the time the first checkout of the day to be done.")
                Exit Sub
            End If
            ''''''''''''''''''''''
            'Close
            ''''''''''''''''''''''
        ElseIf DropListStatus.Text = CommonConst.MONEY_STATUS5 Then
            _MoneyReserveModel.Status = CommonConst.MONEY_STATUS5
            'Add there is no transaction exist
            'Time comparison

            If dtNow.ToString("HH:mm") >= DayStart And dtNow.ToString("HH:mm") <= DayEnd Then
                '''''''''''''''''
                'Insert Transaction 
                dtStatus = InsertTReserve(_MoneyReserveModel)
                If dtStatus.Status = 2 Then
                    Call showMsg(dtStatus.Message)
                    Exit Sub
                End If
                Call setResult(CommonConst.MONEY_STATUS5, dtStatus.Diff)
                lblDiff3.Text = dtStatus.Diff & " INR"
                'ログインユーザ名セット
                lblName3.Text = username
                '時間のセット
                lblTime3.Text = dtNow
                'mistakecount
                If dtStatus.MistakeCount >= 1 Then
                    lblmistake4.Visible = True
                    lblmistakeClose.Text = dtStatus.MistakeCount
                End If
                '確認者チェック
                lblConfirmOut.Text = "Not Confirmation"
                lblConfirmOut.ForeColor = System.Drawing.Color.FromArgb(255, 0, 0)
                lblConfirmOut2.Text = ""
                'Display to User 
                lblLastupdate.Text = "Last Updated: " & dtStatus.LastUpdated
                lblIP.Text = "IP: " & dtStatus.IpAddress
            Else
                Call showMsg("Now is not the time the first checkout of the day to be done.")
                Exit Sub
            End If
        End If

        '***コントロール制御***
        '登録後リスト選択不可
        DropListStatus.Enabled = False
        '確認者の登録OK
        btnSend2.Enabled = True
        txtConfUser.Enabled = True
        txtConfPwd.Enabled = True
    End Sub


    Private Function InsertTReserve(ByVal queryParams As MoneyReserveModel) As StatusModel
        'Define status objects
        Dim dtStatus As StatusModel = New StatusModel()

        '***共通処理***
        'Dim diff As String
        ' Dim regiDeposit As String
        Dim clsSet As New Class_money
        Dim clsSetCommon As New Class_common
        Dim ci As New System.Globalization.CultureInfo("en-US")
        Dim DateTimeNow As DateTime = DateTime.Now
        Dim dtNow As DateTime = DateTimeNow.AddMinutes(ConfigurationManager.AppSettings("TimeDiff"))
        ' Dim dtNow As DateTime = clsSetCommon.dtIndia
        Dim i2000Cnt As Integer
        Dim i500Cnt As Integer
        Dim i200Cnt As Integer
        Dim i100Cnt As Integer
        Dim i50Cnt As Integer
        Dim i20Cnt As Integer
        Dim i10Cnt As Integer
        Dim i10CoinCnt As Integer
        Dim i5CoinCnt As Integer
        Dim i2CoinCnt As Integer
        Dim i1CoinCnt As Integer
        '数値チェックと設定
        If TextM2000Cnt.Text = "" Then i2000Cnt = 0 Else i2000Cnt = Convert.ToInt32(TextM2000Cnt.Text)
        If TextM500Cnt.Text = "" Then i500Cnt = 0 Else i500Cnt = Convert.ToInt32(TextM500Cnt.Text)
        If TextM200Cnt.Text = "" Then i200Cnt = 0 Else i200Cnt = Convert.ToInt32(TextM200Cnt.Text)
        If TextM100Cnt.Text = "" Then i100Cnt = 0 Else i100Cnt = Convert.ToInt32(TextM100Cnt.Text)
        If TextM50Cnt.Text = "" Then i50Cnt = 0 Else i50Cnt = Convert.ToInt32(TextM50Cnt.Text)
        If TextM20Cnt.Text = "" Then i20Cnt = 0 Else i20Cnt = Convert.ToInt32(TextM20Cnt.Text)
        If TextM10Cnt.Text = "" Then i10Cnt = 0 Else i10Cnt = Convert.ToInt32(TextM10Cnt.Text)
        If TextCoin10Cnt.Text = "" Then i10CoinCnt = 0 Else i10CoinCnt = Convert.ToInt32(TextCoin10Cnt.Text)
        If TextCoin5Cnt.Text = "" Then i5CoinCnt = 0 Else i5CoinCnt = Convert.ToInt32(TextCoin5Cnt.Text)
        If TextCoin2Cnt.Text = "" Then i2CoinCnt = 0 Else i2CoinCnt = Convert.ToInt32(TextCoin2Cnt.Text)
        If TextCoin1Cnt.Text = "" Then i1CoinCnt = 0 Else i1CoinCnt = Convert.ToInt32(TextCoin1Cnt.Text)
        If TextCoin1Cnt.Text = "" Then i1CoinCnt = 0 Else i1CoinCnt = Convert.ToInt32(TextCoin1Cnt.Text)
        Dim l2000Sum As Long
        Dim l500Sum As Long
        Dim l200Sum As Long
        Dim l100Sum As Long
        Dim l50Sum As Long
        Dim l20Sum As Long
        Dim l10Sum As Long
        Dim l10CoinSum As Long
        Dim l5CoinSum As Long
        Dim l2CoinSum As Long
        Dim l1CoinSum As Long
        Dim allSum As Long
        l2000Sum = 2000 * i2000Cnt
        l500Sum = 500 * i500Cnt
        l200Sum = 200 * i200Cnt
        l100Sum = 100 * i100Cnt
        l50Sum = 50 * i50Cnt
        l20Sum = 20 * i20Cnt
        l10Sum = 10 * i10Cnt
        l10CoinSum = 10 * i10CoinCnt
        l5CoinSum = 5 * i5CoinCnt
        l2CoinSum = 2 * i2CoinCnt
        l1CoinSum = 1 * i1CoinCnt
        '入力値合計
        allSum &= l2000Sum + l500Sum + l200Sum + l100Sum + l50Sum + l20Sum + l10Sum + l10CoinSum + l5CoinSum + l2CoinSum + l1CoinSum
        Dim _MoneyReserveModel As MoneyReserveModel = New MoneyReserveModel()
        Dim _MoneyReserveControl As MoneyReserveControl = New MoneyReserveControl()
        _MoneyReserveModel.UserName = queryParams.UserName
        _MoneyReserveModel.ShipCode = queryParams.ShipCode
        _MoneyReserveModel.Status = queryParams.Status  'Status Always Open - To check for Total 
        If queryParams.PreviousDateClosing Then ' If previous date is not closed then close for next working day open
            _MoneyReserveModel.TDateTime = queryParams.OpenDate
        Else
            _MoneyReserveModel.TDateTime = dtNow.ToShortDateString
        End If
        _MoneyReserveModel.YouserName = queryParams.YouserName
        'Mistake count
        'three type of mistake possible 1) If try to do same status again 2) Opening Cash is below from all Textbox 3) AllTextBox - (OpeningCash+CashTrackAmount)
        '1st mistake   If try to do same status again
        dtStatus.MistakeCount = _MoneyReserveControl.SelectMistakeCount(_MoneyReserveModel) 'tblRecMONEY_STATUS1.Rows.Count
        _MoneyReserveModel.Status = CommonConst.MONEY_STATUS1  'Status Always 'open' - To check for Total 
        'Get Total amount from T_Reserve - Filter Status by Open/Inspection1/Inspection2/Inspection3/Close
        Dim tblRecMONEY_STATUS1 As DataTable = _MoneyReserveControl.SelectStatusRec(_MoneyReserveModel)
        If Not (tblRecMONEY_STATUS1.Rows.Count = 0) Then
            dtStatus.Total = tblRecMONEY_STATUS1.Rows(0)("total")
            dtStatus.OpeningCash = tblRecMONEY_STATUS1.Rows(0)("total")
            dtStatus.TDateTime = tblRecMONEY_STATUS1.Rows(0)("datetime")
            dtStatus.UserName = tblRecMONEY_STATUS1.Rows(0)("youser_name")
        End If
        'Get it from M_Ship_base
        Dim dtShipBase As DataTable = _MoneyReserveControl.SelectShipBase(_MoneyReserveModel.ShipCode)
        If dtShipBase.Rows.Count > 0 Then
            dtStatus.RegiDeposit = dtShipBase.Rows(0)("regi_deposit")
        End If
        'Check Opening Deposit cann't be lower than entered
        '2nd Mistake Opening Cash is below from all Textbox
        'No Need check Data Recovery branch to check it
        If not (queryParams.BRType = "DR") Then
            'Fixed Amount for Reserve for every center Integer.Parse(ConfigurationManager.AppSettings("SmtpPort"))
            'If Convert.ToDecimal(allSum) < Convert.ToDecimal(dtStatus.RegiDeposit) Then
            If Convert.ToDecimal(allSum) < Integer.Parse(ConfigurationManager.AppSettings("ReserveAmt")) Then
                dtStatus.Status = 2
                dtStatus.Message = "It became more negative than reserve settings. Please check again."
                Return dtStatus
                Exit Function
            End If
        End If
        '2nd Mistake Opening Cash is below from all Textbox 
        If dtStatus.MistakeCount >= 1 Then
            If (queryParams.UserLevel = CommonConst.UserLevel0) Or
      (queryParams.UserLevel = CommonConst.UserLevel1) Or
      (queryParams.UserLevel = CommonConst.UserLevel2) Or
(queryParams.AdminFlg = True) Then
            Else
                If DateDiff("n", dtStatus.TDateTime, dtNow) > 30 Then
                    dtStatus.Status = 2
                    dtStatus.Message = "Failed to register because it exceeded 30 minutes from the first registration, it can not be registered."
                    'Call fixedText()
                    btnSend.Enabled = False
                    Return dtStatus
                    Exit Function
                End If
            End If
        End If
        Dim CollectedAmt As Decimal = 0.00
        Dim BankDespositAmt As Decimal = 0.00
        'Open
        If queryParams.Status = CommonConst.MONEY_STATUS1 Then
            dtStatus.Diff = clsSet.setINR((Convert.ToInt64(dtStatus.RegiDeposit) - allSum).ToString)
            'Close/Inspection1/Inspection2/Inspection3
        ElseIf (queryParams.Status = CommonConst.MONEY_STATUS5) Or (queryParams.Status = CommonConst.MONEY_STATUS2) Or (queryParams.Status = CommonConst.MONEY_STATUS3) Or (queryParams.Status = CommonConst.MONEY_STATUS4) Then
            'Deposited to bank on the day
            BankDespositAmt = _MoneyReserveControl.SelectCashTrackBankDeposit(_MoneyReserveModel)
            'Collected amount from the customer
            CollectedAmt = _MoneyReserveControl.SelectCashTrackClaim(_MoneyReserveModel)
            dtStatus.Diff = clsSet.setINR((Convert.ToDecimal(allSum) + BankDespositAmt) - (CollectedAmt + dtStatus.OpeningCash))
            '''dtStatus.Diff = clsSet.setINR((amount - (Convert.ToDecimal(allSum) - dtStatus.Total)).ToString)
        End If
        '***共通結果表示***
        TextM2000Sum.Text = l2000Sum.ToString
        TextM500Sum.Text = l500Sum.ToString
        TextM200Sum.Text = l200Sum.ToString
        TextM100Sum.Text = l100Sum.ToString
        TextM50Sum.Text = l50Sum.ToString
        TextM20Sum.Text = l20Sum.ToString
        TextM10Sum.Text = l10Sum.ToString
        TextCoin10Sum.Text = l10CoinSum.ToString
        TextCoin5Sum.Text = l5CoinSum.ToString
        TextCoin2Sum.Text = l2CoinSum.ToString
        TextCoin1Sum.Text = l1CoinSum.ToString
        TextTotal.Text = allSum.ToString
        TextDiff.Visible = True
        TextDiff.Text = dtStatus.Diff
        'Intialize the Object
        Dim _MoneyReserveModel1 As MoneyReserveModel = New MoneyReserveModel()
        _MoneyReserveModel1.CRTDT = dtNow
        _MoneyReserveModel1.CRTCD = queryParams.UserId
        _MoneyReserveModel1.UPDPG = "MoneyReserveQg"
        _MoneyReserveModel1.DELFG = 0
        _MoneyReserveModel1.Status = queryParams.Status
        _MoneyReserveModel1.YouserName = queryParams.YouserName
        'Previous working day not closed then close begining of next working day
        If queryParams.PreviousDateClosing Then
            _MoneyReserveModel1.TDateTime = queryParams.OpenDate
        Else
            _MoneyReserveModel1.TDateTime = dtNow
        End If
        _MoneyReserveModel1.M2000 = l2000Sum.ToString
        _MoneyReserveModel1.M500 = l500Sum.ToString
        _MoneyReserveModel1.M200 = l200Sum.ToString
        _MoneyReserveModel1.M100 = l100Sum.ToString
        _MoneyReserveModel1.M50 = l50Sum.ToString
        _MoneyReserveModel1.M20 = l20Sum.ToString
        _MoneyReserveModel1.M10 = l10Sum.ToString
        _MoneyReserveModel1.C10 = l10CoinSum.ToString
        _MoneyReserveModel1.C5 = l5CoinSum.ToString
        _MoneyReserveModel1.C2 = l2CoinSum.ToString
        _MoneyReserveModel1.C1 = l1CoinSum.ToString
        _MoneyReserveModel1.Total = allSum.ToString
        _MoneyReserveModel1.Diff = dtStatus.Diff
        _MoneyReserveModel1.Reserve = dtStatus.RegiDeposit
        _MoneyReserveModel1.ShipCode = queryParams.ShipCode
        If Convert.ToDecimal(dtStatus.Diff) = 0.00 Then
            _MoneyReserveModel1.Mistake = 0
        Else
            _MoneyReserveModel1.Mistake = 1
            '3  Mistake  AllTextBox - (OpeningCash+CashTrackAmount)
            dtStatus.MistakeCount = 1 
        End If
        _MoneyReserveModel1.IpAddress = System.Web.HttpContext.Current.Request.UserHostAddress
        dtStatus.IpAddress = System.Web.HttpContext.Current.Request.UserHostAddress
        dtStatus.LastUpdated = dtNow

        Dim blInsert As Boolean = _MoneyReserveControl.InsertTReserve(_MoneyReserveModel1)
        If Not blInsert Then
            dtStatus.Status = 2
            dtStatus.Message = "The denomination registration process failed."
            Return dtStatus
            Exit Function
        Else
            dtStatus.Status = 1
            dtStatus.Message = "Success"
            Return dtStatus
        End If
    End Function

    Protected Sub showMsg(ByVal Msg As String)

        lblMsg.Text = Msg
        Dim sScript As String = "$(function () { $( ""#dialog"" ).dialog({width: 400, buttons: {""OK"": function () {$(this).dialog('close');}}});});"
        'JavaScriptの埋め込み
        ClientScript.RegisterStartupScript(Me.GetType(), "startup", sScript, True)

    End Sub
    '確認者の確認完了の登録
    Protected Sub btnSend2_Click(sender As Object, e As ImageClickEventArgs) Handles btnSend2.Click
        Dim shipCode As String = Session("ship_code")
        Dim userid As String = Session("user_id")
        Dim shipName As String = Session("ship_Name")
        Dim username As String = Session("user_Name")
        'Find confirm user empty or entered
        If txtConfUser.Text = "" Then
            Call showMsg("Enter confirm User ID")
            Exit Sub
        End If
        'Find Password empty or entered
        If txtConfPwd.Text = "" Then
            Call showMsg("Enter Password")
            Exit Sub
        End If
        'Verify Current User and Confirm user is different 
        If userid = txtConfUser.Text.Trim() Then
            Call showMsg("Since login user and confirmer ID are the same, processing is canceled.")
            Exit Sub
        End If
        'Verify entered user and password correct or not with the database
        Dim _UserInfoModel As UserInfoModel = New UserInfoModel()
        Dim _UserInfoControl As UserInfoControl = New UserInfoControl()
        _UserInfoModel.UserId = txtConfUser.Text.ToString.Trim()
        _UserInfoModel.Password = txtConfPwd.Text.ToString().Trim()
        Dim UserInfoList As List(Of UserInfoModel) = _UserInfoControl.SelectUserInfo(_UserInfoModel)
        'User Doesn't exist
        If UserInfoList Is Nothing OrElse UserInfoList.Count = 0 Then
            Call showMsg("The username / password incorrect. Please try again")
            Exit Sub
        End If
        'Store user level
        ViewState("userlevel") = UserInfoList(0)
        'Exstract 
        Dim ship() As String
        If UserInfoList(0).Ship1 IsNot DBNull.Value Then
            ship = Split(UserInfoList(0).Ship1, ",")
        End If
        If (UserInfoList(0).UserLevel = CommonConst.UserLevel0) Or
                        (UserInfoList(0).UserLevel = CommonConst.UserLevel1) Or
                        (UserInfoList(0).UserLevel = CommonConst.UserLevel2) Or
                (UserInfoList(0).AdminFlg = True) Then
        Else
            Dim hitFlg As Integer
            If ship IsNot Nothing Then
                For i = 0 To ship.Length - 1
                    If shipCode = ship(i) Then
                        hitFlg = 1
                        Exit For
                    End If
                Next i
                If hitFlg = 0 Then
                    Call showMsg("Since the login user and the site are different, confirmation registration is not possible.")
                    Exit Sub
                End If
            End If
        End If
        'Update the confirm user information
        ''''''''''''''''''''''''''''''''''''''''''''''Remove
        Dim userIDConfirm As String = Trim(txtConfUser.Text)
        Dim DateTimeNow As DateTime = DateTime.Now
        Dim confirmTime As DateTime = DateTimeNow.AddMinutes(ConfigurationManager.AppSettings("TimeDiff"))
        Dim _MoneyReserveModel As MoneyReserveModel = New MoneyReserveModel()
        Dim _MoneyReserveControl As MoneyReserveControl = New MoneyReserveControl()
        _MoneyReserveModel.ShipCode = shipCode
        _MoneyReserveModel.Status = DropListStatus.Text
        _MoneyReserveModel.UserId = userid
        _MoneyReserveModel.ConfUser = txtConfUser.Text
        _MoneyReserveModel.ConfDateTime = confirmTime
        _MoneyReserveModel.ConfIpAddress = System.Web.HttpContext.Current.Request.UserHostAddress

        'Update Confirmation
        Dim blUpdate = _MoneyReserveControl.UpdateConfirmUser(_MoneyReserveModel)
        If Not blUpdate Then
            Call showMsg("Failed to register Confirmation.")
            Exit Sub
        Else
            'Disable after update to db
            btnSend2.Enabled = False
            txtConfUser.Enabled = False
            txtConfPwd.Enabled = False
        End If
        'Dropdown selected as Open
        'Update Open time
        If (DropListStatus.Text = CommonConst.MONEY_STATUS1) Or (DropListStatus.Text = CommonConst.MONEY_STATUS5) Then
            _MoneyReserveModel.RegiDeposit = TextTotal.Text.Trim 'If Status is close then Need to update regi deposit for next day opening
            blUpdate = _MoneyReserveControl.UpdateOpen(_MoneyReserveModel)
            If blUpdate Then
                Call showMsg("Registration of Confirmation is completed.")
                '確認者の登録OK

            Else
                Call showMsg("Failed to open processing start registration to ship_base failed.")
                Exit Sub
            End If
            'opening storeの表示
            ''''''''''''''''''''''''''' Call showdata2()
        End If
        'Get Surname
        ' Dim userNameConfirm As String = _MoneyReserveControl.SelectSurname(_MoneyReserveModel)
        Dim ci As New System.Globalization.CultureInfo("en-US")
        If DropListStatus.Text = CommonConst.MONEY_STATUS1 Then
            lblConfirm.Text = ""
            lblConfirm2.Text = "Confirmation " & txtConfUser.Text.Trim & " " & confirmTime
            lblConfirm2.ForeColor = System.Drawing.Color.FromArgb(51, 204, 51)
        ElseIf DropListStatus.Text = CommonConst.MONEY_STATUS2 Then
            lblCIns1.Text = ""
            lblCIns1_.Text = "Confirmation " & txtConfUser.Text.Trim & " " & confirmTime
            lblCIns1_.ForeColor = System.Drawing.Color.FromArgb(51, 204, 51)
        ElseIf DropListStatus.Text = CommonConst.MONEY_STATUS3 Then
            lblCIns2.Text = ""
            lblCIns2_.Text = "Confirmation " & txtConfUser.Text.Trim & " " & confirmTime
            lblCIns2_.ForeColor = System.Drawing.Color.FromArgb(51, 204, 51)
        ElseIf DropListStatus.Text = CommonConst.MONEY_STATUS4 Then
            lblCIns3.Text = ""
            lblCIns3_.Text = "Confirmation " & txtConfUser.Text.Trim & " " & confirmTime
            lblCIns3_.ForeColor = System.Drawing.Color.FromArgb(51, 204, 51)
        ElseIf DropListStatus.Text = CommonConst.MONEY_STATUS5 Then
            lblConfirmOut.Text = ""
            lblConfirmOut2.Text = "Confirmation " & txtConfUser.Text.Trim & " " & confirmTime
            lblConfirmOut2.ForeColor = System.Drawing.Color.FromArgb(51, 204, 51)
            '完了メッセージ
            If Trim(lblDiff3.Text) = "0 INR" Or Trim(lblDiff3.Text) = "0.00 INR" Then
                lblLastMsg.Visible = True
            Else
                lblLastMsg.Visible = False
            End If
        End If
        'Dispay Log to user
        lblUpdateShow2.Visible = True
        lblLastupdate2.Text = confirmTime.ToString
        lblIPShow2.Visible = True
        lblIP2.Visible = True
        lblIP2.Text = System.Web.HttpContext.Current.Request.UserHostAddress


        If ConfigurationManager.AppSettings("EmailSend") = "true" Then
            ''''''''''''''''''''''
            'Send Report by Email
            '''''''''''''''''''''
            Dim userMail As String = _MoneyReserveControl.SelectEmail(_MoneyReserveModel)
            Dim mailStatus As Boolean = False
            Try
                Dim mailParams As MailModel = New MailModel()
                Dim mailBLL As MailControl = New MailControl()
                mailParams.FromMail = ConfigurationManager.AppSettings("Mail")
                Dim lstMail As List(Of String) = New List(Of String)()
                lstMail.Add(ConfigurationManager.AppSettings("ToEmail1"))
                lstMail.Add(ConfigurationManager.AppSettings("ToEmail2"))
                lstMail.Add(ConfigurationManager.AppSettings("ToEmail3"))
                lstMail.Add(ConfigurationManager.AppSettings("ToEmail4"))
                mailParams.ToMail = lstMail
                mailParams.TitleMail = "**[" & DropListStatus.Text & " SSC]** " & DateTimeNow.ToShortDateString & "_" & shipName
                Dim body As String = String.Empty
                'body = body & " Line 1" & Environment.NewLine
                'body = body & " Line 2" & Environment.NewLine

                body &= "-----" & DropListStatus.Text & " SSC Report -----" & vbCrLf
                body &= DateTimeNow.ToString & vbCrLf
                body &= "Money Count User : " & username & vbCrLf
                body &= "Confirmation User : " & txtConfUser.Text.Trim & vbCrLf
                body &= lblLastupdate.Text & vbCrLf
                body &= "Confirm Lastupdate : " & lblLastupdate2.Text & vbCrLf
                If DropListStatus.Text = CommonConst.MONEY_STATUS1 Then
                    body &= "Open Deposit : " & lblResult.Text & "  " & lblName.Text & " " & lblTime.Text & " " & lblRegiDeposi.Text & " " & lblConfirm2.Text
                ElseIf DropListStatus.Text = CommonConst.MONEY_STATUS2 Then
                    body &= DropListStatus.Text & " : " & lblResultIns1.Text & "  " & lblName0.Text & " " & lblTime0.Text & " " & lblDiff0.Text & " " & lblCIns1_.Text
                ElseIf DropListStatus.Text = CommonConst.MONEY_STATUS3 Then
                    body &= DropListStatus.Text & " : " & lblResultIns2.Text & "  " & lblName1.Text & " " & lblTime1.Text & " " & lblDiff1.Text & " " & lblCIns2_.Text
                ElseIf DropListStatus.Text = CommonConst.MONEY_STATUS4 Then
                    body &= DropListStatus.Text & " : " & lblResultIns3.Text & "  " & lblName2.Text & " " & lblTime2.Text & " " & lblDiff2.Text & " " & lblCIns3_.Text
                ElseIf DropListStatus.Text = CommonConst.MONEY_STATUS5 Then
                    body &= DropListStatus.Text & " : " & lblResultClose.Text & "  " & lblName3.Text & " " & lblTime3.Text & " " & lblDiff3.Text & " " & lblConfirmOut2.Text
                End If
                body &= vbCrLf
                'For convert to decimal
                Dim clsSet As New Class_money
                body &= "Total:" & clsSet.setINR(TextTotal.Text.Trim) & " INR    Diff: " & TextDiff.Text.Trim & " INR"
                mailParams.BodyMail = body
                mailParams.SmtpMail = ConfigurationManager.AppSettings("Smtp")
                mailParams.PortMail = Integer.Parse(ConfigurationManager.AppSettings("SmtpPort"))
                mailParams.EnableSslMail = True
                mailParams.CredentialsMail = ConfigurationManager.AppSettings("Mail")
                mailParams.CredentialsPassword = ConfigurationManager.AppSettings("Password")
                Dim flag As Boolean = mailBLL.SendMail(mailParams)
                If flag Then
                    mailStatus = True
                Else
                    mailStatus = False
                End If
            Catch ex As Exception
                mailStatus = False
            End Try
            If mailStatus Then
                Call showMsg("The Email has been sent successfully")
            Else
                Call showMsg("Failed to send Email")
            End If
        End If

        btnSend2.Enabled = False
        btnSend.Enabled = False
    End Sub


    Protected Sub setResult(ByVal status As String, ByVal diff As String)

        If status = CommonConst.MONEY_STATUS1 Then

            If diff = "0" Or diff = "0.00" Then
                lblName.ForeColor = System.Drawing.Color.FromArgb(0, 0, 0)
                lblTime.ForeColor = System.Drawing.Color.FromArgb(0, 0, 0)
                lblRegiDeposi.ForeColor = System.Drawing.Color.FromArgb(0, 0, 0)
                lblResult.ForeColor = System.Drawing.Color.FromArgb(0, 0, 0)
                lblmistake0.ForeColor = System.Drawing.Color.FromArgb(0, 0, 0)
                lblmistakeOpen.ForeColor = System.Drawing.Color.FromArgb(0, 0, 0)
                lblResult.Text = "OK"
            Else
                '赤色
                lblName.ForeColor = System.Drawing.Color.FromArgb(255, 0, 0)
                lblTime.ForeColor = System.Drawing.Color.FromArgb(255, 0, 0)
                lblRegiDeposi.ForeColor = System.Drawing.Color.FromArgb(255, 0, 0)
                lblResult.ForeColor = System.Drawing.Color.FromArgb(255, 0, 0)
                lblmistake0.ForeColor = System.Drawing.Color.FromArgb(255, 0, 0)
                lblmistakeOpen.ForeColor = System.Drawing.Color.FromArgb(255, 0, 0)
                lblResult.Text = "mistake"
            End If

        ElseIf status = CommonConst.MONEY_STATUS2 Then

            If diff = "0" Or diff = "0.00" Then
                lblDiff0.ForeColor = System.Drawing.Color.FromArgb(0, 0, 0)
                lblName0.ForeColor = System.Drawing.Color.FromArgb(0, 0, 0)
                lblTime0.ForeColor = System.Drawing.Color.FromArgb(0, 0, 0)
                lblResultIns1.ForeColor = System.Drawing.Color.FromArgb(0, 0, 0)
                lblResultIns1.Text = "OK"
                lblmistake1.ForeColor = System.Drawing.Color.FromArgb(0, 0, 0)
                lblmistakeIns1.ForeColor = System.Drawing.Color.FromArgb(0, 0, 0)
            Else
                '赤色
                lblDiff0.ForeColor = System.Drawing.Color.FromArgb(255, 0, 0)
                lblName0.ForeColor = System.Drawing.Color.FromArgb(255, 0, 0)
                lblTime0.ForeColor = System.Drawing.Color.FromArgb(255, 0, 0)
                lblResultIns1.ForeColor = System.Drawing.Color.FromArgb(255, 0, 0)
                lblResultIns1.Text = "mistake"
                lblmistake1.ForeColor = System.Drawing.Color.FromArgb(255, 0, 0)
                lblmistakeIns1.ForeColor = System.Drawing.Color.FromArgb(255, 0, 0)
            End If

        ElseIf status = CommonConst.MONEY_STATUS3 Then

            If diff = "0" Or diff = "0.00" Then
                lblDiff1.ForeColor = System.Drawing.Color.FromArgb(0, 0, 0)
                lblName1.ForeColor = System.Drawing.Color.FromArgb(0, 0, 0)
                lblTime1.ForeColor = System.Drawing.Color.FromArgb(0, 0, 0)
                lblResultIns2.ForeColor = System.Drawing.Color.FromArgb(0, 0, 0)
                lblResultIns2.Text = "OK"
                lblmistake2.ForeColor = System.Drawing.Color.FromArgb(0, 0, 0)
                lblmistakeIns2.ForeColor = System.Drawing.Color.FromArgb(0, 0, 0)
            Else
                '赤色
                lblDiff1.ForeColor = System.Drawing.Color.FromArgb(255, 0, 0)
                lblName1.ForeColor = System.Drawing.Color.FromArgb(255, 0, 0)
                lblTime1.ForeColor = System.Drawing.Color.FromArgb(255, 0, 0)
                lblResultIns2.ForeColor = System.Drawing.Color.FromArgb(255, 0, 0)
                lblResultIns2.Text = "mistake"
                lblmistake2.ForeColor = System.Drawing.Color.FromArgb(255, 0, 0)
                lblmistakeIns2.ForeColor = System.Drawing.Color.FromArgb(255, 0, 0)
            End If

        ElseIf status = CommonConst.MONEY_STATUS4 Then

            If diff = "0" Or diff = "0.00" Then
                lblDiff2.ForeColor = System.Drawing.Color.FromArgb(0, 0, 0)
                lblName2.ForeColor = System.Drawing.Color.FromArgb(0, 0, 0)
                lblTime2.ForeColor = System.Drawing.Color.FromArgb(0, 0, 0)
                lblResultIns3.ForeColor = System.Drawing.Color.FromArgb(0, 0, 0)
                lblResultIns3.Text = "OK"
                lblmistake3.ForeColor = System.Drawing.Color.FromArgb(0, 0, 0)
                lblmistakeIns3.ForeColor = System.Drawing.Color.FromArgb(0, 0, 0)
            Else
                '赤色
                lblDiff2.ForeColor = System.Drawing.Color.FromArgb(255, 0, 0)
                lblName2.ForeColor = System.Drawing.Color.FromArgb(255, 0, 0)
                lblTime2.ForeColor = System.Drawing.Color.FromArgb(255, 0, 0)
                lblResultIns3.ForeColor = System.Drawing.Color.FromArgb(255, 0, 0)
                lblResultIns3.Text = "mistake"
                lblmistake3.ForeColor = System.Drawing.Color.FromArgb(255, 0, 0)
                lblmistakeIns3.ForeColor = System.Drawing.Color.FromArgb(255, 0, 0)
            End If

        ElseIf status = CommonConst.MONEY_STATUS5 Then

            If diff = "0" Or diff = "0.00" Then
                lblDiff3.ForeColor = System.Drawing.Color.FromArgb(0, 0, 0)
                lblName3.ForeColor = System.Drawing.Color.FromArgb(0, 0, 0)
                lblTime3.ForeColor = System.Drawing.Color.FromArgb(0, 0, 0)
                lblResultClose.ForeColor = System.Drawing.Color.FromArgb(0, 0, 0)
                lblResultClose.Text = "OK"
                lblmistake4.ForeColor = System.Drawing.Color.FromArgb(0, 0, 0)
                lblmistakeClose.ForeColor = System.Drawing.Color.FromArgb(0, 0, 0)
            Else
                '赤色
                lblDiff3.ForeColor = System.Drawing.Color.FromArgb(255, 0, 0)
                lblName3.ForeColor = System.Drawing.Color.FromArgb(255, 0, 0)
                lblTime3.ForeColor = System.Drawing.Color.FromArgb(255, 0, 0)
                lblResultClose.ForeColor = System.Drawing.Color.FromArgb(255, 0, 0)
                lblResultClose.Text = "mistake"
                lblmistake4.ForeColor = System.Drawing.Color.FromArgb(255, 0, 0)
                lblmistakeClose.ForeColor = System.Drawing.Color.FromArgb(255, 0, 0)
            End If

        End If

    End Sub

    ''' <summary>
    ''' Check showdata downn'''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
    ''' </summary>
    Protected Sub showdata2()

        Dim shipCode As String = Session("ship_code")

        '***OPEN or CLOSEの表示
        Dim clsSet As New Class_money
        Dim opentime As String = ""
        Dim closetime As String = ""
        Dim errMsg As String

        If clsSet.chkOpen(shipCode, opentime, closetime, errMsg) = False Then
            lblDispStatu.Text = "closed"
        Else
            lblDispStatu.Text = "opening store"
        End If

        If errMsg <> "" Then
            lblDispStatu.Text = ""
            Call showMsg(errMsg)
            Exit Sub
        End If

    End Sub

    Protected Sub setDenomination(ByVal reserveData As Class_money.T_Reserve)

        TextM2000Sum.Text = reserveData.M_2000
        TextM500Sum.Text = reserveData.M_500
        TextM200Sum.Text = reserveData.M_200
        TextM100Sum.Text = reserveData.M_100
        TextM50Sum.Text = reserveData.M_50
        TextM20Sum.Text = reserveData.M_20
        TextM10Sum.Text = reserveData.M_10
        TextCoin10Sum.Text = reserveData.Coin_10
        TextCoin5Sum.Text = reserveData.Coin_5
        TextCoin2Sum.Text = reserveData.Coin_2
        TextCoin1Sum.Text = reserveData.Coin_1
        TextTotal.Text = reserveData.total
        TextDiff.Text = reserveData.diff

    End Sub
    ''' <summary>
    ''' Initialize Money Transaction status dropdown box
    ''' </summary>
    Private Sub IntiaizeMoneyStatus()
        '***処理ステータスをリストにセット***
        With DropListStatus
            .Items.Clear()
            .Items.Add("Select processing")
            .Items.Add(CommonConst.MONEY_STATUS1)
            .Items.Add(CommonConst.MONEY_STATUS2)
            .Items.Add(CommonConst.MONEY_STATUS3)
            .Items.Add(CommonConst.MONEY_STATUS4)
            .Items.Add(CommonConst.MONEY_STATUS5)
        End With
    End Sub
    ''' <summary>
    ''' Loading Default Settings
    ''' </summary>
    Private Sub LoadDefault()
        '***テキスト等制御***
        TextM2000Sum.Enabled = False
        TextM500Sum.Enabled = False
        TextM200Sum.Enabled = False
        TextM100Sum.Enabled = False
        TextM50Sum.Enabled = False
        TextM20Sum.Enabled = False
        TextM10Sum.Enabled = False
        TextCoin10Sum.Enabled = False
        TextCoin5Sum.Enabled = False
        TextCoin2Sum.Enabled = False
        TextCoin1Sum.Enabled = False
        TextTotal.Enabled = False
        TextDiff.Enabled = False
        lblLastMsg.Visible = False


        lblmistake0.Visible = False
        lblmistake1.Visible = False
        lblmistake2.Visible = False
        lblmistake3.Visible = False
        lblmistake4.Visible = False
        lblUpdateShow.Visible = False
        lblIPShow.Visible = False
        lblUpdateShow2.Visible = False
        lblIPShow2.Visible = False

        btnSend2.Enabled = False
        txtConfUser.Enabled = False
        txtConfPwd.Enabled = False
    End Sub

    Protected Sub fixedText()

        TextM2000Sum.Enabled = False
        TextM2000Cnt.Enabled = False
        TextM500Sum.Enabled = False
        TextM500Cnt.Enabled = False
        TextM200Sum.Enabled = False
        TextM200Cnt.Enabled = False
        TextM100Sum.Enabled = False
        TextM100Cnt.Enabled = False
        TextM50Sum.Enabled = False
        TextM50Cnt.Enabled = False
        TextM20Sum.Enabled = False
        TextM20Cnt.Enabled = False
        TextM10Sum.Enabled = False
        TextM10Cnt.Enabled = False
        TextCoin10Sum.Enabled = False
        TextCoin10Cnt.Enabled = False
        TextCoin5Sum.Enabled = False
        TextCoin5Cnt.Enabled = False
        TextCoin2Sum.Enabled = False
        TextCoin2Cnt.Enabled = False
        TextCoin1Sum.Enabled = False
        TextCoin1Cnt.Enabled = False
        TextTotal.Enabled = False
        TextDiff.Enabled = False

    End Sub


End Class


