
Public Class Analysis_Custody_qg
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        '***初期処理***
        If IsPostBack = False Then

            '***セッションなしログインユーザの対応***
            Dim userid As String = Session("user_id")
            If userid = "" Then
                Response.Redirect("Login.aspx")
            End If

            Dim userLevel As String = Session("user_level")
            Dim adminFlg As Boolean = Session("admin_Flg")

            If userLevel = "0" Or userLevel = "1" Or userLevel = "2" Or userLevel = "3" Or adminFlg = True Then
                CheckFalse.Enabled = True
            Else
                CheckFalse.Enabled = False
            End If

        End If

    End Sub

    Protected Sub btnStart_Click(sender As Object, e As ImageClickEventArgs) Handles btnStart.Click

        '***入力チェック***
        Dim searchNo As String = Trim(TextSearchNo.Text)  'keep_no
        Dim searchName As String = Trim(TextSearchName.Text)
        Dim searchtel As String = Trim(TextSearchtel.Text)

        If searchNo = "" And searchName = "" And searchtel = "" Then
            Call showMsg("Please enter your search content.", "")
            Exit Sub
        End If

        '***FALSEチェック***
        Dim falseChk As Boolean
        If CheckFalse.Checked = True Then
            falseChk = True
        Else
            falseChk = False
        End If

        '***セッション設定***
        Session("search_No") = searchNo
        Session("search_Name") = searchName
        Session("search_tel") = searchtel
        Session("false_Chk") = falseChk

        Response.Redirect("Analysis_Custody_Search.aspx")

    End Sub

    Protected Sub btnSend_Click(sender As Object, e As ImageClickEventArgs) Handles btnSend.Click

        '***セッション取得***
        Dim userid As String = Session("user_id")

        If userid = "" Then
            Call showMsg("The session has expired. Please login again.", "")
            Exit Sub
        End If

        Dim shipCode As String = Session("ship_code")
        Dim userName As String = Session("user_Name")

        '***リセット***
        Call reSet()

        '***入力チェック/設定***
        Dim custodyData As Class_analysis.CUSTODY
        Dim intChk As Integer

        If TextCustomerName.Text = "" Then
            Call showMsg("Please enter customer information.", "")
            TextCustomerName.BackColor = System.Drawing.Color.FromArgb(255, 0, 0)
            Exit Sub
        Else
            custodyData.customer_name = Trim(TextCustomerName.Text)
        End If

        If TextCustomerTelNo.Text = "" Then
            Call showMsg("Please enter phone number.", "")
            TextCustomerTelNo.BackColor = System.Drawing.Color.FromArgb(255, 0, 0)
            Exit Sub
        Else
            custodyData.customer_tel = Trim(TextCustomerTelNo.Text)
        End If

        'cash欄の数値チェック
        If Trim(TextCash.Text) = "" Then
            custodyData.cash = 0.00
        Else
            If Integer.TryParse(Trim(TextCash.Text), intChk) = False Then
                TextCash.BackColor = System.Drawing.Color.FromArgb(255, 0, 0)
                Call showMsg("Please enter the amount.", "")
                Exit Sub
            End If
            custodyData.cash = Convert.ToInt32(Trim(TextCash.Text))
        End If

        custodyData.samsung_claim_no = Trim(TextSumsungClaimNo.Text)
        custodyData.product_name = Trim(TextProductsName.Text)

        '***keep_noの最終番号取得***
        Dim errFlg As Integer
        Dim keep_no_Max_int As Integer
        Dim strSQL As String = "SELECT MAX(keep_no) AS keep_no_Max FROM dbo.custody "
        strSQL &= "WHERE DELFG = 0 And ship_code = '" & shipCode & "';"
        Dim DT_custody As DataTable
        DT_custody = DBCommon.ExecuteGetDT(strSQL, errFlg)

        If errFlg = 1 Then
            Call showMsg("failed to acquire the last number of keep_no from custody.", "")
            Exit Sub
        End If

        If DT_custody IsNot Nothing Then
            If DT_custody.Rows(0)("keep_no_Max") IsNot DBNull.Value Then
                keep_no_Max_int = DT_custody.Rows(0)("keep_no_Max")
                keep_no_Max_int = keep_no_Max_int + 1
            End If
        End If

        If keep_no_Max_int = 0 Then
            custodyData.keep_no = 1
        Else
            custodyData.keep_no = keep_no_Max_int.ToString
        End If

        'custodyData.keep_no = txtKeepNo.Text

        '***登録***
        Dim clsSet As New Class_analysis
        Call clsSet.setCustody(custodyData, userid, userName, errFlg, shipCode)

        '***結果表示***
        If errFlg = 0 Then

            lblCustomerName.Text = custodyData.customer_name
            lblCustomerTelNo.Text = custodyData.customer_tel
            lblSumsungClaimNo.Text = custodyData.samsung_claim_no
            lblProductsName.Text = custodyData.product_name
            lblCash.Text = custodyData.cash
            lblKeepNo.Text = custodyData.keep_no

            '***total情報を反映***
            Dim strSQL2 As String = "SELECT SUM(cash) AS cash_SUM, COUNT(*) AS COUNT FROM dbo.custody "
            strSQL2 &= "WHERE DELFG = 0 AND ship_code = '" & shipCode & "' AND customer_name ='" & custodyData.customer_name & "' AND customer_tel = '" & custodyData.customer_tel & "' AND takeout = 1;"
            DT_custody = DBCommon.ExecuteGetDT(strSQL2, errFlg)

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

            Call showMsg("Registration of custody is completed.", "")
            Call reSet2()

        Else
            Call showMsg("Registration of custody failed.", "")
            Exit Sub
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
    '登録前リセット処理
    Protected Sub reSet()

        TextCustomerName.BackColor = System.Drawing.Color.FromArgb(255, 255, 255)
        TextCustomerTelNo.BackColor = System.Drawing.Color.FromArgb(255, 255, 255)
        TextCash.BackColor = System.Drawing.Color.FromArgb(255, 255, 255)

        lblCustomerName.Text = ""
        lblCustomerTelNo.Text = ""
        lblSumsungClaimNo.Text = ""
        lblProductsName.Text = ""
        lblCash.Text = ""
        lblKeepNo.Text = ""
        lblTotalCash.Text = ""
        lblTotalNumber.Text = ""

    End Sub
    '登録後リセット処理
    Protected Sub reSet2()

        TextCustomerName.Text = ""
        TextCustomerTelNo.Text = ""
        TextSumsungClaimNo.Text = ""
        TextProductsName.Text = ""
        TextCash.Text = ""

    End Sub
End Class