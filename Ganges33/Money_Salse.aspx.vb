Public Class Money_Salse
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        If IsPostBack = False Then

            '***セッションなしログインユーザの対応***
            Dim userid As String = Session("user_id")
            If userid = "" Then
                Response.Redirect("Login.aspx")
            End If

            '***セッション取得***
            Dim userLevel As String = Session("user_level")
            Dim adminFlg As Boolean = Session("admin_Flg")

            Dim setShipname As String = Session("ship_Name")
            Dim shipCode As String = Session("ship_code")
            Dim userName As String = Session("user_Name")


            '***表示の設定***
            lblShipInfo.Text = shipCode & "  " & setShipname
            lblName.Text = userName

            '***拠点名称の設定***
            DropListLocation.Items.Clear()

            If userLevel = "0" Or userLevel = "1" Or userLevel = "2" Or adminFlg = True Then
                Dim shipName() As String
                If Session("ship_name_list") IsNot Nothing Then
                    shipName = Session("ship_name_list")
                    With DropListLocation
                        .Items.Add("Select shipname")
                        .Items.Add("ALL")
                        For i = 0 To UBound(shipName)
                            If Trim(shipName(i)) <> "" Then
                                .Items.Add(shipName(i))
                            End If
                        Next i
                    End With
                End If

            Else
                DropListLocation.Items.Add(setShipname)
            End If

        Else

            '拠点を指定
            Dim setShipName As String = DropListLocation.Text
            Session("set_shipName") = setShipName

        End If

    End Sub

    Protected Sub btnSearch_Click(sender As Object, e As ImageClickEventArgs) Handles btnSearch.Click

        '日付チェック
        Dim dt As DateTime
        Dim startDate As String
        Dim setShipName As String = Session("set_shipName")

        If TextTargetDate.Text <> "" Then
            If DateTime.TryParse(TextTargetDate.Text, dt) Then
                startDate = DateTime.Parse(Trim(TextTargetDate.Text)).ToShortDateString
                Session("start_Date") = startDate
            Else
                Call showMsg("The date specification of TargetDate is invalid.")
                Exit Sub
            End If
        Else
            Call showMsg("Please specify the date of TargetDate.")
            Exit Sub
        End If

        If setShipName = "Select shipname" Then
            Call showMsg("Please select the branch name")
            Exit Sub
        Else
            If setShipName = "ALL" Then
                Response.Redirect("Money_Salse_show.aspx")
            Else
                Response.Redirect("Money_Salse_show2.aspx")
            End If
        End If

    End Sub

    Protected Sub showMsg(ByVal Msg As String)

        lblMsg.Text = Msg
        Dim sScript As String = "$(function () { $( ""#dialog"" ).dialog({width: 400, buttons: {""OK"": function () {$(this).dialog('close');}}});});"
        'JavaScriptの埋め込み
        ClientScript.RegisterStartupScript(Me.GetType(), "startup", sScript, True)

    End Sub

End Class