Public Class Analysis_Custody_Search
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        '***初期処理***
        If IsPostBack = False Then

            '***セッションなしログインユーザの対応***
            Dim userid As String = Session("user_id")
            If userid = "" Then
                Response.Redirect("Login.aspx")
            End If

            '***コントロール***
            btnTakeOut.Enabled = False

            '***GRIDVIEW/totalInfoへ反映 ***
            Call showGridData()

        End If

    End Sub
    Protected Sub showGridData()

        '***セッション取得***
        Dim shipCode As String = Session("ship_code")

        Dim searchNo As String = Session("search_No")
        Dim searchName As String = Session("search_Name")
        Dim searchtel As String = Session("search_tel")
        Dim falseChk As Boolean = Session("false_Chk")

        '***GRIDVIEWへ反映 ***
        'searchkeyの設定
        Dim searchKey As String

        If searchNo <> "" Then
            searchKey = "AND keep_no LIKE '%" & searchNo & "%' "
        End If

        If searchName <> "" Then
            searchKey = "AND customer_name LIKE '%" & searchName & "%' "
        End If

        If searchtel <> "" Then
            searchKey = "AND customer_tel LIKE '%" & searchtel & "%' "
        End If

        Dim strSQL As String
        Dim errFlg As Integer

        If falseChk = True Then
            strSQL = "SELECT keep_no AS 'keep_no.', customer_name AS Name, customer_tel AS Tel, samsung_claim_no AS 'Claim No.', product_name AS Product, CONVERT(decimal(13, 2), cash) AS Cash, "
            strSQL &= "CASE WHEN takeout = 0 THEN CONVERT(VARCHAR, UPDDT, 111) + ' out' "
            strSQL &= "WHEN takeout = 1 THEN CONVERT(VARCHAR, CRTDT, 111) + ' in' END AS 'in/out' "
            strSQL &= "FROM dbo.custody WHERE DELFG = 0 AND ship_code = '" & shipCode & "' "
            strSQL &= searchKey
            strSQL &= "ORDER BY CRTCD;"
        Else
            strSQL = "SELECT keep_no AS 'keep_no.', customer_name AS Name, customer_tel AS Tel, samsung_claim_no AS 'Claim No.', product_name AS Product, CONVERT(decimal(13, 2), cash) AS Cash "
            strSQL &= "FROM dbo.custody WHERE DELFG = 0 AND ship_code = '" & shipCode & "' "
            strSQL &= searchKey
            strSQL &= "ORDER BY CRTCD;"
        End If

        If strSQL <> "" Then

            Dim DT_SERCH As DataTable = DBCommon.ExecuteGetDT(strSQL, errFlg)

            If errFlg = 1 Then
                Call showMsg("Failed to acquire search information.", "")
                Exit Sub
            End If

            If DT_SERCH IsNot Nothing Then

                GridInfo.DataSource = DT_SERCH
                GridInfo.DataBind()

                If falseChk = True Then
                    GridInfo.Columns(0).Visible = True
                Else
                    GridInfo.Columns(0).Visible = False
                End If

                '次ページ反映ように取得しておく
                Session("Data_DT_SERCH") = DT_SERCH
            Else

                GridInfo.DataSource = Nothing
                GridInfo.DataBind()

            End If

        End If

        '***total情報を反映***
        Dim strSQL2 As String = "SELECT SUM(cash) AS cash_SUM, COUNT(*) AS COUNT "
        strSQL2 &= "FROM dbo.custody WHERE DELFG = 0 AND ship_code = '" & shipCode & "' AND takeout = 1 "
        strSQL2 &= searchKey

        Dim DT_custody As DataTable = DBCommon.ExecuteGetDT(strSQL2, errFlg)

        If errFlg = 1 Then
            Call showMsg("Failed to acquire total cash, total number than custody.", "")
            Exit Sub
        End If

        Dim TotalCash As Decimal
        Dim TotalNumber As Integer

        If DT_custody IsNot Nothing Then

            If DT_custody.Rows(0)("cash_SUM") IsNot DBNull.Value Then
                TotalCash = DT_custody.Rows(0)("cash_SUM")
            End If

            If DT_custody.Rows(0)("COUNT") IsNot DBNull.Value Then
                TotalNumber = DT_custody.Rows(0)("COUNT")
            End If

        End If

        Dim clsSetMoney As New Class_money

        lblTotalCash.Text = clsSetMoney.setINR(TotalCash.ToString) & "INR"
        lblTotalNumber.Text = TotalNumber.ToString

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

    Private Sub GridInfo_PageIndexChanging(sender As Object, e As GridViewPageEventArgs) Handles GridInfo.PageIndexChanging

        GridInfo.PageIndex = e.NewPageIndex
        GridInfo.DataSource = Session("Data_DT_SERCH")
        GridInfo.DataBind()

        '画面が遷移するのを防ぐ。。。nodisplay and readonly
        TextBox1.Focus()

    End Sub

    Private Sub GridInfo_RowCommand(sender As Object, e As GridViewCommandEventArgs) Handles GridInfo.RowCommand

        Dim falseChk As Boolean = Session("false_Chk")
        Call reSet()

        'コマンド名が“TakeOut”の場合にのみ処理
        If e.CommandName = "TakeOut" Then

            'クリック行のインデックス取得
            Dim Index As Integer = Convert.ToInt32(e.CommandArgument)

            '該当行取得
            '※1行目は0が入る。
            Dim gridRow As GridViewRow = GridInfo.Rows(Index)

            '項目情報取得
            Dim keepNo As Integer = gridRow.Cells(1).Text
            Dim customerName As String = gridRow.Cells(2).Text
            Dim customerTel As String = gridRow.Cells(3).Text
            Dim samsungClaim_no As String = gridRow.Cells(4).Text
            Dim productName As String = gridRow.Cells(5).Text
            Dim cash As Decimal = gridRow.Cells(6).Text

            Dim takeOutStatus As String

            If falseChk = True Then
                takeOutStatus = gridRow.Cells(7).Text
            End If

            'takeout未済のみ、右上画面に情報を表示
            If takeOutStatus <> "" Then

                If Right(takeOutStatus, 3) <> "out" Then

                    lblKeepNo.Text = keepNo.ToString
                    lblCustomerName.Text = customerName
                    lblCustomerTelNO.Text = customerTel
                    lblSamsungClaimNo.Text = samsungClaim_no
                    lblProductName.Text = productName
                    lblCash.Text = cash

                    Session("keep_No") = keepNo

                    btnTakeOut.Enabled = True

                Else
                    Call showMsg("take out is complete.", "")
                    Exit Sub
                End If

            End If

        End If

    End Sub
    'takeout処理
    Protected Sub btnTakeOut_Click(sender As Object, e As ImageClickEventArgs) Handles btnTakeOut.Click

        '***セッション取得***
        Dim shipCode As String = Session("ship_code")
        Dim userid As String = Session("user_id")

        If shipCode Is Nothing Then
            Call showMsg("The session has expired. Please login again.", "")
            Exit Sub
        End If

        Dim keepNo As Integer = Session("keep_No")

        Dim clsSet As New Class_analysis
        Dim errFlg As Integer
        Dim tourokuFlg As Integer

        'takeout処理
        Call clsSet.delSet_custody(keepNo, shipCode, userid, errFlg, tourokuFlg)

        If errFlg = 1 Then
            Call showMsg("Takeout processing failed.", "")
            Exit Sub
        Else
            If tourokuFlg = 1 Then
                Call showMsg("It has already taken out.", "")
            Else
                Call showMsg("Takeout processing is complete.", "")
                Call showGridData()
            End If
        End If

    End Sub

    Protected Sub reSet()

        lblKeepNo.Text = ""
        lblCustomerName.Text = ""
        lblCustomerTelNO.Text = ""
        lblSamsungClaimNo.Text = ""
        lblProductName.Text = ""
        lblCash.Text = ""

    End Sub

    Protected Sub btnBack_Click(sender As Object, e As ImageClickEventArgs) Handles btnBack.Click

        Response.Redirect("Analysis_Custody.aspx")

    End Sub
End Class

