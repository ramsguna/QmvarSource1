Imports System.IO
Imports System.Text
Imports System.Globalization
Imports Ganges33.Ganges33.model
Imports Ganges33.Ganges33.logic
Imports System.Data.SqlClient
Imports System.Configuration



Public Class Analysis_Store_Management
    Inherits System.Web.UI.Page
    Private Property SortDirection As String
        Get
            Return IIf(ViewState("SortDirection") IsNot Nothing, Convert.ToString(ViewState("SortDirection")), "ASC")
        End Get
        Set(value As String)
            ViewState("SortDirection") = value
        End Set
    End Property
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



            DefaultDisplay()
            Stored.Visible = False
            Credit.Visible = False
            Getdta.Visible = False
            Label3.Visible = False
            Label43.Visible = False
            LblINFO.Visible = False
            Label40.Visible = False
            P1.Visible = False
            Table5.Visible = False
            Table7.Visible = False
            Table1.Visible = False
            Table2.Visible = False
            Table3.Visible = False
            tblSingle.Visible = False
            tblSingleRWT.Visible = False
            Label104.Visible = False
            P2.Visible = False
            Label91.Visible = False
            Label32.Visible = True

            'PO_Collection
            Label200.Visible = False
            TableCollections.Visible = False

            Table6.Visible = False
            tblPaymentGrid.Visible = False

            InitDropDownList()
            InitDropDownList1()
            InitDropDownList2()
            InitDropDownList3()
            InitDropDownList5()
            'PO_Collection
            InitDropDownList6()


            TextFromDate.Text = ""
            TextToDate.Text = ""



            'Dim dDate As DateTime = Date.Today


            'TextBox2.Text = dDate.ToString("MM/dd/yyyy", CultureInfo.InvariantCulture)

            'TextBox8.Text = dDate.ToString("MM/dd/yyyy", CultureInfo.InvariantCulture)



            ''Default Year
            'DropDownYear.SelectedValue = Now.Year
            ''Default Month
            'DropDownMonth.SelectedValue = DateTime.Now.AddMonths(-1).Month 'Now.Month


            'drpManagementFunc.
            LoadDB()


            Dim creditInfoModel As New CreditModel
            Dim CreditInfocontrol As New CreditControl

            If DropdownList1.SelectedValue = "Select" Then
                TextBox5.Text = "0.00"
                TextBox6.Text = "0.00"

            Else
                creditInfoModel.BRANCH_NO = DropdownList1.SelectedItem.Text
                Dim _Datatable As DataTable = CreditInfocontrol.Get_Credit_info(creditInfoModel)
                If (_Datatable Is Nothing) Or (_Datatable.Rows.Count = 0) Then
                    Exit Sub
                Else
                    If Not IsDBNull(_Datatable.Rows(0)("CREDIT_LIMIT")) Then
                        TextBox5.Text = _Datatable.Rows(0)("CREDIT_LIMIT")
                    End If
                    If Not IsDBNull(_Datatable.Rows(0)("PER_DAY")) Then
                        TextBox6.Text = _Datatable.Rows(0)("PER_DAY")
                    End If
                End If
            End If
            LoadDB1()

            If DropDownList2.SelectedValue = "Select" Then
                TextBox1.Text = "0.00"


            Else
                creditInfoModel.ship_Name = DropDownList2.SelectedItem.Text
                Dim _Dtable As DataTable = CreditInfocontrol.Get_GSTIN(creditInfoModel)
                If (_Dtable Is Nothing) Or (_Dtable.Rows.Count = 0) Then
                    Exit Sub
                Else
                    If Not IsDBNull(_Dtable.Rows(0)("GSTIN")) Then
                        TextBox1.Text = _Dtable.Rows(0)("GSTIN")
                    End If
                End If
            End If
        End If
    End Sub
    Protected Sub RowDataBound(ByVal sender As Object, ByVal e As GridViewRowEventArgs)
        If e.Row.RowType = DataControlRowType.DataRow AndAlso gvDisplayPaymentValue.EditIndex = e.Row.RowIndex Then
            'Dim ddlCities As DropDownList = CType(e.Row.FindControl("ddlCities"), DropDownList)
            'Dim sql As String = "SELECT DISTINCT City FROM Customers"
            'Dim conString As String = ConfigurationManager.ConnectionStrings("conString").ConnectionString
            'Using con As SqlConnection = New SqlConnection(conString)
            '    Using sda As SqlDataAdapter = New SqlDataAdapter(sql, con)
            '        Using dt As DataTable = New DataTable()
            '            sda.Fill(dt)
            '            ddlCities.DataSource = dt
            '            ddlCities.DataTextField = "City"
            '            ddlCities.DataValueField = "City"
            '            ddlCities.DataBind()
            '            Dim selectedCity As String = DataBinder.Eval(e.Row.DataItem, "City").ToString()
            '            ddlCities.Items.FindByValue(selectedCity).Selected = True
            '        End Using
            '    End Using
            'End Using
        End If
    End Sub

    Protected Sub txtPageSize_TextChanged(sender As Object, e As EventArgs) Handles txtPageSize.TextChanged

        Dim intValue As Integer
        If Integer.TryParse(txtPageSize.Text, intValue) AndAlso intValue > 0 AndAlso intValue <= 9999 Then
            lblErrorMessage.Visible = False

            'Dim dt As DataTable = CType(exfiledtl.ExportAnalysisDetails(Session("Exportinputdtl"), "R"), DataTable)
            Dim _PaymentVlaueControl As PaymentValueControl = New PaymentValueControl()
            Dim dt As DataTable = _PaymentVlaueControl.ShowPaymentValueGrid()
            Dim dv As DataView = New DataView(dt)
            If ViewState("SortExpression") Is Nothing Then
                'dgColdtl.DefaultView.Sort = "unq_no ASC"
                'dv.Sort = gvDisplayPaymentValue.Columns(0).SortExpression & " " & Me.SortDirection
                dv.Sort = "unq_no Desc"

            Else
                dv.Sort = ViewState("SortExpression") & " " & Me.SortDirection
            End If

            txtPageSize.Text = Convert.ToInt16(txtPageSize.Text)
            gvDisplayPaymentValue.PageSize = Convert.ToInt16(txtPageSize.Text)
            gvDisplayPaymentValue.PageIndex = 0
            gvDisplayPaymentValue.DataSource = dv
            gvDisplayPaymentValue.DataBind()

        Else
            lblErrorMessage.Visible = True
            txtPageSize.Text = gvDisplayPaymentValue.PageSize

        End If

    End Sub

    Protected Sub OnRowEditing(sender As Object, e As GridViewEditEventArgs)
        gvDisplayPaymentValue.EditIndex = e.NewEditIndex
        Dim _PaymentVlaueControl As PaymentValueControl = New PaymentValueControl()
        Dim dtDisplayPaymentValue As DataTable = _PaymentVlaueControl.ShowPaymentValueGrid()
        dtDisplayPaymentValue = dtDisplayPaymentValue.DefaultView.ToTable()
        If Not dtDisplayPaymentValue Is Nothing Then
            If dtDisplayPaymentValue.Rows.Count > 0 Then



                gvDisplayPaymentValue.Visible = True
                gvDisplayPaymentValue.DataSource = dtDisplayPaymentValue
                gvDisplayPaymentValue.DataBind()
                

                Dim row As GridViewRow = gvDisplayPaymentValue.Rows(e.NewEditIndex)
                Dim dd1 As DropDownList = row.FindControl("drpdowntargetssc")
                Dim calander As AjaxControlToolkit.CalendarExtender = row.FindControl("CalendarExtendertxttargetdate1")
                calander.EndDate = DateTime.Now.Date

                dd1.DataSource = Session("codeMasterList")
                dd1.DataTextField = "CodeDispValue"
                dd1.DataValueField = "CodeValue"
                dd1.DataBind()
                Dim row2 As GridViewRow = gvDisplayPaymentValue.Rows(e.NewEditIndex)
                Dim hdn As HiddenField = row.FindControl("HiddenField1")
                dd1.Items.FindByText(hdn.Value).Selected = True

                'Dim unq_no As Integer = Convert.ToInt32(gvDisplayPaymentValue.DataKeys(e.RowIndex).Values(0))
                'Dim CreatedDate As String = (TryCast(row.FindControl("txtcreateddate1"), TextBox)).Text

            End If
        End If
    End Sub

    Protected Sub OnRowCancelingEdit(sender As Object, e As EventArgs)
        gvDisplayPaymentValue.EditIndex = -1
        Dim _PaymentVlaueControl As PaymentValueControl = New PaymentValueControl()
        Dim dtDisplayPaymentValue As DataTable = _PaymentVlaueControl.ShowPaymentValueGrid()
        dtDisplayPaymentValue = dtDisplayPaymentValue.DefaultView.ToTable()
        If Not dtDisplayPaymentValue Is Nothing Then
            If dtDisplayPaymentValue.Rows.Count > 0 Then
                gvDisplayPaymentValue.Visible = True
                gvDisplayPaymentValue.DataSource = dtDisplayPaymentValue
                gvDisplayPaymentValue.DataBind()
            End If
        End If
    End Sub
    Protected Sub OnRowUpdating(ByVal sender As Object, ByVal e As GridViewUpdateEventArgs)
        Dim _PaymentVlaueControl As PaymentValueControl = New PaymentValueControl()
        Dim row As GridViewRow = gvDisplayPaymentValue.Rows(e.RowIndex)
        Dim unq_no As Integer = Convert.ToInt32(gvDisplayPaymentValue.DataKeys(e.RowIndex).Values(0))
        Dim CreatedDate As String = (TryCast(row.FindControl("txtcreateddate1"), TextBox)).Text
        'Dim TargetSSC As String = (TryCast(row.FindControl("txttargetssc1"), TextBox)).Text
        Dim TargetSSC As String = (TryCast(row.FindControl("drpdowntargetssc"), DropDownList)).SelectedItem.Text
        Dim PaymentAmount As String = (TryCast(row.FindControl("txtpaymentamount1"), TextBox)).Text
        Dim TargetDate As String = (TryCast(row.FindControl("txttargetdate1"), TextBox)).Text
        'query
        Dim _PaymentVlaueModel As PaymentValueModel = New PaymentValueModel()

        _PaymentVlaueModel.UNQ_NO = Convert.ToString(unq_no)
        '_PaymentVlaueModel.SHIP_TO_BRANCH_CODE = DropdownList5.SelectedItem.Value
        _PaymentVlaueModel.CRTCD = CreatedDate
        _PaymentVlaueModel.SHIP_TO_BRANCH = TargetSSC
        _PaymentVlaueModel.VALUE = PaymentAmount
        _PaymentVlaueModel.TARGET_DATE = TargetDate
        '_PaymentVlaueModel.UserId = Session("user_id").ToString

        Dim dtUpdatePaymentValue As Boolean = _PaymentVlaueControl.UpdatePaymentValueFromGrid(_PaymentVlaueModel)

        gvDisplayPaymentValue.EditIndex = -1

        Dim dtDisplayPaymentValue As DataTable = _PaymentVlaueControl.ShowPaymentValueGrid()
        dtDisplayPaymentValue = dtDisplayPaymentValue.DefaultView.ToTable()
        If Not dtDisplayPaymentValue Is Nothing Then
            If dtDisplayPaymentValue.Rows.Count > 0 Then
                gvDisplayPaymentValue.Visible = True
                gvDisplayPaymentValue.DataSource = dtDisplayPaymentValue
                gvDisplayPaymentValue.DataBind()
            End If
        End If
    End Sub

    Protected Sub OnPageIndexChanging(ByVal sender As Object, ByVal e As GridViewPageEventArgs)
        gvDisplayPaymentValue.PageIndex = e.NewPageIndex
        Dim _PaymentVlaueControl As PaymentValueControl = New PaymentValueControl()
        Dim dt As DataTable = _PaymentVlaueControl.ShowPaymentValueGrid()
        'Dim dt As DataTable = CType(exfiledtl.ExportAnalysisDetails(Session("Exportinputdtl"), "R"), DataTable)

        Dim dv As DataView = New DataView(dt)
        If ViewState("SortExpression") Is Nothing Then
            'dv.Sort = gvDisplayPaymentValue.Columns(0).SortExpression & " " & Me.SortDirection
            dv.Sort = "unq_no Desc"
        Else
            dv.Sort = ViewState("SortExpression") & " " & Me.SortDirection
        End If
        ' dv.Sort = "ServiceOrder_No " & Me.SortDirection
        gvDisplayPaymentValue.DataSource = dv
        gvDisplayPaymentValue.DataBind()
        '_ScDsrControl.ExportScDsr(Session("Exportinputdtl"))
        ' Me.BindGrid()
    End Sub

    Protected Sub OnSorting(ByVal sender As Object, ByVal e As GridViewSortEventArgs)
        If IsPostBack = True Then
            Dim _PaymentVlaueControl As PaymentValueControl = New PaymentValueControl()
            Dim dt As DataTable = _PaymentVlaueControl.ShowPaymentValueGrid()
            'Dim dt As DataTable = CType(exfiledtl.ExportAnalysisDetails(Session("Exportinputdtl"), "R"), DataTable)
            If (Not (dt) Is Nothing) Then
                Dim dv As DataView = New DataView(dt)
                ViewState("SortExpression") = e.SortExpression

                Me.SortDirection = IIf(Me.SortDirection = "ASC", "DESC", "ASC")
                dv.Sort = e.SortExpression & " " & Me.SortDirection
                gvDisplayPaymentValue.DataSource = dv
                gvDisplayPaymentValue.DataBind()
                '
            End If
        End If
        'https://www.aspsnippets.com/Articles/Ascending-Descending-Sorting-using-Columns-Header-in-ASPNet-GridView.aspx
        'https://www.codeproject.com/Articles/1195569/Angular-Data-Grid-with-Sorting-Filtering-Export-to
        'https://forums.asp.net/t/1412788.aspx?How+to+Export+GridView+To+Word+Excel+PDF+CSV+in+ASP+Net

    End Sub
    Private Function ConvertSortDirectionToSql(ByVal sortDirection As SortDirection) As String
        Dim newSortDirection As String = String.Empty
        Select Case (sortDirection)
            Case SortDirection.Ascending
                newSortDirection = "ASC"
            Case SortDirection.Descending
                newSortDirection = "DESC"
        End Select

        Return newSortDirection
    End Function




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
        Dim codeMasterList As List(Of CodeMasterModel) = codeMasterControl.SelectBranchSSC(QryFlag, "'" & UserInfoList(0).Ship1.Replace(",", "','") & "'")

        ' Dim codeMasterControl As CodeMasterControl = New CodeMasterControl()
        'Loading branch name and code in the dropdown list
        '  Dim codeMasterList As List(Of CodeMasterModel) = codeMasterControl.SelectBranchMaster()
        'Dim codeMaster1 As CodeMasterModel = New CodeMasterModel()
        'codeMaster1.CodeValue = "Select Branch"
        'codeMaster1.CodeDispValue = "Select Branch"
        'codeMasterList.Insert(0, codeMaster1)

        Dim codeMaster2 As CodeMasterModel = New CodeMasterModel()
        codeMaster2.CodeValue = "ALL"
        codeMaster2.CodeDispValue = "ALL"
        codeMasterList.Insert(0, codeMaster2)



        Me.DropListLocation.DataSource = codeMasterList
        Me.DropListLocation.DataTextField = "CodeDispValue"
        Me.DropListLocation.DataValueField = "CodeValue"
        Me.DropListLocation.DataBind()
    End Sub

    ''' <summary>
    ''' Loading All branches
    ''' </summary>
    Private Sub InitDropDownList1()
        Log4NetControl.ComInfoLogWrite(Session("UserName"))
        Dim userName As String = Session("user_id") 'Session("user_Name")
        'Clear the branch location
        DropdownList1.Items.Clear()
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
        Dim codeMasterList As List(Of CodeMasterModel) = codeMasterControl.SelectBranchSSCCreditInfo(QryFlag, "'" & UserInfoList(0).Ship1.Replace(",", "','") & "'")

        ' Dim codeMasterControl As CodeMasterControl = New CodeMasterControl()
        'Loading branch name and code in the dropdown list
        '  Dim codeMasterList As List(Of CodeMasterModel) = codeMasterControl.SelectBranchMaster()
        'Dim codeMaster1 As CodeMasterModel = New CodeMasterModel()
        'codeMaster1.CodeValue = "Select Branch"
        'codeMaster1.CodeDispValue = "Select Branch"
        'codeMasterList.Insert(0, codeMaster1)

        Dim codeMaster2 As CodeMasterModel = New CodeMasterModel()
        codeMaster2.CodeValue = "Select"
        codeMaster2.CodeDispValue = "Select"
        codeMasterList.Insert(0, codeMaster2)



        Me.DropdownList1.DataSource = codeMasterList
        Me.DropdownList1.DataTextField = "CodeDispValue"
        Me.DropdownList1.DataValueField = "CodeValue"
        Me.DropdownList1.DataBind()
    End Sub
    Private Sub InitDropDownList2()
        Log4NetControl.ComInfoLogWrite(Session("UserName"))
        Dim userName As String = Session("user_id") 'Session("user_Name")
        'Clear the branch location
        DropDownList2.Items.Clear()
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
        Dim codeMasterList As List(Of CodeMasterModel) = codeMasterControl.SelectBranchSSC(QryFlag, "'" & UserInfoList(0).Ship1.Replace(",", "','") & "'")

        ' Dim codeMasterControl As CodeMasterControl = New CodeMasterControl()
        'Loading branch name and code in the dropdown list
        '  Dim codeMasterList As List(Of CodeMasterModel) = codeMasterControl.SelectBranchMaster()
        'Dim codeMaster1 As CodeMasterModel = New CodeMasterModel()
        'codeMaster1.CodeValue = "Select Branch"
        'codeMaster1.CodeDispValue = "Select Branch"
        'codeMasterList.Insert(0, codeMaster1)

        Dim codeMaster2 As CodeMasterModel = New CodeMasterModel()
        codeMaster2.CodeValue = "Select"
        codeMaster2.CodeDispValue = "Select"
        codeMasterList.Insert(0, codeMaster2)



        Me.DropDownList2.DataSource = codeMasterList
        Me.DropDownList2.DataTextField = "CodeDispValue"
        Me.DropDownList2.DataValueField = "CodeValue"
        Me.DropDownList2.DataBind()
    End Sub

    Private Sub InitDropDownList3()
        Log4NetControl.ComInfoLogWrite(Session("UserName"))
        Dim userName As String = Session("user_id") 'Session("user_Name")
        'Clear the branch location
        DropdownList3.Items.Clear()
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
        Dim codeMasterList As List(Of CodeMasterModel) = codeMasterControl.SelectBranchSSC(QryFlag, "'" & UserInfoList(0).Ship1.Replace(",", "','") & "'")

        ' Dim codeMasterControl As CodeMasterControl = New CodeMasterControl()
        'Loading branch name and code in the dropdown list
        '  Dim codeMasterList As List(Of CodeMasterModel) = codeMasterControl.SelectBranchMaster()
        'Dim codeMaster1 As CodeMasterModel = New CodeMasterModel()
        'codeMaster1.CodeValue = "Select Branch"
        'codeMaster1.CodeDispValue = "Select Branch"
        'codeMasterList.Insert(0, codeMaster1)

        Dim codeMaster2 As CodeMasterModel = New CodeMasterModel()

        codeMaster2.CodeValue = "ALL"
        codeMaster2.CodeDispValue = "ALL"
        'codeMaster2.CodeValue = "Select"
        'codeMaster2.CodeDispValue = "Select"

        codeMasterList.Insert(0, codeMaster2) '


        Me.DropdownList3.DataSource = codeMasterList
        Me.DropdownList3.DataTextField = "CodeDispValue"
        Me.DropdownList3.DataValueField = "CodeValue"
        Me.DropdownList3.DataBind()

        If DropdownList3.SelectedItem.Text = "ALL" Then
            Table5.Visible = False
            Table7.Visible = False
            TextDateTo.Text = ""
            TextFromDate.Text = ""
        End If
        If DropdownList3.SelectedItem.Text = "SSC1" Then
            Table5.Visible = False
            Table7.Visible = False
            TextDateTo.Text = ""
            TextFromDate.Text = ""
        End If
        If DropdownList3.SelectedItem.Text = "SSC2" Then
            Table5.Visible = False
            Table7.Visible = False
            TextDateTo.Text = ""
            TextFromDate.Text = ""
        End If
        If DropdownList3.SelectedItem.Text = "SSC3" Then
            Table5.Visible = False
            Table7.Visible = False
            TextDateTo.Text = ""
            TextFromDate.Text = ""
        End If
        If DropdownList3.SelectedItem.Text = "SSC4" Then
            Table5.Visible = False
            Table7.Visible = False
            TextDateTo.Text = ""
            TextFromDate.Text = ""
        End If
        If DropdownList3.SelectedItem.Text = "SSC5" Then
            Table5.Visible = False
            Table7.Visible = False
            TextDateTo.Text = ""
            TextFromDate.Text = ""
        End If
        If DropdownList3.SelectedItem.Text = "SSC6" Then
            Table5.Visible = False
            Table7.Visible = False
            TextDateTo.Text = ""
            TextFromDate.Text = ""
        End If
        If DropdownList3.SelectedItem.Text = "SSC7" Then
            Table5.Visible = False
            Table7.Visible = False
            TextDateTo.Text = ""
            TextFromDate.Text = ""
        End If
        If DropdownList3.SelectedItem.Text = "SSC8" Then
            Table5.Visible = False
            Table7.Visible = False
            TextDateTo.Text = ""
            TextFromDate.Text = ""
        End If
        If DropdownList3.SelectedItem.Text = "SSC9" Then
            Table5.Visible = False
            Table7.Visible = False
            TextDateTo.Text = ""
            TextFromDate.Text = ""
        End If
    End Sub

    Private Sub InitDropDownList5()
        Log4NetControl.ComInfoLogWrite(Session("UserName"))
        Dim userName As String = Session("user_id") 'Session("user_Name")
        'Clear the branch location
        DropdownList5.Items.Clear()
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
        Dim codeMasterList As List(Of CodeMasterModel) = codeMasterControl.SelectBranchSSC(QryFlag, "'" & UserInfoList(0).Ship1.Replace(",", "','") & "'")

        ' Dim codeMasterControl As CodeMasterControl = New CodeMasterControl()
        'Loading branch name and code in the dropdown list
        '  Dim codeMasterList As List(Of CodeMasterModel) = codeMasterControl.SelectBranchMaster()
        'Dim codeMaster1 As CodeMasterModel = New CodeMasterModel()
        'codeMaster1.CodeValue = "Select Branch"
        'codeMaster1.CodeDispValue = "Select Branch"
        'codeMasterList.Insert(0, codeMaster1)

        Dim codeMaster2 As CodeMasterModel = New CodeMasterModel()
        codeMaster2.CodeValue = "Select"
        codeMaster2.CodeDispValue = "Select"
        codeMasterList.Insert(0, codeMaster2)



        Me.DropdownList5.DataSource = codeMasterList
        Me.DropdownList5.DataTextField = "CodeDispValue"
        Me.DropdownList5.DataValueField = "CodeValue"
        Me.DropdownList5.DataBind()

        codeMasterList.RemoveAt(0)
        Session("codeMasterList") = codeMasterList
    End Sub

    'PO_Collection
    Private Sub InitDropDownList6()
        Log4NetControl.ComInfoLogWrite(Session("UserName"))
        Dim userName As String = Session("user_id") 'Session("user_Name")
        'Clear the branch location
        DropdownList6.Items.Clear()
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
        Dim codeMasterList As List(Of CodeMasterModel) = codeMasterControl.SelectBranchSSC(QryFlag, "'" & UserInfoList(0).Ship1.Replace(",", "','") & "'")

        ' Dim codeMasterControl As CodeMasterControl = New CodeMasterControl()
        'Loading branch name and code in the dropdown list
        '  Dim codeMasterList As List(Of CodeMasterModel) = codeMasterControl.SelectBranchMaster()
        'Dim codeMaster1 As CodeMasterModel = New CodeMasterModel()
        'codeMaster1.CodeValue = "Select Branch"
        'codeMaster1.CodeDispValue = "Select Branch"
        'codeMasterList.Insert(0, codeMaster1)

        Dim codeMaster2 As CodeMasterModel = New CodeMasterModel()
        codeMaster2.CodeValue = "Select"
        codeMaster2.CodeDispValue = "Select"
        codeMasterList.Insert(0, codeMaster2)



        Me.DropdownList6.DataSource = codeMasterList
        Me.DropdownList6.DataTextField = "CodeDispValue"
        Me.DropdownList6.DataValueField = "CodeValue"
        Me.DropdownList6.DataBind()
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


        'Dim _dtPlReprt As New DataTable

        '_dtPlReprt.Columns.Add("CustomerVisit", GetType(Integer))
        '_dtPlReprt.Columns.Add("CallRegistered", GetType(Integer))
        '_dtPlReprt.Columns.Add("RepairCompleted", GetType(Integer))
        '_dtPlReprt.Columns.Add("GoodsDelivered", GetType(Integer))
        '_dtPlReprt.Columns.Add("Pending", GetType(Integer))
        '_dtPlReprt.Columns.Add("Warranty", GetType(Integer))
        '_dtPlReprt.Columns.Add("InWarranty", GetType(Integer))
        '_dtPlReprt.Columns.Add("OutWarranty", GetType(Integer))
        '_dtPlReprt.Columns.Add("InWarrantyAmount", GetType(Decimal))
        '_dtPlReprt.Columns.Add("InWarrantyLabour", GetType(Decimal))
        '_dtPlReprt.Columns.Add("InWarrantyParts", GetType(Decimal))
        '_dtPlReprt.Columns.Add("InWarrantyTransport", GetType(Decimal))
        '_dtPlReprt.Columns.Add("InWarrantyOthers", GetType(Decimal))
        '_dtPlReprt.Columns.Add("OutWarrantyAmount", GetType(Decimal))
        '_dtPlReprt.Columns.Add("OutWarrantyLabour", GetType(Decimal))
        '_dtPlReprt.Columns.Add("OutWarrantyParts", GetType(Decimal))
        '_dtPlReprt.Columns.Add("OutWarrantyTransport", GetType(Decimal))
        '_dtPlReprt.Columns.Add("OutWarrantyOthers", GetType(Decimal))
        '_dtPlReprt.Columns.Add("SawDiscountLabour", GetType(Decimal))
        '_dtPlReprt.Columns.Add("SawDiscountParts", GetType(Decimal))
        '_dtPlReprt.Columns.Add("OutWarrantyLabourwSawDisc", GetType(Decimal))
        '_dtPlReprt.Columns.Add("OutWarrantyPartswSawDisc", GetType(Decimal))
        '_dtPlReprt.Columns.Add("RevenueWithoutTax", GetType(Decimal))
        '_dtPlReprt.Columns.Add("IwPartsConsumed", GetType(Decimal))
        '_dtPlReprt.Columns.Add("TotalPartsConsumed", GetType(Decimal))
        '_dtPlReprt.Columns.Add("PrimeCostTotal", GetType(Decimal))
        '_dtPlReprt.Columns.Add("GrossProfit", GetType(Decimal))

        '_dtPlReprt.Rows.Add(0, 0, 0, 0, 0, 570, 255, 315, 44375, 44375, 621928, 0, 0, 822336.7, 77437.49, 693913.75, 0, 0, 3850, 47135.46, 81287.49, 741049.21, 866711.7, 547296.66, 1199419.94, 652123.28, 214588.42)

        'Validation 
        If Not (CommonControl.isNumberCT_CC(txtGM.Text)) Then
            Call showMsg("The GM value is invalid...", "")
            Exit Sub
        End If

        If DrpType.SelectedItem.Value = "0" Then
            Call showMsg("Please select the type", "")
            Exit Sub
        End If

        'For Month Selected or Not
        If DrpType.SelectedItem.Value = "02" Then
            If DropDownMonth.SelectedItem.Value = "0" Then
                Call showMsg("Please select the month", "")
                Exit Sub
            End If
        End If

        'For Month Selected or Not
        If DrpType.SelectedItem.Value = "03" Then
            'TextDateFrom
            'TextDateTo
            Dim dt As DateTime
            If TextDateFrom.Text <> "" Then
                If DateTime.TryParse(TextDateFrom.Text, dt) Then
                Else
                    Call showMsg("The from date specification is incorrect.", "")
                    Exit Sub
                End If
            Else
                Call showMsg("Please enter from date.", "")
                Exit Sub
            End If
            If TextDateTo.Text <> "" Then
                If DateTime.TryParse(TextDateTo.Text, dt) Then
                Else
                    Call showMsg("The to date specification is incorrect.", "")
                    Exit Sub
                End If
            Else
                Call showMsg("Please enter to date.", "")
                Exit Sub
            End If
        End If


        'Define Variables
        Dim comcontrol As New CommonControl
        Dim _dtPlReprt As New DataTable

        Dim intNumberOfCounters As Integer = 0
        Dim intNumberOfBusinessDat As Integer = 0
        Dim intNumberOfStaffs As Integer = 0


        Dim intCustomerVisit As Integer = 0
        Dim intCallRegistered As Integer = 0
        Dim intRepairCompleted As Integer = 0
        Dim intGoodsDelivered As Integer = 0
        Dim intPending As Integer = 0
        Dim intWarranty As Integer = 0
        Dim intInWarranty As Integer = 0
        Dim intOutWarranty As Integer = 0
        Dim decInWarrantyAmount As Decimal = 0.00
        Dim decInWarrantyLabour As Decimal = 0.00
        Dim decInWarrantyParts As Decimal = 0.00
        Dim decInWarrantyTransport As Decimal = 0.00
        Dim decInWarrantyOthers As Decimal = 0.00
        Dim decOutWarrantyAmount As Decimal = 0.00
        Dim decOutWarrantyLabour As Decimal = 0.00
        Dim decOutWarrantyParts As Decimal = 0.00
        Dim decOutWarrantyTransport As Decimal = 0.00
        Dim decOutWarrantyOthers As Decimal = 0.00
        Dim decSawDiscountLabour As Decimal = 0.00
        Dim decSawDiscountParts As Decimal = 0.00
        Dim decOutWarrantyLabourwSawDisc As Decimal = 0.00
        Dim decOutWarrantyPartswSawDisc As Decimal = 0.00
        Dim decRevenueWithoutTax As Decimal = 0.00
        Dim decIwPartsConsumed As Decimal = 0.00
        Dim decTotalPartsConsumed As Decimal = 0.00
        Dim decPrimeCostTotal As Decimal = 0.00
        Dim decGrossProfit As Decimal = 0.00
        Dim decFinalPercentage As Decimal = 0.00

        Dim intNumberOfCountersTot As Integer = 0
        Dim intNumberOfBusinessDatTot As Integer = 0
        Dim intNumberOfStaffsTot As Integer = 0

        Dim intCustomerVisitTot As Integer = 0
        Dim intCallRegisteredTot As Integer = 0
        Dim intRepairCompletedTot As Integer = 0
        Dim intGoodsDeliveredTot As Integer = 0
        Dim intPendingTot As Integer = 0
        Dim intWarrantyTot As Integer = 0
        Dim intInWarrantyTot As Integer = 0
        Dim intOutWarrantyTot As Integer = 0
        Dim decInWarrantyAmountTot As Decimal = 0.00
        Dim decInWarrantyLabourTot As Decimal = 0.00
        Dim decInWarrantyPartsTot As Decimal = 0.00
        Dim decInWarrantyTransportTot As Decimal = 0.00
        Dim decInWarrantyOthersTot As Decimal = 0.00
        Dim decOutWarrantyAmountTot As Decimal = 0.00
        Dim decOutWarrantyLabourTot As Decimal = 0.00
        Dim decOutWarrantyPartsTot As Decimal = 0.00
        Dim decOutWarrantyTransportTot As Decimal = 0.00
        Dim decOutWarrantyOthersTot As Decimal = 0.00
        Dim decSawDiscountLabourTot As Decimal = 0.00
        Dim decSawDiscountPartsTot As Decimal = 0.00
        Dim decOutWarrantyLabourwSawDiscTot As Decimal = 0.00
        Dim decOutWarrantyPartswSawDiscTot As Decimal = 0.00
        Dim decRevenueWithoutTaxTot As Decimal = 0.00
        Dim decIwPartsConsumedTot As Decimal = 0.00
        Dim decTotalPartsConsumedTot As Decimal = 0.00
        Dim decPrimeCostTotalTot As Decimal = 0.00
        Dim decGrossProfitTot As Decimal = 0.00
        Dim decFinalPercentageTot As Decimal = 0.00

        'GM
        Dim Gm As Decimal = 0.88
        Gm = txtGM.Text

        'For Temporary
        Dim FinalPercentage1 As Decimal = 0.00
        Dim SscTotals031 As Decimal = 0.00
        Dim SscTotals131 As Decimal = 0.00
        Dim SscTotals231 As Decimal = 0.00
        Dim SscTotals331 As Decimal = 0.00


        Dim _FinalPercentage As Decimal = 0.00



        'Today Date
        Dim strDay As String = ""
        Dim strMon As String = ""
        Dim strYear As String = ""

        Dim strDate As String = ""

        strDay = Now.Day()
        strMon = Now.Month
        strYear = Now.Year

        If Len(strDay) < 1 Then
            strDay = "0" & strDay
        End If
        If Len(strMon) < 1 Then
            strMon = "0" & strMon
        End If
        strDate = strYear & "/" & strMon & "/" & strDay

        Dim clsSet As New Class_analysis


        Dim csvContents = New List(Of String)
        '****************************************
        '4week 
        'Begin
        '****************************************
        If DrpType.SelectedItem.Value = "01" Then ' 4week
            'For FileName
            Dim PeriodFrom As New System.DateTime(strYear, strMon, strDay, 0, 0, 0)

            Dim Sunday4Week As New System.DateTime(strYear, strMon, strDay, 0, 0, 0)
            If (Sunday4Week.DayOfWeek = 1) Then
                'Monday
                Sunday4Week = Sunday4Week.AddDays(-1)
            ElseIf (Sunday4Week.DayOfWeek = 2) Then
                'Tuesday
                Sunday4Week = Sunday4Week.AddDays(-2)
            ElseIf (Sunday4Week.DayOfWeek = 3) Then
                'Wednesday
                Sunday4Week = Sunday4Week.AddDays(-3)
            ElseIf (Sunday4Week.DayOfWeek = 4) Then
                'Thursday
                Sunday4Week = Sunday4Week.AddDays(-4)
            ElseIf (Sunday4Week.DayOfWeek = 5) Then
                'Friday
                Sunday4Week = Sunday4Week.AddDays(-5)
            ElseIf (Sunday4Week.DayOfWeek = 6) Then
                'Saturday
                Sunday4Week = Sunday4Week.AddDays(-6)
            ElseIf (Sunday4Week.DayOfWeek = 7) Then
                'Sunday
                'No change
            End If



            Dim Monday4Week As New System.DateTime(strYear, strMon, strDay, 0, 0, 0)
            'Find Last Monday from Last Sunday
            Monday4Week = Sunday4Week.AddDays(-6)

            'Define Date
            Dim Monday3Week As New System.DateTime(strYear, strMon, strDay, 0, 0, 0)
            Dim Sunday3Week As New System.DateTime(strYear, strMon, strDay, 0, 0, 0)
            Monday3Week = Monday4Week.AddDays(-7)
            Sunday3Week = Sunday4Week.AddDays(-7)
            Dim Monday2Week As New System.DateTime(strYear, strMon, strDay, 0, 0, 0)
            Dim Sunday2Week As New System.DateTime(strYear, strMon, strDay, 0, 0, 0)
            Monday2Week = Monday3Week.AddDays(-7)
            Sunday2Week = Sunday3Week.AddDays(-7)
            Dim Monday1Week As New System.DateTime(strYear, strMon, strDay, 0, 0, 0)
            Dim Sunday1Week As New System.DateTime(strYear, strMon, strDay, 0, 0, 0)
            Monday1Week = Monday2Week.AddDays(-7)
            Sunday1Week = Sunday2Week.AddDays(-7)



            '1st Find day of week
            'lblText.Text = "1st Week From: " & Monday1Week & "  To: " & Sunday1Week
            'lblText.Text = lblText.Text & "<br>2nd Week From: " & Monday2Week & "  To: " & Sunday2Week
            'lblText.Text = lblText.Text & "<br>3rd Week From: " & Monday3Week & "  To: " & Sunday3Week
            'lblText.Text = lblText.Text & "<br>4th  Week From: " & Monday4Week & "  To: " & Sunday4Week



            Dim row0 As String = ""

            If DropListLocation.SelectedItem.Text = "ALL" Then
                row0 = ""
            Else
                row0 = DropListLocation.SelectedItem.Text
            End If
            Dim rowTitle As String = " "

            Dim rowNumberOfCounters As String = "Number of counters"
            Dim rowNumberOfBusinessDat As String = "Number of Business dat"
            Dim rowNumberOfStaffs As String = "Number of Staffs"

            Dim rowCustomerVisit As String = "Customer Visit"
            Dim rowCallRegistered As String = "Call Registered"
            Dim rowRepairCompleted As String = "Repair Completed"
            Dim rowGoodsDelivered As String = "Goods Delivered"
            Dim rowPending As String = "Pending"
            Dim rowWarranty As String = "Warranty"
            Dim rowInWarranty As String = "InWarranty"
            Dim rowOutWarranty As String = "OutWarranty"
            Dim rowInWarrantyAmount As String = "InWarrantyAmount"
            Dim rowInWarrantyLabour As String = "InWarrantyLabour"
            Dim rowInWarrantyParts As String = "InWarrantyParts"
            Dim rowInWarrantyTransport As String = "InWarrantyTransport"
            Dim rowInWarrantyOthers As String = "InWarrantyOthers"
            Dim rowOutWarrantyAmount As String = "OutWarrantyAmount"
            Dim rowOutWarrantyLabour As String = "OutWarrantyLabour"
            Dim rowOutWarrantyParts As String = "OutWarrantyParts"
            Dim rowOutWarrantyTransport As String = "OutWarrantyTransport"
            Dim rowOutWarrantyOthers As String = "OutWarrantyOthers"
            Dim rowSawDiscountLabour As String = "SawDiscountLabour"
            Dim rowSawDiscountParts As String = "SawDiscountParts"
            Dim rowOutWarrantyLabourwSawDisc As String = "OutWarrantyLabourwSawDisc"
            Dim rowOutWarrantyPartswSawDisc As String = "OutWarrantyPartswSawDisc"
            Dim rowRevenueWithoutTax As String = "RevenueWithoutTax"
            Dim rowIwPartsConsumed As String = "IwPartsConsumed"
            Dim rowTotalPartsConsumed As String = "OutTotalPartsConsumed"
            Dim rowPrimeCostTotal As String = "PrimeCostTotal"
            Dim rowGrossProfit As String = "GrossProfit"

            Dim rowFinalPercentage As String = " "





            'For all ssc total 
            Dim SscTotals(6, 31) As String



            For Each item As ListItem In DropListLocation.Items
                If DropListLocation.SelectedItem.Text = "ALL" Then

                    If item.Text = "ALL" Then 'Skip 1st List when  "ALL" in the loop
                        Continue For
                    Else
                        'lblLoc.Text = lblLoc.Text & "<br>" & item.Text

                        'Monday1Week & Sunday1Week
                        strDay = Monday1Week.Day()
                        strMon = Monday1Week.Month
                        strYear = Monday1Week.Year

                        If Len(strDay) <= 1 Then
                            strDay = "0" & strDay
                        End If
                        If Len(strMon) <= 1 Then
                            strMon = "0" & strMon
                        End If
                        strDate = strYear & strMon & strDay


                        'Read from 
                        _dtPlReprt = comcontrol.SelectPlReport(Monday1Week, Sunday1Week, item.Text, item.Value, Gm)

                        If (_dtPlReprt Is Nothing) Or (_dtPlReprt.Rows.Count = 0) Then
                            row0 = row0 & "," & item.Text
                            rowTitle = rowTitle & "," & strDate

                            rowNumberOfCounters = rowNumberOfCounters & ","
                            rowNumberOfBusinessDat = rowNumberOfBusinessDat & ","
                            rowNumberOfStaffs = rowNumberOfStaffs & ","

                            rowCustomerVisit = rowCustomerVisit & ",0"
                            rowCallRegistered = rowCallRegistered & ",0"
                            rowRepairCompleted = rowRepairCompleted & ",0"
                            rowGoodsDelivered = rowGoodsDelivered & "," & intGoodsDelivered
                            rowPending = rowPending & "," & intPending
                            rowWarranty = rowWarranty & "," & intWarranty
                            rowInWarranty = rowInWarranty & "," & intInWarranty
                            rowOutWarranty = rowOutWarranty & "," & intOutWarranty
                            rowInWarrantyAmount = rowInWarrantyAmount & "," & decInWarrantyAmount
                            rowInWarrantyLabour = rowInWarrantyLabour & "," & decInWarrantyLabour
                            rowInWarrantyParts = rowInWarrantyParts & "," & decInWarrantyParts
                            rowInWarrantyTransport = rowInWarrantyTransport & "," & decInWarrantyTransport
                            rowInWarrantyOthers = rowInWarrantyOthers & "," & decInWarrantyOthers
                            rowOutWarrantyAmount = rowOutWarrantyAmount & "," & decOutWarrantyAmount
                            rowOutWarrantyLabour = rowOutWarrantyLabour & "," & decOutWarrantyLabour
                            rowOutWarrantyParts = rowOutWarrantyParts & "," & decOutWarrantyParts
                            rowOutWarrantyTransport = rowOutWarrantyTransport & "," & decOutWarrantyTransport
                            rowOutWarrantyOthers = rowOutWarrantyOthers & "," & decOutWarrantyOthers
                            rowSawDiscountLabour = rowSawDiscountLabour & "," & decSawDiscountLabour
                            rowSawDiscountParts = rowSawDiscountParts & "," & decSawDiscountParts
                            rowOutWarrantyLabourwSawDisc = rowOutWarrantyLabourwSawDisc & "," & decOutWarrantyLabourwSawDisc
                            rowOutWarrantyPartswSawDisc = rowOutWarrantyPartswSawDisc & "," & decOutWarrantyPartswSawDisc
                            rowRevenueWithoutTax = rowRevenueWithoutTax & "," & decRevenueWithoutTax
                            rowIwPartsConsumed = rowIwPartsConsumed & "," & decIwPartsConsumed
                            rowTotalPartsConsumed = rowTotalPartsConsumed & "," & decTotalPartsConsumed
                            rowPrimeCostTotal = rowPrimeCostTotal & "," & decPrimeCostTotal
                            rowGrossProfit = rowGrossProfit & "," & decGrossProfit

                            'rowFinalPercentage = rowFinalPercentage & "," & decFinalPercentage
                            If decRevenueWithoutTax = 0 Then
                                FinalPercentage1 = decGrossProfit
                            Else
                                FinalPercentage1 = comcontrol.Money_Round((decGrossProfit / decRevenueWithoutTax) * 100, 2)
                            End If
                            rowFinalPercentage = rowFinalPercentage & "," & FinalPercentage1 & "%" 'decFinalPercentage / 100


                        Else
                            For Each row As DataRow In _dtPlReprt.Rows

                                intNumberOfCounters = row.Item("NumberOfCounters")
                                intNumberOfBusinessDat = row.Item("NumberOfBusinessDat")
                                intNumberOfStaffs = row.Item("NumberOfStaffs")

                                intCustomerVisit = row.Item("CustomerVisit")
                                intCallRegistered = row.Item("CallRegistered")
                                intRepairCompleted = row.Item("RepairCompleted")
                                intGoodsDelivered = row.Item("GoodsDelivered")
                                intPending = row.Item("Pending")
                                intWarranty = row.Item("Warranty")
                                intInWarranty = row.Item("InWarranty")
                                intOutWarranty = row.Item("OutWarranty")
                                decInWarrantyAmount = row.Item("InWarrantyAmount")
                                decInWarrantyLabour = row.Item("InWarrantyLabour")
                                decInWarrantyParts = row.Item("InWarrantyParts")
                                decInWarrantyTransport = row.Item("InWarrantyTransport")
                                decInWarrantyOthers = row.Item("InWarrantyOthers")
                                decOutWarrantyAmount = row.Item("OutWarrantyAmount")
                                decOutWarrantyLabour = row.Item("OutWarrantyLabour")
                                decOutWarrantyParts = row.Item("OutWarrantyParts")
                                decOutWarrantyTransport = row.Item("OutWarrantyTransport")
                                decOutWarrantyOthers = row.Item("OutWarrantyOthers")
                                decSawDiscountLabour = row.Item("SawDiscountLabour")
                                decSawDiscountParts = row.Item("SawDiscountParts")
                                decOutWarrantyLabourwSawDisc = row.Item("OutWarrantyLabourwSawDisc")
                                decOutWarrantyPartswSawDisc = row.Item("OutWarrantyPartswSawDisc")
                                decRevenueWithoutTax = row.Item("RevenueWithoutTax")
                                decIwPartsConsumed = row.Item("IwPartsConsumed")
                                decTotalPartsConsumed = row.Item("TotalPartsConsumed")
                                decPrimeCostTotal = row.Item("PrimeCostTotal")
                                decGrossProfit = row.Item("GrossProfit")
                                decFinalPercentage = row.Item("FinalPercentage")

                                intNumberOfCountersTot = intNumberOfCountersTot + intNumberOfCounters
                                intNumberOfBusinessDatTot = intNumberOfBusinessDatTot + intNumberOfBusinessDat
                                intNumberOfStaffsTot = intNumberOfStaffsTot + intNumberOfStaffs

                                intCustomerVisitTot = intCustomerVisitTot + intCustomerVisit
                                intCallRegisteredTot = intCallRegisteredTot + intCallRegistered
                                intRepairCompletedTot = intRepairCompletedTot + intRepairCompleted
                                intGoodsDeliveredTot = intGoodsDeliveredTot + intGoodsDelivered
                                intPendingTot = intPendingTot + intPending
                                intWarrantyTot = intWarrantyTot + intWarranty
                                intInWarrantyTot = intInWarrantyTot + intInWarranty
                                intOutWarrantyTot = intOutWarrantyTot + intOutWarranty
                                decInWarrantyAmountTot = decInWarrantyAmountTot + decInWarrantyAmount
                                decInWarrantyLabourTot = decInWarrantyLabourTot + decInWarrantyLabour
                                decInWarrantyPartsTot = decInWarrantyPartsTot + decInWarrantyParts
                                decInWarrantyTransportTot = decInWarrantyTransportTot + decInWarrantyTransport
                                decInWarrantyOthersTot = decInWarrantyOthersTot + decInWarrantyOthers
                                decOutWarrantyAmountTot = decOutWarrantyAmountTot + decOutWarrantyAmount
                                decOutWarrantyLabourTot = decOutWarrantyLabourTot + decOutWarrantyLabour
                                decOutWarrantyPartsTot = decOutWarrantyPartsTot + decOutWarrantyParts
                                decOutWarrantyTransportTot = decOutWarrantyTransportTot + decOutWarrantyTransport
                                decOutWarrantyOthersTot = decOutWarrantyOthersTot + decOutWarrantyOthers
                                decSawDiscountLabourTot = decSawDiscountLabourTot + decSawDiscountLabour
                                decSawDiscountPartsTot = decSawDiscountPartsTot + decSawDiscountParts
                                decOutWarrantyLabourwSawDiscTot = decOutWarrantyLabourwSawDiscTot + decOutWarrantyLabourwSawDisc
                                decOutWarrantyPartswSawDiscTot = decOutWarrantyPartswSawDiscTot + decOutWarrantyPartswSawDisc
                                decRevenueWithoutTaxTot = decRevenueWithoutTaxTot + decRevenueWithoutTax
                                decIwPartsConsumedTot = decIwPartsConsumedTot + decIwPartsConsumed
                                decTotalPartsConsumedTot = decTotalPartsConsumedTot + decTotalPartsConsumed
                                decPrimeCostTotalTot = decPrimeCostTotalTot + decPrimeCostTotal
                                decGrossProfitTot = decGrossProfitTot + decGrossProfit

                                decFinalPercentageTot = decFinalPercentageTot + decFinalPercentage

                                row0 = row0 & "," & item.Text
                                rowTitle = rowTitle & "," & strDate

                                rowNumberOfCounters = rowNumberOfCounters & "," '& intNumberOfCounters
                                rowNumberOfBusinessDat = rowNumberOfBusinessDat & "," '& intNumberOfBusinessDat
                                rowNumberOfStaffs = rowNumberOfStaffs & "," '& intNumberOfStaffs

                                rowCustomerVisit = rowCustomerVisit & "," & intCustomerVisit
                                rowCallRegistered = rowCallRegistered & "," & intCallRegistered
                                rowRepairCompleted = rowRepairCompleted & "," & intRepairCompleted
                                rowGoodsDelivered = rowGoodsDelivered & "," & intGoodsDelivered
                                rowPending = rowPending & "," & intPending
                                rowWarranty = rowWarranty & "," & intWarranty
                                rowInWarranty = rowInWarranty & "," & intInWarranty
                                rowOutWarranty = rowOutWarranty & "," & intOutWarranty
                                rowInWarrantyAmount = rowInWarrantyAmount & "," & decInWarrantyAmount
                                rowInWarrantyLabour = rowInWarrantyLabour & "," & decInWarrantyLabour
                                rowInWarrantyParts = rowInWarrantyParts & "," & decInWarrantyParts
                                rowInWarrantyTransport = rowInWarrantyTransport & "," & decInWarrantyTransport
                                rowInWarrantyOthers = rowInWarrantyOthers & "," & decInWarrantyOthers
                                rowOutWarrantyAmount = rowOutWarrantyAmount & "," & decOutWarrantyAmount
                                rowOutWarrantyLabour = rowOutWarrantyLabour & "," & decOutWarrantyLabour
                                rowOutWarrantyParts = rowOutWarrantyParts & "," & decOutWarrantyParts
                                rowOutWarrantyTransport = rowOutWarrantyTransport & "," & decOutWarrantyTransport
                                rowOutWarrantyOthers = rowOutWarrantyOthers & "," & decOutWarrantyOthers
                                rowSawDiscountLabour = rowSawDiscountLabour & "," & decSawDiscountLabour
                                rowSawDiscountParts = rowSawDiscountParts & "," & decSawDiscountParts
                                rowOutWarrantyLabourwSawDisc = rowOutWarrantyLabourwSawDisc & "," & decOutWarrantyLabourwSawDisc
                                rowOutWarrantyPartswSawDisc = rowOutWarrantyPartswSawDisc & "," & decOutWarrantyPartswSawDisc
                                rowRevenueWithoutTax = rowRevenueWithoutTax & "," & decRevenueWithoutTax
                                rowIwPartsConsumed = rowIwPartsConsumed & "," & decIwPartsConsumed
                                rowTotalPartsConsumed = rowTotalPartsConsumed & "," & decTotalPartsConsumed
                                rowPrimeCostTotal = rowPrimeCostTotal & "," & decPrimeCostTotal
                                rowGrossProfit = rowGrossProfit & "," & decGrossProfit

                                If decRevenueWithoutTax = 0 Then
                                    FinalPercentage1 = decGrossProfit
                                Else
                                    FinalPercentage1 = comcontrol.Money_Round((decGrossProfit / decRevenueWithoutTax) * 100, 2)
                                End If
                                rowFinalPercentage = rowFinalPercentage & "," & FinalPercentage1 & "%" 'decFinalPercentage / 100


                                'For Adding Final Report 
                                SscTotals(0, 0) = strDate 'Date

                                SscTotals(0, 1) = SscTotals(0, 1) + intNumberOfCounters 'rowNumberOfCounters
                                SscTotals(0, 2) = SscTotals(0, 2) + intNumberOfBusinessDat 'rowNumberOfBusinessDat
                                SscTotals(0, 3) = SscTotals(0, 3) + intNumberOfStaffs 'rowNumberOfStaffs

                                SscTotals(0, 4) = SscTotals(0, 4) + intCustomerVisit 'rowCustomerVisit
                                SscTotals(0, 5) = SscTotals(0, 5) + intCallRegistered 'rowCallRegistered
                                SscTotals(0, 6) = SscTotals(0, 6) + intRepairCompleted 'rowRepairCompleted
                                SscTotals(0, 7) = SscTotals(0, 7) + intGoodsDelivered 'rowGoodsDelivered
                                SscTotals(0, 8) = SscTotals(0, 8) + intPending 'rowPending
                                SscTotals(0, 9) = SscTotals(0, 9) + intWarranty 'rowWarranty
                                SscTotals(0, 10) = SscTotals(0, 10) + intInWarranty 'rowInWarranty
                                SscTotals(0, 11) = SscTotals(0, 11) + intOutWarranty 'rowOutWarranty
                                SscTotals(0, 12) = SscTotals(0, 12) + decInWarrantyAmount 'rowInWarrantyAmount
                                SscTotals(0, 13) = SscTotals(0, 13) + decInWarrantyLabour 'rowInWarrantyLabour
                                SscTotals(0, 14) = SscTotals(0, 14) + decInWarrantyParts 'rowInWarrantyParts
                                SscTotals(0, 15) = SscTotals(0, 15) + decInWarrantyTransport 'rowInWarrantyTransport
                                SscTotals(0, 16) = SscTotals(0, 16) + decInWarrantyOthers 'rowInWarrantyOthers
                                SscTotals(0, 17) = SscTotals(0, 17) + decOutWarrantyAmount 'rowOutWarrantyAmount
                                SscTotals(0, 18) = SscTotals(0, 18) + decOutWarrantyLabour 'rowOutWarrantyLabour
                                SscTotals(0, 19) = SscTotals(0, 19) + decOutWarrantyParts 'rowOutWarrantyParts
                                SscTotals(0, 20) = SscTotals(0, 20) + decOutWarrantyTransport 'rowOutWarrantyTransport
                                SscTotals(0, 21) = SscTotals(0, 21) + decOutWarrantyOthers 'rowOutWarrantyOthers
                                SscTotals(0, 22) = SscTotals(0, 22) + decSawDiscountLabour 'rowSawDiscountLabour
                                SscTotals(0, 23) = SscTotals(0, 23) + decSawDiscountParts 'rowSawDiscountParts
                                SscTotals(0, 24) = SscTotals(0, 24) + decOutWarrantyLabourwSawDisc 'rowOutWarrantyLabourwSawDisc
                                SscTotals(0, 25) = SscTotals(0, 25) + decOutWarrantyPartswSawDisc 'rowOutWarrantyPartswSawDisc
                                SscTotals(0, 26) = SscTotals(0, 26) + decRevenueWithoutTax 'rowRevenueWithoutTax
                                SscTotals(0, 27) = SscTotals(0, 27) + decIwPartsConsumed 'rowIwPartsConsumed
                                SscTotals(0, 28) = SscTotals(0, 28) + decTotalPartsConsumed 'rowTotalPartsConsumed
                                SscTotals(0, 29) = SscTotals(0, 29) + decPrimeCostTotal 'rowPrimeCostTotal
                                SscTotals(0, 30) = SscTotals(0, 30) + decGrossProfit 'rowGrossProfit 
                                SscTotals(0, 31) = SscTotals(0, 31) + decFinalPercentage 'rowFinalPercentage 


                            Next row
                        End If

                        'Monday2Week & Sunday2Week
                        strDay = Monday2Week.Day()
                        strMon = Monday2Week.Month
                        strYear = Monday2Week.Year

                        If Len(strDay) <= 1 Then
                            strDay = "0" & strDay
                        End If
                        If Len(strMon) <= 1 Then
                            strMon = "0" & strMon
                        End If
                        strDate = strYear & strMon & strDay
                        'strWeek2Tot
                        'Read from 
                        _dtPlReprt = comcontrol.SelectPlReport(Monday2Week, Sunday2Week, item.Text, item.Value, Gm)
                        If (_dtPlReprt Is Nothing) Or (_dtPlReprt.Rows.Count = 0) Then
                        Else
                            For Each row As DataRow In _dtPlReprt.Rows

                                intNumberOfCounters = row.Item("NumberOfCounters")
                                intNumberOfBusinessDat = row.Item("NumberOfBusinessDat")
                                intNumberOfStaffs = row.Item("NumberOfStaffs")

                                intCustomerVisit = row.Item("CustomerVisit")
                                intCallRegistered = row.Item("CallRegistered")
                                intRepairCompleted = row.Item("RepairCompleted")
                                intGoodsDelivered = row.Item("GoodsDelivered")
                                intPending = row.Item("Pending")
                                intWarranty = row.Item("Warranty")
                                intInWarranty = row.Item("InWarranty")
                                intOutWarranty = row.Item("OutWarranty")
                                decInWarrantyAmount = row.Item("InWarrantyAmount")
                                decInWarrantyLabour = row.Item("InWarrantyLabour")
                                decInWarrantyParts = row.Item("InWarrantyParts")
                                decInWarrantyTransport = row.Item("InWarrantyTransport")
                                decInWarrantyOthers = row.Item("InWarrantyOthers")
                                decOutWarrantyAmount = row.Item("OutWarrantyAmount")
                                decOutWarrantyLabour = row.Item("OutWarrantyLabour")
                                decOutWarrantyParts = row.Item("OutWarrantyParts")
                                decOutWarrantyTransport = row.Item("OutWarrantyTransport")
                                decOutWarrantyOthers = row.Item("OutWarrantyOthers")
                                decSawDiscountLabour = row.Item("SawDiscountLabour")
                                decSawDiscountParts = row.Item("SawDiscountParts")
                                decOutWarrantyLabourwSawDisc = row.Item("OutWarrantyLabourwSawDisc")
                                decOutWarrantyPartswSawDisc = row.Item("OutWarrantyPartswSawDisc")
                                decRevenueWithoutTax = row.Item("RevenueWithoutTax")
                                decIwPartsConsumed = row.Item("IwPartsConsumed")
                                decTotalPartsConsumed = row.Item("TotalPartsConsumed")
                                decPrimeCostTotal = row.Item("PrimeCostTotal")
                                decGrossProfit = row.Item("GrossProfit")

                                decFinalPercentage = row.Item("FinalPercentage")

                                intNumberOfCountersTot = intNumberOfCountersTot + intNumberOfCounters
                                intNumberOfBusinessDatTot = intNumberOfBusinessDatTot + intNumberOfBusinessDat
                                intNumberOfStaffsTot = intNumberOfStaffsTot + intNumberOfStaffs

                                intCustomerVisitTot = intCustomerVisitTot + intCustomerVisit
                                intCallRegisteredTot = intCallRegisteredTot + intCallRegistered
                                intRepairCompletedTot = intRepairCompletedTot + intRepairCompleted
                                intGoodsDeliveredTot = intGoodsDeliveredTot + intGoodsDelivered
                                intPendingTot = intPendingTot + intPending
                                intWarrantyTot = intWarrantyTot + intWarranty
                                intInWarrantyTot = intInWarrantyTot + intInWarranty
                                intOutWarrantyTot = intOutWarrantyTot + intOutWarranty
                                decInWarrantyAmountTot = decInWarrantyAmountTot + decInWarrantyAmount
                                decInWarrantyLabourTot = decInWarrantyLabourTot + decInWarrantyLabour
                                decInWarrantyPartsTot = decInWarrantyPartsTot + decInWarrantyParts
                                decInWarrantyTransportTot = decInWarrantyTransportTot + decInWarrantyTransport
                                decInWarrantyOthersTot = decInWarrantyOthersTot + decInWarrantyOthers
                                decOutWarrantyAmountTot = decOutWarrantyAmountTot + decOutWarrantyAmount
                                decOutWarrantyLabourTot = decOutWarrantyLabourTot + decOutWarrantyLabour
                                decOutWarrantyPartsTot = decOutWarrantyPartsTot + decOutWarrantyParts
                                decOutWarrantyTransportTot = decOutWarrantyTransportTot + decOutWarrantyTransport
                                decOutWarrantyOthersTot = decOutWarrantyOthersTot + decOutWarrantyOthers
                                decSawDiscountLabourTot = decSawDiscountLabourTot + decSawDiscountLabour
                                decSawDiscountPartsTot = decSawDiscountPartsTot + decSawDiscountParts
                                decOutWarrantyLabourwSawDiscTot = decOutWarrantyLabourwSawDiscTot + decOutWarrantyLabourwSawDisc
                                decOutWarrantyPartswSawDiscTot = decOutWarrantyPartswSawDiscTot + decOutWarrantyPartswSawDisc
                                decRevenueWithoutTaxTot = decRevenueWithoutTaxTot + decRevenueWithoutTax
                                decIwPartsConsumedTot = decIwPartsConsumedTot + decIwPartsConsumed
                                decTotalPartsConsumedTot = decTotalPartsConsumedTot + decTotalPartsConsumed
                                decPrimeCostTotalTot = decPrimeCostTotalTot + decPrimeCostTotal
                                decGrossProfitTot = decGrossProfitTot + decGrossProfit

                                decFinalPercentageTot = decFinalPercentageTot + decFinalPercentage


                                row0 = row0 & "," & item.Text
                                rowTitle = rowTitle & "," & strDate

                                rowNumberOfCounters = rowNumberOfCounters & "," '& intNumberOfCounters
                                rowNumberOfBusinessDat = rowNumberOfBusinessDat & "," '& intNumberOfBusinessDat
                                rowNumberOfStaffs = rowNumberOfStaffs & "," '& intNumberOfStaffs

                                rowCustomerVisit = rowCustomerVisit & "," & intCustomerVisit
                                rowCallRegistered = rowCallRegistered & "," & intCallRegistered
                                rowRepairCompleted = rowRepairCompleted & "," & intRepairCompleted
                                rowGoodsDelivered = rowGoodsDelivered & "," & intGoodsDelivered
                                rowPending = rowPending & "," & intPending
                                rowWarranty = rowWarranty & "," & intWarranty
                                rowInWarranty = rowInWarranty & "," & intInWarranty
                                rowOutWarranty = rowOutWarranty & "," & intOutWarranty
                                rowInWarrantyAmount = rowInWarrantyAmount & "," & decInWarrantyAmount
                                rowInWarrantyLabour = rowInWarrantyLabour & "," & decInWarrantyLabour
                                rowInWarrantyParts = rowInWarrantyParts & "," & decInWarrantyParts
                                rowInWarrantyTransport = rowInWarrantyTransport & "," & decInWarrantyTransport
                                rowInWarrantyOthers = rowInWarrantyOthers & "," & decInWarrantyOthers
                                rowOutWarrantyAmount = rowOutWarrantyAmount & "," & decOutWarrantyAmount
                                rowOutWarrantyLabour = rowOutWarrantyLabour & "," & decOutWarrantyLabour
                                rowOutWarrantyParts = rowOutWarrantyParts & "," & decOutWarrantyParts
                                rowOutWarrantyTransport = rowOutWarrantyTransport & "," & decOutWarrantyTransport
                                rowOutWarrantyOthers = rowOutWarrantyOthers & "," & decOutWarrantyOthers
                                rowSawDiscountLabour = rowSawDiscountLabour & "," & decSawDiscountLabour
                                rowSawDiscountParts = rowSawDiscountParts & "," & decSawDiscountParts
                                rowOutWarrantyLabourwSawDisc = rowOutWarrantyLabourwSawDisc & "," & decOutWarrantyLabourwSawDisc
                                rowOutWarrantyPartswSawDisc = rowOutWarrantyPartswSawDisc & "," & decOutWarrantyPartswSawDisc
                                rowRevenueWithoutTax = rowRevenueWithoutTax & "," & decRevenueWithoutTax
                                rowIwPartsConsumed = rowIwPartsConsumed & "," & decIwPartsConsumed
                                rowTotalPartsConsumed = rowTotalPartsConsumed & "," & decTotalPartsConsumed
                                rowPrimeCostTotal = rowPrimeCostTotal & "," & decPrimeCostTotal
                                rowGrossProfit = rowGrossProfit & "," & decGrossProfit
                                If decRevenueWithoutTax = 0 Then
                                    FinalPercentage1 = decGrossProfit
                                Else
                                    FinalPercentage1 = comcontrol.Money_Round((decGrossProfit / decRevenueWithoutTax) * 100, 2)
                                End If
                                rowFinalPercentage = rowFinalPercentage & "," & FinalPercentage1 & "%" 'decFinalPercentage / 100




                                'For Adding Final Report 
                                SscTotals(1, 0) = strDate 'Date

                                SscTotals(1, 1) = SscTotals(1, 1) + intNumberOfCounters 'rowNumberOfCounters
                                SscTotals(1, 2) = SscTotals(1, 2) + intNumberOfBusinessDat 'rowNumberOfBusinessDat
                                SscTotals(1, 3) = SscTotals(1, 3) + intNumberOfStaffs 'rowNumberOfStaffs

                                SscTotals(1, 4) = SscTotals(1, 4) + intCustomerVisit 'rowCustomerVisit
                                SscTotals(1, 5) = SscTotals(1, 5) + intCallRegistered 'rowCallRegistered
                                SscTotals(1, 6) = SscTotals(1, 6) + intRepairCompleted 'rowRepairCompleted
                                SscTotals(1, 7) = SscTotals(1, 7) + intGoodsDelivered 'rowGoodsDelivered
                                SscTotals(1, 8) = SscTotals(1, 8) + intPending 'rowPending
                                SscTotals(1, 9) = SscTotals(1, 9) + intWarranty 'rowWarranty
                                SscTotals(1, 10) = SscTotals(1, 10) + intInWarranty 'rowInWarranty
                                SscTotals(1, 11) = SscTotals(1, 11) + intOutWarranty 'rowOutWarranty
                                SscTotals(1, 12) = SscTotals(1, 12) + decInWarrantyAmount 'rowInWarrantyAmount
                                SscTotals(1, 13) = SscTotals(1, 13) + decInWarrantyLabour 'rowInWarrantyLabour
                                SscTotals(1, 14) = SscTotals(1, 14) + decInWarrantyParts 'rowInWarrantyParts
                                SscTotals(1, 15) = SscTotals(1, 15) + decInWarrantyTransport 'rowInWarrantyTransport
                                SscTotals(1, 16) = SscTotals(1, 16) + decInWarrantyOthers 'rowInWarrantyOthers
                                SscTotals(1, 17) = SscTotals(1, 17) + decOutWarrantyAmount 'rowOutWarrantyAmount
                                SscTotals(1, 18) = SscTotals(1, 18) + decOutWarrantyLabour 'rowOutWarrantyLabour
                                SscTotals(1, 19) = SscTotals(1, 19) + decOutWarrantyParts 'rowOutWarrantyParts
                                SscTotals(1, 20) = SscTotals(1, 20) + decOutWarrantyTransport 'rowOutWarrantyTransport
                                SscTotals(1, 21) = SscTotals(1, 21) + decOutWarrantyOthers 'rowOutWarrantyOthers
                                SscTotals(1, 22) = SscTotals(1, 22) + decSawDiscountLabour 'rowSawDiscountLabour
                                SscTotals(1, 23) = SscTotals(1, 23) + decSawDiscountParts 'rowSawDiscountParts
                                SscTotals(1, 24) = SscTotals(1, 24) + decOutWarrantyLabourwSawDisc 'rowOutWarrantyLabourwSawDisc
                                SscTotals(1, 25) = SscTotals(1, 25) + decOutWarrantyPartswSawDisc 'rowOutWarrantyPartswSawDisc
                                SscTotals(1, 26) = SscTotals(1, 26) + decRevenueWithoutTax 'rowRevenueWithoutTax
                                SscTotals(1, 27) = SscTotals(1, 27) + decIwPartsConsumed 'rowIwPartsConsumed
                                SscTotals(1, 28) = SscTotals(1, 28) + decTotalPartsConsumed 'rowTotalPartsConsumed
                                SscTotals(1, 29) = SscTotals(1, 29) + decPrimeCostTotal 'rowPrimeCostTotal
                                SscTotals(1, 30) = SscTotals(1, 30) + decGrossProfit 'rowGrossProfit
                                SscTotals(1, 31) = SscTotals(1, 31) + decFinalPercentage 'rowFinalPercentage


                            Next row
                        End If


                        'Monday3Week & Sunday3Week
                        strDay = Monday3Week.Day()
                        strMon = Monday3Week.Month
                        strYear = Monday3Week.Year

                        If Len(strDay) <= 1 Then
                            strDay = "0" & strDay
                        End If
                        If Len(strMon) <= 1 Then
                            strMon = "0" & strMon
                        End If
                        strDate = strYear & strMon & strDay
                        'Read from 
                        _dtPlReprt = comcontrol.SelectPlReport(Monday3Week, Sunday3Week, item.Text, item.Value, Gm)

                        If (_dtPlReprt Is Nothing) Or (_dtPlReprt.Rows.Count = 0) Then
                        Else
                            For Each row As DataRow In _dtPlReprt.Rows

                                intNumberOfCounters = row.Item("NumberOfCounters")
                                intNumberOfBusinessDat = row.Item("NumberOfBusinessDat")
                                intNumberOfStaffs = row.Item("NumberOfStaffs")

                                intCustomerVisit = row.Item("CustomerVisit")
                                intCallRegistered = row.Item("CallRegistered")
                                intRepairCompleted = row.Item("RepairCompleted")
                                intGoodsDelivered = row.Item("GoodsDelivered")
                                intPending = row.Item("Pending")
                                intWarranty = row.Item("Warranty")
                                intInWarranty = row.Item("InWarranty")
                                intOutWarranty = row.Item("OutWarranty")
                                decInWarrantyAmount = row.Item("InWarrantyAmount")
                                decInWarrantyLabour = row.Item("InWarrantyLabour")
                                decInWarrantyParts = row.Item("InWarrantyParts")
                                decInWarrantyTransport = row.Item("InWarrantyTransport")
                                decInWarrantyOthers = row.Item("InWarrantyOthers")
                                decOutWarrantyAmount = row.Item("OutWarrantyAmount")
                                decOutWarrantyLabour = row.Item("OutWarrantyLabour")
                                decOutWarrantyParts = row.Item("OutWarrantyParts")
                                decOutWarrantyTransport = row.Item("OutWarrantyTransport")
                                decOutWarrantyOthers = row.Item("OutWarrantyOthers")
                                decSawDiscountLabour = row.Item("SawDiscountLabour")
                                decSawDiscountParts = row.Item("SawDiscountParts")
                                decOutWarrantyLabourwSawDisc = row.Item("OutWarrantyLabourwSawDisc")
                                decOutWarrantyPartswSawDisc = row.Item("OutWarrantyPartswSawDisc")
                                decRevenueWithoutTax = row.Item("RevenueWithoutTax")
                                decIwPartsConsumed = row.Item("IwPartsConsumed")
                                decTotalPartsConsumed = row.Item("TotalPartsConsumed")
                                decPrimeCostTotal = row.Item("PrimeCostTotal")
                                decGrossProfit = row.Item("GrossProfit")

                                decFinalPercentage = row.Item("FinalPercentage")

                                intNumberOfCountersTot = intNumberOfCountersTot + intNumberOfCounters
                                intNumberOfBusinessDatTot = intNumberOfBusinessDatTot + intNumberOfBusinessDat
                                intNumberOfStaffsTot = intNumberOfStaffsTot + intNumberOfStaffs

                                intCustomerVisitTot = intCustomerVisitTot + intCustomerVisit
                                intCallRegisteredTot = intCallRegisteredTot + intCallRegistered
                                intRepairCompletedTot = intRepairCompletedTot + intRepairCompleted
                                intGoodsDeliveredTot = intGoodsDeliveredTot + intGoodsDelivered
                                intPendingTot = intPendingTot + intPending
                                intWarrantyTot = intWarrantyTot + intWarranty
                                intInWarrantyTot = intInWarrantyTot + intInWarranty
                                intOutWarrantyTot = intOutWarrantyTot + intOutWarranty
                                decInWarrantyAmountTot = decInWarrantyAmountTot + decInWarrantyAmount
                                decInWarrantyLabourTot = decInWarrantyLabourTot + decInWarrantyLabour
                                decInWarrantyPartsTot = decInWarrantyPartsTot + decInWarrantyParts
                                decInWarrantyTransportTot = decInWarrantyTransportTot + decInWarrantyTransport
                                decInWarrantyOthersTot = decInWarrantyOthersTot + decInWarrantyOthers
                                decOutWarrantyAmountTot = decOutWarrantyAmountTot + decOutWarrantyAmount
                                decOutWarrantyLabourTot = decOutWarrantyLabourTot + decOutWarrantyLabour
                                decOutWarrantyPartsTot = decOutWarrantyPartsTot + decOutWarrantyParts
                                decOutWarrantyTransportTot = decOutWarrantyTransportTot + decOutWarrantyTransport
                                decOutWarrantyOthersTot = decOutWarrantyOthersTot + decOutWarrantyOthers
                                decSawDiscountLabourTot = decSawDiscountLabourTot + decSawDiscountLabour
                                decSawDiscountPartsTot = decSawDiscountPartsTot + decSawDiscountParts
                                decOutWarrantyLabourwSawDiscTot = decOutWarrantyLabourwSawDiscTot + decOutWarrantyLabourwSawDisc
                                decOutWarrantyPartswSawDiscTot = decOutWarrantyPartswSawDiscTot + decOutWarrantyPartswSawDisc
                                decRevenueWithoutTaxTot = decRevenueWithoutTaxTot + decRevenueWithoutTax
                                decIwPartsConsumedTot = decIwPartsConsumedTot + decIwPartsConsumed
                                decTotalPartsConsumedTot = decTotalPartsConsumedTot + decTotalPartsConsumed
                                decPrimeCostTotalTot = decPrimeCostTotalTot + decPrimeCostTotal
                                decGrossProfitTot = decGrossProfitTot + decGrossProfit
                                decFinalPercentageTot = decFinalPercentageTot + decFinalPercentage


                                row0 = row0 & "," & item.Text
                                rowTitle = rowTitle & "," & strDate

                                rowNumberOfCounters = rowNumberOfCounters & "," '& intNumberOfCounters
                                rowNumberOfBusinessDat = rowNumberOfBusinessDat & "," '& intNumberOfBusinessDat
                                rowNumberOfStaffs = rowNumberOfStaffs & "," '& intNumberOfStaffs

                                rowCustomerVisit = rowCustomerVisit & "," & intCustomerVisit
                                rowCallRegistered = rowCallRegistered & "," & intCallRegistered
                                rowRepairCompleted = rowRepairCompleted & "," & intRepairCompleted
                                rowGoodsDelivered = rowGoodsDelivered & "," & intGoodsDelivered
                                rowPending = rowPending & "," & intPending
                                rowWarranty = rowWarranty & "," & intWarranty
                                rowInWarranty = rowInWarranty & "," & intInWarranty
                                rowOutWarranty = rowOutWarranty & "," & intOutWarranty
                                rowInWarrantyAmount = rowInWarrantyAmount & "," & decInWarrantyAmount
                                rowInWarrantyLabour = rowInWarrantyLabour & "," & decInWarrantyLabour
                                rowInWarrantyParts = rowInWarrantyParts & "," & decInWarrantyParts
                                rowInWarrantyTransport = rowInWarrantyTransport & "," & decInWarrantyTransport
                                rowInWarrantyOthers = rowInWarrantyOthers & "," & decInWarrantyOthers
                                rowOutWarrantyAmount = rowOutWarrantyAmount & "," & decOutWarrantyAmount
                                rowOutWarrantyLabour = rowOutWarrantyLabour & "," & decOutWarrantyLabour
                                rowOutWarrantyParts = rowOutWarrantyParts & "," & decOutWarrantyParts
                                rowOutWarrantyTransport = rowOutWarrantyTransport & "," & decOutWarrantyTransport
                                rowOutWarrantyOthers = rowOutWarrantyOthers & "," & decOutWarrantyOthers
                                rowSawDiscountLabour = rowSawDiscountLabour & "," & decSawDiscountLabour
                                rowSawDiscountParts = rowSawDiscountParts & "," & decSawDiscountParts
                                rowOutWarrantyLabourwSawDisc = rowOutWarrantyLabourwSawDisc & "," & decOutWarrantyLabourwSawDisc
                                rowOutWarrantyPartswSawDisc = rowOutWarrantyPartswSawDisc & "," & decOutWarrantyPartswSawDisc
                                rowRevenueWithoutTax = rowRevenueWithoutTax & "," & decRevenueWithoutTax
                                rowIwPartsConsumed = rowIwPartsConsumed & "," & decIwPartsConsumed
                                rowTotalPartsConsumed = rowTotalPartsConsumed & "," & decTotalPartsConsumed
                                rowPrimeCostTotal = rowPrimeCostTotal & "," & decPrimeCostTotal
                                rowGrossProfit = rowGrossProfit & "," & decGrossProfit
                                If decRevenueWithoutTax = 0 Then
                                    FinalPercentage1 = decGrossProfit
                                Else
                                    FinalPercentage1 = comcontrol.Money_Round((decGrossProfit / decRevenueWithoutTax) * 100, 2)
                                End If
                                rowFinalPercentage = rowFinalPercentage & "," & FinalPercentage1 & "%" 'decFinalPercentage / 100



                                'For Adding Final Report 
                                SscTotals(2, 0) = strDate 'Date

                                SscTotals(2, 1) = SscTotals(2, 1) + intNumberOfCounters 'rowNumberOfCounters
                                SscTotals(2, 2) = SscTotals(2, 2) + intNumberOfBusinessDat 'rowNumberOfBusinessDat
                                SscTotals(2, 3) = SscTotals(2, 3) + intNumberOfStaffs 'rowNumberOfStaffs

                                SscTotals(2, 4) = SscTotals(2, 4) + intCustomerVisit 'rowCustomerVisit
                                SscTotals(2, 5) = SscTotals(2, 5) + intCallRegistered 'rowCallRegistered
                                SscTotals(2, 6) = SscTotals(2, 6) + intRepairCompleted 'rowRepairCompleted
                                SscTotals(2, 7) = SscTotals(2, 7) + intGoodsDelivered 'rowGoodsDelivered
                                SscTotals(2, 8) = SscTotals(2, 8) + intPending 'rowPending
                                SscTotals(2, 9) = SscTotals(2, 9) + intWarranty 'rowWarranty
                                SscTotals(2, 10) = SscTotals(2, 10) + intInWarranty 'rowInWarranty
                                SscTotals(2, 11) = SscTotals(2, 11) + intOutWarranty 'rowOutWarranty
                                SscTotals(2, 12) = SscTotals(2, 12) + decInWarrantyAmount 'rowInWarrantyAmount
                                SscTotals(2, 13) = SscTotals(2, 13) + decInWarrantyLabour 'rowInWarrantyLabour
                                SscTotals(2, 14) = SscTotals(2, 14) + decInWarrantyParts 'rowInWarrantyParts
                                SscTotals(2, 15) = SscTotals(2, 15) + decInWarrantyTransport 'rowInWarrantyTransport
                                SscTotals(2, 16) = SscTotals(2, 16) + decInWarrantyOthers 'rowInWarrantyOthers
                                SscTotals(2, 17) = SscTotals(2, 17) + decOutWarrantyAmount 'rowOutWarrantyAmount
                                SscTotals(2, 18) = SscTotals(2, 18) + decOutWarrantyLabour 'rowOutWarrantyLabour
                                SscTotals(2, 19) = SscTotals(2, 19) + decOutWarrantyParts 'rowOutWarrantyParts
                                SscTotals(2, 20) = SscTotals(2, 20) + decOutWarrantyTransport 'rowOutWarrantyTransport
                                SscTotals(2, 21) = SscTotals(2, 21) + decOutWarrantyOthers 'rowOutWarrantyOthers
                                SscTotals(2, 22) = SscTotals(2, 22) + decSawDiscountLabour 'rowSawDiscountLabour
                                SscTotals(2, 23) = SscTotals(2, 23) + decSawDiscountParts 'rowSawDiscountParts
                                SscTotals(2, 24) = SscTotals(2, 24) + decOutWarrantyLabourwSawDisc 'rowOutWarrantyLabourwSawDisc
                                SscTotals(2, 25) = SscTotals(2, 25) + decOutWarrantyPartswSawDisc 'rowOutWarrantyPartswSawDisc
                                SscTotals(2, 26) = SscTotals(2, 26) + decRevenueWithoutTax 'rowRevenueWithoutTax
                                SscTotals(2, 27) = SscTotals(2, 27) + decIwPartsConsumed 'rowIwPartsConsumed
                                SscTotals(2, 28) = SscTotals(2, 28) + decTotalPartsConsumed 'rowTotalPartsConsumed
                                SscTotals(2, 29) = SscTotals(2, 29) + decPrimeCostTotal 'rowPrimeCostTotal
                                SscTotals(2, 30) = SscTotals(2, 30) + decGrossProfit 'rowGrossProfit 
                                SscTotals(2, 31) = SscTotals(2, 31) + decFinalPercentage 'rowFinalPercentage

                            Next row
                        End If


                        'Monday4Week & Sunday4Week
                        strDay = Monday4Week.Day()
                        strMon = Monday4Week.Month
                        strYear = Monday4Week.Year

                        If Len(strDay) <= 1 Then
                            strDay = "0" & strDay
                        End If
                        If Len(strMon) <= 1 Then
                            strMon = "0" & strMon
                        End If
                        strDate = strYear & strMon & strDay
                        'Read from 
                        _dtPlReprt = comcontrol.SelectPlReport(Monday4Week, Sunday4Week, item.Text, item.Value, Gm)
                        If (_dtPlReprt Is Nothing) Or (_dtPlReprt.Rows.Count = 0) Then
                        Else
                            For Each row As DataRow In _dtPlReprt.Rows

                                intNumberOfCounters = row.Item("NumberOfCounters")
                                intNumberOfBusinessDat = row.Item("NumberOfBusinessDat")
                                intNumberOfStaffs = row.Item("NumberOfStaffs")

                                intCustomerVisit = row.Item("CustomerVisit")
                                intCallRegistered = row.Item("CallRegistered")
                                intRepairCompleted = row.Item("RepairCompleted")
                                intGoodsDelivered = row.Item("GoodsDelivered")
                                intPending = row.Item("Pending")
                                intWarranty = row.Item("Warranty")
                                intInWarranty = row.Item("InWarranty")
                                intOutWarranty = row.Item("OutWarranty")
                                decInWarrantyAmount = row.Item("InWarrantyAmount")
                                decInWarrantyLabour = row.Item("InWarrantyLabour")
                                decInWarrantyParts = row.Item("InWarrantyParts")
                                decInWarrantyTransport = row.Item("InWarrantyTransport")
                                decInWarrantyOthers = row.Item("InWarrantyOthers")
                                decOutWarrantyAmount = row.Item("OutWarrantyAmount")
                                decOutWarrantyLabour = row.Item("OutWarrantyLabour")
                                decOutWarrantyParts = row.Item("OutWarrantyParts")
                                decOutWarrantyTransport = row.Item("OutWarrantyTransport")
                                decOutWarrantyOthers = row.Item("OutWarrantyOthers")
                                decSawDiscountLabour = row.Item("SawDiscountLabour")
                                decSawDiscountParts = row.Item("SawDiscountParts")
                                decOutWarrantyLabourwSawDisc = row.Item("OutWarrantyLabourwSawDisc")
                                decOutWarrantyPartswSawDisc = row.Item("OutWarrantyPartswSawDisc")
                                decRevenueWithoutTax = row.Item("RevenueWithoutTax")
                                decIwPartsConsumed = row.Item("IwPartsConsumed")
                                decTotalPartsConsumed = row.Item("TotalPartsConsumed")
                                decPrimeCostTotal = row.Item("PrimeCostTotal")
                                decGrossProfit = row.Item("GrossProfit")
                                decFinalPercentage = row.Item("FinalPercentage")

                                intNumberOfCountersTot = intNumberOfCountersTot + intNumberOfCounters
                                intNumberOfBusinessDatTot = intNumberOfBusinessDatTot + intNumberOfBusinessDat
                                intNumberOfStaffsTot = intNumberOfStaffsTot + intNumberOfStaffs

                                intCustomerVisitTot = intCustomerVisitTot + intCustomerVisit
                                intCallRegisteredTot = intCallRegisteredTot + intCallRegistered
                                intRepairCompletedTot = intRepairCompletedTot + intRepairCompleted
                                intGoodsDeliveredTot = intGoodsDeliveredTot + intGoodsDelivered
                                intPendingTot = intPendingTot + intPending
                                intWarrantyTot = intWarrantyTot + intWarranty
                                intInWarrantyTot = intInWarrantyTot + intInWarranty
                                intOutWarrantyTot = intOutWarrantyTot + intOutWarranty
                                decInWarrantyAmountTot = decInWarrantyAmountTot + decInWarrantyAmount
                                decInWarrantyLabourTot = decInWarrantyLabourTot + decInWarrantyLabour
                                decInWarrantyPartsTot = decInWarrantyPartsTot + decInWarrantyParts
                                decInWarrantyTransportTot = decInWarrantyTransportTot + decInWarrantyTransport
                                decInWarrantyOthersTot = decInWarrantyOthersTot + decInWarrantyOthers
                                decOutWarrantyAmountTot = decOutWarrantyAmountTot + decOutWarrantyAmount
                                decOutWarrantyLabourTot = decOutWarrantyLabourTot + decOutWarrantyLabour
                                decOutWarrantyPartsTot = decOutWarrantyPartsTot + decOutWarrantyParts
                                decOutWarrantyTransportTot = decOutWarrantyTransportTot + decOutWarrantyTransport
                                decOutWarrantyOthersTot = decOutWarrantyOthersTot + decOutWarrantyOthers
                                decSawDiscountLabourTot = decSawDiscountLabourTot + decSawDiscountLabour
                                decSawDiscountPartsTot = decSawDiscountPartsTot + decSawDiscountParts
                                decOutWarrantyLabourwSawDiscTot = decOutWarrantyLabourwSawDiscTot + decOutWarrantyLabourwSawDisc
                                decOutWarrantyPartswSawDiscTot = decOutWarrantyPartswSawDiscTot + decOutWarrantyPartswSawDisc
                                decRevenueWithoutTaxTot = decRevenueWithoutTaxTot + decRevenueWithoutTax
                                decIwPartsConsumedTot = decIwPartsConsumedTot + decIwPartsConsumed
                                decTotalPartsConsumedTot = decTotalPartsConsumedTot + decTotalPartsConsumed
                                decPrimeCostTotalTot = decPrimeCostTotalTot + decPrimeCostTotal
                                decGrossProfitTot = decGrossProfitTot + decGrossProfit
                                decFinalPercentageTot = decFinalPercentageTot + decFinalPercentage


                                row0 = row0 & "," & item.Text
                                rowTitle = rowTitle & "," & strDate

                                rowNumberOfCounters = rowNumberOfCounters & "," '& intNumberOfCounters
                                rowNumberOfBusinessDat = rowNumberOfBusinessDat & "," '& intNumberOfBusinessDat
                                rowNumberOfStaffs = rowNumberOfStaffs & "," '& intNumberOfStaffs

                                rowCustomerVisit = rowCustomerVisit & "," & intCustomerVisit
                                rowCallRegistered = rowCallRegistered & "," & intCallRegistered
                                rowRepairCompleted = rowRepairCompleted & "," & intRepairCompleted
                                rowGoodsDelivered = rowGoodsDelivered & "," & intGoodsDelivered
                                rowPending = rowPending & "," & intPending
                                rowWarranty = rowWarranty & "," & intWarranty
                                rowInWarranty = rowInWarranty & "," & intInWarranty
                                rowOutWarranty = rowOutWarranty & "," & intOutWarranty
                                rowInWarrantyAmount = rowInWarrantyAmount & "," & decInWarrantyAmount
                                rowInWarrantyLabour = rowInWarrantyLabour & "," & decInWarrantyLabour
                                rowInWarrantyParts = rowInWarrantyParts & "," & decInWarrantyParts
                                rowInWarrantyTransport = rowInWarrantyTransport & "," & decInWarrantyTransport
                                rowInWarrantyOthers = rowInWarrantyOthers & "," & decInWarrantyOthers
                                rowOutWarrantyAmount = rowOutWarrantyAmount & "," & decOutWarrantyAmount
                                rowOutWarrantyLabour = rowOutWarrantyLabour & "," & decOutWarrantyLabour
                                rowOutWarrantyParts = rowOutWarrantyParts & "," & decOutWarrantyParts
                                rowOutWarrantyTransport = rowOutWarrantyTransport & "," & decOutWarrantyTransport
                                rowOutWarrantyOthers = rowOutWarrantyOthers & "," & decOutWarrantyOthers
                                rowSawDiscountLabour = rowSawDiscountLabour & "," & decSawDiscountLabour
                                rowSawDiscountParts = rowSawDiscountParts & "," & decSawDiscountParts
                                rowOutWarrantyLabourwSawDisc = rowOutWarrantyLabourwSawDisc & "," & decOutWarrantyLabourwSawDisc
                                rowOutWarrantyPartswSawDisc = rowOutWarrantyPartswSawDisc & "," & decOutWarrantyPartswSawDisc
                                rowRevenueWithoutTax = rowRevenueWithoutTax & "," & decRevenueWithoutTax
                                rowIwPartsConsumed = rowIwPartsConsumed & "," & decIwPartsConsumed
                                rowTotalPartsConsumed = rowTotalPartsConsumed & "," & decTotalPartsConsumed
                                rowPrimeCostTotal = rowPrimeCostTotal & "," & decPrimeCostTotal
                                rowGrossProfit = rowGrossProfit & "," & decGrossProfit
                                'rowFinalPercentage = rowFinalPercentage & "," & decFinalPercentage & "%"
                                If decRevenueWithoutTax = 0 Then
                                    FinalPercentage1 = decGrossProfit
                                Else
                                    FinalPercentage1 = comcontrol.Money_Round((decGrossProfit / decRevenueWithoutTax) * 100, 2)
                                End If
                                rowFinalPercentage = rowFinalPercentage & "," & FinalPercentage1 & "%" 'decFinalPercentage / 100



                                'For Adding Final Report 
                                SscTotals(3, 0) = strDate 'Date

                                SscTotals(3, 1) = SscTotals(3, 1) + intNumberOfCounters 'rowNumberOfCounters
                                SscTotals(3, 2) = SscTotals(3, 2) + intNumberOfBusinessDat 'rowNumberOfBusinessDat
                                SscTotals(3, 3) = SscTotals(3, 3) + intNumberOfStaffs 'rowNumberOfStaffs

                                SscTotals(3, 4) = SscTotals(3, 4) + intCustomerVisit 'rowCustomerVisit
                                SscTotals(3, 5) = SscTotals(3, 5) + intCallRegistered 'rowCallRegistered
                                SscTotals(3, 6) = SscTotals(3, 6) + intRepairCompleted 'rowRepairCompleted
                                SscTotals(3, 7) = SscTotals(3, 7) + intGoodsDelivered 'rowGoodsDelivered
                                SscTotals(3, 8) = SscTotals(3, 8) + intPending 'rowPending
                                SscTotals(3, 9) = SscTotals(3, 9) + intWarranty 'rowWarranty
                                SscTotals(3, 10) = SscTotals(3, 10) + intInWarranty 'rowInWarranty
                                SscTotals(3, 11) = SscTotals(3, 11) + intOutWarranty 'rowOutWarranty
                                SscTotals(3, 12) = SscTotals(3, 12) + decInWarrantyAmount 'rowInWarrantyAmount
                                SscTotals(3, 13) = SscTotals(3, 13) + decInWarrantyLabour 'rowInWarrantyLabour
                                SscTotals(3, 14) = SscTotals(3, 14) + decInWarrantyParts 'rowInWarrantyParts
                                SscTotals(3, 15) = SscTotals(3, 15) + decInWarrantyTransport 'rowInWarrantyTransport
                                SscTotals(3, 16) = SscTotals(3, 16) + decInWarrantyOthers 'rowInWarrantyOthers
                                SscTotals(3, 17) = SscTotals(3, 17) + decOutWarrantyAmount 'rowOutWarrantyAmount
                                SscTotals(3, 18) = SscTotals(3, 18) + decOutWarrantyLabour 'rowOutWarrantyLabour
                                SscTotals(3, 19) = SscTotals(3, 19) + decOutWarrantyParts 'rowOutWarrantyParts
                                SscTotals(3, 20) = SscTotals(3, 20) + decOutWarrantyTransport 'rowOutWarrantyTransport
                                SscTotals(3, 21) = SscTotals(3, 21) + decOutWarrantyOthers 'rowOutWarrantyOthers
                                SscTotals(3, 22) = SscTotals(3, 22) + decSawDiscountLabour 'rowSawDiscountLabour
                                SscTotals(3, 23) = SscTotals(3, 23) + decSawDiscountParts 'rowSawDiscountParts
                                SscTotals(3, 24) = SscTotals(3, 24) + decOutWarrantyLabourwSawDisc 'rowOutWarrantyLabourwSawDisc
                                SscTotals(3, 25) = SscTotals(3, 25) + decOutWarrantyPartswSawDisc 'rowOutWarrantyPartswSawDisc
                                SscTotals(3, 26) = SscTotals(3, 26) + decRevenueWithoutTax 'rowRevenueWithoutTax
                                SscTotals(3, 27) = SscTotals(3, 27) + decIwPartsConsumed 'rowIwPartsConsumed
                                SscTotals(3, 28) = SscTotals(3, 28) + decTotalPartsConsumed 'rowTotalPartsConsumed
                                SscTotals(3, 29) = SscTotals(3, 29) + decPrimeCostTotal 'rowPrimeCostTotal
                                SscTotals(3, 30) = SscTotals(3, 30) + decGrossProfit 'rowGrossProfit 
                                SscTotals(3, 31) = SscTotals(3, 31) + decFinalPercentage 'rowFinalPercentage


                            Next row
                        End If

                    End If


                Else
                    'lblLoc.Text = lblLoc.Text & "<br>" & DropListLocation.SelectedItem.Text

                    'Monday1Week & Sunday1Week
                    strDay = Monday1Week.Day()
                    strMon = Monday1Week.Month
                    strYear = Monday1Week.Year

                    If Len(strDay) <= 1 Then
                        strDay = "0" & strDay
                    End If
                    If Len(strMon) <= 1 Then
                        strMon = "0" & strMon
                    End If
                    strDate = strYear & strMon & strDay
                    'Read from 
                    _dtPlReprt = comcontrol.SelectPlReport(Monday1Week, Sunday1Week, DropListLocation.SelectedItem.Text, DropListLocation.SelectedItem.Value, Gm)

                    If (_dtPlReprt Is Nothing) Or (_dtPlReprt.Rows.Count = 0) Then
                    Else
                        For Each row As DataRow In _dtPlReprt.Rows

                            intNumberOfCounters = row.Item("NumberOfCounters")
                            intNumberOfBusinessDat = row.Item("NumberOfBusinessDat")
                            intNumberOfStaffs = row.Item("NumberOfStaffs")

                            intCustomerVisit = row.Item("CustomerVisit")
                            intCallRegistered = row.Item("CallRegistered")
                            intRepairCompleted = row.Item("RepairCompleted")
                            intGoodsDelivered = row.Item("GoodsDelivered")
                            intPending = row.Item("Pending")
                            intWarranty = row.Item("Warranty")
                            intInWarranty = row.Item("InWarranty")
                            intOutWarranty = row.Item("OutWarranty")
                            decInWarrantyAmount = row.Item("InWarrantyAmount")
                            decInWarrantyLabour = row.Item("InWarrantyLabour")
                            decInWarrantyParts = row.Item("InWarrantyParts")
                            decInWarrantyTransport = row.Item("InWarrantyTransport")
                            decInWarrantyOthers = row.Item("InWarrantyOthers")
                            decOutWarrantyAmount = row.Item("OutWarrantyAmount")
                            decOutWarrantyLabour = row.Item("OutWarrantyLabour")
                            decOutWarrantyParts = row.Item("OutWarrantyParts")
                            decOutWarrantyTransport = row.Item("OutWarrantyTransport")
                            decOutWarrantyOthers = row.Item("OutWarrantyOthers")
                            decSawDiscountLabour = row.Item("SawDiscountLabour")
                            decSawDiscountParts = row.Item("SawDiscountParts")
                            decOutWarrantyLabourwSawDisc = row.Item("OutWarrantyLabourwSawDisc")
                            decOutWarrantyPartswSawDisc = row.Item("OutWarrantyPartswSawDisc")
                            decRevenueWithoutTax = row.Item("RevenueWithoutTax")
                            decIwPartsConsumed = row.Item("IwPartsConsumed")
                            decTotalPartsConsumed = row.Item("TotalPartsConsumed")
                            decPrimeCostTotal = row.Item("PrimeCostTotal")
                            decGrossProfit = row.Item("GrossProfit")
                            decFinalPercentage = row.Item("FinalPercentage")

                            intNumberOfCountersTot = intNumberOfCountersTot + intNumberOfCounters
                            intNumberOfBusinessDatTot = intNumberOfBusinessDatTot + intNumberOfBusinessDat
                            intNumberOfStaffsTot = intNumberOfStaffsTot + intNumberOfStaffs

                            intCustomerVisitTot = intCustomerVisitTot + intCustomerVisit
                            intCallRegisteredTot = intCallRegisteredTot + intCallRegistered
                            intRepairCompletedTot = intRepairCompletedTot + intRepairCompleted
                            intGoodsDeliveredTot = intGoodsDeliveredTot + intGoodsDelivered
                            intPendingTot = intPendingTot + intPending
                            intWarrantyTot = intWarrantyTot + intWarranty
                            intInWarrantyTot = intInWarrantyTot + intInWarranty
                            intOutWarrantyTot = intOutWarrantyTot + intOutWarranty
                            decInWarrantyAmountTot = decInWarrantyAmountTot + decInWarrantyAmount
                            decInWarrantyLabourTot = decInWarrantyLabourTot + decInWarrantyLabour
                            decInWarrantyPartsTot = decInWarrantyPartsTot + decInWarrantyParts
                            decInWarrantyTransportTot = decInWarrantyTransportTot + decInWarrantyTransport
                            decInWarrantyOthersTot = decInWarrantyOthersTot + decInWarrantyOthers
                            decOutWarrantyAmountTot = decOutWarrantyAmountTot + decOutWarrantyAmount
                            decOutWarrantyLabourTot = decOutWarrantyLabourTot + decOutWarrantyLabour
                            decOutWarrantyPartsTot = decOutWarrantyPartsTot + decOutWarrantyParts
                            decOutWarrantyTransportTot = decOutWarrantyTransportTot + decOutWarrantyTransport
                            decOutWarrantyOthersTot = decOutWarrantyOthersTot + decOutWarrantyOthers
                            decSawDiscountLabourTot = decSawDiscountLabourTot + decSawDiscountLabour
                            decSawDiscountPartsTot = decSawDiscountPartsTot + decSawDiscountParts
                            decOutWarrantyLabourwSawDiscTot = decOutWarrantyLabourwSawDiscTot + decOutWarrantyLabourwSawDisc
                            decOutWarrantyPartswSawDiscTot = decOutWarrantyPartswSawDiscTot + decOutWarrantyPartswSawDisc
                            decRevenueWithoutTaxTot = decRevenueWithoutTaxTot + decRevenueWithoutTax
                            decIwPartsConsumedTot = decIwPartsConsumedTot + decIwPartsConsumed
                            decTotalPartsConsumedTot = decTotalPartsConsumedTot + decTotalPartsConsumed
                            decPrimeCostTotalTot = decPrimeCostTotalTot + decPrimeCostTotal
                            decGrossProfitTot = decGrossProfitTot + decGrossProfit

                            decFinalPercentageTot = decFinalPercentageTot + decFinalPercentage

                            rowTitle = rowTitle & "," & strDate

                            rowNumberOfCounters = rowNumberOfCounters & "," '& intNumberOfCounters
                            rowNumberOfBusinessDat = rowNumberOfBusinessDat & "," '& intNumberOfBusinessDat
                            rowNumberOfStaffs = rowNumberOfStaffs & "," '& intNumberOfStaffs

                            rowCustomerVisit = rowCustomerVisit & "," & intCustomerVisit
                            rowCallRegistered = rowCallRegistered & "," & intCallRegistered
                            rowRepairCompleted = rowRepairCompleted & "," & intRepairCompleted
                            rowGoodsDelivered = rowGoodsDelivered & "," & intGoodsDelivered
                            rowPending = rowPending & "," & intPending
                            rowWarranty = rowWarranty & "," & intWarranty
                            rowInWarranty = rowInWarranty & "," & intInWarranty
                            rowOutWarranty = rowOutWarranty & "," & intOutWarranty
                            rowInWarrantyAmount = rowInWarrantyAmount & "," & decInWarrantyAmount
                            rowInWarrantyLabour = rowInWarrantyLabour & "," & decInWarrantyLabour
                            rowInWarrantyParts = rowInWarrantyParts & "," & decInWarrantyParts
                            rowInWarrantyTransport = rowInWarrantyTransport & "," & decInWarrantyTransport
                            rowInWarrantyOthers = rowInWarrantyOthers & "," & decInWarrantyOthers
                            rowOutWarrantyAmount = rowOutWarrantyAmount & "," & decOutWarrantyAmount
                            rowOutWarrantyLabour = rowOutWarrantyLabour & "," & decOutWarrantyLabour
                            rowOutWarrantyParts = rowOutWarrantyParts & "," & decOutWarrantyParts
                            rowOutWarrantyTransport = rowOutWarrantyTransport & "," & decOutWarrantyTransport
                            rowOutWarrantyOthers = rowOutWarrantyOthers & "," & decOutWarrantyOthers
                            rowSawDiscountLabour = rowSawDiscountLabour & "," & decSawDiscountLabour
                            rowSawDiscountParts = rowSawDiscountParts & "," & decSawDiscountParts
                            rowOutWarrantyLabourwSawDisc = rowOutWarrantyLabourwSawDisc & "," & decOutWarrantyLabourwSawDisc
                            rowOutWarrantyPartswSawDisc = rowOutWarrantyPartswSawDisc & "," & decOutWarrantyPartswSawDisc
                            rowRevenueWithoutTax = rowRevenueWithoutTax & "," & decRevenueWithoutTax
                            rowIwPartsConsumed = rowIwPartsConsumed & "," & decIwPartsConsumed
                            rowTotalPartsConsumed = rowTotalPartsConsumed & "," & decTotalPartsConsumed
                            rowPrimeCostTotal = rowPrimeCostTotal & "," & decPrimeCostTotal
                            rowGrossProfit = rowGrossProfit & "," & decGrossProfit
                            If decRevenueWithoutTax = 0 Then
                                FinalPercentage1 = decGrossProfit
                            Else
                                FinalPercentage1 = comcontrol.Money_Round((decGrossProfit / decRevenueWithoutTax) * 100, 2)
                            End If
                            rowFinalPercentage = rowFinalPercentage & "," & FinalPercentage1 & "%" 'decFinalPercentage / 100

                        Next row
                    End If

                    'Monday2Week & Sunday2Week
                    strDay = Monday2Week.Day()
                    strMon = Monday2Week.Month
                    strYear = Monday2Week.Year

                    If Len(strDay) <= 1 Then
                        strDay = "0" & strDay
                    End If
                    If Len(strMon) <= 1 Then
                        strMon = "0" & strMon
                    End If
                    strDate = strYear & strMon & strDay
                    'Read from 
                    _dtPlReprt = comcontrol.SelectPlReport(Monday2Week, Sunday2Week, DropListLocation.SelectedItem.Text, DropListLocation.SelectedItem.Value, Gm)
                    If (_dtPlReprt Is Nothing) Or (_dtPlReprt.Rows.Count = 0) Then
                    Else
                        For Each row As DataRow In _dtPlReprt.Rows

                            intNumberOfCounters = row.Item("NumberOfCounters")
                            intNumberOfBusinessDat = row.Item("NumberOfBusinessDat")
                            intNumberOfStaffs = row.Item("NumberOfStaffs")

                            intCustomerVisit = row.Item("CustomerVisit")
                            intCallRegistered = row.Item("CallRegistered")
                            intRepairCompleted = row.Item("RepairCompleted")
                            intGoodsDelivered = row.Item("GoodsDelivered")
                            intPending = row.Item("Pending")
                            intWarranty = row.Item("Warranty")
                            intInWarranty = row.Item("InWarranty")
                            intOutWarranty = row.Item("OutWarranty")
                            decInWarrantyAmount = row.Item("InWarrantyAmount")
                            decInWarrantyLabour = row.Item("InWarrantyLabour")
                            decInWarrantyParts = row.Item("InWarrantyParts")
                            decInWarrantyTransport = row.Item("InWarrantyTransport")
                            decInWarrantyOthers = row.Item("InWarrantyOthers")
                            decOutWarrantyAmount = row.Item("OutWarrantyAmount")
                            decOutWarrantyLabour = row.Item("OutWarrantyLabour")
                            decOutWarrantyParts = row.Item("OutWarrantyParts")
                            decOutWarrantyTransport = row.Item("OutWarrantyTransport")
                            decOutWarrantyOthers = row.Item("OutWarrantyOthers")
                            decSawDiscountLabour = row.Item("SawDiscountLabour")
                            decSawDiscountParts = row.Item("SawDiscountParts")
                            decOutWarrantyLabourwSawDisc = row.Item("OutWarrantyLabourwSawDisc")
                            decOutWarrantyPartswSawDisc = row.Item("OutWarrantyPartswSawDisc")
                            decRevenueWithoutTax = row.Item("RevenueWithoutTax")
                            decIwPartsConsumed = row.Item("IwPartsConsumed")
                            decTotalPartsConsumed = row.Item("TotalPartsConsumed")
                            decPrimeCostTotal = row.Item("PrimeCostTotal")
                            decGrossProfit = row.Item("GrossProfit")
                            decFinalPercentage = row.Item("FinalPercentage")

                            intNumberOfCountersTot = intNumberOfCountersTot + intNumberOfCounters
                            intNumberOfBusinessDatTot = intNumberOfBusinessDatTot + intNumberOfBusinessDat
                            intNumberOfStaffsTot = intNumberOfStaffsTot + intNumberOfStaffs

                            intCustomerVisitTot = intCustomerVisitTot + intCustomerVisit
                            intCallRegisteredTot = intCallRegisteredTot + intCallRegistered
                            intRepairCompletedTot = intRepairCompletedTot + intRepairCompleted
                            intGoodsDeliveredTot = intGoodsDeliveredTot + intGoodsDelivered
                            intPendingTot = intPendingTot + intPending
                            intWarrantyTot = intWarrantyTot + intWarranty
                            intInWarrantyTot = intInWarrantyTot + intInWarranty
                            intOutWarrantyTot = intOutWarrantyTot + intOutWarranty
                            decInWarrantyAmountTot = decInWarrantyAmountTot + decInWarrantyAmount
                            decInWarrantyLabourTot = decInWarrantyLabourTot + decInWarrantyLabour
                            decInWarrantyPartsTot = decInWarrantyPartsTot + decInWarrantyParts
                            decInWarrantyTransportTot = decInWarrantyTransportTot + decInWarrantyTransport
                            decInWarrantyOthersTot = decInWarrantyOthersTot + decInWarrantyOthers
                            decOutWarrantyAmountTot = decOutWarrantyAmountTot + decOutWarrantyAmount
                            decOutWarrantyLabourTot = decOutWarrantyLabourTot + decOutWarrantyLabour
                            decOutWarrantyPartsTot = decOutWarrantyPartsTot + decOutWarrantyParts
                            decOutWarrantyTransportTot = decOutWarrantyTransportTot + decOutWarrantyTransport
                            decOutWarrantyOthersTot = decOutWarrantyOthersTot + decOutWarrantyOthers
                            decSawDiscountLabourTot = decSawDiscountLabourTot + decSawDiscountLabour
                            decSawDiscountPartsTot = decSawDiscountPartsTot + decSawDiscountParts
                            decOutWarrantyLabourwSawDiscTot = decOutWarrantyLabourwSawDiscTot + decOutWarrantyLabourwSawDisc
                            decOutWarrantyPartswSawDiscTot = decOutWarrantyPartswSawDiscTot + decOutWarrantyPartswSawDisc
                            decRevenueWithoutTaxTot = decRevenueWithoutTaxTot + decRevenueWithoutTax
                            decIwPartsConsumedTot = decIwPartsConsumedTot + decIwPartsConsumed
                            decTotalPartsConsumedTot = decTotalPartsConsumedTot + decTotalPartsConsumed
                            decPrimeCostTotalTot = decPrimeCostTotalTot + decPrimeCostTotal
                            decGrossProfitTot = decGrossProfitTot + decGrossProfit
                            decFinalPercentageTot = decFinalPercentageTot + decFinalPercentage

                            rowTitle = rowTitle & "," & strDate

                            rowNumberOfCounters = rowNumberOfCounters & "," '& intNumberOfCounters
                            rowNumberOfBusinessDat = rowNumberOfBusinessDat & "," '& intNumberOfBusinessDat
                            rowNumberOfStaffs = rowNumberOfStaffs & "," '& intNumberOfStaffs

                            rowCustomerVisit = rowCustomerVisit & "," & intCustomerVisit
                            rowCallRegistered = rowCallRegistered & "," & intCallRegistered
                            rowRepairCompleted = rowRepairCompleted & "," & intRepairCompleted
                            rowGoodsDelivered = rowGoodsDelivered & "," & intGoodsDelivered
                            rowPending = rowPending & "," & intPending
                            rowWarranty = rowWarranty & "," & intWarranty
                            rowInWarranty = rowInWarranty & "," & intInWarranty
                            rowOutWarranty = rowOutWarranty & "," & intOutWarranty
                            rowInWarrantyAmount = rowInWarrantyAmount & "," & decInWarrantyAmount
                            rowInWarrantyLabour = rowInWarrantyLabour & "," & decInWarrantyLabour
                            rowInWarrantyParts = rowInWarrantyParts & "," & decInWarrantyParts
                            rowInWarrantyTransport = rowInWarrantyTransport & "," & decInWarrantyTransport
                            rowInWarrantyOthers = rowInWarrantyOthers & "," & decInWarrantyOthers
                            rowOutWarrantyAmount = rowOutWarrantyAmount & "," & decOutWarrantyAmount
                            rowOutWarrantyLabour = rowOutWarrantyLabour & "," & decOutWarrantyLabour
                            rowOutWarrantyParts = rowOutWarrantyParts & "," & decOutWarrantyParts
                            rowOutWarrantyTransport = rowOutWarrantyTransport & "," & decOutWarrantyTransport
                            rowOutWarrantyOthers = rowOutWarrantyOthers & "," & decOutWarrantyOthers
                            rowSawDiscountLabour = rowSawDiscountLabour & "," & decSawDiscountLabour
                            rowSawDiscountParts = rowSawDiscountParts & "," & decSawDiscountParts
                            rowOutWarrantyLabourwSawDisc = rowOutWarrantyLabourwSawDisc & "," & decOutWarrantyLabourwSawDisc
                            rowOutWarrantyPartswSawDisc = rowOutWarrantyPartswSawDisc & "," & decOutWarrantyPartswSawDisc
                            rowRevenueWithoutTax = rowRevenueWithoutTax & "," & decRevenueWithoutTax
                            rowIwPartsConsumed = rowIwPartsConsumed & "," & decIwPartsConsumed
                            rowTotalPartsConsumed = rowTotalPartsConsumed & "," & decTotalPartsConsumed
                            rowPrimeCostTotal = rowPrimeCostTotal & "," & decPrimeCostTotal
                            rowGrossProfit = rowGrossProfit & "," & decGrossProfit
                            If decRevenueWithoutTax = 0 Then
                                FinalPercentage1 = decGrossProfit
                            Else
                                FinalPercentage1 = comcontrol.Money_Round((decGrossProfit / decRevenueWithoutTax) * 100, 2)
                            End If
                            rowFinalPercentage = rowFinalPercentage & "," & FinalPercentage1 & "%" 'decFinalPercentage / 100
                        Next row
                    End If


                    'Monday3Week & Sunday3Week
                    strDay = Monday3Week.Day()
                    strMon = Monday3Week.Month
                    strYear = Monday3Week.Year

                    If Len(strDay) <= 1 Then
                        strDay = "0" & strDay
                    End If
                    If Len(strMon) <= 1 Then
                        strMon = "0" & strMon
                    End If
                    strDate = strYear & strMon & strDay
                    'Read from 
                    _dtPlReprt = comcontrol.SelectPlReport(Monday3Week, Sunday3Week, DropListLocation.SelectedItem.Text, DropListLocation.SelectedItem.Value, Gm)
                    If (_dtPlReprt Is Nothing) Or (_dtPlReprt.Rows.Count = 0) Then
                    Else
                        For Each row As DataRow In _dtPlReprt.Rows

                            intNumberOfCounters = row.Item("NumberOfCounters")
                            intNumberOfBusinessDat = row.Item("NumberOfBusinessDat")
                            intNumberOfStaffs = row.Item("NumberOfStaffs")

                            intCustomerVisit = row.Item("CustomerVisit")
                            intCallRegistered = row.Item("CallRegistered")
                            intRepairCompleted = row.Item("RepairCompleted")
                            intGoodsDelivered = row.Item("GoodsDelivered")
                            intPending = row.Item("Pending")
                            intWarranty = row.Item("Warranty")
                            intInWarranty = row.Item("InWarranty")
                            intOutWarranty = row.Item("OutWarranty")
                            decInWarrantyAmount = row.Item("InWarrantyAmount")
                            decInWarrantyLabour = row.Item("InWarrantyLabour")
                            decInWarrantyParts = row.Item("InWarrantyParts")
                            decInWarrantyTransport = row.Item("InWarrantyTransport")
                            decInWarrantyOthers = row.Item("InWarrantyOthers")
                            decOutWarrantyAmount = row.Item("OutWarrantyAmount")
                            decOutWarrantyLabour = row.Item("OutWarrantyLabour")
                            decOutWarrantyParts = row.Item("OutWarrantyParts")
                            decOutWarrantyTransport = row.Item("OutWarrantyTransport")
                            decOutWarrantyOthers = row.Item("OutWarrantyOthers")
                            decSawDiscountLabour = row.Item("SawDiscountLabour")
                            decSawDiscountParts = row.Item("SawDiscountParts")
                            decOutWarrantyLabourwSawDisc = row.Item("OutWarrantyLabourwSawDisc")
                            decOutWarrantyPartswSawDisc = row.Item("OutWarrantyPartswSawDisc")
                            decRevenueWithoutTax = row.Item("RevenueWithoutTax")
                            decIwPartsConsumed = row.Item("IwPartsConsumed")
                            decTotalPartsConsumed = row.Item("TotalPartsConsumed")
                            decPrimeCostTotal = row.Item("PrimeCostTotal")
                            decGrossProfit = row.Item("GrossProfit")
                            decFinalPercentage = row.Item("FinalPercentage")

                            intNumberOfCountersTot = intNumberOfCountersTot + intNumberOfCounters
                            intNumberOfBusinessDatTot = intNumberOfBusinessDatTot + intNumberOfBusinessDat
                            intNumberOfStaffsTot = intNumberOfStaffsTot + intNumberOfStaffs

                            intCustomerVisitTot = intCustomerVisitTot + intCustomerVisit
                            intCallRegisteredTot = intCallRegisteredTot + intCallRegistered
                            intRepairCompletedTot = intRepairCompletedTot + intRepairCompleted
                            intGoodsDeliveredTot = intGoodsDeliveredTot + intGoodsDelivered
                            intPendingTot = intPendingTot + intPending
                            intWarrantyTot = intWarrantyTot + intWarranty
                            intInWarrantyTot = intInWarrantyTot + intInWarranty
                            intOutWarrantyTot = intOutWarrantyTot + intOutWarranty
                            decInWarrantyAmountTot = decInWarrantyAmountTot + decInWarrantyAmount
                            decInWarrantyLabourTot = decInWarrantyLabourTot + decInWarrantyLabour
                            decInWarrantyPartsTot = decInWarrantyPartsTot + decInWarrantyParts
                            decInWarrantyTransportTot = decInWarrantyTransportTot + decInWarrantyTransport
                            decInWarrantyOthersTot = decInWarrantyOthersTot + decInWarrantyOthers
                            decOutWarrantyAmountTot = decOutWarrantyAmountTot + decOutWarrantyAmount
                            decOutWarrantyLabourTot = decOutWarrantyLabourTot + decOutWarrantyLabour
                            decOutWarrantyPartsTot = decOutWarrantyPartsTot + decOutWarrantyParts
                            decOutWarrantyTransportTot = decOutWarrantyTransportTot + decOutWarrantyTransport
                            decOutWarrantyOthersTot = decOutWarrantyOthersTot + decOutWarrantyOthers
                            decSawDiscountLabourTot = decSawDiscountLabourTot + decSawDiscountLabour
                            decSawDiscountPartsTot = decSawDiscountPartsTot + decSawDiscountParts
                            decOutWarrantyLabourwSawDiscTot = decOutWarrantyLabourwSawDiscTot + decOutWarrantyLabourwSawDisc
                            decOutWarrantyPartswSawDiscTot = decOutWarrantyPartswSawDiscTot + decOutWarrantyPartswSawDisc
                            decRevenueWithoutTaxTot = decRevenueWithoutTaxTot + decRevenueWithoutTax
                            decIwPartsConsumedTot = decIwPartsConsumedTot + decIwPartsConsumed
                            decTotalPartsConsumedTot = decTotalPartsConsumedTot + decTotalPartsConsumed
                            decPrimeCostTotalTot = decPrimeCostTotalTot + decPrimeCostTotal
                            decGrossProfitTot = decGrossProfitTot + decGrossProfit
                            decFinalPercentageTot = decFinalPercentageTot + decFinalPercentage

                            rowTitle = rowTitle & "," & strDate

                            rowNumberOfCounters = rowNumberOfCounters & "," '& intNumberOfCounters
                            rowNumberOfBusinessDat = rowNumberOfBusinessDat & "," '& intNumberOfBusinessDat
                            rowNumberOfStaffs = rowNumberOfStaffs & "," '& intNumberOfStaffs

                            rowCustomerVisit = rowCustomerVisit & "," & intCustomerVisit
                            rowCallRegistered = rowCallRegistered & "," & intCallRegistered
                            rowRepairCompleted = rowRepairCompleted & "," & intRepairCompleted
                            rowGoodsDelivered = rowGoodsDelivered & "," & intGoodsDelivered
                            rowPending = rowPending & "," & intPending
                            rowWarranty = rowWarranty & "," & intWarranty
                            rowInWarranty = rowInWarranty & "," & intInWarranty
                            rowOutWarranty = rowOutWarranty & "," & intOutWarranty
                            rowInWarrantyAmount = rowInWarrantyAmount & "," & decInWarrantyAmount
                            rowInWarrantyLabour = rowInWarrantyLabour & "," & decInWarrantyLabour
                            rowInWarrantyParts = rowInWarrantyParts & "," & decInWarrantyParts
                            rowInWarrantyTransport = rowInWarrantyTransport & "," & decInWarrantyTransport
                            rowInWarrantyOthers = rowInWarrantyOthers & "," & decInWarrantyOthers
                            rowOutWarrantyAmount = rowOutWarrantyAmount & "," & decOutWarrantyAmount
                            rowOutWarrantyLabour = rowOutWarrantyLabour & "," & decOutWarrantyLabour
                            rowOutWarrantyParts = rowOutWarrantyParts & "," & decOutWarrantyParts
                            rowOutWarrantyTransport = rowOutWarrantyTransport & "," & decOutWarrantyTransport
                            rowOutWarrantyOthers = rowOutWarrantyOthers & "," & decOutWarrantyOthers
                            rowSawDiscountLabour = rowSawDiscountLabour & "," & decSawDiscountLabour
                            rowSawDiscountParts = rowSawDiscountParts & "," & decSawDiscountParts
                            rowOutWarrantyLabourwSawDisc = rowOutWarrantyLabourwSawDisc & "," & decOutWarrantyLabourwSawDisc
                            rowOutWarrantyPartswSawDisc = rowOutWarrantyPartswSawDisc & "," & decOutWarrantyPartswSawDisc
                            rowRevenueWithoutTax = rowRevenueWithoutTax & "," & decRevenueWithoutTax
                            rowIwPartsConsumed = rowIwPartsConsumed & "," & decIwPartsConsumed
                            rowTotalPartsConsumed = rowTotalPartsConsumed & "," & decTotalPartsConsumed
                            rowPrimeCostTotal = rowPrimeCostTotal & "," & decPrimeCostTotal
                            rowGrossProfit = rowGrossProfit & "," & decGrossProfit
                            If decRevenueWithoutTax = 0 Then
                                FinalPercentage1 = decGrossProfit
                            Else
                                FinalPercentage1 = comcontrol.Money_Round((decGrossProfit / decRevenueWithoutTax) * 100, 2)
                            End If
                            rowFinalPercentage = rowFinalPercentage & "," & FinalPercentage1 & "%" 'decFinalPercentage / 100
                        Next row
                    End If


                    'Monday4Week & Sunday4Week
                    strDay = Monday4Week.Day()
                    strMon = Monday4Week.Month
                    strYear = Monday4Week.Year

                    If Len(strDay) <= 1 Then
                        strDay = "0" & strDay
                    End If
                    If Len(strMon) <= 1 Then
                        strMon = "0" & strMon
                    End If
                    strDate = strYear & strMon & strDay
                    'Read from 
                    _dtPlReprt = comcontrol.SelectPlReport(Monday4Week, Sunday4Week, DropListLocation.SelectedItem.Text, DropListLocation.SelectedItem.Value, Gm)

                    If (_dtPlReprt Is Nothing) Or (_dtPlReprt.Rows.Count = 0) Then
                    Else
                        For Each row As DataRow In _dtPlReprt.Rows
                            intNumberOfCounters = row.Item("NumberOfCounters")
                            intNumberOfBusinessDat = row.Item("NumberOfBusinessDat")
                            intNumberOfStaffs = row.Item("NumberOfStaffs")

                            intCustomerVisit = row.Item("CustomerVisit")
                            intCallRegistered = row.Item("CallRegistered")
                            intRepairCompleted = row.Item("RepairCompleted")
                            intGoodsDelivered = row.Item("GoodsDelivered")
                            intPending = row.Item("Pending")
                            intWarranty = row.Item("Warranty")
                            intInWarranty = row.Item("InWarranty")
                            intOutWarranty = row.Item("OutWarranty")
                            decInWarrantyAmount = row.Item("InWarrantyAmount")
                            decInWarrantyLabour = row.Item("InWarrantyLabour")
                            decInWarrantyParts = row.Item("InWarrantyParts")
                            decInWarrantyTransport = row.Item("InWarrantyTransport")
                            decInWarrantyOthers = row.Item("InWarrantyOthers")
                            decOutWarrantyAmount = row.Item("OutWarrantyAmount")
                            decOutWarrantyLabour = row.Item("OutWarrantyLabour")
                            decOutWarrantyParts = row.Item("OutWarrantyParts")
                            decOutWarrantyTransport = row.Item("OutWarrantyTransport")
                            decOutWarrantyOthers = row.Item("OutWarrantyOthers")
                            decSawDiscountLabour = row.Item("SawDiscountLabour")
                            decSawDiscountParts = row.Item("SawDiscountParts")
                            decOutWarrantyLabourwSawDisc = row.Item("OutWarrantyLabourwSawDisc")
                            decOutWarrantyPartswSawDisc = row.Item("OutWarrantyPartswSawDisc")
                            decRevenueWithoutTax = row.Item("RevenueWithoutTax")
                            decIwPartsConsumed = row.Item("IwPartsConsumed")
                            decTotalPartsConsumed = row.Item("TotalPartsConsumed")
                            decPrimeCostTotal = row.Item("PrimeCostTotal")
                            decGrossProfit = row.Item("GrossProfit")
                            decFinalPercentage = row.Item("FinalPercentage")

                            intNumberOfCountersTot = intNumberOfCountersTot + intNumberOfCounters
                            intNumberOfBusinessDatTot = intNumberOfBusinessDatTot + intNumberOfBusinessDat
                            intNumberOfStaffsTot = intNumberOfStaffsTot + intNumberOfStaffs

                            intCustomerVisitTot = intCustomerVisitTot + intCustomerVisit
                            intCallRegisteredTot = intCallRegisteredTot + intCallRegistered
                            intRepairCompletedTot = intRepairCompletedTot + intRepairCompleted
                            intGoodsDeliveredTot = intGoodsDeliveredTot + intGoodsDelivered
                            intPendingTot = intPendingTot + intPending
                            intWarrantyTot = intWarrantyTot + intWarranty
                            intInWarrantyTot = intInWarrantyTot + intInWarranty
                            intOutWarrantyTot = intOutWarrantyTot + intOutWarranty
                            decInWarrantyAmountTot = decInWarrantyAmountTot + decInWarrantyAmount
                            decInWarrantyLabourTot = decInWarrantyLabourTot + decInWarrantyLabour
                            decInWarrantyPartsTot = decInWarrantyPartsTot + decInWarrantyParts
                            decInWarrantyTransportTot = decInWarrantyTransportTot + decInWarrantyTransport
                            decInWarrantyOthersTot = decInWarrantyOthersTot + decInWarrantyOthers
                            decOutWarrantyAmountTot = decOutWarrantyAmountTot + decOutWarrantyAmount
                            decOutWarrantyLabourTot = decOutWarrantyLabourTot + decOutWarrantyLabour
                            decOutWarrantyPartsTot = decOutWarrantyPartsTot + decOutWarrantyParts
                            decOutWarrantyTransportTot = decOutWarrantyTransportTot + decOutWarrantyTransport
                            decOutWarrantyOthersTot = decOutWarrantyOthersTot + decOutWarrantyOthers
                            decSawDiscountLabourTot = decSawDiscountLabourTot + decSawDiscountLabour
                            decSawDiscountPartsTot = decSawDiscountPartsTot + decSawDiscountParts
                            decOutWarrantyLabourwSawDiscTot = decOutWarrantyLabourwSawDiscTot + decOutWarrantyLabourwSawDisc
                            decOutWarrantyPartswSawDiscTot = decOutWarrantyPartswSawDiscTot + decOutWarrantyPartswSawDisc
                            decRevenueWithoutTaxTot = decRevenueWithoutTaxTot + decRevenueWithoutTax
                            decIwPartsConsumedTot = decIwPartsConsumedTot + decIwPartsConsumed
                            decTotalPartsConsumedTot = decTotalPartsConsumedTot + decTotalPartsConsumed
                            decPrimeCostTotalTot = decPrimeCostTotalTot + decPrimeCostTotal
                            decGrossProfitTot = decGrossProfitTot + decGrossProfit
                            decFinalPercentageTot = decFinalPercentageTot + decFinalPercentage

                            rowTitle = rowTitle & "," & strDate

                            rowNumberOfCounters = rowNumberOfCounters & "," '& intNumberOfCounters
                            rowNumberOfBusinessDat = rowNumberOfBusinessDat & "," '& intNumberOfBusinessDat
                            rowNumberOfStaffs = rowNumberOfStaffs & "," '& intNumberOfStaffs

                            rowCustomerVisit = rowCustomerVisit & "," & intCustomerVisit
                            rowCallRegistered = rowCallRegistered & "," & intCallRegistered
                            rowRepairCompleted = rowRepairCompleted & "," & intRepairCompleted
                            rowGoodsDelivered = rowGoodsDelivered & "," & intGoodsDelivered
                            rowPending = rowPending & "," & intPending
                            rowWarranty = rowWarranty & "," & intWarranty
                            rowInWarranty = rowInWarranty & "," & intInWarranty
                            rowOutWarranty = rowOutWarranty & "," & intOutWarranty
                            rowInWarrantyAmount = rowInWarrantyAmount & "," & decInWarrantyAmount
                            rowInWarrantyLabour = rowInWarrantyLabour & "," & decInWarrantyLabour
                            rowInWarrantyParts = rowInWarrantyParts & "," & decInWarrantyParts
                            rowInWarrantyTransport = rowInWarrantyTransport & "," & decInWarrantyTransport
                            rowInWarrantyOthers = rowInWarrantyOthers & "," & decInWarrantyOthers
                            rowOutWarrantyAmount = rowOutWarrantyAmount & "," & decOutWarrantyAmount
                            rowOutWarrantyLabour = rowOutWarrantyLabour & "," & decOutWarrantyLabour
                            rowOutWarrantyParts = rowOutWarrantyParts & "," & decOutWarrantyParts
                            rowOutWarrantyTransport = rowOutWarrantyTransport & "," & decOutWarrantyTransport
                            rowOutWarrantyOthers = rowOutWarrantyOthers & "," & decOutWarrantyOthers
                            rowSawDiscountLabour = rowSawDiscountLabour & "," & decSawDiscountLabour
                            rowSawDiscountParts = rowSawDiscountParts & "," & decSawDiscountParts
                            rowOutWarrantyLabourwSawDisc = rowOutWarrantyLabourwSawDisc & "," & decOutWarrantyLabourwSawDisc
                            rowOutWarrantyPartswSawDisc = rowOutWarrantyPartswSawDisc & "," & decOutWarrantyPartswSawDisc
                            rowRevenueWithoutTax = rowRevenueWithoutTax & "," & decRevenueWithoutTax
                            rowIwPartsConsumed = rowIwPartsConsumed & "," & decIwPartsConsumed
                            rowTotalPartsConsumed = rowTotalPartsConsumed & "," & decTotalPartsConsumed
                            rowPrimeCostTotal = rowPrimeCostTotal & "," & decPrimeCostTotal
                            rowGrossProfit = rowGrossProfit & "," & decGrossProfit
                            If decRevenueWithoutTax = 0 Then
                                FinalPercentage1 = decGrossProfit
                            Else
                                FinalPercentage1 = comcontrol.Money_Round((decGrossProfit / decRevenueWithoutTax) * 100, 2)
                            End If
                            rowFinalPercentage = rowFinalPercentage & "," & FinalPercentage1 & "%" 'decFinalPercentage / 100
                        Next row
                    End If


                    Exit For
                End If
            Next

            If DropListLocation.SelectedItem.Text = "ALL" Then
                'For Total Displa
                row0 = row0 & ",ALL SSC,ALL SSC,ALL SSC,ALL SSC"
                rowTitle = rowTitle & "," & SscTotals(0, 0) & "," & SscTotals(1, 0) & "," & SscTotals(2, 0) & "," & SscTotals(3, 0) & ",Total"

                rowNumberOfCounters = rowNumberOfCounters & "," & SscTotals(0, 1) & "," & SscTotals(1, 1) & "," & SscTotals(2, 1) & "," & SscTotals(3, 1) & "," & intNumberOfCountersTot
                rowNumberOfBusinessDat = rowNumberOfBusinessDat & "," & SscTotals(0, 2) & "," & SscTotals(1, 2) & "," & SscTotals(2, 2) & "," & SscTotals(3, 2) & "," & intNumberOfBusinessDatTot
                rowNumberOfStaffs = rowNumberOfStaffs & "," & SscTotals(0, 3) & "," & SscTotals(1, 3) & "," & SscTotals(2, 3) & "," & SscTotals(3, 3) & "," & intNumberOfStaffsTot

                rowCustomerVisit = rowCustomerVisit & "," & SscTotals(0, 4) & "," & SscTotals(1, 4) & "," & SscTotals(2, 4) & "," & SscTotals(3, 4) & "," & intCustomerVisitTot
                rowCallRegistered = rowCallRegistered & "," & SscTotals(0, 5) & "," & SscTotals(1, 5) & "," & SscTotals(2, 5) & "," & SscTotals(3, 5) & "," & intCallRegisteredTot
                rowRepairCompleted = rowRepairCompleted & "," & SscTotals(0, 6) & "," & SscTotals(1, 6) & "," & SscTotals(2, 6) & "," & SscTotals(3, 6) & "," & intRepairCompletedTot
                rowGoodsDelivered = rowGoodsDelivered & "," & SscTotals(0, 7) & "," & SscTotals(1, 7) & "," & SscTotals(2, 7) & "," & SscTotals(3, 7) & "," & intGoodsDeliveredTot
                rowPending = rowPending & "," & SscTotals(0, 8) & "," & SscTotals(1, 8) & "," & SscTotals(2, 8) & "," & SscTotals(3, 8) & "," & intPendingTot
                rowWarranty = rowWarranty & "," & SscTotals(0, 9) & "," & SscTotals(1, 9) & "," & SscTotals(2, 9) & "," & SscTotals(3, 9) & "," & intWarrantyTot
                rowInWarranty = rowInWarranty & "," & SscTotals(0, 10) & "," & SscTotals(1, 10) & "," & SscTotals(2, 10) & "," & SscTotals(3, 10) & "," & intInWarrantyTot
                rowOutWarranty = rowOutWarranty & "," & SscTotals(0, 11) & "," & SscTotals(1, 11) & "," & SscTotals(2, 11) & "," & SscTotals(3, 11) & "," & intOutWarrantyTot
                rowInWarrantyAmount = rowInWarrantyAmount & "," & SscTotals(0, 12) & "," & SscTotals(1, 12) & "," & SscTotals(2, 12) & "," & SscTotals(3, 12) & "," & decInWarrantyAmountTot
                rowInWarrantyLabour = rowInWarrantyLabour & "," & SscTotals(0, 13) & "," & SscTotals(1, 13) & "," & SscTotals(2, 13) & "," & SscTotals(3, 13) & "," & decInWarrantyLabourTot
                rowInWarrantyParts = rowInWarrantyParts & "," & SscTotals(0, 14) & "," & SscTotals(1, 14) & "," & SscTotals(2, 14) & "," & SscTotals(3, 14) & "," & decInWarrantyPartsTot
                rowInWarrantyTransport = rowInWarrantyTransport & "," & SscTotals(0, 15) & "," & SscTotals(1, 15) & "," & SscTotals(2, 15) & "," & SscTotals(3, 15) & "," & decInWarrantyTransportTot
                rowInWarrantyOthers = rowInWarrantyOthers & "," & SscTotals(0, 16) & "," & SscTotals(1, 16) & "," & SscTotals(2, 16) & "," & SscTotals(3, 16) & "," & decInWarrantyOthersTot
                rowOutWarrantyAmount = rowOutWarrantyAmount & "," & SscTotals(0, 17) & "," & SscTotals(1, 17) & "," & SscTotals(2, 17) & "," & SscTotals(3, 17) & "," & decOutWarrantyAmountTot
                rowOutWarrantyLabour = rowOutWarrantyLabour & "," & SscTotals(0, 18) & "," & SscTotals(1, 18) & "," & SscTotals(2, 18) & "," & SscTotals(3, 18) & "," & decOutWarrantyLabourTot
                rowOutWarrantyParts = rowOutWarrantyParts & "," & SscTotals(0, 19) & "," & SscTotals(1, 19) & "," & SscTotals(2, 19) & "," & SscTotals(3, 19) & "," & decOutWarrantyPartsTot
                rowOutWarrantyTransport = rowOutWarrantyTransport & "," & SscTotals(0, 20) & "," & SscTotals(1, 20) & "," & SscTotals(2, 20) & "," & SscTotals(3, 20) & "," & decOutWarrantyTransportTot
                rowOutWarrantyOthers = rowOutWarrantyOthers & "," & SscTotals(0, 21) & "," & SscTotals(1, 21) & "," & SscTotals(2, 21) & "," & SscTotals(3, 21) & "," & decOutWarrantyOthersTot
                rowSawDiscountLabour = rowSawDiscountLabour & "," & SscTotals(0, 22) & "," & SscTotals(1, 22) & "," & SscTotals(2, 22) & "," & SscTotals(3, 22) & "," & decSawDiscountLabourTot
                rowSawDiscountParts = rowSawDiscountParts & "," & SscTotals(0, 23) & "," & SscTotals(1, 23) & "," & SscTotals(2, 23) & "," & SscTotals(3, 23) & "," & decSawDiscountPartsTot
                rowOutWarrantyLabourwSawDisc = rowOutWarrantyLabourwSawDisc & "," & SscTotals(0, 24) & "," & SscTotals(1, 24) & "," & SscTotals(2, 24) & "," & SscTotals(3, 24) & "," & decOutWarrantyLabourwSawDiscTot
                rowOutWarrantyPartswSawDisc = rowOutWarrantyPartswSawDisc & "," & SscTotals(0, 25) & "," & SscTotals(1, 25) & "," & SscTotals(2, 25) & "," & SscTotals(3, 25) & "," & decOutWarrantyPartswSawDiscTot
                rowRevenueWithoutTax = rowRevenueWithoutTax & "," & SscTotals(0, 26) & "," & SscTotals(1, 26) & "," & SscTotals(2, 26) & "," & SscTotals(3, 26) & "," & decRevenueWithoutTaxTot
                rowIwPartsConsumed = rowIwPartsConsumed & "," & SscTotals(0, 27) & "," & SscTotals(1, 27) & "," & SscTotals(2, 27) & "," & SscTotals(3, 27) & "," & decIwPartsConsumedTot
                rowTotalPartsConsumed = rowTotalPartsConsumed & "," & SscTotals(0, 28) & "," & SscTotals(1, 28) & "," & SscTotals(2, 28) & "," & SscTotals(3, 28) & "," & decTotalPartsConsumedTot
                rowPrimeCostTotal = rowPrimeCostTotal & "," & SscTotals(0, 29) & "," & SscTotals(1, 29) & "," & SscTotals(2, 29) & "," & SscTotals(3, 29) & "," & decPrimeCostTotalTot
                rowGrossProfit = rowGrossProfit & "," & SscTotals(0, 30) & "," & SscTotals(1, 30) & "," & SscTotals(2, 30) & "," & SscTotals(3, 30) & "," & decGrossProfitTot
                'rowFinalPercentage = rowFinalPercentage & "," & SscTotals(0, 31) & "," & SscTotals(1, 31) & "," & SscTotals(2, 31) & "," & SscTotals(3, 31) & "," & decGrossProfitTot



                If SscTotals(0, 26) = 0 Then ' RevenueWithoutTax = 0
                    SscTotals031 = SscTotals(0, 30) 'GrossProfit
                Else
                    SscTotals031 = comcontrol.Money_Round((SscTotals(0, 30) / SscTotals(0, 26)) * 100, 2)
                End If
                If SscTotals(1, 26) = 0 Then ' RevenueWithoutTax = 0
                    SscTotals131 = SscTotals(1, 30) 'GrossProfit
                Else
                    SscTotals131 = comcontrol.Money_Round((SscTotals(1, 30) / SscTotals(1, 26)) * 100, 2)
                End If
                If SscTotals(2, 26) = 0 Then ' RevenueWithoutTax = 0
                    SscTotals231 = SscTotals(2, 30) 'GrossProfit
                Else
                    SscTotals231 = comcontrol.Money_Round((SscTotals(2, 30) / SscTotals(2, 26)) * 100, 2)
                End If
                If SscTotals(3, 26) = 0 Then ' RevenueWithoutTax = 0
                    SscTotals331 = SscTotals(3, 30) 'GrossProfit
                Else
                    SscTotals331 = comcontrol.Money_Round((SscTotals(3, 30) / SscTotals(3, 26)) * 100, 2)
                End If
                If decRevenueWithoutTaxTot = 0 Then ' RevenueWithoutTax = 0
                    _FinalPercentage = decGrossProfitTot 'GrossProfit
                Else
                    _FinalPercentage = comcontrol.Money_Round((decGrossProfitTot / decRevenueWithoutTaxTot) * 100, 2)
                End If
                rowFinalPercentage = rowFinalPercentage & "," & SscTotals031 & "%" & "," & SscTotals131 & "%" & "," & SscTotals231 & "%" & "," & SscTotals331 & "%" & "," & _FinalPercentage & "%"

                'For Display End of Label
                rowTitle = rowTitle & ","

                rowNumberOfCounters = rowNumberOfCounters & "," & "Number of counters"
                rowNumberOfBusinessDat = rowNumberOfBusinessDat & "," & "Number of Business dat"
                rowNumberOfStaffs = rowNumberOfStaffs & "," & "Number of Staffs"

                rowCustomerVisit = rowCustomerVisit & "," & "CustomerVisit"
                rowCallRegistered = rowCallRegistered & "," & "CallRegistered"
                rowRepairCompleted = rowRepairCompleted & "," & "RepairCompleted"
                rowGoodsDelivered = rowGoodsDelivered & "," & "GoodsDelivered"
                rowPending = rowPending & "," & "Pending"
                rowWarranty = rowWarranty & "," & "Warranty"
                rowInWarranty = rowInWarranty & "," & "InWarranty"
                rowOutWarranty = rowOutWarranty & "," & "OutWarranty"
                rowInWarrantyAmount = rowInWarrantyAmount & "," & "InWarrantyAmount"
                rowInWarrantyLabour = rowInWarrantyLabour & "," & "InWarrantyLabour"
                rowInWarrantyParts = rowInWarrantyParts & "," & "InWarrantyParts"
                rowInWarrantyTransport = rowInWarrantyTransport & "," & "InWarrantyTransport"
                rowInWarrantyOthers = rowInWarrantyOthers & "," & "InWarrantyOthers"
                rowOutWarrantyAmount = rowOutWarrantyAmount & "," & "OutWarrantyAmount"
                rowOutWarrantyLabour = rowOutWarrantyLabour & "," & "OutWarrantyLabour"
                rowOutWarrantyParts = rowOutWarrantyParts & "," & "OutWarrantyParts"
                rowOutWarrantyTransport = rowOutWarrantyTransport & "," & "OutWarrantyTransport"
                rowOutWarrantyOthers = rowOutWarrantyOthers & "," & "OutWarrantyOthers"
                rowSawDiscountLabour = rowSawDiscountLabour & "," & "SawDiscountLabour"
                rowSawDiscountParts = rowSawDiscountParts & "," & "SawDiscountParts"
                rowOutWarrantyLabourwSawDisc = rowOutWarrantyLabourwSawDisc & "," & "OutWarrantyLabourwSawDisc"
                rowOutWarrantyPartswSawDisc = rowOutWarrantyPartswSawDisc & "," & "OutWarrantyPartswSawDisc"
                rowRevenueWithoutTax = rowRevenueWithoutTax & "," & "RevenueWithoutTax"
                rowIwPartsConsumed = rowIwPartsConsumed & "," & "IwPartsConsumed"
                rowTotalPartsConsumed = rowTotalPartsConsumed & "," & "TotalPartsConsumed"
                rowPrimeCostTotal = rowPrimeCostTotal & "," & "PrimeCostTotal"
                rowGrossProfit = rowGrossProfit & "," & "GrossProfit"
                rowFinalPercentage = rowFinalPercentage & "," & " "

            Else
                'For Total Displa
                rowTitle = rowTitle & ",Total"

                rowNumberOfCounters = rowNumberOfCounters & "," '& intNumberOfCounters
                rowNumberOfBusinessDat = rowNumberOfBusinessDat & "," '& intNumberOfBusinessDat
                rowNumberOfStaffs = rowNumberOfStaffs & "," '& intNumberOfStaffs

                rowCustomerVisit = rowCustomerVisit & "," & intCustomerVisitTot
                rowCallRegistered = rowCallRegistered & "," & intCallRegisteredTot
                rowRepairCompleted = rowRepairCompleted & "," & intRepairCompletedTot
                rowGoodsDelivered = rowGoodsDelivered & "," & intGoodsDeliveredTot
                rowPending = rowPending & "," & intPendingTot
                rowWarranty = rowWarranty & "," & intWarrantyTot
                rowInWarranty = rowInWarranty & "," & intInWarrantyTot
                rowOutWarranty = rowOutWarranty & "," & intOutWarrantyTot
                rowInWarrantyAmount = rowInWarrantyAmount & "," & decInWarrantyAmountTot
                rowInWarrantyLabour = rowInWarrantyLabour & "," & decInWarrantyLabourTot
                rowInWarrantyParts = rowInWarrantyParts & "," & decInWarrantyPartsTot
                rowInWarrantyTransport = rowInWarrantyTransport & "," & decInWarrantyTransportTot
                rowInWarrantyOthers = rowInWarrantyOthers & "," & decInWarrantyOthersTot
                rowOutWarrantyAmount = rowOutWarrantyAmount & "," & decOutWarrantyAmountTot
                rowOutWarrantyLabour = rowOutWarrantyLabour & "," & decOutWarrantyLabourTot
                rowOutWarrantyParts = rowOutWarrantyParts & "," & decOutWarrantyPartsTot
                rowOutWarrantyTransport = rowOutWarrantyTransport & "," & decOutWarrantyTransportTot
                rowOutWarrantyOthers = rowOutWarrantyOthers & "," & decOutWarrantyOthersTot
                rowSawDiscountLabour = rowSawDiscountLabour & "," & decSawDiscountLabourTot
                rowSawDiscountParts = rowSawDiscountParts & "," & decSawDiscountPartsTot
                rowOutWarrantyLabourwSawDisc = rowOutWarrantyLabourwSawDisc & "," & decOutWarrantyLabourwSawDiscTot
                rowOutWarrantyPartswSawDisc = rowOutWarrantyPartswSawDisc & "," & decOutWarrantyPartswSawDiscTot
                rowRevenueWithoutTax = rowRevenueWithoutTax & "," & decRevenueWithoutTaxTot
                rowIwPartsConsumed = rowIwPartsConsumed & "," & decIwPartsConsumedTot
                rowTotalPartsConsumed = rowTotalPartsConsumed & "," & decTotalPartsConsumedTot
                rowPrimeCostTotal = rowPrimeCostTotal & "," & decPrimeCostTotalTot
                rowGrossProfit = rowGrossProfit & "," & decGrossProfitTot
                rowFinalPercentage = rowFinalPercentage & "," & decFinalPercentage

                'For Display End of Label
                rowTitle = rowTitle & ","
                rowNumberOfCounters = rowNumberOfCounters & "," & "Number of counters"
                rowNumberOfBusinessDat = rowNumberOfBusinessDat & "," & "Number of Business dat"
                rowNumberOfStaffs = rowNumberOfStaffs & "," & "Number of Staffs"

                rowCustomerVisit = rowCustomerVisit & "," & "CustomerVisit"
                rowCallRegistered = rowCallRegistered & "," & "CallRegistered"
                rowRepairCompleted = rowRepairCompleted & "," & "RepairCompleted"
                rowGoodsDelivered = rowGoodsDelivered & "," & "GoodsDelivered"
                rowPending = rowPending & "," & "Pending"
                rowWarranty = rowWarranty & "," & "Warranty"
                rowInWarranty = rowInWarranty & "," & "InWarranty"
                rowOutWarranty = rowOutWarranty & "," & "OutWarranty"
                rowInWarrantyAmount = rowInWarrantyAmount & "," & "InWarrantyAmount"
                rowInWarrantyLabour = rowInWarrantyLabour & "," & "InWarrantyLabour"
                rowInWarrantyParts = rowInWarrantyParts & "," & "InWarrantyParts"
                rowInWarrantyTransport = rowInWarrantyTransport & "," & "InWarrantyTransport"
                rowInWarrantyOthers = rowInWarrantyOthers & "," & "InWarrantyOthers"
                rowOutWarrantyAmount = rowOutWarrantyAmount & "," & "OutWarrantyAmount"
                rowOutWarrantyLabour = rowOutWarrantyLabour & "," & "OutWarrantyLabour"
                rowOutWarrantyParts = rowOutWarrantyParts & "," & "OutWarrantyParts"
                rowOutWarrantyTransport = rowOutWarrantyTransport & "," & "OutWarrantyTransport"
                rowOutWarrantyOthers = rowOutWarrantyOthers & "," & "OutWarrantyOthers"
                rowSawDiscountLabour = rowSawDiscountLabour & "," & "SawDiscountLabour"
                rowSawDiscountParts = rowSawDiscountParts & "," & "SawDiscountParts"
                rowOutWarrantyLabourwSawDisc = rowOutWarrantyLabourwSawDisc & "," & "OutWarrantyLabourwSawDisc"
                rowOutWarrantyPartswSawDisc = rowOutWarrantyPartswSawDisc & "," & "OutWarrantyPartswSawDisc"
                rowRevenueWithoutTax = rowRevenueWithoutTax & "," & "RevenueWithoutTax"
                rowIwPartsConsumed = rowIwPartsConsumed & "," & "IwPartsConsumed"
                rowTotalPartsConsumed = rowTotalPartsConsumed & "," & "TotalPartsConsumed"
                rowPrimeCostTotal = rowPrimeCostTotal & "," & "PrimeCostTotal"
                rowGrossProfit = rowGrossProfit & "," & "GrossProfit"

                rowFinalPercentage = rowFinalPercentage & "," & " "


            End If



            csvContents.Add(row0)
            csvContents.Add(rowTitle)

            csvContents.Add(rowNumberOfCounters)
            csvContents.Add(rowNumberOfBusinessDat)
            csvContents.Add(rowNumberOfStaffs)

            csvContents.Add(rowCustomerVisit)
            csvContents.Add(rowCallRegistered)
            csvContents.Add(rowRepairCompleted)
            csvContents.Add(rowGoodsDelivered)
            csvContents.Add(rowPending)
            csvContents.Add(rowWarranty)
            csvContents.Add(rowInWarranty)
            csvContents.Add(rowOutWarranty)
            csvContents.Add(rowInWarrantyAmount)
            csvContents.Add(rowInWarrantyLabour)
            csvContents.Add(rowInWarrantyParts)
            csvContents.Add(rowInWarrantyTransport)
            csvContents.Add(rowInWarrantyOthers)
            csvContents.Add(rowOutWarrantyAmount)
            csvContents.Add(rowOutWarrantyLabour)
            csvContents.Add(rowOutWarrantyParts)
            csvContents.Add(rowOutWarrantyTransport)
            csvContents.Add(rowOutWarrantyOthers)
            csvContents.Add(rowSawDiscountLabour)
            csvContents.Add(rowSawDiscountParts)
            csvContents.Add(rowOutWarrantyLabourwSawDisc)
            csvContents.Add(rowOutWarrantyPartswSawDisc)
            csvContents.Add(rowRevenueWithoutTax)
            csvContents.Add(rowIwPartsConsumed)
            csvContents.Add(rowTotalPartsConsumed)
            csvContents.Add(rowPrimeCostTotal)
            csvContents.Add(rowGrossProfit)
            csvContents.Add(rowFinalPercentage)

            Dim csvFileName As String

            Dim clsSetCommon As New Class_common
            Dim dtNow As DateTime = clsSetCommon.dtIndia
            Dim exportFile As String = ""

            Dim outputPath As String

            'PeriodFrom = Replace(PeriodFrom, "/", "")
            'PeriodTo = Replace(PeriodTo, "/", "")

            strDay = Now.Day()
            strMon = Now.Month
            strYear = Now.Year

            If Len(strDay) <= 1 Then
                strDay = "0" & strDay
            End If
            If Len(strMon) <= 1 Then
                strMon = "0" & strMon
            End If

            If DropListLocation.SelectedItem.Text = "ALL" Then
                csvFileName = "SM_4WEEK_PL_ALL_" & strYear & strMon & strDay & ".csv"
            Else
                csvFileName = "SM_4WEEK_PL_" & DropListLocation.SelectedItem.Text & "_" & strYear & strMon & strDay & ".csv"
            End If



            outputPath = clsSet.CsvFilePass & csvFileName

            ' outputPath = csvFileName

            Using writer As New StreamWriter(outputPath, False, Encoding.Default)
                writer.WriteLine(String.Join(Environment.NewLine, csvContents))
            End Using

            Dim Buffer As Byte() = System.IO.File.ReadAllBytes(outputPath)
            If outputPath <> "" Then
                If System.IO.File.Exists(outputPath) = True Then
                    System.IO.File.Delete(outputPath)
                End If
            End If
            Response.Clear()
            Response.Clear()
            Response.ClearHeaders()
            Response.Buffer = True
            Response.ContentType = "application/text/csv"
            Response.AddHeader("Content-Disposition", "attachment; filename=" & csvFileName)

            'Response.Write("<b>File Contents: </b>")
            Response.BinaryWrite(Buffer)

            Response.Flush()
            'Response.WriteFile(outputPath) 'if not required dialog box to user, then make comment this line
            Response.End()








            '****************************************
            'Monthly 
            'Begin
            '****************************************
        ElseIf DrpType.SelectedItem.Value = "02" Then ' Monthly


            strDay = 1
            strMon = DropDownMonth.SelectedItem.Value
            strYear = DropDownYear.SelectedItem.Text

            'For FileName
            Dim PeriodFrom As New System.DateTime(strYear, strMon, strDay, 0, 0, 0)

            Dim mon As Integer
            'De
            Dim monMonday1Week As New System.DateTime(strYear, strMon, strDay, 0, 0, 0)

            'lblText.Text = monMonday1Week & "<br>" & monMonday1Week.DayOfWeek

            If (monMonday1Week.DayOfWeek = 0) Then
                'Sunday
                monMonday1Week = monMonday1Week.AddDays(-6) 'Need to start Monday from previous month
            ElseIf (monMonday1Week.DayOfWeek = 1) Then
                'Monday

            ElseIf (monMonday1Week.DayOfWeek = 2) Then
                'Tuesday
                monMonday1Week = monMonday1Week.AddDays(-1)
            ElseIf (monMonday1Week.DayOfWeek = 3) Then
                'Wednesday
                monMonday1Week = monMonday1Week.AddDays(-2)
            ElseIf (monMonday1Week.DayOfWeek = 4) Then
                'Thursday
                monMonday1Week = monMonday1Week.AddDays(-3)
            ElseIf (monMonday1Week.DayOfWeek = 5) Then
                'Friday
                monMonday1Week = monMonday1Week.AddDays(-4)
            ElseIf (monMonday1Week.DayOfWeek = 6) Then
                'Saturday
                monMonday1Week = monMonday1Week.AddDays(-5)
            End If

            Dim monSunday1Week As New System.DateTime(strYear, strMon, strDay, 0, 0, 0)
            monSunday1Week = monMonday1Week.AddDays(6)

            Dim monMonday2Week As New System.DateTime(strYear, strMon, strDay, 0, 0, 0)
            Dim monSunday2Week As New System.DateTime(strYear, strMon, strDay, 0, 0, 0)
            monMonday2Week = monSunday1Week.AddDays(1)
            monSunday2Week = monMonday2Week.AddDays(6)

            Dim monMonday3Week As New System.DateTime(strYear, strMon, strDay, 0, 0, 0)
            Dim monSunday3Week As New System.DateTime(strYear, strMon, strDay, 0, 0, 0)
            monMonday3Week = monSunday2Week.AddDays(1)
            monSunday3Week = monMonday3Week.AddDays(6)

            Dim monMonday4Week As New System.DateTime(strYear, strMon, strDay, 0, 0, 0)
            Dim monSunday4Week As New System.DateTime(strYear, strMon, strDay, 0, 0, 0)
            monMonday4Week = monSunday3Week.AddDays(1)
            monSunday4Week = monMonday4Week.AddDays(6)

            Dim monMonday5Week As New System.DateTime(strYear, strMon, strDay, 0, 0, 0)
            Dim monSunday5Week As New System.DateTime(strYear, strMon, strDay, 0, 0, 0)

            Dim LastDayOfMonth As Integer
            LastDayOfMonth = System.DateTime.DaysInMonth(monMonday4Week.Year, monMonday4Week.Month)

            Dim blWeek5 As Boolean = False

            'Verify that date is more than from current date 
            If monSunday4Week.Day > monMonday4Week.Day Then
                If monSunday4Week.Day = LastDayOfMonth Then
                    'No need Next date
                ElseIf monSunday4Week.Day < LastDayOfMonth Then
                    monMonday5Week = monSunday4Week.AddDays(1)
                    monSunday5Week = monMonday5Week.AddDays(6)
                    blWeek5 = True
                End If
            End If

            Dim monMonday6Week As New System.DateTime(strYear, strMon, strDay, 0, 0, 0)
            Dim monSunday6Week As New System.DateTime(strYear, strMon, strDay, 0, 0, 0)
            'lblText.Text = monSunday5Week.Day & " < " & LastDayOfMonth
            Dim blWeek6 As Boolean = False
            If monSunday5Week.Day > monMonday5Week.Day Then

                If monSunday5Week.Day = LastDayOfMonth Then
                    'No need Next date
                ElseIf monSunday5Week.Day < LastDayOfMonth Then
                    monMonday6Week = monSunday5Week.AddDays(1)
                    monSunday6Week = monMonday6Week.AddDays(6)
                    blWeek6 = True
                End If
            End If

            'For all ssc total 
            Dim SscTotals(6, 31) As String

            ''2nd Month
            'lblLoc.Text = "1st Week From: " & monMonday1Week & "  To: " & monSunday1Week
            'lblLoc.Text = lblLoc.Text & "<br>2nd Week From: " & monMonday2Week & "  To: " & monSunday2Week
            'lblLoc.Text = lblLoc.Text & "<br>3rd Week From: " & monMonday3Week & "  To: " & monSunday3Week
            'lblLoc.Text = lblLoc.Text & "<br>4th  Week From: " & monMonday4Week & "  To: " & monSunday4Week
            'If blWeek5 Then
            '    lblLoc.Text = lblLoc.Text & "<br>5th  Week From: " & monMonday5Week & "  To: " & monSunday5Week
            'End If
            'If blWeek6 Then
            '    lblLoc.Text = lblLoc.Text & "<br>6th  Week From: " & monMonday6Week & "  To: " & monSunday6Week
            'End If
            Dim row0 As String = ""

            If DropListLocation.SelectedItem.Text = "ALL" Then
                row0 = ""
            Else
                row0 = DropListLocation.SelectedItem.Text
            End If
            Dim rowTitle As String = " "
            Dim rowNumberOfCounters As String = "Number of counters"
            Dim rowNumberOfBusinessDat As String = "Number of Business dat"
            Dim rowNumberOfStaffs As String = "Number of Staffs"

            Dim rowCustomerVisit As String = "Customer Visit"
            Dim rowCallRegistered As String = "Call Registered"
            Dim rowRepairCompleted As String = "Repair Completed"
            Dim rowGoodsDelivered As String = "Goods Delivered"
            Dim rowPending As String = "Pending"
            Dim rowWarranty As String = "Warranty"
            Dim rowInWarranty As String = "InWarranty"
            Dim rowOutWarranty As String = "OutWarranty"
            Dim rowInWarrantyAmount As String = "InWarrantyAmount"
            Dim rowInWarrantyLabour As String = "InWarrantyLabour"
            Dim rowInWarrantyParts As String = "InWarrantyParts"
            Dim rowInWarrantyTransport As String = "InWarrantyTransport"
            Dim rowInWarrantyOthers As String = "InWarrantyOthers"
            Dim rowOutWarrantyAmount As String = "OutWarrantyAmount"
            Dim rowOutWarrantyLabour As String = "OutWarrantyLabour"
            Dim rowOutWarrantyParts As String = "OutWarrantyParts"
            Dim rowOutWarrantyTransport As String = "OutWarrantyTransport"
            Dim rowOutWarrantyOthers As String = "OutWarrantyOthers"
            Dim rowSawDiscountLabour As String = "SawDiscountLabour"
            Dim rowSawDiscountParts As String = "SawDiscountParts"
            Dim rowOutWarrantyLabourwSawDisc As String = "OutWarrantyLabourwSawDisc"
            Dim rowOutWarrantyPartswSawDisc As String = "OutWarrantyPartswSawDisc"
            Dim rowRevenueWithoutTax As String = "RevenueWithoutTax"
            Dim rowIwPartsConsumed As String = "IwPartsConsumed"
            Dim rowTotalPartsConsumed As String = "OutTotalPartsConsumed"
            Dim rowPrimeCostTotal As String = "PrimeCostTotal"
            Dim rowGrossProfit As String = "GrossProfit"
            Dim rowFinalPercentage As String = " "


            For Each item As ListItem In DropListLocation.Items
                If DropListLocation.SelectedItem.Text = "ALL" Then

                    If item.Text = "ALL" Then 'Skip 1st List when  "ALL" in the loop
                        Continue For
                    Else
                        'monMonday1Week & monSunday1Week
                        strDay = monMonday1Week.Day()
                        strMon = monMonday1Week.Month
                        strYear = monMonday1Week.Year

                        If Len(strDay) <= 1 Then
                            strDay = "0" & strDay
                        End If
                        If Len(strMon) <= 1 Then
                            strMon = "0" & strMon
                        End If
                        strDate = strYear & strMon & strDay
                        'Read from 
                        _dtPlReprt = comcontrol.SelectPlReport(monMonday1Week, monSunday1Week, item.Text, item.Value, Gm)

                        If (_dtPlReprt Is Nothing) Or (_dtPlReprt.Rows.Count = 0) Then
                        Else
                            For Each row As DataRow In _dtPlReprt.Rows
                                intNumberOfCounters = row.Item("NumberOfCounters")
                                intNumberOfBusinessDat = row.Item("NumberOfBusinessDat")
                                intNumberOfStaffs = row.Item("NumberOfStaffs")

                                intCustomerVisit = row.Item("CustomerVisit")
                                intCallRegistered = row.Item("CallRegistered")
                                intRepairCompleted = row.Item("RepairCompleted")
                                intGoodsDelivered = row.Item("GoodsDelivered")
                                intPending = row.Item("Pending")
                                intWarranty = row.Item("Warranty")
                                intInWarranty = row.Item("InWarranty")
                                intOutWarranty = row.Item("OutWarranty")
                                decInWarrantyAmount = row.Item("InWarrantyAmount")
                                decInWarrantyLabour = row.Item("InWarrantyLabour")
                                decInWarrantyParts = row.Item("InWarrantyParts")
                                decInWarrantyTransport = row.Item("InWarrantyTransport")
                                decInWarrantyOthers = row.Item("InWarrantyOthers")
                                decOutWarrantyAmount = row.Item("OutWarrantyAmount")
                                decOutWarrantyLabour = row.Item("OutWarrantyLabour")
                                decOutWarrantyParts = row.Item("OutWarrantyParts")
                                decOutWarrantyTransport = row.Item("OutWarrantyTransport")
                                decOutWarrantyOthers = row.Item("OutWarrantyOthers")
                                decSawDiscountLabour = row.Item("SawDiscountLabour")
                                decSawDiscountParts = row.Item("SawDiscountParts")
                                decOutWarrantyLabourwSawDisc = row.Item("OutWarrantyLabourwSawDisc")
                                decOutWarrantyPartswSawDisc = row.Item("OutWarrantyPartswSawDisc")
                                decRevenueWithoutTax = row.Item("RevenueWithoutTax")
                                decIwPartsConsumed = row.Item("IwPartsConsumed")
                                decTotalPartsConsumed = row.Item("TotalPartsConsumed")
                                decPrimeCostTotal = row.Item("PrimeCostTotal")
                                decGrossProfit = row.Item("GrossProfit")
                                decFinalPercentage = row.Item("FinalPercentage")

                                intNumberOfCountersTot = intNumberOfCountersTot + intNumberOfCounters
                                intNumberOfBusinessDatTot = intNumberOfBusinessDatTot + intNumberOfBusinessDat
                                intNumberOfStaffsTot = intNumberOfStaffsTot + intNumberOfStaffs

                                intCustomerVisitTot = intCustomerVisitTot + intCustomerVisit
                                intCallRegisteredTot = intCallRegisteredTot + intCallRegistered
                                intRepairCompletedTot = intRepairCompletedTot + intRepairCompleted
                                intGoodsDeliveredTot = intGoodsDeliveredTot + intGoodsDelivered
                                intPendingTot = intPendingTot + intPending
                                intWarrantyTot = intWarrantyTot + intWarranty
                                intInWarrantyTot = intInWarrantyTot + intInWarranty
                                intOutWarrantyTot = intOutWarrantyTot + intOutWarranty
                                decInWarrantyAmountTot = decInWarrantyAmountTot + decInWarrantyAmount
                                decInWarrantyLabourTot = decInWarrantyLabourTot + decInWarrantyLabour
                                decInWarrantyPartsTot = decInWarrantyPartsTot + decInWarrantyParts
                                decInWarrantyTransportTot = decInWarrantyTransportTot + decInWarrantyTransport
                                decInWarrantyOthersTot = decInWarrantyOthersTot + decInWarrantyOthers
                                decOutWarrantyAmountTot = decOutWarrantyAmountTot + decOutWarrantyAmount
                                decOutWarrantyLabourTot = decOutWarrantyLabourTot + decOutWarrantyLabour
                                decOutWarrantyPartsTot = decOutWarrantyPartsTot + decOutWarrantyParts
                                decOutWarrantyTransportTot = decOutWarrantyTransportTot + decOutWarrantyTransport
                                decOutWarrantyOthersTot = decOutWarrantyOthersTot + decOutWarrantyOthers
                                decSawDiscountLabourTot = decSawDiscountLabourTot + decSawDiscountLabour
                                decSawDiscountPartsTot = decSawDiscountPartsTot + decSawDiscountParts
                                decOutWarrantyLabourwSawDiscTot = decOutWarrantyLabourwSawDiscTot + decOutWarrantyLabourwSawDisc
                                decOutWarrantyPartswSawDiscTot = decOutWarrantyPartswSawDiscTot + decOutWarrantyPartswSawDisc
                                decRevenueWithoutTaxTot = decRevenueWithoutTaxTot + decRevenueWithoutTax
                                decIwPartsConsumedTot = decIwPartsConsumedTot + decIwPartsConsumed
                                decTotalPartsConsumedTot = decTotalPartsConsumedTot + decTotalPartsConsumed
                                decPrimeCostTotalTot = decPrimeCostTotalTot + decPrimeCostTotal
                                decGrossProfitTot = decGrossProfitTot + decGrossProfit
                                decFinalPercentageTot = decFinalPercentageTot + decFinalPercentage

                                row0 = row0 & "," & item.Text
                                rowTitle = rowTitle & "," & strDate

                                rowNumberOfCounters = rowNumberOfCounters & "," '& intNumberOfCounters
                                rowNumberOfBusinessDat = rowNumberOfBusinessDat & "," '& intNumberOfBusinessDat
                                rowNumberOfStaffs = rowNumberOfStaffs & "," '& intNumberOfStaffs

                                rowCustomerVisit = rowCustomerVisit & "," & intCustomerVisit
                                rowCallRegistered = rowCallRegistered & "," & intCallRegistered
                                rowRepairCompleted = rowRepairCompleted & "," & intRepairCompleted
                                rowGoodsDelivered = rowGoodsDelivered & "," & intGoodsDelivered
                                rowPending = rowPending & "," & intPending
                                rowWarranty = rowWarranty & "," & intWarranty
                                rowInWarranty = rowInWarranty & "," & intInWarranty
                                rowOutWarranty = rowOutWarranty & "," & intOutWarranty
                                rowInWarrantyAmount = rowInWarrantyAmount & "," & decInWarrantyAmount
                                rowInWarrantyLabour = rowInWarrantyLabour & "," & decInWarrantyLabour
                                rowInWarrantyParts = rowInWarrantyParts & "," & decInWarrantyParts
                                rowInWarrantyTransport = rowInWarrantyTransport & "," & decInWarrantyTransport
                                rowInWarrantyOthers = rowInWarrantyOthers & "," & decInWarrantyOthers
                                rowOutWarrantyAmount = rowOutWarrantyAmount & "," & decOutWarrantyAmount
                                rowOutWarrantyLabour = rowOutWarrantyLabour & "," & decOutWarrantyLabour
                                rowOutWarrantyParts = rowOutWarrantyParts & "," & decOutWarrantyParts
                                rowOutWarrantyTransport = rowOutWarrantyTransport & "," & decOutWarrantyTransport
                                rowOutWarrantyOthers = rowOutWarrantyOthers & "," & decOutWarrantyOthers
                                rowSawDiscountLabour = rowSawDiscountLabour & "," & decSawDiscountLabour
                                rowSawDiscountParts = rowSawDiscountParts & "," & decSawDiscountParts
                                rowOutWarrantyLabourwSawDisc = rowOutWarrantyLabourwSawDisc & "," & decOutWarrantyLabourwSawDisc
                                rowOutWarrantyPartswSawDisc = rowOutWarrantyPartswSawDisc & "," & decOutWarrantyPartswSawDisc
                                rowRevenueWithoutTax = rowRevenueWithoutTax & "," & decRevenueWithoutTax
                                rowIwPartsConsumed = rowIwPartsConsumed & "," & decIwPartsConsumed
                                rowTotalPartsConsumed = rowTotalPartsConsumed & "," & decTotalPartsConsumed
                                rowPrimeCostTotal = rowPrimeCostTotal & "," & decPrimeCostTotal
                                rowGrossProfit = rowGrossProfit & "," & decGrossProfit
                                'rowFinalPercentage = rowFinalPercentage & "," & decFinalPercentage
                                If decRevenueWithoutTax = 0 Then
                                    FinalPercentage1 = decGrossProfit
                                Else
                                    FinalPercentage1 = comcontrol.Money_Round((decGrossProfit / decRevenueWithoutTax) * 100, 2)
                                End If
                                rowFinalPercentage = rowFinalPercentage & "," & FinalPercentage1 & "%" 'decFinalPercentage / 100


                                'For Adding Final Report 
                                SscTotals(0, 0) = strDate 'Date

                                SscTotals(0, 1) = SscTotals(0, 1) + intNumberOfCounters 'rowNumberOfCounters
                                SscTotals(0, 2) = SscTotals(0, 2) + intNumberOfBusinessDat 'rowNumberOfBusinessDat
                                SscTotals(0, 3) = SscTotals(0, 3) + intNumberOfStaffs 'rowNumberOfStaffs

                                SscTotals(0, 4) = SscTotals(0, 4) + intCustomerVisit 'rowCustomerVisit
                                SscTotals(0, 5) = SscTotals(0, 5) + intCallRegistered 'rowCallRegistered
                                SscTotals(0, 6) = SscTotals(0, 6) + intRepairCompleted 'rowRepairCompleted
                                SscTotals(0, 7) = SscTotals(0, 7) + intGoodsDelivered 'rowGoodsDelivered
                                SscTotals(0, 8) = SscTotals(0, 8) + intPending 'rowPending
                                SscTotals(0, 9) = SscTotals(0, 9) + intWarranty 'rowWarranty
                                SscTotals(0, 10) = SscTotals(0, 10) + intInWarranty 'rowInWarranty
                                SscTotals(0, 11) = SscTotals(0, 11) + intOutWarranty 'rowOutWarranty
                                SscTotals(0, 12) = SscTotals(0, 12) + decInWarrantyAmount 'rowInWarrantyAmount
                                SscTotals(0, 13) = SscTotals(0, 13) + decInWarrantyLabour 'rowInWarrantyLabour
                                SscTotals(0, 14) = SscTotals(0, 14) + decInWarrantyParts 'rowInWarrantyParts
                                SscTotals(0, 15) = SscTotals(0, 15) + decInWarrantyTransport 'rowInWarrantyTransport
                                SscTotals(0, 16) = SscTotals(0, 16) + decInWarrantyOthers 'rowInWarrantyOthers
                                SscTotals(0, 17) = SscTotals(0, 17) + decOutWarrantyAmount 'rowOutWarrantyAmount
                                SscTotals(0, 18) = SscTotals(0, 18) + decOutWarrantyLabour 'rowOutWarrantyLabour
                                SscTotals(0, 19) = SscTotals(0, 19) + decOutWarrantyParts 'rowOutWarrantyParts
                                SscTotals(0, 20) = SscTotals(0, 20) + decOutWarrantyTransport 'rowOutWarrantyTransport
                                SscTotals(0, 21) = SscTotals(0, 21) + decOutWarrantyOthers 'rowOutWarrantyOthers
                                SscTotals(0, 22) = SscTotals(0, 22) + decSawDiscountLabour 'rowSawDiscountLabour
                                SscTotals(0, 23) = SscTotals(0, 23) + decSawDiscountParts 'rowSawDiscountParts
                                SscTotals(0, 24) = SscTotals(0, 24) + decOutWarrantyLabourwSawDisc 'rowOutWarrantyLabourwSawDisc
                                SscTotals(0, 25) = SscTotals(0, 25) + decOutWarrantyPartswSawDisc 'rowOutWarrantyPartswSawDisc
                                SscTotals(0, 26) = SscTotals(0, 26) + decRevenueWithoutTax 'rowRevenueWithoutTax
                                SscTotals(0, 27) = SscTotals(0, 27) + decIwPartsConsumed 'rowIwPartsConsumed
                                SscTotals(0, 28) = SscTotals(0, 28) + decTotalPartsConsumed 'rowTotalPartsConsumed
                                SscTotals(0, 29) = SscTotals(0, 29) + decPrimeCostTotal 'rowPrimeCostTotal
                                SscTotals(0, 30) = SscTotals(0, 30) + decGrossProfit 'rowGrossProfit 
                                SscTotals(0, 31) = SscTotals(0, 31) + decFinalPercentage 'rowFinalPercentage 



                            Next row
                        End If

                        'monMonday2Week & monSunday2Week
                        strDay = monMonday2Week.Day()
                        strMon = monMonday2Week.Month
                        strYear = monMonday2Week.Year

                        If Len(strDay) <= 1 Then
                            strDay = "0" & strDay
                        End If
                        If Len(strMon) <= 1 Then
                            strMon = "0" & strMon
                        End If
                        strDate = strYear & strMon & strDay
                        'Read from 
                        _dtPlReprt = comcontrol.SelectPlReport(monMonday2Week, monSunday2Week, item.Text, item.Value, Gm)
                        If (_dtPlReprt Is Nothing) Or (_dtPlReprt.Rows.Count = 0) Then
                        Else
                            For Each row As DataRow In _dtPlReprt.Rows

                                intNumberOfCounters = row.Item("NumberOfCounters")
                                intNumberOfBusinessDat = row.Item("NumberOfBusinessDat")
                                intNumberOfStaffs = row.Item("NumberOfStaffs")

                                intCustomerVisit = row.Item("CustomerVisit")
                                intCallRegistered = row.Item("CallRegistered")
                                intRepairCompleted = row.Item("RepairCompleted")
                                intGoodsDelivered = row.Item("GoodsDelivered")
                                intPending = row.Item("Pending")
                                intWarranty = row.Item("Warranty")
                                intInWarranty = row.Item("InWarranty")
                                intOutWarranty = row.Item("OutWarranty")
                                decInWarrantyAmount = row.Item("InWarrantyAmount")
                                decInWarrantyLabour = row.Item("InWarrantyLabour")
                                decInWarrantyParts = row.Item("InWarrantyParts")
                                decInWarrantyTransport = row.Item("InWarrantyTransport")
                                decInWarrantyOthers = row.Item("InWarrantyOthers")
                                decOutWarrantyAmount = row.Item("OutWarrantyAmount")
                                decOutWarrantyLabour = row.Item("OutWarrantyLabour")
                                decOutWarrantyParts = row.Item("OutWarrantyParts")
                                decOutWarrantyTransport = row.Item("OutWarrantyTransport")
                                decOutWarrantyOthers = row.Item("OutWarrantyOthers")
                                decSawDiscountLabour = row.Item("SawDiscountLabour")
                                decSawDiscountParts = row.Item("SawDiscountParts")
                                decOutWarrantyLabourwSawDisc = row.Item("OutWarrantyLabourwSawDisc")
                                decOutWarrantyPartswSawDisc = row.Item("OutWarrantyPartswSawDisc")
                                decRevenueWithoutTax = row.Item("RevenueWithoutTax")
                                decIwPartsConsumed = row.Item("IwPartsConsumed")
                                decTotalPartsConsumed = row.Item("TotalPartsConsumed")
                                decPrimeCostTotal = row.Item("PrimeCostTotal")
                                decGrossProfit = row.Item("GrossProfit")
                                decFinalPercentage = row.Item("FinalPercentage")


                                intNumberOfCountersTot = intNumberOfCountersTot + intNumberOfCounters
                                intNumberOfBusinessDatTot = intNumberOfBusinessDatTot + intNumberOfBusinessDat
                                intNumberOfStaffsTot = intNumberOfStaffsTot + intNumberOfStaffs

                                intCustomerVisitTot = intCustomerVisitTot + intCustomerVisit
                                intCallRegisteredTot = intCallRegisteredTot + intCallRegistered
                                intRepairCompletedTot = intRepairCompletedTot + intRepairCompleted
                                intGoodsDeliveredTot = intGoodsDeliveredTot + intGoodsDelivered
                                intPendingTot = intPendingTot + intPending
                                intWarrantyTot = intWarrantyTot + intWarranty
                                intInWarrantyTot = intInWarrantyTot + intInWarranty
                                intOutWarrantyTot = intOutWarrantyTot + intOutWarranty
                                decInWarrantyAmountTot = decInWarrantyAmountTot + decInWarrantyAmount
                                decInWarrantyLabourTot = decInWarrantyLabourTot + decInWarrantyLabour
                                decInWarrantyPartsTot = decInWarrantyPartsTot + decInWarrantyParts
                                decInWarrantyTransportTot = decInWarrantyTransportTot + decInWarrantyTransport
                                decInWarrantyOthersTot = decInWarrantyOthersTot + decInWarrantyOthers
                                decOutWarrantyAmountTot = decOutWarrantyAmountTot + decOutWarrantyAmount
                                decOutWarrantyLabourTot = decOutWarrantyLabourTot + decOutWarrantyLabour
                                decOutWarrantyPartsTot = decOutWarrantyPartsTot + decOutWarrantyParts
                                decOutWarrantyTransportTot = decOutWarrantyTransportTot + decOutWarrantyTransport
                                decOutWarrantyOthersTot = decOutWarrantyOthersTot + decOutWarrantyOthers
                                decSawDiscountLabourTot = decSawDiscountLabourTot + decSawDiscountLabour
                                decSawDiscountPartsTot = decSawDiscountPartsTot + decSawDiscountParts
                                decOutWarrantyLabourwSawDiscTot = decOutWarrantyLabourwSawDiscTot + decOutWarrantyLabourwSawDisc
                                decOutWarrantyPartswSawDiscTot = decOutWarrantyPartswSawDiscTot + decOutWarrantyPartswSawDisc
                                decRevenueWithoutTaxTot = decRevenueWithoutTaxTot + decRevenueWithoutTax
                                decIwPartsConsumedTot = decIwPartsConsumedTot + decIwPartsConsumed
                                decTotalPartsConsumedTot = decTotalPartsConsumedTot + decTotalPartsConsumed
                                decPrimeCostTotalTot = decPrimeCostTotalTot + decPrimeCostTotal
                                decGrossProfitTot = decGrossProfitTot + decGrossProfit
                                decFinalPercentageTot = decFinalPercentageTot + decFinalPercentage

                                row0 = row0 & "," & item.Text
                                rowTitle = rowTitle & "," & strDate

                                rowNumberOfCounters = rowNumberOfCounters & "," '& intNumberOfCounters
                                rowNumberOfBusinessDat = rowNumberOfBusinessDat & "," '& intNumberOfBusinessDat
                                rowNumberOfStaffs = rowNumberOfStaffs & "," '& intNumberOfStaffs

                                rowCustomerVisit = rowCustomerVisit & "," & intCustomerVisit
                                rowCallRegistered = rowCallRegistered & "," & intCallRegistered
                                rowRepairCompleted = rowRepairCompleted & "," & intRepairCompleted
                                rowGoodsDelivered = rowGoodsDelivered & "," & intGoodsDelivered
                                rowPending = rowPending & "," & intPending
                                rowWarranty = rowWarranty & "," & intWarranty
                                rowInWarranty = rowInWarranty & "," & intInWarranty
                                rowOutWarranty = rowOutWarranty & "," & intOutWarranty
                                rowInWarrantyAmount = rowInWarrantyAmount & "," & decInWarrantyAmount
                                rowInWarrantyLabour = rowInWarrantyLabour & "," & decInWarrantyLabour
                                rowInWarrantyParts = rowInWarrantyParts & "," & decInWarrantyParts
                                rowInWarrantyTransport = rowInWarrantyTransport & "," & decInWarrantyTransport
                                rowInWarrantyOthers = rowInWarrantyOthers & "," & decInWarrantyOthers
                                rowOutWarrantyAmount = rowOutWarrantyAmount & "," & decOutWarrantyAmount
                                rowOutWarrantyLabour = rowOutWarrantyLabour & "," & decOutWarrantyLabour
                                rowOutWarrantyParts = rowOutWarrantyParts & "," & decOutWarrantyParts
                                rowOutWarrantyTransport = rowOutWarrantyTransport & "," & decOutWarrantyTransport
                                rowOutWarrantyOthers = rowOutWarrantyOthers & "," & decOutWarrantyOthers
                                rowSawDiscountLabour = rowSawDiscountLabour & "," & decSawDiscountLabour
                                rowSawDiscountParts = rowSawDiscountParts & "," & decSawDiscountParts
                                rowOutWarrantyLabourwSawDisc = rowOutWarrantyLabourwSawDisc & "," & decOutWarrantyLabourwSawDisc
                                rowOutWarrantyPartswSawDisc = rowOutWarrantyPartswSawDisc & "," & decOutWarrantyPartswSawDisc
                                rowRevenueWithoutTax = rowRevenueWithoutTax & "," & decRevenueWithoutTax
                                rowIwPartsConsumed = rowIwPartsConsumed & "," & decIwPartsConsumed
                                rowTotalPartsConsumed = rowTotalPartsConsumed & "," & decTotalPartsConsumed
                                rowPrimeCostTotal = rowPrimeCostTotal & "," & decPrimeCostTotal
                                rowGrossProfit = rowGrossProfit & "," & decGrossProfit
                                'rowFinalPercentage = rowFinalPercentage & "," & decFinalPercentage
                                If decRevenueWithoutTax = 0 Then
                                    FinalPercentage1 = decGrossProfit
                                Else
                                    FinalPercentage1 = comcontrol.Money_Round((decGrossProfit / decRevenueWithoutTax) * 100, 2)
                                End If
                                rowFinalPercentage = rowFinalPercentage & "," & FinalPercentage1 & "%" 'decFinalPercentage / 100


                                'For Adding Final Report 
                                SscTotals(1, 0) = strDate 'Date

                                SscTotals(1, 1) = SscTotals(1, 1) + intNumberOfCounters 'rowNumberOfCounters
                                SscTotals(1, 2) = SscTotals(1, 2) + intNumberOfBusinessDat 'rowNumberOfBusinessDat
                                SscTotals(1, 3) = SscTotals(1, 3) + intNumberOfStaffs 'rowNumberOfStaffs

                                SscTotals(1, 4) = SscTotals(1, 4) + intCustomerVisit 'rowCustomerVisit
                                SscTotals(1, 5) = SscTotals(1, 5) + intCallRegistered 'rowCallRegistered
                                SscTotals(1, 6) = SscTotals(1, 6) + intRepairCompleted 'rowRepairCompleted
                                SscTotals(1, 7) = SscTotals(1, 7) + intGoodsDelivered 'rowGoodsDelivered
                                SscTotals(1, 8) = SscTotals(1, 8) + intPending 'rowPending
                                SscTotals(1, 9) = SscTotals(1, 9) + intWarranty 'rowWarranty
                                SscTotals(1, 10) = SscTotals(1, 10) + intInWarranty 'rowInWarranty
                                SscTotals(1, 11) = SscTotals(1, 11) + intOutWarranty 'rowOutWarranty
                                SscTotals(1, 12) = SscTotals(1, 12) + decInWarrantyAmount 'rowInWarrantyAmount
                                SscTotals(1, 13) = SscTotals(1, 13) + decInWarrantyLabour 'rowInWarrantyLabour
                                SscTotals(1, 14) = SscTotals(1, 14) + decInWarrantyParts 'rowInWarrantyParts
                                SscTotals(1, 15) = SscTotals(1, 15) + decInWarrantyTransport 'rowInWarrantyTransport
                                SscTotals(1, 16) = SscTotals(1, 16) + decInWarrantyOthers 'rowInWarrantyOthers
                                SscTotals(1, 17) = SscTotals(1, 17) + decOutWarrantyAmount 'rowOutWarrantyAmount
                                SscTotals(1, 18) = SscTotals(1, 18) + decOutWarrantyLabour 'rowOutWarrantyLabour
                                SscTotals(1, 19) = SscTotals(1, 19) + decOutWarrantyParts 'rowOutWarrantyParts
                                SscTotals(1, 20) = SscTotals(1, 20) + decOutWarrantyTransport 'rowOutWarrantyTransport
                                SscTotals(1, 21) = SscTotals(1, 21) + decOutWarrantyOthers 'rowOutWarrantyOthers
                                SscTotals(1, 22) = SscTotals(1, 22) + decSawDiscountLabour 'rowSawDiscountLabour
                                SscTotals(1, 23) = SscTotals(1, 23) + decSawDiscountParts 'rowSawDiscountParts
                                SscTotals(1, 24) = SscTotals(1, 24) + decOutWarrantyLabourwSawDisc 'rowOutWarrantyLabourwSawDisc
                                SscTotals(1, 25) = SscTotals(1, 25) + decOutWarrantyPartswSawDisc 'rowOutWarrantyPartswSawDisc
                                SscTotals(1, 26) = SscTotals(1, 26) + decRevenueWithoutTax 'rowRevenueWithoutTax
                                SscTotals(1, 27) = SscTotals(1, 27) + decIwPartsConsumed 'rowIwPartsConsumed
                                SscTotals(1, 28) = SscTotals(1, 28) + decTotalPartsConsumed 'rowTotalPartsConsumed
                                SscTotals(1, 29) = SscTotals(1, 29) + decPrimeCostTotal 'rowPrimeCostTotal
                                SscTotals(1, 30) = SscTotals(1, 30) + decGrossProfit 'rowGrossProfit
                                SscTotals(1, 31) = SscTotals(1, 31) + decFinalPercentage 'rowFinalPercentage


                            Next row
                        End If



                        'monMonday3Week & monSunday3Week
                        strDay = monMonday3Week.Day()
                        strMon = monMonday3Week.Month
                        strYear = monMonday3Week.Year

                        If Len(strDay) <= 1 Then
                            strDay = "0" & strDay
                        End If
                        If Len(strMon) <= 1 Then
                            strMon = "0" & strMon
                        End If
                        strDate = strYear & strMon & strDay
                        'Read from 
                        _dtPlReprt = comcontrol.SelectPlReport(monMonday3Week, monSunday3Week, item.Text, item.Value, Gm)
                        If (_dtPlReprt Is Nothing) Or (_dtPlReprt.Rows.Count = 0) Then
                        Else
                            For Each row As DataRow In _dtPlReprt.Rows

                                intNumberOfCounters = row.Item("NumberOfCounters")
                                intNumberOfBusinessDat = row.Item("NumberOfBusinessDat")
                                intNumberOfStaffs = row.Item("NumberOfStaffs")

                                intCustomerVisit = row.Item("CustomerVisit")
                                intCallRegistered = row.Item("CallRegistered")
                                intRepairCompleted = row.Item("RepairCompleted")
                                intGoodsDelivered = row.Item("GoodsDelivered")
                                intPending = row.Item("Pending")
                                intWarranty = row.Item("Warranty")
                                intInWarranty = row.Item("InWarranty")
                                intOutWarranty = row.Item("OutWarranty")
                                decInWarrantyAmount = row.Item("InWarrantyAmount")
                                decInWarrantyLabour = row.Item("InWarrantyLabour")
                                decInWarrantyParts = row.Item("InWarrantyParts")
                                decInWarrantyTransport = row.Item("InWarrantyTransport")
                                decInWarrantyOthers = row.Item("InWarrantyOthers")
                                decOutWarrantyAmount = row.Item("OutWarrantyAmount")
                                decOutWarrantyLabour = row.Item("OutWarrantyLabour")
                                decOutWarrantyParts = row.Item("OutWarrantyParts")
                                decOutWarrantyTransport = row.Item("OutWarrantyTransport")
                                decOutWarrantyOthers = row.Item("OutWarrantyOthers")
                                decSawDiscountLabour = row.Item("SawDiscountLabour")
                                decSawDiscountParts = row.Item("SawDiscountParts")
                                decOutWarrantyLabourwSawDisc = row.Item("OutWarrantyLabourwSawDisc")
                                decOutWarrantyPartswSawDisc = row.Item("OutWarrantyPartswSawDisc")
                                decRevenueWithoutTax = row.Item("RevenueWithoutTax")
                                decIwPartsConsumed = row.Item("IwPartsConsumed")
                                decTotalPartsConsumed = row.Item("TotalPartsConsumed")
                                decPrimeCostTotal = row.Item("PrimeCostTotal")
                                decGrossProfit = row.Item("GrossProfit")
                                decFinalPercentage = row.Item("FinalPercentage")

                                intNumberOfCountersTot = intNumberOfCountersTot + intNumberOfCounters
                                intNumberOfBusinessDatTot = intNumberOfBusinessDatTot + intNumberOfBusinessDat
                                intNumberOfStaffsTot = intNumberOfStaffsTot + intNumberOfStaffs

                                intCustomerVisitTot = intCustomerVisitTot + intCustomerVisit
                                intCallRegisteredTot = intCallRegisteredTot + intCallRegistered
                                intRepairCompletedTot = intRepairCompletedTot + intRepairCompleted
                                intGoodsDeliveredTot = intGoodsDeliveredTot + intGoodsDelivered
                                intPendingTot = intPendingTot + intPending
                                intWarrantyTot = intWarrantyTot + intWarranty
                                intInWarrantyTot = intInWarrantyTot + intInWarranty
                                intOutWarrantyTot = intOutWarrantyTot + intOutWarranty
                                decInWarrantyAmountTot = decInWarrantyAmountTot + decInWarrantyAmount
                                decInWarrantyLabourTot = decInWarrantyLabourTot + decInWarrantyLabour
                                decInWarrantyPartsTot = decInWarrantyPartsTot + decInWarrantyParts
                                decInWarrantyTransportTot = decInWarrantyTransportTot + decInWarrantyTransport
                                decInWarrantyOthersTot = decInWarrantyOthersTot + decInWarrantyOthers
                                decOutWarrantyAmountTot = decOutWarrantyAmountTot + decOutWarrantyAmount
                                decOutWarrantyLabourTot = decOutWarrantyLabourTot + decOutWarrantyLabour
                                decOutWarrantyPartsTot = decOutWarrantyPartsTot + decOutWarrantyParts
                                decOutWarrantyTransportTot = decOutWarrantyTransportTot + decOutWarrantyTransport
                                decOutWarrantyOthersTot = decOutWarrantyOthersTot + decOutWarrantyOthers
                                decSawDiscountLabourTot = decSawDiscountLabourTot + decSawDiscountLabour
                                decSawDiscountPartsTot = decSawDiscountPartsTot + decSawDiscountParts
                                decOutWarrantyLabourwSawDiscTot = decOutWarrantyLabourwSawDiscTot + decOutWarrantyLabourwSawDisc
                                decOutWarrantyPartswSawDiscTot = decOutWarrantyPartswSawDiscTot + decOutWarrantyPartswSawDisc
                                decRevenueWithoutTaxTot = decRevenueWithoutTaxTot + decRevenueWithoutTax
                                decIwPartsConsumedTot = decIwPartsConsumedTot + decIwPartsConsumed
                                decTotalPartsConsumedTot = decTotalPartsConsumedTot + decTotalPartsConsumed
                                decPrimeCostTotalTot = decPrimeCostTotalTot + decPrimeCostTotal
                                decGrossProfitTot = decGrossProfitTot + decGrossProfit
                                decFinalPercentageTot = decFinalPercentageTot + decFinalPercentage


                                row0 = row0 & "," & item.Text
                                rowTitle = rowTitle & "," & strDate

                                rowNumberOfCounters = rowNumberOfCounters & "," '& intNumberOfCounters
                                rowNumberOfBusinessDat = rowNumberOfBusinessDat & "," '& intNumberOfBusinessDat
                                rowNumberOfStaffs = rowNumberOfStaffs & "," '& intNumberOfStaffs

                                rowCustomerVisit = rowCustomerVisit & "," & intCustomerVisit
                                rowCallRegistered = rowCallRegistered & "," & intCallRegistered
                                rowRepairCompleted = rowRepairCompleted & "," & intRepairCompleted
                                rowGoodsDelivered = rowGoodsDelivered & "," & intGoodsDelivered
                                rowPending = rowPending & "," & intPending
                                rowWarranty = rowWarranty & "," & intWarranty
                                rowInWarranty = rowInWarranty & "," & intInWarranty
                                rowOutWarranty = rowOutWarranty & "," & intOutWarranty
                                rowInWarrantyAmount = rowInWarrantyAmount & "," & decInWarrantyAmount
                                rowInWarrantyLabour = rowInWarrantyLabour & "," & decInWarrantyLabour
                                rowInWarrantyParts = rowInWarrantyParts & "," & decInWarrantyParts
                                rowInWarrantyTransport = rowInWarrantyTransport & "," & decInWarrantyTransport
                                rowInWarrantyOthers = rowInWarrantyOthers & "," & decInWarrantyOthers
                                rowOutWarrantyAmount = rowOutWarrantyAmount & "," & decOutWarrantyAmount
                                rowOutWarrantyLabour = rowOutWarrantyLabour & "," & decOutWarrantyLabour
                                rowOutWarrantyParts = rowOutWarrantyParts & "," & decOutWarrantyParts
                                rowOutWarrantyTransport = rowOutWarrantyTransport & "," & decOutWarrantyTransport
                                rowOutWarrantyOthers = rowOutWarrantyOthers & "," & decOutWarrantyOthers
                                rowSawDiscountLabour = rowSawDiscountLabour & "," & decSawDiscountLabour
                                rowSawDiscountParts = rowSawDiscountParts & "," & decSawDiscountParts
                                rowOutWarrantyLabourwSawDisc = rowOutWarrantyLabourwSawDisc & "," & decOutWarrantyLabourwSawDisc
                                rowOutWarrantyPartswSawDisc = rowOutWarrantyPartswSawDisc & "," & decOutWarrantyPartswSawDisc
                                rowRevenueWithoutTax = rowRevenueWithoutTax & "," & decRevenueWithoutTax
                                rowIwPartsConsumed = rowIwPartsConsumed & "," & decIwPartsConsumed
                                rowTotalPartsConsumed = rowTotalPartsConsumed & "," & decTotalPartsConsumed
                                rowPrimeCostTotal = rowPrimeCostTotal & "," & decPrimeCostTotal
                                rowGrossProfit = rowGrossProfit & "," & decGrossProfit
                                'rowFinalPercentage = rowFinalPercentage & "," & decFinalPercentage
                                If decRevenueWithoutTax = 0 Then
                                    FinalPercentage1 = decGrossProfit
                                Else
                                    FinalPercentage1 = comcontrol.Money_Round((decGrossProfit / decRevenueWithoutTax) * 100, 2)
                                End If
                                rowFinalPercentage = rowFinalPercentage & "," & FinalPercentage1 & "%" 'decFinalPercentage / 100


                                'For Adding Final Report 
                                SscTotals(2, 0) = strDate 'Date

                                SscTotals(2, 1) = SscTotals(2, 1) + intNumberOfCounters 'rowNumberOfCounters
                                SscTotals(2, 2) = SscTotals(2, 2) + intNumberOfBusinessDat 'rowNumberOfBusinessDat
                                SscTotals(2, 3) = SscTotals(2, 3) + intNumberOfStaffs 'rowNumberOfStaffs

                                SscTotals(2, 4) = SscTotals(2, 4) + intCustomerVisit 'rowCustomerVisit
                                SscTotals(2, 5) = SscTotals(2, 5) + intCallRegistered 'rowCallRegistered
                                SscTotals(2, 6) = SscTotals(2, 6) + intRepairCompleted 'rowRepairCompleted
                                SscTotals(2, 7) = SscTotals(2, 7) + intGoodsDelivered 'rowGoodsDelivered
                                SscTotals(2, 8) = SscTotals(2, 8) + intPending 'rowPending
                                SscTotals(2, 9) = SscTotals(2, 9) + intWarranty 'rowWarranty
                                SscTotals(2, 10) = SscTotals(2, 10) + intInWarranty 'rowInWarranty
                                SscTotals(2, 11) = SscTotals(2, 11) + intOutWarranty 'rowOutWarranty
                                SscTotals(2, 12) = SscTotals(2, 12) + decInWarrantyAmount 'rowInWarrantyAmount
                                SscTotals(2, 13) = SscTotals(2, 13) + decInWarrantyLabour 'rowInWarrantyLabour
                                SscTotals(2, 14) = SscTotals(2, 14) + decInWarrantyParts 'rowInWarrantyParts
                                SscTotals(2, 15) = SscTotals(2, 15) + decInWarrantyTransport 'rowInWarrantyTransport
                                SscTotals(2, 16) = SscTotals(2, 16) + decInWarrantyOthers 'rowInWarrantyOthers
                                SscTotals(2, 17) = SscTotals(2, 17) + decOutWarrantyAmount 'rowOutWarrantyAmount
                                SscTotals(2, 18) = SscTotals(2, 18) + decOutWarrantyLabour 'rowOutWarrantyLabour
                                SscTotals(2, 19) = SscTotals(2, 19) + decOutWarrantyParts 'rowOutWarrantyParts
                                SscTotals(2, 20) = SscTotals(2, 20) + decOutWarrantyTransport 'rowOutWarrantyTransport
                                SscTotals(2, 21) = SscTotals(2, 21) + decOutWarrantyOthers 'rowOutWarrantyOthers
                                SscTotals(2, 22) = SscTotals(2, 22) + decSawDiscountLabour 'rowSawDiscountLabour
                                SscTotals(2, 23) = SscTotals(2, 23) + decSawDiscountParts 'rowSawDiscountParts
                                SscTotals(2, 24) = SscTotals(2, 24) + decOutWarrantyLabourwSawDisc 'rowOutWarrantyLabourwSawDisc
                                SscTotals(2, 25) = SscTotals(2, 25) + decOutWarrantyPartswSawDisc 'rowOutWarrantyPartswSawDisc
                                SscTotals(2, 26) = SscTotals(2, 26) + decRevenueWithoutTax 'rowRevenueWithoutTax
                                SscTotals(2, 27) = SscTotals(2, 27) + decIwPartsConsumed 'rowIwPartsConsumed
                                SscTotals(2, 28) = SscTotals(2, 28) + decTotalPartsConsumed 'rowTotalPartsConsumed
                                SscTotals(2, 29) = SscTotals(2, 29) + decPrimeCostTotal 'rowPrimeCostTotal
                                SscTotals(2, 30) = SscTotals(2, 30) + decGrossProfit 'rowGrossProfit 
                                SscTotals(2, 31) = SscTotals(2, 31) + decFinalPercentage 'rowFinalPercentage


                            Next row
                        End If

                        'monMonday4Week & monSunday4Week
                        strDay = monMonday4Week.Day()
                        strMon = monMonday4Week.Month
                        strYear = monMonday4Week.Year

                        If Len(strDay) <= 1 Then
                            strDay = "0" & strDay
                        End If
                        If Len(strMon) <= 1 Then
                            strMon = "0" & strMon
                        End If
                        strDate = strYear & strMon & strDay
                        'Read from 
                        _dtPlReprt = comcontrol.SelectPlReport(monMonday4Week, monSunday4Week, item.Text, item.Value, Gm)
                        If (_dtPlReprt Is Nothing) Or (_dtPlReprt.Rows.Count = 0) Then
                        Else
                            For Each row As DataRow In _dtPlReprt.Rows

                                intNumberOfCounters = row.Item("NumberOfCounters")
                                intNumberOfBusinessDat = row.Item("NumberOfBusinessDat")
                                intNumberOfStaffs = row.Item("NumberOfStaffs")

                                intCustomerVisit = row.Item("CustomerVisit")
                                intCallRegistered = row.Item("CallRegistered")
                                intRepairCompleted = row.Item("RepairCompleted")
                                intGoodsDelivered = row.Item("GoodsDelivered")
                                intPending = row.Item("Pending")
                                intWarranty = row.Item("Warranty")
                                intInWarranty = row.Item("InWarranty")
                                intOutWarranty = row.Item("OutWarranty")
                                decInWarrantyAmount = row.Item("InWarrantyAmount")
                                decInWarrantyLabour = row.Item("InWarrantyLabour")
                                decInWarrantyParts = row.Item("InWarrantyParts")
                                decInWarrantyTransport = row.Item("InWarrantyTransport")
                                decInWarrantyOthers = row.Item("InWarrantyOthers")
                                decOutWarrantyAmount = row.Item("OutWarrantyAmount")
                                decOutWarrantyLabour = row.Item("OutWarrantyLabour")
                                decOutWarrantyParts = row.Item("OutWarrantyParts")
                                decOutWarrantyTransport = row.Item("OutWarrantyTransport")
                                decOutWarrantyOthers = row.Item("OutWarrantyOthers")
                                decSawDiscountLabour = row.Item("SawDiscountLabour")
                                decSawDiscountParts = row.Item("SawDiscountParts")
                                decOutWarrantyLabourwSawDisc = row.Item("OutWarrantyLabourwSawDisc")
                                decOutWarrantyPartswSawDisc = row.Item("OutWarrantyPartswSawDisc")
                                decRevenueWithoutTax = row.Item("RevenueWithoutTax")
                                decIwPartsConsumed = row.Item("IwPartsConsumed")
                                decTotalPartsConsumed = row.Item("TotalPartsConsumed")
                                decPrimeCostTotal = row.Item("PrimeCostTotal")
                                decGrossProfit = row.Item("GrossProfit")
                                decFinalPercentage = row.Item("FinalPercentage")

                                intNumberOfCountersTot = intNumberOfCountersTot + intNumberOfCounters
                                intNumberOfBusinessDatTot = intNumberOfBusinessDatTot + intNumberOfBusinessDat
                                intNumberOfStaffsTot = intNumberOfStaffsTot + intNumberOfStaffs

                                intCustomerVisitTot = intCustomerVisitTot + intCustomerVisit
                                intCallRegisteredTot = intCallRegisteredTot + intCallRegistered
                                intRepairCompletedTot = intRepairCompletedTot + intRepairCompleted
                                intGoodsDeliveredTot = intGoodsDeliveredTot + intGoodsDelivered
                                intPendingTot = intPendingTot + intPending
                                intWarrantyTot = intWarrantyTot + intWarranty
                                intInWarrantyTot = intInWarrantyTot + intInWarranty
                                intOutWarrantyTot = intOutWarrantyTot + intOutWarranty
                                decInWarrantyAmountTot = decInWarrantyAmountTot + decInWarrantyAmount
                                decInWarrantyLabourTot = decInWarrantyLabourTot + decInWarrantyLabour
                                decInWarrantyPartsTot = decInWarrantyPartsTot + decInWarrantyParts
                                decInWarrantyTransportTot = decInWarrantyTransportTot + decInWarrantyTransport
                                decInWarrantyOthersTot = decInWarrantyOthersTot + decInWarrantyOthers
                                decOutWarrantyAmountTot = decOutWarrantyAmountTot + decOutWarrantyAmount
                                decOutWarrantyLabourTot = decOutWarrantyLabourTot + decOutWarrantyLabour
                                decOutWarrantyPartsTot = decOutWarrantyPartsTot + decOutWarrantyParts
                                decOutWarrantyTransportTot = decOutWarrantyTransportTot + decOutWarrantyTransport
                                decOutWarrantyOthersTot = decOutWarrantyOthersTot + decOutWarrantyOthers
                                decSawDiscountLabourTot = decSawDiscountLabourTot + decSawDiscountLabour
                                decSawDiscountPartsTot = decSawDiscountPartsTot + decSawDiscountParts
                                decOutWarrantyLabourwSawDiscTot = decOutWarrantyLabourwSawDiscTot + decOutWarrantyLabourwSawDisc
                                decOutWarrantyPartswSawDiscTot = decOutWarrantyPartswSawDiscTot + decOutWarrantyPartswSawDisc
                                decRevenueWithoutTaxTot = decRevenueWithoutTaxTot + decRevenueWithoutTax
                                decIwPartsConsumedTot = decIwPartsConsumedTot + decIwPartsConsumed
                                decTotalPartsConsumedTot = decTotalPartsConsumedTot + decTotalPartsConsumed
                                decPrimeCostTotalTot = decPrimeCostTotalTot + decPrimeCostTotal
                                decGrossProfitTot = decGrossProfitTot + decGrossProfit
                                decFinalPercentageTot = decFinalPercentageTot + decFinalPercentage

                                row0 = row0 & "," & item.Text
                                rowTitle = rowTitle & "," & strDate

                                rowNumberOfCounters = rowNumberOfCounters & "," '& intNumberOfCounters
                                rowNumberOfBusinessDat = rowNumberOfBusinessDat & "," '& intNumberOfBusinessDat
                                rowNumberOfStaffs = rowNumberOfStaffs & "," '& intNumberOfStaffs

                                rowCustomerVisit = rowCustomerVisit & "," & intCustomerVisit
                                rowCallRegistered = rowCallRegistered & "," & intCallRegistered
                                rowRepairCompleted = rowRepairCompleted & "," & intRepairCompleted
                                rowGoodsDelivered = rowGoodsDelivered & "," & intGoodsDelivered
                                rowPending = rowPending & "," & intPending
                                rowWarranty = rowWarranty & "," & intWarranty
                                rowInWarranty = rowInWarranty & "," & intInWarranty
                                rowOutWarranty = rowOutWarranty & "," & intOutWarranty
                                rowInWarrantyAmount = rowInWarrantyAmount & "," & decInWarrantyAmount
                                rowInWarrantyLabour = rowInWarrantyLabour & "," & decInWarrantyLabour
                                rowInWarrantyParts = rowInWarrantyParts & "," & decInWarrantyParts
                                rowInWarrantyTransport = rowInWarrantyTransport & "," & decInWarrantyTransport
                                rowInWarrantyOthers = rowInWarrantyOthers & "," & decInWarrantyOthers
                                rowOutWarrantyAmount = rowOutWarrantyAmount & "," & decOutWarrantyAmount
                                rowOutWarrantyLabour = rowOutWarrantyLabour & "," & decOutWarrantyLabour
                                rowOutWarrantyParts = rowOutWarrantyParts & "," & decOutWarrantyParts
                                rowOutWarrantyTransport = rowOutWarrantyTransport & "," & decOutWarrantyTransport
                                rowOutWarrantyOthers = rowOutWarrantyOthers & "," & decOutWarrantyOthers
                                rowSawDiscountLabour = rowSawDiscountLabour & "," & decSawDiscountLabour
                                rowSawDiscountParts = rowSawDiscountParts & "," & decSawDiscountParts
                                rowOutWarrantyLabourwSawDisc = rowOutWarrantyLabourwSawDisc & "," & decOutWarrantyLabourwSawDisc
                                rowOutWarrantyPartswSawDisc = rowOutWarrantyPartswSawDisc & "," & decOutWarrantyPartswSawDisc
                                rowRevenueWithoutTax = rowRevenueWithoutTax & "," & decRevenueWithoutTax
                                rowIwPartsConsumed = rowIwPartsConsumed & "," & decIwPartsConsumed
                                rowTotalPartsConsumed = rowTotalPartsConsumed & "," & decTotalPartsConsumed
                                rowPrimeCostTotal = rowPrimeCostTotal & "," & decPrimeCostTotal
                                rowGrossProfit = rowGrossProfit & "," & decGrossProfit
                                'rowFinalPercentage = rowFinalPercentage & "," & decFinalPercentage
                                If decRevenueWithoutTax = 0 Then
                                    FinalPercentage1 = decGrossProfit
                                Else
                                    FinalPercentage1 = comcontrol.Money_Round((decGrossProfit / decRevenueWithoutTax) * 100, 2)
                                End If
                                rowFinalPercentage = rowFinalPercentage & "," & FinalPercentage1 & "%" 'decFinalPercentage / 100


                                'For Adding Final Report 
                                SscTotals(3, 0) = strDate 'Date

                                SscTotals(3, 1) = SscTotals(3, 1) + intNumberOfCounters 'rowNumberOfCounters
                                SscTotals(3, 2) = SscTotals(3, 2) + intNumberOfBusinessDat 'rowNumberOfBusinessDat
                                SscTotals(3, 3) = SscTotals(3, 3) + intNumberOfStaffs 'rowNumberOfStaffs

                                SscTotals(3, 4) = SscTotals(3, 4) + intCustomerVisit 'rowCustomerVisit
                                SscTotals(3, 5) = SscTotals(3, 5) + intCallRegistered 'rowCallRegistered
                                SscTotals(3, 6) = SscTotals(3, 6) + intRepairCompleted 'rowRepairCompleted
                                SscTotals(3, 7) = SscTotals(3, 7) + intGoodsDelivered 'rowGoodsDelivered
                                SscTotals(3, 8) = SscTotals(3, 8) + intPending 'rowPending
                                SscTotals(3, 9) = SscTotals(3, 9) + intWarranty 'rowWarranty
                                SscTotals(3, 10) = SscTotals(3, 10) + intInWarranty 'rowInWarranty
                                SscTotals(3, 11) = SscTotals(3, 11) + intOutWarranty 'rowOutWarranty
                                SscTotals(3, 12) = SscTotals(3, 12) + decInWarrantyAmount 'rowInWarrantyAmount
                                SscTotals(3, 13) = SscTotals(3, 13) + decInWarrantyLabour 'rowInWarrantyLabour
                                SscTotals(3, 14) = SscTotals(3, 14) + decInWarrantyParts 'rowInWarrantyParts
                                SscTotals(3, 15) = SscTotals(3, 15) + decInWarrantyTransport 'rowInWarrantyTransport
                                SscTotals(3, 16) = SscTotals(3, 16) + decInWarrantyOthers 'rowInWarrantyOthers
                                SscTotals(3, 17) = SscTotals(3, 17) + decOutWarrantyAmount 'rowOutWarrantyAmount
                                SscTotals(3, 18) = SscTotals(3, 18) + decOutWarrantyLabour 'rowOutWarrantyLabour
                                SscTotals(3, 19) = SscTotals(3, 19) + decOutWarrantyParts 'rowOutWarrantyParts
                                SscTotals(3, 20) = SscTotals(3, 20) + decOutWarrantyTransport 'rowOutWarrantyTransport
                                SscTotals(3, 21) = SscTotals(3, 21) + decOutWarrantyOthers 'rowOutWarrantyOthers
                                SscTotals(3, 22) = SscTotals(3, 22) + decSawDiscountLabour 'rowSawDiscountLabour
                                SscTotals(3, 23) = SscTotals(3, 23) + decSawDiscountParts 'rowSawDiscountParts
                                SscTotals(3, 24) = SscTotals(3, 24) + decOutWarrantyLabourwSawDisc 'rowOutWarrantyLabourwSawDisc
                                SscTotals(3, 25) = SscTotals(3, 25) + decOutWarrantyPartswSawDisc 'rowOutWarrantyPartswSawDisc
                                SscTotals(3, 26) = SscTotals(3, 26) + decRevenueWithoutTax 'rowRevenueWithoutTax
                                SscTotals(3, 27) = SscTotals(3, 27) + decIwPartsConsumed 'rowIwPartsConsumed
                                SscTotals(3, 28) = SscTotals(3, 28) + decTotalPartsConsumed 'rowTotalPartsConsumed
                                SscTotals(3, 29) = SscTotals(3, 29) + decPrimeCostTotal 'rowPrimeCostTotal
                                SscTotals(3, 30) = SscTotals(3, 30) + decGrossProfit 'rowGrossProfit 
                                SscTotals(3, 31) = SscTotals(3, 31) + decFinalPercentage 'rowFinalPercentage



                            Next row
                        End If

                        'monMonday5Week & monSunday5Week
                        If blWeek5 Then
                            ' lblLoc.Text = lblLoc.Text & "<br>5th  Week From: " & monMonday5Week & "  To: " & monSunday5Week
                            strDay = monMonday5Week.Day()
                            strMon = monMonday5Week.Month
                            strYear = monMonday5Week.Year

                            If Len(strDay) <= 1 Then
                                strDay = "0" & strDay
                            End If
                            If Len(strMon) <= 1 Then
                                strMon = "0" & strMon
                            End If
                            strDate = strYear & strMon & strDay
                            'Read from 
                            _dtPlReprt = comcontrol.SelectPlReport(monMonday5Week, monSunday5Week, item.Text, item.Value, Gm)

                            If (_dtPlReprt Is Nothing) Or (_dtPlReprt.Rows.Count = 0) Then
                            Else
                                For Each row As DataRow In _dtPlReprt.Rows

                                    intNumberOfCounters = row.Item("NumberOfCounters")
                                    intNumberOfBusinessDat = row.Item("NumberOfBusinessDat")
                                    intNumberOfStaffs = row.Item("NumberOfStaffs")

                                    intCustomerVisit = row.Item("CustomerVisit")
                                    intCallRegistered = row.Item("CallRegistered")
                                    intRepairCompleted = row.Item("RepairCompleted")
                                    intGoodsDelivered = row.Item("GoodsDelivered")
                                    intPending = row.Item("Pending")
                                    intWarranty = row.Item("Warranty")
                                    intInWarranty = row.Item("InWarranty")
                                    intOutWarranty = row.Item("OutWarranty")
                                    decInWarrantyAmount = row.Item("InWarrantyAmount")
                                    decInWarrantyLabour = row.Item("InWarrantyLabour")
                                    decInWarrantyParts = row.Item("InWarrantyParts")
                                    decInWarrantyTransport = row.Item("InWarrantyTransport")
                                    decInWarrantyOthers = row.Item("InWarrantyOthers")
                                    decOutWarrantyAmount = row.Item("OutWarrantyAmount")
                                    decOutWarrantyLabour = row.Item("OutWarrantyLabour")
                                    decOutWarrantyParts = row.Item("OutWarrantyParts")
                                    decOutWarrantyTransport = row.Item("OutWarrantyTransport")
                                    decOutWarrantyOthers = row.Item("OutWarrantyOthers")
                                    decSawDiscountLabour = row.Item("SawDiscountLabour")
                                    decSawDiscountParts = row.Item("SawDiscountParts")
                                    decOutWarrantyLabourwSawDisc = row.Item("OutWarrantyLabourwSawDisc")
                                    decOutWarrantyPartswSawDisc = row.Item("OutWarrantyPartswSawDisc")
                                    decRevenueWithoutTax = row.Item("RevenueWithoutTax")
                                    decIwPartsConsumed = row.Item("IwPartsConsumed")
                                    decTotalPartsConsumed = row.Item("TotalPartsConsumed")
                                    decPrimeCostTotal = row.Item("PrimeCostTotal")
                                    decGrossProfit = row.Item("GrossProfit")
                                    decFinalPercentage = row.Item("FinalPercentage")

                                    intNumberOfCountersTot = intNumberOfCountersTot + intNumberOfCounters
                                    intNumberOfBusinessDatTot = intNumberOfBusinessDatTot + intNumberOfBusinessDat
                                    intNumberOfStaffsTot = intNumberOfStaffsTot + intNumberOfStaffs

                                    intCustomerVisitTot = intCustomerVisitTot + intCustomerVisit
                                    intCallRegisteredTot = intCallRegisteredTot + intCallRegistered
                                    intRepairCompletedTot = intRepairCompletedTot + intRepairCompleted
                                    intGoodsDeliveredTot = intGoodsDeliveredTot + intGoodsDelivered
                                    intPendingTot = intPendingTot + intPending
                                    intWarrantyTot = intWarrantyTot + intWarranty
                                    intInWarrantyTot = intInWarrantyTot + intInWarranty
                                    intOutWarrantyTot = intOutWarrantyTot + intOutWarranty
                                    decInWarrantyAmountTot = decInWarrantyAmountTot + decInWarrantyAmount
                                    decInWarrantyLabourTot = decInWarrantyLabourTot + decInWarrantyLabour
                                    decInWarrantyPartsTot = decInWarrantyPartsTot + decInWarrantyParts
                                    decInWarrantyTransportTot = decInWarrantyTransportTot + decInWarrantyTransport
                                    decInWarrantyOthersTot = decInWarrantyOthersTot + decInWarrantyOthers
                                    decOutWarrantyAmountTot = decOutWarrantyAmountTot + decOutWarrantyAmount
                                    decOutWarrantyLabourTot = decOutWarrantyLabourTot + decOutWarrantyLabour
                                    decOutWarrantyPartsTot = decOutWarrantyPartsTot + decOutWarrantyParts
                                    decOutWarrantyTransportTot = decOutWarrantyTransportTot + decOutWarrantyTransport
                                    decOutWarrantyOthersTot = decOutWarrantyOthersTot + decOutWarrantyOthers
                                    decSawDiscountLabourTot = decSawDiscountLabourTot + decSawDiscountLabour
                                    decSawDiscountPartsTot = decSawDiscountPartsTot + decSawDiscountParts
                                    decOutWarrantyLabourwSawDiscTot = decOutWarrantyLabourwSawDiscTot + decOutWarrantyLabourwSawDisc
                                    decOutWarrantyPartswSawDiscTot = decOutWarrantyPartswSawDiscTot + decOutWarrantyPartswSawDisc
                                    decRevenueWithoutTaxTot = decRevenueWithoutTaxTot + decRevenueWithoutTax
                                    decIwPartsConsumedTot = decIwPartsConsumedTot + decIwPartsConsumed
                                    decTotalPartsConsumedTot = decTotalPartsConsumedTot + decTotalPartsConsumed
                                    decPrimeCostTotalTot = decPrimeCostTotalTot + decPrimeCostTotal
                                    decGrossProfitTot = decGrossProfitTot + decGrossProfit
                                    decFinalPercentageTot = decFinalPercentageTot + decFinalPercentage

                                    row0 = row0 & "," & item.Text
                                    rowTitle = rowTitle & "," & strDate

                                    rowNumberOfCounters = rowNumberOfCounters & "," '& intNumberOfCounters
                                    rowNumberOfBusinessDat = rowNumberOfBusinessDat & "," '& intNumberOfBusinessDat
                                    rowNumberOfStaffs = rowNumberOfStaffs & "," '& intNumberOfStaffs

                                    rowCustomerVisit = rowCustomerVisit & "," & intCustomerVisit
                                    rowCallRegistered = rowCallRegistered & "," & intCallRegistered
                                    rowRepairCompleted = rowRepairCompleted & "," & intRepairCompleted
                                    rowGoodsDelivered = rowGoodsDelivered & "," & intGoodsDelivered
                                    rowPending = rowPending & "," & intPending
                                    rowWarranty = rowWarranty & "," & intWarranty
                                    rowInWarranty = rowInWarranty & "," & intInWarranty
                                    rowOutWarranty = rowOutWarranty & "," & intOutWarranty
                                    rowInWarrantyAmount = rowInWarrantyAmount & "," & decInWarrantyAmount
                                    rowInWarrantyLabour = rowInWarrantyLabour & "," & decInWarrantyLabour
                                    rowInWarrantyParts = rowInWarrantyParts & "," & decInWarrantyParts
                                    rowInWarrantyTransport = rowInWarrantyTransport & "," & decInWarrantyTransport
                                    rowInWarrantyOthers = rowInWarrantyOthers & "," & decInWarrantyOthers
                                    rowOutWarrantyAmount = rowOutWarrantyAmount & "," & decOutWarrantyAmount
                                    rowOutWarrantyLabour = rowOutWarrantyLabour & "," & decOutWarrantyLabour
                                    rowOutWarrantyParts = rowOutWarrantyParts & "," & decOutWarrantyParts
                                    rowOutWarrantyTransport = rowOutWarrantyTransport & "," & decOutWarrantyTransport
                                    rowOutWarrantyOthers = rowOutWarrantyOthers & "," & decOutWarrantyOthers
                                    rowSawDiscountLabour = rowSawDiscountLabour & "," & decSawDiscountLabour
                                    rowSawDiscountParts = rowSawDiscountParts & "," & decSawDiscountParts
                                    rowOutWarrantyLabourwSawDisc = rowOutWarrantyLabourwSawDisc & "," & decOutWarrantyLabourwSawDisc
                                    rowOutWarrantyPartswSawDisc = rowOutWarrantyPartswSawDisc & "," & decOutWarrantyPartswSawDisc
                                    rowRevenueWithoutTax = rowRevenueWithoutTax & "," & decRevenueWithoutTax
                                    rowIwPartsConsumed = rowIwPartsConsumed & "," & decIwPartsConsumed
                                    rowTotalPartsConsumed = rowTotalPartsConsumed & "," & decTotalPartsConsumed
                                    rowPrimeCostTotal = rowPrimeCostTotal & "," & decPrimeCostTotal
                                    rowGrossProfit = rowGrossProfit & "," & decGrossProfit
                                    'rowFinalPercentage = rowFinalPercentage & "," & decFinalPercentage
                                    If decRevenueWithoutTax = 0 Then
                                        FinalPercentage1 = decGrossProfit
                                    Else
                                        FinalPercentage1 = comcontrol.Money_Round((decGrossProfit / decRevenueWithoutTax) * 100, 2)
                                    End If
                                    rowFinalPercentage = rowFinalPercentage & "," & FinalPercentage1 & "%" 'decFinalPercentage / 100


                                    'For Adding Final Report 
                                    SscTotals(4, 0) = strDate 'Date

                                    SscTotals(4, 1) = SscTotals(4, 1) + intNumberOfCounters 'rowNumberOfCounters
                                    SscTotals(4, 2) = SscTotals(4, 2) + intNumberOfBusinessDat 'rowNumberOfBusinessDat
                                    SscTotals(4, 3) = SscTotals(4, 3) + intNumberOfStaffs 'rowNumberOfStaffs

                                    SscTotals(4, 4) = SscTotals(4, 4) + intCustomerVisit 'rowCustomerVisit
                                    SscTotals(4, 5) = SscTotals(4, 5) + intCallRegistered 'rowCallRegistered
                                    SscTotals(4, 6) = SscTotals(4, 6) + intRepairCompleted 'rowRepairCompleted
                                    SscTotals(4, 7) = SscTotals(4, 7) + intGoodsDelivered 'rowGoodsDelivered
                                    SscTotals(4, 8) = SscTotals(4, 8) + intPending 'rowPending
                                    SscTotals(4, 9) = SscTotals(4, 9) + intWarranty 'rowWarranty
                                    SscTotals(4, 10) = SscTotals(4, 10) + intInWarranty 'rowInWarranty
                                    SscTotals(4, 11) = SscTotals(4, 11) + intOutWarranty 'rowOutWarranty
                                    SscTotals(4, 12) = SscTotals(4, 12) + decInWarrantyAmount 'rowInWarrantyAmount
                                    SscTotals(4, 13) = SscTotals(4, 13) + decInWarrantyLabour 'rowInWarrantyLabour
                                    SscTotals(4, 14) = SscTotals(4, 14) + decInWarrantyParts 'rowInWarrantyParts
                                    SscTotals(4, 15) = SscTotals(4, 15) + decInWarrantyTransport 'rowInWarrantyTransport
                                    SscTotals(4, 16) = SscTotals(4, 16) + decInWarrantyOthers 'rowInWarrantyOthers
                                    SscTotals(4, 17) = SscTotals(4, 17) + decOutWarrantyAmount 'rowOutWarrantyAmount
                                    SscTotals(4, 18) = SscTotals(4, 18) + decOutWarrantyLabour 'rowOutWarrantyLabour
                                    SscTotals(4, 19) = SscTotals(4, 19) + decOutWarrantyParts 'rowOutWarrantyParts
                                    SscTotals(4, 20) = SscTotals(4, 20) + decOutWarrantyTransport 'rowOutWarrantyTransport
                                    SscTotals(4, 21) = SscTotals(4, 21) + decOutWarrantyOthers 'rowOutWarrantyOthers
                                    SscTotals(4, 22) = SscTotals(4, 22) + decSawDiscountLabour 'rowSawDiscountLabour
                                    SscTotals(4, 23) = SscTotals(4, 23) + decSawDiscountParts 'rowSawDiscountParts
                                    SscTotals(4, 24) = SscTotals(4, 24) + decOutWarrantyLabourwSawDisc 'rowOutWarrantyLabourwSawDisc
                                    SscTotals(4, 25) = SscTotals(4, 25) + decOutWarrantyPartswSawDisc 'rowOutWarrantyPartswSawDisc
                                    SscTotals(4, 26) = SscTotals(4, 26) + decRevenueWithoutTax 'rowRevenueWithoutTax
                                    SscTotals(4, 27) = SscTotals(4, 27) + decIwPartsConsumed 'rowIwPartsConsumed
                                    SscTotals(4, 28) = SscTotals(4, 28) + decTotalPartsConsumed 'rowTotalPartsConsumed
                                    SscTotals(4, 29) = SscTotals(4, 29) + decPrimeCostTotal 'rowPrimeCostTotal
                                    SscTotals(4, 30) = SscTotals(4, 30) + decGrossProfit 'rowGrossProfit 
                                    SscTotals(4, 31) = SscTotals(4, 31) + decFinalPercentage 'rowFinalPercentage



                                Next row
                            End If


                        End If

                        'monMonday6Week & monSunday6Week
                        If blWeek6 Then
                            'lblLoc.Text = lblLoc.Text & "<br>6th  Week From: " & monMonday6Week & "  To: " & monSunday6Week
                            strDay = monMonday6Week.Day()
                            strMon = monMonday6Week.Month
                            strYear = monMonday6Week.Year

                            If Len(strDay) <= 1 Then
                                strDay = "0" & strDay
                            End If
                            If Len(strMon) <= 1 Then
                                strMon = "0" & strMon
                            End If
                            strDate = strYear & strMon & strDay
                            'Read from 
                            _dtPlReprt = comcontrol.SelectPlReport(monMonday6Week, monSunday6Week, item.Text, item.Value, Gm)

                            If (_dtPlReprt Is Nothing) Or (_dtPlReprt.Rows.Count = 0) Then
                            Else
                                For Each row As DataRow In _dtPlReprt.Rows

                                    intNumberOfCounters = row.Item("NumberOfCounters")
                                    intNumberOfBusinessDat = row.Item("NumberOfBusinessDat")
                                    intNumberOfStaffs = row.Item("NumberOfStaffs")

                                    intCustomerVisit = row.Item("CustomerVisit")
                                    intCallRegistered = row.Item("CallRegistered")
                                    intRepairCompleted = row.Item("RepairCompleted")
                                    intGoodsDelivered = row.Item("GoodsDelivered")
                                    intPending = row.Item("Pending")
                                    intWarranty = row.Item("Warranty")
                                    intInWarranty = row.Item("InWarranty")
                                    intOutWarranty = row.Item("OutWarranty")
                                    decInWarrantyAmount = row.Item("InWarrantyAmount")
                                    decInWarrantyLabour = row.Item("InWarrantyLabour")
                                    decInWarrantyParts = row.Item("InWarrantyParts")
                                    decInWarrantyTransport = row.Item("InWarrantyTransport")
                                    decInWarrantyOthers = row.Item("InWarrantyOthers")
                                    decOutWarrantyAmount = row.Item("OutWarrantyAmount")
                                    decOutWarrantyLabour = row.Item("OutWarrantyLabour")
                                    decOutWarrantyParts = row.Item("OutWarrantyParts")
                                    decOutWarrantyTransport = row.Item("OutWarrantyTransport")
                                    decOutWarrantyOthers = row.Item("OutWarrantyOthers")
                                    decSawDiscountLabour = row.Item("SawDiscountLabour")
                                    decSawDiscountParts = row.Item("SawDiscountParts")
                                    decOutWarrantyLabourwSawDisc = row.Item("OutWarrantyLabourwSawDisc")
                                    decOutWarrantyPartswSawDisc = row.Item("OutWarrantyPartswSawDisc")
                                    decRevenueWithoutTax = row.Item("RevenueWithoutTax")
                                    decIwPartsConsumed = row.Item("IwPartsConsumed")
                                    decTotalPartsConsumed = row.Item("TotalPartsConsumed")
                                    decPrimeCostTotal = row.Item("PrimeCostTotal")
                                    decGrossProfit = row.Item("GrossProfit")
                                    decFinalPercentage = row.Item("FinalPercentage")

                                    intNumberOfCountersTot = intNumberOfCountersTot + intNumberOfCounters
                                    intNumberOfBusinessDatTot = intNumberOfBusinessDatTot + intNumberOfBusinessDat
                                    intNumberOfStaffsTot = intNumberOfStaffsTot + intNumberOfStaffs

                                    intCustomerVisitTot = intCustomerVisitTot + intCustomerVisit
                                    intCallRegisteredTot = intCallRegisteredTot + intCallRegistered
                                    intRepairCompletedTot = intRepairCompletedTot + intRepairCompleted
                                    intGoodsDeliveredTot = intGoodsDeliveredTot + intGoodsDelivered
                                    intPendingTot = intPendingTot + intPending
                                    intWarrantyTot = intWarrantyTot + intWarranty
                                    intInWarrantyTot = intInWarrantyTot + intInWarranty
                                    intOutWarrantyTot = intOutWarrantyTot + intOutWarranty
                                    decInWarrantyAmountTot = decInWarrantyAmountTot + decInWarrantyAmount
                                    decInWarrantyLabourTot = decInWarrantyLabourTot + decInWarrantyLabour
                                    decInWarrantyPartsTot = decInWarrantyPartsTot + decInWarrantyParts
                                    decInWarrantyTransportTot = decInWarrantyTransportTot + decInWarrantyTransport
                                    decInWarrantyOthersTot = decInWarrantyOthersTot + decInWarrantyOthers
                                    decOutWarrantyAmountTot = decOutWarrantyAmountTot + decOutWarrantyAmount
                                    decOutWarrantyLabourTot = decOutWarrantyLabourTot + decOutWarrantyLabour
                                    decOutWarrantyPartsTot = decOutWarrantyPartsTot + decOutWarrantyParts
                                    decOutWarrantyTransportTot = decOutWarrantyTransportTot + decOutWarrantyTransport
                                    decOutWarrantyOthersTot = decOutWarrantyOthersTot + decOutWarrantyOthers
                                    decSawDiscountLabourTot = decSawDiscountLabourTot + decSawDiscountLabour
                                    decSawDiscountPartsTot = decSawDiscountPartsTot + decSawDiscountParts
                                    decOutWarrantyLabourwSawDiscTot = decOutWarrantyLabourwSawDiscTot + decOutWarrantyLabourwSawDisc
                                    decOutWarrantyPartswSawDiscTot = decOutWarrantyPartswSawDiscTot + decOutWarrantyPartswSawDisc
                                    decRevenueWithoutTaxTot = decRevenueWithoutTaxTot + decRevenueWithoutTax
                                    decIwPartsConsumedTot = decIwPartsConsumedTot + decIwPartsConsumed
                                    decTotalPartsConsumedTot = decTotalPartsConsumedTot + decTotalPartsConsumed
                                    decPrimeCostTotalTot = decPrimeCostTotalTot + decPrimeCostTotal
                                    decGrossProfitTot = decGrossProfitTot + decGrossProfit
                                    decFinalPercentageTot = decFinalPercentageTot + decFinalPercentage

                                    row0 = row0 & "," & item.Text
                                    rowTitle = rowTitle & "," & strDate

                                    rowNumberOfCounters = rowNumberOfCounters & "," '& intNumberOfCounters
                                    rowNumberOfBusinessDat = rowNumberOfBusinessDat & "," '& intNumberOfBusinessDat
                                    rowNumberOfStaffs = rowNumberOfStaffs & "," '& intNumberOfStaffs

                                    rowCustomerVisit = rowCustomerVisit & "," & intCustomerVisit
                                    rowCallRegistered = rowCallRegistered & "," & intCallRegistered
                                    rowRepairCompleted = rowRepairCompleted & "," & intRepairCompleted
                                    rowGoodsDelivered = rowGoodsDelivered & "," & intGoodsDelivered
                                    rowPending = rowPending & "," & intPending
                                    rowWarranty = rowWarranty & "," & intWarranty
                                    rowInWarranty = rowInWarranty & "," & intInWarranty
                                    rowOutWarranty = rowOutWarranty & "," & intOutWarranty
                                    rowInWarrantyAmount = rowInWarrantyAmount & "," & decInWarrantyAmount
                                    rowInWarrantyLabour = rowInWarrantyLabour & "," & decInWarrantyLabour
                                    rowInWarrantyParts = rowInWarrantyParts & "," & decInWarrantyParts
                                    rowInWarrantyTransport = rowInWarrantyTransport & "," & decInWarrantyTransport
                                    rowInWarrantyOthers = rowInWarrantyOthers & "," & decInWarrantyOthers
                                    rowOutWarrantyAmount = rowOutWarrantyAmount & "," & decOutWarrantyAmount
                                    rowOutWarrantyLabour = rowOutWarrantyLabour & "," & decOutWarrantyLabour
                                    rowOutWarrantyParts = rowOutWarrantyParts & "," & decOutWarrantyParts
                                    rowOutWarrantyTransport = rowOutWarrantyTransport & "," & decOutWarrantyTransport
                                    rowOutWarrantyOthers = rowOutWarrantyOthers & "," & decOutWarrantyOthers
                                    rowSawDiscountLabour = rowSawDiscountLabour & "," & decSawDiscountLabour
                                    rowSawDiscountParts = rowSawDiscountParts & "," & decSawDiscountParts
                                    rowOutWarrantyLabourwSawDisc = rowOutWarrantyLabourwSawDisc & "," & decOutWarrantyLabourwSawDisc
                                    rowOutWarrantyPartswSawDisc = rowOutWarrantyPartswSawDisc & "," & decOutWarrantyPartswSawDisc
                                    rowRevenueWithoutTax = rowRevenueWithoutTax & "," & decRevenueWithoutTax
                                    rowIwPartsConsumed = rowIwPartsConsumed & "," & decIwPartsConsumed
                                    rowTotalPartsConsumed = rowTotalPartsConsumed & "," & decTotalPartsConsumed
                                    rowPrimeCostTotal = rowPrimeCostTotal & "," & decPrimeCostTotal
                                    rowGrossProfit = rowGrossProfit & "," & decGrossProfit
                                    'rowFinalPercentage = rowFinalPercentage & "," & decFinalPercentage
                                    If decRevenueWithoutTax = 0 Then
                                        FinalPercentage1 = decGrossProfit
                                    Else
                                        FinalPercentage1 = comcontrol.Money_Round((decGrossProfit / decRevenueWithoutTax) * 100, 2)
                                    End If
                                    rowFinalPercentage = rowFinalPercentage & "," & FinalPercentage1 & "%" 'decFinalPercentage / 100


                                    'For Adding Final Report 
                                    SscTotals(5, 0) = strDate 'Date

                                    SscTotals(5, 1) = SscTotals(5, 1) + intNumberOfCounters 'rowNumberOfCounters
                                    SscTotals(5, 2) = SscTotals(5, 2) + intNumberOfBusinessDat 'rowNumberOfBusinessDat
                                    SscTotals(5, 3) = SscTotals(5, 3) + intNumberOfStaffs 'rowNumberOfStaffs

                                    SscTotals(5, 4) = SscTotals(5, 4) + intCustomerVisit 'rowCustomerVisit
                                    SscTotals(5, 5) = SscTotals(5, 5) + intCallRegistered 'rowCallRegistered
                                    SscTotals(5, 6) = SscTotals(5, 6) + intRepairCompleted 'rowRepairCompleted
                                    SscTotals(5, 7) = SscTotals(5, 7) + intGoodsDelivered 'rowGoodsDelivered
                                    SscTotals(5, 8) = SscTotals(5, 8) + intPending 'rowPending
                                    SscTotals(5, 9) = SscTotals(5, 9) + intWarranty 'rowWarranty
                                    SscTotals(5, 10) = SscTotals(5, 10) + intInWarranty 'rowInWarranty
                                    SscTotals(5, 11) = SscTotals(5, 11) + intOutWarranty 'rowOutWarranty
                                    SscTotals(5, 12) = SscTotals(5, 12) + decInWarrantyAmount 'rowInWarrantyAmount
                                    SscTotals(5, 13) = SscTotals(5, 13) + decInWarrantyLabour 'rowInWarrantyLabour
                                    SscTotals(5, 14) = SscTotals(5, 14) + decInWarrantyParts 'rowInWarrantyParts
                                    SscTotals(5, 15) = SscTotals(5, 15) + decInWarrantyTransport 'rowInWarrantyTransport
                                    SscTotals(5, 16) = SscTotals(5, 16) + decInWarrantyOthers 'rowInWarrantyOthers
                                    SscTotals(5, 17) = SscTotals(5, 17) + decOutWarrantyAmount 'rowOutWarrantyAmount
                                    SscTotals(5, 18) = SscTotals(5, 18) + decOutWarrantyLabour 'rowOutWarrantyLabour
                                    SscTotals(5, 19) = SscTotals(5, 19) + decOutWarrantyParts 'rowOutWarrantyParts
                                    SscTotals(5, 20) = SscTotals(5, 20) + decOutWarrantyTransport 'rowOutWarrantyTransport
                                    SscTotals(5, 21) = SscTotals(5, 21) + decOutWarrantyOthers 'rowOutWarrantyOthers
                                    SscTotals(5, 22) = SscTotals(5, 22) + decSawDiscountLabour 'rowSawDiscountLabour
                                    SscTotals(5, 23) = SscTotals(5, 23) + decSawDiscountParts 'rowSawDiscountParts
                                    SscTotals(5, 24) = SscTotals(5, 24) + decOutWarrantyLabourwSawDisc 'rowOutWarrantyLabourwSawDisc
                                    SscTotals(5, 25) = SscTotals(5, 25) + decOutWarrantyPartswSawDisc 'rowOutWarrantyPartswSawDisc
                                    SscTotals(5, 26) = SscTotals(5, 26) + decRevenueWithoutTax 'rowRevenueWithoutTax
                                    SscTotals(5, 27) = SscTotals(5, 27) + decIwPartsConsumed 'rowIwPartsConsumed
                                    SscTotals(5, 28) = SscTotals(5, 28) + decTotalPartsConsumed 'rowTotalPartsConsumed
                                    SscTotals(5, 29) = SscTotals(5, 29) + decPrimeCostTotal 'rowPrimeCostTotal
                                    SscTotals(5, 30) = SscTotals(5, 30) + decGrossProfit 'rowGrossProfit 
                                    SscTotals(5, 31) = SscTotals(5, 31) + decFinalPercentage 'rowFinalPercentage

                                Next row
                            End If


                        End If
                    End If
                Else 'Selected SSC
                    'monMonday1Week & monSunday1Week
                    strDay = monMonday1Week.Day()
                    strMon = monMonday1Week.Month
                    strYear = monMonday1Week.Year

                    If Len(strDay) <= 1 Then
                        strDay = "0" & strDay
                    End If
                    If Len(strMon) <= 1 Then
                        strMon = "0" & strMon
                    End If
                    strDate = strYear & strMon & strDay
                    'Read from 
                    _dtPlReprt = comcontrol.SelectPlReport(monMonday1Week, monMonday1Week, DropListLocation.SelectedItem.Text, DropListLocation.SelectedItem.Value, Gm)

                    If (_dtPlReprt Is Nothing) Or (_dtPlReprt.Rows.Count = 0) Then
                    Else
                        For Each row As DataRow In _dtPlReprt.Rows

                            intNumberOfCounters = row.Item("NumberOfCounters")
                            intNumberOfBusinessDat = row.Item("NumberOfBusinessDat")
                            intNumberOfStaffs = row.Item("NumberOfStaffs")

                            intCustomerVisit = row.Item("CustomerVisit")
                            intCallRegistered = row.Item("CallRegistered")
                            intRepairCompleted = row.Item("RepairCompleted")
                            intGoodsDelivered = row.Item("GoodsDelivered")
                            intPending = row.Item("Pending")
                            intWarranty = row.Item("Warranty")
                            intInWarranty = row.Item("InWarranty")
                            intOutWarranty = row.Item("OutWarranty")
                            decInWarrantyAmount = row.Item("InWarrantyAmount")
                            decInWarrantyLabour = row.Item("InWarrantyLabour")
                            decInWarrantyParts = row.Item("InWarrantyParts")
                            decInWarrantyTransport = row.Item("InWarrantyTransport")
                            decInWarrantyOthers = row.Item("InWarrantyOthers")
                            decOutWarrantyAmount = row.Item("OutWarrantyAmount")
                            decOutWarrantyLabour = row.Item("OutWarrantyLabour")
                            decOutWarrantyParts = row.Item("OutWarrantyParts")
                            decOutWarrantyTransport = row.Item("OutWarrantyTransport")
                            decOutWarrantyOthers = row.Item("OutWarrantyOthers")
                            decSawDiscountLabour = row.Item("SawDiscountLabour")
                            decSawDiscountParts = row.Item("SawDiscountParts")
                            decOutWarrantyLabourwSawDisc = row.Item("OutWarrantyLabourwSawDisc")
                            decOutWarrantyPartswSawDisc = row.Item("OutWarrantyPartswSawDisc")
                            decRevenueWithoutTax = row.Item("RevenueWithoutTax")
                            decIwPartsConsumed = row.Item("IwPartsConsumed")
                            decTotalPartsConsumed = row.Item("TotalPartsConsumed")
                            decPrimeCostTotal = row.Item("PrimeCostTotal")
                            decGrossProfit = row.Item("GrossProfit")
                            decFinalPercentage = row.Item("FinalPercentage")

                            intNumberOfCountersTot = intNumberOfCountersTot + intNumberOfCounters
                            intNumberOfBusinessDatTot = intNumberOfBusinessDatTot + intNumberOfBusinessDat
                            intNumberOfStaffsTot = intNumberOfStaffsTot + intNumberOfStaffs

                            intCustomerVisitTot = intCustomerVisitTot + intCustomerVisit
                            intCallRegisteredTot = intCallRegisteredTot + intCallRegistered
                            intRepairCompletedTot = intRepairCompletedTot + intRepairCompleted
                            intGoodsDeliveredTot = intGoodsDeliveredTot + intGoodsDelivered
                            intPendingTot = intPendingTot + intPending
                            intWarrantyTot = intWarrantyTot + intWarranty
                            intInWarrantyTot = intInWarrantyTot + intInWarranty
                            intOutWarrantyTot = intOutWarrantyTot + intOutWarranty
                            decInWarrantyAmountTot = decInWarrantyAmountTot + decInWarrantyAmount
                            decInWarrantyLabourTot = decInWarrantyLabourTot + decInWarrantyLabour
                            decInWarrantyPartsTot = decInWarrantyPartsTot + decInWarrantyParts
                            decInWarrantyTransportTot = decInWarrantyTransportTot + decInWarrantyTransport
                            decInWarrantyOthersTot = decInWarrantyOthersTot + decInWarrantyOthers
                            decOutWarrantyAmountTot = decOutWarrantyAmountTot + decOutWarrantyAmount
                            decOutWarrantyLabourTot = decOutWarrantyLabourTot + decOutWarrantyLabour
                            decOutWarrantyPartsTot = decOutWarrantyPartsTot + decOutWarrantyParts
                            decOutWarrantyTransportTot = decOutWarrantyTransportTot + decOutWarrantyTransport
                            decOutWarrantyOthersTot = decOutWarrantyOthersTot + decOutWarrantyOthers
                            decSawDiscountLabourTot = decSawDiscountLabourTot + decSawDiscountLabour
                            decSawDiscountPartsTot = decSawDiscountPartsTot + decSawDiscountParts
                            decOutWarrantyLabourwSawDiscTot = decOutWarrantyLabourwSawDiscTot + decOutWarrantyLabourwSawDisc
                            decOutWarrantyPartswSawDiscTot = decOutWarrantyPartswSawDiscTot + decOutWarrantyPartswSawDisc
                            decRevenueWithoutTaxTot = decRevenueWithoutTaxTot + decRevenueWithoutTax
                            decIwPartsConsumedTot = decIwPartsConsumedTot + decIwPartsConsumed
                            decTotalPartsConsumedTot = decTotalPartsConsumedTot + decTotalPartsConsumed
                            decPrimeCostTotalTot = decPrimeCostTotalTot + decPrimeCostTotal
                            decGrossProfitTot = decGrossProfitTot + decGrossProfit
                            decFinalPercentageTot = decFinalPercentageTot + decFinalPercentage

                            rowTitle = rowTitle & "," & strDate

                            rowNumberOfCounters = rowNumberOfCounters & "," '& intNumberOfCounters
                            rowNumberOfBusinessDat = rowNumberOfBusinessDat & "," '& intNumberOfBusinessDat
                            rowNumberOfStaffs = rowNumberOfStaffs & "," '& intNumberOfStaffs

                            rowCustomerVisit = rowCustomerVisit & "," & intCustomerVisit
                            rowCallRegistered = rowCallRegistered & "," & intCallRegistered
                            rowRepairCompleted = rowRepairCompleted & "," & intRepairCompleted
                            rowGoodsDelivered = rowGoodsDelivered & "," & intGoodsDelivered
                            rowPending = rowPending & "," & intPending
                            rowWarranty = rowWarranty & "," & intWarranty
                            rowInWarranty = rowInWarranty & "," & intInWarranty
                            rowOutWarranty = rowOutWarranty & "," & intOutWarranty
                            rowInWarrantyAmount = rowInWarrantyAmount & "," & decInWarrantyAmount
                            rowInWarrantyLabour = rowInWarrantyLabour & "," & decInWarrantyLabour
                            rowInWarrantyParts = rowInWarrantyParts & "," & decInWarrantyParts
                            rowInWarrantyTransport = rowInWarrantyTransport & "," & decInWarrantyTransport
                            rowInWarrantyOthers = rowInWarrantyOthers & "," & decInWarrantyOthers
                            rowOutWarrantyAmount = rowOutWarrantyAmount & "," & decOutWarrantyAmount
                            rowOutWarrantyLabour = rowOutWarrantyLabour & "," & decOutWarrantyLabour
                            rowOutWarrantyParts = rowOutWarrantyParts & "," & decOutWarrantyParts
                            rowOutWarrantyTransport = rowOutWarrantyTransport & "," & decOutWarrantyTransport
                            rowOutWarrantyOthers = rowOutWarrantyOthers & "," & decOutWarrantyOthers
                            rowSawDiscountLabour = rowSawDiscountLabour & "," & decSawDiscountLabour
                            rowSawDiscountParts = rowSawDiscountParts & "," & decSawDiscountParts
                            rowOutWarrantyLabourwSawDisc = rowOutWarrantyLabourwSawDisc & "," & decOutWarrantyLabourwSawDisc
                            rowOutWarrantyPartswSawDisc = rowOutWarrantyPartswSawDisc & "," & decOutWarrantyPartswSawDisc
                            rowRevenueWithoutTax = rowRevenueWithoutTax & "," & decRevenueWithoutTax
                            rowIwPartsConsumed = rowIwPartsConsumed & "," & decIwPartsConsumed
                            rowTotalPartsConsumed = rowTotalPartsConsumed & "," & decTotalPartsConsumed
                            rowPrimeCostTotal = rowPrimeCostTotal & "," & decPrimeCostTotal
                            rowGrossProfit = rowGrossProfit & "," & decGrossProfit
                            'rowFinalPercentage = rowFinalPercentage & "," & decFinalPercentage
                            If decRevenueWithoutTax = 0 Then
                                FinalPercentage1 = decGrossProfit
                            Else
                                FinalPercentage1 = comcontrol.Money_Round((decGrossProfit / decRevenueWithoutTax) * 100, 2)
                            End If
                            rowFinalPercentage = rowFinalPercentage & "," & FinalPercentage1 & "%" 'decFinalPercentage / 100


                        Next row
                    End If
                    'monMonday2Week & monSunday2Week
                    strDay = monMonday2Week.Day()
                    strMon = monMonday2Week.Month
                    strYear = monMonday2Week.Year

                    If Len(strDay) <= 1 Then
                        strDay = "0" & strDay
                    End If
                    If Len(strMon) <= 1 Then
                        strMon = "0" & strMon
                    End If
                    strDate = strYear & strMon & strDay
                    'Read from 
                    _dtPlReprt = comcontrol.SelectPlReport(monMonday2Week, monSunday2Week, DropListLocation.SelectedItem.Text, DropListLocation.SelectedItem.Value, Gm)

                    If (_dtPlReprt Is Nothing) Or (_dtPlReprt.Rows.Count = 0) Then
                    Else
                        For Each row As DataRow In _dtPlReprt.Rows

                            intNumberOfCounters = row.Item("NumberOfCounters")
                            intNumberOfBusinessDat = row.Item("NumberOfBusinessDat")
                            intNumberOfStaffs = row.Item("NumberOfStaffs")

                            intCustomerVisit = row.Item("CustomerVisit")
                            intCallRegistered = row.Item("CallRegistered")
                            intRepairCompleted = row.Item("RepairCompleted")
                            intGoodsDelivered = row.Item("GoodsDelivered")
                            intPending = row.Item("Pending")
                            intWarranty = row.Item("Warranty")
                            intInWarranty = row.Item("InWarranty")
                            intOutWarranty = row.Item("OutWarranty")
                            decInWarrantyAmount = row.Item("InWarrantyAmount")
                            decInWarrantyLabour = row.Item("InWarrantyLabour")
                            decInWarrantyParts = row.Item("InWarrantyParts")
                            decInWarrantyTransport = row.Item("InWarrantyTransport")
                            decInWarrantyOthers = row.Item("InWarrantyOthers")
                            decOutWarrantyAmount = row.Item("OutWarrantyAmount")
                            decOutWarrantyLabour = row.Item("OutWarrantyLabour")
                            decOutWarrantyParts = row.Item("OutWarrantyParts")
                            decOutWarrantyTransport = row.Item("OutWarrantyTransport")
                            decOutWarrantyOthers = row.Item("OutWarrantyOthers")
                            decSawDiscountLabour = row.Item("SawDiscountLabour")
                            decSawDiscountParts = row.Item("SawDiscountParts")
                            decOutWarrantyLabourwSawDisc = row.Item("OutWarrantyLabourwSawDisc")
                            decOutWarrantyPartswSawDisc = row.Item("OutWarrantyPartswSawDisc")
                            decRevenueWithoutTax = row.Item("RevenueWithoutTax")
                            decIwPartsConsumed = row.Item("IwPartsConsumed")
                            decTotalPartsConsumed = row.Item("TotalPartsConsumed")
                            decPrimeCostTotal = row.Item("PrimeCostTotal")
                            decGrossProfit = row.Item("GrossProfit")
                            decFinalPercentage = row.Item("FinalPercentage")

                            intNumberOfCountersTot = intNumberOfCountersTot + intNumberOfCounters
                            intNumberOfBusinessDatTot = intNumberOfBusinessDatTot + intNumberOfBusinessDat
                            intNumberOfStaffsTot = intNumberOfStaffsTot + intNumberOfStaffs

                            intCustomerVisitTot = intCustomerVisitTot + intCustomerVisit
                            intCallRegisteredTot = intCallRegisteredTot + intCallRegistered
                            intRepairCompletedTot = intRepairCompletedTot + intRepairCompleted
                            intGoodsDeliveredTot = intGoodsDeliveredTot + intGoodsDelivered
                            intPendingTot = intPendingTot + intPending
                            intWarrantyTot = intWarrantyTot + intWarranty
                            intInWarrantyTot = intInWarrantyTot + intInWarranty
                            intOutWarrantyTot = intOutWarrantyTot + intOutWarranty
                            decInWarrantyAmountTot = decInWarrantyAmountTot + decInWarrantyAmount
                            decInWarrantyLabourTot = decInWarrantyLabourTot + decInWarrantyLabour
                            decInWarrantyPartsTot = decInWarrantyPartsTot + decInWarrantyParts
                            decInWarrantyTransportTot = decInWarrantyTransportTot + decInWarrantyTransport
                            decInWarrantyOthersTot = decInWarrantyOthersTot + decInWarrantyOthers
                            decOutWarrantyAmountTot = decOutWarrantyAmountTot + decOutWarrantyAmount
                            decOutWarrantyLabourTot = decOutWarrantyLabourTot + decOutWarrantyLabour
                            decOutWarrantyPartsTot = decOutWarrantyPartsTot + decOutWarrantyParts
                            decOutWarrantyTransportTot = decOutWarrantyTransportTot + decOutWarrantyTransport
                            decOutWarrantyOthersTot = decOutWarrantyOthersTot + decOutWarrantyOthers
                            decSawDiscountLabourTot = decSawDiscountLabourTot + decSawDiscountLabour
                            decSawDiscountPartsTot = decSawDiscountPartsTot + decSawDiscountParts
                            decOutWarrantyLabourwSawDiscTot = decOutWarrantyLabourwSawDiscTot + decOutWarrantyLabourwSawDisc
                            decOutWarrantyPartswSawDiscTot = decOutWarrantyPartswSawDiscTot + decOutWarrantyPartswSawDisc
                            decRevenueWithoutTaxTot = decRevenueWithoutTaxTot + decRevenueWithoutTax
                            decIwPartsConsumedTot = decIwPartsConsumedTot + decIwPartsConsumed
                            decTotalPartsConsumedTot = decTotalPartsConsumedTot + decTotalPartsConsumed
                            decPrimeCostTotalTot = decPrimeCostTotalTot + decPrimeCostTotal
                            decGrossProfitTot = decGrossProfitTot + decGrossProfit
                            decFinalPercentageTot = decFinalPercentageTot + decFinalPercentage

                            rowTitle = rowTitle & "," & strDate

                            rowNumberOfCounters = rowNumberOfCounters & "," '& intNumberOfCounters
                            rowNumberOfBusinessDat = rowNumberOfBusinessDat & "," '& intNumberOfBusinessDat
                            rowNumberOfStaffs = rowNumberOfStaffs & "," '& intNumberOfStaffs

                            rowCustomerVisit = rowCustomerVisit & "," & intCustomerVisit
                            rowCallRegistered = rowCallRegistered & "," & intCallRegistered
                            rowRepairCompleted = rowRepairCompleted & "," & intRepairCompleted
                            rowGoodsDelivered = rowGoodsDelivered & "," & intGoodsDelivered
                            rowPending = rowPending & "," & intPending
                            rowWarranty = rowWarranty & "," & intWarranty
                            rowInWarranty = rowInWarranty & "," & intInWarranty
                            rowOutWarranty = rowOutWarranty & "," & intOutWarranty
                            rowInWarrantyAmount = rowInWarrantyAmount & "," & decInWarrantyAmount
                            rowInWarrantyLabour = rowInWarrantyLabour & "," & decInWarrantyLabour
                            rowInWarrantyParts = rowInWarrantyParts & "," & decInWarrantyParts
                            rowInWarrantyTransport = rowInWarrantyTransport & "," & decInWarrantyTransport
                            rowInWarrantyOthers = rowInWarrantyOthers & "," & decInWarrantyOthers
                            rowOutWarrantyAmount = rowOutWarrantyAmount & "," & decOutWarrantyAmount
                            rowOutWarrantyLabour = rowOutWarrantyLabour & "," & decOutWarrantyLabour
                            rowOutWarrantyParts = rowOutWarrantyParts & "," & decOutWarrantyParts
                            rowOutWarrantyTransport = rowOutWarrantyTransport & "," & decOutWarrantyTransport
                            rowOutWarrantyOthers = rowOutWarrantyOthers & "," & decOutWarrantyOthers
                            rowSawDiscountLabour = rowSawDiscountLabour & "," & decSawDiscountLabour
                            rowSawDiscountParts = rowSawDiscountParts & "," & decSawDiscountParts
                            rowOutWarrantyLabourwSawDisc = rowOutWarrantyLabourwSawDisc & "," & decOutWarrantyLabourwSawDisc
                            rowOutWarrantyPartswSawDisc = rowOutWarrantyPartswSawDisc & "," & decOutWarrantyPartswSawDisc
                            rowRevenueWithoutTax = rowRevenueWithoutTax & "," & decRevenueWithoutTax
                            rowIwPartsConsumed = rowIwPartsConsumed & "," & decIwPartsConsumed
                            rowTotalPartsConsumed = rowTotalPartsConsumed & "," & decTotalPartsConsumed
                            rowPrimeCostTotal = rowPrimeCostTotal & "," & decPrimeCostTotal
                            rowGrossProfit = rowGrossProfit & "," & decGrossProfit
                            'rowFinalPercentage = rowFinalPercentage & "," & decFinalPercentage
                            If decRevenueWithoutTax = 0 Then
                                FinalPercentage1 = decGrossProfit
                            Else
                                FinalPercentage1 = comcontrol.Money_Round((decGrossProfit / decRevenueWithoutTax) * 100, 2)
                            End If
                            rowFinalPercentage = rowFinalPercentage & "," & FinalPercentage1 & "%" 'decFinalPercentage / 100


                        Next row
                    End If
                    'monMonday3Week & monSunday3Week
                    strDay = monMonday3Week.Day()
                    strMon = monMonday3Week.Month
                    strYear = monMonday3Week.Year

                    If Len(strDay) <= 1 Then
                        strDay = "0" & strDay
                    End If
                    If Len(strMon) <= 1 Then
                        strMon = "0" & strMon
                    End If
                    strDate = strYear & strMon & strDay
                    'Read from 
                    _dtPlReprt = comcontrol.SelectPlReport(monMonday3Week, monSunday3Week, DropListLocation.SelectedItem.Text, DropListLocation.SelectedItem.Value, Gm)

                    If (_dtPlReprt Is Nothing) Or (_dtPlReprt.Rows.Count = 0) Then
                    Else
                        For Each row As DataRow In _dtPlReprt.Rows
                            intNumberOfCounters = row.Item("NumberOfCounters")
                            intNumberOfBusinessDat = row.Item("NumberOfBusinessDat")
                            intNumberOfStaffs = row.Item("NumberOfStaffs")

                            intCustomerVisit = row.Item("CustomerVisit")
                            intCallRegistered = row.Item("CallRegistered")
                            intRepairCompleted = row.Item("RepairCompleted")
                            intGoodsDelivered = row.Item("GoodsDelivered")
                            intPending = row.Item("Pending")
                            intWarranty = row.Item("Warranty")
                            intInWarranty = row.Item("InWarranty")
                            intOutWarranty = row.Item("OutWarranty")
                            decInWarrantyAmount = row.Item("InWarrantyAmount")
                            decInWarrantyLabour = row.Item("InWarrantyLabour")
                            decInWarrantyParts = row.Item("InWarrantyParts")
                            decInWarrantyTransport = row.Item("InWarrantyTransport")
                            decInWarrantyOthers = row.Item("InWarrantyOthers")
                            decOutWarrantyAmount = row.Item("OutWarrantyAmount")
                            decOutWarrantyLabour = row.Item("OutWarrantyLabour")
                            decOutWarrantyParts = row.Item("OutWarrantyParts")
                            decOutWarrantyTransport = row.Item("OutWarrantyTransport")
                            decOutWarrantyOthers = row.Item("OutWarrantyOthers")
                            decSawDiscountLabour = row.Item("SawDiscountLabour")
                            decSawDiscountParts = row.Item("SawDiscountParts")
                            decOutWarrantyLabourwSawDisc = row.Item("OutWarrantyLabourwSawDisc")
                            decOutWarrantyPartswSawDisc = row.Item("OutWarrantyPartswSawDisc")
                            decRevenueWithoutTax = row.Item("RevenueWithoutTax")
                            decIwPartsConsumed = row.Item("IwPartsConsumed")
                            decTotalPartsConsumed = row.Item("TotalPartsConsumed")
                            decPrimeCostTotal = row.Item("PrimeCostTotal")
                            decGrossProfit = row.Item("GrossProfit")
                            decFinalPercentage = row.Item("FinalPercentage")

                            intNumberOfCountersTot = intNumberOfCountersTot + intNumberOfCounters
                            intNumberOfBusinessDatTot = intNumberOfBusinessDatTot + intNumberOfBusinessDat
                            intNumberOfStaffsTot = intNumberOfStaffsTot + intNumberOfStaffs

                            intCustomerVisitTot = intCustomerVisitTot + intCustomerVisit
                            intCallRegisteredTot = intCallRegisteredTot + intCallRegistered
                            intRepairCompletedTot = intRepairCompletedTot + intRepairCompleted
                            intGoodsDeliveredTot = intGoodsDeliveredTot + intGoodsDelivered
                            intPendingTot = intPendingTot + intPending
                            intWarrantyTot = intWarrantyTot + intWarranty
                            intInWarrantyTot = intInWarrantyTot + intInWarranty
                            intOutWarrantyTot = intOutWarrantyTot + intOutWarranty
                            decInWarrantyAmountTot = decInWarrantyAmountTot + decInWarrantyAmount
                            decInWarrantyLabourTot = decInWarrantyLabourTot + decInWarrantyLabour
                            decInWarrantyPartsTot = decInWarrantyPartsTot + decInWarrantyParts
                            decInWarrantyTransportTot = decInWarrantyTransportTot + decInWarrantyTransport
                            decInWarrantyOthersTot = decInWarrantyOthersTot + decInWarrantyOthers
                            decOutWarrantyAmountTot = decOutWarrantyAmountTot + decOutWarrantyAmount
                            decOutWarrantyLabourTot = decOutWarrantyLabourTot + decOutWarrantyLabour
                            decOutWarrantyPartsTot = decOutWarrantyPartsTot + decOutWarrantyParts
                            decOutWarrantyTransportTot = decOutWarrantyTransportTot + decOutWarrantyTransport
                            decOutWarrantyOthersTot = decOutWarrantyOthersTot + decOutWarrantyOthers
                            decSawDiscountLabourTot = decSawDiscountLabourTot + decSawDiscountLabour
                            decSawDiscountPartsTot = decSawDiscountPartsTot + decSawDiscountParts
                            decOutWarrantyLabourwSawDiscTot = decOutWarrantyLabourwSawDiscTot + decOutWarrantyLabourwSawDisc
                            decOutWarrantyPartswSawDiscTot = decOutWarrantyPartswSawDiscTot + decOutWarrantyPartswSawDisc
                            decRevenueWithoutTaxTot = decRevenueWithoutTaxTot + decRevenueWithoutTax
                            decIwPartsConsumedTot = decIwPartsConsumedTot + decIwPartsConsumed
                            decTotalPartsConsumedTot = decTotalPartsConsumedTot + decTotalPartsConsumed
                            decPrimeCostTotalTot = decPrimeCostTotalTot + decPrimeCostTotal
                            decGrossProfitTot = decGrossProfitTot + decGrossProfit
                            decFinalPercentageTot = decFinalPercentageTot + decFinalPercentage

                            rowTitle = rowTitle & "," & strDate

                            rowNumberOfCounters = rowNumberOfCounters & "," '& intNumberOfCounters
                            rowNumberOfBusinessDat = rowNumberOfBusinessDat & "," '& intNumberOfBusinessDat
                            rowNumberOfStaffs = rowNumberOfStaffs & "," '& intNumberOfStaffs

                            rowCustomerVisit = rowCustomerVisit & "," & intCustomerVisit
                            rowCallRegistered = rowCallRegistered & "," & intCallRegistered
                            rowRepairCompleted = rowRepairCompleted & "," & intRepairCompleted
                            rowGoodsDelivered = rowGoodsDelivered & "," & intGoodsDelivered
                            rowPending = rowPending & "," & intPending
                            rowWarranty = rowWarranty & "," & intWarranty
                            rowInWarranty = rowInWarranty & "," & intInWarranty
                            rowOutWarranty = rowOutWarranty & "," & intOutWarranty
                            rowInWarrantyAmount = rowInWarrantyAmount & "," & decInWarrantyAmount
                            rowInWarrantyLabour = rowInWarrantyLabour & "," & decInWarrantyLabour
                            rowInWarrantyParts = rowInWarrantyParts & "," & decInWarrantyParts
                            rowInWarrantyTransport = rowInWarrantyTransport & "," & decInWarrantyTransport
                            rowInWarrantyOthers = rowInWarrantyOthers & "," & decInWarrantyOthers
                            rowOutWarrantyAmount = rowOutWarrantyAmount & "," & decOutWarrantyAmount
                            rowOutWarrantyLabour = rowOutWarrantyLabour & "," & decOutWarrantyLabour
                            rowOutWarrantyParts = rowOutWarrantyParts & "," & decOutWarrantyParts
                            rowOutWarrantyTransport = rowOutWarrantyTransport & "," & decOutWarrantyTransport
                            rowOutWarrantyOthers = rowOutWarrantyOthers & "," & decOutWarrantyOthers
                            rowSawDiscountLabour = rowSawDiscountLabour & "," & decSawDiscountLabour
                            rowSawDiscountParts = rowSawDiscountParts & "," & decSawDiscountParts
                            rowOutWarrantyLabourwSawDisc = rowOutWarrantyLabourwSawDisc & "," & decOutWarrantyLabourwSawDisc
                            rowOutWarrantyPartswSawDisc = rowOutWarrantyPartswSawDisc & "," & decOutWarrantyPartswSawDisc
                            rowRevenueWithoutTax = rowRevenueWithoutTax & "," & decRevenueWithoutTax
                            rowIwPartsConsumed = rowIwPartsConsumed & "," & decIwPartsConsumed
                            rowTotalPartsConsumed = rowTotalPartsConsumed & "," & decTotalPartsConsumed
                            rowPrimeCostTotal = rowPrimeCostTotal & "," & decPrimeCostTotal
                            rowGrossProfit = rowGrossProfit & "," & decGrossProfit
                            'rowFinalPercentage = rowFinalPercentage & "," & decFinalPercentage
                            If decRevenueWithoutTax = 0 Then
                                FinalPercentage1 = decGrossProfit
                            Else
                                FinalPercentage1 = comcontrol.Money_Round((decGrossProfit / decRevenueWithoutTax) * 100, 2)
                            End If
                            rowFinalPercentage = rowFinalPercentage & "," & FinalPercentage1 & "%" 'decFinalPercentage / 100


                        Next row
                    End If
                    'monMonday4Week & monSunday4Week
                    strDay = monMonday4Week.Day()
                    strMon = monMonday4Week.Month
                    strYear = monMonday4Week.Year

                    If Len(strDay) <= 1 Then
                        strDay = "0" & strDay
                    End If
                    If Len(strMon) <= 1 Then
                        strMon = "0" & strMon
                    End If
                    strDate = strYear & strMon & strDay
                    'Read from 
                    _dtPlReprt = comcontrol.SelectPlReport(monMonday4Week, monMonday4Week, DropListLocation.SelectedItem.Text, DropListLocation.SelectedItem.Value, Gm)

                    If (_dtPlReprt Is Nothing) Or (_dtPlReprt.Rows.Count = 0) Then
                    Else
                        For Each row As DataRow In _dtPlReprt.Rows

                            intNumberOfCounters = row.Item("NumberOfCounters")
                            intNumberOfBusinessDat = row.Item("NumberOfBusinessDat")
                            intNumberOfStaffs = row.Item("NumberOfStaffs")

                            intCustomerVisit = row.Item("CustomerVisit")
                            intCallRegistered = row.Item("CallRegistered")
                            intRepairCompleted = row.Item("RepairCompleted")
                            intGoodsDelivered = row.Item("GoodsDelivered")
                            intPending = row.Item("Pending")
                            intWarranty = row.Item("Warranty")
                            intInWarranty = row.Item("InWarranty")
                            intOutWarranty = row.Item("OutWarranty")
                            decInWarrantyAmount = row.Item("InWarrantyAmount")
                            decInWarrantyLabour = row.Item("InWarrantyLabour")
                            decInWarrantyParts = row.Item("InWarrantyParts")
                            decInWarrantyTransport = row.Item("InWarrantyTransport")
                            decInWarrantyOthers = row.Item("InWarrantyOthers")
                            decOutWarrantyAmount = row.Item("OutWarrantyAmount")
                            decOutWarrantyLabour = row.Item("OutWarrantyLabour")
                            decOutWarrantyParts = row.Item("OutWarrantyParts")
                            decOutWarrantyTransport = row.Item("OutWarrantyTransport")
                            decOutWarrantyOthers = row.Item("OutWarrantyOthers")
                            decSawDiscountLabour = row.Item("SawDiscountLabour")
                            decSawDiscountParts = row.Item("SawDiscountParts")
                            decOutWarrantyLabourwSawDisc = row.Item("OutWarrantyLabourwSawDisc")
                            decOutWarrantyPartswSawDisc = row.Item("OutWarrantyPartswSawDisc")
                            decRevenueWithoutTax = row.Item("RevenueWithoutTax")
                            decIwPartsConsumed = row.Item("IwPartsConsumed")
                            decTotalPartsConsumed = row.Item("TotalPartsConsumed")
                            decPrimeCostTotal = row.Item("PrimeCostTotal")
                            decGrossProfit = row.Item("GrossProfit")
                            decFinalPercentage = row.Item("FinalPercentage")


                            intNumberOfCountersTot = intNumberOfCountersTot + intNumberOfCounters
                            intNumberOfBusinessDatTot = intNumberOfBusinessDatTot + intNumberOfBusinessDat
                            intNumberOfStaffsTot = intNumberOfStaffsTot + intNumberOfStaffs

                            intCustomerVisitTot = intCustomerVisitTot + intCustomerVisit
                            intCallRegisteredTot = intCallRegisteredTot + intCallRegistered
                            intRepairCompletedTot = intRepairCompletedTot + intRepairCompleted
                            intGoodsDeliveredTot = intGoodsDeliveredTot + intGoodsDelivered
                            intPendingTot = intPendingTot + intPending
                            intWarrantyTot = intWarrantyTot + intWarranty
                            intInWarrantyTot = intInWarrantyTot + intInWarranty
                            intOutWarrantyTot = intOutWarrantyTot + intOutWarranty
                            decInWarrantyAmountTot = decInWarrantyAmountTot + decInWarrantyAmount
                            decInWarrantyLabourTot = decInWarrantyLabourTot + decInWarrantyLabour
                            decInWarrantyPartsTot = decInWarrantyPartsTot + decInWarrantyParts
                            decInWarrantyTransportTot = decInWarrantyTransportTot + decInWarrantyTransport
                            decInWarrantyOthersTot = decInWarrantyOthersTot + decInWarrantyOthers
                            decOutWarrantyAmountTot = decOutWarrantyAmountTot + decOutWarrantyAmount
                            decOutWarrantyLabourTot = decOutWarrantyLabourTot + decOutWarrantyLabour
                            decOutWarrantyPartsTot = decOutWarrantyPartsTot + decOutWarrantyParts
                            decOutWarrantyTransportTot = decOutWarrantyTransportTot + decOutWarrantyTransport
                            decOutWarrantyOthersTot = decOutWarrantyOthersTot + decOutWarrantyOthers
                            decSawDiscountLabourTot = decSawDiscountLabourTot + decSawDiscountLabour
                            decSawDiscountPartsTot = decSawDiscountPartsTot + decSawDiscountParts
                            decOutWarrantyLabourwSawDiscTot = decOutWarrantyLabourwSawDiscTot + decOutWarrantyLabourwSawDisc
                            decOutWarrantyPartswSawDiscTot = decOutWarrantyPartswSawDiscTot + decOutWarrantyPartswSawDisc
                            decRevenueWithoutTaxTot = decRevenueWithoutTaxTot + decRevenueWithoutTax
                            decIwPartsConsumedTot = decIwPartsConsumedTot + decIwPartsConsumed
                            decTotalPartsConsumedTot = decTotalPartsConsumedTot + decTotalPartsConsumed
                            decPrimeCostTotalTot = decPrimeCostTotalTot + decPrimeCostTotal
                            decGrossProfitTot = decGrossProfitTot + decGrossProfit
                            decFinalPercentageTot = decFinalPercentageTot + decFinalPercentage


                            rowTitle = rowTitle & "," & strDate

                            rowNumberOfCounters = rowNumberOfCounters & "," '& intNumberOfCounters
                            rowNumberOfBusinessDat = rowNumberOfBusinessDat & "," '& intNumberOfBusinessDat
                            rowNumberOfStaffs = rowNumberOfStaffs & "," '& intNumberOfStaffs

                            rowCustomerVisit = rowCustomerVisit & "," & intCustomerVisit
                            rowCallRegistered = rowCallRegistered & "," & intCallRegistered
                            rowRepairCompleted = rowRepairCompleted & "," & intRepairCompleted
                            rowGoodsDelivered = rowGoodsDelivered & "," & intGoodsDelivered
                            rowPending = rowPending & "," & intPending
                            rowWarranty = rowWarranty & "," & intWarranty
                            rowInWarranty = rowInWarranty & "," & intInWarranty
                            rowOutWarranty = rowOutWarranty & "," & intOutWarranty
                            rowInWarrantyAmount = rowInWarrantyAmount & "," & decInWarrantyAmount
                            rowInWarrantyLabour = rowInWarrantyLabour & "," & decInWarrantyLabour
                            rowInWarrantyParts = rowInWarrantyParts & "," & decInWarrantyParts
                            rowInWarrantyTransport = rowInWarrantyTransport & "," & decInWarrantyTransport
                            rowInWarrantyOthers = rowInWarrantyOthers & "," & decInWarrantyOthers
                            rowOutWarrantyAmount = rowOutWarrantyAmount & "," & decOutWarrantyAmount
                            rowOutWarrantyLabour = rowOutWarrantyLabour & "," & decOutWarrantyLabour
                            rowOutWarrantyParts = rowOutWarrantyParts & "," & decOutWarrantyParts
                            rowOutWarrantyTransport = rowOutWarrantyTransport & "," & decOutWarrantyTransport
                            rowOutWarrantyOthers = rowOutWarrantyOthers & "," & decOutWarrantyOthers
                            rowSawDiscountLabour = rowSawDiscountLabour & "," & decSawDiscountLabour
                            rowSawDiscountParts = rowSawDiscountParts & "," & decSawDiscountParts
                            rowOutWarrantyLabourwSawDisc = rowOutWarrantyLabourwSawDisc & "," & decOutWarrantyLabourwSawDisc
                            rowOutWarrantyPartswSawDisc = rowOutWarrantyPartswSawDisc & "," & decOutWarrantyPartswSawDisc
                            rowRevenueWithoutTax = rowRevenueWithoutTax & "," & decRevenueWithoutTax
                            rowIwPartsConsumed = rowIwPartsConsumed & "," & decIwPartsConsumed
                            rowTotalPartsConsumed = rowTotalPartsConsumed & "," & decTotalPartsConsumed
                            rowPrimeCostTotal = rowPrimeCostTotal & "," & decPrimeCostTotal
                            rowGrossProfit = rowGrossProfit & "," & decGrossProfit
                            'rowFinalPercentage = rowFinalPercentage & "," & decFinalPercentage
                            If decRevenueWithoutTax = 0 Then
                                FinalPercentage1 = decGrossProfit
                            Else
                                FinalPercentage1 = comcontrol.Money_Round((decGrossProfit / decRevenueWithoutTax) * 100, 2)
                            End If
                            rowFinalPercentage = rowFinalPercentage & "," & FinalPercentage1 & "%" 'decFinalPercentage / 100


                        Next row
                    End If
                    'monMonday5Week & monSunday5Week
                    If blWeek5 Then
                        '     lblLoc.Text = lblLoc.Text & "<br>5th  Week From: " & monMonday5Week & "  To: " & monSunday5Week
                        strDay = monMonday5Week.Day()
                        strMon = monMonday5Week.Month
                        strYear = monMonday5Week.Year

                        If Len(strDay) <= 1 Then
                            strDay = "0" & strDay
                        End If
                        If Len(strMon) <= 1 Then
                            strMon = "0" & strMon
                        End If
                        strDate = strYear & strMon & strDay
                        'Read from 
                        _dtPlReprt = comcontrol.SelectPlReport(monMonday5Week, monSunday5Week, DropListLocation.SelectedItem.Text, DropListLocation.SelectedItem.Value, Gm)

                        If (_dtPlReprt Is Nothing) Or (_dtPlReprt.Rows.Count = 0) Then
                        Else
                            For Each row As DataRow In _dtPlReprt.Rows

                                intNumberOfCounters = row.Item("NumberOfCounters")
                                intNumberOfBusinessDat = row.Item("NumberOfBusinessDat")
                                intNumberOfStaffs = row.Item("NumberOfStaffs")

                                intCustomerVisit = row.Item("CustomerVisit")
                                intCallRegistered = row.Item("CallRegistered")
                                intRepairCompleted = row.Item("RepairCompleted")
                                intGoodsDelivered = row.Item("GoodsDelivered")
                                intPending = row.Item("Pending")
                                intWarranty = row.Item("Warranty")
                                intInWarranty = row.Item("InWarranty")
                                intOutWarranty = row.Item("OutWarranty")
                                decInWarrantyAmount = row.Item("InWarrantyAmount")
                                decInWarrantyLabour = row.Item("InWarrantyLabour")
                                decInWarrantyParts = row.Item("InWarrantyParts")
                                decInWarrantyTransport = row.Item("InWarrantyTransport")
                                decInWarrantyOthers = row.Item("InWarrantyOthers")
                                decOutWarrantyAmount = row.Item("OutWarrantyAmount")
                                decOutWarrantyLabour = row.Item("OutWarrantyLabour")
                                decOutWarrantyParts = row.Item("OutWarrantyParts")
                                decOutWarrantyTransport = row.Item("OutWarrantyTransport")
                                decOutWarrantyOthers = row.Item("OutWarrantyOthers")
                                decSawDiscountLabour = row.Item("SawDiscountLabour")
                                decSawDiscountParts = row.Item("SawDiscountParts")
                                decOutWarrantyLabourwSawDisc = row.Item("OutWarrantyLabourwSawDisc")
                                decOutWarrantyPartswSawDisc = row.Item("OutWarrantyPartswSawDisc")
                                decRevenueWithoutTax = row.Item("RevenueWithoutTax")
                                decIwPartsConsumed = row.Item("IwPartsConsumed")
                                decTotalPartsConsumed = row.Item("TotalPartsConsumed")
                                decPrimeCostTotal = row.Item("PrimeCostTotal")
                                decGrossProfit = row.Item("GrossProfit")
                                decFinalPercentage = row.Item("FinalPercentage")

                                intNumberOfCountersTot = intNumberOfCountersTot + intNumberOfCounters
                                intNumberOfBusinessDatTot = intNumberOfBusinessDatTot + intNumberOfBusinessDat
                                intNumberOfStaffsTot = intNumberOfStaffsTot + intNumberOfStaffs

                                intCustomerVisitTot = intCustomerVisitTot + intCustomerVisit
                                intCallRegisteredTot = intCallRegisteredTot + intCallRegistered
                                intRepairCompletedTot = intRepairCompletedTot + intRepairCompleted
                                intGoodsDeliveredTot = intGoodsDeliveredTot + intGoodsDelivered
                                intPendingTot = intPendingTot + intPending
                                intWarrantyTot = intWarrantyTot + intWarranty
                                intInWarrantyTot = intInWarrantyTot + intInWarranty
                                intOutWarrantyTot = intOutWarrantyTot + intOutWarranty
                                decInWarrantyAmountTot = decInWarrantyAmountTot + decInWarrantyAmount
                                decInWarrantyLabourTot = decInWarrantyLabourTot + decInWarrantyLabour
                                decInWarrantyPartsTot = decInWarrantyPartsTot + decInWarrantyParts
                                decInWarrantyTransportTot = decInWarrantyTransportTot + decInWarrantyTransport
                                decInWarrantyOthersTot = decInWarrantyOthersTot + decInWarrantyOthers
                                decOutWarrantyAmountTot = decOutWarrantyAmountTot + decOutWarrantyAmount
                                decOutWarrantyLabourTot = decOutWarrantyLabourTot + decOutWarrantyLabour
                                decOutWarrantyPartsTot = decOutWarrantyPartsTot + decOutWarrantyParts
                                decOutWarrantyTransportTot = decOutWarrantyTransportTot + decOutWarrantyTransport
                                decOutWarrantyOthersTot = decOutWarrantyOthersTot + decOutWarrantyOthers
                                decSawDiscountLabourTot = decSawDiscountLabourTot + decSawDiscountLabour
                                decSawDiscountPartsTot = decSawDiscountPartsTot + decSawDiscountParts
                                decOutWarrantyLabourwSawDiscTot = decOutWarrantyLabourwSawDiscTot + decOutWarrantyLabourwSawDisc
                                decOutWarrantyPartswSawDiscTot = decOutWarrantyPartswSawDiscTot + decOutWarrantyPartswSawDisc
                                decRevenueWithoutTaxTot = decRevenueWithoutTaxTot + decRevenueWithoutTax
                                decIwPartsConsumedTot = decIwPartsConsumedTot + decIwPartsConsumed
                                decTotalPartsConsumedTot = decTotalPartsConsumedTot + decTotalPartsConsumed
                                decPrimeCostTotalTot = decPrimeCostTotalTot + decPrimeCostTotal
                                decGrossProfitTot = decGrossProfitTot + decGrossProfit
                                decFinalPercentageTot = decFinalPercentageTot + decFinalPercentage

                                rowTitle = rowTitle & "," & strDate

                                rowNumberOfCounters = rowNumberOfCounters & "," '& intNumberOfCounters
                                rowNumberOfBusinessDat = rowNumberOfBusinessDat & "," '& intNumberOfBusinessDat
                                rowNumberOfStaffs = rowNumberOfStaffs & "," '& intNumberOfStaffs

                                rowCustomerVisit = rowCustomerVisit & "," & intCustomerVisit
                                rowCallRegistered = rowCallRegistered & "," & intCallRegistered
                                rowRepairCompleted = rowRepairCompleted & "," & intRepairCompleted
                                rowGoodsDelivered = rowGoodsDelivered & "," & intGoodsDelivered
                                rowPending = rowPending & "," & intPending
                                rowWarranty = rowWarranty & "," & intWarranty
                                rowInWarranty = rowInWarranty & "," & intInWarranty
                                rowOutWarranty = rowOutWarranty & "," & intOutWarranty
                                rowInWarrantyAmount = rowInWarrantyAmount & "," & decInWarrantyAmount
                                rowInWarrantyLabour = rowInWarrantyLabour & "," & decInWarrantyLabour
                                rowInWarrantyParts = rowInWarrantyParts & "," & decInWarrantyParts
                                rowInWarrantyTransport = rowInWarrantyTransport & "," & decInWarrantyTransport
                                rowInWarrantyOthers = rowInWarrantyOthers & "," & decInWarrantyOthers
                                rowOutWarrantyAmount = rowOutWarrantyAmount & "," & decOutWarrantyAmount
                                rowOutWarrantyLabour = rowOutWarrantyLabour & "," & decOutWarrantyLabour
                                rowOutWarrantyParts = rowOutWarrantyParts & "," & decOutWarrantyParts
                                rowOutWarrantyTransport = rowOutWarrantyTransport & "," & decOutWarrantyTransport
                                rowOutWarrantyOthers = rowOutWarrantyOthers & "," & decOutWarrantyOthers
                                rowSawDiscountLabour = rowSawDiscountLabour & "," & decSawDiscountLabour
                                rowSawDiscountParts = rowSawDiscountParts & "," & decSawDiscountParts
                                rowOutWarrantyLabourwSawDisc = rowOutWarrantyLabourwSawDisc & "," & decOutWarrantyLabourwSawDisc
                                rowOutWarrantyPartswSawDisc = rowOutWarrantyPartswSawDisc & "," & decOutWarrantyPartswSawDisc
                                rowRevenueWithoutTax = rowRevenueWithoutTax & "," & decRevenueWithoutTax
                                rowIwPartsConsumed = rowIwPartsConsumed & "," & decIwPartsConsumed
                                rowTotalPartsConsumed = rowTotalPartsConsumed & "," & decTotalPartsConsumed
                                rowPrimeCostTotal = rowPrimeCostTotal & "," & decPrimeCostTotal
                                rowGrossProfit = rowGrossProfit & "," & decGrossProfit
                                'rowFinalPercentage = rowFinalPercentage & "," & decFinalPercentage
                                If decRevenueWithoutTax = 0 Then
                                    FinalPercentage1 = decGrossProfit
                                Else
                                    FinalPercentage1 = comcontrol.Money_Round((decGrossProfit / decRevenueWithoutTax) * 100, 2)
                                End If
                                rowFinalPercentage = rowFinalPercentage & "," & FinalPercentage1 & "%" 'decFinalPercentage / 100


                            Next row
                        End If
                    End If

                    'monMonday6Week & monSunday6Week
                    If blWeek6 Then
                        '   lblLoc.Text = lblLoc.Text & "<br>6th  Week From: " & monMonday6Week & "  To: " & monSunday6Week
                        strDay = monMonday6Week.Day()
                        strMon = monMonday6Week.Month
                        strYear = monMonday6Week.Year

                        If Len(strDay) <= 1 Then
                            strDay = "0" & strDay
                        End If
                        If Len(strMon) <= 1 Then
                            strMon = "0" & strMon
                        End If
                        strDate = strYear & strMon & strDay
                        'Read from 
                        _dtPlReprt = comcontrol.SelectPlReport(monMonday6Week, monSunday6Week, DropListLocation.SelectedItem.Text, DropListLocation.SelectedItem.Value, Gm)

                        If (_dtPlReprt Is Nothing) Or (_dtPlReprt.Rows.Count = 0) Then
                        Else
                            For Each row As DataRow In _dtPlReprt.Rows

                                intNumberOfCounters = row.Item("NumberOfCounters")
                                intNumberOfBusinessDat = row.Item("NumberOfBusinessDat")
                                intNumberOfStaffs = row.Item("NumberOfStaffs")

                                intCustomerVisit = row.Item("CustomerVisit")
                                intCallRegistered = row.Item("CallRegistered")
                                intRepairCompleted = row.Item("RepairCompleted")
                                intGoodsDelivered = row.Item("GoodsDelivered")
                                intPending = row.Item("Pending")
                                intWarranty = row.Item("Warranty")
                                intInWarranty = row.Item("InWarranty")
                                intOutWarranty = row.Item("OutWarranty")
                                decInWarrantyAmount = row.Item("InWarrantyAmount")
                                decInWarrantyLabour = row.Item("InWarrantyLabour")
                                decInWarrantyParts = row.Item("InWarrantyParts")
                                decInWarrantyTransport = row.Item("InWarrantyTransport")
                                decInWarrantyOthers = row.Item("InWarrantyOthers")
                                decOutWarrantyAmount = row.Item("OutWarrantyAmount")
                                decOutWarrantyLabour = row.Item("OutWarrantyLabour")
                                decOutWarrantyParts = row.Item("OutWarrantyParts")
                                decOutWarrantyTransport = row.Item("OutWarrantyTransport")
                                decOutWarrantyOthers = row.Item("OutWarrantyOthers")
                                decSawDiscountLabour = row.Item("SawDiscountLabour")
                                decSawDiscountParts = row.Item("SawDiscountParts")
                                decOutWarrantyLabourwSawDisc = row.Item("OutWarrantyLabourwSawDisc")
                                decOutWarrantyPartswSawDisc = row.Item("OutWarrantyPartswSawDisc")
                                decRevenueWithoutTax = row.Item("RevenueWithoutTax")
                                decIwPartsConsumed = row.Item("IwPartsConsumed")
                                decTotalPartsConsumed = row.Item("TotalPartsConsumed")
                                decPrimeCostTotal = row.Item("PrimeCostTotal")
                                decGrossProfit = row.Item("GrossProfit")
                                decFinalPercentage = row.Item("FinalPercentage")

                                intNumberOfCountersTot = intNumberOfCountersTot + intNumberOfCounters
                                intNumberOfBusinessDatTot = intNumberOfBusinessDatTot + intNumberOfBusinessDat
                                intNumberOfStaffsTot = intNumberOfStaffsTot + intNumberOfStaffs

                                intCustomerVisitTot = intCustomerVisitTot + intCustomerVisit
                                intCallRegisteredTot = intCallRegisteredTot + intCallRegistered
                                intRepairCompletedTot = intRepairCompletedTot + intRepairCompleted
                                intGoodsDeliveredTot = intGoodsDeliveredTot + intGoodsDelivered
                                intPendingTot = intPendingTot + intPending
                                intWarrantyTot = intWarrantyTot + intWarranty
                                intInWarrantyTot = intInWarrantyTot + intInWarranty
                                intOutWarrantyTot = intOutWarrantyTot + intOutWarranty
                                decInWarrantyAmountTot = decInWarrantyAmountTot + decInWarrantyAmount
                                decInWarrantyLabourTot = decInWarrantyLabourTot + decInWarrantyLabour
                                decInWarrantyPartsTot = decInWarrantyPartsTot + decInWarrantyParts
                                decInWarrantyTransportTot = decInWarrantyTransportTot + decInWarrantyTransport
                                decInWarrantyOthersTot = decInWarrantyOthersTot + decInWarrantyOthers
                                decOutWarrantyAmountTot = decOutWarrantyAmountTot + decOutWarrantyAmount
                                decOutWarrantyLabourTot = decOutWarrantyLabourTot + decOutWarrantyLabour
                                decOutWarrantyPartsTot = decOutWarrantyPartsTot + decOutWarrantyParts
                                decOutWarrantyTransportTot = decOutWarrantyTransportTot + decOutWarrantyTransport
                                decOutWarrantyOthersTot = decOutWarrantyOthersTot + decOutWarrantyOthers
                                decSawDiscountLabourTot = decSawDiscountLabourTot + decSawDiscountLabour
                                decSawDiscountPartsTot = decSawDiscountPartsTot + decSawDiscountParts
                                decOutWarrantyLabourwSawDiscTot = decOutWarrantyLabourwSawDiscTot + decOutWarrantyLabourwSawDisc
                                decOutWarrantyPartswSawDiscTot = decOutWarrantyPartswSawDiscTot + decOutWarrantyPartswSawDisc
                                decRevenueWithoutTaxTot = decRevenueWithoutTaxTot + decRevenueWithoutTax
                                decIwPartsConsumedTot = decIwPartsConsumedTot + decIwPartsConsumed
                                decTotalPartsConsumedTot = decTotalPartsConsumedTot + decTotalPartsConsumed
                                decPrimeCostTotalTot = decPrimeCostTotalTot + decPrimeCostTotal
                                decGrossProfitTot = decGrossProfitTot + decGrossProfit
                                decFinalPercentageTot = decFinalPercentageTot + decFinalPercentage

                                rowTitle = rowTitle & "," & strDate

                                rowNumberOfCounters = rowNumberOfCounters & "," '& intNumberOfCounters
                                rowNumberOfBusinessDat = rowNumberOfBusinessDat & "," '& intNumberOfBusinessDat
                                rowNumberOfStaffs = rowNumberOfStaffs & "," '& intNumberOfStaffs

                                rowCustomerVisit = rowCustomerVisit & "," & intCustomerVisit
                                rowCallRegistered = rowCallRegistered & "," & intCallRegistered
                                rowRepairCompleted = rowRepairCompleted & "," & intRepairCompleted
                                rowGoodsDelivered = rowGoodsDelivered & "," & intGoodsDelivered
                                rowPending = rowPending & "," & intPending
                                rowWarranty = rowWarranty & "," & intWarranty
                                rowInWarranty = rowInWarranty & "," & intInWarranty
                                rowOutWarranty = rowOutWarranty & "," & intOutWarranty
                                rowInWarrantyAmount = rowInWarrantyAmount & "," & decInWarrantyAmount
                                rowInWarrantyLabour = rowInWarrantyLabour & "," & decInWarrantyLabour
                                rowInWarrantyParts = rowInWarrantyParts & "," & decInWarrantyParts
                                rowInWarrantyTransport = rowInWarrantyTransport & "," & decInWarrantyTransport
                                rowInWarrantyOthers = rowInWarrantyOthers & "," & decInWarrantyOthers
                                rowOutWarrantyAmount = rowOutWarrantyAmount & "," & decOutWarrantyAmount
                                rowOutWarrantyLabour = rowOutWarrantyLabour & "," & decOutWarrantyLabour
                                rowOutWarrantyParts = rowOutWarrantyParts & "," & decOutWarrantyParts
                                rowOutWarrantyTransport = rowOutWarrantyTransport & "," & decOutWarrantyTransport
                                rowOutWarrantyOthers = rowOutWarrantyOthers & "," & decOutWarrantyOthers
                                rowSawDiscountLabour = rowSawDiscountLabour & "," & decSawDiscountLabour
                                rowSawDiscountParts = rowSawDiscountParts & "," & decSawDiscountParts
                                rowOutWarrantyLabourwSawDisc = rowOutWarrantyLabourwSawDisc & "," & decOutWarrantyLabourwSawDisc
                                rowOutWarrantyPartswSawDisc = rowOutWarrantyPartswSawDisc & "," & decOutWarrantyPartswSawDisc
                                rowRevenueWithoutTax = rowRevenueWithoutTax & "," & decRevenueWithoutTax
                                rowIwPartsConsumed = rowIwPartsConsumed & "," & decIwPartsConsumed
                                rowTotalPartsConsumed = rowTotalPartsConsumed & "," & decTotalPartsConsumed
                                rowPrimeCostTotal = rowPrimeCostTotal & "," & decPrimeCostTotal
                                rowGrossProfit = rowGrossProfit & "," & decGrossProfit
                                'rowFinalPercentage = rowFinalPercentage & "," & decFinalPercentage
                                If decRevenueWithoutTax = 0 Then
                                    FinalPercentage1 = decGrossProfit
                                Else
                                    FinalPercentage1 = comcontrol.Money_Round((decGrossProfit / decRevenueWithoutTax) * 100, 2)
                                End If
                                rowFinalPercentage = rowFinalPercentage & "," & FinalPercentage1 & "%" 'decFinalPercentage / 100

                            Next row
                        End If
                    End If


                    'lblLoc.Text = lblLoc.Text & "<br>" & DropListLocation.SelectedItem.Text
                    Exit For
                End If
            Next


            If DropListLocation.SelectedItem.Text = "ALL" Then
                'For Total Displa
                Dim strTmp1 As String = ""
                Dim strTmp2 As String = ""
                Dim strTmp3 As String = ""
                Dim strTmp4 As String = ""
                Dim strTmp5 As String = ""
                Dim strTmp6 As String = ""
                Dim strTmp7 As String = ""
                Dim strTmp8 As String = ""
                Dim strTmp9 As String = ""
                Dim strTmp10 As String = ""
                Dim strTmp11 As String = ""
                Dim strTmp12 As String = ""
                Dim strTmp13 As String = ""
                Dim strTmp14 As String = ""
                Dim strTmp15 As String = ""
                Dim strTmp16 As String = ""
                Dim strTmp17 As String = ""
                Dim strTmp18 As String = ""
                Dim strTmp19 As String = ""
                Dim strTmp20 As String = ""
                Dim strTmp21 As String = ""
                Dim strTmp22 As String = ""
                Dim strTmp23 As String = ""
                Dim strTmp24 As String = ""
                Dim strTmp25 As String = ""
                Dim strTmp26 As String = ""
                Dim strTmp27 As String = ""

                Dim strTmp28 As String = ""
                Dim strTmp29 As String = ""
                Dim strTmp30 As String = ""
                Dim strTmp31 As String = ""
                Dim strTmp32 As String = ""



                If blWeek5 Then
                    strTmp1 = "," & SscTotals(4, 0)
                    strTmp2 = "," & SscTotals(4, 1)
                    strTmp3 = "," & SscTotals(4, 2)
                    strTmp4 = "," & SscTotals(4, 3)
                    strTmp5 = "," & SscTotals(4, 4)
                    strTmp6 = "," & SscTotals(4, 5)
                    strTmp7 = "," & SscTotals(4, 6)
                    strTmp8 = "," & SscTotals(4, 7)
                    strTmp9 = "," & SscTotals(4, 8)
                    strTmp10 = "," & SscTotals(4, 9)
                    strTmp11 = "," & SscTotals(4, 10)
                    strTmp12 = "," & SscTotals(4, 11)
                    strTmp13 = "," & SscTotals(4, 12)
                    strTmp14 = "," & SscTotals(4, 13)
                    strTmp15 = "," & SscTotals(4, 14)
                    strTmp16 = "," & SscTotals(4, 15)
                    strTmp17 = "," & SscTotals(4, 16)
                    strTmp18 = "," & SscTotals(4, 17)
                    strTmp19 = "," & SscTotals(4, 18)
                    strTmp20 = "," & SscTotals(4, 19)
                    strTmp21 = "," & SscTotals(4, 20)
                    strTmp22 = "," & SscTotals(4, 21)
                    strTmp23 = "," & SscTotals(4, 22)
                    strTmp24 = "," & SscTotals(4, 23)
                    strTmp25 = "," & SscTotals(4, 24)
                    strTmp26 = "," & SscTotals(4, 25)
                    strTmp27 = "," & SscTotals(4, 26) 'rowRevenueWithoutTax

                    strTmp28 = "," & SscTotals(4, 27)
                    strTmp29 = "," & SscTotals(4, 28)
                    strTmp30 = "," & SscTotals(4, 29)
                    strTmp31 = "," & SscTotals(4, 30) 'rowGrossProfit 
                    If SscTotals(4, 26) = 0 Then
                        FinalPercentage1 = SscTotals(4, 30)
                    Else
                        FinalPercentage1 = comcontrol.Money_Round((SscTotals(4, 30) / SscTotals(4, 26)) * 100, 2)
                    End If
                    strTmp32 = "," & FinalPercentage1 & "%"

                End If
                If blWeek6 Then
                    strTmp1 = strTmp1 & "," & SscTotals(5, 0)
                    strTmp2 = strTmp2 & "," & SscTotals(5, 1)
                    strTmp3 = strTmp3 & "," & SscTotals(5, 2)
                    strTmp4 = strTmp4 & "," & SscTotals(5, 3)
                    strTmp5 = strTmp5 & "," & SscTotals(5, 4)
                    strTmp6 = strTmp6 & "," & SscTotals(5, 5)
                    strTmp7 = strTmp7 & "," & SscTotals(5, 6)
                    strTmp8 = strTmp8 & "," & SscTotals(5, 7)
                    strTmp9 = strTmp9 & "," & SscTotals(5, 8)
                    strTmp10 = strTmp10 & "," & SscTotals(5, 9)
                    strTmp11 = strTmp11 & "," & SscTotals(5, 10)
                    strTmp12 = strTmp12 & "," & SscTotals(5, 11)
                    strTmp13 = strTmp13 & "," & SscTotals(5, 12)
                    strTmp14 = strTmp14 & "," & SscTotals(5, 13)
                    strTmp15 = strTmp15 & "," & SscTotals(5, 14)
                    strTmp16 = strTmp16 & "," & SscTotals(5, 15)
                    strTmp17 = strTmp17 & "," & SscTotals(5, 16)
                    strTmp18 = strTmp18 & "," & SscTotals(5, 17)
                    strTmp19 = strTmp19 & "," & SscTotals(5, 18)
                    strTmp20 = strTmp20 & "," & SscTotals(5, 19)
                    strTmp21 = strTmp21 & "," & SscTotals(5, 20)
                    strTmp22 = strTmp22 & "," & SscTotals(5, 21)
                    strTmp23 = strTmp23 & "," & SscTotals(5, 22)
                    strTmp24 = strTmp24 & "," & SscTotals(5, 23)
                    strTmp25 = strTmp25 & "," & SscTotals(5, 24)
                    strTmp26 = strTmp26 & "," & SscTotals(5, 25)
                    strTmp27 = strTmp27 & "," & SscTotals(5, 26) 'rowRevenueWithoutTax

                    strTmp28 = strTmp28 & "," & SscTotals(5, 27)
                    strTmp29 = strTmp29 & "," & SscTotals(5, 28)
                    strTmp30 = strTmp30 & "," & SscTotals(5, 29)
                    strTmp31 = strTmp31 & "," & SscTotals(5, 30) 'rowGrossProfit 
                    '                    strTmp32 = strTmp32 & "," & SscTotals(5, 31)
                    If SscTotals(5, 26) = 0 Then
                        FinalPercentage1 = SscTotals(5, 30)
                    Else
                        FinalPercentage1 = comcontrol.Money_Round((SscTotals(5, 30) / SscTotals(5, 26)) * 100, 2)
                    End If
                    strTmp32 = strTmp32 & "," & FinalPercentage1 & "%"



                End If
                'For Total Displa
                '        row0 = row0 & ",ALL SSC,ALL SSC,ALL SSC,ALL SSC"
                rowTitle = rowTitle & "," & SscTotals(0, 0) & "," & SscTotals(1, 0) & "," & SscTotals(2, 0) & "," & SscTotals(3, 0) & strTmp1 & ",Total"

                rowNumberOfCounters = rowNumberOfCounters & "," & SscTotals(0, 1) & "," & SscTotals(1, 1) & "," & SscTotals(2, 1) & "," & SscTotals(3, 1) & strTmp2 & "," & intNumberOfCountersTot
                rowNumberOfBusinessDat = rowNumberOfBusinessDat & "," & SscTotals(0, 2) & "," & SscTotals(1, 2) & "," & SscTotals(2, 2) & "," & SscTotals(3, 2) & strTmp3 & "," & intNumberOfBusinessDatTot
                rowNumberOfStaffs = rowNumberOfStaffs & "," & SscTotals(0, 3) & "," & SscTotals(1, 3) & "," & SscTotals(2, 3) & "," & SscTotals(3, 3) & strTmp4 & "," & intNumberOfStaffsTot



                rowCustomerVisit = rowCustomerVisit & "," & SscTotals(0, 4) & "," & SscTotals(1, 4) & "," & SscTotals(2, 4) & "," & SscTotals(3, 4) & strTmp5 & "," & intCustomerVisitTot
                rowCallRegistered = rowCallRegistered & "," & SscTotals(0, 5) & "," & SscTotals(1, 5) & "," & SscTotals(2, 5) & "," & SscTotals(3, 5) & strTmp6 & "," & intCallRegisteredTot
                rowRepairCompleted = rowRepairCompleted & "," & SscTotals(0, 6) & "," & SscTotals(1, 6) & "," & SscTotals(2, 6) & "," & SscTotals(3, 6) & strTmp7 & "," & intRepairCompletedTot
                rowGoodsDelivered = rowGoodsDelivered & "," & SscTotals(0, 7) & "," & SscTotals(1, 7) & "," & SscTotals(2, 7) & "," & SscTotals(3, 7) & strTmp8 & "," & intGoodsDeliveredTot
                rowPending = rowPending & "," & SscTotals(0, 8) & "," & SscTotals(1, 8) & "," & SscTotals(2, 8) & "," & SscTotals(3, 8) & strTmp9 & "," & intPendingTot
                rowWarranty = rowWarranty & "," & SscTotals(0, 9) & "," & SscTotals(1, 9) & "," & SscTotals(2, 9) & "," & SscTotals(3, 9) & strTmp10 & "," & intWarrantyTot
                rowInWarranty = rowInWarranty & "," & SscTotals(0, 10) & "," & SscTotals(1, 10) & "," & SscTotals(2, 10) & "," & SscTotals(3, 10) & strTmp11 & "," & intInWarrantyTot
                rowOutWarranty = rowOutWarranty & "," & SscTotals(0, 11) & "," & SscTotals(1, 11) & "," & SscTotals(2, 11) & "," & SscTotals(3, 11) & strTmp12 & "," & intOutWarrantyTot
                rowInWarrantyAmount = rowInWarrantyAmount & "," & SscTotals(0, 12) & "," & SscTotals(1, 12) & "," & SscTotals(2, 12) & "," & SscTotals(3, 12) & strTmp13 & "," & decInWarrantyAmountTot
                rowInWarrantyLabour = rowInWarrantyLabour & "," & SscTotals(0, 13) & "," & SscTotals(1, 13) & "," & SscTotals(2, 13) & "," & SscTotals(3, 13) & strTmp14 & "," & decInWarrantyLabourTot
                rowInWarrantyParts = rowInWarrantyParts & "," & SscTotals(0, 14) & "," & SscTotals(1, 14) & "," & SscTotals(2, 14) & "," & SscTotals(3, 14) & strTmp15 & "," & decInWarrantyPartsTot
                rowInWarrantyTransport = rowInWarrantyTransport & "," & SscTotals(0, 15) & "," & SscTotals(1, 15) & "," & SscTotals(2, 15) & "," & SscTotals(3, 15) & strTmp16 & "," & decInWarrantyTransportTot
                rowInWarrantyOthers = rowInWarrantyOthers & "," & SscTotals(0, 16) & "," & SscTotals(1, 16) & "," & SscTotals(2, 16) & "," & SscTotals(3, 16) & strTmp17 & "," & decInWarrantyOthersTot
                rowOutWarrantyAmount = rowOutWarrantyAmount & "," & SscTotals(0, 17) & "," & SscTotals(1, 17) & "," & SscTotals(2, 17) & "," & SscTotals(3, 17) & strTmp18 & "," & decOutWarrantyAmountTot
                rowOutWarrantyLabour = rowOutWarrantyLabour & "," & SscTotals(0, 18) & "," & SscTotals(1, 18) & "," & SscTotals(2, 18) & "," & SscTotals(3, 18) & strTmp19 & "," & decOutWarrantyLabourTot
                rowOutWarrantyParts = rowOutWarrantyParts & "," & SscTotals(0, 19) & "," & SscTotals(1, 19) & "," & SscTotals(2, 19) & "," & SscTotals(3, 19) & strTmp20 & "," & decOutWarrantyPartsTot
                rowOutWarrantyTransport = rowOutWarrantyTransport & "," & SscTotals(0, 20) & "," & SscTotals(1, 20) & "," & SscTotals(2, 20) & "," & SscTotals(3, 20) & strTmp21 & "," & decOutWarrantyTransportTot
                rowOutWarrantyOthers = rowOutWarrantyOthers & "," & SscTotals(0, 21) & "," & SscTotals(1, 21) & "," & SscTotals(2, 21) & "," & SscTotals(3, 21) & strTmp22 & "," & decOutWarrantyOthersTot
                rowSawDiscountLabour = rowSawDiscountLabour & "," & SscTotals(0, 22) & "," & SscTotals(1, 22) & "," & SscTotals(2, 22) & "," & SscTotals(3, 22) & strTmp23 & "," & decSawDiscountLabourTot
                rowSawDiscountParts = rowSawDiscountParts & "," & SscTotals(0, 23) & "," & SscTotals(1, 23) & "," & SscTotals(2, 23) & "," & SscTotals(3, 23) & strTmp24 & "," & decSawDiscountPartsTot
                rowOutWarrantyLabourwSawDisc = rowOutWarrantyLabourwSawDisc & "," & SscTotals(0, 24) & "," & SscTotals(1, 24) & "," & SscTotals(2, 24) & "," & SscTotals(3, 24) & strTmp25 & "," & decOutWarrantyLabourwSawDiscTot
                rowOutWarrantyPartswSawDisc = rowOutWarrantyPartswSawDisc & "," & SscTotals(0, 25) & "," & SscTotals(1, 25) & "," & SscTotals(2, 25) & "," & SscTotals(3, 25) & strTmp26 & "," & decOutWarrantyPartswSawDiscTot
                rowRevenueWithoutTax = rowRevenueWithoutTax & "," & SscTotals(0, 26) & "," & SscTotals(1, 26) & "," & SscTotals(2, 26) & "," & SscTotals(3, 26) & strTmp27 & "," & decRevenueWithoutTaxTot
                rowIwPartsConsumed = rowIwPartsConsumed & "," & SscTotals(0, 27) & "," & SscTotals(1, 27) & "," & SscTotals(2, 27) & "," & SscTotals(3, 27) & strTmp28 & "," & decIwPartsConsumedTot
                rowTotalPartsConsumed = rowTotalPartsConsumed & "," & SscTotals(0, 28) & "," & SscTotals(1, 28) & "," & SscTotals(2, 28) & "," & SscTotals(3, 28) & strTmp29 & "," & decTotalPartsConsumedTot
                rowPrimeCostTotal = rowPrimeCostTotal & "," & SscTotals(0, 29) & "," & SscTotals(1, 29) & "," & SscTotals(2, 29) & "," & SscTotals(3, 29) & strTmp30 & "," & decPrimeCostTotalTot
                rowGrossProfit = rowGrossProfit & "," & SscTotals(0, 30) & "," & SscTotals(1, 30) & "," & SscTotals(2, 30) & "," & SscTotals(3, 30) & strTmp31 & "," & decGrossProfitTot
                'rowFinalPercentage = rowFinalPercentage & "," & SscTotals(0, 31) & "," & SscTotals(1, 31) & "," & SscTotals(2, 31) & "," & SscTotals(3, 31) & strTmp32 & "," & decFinalPercentage
                If SscTotals(0, 26) = 0 Then ' RevenueWithoutTax = 0
                    SscTotals031 = SscTotals(0, 30) 'GrossProfit
                Else
                    SscTotals031 = comcontrol.Money_Round((SscTotals(0, 30) / SscTotals(0, 26)) * 100, 2)
                End If
                If SscTotals(1, 26) = 0 Then ' RevenueWithoutTax = 0
                    SscTotals131 = SscTotals(1, 30) 'GrossProfit
                Else
                    SscTotals131 = comcontrol.Money_Round((SscTotals(1, 30) / SscTotals(1, 26)) * 100, 2)
                End If
                If SscTotals(2, 26) = 0 Then ' RevenueWithoutTax = 0
                    SscTotals231 = SscTotals(2, 30) 'GrossProfit
                Else
                    SscTotals231 = comcontrol.Money_Round((SscTotals(2, 30) / SscTotals(2, 26)) * 100, 2)
                End If
                If SscTotals(3, 26) = 0 Then ' RevenueWithoutTax = 0
                    SscTotals331 = SscTotals(3, 30) 'GrossProfit
                Else
                    SscTotals331 = comcontrol.Money_Round((SscTotals(3, 30) / SscTotals(3, 26)) * 100, 2)
                End If
                If decRevenueWithoutTaxTot = 0 Then ' RevenueWithoutTax = 0
                    _FinalPercentage = decGrossProfitTot 'GrossProfit
                Else
                    _FinalPercentage = comcontrol.Money_Round((decGrossProfitTot / decRevenueWithoutTaxTot) * 100, 2)
                End If
                rowFinalPercentage = rowFinalPercentage & "," & SscTotals031 & "%" & "," & SscTotals131 & "%" & "," & SscTotals231 & "%" & "," & SscTotals331 & "%" & strTmp32 & "," & _FinalPercentage & "%"



                'For Display End of Label
                rowTitle = rowTitle & ","

                rowNumberOfCounters = rowNumberOfCounters & "," '& intNumberOfCounters
                rowNumberOfBusinessDat = rowNumberOfBusinessDat & "," '& intNumberOfBusinessDat
                rowNumberOfStaffs = rowNumberOfStaffs & "," '& intNumberOfStaffs


                rowCustomerVisit = rowCustomerVisit & "," & "CustomerVisit"
                rowCallRegistered = rowCallRegistered & "," & "CallRegistered"
                rowRepairCompleted = rowRepairCompleted & "," & "RepairCompleted"
                rowGoodsDelivered = rowGoodsDelivered & "," & "GoodsDelivered"
                rowPending = rowPending & "," & "Pending"
                rowWarranty = rowWarranty & "," & "Warranty"
                rowInWarranty = rowInWarranty & "," & "InWarranty"
                rowOutWarranty = rowOutWarranty & "," & "OutWarranty"
                rowInWarrantyAmount = rowInWarrantyAmount & "," & "InWarrantyAmount"
                rowInWarrantyLabour = rowInWarrantyLabour & "," & "InWarrantyLabour"
                rowInWarrantyParts = rowInWarrantyParts & "," & "InWarrantyParts"
                rowInWarrantyTransport = rowInWarrantyTransport & "," & "InWarrantyTransport"
                rowInWarrantyOthers = rowInWarrantyOthers & "," & "InWarrantyOthers"
                rowOutWarrantyAmount = rowOutWarrantyAmount & "," & "OutWarrantyAmount"
                rowOutWarrantyLabour = rowOutWarrantyLabour & "," & "OutWarrantyLabour"
                rowOutWarrantyParts = rowOutWarrantyParts & "," & "OutWarrantyParts"
                rowOutWarrantyTransport = rowOutWarrantyTransport & "," & "OutWarrantyTransport"
                rowOutWarrantyOthers = rowOutWarrantyOthers & "," & "OutWarrantyOthers"
                rowSawDiscountLabour = rowSawDiscountLabour & "," & "SawDiscountLabour"
                rowSawDiscountParts = rowSawDiscountParts & "," & "SawDiscountParts"
                rowOutWarrantyLabourwSawDisc = rowOutWarrantyLabourwSawDisc & "," & "OutWarrantyLabourwSawDisc"
                rowOutWarrantyPartswSawDisc = rowOutWarrantyPartswSawDisc & "," & "OutWarrantyPartswSawDisc"
                rowRevenueWithoutTax = rowRevenueWithoutTax & "," & "RevenueWithoutTax"
                rowIwPartsConsumed = rowIwPartsConsumed & "," & "IwPartsConsumed"
                rowTotalPartsConsumed = rowTotalPartsConsumed & "," & "TotalPartsConsumed"
                rowPrimeCostTotal = rowPrimeCostTotal & "," & "PrimeCostTotal"
                rowGrossProfit = rowGrossProfit & "," & "GrossProfit"
                rowFinalPercentage = rowFinalPercentage & "," & " "


            Else
                'For Total Displa
                rowTitle = rowTitle & ",Total"

                rowNumberOfCounters = rowNumberOfCounters & "," '& intNumberOfCountersTot
                rowNumberOfBusinessDat = rowNumberOfBusinessDat & "," '& intNumberOfBusinessDatTot
                rowNumberOfStaffs = rowNumberOfStaffs & "," '& intNumberOfStaffsTot

                rowCustomerVisit = rowCustomerVisit & "," & intCustomerVisitTot
                rowCallRegistered = rowCallRegistered & "," & intCallRegisteredTot
                rowRepairCompleted = rowRepairCompleted & "," & intRepairCompletedTot
                rowGoodsDelivered = rowGoodsDelivered & "," & intGoodsDeliveredTot
                rowPending = rowPending & "," & intPendingTot
                rowWarranty = rowWarranty & "," & intWarrantyTot
                rowInWarranty = rowInWarranty & "," & intInWarrantyTot
                rowOutWarranty = rowOutWarranty & "," & intOutWarrantyTot
                rowInWarrantyAmount = rowInWarrantyAmount & "," & decInWarrantyAmountTot
                rowInWarrantyLabour = rowInWarrantyLabour & "," & decInWarrantyLabourTot
                rowInWarrantyParts = rowInWarrantyParts & "," & decInWarrantyPartsTot
                rowInWarrantyTransport = rowInWarrantyTransport & "," & decInWarrantyTransportTot
                rowInWarrantyOthers = rowInWarrantyOthers & "," & decInWarrantyOthersTot
                rowOutWarrantyAmount = rowOutWarrantyAmount & "," & decOutWarrantyAmountTot
                rowOutWarrantyLabour = rowOutWarrantyLabour & "," & decOutWarrantyLabourTot
                rowOutWarrantyParts = rowOutWarrantyParts & "," & decOutWarrantyPartsTot
                rowOutWarrantyTransport = rowOutWarrantyTransport & "," & decOutWarrantyTransportTot
                rowOutWarrantyOthers = rowOutWarrantyOthers & "," & decOutWarrantyOthersTot
                rowSawDiscountLabour = rowSawDiscountLabour & "," & decSawDiscountLabourTot
                rowSawDiscountParts = rowSawDiscountParts & "," & decSawDiscountPartsTot
                rowOutWarrantyLabourwSawDisc = rowOutWarrantyLabourwSawDisc & "," & decOutWarrantyLabourwSawDiscTot
                rowOutWarrantyPartswSawDisc = rowOutWarrantyPartswSawDisc & "," & decOutWarrantyPartswSawDiscTot
                rowRevenueWithoutTax = rowRevenueWithoutTax & "," & decRevenueWithoutTaxTot
                rowIwPartsConsumed = rowIwPartsConsumed & "," & decIwPartsConsumedTot
                rowTotalPartsConsumed = rowTotalPartsConsumed & "," & decTotalPartsConsumedTot
                rowPrimeCostTotal = rowPrimeCostTotal & "," & decPrimeCostTotalTot
                rowGrossProfit = rowGrossProfit & "," & decGrossProfitTot

                If decRevenueWithoutTaxTot = 0 Then ' RevenueWithoutTax = 0
                    _FinalPercentage = decGrossProfitTot 'GrossProfit
                Else
                    _FinalPercentage = comcontrol.Money_Round((decGrossProfitTot / decRevenueWithoutTaxTot) * 100, 2)
                End If
                rowFinalPercentage = rowFinalPercentage & "," & _FinalPercentage & "%"


                'For Display End of Label
                rowTitle = rowTitle & ","

                rowNumberOfCounters = rowNumberOfCounters & "," & "Number of counters"
                rowNumberOfBusinessDat = rowNumberOfBusinessDat & "," & "Number of Business dat"
                rowNumberOfStaffs = rowNumberOfStaffs & "," & "Number of Staffs"

                rowCustomerVisit = rowCustomerVisit & "," & "CustomerVisit"
                rowCallRegistered = rowCallRegistered & "," & "CallRegistered"
                rowRepairCompleted = rowRepairCompleted & "," & "RepairCompleted"
                rowGoodsDelivered = rowGoodsDelivered & "," & "GoodsDelivered"
                rowPending = rowPending & "," & "Pending"
                rowWarranty = rowWarranty & "," & "Warranty"
                rowInWarranty = rowInWarranty & "," & "InWarranty"
                rowOutWarranty = rowOutWarranty & "," & "OutWarranty"
                rowInWarrantyAmount = rowInWarrantyAmount & "," & "InWarrantyAmount"
                rowInWarrantyLabour = rowInWarrantyLabour & "," & "InWarrantyLabour"
                rowInWarrantyParts = rowInWarrantyParts & "," & "InWarrantyParts"
                rowInWarrantyTransport = rowInWarrantyTransport & "," & "InWarrantyTransport"
                rowInWarrantyOthers = rowInWarrantyOthers & "," & "InWarrantyOthers"
                rowOutWarrantyAmount = rowOutWarrantyAmount & "," & "OutWarrantyAmount"
                rowOutWarrantyLabour = rowOutWarrantyLabour & "," & "OutWarrantyLabour"
                rowOutWarrantyParts = rowOutWarrantyParts & "," & "OutWarrantyParts"
                rowOutWarrantyTransport = rowOutWarrantyTransport & "," & "OutWarrantyTransport"
                rowOutWarrantyOthers = rowOutWarrantyOthers & "," & "OutWarrantyOthers"
                rowSawDiscountLabour = rowSawDiscountLabour & "," & "SawDiscountLabour"
                rowSawDiscountParts = rowSawDiscountParts & "," & "SawDiscountParts"
                rowOutWarrantyLabourwSawDisc = rowOutWarrantyLabourwSawDisc & "," & "OutWarrantyLabourwSawDisc"
                rowOutWarrantyPartswSawDisc = rowOutWarrantyPartswSawDisc & "," & "OutWarrantyPartswSawDisc"
                rowRevenueWithoutTax = rowRevenueWithoutTax & "," & "RevenueWithoutTax"
                rowIwPartsConsumed = rowIwPartsConsumed & "," & "IwPartsConsumed"
                rowTotalPartsConsumed = rowTotalPartsConsumed & "," & "TotalPartsConsumed"
                rowPrimeCostTotal = rowPrimeCostTotal & "," & "PrimeCostTotal"
                rowGrossProfit = rowGrossProfit & "," & "GrossProfit"
                rowFinalPercentage = rowFinalPercentage & "," & " "

            End If



            csvContents.Add(row0)
            csvContents.Add(rowTitle)

            csvContents.Add(rowNumberOfCounters)
            csvContents.Add(rowNumberOfBusinessDat)
            csvContents.Add(rowNumberOfStaffs)

            csvContents.Add(rowCustomerVisit)
            csvContents.Add(rowCallRegistered)
            csvContents.Add(rowRepairCompleted)
            csvContents.Add(rowGoodsDelivered)
            csvContents.Add(rowPending)
            csvContents.Add(rowWarranty)
            csvContents.Add(rowInWarranty)
            csvContents.Add(rowOutWarranty)
            csvContents.Add(rowInWarrantyAmount)
            csvContents.Add(rowInWarrantyLabour)
            csvContents.Add(rowInWarrantyParts)
            csvContents.Add(rowInWarrantyTransport)
            csvContents.Add(rowInWarrantyOthers)
            csvContents.Add(rowOutWarrantyAmount)
            csvContents.Add(rowOutWarrantyLabour)
            csvContents.Add(rowOutWarrantyParts)
            csvContents.Add(rowOutWarrantyTransport)
            csvContents.Add(rowOutWarrantyOthers)
            csvContents.Add(rowSawDiscountLabour)
            csvContents.Add(rowSawDiscountParts)
            csvContents.Add(rowOutWarrantyLabourwSawDisc)
            csvContents.Add(rowOutWarrantyPartswSawDisc)
            csvContents.Add(rowRevenueWithoutTax)
            csvContents.Add(rowIwPartsConsumed)
            csvContents.Add(rowTotalPartsConsumed)
            csvContents.Add(rowPrimeCostTotal)
            csvContents.Add(rowGrossProfit)
            csvContents.Add(rowFinalPercentage)

            Dim csvFileName As String

            Dim clsSetCommon As New Class_common
            Dim dtNow As DateTime = clsSetCommon.dtIndia
            Dim exportFile As String = Session("export_File")

            Dim outputPath As String

            'PeriodFrom = Replace(PeriodFrom, "/", "")
            'PeriodTo = Replace(PeriodTo, "/", "")

            strDay = 0
            strMon = DropDownMonth.SelectedItem.Value
            strYear = DropDownYear.SelectedItem.Value

            If Len(strDay) <= 1 Then
                strDay = "0" & strDay
            End If
            If Len(strMon) <= 1 Then
                strMon = "0" & strMon
            End If

            If DropListLocation.SelectedItem.Text = "ALL" Then
                csvFileName = "SM_MONTHLY_PL_ALL_" & strYear & strMon & ".csv"
            Else
                csvFileName = "SM_MONTHLY_PL_" & DropListLocation.SelectedItem.Text & "_" & strYear & strMon & ".csv"
            End If
            ''csvFileName = exportFile & "_ " & DropListLocation.SelectedItem.Text & "_" & PeriodFrom.Year & PeriodFrom.Month & PeriodFrom.Day & ".csv"

            outputPath = clsSet.CsvFilePass & csvFileName

            Using writer As New StreamWriter(outputPath, False, Encoding.Default)
                writer.WriteLine(String.Join(Environment.NewLine, csvContents))
            End Using

            Dim Buffer As Byte() = System.IO.File.ReadAllBytes(outputPath)
            If outputPath <> "" Then
                If System.IO.File.Exists(outputPath) = True Then
                    System.IO.File.Delete(outputPath)
                End If
            End If
            Response.ContentType = "application/text/csv"
            Response.AddHeader("Content-Disposition", "attachment; filename=" & csvFileName)
            Response.Flush()
            'Response.Write("<b>File Contents: </b>")
            Response.BinaryWrite(Buffer)
            'Response.WriteFile(outputPath) 'if not required dialog box to user, then make comment this line
            Response.End()












            '****************************************
            'Specified period 
            'Begin
            '****************************************
        ElseIf DrpType.SelectedItem.Value = "03" Then ' Specified period
            '3rd Period
            Dim PeriodFrom As New System.DateTime(strYear, 9, 30, 0, 0, 0)
            Dim PeriodTo As New System.DateTime(strYear, 10, 13, 0, 0, 0)

            PeriodFrom = TextDateFrom.Text
            PeriodTo = TextDateTo.Text

            '1st Condition - From date must Monday
            '2nd Condition - To date must Sunday

            lblLoc.Text = PeriodFrom

            Dim PeriodCurrentFrom As New System.DateTime(strYear, strMon, strDay, 0, 0, 0)
            Dim PeriodCurrentTo As New System.DateTime(strYear, strMon, strDay, 0, 0, 0)


            'Verify that From must 1 (Monday)
            'Verify that To must 0 (Sunday)
            'Allowed maximum 50 weeks
            'Only one SSC


            If PeriodFrom.DayOfWeek <> 1 Then
                'Show the error message
                Call showMsg("There is an error in the from date specification (Monday)", "")
                Exit Sub
            End If

            If PeriodTo.DayOfWeek <> 0 Then
                'Show the error message
                Call showMsg("There is an error in the to date specification (Sunday)", "")
                Exit Sub
            End If
            'Only Allowed One SSC
            If DropListLocation.SelectedItem.Text = "ALL" Then
                Call showMsg("Only one SSC must select", "")
                Exit Sub
            End If

            Dim lpContinue As Boolean = True
            'Initially move currentfrom previous monday then loop will assign
            PeriodCurrentFrom = PeriodFrom.AddDays(-7)

            Dim intMaxCount As Integer = 0
            Dim strText As String = ""
            ' lblText.Text = ""
            While (lpContinue = True)
                intMaxCount = intMaxCount + 1
                PeriodCurrentFrom = PeriodCurrentFrom.AddDays(7)
                PeriodCurrentTo = PeriodCurrentFrom.AddDays(6)
                ' lblText.Text = lblText.Text & "intMaxCount = " & intMaxCount & "    PeriodCurrentFrom =" & PeriodCurrentFrom & " PeriodCurrentTo = " & PeriodCurrentTo & "<br>"
                If PeriodTo <= PeriodCurrentTo Then
                    lpContinue = False
                End If
                If intMaxCount >= 51 Then
                    lpContinue = False
                End If
            End While

            If intMaxCount >= 51 Then
                Call showMsg("Maximum allowed 50 weeks", "")
                Exit Sub
            End If


            PeriodFrom = TextDateFrom.Text
            PeriodTo = TextDateTo.Text

            PeriodCurrentFrom = PeriodFrom.AddDays(-7)

            lpContinue = True
            intMaxCount = 0
            Dim row0 As String = DropListLocation.SelectedItem.Text & ","
            Dim rowTitle As String = " "

            Dim rowNumberOfCounters As String = "Number of counters"
            Dim rowNumberOfBusinessDat As String = "Number of Business dat"
            Dim rowNumberOfStaffs As String = "Number of Staffs"

            Dim rowCustomerVisit As String = "Customer Visit"
            Dim rowCallRegistered As String = "Call Registered"
            Dim rowRepairCompleted As String = "Repair Completed"
            Dim rowGoodsDelivered As String = "Goods Delivered"
            Dim rowPending As String = "Pending"
            Dim rowWarranty As String = "Warranty"
            Dim rowInWarranty As String = "InWarranty"
            Dim rowOutWarranty As String = "OutWarranty"
            Dim rowInWarrantyAmount As String = "InWarrantyAmount"
            Dim rowInWarrantyLabour As String = "InWarrantyLabour"
            Dim rowInWarrantyParts As String = "InWarrantyParts"
            Dim rowInWarrantyTransport As String = "InWarrantyTransport"
            Dim rowInWarrantyOthers As String = "InWarrantyOthers"
            Dim rowOutWarrantyAmount As String = "OutWarrantyAmount"
            Dim rowOutWarrantyLabour As String = "OutWarrantyLabour"
            Dim rowOutWarrantyParts As String = "OutWarrantyParts"
            Dim rowOutWarrantyTransport As String = "OutWarrantyTransport"
            Dim rowOutWarrantyOthers As String = "OutWarrantyOthers"
            Dim rowSawDiscountLabour As String = "SawDiscountLabour"
            Dim rowSawDiscountParts As String = "SawDiscountParts"
            Dim rowOutWarrantyLabourwSawDisc As String = "OutWarrantyLabourwSawDisc"
            Dim rowOutWarrantyPartswSawDisc As String = "OutWarrantyPartswSawDisc"
            Dim rowRevenueWithoutTax As String = "RevenueWithoutTax"
            Dim rowIwPartsConsumed As String = "IwPartsConsumed"
            Dim rowTotalPartsConsumed As String = "OutTotalPartsConsumed"
            Dim rowPrimeCostTotal As String = "PrimeCostTotal"
            Dim rowGrossProfit As String = "GrossProfit"
            Dim rowFinalPercentage As String = " "

            While (lpContinue = True)
                intMaxCount = intMaxCount + 1
                PeriodCurrentFrom = PeriodCurrentFrom.AddDays(7)
                PeriodCurrentTo = PeriodCurrentFrom.AddDays(6)



                strDay = PeriodCurrentFrom.Day()
                strMon = PeriodCurrentFrom.Month
                strYear = PeriodCurrentFrom.Year

                If Len(strDay) <= 1 Then
                    strDay = "0" & strDay
                End If
                If Len(strMon) <= 1 Then
                    strMon = "0" & strMon
                End If
                strDate = strYear & strMon & strDay


                'Read from 
                _dtPlReprt = comcontrol.SelectPlReport(PeriodCurrentFrom, PeriodCurrentTo, DropListLocation.SelectedItem.Text, DropListLocation.SelectedItem.Value, Gm)

                If (_dtPlReprt Is Nothing) Or (_dtPlReprt.Rows.Count = 0) Then

                Else






                    For Each row As DataRow In _dtPlReprt.Rows

                        intNumberOfCounters = row.Item("NumberOfCounters")
                        intNumberOfBusinessDat = row.Item("NumberOfBusinessDat")
                        intNumberOfStaffs = row.Item("NumberOfStaffs")

                        intCustomerVisit = row.Item("CustomerVisit")
                        intCallRegistered = row.Item("CallRegistered")
                        intRepairCompleted = row.Item("RepairCompleted")
                        intGoodsDelivered = row.Item("GoodsDelivered")
                        intPending = row.Item("Pending")
                        intWarranty = row.Item("Warranty")
                        intInWarranty = row.Item("InWarranty")
                        intOutWarranty = row.Item("OutWarranty")
                        decInWarrantyAmount = row.Item("InWarrantyAmount")
                        decInWarrantyLabour = row.Item("InWarrantyLabour")
                        decInWarrantyParts = row.Item("InWarrantyParts")
                        decInWarrantyTransport = row.Item("InWarrantyTransport")
                        decInWarrantyOthers = row.Item("InWarrantyOthers")
                        decOutWarrantyAmount = row.Item("OutWarrantyAmount")
                        decOutWarrantyLabour = row.Item("OutWarrantyLabour")
                        decOutWarrantyParts = row.Item("OutWarrantyParts")
                        decOutWarrantyTransport = row.Item("OutWarrantyTransport")
                        decOutWarrantyOthers = row.Item("OutWarrantyOthers")
                        decSawDiscountLabour = row.Item("SawDiscountLabour")
                        decSawDiscountParts = row.Item("SawDiscountParts")
                        decOutWarrantyLabourwSawDisc = row.Item("OutWarrantyLabourwSawDisc")
                        decOutWarrantyPartswSawDisc = row.Item("OutWarrantyPartswSawDisc")
                        decRevenueWithoutTax = row.Item("RevenueWithoutTax")
                        decIwPartsConsumed = row.Item("IwPartsConsumed")
                        decTotalPartsConsumed = row.Item("TotalPartsConsumed")
                        decPrimeCostTotal = row.Item("PrimeCostTotal")
                        decGrossProfit = row.Item("GrossProfit")
                        decFinalPercentage = row.Item("FinalPercentage")

                        intNumberOfCountersTot = intNumberOfCountersTot + intNumberOfCounters
                        intNumberOfBusinessDatTot = intNumberOfBusinessDatTot + intNumberOfBusinessDat
                        intNumberOfStaffsTot = intNumberOfStaffsTot + intNumberOfStaffs

                        intCustomerVisitTot = intCustomerVisitTot + intCustomerVisit
                        intCallRegisteredTot = intCallRegisteredTot + intCallRegistered
                        intRepairCompletedTot = intRepairCompletedTot + intRepairCompleted
                        intGoodsDeliveredTot = intGoodsDeliveredTot + intGoodsDelivered
                        intPendingTot = intPendingTot + intPending
                        intWarrantyTot = intWarrantyTot + intWarranty
                        intInWarrantyTot = intInWarrantyTot + intInWarranty
                        intOutWarrantyTot = intOutWarrantyTot + intOutWarranty
                        decInWarrantyAmountTot = decInWarrantyAmountTot + decInWarrantyAmount
                        decInWarrantyLabourTot = decInWarrantyLabourTot + decInWarrantyLabour
                        decInWarrantyPartsTot = decInWarrantyPartsTot + decInWarrantyParts
                        decInWarrantyTransportTot = decInWarrantyTransportTot + decInWarrantyTransport
                        decInWarrantyOthersTot = decInWarrantyOthersTot + decInWarrantyOthers
                        decOutWarrantyAmountTot = decOutWarrantyAmountTot + decOutWarrantyAmount
                        decOutWarrantyLabourTot = decOutWarrantyLabourTot + decOutWarrantyLabour
                        decOutWarrantyPartsTot = decOutWarrantyPartsTot + decOutWarrantyParts
                        decOutWarrantyTransportTot = decOutWarrantyTransportTot + decOutWarrantyTransport
                        decOutWarrantyOthersTot = decOutWarrantyOthersTot + decOutWarrantyOthers
                        decSawDiscountLabourTot = decSawDiscountLabourTot + decSawDiscountLabour
                        decSawDiscountPartsTot = decSawDiscountPartsTot + decSawDiscountParts
                        decOutWarrantyLabourwSawDiscTot = decOutWarrantyLabourwSawDiscTot + decOutWarrantyLabourwSawDisc
                        decOutWarrantyPartswSawDiscTot = decOutWarrantyPartswSawDiscTot + decOutWarrantyPartswSawDisc
                        decRevenueWithoutTaxTot = decRevenueWithoutTaxTot + decRevenueWithoutTax
                        decIwPartsConsumedTot = decIwPartsConsumedTot + decIwPartsConsumed
                        decTotalPartsConsumedTot = decTotalPartsConsumedTot + decTotalPartsConsumed
                        decPrimeCostTotalTot = decPrimeCostTotalTot + decPrimeCostTotal
                        decGrossProfitTot = decGrossProfitTot + decGrossProfit
                        decFinalPercentageTot = decFinalPercentageTot + decFinalPercentage


                        rowTitle = rowTitle & "," & strDate

                        rowNumberOfCounters = rowNumberOfCounters & "," '& intNumberOfCounters
                        rowNumberOfBusinessDat = rowNumberOfBusinessDat & "," '& intNumberOfBusinessDat
                        rowNumberOfStaffs = rowNumberOfStaffs & "," '& intNumberOfStaffs

                        rowCustomerVisit = rowCustomerVisit & "," & intCustomerVisit
                        rowCallRegistered = rowCallRegistered & "," & intCallRegistered
                        rowRepairCompleted = rowRepairCompleted & "," & intRepairCompleted
                        rowGoodsDelivered = rowGoodsDelivered & "," & intGoodsDelivered
                        rowPending = rowPending & "," & intPending
                        rowWarranty = rowWarranty & "," & intWarranty
                        rowInWarranty = rowInWarranty & "," & intInWarranty
                        rowOutWarranty = rowOutWarranty & "," & intOutWarranty
                        rowInWarrantyAmount = rowInWarrantyAmount & "," & decInWarrantyAmount
                        rowInWarrantyLabour = rowInWarrantyLabour & "," & decInWarrantyLabour
                        rowInWarrantyParts = rowInWarrantyParts & "," & decInWarrantyParts
                        rowInWarrantyTransport = rowInWarrantyTransport & "," & decInWarrantyTransport
                        rowInWarrantyOthers = rowInWarrantyOthers & "," & decInWarrantyOthers
                        rowOutWarrantyAmount = rowOutWarrantyAmount & "," & decOutWarrantyAmount
                        rowOutWarrantyLabour = rowOutWarrantyLabour & "," & decOutWarrantyLabour
                        rowOutWarrantyParts = rowOutWarrantyParts & "," & decOutWarrantyParts
                        rowOutWarrantyTransport = rowOutWarrantyTransport & "," & decOutWarrantyTransport
                        rowOutWarrantyOthers = rowOutWarrantyOthers & "," & decOutWarrantyOthers
                        rowSawDiscountLabour = rowSawDiscountLabour & "," & decSawDiscountLabour
                        rowSawDiscountParts = rowSawDiscountParts & "," & decSawDiscountParts
                        rowOutWarrantyLabourwSawDisc = rowOutWarrantyLabourwSawDisc & "," & decOutWarrantyLabourwSawDisc
                        rowOutWarrantyPartswSawDisc = rowOutWarrantyPartswSawDisc & "," & decOutWarrantyPartswSawDisc
                        rowRevenueWithoutTax = rowRevenueWithoutTax & "," & decRevenueWithoutTax
                        rowIwPartsConsumed = rowIwPartsConsumed & "," & decIwPartsConsumed
                        rowTotalPartsConsumed = rowTotalPartsConsumed & "," & decTotalPartsConsumed
                        rowPrimeCostTotal = rowPrimeCostTotal & "," & decPrimeCostTotal
                        rowGrossProfit = rowGrossProfit & "," & decGrossProfit
                        'rowFinalPercentage = rowFinalPercentage & "," & decFinalPercentage

                        If decRevenueWithoutTax = 0 Then
                            FinalPercentage1 = decGrossProfit
                        Else
                            FinalPercentage1 = comcontrol.Money_Round((decGrossProfit / decRevenueWithoutTax) * 100, 2)
                        End If
                        rowFinalPercentage = rowFinalPercentage & "," & FinalPercentage1 & "%" 'decFinalPercentage / 100
                    Next row
                End If

                ' lblText.Text = lblText.Text & "intMaxCount = " & intMaxCount & "    PeriodCurrentFrom =" & PeriodCurrentFrom & " PeriodCurrentTo = " & PeriodCurrentTo & "<br>"
                If PeriodTo <= PeriodCurrentTo Then
                    lpContinue = False
                End If
                If intMaxCount >= 51 Then
                    lpContinue = False
                End If
            End While

            'For Total Displa
            rowTitle = rowTitle & ",Total"

            rowNumberOfCounters = rowNumberOfCounters & "," '& intNumberOfCountersTot
            rowNumberOfBusinessDat = rowNumberOfBusinessDat & "," '& intNumberOfBusinessDatTot
            rowNumberOfStaffs = rowNumberOfStaffs & "," '& intNumberOfStaffsTot

            rowCustomerVisit = rowCustomerVisit & "," & intCustomerVisitTot
            rowCallRegistered = rowCallRegistered & "," & intCallRegisteredTot
            rowRepairCompleted = rowRepairCompleted & "," & intRepairCompletedTot
            rowGoodsDelivered = rowGoodsDelivered & "," & intGoodsDeliveredTot
            rowPending = rowPending & "," & intPendingTot
            rowWarranty = rowWarranty & "," & intWarrantyTot
            rowInWarranty = rowInWarranty & "," & intInWarrantyTot
            rowOutWarranty = rowOutWarranty & "," & intOutWarrantyTot
            rowInWarrantyAmount = rowInWarrantyAmount & "," & decInWarrantyAmountTot
            rowInWarrantyLabour = rowInWarrantyLabour & "," & decInWarrantyLabourTot
            rowInWarrantyParts = rowInWarrantyParts & "," & decInWarrantyPartsTot
            rowInWarrantyTransport = rowInWarrantyTransport & "," & decInWarrantyTransportTot
            rowInWarrantyOthers = rowInWarrantyOthers & "," & decInWarrantyOthersTot
            rowOutWarrantyAmount = rowOutWarrantyAmount & "," & decOutWarrantyAmountTot
            rowOutWarrantyLabour = rowOutWarrantyLabour & "," & decOutWarrantyLabourTot
            rowOutWarrantyParts = rowOutWarrantyParts & "," & decOutWarrantyPartsTot
            rowOutWarrantyTransport = rowOutWarrantyTransport & "," & decOutWarrantyTransportTot
            rowOutWarrantyOthers = rowOutWarrantyOthers & "," & decOutWarrantyOthersTot
            rowSawDiscountLabour = rowSawDiscountLabour & "," & decSawDiscountLabourTot
            rowSawDiscountParts = rowSawDiscountParts & "," & decSawDiscountPartsTot
            rowOutWarrantyLabourwSawDisc = rowOutWarrantyLabourwSawDisc & "," & decOutWarrantyLabourwSawDiscTot
            rowOutWarrantyPartswSawDisc = rowOutWarrantyPartswSawDisc & "," & decOutWarrantyPartswSawDiscTot
            rowRevenueWithoutTax = rowRevenueWithoutTax & "," & decRevenueWithoutTaxTot
            rowIwPartsConsumed = rowIwPartsConsumed & "," & decIwPartsConsumedTot
            rowTotalPartsConsumed = rowTotalPartsConsumed & "," & decTotalPartsConsumedTot
            rowPrimeCostTotal = rowPrimeCostTotal & "," & decPrimeCostTotalTot
            rowGrossProfit = rowGrossProfit & "," & decGrossProfitTot
            'rowFinalPercentage = rowFinalPercentage & "," & decFinalPercentageTot
            If decRevenueWithoutTaxTot = 0 Then ' RevenueWithoutTax = 0
                _FinalPercentage = decGrossProfitTot 'GrossProfit
            Else
                _FinalPercentage = comcontrol.Money_Round((decGrossProfitTot / decRevenueWithoutTaxTot) * 100, 2)
            End If
            rowFinalPercentage = rowFinalPercentage & "," & _FinalPercentage & "%"


            'For Display End of Label
            rowTitle = rowTitle & ","

            rowNumberOfCounters = rowNumberOfCounters & "," & "Number of counters"
            rowNumberOfBusinessDat = rowNumberOfBusinessDat & "," & "Number of Business dat"
            rowNumberOfStaffs = rowNumberOfStaffs & "," & "Number of Staffs"

            rowCustomerVisit = rowCustomerVisit & "," & "CustomerVisit"
            rowCallRegistered = rowCallRegistered & "," & "CallRegistered"
            rowRepairCompleted = rowRepairCompleted & "," & "RepairCompleted"
            rowGoodsDelivered = rowGoodsDelivered & "," & "GoodsDelivered"
            rowPending = rowPending & "," & "Pending"
            rowWarranty = rowWarranty & "," & "Warranty"
            rowInWarranty = rowInWarranty & "," & "InWarranty"
            rowOutWarranty = rowOutWarranty & "," & "OutWarranty"
            rowInWarrantyAmount = rowInWarrantyAmount & "," & "InWarrantyAmount"
            rowInWarrantyLabour = rowInWarrantyLabour & "," & "InWarrantyLabour"
            rowInWarrantyParts = rowInWarrantyParts & "," & "InWarrantyParts"
            rowInWarrantyTransport = rowInWarrantyTransport & "," & "InWarrantyTransport"
            rowInWarrantyOthers = rowInWarrantyOthers & "," & "InWarrantyOthers"
            rowOutWarrantyAmount = rowOutWarrantyAmount & "," & "OutWarrantyAmount"
            rowOutWarrantyLabour = rowOutWarrantyLabour & "," & "OutWarrantyLabour"
            rowOutWarrantyParts = rowOutWarrantyParts & "," & "OutWarrantyParts"
            rowOutWarrantyTransport = rowOutWarrantyTransport & "," & "OutWarrantyTransport"
            rowOutWarrantyOthers = rowOutWarrantyOthers & "," & "OutWarrantyOthers"
            rowSawDiscountLabour = rowSawDiscountLabour & "," & "SawDiscountLabour"
            rowSawDiscountParts = rowSawDiscountParts & "," & "SawDiscountParts"
            rowOutWarrantyLabourwSawDisc = rowOutWarrantyLabourwSawDisc & "," & "OutWarrantyLabourwSawDisc"
            rowOutWarrantyPartswSawDisc = rowOutWarrantyPartswSawDisc & "," & "OutWarrantyPartswSawDisc"
            rowRevenueWithoutTax = rowRevenueWithoutTax & "," & "RevenueWithoutTax"
            rowIwPartsConsumed = rowIwPartsConsumed & "," & "IwPartsConsumed"
            rowTotalPartsConsumed = rowTotalPartsConsumed & "," & "TotalPartsConsumed"
            rowPrimeCostTotal = rowPrimeCostTotal & "," & "PrimeCostTotal"
            rowGrossProfit = rowGrossProfit & "," & "GrossProfit"
            rowFinalPercentage = rowFinalPercentage & "," & " "


            csvContents.Add(row0)
            csvContents.Add(rowTitle)

            csvContents.Add(rowNumberOfCounters)
            csvContents.Add(rowNumberOfBusinessDat)
            csvContents.Add(rowNumberOfStaffs)

            csvContents.Add(rowCustomerVisit)
            csvContents.Add(rowCallRegistered)
            csvContents.Add(rowRepairCompleted)
            csvContents.Add(rowGoodsDelivered)
            csvContents.Add(rowPending)
            csvContents.Add(rowWarranty)
            csvContents.Add(rowInWarranty)
            csvContents.Add(rowOutWarranty)
            csvContents.Add(rowInWarrantyAmount)
            csvContents.Add(rowInWarrantyLabour)
            csvContents.Add(rowInWarrantyParts)
            csvContents.Add(rowInWarrantyTransport)
            csvContents.Add(rowInWarrantyOthers)
            csvContents.Add(rowOutWarrantyAmount)
            csvContents.Add(rowOutWarrantyLabour)
            csvContents.Add(rowOutWarrantyParts)
            csvContents.Add(rowOutWarrantyTransport)
            csvContents.Add(rowOutWarrantyOthers)
            csvContents.Add(rowSawDiscountLabour)
            csvContents.Add(rowSawDiscountParts)
            csvContents.Add(rowOutWarrantyLabourwSawDisc)
            csvContents.Add(rowOutWarrantyPartswSawDisc)
            csvContents.Add(rowRevenueWithoutTax)
            csvContents.Add(rowIwPartsConsumed)
            csvContents.Add(rowTotalPartsConsumed)
            csvContents.Add(rowPrimeCostTotal)
            csvContents.Add(rowGrossProfit)
            csvContents.Add(rowFinalPercentage)

            Dim csvFileName As String
            'Dim dateFrom As String
            'Dim dateTo As String
            Dim clsSetCommon As New Class_common
            Dim dtNow As DateTime = clsSetCommon.dtIndia
            Dim exportFile As String = Session("export_File")

            Dim outputPath As String

            'PeriodFrom = Replace(PeriodFrom, "/", "")
            'PeriodTo = Replace(PeriodTo, "/", "")

            strDay = PeriodFrom.Day()
            strMon = PeriodFrom.Month
            strYear = PeriodFrom.Year

            If Len(strDay) <= 1 Then
                strDay = "0" & strDay
            End If
            If Len(strMon) <= 1 Then
                strMon = "0" & strMon
            End If

            Dim strDay1 As String = ""
            Dim strMon1 As String = ""
            Dim strYear1 As String = ""

            strDay1 = PeriodTo.Day()
            strMon1 = PeriodTo.Month
            strYear1 = PeriodTo.Year

            If Len(strDay1) <= 1 Then
                strDay = "0" & strDay1
            End If
            If Len(strMon1) <= 1 Then
                strMon = "0" & strMon1
            End If


            csvFileName = "SM_SPECIFIED_PERIOD_PL_" & DropListLocation.SelectedItem.Text & "_" & strYear & strMon & strDay & "_" & strYear1 & strMon1 & strDay1 & ".csv"

            ' csvFileName = exportFile & "_ " & DropListLocation.SelectedItem.Text & "_" & PeriodFrom.Year & PeriodFrom.Month & PeriodFrom.Day & "_" & PeriodTo.Year & PeriodTo.Month & PeriodTo.Day & ".csv"

            outputPath = clsSet.CsvFilePass & csvFileName

            Using writer As New StreamWriter(outputPath, False, Encoding.Default)
                writer.WriteLine(String.Join(Environment.NewLine, csvContents))
            End Using

            Dim Buffer As Byte() = System.IO.File.ReadAllBytes(outputPath)
            If outputPath <> "" Then
                If System.IO.File.Exists(outputPath) = True Then
                    System.IO.File.Delete(outputPath)
                End If
            End If
            Response.ContentType = "application/text/csv"
            Response.AddHeader("Content-Disposition", "attachment; filename=" & csvFileName)
            Response.Flush()
            'Response.Write("<b>File Contents: </b>")
            Response.BinaryWrite(Buffer)
            'Response.WriteFile(outputPath) 'if not required dialog box to user, then make comment this line
            Response.End()

        End If





        'Dim _SalesReportModel As SalesReportModel = New SalesReportModel()
        'Dim _SalesReportControl As SalesReportControl = New SalesReportControl()


        'Dim dtSalesReportView As DataTable = _SalesReportControl.SelectSalesReport(_SalesReportModel)
        'If (dtSalesReportView Is Nothing) Or (dtSalesReportView.Rows.Count = 0) Then
        '    'If no Records
        '    Call showMsg("No records found for the month of " & DropDownMonth.SelectedItem.Text, "")
        '    Exit Sub
        'Else
        '    Response.ContentType = "text/csv"
        '    Response.AddHeader("Content-Disposition", "attachment;filename=" & strFileName)
        '    Response.AddHeader("Pragma", "no-cache")
        '    Response.AddHeader("Cache-Control", "no-cache")
        '    Dim myData As Byte() = CommonControl.csvBytesWriter(dtSalesReportView)
        '    Response.BinaryWrite(myData)
        '    Response.Flush()
        '    Response.SuppressContent = True
        '    HttpContext.Current.ApplicationInstance.CompleteRequest()
        'End If


    End Sub
    Public Shared Function StrToByteArray(ByVal str As String) As Byte()
        Dim encoding As System.Text.UTF8Encoding = New System.Text.UTF8Encoding()
        Return encoding.GetBytes(str)
    End Function

    'Protected Sub btnExport_Click(sender As Object, e As ImageClickEventArgs) Handles btnExport.Click


    'If DropListLocation.Text = "Select Branch" Then
    '    Call showMsg("Please specify Branch.", "")
    '    Exit Sub
    'End If


    'Dim ShipName As String = Session("ship_Name")
    'Dim shipCode As String = Session("ship_code")
    'Dim userName As String = Session("user_Name")
    'Dim userid As String = Session("user_id")
    'Dim uploadShipname As String = DropListLocation.Text
    'Dim listHistoryMsg() As String = Session("list_History_Msg")

    'Dim _StockOverviewModel As StockOverviewModel = New StockOverviewModel()
    'Dim _StockOverviewControl As StockOverviewControl = New StockOverviewControl()
    'Dim strFileName As String = DropListLocation.SelectedItem.Text & "_" & DropDownYear.SelectedItem.Value & DropDownMonth.SelectedItem.Value & ".csv"
    '_StockOverviewModel.UserId = userid
    '_StockOverviewModel.UserName = userName
    '_StockOverviewModel.ShipToBranchCode = DropListLocation.SelectedItem.Value
    '_StockOverviewModel.strMonth = DropDownYear.SelectedItem.Value & "/" & DropDownMonth.SelectedItem.Value

    'Dim dtStockOverview As DataTable = _StockOverviewControl.SelectStockOverviewCountExport(_StockOverviewModel)
    'If (dtStockOverview Is Nothing) Or (dtStockOverview.Rows.Count = 0) Then
    '    'If no Records
    '    Call showMsg("No records found for the month of " & DropDownMonth.SelectedItem.Text, "")
    '    Exit Sub
    'Else

    '    Response.ContentType = "text/csv"
    '    Response.AddHeader("Content-Disposition", "attachment;filename=" & strFileName)
    '    Response.AddHeader("Pragma", "no-cache")
    '    Response.AddHeader("Cache-Control", "no-cache")
    '    Dim myData As Byte() = CommonControl.csvBytesWriter(dtStockOverview)
    '    Response.BinaryWrite(myData)
    '    Response.Flush()
    '    Response.SuppressContent = True
    '    HttpContext.Current.ApplicationInstance.CompleteRequest()
    'End If
    '   End Sub



    Sub DefaultDisplay()
        lblMonth.Visible = False
        DropDownMonth.Visible = False
        DropDownYear.Visible = False

        lblDateFrom.Visible = False
        TextDateFrom.Visible = False
        lblDateTo.Visible = False
        TextDateTo.Visible = False
    End Sub

    Protected Sub DrpType_SelectedIndexChanged(sender As Object, e As EventArgs) Handles DrpType.SelectedIndexChanged
        DefaultDisplay()
        If DrpType.SelectedValue = "0" Then

        ElseIf DrpType.SelectedValue = "01" Then

        ElseIf DrpType.SelectedValue = "02" Then
            lblMonth.Visible = True
            DropDownMonth.Visible = True
            DropDownYear.Visible = True

        ElseIf DrpType.SelectedValue = "03" Then
            lblDateFrom.Visible = True
            TextDateFrom.Visible = True
            lblDateTo.Visible = True
            TextDateTo.Visible = True
        End If
    End Sub
    Protected Sub DropdownList1_SelectedIndexChanged(sender As Object, e As EventArgs) Handles DropdownList1.SelectedIndexChanged


        If DropdownList1.SelectedValue = "Select" Then

            TextBox5.Text = "0.00"
            TextBox6.Text = "0.00"

        Else

            Dim cnstr As String = ConfigurationManager.ConnectionStrings("cnstr").ConnectionString
            Using Con As SqlConnection = New SqlConnection(cnstr)
                Using cmd As SqlCommand = New SqlCommand("Select  SUM(CREDIT_LIMIT) CREDIT_LIMIT,SUM (PER_DAY) PER_DAY  from R_CREDIT_I Where BRANCH_NO='" & DropdownList1.SelectedItem.Text & "'; ")
                    cmd.CommandType = CommandType.Text
                    cmd.Connection = Con
                    Con.Open()
                    Using sdr As SqlDataReader = cmd.ExecuteReader()
                        sdr.Read()
                        TextBox5.Text = sdr("CREDIT_LIMIT").ToString
                        TextBox6.Text = sdr("PER_DAY").ToString
                    End Using
                    Con.Close()
                    TextBox3.Text = ""
                    TextBox4.Text = ""
                End Using
            End Using

        End If
    End Sub


    Protected Sub drpManagementFunc_SelectedIndexChanged(sender As Object, e As EventArgs) Handles drpManagementFunc.SelectedIndexChanged

        DefaultDisplay()
        Stored.Visible = False
        Credit.Visible = False
        Getdta.Visible = False
        Label3.Visible = False
        Label43.Visible = False
        LblINFO.Visible = False
        Label40.Visible = False
        P1.Visible = False
        Table1.Visible = False
        Table5.Visible = False
        Table7.Visible = False
        Label91.Visible = False

        'PO_Collection
        Label200.Visible = False
        TableCollections.Visible = False

        tblSingle.Visible = False
        tblSingleRWT.Visible = False
        Table2.Visible = False
        Table3.Visible = False
        Label104.Visible = False
        TextFromDate.Text = ""
        Table6.Visible = False
        tblPaymentGrid.Visible = False
        TextToDate.Text = ""
        P2.Visible = False
        Label32.Visible = True

        LoadDB()

        If drpManagementFunc.SelectedValue = "01" Then
            Stored.Visible = True
            Credit.Visible = False
            Getdta.Visible = False
            Label3.Visible = True
            Label43.Visible = False
            Label91.Visible = False

            'PO_Collection
            Label200.Visible = False
            TableCollections.Visible = False

            LblINFO.Visible = False
            Label40.Visible = False
            P1.Visible = False
            Table1.Visible = False
            Table2.Visible = False
            Table3.Visible = False
            Label104.Visible = False
            P2.Visible = False
            Table5.Visible = False
            Table7.Visible = False
            tblSingle.Visible = False
            tblSingleRWT.Visible = False
            TextFromDate.Text = ""
            Table6.Visible = False
            tblPaymentGrid.Visible = False
            TextToDate.Text = ""
            Label32.Visible = False

            LoadDB1()
            LoadDB()
        End If
        If drpManagementFunc.SelectedValue = "02" Then
            Stored.Visible = False
            Credit.Visible = True
            Getdta.Visible = True
            Label3.Visible = False
            Label43.Visible = True
            LblINFO.Visible = True
            Label40.Visible = False
            Label91.Visible = False

            'PO_Collection
            Label200.Visible = False
            TableCollections.Visible = False

            P1.Visible = False
            Table1.Visible = False
            Table2.Visible = False
            Table3.Visible = False
            Label104.Visible = False
            Table5.Visible = False
            Table7.Visible = False
            tblSingle.Visible = False
            tblSingleRWT.Visible = False
            TextFromDate.Text = ""
            TextToDate.Text = ""
            P2.Visible = False
            Table6.Visible = False
            tblPaymentGrid.Visible = False
            Label32.Visible = False
            LoadDB()


        End If
        If drpManagementFunc.SelectedValue = "00" Then
            Stored.Visible = False
            Credit.Visible = False
            Getdta.Visible = False
            Label3.Visible = False
            Label43.Visible = False
            Label91.Visible = False

            'PO_Collection
            Label200.Visible = False
            TableCollections.Visible = False

            LblINFO.Visible = False
            Label40.Visible = False
            P1.Visible = False
            Table1.Visible = False
            Table2.Visible = False
            Table3.Visible = False
            Label104.Visible = False
            Table5.Visible = False
            Table7.Visible = False
            tblSingle.Visible = False
            tblSingleRWT.Visible = False
            TextFromDate.Text = ""
            TextToDate.Text = ""
            P2.Visible = False
            Table5.Visible = False
            Table7.Visible = False
            Table6.Visible = False
            tblPaymentGrid.Visible = False
            Label32.Visible = False
            LoadDB()
            LoadDB1()
        End If
        If drpManagementFunc.SelectedValue = "03" Then
            Stored.Visible = False
            Credit.Visible = False
            Getdta.Visible = False
            Label3.Visible = False
            Label43.Visible = False
            LblINFO.Visible = False
            Label40.Visible = True
            TextFromDate.Text = ""
            TextToDate.Text = ""
            Label91.Visible = False

            'PO_Collection
            Label200.Visible = False
            TableCollections.Visible = False
            Label32.Visible = False


            P1.Visible = True
            Table1.Visible = True
            Table2.Visible = True
            Table3.Visible = False
            Label104.Visible = False
            P2.Visible = False
            Table5.Visible = False
            Table7.Visible = False
            tblSingle.Visible = False
            tblSingleRWT.Visible = False
            Table6.Visible = False
            tblPaymentGrid.Visible = False
            Label32.Visible = False
            LoadDB()
            LoadDB1()
        End If

        If drpManagementFunc.SelectedValue = "04" Then
            Stored.Visible = False
            Credit.Visible = False
            Getdta.Visible = False
            Label3.Visible = False
            Label43.Visible = False
            LblINFO.Visible = False
            Label40.Visible = False
            Label91.Visible = False
            Label32.Visible = False

            'PO_Collection
            Label200.Visible = False
            TableCollections.Visible = False

            TextFromDate.Text = ""
            TextToDate.Text = ""
            P1.Visible = False
            Table1.Visible = False
            Table2.Visible = False
            Table3.Visible = True
            P2.Visible = False
            Label104.Visible = True
            Table6.Visible = False
            tblPaymentGrid.Visible = False
            Table5.Visible = False
            Table7.Visible = False
            Label32.Visible = False
            LoadDB()
            LoadDB1()

        End If

        If drpManagementFunc.SelectedValue = "05" Then
            Stored.Visible = False
            Credit.Visible = False
            Getdta.Visible = False
            Label3.Visible = False
            Label43.Visible = False
            LblINFO.Visible = False
            Label40.Visible = False
            P1.Visible = False
            Table1.Visible = False
            Label91.Visible = True
            Label32.Visible = False

            'PO_Collection
            Label200.Visible = False
            TableCollections.Visible = False

            Table2.Visible = False
            Table3.Visible = False
            Label104.Visible = False
            Table5.Visible = False
            Table7.Visible = False
            tblSingle.Visible = False
            tblSingleRWT.Visible = False
            TextFromDate.Text = ""
            TextToDate.Text = ""
            txtPaymentAmount.Text = ""
            txtTargetDate.Text = ""
            P2.Visible = False
            Table6.Visible = True
            tblPaymentGrid.Visible = True
            Label32.Visible = False
            LoadDB()
            Dim _PaymentVlaueControl As PaymentValueControl = New PaymentValueControl()
            Dim dtDisplayPaymentValue As DataTable = _PaymentVlaueControl.ShowPaymentValueGrid()
            'dtDisplayPaymentValue.DefaultView.Sort = "Id ASC"
            dtDisplayPaymentValue = dtDisplayPaymentValue.DefaultView.ToTable()
            If Not dtDisplayPaymentValue Is Nothing Then
                If dtDisplayPaymentValue.Rows.Count > 0 Then
                    txtPageSize.Visible = True
                    lblPagesize.Visible = True
                    txtPageSize.Text = 10
                    gvDisplayPaymentValue.PageSize = Convert.ToInt16(txtPageSize.Text)
                    gvDisplayPaymentValue.Visible = True
                    gvDisplayPaymentValue.AllowSorting = True
                    gvDisplayPaymentValue.PageIndex = 0
                    gvDisplayPaymentValue.DataSource = dtDisplayPaymentValue
                    gvDisplayPaymentValue.DataBind()

                    'btnExport.Visible = True
                    'lblErrorMessage.Visible = True
                    lblErrorMessage.Visible = False
                    lbltotal.Text = "Total No of Records : " & dtDisplayPaymentValue.Rows.Count
                    'lblTitle.Text = drpTaskExport.SelectedItem.Text
                Else
                    gvDisplayPaymentValue.AllowSorting = False
                    gvDisplayPaymentValue.DataBind()
                    gvDisplayPaymentValue.Visible = False
                    lblErrorMessage.Visible = False
                    'btnExport.Visible = False
                    txtPageSize.Visible = False
                    lblPagesize.Visible = False
                End If
            End If


        End If


        'PO_Collection
        If drpManagementFunc.SelectedValue = "06" Then
            Stored.Visible = False
            Credit.Visible = False
            Getdta.Visible = False
            Label3.Visible = False
            Label43.Visible = False
            LblINFO.Visible = False
            Label40.Visible = False
            P1.Visible = False
            Table1.Visible = False
            Label91.Visible = False
            Label32.Visible = False
            'PO_Collection
            Label200.Visible = True
            Table2.Visible = False
            Table3.Visible = False
            Label104.Visible = False
            Table5.Visible = False
            Table7.Visible = False
            tblSingle.Visible = False
            tblSingleRWT.Visible = False
            TextFromDate.Text = ""
            TextToDate.Text = ""
            TextBoxDeposit.Text = ""
            TextBoxCreditSales.Text = ""
            TextBoxTargetDate.Text = ""
            P2.Visible = False
            Table6.Visible = False
            tblPaymentGrid.Visible = False
            TableCollections.Visible = True
            LoadDB()

        End If


    End Sub


    Protected Sub ImageButton2_Click(sender As Object, e As EventArgs)
        If IsPostBack Then

            Dim Droplistlocation1 As String = DropdownList1.SelectedItem.Text
            Dim CREDIT_LIMIT As String = Trim(TextBox3.Text)
            Dim PERDAY As String = Trim(TextBox4.Text)
            If DropdownList1.SelectedItem.Text = "select" Then
                Call showMsg("Please select a branch.", "")
                Exit Sub
            End If
            If (CREDIT_LIMIT = "") And (PERDAY = "") Then
                Call showMsg("Please enter Any one value.", "")
                Exit Sub
            End If
            If Not (CommonControl.isNumberDR(TextBox3.Text) And
                    CommonControl.isNumberDR(TextBox4.Text)) Then
                Call showMsg("Only enter cash...", "")
                Exit Sub
            End If

            Dim creditInfoModel As New CreditModel

            Dim CreditInfocontrol As New CreditControl
            creditInfoModel.BRANCH_NO = DropdownList1.SelectedItem.Text
            creditInfoModel.CREDIT_LIMIT = TextBox3.Text
            creditInfoModel.PER_DAY = TextBox4.Text
            Dim insertCredit As Boolean = CreditInfocontrol.Insert_Credit_info(creditInfoModel)
            If (insertCredit = True) Then
                Call showMsg("Data updated successfully in " & DropdownList1.SelectedItem.Text, "")

            End If
            TextBox3.Text = ""
            TextBox4.Text = ""
            TextBox5.Text = "0.00"
            TextBox6.Text = "0.00"
            LoadDB()
            DropdownList1.Text = "Select"

        End If
    End Sub



    Public Sub LoadDB()

        Dim creditInfoModel As New CreditModel
        Dim CreditInfocontrol As New CreditControl
        Dim _Datatble As DataTable = CreditInfocontrol.Get_Credit_info(creditInfoModel)
        Dim _dataview As New DataView(_Datatble)
        CreitInfo.DataSource = _dataview
        CreitInfo.DataBind()
        TextBox3.Text = ""
        TextBox4.Text = ""
        TextBox5.Text = "0.00"
        TextBox6.Text = "0.00"
       
    End Sub

    Protected Sub ImageButton3_Click(sender As Object, e As EventArgs)
        Dim GSTIN As String = Trim(TextBox3.Text)

        If DropDownList2.SelectedItem.Text = "select" Then
            Call showMsg("Please select a branch.", "")
            Exit Sub
        End If
        If (TextBox7.Text = "") Then
            Call showMsg("Please enter the value.", "")
            Exit Sub
        End If


        Dim creditInfoModel As New CreditModel
        Dim CreditIcontrol As New CreditControl
        creditInfoModel.ship_Name = DropDownList2.SelectedItem.Text
        creditInfoModel.GSTIN = TextBox7.Text
        Dim InsertGSTIN As Boolean = CreditIcontrol.Insert_GSTIN(creditInfoModel)
        If (InsertGSTIN = True) Then
            Call showMsg("Data updated successfully in " & DropDownList2.SelectedItem.Text, "")
        End If

        TextBox7.Text = ""
        TextBox1.Text = "0.00"
        DropDownList2.Text = "Select"

    End Sub

    Protected Sub DropDownList2_SelectedIndexChanged(sender As Object, e As EventArgs)

        If DropDownList2.SelectedValue = "Select" Then

            TextBox7.Text = ""
            TextBox1.Text = "0.00"

        Else

            Dim cnstr As String = ConfigurationManager.ConnectionStrings("cnstr").ConnectionString
            Using Con As SqlConnection = New SqlConnection(cnstr)
                Using cmd As SqlCommand = New SqlCommand("select GSTIN From M_ship_base Where ship_Name='" & DropDownList2.SelectedItem.Text & "'; ")
                    cmd.CommandType = CommandType.Text
                    cmd.Connection = Con
                    Con.Open()
                    Using sdr As SqlDataReader = cmd.ExecuteReader()
                        sdr.Read()
                        TextBox1.Text = sdr("GSTIN").ToString
                    End Using
                    Con.Close()
                End Using
            End Using
        End If
    End Sub

    Public Sub LoadDB1()
        Dim creditInfoModel As New CreditModel
        Dim CreditInfocontrol As New CreditControl
        Dim _Datatble As DataTable = CreditInfocontrol.Get_GSTIN(creditInfoModel)
        Dim _dataview As New DataView(_Datatble)
        GridView2.DataSource = _dataview
        GridView2.DataBind()
        TextBox1.Text = "0.00"
        TextBox7.Text = ""
        DropdownList1.SelectedItem.Text = "select"

    End Sub

    Protected Sub ImageButton4_Click(sender As Object, e As EventArgs) Handles ImageButton4.Click

        If TextFromDate.Text = "" And TextToDate.Text = "" Then
            Call showMsg("Please Select The Date", "")
            Exit Sub
            'ElseIf TextToDate.Text = "" Then
            '    Call showMsg("Please Select To Date", "")
            '    Exit Sub
        ElseIf DropDownList4.SelectedItem.Text = "Select" Then
            Call showMsg("Please Select To Type", "")
            Exit Sub
        ElseIf DropdownList3.SelectedItem.Text = "ALL" Then
            Table5.Visible = False
            Table7.Visible = False
            tblSingle.Visible = False
            tblSingleRWT.Visible = False
        ElseIf DropdownList3.SelectedItem.Text = "" Then
            tblSingle.Visible = True
            tblSingleRWT.Visible = True
            Table5.Visible = False
            Table7.Visible = False
        End If



        Dim DtFrom As String = ""
        Dim DtTo As String = ""
        DtFrom = Trim(TextFromDate.Text)
        DtTo = Trim(TextToDate.Text)


        ' Task = 1 'Assign From or To or both filter
        If Len(Trim(DtFrom)) > 7 And Len(Trim(DtTo)) > 7 Then
            Dim date1, date2 As Date
            date1 = Date.Parse(TextFromDate.Text)
            date2 = Date.Parse(TextToDate.Text)
            If (DateTime.Compare(date1, date2) > 0) Then ' which means ("date1 > date2") 
                Call showMsg("Please verify from date and to date", "")
                Exit Sub
            End If
        End If



        If Len(DtFrom) > 5 And DtTo = "" Then
            DtTo = DtFrom
        End If
        If DtFrom = "" And Len(DtTo) > 5 Then
            DtFrom = DtTo
        End If


        Dim dtTbl1, dtTbl2 As DateTime
        If (Trim(DtFrom) <> "") Then
            If DateTime.TryParse(DtFrom, dtTbl1) Then
                dtTbl1 = DateTime.Parse(Trim(DtFrom)).ToShortDateString
            Else
                Call showMsg("There is an error in the date specification", "")
                Exit Sub
            End If
        End If
        If (Trim(DtTo) <> "") Then
            If DateTime.TryParse(DtTo, dtTbl2) Then
                dtTbl2 = DateTime.Parse(Trim(DtTo)).ToShortDateString
            Else
                Call showMsg("There is an error in the date specification", "")
                Exit Sub
            End If
        End If
        'End If

        Try
            Dim Sc_Drs_Model As ScDsrModel = New ScDsrModel()
            Dim Sc_Drs_Control As ScDsrControl = New ScDsrControl()

            Sc_Drs_Model.BranchName = DropdownList3.SelectedItem.Text
            Sc_Drs_Model.DateFrom = TextFromDate.Text
            Sc_Drs_Model.DateTo = TextToDate.Text




            'Dim dtScDsrViewCount As DataTable = Sc_Drs_Control.StoreManagement_drsCounts(Sc_Drs_Model)

            'Sc_Drs_Control.StoreManagement_drsCounts(Sc_Drs_Model)

            Dim PositionStart As Integer = 0
            If Len(Trim(DtFrom)) > 7 Then
                PositionStart = InStr(1, DtFrom, "/")
                If PositionStart = 0 Then ' There is no _ symbol in the file name
                    Call showMsg("Please verify date format (MM/DD/YYYY)", "")
                    Exit Sub
                End If
            End If
            If Len(Trim(DtTo)) > 7 Then
                PositionStart = InStr(1, DtTo, "/")
                If PositionStart = 0 Then ' There is no _ symbol in the file name
                    Call showMsg("Please verify date format(MM/DD/YYYY)", "")
                    Exit Sub
                End If
            End If

            'DateConversion
            Dim DtConvFrom() As String
            DtConvFrom = Split(DtFrom, "/")
            If Len(DtConvFrom(0)) = 1 Then
                DtConvFrom(0) = "0" & DtConvFrom(0)
            End If
            If Len(DtConvFrom(1)) = 1 Then
                DtConvFrom(1) = "0" & DtConvFrom(1)
            End If
            Sc_Drs_Model.DateFrom = DtConvFrom(2) & "/" & DtConvFrom(0) & "/" & DtConvFrom(1)
            'DateConversion
            Dim DtConvTo() As String
            DtConvTo = Split(DtTo, "/")
            If Len(DtConvTo(0)) = 1 Then
                DtConvTo(0) = "0" & DtConvTo(0)
            End If
            If Len(DtConvTo(1)) = 1 Then
                DtConvTo(1) = "0" & DtConvTo(1)
            End If

            Sc_Drs_Model.DateTo = DtConvTo(2) & "/" & DtConvTo(0) & "/" & DtConvTo(1)


            'Export started

            'Dim userName As String = Session("user_Name")
            'Dim userid As String = Session("user_id")

            If DropDownList4.SelectedItem.Text = "CSV" Then
                Dim _ScDsrModel As ScDsrModel = New ScDsrModel()
                Dim _ScDsrControl As ScDsrControl = New ScDsrControl()
                Dim strFileName As String = ""
                '_ScDsrModel.UserId = userid
                '_ScDsrModel.UserName = userName

                If DropdownList3.SelectedItem.Text = "ALL" Then
                    Dim AllBranches As String
                    For i As Integer = 1 To DropdownList3.Items.Count - 1
                        AllBranches = AllBranches + "'" + DropdownList3.Items(i).Text + "',"
                    Next
                    AllBranches = Left(AllBranches, Len(AllBranches) - 1)
                    Sc_Drs_Model.BranchName = AllBranches
                    'strFileName = _ScDsrModel.BranchName & "rajatest" & ".csv"
                    Dim dtScDsrView As DataTable = Sc_Drs_Control.StoreManagement_drsCounts(Sc_Drs_Model)
                    Dim dtScDsrRevWoTaxView As DataTable = Sc_Drs_Control.StoreManagement_RevWoTax(Sc_Drs_Model)
                    If (dtScDsrView Is Nothing) Or (dtScDsrView.Rows.Count = 0) Then
                        Table5.Visible = False
                        tblSingle.Visible = False
                        tblSingleRWT.Visible = False
                        'If no Records
                        Call showMsg("No records found for the month of " & DropDownMonth.SelectedItem.Text, "")
                        Exit Sub
                    Else
                        'for multi export
                        ExportExcel(dtScDsrView, dtScDsrRevWoTaxView, "ALL", "", DtConvFrom(2) & "" & DtConvFrom(0) & "" & DtConvFrom(1), DtConvTo(2) & "" & DtConvTo(0) & "" & DtConvTo(1))
                    End If
                    If (dtScDsrRevWoTaxView Is Nothing) Or (dtScDsrRevWoTaxView.Rows.Count = 0) Then
                        Table7.Visible = False
                        '   tblSingle.Visible = False
                        tblSingleRWT.Visible = False
                        'If no Records
                        Call showMsg("No records found for the month of " & DropDownMonth.SelectedItem.Text, "")
                        Exit Sub
                    Else
                        'for multi export
                        ' ExportExcel(dtScDsrRevWoTaxView, "ALL", "", DtConvFrom(2) & "" & DtConvFrom(0) & "" & DtConvFrom(1), DtConvTo(2) & "" & DtConvTo(0) & "" & DtConvTo(1))
                    End If
                Else
                    'for single
                    Dim Branche As String
                    Branche = DropdownList3.SelectedItem.Text
                    Sc_Drs_Model.BranchName = "'" & Branche & "'"
                    Dim _DataTable As DataTable = Sc_Drs_Control.StoreManagement_drsCounts(Sc_Drs_Model)
                    Dim dtScDsrRevWoTaxView As DataTable = Sc_Drs_Control.StoreManagement_RevWoTax(Sc_Drs_Model)
                    If (_DataTable Is Nothing) Or (_DataTable.Rows.Count = 0) Then
                        Table5.Visible = False
                        tblSingle.Visible = False
                        'tblSingleRWT.Visible = False
                        Call showMsg("No Record Found", "")
                        Exit Sub
                    Else
                        'for single export
                        ExportExcel(_DataTable, dtScDsrRevWoTaxView, "", Branche, DtConvFrom(2) & "" & DtConvFrom(0) & "" & DtConvFrom(1), DtConvTo(2) & "" & DtConvTo(0) & "" & DtConvTo(1))
                    End If
                    If (dtScDsrRevWoTaxView Is Nothing) Or (dtScDsrRevWoTaxView.Rows.Count = 0) Then
                        Table7.Visible = False
                        'tblSingle.Visible = False
                        tblSingleRWT.Visible = False
                        Call showMsg("No Record Found", "")
                        Exit Sub
                    Else
                        'for single export
                        'ExportExcel(dtScDsrRevWoTaxView, "", Branche, DtConvFrom(2) & "" & DtConvFrom(0) & "" & DtConvFrom(1), DtConvTo(2) & "" & DtConvTo(0) & "" & DtConvTo(1))
                    End If
                End If

            End If

            If DropdownList3.SelectedItem.Text = "ALL" Then

                Dim AllBranches As String
                For i As Integer = 1 To DropdownList3.Items.Count - 1
                    AllBranches = AllBranches + "'" + DropdownList3.Items(i).Text + "',"
                Next
                AllBranches = Left(AllBranches, Len(AllBranches) - 1)
                Sc_Drs_Model.BranchName = AllBranches
                Dim _DataTable As DataTable = Sc_Drs_Control.StoreManagement_drsCounts(Sc_Drs_Model)
                Dim dtScDsrRevWoTaxView As DataTable = Sc_Drs_Control.StoreManagement_RevWoTax(Sc_Drs_Model)
                If (_DataTable Is Nothing) Or (_DataTable.Rows.Count = 0) Then
                    tblSingle.Visible = False
                    Table5.Visible = False
                    Call showMsg("No Record Found", "")
                    Exit Sub
                    Table5.Visible = False
                Else
                    Table3.Visible = True
                    Table5.Visible = False

                    lblSSC1IW.Text = "0"
                    lblSSC1OW.Text = "0"
                    lblIO1.Text = "0"

                    lblSSC2IW.Text = "0"
                    lblSSC2OW.Text = "0"
                    lblIO2.Text = "0"

                    lblSSC3IW.Text = "0"
                    lblSSC3OW.Text = "0"
                    lblIO3.Text = "0"

                    lblSSC4IW.Text = "0"
                    lblSSC4OW.Text = "0"
                    lblIO4.Text = "0"

                    lblSSC5IW.Text = "0"
                    lblSSC5OW.Text = "0"
                    lblIO5.Text = "0"

                    lblSSC6IW.Text = "0"
                    lblSSC6OW.Text = "0"
                    lblIO6.Text = "0"

                    lblSSC7IW.Text = "0"
                    lblSSC7OW.Text = "0"
                    lblIO7.Text = "0"

                    lblSSC8IW.Text = "0"
                    lblSSC8OW.Text = "0"
                    lblIO8.Text = "0"

                    lblSSC9IW.Text = "0"
                    lblSSC9OW.Text = "0"
                    lblIO9.Text = "0"

                    'SSC10
                    lblSSC10IW.Text = "0"
                    lblSSC10OW.Text = "0"
                    lblI10.Text = "0"

                    'SSC11
                    lblSSC11IW.Text = "0"
                    lblSSC11OW.Text = "0"
                    lblI11.Text = "0"


                    Dim rwIndex As String
                    For Each item As DataRow In _DataTable.Rows
                        If (item.Item("Branch_name").ToString = "SSC1") Then
                            rwIndex = item.Table.Rows.IndexOf(item).ToString
                            If _DataTable.Rows(Convert.ToInt32(rwIndex))("IW_goods_total").ToString().Length > 0 Then
                                lblSSC1IW.Text = _DataTable.Rows(Convert.ToInt32(rwIndex))("IW_goods_total")
                            Else
                                lblSSC1IW.Text = 0
                            End If


                            If _DataTable.Rows(Convert.ToInt32(rwIndex))("OW_goods_total").ToString().Length > 0 Then
                                lblSSC1OW.Text = _DataTable.Rows(Convert.ToInt32(rwIndex))("OW_goods_total")
                            Else
                                lblSSC1OW.Text = 0
                            End If


                            If _DataTable.Rows(Convert.ToInt32(rwIndex))("TotalGoods").ToString().Length > 0 Then
                                lblIO1.Text = _DataTable.Rows(Convert.ToInt32(rwIndex))("TotalGoods")
                            Else
                                lblIO1.Text = 0
                            End If



                        ElseIf (item.Item("Branch_name").ToString = "SSC2") Then

                            rwIndex = item.Table.Rows.IndexOf(item).ToString
                            If _DataTable.Rows(Convert.ToInt32(rwIndex))("IW_goods_total").ToString().Length > 0 Then
                                lblSSC2IW.Text = _DataTable.Rows(Convert.ToInt32(rwIndex))("IW_goods_total")
                            Else
                                lblSSC2IW.Text = 0
                            End If


                            If _DataTable.Rows(Convert.ToInt32(rwIndex))("OW_goods_total").ToString().Length > 0 Then
                                lblSSC2OW.Text = _DataTable.Rows(Convert.ToInt32(rwIndex))("OW_goods_total")
                            Else
                                lblSSC2OW.Text = 0
                            End If


                            If _DataTable.Rows(Convert.ToInt32(rwIndex))("TotalGoods").ToString().Length > 0 Then
                                lblIO2.Text = _DataTable.Rows(Convert.ToInt32(rwIndex))("TotalGoods")
                            Else
                                lblIO2.Text = 0
                            End If


                        ElseIf (item.Item("Branch_name").ToString = "SSC3") Then
                            rwIndex = item.Table.Rows.IndexOf(item).ToString

                            If _DataTable.Rows(Convert.ToInt32(rwIndex))("IW_goods_total").ToString().Length > 0 Then
                                lblSSC3IW.Text = _DataTable.Rows(Convert.ToInt32(rwIndex))("IW_goods_total")
                            Else
                                lblSSC3IW.Text = 0
                            End If


                            If _DataTable.Rows(Convert.ToInt32(rwIndex))("OW_goods_total").ToString().Length > 0 Then
                                lblSSC3OW.Text = _DataTable.Rows(Convert.ToInt32(rwIndex))("OW_goods_total")
                            Else
                                lblSSC3OW.Text = 0
                            End If


                            If _DataTable.Rows(Convert.ToInt32(rwIndex))("TotalGoods").ToString().Length > 0 Then
                                lblIO3.Text = _DataTable.Rows(Convert.ToInt32(rwIndex))("TotalGoods")
                            Else
                                lblIO3.Text = 0
                            End If

                        ElseIf (item.Item("Branch_name").ToString = "SSC4") Then
                            rwIndex = item.Table.Rows.IndexOf(item).ToString
                            If _DataTable.Rows(Convert.ToInt32(rwIndex))("IW_goods_total").ToString().Length > 0 Then
                                lblSSC4IW.Text = _DataTable.Rows(Convert.ToInt32(rwIndex))("IW_goods_total")
                            Else
                                lblSSC4IW.Text = 0
                            End If


                            If _DataTable.Rows(Convert.ToInt32(rwIndex))("OW_goods_total").ToString().Length > 0 Then
                                lblSSC4OW.Text = _DataTable.Rows(Convert.ToInt32(rwIndex))("OW_goods_total")
                            Else
                                lblSSC4OW.Text = 0
                            End If


                            If _DataTable.Rows(Convert.ToInt32(rwIndex))("TotalGoods").ToString().Length > 0 Then
                                lblIO4.Text = _DataTable.Rows(Convert.ToInt32(rwIndex))("TotalGoods")
                            Else
                                lblIO4.Text = 0
                            End If

                        ElseIf (item.Item("Branch_name").ToString = "SSC5") Then
                            rwIndex = item.Table.Rows.IndexOf(item).ToString

                            If _DataTable.Rows(Convert.ToInt32(rwIndex))("IW_goods_total").ToString().Length > 0 Then
                                lblSSC5IW.Text = _DataTable.Rows(Convert.ToInt32(rwIndex))("IW_goods_total")
                            Else
                                lblSSC5IW.Text = 0
                            End If


                            If _DataTable.Rows(Convert.ToInt32(rwIndex))("OW_goods_total").ToString().Length > 0 Then
                                lblSSC5OW.Text = _DataTable.Rows(Convert.ToInt32(rwIndex))("OW_goods_total")
                            Else
                                lblSSC5OW.Text = 0
                            End If

                            If _DataTable.Rows(Convert.ToInt32(rwIndex))("TotalGoods").ToString().Length > 0 Then
                                lblIO5.Text = _DataTable.Rows(Convert.ToInt32(rwIndex))("TotalGoods")
                            Else
                                lblIO5.Text = 0
                            End If

                        ElseIf (item.Item("Branch_name").ToString = "SSC6") Then
                            rwIndex = item.Table.Rows.IndexOf(item).ToString


                            If _DataTable.Rows(Convert.ToInt32(rwIndex))("IW_goods_total").ToString().Length > 0 Then
                                lblSSC6IW.Text = _DataTable.Rows(Convert.ToInt32(rwIndex))("IW_goods_total")
                            Else
                                lblSSC6IW.Text = 0
                            End If


                            If _DataTable.Rows(Convert.ToInt32(rwIndex))("OW_goods_total").ToString().Length > 0 Then
                                lblSSC6OW.Text = _DataTable.Rows(Convert.ToInt32(rwIndex))("OW_goods_total")
                            Else
                                lblSSC6OW.Text = 0
                            End If

                            If _DataTable.Rows(Convert.ToInt32(rwIndex))("TotalGoods").ToString().Length > 0 Then
                                lblIO6.Text = _DataTable.Rows(Convert.ToInt32(rwIndex))("TotalGoods")
                            Else
                                lblIO6.Text = 0
                            End If


                        ElseIf (item.Item("Branch_name").ToString = "SSC7") Then
                            rwIndex = item.Table.Rows.IndexOf(item).ToString
                            If _DataTable.Rows(Convert.ToInt32(rwIndex))("IW_goods_total").ToString().Length > 0 Then
                                lblSSC7IW.Text = _DataTable.Rows(Convert.ToInt32(rwIndex))("IW_goods_total")
                            Else
                                lblSSC7IW.Text = 0
                            End If


                            If _DataTable.Rows(Convert.ToInt32(rwIndex))("OW_goods_total").ToString().Length > 0 Then
                                lblSSC7OW.Text = _DataTable.Rows(Convert.ToInt32(rwIndex))("OW_goods_total")
                            Else
                                lblSSC7OW.Text = 0
                            End If

                            If _DataTable.Rows(Convert.ToInt32(rwIndex))("TotalGoods").ToString().Length > 0 Then
                                lblIO7.Text = _DataTable.Rows(Convert.ToInt32(rwIndex))("TotalGoods")
                            Else
                                lblIO7.Text = 0
                            End If

                        ElseIf (item.Item("Branch_name").ToString = "SSC8") Then
                            rwIndex = item.Table.Rows.IndexOf(item).ToString

                            If _DataTable.Rows(Convert.ToInt32(rwIndex))("IW_goods_total").ToString().Length > 0 Then
                                lblSSC8IW.Text = _DataTable.Rows(Convert.ToInt32(rwIndex))("IW_goods_total")
                            Else
                                lblSSC8IW.Text = 0
                            End If


                            If _DataTable.Rows(Convert.ToInt32(rwIndex))("OW_goods_total").ToString().Length > 0 Then
                                lblSSC8OW.Text = _DataTable.Rows(Convert.ToInt32(rwIndex))("OW_goods_total")
                            Else
                                lblSSC8OW.Text = 0
                            End If

                            If _DataTable.Rows(Convert.ToInt32(rwIndex))("TotalGoods").ToString().Length > 0 Then
                                lblIO8.Text = _DataTable.Rows(Convert.ToInt32(rwIndex))("TotalGoods")
                            Else
                                lblIO8.Text = 0
                            End If

                        ElseIf (item.Item("Branch_name").ToString = "SSC9") Then
                            rwIndex = item.Table.Rows.IndexOf(item).ToString
                            If _DataTable.Rows(Convert.ToInt32(rwIndex))("IW_goods_total").ToString().Length > 0 Then
                                lblSSC9IW.Text = _DataTable.Rows(Convert.ToInt32(rwIndex))("IW_goods_total")
                            Else
                                lblSSC9IW.Text = 0
                            End If


                            If _DataTable.Rows(Convert.ToInt32(rwIndex))("OW_goods_total").ToString().Length > 0 Then
                                lblSSC9OW.Text = _DataTable.Rows(Convert.ToInt32(rwIndex))("OW_goods_total")
                            Else
                                lblSSC9OW.Text = 0
                            End If

                            If _DataTable.Rows(Convert.ToInt32(rwIndex))("TotalGoods").ToString().Length > 0 Then
                                lblIO9.Text = _DataTable.Rows(Convert.ToInt32(rwIndex))("TotalGoods")
                            Else
                                lblIO9.Text = 0
                            End If

                            'SSC10
                        ElseIf (item.Item("Branch_name").ToString = "SSC10") Then
                            rwIndex = item.Table.Rows.IndexOf(item).ToString

                            If _DataTable.Rows(Convert.ToInt32(rwIndex))("IW_goods_total").ToString().Length > 0 Then
                                lblSSC10IW.Text = _DataTable.Rows(Convert.ToInt32(rwIndex))("IW_goods_total")
                            Else
                                lblSSC10IW.Text = 0
                            End If


                            If _DataTable.Rows(Convert.ToInt32(rwIndex))("OW_goods_total").ToString().Length > 0 Then
                                lblSSC10OW.Text = _DataTable.Rows(Convert.ToInt32(rwIndex))("OW_goods_total")
                            Else
                                lblSSC10OW.Text = 0
                            End If

                            If _DataTable.Rows(Convert.ToInt32(rwIndex))("TotalGoods").ToString().Length > 0 Then
                                lblI10.Text = _DataTable.Rows(Convert.ToInt32(rwIndex))("TotalGoods")
                            Else
                                lblI10.Text = 0
                            End If
                        ElseIf (item.Item("Branch_name").ToString = "SSC11") Then
                            rwIndex = item.Table.Rows.IndexOf(item).ToString

                            If _DataTable.Rows(Convert.ToInt32(rwIndex))("IW_goods_total").ToString().Length > 0 Then
                                lblSSC11IW.Text = _DataTable.Rows(Convert.ToInt32(rwIndex))("IW_goods_total")
                            Else
                                lblSSC11IW.Text = 0
                            End If


                            If _DataTable.Rows(Convert.ToInt32(rwIndex))("OW_goods_total").ToString().Length > 0 Then
                                lblSSC11OW.Text = _DataTable.Rows(Convert.ToInt32(rwIndex))("OW_goods_total")
                            Else
                                lblSSC11OW.Text = 0
                            End If

                            If _DataTable.Rows(Convert.ToInt32(rwIndex))("TotalGoods").ToString().Length > 0 Then
                                lblI11.Text = _DataTable.Rows(Convert.ToInt32(rwIndex))("TotalGoods")
                            Else
                                lblI11.Text = 0
                            End If
                        End If

                    Next
                    Table5.Visible = True
                    'Table5.Visible = False
                End If
                If (dtScDsrRevWoTaxView Is Nothing) Or (dtScDsrRevWoTaxView.Rows.Count = 0) Then
                    'tblSingle.Visible = False
                    tblSingleRWT.Visible = False
                    Table7.Visible = False
                    Call showMsg("No Record Found", "")
                    Exit Sub
                    Table7.Visible = False
                Else
                    Table3.Visible = True
                    Table7.Visible = False



                    Dim rwIndex As String
                    For Each item As DataRow In dtScDsrRevWoTaxView.Rows
                        If (item.Item("Branch_name").ToString = "SSC1") Then
                            lblRWTSSC1.Text = item.Item("RevWoTax").ToString
                        ElseIf (item.Item("Branch_name").ToString = "SSC2") Then
                            lblRWTSSC2.Text = item.Item("RevWoTax").ToString
                        ElseIf (item.Item("Branch_name").ToString = "SSC3") Then
                            lblRWTSSC3.Text = item.Item("RevWoTax").ToString
                        ElseIf (item.Item("Branch_name").ToString = "SSC4") Then
                            lblRWTSSC4.Text = item.Item("RevWoTax").ToString
                        ElseIf (item.Item("Branch_name").ToString = "SSC5") Then
                            lblRWTSSC5.Text = item.Item("RevWoTax").ToString
                        ElseIf (item.Item("Branch_name").ToString = "SSC6") Then
                            lblRWTSSC6.Text = item.Item("RevWoTax").ToString
                        ElseIf (item.Item("Branch_name").ToString = "SSC7") Then
                            lblRWTSSC7.Text = item.Item("RevWoTax").ToString
                        ElseIf (item.Item("Branch_name").ToString = "SSC8") Then
                            lblRWTSSC8.Text = item.Item("RevWoTax").ToString
                        ElseIf (item.Item("Branch_name").ToString = "SSC9") Then
                            lblRWTSSC9.Text = item.Item("RevWoTax").ToString
                        ElseIf (item.Item("Branch_name").ToString = "SSC10") Then
                            lblRWTSSC10.Text = item.Item("RevWoTax").ToString
                        ElseIf (item.Item("Branch_name").ToString = "SSC11") Then
                            lblRWTSSC11.Text = item.Item("RevWoTax").ToString
                        End If

                    Next
                    Table7.Visible = True
                    'Table5.Visible = False
                End If
            Else
                Dim Branche As String
                Branche = DropdownList3.SelectedItem.Text
                Sc_Drs_Model.BranchName = "'" & Branche & "'"
                Dim _DataTable As DataTable = Sc_Drs_Control.StoreManagement_drsCounts(Sc_Drs_Model)
                Dim dtScDsrRevWoTaxView As DataTable = Sc_Drs_Control.StoreManagement_RevWoTax(Sc_Drs_Model)
                If (_DataTable Is Nothing) Or (_DataTable.Rows.Count = 0) Then
                    tblSingle.Visible = False

                    Table5.Visible = False
                    'Call showMsg("No Record Found", "")
                    'Exit Sub
                Else
                    tblSingle.Visible = True
                    If _DataTable.Rows(0)("Branch_name") = "SSC1" Then

                        lblBranchname.Text = _DataTable.Rows(0)("Branch_name")

                        If _DataTable.Rows(0)("IW_goods_total").ToString().Length > 0 Then
                            lblSSCSIW.Text = _DataTable.Rows(0)("IW_goods_total")
                        Else
                            lblSSCSIW.Text = 0
                        End If

                        If _DataTable.Rows(0)("OW_goods_total").ToString().Length > 0 Then
                            lblSSCSOW.Text = _DataTable.Rows(0)("OW_goods_total")
                        Else
                            lblSSCSOW.Text = 0
                        End If
                        If _DataTable.Rows(0)("TotalGoods").ToString().Length > 0 Then
                            lblTotalSingle.Text = _DataTable.Rows(0)("TotalGoods")
                        Else
                            lblTotalSingle.Text = 0
                        End If

                    ElseIf _DataTable.Rows(0)("Branch_name") = "SSC2" Then
                        lblBranchname.Text = _DataTable.Rows(0)("Branch_name")
                        If _DataTable.Rows(0)("IW_goods_total").ToString().Length > 0 Then
                            lblSSCSIW.Text = _DataTable.Rows(0)("IW_goods_total")
                        Else
                            lblSSCSIW.Text = 0
                        End If

                        If _DataTable.Rows(0)("OW_goods_total").ToString().Length > 0 Then
                            lblSSCSOW.Text = _DataTable.Rows(0)("OW_goods_total")
                        Else
                            lblSSCSOW.Text = 0
                        End If
                        If _DataTable.Rows(0)("TotalGoods").ToString().Length > 0 Then
                            lblTotalSingle.Text = _DataTable.Rows(0)("TotalGoods")
                        Else
                            lblTotalSingle.Text = 0
                        End If

                    ElseIf _DataTable.Rows(0)("Branch_name") = "SSC3" Then
                        lblBranchname.Text = _DataTable.Rows(0)("Branch_name")
                        If _DataTable.Rows(0)("IW_goods_total").ToString().Length > 0 Then
                            lblSSCSIW.Text = _DataTable.Rows(0)("IW_goods_total")
                        Else
                            lblSSCSIW.Text = 0
                        End If

                        If _DataTable.Rows(0)("OW_goods_total").ToString().Length > 0 Then
                            lblSSCSOW.Text = _DataTable.Rows(0)("OW_goods_total")
                        Else
                            lblSSCSOW.Text = 0
                        End If
                        If _DataTable.Rows(0)("TotalGoods").ToString().Length > 0 Then
                            lblTotalSingle.Text = _DataTable.Rows(0)("TotalGoods")
                        Else
                            lblTotalSingle.Text = 0
                        End If


                    ElseIf _DataTable.Rows(0)("Branch_name") = "SSC4" Then
                        lblBranchname.Text = _DataTable.Rows(0)("Branch_name")
                        If _DataTable.Rows(0)("IW_goods_total").ToString().Length > 0 Then
                            lblSSCSIW.Text = _DataTable.Rows(0)("IW_goods_total")
                        Else
                            lblSSCSIW.Text = 0
                        End If

                        If _DataTable.Rows(0)("OW_goods_total").ToString().Length > 0 Then
                            lblSSCSOW.Text = _DataTable.Rows(0)("OW_goods_total")
                        Else
                            lblSSCSOW.Text = 0
                        End If
                        If _DataTable.Rows(0)("TotalGoods").ToString().Length > 0 Then
                            lblTotalSingle.Text = _DataTable.Rows(0)("TotalGoods")
                        Else
                            lblTotalSingle.Text = 0
                        End If

                    ElseIf _DataTable.Rows(0)("Branch_name") = "SSC5" Then
                        lblBranchname.Text = _DataTable.Rows(0)("Branch_name")
                        If _DataTable.Rows(0)("IW_goods_total").ToString().Length > 0 Then
                            lblSSCSIW.Text = _DataTable.Rows(0)("IW_goods_total")
                        Else
                            lblSSCSIW.Text = 0
                        End If

                        If _DataTable.Rows(0)("OW_goods_total").ToString().Length > 0 Then
                            lblSSCSOW.Text = _DataTable.Rows(0)("OW_goods_total")
                        Else
                            lblSSCSOW.Text = 0
                        End If
                        If _DataTable.Rows(0)("TotalGoods").ToString().Length > 0 Then
                            lblTotalSingle.Text = _DataTable.Rows(0)("TotalGoods")
                        Else
                            lblTotalSingle.Text = 0
                        End If

                    ElseIf _DataTable.Rows(0)("Branch_name") = "SSC6" Then
                        lblBranchname.Text = _DataTable.Rows(0)("Branch_name")
                        If _DataTable.Rows(0)("IW_goods_total").ToString().Length > 0 Then
                            lblSSCSIW.Text = _DataTable.Rows(0)("IW_goods_total")
                        Else
                            lblSSCSIW.Text = 0
                        End If

                        If _DataTable.Rows(0)("OW_goods_total").ToString().Length > 0 Then
                            lblSSCSOW.Text = _DataTable.Rows(0)("OW_goods_total")
                        Else
                            lblSSCSOW.Text = 0
                        End If
                        If _DataTable.Rows(0)("TotalGoods").ToString().Length > 0 Then
                            lblTotalSingle.Text = _DataTable.Rows(0)("TotalGoods")
                        Else
                            lblTotalSingle.Text = 0
                        End If

                    ElseIf _DataTable.Rows(0)("Branch_name") = "SSC7" Then
                        lblBranchname.Text = _DataTable.Rows(0)("Branch_name")
                        If _DataTable.Rows(0)("IW_goods_total").ToString().Length > 0 Then
                            lblSSCSIW.Text = _DataTable.Rows(0)("IW_goods_total")
                        Else
                            lblSSCSIW.Text = 0
                        End If

                        If _DataTable.Rows(0)("OW_goods_total").ToString().Length > 0 Then
                            lblSSCSOW.Text = _DataTable.Rows(0)("OW_goods_total")
                        Else
                            lblSSCSOW.Text = 0
                        End If
                        If _DataTable.Rows(0)("TotalGoods").ToString().Length > 0 Then
                            lblTotalSingle.Text = _DataTable.Rows(0)("TotalGoods")
                        Else
                            lblTotalSingle.Text = 0
                        End If

                    ElseIf _DataTable.Rows(0)("Branch_name") = "SSC8" Then
                        lblBranchname.Text = _DataTable.Rows(0)("Branch_name")
                        If _DataTable.Rows(0)("IW_goods_total").ToString().Length > 0 Then
                            lblSSCSIW.Text = _DataTable.Rows(0)("IW_goods_total")
                        Else
                            lblSSCSIW.Text = 0
                        End If

                        If _DataTable.Rows(0)("OW_goods_total").ToString().Length > 0 Then
                            lblSSCSOW.Text = _DataTable.Rows(0)("OW_goods_total")
                        Else
                            lblSSCSOW.Text = 0
                        End If
                        If _DataTable.Rows(0)("TotalGoods").ToString().Length > 0 Then
                            lblTotalSingle.Text = _DataTable.Rows(0)("TotalGoods")
                        Else
                            lblTotalSingle.Text = 0
                        End If
                    ElseIf _DataTable.Rows(0)("Branch_name") = "SSC9" Then
                        lblBranchname.Text = _DataTable.Rows(0)("Branch_name")
                        If _DataTable.Rows(0)("IW_goods_total").ToString().Length > 0 Then
                            lblSSCSIW.Text = _DataTable.Rows(0)("IW_goods_total")
                        Else
                            lblSSCSIW.Text = 0
                        End If

                        If _DataTable.Rows(0)("OW_goods_total").ToString().Length > 0 Then
                            lblSSCSOW.Text = _DataTable.Rows(0)("OW_goods_total")
                        Else
                            lblSSCSOW.Text = 0
                        End If
                        If _DataTable.Rows(0)("TotalGoods").ToString().Length > 0 Then
                            lblTotalSingle.Text = _DataTable.Rows(0)("TotalGoods")
                        Else
                            lblTotalSingle.Text = 0
                        End If
                        'SSC10
                    ElseIf _DataTable.Rows(0)("Branch_name") = "SSC10" Then
                        lblBranchname.Text = _DataTable.Rows(0)("Branch_name")
                        If _DataTable.Rows(0)("IW_goods_total").ToString().Length > 0 Then
                            lblSSCSIW.Text = _DataTable.Rows(0)("IW_goods_total")
                        Else
                            lblSSCSIW.Text = 0
                        End If

                        If _DataTable.Rows(0)("OW_goods_total").ToString().Length > 0 Then
                            lblSSCSOW.Text = _DataTable.Rows(0)("OW_goods_total")
                        Else
                            lblSSCSOW.Text = 0
                        End If
                        If _DataTable.Rows(0)("TotalGoods").ToString().Length > 0 Then
                            lblTotalSingle.Text = _DataTable.Rows(0)("TotalGoods")
                        Else
                            lblTotalSingle.Text = 0
                        End If
                    ElseIf _DataTable.Rows(0)("Branch_name") = "SSC11" Then
                        lblBranchname.Text = _DataTable.Rows(0)("Branch_name")
                        If _DataTable.Rows(0)("IW_goods_total").ToString().Length > 0 Then
                            lblSSCSIW.Text = _DataTable.Rows(0)("IW_goods_total")
                        Else
                            lblSSCSIW.Text = 0
                        End If

                        If _DataTable.Rows(0)("OW_goods_total").ToString().Length > 0 Then
                            lblSSCSOW.Text = _DataTable.Rows(0)("OW_goods_total")
                        Else
                            lblSSCSOW.Text = 0
                        End If
                        If _DataTable.Rows(0)("TotalGoods").ToString().Length > 0 Then
                            lblTotalSingle.Text = _DataTable.Rows(0)("TotalGoods")
                        Else
                            lblTotalSingle.Text = 0
                        End If
                    End If
                    Table3.Visible = True
                    Table5.Visible = False
                    'tblSingle.Visible = False
                End If
                If (dtScDsrRevWoTaxView Is Nothing) Or (dtScDsrRevWoTaxView.Rows.Count = 0) Then
                    'tblSingle.Visible = False
                    tblSingleRWT.Visible = False
                    Table7.Visible = False
                    Call showMsg("No Record Found", "")
                    Exit Sub
                Else
                    'tblSingle.Visible = True
                    tblSingleRWT.Visible = True

                    lblSSCName.Text = dtScDsrRevWoTaxView.Rows(0)("Branch_name")
                    lblSSCNameRWT.Text = dtScDsrRevWoTaxView.Rows(0)("RevWoTax")


                    Table3.Visible = True
                    Table7.Visible = False
                    tblSingleRWT.Visible = True
                    'tblSingle.Visible = False
                End If
            End If



        Catch ex As Exception
            Call showMsg(ex.Message, "")
        End Try

    End Sub


    Public Sub ExportExcel(ByVal dtScDsrView As DataTable, ByVal dtScDsrRevWoTaxView As DataTable, targetType As String, branch As String, fromdate As String, todate As String)

        Dim sb As StringBuilder = New StringBuilder()
        Dim arrayListIWOW As ArrayList = New ArrayList()
        Dim arrayListTotal As ArrayList = New ArrayList()

        For value As Integer = 0 To 21
            arrayListIWOW.Add(0)
        Next

        For values As Integer = 1 To 22
            If (values Mod 2 = 0) Then
                arrayListTotal.Add("0")
            Else
                arrayListTotal.Add("total")
            End If
        Next

        'Revenue Without Tax
        Dim arrayListRWT As ArrayList = New ArrayList()

        Dim TotalRWT As Decimal
        TotalRWT = 0.00


        For value As Integer = 0 To 10
            arrayListRWT.Add(0)
        Next

        For Each item As DataRow In dtScDsrRevWoTaxView.Rows
            If item.Item("Branch_name").ToString = "SSC1" Then
                arrayListRWT(0) = item.Item("RevWoTax").ToString
                TotalRWT += Convert.ToDecimal(item.Item("RevWoTax").ToString)
            ElseIf item.Item("Branch_name").ToString = "SSC2" Then
                arrayListRWT(1) = item.Item("RevWoTax").ToString
                TotalRWT += Convert.ToDecimal(item.Item("RevWoTax").ToString)
            ElseIf item.Item("Branch_name").ToString = "SSC3" Then
                arrayListRWT(2) = item.Item("RevWoTax").ToString
                TotalRWT += Convert.ToDecimal(item.Item("RevWoTax").ToString)
            ElseIf item.Item("Branch_name").ToString = "SSC4" Then
                arrayListRWT(3) = item.Item("RevWoTax").ToString
                TotalRWT += Convert.ToDecimal(item.Item("RevWoTax").ToString)
            ElseIf item.Item("Branch_name").ToString = "SSC5" Then
                arrayListRWT(4) = item.Item("RevWoTax").ToString
                TotalRWT += Convert.ToDecimal(item.Item("RevWoTax").ToString)
            ElseIf item.Item("Branch_name").ToString = "SSC6" Then
                arrayListRWT(5) = item.Item("RevWoTax").ToString
                TotalRWT += Convert.ToDecimal(item.Item("RevWoTax").ToString)
            ElseIf item.Item("Branch_name").ToString = "SSC7" Then
                arrayListRWT(6) = item.Item("RevWoTax").ToString
                TotalRWT += Convert.ToDecimal(item.Item("RevWoTax").ToString)
            ElseIf item.Item("Branch_name").ToString = "SSC8" Then
                arrayListRWT(7) = item.Item("RevWoTax").ToString
                TotalRWT += Convert.ToDecimal(item.Item("RevWoTax").ToString)
            ElseIf item.Item("Branch_name").ToString = "SSC9" Then
                arrayListRWT(8) = item.Item("RevWoTax").ToString
                TotalRWT += Convert.ToDecimal(item.Item("RevWoTax").ToString)
            ElseIf item.Item("Branch_name").ToString = "SSC10" Then
                arrayListRWT(9) = item.Item("RevWoTax").ToString
                TotalRWT += Convert.ToDecimal(item.Item("RevWoTax").ToString)
            ElseIf item.Item("Branch_name").ToString = "SSC11" Then
                arrayListRWT(10) = item.Item("RevWoTax").ToString
                TotalRWT += Convert.ToDecimal(item.Item("RevWoTax").ToString)
            End If

        Next
        'Revenue Without Tax
        'Total IW, OW and sum of IW & OW
        Dim sumIW As Integer
        Dim sumOW As Integer

        If dtScDsrView.Compute("SUM(IW_goods_total)", String.Empty).ToString().Length > 0 Then
            sumIW = Convert.ToInt32(dtScDsrView.Compute("SUM(IW_goods_total)", String.Empty))
        Else
            sumIW = 0
        End If

        If dtScDsrView.Compute("SUM(OW_goods_total)", String.Empty).ToString().Length > 0 Then
            sumOW = Convert.ToInt32(dtScDsrView.Compute("SUM(OW_goods_total)", String.Empty))
        Else
            sumOW = 0
        End If
        Dim sumIOW As Integer = (sumIW + sumOW)

        sb.Append(String.Format("{0},{1},{2},{3},{4},{5}", "", "", "", "", "", "") + Environment.NewLine)
        sb.Append(String.Format("{0},{1},{2},{3},{4}", drpManagementFunc.SelectedItem.Text, DateTime.Now.ToString("yyyy/MM/dd"), DateTime.Now.ToString("HH:mm"), "User:", Convert.ToString(Session("user_Name"))) + Environment.NewLine)
        sb.Append(String.Format("{0},{1},{2},{3},{4},{5}", "", "", "", "", "", "") + Environment.NewLine)
        sb.Append(String.Format("{0},{1},{2},{3},{4},{5}", "total IW", sumIW, "total OW", sumOW, "total", sumIOW) + Environment.NewLine)
        sb.Append(String.Format("{0},{1},{2},{3},{4},{5}", "", "", "", "", "", "") + Environment.NewLine)

        sb.Append(String.Format("{0},{1},{2},{3},{4},{5},{6},{7},{8},{9},{10},{11},{12},{13},{14},{15},{16},{17},{18},{19},{20},{21}", "SSC1", "", "SSC2", "", "SSC3", "", "SSC4", "", "SSC5", "", "SSC6", "", "SSC7", "", "SSC8", "", "SSC9", "", "SSC10", "", "SSC11", "") + Environment.NewLine)
        'sb.Append(String.Format("{0},{1},{2},{3},{4},{5},{6},{7},{8},{9},{10},{11},{12},{13},{14},{15},{16},{17}", "SSC1", "", "SSC2", "", "SSC3", "", "SSC4", "", "SSC5", "", "SSC6", "", "SSC7", "", "SSC8", "", "SSC9", "") + Environment.NewLine)
        'sb.Append(String.Format("{0},{1},{2},{3},{4},{5},{6},{7},{8},{9},{10},{11},{12},{13},{14},{15},{16},{17}", "IW", "OW", "IW", "OW", "IW", "OW", "IW", "OW", "IW", "OW", "IW", "OW", "IW", "OW", "IW", "OW", "IW", "OW") + Environment.NewLine)
        sb.Append(String.Format("{0},{1},{2},{3},{4},{5},{6},{7},{8},{9},{10},{11},{12},{13},{14},{15},{16},{17},{18},{19},{20},{21}", "IW", "OW", "IW", "OW", "IW", "OW", "IW", "OW", "IW", "OW", "IW", "OW", "IW", "OW", "IW", "OW", "IW", "OW", "IW", "OW", "IW", "OW") + Environment.NewLine)



        Dim rowIndex As String
        Dim rowIWOWIndex As Integer
        Dim rowTotalIndex As Integer
        Dim incValue As Integer
        Dim incTotal As Integer
        incValue = 1
        incTotal = 1
        rowIWOWIndex = 0
        rowTotalIndex = 0

        'for IW_goods_total & OW_goods_total

        For Each item As DataRow In dtScDsrView.Rows
            If targetType = "ALL" Then

                'If (item.Item("Branch_name").ToString = "SSC" + incValue.ToString) Then
                rowIndex = item.Table.Rows.IndexOf(item).ToString
                If item.Item("Branch_name").ToString = "SSC1" Then
                    If dtScDsrView.Rows(Convert.ToInt32(rowIndex))("IW_goods_total").ToString().Length > 0 Then
                        arrayListIWOW(0) = dtScDsrView.Rows(Convert.ToInt32(rowIndex))("IW_goods_total").ToString
                    Else
                        arrayListIWOW(0) = 0
                    End If
                    If dtScDsrView.Rows(Convert.ToInt32(rowIndex))("OW_goods_total").ToString().Length > 0 Then
                        arrayListIWOW(1) = dtScDsrView.Rows(Convert.ToInt32(rowIndex))("OW_goods_total").ToString()
                    Else
                        arrayListIWOW(1) = 0
                    End If

                ElseIf (item.Item("Branch_name").ToString = "SSC2") Then
                    If dtScDsrView.Rows(Convert.ToInt32(rowIndex))("IW_goods_total").ToString().Length > 0 Then
                        arrayListIWOW(2) = dtScDsrView.Rows(Convert.ToInt32(rowIndex))("IW_goods_total").ToString
                    Else
                        arrayListIWOW(2) = 0
                    End If
                    If dtScDsrView.Rows(Convert.ToInt32(rowIndex))("OW_goods_total").ToString().Length > 0 Then
                        arrayListIWOW(3) = dtScDsrView.Rows(Convert.ToInt32(rowIndex))("OW_goods_total").ToString()
                    Else
                        arrayListIWOW(3) = 0
                    End If

                ElseIf (item.Item("Branch_name").ToString = "SSC3") Then
                    If dtScDsrView.Rows(Convert.ToInt32(rowIndex))("IW_goods_total").ToString().Length > 0 Then
                        arrayListIWOW(4) = dtScDsrView.Rows(Convert.ToInt32(rowIndex))("IW_goods_total").ToString
                    Else
                        arrayListIWOW(4) = 0
                    End If
                    If dtScDsrView.Rows(Convert.ToInt32(rowIndex))("OW_goods_total").ToString().Length > 0 Then

                        arrayListIWOW(5) = dtScDsrView.Rows(Convert.ToInt32(rowIndex))("OW_goods_total").ToString()
                    Else
                        arrayListIWOW(5) = 0
                    End If

                ElseIf (item.Item("Branch_name").ToString = "SSC4") Then
                    If dtScDsrView.Rows(Convert.ToInt32(rowIndex))("IW_goods_total").ToString().Length > 0 Then
                        arrayListIWOW(6) = dtScDsrView.Rows(Convert.ToInt32(rowIndex))("IW_goods_total").ToString
                    Else
                        arrayListIWOW(6) = 0
                    End If
                    If dtScDsrView.Rows(Convert.ToInt32(rowIndex))("OW_goods_total").ToString().Length > 0 Then
                        arrayListIWOW(7) = dtScDsrView.Rows(Convert.ToInt32(rowIndex))("OW_goods_total").ToString()
                    Else
                        arrayListIWOW(7) = 0
                    End If

                ElseIf (item.Item("Branch_name").ToString = "SSC5") Then
                    If dtScDsrView.Rows(Convert.ToInt32(rowIndex))("IW_goods_total").ToString().Length > 0 Then
                        arrayListIWOW(8) = dtScDsrView.Rows(Convert.ToInt32(rowIndex))("IW_goods_total").ToString
                    Else
                        arrayListIWOW(8) = 0
                    End If
                    If dtScDsrView.Rows(Convert.ToInt32(rowIndex))("OW_goods_total").ToString().Length > 0 Then
                        arrayListIWOW(9) = dtScDsrView.Rows(Convert.ToInt32(rowIndex))("OW_goods_total").ToString()
                    Else
                        arrayListIWOW(9) = 0
                    End If
                ElseIf (item.Item("Branch_name").ToString = "SSC6") Then
                    If dtScDsrView.Rows(Convert.ToInt32(rowIndex))("IW_goods_total").ToString().Length > 0 Then
                        arrayListIWOW(10) = dtScDsrView.Rows(Convert.ToInt32(rowIndex))("IW_goods_total").ToString
                    Else
                        arrayListIWOW(10) = 0
                    End If
                    If dtScDsrView.Rows(Convert.ToInt32(rowIndex))("OW_goods_total").ToString().Length > 0 Then
                        arrayListIWOW(11) = dtScDsrView.Rows(Convert.ToInt32(rowIndex))("OW_goods_total").ToString()
                    Else
                        arrayListIWOW(11) = 0
                    End If

                ElseIf (item.Item("Branch_name").ToString = "SSC7") Then
                    If dtScDsrView.Rows(Convert.ToInt32(rowIndex))("IW_goods_total").ToString().Length > 0 Then
                        arrayListIWOW(12) = dtScDsrView.Rows(Convert.ToInt32(rowIndex))("IW_goods_total").ToString
                    Else
                        arrayListIWOW(12) = 0
                    End If
                    If dtScDsrView.Rows(Convert.ToInt32(rowIndex))("OW_goods_total").ToString().Length > 0 Then
                        arrayListIWOW(13) = dtScDsrView.Rows(Convert.ToInt32(rowIndex))("OW_goods_total").ToString()
                    Else
                        arrayListIWOW(13) = 0
                    End If

                ElseIf (item.Item("Branch_name").ToString = "SSC8") Then
                    If dtScDsrView.Rows(Convert.ToInt32(rowIndex))("IW_goods_total").ToString().Length > 0 Then
                        arrayListIWOW(14) = dtScDsrView.Rows(Convert.ToInt32(rowIndex))("IW_goods_total").ToString
                    Else
                        arrayListIWOW(14) = 0
                    End If
                    If dtScDsrView.Rows(Convert.ToInt32(rowIndex))("OW_goods_total").ToString().Length > 0 Then
                        arrayListIWOW(15) = dtScDsrView.Rows(Convert.ToInt32(rowIndex))("OW_goods_total").ToString()
                    Else
                        arrayListIWOW(15) = 0
                    End If
                ElseIf (item.Item("Branch_name").ToString = "SSC9") Then
                    If dtScDsrView.Rows(Convert.ToInt32(rowIndex))("IW_goods_total").ToString().Length > 0 Then
                        arrayListIWOW(16) = dtScDsrView.Rows(Convert.ToInt32(rowIndex))("IW_goods_total").ToString
                    Else
                        arrayListIWOW(16) = 0
                    End If
                    If dtScDsrView.Rows(Convert.ToInt32(rowIndex))("OW_goods_total").ToString().Length > 0 Then
                        arrayListIWOW(17) = dtScDsrView.Rows(Convert.ToInt32(rowIndex))("OW_goods_total").ToString()
                    Else
                        arrayListIWOW(17) = 0
                    End If
                ElseIf (item.Item("Branch_name").ToString = "SSC10") Then
                    If dtScDsrView.Rows(Convert.ToInt32(rowIndex))("IW_goods_total").ToString().Length > 0 Then
                        arrayListIWOW(18) = dtScDsrView.Rows(Convert.ToInt32(rowIndex))("IW_goods_total").ToString
                    Else
                        arrayListIWOW(18) = 0
                    End If
                    If dtScDsrView.Rows(Convert.ToInt32(rowIndex))("OW_goods_total").ToString().Length > 0 Then
                        arrayListIWOW(19) = dtScDsrView.Rows(Convert.ToInt32(rowIndex))("OW_goods_total").ToString()
                    Else
                        arrayListIWOW(19) = 0
                    End If
                ElseIf (item.Item("Branch_name").ToString = "SSC11") Then
                    If dtScDsrView.Rows(Convert.ToInt32(rowIndex))("IW_goods_total").ToString().Length > 0 Then
                        arrayListIWOW(20) = dtScDsrView.Rows(Convert.ToInt32(rowIndex))("IW_goods_total").ToString
                    Else
                        arrayListIWOW(20) = 0
                    End If
                    If dtScDsrView.Rows(Convert.ToInt32(rowIndex))("OW_goods_total").ToString().Length > 0 Then
                        arrayListIWOW(21) = dtScDsrView.Rows(Convert.ToInt32(rowIndex))("OW_goods_total").ToString()
                    Else
                        arrayListIWOW(21) = 0
                    End If
                End If
                'arrayListIWOW(rowIWOWIndex) = dtScDsrView.Rows(Convert.ToInt32(rowIndex))("IW_goods_total").ToString
                'rowIWOWIndex = rowIWOWIndex + 1
                'arrayListIWOW(rowIWOWIndex) = dtScDsrView.Rows(Convert.ToInt32(rowIndex))("OW_goods_total").ToString


            Else
                Dim IW_goods_total As String
                Dim OW_goods_total As String
                rowIndex = item.Table.Rows.IndexOf(item).ToString
                'arrayListIWOW(rowIWOWIndex) = dtScDsrView.Rows(Convert.ToInt32(rowIndex))("IW_goods_total").ToString


                If dtScDsrView.Rows(Convert.ToInt32(rowIndex))("IW_goods_total").ToString.Length > 0 Then
                    IW_goods_total = dtScDsrView.Rows(Convert.ToInt32(rowIndex))("IW_goods_total").ToString
                Else
                    IW_goods_total = 0
                End If


                If dtScDsrView.Rows(Convert.ToInt32(rowIndex))("OW_goods_total").ToString.Length > 0 Then
                    OW_goods_total = dtScDsrView.Rows(Convert.ToInt32(rowIndex))("OW_goods_total").ToString
                Else
                    OW_goods_total = 0
                End If



                rowIWOWIndex = rowIWOWIndex + 1
                ' arrayListIWOW(rowIWOWIndex) = dtScDsrView.Rows(Convert.ToInt32(rowIndex))("OW_goods_total").ToString

                If branch = "SSC1" Then
                    sb.Append(String.Format("{0},{1},{2},{3},{4},{5},{6},{7},{8},{9},{10},{11},{12},{13},{14},{15},{16},{17},{18},{19},{20},{21}", IW_goods_total, OW_goods_total, "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0") + Environment.NewLine)
                ElseIf (branch = "SSC2") Then
                    sb.Append(String.Format("{0},{1},{2},{3},{4},{5},{6},{7},{8},{9},{10},{11},{12},{13},{14},{15},{16},{17},{18},{19},{20},{21}", "0", "0", IW_goods_total, OW_goods_total, "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0") + Environment.NewLine)
                    'note: {0},{1}=ssc1,{2}{3}=ssc2
                ElseIf (branch = "SSC3") Then
                    sb.Append(String.Format("{0},{1},{2},{3},{4},{5},{6},{7},{8},{9},{10},{11},{12},{13},{14},{15},{16},{17},{18},{19},{20},{21}", "0", "0", "0", "0", IW_goods_total, OW_goods_total, "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0") + Environment.NewLine)
                    'do  it for up to ssc8. but {} is added.ok
                ElseIf (branch = "SSC4") Then
                    sb.Append(String.Format("{0},{1},{2},{3},{4},{5},{6},{7},{8},{9},{10},{11},{12},{13},{14},{15},{16},{17},{18},{19},{20},{21}", "0", "0", "0", "0", "0", "0", IW_goods_total, OW_goods_total, "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0") + Environment.NewLine)
                ElseIf (branch = "SSC5") Then
                    sb.Append(String.Format("{0},{1},{2},{3},{4},{5},{6},{7},{8},{9},{10},{11},{12},{13},{14},{15},{16},{17},{18},{19},{20},{21}", "0", "0", "0", "0", "0", "0", "0", "0", IW_goods_total, OW_goods_total, "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0") + Environment.NewLine)
                ElseIf (branch = "SSC6") Then
                    sb.Append(String.Format("{0},{1},{2},{3},{4},{5},{6},{7},{8},{9},{10},{11},{12},{13},{14},{15},{16},{17},{18},{19},{20},{21}", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", IW_goods_total, OW_goods_total, "0", "0", "0", "0", "0", "0", "0", "0", "0", "0") + Environment.NewLine)
                ElseIf (branch = "SSC7") Then
                    sb.Append(String.Format("{0},{1},{2},{3},{4},{5},{6},{7},{8},{9},{10},{11},{12},{13},{14},{15},{16},{17},{18},{19},{20},{21}", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", IW_goods_total, OW_goods_total, "0", "0", "0", "0", "0", "0", "0", "0") + Environment.NewLine)
                ElseIf (branch = "SSC8") Then
                    sb.Append(String.Format("{0},{1},{2},{3},{4},{5},{6},{7},{8},{9},{10},{11},{12},{13},{14},{15},{16},{17},{18},{19},{20},{21}", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", IW_goods_total, OW_goods_total, "0", "0", "0", "0", "0", "0") + Environment.NewLine)
                ElseIf (branch = "SSC9") Then
                    sb.Append(String.Format("{0},{1},{2},{3},{4},{5},{6},{7},{8},{9},{10},{11},{12},{13},{14},{15},{16},{17},{18},{19},{20},{21}", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", IW_goods_total, OW_goods_total, "0", "0", "0", "0") + Environment.NewLine)
                ElseIf (branch = "SSC10") Then
                    sb.Append(String.Format("{0},{1},{2},{3},{4},{5},{6},{7},{8},{9},{10},{11},{12},{13},{14},{15},{16},{17},{18},{19},{20},{21}", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", IW_goods_total, OW_goods_total, "0", "0") + Environment.NewLine)
                ElseIf (branch = "SSC11") Then
                    sb.Append(String.Format("{0},{1},{2},{3},{4},{5},{6},{7},{8},{9},{10},{11},{12},{13},{14},{15},{16},{17},{18},{19},{20},{21}", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", IW_goods_total, OW_goods_total) + Environment.NewLine)
                End If


            End If
            'incValue = incValue + 1
            'rowIWOWIndex = rowIWOWIndex + 1
        Next

        If targetType = "ALL" Then
            sb.Append(String.Format("{0},{1},{2},{3},{4},{5},{6},{7},{8},{9},{10},{11},{12},{13},{14},{15},{16},{17},{18},{19},{20},{21}", arrayListIWOW(0), arrayListIWOW(1), arrayListIWOW(2), arrayListIWOW(3), arrayListIWOW(4), arrayListIWOW(5), arrayListIWOW(6), arrayListIWOW(7), arrayListIWOW(8), arrayListIWOW(9), arrayListIWOW(10), arrayListIWOW(11), arrayListIWOW(12), arrayListIWOW(13), arrayListIWOW(14), arrayListIWOW(15), arrayListIWOW(16), arrayListIWOW(17), arrayListIWOW(18), arrayListIWOW(19), arrayListIWOW(20), arrayListIWOW(21)) + Environment.NewLine)
        End If

        'for TotalGoods
        For Each item As DataRow In dtScDsrView.Rows

            If targetType = "ALL" Then

                'If (item.Item("Branch_name").ToString = "SSC" + incTotal.ToString) Then
                rowIndex = item.Table.Rows.IndexOf(item).ToString
                '    rowTotalIndex = rowTotalIndex + 1
                '    arrayListTotal(rowTotalIndex) = dtScDsrView.Rows(Convert.ToInt32(rowIndex))("TotalGoods").ToString
                'End If
                If item.Item("Branch_name").ToString = "SSC1" Then
                    If dtScDsrView.Rows(Convert.ToInt32(rowIndex))("TotalGoods").ToString.Length > 0 Then
                        arrayListTotal(1) = dtScDsrView.Rows(Convert.ToInt32(rowIndex))("TotalGoods").ToString
                    Else
                        arrayListTotal(1) = 0
                    End If


                ElseIf (item.Item("Branch_name").ToString = "SSC2") Then
                    If dtScDsrView.Rows(Convert.ToInt32(rowIndex))("TotalGoods").ToString.Length > 0 Then
                        arrayListTotal(3) = dtScDsrView.Rows(Convert.ToInt32(rowIndex))("TotalGoods").ToString
                    Else
                        arrayListTotal(3) = 0
                    End If

                ElseIf (item.Item("Branch_name").ToString = "SSC3") Then
                    If dtScDsrView.Rows(Convert.ToInt32(rowIndex))("TotalGoods").ToString.Length > 0 Then
                        arrayListTotal(5) = dtScDsrView.Rows(Convert.ToInt32(rowIndex))("TotalGoods").ToString
                    Else
                        arrayListTotal(5) = 0
                    End If
                ElseIf (item.Item("Branch_name").ToString = "SSC4") Then
                    If dtScDsrView.Rows(Convert.ToInt32(rowIndex))("TotalGoods").ToString.Length > 0 Then
                        arrayListTotal(7) = dtScDsrView.Rows(Convert.ToInt32(rowIndex))("TotalGoods").ToString
                    Else
                        arrayListTotal(7) = 0
                    End If
                ElseIf (item.Item("Branch_name").ToString = "SSC5") Then
                    If dtScDsrView.Rows(Convert.ToInt32(rowIndex))("TotalGoods").ToString.Length > 0 Then
                        arrayListTotal(9) = dtScDsrView.Rows(Convert.ToInt32(rowIndex))("TotalGoods").ToString
                    Else
                        arrayListTotal(9) = 0
                    End If
                ElseIf (item.Item("Branch_name").ToString = "SSC6") Then
                    If dtScDsrView.Rows(Convert.ToInt32(rowIndex))("TotalGoods").ToString.Length > 0 Then
                        arrayListTotal(11) = dtScDsrView.Rows(Convert.ToInt32(rowIndex))("TotalGoods").ToString
                    Else
                        arrayListTotal(11) = 0
                    End If
                ElseIf (item.Item("Branch_name").ToString = "SSC7") Then
                    If dtScDsrView.Rows(Convert.ToInt32(rowIndex))("TotalGoods").ToString.Length > 0 Then
                        arrayListTotal(13) = dtScDsrView.Rows(Convert.ToInt32(rowIndex))("TotalGoods").ToString
                    Else
                        arrayListTotal(13) = 0
                    End If
                ElseIf (item.Item("Branch_name").ToString = "SSC8") Then
                    If dtScDsrView.Rows(Convert.ToInt32(rowIndex))("TotalGoods").ToString.Length > 0 Then
                        arrayListTotal(15) = dtScDsrView.Rows(Convert.ToInt32(rowIndex))("TotalGoods").ToString
                    Else
                        arrayListTotal(15) = 0
                    End If
                ElseIf (item.Item("Branch_name").ToString = "SSC9") Then
                    If dtScDsrView.Rows(Convert.ToInt32(rowIndex))("TotalGoods").ToString.Length > 0 Then
                        arrayListTotal(17) = dtScDsrView.Rows(Convert.ToInt32(rowIndex))("TotalGoods").ToString
                    Else
                        arrayListTotal(17) = 0
                    End If
                ElseIf (item.Item("Branch_name").ToString = "SSC10") Then
                    If dtScDsrView.Rows(Convert.ToInt32(rowIndex))("TotalGoods").ToString.Length > 0 Then
                        arrayListTotal(19) = dtScDsrView.Rows(Convert.ToInt32(rowIndex))("TotalGoods").ToString
                    Else
                        arrayListTotal(19) = 0
                    End If
                ElseIf (item.Item("Branch_name").ToString = "SSC11") Then
                    If dtScDsrView.Rows(Convert.ToInt32(rowIndex))("TotalGoods").ToString.Length > 0 Then
                        arrayListTotal(21) = dtScDsrView.Rows(Convert.ToInt32(rowIndex))("TotalGoods").ToString
                    Else
                        arrayListTotal(21) = 0
                    End If
                End If

            Else

                rowIndex = item.Table.Rows.IndexOf(item).ToString
                'rowTotalIndex = rowTotalIndex + 1
                'arrayListTotal(rowTotalIndex) = dtScDsrView.Rows(Convert.ToInt32(rowIndex))("TotalGoods").ToString

                Dim Total As String
                If dtScDsrView.Rows(Convert.ToInt32(rowIndex))("TotalGoods").ToString.Length > 0 Then
                    Total = dtScDsrView.Rows(Convert.ToInt32(rowIndex))("TotalGoods").ToString()
                Else
                    Total = "0"
                End If

                If branch = "SSC1" Then
                    sb.Append(String.Format("{0},{1}", "total", Total, "total", "0", "total", "0", "total", "0", "total", "0", "total", "0", "total", "0", "total", "0", "total", "0") + Environment.NewLine)
                    'note:{0}{1}=ssc
                ElseIf (branch = "SSC2") Then
                    sb.Append(String.Format("{0},{1},{2},{3},{4},{5},{6},{7},{8},{9},{10},{11},{12},{13},{14},{15},{16},{17},{18},{19},{20},{21}", "total", "0", "total", Total, "total", "0", "total", "0", "total", "0", "total", "0", "total", "0", "total", "0", "total", "0", "total", "0", "total", "0") + Environment.NewLine)
                ElseIf (branch = "SSC3") Then
                    sb.Append(String.Format("{0},{1},{2},{3},{4},{5},{6},{7},{8},{9},{10},{11},{12},{13},{14},{15},{16},{17},{18},{19},{20},{21}", "total", "0", "total", "0", "total", Total, "total", "0", "total", "0", "total", "0", "total", "0", "total", "0", "total", "0", "total", "0", "total", "0") + Environment.NewLine)
                ElseIf (branch = "SSC4") Then
                    sb.Append(String.Format("{0},{1},{2},{3},{4},{5},{6},{7},{8},{9},{10},{11},{12},{13},{14},{15},{16},{17},{18},{19},{20},{21}", "total", "0", "total", "0", "total", "0", "total", Total, "total", "0", "total", "0", "total", "0", "total", "0", "total", "0", "total", "0", "total", "0") + Environment.NewLine)
                ElseIf (branch = "SSC5") Then
                    sb.Append(String.Format("{0},{1},{2},{3},{4},{5},{6},{7},{8},{9},{10},{11},{12},{13},{14},{15},{16},{17},{18},{19},{20},{21}", "total", "0", "total", "0", "total", "0", "total", "0", "total", Total, "total", "0", "total", "0", "total", "0", "total", "0", "total", "0", "total", "0") + Environment.NewLine)
                ElseIf (branch = "SSC6") Then
                    sb.Append(String.Format("{0},{1},{2},{3},{4},{5},{6},{7},{8},{9},{10},{11},{12},{13},{14},{15},{16},{17},{18},{19},{20},{21}", "total", "0", "total", "0", "total", "0", "total", "0", "total", "0", "total", Total, "total", "0", "total", "0", "total", "0", "total", "0", "total", "0") + Environment.NewLine)
                ElseIf (branch = "SSC7") Then
                    sb.Append(String.Format("{0},{1},{2},{3},{4},{5},{6},{7},{8},{9},{10},{11},{12},{13},{14},{15},{16},{17},{18},{19},{20},{21}", "total", "0", "total", "0", "total", "0", "total", "0", "total", "0", "total", "0", "total", Total, "total", "0", "total", "0", "total", "0", "total", "0") + Environment.NewLine)
                ElseIf (branch = "SSC8") Then
                    sb.Append(String.Format("{0},{1},{2},{3},{4},{5},{6},{7},{8},{9},{10},{11},{12},{13},{14},{15},{16},{17},{18},{19},{20},{21}", "total", "0", "total", "0", "total", "0", "total", "0", "total", "0", "total", "0", "total", "0", "total", Total, "total", "0", "total", "0", "total", "0") + Environment.NewLine)
                ElseIf (branch = "SSC9") Then
                    sb.Append(String.Format("{0},{1},{2},{3},{4},{5},{6},{7},{8},{9},{10},{11},{12},{13},{14},{15},{16},{17},{18},{19},{20},{21}", "total", "0", "total", "0", "total", "0", "total", "0", "total", "0", "total", "0", "total", "0", "total", "0", "total", Total, "total", "0", "total", "0") + Environment.NewLine)
                ElseIf (branch = "SSC10") Then
                    sb.Append(String.Format("{0},{1},{2},{3},{4},{5},{6},{7},{8},{9},{10},{11},{12},{13},{14},{15},{16},{17},{18},{19},{20},{21}", "total", "0", "total", "0", "total", "0", "total", "0", "total", "0", "total", "0", "total", "0", "total", "0", "total", "0", "total", Total, "total", "0") + Environment.NewLine)
                ElseIf (branch = "SSC11") Then
                    sb.Append(String.Format("{0},{1},{2},{3},{4},{5},{6},{7},{8},{9},{10},{11},{12},{13},{14},{15},{16},{17},{18},{19},{20},{21}", "total", "0", "total", "0", "total", "0", "total", "0", "total", "0", "total", "0", "total", "0", "total", "0", "total", "0", "total", "0", "total", Total) + Environment.NewLine)
                End If

            End If
            'incTotal = incTotal + 1
            ' rowTotalIndex = rowTotalIndex + 1
        Next
        If targetType = "ALL" Then
            sb.Append(String.Format("{0},{1},{2},{3},{4},{5},{6},{7},{8},{9},{10},{11},{12},{13},{14},{15},{16},{17},{18},{19},{20},{21}", arrayListTotal(0).ToString, arrayListTotal(1).ToString, arrayListTotal(2).ToString, arrayListTotal(3).ToString, arrayListTotal(4).ToString, arrayListTotal(5).ToString, arrayListTotal(6).ToString, arrayListTotal(7).ToString, arrayListTotal(8).ToString, arrayListTotal(9).ToString, arrayListTotal(10).ToString, arrayListTotal(11).ToString, arrayListTotal(12).ToString, arrayListTotal(13).ToString, arrayListTotal(14).ToString, arrayListTotal(15).ToString, arrayListTotal(16).ToString, arrayListTotal(17).ToString, arrayListTotal(18).ToString, arrayListTotal(19).ToString, arrayListTotal(20).ToString, arrayListTotal(21).ToString) + Environment.NewLine)
        End If
        'Revenue Without Tax

        sb.Append(String.Format("{0},{1},{2},{3},{4},{5}", "", "", "", "", "", "") + Environment.NewLine)
        sb.Append(String.Format("{0},{1},{2},{3},{4},{5}", "", "", "", "", "", "") + Environment.NewLine)
        sb.Append(String.Format("{0},{1},{2},{3},{4},{5}", "Revenue Without Tax", "", "", "", "", "") + Environment.NewLine)
        sb.Append(String.Format("{0},{1},{2},{3},{4},{5},{6},{7},{8},{9},{10}", "SSC1", "SSC2", "SSC3", "SSC4", "SSC5", "SSC6", "SSC7", "SSC8", "SSC9", "SSC10", "SSC11") + Environment.NewLine)
        sb.Append(String.Format("{0},{1},{2},{3},{4},{5},{6},{7},{8},{9},{10}", arrayListRWT(0), arrayListRWT(1), arrayListRWT(2), arrayListRWT(3), arrayListRWT(4), arrayListRWT(5), arrayListRWT(6), arrayListRWT(7), arrayListRWT(8), arrayListRWT(9), arrayListRWT(10)) + Environment.NewLine)
        sb.Append(String.Format("{0},{1},{2},{3},{4},{5}", "Total", TotalRWT, "", "", "", "") + Environment.NewLine)
        'Revenue Without Tax
        'export to csv
        Dim bytes As Byte() = Encoding.ASCII.GetBytes(sb.ToString)
        If bytes IsNot Nothing Then
            Response.ClearHeaders()
            Response.Buffer = True
            Response.ContentType = "application/text/csv"
            Response.AddHeader("Content-Length", bytes.Length.ToString())
            'Response.AddHeader("Content-disposition", "attachment; filename=""sample.csv" & """")
            'Response.AddHeader("Content-disposition", "attachment; filename=""sample.csv")


            'SSC1_DRS_COUNT_20191207_20200122
            'SSC1-SS8_DRS_COUNT_20191207_20200122
            Dim csvfileName As String
            If targetType = "ALL" Then
                csvfileName = "SSC1_SS11_DRS_COUNT_" & fromdate & "_" & todate & ".csv"
            Else
                csvfileName = branch & "_" & "DRS_COUNT_" & fromdate & "_" & todate & ".csv"
            End If

            Response.AddHeader("Content-disposition", "attachment; filename=" & csvfileName)
            Response.BinaryWrite(bytes)
            Response.Flush()
            HttpContext.Current.ApplicationInstance.CompleteRequest()
            Exit Sub
        Else
            Call showMsg("Unable to export...", "")
            Exit Sub
        End If
    End Sub





    Protected Sub CheckBox1_CheckedChanged(sender As Object, e As EventArgs)
        If CheckBox1.Checked Then
            TextFromDate.Text = DateTime.Now.AddDays(-1).ToString("MM/dd/yyyy", CultureInfo.InvariantCulture)
            TextToDate.ReadOnly = True
            TextToDate.Text = ""
        Else
            TextFromDate.Text = ""
            TextToDate.ReadOnly = False

        End If
    End Sub

    Protected Sub ImageButton1_Click(sender As Object, e As EventArgs) Handles ImageButton1.Click
        If DropdownList5.SelectedItem.Text = "Select" Then
            Call showMsg("Please Select SSC", "")
            Exit Sub
        ElseIf txtPaymentAmount.Text = "" Then
            Call showMsg("Please Enter The Payment Amount", "")
            Exit Sub
        ElseIf txtTargetDate.Text = "" Then
            Call showMsg("Please Select The Date", "")
            Exit Sub
        End If

        Dim _PaymentVlaueModel As PaymentValueModel = New PaymentValueModel()
        Dim _PaymentVlaueControl As PaymentValueControl = New PaymentValueControl()

        _PaymentVlaueModel.UPDPG = "Class_Store.vb"
        _PaymentVlaueModel.SHIP_TO_BRANCH_CODE = DropdownList5.SelectedItem.Value
        _PaymentVlaueModel.SHIP_TO_BRANCH = DropdownList5.SelectedItem.Text
        _PaymentVlaueModel.VALUE = txtPaymentAmount.Text
        _PaymentVlaueModel.TARGET_DATE = txtTargetDate.Text
        _PaymentVlaueModel.UserId = Session("user_id").ToString

        Dim isInserted As Boolean = _PaymentVlaueControl.PaymentValueInsert(_PaymentVlaueModel)

        If (isInserted = True) Then
            Call showMsg("Success" & "<br/> " & txtTargetDate.Text & ", " & DropdownList5.SelectedItem.Text & "<br/> " & ", " & "Payment Value " & txtPaymentAmount.Text, "")
            'Exit Sub

        End If
        txtPaymentAmount.Text = ""
        txtTargetDate.Text = ""
        Dim dtDisplayPaymentValue As DataTable = _PaymentVlaueControl.ShowPaymentValueGrid()
        'dtDisplayPaymentValue.DefaultView.Sort = "Id ASC"
        dtDisplayPaymentValue = dtDisplayPaymentValue.DefaultView.ToTable()
        If Not dtDisplayPaymentValue Is Nothing Then
            If dtDisplayPaymentValue.Rows.Count > 0 Then
                txtPageSize.Visible = True
                lblPagesize.Visible = True
                txtPageSize.Text = 10
                gvDisplayPaymentValue.PageSize = Convert.ToInt16(txtPageSize.Text)
                gvDisplayPaymentValue.Visible = True
                gvDisplayPaymentValue.AllowSorting = True
                gvDisplayPaymentValue.PageIndex = 0
                gvDisplayPaymentValue.DataSource = dtDisplayPaymentValue
                gvDisplayPaymentValue.DataBind()
                'btnExport.Visible = True
                lbltotal.Text = "Total No of Records : " & dtDisplayPaymentValue.Rows.Count
                'lblTitle.Text = drpTaskExport.SelectedItem.Text
            Else
                gvDisplayPaymentValue.AllowSorting = False
                gvDisplayPaymentValue.DataBind()
                gvDisplayPaymentValue.Visible = False
                'btnExport.Visible = False
                txtPageSize.Visible = False
                lblPagesize.Visible = False
            End If
        End If

    End Sub


    'PO_Collection
    Protected Sub ImageButton5_Click(sender As Object, e As EventArgs) Handles ImageButton5.Click
        If DropdownList6.SelectedItem.Text = "Select" Then
            Call showMsg("Please Select SSC", "")
            Exit Sub
        ElseIf TextBoxDeposit.Text = "" Then
            Call showMsg("Please Enter The Daily Deposit", "")
            Exit Sub
        ElseIf TextBoxCreditSales.Text = "" Then
            Call showMsg("Please Enter The Credit Sales", "")
            Exit Sub
        ElseIf TextBoxTargetDate.Text = "" Then
            Call showMsg("Please Select The Date", "")
            Exit Sub
        End If

        If System.Text.RegularExpressions.Regex.IsMatch(TextBoxDeposit.Text, "[^0-9]") Then
            Call showMsg("Please Enter The Number for Deposit", "")
            TextBoxDeposit.Text = ""
            Exit Sub
        End If


        If TextBoxDeposit.Text.Length > 6 Then
            Call showMsg("Enter upto 6 Numbers for Deposit", "")
            TextBoxDeposit.Text = ""
            Exit Sub
        End If

        If System.Text.RegularExpressions.Regex.IsMatch(TextBoxCreditSales.Text, "[^0-9]") Then
            Call showMsg("Please Enter The Number for Credit Sales", "")
            TextBoxCreditSales.Text = ""
            Exit Sub
        End If

        If TextBoxCreditSales.Text.Length > 6 Then
            Call showMsg("Enter upto 6 Numbers for Credit Sales", "")
            TextBoxCreditSales.Text = ""
            Exit Sub
        End If


        Dim _CollectionModel As CollectionModel = New CollectionModel()
        Dim _CollectionControl As CollectionControl = New CollectionControl()

        _CollectionModel.UPDPG = "Class_Store.vb"
        _CollectionModel.SHIP_TO_BRANCH_CODE = DropdownList6.SelectedItem.Value
        _CollectionModel.SHIP_TO_BRANCH = DropdownList6.SelectedItem.Text
        _CollectionModel.DEPOSIT = TextBoxDeposit.Text
        _CollectionModel.SALES = TextBoxCreditSales.Text
        _CollectionModel.TARGET_DATE = TextBoxTargetDate.Text
        _CollectionModel.UserId = Session("user_id").ToString

        Dim isInserted As Boolean = _CollectionControl.CollectionInsert(_CollectionModel)

        If (isInserted = True) Then
            Call showMsg("Success" & "<br/> " & TextBoxTargetDate.Text & ", " & DropdownList6.SelectedItem.Text & "<br/> " & ", " & "Daily Deposit " & TextBoxDeposit.Text & "," & "Credit Sales " & TextBoxCreditSales.Text, "")
            'Exit Sub

        End If
        TextBoxDeposit.Text = ""
        TextBoxCreditSales.Text = ""
        TextBoxTargetDate.Text = ""
    End Sub

    Protected Sub TextBoxDeposit_TextChanged(sender As Object, e As EventArgs) Handles TextBoxDeposit.TextChanged


    End Sub
End Class
