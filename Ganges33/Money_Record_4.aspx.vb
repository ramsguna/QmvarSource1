Imports System.Drawing
Public Class Money_Record_4
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        If IsPostBack = False Then

            '***セッションなしログインユーザの対応***
            Dim userid As String = Session("user_id")
            If userid = "" Then
                Response.Redirect("Login.aspx")
            End If

            '***初期表示***
            '管理番号より登録情報の表示
            Dim poNo As String = Session("po_No_NoGSPN")
            Call showData(poNo)

            '閲覧モード　　
            Call showMode()

            btnSend.Enabled = False
            btnCalculation.Enabled = False
            lblQtySum1.Text = ""
            lblQtySum2.Text = ""

        Else

            Dim BtnCancelChk As String = ""
            Dim BtnOKChk As String = ""
            Dim BtnOK2Chk As String = ""
            Dim BtnOKLastChk As String = ""
            Dim BtnOK2LastChk As String = ""
            Dim errFlg As Integer

            '***どのボタンが押下されたか確認***
            For Each s In Context.Request.Form.AllKeys
                If s.Contains("BtnCancel") Then
                    BtnCancelChk = "BtnCancelOn"
                    Exit For
                End If
            Next s

            For Each s In Context.Request.Form.AllKeys
                If s.Contains("BtnOK") Then
                    BtnOKChk = "BtnOKOn"
                    Exit For
                End If
            Next s

            For Each s In Context.Request.Form.AllKeys
                If s.Contains("Btn2OK") Then
                    BtnOK2Chk = "BtnOKOn2"
                    Exit For
                End If
            Next s

            For Each s In Context.Request.Form.AllKeys
                If s.Contains("BtnLastOK") Then
                    BtnOKLastChk = "BtnOKOnLast"
                    Exit For
                End If
            Next s

            For Each s In Context.Request.Form.AllKeys
                If s.Contains("Btn2LastOK") Then
                    BtnOK2LastChk = "BtnOKOnLast2"
                    Exit For
                End If
            Next s

            '***上書き登録のダイアログでキャンセルボタン押下処理　OR　計算ボタン押下可否のダイアログでキャンセルボタン押下処理***
            If (BtnCancelChk = "BtnCancelOn") Then
                Call showMsg("Process canceled.", "")
                btnStart.Enabled = True
            End If

            '***上書き登録のダイアログでOKボタン押下処理***
            If (BtnOKChk = "BtnOKOn") Then

                '***データ登録***
                Dim userid As String = Session("user_id")
                Dim shipCode As String = Session("ship_code")
                Dim shipName As String = Session("ship_name")
                Dim userLevel As String = Session("user_level")
                Dim adminFlg As Boolean = Session("admin_Flg")
                Dim poM As String = Session("po_M")
                Dim poNoMax As Long = Session("poNo_Max")

                Dim levelFlg As String
                If userLevel = "0" Or userLevel = "1" Or userLevel = "2" Or adminFlg = True Then
                    levelFlg = "1"
                End If

                Dim clsSet As New Class_money

                '画面からの入力情報
                Dim otherData As Class_money.OTHER_DATA
                If Session("other_Data") IsNot Nothing Then
                    otherData = Session("other_Data")
                End If

                Dim tourokuFlg As Integer = -1
                Call clsSet.setCsvData2(Nothing, errFlg, otherData, Nothing, userid, shipCode, poM, poNoMax, "", levelFlg, tourokuFlg)

                If errFlg = 1 Then
                    Call showMsg("Data registration is failed.", "Last")
                    Exit Sub
                Else

                    '***PDF出力処理***
                    Dim clsSetCommon As New Class_common
                    Dim dtNow As DateTime = clsSetCommon.dtIndia
                    Dim pdfFileName As String = dtNow.ToString("yyyyMMddHHmmss") & "_Estimate.pdf"
                    Dim pdfFileNameWorkReport As String = dtNow.ToString("yyyyMMddHHmmss") & "_WorkReport.pdf"

                    '見積書作成にチェックあり
                    If ChkEsdtimate.Checked = True Then

                        '完了日の記載なし
                        If otherData.Completed_Date = "" Then

                            '見積書出力
                            Call clsSet.EstimatesPDF2(otherData, pdfFileName, errFlg, shipName)

                            If errFlg = 1 Then
                                'サーバに保存されたファイルを削除
                                Dim FilePath As String = clsSet.savePDFPass & pdfFileName
                                If FilePath <> "" Then
                                    If System.IO.File.Exists(FilePath) = True Then
                                        System.IO.File.Delete(FilePath)
                                    End If
                                End If
                                Call showMsg("Failed to create estimate.", "Last")
                                Exit Sub
                            End If

                            'メッセージ出力後、⇓ファイルダウンロード
                            Session("pdf_FileName") = pdfFileName
                            Call showMsg("Data registration and report preparation completed.<br />Please go back to the menu with the back button.", "Last2")

                        Else

                            '完了しているので、作業報告書のみ出力
                            Call clsSet.WorkReportPDF(otherData, pdfFileNameWorkReport, errFlg, shipName)

                            If errFlg = 1 Then
                                'サーバに保存されたファイルを削除
                                Dim FilePath As String = clsSet.savePDFPass & pdfFileNameWorkReport
                                If FilePath <> "" Then
                                    If System.IO.File.Exists(FilePath) = True Then
                                        System.IO.File.Delete(FilePath)
                                    End If
                                End If
                                Call showMsg("Failed to create work report.", "Last")
                                Exit Sub
                            End If

                            'メッセージ出力後、load処理で、ファイルダウンロード
                            Session("pdf_FileName") = pdfFileNameWorkReport
                            Call showMsg("Data registration and report preparation completed.<br />Please go back to the menu with the back button.", "Last2")

                        End If

                    Else
                        '完了日の記載あり
                        If otherData.Completed_Date <> "" Then

                            '作業報告書出力
                            Call clsSet.WorkReportPDF(otherData, pdfFileNameWorkReport, errFlg, shipName)

                            If errFlg = 1 Then
                                'サーバに保存されたファイルを削除
                                Dim FilePath As String = clsSet.savePDFPass & pdfFileNameWorkReport
                                If FilePath <> "" Then
                                    If System.IO.File.Exists(FilePath) = True Then
                                        System.IO.File.Delete(FilePath)
                                    End If
                                End If
                                Call showMsg("Failed to create work report.", "Last")
                                Exit Sub
                            End If

                            'メッセージ出力後、load処理で、ファイルダウンロード
                            Session("pdf_FileName") = pdfFileNameWorkReport
                            Call showMsg("Data registration and report preparation completed<br />Please go back to the menu with the back button.", "Last2")

                        Else
                            Call showMsg("Update processing is complete.", "Last")
                            Exit Sub
                        End If

                    End If

                    btnStart.Enabled = False
                    btnSend.Enabled = False

                End If

            End If

            '***計算ボタン押下可否のダイアログでOKボタン押下処理 ***
            If (BtnOK2Chk = "BtnOKOn2") Then

                Dim otherData As Class_money.OTHER_DATA
                Dim errMsg As String

                '念の為、部品情報の計算
                If DropListDenomination.Text <> "resept only" Then
                    Call Calculation(otherData, errMsg)
                    If errMsg <> "" Then
                        Call showMsg(errMsg, "")
                        Exit Sub
                    End If
                Else
                    TextComment2.Focus()
                End If

                '登録処理
                Call touroku(otherData)

                End If

                '***メニュー画面へ遷移***
                If BtnOKLastChk = "BtnOKOnLast" Then
                Call reSetSession()
                Response.Redirect("Menu.aspx")

            End If

            '***登録OK後処理（PDFファイルのダウンロード）***
            If BtnOK2LastChk = "BtnOKOnLast2" Then
                Dim pdfFileName As String = Session("pdf_FileName")
                Call reSetSession()
                Call FileDownload(pdfFileName, "application/pdf")
            End If

        End If

    End Sub

    Protected Sub FileDownload(FileName As String, MimeType As String)

        Dim clsSet As New Class_money

        'ダウンロードするファイル名
        Dim dlFileName As String

        'ファイル名が日本語の場合を考慮したダウンロードファイル名を作成
        If Request.Browser.Browser = "IE" Then
            'IEの場合はファイル名をURLエンコード
            dlFileName = HttpUtility.UrlEncode(FileName)
        Else
            'IE以外の場合はそのままでOK
            dlFileName = FileName
        End If

        Dim FilePath As String = clsSet.savePDFPass & FileName

        Try

            'ファイルの内容をバイト配列にすべて読み込む 
            Dim Buffer As Byte() = System.IO.File.ReadAllBytes(FilePath)

            'サーバに保存されたCSVファイルを削除
            '※Response.End以降、ファイル削除処理ができないため、一旦ファイルの内容を
            '上記、Bufferに保存し、ダウンロード処理を行う。

            If FilePath <> "" Then
                If System.IO.File.Exists(FilePath) = True Then
                    System.IO.File.Delete(FilePath)
                End If
            End If

            'ダウンロード処理
            'Response情報クリア
            Response.ClearContent()

            'バッファリング
            Response.Buffer = True

            'HTTPヘッダー情報・MIMEタイプ設定
            Response.AddHeader("Content-Disposition", String.Format("attachment;filename={0}", dlFileName))
            Response.ContentType = MimeType

            'ファイルを書き出し
            Response.BinaryWrite(Buffer)
            'Response.WriteFile(FilePath)
            Response.Flush()
            Response.End()

        Catch ex As System.Threading.ThreadAbortException
            'Response.End()の呼び出しによりエラーメッセージを出力しないようにする

        Catch ex As Exception
            If FilePath <> "" Then
                If System.IO.File.Exists(FilePath) = True Then
                    System.IO.File.Delete(FilePath)
                End If
            End If
            Call showMsg("Failed  to download prcess.", "")
        End Try

    End Sub

    Protected Sub showData(ByVal poNo As String)

        '***セッション情報取得***
        Dim userid As String = Session("user_id")
        Dim shipCode As String = Session("ship_code")

        If Session("ship_code") Is Nothing Then
            Call showMsg("セッションがきれています。ログインしなおしてください。", "")
            Exit Sub
        End If

        '***管理番号に紐づく情報を表示***
        TextPo.Text = poNo

        Dim clsSet As New Class_money
        Dim dsT_repair1 As New DataSet
        Dim sqlStr As String
        Dim errFlg As Integer
        Dim errMsg As String

        sqlStr = "SELECT * FROM dbo.T_repair1 WHERE DELFG = 0 AND Branch_Code = '" & shipCode & "' AND po_no = '" & poNo & "';"
        dsT_repair1 = DBCommon.Get_DS(sqlStr, errFlg)

        If errFlg = 1 Then
            Call showMsg("Failed to acquire po information.", "")
            Exit Sub
        End If

        If dsT_repair1 IsNot Nothing Then

            If dsT_repair1.Tables(0).Rows.Count = 1 Then

                Dim dr As DataRow = dsT_repair1.Tables(0).Rows(0)

                '■入力情報
                If dr("comment") IsNot DBNull.Value Then
                    Dim comment As String = ""
                    comment = dr("comment")
                    TextComment.Text = comment
                End If

                If dr("comment2") IsNot DBNull.Value Then
                    Dim comment2 As String = ""
                    comment2 = dr("comment2")
                    TextComment2.Text = comment2
                End If

                If dr("Consumer_Name") IsNot DBNull.Value Then
                    Dim ConsumerName As String = ""
                    ConsumerName = dr("Consumer_Name")
                    TextConsumer_Name.Text = ConsumerName
                End If

                If dr("Consumer_Addr1") IsNot DBNull.Value Then
                    Dim ConsumerAddr1 As String = ""
                    ConsumerAddr1 = dr("Consumer_Addr1")
                    TextConsumer_Addr1.Text = ConsumerAddr1
                End If

                If dr("Consumer_Addr2") IsNot DBNull.Value Then
                    Dim ConsumerAddr2 As String = ""
                    ConsumerAddr2 = dr("Consumer_Addr2")
                    TextConsumer_Addr2.Text = ConsumerAddr2
                End If

                If dr("Consumer_MailAddress") IsNot DBNull.Value Then
                    Dim ConsumerMailAddress As String = ""
                    ConsumerMailAddress = dr("Consumer_MailAddress")
                    TextCustomer_mail_address.Text = ConsumerMailAddress
                End If

                If dr("Consumer_Telephone") IsNot DBNull.Value Then
                    Dim ConsumerTelephone As String = ""
                    ConsumerTelephone = dr("Consumer_Telephone")
                    TextConsumer_Telephone.Text = ConsumerTelephone
                End If

                If dr("Consumer_Fax") IsNot DBNull.Value Then
                    Dim ConsumerFax As String = ""
                    ConsumerFax = dr("Consumer_Fax")
                    TextConsumer_Fax.Text = ConsumerFax
                End If

                If dr("Postal_Code") IsNot DBNull.Value Then
                    Dim PostalCode As String = ""
                    PostalCode = dr("Postal_Code")
                    TextPostal_Code2.Text = PostalCode
                    Call setListPostal(PostalCode, "", errMsg)
                    If errMsg <> "" Then
                        Call showMsg(errMsg, "")
                        Call reSet()
                        Exit Sub
                    End If
                End If

                If dr("State_Name") IsNot DBNull.Value Then
                    Dim StateName As String = ""
                    StateName = dr("State_Name")
                    Call setListPostal("", StateName, errMsg)
                    If errMsg <> "" Then
                        Call showMsg(errMsg, "")
                        Call reSet()
                        Exit Sub
                    End If
                End If

                If dr("Model") IsNot DBNull.Value Then
                    Dim Model As String = ""
                    Model = dr("Model")
                    TextModel.Text = Model
                End If

                If dr("Serial_No") IsNot DBNull.Value Then
                    Dim SerialNo As String = ""
                    SerialNo = dr("Serial_No")
                    TextSerial_No.Text = SerialNo
                End If

                If dr("IMEI_No") IsNot DBNull.Value Then
                    Dim IMEI_No As String = ""
                    IMEI_No = dr("IMEI_No")
                    TextIMEI_No.Text = IMEI_No
                End If

                If dr("Repair_Description") IsNot DBNull.Value Then
                    Dim RepairDescription As String = ""
                    RepairDescription = dr("Repair_Description")
                    TextRepair_Description.Text = RepairDescription
                End If

                If dr("Repair_Received_Date") IsNot DBNull.Value Then
                    Dim RepairReceivedDate As DateTime
                    RepairReceivedDate = dr("Repair_Received_Date")
                    TextRepair_Received_Date.Text = RepairReceivedDate.ToShortDateString
                End If

                If dr("Maker") IsNot DBNull.Value Then
                    Dim Maker As String = ""
                    Maker = dr("Maker")
                    TextMaker.Text = Maker
                End If

                If dr("Product_Type") IsNot DBNull.Value Then
                    Dim ProductType As String = ""
                    ProductType = dr("Product_Type")
                    DropListProductType.Items.Clear()
                    Call setListProductType("QS", ProductType, errMsg)
                    If errMsg <> "" Then
                        Call showMsg(errMsg, "")
                        Call reSet()
                        Exit Sub
                    End If
                End If

                If dr("warranty") IsNot DBNull.Value Then
                    Dim warranty As String = ""
                    warranty = dr("warranty")
                    DropListWarranty.Items.Clear()
                    With DropListWarranty
                        If warranty = "" Then
                            .Items.Add("select warranty")
                        Else
                            .Items.Add(warranty)
                        End If
                        If warranty <> "in warranty" Then
                            .Items.Add("in warranty")
                        End If
                        If warranty <> "out of warranty" Then
                            .Items.Add("out of warranty")
                        End If
                        If warranty <> "other" Then
                            .Items.Add("other")
                        End If
                    End With
                End If

                '受付日（レコード作成日）
                If dr("CRTDT") IsNot DBNull.Value Then
                    Dim CRTDT As DateTime
                    CRTDT = dr("CRTDT")
                    lblReceptionDate.Text = CRTDT.ToShortDateString
                End If

                '■見積完了情報
                If dr("Completed_Date") IsNot DBNull.Value Then
                    Dim ComDate As DateTime
                    ComDate = dr("Completed_Date")
                    TextCompleted_Date.Text = ComDate.ToShortDateString
                    'Session("Completed_Date") = ComDate.ToShortDateString
                End If

                If dr("Delivery_Date") IsNot DBNull.Value Then
                    Dim DeliveryDate As DateTime
                    DeliveryDate = dr("Delivery_Date")
                    TextDelivery_Date.Text = DeliveryDate.ToShortDateString
                End If

                If dr("rec_datetime") IsNot DBNull.Value Then
                    Dim recDatetime As DateTime
                    recDatetime = dr("rec_datetime")
                    TextRec_datetime.Text = recDatetime.ToShortDateString
                End If

                If dr("denomi") IsNot DBNull.Value Then
                    Dim denomi As String = ""
                    denomi = dr("denomi")
                    DropListDenomination.Items.Clear()
                    With DropListDenomination
                        If denomi = "" Then
                            .Items.Add("select Denomination")
                        Else
                            .Items.Add(denomi)
                        End If
                        If denomi <> "cash" Then
                            .Items.Add("cash")
                        End If
                        If denomi <> "card" Then
                            .Items.Add("card")
                        End If
                        If denomi <> "no claim" Then
                            .Items.Add("no claim")
                        End If
                        If denomi <> "resept only" Then
                            .Items.Add("resept only")
                        End If
                    End With
                End If

                If dr("rec_yuser") IsNot DBNull.Value Then
                    TextRec_yuser.Text = dr("rec_yuser")
                End If

                If dr("Part_1") IsNot DBNull.Value Then
                    TextParts1.Text = dr("Part_1")
                End If

                If dr("Qty_1") IsNot DBNull.Value Then
                    TextQtyParts1.Text = dr("Qty_1")
                End If

                If dr("Unit_Price_1") IsNot DBNull.Value Then
                    Dim unitPrice1 As String
                    unitPrice1 = dr("Unit_Price_1")
                    lblGPricePa1.Text = clsSet.setINR(unitPrice1)
                End If

                If dr("Part_2") IsNot DBNull.Value Then
                    TextParts2.Text = dr("Part_2")
                End If

                If dr("Qty_2") IsNot DBNull.Value Then
                    TextQtyParts2.Text = dr("Qty_2")
                End If

                If dr("Unit_Price_2") IsNot DBNull.Value Then
                    Dim unitPrice2 As String
                    unitPrice2 = dr("Unit_Price_2")
                    lblGPricePa2.Text = clsSet.setINR(unitPrice2)
                End If

                If dr("Part_3") IsNot DBNull.Value Then
                    TextParts3.Text = dr("Part_3")
                End If

                If dr("Qty_3") IsNot DBNull.Value Then
                    TextQtyParts3.Text = dr("Qty_3")
                End If

                If dr("Unit_Price_3") IsNot DBNull.Value Then
                    Dim unitPrice3 As String
                    unitPrice3 = dr("Unit_Price_3")
                    lblGPricePa3.Text = clsSet.setINR(unitPrice3)
                End If

                If dr("Part_4") IsNot DBNull.Value Then
                    TextParts4.Text = dr("Part_4")
                End If

                If dr("Qty_4") IsNot DBNull.Value Then
                    TextQtyParts4.Text = dr("Qty_4")
                End If

                If dr("Unit_Price_4") IsNot DBNull.Value Then
                    Dim unitPrice4 As String
                    unitPrice4 = dr("Unit_Price_4")
                    lblGPricePa4.Text = clsSet.setINR(unitPrice4)
                End If

                If dr("Part_5") IsNot DBNull.Value Then
                    TextParts5.Text = dr("Part_5")
                End If

                If dr("Qty_5") IsNot DBNull.Value Then
                    TextQtyParts5.Text = dr("Qty_5")
                End If

                If dr("Unit_Price_5") IsNot DBNull.Value Then
                    Dim unitPrice5 As String
                    unitPrice5 = dr("Unit_Price_5")
                    lblGPricePa5.Text = clsSet.setINR(unitPrice5)
                End If

                If dr("Labor_No") IsNot DBNull.Value Then
                    TextLaborAmount.Text = dr("Labor_No")
                End If

                If dr("Labor_Qty") IsNot DBNull.Value Then
                    TextQtyLabor.Text = dr("Labor_Qty")
                End If

                If dr("Labor_Amount") IsNot DBNull.Value Then
                    Dim LaborAmount As String
                    LaborAmount = dr("Labor_Amount")
                    lblGPriceLa.Text = clsSet.setINR(LaborAmount)
                End If

                If dr("Other") IsNot DBNull.Value Then
                    TextOther.Text = dr("Other")
                End If

                If dr("Other_Qty") IsNot DBNull.Value Then
                    TextQtyOther.Text = dr("Other_Qty")
                End If

                If dr("Other_Price") IsNot DBNull.Value Then
                    Dim OtherPrice As String
                    OtherPrice = dr("Other_Price")
                    lblGPriceOther.Text = clsSet.setINR(OtherPrice)
                End If

                If dr("Freight") IsNot DBNull.Value Then
                    TextShipMent.Text = dr("Freight")
                End If

                If dr("Freight_Qty") IsNot DBNull.Value Then
                    TextQtyShipMent.Text = dr("Freight_Qty")
                End If

                If dr("Freight_Price") IsNot DBNull.Value Then
                    Dim FreightPrice As String
                    FreightPrice = dr("Freight_Price")
                    lblGPriceShip.Text = clsSet.setINR(FreightPrice)
                End If

                If dr("Other_Freight_Amount") IsNot DBNull.Value Then
                    Dim OtherFreightAmount As String
                    OtherFreightAmount = dr("Other_Freight_Amount")
                    lblGOtherSum.Text = clsSet.setINR(OtherFreightAmount)
                End If

                If dr("Parts_Amount") IsNot DBNull.Value Then
                    Dim PartsAmount As String
                    PartsAmount = dr("Parts_Amount")
                    lblGPartsSum.Text = clsSet.setINR(PartsAmount)
                End If

                If dr("SGST") IsNot DBNull.Value Then
                    Dim SGST As String
                    SGST = dr("SGST")
                    lblUTSGTLa.Text = clsSet.setINR(SGST)
                End If

                If dr("IGST") IsNot DBNull.Value Then
                    Dim IGST As String
                    IGST = dr("IGST")
                    lblIGSTLa.Text = clsSet.setINR(IGST)
                End If

                If dr("CGST") IsNot DBNull.Value Then
                    Dim CGST As String
                    CGST = dr("CGST")
                    lblCGSTLa.Text = clsSet.setINR(CGST)
                End If

                If dr("Parts_SGST") IsNot DBNull.Value Then
                    Dim PartsSGST As String
                    PartsSGST = dr("Parts_SGST")
                    lblUTSGTPa.Text = clsSet.setINR(PartsSGST)
                End If

                If dr("Parts_IGST") IsNot DBNull.Value Then
                    Dim PartsIGST As String
                    PartsIGST = dr("Parts_IGST")
                    lblIGSTPa.Text = clsSet.setINR(PartsIGST)
                End If

                If dr("Parts_CGST") IsNot DBNull.Value Then
                    Dim PartsCGST As String
                    PartsCGST = dr("Parts_CGST")
                    lblCGSTPa.Text = clsSet.setINR(PartsCGST)
                End If

                If dr("Other_Freight_SGST") IsNot DBNull.Value Then
                    Dim OtherFreightSGST As String
                    OtherFreightSGST = dr("Other_Freight_SGST")
                    lblUTSGTO.Text = clsSet.setINR(OtherFreightSGST)
                End If

                If dr("Other_Freight_CGST") IsNot DBNull.Value Then
                    Dim OtherFreightCGST As String
                    OtherFreightCGST = dr("Other_Freight_CGST")
                    lblCGSTO.Text = clsSet.setINR(OtherFreightCGST)
                End If

                If dr("Other_Freight_IGST") IsNot DBNull.Value Then
                    Dim OtherFreightIGST As String
                    OtherFreightIGST = dr("Other_Freight_IGST")
                    lblIGSTO.Text = clsSet.setINR(OtherFreightIGST)
                End If

                If dr("Total_Invoice_Amount") IsNot DBNull.Value Then
                    Dim TotalInvoiceAmount As String
                    TotalInvoiceAmount = dr("Total_Invoice_Amount")
                    lblSumSales.Text = clsSet.setINR(TotalInvoiceAmount)
                End If

                If dr("amount") IsNot DBNull.Value Then
                    Dim amount As String
                    amount = dr("amount")
                    lblTotalAmount.Text = clsSet.setINR(amount)
                End If

            End If

            Session("ds_repair1") = dsT_repair1

        End If

    End Sub

    Protected Sub showMsg(ByVal Msg As String, ByVal msgChk As String)

        lblMsg.Text = Msg
        Dim sScript As String

        If msgChk = "CancelMsg" Then
            'OKとキャンセルボタン
            sScript = "$(function () {$(""#dialog"" ).dialog({width: 400,buttons:{""OK"": function () {$(this).dialog('close');$('[id$=""BtnOK""]').click();},""CANCEL"": function () {$(this).dialog('close');$('[id$=""BtnCancel""]').click();}}});});"
        ElseIf msgChk = "CancelMsg2" Then
            'OKとキャンセルボタン 
            sScript = "$(function () {$(""#dialog"" ).dialog({width: 400,buttons:{""OK"": function () {$(this).dialog('close');$('[id$=""Btn2OK""]').click();},""CANCEL"": function () {$(this).dialog('close');$('[id$=""BtnCancel""]').click();}}});});"
        ElseIf msgChk = "Last" Then
            'OKボタンのみ　完了OK ⇒画面遷移
            sScript = "$(function () {$(""#dialog"" ).dialog({width: 400,buttons:{""OK"": function () {$(this).dialog('close');$('[id$=""BtnLastOK""]').click();}}});});"
        ElseIf msgChk = "Last2" Then
            'OKボタンのみ　完了OK ⇒PDF作成
            sScript = "$(function () {$(""#dialog"" ).dialog({width: 400,buttons:{""OK"": function () {$(this).dialog('close');$('[id$=""Btn2LastOK""]').click();}}});});"
        Else
            'OKボタンのみ
            sScript = "$(function () {$(""#dialog"" ).dialog({width: 400, buttons:{""OK"": function () {$(this).dialog('close');}}});});"
        End If

        'JavaScriptの埋め込み
        ClientScript.RegisterStartupScript(Me.GetType(), "startup", sScript, True)

    End Sub

    'ProductTypeをリストにセット
    Protected Sub setListProductType(ByVal poM As String, ByVal ProductType As String, ByRef errMsg As String)

        Dim errFlg As Integer
        Dim dsMgroup As New DataSet
        Dim sqlStr As String

        sqlStr = "SELECT * FROM dbo.M_group WHERE DELFG = 0 AND group_no = '4' AND item_1 = '" & poM & "';"
        dsMgroup = DBCommon.Get_DS(sqlStr, errFlg)

        If errFlg = 1 Then
            errMsg = "Failed to acquire M_group information."
            Exit Sub
        End If

        If ProductType = "" Then

            If dsMgroup Is Nothing Then
                errMsg = "The manufacturer information does not exist."
                Exit Sub
            Else

                If poM <> "SS" Then
                    DropListProductType.Items.Add("select product type")
                End If

                If dsMgroup.Tables(0).Rows.Count <> 0 Then
                    For i = 0 To dsMgroup.Tables(0).Rows.Count - 1
                        Dim dr As DataRow = dsMgroup.Tables(0).Rows(i)
                        With DropListProductType
                            If dr("item_2") IsNot DBNull.Value Then
                                .Items.Add(dr("item_2"))
                            End If
                        End With
                    Next i
                End If

            End If

        Else

            DropListProductType.Items.Add(ProductType)

            If poM <> "SS" Then
                If dsMgroup.Tables(0).Rows.Count <> 0 Then
                    For i = 0 To dsMgroup.Tables(0).Rows.Count - 1
                        Dim dr As DataRow = dsMgroup.Tables(0).Rows(i)
                        With DropListProductType
                            If dr("item_2") IsNot DBNull.Value Then
                                If ProductType <> dr("item_2") Then
                                    .Items.Add(dr("item_2"))
                                End If
                            End If
                        End With
                    Next i
                End If
            End If

        End If

    End Sub
    '州情報をリストにセット
    Protected Sub setListPostal(ByVal posCord As String, ByVal stateName As String, ByRef errMsg As String)

        Dim strSQL As String = ""
        Dim errFlg As Integer
        Dim dsM_Postal As New DataSet
        Dim errMsg2 As String = ""

        DropListState.Items.Clear()

        strSQL = "SELECT DISTINCT state FROM dbo.M_Postal;"

        dsM_Postal = DBCommon.Get_DS(strSQL, errFlg)

        If errFlg = 1 Then
            errMsg = "DB(M_Posta)Failed to get information"
            Exit Sub
        End If

        If posCord = "" Then

            If stateName = "" Then

                '■郵便番号情報なし：全ての州を表示
                If dsM_Postal IsNot Nothing Then
                    DropListState.Items.Add("select state")
                    If dsM_Postal.Tables(0).Rows.Count <> 0 Then
                        For i = 0 To dsM_Postal.Tables(0).Rows.Count - 1
                            Dim dr As DataRow = dsM_Postal.Tables(0).Rows(i)
                            With DropListState
                                .Items.Add(dr("state"))
                            End With
                        Next i
                    End If
                Else
                    errMsg = "Postal information does not exist."
                    Exit Sub
                End If

            Else

                '■州名称のみあり：1行目に、登録済の州を表示　２行目以下は全ての州を表示
                If dsM_Postal IsNot Nothing Then
                    DropListState.Items.Add(stateName)
                    If errMsg2 <> "" Then
                        errMsg = errMsg2
                        Exit Sub
                    End If
                    If dsM_Postal.Tables(0).Rows.Count <> 0 Then
                        For i = 0 To dsM_Postal.Tables(0).Rows.Count - 1
                            Dim dr As DataRow = dsM_Postal.Tables(0).Rows(i)
                            With DropListState
                                If stateName <> dr("state") Then
                                    .Items.Add(dr("state"))
                                End If
                            End With
                        Next i
                    End If
                Else
                    errMsg = "Postal information does not exist."
                    Exit Sub
                End If

            End If

        Else

            '■郵便番号のみ：1行目に、登録済の州を表示　２行目以下は全ての州を表示
            If stateName = "" Then

                posCord = Left(posCord, 2)

                strSQL = "SELECT * FROM dbo.M_Postal WHERE zip_code = '" & posCord & "';"

                Dim DT_M_Postal As DataTable = DBCommon.ExecuteGetDT(strSQL, errFlg)

                If errFlg = 1 Then
                    errMsg = "DB(M_Posta)Failed to get information"
                    Exit Sub
                End If

                If DT_M_Postal IsNot Nothing Then

                    If DT_M_Postal.Rows(0)("state") IsNot DBNull.Value Then
                        DropListState.Items.Add(DT_M_Postal.Rows(0)("state"))
                        If errMsg2 <> "" Then
                            errMsg = errMsg2
                            Exit Sub
                        End If
                    Else
                        errMsg = "The state of the specified code does not exist."
                        Exit Sub
                    End If

                    If dsM_Postal.Tables(0).Rows.Count <> 0 Then
                        For i = 0 To dsM_Postal.Tables(0).Rows.Count - 1
                            Dim dr As DataRow = dsM_Postal.Tables(0).Rows(i)
                            With DropListState
                                If DT_M_Postal.Rows(0)("state") <> dr("state") Then
                                    .Items.Add(dr("state"))
                                End If
                            End With
                        Next i
                    End If
                Else
                    errMsg = "The specified code does not exist."
                End If

            End If

        End If

    End Sub
    'sendボタン押下処理
    Protected Sub btnSend_Click(sender As Object, e As ImageClickEventArgs) Handles btnSend.Click

        Dim calBtnCount As Integer = Session("cal_BtnCount")
        Dim otherData As Class_money.OTHER_DATA

        If calBtnCount = 0 And DropListDenomination.Text <> "resept only" Then
            Call showMsg("Is calculation unnecessary? <br />Yes: OK <br />NO: CANCEL", "CancelMsg2")
            Exit Sub
        Else
            '部品情報計算処理
            If DropListDenomination.Text <> "resept only" Then
                Dim errMsg As String
                Call Calculation(otherData, errMsg)
                If errMsg <> "" Then
                    showMsg(errMsg, "")
                    Exit Sub
                End If
            Else
                TextComment2.Focus()
            End If

            '登録処理
            Call touroku(otherData)

        End If

    End Sub

    Protected Sub reSetSession()

        Session("other_Data") = Nothing
        Session("poNo_Max") = Nothing
        Session("po_M") = Nothing
        Session("po_No_NoGSPN") = Nothing
        'Session("Completed_Date") = Nothing
        Session("cal_BtnCount") = 0
        Session("ds_repair1") = Nothing

    End Sub
    'startボタン押下処理
    Protected Sub btnStart_Click(sender As Object, e As ImageClickEventArgs) Handles btnStart.Click

        '***セッション情報取得***
        Dim userid As String = Session("user_id")
        Dim userName As String = Session("user_Name")
        Dim shipCode As String = Session("ship_code")
        Dim poNo As String = Session("po_No_NoGSPN")
        'Dim CompletedDate As String = Session("Completed_Date")
        Dim userLevel As String = Session("user_level")
        Dim adminFlg As Boolean = Session("admin_Flg")

        If Session("user_id") Is Nothing Then
            Call showMsg("The session is clear. Please login again.", "")
            Exit Sub
        End If

        '***リスト等の初期化****
        DropListProductType.Items.Clear()

        'テキストクリア
        Call reSet()

        '登録モード
        Call RegistrationMode()
        lblMode.Text = "Registration mode"

        btnCalculation.Enabled = True
        TextPo.Enabled = False

        '***ユーザ情報取得  時間とログイン名を表示****
        'ログイン名表示
        lblName.Text = userName

        '時間を表示
        Dim clsSetCommon As New Class_common
        Dim dtNow As DateTime = clsSetCommon.dtIndia
        lblRecord.Text = dtNow.ToString("yyyyMMddHHmmss")

        '***管理情報表示***
        Call showData(poNo)

        '***リストの設定***
        '金種
        If DropListDenomination.Text = "" Then
            With DropListDenomination
                .Items.Add("select Denomination")
                .Items.Add("cash")
                .Items.Add("card")
                .Items.Add("no claim")
                .Items.Add("resept only")
            End With
        End If

        '保証
        If DropListWarranty.Text = "" Then
            With DropListWarranty
                .Items.Add("select warranty")
                .Items.Add("in warranty")
                .Items.Add("out of warranty")
                .Items.Add("other")
            End With
        End If

        '***ログインユーザの登録可能チェック***
        '完了日に記載がある
        If TextCompleted_Date.Text <> "" Then

            If (userLevel = "0" Or userLevel = "1" Or userLevel = "2" Or adminFlg = True) Then
                'ユーザ権限あるが、完了日が当月以外は変更不可
                If (Left(TextCompleted_Date.Text, 7) <> Left(dtNow.ToShortDateString, 7)) Then
                    Call showMsg("Please check the completion date", "")
                    Exit Sub
                End If
            Else
                'ユーザ権限ない
                Call showMsg("You do not have permission to change.", "")
                Exit Sub
            End If

        Else

            '権限なしユーザは入力できる項目を制御
            If (userLevel = "0" Or userLevel = "1" Or userLevel = "2" Or adminFlg = True) Then
            Else
                TextRepair_Received_Date.Enabled = False
                TextConsumer_Name.Enabled = False
                TextPostal_Code2.Enabled = False
                DropListState.Enabled = False
                TextConsumer_Addr1.Enabled = False
                TextConsumer_Addr2.Enabled = False
                TextCustomer_mail_address.Enabled = False
                TextConsumer_Telephone.Enabled = False
                TextConsumer_Fax.Enabled = False
                TextRepair_Description.Enabled = False
            End If

        End If

        btnStart.Enabled = False
        btnSend.Enabled = True

        'Session("Completed_Date") = Nothing

    End Sub

    Protected Sub reSet()

        TextComment.Text = ""
        TextComment2.Text = ""
        TextConsumer_Name.Text = ""
        TextConsumer_Addr1.Text = ""
        TextConsumer_Addr2.Text = ""
        TextCustomer_mail_address.Text = ""
        TextConsumer_Telephone.Text = ""
        TextConsumer_Fax.Text = ""
        TextPostal_Code2.Text = ""
        TextModel.Text = ""
        TextSerial_No.Text = ""
        TextIMEI_No.Text = ""
        TextRepair_Description.Text = ""
        TextRepair_Received_Date.Text = ""
        TextMaker.Text = ""
        DropListState.Items.Clear()

    End Sub

    '郵便番号よりstate情報取得
    Protected Sub btnPostal_Click(sender As Object, e As EventArgs) Handles btnPostal.Click

        Dim PostalCode As String = Trim(TextPostal_Code2.Text)
        Dim strSQL As String = ""
        Dim errMsg As String = ""
        Dim errFlg As Integer

        DropListState.Items.Clear()

        If PostalCode = "" Then

            Call setListPostal("", "", errMsg)
            If errMsg <> "" Then
                Call showMsg(errMsg, "")
                Exit Sub
            End If

        Else

            PostalCode = Left(PostalCode, 2)

            strSQL = "SELECT * FROM dbo.M_Postal WHERE zip_code = '" & PostalCode & "';"

            Dim DT_M_Postal As DataTable = DBCommon.ExecuteGetDT(strSQL, errFlg)

            If errFlg = 1 Then
                Call showMsg("DB(M_Posta)Failed to get information", "")
                Exit Sub
            End If

            If DT_M_Postal IsNot Nothing Then
                If DT_M_Postal.Rows(0)("state") IsNot DBNull.Value Then
                    DropListState.Items.Add(DT_M_Postal.Rows(0)("state"))
                    TextPostal_Code2.Text = PostalCode
                    If DT_M_Postal.Rows(0)("state") = "" Then
                        Call showMsg("The state name for the code does not exist.", "")
                        Exit Sub
                    End If
                Else
                    Call showMsg("The state of the specified code does not exist.", "")
                    Exit Sub
                End If
            Else
                Call showMsg("The specified code does not exist.", "")
                Exit Sub
            End If

        End If

    End Sub

    '税金種別をチェック
    Protected Sub chkApply(ByVal PostalName As String, ByRef taxKind As String, ByRef errMsg As String)

        '***セッション情報取得***
        Dim userid As String = Session("user_id")
        Dim shipCode As String = Session("ship_code")

        If shipCode Is Nothing Then
            errMsg = "The session has expired. Please login again."
            Exit Sub
        End If

        Dim strSQL As String
        Dim errFlg As Integer
        Dim item_2 As String '拠点の州名称
        Dim clsSet As New Class_money

        '州名の表示があり
        If PostalName <> "" Then

            strSQL = "Select * FROM dbo.M_ship_base WHERE DELFG = 0 And ship_code = '" & shipCode & "';"

            Dim DT_M_ship_base As DataTable = DBCommon.ExecuteGetDT(strSQL, errFlg)

            If errFlg = 1 Then
                errMsg = "DB(M_ship_base)Failed to get information"
                Exit Sub
            End If

            If DT_M_ship_base IsNot Nothing Then
                If DT_M_ship_base.Rows(0)("item_2") IsNot DBNull.Value Then
                    '拠点の州名称
                    item_2 = DT_M_ship_base.Rows(0)("item_2")
                Else
                    errMsg = "Since the state name of the base does not exist, tax type can not be confirmed."
                    Exit Sub
                End If
            Else
                errMsg = "Because there is no location information, tax type can not be confirmed."
                Exit Sub
            End If

            If item_2 = "" Then
                errMsg = "Since the state name of the branch does not exist, tax type can not be confirmed."
                Exit Sub
            End If

            If PostalName = item_2 Then
                '拠点が同じ
                taxKind = "SGST"
            Else
                '拠点が違う
                taxKind = "IGST"
            End If

        End If

    End Sub
    '計算ボタン押下処理
    Protected Sub btnCalculation_Click(sender As Object, e As EventArgs) Handles btnCalculation.Click

        'ボタン押下カウント
        Dim calBtnCount As Integer = Session("cal_BtnCount")
        calBtnCount = calBtnCount + 1

        If DropListDenomination.Text <> "resept only" Then
            Dim errMsg As String
            Call Calculation(Nothing, errMsg)
            If errMsg <> "" Then
                showMsg(errMsg, "")
            End If
        Else
            TextComment2.Focus()
            Call reSet2()
        End If

        Session("cal_BtnCount") = calBtnCount

    End Sub

    Protected Sub reSet2()

        '■作業費情報
        '作業費用
        lblPriceLabor.Text = "0.00"
        lblGPriceLa.Text = "0.00"

        '作業費の税金
        lblUTSGTLa.Text = "0.00"
        lblIGSTLa.Text = "0.00"
        lblCGSTLa.Text = "0.00"

        '作業費合計
        lblSum1.Text = "0.00"

        '■部品情報
        '部品料金
        lblPricePa1.Text = "0.00"
        lblPricePa2.Text = "0.00"
        lblPricePa3.Text = "0.00"
        lblPricePa4.Text = "0.00"
        lblPricePa5.Text = "0.00"

        lblGPricePa1.Text = "0.00"
        lblGPricePa2.Text = "0.00"
        lblGPricePa3.Text = "0.00"
        lblGPricePa4.Text = "0.00"
        lblGPricePa5.Text = "0.00"

        '部品料金のみ合計
        lblPartsSum.Text = "0.00"
        lblGPartsSum.Text = "0.00"

        '部品それぞれの税金
        lblp1SGST.Text = "0.00"
        lblp1CGST.Text = "0.00"
        lblp1IGST.Text = "0.00"

        lblp2SGST.Text = "0.00"
        lblp2CGST.Text = "0.00"
        lblp2IGST.Text = "0.00"

        lblp3SGST.Text = "0.00"
        lblp3CGST.Text = "0.00"
        lblp3IGST.Text = "0.00"

        lblp4SGST.Text = "0.00"
        lblp4CGST.Text = "0.00"
        lblp4IGST.Text = "0.00"

        lblp5SGST.Text = "0.00"
        lblp5CGST.Text = "0.00"
        lblp5IGST.Text = "0.00"

        '部品料金の税金のみ合計
        lblUTSGTPa.Text = "0.00"
        lblIGSTPa.Text = "0.00"
        lblCGSTPa.Text = "0.00"

        '部品それぞれの合計
        lblSum2_1.Text = "0.00"
        lblSum2_2.Text = "0.00"
        lblSum2_3.Text = "0.00"
        lblSum2_4.Text = "0.00"
        lblSum2_5.Text = "0.00"

        '■その他費用
        lblPriceOther.Text = "0.00"
        lblGPriceOther.Text = "0.00"

        'その他費用の税金
        lbloSGST.Text = "0.00"
        lbloCGST.Text = "0.00"
        lbloIGST.Text = "0.00"

        'その他費用合計（税込み）
        lblSumO.Text = "0.00"

        '■郵送費用
        lblPriceShip.Text = "0.00"
        lblGPriceShip.Text = "0.00"

        '郵送費用の税金
        lblsSGST.Text = "0.00"
        lblsCGST.Text = "0.00"
        lblsIGST.Text = "0.00"

        '郵送費用合計（税込み）
        lblSumS.Text = "0.00"

        '■郵送費、その他費用合計
        lblOtherSum.Text = "0.00"
        lblGOtherSum.Text = "0.00"
        lblUTSGTO.Text = "0.00"
        lblCGSTO.Text = "0.00"
        lblIGSTO.Text = "0.00"
        lblSum4.Text = "0.00"

        '■total
        '部品①～⑤合計（税込み）
        lblSumPa.Text = "0.00"

        'それぞれ列の合計
        lblSumCost.Text = "0.00"
        lblSumSales.Text = "0.00"
        lblSumSGST.Text = "0.00"
        lblSumCGST.Text = "0.00"
        lblSumIGST.Text = "0.00"
        lblTotal.Text = "0.00"

        '個数
        lblQtySum1.Text = "0"
        lblQtySum2.Text = "0"

        '請求金額
        lblTotalAmount.Text = "0.00"

        '■入力欄をデフォルトの色
        TextLaborAmount.BackColor = System.Drawing.Color.FromArgb(255, 255, 255)
        TextParts1.BackColor = System.Drawing.Color.FromArgb(255, 255, 255)
        TextParts2.BackColor = System.Drawing.Color.FromArgb(255, 255, 255)
        TextParts3.BackColor = System.Drawing.Color.FromArgb(255, 255, 255)
        TextParts4.BackColor = System.Drawing.Color.FromArgb(255, 255, 255)
        TextParts5.BackColor = System.Drawing.Color.FromArgb(255, 255, 255)
        TextShipMent.BackColor = System.Drawing.Color.FromArgb(255, 255, 255)
        TextOther.BackColor = System.Drawing.Color.FromArgb(255, 255, 255)
        TextQtyLabor.BackColor = System.Drawing.Color.FromArgb(255, 255, 255)
        TextQtyParts1.BackColor = System.Drawing.Color.FromArgb(255, 255, 255)
        TextQtyParts2.BackColor = System.Drawing.Color.FromArgb(255, 255, 255)
        TextQtyParts3.BackColor = System.Drawing.Color.FromArgb(255, 255, 255)
        TextQtyParts4.BackColor = System.Drawing.Color.FromArgb(255, 255, 255)
        TextQtyParts5.BackColor = System.Drawing.Color.FromArgb(255, 255, 255)
        TextQtyOther.BackColor = System.Drawing.Color.FromArgb(255, 255, 255)
        TextQtyShipMent.BackColor = System.Drawing.Color.FromArgb(255, 255, 255)

        If DropListDenomination.Text = "resept only" Then
            TextLaborAmount.Text = ""
            TextParts1.Text = ""
            TextParts2.Text = ""
            TextParts3.Text = ""
            TextParts4.Text = ""
            TextParts5.Text = ""
            TextShipMent.Text = ""
            TextOther.Text = ""
            TextQtyLabor.Text = ""
            TextQtyParts1.Text = ""
            TextQtyParts2.Text = ""
            TextQtyParts3.Text = ""
            TextQtyParts4.Text = ""
            TextQtyParts5.Text = ""
            TextQtyOther.Text = ""
            TextQtyShipMent.Text = ""
        End If

    End Sub
    '登録モード
    Protected Sub RegistrationMode()

        TextPo.Enabled = True
        TextComment.Enabled = True
        TextComment2.Enabled = True
        TextConsumer_Name.Enabled = True
        TextConsumer_Addr1.Enabled = True
        TextConsumer_Addr2.Enabled = True
        TextCustomer_mail_address.Enabled = True
        TextConsumer_Telephone.Enabled = True
        TextConsumer_Fax.Enabled = True
        TextPostal_Code2.Enabled = True
        TextModel.Enabled = True
        TextSerial_No.Enabled = True
        TextIMEI_No.Enabled = True
        TextRepair_Description.Enabled = True
        TextRepair_Received_Date.Enabled = True
        TextMaker.Enabled = True
        DropListState.Enabled = True
        DropListWarranty.Enabled = True
        DropListProductType.Enabled = True
        DropListDenomination.Enabled = True
        TextCompleted_Date.Enabled = True
        TextDelivery_Date.Enabled = True
        TextRec_datetime.Enabled = True
        TextRec_yuser.Enabled = True
        Call RegistrationModesParts()

    End Sub
    '登録モード
    Protected Sub RegistrationModesParts()

        TextLaborAmount.Enabled = True
        TextParts1.Enabled = True
        TextParts2.Enabled = True
        TextParts3.Enabled = True
        TextParts4.Enabled = True
        TextParts5.Enabled = True
        TextQtyLabor.Enabled = True
        TextQtyParts1.Enabled = True
        TextQtyParts2.Enabled = True
        TextQtyParts3.Enabled = True
        TextQtyParts4.Enabled = True
        TextQtyParts5.Enabled = True
        TextShipMent.Enabled = True
        TextQtyShipMent.Enabled = True
        TextOther.Enabled = True
        TextQtyOther.Enabled = True

    End Sub
    '閲覧モード
    Protected Sub showMode()

        TextPo.Enabled = False
        TextComment.Enabled = False
        TextComment2.Enabled = False
        TextConsumer_Name.Enabled = False
        TextConsumer_Addr1.Enabled = False
        TextConsumer_Addr2.Enabled = False
        TextCustomer_mail_address.Enabled = False
        TextConsumer_Telephone.Enabled = False
        TextConsumer_Fax.Enabled = False
        TextPostal_Code2.Enabled = False
        TextModel.Enabled = False
        TextSerial_No.Enabled = False
        TextIMEI_No.Enabled = False
        TextRepair_Description.Enabled = False
        TextRepair_Received_Date.Enabled = False
        TextMaker.Enabled = False
        DropListState.Enabled = False
        DropListWarranty.Enabled = False
        DropListProductType.Enabled = False
        DropListDenomination.Enabled = False
        TextCompleted_Date.Enabled = False
        TextDelivery_Date.Enabled = False
        TextRec_datetime.Enabled = False
        TextRec_yuser.Enabled = False

        Call showModeParts()

    End Sub
    '閲覧モード
    Protected Sub showModeParts()

        TextLaborAmount.Enabled = False
        TextParts1.Enabled = False
        TextParts2.Enabled = False
        TextParts3.Enabled = False
        TextParts4.Enabled = False
        TextParts5.Enabled = False
        TextQtyLabor.Enabled = False
        TextQtyParts1.Enabled = False
        TextQtyParts2.Enabled = False
        TextQtyParts3.Enabled = False
        TextQtyParts4.Enabled = False
        TextQtyParts5.Enabled = False
        TextShipMent.Enabled = False
        TextQtyShipMent.Enabled = False
        TextOther.Enabled = False
        TextQtyOther.Enabled = False

    End Sub
    '登録処理
    Protected Sub touroku(ByRef otherData As Class_money.OTHER_DATA)

        '***セッション情報取得***
        Dim userid As String = Session("user_id")
        Dim shipCode As String = Session("ship_code")
        Dim shipName As String = Session("ship_name")
        Dim userLevel As String = Session("user_level")
        Dim adminFlg As Boolean = Session("admin_Flg")
        Dim poNoMax As Long = Session("poNo_Max")
        Dim poM As String = Session("po_M")

        If userid Is Nothing Then
            Call showMsg("The session has expired. Please login again.", "")
            Exit Sub
        End If

        Dim levelFlg As String
        If userLevel = "0" Or userLevel = "1" Or userLevel = "2" Or adminFlg = True Then
            levelFlg = "1"
        End If

        Dim clsSet As New Class_money
        Dim errFlg As Integer
        Dim errFlgDt As Integer
        Dim errMsg As String = ""

        'PO番号
        Dim poNo As String = Trim(TextPo.Text)

        '***入力チェック***
        Dim ConsumerName As String = Trim(TextConsumer_Name.Text)
        Dim SerialNo As String = Trim(TextSerial_No.Text)

        If ConsumerName = "" Then
            Call showMsg("Please enter Consumer Name.", "")
            TextConsumer_Name.Focus()
            Exit Sub
        End If

        If SerialNo = "" Then
            Call showMsg("Please enter Serial No.", "")
            TextSerial_No.Focus()
            Exit Sub
        End If

        'post番号チェック
        Dim postCord As String = Trim(Left(TextPostal_Code2.Text, 2))
        Dim stateName As String = DropListState.Text

        If clsSet.chkPostCord(postCord, stateName, errMsg) = False Then
            If errMsg <> "" Then
                Call showMsg(errMsg, "")
                TextPostal_Code2.Focus()
                Exit Sub
            End If
            Call showMsg("Zip code and name do not match. <br /> 
                         Understand the code: Please press the mail button. <br /> Code unknown: Please leave the code blank.", "")
            TextPostal_Code2.Focus()
            Exit Sub
        End If

        '日付チェックと設定
        Dim dt As DateTime
        If DateTime.TryParse(Trim(TextRepair_Received_Date.Text), dt) Then
            otherData.Repair_Received_Date = DateTime.Parse(TextRepair_Received_Date.Text)
        Else
            If Trim(TextRepair_Received_Date.Text) <> "" Then
                Call showMsg("Received_Date:YYYY/MM/DD Please type in the date type.", "")
                Exit Sub
            Else
                otherData.Repair_Received_Date = ""
            End If
        End If

        If DateTime.TryParse(Trim(TextCompleted_Date.Text), dt) Then
            otherData.Completed_Date = DateTime.Parse(TextCompleted_Date.Text)
            If otherData.Repair_Received_Date > otherData.Completed_Date Then
                Call showMsg("On the completion date, you can not specify the past date from the reception date.", "")
                Exit Sub
            End If
        Else
            If Trim(TextCompleted_Date.Text) <> "" Then
                Call showMsg("Completed_Datet:YYYY/MM/DD Please type in the date type.", "")
                Exit Sub
            Else
                otherData.Completed_Date = ""
            End If
        End If

        If DateTime.TryParse(Trim(TextDelivery_Date.Text), dt) Then
            otherData.Delivery_Date = DateTime.Parse(TextDelivery_Date.Text)
        Else
            If Trim(TextDelivery_Date.Text) <> "" Then
                Call showMsg("Delivery_Date:YYYY/MM/DD Please type in the date type.", "")
                Exit Sub
            Else
                otherData.Delivery_Date = ""
            End If
        End If

        If DateTime.TryParse(Trim(TextRec_datetime.Text), dt) Then
            otherData.rec_datetime = DateTime.Parse(TextRec_datetime.Text)
        Else
            If Trim(TextRec_datetime.Text) <> "" Then
                Call showMsg("Rec_datetime:YYYY/MM/DD Please type in the date type.", "")
                Exit Sub
            Else
                otherData.rec_datetime = ""
            End If
        End If

        '金種チェック
        If ChkEsdtimate.Checked = True Or otherData.Completed_Date <> "" Then

            '※resept only（受付のみは集計にはいらない。）
            If DropListDenomination.Text <> "resept only" Then

                If DropListWarranty.Text = "select warranty" Or DropListWarranty.Text = "" Then
                    Call showMsg("Please select warranty.", "")
                    DropListWarranty.Focus()
                    Exit Sub
                Else
                    If (DropListWarranty.Text = "in warranty") And (DropListDenomination.Text <> "no claim") Then
                        Call showMsg("in warranty is selected.<br/>Denomination is not selected as no claim.", "")
                        Exit Sub
                    End If

                    If (DropListWarranty.Text = "out of warranty") And (DropListDenomination.Text <> "cash" AndAlso DropListDenomination.Text <> "card") Then
                        Call showMsg("Out of warranty is selected.<br/>Denomination is not selected for cash or card.", "")
                        Exit Sub
                    End If

                    If (DropListWarranty.Text = "other") And (DropListDenomination.Text <> "cash" AndAlso DropListDenomination.Text <> "card") Then
                        Call showMsg("Other is selected.<br/>Denomination is not selected for cash or card.", "")
                        Exit Sub
                    End If

                End If

            End If

            'denomi
            If DropListDenomination.Text = "select Denomination" Or DropListDenomination.Text = "" Then
                TextComment2.Focus()
                Call showMsg("Please select Denomination.", "")
                Exit Sub
            End If

        End If

        'product typeチェック
        If DropListProductType.Text = "select product type" Or DropListProductType.Text = "" Then
            DropListProductType.Focus()
            Call showMsg("Please select product type.", "")
            Exit Sub
        End If

        '***入力値設定***
        Dim denomi As String
        If DropListDenomination.Text = "select Denomination" Then
            denomi = ""
        Else
            denomi = DropListDenomination.Text
        End If

        If DropListState.Text = "select state" Then
            stateName = ""
        Else
            stateName = DropListState.Text
        End If

        otherData.Product_Type = DropListProductType.Text
        otherData.warranty = DropListWarranty.Text
        otherData.denomi = denomi
        otherData.State_Name = stateName
        otherData.rec_yuser = Trim(TextRec_yuser.Text)
        otherData.comment = Trim(TextComment.Text)
        otherData.comment2 = Trim(TextComment2.Text)
        otherData.Consumer_Name = Trim(TextConsumer_Name.Text)
        otherData.Consumer_Addr1 = Trim(TextConsumer_Addr1.Text)
        otherData.Consumer_Addr2 = Trim(TextConsumer_Addr2.Text)
        otherData.Customer_mail_address = Trim(TextCustomer_mail_address.Text)
        otherData.Consumer_Telephone = Trim(TextConsumer_Telephone.Text)
        otherData.Consumer_Fax = Trim(TextConsumer_Fax.Text)
        otherData.Model = Trim(TextModel.Text)
        otherData.Serial_No = Trim(TextSerial_No.Text)
        otherData.IMEI_No = Trim(TextIMEI_No.Text)
        otherData.Repair_Description = Trim(TextRepair_Description.Text)
        otherData.Maker = Trim(TextMaker.Text)
        otherData.Postal_Code = postCord

        If DropListDenomination.Text <> "resept only" Then
            '部品情報
            otherData.Labor_No = Trim(TextLaborAmount.Text)
            otherData.Labor_Amount = Trim(lblGPriceLa.Text)
            otherData.Labor_Qty = Trim(TextQtyLabor.Text)
            otherData.Parts_Amount = Trim(lblGPartsSum.Text)
            otherData.Part_1 = Trim(TextParts1.Text)
            otherData.Qty_1 = Trim(TextQtyParts1.Text)
            otherData.GUnit_Price_1 = Trim(lblGPricePa1.Text)
            otherData.Part_2 = Trim(TextParts2.Text)
            otherData.Qty_2 = Trim(TextQtyParts2.Text)
            otherData.GUnit_Price_2 = Trim(lblGPricePa2.Text)
            otherData.Part_3 = Trim(TextParts3.Text)
            otherData.Qty_3 = Trim(TextQtyParts3.Text)
            otherData.GUnit_Price_3 = Trim(lblGPricePa3.Text)
            otherData.Part_4 = Trim(TextParts4.Text)
            otherData.Qty_4 = Trim(TextQtyParts4.Text)
            otherData.GUnit_Price_4 = Trim(lblGPricePa4.Text)
            otherData.Part_5 = Trim(TextParts5.Text)
            otherData.Qty_5 = Trim(TextQtyParts5.Text)
            otherData.GUnit_Price_5 = Trim(lblGPricePa5.Text)
            otherData.SGST = Trim(lblUTSGTLa.Text)
            otherData.IGST = Trim(lblIGSTLa.Text)
            otherData.CGST = Trim(lblCGSTLa.Text)
            otherData.Parts_SGST = Trim(lblUTSGTPa.Text)
            otherData.Parts_IGST = Trim(lblIGSTPa.Text)
            otherData.Parts_CGST = Trim(lblCGSTPa.Text)

            'その他
            otherData.Other_No = Trim(TextOther.Text)
            otherData.Other_Qty = Trim(TextQtyOther.Text)
            otherData.Other_Price = Trim(lblGPriceOther.Text)

            '郵送費
            otherData.ShipMent_No = Trim(TextShipMent.Text)
            otherData.ShipMent_Qty = Trim(TextQtyShipMent.Text)
            otherData.ShipMent_Price = Trim(lblGPriceShip.Text)

            'その他費用、郵送費合計
            otherData.Other_Freight_Amount = Trim(lblGOtherSum.Text)
            otherData.Other_Freight_SGST = Trim(lblUTSGTO.Text)
            otherData.Other_Freight_IGST = Trim(lblIGSTO.Text)
            otherData.Other_Freight_CGST = Trim(lblCGSTO.Text)

            otherData.amount = Trim(lblTotalAmount.Text)

            Dim tmp1, tmp2, tmp3 As Decimal

            If otherData.Labor_Amount = "" Then
                tmp1 = 0.00
            Else
                tmp1 = Convert.ToDecimal(otherData.Labor_Amount)
            End If

            If otherData.Parts_Amount = "" Then
                tmp2 = 0.00
            Else
                tmp2 = Convert.ToDecimal(otherData.Parts_Amount)
            End If

            If otherData.Other_Freight_Amount = "" Then
                tmp3 = 0.00
            Else
                tmp3 = Convert.ToDecimal(otherData.Other_Freight_Amount)
            End If

            otherData.Total_Invoice_Amount = (tmp1 + tmp2 + tmp3).ToString

            '***費用登録時のチェック***
            '見積にチェックあり、又は完了日に記載がある場合
            If ChkEsdtimate.Checked = True Or otherData.Completed_Date <> "" Then

                '作業費用必須
                If otherData.Labor_Amount = "" Or otherData.Labor_Amount = "0.00" Or otherData.Labor_Amount = "0" Or otherData.Labor_Amount Is Nothing Then
                    Call showMsg("Work costs are mandatory.", "")
                    Exit Sub
                End If

            End If

        Else
            otherData.Labor_Amount = "0.00"
            otherData.Parts_Amount = "0.00"
            otherData.GUnit_Price_1 = "0.00"
            otherData.GUnit_Price_2 = "0.00"
            otherData.GUnit_Price_3 = "0.00"
            otherData.GUnit_Price_4 = "0.00"
            otherData.GUnit_Price_5 = "0.00"
            otherData.SGST = "0.00"
            otherData.IGST = "0.00"
            otherData.CGST = "0.00"
            otherData.Parts_SGST = "0.00"
            otherData.Parts_IGST = "0.00"
            otherData.Parts_CGST = "0.00"
            otherData.Other_Price = "0.00"
            otherData.ShipMent_Price = "0.00"
            otherData.Other_Freight_Amount = "0.00"
            otherData.Other_Freight_SGST = "0.00"
            otherData.Other_Freight_IGST = "0.00"
            otherData.Other_Freight_CGST = "0.00"
            otherData.amount = "0.00"
            otherData.Total_Invoice_Amount = "0.00"
        End If

        'po
        otherData.po_no = Trim(TextPo.Text)

        '***変更箇所の確認***
        Dim dsT_repair1Chk As New DataSet

        If Session("ds_repair1") IsNot Nothing Then
            dsT_repair1Chk = Session("ds_repair1")
        End If

        If clsSet.updateChk(otherData, dsT_repair1Chk, errMsg) = True Then

            If errMsg <> "" Then
                Call showMsg(errMsg, "Last")
                Exit Sub
            End If

            If otherData.Completed_Date = "" Then
                If ChkEsdtimate.Checked = False Then
                    Call showMsg("Could not update it", "Last")
                    Exit Sub
                End If
            End If

        Else

            If errMsg <> "" Then
                Call showMsg(errMsg, "Last")
                Exit Sub
            End If

        End If

        '***報告書出力用に店舗情報を設定***

        Dim DT_M_ship_base As DataTable

            Dim strSQL As String = "SELECT * FROM dbo.M_ship_base WHERE DELFG = 0 and ship_code = '" & shipCode & "';"

            DT_M_ship_base = DBCommon.ExecuteGetDT(strSQL, errFlgDt)

            If errFlgDt = 1 Then
            Call showMsg("Failed to acquire information from M_ship_base.", "Last")
            Exit Sub
            End If

        If DT_M_ship_base IsNot Nothing Then

            If DT_M_ship_base.Rows(0)("ship_add1") IsNot DBNull.Value Then
                otherData.Ship_Addr1 = DT_M_ship_base.Rows(0)("ship_add1")
            End If

            If DT_M_ship_base.Rows(0)("e_mail") IsNot DBNull.Value Then
                otherData.Ship_Mail = DT_M_ship_base.Rows(0)("e_mail")
            End If

            If DT_M_ship_base.Rows(0)("ship_tel") IsNot DBNull.Value Then
                otherData.Ship_Tel = DT_M_ship_base.Rows(0)("ship_tel")
            End If

            If DT_M_ship_base.Rows(0)("GSTIN") IsNot DBNull.Value Then
                otherData.GSTIN = DT_M_ship_base.Rows(0)("GSTIN")
            End If

            If DT_M_ship_base.Rows(0)("ship_name") IsNot DBNull.Value Then
                otherData.ship_Name = DT_M_ship_base.Rows(0)("ship_name")
            End If

        End If

        '***登録処理***
        '受付登録済か確認
        Dim DT_T_repair1 As DataTable

        Dim strSQL2 As String = "SELECT * FROM dbo.T_repair1 WHERE po_no = '" & poNo & "' AND DELFG = 0 and Branch_Code = '" & shipCode & "';"

        DT_T_repair1 = DBCommon.ExecuteGetDT(strSQL2, errFlgDt)

        If errFlgDt = 1 Then
            Call showMsg("Failed to get information from T_repair 1.", "Last")
            Exit Sub
        End If

        If DT_T_repair1 IsNot Nothing Then

            '完了日が登録済であれば、上書き登録かを確認して、ロードで登録処理
            If DT_T_repair1.Rows(0)("Completed_Date") IsNot DBNull.Value Then

                If DT_T_repair1.Rows(0)("Consumer_Name") IsNot DBNull.Value Then
                    '登録用のセッション設定
                    Session("other_Data") = otherData
                    Session("poNo_Max") = poNoMax
                    Session("po_M") = poM
                    showMsg("Completed PO: " & poNo & "Exist. Do you want to continue?", "CancelMsg")
                    Exit Sub
                End If

            End If

            '完了日が登録済でない場合のデータ登録処理
            Dim tourokuFlg As Integer = -1
            Call clsSet.setCsvData2(Nothing, errFlg, otherData, Nothing, userid, shipCode, poM, poNoMax, "", levelFlg, tourokuFlg)

            If errFlg = 1 Then
                Call showMsg("Data registration failed.", "Last")
                Exit Sub
            Else

                '***PDF出力処理***
                Dim clsSetCommon As New Class_common
                Dim dtNow As DateTime = clsSetCommon.dtIndia
                Dim pdfFileName As String = dtNow.ToString("yyyyMMddHHmmss") & "_Estimate.pdf"
                Dim pdfFileNameWorkReport As String = dtNow.ToString("yyyyMMddHHmmss") & "_WorkReport.pdf"

                '見積書作成にチェックあり
                If ChkEsdtimate.Checked = True Then

                    If otherData.Completed_Date = "" Then

                        '見積書出力
                        Call clsSet.EstimatesPDF2(otherData, pdfFileName, errFlg, shipName)

                        If errFlg = 1 Then
                            'サーバに保存されたファイルを削除
                            Dim FilePath As String = clsSet.savePDFPass & pdfFileName
                            If FilePath <> "" Then
                                If System.IO.File.Exists(FilePath) = True Then
                                    System.IO.File.Delete(FilePath)
                                End If
                            End If
                            Call showMsg("Failed to create estimate.", "Last")
                            Exit Sub
                        End If

                        'メッセージ出力後、load処理で、ファイルダウンロード
                        Session("pdf_FileName") = pdfFileName
                        Call showMsg("Data registration and estimate preparation completed. <br/>After the download is completed, please return to the menu screen by pressing the return button.", "Last2")

                    Else

                        '完了日に記載があるので作業報告書出力
                        Call clsSet.WorkReportPDF(otherData, pdfFileNameWorkReport, errFlg, shipName)

                        If errFlg = 1 Then
                            'サーバに保存されたファイルを削除
                            Dim FilePath As String = clsSet.savePDFPass & pdfFileNameWorkReport
                            If FilePath <> "" Then
                                If System.IO.File.Exists(FilePath) = True Then
                                    System.IO.File.Delete(FilePath)
                                End If
                            End If
                            Call showMsg("Failed to create work report.", "Last")
                            Exit Sub
                        End If

                        'メッセージ出力後、load処理で、ファイルダウンロード
                        Session("pdf_FileName") = pdfFileNameWorkReport
                        Call showMsg("Data registration and report preparation completed.<br/>Please go back to the menu with the back button.", "Last2")

                    End If

                Else

                    '見積書作成にチェックなし

                    If otherData.Completed_Date <> "" Then

                        '作業報告書出力
                        Call clsSet.WorkReportPDF(otherData, pdfFileNameWorkReport, errFlg, shipName)

                        If errFlg = 1 Then
                            Dim FilePath As String = clsSet.savePDFPass & pdfFileNameWorkReport
                            If FilePath <> "" Then
                                If System.IO.File.Exists(FilePath) = True Then
                                    System.IO.File.Delete(FilePath)
                                End If
                            End If
                            Call showMsg("Failed to create work report.", "Last")
                            Exit Sub
                        End If

                        'メッセージ出力後、load処理で、ファイルダウンロード
                        Session("pdf_FileName") = pdfFileNameWorkReport
                        Call showMsg("Data registration and report preparation completed.<br/>Please go back to the menu with the back button.", "Last2")

                    Else
                        Call showMsg("Update processing is complete.", "Last")
                        Exit Sub
                    End If

                End If

            End If

        Else
            Call showMsg(poNo & " : Reception registration is not done.", "Last")
            Exit Sub
        End If

        '***後処理***
        btnStart.Enabled = False
        btnSend.Enabled = False

    End Sub

    Protected Sub BtnCancel_Click(sender As Object, e As EventArgs) Handles BtnCancel.Click

    End Sub

    Protected Sub Btn2OK_Click(sender As Object, e As EventArgs) Handles Btn2OK.Click

    End Sub
    '計算    
    Protected Sub Calculation(ByRef otherdata As Class_money.OTHER_DATA, ByRef errMsgShow As String)

        'ポストバックで画面が上に戻るのを防ぐ。（暫定・・）
        TextComment2.Focus()

        '***セッション情報取得***
        Dim userid As String = Session("user_id")
        Dim shipCode As String = Session("ship_code")

        If shipCode Is Nothing Then
            errMsgShow = "The session has expired. Please login again."
            Exit Sub
        End If

        Dim clsSetRepair As New Class_repair
        Dim clsSet As New Class_money
        Dim errMsg As String

        '***post番号チェック***
        Dim postCord As String = Trim(Left(TextPostal_Code2.Text, 2))
        Dim stateName As String = DropListState.Text
        If clsSet.chkPostCord(postCord, stateName, errMsg) = False Then
            If errMsg <> "" Then
                errMsgShow = errMsg
                TextPostal_Code2.Focus()
                Exit Sub
            End If
            errMsgShow = "Zip code and name do not match. <br/> 
                         Understand the code: Please press the mail button. <br/> Code unknown: Please leave the code blank."
            TextPostal_Code2.Focus()
            Exit Sub
        End If

        '***税金種別の確認***
        Dim taxKind As String

        If DropListState.Text = "" Or DropListState.Text = "select state" Then
            errMsgShow = "Please select the state name."
            Exit Sub
        Else
            Call chkApply(DropListState.Text, taxKind, errMsg)
            If errMsg <> "" Then
                errMsgShow = errMsg
                Exit Sub
            End If
        End If

        Dim unitPrice As String = ""
        Dim gUnitPrice As String = ""
        Dim pricePartsSum As Decimal
        Dim PriceLaborSum As Decimal
        Dim priceOtherShipSum As Decimal
        Dim gPricePartsSum As Decimal
        Dim gPriceLaborSum As Decimal
        Dim gPriceOtherSum As Decimal
        Dim gPriceParts1d As Decimal
        Dim gPriceParts2d As Decimal
        Dim gPriceParts3d As Decimal
        Dim gPriceParts4d As Decimal
        Dim gPriceParts5d As Decimal
        Dim gPriceShipMentSum As Decimal
        Dim gpriceOtherShipSum As Decimal
        Dim name As String

        Call reSet2()

        '***作業費・部品費用・その他費用・郵送費情報設定***
        '作業費用
        If TextLaborAmount.Text <> "" Then

            '部品コード名
            Dim laborAmount As String = Trim(TextLaborAmount.Text)

            '個数
            Dim QtyLabor As Integer
            If TextQtyLabor.Text <> "" Then
                If Integer.TryParse(Trim(TextQtyLabor.Text), QtyLabor) = False Then
                    errMsgShow = "Please enter a numerical value."
                    TextQtyLabor.BackColor = System.Drawing.Color.FromArgb(255, 0, 0)
                    Exit Sub
                Else
                    QtyLabor = Convert.ToInt32(Trim(TextQtyLabor.Text))
                End If
            Else
                errMsgShow = "Please check the number."
                TextQtyLabor.BackColor = System.Drawing.Color.FromArgb(255, 0, 0)
                Exit Sub
            End If

            '単価情報取得
            Call clsSetRepair.setPrice(laborAmount, shipCode, unitPrice, gUnitPrice, name, errMsg)
            If errMsg <> "" Then
                errMsgShow = errMsg
                If errMsg = "It does not exist in master. Please check the part name." Then
                    errMsgShow = "It does not exist in master. Please check the work cost name."
                    TextLaborAmount.BackColor = System.Drawing.Color.FromArgb(255, 0, 0)
                End If
                Exit Sub
            End If

            '**構造体へ設定**
            '単価設定
            otherdata.gUnitPriceLabor = gUnitPrice

            '名称設定
            otherdata.LaborName = name

            '単価×個数
            Dim priceLabor As String = "0"
            If unitPrice <> "" Then
                priceLabor = (Convert.ToDecimal(unitPrice) * QtyLabor).ToString
                PriceLaborSum = Convert.ToDecimal(unitPrice) * QtyLabor
            End If

            Dim gPriceLabor As String = "0"
            If gUnitPrice <> "" Then
                gPriceLabor = (Convert.ToDecimal(gUnitPrice) * QtyLabor).ToString
                gPriceLaborSum = Convert.ToDecimal(gUnitPrice) * QtyLabor
            End If

            '**画面へ設定**
            lblPriceLabor.Text = clsSet.setINR(priceLabor)
            lblGPriceLa.Text = clsSet.setINR(gPriceLabor)

        Else
            '作業費名称なし
            TextQtyLabor.Text = ""
        End If

        '※⇓部品①～⑤⇓
        Dim QtyParts1 As Integer
        If TextParts1.Text <> "" Then

            Dim priceParts1 As String = "0"
            Dim gPriceParts1 As String = "0"

            '部品コード名
            Dim partsName1 As String = Trim(TextParts1.Text)

            '個数
            If TextQtyParts1.Text <> "" Then
                If Integer.TryParse(Trim(TextQtyParts1.Text), QtyParts1) = False Then
                    errMsgShow = "Please enter a numerical value."
                    TextQtyParts1.BackColor = System.Drawing.Color.FromArgb(255, 0, 0)
                    Exit Sub
                Else
                    QtyParts1 = Convert.ToInt32(TextQtyParts1.Text)
                End If
            Else
                errMsgShow = "Please check the number."
                TextQtyParts1.BackColor = System.Drawing.Color.FromArgb(255, 0, 0)
                Exit Sub
            End If

            '単価情報取得
            Call clsSetRepair.setPrice(partsName1, shipCode, unitPrice, gUnitPrice, name, errMsg)
            If errMsg <> "" Then
                errMsgShow = errMsg
                If errMsg = "It does not exist in master. Please check the part name." Then
                    TextParts1.BackColor = System.Drawing.Color.FromArgb(255, 0, 0)
                End If
                Exit Sub
            End If

            '単価設定
            otherdata.gUnitPriceParts1 = gUnitPrice

            '名称設定
            otherdata.Parts1Name = name

            '単価×個数
            If unitPrice <> "" Then
                priceParts1 = (Convert.ToDecimal(unitPrice) * QtyParts1).ToString
                pricePartsSum = pricePartsSum + Convert.ToDecimal(unitPrice) * QtyParts1
            End If

            If gUnitPrice <> "" Then
                gPriceParts1 = (Convert.ToDecimal(gUnitPrice) * QtyParts1).ToString
                gPriceParts1d = Convert.ToDecimal(gUnitPrice) * QtyParts1
                gPricePartsSum = gPricePartsSum + Convert.ToDecimal(gUnitPrice) * QtyParts1
            End If

            '設定
            lblPricePa1.Text = clsSet.setINR(priceParts1)
            lblGPricePa1.Text = clsSet.setINR(gPriceParts1)
        Else
            '部品名空欄のときは、個数も空欄を設定
            TextQtyParts1.Text = ""
        End If

        '個数がないときは、名称を空欄に設定
        If QtyParts1 = 0 Then
            TextParts1.Text = ""
        End If

        Dim QtyParts2 As Integer
        If TextParts2.Text <> "" Then

            Dim priceParts2 As String = "0"
            Dim gPriceParts2 As String = "0"

            '部品コード名
            Dim partsName2 As String = Trim(TextParts2.Text)

            '個数
            If TextQtyParts2.Text <> "" Then
                If Integer.TryParse(Trim(TextQtyParts2.Text), QtyParts2) = False Then
                    errMsgShow = "Please enter a numerical value."
                    TextQtyParts2.BackColor = System.Drawing.Color.FromArgb(255, 0, 0)
                    Exit Sub
                Else
                    QtyParts2 = Convert.ToInt32(TextQtyParts2.Text)
                End If
            Else
                errMsgShow = "Please check the number."
                TextQtyParts2.BackColor = System.Drawing.Color.FromArgb(255, 0, 0)
                Exit Sub
            End If

            '単価情報取得
            Call clsSetRepair.setPrice(partsName2, shipCode, unitPrice, gUnitPrice, name, errMsg)
            If errMsg <> "" Then
                errMsgShow = errMsg
                If errMsg = "It does not exist in master. Please check the part name." Then
                    TextParts2.BackColor = System.Drawing.Color.FromArgb(255, 0, 0)
                End If
                Exit Sub
            End If

            '単価設定
            otherdata.gUnitPriceParts2 = gUnitPrice

            '名称設定
            otherdata.Parts2Name = name

            '単価×個数
            If unitPrice <> "" Then
                priceParts2 = (Convert.ToDecimal(unitPrice) * QtyParts2).ToString
                pricePartsSum = pricePartsSum + Convert.ToDecimal(unitPrice) * QtyParts2
            End If

            If gUnitPrice <> "" Then
                gPriceParts2 = (Convert.ToDecimal(gUnitPrice) * QtyParts2).ToString
                gPriceParts2d = Convert.ToDecimal(gUnitPrice) * QtyParts2
                gPricePartsSum = gPricePartsSum + Convert.ToDecimal(gUnitPrice) * QtyParts2
            End If

            '設定
            lblPricePa2.Text = clsSet.setINR(priceParts2)
            lblGPricePa2.Text = clsSet.setINR(gPriceParts2)

        Else
            TextQtyParts2.Text = ""
        End If

        '個数がないときは、名称を空欄に設定
        If QtyParts2 = 0 Then
            TextParts2.Text = ""
        End If

        Dim QtyParts3 As Integer
        If TextParts3.Text <> "" Then

            Dim priceParts3 As String = "0"
            Dim gPriceParts3 As String = "0"

            '部品コード名
            Dim partsName3 As String = Trim(TextParts3.Text)

            '個数
            If TextQtyParts3.Text <> "" Then
                If Integer.TryParse(Trim(TextQtyParts3.Text), QtyParts3) = False Then
                    errMsgShow = "Please enter a numerical value."
                    TextQtyParts3.BackColor = System.Drawing.Color.FromArgb(255, 0, 0)
                    Exit Sub
                Else
                    QtyParts3 = Convert.ToInt32(TextQtyParts3.Text)
                End If
            Else
                errMsgShow = "Please check the number."
                TextQtyParts3.BackColor = System.Drawing.Color.FromArgb(255, 0, 0)
                Exit Sub
            End If

            '単価情報取得
            Call clsSetRepair.setPrice(partsName3, shipCode, unitPrice, gUnitPrice, name, errMsg)
            If errMsg <> "" Then
                errMsgShow = errMsg
                If errMsg = "It does not exist in master. Please check the part name." Then
                    TextParts3.BackColor = System.Drawing.Color.FromArgb(255, 0, 0)
                End If
                Exit Sub
            End If

            '単価設定
            otherdata.gUnitPriceParts3 = gUnitPrice

            '名称設定
            otherdata.Parts3Name = name

            '単価×個数
            If unitPrice <> "" Then
                priceParts3 = (Convert.ToDecimal(unitPrice) * QtyParts3).ToString
                pricePartsSum = pricePartsSum + Convert.ToDecimal(unitPrice) * QtyParts3
            End If

            If gUnitPrice <> "" Then
                gPriceParts3 = (Convert.ToDecimal(gUnitPrice) * QtyParts3).ToString
                gPriceParts3d = Convert.ToDecimal(gUnitPrice) * QtyParts3
                gPricePartsSum = gPricePartsSum + Convert.ToDecimal(gUnitPrice) * QtyParts3
            End If

            '設定
            lblPricePa3.Text = clsSet.setINR(priceParts3)
            lblGPricePa3.Text = clsSet.setINR(gPriceParts3)

        Else
            TextQtyParts3.Text = ""
        End If

        '個数がないときは、名称を空欄に設定
        If QtyParts3 = 0 Then
            TextParts3.Text = ""
        End If

        Dim QtyParts4 As Integer
        If TextParts4.Text <> "" Then

            Dim priceParts4 As String = "0"
            Dim gPriceParts4 As String = "0"

            '部品コード名
            Dim partsName4 As String = Trim(TextParts4.Text)

            '個数
            If TextQtyParts4.Text <> "" Then
                If Integer.TryParse(Trim(TextQtyParts4.Text), QtyParts4) = False Then
                    errMsgShow = "Please enter a numerical value."
                    TextQtyParts4.BackColor = System.Drawing.Color.FromArgb(255, 0, 0)
                    Exit Sub
                Else
                    QtyParts4 = Convert.ToInt32(TextQtyParts4.Text)
                End If
            Else
                errMsgShow = "Please check the number."
                TextQtyParts4.BackColor = System.Drawing.Color.FromArgb(255, 0, 0)
                Exit Sub
            End If

            '単価情報取得
            Call clsSetRepair.setPrice(partsName4, shipCode, unitPrice, gUnitPrice, name, errMsg)
            If errMsg <> "" Then
                errMsgShow = errMsg
                If errMsg = "It does not exist in master. Please check the part name." Then
                    TextParts4.BackColor = System.Drawing.Color.FromArgb(255, 0, 0)
                End If
                Exit Sub
            End If

            '単価設定
            otherdata.gUnitPriceParts4 = gUnitPrice

            '名称設定
            otherdata.Parts4Name = name

            '単価×個数
            If unitPrice <> "" Then
                priceParts4 = (Convert.ToDecimal(unitPrice) * QtyParts4).ToString
                pricePartsSum = pricePartsSum + Convert.ToDecimal(unitPrice) * QtyParts4
            End If

            If gUnitPrice <> "" Then
                gPriceParts4 = (Convert.ToDecimal(gUnitPrice) * QtyParts4).ToString
                gPriceParts4d = Convert.ToDecimal(gUnitPrice) * QtyParts4
                gPricePartsSum = gPricePartsSum + Convert.ToDecimal(gUnitPrice) * QtyParts4
            End If

            '設定
            lblPricePa4.Text = clsSet.setINR(priceParts4)
            lblGPricePa4.Text = clsSet.setINR(gPriceParts4)
        Else
            TextQtyParts4.Text = ""
        End If

        '個数がないときは、名称を空欄に設定
        If QtyParts4 = 0 Then
            TextParts4.Text = ""
        End If

        Dim QtyParts5 As Integer
        If TextParts5.Text <> "" Then

            Dim priceParts5 As String = "0"
            Dim gPriceParts5 As String = "0"

            '部品コード名
            Dim partsName5 As String = Trim(TextParts5.Text)

            '個数
            If TextQtyParts5.Text <> "" Then
                If Integer.TryParse(Trim(TextQtyParts5.Text), QtyParts5) = False Then
                    errMsgShow = "Please enter a numerical value."
                    TextQtyParts5.BackColor = System.Drawing.Color.FromArgb(255, 0, 0)
                    Exit Sub
                Else
                    QtyParts5 = Convert.ToInt32(TextQtyParts5.Text)
                End If
            Else
                errMsgShow = "Please check the number."
                TextQtyParts5.BackColor = System.Drawing.Color.FromArgb(255, 0, 0)
                Exit Sub
            End If

            '単価情報取得
            Call clsSetRepair.setPrice(partsName5, shipCode, unitPrice, gUnitPrice, name, errMsg)
            If errMsg <> "" Then
                errMsgShow = errMsg
                If errMsg = "It does not exist in master. Please check the part name." Then
                    TextParts5.BackColor = System.Drawing.Color.FromArgb(255, 0, 0)
                End If
                Exit Sub
            End If

            '単価設定
            otherdata.gUnitPriceParts5 = gUnitPrice

            '名称設定
            otherdata.Parts5Name = name

            '単価×個数
            If unitPrice <> "" Then
                priceParts5 = (Convert.ToDecimal(unitPrice) * QtyParts5).ToString
                pricePartsSum = pricePartsSum + Convert.ToDecimal(unitPrice) * QtyParts5
            End If

            If gUnitPrice <> "" Then
                gPriceParts5 = (Convert.ToDecimal(gUnitPrice) * QtyParts5).ToString
                gPriceParts5d = Convert.ToDecimal(gUnitPrice) * QtyParts5
                gPricePartsSum = gPricePartsSum + Convert.ToDecimal(gUnitPrice) * QtyParts5
            End If

            '設定
            lblPricePa5.Text = clsSet.setINR(priceParts5)
            lblGPricePa5.Text = clsSet.setINR(gPriceParts5)
        Else
            TextQtyParts5.Text = ""
        End If

        '個数がないときは、名称を空欄に設定
        If QtyParts5 = 0 Then
            TextParts5.Text = ""
        End If

        'その他
        Dim QtyOther As Integer
        If TextOther.Text <> "" Then

            'その他費用NO
            Dim otherNo As String = Trim(TextOther.Text)

            '個数
            If TextQtyOther.Text <> "" Then
                If Integer.TryParse(Trim(TextQtyOther.Text), QtyOther) = False Then
                    errMsgShow = "Please enter a numerical value."
                    TextQtyOther.BackColor = System.Drawing.Color.FromArgb(255, 0, 0)
                    Exit Sub
                Else
                    QtyOther = Convert.ToInt32(TextQtyOther.Text)
                End If
            Else
                errMsgShow = "Please check the number."
                TextQtyOther.BackColor = System.Drawing.Color.FromArgb(255, 0, 0)
                Exit Sub
            End If

            '単価情報取得
            Call clsSetRepair.setPrice(otherNo, shipCode, unitPrice, gUnitPrice, name, errMsg)
            If errMsg <> "" Then
                errMsgShow = errMsg
                If errMsg = "It does not exist in master. Please check the part name." Then
                    errMsgShow = "It does not exist in master. Please check other cost names."
                    TextOther.BackColor = System.Drawing.Color.FromArgb(255, 0, 0)
                End If
                Exit Sub
            End If

            '単価設定
            otherdata.gUnitPriceOther = gUnitPrice

            '名称設定
            otherdata.OtherName = name

            '単価×個数
            Dim priceOther As String = "0"
            If unitPrice <> "" Then
                priceOther = (Convert.ToDecimal(unitPrice) * QtyOther).ToString
                priceOtherShipSum = Convert.ToDecimal(unitPrice) * QtyOther
            End If

            Dim gPriceOther As String = "0"
            If gUnitPrice <> "" Then
                gPriceOther = (Convert.ToDecimal(gUnitPrice) * QtyOther).ToString
                gPriceOtherSum = Convert.ToDecimal(gUnitPrice) * QtyOther
                gpriceOtherShipSum = Convert.ToDecimal(gUnitPrice) * QtyOther
            End If

            '設定
            lblPriceOther.Text = clsSet.setINR(priceOther)
            lblGPriceOther.Text = clsSet.setINR(gPriceOther)

        Else
            'その他費用ID未入力であれば、その他費用数量もなし
            TextQtyOther.Text = ""
        End If

        '個数がないときは、名称を空欄に設定
        If QtyOther = 0 Then
            TextOther.Text = ""
        End If

        '郵送費
        Dim QtyShipMent As Integer
        If TextShipMent.Text <> "" Then

            '郵送費費用NO
            Dim shipMentNo As String = Trim(TextShipMent.Text)

            '個数
            If TextQtyShipMent.Text <> "" Then
                If Integer.TryParse(Trim(TextQtyShipMent.Text), QtyShipMent) = False Then
                    errMsgShow = "Please enter a numerical value."
                    TextQtyShipMent.BackColor = System.Drawing.Color.FromArgb(255, 0, 0)
                    Exit Sub
                Else
                    QtyShipMent = Convert.ToInt32(TextQtyShipMent.Text)
                End If
            Else
                errMsgShow = "Please check the number."
                TextQtyShipMent.BackColor = System.Drawing.Color.FromArgb(255, 0, 0)
                Exit Sub
            End If

            '単価情報取得
            Call clsSetRepair.setPrice(shipMentNo, shipCode, unitPrice, gUnitPrice, name, errMsg)
            If errMsg <> "" Then
                errMsgShow = errMsg
                If errMsg = "It does not exist in master. Please check the part name." Then
                    errMsgShow = "It does not exist in master. Please check the postage cost name."
                    TextShipMent.BackColor = System.Drawing.Color.FromArgb(255, 0, 0)
                End If
                Exit Sub
            End If

            '単価設定
            otherdata.gUnitPriceShipMent = gUnitPrice

            '名称設定
            otherdata.ShipMentName = name

            '単価×個数
            Dim priceShipMent As String = "0"
            If unitPrice <> "" Then
                priceShipMent = (Convert.ToDecimal(unitPrice) * QtyShipMent).ToString
                priceOtherShipSum = priceOtherShipSum + (Convert.ToDecimal(unitPrice) * QtyShipMent)
            End If

            Dim gPriceShipMent As String = "0"
            If gUnitPrice <> "" Then
                gPriceShipMent = (Convert.ToDecimal(gUnitPrice) * QtyShipMent).ToString
                gPriceShipMentSum = Convert.ToDecimal(gUnitPrice) * QtyShipMent
                gpriceOtherShipSum = gpriceOtherShipSum + (Convert.ToDecimal(gUnitPrice) * QtyShipMent)
            End If

            '設定
            lblPriceShip.Text = clsSet.setINR(priceShipMent)
            lblGPriceShip.Text = clsSet.setINR(gPriceShipMent)

        Else
            'その他費用ID未入力であれば、その他費用数量もなし
            TextQtyShipMent.Text = ""
        End If

        '個数がないときは、名称を空欄に設定
        If QtyShipMent = 0 Then
            TextShipMent.Text = ""
        End If

        '**部品qty合計**
        lblQtySum1.Text = (QtyParts1 + QtyParts2 + QtyParts3 + QtyParts4 + QtyParts5)

        '**部品price合計**
        lblPartsSum.Text = clsSet.setINR(pricePartsSum.ToString)
        lblGPartsSum.Text = clsSet.setINR(gPricePartsSum.ToString)

        '**その他費用、郵送費の合計**
        'qty
        lblQtySum2.Text = (QtyOther + QtyShipMent)
        'price
        lblOtherSum.Text = clsSet.setINR(priceOtherShipSum.ToString)
        lblGOtherSum.Text = clsSet.setINR(gpriceOtherShipSum.ToString)

        '**税金料・税込み(作業費に対する)**
        If taxKind = "SGST" Then
            '■同じ拠点
            '税抜き
            lblUTSGTLa.Text = clsSet.setINR((gPriceLaborSum * clsSet.SGST).ToString)
            lblCGSTLa.Text = clsSet.setINR((gPriceLaborSum * clsSet.CGST).ToString)
            '税込み
            lblSum1.Text = clsSet.setINR((gPriceLaborSum + (gPriceLaborSum * clsSet.SGST) + (gPriceLaborSum * clsSet.CGST)).ToString)
        ElseIf taxKind = "IGST" Then
            '■拠点外
            '税抜き
            lblIGSTLa.Text = clsSet.setINR((gPriceLaborSum * clsSet.IGST).ToString)
            '税込み
            lblSum1.Text = clsSet.setINR((gPriceLaborSum + (gPriceLaborSum * clsSet.IGST)).ToString)
        End If

        '**税金料・税込み（部品、その他費用、郵送費に対する）**
        If taxKind = "SGST" Then
            '■同じ拠点
            '□部品合計に対する税料金
            lblUTSGTPa.Text = clsSet.setINR((gPricePartsSum * clsSet.SGST).ToString)
            lblCGSTPa.Text = clsSet.setINR((gPricePartsSum * clsSet.CGST).ToString)

            '□部品①に対する
            '税料金
            lblp1SGST.Text = clsSet.setINR((gPriceParts1d * clsSet.SGST).ToString)
            lblp1CGST.Text = clsSet.setINR((gPriceParts1d * clsSet.CGST).ToString)
            '税込み
            lblSum2_1.Text = clsSet.setINR((gPriceParts1d + (gPriceParts1d * clsSet.SGST) + (gPriceParts1d * clsSet.CGST)).ToString)

            '□部品②に対する
            '税料金
            lblp2SGST.Text = clsSet.setINR((gPriceParts2d * clsSet.SGST).ToString)
            lblp2CGST.Text = clsSet.setINR((gPriceParts2d * clsSet.CGST).ToString)
            '税込み
            lblSum2_2.Text = clsSet.setINR((gPriceParts2d + (gPriceParts2d * clsSet.SGST) + (gPriceParts2d * clsSet.CGST)).ToString)

            '□部品③に対する
            '税料金
            lblp3SGST.Text = clsSet.setINR((gPriceParts3d * clsSet.SGST).ToString)
            lblp3CGST.Text = clsSet.setINR((gPriceParts3d * clsSet.CGST).ToString)
            '税込み
            lblSum2_3.Text = clsSet.setINR((gPriceParts3d + (gPriceParts3d * clsSet.SGST) + (gPriceParts3d * clsSet.CGST)).ToString)

            '□部品④に対する
            '税料金
            lblp4SGST.Text = clsSet.setINR((gPriceParts4d * clsSet.SGST).ToString)
            lblp4CGST.Text = clsSet.setINR((gPriceParts4d * clsSet.CGST).ToString)
            '税込み
            lblSum2_4.Text = clsSet.setINR((gPriceParts4d + (gPriceParts4d * clsSet.SGST) + (gPriceParts4d * clsSet.CGST)).ToString)

            '□部品⑤に対する
            '税料金
            lblp5SGST.Text = clsSet.setINR((gPriceParts5d * clsSet.SGST).ToString)
            lblp5CGST.Text = clsSet.setINR((gPriceParts5d * clsSet.CGST).ToString)
            '税込み
            lblSum2_5.Text = clsSet.setINR((gPriceParts5d + (gPriceParts5d * clsSet.SGST) + (gPriceParts5d * clsSet.CGST)).ToString)

            '部品合計（税込み）（parts1～parts5）
            lblSumPa.Text = clsSet.setINR((gPricePartsSum + (gPricePartsSum * clsSet.SGST) + (gPricePartsSum * clsSet.CGST)).ToString)

            '□その他費用に対する
            '税料金
            lbloSGST.Text = clsSet.setINR((gPriceOtherSum * clsSet.SGST).ToString)
            lbloCGST.Text = clsSet.setINR((gPriceOtherSum * clsSet.CGST).ToString)
            '税込み
            lblSumO.Text = clsSet.setINR((gPriceOtherSum + (gPriceOtherSum * clsSet.SGST) + (gPriceOtherSum * clsSet.CGST)).ToString)

            '□郵送費に対する
            '税料金
            lblsSGST.Text = clsSet.setINR((gPriceShipMentSum * clsSet.SGST).ToString)
            lblsCGST.Text = clsSet.setINR((gPriceShipMentSum * clsSet.CGST).ToString)
            '税込み
            lblSumS.Text = clsSet.setINR((gPriceShipMentSum + (gPriceShipMentSum * clsSet.SGST) + (gPriceShipMentSum * clsSet.CGST)).ToString)

            '□その他、郵送費合計に対する
            '税料金
            lblUTSGTO.Text = clsSet.setINR((gpriceOtherShipSum * clsSet.SGST).ToString)
            lblCGSTO.Text = clsSet.setINR((gpriceOtherShipSum * clsSet.CGST).ToString)
            '税込み
            lblSum4.Text = clsSet.setINR((gpriceOtherShipSum + (gpriceOtherShipSum * clsSet.SGST) + (gpriceOtherShipSum * clsSet.CGST)).ToString)

        ElseIf taxKind = "IGST" Then
            '■拠点外
            '□部品合計に対する税料金
            lblIGSTPa.Text = clsSet.setINR((gPricePartsSum * clsSet.IGST).ToString)

            '□部品①に対する
            '税料金
            lblp1IGST.Text = clsSet.setINR((gPriceParts1d * clsSet.IGST).ToString)
            '税込み
            lblSum2_1.Text = clsSet.setINR((gPriceParts1d + (gPriceParts1d * clsSet.IGST)).ToString)

            '□部品②に対する
            '税料金
            lblp2IGST.Text = clsSet.setINR((gPriceParts2d * clsSet.IGST).ToString)
            '税込み
            lblSum2_2.Text = clsSet.setINR((gPriceParts2d + (gPriceParts2d * clsSet.IGST)).ToString)

            '□部品③に対する
            '税料金
            lblp3IGST.Text = clsSet.setINR((gPriceParts3d * clsSet.IGST).ToString)
            '税込み
            lblSum2_3.Text = clsSet.setINR((gPriceParts3d + (gPriceParts3d * clsSet.IGST)).ToString)

            '□部品④に対する
            '税料金
            lblp4IGST.Text = clsSet.setINR((gPriceParts4d * clsSet.IGST).ToString)
            '税込み
            lblSum2_4.Text = clsSet.setINR((gPriceParts4d + (gPriceParts4d * clsSet.IGST)).ToString)

            '□部品⑤に対する
            '税料金
            lblp5IGST.Text = clsSet.setINR((gPriceParts5d * clsSet.IGST).ToString)
            '税込み
            lblSum2_5.Text = clsSet.setINR((gPriceParts5d + (gPriceParts5d * clsSet.IGST)).ToString)

            '部品合計（税込み）（parts1～parts5）
            lblSumPa.Text = clsSet.setINR((gPricePartsSum + (gPricePartsSum * clsSet.IGST)).ToString)

            '□その他費用に対する
            '税料金
            lbloIGST.Text = clsSet.setINR((gPriceOtherSum * clsSet.IGST).ToString)
            '税込み
            lblSumO.Text = clsSet.setINR((gPriceOtherSum + (gPriceOtherSum * clsSet.IGST)).ToString)

            '□郵送費に対する
            '税料金
            lblsIGST.Text = clsSet.setINR((gPriceShipMentSum * clsSet.IGST).ToString)
            '税込み
            lblSumS.Text = clsSet.setINR((gPriceShipMentSum + (gPriceShipMentSum * clsSet.IGST)).ToString)

            '□その他費用、郵送費合計に対する
            '税料金
            lblIGSTO.Text = clsSet.setINR((gpriceOtherShipSum * clsSet.IGST).ToString)
            '税込み
            lblSum4.Text = clsSet.setINR((gpriceOtherShipSum + (gpriceOtherShipSum * clsSet.IGST)).ToString)

        End If

        '**total**
        lblSumCost.Text = clsSet.setINR((PriceLaborSum + pricePartsSum + priceOtherShipSum).ToString)
        lblSumSales.Text = clsSet.setINR((gPriceLaborSum + gPricePartsSum + gpriceOtherShipSum).ToString)

        If taxKind = "SGST" Then
            '拠点内
            lblSumSGST.Text = clsSet.setINR((gPriceLaborSum * clsSet.SGST) + (gPricePartsSum * clsSet.SGST) + (gpriceOtherShipSum * clsSet.SGST))
            lblSumCGST.Text = clsSet.setINR((gPriceLaborSum * clsSet.CGST) + (gPricePartsSum * clsSet.CGST) + (gpriceOtherShipSum * clsSet.CGST))
            lblTotal.Text = clsSet.setINR(((gPriceLaborSum + (gPriceLaborSum * clsSet.SGST) + (gPriceLaborSum * clsSet.CGST))) + ((gPricePartsSum + (gPricePartsSum * clsSet.SGST) + (gPricePartsSum * clsSet.CGST))) + ((gpriceOtherShipSum + (gpriceOtherShipSum * clsSet.SGST) + (gpriceOtherShipSum * clsSet.CGST))))
            lblTotalAmount.Text = clsSet.setINR(((gPriceLaborSum + (gPriceLaborSum * clsSet.SGST) + (gPriceLaborSum * clsSet.CGST))) + ((gPricePartsSum + (gPricePartsSum * clsSet.SGST) + (gPricePartsSum * clsSet.CGST))) + ((gpriceOtherShipSum + (gpriceOtherShipSum * clsSet.SGST) + (gpriceOtherShipSum * clsSet.CGST))))
        ElseIf taxKind = "IGST" Then
            '拠点外
            lblSumIGST.Text = clsSet.setINR((gPriceLaborSum * clsSet.IGST) + (gPricePartsSum * clsSet.IGST) + (gpriceOtherShipSum * clsSet.IGST))
            lblTotal.Text = clsSet.setINR((gPriceLaborSum + (gPriceLaborSum * clsSet.IGST)) + (gPricePartsSum + (gPricePartsSum * clsSet.IGST)) + (gpriceOtherShipSum + (gpriceOtherShipSum * clsSet.IGST)))
            lblTotalAmount.Text = clsSet.setINR((gPriceLaborSum + (gPriceLaborSum * clsSet.IGST)) + (gPricePartsSum + (gPricePartsSum * clsSet.IGST)) + (gpriceOtherShipSum + (gpriceOtherShipSum * clsSet.IGST)))
        End If

    End Sub

    Protected Sub BtnLastOK_Click(sender As Object, e As EventArgs) Handles BtnLastOK.Click

    End Sub

    Protected Sub Btn2LastOK_Click(sender As Object, e As EventArgs) Handles Btn2LastOK.Click

    End Sub

    Protected Sub Return_Click(sender As Object, e As EventArgs) Handles [Return].Click

        Dim userLevel As String = Session("user_level")
        Dim adminFlg As Boolean = Session("admin_Flg")

        If userLevel = "0" Or userLevel = "1" Or userLevel = "2" Or adminFlg = True Then
            Response.Redirect("Menu.aspx")
        Else
            Response.Redirect("Menu2.aspx")
        End If

    End Sub

    Protected Sub btnBack_Click(sender As Object, e As ImageClickEventArgs) Handles btnBack.Click

        Response.Redirect("Money_Record_3.aspx")

    End Sub

End Class