Imports System.Globalization
Imports System.IO
Imports Ganges33.Ganges33.model
Imports Ganges33.Ganges33.logic

Public Class inv_corporation_regi
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        If Not IsPostBack Then
            showMenu()
            lstListLocationView.Attributes.Add("disabled", "")
            lstListLocationSearchView.Attributes.Add("disabled", "")
            'InitList()
        End If
        'user level 0~3
        Dim userLevel As String = Session("user_level")
        If (userLevel = CommonConst.UserLevel0) Or
                       (userLevel = CommonConst.UserLevel1) Or
                       (userLevel = CommonConst.UserLevel2) Or
               (userLevel = CommonConst.UserLevel3) Then
            'Limited Access
            imgCashOnSaleEdit.Visible = True
        Else
            imgCashOnSaleEdit.Visible = False
            disableAll()
        End If

    End Sub
    Protected Sub imgCorporateRegistration_Click(sender As Object, e As ImageClickEventArgs) Handles imgCorporateRegistration.Click
        Dim userName As String = Session("user_Name")
        Dim userid As String = Session("user_id")
        disableAll()
        tblCorporateRegi.Visible = True
        lstListLocationAdd.Items.Clear()
        InitList("add")
    End Sub

    Protected Sub imgCorporateEdit_Click(sender As Object, e As ImageClickEventArgs) Handles imgCorporateEdit.Click
        Dim userName As String = Session("user_Name")
        Dim userid As String = Session("user_id")
        disableAll()
        tblCorporateEdit.Visible = True
        lstListLocationEdit.Items.Clear()
        InitList("edit")
    End Sub
    Protected Sub imgCorporateInformation_Click(sender As Object, e As ImageClickEventArgs) Handles imgCorporateInformation.Click
        Dim userName As String = Session("user_Name")
        Dim userid As String = Session("user_id")
        disableAll()
        tblCorporateInformation.Visible = True
        lstListLocationView.Items.Clear()
        InitList("view")
    End Sub
    Protected Sub imgCorporateSearch_Click(sender As Object, e As ImageClickEventArgs) Handles imgCorporateSearch.Click
        Dim userName As String = Session("user_Name")
        Dim userid As String = Session("user_id")
        disableAll()
        tblCorporateSearch.Visible = True
        InitList("search")
        SearchIntialize()
    End Sub
    Protected Sub imgCashOnSaleAdd_Click(sender As Object, e As ImageClickEventArgs) Handles imgCashOnSaleAdd.Click
        Dim userName As String = Session("user_Name")
        Dim userid As String = Session("user_id")
        disableAll()
        tblCashOnSaleAdd.Visible = True
        InitList("corpsaleadd")
    End Sub
    Protected Sub imgCashOnSaleEdit_Click(sender As Object, e As ImageClickEventArgs) Handles imgCashOnSaleEdit.Click
        Dim userName As String = Session("user_Name")
        Dim userid As String = Session("user_id")
        disableAll()
        tblCashOnSaleEdit.Visible = True
        InitList("corpsaleedit")
    End Sub
    Protected Sub imgCashOnSaleSearch_Click(sender As Object, e As ImageClickEventArgs) Handles imgCashOnSaleSearch.Click
        Dim userName As String = Session("user_Name")
        Dim userid As String = Session("user_id")
        disableAll()
        tblCashOnSaleSearch.Visible = True
        InitList("corpsalesearch")
    End Sub
    Protected Sub imgCreateInvoice_Click(sender As Object, e As ImageClickEventArgs) Handles imgCreateInvoice.Click
        Dim userName As String = Session("user_Name")
        Dim userid As String = Session("user_id")
        disableAll()
        tblCreateInvoiceSearch.Visible = True
        InitList("createinvoice")

    End Sub

    Protected Sub imgBack1_Click(sender As Object, e As ImageClickEventArgs) Handles imgBack1.Click
        disableAll()
        showMenu()
    End Sub
    Protected Sub imgBack2_Click(sender As Object, e As ImageClickEventArgs) Handles imgBack2.Click
        disableAll()
        showMenu()
    End Sub
    Protected Sub imgBack3_Click(sender As Object, e As ImageClickEventArgs) Handles imgBack3.Click
        disableAll()
        showMenu()
    End Sub
    Protected Sub imgBack4_Click(sender As Object, e As ImageClickEventArgs) Handles imgBack4.Click
        disableAll()
        showMenu()
    End Sub
    Protected Sub imgBack5_Click(sender As Object, e As ImageClickEventArgs) Handles imgBack5.Click
        disableAll()
        showMenu()
    End Sub
    Protected Sub imgBack6_Click(sender As Object, e As ImageClickEventArgs) Handles imgBack6.Click
        disableAll()
        showMenu()
    End Sub
    Protected Sub imgBack7_Click(sender As Object, e As ImageClickEventArgs) Handles imgBack7.Click
        disableAll()
        showMenu()
    End Sub

    Protected Sub imgBack8_Click(sender As Object, e As ImageClickEventArgs) Handles imgBack8.Click
        disableAll()
        showMenu()
    End Sub


    Public Sub showMenu()
        'Title show
        tblMenu.Visible = True
        tblCorporateRegi.Visible = False
        tblCorporateEdit.Visible = False
        tblCorporateInformation.Visible = False
        tblCorporateSearch.Visible = False
        'Need display when the search condition is satisfy
        tblSearchView.Visible = False

        'Cash On Sale
        tblCashOnSaleAdd.Visible = False
        tblCashOnSaleEdit.Visible = False
        tblCashOnSaleEditView.Visible = False
        tblCashOnSaleSearch.Visible = False
        tblCashOnSaleSearchView.Visible = False

        'Create Invoice
        tblCreateInvoiceSearch.Visible = False
        tblCreateInvoiceSearchView.Visible = False

    End Sub

    Public Sub disableAll()
        tblMenu.Visible = False
        tblCorporateRegi.Visible = False
        tblCorporateEdit.Visible = False
        tblCorporateInformation.Visible = False
        tblCorporateSearch.Visible = False
        'Need display when the search condition is satisfy
        tblSearchView.Visible = False
        tblCorporateEditResult.Visible = False
        tblCorporateViewResult.Visible=False 
        'Need to empty the search textbox always
        txtCorpNumberEdit.Text = ""
        txtCorpNumberView.Text = ""


        '*********
        ' Cash On Sale 
        '*************
        tblCashOnSaleAdd.Visible = False
        tblCashOnSaleEdit.Visible = False
        tblCashOnSaleEditView.Visible = False
        'Need to empty the search textbox always
        txtClaimNoSearch.Text = ""
        txtUserName.Text = ""
        txtPassword.Text = ""

        tblCashOnSaleSearch.Visible = False
        tblCashOnSaleSearchView.Visible = False
        'Need to empty the search textbox always
        txtCashOnSaleSearchFrom.Text = ""
        txtCashOnSaleSearchTo.Text = ""
        txtCashOnSaleSearchCustomerName.Text = ""
        txtCashOnSaleSearchAscClaimNo.Text = ""
        txtCashOnSaleTotAmt.Text = ""
        txtCashOnSaleSearchCorpNumber.Text = ""

        '**************
        'Create Invoice
        '*************
        tblCreateInvoiceSearch.Visible = False
        tblCreateInvoiceSearchView.Visible = False
        'Need to empty the search textbox always
        txtCreateInvoiceFrom.Text = ""
        txtCreateInvoiceTo.Text = ""

    End Sub


    Protected Sub showMsg(ByVal Msg As String)
        lblMsg.Text = Msg
        Dim sScript As String = "$(function () { $( ""#dialog"" ).dialog({width: 400, buttons: {""OK"": function () {$(this).dialog('close');}}});});"
        'JavaScriptの埋め込み
        ClientScript.RegisterStartupScript(Me.GetType(), "startup", sScript, True)

    End Sub
    Private Sub InitList(ByVal Optional strMode As String = "Nothing")
        Log4NetControl.ComInfoLogWrite(Session("UserName"))
        Dim userName As String = Session("user_id") 'Session("user_Name")

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

        If UCase(strMode) = "ADD" Then
            Me.lstListLocationAdd.DataSource = codeMasterList
            Me.lstListLocationAdd.DataTextField = "CodeDispValue"
            Me.lstListLocationAdd.DataValueField = "CodeValue"
            Me.lstListLocationAdd.DataBind()
        ElseIf UCase(strMode) = "EDIT" Then
            Me.lstListLocationEdit.DataSource = codeMasterList
            Me.lstListLocationEdit.DataTextField = "CodeDispValue"
            Me.lstListLocationEdit.DataValueField = "CodeValue"
            Me.lstListLocationEdit.DataBind()
        ElseIf UCase(strMode) = "VIEW" Then
            Me.lstListLocationView.DataSource = codeMasterList
            Me.lstListLocationView.DataTextField = "CodeDispValue"
            Me.lstListLocationView.DataValueField = "CodeValue"
            Me.lstListLocationView.DataBind()


        ElseIf UCase(strMode) = "SEARCH" Then
            Me.lstListLocationSearchView.DataSource = codeMasterList
            Me.lstListLocationSearchView.DataTextField = "CodeDispValue"
            Me.lstListLocationSearchView.DataValueField = "CodeValue"
            Me.lstListLocationSearchView.DataBind()
        ElseIf UCase(strMode) = "CORPSALEADD" Then
            Dim codeMasterList2 As List(Of CodeMasterModel) = codeMasterControl.SelectCorpMaster()
            Me.drpCashOnSaleCorp.DataSource = codeMasterList2
            Me.drpCashOnSaleCorp.DataTextField = "CodeDispValue"
            Me.drpCashOnSaleCorp.DataValueField = "CodeValue"
            Me.drpCashOnSaleCorp.DataBind()

            'Load Sales 
            Dim _CashOnSaleControl As CashOnSaleControl = New CashOnSaleControl()
            Dim cashOnSalePending As List(Of CodeMasterModel) = _CashOnSaleControl.SelectCashOnSalePending()
            Me.lstClaimNo.DataSource = cashOnSalePending
            Me.lstClaimNo.DataTextField = "CodeDispValue"
            Me.lstClaimNo.DataValueField = "CodeValue"
            Me.lstClaimNo.DataBind()
        ElseIf UCase(strMode) = "CORPSALEEDIT" Then
            Dim codeMasterList2 As List(Of CodeMasterModel) = codeMasterControl.SelectCorpMaster()
            Me.drpCashOnSaleCorpEdit.DataSource = codeMasterList2
            Me.drpCashOnSaleCorpEdit.DataTextField = "CodeDispValue"
            Me.drpCashOnSaleCorpEdit.DataValueField = "CodeValue"
            Me.drpCashOnSaleCorpEdit.DataBind()
        ElseIf UCase(strMode) = "CORPSALESEARCH" Then
            Dim codeMasterList2 As List(Of CodeMasterModel) = codeMasterControl.SelectCorpMaster()

            Dim codeMaster1 As CodeMasterModel = New CodeMasterModel()
            codeMaster1.CodeValue = ""
            codeMaster1.CodeDispValue = ""
            codeMasterList2.Insert(0, codeMaster1)

            Me.drpCashOnSaleCorpSearch.DataSource = codeMasterList2
            Me.drpCashOnSaleCorpSearch.DataTextField = "CodeDispValue"
            Me.drpCashOnSaleCorpSearch.DataValueField = "CodeValue"
            Me.drpCashOnSaleCorpSearch.DataBind()

        ElseIf UCase(strMode) = "CREATEINVOICE" Then
            Dim codeMasterList3 As List(Of CodeMasterModel) = codeMasterControl.SelectCorpMaster()
            Me.drpCreateInvoiceCorp.DataSource = codeMasterList3
            Me.drpCreateInvoiceCorp.DataTextField = "CodeDispValue"
            Me.drpCreateInvoiceCorp.DataValueField = "CodeValue"
            Me.drpCreateInvoiceCorp.DataBind()
        End If


    End Sub
    ''' <summary>
    '''ADD Corporate Information (NEW)
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Protected Sub imgSubmit_Click(sender As Object, e As ImageClickEventArgs) Handles imgSubmit.Click
        'Validation for updation 
        'Storing Empty Error
        Dim strEmptCheck As String = ""
        Dim isValid As Boolean = True
        Dim lstSelected As Boolean = False
        If (txtCorporateNameAdd.Text.Trim) = "" Then
            strEmptCheck = strEmptCheck & "Corporate Name<br>"
            isValid = False
        End If
        If (txtCorporatePersonAdd.Text.Trim) = "" Then
            strEmptCheck = strEmptCheck & "Corporate Person Name<br>"
            isValid = False
        End If
        If (txtAddressLine1Add.Text.Trim) = "" Then
            strEmptCheck = strEmptCheck & "Address Line1<br>"
            isValid = False
        End If

        For Each item As ListItem In lstListLocationAdd.Items
            If item.Selected Then
                lstSelected = True
                Exit For
            End If
        Next

        If Not lstSelected Then
            isValid = False
            strEmptCheck = strEmptCheck & "Responsible Branch<br>"
        End If

        If Not (CommonControl.IsValidEmailFormat(txtEmailAdd.Text.Trim)) Then
            strEmptCheck = strEmptCheck & "Email id<br>"
            isValid = False
        End If

        If Not isValid Then
            Call showMsg("The following field(s) are mandatory...<br>" & strEmptCheck)
            Exit Sub
        End If

        Dim userid As String = Session("user_id")
        Dim username As String = Session("user_Name")
        Dim shipname As String = Session("ship_Name")
        Dim shipmark As String = Session("ship_mark")
        'Intialize the object
        Dim _CorporateModel As CorporateModel = New CorporateModel()
        Dim _CorporateControl As CorporateControl = New CorporateControl()
        _CorporateModel.UserId = userid
        _CorporateModel.UserName = username
        ' CRTDT = dtNow  - no need to pass
        _CorporateModel.CRTCD = userid
        '[UPDDT] update user - no need to pass
        '[UPDCD]  update user - no need to pass
        _CorporateModel.UPDPG = "CorporateRegistration"
        _CorporateModel.DELFG = 0
        _CorporateModel.CreateUser = userid
        _CorporateModel.CorpName = txtCorporateNameAdd.Text.Trim
        _CorporateModel.CorpPerName = txtCorporatePersonAdd.Text.Trim
        _CorporateModel.CorpAdd1 = txtAddressLine1Add.Text.Trim
        _CorporateModel.CorpAdd2 = txtAddressLine2Add.Text.Trim
        _CorporateModel.CorpTel = txtTelephoneAdd.Text.Trim
        _CorporateModel.CorpFax = txtFaxAdd.Text.Trim
        _CorporateModel.CorpZip = txtZipAdd.Text.Trim
        Dim strResponseCode As String = ""
        For Each item As ListItem In lstListLocationAdd.Items
            If item.Selected Then
                strResponseCode = strResponseCode & item.Value & ","
            End If
        Next
        If Len(strResponseCode) > 1 Then
            strResponseCode = Left(strResponseCode, Len(strResponseCode) - 1)
        End If

        _CorporateModel.ResponsShipCode = strResponseCode
        '_CorporateModel.ResponsShipName = DropListLocation.SelectedItem.Text
        '_CorporateModel.UserPO =
        _CorporateModel.CloseDate = txtClosedDateAdd.Text.Trim
        _CorporateModel.PaymentDate = txtPaymentAdd.Text.Trim
        _CorporateModel.CorpEmail = txtEmailAdd.Text.Trim
        _CorporateModel.CorpBank = txtBankNameAdd.Text.Trim
        _CorporateModel.CasaAccount = txtAccountAdd.Text.Trim
        _CorporateModel.CasaNumber = txtAccountNumberAdd.Text.Trim
        _CorporateModel.CasaType = drpCaSaAdd.SelectedValue

        Dim strCorpNumber1 As String = ""
        'For future use  can use the below codes if neccessary
        ''Dim intCorpNumber1 As Integer = 0
        ''strCorpNumber1 = Right(shipname, Len(shipname) - 3)
        ''If Int32.TryParse(strCorpNumber1, intCorpNumber1) = False Then
        ''    intCorpNumber1 = 0
        ''Else
        ''    intCorpNumber1 = intCorpNumber1
        ''End If
        ''strCorpNumber1 = intCorpNumber1
        ''If Len(strCorpNumber1) = 1 Then
        ''    strCorpNumber1 = "0" & strCorpNumber1
        ''End If
        strCorpNumber1 = shipmark


        Dim strCorpNumber2 As String = ""
        Dim intCorpNumber2 As Integer = 0
        strCorpNumber2 = _CorporateControl.SelectCorporateNumber(_CorporateModel) 'select the corporate number
        If strCorpNumber2 = "" Then
            strCorpNumber2 = 0 'If there is no records
        End If
        If Int32.TryParse(strCorpNumber2, intCorpNumber2) = False Then
            intCorpNumber2 = 0
        Else
            intCorpNumber2 = intCorpNumber2 + 1
        End If
        strCorpNumber2 = intCorpNumber2
        If Len(strCorpNumber2) = 1 Then
            strCorpNumber2 = "00" & strCorpNumber2
        ElseIf Len(strCorpNumber2) = 2 Then
            strCorpNumber2 = "0" & strCorpNumber2
        End If



        _CorporateModel.CorpNumber = strCorpNumber1 & "-" & strCorpNumber2



        If _CorporateControl.AddCorporate(_CorporateModel) Then
            Call showMsg("The corporate information has been successfully saved. Corporate No is " & _CorporateModel.CorpNumber)
            ' GridViewBind()
        Else
            Call showMsg("Failed to upload it")
        End If

    End Sub
    ''' <summary>
    '''Edit Corporate Information (SEARCH)
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Protected Sub imgSearchCorpEdit_Click(sender As Object, e As ImageClickEventArgs) Handles imgSearchCorpEdit.Click
        'Intialize the Edit Corporate Informtion

        EditIntialize()
        If (txtCorpNumberEdit.Text.Trim) = "" Then
            Call showMsg("Corporate Number is empty!!!")
            Exit Sub
        End If
        Dim _CorporateModel As CorporateModel = New CorporateModel()
        Dim _CorporateControl As CorporateControl = New CorporateControl()
        _CorporateModel.CorpNumber = txtCorpNumberEdit.Text.Trim
        Dim CorpNumExistCnt As String = _CorporateControl.SelectCorpNumberCheck(_CorporateModel)
        'if No Datas found, then show the error message to the user
        If CorpNumExistCnt = "0" Then
            Call showMsg("The corporate number does not exist (" & txtCorpNumberEdit.Text.Trim & ") !!!")
            Exit Sub
        End If


        Dim dtCorpDetails As DataTable = _CorporateControl.SelectCorpDetails(_CorporateModel)

        If (dtCorpDetails Is Nothing) Or (dtCorpDetails.Rows.Count = 0) Then
            Call showMsg("The corporate number does not exist (" & txtCorpNumberEdit.Text.Trim & ") !!!")
            Exit Sub
        Else
            'Visible hence the condition is satisfy
            tblCorporateEditResult.Visible = True

            If Not IsDBNull(dtCorpDetails.Rows(0)("corp_name")) Then
                txtCorporateNameEdit.Text = dtCorpDetails.Rows(0)("corp_name")
            End If
            If Not IsDBNull(dtCorpDetails.Rows(0)("corp_per_name")) Then
                txtCorporatePersonEdit.Text = dtCorpDetails.Rows(0)("corp_per_name")
            End If
            If Not IsDBNull(dtCorpDetails.Rows(0)("corp_add_1")) Then
                txtAddressLine1Edit.Text = dtCorpDetails.Rows(0)("corp_add_1")
            End If
            If Not IsDBNull(dtCorpDetails.Rows(0)("corp_add_2")) Then
                txtAddressLine2Edit.Text = dtCorpDetails.Rows(0)("corp_add_2")
            End If
            If Not IsDBNull(dtCorpDetails.Rows(0)("corp_tel")) Then
                txtTelephoneEdit.Text = dtCorpDetails.Rows(0)("corp_tel")
            End If
            If Not IsDBNull(dtCorpDetails.Rows(0)("corp_fax")) Then
                txtFaxEdit.Text = dtCorpDetails.Rows(0)("corp_fax")
            End If
            If Not IsDBNull(dtCorpDetails.Rows(0)("corp_zip")) Then
                txtZipEdit.Text = dtCorpDetails.Rows(0)("corp_zip")
            End If
            If Not IsDBNull(dtCorpDetails.Rows(0)("response_ship")) Then
                Dim ship() As String
                ship = Split(dtCorpDetails.Rows(0)("response_ship"), ",")
                For i = 0 To UBound(ship)
                    If Trim(ship(i)) <> "" Or Trim(ship(i)) <> vbNullString Then
                        For Each item As ListItem In lstListLocationEdit.Items
                            If item.Value = ship(i) Then
                                item.Selected = True
                            End If
                        Next
                    End If
                Next i
            End If
            If Not IsDBNull(dtCorpDetails.Rows(0)("close_date")) Then
                txtClosedDateEdit.Text = dtCorpDetails.Rows(0)("close_date")
            End If
            If Not IsDBNull(dtCorpDetails.Rows(0)("payment_date")) Then
                txtPaymentEdit.Text = dtCorpDetails.Rows(0)("payment_date")
            End If
            If Not IsDBNull(dtCorpDetails.Rows(0)("corp_email")) Then
                txtEmailEdit.Text = dtCorpDetails.Rows(0)("corp_email")
            End If
            If Not IsDBNull(dtCorpDetails.Rows(0)("corp_bank")) Then
                txtBankNameEdit.Text = dtCorpDetails.Rows(0)("corp_bank")
            End If
            If Not IsDBNull(dtCorpDetails.Rows(0)("casa_account")) Then
                txtAccountEdit.Text = dtCorpDetails.Rows(0)("casa_account")
            End If
            If Not IsDBNull(dtCorpDetails.Rows(0)("casa_number")) Then
                txtAccountNumberEdit.Text = dtCorpDetails.Rows(0)("casa_number")
            End If
            If Not IsDBNull(dtCorpDetails.Rows(0)("casa_type")) Then
                drpCaSaEdit.ClearSelection()
                For Each item As ListItem In drpCaSaEdit.Items
                    If item.Value = dtCorpDetails.Rows(0)("casa_type") Then
                        item.Selected = True
                        Exit For
                    End If
                Next
            End If
        End If
    End Sub
    ''' <summary>
    '''Edit Corporate Information (UPDATE)
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Protected Sub imgEdit_Click(sender As Object, e As ImageClickEventArgs) Handles imgEdit.Click
        'Validation for updation 
        'Storing Empty Error
        Dim strEmptCheck As String = ""
        Dim isValid As Boolean = True
        Dim lstSelected As Boolean = False
        If (txtCorporateNameEdit.Text.Trim) = "" Then
            strEmptCheck = strEmptCheck & "Corporate Name<br>"
            isValid = False
        End If
        If (txtCorporatePersonEdit.Text.Trim) = "" Then
            strEmptCheck = strEmptCheck & "Corporate Person Name<br>"
            isValid = False
        End If
        If (txtAddressLine1Edit.Text.Trim) = "" Then
            strEmptCheck = strEmptCheck & "Address Line1<br>"
            isValid = False
        End If

        For Each item As ListItem In lstListLocationEdit.Items
            If item.Selected Then
                lstSelected = True
                Exit For
            End If
        Next

        If Not lstSelected Then
            isValid = False
            strEmptCheck = strEmptCheck & "Responsible Branch<br>"
        End If
        If Not (txtEmailEdit.Text.Trim = "") Then
            If Not (CommonControl.IsValidEmailFormat(txtEmailEdit.Text.Trim)) Then
                strEmptCheck = strEmptCheck & "Email id<br>"
                isValid = False
            End If
        End If
        If Not isValid Then
            Call showMsg("The following are mandatory...<br>" & strEmptCheck)
            Exit Sub
        End If
        Dim userid As String = Session("user_id")
        Dim username As String = Session("user_Name")
        Dim shipname As String = Session("ship_Name")
        Dim shipmark As String = Session("ship_mark")

        'Intialize the object
        Dim _CorporateModel As CorporateModel = New CorporateModel()
        Dim _CorporateControl As CorporateControl = New CorporateControl()
        _CorporateModel.UserId = userid
        _CorporateModel.UserName = username
        ' CRTDT = dtNow  - no need to pass
        _CorporateModel.CRTCD = userid
        '[UPDDT] update user - no need to pass
        '[UPDCD]  update user - no need to pass
        _CorporateModel.UPDPG = "CorporateRegistration"
        _CorporateModel.DELFG = 0
        _CorporateModel.CreateUser = userid
        _CorporateModel.CorpName = txtCorporateNameEdit.Text.Trim
        _CorporateModel.CorpPerName = txtCorporatePersonEdit.Text.Trim
        _CorporateModel.CorpAdd1 = txtAddressLine1Edit.Text.Trim
        _CorporateModel.CorpAdd2 = txtAddressLine2Edit.Text.Trim
        _CorporateModel.CorpTel = txtTelephoneEdit.Text.Trim
        _CorporateModel.CorpFax = txtFaxEdit.Text.Trim
        _CorporateModel.CorpZip = txtZipEdit.Text.Trim
        Dim strResponseCode As String = ""
        For Each item As ListItem In lstListLocationEdit.Items
            If item.Selected Then
                strResponseCode = strResponseCode & item.Value & ","
            End If
        Next
        If Len(strResponseCode) > 1 Then
            strResponseCode = Left(strResponseCode, Len(strResponseCode) - 1)
        End If

        _CorporateModel.ResponsShipCode = strResponseCode
        '_CorporateModel.ResponsShipName = DropListLocation.SelectedItem.Text
        '_CorporateModel.UserPO =
        _CorporateModel.CloseDate = txtClosedDateEdit.Text.Trim
        _CorporateModel.PaymentDate = txtPaymentEdit.Text.Trim
        _CorporateModel.CorpEmail = txtEmailEdit.Text.Trim
        _CorporateModel.CorpBank = txtBankNameEdit.Text.Trim
        _CorporateModel.CasaAccount = txtAccountEdit.Text.Trim
        _CorporateModel.CasaNumber = txtAccountNumberEdit.Text.Trim
        _CorporateModel.CasaType = drpCaSaEdit.SelectedValue

        'Dim strCorpNumber1 As String = ""
        'strCorpNumber1 = shipmark
        'Dim strCorpNumber2 As String = ""
        'Dim intCorpNumber2 As Integer = 0
        'strCorpNumber2 = _CorporateControl.SelectCorporateNumber(_CorporateModel) 'select the corporate number
        'If strCorpNumber2 = "" Then
        '    strCorpNumber2 = 0 'If there is no records
        'End If
        'If Int32.TryParse(strCorpNumber2, intCorpNumber2) = False Then
        '    intCorpNumber2 = 0
        'Else
        '    intCorpNumber2 = intCorpNumber2 + 1
        'End If
        'strCorpNumber2 = intCorpNumber2
        'If Len(strCorpNumber2) = 1 Then
        '    strCorpNumber2 = "00" & strCorpNumber2
        'ElseIf Len(strCorpNumber2) = 2 Then
        '    strCorpNumber2 = "0" & strCorpNumber2
        'End If


        _CorporateModel.CorpNumber = txtCorpNumberEdit.Text.Trim
        If _CorporateControl.EditCorporate(_CorporateModel) Then
            Call showMsg("Successfully Updated of Corporate No " & _CorporateModel.CorpNumber)
            ' GridViewBind()
            EditIntialize()
        Else
            Call showMsg("Failed to upload it")
        End If
    End Sub

    ''' <summary>
    ''' Intialize the Edit Corporate Information
    ''' </summary>
    Public Sub EditIntialize()
        txtCorporateNameEdit.Text = ""
        txtCorporatePersonEdit.Text = ""
        txtAddressLine1Edit.Text = ""
        txtAddressLine2Edit.Text = ""
        txtTelephoneEdit.Text = ""
        txtFaxEdit.Text = ""
        txtZipEdit.Text = ""
        lstListLocationEdit.ClearSelection()
        txtClosedDateEdit.Text = ""
        txtPaymentEdit.Text = ""
        txtEmailEdit.Text = ""
        txtBankNameEdit.Text = ""
        txtAccountEdit.Text = ""
        txtAccountNumberEdit.Text = ""
        drpCaSaEdit.ClearSelection()
        'Need to disable until condition is satisfy
        tblCorporateEditResult.Visible = False
    End Sub

    ''' <summary>
    ''' Intialize the Edit Cash On Sale
    ''' </summary>
    Public Sub EditCashOnIntialize()
        txtUserName.Text = ""
        txtPassword.Text = ""
        lblAscClaimNoCashOnSaleEdit.Text = ""
        lblCorpNoCashOnSaleEdit.Text = ""
        lblCorpNameCashOnSaleEdit.Text = ""
        lblCashCollectedCashOnSaleEdit.Text = ""
        'Need to disable until condition is satisfy
        tblCashOnSaleEditView.Visible = False

    End Sub

    ''' <summary>
    '''View Corporate Information (SEARCH)
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Protected Sub imgSearchCorpView_Click(sender As Object, e As ImageClickEventArgs) Handles imgSearchCorpView.Click
        'Intialize the Edit Corporate Informtion
        ViewIntialize()
        If (txtCorpNumberView.Text.Trim) = "" Then
            Call showMsg("Corporate Number is empty!!!")
            Exit Sub
        End If
        Dim _CorporateModel As CorporateModel = New CorporateModel()
        Dim _CorporateControl As CorporateControl = New CorporateControl()
        _CorporateModel.CorpNumber = txtCorpNumberView.Text.Trim
        Dim CorpNumExistCnt As String = _CorporateControl.SelectCorpNumberCheck(_CorporateModel)
        'if No Datas found, then show the error message to the user
        If CorpNumExistCnt = "0" Then
            Call showMsg("The corporate number does not exist (" & txtCorpNumberView.Text.Trim & ") !!!")
            Exit Sub
        End If


        Dim dtCorpDetails As DataTable = _CorporateControl.SelectCorpDetails(_CorporateModel)

        If (dtCorpDetails Is Nothing) Or (dtCorpDetails.Rows.Count = 0) Then
            Call showMsg("The corporate number does not exist (" & txtCorpNumberView.Text.Trim & ") !!!")
            Exit Sub
        Else
            'Visible hence the condition is satisfy
            tblCorporateViewResult.Visible = True

            If Not IsDBNull(dtCorpDetails.Rows(0)("corp_name")) Then
                txtCorporateNameView.Text = dtCorpDetails.Rows(0)("corp_name")
            End If
            If Not IsDBNull(dtCorpDetails.Rows(0)("corp_per_name")) Then
                txtCorporatePersonView.Text = dtCorpDetails.Rows(0)("corp_per_name")
            End If
            If Not IsDBNull(dtCorpDetails.Rows(0)("corp_add_1")) Then
                txtAddressLine1View.Text = dtCorpDetails.Rows(0)("corp_add_1")
            End If
            If Not IsDBNull(dtCorpDetails.Rows(0)("corp_add_2")) Then
                txtAddressLine2View.Text = dtCorpDetails.Rows(0)("corp_add_2")
            End If
            If Not IsDBNull(dtCorpDetails.Rows(0)("corp_tel")) Then
                txtTelephoneView.Text = dtCorpDetails.Rows(0)("corp_tel")
            End If
            If Not IsDBNull(dtCorpDetails.Rows(0)("corp_fax")) Then
                txtFaxView.Text = dtCorpDetails.Rows(0)("corp_fax")
            End If
            If Not IsDBNull(dtCorpDetails.Rows(0)("corp_zip")) Then
                txtZipView.Text = dtCorpDetails.Rows(0)("corp_zip")
            End If
            If Not IsDBNull(dtCorpDetails.Rows(0)("response_ship")) Then
                Dim ship() As String
                ship = Split(dtCorpDetails.Rows(0)("response_ship"), ",")
                For i = 0 To UBound(ship)
                    If Trim(ship(i)) <> "" Or Trim(ship(i)) <> vbNullString Then
                        For Each item As ListItem In lstListLocationView.Items

                            If item.Value = ship(i) Then
                                item.Selected = True
                            End If
                        Next
                    End If
                Next i
            End If
            If Not IsDBNull(dtCorpDetails.Rows(0)("close_date")) Then
                txtClosedDateView.Text = dtCorpDetails.Rows(0)("close_date")
            End If
            If Not IsDBNull(dtCorpDetails.Rows(0)("payment_date")) Then
                txtPaymentView.Text = dtCorpDetails.Rows(0)("payment_date")
            End If
            If Not IsDBNull(dtCorpDetails.Rows(0)("corp_email")) Then
                txtEmailView.Text = dtCorpDetails.Rows(0)("corp_email")
            End If
            If Not IsDBNull(dtCorpDetails.Rows(0)("corp_bank")) Then
                txtBankNameView.Text = dtCorpDetails.Rows(0)("corp_bank")
            End If
            If Not IsDBNull(dtCorpDetails.Rows(0)("casa_account")) Then
                txtAccountView.Text = dtCorpDetails.Rows(0)("casa_account")
            End If
            If Not IsDBNull(dtCorpDetails.Rows(0)("casa_number")) Then
                txtAccountNumberView.Text = dtCorpDetails.Rows(0)("casa_number")
            End If
            If Not IsDBNull(dtCorpDetails.Rows(0)("casa_type")) Then
                drpCaSaView.ClearSelection()
                For Each item As ListItem In drpCaSaView.Items
                    If item.Value = dtCorpDetails.Rows(0)("casa_type") Then
                        item.Selected = True
                        Exit For
                    End If
                Next
            End If
        End If
    End Sub

    ''' <summary>
    ''' Intialize the View Corporate Information
    ''' </summary>
    Public Sub ViewIntialize()
        txtCorporateNameView.Text = ""
        txtCorporatePersonView.Text = ""
        txtAddressLine1View.Text = ""
        txtAddressLine2View.Text = ""
        txtTelephoneView.Text = ""
        txtFaxView.Text = ""
        txtZipView.Text = ""
        lstListLocationView.ClearSelection()
        txtClosedDateView.Text = ""
        txtPaymentView.Text = ""
        txtEmailView.Text = ""
        txtBankNameView.Text = ""
        txtAccountView.Text = ""
        txtAccountNumberView.Text = ""
        drpCaSaView.ClearSelection()
        'Need to disable until condition is satisfy
        tblCorporateViewResult.Visible = False
    End Sub

    ''' <summary>
    '''Search Corporate Information (GridView)
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Protected Sub imgSearch_Click(sender As Object, e As ImageClickEventArgs) Handles imgSearch.Click
        'Intialize the GridView
        gvCorpInfo.DataSource = Nothing
        gvCorpInfo.DataBind()
        gvCorpInfo.Visible = False
        tblSearchView.Visible = False
        'Empty Validation
        If (txtCorporateNameSearch.Text = "") And
                (txtEmailSearch.Text = "") And
                (txtPersonSearch.Text = "") And
                (txtPhoneSearch.Text = "") And
                (txtZipCodeSarch.Text = "") And
                (txtMonthFromSearch.Text = "") And
                (txtMonthToSearch.Text = "") Then
            Call showMsg("Please enter atleast one search condition")
            Exit Sub
        End If
        Dim _CorporateModel As CorporateModel = New CorporateModel()
        Dim _CorporateControl As CorporateControl = New CorporateControl()
        _CorporateModel.CorpName = txtCorporateNameSearch.Text.Trim
        _CorporateModel.CorpPerName = txtPersonSearch.Text.Trim
        _CorporateModel.CorpZip = txtZipCodeSarch.Text.Trim
        _CorporateModel.CorpEmail = txtEmailSearch.Text.Trim
        _CorporateModel.CorpTel = txtPhoneSearch.Text.Trim
        _CorporateModel.RegistrationDateFrom = txtMonthFromSearch.Text.Trim
        _CorporateModel.RegistrationDateTo = txtMonthToSearch.Text.Trim
        Dim CorpNumExistCnt As String = _CorporateControl.SelectCorpNumberCheck(_CorporateModel)
        'if No Datas found, then show the error message to the user
        If CorpNumExistCnt = "0" Then
            Call showMsg("The corporate Information Does Not Found !!!")
            Exit Sub
        End If
        GridViewBind()

    End Sub

    Protected Sub GridViewBind(ByVal Optional Location As String = "")
        Dim _CorporateModel As CorporateModel = New CorporateModel()
        Dim _CorporateControl As CorporateControl = New CorporateControl()
        _CorporateModel.CorpName = txtCorporateNameSearch.Text.Trim
        _CorporateModel.CorpPerName = txtPersonSearch.Text.Trim
        _CorporateModel.CorpZip = txtZipCodeSarch.Text.Trim
        _CorporateModel.CorpEmail = txtEmailSearch.Text.Trim
        _CorporateModel.CorpTel = txtPhoneSearch.Text.Trim
        _CorporateModel.RegistrationDateFrom = txtMonthFromSearch.Text.Trim
        _CorporateModel.RegistrationDateTo = txtMonthToSearch.Text.Trim
        Dim dtCorpDetails As DataTable = _CorporateControl.SelectCorpDetails(_CorporateModel)
        If (dtCorpDetails Is Nothing) Or (dtCorpDetails.Rows.Count = 0) Then
            gvCorpInfo.DataSource = Nothing
            gvCorpInfo.DataBind()
            gvCorpInfo.Visible = False
            ViewState("dtCorpCount") = 0

        Else
            ViewState("dtCorpCount") = gvCorpInfo.Rows.Count
        End If
        gvCorpInfo.DataSource = dtCorpDetails
        ViewState("dtCorpCount") = dtCorpDetails
        gvCorpInfo.DataBind()

        If gvCorpInfo.Rows.Count = 0 Then
            gvCorpInfo.Visible = False
        Else
            gvCorpInfo.Visible = True
        End If

    End Sub
    Protected Sub gvCorpInfo_RowCreated(ByVal sender As Object, ByVal e As GridViewRowEventArgs)
        Log4NetControl.ComInfoLogWrite(Session("UserName"))
        If e.Row.FindControl("lnkCorporateNo") IsNot Nothing Then
            Dim lnkHisId As LinkButton = CType(e.Row.FindControl("lnkCorporateNo"), LinkButton)
            AddHandler lnkHisId.Click, New EventHandler(AddressOf lnkCorporateNo_Click)
        End If
    End Sub

    Private Sub lnkCorporateNo_Click(ByVal sender As Object, ByVal e As EventArgs)
        Log4NetControl.ComInfoLogWrite(Session("UserName"))
        Dim lnkHisId As LinkButton = CType(sender, LinkButton)
        Dim strCorpNumber As String = ""
        strCorpNumber = lnkHisId.Text
        Dim _CorporateModel As CorporateModel = New CorporateModel()
        Dim _CorporateControl As CorporateControl = New CorporateControl()
        _CorporateModel.CorpNumber = strCorpNumber
        Dim CorpNumExistCnt As String = _CorporateControl.SelectCorpNumberCheck(_CorporateModel)
        'if No Datas found, then show the error message to the user
        If CorpNumExistCnt = "0" Then
            Call showMsg("The corporate number does not exist (" & strCorpNumber & ") !!!")
            tblSearchView.Visible = False
            Exit Sub
        End If
        Dim dtCorpDetails As DataTable = _CorporateControl.SelectCorpDetails(_CorporateModel)
        If (dtCorpDetails Is Nothing) Or (dtCorpDetails.Rows.Count = 0) Then
            Call showMsg("The corporate number does not exist (" & strCorpNumber & ") !!!")
            tblSearchView.Visible = False
            Exit Sub
        Else
            tblSearchView.Visible = True

            If Not IsDBNull(dtCorpDetails.Rows(0)("corp_name")) Then
                txtCorporateNameSearchView.Text = dtCorpDetails.Rows(0)("corp_name")
            End If
            If Not IsDBNull(dtCorpDetails.Rows(0)("corp_per_name")) Then
                txtCorporatePersonSearchView.Text = dtCorpDetails.Rows(0)("corp_per_name")
            End If
            If Not IsDBNull(dtCorpDetails.Rows(0)("corp_add_1")) Then
                txtAddressLine1SearchView.Text = dtCorpDetails.Rows(0)("corp_add_1")
            End If
            If Not IsDBNull(dtCorpDetails.Rows(0)("corp_add_2")) Then
                txtAddressLine2SearchView.Text = dtCorpDetails.Rows(0)("corp_add_2")
            End If
            If Not IsDBNull(dtCorpDetails.Rows(0)("corp_tel")) Then
                txtTelephoneSearchView.Text = dtCorpDetails.Rows(0)("corp_tel")
            End If
            If Not IsDBNull(dtCorpDetails.Rows(0)("corp_fax")) Then
                txtFaxSearchView.Text = dtCorpDetails.Rows(0)("corp_fax")
            End If
            If Not IsDBNull(dtCorpDetails.Rows(0)("corp_zip")) Then
                txtZipSearchView.Text = dtCorpDetails.Rows(0)("corp_zip")
            End If
            If Not IsDBNull(dtCorpDetails.Rows(0)("response_ship")) Then
                Dim ship() As String
                lstListLocationSearchView.ClearSelection()
                ship = Split(dtCorpDetails.Rows(0)("response_ship"), ",")
                For i = 0 To UBound(ship)
                    If Trim(ship(i)) <> "" Or Trim(ship(i)) <> vbNullString Then
                        For Each item As ListItem In lstListLocationSearchView.Items
                            If item.Value = ship(i) Then
                                item.Selected = True
                            End If
                        Next
                    End If
                Next i
            End If
            If Not IsDBNull(dtCorpDetails.Rows(0)("close_date")) Then
                txtClosedDateSearchView.Text = dtCorpDetails.Rows(0)("close_date")
            End If
            If Not IsDBNull(dtCorpDetails.Rows(0)("payment_date")) Then
                txtPaymentSearchView.Text = dtCorpDetails.Rows(0)("payment_date")
            End If
            If Not IsDBNull(dtCorpDetails.Rows(0)("corp_email")) Then
                txtEmailSearchView.Text = dtCorpDetails.Rows(0)("corp_email")
            End If
            If Not IsDBNull(dtCorpDetails.Rows(0)("corp_bank")) Then
                txtBankNameSearchView.Text = dtCorpDetails.Rows(0)("corp_bank")
            End If
            If Not IsDBNull(dtCorpDetails.Rows(0)("casa_account")) Then
                txtAccountSearchView.Text = dtCorpDetails.Rows(0)("casa_account")
            End If
            If Not IsDBNull(dtCorpDetails.Rows(0)("casa_number")) Then
                txtAccountNumberSearchView.Text = dtCorpDetails.Rows(0)("casa_number")
            End If
            If Not IsDBNull(dtCorpDetails.Rows(0)("casa_type")) Then
                drpCaSaSearchView.ClearSelection()
                For Each item As ListItem In drpCaSaSearchView.Items
                    If item.Value = dtCorpDetails.Rows(0)("casa_type") Then
                        item.Selected = True
                        Exit For
                    End If
                Next
            End If
        End If

    End Sub

    Protected Sub gvCorpInfo_PageIndexChanging(ByVal sender As Object, ByVal e As GridViewPageEventArgs)
        Log4NetControl.ComInfoLogWrite(Session("UserName"))
        gvCorpInfo.PageIndex = e.NewPageIndex
        GridViewBind()
    End Sub
    ''' <summary>
    ''' Intialize the Search
    ''' </summary>
    Public Sub SearchIntialize()
        txtCorporateNameSearch.Text = ""
        txtEmailSearch.Text = ""
        txtPersonSearch.Text = ""
        txtPhoneSearch.Text = ""
        txtZipCodeSarch.Text = ""
        txtMonthFromSearch.Text = ""
        txtMonthToSearch.Text = ""
        gvCorpInfo.DataSource = Nothing
        gvCorpInfo.DataBind()
        gvCorpInfo.Visible = False


        'For Search View
        txtCorporateNameSearchView.Text = ""
        txtCorporatePersonSearchView.Text = ""
        txtAddressLine1SearchView.Text = ""
        txtAddressLine2SearchView.Text = ""
        txtTelephoneSearchView.Text = ""
        txtFaxSearchView.Text = ""
        txtZipSearchView.Text = ""
        lstListLocationSearchView.ClearSelection()
        txtClosedDateSearchView.Text = ""
        txtPaymentSearchView.Text = ""
        txtEmailSearchView.Text = ""
        txtBankNameSearchView.Text = ""
        txtAccountSearchView.Text = ""
        txtAccountNumberSearchView.Text = ""
        drpCaSaSearchView.ClearSelection()


    End Sub


    ''' <summary>
    '''Cash On Sale Add
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Protected Sub imgCashOnSaleAssign_Click(sender As Object, e As ImageClickEventArgs) Handles imgCashOnSaleAssign.Click
        'Verify claim is selected or not
        Dim isSelected As Boolean = False
        For Each item As ListItem In lstClaimNo.Items
            If item.Selected Then
                isSelected = True
                Exit For
            End If
        Next
        If Not isSelected Then
            Call showMsg("Select the ASC No / Service Order No.")
            Exit Sub
        End If

        Dim _CashOnSaleModel As CashOnSaleModel = New CashOnSaleModel()
        Dim _CashOnSaleControl As CashOnSaleControl = New CashOnSaleControl()

        Dim strMsg As String = ""

        'For Each item As ListItem In lstServiceOrderNo.Items
        For Each item As ListItem In lstClaimNo.Items
            If item.Selected Then
                _CashOnSaleModel.CorpNumber = drpCashOnSaleCorp.SelectedItem.Value
                _CashOnSaleModel.Location = item.Value
                _CashOnSaleModel.ClaimNo = item.Text
                _CashOnSaleModel.CorpFlg = 1
                _CashOnSaleModel.CorpCollect = 1

                If _CashOnSaleControl.AddCashOnSale(_CashOnSaleModel) Then
                    strMsg = strMsg & _CashOnSaleModel.ClaimNo & "-------OK------<br>"
                Else
                    strMsg = strMsg & _CashOnSaleModel.ClaimNo & "-------Error------<br>"
                End If
            End If
        Next

        Call showMsg("Result<br>" & strMsg)

        disableAll()
        tblCashOnSaleAdd.Visible = True
        InitList("corpsaleadd")
    End Sub


    ''' <summary>
    '''Edit Cash on Sale (SEARCH)
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Protected Sub imgSearchCashOnSaleEdit_Click(sender As Object, e As ImageClickEventArgs) Handles imgSearchCashOnSaleEdit.Click
        'Intialize the Edit Cash On Sale Informtion
        EditCashOnIntialize()
        If (txtClaimNoSearch.Text.Trim) = "" Then
            Call showMsg("Claim Number (ASC Claim/Service Order Number) is empty!!!")
            Exit Sub
        End If

        Dim _CashOnSaleModel As CashOnSaleModel = New CashOnSaleModel()
        Dim _CashOnSaleControl As CashOnSaleControl = New CashOnSaleControl()
        _CashOnSaleModel.ClaimNo = txtClaimNoSearch.Text.Trim
        Dim ClaimNumExistCnt As String = _CashOnSaleControl.SelectClaimNoCheck(_CashOnSaleModel)
        'if No Datas found, then show the error message to the user
        If ClaimNumExistCnt = "0" Then
            Call showMsg("The ASC Claim number does not exist / Incomplete (" & txtCorpNumberEdit.Text.Trim & ") !!!")
            Exit Sub
        End If



        Dim dtClaimDetails As DataTable = _CashOnSaleControl.SelectClaimDetails(_CashOnSaleModel)

        If (dtClaimDetails Is Nothing) Or (dtClaimDetails.Rows.Count = 0) Then
            Call showMsg("The corporate number does not exist (" & txtCorpNumberEdit.Text.Trim & ") !!!")
            Exit Sub
        Else
            ''    'Visible hence the condition is satisfy
            tblCashOnSaleEditView.Visible = True

            'By Default
            lblAscClaimNoCashOnSaleEdit.Text = txtClaimNoSearch.Text.Trim

            If Not IsDBNull(dtClaimDetails.Rows(0)("corp_number")) Then
                lblCorpNoCashOnSaleEdit.Text = dtClaimDetails.Rows(0)("corp_number")
            End If

            If Not IsDBNull(dtClaimDetails.Rows(0)("corp_name")) Then
                lblCorpNameCashOnSaleEdit.Text = dtClaimDetails.Rows(0)("corp_name")
            End If

            If Not IsDBNull(dtClaimDetails.Rows(0)("corp_collect")) Then
                Dim isCollected As Boolean = False
                isCollected = dtClaimDetails.Rows(0)("corp_collect")
                If isCollected Then
                    lblCashCollectedCashOnSaleEdit.Text = "Yes"
                Else
                    lblCashCollectedCashOnSaleEdit.Text = "No"
                End If
            End If
        End If


    End Sub


    ''' <summary>
    '''Cash On Sale Edit Update 
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Protected Sub imgCashOnSaleEditUpdate_Click(sender As Object, e As ImageClickEventArgs) Handles imgCashOnSaleEditUpdate.Click
        'lblAscClaimNoCashOnSaleEdit 
        If lblCorpNameCashOnSaleEdit.Text = drpCashOnSaleCorpEdit.SelectedItem.Text Then
            Call showMsg("Can not update, No Change in Corporate!!!")
            Exit Sub
        End If

        If (lblCashCollectedCashOnSaleEdit.Text = "Yes") And (drpCashCollected.SelectedItem.Text = "No") Then
            Call showMsg("Collected Cash cann't change to 'No' ")
            Exit Sub
        End If

        If txtUserName.Text = "" Then
            Call showMsg("Enter User name")
            Exit Sub
        End If
        'Find Password empty or entered
        If txtPassword.Text = "" Then
            Call showMsg("Enter Password")
            Exit Sub
        End If

        'Verify entered user and password correct or not with the database
        Dim _UserInfoModel As UserInfoModel = New UserInfoModel()
        Dim _UserInfoControl As UserInfoControl = New UserInfoControl()
        _UserInfoModel.UserId = txtUserName.Text.ToString.Trim()
        _UserInfoModel.Password = txtPassword.Text.ToString().Trim()
        Dim UserInfoList As List(Of UserInfoModel) = _UserInfoControl.SelectUserInfo(_UserInfoModel)
        'User Doesn't exist
        If UserInfoList Is Nothing OrElse UserInfoList.Count = 0 Then
            Call showMsg("The username / password incorrect. Please try again")
            Exit Sub
        End If


        'Store user level
        ViewState("userlevel") = UserInfoList(0)


        Dim _CashOnSaleModel As CashOnSaleModel = New CashOnSaleModel()
        Dim _CashOnSaleControl As CashOnSaleControl = New CashOnSaleControl()

        Dim strMsg As String = ""

        _CashOnSaleModel.CorpNumber = drpCashOnSaleCorpEdit.SelectedItem.Value
        _CashOnSaleModel.EditLog = txtUserName.Text & DateTime.Now.ToString("yyyyMMddHHmmssfff")
        _CashOnSaleModel.ClaimNo = lblAscClaimNoCashOnSaleEdit.Text

        If Not (lblCashCollectedCashOnSaleEdit.Text = "Yes") Then
            If drpCashCollected.SelectedItem.Text = "Yes" Then
                _CashOnSaleModel.CorpCollect = 1
                _CashOnSaleModel.CorpCollects = 1
            ElseIf drpCashCollected.SelectedItem.Text = "No" Then
                _CashOnSaleModel.CorpCollect = 0
                _CashOnSaleModel.CorpCollects = 0
            End If
        End If

        If _CashOnSaleControl.UpdateCashOnSale(_CashOnSaleModel) Then
            Call showMsg("Successfully Updated...")

            disableAll()
            tblCashOnSaleEdit.Visible = True
            InitList("corpsaleedit")
            Exit Sub
        Else
            Call showMsg("Failed to Update...")
            Exit Sub
        End If
    End Sub

    ''' <summary>
    '''Edit Cash on Sale Search & Download
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Protected Sub imgSearchCashOnSaleSearch_Click(sender As Object, e As ImageClickEventArgs) Handles imgSearchCashOnSaleSearch.Click


        Dim _CashOnSaleModel As CashOnSaleModel = New CashOnSaleModel()
        Dim _CashOnSaleControl As CashOnSaleControl = New CashOnSaleControl()
        _CashOnSaleModel.ClaimNo = txtCashOnSaleSearchAscClaimNo.Text.Trim

        Dim tmpAmount As Decimal
        If Not (txtCashOnSaleTotAmt.Text.Trim = "") Then
            If Decimal.TryParse(Trim(txtCashOnSaleTotAmt.Text), tmpAmount) = False Then
                Call showMsg("Total Amount Is Invalid...")
                Exit Sub
            Else
                tmpAmount = Convert.ToDecimal(txtCashOnSaleTotAmt.Text)
                _CashOnSaleModel.TotalAmt = tmpAmount
            End If
        End If

        'Corporate Number and Name - Corporate Code is taken from the drop downlist
        Dim strCorpNumber As String = ""
        If Not (txtCashOnSaleSearchCorpNumber.Text.Trim = "") Then
            strCorpNumber = txtCashOnSaleSearchCorpNumber.Text.Trim
        End If
        If Not (drpCashOnSaleCorpSearch.SelectedItem.Text = "") Then
            If Len(strCorpNumber) > 1 Then
                strCorpNumber = "'" & strCorpNumber & "','" & drpCashOnSaleCorpSearch.SelectedValue & "'"
            Else
                strCorpNumber = "'" & drpCashOnSaleCorpSearch.SelectedValue & "'"
            End If
        End If
        If Len(strCorpNumber) > 1 Then
            _CashOnSaleModel.CorpNumbers = "( " & strCorpNumber & " ) "
        End If

        If Not (drpCashOnSearchCollected.SelectedItem.Text = "") Then
            If drpCashOnSearchCollected.SelectedItem.Text = "Yes" Then
                _CashOnSaleModel.CorpCollect = True
                _CashOnSaleModel.CorpCollects = "True"
            ElseIf drpCashOnSearchCollected.SelectedItem.Text = "No" Then
                _CashOnSaleModel.CorpCollect = False
                _CashOnSaleModel.CorpCollects = "False"
            End If
        End If

        If Not (txtCashOnSaleSearchCustomerName.Text.Trim = "") Then
            _CashOnSaleModel.CustomerName = txtCashOnSaleSearchCustomerName.Text.Trim
        End If
        If Not (txtCashOnSaleSearchFrom.Text.Trim = "") Then
            _CashOnSaleModel.DateFrom = txtCashOnSaleSearchFrom.Text.Trim
        End If
        If Not (txtCashOnSaleSearchTo.Text.Trim = "") Then
            _CashOnSaleModel.DateTo = txtCashOnSaleSearchTo.Text.Trim
        End If

        Dim CashOnSaleSearchExistCnt As String = _CashOnSaleControl.SelectCashOnSearchCheck(_CashOnSaleModel)
        'if No Datas found, then show the error message to the user
        If CashOnSaleSearchExistCnt = "0" Then
            Call showMsg(" Search Condition, Could not found !!!")
            tblCashOnSaleSearchView.Visible = False
            Exit Sub
        End If
        tblCashOnSaleSearchView.Visible = True
        GridViewBindCashOnSale()

    End Sub

    Protected Sub GridViewBindCashOnSale(ByVal Optional Location As String = "")
        Dim _CashOnSaleModel As CashOnSaleModel = New CashOnSaleModel()
        Dim _CashOnSaleControl As CashOnSaleControl = New CashOnSaleControl()
        _CashOnSaleModel.ClaimNo = txtCashOnSaleSearchAscClaimNo.Text.Trim

        Dim tmpAmount As Decimal
        If Not (txtCashOnSaleTotAmt.Text.Trim = "") Then
            If Decimal.TryParse(Trim(txtCashOnSaleTotAmt.Text), tmpAmount) = False Then
                Call showMsg("Total Amount Is Invalid...")
                Exit Sub
            Else
                tmpAmount = Convert.ToDecimal(txtCashOnSaleTotAmt.Text)
                _CashOnSaleModel.TotalAmt = tmpAmount
            End If
        End If

        'Corporate Number and Name - Corporate Code is taken from the drop downlist
        Dim strCorpNumber As String = ""
        If Not (txtCashOnSaleSearchCorpNumber.Text.Trim = "") Then
            strCorpNumber = txtCashOnSaleSearchCorpNumber.Text.Trim
        End If
        If Not (drpCashOnSaleCorpSearch.SelectedItem.Text = "") Then
            If Len(strCorpNumber) > 1 Then
                strCorpNumber = "'" & strCorpNumber & "','" & drpCashOnSaleCorpSearch.SelectedValue & "'"
            Else
                strCorpNumber = "'" & drpCashOnSaleCorpSearch.SelectedValue & "'"
            End If
        End If
        If Len(strCorpNumber) > 1 Then
            _CashOnSaleModel.CorpNumbers = "( " & strCorpNumber & " ) "
        End If

        If Not (drpCashOnSearchCollected.SelectedItem.Text = "") Then
            If drpCashOnSearchCollected.SelectedItem.Text = "Yes" Then
                _CashOnSaleModel.CorpCollect = True
                _CashOnSaleModel.CorpCollects = "True"
            ElseIf drpCashOnSearchCollected.SelectedItem.Text = "No" Then
                _CashOnSaleModel.CorpCollect = False
                _CashOnSaleModel.CorpCollects = "False"
            End If
        End If

        If Not (txtCashOnSaleSearchCustomerName.Text.Trim = "") Then
            _CashOnSaleModel.CustomerName = txtCashOnSaleSearchCustomerName.Text.Trim
        End If
        If Not (txtCashOnSaleSearchFrom.Text.Trim = "") Then
            _CashOnSaleModel.DateFrom = txtCashOnSaleSearchFrom.Text.Trim
        End If
        If Not (txtCashOnSaleSearchTo.Text.Trim = "") Then
            _CashOnSaleModel.DateTo = txtCashOnSaleSearchTo.Text.Trim
        End If

        tblCashOnSaleSearchView.Visible = True
        Dim dtCashOnSaleView As DataTable = _CashOnSaleControl.SelectCashOnSearchInfo(_CashOnSaleModel)
        If (dtCashOnSaleView Is Nothing) Or (dtCashOnSaleView.Rows.Count = 0) Then
            gvCashOnSaleInfo.DataSource = Nothing
            gvCashOnSaleInfo.DataBind()
            gvCashOnSaleInfo.Visible = False
            ViewState("dtCashOnSaleCount") = 0

        Else
            ViewState("dtCashOnSaleCount") = gvCashOnSaleInfo.Rows.Count
        End If

        gvCashOnSaleInfo.DataSource = dtCashOnSaleView
        ViewState("dtCashOnSaleCount") = dtCashOnSaleView
        gvCashOnSaleInfo.DataBind()
        If gvCashOnSaleInfo.Rows.Count = 0 Then
            gvCashOnSaleInfo.Visible = False
        Else
            gvCashOnSaleInfo.Visible = True
        End If
    End Sub

    Protected Sub gvCashOnSaleInfo_PageIndexChanging(ByVal sender As Object, ByVal e As GridViewPageEventArgs)
        Log4NetControl.ComInfoLogWrite(Session("UserName"))
        gvCashOnSaleInfo.PageIndex = e.NewPageIndex
        GridViewBindCashOnSale()
    End Sub

    ''' <summary>
    '''Cash on Sale  Download
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Protected Sub imgCashOnSaleSearchDownload_Click(sender As Object, e As ImageClickEventArgs) Handles imgCashOnSaleSearchDownload.Click
        Dim userName As String = Session("user_Name")
        Dim userid As String = Session("user_id")

        Dim _CashOnSaleModel As CashOnSaleModel = New CashOnSaleModel()
        Dim _CashOnSaleControl As CashOnSaleControl = New CashOnSaleControl()
        _CashOnSaleModel.ClaimNo = txtCashOnSaleSearchAscClaimNo.Text.Trim

        Dim tmpAmount As Decimal
        If Not (txtCashOnSaleTotAmt.Text.Trim = "") Then
            If Decimal.TryParse(Trim(txtCashOnSaleTotAmt.Text), tmpAmount) = False Then
                Call showMsg("Total Amount Is Invalid...")
                Exit Sub
            Else
                tmpAmount = Convert.ToDecimal(txtCashOnSaleTotAmt.Text)
                _CashOnSaleModel.TotalAmt = tmpAmount
            End If
        End If

        'Corporate Number and Name - Corporate Code is taken from the drop downlist
        Dim strCorpNumber As String = ""
        If Not (txtCashOnSaleSearchCorpNumber.Text.Trim = "") Then
            strCorpNumber = txtCashOnSaleSearchCorpNumber.Text.Trim
        End If
        If Not (drpCashOnSaleCorpSearch.SelectedItem.Text = "") Then
            If Len(strCorpNumber) > 1 Then
                strCorpNumber = "'" & strCorpNumber & "','" & drpCashOnSaleCorpSearch.SelectedValue & "'"
            Else
                strCorpNumber = "'" & drpCashOnSaleCorpSearch.SelectedValue & "'"
            End If
        End If
        If Len(strCorpNumber) > 1 Then
            _CashOnSaleModel.CorpNumbers = "( " & strCorpNumber & " ) "
        End If

        If Not (drpCashOnSearchCollected.SelectedItem.Text = "") Then
            If drpCashOnSearchCollected.SelectedItem.Text = "Yes" Then
                _CashOnSaleModel.CorpCollect = True
                _CashOnSaleModel.CorpCollects = "True"
            ElseIf drpCashOnSearchCollected.SelectedItem.Text = "No" Then
                _CashOnSaleModel.CorpCollect = False
                _CashOnSaleModel.CorpCollects = "False"
            End If
        End If

        If Not (txtCashOnSaleSearchCustomerName.Text.Trim = "") Then
            _CashOnSaleModel.CustomerName = txtCashOnSaleSearchCustomerName.Text.Trim
        End If
        If Not (txtCashOnSaleSearchFrom.Text.Trim = "") Then
            _CashOnSaleModel.DateFrom = txtCashOnSaleSearchFrom.Text.Trim
        End If
        If Not (txtCashOnSaleSearchTo.Text.Trim = "") Then
            _CashOnSaleModel.DateTo = txtCashOnSaleSearchTo.Text.Trim
        End If

        tblCashOnSaleSearchView.Visible = True

        Dim _dtCashOnSaleView As DataTable = _CashOnSaleControl.SelectCashOnSearchInfo(_CashOnSaleModel)
        If (_dtCashOnSaleView Is Nothing) Or (_dtCashOnSaleView.Rows.Count = 0) Then
        Else


            Dim _excel As New Microsoft.Office.Interop.Excel.Application
            Dim wBook As Microsoft.Office.Interop.Excel.Workbook
            Dim wSheet As Microsoft.Office.Interop.Excel.Worksheet

            wBook = _excel.Workbooks.Add()
            wSheet = wBook.ActiveSheet()

            Dim dt As System.Data.DataTable = _dtCashOnSaleView
            Dim dc As System.Data.DataColumn
            Dim dr As System.Data.DataRow
            Dim colIndex As Integer = 0
            Dim rowIndex As Integer = 0

            For Each dc In dt.Columns
                colIndex = colIndex + 1
                _excel.Cells(1, colIndex) = dc.ColumnName
            Next
            For Each dr In dt.Rows
                rowIndex = rowIndex + 1
                colIndex = 0
                For Each dc In dt.Columns
                    colIndex = colIndex + 1
                    _excel.Cells(rowIndex + 1, colIndex) = dr(dc.ColumnName)
                Next
            Next
            wSheet.Columns.AutoFit()
            'Dim strFileName As String = "C:\datatable.xls"
            'If System.IO.File.Exists(strFileName) Then
            '    System.IO.File.Delete(strFileName)
            'End If
            'Move the file to temporary folder
            If Not Directory.Exists(ConfigurationManager.AppSettings("RootDir") & "\" & userName & "\download") Then
                Directory.CreateDirectory(ConfigurationManager.AppSettings("RootDir") & "\" & userName & "\download")
            End If
            'Assign File Name
            Dim dirPath As String = ConfigurationManager.AppSettings("RootDir") & "\" & userName & "\download"
            Dim fileName As String = DateTime.Now.ToString("yyyyMMddHHmmssfff") & ".xls"
            Dim filenameFullPath As String = dirPath & "\" & fileName
            'Save the file in temporary folder
            'FileUploadAnalysis.SaveAs(filenameFullPath)

            wBook.SaveAs(filenameFullPath)
            wBook.Close()
            _excel.Quit()

            'Response情報クリア
            Response.ClearContent()
            'バッファリング
            Response.Buffer = True
            'HTTPヘッダー情報・MIMEタイプ設定
            Response.AddHeader("Content-Disposition", String.Format("attachment;filename={0}", filenameFullPath))
            'Response.ContentType = MimeType
            'ファイルを書き出し
            Response.WriteFile(filenameFullPath)
            Response.Flush()
            Response.End()
        End If
    End Sub



    ''' <summary>
    '''Download
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Protected Sub imgCashOnSaleInvoiceDownload_Click(sender As Object, e As ImageClickEventArgs) Handles imgCashOnSaleInvoiceDownload.Click

        ''      Call clsSet.WorkReportPDF(otherData, pdfFileNameWorkReport, errFlg, shipName)
        Dim userName As String = Session("user_Name")
        Dim userid As String = Session("user_id")



        'Move the file to temporary folder
        If Not Directory.Exists(ConfigurationManager.AppSettings("RootDir") & "\" & userName & "\download") Then
            Directory.CreateDirectory(ConfigurationManager.AppSettings("RootDir") & "\" & userName & "\download")
        End If
        'Assign File Name
        Dim dirPath As String = ConfigurationManager.AppSettings("RootDir") & "\" & userName & "\download"
        Dim fileName As String = DateTime.Now.ToString("yyyyMMddHHmmssfff") & ".pdf"
        Dim filenameFullPath As String = dirPath & "\" & fileName

        Dim _CashOnSaleInvoiceControl As CashOnSaleInvoiceControl = New CashOnSaleInvoiceControl()
        _CashOnSaleInvoiceControl.CreatePDF(userName, filenameFullPath)

        'Response情報クリア
        Response.ClearContent()
        'バッファリング
        Response.Buffer = True
        'HTTPヘッダー情報・MIMEタイプ設定
        Response.AddHeader("Content-Disposition", String.Format("attachment;filename={0}", filenameFullPath))
        'Response.ContentType = MimeType
        'ファイルを書き出し
        Response.WriteFile(filenameFullPath)
        Response.Flush()
        Response.End()

    End Sub

    ''' <summary>
    '''Create Invoice Search
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Protected Sub imgCreateInvoiceSearch_Click(sender As Object, e As ImageClickEventArgs) Handles imgCreateInvoiceSearch.Click
        tblCreateInvoiceSearchView.Visible = True
        GridViewBindCashOnSaleInvoice()
    End Sub

    Protected Sub GridViewBindCashOnSaleInvoice(ByVal Optional Location As String = "")
        'PO Generate
        Dim userid As String = Session("user_id")
        Dim username As String = Session("user_Name")
        Dim shipname As String = Session("ship_Name")
        Dim shipmark As String = Session("ship_mark")
        Dim _CashOnSaleInvoiceModel As CashOnSaleInvoiceModel = New CashOnSaleInvoiceModel()
        Dim _CashOnSaleInvoiceControl As CashOnSaleInvoiceControl = New CashOnSaleInvoiceControl()
        _CashOnSaleInvoiceModel.ShipMark = shipmark
        Dim PoNo As String = ""
        Dim PartCorpNo As String = ""
        Dim CorpNo As String = ""
        Dim IntTmp As Integer = 0 'For Temporary 
        '1st
        PoNo = _CashOnSaleInvoiceControl.SelectPoNo(_CashOnSaleInvoiceModel)
        If PoNo = "" Then
            PoNo = 0 'If there is no records
        End If
        '2nd
        Dim PartLocation As String = ""
        'PositionStart = InStr(1, shipname, "SSC")
        PartLocation = Right(shipname, Len(shipname) - 3) 'Hence
        If Int32.TryParse(PartLocation, IntTmp) = False Then
            PartLocation = "00"
        Else
            PartLocation = PartLocation
            If Len(PartLocation) = 1 Then
                PartLocation = "0" & PartLocation
            Else
                PartLocation = PartLocation
            End If
        End If
        '3rd
        CorpNo = drpCreateInvoiceCorp.SelectedValue
        Dim PositionStart As Int16 = 0
        PositionStart = InStr(1, CorpNo, "-")
        PartCorpNo = Right(CorpNo, Len(CorpNo) - PositionStart)
        If Int32.TryParse(PartCorpNo, IntTmp) = False Then
            PartCorpNo = "000"
        Else
            PartCorpNo = PartCorpNo
        End If
        '4th 
        Dim strSlipNumber As String = ""
        Dim intSlipNumber As Integer = 0
        strSlipNumber = _CashOnSaleInvoiceControl.SelectSlipNo(_CashOnSaleInvoiceModel) 'select the Slip number
        If strSlipNumber = "" Then
            strSlipNumber = 0 'If there is no records
        End If
        If Int32.TryParse(strSlipNumber, intSlipNumber) = False Then
            intSlipNumber = 0
        Else
            intSlipNumber = intSlipNumber + 1
        End If
        strSlipNumber = intSlipNumber
        If Len(strSlipNumber) = 1 Then
            strSlipNumber = "0000" & strSlipNumber
        ElseIf Len(strSlipNumber) = 2 Then
            strSlipNumber = "000" & strSlipNumber
        ElseIf Len(strSlipNumber) = 3 Then
            strSlipNumber = "00" & strSlipNumber
        ElseIf Len(strSlipNumber) = 4 Then
            strSlipNumber = "0" & strSlipNumber
        End If

        PoNo = PoNo & PartLocation & PartCorpNo & strSlipNumber
        _CashOnSaleInvoiceModel.CorpPoNo = PoNo
        Dim dtCashOnSaleInvoiceView As DataTable = _CashOnSaleInvoiceControl.SelectCorpPo(_CashOnSaleInvoiceModel)
        If (dtCashOnSaleInvoiceView Is Nothing) Or (dtCashOnSaleInvoiceView.Rows.Count = 0) Then
            gvCreateInvoice.DataSource = Nothing
            gvCreateInvoice.DataBind()
            gvCreateInvoice.Visible = False
            ViewState("dtCashOnSaleInvoiceCount") = 0

        Else
            ViewState("dtCashOnSaleInvoiceCount") = gvCashOnSaleInfo.Rows.Count
        End If

        gvCreateInvoice.DataSource = dtCashOnSaleInvoiceView
        ViewState("dtCashOnSaleInvoiceCount") = dtCashOnSaleInvoiceView
        gvCreateInvoice.DataBind()
        If gvCreateInvoice.Rows.Count = 0 Then
            gvCreateInvoice.Visible = False
        Else
            gvCreateInvoice.Visible = True
        End If
    End Sub
    Protected Sub gvCreateInvoice_PageIndexChanging(ByVal sender As Object, ByVal e As GridViewPageEventArgs)
        Log4NetControl.ComInfoLogWrite(Session("UserName"))
        gvCreateInvoice.PageIndex = e.NewPageIndex
        GridViewBindCashOnSaleInvoice()
    End Sub
    ''' <summary>
    '''Create Cash On Sale Invoice Create
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Protected Sub imgCashOnSaleInvoiceCreate_Click(sender As Object, e As ImageClickEventArgs) Handles imgCashOnSaleInvoiceCreate.Click
        Dim userid As String = Session("user_id")
        Dim username As String = Session("user_Name")
        Dim shipname As String = Session("ship_Name")
        Dim shipmark As String = Session("ship_mark")
        'Dim _CashOnSalesInvoiceCreateModel As CashOnSalesInvoiceCreateModel = New CashOnSalesInvoiceCreateModel()


        Dim lstModel As List(Of CashOnSalesInvoiceCreateModel) = New List(Of CashOnSalesInvoiceCreateModel)()

        Dim chkBoxSelect As Integer = 0


        For i As Integer = 0 To Me.gvCreateInvoice.Rows.Count - 1
            Dim chkCopy As CheckBox = CType(Me.gvCreateInvoice.Rows(i).FindControl("chkSelectClaim"), CheckBox)
            Dim lblClaimNo As Label = CType(gvCreateInvoice.Rows(i).FindControl("lblClaimNo"), Label)
            Dim lblCorpPoNo As Label = CType(gvCreateInvoice.Rows(i).FindControl("lblCorpPoNo"), Label)
            Dim targetModel As CashOnSalesInvoiceCreateModel = New CashOnSalesInvoiceCreateModel()
            If chkCopy.Checked Then
                chkBoxSelect = 1
                targetModel.CorpPoNo = lblCorpPoNo.Text
                targetModel.ClaimNo = lblClaimNo.Text
                lstModel.Add(targetModel)
            End If

        Next
        Dim _CashOnSaleInvoiceControl As CashOnSaleInvoiceControl = New CashOnSaleInvoiceControl()

        If chkBoxSelect = 1 Then
            If _CashOnSaleInvoiceControl.UpdateCashOnInvoice(lstModel) Then
                Call showMsg("Successfully Updated")
                GridViewBind()
            Else
                Call showMsg("Failed to upload it")
            End If
        End If


    End Sub

    ''' <summary>
    '''Multiple Work sheets
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Protected Sub imgMultipleSheet_Click(sender As Object, e As ImageClickEventArgs) Handles imgMultipleSheet.Click

        ''      Call clsSet.WorkReportPDF(otherData, pdfFileNameWorkReport, errFlg, shipName)
        Dim userName As String = Session("user_Name")
        Dim userid As String = Session("user_id")

        Dim _excel As New Microsoft.Office.Interop.Excel.Application
        Dim wBook As Microsoft.Office.Interop.Excel.Workbook
        Dim wSheet As Microsoft.Office.Interop.Excel.Worksheet
        Dim wSheet1 As Microsoft.Office.Interop.Excel.Worksheet

        wBook = _excel.Workbooks.Add()
        wSheet = wBook.ActiveSheet()
        wSheet.Name = "Customer1"

        Dim table As New DataTable

        ' Create four typed columns in the DataTable.
        table.Columns.Add("SNo", GetType(Integer))
        table.Columns.Add("Name", GetType(String))
        table.Columns.Add("Dept", GetType(String))
        table.Columns.Add("Date", GetType(DateTime))

        ' Add five rows with those columns filled in the DataTable.
        table.Rows.Add(1, "Name1", "Dept1", DateTime.Now)
        table.Rows.Add(2, "Name2", "Dept2", DateTime.Now)
        table.Rows.Add(3, "Name3", "Dept3", DateTime.Now)
        table.Rows.Add(4, "Name4", "Dept4", DateTime.Now)
        table.Rows.Add(5, "Name5", "Dept5", DateTime.Now)





        Dim dt As System.Data.DataTable = table
        Dim dc As System.Data.DataColumn
        Dim dr As System.Data.DataRow
        Dim colIndex As Integer = 0
        Dim rowIndex As Integer = 0

        For Each dc In dt.Columns
            colIndex = colIndex + 1
            _excel.Cells(1, colIndex) = dc.ColumnName
        Next
        For Each dr In dt.Rows
            rowIndex = rowIndex + 1
            colIndex = 0
            For Each dc In dt.Columns
                colIndex = colIndex + 1
                _excel.Cells(rowIndex + 1, colIndex) = dr(dc.ColumnName)
            Next
        Next
        wSheet.Columns.AutoFit()


        wSheet1 = wBook.Sheets.Add
        wSheet1.Name = "Customer2"


        Dim table1 As New DataTable

        ' Create four typed columns in the DataTable.
        table1.Columns.Add("SNo", GetType(Integer))
        table1.Columns.Add("Name", GetType(String))
        table1.Columns.Add("Dept", GetType(String))
        table1.Columns.Add("Date", GetType(DateTime))

        ' Add five rows with those columns filled in the DataTable.
        table1.Rows.Add(6, "Name1", "Dept6", DateTime.Now)
        table1.Rows.Add(7, "Name2", "Dept7", DateTime.Now)
        table1.Rows.Add(8, "Name3", "Dept8", DateTime.Now)
        table1.Rows.Add(9, "Name4", "Dept9", DateTime.Now)
        table1.Rows.Add(10, "Name5", "Dept10", DateTime.Now)




        Dim dt1 As System.Data.DataTable = table1
        Dim dc1 As System.Data.DataColumn
        Dim dr1 As System.Data.DataRow
        Dim colIndex1 As Integer = 0
        Dim rowIndex1 As Integer = 0


        For Each dc1 In dt1.Columns
            colIndex1 = colIndex1 + 1
            _excel.Cells(1, colIndex1) = dc1.ColumnName
        Next
        For Each dr1 In dt1.Rows
            rowIndex1 = rowIndex1 + 1
            colIndex1 = 0
            For Each dc1 In dt1.Columns
                colIndex1 = colIndex1 + 1
                _excel.Cells(rowIndex1 + 1, colIndex1) = dr1(dc1.ColumnName)
            Next
        Next

        wSheet1.Columns.AutoFit()

        'Dim strFileName As String = "C:\datatable.xls"
        'If System.IO.File.Exists(strFileName) Then
        '    System.IO.File.Delete(strFileName)
        'End If
        'Move the file to temporary folder
        If Not Directory.Exists(ConfigurationManager.AppSettings("RootDir") & "\" & userName & "\download") Then
            Directory.CreateDirectory(ConfigurationManager.AppSettings("RootDir") & "\" & userName & "\download")
        End If
        'Assign File Name
        Dim dirPath As String = ConfigurationManager.AppSettings("RootDir") & "\" & userName & "\download"
        Dim fileName As String = DateTime.Now.ToString("yyyyMMddHHmmssfff") & ".xls"
        Dim filenameFullPath As String = dirPath & "\" & fileName
        'Save the file in temporary folder
        'FileUploadAnalysis.SaveAs(filenameFullPath)

        wBook.SaveAs(filenameFullPath)
        wBook.Close()
        _excel.Quit()

        'Response情報クリア
        Response.ClearContent()
        'バッファリング
        Response.Buffer = True
        'HTTPヘッダー情報・MIMEタイプ設定
        Response.AddHeader("Content-Disposition", String.Format("attachment;filename={0}", filenameFullPath))
        'Response.ContentType = MimeType
        'ファイルを書き出し
        Response.WriteFile(filenameFullPath)
        Response.Flush()
        Response.End()
    End Sub

End Class