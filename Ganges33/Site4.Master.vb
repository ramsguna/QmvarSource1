Public Class Site4
    Inherits System.Web.UI.MasterPage

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        Dim userid As String = Session("user_id")
        Dim setShipname As String = Session("ship_Name")
        Dim userName As String = Session("user_Name")

        If setShipname IsNot Nothing Then
            lblShipName.Text = setShipname
        End If

        If userid IsNot Nothing Then
            lblUser.Text = userid & " " & userName
        End If

    End Sub

    'ログオフ処理
    Protected Sub BtnLogof_Click(sender As Object, e As ImageClickEventArgs) Handles BtnLogof.Click

        Call money_reSetSession()
        Response.Redirect("Login.aspx")

    End Sub

    'メニュー画面へ遷移
    Protected Sub BtnHome_Click(sender As Object, e As ImageClickEventArgs) Handles BtnHome.Click

        Call money_reSetSession()

        Dim userLevel As String = Session("user_level")
        Dim adminFlg As Boolean = Session("admin_Flg")

        If userLevel = "0" Or userLevel = "1" Or userLevel = "2" Or adminFlg = True Then
            Response.Redirect("Menu.aspx")
        Else
            Response.Redirect("Menu2.aspx")
        End If

    End Sub

    'セッション開放
    Protected Sub money_reSetSession()

        'Money_Reserveのセッション
        Session("open_SyoriFlg") = Nothing
        Session("ins1_SyoriFlg") = Nothing
        Session("ins2_SyoriFlg") = Nothing
        Session("ins3_SyoriFlg") = Nothing
        Session("close_SyoriFlg") = Nothing
        Session("ins1_SyoriFlg2") = Nothing
        Session("ins2_SyoriFlg2") = Nothing
        Session("ins3_SyoriFlg2") = Nothing
        Session("close_SyoriFlg2") = Nothing

        'Money_Salse_showのセッション
        Session("page_NO") = Nothing
        Session("btnNext_Cnt") = Nothing
        Session("btnStart_Cnt") = Nothing
        Session("last_ShowNO") = Nothing
        Session("set_shipName") = Nothing
        Session("start_Date") = Nothing

        '後始末　サーバ側に保存された不要なPDFファイルを削除
        Dim pdfFileName As String = Session("pdf_FileName")

        Dim clsSet As New Class_money

        'サーバに保存されたPDFファイルを削除
        If pdfFileName <> "" Then
            If System.IO.File.Exists(clsSet.savePDFPass & pdfFileName) = True Then
                System.IO.File.Delete(clsSet.savePDFPass & pdfFileName)
            End If
        End If

        Session("pdf_FileName") = Nothing

    End Sub

    Protected Sub btnOpenClose_Click(sender As Object, e As ImageClickEventArgs) Handles btnOpenClose.Click
        Call money_reSetSession()
        Response.Redirect("Money_Reserve_qg.aspx")
    End Sub

    Protected Sub btnCash_Click(sender As Object, e As ImageClickEventArgs) Handles btnCash.Click
        Call money_reSetSession()
        Response.Redirect("Analysis_Cash_Tracking_imp.aspx")
    End Sub

    Protected Sub btnCustody_Click(sender As Object, e As ImageClickEventArgs) Handles btnCustody.Click
        Call money_reSetSession()
        Response.Redirect("Analysis_Custody.aspx")
    End Sub

    Protected Sub btnAggregation_Click(sender As Object, e As ImageClickEventArgs) Handles btnAggregation.Click
        Call money_reSetSession()
        Response.Redirect("Money_Aggregation_qg.aspx")
    End Sub

    Protected Sub btnConfirm_Click(sender As Object, e As ImageClickEventArgs) Handles btnConfirm.Click
        Call money_reSetSession()
        Response.Redirect("Money_Report_qg.aspx")
    End Sub

    Protected Sub btnSales_Click(sender As Object, e As ImageClickEventArgs) Handles btnSales.Click
        Call money_reSetSession()
        Response.Redirect("Money_Sales.aspx")
    End Sub

End Class