Imports System.IO
Imports System.Text
Imports System.Globalization
Imports Ganges33.Ganges33.model
Imports Ganges33.Ganges33.logic
Public Class Analysis_Parts_Compare
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

            InitDropDownList()

            'Default Year
            DropDownYear.SelectedValue = Now.Year
            'Default Month
            DropDownMonth.SelectedValue = DateTime.Now.AddMonths(-1).Month 'Now.Month



        End If
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

    Protected Sub btnSend_Click(sender As Object, e As EventArgs) Handles btnSend.Click


        If DropListLocation.Text = "Select Branch" Then
            Call showMsg("Please specify Branch.", "")
            Exit Sub
        End If

        'By default
        tblPartsCompare.Visible = False

        Dim ShipName As String = Session("ship_Name")
        Dim shipCode As String = Session("ship_code")
        Dim userName As String = Session("user_Name")
        Dim userid As String = Session("user_id")
        Dim uploadShipname As String = DropListLocation.Text
        Dim listHistoryMsg() As String = Session("list_History_Msg")

        Dim _StockOverviewModel As StockOverviewModel = New StockOverviewModel()
        Dim _StockOverviewControl As StockOverviewControl = New StockOverviewControl()
        Dim strFileName As String = ""
        _StockOverviewModel.UserId = userid
        _StockOverviewModel.UserName = userName
        _StockOverviewModel.ShipToBranchCode = DropListLocation.SelectedItem.Value
        _StockOverviewModel.strMonth = DropDownYear.SelectedItem.Value & "/" & DropDownMonth.SelectedItem.Value

        Dim dtStockOverview As DataTable = _StockOverviewControl.SelectStockOverviewCountDisplay(_StockOverviewModel)
        If (dtStockOverview Is Nothing) Or (dtStockOverview.Rows.Count = 0) Then
            'If no Records
            tblPartsCompare.Visible = False
            Call showMsg("No records found for the month of " & DropDownMonth.SelectedItem.Text, "")
            Exit Sub
        Else
            ''''lblUpdatedDate.Text = dtStockOverview.Rows(0)("LATEST_MODIFY_DATE")

            ''''If lblUpdatedDate.Text = "1900/01/01" Then
            ''''    'If no Records
            ''''    tblPartsCompare.Visible = False
            ''''    Call showMsg("No records found for the month of " & DropDownMonth.SelectedItem.Text, "")
            ''''    Exit Sub
            ''''Else
            ''''    tblPartsCompare.Visible = True


            ''''    lblTotalRecordsCount.Text = dtStockOverview.Rows(0)("TOTAL_RECORDS")
            ''''    lblTotalCountParts.Text = dtStockOverview.Rows(0)("R_TOTAL_STOCK_QTY")
            ''''    lblStockOverViewGSPN.Text = dtStockOverview.Rows(0)("TOTAL_STOCK_QTY")
            ''''    lblMatchedPartsRecord.Text = dtStockOverview.Rows(0)("TOTAL_STOCK_QTY")


            ''''    lblUpdatedDate.Text = dtStockOverview.Rows(0)("LATEST_MODIFY_DATE")
            ''''    lblLastUpdatedUser.Text = dtStockOverview.Rows(0)("UPDCD")
            ''''End If
            '''


            Dim intTotalStockQty, intSumTotalStockQty As Int16
            Dim intWarehouseStockQty, intSumWarehouseStockQty As Int16
            Dim intEngineerStockQty, intSumEngineerStockQty As Int16
            Dim intDifference, intSumDifference As Int16
            Dim intRtotalStockQty, intSumRtotalStockQty As Int16
            Dim intShelfStockQty, intSumShelfStockQty As Int16
            Dim intEgOtherStockQty, intSumEgOtherStockQty As Int16
            Dim strLatestModifyDate As String = ""
            Dim strUpdcd As String = ""

            intTotalStockQty = 0
            intWarehouseStockQty = 0
            intEngineerStockQty = 0
            intDifference = 0
            intRtotalStockQty = 0
            intShelfStockQty = 0
            intEgOtherStockQty = 0

            intSumTotalStockQty = 0
            intSumWarehouseStockQty = 0
            intSumEngineerStockQty = 0
            intSumDifference = 0
            intSumRtotalStockQty = 0
            intSumShelfStockQty = 0
            intSumEgOtherStockQty = 0


            Dim IntMatched As Int16 = 0
            Dim intUnMatched As Int16 = 0
            Dim intNegative As Int16 = 0
            Dim intSumNegative As Int16 = 0
            Dim intPositive As Int16 = 0
            Dim intSumPositive As Int16 = 0

            Dim intTotalRecordCount As Int16 = 0
            '1) total count record ----------- lblTotalRecords
            '2) total count parts--------------lblTotalCountParts
            '3) StockoverView number(GSPN)---- lblStockOverView
            '4) match parts record------------ lblMatchPartsRecord
            '5) unmatch parts record---------- lblUnmatchPartsRecord
            '6) different record + ----------- lblPositiveRecord
            '7) different number +------------ lblPositiveRecordSum
            '8) different record- ------------- lblNegativeRecord
            '9) different number - ------------ lblNegativeRecordSum

            intSumTotalStockQty = 0

            For Each row As DataRow In dtStockOverview.Rows
                'TOTAL_RECORD_COUNT
                intTotalRecordCount = intTotalRecordCount + 1

                'TOTAL_STOCK_QTY
                intTotalStockQty = row("TOTAL_STOCK_QTY")
                intSumTotalStockQty = intTotalStockQty + intSumTotalStockQty

                'WAREHOUSE_STOCK_QTY
                intWarehouseStockQty = row("WAREHOUSE_STOCK_QTY")
                intSumWarehouseStockQty = intWarehouseStockQty + intSumWarehouseStockQty

                'ENGINEER_STOCK_QTY
                intEngineerStockQty = row("ENGINEER_STOCK_QTY")
                intSumEngineerStockQty = intEngineerStockQty + intSumEngineerStockQty

                'DIFFERENCE
                intDifference = row("DIFFERENCE")
                If intDifference = 0 Then ' Other than Zero is unmatched
                    IntMatched = IntMatched + 1
                Else
                    intUnMatched = intUnMatched + 1
                    If (intDifference < 0) Then
                        intNegative = intNegative + 1
                        intSumNegative = intSumNegative + intDifference '''
                    ElseIf (intDifference > 0) Then
                        intPositive = intPositive + 1
                        intSumPositive = intSumPositive + intDifference
                    End If
                End If


                'R_TOTAL_STOCK_QTY
                intRtotalStockQty = row("R_TOTAL_STOCK_QTY")
                intSumRtotalStockQty = intRtotalStockQty + intSumRtotalStockQty

                'SHELF_STOCK_QTY
                intShelfStockQty = row("SHELF_STOCK_QTY")
                intSumShelfStockQty = intShelfStockQty + intSumShelfStockQty

                'EG_OTHER_STOCK_QTY
                intEgOtherStockQty = row("EG_OTHER_STOCK_QTY")
                intSumEgOtherStockQty = intEgOtherStockQty + intSumEgOtherStockQty

                ' LATEST_MODIFY_DATE
                strLatestModifyDate = row("LATEST_MODIFY_DATE")

                ' UPDCD 
                strUpdcd = row("UPDCD")

            Next row
            'Display All the records 
            If intTotalRecordCount > 0 Then
                tblPartsCompare.Visible = True
                lblTotalRecords.Text = intTotalRecordCount ' COUNT(*)
                lblTotalCountParts.Text = intSumRtotalStockQty 'R_TOTAL_STOCK_QTY
                lblStockOverView.Text = intSumTotalStockQty ' TOTAL_STOCK_QTY
                lblMatchPartsRecord.Text = IntMatched ' == 0
                lblUnmatchPartsRecord.Text = intUnMatched ' != 0
                lblPositiveRecord.Text = intPositive ' >0 --- Record Count
                lblPositiveRecordSum.Text = intSumPositive ' > 0 Sum of Record Count
                lblNegativeRecord.Text = intNegative '< 0 --- Record count
                lblNegativeRecordSum.Text = intSumNegative '< 0 --- Sum of Record count

                lblResultMont.Text = DropDownYear.SelectedItem.Value & "/" & DropDownMonth.SelectedItem.Value
                lblLastUpdatedUser.Text = strUpdcd
                lblUpdatedDate.Text = strLatestModifyDate
            End If

        End If


    End Sub

    Protected Sub btnExport_Click(sender As Object, e As EventArgs) Handles btnExport.Click


        If DropListLocation.Text = "Select Branch" Then
            Call showMsg("Please specify Branch.", "")
            Exit Sub
        End If


        Dim ShipName As String = Session("ship_Name")
        Dim shipCode As String = Session("ship_code")
        Dim userName As String = Session("user_Name")
        Dim userid As String = Session("user_id")
        Dim uploadShipname As String = DropListLocation.Text
        Dim listHistoryMsg() As String = Session("list_History_Msg")

        Dim _StockOverviewModel As StockOverviewModel = New StockOverviewModel()
        Dim _StockOverviewControl As StockOverviewControl = New StockOverviewControl()
        Dim strFileName As String = DropListLocation.SelectedItem.Text & "_" & DropDownYear.SelectedItem.Value & DropDownMonth.SelectedItem.Value & ".csv"
        _StockOverviewModel.UserId = userid
        _StockOverviewModel.UserName = userName
        _StockOverviewModel.ShipToBranchCode = DropListLocation.SelectedItem.Value
        _StockOverviewModel.strMonth = DropDownYear.SelectedItem.Value & "/" & DropDownMonth.SelectedItem.Value

        Dim dtStockOverview As DataTable = _StockOverviewControl.SelectStockOverviewCountExport(_StockOverviewModel)
        If (dtStockOverview Is Nothing) Or (dtStockOverview.Rows.Count = 0) Then
            'If no Records
            Call showMsg("No records found for the month of " & DropDownMonth.SelectedItem.Text, "")
            Exit Sub
        Else

            Response.ContentType = "text/csv"
            Response.AddHeader("Content-Disposition", "attachment;filename=" & strFileName)
            Response.AddHeader("Pragma", "no-cache")
            Response.AddHeader("Cache-Control", "no-cache")
            Dim myData As Byte() = CommonControl.csvBytesWriter(dtStockOverview)
            Response.BinaryWrite(myData)
            Response.Flush()
            Response.SuppressContent = True
            HttpContext.Current.ApplicationInstance.CompleteRequest()
        End If
    End Sub
End Class