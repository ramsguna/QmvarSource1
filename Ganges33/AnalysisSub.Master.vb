Public Class AnalysisSub
    Inherits System.Web.UI.MasterPage

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        Dim userid As String = Session("user_id")
        Dim setShipname As String = Session("ship_Name")
        Dim userName As String = Session("user_Name")
        Dim userLevel As String = Session("user_level")

        If setShipname IsNot Nothing Then
            LblShipName.Text = setShipname
        End If

        If userid IsNot Nothing Then
            lblUser.Text = userid & " " & userName
        End If
        ' Comment as per mohan san*
        'If userLevel = "6" Then 'VJ 2019/10/29 Add new user level 6 for display only Analysis  Export Start
        '    btnFileUpload.Enabled = False
        '    btnFileUpload.ImageUrl = "~/icon/mneu_money/blank.png"
        '    btnReport.Enabled = False
        '    btnReport.ImageUrl = "~/icon/mneu_money/blank.png"
        '    btnRecovery.Enabled = False
        '    btnRecovery.ImageUrl = "~/icon/mneu_money/blank.png"
        'End If 'VJ 2019/10/29 Add new user level 6 for display only Analysis  Export End
    End Sub

    Protected Sub BtnHome_Click(sender As Object, e As ImageClickEventArgs) Handles BtnHome.Click

        Dim userLevel As String = Session("user_level")
        Dim adminFlg As Boolean = Session("admin_Flg")

        If Not (userLevel = "9") Then
            If userLevel = "0" Or userLevel = "1" Or userLevel = "2" Or adminFlg = True Then
                Response.Redirect("Menu.aspx")
            ElseIf userLevel = "6" Then 'VJ 2019/10/29 Add new user level 6 for display only Analysis  Export
                Response.Redirect("Analysis_Export.aspx")
            Else
                Response.Redirect("Menu2.aspx")
            End If
        End If

    End Sub

    Protected Sub BtnLogof_Click(sender As Object, e As ImageClickEventArgs) Handles BtnLogof.Click
        Response.Redirect("Login.aspx")
    End Sub

    Protected Sub btnFileUpload_Click(sender As Object, e As ImageClickEventArgs) Handles btnFileUpload.Click
        Dim userLevel As String = Session("user_level")
        If userLevel = "6" Then 'VJ 2019/10/29 Add new user level 6 for display only Analysis  Export
            Response.Redirect("Analysis_Export.aspx")
        End If
        If Not (userLevel = "9") Then
            Response.Redirect("Analysis_FileUpload.aspx")
        End If
    End Sub

    Protected Sub btnExportData_Click(sender As Object, e As ImageClickEventArgs) Handles btnExportData.Click
        Dim userLevel As String = Session("user_level")
        If Not (userLevel = "9") Then
            Response.Redirect("Analysis_Export.aspx")
        End If
    End Sub


    Protected Sub btnReport_Click(sender As Object, e As ImageClickEventArgs) Handles btnReport.Click
        Dim userLevel As String = Session("user_level")
        If userLevel = "6" Then 'VJ 2019/10/29 Add new user level 6 for display only Analysis  Export
            Response.Redirect("Analysis_Export.aspx")
        End If
        Response.Redirect("Analysis_Report.aspx")
    End Sub

    Protected Sub btnRecovery_Click(sender As Object, e As ImageClickEventArgs) Handles btnRecovery.Click
        Dim userLevel As String = Session("user_level")

        If userLevel = "6" Then 'VJ 2019/10/29 Add new user level 6 for display only Analysis  Export
            Response.Redirect("Analysis_Export.aspx")
        End If

        If Not (userLevel = "9") Then
            Response.Redirect("Analysis_Refresh.aspx")
        End If
    End Sub

    Protected Sub btnAnalysisData_Click(sender As Object, e As ImageClickEventArgs) Handles btnAnalysisData.Click
        Dim userLevel As String = Session("user_level")
        If Not (userLevel = "9") Then
            Response.Redirect("Analysis_Export_New.aspx")
        End If
    End Sub

    Protected Sub btnUploadSummary_Click(sender As Object, e As ImageClickEventArgs) Handles btnUploadSummary.Click
        Dim userLevel As String = Session("user_level")
        If Not (userLevel = "9") Then
            Response.Redirect("Analysis_Upload_Summary.aspx")
        End If
    End Sub

    Protected Sub btnUploadVerification_Click(sender As Object, e As ImageClickEventArgs) Handles btnUploadVerification.Click
        Dim userLevel As String = Session("user_level")
        If Not (userLevel = "9") Then
            Response.Redirect("Analysis_Export_New.aspx")
        End If
    End Sub

    Protected Sub btnPartsCompare_Click(sender As Object, e As ImageClickEventArgs) Handles btnPartsCompare.Click
        Dim userLevel As String = Session("user_level")
        If Not (userLevel = "9") Then
            Response.Redirect("Analysis_Parts_Compare.aspx")
        End If
    End Sub

    Protected Sub btnStoreManagement_Click(sender As Object, e As ImageClickEventArgs) Handles btnStoreManagement.Click
        Dim userLevel As String = Session("user_level")
        If Not (userLevel = "9") Then
            Response.Redirect("Analysis_Store_Management.aspx")
        End If
    End Sub

    Protected Sub btnRPA_Click(sender As Object, e As ImageClickEventArgs) Handles btnRPA.Click
        Dim userLevel As String = Session("user_level")
        If Not (userLevel = "9") Then
            Response.Redirect("rpa.aspx")
        End If
    End Sub
End Class