Imports System.Globalization
Imports System.IO

Imports Ganges33.Ganges33.model
Imports Ganges33.Ganges33.logic

Public Class Analysis_FileUpload_qg
    Inherits System.Web.UI.Page
    Private csvData()() As String
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        If IsPostBack = False Then

            '***セッションなしログインユーザの対応***
            Dim userid As String = Session("user_id")
            If userid = "" Then
                Response.Redirect("Login.aspx")
            End If

            Dim setShipname As String = Session("ship_Name")
            Dim userName As String = Session("user_Name")
            Dim userLevel As String = Session("user_level")
            Dim adminFlg As Boolean = Session("admin_Flg")

            lblLoc.Text = setShipname
            lblName.Text = userName

            '***拠点名称の設定***
            DropListLocation.Items.Clear()

            If userLevel = "0" Or userLevel = "1" Or userLevel = "2" Or adminFlg = True Then
                Dim shipName() As String
                If Session("ship_name_list") IsNot Nothing Then
                    shipName = Session("ship_name_list")
                    With DropListLocation
                        .Items.Add("Select Branch")
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





            'Load all incomplete
            GridViewBind(setShipname)

        Else

            '***セッション設定***
            Session("upload_Shipname") = DropListLocation.Text


        End If

    End Sub

    Protected Sub btnSend_Click(sender As Object, e As ImageClickEventArgs) Handles btnSend.Click
        Dim userName As String = Session("user_Name")
        'Verify that location has been selected or not
        If DropListLocation.Text = "Select Branch" Then
            Call showMsg("Select the Branch", "")
            Exit Sub
        End If
        'If File not uploaded then show the error msg to the user
        If Not FileUploadAnalysis.HasFile Then
            Call showMsg("File Not Uploaded!!!", "")
            Exit Sub
        End If
        'Find CSV file 
        Dim strExtension As String = System.IO.Path.GetExtension(FileUploadAnalysis.FileName)
        If Not (strExtension = ".csv") Then
            Call showMsg("Uploaded file must be CSV", "")
            Exit Sub
        End If
        'Move the file to temporary folder
        If Not Directory.Exists(ConfigurationManager.AppSettings("RootDir") & "\" & userName & "\" & DropListLocation.Text) Then
            Directory.CreateDirectory(ConfigurationManager.AppSettings("RootDir") & "\" & userName & "\" & DropListLocation.Text)
        End If
        'Assign File Name
        Dim dirPath As String = ConfigurationManager.AppSettings("RootDir") & "\" & userName & "\" & DropListLocation.Text
        Dim fileName As String = DateTime.Now.ToString("yyyyMMddHHmmssfff") & strExtension
        Dim filenameFullPath As String = dirPath & "\" & fileName
        'Save the file in temporary folder
        FileUploadAnalysis.SaveAs(filenameFullPath)
        'Read the content from stored file
        Dim cReader
        Dim textLines As New List(Of String())
        Dim strArr()() As String
        Try
            'StreamReader の新しいインスタンスを生成する
            cReader = New System.IO.StreamReader(filenameFullPath, System.Text.Encoding.Default)

            Dim rowCnt As Integer

            '読み込んだものを追加で格納
            Dim cols() As String
            Dim colsHead() As String 'ヘッダ１行目
            Dim colsHead2() As String 'ヘッダ２行目
            Dim colsValues() As String

            While (cReader.Peek() >= 0)

                'ファイルを1 行ずつ読み込む
                Dim stBuffer As String = cReader.ReadLine()
                rowCnt = rowCnt + 1


                '『"』の位置取得
                Dim pos As Integer = 0

                '全ての『"』の位置情報をセット
                Dim posSet() As Integer

                Dim cnt As Integer = 0
                Dim key As String = """"
                Dim tmp As String = ""
                Dim tmpAfter As String = ""
                Dim workPos As Integer
                Dim workLenStr As Integer

                While True


                    If rowCnt = 1 Then
                        colsHead = Split(stBuffer, ",")
                        Exit While ' Assign 1st header, then close
                    ElseIf rowCnt = 2 Then
                        colsHead2 = Split(stBuffer, ",")
                        Exit While ' Assign 2nd header, then close
                    Else
                        colsValues = Split(stBuffer, ",")
                    End If


                    pos = stBuffer.IndexOf(key, pos)
                    If pos = -1 Then
                        Exit While
                    Else
                        ReDim Preserve posSet(cnt)
                        posSet(cnt) = pos
                        cnt = cnt + 1
                        pos = pos + 1

                        If cnt Mod 2 = 0 Then
                            '2の倍数になったら(""で囲まれた文字列取得できたら")その文字列の最初の位置と長さを取得
                            workPos = posSet(cnt - 2)
                            workLenStr = (posSet(cnt - 1) - posSet(cnt - 2)) + 1
                            tmp = stBuffer.Substring(workPos, workLenStr)
                            '『,』を外す
                            tmpAfter = Replace(tmp, ",", "")
                            '『"』を外す
                            tmpAfter = Replace(tmpAfter, """", "")
                            'stBufferを置換
                            stBuffer = Replace(stBuffer, tmp, tmpAfter)
                            pos = 0
                        End If

                    End If

                End While

                cols = Split(stBuffer, ",")

                If (rowCnt <> 1) And (rowCnt <> 2) Then
                    'Insert into table 


                End If

                textLines.Add(cols)

                'csvファイル種類切り分けのため、ヘッダの項目名取得しておく


            End While

        Catch ex As Exception
            cReader.Close()
        Finally

            cReader.Close()
        End Try

        strArr = textLines.ToArray

        'Return strArr

        'Pass the paramters to Update in Datatabase
        Dim _CashTrackModel As CashTrackModel = New CashTrackModel()
        Dim _CashTrackControl As CashTrackControl = New CashTrackControl()

        _CashTrackModel.FileName = fileName
        _CashTrackModel.UserId = userName
        _CashTrackModel.Location = DropListLocation.Text
        'import to csv file to database of cash tracking (GSPN to MultiVendor)
        'インポート に CSV ファイル データベース の 現金 
        Dim blStatus As Boolean = _CashTrackControl.UpdateAutoCashTrack(strArr, _CashTrackModel)
        'If Error Raised then show the errors
        'もし エラー 上げた  それから  見せる の エラー
        If Not blStatus Then
            Call showMsg("Couldn't import the file!!!", "")
            Exit Sub
        End If


        GridViewBind()



    End Sub




    Protected Sub showMsg(ByVal Msg As String, ByVal msgChk As String)

        Dim FileName As String = Session("File_Name")
        Dim billingDateAll() As String = Session("billingDate_All")
        Dim deliveryDateAll() As String = Session("deliveryDate_All")

        Dim clsSetCommon As New Class_common
        Dim dtnow As DateTime = clsSetCommon.dtIndia

        ListMsg.Items.Clear()

        'メッセージ欄にメッセージ記載
        ListMsg.Items.Add(Msg & FileName & "  " & dtnow)

        'メッセージ履歴欄にメッセージ記載
        '※履歴の為メッセージは残す
        ListHistory.Items.Add(Msg & FileName & "  " & dtnow)

        '登録された請求日期間を記載
        '※請求日に誤りがないか確認するもの
        If billingDateAll IsNot Nothing Then
            If billingDateAll.Length = 1 Then
                ListHistory.Items.Add("Registered data is " & billingDateAll(0) & " ")
            Else
                ListHistory.Items.Add("Registered data is " & billingDateAll(0) & "～" & billingDateAll(billingDateAll.Length - 1) & " ")
            End If
        End If

        If deliveryDateAll IsNot Nothing Then
            If deliveryDateAll.Length = 1 Then
                ListHistory.Items.Add("Registered data is " & deliveryDateAll(0) & " ")
            Else
                ListHistory.Items.Add("Registered data is " & deliveryDateAll(0) & "～" & deliveryDateAll(deliveryDateAll.Length - 1) & " ")
            End If
        End If

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

        Session("File_Name") = Nothing
        Session("billingDate_All") = Nothing
        Session("deliveryDate_All") = Nothing

    End Sub

    Protected Sub btnback_Click(sender As Object, e As ImageClickEventArgs) Handles btnback.Click

        Response.Redirect("Menu.aspx")

    End Sub

    Protected Sub GridViewBind(ByVal Optional Location As String = "")
        Dim _CashTrackModel As CashTrackModel = New CashTrackModel()
        Dim _CashTrackControl As CashTrackControl = New CashTrackControl()
        If Len(Location) > 0 Then
            _CashTrackModel.Location = Location
        Else
            _CashTrackModel.Location = DropListLocation.Text
        End If

        Dim dtCashTrack As DataTable = _CashTrackControl.SelectCashTrackIncomplete(_CashTrackModel)
        If dtCashTrack Is Nothing Then
            ViewState("dtCashTrackCount") = 0
        Else
            ViewState("dtCashTrackCount") = dtCashTrack.Rows.Count
        End If
        gvCashTrack.DataSource = dtCashTrack
        ViewState("dtCashTrackCount") = dtCashTrack
        gvCashTrack.DataBind()
     
    End Sub

    Protected Sub BtnUpdateCashTrack_Click(sender As Object, e As EventArgs) Handles BtnUpdateCashTrack.Click
        'Read
        '"SELECT CRTDT,CRTCD,UPDDT,UPDCD,UPDPG,DELFG,claim_no,invoice_date,Invoice_No,customer_name,Warranty,payment,payment_kind,total_amount,input_user,location,card_number,card_type,deposit,change,count_no,message,FALSE,claim,claim_card,full_discount,discount,discount_after_amt,discount_after_amt_credit,incomplete FROM cash_track "
        '
        '1st 
        'Verify that location has been selected or not
        If DropListLocation.Text = "Select Branch" Then
            Call showMsg("Select the Branch", "")
            Exit Sub
        End If
        'Grid Row Count 
        If Me.gvCashTrack.Rows.Count = 0 Then
            Call showMsg("There is no records to update it", "")
            Exit Sub
        End If

        If DropListLocation.Text = "" Then
            Call showMsg("Successfully Updated", "")
        End If
        Dim lstModel As List(Of CashTrackModel) = New List(Of CashTrackModel)()
        Dim chkBoxSelect As Integer = 0
        Dim _CashTrackControl As CashTrackControl = New CashTrackControl()
        For Each target As GridViewRow In Me.gvCashTrack.Rows
            Dim lblServiceOrderNo As Label = CType(target.FindControl("lblServiceOrderNo"), Label)
            Dim chkCard As CheckBox = CType(target.FindControl("chkCard"), CheckBox)
            Dim txtDiscount As TextBox = CType(target.FindControl("txtDiscount"), TextBox)
            Dim chkIncomplete As CheckBox = CType(target.FindControl("chkIncomplete"), CheckBox)
            Dim location As HiddenField = CType(target.FindControl("location"), HiddenField)
            If Not String.IsNullOrEmpty(lblServiceOrderNo.Text) Then
                'Only for update is required
                'のみ にとって 更新 です 必須
                If Not chkIncomplete.Checked Then
                    chkBoxSelect = 1
                    Dim targetModel As CashTrackModel = New CashTrackModel()
                    targetModel.ServiceOrderNo = lblServiceOrderNo.Text
                    targetModel.Location = DropListLocation.Text
                    If chkCard.Checked Then
                        targetModel.IsCard = True
                    Else
                        targetModel.IsCard = False
                    End If
                    targetModel.Discount = txtDiscount.Text
                    targetModel.InComplete = True
                    lstModel.Add(targetModel)
                End If
            Else
                Exit For
            End If
        Next

        'If selected then update to database
        'もし 選択された それから 更新 に データベース
        If chkBoxSelect = 1 Then
            If _CashTrackControl.UpdateCashTrackIncomplete(lstModel) Then
                GridViewBind()
                Call showMsg("Successfully Updated", "")
            Else
                Call showMsg("Failed to upload it", "")
            End If
        End If
    End Sub
    Protected Sub drpType1_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs)
        Log4NetControl.ComInfoLogWrite(Session("UserName"))
        Dim ddl As DropDownList = CType(sender, DropDownList)
        Dim row As GridViewRow = CType(ddl.Parent.Parent, GridViewRow)
        Dim idx As Integer = row.RowIndex
        Dim lblCash As Label = CType(row.Cells(0).FindControl("lblCash"), Label)
        Dim txtCash As TextBox = CType(row.Cells(0).FindControl("txtCash"), TextBox)
        Dim lblCard As Label = CType(row.Cells(0).FindControl("lblCard"), Label)
        Dim txtCard As TextBox = CType(row.Cells(0).FindControl("txtCard"), TextBox)

        If ddl.SelectedItem.Text = "Both" Then
            lblCash.Visible = True
            txtCash.Visible = True
            lblCard.Visible = True
            txtCard.Visible = True
        Else
            lblCash.Visible = False
            txtCash.Visible = False
            lblCard.Visible = False
            txtCard.Visible = False
        End If



    End Sub
End Class