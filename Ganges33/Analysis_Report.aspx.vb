Imports Ganges33.Ganges33.model
Imports Ganges33.Ganges33.logic
Public Class Analysis_Report
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        '***初期処理***
        If IsPostBack = False Then

            'Bydefailt set maximum length 
            TextNote.Attributes("maxlength") = 2000

            '***セッションなしログインユーザの対応***
            Dim userid As String = Session("user_id")
            If userid = "" Then
                Response.Redirect("Login.aspx")
            End If

            Dim tb As New DataTable
            tb.Columns.Add("Text", GetType(String))
            tb.Columns.Add("Value", GetType(Integer))
            tb.Rows.Add("January", 1)
            tb.Rows.Add("February", 2)
            tb.Rows.Add("March", 3)
            tb.Rows.Add("April", 4)
            tb.Rows.Add("May", 5)
            tb.Rows.Add("June", 6)
            tb.Rows.Add("July", 7)
            tb.Rows.Add("August", 8)
            tb.Rows.Add("September", 9)
            tb.Rows.Add("October", 10)
            tb.Rows.Add("November", 11)
            tb.Rows.Add("December", 12)


            '***セッション取得***
            Dim userLevel As String = Session("user_level")
            Dim adminFlg As Boolean = Session("admin_Flg")
            Dim setShipname As String = Session("ship_Name")
            Dim shipCode As String = Session("ship_code")
            Dim userName As String = Session("user_Name")

            Dim clsSetCommon As New Class_common
            Dim dtNow As DateTime = clsSetCommon.dtIndia
            Dim workDay As Integer
            Dim workMonth As Integer
            Dim i As Integer

            '***表示の設定***
            lblLoc.Text = setShipname
            lblName.Text = userName

            If (userLevel = "0" Or userLevel = "1" Or userLevel = "2" Or adminFlg = True) Then

                lblMonNow.Visible = False
                DropDownActiveMonth.Visible = True

                '**ActiveMonthのリスト設定
                '今月
                'workMonth = Convert.ToInt32(dtNow.ToShortDateString.Substring(5, 2))

                '/* Added Mon Year VJ 20100106*/

                '管理者が選択できる月を設定
                'For i = 1 To workMonth
                '    '1月から今月まで
                '    DropDownActiveMonth.Items.Add(i)
                'Next i

                DropDownActiveMonth.Items.Clear()
                DropDownActiveMonth.DataTextField = "Text"
                DropDownActiveMonth.DataValueField = "Value"
                DropDownActiveMonth.DataSource = tb
                DropDownActiveMonth.DataBind()
            Else
                '**ActiveMonthのラベル設定
                DropDownActiveMonth.Visible = False
                lblMonNow.Visible = True
                lblMonNow.Text = dtNow.ToShortDateString.Substring(5, 2)
            End If

            '***日付リストの設定***
            DropDownDay.Items.Clear()

            '本日日付
            workDay = Convert.ToInt32(dtNow.ToShortDateString.Substring(8, 2))

            With DropDownDay
                If (userLevel = "0" Or userLevel = "1" Or userLevel = "2" Or adminFlg = True) Then
                    '管理者が選択できる日付を設定
                    'デフォルトは１月なので、31日まで。
                    For i = 1 To 31
                        If i.ToString.Length = 1 Then
                            DropDownDay.Items.Add("0" & i.ToString())
                        Else
                            DropDownDay.Items.Add(i.ToString())
                        End If
                    Next i
                Else
                    '管理者以外が選択できる日付を設定
                    If workDay > 3 Then
                        '本日日付が4日以降
                        For i = workDay - 2 To workDay
                            If (i.ToString).Length = 1 Then
                                DropDownDay.Items.Add("0" & i)
                            Else
                                DropDownDay.Items.Add(i)
                            End If
                        Next i
                    Else
                        '本日日付が3日以内
                        For i = 1 To workDay
                            DropDownDay.Items.Add("0" & i)
                        Next i
                    End If
                End If
            End With

            '***monthリストの設定***
            DropDownMonth.Items.Clear()



            DropDownMonth.DataTextField = "Text"
            DropDownMonth.DataValueField = "Value"
            DropDownMonth.DataSource = tb
            DropDownMonth.DataBind()
            '/* Added Mon Year VJ 20100106*/
            'With DropDownMonth
            '    .Items.Add("January")
            '    .Items.Add("February")
            '    .Items.Add("March")
            '    .Items.Add("April")
            '    .Items.Add("May")
            '    .Items.Add("June")
            '    .Items.Add("July")
            '    .Items.Add("August")
            '    .Items.Add("September")
            '    .Items.Add("October")
            '    .Items.Add("November")
            '    .Items.Add("December")
            'End With
            Dim tblYear As New DataTable
            tblYear.Columns.Add("Text", GetType(String))
            tblYear.Columns.Add("Value", GetType(Integer))
            tblYear.Rows.Add("2018", 2018)
            tblYear.Rows.Add("2019", 2019)
            tblYear.Rows.Add("2020", 2020)
            tblYear.Rows.Add("2021", 2021)
            tblYear.Rows.Add("2022", 2022)
            tblYear.Rows.Add("2023", 2023)
            tblYear.Rows.Add("2024", 2024)
            tblYear.Rows.Add("2025", 2025)
            tblYear.Rows.Add("2026", 2026)
            tblYear.Rows.Add("2027", 2027)
            tblYear.Rows.Add("2028", 2028)
            tblYear.Rows.Add("2029", 2029)
            tblYear.Rows.Add("2030", 2030)

            DropDownYear.Items.Clear()
            DropDownYear.DataTextField = "Text"
            DropDownYear.DataValueField = "Value"
            DropDownYear.DataSource = tblYear
            DropDownYear.DataBind()

            ddlActiveYear.Items.Clear()
            ddlActiveYear.DataTextField = "Text"
            ddlActiveYear.DataValueField = "Value"
            ddlActiveYear.DataSource = tblYear
            ddlActiveYear.DataBind()
            '/* Added Mon Year VJ 20100106*/
            'With DropDownYear
            '    .Items.Add("2018")
            '    .Items.Add("2019")
            '    .Items.Add("2020")
            '    .Items.Add("2021")
            '    .Items.Add("2022")
            '    .Items.Add("2023")
            '    .Items.Add("2024")
            '    .Items.Add("2025")
            '    .Items.Add("2026")
            '    .Items.Add("2027")
            '    .Items.Add("2028")
            '    .Items.Add("2029")
            '    .Items.Add("2030")
            'End With
            'Added on 20191113
            'Default selection for month
            DropDownActiveMonth.SelectedValue = Trim(Now.Month())
            'Default Day selection
            Dim strDay As String = ""
            strDay = Now.Day()
            If (Len(strDay) < 2) Then
                strDay = "0" & strDay
            End If
            DropDownDay.SelectedValue = strDay
            'Default Year
            DropDownYear.SelectedValue = Now.Year
            ddlActiveYear.SelectedValue = Now.Year
            'Default Month
            DropDownMonth.SelectedValue = Now.Month

            DropDownSelection()
        Else
            '***月日付の指定をセッション変数に設定***
            'Comment on 20191120
            Dim setMon As String = DropDownMonth.SelectedIndex.ToString("00")
            'Dim setMon As String = DropDownMonth.SelectedItem.Value
            Dim setDay As String = DropDownDay.Text
            Dim setYear As String = DropDownYear.Text
            'open用の月指定
            Session("set_Mon") = setMon
            Session("set_Day") = setDay
            Session("set_Year") = setYear
            'ActiveMonthの設定
            Dim setMon2 As String
            If DropDownActiveMonth.Visible = True Then
                setMon2 = DropDownActiveMonth.Text
                If setMon2.Length = 1 Then
                    setMon2 = "0" & setMon2
                End If
            Else
                setMon2 = lblMonNow.Text
            End If
            'send用の月指定
            Session("set_Mon2") = setMon2
        End If


    End Sub

    Protected Sub btnSend_Click(sender As Object, e As EventArgs) Handles btnSend.Click

        '***セッション取得***
        Dim userid As String = Session("user_id")

        If userid = "" Then
            Call showMsg("The session has expired. Please login again.", "")
            Exit Sub
        End If

        Dim Shipname As String = Session("ship_Name")
        Dim shipCode As String = Session("ship_code")
        Dim userName As String = Session("user_Name")
        Dim setDay As String = Session("set_Day")
        Dim setMon2 As String = Session("set_Mon2")

        '***リセット***
        Call reSet()

        '***入力チェック/設定***
        Dim report As Class_analysis.ACTIVITY_REPORT
        Dim intChk As Integer

        'Comment on 20191120
        ''''''If TextCustomer_Visit.Text = "" Then
        ''''''    Call showMsg("Please enter a numerical value.", "")
        ''''''    TextCustomer_Visit.BackColor = System.Drawing.Color.FromArgb(255, 0, 0)
        ''''''    Exit Sub
        ''''''Else
        ''''''    If Integer.TryParse(Trim(TextCustomer_Visit.Text), intChk) = False Then
        ''''''        Call showMsg("Please enter a numerical value.", "")
        ''''''        TextCustomer_Visit.BackColor = System.Drawing.Color.FromArgb(255, 0, 0)
        ''''''        Exit Sub
        ''''''    Else
        ''''''        report.Customer_Visit = TextCustomer_Visit.Text
        ''''''    End If
        ''''''End If

        ''''''If TextCall_Registered.Text = "" Then
        ''''''    Call showMsg("Please enter a numerical value.", "")
        ''''''    TextCall_Registered.BackColor = System.Drawing.Color.FromArgb(255, 0, 0)
        ''''''    Exit Sub
        ''''''Else
        ''''''    If Integer.TryParse(Trim(TextCall_Registered.Text), intChk) = False Then
        ''''''        Call showMsg("Please enter a numerical value.", "")
        ''''''        TextCall_Registered.BackColor = System.Drawing.Color.FromArgb(255, 0, 0)
        ''''''        Exit Sub
        ''''''    Else
        ''''''        report.Call_Registerd = TextCall_Registered.Text
        ''''''    End If
        ''''''End If

        ''''''If TextRepair_Completed.Text = "" Then
        ''''''    Call showMsg("Please enter a numerical value.", "")
        ''''''    TextRepair_Completed.BackColor = System.Drawing.Color.FromArgb(255, 0, 0)
        ''''''    Exit Sub
        ''''''Else
        ''''''    If Integer.TryParse(Trim(TextRepair_Completed.Text), intChk) = False Then
        ''''''        Call showMsg("Please enter a numerical value.", "")
        ''''''        TextRepair_Completed.BackColor = System.Drawing.Color.FromArgb(255, 0, 0)
        ''''''        Exit Sub
        ''''''    Else
        ''''''        report.Repair_Completed = TextRepair_Completed.Text
        ''''''    End If
        ''''''End If

        ''''''If TextGoods_Delivered.Text = "" Then
        ''''''    Call showMsg("Please enter a numerical value.", "")
        ''''''    TextGoods_Delivered.BackColor = System.Drawing.Color.FromArgb(255, 0, 0)
        ''''''    Exit Sub
        ''''''Else
        ''''''    If Integer.TryParse(Trim(TextGoods_Delivered.Text), intChk) = False Then
        ''''''        Call showMsg("Please enter a numerical value.", "")
        ''''''        TextGoods_Delivered.BackColor = System.Drawing.Color.FromArgb(255, 0, 0)
        ''''''        Exit Sub
        ''''''    Else
        ''''''        report.Goods_Delivered = TextGoods_Delivered.Text
        ''''''    End If
        ''''''End If

        ''''''If TextPending_Calls.Text = "" Then
        ''''''    Call showMsg("Please enter a numerical value.", "")
        ''''''    TextPending_Calls.BackColor = System.Drawing.Color.FromArgb(255, 0, 0)
        ''''''    Exit Sub
        ''''''Else
        ''''''    If Integer.TryParse(Trim(TextPending_Calls.Text), intChk) = False Then
        ''''''        Call showMsg("Please enter a numerical value.", "")
        ''''''        TextPending_Calls.BackColor = System.Drawing.Color.FromArgb(255, 0, 0)
        ''''''        Exit Sub
        ''''''    Else
        ''''''        report.Pending_Calls = TextPending_Calls.Text
        ''''''    End If
        ''''''End If

        ''''''If TextCancelled_Calls.Text = "" Then
        ''''''    Call showMsg("Please enter a numerical value.", "")
        ''''''    TextCancelled_Calls.BackColor = System.Drawing.Color.FromArgb(255, 0, 0)
        ''''''    Exit Sub
        ''''''Else
        ''''''    If Integer.TryParse(Trim(TextCancelled_Calls.Text), intChk) = False Then
        ''''''        Call showMsg("Please enter a numerical value.", "")
        ''''''        TextCancelled_Calls.BackColor = System.Drawing.Color.FromArgb(255, 0, 0)
        ''''''        Exit Sub
        ''''''    Else
        ''''''        report.Cancelled_Calls = TextCancelled_Calls.Text
        ''''''    End If
        ''''''End If
        '''
        'Required Fields validation

        Dim strEmptCheck As String = ""
        Dim isValid As Boolean = True
        If Not (CommonControl.isNumberValid(TextCustomer_Visit.Text.Trim)) Then
            strEmptCheck = strEmptCheck & "Customer Visit<br>"
            isValid = False
        End If
        If Not (CommonControl.isNumberValid(TextCall_Registered.Text.Trim)) Then
            strEmptCheck = strEmptCheck & "Call Registered<br>"
            isValid = False
        End If

        If Not (CommonControl.isNumberValid(TextRepair_Completed.Text.Trim)) Then
            strEmptCheck = strEmptCheck & "Repair Completed<br>"
            isValid = False
        End If

        If Not (CommonControl.isNumberValid(TextGoods_Delivered.Text.Trim)) Then
            strEmptCheck = strEmptCheck & "Goods Delivered<br>"
            isValid = False
        End If

        If Not (CommonControl.isNumberValid(TextPending_Calls.Text.Trim)) Then
            strEmptCheck = strEmptCheck & "Pending Calls<br>"
            isValid = False
        End If

        If Not (CommonControl.isNumberValid(TextCancelled_Calls.Text.Trim)) Then
            strEmptCheck = strEmptCheck & "Cancelled Calls<br>"
            isValid = False
        End If



        If Not isValid Then
            Call showMsg("The following field(s) are mandatory and must be +ve ...<br><br>" & strEmptCheck)
            Exit Sub
        End If

        'Assign value to the object
        report.Customer_Visit = TextCustomer_Visit.Text
        report.Call_Registerd = TextCall_Registered.Text
        report.Repair_Completed = TextRepair_Completed.Text
        report.Goods_Delivered = TextGoods_Delivered.Text
        report.Pending_Calls = TextPending_Calls.Text
        report.Cancelled_Calls = TextCancelled_Calls.Text


        report.note = Trim(TextNote.Text)
        'report.month = setMon2 '/* Added Mon Year VJ 20100106*/
        report.ActMonth = setMon2 '/* Added Mon Year VJ 20100106*/
        report.ActYear = ddlActiveYear.SelectedValue '/* Added Mon Year VJ 20100106*/

        '***登録***
        Dim tourokuFlg As Integer
        Dim errFlg As Integer
        Dim clsSet As New Class_analysis

        Call clsSet.setActivityReport(report, userid, userName, setDay, errFlg, shipCode, tourokuFlg)

        'インド時間設定（日本のサーバ時刻との差分を設定）
        Dim clsSetCommon As New Class_common
        Dim dtNow As DateTime = clsSetCommon.dtIndia
        'Modified by Mohan on 03.Mar.2019 For Comments changed from Japanese to English
        If errFlg = 1 Then
            ListMsg.Items.Add("updatefailed…   " & dtNow)
            Call showMsg("Registration Of Activity Report failed.", "")
            Exit Sub
        Else
            '''Comment on 20191126
            'If tourokuFlg = 0 Then
            '    ListMsg.Items.Add("success! (sign up) Date：" & dtNow)
            'Else
            '    ListMsg.Items.Add("success! (Activity report) Date：" & dtNow)
            'End If

            If tourokuFlg = 0 Then
                ListMsg.Items.Add("success! (sign up)date：" & setDay & "     time：" & dtNow)
            Else
                ListMsg.Items.Add("success! (Update registration)date：" & setDay & "     time：" & dtNow)
            End If


            Call showMsg("Activity report has been successfully saved...", "")
        End If

    End Sub

    Protected Sub showMsg(ByVal Msg As String)
        lblMsg.Text = Msg
        Dim sScript As String = "$(function () { $( ""#dialog"" ).dialog({width: 400, buttons: {""OK"": function () {$(this).dialog('close');}}});});"
        'JavaScriptの埋め込み
        ClientScript.RegisterStartupScript(Me.GetType(), "startup", sScript, True)

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

    Protected Sub reSet()

        TextCustomer_Visit.BackColor = System.Drawing.Color.FromArgb(255, 255, 255)
        TextCall_Registered.BackColor = System.Drawing.Color.FromArgb(255, 255, 255)
        TextRepair_Completed.BackColor = System.Drawing.Color.FromArgb(255, 255, 255)
        TextGoods_Delivered.BackColor = System.Drawing.Color.FromArgb(255, 255, 255)
        TextPending_Calls.BackColor = System.Drawing.Color.FromArgb(255, 255, 255)
        TextCancelled_Calls.BackColor = System.Drawing.Color.FromArgb(255, 255, 255)

    End Sub

    Protected Sub btnOpen_Click(sender As Object, e As EventArgs) Handles btnOpen.Click

        Response.Redirect("Analysis_Report2.aspx")

    End Sub

    Private Sub DropDownActiveMonth_SelectedIndexChanged(sender As Object, e As EventArgs) Handles DropDownActiveMonth.SelectedIndexChanged

        Dim endday As Integer
        Dim month As Integer

        If DropDownActiveMonth.Text <> " " Then
            month = DropDownActiveMonth.SelectedIndex
        End If

        '本日日付
        Dim workDay As Integer
        Dim dtNow As DateTime = DateTime.Now
        workDay = Convert.ToInt32(dtNow.ToShortDateString.Substring(8, 2))

        '今月
        Dim workMonth As Integer
        'workMonth = Convert.ToInt32(dtNow.ToShortDateString.Substring(5, 2))

        '指定された月が今月
        If month + 1 = workMonth Then
            '本日まで
            endday = workDay
        Else
            '月末まで
            Select Case month

                Case 0, 2, 4, 6, 7, 9, 11
                    endday = 31
                Case 3, 5, 8, 10
                    endday = 30
                Case 1

                    endday = 29
                Case Else
                    endday = 31
            End Select
        End If

        Dim indexSelected As Integer = DropDownDay.SelectedIndex
        DropDownDay.Items.Clear()

        For i As Integer = 1 To endday
            If i.ToString.Length = 1 Then
                DropDownDay.Items.Add("0" & i.ToString())
            Else
                DropDownDay.Items.Add(i.ToString())
            End If
        Next

        If indexSelected <= endday - 1 Then
            DropDownDay.SelectedIndex = indexSelected
        End If
        Clearvalue()
        DropDownSelection()

    End Sub
    Protected Sub DropDownDay_SelectedIndexChanged(sender As Object, e As EventArgs)

        DropDownSelection()
    End Sub

    Protected Sub ddlActiveYear_SelectedIndexChanged(sender As Object, e As EventArgs)
        Clearvalue()
        DropDownSelection()
    End Sub

    Protected Sub DropDownActiveMonth_SelectedIndexChanged1(sender As Object, e As EventArgs)
        Clearvalue()
        DropDownSelection()
    End Sub

    Public Sub Clearvalue()

        TextCustomer_Visit.Text = ""
        TextCall_Registered.Text = ""
        TextRepair_Completed.Text = ""
        TextGoods_Delivered.Text = ""
        TextPending_Calls.Text = ""
        TextCancelled_Calls.Text = ""
        TextNote.Text = ""

    End Sub
    Public Sub DropDownSelection()
        Dim userLevel As String = Session("user_level")
        Dim adminFlg As Boolean = Session("admin_Flg")
        Dim setShipname As String = Session("ship_Name")
        Dim shipCode As String = Session("ship_code")
        Dim userName As String = Session("user_Name")
        Dim location As String = Session("ship_code")
        Dim mon As String

        mon = DropDownActiveMonth.Text
        If mon.Length = 1 Then
            mon = "0" & mon
        End If

        Dim Year = Convert.ToInt32(ddlActiveYear.Text)
        Dim month = Year & "/" & mon

        Dim _Analysis_ReportModel As ActivityReportModel = New ActivityReportModel()
        Dim _Anaysis_ReportControl As ActivityReportControl = New ActivityReportControl()
        _Analysis_ReportModel.location = location
        _Analysis_ReportModel.Day = DropDownDay.Text
        _Analysis_ReportModel.Month = month
        Dim _Datatable As DataTable = _Anaysis_ReportControl.GetAnalysis_ReportDate(_Analysis_ReportModel)

        If (_Datatable Is Nothing) Or (_Datatable.Rows.Count = 0) Then
            Clearvalue()
            Exit Sub
        Else
            If Not IsDBNull(_Datatable.Rows(0)("Customer_Visit")) Then
                TextCustomer_Visit.Text = _Datatable.Rows(0)("Customer_Visit")
            End If
            If Not IsDBNull(_Datatable.Rows(0)("Call_Registerd")) Then
                TextCall_Registered.Text = _Datatable.Rows(0)("Call_Registerd")
            End If
            If Not IsDBNull(_Datatable.Rows(0)("Repair_Completed")) Then
                TextRepair_Completed.Text = _Datatable.Rows(0)("Repair_Completed")
            End If
            If Not IsDBNull(_Datatable.Rows(0)("Goods_Delivered")) Then
                TextGoods_Delivered.Text = _Datatable.Rows(0)("Goods_Delivered")
            End If
            If Not IsDBNull(_Datatable.Rows(0)("Pending_Calls")) Then
                TextPending_Calls.Text = _Datatable.Rows(0)("Pending_Calls")
            End If
            If Not IsDBNull(_Datatable.Rows(0)("Cancelled_Calls")) Then
                TextCancelled_Calls.Text = _Datatable.Rows(0)("Cancelled_Calls")
            End If
            If Not IsDBNull(_Datatable.Rows(0)("note")) Then
                TextNote.Text = _Datatable.Rows(0)("note")
            End If
        End If
    End Sub

End Class