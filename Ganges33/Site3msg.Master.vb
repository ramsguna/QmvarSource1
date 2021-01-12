Public Class Site3Msg
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

        Call inv_resetSession()

        Response.Redirect("Login.aspx")

    End Sub
    'メニュー画面へ遷移
    Protected Sub BtnHome_Click(sender As Object, e As ImageClickEventArgs) Handles BtnHome.Click

        Call inv_resetSession()

        Dim userLevel As String = Session("user_level")
        Dim adminFlg As Boolean = Session("admin_Flg")

        If userLevel = "0" Or userLevel = "1" Or userLevel = "2" Or adminFlg = True Then
            Response.Redirect("Menu.aspx")
        Else
            Response.Redirect("Menu2.aspx")
        End If

    End Sub

    Protected Sub inv_resetSession()

        'arrival処理のセッション
        Session("push_Count") = 0
        Session("push_CsvCount") = 0
        Session("serial_Cnt") = 0
        Session("serial_CntArrival") = 0
        Session("serial_Max") = 0
        Session("ds_copy") = Nothing
        Session("ds_copy_kosu") = Nothing
        Session("ds_copy_serial") = Nothing
        Session("ds_copy_serial_arrival") = Nothing
        Session("qtyCount_Kosu") = 0
        Session("Kosu_Update") = Nothing
        Session("item_Chk") = Nothing
        Session("file_Name") = Nothing
        Session("item_No") = Nothing
        Session("all_Kinditem") = 0
        Session("all_Qty") = 0
        Session("err_Flg") = Nothing
        Session("error_Item") = Nothing

        'report処理のセッション
        Session("CSV_DATA") = Nothing
        Session("tourokuSData") = Nothing
        Session("user_Mail") = Nothing

        'return処理のセッション
        Session("ds_copy_return") = Nothing
        Session("update_item") = Nothing
        Session("item_Chk") = Nothing
        Session("up_Cnt") = 0
        Session("all_CsvRowCount") = 0
        Session("file_Chk") = Nothing
        Session("parts_Status") = Nothing

    End Sub

End Class