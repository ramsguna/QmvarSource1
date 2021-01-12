Public Class Maintenance
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        '' セッション情報をクリア
        Session.Clear()
    End Sub

    Protected Sub btnBack_Click(sender As Object, e As EventArgs) Handles btnBack.Click
        Session.Clear()
        '' ログイン画面へリダイレクト
        Response.Redirect("~/login.aspx", True)
    End Sub
End Class