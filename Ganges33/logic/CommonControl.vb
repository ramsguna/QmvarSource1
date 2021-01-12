Imports System
Imports System.IO
Imports System.Configuration
Imports System.Data.SqlClient
Imports System.Web.UI.HtmlControls
Imports System.Collections.Generic
Imports Ganges33.Ganges33.model

Namespace Ganges33.logic
    ''' <summary>
    ''' Define common control
    ''' </summary>
    Public Class CommonControl

        Dim _ShipBaseControl As ShipBaseControl = New ShipBaseControl()

        Public Shared Function ConvertShipCode(ShipCode As String) As String
            Dim intShipcode As Integer
            intShipcode = Len(ShipCode)
            Select Case intShipcode
                Case 6
                    ShipCode = "0000" & ShipCode
                Case 7
                    ShipCode = "000" & ShipCode
                Case 8
                    ShipCode = "00" & ShipCode
                Case 9
                    ShipCode = "0" & ShipCode
                Case Else
                    ShipCode = ShipCode
            End Select
            Return ShipCode
        End Function



        Shared Function CheckDataTable(myTable As DataTable, checkFunc As Func(Of DataRow, Boolean)) As Boolean
            For Each row As DataRow In myTable.Rows
                If checkFunc(row) Then Return True
            Next
            Return False
        End Function

        Public Shared Function GetNullableParameter(ByVal parameterName As String, ByVal value As Object) As SqlParameter
            If value IsNot Nothing AndAlso value.ToString() <> String.Empty Then
                Return New SqlParameter(parameterName, value)
            Else
                Return New SqlParameter(parameterName, DBNull.Value)
            End If
        End Function


        Public Shared Function GetNotNullableParameter(ByVal parameterName As String, ByVal value As Object) As SqlParameter
            If value IsNot Nothing AndAlso value.ToString() <> String.Empty Then
                Return New SqlParameter(parameterName, value)
            Else
                Return New SqlParameter(parameterName, "")
            End If
        End Function

        Public Shared Function isDate(ByVal str As String) As Boolean
            Return System.Text.RegularExpressions.Regex.IsMatch(str, "^(\d{4})/(\d{1,2})/(\d{1,2})$")
        End Function

        Public Shared Function GetLastDayOfMonth(intMonth, intYear) As Date
            GetLastDayOfMonth = DateSerial(intYear, intMonth + 1, 0)
        End Function

        Public Shared Function csvBytesWriter(ByRef dTable As DataTable) As Byte()

            '--------Columns Name--------------------------------------------------------------------------- 

            Dim sb As StringBuilder = New StringBuilder()
            Dim intClmn As Integer = dTable.Columns.Count

            Dim i As Integer = 0
            For i = 0 To intClmn - 1 Step i + 1
                sb.Append("""" + dTable.Columns(i).ColumnName.ToString() + """")
                If i = intClmn - 1 Then
                    sb.Append(" ")
                Else
                    sb.Append(",")
                End If
            Next
            sb.Append(vbNewLine)

            '--------Data By  Columns--------------------------------------------------------------------------- 

            Dim row As DataRow
            For Each row In dTable.Rows

                Dim ir As Integer = 0
                For ir = 0 To intClmn - 1 Step ir + 1
                    sb.Append("""" + row(ir).ToString().Replace("""", """""") + """")
                    If ir = intClmn - 1 Then
                        sb.Append(" ")
                    Else
                        sb.Append(",")
                    End If

                Next
                sb.Append(vbNewLine)
            Next

            Return System.Text.Encoding.UTF8.GetBytes(sb.ToString)

        End Function

        Public Shared Function _csvBytesWriter(ByRef dTable As DataTable) As Byte()

            '--------Columns Name--------------------------------------------------------------------------- 

            Dim sb As StringBuilder = New StringBuilder()
            Dim intClmn As Integer = dTable.Columns.Count

            Dim i As Integer = 0
            For i = 0 To intClmn - 1 Step i + 1
                sb.Append("""" + dTable.Columns(i).ColumnName.ToString() + """")
                If i = intClmn - 1 Then
                    sb.Append(" ")
                Else
                    sb.Append(",")
                End If
            Next
            sb.Append(vbNewLine)

            '--------Data By  Columns--------------------------------------------------------------------------- 

            Dim row As DataRow
            For Each row In dTable.Rows

                Dim ir As Integer = 0
                For ir = 0 To intClmn - 1 Step ir + 1
                    sb.Append("""" + row(ir).ToString().Replace("""", """""") + """")
                    If ir = intClmn - 1 Then
                        sb.Append(" ")
                    Else
                        sb.Append(",")
                    End If

                Next
                sb.Append(vbNewLine)
            Next

            Return System.Text.Encoding.UTF8.GetBytes(sb.ToString)

        End Function


        '****************************************************
        '処理概要：Money型の小数点３、４桁目を切り捨てる
        '引数： moneyInr　金額
        '返却： moneyInr　金額の小数点以下を２桁にセットして返す　
        '****************************************************
        Public Shared Function setINR(ByVal moneyInr As String) As String

            If moneyInr = "" Then
                Exit Function
            End If

            Dim amountDelimit() As String

            amountDelimit = Split(moneyInr, ".")


            If amountDelimit.Length = 1 Then

                '小数点なしは、.00を付加　　例）30 ⇒　30.00
                moneyInr = moneyInr & ".00"

            ElseIf amountDelimit.Length = 2 Then

                '小数点ありは、３桁以上切り捨て 例）30.2300 ⇒　30.23
                moneyInr = amountDelimit(0) & "." & Left(amountDelimit(1), 2)

            End If

            Return moneyInr

        End Function
        '****************************************************
        '処理概要：decimal型の小数点３、４桁目を切り捨てる
        '引数： moneyInr　金額
        '返却： moneyInr　金額の小数点以下を２桁にセットして返す　
        '****************************************************
        Public Shared Function setINR2(ByVal moneyInr As Decimal) As Decimal

            Dim amountDelimit() As String
            Dim moneyInrStr As String

            amountDelimit = Split(moneyInr.ToString, ".")

            If amountDelimit.Length = 1 Then

                '小数点なしは、.00を付加　　例）30 ⇒　30.00
                moneyInrStr = moneyInr.ToString & ".00"

            ElseIf amountDelimit.Length = 2 Then

                '小数点ありは、３桁以上切り捨て 例）30.2300 ⇒　30.23
                moneyInrStr = amountDelimit(0) & "." & Left(amountDelimit(1), 2)

            End If

            Return Convert.ToDecimal(moneyInrStr)

        End Function
        Public Shared Function isNumber(ByVal str As String) As Boolean
            If Len(str) <= 0 Then
                Return True
                Exit Function
            End If
            Return System.Text.RegularExpressions.Regex.IsMatch(str, "^\d*[1-9]\d*$")
        End Function

        Public Shared Function isNumberValid(ByVal str As String) As Boolean
            If Len(str) <= 0 Then
                Return False
                Exit Function
            End If
            Return System.Text.RegularExpressions.Regex.IsMatch(str, "^\d*[0-9]\d*$")
        End Function
        Public Shared Function isMaxZero(ByVal intVal As Int16) As Boolean
            If intVal >= 0 Then
                Return True
            Else
                Return False
            End If
        End Function

        Public Shared Function isNumberDR(ByVal str As String) As Boolean
            If Len(str) <= 0 Then
                Return True
                Exit Function
            End If
            Return System.Text.RegularExpressions.Regex.IsMatch(str, "^(0|[1-9]\d*)(\.\d+)?$")
        End Function
        Public Shared Function isNumberCT_CC(ByVal str As String) As Boolean
            If Len(str) <= 0 Then
                Return False
                Exit Function
            End If
            Return System.Text.RegularExpressions.Regex.IsMatch(str, "^(0|[1-9]\d*)(\.\d+)?$")
        End Function

        Public Shared Function IsValidEmailFormat(ByVal s As String) As Boolean
            Return Regex.IsMatch(s, "^([0-9a-zA-Z]([-\.\w]*[0-9a-zA-Z])*@([0-9a-zA-Z][-\w]*[0-9a-zA-Z]\.)+[a-zA-Z]{2,9})$")
        End Function

        Public Function InitDropDownList(ByVal UserName As String, ByVal user_id As String) As List(Of CodeMasterModel)
            Log4NetControl.ComInfoLogWrite(UserName)
            'Dim user_id As String = Session("user_id") 'Session("user_Name")
            'Clear the branch location
            'DropListLocation.Items.Clear()
            'For store the branch codes in array
            Dim shipCodeAll() As String
            'Verify entered user and password correct or not with the database
            Dim _UserInfoModel As UserInfoModel = New UserInfoModel()
            Dim _UserInfoControl As UserInfoControl = New UserInfoControl()
            _UserInfoModel.UserId = user_id
            '_UserInfoModel.Password = TextPass.Text.ToString().Trim()
            Dim UserInfoList As List(Of UserInfoModel) = _UserInfoControl.SelectUserInfo(_UserInfoModel)
            'User Doesn't exist
            'If UserInfoList Is Nothing OrElse UserInfoList.Count = 0 Then
            '    Call showMsg("The username / password incorrect. Please try again", "")
            '    Exit Sub
            'End If
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
            Log4NetControl.ComInfoLogWrite(UserName)
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
            'If Not Type = "M" Then
            Dim codeMaster1 As CodeMasterModel = New CodeMasterModel()
            codeMaster1.CodeValue = "Select Branch"
            codeMaster1.CodeDispValue = "Select Branch"
            codeMasterList.Insert(0, codeMaster1)
            'Dim codeMaster2 As CodeMasterModel = New CodeMasterModel()
            'End If

            '''''codeMaster2.CodeValue = "ALL"
            '''''codeMaster2.CodeDispValue = "ALL"
            '''''codeMasterList.Insert(1, codeMaster2)
            Return codeMasterList
            'Me.DropListLocation.DataSource = codeMasterList
            'Me.DropListLocation.DataTextField = "CodeDispValue"
            'Me.DropListLocation.DataValueField = "CodeValue"
            'Me.DropListLocation.DataBind()
        End Function

        Public Function showMsg(ByVal msgChk As String) As String

            'Dim cs As ClientScriptManager
            'cs.RegisterStartupScript()
            Dim sScript As String

            If msgChk = "CancelMsg" Then
                'OKとキャンセルボタン
                sScript = "$(function () {$(""#dialog"" ).dialog({width: 400,buttons:{""OK"": function () {$(this).dialog('close');$('[id$=""BtnOK""]').click();},""CANCEL"": function () {$(this).dialog('close');$('[id$=""BtnCancel""]').click();}}});});"
            Else
                'OKボタンのみ
                sScript = "$(function () { $( ""#dialog"" ).dialog({width: 400, buttons: {""OK"": function () {$(this).dialog('close');}}});});"
            End If
            'cs.RegisterStartupScript(Me.GetType(), "startup", sScript, True)
            'JavaScriptの埋め込み
            Return sScript

        End Function

        Public Function Money_Round(ByVal MoneyValue As Decimal, ByVal decimalvalue As Byte) As Double
            Return Math.Round(MoneyValue, decimalvalue, MidpointRounding.AwayFromZero)
        End Function
        'Public Function PL_Track_Summary_Total(ByVal csvContents As List(Of String)) As DataTable
        '    Dim dtPLSummaryTotalCount As DataTable = New DataTable()
        '    dtPLSummaryTotalCount.Columns.Add("Summary Name")
        '    dtPLSummaryTotalCount.Columns.Add("Total Count")

        '    For i = 0 To csvContents.Length - 1
        '        If i > 0 Then

        '        End If
        '    Next i
        '    Return dtPLSummaryTotalCount

        'End Function

        Public Function SelectPlReport(ByVal FromDate As String, ByVal ToDate As String, ByVal shipName As String, ByVal shipCode As String, ByVal Optional Gm As Decimal = 0.88) As DataTable
            Dim comcontrol As New CommonControl
            Dim _dtPlReprt As New DataTable

            'Not Defined functionality , Always Zero
            _dtPlReprt.Columns.Add("NumberOfCounters", GetType(Integer))
            _dtPlReprt.Columns.Add("NumberOfBusinessDat", GetType(Integer))
            _dtPlReprt.Columns.Add("NumberOfStaffs", GetType(Integer))
            'End
            _dtPlReprt.Columns.Add("CustomerVisit", GetType(Integer))
            _dtPlReprt.Columns.Add("CallRegistered", GetType(Integer))
            _dtPlReprt.Columns.Add("RepairCompleted", GetType(Integer))
            _dtPlReprt.Columns.Add("GoodsDelivered", GetType(Integer))
            _dtPlReprt.Columns.Add("Pending", GetType(Integer))
            _dtPlReprt.Columns.Add("Warranty", GetType(Integer))
            _dtPlReprt.Columns.Add("InWarranty", GetType(Integer))
            _dtPlReprt.Columns.Add("OutWarranty", GetType(Integer))
            _dtPlReprt.Columns.Add("InWarrantyAmount", GetType(Decimal))
            _dtPlReprt.Columns.Add("InWarrantyLabour", GetType(Decimal))
            _dtPlReprt.Columns.Add("InWarrantyParts", GetType(Decimal))
            _dtPlReprt.Columns.Add("InWarrantyTransport", GetType(Decimal))
            _dtPlReprt.Columns.Add("InWarrantyOthers", GetType(Decimal))
            _dtPlReprt.Columns.Add("OutWarrantyAmount", GetType(Decimal))
            _dtPlReprt.Columns.Add("OutWarrantyLabour", GetType(Decimal))
            _dtPlReprt.Columns.Add("OutWarrantyParts", GetType(Decimal))
            _dtPlReprt.Columns.Add("OutWarrantyTransport", GetType(Decimal))
            _dtPlReprt.Columns.Add("OutWarrantyOthers", GetType(Decimal))
            _dtPlReprt.Columns.Add("SawDiscountLabour", GetType(Decimal))
            _dtPlReprt.Columns.Add("SawDiscountParts", GetType(Decimal))
            _dtPlReprt.Columns.Add("OutWarrantyLabourwSawDisc", GetType(Decimal))
            _dtPlReprt.Columns.Add("OutWarrantyPartswSawDisc", GetType(Decimal))
            _dtPlReprt.Columns.Add("RevenueWithoutTax", GetType(Decimal))
            _dtPlReprt.Columns.Add("IwPartsConsumed", GetType(Decimal))
            _dtPlReprt.Columns.Add("TotalPartsConsumed", GetType(Decimal))
            _dtPlReprt.Columns.Add("PrimeCostTotal", GetType(Decimal))
            _dtPlReprt.Columns.Add("GrossProfit", GetType(Decimal))
            _dtPlReprt.Columns.Add("FinalPercentage", GetType(Decimal))

            Dim errFlg As Integer
            Try
                Log4NetControl.ComInfoLogWrite(Log4NetControl.UserID)

                'Dim activeData() As Class_analysis.ACTIVITY_REPORT
                Dim dsActivity_report As New DataSet
                Dim _dsActivity_report As New DataSet
                Dim strSQL2 As String = ""
                Dim dsSC_DSR_info As New DataSet
                Dim strSQL3 As String = ""

                'Dim ServiceCenterName As String

                strSQL2 &= "SELECT (CONVERT(DATETIME, month + '/' + day)) as day ,  DATENAME(WEEKDAY,(CONVERT(DATETIME, month + '/' + day)) ) as youbi, "
                strSQL2 &= "Customer_Visit, Call_Registerd, Repair_Completed,Goods_Delivered, Pending_Calls, Cancelled_Calls "
                strSQL2 &= "FROM dbo.Activity_report WHERE location = '" & shipCode & "' "
                strSQL2 &= "AND (CONVERT(DATETIME, month + '/' + day)) <= CONVERT(DATETIME, '" & ToDate & "') "
                strSQL2 &= "AND (CONVERT(DATETIME, month + '/' + day)) >= CONVERT(DATETIME, '" & FromDate & "') "
                strSQL2 &= "ORDER BY day;"

                _dsActivity_report = DBCommon.Get_DS(strSQL2, errFlg)



                Dim Date1 As New System.DateTime(2019, 11, 10, 0, 0, 0)
                Dim Date2 As New System.DateTime(2019, 11, 27, 0, 0, 0)
                Dim DateCur As New System.DateTime(2019, 11, 10, 0, 0, 0)
                Date1 = FromDate
                Date2 = ToDate
                DateCur = FromDate

                Dim dtActvitiyReport As New DataTable
                dtActvitiyReport.Columns.Add("day", GetType(Date))
                dtActvitiyReport.Columns.Add("youbi", GetType(String))
                dtActvitiyReport.Columns.Add("Customer_Visit", GetType(Integer))
                dtActvitiyReport.Columns.Add("Call_Registerd", GetType(Integer))
                dtActvitiyReport.Columns.Add("Repair_Completed", GetType(Integer))
                dtActvitiyReport.Columns.Add("Goods_Delivered", GetType(Integer))
                dtActvitiyReport.Columns.Add("Pending_Calls", GetType(Integer))
                dtActvitiyReport.Columns.Add("Cancelled_Calls", GetType(Integer))
                'Comment on 20200122 Added for Saw Discount calculate every day
                dtActvitiyReport.Columns.Add("Saw_Labor", GetType(Decimal))
                dtActvitiyReport.Columns.Add("Saw_Parts", GetType(Decimal))


                ''Find SAW Discount for 1st Day
                Dim decSawLabor As Decimal = 0.00
                Dim decSawParts As Decimal = 0.00





                Dim arrSawDiscount() As String
                arrSawDiscount = GetSawDiscount(DateCur, shipName, shipCode)
                decSawLabor = arrSawDiscount(0)
                decSawParts = arrSawDiscount(1)


                ''ByDefault Assign
                'decSawLabor = 0.00
                'decSawParts = 0.00



                dtActvitiyReport.Rows.Add(Date1, Date1.ToString("dddd"), 0, 0, 0, 0, 0, 0, decSawLabor, decSawParts)

                Do While DateCur <> Date2
                    DateCur = DateCur.AddDays(1)
                    '*****************************************
                    'Begin
                    decSawLabor = 0.00
                    decSawParts = 0.00
                    arrSawDiscount = GetSawDiscount(DateCur, shipName, shipCode)
                    decSawLabor = arrSawDiscount(0)
                    decSawParts = arrSawDiscount(1)
                    'End
                    '*****************************************
                    dtActvitiyReport.Rows.Add(DateCur, DateCur.ToString("dddd"), 0, 0, 0, 0, 0, 0, decSawLabor, decSawParts)

                    'lblText.Text = lblText.Text & DateCur & "==>" & DateCur.ToString("dddd") & "<br>"
                Loop

                Dim ResultsTable As New DataTable

                Try
                    ResultsTable = _dsActivity_report.Tables(0)
                    If (ResultsTable Is Nothing) Or (ResultsTable.Rows.Count = 0) Then
                    Else
                        Dim strDate As String = ""
                        For Each _MyDataRow In _dsActivity_report.Tables(0).Rows
                            strDate = _MyDataRow("day")
                            Dim MyDataRow As DataRow
                            For Each MyDataRow In dtActvitiyReport.Rows 'Dynamic Table
                                If MyDataRow("day") = strDate Then
                                    MyDataRow("Customer_Visit") = _MyDataRow("Customer_Visit")
                                    MyDataRow("Call_Registerd") = _MyDataRow("Call_Registerd")
                                    MyDataRow("Repair_Completed") = _MyDataRow("Repair_Completed")
                                    MyDataRow("Goods_Delivered") = _MyDataRow("Goods_Delivered")
                                    MyDataRow("Pending_Calls") = _MyDataRow("Pending_Calls")
                                    MyDataRow("Cancelled_Calls") = _MyDataRow("Cancelled_Calls")
                                    MyDataRow.AcceptChanges()
                                End If
                            Next
                        Next
                    End If
                Catch ex As Exception

                End Try



                dsActivity_report.Tables.Add(dtActvitiyReport)
                Dim intNumberOfCounters As Integer = 0
                Dim intNumberOfBusinessDat As Integer = 0
                Dim intNumberOfStaffs As Integer = 0

                Dim Total_Customer_Visit As Integer
                Dim Total_Call_Registerd As Integer
                Dim Total_Repair_Completed As Integer
                Dim Total_Goods_Delivered As Integer
                Dim Total_Pending_Calls As Integer
                Dim WarrantyNumber, WarrantyNumber1 As Decimal
                Dim InWarrantyNumber As Decimal
                Dim OutWarrantyNumber As Decimal
                Dim InWarrantyAmount, InWarrantyAmount1 As Decimal
                Dim InWarrantyLabour As Decimal
                Dim InWarrantyParts As Decimal
                Dim InWarrantyTransport As Decimal
                Dim InWarrantyOthers As Decimal
                Dim OutWarrantyAmount, OutWarrantyAmount1 As Decimal
                Dim OutWarrantyLabour As Decimal
                Dim OutWarrantyParts As Decimal
                Dim OutWarrantyTransport As Decimal
                Dim OutWarrantyOthers As Decimal
                Dim SAWDiscountLabour As Decimal
                Dim SAWDiscountParts As Decimal
                Dim OutWarrantyLabourwSAWDisc, OutWarrantyLabourwSAWDisc1 As Decimal
                Dim OutWarrantyPartswSAWDisc, OutWarrantyPartswSAWDisc1 As Decimal
                Dim RevenuewithoutTax, RevenuewithoutTax1 As Decimal
                Dim IWpartsconsumed, IWpartsconsumed1 As Decimal
                Dim TotalPartsConsumed, TotalPartsConsumed1 As Decimal
                Dim PrimeCostTotal, PrimeCostTotal1 As Decimal
                Dim GrossProfit, GrossProfit1 As Decimal
                Dim FinalPercentage, FinalPercentage1 As Decimal




                If dsActivity_report.Tables(0).Rows.Count > 0 Then
                    For i = 0 To dsActivity_report.Tables(0).Rows.Count - 1
                        Dim dr As DataRow
                        dr = dsActivity_report.Tables(0).Rows(i)
                        'rowWork1 &= activeData(i).day & ","
                        'rowWork2 &= activeData(i).youbi & ","
                        'rowWork3 &= activeData(i).Customer_Visit & ","
                        'rowWork4 &= activeData(i).Call_Registerd & ","
                        'rowWork5 &= activeData(i).Repair_Completed & ","
                        'rowWork6 &= activeData(i).Goods_Delivered & ","
                        'rowWork7 &= activeData(i).Pending_Calls & ","
                        'dsActivity_report.Tables(0).Rows(i).
                        If dr("Customer_Visit") IsNot DBNull.Value Then
                            Total_Customer_Visit = Total_Customer_Visit + Convert.ToInt16(dr("Customer_Visit"))
                        End If

                        If dr("Call_Registerd") IsNot DBNull.Value Then
                            Total_Call_Registerd = Total_Call_Registerd + Convert.ToInt16(dr("Call_Registerd"))
                        End If

                        If dr("Repair_Completed") IsNot DBNull.Value Then
                            Total_Repair_Completed = Total_Repair_Completed + Convert.ToInt16(dr("Repair_Completed"))
                        End If

                        If dr("Goods_Delivered") IsNot DBNull.Value Then
                            Total_Goods_Delivered = Total_Goods_Delivered + Convert.ToInt16(dr("Goods_Delivered"))
                        End If

                        If dr("Pending_Calls") IsNot DBNull.Value Then
                            Total_Pending_Calls = Total_Pending_Calls + Convert.ToInt16(dr("Pending_Calls"))
                        End If
                        'SAW Discount
                        If dr("Saw_Labor") IsNot DBNull.Value Then
                            SAWDiscountLabour = SAWDiscountLabour + Convert.ToDecimal(dr("Saw_Labor"))
                        End If
                        If dr("Saw_Parts") IsNot DBNull.Value Then
                            SAWDiscountParts = SAWDiscountParts + Convert.ToDecimal(dr("Saw_Parts"))
                        End If
                    Next i

                    'Dim drActyReport As DataRow

                    Dim drActyReport As DataRow = _dtPlReprt.NewRow
                    'No Functionality Define, Always zero
                    drActyReport("NumberOfCounters") = 0
                    drActyReport("NumberOfBusinessDat") = 0
                    drActyReport("NumberOfStaffs") = 0
                    'End

                    drActyReport("CustomerVisit") = Total_Customer_Visit
                    drActyReport("CallRegistered") = Total_Call_Registerd
                    drActyReport("RepairCompleted") = Total_Repair_Completed
                    drActyReport("GoodsDelivered") = Total_Goods_Delivered
                    drActyReport("Pending") = Total_Pending_Calls
                    'SAW Discount
                    drActyReport("SawDiscountLabour") = SAWDiscountLabour
                    drActyReport("SawDiscountParts") = SAWDiscountParts

                    'VJ 20201005 Added condition in Invoice update table for Labor and Parts

                    strSQL3 &= "SELECT * ,ISNULL((SELECT sum(Invoice_update.LABOR) FROM  Invoice_update where upload_Branch = '" & shipName.Trim() & "' AND Number = 'OWC' and Your_Ref_No in (  select  distinct ASC_Claim_No from Wty_Excel where Wty_Excel.DELFG!=1  AND  Wty_Excel.Branch_Code='" & shipCode.Trim() & "' and  (  (DAY(dbo.SC_DSR_info.Billing_date) = 1 and Wty_Excel.Delivery_Date between  LEFT(CONVERT(VARCHAR, DATEADD(D,-3,dbo.SC_DSR_info.Billing_date), 111), 10) and LEFT(CONVERT(VARCHAR, Billing_date, 111), 10) ) or (DAY(dbo.SC_DSR_info.Billing_date) != 1 and Wty_Excel.Delivery_Date between  LEFT(CONVERT(VARCHAR, Billing_date, 111), 10) and LEFT(CONVERT(VARCHAR, Billing_date, 111), 10) )))),'0') as 'labor', "
                    strSQL3 &= "ISNULL((SELECT sum(Invoice_update.Parts) FROM  Invoice_update where upload_Branch = '" & shipName.Trim() & "' AND Number = 'OWC' and Your_Ref_No in (  select  distinct ASC_Claim_No from Wty_Excel where Wty_Excel.DELFG!=1 AND Wty_Excel.Branch_Code='" & shipCode.Trim() & "' and  (  (DAY(dbo.SC_DSR_info.Billing_date) = 1 and Wty_Excel.Delivery_Date between  LEFT(CONVERT(VARCHAR, DATEADD(D,-3,dbo.SC_DSR_info.Billing_date), 111), 10) and LEFT(CONVERT(VARCHAR, Billing_date, 111), 10) ) or (DAY(dbo.SC_DSR_info.Billing_date) != 1 and Wty_Excel.Delivery_Date between  LEFT(CONVERT(VARCHAR, Billing_date, 111), 10) and LEFT(CONVERT(VARCHAR, Billing_date, 111), 10) )))),'0') as 'parts' "
                    strSQL3 &= " FROM dbo.SC_DSR_info "
                    strSQL3 &= "WHERE DELFG = 0 And Branch_name = '" & shipName.Trim() & "' "
                    strSQL3 &= "AND LEFT(CONVERT(VARCHAR, Billing_date, 111), 10) <= '" & ToDate & "' "
                    strSQL3 &= "AND LEFT(CONVERT(VARCHAR, Billing_date, 111), 10) >= '" & FromDate & "';"

                    dsSC_DSR_info = DBCommon.Get_DS(strSQL3, errFlg)

                    If errFlg = 1 Then
                        Call showMsg("Failed to get information on SC_DSR_info.")
                        'Return _dtPlReprt
                        Exit Function
                    End If

                    Try

                        For i = 0 To dsActivity_report.Tables(0).Rows.Count - 1

                            'Dim table As DataTable = dsSC_DSR_info.Tables(0)
                            Dim table As DataTable = dsSC_DSR_info.Tables(0)
                            Dim expression As String
                            expression = "Billing_date = '" & DateTime.Parse(dsActivity_report.Tables(0).Rows(i).ItemArray(0)) & "'"
                            Dim foundRows() As DataRow
                            ' Use the Select method to find all rows matching the filter.
                            foundRows = table.Select(expression)
                            If foundRows.Length = 1 Then

                                WarrantyNumber = WarrantyNumber + (Money_Round(foundRows(0).Item("IW_goods_total"), 2) + Money_Round(foundRows(0).Item("OW_goods_total"), 2))
                                InWarrantyNumber = InWarrantyNumber + Money_Round(foundRows(0).Item("IW_goods_total"), 2)
                                OutWarrantyNumber = OutWarrantyNumber + Money_Round(foundRows(0).Item("OW_goods_total"), 2)

                                InWarrantyAmount1 = Money_Round(foundRows(0).Item("IW_Labor_total"), 2) + Money_Round(foundRows(0).Item("IW_Transport_total"), 2) + Money_Round(foundRows(0).Item("IW_Others_total"), 2)
                                InWarrantyAmount = InWarrantyAmount + InWarrantyAmount1

                                InWarrantyLabour = InWarrantyLabour + Money_Round(foundRows(0).Item("IW_Labor_total"), 2)
                                InWarrantyParts = InWarrantyParts + Money_Round(foundRows(0).Item("IW_Parts_total"), 2)
                                InWarrantyTransport = InWarrantyTransport + Money_Round(foundRows(0).Item("IW_Transport_total"), 2)
                                InWarrantyOthers = InWarrantyOthers + Money_Round(foundRows(0).Item("IW_Others_total"), 2)

                                OutWarrantyAmount1 = Money_Round(foundRows(0).Item("OW_Labor_total"), 2) + Money_Round(foundRows(0).Item("OW_Parts_total"), 2) + Money_Round(foundRows(0).Item("OW_Transport_total"), 2) + Money_Round(foundRows(0).Item("OW_Others_total"), 2) + Money_Round(foundRows(0).Item("Labor"), 2) + Money_Round(foundRows(0).Item("Parts"), 2)
                                OutWarrantyAmount = OutWarrantyAmount + OutWarrantyAmount1

                                OutWarrantyLabour = OutWarrantyLabour + Money_Round(foundRows(0).Item("OW_Labor_total"), 2)
                                OutWarrantyParts = OutWarrantyParts + Money_Round(foundRows(0).Item("OW_Parts_total"), 2)
                                OutWarrantyTransport = OutWarrantyTransport + Money_Round(foundRows(0).Item("OW_Transport_total"), 2)
                                OutWarrantyOthers = OutWarrantyOthers + Money_Round(foundRows(0).Item("OW_Others_total"), 2)
                                'Comment on 20200122 - SAW discount calculated as above
                                'SAWDiscountLabour = SAWDiscountLabour + Money_Round(foundRows(0).Item("Labor"), 2)
                                'SAWDiscountParts = SAWDiscountParts + Money_Round(foundRows(0).Item("Parts"), 2)

                                OutWarrantyLabourwSAWDisc1 = Money_Round(foundRows(0).Item("OW_Labor_total"), 2) + Money_Round(foundRows(0).Item("Labor"), 2)
                                OutWarrantyLabourwSAWDisc = OutWarrantyLabourwSAWDisc + OutWarrantyLabourwSAWDisc1

                                OutWarrantyPartswSAWDisc1 = (Money_Round(foundRows(0).Item("OW_Parts_total"), 2) + Money_Round(foundRows(0).Item("Parts"), 2))
                                OutWarrantyPartswSAWDisc = OutWarrantyPartswSAWDisc + OutWarrantyPartswSAWDisc1

                                RevenuewithoutTax1 = InWarrantyAmount1 + OutWarrantyAmount1
                                RevenuewithoutTax = RevenuewithoutTax + RevenuewithoutTax1

                                IWpartsconsumed1 = Money_Round(foundRows(0).Item("IW_Parts_total") * Gm, 2)
                                IWpartsconsumed = IWpartsconsumed + IWpartsconsumed1

                                TotalPartsConsumed1 = Money_Round((foundRows(0).Item("IW_Parts_total") + OutWarrantyPartswSAWDisc1) * Gm, 2)
                                TotalPartsConsumed = TotalPartsConsumed + TotalPartsConsumed1

                                PrimeCostTotal1 = TotalPartsConsumed1 - IWpartsconsumed1
                                PrimeCostTotal = PrimeCostTotal + PrimeCostTotal1

                                GrossProfit1 = RevenuewithoutTax1 - PrimeCostTotal1
                                GrossProfit = GrossProfit + GrossProfit1

                                If RevenuewithoutTax1 = 0 Then
                                    FinalPercentage1 = RevenuewithoutTax1
                                Else
                                    FinalPercentage1 = comcontrol.Money_Round((GrossProfit1 / RevenuewithoutTax1) * 100, 2)
                                End If

                                'FinalPercentage1 = FinalPercentage1 & "%"

                                ' FinalPercentage1 = GrossProfit1 / RevenuewithoutTax1
                                FinalPercentage = FinalPercentage + FinalPercentage1
                            End If

                        Next
                    Catch ex As Exception

                    End Try
                    drActyReport("Warranty") = WarrantyNumber
                    drActyReport("InWarranty") = InWarrantyNumber
                    drActyReport("OutWarranty") = OutWarrantyNumber
                    drActyReport("InWarrantyAmount") = InWarrantyAmount
                    drActyReport("InWarrantyLabour") = InWarrantyLabour
                    drActyReport("InWarrantyParts") = InWarrantyParts
                    drActyReport("InWarrantyTransport") = InWarrantyTransport
                    drActyReport("InWarrantyOthers") = InWarrantyOthers
                    drActyReport("OutWarrantyAmount") = OutWarrantyAmount
                    drActyReport("OutWarrantyLabour") = OutWarrantyLabour
                    drActyReport("OutWarrantyParts") = OutWarrantyParts
                    drActyReport("OutWarrantyOthers") = OutWarrantyOthers
                    drActyReport("OutWarrantyTransport") = OutWarrantyTransport
                    'Comment on 20200122 SAW Discount is above
                    'drActyReport("SawDiscountLabour") = SAWDiscountLabour
                    'drActyReport("SawDiscountParts") = SAWDiscountParts
                    drActyReport("OutWarrantyLabourwSawDisc") = OutWarrantyLabourwSAWDisc

                    drActyReport("OutWarrantyPartswSawDisc") = OutWarrantyPartswSAWDisc
                    drActyReport("RevenueWithoutTax") = RevenuewithoutTax
                    drActyReport("IwPartsConsumed") = IWpartsconsumed
                    drActyReport("TotalPartsConsumed") = TotalPartsConsumed
                    drActyReport("PrimeCostTotal") = PrimeCostTotal
                    drActyReport("GrossProfit") = GrossProfit
                    drActyReport("FinalPercentage") = FinalPercentage

                    _dtPlReprt.Rows.Add(drActyReport)
                End If

                If errFlg = 1 Then
                    Call showMsg("Failed to get Activity_report information.")
                    'Return _dtPlReprt
                    Exit Function
                End If
            Catch ex As Exception
                Call showMsg(ex.Message)
            End Try
            Return _dtPlReprt

            'Comment on 20200121 , changed saw discount caculations 
            ''''''''''''Dim comcontrol As New CommonControl
            ''''''''''''Dim _dtPlReprt As New DataTable

            '''''''''''''Not Defined functionality , Always Zero
            ''''''''''''_dtPlReprt.Columns.Add("NumberOfCounters", GetType(Integer))
            ''''''''''''_dtPlReprt.Columns.Add("NumberOfBusinessDat", GetType(Integer))
            ''''''''''''_dtPlReprt.Columns.Add("NumberOfStaffs", GetType(Integer))
            '''''''''''''End
            ''''''''''''_dtPlReprt.Columns.Add("CustomerVisit", GetType(Integer))
            ''''''''''''_dtPlReprt.Columns.Add("CallRegistered", GetType(Integer))
            ''''''''''''_dtPlReprt.Columns.Add("RepairCompleted", GetType(Integer))
            ''''''''''''_dtPlReprt.Columns.Add("GoodsDelivered", GetType(Integer))
            ''''''''''''_dtPlReprt.Columns.Add("Pending", GetType(Integer))
            ''''''''''''_dtPlReprt.Columns.Add("Warranty", GetType(Integer))
            ''''''''''''_dtPlReprt.Columns.Add("InWarranty", GetType(Integer))
            ''''''''''''_dtPlReprt.Columns.Add("OutWarranty", GetType(Integer))
            ''''''''''''_dtPlReprt.Columns.Add("InWarrantyAmount", GetType(Decimal))
            ''''''''''''_dtPlReprt.Columns.Add("InWarrantyLabour", GetType(Decimal))
            ''''''''''''_dtPlReprt.Columns.Add("InWarrantyParts", GetType(Decimal))
            ''''''''''''_dtPlReprt.Columns.Add("InWarrantyTransport", GetType(Decimal))
            ''''''''''''_dtPlReprt.Columns.Add("InWarrantyOthers", GetType(Decimal))
            ''''''''''''_dtPlReprt.Columns.Add("OutWarrantyAmount", GetType(Decimal))
            ''''''''''''_dtPlReprt.Columns.Add("OutWarrantyLabour", GetType(Decimal))
            ''''''''''''_dtPlReprt.Columns.Add("OutWarrantyParts", GetType(Decimal))
            ''''''''''''_dtPlReprt.Columns.Add("OutWarrantyTransport", GetType(Decimal))
            ''''''''''''_dtPlReprt.Columns.Add("OutWarrantyOthers", GetType(Decimal))
            ''''''''''''_dtPlReprt.Columns.Add("SawDiscountLabour", GetType(Decimal))
            ''''''''''''_dtPlReprt.Columns.Add("SawDiscountParts", GetType(Decimal))
            ''''''''''''_dtPlReprt.Columns.Add("OutWarrantyLabourwSawDisc", GetType(Decimal))
            ''''''''''''_dtPlReprt.Columns.Add("OutWarrantyPartswSawDisc", GetType(Decimal))
            ''''''''''''_dtPlReprt.Columns.Add("RevenueWithoutTax", GetType(Decimal))
            ''''''''''''_dtPlReprt.Columns.Add("IwPartsConsumed", GetType(Decimal))
            ''''''''''''_dtPlReprt.Columns.Add("TotalPartsConsumed", GetType(Decimal))
            ''''''''''''_dtPlReprt.Columns.Add("PrimeCostTotal", GetType(Decimal))
            ''''''''''''_dtPlReprt.Columns.Add("GrossProfit", GetType(Decimal))
            ''''''''''''_dtPlReprt.Columns.Add("FinalPercentage", GetType(Decimal))

            ''''''''''''Dim errFlg As Integer
            ''''''''''''Try
            ''''''''''''    Log4NetControl.ComInfoLogWrite(Log4NetControl.UserID)

            ''''''''''''    'Dim activeData() As Class_analysis.ACTIVITY_REPORT
            ''''''''''''    Dim dsActivity_report As New DataSet
            ''''''''''''    Dim _dsActivity_report As New DataSet
            ''''''''''''    Dim strSQL2 As String = ""
            ''''''''''''    Dim dsSC_DSR_info As New DataSet
            ''''''''''''    Dim strSQL3 As String = ""

            ''''''''''''    'Dim ServiceCenterName As String

            ''''''''''''    strSQL2 &= "SELECT (CONVERT(DATETIME, month + '/' + day)) as day ,  DATENAME(WEEKDAY,(CONVERT(DATETIME, month + '/' + day)) ) as youbi, "
            ''''''''''''    strSQL2 &= "Customer_Visit, Call_Registerd, Repair_Completed,Goods_Delivered, Pending_Calls, Cancelled_Calls "
            ''''''''''''    strSQL2 &= "FROM dbo.Activity_report WHERE location = '" & shipCode & "' "
            ''''''''''''    strSQL2 &= "AND (CONVERT(DATETIME, month + '/' + day)) <= CONVERT(DATETIME, '" & ToDate & "') "
            ''''''''''''    strSQL2 &= "AND (CONVERT(DATETIME, month + '/' + day)) >= CONVERT(DATETIME, '" & FromDate & "') "
            ''''''''''''    strSQL2 &= "ORDER BY day;"

            ''''''''''''    _dsActivity_report = DBCommon.Get_DS(strSQL2, errFlg)



            ''''''''''''    Dim Date1 As New System.DateTime(2019, 11, 10, 0, 0, 0)
            ''''''''''''    Dim Date2 As New System.DateTime(2019, 11, 27, 0, 0, 0)
            ''''''''''''    Dim DateCur As New System.DateTime(2019, 11, 10, 0, 0, 0)
            ''''''''''''    Date1 = FromDate
            ''''''''''''    Date2 = ToDate
            ''''''''''''    DateCur = FromDate

            ''''''''''''    Dim dtActvitiyReport As New DataTable
            ''''''''''''    dtActvitiyReport.Columns.Add("day", GetType(Date))
            ''''''''''''    dtActvitiyReport.Columns.Add("youbi", GetType(String))
            ''''''''''''    dtActvitiyReport.Columns.Add("Customer_Visit", GetType(Integer))
            ''''''''''''    dtActvitiyReport.Columns.Add("Call_Registerd", GetType(Integer))
            ''''''''''''    dtActvitiyReport.Columns.Add("Repair_Completed", GetType(Integer))
            ''''''''''''    dtActvitiyReport.Columns.Add("Goods_Delivered", GetType(Integer))
            ''''''''''''    dtActvitiyReport.Columns.Add("Pending_Calls", GetType(Integer))
            ''''''''''''    dtActvitiyReport.Columns.Add("Cancelled_Calls", GetType(Integer))

            ''''''''''''    dtActvitiyReport.Rows.Add(Date1, Date1.ToString("dddd"), 0, 0, 0, 0, 0, 0)
            ''''''''''''    Do While DateCur <> Date2
            ''''''''''''        DateCur = DateCur.AddDays(1)
            ''''''''''''        dtActvitiyReport.Rows.Add(DateCur, DateCur.ToString("dddd"), 0, 0, 0, 0, 0, 0)

            ''''''''''''        'lblText.Text = lblText.Text & DateCur & "==>" & DateCur.ToString("dddd") & "<br>"
            ''''''''''''    Loop

            ''''''''''''    Dim ResultsTable As New DataTable

            ''''''''''''    Try
            ''''''''''''        ResultsTable = _dsActivity_report.Tables(0)
            ''''''''''''        If (ResultsTable Is Nothing) Or (ResultsTable.Rows.Count = 0) Then
            ''''''''''''        Else
            ''''''''''''            Dim strDate As String = ""
            ''''''''''''            For Each _MyDataRow In _dsActivity_report.Tables(0).Rows
            ''''''''''''                strDate = _MyDataRow("day")
            ''''''''''''                Dim MyDataRow As DataRow
            ''''''''''''                For Each MyDataRow In dtActvitiyReport.Rows 'Dynamic Table
            ''''''''''''                    If MyDataRow("day") = strDate Then
            ''''''''''''                        MyDataRow("Customer_Visit") = _MyDataRow("Customer_Visit")
            ''''''''''''                        MyDataRow("Call_Registerd") = _MyDataRow("Call_Registerd")
            ''''''''''''                        MyDataRow("Repair_Completed") = _MyDataRow("Repair_Completed")
            ''''''''''''                        MyDataRow("Goods_Delivered") = _MyDataRow("Goods_Delivered")
            ''''''''''''                        MyDataRow("Pending_Calls") = _MyDataRow("Pending_Calls")
            ''''''''''''                        MyDataRow("Cancelled_Calls") = _MyDataRow("Cancelled_Calls")
            ''''''''''''                        MyDataRow.AcceptChanges()
            ''''''''''''                    End If
            ''''''''''''                Next
            ''''''''''''            Next
            ''''''''''''        End If
            ''''''''''''    Catch ex As Exception

            ''''''''''''    End Try



            ''''''''''''    dsActivity_report.Tables.Add(dtActvitiyReport)
            ''''''''''''    Dim intNumberOfCounters As Integer = 0
            ''''''''''''    Dim intNumberOfBusinessDat As Integer = 0
            ''''''''''''    Dim intNumberOfStaffs As Integer = 0

            ''''''''''''    Dim Total_Customer_Visit As Integer
            ''''''''''''    Dim Total_Call_Registerd As Integer
            ''''''''''''    Dim Total_Repair_Completed As Integer
            ''''''''''''    Dim Total_Goods_Delivered As Integer
            ''''''''''''    Dim Total_Pending_Calls As Integer
            ''''''''''''    Dim WarrantyNumber, WarrantyNumber1 As Decimal
            ''''''''''''    Dim InWarrantyNumber As Decimal
            ''''''''''''    Dim OutWarrantyNumber As Decimal
            ''''''''''''    Dim InWarrantyAmount, InWarrantyAmount1 As Decimal
            ''''''''''''    Dim InWarrantyLabour As Decimal
            ''''''''''''    Dim InWarrantyParts As Decimal
            ''''''''''''    Dim InWarrantyTransport As Decimal
            ''''''''''''    Dim InWarrantyOthers As Decimal
            ''''''''''''    Dim OutWarrantyAmount, OutWarrantyAmount1 As Decimal
            ''''''''''''    Dim OutWarrantyLabour As Decimal
            ''''''''''''    Dim OutWarrantyParts As Decimal
            ''''''''''''    Dim OutWarrantyTransport As Decimal
            ''''''''''''    Dim OutWarrantyOthers As Decimal
            ''''''''''''    Dim SAWDiscountLabour As Decimal
            ''''''''''''    Dim SAWDiscountParts As Decimal
            ''''''''''''    Dim OutWarrantyLabourwSAWDisc, OutWarrantyLabourwSAWDisc1 As Decimal
            ''''''''''''    Dim OutWarrantyPartswSAWDisc, OutWarrantyPartswSAWDisc1 As Decimal
            ''''''''''''    Dim RevenuewithoutTax, RevenuewithoutTax1 As Decimal
            ''''''''''''    Dim IWpartsconsumed, IWpartsconsumed1 As Decimal
            ''''''''''''    Dim TotalPartsConsumed, TotalPartsConsumed1 As Decimal
            ''''''''''''    Dim PrimeCostTotal, PrimeCostTotal1 As Decimal
            ''''''''''''    Dim GrossProfit, GrossProfit1 As Decimal
            ''''''''''''    Dim FinalPercentage, FinalPercentage1 As Decimal




            ''''''''''''    If dsActivity_report.Tables(0).Rows.Count > 0 Then


            ''''''''''''        For i = 0 To dsActivity_report.Tables(0).Rows.Count - 1
            ''''''''''''            Dim dr As DataRow
            ''''''''''''            dr = dsActivity_report.Tables(0).Rows(i)
            ''''''''''''            'rowWork1 &= activeData(i).day & ","
            ''''''''''''            'rowWork2 &= activeData(i).youbi & ","
            ''''''''''''            'rowWork3 &= activeData(i).Customer_Visit & ","
            ''''''''''''            'rowWork4 &= activeData(i).Call_Registerd & ","
            ''''''''''''            'rowWork5 &= activeData(i).Repair_Completed & ","
            ''''''''''''            'rowWork6 &= activeData(i).Goods_Delivered & ","
            ''''''''''''            'rowWork7 &= activeData(i).Pending_Calls & ","
            ''''''''''''            'dsActivity_report.Tables(0).Rows(i).
            ''''''''''''            If dr("Customer_Visit") IsNot DBNull.Value Then
            ''''''''''''                Total_Customer_Visit = Total_Customer_Visit + Convert.ToInt16(dr("Customer_Visit"))
            ''''''''''''            End If

            ''''''''''''            If dr("Call_Registerd") IsNot DBNull.Value Then
            ''''''''''''                Total_Call_Registerd = Total_Call_Registerd + Convert.ToInt16(dr("Call_Registerd"))
            ''''''''''''            End If

            ''''''''''''            If dr("Repair_Completed") IsNot DBNull.Value Then
            ''''''''''''                Total_Repair_Completed = Total_Repair_Completed + Convert.ToInt16(dr("Repair_Completed"))
            ''''''''''''            End If

            ''''''''''''            If dr("Goods_Delivered") IsNot DBNull.Value Then
            ''''''''''''                Total_Goods_Delivered = Total_Goods_Delivered + Convert.ToInt16(dr("Goods_Delivered"))
            ''''''''''''            End If

            ''''''''''''            If dr("Pending_Calls") IsNot DBNull.Value Then
            ''''''''''''                Total_Pending_Calls = Total_Pending_Calls + Convert.ToInt16(dr("Pending_Calls"))
            ''''''''''''            End If

            ''''''''''''        Next i

            ''''''''''''        'Dim drActyReport As DataRow

            ''''''''''''        Dim drActyReport As DataRow = _dtPlReprt.NewRow
            ''''''''''''        'No Functionality Define, Always zero
            ''''''''''''        drActyReport("NumberOfCounters") = 0
            ''''''''''''        drActyReport("NumberOfBusinessDat") = 0
            ''''''''''''        drActyReport("NumberOfStaffs") = 0
            ''''''''''''        'End

            ''''''''''''        drActyReport("CustomerVisit") = Total_Customer_Visit
            ''''''''''''        drActyReport("CallRegistered") = Total_Call_Registerd
            ''''''''''''        drActyReport("RepairCompleted") = Total_Repair_Completed
            ''''''''''''        drActyReport("GoodsDelivered") = Total_Goods_Delivered
            ''''''''''''        drActyReport("Pending") = Total_Pending_Calls

            ''''''''''''        strSQL3 &= "SELECT * ,ISNULL((SELECT sum(Invoice_update.LABOR) FROM  Invoice_update where upload_Branch = '" & shipName.Trim() & "' and Your_Ref_No in (  select  distinct ASC_Claim_No from Wty_Excel where Wty_Excel.DELFG!=1  AND  Wty_Excel.Branch_Code='" & shipCode.Trim() & "' and  (  (DAY(dbo.SC_DSR_info.Billing_date) = 1 and Wty_Excel.Delivery_Date between  LEFT(CONVERT(VARCHAR, DATEADD(D,-3,dbo.SC_DSR_info.Billing_date), 111), 10) and LEFT(CONVERT(VARCHAR, Billing_date, 111), 10) ) or (DAY(dbo.SC_DSR_info.Billing_date) != 1 and Wty_Excel.Delivery_Date between  LEFT(CONVERT(VARCHAR, Billing_date, 111), 10) and LEFT(CONVERT(VARCHAR, Billing_date, 111), 10) )))),'0') as 'labor', "
            ''''''''''''        strSQL3 &= "ISNULL((SELECT sum(Invoice_update.Parts) FROM  Invoice_update where upload_Branch = '" & shipName.Trim() & "' and Your_Ref_No in (  select  distinct ASC_Claim_No from Wty_Excel where Wty_Excel.DELFG!=1 AND Wty_Excel.Branch_Code='" & shipCode.Trim() & "' and  (  (DAY(dbo.SC_DSR_info.Billing_date) = 1 and Wty_Excel.Delivery_Date between  LEFT(CONVERT(VARCHAR, DATEADD(D,-3,dbo.SC_DSR_info.Billing_date), 111), 10) and LEFT(CONVERT(VARCHAR, Billing_date, 111), 10) ) or (DAY(dbo.SC_DSR_info.Billing_date) != 1 and Wty_Excel.Delivery_Date between  LEFT(CONVERT(VARCHAR, Billing_date, 111), 10) and LEFT(CONVERT(VARCHAR, Billing_date, 111), 10) )))),'0') as 'parts' "
            ''''''''''''        strSQL3 &= " FROM dbo.SC_DSR_info "
            ''''''''''''        strSQL3 &= "WHERE DELFG = 0 And Branch_name = '" & shipName.Trim() & "' "
            ''''''''''''        strSQL3 &= "AND LEFT(CONVERT(VARCHAR, Billing_date, 111), 10) <= '" & ToDate & "' "
            ''''''''''''        strSQL3 &= "AND LEFT(CONVERT(VARCHAR, Billing_date, 111), 10) >= '" & FromDate & "';"

            ''''''''''''        dsSC_DSR_info = DBCommon.Get_DS(strSQL3, errFlg)

            ''''''''''''        If errFlg = 1 Then
            ''''''''''''            Call showMsg("Failed to get information on SC_DSR_info.")
            ''''''''''''            'Return _dtPlReprt
            ''''''''''''            Exit Function
            ''''''''''''        End If

            ''''''''''''        Try

            ''''''''''''            For i = 0 To dsActivity_report.Tables(0).Rows.Count - 1

            ''''''''''''                'Dim table As DataTable = dsSC_DSR_info.Tables(0)
            ''''''''''''                Dim table As DataTable = dsSC_DSR_info.Tables(0)
            ''''''''''''                Dim expression As String
            ''''''''''''                expression = "Billing_date = '" & DateTime.Parse(dsActivity_report.Tables(0).Rows(i).ItemArray(0)) & "'"
            ''''''''''''                Dim foundRows() As DataRow
            ''''''''''''                ' Use the Select method to find all rows matching the filter.
            ''''''''''''                foundRows = table.Select(expression)
            ''''''''''''                If foundRows.Length = 1 Then

            ''''''''''''                    WarrantyNumber = WarrantyNumber + (Money_Round(foundRows(0).Item("IW_goods_total"), 2) + Money_Round(foundRows(0).Item("OW_goods_total"), 2))
            ''''''''''''                    InWarrantyNumber = InWarrantyNumber + Money_Round(foundRows(0).Item("IW_goods_total"), 2)
            ''''''''''''                    OutWarrantyNumber = OutWarrantyNumber + Money_Round(foundRows(0).Item("OW_goods_total"), 2)

            ''''''''''''                    InWarrantyAmount1 = Money_Round(foundRows(0).Item("IW_Labor_total"), 2) + Money_Round(foundRows(0).Item("IW_Transport_total"), 2) + Money_Round(foundRows(0).Item("IW_Others_total"), 2)
            ''''''''''''                    InWarrantyAmount = InWarrantyAmount + InWarrantyAmount1

            ''''''''''''                    InWarrantyLabour = InWarrantyLabour + Money_Round(foundRows(0).Item("IW_Labor_total"), 2)
            ''''''''''''                    InWarrantyParts = InWarrantyParts + Money_Round(foundRows(0).Item("IW_Parts_total"), 2)
            ''''''''''''                    InWarrantyTransport = InWarrantyTransport + Money_Round(foundRows(0).Item("IW_Transport_total"), 2)
            ''''''''''''                    InWarrantyOthers = InWarrantyOthers + Money_Round(foundRows(0).Item("IW_Others_total"), 2)

            ''''''''''''                    OutWarrantyAmount1 = Money_Round(foundRows(0).Item("OW_Labor_total"), 2) + Money_Round(foundRows(0).Item("OW_Parts_total"), 2) + Money_Round(foundRows(0).Item("OW_Transport_total"), 2) + Money_Round(foundRows(0).Item("OW_Others_total"), 2) + Money_Round(foundRows(0).Item("Labor"), 2) + Money_Round(foundRows(0).Item("Parts"), 2)
            ''''''''''''                    OutWarrantyAmount = OutWarrantyAmount + OutWarrantyAmount1

            ''''''''''''                    OutWarrantyLabour = OutWarrantyLabour + Money_Round(foundRows(0).Item("OW_Labor_total"), 2)
            ''''''''''''                    OutWarrantyParts = OutWarrantyParts + Money_Round(foundRows(0).Item("OW_Parts_total"), 2)
            ''''''''''''                    OutWarrantyTransport = OutWarrantyTransport + Money_Round(foundRows(0).Item("OW_Transport_total"), 2)
            ''''''''''''                    OutWarrantyOthers = OutWarrantyOthers + Money_Round(foundRows(0).Item("OW_Others_total"), 2)
            ''''''''''''                    SAWDiscountLabour = SAWDiscountLabour + Money_Round(foundRows(0).Item("Labor"), 2)
            ''''''''''''                    SAWDiscountParts = SAWDiscountParts + Money_Round(foundRows(0).Item("Parts"), 2)

            ''''''''''''                    OutWarrantyLabourwSAWDisc1 = Money_Round(foundRows(0).Item("OW_Labor_total"), 2) + Money_Round(foundRows(0).Item("Labor"), 2)
            ''''''''''''                    OutWarrantyLabourwSAWDisc = OutWarrantyLabourwSAWDisc + OutWarrantyLabourwSAWDisc1

            ''''''''''''                    OutWarrantyPartswSAWDisc1 = (Money_Round(foundRows(0).Item("OW_Parts_total"), 2) + Money_Round(foundRows(0).Item("Parts"), 2))
            ''''''''''''                    OutWarrantyPartswSAWDisc = OutWarrantyPartswSAWDisc + OutWarrantyPartswSAWDisc1

            ''''''''''''                    RevenuewithoutTax1 = InWarrantyAmount1 + OutWarrantyAmount1
            ''''''''''''                    RevenuewithoutTax = RevenuewithoutTax + RevenuewithoutTax1

            ''''''''''''                    IWpartsconsumed1 = Money_Round(foundRows(0).Item("IW_Parts_total") * Gm, 2)
            ''''''''''''                    IWpartsconsumed = IWpartsconsumed + IWpartsconsumed1

            ''''''''''''                    TotalPartsConsumed1 = Money_Round((foundRows(0).Item("IW_Parts_total") + OutWarrantyPartswSAWDisc1) * Gm, 2)
            ''''''''''''                    TotalPartsConsumed = TotalPartsConsumed + TotalPartsConsumed1

            ''''''''''''                    PrimeCostTotal1 = TotalPartsConsumed1 - IWpartsconsumed1
            ''''''''''''                    PrimeCostTotal = PrimeCostTotal + PrimeCostTotal1

            ''''''''''''                    GrossProfit1 = RevenuewithoutTax1 - PrimeCostTotal1
            ''''''''''''                    GrossProfit = GrossProfit + GrossProfit1

            ''''''''''''                    If RevenuewithoutTax1 = 0 Then
            ''''''''''''                        FinalPercentage1 = RevenuewithoutTax1
            ''''''''''''                    Else
            ''''''''''''                        FinalPercentage1 = comcontrol.Money_Round((GrossProfit1 / RevenuewithoutTax1) * 100, 2)
            ''''''''''''                    End If

            ''''''''''''                    'FinalPercentage1 = FinalPercentage1 & "%"

            ''''''''''''                    ' FinalPercentage1 = GrossProfit1 / RevenuewithoutTax1
            ''''''''''''                    FinalPercentage = FinalPercentage + FinalPercentage1
            ''''''''''''                End If

            ''''''''''''            Next
            ''''''''''''        Catch ex As Exception

            ''''''''''''        End Try
            ''''''''''''        drActyReport("Warranty") = WarrantyNumber
            ''''''''''''        drActyReport("InWarranty") = InWarrantyNumber
            ''''''''''''        drActyReport("OutWarranty") = OutWarrantyNumber
            ''''''''''''        drActyReport("InWarrantyAmount") = InWarrantyAmount
            ''''''''''''        drActyReport("InWarrantyLabour") = InWarrantyLabour
            ''''''''''''        drActyReport("InWarrantyParts") = InWarrantyParts
            ''''''''''''        drActyReport("InWarrantyTransport") = InWarrantyTransport
            ''''''''''''        drActyReport("InWarrantyOthers") = InWarrantyOthers
            ''''''''''''        drActyReport("OutWarrantyAmount") = OutWarrantyAmount
            ''''''''''''        drActyReport("OutWarrantyLabour") = OutWarrantyLabour
            ''''''''''''        drActyReport("OutWarrantyParts") = OutWarrantyParts
            ''''''''''''        drActyReport("OutWarrantyOthers") = OutWarrantyOthers
            ''''''''''''        drActyReport("OutWarrantyTransport") = OutWarrantyTransport
            ''''''''''''        drActyReport("SawDiscountLabour") = SAWDiscountLabour
            ''''''''''''        drActyReport("SawDiscountParts") = SAWDiscountParts
            ''''''''''''        drActyReport("OutWarrantyLabourwSawDisc") = OutWarrantyLabourwSAWDisc

            ''''''''''''        drActyReport("OutWarrantyPartswSawDisc") = OutWarrantyPartswSAWDisc
            ''''''''''''        drActyReport("RevenueWithoutTax") = RevenuewithoutTax
            ''''''''''''        drActyReport("IwPartsConsumed") = IWpartsconsumed
            ''''''''''''        drActyReport("TotalPartsConsumed") = TotalPartsConsumed
            ''''''''''''        drActyReport("PrimeCostTotal") = PrimeCostTotal
            ''''''''''''        drActyReport("GrossProfit") = GrossProfit
            ''''''''''''        drActyReport("FinalPercentage") = FinalPercentage

            ''''''''''''        _dtPlReprt.Rows.Add(drActyReport)
            ''''''''''''    End If

            ''''''''''''    If errFlg = 1 Then
            ''''''''''''        Call showMsg("Failed to get Activity_report information.")
            ''''''''''''        'Return _dtPlReprt
            ''''''''''''        Exit Function
            ''''''''''''    End If
            ''''''''''''Catch ex As Exception
            ''''''''''''    Call showMsg(ex.Message)
            ''''''''''''End Try
            ''''''''''''Return _dtPlReprt

        End Function

        Public Function GetSawDiscount(strDateCur As String, shipName As String, shipCode As String) As String()
            Dim errFlg As Integer
            Dim decSawLabor As Decimal = 0.00
            Dim decSawParts As Decimal = 0.00
            Dim DateCur As New System.DateTime(2019, 11, 10, 0, 0, 0)
            Dim DateEnd1 As New System.DateTime(2019, 11, 10, 0, 0, 0)
            Dim DateEnd2 As New System.DateTime(2019, 11, 10, 0, 0, 0)
            Dim DateEnd3 As New System.DateTime(2019, 11, 10, 0, 0, 0)
            DateCur = strDateCur

            Dim intTotalDays As Integer = 0
            intTotalDays = System.DateTime.DaysInMonth(DateCur.Year, DateCur.Month)

            DateEnd1 = DateCur.Year & "/" & DateCur.Month & "/" & intTotalDays
            DateEnd2 = DateEnd1.AddDays(-1)
            DateEnd3 = DateEnd1.AddDays(-2)

            If DateCur = DateEnd1 Then
                'No need to check, assign default
            ElseIf DateCur = DateEnd2 Then
                'No need to check, assign default
            ElseIf DateCur = DateEnd3 Then
                'No need to check, assign default
            Else
                Dim strSQL4 As String = ""
                Dim dsActivityReportTmp As New DataSet

                'VJ - 2020-04-16 SSC parent child Add
                'Dim shipName As String = "'" & DropListLocation.SelectedItem.Text & "'"
                Dim shipNameDtl = "'" & shipName & "'"
                Dim shipdtlParent As DataRow() = _ShipBaseControl.SelectShipBaseDetails().Select("ship_name = '" & shipName & "' AND IsChildShip = 1 AND DELFG = 0")
                Dim shipdtlChild As DataRow() = _ShipBaseControl.SelectShipBaseDetails().Select("Parent_Ship_Name = '" & shipName & "' AND IsChildShip = 1 AND DELFG = 0")
                'Dim shipname As String
                If (shipdtlParent.Count() > 0) Then
                    For Each row As DataRow In shipdtlParent
                        shipNameDtl &= ",'" & row.Item("Parent_Ship_Name").ToString() & "'"
                        ' DataTable.ImportRow(row)
                    Next
                End If
                If (shipdtlChild.Count() > 0) Then
                    For Each row As DataRow In shipdtlChild
                        shipNameDtl &= ",'" & row.Item("ship_name").ToString() & "'"
                        'DataTable.ImportRow(row)
                    Next

                End If
                'VJ 20201005 Added condition in Invoice update table for Labor and Parts
                strSQL4 = "SELECT sum(Invoice_update.LABOR) as Labor,sum(Invoice_update.Parts) as Parts FROM  Invoice_update  "
                strSQL4 &= " where  "
                'The upload brach of invoice update is SSC1 and SSC2 are same . i.e uploadeded together in SSC1
                'If (shipName = "SSC1") Or (shipName = "SSC2") Then
                '    strSQL4 &= "  upload_Branch in ( 'SSC1','SSC2') and "
                'Else
                '    strSQL4 &= "  upload_Branch = '" & shipName & "' and "
                'End If
                strSQL4 &= "  upload_Branch in (" & shipNameDtl & ") AND Number = 'OWC' and "
                strSQL4 &= "  Your_Ref_No in (  select  distinct ASC_Claim_No from Wty_Excel where Wty_Excel.DELFG!=1  AND  Wty_Excel.Branch_Code='" & shipCode & "') and "
                strSQL4 &= "  Your_Ref_No in (  select  distinct ServiceOrder_No from SC_DSR where SC_DSR.DELFG!=1  AND  ( (DAY('" & DateCur & "') = 1 and SC_DSR.Billing_date between  LEFT(CONVERT(VARCHAR, DATEADD(D,-3,'" & DateCur & "'), 111), 10) and LEFT(CONVERT(VARCHAR, '" & DateCur & "', 111), 10) ) or (DAY('" & DateCur & "') != 1 and SC_DSR.Billing_date between  LEFT(CONVERT(VARCHAR, '" & DateCur & "', 111), 10) and LEFT(CONVERT(VARCHAR, '" & DateCur & "', 111), 10) )))"
                dsActivityReportTmp = DBCommon.Get_DS(strSQL4, errFlg)


                If errFlg <> 1 Then 'If other than Error
                    If dsActivityReportTmp IsNot Nothing Then
                        If dsActivityReportTmp.Tables(0).Rows.Count <> 0 Then
                            Dim dr1 As DataRow
                            For k = 0 To dsActivityReportTmp.Tables(0).Rows.Count - 1
                                dr1 = dsActivityReportTmp.Tables(0).Rows(k)

                                If dr1("Labor") IsNot DBNull.Value Then
                                    decSawLabor = dr1("Labor")
                                End If
                                If dr1("Parts") IsNot DBNull.Value Then
                                    decSawParts = dr1("Parts")
                                End If
                            Next
                        End If
                    End If
                Else ' If Error is occured, then default assign zero
                End If

            End If



            Return New String() {decSawLabor, decSawParts}
        End Function

    End Class


End Namespace
