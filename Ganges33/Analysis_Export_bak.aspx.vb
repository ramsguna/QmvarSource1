Imports System.IO
Imports System.Text
Public Class Analysis_Export_bak
    Inherits System.Web.UI.Page
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        If IsPostBack = False Then

            '***セッションなしログインユーザの対応***
            Dim userid As String = Session("user_id")
            If userid = "" Then
                Response.Redirect("Login.aspx")
            End If

            '***ログインユーザ情報表示***
            Dim setShipname As String = Session("ship_Name")
            Dim userName As String = Session("user_Name")
            Dim userLevel As String = Session("user_level")
            Dim adminFlg As Boolean = Session("admin_Flg")

            lblLoc.Text = setShipname
            lblName.Text = userName

            '***拠点名称の設定***
            DropListLocation.Items.Clear()
            If userLevel = "0" Or userLevel = "1" Or userLevel = "2" Or adminFlg = True Then
                Dim shipName() As String
                If Session("ship_name_list") IsNot Nothing Then
                    shipName = Session("ship_name_list")
                    With DropListLocation
                        .Items.Add("Select shipname")
                        For i = 0 To UBound(shipName)
                            If Trim(shipName(i)) <> "" Then
                                .Items.Add(shipName(i))
                            End If
                        Next i
                    End With
                End If

            Else
                btnSend.Enabled = False
            End If

            '***monthリストの設定***
            DropDownMonth.Items.Clear()
            With DropDownMonth
                .Items.Add("Select the month")
                .Items.Add("January")
                .Items.Add("February")
                .Items.Add("March")
                .Items.Add("April")
                .Items.Add("May")
                .Items.Add("June")
                .Items.Add("July")
                .Items.Add("August")
                .Items.Add("September")
                .Items.Add("October")
                .Items.Add("November")
                .Items.Add("December")
            End With

            '***exportFileリストの設定***
            DropDownExportFile.Items.Clear()
            With DropDownExportFile
                .Items.Add("Select Export Filename")
                .Items.Add("1.PL_Tracking_Sheet")
                .Items.Add("2.Sales_Register_from_GSPN_software_for_OOW")
                .Items.Add("3.Sales_Invoice_to_samsung_C_IW")
                .Items.Add("4.Sales_Invoiec_to_samsung_EXC_IW")
                .Items.Add("5.Sales_Register_from_GSPN_software_for_IW")
                .Items.Add("6.Sales_Register_from GSPN software_for_OthersSales")
                .Items.Add("7.SAW_Discount_Details")
                .Items.Add("8.Purchase_Register")
                .Items.Add("9.Final_Report")
            End With

        Else

            '***セッション設定***
            '月を指定
            Dim setMon As String = DropDownMonth.SelectedIndex.ToString("00")
            Dim setMonName As String = DropDownMonth.Text

            Session("set_Mon2") = setMon
            Session("set_MonName") = setMonName

            '拠点を指定
            Dim exportShipName As String = DropListLocation.Text
            Session("export_shipName") = exportShipName

            'ダウンロードファイル種類を指定
            Dim exportFile As String = DropDownExportFile.Text
            Session("export_File") = exportFile

        End If

    End Sub

    Protected Sub btnSend_Click(sender As Object, e As ImageClickEventArgs) Handles btnSend.Click

        '***セッション情報取得***
        Dim userid As String = Session("user_id")
        Dim userName As String = Session("user_Name")
        Dim userLevel As String = Session("user_level")
        Dim adminFlg As Boolean = Session("admin_Flg")

        Dim setMon As String = Session("set_Mon2")
        Dim setMonName As String = Session("set_MonName")
        Dim exportFile As String = Session("export_File")
        Dim exportShipName As String = Session("export_shipName")

        Dim clsSetCommon As New Class_common
        Dim dtNow As DateTime = clsSetCommon.dtIndia

        If userid Is Nothing Then
            Call showMsg("The session has expired. Please login again.", "")
            Exit Sub
        End If

        Dim i, j As Integer

        '***入力チェック***
        If exportShipName = "Select shipname" Then
            Call showMsg("Please specify the base name.", "")
            Exit Sub
        End If

        Dim dt As DateTime
        Dim dateFrom As String
        Dim dateTo As String
        Dim outputPath As String

        If TextDateFrom.Text <> "" Then
            If exportFile = "Sales Invoice to samsung ""C""" Or exportFile = "Sales Invoiec to samsung ""EXC""" Then
                Call showMsg("You can select only the month specification.", "")
                Exit Sub
            End If
            If DateTime.TryParse(TextDateFrom.Text, dt) Then
                dateFrom = DateTime.Parse(Trim(TextDateFrom.Text)).ToShortDateString
            Else
                Call showMsg("There is an error in the date specification", "")
                Exit Sub
            End If
        Else
            dateFrom = ""
        End If

        If TextDateTo.Text <> "" Then
            If exportFile = "Sales Invoice to samsung ""C""" Or exportFile = "Sales Invoiec to samsung ""EXC""" Then
                Call showMsg("You can select only the month specification.", "")
                Exit Sub
            End If
            If DateTime.TryParse(TextDateTo.Text, dt) Then
                dateTo = DateTime.Parse(Trim(TextDateTo.Text)).ToShortDateString
            Else
                Call showMsg("The date specification of To is incorrect.", "")
                Exit Sub
            End If
        Else
            dateTo = ""
        End If

        If dateFrom = "" And dateTo = "" And setMon = "00" Then
            Call showMsg("Please specify either output period by month or date", "")
            Exit Sub
        End If

        If dateFrom <> "" And dateTo <> "" And setMon <> "00" Then
            Call showMsg("Please specify either output period by month or date.", "")
            Exit Sub
        End If

        If setMon <> "00" Then
            If dateFrom <> "" Or dateTo <> "" Then
                Call showMsg("Please specify either output period by month or date.", "")
                Exit Sub
            End If
        End If

        If exportFile = "Select Export Filename" Then
            Call showMsg("Please specify the file type to be exported.", "")
            Exit Sub
        End If

        '***CSV出力処理***
        Dim clsSet As New Class_analysis
        Dim errFlg As Integer

        '■拠点コード取得
        Dim shipCode As String
        Dim errMsg As String

        Call clsSetCommon.setShipCode(exportShipName, shipCode, errMsg)

        If errMsg <> "" Then
            Call showMsg(errMsg, "")
            Exit Sub
        End If

        'CSVファイルの種類
        Dim kindExport As Integer = Left(exportFile, 1)

        '■PL_Tracking_Sheet出力
        If kindExport = 1 Then

            Try
                '***内容設定***
                '■Activity_reportデータ取得
                Dim activeData() As Class_analysis.ACTIVITY_REPORT
                Dim dsActivity_report As New DataSet
                Dim strSQL2 As String = ""

                If setMon <> "00" Then
                    '月指定
                    strSQL2 &= "SELECT (CONVERT(DATETIME, month + '/' + day)) as day ,  DATENAME(WEEKDAY,(CONVERT(DATETIME, month + '/' + day)) ) as youbi, "
                    strSQL2 &= "Customer_Visit, Call_Registerd, Repair_Completed,Goods_Delivered, Pending_Calls, Cancelled_Calls "
                    strSQL2 &= "FROM dbo.Activity_report WHERE Month = '" & Left(dtNow.ToShortDateString, 5) & setMon & "' AND location = '" & shipCode & "' "
                    strSQL2 &= "ORDER BY day;"
                Else
                    '日付指定
                    strSQL2 &= "SELECT (CONVERT(DATETIME, month + '/' + day)) as day ,  DATENAME(WEEKDAY,(CONVERT(DATETIME, month + '/' + day)) ) as youbi, "
                    strSQL2 &= "Customer_Visit, Call_Registerd, Repair_Completed,Goods_Delivered, Pending_Calls, Cancelled_Calls "
                    strSQL2 &= "FROM dbo.Activity_report WHERE location = '" & shipCode & "' "

                    If dateTo <> "" Then
                        If dateFrom <> "" Then
                            strSQL2 &= "AND (CONVERT(DATETIME, month + '/' + day)) <= CONVERT(DATETIME, '" & dateTo & "') "
                            strSQL2 &= "AND (CONVERT(DATETIME, month + '/' + day)) >= CONVERT(DATETIME, '" & dateFrom & "') "
                        Else
                            strSQL2 &= "AND (CONVERT(DATETIME, month + '/' + day)) <= CONVERT(DATETIME, '" & dateTo & "') "
                        End If
                    Else
                        If dateFrom <> "" Then
                            strSQL2 &= "AND (CONVERT(DATETIME, month + '/' + day)) >= CONVERT(DATETIME, '" & dateFrom & "') "
                        End If
                    End If

                    strSQL2 &= "ORDER BY day;"

                End If

                dsActivity_report = DBCommon.Get_DS(strSQL2, errFlg)

                If errFlg = 1 Then
                    Call showMsg("Failed to get Activity_report information.", "")
                    Exit Sub
                End If

                If dsActivity_report IsNot Nothing Then

                    If dsActivity_report.Tables(0).Rows.Count <> 0 Then

                        ReDim activeData(dsActivity_report.Tables(0).Rows.Count - 1)

                        Dim dr As DataRow

                        For i = 0 To dsActivity_report.Tables(0).Rows.Count - 1

                            dr = dsActivity_report.Tables(0).Rows(i)

                            Dim tmpDay As DateTime
                            Dim tmpMon As String
                            Dim tmpYear As String

                            If dr("day") IsNot DBNull.Value Then

                                '項目の日付をセット　例）2018/07/01は、01-Jul-2018　
                                tmpDay = dr("day")

                                'yyyy/mm/dd
                                activeData(i).day2 = tmpDay.ToShortDateString

                                'yyyy/mm/dd
                                activeData(i).day = tmpDay.ToShortDateString

                                'dd
                                activeData(i).day = activeData(i).day.Substring(8, 2)

                                'yyyy
                                tmpYear = activeData(i).day2.Substring(0, 4)

                                If setMonName <> "Select the month" Then

                                    '月指定は、ドロップリストで指定された月をセット
                                    'dd-mon-yyyy
                                    activeData(i).day &= "-" & Left(setMonName, 3) & "-" & tmpYear
                                Else

                                    '日付指定は、月を３文字列に変換してセット
                                    tmpMon = activeData(i).day2.Substring(5, 2)
                                    Select Case Convert.ToInt32(tmpMon)
                                        Case 1
                                            activeData(i).day &= "-" & "Jan" & "-" & tmpYear
                                        Case 2
                                            activeData(i).day &= "-" & "Feb" & "-" & tmpYear
                                        Case 3
                                            activeData(i).day &= "-" & "Mar" & "-" & tmpYear
                                        Case 4
                                            activeData(i).day &= "-" & "Apr" & "-" & tmpYear
                                        Case 5
                                            activeData(i).day &= "-" & "May" & "-" & tmpYear
                                        Case 6
                                            activeData(i).day &= "-" & "Jun" & "-" & tmpYear
                                        Case 7
                                            activeData(i).day &= "-" & "Jul" & "-" & tmpYear
                                        Case 8
                                            activeData(i).day &= "-" & "Aug" & "-" & tmpYear
                                        Case 9
                                            activeData(i).day &= "-" & "Sep" & "-" & tmpYear
                                        Case 10
                                            activeData(i).day &= "-" & "Oct" & "-" & tmpYear
                                        Case 11
                                            activeData(i).day &= "-" & "Nov" & "-" & tmpYear
                                        Case 12
                                            activeData(i).day &= "-" & "Dec" & "-" & tmpYear
                                    End Select
                                End If

                            End If

                            If dr("youbi") IsNot DBNull.Value Then
                                activeData(i).youbi = dr("youbi")
                            End If

                            If dr("Customer_Visit") IsNot DBNull.Value Then
                                activeData(i).Customer_Visit = dr("Customer_Visit")
                            End If

                            If dr("Call_Registerd") IsNot DBNull.Value Then
                                activeData(i).Call_Registerd = dr("Call_Registerd")
                            End If

                            If dr("Repair_Completed") IsNot DBNull.Value Then
                                activeData(i).Repair_Completed = dr("Repair_Completed")
                            End If

                            If dr("Goods_Delivered") IsNot DBNull.Value Then
                                activeData(i).Goods_Delivered = dr("Goods_Delivered")
                            End If

                            If dr("Pending_Calls") IsNot DBNull.Value Then
                                activeData(i).Pending_Calls = dr("Pending_Calls")
                            End If

                        Next i

                    End If

                Else
                    Call showMsg("There is no corresponding Activity_report information.", "")
                    Exit Sub
                End If

                '***出力順にセット***
                '■Activity_reportデータ出力

                '項目名設定
                Dim csvContents = New List(Of String)(New String() {"Activity_repot"})
                Dim rowWork1 As String = exportShipName & ","
                Dim rowWork2 As String = " ,"
                Dim rowWork3 As String = "Customer Visit①,"
                Dim rowWork4 As String = "Call Registered②,"
                Dim rowWork5 As String = "Repair Completed③,"
                Dim rowWork6 As String = "Goods Delivered④,"
                Dim rowWork7 As String = "Pending⑤,"

                Dim Total_Customer_Visit As Integer
                Dim Average_Customer_Visit As Integer
                Dim Total_Call_Registerd As Integer
                Dim Average_Call_Registerd As Integer
                Dim Total_Repair_Completed As Integer
                Dim Average_Repair_Completed As Integer
                Dim Total_Goods_Delivered As Integer
                Dim Average_Goods_Delivered As Integer
                Dim Total_Pending_Calls As Integer
                Dim Average_Pending_Calls As Integer

                '日付項目分処理
                For i = 0 To dsActivity_report.Tables(0).Rows.Count - 1
                    rowWork1 &= activeData(i).day & ","
                    rowWork2 &= activeData(i).youbi & ","
                    rowWork3 &= activeData(i).Customer_Visit & ","
                    rowWork4 &= activeData(i).Call_Registerd & ","
                    rowWork5 &= activeData(i).Repair_Completed & ","
                    rowWork6 &= activeData(i).Goods_Delivered & ","
                    rowWork7 &= activeData(i).Pending_Calls & ","
                    Total_Customer_Visit = Total_Customer_Visit + activeData(i).Customer_Visit
                    Total_Call_Registerd = Total_Call_Registerd + activeData(i).Call_Registerd
                    Total_Repair_Completed = Total_Repair_Completed + activeData(i).Repair_Completed
                    Total_Goods_Delivered = Total_Goods_Delivered + activeData(i).Goods_Delivered
                    Total_Pending_Calls = Total_Pending_Calls + activeData(i).Pending_Calls
                Next i

                Average_Customer_Visit = Total_Customer_Visit / dsActivity_report.Tables(0).Rows.Count
                Average_Call_Registerd = Total_Call_Registerd / dsActivity_report.Tables(0).Rows.Count
                Average_Repair_Completed = Total_Repair_Completed / dsActivity_report.Tables(0).Rows.Count
                Average_Goods_Delivered = Total_Goods_Delivered / dsActivity_report.Tables(0).Rows.Count
                Average_Pending_Calls = Total_Pending_Calls / dsActivity_report.Tables(0).Rows.Count

                '項目
                rowWork1 &= "Total,Average"

                '曜日
                rowWork2 &= " , "
                rowWork3 &= Total_Customer_Visit & "," & Average_Customer_Visit
                rowWork4 &= Total_Call_Registerd & "," & Average_Call_Registerd
                rowWork5 &= Total_Repair_Completed & "," & Average_Repair_Completed
                rowWork6 &= Total_Goods_Delivered & "," & Average_Goods_Delivered
                rowWork7 &= Total_Pending_Calls & "," & Average_Pending_Calls

                csvContents.Add(rowWork1)
                csvContents.Add(rowWork2)
                csvContents.Add(rowWork3)
                csvContents.Add(rowWork4)
                csvContents.Add(rowWork5)
                csvContents.Add(rowWork6)
                csvContents.Add(rowWork7)

                '■SC_DSR_infoデータ取得
                Dim totalDailyStatementRepartData() As Class_analysis.DAILYSTATEMENTREPART
                Dim dsSC_DSR_info As New DataSet
                Dim strSQL3 As String = ""

                If setMon <> "00" Then
                    '月指定
                    strSQL3 &= "SELECT * FROM dbo.SC_DSR_info "
                    strSQL3 &= "WHERE DELFG = 0 And Branch_name = '" & exportShipName & "' AND LEFT(CONVERT(VARCHAR, Billing_date,111),7) = '" & Left(dtNow.ToShortDateString, 5) & setMon & "';"
                Else
                    '日付指定
                    strSQL3 &= "SELECT * FROM dbo.SC_DSR_info "
                    strSQL3 &= "WHERE DELFG = 0 And Branch_name = '" & exportShipName & "' "

                    If dateTo <> "" Then
                        If dateFrom <> "" Then
                            strSQL3 &= "AND LEFT(CONVERT(VARCHAR, Billing_date, 111), 10) <= '" & dateTo & "' "
                            strSQL3 &= "AND LEFT(CONVERT(VARCHAR, Billing_date, 111), 10) >= '" & dateFrom & "';"
                        Else
                            strSQL3 &= "AND LEFT(CONVERT(VARCHAR, Billing_date, 111), 10) <= '" & dateTo & "';"
                        End If
                    Else
                        If dateFrom <> "" Then
                            strSQL3 &= "AND LEFT(CONVERT(VARCHAR, Billing_date, 111), 10) >= '" & dateFrom & "';"
                        End If
                    End If

                End If

                dsSC_DSR_info = DBCommon.Get_DS(strSQL3, errFlg)

                If errFlg = 1 Then
                    Call showMsg("Failed to get information on SC_DSR_info.", "")
                    Exit Sub
                End If

                If dsSC_DSR_info IsNot Nothing Then

                    If dsSC_DSR_info.Tables(0).Rows.Count <> 0 Then

                        ReDim totalDailyStatementRepartData(dsSC_DSR_info.Tables(0).Rows.Count - 1)

                        Dim dr As DataRow

                        For i = 0 To dsSC_DSR_info.Tables(0).Rows.Count - 1

                            dr = dsSC_DSR_info.Tables(0).Rows(i)

                            Dim tmpDay As DateTime

                            If dr("Billing_date") IsNot DBNull.Value Then
                                tmpDay = dr("Billing_date")
                                totalDailyStatementRepartData(i).Billing_date = tmpDay.ToShortDateString
                            End If

                            '①
                            If dr("IW_goods_total") IsNot DBNull.Value Then
                                totalDailyStatementRepartData(i).IW_goods_total = dr("IW_goods_total")
                            End If

                            '②
                            If dr("OW_goods_total") IsNot DBNull.Value Then
                                totalDailyStatementRepartData(i).OW_goods_total = dr("OW_goods_total")
                            End If

                            '③
                            If dr("IW_Labor_total") IsNot DBNull.Value Then
                                totalDailyStatementRepartData(i).IW_Labor = dr("IW_Labor_total")
                            End If

                            '④
                            If dr("IW_Parts_total") IsNot DBNull.Value Then
                                totalDailyStatementRepartData(i).IW_Parts = dr("IW_Parts_total")
                            End If

                            '⑤
                            If dr("IW_Tax_total") IsNot DBNull.Value Then
                                totalDailyStatementRepartData(i).IW_Tax = dr("IW_Tax_total")
                            End If

                            '⑥
                            If dr("IW_Total_total") IsNot DBNull.Value Then
                                totalDailyStatementRepartData(i).IW_total = dr("IW_Total_total")
                            End If

                            '⑦
                            If dr("OW_Labor_total") IsNot DBNull.Value Then
                                totalDailyStatementRepartData(i).OW_Labor = dr("OW_Labor_total")
                            End If

                            '⑧
                            If dr("OW_Parts_total") IsNot DBNull.Value Then
                                totalDailyStatementRepartData(i).OW_Parts = dr("OW_Parts_total")
                            End If

                            '⑨
                            If dr("OW_Tax_total") IsNot DBNull.Value Then
                                totalDailyStatementRepartData(i).OW_Tax = dr("OW_Tax_total")
                            End If

                            '⑩
                            If dr("OW_Total_total") IsNot DBNull.Value Then
                                totalDailyStatementRepartData(i).OW_total = dr("OW_Total_total")
                            End If

                        Next i

                    End If

                    '■dailystatementrepartデータ出力
                    '項目名設定
                    Dim rowWork8 As String = "DailyStatementRepart,"
                    Dim rowWork9 As String = "Warranty (Number),"        '①+②
                    Dim rowWork10 As String = "In Warranty (Number),"   '①
                    Dim rowWork11 As String = "Out Warranty (Number),"   '②
                    Dim rowWork12 As String = "In Warranty (Amount),"    '③+④　
                    Dim rowWork13 As String = "In Warranty (Labour),"     '③
                    Dim rowWork14 As String = "In Warranty (Parts),"      '④
                    Dim rowWork15 As String = "Tax In Warranty,"     '⑤
                    Dim rowWork16 As String = "Total Amount In Warranty," '⑥=③+④+⑤
                    Dim rowWork17 As String = "Out Warranty (Amount),"    '⑦+⑧
                    Dim rowWork18 As String = "Out Warranty (Labour),"    '⑦
                    Dim rowWork19 As String = "Out Warranty (Parts),"     '⑧
                    Dim rowWork20 As String = "Tax Out Warranty,"         '⑨
                    Dim rowWork21 As String = "Total Amount Out Warranty," '⑩=⑦+⑧+⑨
                    Dim rowWork22 As String = "Revenue without Tax,"   '③+④+⑦+⑧
                    Dim rowWork23 As String = "Cost of Revenue (Asumption)"

                    'count集計用
                    Dim Total_IW_OW_goods_total As Integer
                    Dim Average_IW_OW_goods_total As Integer
                    Dim Total_IW_goods_total As Integer
                    Dim Total_OW_goods_total As Integer
                    Dim Average_IW_goods_total As Integer
                    Dim Average_OW_goods_total As Integer

                    'money集計用
                    Dim Total_IW_Labor_Parts As Decimal
                    Dim Total_IW_Labor As Decimal
                    Dim Total_IW_Parts As Decimal
                    Dim Total_IW_Tax As Decimal
                    Dim Total_IW_total As Decimal

                    Dim Total_OW_Labor_Parts As Decimal
                    Dim Total_OW_Labor As Decimal
                    Dim Total_OW_Parts As Decimal
                    Dim Total_OW_Tax As Decimal
                    Dim Total_OW_total As Decimal

                    Dim Total_Revenue_without_Tax As Decimal

                    Dim Average_IW_Labor_Parts As Decimal
                    Dim Average_IW_Labor As Decimal
                    Dim Average_IW_Parts As Decimal
                    Dim Average_IW_Tax As Decimal
                    Dim Average_IW_total As Decimal

                    Dim Average_OW_Labor_Parts As Decimal
                    Dim Average_OW_Labor As Decimal
                    Dim Average_OW_Parts As Decimal
                    Dim Average_OW_Tax As Decimal
                    Dim Average_OW_total As Decimal

                    Dim Average_Revenue_without_Tax As Decimal
                    Dim dailyCount As Integer

                    '日付項目分処理
                    For i = 0 To dsActivity_report.Tables(0).Rows.Count - 1

                        For j = 0 To dsSC_DSR_info.Tables(0).Rows.Count - 1

                            If activeData(i).day2 = totalDailyStatementRepartData(j).Billing_date Then

                                dailyCount = dailyCount + 1

                                'count
                                rowWork9 &= totalDailyStatementRepartData(j).IW_goods_total + totalDailyStatementRepartData(j).OW_goods_total
                                rowWork10 &= totalDailyStatementRepartData(j).IW_goods_total
                                rowWork11 &= totalDailyStatementRepartData(j).OW_goods_total

                                'money
                                rowWork12 &= totalDailyStatementRepartData(j).IW_Labor + totalDailyStatementRepartData(j).IW_Parts
                                rowWork13 &= totalDailyStatementRepartData(j).IW_Labor
                                rowWork14 &= totalDailyStatementRepartData(j).IW_Parts
                                rowWork15 &= totalDailyStatementRepartData(j).IW_Tax
                                rowWork16 &= totalDailyStatementRepartData(j).IW_total
                                rowWork17 &= totalDailyStatementRepartData(j).OW_Labor + totalDailyStatementRepartData(j).OW_Parts
                                rowWork18 &= totalDailyStatementRepartData(j).OW_Labor
                                rowWork19 &= totalDailyStatementRepartData(j).OW_Parts
                                rowWork20 &= totalDailyStatementRepartData(j).OW_Tax
                                rowWork21 &= totalDailyStatementRepartData(j).OW_total
                                rowWork22 &= totalDailyStatementRepartData(j).IW_Labor + totalDailyStatementRepartData(j).IW_Parts + totalDailyStatementRepartData(j).OW_Labor + totalDailyStatementRepartData(j).OW_Parts

                                'count合計
                                Total_IW_OW_goods_total = Total_IW_OW_goods_total + (totalDailyStatementRepartData(j).IW_goods_total + totalDailyStatementRepartData(j).OW_goods_total)
                                Total_IW_goods_total = Total_IW_goods_total + totalDailyStatementRepartData(j).IW_goods_total
                                Total_OW_goods_total = Total_OW_goods_total + totalDailyStatementRepartData(j).OW_goods_total

                                'money合計
                                Total_IW_Labor_Parts = Total_IW_Labor_Parts + (totalDailyStatementRepartData(j).IW_Labor + totalDailyStatementRepartData(j).IW_Parts)
                                Total_IW_Labor = Total_IW_Labor + totalDailyStatementRepartData(j).IW_Labor
                                Total_IW_Parts = Total_IW_Parts + totalDailyStatementRepartData(j).IW_Parts
                                Total_IW_Tax = Total_IW_Tax + totalDailyStatementRepartData(j).IW_Tax
                                Total_IW_total = Total_IW_total + totalDailyStatementRepartData(j).IW_total

                                Total_OW_Labor_Parts = Total_OW_Labor_Parts + (totalDailyStatementRepartData(j).OW_Labor + totalDailyStatementRepartData(j).OW_Parts)
                                Total_OW_Labor = Total_OW_Labor + totalDailyStatementRepartData(j).OW_Labor
                                Total_OW_Parts = Total_OW_Parts + totalDailyStatementRepartData(j).OW_Parts
                                Total_OW_Tax = Total_OW_Tax + totalDailyStatementRepartData(j).OW_Tax
                                Total_OW_total = Total_OW_total + totalDailyStatementRepartData(j).OW_total
                                Total_Revenue_without_Tax = Total_Revenue_without_Tax + (totalDailyStatementRepartData(j).IW_Labor + totalDailyStatementRepartData(j).IW_Parts + totalDailyStatementRepartData(j).OW_Labor + totalDailyStatementRepartData(j).OW_Parts)

                            End If

                        Next j

                        rowWork9 &= ","
                        rowWork10 &= ","
                        rowWork11 &= ","
                        rowWork12 &= ","
                        rowWork13 &= ","
                        rowWork14 &= ","
                        rowWork15 &= ","
                        rowWork16 &= ","
                        rowWork17 &= ","
                        rowWork18 &= ","
                        rowWork19 &= ","
                        rowWork20 &= ","
                        rowWork21 &= ","
                        rowWork22 &= ","

                    Next i

                    'count平均
                    Average_IW_OW_goods_total = Total_IW_OW_goods_total / dailyCount
                    Average_IW_goods_total = Total_IW_goods_total / dailyCount
                    Average_OW_goods_total = Total_OW_goods_total / dailyCount

                    'money平均
                    Average_IW_Labor_Parts = Total_IW_Labor_Parts / dailyCount
                    Average_IW_Labor = Total_IW_Labor / dailyCount
                    Average_IW_Parts = Total_IW_Parts / dailyCount
                    Average_IW_Tax = Total_IW_Tax / dailyCount
                    Average_IW_total = Total_IW_total / dailyCount

                    Average_OW_Labor_Parts = Total_OW_Labor_Parts / dailyCount
                    Average_OW_Labor = Total_OW_Labor / dailyCount
                    Average_OW_Parts = Total_OW_Parts / dailyCount
                    Average_OW_Tax = Total_OW_Tax / dailyCount
                    Average_OW_total = Total_OW_total / dailyCount

                    Average_Revenue_without_Tax = Total_Revenue_without_Tax / dailyCount

                    rowWork9 &= Total_IW_OW_goods_total & "," & Average_IW_OW_goods_total
                    rowWork10 &= Total_IW_goods_total & "," & Average_IW_goods_total
                    rowWork11 &= Total_OW_goods_total & "," & Average_OW_goods_total
                    rowWork12 &= Total_IW_Labor_Parts & "," & Average_IW_Labor_Parts
                    rowWork13 &= Total_IW_Labor & "," & Average_IW_Labor
                    rowWork14 &= Total_IW_Parts & "," & Average_IW_Parts
                    rowWork15 &= Total_IW_Tax & "," & Average_IW_Tax
                    rowWork16 &= Total_IW_total & "," & Average_IW_total
                    rowWork17 &= Total_OW_Labor_Parts & "," & Average_OW_Labor_Parts
                    rowWork18 &= Total_OW_Labor & "," & Average_OW_Labor
                    rowWork19 &= Total_OW_Parts & "," & Average_OW_Parts
                    rowWork20 &= Total_OW_Tax & "," & Average_OW_Tax
                    rowWork21 &= Total_OW_total & "," & Average_OW_total
                    rowWork22 &= Total_Revenue_without_Tax & "," & Average_Revenue_without_Tax

                    csvContents.Add(rowWork8)
                    csvContents.Add(rowWork9)
                    csvContents.Add(rowWork10)
                    csvContents.Add(rowWork11)
                    csvContents.Add(rowWork12)
                    csvContents.Add(rowWork13)
                    csvContents.Add(rowWork14)
                    csvContents.Add(rowWork15)
                    csvContents.Add(rowWork16)
                    csvContents.Add(rowWork17)
                    csvContents.Add(rowWork18)
                    csvContents.Add(rowWork19)
                    csvContents.Add(rowWork20)
                    csvContents.Add(rowWork21)
                    csvContents.Add(rowWork22)
                    csvContents.Add(rowWork23)

                Else
                    csvContents.Add("There is no corresponding SC_DSR_info information.")
                End If

                'ファイル名、パスの設定
                Dim csvFileName As String

                dateFrom = Replace(dateFrom, "/", "")
                dateTo = Replace(dateTo, "/", "")

                'exportFile名の頭、数値を除く
                exportFile = Right(exportFile, exportFile.Length - 2)

                If setMon = "00" Then
                    If dateTo <> "" And dateFrom <> "" Then
                        csvFileName = exportFile & "_ " & exportShipName & "_" & dateFrom & "_" & dateTo & ".csv"
                    Else
                        If dateTo <> "" Then
                            csvFileName = exportFile & "_ " & exportShipName & "_" & dateTo & ".csv"
                        End If
                        If dateFrom <> "" Then
                            csvFileName = exportFile & "_ " & exportShipName & "_" & dateFrom & ".csv"
                        End If
                    End If
                Else
                    '月指定のとき
                    csvFileName = exportFile & "_ " & exportShipName & "_" & dtNow.ToString("yyyy") & "_" & setMon & ".csv"
                End If

                outputPath = clsSet.CsvFilePass & csvFileName

                Using writer As New StreamWriter(outputPath, False, Encoding.Default)
                    writer.WriteLine(String.Join(Environment.NewLine, csvContents))
                End Using

                'ファイルの内容をバイト配列にすべて読み込む 
                Dim Buffer As Byte() = System.IO.File.ReadAllBytes(outputPath)

                'サーバに保存されたCSVファイルを削除
                '※Response.End以降、ファイル削除処理ができないため、ファイルのダウンロードではなく、一旦ファイルの内容を
                '上記、Bufferに保存し、ダウンロード処理を行う。

                If outputPath <> "" Then
                    If System.IO.File.Exists(outputPath) = True Then
                        System.IO.File.Delete(outputPath)
                    End If
                End If

                ' ダウンロード
                Response.ContentType = "application/text/csv"
                Response.AddHeader("Content-Disposition", "attachment; filename=" & csvFileName)
                Response.Flush()
                'Response.Write("<b>File Contents: </b>")
                Response.BinaryWrite(Buffer)
                'Response.WriteFile(outputPath)
                Response.End()

            Catch ex As System.Threading.ThreadAbortException
                'Response.End()の呼び出しによりエラーメッセージを出力しないようにする

            Catch ex As Exception
                Call showMsg("CSV output processing failed.", "")
            End Try

        ElseIf kindExport = 2 Then

            'SalesRegister_OOW
            '部品情報を登録して、出力対象情報を取得
            Dim dsWtyExcel As New DataSet
            Dim wtyData() As Class_analysis.WTY_EXCEL
            Call clsSet.setWtyExcelDown(dsWtyExcel, wtyData, userid, userName, shipCode, exportShipName, errFlg, setMon, dateFrom, dateTo)

            If dsWtyExcel Is Nothing Then
                Call showMsg("There is no output target of SalesRegister_OOW.", "")
                Exit Sub
            End If

            If errFlg = 1 Then
                Call showMsg("SalesRegister_OOW information acquisition failed.", "")
                Exit Sub
            End If

            Try
                'ファイル名、パスの設定
                Dim csvFileName As String

                dateFrom = Replace(dateFrom, "/", "")
                dateTo = Replace(dateTo, "/", "")

                'exportFile名の頭、数値を除く
                exportFile = Right(exportFile, exportFile.Length - 2)

                If setMon = "00" Then
                    If dateTo <> "" And dateFrom <> "" Then
                        csvFileName = exportShipName & "_Sales_OOW" & "_" & dateFrom & "_" & dateTo & ".csv"
                    Else
                        If dateTo <> "" Then
                            csvFileName = exportShipName & "_Sales_OOW" & "_" & dateTo & ".csv"
                        End If
                        If dateFrom <> "" Then
                            csvFileName = exportShipName & "_Sales_OOW" & "_" & dateFrom & ".csv"
                        End If
                    End If
                Else
                    '月指定のとき
                    csvFileName = exportShipName & "_Sales_OOW" & "_" & dtNow.ToString("yyyy") & "_" & setMon & ".csv"
                End If

                outputPath = clsSet.CsvFilePass & csvFileName

                '項目名設定
                Dim csvContents = New List(Of String)(New String() {exportShipName & "-Sales Register Out warranty"})
                Dim rowWork1 As String = "ASC Code,Branch Code,ASC Claim No,Samsung Claim No,Service Type,Consumer Name,Consumer Addr1,Consumer Addr2,Consumer Telephone,Consumer Fax,Postal Code,Model,Serial No,IMEI No,Defect Type,Condition,Symptom,Defect Code,Repair Code,Defect Desc,Repair Description,Purchase Date,Repair Received Date,Completed Date,Delivery Date,Production Date,Labor Amount Ⅰ,Parts Amount Ⅰ,Tax,Total Invoice Amount Ⅰ,Remark,Tr No,Tr Type,Status,Engineer,Collection Point,Collection Point Name,Location-1,Part-1,Qty-1,Unit Price-1,Doc Num-1,Matrial Serial-1,Location-2,Part-2,Qty-2,Unit Price-2,Doc Num-2,Matrial Serial-2,Location-3,Part-3,Qty-3,Unit Price-3,Doc Num-3,Matrial Serial-3,Location-4,Part-4,Qty-4,Unit Price-4,Doc Num-4,Matrial Serial-4,Location-5,Part-5,Qty-5,Unit Price-5,Doc Num-5,Matrial Serial-5,Location-6,Part-6,Qty-6,Unit Price-6,Doc Num-6,Matrial Serial-6,Location-7,Part-7,Qty-7,Unit Price-7,Doc Num-7,Matrial Serial-7,Location-8,Part-8,Qty-8,Unit Price-8,Doc Num-8,Matrial Serial-8,Location-9,Part-9,Qty-9,Unit Price-9,Doc Num-9,Matrial Serial-9,Location-10,Part-10,Qty-10,Unit Price-10,Doc Num-10,Matrial Serial-10,Location-11,Part-11,Qty-11,Unit Price-11,Doc Num-11,Matrial Serial-11,Location-12,Part-12,Qty-12,Unit Price-12,Doc Num-12,Matrial Serial-12,Location-13,Part-13,Qty-13,Unit Price-13,Doc Num-13,Matrial Serial-13,Location-14,Part-14,Qty-14,Unit Price-14,Doc Num-14,Matrial Serial-14,Location-15,Part-15,Qty-15,Unit Price-15,Doc Num-15,Matrial Serial-15"

                csvContents.Add(rowWork1)

                For i = 0 To wtyData.Length - 1
                    csvContents.Add(wtyData(i).ASC_Code & "," & wtyData(i).Branch_Code & "," & wtyData(i).ASC_Claim_No & "," & wtyData(i).Samsung_Claim_No & "," & wtyData(i).Service_Type & "," & wtyData(i).Consumer_Name & "," & wtyData(i).Consumer_Addr1 & "," & wtyData(i).Consumer_Addr2 & "," & wtyData(i).Consumer_Telephone & "," & wtyData(i).Consumer_Fax & "," & wtyData(i).Postal_Code & "," & wtyData(i).Model & "," & wtyData(i).Serial_No & "," & wtyData(i).IMEI_No & "," & wtyData(i).Defect_Type & "," & wtyData(i).Condition & "," & wtyData(i).Symptom & "," & wtyData(i).Defect_Code & "," & wtyData(i).Repair_Code & "," & wtyData(i).Defect_Desc & "," & wtyData(i).Repair_Description & "," & wtyData(i).Purchase_Date & "," & wtyData(i).Repair_Received_Date & "," & wtyData(i).Completed_Date & "," & wtyData(i).Delivery_Date & "," & wtyData(i).Production_Date & "," & wtyData(i).Labor_Amount & "," & wtyData(i).Parts_Amount & "," & wtyData(i).Tax & "," & wtyData(i).Total_Invoice_Amount & "," & wtyData(i).Remark & "," & wtyData(i).Tr_No & "," & wtyData(i).Tr_Type & "," & wtyData(i).Status & "," & wtyData(i).Engineer & "," & wtyData(i).Collection_Point & "," & wtyData(i).Collection_Point_Name & "," & wtyData(i).Location_1 & "," & wtyData(i).Part_1 & "," & wtyData(i).Qty_1 & "," & wtyData(i).Unit_Price_1 & "," & wtyData(i).Doc_Num_1 & "," & wtyData(i).Matrial_Serial_1 & "," & wtyData(i).Location_2 & "," & wtyData(i).Part_2 & "," & wtyData(i).Qty_2 & "," & wtyData(i).Unit_Price_2 & "," & wtyData(i).Doc_Num_2 & "," & wtyData(i).Matrial_Serial_2 & "," & wtyData(i).Location_3 & "," & wtyData(i).Part_3 & "," & wtyData(i).Qty_3 & "," & wtyData(i).Unit_Price_3 & "," & wtyData(i).Doc_Num_3 & "," & wtyData(i).Matrial_Serial_3 & "," & wtyData(i).Location_4 & "," & wtyData(i).Part_4 & "," & wtyData(i).Qty_4 & "," & wtyData(i).Unit_Price_4 & "," & wtyData(i).Doc_Num_4 & "," & wtyData(i).Matrial_Serial_4 & "," & wtyData(i).Location_5 & "," & wtyData(i).Part_5 & "," & wtyData(i).Qty_5 & "," & wtyData(i).Unit_Price_5 & "," & wtyData(i).Doc_Num_5 & "," & wtyData(i).Matrial_Serial_5 & "," & wtyData(i).Location_6 & "," & wtyData(i).Part_6 & "," & wtyData(i).Qty_6 & "," & wtyData(i).Unit_Price_6 & "," & wtyData(i).Doc_Num_6 & "," & wtyData(i).Matrial_Serial_6 & "," & wtyData(i).Location_7 & "," & wtyData(i).Part_7 & "," & wtyData(i).Qty_7 & "," & wtyData(i).Unit_Price_7 & "," & wtyData(i).Doc_Num_7 & "," & wtyData(i).Matrial_Serial_7 & "," & wtyData(i).Location_8 & "," & wtyData(i).Part_8 & "," & wtyData(i).Qty_8 & "," & wtyData(i).Unit_Price_8 & "," & wtyData(i).Doc_Num_8 & "," & wtyData(i).Matrial_Serial_8 & "," & wtyData(i).Location_9 & "," & wtyData(i).Part_9 & "," & wtyData(i).Qty_9 & "," & wtyData(i).Unit_Price_9 & "," & wtyData(i).Doc_Num_9 & "," & wtyData(i).Matrial_Serial_9 & "," & wtyData(i).Location_10 & "," & wtyData(i).Part_10 & "," & wtyData(i).Qty_10 & "," & wtyData(i).Unit_Price_10 & "," & wtyData(i).Doc_Num_10 & "," & wtyData(i).Matrial_Serial_10 & "," & wtyData(i).Location_11 & "," & wtyData(i).Part_11 & "," & wtyData(i).Qty_11 & "," & wtyData(i).Unit_Price_11 & "," & wtyData(i).Doc_Num_11 & "," & wtyData(i).Matrial_Serial_11 & "," & wtyData(i).Location_12 & "," & wtyData(i).Part_12 & "," & wtyData(i).Qty_12 & "," & wtyData(i).Unit_Price_12 & "," & wtyData(i).Doc_Num_12 & "," & wtyData(i).Matrial_Serial_12 & "," & wtyData(i).Location_13 & "," & wtyData(i).Part_13 & "," & wtyData(i).Qty_13 & "," & wtyData(i).Unit_Price_13 & "," & wtyData(i).Doc_Num_13 & "," & wtyData(i).Matrial_Serial_13 & "," & wtyData(i).Location_14 & "," & wtyData(i).Part_14 & "," & wtyData(i).Qty_14 & "," & wtyData(i).Unit_Price_14 & "," & wtyData(i).Doc_Num_14 & "," & wtyData(i).Matrial_Serial_14 & "," & wtyData(i).Location_15 & "," & wtyData(i).Part_15 & "," & wtyData(i).Qty_15 & "," & wtyData(i).Unit_Price_15 & "," & wtyData(i).Doc_Num_15 & "," & wtyData(i).Matrial_Serial_15)
                Next i

                Using writer As New StreamWriter(outputPath, False, Encoding.Default)
                    writer.WriteLine(String.Join(Environment.NewLine, csvContents))
                End Using

                'ファイルの内容をバイト配列にすべて読み込む 
                Dim Buffer As Byte() = System.IO.File.ReadAllBytes(outputPath)

                'サーバに保存されたCSVファイルを削除
                '※Response.End以降、ファイル削除処理ができないため、ファイルのダウンロードではなく、一旦ファイルの内容を
                '上記、Bufferに保存し、ダウンロード処理を行う。

                If outputPath <> "" Then
                    If System.IO.File.Exists(outputPath) = True Then
                        System.IO.File.Delete(outputPath)
                    End If
                End If

                ' ダウンロード
                Response.ContentType = "application/text/csv"
                Response.AddHeader("Content-Disposition", "attachment; filename=" & csvFileName)
                Response.Flush()
                'Response.Write("<b>File Contents: </b>")
                Response.BinaryWrite(Buffer)
                'Response.WriteFile(outputPath)
                Response.End()

            Catch ex As System.Threading.ThreadAbortException
                'Response.End()の呼び出しによりエラーメッセージを出力しないようにする

            Catch ex As Exception
                Call showMsg("CSV output processing failed.", "")
            End Try

        ElseIf kindExport = 3 Then

            'Sales Invoice to samsung C
            '出力対象情報を取得
            Dim invoiceData() As Class_analysis.INVOICE
            Call clsSet.setInvoice(invoiceData, exportShipName, errFlg, setMon, "C")

            If invoiceData Is Nothing Then
                Call showMsg("Sales Invoice to samsung There is no output target of C.", "")
                Exit Sub
            End If

            If errFlg = 1 Then
                Call showMsg("InvoiceUpdate information acquisition failed.", "")
                Exit Sub
            End If

            Try
                'ファイル名、パスの設定

                'exportFile名の頭、数値を除く
                exportFile = Right(exportFile, exportFile.Length - 2)

                Dim csvFileName As String = "Sales_Inwarranty_C_" & exportShipName & "_" & dtNow.ToString("yyyy") & "_" & setMon & ".csv"

                outputPath = clsSet.CsvFilePass & csvFileName

                '項目名設定
                Dim rowWork1 As String = "Samsung Ref No,Your Ref No,Model,Serial,Product,Service,Defect Code,Currency,Invoice,Labor,Parts,Freight,Other,Tax,Parts_Invoice_No,Labpr_Invoice_No,Invoice Date"
                Dim csvContents = New List(Of String)(New String() {rowWork1})

                For i = 0 To invoiceData.Length - 1
                    csvContents.Add(invoiceData(i).samsung_Ref_No & "," & invoiceData(i).Your_Ref_No & "," & invoiceData(i).Model & "," & invoiceData(i).Serial & "," & invoiceData(i).Product & "," & invoiceData(i).Serivice & "," & invoiceData(i).Defect_Code & "," & invoiceData(i).Currency & "," & invoiceData(i).Invoice & "," & invoiceData(i).Labor & "," & invoiceData(i).Parts & "," & invoiceData(i).Felight & "," & invoiceData(i).Other & "," & invoiceData(i).Tax & "," & invoiceData(i).Parts_invoice_No & "," & invoiceData(i).Labor_Invoice_No & "," & invoiceData(i).Invoice_date)
                Next i

                Using writer As New StreamWriter(outputPath, False, Encoding.Default)
                    writer.WriteLine(String.Join(Environment.NewLine, csvContents))
                End Using

                'ファイルの内容をバイト配列にすべて読み込む 
                Dim Buffer As Byte() = System.IO.File.ReadAllBytes(outputPath)

                'サーバに保存されたCSVファイルを削除
                '※Response.End以降、ファイル削除処理ができないため、ファイルのダウンロードではなく、一旦ファイルの内容を
                '上記、Bufferに保存し、ダウンロード処理を行う。

                If outputPath <> "" Then
                    If System.IO.File.Exists(outputPath) = True Then
                        System.IO.File.Delete(outputPath)
                    End If
                End If

                ' ダウンロード
                Response.ContentType = "application/text/csv"
                Response.AddHeader("Content-Disposition", "attachment; filename=" & csvFileName)
                Response.Flush()
                'Response.Write("<b>File Contents: </b>")
                Response.BinaryWrite(Buffer)
                'Response.WriteFile(outputPath)
                Response.End()

            Catch ex As System.Threading.ThreadAbortException
                'Response.End()の呼び出しによりエラーメッセージを出力しないようにする

            Catch ex As Exception
                Call showMsg("CSV output processing failed.", "")
            End Try

        ElseIf kindExport = 4 Then

            'Sales Invoiec to samsung EXC
            '出力対象情報を取得
            Dim invoiceData() As Class_analysis.INVOICE
            Call clsSet.setInvoice(invoiceData, exportShipName, errFlg, setMon, "EXC")

            If invoiceData Is Nothing Then
                Call showMsg("Sales Invoiec to samsung There is no output target of ", "")
                Exit Sub
            End If

            If errFlg = 1 Then
                Call showMsg("InvoiceUpdate information acquisition failed.", "")
                Exit Sub
            End If

            Try
                'ファイル名、パスの設定

                'exportFile名の頭、数値を除く
                exportFile = Right(exportFile, exportFile.Length - 2)

                Dim csvFileName As String = "Sales_Inwarranty_EXC_" & exportShipName & "_" & dtNow.ToString("yyyy") & "_" & setMon & ".csv"
                outputPath = clsSet.CsvFilePass & csvFileName


                '項目名設定
                Dim rowWork1 As String = "Samsung Ref No,Your Ref No,Model,Serial,Product,Service,Defect Code,Currency,Invoice,Labor,Parts,Freight,Other,Tax,Parts_Invoice_No,Labpr_Invoice_No,Invoice Date"
                Dim csvContents = New List(Of String)(New String() {rowWork1})

                For i = 0 To invoiceData.Length - 1
                    csvContents.Add(invoiceData(i).samsung_Ref_No & "," & invoiceData(i).Your_Ref_No & "," & invoiceData(i).Model & "," & invoiceData(i).Serial & "," & invoiceData(i).Product & "," & invoiceData(i).Serivice & "," & invoiceData(i).Defect_Code & "," & invoiceData(i).Currency & "," & invoiceData(i).Invoice & "," & invoiceData(i).Labor & "," & invoiceData(i).Parts & "," & invoiceData(i).Felight & "," & invoiceData(i).Other & "," & invoiceData(i).Tax & "," & invoiceData(i).Parts_invoice_No & "," & invoiceData(i).Labor_Invoice_No & "," & invoiceData(i).Invoice_date)
                Next i

                Using writer As New StreamWriter(outputPath, False, Encoding.Default)
                    writer.WriteLine(String.Join(Environment.NewLine, csvContents))
                End Using

                'ファイルの内容をバイト配列にすべて読み込む 
                Dim Buffer As Byte() = System.IO.File.ReadAllBytes(outputPath)

                'サーバに保存されたCSVファイルを削除
                '※Response.End以降、ファイル削除処理ができないため、ファイルのダウンロードではなく、一旦ファイルの内容を
                '上記、Bufferに保存し、ダウンロード処理を行う。

                If outputPath <> "" Then
                    If System.IO.File.Exists(outputPath) = True Then
                        System.IO.File.Delete(outputPath)
                    End If
                End If

                ' ダウンロード
                Response.ContentType = "application/text/csv"
                Response.AddHeader("Content-Disposition", "attachment; filename=" & csvFileName)
                Response.Flush()
                'Response.Write("<b>File Contents: </b>")
                Response.BinaryWrite(Buffer)
                'Response.WriteFile(outputPath)
                Response.End()

            Catch ex As System.Threading.ThreadAbortException
                'Response.End()の呼び出しによりエラーメッセージを出力しないようにする

            Catch ex As Exception
                Call showMsg("CSV output processing failed.", "")
            End Try

        ElseIf kindExport = 8 Then

            'Purchase_Register
            '出力対象情報を取得
            Dim purchaseData2() As Class_analysis.BILLINGINFODETAIL  '出力用　
            Call clsSet.setPurchaseRegister(purchaseData2, exportShipName, shipCode, errFlg, setMon, dateFrom, dateTo)

            If purchaseData2 Is Nothing Then
                Call showMsg("There is no output target of Purchase_Register.", "")
                Exit Sub
            End If

            If errFlg = 1 Then
                Call showMsg("Failed to get good_recived information.", "")
                Exit Sub
            End If

            Try
                'ファイル名、パスの設定
                Dim csvFileName As String

                dateFrom = Replace(dateFrom, "/", "")
                dateTo = Replace(dateTo, "/", "")

                'exportFile名の頭、数値を除く
                exportFile = Right(exportFile, exportFile.Length - 2)

                If setMon = "00" Then
                    If dateTo <> "" And dateFrom <> "" Then
                        csvFileName = "Purchase_Register_" & exportShipName & "_" & dateFrom & "_" & dateTo & ".csv"
                    Else
                        If dateTo <> "" Then
                            csvFileName = "Purchase_Register_" & exportShipName & "_" & dateTo & ".csv"
                        End If
                        If dateFrom <> "" Then
                            csvFileName = "Purchase_Register_" & exportShipName & "_" & dateFrom & ".csv"
                        End If
                    End If
                Else
                    '月指定のとき
                    csvFileName = "Purchase_Register_" & exportShipName & "_" & dtNow.ToString("yyyy") & "_" & setMon & ".csv"
                End If

                outputPath = clsSet.CsvFilePass & csvFileName

                '項目名設定
                Dim rowWork1 As String = "Ship-to-Branch-Code,Ship-to-Branch,Invoice Date,G/R Date,Invoice No,Local Invoice No,Delivery No,Items,Amount,SGST / UTGST,CGST,IGST,Cess,Tax,Total,GR Status"

                Dim csvContents = New List(Of String)(New String() {rowWork1})

                For i = 0 To purchaseData2.Length - 1
                    csvContents.Add(purchaseData2(i).Branch_Code & "," & purchaseData2(i).Ship_Branch & "," & purchaseData2(i).delivery_date & "," & purchaseData2(i).GR_Date & "," & purchaseData2(i).Invoice_No & "," & purchaseData2(i).local_invoice_No & "," & purchaseData2(i).Delivery_No & "," & purchaseData2(i).Items & "," & purchaseData2(i).SumAmount & "," & purchaseData2(i).SumSGST_UTGST & "," & purchaseData2(i).SumCGST & "," & purchaseData2(i).SumIGST & "," & purchaseData2(i).SumCess & "," & purchaseData2(i).SumTax & "," & purchaseData2(i).SumTotal & "," & purchaseData2(i).GR_Status)
                Next i

                Using writer As New StreamWriter(outputPath, False, Encoding.Default)
                    writer.WriteLine(String.Join(Environment.NewLine, csvContents))
                End Using

                'ファイルの内容をバイト配列にすべて読み込む 
                Dim Buffer As Byte() = System.IO.File.ReadAllBytes(outputPath)

                'サーバに保存されたCSVファイルを削除
                '※Response.End以降、ファイル削除処理ができないため、ファイルのダウンロードではなく、一旦ファイルの内容を
                '上記、Bufferに保存し、ダウンロード処理を行う。

                If outputPath <> "" Then
                    If System.IO.File.Exists(outputPath) = True Then
                        System.IO.File.Delete(outputPath)
                    End If
                End If

                ' ダウンロード
                Response.ContentType = "application/text/csv"
                Response.AddHeader("Content-Disposition", "attachment; filename=" & csvFileName)
                Response.Flush()
                'Response.Write("<b>File Contents: </b>")
                Response.BinaryWrite(Buffer)
                'Response.WriteFile(outputPath)
                Response.End()

            Catch ex As System.Threading.ThreadAbortException
                'Response.End()の呼び出しによりエラーメッセージを出力しないようにする

            Catch ex As Exception
                Call showMsg("CSV output processing failed.", "")
            End Try

        ElseIf kindExport = 5 Then

            'SalesRegister_IW
            '出力対象情報を取得
            Dim wtyDataIW() As Class_analysis.WTY_EXCEL

            Call clsSet.setSalesRegister_IW_OTHER(wtyDataIW, exportShipName, shipCode, userid, userName, errFlg, setMon, dateFrom, dateTo, "IW")

            If wtyDataIW Is Nothing Then
                Call showMsg("There is no output target of SalesRegister_IW.", "")
                Exit Sub
            End If

            If errFlg = 1 Then
                Call showMsg("Failed to get information on SalesRegister_IW.", "")
                Exit Sub
            End If

            Try
                'ファイル名、パスの設定
                Dim csvFileName As String

                dateFrom = Replace(dateFrom, "/", "")
                dateTo = Replace(dateTo, "/", "")

                'exportFile名の頭、数値を除く
                exportFile = Right(exportFile, exportFile.Length - 2)

                If setMon = "00" Then
                    If dateTo <> "" And dateFrom <> "" Then
                        csvFileName = "Sales_Register_GSPIN_IW_" & exportShipName & "_" & dateFrom & "_" & dateTo & ".csv"
                    Else
                        If dateTo <> "" Then
                            csvFileName = "Sales_Register_GSPIN_IW_" & exportShipName & "_" & dateTo & ".csv"
                        End If
                        If dateFrom <> "" Then
                            csvFileName = "Sales_Register_GSPIN_IW_" & exportShipName & "_" & dateFrom & ".csv"
                        End If
                    End If
                Else
                    '月指定のとき
                    csvFileName = "Sales_Register_GSPIN_IW_" & exportShipName & "_" & dtNow.ToString("yyyy") & "_" & setMon & ".csv"
                End If

                outputPath = clsSet.CsvFilePass & csvFileName

                '項目名設定
                Dim rowWork1 As String = "ASC Code,Branch Code,ASC Claim No,Part Invoice No,Labour Invoice No,Samsung Claim No,Service Type,Consumer Name,Consumer Addr1,Consumer Addr2,Consumer Telephone,Consumer Fax,Postal Code,Model,Serial No,IMEI No,Defect Type,Condition,Symptom,Defect Code,Repair Code,Defect Desc,Repair Description,Purchase Date,Repair Received Date,Completed Date,Delivery Date,Production Date,Labor Amount Ⅰ,Parts Amount Ⅰ,Total Invoice Amount Ⅰ,Freight,Other,Parts SGST,Parts UTGST,Parts CGST,Parts IGST,Parts Cess,SGST,UTGST,CGST,IGST,Cess,Remark,Tr No,Tr Type,Status,Engineer,Collection Point,Collection Point Name,Location-1,Part-1,Qty-1,Unit Price-1,Doc Num-1,Matrial Serial-1,Location-2,Part-2,Qty-2,Unit Price-2,Doc Num-2,Matrial Serial-2,Location-3,Part-3,Qty-3,Unit Price-3,Doc Num-3,Matrial Serial-3,Location-4,Part-4,Qty-4,Unit Price-4,Doc Num-4,Matrial Serial-4,Location-5,Part-5,Qty-5,Unit Price-5,Doc Num-5,Matrial Serial-5,Location-6,Part-6,Qty-6,Unit Price-6,Doc Num-6,Matrial Serial-6,Location-7,Part-7,Qty-7,Unit Price-7,Doc Num-7,Matrial Serial-7,Location-8,Part-8,Qty-8,Unit Price-8,Doc Num-8,Matrial Serial-8,Location-9,Part-9,Qty-9,Unit Price-9,Doc Num-9,Matrial Serial-9,Location-10,Part-10,Qty-10,Unit Price-10,Doc Num-10,Matrial Serial-10,Location-11,Part-11,Qty-11,Unit Price-11,Doc Num-11,Matrial Serial-11,Location-12,Part-12,Qty-12,Unit Price-12,Doc Num-12,Matrial Serial-12,Location-13,Part-13,Qty-13,Unit Price-13,Doc Num-13,Matrial Serial-13,Location-14,Part-14,Qty-14,Unit Price-14,Doc Num-14,Matrial Serial-14,Location-15,Part-15,Qty-15,Unit Price-15,Doc Num-15,Matrial Serial-15"

                Dim csvContents = New List(Of String)(New String() {rowWork1})

                For i = 0 To wtyDataIW.Length - 1
                    csvContents.Add(wtyDataIW(i).ASC_Code & "," & wtyDataIW(i).Branch_Code & "," & wtyDataIW(i).ASC_Claim_No & "," & wtyDataIW(i).Parts_invoice_No & "," & wtyDataIW(i).Labor_Invoice_No & "," & wtyDataIW(i).Samsung_Claim_No & "," & wtyDataIW(i).Service_Type & "," & wtyDataIW(i).Consumer_Name & "," & wtyDataIW(i).Consumer_Addr1 & "," & wtyDataIW(i).Consumer_Addr2 & "," & wtyDataIW(i).Consumer_Telephone & "," & wtyDataIW(i).Consumer_Fax & "," & wtyDataIW(i).Postal_Code & "," & wtyDataIW(i).Model & "," & wtyDataIW(i).Serial_No & "," & wtyDataIW(i).IMEI_No & "," & wtyDataIW(i).Defect_Type & "," & wtyDataIW(i).Condition & "," & wtyDataIW(i).Symptom & "," & wtyDataIW(i).Defect_Code & "," & wtyDataIW(i).Repair_Code & "," & wtyDataIW(i).Defect_Desc & "," & wtyDataIW(i).Repair_Description & "," & wtyDataIW(i).Purchase_Date & "," & wtyDataIW(i).Repair_Received_Date & "," & wtyDataIW(i).Completed_Date & "," & wtyDataIW(i).Delivery_Date & "," & wtyDataIW(i).Production_Date & "," & wtyDataIW(i).Labor & "," & wtyDataIW(i).Parts & "," & wtyDataIW(i).Invoice & "," & wtyDataIW(i).Freight & "," & wtyDataIW(i).Other & "," & wtyDataIW(i).Parts_SGST & "," & wtyDataIW(i).Parts_UTGST & "," & wtyDataIW(i).Parts_CGST & "," & wtyDataIW(i).Parts_IGST & "," & wtyDataIW(i).Parts_Cess & "," & wtyDataIW(i).SGST & "," & wtyDataIW(i).UTGST & "," & wtyDataIW(i).CGST & "," & wtyDataIW(i).IGST & "," & wtyDataIW(i).Cess & "," & wtyDataIW(i).Remark & "," & wtyDataIW(i).Tr_No & "," & wtyDataIW(i).Tr_Type & "," & wtyDataIW(i).Status & "," & wtyDataIW(i).Engineer & "," & wtyDataIW(i).Collection_Point & "," & wtyDataIW(i).Collection_Point_Name & "," & wtyDataIW(i).Location_1 & "," & wtyDataIW(i).Part_1 & "," & wtyDataIW(i).Qty_1 & "," & wtyDataIW(i).Unit_Price_1 & "," & wtyDataIW(i).Doc_Num_1 & "," & wtyDataIW(i).Matrial_Serial_1 & "," & wtyDataIW(i).Location_2 & "," & wtyDataIW(i).Part_2 & "," & wtyDataIW(i).Qty_2 & "," & wtyDataIW(i).Unit_Price_2 & "," & wtyDataIW(i).Doc_Num_2 & "," & wtyDataIW(i).Matrial_Serial_2 & "," & wtyDataIW(i).Location_3 & "," & wtyDataIW(i).Part_3 & "," & wtyDataIW(i).Qty_3 & "," & wtyDataIW(i).Unit_Price_3 & "," & wtyDataIW(i).Doc_Num_3 & "," & wtyDataIW(i).Matrial_Serial_3 & "," & wtyDataIW(i).Location_4 & "," & wtyDataIW(i).Part_4 & "," & wtyDataIW(i).Qty_4 & "," & wtyDataIW(i).Unit_Price_4 & "," & wtyDataIW(i).Doc_Num_4 & "," & wtyDataIW(i).Matrial_Serial_4 & "," & wtyDataIW(i).Location_5 & "," & wtyDataIW(i).Part_5 & "," & wtyDataIW(i).Qty_5 & "," & wtyDataIW(i).Unit_Price_5 & "," & wtyDataIW(i).Doc_Num_5 & "," & wtyDataIW(i).Matrial_Serial_5 & "," & wtyDataIW(i).Location_6 & "," & wtyDataIW(i).Part_6 & "," & wtyDataIW(i).Qty_6 & "," & wtyDataIW(i).Unit_Price_6 & "," & wtyDataIW(i).Doc_Num_6 & "," & wtyDataIW(i).Matrial_Serial_6 & "," & wtyDataIW(i).Location_7 & "," & wtyDataIW(i).Part_7 & "," & wtyDataIW(i).Qty_7 & "," & wtyDataIW(i).Unit_Price_7 & "," & wtyDataIW(i).Doc_Num_7 & "," & wtyDataIW(i).Matrial_Serial_7 & "," & wtyDataIW(i).Location_8 & "," & wtyDataIW(i).Part_8 & "," & wtyDataIW(i).Qty_8 & "," & wtyDataIW(i).Unit_Price_8 & "," & wtyDataIW(i).Doc_Num_8 & "," & wtyDataIW(i).Matrial_Serial_8 & "," & wtyDataIW(i).Location_9 & "," & wtyDataIW(i).Part_9 & "," & wtyDataIW(i).Qty_9 & "," & wtyDataIW(i).Unit_Price_9 & "," & wtyDataIW(i).Doc_Num_9 & "," & wtyDataIW(i).Matrial_Serial_9 & "," & wtyDataIW(i).Location_10 & "," & wtyDataIW(i).Part_10 & "," & wtyDataIW(i).Qty_10 & "," & wtyDataIW(i).Unit_Price_10 & "," & wtyDataIW(i).Doc_Num_10 & "," & wtyDataIW(i).Matrial_Serial_10 & "," & wtyDataIW(i).Location_11 & "," & wtyDataIW(i).Part_11 & "," & wtyDataIW(i).Qty_11 & "," & wtyDataIW(i).Unit_Price_11 & "," & wtyDataIW(i).Doc_Num_11 & "," & wtyDataIW(i).Matrial_Serial_11 & "," & wtyDataIW(i).Location_12 & "," & wtyDataIW(i).Part_12 & "," & wtyDataIW(i).Qty_12 & "," & wtyDataIW(i).Unit_Price_12 & "," & wtyDataIW(i).Doc_Num_12 & "," & wtyDataIW(i).Matrial_Serial_12 & "," & wtyDataIW(i).Location_13 & "," & wtyDataIW(i).Part_13 & "," & wtyDataIW(i).Qty_13 & "," & wtyDataIW(i).Unit_Price_13 & "," & wtyDataIW(i).Doc_Num_13 & "," & wtyDataIW(i).Matrial_Serial_13 & "," & wtyDataIW(i).Location_14 & "," & wtyDataIW(i).Part_14 & "," & wtyDataIW(i).Qty_14 & "," & wtyDataIW(i).Unit_Price_14 & "," & wtyDataIW(i).Doc_Num_14 & "," & wtyDataIW(i).Matrial_Serial_14 & "," & wtyDataIW(i).Location_15 & "," & wtyDataIW(i).Part_15 & "," & wtyDataIW(i).Qty_15 & "," & wtyDataIW(i).Unit_Price_15 & "," & wtyDataIW(i).Doc_Num_15 & "," & wtyDataIW(i).Matrial_Serial_15)
                Next i

                Using writer As New StreamWriter(outputPath, False, Encoding.Default)
                    writer.WriteLine(String.Join(Environment.NewLine, csvContents))
                End Using

                'ファイルの内容をバイト配列にすべて読み込む 
                Dim Buffer As Byte() = System.IO.File.ReadAllBytes(outputPath)

                'サーバに保存されたCSVファイルを削除
                '※Response.End以降、ファイル削除処理ができないため、ファイルのダウンロードではなく、一旦ファイルの内容を
                '上記、Bufferに保存し、ダウンロード処理を行う。

                If outputPath <> "" Then
                    If System.IO.File.Exists(outputPath) = True Then
                        System.IO.File.Delete(outputPath)
                    End If
                End If

                ' ダウンロード
                Response.ContentType = "application/text/csv"
                Response.AddHeader("Content-Disposition", "attachment; filename=" & csvFileName)
                Response.Flush()
                'Response.Write("<b>File Contents: </b>")
                Response.BinaryWrite(Buffer)
                'Response.WriteFile(outputPath)
                Response.End()

            Catch ex As System.Threading.ThreadAbortException
                'Response.End()の呼び出しによりエラーメッセージを出力しないようにする

            Catch ex As Exception
                Call showMsg("CSV出力処理に失敗しました。", "")
            End Try

        ElseIf kindExport = 6 Then

            'Other_Sales_GSPIN
            '出力対象情報を取得
            Dim wtyDataOther() As Class_analysis.WTY_EXCEL

            Call clsSet.setSalesRegister_IW_OTHER(wtyDataOther, exportShipName, shipCode, userid, userName, errFlg, setMon, dateFrom, dateTo, "OTHER")

            If wtyDataOther Is Nothing Then
                Call showMsg("There is no output target of Other_Sales_GSPIN.", "")
                Exit Sub
            End If

            If errFlg = 1 Then
                Call showMsg("Failed to get information on Other_Sales_GSPIN.", "")
                Exit Sub
            End If

            Try
                'ファイル名、パスの設定
                Dim csvFileName As String

                dateFrom = Replace(dateFrom, "/", "")
                dateTo = Replace(dateTo, "/", "")

                'exportFile名の頭、数値を除く
                exportFile = Right(exportFile, exportFile.Length - 2)

                If setMon = "00" Then
                    If dateTo <> "" And dateFrom <> "" Then
                        csvFileName = "Other_Sales_GSPIN_" & exportShipName & "_" & dateFrom & "_" & dateTo & ".csv"
                    Else
                        If dateTo <> "" Then
                            csvFileName = "Other_Sales_GSPIN_" & exportShipName & "_" & dateTo & ".csv"
                        End If
                        If dateFrom <> "" Then
                            csvFileName = "Other_Sales_GSPIN_" & exportShipName & "_" & dateFrom & ".csv"
                        End If
                    End If
                Else
                    '月指定のとき
                    csvFileName = "Other_Sales_GSPIN_" & exportShipName & "_" & dtNow.ToString("yyyy") & "_" & setMon & ".csv"
                End If

                outputPath = clsSet.CsvFilePass & csvFileName

                '項目名設定
                Dim rowWork1 As String = "ASC Code,Branch Code,ASC Claim No,Part Invoice No,Labour Invoice No,Samsung Claim No,Service Type,Consumer Name,Consumer Addr1,Consumer Addr2,Consumer Telephone,Consumer Fax,Postal Code,Model,Serial No,IMEI No,Defect Type,Condition,Symptom,Defect Code,Repair Code,Defect Desc,Repair Description,Purchase Date,Repair Received Date,Completed Date,Delivery Date,Production Date,Labor Amount Ⅰ,Parts Amount Ⅰ,Total Invoice Amount Ⅰ,Remark,Tr No,Tr Type,Status,Engineer,Collection Point,Collection Point Name,Location-1,Part-1,Qty-1,Unit Price-1,Doc Num-1,Matrial Serial-1,Location-2,Part-2,Qty-2,Unit Price-2,Doc Num-2,Matrial Serial-2,Location-3,Part-3,Qty-3,Unit Price-3,Doc Num-3,Matrial Serial-3,Location-4,Part-4,Qty-4,Unit Price-4,Doc Num-4,Matrial Serial-4,Location-5,Part-5,Qty-5,Unit Price-5,Doc Num-5,Matrial Serial-5,Location-6,Part-6,Qty-6,Unit Price-6,Doc Num-6,Matrial Serial-6,Location-7,Part-7,Qty-7,Unit Price-7,Doc Num-7,Matrial Serial-7,Location-8,Part-8,Qty-8,Unit Price-8,Doc Num-8,Matrial Serial-8,Location-9,Part-9,Qty-9,Unit Price-9,Doc Num-9,Matrial Serial-9,Location-10,Part-10,Qty-10,Unit Price-10,Doc Num-10,Matrial Serial-10,Location-11,Part-11,Qty-11,Unit Price-11,Doc Num-11,Matrial Serial-11,Location-12,Part-12,Qty-12,Unit Price-12,Doc Num-12,Matrial Serial-12,Location-13,Part-13,Qty-13,Unit Price-13,Doc Num-13,Matrial Serial-13,Location-14,Part-14,Qty-14,Unit Price-14,Doc Num-14,Matrial Serial-14,Location-15,Part-15,Qty-15,Unit Price-15,Doc Num-15,Matrial Serial-15"

                Dim csvContents = New List(Of String)(New String() {rowWork1})

                For i = 0 To wtyDataOther.Length - 1
                    csvContents.Add(wtyDataOther(i).ASC_Code & "," & wtyDataOther(i).Branch_Code & "," & wtyDataOther(i).ASC_Claim_No & "," & wtyDataOther(i).Parts_invoice_No & "," & wtyDataOther(i).Labor_Invoice_No & "," & wtyDataOther(i).Samsung_Claim_No & "," & wtyDataOther(i).Service_Type & "," & wtyDataOther(i).Consumer_Name & "," & wtyDataOther(i).Consumer_Addr1 & "," & wtyDataOther(i).Consumer_Addr2 & "," & wtyDataOther(i).Consumer_Telephone & "," & wtyDataOther(i).Consumer_Fax & "," & wtyDataOther(i).Postal_Code & "," & wtyDataOther(i).Model & "," & wtyDataOther(i).Serial_No & "," & wtyDataOther(i).IMEI_No & "," & wtyDataOther(i).Defect_Type & "," & wtyDataOther(i).Condition & "," & wtyDataOther(i).Symptom & "," & wtyDataOther(i).Defect_Code & "," & wtyDataOther(i).Repair_Code & "," & wtyDataOther(i).Defect_Desc & "," & wtyDataOther(i).Repair_Description & "," & wtyDataOther(i).Purchase_Date & "," & wtyDataOther(i).Repair_Received_Date & "," & wtyDataOther(i).Completed_Date & "," & wtyDataOther(i).Delivery_Date & "," & wtyDataOther(i).Production_Date & "," & wtyDataOther(i).Labor & "," & wtyDataOther(i).Parts & "," & wtyDataOther(i).Invoice & "," & wtyDataOther(i).Remark & "," & wtyDataOther(i).Tr_No & "," & wtyDataOther(i).Tr_Type & "," & wtyDataOther(i).Status & "," & wtyDataOther(i).Engineer & "," & wtyDataOther(i).Collection_Point & "," & wtyDataOther(i).Collection_Point_Name & "," & wtyDataOther(i).Location_1 & "," & wtyDataOther(i).Part_1 & "," & wtyDataOther(i).Qty_1 & "," & wtyDataOther(i).Unit_Price_1 & "," & wtyDataOther(i).Doc_Num_1 & "," & wtyDataOther(i).Matrial_Serial_1 & "," & wtyDataOther(i).Location_2 & "," & wtyDataOther(i).Part_2 & "," & wtyDataOther(i).Qty_2 & "," & wtyDataOther(i).Unit_Price_2 & "," & wtyDataOther(i).Doc_Num_2 & "," & wtyDataOther(i).Matrial_Serial_2 & "," & wtyDataOther(i).Location_3 & "," & wtyDataOther(i).Part_3 & "," & wtyDataOther(i).Qty_3 & "," & wtyDataOther(i).Unit_Price_3 & "," & wtyDataOther(i).Doc_Num_3 & "," & wtyDataOther(i).Matrial_Serial_3 & "," & wtyDataOther(i).Location_4 & "," & wtyDataOther(i).Part_4 & "," & wtyDataOther(i).Qty_4 & "," & wtyDataOther(i).Unit_Price_4 & "," & wtyDataOther(i).Doc_Num_4 & "," & wtyDataOther(i).Matrial_Serial_4 & "," & wtyDataOther(i).Location_5 & "," & wtyDataOther(i).Part_5 & "," & wtyDataOther(i).Qty_5 & "," & wtyDataOther(i).Unit_Price_5 & "," & wtyDataOther(i).Doc_Num_5 & "," & wtyDataOther(i).Matrial_Serial_5 & "," & wtyDataOther(i).Location_6 & "," & wtyDataOther(i).Part_6 & "," & wtyDataOther(i).Qty_6 & "," & wtyDataOther(i).Unit_Price_6 & "," & wtyDataOther(i).Doc_Num_6 & "," & wtyDataOther(i).Matrial_Serial_6 & "," & wtyDataOther(i).Location_7 & "," & wtyDataOther(i).Part_7 & "," & wtyDataOther(i).Qty_7 & "," & wtyDataOther(i).Unit_Price_7 & "," & wtyDataOther(i).Doc_Num_7 & "," & wtyDataOther(i).Matrial_Serial_7 & "," & wtyDataOther(i).Location_8 & "," & wtyDataOther(i).Part_8 & "," & wtyDataOther(i).Qty_8 & "," & wtyDataOther(i).Unit_Price_8 & "," & wtyDataOther(i).Doc_Num_8 & "," & wtyDataOther(i).Matrial_Serial_8 & "," & wtyDataOther(i).Location_9 & "," & wtyDataOther(i).Part_9 & "," & wtyDataOther(i).Qty_9 & "," & wtyDataOther(i).Unit_Price_9 & "," & wtyDataOther(i).Doc_Num_9 & "," & wtyDataOther(i).Matrial_Serial_9 & "," & wtyDataOther(i).Location_10 & "," & wtyDataOther(i).Part_10 & "," & wtyDataOther(i).Qty_10 & "," & wtyDataOther(i).Unit_Price_10 & "," & wtyDataOther(i).Doc_Num_10 & "," & wtyDataOther(i).Matrial_Serial_10 & "," & wtyDataOther(i).Location_11 & "," & wtyDataOther(i).Part_11 & "," & wtyDataOther(i).Qty_11 & "," & wtyDataOther(i).Unit_Price_11 & "," & wtyDataOther(i).Doc_Num_11 & "," & wtyDataOther(i).Matrial_Serial_11 & "," & wtyDataOther(i).Location_12 & "," & wtyDataOther(i).Part_12 & "," & wtyDataOther(i).Qty_12 & "," & wtyDataOther(i).Unit_Price_12 & "," & wtyDataOther(i).Doc_Num_12 & "," & wtyDataOther(i).Matrial_Serial_12 & "," & wtyDataOther(i).Location_13 & "," & wtyDataOther(i).Part_13 & "," & wtyDataOther(i).Qty_13 & "," & wtyDataOther(i).Unit_Price_13 & "," & wtyDataOther(i).Doc_Num_13 & "," & wtyDataOther(i).Matrial_Serial_13 & "," & wtyDataOther(i).Location_14 & "," & wtyDataOther(i).Part_14 & "," & wtyDataOther(i).Qty_14 & "," & wtyDataOther(i).Unit_Price_14 & "," & wtyDataOther(i).Doc_Num_14 & "," & wtyDataOther(i).Matrial_Serial_14 & "," & wtyDataOther(i).Location_15 & "," & wtyDataOther(i).Part_15 & "," & wtyDataOther(i).Qty_15 & "," & wtyDataOther(i).Unit_Price_15 & "," & wtyDataOther(i).Doc_Num_15 & "," & wtyDataOther(i).Matrial_Serial_15)
                Next i

                Using writer As New StreamWriter(outputPath, False, Encoding.Default)
                    writer.WriteLine(String.Join(Environment.NewLine, csvContents))
                End Using

                'ファイルの内容をバイト配列にすべて読み込む 
                Dim Buffer As Byte() = System.IO.File.ReadAllBytes(outputPath)

                'サーバに保存されたCSVファイルを削除
                '※Response.End以降、ファイル削除処理ができないため、ファイルのダウンロードではなく、一旦ファイルの内容を
                '上記、Bufferに保存し、ダウンロード処理を行う。

                If outputPath <> "" Then
                    If System.IO.File.Exists(outputPath) = True Then
                        System.IO.File.Delete(outputPath)
                    End If
                End If

                ' ダウンロード
                Response.ContentType = "application/text/csv"
                Response.AddHeader("Content-Disposition", "attachment; filename=" & csvFileName)
                Response.Flush()
                'Response.Write("<b>File Contents: </b>")
                Response.BinaryWrite(Buffer)
                'Response.WriteFile(outputPath)
                Response.End()

            Catch ex As System.Threading.ThreadAbortException
                'Response.End()の呼び出しによりエラーメッセージを出力しないようにする

            Catch ex As Exception
                Call showMsg("CSV output processing failed.", "")
            End Try

        ElseIf kindExport = 7 Then

            'SAW Discount Details
            '出力対象情報を取得
            Dim wtyDataOther() As Class_analysis.WTY_EXCEL

            Call clsSet.setSAW_Discount_Details(wtyDataOther, exportShipName, shipCode, userid, userName, errFlg, setMon, dateFrom, dateTo)

            If wtyDataOther Is Nothing Then
                Call showMsg("No SAW Discount Details output target.", "")
                Exit Sub
            End If

            If errFlg = 1 Then
                Call showMsg("SAW Discount Details information acquisition failed.", "")
                Exit Sub
            End If

            Try
                'ファイル名、パスの設定
                Dim csvFileName As String

                dateFrom = Replace(dateFrom, "/", "")
                dateTo = Replace(dateTo, "/", "")

                'exportFile名の頭、数値を除く
                exportFile = Right(exportFile, exportFile.Length - 2)

                If setMon = "00" Then
                    If dateTo <> "" And dateFrom <> "" Then
                        csvFileName = "SAW_Discount_Details_" & exportShipName & "_" & dateFrom & "_" & dateTo & ".csv"
                    Else
                        If dateTo <> "" Then
                            csvFileName = "SAW_Discount_Details_" & exportShipName & "_" & dateTo & ".csv"
                        End If
                        If dateFrom <> "" Then
                            csvFileName = "SAW_Discount_Details_" & exportShipName & "_" & dateFrom & ".csv"
                        End If
                    End If
                Else
                    '月指定のとき
                    csvFileName = "SAW_Discount_Details_" & exportShipName & "_" & dtNow.ToString("yyyy") & "_" & setMon & ".csv"
                End If

                outputPath = clsSet.CsvFilePass & csvFileName

                '項目名設定
                Dim rowWork1 As String = "Branch Code,ASC Claim No,Model Name,Part Amount (Retail Price) with out tax,SAW Discount Parts Amount,SAW Discount Labor Amount,Invoice Amount collected from Cus,Part Invoice No,Labour Invoice No,SAW Remarks"

                Dim csvContents = New List(Of String)(New String() {rowWork1})

                For i = 0 To wtyDataOther.Length - 1
                    csvContents.Add(exportShipName & "," & wtyDataOther(i).ASC_Claim_No & "," & wtyDataOther(i).Model & ",,,,," & wtyDataOther(i).Parts_invoice_No & "," & wtyDataOther(i).Labor_Invoice_No & ",")
                Next i

                Using writer As New StreamWriter(outputPath, False, Encoding.Default)
                    writer.WriteLine(String.Join(Environment.NewLine, csvContents))
                End Using

                'ファイルの内容をバイト配列にすべて読み込む 
                Dim Buffer As Byte() = System.IO.File.ReadAllBytes(outputPath)

                'サーバに保存されたCSVファイルを削除
                '※Response.End以降、ファイル削除処理ができないため、ファイルのダウンロードではなく、一旦ファイルの内容を
                '上記、Bufferに保存し、ダウンロード処理を行う。

                If outputPath <> "" Then
                    If System.IO.File.Exists(outputPath) = True Then
                        System.IO.File.Delete(outputPath)
                    End If
                End If

                ' ダウンロード
                Response.ContentType = "application/text/csv"
                Response.AddHeader("Content-Disposition", "attachment; filename=" & csvFileName)
                Response.Flush()
                'Response.Write("<b>File Contents: </b>")
                Response.BinaryWrite(Buffer)
                'Response.WriteFile(outputPath)
                Response.End()

            Catch ex As System.Threading.ThreadAbortException
                'Response.End()の呼び出しによりエラーメッセージを出力しないようにする

            Catch ex As Exception
                Call showMsg("CSV output processing failed.", "")
            End Try

        ElseIf kindExport = 9 Then

            'Final_Report
            '***InvoiceNo_Final最終番号取得***
            Dim InvoiceMax As String = ""
            Dim InvoiceNoMax As Long
            Dim strSQL4 As String = "SELECT MAX(RIGHT(InvoiceNo_Final,7)) AS InvoiceNo_Final_Max FROM dbo.Wty_Excel "
            strSQL4 &= "WHERE DELFG = 0 AND Branch_Code = '" & shipCode & "';"
            Dim DT_WtyExcel As DataTable
            DT_WtyExcel = DBCommon.ExecuteGetDT(strSQL4, errFlg)

            If errFlg = 1 Then
                Call showMsg("Failed to acquire po information.", "")
                Exit Sub
            End If

            If DT_WtyExcel IsNot Nothing Then
                If DT_WtyExcel.Rows(0)("InvoiceNo_Final_Max") IsNot DBNull.Value Then
                    InvoiceMax = DT_WtyExcel.Rows(0)("InvoiceNo_Final_Max")
                    InvoiceNoMax = Convert.ToInt64(InvoiceMax)
                    InvoiceNoMax = InvoiceNoMax + 1
                End If
            End If

            '***出力対象取得***
            Dim dsWtyExcel As New DataSet
            Dim wtyData() As Class_analysis.WTY_EXCEL

            Call clsSet.setFinalReport(dsWtyExcel, wtyData, userid, userName, shipCode, exportShipName, errFlg, setMon, dateFrom, dateTo, InvoiceNoMax)

            If dsWtyExcel Is Nothing Then
                Call showMsg("There is no output target of Final_Report.", "")
                Exit Sub
            End If

            If errFlg = 1 Then
                Call showMsg("Failed to get information on Final_Report.", "")
                Exit Sub
            End If

            Try
                '***ファイル名、パスの設定***
                Dim csvFileName As String

                dateFrom = Replace(dateFrom, "/", "")
                dateTo = Replace(dateTo, "/", "")

                'exportFile名の頭、数値を除く
                exportFile = Right(exportFile, exportFile.Length - 2)

                If setMon = "00" Then
                    If dateTo <> "" And dateFrom <> "" Then
                        csvFileName = exportShipName & "_Final_Report" & "_" & dateFrom & "_" & dateTo & ".csv"
                    Else
                        If dateTo <> "" Then
                            csvFileName = exportShipName & "_Final_Report" & "_" & dateTo & ".csv"
                        End If
                        If dateFrom <> "" Then
                            csvFileName = exportShipName & "_Final_Report" & "_" & dateFrom & ".csv"
                        End If
                    End If
                Else
                    '月指定のとき
                    csvFileName = exportShipName & "_Final_Report" & "_" & dtNow.ToString("yyyy") & "_" & setMon & ".csv"
                End If

                outputPath = clsSet.CsvFilePass & csvFileName

                '***項目名設定***
                Dim csvContents = New List(Of String)(New String() {exportShipName & "-Final_Report"})
                Dim rowWork1 As String = "ASC Claim No,Invocie Number,Delivery Date,Payment Method,Part-1,Qty-1,Unit Price-1,Sum of Labor Amount,Sum of Parts Amount,SGST Payable,CGST Payable,IGST Payable,Sum of Tax Amount,Sum of Total Invoice Amount"

                csvContents.Add(rowWork1)

                '***部品情報設定***
                For i = 0 To wtyData.Length - 1

                    ReDim Preserve wtyData(i).partsInfo(15)
                    wtyData(i).partsInfo(0).PartsName = wtyData(i).Part_1
                    wtyData(i).partsInfo(0).Qty = wtyData(i).Qty_1
                    wtyData(i).partsInfo(0).UnitPrice = wtyData(i).Unit_Price_1

                    wtyData(i).partsInfo(1).PartsName = wtyData(i).Part_2
                    wtyData(i).partsInfo(1).Qty = wtyData(i).Qty_2
                    wtyData(i).partsInfo(1).UnitPrice = wtyData(i).Unit_Price_2

                    wtyData(i).partsInfo(2).PartsName = wtyData(i).Part_3
                    wtyData(i).partsInfo(2).Qty = wtyData(i).Qty_3
                    wtyData(i).partsInfo(2).UnitPrice = wtyData(i).Unit_Price_3

                    wtyData(i).partsInfo(3).PartsName = wtyData(i).Part_4
                    wtyData(i).partsInfo(3).Qty = wtyData(i).Qty_4
                    wtyData(i).partsInfo(3).UnitPrice = wtyData(i).Unit_Price_4

                    wtyData(i).partsInfo(4).PartsName = wtyData(i).Part_5
                    wtyData(i).partsInfo(4).Qty = wtyData(i).Qty_5
                    wtyData(i).partsInfo(4).UnitPrice = wtyData(i).Unit_Price_5

                    '部品種類の個数を取得(部品5までは、6以降を確認しない)
                    If wtyData(i).Part_6 = "" Then

                        If wtyData(i).Part_1 = "" Then
                            wtyData(i).partsCount = 0
                        Else
                            If wtyData(i).Part_2 = "" Then
                                wtyData(i).partsCount = 1
                            Else
                                If wtyData(i).Part_3 = "" Then
                                    wtyData(i).partsCount = 2
                                Else
                                    If wtyData(i).Part_4 = "" Then
                                        wtyData(i).partsCount = 3
                                    Else
                                        If wtyData(i).Part_5 = "" Then
                                            wtyData(i).partsCount = 4
                                        Else
                                            If wtyData(i).Part_6 = "" Then
                                                wtyData(i).partsCount = 5
                                            End If
                                        End If
                                    End If
                                End If
                            End If
                        End If
                    Else

                        wtyData(i).partsInfo(5).PartsName = wtyData(i).Part_6
                        wtyData(i).partsInfo(5).Qty = wtyData(i).Qty_6
                        wtyData(i).partsInfo(5).UnitPrice = wtyData(i).Unit_Price_6

                        wtyData(i).partsInfo(6).PartsName = wtyData(i).Part_7
                        wtyData(i).partsInfo(6).Qty = wtyData(i).Qty_7
                        wtyData(i).partsInfo(6).UnitPrice = wtyData(i).Unit_Price_7

                        wtyData(i).partsInfo(7).PartsName = wtyData(i).Part_8
                        wtyData(i).partsInfo(7).Qty = wtyData(i).Qty_8
                        wtyData(i).partsInfo(7).UnitPrice = wtyData(i).Unit_Price_8

                        wtyData(i).partsInfo(8).PartsName = wtyData(i).Part_9
                        wtyData(i).partsInfo(8).Qty = wtyData(i).Qty_9
                        wtyData(i).partsInfo(8).UnitPrice = wtyData(i).Unit_Price_9

                        wtyData(i).partsInfo(9).PartsName = wtyData(i).Part_10
                        wtyData(i).partsInfo(9).Qty = wtyData(i).Qty_10
                        wtyData(i).partsInfo(9).UnitPrice = wtyData(i).Unit_Price_10

                        wtyData(i).partsInfo(10).PartsName = wtyData(i).Part_11
                        wtyData(i).partsInfo(10).Qty = wtyData(i).Qty_11
                        wtyData(i).partsInfo(10).UnitPrice = wtyData(i).Unit_Price_11

                        wtyData(i).partsInfo(11).PartsName = wtyData(i).Part_12
                        wtyData(i).partsInfo(11).Qty = wtyData(i).Qty_12
                        wtyData(i).partsInfo(11).UnitPrice = wtyData(i).Unit_Price_12

                        wtyData(i).partsInfo(12).PartsName = wtyData(i).Part_13
                        wtyData(i).partsInfo(12).Qty = wtyData(i).Qty_13
                        wtyData(i).partsInfo(12).UnitPrice = wtyData(i).Unit_Price_13

                        wtyData(i).partsInfo(13).PartsName = wtyData(i).Part_14
                        wtyData(i).partsInfo(13).Qty = wtyData(i).Qty_14
                        wtyData(i).partsInfo(13).UnitPrice = wtyData(i).Unit_Price_14

                        wtyData(i).partsInfo(14).PartsName = wtyData(i).Part_15
                        wtyData(i).partsInfo(14).Qty = wtyData(i).Qty_15
                        wtyData(i).partsInfo(14).UnitPrice = wtyData(i).Unit_Price_15

                        '部品6以降
                        If wtyData(i).Part_7 = "" Then
                            wtyData(i).partsCount = 6
                        Else
                            If wtyData(i).Part_8 = "" Then
                                wtyData(i).partsCount = 7
                            Else
                                If wtyData(i).Part_9 = "" Then
                                    wtyData(i).partsCount = 8
                                Else
                                    If wtyData(i).Part_10 = "" Then
                                        wtyData(i).partsCount = 9
                                    Else
                                        If wtyData(i).Part_11 = "" Then
                                            wtyData(i).partsCount = 10
                                        Else
                                            If wtyData(i).Part_12 = "" Then
                                                wtyData(i).partsCount = 11
                                            Else
                                                If wtyData(i).Part_13 = "" Then
                                                    wtyData(i).partsCount = 12
                                                Else
                                                    If wtyData(i).Part_14 = "" Then
                                                        wtyData(i).partsCount = 13
                                                    Else
                                                        If wtyData(i).Part_15 = "" Then
                                                            wtyData(i).partsCount = 14
                                                        Else
                                                            wtyData(i).partsCount = 15
                                                        End If
                                                    End If
                                                End If
                                            End If
                                        End If
                                    End If
                                End If
                            End If
                        End If
                    End If
                Next i

                '***CSV出力***
                For i = 0 To wtyData.Length - 1

                    '合計
                    'wtyData(i).Sum_Total_Invoice_Amount = wtyData(i).Labor_Amount + wtyData(i).Parts_Amount + wtyData(i).Sum_Tax_Amount

                    'InvoiceNo_Final 000の表記外す
                    Dim tmp() As String = Split(wtyData(i).InvoiceNo_Final, "-")
                    Dim tmp2 As Integer

                    If tmp.Length = 2 Then
                        tmp2 = tmp(1)
                        wtyData(i).InvoiceNo_Final = tmp(0) & "-" & tmp2.ToString
                    End If

                    '最初の１行目、parts1と合計情報
                    If wtyData(i).partsCount <> 0 Then
                        'csvContents.Add(wtyData(i).ASC_Claim_No & "," & wtyData(i).InvoiceNo_Final & "," & wtyData(i).Delivery_Date & "," & wtyData(i).Part_1 & "," & wtyData(i).Qty_1 & "," & wtyData(i).Unit_Price_1 & "," & wtyData(i).Labor_Amount & "," & wtyData(i).Parts_Amount & "," & wtyData(i).SGST_Payable & "," & wtyData(i).CGST_Payable & "," & wtyData(i).IGST_Payable & "," & wtyData(i).Sum_Tax_Amount & "," & wtyData(i).Sum_Total_Invoice_Amount)
                        csvContents.Add(wtyData(i).ASC_Claim_No & "," & wtyData(i).InvoiceNo_Final & "," & wtyData(i).Delivery_Date & "," & wtyData(i).payment & "," & wtyData(i).Part_1 & "," & wtyData(i).Qty_1 & "," & wtyData(i).Unit_Price_1 & "," & wtyData(i).OW_Labor & "," & wtyData(i).OW_Parts & "," & wtyData(i).SGST_Payable & "," & wtyData(i).CGST_Payable & "," & wtyData(i).IGST_Payable & "," & wtyData(i).Sum_Tax_Amount & "," & wtyData(i).OW_total)
                    End If

                    If wtyData(i).partsCount = 1 Then
                        'parts情報最後の行に、作業費を設定
                        csvContents.Add(wtyData(i).ASC_Claim_No & "," & wtyData(i).InvoiceNo_Final & "," & wtyData(i).Delivery_Date & "," & wtyData(i).payment & ",Labour Charges,," & wtyData(i).OW_Labor)
                    Else
                        For j = 1 To wtyData(i).partsCount
                            '各行に部品毎情報を設定
                            If j = wtyData(i).partsCount Then
                                csvContents.Add(wtyData(i).ASC_Claim_No & "," & wtyData(i).InvoiceNo_Final & "," & wtyData(i).Delivery_Date & "," & wtyData(i).payment & ",Labour Charges,," & wtyData(i).OW_Labor)
                            Else
                                csvContents.Add(wtyData(i).ASC_Claim_No & "," & wtyData(i).InvoiceNo_Final & "," & wtyData(i).Delivery_Date & "," & wtyData(i).payment & "," & wtyData(i).partsInfo(j).PartsName & "," & wtyData(i).partsInfo(j).Qty & "," & wtyData(i).partsInfo(j).UnitPrice)
                            End If
                        Next j
                    End If

                Next i

                Using writer As New StreamWriter(outputPath, False, Encoding.Default)
                    writer.WriteLine(String.Join(Environment.NewLine, csvContents))
                End Using

                'ファイルの内容をバイト配列にすべて読み込む 
                Dim Buffer As Byte() = System.IO.File.ReadAllBytes(outputPath)

                'サーバに保存されたCSVファイルを削除
                '※Response.End以降、ファイル削除処理ができないため、ファイルのダウンロードではなく、一旦ファイルの内容を
                '上記、Bufferに保存し、ダウンロード処理を行う。

                If outputPath <> "" Then
                    If System.IO.File.Exists(outputPath) = True Then
                        System.IO.File.Delete(outputPath)
                    End If
                End If

                ' ダウンロード
                Response.ContentType = "application/text/csv"
                Response.AddHeader("Content-Disposition", "attachment; filename=" & csvFileName)
                Response.Flush()
                'Response.Write("<b>File Contents: </b>")
                Response.BinaryWrite(Buffer)
                'Response.WriteFile(outputPath)
                Response.End()

            Catch ex As System.Threading.ThreadAbortException
                'Response.End()の呼び出しによりエラーメッセージを出力しないようにする

            Catch ex As Exception
                If outputPath <> "" Then
                    If System.IO.File.Exists(outputPath) = True Then
                        System.IO.File.Delete(outputPath)
                    End If
                End If
                Call showMsg("CSV output processing failed.", "")
            End Try

        End If

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

End Class