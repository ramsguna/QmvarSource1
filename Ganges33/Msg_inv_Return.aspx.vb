Public Class Msg_inv_Return
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        Dim returnNo As String = Session("return_No")
        Dim allKindItem As Integer = Session("all_Kinditem")
        Dim allQty As Integer = Session("all_Qty")
        Dim partsStatus() As Class_inv.STATUS_Item
        If Session("parts_Status") IsNot Nothing Then
            partsStatus = Session("parts_Status")
        End If

        lblMsg.Visible = False
        'シリアルありの入荷で、登録できなかったアイテムのメッセージ出力
        If Session("parts_Status") IsNot Nothing Then

            Dim msgString As String = ""
            Dim msgCnt As Integer = 0

            For i = 0 To UBound(partsStatus)
                If partsStatus(i).status <> "cancel" Then
                    If partsStatus(i).status <> "2" Then
                        msgString &= partsStatus(i).filename & ":" & partsStatus(i).item & " "
                        msgCnt = msgCnt + 1
                    End If
                End If
            Next

            If msgString <> "" Then
                lblMsg.Visible = True
                lblMsgContent.Text = msgCnt & "件" & "<br>" & msgString
            End If

        End If

        '返却番号
        lblReturnNo.Text = "Re" & returnNo

        'itemの種類数と総個数の表示
        lblAllParts.Text = allQty
        lblKindParts.Text = allKindItem

        Session("all_Kinditem") = 0
        Session("all_Qty") = 0
        Session("return_No") = Nothing
        Session("partsStatu") = Nothing

    End Sub
    'backボタン押下処理
    Protected Sub btnBack_Click(sender As Object, e As ImageClickEventArgs) Handles btnBack.Click

        Response.Redirect("inv_Return.aspx")

    End Sub
End Class