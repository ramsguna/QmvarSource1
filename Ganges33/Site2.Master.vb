Public Class Site2
    Inherits System.Web.UI.MasterPage

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        Dim userid As String = Session("user_id")
        Dim setShipname As String = Session("ship_Name")
        Dim userName As String = Session("user_Name")

        If setShipname IsNot Nothing Then
            lblShipName.Text = setShipname
        End If

        If userid IsNot Nothing Then
            'lblUser.Text = userid & " " & userName
            lblUser.Text = userid
        End If

    End Sub
    'ホームボタン押下処理
    Protected Sub BtnHome_Click(sender As Object, e As ImageClickEventArgs) Handles BtnHome.Click

        Call reSetSession()

    End Sub
    'ログオフボタン押下処理
    Protected Sub BtnLogof_Click(sender As Object, e As ImageClickEventArgs) Handles BtnLogof.Click

        Response.Redirect("Login.aspx")

    End Sub

    Protected Sub reSetSession()

        'Money_Record_3のセッション
        Session("other_Data") = Nothing
        Session("poNo_Max") = Nothing
        Session("po_M") = Nothing
        Session("po_No_NoGSPN") = Nothing

        'Money_Record_4のセッション
        'Session("Completed_Date") = Nothing
        Session("cal_BtnCount") = 0
        Session("ds_repair1") = Nothing

        'Money_Salse_showのセッション
        Session("page_NO") = Nothing
        Session("btnNext_Cnt") = Nothing
        Session("btnStart_Cnt") = Nothing
        Session("last_ShowNO") = Nothing
        Session("set_shipName") = Nothing
        Session("start_Date") = Nothing

    End Sub

End Class