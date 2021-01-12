Imports System.Data.SqlClient
Public Class SonyMenu
    Inherits System.Web.UI.Page


    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        '***セッション情報取得***
        Dim userid As String = Session("user_id")
        Dim setShipname As String = Session("ship_Name")

        Dim userLevel As String = Session("user_level")
        Dim adminFlg As Boolean = Session("admin_Flg")

        '***セッションなしログインユーザの対応***
        If userid = "" Then
            Response.Redirect("Login.aspx")
        End If

        '***ログインユーザのレベルに応じたアイコン表示設定***
        Dim setImage1 As Image
        Dim setImage2 As Image
        Dim setImage3 As Image
        Dim setImage4 As Image
        Dim setImage5 As Image
        Dim setImage6 As Image
        Dim setImage7 As Image
        Dim setImage8 As Image

        setImage1 = CType(Master.FindControl("Image1"), Image)
        setImage2 = CType(Master.FindControl("Image2"), Image)
        setImage3 = CType(Master.FindControl("Image3"), Image)
        setImage4 = CType(Master.FindControl("Image4"), Image)
        setImage5 = CType(Master.FindControl("Image5"), Image)
        setImage6 = CType(Master.FindControl("Image6"), Image)
        setImage7 = CType(Master.FindControl("Image7"), Image)
        setImage8 = CType(Master.FindControl("Image8"), Image)

        If userLevel = "0" Or userLevel = "1" Or adminFlg = True Then

            '全部
            If Not setImage1 Is Nothing Then
                setImage1.ImageUrl = "~/icon/repair.png"
            End If

            If Not setImage2 Is Nothing Then
                setImage2.ImageUrl = "~/icon/daylyreport.png"
            End If

            If Not setImage3 Is Nothing Then
                setImage3.ImageUrl = "~/icon/money.png"
            End If

            If Not setImage4 Is Nothing Then
                setImage4.ImageUrl = "~/icon/inventory.png"
            End If

            If Not setImage5 Is Nothing Then
                setImage5.ImageUrl = "~/icon/person.png"
            End If

            If Not setImage6 Is Nothing Then
                setImage6.ImageUrl = "~/icon/analysis.png"
            End If

            If Not setImage7 Is Nothing Then
                setImage7.ImageUrl = "~/icon/nmvar.png"
            End If

            If Not setImage8 Is Nothing Then
                setImage8.ImageUrl = "~/icon/system.png"
            End If

        Else

            Select Case userLevel

           '管理者
                Case 2
                    If Not setImage1 Is Nothing Then
                        setImage1.ImageUrl = "~/icon/repair.png"
                    End If

                    If Not setImage2 Is Nothing Then
                        setImage2.ImageUrl = "~/icon/daylyreport.png"
                    End If

                    If Not setImage3 Is Nothing Then
                        setImage3.ImageUrl = "~/icon/money.png"
                    End If

                    If Not setImage4 Is Nothing Then
                        setImage4.ImageUrl = "~/icon/inventory.png"
                    End If

                    If Not setImage5 Is Nothing Then
                        setImage5.ImageUrl = "~/icon/person.png"
                    End If

                    If Not setImage6 Is Nothing Then
                        setImage6.ImageUrl = "~/icon/analysis.png"
                    End If

                    If Not setImage7 Is Nothing Then
                        setImage7.ImageUrl = "~/icon/system.png"
                    End If

                    BtnMVAR.Visible = False
                    lblnMVAR.Visible = False

                Case Else

                    btnAnalysis.Enabled = False
                    BtnMVAR.Enabled = False
                    btnSystem.Enabled = False
                    btnPerson.Enabled = False
                    btnRepair.Enabled = False
                    btnMoney.Enabled = False
                    btnInventory.Enabled = False
                    BtnDailyReport.Enabled = False

            End Select

        End If

        '***拠点コード取得***
        Dim clsSetCommon As New Class_common
        Dim shipCode As String = ""
        Dim errMsg As String

        clsSetCommon.setShipCode(setShipname, shipCode, errMsg)

        If errMsg <> "" Then
            Call showMsg(errMsg)
            Exit Sub
        End If

        '***ユーザ名取得***
        Dim userName As String = ""

        clsSetCommon.setUserName(userid, userName, errMsg)

        If errMsg <> "" Then
            Call showMsg(errMsg)
            Exit Sub
        End If

        '***システム名称取得***
        Dim systemName As String = ""

        clsSetCommon.setSystemName(shipCode, systemName, errMsg)

        If errMsg <> "" Then
            Call showMsg(errMsg)
            Exit Sub
        End If

        '***本日の開店情報表示***
        Dim reserveData As Class_money.T_Reserve
        Dim SyoriFlg As Boolean = False
        Dim clsSetMoney As New Class_money

        'OPEN処理終了確認
        clsSetMoney.chkSyoriOpenClose("open", shipCode, SyoriFlg, reserveData, errMsg)

        If errMsg <> "" Then
            Call showMsg(errMsg)
            Exit Sub
        End If

        If SyoriFlg = True Then
            TextTodayMsg.Text = "open " & reserveData.conf_datetime.ToString & " " & reserveData.conf_user
        Else

        End If

        '***セッション設定***
        Session("ship_code") = shipCode
        Session("user_Name") = userName
        Session("system_Name") = systemName

    End Sub

    Protected Sub BtnAnalysis_Click(sender As Object, e As ImageClickEventArgs) Handles btnAnalysis.Click
        Response.Redirect("SonyAnalysis_FileUpload.aspx")
    End Sub



    Protected Sub showMsg(ByVal Msg As String)

        lblMsg.Text = Msg
        Dim sScript As String = "$(function () { $( ""#dialog"" ).dialog({width: 400, buttons: {""OK"": function () {$(this).dialog('close');}}});});"
        'JavaScriptの埋め込み
        ClientScript.RegisterStartupScript(Me.GetType(), "startup", sScript, True)

    End Sub

End Class