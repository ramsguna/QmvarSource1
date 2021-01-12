Imports System
Imports System.Web
Imports System.Data.SqlClient
Imports System.Collections.Generic
Imports System.Configuration
Imports Ganges33.Ganges33.logic
Imports Ganges33.Ganges33.model

''' <summary>
''' Login Page 
''' </summary>
Public Class Login_3tire
    Inherits System.Web.UI.Page
    ''' <summary>
    ''' Page loading
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        '初期処理
        If IsPostBack = False Then
            'セッション情報クリア
            Session.Clear()
        End If
    End Sub
    ''' <summary>
    ''' 送信ボタン押下処理
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Protected Sub BtnSubmit_Click(sender As Object, e As ImageClickEventArgs) Handles BtnSubmit.Click
        'Write Log
        Log4NetControl.ComInfoLogWrite(TextId.ToString().Trim())
        'Verify username and password entered by user
        If (TextId.Text().Trim() = "") Or (TextPass.Text.Trim() = "") Then
            Call showMsg("Please Enter ID and Password.")
            Exit Sub
        End If
        'Clear the branch location
        DropListLocation.Items.Clear()
        'For store the branch codes in array
        Dim shipCodeAll() As String
        'Verify entered user and password correct or not with the database
        Dim _UserInfoModel As UserInfoModel = New UserInfoModel()
        Dim _UserInfoControl As UserInfoControl = New UserInfoControl()
        _UserInfoModel.UserId = TextId.Text.Trim()
        _UserInfoModel.Password = TextPass.Text.Trim()
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
        'Loading branch name and code in the dropdown list
        Dim codeMasterList As List(Of CodeMasterModel) = codeMasterControl.SelectBranchMaster(QryFlag, "'" & UserInfoList(0).Ship1.Replace(",", "','") & "'")
        Me.DropListLocation.DataSource = codeMasterList
        Me.DropListLocation.DataTextField = "CodeDispValue"
        Me.DropListLocation.DataValueField = "CodeValue"
        Me.DropListLocation.DataBind()
        'Split the branch names from the dropdown list and it will assign to the sessions
        Dim BranchName As List(Of String) = New List(Of String)()
        For Each item As ListItem In DropListLocation.Items
            BranchName.Add(item.Text)
        Next
        'Storing session values
        Session("UserName") = TextId.Text.Trim()
        Log4NetControl.UserID = TextId.Text.Trim()
        Session("user_id") = TextId.Text.Trim()
        Session("user_level") = UserInfoList(0).UserLevel
        Session("admin_Flg") = UserInfoList(0).AdminFlg
        Session("shipCode_All") = shipCodeAll
        Session("ship_name_list") = BranchName

    End Sub


    ''' <summary>
    ''' ログインボタン押下処理
    ''' </summary>SelectBranchCode    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Protected Sub BtnLogin_Click(sender As Object, e As ImageClickEventArgs) Handles BtnLogin.Click
        '入力チェック(初回ログイン：アカウント)
        If (Session("user_id") Is Nothing) Or (Trim(TextId.Text) = "") Then
            Call showMsg("Please Enter ID and Password.")
            Exit Sub
        End If
        'ドロップリストより選択された拠点名称
        Dim shipName As String = DropListLocation.SelectedItem.Text.ToString()
        If shipName.Length = 0 Then
            Call showMsg("Please select the branch and then try again.")
            Exit Sub
        End If
        Session("ship_Name") = shipName

        'To get ship mark
        Dim shipMark As String = ""
        Dim _ShipBaseModel As ShipBaseModel = New ShipBaseModel()
        Dim _ShipBaseControl As ShipBaseControl = New ShipBaseControl()
        shipMark = _ShipBaseControl.SelectShipMark(_ShipBaseModel)
        Session("ship_mark") = shipMark

        'セッション情報取得
        Dim UserLevel As String = Session("user_level")
        Dim AdminFlg As Boolean = Session("admin_Flg")
        If UserLevel = "9" Then
            Response.Redirect("Analysis_Report.aspx")
        ElseIf (UserLevel = CommonConst.UserLevel0) Or
                     (UserLevel = CommonConst.UserLevel1) Or
                     (UserLevel = CommonConst.UserLevel2) Or
             (AdminFlg = True) Then
            Response.Redirect("Menu.aspx")
        Else
            Response.Redirect("Menu2.aspx")
        End If
    End Sub
    ''' <summary>
    ''' Show message to the user
    ''' </summary>
    ''' <param name="Msg"></param>
    Protected Sub showMsg(ByVal Msg As String)
        lblMsg.Text = Msg
        Dim sScript As String = "$(function () { $( ""#dialog"" ).dialog({width: 400, buttons: {""OK"": function () {$(this).dialog('close');}}});});"
        'JavaScriptの埋め込み
        ClientScript.RegisterStartupScript(Me.GetType(), "startup", sScript, True)
    End Sub


End Class