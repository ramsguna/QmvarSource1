Imports System.Data.SqlClient
Public Class Money_Menu
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        If IsPostBack = False Then

            '***セッションなしログインユーザの対応***
            Dim userid As String = Session("user_id")
            If userid = "" Then
                Response.Redirect("Login.aspx")
            End If

            lblNews.Text = ""

            '***セッション情報取得***
            Dim shipCode As String = Session("ship_code")

            '***開店・閉店情報を表示***
            Dim clsSet As New Class_money
            Dim errMsg As String = ""
            Dim opentime As String = ""
            Dim closetime As String = ""

            If clsSet.chkOpen(shipCode, opentime, closetime, errMsg) = False Then
                lblNews.Text = "closed" & vbCrLf
            Else
                lblNews.Text = "opening store" & vbCrLf
            End If

            If errMsg <> "" Then
                Call showMsg(errMsg)
                Exit Sub
            End If

            '***diffを表示***
            Dim errFlg As Integer
            Dim diff As String = ""

            Dim clsSetCommon As New Class_common
            Dim dtNow As DateTime = clsSetCommon.dtIndia
            Dim yesterday As DateTime = DateAdd("d", -1, dtNow)

            Dim strSQL As String
            strSQL &= "SELECT TOP 1 * FROM dbo.T_Reserve WHERE DELFG = 0 "
            strSQL &= "AND LEFT(CONVERT(VARCHAR, datetime, 111), 10) = '" & yesterday.ToShortDateString & "' "
            strSQL &= "AND ship_code = '" & shipCode & "' ORDER BY datetime DESC;"

            Dim DT_T_Reserve As DataTable = DBCommon.ExecuteGetDT(strSQL, errFlg)

            If DT_T_Reserve IsNot Nothing Then
                If DT_T_Reserve(0)("diff") IsNot DBNull.Value Then
                    diff = DT_T_Reserve(0)("diff")
                    lblNews.Text &= "Yesterday's cash difference is " & diff & "INR " & vbCrLf
                End If
            End If

            If errFlg = 1 Then
                Call showMsg("T_Reserve data acquisition failed.")
                Exit Sub
            End If

            lblNews.Text &= "" & vbCrLf
            lblNews.Text = Replace(lblNews.Text, vbCrLf, "<br/>")

        End If

    End Sub

    Protected Sub showMsg(ByVal Msg As String)

        lblMsg.Text = Msg
        Dim sScript As String = "$(function () { $( ""#dialog"" ).dialog({width: 400, buttons: {""OK"": function () {$(this).dialog('close');}}});});"
        'JavaScriptの埋め込み
        ClientScript.RegisterStartupScript(Me.GetType(), "startup", sScript, True)

    End Sub

    Protected Sub btnChange_Click(sender As Object, e As ImageClickEventArgs) Handles btnChange.Click
        Response.Redirect("Money_Reserve.aspx")
    End Sub

    Protected Sub btnAggregation_Click(sender As Object, e As ImageClickEventArgs) Handles btnAggregation.Click
        Response.Redirect("Money_Aggregation.aspx")
    End Sub

    Protected Sub btnControl_Click(sender As Object, e As ImageClickEventArgs) Handles btnControl.Click

    End Sub

End Class