Imports Ganges33.Ganges33.model
Imports Ganges33.Ganges33.logic
Public Class Analysis_Custody
    Inherits System.Web.UI.Page
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        '***初期処理***
        If IsPostBack = False Then

            '''''***セッションなしログインユーザの対応***
            ''''Dim userid As String = Session("user_id")
            ''''If userid = "" Then
            ''''    Response.Redirect("Login.aspx")
            ''''End If

            ''''Dim userLevel As String = Session("user_level")
            ''''Dim adminFlg As Boolean = Session("admin_Flg")

            ''''If userLevel = "0" Or userLevel = "1" Or userLevel = "2" Or userLevel = "3" Or adminFlg = True Then
            ''''    CheckFalse.Enabled = True
            ''''Else
            ''''    CheckFalse.Enabled = False
            ''''End If

            'Deafult Setting
            DefaultSetting()
        End If

    End Sub



    Public Sub DefaultSetting()


        'Enable Custody Add
        tblCustodayAdd.Visible = True
        TextCustomerName.Text = ""
        TextCustomerTelNo.Text = ""
        TextSumsungClaimNo.Text = ""
        TextProductsName.Text = ""
        TextCash.Text = ""
        txtKeepNo.Text = ""

        'Disable Search Setting
        tblCustodySearch.Visible = False
        'By Default Update button visible false
        imgUpdate.Visible = False


    End Sub

    ''' <summary>
    '''Search Custody
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Protected Sub imgSearch_Click(sender As Object, e As ImageClickEventArgs) Handles imgSearch.Click
        'Enable Custody Search
        tblCustodySearch.Visible = True


        'Disable Custody Add
        tblCustodayAdd.Visible = False
    End Sub

    ''' <summary>
    ''' Custody - Back
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Protected Sub imgBack_Click(sender As Object, e As ImageClickEventArgs) Handles imgBack.Click
        'Enable Custody Add
        tblCustodayAdd.Visible = True
        'Disable Custody Search
        tblCustodySearch.Visible = False
        'Clean Search condition and Result
        gvCustodyInfo.DataSource = Nothing
        gvCustodyInfo.DataBind()
        gvCustodyInfo.Visible = False
        txtAdvanceRefNo.Text = ""
        txtName.Text = ""
        txtTelephone.Text = ""
        'By Default update button visible is false
        imgUpdate.Visible = False
    End Sub

    ''' <summary>
    ''' Add Custody
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Protected Sub imgCustodyAdd_Click(sender As Object, e As ImageClickEventArgs) Handles imgCustodyAdd.Click
        'Validation for updation 
        'Storing Empty Error
        Dim strEmptCheck As String = ""
        Dim isValid As Boolean = True
        If (txtKeepNo.Text.Trim) = "" Then
            strEmptCheck = strEmptCheck & "Advance Ref No<br>"
            isValid = False
        End If
        If (TextCustomerName.Text.Trim) = "" Then
            strEmptCheck = strEmptCheck & "Customer Name<br>"
            isValid = False
        End If
        If (TextCustomerTelNo.Text.Trim) = "" Then
            strEmptCheck = strEmptCheck & "Telepnone No<br>"
            isValid = False
        End If
        If (TextCash.Text.Trim) = "" Then
            strEmptCheck = strEmptCheck & "Advance Amount<br>"
            isValid = False
        End If


        If Not isValid Then
            Call showMsg("The following fields are mandatory...<br>" & strEmptCheck)
            Exit Sub
        End If
        Dim userid As String = Session("user_id")
        Dim username As String = Session("user_Name")
        Dim shipname As String = Session("ship_Name")
        Dim shipmark As String = Session("ship_mark")
        Dim shipCode As String = Session("ship_code")
        'Intialize the object
        Dim _CustodyModel As CustodyModel = New CustodyModel()
        Dim _CustodyControl As CustodyControl = New CustodyControl()
        _CustodyModel.UserId = userid
        _CustodyModel.UserName = username
        ' CRTDT = dtNow  - no need to pass
        _CustodyModel.CRTCD = userid
        '[UPDDT] update user - no need to pass
        '[UPDCD]  update user - no need to pass
        _CustodyModel.UPDPG = "AnalysisCustody.aspx"
        _CustodyModel.DELFG = 0
        _CustodyModel.KeepNo = txtKeepNo.Text.Trim
        _CustodyModel.CustomerName = TextCustomerName.Text.Trim
        _CustodyModel.CustomerTel = TextCustomerTelNo.Text.Trim
        _CustodyModel.SamsungClaimNo = TextSumsungClaimNo.Text.Trim
        _CustodyModel.ProductName = TextProductsName.Text.Trim
        _CustodyModel.Cash = TextCash.Text.Trim
        _CustodyModel.ShipCode = shipCode
        _CustodyModel.TakeOut = 1
        If _CustodyControl.AddCashAdvance(_CustodyModel) Then
            Call showMsg("Successfully Updated of " & _CustodyModel.KeepNo)
            'Call default function
            DefaultSetting()

        Else
            Call showMsg("The system couldn't updated...")
        End If



    End Sub

    ''' <summary>
    '''Search Custody (GridView)
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Protected Sub imgCustodySearch_Click(sender As Object, e As ImageClickEventArgs) Handles imgCustodySearch.Click
        'Intialize the GridView
        gvCustodyInfo.DataSource = Nothing
        gvCustodyInfo.DataBind()
        gvCustodyInfo.Visible = False
        'Disable image update button
        imgUpdate.Visible = False

        '' tblSearchView.Visible = False
        'Empty Validation
        If (txtAdvanceRefNo.Text = "") And
                (txtName.Text = "") And
                (txtTelephone.Text = "") Then
            Call showMsg("Please enter atleast one search condition")
            Exit Sub
        End If
        Dim _CustodyModel As CustodyModel = New CustodyModel()
        Dim _CustodyControl As CustodyControl = New CustodyControl()
        _CustodyModel.KeepNo = txtAdvanceRefNo.Text.Trim
        _CustodyModel.CustomerName = txtName.Text.Trim
        _CustodyModel.CustomerTel = txtTelephone.Text.Trim

        Dim CustExistCnt As String = _CustodyControl.SelectCustodyCheck(_CustodyModel)
        'if No Datas found, then show the error message to the user
        If CustExistCnt = "0" Then
            Call showMsg("The custody Information Does Not Found !!!")
            Exit Sub
        End If
        GridViewBind()

    End Sub

    Protected Sub GridViewBind(ByVal Optional Location As String = "")
        Dim _CustodyModel As CustodyModel = New CustodyModel()
        Dim _CustodyControl As CustodyControl = New CustodyControl()
        _CustodyModel.KeepNo = txtAdvanceRefNo.Text.Trim
        _CustodyModel.CustomerName = txtName.Text.Trim
        _CustodyModel.CustomerTel = txtTelephone.Text.Trim
        Dim dtCustodyDetails As DataTable = _CustodyControl.SelectCustodyDetails(_CustodyModel)

        Dim userLevel As String = Session("user_level")
        Dim adminFlg As Boolean = Session("admin_Flg")
        If (dtCustodyDetails Is Nothing) Or (dtCustodyDetails.Rows.Count = 0) Then
            gvCustodyInfo.DataSource = Nothing
            gvCustodyInfo.DataBind()
            gvCustodyInfo.Visible = False
            ViewState("dtCustodyCount") = 0
        Else
            ViewState("dtCustodyCount") = gvCustodyInfo.Rows.Count
        End If
        gvCustodyInfo.DataSource = dtCustodyDetails
        ViewState("dtCustodyCount") = dtCustodyDetails
        gvCustodyInfo.DataBind()
        'For Verify if there is no data available for update
        Dim isUpdate As Boolean = False
        If userLevel = "0" Or userLevel = "1" Or userLevel = "2" Or userLevel = "3" Or adminFlg = True Then
            gvCustodyInfo.Columns(0).Visible = True
            Dim intRecordIndex As Integer = gvCustodyInfo.Rows.Count * gvCustodyInfo.PageIndex
            For Each target As GridViewRow In Me.gvCustodyInfo.Rows
                Dim hidTakeOut As HiddenField = CType(target.FindControl("hidTakeOut"), HiddenField)
                Dim chkTakeOut As CheckBox = CType(target.FindControl("chkTakeOut"), CheckBox)
                If UCase(hidTakeOut.Value) = "TRUE" Then
                    isUpdate = True
                Else
                    chkTakeOut.Checked = True
                    chkTakeOut.Enabled = False
                End If
            Next
        Else
            gvCustodyInfo.Columns(0).Visible = False
        End If

        If isUpdate Then
            imgUpdate.Visible = True
        Else
            imgUpdate.Visible = False
        End If

        If gvCustodyInfo.Rows.Count = 0 Then
            gvCustodyInfo.Visible = False
        Else
            gvCustodyInfo.Visible = True
        End If
    End Sub

    Protected Sub gvCustodyInfo_PageIndexChanging(ByVal sender As Object, ByVal e As GridViewPageEventArgs)
        Log4NetControl.ComInfoLogWrite(Session("UserName"))
        gvCustodyInfo.PageIndex = e.NewPageIndex
        GridViewBind()
    End Sub
    ''' <summary>
    ''' Custody - Update of Take out
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Protected Sub imgUpdate_Click(sender As Object, e As ImageClickEventArgs) Handles imgUpdate.Click
        Dim userid As String = Session("user_id")
        Dim lstModel As List(Of CustodyKeepModel) = New List(Of CustodyKeepModel)()
        'For Verify if there is data line is checked to update
        Dim isUpdateSelected As Boolean = False
        For i As Integer = 0 To Me.gvCustodyInfo.Rows.Count - 1
            Dim chkTakeOut As CheckBox = CType(Me.gvCustodyInfo.Rows(i).FindControl("chkTakeOut"), CheckBox)
            'Only Enabled 
            If chkTakeOut.Enabled Then
                If chkTakeOut.Checked Then
                    isUpdateSelected = True
                    Dim lblAdvanceRefNo As Label = CType(gvCustodyInfo.Rows(i).FindControl("lblAdvanceRefNo"), Label)
                    Dim targetModel As CustodyKeepModel = New CustodyKeepModel()
                    targetModel.KeepNo = lblAdvanceRefNo.Text
                    targetModel.UpdateUser = userid
                    lstModel.Add(targetModel)
                End If
            End If
        Next
        If isUpdateSelected Then
            Dim _CustodyControl As CustodyControl = New CustodyControl()
            If _CustodyControl.UpdateCashTakeOut(lstModel) Then
                Call showMsg("Successfully Updated")
                GridViewBind()
            Else
                Call showMsg("Failed to upload it")
            End If
        Else
            Call showMsg("There is no custody items are selected for the updation")
            Exit Sub
        End If

    End Sub


    Protected Sub showMsg(ByVal Msg As String)
        lblMsg.Text = Msg
        Dim sScript As String = "$(function () { $( ""#dialog"" ).dialog({width: 400, buttons: {""OK"": function () {$(this).dialog('close');}}});});"
        'JavaScriptの埋め込み
        ClientScript.RegisterStartupScript(Me.GetType(), "startup", sScript, True)

    End Sub

    ''''Protected Sub btnStart_Click(sender As Object, e As ImageClickEventArgs) Handles btnStart.Click

    ''''    '***入力チェック***
    ''''    Dim searchNo As String = Trim(TextSearchNo.Text)  'keep_no
    ''''    Dim searchName As String = Trim(TextSearchName.Text)
    ''''    Dim searchtel As String = Trim(TextSearchtel.Text)

    ''''    If searchNo = "" And searchName = "" And searchtel = "" Then
    ''''        Call showMsg("Please enter your search content.", "")
    ''''        Exit Sub
    ''''    End If

    ''''    '***FALSEチェック***
    ''''    Dim falseChk As Boolean
    ''''    If CheckFalse.Checked = True Then
    ''''        falseChk = True
    ''''    Else
    ''''        falseChk = False
    ''''    End If

    ''''    '***セッション設定***
    ''''    Session("search_No") = searchNo
    ''''    Session("search_Name") = searchName
    ''''    Session("search_tel") = searchtel
    ''''    Session("false_Chk") = falseChk

    ''''    Response.Redirect("Analysis_Custody_Search.aspx")

    ''''End Sub

    ''''Protected Sub btnSend_Click(sender As Object, e As ImageClickEventArgs) Handles btnSend.Click

    ''''    '***セッション取得***
    ''''    Dim userid As String = Session("user_id")

    ''''    If userid = "" Then
    ''''        Call showMsg("The session has expired. Please login again.", "")
    ''''        Exit Sub
    ''''    End If

    ''''    Dim shipCode As String = Session("ship_code")
    ''''    Dim userName As String = Session("user_Name")

    ''''    '***リセット***
    ''''    Call Reset()

    ''''    '***入力チェック/設定***
    ''''    Dim custodyData As Class_analysis.CUSTODY
    ''''    Dim intChk As Integer

    ''''    If TextCustomerName.Text = "" Then
    ''''        Call showMsg("Please enter customer information.", "")
    ''''        TextCustomerName.BackColor = System.Drawing.Color.FromArgb(255, 0, 0)
    ''''        Exit Sub
    ''''    Else
    ''''        custodyData.customer_name = Trim(TextCustomerName.Text)
    ''''    End If

    ''''    If TextCustomerTelNo.Text = "" Then
    ''''        Call showMsg("Please enter phone number.", "")
    ''''        TextCustomerTelNo.BackColor = System.Drawing.Color.FromArgb(255, 0, 0)
    ''''        Exit Sub
    ''''    Else
    ''''        custodyData.customer_tel = Trim(TextCustomerTelNo.Text)
    ''''    End If

    ''''    'cash欄の数値チェック
    ''''    If Trim(TextCash.Text) = "" Then
    ''''        custodyData.cash = 0.00
    ''''    Else
    ''''        If Integer.TryParse(Trim(TextCash.Text), intChk) = False Then
    ''''            TextCash.BackColor = System.Drawing.Color.FromArgb(255, 0, 0)
    ''''            Call showMsg("Please enter the amount.", "")
    ''''            Exit Sub
    ''''        End If
    ''''        custodyData.cash = Convert.ToInt32(Trim(TextCash.Text))
    ''''    End If

    ''''    custodyData.samsung_claim_no = Trim(TextSumsungClaimNo.Text)
    ''''    custodyData.product_name = Trim(TextProductsName.Text)

    ''''    '***keep_noの最終番号取得***
    ''''    Dim errFlg As Integer
    ''''    Dim keep_no_Max_int As Integer
    ''''    Dim strSQL As String = "SELECT MAX(keep_no) AS keep_no_Max FROM dbo.custody "
    ''''    strSQL &= "WHERE DELFG = 0 And ship_code = '" & shipCode & "';"
    ''''    Dim DT_custody As DataTable
    ''''    DT_custody = DBCommon.ExecuteGetDT(strSQL, errFlg)

    ''''    If errFlg = 1 Then
    ''''        Call showMsg("failed to acquire the last number of keep_no from custody.", "")
    ''''        Exit Sub
    ''''    End If

    ''''    If DT_custody IsNot Nothing Then
    ''''        If DT_custody.Rows(0)("keep_no_Max") IsNot DBNull.Value Then
    ''''            keep_no_Max_int = DT_custody.Rows(0)("keep_no_Max")
    ''''            keep_no_Max_int = keep_no_Max_int + 1
    ''''        End If
    ''''    End If

    ''''    If keep_no_Max_int = 0 Then
    ''''        custodyData.keep_no = 1
    ''''    Else
    ''''        custodyData.keep_no = keep_no_Max_int.ToString
    ''''    End If

    ''''    'custodyData.keep_no = txtKeepNo.Text

    ''''    '***登録***
    ''''    Dim clsSet As New Class_analysis
    ''''    Call clsSet.setCustody(custodyData, userid, userName, errFlg, shipCode)

    ''''    '***結果表示***
    ''''    If errFlg = 0 Then

    ''''        lblCustomerName.Text = custodyData.customer_name
    ''''        lblCustomerTelNo.Text = custodyData.customer_tel
    ''''        lblSumsungClaimNo.Text = custodyData.samsung_claim_no
    ''''        lblProductsName.Text = custodyData.product_name
    ''''        lblCash.Text = custodyData.cash
    ''''        lblKeepNo.Text = custodyData.keep_no

    ''''        '***total情報を反映***
    ''''        Dim strSQL2 As String = "SELECT SUM(cash) AS cash_SUM, COUNT(*) AS COUNT FROM dbo.custody "
    ''''        strSQL2 &= "WHERE DELFG = 0 AND ship_code = '" & shipCode & "' AND customer_name ='" & custodyData.customer_name & "' AND customer_tel = '" & custodyData.customer_tel & "' AND takeout = 1;"
    ''''        DT_custody = DBCommon.ExecuteGetDT(strSQL2, errFlg)

    ''''        If errFlg = 1 Then
    ''''            Call showMsg("Failed to acquire total cash, total number than custody.", "")
    ''''            Exit Sub
    ''''        End If

    ''''        Dim TotalCash As Decimal
    ''''        Dim TotalNumber As Integer

    ''''        If DT_custody IsNot Nothing Then

    ''''            If DT_custody.Rows(0)("cash_SUM") IsNot DBNull.Value Then
    ''''                TotalCash = DT_custody.Rows(0)("cash_SUM")
    ''''            End If

    ''''            If DT_custody.Rows(0)("COUNT") IsNot DBNull.Value Then
    ''''                TotalNumber = DT_custody.Rows(0)("COUNT")
    ''''            End If

    ''''        End If

    ''''        Dim clsSetMoney As New Class_money

    ''''        lblTotalCash.Text = clsSetMoney.setINR(TotalCash.ToString) & "INR"
    ''''        lblTotalNumber.Text = TotalNumber.ToString

    ''''        Call showMsg("Registration of custody is completed.", "")
    ''''        Call reSet2()

    ''''    Else
    ''''        Call showMsg("Registration of custody failed.", "")
    ''''        Exit Sub
    ''''    End If

    ''''End Sub

    ''''Protected Sub showMsg(ByVal Msg As String, ByVal msgChk As String)

    ''''    lblMsg.Text = Msg
    ''''    Dim sScript As String

    ''''    If msgChk = "CancelMsg" Then
    ''''        'OKとキャンセルボタン
    ''''        sScript = "$(function () {$(""#dialog"" ).dialog({width: 400,buttons:{""OK"": function () {$(this).dialog('close');$('[id$=""BtnOK""]').click();},""CANCEL"": function () {$(this).dialog('close');$('[id$=""BtnCancel""]').click();}}});});"
    ''''    Else
    ''''        'OKボタンのみ
    ''''        sScript = "$(function () { $( ""#dialog"" ).dialog({width: 400, buttons: {""OK"": function () {$(this).dialog('close');}}});});"
    ''''    End If

    ''''    'JavaScriptの埋め込み
    ''''    ClientScript.RegisterStartupScript(Me.GetType(), "startup", sScript, True)

    ''''End Sub
    '''''登録前リセット処理
    ''''Protected Sub reSet()

    ''''    TextCustomerName.BackColor = System.Drawing.Color.FromArgb(255, 255, 255)
    ''''    TextCustomerTelNo.BackColor = System.Drawing.Color.FromArgb(255, 255, 255)
    ''''    TextCash.BackColor = System.Drawing.Color.FromArgb(255, 255, 255)

    ''''    lblCustomerName.Text = ""
    ''''    lblCustomerTelNo.Text = ""
    ''''    lblSumsungClaimNo.Text = ""
    ''''    lblProductsName.Text = ""
    ''''    lblCash.Text = ""
    ''''    lblKeepNo.Text = ""
    ''''    lblTotalCash.Text = ""
    ''''    lblTotalNumber.Text = ""

    ''''End Sub
    '''''登録後リセット処理
    ''''Protected Sub reSet2()

    ''''    TextCustomerName.Text = ""
    ''''    TextCustomerTelNo.Text = ""
    ''''    TextSumsungClaimNo.Text = ""
    ''''    TextProductsName.Text = ""
    ''''    TextCash.Text = ""

    ''''End Sub

End Class
