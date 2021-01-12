Public Class Analysis
    Inherits System.Web.UI.MasterPage

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If IsPostBack = False Then
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
            Dim activepage As String = Request.RawUrl

            If activepage.Contains("Dashboard.aspx") Then
                dashboard.Attributes.Add("class", "active")
                btnDashboard.Attributes.Add("class", "btn_active")
            ElseIf activepage.Contains("Analysis_FileUpload.aspx") Then
                fileupload.Attributes.Add("class", "active")
                btnFileUpload.Attributes.Add("class", "btn_active")

            ElseIf activepage.Contains("Analysis_Export.aspx") Then
                Analysis_Export.Attributes.Add("class", "active")
                btnExportData.Attributes.Add("class", "btn_active")

            ElseIf activepage.Contains("Analysis_Report.aspx") Then
                Analysis_Refresh.Attributes.Add("class", "active")
                btnReport.Attributes.Add("class", "btn_active")
            ElseIf activepage.Contains("Analysis_Report2.aspx") Then
                Analysis_Refresh.Attributes.Add("class", "active")
                btnReport.Attributes.Add("class", "btn_active")
            ElseIf activepage.Contains("Analysis_Refresh.aspx") Then
                Analysis_Recovery.Attributes.Add("class", "active")
                btnRecovery.Attributes.Add("class", "btn_active")

            ElseIf activepage.Contains("Analysis_Upload_Summary.aspx") Then
                analyis.Attributes.Add("class", "btn_active")
                drp1.Attributes.Add("class", "liactive collapsed nav-link collapsed text-truncate")
            ElseIf activepage.Contains("Analysis_Export_New.aspx") Then
                analyis.Attributes.Add("class", "btn_active")
                drp1.Attributes.Add("class", "liactive collapsed nav-link collapsed text-truncate")
            ElseIf activepage.Contains("Analysis_Parts_Compare.aspx") Then
                analyis.Attributes.Add("class", "btn_active")
                drp1.Attributes.Add("class", "liactive collapsed nav-link collapsed text-truncate")
                '  btnPartsCompare.Attributes.Add("class", "btn_active")
            ElseIf activepage.Contains("Analysis_Store_Management.aspx") Then
                analyis.Attributes.Add("class", "btn_active")
                drp1.Attributes.Add("class", "liactive collapsed nav-link collapsed text-truncate")

            ElseIf activepage.Contains("rpa_scheduler.aspx") Then
                rpa.Attributes.Add("class", "btn_active")
                drp2.Attributes.Add("class", "liactive collapsed nav-link collapsed text-truncate")
            ElseIf activepage.Contains("rpa_logs.aspx") Then
                rpa.Attributes.Add("class", "btn_active")
                drp2.Attributes.Add("class", "liactive collapsed nav-link collapsed text-truncate")
            ElseIf activepage.Contains("Rpa_TaskApp.aspx") Then
                rpa.Attributes.Add("class", "btn_active")
                drp2.Attributes.Add("class", "liactive collapsed nav-link collapsed text-truncate")
            ElseIf activepage.Contains("Rpa_OnOff.aspx") Then
                rpa.Attributes.Add("class", "btn_active")
                drp2.Attributes.Add("class", "liactive collapsed nav-link collapsed text-truncate")
            Else
                fileupload.Attributes.Add("class", "active")
                btnFileUpload.Attributes.Add("class", "btn_active")
            End If

        End If


    End Sub

    Protected Sub BtnHome_Click(sender As Object, e As EventArgs) Handles BtnHome.Click

        Dim userLevel As String = Session("user_level")
        Dim adminFlg As Boolean = Session("admin_Flg")

        If Not (userLevel = "9") Then
            If userLevel = "0" Or userLevel = "1" Or userLevel = "2" Or adminFlg = True Then
                Response.Redirect("Menu.aspx")
            ElseIf userLevel = "6" Then 'VJ 2019/10/29 Add new user level 6 for display only Analysis  Export
                Response.Redirect("Analysis_Export.aspx")
            ElseIf userLevel = "8" Then 'VJ 2020/03/03 Add new user level 8 for display only Analysis  Export
                Response.Redirect("Analysis_Export.aspx")
            Else
                Response.Redirect("Menu2.aspx")
            End If
        End If

    End Sub

    Protected Sub BtnLogof_Click(sender As Object, e As EventArgs) Handles BtnLogof.Click
        Response.Redirect("Login.aspx")
    End Sub

    Protected Sub btnFileUpload_Click(sender As Object, e As EventArgs) Handles btnFileUpload.Click
        Dim userLevel As String = Session("user_level")
        If userLevel = "6" Then 'VJ 2019/10/29 Add new user level 6 for display only Analysis  Export
            Response.Redirect("Analysis_Export.aspx")
        ElseIf userLevel = "8" Then 'VJ 2020/03/03 Add new user level 8 for display only Analysis  Export
            Response.Redirect("Analysis_Export.aspx")
        End If
        If Not (userLevel = "9") Then
            Response.Redirect("Analysis_FileUpload.aspx")
        End If
    End Sub

    Protected Sub btnExportData_Click(sender As Object, e As EventArgs) Handles btnExportData.Click
        Dim userLevel As String = Session("user_level")
        If userLevel = "8" Then 'VJ 2020/03/03 Add new user level 8 for display only Analysis  Export
            Response.Redirect("Analysis_Export.aspx")
        End If
        If Not (userLevel = "9") Then
            Response.Redirect("Analysis_Export.aspx")
        End If
    End Sub


    Protected Sub btnReport_Click(sender As Object, e As EventArgs) Handles btnReport.Click
        Dim userLevel As String = Session("user_level")
        If userLevel = "6" Then 'VJ 2019/10/29 Add new user level 6 for display only Analysis  Export
            Response.Redirect("Analysis_Export.aspx")
        ElseIf userLevel = "8" Then 'VJ 2020/03/03 Add new user level 8 for display only Analysis  Export
            Response.Redirect("Analysis_Export.aspx")
        End If
        Response.Redirect("Analysis_Report.aspx")
    End Sub

    Protected Sub btnRecovery_Click(sender As Object, e As EventArgs) Handles btnRecovery.Click
        Dim userLevel As String = Session("user_level")

        If userLevel = "6" Then 'VJ 2019/10/29 Add new user level 6 for display only Analysis  Export
            Response.Redirect("Analysis_Export.aspx")
        ElseIf userLevel = "8" Then 'VJ 2020/03/03 Add new user level 8 for display only Analysis  Export
            Response.Redirect("Analysis_Export.aspx")
        End If

        If Not (userLevel = "9") Then
            Response.Redirect("Analysis_Refresh.aspx")
        End If
    End Sub

    'Protected Sub btnAnalysisData_Click(sender As Object, e As ImageClickEventArgs) Handles btnAnalysisData.Click
    '    Dim userLevel As String = Session("user_level")
    '    If userLevel = "8" Then 'VJ 2020/03/03 Add new user level 8 for display only Analysis  Export
    '        Response.Redirect("Analysis_Export.aspx")
    '    End If
    '    If Not (userLevel = "9") Then
    '        Response.Redirect("Analysis_Data.aspx")
    '    End If
    'End Sub

    'Protected Sub btnRPA_Click(sender As Object, e As ImageClickEventArgs) Handles btnRPA.Click
    '    Dim userLevel As String = Session("user_level")
    '    If Not (userLevel = "9") Then
    '        Response.Redirect("rpa.aspx")
    '    End If
    'End Sub
    Protected Sub btnUploadSummary_Click(sender As Object, e As EventArgs) Handles btnUploadSummary.Click
        Dim userLevel As String = Session("user_level")
        If Not (userLevel = "9") Then
            Response.Redirect("Analysis_Upload_Summary.aspx")
        End If
    End Sub

    Protected Sub btnUploadVerification_Click(sender As Object, e As EventArgs) Handles btnUploadVerification.Click
        Dim userLevel As String = Session("user_level")
        If Not (userLevel = "9") Then
            Response.Redirect("Analysis_Export_New.aspx")
        End If
    End Sub

    Protected Sub btnPartsCompare_Click(sender As Object, e As EventArgs) Handles btnPartsCompare.Click
        Dim userLevel As String = Session("user_level")
        If Not (userLevel = "9") Then
            Response.Redirect("Analysis_Parts_Compare.aspx")
        End If
    End Sub

    Protected Sub btnStoreManagement_Click(sender As Object, e As EventArgs) Handles btnStoreManagement.Click
        Dim userLevel As String = Session("user_level")
        If Not (userLevel = "9") Then
            Response.Redirect("Analysis_Store_Management.aspx")
        End If
    End Sub
    'Protected Sub btnRPA_Click(sender As Object, e As EventArgs) Handles btnRpa.Click
    '    Dim userLevel As String = Session("user_level")
    '    If Not (userLevel = "9") Then
    '        Response.Redirect("rpa.aspx")
    '    End If
    'End Sub

    Protected Sub btnscheduler_Click(sender As Object, e As EventArgs) Handles btnscheduler.Click
        Response.Redirect("rpa_scheduler.aspx")
    End Sub

    Protected Sub btnRpaLog_Click(sender As Object, e As EventArgs) Handles btnRpaLog.Click
        Response.Redirect("rpa_logs.aspx")
    End Sub

    Protected Sub btnRpaTaskApp_Click(sender As Object, e As EventArgs) Handles btnRpaTaskApp.Click
        Response.Redirect("Rpa_TaskApp.aspx")
    End Sub

    Protected Sub btnRpaOnOff_Click(sender As Object, e As EventArgs) Handles btnRpaOnOff.Click
        Response.Redirect("Rpa_OnOff.aspx")
    End Sub

    Private Sub btnDashboard_Click(sender As Object, e As EventArgs) Handles btnDashboard.Click
        Response.Redirect("Dashboard.aspx")
    End Sub
End Class